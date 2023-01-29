.class public Lmono/androidx/constraintlayout/motion/widget/MotionLayout_TransitionListenerImplementor;
.super Ljava/lang/Object;
.source "MotionLayout_TransitionListenerImplementor.java"

# interfaces
.implements Lmono/android/IGCUserPeer;
.implements Landroidx/constraintlayout/motion/widget/MotionLayout$TransitionListener;


# static fields
.field public static final __md_methods:Ljava/lang/String;


# instance fields
.field private refList:Ljava/util/ArrayList;


# direct methods
.method static constructor <clinit>()V
    .locals 3

    .line 13
    const-string v0, "n_onTransitionChange:(Landroidx/constraintlayout/motion/widget/MotionLayout;IIF)V:GetOnTransitionChange_Landroidx_constraintlayout_motion_widget_MotionLayout_IIFHandler:AndroidX.ConstraintLayout.Motion.Widget.MotionLayout/ITransitionListenerInvoker, Xamarin.AndroidX.ConstraintLayout\nn_onTransitionCompleted:(Landroidx/constraintlayout/motion/widget/MotionLayout;I)V:GetOnTransitionCompleted_Landroidx_constraintlayout_motion_widget_MotionLayout_IHandler:AndroidX.ConstraintLayout.Motion.Widget.MotionLayout/ITransitionListenerInvoker, Xamarin.AndroidX.ConstraintLayout\nn_onTransitionStarted:(Landroidx/constraintlayout/motion/widget/MotionLayout;II)V:GetOnTransitionStarted_Landroidx_constraintlayout_motion_widget_MotionLayout_IIHandler:AndroidX.ConstraintLayout.Motion.Widget.MotionLayout/ITransitionListenerInvoker, Xamarin.AndroidX.ConstraintLayout\nn_onTransitionTrigger:(Landroidx/constraintlayout/motion/widget/MotionLayout;IZF)V:GetOnTransitionTrigger_Landroidx_constraintlayout_motion_widget_MotionLayout_IZFHandler:AndroidX.ConstraintLayout.Motion.Widget.MotionLayout/ITransitionListenerInvoker, Xamarin.AndroidX.ConstraintLayout\n"

    sput-object v0, Lmono/androidx/constraintlayout/motion/widget/MotionLayout_TransitionListenerImplementor;->__md_methods:Ljava/lang/String;

    .line 19
    const-class v1, Lmono/androidx/constraintlayout/motion/widget/MotionLayout_TransitionListenerImplementor;

    const-string v2, "AndroidX.ConstraintLayout.Motion.Widget.MotionLayout+ITransitionListenerImplementor, Xamarin.AndroidX.ConstraintLayout"

    invoke-static {v2, v1, v0}, Lmono/android/Runtime;->register(Ljava/lang/String;Ljava/lang/Class;Ljava/lang/String;)V

    .line 20
    return-void
.end method

.method public constructor <init>()V
    .locals 3

    .line 25
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 26
    invoke-virtual {p0}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    move-result-object v0

    const-class v1, Lmono/androidx/constraintlayout/motion/widget/MotionLayout_TransitionListenerImplementor;

    if-ne v0, v1, :cond_0

    .line 27
    const/4 v0, 0x0

    new-array v0, v0, [Ljava/lang/Object;

    const-string v1, "AndroidX.ConstraintLayout.Motion.Widget.MotionLayout+ITransitionListenerImplementor, Xamarin.AndroidX.ConstraintLayout"

    const-string v2, ""

    invoke-static {v1, v2, p0, v0}, Lmono/android/TypeManager;->Activate(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Object;[Ljava/lang/Object;)V

    .line 29
    :cond_0
    return-void
.end method

.method private native n_onTransitionChange(Landroidx/constraintlayout/motion/widget/MotionLayout;IIF)V
.end method

.method private native n_onTransitionCompleted(Landroidx/constraintlayout/motion/widget/MotionLayout;I)V
.end method

.method private native n_onTransitionStarted(Landroidx/constraintlayout/motion/widget/MotionLayout;II)V
.end method

.method private native n_onTransitionTrigger(Landroidx/constraintlayout/motion/widget/MotionLayout;IZF)V
.end method


# virtual methods
.method public monodroidAddReference(Ljava/lang/Object;)V
    .locals 1

    .line 66
    iget-object v0, p0, Lmono/androidx/constraintlayout/motion/widget/MotionLayout_TransitionListenerImplementor;->refList:Ljava/util/ArrayList;

    if-nez v0, :cond_0

    .line 67
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    iput-object v0, p0, Lmono/androidx/constraintlayout/motion/widget/MotionLayout_TransitionListenerImplementor;->refList:Ljava/util/ArrayList;

    .line 68
    :cond_0
    iget-object v0, p0, Lmono/androidx/constraintlayout/motion/widget/MotionLayout_TransitionListenerImplementor;->refList:Ljava/util/ArrayList;

    invoke-virtual {v0, p1}, Ljava/util/ArrayList;->add(Ljava/lang/Object;)Z

    .line 69
    return-void
.end method

.method public monodroidClearReferences()V
    .locals 1

    .line 73
    iget-object v0, p0, Lmono/androidx/constraintlayout/motion/widget/MotionLayout_TransitionListenerImplementor;->refList:Ljava/util/ArrayList;

    if-eqz v0, :cond_0

    .line 74
    invoke-virtual {v0}, Ljava/util/ArrayList;->clear()V

    .line 75
    :cond_0
    return-void
.end method

.method public onTransitionChange(Landroidx/constraintlayout/motion/widget/MotionLayout;IIF)V
    .locals 0

    .line 34
    invoke-direct {p0, p1, p2, p3, p4}, Lmono/androidx/constraintlayout/motion/widget/MotionLayout_TransitionListenerImplementor;->n_onTransitionChange(Landroidx/constraintlayout/motion/widget/MotionLayout;IIF)V

    .line 35
    return-void
.end method

.method public onTransitionCompleted(Landroidx/constraintlayout/motion/widget/MotionLayout;I)V
    .locals 0

    .line 42
    invoke-direct {p0, p1, p2}, Lmono/androidx/constraintlayout/motion/widget/MotionLayout_TransitionListenerImplementor;->n_onTransitionCompleted(Landroidx/constraintlayout/motion/widget/MotionLayout;I)V

    .line 43
    return-void
.end method

.method public onTransitionStarted(Landroidx/constraintlayout/motion/widget/MotionLayout;II)V
    .locals 0

    .line 50
    invoke-direct {p0, p1, p2, p3}, Lmono/androidx/constraintlayout/motion/widget/MotionLayout_TransitionListenerImplementor;->n_onTransitionStarted(Landroidx/constraintlayout/motion/widget/MotionLayout;II)V

    .line 51
    return-void
.end method

.method public onTransitionTrigger(Landroidx/constraintlayout/motion/widget/MotionLayout;IZF)V
    .locals 0

    .line 58
    invoke-direct {p0, p1, p2, p3, p4}, Lmono/androidx/constraintlayout/motion/widget/MotionLayout_TransitionListenerImplementor;->n_onTransitionTrigger(Landroidx/constraintlayout/motion/widget/MotionLayout;IZF)V

    .line 59
    return-void
.end method
