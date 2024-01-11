using System.Net.Mime;
using Android.Content;
using Android.Graphics;
using Android.Text;
using Android.Text.Style;
using Android.Views;
using Android.Widget;
using DIPS.Mobile.UI.Extensions.Android;
using Microsoft.Maui.Platform;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;
using Rect = Android.Graphics.Rect;

namespace DIPS.Mobile.UI.Components.Labels.Android;

public class MauiTextView : Microsoft.Maui.Platform.MauiTextView
{
    private readonly Label m_label;
    
    private readonly string m_originalText;
    
    private string m_textAfterCustomTruncation;
    
    private int m_maxLinesAfterCustomTruncation;

    public MauiTextView(Context context, Label label) : base(context)
    {
        m_label = label;
        
        m_originalText = m_label.Text;
    }

    protected override void OnDraw(Canvas? canvas)
    {
        base.OnDraw(canvas);
        
        // Because SetTruncatedText() fires this function we have to check if the text is any different from after setting the custom truncation text or if MaxLines is set to something else, if not, we know that the consumer has not done any changes to the Label
        if (m_textAfterCustomTruncation == GetTextFromLabel() && m_maxLinesAfterCustomTruncation == m_label.MaxLines)
            return;

        // If the consumer only changed MaxLines, the Text will still be the custom truncated text, so we have to check if they are equal, if so, set the Text to the original
        if (GetTextFromLabel() == m_textAfterCustomTruncation)
        {
            SetText(m_originalText, BufferType.Spannable);
        }
        
        m_label.IsTruncated = CheckIfTruncated();
        SetTruncatedText();
    }

    private void SetTruncatedText()
    {
        if(!m_label.IsTruncated || string.IsNullOrEmpty(m_label.TruncatedText) || string.IsNullOrEmpty(Text))
            return;

        
        
        
        RemoveTextUntilNotTruncated();
        
        m_textAfterCustomTruncation = GetTextFromLabel();
        m_maxLinesAfterCustomTruncation = m_label.MaxLines;
    }
    
    private void RemoveTextUntilNotTruncated()
    {
        string customTruncatedText;
        var modifiedOriginalText = m_originalText;
        while (true)
        {
            modifiedOriginalText = modifiedOriginalText?
                .Substring(0, modifiedOriginalText.Length - 1);

            var modifiedOriginalTextWithCustomTruncationText = modifiedOriginalText + m_label.TruncatedText;
            
            if (!CheckIfTruncated(modifiedOriginalTextWithCustomTruncationText))
            {
                customTruncatedText = modifiedOriginalTextWithCustomTruncationText;
                break;
            }

        }
        var spannableString = new SpannableString(customTruncatedText);
            
        spannableString.SetSpan(new ForegroundColorSpan(Colors.GetColor(ColorName.color_primary_90).ToPlatform()), customTruncatedText.Length - m_label.TruncatedText.Length, customTruncatedText.Length, SpanTypes.ExclusiveExclusive);
        SetText(spannableString, BufferType.Spannable);
        
        

        /*var modifiedString2 = m_label.FormattedText.Spans[0].Text
            .Substring(0, m_label.FormattedText.Spans[0].Text.Length - 10);

        m_label.FormattedText.Spans[0].Text = modifiedString2;*/
    }

    private bool CheckIfTruncated(string? stringToCheck = null)
    {
        var text = stringToCheck ?? GetTextFromLabel();

        Console.WriteLine(text);
        var tempPaint = new TextPaint(PaintFlags.AntiAlias)
        {
            TextSize = TextSize,
        };
        tempPaint.SetTypeface(Typeface);
        
        var width = Width - PaddingLeft - PaddingRight;
        
#pragma warning disable CA1422
        var staticLayout = new StaticLayout(text, tempPaint, width,
            global::Android.Text.Layout.Alignment.AlignNormal, 1f, 0f, false);
#pragma warning restore CA1422
        
        if (m_label.MaxLines == -1)
            return false;

        return staticLayout.LineCount > m_label.MaxLines;
    }
    
    /*private bool CheckIfTruncated(bool internalCheck = false)
    {
        var lineBounds = new Rect();
        GetLineBounds(MaxLines - 1, lineBounds);



        return m_label.Width.ToMauiPixel() > lineBounds.Width();
    }*/


    private string GetTextFromLabel()
        => Text ?? string.Empty;

}