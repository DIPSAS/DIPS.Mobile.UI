.class public Landroidx/browser/customtabs/CustomTabsClient_CustomTabsCallbackImpl;
.super Landroidx/browser/customtabs/CustomTabsCallback;
.source "CustomTabsClient_CustomTabsCallbackImpl.java"

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
    const-string v0, "n_onNavigationEvent:(ILandroid/os/Bundle;)V:GetOnNavigationEvent_ILandroid_os_Bundle_Handler\nn_extraCallback:(Ljava/lang/String;Landroid/os/Bundle;)V:GetExtraCallback_Ljava_lang_String_Landroid_os_Bundle_Handler\n"

    sput-object v0, Landroidx/browser/customtabs/CustomTabsClient_CustomTabsCallbackImpl;->__md_methods:Ljava/lang/String;

    .line 16
    const-class v1, Landroidx/browser/customtabs/CustomTabsClient_CustomTabsCallbackImpl;

    const-string v2, "AndroidX.Browser.CustomTabs.CustomTabsClient+CustomTabsCallbackImpl, Xamarin.AndroidX.Browser"

    invoke-static {v2, v1, v0}, Lmono/android/Runtime;->register(Ljava/lang/String;Ljava/lang/Class;Ljava/lang/String;)V

    .line 17
    return-void
.end method

.method public constructor <init>()V
    .locals 3

    .line 22
    invoke-direct {p0}, Landroidx/browser/customtabs/CustomTabsCallback;-><init>()V

    .line 23
    invoke-virtual {p0}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    move-result-object v0

    const-class v1, Landroidx/browser/customtabs/CustomTabsClient_CustomTabsCallbackImpl;

    if-ne v0, v1, :cond_0

    .line 24
    const/4 v0, 0x0

    new-array v0, v0, [Ljava/lang/Object;

    const-string v1, "AndroidX.Browser.CustomTabs.CustomTabsClient+CustomTabsCallbackImpl, Xamarin.AndroidX.Browser"

    const-string v2, ""

    invoke-static {v1, v2, p0, v0}, Lmono/android/TypeManager;->Activate(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Object;[Ljava/lang/Object;)V

    .line 26
    :cond_0
    return-void
.end method

.method private native n_extraCallback(Ljava/lang/String;Landroid/os/Bundle;)V
.end method

.method private native n_onNavigationEvent(ILandroid/os/Bundle;)V
.end method


# virtual methods
.method public extraCallback(Ljava/lang/String;Landroid/os/Bundle;)V
    .locals 0

    .line 39
    invoke-direct {p0, p1, p2}, Landroidx/browser/customtabs/CustomTabsClient_CustomTabsCallbackImpl;->n_extraCallback(Ljava/lang/String;Landroid/os/Bundle;)V

    .line 40
    return-void
.end method

.method public monodroidAddReference(Ljava/lang/Object;)V
    .locals 1

    .line 47
    iget-object v0, p0, Landroidx/browser/customtabs/CustomTabsClient_CustomTabsCallbackImpl;->refList:Ljava/util/ArrayList;

    if-nez v0, :cond_0

    .line 48
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    iput-object v0, p0, Landroidx/browser/customtabs/CustomTabsClient_CustomTabsCallbackImpl;->refList:Ljava/util/ArrayList;

    .line 49
    :cond_0
    iget-object v0, p0, Landroidx/browser/customtabs/CustomTabsClient_CustomTabsCallbackImpl;->refList:Ljava/util/ArrayList;

    invoke-virtual {v0, p1}, Ljava/util/ArrayList;->add(Ljava/lang/Object;)Z

    .line 50
    return-void
.end method

.method public monodroidClearReferences()V
    .locals 1

    .line 54
    iget-object v0, p0, Landroidx/browser/customtabs/CustomTabsClient_CustomTabsCallbackImpl;->refList:Ljava/util/ArrayList;

    if-eqz v0, :cond_0

    .line 55
    invoke-virtual {v0}, Ljava/util/ArrayList;->clear()V

    .line 56
    :cond_0
    return-void
.end method

.method public onNavigationEvent(ILandroid/os/Bundle;)V
    .locals 0

    .line 31
    invoke-direct {p0, p1, p2}, Landroidx/browser/customtabs/CustomTabsClient_CustomTabsCallbackImpl;->n_onNavigationEvent(ILandroid/os/Bundle;)V

    .line 32
    return-void
.end method
