using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DIPS.Mobile.UI.API.Camera.Scanning;

namespace Components.ComponentsSamples.BarcodeScanning;

public partial class BarcodeScanningSample
{
    private readonly Scanner m_scanner;

    public BarcodeScanningSample()
    {
        InitializeComponent();
        m_scanner = new Scanner();
    }

    private async void StartScanning(object? sender, EventArgs e)
    {
        await Start();
    }

    private async Task Start()
    {
        try
        {
            var result = await m_scanner.Start(Preview);
            m_scanner.Stop();
            Application.Current.MainPage.DisplayAlert("Woah!", result, "Ok");
        }
        catch (Exception exception)
        {
            Application.Current.MainPage.DisplayAlert("Failed, check console!", exception.Message, "Ok");
            Console.WriteLine(exception);
        }
    }

    private void StopScanning(object? sender, EventArgs e)
    {
        m_scanner.Stop();
    }

    protected override void OnDisappearing()
    {
        m_scanner.Stop();
        base.OnDisappearing();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
    }
}