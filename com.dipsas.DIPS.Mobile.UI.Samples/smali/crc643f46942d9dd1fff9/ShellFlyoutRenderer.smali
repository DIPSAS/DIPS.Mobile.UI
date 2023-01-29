.class public Lcrc643f46942d9dd1fff9/ShellFlyoutRenderer;
.super Landroidx/drawerlayout/widget/DrawerLayout;
.source "ShellFlyoutRenderer.java"

# interfaces
.implements Lmono/android/IGCUserPeer;
.implements Landroidx/drawerlayout/widget/DrawerLayout$DrawerListener;


# static fields
.field public static final __md_methods:Ljava/lang/String;


# instance fields
.field private refList:Ljava/util/ArrayList;


# direct methods
.method static constructor <clinit>()V
    .locals 3

    .line 13
    const-string v0, "n_onInterceptTouchEvent:(Landroid/view/MotionEvent;)Z:GetOnInterceptTouchEvent_Landroid_view_MotionEvent_Handler\nn_drawChild:(Landroid/graphics/Canvas;Landroid/view/View;J)Z:GetDrawChild_Landroid_graphics_Canvas_Landroid_view_View_JHandler\nn_onDrawerClosed:(Landroid/view/View;)V:GetOnDrawerClosed_Landroid_view_View_Handler:AndroidX.DrawerLayout.Widget.DrawerLayout/IDrawerListenerInvoker, Xamarin.AndroidX.DrawerLayout\nn_onDrawerOpened:(Landroid/view/View;)V:GetOnDrawerOpened_Landroid_view_View_Handler:AndroidX.DrawerLayout.Widget.DrawerLayout/IDrawerListenerInvoker, Xamarin.AndroidX.DrawerLayout\nn_onDrawerSlide:(Landroid/view/View;F)V:GetOnDrawerSlide_Landroid_view_View_FHandler:AndroidX.DrawerLayout.Widget.DrawerLayout/IDrawerListenerInvoker, Xamarin.AndroidX.DrawerLayout\nn_onDrawerStateChanged:(I)V:GetOnDrawerStateChanged_IHandler:AndroidX.DrawerLayout.Widget.DrawerLayout/IDrawerListenerInvoker, Xamarin.AndroidX.DrawerLayout\n"

    sput-object v0, Lcrc643f46942d9dd1fff9/ShellFlyoutRenderer;->__md_methods:Ljava/lang/String;

    .line 21
    const-class v1, Lcrc643f46942d9dd1fff9/ShellFlyoutRenderer;

    const-string v2, "Xamarin.Forms.Platform.Android.ShellFlyoutRenderer, Xamarin.Forms.Platform.Android"

    invoke-static {v2, v1, v0}, Lmono/android/Runtime;->register(Ljava/lang/String;Ljava/lang/Class;Ljava/lang/String;)V

    .line 22
    return-void
.end method

.method public constructor <init>(Landroid/content/Context;)V
    .locals 2

    .line 27
    invoke-direct {p0, p1}, Landroidx/drawerlayout/widget/DrawerLayout;-><init>(Landroid/content/Context;)V

    .line 28
    invoke-virtual {p0}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    move-result-object v0

    const-class v1, Lcrc643f46942d9dd1fff9/ShellFlyoutRenderer;

    if-ne v0, v1, :cond_0

    .line 29
    const/4 v0, 0x1

    new-array v0, v0, [Ljava/lang/Object;

    const/4 v1, 0x0

    aput-object p1, v0, v1

    const-string p1, "Xamarin.Forms.Platform.Android.ShellFlyoutRenderer, Xamarin.Forms.Platform.Android"

    const-string v1, "Android.Content.Context, Mono.Android"

    invoke-static {p1, v1, p0, v0}, Lmono/android/TypeManager;->Activate(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Object;[Ljava/lang/Object;)V

    .line 31
    :cond_0
    return-void
.end method

.method public constructor <init>(Landroid/content/Context;Landroid/util/AttributeSet;)V
    .locals 2

    .line 36
    invoke-direct {p0, p1, p2}, Landroidx/drawerlayout/widget/DrawerLayout;-><init>(Landroid/content/Context;Landroid/util/AttributeSet;)V

    .line 37
    invoke-virtual {p0}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    move-result-object v0

    const-class v1, Lcrc643f46942d9dd1fff9/ShellFlyoutRenderer;

    if-ne v0, v1, :cond_0

    .line 38
    const/4 v0, 0x2

    new-array v0, v0, [Ljava/lang/Object;

    const/4 v1, 0x0

    aput-object p1, v0, v1

    const/4 p1, 0x1

    aput-object p2, v0, p1

    const-string p1, "Xamarin.Forms.Platform.Android.ShellFlyoutRenderer, Xamarin.Forms.Platform.Android"

    const-string p2, "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android"

    invoke-static {p1, p2, p0, v0}, Lmono/android/TypeManager;->Activate(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Object;[Ljava/lang/Object;)V

    .line 40
    :cond_0
    return-void
.end method

.method public constructor <init>(Landroid/content/Context;Landroid/util/AttributeSet;I)V
    .locals 2

    .line 45
    invoke-direct {p0, p1, p2, p3}, Landroidx/drawerlayout/widget/DrawerLayout;-><init>(Landroid/content/Context;Landroid/util/AttributeSet;I)V

    .line 46
    invoke-virtual {p0}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    move-result-object v0

    const-class v1, Lcrc643f46942d9dd1fff9/ShellFlyoutRenderer;

    if-ne v0, v1, :cond_0

    .line 47
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

    const-string p1, "Xamarin.Forms.Platform.Android.ShellFlyoutRenderer, Xamarin.Forms.Platform.Android"

    const-string p2, "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android:System.Int32, mscorlib"

    invoke-static {p1, p2, p0, v0}, Lmono/android/TypeManager;->Activate(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Object;[Ljava/lang/Object;)V

    .line 49
    :cond_0
    return-void
.end method

.method private native n_drawChild(Landroid/graphics/Canvas;Landroid/view/View;J)Z
.end method

.method private native n_onDrawerClosed(Landroid/view/View;)V
.end method

.method private native n_onDrawerOpened(Landroid/view/View;)V
.end method

.method private native n_onDrawerSlide(Landroid/view/View;F)V
.end method

.method private native n_onDrawerStateChanged(I)V
.end method

.method private native n_onInterceptTouchEvent(Landroid/view/MotionEvent;)Z
.end method


# virtual methods
.method public drawChild(Landroid/graphics/Canvas;Landroid/view/View;J)Z
    .locals 0

    .line 62
    invoke-direct {p0, p1, p2, p3, p4}, Lcrc643f46942d9dd1fff9/ShellFlyoutRenderer;->n_drawChild(Landroid/graphics/Canvas;Landroid/view/View;J)Z

    move-result p1

    return p1
.end method

.method public monodroidAddReference(Ljava/lang/Object;)V
    .locals 1

    .line 102
    iget-object v0, p0, Lcrc643f46942d9dd1fff9/ShellFlyoutRenderer;->refList:Ljava/util/ArrayList;

    if-nez v0, :cond_0

    .line 103
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    iput-object v0, p0, Lcrc643f46942d9dd1fff9/ShellFlyoutRenderer;->refList:Ljava/util/ArrayList;

    .line 104
    :cond_0
    iget-object v0, p0, Lcrc643f46942d9dd1fff9/ShellFlyoutRenderer;->refList:Ljava/util/ArrayList;

    invoke-virtual {v0, p1}, Ljava/util/ArrayList;->add(Ljava/lang/Object;)Z

    .line 105
    return-void
.end method

.method public monodroidClearReferences()V
    .locals 1

    .line 109
    iget-object v0, p0, Lcrc643f46942d9dd1fff9/ShellFlyoutRenderer;->refList:Ljava/util/ArrayList;

    if-eqz v0, :cond_0

    .line 110
    invoke-virtual {v0}, Ljava/util/ArrayList;->clear()V

    .line 111
    :cond_0
    return-void
.end method

.method public onDrawerClosed(Landroid/view/View;)V
    .locals 0

    .line 70
    invoke-direct {p0, p1}, Lcrc643f46942d9dd1fff9/ShellFlyoutRenderer;->n_onDrawerClosed(Landroid/view/View;)V

    .line 71
    return-void
.end method

.method public onDrawerOpened(Landroid/view/View;)V
    .locals 0

    .line 78
    invoke-direct {p0, p1}, Lcrc643f46942d9dd1fff9/ShellFlyoutRenderer;->n_onDrawerOpened(Landroid/view/View;)V

    .line 79
    return-void
.end method

.method public onDrawerSlide(Landroid/view/View;F)V
    .locals 0

    .line 86
    invoke-direct {p0, p1, p2}, Lcrc643f46942d9dd1fff9/ShellFlyoutRenderer;->n_onDrawerSlide(Landroid/view/View;F)V

    .line 87
    return-void
.end method

.method public onDrawerStateChanged(I)V
    .locals 0

    .line 94
    invoke-direct {p0, p1}, Lcrc643f46942d9dd1fff9/ShellFlyoutRenderer;->n_onDrawerStateChanged(I)V

    .line 95
    return-void
.end method

.method public onInterceptTouchEvent(Landroid/view/MotionEvent;)Z
    .locals 0

    .line 54
    invoke-direct {p0, p1}, Lcrc643f46942d9dd1fff9/ShellFlyoutRenderer;->n_onInterceptTouchEvent(Landroid/view/MotionEvent;)Z

    move-result p1

    return p1
.end method
