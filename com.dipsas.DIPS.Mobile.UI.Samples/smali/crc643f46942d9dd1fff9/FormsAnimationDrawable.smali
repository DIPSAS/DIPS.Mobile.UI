.class public Lcrc643f46942d9dd1fff9/FormsAnimationDrawable;
.super Landroid/graphics/drawable/AnimationDrawable;
.source "FormsAnimationDrawable.java"

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
    const-string v0, "n_isRunning:()Z:GetIsRunningHandler\nn_start:()V:GetStartHandler\nn_stop:()V:GetStopHandler\nn_selectDrawable:(I)Z:GetSelectDrawable_IHandler\n"

    sput-object v0, Lcrc643f46942d9dd1fff9/FormsAnimationDrawable;->__md_methods:Ljava/lang/String;

    .line 18
    const-class v1, Lcrc643f46942d9dd1fff9/FormsAnimationDrawable;

    const-string v2, "Xamarin.Forms.Platform.Android.FormsAnimationDrawable, Xamarin.Forms.Platform.Android"

    invoke-static {v2, v1, v0}, Lmono/android/Runtime;->register(Ljava/lang/String;Ljava/lang/Class;Ljava/lang/String;)V

    .line 19
    return-void
.end method

.method public constructor <init>()V
    .locals 3

    .line 24
    invoke-direct {p0}, Landroid/graphics/drawable/AnimationDrawable;-><init>()V

    .line 25
    invoke-virtual {p0}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    move-result-object v0

    const-class v1, Lcrc643f46942d9dd1fff9/FormsAnimationDrawable;

    if-ne v0, v1, :cond_0

    .line 26
    const/4 v0, 0x0

    new-array v0, v0, [Ljava/lang/Object;

    const-string v1, "Xamarin.Forms.Platform.Android.FormsAnimationDrawable, Xamarin.Forms.Platform.Android"

    const-string v2, ""

    invoke-static {v1, v2, p0, v0}, Lmono/android/TypeManager;->Activate(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Object;[Ljava/lang/Object;)V

    .line 28
    :cond_0
    return-void
.end method

.method private native n_isRunning()Z
.end method

.method private native n_selectDrawable(I)Z
.end method

.method private native n_start()V
.end method

.method private native n_stop()V
.end method


# virtual methods
.method public isRunning()Z
    .locals 1

    .line 33
    invoke-direct {p0}, Lcrc643f46942d9dd1fff9/FormsAnimationDrawable;->n_isRunning()Z

    move-result v0

    return v0
.end method

.method public monodroidAddReference(Ljava/lang/Object;)V
    .locals 1

    .line 65
    iget-object v0, p0, Lcrc643f46942d9dd1fff9/FormsAnimationDrawable;->refList:Ljava/util/ArrayList;

    if-nez v0, :cond_0

    .line 66
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    iput-object v0, p0, Lcrc643f46942d9dd1fff9/FormsAnimationDrawable;->refList:Ljava/util/ArrayList;

    .line 67
    :cond_0
    iget-object v0, p0, Lcrc643f46942d9dd1fff9/FormsAnimationDrawable;->refList:Ljava/util/ArrayList;

    invoke-virtual {v0, p1}, Ljava/util/ArrayList;->add(Ljava/lang/Object;)Z

    .line 68
    return-void
.end method

.method public monodroidClearReferences()V
    .locals 1

    .line 72
    iget-object v0, p0, Lcrc643f46942d9dd1fff9/FormsAnimationDrawable;->refList:Ljava/util/ArrayList;

    if-eqz v0, :cond_0

    .line 73
    invoke-virtual {v0}, Ljava/util/ArrayList;->clear()V

    .line 74
    :cond_0
    return-void
.end method

.method public selectDrawable(I)Z
    .locals 0

    .line 57
    invoke-direct {p0, p1}, Lcrc643f46942d9dd1fff9/FormsAnimationDrawable;->n_selectDrawable(I)Z

    move-result p1

    return p1
.end method

.method public start()V
    .locals 0

    .line 41
    invoke-direct {p0}, Lcrc643f46942d9dd1fff9/FormsAnimationDrawable;->n_start()V

    .line 42
    return-void
.end method

.method public stop()V
    .locals 0

    .line 49
    invoke-direct {p0}, Lcrc643f46942d9dd1fff9/FormsAnimationDrawable;->n_stop()V

    .line 50
    return-void
.end method
