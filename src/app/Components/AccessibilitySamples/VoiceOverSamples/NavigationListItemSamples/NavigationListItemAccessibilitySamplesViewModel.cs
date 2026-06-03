using System.Windows.Input;
using Components.Resources.LocalizedStrings;
using DIPS.Mobile.UI.MVVM;

namespace Components.AccessibilitySamples.VoiceOverSamples.NavigationListItemSamples;

public class NavigationListItemAccessibilitySamplesViewModel : ViewModel
{
    public NavigationListItemAccessibilitySamplesViewModel()
    {
        ItemActivatedCommand = new Command(() => { });

        CollectionItems =
        [
            new NavigationListItemSampleItem(LocalizedStrings.Buttons, LocalizedStrings.VoiceOver_NavigationListItem_Collection_ButtonSubtitle),
            new NavigationListItemSampleItem(LocalizedStrings.ListItems, LocalizedStrings.VoiceOver_NavigationListItem_Collection_ListItemSubtitle),
            new NavigationListItemSampleItem(LocalizedStrings.Pickers, LocalizedStrings.VoiceOver_NavigationListItem_Collection_PickerSubtitle),
            new NavigationListItemSampleItem(LocalizedStrings.TextFields, LocalizedStrings.VoiceOver_NavigationListItem_Collection_TextFieldSubtitle)
        ];
    }

    public ICommand ItemActivatedCommand { get; }

    public IReadOnlyList<NavigationListItemSampleItem> CollectionItems { get; }
}

public class NavigationListItemSampleItem
{
    public NavigationListItemSampleItem(string title, string subtitle)
    {
        Title = title;
        Subtitle = subtitle;
        Command = new Command(() => { });
    }

    public string Title { get; }

    public string Subtitle { get; }

    public ICommand Command { get; }
}
