using System.Diagnostics.Metrics;

namespace DIPS.Mobile.UI.API.Diagnostics;

/// <summary>
/// Service for capturing per-render layout diagnostics using the native MAUI metrics system.
/// Captures measure/arrange counts per element type during render windows.
/// </summary>
/// <remarks>
/// Metrics support is enabled by default in both Debug and Release builds.
/// The consumer must call <c>builder.Services.AddMetrics()</c> in MauiProgram.cs
/// to register the <c>IMeterFactory</c> required by MAUI's diagnostics system.
/// </remarks>
public static partial class LayoutDiagnosticsService
{
    private static MeterListener? s_listener;
    private static LayoutDiagnosticsSnapshot? s_currentSnapshot;
    private static readonly object s_lock = new();
    private static readonly List<LayoutDiagnosticsSnapshot> s_completedSnapshots = [];
    
    /// <summary>
    /// Raised when a snapshot is completed (render window ended and analyzed)
    /// </summary>
    public static event Action<LayoutDiagnosticsSnapshot>? SnapshotCompleted;
    
    /// <summary>
    /// Whether diagnostics are currently capturing
    /// </summary>
    public static bool IsEnabled { get; private set; }
    
    /// <summary>
    /// Whether the diagnostics system has been initialized (overlay is shown)
    /// </summary>
    public static bool IsInitialized { get; private set; }
    
    /// <summary>
    /// All completed snapshots since diagnostics were last enabled
    /// </summary>
    public static IReadOnlyList<LayoutDiagnosticsSnapshot> CompletedSnapshots => s_completedSnapshots;

    /// <summary>
    /// The currently active snapshot being captured, or null if no render is in progress
    /// </summary>
    public static LayoutDiagnosticsSnapshot? CurrentSnapshot => s_currentSnapshot;

    /// <summary>
    /// Initializes the diagnostics system and shows the floating overlay in a dormant state.
    /// Tap the overlay to toggle capturing on/off at runtime.
    /// </summary>
    public static void Initialize()
    {
        if (IsInitialized)
            return;
        
        IsInitialized = true;
        AttachOverlay();
    }

    /// <summary>
    /// Enables layout diagnostics capturing. Attaches a MeterListener to the Microsoft.Maui meter.
    /// </summary>
    public static void Enable()
    {
        if (IsEnabled)
            return;
        
        if (!IsInitialized)
            Initialize();
        
        IsEnabled = true;
        s_completedSnapshots.Clear();
        StartListening();
        SetOverlayRecording();
    }

    /// <summary>
    /// Disables layout diagnostics capturing. Disposes the MeterListener but keeps the overlay visible.
    /// </summary>
    /// <returns>All snapshots captured during this session</returns>
    public static IReadOnlyList<LayoutDiagnosticsSnapshot> Disable()
    {
        if (!IsEnabled)
            return [];
        
        IsEnabled = false;
        
        // Complete any in-progress snapshot before stopping
        lock (s_lock)
        {
            CompleteCurrentSnapshot();
        }
        
        StopListening();
        SetOverlayStopped();
        
        return s_completedSnapshots.ToList();
    }

    /// <summary>
    /// Toggles diagnostics capturing on/off. Called by the overlay tap gesture.
    /// </summary>
    public static void Toggle()
    {
        if (IsEnabled)
            Disable();
        else
            Enable();
    }

    /// <summary>
    /// Removes the overlay and fully tears down the diagnostics system.
    /// </summary>
    public static void Teardown()
    {
        Disable();
        RemoveOverlay();
        IsInitialized = false;
    }

    /// <summary>
    /// Clears all completed snapshots
    /// </summary>
    public static void ClearSnapshots()
    {
        s_completedSnapshots.Clear();
    }

    /// <summary>
    /// Exports all completed snapshots as a JSON string.
    /// Suitable for copying and sharing with developers for analysis.
    /// </summary>
    public static string ExportAllToJson()
    {
        var snapshots = s_completedSnapshots.Select(s => s.ToJson()).ToList();
        return $"[{string.Join(",", snapshots)}]";
    }

    /// <summary>
    /// Called internally when a new render window begins (page appearing, bottom sheet opening, etc.)
    /// </summary>
    internal static void BeginSnapshot(string sourceName)
    {
        if (!IsEnabled)
            return;

        lock (s_lock)
        {
            // Complete any existing snapshot first
            CompleteCurrentSnapshot();
            
            s_currentSnapshot = new LayoutDiagnosticsSnapshot(sourceName);
        }
    }

    /// <summary>
    /// Called internally when a render window ends (page disappeared, bottom sheet closed, etc.)
    /// </summary>
    internal static void EndSnapshot()
    {
        if (!IsEnabled)
            return;

        lock (s_lock)
        {
            CompleteCurrentSnapshot();
        }
    }

    private static void CompleteCurrentSnapshot()
    {
        if (s_currentSnapshot is null)
            return;

        // Discard empty snapshots (e.g. back-navigation where nothing re-renders)
        if (s_currentSnapshot.TotalMeasureCount == 0 && s_currentSnapshot.TotalArrangeCount == 0)
        {
            s_currentSnapshot = null;
            return;
        }

        s_currentSnapshot.Analyze();
        s_completedSnapshots.Add(s_currentSnapshot);
        
        var snapshot = s_currentSnapshot;
        s_currentSnapshot = null;
        
        UpdateOverlay(snapshot);
        SnapshotCompleted?.Invoke(snapshot);
    }

    private static void StartListening()
    {
        s_listener = new MeterListener();

        s_listener.InstrumentPublished = (instrument, listener) =>
        {
            if (instrument.Meter.Name == "Microsoft.Maui")
            {
                listener.EnableMeasurementEvents(instrument);
            }
        };

        s_listener.SetMeasurementEventCallback<int>(OnMeasurementRecorded);
        s_listener.Start();
    }

    private static void StopListening()
    {
        s_listener?.Dispose();
        s_listener = null;
    }

    private static void OnMeasurementRecorded(
        Instrument instrument,
        int measurement,
        ReadOnlySpan<KeyValuePair<string, object?>> tags,
        object? state)
    {
        RecordMeasurement(instrument.Name, measurement, tags);
    }

    private static void RecordMeasurement(
        string instrumentName,
        int measurement,
        ReadOnlySpan<KeyValuePair<string, object?>> tags)
    {
        if (s_currentSnapshot is null)
            return;

        var elementType = "Unknown";
        var elementId = Guid.Empty;
        foreach (var tag in tags)
        {
            if (tag.Key == "element.type" && tag.Value is string type)
            {
                elementType = type;
            }
            else if (tag.Key == "element.id" && tag.Value is Guid id)
            {
                elementId = id;
            }
        }

        lock (s_lock)
        {
            if (s_currentSnapshot is null)
                return;
            
            switch (instrumentName)
            {
                case "maui.layout.measure_count":
                    s_currentSnapshot.RecordMeasure(elementType, elementId);
                    break;
                case "maui.layout.measure_duration":
                    s_currentSnapshot.RecordMeasureDuration(elementType, measurement);
                    break;
                case "maui.layout.arrange_count":
                    s_currentSnapshot.RecordArrange(elementType, elementId);
                    break;
                case "maui.layout.arrange_duration":
                    s_currentSnapshot.RecordArrangeDuration(elementType, measurement);
                    break;
            }
        }
    }

    private static partial void AttachOverlay();
    private static partial void RemoveOverlay();
    private static partial void UpdateOverlay(LayoutDiagnosticsSnapshot snapshot);
    private static partial void SetOverlayRecording();
    private static partial void SetOverlayStopped();
}
