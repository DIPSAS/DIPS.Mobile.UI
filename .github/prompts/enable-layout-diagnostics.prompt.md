# Enable Layout Diagnostics

Enable DIPS.Mobile.UI layout diagnostics in this app to track measure/arrange counts per page and bottom sheet.

## What you get
- A floating overlay pill (tap to expand) with Start/Stop recording and live snapshot info
- A Diagnostics tab in the Shell TabBar showing all captured snapshots, with warnings for types that have excessive layout passes
- JSON export of all snapshots via `LayoutDiagnosticsService.ExportAllToJson()`

## Steps

### 1. Add the metrics package

Add `<PackageReference Include="Microsoft.Extensions.Diagnostics" />` to the app's `.csproj`. If `Directory.Packages.props` is used, add it there with a version (e.g. `10.0.0`) and reference without version in the `.csproj`.

### 2. Register metrics in MauiProgram.cs

Add `builder.Services.AddMetrics()` **before** `.UseMauiApp<App>()`. This registers `IMeterFactory` which MAUI's diagnostics system requires to create its layout instruments.

```csharp
var builder = MauiApp.CreateBuilder();
builder.Services.AddMetrics(); // Required for MAUI diagnostics metrics
builder
    .UseMauiApp<App>()
    .UseDIPSUI(configurator => { ... });
```

### 3. Initialize the overlay

In `App.xaml.cs` (or wherever the Shell is constructed), call `LayoutDiagnosticsService.Initialize()` — this shows the floating overlay pill.

```csharp
using DIPS.Mobile.UI.API.Diagnostics;

// After creating the Shell:
LayoutDiagnosticsService.Initialize();
```

Optionally, build a diagnostics tab using the API (see reference implementations in `src/app/Components/DiagnosticsSamples/LayoutDiagnosticsPage.xaml` or `src/app/Playground/DiagnosticsSamples/LayoutDiagnosticsPage.xaml`).

### 4. Usage

1. Tap the overlay pill → expand → tap **Start**
2. Navigate between pages and open/close bottom sheets — each transition captures a snapshot
3. Go to the Diagnostics tab to see all snapshots with per-type measure/arrange counts
4. Types with avg > 3 layout passes per instance are flagged with ⚠️ warnings
5. Use `LayoutDiagnosticsService.ExportAllToJson()` to get all snapshot data as JSON

## API Reference

All types are in `DIPS.Mobile.UI.API.Diagnostics`.

### LayoutDiagnosticsService (static)

| Member | Description |
|---|---|
| `Initialize()` | Shows the floating overlay pill (dormant, not capturing) |
| `Enable()` | Starts capturing layout metrics |
| `Disable()` | Stops capturing, returns `IReadOnlyList<LayoutDiagnosticsSnapshot>` of all captured snapshots |
| `Toggle()` | Toggles between Enable/Disable |
| `Teardown()` | Removes overlay and fully tears down the system |
| `ClearSnapshots()` | Clears all completed snapshots |
| `ExportAllToJson()` | Returns all snapshots as a JSON string |
| `IsEnabled` | Whether diagnostics are currently capturing |
| `IsInitialized` | Whether the overlay has been initialized |
| `CompletedSnapshots` | `IReadOnlyList<LayoutDiagnosticsSnapshot>` of all captured snapshots |
| `CurrentSnapshot` | The in-progress snapshot, or null |
| `SnapshotCompleted` | `event Action<LayoutDiagnosticsSnapshot>` — raised when a snapshot finishes |

### LayoutDiagnosticsSnapshot

| Property | Type | Description |
|---|---|---|
| `SourceName` | `string` | Page or bottom sheet type name that triggered the snapshot |
| `StartedAt` | `DateTime` | When capture started (UTC) |
| `EndedAt` | `DateTime?` | When capture ended (UTC) |
| `TotalMeasureCount` | `int` | Total measure operations |
| `TotalArrangeCount` | `int` | Total arrange operations |
| `MeasureCountsByType` | `Dictionary<string, int>` | Measure count per element type |
| `ArrangeCountsByType` | `Dictionary<string, int>` | Arrange count per element type |
| `MeasureInstancesByType` | `Dictionary<string, HashSet<Guid>>` | Unique instances seen per type (measure) |
| `ArrangeInstancesByType` | `Dictionary<string, HashSet<Guid>>` | Unique instances seen per type (arrange) |
| `Warnings` | `List<string>` | Types exceeding avg 3 passes per instance |

| Method | Returns | Description |
|---|---|---|
| `ToCompactString()` | `string` | One-line summary: `M:120 A:60 ⚠️2` |
| `ToDetailedString()` | `string` | Multi-line report with top types and warnings |
| `ToJson()` | `string` | Full JSON export with per-type counts, instances, and warnings |

### LayoutDiagnosticsPage (not included — build your own)

There is no built-in page. Use the API above to build a diagnostics page suited to your app. See `src/app/Components/DiagnosticsSamples/LayoutDiagnosticsPage.xaml` or `src/app/Playground/DiagnosticsSamples/LayoutDiagnosticsPage.xaml` for a reference implementation.

### Example: Subscribe to snapshots programmatically

```csharp
LayoutDiagnosticsService.SnapshotCompleted += snapshot =>
{
    if (snapshot.Warnings.Count > 0)
    {
        Debug.WriteLine(snapshot.ToDetailedString());
    }
};
```
