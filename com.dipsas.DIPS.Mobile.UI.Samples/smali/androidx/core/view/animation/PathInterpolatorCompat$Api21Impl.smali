.class Landroidx/core/view/animation/PathInterpolatorCompat$Api21Impl;
.super Ljava/lang/Object;
.source "PathInterpolatorCompat.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Landroidx/core/view/animation/PathInterpolatorCompat;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x8
    name = "Api21Impl"
.end annotation


# direct methods
.method private constructor <init>()V
    .locals 0

    .line 96
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 98
    return-void
.end method

.method static createPathInterpolator(FF)Landroid/view/animation/PathInterpolator;
    .locals 1
    .param p0, "controlX"    # F
    .param p1, "controlY"    # F

    .line 107
    new-instance v0, Landroid/view/animation/PathInterpolator;

    invoke-direct {v0, p0, p1}, Landroid/view/animation/PathInterpolator;-><init>(FF)V

    return-object v0
.end method

.method static createPathInterpolator(FFFF)Landroid/view/animation/PathInterpolator;
    .locals 1
    .param p0, "controlX1"    # F
    .param p1, "controlY1"    # F
    .param p2, "controlX2"    # F
    .param p3, "controlY2"    # F

    .line 113
    new-instance v0, Landroid/view/animation/PathInterpolator;

    invoke-direct {v0, p0, p1, p2, p3}, Landroid/view/animation/PathInterpolator;-><init>(FFFF)V

    return-object v0
.end method

.method static createPathInterpolator(Landroid/graphics/Path;)Landroid/view/animation/PathInterpolator;
    .locals 1
    .param p0, "path"    # Landroid/graphics/Path;

    .line 102
    new-instance v0, Landroid/view/animation/PathInterpolator;

    invoke-direct {v0, p0}, Landroid/view/animation/PathInterpolator;-><init>(Landroid/graphics/Path;)V

    return-object v0
.end method
