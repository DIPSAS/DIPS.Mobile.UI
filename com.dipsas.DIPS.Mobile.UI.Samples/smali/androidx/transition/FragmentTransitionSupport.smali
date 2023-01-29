.class public Landroidx/transition/FragmentTransitionSupport;
.super Landroidx/fragment/app/FragmentTransitionImpl;
.source "FragmentTransitionSupport.java"


# direct methods
.method public constructor <init>()V
    .locals 0

    .line 44
    invoke-direct {p0}, Landroidx/fragment/app/FragmentTransitionImpl;-><init>()V

    return-void
.end method

.method private static hasSimpleTarget(Landroidx/transition/Transition;)Z
    .locals 1
    .param p0, "transition"    # Landroidx/transition/Transition;

    .line 128
    invoke-virtual {p0}, Landroidx/transition/Transition;->getTargetIds()Ljava/util/List;

    move-result-object v0

    invoke-static {v0}, Landroidx/transition/FragmentTransitionSupport;->isNullOrEmpty(Ljava/util/List;)Z

    move-result v0

    if-eqz v0, :cond_1

    .line 129
    invoke-virtual {p0}, Landroidx/transition/Transition;->getTargetNames()Ljava/util/List;

    move-result-object v0

    invoke-static {v0}, Landroidx/transition/FragmentTransitionSupport;->isNullOrEmpty(Ljava/util/List;)Z

    move-result v0

    if-eqz v0, :cond_1

    .line 130
    invoke-virtual {p0}, Landroidx/transition/Transition;->getTargetTypes()Ljava/util/List;

    move-result-object v0

    invoke-static {v0}, Landroidx/transition/FragmentTransitionSupport;->isNullOrEmpty(Ljava/util/List;)Z

    move-result v0

    if-nez v0, :cond_0

    goto :goto_0

    :cond_0
    const/4 v0, 0x0

    goto :goto_1

    :cond_1
    :goto_0
    const/4 v0, 0x1

    .line 128
    :goto_1
    return v0
.end method


# virtual methods
.method public addTarget(Ljava/lang/Object;Landroid/view/View;)V
    .locals 1
    .param p1, "transitionObj"    # Ljava/lang/Object;
    .param p2, "view"    # Landroid/view/View;

    .line 334
    if-eqz p1, :cond_0

    .line 335
    move-object v0, p1

    check-cast v0, Landroidx/transition/Transition;

    .line 336
    .local v0, "transition":Landroidx/transition/Transition;
    invoke-virtual {v0, p2}, Landroidx/transition/Transition;->addTarget(Landroid/view/View;)Landroidx/transition/Transition;

    .line 338
    .end local v0    # "transition":Landroidx/transition/Transition;
    :cond_0
    return-void
.end method

.method public addTargets(Ljava/lang/Object;Ljava/util/ArrayList;)V
    .locals 5
    .param p1, "transitionObj"    # Ljava/lang/Object;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/lang/Object;",
            "Ljava/util/ArrayList<",
            "Landroid/view/View;",
            ">;)V"
        }
    .end annotation

    .line 104
    .local p2, "views":Ljava/util/ArrayList;, "Ljava/util/ArrayList<Landroid/view/View;>;"
    move-object v0, p1

    check-cast v0, Landroidx/transition/Transition;

    .line 105
    .local v0, "transition":Landroidx/transition/Transition;
    if-nez v0, :cond_0

    .line 106
    return-void

    .line 108
    :cond_0
    instance-of v1, v0, Landroidx/transition/TransitionSet;

    if-eqz v1, :cond_2

    .line 109
    move-object v1, v0

    check-cast v1, Landroidx/transition/TransitionSet;

    .line 110
    .local v1, "set":Landroidx/transition/TransitionSet;
    invoke-virtual {v1}, Landroidx/transition/TransitionSet;->getTransitionCount()I

    move-result v2

    .line 111
    .local v2, "numTransitions":I
    const/4 v3, 0x0

    .local v3, "i":I
    :goto_0
    if-ge v3, v2, :cond_1

    .line 112
    invoke-virtual {v1, v3}, Landroidx/transition/TransitionSet;->getTransitionAt(I)Landroidx/transition/Transition;

    move-result-object v4

    .line 113
    .local v4, "child":Landroidx/transition/Transition;
    invoke-virtual {p0, v4, p2}, Landroidx/transition/FragmentTransitionSupport;->addTargets(Ljava/lang/Object;Ljava/util/ArrayList;)V

    .line 111
    .end local v4    # "child":Landroidx/transition/Transition;
    add-int/lit8 v3, v3, 0x1

    goto :goto_0

    .end local v1    # "set":Landroidx/transition/TransitionSet;
    .end local v2    # "numTransitions":I
    .end local v3    # "i":I
    :cond_1
    goto :goto_2

    .line 115
    :cond_2
    invoke-static {v0}, Landroidx/transition/FragmentTransitionSupport;->hasSimpleTarget(Landroidx/transition/Transition;)Z

    move-result v1

    if-nez v1, :cond_3

    .line 116
    invoke-virtual {v0}, Landroidx/transition/Transition;->getTargets()Ljava/util/List;

    move-result-object v1

    .line 117
    .local v1, "targets":Ljava/util/List;, "Ljava/util/List<Landroid/view/View;>;"
    invoke-static {v1}, Landroidx/transition/FragmentTransitionSupport;->isNullOrEmpty(Ljava/util/List;)Z

    move-result v2

    if-eqz v2, :cond_4

    .line 119
    invoke-virtual {p2}, Ljava/util/ArrayList;->size()I

    move-result v2

    .line 120
    .local v2, "numViews":I
    const/4 v3, 0x0

    .restart local v3    # "i":I
    :goto_1
    if-ge v3, v2, :cond_4

    .line 121
    invoke-virtual {p2, v3}, Ljava/util/ArrayList;->get(I)Ljava/lang/Object;

    move-result-object v4

    check-cast v4, Landroid/view/View;

    invoke-virtual {v0, v4}, Landroidx/transition/Transition;->addTarget(Landroid/view/View;)Landroidx/transition/Transition;

    .line 120
    add-int/lit8 v3, v3, 0x1

    goto :goto_1

    .line 115
    .end local v1    # "targets":Ljava/util/List;, "Ljava/util/List<Landroid/view/View;>;"
    .end local v2    # "numViews":I
    .end local v3    # "i":I
    :cond_3
    :goto_2
    nop

    .line 125
    :cond_4
    return-void
.end method

.method public beginDelayedTransition(Landroid/view/ViewGroup;Ljava/lang/Object;)V
    .locals 1
    .param p1, "sceneRoot"    # Landroid/view/ViewGroup;
    .param p2, "transition"    # Ljava/lang/Object;

    .line 223
    move-object v0, p2

    check-cast v0, Landroidx/transition/Transition;

    invoke-static {p1, v0}, Landroidx/transition/TransitionManager;->beginDelayedTransition(Landroid/view/ViewGroup;Landroidx/transition/Transition;)V

    .line 224
    return-void
.end method

.method public canHandle(Ljava/lang/Object;)Z
    .locals 1
    .param p1, "transition"    # Ljava/lang/Object;

    .line 48
    instance-of v0, p1, Landroidx/transition/Transition;

    return v0
.end method

.method public cloneTransition(Ljava/lang/Object;)Ljava/lang/Object;
    .locals 2
    .param p1, "transition"    # Ljava/lang/Object;

    .line 53
    const/4 v0, 0x0

    .line 54
    .local v0, "copy":Landroidx/transition/Transition;
    if-eqz p1, :cond_0

    .line 55
    move-object v1, p1

    check-cast v1, Landroidx/transition/Transition;

    invoke-virtual {v1}, Landroidx/transition/Transition;->clone()Landroidx/transition/Transition;

    move-result-object v0

    .line 57
    :cond_0
    return-object v0
.end method

.method public mergeTransitionsInSequence(Ljava/lang/Object;Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;
    .locals 6
    .param p1, "exitTransitionObj"    # Ljava/lang/Object;
    .param p2, "enterTransitionObj"    # Ljava/lang/Object;
    .param p3, "sharedElementTransitionObj"    # Ljava/lang/Object;

    .line 195
    const/4 v0, 0x0

    .line 196
    .local v0, "staggered":Landroidx/transition/Transition;
    move-object v1, p1

    check-cast v1, Landroidx/transition/Transition;

    .line 197
    .local v1, "exitTransition":Landroidx/transition/Transition;
    move-object v2, p2

    check-cast v2, Landroidx/transition/Transition;

    .line 198
    .local v2, "enterTransition":Landroidx/transition/Transition;
    move-object v3, p3

    check-cast v3, Landroidx/transition/Transition;

    .line 199
    .local v3, "sharedElementTransition":Landroidx/transition/Transition;
    if-eqz v1, :cond_0

    if-eqz v2, :cond_0

    .line 200
    new-instance v4, Landroidx/transition/TransitionSet;

    invoke-direct {v4}, Landroidx/transition/TransitionSet;-><init>()V

    .line 201
    invoke-virtual {v4, v1}, Landroidx/transition/TransitionSet;->addTransition(Landroidx/transition/Transition;)Landroidx/transition/TransitionSet;

    move-result-object v4

    .line 202
    invoke-virtual {v4, v2}, Landroidx/transition/TransitionSet;->addTransition(Landroidx/transition/Transition;)Landroidx/transition/TransitionSet;

    move-result-object v4

    const/4 v5, 0x1

    .line 203
    invoke-virtual {v4, v5}, Landroidx/transition/TransitionSet;->setOrdering(I)Landroidx/transition/TransitionSet;

    move-result-object v0

    goto :goto_0

    .line 204
    :cond_0
    if-eqz v1, :cond_1

    .line 205
    move-object v0, v1

    goto :goto_0

    .line 206
    :cond_1
    if-eqz v2, :cond_2

    .line 207
    move-object v0, v2

    .line 209
    :cond_2
    :goto_0
    if-eqz v3, :cond_4

    .line 210
    new-instance v4, Landroidx/transition/TransitionSet;

    invoke-direct {v4}, Landroidx/transition/TransitionSet;-><init>()V

    .line 211
    .local v4, "together":Landroidx/transition/TransitionSet;
    if-eqz v0, :cond_3

    .line 212
    invoke-virtual {v4, v0}, Landroidx/transition/TransitionSet;->addTransition(Landroidx/transition/Transition;)Landroidx/transition/TransitionSet;

    .line 214
    :cond_3
    invoke-virtual {v4, v3}, Landroidx/transition/TransitionSet;->addTransition(Landroidx/transition/Transition;)Landroidx/transition/TransitionSet;

    .line 215
    return-object v4

    .line 217
    .end local v4    # "together":Landroidx/transition/TransitionSet;
    :cond_4
    return-object v0
.end method

.method public mergeTransitionsTogether(Ljava/lang/Object;Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;
    .locals 2
    .param p1, "transition1"    # Ljava/lang/Object;
    .param p2, "transition2"    # Ljava/lang/Object;
    .param p3, "transition3"    # Ljava/lang/Object;

    .line 136
    new-instance v0, Landroidx/transition/TransitionSet;

    invoke-direct {v0}, Landroidx/transition/TransitionSet;-><init>()V

    .line 137
    .local v0, "transitionSet":Landroidx/transition/TransitionSet;
    if-eqz p1, :cond_0

    .line 138
    move-object v1, p1

    check-cast v1, Landroidx/transition/Transition;

    invoke-virtual {v0, v1}, Landroidx/transition/TransitionSet;->addTransition(Landroidx/transition/Transition;)Landroidx/transition/TransitionSet;

    .line 140
    :cond_0
    if-eqz p2, :cond_1

    .line 141
    move-object v1, p2

    check-cast v1, Landroidx/transition/Transition;

    invoke-virtual {v0, v1}, Landroidx/transition/TransitionSet;->addTransition(Landroidx/transition/Transition;)Landroidx/transition/TransitionSet;

    .line 143
    :cond_1
    if-eqz p3, :cond_2

    .line 144
    move-object v1, p3

    check-cast v1, Landroidx/transition/Transition;

    invoke-virtual {v0, v1}, Landroidx/transition/TransitionSet;->addTransition(Landroidx/transition/Transition;)Landroidx/transition/TransitionSet;

    .line 146
    :cond_2
    return-object v0
.end method

.method public removeTarget(Ljava/lang/Object;Landroid/view/View;)V
    .locals 1
    .param p1, "transitionObj"    # Ljava/lang/Object;
    .param p2, "view"    # Landroid/view/View;

    .line 342
    if-eqz p1, :cond_0

    .line 343
    move-object v0, p1

    check-cast v0, Landroidx/transition/Transition;

    .line 344
    .local v0, "transition":Landroidx/transition/Transition;
    invoke-virtual {v0, p2}, Landroidx/transition/Transition;->removeTarget(Landroid/view/View;)Landroidx/transition/Transition;

    .line 346
    .end local v0    # "transition":Landroidx/transition/Transition;
    :cond_0
    return-void
.end method

.method public replaceTargets(Ljava/lang/Object;Ljava/util/ArrayList;Ljava/util/ArrayList;)V
    .locals 5
    .param p1, "transitionObj"    # Ljava/lang/Object;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/lang/Object;",
            "Ljava/util/ArrayList<",
            "Landroid/view/View;",
            ">;",
            "Ljava/util/ArrayList<",
            "Landroid/view/View;",
            ">;)V"
        }
    .end annotation

    .line 308
    .local p2, "oldTargets":Ljava/util/ArrayList;, "Ljava/util/ArrayList<Landroid/view/View;>;"
    .local p3, "newTargets":Ljava/util/ArrayList;, "Ljava/util/ArrayList<Landroid/view/View;>;"
    move-object v0, p1

    check-cast v0, Landroidx/transition/Transition;

    .line 309
    .local v0, "transition":Landroidx/transition/Transition;
    instance-of v1, v0, Landroidx/transition/TransitionSet;

    if-eqz v1, :cond_1

    .line 310
    move-object v1, v0

    check-cast v1, Landroidx/transition/TransitionSet;

    .line 311
    .local v1, "set":Landroidx/transition/TransitionSet;
    invoke-virtual {v1}, Landroidx/transition/TransitionSet;->getTransitionCount()I

    move-result v2

    .line 312
    .local v2, "numTransitions":I
    const/4 v3, 0x0

    .local v3, "i":I
    :goto_0
    if-ge v3, v2, :cond_0

    .line 313
    invoke-virtual {v1, v3}, Landroidx/transition/TransitionSet;->getTransitionAt(I)Landroidx/transition/Transition;

    move-result-object v4

    .line 314
    .local v4, "child":Landroidx/transition/Transition;
    invoke-virtual {p0, v4, p2, p3}, Landroidx/transition/FragmentTransitionSupport;->replaceTargets(Ljava/lang/Object;Ljava/util/ArrayList;Ljava/util/ArrayList;)V

    .line 312
    .end local v4    # "child":Landroidx/transition/Transition;
    add-int/lit8 v3, v3, 0x1

    goto :goto_0

    .end local v1    # "set":Landroidx/transition/TransitionSet;
    .end local v2    # "numTransitions":I
    .end local v3    # "i":I
    :cond_0
    goto :goto_4

    .line 316
    :cond_1
    invoke-static {v0}, Landroidx/transition/FragmentTransitionSupport;->hasSimpleTarget(Landroidx/transition/Transition;)Z

    move-result v1

    if-nez v1, :cond_4

    .line 317
    invoke-virtual {v0}, Landroidx/transition/Transition;->getTargets()Ljava/util/List;

    move-result-object v1

    .line 318
    .local v1, "targets":Ljava/util/List;, "Ljava/util/List<Landroid/view/View;>;"
    invoke-interface {v1}, Ljava/util/List;->size()I

    move-result v2

    invoke-virtual {p2}, Ljava/util/ArrayList;->size()I

    move-result v3

    if-ne v2, v3, :cond_5

    .line 319
    invoke-interface {v1, p2}, Ljava/util/List;->containsAll(Ljava/util/Collection;)Z

    move-result v2

    if-eqz v2, :cond_5

    .line 321
    if-nez p3, :cond_2

    const/4 v2, 0x0

    goto :goto_1

    :cond_2
    invoke-virtual {p3}, Ljava/util/ArrayList;->size()I

    move-result v2

    .line 322
    .local v2, "targetCount":I
    :goto_1
    const/4 v3, 0x0

    .restart local v3    # "i":I
    :goto_2
    if-ge v3, v2, :cond_3

    .line 323
    invoke-virtual {p3, v3}, Ljava/util/ArrayList;->get(I)Ljava/lang/Object;

    move-result-object v4

    check-cast v4, Landroid/view/View;

    invoke-virtual {v0, v4}, Landroidx/transition/Transition;->addTarget(Landroid/view/View;)Landroidx/transition/Transition;

    .line 322
    add-int/lit8 v3, v3, 0x1

    goto :goto_2

    .line 325
    .end local v3    # "i":I
    :cond_3
    invoke-virtual {p2}, Ljava/util/ArrayList;->size()I

    move-result v3

    add-int/lit8 v3, v3, -0x1

    .restart local v3    # "i":I
    :goto_3
    if-ltz v3, :cond_5

    .line 326
    invoke-virtual {p2, v3}, Ljava/util/ArrayList;->get(I)Ljava/lang/Object;

    move-result-object v4

    check-cast v4, Landroid/view/View;

    invoke-virtual {v0, v4}, Landroidx/transition/Transition;->removeTarget(Landroid/view/View;)Landroidx/transition/Transition;

    .line 325
    add-int/lit8 v3, v3, -0x1

    goto :goto_3

    .line 316
    .end local v1    # "targets":Ljava/util/List;, "Ljava/util/List<Landroid/view/View;>;"
    .end local v2    # "targetCount":I
    .end local v3    # "i":I
    :cond_4
    :goto_4
    nop

    .line 330
    :cond_5
    return-void
.end method

.method public scheduleHideFragmentView(Ljava/lang/Object;Landroid/view/View;Ljava/util/ArrayList;)V
    .locals 2
    .param p1, "exitTransitionObj"    # Ljava/lang/Object;
    .param p2, "fragmentView"    # Landroid/view/View;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/lang/Object;",
            "Landroid/view/View;",
            "Ljava/util/ArrayList<",
            "Landroid/view/View;",
            ">;)V"
        }
    .end annotation

    .line 152
    .local p3, "exitingViews":Ljava/util/ArrayList;, "Ljava/util/ArrayList<Landroid/view/View;>;"
    move-object v0, p1

    check-cast v0, Landroidx/transition/Transition;

    .line 153
    .local v0, "exitTransition":Landroidx/transition/Transition;
    new-instance v1, Landroidx/transition/FragmentTransitionSupport$2;

    invoke-direct {v1, p0, p2, p3}, Landroidx/transition/FragmentTransitionSupport$2;-><init>(Landroidx/transition/FragmentTransitionSupport;Landroid/view/View;Ljava/util/ArrayList;)V

    invoke-virtual {v0, v1}, Landroidx/transition/Transition;->addListener(Landroidx/transition/Transition$TransitionListener;)Landroidx/transition/Transition;

    .line 188
    return-void
.end method

.method public scheduleRemoveTargets(Ljava/lang/Object;Ljava/lang/Object;Ljava/util/ArrayList;Ljava/lang/Object;Ljava/util/ArrayList;Ljava/lang/Object;Ljava/util/ArrayList;)V
    .locals 10
    .param p1, "overallTransitionObj"    # Ljava/lang/Object;
    .param p2, "enterTransition"    # Ljava/lang/Object;
    .param p4, "exitTransition"    # Ljava/lang/Object;
    .param p6, "sharedElementTransition"    # Ljava/lang/Object;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/lang/Object;",
            "Ljava/lang/Object;",
            "Ljava/util/ArrayList<",
            "Landroid/view/View;",
            ">;",
            "Ljava/lang/Object;",
            "Ljava/util/ArrayList<",
            "Landroid/view/View;",
            ">;",
            "Ljava/lang/Object;",
            "Ljava/util/ArrayList<",
            "Landroid/view/View;",
            ">;)V"
        }
    .end annotation

    .line 231
    .local p3, "enteringViews":Ljava/util/ArrayList;, "Ljava/util/ArrayList<Landroid/view/View;>;"
    .local p5, "exitingViews":Ljava/util/ArrayList;, "Ljava/util/ArrayList<Landroid/view/View;>;"
    .local p7, "sharedElementsIn":Ljava/util/ArrayList;, "Ljava/util/ArrayList<Landroid/view/View;>;"
    move-object v0, p1

    check-cast v0, Landroidx/transition/Transition;

    .line 232
    .local v0, "overallTransition":Landroidx/transition/Transition;
    new-instance v9, Landroidx/transition/FragmentTransitionSupport$3;

    move-object v1, v9

    move-object v2, p0

    move-object v3, p2

    move-object v4, p3

    move-object v5, p4

    move-object v6, p5

    move-object/from16 v7, p6

    move-object/from16 v8, p7

    invoke-direct/range {v1 .. v8}, Landroidx/transition/FragmentTransitionSupport$3;-><init>(Landroidx/transition/FragmentTransitionSupport;Ljava/lang/Object;Ljava/util/ArrayList;Ljava/lang/Object;Ljava/util/ArrayList;Ljava/lang/Object;Ljava/util/ArrayList;)V

    invoke-virtual {v0, v9}, Landroidx/transition/Transition;->addListener(Landroidx/transition/Transition$TransitionListener;)Landroidx/transition/Transition;

    .line 251
    return-void
.end method

.method public setEpicenter(Ljava/lang/Object;Landroid/graphics/Rect;)V
    .locals 2
    .param p1, "transitionObj"    # Ljava/lang/Object;
    .param p2, "epicenter"    # Landroid/graphics/Rect;

    .line 350
    if-eqz p1, :cond_0

    .line 351
    move-object v0, p1

    check-cast v0, Landroidx/transition/Transition;

    .line 352
    .local v0, "transition":Landroidx/transition/Transition;
    new-instance v1, Landroidx/transition/FragmentTransitionSupport$6;

    invoke-direct {v1, p0, p2}, Landroidx/transition/FragmentTransitionSupport$6;-><init>(Landroidx/transition/FragmentTransitionSupport;Landroid/graphics/Rect;)V

    invoke-virtual {v0, v1}, Landroidx/transition/Transition;->setEpicenterCallback(Landroidx/transition/Transition$EpicenterCallback;)V

    .line 362
    .end local v0    # "transition":Landroidx/transition/Transition;
    :cond_0
    return-void
.end method

.method public setEpicenter(Ljava/lang/Object;Landroid/view/View;)V
    .locals 3
    .param p1, "transitionObj"    # Ljava/lang/Object;
    .param p2, "view"    # Landroid/view/View;

    .line 88
    if-eqz p2, :cond_0

    .line 89
    move-object v0, p1

    check-cast v0, Landroidx/transition/Transition;

    .line 90
    .local v0, "transition":Landroidx/transition/Transition;
    new-instance v1, Landroid/graphics/Rect;

    invoke-direct {v1}, Landroid/graphics/Rect;-><init>()V

    .line 91
    .local v1, "epicenter":Landroid/graphics/Rect;
    invoke-virtual {p0, p2, v1}, Landroidx/transition/FragmentTransitionSupport;->getBoundsOnScreen(Landroid/view/View;Landroid/graphics/Rect;)V

    .line 93
    new-instance v2, Landroidx/transition/FragmentTransitionSupport$1;

    invoke-direct {v2, p0, v1}, Landroidx/transition/FragmentTransitionSupport$1;-><init>(Landroidx/transition/FragmentTransitionSupport;Landroid/graphics/Rect;)V

    invoke-virtual {v0, v2}, Landroidx/transition/Transition;->setEpicenterCallback(Landroidx/transition/Transition$EpicenterCallback;)V

    .line 100
    .end local v0    # "transition":Landroidx/transition/Transition;
    .end local v1    # "epicenter":Landroid/graphics/Rect;
    :cond_0
    return-void
.end method

.method public setListenerForTransitionEnd(Landroidx/fragment/app/Fragment;Ljava/lang/Object;Landroidx/core/os/CancellationSignal;Ljava/lang/Runnable;)V
    .locals 2
    .param p1, "outFragment"    # Landroidx/fragment/app/Fragment;
    .param p2, "transition"    # Ljava/lang/Object;
    .param p3, "signal"    # Landroidx/core/os/CancellationSignal;
    .param p4, "transitionCompleteRunnable"    # Ljava/lang/Runnable;

    .line 267
    move-object v0, p2

    check-cast v0, Landroidx/transition/Transition;

    .line 268
    .local v0, "realTransition":Landroidx/transition/Transition;
    new-instance v1, Landroidx/transition/FragmentTransitionSupport$4;

    invoke-direct {v1, p0, v0}, Landroidx/transition/FragmentTransitionSupport$4;-><init>(Landroidx/transition/FragmentTransitionSupport;Landroidx/transition/Transition;)V

    invoke-virtual {p3, v1}, Landroidx/core/os/CancellationSignal;->setOnCancelListener(Landroidx/core/os/CancellationSignal$OnCancelListener;)V

    .line 274
    new-instance v1, Landroidx/transition/FragmentTransitionSupport$5;

    invoke-direct {v1, p0, p4}, Landroidx/transition/FragmentTransitionSupport$5;-><init>(Landroidx/transition/FragmentTransitionSupport;Ljava/lang/Runnable;)V

    invoke-virtual {v0, v1}, Landroidx/transition/Transition;->addListener(Landroidx/transition/Transition$TransitionListener;)Landroidx/transition/Transition;

    .line 292
    return-void
.end method

.method public setSharedElementTargets(Ljava/lang/Object;Landroid/view/View;Ljava/util/ArrayList;)V
    .locals 5
    .param p1, "transitionObj"    # Ljava/lang/Object;
    .param p2, "nonExistentView"    # Landroid/view/View;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/lang/Object;",
            "Landroid/view/View;",
            "Ljava/util/ArrayList<",
            "Landroid/view/View;",
            ">;)V"
        }
    .end annotation

    .line 73
    .local p3, "sharedViews":Ljava/util/ArrayList;, "Ljava/util/ArrayList<Landroid/view/View;>;"
    move-object v0, p1

    check-cast v0, Landroidx/transition/TransitionSet;

    .line 74
    .local v0, "transition":Landroidx/transition/TransitionSet;
    invoke-virtual {v0}, Landroidx/transition/TransitionSet;->getTargets()Ljava/util/List;

    move-result-object v1

    .line 75
    .local v1, "views":Ljava/util/List;, "Ljava/util/List<Landroid/view/View;>;"
    invoke-interface {v1}, Ljava/util/List;->clear()V

    .line 76
    invoke-virtual {p3}, Ljava/util/ArrayList;->size()I

    move-result v2

    .line 77
    .local v2, "count":I
    const/4 v3, 0x0

    .local v3, "i":I
    :goto_0
    if-ge v3, v2, :cond_0

    .line 78
    invoke-virtual {p3, v3}, Ljava/util/ArrayList;->get(I)Ljava/lang/Object;

    move-result-object v4

    check-cast v4, Landroid/view/View;

    .line 79
    .local v4, "view":Landroid/view/View;
    invoke-static {v1, v4}, Landroidx/transition/FragmentTransitionSupport;->bfsAddViewChildren(Ljava/util/List;Landroid/view/View;)V

    .line 77
    .end local v4    # "view":Landroid/view/View;
    add-int/lit8 v3, v3, 0x1

    goto :goto_0

    .line 81
    .end local v3    # "i":I
    :cond_0
    invoke-interface {v1, p2}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    .line 82
    invoke-virtual {p3, p2}, Ljava/util/ArrayList;->add(Ljava/lang/Object;)Z

    .line 83
    invoke-virtual {p0, v0, p3}, Landroidx/transition/FragmentTransitionSupport;->addTargets(Ljava/lang/Object;Ljava/util/ArrayList;)V

    .line 84
    return-void
.end method

.method public swapSharedElementTargets(Ljava/lang/Object;Ljava/util/ArrayList;Ljava/util/ArrayList;)V
    .locals 2
    .param p1, "sharedElementTransitionObj"    # Ljava/lang/Object;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/lang/Object;",
            "Ljava/util/ArrayList<",
            "Landroid/view/View;",
            ">;",
            "Ljava/util/ArrayList<",
            "Landroid/view/View;",
            ">;)V"
        }
    .end annotation

    .line 297
    .local p2, "sharedElementsOut":Ljava/util/ArrayList;, "Ljava/util/ArrayList<Landroid/view/View;>;"
    .local p3, "sharedElementsIn":Ljava/util/ArrayList;, "Ljava/util/ArrayList<Landroid/view/View;>;"
    move-object v0, p1

    check-cast v0, Landroidx/transition/TransitionSet;

    .line 298
    .local v0, "sharedElementTransition":Landroidx/transition/TransitionSet;
    if-eqz v0, :cond_0

    .line 299
    invoke-virtual {v0}, Landroidx/transition/TransitionSet;->getTargets()Ljava/util/List;

    move-result-object v1

    invoke-interface {v1}, Ljava/util/List;->clear()V

    .line 300
    invoke-virtual {v0}, Landroidx/transition/TransitionSet;->getTargets()Ljava/util/List;

    move-result-object v1

    invoke-interface {v1, p3}, Ljava/util/List;->addAll(Ljava/util/Collection;)Z

    .line 301
    invoke-virtual {p0, v0, p2, p3}, Landroidx/transition/FragmentTransitionSupport;->replaceTargets(Ljava/lang/Object;Ljava/util/ArrayList;Ljava/util/ArrayList;)V

    .line 303
    :cond_0
    return-void
.end method

.method public wrapTransitionInSet(Ljava/lang/Object;)Ljava/lang/Object;
    .locals 2
    .param p1, "transition"    # Ljava/lang/Object;

    .line 62
    if-nez p1, :cond_0

    .line 63
    const/4 v0, 0x0

    return-object v0

    .line 65
    :cond_0
    new-instance v0, Landroidx/transition/TransitionSet;

    invoke-direct {v0}, Landroidx/transition/TransitionSet;-><init>()V

    .line 66
    .local v0, "transitionSet":Landroidx/transition/TransitionSet;
    move-object v1, p1

    check-cast v1, Landroidx/transition/Transition;

    invoke-virtual {v0, v1}, Landroidx/transition/TransitionSet;->addTransition(Landroidx/transition/Transition;)Landroidx/transition/TransitionSet;

    .line 67
    return-object v0
.end method
