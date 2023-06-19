namespace DIPS.Mobile.UI.Components.Alerts.SystemMessage;

public interface ISystemMessageConfigurator
{
    /// <summary>
    /// Sets the background color of the message
    /// </summary>
    /// <param name="color">The background color to be displayed</param>
    void SetBackgroundColor(Color color);

    /// <summary>
    /// Sets the text color 
    /// </summary>
    /// <param name="color">The text color to be displayed</param>
    void SetTextColor(Color color);

    /// <summary>
    /// Sets the icon color
    /// </summary>
    /// <param name="color">The icon color to be displayed</param>
    void SetIconColor(Color color);
    
    /// <summary>
    /// Sets how long the message should be visible for
    /// </summary>
    /// <param name="duration">in milliseconds</param>
    /// <remarks>default is 2500</remarks>
    void SetDuration(float duration);
    
    /// <summary>
    /// Sets what text should be displayed
    /// </summary>
    /// <param name="text">The text to be displayed</param>
    void SetText(string text);

    /// <summary>
    /// Sets what icon should be displayed next to the text
    /// </summary>
    /// <param name="imageSource">The icon to be displayed</param>
    void SetIcon(ImageSource imageSource);
}