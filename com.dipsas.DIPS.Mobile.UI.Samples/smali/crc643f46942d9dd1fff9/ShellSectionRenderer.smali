.class public Lcrc643f46942d9dd1fff9/ShellSectionRenderer;
.super Landroidx/fragment/app/Fragment;
.source "ShellSectionRenderer.java"

# interfaces
.implements Lmono/android/IGCUserPeer;
.implements Landroidx/viewpager/widget/ViewPager$OnPageChangeListener;
.implements Landroid/view/View$OnClickListener;


# static fields
.field public static final __md_methods:Ljava/lang/String;


# instance fields
.field private refList:Ljava/util/ArrayList;


# direct methods
.method static constructor <clinit>()V
    .locals 3

    .line 14
    const-string v0, "n_onCreateView:(Landroid/view/LayoutInflater;Landroid/view/ViewGroup;Landroid/os/Bundle;)Landroid/view/View;:GetOnCreateView_Landroid_view_LayoutInflater_Landroid_view_ViewGroup_Landroid_os_Bundle_Handler\nn_onDestroy:()V:GetOnDestroyHandler\nn_onPageScrollStateChanged:(I)V:GetOnPageScrollStateChanged_IHandler:AndroidX.ViewPager.Widget.ViewPager/IOnPageChangeListenerInvoker, Xamarin.AndroidX.ViewPager\nn_onPageScrolled:(IFI)V:GetOnPageScrolled_IFIHandler:AndroidX.ViewPager.Widget.ViewPager/IOnPageChangeListenerInvoker, Xamarin.AndroidX.ViewPager\nn_onPageSelected:(I)V:GetOnPageSelected_IHandler:AndroidX.ViewPager.Widget.ViewPager/IOnPageChangeListenerInvoker, Xamarin.AndroidX.ViewPager\nn_onClick:(Landroid/view/View;)V:GetOnClick_Landroid_view_View_Handler:Android.Views.View/IOnClickListenerInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n"

    sput-object v0, Lcrc643f46942d9dd1fff9/ShellSectionRenderer;->__md_methods:Ljava/lang/String;

    .line 22
    const-class v1, Lcrc643f46942d9dd1fff9/ShellSectionRenderer;

    const-string v2, "Xamarin.Forms.Platform.Android.ShellSectionRenderer, Xamarin.Forms.Platform.Android"

    invoke-static {v2, v1, v0}, Lmono/android/Runtime;->register(Ljava/lang/String;Ljava/lang/Class;Ljava/lang/String;)V

    .line 23
    return-void
.end method

.method public constructor <init>()V
    .locals 3

    .line 28
    invoke-direct {p0}, Landroidx/fragment/app/Fragment;-><init>()V

    .line 29
    invoke-virtual {p0}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    move-result-object v0

    const-class v1, Lcrc643f46942d9dd1fff9/ShellSectionRenderer;

    if-ne v0, v1, :cond_0

    .line 30
    const/4 v0, 0x0

    new-array v0, v0, [Ljava/lang/Object;

    const-string v1, "Xamarin.Forms.Platform.Android.ShellSectionRenderer, Xamarin.Forms.Platform.Android"

    const-string v2, ""

    invoke-static {v1, v2, p0, v0}, Lmono/android/TypeManager;->Activate(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Object;[Ljava/lang/Object;)V

    .line 32
    :cond_0
    return-void
.end method

.method public constructor <init>(I)V
    .locals 2

    .line 37
    invoke-direct {p0, p1}, Landroidx/fragment/app/Fragment;-><init>(I)V

    .line 38
    invoke-virtual {p0}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    move-result-object v0

    const-class v1, Lcrc643f46942d9dd1fff9/ShellSectionRenderer;

    if-ne v0, v1, :cond_0

    .line 39
    const/4 v0, 0x1

    new-array v0, v0, [Ljava/lang/Object;

    const/4 v1, 0x0

    invoke-static {p1}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object p1

    aput-object p1, v0, v1

    const-string p1, "Xamarin.Forms.Platform.Android.ShellSectionRenderer, Xamarin.Forms.Platform.Android"

    const-string v1, "System.Int32, mscorlib"

    invoke-static {p1, v1, p0, v0}, Lmono/android/TypeManager;->Activate(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Object;[Ljava/lang/Object;)V

    .line 41
    :cond_0
    return-void
.end method

.method private native n_onClick(Landroid/view/View;)V
.end method

.method private native n_onCreateView(Landroid/view/LayoutInflater;Landroid/view/ViewGroup;Landroid/os/Bundle;)Landroid/view/View;
.end method

.method private native n_onDestroy()V
.end method

.method private native n_onPageScrollStateChanged(I)V
.end method

.method private native n_onPageScrolled(IFI)V
.end method

.method private native n_onPageSelected(I)V
.end method


# virtual methods
.method public monodroidAddReference(Ljava/lang/Object;)V
    .locals 1

    .line 94
    iget-object v0, p0, Lcrc643f46942d9dd1fff9/ShellSectionRenderer;->refList:Ljava/util/ArrayList;

    if-nez v0, :cond_0

    .line 95
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    iput-object v0, p0, Lcrc643f46942d9dd1fff9/ShellSectionRenderer;->refList:Ljava/util/ArrayList;

    .line 96
    :cond_0
    iget-object v0, p0, Lcrc643f46942d9dd1fff9/ShellSectionRenderer;->refList:Ljava/util/ArrayList;

    invoke-virtual {v0, p1}, Ljava/util/ArrayList;->add(Ljava/lang/Object;)Z

    .line 97
    return-void
.end method

.method public monodroidClearReferences()V
    .locals 1

    .line 101
    iget-object v0, p0, Lcrc643f46942d9dd1fff9/ShellSectionRenderer;->refList:Ljava/util/ArrayList;

    if-eqz v0, :cond_0

    .line 102
    invoke-virtual {v0}, Ljava/util/ArrayList;->clear()V

    .line 103
    :cond_0
    return-void
.end method

.method public onClick(Landroid/view/View;)V
    .locals 0

    .line 86
    invoke-direct {p0, p1}, Lcrc643f46942d9dd1fff9/ShellSectionRenderer;->n_onClick(Landroid/view/View;)V

    .line 87
    return-void
.end method

.method public onCreateView(Landroid/view/LayoutInflater;Landroid/view/ViewGroup;Landroid/os/Bundle;)Landroid/view/View;
    .locals 0

    .line 46
    invoke-direct {p0, p1, p2, p3}, Lcrc643f46942d9dd1fff9/ShellSectionRenderer;->n_onCreateView(Landroid/view/LayoutInflater;Landroid/view/ViewGroup;Landroid/os/Bundle;)Landroid/view/View;

    move-result-object p1

    return-object p1
.end method

.method public onDestroy()V
    .locals 0

    .line 54
    invoke-direct {p0}, Lcrc643f46942d9dd1fff9/ShellSectionRenderer;->n_onDestroy()V

    .line 55
    return-void
.end method

.method public onPageScrollStateChanged(I)V
    .locals 0

    .line 62
    invoke-direct {p0, p1}, Lcrc643f46942d9dd1fff9/ShellSectionRenderer;->n_onPageScrollStateChanged(I)V

    .line 63
    return-void
.end method

.method public onPageScrolled(IFI)V
    .locals 0

    .line 70
    invoke-direct {p0, p1, p2, p3}, Lcrc643f46942d9dd1fff9/ShellSectionRenderer;->n_onPageScrolled(IFI)V

    .line 71
    return-void
.end method

.method public onPageSelected(I)V
    .locals 0

    .line 78
    invoke-direct {p0, p1}, Lcrc643f46942d9dd1fff9/ShellSectionRenderer;->n_onPageSelected(I)V

    .line 79
    return-void
.end method
