using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using DIPS.Mobile.UI.API.Library;
using Google.Android.Material.BottomSheet;
using Microsoft.Maui.Platform;
using AView = Android.Views.View;

namespace DIPS.Mobile.UI.Components.BottomSheets.Android
{
    public class BottomSheetFragment : BottomSheetDialogFragment
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
            var errorView = new AView(Context);
            if (mauiContext == null) return errorView;

            if (Dialog is not BottomSheetDialog bottomSheetDialog) return errorView;
            if (m_bottomSheetBehavior == null) return errorView;
            if (Context == null) return errorView;

            var rootLayout = new LinearLayout(Context)
            {
                LayoutParameters =
                    new LinearLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent,
                        ViewGroup.LayoutParams.WrapContent),
                Orientation = Orientation.Vertical
            };

            m_bottomSheet.RootLayout = rootLayout;
            m_bottomSheet.BottomSheetDialog = bottomSheetDialog;
            m_bottomSheet.BottomSheetBehavior = m_bottomSheetBehavior;


            var bottomSheetView = m_bottomSheet.ToPlatform(mauiContext!); //Triggers handler creation
            if (m_bottomSheet.Handler is not BottomSheetHandler bottomSheetHandler) return errorView;
            if (m_bottomSheetBehavior == null) return errorView;


            return bottomSheetHandler.OnBeforeOpening(mauiContext, Context, bottomSheetView, rootLayout);
        }

        public override Dialog OnCreateDialog(Bundle? savedInstanceState)
        {
            var activity = Platform.CurrentActivity;
            var dialog = base.OnCreateDialog(savedInstanceState);

            if (activity is null) return dialog;

            if (dialog is BottomSheetDialog bottomSheetDialog)
            {
                m_bottomSheetBehavior = bottomSheetDialog.Behavior;
            }

            var window = activity.Window;
            if (window is
                {
                    Attributes: not null
                }) //Make sure the dialog inherits window flag from the activity, useful when the activity is set as secured.
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
            BottomSheetService.RemoveFromStack(m_bottomSheet);
            m_bottomSheet.Handler?.DisconnectHandler();
        }

        public Task Close(bool animated)
        {
            Dismiss();
            return m_dismissTaskCompletionSource.Task;
        }
    }
}