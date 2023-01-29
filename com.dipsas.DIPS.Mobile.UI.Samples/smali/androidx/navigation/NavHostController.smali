.class public Landroidx/navigation/NavHostController;
.super Landroidx/navigation/NavController;
.source "NavHostController.java"


# direct methods
.method public constructor <init>(Landroid/content/Context;)V
    .locals 0
    .param p1, "context"    # Landroid/content/Context;

    .line 53
    invoke-direct {p0, p1}, Landroidx/navigation/NavController;-><init>(Landroid/content/Context;)V

    .line 54
    return-void
.end method


# virtual methods
.method public final enableOnBackPressed(Z)V
    .locals 0
    .param p1, "enabled"    # Z

    .line 97
    invoke-super {p0, p1}, Landroidx/navigation/NavController;->enableOnBackPressed(Z)V

    .line 98
    return-void
.end method

.method public final setLifecycleOwner(Landroidx/lifecycle/LifecycleOwner;)V
    .locals 0
    .param p1, "owner"    # Landroidx/lifecycle/LifecycleOwner;

    .line 64
    invoke-super {p0, p1}, Landroidx/navigation/NavController;->setLifecycleOwner(Landroidx/lifecycle/LifecycleOwner;)V

    .line 65
    return-void
.end method

.method public final setOnBackPressedDispatcher(Landroidx/activity/OnBackPressedDispatcher;)V
    .locals 0
    .param p1, "dispatcher"    # Landroidx/activity/OnBackPressedDispatcher;

    .line 86
    invoke-super {p0, p1}, Landroidx/navigation/NavController;->setOnBackPressedDispatcher(Landroidx/activity/OnBackPressedDispatcher;)V

    .line 87
    return-void
.end method

.method public final setViewModelStore(Landroidx/lifecycle/ViewModelStore;)V
    .locals 0
    .param p1, "viewModelStore"    # Landroidx/lifecycle/ViewModelStore;

    .line 114
    invoke-super {p0, p1}, Landroidx/navigation/NavController;->setViewModelStore(Landroidx/lifecycle/ViewModelStore;)V

    .line 115
    return-void
.end method
