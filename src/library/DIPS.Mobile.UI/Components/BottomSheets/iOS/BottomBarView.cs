using DIPS.Mobile.UI.API.Library;
using Microsoft.Maui.Platform;
using UIKit;
using Colors = Microsoft.Maui.Graphics.Colors;
using ContentView = Microsoft.Maui.Controls.ContentView;

namespace DIPS.Mobile.UI.Components.BottomSheets.iOS;

public class BottomBarView : ContentView
{
    private readonly UIView m_rootView;
    private readonly BottomSheet m_bottomSheet;
    
    private const int BottomBarHeight = 120;

    public BottomBarView(UIView rootView, BottomSheet bottomSheet)
    {
        m_rootView = rootView;
        m_bottomSheet = bottomSheet;

        Content = CreateBottomBar();
        
        NativeView = this.ToPlatform(DUI.GetCurrentMauiContext!);
    }
    
    public UIView NativeView { get; }

    public void SetConstraints()
    {
        NativeView.TranslatesAutoresizingMaskIntoConstraints = false;
        
        NSLayoutConstraint.ActivateConstraints([
            NativeView.LeadingAnchor.ConstraintEqualTo(m_rootView.LeadingAnchor),
            NativeView.BottomAnchor.ConstraintEqualTo(m_rootView.BottomAnchor),
            NativeView.TrailingAnchor.ConstraintEqualTo(m_rootView.TrailingAnchor),
            NativeView.HeightAnchor.ConstraintEqualTo(BottomBarHeight)
        ]);
    }

    public void Show()
    {
        m_rootView.AddSubview(NativeView);
        SetConstraints();
    }

    public void Remove()
    {
        NativeView.RemoveFromSuperview();
    }
    
    private Grid CreateBottomBar()
    {
        var grid = new Grid
        {
            ColumnSpacing = Sizes.GetSize(SizeName.size_2), 
            RowDefinitions = [new RowDefinition(GridLength.Star)],
            Padding = Sizes.GetSize(SizeName.size_3),
            Background = new LinearGradientBrush
            {
                EndPoint = new Point(0, 1),
                GradientStops =
                [
                    new GradientStop { Color = m_bottomSheet.BackgroundColor.WithAlpha(0), Offset = .0f },
                    new GradientStop { Color = m_bottomSheet.BackgroundColor, Offset = .25f }
                ]
            }
        };
       
        foreach (var button in m_bottomSheet.BottombarButtons)
        {
            grid.AddColumnDefinition(new ColumnDefinition(GridLength.Star));
            grid.Add(button, grid.ColumnDefinitions.Count - 1);
        }
        
        grid.BindingContext = m_bottomSheet.BindingContext;
        
        //add extra space to not get too close to bottom safe area
        if (UIApplication.SharedApplication.Delegate.GetWindow().SafeAreaInsets.Bottom <= 0)
            return grid;

        grid.Margin = new Thickness(0, 0, 0, Sizes.GetSize(SizeName.size_2));    
        
        return grid;
    }
}