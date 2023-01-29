.class public Lcrc643f46942d9dd1fff9/PlatformRenderer;
.super Landroid/view/ViewGroup;
.source "PlatformRenderer.java"

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
    const-string v0, "n_dispatchTouchEvent:(Landroid/view/MotionEvent;)Z:GetDispatchTouchEvent_Landroid_view_MotionEvent_Handler\nn_onLayout:(ZIIII)V:GetOnLayout_ZIIIIHandler\nn_onMeasure:(II)V:GetOnMeasure_IIHandler\n"

    sput-object v0, Lcrc643f46942d9dd1fff9/PlatformRenderer;->__md_methods:Ljava/lang/String;

    .line 17
    const-class v1, Lcrc643f46942d9dd1fff9/PlatformRenderer;

    const-string v2, "Xamarin.Forms.Platform.Android.PlatformRenderer, Xamarin.Forms.Platform.Android"

    invoke-static {v2, v1, v0}, Lmono/android/Runtime;->register(Ljava/lang/String;Ljava/lang/Class;Ljava/lang/String;)V

    .line 18
    return-void
.end method

.method public constructor <init>(Landroid/content/Context;)V
    .locals 2

    .line 23
    invoke-direct {p0, p1}, Landroid/view/ViewGroup;-><init>(Landroid/content/Context;)V

    .line 24
    invoke-virtual {p0}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    move-result-object v0

    const-class v1, Lcrc643f46942d9dd1fff9/PlatformRenderer;

    if-ne v0, v1, :cond_0

    .line 25
    const/4 v0, 0x1

    new-array v0, v0, [Ljava/lang/Object;

    const/4 v1, 0x0

    aput-object p1, v0, v1

    const-string p1, "Xamarin.Forms.Platform.Android.PlatformRenderer, Xamarin.Forms.Platform.Android"

    const-string v1, "Android.Content.Context, Mono.Android"

    invoke-static {p1, v1, p0, v0}, Lmono/android/TypeManager;->Activate(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Object;[Ljava/lang/Object;)V

    .line 27
    :cond_0
    return-void
.end method

.method public constructor <init>(Landroid/content/Context;Landroid/util/AttributeSet;)V
    .locals 2

    .line 32
    invoke-direct {p0, p1, p2}, Landroid/view/ViewGroup;-><init>(Landroid/content/Context;Landroid/util/AttributeSet;)V

    .line 33
    invoke-virtual {p0}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    move-result-object v0

    const-class v1, Lcrc643f46942d9dd1fff9/PlatformRenderer;

    if-ne v0, v1, :cond_0

    .line 34
    const/4 v0, 0x2

    new-array v0, v0, [Ljava/lang/Object;

    const/4 v1, 0x0

    aput-object p1, v0, v1

    const/4 p1, 0x1

    aput-object p2, v0, p1

    const-string p1, "Xamarin.Forms.Platform.Android.PlatformRenderer, Xamarin.Forms.Platform.Android"

    const-string p2, "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android"

    invoke-static {p1, p2, p0, v0}, Lmono/android/TypeManager;->Activate(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Object;[Ljava/lang/Object;)V

    .line 36
    :cond_0
    return-void
.end method

.method public constructor <init>(Landroid/content/Context;Landroid/util/AttributeSet;I)V
    .locals 2

    .line 41
    invoke-direct {p0, p1, p2, p3}, Landroid/view/ViewGroup;-><init>(Landroid/content/Context;Landroid/util/AttributeSet;I)V

    .line 42
    invoke-virtual {p0}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    move-result-object v0

    const-class v1, Lcrc643f46942d9dd1fff9/PlatformRenderer;

    if-ne v0, v1, :cond_0

    .line 43
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

    const-string p1, "Xamarin.Forms.Platform.Android.PlatformRenderer, Xamarin.Forms.Platform.Android"

    const-string p2, "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android:System.Int32, mscorlib"

    invoke-static {p1, p2, p0, v0}, Lmono/android/TypeManager;->Activate(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Object;[Ljava/lang/Object;)V

    .line 45
    :cond_0
    return-void
.end method

.method public constructor <init>(Landroid/content/Context;Landroid/util/AttributeSet;II)V
    .locals 2

    .line 50
    invoke-direct {p0, p1, p2, p3, p4}, Landroid/view/ViewGroup;-><init>(Landroid/content/Context;Landroid/util/AttributeSet;II)V

    .line 51
    invoke-virtual {p0}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    move-result-object v0

    const-class v1, Lcrc643f46942d9dd1fff9/PlatformRenderer;

    if-ne v0, v1, :cond_0

    .line 52
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

    const-string p1, "Xamarin.Forms.Platform.Android.PlatformRenderer, Xamarin.Forms.Platform.Android"

    const-string p2, "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android:System.Int32, mscorlib:System.Int32, mscorlib"

    invoke-static {p1, p2, p0, v0}, Lmono/android/TypeManager;->Activate(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Object;[Ljava/lang/Object;)V

    .line 54
    :cond_0
    return-void
.end method

.method private native n_dispatchTouchEvent(Landroid/view/MotionEvent;)Z
.end method

.method private native n_onLayout(ZIIII)V
.end method

.method private native n_onMeasure(II)V
.end method


# virtual methods
.method public dispatchTouchEvent(Landroid/view/MotionEvent;)Z
    .locals 0

    .line 59
    invoke-direct {p0, p1}, Lcrc643f46942d9dd1fff9/PlatformRenderer;->n_dispatchTouchEvent(Landroid/view/MotionEvent;)Z

    move-result p1

    return p1
.end method

.method public monodroidAddReference(Ljava/lang/Object;)V
    .locals 1

    .line 83
    iget-object v0, p0, Lcrc643f46942d9dd1fff9/PlatformRenderer;->refList:Ljava/util/ArrayList;

    if-nez v0, :cond_0

    .line 84
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    iput-object v0, p0, Lcrc643f46942d9dd1fff9/PlatformRenderer;->refList:Ljava/util/ArrayList;

    .line 85
    :cond_0
    iget-object v0, p0, Lcrc643f46942d9dd1fff9/PlatformRenderer;->refList:Ljava/util/ArrayList;

    invoke-virtual {v0, p1}, Ljava/util/ArrayList;->add(Ljava/lang/Object;)Z

    .line 86
    return-void
.end method

.method public monodroidClearReferences()V
    .locals 1

    .line 90
    iget-object v0, p0, Lcrc643f46942d9dd1fff9/PlatformRenderer;->refList:Ljava/util/ArrayList;

    if-eqz v0, :cond_0

    .line 91
    invoke-virtual {v0}, Ljava/util/ArrayList;->clear()V

    .line 92
    :cond_0
    return-void
.end method

.method public onLayout(ZIIII)V
    .locals 0

    .line 67
    invoke-direct/range {p0 .. p5}, Lcrc643f46942d9dd1fff9/PlatformRenderer;->n_onLayout(ZIIII)V

    .line 68
    return-void
.end method

.method public onMeasure(II)V
    .locals 0

    .line 75
    invoke-direct {p0, p1, p2}, Lcrc643f46942d9dd1fff9/PlatformRenderer;->n_onMeasure(II)V

    .line 76
    return-void
.end method
