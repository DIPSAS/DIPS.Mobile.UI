.class Landroidx/fragment/app/FragmentTransitionCompat21$Api19Impl;
.super Ljava/lang/Object;
.source "FragmentTransitionCompat21.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Landroidx/fragment/app/FragmentTransitionCompat21;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x8
    name = "Api19Impl"
.end annotation


# direct methods
.method private constructor <init>()V
    .locals 0

    .line 363
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method

.method static addListener(Landroid/transition/Transition;Landroid/transition/Transition$TransitionListener;)V
    .locals 0
    .param p0, "transition"    # Landroid/transition/Transition;
    .param p1, "listener"    # Landroid/transition/Transition$TransitionListener;

    .line 367
    invoke-virtual {p0, p1}, Landroid/transition/Transition;->addListener(Landroid/transition/Transition$TransitionListener;)Landroid/transition/Transition;

    .line 368
    return-void
.end method

.method static removeListener(Landroid/transition/Transition;Landroid/transition/Transition$TransitionListener;)V
    .locals 0
    .param p0, "transition"    # Landroid/transition/Transition;
    .param p1, "listener"    # Landroid/transition/Transition$TransitionListener;

    .line 372
    invoke-virtual {p0, p1}, Landroid/transition/Transition;->removeListener(Landroid/transition/Transition$TransitionListener;)Landroid/transition/Transition;

    .line 373
    return-void
.end method
