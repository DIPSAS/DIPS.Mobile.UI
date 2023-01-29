.class public final Landroidx/navigation/ui/NavigationUI;
.super Ljava/lang/Object;
.source "NavigationUI.java"


# direct methods
.method private constructor <init>()V
    .locals 0

    .line 54
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 55
    return-void
.end method

.method static findBottomSheetBehavior(Landroid/view/View;)Lcom/google/android/material/bottomsheet/BottomSheetBehavior;
    .locals 4
    .param p0, "view"    # Landroid/view/View;

    .line 495
    invoke-virtual {p0}, Landroid/view/View;->getLayoutParams()Landroid/view/ViewGroup$LayoutParams;

    move-result-object v0

    .line 496
    .local v0, "params":Landroid/view/ViewGroup$LayoutParams;
    instance-of v1, v0, Landroidx/coordinatorlayout/widget/CoordinatorLayout$LayoutParams;

    const/4 v2, 0x0

    if-nez v1, :cond_1

    .line 497
    invoke-virtual {p0}, Landroid/view/View;->getParent()Landroid/view/ViewParent;

    move-result-object v1

    .line 498
    .local v1, "parent":Landroid/view/ViewParent;
    instance-of v3, v1, Landroid/view/View;

    if-eqz v3, :cond_0

    .line 499
    move-object v2, v1

    check-cast v2, Landroid/view/View;

    invoke-static {v2}, Landroidx/navigation/ui/NavigationUI;->findBottomSheetBehavior(Landroid/view/View;)Lcom/google/android/material/bottomsheet/BottomSheetBehavior;

    move-result-object v2

    return-object v2

    .line 501
    :cond_0
    return-object v2

    .line 503
    .end local v1    # "parent":Landroid/view/ViewParent;
    :cond_1
    move-object v1, v0

    check-cast v1, Landroidx/coordinatorlayout/widget/CoordinatorLayout$LayoutParams;

    .line 504
    invoke-virtual {v1}, Landroidx/coordinatorlayout/widget/CoordinatorLayout$LayoutParams;->getBehavior()Landroidx/coordinatorlayout/widget/CoordinatorLayout$Behavior;

    move-result-object v1

    .line 505
    .local v1, "behavior":Landroidx/coordinatorlayout/widget/CoordinatorLayout$Behavior;
    instance-of v3, v1, Lcom/google/android/material/bottomsheet/BottomSheetBehavior;

    if-nez v3, :cond_2

    .line 507
    return-object v2

    .line 509
    :cond_2
    move-object v2, v1

    check-cast v2, Lcom/google/android/material/bottomsheet/BottomSheetBehavior;

    return-object v2
.end method

.method static findStartDestination(Landroidx/navigation/NavGraph;)Landroidx/navigation/NavDestination;
    .locals 3
    .param p0, "graph"    # Landroidx/navigation/NavGraph;

    .line 596
    move-object v0, p0

    .line 597
    .local v0, "startDestination":Landroidx/navigation/NavDestination;
    :goto_0
    instance-of v1, v0, Landroidx/navigation/NavGraph;

    if-eqz v1, :cond_0

    .line 598
    move-object v1, v0

    check-cast v1, Landroidx/navigation/NavGraph;

    .line 599
    .local v1, "parent":Landroidx/navigation/NavGraph;
    invoke-virtual {v1}, Landroidx/navigation/NavGraph;->getStartDestination()I

    move-result v2

    invoke-virtual {v1, v2}, Landroidx/navigation/NavGraph;->findNode(I)Landroidx/navigation/NavDestination;

    move-result-object v0

    .line 600
    .end local v1    # "parent":Landroidx/navigation/NavGraph;
    goto :goto_0

    .line 601
    :cond_0
    return-object v0
.end method

.method static matchDestination(Landroidx/navigation/NavDestination;I)Z
    .locals 2
    .param p0, "destination"    # Landroidx/navigation/NavDestination;
    .param p1, "destId"    # I

    .line 565
    move-object v0, p0

    .line 566
    .local v0, "currentDestination":Landroidx/navigation/NavDestination;
    :goto_0
    invoke-virtual {v0}, Landroidx/navigation/NavDestination;->getId()I

    move-result v1

    if-eq v1, p1, :cond_0

    invoke-virtual {v0}, Landroidx/navigation/NavDestination;->getParent()Landroidx/navigation/NavGraph;

    move-result-object v1

    if-eqz v1, :cond_0

    .line 567
    invoke-virtual {v0}, Landroidx/navigation/NavDestination;->getParent()Landroidx/navigation/NavGraph;

    move-result-object v0

    goto :goto_0

    .line 569
    :cond_0
    invoke-virtual {v0}, Landroidx/navigation/NavDestination;->getId()I

    move-result v1

    if-ne v1, p1, :cond_1

    const/4 v1, 0x1

    goto :goto_1

    :cond_1
    const/4 v1, 0x0

    :goto_1
    return v1
.end method

.method static matchDestinations(Landroidx/navigation/NavDestination;Ljava/util/Set;)Z
    .locals 2
    .param p0, "destination"    # Landroidx/navigation/NavDestination;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Landroidx/navigation/NavDestination;",
            "Ljava/util/Set<",
            "Ljava/lang/Integer;",
            ">;)Z"
        }
    .end annotation

    .line 580
    .local p1, "destinationIds":Ljava/util/Set;, "Ljava/util/Set<Ljava/lang/Integer;>;"
    move-object v0, p0

    .line 582
    .local v0, "currentDestination":Landroidx/navigation/NavDestination;
    :goto_0
    invoke-virtual {v0}, Landroidx/navigation/NavDestination;->getId()I

    move-result v1

    invoke-static {v1}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v1

    invoke-interface {p1, v1}, Ljava/util/Set;->contains(Ljava/lang/Object;)Z

    move-result v1

    if-eqz v1, :cond_0

    .line 583
    const/4 v1, 0x1

    return v1

    .line 585
    :cond_0
    invoke-virtual {v0}, Landroidx/navigation/NavDestination;->getParent()Landroidx/navigation/NavGraph;

    move-result-object v0

    .line 586
    if-nez v0, :cond_1

    .line 587
    const/4 v1, 0x0

    return v1

    .line 586
    :cond_1
    goto :goto_0
.end method

.method public static navigateUp(Landroidx/navigation/NavController;Landroidx/customview/widget/Openable;)Z
    .locals 2
    .param p0, "navController"    # Landroidx/navigation/NavController;
    .param p1, "openableLayout"    # Landroidx/customview/widget/Openable;

    .line 117
    new-instance v0, Landroidx/navigation/ui/AppBarConfiguration$Builder;

    invoke-virtual {p0}, Landroidx/navigation/NavController;->getGraph()Landroidx/navigation/NavGraph;

    move-result-object v1

    invoke-direct {v0, v1}, Landroidx/navigation/ui/AppBarConfiguration$Builder;-><init>(Landroidx/navigation/NavGraph;)V

    .line 118
    invoke-virtual {v0, p1}, Landroidx/navigation/ui/AppBarConfiguration$Builder;->setOpenableLayout(Landroidx/customview/widget/Openable;)Landroidx/navigation/ui/AppBarConfiguration$Builder;

    move-result-object v0

    .line 119
    invoke-virtual {v0}, Landroidx/navigation/ui/AppBarConfiguration$Builder;->build()Landroidx/navigation/ui/AppBarConfiguration;

    move-result-object v0

    .line 117
    invoke-static {p0, v0}, Landroidx/navigation/ui/NavigationUI;->navigateUp(Landroidx/navigation/NavController;Landroidx/navigation/ui/AppBarConfiguration;)Z

    move-result v0

    return v0
.end method

.method public static navigateUp(Landroidx/navigation/NavController;Landroidx/navigation/ui/AppBarConfiguration;)Z
    .locals 5
    .param p0, "navController"    # Landroidx/navigation/NavController;
    .param p1, "configuration"    # Landroidx/navigation/ui/AppBarConfiguration;

    .line 139
    invoke-virtual {p1}, Landroidx/navigation/ui/AppBarConfiguration;->getOpenableLayout()Landroidx/customview/widget/Openable;

    move-result-object v0

    .line 140
    .local v0, "openableLayout":Landroidx/customview/widget/Openable;
    invoke-virtual {p0}, Landroidx/navigation/NavController;->getCurrentDestination()Landroidx/navigation/NavDestination;

    move-result-object v1

    .line 141
    .local v1, "currentDestination":Landroidx/navigation/NavDestination;
    invoke-virtual {p1}, Landroidx/navigation/ui/AppBarConfiguration;->getTopLevelDestinations()Ljava/util/Set;

    move-result-object v2

    .line 142
    .local v2, "topLevelDestinations":Ljava/util/Set;, "Ljava/util/Set<Ljava/lang/Integer;>;"
    const/4 v3, 0x1

    if-eqz v0, :cond_0

    if-eqz v1, :cond_0

    .line 143
    invoke-static {v1, v2}, Landroidx/navigation/ui/NavigationUI;->matchDestinations(Landroidx/navigation/NavDestination;Ljava/util/Set;)Z

    move-result v4

    if-eqz v4, :cond_0

    .line 144
    invoke-interface {v0}, Landroidx/customview/widget/Openable;->open()V

    .line 145
    return v3

    .line 147
    :cond_0
    invoke-virtual {p0}, Landroidx/navigation/NavController;->navigateUp()Z

    move-result v4

    if-eqz v4, :cond_1

    .line 148
    return v3

    .line 149
    :cond_1
    invoke-virtual {p1}, Landroidx/navigation/ui/AppBarConfiguration;->getFallbackOnNavigateUpListener()Landroidx/navigation/ui/AppBarConfiguration$OnNavigateUpListener;

    move-result-object v3

    if-eqz v3, :cond_2

    .line 150
    invoke-virtual {p1}, Landroidx/navigation/ui/AppBarConfiguration;->getFallbackOnNavigateUpListener()Landroidx/navigation/ui/AppBarConfiguration$OnNavigateUpListener;

    move-result-object v3

    invoke-interface {v3}, Landroidx/navigation/ui/AppBarConfiguration$OnNavigateUpListener;->onNavigateUp()Z

    move-result v3

    return v3

    .line 152
    :cond_2
    const/4 v3, 0x0

    return v3
.end method

.method public static onNavDestinationSelected(Landroid/view/MenuItem;Landroidx/navigation/NavController;)Z
    .locals 6
    .param p0, "item"    # Landroid/view/MenuItem;
    .param p1, "navController"    # Landroidx/navigation/NavController;

    .line 76
    new-instance v0, Landroidx/navigation/NavOptions$Builder;

    invoke-direct {v0}, Landroidx/navigation/NavOptions$Builder;-><init>()V

    .line 77
    const/4 v1, 0x1

    invoke-virtual {v0, v1}, Landroidx/navigation/NavOptions$Builder;->setLaunchSingleTop(Z)Landroidx/navigation/NavOptions$Builder;

    move-result-object v0

    .line 78
    .local v0, "builder":Landroidx/navigation/NavOptions$Builder;
    invoke-virtual {p1}, Landroidx/navigation/NavController;->getCurrentDestination()Landroidx/navigation/NavDestination;

    move-result-object v2

    invoke-virtual {v2}, Landroidx/navigation/NavDestination;->getParent()Landroidx/navigation/NavGraph;

    move-result-object v2

    invoke-interface {p0}, Landroid/view/MenuItem;->getItemId()I

    move-result v3

    invoke-virtual {v2, v3}, Landroidx/navigation/NavGraph;->findNode(I)Landroidx/navigation/NavDestination;

    move-result-object v2

    instance-of v2, v2, Landroidx/navigation/ActivityNavigator$Destination;

    if-eqz v2, :cond_0

    .line 80
    sget v2, Landroidx/navigation/ui/R$anim;->nav_default_enter_anim:I

    invoke-virtual {v0, v2}, Landroidx/navigation/NavOptions$Builder;->setEnterAnim(I)Landroidx/navigation/NavOptions$Builder;

    move-result-object v2

    sget v3, Landroidx/navigation/ui/R$anim;->nav_default_exit_anim:I

    .line 81
    invoke-virtual {v2, v3}, Landroidx/navigation/NavOptions$Builder;->setExitAnim(I)Landroidx/navigation/NavOptions$Builder;

    move-result-object v2

    sget v3, Landroidx/navigation/ui/R$anim;->nav_default_pop_enter_anim:I

    .line 82
    invoke-virtual {v2, v3}, Landroidx/navigation/NavOptions$Builder;->setPopEnterAnim(I)Landroidx/navigation/NavOptions$Builder;

    move-result-object v2

    sget v3, Landroidx/navigation/ui/R$anim;->nav_default_pop_exit_anim:I

    .line 83
    invoke-virtual {v2, v3}, Landroidx/navigation/NavOptions$Builder;->setPopExitAnim(I)Landroidx/navigation/NavOptions$Builder;

    goto :goto_0

    .line 86
    :cond_0
    sget v2, Landroidx/navigation/ui/R$animator;->nav_default_enter_anim:I

    invoke-virtual {v0, v2}, Landroidx/navigation/NavOptions$Builder;->setEnterAnim(I)Landroidx/navigation/NavOptions$Builder;

    move-result-object v2

    sget v3, Landroidx/navigation/ui/R$animator;->nav_default_exit_anim:I

    .line 87
    invoke-virtual {v2, v3}, Landroidx/navigation/NavOptions$Builder;->setExitAnim(I)Landroidx/navigation/NavOptions$Builder;

    move-result-object v2

    sget v3, Landroidx/navigation/ui/R$animator;->nav_default_pop_enter_anim:I

    .line 88
    invoke-virtual {v2, v3}, Landroidx/navigation/NavOptions$Builder;->setPopEnterAnim(I)Landroidx/navigation/NavOptions$Builder;

    move-result-object v2

    sget v3, Landroidx/navigation/ui/R$animator;->nav_default_pop_exit_anim:I

    .line 89
    invoke-virtual {v2, v3}, Landroidx/navigation/NavOptions$Builder;->setPopExitAnim(I)Landroidx/navigation/NavOptions$Builder;

    .line 91
    :goto_0
    invoke-interface {p0}, Landroid/view/MenuItem;->getOrder()I

    move-result v2

    const/high16 v3, 0x30000

    and-int/2addr v2, v3

    const/4 v3, 0x0

    if-nez v2, :cond_1

    .line 92
    invoke-virtual {p1}, Landroidx/navigation/NavController;->getGraph()Landroidx/navigation/NavGraph;

    move-result-object v2

    invoke-static {v2}, Landroidx/navigation/ui/NavigationUI;->findStartDestination(Landroidx/navigation/NavGraph;)Landroidx/navigation/NavDestination;

    move-result-object v2

    invoke-virtual {v2}, Landroidx/navigation/NavDestination;->getId()I

    move-result v2

    invoke-virtual {v0, v2, v3}, Landroidx/navigation/NavOptions$Builder;->setPopUpTo(IZ)Landroidx/navigation/NavOptions$Builder;

    .line 94
    :cond_1
    invoke-virtual {v0}, Landroidx/navigation/NavOptions$Builder;->build()Landroidx/navigation/NavOptions;

    move-result-object v2

    .line 97
    .local v2, "options":Landroidx/navigation/NavOptions;
    :try_start_0
    invoke-interface {p0}, Landroid/view/MenuItem;->getItemId()I

    move-result v4

    const/4 v5, 0x0

    invoke-virtual {p1, v4, v5, v2}, Landroidx/navigation/NavController;->navigate(ILandroid/os/Bundle;Landroidx/navigation/NavOptions;)V
    :try_end_0
    .catch Ljava/lang/IllegalArgumentException; {:try_start_0 .. :try_end_0} :catch_0

    .line 98
    return v1

    .line 99
    :catch_0
    move-exception v1

    .line 100
    .local v1, "e":Ljava/lang/IllegalArgumentException;
    return v3
.end method

.method public static setupActionBarWithNavController(Landroidx/appcompat/app/AppCompatActivity;Landroidx/navigation/NavController;)V
    .locals 2
    .param p0, "activity"    # Landroidx/appcompat/app/AppCompatActivity;
    .param p1, "navController"    # Landroidx/navigation/NavController;

    .line 178
    new-instance v0, Landroidx/navigation/ui/AppBarConfiguration$Builder;

    .line 179
    invoke-virtual {p1}, Landroidx/navigation/NavController;->getGraph()Landroidx/navigation/NavGraph;

    move-result-object v1

    invoke-direct {v0, v1}, Landroidx/navigation/ui/AppBarConfiguration$Builder;-><init>(Landroidx/navigation/NavGraph;)V

    .line 180
    invoke-virtual {v0}, Landroidx/navigation/ui/AppBarConfiguration$Builder;->build()Landroidx/navigation/ui/AppBarConfiguration;

    move-result-object v0

    .line 178
    invoke-static {p0, p1, v0}, Landroidx/navigation/ui/NavigationUI;->setupActionBarWithNavController(Landroidx/appcompat/app/AppCompatActivity;Landroidx/navigation/NavController;Landroidx/navigation/ui/AppBarConfiguration;)V

    .line 181
    return-void
.end method

.method public static setupActionBarWithNavController(Landroidx/appcompat/app/AppCompatActivity;Landroidx/navigation/NavController;Landroidx/customview/widget/Openable;)V
    .locals 2
    .param p0, "activity"    # Landroidx/appcompat/app/AppCompatActivity;
    .param p1, "navController"    # Landroidx/navigation/NavController;
    .param p2, "openableLayout"    # Landroidx/customview/widget/Openable;

    .line 208
    new-instance v0, Landroidx/navigation/ui/AppBarConfiguration$Builder;

    .line 209
    invoke-virtual {p1}, Landroidx/navigation/NavController;->getGraph()Landroidx/navigation/NavGraph;

    move-result-object v1

    invoke-direct {v0, v1}, Landroidx/navigation/ui/AppBarConfiguration$Builder;-><init>(Landroidx/navigation/NavGraph;)V

    .line 210
    invoke-virtual {v0, p2}, Landroidx/navigation/ui/AppBarConfiguration$Builder;->setOpenableLayout(Landroidx/customview/widget/Openable;)Landroidx/navigation/ui/AppBarConfiguration$Builder;

    move-result-object v0

    .line 211
    invoke-virtual {v0}, Landroidx/navigation/ui/AppBarConfiguration$Builder;->build()Landroidx/navigation/ui/AppBarConfiguration;

    move-result-object v0

    .line 208
    invoke-static {p0, p1, v0}, Landroidx/navigation/ui/NavigationUI;->setupActionBarWithNavController(Landroidx/appcompat/app/AppCompatActivity;Landroidx/navigation/NavController;Landroidx/navigation/ui/AppBarConfiguration;)V

    .line 212
    return-void
.end method

.method public static setupActionBarWithNavController(Landroidx/appcompat/app/AppCompatActivity;Landroidx/navigation/NavController;Landroidx/navigation/ui/AppBarConfiguration;)V
    .locals 1
    .param p0, "activity"    # Landroidx/appcompat/app/AppCompatActivity;
    .param p1, "navController"    # Landroidx/navigation/NavController;
    .param p2, "configuration"    # Landroidx/navigation/ui/AppBarConfiguration;

    .line 237
    new-instance v0, Landroidx/navigation/ui/ActionBarOnDestinationChangedListener;

    invoke-direct {v0, p0, p2}, Landroidx/navigation/ui/ActionBarOnDestinationChangedListener;-><init>(Landroidx/appcompat/app/AppCompatActivity;Landroidx/navigation/ui/AppBarConfiguration;)V

    invoke-virtual {p1, v0}, Landroidx/navigation/NavController;->addOnDestinationChangedListener(Landroidx/navigation/NavController$OnDestinationChangedListener;)V

    .line 239
    return-void
.end method

.method public static setupWithNavController(Landroidx/appcompat/widget/Toolbar;Landroidx/navigation/NavController;)V
    .locals 2
    .param p0, "toolbar"    # Landroidx/appcompat/widget/Toolbar;
    .param p1, "navController"    # Landroidx/navigation/NavController;

    .line 261
    new-instance v0, Landroidx/navigation/ui/AppBarConfiguration$Builder;

    .line 262
    invoke-virtual {p1}, Landroidx/navigation/NavController;->getGraph()Landroidx/navigation/NavGraph;

    move-result-object v1

    invoke-direct {v0, v1}, Landroidx/navigation/ui/AppBarConfiguration$Builder;-><init>(Landroidx/navigation/NavGraph;)V

    invoke-virtual {v0}, Landroidx/navigation/ui/AppBarConfiguration$Builder;->build()Landroidx/navigation/ui/AppBarConfiguration;

    move-result-object v0

    .line 261
    invoke-static {p0, p1, v0}, Landroidx/navigation/ui/NavigationUI;->setupWithNavController(Landroidx/appcompat/widget/Toolbar;Landroidx/navigation/NavController;Landroidx/navigation/ui/AppBarConfiguration;)V

    .line 263
    return-void
.end method

.method public static setupWithNavController(Landroidx/appcompat/widget/Toolbar;Landroidx/navigation/NavController;Landroidx/customview/widget/Openable;)V
    .locals 2
    .param p0, "toolbar"    # Landroidx/appcompat/widget/Toolbar;
    .param p1, "navController"    # Landroidx/navigation/NavController;
    .param p2, "openableLayout"    # Landroidx/customview/widget/Openable;

    .line 288
    new-instance v0, Landroidx/navigation/ui/AppBarConfiguration$Builder;

    .line 289
    invoke-virtual {p1}, Landroidx/navigation/NavController;->getGraph()Landroidx/navigation/NavGraph;

    move-result-object v1

    invoke-direct {v0, v1}, Landroidx/navigation/ui/AppBarConfiguration$Builder;-><init>(Landroidx/navigation/NavGraph;)V

    .line 290
    invoke-virtual {v0, p2}, Landroidx/navigation/ui/AppBarConfiguration$Builder;->setOpenableLayout(Landroidx/customview/widget/Openable;)Landroidx/navigation/ui/AppBarConfiguration$Builder;

    move-result-object v0

    .line 291
    invoke-virtual {v0}, Landroidx/navigation/ui/AppBarConfiguration$Builder;->build()Landroidx/navigation/ui/AppBarConfiguration;

    move-result-object v0

    .line 288
    invoke-static {p0, p1, v0}, Landroidx/navigation/ui/NavigationUI;->setupWithNavController(Landroidx/appcompat/widget/Toolbar;Landroidx/navigation/NavController;Landroidx/navigation/ui/AppBarConfiguration;)V

    .line 292
    return-void
.end method

.method public static setupWithNavController(Landroidx/appcompat/widget/Toolbar;Landroidx/navigation/NavController;Landroidx/navigation/ui/AppBarConfiguration;)V
    .locals 1
    .param p0, "toolbar"    # Landroidx/appcompat/widget/Toolbar;
    .param p1, "navController"    # Landroidx/navigation/NavController;
    .param p2, "configuration"    # Landroidx/navigation/ui/AppBarConfiguration;

    .line 316
    new-instance v0, Landroidx/navigation/ui/ToolbarOnDestinationChangedListener;

    invoke-direct {v0, p0, p2}, Landroidx/navigation/ui/ToolbarOnDestinationChangedListener;-><init>(Landroidx/appcompat/widget/Toolbar;Landroidx/navigation/ui/AppBarConfiguration;)V

    invoke-virtual {p1, v0}, Landroidx/navigation/NavController;->addOnDestinationChangedListener(Landroidx/navigation/NavController$OnDestinationChangedListener;)V

    .line 318
    new-instance v0, Landroidx/navigation/ui/NavigationUI$1;

    invoke-direct {v0, p1, p2}, Landroidx/navigation/ui/NavigationUI$1;-><init>(Landroidx/navigation/NavController;Landroidx/navigation/ui/AppBarConfiguration;)V

    invoke-virtual {p0, v0}, Landroidx/appcompat/widget/Toolbar;->setNavigationOnClickListener(Landroid/view/View$OnClickListener;)V

    .line 324
    return-void
.end method

.method public static setupWithNavController(Lcom/google/android/material/appbar/CollapsingToolbarLayout;Landroidx/appcompat/widget/Toolbar;Landroidx/navigation/NavController;)V
    .locals 2
    .param p0, "collapsingToolbarLayout"    # Lcom/google/android/material/appbar/CollapsingToolbarLayout;
    .param p1, "toolbar"    # Landroidx/appcompat/widget/Toolbar;
    .param p2, "navController"    # Landroidx/navigation/NavController;

    .line 351
    new-instance v0, Landroidx/navigation/ui/AppBarConfiguration$Builder;

    .line 352
    invoke-virtual {p2}, Landroidx/navigation/NavController;->getGraph()Landroidx/navigation/NavGraph;

    move-result-object v1

    invoke-direct {v0, v1}, Landroidx/navigation/ui/AppBarConfiguration$Builder;-><init>(Landroidx/navigation/NavGraph;)V

    invoke-virtual {v0}, Landroidx/navigation/ui/AppBarConfiguration$Builder;->build()Landroidx/navigation/ui/AppBarConfiguration;

    move-result-object v0

    .line 351
    invoke-static {p0, p1, p2, v0}, Landroidx/navigation/ui/NavigationUI;->setupWithNavController(Lcom/google/android/material/appbar/CollapsingToolbarLayout;Landroidx/appcompat/widget/Toolbar;Landroidx/navigation/NavController;Landroidx/navigation/ui/AppBarConfiguration;)V

    .line 353
    return-void
.end method

.method public static setupWithNavController(Lcom/google/android/material/appbar/CollapsingToolbarLayout;Landroidx/appcompat/widget/Toolbar;Landroidx/navigation/NavController;Landroidx/customview/widget/Openable;)V
    .locals 2
    .param p0, "collapsingToolbarLayout"    # Lcom/google/android/material/appbar/CollapsingToolbarLayout;
    .param p1, "toolbar"    # Landroidx/appcompat/widget/Toolbar;
    .param p2, "navController"    # Landroidx/navigation/NavController;
    .param p3, "openableLayout"    # Landroidx/customview/widget/Openable;

    .line 383
    new-instance v0, Landroidx/navigation/ui/AppBarConfiguration$Builder;

    .line 384
    invoke-virtual {p2}, Landroidx/navigation/NavController;->getGraph()Landroidx/navigation/NavGraph;

    move-result-object v1

    invoke-direct {v0, v1}, Landroidx/navigation/ui/AppBarConfiguration$Builder;-><init>(Landroidx/navigation/NavGraph;)V

    .line 385
    invoke-virtual {v0, p3}, Landroidx/navigation/ui/AppBarConfiguration$Builder;->setOpenableLayout(Landroidx/customview/widget/Openable;)Landroidx/navigation/ui/AppBarConfiguration$Builder;

    move-result-object v0

    .line 386
    invoke-virtual {v0}, Landroidx/navigation/ui/AppBarConfiguration$Builder;->build()Landroidx/navigation/ui/AppBarConfiguration;

    move-result-object v0

    .line 383
    invoke-static {p0, p1, p2, v0}, Landroidx/navigation/ui/NavigationUI;->setupWithNavController(Lcom/google/android/material/appbar/CollapsingToolbarLayout;Landroidx/appcompat/widget/Toolbar;Landroidx/navigation/NavController;Landroidx/navigation/ui/AppBarConfiguration;)V

    .line 387
    return-void
.end method

.method public static setupWithNavController(Lcom/google/android/material/appbar/CollapsingToolbarLayout;Landroidx/appcompat/widget/Toolbar;Landroidx/navigation/NavController;Landroidx/navigation/ui/AppBarConfiguration;)V
    .locals 1
    .param p0, "collapsingToolbarLayout"    # Lcom/google/android/material/appbar/CollapsingToolbarLayout;
    .param p1, "toolbar"    # Landroidx/appcompat/widget/Toolbar;
    .param p2, "navController"    # Landroidx/navigation/NavController;
    .param p3, "configuration"    # Landroidx/navigation/ui/AppBarConfiguration;

    .line 417
    new-instance v0, Landroidx/navigation/ui/CollapsingToolbarOnDestinationChangedListener;

    invoke-direct {v0, p0, p1, p3}, Landroidx/navigation/ui/CollapsingToolbarOnDestinationChangedListener;-><init>(Lcom/google/android/material/appbar/CollapsingToolbarLayout;Landroidx/appcompat/widget/Toolbar;Landroidx/navigation/ui/AppBarConfiguration;)V

    invoke-virtual {p2, v0}, Landroidx/navigation/NavController;->addOnDestinationChangedListener(Landroidx/navigation/NavController$OnDestinationChangedListener;)V

    .line 420
    new-instance v0, Landroidx/navigation/ui/NavigationUI$2;

    invoke-direct {v0, p2, p3}, Landroidx/navigation/ui/NavigationUI$2;-><init>(Landroidx/navigation/NavController;Landroidx/navigation/ui/AppBarConfiguration;)V

    invoke-virtual {p1, v0}, Landroidx/appcompat/widget/Toolbar;->setNavigationOnClickListener(Landroid/view/View$OnClickListener;)V

    .line 426
    return-void
.end method

.method public static setupWithNavController(Lcom/google/android/material/bottomnavigation/BottomNavigationView;Landroidx/navigation/NavController;)V
    .locals 2
    .param p0, "bottomNavigationView"    # Lcom/google/android/material/bottomnavigation/BottomNavigationView;
    .param p1, "navController"    # Landroidx/navigation/NavController;

    .line 527
    new-instance v0, Landroidx/navigation/ui/NavigationUI$5;

    invoke-direct {v0, p1}, Landroidx/navigation/ui/NavigationUI$5;-><init>(Landroidx/navigation/NavController;)V

    invoke-virtual {p0, v0}, Lcom/google/android/material/bottomnavigation/BottomNavigationView;->setOnNavigationItemSelectedListener(Lcom/google/android/material/bottomnavigation/BottomNavigationView$OnNavigationItemSelectedListener;)V

    .line 534
    new-instance v0, Ljava/lang/ref/WeakReference;

    invoke-direct {v0, p0}, Ljava/lang/ref/WeakReference;-><init>(Ljava/lang/Object;)V

    .line 536
    .local v0, "weakReference":Ljava/lang/ref/WeakReference;, "Ljava/lang/ref/WeakReference<Lcom/google/android/material/bottomnavigation/BottomNavigationView;>;"
    new-instance v1, Landroidx/navigation/ui/NavigationUI$6;

    invoke-direct {v1, v0, p1}, Landroidx/navigation/ui/NavigationUI$6;-><init>(Ljava/lang/ref/WeakReference;Landroidx/navigation/NavController;)V

    invoke-virtual {p1, v1}, Landroidx/navigation/NavController;->addOnDestinationChangedListener(Landroidx/navigation/NavController$OnDestinationChangedListener;)V

    .line 555
    return-void
.end method

.method public static setupWithNavController(Lcom/google/android/material/navigation/NavigationView;Landroidx/navigation/NavController;)V
    .locals 2
    .param p0, "navigationView"    # Lcom/google/android/material/navigation/NavigationView;
    .param p1, "navController"    # Landroidx/navigation/NavController;

    .line 449
    new-instance v0, Landroidx/navigation/ui/NavigationUI$3;

    invoke-direct {v0, p1, p0}, Landroidx/navigation/ui/NavigationUI$3;-><init>(Landroidx/navigation/NavController;Lcom/google/android/material/navigation/NavigationView;)V

    invoke-virtual {p0, v0}, Lcom/google/android/material/navigation/NavigationView;->setNavigationItemSelectedListener(Lcom/google/android/material/navigation/NavigationView$OnNavigationItemSelectedListener;)V

    .line 469
    new-instance v0, Ljava/lang/ref/WeakReference;

    invoke-direct {v0, p0}, Ljava/lang/ref/WeakReference;-><init>(Ljava/lang/Object;)V

    .line 470
    .local v0, "weakReference":Ljava/lang/ref/WeakReference;, "Ljava/lang/ref/WeakReference<Lcom/google/android/material/navigation/NavigationView;>;"
    new-instance v1, Landroidx/navigation/ui/NavigationUI$4;

    invoke-direct {v1, v0, p1}, Landroidx/navigation/ui/NavigationUI$4;-><init>(Ljava/lang/ref/WeakReference;Landroidx/navigation/NavController;)V

    invoke-virtual {p1, v1}, Landroidx/navigation/NavController;->addOnDestinationChangedListener(Landroidx/navigation/NavController$OnDestinationChangedListener;)V

    .line 487
    return-void
.end method
