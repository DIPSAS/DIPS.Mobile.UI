.class public Lcrc64720bb2db43a66fe9/FragmentContainer;
.super Landroidx/fragment/app/Fragment;
.source "FragmentContainer.java"

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
    const-string v0, "n_onCreateView:(Landroid/view/LayoutInflater;Landroid/view/ViewGroup;Landroid/os/Bundle;)Landroid/view/View;:GetOnCreateView_Landroid_view_LayoutInflater_Landroid_view_ViewGroup_Landroid_os_Bundle_Handler\nn_onDestroyView:()V:GetOnDestroyViewHandler\nn_onHiddenChanged:(Z)V:GetOnHiddenChanged_ZHandler\nn_onPause:()V:GetOnPauseHandler\nn_onResume:()V:GetOnResumeHandler\n"

    sput-object v0, Lcrc64720bb2db43a66fe9/FragmentContainer;->__md_methods:Ljava/lang/String;

    .line 19
    const-class v1, Lcrc64720bb2db43a66fe9/FragmentContainer;

    const-string v2, "Xamarin.Forms.Platform.Android.AppCompat.FragmentContainer, Xamarin.Forms.Platform.Android"

    invoke-static {v2, v1, v0}, Lmono/android/Runtime;->register(Ljava/lang/String;Ljava/lang/Class;Ljava/lang/String;)V

    .line 20
    return-void
.end method

.method public constructor <init>()V
    .locals 3

    .line 25
    invoke-direct {p0}, Landroidx/fragment/app/Fragment;-><init>()V

    .line 26
    invoke-virtual {p0}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    move-result-object v0

    const-class v1, Lcrc64720bb2db43a66fe9/FragmentContainer;

    if-ne v0, v1, :cond_0

    .line 27
    const/4 v0, 0x0

    new-array v0, v0, [Ljava/lang/Object;

    const-string v1, "Xamarin.Forms.Platform.Android.AppCompat.FragmentContainer, Xamarin.Forms.Platform.Android"

    const-string v2, ""

    invoke-static {v1, v2, p0, v0}, Lmono/android/TypeManager;->Activate(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Object;[Ljava/lang/Object;)V

    .line 29
    :cond_0
    return-void
.end method

.method public constructor <init>(I)V
    .locals 2

    .line 34
    invoke-direct {p0, p1}, Landroidx/fragment/app/Fragment;-><init>(I)V

    .line 35
    invoke-virtual {p0}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    move-result-object v0

    const-class v1, Lcrc64720bb2db43a66fe9/FragmentContainer;

    if-ne v0, v1, :cond_0

    .line 36
    const/4 v0, 0x1

    new-array v0, v0, [Ljava/lang/Object;

    const/4 v1, 0x0

    invoke-static {p1}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object p1

    aput-object p1, v0, v1

    const-string p1, "Xamarin.Forms.Platform.Android.AppCompat.FragmentContainer, Xamarin.Forms.Platform.Android"

    const-string v1, "System.Int32, mscorlib"

    invoke-static {p1, v1, p0, v0}, Lmono/android/TypeManager;->Activate(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Object;[Ljava/lang/Object;)V

    .line 38
    :cond_0
    return-void
.end method

.method private native n_onCreateView(Landroid/view/LayoutInflater;Landroid/view/ViewGroup;Landroid/os/Bundle;)Landroid/view/View;
.end method

.method private native n_onDestroyView()V
.end method

.method private native n_onHiddenChanged(Z)V
.end method

.method private native n_onPause()V
.end method

.method private native n_onResume()V
.end method


# virtual methods
.method public monodroidAddReference(Ljava/lang/Object;)V
    .locals 1

    .line 83
    iget-object v0, p0, Lcrc64720bb2db43a66fe9/FragmentContainer;->refList:Ljava/util/ArrayList;

    if-nez v0, :cond_0

    .line 84
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    iput-object v0, p0, Lcrc64720bb2db43a66fe9/FragmentContainer;->refList:Ljava/util/ArrayList;

    .line 85
    :cond_0
    iget-object v0, p0, Lcrc64720bb2db43a66fe9/FragmentContainer;->refList:Ljava/util/ArrayList;

    invoke-virtual {v0, p1}, Ljava/util/ArrayList;->add(Ljava/lang/Object;)Z

    .line 86
    return-void
.end method

.method public monodroidClearReferences()V
    .locals 1

    .line 90
    iget-object v0, p0, Lcrc64720bb2db43a66fe9/FragmentContainer;->refList:Ljava/util/ArrayList;

    if-eqz v0, :cond_0

    .line 91
    invoke-virtual {v0}, Ljava/util/ArrayList;->clear()V

    .line 92
    :cond_0
    return-void
.end method

.method public onCreateView(Landroid/view/LayoutInflater;Landroid/view/ViewGroup;Landroid/os/Bundle;)Landroid/view/View;
    .locals 0

    .line 43
    invoke-direct {p0, p1, p2, p3}, Lcrc64720bb2db43a66fe9/FragmentContainer;->n_onCreateView(Landroid/view/LayoutInflater;Landroid/view/ViewGroup;Landroid/os/Bundle;)Landroid/view/View;

    move-result-object p1

    return-object p1
.end method

.method public onDestroyView()V
    .locals 0

    .line 51
    invoke-direct {p0}, Lcrc64720bb2db43a66fe9/FragmentContainer;->n_onDestroyView()V

    .line 52
    return-void
.end method

.method public onHiddenChanged(Z)V
    .locals 0

    .line 59
    invoke-direct {p0, p1}, Lcrc64720bb2db43a66fe9/FragmentContainer;->n_onHiddenChanged(Z)V

    .line 60
    return-void
.end method

.method public onPause()V
    .locals 0

    .line 67
    invoke-direct {p0}, Lcrc64720bb2db43a66fe9/FragmentContainer;->n_onPause()V

    .line 68
    return-void
.end method

.method public onResume()V
    .locals 0

    .line 75
    invoke-direct {p0}, Lcrc64720bb2db43a66fe9/FragmentContainer;->n_onResume()V

    .line 76
    return-void
.end method
