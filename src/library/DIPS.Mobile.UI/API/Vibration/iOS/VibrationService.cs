using DIPS.Mobile.UI.Components.Slidable.Util;
using UIKit;

namespace DIPS.Mobile.UI.API.Vibration
{
    public partial class VibrationService
    {
        public static partial void Vibrate(int duration)
        {
            new UIImpactFeedbackGenerator(UIImpactFeedbackStyle.Medium).ImpactOccurred();
        }

        public static partial void Click()
        {
            new UIImpactFeedbackGenerator(UIImpactFeedbackStyle.Medium).ImpactOccurred();
        }

        public static partial void HeavyClick()
        {
            new UIImpactFeedbackGenerator(UIImpactFeedbackStyle.Rigid).ImpactOccurred();
        }

        public static partial void DoubleClick()
        {
            new UINotificationFeedbackGenerator().NotificationOccurred(UINotificationFeedbackType.Warning);
        }

        public static partial void SelectionChanged()
        {
            new UISelectionFeedbackGenerator().SelectionChanged();
        }

        public static partial void Error()
        {
            new UINotificationFeedbackGenerator().NotificationOccurred(UINotificationFeedbackType.Error);
        }

        public static partial void Success()
        {
            new UINotificationFeedbackGenerator().NotificationOccurred(UINotificationFeedbackType.Success);
        }
    }
}