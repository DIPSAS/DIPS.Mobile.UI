using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Playground.VetleSamples;

/// <summary>
/// Reproduces: When BindableLayout.ItemsSource is replaced with new VM instances
/// (simulating a server Refresh signal), ItemPicker in ContextMenu mode loses SelectedItem
/// even though the ViewModel has the correct value set.
///
/// Steps to reproduce:
/// 1. Run the app and navigate to this page
/// 2. Observe that "Indikasjon" ItemPicker shows "Monitorering" (pre-selected)
/// 3. Select a different radio option in the ChipGroup (simulating answering a criterion)
/// 4. Tap "Simulate Refresh" — this recreates all group VMs with the same data
/// 5. Bug: ItemPicker shows placeholder text instead of "Monitorering"
///    ChipGroup correctly shows the selected value
/// </summary>
public class ItemPickerRefreshReproViewModel : BindableObject
{
    private ObservableCollection<ReproCriterionGroupViewModel> m_groups = [];

    public ItemPickerRefreshReproViewModel()
    {
        BuildGroups();
    }

    public ObservableCollection<ReproCriterionGroupViewModel> Groups
    {
        get => m_groups;
        set
        {
            m_groups = value;
            OnPropertyChanged();
        }
    }

    /// <summary>
    /// Simulates what happens in CriteriaViewModel.CriterionChanged when server returns PatchCriterionResult.Refresh:
    /// All criteria are re-fetched and all VMs are recreated from scratch.
    /// </summary>
    public void SimulateRefresh()
    {
        // Recreate everything from scratch — new Criterion instances, new VMs
        // This is what happens in the real app when the server returns Refresh
        BuildGroups();
    }

    private void BuildGroups()
    {
        // Simulates two criterion groups returned from the server
        var group1 = new ReproCriterionGroupViewModel(
            "Fordi du valgte \"P-Faktor VIII\" i \"Von Willebrand sykdom\"",
            new ReproCriterion
            {
                Caption = "Indikasjon for rekvirering",
                Options =
                [
                    new ReproCriterionOption { Key = "1", Value = "Monitorering" },
                    new ReproCriterionOption { Key = "2", Value = "Utredning" },
                    new ReproCriterionOption { Key = "3", Value = "Kontroll" }
                ],
                Value = "1" // Server says "Monitorering" is selected
            });

        var group2 = new ReproCriterionGroupViewModel(
            "Kjent blødersykdom hos pasient",
            new ReproCriterion
            {
                Caption = "Kjent blødersykdom",
                Options =
                [
                    new ReproCriterionOption { Key = "yes", Value = "Ja" },
                    new ReproCriterionOption { Key = "no", Value = "Nei" },
                    new ReproCriterionOption { Key = "unknown", Value = "Vet ikke" }
                ],
                Value = "no" // Server says "Nei" is selected
            });

        Groups = new ObservableCollection<ReproCriterionGroupViewModel>([group1, group2]);
    }
}

public class ReproCriterionGroupViewModel : BindableObject
{
    private ReproCriterionOption? m_selectedOption;
    private List<object>? m_selectedChipOptions;

    public ReproCriterionGroupViewModel(string groupName, ReproCriterion criterion)
    {
        GroupName = groupName;
        Criterion = criterion;

        // Initialize selected option from server value (same as ChoiceCriterionViewModel constructor)
        m_selectedOption = criterion.Options?.FirstOrDefault(o => o.Key == criterion.Value);

        // Initialize chip selection (same as RadioCriterionViewModel constructor)
        var defaultChipOption = criterion.Options?.FirstOrDefault(o => o.Key == criterion.Value);
        if (defaultChipOption is not null)
        {
            m_selectedChipOptions = [defaultChipOption];
        }

        SelectedItemCommand = new Command<ReproCriterionOption>(OnSelectedItem);
    }

    public string GroupName { get; }
    public ReproCriterion Criterion { get; }

    public ReproCriterionOption? SelectedOption
    {
        get => m_selectedOption;
        set
        {
            m_selectedOption = value;
            OnPropertyChanged();
        }
    }

    public List<object>? SelectedChipOptions
    {
        get => m_selectedChipOptions;
        set
        {
            m_selectedChipOptions = value;
            OnPropertyChanged();
        }
    }

    public ICommand SelectedItemCommand { get; }

    private void OnSelectedItem(ReproCriterionOption option)
    {
        SelectedOption = option;
    }
}

public class ReproCriterion
{
    public string Caption { get; set; } = string.Empty;
    public List<ReproCriterionOption>? Options { get; set; }
    public string? Value { get; set; }
}

public class ReproCriterionOption
{
    public string Key { get; set; } = string.Empty;
    public string Value { get; set; } = string.Empty;
}
