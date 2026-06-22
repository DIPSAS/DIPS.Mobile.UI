using Android.Animation;
using Android.Graphics;
using View = Android.Views.View;

namespace DIPS.Mobile.UI.API.Camera.ImageCapturing.Views.BottomToolbar.StreamingState;

internal sealed partial class CapturedImagesGalleryButton
{
    private const float BlurRadius = 12f;
    private const long BlurFadeDurationInMilliseconds = 200;

    private ValueAnimator? m_blurAnimator;
    private float m_currentBlurRadius;

    private partial void SetThumbnailBlurred(bool shouldBlurThumbnail)
    {
        if (m_thumbnail.Handler?.PlatformView is not View nativeThumbnail)
            return;
        
        var targetBlurRadius = shouldBlurThumbnail ? BlurRadius : 0f;
        
        AnimateBlurRadius(nativeThumbnail, targetBlurRadius);
    }

    private void AnimateBlurRadius(View nativeThumbnail, float targetRadius)
    {
        m_blurAnimator?.Cancel();

        var startBlurRadius = m_currentBlurRadius;
        
        var animator = ValueAnimator.OfFloat(0f, 1f);

        if (animator is null) return;
        
        animator.SetDuration(BlurFadeDurationInMilliseconds);
        
        animator.Update += (_, e) =>
        {
            var animatedFraction = e.Animation?.AnimatedFraction ?? 1f;

            var fractionalBlurIncreaseOrDecrease = ((targetRadius - startBlurRadius) * animatedFraction);
            
            var blurRadius = startBlurRadius + fractionalBlurIncreaseOrDecrease;
            
            m_currentBlurRadius = blurRadius;
            
            nativeThumbnail.ApplyBlurRadius(blurRadius);
        };
        
        animator.Start();

        m_blurAnimator = animator;
    }
}

internal static class AndroidImageFilterExtensions
{
    public static void ApplyBlurRadius(this View nativeThumbnail, float radius)
    {
        if (nativeThumbnail.Handle == IntPtr.Zero)
            return;
        
        // Disable blur if radius is non-positive.
        var blurEffect = radius <= 0f
            ? null
            : RenderEffect.CreateBlurEffect(radius, radius, Shader.TileMode.Clamp!);

        nativeThumbnail.SetRenderEffect(blurEffect);
    }
}


