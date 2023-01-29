.class public Lmono/androidx/appcompat/app/ActionBar_TabListenerImplementor;
.super Ljava/lang/Object;
.source "ActionBar_TabListenerImplementor.java"

# interfaces
.implements Lmono/android/IGCUserPeer;
.implements Landroidx/appcompat/app/ActionBar$TabListener;


# static fields
.field public static final __md_methods:Ljava/lang/String;


# instance fields
.field private refList:Ljava/util/ArrayList;


# direct methods
.method static constructor <clinit>()V
    .locals 3

    .line 13
    const-string v0, "n_onTabReselected:(Landroidx/appcompat/app/ActionBar$Tab;Landroidx/fragment/app/FragmentTransaction;)V:GetOnTabReselected_Landroidx_appcompat_app_ActionBar_Tab_Landroidx_fragment_app_FragmentTransaction_Handler:AndroidX.AppCompat.App.ActionBar/ITabListenerInvoker, Xamarin.AndroidX.AppCompat\nn_onTabSelected:(Landroidx/appcompat/app/ActionBar$Tab;Landroidx/fragment/app/FragmentTransaction;)V:GetOnTabSelected_Landroidx_appcompat_app_ActionBar_Tab_Landroidx_fragment_app_FragmentTransaction_Handler:AndroidX.AppCompat.App.ActionBar/ITabListenerInvoker, Xamarin.AndroidX.AppCompat\nn_onTabUnselected:(Landroidx/appcompat/app/ActionBar$Tab;Landroidx/fragment/app/FragmentTransaction;)V:GetOnTabUnselected_Landroidx_appcompat_app_ActionBar_Tab_Landroidx_fragment_app_FragmentTransaction_Handler:AndroidX.AppCompat.App.ActionBar/ITabListenerInvoker, Xamarin.AndroidX.AppCompat\n"

    sput-object v0, Lmono/androidx/appcompat/app/ActionBar_TabListenerImplementor;->__md_methods:Ljava/lang/String;

    .line 18
    const-class v1, Lmono/androidx/appcompat/app/ActionBar_TabListenerImplementor;

    const-string v2, "AndroidX.AppCompat.App.ActionBar+ITabListenerImplementor, Xamarin.AndroidX.AppCompat"

    invoke-static {v2, v1, v0}, Lmono/android/Runtime;->register(Ljava/lang/String;Ljava/lang/Class;Ljava/lang/String;)V

    .line 19
    return-void
.end method

.method public constructor <init>()V
    .locals 3

    .line 24
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 25
    invoke-virtual {p0}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    move-result-object v0

    const-class v1, Lmono/androidx/appcompat/app/ActionBar_TabListenerImplementor;

    if-ne v0, v1, :cond_0

    .line 26
    const/4 v0, 0x0

    new-array v0, v0, [Ljava/lang/Object;

    const-string v1, "AndroidX.AppCompat.App.ActionBar+ITabListenerImplementor, Xamarin.AndroidX.AppCompat"

    const-string v2, ""

    invoke-static {v1, v2, p0, v0}, Lmono/android/TypeManager;->Activate(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Object;[Ljava/lang/Object;)V

    .line 28
    :cond_0
    return-void
.end method

.method private native n_onTabReselected(Landroidx/appcompat/app/ActionBar$Tab;Landroidx/fragment/app/FragmentTransaction;)V
.end method

.method private native n_onTabSelected(Landroidx/appcompat/app/ActionBar$Tab;Landroidx/fragment/app/FragmentTransaction;)V
.end method

.method private native n_onTabUnselected(Landroidx/appcompat/app/ActionBar$Tab;Landroidx/fragment/app/FragmentTransaction;)V
.end method


# virtual methods
.method public monodroidAddReference(Ljava/lang/Object;)V
    .locals 1

    .line 57
    iget-object v0, p0, Lmono/androidx/appcompat/app/ActionBar_TabListenerImplementor;->refList:Ljava/util/ArrayList;

    if-nez v0, :cond_0

    .line 58
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    iput-object v0, p0, Lmono/androidx/appcompat/app/ActionBar_TabListenerImplementor;->refList:Ljava/util/ArrayList;

    .line 59
    :cond_0
    iget-object v0, p0, Lmono/androidx/appcompat/app/ActionBar_TabListenerImplementor;->refList:Ljava/util/ArrayList;

    invoke-virtual {v0, p1}, Ljava/util/ArrayList;->add(Ljava/lang/Object;)Z

    .line 60
    return-void
.end method

.method public monodroidClearReferences()V
    .locals 1

    .line 64
    iget-object v0, p0, Lmono/androidx/appcompat/app/ActionBar_TabListenerImplementor;->refList:Ljava/util/ArrayList;

    if-eqz v0, :cond_0

    .line 65
    invoke-virtual {v0}, Ljava/util/ArrayList;->clear()V

    .line 66
    :cond_0
    return-void
.end method

.method public onTabReselected(Landroidx/appcompat/app/ActionBar$Tab;Landroidx/fragment/app/FragmentTransaction;)V
    .locals 0

    .line 33
    invoke-direct {p0, p1, p2}, Lmono/androidx/appcompat/app/ActionBar_TabListenerImplementor;->n_onTabReselected(Landroidx/appcompat/app/ActionBar$Tab;Landroidx/fragment/app/FragmentTransaction;)V

    .line 34
    return-void
.end method

.method public onTabSelected(Landroidx/appcompat/app/ActionBar$Tab;Landroidx/fragment/app/FragmentTransaction;)V
    .locals 0

    .line 41
    invoke-direct {p0, p1, p2}, Lmono/androidx/appcompat/app/ActionBar_TabListenerImplementor;->n_onTabSelected(Landroidx/appcompat/app/ActionBar$Tab;Landroidx/fragment/app/FragmentTransaction;)V

    .line 42
    return-void
.end method

.method public onTabUnselected(Landroidx/appcompat/app/ActionBar$Tab;Landroidx/fragment/app/FragmentTransaction;)V
    .locals 0

    .line 49
    invoke-direct {p0, p1, p2}, Lmono/androidx/appcompat/app/ActionBar_TabListenerImplementor;->n_onTabUnselected(Landroidx/appcompat/app/ActionBar$Tab;Landroidx/fragment/app/FragmentTransaction;)V

    .line 50
    return-void
.end method
