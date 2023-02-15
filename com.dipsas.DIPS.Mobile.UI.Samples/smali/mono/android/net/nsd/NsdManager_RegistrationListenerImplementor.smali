.class public Lmono/android/net/nsd/NsdManager_RegistrationListenerImplementor;
.super Ljava/lang/Object;
.source "NsdManager_RegistrationListenerImplementor.java"

# interfaces
.implements Lmono/android/IGCUserPeer;
.implements Landroid/net/nsd/NsdManager$RegistrationListener;


# static fields
.field public static final __md_methods:Ljava/lang/String;


# instance fields
.field private refList:Ljava/util/ArrayList;


# direct methods
.method static constructor <clinit>()V
    .locals 3

    .line 13
    const-string v0, "n_onRegistrationFailed:(Landroid/net/nsd/NsdServiceInfo;I)V:GetOnRegistrationFailed_Landroid_net_nsd_NsdServiceInfo_IHandler:Android.Net.Nsd.NsdManager/IRegistrationListenerInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\nn_onServiceRegistered:(Landroid/net/nsd/NsdServiceInfo;)V:GetOnServiceRegistered_Landroid_net_nsd_NsdServiceInfo_Handler:Android.Net.Nsd.NsdManager/IRegistrationListenerInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\nn_onServiceUnregistered:(Landroid/net/nsd/NsdServiceInfo;)V:GetOnServiceUnregistered_Landroid_net_nsd_NsdServiceInfo_Handler:Android.Net.Nsd.NsdManager/IRegistrationListenerInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\nn_onUnregistrationFailed:(Landroid/net/nsd/NsdServiceInfo;I)V:GetOnUnregistrationFailed_Landroid_net_nsd_NsdServiceInfo_IHandler:Android.Net.Nsd.NsdManager/IRegistrationListenerInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n"

    sput-object v0, Lmono/android/net/nsd/NsdManager_RegistrationListenerImplementor;->__md_methods:Ljava/lang/String;

    .line 19
    const-class v1, Lmono/android/net/nsd/NsdManager_RegistrationListenerImplementor;

    const-string v2, "Android.Net.Nsd.NsdManager+IRegistrationListenerImplementor, Mono.Android"

    invoke-static {v2, v1, v0}, Lmono/android/Runtime;->register(Ljava/lang/String;Ljava/lang/Class;Ljava/lang/String;)V

    .line 20
    return-void
.end method

.method public constructor <init>()V
    .locals 3

    .line 25
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 26
    invoke-virtual {p0}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    move-result-object v0

    const-class v1, Lmono/android/net/nsd/NsdManager_RegistrationListenerImplementor;

    if-ne v0, v1, :cond_0

    .line 27
    const/4 v0, 0x0

    new-array v0, v0, [Ljava/lang/Object;

    const-string v1, "Android.Net.Nsd.NsdManager+IRegistrationListenerImplementor, Mono.Android"

    const-string v2, ""

    invoke-static {v1, v2, p0, v0}, Lmono/android/TypeManager;->Activate(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Object;[Ljava/lang/Object;)V

    .line 29
    :cond_0
    return-void
.end method

.method private native n_onRegistrationFailed(Landroid/net/nsd/NsdServiceInfo;I)V
.end method

.method private native n_onServiceRegistered(Landroid/net/nsd/NsdServiceInfo;)V
.end method

.method private native n_onServiceUnregistered(Landroid/net/nsd/NsdServiceInfo;)V
.end method

.method private native n_onUnregistrationFailed(Landroid/net/nsd/NsdServiceInfo;I)V
.end method


# virtual methods
.method public monodroidAddReference(Ljava/lang/Object;)V
    .locals 1

    .line 66
    iget-object v0, p0, Lmono/android/net/nsd/NsdManager_RegistrationListenerImplementor;->refList:Ljava/util/ArrayList;

    if-nez v0, :cond_0

    .line 67
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    iput-object v0, p0, Lmono/android/net/nsd/NsdManager_RegistrationListenerImplementor;->refList:Ljava/util/ArrayList;

    .line 68
    :cond_0
    iget-object v0, p0, Lmono/android/net/nsd/NsdManager_RegistrationListenerImplementor;->refList:Ljava/util/ArrayList;

    invoke-virtual {v0, p1}, Ljava/util/ArrayList;->add(Ljava/lang/Object;)Z

    .line 69
    return-void
.end method

.method public monodroidClearReferences()V
    .locals 1

    .line 73
    iget-object v0, p0, Lmono/android/net/nsd/NsdManager_RegistrationListenerImplementor;->refList:Ljava/util/ArrayList;

    if-eqz v0, :cond_0

    .line 74
    invoke-virtual {v0}, Ljava/util/ArrayList;->clear()V

    .line 75
    :cond_0
    return-void
.end method

.method public onRegistrationFailed(Landroid/net/nsd/NsdServiceInfo;I)V
    .locals 0

    .line 34
    invoke-direct {p0, p1, p2}, Lmono/android/net/nsd/NsdManager_RegistrationListenerImplementor;->n_onRegistrationFailed(Landroid/net/nsd/NsdServiceInfo;I)V

    .line 35
    return-void
.end method

.method public onServiceRegistered(Landroid/net/nsd/NsdServiceInfo;)V
    .locals 0

    .line 42
    invoke-direct {p0, p1}, Lmono/android/net/nsd/NsdManager_RegistrationListenerImplementor;->n_onServiceRegistered(Landroid/net/nsd/NsdServiceInfo;)V

    .line 43
    return-void
.end method

.method public onServiceUnregistered(Landroid/net/nsd/NsdServiceInfo;)V
    .locals 0

    .line 50
    invoke-direct {p0, p1}, Lmono/android/net/nsd/NsdManager_RegistrationListenerImplementor;->n_onServiceUnregistered(Landroid/net/nsd/NsdServiceInfo;)V

    .line 51
    return-void
.end method

.method public onUnregistrationFailed(Landroid/net/nsd/NsdServiceInfo;I)V
    .locals 0

    .line 58
    invoke-direct {p0, p1, p2}, Lmono/android/net/nsd/NsdManager_RegistrationListenerImplementor;->n_onUnregistrationFailed(Landroid/net/nsd/NsdServiceInfo;I)V

    .line 59
    return-void
.end method
