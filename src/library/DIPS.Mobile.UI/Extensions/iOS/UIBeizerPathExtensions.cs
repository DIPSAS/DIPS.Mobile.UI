using CoreGraphics;
using UIKit;

namespace DIPS.Mobile.UI.Extensions.iOS
{
    public static class UIBeizerPathExtensions
    {
        public static UIBezierPath CreateRoundedRectPath(this CGRect rect, CornerRadius cornerRadius)
        {
            var path = new UIBezierPath();

            path.MoveTo(new CGPoint(rect.Width - cornerRadius.TopRight, rect.Y));

            path.AddArc(new CGPoint((float)rect.X + rect.Width - cornerRadius.TopRight, (float)rect.Y + cornerRadius.TopRight), (nfloat)cornerRadius.TopRight, (float)(Math.PI * 1.5), (float)Math.PI * 2, true);
            path.AddLineTo(new CGPoint(rect.Width, rect.Height - cornerRadius.BottomRight));

            path.AddArc(new CGPoint((float)rect.X + rect.Width - cornerRadius.BottomRight, (float)rect.Y + rect.Height - cornerRadius.BottomRight), (nfloat)cornerRadius.BottomRight, 0, (float)(Math.PI * .5), true);
            path.AddLineTo(new CGPoint(cornerRadius.BottomLeft, rect.Height));

            path.AddArc(new CGPoint((float)rect.X + cornerRadius.BottomLeft, (float)rect.Y + rect.Height - cornerRadius.BottomLeft), (nfloat)cornerRadius.BottomLeft, (float)(Math.PI * .5), (float)Math.PI, true);
            path.AddLineTo(new CGPoint(rect.X, cornerRadius.TopLeft));

            path.AddArc(new CGPoint((float)rect.X + cornerRadius.TopLeft, (float)rect.Y + cornerRadius.TopLeft), (nfloat)cornerRadius.TopLeft, (float)Math.PI, (float)(Math.PI * 1.5), true);

            path.ClosePath();

            return path;
        }
    }
}