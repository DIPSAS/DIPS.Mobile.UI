namespace MemoryLeakTests;

public partial class MyViewHandler
{
    internal partial void Connect()
    {
        //Subscribe to whatever
    }
    
    internal partial void Disconnect()
    {
        //Unsubsribe to whatever
        //Clean up resources
    }
}