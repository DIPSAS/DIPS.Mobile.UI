.class public Lcrc643f46942d9dd1fff9/ListViewRenderer_ListViewScrollDetector;
.super Ljava/lang/Object;
.source "ListViewRenderer_ListViewScrollDetector.java"

# interfaces
.implements Lmono/android/IGCUserPeer;
.implements Landroid/widget/AbsListView$OnScrollListener;


# static fields
.field public static final __md_methods:Ljava/lang/String;


# instance fields
.field private refList:Ljava/util/ArrayList;


# direct methods
.method static constructor <clinit>()V
    .locals 3

    .line 13
    const-string v0, "n_onScroll:(Landroid/widget/AbsListView;III)V:GetOnScroll_Landroid_widget_AbsListView_IIIHandler:Android.Widget.AbsListView/IOnScrollListenerInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\nn_onScrollStateChanged:(Landroid/widget/AbsListView;I)V:GetOnScrollStateChanged_Landroid_widget_AbsListView_IHandler:Android.Widget.AbsListView/IOnScrollListenerInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n"

    sput-object v0, Lcrc643f46942d9dd1fff9/ListViewRenderer_ListViewScrollDetector;->__md_methods:Ljava/lang/String;

    .line 17
    const-class v1, Lcrc643f46942d9dd1fff9/ListViewRenderer_ListViewScrollDetector;

    const-string v2, "Xamarin.Forms.Platform.Android.ListViewRenderer+ListViewScrollDetector, Xamarin.Forms.Platform.Android"

    invoke-static {v2, v1, v0}, Lmono/android/Runtime;->register(Ljava/lang/String;Ljava/lang/Class;Ljava/lang/String;)V

    .line 18
    return-void
.end method

.method public constructor <init>()V
    .locals 3

    .line 23
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 24
    invoke-virtual {p0}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    move-result-object v0

    const-class v1, Lcrc643f46942d9dd1fff9/ListViewRenderer_ListViewScrollDetector;

    if-ne v0, v1, :cond_0

    .line 25
    const/4 v0, 0x0

    new-array v0, v0, [Ljava/lang/Object;

    const-string v1, "Xamarin.Forms.Platform.Android.ListViewRenderer+ListViewScrollDetector, Xamarin.Forms.Platform.Android"

    const-string v2, ""

    invoke-static {v1, v2, p0, v0}, Lmono/android/TypeManager;->Activate(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Object;[Ljava/lang/Object;)V

    .line 27
    :cond_0
    return-void
.end method

.method public constructor <init>(Lcrc643f46942d9dd1fff9/ListViewRenderer;)V
    .locals 2

    .line 31
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 32
    invoke-virtual {p0}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    move-result-object v0

    const-class v1, Lcrc643f46942d9dd1fff9/ListViewRenderer_ListViewScrollDetector;

    if-ne v0, v1, :cond_0

    .line 33
    const/4 v0, 0x1

    new-array v0, v0, [Ljava/lang/Object;

    const/4 v1, 0x0

    aput-object p1, v0, v1

    const-string p1, "Xamarin.Forms.Platform.Android.ListViewRenderer+ListViewScrollDetector, Xamarin.Forms.Platform.Android"

    const-string v1, "Xamarin.Forms.Platform.Android.ListViewRenderer, Xamarin.Forms.Platform.Android"

    invoke-static {p1, v1, p0, v0}, Lmono/android/TypeManager;->Activate(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Object;[Ljava/lang/Object;)V

    .line 35
    :cond_0
    return-void
.end method

.method private native n_onScroll(Landroid/widget/AbsListView;III)V
.end method

.method private native n_onScrollStateChanged(Landroid/widget/AbsListView;I)V
.end method


# virtual methods
.method public monodroidAddReference(Ljava/lang/Object;)V
    .locals 1

    .line 56
    iget-object v0, p0, Lcrc643f46942d9dd1fff9/ListViewRenderer_ListViewScrollDetector;->refList:Ljava/util/ArrayList;

    if-nez v0, :cond_0

    .line 57
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    iput-object v0, p0, Lcrc643f46942d9dd1fff9/ListViewRenderer_ListViewScrollDetector;->refList:Ljava/util/ArrayList;

    .line 58
    :cond_0
    iget-object v0, p0, Lcrc643f46942d9dd1fff9/ListViewRenderer_ListViewScrollDetector;->refList:Ljava/util/ArrayList;

    invoke-virtual {v0, p1}, Ljava/util/ArrayList;->add(Ljava/lang/Object;)Z

    .line 59
    return-void
.end method

.method public monodroidClearReferences()V
    .locals 1

    .line 63
    iget-object v0, p0, Lcrc643f46942d9dd1fff9/ListViewRenderer_ListViewScrollDetector;->refList:Ljava/util/ArrayList;

    if-eqz v0, :cond_0

    .line 64
    invoke-virtual {v0}, Ljava/util/ArrayList;->clear()V

    .line 65
    :cond_0
    return-void
.end method

.method public onScroll(Landroid/widget/AbsListView;III)V
    .locals 0

    .line 40
    invoke-direct {p0, p1, p2, p3, p4}, Lcrc643f46942d9dd1fff9/ListViewRenderer_ListViewScrollDetector;->n_onScroll(Landroid/widget/AbsListView;III)V

    .line 41
    return-void
.end method

.method public onScrollStateChanged(Landroid/widget/AbsListView;I)V
    .locals 0

    .line 48
    invoke-direct {p0, p1, p2}, Lcrc643f46942d9dd1fff9/ListViewRenderer_ListViewScrollDetector;->n_onScrollStateChanged(Landroid/widget/AbsListView;I)V

    .line 49
    return-void
.end method
