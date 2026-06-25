using System.Collections.ObjectModel;
using DIPS.Mobile.UI.Components.ContextMenus;
using DIPS.Mobile.UI.Components.Dividers;
using DIPS.Mobile.UI.Effects.Touch;
using DIPS.Mobile.UI.Resources.Colors;
using DIPS.Mobile.UI.Resources.Icons;
using DIPS.Mobile.UI.Resources.Sizes;
using DIPS.Mobile.UI.Resources.Styles;
using DIPS.Mobile.UI.Resources.Styles.Label;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;
using ContextMenu = DIPS.Mobile.UI.Components.ContextMenus.ContextMenu;
using ContextMenuItem = DIPS.Mobile.UI.Components.ContextMenus.ContextMenuItem;
using DuiCollectionView = DIPS.Mobile.UI.Components.Lists.CollectionView;
using DuiContentPage = DIPS.Mobile.UI.Components.Pages.ContentPage;
using DuiImage = DIPS.Mobile.UI.Components.Images.Image.Image;
using DuiLabel = DIPS.Mobile.UI.Components.Labels.Label;
using DuiRefreshView = DIPS.Mobile.UI.Components.Loading.RefreshView;

namespace Playground.VetleSamples;

public class PatientListMenuFlingReproPage : DuiContentPage
{
    private readonly ObservableCollection<PatientReproItem> m_items = new(
        Enumerable.Range(1, 80).Select(index => new PatientReproItem(index)));

    private readonly DuiLabel m_statusLabel = new()
    {
        Style = Styles.GetLabelStyle(LabelStyle.UI200),
        Text = "No selection yet"
    };

    public PatientListMenuFlingReproPage()
    {
        Title = "Patient List Menu Fling Repro";

        var header = new Grid
        {
            Padding = new Thickness(
                Sizes.GetSize(SizeName.size_5),
                Sizes.GetSize(SizeName.size_4),
                Sizes.GetSize(SizeName.size_5),
                Sizes.GetSize(SizeName.size_2))
        };

        header.Add(new DuiLabel
        {
            Text = "Patients (80)",
            Style = Styles.GetLabelStyle(LabelStyle.SectionHeader),
            VerticalTextAlignment = TextAlignment.Center
        });

        var collectionView = new DuiCollectionView
        {
            ItemSpacing = Sizes.GetSize(SizeName.size_2),
            ShouldBounce = true,
            HasAdditionalSpaceAtTheEnd = true,
            ItemsSource = m_items,
            ItemTemplate = new DataTemplate(CreatePatientCell),
            EmptyView = m_statusLabel
        };

        var refreshView = new DuiRefreshView
        {
            Content = collectionView,
            Command = new Command(() => SetStatus("Refresh completed"))
        };

        refreshView.PropertyChanged += (_, args) =>
        {
            if (args.PropertyName == DuiRefreshView.IsRefreshingProperty.PropertyName && refreshView.IsRefreshing)
            {
                refreshView.IsRefreshing = false;
            }
        };

        Grid.SetRow(refreshView, 1);

        Content = new Grid
        {
            RowDefinitions =
            [
                new RowDefinition(GridLength.Auto),
                new RowDefinition(GridLength.Star)
            ],
            Children =
            {
                header,
                refreshView
            }
        };
    }

    private View CreatePatientCell()
    {
        var root = new Grid
        {
            ColumnDefinitions =
            [
                new ColumnDefinition(GridLength.Star),
                new ColumnDefinition(GridLength.Auto),
                new ColumnDefinition(GridLength.Auto)
            ],
            Padding = new Thickness(Sizes.GetSize(SizeName.size_3), 0, 0, 0),
            BackgroundColor = Colors.GetColor(ColorName.color_surface_default),
            RowDefinitions = [new RowDefinition(GridLength.Auto)]
        };

        Touch.SetCommand(root, new Command(() => SetStatus("Patient row tapped")));
        SemanticProperties.SetDescription(root, "Patient row");

        ContextMenuEffect.SetMode(root, ContextMenuEffect.ContextMenuMode.LongPressed);
        ContextMenuEffect.SetMenu(root, CreatePatientMenu("Row menu"));

        var textGrid = new Grid
        {
            RowDefinitions =
            [
                new RowDefinition(GridLength.Auto),
                new RowDefinition(GridLength.Auto)
            ]
        };

        var nameLabel = new DuiLabel
        {
            Style = Styles.GetLabelStyle(LabelStyle.UI300),
            MaxLines = 2,
            LineBreakMode = LineBreakMode.TailTruncation,
            Margin = new Thickness(0, Sizes.GetSize(SizeName.size_3), 0, 0)
        };
        nameLabel.SetBinding(Label.TextProperty, nameof(PatientReproItem.FullName));

        var personaliaLabel = new DuiLabel
        {
            Style = Styles.GetLabelStyle(LabelStyle.UI200),
            TextColor = Colors.GetColor(ColorName.color_text_subtle),
            Margin = new Thickness(0, 0, 0, Sizes.GetSize(SizeName.size_3))
        };
        personaliaLabel.SetBinding(Label.TextProperty, nameof(PatientReproItem.Personalia));

        textGrid.Add(nameLabel);
        textGrid.Add(personaliaLabel, 0, 1);
        root.Add(textGrid);

        root.Add(CreateIconStrip(), 1);
        root.Add(CreateOptionsButton(), 2);

        return root;
    }

    private static View CreateIconStrip()
    {
        var iconStrip = new Grid
        {
            ColumnDefinitions =
            [
                new ColumnDefinition(GridLength.Auto),
                new ColumnDefinition(GridLength.Auto)
            ],
            ColumnSpacing = Sizes.GetSize(SizeName.size_1),
            Margin = new Thickness(0, Sizes.GetSize(SizeName.size_3), Sizes.GetSize(SizeName.size_2), 0),
            VerticalOptions = LayoutOptions.Start
        };

        AutomationProperties.SetExcludedWithChildren(iconStrip, true);

        iconStrip.Add(new DuiImage
        {
            Source = Icons.GetIcon(IconName.comment_line),
            WidthRequest = Sizes.GetSize(SizeName.size_4),
            HeightRequest = Sizes.GetSize(SizeName.size_4)
        });

        iconStrip.Add(new DuiImage
        {
            Source = Icons.GetIcon(IconName.star_fill),
            WidthRequest = Sizes.GetSize(SizeName.size_4),
            HeightRequest = Sizes.GetSize(SizeName.size_4)
        }, 1);

        return iconStrip;
    }

    private View CreateOptionsButton()
    {
        var optionsButton = new Grid
        {
            ColumnDefinitions =
            [
                new ColumnDefinition(1),
                new ColumnDefinition(GridLength.Auto)
            ],
            ColumnSpacing = Sizes.GetSize(SizeName.size_2),
            HorizontalOptions = LayoutOptions.End,
            VerticalOptions = LayoutOptions.Fill
        };

        ContextMenuEffect.SetMode(optionsButton, ContextMenuEffect.ContextMenuMode.Pressed);
        ContextMenuEffect.SetMenu(optionsButton, CreatePatientMenu("Options menu"));
        SemanticProperties.SetDescription(optionsButton, "Patient options");
        AutomationProperties.SetIsInAccessibleTree(optionsButton, true);

        optionsButton.Add(new Divider
        {
            VerticalOptions = LayoutOptions.Fill
        });

        optionsButton.Add(new DuiImage
        {
            Source = Icons.GetIcon(IconName.more_fill),
            TintColor = Colors.GetColor(ColorName.color_icon_action),
            Margin = new Thickness(0, 0, Sizes.GetSize(SizeName.size_3), 0),
            VerticalOptions = LayoutOptions.Center,
            HorizontalOptions = LayoutOptions.Center,
            WidthRequest = Sizes.GetSize(SizeName.size_6),
            HeightRequest = Sizes.GetSize(SizeName.size_6)
        }, 1);

        return optionsButton;
    }

    private ContextMenu CreatePatientMenu(string title)
    {
        return new ContextMenu
        {
            Title = title,
            ItemsSource =
            [
                new ContextMenuItem { Title = "Open", Command = new Command(() => SetStatus($"{title}: Open")) },
                new ContextMenuItem { Title = "Favorite", Command = new Command(() => SetStatus($"{title}: Favorite")) },
                new ContextMenuItem { Title = "Remove", IsDestructive = true, Command = new Command(() => SetStatus($"{title}: Remove")) }
            ]
        };
    }

    private void SetStatus(string status)
    {
        m_statusLabel.Text = status;
        Console.WriteLine(status);
    }

    private sealed class PatientReproItem(int index)
    {
        public string FullName { get; } = $"Patient, Test {index:00}";
        public string Personalia { get; } = $"0101{index:00}12345 - Ward {index % 5 + 1}";
    }
}