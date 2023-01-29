.class public Lcrc64c740772847798805/BottomSheetFragment;
.super Lcom/google/android/material/bottomsheet/BottomSheetDialogFragment;
.source "BottomSheetFragment.java"

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
    const-string v0, "n_onCreateView:(Landroid/view/LayoutInflater;Landroid/view/ViewGroup;Landroid/os/Bundle;)Landroid/view/View;:GetOnCreateView_Landroid_view_LayoutInflater_Landroid_view_ViewGroup_Landroid_os_Bundle_Handler\nn_onCreateDialog:(Landroid/os/Bundle;)Landroid/app/Dialog;:GetOnCreateDialog_Landroid_os_Bundle_Handler\n"

    sput-object v0, Lcrc64c740772847798805/BottomSheetFragment;->__md_methods:Ljava/lang/String;

    .line 16
    const-class v1, Lcrc64c740772847798805/BottomSheetFragment;

    const-string v2, "DIPS.Mobile.UI.Droid.Components.BottomSheet.BottomSheetFragment, DIPS.Mobile.UI.Droid"

    invoke-static {v2, v1, v0}, Lmono/android/Runtime;->register(Ljava/lang/String;Ljava/lang/Class;Ljava/lang/String;)V

    .line 17
    return-void
.end method

.method public constructor <init>()V
    .locals 3

    .line 22
    invoke-direct {p0}, Lcom/google/android/material/bottomsheet/BottomSheetDialogFragment;-><init>()V

    .line 23
    invoke-virtual {p0}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    move-result-object v0

    const-class v1, Lcrc64c740772847798805/BottomSheetFragment;

    if-ne v0, v1, :cond_0

    .line 24
    const/4 v0, 0x0

    new-array v0, v0, [Ljava/lang/Object;

    const-string v1, "DIPS.Mobile.UI.Droid.Components.BottomSheet.BottomSheetFragment, DIPS.Mobile.UI.Droid"

    const-string v2, ""

    invoke-static {v1, v2, p0, v0}, Lmono/android/TypeManager;->Activate(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Object;[Ljava/lang/Object;)V

    .line 26
    :cond_0
    return-void
.end method

.method private native n_onCreateDialog(Landroid/os/Bundle;)Landroid/app/Dialog;
.end method

.method private native n_onCreateView(Landroid/view/LayoutInflater;Landroid/view/ViewGroup;Landroid/os/Bundle;)Landroid/view/View;
.end method


# virtual methods
.method public monodroidAddReference(Ljava/lang/Object;)V
    .locals 1

    .line 47
    iget-object v0, p0, Lcrc64c740772847798805/BottomSheetFragment;->refList:Ljava/util/ArrayList;

    if-nez v0, :cond_0

    .line 48
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    iput-object v0, p0, Lcrc64c740772847798805/BottomSheetFragment;->refList:Ljava/util/ArrayList;

    .line 49
    :cond_0
    iget-object v0, p0, Lcrc64c740772847798805/BottomSheetFragment;->refList:Ljava/util/ArrayList;

    invoke-virtual {v0, p1}, Ljava/util/ArrayList;->add(Ljava/lang/Object;)Z

    .line 50
    return-void
.end method

.method public monodroidClearReferences()V
    .locals 1

    .line 54
    iget-object v0, p0, Lcrc64c740772847798805/BottomSheetFragment;->refList:Ljava/util/ArrayList;

    if-eqz v0, :cond_0

    .line 55
    invoke-virtual {v0}, Ljava/util/ArrayList;->clear()V

    .line 56
    :cond_0
    return-void
.end method

.method public onCreateDialog(Landroid/os/Bundle;)Landroid/app/Dialog;
    .locals 0

    .line 39
    invoke-direct {p0, p1}, Lcrc64c740772847798805/BottomSheetFragment;->n_onCreateDialog(Landroid/os/Bundle;)Landroid/app/Dialog;

    move-result-object p1

    return-object p1
.end method

.method public onCreateView(Landroid/view/LayoutInflater;Landroid/view/ViewGroup;Landroid/os/Bundle;)Landroid/view/View;
    .locals 0

    .line 31
    invoke-direct {p0, p1, p2, p3}, Lcrc64c740772847798805/BottomSheetFragment;->n_onCreateView(Landroid/view/LayoutInflater;Landroid/view/ViewGroup;Landroid/os/Bundle;)Landroid/view/View;

    move-result-object p1

    return-object p1
.end method
