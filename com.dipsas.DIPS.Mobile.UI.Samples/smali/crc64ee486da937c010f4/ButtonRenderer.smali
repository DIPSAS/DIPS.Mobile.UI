.class public Lcrc64ee486da937c010f4/ButtonRenderer;
.super Landroidx/appcompat/widget/AppCompatButton;
.source "ButtonRenderer.java"

# interfaces
.implements Lmono/android/IGCUserPeer;
.implements Landroid/view/View$OnAttachStateChangeListener;
.implements Landroid/view/View$OnFocusChangeListener;
.implements Landroid/view/View$OnClickListener;
.implements Landroid/view/View$OnTouchListener;


# static fields
.field public static final __md_methods:Ljava/lang/String;


# instance fields
.field private refList:Ljava/util/ArrayList;


# direct methods
.method static constructor <clinit>()V
    .locals 3

    .line 16
    const-string v0, "n_draw:(Landroid/graphics/Canvas;)V:GetDraw_Landroid_graphics_Canvas_Handler\nn_onTouchEvent:(Landroid/view/MotionEvent;)Z:GetOnTouchEvent_Landroid_view_MotionEvent_Handler\nn_onLayout:(ZIIII)V:GetOnLayout_ZIIIIHandler\nn_onViewAttachedToWindow:(Landroid/view/View;)V:GetOnViewAttachedToWindow_Landroid_view_View_Handler:Android.Views.View/IOnAttachStateChangeListenerInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\nn_onViewDetachedFromWindow:(Landroid/view/View;)V:GetOnViewDetachedFromWindow_Landroid_view_View_Handler:Android.Views.View/IOnAttachStateChangeListenerInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\nn_onFocusChange:(Landroid/view/View;Z)V:GetOnFocusChange_Landroid_view_View_ZHandler:Android.Views.View/IOnFocusChangeListenerInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\nn_onClick:(Landroid/view/View;)V:GetOnClick_Landroid_view_View_Handler:Android.Views.View/IOnClickListenerInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\nn_onTouch:(Landroid/view/View;Landroid/view/MotionEvent;)Z:GetOnTouch_Landroid_view_View_Landroid_view_MotionEvent_Handler:Android.Views.View/IOnTouchListenerInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n"

    sput-object v0, Lcrc64ee486da937c010f4/ButtonRenderer;->__md_methods:Ljava/lang/String;

    .line 26
    const-class v1, Lcrc64ee486da937c010f4/ButtonRenderer;

    const-string v2, "Xamarin.Forms.Platform.Android.FastRenderers.ButtonRenderer, Xamarin.Forms.Platform.Android"

    invoke-static {v2, v1, v0}, Lmono/android/Runtime;->register(Ljava/lang/String;Ljava/lang/Class;Ljava/lang/String;)V

    .line 27
    return-void
.end method

.method public constructor <init>(Landroid/content/Context;)V
    .locals 2

    .line 32
    invoke-direct {p0, p1}, Landroidx/appcompat/widget/AppCompatButton;-><init>(Landroid/content/Context;)V

    .line 33
    invoke-virtual {p0}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    move-result-object v0

    const-class v1, Lcrc64ee486da937c010f4/ButtonRenderer;

    if-ne v0, v1, :cond_0

    .line 34
    const/4 v0, 0x1

    new-array v0, v0, [Ljava/lang/Object;

    const/4 v1, 0x0

    aput-object p1, v0, v1

    const-string p1, "Xamarin.Forms.Platform.Android.FastRenderers.ButtonRenderer, Xamarin.Forms.Platform.Android"

    const-string v1, "Android.Content.Context, Mono.Android"

    invoke-static {p1, v1, p0, v0}, Lmono/android/TypeManager;->Activate(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Object;[Ljava/lang/Object;)V

    .line 36
    :cond_0
    return-void
.end method

.method public constructor <init>(Landroid/content/Context;Landroid/util/AttributeSet;)V
    .locals 2

    .line 41
    invoke-direct {p0, p1, p2}, Landroidx/appcompat/widget/AppCompatButton;-><init>(Landroid/content/Context;Landroid/util/AttributeSet;)V

    .line 42
    invoke-virtual {p0}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    move-result-object v0

    const-class v1, Lcrc64ee486da937c010f4/ButtonRenderer;

    if-ne v0, v1, :cond_0

    .line 43
    const/4 v0, 0x2

    new-array v0, v0, [Ljava/lang/Object;

    const/4 v1, 0x0

    aput-object p1, v0, v1

    const/4 p1, 0x1

    aput-object p2, v0, p1

    const-string p1, "Xamarin.Forms.Platform.Android.FastRenderers.ButtonRenderer, Xamarin.Forms.Platform.Android"

    const-string p2, "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android"

    invoke-static {p1, p2, p0, v0}, Lmono/android/TypeManager;->Activate(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Object;[Ljava/lang/Object;)V

    .line 45
    :cond_0
    return-void
.end method

.method public constructor <init>(Landroid/content/Context;Landroid/util/AttributeSet;I)V
    .locals 2

    .line 50
    invoke-direct {p0, p1, p2, p3}, Landroidx/appcompat/widget/AppCompatButton;-><init>(Landroid/content/Context;Landroid/util/AttributeSet;I)V

    .line 51
    invoke-virtual {p0}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    move-result-object v0

    const-class v1, Lcrc64ee486da937c010f4/ButtonRenderer;

    if-ne v0, v1, :cond_0

    .line 52
    const/4 v0, 0x3

    new-array v0, v0, [Ljava/lang/Object;

    const/4 v1, 0x0

    aput-object p1, v0, v1

    const/4 p1, 0x1

    aput-object p2, v0, p1

    const/4 p1, 0x2

    invoke-static {p3}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object p2

    aput-object p2, v0, p1

    const-string p1, "Xamarin.Forms.Platform.Android.FastRenderers.ButtonRenderer, Xamarin.Forms.Platform.Android"

    const-string p2, "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android:System.Int32, mscorlib"

    invoke-static {p1, p2, p0, v0}, Lmono/android/TypeManager;->Activate(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Object;[Ljava/lang/Object;)V

    .line 54
    :cond_0
    return-void
.end method

.method private native n_draw(Landroid/graphics/Canvas;)V
.end method

.method private native n_onClick(Landroid/view/View;)V
.end method

.method private native n_onFocusChange(Landroid/view/View;Z)V
.end method

.method private native n_onLayout(ZIIII)V
.end method

.method private native n_onTouch(Landroid/view/View;Landroid/view/MotionEvent;)Z
.end method

.method private native n_onTouchEvent(Landroid/view/MotionEvent;)Z
.end method

.method private native n_onViewAttachedToWindow(Landroid/view/View;)V
.end method

.method private native n_onViewDetachedFromWindow(Landroid/view/View;)V
.end method


# virtual methods
.method public draw(Landroid/graphics/Canvas;)V
    .locals 0

    .line 59
    invoke-direct {p0, p1}, Lcrc64ee486da937c010f4/ButtonRenderer;->n_draw(Landroid/graphics/Canvas;)V

    .line 60
    return-void
.end method

.method public monodroidAddReference(Ljava/lang/Object;)V
    .locals 1

    .line 123
    iget-object v0, p0, Lcrc64ee486da937c010f4/ButtonRenderer;->refList:Ljava/util/ArrayList;

    if-nez v0, :cond_0

    .line 124
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    iput-object v0, p0, Lcrc64ee486da937c010f4/ButtonRenderer;->refList:Ljava/util/ArrayList;

    .line 125
    :cond_0
    iget-object v0, p0, Lcrc64ee486da937c010f4/ButtonRenderer;->refList:Ljava/util/ArrayList;

    invoke-virtual {v0, p1}, Ljava/util/ArrayList;->add(Ljava/lang/Object;)Z

    .line 126
    return-void
.end method

.method public monodroidClearReferences()V
    .locals 1

    .line 130
    iget-object v0, p0, Lcrc64ee486da937c010f4/ButtonRenderer;->refList:Ljava/util/ArrayList;

    if-eqz v0, :cond_0

    .line 131
    invoke-virtual {v0}, Ljava/util/ArrayList;->clear()V

    .line 132
    :cond_0
    return-void
.end method

.method public onClick(Landroid/view/View;)V
    .locals 0

    .line 107
    invoke-direct {p0, p1}, Lcrc64ee486da937c010f4/ButtonRenderer;->n_onClick(Landroid/view/View;)V

    .line 108
    return-void
.end method

.method public onFocusChange(Landroid/view/View;Z)V
    .locals 0

    .line 99
    invoke-direct {p0, p1, p2}, Lcrc64ee486da937c010f4/ButtonRenderer;->n_onFocusChange(Landroid/view/View;Z)V

    .line 100
    return-void
.end method

.method public onLayout(ZIIII)V
    .locals 0

    .line 75
    invoke-direct/range {p0 .. p5}, Lcrc64ee486da937c010f4/ButtonRenderer;->n_onLayout(ZIIII)V

    .line 76
    return-void
.end method

.method public onTouch(Landroid/view/View;Landroid/view/MotionEvent;)Z
    .locals 0

    .line 115
    invoke-direct {p0, p1, p2}, Lcrc64ee486da937c010f4/ButtonRenderer;->n_onTouch(Landroid/view/View;Landroid/view/MotionEvent;)Z

    move-result p1

    return p1
.end method

.method public onTouchEvent(Landroid/view/MotionEvent;)Z
    .locals 0

    .line 67
    invoke-direct {p0, p1}, Lcrc64ee486da937c010f4/ButtonRenderer;->n_onTouchEvent(Landroid/view/MotionEvent;)Z

    move-result p1

    return p1
.end method

.method public onViewAttachedToWindow(Landroid/view/View;)V
    .locals 0

    .line 83
    invoke-direct {p0, p1}, Lcrc64ee486da937c010f4/ButtonRenderer;->n_onViewAttachedToWindow(Landroid/view/View;)V

    .line 84
    return-void
.end method

.method public onViewDetachedFromWindow(Landroid/view/View;)V
    .locals 0

    .line 91
    invoke-direct {p0, p1}, Lcrc64ee486da937c010f4/ButtonRenderer;->n_onViewDetachedFromWindow(Landroid/view/View;)V

    .line 92
    return-void
.end method
