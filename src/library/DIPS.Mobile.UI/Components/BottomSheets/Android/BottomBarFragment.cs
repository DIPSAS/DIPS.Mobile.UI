using Android.App;
using Android.Content;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Views;
using Microsoft.Maui.Platform;
using Color = Android.Graphics.Color;
using DialogFragment = AndroidX.Fragment.App.DialogFragment;
using View = Android.Views.View;


namespace DIPS.Mobile.UI.Components.BottomSheets.Android;

public class BottomBarFragment(BottomSheetHandler bottomSheetHandler, IMauiContext mauiContext) : DialogFragment
{
    private Border? m_border;
    private HorizontalStackLayout? m_horizontalStackLayout;

    public override void OnCreate(Bundle? savedInstanceState)
    {
        SetStyle(DialogFragment.StyleNoFrame, global::Android.Resource.Style.Theme);

        base.OnCreate(savedInstanceState);
    }

    public override View? OnCreateView(LayoutInflater inflater, ViewGroup? container, Bundle? savedInstanceState)
    {
        m_border = new Border()
        {
            Padding = Sizes.GetSize(SizeName.size_2),
            StrokeThickness = 0,
            VerticalOptions = LayoutOptions.End,
            HorizontalOptions = LayoutOptions.Center,
            BackgroundColor = bottomSheetHandler.m_bottomSheet.BackgroundColor
        };
        m_horizontalStackLayout = new HorizontalStackLayout()
        {
            HorizontalOptions = LayoutOptions.Center, VerticalOptions = LayoutOptions.End,
        };
        foreach (var button in bottomSheetHandler.m_bottomSheet.BottombarButtons)
        {
            m_horizontalStackLayout.Add(button);
        }

        m_border.Content = m_horizontalStackLayout;
        var view =  m_border.ToPlatform(mauiContext);
        view.SetMinimumHeight(500);
        view.Background =
            new GradientDrawable(GradientDrawable.Orientation.TopBottom, new int[] {Color.Transparent, m_border.BackgroundColor.ToPlatform()});
        return view;
    }

    public override Dialog OnCreateDialog(Bundle? savedInstanceState)
    {
        var dialog = base.OnCreateDialog(savedInstanceState);

        //Align bottom
        dialog.Window?.SetGravity(GravityFlags.Bottom | GravityFlags.CenterHorizontal);
        //Forward back actions
        dialog.SetOnKeyListener(new BottomBarFragmentKeyListener(bottomSheetHandler));
        //Make sure its no bigger than what it needs, full width but wrap content in height
        dialog.Window?.SetLayout(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.WrapContent);

        dialog?.Window?.SetBackgroundDrawable(new ColorDrawable(Color.Transparent));

        //Make sure we do not have a dim background and can tap through it
        dialog.Window?.SetFlags(WindowManagerFlags.NotTouchModal,
            WindowManagerFlags.NotTouchModal);
        dialog.Window?.ClearFlags(WindowManagerFlags.DimBehind);


        return dialog;
    }

    public class BottomBarFragmentKeyListener(BottomSheetHandler bottomSheetHandler)
        : Java.Lang.Object, IDialogInterfaceOnKeyListener
    {
        public bool OnKey(IDialogInterface? dialog, Keycode keyCode, KeyEvent? e)
        {
            var shouldKeepOpen = bottomSheetHandler.OnKey(dialog, keyCode, e);
            if (!shouldKeepOpen)
            {
                bottomSheetHandler.m_bottomSheet.Close();
            }

            return true;
        }
    }

    /// <summary>
    /// 1.0 = full screen
    /// 0.0 = medium screen
    /// -1.0 = closed
    /// </summary>
    public void OnBottomSheetSlide(float slideOffset)
    {
        var theViewToFadeOut = m_border;
        if (theViewToFadeOut == null) return;
        if (slideOffset <= -0.55)
        {
            theViewToFadeOut?.TranslateTo(0, 300);
        }
        else if (slideOffset > -0.55 && theViewToFadeOut.TranslationY > 0)
        {
            theViewToFadeOut.TranslateTo(0,0);
        }
    }
}