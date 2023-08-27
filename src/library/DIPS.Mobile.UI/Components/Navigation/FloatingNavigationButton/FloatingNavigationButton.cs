using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;

namespace DIPS.Mobile.UI.Components.Navigation.FloatingNavigationButton;

/// Must be a <see cref="Grid"/> because CascadingInputTransparent does not work on <see cref="ContentView"/>
internal class FloatingNavigationButton : Grid
{
    private readonly FloatingNavigationButtonConfigurator m_floatingNavigationButtonConfigurator;
    private readonly Grid m_contentGrid = new();

    private const int MenuButtonsSpacing = 75;

    private bool m_isExpanded;
    
#nullable disable
    private NavigationMenuButton.NavigationMenuButton m_mainButton;
    private Microsoft.Maui.Controls.Animation m_fadeOutColorAnimation;
    private Microsoft.Maui.Controls.Animation m_fadeInColorAnimation;
#nullable restore

    public FloatingNavigationButton(FloatingNavigationButtonConfigurator floatingNavigationButtonConfigurator)
    {
        m_floatingNavigationButtonConfigurator = floatingNavigationButtonConfigurator;
        
        Add(m_contentGrid);
        
        Padding = new Thickness(0, 0, Sizes.GetSize(SizeName.size_3), Sizes.GetSize(SizeName.size_13));
        CascadeInputTransparent = false;
        
        m_contentGrid.RowDefinitions = new RowDefinitionCollection { new() { Height = GridLength.Star } };
        m_contentGrid.ColumnDefinitions = new ColumnDefinitionCollection { new() { Width = GridLength.Auto } };
        m_contentGrid.HorizontalOptions = LayoutOptions.End;
        m_contentGrid.CascadeInputTransparent = false;
        m_contentGrid.InputTransparent = true;

        AddMainButton();
        CreateAnimations();
    }
    
    protected override void OnHandlerChanged()
    {
        base.OnHandlerChanged();
        
        InputTransparent = true;
    }

    public async Task Show(bool shouldAnimate)
    {
        IsVisible = true;
        if (shouldAnimate)
        {
            await this.FadeTo(1, easing: Easing.CubicOut);    
        }
        else
        {
            this.Opacity = 1;
        }
    }

    public async Task Hide(bool shouldAnimate)
    {
        if(Opacity == 0)
            return;
        if (shouldAnimate)
        {
            await this.FadeTo(0, easing: Easing.CubicIn);    
        }
        else
        {
            this.Opacity = 0;
        }
        
        IsVisible = false;
    }

    private void OnTappedBackground()
    {
        _ = Close();
    }

    private void CreateAnimations()
    {
        m_fadeOutColorAnimation = new Microsoft.Maui.Controls.Animation(alpha => BackgroundColor = new Color(0, 0, 0, (int)alpha), 100, 0);
        m_fadeInColorAnimation = new Microsoft.Maui.Controls.Animation(alpha => BackgroundColor = new Color(0, 0, 0, (int)alpha), 0, 100);
    }

    private void AddMainButton()
    {
        m_mainButton = new NavigationMenuButton.NavigationMenuButton
        {
            HorizontalOptions = LayoutOptions.End,
            VerticalOptions = LayoutOptions.End,
            Icon = Icons.GetIcon(IconName.arrow_right_s_line),
            ButtonBackgroundColor = Colors.GetColor(ColorName.color_obsolete_accent),
            Command = new Command(OnClickedMainButton),
            Opacity = .5,
            IconRotation = 270,
            
        };
        
        m_contentGrid.Add(m_mainButton);

    }

    private void OnClickedMainButton()
    {
        if (m_isExpanded)
        {
            _ = Close();
        }
        else
        {
            foreach (var navigationMenuButton in m_floatingNavigationButtonConfigurator.NavigationMenuButtons)
            {
                if(m_contentGrid.Contains(navigationMenuButton))
                    continue;
                m_contentGrid.Add(navigationMenuButton);
                navigationMenuButton.Opacity = 0;
                navigationMenuButton.VerticalOptions = LayoutOptions.End;
                navigationMenuButton.HorizontalOptions = LayoutOptions.End;
            }

            Expand();
        }
    }

    private void Expand()
    {
        InputTransparent = false;
        
        m_isExpanded = true;
        
        GestureRecognizers.Add(new TapGestureRecognizer
        {
            Command = new Command(OnTappedBackground)
        });
        
        this.AbortAnimation("FadeOut");
        m_fadeInColorAnimation.Commit(this, "FadeIn", easing: Easing.CubicOut);
        
        m_mainButton.RotateIconTo(90);
        m_mainButton.HideBadge();
        m_mainButton.Opacity = 1;
        
        for (var i = 1; i <= m_floatingNavigationButtonConfigurator.NavigationMenuButtons.Count; i++)
        {
            AnimateExpand(i);
        }
    }

    public async Task Close()
    {
        if(!m_isExpanded)
            return;
        
        InputTransparent = true;
        
        m_isExpanded = false;
        
        GestureRecognizers.RemoveAt(0);
        
        this.AbortAnimation("FadeIn");
        m_fadeOutColorAnimation.Commit(this, "FadeOut", easing: Easing.CubicIn);
        
        m_mainButton.RotateIconTo(270);
        m_mainButton.Opacity = .5;
        
        for (var i = 1; i <= m_floatingNavigationButtonConfigurator.NavigationMenuButtons.Count; i++)
        {
            AnimateClose(i);
        }
        
        // Wait for NavigationMenuButtons to animate before removing
        await Task.Delay(250);
            
        if(m_mainButton.BadgeCount != 0)
            m_mainButton.ShowBadge();
        
        foreach (var navigationMenuButton in m_floatingNavigationButtonConfigurator.NavigationMenuButtons)
        {
            m_contentGrid.Remove(navigationMenuButton);
        }
        
        
    }

    private void AnimateExpand(int index)
    {
        var navMenuButton = m_floatingNavigationButtonConfigurator.NavigationMenuButtons[index - 1];
        
        navMenuButton.Scale = 0.5;
        navMenuButton.FadeTo(1, easing: Easing.CubicOut);
        navMenuButton.TranslateTo(0, -MenuButtonsSpacing * index, easing: Easing.CubicOut);
        navMenuButton.ScaleTo(1, easing: Easing.CubicOut);
    }
    
    private void AnimateClose(int index)
    {
        var navMenuButton = m_floatingNavigationButtonConfigurator.NavigationMenuButtons[index - 1];
        
        navMenuButton.FadeTo(0, easing: Easing.CubicIn);
        navMenuButton.TranslateTo(0, 0, easing: Easing.CubicIn);
    }

    public void TryHideOrShowFloatingNavigationButton(ContentPage page, bool shouldAnimate = false)
    {
        if (m_floatingNavigationButtonConfigurator.PagesThatHidesButton.Contains(page.GetType()))
        {
            _ = Hide(shouldAnimate);
        }
        else
        {
            _ = Show(shouldAnimate);
        }
    }
    
    public void SetBadgeColor(Color color)
    {
        m_mainButton.BadgeColor = color;
    }

    private void UpdateBadgeCount()
    {
        var totalBadgeCount = m_floatingNavigationButtonConfigurator.NavigationMenuButtons.Sum(navBtn => navBtn.BadgeCount);
        m_mainButton.BadgeCount = totalBadgeCount;
    }

    public void SetNavigationMenuBadgeCount(string identifier, int value)
    {
        var navBtn = GetButtonFromIdentifier(identifier);
        
        if(navBtn is null)
            return;

        navBtn.BadgeCount = value;
        UpdateBadgeCount();
    }

    public void ChangeNavigationMenuButtonBadgeColor(string identifier, Color color)
    {
        var navBtn = GetButtonFromIdentifier(identifier);
        
        if(navBtn is null)
            return;

        navBtn.BadgeColor = color;
    }
    
    public void RemoveNavigationMenuButton(string identifier)
    {
        var navBtn = GetButtonFromIdentifier(identifier);

        if (navBtn is null)
            return;

        m_floatingNavigationButtonConfigurator.NavigationMenuButtons.Remove(navBtn);
        m_contentGrid.Remove(navBtn);
        
        for (var i = 1; i <= m_floatingNavigationButtonConfigurator.NavigationMenuButtons.Count; i++)
        {
            AnimateExpand(i);
        }
    }

    public void AddNavigationMenuButton(ExtendedNavigationMenuButton.ExtendedNavigationMenuButton navigationMenuButton, int? index)
    {
        var insertIndex = index ?? m_floatingNavigationButtonConfigurator.NavigationMenuButtons.Count;
        m_floatingNavigationButtonConfigurator.NavigationMenuButtons.Insert(insertIndex, navigationMenuButton);
        
        if(!m_isExpanded)
            return;
        
        m_contentGrid.Insert(insertIndex, navigationMenuButton);

        navigationMenuButton.Opacity = 0;
        navigationMenuButton.VerticalOptions = LayoutOptions.End;
        navigationMenuButton.HorizontalOptions = LayoutOptions.End;
        for (var i = 1; i <= m_floatingNavigationButtonConfigurator.NavigationMenuButtons.Count; i++)
        {
            AnimateExpand(i);
        }
    }
    
    public bool ContainsNavigationMenuButton(string identifier)
    {
        return GetButtonFromIdentifier(identifier) is not null;
    }
    
    public void SetButtonAvailability(string identifier, bool isEnabled)
    {
        var navBtn = GetButtonFromIdentifier(identifier);
        
        if(navBtn is null)
            return;

        navBtn.IsEnabled = isEnabled;
    }

    public bool CheckButtonAvailability(string identifier)
    {
        var navBtn = GetButtonFromIdentifier(identifier);
        
        return navBtn is not null && navBtn.IsEnabled;
    }
    
    private ExtendedNavigationMenuButton.ExtendedNavigationMenuButton? GetButtonFromIdentifier(string identifier) => 
        m_floatingNavigationButtonConfigurator.NavigationMenuButtons.FirstOrDefault(navButton => navButton.AutomationId.Equals(identifier));

}