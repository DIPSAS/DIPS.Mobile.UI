namespace Playground.VetleSamples;

public class TemplateSelector : DataTemplateSelector
{
    public DataTemplate Test1 { get; set; }
    
    public DataTemplate Test2 { get; set; }
    
    protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
    {
        return Random.Shared.Next(0, 2) == 0 ? Test1 : Test2;
    }
}