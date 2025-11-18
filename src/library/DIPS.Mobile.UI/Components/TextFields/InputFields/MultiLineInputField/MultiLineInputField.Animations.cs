namespace DIPS.Mobile.UI.Components.TextFields.InputFields.MultiLineInputField;

public partial class MultiLineInputField
{
    private async Task PerformBreathingAnimation(double topAlpha, double bottomAlpha, double totalDurationInMs, 
        Color originalColor, 
        Color breathingEffectColor, int totalSegments = 8)
    {
        // The animation is split up into segments to make cancelling the animation more responsive. 
        var durationOfEachAnimationSegment = double.ConvertToInteger<uint>(totalDurationInMs / totalSegments);

        var alphaValuesAnimationSequence = BuildAnimationSequence(topAlpha, bottomAlpha, totalSegments);
        
        try 
        {
            while (m_isDictationActive)
            {
                foreach(var targetAlphaValue in alphaValuesAnimationSequence)
                {
                    // Cancel animation
                    if (!m_isDictationActive)
                    {
                        await AnimateDictationButtonBorderColorAlpha(1, 200, originalColor);
                        return;
                    }
                    
                    await AnimateDictationButtonBorderColorAlpha(targetAlphaValue, durationOfEachAnimationSegment, breathingEffectColor);
                }
            }
        }
        finally
        {
            m_isDictationAnimationActive = false;
            if (m_toggleDictationButton is not null && !m_toggleDictationButton.BorderColor.Equals(originalColor))
            {
                await AnimateDictationButtonBorderColorAlpha(1, 200, originalColor);
            }
        }
    }
    
    private static List<double> BuildAnimationSequence(double topAlpha, double bottomAlpha, int totalSegments)
    {
        List<double> alphaValuesAnimationSequence = [];

        var segmentsPerAnimationHalf = totalSegments / 2;
        
        var alphaDifferenceBetweenEndAndStart = topAlpha - bottomAlpha;
        
        var alphaDifferenceInEachSegment = alphaDifferenceBetweenEndAndStart / segmentsPerAnimationHalf;
        
        AddFirstHalfOfAnimationSequence(alphaValuesAnimationSequence, alphaDifferenceInEachSegment, 
            segmentsPerAnimationHalf, topAlpha);
        
        AddLastHalfOfAnimationSequence(alphaValuesAnimationSequence, alphaDifferenceInEachSegment, 
            segmentsPerAnimationHalf, bottomAlpha);

        return alphaValuesAnimationSequence;
    }
    
    private static void AddFirstHalfOfAnimationSequence(List<double> alphaValuesAnimationSequence, 
        double alphaDifferenceInEachSegment, int segments, double topAlpha)
    {
        for (var i = 1; i <= segments; i++)
        {
            var targetAlpha = topAlpha - (i * alphaDifferenceInEachSegment);
            alphaValuesAnimationSequence.Add(targetAlpha);
        }
    }

    private static void AddLastHalfOfAnimationSequence(List<double> alphaValuesAnimationSequence, 
        double alphaDifferenceInEachSegment, int segments, double bottomAlpha)
    {
        for (var i = 1; i <= segments; i++)
        {
            var targetAlpha = bottomAlpha + (i * alphaDifferenceInEachSegment);
            alphaValuesAnimationSequence.Add(targetAlpha);
        }
    }
    
    private Task<bool> AnimateDictationButtonBorderColorAlpha(double targetAlpha, uint duration, Color color)
    {
        var taskCompletionSource = new TaskCompletionSource<bool>();
        
        if (m_toggleDictationButton is null) return taskCompletionSource.Task;
        
        var animation = new Animation(
            callback: alphaValue =>
            {
                m_toggleDictationButton.BorderColor = color.WithAlpha((float)alphaValue);
            },
            start: m_toggleDictationButton.BorderColor.Alpha,
            end: targetAlpha,
            easing: Easing.SinInOut);
        
        
        animation.Commit(
            owner: m_toggleDictationButton,
            name: "BorderPulseAnimation",
            length: duration,
            finished: (_, _) => taskCompletionSource.SetResult(true));
        
        return taskCompletionSource.Task;
    }
    
    private Task<bool> AnimateDictationButtonBorderWidth(double targetWidth, uint duration)
    {
        var taskCompletionSource = new TaskCompletionSource<bool>();
        
        if (m_toggleDictationButton is null) return taskCompletionSource.Task;
        
        var animation = new Animation(
            callback: borderWidth =>
            {
                m_toggleDictationButton.BorderWidth = borderWidth;
            },
            start: m_toggleDictationButton.BorderWidth,
            end: targetWidth,
            easing: Easing.SinInOut);
        
        animation.Commit(
            owner: m_toggleDictationButton,
            name: "BorderWidthAnimation",
            length: duration,
            finished: (_, _) => taskCompletionSource.SetResult(true));
        
        return taskCompletionSource.Task;
    }
}