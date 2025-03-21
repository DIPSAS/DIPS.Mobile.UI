using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.Graphics.Drawable;
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
using DIPS.Mobile.UI.Components.BottomSheets.Header;

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

    public AView OnBeforeOpening(IMauiContext mauiContext, Context context, AView bottomSheetAndroidView,
        RelativeLayout rootLayout, LinearLayout bottomSheetLayout)
    {
        if (VirtualView is not BottomSheet bottomSheet) return new AView(Context);
        
        m_bottomSheet = bottomSheet;
        
        bottomSheet.BottomSheetDialog.Behavior.AddBottomSheetCallback(
            new BottomSheetCallback(this));
        bottomSheet.BottomSheetDialog.SetOnShowListener(new DialogInterfaceOnShowListener(this));
        bottomSheet.BottomSheetDialog.SetOnKeyListener(new KeyListener(this));

        //Add a handle, with a innerGrid that works as a big hit box for the user to hit
        //Inspired by com.google.android.material.bottomsheet.BottomSheetDragHandleView , which will be added in Xamarin Android Material Design v1.7.0.  https://github.com/material-components/material-components-android/commit/ac7b761294808748df167b50b223b591ca9dac06
        if (m_bottomSheet.BottomSheetBehavior.Draggable)
        {
            var innerGrid = new Grid
            {
                Padding = new Thickness(0, Sizes.GetSize(SizeName.content_margin_small), 0, 0), 
                BackgroundColor = Colors.GetColor(ColorName.color_system_white)
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
                BackgroundColor = Colors.GetColor(ColorName.color_neutral_40),
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center
            };
            
            innerGrid.Add(handle);

            bottomSheetLayout.AddView(innerGrid.ToPlatform(mauiContext));
        }

        bottomSheetLayout.AddView(new BottomSheetHeader(m_bottomSheet).ToPlatform(mauiContext));
        
        m_searchBarView = m_bottomSheet.SearchBar.ToPlatform(mauiContext);
        bottomSheetLayout.AddView(m_searchBarView);
        ToggleSearchBar();

        bottomSheetLayout.AddView(bottomSheetAndroidView);

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

    private void ToggleSearchBar()
    {
        if (m_searchBarView == null) return;
        if (m_bottomSheet.HasSearchBar)
        {
            m_searchBarView.Visibility = ViewStates.Visible;
        }
        else
        {
            m_searchBarView.Visibility = ViewStates.Gone;
        }
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

    protected override void DisconnectHandler(ContentViewGroup platformView)
    {
        base.DisconnectHandler(platformView);
        
        s_mEmptyNonFitToContentView?.RemoveFromParent();
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

        m_bottomSheet.OnBackButtonPressedCommand?.Execute(() =>
        {
            m_bottomSheet.Close();
        });
        
        return !m_bottomSheet.IsInteractiveCloseable; //Returning true will block back key action
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
