namespace Playground.VetleSamples;

public partial class ItemPickerRefreshRepro
{
    private readonly ItemPickerRefreshReproViewModel m_viewModel;

    public ItemPickerRefreshRepro()
    {
        m_viewModel = new ItemPickerRefreshReproViewModel();
        BindingContext = m_viewModel;
        InitializeComponent();
    }

    private void OnSimulateRefresh(object sender, EventArgs e)
    {
        m_viewModel.SimulateRefresh();
    }
}
