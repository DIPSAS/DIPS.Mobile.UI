namespace Playground.HåvardSamples;

public partial class HåvardPage
{
    private int m_numberOfTimesTapped;

    public HåvardPage()
    {
        InitializeComponent();
    }

    private void NavigationListItem_OnTapped(object sender, EventArgs e)
    {
        label.Text = m_numberOfTimesTapped.ToString();
        m_numberOfTimesTapped++;
    }
}