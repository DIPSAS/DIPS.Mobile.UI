using DIPS.Mobile.UI.Components.BottomSheets;

namespace MemoryLeakTests.Tests;

public class BottomSheetTests : UITest
{
    public override void BeforeTest(ContentPage contentPage)
    {
        contentPage.Content = new VerticalStackLayout
        {
            Spacing = 8,
            Children =
            {
                new Button
                {
                    Text = "Open simple",
                    Command = new Command(() =>
                    {
                        _ = BottomSheetService.Open(new BottomSheet { Title = "Testing", BindingContext = "This is a test binding context" });
                    })
                },
                new Button
                {
                    Text = "Open with PushAsync + persistent bottom bar",
                    Command = new Command(async () =>
                    {
                        var sheet = new BottomSheet
                        {
                            Title = "Root",
                            BindingContext = "Persistent bottom bar test",
                            ShowBottombarButtonsOnSubViews = true,
                        };
                        sheet.BottombarButtons.Add(new Button { Text = "Reset" });
                        sheet.BottombarButtons.Add(new Button { Text = "Apply" });

                        await BottomSheetService.Open(sheet);

                        // Push a sub-view a moment after the sheet opens. The bottom bar
                        // should remain visible. Pop is triggered via the back navigation;
                        // the test verifies that everything is collected after the sheet closes.
                        await Task.Delay(300);
                        await sheet.PushAsync(new ContentPage { Title = "Sub-view", Content = new ContentView { Content = new Label { Text = "Sub-view" } } });
                    })
                }
            }
        };
    }

    public override string Name => "Bottom Sheet Tests";
}