using DataTemplate = Microsoft.Maui.Controls.DataTemplate;

namespace Playground.VetleSamples;

public class TemplateSelector : DataTemplateSelector
{
    public DataTemplate Test1 { get; set; }
    
    public DataTemplate Test2 { get; set; }
    
    public static int Test = 0;
    
    protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
    {
        if (Test == 0)
        {
            Test++;
            return new DataTemplate(() => new ViewCellTest());
        }
        else
        {
            return new DataTemplate(() => new VitalSignView());
        }
        
        
    }

}