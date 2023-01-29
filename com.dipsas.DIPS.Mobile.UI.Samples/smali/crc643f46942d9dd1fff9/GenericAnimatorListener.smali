.class public Lcrc643f46942d9dd1fff9/GenericAnimatorListener;
.super Landroid/animation/AnimatorListenerAdapter;
.source "GenericAnimatorListener.java"

# interfaces
.implements Lmono/android/IGCUserPeer;


# static fields
.field public static final __md_methods:Ljava/lang/String;


# instance fields
.field private refList:Ljava/util/ArrayList;


# direct methods
.method static constructor <clinit>()V
    .locals 3

    .line 12
    const-string v0, "n_onAnimationCancel:(Landroid/animation/Animator;)V:GetOnAnimationCancel_Landroid_animation_Animator_Handler\nn_onAnimationEnd:(Landroid/animation/Animator;)V:GetOnAnimationEnd_Landroid_animation_Animator_Handler\nn_onAnimationRepeat:(Landroid/animation/Animator;)V:GetOnAnimationRepeat_Landroid_animation_Animator_Handler\nn_finalize:()V:GetJavaFinalizeHandler\n"

    sput-object v0, Lcrc643f46942d9dd1fff9/GenericAnimatorListener;->__md_methods:Ljava/lang/String;

    .line 18
    const-class v1, Lcrc643f46942d9dd1fff9/GenericAnimatorListener;

    const-string v2, "Xamarin.Forms.Platform.Android.GenericAnimatorListener, Xamarin.Forms.Platform.Android"

    invoke-static {v2, v1, v0}, Lmono/android/Runtime;->register(Ljava/lang/String;Ljava/lang/Class;Ljava/lang/String;)V

    .line 19
    return-void
.end method

.method public constructor <init>()V
    .locals 3

    .line 24
    invoke-direct {p0}, Landroid/animation/AnimatorListenerAdapter;-><init>()V

    .line 25
    invoke-virtual {p0}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    move-result-object v0

    const-class v1, Lcrc643f46942d9dd1fff9/GenericAnimatorListener;

    if-ne v0, v1, :cond_0

    .line 26
    const/4 v0, 0x0

    new-array v0, v0, [Ljava/lang/Object;

    const-string v1, "Xamarin.Forms.Platform.Android.GenericAnimatorListener, Xamarin.Forms.Platform.Android"

    const-string v2, ""

    invoke-static {v1, v2, p0, v0}, Lmono/android/TypeManager;->Activate(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Object;[Ljava/lang/Object;)V

    .line 28
    :cond_0
    return-void
.end method

.method private native n_finalize()V
.end method

.method private native n_onAnimationCancel(Landroid/animation/Animator;)V
.end method

.method private native n_onAnimationEnd(Landroid/animation/Animator;)V
.end method

.method private native n_onAnimationRepeat(Landroid/animation/Animator;)V
.end method


# virtual methods
.method public finalize()V
    .locals 0

    .line 57
    invoke-direct {p0}, Lcrc643f46942d9dd1fff9/GenericAnimatorListener;->n_finalize()V

    .line 58
    return-void
.end method

.method public monodroidAddReference(Ljava/lang/Object;)V
    .locals 1

    .line 65
    iget-object v0, p0, Lcrc643f46942d9dd1fff9/GenericAnimatorListener;->refList:Ljava/util/ArrayList;

    if-nez v0, :cond_0

    .line 66
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    iput-object v0, p0, Lcrc643f46942d9dd1fff9/GenericAnimatorListener;->refList:Ljava/util/ArrayList;

    .line 67
    :cond_0
    iget-object v0, p0, Lcrc643f46942d9dd1fff9/GenericAnimatorListener;->refList:Ljava/util/ArrayList;

    invoke-virtual {v0, p1}, Ljava/util/ArrayList;->add(Ljava/lang/Object;)Z

    .line 68
    return-void
.end method

.method public monodroidClearReferences()V
    .locals 1

    .line 72
    iget-object v0, p0, Lcrc643f46942d9dd1fff9/GenericAnimatorListener;->refList:Ljava/util/ArrayList;

    if-eqz v0, :cond_0

    .line 73
    invoke-virtual {v0}, Ljava/util/ArrayList;->clear()V

    .line 74
    :cond_0
    return-void
.end method

.method public onAnimationCancel(Landroid/animation/Animator;)V
    .locals 0

    .line 33
    invoke-direct {p0, p1}, Lcrc643f46942d9dd1fff9/GenericAnimatorListener;->n_onAnimationCancel(Landroid/animation/Animator;)V

    .line 34
    return-void
.end method

.method public onAnimationEnd(Landroid/animation/Animator;)V
    .locals 0

    .line 41
    invoke-direct {p0, p1}, Lcrc643f46942d9dd1fff9/GenericAnimatorListener;->n_onAnimationEnd(Landroid/animation/Animator;)V

    .line 42
    return-void
.end method

.method public onAnimationRepeat(Landroid/animation/Animator;)V
    .locals 0

    .line 49
    invoke-direct {p0, p1}, Lcrc643f46942d9dd1fff9/GenericAnimatorListener;->n_onAnimationRepeat(Landroid/animation/Animator;)V

    .line 50
    return-void
.end method
