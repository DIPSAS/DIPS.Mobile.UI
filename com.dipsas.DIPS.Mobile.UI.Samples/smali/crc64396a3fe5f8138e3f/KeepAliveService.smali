.class public Lcrc64396a3fe5f8138e3f/KeepAliveService;
.super Landroid/app/Service;
.source "KeepAliveService.java"

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
    const-string v0, "n_onBind:(Landroid/content/Intent;)Landroid/os/IBinder;:GetOnBind_Landroid_content_Intent_Handler\n"

    sput-object v0, Lcrc64396a3fe5f8138e3f/KeepAliveService;->__md_methods:Ljava/lang/String;

    .line 15
    const-class v1, Lcrc64396a3fe5f8138e3f/KeepAliveService;

    const-string v2, "AndroidX.Browser.CustomTabs.KeepAliveService, Xamarin.AndroidX.Browser"

    invoke-static {v2, v1, v0}, Lmono/android/Runtime;->register(Ljava/lang/String;Ljava/lang/Class;Ljava/lang/String;)V

    .line 16
    return-void
.end method

.method public constructor <init>()V
    .locals 3

    .line 21
    invoke-direct {p0}, Landroid/app/Service;-><init>()V

    .line 22
    invoke-virtual {p0}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    move-result-object v0

    const-class v1, Lcrc64396a3fe5f8138e3f/KeepAliveService;

    if-ne v0, v1, :cond_0

    .line 23
    const/4 v0, 0x0

    new-array v0, v0, [Ljava/lang/Object;

    const-string v1, "AndroidX.Browser.CustomTabs.KeepAliveService, Xamarin.AndroidX.Browser"

    const-string v2, ""

    invoke-static {v1, v2, p0, v0}, Lmono/android/TypeManager;->Activate(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Object;[Ljava/lang/Object;)V

    .line 25
    :cond_0
    return-void
.end method

.method private native n_onBind(Landroid/content/Intent;)Landroid/os/IBinder;
.end method


# virtual methods
.method public monodroidAddReference(Ljava/lang/Object;)V
    .locals 1

    .line 38
    iget-object v0, p0, Lcrc64396a3fe5f8138e3f/KeepAliveService;->refList:Ljava/util/ArrayList;

    if-nez v0, :cond_0

    .line 39
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    iput-object v0, p0, Lcrc64396a3fe5f8138e3f/KeepAliveService;->refList:Ljava/util/ArrayList;

    .line 40
    :cond_0
    iget-object v0, p0, Lcrc64396a3fe5f8138e3f/KeepAliveService;->refList:Ljava/util/ArrayList;

    invoke-virtual {v0, p1}, Ljava/util/ArrayList;->add(Ljava/lang/Object;)Z

    .line 41
    return-void
.end method

.method public monodroidClearReferences()V
    .locals 1

    .line 45
    iget-object v0, p0, Lcrc64396a3fe5f8138e3f/KeepAliveService;->refList:Ljava/util/ArrayList;

    if-eqz v0, :cond_0

    .line 46
    invoke-virtual {v0}, Ljava/util/ArrayList;->clear()V

    .line 47
    :cond_0
    return-void
.end method

.method public onBind(Landroid/content/Intent;)Landroid/os/IBinder;
    .locals 0

    .line 30
    invoke-direct {p0, p1}, Lcrc64396a3fe5f8138e3f/KeepAliveService;->n_onBind(Landroid/content/Intent;)Landroid/os/IBinder;

    move-result-object p1

    return-object p1
.end method
