.class public Lcrc643f46942d9dd1fff9/EntryCellAppCompatEditText;
.super Lcrc643f46942d9dd1fff9/FormsAppCompatEditTextBase;
.source "EntryCellAppCompatEditText.java"

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
    const-string v0, "n_onKeyPreIme:(ILandroid/view/KeyEvent;)Z:GetOnKeyPreIme_ILandroid_view_KeyEvent_Handler\nn_onFocusChanged:(ZILandroid/graphics/Rect;)V:GetOnFocusChanged_ZILandroid_graphics_Rect_Handler\n"

    sput-object v0, Lcrc643f46942d9dd1fff9/EntryCellAppCompatEditText;->__md_methods:Ljava/lang/String;

    .line 16
    const-class v1, Lcrc643f46942d9dd1fff9/EntryCellAppCompatEditText;

    const-string v2, "Xamarin.Forms.Platform.Android.EntryCellAppCompatEditText, Xamarin.Forms.Platform.Android"

    invoke-static {v2, v1, v0}, Lmono/android/Runtime;->register(Ljava/lang/String;Ljava/lang/Class;Ljava/lang/String;)V

    .line 17
    return-void
.end method

.method public constructor <init>(Landroid/content/Context;)V
    .locals 2

    .line 22
    invoke-direct {p0, p1}, Lcrc643f46942d9dd1fff9/FormsAppCompatEditTextBase;-><init>(Landroid/content/Context;)V

    .line 23
    invoke-virtual {p0}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    move-result-object v0

    const-class v1, Lcrc643f46942d9dd1fff9/EntryCellAppCompatEditText;

    if-ne v0, v1, :cond_0

    .line 24
    const/4 v0, 0x1

    new-array v0, v0, [Ljava/lang/Object;

    const/4 v1, 0x0

    aput-object p1, v0, v1

    const-string p1, "Xamarin.Forms.Platform.Android.EntryCellAppCompatEditText, Xamarin.Forms.Platform.Android"

    const-string v1, "Android.Content.Context, Mono.Android"

    invoke-static {p1, v1, p0, v0}, Lmono/android/TypeManager;->Activate(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Object;[Ljava/lang/Object;)V

    .line 26
    :cond_0
    return-void
.end method

.method public constructor <init>(Landroid/content/Context;Landroid/util/AttributeSet;)V
    .locals 2

    .line 31
    invoke-direct {p0, p1, p2}, Lcrc643f46942d9dd1fff9/FormsAppCompatEditTextBase;-><init>(Landroid/content/Context;Landroid/util/AttributeSet;)V

    .line 32
    invoke-virtual {p0}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    move-result-object v0

    const-class v1, Lcrc643f46942d9dd1fff9/EntryCellAppCompatEditText;

    if-ne v0, v1, :cond_0

    .line 33
    const/4 v0, 0x2

    new-array v0, v0, [Ljava/lang/Object;

    const/4 v1, 0x0

    aput-object p1, v0, v1

    const/4 p1, 0x1

    aput-object p2, v0, p1

    const-string p1, "Xamarin.Forms.Platform.Android.EntryCellAppCompatEditText, Xamarin.Forms.Platform.Android"

    const-string p2, "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android"

    invoke-static {p1, p2, p0, v0}, Lmono/android/TypeManager;->Activate(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Object;[Ljava/lang/Object;)V

    .line 35
    :cond_0
    return-void
.end method

.method public constructor <init>(Landroid/content/Context;Landroid/util/AttributeSet;I)V
    .locals 2

    .line 40
    invoke-direct {p0, p1, p2, p3}, Lcrc643f46942d9dd1fff9/FormsAppCompatEditTextBase;-><init>(Landroid/content/Context;Landroid/util/AttributeSet;I)V

    .line 41
    invoke-virtual {p0}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    move-result-object v0

    const-class v1, Lcrc643f46942d9dd1fff9/EntryCellAppCompatEditText;

    if-ne v0, v1, :cond_0

    .line 42
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

    const-string p1, "Xamarin.Forms.Platform.Android.EntryCellAppCompatEditText, Xamarin.Forms.Platform.Android"

    const-string p2, "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android:System.Int32, mscorlib"

    invoke-static {p1, p2, p0, v0}, Lmono/android/TypeManager;->Activate(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Object;[Ljava/lang/Object;)V

    .line 44
    :cond_0
    return-void
.end method

.method private native n_onFocusChanged(ZILandroid/graphics/Rect;)V
.end method

.method private native n_onKeyPreIme(ILandroid/view/KeyEvent;)Z
.end method


# virtual methods
.method public monodroidAddReference(Ljava/lang/Object;)V
    .locals 1

    .line 65
    iget-object v0, p0, Lcrc643f46942d9dd1fff9/EntryCellAppCompatEditText;->refList:Ljava/util/ArrayList;

    if-nez v0, :cond_0

    .line 66
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    iput-object v0, p0, Lcrc643f46942d9dd1fff9/EntryCellAppCompatEditText;->refList:Ljava/util/ArrayList;

    .line 67
    :cond_0
    iget-object v0, p0, Lcrc643f46942d9dd1fff9/EntryCellAppCompatEditText;->refList:Ljava/util/ArrayList;

    invoke-virtual {v0, p1}, Ljava/util/ArrayList;->add(Ljava/lang/Object;)Z

    .line 68
    return-void
.end method

.method public monodroidClearReferences()V
    .locals 1

    .line 72
    iget-object v0, p0, Lcrc643f46942d9dd1fff9/EntryCellAppCompatEditText;->refList:Ljava/util/ArrayList;

    if-eqz v0, :cond_0

    .line 73
    invoke-virtual {v0}, Ljava/util/ArrayList;->clear()V

    .line 74
    :cond_0
    return-void
.end method

.method public onFocusChanged(ZILandroid/graphics/Rect;)V
    .locals 0

    .line 57
    invoke-direct {p0, p1, p2, p3}, Lcrc643f46942d9dd1fff9/EntryCellAppCompatEditText;->n_onFocusChanged(ZILandroid/graphics/Rect;)V

    .line 58
    return-void
.end method

.method public onKeyPreIme(ILandroid/view/KeyEvent;)Z
    .locals 0

    .line 49
    invoke-direct {p0, p1, p2}, Lcrc643f46942d9dd1fff9/EntryCellAppCompatEditText;->n_onKeyPreIme(ILandroid/view/KeyEvent;)Z

    move-result p1

    return p1
.end method
