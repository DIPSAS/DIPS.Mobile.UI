.class Landroidx/browser/browseractions/BrowserActionsFallbackMenuDialog;
.super Landroid/app/Dialog;
.source "BrowserActionsFallbackMenuDialog.java"


# annotations
.annotation runtime Ljava/lang/Deprecated;
.end annotation


# static fields
.field private static final ENTER_ANIMATION_DURATION_MS:J = 0xfaL

.field private static final EXIT_ANIMATION_DURATION_MS:J = 0x96L


# instance fields
.field private final mContentView:Landroid/view/View;


# direct methods
.method constructor <init>(Landroid/content/Context;Landroid/view/View;)V
    .locals 0
    .param p1, "context"    # Landroid/content/Context;
    .param p2, "contentView"    # Landroid/view/View;
    .annotation system Ldalvik/annotation/MethodParameters;
        accessFlags = {
            0x0,
            0x0
        }
        names = {
            "context",
            "contentView"
        }
    .end annotation

    .line 45
    invoke-direct {p0, p1}, Landroid/app/Dialog;-><init>(Landroid/content/Context;)V

    .line 46
    iput-object p2, p0, Landroidx/browser/browseractions/BrowserActionsFallbackMenuDialog;->mContentView:Landroid/view/View;

    .line 47
    return-void
.end method

.method static synthetic access$001(Landroidx/browser/browseractions/BrowserActionsFallbackMenuDialog;)V
    .locals 0
    .param p0, "x0"    # Landroidx/browser/browseractions/BrowserActionsFallbackMenuDialog;

    .line 38
    invoke-super {p0}, Landroid/app/Dialog;->dismiss()V

    return-void
.end method

.method private startAnimation(Z)V
    .locals 6
    .param p1, "isEnterAnimation"    # Z
    .annotation system Ldalvik/annotation/MethodParameters;
        accessFlags = {
            0x10
        }
        names = {
            "isEnterAnimation"
        }
    .end annotation

    .line 72
    const/4 v0, 0x0

    const/high16 v1, 0x3f800000    # 1.0f

    if-eqz p1, :cond_0

    const/4 v2, 0x0

    goto :goto_0

    :cond_0
    const/high16 v2, 0x3f800000    # 1.0f

    .line 73
    .local v2, "from":F
    :goto_0
    if-eqz p1, :cond_1

    const/high16 v0, 0x3f800000    # 1.0f

    .line 74
    .local v0, "to":F
    :cond_1
    if-eqz p1, :cond_2

    const-wide/16 v3, 0xfa

    goto :goto_1

    :cond_2
    const-wide/16 v3, 0x96

    .line 75
    .local v3, "duration":J
    :goto_1
    iget-object v1, p0, Landroidx/browser/browseractions/BrowserActionsFallbackMenuDialog;->mContentView:Landroid/view/View;

    invoke-virtual {v1, v2}, Landroid/view/View;->setScaleX(F)V

    .line 76
    iget-object v1, p0, Landroidx/browser/browseractions/BrowserActionsFallbackMenuDialog;->mContentView:Landroid/view/View;

    invoke-virtual {v1, v2}, Landroid/view/View;->setScaleY(F)V

    .line 78
    iget-object v1, p0, Landroidx/browser/browseractions/BrowserActionsFallbackMenuDialog;->mContentView:Landroid/view/View;

    invoke-virtual {v1}, Landroid/view/View;->animate()Landroid/view/ViewPropertyAnimator;

    move-result-object v1

    .line 79
    invoke-virtual {v1, v0}, Landroid/view/ViewPropertyAnimator;->scaleX(F)Landroid/view/ViewPropertyAnimator;

    move-result-object v1

    .line 80
    invoke-virtual {v1, v0}, Landroid/view/ViewPropertyAnimator;->scaleY(F)Landroid/view/ViewPropertyAnimator;

    move-result-object v1

    .line 81
    invoke-virtual {v1, v3, v4}, Landroid/view/ViewPropertyAnimator;->setDuration(J)Landroid/view/ViewPropertyAnimator;

    move-result-object v1

    new-instance v5, Landroidx/interpolator/view/animation/LinearOutSlowInInterpolator;

    invoke-direct {v5}, Landroidx/interpolator/view/animation/LinearOutSlowInInterpolator;-><init>()V

    .line 82
    invoke-virtual {v1, v5}, Landroid/view/ViewPropertyAnimator;->setInterpolator(Landroid/animation/TimeInterpolator;)Landroid/view/ViewPropertyAnimator;

    move-result-object v1

    new-instance v5, Landroidx/browser/browseractions/BrowserActionsFallbackMenuDialog$1;

    invoke-direct {v5, p0, p1}, Landroidx/browser/browseractions/BrowserActionsFallbackMenuDialog$1;-><init>(Landroidx/browser/browseractions/BrowserActionsFallbackMenuDialog;Z)V

    .line 83
    invoke-virtual {v1, v5}, Landroid/view/ViewPropertyAnimator;->setListener(Landroid/animation/Animator$AnimatorListener;)Landroid/view/ViewPropertyAnimator;

    move-result-object v1

    .line 91
    invoke-virtual {v1}, Landroid/view/ViewPropertyAnimator;->start()V

    .line 92
    return-void
.end method


# virtual methods
.method public dismiss()V
    .locals 1

    .line 68
    const/4 v0, 0x0

    invoke-direct {p0, v0}, Landroidx/browser/browseractions/BrowserActionsFallbackMenuDialog;->startAnimation(Z)V

    .line 69
    return-void
.end method

.method public onTouchEvent(Landroid/view/MotionEvent;)Z
    .locals 1
    .param p1, "event"    # Landroid/view/MotionEvent;
    .annotation system Ldalvik/annotation/MethodParameters;
        accessFlags = {
            0x0
        }
        names = {
            "event"
        }
    .end annotation

    .line 59
    invoke-virtual {p1}, Landroid/view/MotionEvent;->getAction()I

    move-result v0

    if-nez v0, :cond_0

    .line 60
    invoke-virtual {p0}, Landroidx/browser/browseractions/BrowserActionsFallbackMenuDialog;->dismiss()V

    .line 61
    const/4 v0, 0x1

    return v0

    .line 63
    :cond_0
    const/4 v0, 0x0

    return v0
.end method

.method public show()V
    .locals 3

    .line 51
    invoke-virtual {p0}, Landroidx/browser/browseractions/BrowserActionsFallbackMenuDialog;->getWindow()Landroid/view/Window;

    move-result-object v0

    .line 52
    .local v0, "dialogWindow":Landroid/view/Window;
    new-instance v1, Landroid/graphics/drawable/ColorDrawable;

    const/4 v2, 0x0

    invoke-direct {v1, v2}, Landroid/graphics/drawable/ColorDrawable;-><init>(I)V

    invoke-virtual {v0, v1}, Landroid/view/Window;->setBackgroundDrawable(Landroid/graphics/drawable/Drawable;)V

    .line 53
    const/4 v1, 0x1

    invoke-direct {p0, v1}, Landroidx/browser/browseractions/BrowserActionsFallbackMenuDialog;->startAnimation(Z)V

    .line 54
    invoke-super {p0}, Landroid/app/Dialog;->show()V

    .line 55
    return-void
.end method
