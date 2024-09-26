using Microsoft.Maui.Controls.Shapes;

namespace DIPS.Mobile.UI.API.Camera.ImageCapturing.Views.StreamingBottomToolbar;

internal partial class ShutterButton : Grid, IDisposable
{
    private readonly Action m_onTappedShutterButton;
    private readonly Border m_shutterContentWhiteOverlay;
    
    public ShutterButton(Action onTappedShutterButton)
    {
        m_onTappedShutterButton = onTappedShutterButton;
        
        UI.Effects.Layout.Layout.SetCornerRadius(this, 35);
        BackgroundColor = Microsoft.Maui.Graphics.Colors.White;
        VerticalOptions = LayoutOptions.Center;
        HorizontalOptions = LayoutOptions.Center;
        WidthRequest = 70;
        HeightRequest = 70;
        Padding = 3;
        
        var shutterContentBlackOverlay = new Border
        {
            BackgroundColor = Microsoft.Maui.Graphics.Colors.Black, 
            StrokeShape = new Ellipse(),
        };
        
        m_shutterContentWhiteOverlay = new Border
        {
            BackgroundColor = Microsoft.Maui.Graphics.Colors.White,
            StrokeShape = new Ellipse(),
            Margin = 2
        };
        
        Children.Add(shutterContentBlackOverlay);
        Children.Add(m_shutterContentWhiteOverlay);
    }

    private partial void AddPlatformGestureRecognizer();
    
    public void Enable()
    {
        AddPlatformGestureRecognizer();
    }

    public void Disable()
    {
        Dispose();
    }

    protected override void OnHandlerChanging(HandlerChangingEventArgs args)
    {
        base.OnHandlerChanging(args);
        
        if(args.NewHandler is null)
            Dispose();
    }

    public partial void Dispose();
}