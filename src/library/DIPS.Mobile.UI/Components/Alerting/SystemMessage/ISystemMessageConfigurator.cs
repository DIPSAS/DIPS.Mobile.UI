namespace DIPS.Mobile.UI.Components.Alerting.SystemMessage;

public interface ISystemMessageConfigurator
{
    /// <summary>
    /// Sets the background color of the message
    /// </summary>
    Color BackgroundColor { get; set; }
    
    /// <summary>
    /// Sets the text color 
    /// </summary>
    Color TextColor { get; set; }
    
    /// <summary>
    /// Sets the icon color
    /// </summary>
    Color IconColor { get; set; }
    
    /// <summary>
    /// Sets how long the message should be visible for
    /// </summary>
    float Duration { get; set; }
    
    /// <summary>
    /// Sets what text should be displayed
    /// </summary>
    string Text { get; set; }
    
    /// <summary>
    /// Sets what icon should be displayed next to the text
    /// </summary>
    ImageSource? Icon { get; set; }
}