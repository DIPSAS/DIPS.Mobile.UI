A `TabView` is a top tab layout (or TabView at the top) is a UI pattern where tabs are placed horizontally across the top of the screen, just below the app bar or toolbar. Each tab corresponds to a different section or page, and switching tabs changes the view or content area below.

# Inspiration
[Material Design 2 - Tabs](https://m2.material.io/components/tabs/android)

# Remarks
iOS does not have a fulfilling concept of `Tabs`, their component have limitations and cannot be used multiple times in the same page, thus, this is implemented using a custom component Tab.

# Usage
In the following example, a TabView is used to display multiple tabs, each represented by a TabItem. Each TabItem has a Title and an optional Counter to display how many elements are present in the tab. The TabView also have following properties that can be set by the consumer: SelectedTextColor, DefaultTextColor, SelectedTextStyle and DefaultTextStyle. ContentViews or similar elements can be tied to specific tabs through the public property SelectedTabIndex, as shown in the example. The EventHandler SetSelectedTabIndex can also be used for this purpose. Finally, the property CanSwitchTab can be set to decide if a tabindex can be switched to.
```xml
<dui:VerticalStackLayout>
    <dui:TabView x:Name="TabViewSample">
        <dui:TabItem Title="Tab 1" Counter="3"/>
        <dui:TabItem Title="Tab 2" />
        <dui:TabItem Title="Tab 3" Counter="2"/>
        <dui:TabItem Title="Tab 4" Counter="2"/>
        <dui:TabItem Title="Tab 5"/>
        <dui:TabItem Title="Tab 6" Counter="1"/>
    </dui:TabView>
    <ContentView IsVisible="False">
    <ContentView.Triggers>
        <DataTrigger TargetType="ContentView" Binding="{Binding SelectedTabIndex, Source={x:Reference TabViewSample}}" Value="0">
            <Setter Property="IsVisible" Value="True" />
        </DataTrigger>
    </ContentView.Triggers>
    <Label Text="This is the view for the FIRST tab"></Label>
    </ContentView>
</dui:VerticalStackLayout>
```

# Properties
Inspect the [components properties class](https://github.com/DIPSAS/DIPS.Mobile.UI/blob/main/src/library/DIPS.Mobile.UI/Components/TabView/TabView.Properties.cs) to further customize and use it.