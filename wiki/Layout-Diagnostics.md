A runtime diagnostics tool for profiling .NET MAUI layout performance on iOS and Android. It captures per-element **measure** and **arrange** counts during page navigation and bottom sheet presentation, helping developers identify layout thrashing — views that are measured or arranged far more than necessary.

# How It Works

.NET MAUI's layout system uses a two-phase process for every frame where layout is needed ([source](https://learn.microsoft.com/en-us/dotnet/maui/user-interface/layouts/custom#layout-process)):

1. **Measure phase** — The framework calls `IView.Measure()` on each view to determine its desired size given constraints. Layouts call `Measure()` on all their children recursively, bottom-up.
2. **Arrange phase** — The framework calls `IView.Arrange()` on each view to assign its final position and bounds within the parent layout.

These phases can be triggered multiple times per frame. A parent layout may speculatively measure children, change constraints, and re-measure. Deeply nested layouts with complex constraints can cause exponential measure/arrange calls — known as **layout thrashing**.

Layout Diagnostics hooks into MAUI's built-in `System.Diagnostics.Metrics` instruments (`maui.layout.measure_count`, `maui.layout.arrange_count`) via `MeterListener` to count every measure and arrange call per element type and instance.

# Usage

## Prerequisites

Register metrics in `MauiProgram.cs` **before** `.UseMauiApp<App>()`:

```csharp
builder.Services.AddMetrics(); // Microsoft.Extensions.Diagnostics
```

## Initialize

```csharp
// Show the floating overlay (dormant — not yet recording)
LayoutDiagnosticsService.Initialize();
```

## API

| Member | Description |
|---|---|
| `Initialize()` | Shows the floating overlay pill |
| `Teardown()` | Removes the overlay and tears down the system |
| `Enable()` | Starts capturing layout metrics |
| `Disable()` | Stops capturing, returns all snapshots |
| `Toggle()` | Toggles between Enable/Disable |
| `ClearSnapshots()` | Clears all completed snapshots |
| `ExportAllToJson()` | Exports all snapshots as a JSON string |
| `IsEnabled` | Whether diagnostics are currently capturing |
| `IsInitialized` | Whether the overlay has been initialized |
| `CompletedSnapshots` | All snapshots captured during this session |
| `CurrentSnapshot` | The in-progress snapshot (null if none) |
| `SnapshotCompleted` | Event raised when a snapshot finishes |

## Automatic Snapshots

Snapshots are automatically created and ended during page and bottom sheet lifecycle:

- `ContentPage.OnAppearing()` → `BeginSnapshot("Page: {TypeName}")`
- `ContentPage.OnNavigatingFrom()` → `EndSnapshot()`
- `BottomSheet.OnAppearing()` → `BeginSnapshot("BottomSheet: {TypeName}")`
- `BottomSheet.OnDisappearing()` → `EndSnapshot()`

## Reading Snapshot Data

Each `LayoutDiagnosticsSnapshot` contains:

- **`SourceName`** — What triggered it (e.g., "Page: HomePage")
- **`TotalMeasureCount` / `TotalArrangeCount`** — Aggregate counts
- **`MeasureCountsByType` / `ArrangeCountsByType`** — Per-type breakdown (e.g., `"Microsoft.Maui.Controls.Label": 45`)
- **`MeasureInstancesByType`** — Unique instances per type, enabling per-instance averages
- **`Warnings`** — Types exceeding an average of 3 layout passes per instance

## Interpreting Results

| Metric | Healthy | Investigate |
|---|---|---|
| Avg measures per instance | 1–2× | > 3× (flagged with ⚠️) |
| Total measures for a page | < 200 | > 500 |

Common causes of high measure counts:
- Deeply nested layouts (Grid inside StackLayout inside ScrollView)
- `Auto`-sized rows/columns that cause re-measure cascades
- Changing `IsVisible` or `Text` on elements during layout

## Overlay

The diagnostics overlay is a floating pill that stays visible over all content, including modals and bottom sheets:

- **iOS**: Rendered in a separate `UIWindow` at `UIWindowLevel.Alert - 1`
- **Android**: Uses `TranslationZ` for z-ordering and re-parents to dialog `DecorView` when dialogs open

Tap the pill to expand and see live recording status, start/stop controls, and the current snapshot summary.

# Properties

Inspect the [`LayoutDiagnosticsService`](https://github.com/DIPSAS/DIPS.Mobile.UI/blob/main/src/library/DIPS.Mobile.UI/API/Diagnostics/LayoutDiagnosticsService.cs) and [`LayoutDiagnosticsSnapshot`](https://github.com/DIPSAS/DIPS.Mobile.UI/blob/main/src/library/DIPS.Mobile.UI/API/Diagnostics/LayoutDiagnosticsSnapshot.cs) classes for the full API surface.
