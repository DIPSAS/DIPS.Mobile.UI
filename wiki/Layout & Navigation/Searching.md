DIPS Mobile UI delivers a way for you to add searching to your application. This is useful when people need to search for something from a service that you want to present, or when they need to filter items in the page.

## Usage
Searching can be achieved two ways:
- Adding a `SearchBar` to your page.
- Navigating people to a `SearchPage`

### SearchBar
To add a search bar to your page, simply do the following:
```xml
<dui:SearchBar />
```

When people search, the `TextChanged` event will be raised with the search query that was entered. This is when you should do something in your page.

#### Properties
Inspect the [components properties class](https://github.com/DIPSAS/DIPS.Mobile.UI/blob/main/src/library/DIPS.Mobile.UI/Components/Searching/SearchBar.Properties.cs) to further customise and use it.

### SearchPage
To navigate people to a `SearchPage`, you will have to create your own `ContentPage` that must inherit from `SearchPage`. 

> We recommend navigating to the page as a `Modal` because this gives the best user experience for keeping the users attention to the searching and not having to worry about navigating back to their context.

```xml
<dui:SearchPage xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
                xmlns:x="http://schemas.microsoft.com/winfx/2009/
                x:Class="..."
                SearchPlaceholder="Search for something">
    <dui:SearchPage.HintView>
        <dui:Label Text="To best use my search, type this and that."
                   VerticalOptions="CenterAndExpand"
                   HorizontalOptions="CenterAndExpand" />
    </dui:SearchPage.HintView>
    <dui:SearchPage.NoResultView>
        <dui:Label Text="Too bad, no results found."
                   VerticalOptions="CenterAndExpand"
                   HorizontalOptions="CenterAndExpand" />
    </dui:SearchPage.NoResultView>
    <dui:SearchPage.ResultItemTemplate>
        <DataTemplate x:DataType="{x:Type MySearchResult}">
                <!-- Custom view for the result -->
                <dui:ListItem Title="{Binding SomeProperty}"
                              CornerRadius="0" />
        </DataTemplate>
    </dui:SearchPage.ResultItemTemplate>
</dui:SearchPage>
```

In the code behind of the page, you will have to implement the following method:
```csharp
 public override async Task<IEnumerable<object>> ProvideSearchResult(string searchQuery,
        CancellationToken searchCancellationToken)
    {
        //Call a service to retrieve results of type MySearchResult?
        var results = await viewmodel.Search(); //ViewModel is the BindingContext
        return results;
    }
````

Your responsibility is to deliver a `IEnumerable` with the type of object you want to display for people searching. When you have delivered this you will have to create your own view in the pages `ResultItemTemplate`. 

> The `ProvideSearchResult` method is a `Task`, and if this takes time the `SearchPage` will display a native busy indication in the search bar of the page.

#### Properties
Inspect the [components properties class](https://github.com/DIPSAS/DIPS.Mobile.UI/blob/main/src/library/DIPS.Mobile.UI/Components/Searching/SearchPage.Properties.cs) to further customise and use it.
