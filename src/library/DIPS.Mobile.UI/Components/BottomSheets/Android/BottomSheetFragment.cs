using Android.App;
using Android.Content;
using Android.OS;
using Android.Text;
using Android.Text.Style;
using Android.Views;
using Android.Widget;
using DIPS.Mobile.UI.API.Library;
using Google.Android.Material.AppBar;
using Google.Android.Material.BottomSheet;
using Microsoft.Maui.Platform;
using Application = Android.App.Application;
using Grid = Microsoft.Maui.Controls.Grid;
using AView = Android.Views.View;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;
using Object = Java.Lang.Object;
using Orientation = Android.Widget.Orientation;

namespace DIPS.Mobile.UI.Components.BottomSheets.Android
{
    internal class BottomSheetFragment : BottomSheetDialogFragment
    {
        private readonly BottomSheet m_bottomSheet;
        private TaskCompletionSource<bool> m_showTaskCompletionSource;
        private BottomSheetBehavior? m_bottomSheetBehavior;
        private TaskCompletionSource<bool> m_dismissTaskCompletionSource;

        public BottomSheetFragment(BottomSheet bottomSheet)
        {
            m_bottomSheet = bottomSheet;
            m_showTaskCompletionSource = new TaskCompletionSource<bool>();
            m_dismissTaskCompletionSource = new TaskCompletionSource<bool>();
        }

       public override AView OnCreateView(LayoutInflater inflater, ViewGroup? container, Bundle? savedInstanceState)
       {
           var mauiContext = DUI.GetCurrentMauiContext;

           var linearLayout = new LinearLayout(Context)
           {
               LayoutParameters =
                   new LinearLayout.LayoutParams(ViewGroup.LayoutParams.WrapContent,
                       ViewGroup.LayoutParams.WrapContent),
               Orientation = Orientation.Vertical
           };

           //Add a handle, with a innerGrid that works as a big hit box for the user to hit
           //Inspired by com.google.android.material.bottomsheet.BottomSheetDragHandleView , which will be added in Xamarin Android Material Design v1.7.0.  https://github.com/material-components/material-components-android/commit/ac7b761294808748df167b50b223b591ca9dac06
           if (m_bottomSheetBehavior!.Draggable)
           {
               var innerGrid = new Grid {Padding = new Thickness(0,  Sizes.GetSize(SizeName.size_2))};
               innerGrid.GestureRecognizers.Add(new TapGestureRecognizer()
               {
                   Command = new Command(ToggleBottomSheetIfPossible)
               });
               var handle = new BoxView()
               {
                   HeightRequest = 4,
                   WidthRequest = 32,
                   CornerRadius = 10,
                   BackgroundColor = Colors.GetColor(ColorName.color_neutral_40),
                   HorizontalOptions = LayoutOptions.Center,
                   VerticalOptions = LayoutOptions.Center
               };
               innerGrid.Add(handle);

               linearLayout.AddView(innerGrid.ToPlatform(mauiContext!));

           }

           if (m_bottomSheet.ShouldHaveNavigationBar)
           {
               var toolbar = new MaterialToolbar(Context!);
               ConfigureToolbar(toolbar);
               linearLayout.AddView(toolbar);
           }

           if (m_bottomSheet.HasSearchBar)
           {
               linearLayout.AddView(m_bottomSheet.SearchBar!.ToPlatform(mauiContext!));
           }

           linearLayout.AddView(m_bottomSheet.ToPlatform(mauiContext!));

           if (!m_bottomSheet.ShouldFitToContent)
           {
                // Add an empty view, where its dimensions is set to always match the parent so that the LinearLayout will always take up available space
                linearLayout.AddView(new AView(Application.Context)
                {
                    LayoutParameters = new ViewGroup.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent)
                });
           }
           
            return linearLayout;
        }

       private void ConfigureToolbar(MaterialToolbar toolbar)
       {
           //TODO: Move to Mapper in handler
           toolbar.Title = m_bottomSheet.Title;
           toolbar.TitleCentered = true;

           //TODO: Move to mapper in handler
           if (toolbar.Menu == null) return;
           
           foreach (var toolbarItem in m_bottomSheet.ToolbarItems)
           {
               toolbarItem.BindingContext = m_bottomSheet.BindingContext;
               var color = Colors.GetColor(BottomSheet.ToolbarActionButtonsName).ToPlatform();

               var text = toolbarItem.Text ?? string.Empty;
               var titleTinted = new SpannableString(text);
               titleTinted.SetSpan(new ForegroundColorSpan(color), 0, titleTinted.Length(), 0);

               var menuItem = toolbar.Menu.Add(0, AView.GenerateViewId(), (int)toolbarItem.Order, titleTinted);
               menuItem!.SetShowAsAction(ShowAsAction.IfRoom);
               menuItem.SetOnMenuItemClickListener(new GenericMenuClickListener(((IMenuItemController)toolbarItem).Activate));
               SetMenuItemIcon(menuItem, toolbarItem);
           }
       }
       
       private static void SetMenuItemIcon(IMenuItem menuItem, ToolbarItem toolBarItem)
       {
           toolBarItem.IconImageSource.LoadImage(DUI.GetCurrentMauiContext!, result =>
           {
               var baseDrawable = result?.Value;

               if (baseDrawable == null)
                   return;

               using var constant = baseDrawable.GetConstantState();
               using var newDrawable = constant!.NewDrawable();
               using var iconDrawable = newDrawable.Mutate();
               iconDrawable.SetColorFilter(Colors.GetColor(BottomSheet.ToolbarActionButtonsName), FilterMode.SrcAtop);

               menuItem.SetIcon(iconDrawable);
           });
       }

        private void ToggleBottomSheetIfPossible()
        {
            if (Dialog is not BottomSheetDialog bottomSheetDialog)
                return;

            var bottomSheetBehavior = bottomSheetDialog.Behavior;
            var collapsed = bottomSheetDialog.Behavior.State == BottomSheetBehavior.StateCollapsed;
            bottomSheetBehavior.State =
                collapsed ? BottomSheetBehavior.StateExpanded : BottomSheetBehavior.StateCollapsed;
        }
        
        public override Dialog OnCreateDialog(Bundle? savedInstanceState)
        {
            var context = Platform.AppContext;
            var activity = Platform.CurrentActivity;
            var dialog = base.OnCreateDialog(savedInstanceState);
            
            if (activity is null) return dialog;
            
            if (dialog is BottomSheetDialog bottomSheetDialog)
            {
                m_bottomSheetBehavior = bottomSheetDialog.Behavior;
                
                //TODO: Move to handler
                if (!m_bottomSheet.IsInteractiveCloseable)
                {
                    bottomSheetDialog.SetCancelable(false);
                    bottomSheetDialog.SetCanceledOnTouchOutside(false);
                    bottomSheetDialog.Behavior.AddBottomSheetCallback(new BottomSheetCallback(bottomSheetDialog.Behavior));
                    bottomSheetDialog.SetOnKeyListener(new KeyListener(m_bottomSheet));
                }
                
                //TODO: Move to handler
                bottomSheetDialog.Behavior.FitToContents = m_bottomSheet.ShouldFitToContent;
                if (!m_bottomSheet.ShouldFitToContent)
                {
                    var fullScreenHeight = context.Resources?.DisplayMetrics?.HeightPixels;
                    if (fullScreenHeight != null)
                    {
                        bottomSheetDialog.Behavior.PeekHeight = fullScreenHeight.Value / 2;
                    }    
                }
            }

            var window = activity.Window;
            if (window is {Attributes: not null}) //Make sure the dialog inherits window flag from the activity, useful when the activity is set as secured.
            {
                var flags = window.Attributes.Flags;
                dialog.Window?.SetFlags(flags, flags);
            }

            dialog.Window?.SetSoftInputMode(SoftInput.AdjustResize);

            return dialog;
        }
        
        public override void OnCreate(Bundle? savedInstanceState)
        {
            m_showTaskCompletionSource.SetResult(true);
            base.OnCreate(savedInstanceState);
        }

        public Task Show()
        {
            var activity = Platform.CurrentActivity;
            var fragmentManager = activity?.GetFragmentManager();
            if (fragmentManager == null) return Task.CompletedTask;
            
            m_showTaskCompletionSource = new TaskCompletionSource<bool>();
            m_dismissTaskCompletionSource = new TaskCompletionSource<bool>();
            Show(fragmentManager, nameof(BottomSheetFragment));
            m_bottomSheet.SendOpen();
            return m_showTaskCompletionSource.Task;
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
            m_dismissTaskCompletionSource.SetResult(true);
            m_bottomSheet.SendClose();
            BottomSheetService.Current = null;
        }

        public Task Close(bool animated)
        {
            Dismiss();
            return m_dismissTaskCompletionSource.Task;
        }
        
        internal class GenericMenuClickListener : Object, IMenuItemOnMenuItemClickListener
        {
            readonly Action m_callback;

            public GenericMenuClickListener(Action callback)
            {
                m_callback = callback;
            }

            public bool OnMenuItemClick(IMenuItem item)
            {
                m_callback.Invoke();
                return true;
            }
        }
    }

    internal class BottomSheetCallback : BottomSheetBehavior.BottomSheetCallback

    {
        private readonly BottomSheetBehavior m_behavior;

        public BottomSheetCallback(BottomSheetBehavior behavior)
        {
            m_behavior = behavior;
        }

        public override void OnSlide(AView bottomSheet, float slideOffset)
        {
            if (slideOffset < 0)
            {
                m_behavior.State = BottomSheetBehavior.StateHalfExpanded;
            }
        }

        public override void OnStateChanged(AView bottomSheet, int newState)
        {
        }

    }
    
    internal class KeyListener : Object, IDialogInterfaceOnKeyListener
    {
        private readonly BottomSheet m_bottomSheet;

        public KeyListener(BottomSheet bottomSheet)
        {
            m_bottomSheet = bottomSheet;
        }
        
        public bool OnKey(IDialogInterface? dialog, Keycode keyCode, KeyEvent? e)
        {
            m_bottomSheet.OnBackButtonPressedCommand?.Execute(null);
            
            return true;
        }
    }
    
}
