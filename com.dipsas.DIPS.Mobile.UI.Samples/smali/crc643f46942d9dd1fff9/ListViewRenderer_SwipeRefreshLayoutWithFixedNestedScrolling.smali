.class public Lcrc643f46942d9dd1fff9/ListViewRenderer_SwipeRefreshLayoutWithFixedNestedScrolling;
.super Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;
.source "ListViewRenderer_SwipeRefreshLayoutWithFixedNestedScrolling.java"

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
    const-string v0, "n_onInterceptTouchEvent:(Landroid/view/MotionEvent;)Z:GetOnInterceptTouchEvent_Landroid_view_MotionEvent_Handler\nn_onNestedScrollAccepted:(Landroid/view/View;Landroid/view/View;I)V:GetOnNestedScrollAccepted_Landroid_view_View_Landroid_view_View_IHandler\nn_onStopNestedScroll:(Landroid/view/View;)V:GetOnStopNestedScroll_Landroid_view_View_Handler\nn_onNestedScroll:(Landroid/view/View;IIII)V:GetOnNestedScroll_Landroid_view_View_IIIIHandler\n"

    sput-object v0, Lcrc643f46942d9dd1fff9/ListViewRenderer_SwipeRefreshLayoutWithFixedNestedScrolling;->__md_methods:Ljava/lang/String;

    .line 18
    const-class v1, Lcrc643f46942d9dd1fff9/ListViewRenderer_SwipeRefreshLayoutWithFixedNestedScrolling;

    const-string v2, "Xamarin.Forms.Platform.Android.ListViewRenderer+SwipeRefreshLayoutWithFixedNestedScrolling, Xamarin.Forms.Platform.Android"

    invoke-static {v2, v1, v0}, Lmono/android/Runtime;->register(Ljava/lang/String;Ljava/lang/Class;Ljava/lang/String;)V

    .line 19
    return-void
.end method

.method public constructor <init>(Landroid/content/Context;)V
    .locals 2

    .line 24
    invoke-direct {p0, p1}, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;-><init>(Landroid/content/Context;)V

    .line 25
    invoke-virtual {p0}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    move-result-object v0

    const-class v1, Lcrc643f46942d9dd1fff9/ListViewRenderer_SwipeRefreshLayoutWithFixedNestedScrolling;

    if-ne v0, v1, :cond_0

    .line 26
    const/4 v0, 0x1

    new-array v0, v0, [Ljava/lang/Object;

    const/4 v1, 0x0

    aput-object p1, v0, v1

    const-string p1, "Xamarin.Forms.Platform.Android.ListViewRenderer+SwipeRefreshLayoutWithFixedNestedScrolling, Xamarin.Forms.Platform.Android"

    const-string v1, "Android.Content.Context, Mono.Android"

    invoke-static {p1, v1, p0, v0}, Lmono/android/TypeManager;->Activate(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Object;[Ljava/lang/Object;)V

    .line 28
    :cond_0
    return-void
.end method

.method public constructor <init>(Landroid/content/Context;Landroid/util/AttributeSet;)V
    .locals 2

    .line 33
    invoke-direct {p0, p1, p2}, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;-><init>(Landroid/content/Context;Landroid/util/AttributeSet;)V

    .line 34
    invoke-virtual {p0}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    move-result-object v0

    const-class v1, Lcrc643f46942d9dd1fff9/ListViewRenderer_SwipeRefreshLayoutWithFixedNestedScrolling;

    if-ne v0, v1, :cond_0

    .line 35
    const/4 v0, 0x2

    new-array v0, v0, [Ljava/lang/Object;

    const/4 v1, 0x0

    aput-object p1, v0, v1

    const/4 p1, 0x1

    aput-object p2, v0, p1

    const-string p1, "Xamarin.Forms.Platform.Android.ListViewRenderer+SwipeRefreshLayoutWithFixedNestedScrolling, Xamarin.Forms.Platform.Android"

    const-string p2, "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android"

    invoke-static {p1, p2, p0, v0}, Lmono/android/TypeManager;->Activate(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Object;[Ljava/lang/Object;)V

    .line 37
    :cond_0
    return-void
.end method

.method private native n_onInterceptTouchEvent(Landroid/view/MotionEvent;)Z
.end method

.method private native n_onNestedScroll(Landroid/view/View;IIII)V
.end method

.method private native n_onNestedScrollAccepted(Landroid/view/View;Landroid/view/View;I)V
.end method

.method private native n_onStopNestedScroll(Landroid/view/View;)V
.end method


# virtual methods
.method public monodroidAddReference(Ljava/lang/Object;)V
    .locals 1

    .line 74
    iget-object v0, p0, Lcrc643f46942d9dd1fff9/ListViewRenderer_SwipeRefreshLayoutWithFixedNestedScrolling;->refList:Ljava/util/ArrayList;

    if-nez v0, :cond_0

    .line 75
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    iput-object v0, p0, Lcrc643f46942d9dd1fff9/ListViewRenderer_SwipeRefreshLayoutWithFixedNestedScrolling;->refList:Ljava/util/ArrayList;

    .line 76
    :cond_0
    iget-object v0, p0, Lcrc643f46942d9dd1fff9/ListViewRenderer_SwipeRefreshLayoutWithFixedNestedScrolling;->refList:Ljava/util/ArrayList;

    invoke-virtual {v0, p1}, Ljava/util/ArrayList;->add(Ljava/lang/Object;)Z

    .line 77
    return-void
.end method

.method public monodroidClearReferences()V
    .locals 1

    .line 81
    iget-object v0, p0, Lcrc643f46942d9dd1fff9/ListViewRenderer_SwipeRefreshLayoutWithFixedNestedScrolling;->refList:Ljava/util/ArrayList;

    if-eqz v0, :cond_0

    .line 82
    invoke-virtual {v0}, Ljava/util/ArrayList;->clear()V

    .line 83
    :cond_0
    return-void
.end method

.method public onInterceptTouchEvent(Landroid/view/MotionEvent;)Z
    .locals 0

    .line 42
    invoke-direct {p0, p1}, Lcrc643f46942d9dd1fff9/ListViewRenderer_SwipeRefreshLayoutWithFixedNestedScrolling;->n_onInterceptTouchEvent(Landroid/view/MotionEvent;)Z

    move-result p1

    return p1
.end method

.method public onNestedScroll(Landroid/view/View;IIII)V
    .locals 0

    .line 66
    invoke-direct/range {p0 .. p5}, Lcrc643f46942d9dd1fff9/ListViewRenderer_SwipeRefreshLayoutWithFixedNestedScrolling;->n_onNestedScroll(Landroid/view/View;IIII)V

    .line 67
    return-void
.end method

.method public onNestedScrollAccepted(Landroid/view/View;Landroid/view/View;I)V
    .locals 0

    .line 50
    invoke-direct {p0, p1, p2, p3}, Lcrc643f46942d9dd1fff9/ListViewRenderer_SwipeRefreshLayoutWithFixedNestedScrolling;->n_onNestedScrollAccepted(Landroid/view/View;Landroid/view/View;I)V

    .line 51
    return-void
.end method

.method public onStopNestedScroll(Landroid/view/View;)V
    .locals 0

    .line 58
    invoke-direct {p0, p1}, Lcrc643f46942d9dd1fff9/ListViewRenderer_SwipeRefreshLayoutWithFixedNestedScrolling;->n_onStopNestedScroll(Landroid/view/View;)V

    .line 59
    return-void
.end method
