.class Landroidx/core/view/ViewPropertyAnimatorCompat$Api18Impl;
.super Ljava/lang/Object;
.source "ViewPropertyAnimatorCompat.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Landroidx/core/view/ViewPropertyAnimatorCompat;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x8
    name = "Api18Impl"
.end annotation


# direct methods
.method private constructor <init>()V
    .locals 0

    .line 865
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 867
    return-void
.end method

.method static getInterpolator(Landroid/view/ViewPropertyAnimator;)Landroid/view/animation/Interpolator;
    .locals 1
    .param p0, "viewPropertyAnimator"    # Landroid/view/ViewPropertyAnimator;

    .line 871
    invoke-virtual {p0}, Landroid/view/ViewPropertyAnimator;->getInterpolator()Landroid/animation/TimeInterpolator;

    move-result-object v0

    check-cast v0, Landroid/view/animation/Interpolator;

    return-object v0
.end method
