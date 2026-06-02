using System.Windows.Input;

namespace Playground.HåvardSamples;

/// <summary>
/// Repro page for Arena.Mobile #1455:
/// AlertView save button not visible with large text size on iOS 26.
/// Also tests that AlertView layout is correct when toggling IsVisible.
/// </summary>
public partial class AlertViewReproPage
{
    private int m_toggleCount;
    
    public ICommand SaveCommand { get; }

    public AlertViewReproPage()
    {
        SaveCommand = new Command(OnSave);
        InitializeComponent();
    }

    private void OnSave()
    {
        StatusLabel.Text = "Status: Lagre-knapp trykket!";
        AlertWithButton.IsVisible = false;
        AlertSuccess.IsVisible = true;
    }

    private void ToggleAlertView(object sender, EventArgs e)
    {
        m_toggleCount++;
        AlertWithButton.IsVisible = !AlertWithButton.IsVisible;
        StatusLabel.Text = $"Status: Toggle #{m_toggleCount}, IsVisible={AlertWithButton.IsVisible}";
    }

    private void ShowAlertView(object sender, EventArgs e)
    {
        AlertWithButton.IsVisible = true;
        StatusLabel.Text = "Status: Show (IsVisible=True)";
    }

    private void HideAlertView(object sender, EventArgs e)
    {
        AlertWithButton.IsVisible = false;
        StatusLabel.Text = "Status: Hide (IsVisible=False)";
    }

    private void ToggleAggressiveAlertView(object sender, EventArgs e)
    {
        AlertAggressive.IsVisible = !AlertAggressive.IsVisible;
    }

    private void ToggleLongTitleAlertView(object sender, EventArgs e)
    {
        AlertLongTitle.IsVisible = !AlertLongTitle.IsVisible;
    }

    private void ToggleSuccessAlertView(object sender, EventArgs e)
    {
        AlertSuccess.IsVisible = !AlertSuccess.IsVisible;
    }
}
