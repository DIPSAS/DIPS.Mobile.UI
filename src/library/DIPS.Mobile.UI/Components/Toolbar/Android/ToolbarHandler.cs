using Android.Content.Res;
using Android.Views;
using Android.Widget;
using DIPS.Mobile.UI.API.Library;
using Google.Android.Material.Button;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;

namespace DIPS.Mobile.UI.Components.Toolbar;

public partial class ToolbarHandler : ViewHandler<Toolbar, LinearLayout>
{
    private LinearLayout? m_buttonsLayout;
    private readonly List<(MaterialButton Button, EventHandler Handler)> m_buttonHandlers = [];

    protected override LinearLayout CreatePlatformView()
    {
        // Outer vertical layout: [top border row] + [buttons row]
        var outer = new LinearLayout(Context)
        {
            Orientation = Orientation.Vertical,
        };
        outer.SetBackgroundColor(Resources.Colors.Colors.GetColor(ColorName.color_surface_default).ToPlatform());

        // Top border
        var density = Context?.Resources?.DisplayMetrics?.Density ?? 1f;
        var borderPx = (int)(Sizes.GetSize(SizeName.stroke_small) * density);
        var border = new View(Context);
        border.SetBackgroundColor(Resources.Colors.Colors.GetColor(ColorName.color_border_default).ToPlatform());
        outer.AddView(border, new LinearLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent, borderPx));

        // Buttons row: fills remaining space, centers buttons vertically (M3 spec: icons vertically centered in 80dp bar)
        m_buttonsLayout = new LinearLayout(Context)
        {
            Orientation = Orientation.Horizontal,
            Gravity = GravityFlags.CenterVertical,
        };
        outer.AddView(m_buttonsLayout, new LinearLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent));

        return outer;
    }

    protected override void ConnectHandler(LinearLayout platformView)
    {
        base.ConnectHandler(platformView);
        UpdateButtons();
    }

    protected override void DisconnectHandler(LinearLayout platformView)
    {
        UnsubscribeAllButtonHandlers();
        base.DisconnectHandler(platformView);
    }

    private static partial void MapButtons(ToolbarHandler handler, Toolbar toolbar)
    {
        handler.UpdateButtons();
    }

    private void UpdateButtons()
    {
        UnsubscribeAllButtonHandlers();
        m_buttonsLayout?.RemoveAllViews();

        if (VirtualView.Buttons is null || m_buttonsLayout is null)
            return;

        foreach (var toolbarButton in VirtualView.Buttons)
        {
            var (button, handler) = CreateMaterialIconButton(toolbarButton);
            var layoutParams = new LinearLayout.LayoutParams(0, ViewGroup.LayoutParams.WrapContent, 1f);
            m_buttonsLayout.AddView(button, layoutParams);
            m_buttonHandlers.Add((button, handler));
        }
    }

    private void UnsubscribeAllButtonHandlers()
    {
        foreach (var (button, handler) in m_buttonHandlers)
        {
            button.Click -= handler;
        }
        m_buttonHandlers.Clear();
    }

    private (MaterialButton Button, EventHandler Handler) CreateMaterialIconButton(ToolbarButton toolbarButton)
    {
        var iconColor = Resources.Colors.Colors.GetColor(ColorName.color_icon_action).ToPlatform();
        var iconColorStateList = new ColorStateList(
            [[Android.Resource.Attribute.StateEnabled], [-Android.Resource.Attribute.StateEnabled]],
            [iconColor, Resources.Colors.Colors.GetColor(ColorName.color_icon_disabled).ToPlatform()]);

        var button = new MaterialButton(Context!, null, Resource.Attribute.materialIconButtonStyle)
        {
            IconGravity = MaterialButton.IconGravityTextTop,
            IconTint = iconColorStateList,
            SoundEffectsEnabled = false,
        };

        button.Enabled = toolbarButton.IsEnabled;

        if (!string.IsNullOrEmpty(toolbarButton.Title))
        {
            button.ContentDescription = toolbarButton.Title;
        }

        if (toolbarButton.Icon is not null &&
            DUI.TryGetDrawableFromFileImageSource(toolbarButton.Icon, out var drawable) &&
            drawable is not null)
        {
            button.Icon = drawable;
        }

        void OnClick(object? sender, EventArgs e) =>
            toolbarButton.Command?.Execute(toolbarButton.CommandParameter);

        button.Click += OnClick;

        return (button, OnClick);
    }
}
