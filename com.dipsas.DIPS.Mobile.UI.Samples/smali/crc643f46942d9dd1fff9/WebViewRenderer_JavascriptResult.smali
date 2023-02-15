.class public Lcrc643f46942d9dd1fff9/WebViewRenderer_JavascriptResult;
.super Ljava/lang/Object;
.source "WebViewRenderer_JavascriptResult.java"

# interfaces
.implements Lmono/android/IGCUserPeer;
.implements Landroid/webkit/ValueCallback;


# static fields
.field public static final __md_methods:Ljava/lang/String;


# instance fields
.field private refList:Ljava/util/ArrayList;


# direct methods
.method static constructor <clinit>()V
    .locals 3

    .line 13
    const-string v0, "n_onReceiveValue:(Ljava/lang/Object;)V:GetOnReceiveValue_Ljava_lang_Object_Handler:Android.Webkit.IValueCallbackInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n"

    sput-object v0, Lcrc643f46942d9dd1fff9/WebViewRenderer_JavascriptResult;->__md_methods:Ljava/lang/String;

    .line 16
    const-class v1, Lcrc643f46942d9dd1fff9/WebViewRenderer_JavascriptResult;

    const-string v2, "Xamarin.Forms.Platform.Android.WebViewRenderer+JavascriptResult, Xamarin.Forms.Platform.Android"

    invoke-static {v2, v1, v0}, Lmono/android/Runtime;->register(Ljava/lang/String;Ljava/lang/Class;Ljava/lang/String;)V

    .line 17
    return-void
.end method

.method public constructor <init>()V
    .locals 3

    .line 22
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 23
    invoke-virtual {p0}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    move-result-object v0

    const-class v1, Lcrc643f46942d9dd1fff9/WebViewRenderer_JavascriptResult;

    if-ne v0, v1, :cond_0

    .line 24
    const/4 v0, 0x0

    new-array v0, v0, [Ljava/lang/Object;

    const-string v1, "Xamarin.Forms.Platform.Android.WebViewRenderer+JavascriptResult, Xamarin.Forms.Platform.Android"

    const-string v2, ""

    invoke-static {v1, v2, p0, v0}, Lmono/android/TypeManager;->Activate(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Object;[Ljava/lang/Object;)V

    .line 26
    :cond_0
    return-void
.end method

.method private native n_onReceiveValue(Ljava/lang/Object;)V
.end method


# virtual methods
.method public monodroidAddReference(Ljava/lang/Object;)V
    .locals 1

    .line 39
    iget-object v0, p0, Lcrc643f46942d9dd1fff9/WebViewRenderer_JavascriptResult;->refList:Ljava/util/ArrayList;

    if-nez v0, :cond_0

    .line 40
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    iput-object v0, p0, Lcrc643f46942d9dd1fff9/WebViewRenderer_JavascriptResult;->refList:Ljava/util/ArrayList;

    .line 41
    :cond_0
    iget-object v0, p0, Lcrc643f46942d9dd1fff9/WebViewRenderer_JavascriptResult;->refList:Ljava/util/ArrayList;

    invoke-virtual {v0, p1}, Ljava/util/ArrayList;->add(Ljava/lang/Object;)Z

    .line 42
    return-void
.end method

.method public monodroidClearReferences()V
    .locals 1

    .line 46
    iget-object v0, p0, Lcrc643f46942d9dd1fff9/WebViewRenderer_JavascriptResult;->refList:Ljava/util/ArrayList;

    if-eqz v0, :cond_0

    .line 47
    invoke-virtual {v0}, Ljava/util/ArrayList;->clear()V

    .line 48
    :cond_0
    return-void
.end method

.method public onReceiveValue(Ljava/lang/Object;)V
    .locals 0

    .line 31
    invoke-direct {p0, p1}, Lcrc643f46942d9dd1fff9/WebViewRenderer_JavascriptResult;->n_onReceiveValue(Ljava/lang/Object;)V

    .line 32
    return-void
.end method
