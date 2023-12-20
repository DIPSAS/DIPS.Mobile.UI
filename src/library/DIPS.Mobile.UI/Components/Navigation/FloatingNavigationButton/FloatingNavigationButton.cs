using DIPS.Mobile.UI.API.Accessibility;
using DIPS.Mobile.UI.API.Animations;
using DIPS.Mobile.UI.API.Library;
using DIPS.Mobile.UI.Components.CheckBoxes;
using DIPS.Mobile.UI.Effects.Touch;
using DIPS.Mobile.UI.Resources.Animations;
using DIPS.Mobile.UI.Resources.LocalizedStrings.LocalizedStrings;
using Microsoft.Maui.Controls.Shapes;
using SkiaSharp.Extended.UI.Controls;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;

namespace DIPS.Mobile.UI.Components.Navigation.FloatingNavigationButton;

/// Must be a <see cref="Grid"/> because CascadingInputTransparent does not work on <see cref="ContentView"/>
internal class FloatingNavigationButton : Grid
{
    private Color OpenedColor = Colors.GetColor(ColorName.color_obsolete_accent);
    private Color ClosedColor = Color.FromArgb("#BF8DCE");
    private ImageSource OpenedIcon = Icons.GetIcon(IconName.close_line);
    private ImageSource ClosedIcon = Icons.GetIcon(IconName.menu_line);
    private readonly FloatingNavigationButtonConfigurator m_floatingNavigationButtonConfigurator;
    private readonly Grid m_contentGrid = new();

    private const int MenuButtonsSpacing = 75;

    private bool m_isOpened;

#nullable disable
    internal NavigationMenuButton.NavigationMenuButton m_mainButton;
    private Animation m_fadeOutColorAnimation;
    private Animation m_fadeInColorAnimation;
#nullable restore
    
    public FloatingNavigationButton(FloatingNavigationButtonConfigurator floatingNavigationButtonConfigurator)
    {
        m_floatingNavigationButtonConfigurator = floatingNavigationButtonConfigurator;
        
        Add(m_contentGrid);

        Padding = new Thickness(0, 0, Sizes.GetSize(SizeName.size_3), Sizes.GetSize(SizeName.size_13));

        m_contentGrid.RowDefinitions = new RowDefinitionCollection {new() {Height = GridLength.Star}};
        m_contentGrid.ColumnDefinitions = new ColumnDefinitionCollection {new() {Width = GridLength.Auto}};
        m_contentGrid.HorizontalOptions = LayoutOptions.End;
        m_contentGrid.InputTransparent =
            true; //Do not remove, this has to be input transparent so people can tap the right side of the screen.
        m_contentGrid.CascadeInputTransparent =
            false; //Do not remove, this will make sure that each button from the FAB is tappable

        AddMainButton();
        CreateAnimations();

#if __IOS__ //Somehow this is needed on iOS when the view draws to make it input transparent, the handler code was not enough.
        InputTransparent = true;
        CascadeInputTransparent = false;
#endif

        DeviceDisplay.MainDisplayInfoChanged += OnOrientationChanged;
    }

    private void OnOrientationChanged(object? sender, DisplayInfoChangedEventArgs e)
    {
        WidthRequest = e.DisplayInfo.Width / DeviceDisplay.MainDisplayInfo.Density;
        HeightRequest = e.DisplayInfo.Height / DeviceDisplay.MainDisplayInfo.Density;
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
        if (Opacity == 0)
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

    private void CreateAnimations()
    {
        m_fadeOutColorAnimation = new Animation(alpha => BackgroundColor = new Color(0, 0, 0, (int)alpha), 100, 0);
        m_fadeInColorAnimation = new Animation(alpha => BackgroundColor = new Color(0, 0, 0, (int)alpha), 0, 100);
    }

    private void AddMainButton()
    {
        m_mainButton = new NavigationMenuButton.NavigationMenuButton
        {
            HorizontalOptions = LayoutOptions.End,
            VerticalOptions = LayoutOptions.End,
            Icon = ClosedIcon,
            ButtonBackgroundColor = Colors.GetColor(ColorName.color_obsolete_accent),
            Command = new Command(OnClickedMainButton),
        };
        
        //Accessibility
        SemanticProperties.SetDescription(m_mainButton.Button,
            SemanticDescription.GetDescription(
                DUILocalizedStrings.Accessability_FloatingNavigationButton_Description, ControlType.Button));
        //Removes the voice over focus on the entire tappable grid
        AutomationProperties.SetIsInAccessibleTree(this, false);
        AutomationProperties.SetIsInAccessibleTree(m_contentGrid, false);

        m_contentGrid.Add(m_mainButton);
    }

    /// <summary>
    /// Keep this code:
    /// Not to be used yet, due to complexity. But defensively possible in the future.
    /// </summary>
    /// <returns></returns>
    public View CreateAnimateableButton()
    {
        DUI.EnsureSkLottieResourcesAdded();
        
        var closedColor = Color.FromArgb("#BF8DCE");
        var button = new Border()
        {
            HeightRequest = Sizes.GetSize(SizeName.size_15),
            WidthRequest = Sizes.GetSize(SizeName.size_15),
            Padding = Sizes.GetSize(SizeName.size_3),
            Stroke = Colors.GetColor(ColorName.color_system_white),
            StrokeThickness = 3,
            StrokeShape = new Ellipse(),
            BackgroundColor = closedColor,
        };

        var menuAnimation = new SKLottieView()
        {
            // Source = Animations.GetAnimation(AnimationName.menu),
            IsAnimationEnabled = false, //Do not animate when rendered
            RepeatCount = -1, //Runs for ever
            RepeatMode = SKLottieRepeatMode.Reverse //Make sure it will animate back
        };

        button.Content = menuAnimation;
        Touch.SetCommand(button, new Command(async () =>
        {
            // if (m_menuAnimationDuration == 0) //This can be used to wait for the animation to finish, Task.Delay(this value)
            // {
            //     m_menuAnimationDuration = (uint)menuAnimation.Duration.TotalMilliseconds;
            // }
            if (menuAnimation.IsAnimationEnabled)
            {
                return; //Do not allow spamming
            }
            OnClickedMainButton();
            menuAnimation.IsAnimationEnabled = true; //Start the animation
            await Task.Delay((int)menuAnimation.Duration.TotalMilliseconds); //Wait for it to finish
            menuAnimation.IsAnimationEnabled = false; //Stop it
        }));
        return button;
    }

    private void OnClickedMainButton()
    {
        if (m_isOpened)
        {
            _ = Close();
        }
        else
        {
            foreach (var extendedNavigationMenuButton in m_floatingNavigationButtonConfigurator.NavigationMenuButtons)
            {
                if (m_contentGrid.Contains(extendedNavigationMenuButton))
                    continue;
                m_contentGrid.Add(extendedNavigationMenuButton);
                extendedNavigationMenuButton.Opacity = 0;
                extendedNavigationMenuButton.VerticalOptions = LayoutOptions.End;
                extendedNavigationMenuButton.HorizontalOptions = LayoutOptions.End;
            }

            Open();
        }
    }

    private bool IsClosing()
    {
        if (m_contentGrid.Any(c => c is ExtendedNavigationMenuButton.ExtendedNavigationMenuButton))
        {
            return true;
        }

        return false;
    }

    private async void Open()
    {
        IsClickable = true; //Wait until animation is finished before letting people
        m_isOpened = true;

        this.AbortAnimation("FadeOut");
        m_fadeInColorAnimation.Commit(this, "FadeIn", easing: Easing.CubicOut);

        m_mainButton.HideBadge();
        m_mainButton.Button.BackgroundColorTo(OpenedColor, length: 1500);
        m_mainButton.Icon = OpenedIcon;

        var numberOfNavigationButtons = m_floatingNavigationButtonConfigurator.NavigationMenuButtons.Count;
        for (var i = 1; i <= numberOfNavigationButtons; i++)
        {
            AnimateNavigationButtons(i);
        }

        //Accessibility
        var firstNavButton = m_floatingNavigationButtonConfigurator.NavigationMenuButtons.FirstOrDefault();
        firstNavButton?.SetSemanticFocus();
    }


    public async Task Close()
    {
        if (!m_isOpened)
            return;

        IsClickable = false;

        m_isOpened = false;

        this.AbortAnimation("FadeIn");
        m_fadeOutColorAnimation.Commit(this, "FadeOut", easing: Easing.CubicIn);

        for (var i = 1; i <= m_floatingNavigationButtonConfigurator.NavigationMenuButtons.Count; i++)
        {
            AnimateClose(i);
        }

        _ = m_mainButton.Button.BackgroundColorTo(ClosedColor, length: 1500);
        m_mainButton.Icon = ClosedIcon;
        // Wait for NavigationMenuButtons to animate before removing
        await Task.Delay(250);

        if (m_mainButton.BadgeCount != 0)
            m_mainButton.ShowBadge();
    }

    private void AnimateNavigationButtons(int index)
    {
        var navMenuButton = m_floatingNavigationButtonConfigurator.NavigationMenuButtons[index - 1];

        navMenuButton.Scale = 0.5;
        navMenuButton.FadeTo(1, easing: Easing.CubicOut);
        navMenuButton.TranslateTo(0, -MenuButtonsSpacing * index, easing: Easing.CubicOut);
        navMenuButton.ScaleTo(1, easing: Easing.CubicOut);
    }

    private async void AnimateClose(int index)
    {
        var navMenuButton = m_floatingNavigationButtonConfigurator.NavigationMenuButtons[index - 1];

        _ = navMenuButton.FadeTo(0, easing: Easing.CubicIn);
        await navMenuButton.TranslateTo(0, 0, easing: Easing.CubicIn);
        m_contentGrid.Remove(navMenuButton);
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
        var totalBadgeCount =
            m_floatingNavigationButtonConfigurator.NavigationMenuButtons.Sum(navBtn => navBtn.BadgeCount);
        m_mainButton.BadgeCount = totalBadgeCount;
    }

    public void SetNavigationMenuBadgeCount(string identifier, int value)
    {
        var navBtn = GetButtonFromIdentifier(identifier);

        if (navBtn is null)
            return;

        navBtn.BadgeCount = value;
        UpdateBadgeCount();
    }

    public void ChangeNavigationMenuButtonBadgeColor(string identifier, Color color)
    {
        var navBtn = GetButtonFromIdentifier(identifier);

        if (navBtn is null)
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
            AnimateNavigationButtons(i);
        }
    }

    public void AddNavigationMenuButton(ExtendedNavigationMenuButton.ExtendedNavigationMenuButton navigationMenuButton,
        int? index)
    {
        var insertIndex = m_floatingNavigationButtonConfigurator.NavigationMenuButtons.Count;
        if (index != null)
        {
            insertIndex = (int)index;
        }
        else if (m_floatingNavigationButtonConfigurator.NavigationMenuButtons.FirstOrDefault(b => b.IsLast) != null)
        {
            insertIndex = m_floatingNavigationButtonConfigurator.NavigationMenuButtons.Count - 1;
        }

        m_floatingNavigationButtonConfigurator.NavigationMenuButtons.Insert(insertIndex, navigationMenuButton);

        if (!m_isOpened)
            return;

        m_contentGrid.Insert(insertIndex, navigationMenuButton);

        navigationMenuButton.Opacity = 0;
        navigationMenuButton.VerticalOptions = LayoutOptions.End;
        navigationMenuButton.HorizontalOptions = LayoutOptions.End;
        for (var i = 1; i <= m_floatingNavigationButtonConfigurator.NavigationMenuButtons.Count; i++)
        {
            AnimateNavigationButtons(i);
        }
    }

    public bool ContainsNavigationMenuButton(string identifier)
    {
        return GetButtonFromIdentifier(identifier) is not null;
    }

    public void SetButtonAvailability(string identifier, bool isEnabled)
    {
        var navBtn = GetButtonFromIdentifier(identifier);

        if (navBtn is null)
            return;

        navBtn.IsEnabled = isEnabled;
    }

    public bool CheckButtonAvailability(string identifier)
    {
        var navBtn = GetButtonFromIdentifier(identifier);

        return navBtn is not null && navBtn.IsEnabled;
    }

    private ExtendedNavigationMenuButton.ExtendedNavigationMenuButton? GetButtonFromIdentifier(string identifier) =>
        m_floatingNavigationButtonConfigurator.NavigationMenuButtons.FirstOrDefault(navButton =>
            navButton.AutomationId.Equals(identifier));

    public static readonly BindableProperty IsClickableProperty = BindableProperty.Create(
        nameof(IsClickable),
        typeof(bool),
        typeof(FloatingNavigationButton), defaultValue: false);
    
    public bool IsClickable
    {
        get => (bool)GetValue(IsClickableProperty);
        set => SetValue(IsClickableProperty, value);
    }

    protected override void OnHandlerChanging(HandlerChangingEventArgs args)
    {
        base.OnHandlerChanging(args);

        if (args.NewHandler is null)
            return;
        
        DeviceDisplay.MainDisplayInfoChanged += OnOrientationChanged;
    }
}