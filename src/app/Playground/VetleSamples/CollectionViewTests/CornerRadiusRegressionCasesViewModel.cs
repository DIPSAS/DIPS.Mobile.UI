using System.Collections.ObjectModel;

namespace Playground.VetleSamples.CollectionViewTests;

public class CornerRadiusRegressionCasesViewModel
{
    private int m_planElementCounter;

    public CornerRadiusRegressionCasesViewModel()
    {
        AddLastPlanElementCommand = new Command(AddLastPlanElement);
        MinimizeFirstPlanElementGroupCommand = new Command(MinimizeFirstPlanElementGroup);
        ExpandFirstPlanElementGroupCommand = new Command(ExpandFirstPlanElementGroup);
        ResetPlanElementGroupsCommand = new Command(ResetPlanElementGroups);

        ResetPlanElementGroups();
    }

    public ObservableCollection<CornerRadiusCardItem> RootStyledCards { get; } =
    [
        new("Opprett dokument", "Template root has its own corner radius and stroke."),
        new("Timebok", "CollectionView AutoCornerRadius is false."),
        new("Kritisk informasjon", "The root styling should survive cell styling."),
        new("Egne pasienter", "A neutral wrapper should not be required.")
    ];

    public ObservableCollection<PlanElementGroup> PlanElementGroups { get; } = [];

    public Command AddLastPlanElementCommand { get; }
    public Command MinimizeFirstPlanElementGroupCommand { get; }
    public Command ExpandFirstPlanElementGroupCommand { get; }
    public Command ResetPlanElementGroupsCommand { get; }

    private void AddLastPlanElement()
    {
        if (PlanElementGroups.Count == 0)
            ResetPlanElementGroups();

        PlanElementGroups[0].Add(CreatePlanElement("Nytt nederste planelement", "New last item should take bottom corner radius and hide the divider.", "#8E7CC3"));
    }

    private void MinimizeFirstPlanElementGroup()
    {
        if (PlanElementGroups.Count == 0)
            return;

        while (PlanElementGroups[0].Count > 1)
        {
            PlanElementGroups[0].RemoveAt(PlanElementGroups[0].Count - 1);
        }
    }

    private void ExpandFirstPlanElementGroup()
    {
        if (PlanElementGroups.Count == 0)
            ResetPlanElementGroups();

        while (PlanElementGroups[0].Count < 4)
        {
            AddLastPlanElement();
        }
    }

    private void ResetPlanElementGroups()
    {
        m_planElementCounter = 0;
        PlanElementGroups.Clear();

        PlanElementGroups.Add(new PlanElementGroup("Runtime mutation section",
        [
            CreatePlanElement("Forste planelement", "Should have top corners.", "#69B7D6"),
            CreatePlanElement("Midterste planelement", "Should not have rounded corners.", "#7BBF8A"),
            CreatePlanElement("Nederste planelement", "Should have bottom corners and no divider.", "#F07F7F")
        ]));

        PlanElementGroups.Add(new PlanElementGroup("Single item section",
        [
            CreatePlanElement("Eneste planelement", "Should have all corners and no divider.", "#F4B740")
        ]));
    }

    private PlanElementItem CreatePlanElement(string title, string description, string color)
    {
        m_planElementCounter++;
        return new PlanElementItem($"{title} {m_planElementCounter}", description, Color.FromArgb(color));
    }
}

public record CornerRadiusCardItem(string Title, string Description);

public class PlanElementGroup : ObservableCollection<PlanElementItem>
{
    public PlanElementGroup(string header, IEnumerable<PlanElementItem> items) : base(items)
    {
        Header = header;
    }

    public string Header { get; }
}

public record PlanElementItem(string Title, string Description, Color StatusColor);
