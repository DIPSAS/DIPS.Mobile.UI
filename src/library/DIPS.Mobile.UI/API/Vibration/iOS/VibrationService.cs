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

        public static void SelectionChanged()
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

        public  static partial IPlatformFeedbackGenerator Generate()
        {
            return new PlatformFeedbackGenerator();
        }

        private class PlatformFeedbackGenerator : IPlatformFeedbackGenerator
        {
            private UISelectionFeedbackGenerator? m_generator;

            public PlatformFeedbackGenerator()
            {
                m_generator = new UISelectionFeedbackGenerator();
            }

            public void Prepare()
            {
                m_generator?.Prepare();
            }

            public void Release()
            {
                m_generator = null;
            }

            void IPlatformFeedbackGenerator.SelectionChanged()
            {
                m_generator?.SelectionChanged();
            }
        }
    }
}