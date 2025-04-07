using Components.ComponentsSamples.Alerting;
using Components.ComponentsSamples.AmplitudeView;
using Components.ComponentsSamples.BarcodeScanning;
using Components.ComponentsSamples.BottomSheets;
using Components.ComponentsSamples.Buttons;
using Components.ComponentsSamples.Chips;
using Components.ComponentsSamples.ContextMenus;
using Components.ComponentsSamples.ImageCapturing;
using Components.ComponentsSamples.Labels;
using Components.ComponentsSamples.ListItems;
using Components.ComponentsSamples.Loading;
using Components.ComponentsSamples.Navigation;
using Components.ComponentsSamples.Pickers;
using Components.ComponentsSamples.Saving;
using Components.ComponentsSamples.Searching;
using Components.ComponentsSamples.Selection;
using Components.ComponentsSamples.Sorting;
using Components.ComponentsSamples.SyntaxHighlighting;
using Components.ComponentsSamples.Text;
using Components.ComponentsSamples.TextFields;
using Components.ComponentsSamples.Tip;
using Components.Resources.LocalizedStrings;
using Components.ResourcesSamples.Animations;
using Components.ResourcesSamples.Colors;
using Components.ResourcesSamples.Icons;
using Components.ResourcesSamples.Sizes;

namespace Components;

// ReSharper disable once InconsistentNaming
public static class REGISTER_YOUR_SAMPLES_HERE
{
    public static List<Sample> RegisterSamples()
    {
        return new List<Sample>()
        {
            new(SampleType.Components, LocalizedStrings.BottomSheet, () => new BottomSheetSamples()),
            new(SampleType.Components, LocalizedStrings.Pickers, () => new PickersSample()),
            new(SampleType.Components, LocalizedStrings.ContextMenu, () => new ContextMenuSamples()),
            new(SampleType.Components, LocalizedStrings.Searching, () => new SearchingSamples()),
            new(SampleType.Resources, LocalizedStrings.Colors, () => new ColorsSamples()),
            new(SampleType.Resources, LocalizedStrings.Sizes, () => new SizesSamples()),
            new(SampleType.Resources, LocalizedStrings.Animations, () => new AnimationsSamples()),
            new(SampleType.Components, LocalizedStrings.ListItems, () => new ListItemsSamples()),
            new(SampleType.Components, LocalizedStrings.Chip, () => new ChipsSamples()),
            new(SampleType.Resources, LocalizedStrings.Icons, () => new IconsSamples()),
            new(SampleType.Components, LocalizedStrings.Navigation, () => new NavigationSamples()),
            new(SampleType.Components, LocalizedStrings.Loading, () => new LoadingSamples()),
            new(SampleType.Components, LocalizedStrings.Alerting, () => new AlertingSamples()),
            new(SampleType.Components, LocalizedStrings.Saving, () => new SavingSamples()),
            new(SampleType.Components, "Selection", () => new SelectionSamples()),
            new(SampleType.Components, LocalizedStrings.Sorting, () => new SortingSamples()),
            new(SampleType.Components, LocalizedStrings.Buttons, () => new ButtonsSamples()),
            new(SampleType.Components, "Labels", () => new LabelsSamples()),
            new(SampleType.Components, LocalizedStrings.TextFields, () => new TextFieldsSamples()),
            new(SampleType.Components, LocalizedStrings.BarcodeScanning, () => new BarcodeScanningSample()),
            new(SampleType.Components, LocalizedStrings.PhotoCapturing, () => new ImageCaptureSample(), true),
            new(SampleType.Components, "Tip", () => new TipSamples()),
            new(SampleType.Components, "Syntax Highlighting", () => new SyntaxHighlightingSamples()),
            new(SampleType.Components, "Amplitude View", () => new AmplitudeViewSamples()),
            new(SampleType.Components, "Text", () => new TextSamples()),

            



        };
    }
}