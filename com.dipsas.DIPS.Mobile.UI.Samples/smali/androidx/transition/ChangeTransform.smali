.class public Landroidx/transition/ChangeTransform;
.super Landroidx/transition/Transition;
.source "ChangeTransform.java"


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Landroidx/transition/ChangeTransform$PathAnimatorMatrix;,
        Landroidx/transition/ChangeTransform$GhostListener;,
        Landroidx/transition/ChangeTransform$Transforms;
    }
.end annotation


# static fields
.field private static final NON_TRANSLATIONS_PROPERTY:Landroid/util/Property;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Landroid/util/Property<",
            "Landroidx/transition/ChangeTransform$PathAnimatorMatrix;",
            "[F>;"
        }
    .end annotation
.end field

.field private static final PROPNAME_INTERMEDIATE_MATRIX:Ljava/lang/String; = "android:changeTransform:intermediateMatrix"

.field private static final PROPNAME_INTERMEDIATE_PARENT_MATRIX:Ljava/lang/String; = "android:changeTransform:intermediateParentMatrix"

.field private static final PROPNAME_MATRIX:Ljava/lang/String; = "android:changeTransform:matrix"

.field private static final PROPNAME_PARENT:Ljava/lang/String; = "android:changeTransform:parent"

.field private static final PROPNAME_PARENT_MATRIX:Ljava/lang/String; = "android:changeTransform:parentMatrix"

.field private static final PROPNAME_TRANSFORMS:Ljava/lang/String; = "android:changeTransform:transforms"

.field private static final SUPPORTS_VIEW_REMOVAL_SUPPRESSION:Z

.field private static final TRANSLATIONS_PROPERTY:Landroid/util/Property;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Landroid/util/Property<",
            "Landroidx/transition/ChangeTransform$PathAnimatorMatrix;",
            "Landroid/graphics/PointF;",
            ">;"
        }
    .end annotation
.end field

.field private static final sTransitionProperties:[Ljava/lang/String;


# instance fields
.field private mReparent:Z

.field private mTempMatrix:Landroid/graphics/Matrix;

.field mUseOverlay:Z


# direct methods
.method static constructor <clinit>()V
    .locals 3

    .line 61
    const-string v0, "android:changeTransform:matrix"

    const-string v1, "android:changeTransform:transforms"

    const-string v2, "android:changeTransform:parentMatrix"

    filled-new-array {v0, v1, v2}, [Ljava/lang/String;

    move-result-object v0

    sput-object v0, Landroidx/transition/ChangeTransform;->sTransitionProperties:[Ljava/lang/String;

    .line 70
    new-instance v0, Landroidx/transition/ChangeTransform$1;

    const-class v1, [F

    const-string v2, "nonTranslations"

    invoke-direct {v0, v1, v2}, Landroidx/transition/ChangeTransform$1;-><init>(Ljava/lang/Class;Ljava/lang/String;)V

    sput-object v0, Landroidx/transition/ChangeTransform;->NON_TRANSLATIONS_PROPERTY:Landroid/util/Property;

    .line 86
    new-instance v0, Landroidx/transition/ChangeTransform$2;

    const-class v1, Landroid/graphics/PointF;

    const-string v2, "translations"

    invoke-direct {v0, v1, v2}, Landroidx/transition/ChangeTransform$2;-><init>(Ljava/lang/Class;Ljava/lang/String;)V

    sput-object v0, Landroidx/transition/ChangeTransform;->TRANSLATIONS_PROPERTY:Landroid/util/Property;

    .line 102
    sget v0, Landroid/os/Build$VERSION;->SDK_INT:I

    const/16 v1, 0x15

    if-lt v0, v1, :cond_0

    const/4 v0, 0x1

    goto :goto_0

    :cond_0
    const/4 v0, 0x0

    :goto_0
    sput-boolean v0, Landroidx/transition/ChangeTransform;->SUPPORTS_VIEW_REMOVAL_SUPPRESSION:Z

    return-void
.end method

.method public constructor <init>()V
    .locals 1

    .line 109
    invoke-direct {p0}, Landroidx/transition/Transition;-><init>()V

    .line 104
    const/4 v0, 0x1

    iput-boolean v0, p0, Landroidx/transition/ChangeTransform;->mUseOverlay:Z

    .line 106
    iput-boolean v0, p0, Landroidx/transition/ChangeTransform;->mReparent:Z

    .line 107
    new-instance v0, Landroid/graphics/Matrix;

    invoke-direct {v0}, Landroid/graphics/Matrix;-><init>()V

    iput-object v0, p0, Landroidx/transition/ChangeTransform;->mTempMatrix:Landroid/graphics/Matrix;

    .line 110
    return-void
.end method

.method public constructor <init>(Landroid/content/Context;Landroid/util/AttributeSet;)V
    .locals 5
    .param p1, "context"    # Landroid/content/Context;
    .param p2, "attrs"    # Landroid/util/AttributeSet;

    .line 115
    invoke-direct {p0, p1, p2}, Landroidx/transition/Transition;-><init>(Landroid/content/Context;Landroid/util/AttributeSet;)V

    .line 104
    const/4 v0, 0x1

    iput-boolean v0, p0, Landroidx/transition/ChangeTransform;->mUseOverlay:Z

    .line 106
    iput-boolean v0, p0, Landroidx/transition/ChangeTransform;->mReparent:Z

    .line 107
    new-instance v1, Landroid/graphics/Matrix;

    invoke-direct {v1}, Landroid/graphics/Matrix;-><init>()V

    iput-object v1, p0, Landroidx/transition/ChangeTransform;->mTempMatrix:Landroid/graphics/Matrix;

    .line 116
    sget-object v1, Landroidx/transition/Styleable;->CHANGE_TRANSFORM:[I

    invoke-virtual {p1, p2, v1}, Landroid/content/Context;->obtainStyledAttributes(Landroid/util/AttributeSet;[I)Landroid/content/res/TypedArray;

    move-result-object v1

    .line 117
    .local v1, "a":Landroid/content/res/TypedArray;
    move-object v2, p2

    check-cast v2, Lorg/xmlpull/v1/XmlPullParser;

    const-string v3, "reparentWithOverlay"

    invoke-static {v1, v2, v3, v0, v0}, Landroidx/core/content/res/TypedArrayUtils;->getNamedBoolean(Landroid/content/res/TypedArray;Lorg/xmlpull/v1/XmlPullParser;Ljava/lang/String;IZ)Z

    move-result v2

    iput-boolean v2, p0, Landroidx/transition/ChangeTransform;->mUseOverlay:Z

    .line 119
    move-object v2, p2

    check-cast v2, Lorg/xmlpull/v1/XmlPullParser;

    const-string v3, "reparent"

    const/4 v4, 0x0

    invoke-static {v1, v2, v3, v4, v0}, Landroidx/core/content/res/TypedArrayUtils;->getNamedBoolean(Landroid/content/res/TypedArray;Lorg/xmlpull/v1/XmlPullParser;Ljava/lang/String;IZ)Z

    move-result v0

    iput-boolean v0, p0, Landroidx/transition/ChangeTransform;->mReparent:Z

    .line 121
    invoke-virtual {v1}, Landroid/content/res/TypedArray;->recycle()V

    .line 122
    return-void
.end method

.method private captureValues(Landroidx/transition/TransitionValues;)V
    .locals 8
    .param p1, "transitionValues"    # Landroidx/transition/TransitionValues;

    .line 199
    iget-object v0, p1, Landroidx/transition/TransitionValues;->view:Landroid/view/View;

    .line 200
    .local v0, "view":Landroid/view/View;
    invoke-virtual {v0}, Landroid/view/View;->getVisibility()I

    move-result v1

    const/16 v2, 0x8

    if-ne v1, v2, :cond_0

    .line 201
    return-void

    .line 203
    :cond_0
    iget-object v1, p1, Landroidx/transition/TransitionValues;->values:Ljava/util/Map;

    invoke-virtual {v0}, Landroid/view/View;->getParent()Landroid/view/ViewParent;

    move-result-object v2

    const-string v3, "android:changeTransform:parent"

    invoke-interface {v1, v3, v2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 204
    new-instance v1, Landroidx/transition/ChangeTransform$Transforms;

    invoke-direct {v1, v0}, Landroidx/transition/ChangeTransform$Transforms;-><init>(Landroid/view/View;)V

    .line 205
    .local v1, "transforms":Landroidx/transition/ChangeTransform$Transforms;
    iget-object v2, p1, Landroidx/transition/TransitionValues;->values:Ljava/util/Map;

    const-string v3, "android:changeTransform:transforms"

    invoke-interface {v2, v3, v1}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 206
    invoke-virtual {v0}, Landroid/view/View;->getMatrix()Landroid/graphics/Matrix;

    move-result-object v2

    .line 207
    .local v2, "matrix":Landroid/graphics/Matrix;
    if-eqz v2, :cond_2

    invoke-virtual {v2}, Landroid/graphics/Matrix;->isIdentity()Z

    move-result v3

    if-eqz v3, :cond_1

    goto :goto_0

    .line 210
    :cond_1
    new-instance v3, Landroid/graphics/Matrix;

    invoke-direct {v3, v2}, Landroid/graphics/Matrix;-><init>(Landroid/graphics/Matrix;)V

    move-object v2, v3

    goto :goto_1

    .line 208
    :cond_2
    :goto_0
    const/4 v2, 0x0

    .line 212
    :goto_1
    iget-object v3, p1, Landroidx/transition/TransitionValues;->values:Ljava/util/Map;

    const-string v4, "android:changeTransform:matrix"

    invoke-interface {v3, v4, v2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 213
    iget-boolean v3, p0, Landroidx/transition/ChangeTransform;->mReparent:Z

    if-eqz v3, :cond_3

    .line 214
    new-instance v3, Landroid/graphics/Matrix;

    invoke-direct {v3}, Landroid/graphics/Matrix;-><init>()V

    .line 215
    .local v3, "parentMatrix":Landroid/graphics/Matrix;
    invoke-virtual {v0}, Landroid/view/View;->getParent()Landroid/view/ViewParent;

    move-result-object v4

    check-cast v4, Landroid/view/ViewGroup;

    .line 216
    .local v4, "parent":Landroid/view/ViewGroup;
    invoke-static {v4, v3}, Landroidx/transition/ViewUtils;->transformMatrixToGlobal(Landroid/view/View;Landroid/graphics/Matrix;)V

    .line 217
    invoke-virtual {v4}, Landroid/view/ViewGroup;->getScrollX()I

    move-result v5

    neg-int v5, v5

    int-to-float v5, v5

    invoke-virtual {v4}, Landroid/view/ViewGroup;->getScrollY()I

    move-result v6

    neg-int v6, v6

    int-to-float v6, v6

    invoke-virtual {v3, v5, v6}, Landroid/graphics/Matrix;->preTranslate(FF)Z

    .line 218
    iget-object v5, p1, Landroidx/transition/TransitionValues;->values:Ljava/util/Map;

    const-string v6, "android:changeTransform:parentMatrix"

    invoke-interface {v5, v6, v3}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 219
    iget-object v5, p1, Landroidx/transition/TransitionValues;->values:Ljava/util/Map;

    sget v6, Landroidx/transition/R$id;->transition_transform:I

    .line 220
    invoke-virtual {v0, v6}, Landroid/view/View;->getTag(I)Ljava/lang/Object;

    move-result-object v6

    .line 219
    const-string v7, "android:changeTransform:intermediateMatrix"

    invoke-interface {v5, v7, v6}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 221
    iget-object v5, p1, Landroidx/transition/TransitionValues;->values:Ljava/util/Map;

    sget v6, Landroidx/transition/R$id;->parent_matrix:I

    .line 222
    invoke-virtual {v0, v6}, Landroid/view/View;->getTag(I)Ljava/lang/Object;

    move-result-object v6

    .line 221
    const-string v7, "android:changeTransform:intermediateParentMatrix"

    invoke-interface {v5, v7, v6}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 224
    .end local v3    # "parentMatrix":Landroid/graphics/Matrix;
    .end local v4    # "parent":Landroid/view/ViewGroup;
    :cond_3
    return-void
.end method

.method private createGhostView(Landroid/view/ViewGroup;Landroidx/transition/TransitionValues;Landroidx/transition/TransitionValues;)V
    .locals 8
    .param p1, "sceneRoot"    # Landroid/view/ViewGroup;
    .param p2, "startValues"    # Landroidx/transition/TransitionValues;
    .param p3, "endValues"    # Landroidx/transition/TransitionValues;

    .line 392
    iget-object v0, p3, Landroidx/transition/TransitionValues;->view:Landroid/view/View;

    .line 394
    .local v0, "view":Landroid/view/View;
    iget-object v1, p3, Landroidx/transition/TransitionValues;->values:Ljava/util/Map;

    const-string v2, "android:changeTransform:parentMatrix"

    invoke-interface {v1, v2}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Landroid/graphics/Matrix;

    .line 395
    .local v1, "endMatrix":Landroid/graphics/Matrix;
    new-instance v2, Landroid/graphics/Matrix;

    invoke-direct {v2, v1}, Landroid/graphics/Matrix;-><init>(Landroid/graphics/Matrix;)V

    .line 396
    .local v2, "localEndMatrix":Landroid/graphics/Matrix;
    invoke-static {p1, v2}, Landroidx/transition/ViewUtils;->transformMatrixToLocal(Landroid/view/View;Landroid/graphics/Matrix;)V

    .line 398
    invoke-static {v0, p1, v2}, Landroidx/transition/GhostViewUtils;->addGhost(Landroid/view/View;Landroid/view/ViewGroup;Landroid/graphics/Matrix;)Landroidx/transition/GhostView;

    move-result-object v3

    .line 399
    .local v3, "ghostView":Landroidx/transition/GhostView;
    if-nez v3, :cond_0

    .line 400
    return-void

    .line 403
    :cond_0
    iget-object v4, p2, Landroidx/transition/TransitionValues;->values:Ljava/util/Map;

    const-string v5, "android:changeTransform:parent"

    invoke-interface {v4, v5}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v4

    check-cast v4, Landroid/view/ViewGroup;

    iget-object v5, p2, Landroidx/transition/TransitionValues;->view:Landroid/view/View;

    invoke-interface {v3, v4, v5}, Landroidx/transition/GhostView;->reserveEndViewTransition(Landroid/view/ViewGroup;Landroid/view/View;)V

    .line 406
    move-object v4, p0

    .line 407
    .local v4, "outerTransition":Landroidx/transition/Transition;
    :goto_0
    iget-object v5, v4, Landroidx/transition/Transition;->mParent:Landroidx/transition/TransitionSet;

    if-eqz v5, :cond_1

    .line 408
    iget-object v4, v4, Landroidx/transition/Transition;->mParent:Landroidx/transition/TransitionSet;

    goto :goto_0

    .line 411
    :cond_1
    new-instance v5, Landroidx/transition/ChangeTransform$GhostListener;

    invoke-direct {v5, v0, v3}, Landroidx/transition/ChangeTransform$GhostListener;-><init>(Landroid/view/View;Landroidx/transition/GhostView;)V

    .line 412
    .local v5, "listener":Landroidx/transition/ChangeTransform$GhostListener;
    invoke-virtual {v4, v5}, Landroidx/transition/Transition;->addListener(Landroidx/transition/Transition$TransitionListener;)Landroidx/transition/Transition;

    .line 416
    sget-boolean v6, Landroidx/transition/ChangeTransform;->SUPPORTS_VIEW_REMOVAL_SUPPRESSION:Z

    if-eqz v6, :cond_3

    .line 417
    iget-object v6, p2, Landroidx/transition/TransitionValues;->view:Landroid/view/View;

    iget-object v7, p3, Landroidx/transition/TransitionValues;->view:Landroid/view/View;

    if-eq v6, v7, :cond_2

    .line 418
    iget-object v6, p2, Landroidx/transition/TransitionValues;->view:Landroid/view/View;

    const/4 v7, 0x0

    invoke-static {v6, v7}, Landroidx/transition/ViewUtils;->setTransitionAlpha(Landroid/view/View;F)V

    .line 420
    :cond_2
    const/high16 v6, 0x3f800000    # 1.0f

    invoke-static {v0, v6}, Landroidx/transition/ViewUtils;->setTransitionAlpha(Landroid/view/View;F)V

    .line 422
    :cond_3
    return-void
.end method

.method private createTransformAnimator(Landroidx/transition/TransitionValues;Landroidx/transition/TransitionValues;Z)Landroid/animation/ObjectAnimator;
    .locals 20
    .param p1, "startValues"    # Landroidx/transition/TransitionValues;
    .param p2, "endValues"    # Landroidx/transition/TransitionValues;
    .param p3, "handleParentChange"    # Z

    .line 290
    move-object/from16 v0, p2

    move-object/from16 v1, p1

    iget-object v2, v1, Landroidx/transition/TransitionValues;->values:Ljava/util/Map;

    const-string v3, "android:changeTransform:matrix"

    invoke-interface {v2, v3}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Landroid/graphics/Matrix;

    .line 291
    .local v2, "startMatrix":Landroid/graphics/Matrix;
    iget-object v4, v0, Landroidx/transition/TransitionValues;->values:Ljava/util/Map;

    invoke-interface {v4, v3}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v3

    check-cast v3, Landroid/graphics/Matrix;

    .line 293
    .local v3, "endMatrix":Landroid/graphics/Matrix;
    if-nez v2, :cond_0

    .line 294
    sget-object v2, Landroidx/transition/MatrixUtils;->IDENTITY_MATRIX:Landroid/graphics/Matrix;

    .line 297
    :cond_0
    if-nez v3, :cond_1

    .line 298
    sget-object v3, Landroidx/transition/MatrixUtils;->IDENTITY_MATRIX:Landroid/graphics/Matrix;

    .line 301
    :cond_1
    invoke-virtual {v2, v3}, Landroid/graphics/Matrix;->equals(Ljava/lang/Object;)Z

    move-result v4

    if-eqz v4, :cond_2

    .line 302
    const/4 v4, 0x0

    return-object v4

    .line 305
    :cond_2
    iget-object v4, v0, Landroidx/transition/TransitionValues;->values:Ljava/util/Map;

    const-string v5, "android:changeTransform:transforms"

    invoke-interface {v4, v5}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v4

    move-object v11, v4

    check-cast v11, Landroidx/transition/ChangeTransform$Transforms;

    .line 308
    .local v11, "transforms":Landroidx/transition/ChangeTransform$Transforms;
    iget-object v12, v0, Landroidx/transition/TransitionValues;->view:Landroid/view/View;

    .line 309
    .local v12, "view":Landroid/view/View;
    invoke-static {v12}, Landroidx/transition/ChangeTransform;->setIdentityTransforms(Landroid/view/View;)V

    .line 311
    const/16 v4, 0x9

    new-array v13, v4, [F

    .line 312
    .local v13, "startMatrixValues":[F
    invoke-virtual {v2, v13}, Landroid/graphics/Matrix;->getValues([F)V

    .line 313
    new-array v14, v4, [F

    .line 314
    .local v14, "endMatrixValues":[F
    invoke-virtual {v3, v14}, Landroid/graphics/Matrix;->getValues([F)V

    .line 315
    new-instance v5, Landroidx/transition/ChangeTransform$PathAnimatorMatrix;

    invoke-direct {v5, v12, v13}, Landroidx/transition/ChangeTransform$PathAnimatorMatrix;-><init>(Landroid/view/View;[F)V

    move-object v15, v5

    .line 318
    .local v15, "pathAnimatorMatrix":Landroidx/transition/ChangeTransform$PathAnimatorMatrix;
    sget-object v5, Landroidx/transition/ChangeTransform;->NON_TRANSLATIONS_PROPERTY:Landroid/util/Property;

    new-instance v6, Landroidx/transition/FloatArrayEvaluator;

    new-array v4, v4, [F

    invoke-direct {v6, v4}, Landroidx/transition/FloatArrayEvaluator;-><init>([F)V

    const/4 v4, 0x2

    new-array v7, v4, [[F

    const/4 v8, 0x0

    aput-object v13, v7, v8

    const/4 v9, 0x1

    aput-object v14, v7, v9

    invoke-static {v5, v6, v7}, Landroid/animation/PropertyValuesHolder;->ofObject(Landroid/util/Property;Landroid/animation/TypeEvaluator;[Ljava/lang/Object;)Landroid/animation/PropertyValuesHolder;

    move-result-object v16

    .line 321
    .local v16, "valuesProperty":Landroid/animation/PropertyValuesHolder;
    invoke-virtual/range {p0 .. p0}, Landroidx/transition/ChangeTransform;->getPathMotion()Landroidx/transition/PathMotion;

    move-result-object v5

    aget v6, v13, v4

    const/4 v7, 0x5

    aget v10, v13, v7

    aget v9, v14, v4

    aget v7, v14, v7

    invoke-virtual {v5, v6, v10, v9, v7}, Landroidx/transition/PathMotion;->getPath(FFFF)Landroid/graphics/Path;

    move-result-object v10

    .line 324
    .local v10, "path":Landroid/graphics/Path;
    sget-object v5, Landroidx/transition/ChangeTransform;->TRANSLATIONS_PROPERTY:Landroid/util/Property;

    invoke-static {v5, v10}, Landroidx/transition/PropertyValuesHolderUtils;->ofPointF(Landroid/util/Property;Landroid/graphics/Path;)Landroid/animation/PropertyValuesHolder;

    move-result-object v18

    .line 326
    .local v18, "translationProperty":Landroid/animation/PropertyValuesHolder;
    new-array v4, v4, [Landroid/animation/PropertyValuesHolder;

    aput-object v16, v4, v8

    const/4 v5, 0x1

    aput-object v18, v4, v5

    invoke-static {v15, v4}, Landroid/animation/ObjectAnimator;->ofPropertyValuesHolder(Ljava/lang/Object;[Landroid/animation/PropertyValuesHolder;)Landroid/animation/ObjectAnimator;

    move-result-object v9

    .line 329
    .local v9, "animator":Landroid/animation/ObjectAnimator;
    move-object v7, v3

    .line 331
    .local v7, "finalEndMatrix":Landroid/graphics/Matrix;
    new-instance v17, Landroidx/transition/ChangeTransform$3;

    move-object/from16 v4, v17

    move-object/from16 v5, p0

    move/from16 v6, p3

    move-object v8, v12

    move-object v0, v9

    .end local v9    # "animator":Landroid/animation/ObjectAnimator;
    .local v0, "animator":Landroid/animation/ObjectAnimator;
    move-object v9, v11

    move-object/from16 v19, v10

    .end local v10    # "path":Landroid/graphics/Path;
    .local v19, "path":Landroid/graphics/Path;
    move-object v10, v15

    invoke-direct/range {v4 .. v10}, Landroidx/transition/ChangeTransform$3;-><init>(Landroidx/transition/ChangeTransform;ZLandroid/graphics/Matrix;Landroid/view/View;Landroidx/transition/ChangeTransform$Transforms;Landroidx/transition/ChangeTransform$PathAnimatorMatrix;)V

    .line 372
    .local v4, "listener":Landroid/animation/AnimatorListenerAdapter;
    invoke-virtual {v0, v4}, Landroid/animation/ObjectAnimator;->addListener(Landroid/animation/Animator$AnimatorListener;)V

    .line 373
    invoke-static {v0, v4}, Landroidx/transition/AnimatorUtils;->addPauseListener(Landroid/animation/Animator;Landroid/animation/AnimatorListenerAdapter;)V

    .line 374
    return-object v0
.end method

.method private parentsMatch(Landroid/view/ViewGroup;Landroid/view/ViewGroup;)Z
    .locals 5
    .param p1, "startParent"    # Landroid/view/ViewGroup;
    .param p2, "endParent"    # Landroid/view/ViewGroup;

    .line 378
    const/4 v0, 0x0

    .line 379
    .local v0, "parentsMatch":Z
    invoke-virtual {p0, p1}, Landroidx/transition/ChangeTransform;->isValidTarget(Landroid/view/View;)Z

    move-result v1

    const/4 v2, 0x0

    const/4 v3, 0x1

    if-eqz v1, :cond_2

    invoke-virtual {p0, p2}, Landroidx/transition/ChangeTransform;->isValidTarget(Landroid/view/View;)Z

    move-result v1

    if-nez v1, :cond_0

    goto :goto_0

    .line 382
    :cond_0
    invoke-virtual {p0, p1, v3}, Landroidx/transition/ChangeTransform;->getMatchedTransitionValues(Landroid/view/View;Z)Landroidx/transition/TransitionValues;

    move-result-object v1

    .line 383
    .local v1, "endValues":Landroidx/transition/TransitionValues;
    if-eqz v1, :cond_4

    .line 384
    iget-object v4, v1, Landroidx/transition/TransitionValues;->view:Landroid/view/View;

    if-ne p2, v4, :cond_1

    const/4 v2, 0x1

    :cond_1
    move v0, v2

    goto :goto_1

    .line 380
    .end local v1    # "endValues":Landroidx/transition/TransitionValues;
    :cond_2
    :goto_0
    if-ne p1, p2, :cond_3

    const/4 v2, 0x1

    :cond_3
    move v0, v2

    .line 387
    :cond_4
    :goto_1
    return v0
.end method

.method static setIdentityTransforms(Landroid/view/View;)V
    .locals 9
    .param p0, "view"    # Landroid/view/View;

    .line 445
    const/4 v1, 0x0

    const/4 v2, 0x0

    const/4 v3, 0x0

    const/high16 v4, 0x3f800000    # 1.0f

    const/high16 v5, 0x3f800000    # 1.0f

    const/4 v6, 0x0

    const/4 v7, 0x0

    const/4 v8, 0x0

    move-object v0, p0

    invoke-static/range {v0 .. v8}, Landroidx/transition/ChangeTransform;->setTransforms(Landroid/view/View;FFFFFFFF)V

    .line 446
    return-void
.end method

.method private setMatricesForParent(Landroidx/transition/TransitionValues;Landroidx/transition/TransitionValues;)V
    .locals 6
    .param p1, "startValues"    # Landroidx/transition/TransitionValues;
    .param p2, "endValues"    # Landroidx/transition/TransitionValues;

    .line 425
    iget-object v0, p2, Landroidx/transition/TransitionValues;->values:Ljava/util/Map;

    const-string v1, "android:changeTransform:parentMatrix"

    invoke-interface {v0, v1}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Landroid/graphics/Matrix;

    .line 426
    .local v0, "endParentMatrix":Landroid/graphics/Matrix;
    iget-object v2, p2, Landroidx/transition/TransitionValues;->view:Landroid/view/View;

    sget v3, Landroidx/transition/R$id;->parent_matrix:I

    invoke-virtual {v2, v3, v0}, Landroid/view/View;->setTag(ILjava/lang/Object;)V

    .line 428
    iget-object v2, p0, Landroidx/transition/ChangeTransform;->mTempMatrix:Landroid/graphics/Matrix;

    .line 429
    .local v2, "toLocal":Landroid/graphics/Matrix;
    invoke-virtual {v2}, Landroid/graphics/Matrix;->reset()V

    .line 430
    invoke-virtual {v0, v2}, Landroid/graphics/Matrix;->invert(Landroid/graphics/Matrix;)Z

    .line 432
    iget-object v3, p1, Landroidx/transition/TransitionValues;->values:Ljava/util/Map;

    const-string v4, "android:changeTransform:matrix"

    invoke-interface {v3, v4}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v3

    check-cast v3, Landroid/graphics/Matrix;

    .line 433
    .local v3, "startLocal":Landroid/graphics/Matrix;
    if-nez v3, :cond_0

    .line 434
    new-instance v5, Landroid/graphics/Matrix;

    invoke-direct {v5}, Landroid/graphics/Matrix;-><init>()V

    move-object v3, v5

    .line 435
    iget-object v5, p1, Landroidx/transition/TransitionValues;->values:Ljava/util/Map;

    invoke-interface {v5, v4, v3}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 438
    :cond_0
    iget-object v4, p1, Landroidx/transition/TransitionValues;->values:Ljava/util/Map;

    invoke-interface {v4, v1}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Landroid/graphics/Matrix;

    .line 439
    .local v1, "startParentMatrix":Landroid/graphics/Matrix;
    invoke-virtual {v3, v1}, Landroid/graphics/Matrix;->postConcat(Landroid/graphics/Matrix;)Z

    .line 440
    invoke-virtual {v3, v2}, Landroid/graphics/Matrix;->postConcat(Landroid/graphics/Matrix;)Z

    .line 441
    return-void
.end method

.method static setTransforms(Landroid/view/View;FFFFFFFF)V
    .locals 0
    .param p0, "view"    # Landroid/view/View;
    .param p1, "translationX"    # F
    .param p2, "translationY"    # F
    .param p3, "translationZ"    # F
    .param p4, "scaleX"    # F
    .param p5, "scaleY"    # F
    .param p6, "rotationX"    # F
    .param p7, "rotationY"    # F
    .param p8, "rotationZ"    # F

    .line 452
    invoke-virtual {p0, p1}, Landroid/view/View;->setTranslationX(F)V

    .line 453
    invoke-virtual {p0, p2}, Landroid/view/View;->setTranslationY(F)V

    .line 454
    invoke-static {p0, p3}, Landroidx/core/view/ViewCompat;->setTranslationZ(Landroid/view/View;F)V

    .line 455
    invoke-virtual {p0, p4}, Landroid/view/View;->setScaleX(F)V

    .line 456
    invoke-virtual {p0, p5}, Landroid/view/View;->setScaleY(F)V

    .line 457
    invoke-virtual {p0, p6}, Landroid/view/View;->setRotationX(F)V

    .line 458
    invoke-virtual {p0, p7}, Landroid/view/View;->setRotationY(F)V

    .line 459
    invoke-virtual {p0, p8}, Landroid/view/View;->setRotation(F)V

    .line 460
    return-void
.end method


# virtual methods
.method public captureEndValues(Landroidx/transition/TransitionValues;)V
    .locals 0
    .param p1, "transitionValues"    # Landroidx/transition/TransitionValues;

    .line 240
    invoke-direct {p0, p1}, Landroidx/transition/ChangeTransform;->captureValues(Landroidx/transition/TransitionValues;)V

    .line 241
    return-void
.end method

.method public captureStartValues(Landroidx/transition/TransitionValues;)V
    .locals 2
    .param p1, "transitionValues"    # Landroidx/transition/TransitionValues;

    .line 228
    invoke-direct {p0, p1}, Landroidx/transition/ChangeTransform;->captureValues(Landroidx/transition/TransitionValues;)V

    .line 229
    sget-boolean v0, Landroidx/transition/ChangeTransform;->SUPPORTS_VIEW_REMOVAL_SUPPRESSION:Z

    if-nez v0, :cond_0

    .line 233
    iget-object v0, p1, Landroidx/transition/TransitionValues;->view:Landroid/view/View;

    invoke-virtual {v0}, Landroid/view/View;->getParent()Landroid/view/ViewParent;

    move-result-object v0

    check-cast v0, Landroid/view/ViewGroup;

    iget-object v1, p1, Landroidx/transition/TransitionValues;->view:Landroid/view/View;

    invoke-virtual {v0, v1}, Landroid/view/ViewGroup;->startViewTransition(Landroid/view/View;)V

    .line 236
    :cond_0
    return-void
.end method

.method public createAnimator(Landroid/view/ViewGroup;Landroidx/transition/TransitionValues;Landroidx/transition/TransitionValues;)Landroid/animation/Animator;
    .locals 7
    .param p1, "sceneRoot"    # Landroid/view/ViewGroup;
    .param p2, "startValues"    # Landroidx/transition/TransitionValues;
    .param p3, "endValues"    # Landroidx/transition/TransitionValues;

    .line 248
    if-eqz p2, :cond_7

    if-eqz p3, :cond_7

    iget-object v0, p2, Landroidx/transition/TransitionValues;->values:Ljava/util/Map;

    .line 249
    const-string v1, "android:changeTransform:parent"

    invoke-interface {v0, v1}, Ljava/util/Map;->containsKey(Ljava/lang/Object;)Z

    move-result v0

    if-eqz v0, :cond_7

    iget-object v0, p3, Landroidx/transition/TransitionValues;->values:Ljava/util/Map;

    .line 250
    invoke-interface {v0, v1}, Ljava/util/Map;->containsKey(Ljava/lang/Object;)Z

    move-result v0

    if-nez v0, :cond_0

    goto :goto_2

    .line 254
    :cond_0
    iget-object v0, p2, Landroidx/transition/TransitionValues;->values:Ljava/util/Map;

    invoke-interface {v0, v1}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Landroid/view/ViewGroup;

    .line 255
    .local v0, "startParent":Landroid/view/ViewGroup;
    iget-object v2, p3, Landroidx/transition/TransitionValues;->values:Ljava/util/Map;

    invoke-interface {v2, v1}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Landroid/view/ViewGroup;

    .line 256
    .local v1, "endParent":Landroid/view/ViewGroup;
    iget-boolean v2, p0, Landroidx/transition/ChangeTransform;->mReparent:Z

    if-eqz v2, :cond_1

    invoke-direct {p0, v0, v1}, Landroidx/transition/ChangeTransform;->parentsMatch(Landroid/view/ViewGroup;Landroid/view/ViewGroup;)Z

    move-result v2

    if-nez v2, :cond_1

    const/4 v2, 0x1

    goto :goto_0

    :cond_1
    const/4 v2, 0x0

    .line 258
    .local v2, "handleParentChange":Z
    :goto_0
    iget-object v3, p2, Landroidx/transition/TransitionValues;->values:Ljava/util/Map;

    const-string v4, "android:changeTransform:intermediateMatrix"

    invoke-interface {v3, v4}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v3

    check-cast v3, Landroid/graphics/Matrix;

    .line 259
    .local v3, "startMatrix":Landroid/graphics/Matrix;
    if-eqz v3, :cond_2

    .line 260
    iget-object v4, p2, Landroidx/transition/TransitionValues;->values:Ljava/util/Map;

    const-string v5, "android:changeTransform:matrix"

    invoke-interface {v4, v5, v3}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 263
    :cond_2
    iget-object v4, p2, Landroidx/transition/TransitionValues;->values:Ljava/util/Map;

    .line 264
    const-string v5, "android:changeTransform:intermediateParentMatrix"

    invoke-interface {v4, v5}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v4

    check-cast v4, Landroid/graphics/Matrix;

    .line 265
    .local v4, "startParentMatrix":Landroid/graphics/Matrix;
    if-eqz v4, :cond_3

    .line 266
    iget-object v5, p2, Landroidx/transition/TransitionValues;->values:Ljava/util/Map;

    const-string v6, "android:changeTransform:parentMatrix"

    invoke-interface {v5, v6, v4}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 270
    :cond_3
    if-eqz v2, :cond_4

    .line 271
    invoke-direct {p0, p2, p3}, Landroidx/transition/ChangeTransform;->setMatricesForParent(Landroidx/transition/TransitionValues;Landroidx/transition/TransitionValues;)V

    .line 275
    :cond_4
    invoke-direct {p0, p2, p3, v2}, Landroidx/transition/ChangeTransform;->createTransformAnimator(Landroidx/transition/TransitionValues;Landroidx/transition/TransitionValues;Z)Landroid/animation/ObjectAnimator;

    move-result-object v5

    .line 278
    .local v5, "transformAnimator":Landroid/animation/ObjectAnimator;
    if-eqz v2, :cond_5

    if-eqz v5, :cond_5

    iget-boolean v6, p0, Landroidx/transition/ChangeTransform;->mUseOverlay:Z

    if-eqz v6, :cond_5

    .line 279
    invoke-direct {p0, p1, p2, p3}, Landroidx/transition/ChangeTransform;->createGhostView(Landroid/view/ViewGroup;Landroidx/transition/TransitionValues;Landroidx/transition/TransitionValues;)V

    goto :goto_1

    .line 280
    :cond_5
    sget-boolean v6, Landroidx/transition/ChangeTransform;->SUPPORTS_VIEW_REMOVAL_SUPPRESSION:Z

    if-nez v6, :cond_6

    .line 282
    iget-object v6, p2, Landroidx/transition/TransitionValues;->view:Landroid/view/View;

    invoke-virtual {v0, v6}, Landroid/view/ViewGroup;->endViewTransition(Landroid/view/View;)V

    .line 285
    :cond_6
    :goto_1
    return-object v5

    .line 251
    .end local v0    # "startParent":Landroid/view/ViewGroup;
    .end local v1    # "endParent":Landroid/view/ViewGroup;
    .end local v2    # "handleParentChange":Z
    .end local v3    # "startMatrix":Landroid/graphics/Matrix;
    .end local v4    # "startParentMatrix":Landroid/graphics/Matrix;
    .end local v5    # "transformAnimator":Landroid/animation/ObjectAnimator;
    :cond_7
    :goto_2
    const/4 v0, 0x0

    return-object v0
.end method

.method public getReparent()Z
    .locals 1

    .line 176
    iget-boolean v0, p0, Landroidx/transition/ChangeTransform;->mReparent:Z

    return v0
.end method

.method public getReparentWithOverlay()Z
    .locals 1

    .line 142
    iget-boolean v0, p0, Landroidx/transition/ChangeTransform;->mUseOverlay:Z

    return v0
.end method

.method public getTransitionProperties()[Ljava/lang/String;
    .locals 1

    .line 195
    sget-object v0, Landroidx/transition/ChangeTransform;->sTransitionProperties:[Ljava/lang/String;

    return-object v0
.end method

.method public setReparent(Z)V
    .locals 0
    .param p1, "reparent"    # Z

    .line 189
    iput-boolean p1, p0, Landroidx/transition/ChangeTransform;->mReparent:Z

    .line 190
    return-void
.end method

.method public setReparentWithOverlay(Z)V
    .locals 0
    .param p1, "reparentWithOverlay"    # Z

    .line 164
    iput-boolean p1, p0, Landroidx/transition/ChangeTransform;->mUseOverlay:Z

    .line 165
    return-void
.end method
