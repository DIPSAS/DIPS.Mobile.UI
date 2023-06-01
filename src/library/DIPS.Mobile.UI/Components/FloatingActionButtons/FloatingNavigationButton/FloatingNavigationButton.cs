using System.Collections.Specialized;
using DIPS.Mobile.UI.Effects.DUIImageEffect;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;

namespace DIPS.Mobile.UI.Components.FloatingActionButtons.FloatingNavigationButton;

/// Must be a <see cref="Grid"/> because CascadingInputTransparent does not work on <see cref="ContentView"/>
internal partial class FloatingNavigationButton : Grid
{
    private readonly FloatingNavigationButtonConfigurator m_floatingNavigationButtonConfigurator;
    private readonly Grid m_contentGrid = new();

    private const int MenuButtonsSpacing = 75;

    private bool m_isExpanded;
    
#nullable disable
    private FloatingActionButton.FloatingActionButton m_mainButton;
    private Animation m_fadeOutColorAnimation;
    private Animation m_fadeInColorAnimation;
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

        AddMainButton();
        CreateAnimations();
    }
    protected override void OnHandlerChanged()
    {
        base.OnHandlerChanged();
        
        InputTransparent = true;
        
        Microsoft.Maui.Controls.Shell.Current.Navigating += OnNavigating;

        m_floatingNavigationButtonConfigurator.NavigationMenuButtons.CollectionChanged += OnNavigationMenuButtonCollectionChanged;
    }

    private async void OnNavigating(object? sender, ShellNavigatingEventArgs shellNavigatingEventArgs)
    {
        // Need a small delay to wait for Shell to set its CurrentPage to the one being navigated to
        await Task.Delay(10);
        var currentPage = Microsoft.Maui.Controls.Shell.Current.CurrentPage;
        if (m_floatingNavigationButtonConfigurator.PagesThatHidesButton.Contains(currentPage.GetType()))
            _ = Hide();
        else
            UnHide();
    }
    
    public void UnHide()
    {
        IsVisible = true;
        _ = this.FadeTo(1, easing: Easing.CubicOut);
    }

    public async Task Hide()
    {
        await this.FadeTo(0, easing: Easing.CubicIn);
        IsVisible = false;
    }

    private void OnTappedBackground()
    {
        _ = Close();
    }

    private void CreateAnimations()
    {
        m_fadeOutColorAnimation = new Animation(alpha => BackgroundColor = new Color(0, 0, 0, (int)alpha), 100, 0);
        m_fadeInColorAnimation = new Animation(alpha => BackgroundColor = new Color(0, 0, 0, (int)alpha), 0, 100);
    }

    private void AddMainButton()
    {
        m_mainButton = new FloatingActionButton.FloatingActionButton
        {
            HorizontalOptions = LayoutOptions.End,
            VerticalOptions = LayoutOptions.End,
            Icon = Icons.GetIcon(IconName.arrow_right_s_line),
            IconRotation = 270,
            ButtonBackgroundColor = Colors.GetColor(ColorName.color_obsolete_accent),
            Command = new Command(OnClickedMainButton)
        };
        DUIImageEffect.SetColor(m_mainButton, Colors.GetColor(ColorName.color_system_white));
        
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
        for (var i = 1; i <= m_floatingNavigationButtonConfigurator.NavigationMenuButtons.Count; i++)
        {
            AnimateExpand(i);
        }
    }

    private async Task Close()
    {
        InputTransparent = true;
        
        m_isExpanded = false;
        
        GestureRecognizers.RemoveAt(0);
        
        this.AbortAnimation("FadeIn");
        m_fadeOutColorAnimation.Commit(this, "FadeOut", easing: Easing.CubicIn);
        
        m_mainButton.RotateIconTo(270);
        for (var i = 1; i <= m_floatingNavigationButtonConfigurator.NavigationMenuButtons.Count; i++)
        {
            AnimateClose(i);
        }
        
        // Wait for NavigationMenuButtons to animate before removing
        await Task.Delay(250);
            
        foreach (var navigationMenuButton in m_floatingNavigationButtonConfigurator.NavigationMenuButtons)
        {
            m_contentGrid.Remove(navigationMenuButton);
        }
        
    }

    private void AnimateExpand(int index)
    {
        var navMenuButton = m_floatingNavigationButtonConfigurator.NavigationMenuButtons[index - 1];

        navMenuButton.FadeTo(1, easing: Easing.SpringOut);
        navMenuButton.TranslateTo(0, -MenuButtonsSpacing * index, easing: Easing.SpringOut);
    }
    
    private void AnimateClose(int index)
    {
        var navMenuButton = m_floatingNavigationButtonConfigurator.NavigationMenuButtons[index - 1];
        
        navMenuButton.FadeTo(0, easing: Easing.CubicIn);
        navMenuButton.TranslateTo(0, 0, easing: Easing.SpringIn);
    }


    /*private static void OnNavigationMenuButtonsChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is not FloatingNavigationButton floatingActionButtonMenu)
            return;
        
        if (oldValue is ObservableCollection<ExtendedFloatingActionButton.ExtendedFloatingActionButton> oldList)
        {
            oldList.CollectionChanged -= floatingActionButtonMenu.OnNavigationMenuButtonCollectionChanged;
            foreach (var item in oldList)
            {
                floatingActionButtonMenu.m_contentGrid.Remove(item);
            }
        }

        if (newValue is not ObservableCollection<ExtendedFloatingActionButton.ExtendedFloatingActionButton> newList)
            return;

        foreach (var item in newList)
        {
            floatingActionButtonMenu.m_contentGrid.Add(item);
        }
        
        newList.CollectionChanged += floatingActionButtonMenu.OnNavigationMenuButtonCollectionChanged;

    }*/

    private void OnNavigationMenuButtonCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        if (e.Action == NotifyCollectionChangedAction.Add)
        {
            foreach (var item in e.NewItems ?? Array.Empty<ExtendedFloatingActionButton.ExtendedFloatingActionButton>())
            {
                if(item is not ExtendedFloatingActionButton.ExtendedFloatingActionButton navigationMenuButton)
                    return;
                
                m_contentGrid.Add(navigationMenuButton);
                AnimateExpand(m_contentGrid.Count - 1);
            }
        }
        else if (e.Action == NotifyCollectionChangedAction.Remove)
        {
            foreach (var item in e.OldItems ?? Array.Empty<ExtendedFloatingActionButton.ExtendedFloatingActionButton>())
            {
                if (item is not ExtendedFloatingActionButton.ExtendedFloatingActionButton navigationMenuButton)
                    return;
                
                m_contentGrid.Remove(navigationMenuButton);
            }
        }
        
    }

}