using System.Windows.Input;
using DIPS.Mobile.UI.Components.Alerting.SystemMessage;
using DIPS.Mobile.UI.MVVM;
using DIPS.Mobile.UI.Resources.Colors;
using DIPS.Mobile.UI.Resources.Icons;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;
using Enum = System.Enum;

namespace Components.ComponentsSamples.Alerting.SystemMessages;

public class SystemMessageSamplesViewModel : ViewModel
{

    private Color? m_selectedTextColor;
    private Color? m_selectedBackgroundColor;
    private Color? m_selectedIconColor;
    private ImageSource? m_selectedIcon;
    private double m_duration = 3000;

    public SystemMessageSamplesViewModel()
    {
        DisplayCommand = new Command(Display);
        RemoveCommand = new Command(() => Remove(false));
        RemoveAnimateCommand = new Command(() => Remove(true));
        DisposeCommand = new Command(Dispose);
        ChangeTextColorCommand = new Command<string>(ChangeTextColor);
        ChangeBackgroundColorCommand = new Command<string>(ChangeBackgroundColor);
        ChangeIconColorCommand = new Command<string>(ChangeIconColor);
        ChangeIconCommand = new Command<string>(ChangeIcon);
        
        ColorList = new(DIPS.Mobile.UI.Extensions.Enum.ToList<ColorName>().Select(c => c.ToString()));
        IconList = new(DIPS.Mobile.UI.Extensions.Enum.ToList<IconName>().Select(i => i.ToString()));
    }

    private void ChangeIconColor(string selectedColor)
    {
        if(!Enum.TryParse<ColorName>(selectedColor, out var color))
            return;

        m_selectedIconColor = Colors.GetColor(color);
    }

    private void ChangeBackgroundColor(string selectedColor)
    {
        if(!Enum.TryParse<ColorName>(selectedColor, out var color))
            return;

        m_selectedBackgroundColor = Colors.GetColor(color);;
    }

    private void ChangeIcon(string selectedIcon)
    {
        if(!Enum.TryParse<IconName>(selectedIcon, out var icon))
            return;

        m_selectedIcon = Icons.GetIcon(icon);
    }

    private void ChangeTextColor(string selectedColor)
    {
        if(!Enum.TryParse<ColorName>(selectedColor, out var color))
            return;

        m_selectedTextColor = Colors.GetColor(color);
    }

    private void Remove(bool animate)
    {
        _ = SystemMessageService.Remove(animate);
    }

    private void Display()
    {
        SystemMessageService.Display(config =>
        {
            config.Duration = ((int)Duration);
            if(!string.IsNullOrEmpty(Input))
                config.Text = Input;
            if(m_selectedIconColor is not null)
                config.IconColor = m_selectedIconColor;
            if(m_selectedIcon is not null)
                config.Icon = m_selectedIcon;
            if(m_selectedTextColor is not null)
                config.TextColor = m_selectedTextColor;
            if(m_selectedBackgroundColor is not null)
                config.BackgroundColor = m_selectedBackgroundColor;
        });
    }
    
    private void Dispose()
    {
        SystemMessageService.Dispose();
    }
    
    public ICommand RemoveAnimateCommand { get; }
    public ICommand RemoveCommand { get; }
    public ICommand DisplayCommand { get; }
    public ICommand DisposeCommand { get; }
    public ICommand ChangeTextColorCommand { get; }
    public ICommand ChangeBackgroundColorCommand { get; }
    public ICommand ChangeIconColorCommand { get; }
    public ICommand ChangeIconCommand { get; }

    public string Input { get; set; }
    public List<string> ColorList { get; }
    public List<string> IconList { get; }
    public double Duration
    { 
        get => m_duration;
        set => RaiseWhenSet(ref m_duration, value); 
    }

}