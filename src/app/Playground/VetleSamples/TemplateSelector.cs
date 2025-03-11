using DataTemplate = Microsoft.Maui.Controls.DataTemplate;

namespace Playground.VetleSamples;

public class TemplateSelector : DataTemplateSelector
{
    public DataTemplate Test1 { get; set; }
    
    public DataTemplate Test2 { get; set; }
    
    public static int Test = 0;
    
    protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
    {
        return new Random().Next(0, 2) == 0 ? Test1 : Test2;
    }

}