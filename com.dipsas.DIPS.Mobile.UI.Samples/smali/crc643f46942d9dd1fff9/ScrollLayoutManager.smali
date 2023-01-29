.class public Lcrc643f46942d9dd1fff9/ScrollLayoutManager;
.super Landroidx/recyclerview/widget/LinearLayoutManager;
.source "ScrollLayoutManager.java"

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
    const-string v0, "n_canScrollVertically:()Z:GetCanScrollVerticallyHandler\n"

    sput-object v0, Lcrc643f46942d9dd1fff9/ScrollLayoutManager;->__md_methods:Ljava/lang/String;

    .line 15
    const-class v1, Lcrc643f46942d9dd1fff9/ScrollLayoutManager;

    const-string v2, "Xamarin.Forms.Platform.Android.ScrollLayoutManager, Xamarin.Forms.Platform.Android"

    invoke-static {v2, v1, v0}, Lmono/android/Runtime;->register(Ljava/lang/String;Ljava/lang/Class;Ljava/lang/String;)V

    .line 16
    return-void
.end method

.method public constructor <init>(Landroid/content/Context;)V
    .locals 2

    .line 21
    invoke-direct {p0, p1}, Landroidx/recyclerview/widget/LinearLayoutManager;-><init>(Landroid/content/Context;)V

    .line 22
    invoke-virtual {p0}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    move-result-object v0

    const-class v1, Lcrc643f46942d9dd1fff9/ScrollLayoutManager;

    if-ne v0, v1, :cond_0

    .line 23
    const/4 v0, 0x1

    new-array v0, v0, [Ljava/lang/Object;

    const/4 v1, 0x0

    aput-object p1, v0, v1

    const-string p1, "Xamarin.Forms.Platform.Android.ScrollLayoutManager, Xamarin.Forms.Platform.Android"

    const-string v1, "Android.Content.Context, Mono.Android"

    invoke-static {p1, v1, p0, v0}, Lmono/android/TypeManager;->Activate(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Object;[Ljava/lang/Object;)V

    .line 25
    :cond_0
    return-void
.end method

.method public constructor <init>(Landroid/content/Context;IZ)V
    .locals 2

    .line 39
    invoke-direct {p0, p1, p2, p3}, Landroidx/recyclerview/widget/LinearLayoutManager;-><init>(Landroid/content/Context;IZ)V

    .line 40
    invoke-virtual {p0}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    move-result-object v0

    const-class v1, Lcrc643f46942d9dd1fff9/ScrollLayoutManager;

    if-ne v0, v1, :cond_0

    .line 41
    const/4 v0, 0x3

    new-array v0, v0, [Ljava/lang/Object;

    const/4 v1, 0x0

    aput-object p1, v0, v1

    const/4 p1, 0x1

    invoke-static {p2}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object p2

    aput-object p2, v0, p1

    const/4 p1, 0x2

    invoke-static {p3}, Ljava/lang/Boolean;->valueOf(Z)Ljava/lang/Boolean;

    move-result-object p2

    aput-object p2, v0, p1

    const-string p1, "Xamarin.Forms.Platform.Android.ScrollLayoutManager, Xamarin.Forms.Platform.Android"

    const-string p2, "Android.Content.Context, Mono.Android:System.Int32, mscorlib:System.Boolean, mscorlib"

    invoke-static {p1, p2, p0, v0}, Lmono/android/TypeManager;->Activate(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Object;[Ljava/lang/Object;)V

    .line 43
    :cond_0
    return-void
.end method

.method public constructor <init>(Landroid/content/Context;Landroid/util/AttributeSet;II)V
    .locals 2

    .line 30
    invoke-direct {p0, p1, p2, p3, p4}, Landroidx/recyclerview/widget/LinearLayoutManager;-><init>(Landroid/content/Context;Landroid/util/AttributeSet;II)V

    .line 31
    invoke-virtual {p0}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    move-result-object v0

    const-class v1, Lcrc643f46942d9dd1fff9/ScrollLayoutManager;

    if-ne v0, v1, :cond_0

    .line 32
    const/4 v0, 0x4

    new-array v0, v0, [Ljava/lang/Object;

    const/4 v1, 0x0

    aput-object p1, v0, v1

    const/4 p1, 0x1

    aput-object p2, v0, p1

    const/4 p1, 0x2

    invoke-static {p3}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object p2

    aput-object p2, v0, p1

    const/4 p1, 0x3

    invoke-static {p4}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object p2

    aput-object p2, v0, p1

    const-string p1, "Xamarin.Forms.Platform.Android.ScrollLayoutManager, Xamarin.Forms.Platform.Android"

    const-string p2, "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android:System.Int32, mscorlib:System.Int32, mscorlib"

    invoke-static {p1, p2, p0, v0}, Lmono/android/TypeManager;->Activate(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Object;[Ljava/lang/Object;)V

    .line 34
    :cond_0
    return-void
.end method

.method private native n_canScrollVertically()Z
.end method


# virtual methods
.method public canScrollVertically()Z
    .locals 1

    .line 48
    invoke-direct {p0}, Lcrc643f46942d9dd1fff9/ScrollLayoutManager;->n_canScrollVertically()Z

    move-result v0

    return v0
.end method

.method public monodroidAddReference(Ljava/lang/Object;)V
    .locals 1

    .line 56
    iget-object v0, p0, Lcrc643f46942d9dd1fff9/ScrollLayoutManager;->refList:Ljava/util/ArrayList;

    if-nez v0, :cond_0

    .line 57
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    iput-object v0, p0, Lcrc643f46942d9dd1fff9/ScrollLayoutManager;->refList:Ljava/util/ArrayList;

    .line 58
    :cond_0
    iget-object v0, p0, Lcrc643f46942d9dd1fff9/ScrollLayoutManager;->refList:Ljava/util/ArrayList;

    invoke-virtual {v0, p1}, Ljava/util/ArrayList;->add(Ljava/lang/Object;)Z

    .line 59
    return-void
.end method

.method public monodroidClearReferences()V
    .locals 1

    .line 63
    iget-object v0, p0, Lcrc643f46942d9dd1fff9/ScrollLayoutManager;->refList:Ljava/util/ArrayList;

    if-eqz v0, :cond_0

    .line 64
    invoke-virtual {v0}, Ljava/util/ArrayList;->clear()V

    .line 65
    :cond_0
    return-void
.end method
