using System.Collections.ObjectModel;
using System.Collections.Specialized;
using DIPS.Mobile.UI.Effects.DUIImageEffect;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;

namespace DIPS.Mobile.UI.Components.FloatingActionButton.FloatingActionButtonMenu;

/// Must be a <see cref="Grid"/> because CascadingInputTransparent does not work on <see cref="ContentView"/>
public partial class FloatingActionButtonMenu : Grid
{
    private readonly Grid m_contentGrid = new();

    private const int MenuButtonsSpacing = 75;

    private bool m_isExpanded;
    
#nullable disable
    private ImageButton m_mainButton;
    private Animation m_fadeOutColorAnimation;
    private Animation m_fadeInColorAnimation;
#nullable restore

    public FloatingActionButtonMenu()
    {
        Add(m_contentGrid);
        Padding = new Thickness(0, 0, Sizes.GetSize(SizeName.size_3), Sizes.GetSize(SizeName.size_13));
        
        m_contentGrid.RowDefinitions = new RowDefinitionCollection { new() { Height = GridLength.Star } };
        m_contentGrid.ColumnDefinitions = new ColumnDefinitionCollection { new() { Width = GridLength.Auto } };

        m_contentGrid.HorizontalOptions = LayoutOptions.End;

        CascadeInputTransparent = false;
        InputTransparent = true;

        AddMainButton();
        CreateAnimations();
        
        GestureRecognizers.Add(new TapGestureRecognizer
        {
            Command = new Command(OnTappedBackground)
        });

    }
    protected override void OnHandlerChanged()
    {
        base.OnHandlerChanged();
        
        Microsoft.Maui.Controls.Shell.Current.Navigating += OnNavigating;

        foreach (var navMenuButton in NavigationMenuButtons)
        {
            m_contentGrid.Add(navMenuButton);
        }
    }

    private async void OnNavigating(object? sender, ShellNavigatingEventArgs shellNavigatingEventArgs)
    {
        // Need a small delay to wait for Shell to set its CurrentPage to the one being navigated to
        await Task.Delay(10);
        var currentPage = Microsoft.Maui.Controls.Shell.Current.CurrentPage;
        if (PagesNotContaining.Contains(currentPage.GetType()))
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
        m_mainButton = new ImageButton()
        {
            InputTransparent = false,
            CornerRadius = 30,
            WidthRequest = Sizes.GetSize(SizeName.size_15),
            HeightRequest = Sizes.GetSize(SizeName.size_15),
            HorizontalOptions = LayoutOptions.End,
            VerticalOptions = LayoutOptions.End,
            Source = Icons.GetIcon(IconName.arrow_right_s_line),
            BorderColor = Colors.GetColor(ColorName.color_system_white),
            BorderWidth = 3,
            Padding = Sizes.GetSize(SizeName.size_3),
            Rotation = 270,
            BackgroundColor = Colors.GetColor(ColorName.color_obsolete_accent),
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
            foreach (var navigationMenuButton in NavigationMenuButtons)
            {
                m_contentGrid.Add(navigationMenuButton);
            }

            Expand();
        }
    }

    private void Expand()
    {
        InputTransparent = false;
        
        m_isExpanded = true;
        
        this.AbortAnimation("FadeOut");
        m_fadeInColorAnimation.Commit(this, "FadeIn", easing: Easing.CubicOut);
        
        m_mainButton.RotateTo(90, easing: Easing.CubicOut);
        for (var i = 1; i <= NavigationMenuButtons.Count; i++)
        {
            AnimateExpand(i);
        }
    }

    private async Task Close()
    {
        InputTransparent = true;
        
        m_isExpanded = false;
        
        this.AbortAnimation("FadeIn");
        m_fadeOutColorAnimation.Commit(this, "FadeOut", easing: Easing.CubicIn);
        
        _ = m_mainButton.RotateTo(270, easing: Easing.CubicIn);
        for (var i = 1; i <= NavigationMenuButtons.Count; i++)
        {
            AnimateClose(i);
        }
        
        // Wait for NavigationMenuButtons to animate
        await Task.Delay(250);
            
        foreach (var navigationMenuButton in NavigationMenuButtons)
        {
            m_contentGrid.Remove(navigationMenuButton);
        }
        
    }

    private void AnimateExpand(int index)
    {
        var navMenuButton = NavigationMenuButtons[index - 1];

        navMenuButton.FadeTo(1, easing: Easing.SpringOut);
        navMenuButton.TranslateTo(0, -MenuButtonsSpacing * index, easing: Easing.SpringOut);
    }
    
    private void AnimateClose(int index)
    {
        var navMenuButton = NavigationMenuButtons[index - 1];
        navMenuButton.FadeTo(0, easing: Easing.CubicIn);
        navMenuButton.TranslateTo(0, 0, easing: Easing.SpringIn);
    }


    private static void OnNavigationMenuButtonsChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is not FloatingActionButtonMenu floatingActionButtonMenu)
            return;
        
        if (oldValue is ObservableCollection<NavigationMenuButton.NavigationMenuButton> oldList)
        {
            oldList.CollectionChanged -= floatingActionButtonMenu.OnNavigationMenuButtonCollectionChanged;
            foreach (var item in oldList)
            {
                floatingActionButtonMenu.m_contentGrid.Remove(item);
            }
        }

        if (newValue is not ObservableCollection<NavigationMenuButton.NavigationMenuButton> newList)
            return;

        foreach (var item in newList)
        {
            floatingActionButtonMenu.m_contentGrid.Add(item);
        }
        
        newList.CollectionChanged += floatingActionButtonMenu.OnNavigationMenuButtonCollectionChanged;

    }

    private void OnNavigationMenuButtonCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        if (e.Action == NotifyCollectionChangedAction.Add)
        {
            foreach (var item in e.NewItems ?? Array.Empty<NavigationMenuButton.NavigationMenuButton>())
            {
                if(item is not NavigationMenuButton.NavigationMenuButton navigationMenuButton)
                    return;
                
                m_contentGrid.Add(navigationMenuButton);
                AnimateExpand(m_contentGrid.Count - 1);
            }
        }
        else if (e.Action == NotifyCollectionChangedAction.Remove)
        {
            foreach (var item in e.OldItems ?? Array.Empty<NavigationMenuButton.NavigationMenuButton>())
            {
                if (item is not NavigationMenuButton.NavigationMenuButton navigationMenuButton)
                    return;
                
                m_contentGrid.Remove(navigationMenuButton);
            }
        }
        
    }

}