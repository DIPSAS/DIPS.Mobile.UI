.class public Lcrc64720bb2db43a66fe9/NavigationPageRenderer_DrawerMultiplexedListener;
.super Ljava/lang/Object;
.source "NavigationPageRenderer_DrawerMultiplexedListener.java"

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
    const-string v0, "n_onDrawerClosed:(Landroid/view/View;)V:GetOnDrawerClosed_Landroid_view_View_Handler:AndroidX.DrawerLayout.Widget.DrawerLayout/IDrawerListenerInvoker, Xamarin.AndroidX.DrawerLayout\nn_onDrawerOpened:(Landroid/view/View;)V:GetOnDrawerOpened_Landroid_view_View_Handler:AndroidX.DrawerLayout.Widget.DrawerLayout/IDrawerListenerInvoker, Xamarin.AndroidX.DrawerLayout\nn_onDrawerSlide:(Landroid/view/View;F)V:GetOnDrawerSlide_Landroid_view_View_FHandler:AndroidX.DrawerLayout.Widget.DrawerLayout/IDrawerListenerInvoker, Xamarin.AndroidX.DrawerLayout\nn_onDrawerStateChanged:(I)V:GetOnDrawerStateChanged_IHandler:AndroidX.DrawerLayout.Widget.DrawerLayout/IDrawerListenerInvoker, Xamarin.AndroidX.DrawerLayout\n"

    sput-object v0, Lcrc64720bb2db43a66fe9/NavigationPageRenderer_DrawerMultiplexedListener;->__md_methods:Ljava/lang/String;

    .line 19
    const-class v1, Lcrc64720bb2db43a66fe9/NavigationPageRenderer_DrawerMultiplexedListener;

    const-string v2, "Xamarin.Forms.Platform.Android.AppCompat.NavigationPageRenderer+DrawerMultiplexedListener, Xamarin.Forms.Platform.Android"

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

    const-class v1, Lcrc64720bb2db43a66fe9/NavigationPageRenderer_DrawerMultiplexedListener;

    if-ne v0, v1, :cond_0

    .line 27
    const/4 v0, 0x0

    new-array v0, v0, [Ljava/lang/Object;

    const-string v1, "Xamarin.Forms.Platform.Android.AppCompat.NavigationPageRenderer+DrawerMultiplexedListener, Xamarin.Forms.Platform.Android"

    const-string v2, ""

    invoke-static {v1, v2, p0, v0}, Lmono/android/TypeManager;->Activate(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Object;[Ljava/lang/Object;)V

    .line 29
    :cond_0
    return-void
.end method

.method private native n_onDrawerClosed(Landroid/view/View;)V
.end method

.method private native n_onDrawerOpened(Landroid/view/View;)V
.end method

.method private native n_onDrawerSlide(Landroid/view/View;F)V
.end method

.method private native n_onDrawerStateChanged(I)V
.end method


# virtual methods
.method public monodroidAddReference(Ljava/lang/Object;)V
    .locals 1

    .line 66
    iget-object v0, p0, Lcrc64720bb2db43a66fe9/NavigationPageRenderer_DrawerMultiplexedListener;->refList:Ljava/util/ArrayList;

    if-nez v0, :cond_0

    .line 67
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    iput-object v0, p0, Lcrc64720bb2db43a66fe9/NavigationPageRenderer_DrawerMultiplexedListener;->refList:Ljava/util/ArrayList;

    .line 68
    :cond_0
    iget-object v0, p0, Lcrc64720bb2db43a66fe9/NavigationPageRenderer_DrawerMultiplexedListener;->refList:Ljava/util/ArrayList;

    invoke-virtual {v0, p1}, Ljava/util/ArrayList;->add(Ljava/lang/Object;)Z

    .line 69
    return-void
.end method

.method public monodroidClearReferences()V
    .locals 1

    .line 73
    iget-object v0, p0, Lcrc64720bb2db43a66fe9/NavigationPageRenderer_DrawerMultiplexedListener;->refList:Ljava/util/ArrayList;

    if-eqz v0, :cond_0

    .line 74
    invoke-virtual {v0}, Ljava/util/ArrayList;->clear()V

    .line 75
    :cond_0
    return-void
.end method

.method public onDrawerClosed(Landroid/view/View;)V
    .locals 0

    .line 34
    invoke-direct {p0, p1}, Lcrc64720bb2db43a66fe9/NavigationPageRenderer_DrawerMultiplexedListener;->n_onDrawerClosed(Landroid/view/View;)V

    .line 35
    return-void
.end method

.method public onDrawerOpened(Landroid/view/View;)V
    .locals 0

    .line 42
    invoke-direct {p0, p1}, Lcrc64720bb2db43a66fe9/NavigationPageRenderer_DrawerMultiplexedListener;->n_onDrawerOpened(Landroid/view/View;)V

    .line 43
    return-void
.end method

.method public onDrawerSlide(Landroid/view/View;F)V
    .locals 0

    .line 50
    invoke-direct {p0, p1, p2}, Lcrc64720bb2db43a66fe9/NavigationPageRenderer_DrawerMultiplexedListener;->n_onDrawerSlide(Landroid/view/View;F)V

    .line 51
    return-void
.end method

.method public onDrawerStateChanged(I)V
    .locals 0

    .line 58
    invoke-direct {p0, p1}, Lcrc64720bb2db43a66fe9/NavigationPageRenderer_DrawerMultiplexedListener;->n_onDrawerStateChanged(I)V

    .line 59
    return-void
.end method
