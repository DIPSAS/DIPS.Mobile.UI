using CoreGraphics;
using DIPS.Mobile.UI.Components.Buttons;
using DIPS.Mobile.UI.Resources.Colors;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;
using UIKit;
using Button = DIPS.Mobile.UI.Components.Buttons.Button;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;

// ReSharper disable once CheckNamespace
namespace DIPS.Mobile.UI.Components.FloatingActionButton.FloatingActionButtonMenu;

public partial class FloatingActionButtonMenuHandler : ViewHandler<FloatingActionButtonMenu, LayoutView>
{
    private UIButton m_mainButton = new UIButton();
    private UIStackView m_content = new UIStackView();
    
    protected override LayoutView CreatePlatformView()
    {
        var stackView = new UIStackView()
        {
            Axis = UILayoutConstraintAxis.Vertical,
            Distribution = UIStackViewDistribution.Fill,
            Alignment = UIStackViewAlignment.Trailing,
            Spacing = 16,
            ClipsToBounds = true,
            BackgroundColor = new UIColor(0, 0, 0, 0.5f)
        };

        var stackLayout = new VerticalStackLayout
        {
            Spacing = 16, BackgroundColor = Colors.GetColor(ColorName.color_system_black)
        };
        stackLayout.Add(new Button(){BackgroundColor = Colors.GetColor(ColorName.color_error_dark)});
        
        return stackLayout.ToPlatform(MauiContext!) as LayoutView;
    }

    protected override void ConnectHandler(LayoutView platformView)
    {
        base.ConnectHandler(platformView);
        
        m_mainButton.Layer.ShadowColor = Colors.GetColor(ColorName.color_system_black).ToPlatform().CGColor;
        m_mainButton.Layer.ShadowOffset = new CGSize(0, 0);
        m_mainButton.Layer.ShadowRadius = 5;
        m_mainButton.Layer.ShadowOpacity = 0.5f;
        m_mainButton.Layer.CornerRadius = 25;
        m_mainButton.BackgroundColor = UIColor.Brown;
        
       // platformView.AddArrangedSubview(m_content);
       // platformView.AddArrangedSubview(m_mainButton);

        /*NSLayoutConstraint.ActivateConstraints(new[]
            {
                m_mainButton.TrailingAnchor.ConstraintEqualTo(platformView.TrailingAnchor),
                m_mainButton.BottomAnchor.ConstraintEqualTo(platformView.BottomAnchor),
                m_mainButton.WidthAnchor.ConstraintEqualTo(50),
                m_mainButton.HeightAnchor.ConstraintEqualTo(50),
                
                m_content.TrailingAnchor.ConstraintEqualTo(platformView.TrailingAnchor),
                m_content.TopAnchor.ConstraintEqualTo(platformView.TopAnchor),
                m_content.WidthAnchor.ConstraintEqualTo(150)
            }
        );*/
    }
}