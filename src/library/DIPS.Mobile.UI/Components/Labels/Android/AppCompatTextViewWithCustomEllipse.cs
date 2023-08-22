using System.Net.Mime;
using Android.Content;
using Android.Graphics;
using Android.Runtime;
using Android.Text;
using Android.Text.Style;
using Android.Util;
using AndroidX.AppCompat.Widget;
using DIPS.Mobile.UI.Resources.LocalizedStrings.LocalizedStrings;
using Kotlin.Text;
using Microsoft.Maui.Platform;
using Color = Android.Graphics.Color;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;

namespace DIPS.Mobile.UI.Components.Labels.Android;

public class AppCompatTextViewWithCustomEllipse : MauiTextView
{
    private SpannableString NewEllipsis;

    private string DefaultEllipsis => Typography.Ellipsis.ToString();

    private readonly SpannableStringBuilder m_spannableStringBuilder = new();
    
    public AppCompatTextViewWithCustomEllipse(Context context) : base(context)
    {
        NewEllipsis = new SpannableString($"... {DUILocalizedStrings.More.ToLower()}");
        NewEllipsis.SetSpan(new ForegroundColorSpan(Colors.GetColor(ColorName.color_primary_90).ToPlatform()), 0,
            NewEllipsis.Count(), SpanTypes.ExclusiveExclusive);
        NewEllipsis.SetSpan(new StyleSpan(TypefaceStyle.Bold), 0,
            NewEllipsis.Count(), SpanTypes.ExclusiveExclusive);
    }

    protected override void OnMeasure(int widthMeasureSpec, int heightMeasureSpec)
    {
        base.OnMeasure(widthMeasureSpec, heightMeasureSpec);
        
        var availableScreenWidth = MeasuredWidth - (float)CompoundPaddingLeft - CompoundPaddingRight;
        var availableTextWidth = availableScreenWidth * MaxLines;

        var ellipsizedText = TextUtils.Ellipsize(Text, Paint, availableTextWidth, Ellipsize);

        if (ellipsizedText != Text)
        {
            availableTextWidth = (availableScreenWidth - Paint!.MeasureText(NewEllipsis.ToString())) * MaxLines;
            ellipsizedText = TextUtils.Ellipsize(Text, Paint, availableTextWidth, Ellipsize);

            var defaultEllipsisStart = ellipsizedText.IndexOf(DefaultEllipsis);
            var defaultEllipsisEnd = defaultEllipsisStart + 1;

            m_spannableStringBuilder.Clear();
            m_spannableStringBuilder.Append(ellipsizedText);
            m_spannableStringBuilder.Replace(defaultEllipsisStart, defaultEllipsisEnd, NewEllipsis);
        
            SetText(m_spannableStringBuilder, BufferType.Spannable);
        }

    }

}