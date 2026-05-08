# StepFlow

`StepFlow` is an animated, accordion-style multi-step flow component for guiding users through sequential actions — wizards, checklists, multi-step forms. Only one step is expanded at a time; completing a step auto-collapses it and advances to the next.

> The component is named **`StepFlow`** (not `Stepper`, which is already a built-in MAUI control).

## When to use

- Sequential, gated flows where step _N+1_ only makes sense after step _N_ is finished (sampling, ID-checks, signing).
- Linear data-entry where you want to keep the user focused on one section at a time.

## When **not** to use

- Pages where the user can fill in fields in any order — use a regular form.
- Settings screens or open-ended lists — use [ListItem](ListItem).
- Tabbed parallel content — use [TabView](TabView).

## Anatomy

A `StepFlow` hosts a sequence of `StepFlowItem`s. Each item has four lifecycle states:

| State | Visual | Interactive |
| --- | --- | --- |
| `Disabled` | Dimmed (opacity 0.45) | No |
| `Active` | Expanded, full opacity | Yes |
| `Completed` | Collapsed, check icon, dimmed (opacity 0.78) | No (by default) |
| `Error` | Red ring around the indicator | Yes |

The recommended usage is **controller-driven**: a single `StepFlowController` lives on the consumer's view model and the flow is driven imperatively via `CompleteCurrent`, `GoTo`, and `Reset`. The controller is a plain CLR object with no MAUI dependencies, which makes it trivially unit-testable.

## Quick start

```csharp
public class SamplingViewModel : ViewModel
{
    public StepFlowController Flow { get; } = new();

    public AsyncCommand ConfirmPatientCommand { get; }
    public AsyncCommand FinishScanningCommand { get; }
    public AsyncCommand SubmitSamplingCommand { get; }

    public SamplingViewModel()
    {
        ConfirmPatientCommand = new AsyncCommand(() => { Flow.CompleteCurrent(); return Task.CompletedTask; });
        FinishScanningCommand = new AsyncCommand(() => { Flow.CompleteCurrent(); return Task.CompletedTask; });
        SubmitSamplingCommand = new AsyncCommand(() => { Flow.CompleteCurrent(); return Task.CompletedTask; });

        Flow.FlowCompleted += (_, _) => { /* show snackbar etc. */ };
    }
}
```

```xml
<dui:StepFlow Controller="{Binding Flow}">
    <dui:StepFlowItem Title="Confirm patient">
        <dui:Button Text="Confirm" Command="{Binding ConfirmPatientCommand}" />
    </dui:StepFlowItem>
    <dui:StepFlowItem Title="Scan sample labels">
        <!-- content -->
    </dui:StepFlowItem>
    <dui:StepFlowItem Title="Confirm sampling">
        <dui:Button Text="Submit" Command="{Binding SubmitSamplingCommand}" />
    </dui:StepFlowItem>
</dui:StepFlow>
```

## API

### `StepFlowController`

| Member | Description |
| --- | --- |
| `StepCount` | Number of steps (set by the container when items are attached). |
| `CurrentIndex` | Index of the currently `Active` step, or `-1`. |
| `States` | Read-only snapshot of every step's `StepFlowItemState`. |
| `IsCompleted` | `true` when every step is `Completed`. |
| `AutoAdvance` | When `true` (default), completing a step activates the next non-completed step. |
| `AutoAdvanceDelay` | Delay before auto-advance. Defaults to 800 ms. |
| `CompleteCurrent()` | Marks `CurrentIndex` `Completed` and (optionally) auto-advances. |
| `Complete(int)` | Marks the step at the given index `Completed`. |
| `GoTo(int)` | Activates the step at the given index. No-op if disabled or completed. |
| `Reset()` | Step 0 → `Active`, all others → `Disabled`. |
| `SetState(int, state)` | Explicitly set a step's state. Use sparingly. |
| `StepCompleted` | Raised when a step transitions to `Completed`. |
| `StepActivated` | Raised when a step transitions to `Active`. |
| `FlowCompleted` | Raised when the last step is completed. |

### `StepFlow` (container)

| Property | Description |
| --- | --- |
| `Items` | The steps, in order. XAML default `ContentProperty`. |
| `Controller` | The `StepFlowController` driving the flow. The container creates one automatically if you omit it. |
| `AllowDirectStepActivation` | When `true`, tapping a `Disabled` step's header activates it. Defaults to `false`. |
| `AutoScrollIntoView` | When `true` (default), the closest ancestor `ScrollView` is scrolled so the newly active step is pinned to the top. See [Auto-scroll](#auto-scroll). |
| `FlowCompleted` | Mirrors `Controller.FlowCompleted`. |

### `StepFlowItem`

| Property | Description |
| --- | --- |
| `Title` | Header text. |
| `Subtitle` | Optional smaller line below the title. |
| `Content` | The body shown when the step is `Active`. XAML default content. |
| `IndicatorTemplate` | Optional template for the leading indicator. Defaults to a numbered/check circle. |
| `LockWhenCompleted` | When `true` (default) tapping a completed step does nothing. |
| `State` | Current `StepFlowItemState`. Driven by the container — read-only for advanced scenarios. |

### `StepFlowItemState`

`Disabled`, `Active`, `Completed`, `Error`.

## Animations

The component is choreographed for a premium feel — every timing and easing is intentional:

- **Expand**: animated `HeightRequest` with a tiny damped-sine "settle" near the end (`easeOutQuint + sin·exp damping`), paired with a 15 % delayed opacity fade-in and a `-12 → 0` Y-translation. 420 ms.
- **Collapse**: `easeInCubic` (`t³`) over 320 ms.
- **Stagger**: when one panel collapses and another expands, the expand waits ≈ 110 ms ("breath") before starting.
- **Completion climax**: the check icon does a wind-up (scale `0 → 1.25`, rotation `-18° → +8°` over 220 ms) then settles to `1.0, 0°` via a critically-damped sine spring. A success-colored ring scales `0.6 → 2.4` while opacity fades `0.7 → 0` over 600 ms — the iconic "tap success" pulse.
- **Press feedback**: 1.0 → 0.97 → 1.0 with a spring on the release.
- **Lift**: when an item goes `Disabled → Active`, scale springs `0.97 → 1.0` while opacity ramps to `1.0` (380 ms).
- **Completed dimming**: opacity fades to 0.78 over 500 ms.

All animations use a unique token per item instance, so multiple `StepFlow`s on the same page do not interfere.

## Auto-scroll

When the `StepFlow` lives inside a `ScrollView`, it automatically scrolls the newly active step to the top of the scroller as the flow advances. There is no need to subscribe to `Controller.StepActivated` or call `ScrollToAsync` from the hosting page — the component walks up the visual tree to find the closest ancestor `ScrollView` and uses `ScrollToPosition.Start`, so the freshly expanded body is fully visible rather than half-cropped at the bottom of the viewport.

The scroll is timed to start once the expand animation has finished, so the target rect reflects the body's measured height rather than the still-animating zero-height rect.

Set `AutoScrollIntoView="False"` on the `StepFlow` to opt out — for example when you want to handle scrolling yourself, or when the flow is not hosted inside a scroller.

```xml
<dui:ScrollView>
    <dui:StepFlow Controller="{Binding Flow}" AutoScrollIntoView="True">
        <!-- steps -->
    </dui:StepFlow>
</dui:ScrollView>
```

If no ancestor `ScrollView` is found, `AutoScrollIntoView` is a silent no-op. Non-MAUI scrollers (`CollectionView` etc.) are not supported.

## Escape hatch (binding-only)

If you don't want a controller you can omit the `Controller` property and bind `State` two-way on each item. The container still enforces single-active and animates correctly, but you become responsible for orchestration. **Not recommended.**

## Sample

See the **Step Flow** entry in the Components sample app for a working three-step sampling flow that mirrors the Arena Mobile pattern: confirm patient → scan labels → confirm sampling, with `Reset` to demonstrate the imperative reset path.
