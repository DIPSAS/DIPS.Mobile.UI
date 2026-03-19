# Corner Radius

## Introduction
Corner radius refers to the rounded edges of UI elements such as buttons, cards, and containers in an application. Setting a corner radius can significantly impact the visual appeal and user experience of an app.

## Why Set Corner Radius

1. **Aesthetics**: Rounded corners can make UI elements look more modern and visually appealing. They help in creating a smooth and polished look.

2. **User Experience**: Rounded corners can make interactive elements feel more approachable and touch-friendly, enhancing the overall user experience.

3. **Consistency**: Using a consistent corner radius across different UI elements helps in maintaining a uniform design language throughout the app.

4. **Focus**: Rounded corners can help in drawing attention to specific elements, making it easier for users to focus on important actions or information.

5. **Brand Identity**: Customizing the corner radius can help in aligning the app's design with the brand's identity, making the app more recognizable and unique.

## Implementation


### Layout Effect
We have made setting the corner radius of a `Visual Element` a lot easier. Instead of wrapping your element with a `Border` simply use our `Layout` effect:

```xml
<BoxView HeightRequest="100"
         WidthRequest="100"
         BackgroundColor="Red"
         dui:Layout.CornerRadius="10, 10, 10, 10" />
```

You can also use a different property, which will auto set the corner radius:

```xml
<BoxView HeightRequest="100"
         WidthRequest="100"
         BackgroundColor="Red"
         dui:Layout.AutoCornerRadius="True" />
```

#### CollectionView
The property AutoCornerRadius is default set to true in our CollectionViews, which means that the first and last element will receive corner radius.

To turn this behavior off, set the property to false:
```xml
<dui:CollectionView HeightRequest="100"
                    WidthRequest="100"
                    BackgroundColor="Red"
                    dui:LayoutAutoCornerRadius="False" />
```


