# StateView Control

The `StateView` control is designed to dynamically load different views. A `StateViewModel` is given to consumers for configuration of the `StateView` which includes a `CurrentState` property which is an enum. The available states include:

- **Default**: The default state. **(ContentProperty)**
- **Loading**: Indicates a loading state.
- **Error**: Represents an error state.
- **Empty**: Denotes an empty state.

## Features

- **Dynamic State Handling**: The control dynamically switches between different views based on `CurrentState` property in `StateViewModel`.
  
- **StateView Customization**: Consumers have the flexibility to configure each state's view individually.

- **Default Implementation**: Users can utilize the default implementation if consumers have not overridden the state views. They are located here: [Implementations](https://github.com/DIPSAS/DIPS.Mobile.UI/tree/main/src/library/DIPS.Mobile.UI/Components/Loading/StateView)

- **StateViewModel Configuration**: The `StateViewModel` bindable property allows configuration of the default state views. This is a OneWay, which means that you must create the `StateViewModel` in your viewmodel and bind it to `StateView`

- **Animations**: The `StateView` can be configured to fade between the different states. **If** `ShouldUpdateViewWhenStateSetToSame`property is set to true, the content of StateView will be set to the same view that is already visible, this can be useful if animations are enabled, so that a "refresh effect" is apparent.

- **Dynamic RefreshView**: To use `RefreshView` together with the views, there is a property `HasRefreshView`, in `StateViewModel`'s `Error`, `Default` and `Empty` properties. By setting the property `true`, `StateView` will automatically wrap the view with a `RefreshView`. This `RefreshView` is bound to `RefreshCommand` and `IsRefreshing` properties in `StateViewModel`.

## Usage

To use the `StateView` control, bind the `StateViewModel` property to a `StateViewModel` created in your viewmodel. In `StateViewModel`'s constructor you can define its starting state. 

> If the `StateViewModel` property has not been set a `Label` will appear explaining that the `StateViewModel` has not been set.

### Example

In the example below, we have bound the `MyStateViewModel` to the `StateViewModel` property on `StateView`. Here we set the starting state state to `Loading`. Later the state will be set to either `Default` or `Error` state depending on if something went wrong. 

```csharp
public StateViewModel MyStateViewModel {get;set;} = new StateViewModel(State.Loading);

private async Task Initialize()
{
    try{
        await m_service.LoadList();
        MyStateViewModel.CurrentState = State.Default;
    }
    catch(Exception e)
    {
        MyStateViewModel.Error.Title = "Something went wrong";
        MyStateViewModel.Error.Description = e.Exception.ToString();
        MyStateViewModel.CurrentState = State.Error;
    }
} 
```

In the example below, the control will load the specified views based on the `CurrentState` property of `MyStateViewModel`. If consumers have not overridden the state views, it will use the default implementations. In this example we only override the `LoadingView`, but every view can be overriden.

```xaml
<dui:StateView StateViewModel="{Binding MyStateViewModel}">
    <!-- Users can utilize the default implementation or override state views as needed -->
    <dui:StateView.LoadingView>
        <dui:Label Text="Loading..." />
    </dui:StateView.LoadingView>

    <dui:Label Text="This is the default view!" />

</dui:StateView>
```

In the example below, we have signaled that a `RefreshView` should wrap around our default view, and when the `RefreshView` is refreshed, we should refresh the data.

XAML:
```xaml

<dui:StateView StateViewModel="{Binding MyStateViewModel}">
    <!-- Users can utilize the default implementation or override state views as needed -->

    <dui:CollectionView ItemsSource="{Binding Data}" />

</dui:StateView>

```

C#

```csharp

public LoadingPageViewModel()
{
    StateViewModel.Default.HasRefreshView = true;
    StateViewModel.RefreshCommand = new Command(() => _ = Refresh());
}

private async Task Refresh()
{
    // Get the data
    await Task.Delay(1000);

    Data = new List<Data>(response);

    StateViewModel.IsRefreshing = false;
}


public StateViewModel MyStateViewModel { get; } = new(State.Loading);

public List<Data> Data 
{    
    get => m_data
    private set => RaiseWhenSet(ref m_data, value);
}

```

## Limitations
On **Android** we have observed that wrapping the `StateView` around a `RefreshView` will not show the content:

```xaml
<dui:StateView StateViewModel="{Binding MyStateViewModel}">
    <dui:RefreshView>
        <dui:Label Text="This is my default view!">
    </dui:RefreshView>
</dui:StateView>
```

To work around this issue you can instead do as the example just above.