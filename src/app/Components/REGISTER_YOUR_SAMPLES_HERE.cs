using Components.ComponentsSamples.BottomSheets;
using Components.ComponentsSamples.Checkboxes;
using Components.ComponentsSamples.ContextMenus;
using Components.ComponentsSamples.Pickers;
using Components.ComponentsSamples.Searching;
using Components.Resources.LocalizedStrings;
using Components.ResourcesSamples.Colors;
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
            new(SampleType.Components, LocalizedStrings.Checkboxes, () => new CheckboxesSample()),
            new(SampleType.Resources, LocalizedStrings.Colors, () => new ColorsSamples()),
            new(SampleType.Resources, LocalizedStrings.Sizes, () => new SizesSamples()),
        };
    }
}