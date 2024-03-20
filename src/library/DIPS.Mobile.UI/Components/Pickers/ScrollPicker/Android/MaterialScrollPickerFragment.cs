using Android.OS;
using Android.Views;
using Android.Widget;
using DIPS.Mobile.UI.API.Library;
using DIPS.Mobile.UI.Effects.Touch;
using DIPS.Mobile.UI.Extensions.Android;
using DIPS.Mobile.UI.Resources.LocalizedStrings.LocalizedStrings;
using DIPS.Mobile.UI.Resources.Styles;
using DIPS.Mobile.UI.Resources.Styles.Button;
using DIPS.Mobile.UI.Resources.Styles.Label;
using Microsoft.Maui.Platform;
using Button = Microsoft.Maui.Controls.Button;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;
using DialogFragment = AndroidX.Fragment.App.DialogFragment;
using View = Android.Views.View;

namespace DIPS.Mobile.UI.Components.Pickers.ScrollPicker;

public class MaterialScrollPickerFragment : DialogFragment
{
    private readonly ScrollPicker m_scrollPicker;

    public MaterialScrollPickerFragment(ScrollPicker scrollPicker)
    {
        m_scrollPicker = scrollPicker;
    }
    
    public override View? OnCreateView(LayoutInflater inflater, ViewGroup? container, Bundle? savedInstanceState)
    {
        var linearLayout = new LinearLayout(Context)
        {
            LayoutParameters = new LinearLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent,
                ViewGroup.LayoutParams.WrapContent),
            Orientation = Orientation.Vertical
        };
        
        var horizontalPadding = ((double)Sizes.GetSize(SizeName.size_3)).ToMauiPixel();
        var verticalPadding = ((double)Sizes.GetSize(SizeName.size_2)).ToMauiPixel();

        linearLayout.SetPadding(horizontalPadding, verticalPadding, horizontalPadding, verticalPadding);
        
        var title = new Label
        {
            Style = Styles.GetLabelStyle(LabelStyle.SectionHeader),
            Text = m_scrollPicker.Title
        }.ToPlatform(DUI.GetCurrentMauiContext!);
        
        var numberPicker = new NumberPicker(Context)
        {
            LayoutParameters = new ViewGroup.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.WrapContent)
        };

        var items = m_scrollPicker.ItemsSource.Cast<object>();

        var enumerable = items.ToList();
        if (enumerable.Count == 0)
            return new View(Context);

        numberPicker.SelectionDividerHeight = 0.5.ToMauiPixel();
        numberPicker.MinValue = 0;
        numberPicker.MaxValue = enumerable.Count - 1;
        numberPicker.WrapSelectorWheel = false;
        numberPicker.Value = m_scrollPicker.SelectedIndex;
        
        var stringValues = enumerable.Select(item => item.ToString());
        
        numberPicker.SetDisplayedValues(stringValues.ToArray()!);

        var cancelButton = CreateButton(DUILocalizedStrings.Cancel.ToUpper(), Dismiss);
        var okButton = CreateButton("OK", () =>
        {
            Dismiss();
            m_scrollPicker.SelectedIndex = numberPicker.Value;
        });
        
        var buttonsLayout = new LinearLayout(Context)
        {
            LayoutParameters = new LinearLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.WrapContent),
            Orientation = Orientation.Horizontal,
            
        };
        buttonsLayout.SetHorizontalGravity(GravityFlags.End);
        buttonsLayout.AddView(cancelButton.ToPlatform(DUI.GetCurrentMauiContext!));
        buttonsLayout.AddView(new Space(Context){LayoutParameters = new ViewGroup.LayoutParams(4.0.ToMauiPixel(), ViewGroup.LayoutParams.MatchParent)});
        buttonsLayout.AddView(okButton.ToPlatform(DUI.GetCurrentMauiContext!));
        
        linearLayout.AddView(title);
        linearLayout.AddView(new Space(Context){LayoutParameters = new ViewGroup.LayoutParams(ViewGroup.LayoutParams.MatchParent, 20.0.ToMauiPixel())});
        linearLayout.AddView(numberPicker);
        linearLayout.AddView(new Space(Context){LayoutParameters = new ViewGroup.LayoutParams(ViewGroup.LayoutParams.MatchParent, 20.0.ToMauiPixel())});
        linearLayout.AddView(buttonsLayout);

        return linearLayout;
    }

    private Button CreateButton(string text, Action command)
    {
        return new Buttons.Button
        {
            Text = text, Style = Styles.GetButtonStyle(ButtonStyle.GhostSmall), TextColor = Colors.GetColor(ColorName.color_primary_90), FontFamily = "UI", Command = new Command(command), HorizontalOptions = LayoutOptions.End
        };
    }
}