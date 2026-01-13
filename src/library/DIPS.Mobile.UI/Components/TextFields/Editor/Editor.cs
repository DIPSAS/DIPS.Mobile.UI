using DIPS.Mobile.UI.Formatters;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;

namespace DIPS.Mobile.UI.Components.TextFields.Editor;

public partial class Editor : Microsoft.Maui.Controls.Editor
{
    public Editor()
    {
        PlaceholderColor = Colors.GetColor(ColorName.color_text_subtle);
        FontFamily = "Body";
        FontSize = 16;
        TextColor = Colors.GetColor(ColorName.color_text_default);
        Keyboard = Keyboard.Text;

        Unfocused += OnUnfocusedReplaceEmojis;
    }

    private void OnUnfocusedReplaceEmojis(object? sender, FocusEventArgs e)
    {
        Text = StringFormatter.ReplaceAllEmojisWithPlaceholder(Text);
    }

    protected override void OnHandlerChanging(HandlerChangingEventArgs args)
    {
        base.OnHandlerChanging(args);

        if (args.NewHandler is not null)
            return;

        Unfocused -= OnUnfocusedReplaceEmojis;
    }
}