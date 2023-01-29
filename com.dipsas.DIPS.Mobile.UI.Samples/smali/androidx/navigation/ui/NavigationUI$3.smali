.class Landroidx/navigation/ui/NavigationUI$3;
.super Ljava/lang/Object;
.source "NavigationUI.java"

# interfaces
.implements Lcom/google/android/material/navigation/NavigationView$OnNavigationItemSelectedListener;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Landroidx/navigation/ui/NavigationUI;->setupWithNavController(Lcom/google/android/material/navigation/NavigationView;Landroidx/navigation/NavController;)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic val$navController:Landroidx/navigation/NavController;

.field final synthetic val$navigationView:Lcom/google/android/material/navigation/NavigationView;


# direct methods
.method constructor <init>(Landroidx/navigation/NavController;Lcom/google/android/material/navigation/NavigationView;)V
    .locals 0

    .line 450
    iput-object p1, p0, Landroidx/navigation/ui/NavigationUI$3;->val$navController:Landroidx/navigation/NavController;

    iput-object p2, p0, Landroidx/navigation/ui/NavigationUI$3;->val$navigationView:Lcom/google/android/material/navigation/NavigationView;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public onNavigationItemSelected(Landroid/view/MenuItem;)Z
    .locals 4
    .param p1, "item"    # Landroid/view/MenuItem;

    .line 453
    iget-object v0, p0, Landroidx/navigation/ui/NavigationUI$3;->val$navController:Landroidx/navigation/NavController;

    invoke-static {p1, v0}, Landroidx/navigation/ui/NavigationUI;->onNavDestinationSelected(Landroid/view/MenuItem;Landroidx/navigation/NavController;)Z

    move-result v0

    .line 454
    .local v0, "handled":Z
    if-eqz v0, :cond_1

    .line 455
    iget-object v1, p0, Landroidx/navigation/ui/NavigationUI$3;->val$navigationView:Lcom/google/android/material/navigation/NavigationView;

    invoke-virtual {v1}, Lcom/google/android/material/navigation/NavigationView;->getParent()Landroid/view/ViewParent;

    move-result-object v1

    .line 456
    .local v1, "parent":Landroid/view/ViewParent;
    instance-of v2, v1, Landroidx/customview/widget/Openable;

    if-eqz v2, :cond_0

    .line 457
    move-object v2, v1

    check-cast v2, Landroidx/customview/widget/Openable;

    invoke-interface {v2}, Landroidx/customview/widget/Openable;->close()V

    goto :goto_0

    .line 459
    :cond_0
    iget-object v2, p0, Landroidx/navigation/ui/NavigationUI$3;->val$navigationView:Lcom/google/android/material/navigation/NavigationView;

    .line 460
    invoke-static {v2}, Landroidx/navigation/ui/NavigationUI;->findBottomSheetBehavior(Landroid/view/View;)Lcom/google/android/material/bottomsheet/BottomSheetBehavior;

    move-result-object v2

    .line 461
    .local v2, "bottomSheetBehavior":Lcom/google/android/material/bottomsheet/BottomSheetBehavior;
    if-eqz v2, :cond_1

    .line 462
    const/4 v3, 0x5

    invoke-virtual {v2, v3}, Lcom/google/android/material/bottomsheet/BottomSheetBehavior;->setState(I)V

    .line 466
    .end local v1    # "parent":Landroid/view/ViewParent;
    .end local v2    # "bottomSheetBehavior":Lcom/google/android/material/bottomsheet/BottomSheetBehavior;
    :cond_1
    :goto_0
    return v0
.end method
