Dividers are used in user interfaces to separate content into distinct sections. They help improve the visual organization and readability of the content. In this document, we will explore the different types of dividers and their usage in mobile UI design.

Dividers play a crucial role in UI design by enhancing the user experience. Here are some common use cases:

- Separating items in a list
- Dividing sections
- Creating visual hierarchy in a layout

# Implementation
In our library, we have created a ```<dui:Divider />``` component, and is easy to use. For example to divide elements:

```xaml
<VerticalStackLayout>

    <BoxView HeightRequest="20"
             BackgroundColor="Red"  />
    <Divider />
    <BoxView HeightRequest="20"
             BackgroundColor="Red"  />
</VerticalStackLayout>
```

# Tips and tricks

## Dividing elements in a list
In many designs, a divider is often present between elements in a list, but the last divider should not be visible. Achieving this through bindings can be cumbersome. To simplify this, we have introduced a property that handles it effortlessly.

Simply by adding `dui:Layout.AutoHideLastDivider="True"` property to a `CollectionView` or a `VerticalStackLayout` together with `BindableLayout`, will make sure the last `Divider` is made invisible:

```xaml
<dui:CollectionView ItemsSource="{Binding Tests}"
                    dui:Layout.AutoHideLastDivider="True">

    <dui:CollectionView.ItemTemplate>
        <DataTemplate>
            <BoxView HeightRequest="20" BackgroundColor="Red" />       
            <Divider /> 
        </DataTemplate>
    </dui:CollectionView.ItemTemplate>

</dui:CollectionView>
```

> **NOTE:** `dui:CollectionView` must be used

> Only vertical `CollectionView` is supported

> If you are using a `ListItem` in the `ItemTemplate`, set `HasBottomDivider` to true

or 

```xaml
<VerticalStackLayout BindableLayout.ItemsSource="{Binding Tests}"
                    dui:Layout.AutoHideLastDivider="True">

    <BindableLayout.ItemTemplate>
        <DataTemplate>
            <BoxView HeightRequest="20" BackgroundColor="Red" />       
            <Divider /> 
        </DataTemplate>
    </BindableLayout.ItemTemplate>

</VerticalStackLayout>
```

