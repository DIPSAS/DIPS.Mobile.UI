.class Landroidx/navigation/ui/NavigationUI$6;
.super Ljava/lang/Object;
.source "NavigationUI.java"

# interfaces
.implements Landroidx/navigation/NavController$OnDestinationChangedListener;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Landroidx/navigation/ui/NavigationUI;->setupWithNavController(Lcom/google/android/material/bottomnavigation/BottomNavigationView;Landroidx/navigation/NavController;)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic val$navController:Landroidx/navigation/NavController;

.field final synthetic val$weakReference:Ljava/lang/ref/WeakReference;


# direct methods
.method constructor <init>(Ljava/lang/ref/WeakReference;Landroidx/navigation/NavController;)V
    .locals 0

    .line 537
    iput-object p1, p0, Landroidx/navigation/ui/NavigationUI$6;->val$weakReference:Ljava/lang/ref/WeakReference;

    iput-object p2, p0, Landroidx/navigation/ui/NavigationUI$6;->val$navController:Landroidx/navigation/NavController;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public onDestinationChanged(Landroidx/navigation/NavController;Landroidx/navigation/NavDestination;Landroid/os/Bundle;)V
    .locals 6
    .param p1, "controller"    # Landroidx/navigation/NavController;
    .param p2, "destination"    # Landroidx/navigation/NavDestination;
    .param p3, "arguments"    # Landroid/os/Bundle;

    .line 541
    iget-object v0, p0, Landroidx/navigation/ui/NavigationUI$6;->val$weakReference:Ljava/lang/ref/WeakReference;

    invoke-virtual {v0}, Ljava/lang/ref/WeakReference;->get()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Lcom/google/android/material/bottomnavigation/BottomNavigationView;

    .line 542
    .local v0, "view":Lcom/google/android/material/bottomnavigation/BottomNavigationView;
    if-nez v0, :cond_0

    .line 543
    iget-object v1, p0, Landroidx/navigation/ui/NavigationUI$6;->val$navController:Landroidx/navigation/NavController;

    invoke-virtual {v1, p0}, Landroidx/navigation/NavController;->removeOnDestinationChangedListener(Landroidx/navigation/NavController$OnDestinationChangedListener;)V

    .line 544
    return-void

    .line 546
    :cond_0
    invoke-virtual {v0}, Lcom/google/android/material/bottomnavigation/BottomNavigationView;->getMenu()Landroid/view/Menu;

    move-result-object v1

    .line 547
    .local v1, "menu":Landroid/view/Menu;
    const/4 v2, 0x0

    .local v2, "h":I
    invoke-interface {v1}, Landroid/view/Menu;->size()I

    move-result v3

    .local v3, "size":I
    :goto_0
    if-ge v2, v3, :cond_2

    .line 548
    invoke-interface {v1, v2}, Landroid/view/Menu;->getItem(I)Landroid/view/MenuItem;

    move-result-object v4

    .line 549
    .local v4, "item":Landroid/view/MenuItem;
    invoke-interface {v4}, Landroid/view/MenuItem;->getItemId()I

    move-result v5

    invoke-static {p2, v5}, Landroidx/navigation/ui/NavigationUI;->matchDestination(Landroidx/navigation/NavDestination;I)Z

    move-result v5

    if-eqz v5, :cond_1

    .line 550
    const/4 v5, 0x1

    invoke-interface {v4, v5}, Landroid/view/MenuItem;->setChecked(Z)Landroid/view/MenuItem;

    .line 547
    .end local v4    # "item":Landroid/view/MenuItem;
    :cond_1
    add-int/lit8 v2, v2, 0x1

    goto :goto_0

    .line 553
    .end local v2    # "h":I
    .end local v3    # "size":I
    :cond_2
    return-void
.end method
