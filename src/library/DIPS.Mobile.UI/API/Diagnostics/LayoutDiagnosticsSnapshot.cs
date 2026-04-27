using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DIPS.Mobile.UI.API.Diagnostics;

/// <summary>
/// A snapshot of layout diagnostics captured during a render window (page appear, bottom sheet open, etc.)
/// </summary>
public class LayoutDiagnosticsSnapshot
{
    internal LayoutDiagnosticsSnapshot(string sourceName)
    {
        SourceName = sourceName;
        StartedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// The name of the source that triggered this snapshot (e.g. page type name, bottom sheet type name)
    /// </summary>
    public string SourceName { get; }
    
    /// <summary>
    /// When the snapshot capture started
    /// </summary>
    public DateTime StartedAt { get; }
    
    /// <summary>
    /// When the snapshot capture ended
    /// </summary>
    public DateTime? EndedAt { get; internal set; }
    
    /// <summary>
    /// Total measure operations during this snapshot
    /// </summary>
    public int TotalMeasureCount { get; internal set; }
    
    /// <summary>
    /// Total arrange operations during this snapshot
    /// </summary>
    public int TotalArrangeCount { get; internal set; }
    
    /// <summary>
    /// Measure counts per element type (full type name → count)
    /// </summary>
    public Dictionary<string, int> MeasureCountsByType { get; } = new();
    
    /// <summary>
    /// Arrange counts per element type (full type name → count)
    /// </summary>
    public Dictionary<string, int> ArrangeCountsByType { get; } = new();

    /// <summary>
    /// Unique element instance IDs seen per type during measure
    /// </summary>
    public Dictionary<string, HashSet<Guid>> MeasureInstancesByType { get; } = new();

    /// <summary>
    /// Unique element instance IDs seen per type during arrange
    /// </summary>
    public Dictionary<string, HashSet<Guid>> ArrangeInstancesByType { get; } = new();
    
    /// <summary>
    /// Warnings detected during this snapshot
    /// </summary>
    public List<string> Warnings { get; } = [];

    internal void RecordMeasure(string elementType, Guid elementId)
    {
        TotalMeasureCount++;
        
        if (!MeasureCountsByType.TryAdd(elementType, 1))
        {
            MeasureCountsByType[elementType]++;
        }

        if (!MeasureInstancesByType.TryGetValue(elementType, out var instances))
        {
            instances = new HashSet<Guid>();
            MeasureInstancesByType[elementType] = instances;
        }
        instances.Add(elementId);
    }

    internal void RecordMeasureDuration(string elementType, int durationNs)
    {
        // Duration recorded but not tracked — kept for future use
    }

    internal void RecordArrange(string elementType, Guid elementId)
    {
        TotalArrangeCount++;
        
        if (!ArrangeCountsByType.TryAdd(elementType, 1))
        {
            ArrangeCountsByType[elementType]++;
        }

        if (!ArrangeInstancesByType.TryGetValue(elementType, out var instances))
        {
            instances = new HashSet<Guid>();
            ArrangeInstancesByType[elementType] = instances;
        }
        instances.Add(elementId);
    }

    internal void RecordArrangeDuration(string elementType, int durationNs)
    {
        // Duration recorded but not tracked — kept for future use
    }

    internal void Analyze()
    {
        EndedAt = DateTime.UtcNow;
        
        // Warn when the average measures per instance is high — that indicates layout thrashing
        const double avgPerInstanceThreshold = 3.0;
        
        foreach (var (type, count) in MeasureCountsByType)
        {
            var instanceCount = MeasureInstancesByType.TryGetValue(type, out var inst) ? inst.Count : 0;
            var avgPerInstance = instanceCount > 0 ? (double)count / instanceCount : count;
            
            if (avgPerInstance > avgPerInstanceThreshold)
            {
                var shortName = GetShortTypeName(type);
                Warnings.Add(instanceCount == 1
                    ? $"{shortName} measured {count}\u00d7"
                    : $"{shortName} measured ~{avgPerInstance:F0}\u00d7 per instance ({instanceCount} instances)");
            }
        }
    }

    private static string GetShortTypeName(string fullTypeName)
    {
        var lastDot = fullTypeName.LastIndexOf('.');
        return lastDot >= 0 ? fullTypeName[(lastDot + 1)..] : fullTypeName;
    }

    /// <summary>
    /// Gets a compact one-line summary suitable for overlay display
    /// </summary>
    public string ToCompactString()
    {
        return $"M:{TotalMeasureCount} A:{TotalArrangeCount} ⚠️{Warnings.Count}";
    }
    
    /// <summary>
    /// Gets a detailed multi-line report
    /// </summary>
    public string ToDetailedString()
    {
        var sb = new StringBuilder();
        sb.AppendLine($"📊 Layout Diagnostics: {SourceName}");
        sb.AppendLine($"   Measures: {TotalMeasureCount}");
        sb.AppendLine($"   Arranges: {TotalArrangeCount}");
        
        if (Warnings.Count > 0)
        {
            sb.AppendLine($"   ⚠️ Warnings ({Warnings.Count}):");
            foreach (var warning in Warnings)
            {
                sb.AppendLine($"     - {warning}");
            }
        }

        var topMeasured = MeasureCountsByType
            .OrderByDescending(kvp => kvp.Value)
            .Take(5);
            
        sb.AppendLine("   Top measured:");
        foreach (var (type, count) in topMeasured)
        {
            var shortName = GetShortTypeName(type);
            sb.AppendLine($"     {shortName}: {count}");
        }

        return sb.ToString();
    }

    /// <summary>
    /// Exports the snapshot as a JSON string suitable for sharing with developers.
    /// </summary>
    public string ToJson()
    {
        var report = new Dictionary<string, object>
        {
            ["source"] = SourceName,
            ["startedAt"] = StartedAt.ToString("O"),
            ["endedAt"] = EndedAt?.ToString("O") ?? "",
            ["totalMeasures"] = TotalMeasureCount,
            ["totalArranges"] = TotalArrangeCount,
            ["warnings"] = Warnings,
            ["measuresByType"] = MeasureCountsByType
                .OrderByDescending(kvp => kvp.Value)
                .Select(kvp =>
                {
                    var instanceCount = MeasureInstancesByType.TryGetValue(kvp.Key, out var inst) ? inst.Count : 0;
                    return new Dictionary<string, object>
                    {
                        ["type"] = kvp.Key,
                        ["count"] = kvp.Value,
                        ["instances"] = instanceCount
                    };
                })
                .ToList(),
            ["arrangesByType"] = ArrangeCountsByType
                .OrderByDescending(kvp => kvp.Value)
                .Select(kvp =>
                {
                    var instanceCount = ArrangeInstancesByType.TryGetValue(kvp.Key, out var inst) ? inst.Count : 0;
                    return new Dictionary<string, object>
                    {
                        ["type"] = kvp.Key,
                        ["count"] = kvp.Value,
                        ["instances"] = instanceCount
                    };
                })
                .ToList()
        };

        return JsonSerializer.Serialize(report, new JsonSerializerOptions
        {
            WriteIndented = true
        });
    }
}
