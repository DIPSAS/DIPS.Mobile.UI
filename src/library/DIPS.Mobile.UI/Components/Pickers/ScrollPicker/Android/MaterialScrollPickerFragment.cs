using Android.OS;
using Android.Views;
using Android.Widget;
using DIPS.Mobile.UI.API.Library;
using DIPS.Mobile.UI.Extensions.Android;
using DIPS.Mobile.UI.Resources.LocalizedStrings.LocalizedStrings;
using DIPS.Mobile.UI.Resources.Styles;
using DIPS.Mobile.UI.Resources.Styles.Button;
using DIPS.Mobile.UI.Resources.Styles.Label;
using Microsoft.Maui.Platform;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;
using DialogFragment = AndroidX.Fragment.App.DialogFragment;
using View = Android.Views.View;

namespace DIPS.Mobile.UI.Components.Pickers.ScrollPicker;

public class MaterialScrollPickerFragment(ScrollPicker scrollPicker, IScrollPickerViewModel scrollPickerViewModel, Action onSave) : DialogFragment
{
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
            Text = scrollPicker.Title
        }.ToPlatform(DUI.GetCurrentMauiContext!);

        var numberPickersLayout = new LinearLayout(Context)
        {
            LayoutParameters = new LinearLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.WrapContent),
            Orientation = Orientation.Horizontal
        };
        
        List<NumberPicker> numberPickers = [];
        
        for (var i = 0; i < scrollPickerViewModel.GetComponentCount(); i++)
        {
            var numberPicker = CreateNumberPicker(i);
            numberPickers.Add(numberPicker);
            numberPickersLayout.AddView(numberPicker); 
        }
        
        var cancelButton = CreateButton(DUILocalizedStrings.Cancel.ToUpper(), Dismiss);
        var okButton = CreateButton("OK", () =>
        {
            Dismiss();

            for (var i = 0; i < numberPickers.Count; i++)
            {
                scrollPickerViewModel.SelectedRowInComponent(numberPickers[i].Value, i);
            }
            
            onSave.Invoke();
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
        linearLayout.AddView(numberPickersLayout);
        linearLayout.AddView(new Space(Context){LayoutParameters = new ViewGroup.LayoutParams(ViewGroup.LayoutParams.MatchParent, 20.0.ToMauiPixel())});
        linearLayout.AddView(buttonsLayout);

        return linearLayout;
    }

    private static Buttons.Button CreateButton(string text, Action command)
    {
        return new Buttons.Button
        {
            Text = text, Style = Styles.GetButtonStyle(ButtonStyle.GhostSmall), TextColor = Colors.GetColor(ColorName.color_primary_90), FontFamily = "UI", Command = new Command(command), HorizontalOptions = LayoutOptions.End
        };
    }

    private NumberPicker CreateNumberPicker(int component)
    {
        var numberPicker = new NumberPicker(Context)
        {
            LayoutParameters = new LinearLayout.LayoutParams(0, ViewGroup.LayoutParams.WrapContent, 1f)
        };

        var rowsInComponent = scrollPickerViewModel.GetRowsInComponent(component);

        var stringValues = new string[rowsInComponent];
        for (var i = 0; i < rowsInComponent; i++)
        {
            stringValues[i] = scrollPickerViewModel.GetTextForRowInComponent(i, component);
        }
        
        if (rowsInComponent == 0)
            return null!;

        numberPicker.SelectionDividerHeight = 0.5.ToMauiPixel();
        numberPicker.MinValue = 0;
        numberPicker.MaxValue = rowsInComponent - 1;
        numberPicker.WrapSelectorWheel = false;
        numberPicker.Value = scrollPickerViewModel.SelectedIndexForComponent(component);
        
        numberPicker.SetDisplayedValues(stringValues);

        return numberPicker;
    }
}