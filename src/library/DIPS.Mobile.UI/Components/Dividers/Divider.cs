using System.Collections;
using DIPS.Mobile.UI.Internal;
using Microsoft.Maui.Controls.Shapes;

namespace DIPS.Mobile.UI.Components.Dividers;

public partial class Divider : ContentView
{
    public Divider()
    {
        this.SetAppThemeColor(BackgroundColorProperty, ColorName.color_stroke_default);

        var line = new Line{ AutomationId = "Line".ToDUIAutomationId<Divider>()};
        line.SetBinding(BackgroundProperty, static (Divider divider) => divider.BackgroundColor, source: this);
        Content = line;
    }

    /*protected override void OnHandlerChanged()
    {
        base.OnHandlerChanged();

        if (Handler is not null)
        {
            if(AutoVisibility)
                UpdateVisibility();
        }
    }*/

    protected override void OnParentChanged()
    {
        base.OnParentChanged();
        
        if (AutoVisibility)
            UpdateVisibility();
    }

    private void UpdateVisibility()
    {
        var collectionView = this.FindParentOfType<CollectionView>();
        if (collectionView is not null)
        {
            if (collectionView.IsGrouped)
            {
                var groupedItemsSource = collectionView.ItemsSource as IList<IList>;
            }
        }
    }
}