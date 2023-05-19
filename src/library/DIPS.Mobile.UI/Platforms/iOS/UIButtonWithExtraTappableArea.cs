using CoreGraphics;
using UIKit;

namespace DIPS.Mobile.UI.Platforms.iOS
{
    internal class UIButtonWithExtraTappableArea : UIButton
    {
        private readonly nfloat m_horizontalSpace;
        private readonly nfloat m_verticalSpace;

        public UIButtonWithExtraTappableArea(nfloat horizontalSpace, nfloat verticalSpace, CGRect cgRect) : base(cgRect)
        {
            m_horizontalSpace = horizontalSpace;
            m_verticalSpace = verticalSpace;
        }

        public override bool PointInside(CGPoint point, UIEvent? uievent)
        {
            return Bounds.Inset(m_horizontalSpace, m_verticalSpace).Contains(point);
        }
    }
}