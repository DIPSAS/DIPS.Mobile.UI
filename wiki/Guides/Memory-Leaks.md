Memory leaks cause mobile applications to consume more and more memory over time until the operating system terminates them. In healthcare apps, this can disrupt critical workflows. DIPS.Mobile.UI provides built-in tooling to detect and diagnose memory leaks, and includes workarounds for known .NET MAUI framework bugs.

> All debugging features described on this page require `DUI.IsDebug = true`. This should be set in `MauiProgram.cs` with an `#if DEBUG`. See [Getting Started](https://github.com/DIPSAS/DIPS.Mobile.UI/wiki/Getting-Started#builder) for a complete example.

## Table of Contents

- [Understanding Memory Leaks in MAUI](#understanding-memory-leaks-in-maui)
- [Detecting Memory Leaks](#detecting-memory-leaks)
  - [GCCollectionMonitor](#gccollectionmonitor)
  - [Shell Navigation Monitoring](#shell-navigation-monitoring)
  - [What Shell Does Not Monitor](#what-shell-does-not-monitor)
- [Known MAUI Framework Bugs & DUI Workarounds](#known-maui-framework-bugs--dui-workarounds)
  - [Shell Item Change Doesn't Disconnect Child Handlers](#shell-item-change-doesnt-disconnect-child-handlers)
  - [StackNavigationManager Leaks on Android](#stacknavigationmanager-leaks-on-android)
  - [ToolbarItems Hold Page References](#toolbaritems-hold-page-references)
  - [Modal Pages Don't Disconnect Pushed Pages](#modal-pages-dont-disconnect-pushed-pages)
- [Best Practices](#best-practices)
  - [Event Subscriptions](#event-subscriptions)
  - [Handler Lifecycle Cleanup](#handler-lifecycle-cleanup)
  - [ViewModel Disposal](#viewmodel-disposal)
  - [Commands](#commands)
  - [CollectionView](#collectionview)
  - [iOS-Specific](#ios-specific)
- [Common Leak Patterns with Examples](#common-leak-patterns-with-examples)
  - [Pattern 1: Subscribing to Singleton Events](#pattern-1-subscribing-to-singleton-events)
  - [Pattern 2: Relying on OnDisappearing for Cleanup](#pattern-2-relying-on-ondisappearing-for-cleanup)
  - [Pattern 3: Observer Pattern Circular References](#pattern-3-observer-pattern-circular-references)
  - [Pattern 4: Command Closures Capturing this](#pattern-4-command-closures-capturing-this)
  - [Pattern 5: EmptyView False Positives](#pattern-5-emptyview-false-positives)
- [Further Reading](#further-reading)

---

# Understanding Memory Leaks in MAUI

A memory leak occurs when objects that should be garbage collected are kept alive by strong references from long-lived objects. In .NET MAUI, the most common causes are:

1. **C# event subscriptions** — subscribing to an event on a long-lived object (singleton service, `Application.Resources`, `Shell.Current`) creates a strong reference from that object to the subscriber through the event delegate's `Target` property.
2. **Delegate/Func/Action properties** — same mechanism as events. Any delegate holds a `Target` reference to its owning instance.
3. **Native handler retention** — platform handlers (iOS `UIView` subclasses, Android `View` subclasses) hold strong references back to managed MAUI elements. If handlers are not disconnected, the entire visual tree stays alive.
4. **iOS circular references** — C# objects subclassing `NSObject` live in both a garbage-collected world and a reference-counted world. A parent→child→parent cycle via events or properties prevents collection on Apple platforms specifically.

> **Important:** The .NET garbage collector *can* collect circular references between managed objects — but only when there is no external root (like a native handler or a singleton) holding onto any part of the cycle.

For a deeper explanation, see the [official MAUI Memory Leaks wiki](https://github.com/dotnet/maui/wiki/Memory-Leaks).

---

# Detecting Memory Leaks

## GCCollectionMonitor

[`GCCollectionMonitor`](https://github.com/DIPSAS/DIPS.Mobile.UI/blob/main/src/library/DIPS.Mobile.UI/MemoryManagement/GCCollectionMonitor.cs) is a singleton that detects memory leaks by taking a snapshot of a visual tree, forcing garbage collection, and checking which objects survived. Any object that survives is a "zombie" — something is holding a strong reference to it.

When monitoring is triggered for an object, the monitor:

1. Creates a `CollectionContentTarget` — a snapshot that recursively walks the visual tree via `IVisualTreeElement.GetVisualChildren()` and captures a `WeakReference` to every child element, their handlers, effects, and binding contexts
2. Forces **10 GC cycles** with 200ms delays between each (giving finalizers time to run)
3. Checks which `WeakReference` targets are still alive
4. Reports surviving objects as zombies with their type name and `AutomationId`

```
GarbageCollection: Checking if MyPage has memory leaks...
GarbageCollection: Checking 9 GC collections.
GarbageCollection: ---- Visual children zombies of MyPage: ----
GarbageCollection: - 🧟 Microsoft.Maui.Controls.Label is a zombie!
GarbageCollection: - 🧟 TouchPlatformEffect (Attached to: MyButton) is a zombie!
GarbageCollection: ---- Binding Context zombies of MyPage: ----
GarbageCollection: - 🧟 MyPageViewModel is a zombie!
GarbageCollection: ❌ There is memory leaks after checking MyPage.
```

> **Note:** Objects with a `BindingContext` of type `string` are skipped in zombie detection. .NET interns strings as static GC roots, so a `WeakReference` to a string always survives — this is a false positive, not a real leak.

> **Singletons:** The monitoring tooling will always report singletons as memory leaks. This is expected — singletons are never garbage collected because the DI container holds them for the lifetime of the application.

## Shell Navigation Monitoring

DUI's `Shell` uses `GCCollectionMonitor` in its `Navigated` event to automatically monitor pages when they leave the screen. Enable it in your Shell subclass:

```csharp
// In your App.xaml.cs or Shell subclass
ShouldGarbageCollectPreviousPage = true;
```

When enabled, the Shell hooks into the `Navigated` event and reacts based on the navigation source:

| Navigation event | What happens |
|---|---|
| **Pop / PopToRoot / Remove** | Shell creates a `CollectionContentTarget` for each popped page and calls `CheckIfObjectIsAliveAndTryResolveLeaks()` |
| **ShellItemChanged** (e.g. login→logout) | Shell waits 5 seconds for animations, disconnects child handlers on all tab pages ([MAUI#34898 workaround](#shell-item-change-doesnt-disconnect-child-handlers)), then monitors each page |
| **Modal dismissed** | Shell detects the modal was removed, clears ToolbarItems, disconnects handlers for every page in the modal's navigation stack, and monitors each one |
| **BottomSheet closed** | The platform teardown code (`BottomSheetFragment.OnDestroy` on Android, `BottomSheetViewController.Dispose` on iOS) monitors the bottom sheet and disconnects its handlers |

If `EnableAutomaticMemoryLeakResolving()` is also enabled, the monitor will attempt to auto-resolve leaks by recursively clearing Effects, BindingContext, Parent, Resources, ItemsSource, and disconnecting handlers on every zombie.

For most apps, this is all you need. The Application Output window (iOS) or Logcat filtered by "dotnet" (Android) will show zombie reports for every navigation.

## What Shell Does Not Monitor

Shell monitors pages in the navigation stack and modal stack. But it can only monitor objects that are **in the visual tree at the time of navigation**. If you have objects that live **outside** the visual tree — or are removed from it during the page's lifetime — you must monitor them yourself.

The key method is `CheckIfObjectIsAliveAndTryResolveLeaks()`. It takes a `CollectionContentTarget` snapshot and handles both zombie detection (in debug) and auto-resolution (if enabled):

```csharp
_ = GCCollectionMonitor.Instance.CheckIfObjectIsAliveAndTryResolveLeaks(
    myObject.ToCollectionContentTarget());
```

The `ToCollectionContentTarget()` extension method (from `GarbageCollection.cs`) creates the snapshot. Call it **before** disconnecting handlers — the snapshot needs to walk the live visual tree while it still exists.

**Example: how BottomSheet does it**

A `BottomSheet` is not part of any page's visual tree — it's presented as a platform dialog. The Shell has no way to know it exists. So the platform teardown code monitors it explicitly:

```csharp
// Android — BottomSheetFragment.cs
public override void OnDestroy()
{
    base.OnDestroy();
    
    m_bottomSheet.SendClose();
    BottomSheetService.RemoveFromStack(m_bottomSheet);
    m_bottomSheet.DisconnectHandlers();
    
    // Snapshot the visual tree and monitor for leaks
    _ = GCCollectionMonitor.Instance.CheckIfObjectIsAliveAndTryResolveLeaks(
        m_bottomSheet.ToCollectionContentTarget());
    
    // ... cleanup event subscriptions ...
}
```

```csharp
// iOS — BottomSheetViewController.cs
protected override void Dispose(bool disposing)
{
    base.Dispose(disposing);
    
    m_navigationBarHelper?.Dispose();
    BottomSheet.SendClose();
    BottomSheetService.RemoveFromStack(BottomSheet);
    
    // Snapshot BEFORE disconnecting handlers
    _ = GCCollectionMonitor.Instance.CheckIfObjectIsAliveAndTryResolveLeaks(
        BottomSheet.ToCollectionContentTarget());
    
    BottomSheet.DisconnectHandlers();
    m_container.DisconnectHandlers();
    m_bottomBar?.DisconnectHandlers();
}
```

The pattern is the same in both cases:
1. Perform any service/stack cleanup
2. Create the `CollectionContentTarget` snapshot (while the visual tree is still intact)
3. Call `CheckIfObjectIsAliveAndTryResolveLeaks()`
4. Disconnect handlers

If you build custom overlays, popups, or any other element that lives outside the page's visual tree, follow this same pattern in your teardown code.

### ObserveContent for ad-hoc monitoring

For quick investigation during development, you can use `ObserveContent` to register any object for monitoring, then trigger a check later:

```csharp
// Register an object — captures its visual tree as a snapshot
GCCollectionMonitor.Instance.ObserveContent(myView);

// Later, check all registered objects at once
await GCCollectionMonitor.Instance.CheckIfMonitoredObjectsAreStillAlive();
```

This is useful when you want to monitor something without tying it to a specific lifecycle event. All observed objects are checked together, and the log output includes total memory before/after with the difference.

### Elements removed from the visual tree in code-behind

When you remove a view from the visual tree in code-behind (e.g. swapping toolbar states, removing an overlay), that view is no longer there when the page is eventually popped. The automatic monitoring won't see it.

You must manually disconnect handlers on removed views. A good example is `CameraPreview`, which swaps toolbar views during state transitions:

```csharp
// CameraPreview.cs — removing a toolbar view from the grid
public void RemoveTopToolbarView(View? toolbarItems)
{
    if (toolbarItems is null) 
        return;
    
    m_topToolbarContainer.Remove(toolbarItems);
    toolbarItems.DisconnectHandlers(); // Must disconnect manually!
}
```

Another example is `ImageCaptureBottomToolbarView`, which replaces its entire content when switching between streaming, confirm, and edit states:

```csharp
// ImageCaptureBottomToolbarView.cs — disconnect old children before adding new ones
private void DisconnectHandlers(Action beforeResolve)
{
    var childrenThatWillBeRemoved = Children.ToList();
    Clear();
    
    beforeResolve.Invoke(); // Add the new state view
    
    foreach (var view in childrenThatWillBeRemoved)
    {
        view.DisconnectHandlers(); // Old state views won't be in the tree at pop time
    }
}
```

**Rule of thumb:** If you call `Remove()`, `Clear()`, or replace content in code-behind at any point during the page's lifetime, call `DisconnectHandlers()` on the removed views immediately. They will never be cleaned up automatically.

---

# Known MAUI Framework Bugs & DUI Workarounds

DUI's custom `Shell` class includes workarounds for several .NET MAUI framework bugs that cause memory leaks. These workarounds are applied automatically when you use DUI's `Shell`.

## Shell Item Change Doesn't Disconnect Child Handlers

**MAUI Issue:** [dotnet/maui#34898](https://github.com/dotnet/maui/issues/34898)

**Problem:** When Shell items change (e.g. swapping the root `TabBar` during login→logout), MAUI fails to properly disconnect handlers:
- **iOS**: Disconnects the page's handler, but NOT its child components (Labels, Grids, Effects, etc.)
- **Android**: Doesn't disconnect anything — neither the page nor its children

Native handler references on child views root the entire managed visual tree, preventing GC from collecting any of it.

> **Why push/pop doesn't have this problem:** Normal `Shell.GoToAsync` push/pop navigation properly disconnects handlers on the page and all its children. The Shell item change code path is different and misses this step.

**DUI Workaround:** DUI's Shell tracks all content pages across all tabs. When a `ShellItemChanged` event fires:
1. Waits 5 seconds for animations to complete
2. On Android: calls `contentPage.DisconnectHandlers()` (page + all children)
3. On iOS + Android: calls `contentPage.Content.DisconnectHandlers()` (children only — iOS already handles the page itself)

This is where most "invisible" leaks come from in tab-based apps. DUI handles it automatically.

## StackNavigationManager Leaks on Android

**MAUI Issue:** [dotnet/maui#34456](https://github.com/dotnet/maui/issues/34456)

**Problem:** On Android, `StackNavigationManager.Disconnect()` fails to clear three private fields: `_currentPage`, `_fragmentContainerView`, and `_fragmentManager`. These hold strong references that keep the navigation page and its content alive.

**DUI Workaround:** Before disconnecting a modal `NavigationPage`, DUI captures a reference to the `StackNavigationManager`. After disconnect, it uses reflection to null out the leaked fields. This is safe even if MAUI fixes the bug in the future — it simply nulls already-null fields.

## ToolbarItems Hold Page References

**MAUI Issue:** [dotnet/maui#34892](https://github.com/dotnet/maui/issues/34892)

**Problem:** Native toolbar infrastructure holds strong references back to the page through `ToolbarItems`. When a modal page is dismissed, these references prevent the page from being collected.

**DUI Workaround:** When a modal is dismissed, DUI clears `page.ToolbarItems` on all pages in the modal stack before they are expected to be collected.

## Modal Pages Don't Disconnect Pushed Pages

**Problem:** When a modal `NavigationPage` is popped, MAUI only disconnects handlers for the page currently visible at the top of the stack. Any pages that were pushed earlier are not cleaned up.

**DUI Workaround:** DUI subscribes to `NavigationPage.Pushed` to track all pages in the modal's navigation stack. When the modal is popped, it disconnects handlers for the entire stack — not just the visible page.

Enable it in `MauiProgram.cs`:

```csharp
.UseDIPSUI(options =>
{
    options.EnableAutomaticModalHandlerDisconnection();
})
```

---

# Best Practices

## Event Subscriptions

The number one cause of memory leaks: subscribing to events on objects that outlive you, and forgetting to unsubscribe.

**Rule:** Every `+=` must have a matching `-=` in the appropriate cleanup method.

```csharp
// ❌ Leaks — singleton holds strong ref to this ViewModel forever
public class MyViewModel : ViewModel, IDisposable
{
    public MyViewModel(IEventBus eventBus)
    {
        eventBus.SomethingHappened += OnSomethingHappened;
    }

    private void OnSomethingHappened(object? sender, EventArgs e) { /* ... */ }
}

// ✅ Fixed — unsubscribe in Dispose
public class MyViewModel : ViewModel, IDisposable
{
    private readonly IEventBus m_eventBus;

    public MyViewModel(IEventBus eventBus)
    {
        m_eventBus = eventBus;
        m_eventBus.SomethingHappened += OnSomethingHappened;
    }

    public void Dispose()
    {
        m_eventBus.SomethingHappened -= OnSomethingHappened;
    }

    private void OnSomethingHappened(object? sender, EventArgs e) { /* ... */ }
}
```

> **Gotcha:** Double-check that the handler method name in `-=` matches the one in `+=`. A real-world bug was found where `InterestsRemoved -= OnInterestsUpdated` was used instead of the correct `InterestsRemoved -= OnInterestsRemoved`. Copy-paste errors in event unsubscription are silent — no exception, the handler just stays attached forever.

## Handler Lifecycle Cleanup

**Rule:** Never rely solely on `OnDisappearing` for cleanup. Always clean up in `OnHandlerChanging` (or `OnHandlerChanged` when `Handler is null`).

**Why?** During Shell item changes (e.g. login→logout), `OnDisappearing` may or may not fire — but handler disconnection happens regardless. If your cleanup is only in `OnDisappearing`, it will be skipped.

```csharp
// ❌ Leaks during shell item changes
protected override void OnAppearing()
{
    base.OnAppearing();
    Shell.Current.Navigating += OnNavigating;
}

protected override void OnDisappearing()
{
    base.OnDisappearing();
    Shell.Current.Navigating -= OnNavigating; // May never run!
}

// ✅ Correct — clean up when the handler is being disconnected
private IMyViewModel? m_viewModel;

protected override void OnBindingContextChanged()
{
    base.OnBindingContextChanged();

    if (BindingContext is IMyViewModel vm)
    {
        m_viewModel = vm; // Cache reference!
        m_viewModel.PropertyChanged += OnViewModelPropertyChanged;
    }
}

protected override void OnHandlerChanging(HandlerChangingEventArgs args)
{
    base.OnHandlerChanging(args);

    if (args.NewHandler is null) // Handler is being disconnected
    {
        if (m_viewModel is not null)
        {
            m_viewModel.PropertyChanged -= OnViewModelPropertyChanged;
            m_viewModel = null;
        }
    }
}
```

> **Important:** Cache the ViewModel reference in a field. The framework may clear `BindingContext` before disconnecting the handler — if your cleanup code does `((IMyViewModel)BindingContext).PropertyChanged -= handler`, it will throw a `NullReferenceException` or silently skip the unsubscription.

## ViewModel Disposal

**Rule:** A parent ViewModel that creates child ViewModels must dispose/clear them in its own `Dispose()`.

The observer pattern — where a parent passes `this` to a child as a callback — creates circular references. Even if the parent is disposed, the children still hold a strong reference back to it.

```csharp
// ❌ Children hold parent alive
public class ParentViewModel : ViewModel, IDisposable
{
    public ObservableCollection<ChildViewModel> Children { get; }

    public ParentViewModel()
    {
        Children = new ObservableCollection<ChildViewModel>(
            items.Select(i => new ChildViewModel(i, observer: this))
        );
    }

    public void Dispose() { /* Children still reference this! */ }
}

// ✅ Break circular references in Dispose
public void Dispose()
{
    foreach (var child in Children)
    {
        child.Dispose(); // Child nulls its observer reference
    }
    Children.Clear(); // Breaks CollectionView binding
}
```

## Commands

**Rule:** Create commands once in the constructor — not as expression-bodied properties.

```csharp
// ❌ Creates a NEW closure on every property access
public ICommand SaveCommand => new Command(() => Save());

// ✅ Create once
public ICommand SaveCommand { get; }

public MyViewModel()
{
    SaveCommand = new Command(() => Save());
}
```

Expression-bodied command properties create a new `Command` with a new closure (capturing `this`) on every access. XAML bindings may access the property multiple times, creating multiple command instances — each one rooting the ViewModel.

## CollectionView

**Rule:** Use `FooterTemplate`, `HeaderTemplate`, and `EmptyViewTemplate` instead of `Footer`, `Header`, and `EmptyView` when setting visual content.

```xml
<!-- ❌ May cause leaks — direct view assignment keeps references alive -->
<CollectionView Footer="{Binding}" Header="{Binding}" EmptyView="No items" />

<!-- ✅ Use templates — the DataTemplate inherits the CollectionView's BindingContext -->
<CollectionView>
    <CollectionView.EmptyViewTemplate>
        <DataTemplate>
            <Label Text="No items found" />
        </DataTemplate>
    </CollectionView.EmptyViewTemplate>
</CollectionView>
```

> **Note on `EmptyView=""`**: Setting `EmptyView` to an empty string sets the BindingContext to `string.Empty`, which is interned by the CLR and never garbage collected. This causes a false positive in zombie detection — not an actual leak, but confusing during diagnosis.

## iOS-Specific

On iOS (and Catalyst), C# objects subclassing `NSObject` live in both a garbage-collected and a reference-counted world. This creates patterns that leak exclusively on Apple platforms:

```csharp
// ❌ Leaks on iOS — circular reference via Subviews + event
class MyView : UIView
{
    public MyView()
    {
        var picker = new UIDatePicker();
        AddSubview(picker);
        picker.ValueChanged += OnValueChanged; // Target = this → cycle!
    }

    void OnValueChanged(object? sender, EventArgs e) { }
}

// ✅ Fix — use a static method (null Target)
static void OnValueChanged(object? sender, EventArgs e) { }

// ✅ Alternative — use a non-NSObject proxy
class MyView : UIView
{
    readonly Proxy m_proxy = new();

    public MyView()
    {
        var picker = new UIDatePicker();
        AddSubview(picker);
        picker.ValueChanged += m_proxy.OnValueChanged;
    }

    class Proxy
    {
        public void OnValueChanged(object? sender, EventArgs e) { }
    }
}
```

Also ensure iOS-specific event subscriptions (like `CollectionView.Scrolled`) are unsubscribed in `OnHandlerChanging`, not `OnDisappearing`:

```csharp
protected override void OnHandlerChanging(HandlerChangingEventArgs args)
{
    base.OnHandlerChanging(args);

    if (args.NewHandler is null)
    {
        MyCollectionView.Scrolled -= OnCollectionViewScrolled;
    }
}
```

---

# Common Leak Patterns with Examples

## Pattern 1: Subscribing to Singleton Events

**Severity:** Critical

A ViewModel subscribes to events on a singleton service in its constructor. The singleton holds a strong reference to the ViewModel through the event delegate. If `Dispose()` is never called (or unsubscribes the wrong handler), the ViewModel lives forever.

```csharp
// In constructor
m_globalEventBus.LastPatientsListChanged += OnPatientsChanged;

// In Dispose — BUG! Wrong handler name
m_globalEventBus.LastPatientsListChanged -= OnPatientsUpdated; // ← Should be OnPatientsChanged
```

**Fix:** Always pair `+=` with `-=` using the exact same method reference. Review unsubscription code carefully for copy-paste errors.

## Pattern 2: Relying on OnDisappearing for Cleanup

**Severity:** Critical (especially for tab pages)

Code-behind subscribes to events in `OnAppearing` or `OnBindingContextChanged`, and only unsubscribes in `OnDisappearing`. During Shell item changes (login→logout), `OnDisappearing` may or may not fire — but the handler is disconnected directly, leaving subscriptions alive.

**Real example:** A page subscribes to `Shell.Current.Navigating` in `OnAppearing`. On logout, the Shell root changes. The old `Shell` instance holds the page alive through the event subscription, even though the page is no longer visible.

**Fix:** Use `OnHandlerChanging` as described in [Handler Lifecycle Cleanup](#handler-lifecycle-cleanup).

## Pattern 3: Observer Pattern Circular References

**Severity:** High

Parent ViewModel creates child ViewModels, passing `this` as an observer/callback:

```
ParentVM ──holds──→ List<ChildVM>
     ↑                    │
     └──── observer ──────┘
```

Even if the parent's `Dispose()` runs, the children are not disposed and still hold the parent alive. The `CollectionView` binding to the list also keeps children alive.

**Fix:** The parent's `Dispose()` must:
1. Dispose each child ViewModel
2. Clear the collection (breaks `CollectionView` bindings)
3. Children should null their observer reference in their own `Dispose()`

## Pattern 4: Command Closures Capturing `this`

**Severity:** Medium

Commands defined as expression-bodied properties (`=> new Command(...)`) create new closures on every access. Each closure captures `this`. XAML bindings to these properties create and retain multiple command instances.

**Fix:** Initialize commands once in the constructor using a backing property.

## Pattern 5: EmptyView False Positives

**Severity:** Low (not an actual leak)

`EmptyView=""` sets the BindingContext to `string.Empty`, which is CLR-interned and never garbage collected. `GCCollectionMonitor` reports it as a zombie with a blank name.

**Fix:** Use `EmptyViewTemplate` with visual content, or simply ignore these reports — they are false positives.

---

# Further Reading

- [Official MAUI Memory Leaks Wiki](https://github.com/dotnet/maui/wiki/Memory-Leaks) — tooling, patterns, and techniques from the MAUI team
- [MemoryToolkit.Maui](https://github.com/AdamEssenmacher/MemoryToolkit.Maui) — open-source memory leak detection for MAUI
- [dotnet/maui#34898](https://github.com/dotnet/maui/issues/34898) — Shell item change handler disconnect issue
- [dotnet/maui#34456](https://github.com/dotnet/maui/issues/34456) — StackNavigationManager field leak on Android
- [dotnet/maui#34892](https://github.com/dotnet/maui/issues/34892) — ToolbarItems holding page references
- [MemoryLeaksOniOS](https://github.com/jonathanpeppers/MemoryLeaksOniOS) — sample repo for exploring iOS-specific circular reference leaks
