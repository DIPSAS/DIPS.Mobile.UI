namespace Playground.HåvardSamples;

public class Test : ContentView
{
    public Test()
    {
        
    }


    public static readonly BindableProperty IconOptionsProperty = BindableProperty.Create(
        nameof(IconOptions),
        typeof(Options),
        typeof(Test));

    public Options IconOptions
    {
        get => (Options)GetValue(IconOptionsProperty);
        set => SetValue(IconOptionsProperty, value);
    }
}

public class Options
{
    public string Lol { get; set; }
}