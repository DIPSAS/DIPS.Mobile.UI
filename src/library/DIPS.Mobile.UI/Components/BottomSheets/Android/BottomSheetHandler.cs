using System.Windows.Input;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.Graphics.Drawable;
using Google.Android.Material.AppBar;
using Google.Android.Material.BottomSheet;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;
using AndroidResource = Android.Resource;
using Application = Android.App.Application;
using Grid = Microsoft.Maui.Controls.Grid;
using AView = Android.Views.View;
using Color = Microsoft.Maui.Graphics.Color;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;
using Object = Java.Lang.Object;
using RelativeLayout = Android.Widget.RelativeLayout;
using ADrawableCompat = AndroidX.Core.Graphics.Drawable.DrawableCompat;
using Paint = Android.Graphics.Paint;
using System.ComponentModel;
using DIPS.Mobile.UI.API.Library;
using DIPS.Mobile.UI.Components.BottomSheets.Header;
using DIPS.Mobile.UI.Resources.Icons;
using DIPS.Mobile.UI.Resources.LocalizedStrings.LocalizedStrings;
using SearchBar = DIPS.Mobile.UI.Components.Searching.SearchBar;
using View = Microsoft.Maui.Controls.View;

namespace DIPS.Mobile.UI.Components.BottomSheets;

public partial class BottomSheetHandler : ContentViewHandler
{
#nullable disable
    internal BottomSheet m_bottomSheet;
#nullable enable
    
    internal RelativeLayout m_linearLayout;
    internal AView? m_bottomBar;
    
    private static AView? s_mEmptyNonFitToContentView;
    private AView? m_searchBarView;
    private MaterialToolbar? m_headerToolbar;
    private const int CloseMenuItemId = 1;
    private List<WeakReference<SearchBar>> m_weakSearchBars = [];
    private WeakReference<AView>? m_weakCurrentFocusedSearchBar;
    private BottomSheetCallback? m_bottomSheetCallback;
    private KeyListener? m_keyListener;
    private LinearLayout? m_bottomSheetLayout;

    public AView OnBeforeOpening(IMauiContext mauiContext, Context context, AView bottomSheetAndroidView,
        RelativeLayout rootLayout, LinearLayout bottomSheetLayout)
    {
        if (VirtualView is not BottomSheet bottomSheet) return new AView(Context);
        
        m_bottomSheet = bottomSheet;
        m_bottomSheetCallback = new BottomSheetCallback(this);
        m_keyListener = new KeyListener(this);
        bottomSheet.BottomSheetDialog.Behavior.AddBottomSheetCallback(m_bottomSheetCallback);
        bottomSheet.BottomSheetDialog.SetOnShowListener(new DialogInterfaceOnShowListener(this));
        bottomSheet.BottomSheetDialog.SetOnKeyListener(m_keyListener);

        // On modern Android, BottomSheetDialog (ComponentDialog) uses OnBackPressedDispatcher
        // which bypasses SetOnKeyListener. Register a callback so back during pushed navigation pops instead of dismissing.
        m_navigationBackCallback = new NavigationBackPressedCallback(this);
        bottomSheet.BottomSheetDialog.OnBackPressedDispatcher.AddCallback(m_navigationBackCallback);

        //Add a handle, with a innerGrid that works as a big hit box for the user to hit
        //Inspired by com.google.android.material.bottomsheet.BottomSheetDragHandleView , which will be added in Xamarin Android Material Design v1.7.0.  https://github.com/material-components/material-components-android/commit/ac7b761294808748df167b50b223b591ca9dac06
        if (m_bottomSheet.BottomSheetBehavior.Draggable)
        {
            var innerGrid = new Grid
            {
                Padding = new Thickness(0, Sizes.GetSize(SizeName.content_margin_small), 0, 0), 
                BackgroundColor = Colors.GetColor(ColorName.color_surface_default)
            };
            
            innerGrid.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(ToggleBottomSheetIfPossible)
            });
            
            var handle = new BoxView
            {
                HeightRequest = 4,
                WidthRequest = 32,
                CornerRadius = 10,
                BackgroundColor = Colors.GetColor(ColorName.color_border_subtle),
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center
            };
            
            innerGrid.Add(handle);
            var androidInnerGridView = innerGrid.ToPlatform(mauiContext);
            
            androidInnerGridView.ContentDescription = null; //Makes sure screen readers do not read anything here.
            androidInnerGridView.ImportantForAccessibility = ImportantForAccessibility.No;
            
            bottomSheetLayout.AddView(androidInnerGridView);
        }

        m_bottomSheetLayout = bottomSheetLayout;
        
        m_headerToolbar = CreateHeaderToolbar(context);
        bottomSheetLayout.AddView(m_headerToolbar);
        // Apply state after the toolbar is attached so menu items render correctly on first open.
        UpdateHeaderToolbarFromBottomSheet();
        
        m_searchBarView = m_bottomSheet.SearchBar.ToPlatform(mauiContext);
        bottomSheetLayout.AddView(m_searchBarView);
        ToggleSearchBar();
        FindAndSetupSearchBars();

        // Set up navigation container for push/pop of content
        SetupNavigationContainer(context, bottomSheetAndroidView, bottomSheetLayout);

        if (m_bottomSheet.Positioning is not Positioning.Fit)
        {
            var width = Platform.AppContext.Resources?.DisplayMetrics?.WidthPixels;
            if (width.HasValue)
            {
                // Add an empty view, where its dimensions is set to always match the parent so that the LinearLayout will always take up available space
                s_mEmptyNonFitToContentView = new AView(Application.Context)
                {
                    LayoutParameters = new ViewGroup.LayoutParams(width.Value,
                        ViewGroup.LayoutParams.MatchParent)
                };
                rootLayout.AddView(s_mEmptyNonFitToContentView);
            }
        }

        if (!m_bottomSheet.HasBottomBarButtons)
            return m_linearLayout = rootLayout;

        m_bottomBar = m_bottomSheet.CreateBottomBar().ToPlatform(MauiContext!);
        m_bottomBar.LayoutParameters =
            new ViewGroup.LayoutParams(ViewGroup.LayoutParams.MatchParent, (int)Context.ToPixels(BottomSheet.BottomBarHeight));
            
        rootLayout.AddView(m_bottomBar);
        
        return m_linearLayout = rootLayout;
    }

    private void FindAndSetupSearchBars()
    {
        m_weakSearchBars = m_bottomSheet.FindAllChildrenOfType<SearchBar>().Select(sb => new WeakReference<SearchBar>(sb)).ToList();

        foreach (var weakSearchBar in m_weakSearchBars)
        {
            if (!weakSearchBar.TryGetTarget(out var searchBar))
                continue;

            searchBar.Focused += SearchBarOnFocused;
            searchBar.Unfocused += SearchBarOnUnfocused;
        }
        
        // Also, setup the internal search bar in BottomSheet
        if (m_bottomSheet.SearchBar is { } searchBarInternal)
        {
            searchBarInternal.Focused += SearchBarOnFocused;
            searchBarInternal.Unfocused += SearchBarOnUnfocused;
        }
        
    }


    private void ToggleSearchBar()
    {
        if (m_searchBarView == null) 
            return;
        
        m_searchBarView.Visibility = m_bottomSheet.HasSearchBar ? ViewStates.Visible : ViewStates.Gone;
    }

    private void SearchBarOnUnfocused(object? sender, EventArgs e)
    {
        if (m_weakCurrentFocusedSearchBar is not null)
            m_bottomSheet.Positioning = Positioning.Medium;

        m_weakCurrentFocusedSearchBar = null;
    }

    private void SearchBarOnFocused(object? sender, EventArgs e)
    {
        if(m_bottomSheet.Positioning is Positioning.Large)
            return;
        
        m_weakCurrentFocusedSearchBar = new WeakReference<AView>(((sender as View)!).ToPlatform(DUI.GetCurrentMauiContext!));
        m_bottomSheet.Positioning = Positioning.Large;
    }

    private void ToggleFitToContent(BottomSheet bottomSheet)
    {
        if (s_mEmptyNonFitToContentView != null) //Only add there is something in the bottom sheet
        {
            if (bottomSheet.Positioning is not Positioning.Fit)
            {
                s_mEmptyNonFitToContentView.Visibility = ViewStates.Visible;
            }
            else
            {
                s_mEmptyNonFitToContentView.Visibility = ViewStates.Gone;
            }
        }
    }

    private static partial void MapPositioning(BottomSheetHandler handler, BottomSheet bottomSheet)
    {
        //Reset fit to content stuff in case it changes
        bottomSheet.BottomSheetDialog.Behavior.FitToContents =
            (bottomSheet.Positioning) == Positioning.Fit; 
        handler.ToggleFitToContent(bottomSheet);

        if (bottomSheet.Positioning == Positioning.Large)
        {
            bottomSheet.BottomSheetDialog.Behavior.State = BottomSheetBehavior.StateExpanded;
        }
        else if (bottomSheet.Positioning == Positioning.Medium)
        {
            bottomSheet.BottomSheetDialog.Behavior.State = BottomSheetBehavior.StateHalfExpanded;
            if (handler.m_weakCurrentFocusedSearchBar?.TryGetTarget(out var searchBarNativeView) ?? false)
            {
                searchBarNativeView.ClearFocus();
            }
        }
    }

    private void ToggleBottomSheetIfPossible()
    {
        var bottomSheetBehavior = m_bottomSheet.BottomSheetDialog.Behavior;
        var collapsed = m_bottomSheet.BottomSheetDialog.Behavior.State == BottomSheetBehavior.StateCollapsed;
        bottomSheetBehavior.State =
            collapsed ? BottomSheetBehavior.StateExpanded : BottomSheetBehavior.StateCollapsed;
    }

    public static partial void MapHasSearchBar(BottomSheetHandler handler, BottomSheet bottomSheet)
    {
        handler.ToggleSearchBar();
    }

    public static partial void MapIsInteractiveCloseable(BottomSheetHandler handler, BottomSheet bottomSheet)
    {
        bottomSheet.BottomSheetDialog.SetCancelable(bottomSheet.IsInteractiveCloseable);
        bottomSheet.BottomSheetDialog.SetCanceledOnTouchOutside(bottomSheet.IsInteractiveCloseable);
    }
    
    private static partial void MapIsDraggable(BottomSheetHandler handler, BottomSheet bottomSheet)
    {
        bottomSheet.BottomSheetDialog.Behavior.Draggable = bottomSheet.IsDraggable;
    }


    private MaterialToolbar CreateHeaderToolbar(Context context)
    {
        var toolbar = new MaterialToolbar(context)
        {
            LayoutParameters = new LinearLayout.LayoutParams(
                ViewGroup.LayoutParams.MatchParent,
                ViewGroup.LayoutParams.WrapContent)
        };
        toolbar.SetBackgroundColor(Colors.GetColor(ColorName.color_surface_default).ToPlatform());
        toolbar.SetTitleTextColor(Colors.GetColor(ColorName.color_text_default).ToPlatform());
        // Ensure standard action-bar height even before children are laid out
        toolbar.SetMinimumHeight((int)context.ToPixels(56));

        toolbar.NavigationClick += OnHeaderToolbarNavigationClick;
        toolbar.MenuItemClick += OnHeaderToolbarMenuItemClick;

        // Listen for changes that affect the header
        m_bottomSheet.PropertyChanged += OnBottomSheetPropertyChanged;
        if (m_bottomSheet.BottomSheetHeaderBehavior is { } behavior)
        {
            behavior.PropertyChanged += OnHeaderBehaviorPropertyChanged;
        }

        return toolbar;
    }

    private void OnBottomSheetPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(BottomSheet.Title) && m_nativeNavigationStack.Count == 0)
        {
            UpdateHeaderToolbarFromBottomSheet();
        }
    }

    private void OnHeaderBehaviorPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName is nameof(BottomSheetHeaderBehavior.IsBackButtonVisible)
            or nameof(BottomSheetHeaderBehavior.IsCloseButtonVisible)
            or nameof(BottomSheetHeaderBehavior.IsVisible)
            or nameof(BottomSheetHeaderBehavior.TitleAndBackButtonContainerCommand)
            or nameof(BottomSheetHeaderBehavior.CloseButtonCommand))
        {
            // Re-apply for the current navigation level; close visibility applies in both
            if (m_nativeNavigationStack.Count == 0)
            {
                UpdateHeaderToolbarFromBottomSheet();
            }
            else
            {
                var currentEntry = m_bottomSheet.NavigationStack.Peek();
                UpdateHeaderToolbarForNavigation(currentEntry.Page.Title);
            }
        }
    }

    private void UpdateHeaderToolbarFromBottomSheet()
    {
        ApplyHeaderToolbarState(m_bottomSheet.Title, useBehaviorBackButton: true);
    }

    private void UpdateHeaderToolbarForNavigation(string? title)
    {
        ApplyHeaderToolbarState(title, useBehaviorBackButton: false);
    }

    private void ApplyHeaderToolbarState(string? title, bool useBehaviorBackButton)
    {
        if (m_headerToolbar is null) return;

        var behavior = m_bottomSheet.BottomSheetHeaderBehavior;

        // Visibility (only honored at root; pushed navigation always shows the toolbar)
        var isVisible = useBehaviorBackButton ? (behavior?.IsVisible ?? true) : true;
        m_headerToolbar.Visibility = isVisible ? ViewStates.Visible : ViewStates.Gone;

        // Title
        m_headerToolbar.Title = title ?? string.Empty;

        // Back button
        var showBack = useBehaviorBackButton
            ? (behavior?.IsBackButtonVisible ?? false)
            : true;
        m_headerToolbar.NavigationIcon = showBack ? GetNavigationIcon() : null;
        if (showBack)
        {
            m_headerToolbar.NavigationContentDescription = DUILocalizedStrings.Back;
        }

        // Close button (right) — controlled by behavior; shown both at root and during pushed navigation
        m_headerToolbar.Menu?.Clear();
        var showClose = behavior?.IsCloseButtonVisible ?? true;
        if (showClose)
        {
            var menuItem = m_headerToolbar.Menu?.Add(global::Android.Views.IMenu.None, CloseMenuItemId, global::Android.Views.IMenu.None, DUILocalizedStrings.Close);
            if (Icons.TryGetDrawable(IconName.close_line, out var icon) && icon is not null)
            {
                ADrawableCompat.SetTint(icon, Colors.GetColor(ColorName.color_icon_subtle).ToPlatform());
                menuItem?.SetIcon(BuildCircularBackgroundIcon(icon));
            }
            menuItem?.SetShowAsAction(ShowAsAction.Always);
        }
    }

    private Drawable BuildCircularBackgroundIcon(Drawable icon)
    {
        // Mimic the original `ButtonStyle.CloseIconSmall`: a circular `color_fill_neutral` background with the icon centered.
        var background = new global::Android.Graphics.Drawables.GradientDrawable();
        background.SetShape(global::Android.Graphics.Drawables.ShapeType.Oval);
        background.SetColor(Colors.GetColor(ColorName.color_fill_neutral).ToPlatform());

        var layered = new global::Android.Graphics.Drawables.LayerDrawable([background, icon]);
        // Inset the icon so the circle is visible around it
        var inset = (int)Context.ToPixels(8);
        layered.SetLayerInset(1, inset, inset, inset, inset);
        return layered;
    }

    private Drawable GetNavigationIcon()
    {
        var iconColor = Colors.GetColor(ColorName.color_icon_default).ToPlatform();
        var drawable = new DrawerArrowDrawable(Context)
        {
            Progress = 1.0f // Fully arrow
        };
        ADrawableCompat.SetTint(drawable, iconColor);
        return drawable;
    }

    private void OnHeaderToolbarNavigationClick(object? sender, global::AndroidX.AppCompat.Widget.Toolbar.NavigationClickEventArgs e)
    {
        // If we are in pushed navigation, pop. Otherwise delegate to the back-button command.
        if (TryHandleNavigationBack())
            return;

        if (m_bottomSheet.BottomSheetHeaderBehavior?.IsTitleAndBackButtonContainerEnabled != true)
            return;
        
        m_bottomSheet.BottomSheetHeaderBehavior?.TitleAndBackButtonContainerCommand?.Execute(null);
    }

    private void OnHeaderToolbarMenuItemClick(object? sender, global::AndroidX.AppCompat.Widget.Toolbar.MenuItemClickEventArgs e)
    {
        if (e.Item?.ItemId != CloseMenuItemId) return;

        var behavior = m_bottomSheet.BottomSheetHeaderBehavior;
        if (behavior?.CloseButtonCommand is not null)
        {
            behavior.CloseButtonCommand.Execute((Action)(() => m_bottomSheet.Close()));
        }
        else
        {
            m_bottomSheet.Close();
        }
    }

    private void CleanupHeaderToolbar()
    {
        m_bottomSheet.PropertyChanged -= OnBottomSheetPropertyChanged;
        if (m_bottomSheet.BottomSheetHeaderBehavior is { } behavior)
        {
            behavior.PropertyChanged -= OnHeaderBehaviorPropertyChanged;
        }

        if (m_headerToolbar is null) return;

        m_headerToolbar.NavigationClick -= OnHeaderToolbarNavigationClick;
        m_headerToolbar.MenuItemClick -= OnHeaderToolbarMenuItemClick;
        m_bottomSheetLayout?.RemoveView(m_headerToolbar);
        m_headerToolbar.Dispose();
        m_headerToolbar = null;
    }

    protected override void DisconnectHandler(ContentViewGroup platformView)
    {
        base.DisconnectHandler(platformView);
        
        // Clean up navigation stack
        CleanupNavigationStack();
        
        if (m_bottomSheetCallback is not null)
        {
            m_bottomSheet.BottomSheetDialog.Behavior.RemoveBottomSheetCallback(m_bottomSheetCallback);
            m_bottomSheetCallback = null;
        }

        m_bottomSheet.BottomSheetDialog.SetOnShowListener(null);
        m_bottomSheet.BottomSheetDialog.SetOnKeyListener(null);
        m_keyListener = null;

        if (m_navigationBackCallback is not null)
        {
            m_navigationBackCallback.Remove();
            m_navigationBackCallback.Dispose();
            m_navigationBackCallback = null;
        }

        s_mEmptyNonFitToContentView?.RemoveFromParent();
        CleanupHeaderToolbar();

        foreach (var weakSearchBar in m_weakSearchBars)
        {
            if (!weakSearchBar.TryGetTarget(out var searchBar))
                continue;

            searchBar.Focused -= SearchBarOnFocused;
            searchBar.Unfocused -= SearchBarOnUnfocused;
        }
        
        // Also, dispose the internal search bar in BottomSheet
        if (m_bottomSheet.SearchBar is { } searchBarInternal)
        {
            searchBarInternal.Focused -= SearchBarOnFocused;
            searchBarInternal.Unfocused -= SearchBarOnUnfocused;
        }
    }
    
    /// <summary>
    /// 1.0 = full screen
    /// 0.0 = medium screen
    /// -1.0 = closed
    /// </summary>
    private void OnSlide(float slideOffset, AView bottomSheet)
    {
        if (m_bottomSheet.HasBottomBarButtons)
        {
            if(slideOffset < 0)
                return;
            
            SetBottomBarTranslation(bottomSheet);
        }
    }

    internal void SetBottomBarTranslation(AView view)
    {
        var bottomSheetVisibleHeight = view.Height - view.Top;
                
        if(m_bottomBar is not null)
            m_bottomBar.TranslationY = bottomSheetVisibleHeight - m_bottomBar.Height;
    }
    
    internal bool OnKey(IDialogInterface? dialog, Keycode keyCode, KeyEvent? keyEvent)
    {
        if (keyCode != Keycode.Back || keyEvent?.Action == KeyEventActions.Down)
        {
            return false;
        }

        // If there is navigation content to pop, do that instead of closing the sheet
        if (TryHandleNavigationBack())
        {
            return true;
        }

        m_bottomSheet.OnBackButtonPressedCommand?.Execute(() =>
        {
            m_bottomSheet.Close();
        });
        
        return !m_bottomSheet.IsInteractiveCloseable;
    }

    internal class BottomSheetCallback : BottomSheetBehavior.BottomSheetCallback
    {
        private readonly BottomSheetHandler m_bottomSheetHandler;

        public BottomSheetCallback(BottomSheetHandler bottomSheetHandler)
        {
            m_bottomSheetHandler = bottomSheetHandler;
        }

        public override void OnSlide(AView bottomSheet, float slideOffset)
        {
            m_bottomSheetHandler.OnSlide(slideOffset, bottomSheet);
        }

        public override void OnStateChanged(AView bottomSheet, int state)
        {
            m_bottomSheetHandler.m_bottomSheet.Positioning = state switch
            {
                BottomSheetBehavior.StateExpanded => Positioning.Large,
                BottomSheetBehavior.StateHalfExpanded => Positioning.Medium,
                BottomSheetBehavior.StateCollapsed => Positioning.Medium,
                _ => m_bottomSheetHandler.m_bottomSheet.Positioning
            };
        }
    }

    internal class KeyListener : Object, IDialogInterfaceOnKeyListener
    {   
        private readonly BottomSheetHandler m_bottomSheetHandler;

        public KeyListener(BottomSheetHandler bottomSheetHandler)
        {
            m_bottomSheetHandler = bottomSheetHandler;
        }

        public bool OnKey(IDialogInterface? dialog, Keycode keyCode, KeyEvent? e) =>
            m_bottomSheetHandler.OnKey(dialog, keyCode, e);
    }

}

public class DialogInterfaceOnShowListener : Object, IDialogInterfaceOnShowListener
{
    private readonly BottomSheetHandler m_handler;

    public DialogInterfaceOnShowListener(BottomSheetHandler handler)
    {
        m_handler = handler;
    }
    
    public void OnShow(IDialogInterface? dialog)
    {
        if (m_handler.m_linearLayout.Parent is FrameLayout frameLayout)
        {
            m_handler.SetBottomBarTranslation(frameLayout);
        }
    }
}

class TextOrIconDrawable : DrawerArrowDrawable
{
    public Drawable? IconBitmap { get; set; }
    public string Text { get; set; }
    public Color TintColor { get; set; }
    [TypeConverter(nameof(ImageSourceConverter))]
    public ImageSource IconBitmapSource { get; set; }

    readonly float m_defaultSize;

    Color PressedBackgroundColor => TintColor.AddLuminosity(-.12f);//<item name="highlight_alpha_material_light" format="float" type="dimen">0.12</item>

    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);
        if (disposing && IconBitmap != null)
        {
            IconBitmap.Dispose();
        }
    }

    public TextOrIconDrawable(Context context, Color defaultColor, Drawable? icon, string text) : base(context)
    {
        TintColor = defaultColor;
        if (context.TryResolveAttribute(AndroidResource.Attribute.TextSize, out float? value) &&
            value != null)
        {
            m_defaultSize = value.Value;
        }
        else
        {
            m_defaultSize = 50;
        }

        IconBitmap = icon;
        Text = text;
    }

    public override void Draw(Canvas canvas)
    {
        const bool pressed = false;
        if (IconBitmap != null)
        {
            ADrawableCompat.SetTint(IconBitmap, TintColor.ToPlatform());
            ADrawableCompat.SetTintMode(IconBitmap, PorterDuff.Mode.SrcAtop);

            IconBitmap.SetBounds(Bounds.Left, Bounds.Top, Bounds.Right, Bounds.Bottom);
            IconBitmap.Draw(canvas);
        }
        else if (!string.IsNullOrEmpty(Text))
        {
            var paint = new Paint() { AntiAlias = true };
            paint.TextSize = m_defaultSize;
#pragma warning disable CA1416 // https://github.com/xamarin/xamarin-android/issues/6962
            paint.Color = pressed ? PressedBackgroundColor.ToPlatform() : TintColor.ToPlatform();
#pragma warning restore CA1416
            paint.SetStyle(Paint.Style.Fill);
            var y = (Bounds.Height() + paint.TextSize) / 2;
            canvas.DrawText(Text, 0, y, paint);
        }
    }
}
