As with any application people, performance is key. When people are using a mobile application, they normally perform tasks on the go and they depend on their application to stay connected. People might start discarding a poor performing application, especially if they depend on it to help them in situations where they don't have much time to react. DIPS creates applications for the Norwegian Health Care, where the user group is people in situations like this. We want to provide some a helping hand in focusing on performance with this wiki page. 

> All examples need needs you to set `DUI.IsDebug=true;` this should be done with by you in `MauiProgram` with an `#if DEBUG`. Please see [Getting Started](https://github.com/DIPSAS/DIPS.Mobile.UI/wiki/Getting-Started#builder) for a complete example.

# Page loading
The time it takes for your page to start reacting is essential for the performance of a mobile application. You do not want to end up with people having to wait for your pages to render while they navigate, which do feel like the application is "freezing" while navigating.

## Profiling page loading
In order for you to do profiling of your pages loading, we have added a `ShouldLogLoadingTime` property that can be used if you are using [ContentPage](https://github.com/DIPSAS/DIPS.Mobile.UI/blob/main/src/library/DIPS.Mobile.UI/Components/Pages/ContentPage.cs) . This will log the time it took from the page was constructed to the page was loaded. This will most likely highlight the needs for "lazy loading" of views or choosing the correct component for your page. We hope it will be helpful!


# Memory Leaks
Memory leaks are essential to get rid of for the application to perform at its best. We have a dedicated wiki page covering detection tooling, known MAUI framework workarounds, best practices, and common leak patterns:

**[Memory Leaks](https://github.com/DIPSAS/DIPS.Mobile.UI/wiki/Memory-Leaks)**

