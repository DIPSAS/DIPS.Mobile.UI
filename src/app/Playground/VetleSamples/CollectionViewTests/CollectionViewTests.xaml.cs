namespace Playground.VetleSamples.CollectionViewTests;

public partial class CollectionViewTests
{
    public CollectionViewTests()
    {
        InitializeComponent();
    }

    private void RegularCollectionView(object sender, EventArgs e)
    {
        Shell.Current.Navigation.PushAsync(new RegularCollectionView());
    }

    private void GroupedCollectionView(object sender, EventArgs e)
    {
        Shell.Current.Navigation.PushAsync(new GroupedCollectionView());
    }

    private void LoadMoreDividerRepro(object sender, EventArgs e)
    {
        Shell.Current.Navigation.PushAsync(new LoadMoreDividerReproPage());
    }

    private void SearchBarOutside(object sender, EventArgs e)
    {
        Shell.Current.Navigation.PushAsync(new RemoveFocusOnScrollPage(wrapInRefreshView: false, searchBarInHeader: false));
    }

    private void SearchBarOutsideWithRefresh(object sender, EventArgs e)
    {
        Shell.Current.Navigation.PushAsync(new RemoveFocusOnScrollPage(wrapInRefreshView: true, searchBarInHeader: false));
    }

    private void SearchBarInHeader(object sender, EventArgs e)
    {
        Shell.Current.Navigation.PushAsync(new RemoveFocusOnScrollPage(wrapInRefreshView: false, searchBarInHeader: true));
    }

    private void SearchBarInHeaderWithRefresh(object sender, EventArgs e)
    {
        Shell.Current.Navigation.PushAsync(new RemoveFocusOnScrollPage(wrapInRefreshView: true, searchBarInHeader: true));
    }

    private void ScrollViewSearchBarOutside(object sender, EventArgs e)
    {
        Shell.Current.Navigation.PushAsync(new RemoveFocusOnScrollScrollViewPage(searchBarInScrollView: false));
    }

    private void ScrollViewSearchBarInside(object sender, EventArgs e)
    {
        Shell.Current.Navigation.PushAsync(new RemoveFocusOnScrollScrollViewPage(searchBarInScrollView: true));
    }

    private void DelayedSearchBarInHeader(object sender, EventArgs e)
    {
        Shell.Current.Navigation.PushAsync(new RemoveFocusOnScrollPage(wrapInRefreshView: false, searchBarInHeader: true, delayedBinding: true));
    }

    private void DelayedSearchBarInHeaderWithRefresh(object sender, EventArgs e)
    {
        Shell.Current.Navigation.PushAsync(new RemoveFocusOnScrollPage(wrapInRefreshView: true, searchBarInHeader: true, delayedBinding: true));
    }

    private void ObservableAddSearchBarInHeader(object sender, EventArgs e)
    {
        Shell.Current.Navigation.PushAsync(new RemoveFocusOnScrollPage(wrapInRefreshView: false, searchBarInHeader: true, incrementalAdd: true));
    }
}