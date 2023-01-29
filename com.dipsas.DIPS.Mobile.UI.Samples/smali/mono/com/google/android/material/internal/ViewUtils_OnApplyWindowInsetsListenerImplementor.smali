.class public Lmono/com/google/android/material/internal/ViewUtils_OnApplyWindowInsetsListenerImplementor;
.super Ljava/lang/Object;
.source "ViewUtils_OnApplyWindowInsetsListenerImplementor.java"

# interfaces
.implements Lmono/android/IGCUserPeer;
.implements Lcom/google/android/material/internal/ViewUtils$OnApplyWindowInsetsListener;


# static fields
.field public static final __md_methods:Ljava/lang/String;


# instance fields
.field private refList:Ljava/util/ArrayList;


# direct methods
.method static constructor <clinit>()V
    .locals 3

    .line 13
    const-string v0, "n_onApplyWindowInsets:(Landroid/view/View;Landroidx/core/view/WindowInsetsCompat;Lcom/google/android/material/internal/ViewUtils$RelativePadding;)Landroidx/core/view/WindowInsetsCompat;:GetOnApplyWindowInsets_Landroid_view_View_Landroidx_core_view_WindowInsetsCompat_Lcom_google_android_material_internal_ViewUtils_RelativePadding_Handler:Google.Android.Material.Internal.ViewUtils/IOnApplyWindowInsetsListenerInvoker, Xamarin.Google.Android.Material\n"

    sput-object v0, Lmono/com/google/android/material/internal/ViewUtils_OnApplyWindowInsetsListenerImplementor;->__md_methods:Ljava/lang/String;

    .line 16
    const-class v1, Lmono/com/google/android/material/internal/ViewUtils_OnApplyWindowInsetsListenerImplementor;

    const-string v2, "Google.Android.Material.Internal.ViewUtils+IOnApplyWindowInsetsListenerImplementor, Xamarin.Google.Android.Material"

    invoke-static {v2, v1, v0}, Lmono/android/Runtime;->register(Ljava/lang/String;Ljava/lang/Class;Ljava/lang/String;)V

    .line 17
    return-void
.end method

.method public constructor <init>()V
    .locals 3

    .line 22
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 23
    invoke-virtual {p0}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    move-result-object v0

    const-class v1, Lmono/com/google/android/material/internal/ViewUtils_OnApplyWindowInsetsListenerImplementor;

    if-ne v0, v1, :cond_0

    .line 24
    const/4 v0, 0x0

    new-array v0, v0, [Ljava/lang/Object;

    const-string v1, "Google.Android.Material.Internal.ViewUtils+IOnApplyWindowInsetsListenerImplementor, Xamarin.Google.Android.Material"

    const-string v2, ""

    invoke-static {v1, v2, p0, v0}, Lmono/android/TypeManager;->Activate(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Object;[Ljava/lang/Object;)V

    .line 26
    :cond_0
    return-void
.end method

.method private native n_onApplyWindowInsets(Landroid/view/View;Landroidx/core/view/WindowInsetsCompat;Lcom/google/android/material/internal/ViewUtils$RelativePadding;)Landroidx/core/view/WindowInsetsCompat;
.end method


# virtual methods
.method public monodroidAddReference(Ljava/lang/Object;)V
    .locals 1

    .line 39
    iget-object v0, p0, Lmono/com/google/android/material/internal/ViewUtils_OnApplyWindowInsetsListenerImplementor;->refList:Ljava/util/ArrayList;

    if-nez v0, :cond_0

    .line 40
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    iput-object v0, p0, Lmono/com/google/android/material/internal/ViewUtils_OnApplyWindowInsetsListenerImplementor;->refList:Ljava/util/ArrayList;

    .line 41
    :cond_0
    iget-object v0, p0, Lmono/com/google/android/material/internal/ViewUtils_OnApplyWindowInsetsListenerImplementor;->refList:Ljava/util/ArrayList;

    invoke-virtual {v0, p1}, Ljava/util/ArrayList;->add(Ljava/lang/Object;)Z

    .line 42
    return-void
.end method

.method public monodroidClearReferences()V
    .locals 1

    .line 46
    iget-object v0, p0, Lmono/com/google/android/material/internal/ViewUtils_OnApplyWindowInsetsListenerImplementor;->refList:Ljava/util/ArrayList;

    if-eqz v0, :cond_0

    .line 47
    invoke-virtual {v0}, Ljava/util/ArrayList;->clear()V

    .line 48
    :cond_0
    return-void
.end method

.method public onApplyWindowInsets(Landroid/view/View;Landroidx/core/view/WindowInsetsCompat;Lcom/google/android/material/internal/ViewUtils$RelativePadding;)Landroidx/core/view/WindowInsetsCompat;
    .locals 0

    .line 31
    invoke-direct {p0, p1, p2, p3}, Lmono/com/google/android/material/internal/ViewUtils_OnApplyWindowInsetsListenerImplementor;->n_onApplyWindowInsets(Landroid/view/View;Landroidx/core/view/WindowInsetsCompat;Lcom/google/android/material/internal/ViewUtils$RelativePadding;)Landroidx/core/view/WindowInsetsCompat;

    move-result-object p1

    return-object p1
.end method
