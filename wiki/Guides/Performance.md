As with any application people, performance is key. When people are using a mobile application, they normally perform tasks on the go and they depend on their application to stay connected. People might start discarding a poor performing application, especially if they depend on it to help them in situations where they don't have much time to react. DIPS creates applications for the Norwegian Health Care, where the user group is people in situations like this. We want to provide some a helping hand in focusing on performance with this wiki page. 

> All examples need needs you to set `DUI.IsDebug=true;` this should be done with by you in `MauiProgram` with an `#if DEBUG`. Please see [Getting Started](https://github.com/DIPSAS/DIPS.Mobile.UI/wiki/Getting-Started#builder) for a complete example.

# Page loading
The time it takes for your page to start reacting is essential for the performance of a mobile application. You do not want to end up with people having to wait for your pages to render while they navigate, which do feel like the application is "freezing" while navigating.

## Profiling page loading
In order for you to do profiling of your pages loading, we have added a `ShouldLogLoadingTime` property that can be used if you are using [ContentPage](https://github.com/DIPSAS/DIPS.Mobile.UI/blob/main/src/library/DIPS.Mobile.UI/Components/Pages/ContentPage.cs) . This will log the time it took from the page was constructed to the page was loaded. This will most likely highlight the needs for "lazy loading" of views or choosing the correct component for your page. We hope it will be helpful!


# Memory Leaks
Memory leaks are essential to get rid of for the application to perform at its best.  [MAUI has provided a Wiki page](https://github.com/dotnet/maui/wiki/Memory-Leaks) with information regarding memory leaks, to help you better understand memory leaks. In addition to this we have provided a set of helping methods in DIPS.Mobile.UI.

## Monitoring Garbage Collection
DIPS.Mobile.UI provides multiple ways for you to check if an object was garbage collected. An objects [finalizer](https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/finalizers) will run when an objects gets collected by the garbage collector. Garbage collections happens at different times during the application lifecycle, but you can force garbage collecting. **This is recommended to only do while debugging.**

### Finalizers logging
Both `ContentPage` and a `ViewModel` can log when its finalizer has ran. To opt-in, use the `ShouldLogWhenGarbageCollected` property.

### GarbageCollection
We have added [GarbageCollection](https://github.com/DIPSAS/DIPS.Mobile.UI/blob/main/src/library/DIPS.Mobile.UI/MemoryManagement/GarbageCollection.cs) that wraps the [GC](https://learn.microsoft.com/en-us/dotnet/api/system.gc?view=net-8.0) from .NET. Use this to force collection and wait for finalizers.

### ContentPage
`ContentPage` can monitor garbage collection when navigated to. To opt-in, set the `ShouldGarbageCollectAndLogWhenNavigatedTo` for a page of your choice. This will print the current Garbage Collection total memory. If you have set the `ShouldLogWhenGarbageCollected` for other pages that people can navigate out of, they should log. If they do not, they are zombies and live forever.

### Shell
`Shell` has the ability to garbage collect when a `Pop`, `PopToRoot`, `Remove` or `ShellItemChanged` has been initiated from people or programatically. This will monitor the page that people navigated to from the page, and try to garbage collect and monitor it. It will print if was garbage collected, or not. To opt in, use the `ShouldGarbageCollectPreviousPage`.

### GCCollectionMonitor
If you want to monitor a specific object when it should be garbage collected (like when an object in a page should have been destroyed), use the [`GCCollectionMonitor`](https://github.com/DIPSAS/DIPS.Mobile.UI/blob/main/src/library/DIPS.Mobile.UI/MemoryManagement/GCCollectionMonitor.cs) class.

Here is an example of when `NavigationPage` runs `Pop` and you want to monitor the previous page.
```csharp
private void NavPageOnPopped(object? sender, NavigationEventArgs e)
{
    m_monitor.ObserveContent(e.Page);
    m_monitor.CheckIfMonitoredObjectsAreStillAlive();
}
```

## Automatic clean up
We have made an automatic resolving of memory leak tooling. This can be enabled in your `MauiProgram.cs`:

```csharp
.UseDIPSUI(options =>
{
    options.EnableAutomaticMemoryLeakResolving();
});
```

This is based off of AdamEssenmachers's [MemoryToolkit.Maui](https://github.com/AdamEssenmacher/MemoryToolkit.Maui), and it tries to resolve the memory leaks automatically, without you ever having to do anything! However, if this does not resolve your memory leak problems you should clean up your views manually.

There is also the possibility to append the automatic memory resolving:

```csharp
options.EnableAutomaticMemoryLeakResolving(monitorTarget =>
{
    if(monitorTarget is VirtualListView virtualListView)
    {
        virtualListView.GlobalHeader = null;
    }    
});
```

> The automatic resolving of memory leak-tooling only works if you are using `Shell`.

To clean up your views, you need to subscribe to events from the framework that will let you do that. This section describes the possibilities of using `UnLoaded` events.

## Tips and tricks
The tips and tricks described in this chapter presumes you are using [automatic memory leak clean up](https://github.com/DIPSAS/DIPS.Mobile.UI/wiki/Performance#automatic-clean-up).

> Keep in mind that the tooling will always tell you that singletons has memory leaks.  

### Page and their view model
If you need to reference your page in your view model, like using a listener pattern, make sure to remove the reference when the page disappears. An alternative approach is to use `WeakReference`.

### Events
Make sure to unsubscribe to events. Typically events you subscribe to on objects that lives longer than your page/BindingContext/object has to be unsubscribed! Find a suitable disposing method and unsubscribe from it.

### Footer,Header,EmptyView
There might be memory leaks by using `Footer`, `Header` or `EmptyView` for ie. `CollectionView`.
To prevent the leaks, use the `Template` version. The `DataTemplate` you create will inherit the BindingContext of the CollectionView, but you can override this by setting the non-template property to the desired BindingContext. [See the official MAUI wiki](https://learn.microsoft.com/en-us/dotnet/maui/user-interface/controls/collectionview/layout?view=net-maui-8.0#display-a-templated-header-and-footer).

