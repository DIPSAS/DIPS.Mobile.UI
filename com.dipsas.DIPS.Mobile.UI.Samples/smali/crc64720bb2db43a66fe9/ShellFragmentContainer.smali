.class public Lcrc64720bb2db43a66fe9/ShellFragmentContainer;
.super Lcrc64720bb2db43a66fe9/FragmentContainer;
.source "ShellFragmentContainer.java"

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
    const-string v0, "n_onCreateView:(Landroid/view/LayoutInflater;Landroid/view/ViewGroup;Landroid/os/Bundle;)Landroid/view/View;:GetOnCreateView_Landroid_view_LayoutInflater_Landroid_view_ViewGroup_Landroid_os_Bundle_Handler\nn_onDestroyView:()V:GetOnDestroyViewHandler\nn_onDestroy:()V:GetOnDestroyHandler\n"

    sput-object v0, Lcrc64720bb2db43a66fe9/ShellFragmentContainer;->__md_methods:Ljava/lang/String;

    .line 17
    const-class v1, Lcrc64720bb2db43a66fe9/ShellFragmentContainer;

    const-string v2, "Xamarin.Forms.Platform.Android.AppCompat.ShellFragmentContainer, Xamarin.Forms.Platform.Android"

    invoke-static {v2, v1, v0}, Lmono/android/Runtime;->register(Ljava/lang/String;Ljava/lang/Class;Ljava/lang/String;)V

    .line 18
    return-void
.end method

.method public constructor <init>()V
    .locals 3

    .line 23
    invoke-direct {p0}, Lcrc64720bb2db43a66fe9/FragmentContainer;-><init>()V

    .line 24
    invoke-virtual {p0}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    move-result-object v0

    const-class v1, Lcrc64720bb2db43a66fe9/ShellFragmentContainer;

    if-ne v0, v1, :cond_0

    .line 25
    const/4 v0, 0x0

    new-array v0, v0, [Ljava/lang/Object;

    const-string v1, "Xamarin.Forms.Platform.Android.AppCompat.ShellFragmentContainer, Xamarin.Forms.Platform.Android"

    const-string v2, ""

    invoke-static {v1, v2, p0, v0}, Lmono/android/TypeManager;->Activate(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Object;[Ljava/lang/Object;)V

    .line 27
    :cond_0
    return-void
.end method

.method public constructor <init>(I)V
    .locals 2

    .line 32
    invoke-direct {p0, p1}, Lcrc64720bb2db43a66fe9/FragmentContainer;-><init>(I)V

    .line 33
    invoke-virtual {p0}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    move-result-object v0

    const-class v1, Lcrc64720bb2db43a66fe9/ShellFragmentContainer;

    if-ne v0, v1, :cond_0

    .line 34
    const/4 v0, 0x1

    new-array v0, v0, [Ljava/lang/Object;

    const/4 v1, 0x0

    invoke-static {p1}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object p1

    aput-object p1, v0, v1

    const-string p1, "Xamarin.Forms.Platform.Android.AppCompat.ShellFragmentContainer, Xamarin.Forms.Platform.Android"

    const-string v1, "System.Int32, mscorlib"

    invoke-static {p1, v1, p0, v0}, Lmono/android/TypeManager;->Activate(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Object;[Ljava/lang/Object;)V

    .line 36
    :cond_0
    return-void
.end method

.method private native n_onCreateView(Landroid/view/LayoutInflater;Landroid/view/ViewGroup;Landroid/os/Bundle;)Landroid/view/View;
.end method

.method private native n_onDestroy()V
.end method

.method private native n_onDestroyView()V
.end method


# virtual methods
.method public monodroidAddReference(Ljava/lang/Object;)V
    .locals 1

    .line 65
    iget-object v0, p0, Lcrc64720bb2db43a66fe9/ShellFragmentContainer;->refList:Ljava/util/ArrayList;

    if-nez v0, :cond_0

    .line 66
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    iput-object v0, p0, Lcrc64720bb2db43a66fe9/ShellFragmentContainer;->refList:Ljava/util/ArrayList;

    .line 67
    :cond_0
    iget-object v0, p0, Lcrc64720bb2db43a66fe9/ShellFragmentContainer;->refList:Ljava/util/ArrayList;

    invoke-virtual {v0, p1}, Ljava/util/ArrayList;->add(Ljava/lang/Object;)Z

    .line 68
    return-void
.end method

.method public monodroidClearReferences()V
    .locals 1

    .line 72
    iget-object v0, p0, Lcrc64720bb2db43a66fe9/ShellFragmentContainer;->refList:Ljava/util/ArrayList;

    if-eqz v0, :cond_0

    .line 73
    invoke-virtual {v0}, Ljava/util/ArrayList;->clear()V

    .line 74
    :cond_0
    return-void
.end method

.method public onCreateView(Landroid/view/LayoutInflater;Landroid/view/ViewGroup;Landroid/os/Bundle;)Landroid/view/View;
    .locals 0

    .line 41
    invoke-direct {p0, p1, p2, p3}, Lcrc64720bb2db43a66fe9/ShellFragmentContainer;->n_onCreateView(Landroid/view/LayoutInflater;Landroid/view/ViewGroup;Landroid/os/Bundle;)Landroid/view/View;

    move-result-object p1

    return-object p1
.end method

.method public onDestroy()V
    .locals 0

    .line 57
    invoke-direct {p0}, Lcrc64720bb2db43a66fe9/ShellFragmentContainer;->n_onDestroy()V

    .line 58
    return-void
.end method

.method public onDestroyView()V
    .locals 0

    .line 49
    invoke-direct {p0}, Lcrc64720bb2db43a66fe9/ShellFragmentContainer;->n_onDestroyView()V

    .line 50
    return-void
.end method
