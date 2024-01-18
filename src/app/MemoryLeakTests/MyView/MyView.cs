namespace MemoryLeakTests;

public class MyView : ContentView
{
public MyView()
{
    Loaded += Load;
    Unloaded += UnLoad;
}



private void Load(object? sender, EventArgs e)
{
    if (Handler is MyViewHandler myViewHandler)
    {
        myViewHandler.Connect();
    }
}

private void UnLoad(object? sender, EventArgs e)
{
    if (Handler is MyViewHandler myViewHandler)
    {
        myViewHandler.Disconnect();
    }
}

    public string Identifier { get; set; }
}