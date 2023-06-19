namespace DIPS.Mobile.UI.Components.Alerting.SystemMessage;

public static partial class SystemMessageService
{
    private static readonly Queue<SystemMessage> m_systemMessageQueue = new();
    internal const int SystemMessageTagId = 291096;

    public static void Display(Action<ISystemMessageConfigurator> config)
    {
        var configurator = new SystemMessageConfigurator();
        config.Invoke(configurator);
        
        var systemMessage = new SystemMessage(configurator, () => _ = Remove(true));
        m_systemMessageQueue.Enqueue(systemMessage);
        
        // If this is the first message, show it, otherwise just store it 
        if (m_systemMessageQueue.Count != 1)
            return;

        PlatformShow(systemMessage);
        systemMessage.Show();

    }

    public static async Task Remove(bool animate)
    {
        if (!m_systemMessageQueue.TryDequeue(out var systemMessage))
            return;

        if (animate)
            await systemMessage.Hide();
        
        systemMessage.Dispose();
        PlatformRemove();
        
        // If there are more messages stored in the queue, show the next one
        if (m_systemMessageQueue.Count == 0)
            return;

        systemMessage = m_systemMessageQueue.Peek();
        PlatformShow(systemMessage);
        systemMessage.Show();
    }

    private static partial void PlatformRemove();
    
    private static partial void PlatformShow(SystemMessage systemMessage);

    public static void Dispose()
    {
        if (!m_systemMessageQueue.TryDequeue(out var systemMessage))
            return;

        systemMessage.Dispose();
        PlatformRemove();
        
        m_systemMessageQueue.Clear();
    }

}