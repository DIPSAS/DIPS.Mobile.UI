.class public Lmono/android/transition/Transition_TransitionListenerImplementor;
.super Ljava/lang/Object;
.source "Transition_TransitionListenerImplementor.java"

# interfaces
.implements Lmono/android/IGCUserPeer;
.implements Landroid/transition/Transition$TransitionListener;


# static fields
.field public static final __md_methods:Ljava/lang/String;


# instance fields
.field private refList:Ljava/util/ArrayList;


# direct methods
.method static constructor <clinit>()V
    .locals 3

    .line 13
    const-string v0, "n_onTransitionCancel:(Landroid/transition/Transition;)V:GetOnTransitionCancel_Landroid_transition_Transition_Handler:Android.Transitions.Transition/ITransitionListenerInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\nn_onTransitionEnd:(Landroid/transition/Transition;)V:GetOnTransitionEnd_Landroid_transition_Transition_Handler:Android.Transitions.Transition/ITransitionListenerInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\nn_onTransitionPause:(Landroid/transition/Transition;)V:GetOnTransitionPause_Landroid_transition_Transition_Handler:Android.Transitions.Transition/ITransitionListenerInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\nn_onTransitionResume:(Landroid/transition/Transition;)V:GetOnTransitionResume_Landroid_transition_Transition_Handler:Android.Transitions.Transition/ITransitionListenerInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\nn_onTransitionStart:(Landroid/transition/Transition;)V:GetOnTransitionStart_Landroid_transition_Transition_Handler:Android.Transitions.Transition/ITransitionListenerInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n"

    sput-object v0, Lmono/android/transition/Transition_TransitionListenerImplementor;->__md_methods:Ljava/lang/String;

    .line 20
    const-class v1, Lmono/android/transition/Transition_TransitionListenerImplementor;

    const-string v2, "Android.Transitions.Transition+ITransitionListenerImplementor, Mono.Android"

    invoke-static {v2, v1, v0}, Lmono/android/Runtime;->register(Ljava/lang/String;Ljava/lang/Class;Ljava/lang/String;)V

    .line 21
    return-void
.end method

.method public constructor <init>()V
    .locals 3

    .line 26
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 27
    invoke-virtual {p0}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    move-result-object v0

    const-class v1, Lmono/android/transition/Transition_TransitionListenerImplementor;

    if-ne v0, v1, :cond_0

    .line 28
    const/4 v0, 0x0

    new-array v0, v0, [Ljava/lang/Object;

    const-string v1, "Android.Transitions.Transition+ITransitionListenerImplementor, Mono.Android"

    const-string v2, ""

    invoke-static {v1, v2, p0, v0}, Lmono/android/TypeManager;->Activate(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Object;[Ljava/lang/Object;)V

    .line 30
    :cond_0
    return-void
.end method

.method private native n_onTransitionCancel(Landroid/transition/Transition;)V
.end method

.method private native n_onTransitionEnd(Landroid/transition/Transition;)V
.end method

.method private native n_onTransitionPause(Landroid/transition/Transition;)V
.end method

.method private native n_onTransitionResume(Landroid/transition/Transition;)V
.end method

.method private native n_onTransitionStart(Landroid/transition/Transition;)V
.end method


# virtual methods
.method public monodroidAddReference(Ljava/lang/Object;)V
    .locals 1

    .line 75
    iget-object v0, p0, Lmono/android/transition/Transition_TransitionListenerImplementor;->refList:Ljava/util/ArrayList;

    if-nez v0, :cond_0

    .line 76
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    iput-object v0, p0, Lmono/android/transition/Transition_TransitionListenerImplementor;->refList:Ljava/util/ArrayList;

    .line 77
    :cond_0
    iget-object v0, p0, Lmono/android/transition/Transition_TransitionListenerImplementor;->refList:Ljava/util/ArrayList;

    invoke-virtual {v0, p1}, Ljava/util/ArrayList;->add(Ljava/lang/Object;)Z

    .line 78
    return-void
.end method

.method public monodroidClearReferences()V
    .locals 1

    .line 82
    iget-object v0, p0, Lmono/android/transition/Transition_TransitionListenerImplementor;->refList:Ljava/util/ArrayList;

    if-eqz v0, :cond_0

    .line 83
    invoke-virtual {v0}, Ljava/util/ArrayList;->clear()V

    .line 84
    :cond_0
    return-void
.end method

.method public onTransitionCancel(Landroid/transition/Transition;)V
    .locals 0

    .line 35
    invoke-direct {p0, p1}, Lmono/android/transition/Transition_TransitionListenerImplementor;->n_onTransitionCancel(Landroid/transition/Transition;)V

    .line 36
    return-void
.end method

.method public onTransitionEnd(Landroid/transition/Transition;)V
    .locals 0

    .line 43
    invoke-direct {p0, p1}, Lmono/android/transition/Transition_TransitionListenerImplementor;->n_onTransitionEnd(Landroid/transition/Transition;)V

    .line 44
    return-void
.end method

.method public onTransitionPause(Landroid/transition/Transition;)V
    .locals 0

    .line 51
    invoke-direct {p0, p1}, Lmono/android/transition/Transition_TransitionListenerImplementor;->n_onTransitionPause(Landroid/transition/Transition;)V

    .line 52
    return-void
.end method

.method public onTransitionResume(Landroid/transition/Transition;)V
    .locals 0

    .line 59
    invoke-direct {p0, p1}, Lmono/android/transition/Transition_TransitionListenerImplementor;->n_onTransitionResume(Landroid/transition/Transition;)V

    .line 60
    return-void
.end method

.method public onTransitionStart(Landroid/transition/Transition;)V
    .locals 0

    .line 67
    invoke-direct {p0, p1}, Lmono/android/transition/Transition_TransitionListenerImplementor;->n_onTransitionStart(Landroid/transition/Transition;)V

    .line 68
    return-void
.end method
