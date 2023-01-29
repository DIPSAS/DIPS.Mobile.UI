.class public Lmono/android/net/nsd/NsdManager_DiscoveryListenerImplementor;
.super Ljava/lang/Object;
.source "NsdManager_DiscoveryListenerImplementor.java"

# interfaces
.implements Lmono/android/IGCUserPeer;
.implements Landroid/net/nsd/NsdManager$DiscoveryListener;


# static fields
.field public static final __md_methods:Ljava/lang/String;


# instance fields
.field private refList:Ljava/util/ArrayList;


# direct methods
.method static constructor <clinit>()V
    .locals 3

    .line 13
    const-string v0, "n_onDiscoveryStarted:(Ljava/lang/String;)V:GetOnDiscoveryStarted_Ljava_lang_String_Handler:Android.Net.Nsd.NsdManager/IDiscoveryListenerInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\nn_onDiscoveryStopped:(Ljava/lang/String;)V:GetOnDiscoveryStopped_Ljava_lang_String_Handler:Android.Net.Nsd.NsdManager/IDiscoveryListenerInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\nn_onServiceFound:(Landroid/net/nsd/NsdServiceInfo;)V:GetOnServiceFound_Landroid_net_nsd_NsdServiceInfo_Handler:Android.Net.Nsd.NsdManager/IDiscoveryListenerInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\nn_onServiceLost:(Landroid/net/nsd/NsdServiceInfo;)V:GetOnServiceLost_Landroid_net_nsd_NsdServiceInfo_Handler:Android.Net.Nsd.NsdManager/IDiscoveryListenerInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\nn_onStartDiscoveryFailed:(Ljava/lang/String;I)V:GetOnStartDiscoveryFailed_Ljava_lang_String_IHandler:Android.Net.Nsd.NsdManager/IDiscoveryListenerInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\nn_onStopDiscoveryFailed:(Ljava/lang/String;I)V:GetOnStopDiscoveryFailed_Ljava_lang_String_IHandler:Android.Net.Nsd.NsdManager/IDiscoveryListenerInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n"

    sput-object v0, Lmono/android/net/nsd/NsdManager_DiscoveryListenerImplementor;->__md_methods:Ljava/lang/String;

    .line 21
    const-class v1, Lmono/android/net/nsd/NsdManager_DiscoveryListenerImplementor;

    const-string v2, "Android.Net.Nsd.NsdManager+IDiscoveryListenerImplementor, Mono.Android"

    invoke-static {v2, v1, v0}, Lmono/android/Runtime;->register(Ljava/lang/String;Ljava/lang/Class;Ljava/lang/String;)V

    .line 22
    return-void
.end method

.method public constructor <init>()V
    .locals 3

    .line 27
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 28
    invoke-virtual {p0}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    move-result-object v0

    const-class v1, Lmono/android/net/nsd/NsdManager_DiscoveryListenerImplementor;

    if-ne v0, v1, :cond_0

    .line 29
    const/4 v0, 0x0

    new-array v0, v0, [Ljava/lang/Object;

    const-string v1, "Android.Net.Nsd.NsdManager+IDiscoveryListenerImplementor, Mono.Android"

    const-string v2, ""

    invoke-static {v1, v2, p0, v0}, Lmono/android/TypeManager;->Activate(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Object;[Ljava/lang/Object;)V

    .line 31
    :cond_0
    return-void
.end method

.method private native n_onDiscoveryStarted(Ljava/lang/String;)V
.end method

.method private native n_onDiscoveryStopped(Ljava/lang/String;)V
.end method

.method private native n_onServiceFound(Landroid/net/nsd/NsdServiceInfo;)V
.end method

.method private native n_onServiceLost(Landroid/net/nsd/NsdServiceInfo;)V
.end method

.method private native n_onStartDiscoveryFailed(Ljava/lang/String;I)V
.end method

.method private native n_onStopDiscoveryFailed(Ljava/lang/String;I)V
.end method


# virtual methods
.method public monodroidAddReference(Ljava/lang/Object;)V
    .locals 1

    .line 84
    iget-object v0, p0, Lmono/android/net/nsd/NsdManager_DiscoveryListenerImplementor;->refList:Ljava/util/ArrayList;

    if-nez v0, :cond_0

    .line 85
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    iput-object v0, p0, Lmono/android/net/nsd/NsdManager_DiscoveryListenerImplementor;->refList:Ljava/util/ArrayList;

    .line 86
    :cond_0
    iget-object v0, p0, Lmono/android/net/nsd/NsdManager_DiscoveryListenerImplementor;->refList:Ljava/util/ArrayList;

    invoke-virtual {v0, p1}, Ljava/util/ArrayList;->add(Ljava/lang/Object;)Z

    .line 87
    return-void
.end method

.method public monodroidClearReferences()V
    .locals 1

    .line 91
    iget-object v0, p0, Lmono/android/net/nsd/NsdManager_DiscoveryListenerImplementor;->refList:Ljava/util/ArrayList;

    if-eqz v0, :cond_0

    .line 92
    invoke-virtual {v0}, Ljava/util/ArrayList;->clear()V

    .line 93
    :cond_0
    return-void
.end method

.method public onDiscoveryStarted(Ljava/lang/String;)V
    .locals 0

    .line 36
    invoke-direct {p0, p1}, Lmono/android/net/nsd/NsdManager_DiscoveryListenerImplementor;->n_onDiscoveryStarted(Ljava/lang/String;)V

    .line 37
    return-void
.end method

.method public onDiscoveryStopped(Ljava/lang/String;)V
    .locals 0

    .line 44
    invoke-direct {p0, p1}, Lmono/android/net/nsd/NsdManager_DiscoveryListenerImplementor;->n_onDiscoveryStopped(Ljava/lang/String;)V

    .line 45
    return-void
.end method

.method public onServiceFound(Landroid/net/nsd/NsdServiceInfo;)V
    .locals 0

    .line 52
    invoke-direct {p0, p1}, Lmono/android/net/nsd/NsdManager_DiscoveryListenerImplementor;->n_onServiceFound(Landroid/net/nsd/NsdServiceInfo;)V

    .line 53
    return-void
.end method

.method public onServiceLost(Landroid/net/nsd/NsdServiceInfo;)V
    .locals 0

    .line 60
    invoke-direct {p0, p1}, Lmono/android/net/nsd/NsdManager_DiscoveryListenerImplementor;->n_onServiceLost(Landroid/net/nsd/NsdServiceInfo;)V

    .line 61
    return-void
.end method

.method public onStartDiscoveryFailed(Ljava/lang/String;I)V
    .locals 0

    .line 68
    invoke-direct {p0, p1, p2}, Lmono/android/net/nsd/NsdManager_DiscoveryListenerImplementor;->n_onStartDiscoveryFailed(Ljava/lang/String;I)V

    .line 69
    return-void
.end method

.method public onStopDiscoveryFailed(Ljava/lang/String;I)V
    .locals 0

    .line 76
    invoke-direct {p0, p1, p2}, Lmono/android/net/nsd/NsdManager_DiscoveryListenerImplementor;->n_onStopDiscoveryFailed(Ljava/lang/String;I)V

    .line 77
    return-void
.end method
