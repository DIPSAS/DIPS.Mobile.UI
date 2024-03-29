using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using DIPS.Mobile.UI.API.Library;
using DIPS.Mobile.UI.Extensions.Android;
using DIPS.Mobile.UI.Resources.LocalizedStrings.LocalizedStrings;
using DIPS.Mobile.UI.Resources.Styles;
using DIPS.Mobile.UI.Resources.Styles.Button;
using DIPS.Mobile.UI.Resources.Styles.Label;
using Google.Android.Material.Shape;
using Microsoft.Maui.Platform;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;
using DialogFragment = AndroidX.Fragment.App.DialogFragment;
using View = Android.Views.View;

namespace DIPS.Mobile.UI.Components.Pickers.ScrollPicker;

internal class MaterialScrollPickerFragment(
    ScrollPicker scrollPicker,
    IScrollPickerViewModel scrollPickerViewModel,
    Action onSave) : DialogFragment
{
    public override View OnCreateView(LayoutInflater inflater, ViewGroup? container, Bundle? savedInstanceState)
    {
        var linearLayout = new LinearLayout(Context)
        {
            LayoutParameters = new LinearLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent,
                ViewGroup.LayoutParams.WrapContent),
            Orientation = Orientation.Vertical
        };
        
        linearLayout.SetPadding(0, 0, 0, Sizes.GetSize(SizeName.size_2));

        var title = CreateTitle();
        var numberPickersLayout = CreateNumberPickersLayout(out var numberPickers);
        var buttonsLayout = CreateButtonsLayout(numberPickers);

        if(title is not null)
            linearLayout.AddView(title);
        linearLayout.AddView(new Space(Context)
        {
            LayoutParameters = new ViewGroup.LayoutParams(ViewGroup.LayoutParams.MatchParent, 20.0.ToMauiPixel())
        });
        linearLayout.AddView(numberPickersLayout);
        linearLayout.AddView(new Space(Context)
        {
            LayoutParameters = new ViewGroup.LayoutParams(ViewGroup.LayoutParams.MatchParent, 20.0.ToMauiPixel())
        });
        linearLayout.AddView(buttonsLayout);

        return linearLayout;
    }

    private View? CreateTitle()
    {
        if (string.IsNullOrEmpty(scrollPicker.Title))
            return null;
        
        var title = new Label
        {
            Style = Styles.GetLabelStyle(LabelStyle.Body100),
            CharacterSpacing = 1,
            Text = scrollPicker.Title.ToUpper(),
            TextColor = Colors.GetColor(ColorName.color_system_white),
        };
        var grid = new Grid
        {
            HeightRequest = Sizes.GetSize(SizeName.size_10),
            Children = { title },
            BackgroundColor = Colors.GetColor(ColorName.color_primary_90),
            Padding = new Thickness(Sizes.GetSize(SizeName.size_2), Sizes.GetSize(SizeName.size_3))
        }.ToPlatform(DUI.GetCurrentMauiContext!);
        return grid;
    }

    private LinearLayout CreateButtonsLayout(List<NumberPicker> numberPickers)
    {
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
            LayoutParameters =
                new LinearLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent,
                    ViewGroup.LayoutParams.WrapContent),
            Orientation = Orientation.Horizontal,
        };
        buttonsLayout.SetHorizontalGravity(GravityFlags.End);
        buttonsLayout.AddView(cancelButton.ToPlatform(DUI.GetCurrentMauiContext!));
        buttonsLayout.AddView(new Space(Context)
        {
            LayoutParameters = new ViewGroup.LayoutParams(1.0.ToMauiPixel(), ViewGroup.LayoutParams.MatchParent)
        });
        buttonsLayout.AddView(okButton.ToPlatform(DUI.GetCurrentMauiContext!));
        return buttonsLayout;
    }

    private LinearLayout CreateNumberPickersLayout(out List<NumberPicker> numberPickers)
    {
        var numberPickersLayout = new LinearLayout(Context)
        {
            LayoutParameters =
                new LinearLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent,
                    ViewGroup.LayoutParams.WrapContent),
            Orientation = Orientation.Horizontal
        };

        numberPickers = [];

        for (var i = 0; i < scrollPickerViewModel.GetComponentCount(); i++)
        {
            if(i == 0)
                numberPickersLayout.AddView(CreateHorizontalSpace(20.0));
            
            var numberPicker = CreateNumberPicker(i);
            numberPickers.Add(numberPicker);
            numberPickersLayout.AddView(numberPicker);

            numberPickersLayout.AddView(i == scrollPickerViewModel.GetComponentCount() - 1
                ? CreateHorizontalSpace(20.0)
                : CreateHorizontalSpace(4.0));
        }
        
        return numberPickersLayout;
    }
    
    private Space CreateHorizontalSpace(double space)
    {
        return new Space(Context)
        {
            LayoutParameters = new ViewGroup.LayoutParams(space.ToMauiPixel(), ViewGroup.LayoutParams.WrapContent)
        };
    }

    private static Buttons.Button CreateButton(string text, Action command)
    {
        return new Buttons.Button
        {
            Text = text,
            Style = Styles.GetButtonStyle(ButtonStyle.GhostSmall),
            FontSize = 14,
            CharacterSpacing = 1,
            TextColor = Colors.GetColor(ColorName.color_primary_90),
            FontFamily = "UI",
            Command = new Command(command),
            HorizontalOptions = LayoutOptions.End
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

    public override Dialog OnCreateDialog(Bundle? savedInstanceState)
    {
        var dialog = base.OnCreateDialog(savedInstanceState);

        var shapeAppearanceModel = new ShapeAppearanceModel.Builder().SetAllCorners(CornerFamily.Rounded, 12 * Context.GetDisplayDensity()).Build();
        var materialShapeDrawable = new MaterialShapeDrawable(shapeAppearanceModel);

        materialShapeDrawable.FillColor = Colors.GetColor(ColorName.color_system_white).ToDefaultColorStateList();
        materialShapeDrawable.StrokeWidth = 0;

        dialog.Window?.SetBackgroundDrawable(materialShapeDrawable);

        return dialog;
    }
}