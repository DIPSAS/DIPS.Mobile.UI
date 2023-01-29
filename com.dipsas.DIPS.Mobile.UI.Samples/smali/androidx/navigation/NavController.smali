.class public Landroidx/navigation/NavController;
.super Ljava/lang/Object;
.source "NavController.java"


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Landroidx/navigation/NavController$OnDestinationChangedListener;
    }
.end annotation


# static fields
.field private static final KEY_BACK_STACK:Ljava/lang/String; = "android-support-nav:controller:backStack"

.field static final KEY_DEEP_LINK_EXTRAS:Ljava/lang/String; = "android-support-nav:controller:deepLinkExtras"

.field static final KEY_DEEP_LINK_HANDLED:Ljava/lang/String; = "android-support-nav:controller:deepLinkHandled"

.field static final KEY_DEEP_LINK_IDS:Ljava/lang/String; = "android-support-nav:controller:deepLinkIds"

.field public static final KEY_DEEP_LINK_INTENT:Ljava/lang/String; = "android-support-nav:controller:deepLinkIntent"

.field private static final KEY_NAVIGATOR_STATE:Ljava/lang/String; = "android-support-nav:controller:navigatorState"

.field private static final KEY_NAVIGATOR_STATE_NAMES:Ljava/lang/String; = "android-support-nav:controller:navigatorState:names"

.field private static final TAG:Ljava/lang/String; = "NavController"


# instance fields
.field private mActivity:Landroid/app/Activity;

.field final mBackStack:Ljava/util/Deque;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/Deque<",
            "Landroidx/navigation/NavBackStackEntry;",
            ">;"
        }
    .end annotation
.end field

.field private mBackStackToRestore:[Landroid/os/Parcelable;

.field private final mContext:Landroid/content/Context;

.field private mDeepLinkHandled:Z

.field private mEnableOnBackPressedCallback:Z

.field mGraph:Landroidx/navigation/NavGraph;

.field private mInflater:Landroidx/navigation/NavInflater;

.field private final mLifecycleObserver:Landroidx/lifecycle/LifecycleObserver;

.field private mLifecycleOwner:Landroidx/lifecycle/LifecycleOwner;

.field private mNavigatorProvider:Landroidx/navigation/NavigatorProvider;

.field private mNavigatorStateToRestore:Landroid/os/Bundle;

.field private final mOnBackPressedCallback:Landroidx/activity/OnBackPressedCallback;

.field private final mOnDestinationChangedListeners:Ljava/util/concurrent/CopyOnWriteArrayList;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/concurrent/CopyOnWriteArrayList<",
            "Landroidx/navigation/NavController$OnDestinationChangedListener;",
            ">;"
        }
    .end annotation
.end field

.field private mViewModel:Landroidx/navigation/NavControllerViewModel;


# direct methods
.method public constructor <init>(Landroid/content/Context;)V
    .locals 3
    .param p1, "context"    # Landroid/content/Context;

    .line 160
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 93
    new-instance v0, Ljava/util/ArrayDeque;

    invoke-direct {v0}, Ljava/util/ArrayDeque;-><init>()V

    iput-object v0, p0, Landroidx/navigation/NavController;->mBackStack:Ljava/util/Deque;

    .line 99
    new-instance v0, Landroidx/navigation/NavigatorProvider;

    invoke-direct {v0}, Landroidx/navigation/NavigatorProvider;-><init>()V

    iput-object v0, p0, Landroidx/navigation/NavController;->mNavigatorProvider:Landroidx/navigation/NavigatorProvider;

    .line 101
    new-instance v0, Ljava/util/concurrent/CopyOnWriteArrayList;

    invoke-direct {v0}, Ljava/util/concurrent/CopyOnWriteArrayList;-><init>()V

    iput-object v0, p0, Landroidx/navigation/NavController;->mOnDestinationChangedListeners:Ljava/util/concurrent/CopyOnWriteArrayList;

    .line 104
    new-instance v0, Landroidx/navigation/NavController$1;

    invoke-direct {v0, p0}, Landroidx/navigation/NavController$1;-><init>(Landroidx/navigation/NavController;)V

    iput-object v0, p0, Landroidx/navigation/NavController;->mLifecycleObserver:Landroidx/lifecycle/LifecycleObserver;

    .line 115
    new-instance v0, Landroidx/navigation/NavController$2;

    const/4 v1, 0x0

    invoke-direct {v0, p0, v1}, Landroidx/navigation/NavController$2;-><init>(Landroidx/navigation/NavController;Z)V

    iput-object v0, p0, Landroidx/navigation/NavController;->mOnBackPressedCallback:Landroidx/activity/OnBackPressedCallback;

    .line 122
    const/4 v0, 0x1

    iput-boolean v0, p0, Landroidx/navigation/NavController;->mEnableOnBackPressedCallback:Z

    .line 161
    iput-object p1, p0, Landroidx/navigation/NavController;->mContext:Landroid/content/Context;

    .line 162
    :goto_0
    instance-of v0, p1, Landroid/content/ContextWrapper;

    if-eqz v0, :cond_1

    .line 163
    instance-of v0, p1, Landroid/app/Activity;

    if-eqz v0, :cond_0

    .line 164
    move-object v0, p1

    check-cast v0, Landroid/app/Activity;

    iput-object v0, p0, Landroidx/navigation/NavController;->mActivity:Landroid/app/Activity;

    .line 165
    goto :goto_1

    .line 167
    :cond_0
    move-object v0, p1

    check-cast v0, Landroid/content/ContextWrapper;

    invoke-virtual {v0}, Landroid/content/ContextWrapper;->getBaseContext()Landroid/content/Context;

    move-result-object p1

    goto :goto_0

    .line 169
    :cond_1
    :goto_1
    iget-object v0, p0, Landroidx/navigation/NavController;->mNavigatorProvider:Landroidx/navigation/NavigatorProvider;

    new-instance v1, Landroidx/navigation/NavGraphNavigator;

    invoke-direct {v1, v0}, Landroidx/navigation/NavGraphNavigator;-><init>(Landroidx/navigation/NavigatorProvider;)V

    invoke-virtual {v0, v1}, Landroidx/navigation/NavigatorProvider;->addNavigator(Landroidx/navigation/Navigator;)Landroidx/navigation/Navigator;

    .line 170
    iget-object v0, p0, Landroidx/navigation/NavController;->mNavigatorProvider:Landroidx/navigation/NavigatorProvider;

    new-instance v1, Landroidx/navigation/ActivityNavigator;

    iget-object v2, p0, Landroidx/navigation/NavController;->mContext:Landroid/content/Context;

    invoke-direct {v1, v2}, Landroidx/navigation/ActivityNavigator;-><init>(Landroid/content/Context;)V

    invoke-virtual {v0, v1}, Landroidx/navigation/NavigatorProvider;->addNavigator(Landroidx/navigation/Navigator;)Landroidx/navigation/Navigator;

    .line 171
    return-void
.end method

.method private dispatchOnDestinationChanged()Z
    .locals 10

    .line 430
    :goto_0
    iget-object v0, p0, Landroidx/navigation/NavController;->mBackStack:Ljava/util/Deque;

    invoke-interface {v0}, Ljava/util/Deque;->isEmpty()Z

    move-result v0

    const/4 v1, 0x1

    if-nez v0, :cond_0

    iget-object v0, p0, Landroidx/navigation/NavController;->mBackStack:Ljava/util/Deque;

    .line 431
    invoke-interface {v0}, Ljava/util/Deque;->peekLast()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Landroidx/navigation/NavBackStackEntry;

    invoke-virtual {v0}, Landroidx/navigation/NavBackStackEntry;->getDestination()Landroidx/navigation/NavDestination;

    move-result-object v0

    instance-of v0, v0, Landroidx/navigation/NavGraph;

    if-eqz v0, :cond_0

    iget-object v0, p0, Landroidx/navigation/NavController;->mBackStack:Ljava/util/Deque;

    .line 432
    invoke-interface {v0}, Ljava/util/Deque;->peekLast()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Landroidx/navigation/NavBackStackEntry;

    invoke-virtual {v0}, Landroidx/navigation/NavBackStackEntry;->getDestination()Landroidx/navigation/NavDestination;

    move-result-object v0

    invoke-virtual {v0}, Landroidx/navigation/NavDestination;->getId()I

    move-result v0

    invoke-virtual {p0, v0, v1}, Landroidx/navigation/NavController;->popBackStackInternal(IZ)Z

    move-result v0

    if-eqz v0, :cond_0

    goto :goto_0

    .line 435
    :cond_0
    iget-object v0, p0, Landroidx/navigation/NavController;->mBackStack:Ljava/util/Deque;

    invoke-interface {v0}, Ljava/util/Deque;->isEmpty()Z

    move-result v0

    if-nez v0, :cond_c

    .line 439
    iget-object v0, p0, Landroidx/navigation/NavController;->mBackStack:Ljava/util/Deque;

    invoke-interface {v0}, Ljava/util/Deque;->peekLast()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Landroidx/navigation/NavBackStackEntry;

    invoke-virtual {v0}, Landroidx/navigation/NavBackStackEntry;->getDestination()Landroidx/navigation/NavDestination;

    move-result-object v0

    .line 440
    .local v0, "nextResumed":Landroidx/navigation/NavDestination;
    const/4 v2, 0x0

    .line 441
    .local v2, "nextStarted":Landroidx/navigation/NavDestination;
    instance-of v3, v0, Landroidx/navigation/FloatingWindow;

    if-eqz v3, :cond_2

    .line 444
    iget-object v3, p0, Landroidx/navigation/NavController;->mBackStack:Ljava/util/Deque;

    invoke-interface {v3}, Ljava/util/Deque;->descendingIterator()Ljava/util/Iterator;

    move-result-object v3

    .line 445
    .local v3, "iterator":Ljava/util/Iterator;, "Ljava/util/Iterator<Landroidx/navigation/NavBackStackEntry;>;"
    :goto_1
    invoke-interface {v3}, Ljava/util/Iterator;->hasNext()Z

    move-result v4

    if-eqz v4, :cond_2

    .line 446
    invoke-interface {v3}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v4

    check-cast v4, Landroidx/navigation/NavBackStackEntry;

    invoke-virtual {v4}, Landroidx/navigation/NavBackStackEntry;->getDestination()Landroidx/navigation/NavDestination;

    move-result-object v4

    .line 447
    .local v4, "destination":Landroidx/navigation/NavDestination;
    instance-of v5, v4, Landroidx/navigation/NavGraph;

    if-nez v5, :cond_1

    instance-of v5, v4, Landroidx/navigation/FloatingWindow;

    if-nez v5, :cond_1

    .line 449
    move-object v2, v4

    .line 450
    goto :goto_2

    .line 452
    .end local v4    # "destination":Landroidx/navigation/NavDestination;
    :cond_1
    goto :goto_1

    .line 458
    .end local v3    # "iterator":Ljava/util/Iterator;, "Ljava/util/Iterator<Landroidx/navigation/NavBackStackEntry;>;"
    :cond_2
    :goto_2
    new-instance v3, Ljava/util/HashMap;

    invoke-direct {v3}, Ljava/util/HashMap;-><init>()V

    .line 459
    .local v3, "upwardStateTransitions":Ljava/util/HashMap;, "Ljava/util/HashMap<Landroidx/navigation/NavBackStackEntry;Landroidx/lifecycle/Lifecycle$State;>;"
    iget-object v4, p0, Landroidx/navigation/NavController;->mBackStack:Ljava/util/Deque;

    invoke-interface {v4}, Ljava/util/Deque;->descendingIterator()Ljava/util/Iterator;

    move-result-object v4

    .line 460
    .local v4, "iterator":Ljava/util/Iterator;, "Ljava/util/Iterator<Landroidx/navigation/NavBackStackEntry;>;"
    :goto_3
    invoke-interface {v4}, Ljava/util/Iterator;->hasNext()Z

    move-result v5

    if-eqz v5, :cond_8

    .line 461
    invoke-interface {v4}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v5

    check-cast v5, Landroidx/navigation/NavBackStackEntry;

    .line 462
    .local v5, "entry":Landroidx/navigation/NavBackStackEntry;
    invoke-virtual {v5}, Landroidx/navigation/NavBackStackEntry;->getMaxLifecycle()Landroidx/lifecycle/Lifecycle$State;

    move-result-object v6

    .line 463
    .local v6, "currentMaxLifecycle":Landroidx/lifecycle/Lifecycle$State;
    invoke-virtual {v5}, Landroidx/navigation/NavBackStackEntry;->getDestination()Landroidx/navigation/NavDestination;

    move-result-object v7

    .line 464
    .local v7, "destination":Landroidx/navigation/NavDestination;
    if-eqz v0, :cond_4

    invoke-virtual {v7}, Landroidx/navigation/NavDestination;->getId()I

    move-result v8

    invoke-virtual {v0}, Landroidx/navigation/NavDestination;->getId()I

    move-result v9

    if-ne v8, v9, :cond_4

    .line 467
    sget-object v8, Landroidx/lifecycle/Lifecycle$State;->RESUMED:Landroidx/lifecycle/Lifecycle$State;

    if-eq v6, v8, :cond_3

    .line 468
    sget-object v8, Landroidx/lifecycle/Lifecycle$State;->RESUMED:Landroidx/lifecycle/Lifecycle$State;

    invoke-virtual {v3, v5, v8}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 470
    :cond_3
    invoke-virtual {v0}, Landroidx/navigation/NavDestination;->getParent()Landroidx/navigation/NavGraph;

    move-result-object v0

    goto :goto_5

    .line 471
    :cond_4
    if-eqz v2, :cond_7

    invoke-virtual {v7}, Landroidx/navigation/NavDestination;->getId()I

    move-result v8

    invoke-virtual {v2}, Landroidx/navigation/NavDestination;->getId()I

    move-result v9

    if-ne v8, v9, :cond_7

    .line 472
    sget-object v8, Landroidx/lifecycle/Lifecycle$State;->RESUMED:Landroidx/lifecycle/Lifecycle$State;

    if-ne v6, v8, :cond_5

    .line 475
    sget-object v8, Landroidx/lifecycle/Lifecycle$State;->STARTED:Landroidx/lifecycle/Lifecycle$State;

    invoke-virtual {v5, v8}, Landroidx/navigation/NavBackStackEntry;->setMaxLifecycle(Landroidx/lifecycle/Lifecycle$State;)V

    goto :goto_4

    .line 476
    :cond_5
    sget-object v8, Landroidx/lifecycle/Lifecycle$State;->STARTED:Landroidx/lifecycle/Lifecycle$State;

    if-eq v6, v8, :cond_6

    .line 479
    sget-object v8, Landroidx/lifecycle/Lifecycle$State;->STARTED:Landroidx/lifecycle/Lifecycle$State;

    invoke-virtual {v3, v5, v8}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 481
    :cond_6
    :goto_4
    invoke-virtual {v2}, Landroidx/navigation/NavDestination;->getParent()Landroidx/navigation/NavGraph;

    move-result-object v2

    goto :goto_5

    .line 483
    :cond_7
    sget-object v8, Landroidx/lifecycle/Lifecycle$State;->CREATED:Landroidx/lifecycle/Lifecycle$State;

    invoke-virtual {v5, v8}, Landroidx/navigation/NavBackStackEntry;->setMaxLifecycle(Landroidx/lifecycle/Lifecycle$State;)V

    .line 485
    .end local v5    # "entry":Landroidx/navigation/NavBackStackEntry;
    .end local v6    # "currentMaxLifecycle":Landroidx/lifecycle/Lifecycle$State;
    .end local v7    # "destination":Landroidx/navigation/NavDestination;
    :goto_5
    goto :goto_3

    .line 488
    :cond_8
    iget-object v5, p0, Landroidx/navigation/NavController;->mBackStack:Ljava/util/Deque;

    invoke-interface {v5}, Ljava/util/Deque;->iterator()Ljava/util/Iterator;

    move-result-object v4

    .line 489
    :goto_6
    invoke-interface {v4}, Ljava/util/Iterator;->hasNext()Z

    move-result v5

    if-eqz v5, :cond_a

    .line 490
    invoke-interface {v4}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v5

    check-cast v5, Landroidx/navigation/NavBackStackEntry;

    .line 491
    .restart local v5    # "entry":Landroidx/navigation/NavBackStackEntry;
    invoke-virtual {v3, v5}, Ljava/util/HashMap;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v6

    check-cast v6, Landroidx/lifecycle/Lifecycle$State;

    .line 492
    .local v6, "newState":Landroidx/lifecycle/Lifecycle$State;
    if-eqz v6, :cond_9

    .line 493
    invoke-virtual {v5, v6}, Landroidx/navigation/NavBackStackEntry;->setMaxLifecycle(Landroidx/lifecycle/Lifecycle$State;)V

    goto :goto_7

    .line 496
    :cond_9
    invoke-virtual {v5}, Landroidx/navigation/NavBackStackEntry;->updateState()V

    .line 498
    .end local v5    # "entry":Landroidx/navigation/NavBackStackEntry;
    .end local v6    # "newState":Landroidx/lifecycle/Lifecycle$State;
    :goto_7
    goto :goto_6

    .line 501
    :cond_a
    iget-object v5, p0, Landroidx/navigation/NavController;->mBackStack:Ljava/util/Deque;

    invoke-interface {v5}, Ljava/util/Deque;->peekLast()Ljava/lang/Object;

    move-result-object v5

    check-cast v5, Landroidx/navigation/NavBackStackEntry;

    .line 503
    .local v5, "backStackEntry":Landroidx/navigation/NavBackStackEntry;
    iget-object v6, p0, Landroidx/navigation/NavController;->mOnDestinationChangedListeners:Ljava/util/concurrent/CopyOnWriteArrayList;

    invoke-virtual {v6}, Ljava/util/concurrent/CopyOnWriteArrayList;->iterator()Ljava/util/Iterator;

    move-result-object v6

    :goto_8
    invoke-interface {v6}, Ljava/util/Iterator;->hasNext()Z

    move-result v7

    if-eqz v7, :cond_b

    invoke-interface {v6}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v7

    check-cast v7, Landroidx/navigation/NavController$OnDestinationChangedListener;

    .line 504
    .local v7, "listener":Landroidx/navigation/NavController$OnDestinationChangedListener;
    invoke-virtual {v5}, Landroidx/navigation/NavBackStackEntry;->getDestination()Landroidx/navigation/NavDestination;

    move-result-object v8

    .line 505
    invoke-virtual {v5}, Landroidx/navigation/NavBackStackEntry;->getArguments()Landroid/os/Bundle;

    move-result-object v9

    .line 504
    invoke-interface {v7, p0, v8, v9}, Landroidx/navigation/NavController$OnDestinationChangedListener;->onDestinationChanged(Landroidx/navigation/NavController;Landroidx/navigation/NavDestination;Landroid/os/Bundle;)V

    .line 506
    .end local v7    # "listener":Landroidx/navigation/NavController$OnDestinationChangedListener;
    goto :goto_8

    .line 507
    :cond_b
    return v1

    .line 509
    .end local v0    # "nextResumed":Landroidx/navigation/NavDestination;
    .end local v2    # "nextStarted":Landroidx/navigation/NavDestination;
    .end local v3    # "upwardStateTransitions":Ljava/util/HashMap;, "Ljava/util/HashMap<Landroidx/navigation/NavBackStackEntry;Landroidx/lifecycle/Lifecycle$State;>;"
    .end local v4    # "iterator":Ljava/util/Iterator;, "Ljava/util/Iterator<Landroidx/navigation/NavBackStackEntry;>;"
    .end local v5    # "backStackEntry":Landroidx/navigation/NavBackStackEntry;
    :cond_c
    const/4 v0, 0x0

    return v0
.end method

.method private findInvalidDestinationDisplayNameInDeepLink([I)Ljava/lang/String;
    .locals 5
    .param p1, "deepLink"    # [I

    .line 779
    iget-object v0, p0, Landroidx/navigation/NavController;->mGraph:Landroidx/navigation/NavGraph;

    .line 780
    .local v0, "graph":Landroidx/navigation/NavGraph;
    const/4 v1, 0x0

    .local v1, "i":I
    :goto_0
    array-length v2, p1

    const/4 v3, 0x0

    if-ge v1, v2, :cond_4

    .line 781
    aget v2, p1, v1

    .line 782
    .local v2, "destinationId":I
    if-nez v1, :cond_0

    .line 783
    iget-object v4, p0, Landroidx/navigation/NavController;->mGraph:Landroidx/navigation/NavGraph;

    invoke-virtual {v4}, Landroidx/navigation/NavGraph;->getId()I

    move-result v4

    if-ne v4, v2, :cond_1

    iget-object v3, p0, Landroidx/navigation/NavController;->mGraph:Landroidx/navigation/NavGraph;

    goto :goto_1

    .line 784
    :cond_0
    invoke-virtual {v0, v2}, Landroidx/navigation/NavGraph;->findNode(I)Landroidx/navigation/NavDestination;

    move-result-object v3

    :cond_1
    :goto_1
    nop

    .line 785
    .local v3, "node":Landroidx/navigation/NavDestination;
    if-nez v3, :cond_2

    .line 786
    iget-object v4, p0, Landroidx/navigation/NavController;->mContext:Landroid/content/Context;

    invoke-static {v4, v2}, Landroidx/navigation/NavDestination;->getDisplayName(Landroid/content/Context;I)Ljava/lang/String;

    move-result-object v4

    return-object v4

    .line 788
    :cond_2
    array-length v4, p1

    add-int/lit8 v4, v4, -0x1

    if-eq v1, v4, :cond_3

    .line 790
    move-object v0, v3

    check-cast v0, Landroidx/navigation/NavGraph;

    .line 793
    :goto_2
    invoke-virtual {v0}, Landroidx/navigation/NavGraph;->getStartDestination()I

    move-result v4

    invoke-virtual {v0, v4}, Landroidx/navigation/NavGraph;->findNode(I)Landroidx/navigation/NavDestination;

    move-result-object v4

    instance-of v4, v4, Landroidx/navigation/NavGraph;

    if-eqz v4, :cond_3

    .line 794
    invoke-virtual {v0}, Landroidx/navigation/NavGraph;->getStartDestination()I

    move-result v4

    invoke-virtual {v0, v4}, Landroidx/navigation/NavGraph;->findNode(I)Landroidx/navigation/NavDestination;

    move-result-object v4

    move-object v0, v4

    check-cast v0, Landroidx/navigation/NavGraph;

    goto :goto_2

    .line 780
    .end local v2    # "destinationId":I
    .end local v3    # "node":Landroidx/navigation/NavDestination;
    :cond_3
    add-int/lit8 v1, v1, 0x1

    goto :goto_0

    .line 799
    .end local v1    # "i":I
    :cond_4
    return-object v3
.end method

.method private getDestinationCountOnBackStack()I
    .locals 4

    .line 411
    const/4 v0, 0x0

    .line 412
    .local v0, "count":I
    iget-object v1, p0, Landroidx/navigation/NavController;->mBackStack:Ljava/util/Deque;

    invoke-interface {v1}, Ljava/util/Deque;->iterator()Ljava/util/Iterator;

    move-result-object v1

    :goto_0
    invoke-interface {v1}, Ljava/util/Iterator;->hasNext()Z

    move-result v2

    if-eqz v2, :cond_1

    invoke-interface {v1}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Landroidx/navigation/NavBackStackEntry;

    .line 413
    .local v2, "entry":Landroidx/navigation/NavBackStackEntry;
    invoke-virtual {v2}, Landroidx/navigation/NavBackStackEntry;->getDestination()Landroidx/navigation/NavDestination;

    move-result-object v3

    instance-of v3, v3, Landroidx/navigation/NavGraph;

    if-nez v3, :cond_0

    .line 414
    add-int/lit8 v0, v0, 0x1

    .line 416
    .end local v2    # "entry":Landroidx/navigation/NavBackStackEntry;
    :cond_0
    goto :goto_0

    .line 417
    :cond_1
    return v0
.end method

.method private navigate(Landroidx/navigation/NavDestination;Landroid/os/Bundle;Landroidx/navigation/NavOptions;Landroidx/navigation/Navigator$Extras;)V
    .locals 20
    .param p1, "node"    # Landroidx/navigation/NavDestination;
    .param p2, "args"    # Landroid/os/Bundle;
    .param p3, "navOptions"    # Landroidx/navigation/NavOptions;
    .param p4, "navigatorExtras"    # Landroidx/navigation/Navigator$Extras;

    .line 1055
    move-object/from16 v0, p0

    move-object/from16 v1, p1

    move-object/from16 v2, p3

    const/4 v3, 0x0

    .line 1056
    .local v3, "popped":Z
    const/4 v4, 0x0

    .line 1057
    .local v4, "launchSingleTop":Z
    if-eqz v2, :cond_0

    .line 1058
    invoke-virtual/range {p3 .. p3}, Landroidx/navigation/NavOptions;->getPopUpTo()I

    move-result v5

    const/4 v6, -0x1

    if-eq v5, v6, :cond_0

    .line 1059
    invoke-virtual/range {p3 .. p3}, Landroidx/navigation/NavOptions;->getPopUpTo()I

    move-result v5

    .line 1060
    invoke-virtual/range {p3 .. p3}, Landroidx/navigation/NavOptions;->isPopUpToInclusive()Z

    move-result v6

    .line 1059
    invoke-virtual {v0, v5, v6}, Landroidx/navigation/NavController;->popBackStackInternal(IZ)Z

    move-result v3

    .line 1063
    :cond_0
    iget-object v5, v0, Landroidx/navigation/NavController;->mNavigatorProvider:Landroidx/navigation/NavigatorProvider;

    .line 1064
    invoke-virtual/range {p1 .. p1}, Landroidx/navigation/NavDestination;->getNavigatorName()Ljava/lang/String;

    move-result-object v6

    .line 1063
    invoke-virtual {v5, v6}, Landroidx/navigation/NavigatorProvider;->getNavigator(Ljava/lang/String;)Landroidx/navigation/Navigator;

    move-result-object v5

    .line 1065
    .local v5, "navigator":Landroidx/navigation/Navigator;, "Landroidx/navigation/Navigator<Landroidx/navigation/NavDestination;>;"
    invoke-virtual/range {p1 .. p2}, Landroidx/navigation/NavDestination;->addInDefaultArgs(Landroid/os/Bundle;)Landroid/os/Bundle;

    move-result-object v12

    .line 1066
    .local v12, "finalArgs":Landroid/os/Bundle;
    move-object/from16 v13, p4

    invoke-virtual {v5, v1, v12, v2, v13}, Landroidx/navigation/Navigator;->navigate(Landroidx/navigation/NavDestination;Landroid/os/Bundle;Landroidx/navigation/NavOptions;Landroidx/navigation/Navigator$Extras;)Landroidx/navigation/NavDestination;

    move-result-object v14

    .line 1068
    .local v14, "newDest":Landroidx/navigation/NavDestination;
    if-eqz v14, :cond_e

    .line 1069
    instance-of v6, v14, Landroidx/navigation/FloatingWindow;

    const/4 v15, 0x1

    if-nez v6, :cond_1

    .line 1074
    :goto_0
    iget-object v6, v0, Landroidx/navigation/NavController;->mBackStack:Ljava/util/Deque;

    invoke-interface {v6}, Ljava/util/Deque;->isEmpty()Z

    move-result v6

    if-nez v6, :cond_1

    iget-object v6, v0, Landroidx/navigation/NavController;->mBackStack:Ljava/util/Deque;

    .line 1075
    invoke-interface {v6}, Ljava/util/Deque;->peekLast()Ljava/lang/Object;

    move-result-object v6

    check-cast v6, Landroidx/navigation/NavBackStackEntry;

    invoke-virtual {v6}, Landroidx/navigation/NavBackStackEntry;->getDestination()Landroidx/navigation/NavDestination;

    move-result-object v6

    instance-of v6, v6, Landroidx/navigation/FloatingWindow;

    if-eqz v6, :cond_1

    iget-object v6, v0, Landroidx/navigation/NavController;->mBackStack:Ljava/util/Deque;

    .line 1077
    invoke-interface {v6}, Ljava/util/Deque;->peekLast()Ljava/lang/Object;

    move-result-object v6

    check-cast v6, Landroidx/navigation/NavBackStackEntry;

    invoke-virtual {v6}, Landroidx/navigation/NavBackStackEntry;->getDestination()Landroidx/navigation/NavDestination;

    move-result-object v6

    invoke-virtual {v6}, Landroidx/navigation/NavDestination;->getId()I

    move-result v6

    .line 1076
    invoke-virtual {v0, v6, v15}, Landroidx/navigation/NavController;->popBackStackInternal(IZ)Z

    move-result v6

    if-eqz v6, :cond_1

    goto :goto_0

    .line 1084
    :cond_1
    new-instance v6, Ljava/util/ArrayDeque;

    invoke-direct {v6}, Ljava/util/ArrayDeque;-><init>()V

    move-object v11, v6

    .line 1085
    .local v11, "hierarchy":Ljava/util/ArrayDeque;, "Ljava/util/ArrayDeque<Landroidx/navigation/NavBackStackEntry;>;"
    move-object v6, v14

    .line 1086
    .local v6, "destination":Landroidx/navigation/NavDestination;
    instance-of v7, v1, Landroidx/navigation/NavGraph;

    if-eqz v7, :cond_5

    move-object/from16 v16, v6

    .line 1088
    .end local v6    # "destination":Landroidx/navigation/NavDestination;
    .local v16, "destination":Landroidx/navigation/NavDestination;
    :goto_1
    invoke-virtual/range {v16 .. v16}, Landroidx/navigation/NavDestination;->getParent()Landroidx/navigation/NavGraph;

    move-result-object v10

    .line 1089
    .local v10, "parent":Landroidx/navigation/NavGraph;
    if-eqz v10, :cond_2

    .line 1090
    new-instance v17, Landroidx/navigation/NavBackStackEntry;

    iget-object v7, v0, Landroidx/navigation/NavController;->mContext:Landroid/content/Context;

    iget-object v9, v0, Landroidx/navigation/NavController;->mLifecycleOwner:Landroidx/lifecycle/LifecycleOwner;

    iget-object v8, v0, Landroidx/navigation/NavController;->mViewModel:Landroidx/navigation/NavControllerViewModel;

    move-object/from16 v6, v17

    move-object/from16 v18, v8

    move-object v8, v10

    move-object/from16 v19, v9

    move-object v9, v12

    move-object v15, v10

    .end local v10    # "parent":Landroidx/navigation/NavGraph;
    .local v15, "parent":Landroidx/navigation/NavGraph;
    move-object/from16 v10, v19

    move/from16 v19, v4

    move-object v4, v11

    .end local v11    # "hierarchy":Ljava/util/ArrayDeque;, "Ljava/util/ArrayDeque<Landroidx/navigation/NavBackStackEntry;>;"
    .local v4, "hierarchy":Ljava/util/ArrayDeque;, "Ljava/util/ArrayDeque<Landroidx/navigation/NavBackStackEntry;>;"
    .local v19, "launchSingleTop":Z
    move-object/from16 v11, v18

    invoke-direct/range {v6 .. v11}, Landroidx/navigation/NavBackStackEntry;-><init>(Landroid/content/Context;Landroidx/navigation/NavDestination;Landroid/os/Bundle;Landroidx/lifecycle/LifecycleOwner;Landroidx/navigation/NavControllerViewModel;)V

    .line 1092
    .local v6, "entry":Landroidx/navigation/NavBackStackEntry;
    invoke-virtual {v4, v6}, Ljava/util/ArrayDeque;->addFirst(Ljava/lang/Object;)V

    .line 1094
    iget-object v7, v0, Landroidx/navigation/NavController;->mBackStack:Ljava/util/Deque;

    invoke-interface {v7}, Ljava/util/Deque;->isEmpty()Z

    move-result v7

    if-nez v7, :cond_3

    iget-object v7, v0, Landroidx/navigation/NavController;->mBackStack:Ljava/util/Deque;

    .line 1095
    invoke-interface {v7}, Ljava/util/Deque;->getLast()Ljava/lang/Object;

    move-result-object v7

    check-cast v7, Landroidx/navigation/NavBackStackEntry;

    invoke-virtual {v7}, Landroidx/navigation/NavBackStackEntry;->getDestination()Landroidx/navigation/NavDestination;

    move-result-object v7

    if-ne v7, v15, :cond_3

    .line 1096
    invoke-virtual {v15}, Landroidx/navigation/NavGraph;->getId()I

    move-result v7

    const/4 v8, 0x1

    invoke-virtual {v0, v7, v8}, Landroidx/navigation/NavController;->popBackStackInternal(IZ)Z

    goto :goto_2

    .line 1089
    .end local v6    # "entry":Landroidx/navigation/NavBackStackEntry;
    .end local v15    # "parent":Landroidx/navigation/NavGraph;
    .end local v19    # "launchSingleTop":Z
    .local v4, "launchSingleTop":Z
    .restart local v10    # "parent":Landroidx/navigation/NavGraph;
    .restart local v11    # "hierarchy":Ljava/util/ArrayDeque;, "Ljava/util/ArrayDeque<Landroidx/navigation/NavBackStackEntry;>;"
    :cond_2
    move/from16 v19, v4

    move-object v15, v10

    move-object v4, v11

    .line 1099
    .end local v10    # "parent":Landroidx/navigation/NavGraph;
    .end local v11    # "hierarchy":Ljava/util/ArrayDeque;, "Ljava/util/ArrayDeque<Landroidx/navigation/NavBackStackEntry;>;"
    .local v4, "hierarchy":Ljava/util/ArrayDeque;, "Ljava/util/ArrayDeque<Landroidx/navigation/NavBackStackEntry;>;"
    .restart local v15    # "parent":Landroidx/navigation/NavGraph;
    .restart local v19    # "launchSingleTop":Z
    :cond_3
    :goto_2
    move-object v6, v15

    .line 1100
    .end local v15    # "parent":Landroidx/navigation/NavGraph;
    .end local v16    # "destination":Landroidx/navigation/NavDestination;
    .local v6, "destination":Landroidx/navigation/NavDestination;
    if-eqz v6, :cond_6

    if-ne v6, v1, :cond_4

    goto :goto_3

    :cond_4
    move-object v11, v4

    move-object/from16 v16, v6

    move/from16 v4, v19

    const/4 v15, 0x1

    goto :goto_1

    .line 1086
    .end local v19    # "launchSingleTop":Z
    .local v4, "launchSingleTop":Z
    .restart local v11    # "hierarchy":Ljava/util/ArrayDeque;, "Ljava/util/ArrayDeque<Landroidx/navigation/NavBackStackEntry;>;"
    :cond_5
    move/from16 v19, v4

    move-object v4, v11

    .line 1105
    .end local v11    # "hierarchy":Ljava/util/ArrayDeque;, "Ljava/util/ArrayDeque<Landroidx/navigation/NavBackStackEntry;>;"
    .local v4, "hierarchy":Ljava/util/ArrayDeque;, "Ljava/util/ArrayDeque<Landroidx/navigation/NavBackStackEntry;>;"
    .restart local v19    # "launchSingleTop":Z
    :cond_6
    :goto_3
    invoke-virtual {v4}, Ljava/util/ArrayDeque;->isEmpty()Z

    move-result v7

    if-eqz v7, :cond_7

    .line 1106
    move-object v7, v14

    goto :goto_4

    .line 1107
    :cond_7
    invoke-virtual {v4}, Ljava/util/ArrayDeque;->getFirst()Ljava/lang/Object;

    move-result-object v7

    check-cast v7, Landroidx/navigation/NavBackStackEntry;

    invoke-virtual {v7}, Landroidx/navigation/NavBackStackEntry;->getDestination()Landroidx/navigation/NavDestination;

    move-result-object v7

    :goto_4
    move-object v6, v7

    move-object v15, v6

    .line 1108
    .end local v6    # "destination":Landroidx/navigation/NavDestination;
    .local v15, "destination":Landroidx/navigation/NavDestination;
    :goto_5
    if-eqz v15, :cond_9

    invoke-virtual {v15}, Landroidx/navigation/NavDestination;->getId()I

    move-result v6

    invoke-virtual {v0, v6}, Landroidx/navigation/NavController;->findDestination(I)Landroidx/navigation/NavDestination;

    move-result-object v6

    if-nez v6, :cond_9

    .line 1109
    invoke-virtual {v15}, Landroidx/navigation/NavDestination;->getParent()Landroidx/navigation/NavGraph;

    move-result-object v16

    .line 1110
    .local v16, "parent":Landroidx/navigation/NavGraph;
    if-eqz v16, :cond_8

    .line 1111
    new-instance v17, Landroidx/navigation/NavBackStackEntry;

    iget-object v7, v0, Landroidx/navigation/NavController;->mContext:Landroid/content/Context;

    iget-object v10, v0, Landroidx/navigation/NavController;->mLifecycleOwner:Landroidx/lifecycle/LifecycleOwner;

    iget-object v11, v0, Landroidx/navigation/NavController;->mViewModel:Landroidx/navigation/NavControllerViewModel;

    move-object/from16 v6, v17

    move-object/from16 v8, v16

    move-object v9, v12

    invoke-direct/range {v6 .. v11}, Landroidx/navigation/NavBackStackEntry;-><init>(Landroid/content/Context;Landroidx/navigation/NavDestination;Landroid/os/Bundle;Landroidx/lifecycle/LifecycleOwner;Landroidx/navigation/NavControllerViewModel;)V

    .line 1113
    .local v6, "entry":Landroidx/navigation/NavBackStackEntry;
    invoke-virtual {v4, v6}, Ljava/util/ArrayDeque;->addFirst(Ljava/lang/Object;)V

    .line 1115
    .end local v6    # "entry":Landroidx/navigation/NavBackStackEntry;
    :cond_8
    move-object/from16 v15, v16

    .line 1116
    .end local v16    # "parent":Landroidx/navigation/NavGraph;
    goto :goto_5

    .line 1117
    :cond_9
    invoke-virtual {v4}, Ljava/util/ArrayDeque;->isEmpty()Z

    move-result v6

    if-eqz v6, :cond_a

    .line 1118
    move-object v6, v14

    goto :goto_6

    .line 1119
    :cond_a
    invoke-virtual {v4}, Ljava/util/ArrayDeque;->getLast()Ljava/lang/Object;

    move-result-object v6

    check-cast v6, Landroidx/navigation/NavBackStackEntry;

    invoke-virtual {v6}, Landroidx/navigation/NavBackStackEntry;->getDestination()Landroidx/navigation/NavDestination;

    move-result-object v6

    :goto_6
    move-object/from16 v16, v6

    .line 1122
    .local v16, "overlappingDestination":Landroidx/navigation/NavDestination;
    :goto_7
    iget-object v6, v0, Landroidx/navigation/NavController;->mBackStack:Ljava/util/Deque;

    invoke-interface {v6}, Ljava/util/Deque;->isEmpty()Z

    move-result v6

    if-nez v6, :cond_b

    iget-object v6, v0, Landroidx/navigation/NavController;->mBackStack:Ljava/util/Deque;

    .line 1123
    invoke-interface {v6}, Ljava/util/Deque;->getLast()Ljava/lang/Object;

    move-result-object v6

    check-cast v6, Landroidx/navigation/NavBackStackEntry;

    invoke-virtual {v6}, Landroidx/navigation/NavBackStackEntry;->getDestination()Landroidx/navigation/NavDestination;

    move-result-object v6

    instance-of v6, v6, Landroidx/navigation/NavGraph;

    if-eqz v6, :cond_b

    iget-object v6, v0, Landroidx/navigation/NavController;->mBackStack:Ljava/util/Deque;

    .line 1124
    invoke-interface {v6}, Ljava/util/Deque;->getLast()Ljava/lang/Object;

    move-result-object v6

    check-cast v6, Landroidx/navigation/NavBackStackEntry;

    invoke-virtual {v6}, Landroidx/navigation/NavBackStackEntry;->getDestination()Landroidx/navigation/NavDestination;

    move-result-object v6

    check-cast v6, Landroidx/navigation/NavGraph;

    .line 1125
    invoke-virtual/range {v16 .. v16}, Landroidx/navigation/NavDestination;->getId()I

    move-result v7

    const/4 v8, 0x0

    .line 1124
    invoke-virtual {v6, v7, v8}, Landroidx/navigation/NavGraph;->findNode(IZ)Landroidx/navigation/NavDestination;

    move-result-object v6

    if-nez v6, :cond_b

    iget-object v6, v0, Landroidx/navigation/NavController;->mBackStack:Ljava/util/Deque;

    .line 1126
    invoke-interface {v6}, Ljava/util/Deque;->getLast()Ljava/lang/Object;

    move-result-object v6

    check-cast v6, Landroidx/navigation/NavBackStackEntry;

    invoke-virtual {v6}, Landroidx/navigation/NavBackStackEntry;->getDestination()Landroidx/navigation/NavDestination;

    move-result-object v6

    invoke-virtual {v6}, Landroidx/navigation/NavDestination;->getId()I

    move-result v6

    const/4 v7, 0x1

    invoke-virtual {v0, v6, v7}, Landroidx/navigation/NavController;->popBackStackInternal(IZ)Z

    move-result v6

    if-eqz v6, :cond_b

    goto :goto_7

    .line 1129
    :cond_b
    iget-object v6, v0, Landroidx/navigation/NavController;->mBackStack:Ljava/util/Deque;

    invoke-interface {v6, v4}, Ljava/util/Deque;->addAll(Ljava/util/Collection;)Z

    .line 1131
    iget-object v6, v0, Landroidx/navigation/NavController;->mBackStack:Ljava/util/Deque;

    invoke-interface {v6}, Ljava/util/Deque;->isEmpty()Z

    move-result v6

    if-nez v6, :cond_c

    iget-object v6, v0, Landroidx/navigation/NavController;->mBackStack:Ljava/util/Deque;

    invoke-interface {v6}, Ljava/util/Deque;->getFirst()Ljava/lang/Object;

    move-result-object v6

    check-cast v6, Landroidx/navigation/NavBackStackEntry;

    invoke-virtual {v6}, Landroidx/navigation/NavBackStackEntry;->getDestination()Landroidx/navigation/NavDestination;

    move-result-object v6

    iget-object v7, v0, Landroidx/navigation/NavController;->mGraph:Landroidx/navigation/NavGraph;

    if-eq v6, v7, :cond_d

    .line 1132
    :cond_c
    new-instance v17, Landroidx/navigation/NavBackStackEntry;

    iget-object v7, v0, Landroidx/navigation/NavController;->mContext:Landroid/content/Context;

    iget-object v8, v0, Landroidx/navigation/NavController;->mGraph:Landroidx/navigation/NavGraph;

    iget-object v10, v0, Landroidx/navigation/NavController;->mLifecycleOwner:Landroidx/lifecycle/LifecycleOwner;

    iget-object v11, v0, Landroidx/navigation/NavController;->mViewModel:Landroidx/navigation/NavControllerViewModel;

    move-object/from16 v6, v17

    move-object v9, v12

    invoke-direct/range {v6 .. v11}, Landroidx/navigation/NavBackStackEntry;-><init>(Landroid/content/Context;Landroidx/navigation/NavDestination;Landroid/os/Bundle;Landroidx/lifecycle/LifecycleOwner;Landroidx/navigation/NavControllerViewModel;)V

    .line 1134
    .restart local v6    # "entry":Landroidx/navigation/NavBackStackEntry;
    iget-object v7, v0, Landroidx/navigation/NavController;->mBackStack:Ljava/util/Deque;

    invoke-interface {v7, v6}, Ljava/util/Deque;->addFirst(Ljava/lang/Object;)V

    .line 1137
    .end local v6    # "entry":Landroidx/navigation/NavBackStackEntry;
    :cond_d
    new-instance v17, Landroidx/navigation/NavBackStackEntry;

    iget-object v7, v0, Landroidx/navigation/NavController;->mContext:Landroid/content/Context;

    .line 1138
    invoke-virtual {v14, v12}, Landroidx/navigation/NavDestination;->addInDefaultArgs(Landroid/os/Bundle;)Landroid/os/Bundle;

    move-result-object v9

    iget-object v10, v0, Landroidx/navigation/NavController;->mLifecycleOwner:Landroidx/lifecycle/LifecycleOwner;

    iget-object v11, v0, Landroidx/navigation/NavController;->mViewModel:Landroidx/navigation/NavControllerViewModel;

    move-object/from16 v6, v17

    move-object v8, v14

    invoke-direct/range {v6 .. v11}, Landroidx/navigation/NavBackStackEntry;-><init>(Landroid/content/Context;Landroidx/navigation/NavDestination;Landroid/os/Bundle;Landroidx/lifecycle/LifecycleOwner;Landroidx/navigation/NavControllerViewModel;)V

    .line 1139
    .local v6, "newBackStackEntry":Landroidx/navigation/NavBackStackEntry;
    iget-object v7, v0, Landroidx/navigation/NavController;->mBackStack:Ljava/util/Deque;

    invoke-interface {v7, v6}, Ljava/util/Deque;->add(Ljava/lang/Object;)Z

    .end local v4    # "hierarchy":Ljava/util/ArrayDeque;, "Ljava/util/ArrayDeque<Landroidx/navigation/NavBackStackEntry;>;"
    .end local v6    # "newBackStackEntry":Landroidx/navigation/NavBackStackEntry;
    .end local v15    # "destination":Landroidx/navigation/NavDestination;
    .end local v16    # "overlappingDestination":Landroidx/navigation/NavDestination;
    goto :goto_8

    .line 1140
    .end local v19    # "launchSingleTop":Z
    .local v4, "launchSingleTop":Z
    :cond_e
    move/from16 v19, v4

    .end local v4    # "launchSingleTop":Z
    .restart local v19    # "launchSingleTop":Z
    if-eqz v2, :cond_f

    invoke-virtual/range {p3 .. p3}, Landroidx/navigation/NavOptions;->shouldLaunchSingleTop()Z

    move-result v4

    if-eqz v4, :cond_f

    .line 1141
    const/4 v4, 0x1

    .line 1142
    .end local v19    # "launchSingleTop":Z
    .restart local v4    # "launchSingleTop":Z
    iget-object v6, v0, Landroidx/navigation/NavController;->mBackStack:Ljava/util/Deque;

    invoke-interface {v6}, Ljava/util/Deque;->peekLast()Ljava/lang/Object;

    move-result-object v6

    check-cast v6, Landroidx/navigation/NavBackStackEntry;

    .line 1143
    .local v6, "singleTopBackStackEntry":Landroidx/navigation/NavBackStackEntry;
    if-eqz v6, :cond_10

    .line 1144
    invoke-virtual {v6, v12}, Landroidx/navigation/NavBackStackEntry;->replaceArguments(Landroid/os/Bundle;)V

    goto :goto_9

    .line 1140
    .end local v4    # "launchSingleTop":Z
    .end local v6    # "singleTopBackStackEntry":Landroidx/navigation/NavBackStackEntry;
    .restart local v19    # "launchSingleTop":Z
    :cond_f
    :goto_8
    nop

    .line 1147
    move/from16 v4, v19

    .end local v19    # "launchSingleTop":Z
    .restart local v4    # "launchSingleTop":Z
    :cond_10
    :goto_9
    invoke-direct/range {p0 .. p0}, Landroidx/navigation/NavController;->updateOnBackPressedCallbackEnabled()V

    .line 1148
    if-nez v3, :cond_11

    if-nez v14, :cond_11

    if-eqz v4, :cond_12

    .line 1149
    :cond_11
    invoke-direct/range {p0 .. p0}, Landroidx/navigation/NavController;->dispatchOnDestinationChanged()Z

    .line 1151
    :cond_12
    return-void
.end method

.method private onGraphCreated(Landroid/os/Bundle;)V
    .locals 20
    .param p1, "startDestinationArgs"    # Landroid/os/Bundle;

    .line 596
    move-object/from16 v0, p0

    iget-object v1, v0, Landroidx/navigation/NavController;->mNavigatorStateToRestore:Landroid/os/Bundle;

    if-eqz v1, :cond_1

    .line 597
    const-string v2, "android-support-nav:controller:navigatorState:names"

    invoke-virtual {v1, v2}, Landroid/os/Bundle;->getStringArrayList(Ljava/lang/String;)Ljava/util/ArrayList;

    move-result-object v1

    .line 599
    .local v1, "navigatorNames":Ljava/util/ArrayList;, "Ljava/util/ArrayList<Ljava/lang/String;>;"
    if-eqz v1, :cond_1

    .line 600
    invoke-virtual {v1}, Ljava/util/ArrayList;->iterator()Ljava/util/Iterator;

    move-result-object v2

    :goto_0
    invoke-interface {v2}, Ljava/util/Iterator;->hasNext()Z

    move-result v3

    if-eqz v3, :cond_1

    invoke-interface {v2}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v3

    check-cast v3, Ljava/lang/String;

    .line 601
    .local v3, "name":Ljava/lang/String;
    iget-object v4, v0, Landroidx/navigation/NavController;->mNavigatorProvider:Landroidx/navigation/NavigatorProvider;

    invoke-virtual {v4, v3}, Landroidx/navigation/NavigatorProvider;->getNavigator(Ljava/lang/String;)Landroidx/navigation/Navigator;

    move-result-object v4

    .line 602
    .local v4, "navigator":Landroidx/navigation/Navigator;, "Landroidx/navigation/Navigator<*>;"
    iget-object v5, v0, Landroidx/navigation/NavController;->mNavigatorStateToRestore:Landroid/os/Bundle;

    invoke-virtual {v5, v3}, Landroid/os/Bundle;->getBundle(Ljava/lang/String;)Landroid/os/Bundle;

    move-result-object v5

    .line 603
    .local v5, "bundle":Landroid/os/Bundle;
    if-eqz v5, :cond_0

    .line 604
    invoke-virtual {v4, v5}, Landroidx/navigation/Navigator;->onRestoreState(Landroid/os/Bundle;)V

    .line 606
    .end local v3    # "name":Ljava/lang/String;
    .end local v4    # "navigator":Landroidx/navigation/Navigator;, "Landroidx/navigation/Navigator<*>;"
    .end local v5    # "bundle":Landroid/os/Bundle;
    :cond_0
    goto :goto_0

    .line 609
    .end local v1    # "navigatorNames":Ljava/util/ArrayList;, "Ljava/util/ArrayList<Ljava/lang/String;>;"
    :cond_1
    iget-object v1, v0, Landroidx/navigation/NavController;->mBackStackToRestore:[Landroid/os/Parcelable;

    const/4 v2, 0x0

    const/4 v3, 0x0

    if-eqz v1, :cond_5

    .line 610
    array-length v4, v1

    const/4 v5, 0x0

    :goto_1
    if-ge v5, v4, :cond_4

    aget-object v6, v1, v5

    .line 611
    .local v6, "parcelable":Landroid/os/Parcelable;
    move-object v7, v6

    check-cast v7, Landroidx/navigation/NavBackStackEntryState;

    .line 612
    .local v7, "state":Landroidx/navigation/NavBackStackEntryState;
    invoke-virtual {v7}, Landroidx/navigation/NavBackStackEntryState;->getDestinationId()I

    move-result v8

    invoke-virtual {v0, v8}, Landroidx/navigation/NavController;->findDestination(I)Landroidx/navigation/NavDestination;

    move-result-object v8

    .line 613
    .local v8, "node":Landroidx/navigation/NavDestination;
    if-eqz v8, :cond_3

    .line 621
    invoke-virtual {v7}, Landroidx/navigation/NavBackStackEntryState;->getArgs()Landroid/os/Bundle;

    move-result-object v15

    .line 622
    .local v15, "args":Landroid/os/Bundle;
    if-eqz v15, :cond_2

    .line 623
    iget-object v9, v0, Landroidx/navigation/NavController;->mContext:Landroid/content/Context;

    invoke-virtual {v9}, Landroid/content/Context;->getClassLoader()Ljava/lang/ClassLoader;

    move-result-object v9

    invoke-virtual {v15, v9}, Landroid/os/Bundle;->setClassLoader(Ljava/lang/ClassLoader;)V

    .line 625
    :cond_2
    new-instance v17, Landroidx/navigation/NavBackStackEntry;

    iget-object v10, v0, Landroidx/navigation/NavController;->mContext:Landroid/content/Context;

    iget-object v13, v0, Landroidx/navigation/NavController;->mLifecycleOwner:Landroidx/lifecycle/LifecycleOwner;

    iget-object v14, v0, Landroidx/navigation/NavController;->mViewModel:Landroidx/navigation/NavControllerViewModel;

    .line 627
    invoke-virtual {v7}, Landroidx/navigation/NavBackStackEntryState;->getUUID()Ljava/util/UUID;

    move-result-object v16

    invoke-virtual {v7}, Landroidx/navigation/NavBackStackEntryState;->getSavedState()Landroid/os/Bundle;

    move-result-object v18

    move-object/from16 v9, v17

    move-object v11, v8

    move-object v12, v15

    move-object/from16 v19, v15

    .end local v15    # "args":Landroid/os/Bundle;
    .local v19, "args":Landroid/os/Bundle;
    move-object/from16 v15, v16

    move-object/from16 v16, v18

    invoke-direct/range {v9 .. v16}, Landroidx/navigation/NavBackStackEntry;-><init>(Landroid/content/Context;Landroidx/navigation/NavDestination;Landroid/os/Bundle;Landroidx/lifecycle/LifecycleOwner;Landroidx/navigation/NavControllerViewModel;Ljava/util/UUID;Landroid/os/Bundle;)V

    .line 628
    .local v9, "entry":Landroidx/navigation/NavBackStackEntry;
    iget-object v10, v0, Landroidx/navigation/NavController;->mBackStack:Ljava/util/Deque;

    invoke-interface {v10, v9}, Ljava/util/Deque;->add(Ljava/lang/Object;)Z

    .line 610
    .end local v6    # "parcelable":Landroid/os/Parcelable;
    .end local v7    # "state":Landroidx/navigation/NavBackStackEntryState;
    .end local v8    # "node":Landroidx/navigation/NavDestination;
    .end local v9    # "entry":Landroidx/navigation/NavBackStackEntry;
    .end local v19    # "args":Landroid/os/Bundle;
    add-int/lit8 v5, v5, 0x1

    goto :goto_1

    .line 614
    .restart local v6    # "parcelable":Landroid/os/Parcelable;
    .restart local v7    # "state":Landroidx/navigation/NavBackStackEntryState;
    .restart local v8    # "node":Landroidx/navigation/NavDestination;
    :cond_3
    iget-object v1, v0, Landroidx/navigation/NavController;->mContext:Landroid/content/Context;

    .line 615
    invoke-virtual {v7}, Landroidx/navigation/NavBackStackEntryState;->getDestinationId()I

    move-result v2

    .line 614
    invoke-static {v1, v2}, Landroidx/navigation/NavDestination;->getDisplayName(Landroid/content/Context;I)Ljava/lang/String;

    move-result-object v1

    .line 616
    .local v1, "dest":Ljava/lang/String;
    new-instance v2, Ljava/lang/IllegalStateException;

    new-instance v3, Ljava/lang/StringBuilder;

    invoke-direct {v3}, Ljava/lang/StringBuilder;-><init>()V

    const-string v4, "Restoring the Navigation back stack failed: destination "

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v3

    invoke-virtual {v3, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v3

    const-string v4, " cannot be found from the current destination "

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v3

    .line 619
    invoke-virtual/range {p0 .. p0}, Landroidx/navigation/NavController;->getCurrentDestination()Landroidx/navigation/NavDestination;

    move-result-object v4

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    move-result-object v3

    invoke-virtual {v3}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v3

    invoke-direct {v2, v3}, Ljava/lang/IllegalStateException;-><init>(Ljava/lang/String;)V

    throw v2

    .line 630
    .end local v1    # "dest":Ljava/lang/String;
    .end local v6    # "parcelable":Landroid/os/Parcelable;
    .end local v7    # "state":Landroidx/navigation/NavBackStackEntryState;
    .end local v8    # "node":Landroidx/navigation/NavDestination;
    :cond_4
    invoke-direct/range {p0 .. p0}, Landroidx/navigation/NavController;->updateOnBackPressedCallbackEnabled()V

    .line 631
    iput-object v3, v0, Landroidx/navigation/NavController;->mBackStackToRestore:[Landroid/os/Parcelable;

    .line 633
    :cond_5
    iget-object v1, v0, Landroidx/navigation/NavController;->mGraph:Landroidx/navigation/NavGraph;

    if-eqz v1, :cond_8

    iget-object v1, v0, Landroidx/navigation/NavController;->mBackStack:Ljava/util/Deque;

    invoke-interface {v1}, Ljava/util/Deque;->isEmpty()Z

    move-result v1

    if-eqz v1, :cond_8

    .line 634
    iget-boolean v1, v0, Landroidx/navigation/NavController;->mDeepLinkHandled:Z

    if-nez v1, :cond_6

    iget-object v1, v0, Landroidx/navigation/NavController;->mActivity:Landroid/app/Activity;

    if-eqz v1, :cond_6

    .line 635
    invoke-virtual {v1}, Landroid/app/Activity;->getIntent()Landroid/content/Intent;

    move-result-object v1

    invoke-virtual {v0, v1}, Landroidx/navigation/NavController;->handleDeepLink(Landroid/content/Intent;)Z

    move-result v1

    if-eqz v1, :cond_6

    const/4 v2, 0x1

    goto :goto_2

    :cond_6
    nop

    :goto_2
    move v1, v2

    .line 636
    .local v1, "deepLinked":Z
    if-nez v1, :cond_7

    .line 639
    iget-object v2, v0, Landroidx/navigation/NavController;->mGraph:Landroidx/navigation/NavGraph;

    move-object/from16 v4, p1

    invoke-direct {v0, v2, v4, v3, v3}, Landroidx/navigation/NavController;->navigate(Landroidx/navigation/NavDestination;Landroid/os/Bundle;Landroidx/navigation/NavOptions;Landroidx/navigation/Navigator$Extras;)V

    goto :goto_3

    .line 636
    :cond_7
    move-object/from16 v4, p1

    .line 641
    .end local v1    # "deepLinked":Z
    :goto_3
    goto :goto_4

    .line 633
    :cond_8
    move-object/from16 v4, p1

    .line 642
    invoke-direct/range {p0 .. p0}, Landroidx/navigation/NavController;->dispatchOnDestinationChanged()Z

    .line 644
    :goto_4
    return-void
.end method

.method private updateOnBackPressedCallbackEnabled()V
    .locals 3

    .line 1294
    iget-object v0, p0, Landroidx/navigation/NavController;->mOnBackPressedCallback:Landroidx/activity/OnBackPressedCallback;

    iget-boolean v1, p0, Landroidx/navigation/NavController;->mEnableOnBackPressedCallback:Z

    const/4 v2, 0x1

    if-eqz v1, :cond_0

    .line 1295
    invoke-direct {p0}, Landroidx/navigation/NavController;->getDestinationCountOnBackStack()I

    move-result v1

    if-le v1, v2, :cond_0

    goto :goto_0

    :cond_0
    const/4 v2, 0x0

    .line 1294
    :goto_0
    invoke-virtual {v0, v2}, Landroidx/activity/OnBackPressedCallback;->setEnabled(Z)V

    .line 1296
    return-void
.end method


# virtual methods
.method public addOnDestinationChangedListener(Landroidx/navigation/NavController$OnDestinationChangedListener;)V
    .locals 3
    .param p1, "listener"    # Landroidx/navigation/NavController$OnDestinationChangedListener;

    .line 231
    iget-object v0, p0, Landroidx/navigation/NavController;->mBackStack:Ljava/util/Deque;

    invoke-interface {v0}, Ljava/util/Deque;->isEmpty()Z

    move-result v0

    if-nez v0, :cond_0

    .line 232
    iget-object v0, p0, Landroidx/navigation/NavController;->mBackStack:Ljava/util/Deque;

    invoke-interface {v0}, Ljava/util/Deque;->peekLast()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Landroidx/navigation/NavBackStackEntry;

    .line 233
    .local v0, "backStackEntry":Landroidx/navigation/NavBackStackEntry;
    invoke-virtual {v0}, Landroidx/navigation/NavBackStackEntry;->getDestination()Landroidx/navigation/NavDestination;

    move-result-object v1

    .line 234
    invoke-virtual {v0}, Landroidx/navigation/NavBackStackEntry;->getArguments()Landroid/os/Bundle;

    move-result-object v2

    .line 233
    invoke-interface {p1, p0, v1, v2}, Landroidx/navigation/NavController$OnDestinationChangedListener;->onDestinationChanged(Landroidx/navigation/NavController;Landroidx/navigation/NavDestination;Landroid/os/Bundle;)V

    .line 236
    .end local v0    # "backStackEntry":Landroidx/navigation/NavBackStackEntry;
    :cond_0
    iget-object v0, p0, Landroidx/navigation/NavController;->mOnDestinationChangedListeners:Ljava/util/concurrent/CopyOnWriteArrayList;

    invoke-virtual {v0, p1}, Ljava/util/concurrent/CopyOnWriteArrayList;->add(Ljava/lang/Object;)Z

    .line 237
    return-void
.end method

.method public createDeepLink()Landroidx/navigation/NavDeepLinkBuilder;
    .locals 1

    .line 1190
    new-instance v0, Landroidx/navigation/NavDeepLinkBuilder;

    invoke-direct {v0, p0}, Landroidx/navigation/NavDeepLinkBuilder;-><init>(Landroidx/navigation/NavController;)V

    return-object v0
.end method

.method enableOnBackPressed(Z)V
    .locals 0
    .param p1, "enabled"    # Z

    .line 1289
    iput-boolean p1, p0, Landroidx/navigation/NavController;->mEnableOnBackPressedCallback:Z

    .line 1290
    invoke-direct {p0}, Landroidx/navigation/NavController;->updateOnBackPressedCallbackEnabled()V

    .line 1291
    return-void
.end method

.method findDestination(I)Landroidx/navigation/NavDestination;
    .locals 3
    .param p1, "destinationId"    # I

    .line 828
    iget-object v0, p0, Landroidx/navigation/NavController;->mGraph:Landroidx/navigation/NavGraph;

    if-nez v0, :cond_0

    .line 829
    const/4 v0, 0x0

    return-object v0

    .line 831
    :cond_0
    invoke-virtual {v0}, Landroidx/navigation/NavGraph;->getId()I

    move-result v0

    if-ne v0, p1, :cond_1

    .line 832
    iget-object v0, p0, Landroidx/navigation/NavController;->mGraph:Landroidx/navigation/NavGraph;

    return-object v0

    .line 834
    :cond_1
    iget-object v0, p0, Landroidx/navigation/NavController;->mBackStack:Ljava/util/Deque;

    invoke-interface {v0}, Ljava/util/Deque;->isEmpty()Z

    move-result v0

    if-eqz v0, :cond_2

    .line 835
    iget-object v0, p0, Landroidx/navigation/NavController;->mGraph:Landroidx/navigation/NavGraph;

    goto :goto_0

    .line 836
    :cond_2
    iget-object v0, p0, Landroidx/navigation/NavController;->mBackStack:Ljava/util/Deque;

    invoke-interface {v0}, Ljava/util/Deque;->getLast()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Landroidx/navigation/NavBackStackEntry;

    invoke-virtual {v0}, Landroidx/navigation/NavBackStackEntry;->getDestination()Landroidx/navigation/NavDestination;

    move-result-object v0

    :goto_0
    nop

    .line 837
    .local v0, "currentNode":Landroidx/navigation/NavDestination;
    instance-of v1, v0, Landroidx/navigation/NavGraph;

    if-eqz v1, :cond_3

    .line 838
    move-object v1, v0

    check-cast v1, Landroidx/navigation/NavGraph;

    goto :goto_1

    .line 839
    :cond_3
    invoke-virtual {v0}, Landroidx/navigation/NavDestination;->getParent()Landroidx/navigation/NavGraph;

    move-result-object v1

    :goto_1
    nop

    .line 840
    .local v1, "currentGraph":Landroidx/navigation/NavGraph;
    invoke-virtual {v1, p1}, Landroidx/navigation/NavGraph;->findNode(I)Landroidx/navigation/NavDestination;

    move-result-object v2

    return-object v2
.end method

.method public getBackStack()Ljava/util/Deque;
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()",
            "Ljava/util/Deque<",
            "Landroidx/navigation/NavBackStackEntry;",
            ">;"
        }
    .end annotation

    .line 182
    iget-object v0, p0, Landroidx/navigation/NavController;->mBackStack:Ljava/util/Deque;

    return-object v0
.end method

.method public getBackStackEntry(I)Landroidx/navigation/NavBackStackEntry;
    .locals 5
    .param p1, "destinationId"    # I

    .line 1345
    const/4 v0, 0x0

    .line 1346
    .local v0, "lastFromBackStack":Landroidx/navigation/NavBackStackEntry;
    iget-object v1, p0, Landroidx/navigation/NavController;->mBackStack:Ljava/util/Deque;

    invoke-interface {v1}, Ljava/util/Deque;->descendingIterator()Ljava/util/Iterator;

    move-result-object v1

    .line 1347
    .local v1, "iterator":Ljava/util/Iterator;, "Ljava/util/Iterator<Landroidx/navigation/NavBackStackEntry;>;"
    :goto_0
    invoke-interface {v1}, Ljava/util/Iterator;->hasNext()Z

    move-result v2

    if-eqz v2, :cond_1

    .line 1348
    invoke-interface {v1}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Landroidx/navigation/NavBackStackEntry;

    .line 1349
    .local v2, "entry":Landroidx/navigation/NavBackStackEntry;
    invoke-virtual {v2}, Landroidx/navigation/NavBackStackEntry;->getDestination()Landroidx/navigation/NavDestination;

    move-result-object v3

    .line 1350
    .local v3, "destination":Landroidx/navigation/NavDestination;
    invoke-virtual {v3}, Landroidx/navigation/NavDestination;->getId()I

    move-result v4

    if-ne v4, p1, :cond_0

    .line 1351
    move-object v0, v2

    .line 1352
    goto :goto_1

    .line 1354
    .end local v2    # "entry":Landroidx/navigation/NavBackStackEntry;
    .end local v3    # "destination":Landroidx/navigation/NavDestination;
    :cond_0
    goto :goto_0

    .line 1355
    :cond_1
    :goto_1
    if-eqz v0, :cond_2

    .line 1360
    return-object v0

    .line 1356
    :cond_2
    new-instance v2, Ljava/lang/IllegalArgumentException;

    new-instance v3, Ljava/lang/StringBuilder;

    invoke-direct {v3}, Ljava/lang/StringBuilder;-><init>()V

    const-string v4, "No destination with ID "

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v3

    invoke-virtual {v3, p1}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    move-result-object v3

    const-string v4, " is on the NavController\'s back stack. The current destination is "

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v3

    .line 1358
    invoke-virtual {p0}, Landroidx/navigation/NavController;->getCurrentDestination()Landroidx/navigation/NavDestination;

    move-result-object v4

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    move-result-object v3

    invoke-virtual {v3}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v3

    invoke-direct {v2, v3}, Ljava/lang/IllegalArgumentException;-><init>(Ljava/lang/String;)V

    throw v2
.end method

.method getContext()Landroid/content/Context;
    .locals 1

    .line 187
    iget-object v0, p0, Landroidx/navigation/NavController;->mContext:Landroid/content/Context;

    return-object v0
.end method

.method public getCurrentBackStackEntry()Landroidx/navigation/NavBackStackEntry;
    .locals 1

    .line 1370
    iget-object v0, p0, Landroidx/navigation/NavController;->mBackStack:Ljava/util/Deque;

    invoke-interface {v0}, Ljava/util/Deque;->isEmpty()Z

    move-result v0

    if-eqz v0, :cond_0

    .line 1371
    const/4 v0, 0x0

    return-object v0

    .line 1373
    :cond_0
    iget-object v0, p0, Landroidx/navigation/NavController;->mBackStack:Ljava/util/Deque;

    invoke-interface {v0}, Ljava/util/Deque;->getLast()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Landroidx/navigation/NavBackStackEntry;

    return-object v0
.end method

.method public getCurrentDestination()Landroidx/navigation/NavDestination;
    .locals 2

    .line 822
    invoke-virtual {p0}, Landroidx/navigation/NavController;->getCurrentBackStackEntry()Landroidx/navigation/NavBackStackEntry;

    move-result-object v0

    .line 823
    .local v0, "entry":Landroidx/navigation/NavBackStackEntry;
    if-eqz v0, :cond_0

    invoke-virtual {v0}, Landroidx/navigation/NavBackStackEntry;->getDestination()Landroidx/navigation/NavDestination;

    move-result-object v1

    goto :goto_0

    :cond_0
    const/4 v1, 0x0

    :goto_0
    return-object v1
.end method

.method public getGraph()Landroidx/navigation/NavGraph;
    .locals 2

    .line 811
    iget-object v0, p0, Landroidx/navigation/NavController;->mGraph:Landroidx/navigation/NavGraph;

    if-eqz v0, :cond_0

    .line 814
    return-object v0

    .line 812
    :cond_0
    new-instance v0, Ljava/lang/IllegalStateException;

    const-string v1, "You must call setGraph() before calling getGraph()"

    invoke-direct {v0, v1}, Ljava/lang/IllegalStateException;-><init>(Ljava/lang/String;)V

    throw v0
.end method

.method public getNavInflater()Landroidx/navigation/NavInflater;
    .locals 3

    .line 519
    iget-object v0, p0, Landroidx/navigation/NavController;->mInflater:Landroidx/navigation/NavInflater;

    if-nez v0, :cond_0

    .line 520
    new-instance v0, Landroidx/navigation/NavInflater;

    iget-object v1, p0, Landroidx/navigation/NavController;->mContext:Landroid/content/Context;

    iget-object v2, p0, Landroidx/navigation/NavController;->mNavigatorProvider:Landroidx/navigation/NavigatorProvider;

    invoke-direct {v0, v1, v2}, Landroidx/navigation/NavInflater;-><init>(Landroid/content/Context;Landroidx/navigation/NavigatorProvider;)V

    iput-object v0, p0, Landroidx/navigation/NavController;->mInflater:Landroidx/navigation/NavInflater;

    .line 522
    :cond_0
    iget-object v0, p0, Landroidx/navigation/NavController;->mInflater:Landroidx/navigation/NavInflater;

    return-object v0
.end method

.method public getNavigatorProvider()Landroidx/navigation/NavigatorProvider;
    .locals 1

    .line 202
    iget-object v0, p0, Landroidx/navigation/NavController;->mNavigatorProvider:Landroidx/navigation/NavigatorProvider;

    return-object v0
.end method

.method public getPreviousBackStackEntry()Landroidx/navigation/NavBackStackEntry;
    .locals 3

    .line 1387
    iget-object v0, p0, Landroidx/navigation/NavController;->mBackStack:Ljava/util/Deque;

    invoke-interface {v0}, Ljava/util/Deque;->descendingIterator()Ljava/util/Iterator;

    move-result-object v0

    .line 1389
    .local v0, "iterator":Ljava/util/Iterator;, "Ljava/util/Iterator<Landroidx/navigation/NavBackStackEntry;>;"
    invoke-interface {v0}, Ljava/util/Iterator;->hasNext()Z

    move-result v1

    if-eqz v1, :cond_0

    .line 1390
    invoke-interface {v0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    .line 1392
    :cond_0
    :goto_0
    invoke-interface {v0}, Ljava/util/Iterator;->hasNext()Z

    move-result v1

    if-eqz v1, :cond_2

    .line 1393
    invoke-interface {v0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Landroidx/navigation/NavBackStackEntry;

    .line 1394
    .local v1, "entry":Landroidx/navigation/NavBackStackEntry;
    invoke-virtual {v1}, Landroidx/navigation/NavBackStackEntry;->getDestination()Landroidx/navigation/NavDestination;

    move-result-object v2

    instance-of v2, v2, Landroidx/navigation/NavGraph;

    if-nez v2, :cond_1

    .line 1395
    return-object v1

    .line 1397
    .end local v1    # "entry":Landroidx/navigation/NavBackStackEntry;
    :cond_1
    goto :goto_0

    .line 1398
    :cond_2
    const/4 v1, 0x0

    return-object v1
.end method

.method public getViewModelStoreOwner(I)Landroidx/lifecycle/ViewModelStoreOwner;
    .locals 4
    .param p1, "navGraphId"    # I

    .line 1321
    iget-object v0, p0, Landroidx/navigation/NavController;->mViewModel:Landroidx/navigation/NavControllerViewModel;

    if-eqz v0, :cond_1

    .line 1325
    invoke-virtual {p0, p1}, Landroidx/navigation/NavController;->getBackStackEntry(I)Landroidx/navigation/NavBackStackEntry;

    move-result-object v0

    .line 1326
    .local v0, "lastFromBackStack":Landroidx/navigation/NavBackStackEntry;
    invoke-virtual {v0}, Landroidx/navigation/NavBackStackEntry;->getDestination()Landroidx/navigation/NavDestination;

    move-result-object v1

    instance-of v1, v1, Landroidx/navigation/NavGraph;

    if-eqz v1, :cond_0

    .line 1330
    return-object v0

    .line 1327
    :cond_0
    new-instance v1, Ljava/lang/IllegalArgumentException;

    new-instance v2, Ljava/lang/StringBuilder;

    invoke-direct {v2}, Ljava/lang/StringBuilder;-><init>()V

    const-string v3, "No NavGraph with ID "

    invoke-virtual {v2, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v2

    invoke-virtual {v2, p1}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    move-result-object v2

    const-string v3, " is on the NavController\'s back stack"

    invoke-virtual {v2, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v2

    invoke-virtual {v2}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v2

    invoke-direct {v1, v2}, Ljava/lang/IllegalArgumentException;-><init>(Ljava/lang/String;)V

    throw v1

    .line 1322
    .end local v0    # "lastFromBackStack":Landroidx/navigation/NavBackStackEntry;
    :cond_1
    new-instance v0, Ljava/lang/IllegalStateException;

    const-string v1, "You must call setViewModelStore() before calling getViewModelStoreOwner()."

    invoke-direct {v0, v1}, Ljava/lang/IllegalStateException;-><init>(Ljava/lang/String;)V

    throw v0
.end method

.method public handleDeepLink(Landroid/content/Intent;)Z
    .locals 16
    .param p1, "intent"    # Landroid/content/Intent;

    .line 668
    move-object/from16 v0, p0

    move-object/from16 v1, p1

    const/4 v2, 0x0

    if-nez v1, :cond_0

    .line 669
    return v2

    .line 671
    :cond_0
    invoke-virtual/range {p1 .. p1}, Landroid/content/Intent;->getExtras()Landroid/os/Bundle;

    move-result-object v3

    .line 672
    .local v3, "extras":Landroid/os/Bundle;
    const/4 v4, 0x0

    if-eqz v3, :cond_1

    const-string v5, "android-support-nav:controller:deepLinkIds"

    invoke-virtual {v3, v5}, Landroid/os/Bundle;->getIntArray(Ljava/lang/String;)[I

    move-result-object v5

    goto :goto_0

    :cond_1
    move-object v5, v4

    .line 673
    .local v5, "deepLink":[I
    :goto_0
    new-instance v6, Landroid/os/Bundle;

    invoke-direct {v6}, Landroid/os/Bundle;-><init>()V

    .line 674
    .local v6, "bundle":Landroid/os/Bundle;
    if-eqz v3, :cond_2

    const-string v7, "android-support-nav:controller:deepLinkExtras"

    invoke-virtual {v3, v7}, Landroid/os/Bundle;->getBundle(Ljava/lang/String;)Landroid/os/Bundle;

    move-result-object v7

    goto :goto_1

    :cond_2
    move-object v7, v4

    .line 675
    .local v7, "deepLinkExtras":Landroid/os/Bundle;
    :goto_1
    if-eqz v7, :cond_3

    .line 676
    invoke-virtual {v6, v7}, Landroid/os/Bundle;->putAll(Landroid/os/Bundle;)V

    .line 678
    :cond_3
    if-eqz v5, :cond_4

    array-length v8, v5

    if-nez v8, :cond_5

    :cond_4
    invoke-virtual/range {p1 .. p1}, Landroid/content/Intent;->getData()Landroid/net/Uri;

    move-result-object v8

    if-eqz v8, :cond_5

    .line 679
    iget-object v8, v0, Landroidx/navigation/NavController;->mGraph:Landroidx/navigation/NavGraph;

    new-instance v9, Landroidx/navigation/NavDeepLinkRequest;

    invoke-direct {v9, v1}, Landroidx/navigation/NavDeepLinkRequest;-><init>(Landroid/content/Intent;)V

    .line 680
    invoke-virtual {v8, v9}, Landroidx/navigation/NavGraph;->matchDeepLink(Landroidx/navigation/NavDeepLinkRequest;)Landroidx/navigation/NavDestination$DeepLinkMatch;

    move-result-object v8

    .line 681
    .local v8, "matchingDeepLink":Landroidx/navigation/NavDestination$DeepLinkMatch;
    if-eqz v8, :cond_5

    .line 682
    invoke-virtual {v8}, Landroidx/navigation/NavDestination$DeepLinkMatch;->getDestination()Landroidx/navigation/NavDestination;

    move-result-object v9

    .line 683
    .local v9, "destination":Landroidx/navigation/NavDestination;
    invoke-virtual {v9}, Landroidx/navigation/NavDestination;->buildDeepLinkIds()[I

    move-result-object v5

    .line 684
    nop

    .line 685
    invoke-virtual {v8}, Landroidx/navigation/NavDestination$DeepLinkMatch;->getMatchingArgs()Landroid/os/Bundle;

    move-result-object v10

    invoke-virtual {v9, v10}, Landroidx/navigation/NavDestination;->addInDefaultArgs(Landroid/os/Bundle;)Landroid/os/Bundle;

    move-result-object v10

    .line 686
    .local v10, "destinationArgs":Landroid/os/Bundle;
    invoke-virtual {v6, v10}, Landroid/os/Bundle;->putAll(Landroid/os/Bundle;)V

    .line 689
    .end local v8    # "matchingDeepLink":Landroidx/navigation/NavDestination$DeepLinkMatch;
    .end local v9    # "destination":Landroidx/navigation/NavDestination;
    .end local v10    # "destinationArgs":Landroid/os/Bundle;
    :cond_5
    if-eqz v5, :cond_13

    array-length v8, v5

    if-nez v8, :cond_6

    goto/16 :goto_7

    .line 692
    :cond_6
    nop

    .line 693
    invoke-direct {v0, v5}, Landroidx/navigation/NavController;->findInvalidDestinationDisplayNameInDeepLink([I)Ljava/lang/String;

    move-result-object v8

    .line 694
    .local v8, "invalidDestinationDisplayName":Ljava/lang/String;
    if-eqz v8, :cond_7

    .line 695
    new-instance v4, Ljava/lang/StringBuilder;

    invoke-direct {v4}, Ljava/lang/StringBuilder;-><init>()V

    const-string v9, "Could not find destination "

    invoke-virtual {v4, v9}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v4

    invoke-virtual {v4, v8}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v4

    const-string v9, " in the navigation graph, ignoring the deep link from "

    invoke-virtual {v4, v9}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v4

    invoke-virtual {v4, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    move-result-object v4

    invoke-virtual {v4}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v4

    const-string v9, "NavController"

    invoke-static {v9, v4}, Landroid/util/Log;->i(Ljava/lang/String;Ljava/lang/String;)I

    .line 697
    return v2

    .line 699
    :cond_7
    const-string v9, "android-support-nav:controller:deepLinkIntent"

    invoke-virtual {v6, v9, v1}, Landroid/os/Bundle;->putParcelable(Ljava/lang/String;Landroid/os/Parcelable;)V

    .line 700
    invoke-virtual/range {p1 .. p1}, Landroid/content/Intent;->getFlags()I

    move-result v9

    .line 701
    .local v9, "flags":I
    const/high16 v10, 0x10000000

    and-int v11, v9, v10

    const/4 v12, 0x1

    if-eqz v11, :cond_9

    const v11, 0x8000

    and-int v13, v9, v11

    if-nez v13, :cond_9

    .line 706
    invoke-virtual {v1, v11}, Landroid/content/Intent;->addFlags(I)Landroid/content/Intent;

    .line 707
    iget-object v4, v0, Landroidx/navigation/NavController;->mContext:Landroid/content/Context;

    .line 708
    invoke-static {v4}, Landroidx/core/app/TaskStackBuilder;->create(Landroid/content/Context;)Landroidx/core/app/TaskStackBuilder;

    move-result-object v4

    .line 709
    invoke-virtual {v4, v1}, Landroidx/core/app/TaskStackBuilder;->addNextIntentWithParentStack(Landroid/content/Intent;)Landroidx/core/app/TaskStackBuilder;

    move-result-object v4

    .line 710
    .local v4, "taskStackBuilder":Landroidx/core/app/TaskStackBuilder;
    invoke-virtual {v4}, Landroidx/core/app/TaskStackBuilder;->startActivities()V

    .line 711
    iget-object v10, v0, Landroidx/navigation/NavController;->mActivity:Landroid/app/Activity;

    if-eqz v10, :cond_8

    .line 712
    invoke-virtual {v10}, Landroid/app/Activity;->finish()V

    .line 714
    iget-object v10, v0, Landroidx/navigation/NavController;->mActivity:Landroid/app/Activity;

    invoke-virtual {v10, v2, v2}, Landroid/app/Activity;->overridePendingTransition(II)V

    .line 716
    :cond_8
    return v12

    .line 718
    .end local v4    # "taskStackBuilder":Landroidx/core/app/TaskStackBuilder;
    :cond_9
    and-int/2addr v10, v9

    const-string v11, "Deep Linking failed: destination "

    if-eqz v10, :cond_d

    .line 720
    iget-object v10, v0, Landroidx/navigation/NavController;->mBackStack:Ljava/util/Deque;

    invoke-interface {v10}, Ljava/util/Deque;->isEmpty()Z

    move-result v10

    if-nez v10, :cond_a

    .line 721
    iget-object v10, v0, Landroidx/navigation/NavController;->mGraph:Landroidx/navigation/NavGraph;

    invoke-virtual {v10}, Landroidx/navigation/NavGraph;->getId()I

    move-result v10

    invoke-virtual {v0, v10, v12}, Landroidx/navigation/NavController;->popBackStackInternal(IZ)Z

    .line 723
    :cond_a
    const/4 v10, 0x0

    .line 724
    .local v10, "index":I
    :goto_2
    array-length v13, v5

    if-ge v10, v13, :cond_c

    .line 725
    add-int/lit8 v13, v10, 0x1

    .end local v10    # "index":I
    .local v13, "index":I
    aget v10, v5, v10

    .line 726
    .local v10, "destinationId":I
    invoke-virtual {v0, v10}, Landroidx/navigation/NavController;->findDestination(I)Landroidx/navigation/NavDestination;

    move-result-object v14

    .line 727
    .local v14, "node":Landroidx/navigation/NavDestination;
    if-eqz v14, :cond_b

    .line 734
    new-instance v15, Landroidx/navigation/NavOptions$Builder;

    invoke-direct {v15}, Landroidx/navigation/NavOptions$Builder;-><init>()V

    .line 735
    invoke-virtual {v15, v2}, Landroidx/navigation/NavOptions$Builder;->setEnterAnim(I)Landroidx/navigation/NavOptions$Builder;

    move-result-object v15

    invoke-virtual {v15, v2}, Landroidx/navigation/NavOptions$Builder;->setExitAnim(I)Landroidx/navigation/NavOptions$Builder;

    move-result-object v15

    invoke-virtual {v15}, Landroidx/navigation/NavOptions$Builder;->build()Landroidx/navigation/NavOptions;

    move-result-object v15

    .line 734
    invoke-direct {v0, v14, v6, v15, v4}, Landroidx/navigation/NavController;->navigate(Landroidx/navigation/NavDestination;Landroid/os/Bundle;Landroidx/navigation/NavOptions;Landroidx/navigation/Navigator$Extras;)V

    .line 736
    .end local v10    # "destinationId":I
    .end local v14    # "node":Landroidx/navigation/NavDestination;
    move v10, v13

    goto :goto_2

    .line 728
    .restart local v10    # "destinationId":I
    .restart local v14    # "node":Landroidx/navigation/NavDestination;
    :cond_b
    iget-object v2, v0, Landroidx/navigation/NavController;->mContext:Landroid/content/Context;

    invoke-static {v2, v10}, Landroidx/navigation/NavDestination;->getDisplayName(Landroid/content/Context;I)Ljava/lang/String;

    move-result-object v2

    .line 729
    .local v2, "dest":Ljava/lang/String;
    new-instance v4, Ljava/lang/IllegalStateException;

    new-instance v12, Ljava/lang/StringBuilder;

    invoke-direct {v12}, Ljava/lang/StringBuilder;-><init>()V

    invoke-virtual {v12, v11}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v11

    invoke-virtual {v11, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v11

    const-string v12, " cannot be found from the current destination "

    invoke-virtual {v11, v12}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v11

    .line 732
    invoke-virtual/range {p0 .. p0}, Landroidx/navigation/NavController;->getCurrentDestination()Landroidx/navigation/NavDestination;

    move-result-object v12

    invoke-virtual {v11, v12}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    move-result-object v11

    invoke-virtual {v11}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v11

    invoke-direct {v4, v11}, Ljava/lang/IllegalStateException;-><init>(Ljava/lang/String;)V

    throw v4

    .line 737
    .end local v2    # "dest":Ljava/lang/String;
    .end local v13    # "index":I
    .end local v14    # "node":Landroidx/navigation/NavDestination;
    .local v10, "index":I
    :cond_c
    return v12

    .line 740
    .end local v10    # "index":I
    :cond_d
    iget-object v10, v0, Landroidx/navigation/NavController;->mGraph:Landroidx/navigation/NavGraph;

    .line 741
    .local v10, "graph":Landroidx/navigation/NavGraph;
    const/4 v13, 0x0

    .local v13, "i":I
    :goto_3
    array-length v14, v5

    if-ge v13, v14, :cond_12

    .line 742
    aget v14, v5, v13

    .line 743
    .local v14, "destinationId":I
    if-nez v13, :cond_e

    iget-object v15, v0, Landroidx/navigation/NavController;->mGraph:Landroidx/navigation/NavGraph;

    goto :goto_4

    :cond_e
    invoke-virtual {v10, v14}, Landroidx/navigation/NavGraph;->findNode(I)Landroidx/navigation/NavDestination;

    move-result-object v15

    .line 744
    .local v15, "node":Landroidx/navigation/NavDestination;
    :goto_4
    if-eqz v15, :cond_11

    .line 750
    array-length v4, v5

    sub-int/2addr v4, v12

    if-eq v13, v4, :cond_10

    .line 752
    move-object v4, v15

    check-cast v4, Landroidx/navigation/NavGraph;

    .line 755
    .end local v10    # "graph":Landroidx/navigation/NavGraph;
    .local v4, "graph":Landroidx/navigation/NavGraph;
    :goto_5
    invoke-virtual {v4}, Landroidx/navigation/NavGraph;->getStartDestination()I

    move-result v10

    invoke-virtual {v4, v10}, Landroidx/navigation/NavGraph;->findNode(I)Landroidx/navigation/NavDestination;

    move-result-object v10

    instance-of v10, v10, Landroidx/navigation/NavGraph;

    if-eqz v10, :cond_f

    .line 756
    invoke-virtual {v4}, Landroidx/navigation/NavGraph;->getStartDestination()I

    move-result v10

    invoke-virtual {v4, v10}, Landroidx/navigation/NavGraph;->findNode(I)Landroidx/navigation/NavDestination;

    move-result-object v10

    move-object v4, v10

    check-cast v4, Landroidx/navigation/NavGraph;

    goto :goto_5

    .line 755
    :cond_f
    move-object v10, v4

    const/4 v2, 0x0

    goto :goto_6

    .line 760
    .end local v4    # "graph":Landroidx/navigation/NavGraph;
    .restart local v10    # "graph":Landroidx/navigation/NavGraph;
    :cond_10
    invoke-virtual {v15, v6}, Landroidx/navigation/NavDestination;->addInDefaultArgs(Landroid/os/Bundle;)Landroid/os/Bundle;

    move-result-object v4

    new-instance v2, Landroidx/navigation/NavOptions$Builder;

    invoke-direct {v2}, Landroidx/navigation/NavOptions$Builder;-><init>()V

    iget-object v12, v0, Landroidx/navigation/NavController;->mGraph:Landroidx/navigation/NavGraph;

    .line 761
    invoke-virtual {v12}, Landroidx/navigation/NavGraph;->getId()I

    move-result v12

    const/4 v1, 0x1

    invoke-virtual {v2, v12, v1}, Landroidx/navigation/NavOptions$Builder;->setPopUpTo(IZ)Landroidx/navigation/NavOptions$Builder;

    move-result-object v2

    .line 762
    const/4 v1, 0x0

    invoke-virtual {v2, v1}, Landroidx/navigation/NavOptions$Builder;->setEnterAnim(I)Landroidx/navigation/NavOptions$Builder;

    move-result-object v2

    invoke-virtual {v2, v1}, Landroidx/navigation/NavOptions$Builder;->setExitAnim(I)Landroidx/navigation/NavOptions$Builder;

    move-result-object v2

    invoke-virtual {v2}, Landroidx/navigation/NavOptions$Builder;->build()Landroidx/navigation/NavOptions;

    move-result-object v1

    .line 760
    const/4 v2, 0x0

    invoke-direct {v0, v15, v4, v1, v2}, Landroidx/navigation/NavController;->navigate(Landroidx/navigation/NavDestination;Landroid/os/Bundle;Landroidx/navigation/NavOptions;Landroidx/navigation/Navigator$Extras;)V

    .line 741
    .end local v14    # "destinationId":I
    .end local v15    # "node":Landroidx/navigation/NavDestination;
    :goto_6
    add-int/lit8 v13, v13, 0x1

    move-object/from16 v1, p1

    move-object v4, v2

    const/4 v2, 0x0

    const/4 v12, 0x1

    goto :goto_3

    .line 745
    .restart local v14    # "destinationId":I
    .restart local v15    # "node":Landroidx/navigation/NavDestination;
    :cond_11
    iget-object v1, v0, Landroidx/navigation/NavController;->mContext:Landroid/content/Context;

    invoke-static {v1, v14}, Landroidx/navigation/NavDestination;->getDisplayName(Landroid/content/Context;I)Ljava/lang/String;

    move-result-object v1

    .line 746
    .local v1, "dest":Ljava/lang/String;
    new-instance v2, Ljava/lang/IllegalStateException;

    new-instance v4, Ljava/lang/StringBuilder;

    invoke-direct {v4}, Ljava/lang/StringBuilder;-><init>()V

    invoke-virtual {v4, v11}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v4

    invoke-virtual {v4, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v4

    const-string v11, " cannot be found in graph "

    invoke-virtual {v4, v11}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v4

    invoke-virtual {v4, v10}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    move-result-object v4

    invoke-virtual {v4}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v4

    invoke-direct {v2, v4}, Ljava/lang/IllegalStateException;-><init>(Ljava/lang/String;)V

    throw v2

    .line 765
    .end local v1    # "dest":Ljava/lang/String;
    .end local v13    # "i":I
    .end local v14    # "destinationId":I
    .end local v15    # "node":Landroidx/navigation/NavDestination;
    :cond_12
    const/4 v1, 0x1

    iput-boolean v1, v0, Landroidx/navigation/NavController;->mDeepLinkHandled:Z

    .line 766
    return v1

    .line 690
    .end local v8    # "invalidDestinationDisplayName":Ljava/lang/String;
    .end local v9    # "flags":I
    .end local v10    # "graph":Landroidx/navigation/NavGraph;
    :cond_13
    :goto_7
    const/4 v1, 0x0

    return v1
.end method

.method public navigate(I)V
    .locals 1
    .param p1, "resId"    # I

    .line 851
    const/4 v0, 0x0

    invoke-virtual {p0, p1, v0}, Landroidx/navigation/NavController;->navigate(ILandroid/os/Bundle;)V

    .line 852
    return-void
.end method

.method public navigate(ILandroid/os/Bundle;)V
    .locals 1
    .param p1, "resId"    # I
    .param p2, "args"    # Landroid/os/Bundle;

    .line 863
    const/4 v0, 0x0

    invoke-virtual {p0, p1, p2, v0}, Landroidx/navigation/NavController;->navigate(ILandroid/os/Bundle;Landroidx/navigation/NavOptions;)V

    .line 864
    return-void
.end method

.method public navigate(ILandroid/os/Bundle;Landroidx/navigation/NavOptions;)V
    .locals 1
    .param p1, "resId"    # I
    .param p2, "args"    # Landroid/os/Bundle;
    .param p3, "navOptions"    # Landroidx/navigation/NavOptions;

    .line 877
    const/4 v0, 0x0

    invoke-virtual {p0, p1, p2, p3, v0}, Landroidx/navigation/NavController;->navigate(ILandroid/os/Bundle;Landroidx/navigation/NavOptions;Landroidx/navigation/Navigator$Extras;)V

    .line 878
    return-void
.end method

.method public navigate(ILandroid/os/Bundle;Landroidx/navigation/NavOptions;Landroidx/navigation/Navigator$Extras;)V
    .locals 10
    .param p1, "resId"    # I
    .param p2, "args"    # Landroid/os/Bundle;
    .param p3, "navOptions"    # Landroidx/navigation/NavOptions;
    .param p4, "navigatorExtras"    # Landroidx/navigation/Navigator$Extras;

    .line 893
    iget-object v0, p0, Landroidx/navigation/NavController;->mBackStack:Ljava/util/Deque;

    invoke-interface {v0}, Ljava/util/Deque;->isEmpty()Z

    move-result v0

    if-eqz v0, :cond_0

    .line 894
    iget-object v0, p0, Landroidx/navigation/NavController;->mGraph:Landroidx/navigation/NavGraph;

    goto :goto_0

    .line 895
    :cond_0
    iget-object v0, p0, Landroidx/navigation/NavController;->mBackStack:Ljava/util/Deque;

    invoke-interface {v0}, Ljava/util/Deque;->getLast()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Landroidx/navigation/NavBackStackEntry;

    invoke-virtual {v0}, Landroidx/navigation/NavBackStackEntry;->getDestination()Landroidx/navigation/NavDestination;

    move-result-object v0

    :goto_0
    nop

    .line 896
    .local v0, "currentNode":Landroidx/navigation/NavDestination;
    if-eqz v0, :cond_9

    .line 899
    move v1, p1

    .line 900
    .local v1, "destId":I
    invoke-virtual {v0, p1}, Landroidx/navigation/NavDestination;->getAction(I)Landroidx/navigation/NavAction;

    move-result-object v2

    .line 901
    .local v2, "navAction":Landroidx/navigation/NavAction;
    const/4 v3, 0x0

    .line 902
    .local v3, "combinedArgs":Landroid/os/Bundle;
    if-eqz v2, :cond_2

    .line 903
    if-nez p3, :cond_1

    .line 904
    invoke-virtual {v2}, Landroidx/navigation/NavAction;->getNavOptions()Landroidx/navigation/NavOptions;

    move-result-object p3

    .line 906
    :cond_1
    invoke-virtual {v2}, Landroidx/navigation/NavAction;->getDestinationId()I

    move-result v1

    .line 907
    invoke-virtual {v2}, Landroidx/navigation/NavAction;->getDefaultArguments()Landroid/os/Bundle;

    move-result-object v4

    .line 908
    .local v4, "navActionArgs":Landroid/os/Bundle;
    if-eqz v4, :cond_2

    .line 909
    new-instance v5, Landroid/os/Bundle;

    invoke-direct {v5}, Landroid/os/Bundle;-><init>()V

    move-object v3, v5

    .line 910
    invoke-virtual {v3, v4}, Landroid/os/Bundle;->putAll(Landroid/os/Bundle;)V

    .line 914
    .end local v4    # "navActionArgs":Landroid/os/Bundle;
    :cond_2
    if-eqz p2, :cond_4

    .line 915
    if-nez v3, :cond_3

    .line 916
    new-instance v4, Landroid/os/Bundle;

    invoke-direct {v4}, Landroid/os/Bundle;-><init>()V

    move-object v3, v4

    .line 918
    :cond_3
    invoke-virtual {v3, p2}, Landroid/os/Bundle;->putAll(Landroid/os/Bundle;)V

    .line 921
    :cond_4
    if-nez v1, :cond_5

    if-eqz p3, :cond_5

    invoke-virtual {p3}, Landroidx/navigation/NavOptions;->getPopUpTo()I

    move-result v4

    const/4 v5, -0x1

    if-eq v4, v5, :cond_5

    .line 922
    invoke-virtual {p3}, Landroidx/navigation/NavOptions;->getPopUpTo()I

    move-result v4

    invoke-virtual {p3}, Landroidx/navigation/NavOptions;->isPopUpToInclusive()Z

    move-result v5

    invoke-virtual {p0, v4, v5}, Landroidx/navigation/NavController;->popBackStack(IZ)Z

    .line 923
    return-void

    .line 926
    :cond_5
    if-eqz v1, :cond_8

    .line 931
    invoke-virtual {p0, v1}, Landroidx/navigation/NavController;->findDestination(I)Landroidx/navigation/NavDestination;

    move-result-object v4

    .line 932
    .local v4, "node":Landroidx/navigation/NavDestination;
    if-nez v4, :cond_7

    .line 933
    iget-object v5, p0, Landroidx/navigation/NavController;->mContext:Landroid/content/Context;

    invoke-static {v5, v1}, Landroidx/navigation/NavDestination;->getDisplayName(Landroid/content/Context;I)Ljava/lang/String;

    move-result-object v5

    .line 934
    .local v5, "dest":Ljava/lang/String;
    const-string v6, " cannot be found from the current destination "

    if-eqz v2, :cond_6

    .line 935
    new-instance v7, Ljava/lang/IllegalArgumentException;

    new-instance v8, Ljava/lang/StringBuilder;

    invoke-direct {v8}, Ljava/lang/StringBuilder;-><init>()V

    const-string v9, "Navigation destination "

    invoke-virtual {v8, v9}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v8

    invoke-virtual {v8, v5}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v8

    const-string v9, " referenced from action "

    invoke-virtual {v8, v9}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v8

    iget-object v9, p0, Landroidx/navigation/NavController;->mContext:Landroid/content/Context;

    .line 937
    invoke-static {v9, p1}, Landroidx/navigation/NavDestination;->getDisplayName(Landroid/content/Context;I)Ljava/lang/String;

    move-result-object v9

    invoke-virtual {v8, v9}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v8

    invoke-virtual {v8, v6}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v6

    invoke-virtual {v6, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    move-result-object v6

    invoke-virtual {v6}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v6

    invoke-direct {v7, v6}, Ljava/lang/IllegalArgumentException;-><init>(Ljava/lang/String;)V

    throw v7

    .line 940
    :cond_6
    new-instance v7, Ljava/lang/IllegalArgumentException;

    new-instance v8, Ljava/lang/StringBuilder;

    invoke-direct {v8}, Ljava/lang/StringBuilder;-><init>()V

    const-string v9, "Navigation action/destination "

    invoke-virtual {v8, v9}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v8

    invoke-virtual {v8, v5}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v8

    invoke-virtual {v8, v6}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v6

    invoke-virtual {v6, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    move-result-object v6

    invoke-virtual {v6}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v6

    invoke-direct {v7, v6}, Ljava/lang/IllegalArgumentException;-><init>(Ljava/lang/String;)V

    throw v7

    .line 944
    .end local v5    # "dest":Ljava/lang/String;
    :cond_7
    invoke-direct {p0, v4, v3, p3, p4}, Landroidx/navigation/NavController;->navigate(Landroidx/navigation/NavDestination;Landroid/os/Bundle;Landroidx/navigation/NavOptions;Landroidx/navigation/Navigator$Extras;)V

    .line 945
    return-void

    .line 927
    .end local v4    # "node":Landroidx/navigation/NavDestination;
    :cond_8
    new-instance v4, Ljava/lang/IllegalArgumentException;

    const-string v5, "Destination id == 0 can only be used in conjunction with a valid navOptions.popUpTo"

    invoke-direct {v4, v5}, Ljava/lang/IllegalArgumentException;-><init>(Ljava/lang/String;)V

    throw v4

    .line 897
    .end local v1    # "destId":I
    .end local v2    # "navAction":Landroidx/navigation/NavAction;
    .end local v3    # "combinedArgs":Landroid/os/Bundle;
    :cond_9
    new-instance v1, Ljava/lang/IllegalStateException;

    const-string v2, "no current navigation node"

    invoke-direct {v1, v2}, Ljava/lang/IllegalStateException;-><init>(Ljava/lang/String;)V

    throw v1
.end method

.method public navigate(Landroid/net/Uri;)V
    .locals 2
    .param p1, "deepLink"    # Landroid/net/Uri;

    .line 958
    new-instance v0, Landroidx/navigation/NavDeepLinkRequest;

    const/4 v1, 0x0

    invoke-direct {v0, p1, v1, v1}, Landroidx/navigation/NavDeepLinkRequest;-><init>(Landroid/net/Uri;Ljava/lang/String;Ljava/lang/String;)V

    invoke-virtual {p0, v0}, Landroidx/navigation/NavController;->navigate(Landroidx/navigation/NavDeepLinkRequest;)V

    .line 959
    return-void
.end method

.method public navigate(Landroid/net/Uri;Landroidx/navigation/NavOptions;)V
    .locals 2
    .param p1, "deepLink"    # Landroid/net/Uri;
    .param p2, "navOptions"    # Landroidx/navigation/NavOptions;

    .line 973
    new-instance v0, Landroidx/navigation/NavDeepLinkRequest;

    const/4 v1, 0x0

    invoke-direct {v0, p1, v1, v1}, Landroidx/navigation/NavDeepLinkRequest;-><init>(Landroid/net/Uri;Ljava/lang/String;Ljava/lang/String;)V

    invoke-virtual {p0, v0, p2}, Landroidx/navigation/NavController;->navigate(Landroidx/navigation/NavDeepLinkRequest;Landroidx/navigation/NavOptions;)V

    .line 974
    return-void
.end method

.method public navigate(Landroid/net/Uri;Landroidx/navigation/NavOptions;Landroidx/navigation/Navigator$Extras;)V
    .locals 2
    .param p1, "deepLink"    # Landroid/net/Uri;
    .param p2, "navOptions"    # Landroidx/navigation/NavOptions;
    .param p3, "navigatorExtras"    # Landroidx/navigation/Navigator$Extras;

    .line 990
    new-instance v0, Landroidx/navigation/NavDeepLinkRequest;

    const/4 v1, 0x0

    invoke-direct {v0, p1, v1, v1}, Landroidx/navigation/NavDeepLinkRequest;-><init>(Landroid/net/Uri;Ljava/lang/String;Ljava/lang/String;)V

    invoke-virtual {p0, v0, p2, p3}, Landroidx/navigation/NavController;->navigate(Landroidx/navigation/NavDeepLinkRequest;Landroidx/navigation/NavOptions;Landroidx/navigation/Navigator$Extras;)V

    .line 991
    return-void
.end method

.method public navigate(Landroidx/navigation/NavDeepLinkRequest;)V
    .locals 1
    .param p1, "request"    # Landroidx/navigation/NavDeepLinkRequest;

    .line 1003
    const/4 v0, 0x0

    invoke-virtual {p0, p1, v0}, Landroidx/navigation/NavController;->navigate(Landroidx/navigation/NavDeepLinkRequest;Landroidx/navigation/NavOptions;)V

    .line 1004
    return-void
.end method

.method public navigate(Landroidx/navigation/NavDeepLinkRequest;Landroidx/navigation/NavOptions;)V
    .locals 1
    .param p1, "request"    # Landroidx/navigation/NavDeepLinkRequest;
    .param p2, "navOptions"    # Landroidx/navigation/NavOptions;

    .line 1017
    const/4 v0, 0x0

    invoke-virtual {p0, p1, p2, v0}, Landroidx/navigation/NavController;->navigate(Landroidx/navigation/NavDeepLinkRequest;Landroidx/navigation/NavOptions;Landroidx/navigation/Navigator$Extras;)V

    .line 1018
    return-void
.end method

.method public navigate(Landroidx/navigation/NavDeepLinkRequest;Landroidx/navigation/NavOptions;Landroidx/navigation/Navigator$Extras;)V
    .locals 7
    .param p1, "request"    # Landroidx/navigation/NavDeepLinkRequest;
    .param p2, "navOptions"    # Landroidx/navigation/NavOptions;
    .param p3, "navigatorExtras"    # Landroidx/navigation/Navigator$Extras;

    .line 1033
    iget-object v0, p0, Landroidx/navigation/NavController;->mGraph:Landroidx/navigation/NavGraph;

    .line 1034
    invoke-virtual {v0, p1}, Landroidx/navigation/NavGraph;->matchDeepLink(Landroidx/navigation/NavDeepLinkRequest;)Landroidx/navigation/NavDestination$DeepLinkMatch;

    move-result-object v0

    .line 1035
    .local v0, "deepLinkMatch":Landroidx/navigation/NavDestination$DeepLinkMatch;
    if-eqz v0, :cond_1

    .line 1036
    invoke-virtual {v0}, Landroidx/navigation/NavDestination$DeepLinkMatch;->getDestination()Landroidx/navigation/NavDestination;

    move-result-object v1

    .line 1037
    .local v1, "destination":Landroidx/navigation/NavDestination;
    invoke-virtual {v0}, Landroidx/navigation/NavDestination$DeepLinkMatch;->getMatchingArgs()Landroid/os/Bundle;

    move-result-object v2

    invoke-virtual {v1, v2}, Landroidx/navigation/NavDestination;->addInDefaultArgs(Landroid/os/Bundle;)Landroid/os/Bundle;

    move-result-object v2

    .line 1038
    .local v2, "args":Landroid/os/Bundle;
    if-nez v2, :cond_0

    .line 1039
    new-instance v3, Landroid/os/Bundle;

    invoke-direct {v3}, Landroid/os/Bundle;-><init>()V

    move-object v2, v3

    .line 1041
    :cond_0
    invoke-virtual {v0}, Landroidx/navigation/NavDestination$DeepLinkMatch;->getDestination()Landroidx/navigation/NavDestination;

    move-result-object v3

    .line 1042
    .local v3, "node":Landroidx/navigation/NavDestination;
    new-instance v4, Landroid/content/Intent;

    invoke-direct {v4}, Landroid/content/Intent;-><init>()V

    .line 1043
    .local v4, "intent":Landroid/content/Intent;
    invoke-virtual {p1}, Landroidx/navigation/NavDeepLinkRequest;->getUri()Landroid/net/Uri;

    move-result-object v5

    invoke-virtual {p1}, Landroidx/navigation/NavDeepLinkRequest;->getMimeType()Ljava/lang/String;

    move-result-object v6

    invoke-virtual {v4, v5, v6}, Landroid/content/Intent;->setDataAndType(Landroid/net/Uri;Ljava/lang/String;)Landroid/content/Intent;

    .line 1044
    invoke-virtual {p1}, Landroidx/navigation/NavDeepLinkRequest;->getAction()Ljava/lang/String;

    move-result-object v5

    invoke-virtual {v4, v5}, Landroid/content/Intent;->setAction(Ljava/lang/String;)Landroid/content/Intent;

    .line 1045
    const-string v5, "android-support-nav:controller:deepLinkIntent"

    invoke-virtual {v2, v5, v4}, Landroid/os/Bundle;->putParcelable(Ljava/lang/String;Landroid/os/Parcelable;)V

    .line 1046
    invoke-direct {p0, v3, v2, p2, p3}, Landroidx/navigation/NavController;->navigate(Landroidx/navigation/NavDestination;Landroid/os/Bundle;Landroidx/navigation/NavOptions;Landroidx/navigation/Navigator$Extras;)V

    .line 1047
    .end local v1    # "destination":Landroidx/navigation/NavDestination;
    .end local v2    # "args":Landroid/os/Bundle;
    .end local v3    # "node":Landroidx/navigation/NavDestination;
    .end local v4    # "intent":Landroid/content/Intent;
    nop

    .line 1051
    return-void

    .line 1048
    :cond_1
    new-instance v1, Ljava/lang/IllegalArgumentException;

    new-instance v2, Ljava/lang/StringBuilder;

    invoke-direct {v2}, Ljava/lang/StringBuilder;-><init>()V

    const-string v3, "Navigation destination that matches request "

    invoke-virtual {v2, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v2

    invoke-virtual {v2, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    move-result-object v2

    const-string v3, " cannot be found in the navigation graph "

    invoke-virtual {v2, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v2

    iget-object v3, p0, Landroidx/navigation/NavController;->mGraph:Landroidx/navigation/NavGraph;

    invoke-virtual {v2, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    move-result-object v2

    invoke-virtual {v2}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v2

    invoke-direct {v1, v2}, Ljava/lang/IllegalArgumentException;-><init>(Ljava/lang/String;)V

    throw v1
.end method

.method public navigate(Landroidx/navigation/NavDirections;)V
    .locals 2
    .param p1, "directions"    # Landroidx/navigation/NavDirections;

    .line 1159
    invoke-interface {p1}, Landroidx/navigation/NavDirections;->getActionId()I

    move-result v0

    invoke-interface {p1}, Landroidx/navigation/NavDirections;->getArguments()Landroid/os/Bundle;

    move-result-object v1

    invoke-virtual {p0, v0, v1}, Landroidx/navigation/NavController;->navigate(ILandroid/os/Bundle;)V

    .line 1160
    return-void
.end method

.method public navigate(Landroidx/navigation/NavDirections;Landroidx/navigation/NavOptions;)V
    .locals 2
    .param p1, "directions"    # Landroidx/navigation/NavDirections;
    .param p2, "navOptions"    # Landroidx/navigation/NavOptions;

    .line 1169
    invoke-interface {p1}, Landroidx/navigation/NavDirections;->getActionId()I

    move-result v0

    invoke-interface {p1}, Landroidx/navigation/NavDirections;->getArguments()Landroid/os/Bundle;

    move-result-object v1

    invoke-virtual {p0, v0, v1, p2}, Landroidx/navigation/NavController;->navigate(ILandroid/os/Bundle;Landroidx/navigation/NavOptions;)V

    .line 1170
    return-void
.end method

.method public navigate(Landroidx/navigation/NavDirections;Landroidx/navigation/Navigator$Extras;)V
    .locals 3
    .param p1, "directions"    # Landroidx/navigation/NavDirections;
    .param p2, "navigatorExtras"    # Landroidx/navigation/Navigator$Extras;

    .line 1180
    invoke-interface {p1}, Landroidx/navigation/NavDirections;->getActionId()I

    move-result v0

    invoke-interface {p1}, Landroidx/navigation/NavDirections;->getArguments()Landroid/os/Bundle;

    move-result-object v1

    const/4 v2, 0x0

    invoke-virtual {p0, v0, v1, v2, p2}, Landroidx/navigation/NavController;->navigate(ILandroid/os/Bundle;Landroidx/navigation/NavOptions;Landroidx/navigation/Navigator$Extras;)V

    .line 1181
    return-void
.end method

.method public navigateUp()Z
    .locals 9

    .line 356
    invoke-direct {p0}, Landroidx/navigation/NavController;->getDestinationCountOnBackStack()I

    move-result v0

    const/4 v1, 0x1

    if-ne v0, v1, :cond_4

    .line 359
    invoke-virtual {p0}, Landroidx/navigation/NavController;->getCurrentDestination()Landroidx/navigation/NavDestination;

    move-result-object v0

    .line 360
    .local v0, "currentDestination":Landroidx/navigation/NavDestination;
    invoke-virtual {v0}, Landroidx/navigation/NavDestination;->getId()I

    move-result v2

    .line 361
    .local v2, "destId":I
    invoke-virtual {v0}, Landroidx/navigation/NavDestination;->getParent()Landroidx/navigation/NavGraph;

    move-result-object v3

    .line 362
    .local v3, "parent":Landroidx/navigation/NavGraph;
    :goto_0
    if-eqz v3, :cond_3

    .line 363
    invoke-virtual {v3}, Landroidx/navigation/NavGraph;->getStartDestination()I

    move-result v4

    if-eq v4, v2, :cond_2

    .line 364
    new-instance v4, Landroid/os/Bundle;

    invoke-direct {v4}, Landroid/os/Bundle;-><init>()V

    .line 366
    .local v4, "args":Landroid/os/Bundle;
    iget-object v5, p0, Landroidx/navigation/NavController;->mActivity:Landroid/app/Activity;

    if-eqz v5, :cond_0

    invoke-virtual {v5}, Landroid/app/Activity;->getIntent()Landroid/content/Intent;

    move-result-object v5

    if-eqz v5, :cond_0

    .line 367
    iget-object v5, p0, Landroidx/navigation/NavController;->mActivity:Landroid/app/Activity;

    invoke-virtual {v5}, Landroid/app/Activity;->getIntent()Landroid/content/Intent;

    move-result-object v5

    invoke-virtual {v5}, Landroid/content/Intent;->getData()Landroid/net/Uri;

    move-result-object v5

    .line 370
    .local v5, "data":Landroid/net/Uri;
    if-eqz v5, :cond_0

    .line 373
    iget-object v6, p0, Landroidx/navigation/NavController;->mActivity:Landroid/app/Activity;

    invoke-virtual {v6}, Landroid/app/Activity;->getIntent()Landroid/content/Intent;

    move-result-object v6

    const-string v7, "android-support-nav:controller:deepLinkIntent"

    invoke-virtual {v4, v7, v6}, Landroid/os/Bundle;->putParcelable(Ljava/lang/String;Landroid/os/Parcelable;)V

    .line 374
    iget-object v6, p0, Landroidx/navigation/NavController;->mGraph:Landroidx/navigation/NavGraph;

    new-instance v7, Landroidx/navigation/NavDeepLinkRequest;

    iget-object v8, p0, Landroidx/navigation/NavController;->mActivity:Landroid/app/Activity;

    .line 375
    invoke-virtual {v8}, Landroid/app/Activity;->getIntent()Landroid/content/Intent;

    move-result-object v8

    invoke-direct {v7, v8}, Landroidx/navigation/NavDeepLinkRequest;-><init>(Landroid/content/Intent;)V

    .line 374
    invoke-virtual {v6, v7}, Landroidx/navigation/NavGraph;->matchDeepLink(Landroidx/navigation/NavDeepLinkRequest;)Landroidx/navigation/NavDestination$DeepLinkMatch;

    move-result-object v6

    .line 376
    .local v6, "matchingDeepLink":Landroidx/navigation/NavDestination$DeepLinkMatch;
    if-eqz v6, :cond_0

    .line 377
    nop

    .line 378
    invoke-virtual {v6}, Landroidx/navigation/NavDestination$DeepLinkMatch;->getDestination()Landroidx/navigation/NavDestination;

    move-result-object v7

    .line 379
    invoke-virtual {v6}, Landroidx/navigation/NavDestination$DeepLinkMatch;->getMatchingArgs()Landroid/os/Bundle;

    move-result-object v8

    .line 378
    invoke-virtual {v7, v8}, Landroidx/navigation/NavDestination;->addInDefaultArgs(Landroid/os/Bundle;)Landroid/os/Bundle;

    move-result-object v7

    .line 381
    .local v7, "destinationArgs":Landroid/os/Bundle;
    invoke-virtual {v4, v7}, Landroid/os/Bundle;->putAll(Landroid/os/Bundle;)V

    .line 386
    .end local v5    # "data":Landroid/net/Uri;
    .end local v6    # "matchingDeepLink":Landroidx/navigation/NavDestination$DeepLinkMatch;
    .end local v7    # "destinationArgs":Landroid/os/Bundle;
    :cond_0
    new-instance v5, Landroidx/navigation/NavDeepLinkBuilder;

    invoke-direct {v5, p0}, Landroidx/navigation/NavDeepLinkBuilder;-><init>(Landroidx/navigation/NavController;)V

    .line 387
    invoke-virtual {v3}, Landroidx/navigation/NavGraph;->getId()I

    move-result v6

    invoke-virtual {v5, v6}, Landroidx/navigation/NavDeepLinkBuilder;->setDestination(I)Landroidx/navigation/NavDeepLinkBuilder;

    move-result-object v5

    .line 388
    invoke-virtual {v5, v4}, Landroidx/navigation/NavDeepLinkBuilder;->setArguments(Landroid/os/Bundle;)Landroidx/navigation/NavDeepLinkBuilder;

    move-result-object v5

    .line 389
    invoke-virtual {v5}, Landroidx/navigation/NavDeepLinkBuilder;->createTaskStackBuilder()Landroidx/core/app/TaskStackBuilder;

    move-result-object v5

    .line 391
    .local v5, "parentIntents":Landroidx/core/app/TaskStackBuilder;
    invoke-virtual {v5}, Landroidx/core/app/TaskStackBuilder;->startActivities()V

    .line 392
    iget-object v6, p0, Landroidx/navigation/NavController;->mActivity:Landroid/app/Activity;

    if-eqz v6, :cond_1

    .line 393
    invoke-virtual {v6}, Landroid/app/Activity;->finish()V

    .line 395
    :cond_1
    return v1

    .line 397
    .end local v4    # "args":Landroid/os/Bundle;
    .end local v5    # "parentIntents":Landroidx/core/app/TaskStackBuilder;
    :cond_2
    invoke-virtual {v3}, Landroidx/navigation/NavGraph;->getId()I

    move-result v2

    .line 398
    invoke-virtual {v3}, Landroidx/navigation/NavGraph;->getParent()Landroidx/navigation/NavGraph;

    move-result-object v3

    goto :goto_0

    .line 401
    :cond_3
    const/4 v1, 0x0

    return v1

    .line 403
    .end local v0    # "currentDestination":Landroidx/navigation/NavDestination;
    .end local v2    # "destId":I
    .end local v3    # "parent":Landroidx/navigation/NavGraph;
    :cond_4
    invoke-virtual {p0}, Landroidx/navigation/NavController;->popBackStack()Z

    move-result v0

    return v0
.end method

.method public popBackStack()Z
    .locals 2

    .line 259
    iget-object v0, p0, Landroidx/navigation/NavController;->mBackStack:Ljava/util/Deque;

    invoke-interface {v0}, Ljava/util/Deque;->isEmpty()Z

    move-result v0

    if-eqz v0, :cond_0

    .line 261
    const/4 v0, 0x0

    return v0

    .line 264
    :cond_0
    invoke-virtual {p0}, Landroidx/navigation/NavController;->getCurrentDestination()Landroidx/navigation/NavDestination;

    move-result-object v0

    invoke-virtual {v0}, Landroidx/navigation/NavDestination;->getId()I

    move-result v0

    const/4 v1, 0x1

    invoke-virtual {p0, v0, v1}, Landroidx/navigation/NavController;->popBackStack(IZ)Z

    move-result v0

    return v0
.end method

.method public popBackStack(IZ)Z
    .locals 2
    .param p1, "destinationId"    # I
    .param p2, "inclusive"    # Z

    .line 277
    invoke-virtual {p0, p1, p2}, Landroidx/navigation/NavController;->popBackStackInternal(IZ)Z

    move-result v0

    .line 280
    .local v0, "popped":Z
    if-eqz v0, :cond_0

    invoke-direct {p0}, Landroidx/navigation/NavController;->dispatchOnDestinationChanged()Z

    move-result v1

    if-eqz v1, :cond_0

    const/4 v1, 0x1

    goto :goto_0

    :cond_0
    const/4 v1, 0x0

    :goto_0
    return v1
.end method

.method popBackStackInternal(IZ)Z
    .locals 9
    .param p1, "destinationId"    # I
    .param p2, "inclusive"    # Z

    .line 294
    iget-object v0, p0, Landroidx/navigation/NavController;->mBackStack:Ljava/util/Deque;

    invoke-interface {v0}, Ljava/util/Deque;->isEmpty()Z

    move-result v0

    const/4 v1, 0x0

    if-eqz v0, :cond_0

    .line 296
    return v1

    .line 298
    :cond_0
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    .line 299
    .local v0, "popOperations":Ljava/util/ArrayList;, "Ljava/util/ArrayList<Landroidx/navigation/Navigator<*>;>;"
    iget-object v2, p0, Landroidx/navigation/NavController;->mBackStack:Ljava/util/Deque;

    invoke-interface {v2}, Ljava/util/Deque;->descendingIterator()Ljava/util/Iterator;

    move-result-object v2

    .line 300
    .local v2, "iterator":Ljava/util/Iterator;, "Ljava/util/Iterator<Landroidx/navigation/NavBackStackEntry;>;"
    const/4 v3, 0x0

    .line 301
    .local v3, "foundDestination":Z
    :goto_0
    invoke-interface {v2}, Ljava/util/Iterator;->hasNext()Z

    move-result v4

    if-eqz v4, :cond_4

    .line 302
    invoke-interface {v2}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v4

    check-cast v4, Landroidx/navigation/NavBackStackEntry;

    invoke-virtual {v4}, Landroidx/navigation/NavBackStackEntry;->getDestination()Landroidx/navigation/NavDestination;

    move-result-object v4

    .line 303
    .local v4, "destination":Landroidx/navigation/NavDestination;
    iget-object v5, p0, Landroidx/navigation/NavController;->mNavigatorProvider:Landroidx/navigation/NavigatorProvider;

    .line 304
    invoke-virtual {v4}, Landroidx/navigation/NavDestination;->getNavigatorName()Ljava/lang/String;

    move-result-object v6

    .line 303
    invoke-virtual {v5, v6}, Landroidx/navigation/NavigatorProvider;->getNavigator(Ljava/lang/String;)Landroidx/navigation/Navigator;

    move-result-object v5

    .line 305
    .local v5, "navigator":Landroidx/navigation/Navigator;, "Landroidx/navigation/Navigator<*>;"
    if-nez p2, :cond_1

    invoke-virtual {v4}, Landroidx/navigation/NavDestination;->getId()I

    move-result v6

    if-eq v6, p1, :cond_2

    .line 306
    :cond_1
    invoke-virtual {v0, v5}, Ljava/util/ArrayList;->add(Ljava/lang/Object;)Z

    .line 308
    :cond_2
    invoke-virtual {v4}, Landroidx/navigation/NavDestination;->getId()I

    move-result v6

    if-ne v6, p1, :cond_3

    .line 309
    const/4 v3, 0x1

    .line 310
    goto :goto_1

    .line 312
    .end local v4    # "destination":Landroidx/navigation/NavDestination;
    .end local v5    # "navigator":Landroidx/navigation/Navigator;, "Landroidx/navigation/Navigator<*>;"
    :cond_3
    goto :goto_0

    .line 313
    :cond_4
    :goto_1
    if-nez v3, :cond_5

    .line 316
    iget-object v4, p0, Landroidx/navigation/NavController;->mContext:Landroid/content/Context;

    invoke-static {v4, p1}, Landroidx/navigation/NavDestination;->getDisplayName(Landroid/content/Context;I)Ljava/lang/String;

    move-result-object v4

    .line 317
    .local v4, "destinationName":Ljava/lang/String;
    new-instance v5, Ljava/lang/StringBuilder;

    invoke-direct {v5}, Ljava/lang/StringBuilder;-><init>()V

    const-string v6, "Ignoring popBackStack to destination "

    invoke-virtual {v5, v6}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v5

    invoke-virtual {v5, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v5

    const-string v6, " as it was not found on the current back stack"

    invoke-virtual {v5, v6}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v5

    invoke-virtual {v5}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v5

    const-string v6, "NavController"

    invoke-static {v6, v5}, Landroid/util/Log;->i(Ljava/lang/String;Ljava/lang/String;)I

    .line 319
    return v1

    .line 321
    .end local v4    # "destinationName":Ljava/lang/String;
    :cond_5
    const/4 v1, 0x0

    .line 322
    .local v1, "popped":Z
    invoke-virtual {v0}, Ljava/util/ArrayList;->iterator()Ljava/util/Iterator;

    move-result-object v4

    :goto_2
    invoke-interface {v4}, Ljava/util/Iterator;->hasNext()Z

    move-result v5

    if-eqz v5, :cond_8

    invoke-interface {v4}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v5

    check-cast v5, Landroidx/navigation/Navigator;

    .line 323
    .restart local v5    # "navigator":Landroidx/navigation/Navigator;, "Landroidx/navigation/Navigator<*>;"
    invoke-virtual {v5}, Landroidx/navigation/Navigator;->popBackStack()Z

    move-result v6

    if-eqz v6, :cond_8

    .line 324
    iget-object v6, p0, Landroidx/navigation/NavController;->mBackStack:Ljava/util/Deque;

    invoke-interface {v6}, Ljava/util/Deque;->removeLast()Ljava/lang/Object;

    move-result-object v6

    check-cast v6, Landroidx/navigation/NavBackStackEntry;

    .line 325
    .local v6, "entry":Landroidx/navigation/NavBackStackEntry;
    invoke-virtual {v6}, Landroidx/navigation/NavBackStackEntry;->getLifecycle()Landroidx/lifecycle/Lifecycle;

    move-result-object v7

    invoke-virtual {v7}, Landroidx/lifecycle/Lifecycle;->getCurrentState()Landroidx/lifecycle/Lifecycle$State;

    move-result-object v7

    sget-object v8, Landroidx/lifecycle/Lifecycle$State;->CREATED:Landroidx/lifecycle/Lifecycle$State;

    invoke-virtual {v7, v8}, Landroidx/lifecycle/Lifecycle$State;->isAtLeast(Landroidx/lifecycle/Lifecycle$State;)Z

    move-result v7

    if-eqz v7, :cond_6

    .line 326
    sget-object v7, Landroidx/lifecycle/Lifecycle$State;->DESTROYED:Landroidx/lifecycle/Lifecycle$State;

    invoke-virtual {v6, v7}, Landroidx/navigation/NavBackStackEntry;->setMaxLifecycle(Landroidx/lifecycle/Lifecycle$State;)V

    .line 328
    :cond_6
    iget-object v7, p0, Landroidx/navigation/NavController;->mViewModel:Landroidx/navigation/NavControllerViewModel;

    if-eqz v7, :cond_7

    .line 329
    iget-object v8, v6, Landroidx/navigation/NavBackStackEntry;->mId:Ljava/util/UUID;

    invoke-virtual {v7, v8}, Landroidx/navigation/NavControllerViewModel;->clear(Ljava/util/UUID;)V

    .line 331
    :cond_7
    const/4 v1, 0x1

    .line 336
    .end local v5    # "navigator":Landroidx/navigation/Navigator;, "Landroidx/navigation/Navigator<*>;"
    .end local v6    # "entry":Landroidx/navigation/NavBackStackEntry;
    goto :goto_2

    .line 337
    :cond_8
    invoke-direct {p0}, Landroidx/navigation/NavController;->updateOnBackPressedCallbackEnabled()V

    .line 338
    return v1
.end method

.method public removeOnDestinationChangedListener(Landroidx/navigation/NavController$OnDestinationChangedListener;)V
    .locals 1
    .param p1, "listener"    # Landroidx/navigation/NavController$OnDestinationChangedListener;

    .line 247
    iget-object v0, p0, Landroidx/navigation/NavController;->mOnDestinationChangedListeners:Ljava/util/concurrent/CopyOnWriteArrayList;

    invoke-virtual {v0, p1}, Ljava/util/concurrent/CopyOnWriteArrayList;->remove(Ljava/lang/Object;)Z

    .line 248
    return-void
.end method

.method public restoreState(Landroid/os/Bundle;)V
    .locals 1
    .param p1, "navState"    # Landroid/os/Bundle;

    .line 1253
    if-nez p1, :cond_0

    .line 1254
    return-void

    .line 1257
    :cond_0
    iget-object v0, p0, Landroidx/navigation/NavController;->mContext:Landroid/content/Context;

    invoke-virtual {v0}, Landroid/content/Context;->getClassLoader()Ljava/lang/ClassLoader;

    move-result-object v0

    invoke-virtual {p1, v0}, Landroid/os/Bundle;->setClassLoader(Ljava/lang/ClassLoader;)V

    .line 1259
    const-string v0, "android-support-nav:controller:navigatorState"

    invoke-virtual {p1, v0}, Landroid/os/Bundle;->getBundle(Ljava/lang/String;)Landroid/os/Bundle;

    move-result-object v0

    iput-object v0, p0, Landroidx/navigation/NavController;->mNavigatorStateToRestore:Landroid/os/Bundle;

    .line 1260
    const-string v0, "android-support-nav:controller:backStack"

    invoke-virtual {p1, v0}, Landroid/os/Bundle;->getParcelableArray(Ljava/lang/String;)[Landroid/os/Parcelable;

    move-result-object v0

    iput-object v0, p0, Landroidx/navigation/NavController;->mBackStackToRestore:[Landroid/os/Parcelable;

    .line 1261
    const-string v0, "android-support-nav:controller:deepLinkHandled"

    invoke-virtual {p1, v0}, Landroid/os/Bundle;->getBoolean(Ljava/lang/String;)Z

    move-result v0

    iput-boolean v0, p0, Landroidx/navigation/NavController;->mDeepLinkHandled:Z

    .line 1262
    return-void
.end method

.method public saveState()Landroid/os/Bundle;
    .locals 9

    .line 1205
    const/4 v0, 0x0

    .line 1206
    .local v0, "b":Landroid/os/Bundle;
    new-instance v1, Ljava/util/ArrayList;

    invoke-direct {v1}, Ljava/util/ArrayList;-><init>()V

    .line 1207
    .local v1, "navigatorNames":Ljava/util/ArrayList;, "Ljava/util/ArrayList<Ljava/lang/String;>;"
    new-instance v2, Landroid/os/Bundle;

    invoke-direct {v2}, Landroid/os/Bundle;-><init>()V

    .line 1209
    .local v2, "navigatorState":Landroid/os/Bundle;
    iget-object v3, p0, Landroidx/navigation/NavController;->mNavigatorProvider:Landroidx/navigation/NavigatorProvider;

    invoke-virtual {v3}, Landroidx/navigation/NavigatorProvider;->getNavigators()Ljava/util/Map;

    move-result-object v3

    invoke-interface {v3}, Ljava/util/Map;->entrySet()Ljava/util/Set;

    move-result-object v3

    invoke-interface {v3}, Ljava/util/Set;->iterator()Ljava/util/Iterator;

    move-result-object v3

    :goto_0
    invoke-interface {v3}, Ljava/util/Iterator;->hasNext()Z

    move-result v4

    if-eqz v4, :cond_1

    invoke-interface {v3}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v4

    check-cast v4, Ljava/util/Map$Entry;

    .line 1210
    .local v4, "entry":Ljava/util/Map$Entry;, "Ljava/util/Map$Entry<Ljava/lang/String;Landroidx/navigation/Navigator<+Landroidx/navigation/NavDestination;>;>;"
    invoke-interface {v4}, Ljava/util/Map$Entry;->getKey()Ljava/lang/Object;

    move-result-object v5

    check-cast v5, Ljava/lang/String;

    .line 1211
    .local v5, "name":Ljava/lang/String;
    invoke-interface {v4}, Ljava/util/Map$Entry;->getValue()Ljava/lang/Object;

    move-result-object v6

    check-cast v6, Landroidx/navigation/Navigator;

    invoke-virtual {v6}, Landroidx/navigation/Navigator;->onSaveState()Landroid/os/Bundle;

    move-result-object v6

    .line 1212
    .local v6, "savedState":Landroid/os/Bundle;
    if-eqz v6, :cond_0

    .line 1213
    invoke-virtual {v1, v5}, Ljava/util/ArrayList;->add(Ljava/lang/Object;)Z

    .line 1214
    invoke-virtual {v2, v5, v6}, Landroid/os/Bundle;->putBundle(Ljava/lang/String;Landroid/os/Bundle;)V

    .line 1216
    .end local v4    # "entry":Ljava/util/Map$Entry;, "Ljava/util/Map$Entry<Ljava/lang/String;Landroidx/navigation/Navigator<+Landroidx/navigation/NavDestination;>;>;"
    .end local v5    # "name":Ljava/lang/String;
    .end local v6    # "savedState":Landroid/os/Bundle;
    :cond_0
    goto :goto_0

    .line 1217
    :cond_1
    invoke-virtual {v1}, Ljava/util/ArrayList;->isEmpty()Z

    move-result v3

    if-nez v3, :cond_2

    .line 1218
    new-instance v3, Landroid/os/Bundle;

    invoke-direct {v3}, Landroid/os/Bundle;-><init>()V

    move-object v0, v3

    .line 1219
    const-string v3, "android-support-nav:controller:navigatorState:names"

    invoke-virtual {v2, v3, v1}, Landroid/os/Bundle;->putStringArrayList(Ljava/lang/String;Ljava/util/ArrayList;)V

    .line 1220
    const-string v3, "android-support-nav:controller:navigatorState"

    invoke-virtual {v0, v3, v2}, Landroid/os/Bundle;->putBundle(Ljava/lang/String;Landroid/os/Bundle;)V

    .line 1222
    :cond_2
    iget-object v3, p0, Landroidx/navigation/NavController;->mBackStack:Ljava/util/Deque;

    invoke-interface {v3}, Ljava/util/Deque;->isEmpty()Z

    move-result v3

    if-nez v3, :cond_5

    .line 1223
    if-nez v0, :cond_3

    .line 1224
    new-instance v3, Landroid/os/Bundle;

    invoke-direct {v3}, Landroid/os/Bundle;-><init>()V

    move-object v0, v3

    .line 1226
    :cond_3
    iget-object v3, p0, Landroidx/navigation/NavController;->mBackStack:Ljava/util/Deque;

    invoke-interface {v3}, Ljava/util/Deque;->size()I

    move-result v3

    new-array v3, v3, [Landroid/os/Parcelable;

    .line 1227
    .local v3, "backStack":[Landroid/os/Parcelable;
    const/4 v4, 0x0

    .line 1228
    .local v4, "index":I
    iget-object v5, p0, Landroidx/navigation/NavController;->mBackStack:Ljava/util/Deque;

    invoke-interface {v5}, Ljava/util/Deque;->iterator()Ljava/util/Iterator;

    move-result-object v5

    :goto_1
    invoke-interface {v5}, Ljava/util/Iterator;->hasNext()Z

    move-result v6

    if-eqz v6, :cond_4

    invoke-interface {v5}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v6

    check-cast v6, Landroidx/navigation/NavBackStackEntry;

    .line 1229
    .local v6, "backStackEntry":Landroidx/navigation/NavBackStackEntry;
    add-int/lit8 v7, v4, 0x1

    .end local v4    # "index":I
    .local v7, "index":I
    new-instance v8, Landroidx/navigation/NavBackStackEntryState;

    invoke-direct {v8, v6}, Landroidx/navigation/NavBackStackEntryState;-><init>(Landroidx/navigation/NavBackStackEntry;)V

    aput-object v8, v3, v4

    .line 1230
    .end local v6    # "backStackEntry":Landroidx/navigation/NavBackStackEntry;
    move v4, v7

    goto :goto_1

    .line 1231
    .end local v7    # "index":I
    .restart local v4    # "index":I
    :cond_4
    const-string v5, "android-support-nav:controller:backStack"

    invoke-virtual {v0, v5, v3}, Landroid/os/Bundle;->putParcelableArray(Ljava/lang/String;[Landroid/os/Parcelable;)V

    .line 1233
    .end local v3    # "backStack":[Landroid/os/Parcelable;
    .end local v4    # "index":I
    :cond_5
    iget-boolean v3, p0, Landroidx/navigation/NavController;->mDeepLinkHandled:Z

    if-eqz v3, :cond_7

    .line 1234
    if-nez v0, :cond_6

    .line 1235
    new-instance v3, Landroid/os/Bundle;

    invoke-direct {v3}, Landroid/os/Bundle;-><init>()V

    move-object v0, v3

    .line 1237
    :cond_6
    iget-boolean v3, p0, Landroidx/navigation/NavController;->mDeepLinkHandled:Z

    const-string v4, "android-support-nav:controller:deepLinkHandled"

    invoke-virtual {v0, v4, v3}, Landroid/os/Bundle;->putBoolean(Ljava/lang/String;Z)V

    .line 1239
    :cond_7
    return-object v0
.end method

.method public setGraph(I)V
    .locals 1
    .param p1, "graphResId"    # I

    .line 539
    const/4 v0, 0x0

    invoke-virtual {p0, p1, v0}, Landroidx/navigation/NavController;->setGraph(ILandroid/os/Bundle;)V

    .line 540
    return-void
.end method

.method public setGraph(ILandroid/os/Bundle;)V
    .locals 1
    .param p1, "graphResId"    # I
    .param p2, "startDestinationArgs"    # Landroid/os/Bundle;

    .line 557
    invoke-virtual {p0}, Landroidx/navigation/NavController;->getNavInflater()Landroidx/navigation/NavInflater;

    move-result-object v0

    invoke-virtual {v0, p1}, Landroidx/navigation/NavInflater;->inflate(I)Landroidx/navigation/NavGraph;

    move-result-object v0

    invoke-virtual {p0, v0, p2}, Landroidx/navigation/NavController;->setGraph(Landroidx/navigation/NavGraph;Landroid/os/Bundle;)V

    .line 558
    return-void
.end method

.method public setGraph(Landroidx/navigation/NavGraph;)V
    .locals 1
    .param p1, "graph"    # Landroidx/navigation/NavGraph;

    .line 572
    const/4 v0, 0x0

    invoke-virtual {p0, p1, v0}, Landroidx/navigation/NavController;->setGraph(Landroidx/navigation/NavGraph;Landroid/os/Bundle;)V

    .line 573
    return-void
.end method

.method public setGraph(Landroidx/navigation/NavGraph;Landroid/os/Bundle;)V
    .locals 2
    .param p1, "graph"    # Landroidx/navigation/NavGraph;
    .param p2, "startDestinationArgs"    # Landroid/os/Bundle;

    .line 587
    iget-object v0, p0, Landroidx/navigation/NavController;->mGraph:Landroidx/navigation/NavGraph;

    if-eqz v0, :cond_0

    .line 589
    invoke-virtual {v0}, Landroidx/navigation/NavGraph;->getId()I

    move-result v0

    const/4 v1, 0x1

    invoke-virtual {p0, v0, v1}, Landroidx/navigation/NavController;->popBackStackInternal(IZ)Z

    .line 591
    :cond_0
    iput-object p1, p0, Landroidx/navigation/NavController;->mGraph:Landroidx/navigation/NavGraph;

    .line 592
    invoke-direct {p0, p2}, Landroidx/navigation/NavController;->onGraphCreated(Landroid/os/Bundle;)V

    .line 593
    return-void
.end method

.method setLifecycleOwner(Landroidx/lifecycle/LifecycleOwner;)V
    .locals 2
    .param p1, "owner"    # Landroidx/lifecycle/LifecycleOwner;

    .line 1265
    iget-object v0, p0, Landroidx/navigation/NavController;->mLifecycleOwner:Landroidx/lifecycle/LifecycleOwner;

    if-ne p1, v0, :cond_0

    .line 1266
    return-void

    .line 1268
    :cond_0
    iput-object p1, p0, Landroidx/navigation/NavController;->mLifecycleOwner:Landroidx/lifecycle/LifecycleOwner;

    .line 1269
    invoke-interface {p1}, Landroidx/lifecycle/LifecycleOwner;->getLifecycle()Landroidx/lifecycle/Lifecycle;

    move-result-object v0

    iget-object v1, p0, Landroidx/navigation/NavController;->mLifecycleObserver:Landroidx/lifecycle/LifecycleObserver;

    invoke-virtual {v0, v1}, Landroidx/lifecycle/Lifecycle;->addObserver(Landroidx/lifecycle/LifecycleObserver;)V

    .line 1270
    return-void
.end method

.method public setNavigatorProvider(Landroidx/navigation/NavigatorProvider;)V
    .locals 2
    .param p1, "navigatorProvider"    # Landroidx/navigation/NavigatorProvider;

    .line 215
    iget-object v0, p0, Landroidx/navigation/NavController;->mBackStack:Ljava/util/Deque;

    invoke-interface {v0}, Ljava/util/Deque;->isEmpty()Z

    move-result v0

    if-eqz v0, :cond_0

    .line 218
    iput-object p1, p0, Landroidx/navigation/NavController;->mNavigatorProvider:Landroidx/navigation/NavigatorProvider;

    .line 219
    return-void

    .line 216
    :cond_0
    new-instance v0, Ljava/lang/IllegalStateException;

    const-string v1, "NavigatorProvider must be set before setGraph call"

    invoke-direct {v0, v1}, Ljava/lang/IllegalStateException;-><init>(Ljava/lang/String;)V

    throw v0
.end method

.method setOnBackPressedDispatcher(Landroidx/activity/OnBackPressedDispatcher;)V
    .locals 2
    .param p1, "dispatcher"    # Landroidx/activity/OnBackPressedDispatcher;

    .line 1273
    iget-object v0, p0, Landroidx/navigation/NavController;->mLifecycleOwner:Landroidx/lifecycle/LifecycleOwner;

    if-eqz v0, :cond_0

    .line 1278
    iget-object v0, p0, Landroidx/navigation/NavController;->mOnBackPressedCallback:Landroidx/activity/OnBackPressedCallback;

    invoke-virtual {v0}, Landroidx/activity/OnBackPressedCallback;->remove()V

    .line 1280
    iget-object v0, p0, Landroidx/navigation/NavController;->mLifecycleOwner:Landroidx/lifecycle/LifecycleOwner;

    iget-object v1, p0, Landroidx/navigation/NavController;->mOnBackPressedCallback:Landroidx/activity/OnBackPressedCallback;

    invoke-virtual {p1, v0, v1}, Landroidx/activity/OnBackPressedDispatcher;->addCallback(Landroidx/lifecycle/LifecycleOwner;Landroidx/activity/OnBackPressedCallback;)V

    .line 1284
    iget-object v0, p0, Landroidx/navigation/NavController;->mLifecycleOwner:Landroidx/lifecycle/LifecycleOwner;

    invoke-interface {v0}, Landroidx/lifecycle/LifecycleOwner;->getLifecycle()Landroidx/lifecycle/Lifecycle;

    move-result-object v0

    iget-object v1, p0, Landroidx/navigation/NavController;->mLifecycleObserver:Landroidx/lifecycle/LifecycleObserver;

    invoke-virtual {v0, v1}, Landroidx/lifecycle/Lifecycle;->removeObserver(Landroidx/lifecycle/LifecycleObserver;)V

    .line 1285
    iget-object v0, p0, Landroidx/navigation/NavController;->mLifecycleOwner:Landroidx/lifecycle/LifecycleOwner;

    invoke-interface {v0}, Landroidx/lifecycle/LifecycleOwner;->getLifecycle()Landroidx/lifecycle/Lifecycle;

    move-result-object v0

    iget-object v1, p0, Landroidx/navigation/NavController;->mLifecycleObserver:Landroidx/lifecycle/LifecycleObserver;

    invoke-virtual {v0, v1}, Landroidx/lifecycle/Lifecycle;->addObserver(Landroidx/lifecycle/LifecycleObserver;)V

    .line 1286
    return-void

    .line 1274
    :cond_0
    new-instance v0, Ljava/lang/IllegalStateException;

    const-string v1, "You must call setLifecycleOwner() before calling setOnBackPressedDispatcher()"

    invoke-direct {v0, v1}, Ljava/lang/IllegalStateException;-><init>(Ljava/lang/String;)V

    throw v0
.end method

.method setViewModelStore(Landroidx/lifecycle/ViewModelStore;)V
    .locals 2
    .param p1, "viewModelStore"    # Landroidx/lifecycle/ViewModelStore;

    .line 1299
    iget-object v0, p0, Landroidx/navigation/NavController;->mViewModel:Landroidx/navigation/NavControllerViewModel;

    invoke-static {p1}, Landroidx/navigation/NavControllerViewModel;->getInstance(Landroidx/lifecycle/ViewModelStore;)Landroidx/navigation/NavControllerViewModel;

    move-result-object v1

    if-ne v0, v1, :cond_0

    .line 1300
    return-void

    .line 1302
    :cond_0
    iget-object v0, p0, Landroidx/navigation/NavController;->mBackStack:Ljava/util/Deque;

    invoke-interface {v0}, Ljava/util/Deque;->isEmpty()Z

    move-result v0

    if-eqz v0, :cond_1

    .line 1305
    invoke-static {p1}, Landroidx/navigation/NavControllerViewModel;->getInstance(Landroidx/lifecycle/ViewModelStore;)Landroidx/navigation/NavControllerViewModel;

    move-result-object v0

    iput-object v0, p0, Landroidx/navigation/NavController;->mViewModel:Landroidx/navigation/NavControllerViewModel;

    .line 1306
    return-void

    .line 1303
    :cond_1
    new-instance v0, Ljava/lang/IllegalStateException;

    const-string v1, "ViewModelStore should be set before setGraph call"

    invoke-direct {v0, v1}, Ljava/lang/IllegalStateException;-><init>(Ljava/lang/String;)V

    throw v0
.end method
