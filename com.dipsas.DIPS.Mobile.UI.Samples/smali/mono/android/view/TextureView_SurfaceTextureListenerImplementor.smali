.class public Lmono/android/view/TextureView_SurfaceTextureListenerImplementor;
.super Ljava/lang/Object;
.source "TextureView_SurfaceTextureListenerImplementor.java"

# interfaces
.implements Lmono/android/IGCUserPeer;
.implements Landroid/view/TextureView$SurfaceTextureListener;


# static fields
.field public static final __md_methods:Ljava/lang/String;


# instance fields
.field private refList:Ljava/util/ArrayList;


# direct methods
.method static constructor <clinit>()V
    .locals 3

    .line 13
    const-string v0, "n_onSurfaceTextureAvailable:(Landroid/graphics/SurfaceTexture;II)V:GetOnSurfaceTextureAvailable_Landroid_graphics_SurfaceTexture_IIHandler:Android.Views.TextureView/ISurfaceTextureListenerInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\nn_onSurfaceTextureDestroyed:(Landroid/graphics/SurfaceTexture;)Z:GetOnSurfaceTextureDestroyed_Landroid_graphics_SurfaceTexture_Handler:Android.Views.TextureView/ISurfaceTextureListenerInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\nn_onSurfaceTextureSizeChanged:(Landroid/graphics/SurfaceTexture;II)V:GetOnSurfaceTextureSizeChanged_Landroid_graphics_SurfaceTexture_IIHandler:Android.Views.TextureView/ISurfaceTextureListenerInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\nn_onSurfaceTextureUpdated:(Landroid/graphics/SurfaceTexture;)V:GetOnSurfaceTextureUpdated_Landroid_graphics_SurfaceTexture_Handler:Android.Views.TextureView/ISurfaceTextureListenerInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n"

    sput-object v0, Lmono/android/view/TextureView_SurfaceTextureListenerImplementor;->__md_methods:Ljava/lang/String;

    .line 19
    const-class v1, Lmono/android/view/TextureView_SurfaceTextureListenerImplementor;

    const-string v2, "Android.Views.TextureView+ISurfaceTextureListenerImplementor, Mono.Android"

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

    const-class v1, Lmono/android/view/TextureView_SurfaceTextureListenerImplementor;

    if-ne v0, v1, :cond_0

    .line 27
    const/4 v0, 0x0

    new-array v0, v0, [Ljava/lang/Object;

    const-string v1, "Android.Views.TextureView+ISurfaceTextureListenerImplementor, Mono.Android"

    const-string v2, ""

    invoke-static {v1, v2, p0, v0}, Lmono/android/TypeManager;->Activate(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Object;[Ljava/lang/Object;)V

    .line 29
    :cond_0
    return-void
.end method

.method private native n_onSurfaceTextureAvailable(Landroid/graphics/SurfaceTexture;II)V
.end method

.method private native n_onSurfaceTextureDestroyed(Landroid/graphics/SurfaceTexture;)Z
.end method

.method private native n_onSurfaceTextureSizeChanged(Landroid/graphics/SurfaceTexture;II)V
.end method

.method private native n_onSurfaceTextureUpdated(Landroid/graphics/SurfaceTexture;)V
.end method


# virtual methods
.method public monodroidAddReference(Ljava/lang/Object;)V
    .locals 1

    .line 66
    iget-object v0, p0, Lmono/android/view/TextureView_SurfaceTextureListenerImplementor;->refList:Ljava/util/ArrayList;

    if-nez v0, :cond_0

    .line 67
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    iput-object v0, p0, Lmono/android/view/TextureView_SurfaceTextureListenerImplementor;->refList:Ljava/util/ArrayList;

    .line 68
    :cond_0
    iget-object v0, p0, Lmono/android/view/TextureView_SurfaceTextureListenerImplementor;->refList:Ljava/util/ArrayList;

    invoke-virtual {v0, p1}, Ljava/util/ArrayList;->add(Ljava/lang/Object;)Z

    .line 69
    return-void
.end method

.method public monodroidClearReferences()V
    .locals 1

    .line 73
    iget-object v0, p0, Lmono/android/view/TextureView_SurfaceTextureListenerImplementor;->refList:Ljava/util/ArrayList;

    if-eqz v0, :cond_0

    .line 74
    invoke-virtual {v0}, Ljava/util/ArrayList;->clear()V

    .line 75
    :cond_0
    return-void
.end method

.method public onSurfaceTextureAvailable(Landroid/graphics/SurfaceTexture;II)V
    .locals 0

    .line 34
    invoke-direct {p0, p1, p2, p3}, Lmono/android/view/TextureView_SurfaceTextureListenerImplementor;->n_onSurfaceTextureAvailable(Landroid/graphics/SurfaceTexture;II)V

    .line 35
    return-void
.end method

.method public onSurfaceTextureDestroyed(Landroid/graphics/SurfaceTexture;)Z
    .locals 0

    .line 42
    invoke-direct {p0, p1}, Lmono/android/view/TextureView_SurfaceTextureListenerImplementor;->n_onSurfaceTextureDestroyed(Landroid/graphics/SurfaceTexture;)Z

    move-result p1

    return p1
.end method

.method public onSurfaceTextureSizeChanged(Landroid/graphics/SurfaceTexture;II)V
    .locals 0

    .line 50
    invoke-direct {p0, p1, p2, p3}, Lmono/android/view/TextureView_SurfaceTextureListenerImplementor;->n_onSurfaceTextureSizeChanged(Landroid/graphics/SurfaceTexture;II)V

    .line 51
    return-void
.end method

.method public onSurfaceTextureUpdated(Landroid/graphics/SurfaceTexture;)V
    .locals 0

    .line 58
    invoke-direct {p0, p1}, Lmono/android/view/TextureView_SurfaceTextureListenerImplementor;->n_onSurfaceTextureUpdated(Landroid/graphics/SurfaceTexture;)V

    .line 59
    return-void
.end method
