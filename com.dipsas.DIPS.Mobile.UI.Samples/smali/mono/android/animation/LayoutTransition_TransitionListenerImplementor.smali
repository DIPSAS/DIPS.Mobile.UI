.class public Lmono/android/animation/LayoutTransition_TransitionListenerImplementor;
.super Ljava/lang/Object;
.source "LayoutTransition_TransitionListenerImplementor.java"

# interfaces
.implements Lmono/android/IGCUserPeer;
.implements Landroid/animation/LayoutTransition$TransitionListener;


# static fields
.field public static final __md_methods:Ljava/lang/String;


# instance fields
.field private refList:Ljava/util/ArrayList;


# direct methods
.method static constructor <clinit>()V
    .locals 3

    .line 13
    const-string v0, "n_endTransition:(Landroid/animation/LayoutTransition;Landroid/view/ViewGroup;Landroid/view/View;I)V:GetEndTransition_Landroid_animation_LayoutTransition_Landroid_view_ViewGroup_Landroid_view_View_IHandler:Android.Animation.LayoutTransition/ITransitionListenerInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\nn_startTransition:(Landroid/animation/LayoutTransition;Landroid/view/ViewGroup;Landroid/view/View;I)V:GetStartTransition_Landroid_animation_LayoutTransition_Landroid_view_ViewGroup_Landroid_view_View_IHandler:Android.Animation.LayoutTransition/ITransitionListenerInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n"

    sput-object v0, Lmono/android/animation/LayoutTransition_TransitionListenerImplementor;->__md_methods:Ljava/lang/String;

    .line 17
    const-class v1, Lmono/android/animation/LayoutTransition_TransitionListenerImplementor;

    const-string v2, "Android.Animation.LayoutTransition+ITransitionListenerImplementor, Mono.Android"

    invoke-static {v2, v1, v0}, Lmono/android/Runtime;->register(Ljava/lang/String;Ljava/lang/Class;Ljava/lang/String;)V

    .line 18
    return-void
.end method

.method public constructor <init>()V
    .locals 3

    .line 23
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 24
    invoke-virtual {p0}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    move-result-object v0

    const-class v1, Lmono/android/animation/LayoutTransition_TransitionListenerImplementor;

    if-ne v0, v1, :cond_0

    .line 25
    const/4 v0, 0x0

    new-array v0, v0, [Ljava/lang/Object;

    const-string v1, "Android.Animation.LayoutTransition+ITransitionListenerImplementor, Mono.Android"

    const-string v2, ""

    invoke-static {v1, v2, p0, v0}, Lmono/android/TypeManager;->Activate(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Object;[Ljava/lang/Object;)V

    .line 27
    :cond_0
    return-void
.end method

.method private native n_endTransition(Landroid/animation/LayoutTransition;Landroid/view/ViewGroup;Landroid/view/View;I)V
.end method

.method private native n_startTransition(Landroid/animation/LayoutTransition;Landroid/view/ViewGroup;Landroid/view/View;I)V
.end method


# virtual methods
.method public endTransition(Landroid/animation/LayoutTransition;Landroid/view/ViewGroup;Landroid/view/View;I)V
    .locals 0

    .line 32
    invoke-direct {p0, p1, p2, p3, p4}, Lmono/android/animation/LayoutTransition_TransitionListenerImplementor;->n_endTransition(Landroid/animation/LayoutTransition;Landroid/view/ViewGroup;Landroid/view/View;I)V

    .line 33
    return-void
.end method

.method public monodroidAddReference(Ljava/lang/Object;)V
    .locals 1

    .line 48
    iget-object v0, p0, Lmono/android/animation/LayoutTransition_TransitionListenerImplementor;->refList:Ljava/util/ArrayList;

    if-nez v0, :cond_0

    .line 49
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    iput-object v0, p0, Lmono/android/animation/LayoutTransition_TransitionListenerImplementor;->refList:Ljava/util/ArrayList;

    .line 50
    :cond_0
    iget-object v0, p0, Lmono/android/animation/LayoutTransition_TransitionListenerImplementor;->refList:Ljava/util/ArrayList;

    invoke-virtual {v0, p1}, Ljava/util/ArrayList;->add(Ljava/lang/Object;)Z

    .line 51
    return-void
.end method

.method public monodroidClearReferences()V
    .locals 1

    .line 55
    iget-object v0, p0, Lmono/android/animation/LayoutTransition_TransitionListenerImplementor;->refList:Ljava/util/ArrayList;

    if-eqz v0, :cond_0

    .line 56
    invoke-virtual {v0}, Ljava/util/ArrayList;->clear()V

    .line 57
    :cond_0
    return-void
.end method

.method public startTransition(Landroid/animation/LayoutTransition;Landroid/view/ViewGroup;Landroid/view/View;I)V
    .locals 0

    .line 40
    invoke-direct {p0, p1, p2, p3, p4}, Lmono/android/animation/LayoutTransition_TransitionListenerImplementor;->n_startTransition(Landroid/animation/LayoutTransition;Landroid/view/ViewGroup;Landroid/view/View;I)V

    .line 41
    return-void
.end method
