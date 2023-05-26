namespace DIPS.Mobile.UI.Components.Loading.Skeleton;

/// <summary>
/// Skeleton view to show skeleton structure when data is loading.
/// </summary>
[ContentProperty(nameof(MainContent))]
public partial class SkeletonView : ContentView
{
    private const string AnimationName = "SkeletonBounce";
    private Grid m_skeletongrid;
    private Grid? m_skeletonLayout;
    private List<BoxView> m_skeletons = new List<BoxView>();
    private View? m_mainContent;

    /// <summary>
    /// Creates a new instance of skeleton view
    /// </summary>
    public SkeletonView()
    {
        HorizontalOptions = LayoutOptions.Fill;
        VerticalOptions = LayoutOptions.Fill;
        Content = m_skeletongrid = new Grid();
    }

    private void OnChanged()
    {
        if (MainContent == null)
            return;
        MainContent.BindingContext = this.BindingContext;
        OnLoadingChanged();
    }

    protected override void OnBindingContextChanged()
    {
        base.OnBindingContextChanged();
        OnChanged();
    }

    private async void OnLoadingChanged()
    {
        if (MainContent == null)
            return;

        if (!m_skeletongrid.Children.Contains(MainContent))
        {
            m_skeletongrid.Children.Add(MainContent);
            MainContent.Opacity = 0;
        }

        if (!IsLoading)
        {
            if (m_skeletonLayout != null)
            {
                _ = m_skeletonLayout.FadeTo(0, FadeTime);
                StopAnimation();
            }

            await MainContent.FadeTo(1.0, FadeTime * 2);
        }
        else
        {
            if (m_skeletonLayout == null)
            {
                m_skeletonLayout = CreateSkeleton();
                m_skeletonLayout.Opacity = 0.0;
                m_skeletongrid.Children.Add(m_skeletonLayout);
            }

            _ = MainContent.FadeTo(0.0, FadeTime);
            _ = m_skeletonLayout.FadeTo(1.0, FadeTime);
            StartAnimation();
        }
    }

    private Grid CreateSkeleton()
    {
        if (m_skeletonLayout != null)
            return m_skeletonLayout;
        if (Shapes.Count == 0)
        {
            Shapes = new List<SkeletonShape> {new SkeletonShape()};
        }

        var grid = new Grid();
        foreach (var shape in Shapes)
        {
            var box = CreateBox(shape);
            grid.Children.Add(box);
            m_skeletons.Add(box);
        }

        var maxRow = Shapes.Max(s => s.Row + s.RowSpan);
        var maxCol = Shapes.Max(s => s.Column + s.ColumnSpan);
        for (var i = 0; i < maxRow; i++)
        {
            var shape = Shapes.FirstOrDefault(s => s.Row == i && s.Height > -1);
            if (shape != null)
                grid.RowDefinitions.Add(new RowDefinition {Height = shape.Height});
            else
                grid.RowDefinitions.Add(new RowDefinition());
        }

        for (var i = 0; i < maxCol; i++)
        {
            var shape = Shapes.FirstOrDefault(s => s.Column == i && s.Width > -1);
            if (shape != null)
                grid.ColumnDefinitions.Add(new ColumnDefinition {Width = shape.Width});
            else
                grid.ColumnDefinitions.Add(new ColumnDefinition());
        }

        return grid;
    }

    private BoxView CreateBox(SkeletonShape shape)
    {
        var box = new BoxView()
        {
            HorizontalOptions = shape.HorizontalAlignment,
            VerticalOptions = shape.VerticalAlignment,
            Margin = new Thickness(shape.Margin),
        };
        
        box.SetBinding(BoxView.CornerRadiusProperty, new Binding(nameof(SkeletonShape.CornerRadius), source:shape));
        box.SetBinding(BoxView.HeightRequestProperty, new Binding(nameof(SkeletonShape.Height), source:shape));
        box.SetBinding(BoxView.WidthRequestProperty, new Binding(nameof(SkeletonShape.Width), source:shape));
        box.SetBinding(BoxView.ColorProperty, new Binding(nameof(SkeletonColor), source:this));

        Grid.SetRow(box, shape.Row);
        Grid.SetColumn(box, shape.Column);
        Grid.SetRowSpan(box, shape.RowSpan);
        Grid.SetColumnSpan(box, shape.ColumnSpan);

        return box;
    }

    private void StartAnimation()
    {
        StopAnimation();
        var animation = new Animation
        {
            {
                0.0, 0.5, new Animation(a =>
                {
                    foreach (var box in m_skeletons) box.Scale = a;
                }, 0.99, 1.01, Easing.BounceOut)
            },
            {
                0.5, 1.0, new Animation(a =>
                {
                    foreach (var box in m_skeletons) box.Scale = a;
                }, 1.01, 0.99, Easing.BounceOut)
            },
        };
        animation.Commit(this, AnimationName, 16, 1000, Easing.BounceOut, (_, _) => { }, () => IsLoading);
    }

    private void StopAnimation() => this.AbortAnimation(AnimationName);
}