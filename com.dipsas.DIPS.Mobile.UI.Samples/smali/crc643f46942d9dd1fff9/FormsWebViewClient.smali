.class public Lcrc643f46942d9dd1fff9/FormsWebViewClient;
.super Landroid/webkit/WebViewClient;
.source "FormsWebViewClient.java"

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
    const-string v0, "n_shouldOverrideUrlLoading:(Landroid/webkit/WebView;Ljava/lang/String;)Z:GetShouldOverrideUrlLoading_Landroid_webkit_WebView_Ljava_lang_String_Handler\nn_shouldOverrideUrlLoading:(Landroid/webkit/WebView;Landroid/webkit/WebResourceRequest;)Z:GetShouldOverrideUrlLoading_Landroid_webkit_WebView_Landroid_webkit_WebResourceRequest_Handler\nn_onPageStarted:(Landroid/webkit/WebView;Ljava/lang/String;Landroid/graphics/Bitmap;)V:GetOnPageStarted_Landroid_webkit_WebView_Ljava_lang_String_Landroid_graphics_Bitmap_Handler\nn_onPageFinished:(Landroid/webkit/WebView;Ljava/lang/String;)V:GetOnPageFinished_Landroid_webkit_WebView_Ljava_lang_String_Handler\nn_onReceivedError:(Landroid/webkit/WebView;ILjava/lang/String;Ljava/lang/String;)V:GetOnReceivedError_Landroid_webkit_WebView_ILjava_lang_String_Ljava_lang_String_Handler\nn_onReceivedError:(Landroid/webkit/WebView;Landroid/webkit/WebResourceRequest;Landroid/webkit/WebResourceError;)V:GetOnReceivedError_Landroid_webkit_WebView_Landroid_webkit_WebResourceRequest_Landroid_webkit_WebResourceError_Handler\n"

    sput-object v0, Lcrc643f46942d9dd1fff9/FormsWebViewClient;->__md_methods:Ljava/lang/String;

    .line 20
    const-class v1, Lcrc643f46942d9dd1fff9/FormsWebViewClient;

    const-string v2, "Xamarin.Forms.Platform.Android.FormsWebViewClient, Xamarin.Forms.Platform.Android"

    invoke-static {v2, v1, v0}, Lmono/android/Runtime;->register(Ljava/lang/String;Ljava/lang/Class;Ljava/lang/String;)V

    .line 21
    return-void
.end method

.method public constructor <init>()V
    .locals 3

    .line 26
    invoke-direct {p0}, Landroid/webkit/WebViewClient;-><init>()V

    .line 27
    invoke-virtual {p0}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    move-result-object v0

    const-class v1, Lcrc643f46942d9dd1fff9/FormsWebViewClient;

    if-ne v0, v1, :cond_0

    .line 28
    const/4 v0, 0x0

    new-array v0, v0, [Ljava/lang/Object;

    const-string v1, "Xamarin.Forms.Platform.Android.FormsWebViewClient, Xamarin.Forms.Platform.Android"

    const-string v2, ""

    invoke-static {v1, v2, p0, v0}, Lmono/android/TypeManager;->Activate(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Object;[Ljava/lang/Object;)V

    .line 30
    :cond_0
    return-void
.end method

.method public constructor <init>(Lcrc643f46942d9dd1fff9/WebViewRenderer;)V
    .locals 2

    .line 34
    invoke-direct {p0}, Landroid/webkit/WebViewClient;-><init>()V

    .line 35
    invoke-virtual {p0}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    move-result-object v0

    const-class v1, Lcrc643f46942d9dd1fff9/FormsWebViewClient;

    if-ne v0, v1, :cond_0

    .line 36
    const/4 v0, 0x1

    new-array v0, v0, [Ljava/lang/Object;

    const/4 v1, 0x0

    aput-object p1, v0, v1

    const-string p1, "Xamarin.Forms.Platform.Android.FormsWebViewClient, Xamarin.Forms.Platform.Android"

    const-string v1, "Xamarin.Forms.Platform.Android.WebViewRenderer, Xamarin.Forms.Platform.Android"

    invoke-static {p1, v1, p0, v0}, Lmono/android/TypeManager;->Activate(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Object;[Ljava/lang/Object;)V

    .line 38
    :cond_0
    return-void
.end method

.method private native n_onPageFinished(Landroid/webkit/WebView;Ljava/lang/String;)V
.end method

.method private native n_onPageStarted(Landroid/webkit/WebView;Ljava/lang/String;Landroid/graphics/Bitmap;)V
.end method

.method private native n_onReceivedError(Landroid/webkit/WebView;ILjava/lang/String;Ljava/lang/String;)V
.end method

.method private native n_onReceivedError(Landroid/webkit/WebView;Landroid/webkit/WebResourceRequest;Landroid/webkit/WebResourceError;)V
.end method

.method private native n_shouldOverrideUrlLoading(Landroid/webkit/WebView;Landroid/webkit/WebResourceRequest;)Z
.end method

.method private native n_shouldOverrideUrlLoading(Landroid/webkit/WebView;Ljava/lang/String;)Z
.end method


# virtual methods
.method public monodroidAddReference(Ljava/lang/Object;)V
    .locals 1

    .line 91
    iget-object v0, p0, Lcrc643f46942d9dd1fff9/FormsWebViewClient;->refList:Ljava/util/ArrayList;

    if-nez v0, :cond_0

    .line 92
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    iput-object v0, p0, Lcrc643f46942d9dd1fff9/FormsWebViewClient;->refList:Ljava/util/ArrayList;

    .line 93
    :cond_0
    iget-object v0, p0, Lcrc643f46942d9dd1fff9/FormsWebViewClient;->refList:Ljava/util/ArrayList;

    invoke-virtual {v0, p1}, Ljava/util/ArrayList;->add(Ljava/lang/Object;)Z

    .line 94
    return-void
.end method

.method public monodroidClearReferences()V
    .locals 1

    .line 98
    iget-object v0, p0, Lcrc643f46942d9dd1fff9/FormsWebViewClient;->refList:Ljava/util/ArrayList;

    if-eqz v0, :cond_0

    .line 99
    invoke-virtual {v0}, Ljava/util/ArrayList;->clear()V

    .line 100
    :cond_0
    return-void
.end method

.method public onPageFinished(Landroid/webkit/WebView;Ljava/lang/String;)V
    .locals 0

    .line 67
    invoke-direct {p0, p1, p2}, Lcrc643f46942d9dd1fff9/FormsWebViewClient;->n_onPageFinished(Landroid/webkit/WebView;Ljava/lang/String;)V

    .line 68
    return-void
.end method

.method public onPageStarted(Landroid/webkit/WebView;Ljava/lang/String;Landroid/graphics/Bitmap;)V
    .locals 0

    .line 59
    invoke-direct {p0, p1, p2, p3}, Lcrc643f46942d9dd1fff9/FormsWebViewClient;->n_onPageStarted(Landroid/webkit/WebView;Ljava/lang/String;Landroid/graphics/Bitmap;)V

    .line 60
    return-void
.end method

.method public onReceivedError(Landroid/webkit/WebView;ILjava/lang/String;Ljava/lang/String;)V
    .locals 0

    .line 75
    invoke-direct {p0, p1, p2, p3, p4}, Lcrc643f46942d9dd1fff9/FormsWebViewClient;->n_onReceivedError(Landroid/webkit/WebView;ILjava/lang/String;Ljava/lang/String;)V

    .line 76
    return-void
.end method

.method public onReceivedError(Landroid/webkit/WebView;Landroid/webkit/WebResourceRequest;Landroid/webkit/WebResourceError;)V
    .locals 0

    .line 83
    invoke-direct {p0, p1, p2, p3}, Lcrc643f46942d9dd1fff9/FormsWebViewClient;->n_onReceivedError(Landroid/webkit/WebView;Landroid/webkit/WebResourceRequest;Landroid/webkit/WebResourceError;)V

    .line 84
    return-void
.end method

.method public shouldOverrideUrlLoading(Landroid/webkit/WebView;Landroid/webkit/WebResourceRequest;)Z
    .locals 0

    .line 51
    invoke-direct {p0, p1, p2}, Lcrc643f46942d9dd1fff9/FormsWebViewClient;->n_shouldOverrideUrlLoading(Landroid/webkit/WebView;Landroid/webkit/WebResourceRequest;)Z

    move-result p1

    return p1
.end method

.method public shouldOverrideUrlLoading(Landroid/webkit/WebView;Ljava/lang/String;)Z
    .locals 0

    .line 43
    invoke-direct {p0, p1, p2}, Lcrc643f46942d9dd1fff9/FormsWebViewClient;->n_shouldOverrideUrlLoading(Landroid/webkit/WebView;Ljava/lang/String;)Z

    move-result p1

    return p1
.end method
