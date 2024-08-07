using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Playground.HåvardSamples;

public partial class HåvardPage3
{
    private readonly HåvardPageViewModel m_håvardPageViewModel;
    private readonly HåvardPage3ViewModel m_håvardPage3ViewModel;

    public HåvardPage3(HåvardPageViewModel håvardPageViewModel)
    {
        m_håvardPageViewModel = håvardPageViewModel;
        InitializeComponent();
        m_håvardPage3ViewModel = new HåvardPage3ViewModel();
        BindingContext = m_håvardPage3ViewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        m_håvardPageViewModel.SetListener(m_håvardPage3ViewModel);
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        // m_håvardPageViewModel.RemoveListener();
    }

    private void Button_OnClicked(object sender, EventArgs e)
    {
        m_håvardPageViewModel.DoSomething();
    }
}

public class HåvardPage3ViewModel
{
    public void DoSomethingBack()
    {
        
    }
}