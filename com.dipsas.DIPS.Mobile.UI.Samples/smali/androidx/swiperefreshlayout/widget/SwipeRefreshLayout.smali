.class public Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;
.super Landroid/view/ViewGroup;
.source "SwipeRefreshLayout.java"

# interfaces
.implements Landroidx/core/view/NestedScrollingParent3;
.implements Landroidx/core/view/NestedScrollingParent2;
.implements Landroidx/core/view/NestedScrollingChild3;
.implements Landroidx/core/view/NestedScrollingChild2;
.implements Landroidx/core/view/NestedScrollingParent;
.implements Landroidx/core/view/NestedScrollingChild;


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout$OnChildScrollUpCallback;,
        Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout$OnRefreshListener;,
        Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout$SavedState;
    }
.end annotation


# static fields
.field private static final ALPHA_ANIMATION_DURATION:I = 0x12c

.field private static final ANIMATE_TO_START_DURATION:I = 0xc8

.field private static final ANIMATE_TO_TRIGGER_DURATION:I = 0xc8

.field static final CIRCLE_DIAMETER:I = 0x28

.field static final CIRCLE_DIAMETER_LARGE:I = 0x38

.field private static final DECELERATE_INTERPOLATION_FACTOR:F = 2.0f

.field public static final DEFAULT:I = 0x1

.field private static final DEFAULT_CIRCLE_TARGET:I = 0x40

.field public static final DEFAULT_SLINGSHOT_DISTANCE:I = -0x1

.field private static final DRAG_RATE:F = 0.5f

.field private static final INVALID_POINTER:I = -0x1

.field public static final LARGE:I = 0x0

.field private static final LAYOUT_ATTRS:[I

.field private static final LOG_TAG:Ljava/lang/String;

.field private static final MAX_ALPHA:I = 0xff

.field private static final MAX_PROGRESS_ANGLE:F = 0.8f

.field private static final SCALE_DOWN_DURATION:I = 0x96

.field private static final STARTING_PROGRESS_ALPHA:I = 0x4c


# instance fields
.field private mActivePointerId:I

.field private mAlphaMaxAnimation:Landroid/view/animation/Animation;

.field private mAlphaStartAnimation:Landroid/view/animation/Animation;

.field private final mAnimateToCorrectPosition:Landroid/view/animation/Animation;

.field private final mAnimateToStartPosition:Landroid/view/animation/Animation;

.field private mChildScrollUpCallback:Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout$OnChildScrollUpCallback;

.field private mCircleDiameter:I

.field mCircleView:Landroidx/swiperefreshlayout/widget/CircleImageView;

.field private mCircleViewIndex:I

.field mCurrentTargetOffsetTop:I

.field mCustomSlingshotDistance:I

.field private final mDecelerateInterpolator:Landroid/view/animation/DecelerateInterpolator;

.field private mEnableLegacyRequestDisallowInterceptTouch:Z

.field protected mFrom:I

.field private mInitialDownY:F

.field private mInitialMotionY:F

.field private mIsBeingDragged:Z

.field mListener:Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout$OnRefreshListener;

.field private mMediumAnimationDuration:I

.field private mNestedScrollInProgress:Z

.field private final mNestedScrollingChildHelper:Landroidx/core/view/NestedScrollingChildHelper;

.field private final mNestedScrollingParentHelper:Landroidx/core/view/NestedScrollingParentHelper;

.field private final mNestedScrollingV2ConsumedCompat:[I

.field mNotify:Z

.field protected mOriginalOffsetTop:I

.field private final mParentOffsetInWindow:[I

.field private final mParentScrollConsumed:[I

.field mProgress:Landroidx/swiperefreshlayout/widget/CircularProgressDrawable;

.field private mRefreshListener:Landroid/view/animation/Animation$AnimationListener;

.field mRefreshing:Z

.field private mReturningToStart:Z

.field mScale:Z

.field private mScaleAnimation:Landroid/view/animation/Animation;

.field private mScaleDownAnimation:Landroid/view/animation/Animation;

.field private mScaleDownToStartAnimation:Landroid/view/animation/Animation;

.field mSpinnerOffsetEnd:I

.field mStartingScale:F

.field private mTarget:Landroid/view/View;

.field private mTotalDragDistance:F

.field private mTotalUnconsumed:F

.field private mTouchSlop:I

.field mUsingCustomStart:Z


# direct methods
.method static constructor <clinit>()V
    .locals 3

    .line 92
    const-class v0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;

    invoke-virtual {v0}, Ljava/lang/Class;->getSimpleName()Ljava/lang/String;

    move-result-object v0

    sput-object v0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->LOG_TAG:Ljava/lang/String;

    .line 150
    const/4 v0, 0x1

    new-array v0, v0, [I

    const/4 v1, 0x0

    const v2, 0x101000e

    aput v2, v0, v1

    sput-object v0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->LAYOUT_ATTRS:[I

    return-void
.end method

.method public constructor <init>(Landroid/content/Context;)V
    .locals 1
    .param p1, "context"    # Landroid/content/Context;

    .line 404
    const/4 v0, 0x0

    invoke-direct {p0, p1, v0}, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;-><init>(Landroid/content/Context;Landroid/util/AttributeSet;)V

    .line 405
    return-void
.end method

.method public constructor <init>(Landroid/content/Context;Landroid/util/AttributeSet;)V
    .locals 5
    .param p1, "context"    # Landroid/content/Context;
    .param p2, "attrs"    # Landroid/util/AttributeSet;

    .line 414
    invoke-direct {p0, p1, p2}, Landroid/view/ViewGroup;-><init>(Landroid/content/Context;Landroid/util/AttributeSet;)V

    .line 118
    const/4 v0, 0x0

    iput-boolean v0, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mRefreshing:Z

    .line 120
    const/high16 v1, -0x40800000    # -1.0f

    iput v1, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mTotalDragDistance:F

    .line 128
    const/4 v1, 0x2

    new-array v2, v1, [I

    iput-object v2, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mParentScrollConsumed:[I

    .line 129
    new-array v2, v1, [I

    iput-object v2, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mParentOffsetInWindow:[I

    .line 133
    new-array v1, v1, [I

    iput-object v1, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mNestedScrollingV2ConsumedCompat:[I

    .line 142
    const/4 v1, -0x1

    iput v1, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mActivePointerId:I

    .line 155
    iput v1, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mCircleViewIndex:I

    .line 191
    new-instance v1, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout$1;

    invoke-direct {v1, p0}, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout$1;-><init>(Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;)V

    iput-object v1, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mRefreshListener:Landroid/view/animation/Animation$AnimationListener;

    .line 1345
    new-instance v1, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout$6;

    invoke-direct {v1, p0}, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout$6;-><init>(Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;)V

    iput-object v1, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mAnimateToCorrectPosition:Landroid/view/animation/Animation;

    .line 1367
    new-instance v1, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout$7;

    invoke-direct {v1, p0}, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout$7;-><init>(Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;)V

    iput-object v1, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mAnimateToStartPosition:Landroid/view/animation/Animation;

    .line 416
    invoke-static {p1}, Landroid/view/ViewConfiguration;->get(Landroid/content/Context;)Landroid/view/ViewConfiguration;

    move-result-object v1

    invoke-virtual {v1}, Landroid/view/ViewConfiguration;->getScaledTouchSlop()I

    move-result v1

    iput v1, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mTouchSlop:I

    .line 418
    invoke-virtual {p0}, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->getResources()Landroid/content/res/Resources;

    move-result-object v1

    const v2, 0x10e0001

    invoke-virtual {v1, v2}, Landroid/content/res/Resources;->getInteger(I)I

    move-result v1

    iput v1, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mMediumAnimationDuration:I

    .line 421
    invoke-virtual {p0, v0}, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->setWillNotDraw(Z)V

    .line 422
    new-instance v1, Landroid/view/animation/DecelerateInterpolator;

    const/high16 v2, 0x40000000    # 2.0f

    invoke-direct {v1, v2}, Landroid/view/animation/DecelerateInterpolator;-><init>(F)V

    iput-object v1, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mDecelerateInterpolator:Landroid/view/animation/DecelerateInterpolator;

    .line 424
    invoke-virtual {p0}, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->getResources()Landroid/content/res/Resources;

    move-result-object v1

    invoke-virtual {v1}, Landroid/content/res/Resources;->getDisplayMetrics()Landroid/util/DisplayMetrics;

    move-result-object v1

    .line 425
    .local v1, "metrics":Landroid/util/DisplayMetrics;
    iget v2, v1, Landroid/util/DisplayMetrics;->density:F

    const/high16 v3, 0x42200000    # 40.0f

    mul-float v2, v2, v3

    float-to-int v2, v2

    iput v2, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mCircleDiameter:I

    .line 427
    invoke-direct {p0}, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->createProgressView()V

    .line 428
    const/4 v2, 0x1

    invoke-virtual {p0, v2}, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->setChildrenDrawingOrderEnabled(Z)V

    .line 430
    iget v3, v1, Landroid/util/DisplayMetrics;->density:F

    const/high16 v4, 0x42800000    # 64.0f

    mul-float v3, v3, v4

    float-to-int v3, v3

    iput v3, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mSpinnerOffsetEnd:I

    .line 431
    int-to-float v3, v3

    iput v3, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mTotalDragDistance:F

    .line 432
    new-instance v3, Landroidx/core/view/NestedScrollingParentHelper;

    invoke-direct {v3, p0}, Landroidx/core/view/NestedScrollingParentHelper;-><init>(Landroid/view/ViewGroup;)V

    iput-object v3, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mNestedScrollingParentHelper:Landroidx/core/view/NestedScrollingParentHelper;

    .line 434
    new-instance v3, Landroidx/core/view/NestedScrollingChildHelper;

    invoke-direct {v3, p0}, Landroidx/core/view/NestedScrollingChildHelper;-><init>(Landroid/view/View;)V

    iput-object v3, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mNestedScrollingChildHelper:Landroidx/core/view/NestedScrollingChildHelper;

    .line 435
    invoke-virtual {p0, v2}, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->setNestedScrollingEnabled(Z)V

    .line 437
    iget v3, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mCircleDiameter:I

    neg-int v3, v3

    iput v3, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mCurrentTargetOffsetTop:I

    iput v3, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mOriginalOffsetTop:I

    .line 438
    const/high16 v3, 0x3f800000    # 1.0f

    invoke-virtual {p0, v3}, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->moveToStart(F)V

    .line 440
    sget-object v3, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->LAYOUT_ATTRS:[I

    invoke-virtual {p1, p2, v3}, Landroid/content/Context;->obtainStyledAttributes(Landroid/util/AttributeSet;[I)Landroid/content/res/TypedArray;

    move-result-object v3

    .line 441
    .local v3, "a":Landroid/content/res/TypedArray;
    invoke-virtual {v3, v0, v2}, Landroid/content/res/TypedArray;->getBoolean(IZ)Z

    move-result v0

    invoke-virtual {p0, v0}, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->setEnabled(Z)V

    .line 442
    invoke-virtual {v3}, Landroid/content/res/TypedArray;->recycle()V

    .line 443
    return-void
.end method

.method private animateOffsetToCorrectPosition(ILandroid/view/animation/Animation$AnimationListener;)V
    .locals 3
    .param p1, "from"    # I
    .param p2, "listener"    # Landroid/view/animation/Animation$AnimationListener;

    .line 1317
    iput p1, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mFrom:I

    .line 1318
    iget-object v0, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mAnimateToCorrectPosition:Landroid/view/animation/Animation;

    invoke-virtual {v0}, Landroid/view/animation/Animation;->reset()V

    .line 1319
    iget-object v0, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mAnimateToCorrectPosition:Landroid/view/animation/Animation;

    const-wide/16 v1, 0xc8

    invoke-virtual {v0, v1, v2}, Landroid/view/animation/Animation;->setDuration(J)V

    .line 1320
    iget-object v0, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mAnimateToCorrectPosition:Landroid/view/animation/Animation;

    iget-object v1, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mDecelerateInterpolator:Landroid/view/animation/DecelerateInterpolator;

    invoke-virtual {v0, v1}, Landroid/view/animation/Animation;->setInterpolator(Landroid/view/animation/Interpolator;)V

    .line 1321
    if-eqz p2, :cond_0

    .line 1322
    iget-object v0, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mCircleView:Landroidx/swiperefreshlayout/widget/CircleImageView;

    invoke-virtual {v0, p2}, Landroidx/swiperefreshlayout/widget/CircleImageView;->setAnimationListener(Landroid/view/animation/Animation$AnimationListener;)V

    .line 1324
    :cond_0
    iget-object v0, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mCircleView:Landroidx/swiperefreshlayout/widget/CircleImageView;

    invoke-virtual {v0}, Landroidx/swiperefreshlayout/widget/CircleImageView;->clearAnimation()V

    .line 1325
    iget-object v0, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mCircleView:Landroidx/swiperefreshlayout/widget/CircleImageView;

    iget-object v1, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mAnimateToCorrectPosition:Landroid/view/animation/Animation;

    invoke-virtual {v0, v1}, Landroidx/swiperefreshlayout/widget/CircleImageView;->startAnimation(Landroid/view/animation/Animation;)V

    .line 1326
    return-void
.end method

.method private animateOffsetToStartPosition(ILandroid/view/animation/Animation$AnimationListener;)V
    .locals 3
    .param p1, "from"    # I
    .param p2, "listener"    # Landroid/view/animation/Animation$AnimationListener;

    .line 1329
    iget-boolean v0, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mScale:Z

    if-eqz v0, :cond_0

    .line 1331
    invoke-direct {p0, p1, p2}, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->startScaleDownReturnToStartAnimation(ILandroid/view/animation/Animation$AnimationListener;)V

    goto :goto_0

    .line 1333
    :cond_0
    iput p1, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mFrom:I

    .line 1334
    iget-object v0, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mAnimateToStartPosition:Landroid/view/animation/Animation;

    invoke-virtual {v0}, Landroid/view/animation/Animation;->reset()V

    .line 1335
    iget-object v0, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mAnimateToStartPosition:Landroid/view/animation/Animation;

    const-wide/16 v1, 0xc8

    invoke-virtual {v0, v1, v2}, Landroid/view/animation/Animation;->setDuration(J)V

    .line 1336
    iget-object v0, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mAnimateToStartPosition:Landroid/view/animation/Animation;

    iget-object v1, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mDecelerateInterpolator:Landroid/view/animation/DecelerateInterpolator;

    invoke-virtual {v0, v1}, Landroid/view/animation/Animation;->setInterpolator(Landroid/view/animation/Interpolator;)V

    .line 1337
    if-eqz p2, :cond_1

    .line 1338
    iget-object v0, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mCircleView:Landroidx/swiperefreshlayout/widget/CircleImageView;

    invoke-virtual {v0, p2}, Landroidx/swiperefreshlayout/widget/CircleImageView;->setAnimationListener(Landroid/view/animation/Animation$AnimationListener;)V

    .line 1340
    :cond_1
    iget-object v0, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mCircleView:Landroidx/swiperefreshlayout/widget/CircleImageView;

    invoke-virtual {v0}, Landroidx/swiperefreshlayout/widget/CircleImageView;->clearAnimation()V

    .line 1341
    iget-object v0, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mCircleView:Landroidx/swiperefreshlayout/widget/CircleImageView;

    iget-object v1, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mAnimateToStartPosition:Landroid/view/animation/Animation;

    invoke-virtual {v0, v1}, Landroidx/swiperefreshlayout/widget/CircleImageView;->startAnimation(Landroid/view/animation/Animation;)V

    .line 1343
    :goto_0
    return-void
.end method

.method private createProgressView()V
    .locals 2

    .line 462
    new-instance v0, Landroidx/swiperefreshlayout/widget/CircleImageView;

    invoke-virtual {p0}, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->getContext()Landroid/content/Context;

    move-result-object v1

    invoke-direct {v0, v1}, Landroidx/swiperefreshlayout/widget/CircleImageView;-><init>(Landroid/content/Context;)V

    iput-object v0, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mCircleView:Landroidx/swiperefreshlayout/widget/CircleImageView;

    .line 463
    new-instance v0, Landroidx/swiperefreshlayout/widget/CircularProgressDrawable;

    invoke-virtual {p0}, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->getContext()Landroid/content/Context;

    move-result-object v1

    invoke-direct {v0, v1}, Landroidx/swiperefreshlayout/widget/CircularProgressDrawable;-><init>(Landroid/content/Context;)V

    iput-object v0, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mProgress:Landroidx/swiperefreshlayout/widget/CircularProgressDrawable;

    .line 464
    const/4 v1, 0x1

    invoke-virtual {v0, v1}, Landroidx/swiperefreshlayout/widget/CircularProgressDrawable;->setStyle(I)V

    .line 465
    iget-object v0, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mCircleView:Landroidx/swiperefreshlayout/widget/CircleImageView;

    iget-object v1, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mProgress:Landroidx/swiperefreshlayout/widget/CircularProgressDrawable;

    invoke-virtual {v0, v1}, Landroidx/swiperefreshlayout/widget/CircleImageView;->setImageDrawable(Landroid/graphics/drawable/Drawable;)V

    .line 466
    iget-object v0, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mCircleView:Landroidx/swiperefreshlayout/widget/CircleImageView;

    const/16 v1, 0x8

    invoke-virtual {v0, v1}, Landroidx/swiperefreshlayout/widget/CircleImageView;->setVisibility(I)V

    .line 467
    iget-object v0, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mCircleView:Landroidx/swiperefreshlayout/widget/CircleImageView;

    invoke-virtual {p0, v0}, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->addView(Landroid/view/View;)V

    .line 468
    return-void
.end method

.method private ensureTarget()V
    .locals 3

    .line 651
    iget-object v0, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mTarget:Landroid/view/View;

    if-nez v0, :cond_1

    .line 652
    const/4 v0, 0x0

    .local v0, "i":I
    :goto_0
    invoke-virtual {p0}, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->getChildCount()I

    move-result v1

    if-ge v0, v1, :cond_1

    .line 653
    invoke-virtual {p0, v0}, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->getChildAt(I)Landroid/view/View;

    move-result-object v1

    .line 654
    .local v1, "child":Landroid/view/View;
    iget-object v2, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mCircleView:Landroidx/swiperefreshlayout/widget/CircleImageView;

    invoke-virtual {v1, v2}, Ljava/lang/Object;->equals(Ljava/lang/Object;)Z

    move-result v2

    if-nez v2, :cond_0

    .line 655
    iput-object v1, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mTarget:Landroid/view/View;

    .line 656
    goto :goto_1

    .line 652
    .end local v1    # "child":Landroid/view/View;
    :cond_0
    add-int/lit8 v0, v0, 0x1

    goto :goto_0

    .line 660
    .end local v0    # "i":I
    :cond_1
    :goto_1
    return-void
.end method

.method private finishSpinner(F)V
    .locals 3
    .param p1, "overscrollTop"    # F

    .line 1193
    iget v0, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mTotalDragDistance:F

    cmpl-float v0, p1, v0

    if-lez v0, :cond_0

    .line 1194
    const/4 v0, 0x1

    invoke-direct {p0, v0, v0}, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->setRefreshing(ZZ)V

    goto :goto_0

    .line 1197
    :cond_0
    const/4 v0, 0x0

    iput-boolean v0, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mRefreshing:Z

    .line 1198
    iget-object v1, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mProgress:Landroidx/swiperefreshlayout/widget/CircularProgressDrawable;

    const/4 v2, 0x0

    invoke-virtual {v1, v2, v2}, Landroidx/swiperefreshlayout/widget/CircularProgressDrawable;->setStartEndTrim(FF)V

    .line 1199
    const/4 v1, 0x0

    .line 1200
    .local v1, "listener":Landroid/view/animation/Animation$AnimationListener;
    iget-boolean v2, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mScale:Z

    if-nez v2, :cond_1

    .line 1201
    new-instance v2, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout$5;

    invoke-direct {v2, p0}, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout$5;-><init>(Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;)V

    move-object v1, v2

    .line 1220
    :cond_1
    iget v2, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mCurrentTargetOffsetTop:I

    invoke-direct {p0, v2, v1}, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->animateOffsetToStartPosition(ILandroid/view/animation/Animation$AnimationListener;)V

    .line 1221
    iget-object v2, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mProgress:Landroidx/swiperefreshlayout/widget/CircularProgressDrawable;

    invoke-virtual {v2, v0}, Landroidx/swiperefreshlayout/widget/CircularProgressDrawable;->setArrowEnabled(Z)V

    .line 1223
    .end local v1    # "listener":Landroid/view/animation/Animation$AnimationListener;
    :goto_0
    return-void
.end method

.method private isAnimationRunning(Landroid/view/animation/Animation;)Z
    .locals 1
    .param p1, "animation"    # Landroid/view/animation/Animation;

    .line 1137
    if-eqz p1, :cond_0

    invoke-virtual {p1}, Landroid/view/animation/Animation;->hasStarted()Z

    move-result v0

    if-eqz v0, :cond_0

    invoke-virtual {p1}, Landroid/view/animation/Animation;->hasEnded()Z

    move-result v0

    if-nez v0, :cond_0

    const/4 v0, 0x1

    goto :goto_0

    :cond_0
    const/4 v0, 0x0

    :goto_0
    return v0
.end method

.method private moveSpinner(F)V
    .locals 16
    .param p1, "overscrollTop"    # F

    .line 1141
    move-object/from16 v0, p0

    iget-object v1, v0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mProgress:Landroidx/swiperefreshlayout/widget/CircularProgressDrawable;

    const/4 v2, 0x1

    invoke-virtual {v1, v2}, Landroidx/swiperefreshlayout/widget/CircularProgressDrawable;->setArrowEnabled(Z)V

    .line 1142
    iget v1, v0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mTotalDragDistance:F

    div-float v1, p1, v1

    .line 1144
    .local v1, "originalDragPercent":F
    invoke-static {v1}, Ljava/lang/Math;->abs(F)F

    move-result v2

    const/high16 v3, 0x3f800000    # 1.0f

    invoke-static {v3, v2}, Ljava/lang/Math;->min(FF)F

    move-result v2

    .line 1145
    .local v2, "dragPercent":F
    float-to-double v4, v2

    const-wide v6, 0x3fd999999999999aL    # 0.4

    sub-double/2addr v4, v6

    const-wide/16 v6, 0x0

    invoke-static {v4, v5, v6, v7}, Ljava/lang/Math;->max(DD)D

    move-result-wide v4

    double-to-float v4, v4

    const/high16 v5, 0x40a00000    # 5.0f

    mul-float v4, v4, v5

    const/high16 v5, 0x40400000    # 3.0f

    div-float/2addr v4, v5

    .line 1146
    .local v4, "adjustedPercent":F
    invoke-static/range {p1 .. p1}, Ljava/lang/Math;->abs(F)F

    move-result v5

    iget v6, v0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mTotalDragDistance:F

    sub-float/2addr v5, v6

    .line 1147
    .local v5, "extraOS":F
    iget v6, v0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mCustomSlingshotDistance:I

    if-lez v6, :cond_0

    .line 1148
    int-to-float v6, v6

    goto :goto_1

    .line 1149
    :cond_0
    iget-boolean v6, v0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mUsingCustomStart:Z

    if-eqz v6, :cond_1

    .line 1150
    iget v6, v0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mSpinnerOffsetEnd:I

    iget v7, v0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mOriginalOffsetTop:I

    sub-int/2addr v6, v7

    goto :goto_0

    .line 1151
    :cond_1
    iget v6, v0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mSpinnerOffsetEnd:I

    :goto_0
    int-to-float v6, v6

    :goto_1
    nop

    .line 1152
    .local v6, "slingshotDist":F
    const/high16 v7, 0x40000000    # 2.0f

    mul-float v8, v6, v7

    invoke-static {v5, v8}, Ljava/lang/Math;->min(FF)F

    move-result v8

    div-float/2addr v8, v6

    const/4 v9, 0x0

    invoke-static {v9, v8}, Ljava/lang/Math;->max(FF)F

    move-result v8

    .line 1154
    .local v8, "tensionSlingshotPercent":F
    const/high16 v10, 0x40800000    # 4.0f

    div-float v11, v8, v10

    float-to-double v11, v11

    div-float v10, v8, v10

    float-to-double v13, v10

    const-wide/high16 v9, 0x4000000000000000L    # 2.0

    invoke-static {v13, v14, v9, v10}, Ljava/lang/Math;->pow(DD)D

    move-result-wide v9

    sub-double/2addr v11, v9

    double-to-float v9, v11

    mul-float v9, v9, v7

    .line 1156
    .local v9, "tensionPercent":F
    mul-float v10, v6, v9

    mul-float v10, v10, v7

    .line 1158
    .local v10, "extraMove":F
    iget v11, v0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mOriginalOffsetTop:I

    mul-float v12, v6, v2

    add-float/2addr v12, v10

    float-to-int v12, v12

    add-int/2addr v11, v12

    .line 1160
    .local v11, "targetY":I
    iget-object v12, v0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mCircleView:Landroidx/swiperefreshlayout/widget/CircleImageView;

    invoke-virtual {v12}, Landroidx/swiperefreshlayout/widget/CircleImageView;->getVisibility()I

    move-result v12

    if-eqz v12, :cond_2

    .line 1161
    iget-object v12, v0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mCircleView:Landroidx/swiperefreshlayout/widget/CircleImageView;

    const/4 v13, 0x0

    invoke-virtual {v12, v13}, Landroidx/swiperefreshlayout/widget/CircleImageView;->setVisibility(I)V

    .line 1163
    :cond_2
    iget-boolean v12, v0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mScale:Z

    if-nez v12, :cond_3

    .line 1164
    iget-object v12, v0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mCircleView:Landroidx/swiperefreshlayout/widget/CircleImageView;

    invoke-virtual {v12, v3}, Landroidx/swiperefreshlayout/widget/CircleImageView;->setScaleX(F)V

    .line 1165
    iget-object v12, v0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mCircleView:Landroidx/swiperefreshlayout/widget/CircleImageView;

    invoke-virtual {v12, v3}, Landroidx/swiperefreshlayout/widget/CircleImageView;->setScaleY(F)V

    .line 1168
    :cond_3
    iget-boolean v12, v0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mScale:Z

    if-eqz v12, :cond_4

    .line 1169
    iget v12, v0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mTotalDragDistance:F

    div-float v12, p1, v12

    invoke-static {v3, v12}, Ljava/lang/Math;->min(FF)F

    move-result v12

    invoke-virtual {v0, v12}, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->setAnimationProgress(F)V

    .line 1171
    :cond_4
    iget v12, v0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mTotalDragDistance:F

    cmpg-float v12, p1, v12

    if-gez v12, :cond_5

    .line 1172
    iget-object v12, v0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mProgress:Landroidx/swiperefreshlayout/widget/CircularProgressDrawable;

    invoke-virtual {v12}, Landroidx/swiperefreshlayout/widget/CircularProgressDrawable;->getAlpha()I

    move-result v12

    const/16 v13, 0x4c

    if-le v12, v13, :cond_6

    iget-object v12, v0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mAlphaStartAnimation:Landroid/view/animation/Animation;

    .line 1173
    invoke-direct {v0, v12}, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->isAnimationRunning(Landroid/view/animation/Animation;)Z

    move-result v12

    if-nez v12, :cond_6

    .line 1175
    invoke-direct/range {p0 .. p0}, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->startProgressAlphaStartAnimation()V

    goto :goto_2

    .line 1178
    :cond_5
    iget-object v12, v0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mProgress:Landroidx/swiperefreshlayout/widget/CircularProgressDrawable;

    invoke-virtual {v12}, Landroidx/swiperefreshlayout/widget/CircularProgressDrawable;->getAlpha()I

    move-result v12

    const/16 v13, 0xff

    if-ge v12, v13, :cond_6

    iget-object v12, v0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mAlphaMaxAnimation:Landroid/view/animation/Animation;

    invoke-direct {v0, v12}, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->isAnimationRunning(Landroid/view/animation/Animation;)Z

    move-result v12

    if-nez v12, :cond_6

    .line 1180
    invoke-direct/range {p0 .. p0}, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->startProgressAlphaMaxAnimation()V

    .line 1183
    :cond_6
    :goto_2
    const v12, 0x3f4ccccd    # 0.8f

    mul-float v13, v4, v12

    .line 1184
    .local v13, "strokeStart":F
    iget-object v14, v0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mProgress:Landroidx/swiperefreshlayout/widget/CircularProgressDrawable;

    invoke-static {v12, v13}, Ljava/lang/Math;->min(FF)F

    move-result v12

    const/4 v15, 0x0

    invoke-virtual {v14, v15, v12}, Landroidx/swiperefreshlayout/widget/CircularProgressDrawable;->setStartEndTrim(FF)V

    .line 1185
    iget-object v12, v0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mProgress:Landroidx/swiperefreshlayout/widget/CircularProgressDrawable;

    invoke-static {v3, v4}, Ljava/lang/Math;->min(FF)F

    move-result v3

    invoke-virtual {v12, v3}, Landroidx/swiperefreshlayout/widget/CircularProgressDrawable;->setArrowScale(F)V

    .line 1187
    const/high16 v3, -0x41800000    # -0.25f

    const v12, 0x3ecccccd    # 0.4f

    mul-float v12, v12, v4

    add-float/2addr v12, v3

    mul-float v7, v7, v9

    add-float/2addr v12, v7

    const/high16 v3, 0x3f000000    # 0.5f

    mul-float v12, v12, v3

    .line 1188
    .local v12, "rotation":F
    iget-object v3, v0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mProgress:Landroidx/swiperefreshlayout/widget/CircularProgressDrawable;

    invoke-virtual {v3, v12}, Landroidx/swiperefreshlayout/widget/CircularProgressDrawable;->setProgressRotation(F)V

    .line 1189
    iget v3, v0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mCurrentTargetOffsetTop:I

    sub-int v3, v11, v3

    invoke-virtual {v0, v3}, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->setTargetOffsetTopAndBottom(I)V

    .line 1190
    return-void
.end method

.method private onSecondaryPointerUp(Landroid/view/MotionEvent;)V
    .locals 4
    .param p1, "ev"    # Landroid/view/MotionEvent;

    .line 1401
    invoke-virtual {p1}, Landroid/view/MotionEvent;->getActionIndex()I

    move-result v0

    .line 1402
    .local v0, "pointerIndex":I
    invoke-virtual {p1, v0}, Landroid/view/MotionEvent;->getPointerId(I)I

    move-result v1

    .line 1403
    .local v1, "pointerId":I
    iget v2, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mActivePointerId:I

    if-ne v1, v2, :cond_1

    .line 1406
    if-nez v0, :cond_0

    const/4 v2, 0x1

    goto :goto_0

    :cond_0
    const/4 v2, 0x0

    .line 1407
    .local v2, "newPointerIndex":I
    :goto_0
    invoke-virtual {p1, v2}, Landroid/view/MotionEvent;->getPointerId(I)I

    move-result v3

    iput v3, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mActivePointerId:I

    .line 1409
    .end local v2    # "newPointerIndex":I
    :cond_1
    return-void
.end method

.method private setColorViewAlpha(I)V
    .locals 1
    .param p1, "targetAlpha"    # I

    .line 299
    iget-object v0, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mCircleView:Landroidx/swiperefreshlayout/widget/CircleImageView;

    invoke-virtual {v0}, Landroidx/swiperefreshlayout/widget/CircleImageView;->getBackground()Landroid/graphics/drawable/Drawable;

    move-result-object v0

    invoke-virtual {v0, p1}, Landroid/graphics/drawable/Drawable;->setAlpha(I)V

    .line 300
    iget-object v0, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mProgress:Landroidx/swiperefreshlayout/widget/CircularProgressDrawable;

    invoke-virtual {v0, p1}, Landroidx/swiperefreshlayout/widget/CircularProgressDrawable;->setAlpha(I)V

    .line 301
    return-void
.end method

.method private setRefreshing(ZZ)V
    .locals 2
    .param p1, "refreshing"    # Z
    .param p2, "notify"    # Z

    .line 529
    iget-boolean v0, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mRefreshing:Z

    if-eq v0, p1, :cond_1

    .line 530
    iput-boolean p2, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mNotify:Z

    .line 531
    invoke-direct {p0}, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->ensureTarget()V

    .line 532
    iput-boolean p1, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mRefreshing:Z

    .line 533
    if-eqz p1, :cond_0

    .line 534
    iget v0, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mCurrentTargetOffsetTop:I

    iget-object v1, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mRefreshListener:Landroid/view/animation/Animation$AnimationListener;

    invoke-direct {p0, v0, v1}, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->animateOffsetToCorrectPosition(ILandroid/view/animation/Animation$AnimationListener;)V

    goto :goto_0

    .line 536
    :cond_0
    iget-object v0, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mRefreshListener:Landroid/view/animation/Animation$AnimationListener;

    invoke-virtual {p0, v0}, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->startScaleDownAnimation(Landroid/view/animation/Animation$AnimationListener;)V

    .line 539
    :cond_1
    :goto_0
    return-void
.end method

.method private startAlphaAnimation(II)Landroid/view/animation/Animation;
    .locals 3
    .param p1, "startingAlpha"    # I
    .param p2, "endingAlpha"    # I

    .line 563
    new-instance v0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout$4;

    invoke-direct {v0, p0, p1, p2}, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout$4;-><init>(Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;II)V

    .line 570
    .local v0, "alpha":Landroid/view/animation/Animation;
    const-wide/16 v1, 0x12c

    invoke-virtual {v0, v1, v2}, Landroid/view/animation/Animation;->setDuration(J)V

    .line 572
    iget-object v1, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mCircleView:Landroidx/swiperefreshlayout/widget/CircleImageView;

    const/4 v2, 0x0

    invoke-virtual {v1, v2}, Landroidx/swiperefreshlayout/widget/CircleImageView;->setAnimationListener(Landroid/view/animation/Animation$AnimationListener;)V

    .line 573
    iget-object v1, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mCircleView:Landroidx/swiperefreshlayout/widget/CircleImageView;

    invoke-virtual {v1}, Landroidx/swiperefreshlayout/widget/CircleImageView;->clearAnimation()V

    .line 574
    iget-object v1, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mCircleView:Landroidx/swiperefreshlayout/widget/CircleImageView;

    invoke-virtual {v1, v0}, Landroidx/swiperefreshlayout/widget/CircleImageView;->startAnimation(Landroid/view/animation/Animation;)V

    .line 575
    return-object v0
.end method

.method private startDragging(F)V
    .locals 4
    .param p1, "y"    # F

    .line 1308
    iget v0, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mInitialDownY:F

    sub-float v1, p1, v0

    .line 1309
    .local v1, "yDiff":F
    iget v2, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mTouchSlop:I

    int-to-float v3, v2

    cmpl-float v3, v1, v3

    if-lez v3, :cond_0

    iget-boolean v3, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mIsBeingDragged:Z

    if-nez v3, :cond_0

    .line 1310
    int-to-float v2, v2

    add-float/2addr v0, v2

    iput v0, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mInitialMotionY:F

    .line 1311
    const/4 v0, 0x1

    iput-boolean v0, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mIsBeingDragged:Z

    .line 1312
    iget-object v0, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mProgress:Landroidx/swiperefreshlayout/widget/CircularProgressDrawable;

    const/16 v2, 0x4c

    invoke-virtual {v0, v2}, Landroidx/swiperefreshlayout/widget/CircularProgressDrawable;->setAlpha(I)V

    .line 1314
    :cond_0
    return-void
.end method

.method private startProgressAlphaMaxAnimation()V
    .locals 2

    .line 559
    iget-object v0, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mProgress:Landroidx/swiperefreshlayout/widget/CircularProgressDrawable;

    invoke-virtual {v0}, Landroidx/swiperefreshlayout/widget/CircularProgressDrawable;->getAlpha()I

    move-result v0

    const/16 v1, 0xff

    invoke-direct {p0, v0, v1}, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->startAlphaAnimation(II)Landroid/view/animation/Animation;

    move-result-object v0

    iput-object v0, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mAlphaMaxAnimation:Landroid/view/animation/Animation;

    .line 560
    return-void
.end method

.method private startProgressAlphaStartAnimation()V
    .locals 2

    .line 555
    iget-object v0, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mProgress:Landroidx/swiperefreshlayout/widget/CircularProgressDrawable;

    invoke-virtual {v0}, Landroidx/swiperefreshlayout/widget/CircularProgressDrawable;->getAlpha()I

    move-result v0

    const/16 v1, 0x4c

    invoke-direct {p0, v0, v1}, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->startAlphaAnimation(II)Landroid/view/animation/Animation;

    move-result-object v0

    iput-object v0, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mAlphaStartAnimation:Landroid/view/animation/Animation;

    .line 556
    return-void
.end method

.method private startScaleDownReturnToStartAnimation(ILandroid/view/animation/Animation$AnimationListener;)V
    .locals 3
    .param p1, "from"    # I
    .param p2, "listener"    # Landroid/view/animation/Animation$AnimationListener;

    .line 1376
    iput p1, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mFrom:I

    .line 1377
    iget-object v0, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mCircleView:Landroidx/swiperefreshlayout/widget/CircleImageView;

    invoke-virtual {v0}, Landroidx/swiperefreshlayout/widget/CircleImageView;->getScaleX()F

    move-result v0

    iput v0, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mStartingScale:F

    .line 1378
    new-instance v0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout$8;

    invoke-direct {v0, p0}, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout$8;-><init>(Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;)V

    iput-object v0, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mScaleDownToStartAnimation:Landroid/view/animation/Animation;

    .line 1386
    const-wide/16 v1, 0x96

    invoke-virtual {v0, v1, v2}, Landroid/view/animation/Animation;->setDuration(J)V

    .line 1387
    if-eqz p2, :cond_0

    .line 1388
    iget-object v0, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mCircleView:Landroidx/swiperefreshlayout/widget/CircleImageView;

    invoke-virtual {v0, p2}, Landroidx/swiperefreshlayout/widget/CircleImageView;->setAnimationListener(Landroid/view/animation/Animation$AnimationListener;)V

    .line 1390
    :cond_0
    iget-object v0, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mCircleView:Landroidx/swiperefreshlayout/widget/CircleImageView;

    invoke-virtual {v0}, Landroidx/swiperefreshlayout/widget/CircleImageView;->clearAnimation()V

    .line 1391
    iget-object v0, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mCircleView:Landroidx/swiperefreshlayout/widget/CircleImageView;

    iget-object v1, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mScaleDownToStartAnimation:Landroid/view/animation/Animation;

    invoke-virtual {v0, v1}, Landroidx/swiperefreshlayout/widget/CircleImageView;->startAnimation(Landroid/view/animation/Animation;)V

    .line 1392
    return-void
.end method

.method private startScaleUpAnimation(Landroid/view/animation/Animation$AnimationListener;)V
    .locals 3
    .param p1, "listener"    # Landroid/view/animation/Animation$AnimationListener;

    .line 503
    iget-object v0, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mCircleView:Landroidx/swiperefreshlayout/widget/CircleImageView;

    const/4 v1, 0x0

    invoke-virtual {v0, v1}, Landroidx/swiperefreshlayout/widget/CircleImageView;->setVisibility(I)V

    .line 504
    iget-object v0, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mProgress:Landroidx/swiperefreshlayout/widget/CircularProgressDrawable;

    const/16 v1, 0xff

    invoke-virtual {v0, v1}, Landroidx/swiperefreshlayout/widget/CircularProgressDrawable;->setAlpha(I)V

    .line 505
    new-instance v0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout$2;

    invoke-direct {v0, p0}, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout$2;-><init>(Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;)V

    iput-object v0, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mScaleAnimation:Landroid/view/animation/Animation;

    .line 511
    iget v1, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mMediumAnimationDuration:I

    int-to-long v1, v1

    invoke-virtual {v0, v1, v2}, Landroid/view/animation/Animation;->setDuration(J)V

    .line 512
    if-eqz p1, :cond_0

    .line 513
    iget-object v0, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mCircleView:Landroidx/swiperefreshlayout/widget/CircleImageView;

    invoke-virtual {v0, p1}, Landroidx/swiperefreshlayout/widget/CircleImageView;->setAnimationListener(Landroid/view/animation/Animation$AnimationListener;)V

    .line 515
    :cond_0
    iget-object v0, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mCircleView:Landroidx/swiperefreshlayout/widget/CircleImageView;

    invoke-virtual {v0}, Landroidx/swiperefreshlayout/widget/CircleImageView;->clearAnimation()V

    .line 516
    iget-object v0, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mCircleView:Landroidx/swiperefreshlayout/widget/CircleImageView;

    iget-object v1, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mScaleAnimation:Landroid/view/animation/Animation;

    invoke-virtual {v0, v1}, Landroidx/swiperefreshlayout/widget/CircleImageView;->startAnimation(Landroid/view/animation/Animation;)V

    .line 517
    return-void
.end method


# virtual methods
.method public canChildScrollUp()Z
    .locals 3

    .line 736
    iget-object v0, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mChildScrollUpCallback:Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout$OnChildScrollUpCallback;

    if-eqz v0, :cond_0

    .line 737
    iget-object v1, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mTarget:Landroid/view/View;

    invoke-interface {v0, p0, v1}, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout$OnChildScrollUpCallback;->canChildScrollUp(Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;Landroid/view/View;)Z

    move-result v0

    return v0

    .line 739
    :cond_0
    iget-object v0, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mTarget:Landroid/view/View;

    instance-of v1, v0, Landroid/widget/ListView;

    const/4 v2, -0x1

    if-eqz v1, :cond_1

    .line 740
    check-cast v0, Landroid/widget/ListView;

    invoke-static {v0, v2}, Landroidx/core/widget/ListViewCompat;->canScrollList(Landroid/widget/ListView;I)Z

    move-result v0

    return v0

    .line 742
    :cond_1
    invoke-virtual {v0, v2}, Landroid/view/View;->canScrollVertically(I)Z

    move-result v0

    return v0
.end method

.method public dispatchNestedFling(FFZ)Z
    .locals 1
    .param p1, "velocityX"    # F
    .param p2, "velocityY"    # F
    .param p3, "consumed"    # Z

    .line 1128
    iget-object v0, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mNestedScrollingChildHelper:Landroidx/core/view/NestedScrollingChildHelper;

    invoke-virtual {v0, p1, p2, p3}, Landroidx/core/view/NestedScrollingChildHelper;->dispatchNestedFling(FFZ)Z

    move-result v0

    return v0
.end method

.method public dispatchNestedPreFling(FF)Z
    .locals 1
    .param p1, "velocityX"    # F
    .param p2, "velocityY"    # F

    .line 1133
    iget-object v0, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mNestedScrollingChildHelper:Landroidx/core/view/NestedScrollingChildHelper;

    invoke-virtual {v0, p1, p2}, Landroidx/core/view/NestedScrollingChildHelper;->dispatchNestedPreFling(FF)Z

    move-result v0

    return v0
.end method

.method public dispatchNestedPreScroll(II[I[I)Z
    .locals 1
    .param p1, "dx"    # I
    .param p2, "dy"    # I
    .param p3, "consumed"    # [I
    .param p4, "offsetInWindow"    # [I

    .line 1122
    iget-object v0, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mNestedScrollingChildHelper:Landroidx/core/view/NestedScrollingChildHelper;

    invoke-virtual {v0, p1, p2, p3, p4}, Landroidx/core/view/NestedScrollingChildHelper;->dispatchNestedPreScroll(II[I[I)Z

    move-result v0

    return v0
.end method

.method public dispatchNestedPreScroll(II[I[II)Z
    .locals 1
    .param p1, "dx"    # I
    .param p2, "dy"    # I
    .param p3, "consumed"    # [I
    .param p4, "offsetInWindow"    # [I
    .param p5, "type"    # I

    .line 1082
    if-nez p5, :cond_0

    invoke-virtual {p0, p1, p2, p3, p4}, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->dispatchNestedPreScroll(II[I[I)Z

    move-result v0

    if-eqz v0, :cond_0

    const/4 v0, 0x1

    goto :goto_0

    :cond_0
    const/4 v0, 0x0

    :goto_0
    return v0
.end method

.method public dispatchNestedScroll(IIII[II[I)V
    .locals 8
    .param p1, "dxConsumed"    # I
    .param p2, "dyConsumed"    # I
    .param p3, "dxUnconsumed"    # I
    .param p4, "dyUnconsumed"    # I
    .param p5, "offsetInWindow"    # [I
    .param p6, "type"    # I
    .param p7, "consumed"    # [I

    .line 1047
    if-nez p6, :cond_0

    .line 1048
    iget-object v0, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mNestedScrollingChildHelper:Landroidx/core/view/NestedScrollingChildHelper;

    move v1, p1

    move v2, p2

    move v3, p3

    move v4, p4

    move-object v5, p5

    move v6, p6

    move-object v7, p7

    invoke-virtual/range {v0 .. v7}, Landroidx/core/view/NestedScrollingChildHelper;->dispatchNestedScroll(IIII[II[I)V

    .line 1051
    :cond_0
    return-void
.end method

.method public dispatchNestedScroll(IIII[I)Z
    .locals 6
    .param p1, "dxConsumed"    # I
    .param p2, "dyConsumed"    # I
    .param p3, "dxUnconsumed"    # I
    .param p4, "dyUnconsumed"    # I
    .param p5, "offsetInWindow"    # [I

    .line 1116
    iget-object v0, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mNestedScrollingChildHelper:Landroidx/core/view/NestedScrollingChildHelper;

    move v1, p1

    move v2, p2

    move v3, p3

    move v4, p4

    move-object v5, p5

    invoke-virtual/range {v0 .. v5}, Landroidx/core/view/NestedScrollingChildHelper;->dispatchNestedScroll(IIII[I)Z

    move-result v0

    return v0
.end method

.method public dispatchNestedScroll(IIII[II)Z
    .locals 7
    .param p1, "dxConsumed"    # I
    .param p2, "dyConsumed"    # I
    .param p3, "dxUnconsumed"    # I
    .param p4, "dyUnconsumed"    # I
    .param p5, "offsetInWindow"    # [I
    .param p6, "type"    # I

    .line 1075
    if-nez p6, :cond_0

    iget-object v0, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mNestedScrollingChildHelper:Landroidx/core/view/NestedScrollingChildHelper;

    move v1, p1

    move v2, p2

    move v3, p3

    move v4, p4

    move-object v5, p5

    move v6, p6

    invoke-virtual/range {v0 .. v6}, Landroidx/core/view/NestedScrollingChildHelper;->dispatchNestedScroll(IIII[II)Z

    move-result v0

    if-eqz v0, :cond_0

    const/4 v0, 0x1

    goto :goto_0

    :cond_0
    const/4 v0, 0x0

    :goto_0
    return v0
.end method

.method protected getChildDrawingOrder(II)I
    .locals 2
    .param p1, "childCount"    # I
    .param p2, "i"    # I

    .line 447
    iget v0, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mCircleViewIndex:I

    if-gez v0, :cond_0

    .line 448
    return p2

    .line 449
    :cond_0
    add-int/lit8 v1, p1, -0x1

    if-ne p2, v1, :cond_1

    .line 451
    return v0

    .line 452
    :cond_1
    if-lt p2, v0, :cond_2

    .line 454
    add-int/lit8 v0, p2, 0x1

    return v0

    .line 457
    :cond_2
    return p2
.end method

.method public getNestedScrollAxes()I
    .locals 1

    .line 1005
    iget-object v0, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mNestedScrollingParentHelper:Landroidx/core/view/NestedScrollingParentHelper;

    invoke-virtual {v0}, Landroidx/core/view/NestedScrollingParentHelper;->getNestedScrollAxes()I

    move-result v0

    return v0
.end method

.method public getProgressCircleDiameter()I
    .locals 1

    .line 728
    iget v0, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mCircleDiameter:I

    return v0
.end method

.method public getProgressViewEndOffset()I
    .locals 1

    .line 344
    iget v0, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mSpinnerOffsetEnd:I

    return v0
.end method

.method public getProgressViewStartOffset()I
    .locals 1

    .line 336
    iget v0, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mOriginalOffsetTop:I

    return v0
.end method

.method public hasNestedScrollingParent()Z
    .locals 1

    .line 1110
    iget-object v0, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mNestedScrollingChildHelper:Landroidx/core/view/NestedScrollingChildHelper;

    invoke-virtual {v0}, Landroidx/core/view/NestedScrollingChildHelper;->hasNestedScrollingParent()Z

    move-result v0

    return v0
.end method

.method public hasNestedScrollingParent(I)Z
    .locals 1
    .param p1, "type"    # I

    .line 1069
    if-nez p1, :cond_0

    invoke-virtual {p0}, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->hasNestedScrollingParent()Z

    move-result v0

    if-eqz v0, :cond_0

    const/4 v0, 0x1

    goto :goto_0

    :cond_0
    const/4 v0, 0x0

    :goto_0
    return v0
.end method

.method public isNestedScrollingEnabled()Z
    .locals 1

    .line 1095
    iget-object v0, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mNestedScrollingChildHelper:Landroidx/core/view/NestedScrollingChildHelper;

    invoke-virtual {v0}, Landroidx/core/view/NestedScrollingChildHelper;->isNestedScrollingEnabled()Z

    move-result v0

    return v0
.end method

.method public isRefreshing()Z
    .locals 1

    .line 645
    iget-boolean v0, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mRefreshing:Z

    return v0
.end method

.method moveToStart(F)V
    .locals 2
    .param p1, "interpolatedTime"    # F

    .line 1362
    iget v0, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mFrom:I

    iget v1, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mOriginalOffsetTop:I

    sub-int/2addr v1, v0

    int-to-float v1, v1

    mul-float v1, v1, p1

    float-to-int v1, v1

    add-int/2addr v0, v1

    .line 1363
    .local v0, "targetTop":I
    iget-object v1, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mCircleView:Landroidx/swiperefreshlayout/widget/CircleImageView;

    invoke-virtual {v1}, Landroidx/swiperefreshlayout/widget/CircleImageView;->getTop()I

    move-result v1

    sub-int v1, v0, v1

    .line 1364
    .local v1, "offset":I
    invoke-virtual {p0, v1}, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->setTargetOffsetTopAndBottom(I)V

    .line 1365
    return-void
.end method

.method protected onDetachedFromWindow()V
    .locals 0

    .line 294
    invoke-super {p0}, Landroid/view/ViewGroup;->onDetachedFromWindow()V

    .line 295
    invoke-virtual {p0}, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->reset()V

    .line 296
    return-void
.end method

.method public onInterceptTouchEvent(Landroid/view/MotionEvent;)Z
    .locals 4
    .param p1, "ev"    # Landroid/view/MotionEvent;

    .line 756
    invoke-direct {p0}, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->ensureTarget()V

    .line 758
    invoke-virtual {p1}, Landroid/view/MotionEvent;->getActionMasked()I

    move-result v0

    .line 761
    .local v0, "action":I
    iget-boolean v1, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mReturningToStart:Z

    const/4 v2, 0x0

    if-eqz v1, :cond_0

    if-nez v0, :cond_0

    .line 762
    iput-boolean v2, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mReturningToStart:Z

    .line 765
    :cond_0
    invoke-virtual {p0}, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->isEnabled()Z

    move-result v1

    if-eqz v1, :cond_5

    iget-boolean v1, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mReturningToStart:Z

    if-nez v1, :cond_5

    invoke-virtual {p0}, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->canChildScrollUp()Z

    move-result v1

    if-nez v1, :cond_5

    iget-boolean v1, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mRefreshing:Z

    if-nez v1, :cond_5

    iget-boolean v1, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mNestedScrollInProgress:Z

    if-eqz v1, :cond_1

    goto :goto_1

    .line 771
    :cond_1
    const/4 v1, -0x1

    packed-switch v0, :pswitch_data_0

    :pswitch_0
    goto :goto_0

    .line 799
    :pswitch_1
    invoke-direct {p0, p1}, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->onSecondaryPointerUp(Landroid/view/MotionEvent;)V

    .line 800
    goto :goto_0

    .line 785
    :pswitch_2
    iget v3, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mActivePointerId:I

    if-ne v3, v1, :cond_2

    .line 786
    sget-object v1, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->LOG_TAG:Ljava/lang/String;

    const-string v3, "Got ACTION_MOVE event but don\'t have an active pointer id."

    invoke-static {v1, v3}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;)I

    .line 787
    return v2

    .line 790
    :cond_2
    invoke-virtual {p1, v3}, Landroid/view/MotionEvent;->findPointerIndex(I)I

    move-result v1

    .line 791
    .local v1, "pointerIndex":I
    if-gez v1, :cond_3

    .line 792
    return v2

    .line 794
    :cond_3
    invoke-virtual {p1, v1}, Landroid/view/MotionEvent;->getY(I)F

    move-result v2

    .line 795
    .local v2, "y":F
    invoke-direct {p0, v2}, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->startDragging(F)V

    .line 796
    goto :goto_0

    .line 804
    .end local v1    # "pointerIndex":I
    .end local v2    # "y":F
    :pswitch_3
    iput-boolean v2, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mIsBeingDragged:Z

    .line 805
    iput v1, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mActivePointerId:I

    goto :goto_0

    .line 773
    :pswitch_4
    iget v1, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mOriginalOffsetTop:I

    iget-object v3, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mCircleView:Landroidx/swiperefreshlayout/widget/CircleImageView;

    invoke-virtual {v3}, Landroidx/swiperefreshlayout/widget/CircleImageView;->getTop()I

    move-result v3

    sub-int/2addr v1, v3

    invoke-virtual {p0, v1}, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->setTargetOffsetTopAndBottom(I)V

    .line 774
    invoke-virtual {p1, v2}, Landroid/view/MotionEvent;->getPointerId(I)I

    move-result v1

    iput v1, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mActivePointerId:I

    .line 775
    iput-boolean v2, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mIsBeingDragged:Z

    .line 777
    invoke-virtual {p1, v1}, Landroid/view/MotionEvent;->findPointerIndex(I)I

    move-result v1

    .line 778
    .restart local v1    # "pointerIndex":I
    if-gez v1, :cond_4

    .line 779
    return v2

    .line 781
    :cond_4
    invoke-virtual {p1, v1}, Landroid/view/MotionEvent;->getY(I)F

    move-result v2

    iput v2, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mInitialDownY:F

    .line 782
    nop

    .line 809
    .end local v1    # "pointerIndex":I
    :goto_0
    iget-boolean v1, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mIsBeingDragged:Z

    return v1

    .line 768
    :cond_5
    :goto_1
    return v2

    :pswitch_data_0
    .packed-switch 0x0
        :pswitch_4
        :pswitch_3
        :pswitch_2
        :pswitch_3
        :pswitch_0
        :pswitch_0
        :pswitch_1
    .end packed-switch
.end method

.method protected onLayout(ZIIII)V
    .locals 15
    .param p1, "changed"    # Z
    .param p2, "left"    # I
    .param p3, "top"    # I
    .param p4, "right"    # I
    .param p5, "bottom"    # I

    .line 673
    move-object v0, p0

    invoke-virtual {p0}, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->getMeasuredWidth()I

    move-result v1

    .line 674
    .local v1, "width":I
    invoke-virtual {p0}, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->getMeasuredHeight()I

    move-result v2

    .line 675
    .local v2, "height":I
    invoke-virtual {p0}, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->getChildCount()I

    move-result v3

    if-nez v3, :cond_0

    .line 676
    return-void

    .line 678
    :cond_0
    iget-object v3, v0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mTarget:Landroid/view/View;

    if-nez v3, :cond_1

    .line 679
    invoke-direct {p0}, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->ensureTarget()V

    .line 681
    :cond_1
    iget-object v3, v0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mTarget:Landroid/view/View;

    if-nez v3, :cond_2

    .line 682
    return-void

    .line 684
    :cond_2
    iget-object v3, v0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mTarget:Landroid/view/View;

    .line 685
    .local v3, "child":Landroid/view/View;
    invoke-virtual {p0}, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->getPaddingLeft()I

    move-result v4

    .line 686
    .local v4, "childLeft":I
    invoke-virtual {p0}, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->getPaddingTop()I

    move-result v5

    .line 687
    .local v5, "childTop":I
    invoke-virtual {p0}, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->getPaddingLeft()I

    move-result v6

    sub-int v6, v1, v6

    invoke-virtual {p0}, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->getPaddingRight()I

    move-result v7

    sub-int/2addr v6, v7

    .line 688
    .local v6, "childWidth":I
    invoke-virtual {p0}, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->getPaddingTop()I

    move-result v7

    sub-int v7, v2, v7

    invoke-virtual {p0}, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->getPaddingBottom()I

    move-result v8

    sub-int/2addr v7, v8

    .line 689
    .local v7, "childHeight":I
    add-int v8, v4, v6

    add-int v9, v5, v7

    invoke-virtual {v3, v4, v5, v8, v9}, Landroid/view/View;->layout(IIII)V

    .line 690
    iget-object v8, v0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mCircleView:Landroidx/swiperefreshlayout/widget/CircleImageView;

    invoke-virtual {v8}, Landroidx/swiperefreshlayout/widget/CircleImageView;->getMeasuredWidth()I

    move-result v8

    .line 691
    .local v8, "circleWidth":I
    iget-object v9, v0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mCircleView:Landroidx/swiperefreshlayout/widget/CircleImageView;

    invoke-virtual {v9}, Landroidx/swiperefreshlayout/widget/CircleImageView;->getMeasuredHeight()I

    move-result v9

    .line 692
    .local v9, "circleHeight":I
    iget-object v10, v0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mCircleView:Landroidx/swiperefreshlayout/widget/CircleImageView;

    div-int/lit8 v11, v1, 0x2

    div-int/lit8 v12, v8, 0x2

    sub-int/2addr v11, v12

    iget v12, v0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mCurrentTargetOffsetTop:I

    div-int/lit8 v13, v1, 0x2

    div-int/lit8 v14, v8, 0x2

    add-int/2addr v13, v14

    add-int v14, v12, v9

    invoke-virtual {v10, v11, v12, v13, v14}, Landroidx/swiperefreshlayout/widget/CircleImageView;->layout(IIII)V

    .line 694
    return-void
.end method

.method public onMeasure(II)V
    .locals 5
    .param p1, "widthMeasureSpec"    # I
    .param p2, "heightMeasureSpec"    # I

    .line 698
    invoke-super {p0, p1, p2}, Landroid/view/ViewGroup;->onMeasure(II)V

    .line 699
    iget-object v0, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mTarget:Landroid/view/View;

    if-nez v0, :cond_0

    .line 700
    invoke-direct {p0}, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->ensureTarget()V

    .line 702
    :cond_0
    iget-object v0, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mTarget:Landroid/view/View;

    if-nez v0, :cond_1

    .line 703
    return-void

    .line 705
    :cond_1
    nop

    .line 706
    invoke-virtual {p0}, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->getMeasuredWidth()I

    move-result v1

    invoke-virtual {p0}, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->getPaddingLeft()I

    move-result v2

    sub-int/2addr v1, v2

    invoke-virtual {p0}, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->getPaddingRight()I

    move-result v2

    sub-int/2addr v1, v2

    .line 705
    const/high16 v2, 0x40000000    # 2.0f

    invoke-static {v1, v2}, Landroid/view/View$MeasureSpec;->makeMeasureSpec(II)I

    move-result v1

    .line 708
    invoke-virtual {p0}, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->getMeasuredHeight()I

    move-result v3

    invoke-virtual {p0}, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->getPaddingTop()I

    move-result v4

    sub-int/2addr v3, v4

    invoke-virtual {p0}, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->getPaddingBottom()I

    move-result v4

    sub-int/2addr v3, v4

    .line 707
    invoke-static {v3, v2}, Landroid/view/View$MeasureSpec;->makeMeasureSpec(II)I

    move-result v3

    .line 705
    invoke-virtual {v0, v1, v3}, Landroid/view/View;->measure(II)V

    .line 709
    iget-object v0, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mCircleView:Landroidx/swiperefreshlayout/widget/CircleImageView;

    iget v1, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mCircleDiameter:I

    invoke-static {v1, v2}, Landroid/view/View$MeasureSpec;->makeMeasureSpec(II)I

    move-result v1

    iget v3, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mCircleDiameter:I

    .line 710
    invoke-static {v3, v2}, Landroid/view/View$MeasureSpec;->makeMeasureSpec(II)I

    move-result v2

    .line 709
    invoke-virtual {v0, v1, v2}, Landroidx/swiperefreshlayout/widget/CircleImageView;->measure(II)V

    .line 711
    const/4 v0, -0x1

    iput v0, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mCircleViewIndex:I

    .line 713
    const/4 v0, 0x0

    .local v0, "index":I
    :goto_0
    invoke-virtual {p0}, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->getChildCount()I

    move-result v1

    if-ge v0, v1, :cond_3

    .line 714
    invoke-virtual {p0, v0}, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->getChildAt(I)Landroid/view/View;

    move-result-object v1

    iget-object v2, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mCircleView:Landroidx/swiperefreshlayout/widget/CircleImageView;

    if-ne v1, v2, :cond_2

    .line 715
    iput v0, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mCircleViewIndex:I

    .line 716
    goto :goto_1

    .line 713
    :cond_2
    add-int/lit8 v0, v0, 0x1

    goto :goto_0

    .line 719
    .end local v0    # "index":I
    :cond_3
    :goto_1
    return-void
.end method

.method public onNestedFling(Landroid/view/View;FFZ)Z
    .locals 1
    .param p1, "target"    # Landroid/view/View;
    .param p2, "velocityX"    # F
    .param p3, "velocityY"    # F
    .param p4, "consumed"    # Z

    .line 1038
    invoke-virtual {p0, p2, p3, p4}, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->dispatchNestedFling(FFZ)Z

    move-result v0

    return v0
.end method

.method public onNestedPreFling(Landroid/view/View;FF)Z
    .locals 1
    .param p1, "target"    # Landroid/view/View;
    .param p2, "velocityX"    # F
    .param p3, "velocityY"    # F

    .line 1032
    invoke-virtual {p0, p2, p3}, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->dispatchNestedPreFling(FF)Z

    move-result v0

    return v0
.end method

.method public onNestedPreScroll(Landroid/view/View;II[I)V
    .locals 6
    .param p1, "target"    # Landroid/view/View;
    .param p2, "dx"    # I
    .param p3, "dy"    # I
    .param p4, "consumed"    # [I

    .line 975
    const/4 v0, 0x0

    const/4 v1, 0x1

    if-lez p3, :cond_1

    iget v2, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mTotalUnconsumed:F

    cmpl-float v3, v2, v0

    if-lez v3, :cond_1

    .line 976
    int-to-float v3, p3

    cmpl-float v3, v3, v2

    if-lez v3, :cond_0

    .line 977
    float-to-int v2, v2

    aput v2, p4, v1

    .line 978
    iput v0, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mTotalUnconsumed:F

    goto :goto_0

    .line 980
    :cond_0
    int-to-float v3, p3

    sub-float/2addr v2, v3

    iput v2, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mTotalUnconsumed:F

    .line 981
    aput p3, p4, v1

    .line 983
    :goto_0
    iget v2, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mTotalUnconsumed:F

    invoke-direct {p0, v2}, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->moveSpinner(F)V

    .line 990
    :cond_1
    iget-boolean v2, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mUsingCustomStart:Z

    if-eqz v2, :cond_2

    if-lez p3, :cond_2

    iget v2, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mTotalUnconsumed:F

    cmpl-float v0, v2, v0

    if-nez v0, :cond_2

    aget v0, p4, v1

    sub-int v0, p3, v0

    .line 991
    invoke-static {v0}, Ljava/lang/Math;->abs(I)I

    move-result v0

    if-lez v0, :cond_2

    .line 992
    iget-object v0, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mCircleView:Landroidx/swiperefreshlayout/widget/CircleImageView;

    const/16 v2, 0x8

    invoke-virtual {v0, v2}, Landroidx/swiperefreshlayout/widget/CircleImageView;->setVisibility(I)V

    .line 996
    :cond_2
    iget-object v0, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mParentScrollConsumed:[I

    .line 997
    .local v0, "parentConsumed":[I
    const/4 v2, 0x0

    aget v3, p4, v2

    sub-int v3, p2, v3

    aget v4, p4, v1

    sub-int v4, p3, v4

    const/4 v5, 0x0

    invoke-virtual {p0, v3, v4, v0, v5}, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->dispatchNestedPreScroll(II[I[I)Z

    move-result v3

    if-eqz v3, :cond_3

    .line 998
    aget v3, p4, v2

    aget v4, v0, v2

    add-int/2addr v3, v4

    aput v3, p4, v2

    .line 999
    aget v2, p4, v1

    aget v3, v0, v1

    add-int/2addr v2, v3

    aput v2, p4, v1

    .line 1001
    :cond_3
    return-void
.end method

.method public onNestedPreScroll(Landroid/view/View;II[II)V
    .locals 0
    .param p1, "target"    # Landroid/view/View;
    .param p2, "dx"    # I
    .param p3, "dy"    # I
    .param p4, "consumed"    # [I
    .param p5, "type"    # I

    .line 948
    if-nez p5, :cond_0

    .line 949
    invoke-virtual {p0, p1, p2, p3, p4}, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->onNestedPreScroll(Landroid/view/View;II[I)V

    .line 951
    :cond_0
    return-void
.end method

.method public onNestedScroll(Landroid/view/View;IIII)V
    .locals 8
    .param p1, "target"    # Landroid/view/View;
    .param p2, "dxConsumed"    # I
    .param p3, "dyConsumed"    # I
    .param p4, "dxUnconsumed"    # I
    .param p5, "dyUnconsumed"    # I

    .line 1025
    iget-object v7, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mNestedScrollingV2ConsumedCompat:[I

    const/4 v6, 0x0

    move-object v0, p0

    move-object v1, p1

    move v2, p2

    move v3, p3

    move v4, p4

    move v5, p5

    invoke-virtual/range {v0 .. v7}, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->onNestedScroll(Landroid/view/View;IIIII[I)V

    .line 1027
    return-void
.end method

.method public onNestedScroll(Landroid/view/View;IIIII)V
    .locals 8
    .param p1, "target"    # Landroid/view/View;
    .param p2, "dxConsumed"    # I
    .param p3, "dyConsumed"    # I
    .param p4, "dxUnconsumed"    # I
    .param p5, "dyUnconsumed"    # I
    .param p6, "type"    # I

    .line 940
    iget-object v7, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mNestedScrollingV2ConsumedCompat:[I

    move-object v0, p0

    move-object v1, p1

    move v2, p2

    move v3, p3

    move v4, p4

    move v5, p5

    move v6, p6

    invoke-virtual/range {v0 .. v7}, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->onNestedScroll(Landroid/view/View;IIIII[I)V

    .line 942
    return-void
.end method

.method public onNestedScroll(Landroid/view/View;IIIII[I)V
    .locals 11
    .param p1, "target"    # Landroid/view/View;
    .param p2, "dxConsumed"    # I
    .param p3, "dyConsumed"    # I
    .param p4, "dxUnconsumed"    # I
    .param p5, "dyUnconsumed"    # I
    .param p6, "type"    # I
    .param p7, "consumed"    # [I

    .line 865
    move-object v8, p0

    if-eqz p6, :cond_0

    .line 866
    return-void

    .line 873
    :cond_0
    const/4 v9, 0x1

    aget v10, p7, v9

    .line 874
    .local v10, "consumedBeforeParents":I
    iget-object v5, v8, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mParentOffsetInWindow:[I

    move-object v0, p0

    move v1, p2

    move v2, p3

    move v3, p4

    move/from16 v4, p5

    move/from16 v6, p6

    move-object/from16 v7, p7

    invoke-virtual/range {v0 .. v7}, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->dispatchNestedScroll(IIII[II[I)V

    .line 876
    aget v0, p7, v9

    sub-int/2addr v0, v10

    .line 877
    .local v0, "consumedByParents":I
    sub-int v1, p5, v0

    .line 888
    .local v1, "unconsumedAfterParents":I
    if-nez v1, :cond_1

    .line 891
    iget-object v2, v8, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mParentOffsetInWindow:[I

    aget v2, v2, v9

    add-int v2, p5, v2

    .local v2, "remainingDistanceToScroll":I
    goto :goto_0

    .line 893
    .end local v2    # "remainingDistanceToScroll":I
    :cond_1
    move v2, v1

    .line 898
    .restart local v2    # "remainingDistanceToScroll":I
    :goto_0
    if-gez v2, :cond_2

    invoke-virtual {p0}, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->canChildScrollUp()Z

    move-result v3

    if-nez v3, :cond_2

    .line 899
    iget v3, v8, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mTotalUnconsumed:F

    invoke-static {v2}, Ljava/lang/Math;->abs(I)I

    move-result v4

    int-to-float v4, v4

    add-float/2addr v3, v4

    iput v3, v8, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mTotalUnconsumed:F

    .line 900
    invoke-direct {p0, v3}, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->moveSpinner(F)V

    .line 904
    aget v3, p7, v9

    add-int/2addr v3, v1

    aput v3, p7, v9

    .line 906
    :cond_2
    return-void
.end method

.method public onNestedScrollAccepted(Landroid/view/View;Landroid/view/View;I)V
    .locals 1
    .param p1, "child"    # Landroid/view/View;
    .param p2, "target"    # Landroid/view/View;
    .param p3, "axes"    # I

    .line 964
    iget-object v0, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mNestedScrollingParentHelper:Landroidx/core/view/NestedScrollingParentHelper;

    invoke-virtual {v0, p1, p2, p3}, Landroidx/core/view/NestedScrollingParentHelper;->onNestedScrollAccepted(Landroid/view/View;Landroid/view/View;I)V

    .line 966
    and-int/lit8 v0, p3, 0x2

    invoke-virtual {p0, v0}, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->startNestedScroll(I)Z

    .line 967
    const/4 v0, 0x0

    iput v0, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mTotalUnconsumed:F

    .line 968
    const/4 v0, 0x1

    iput-boolean v0, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mNestedScrollInProgress:Z

    .line 969
    return-void
.end method

.method public onNestedScrollAccepted(Landroid/view/View;Landroid/view/View;II)V
    .locals 0
    .param p1, "child"    # Landroid/view/View;
    .param p2, "target"    # Landroid/view/View;
    .param p3, "axes"    # I
    .param p4, "type"    # I

    .line 923
    if-nez p4, :cond_0

    .line 924
    invoke-virtual {p0, p1, p2, p3}, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->onNestedScrollAccepted(Landroid/view/View;Landroid/view/View;I)V

    .line 926
    :cond_0
    return-void
.end method

.method protected onRestoreInstanceState(Landroid/os/Parcelable;)V
    .locals 2
    .param p1, "state"    # Landroid/os/Parcelable;

    .line 287
    move-object v0, p1

    check-cast v0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout$SavedState;

    .line 288
    .local v0, "savedState":Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout$SavedState;
    invoke-virtual {v0}, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout$SavedState;->getSuperState()Landroid/os/Parcelable;

    move-result-object v1

    invoke-super {p0, v1}, Landroid/view/ViewGroup;->onRestoreInstanceState(Landroid/os/Parcelable;)V

    .line 289
    iget-boolean v1, v0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout$SavedState;->mRefreshing:Z

    invoke-virtual {p0, v1}, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->setRefreshing(Z)V

    .line 290
    return-void
.end method

.method protected onSaveInstanceState()Landroid/os/Parcelable;
    .locals 3

    .line 281
    invoke-super {p0}, Landroid/view/ViewGroup;->onSaveInstanceState()Landroid/os/Parcelable;

    move-result-object v0

    .line 282
    .local v0, "superState":Landroid/os/Parcelable;
    new-instance v1, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout$SavedState;

    iget-boolean v2, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mRefreshing:Z

    invoke-direct {v1, v0, v2}, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout$SavedState;-><init>(Landroid/os/Parcelable;Z)V

    return-object v1
.end method

.method public onStartNestedScroll(Landroid/view/View;Landroid/view/View;I)Z
    .locals 1
    .param p1, "child"    # Landroid/view/View;
    .param p2, "target"    # Landroid/view/View;
    .param p3, "nestedScrollAxes"    # I

    .line 957
    invoke-virtual {p0}, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->isEnabled()Z

    move-result v0

    if-eqz v0, :cond_0

    iget-boolean v0, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mReturningToStart:Z

    if-nez v0, :cond_0

    iget-boolean v0, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mRefreshing:Z

    if-nez v0, :cond_0

    and-int/lit8 v0, p3, 0x2

    if-eqz v0, :cond_0

    const/4 v0, 0x1

    goto :goto_0

    :cond_0
    const/4 v0, 0x0

    :goto_0
    return v0
.end method

.method public onStartNestedScroll(Landroid/view/View;Landroid/view/View;II)Z
    .locals 1
    .param p1, "child"    # Landroid/view/View;
    .param p2, "target"    # Landroid/view/View;
    .param p3, "axes"    # I
    .param p4, "type"    # I

    .line 912
    if-nez p4, :cond_0

    .line 913
    invoke-virtual {p0, p1, p2, p3}, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->onStartNestedScroll(Landroid/view/View;Landroid/view/View;I)Z

    move-result v0

    return v0

    .line 915
    :cond_0
    const/4 v0, 0x0

    return v0
.end method

.method public onStopNestedScroll(Landroid/view/View;)V
    .locals 3
    .param p1, "target"    # Landroid/view/View;

    .line 1010
    iget-object v0, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mNestedScrollingParentHelper:Landroidx/core/view/NestedScrollingParentHelper;

    invoke-virtual {v0, p1}, Landroidx/core/view/NestedScrollingParentHelper;->onStopNestedScroll(Landroid/view/View;)V

    .line 1011
    const/4 v0, 0x0

    iput-boolean v0, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mNestedScrollInProgress:Z

    .line 1014
    iget v0, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mTotalUnconsumed:F

    const/4 v1, 0x0

    cmpl-float v2, v0, v1

    if-lez v2, :cond_0

    .line 1015
    invoke-direct {p0, v0}, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->finishSpinner(F)V

    .line 1016
    iput v1, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mTotalUnconsumed:F

    .line 1019
    :cond_0
    invoke-virtual {p0}, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->stopNestedScroll()V

    .line 1020
    return-void
.end method

.method public onStopNestedScroll(Landroid/view/View;I)V
    .locals 0
    .param p1, "target"    # Landroid/view/View;
    .param p2, "type"    # I

    .line 932
    if-nez p2, :cond_0

    .line 933
    invoke-virtual {p0, p1}, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->onStopNestedScroll(Landroid/view/View;)V

    .line 935
    :cond_0
    return-void
.end method

.method public onTouchEvent(Landroid/view/MotionEvent;)Z
    .locals 7
    .param p1, "ev"    # Landroid/view/MotionEvent;

    .line 1227
    invoke-virtual {p1}, Landroid/view/MotionEvent;->getActionMasked()I

    move-result v0

    .line 1228
    .local v0, "action":I
    const/4 v1, -0x1

    .line 1230
    .local v1, "pointerIndex":I
    iget-boolean v2, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mReturningToStart:Z

    const/4 v3, 0x0

    if-eqz v2, :cond_0

    if-nez v0, :cond_0

    .line 1231
    iput-boolean v3, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mReturningToStart:Z

    .line 1234
    :cond_0
    invoke-virtual {p0}, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->isEnabled()Z

    move-result v2

    if-eqz v2, :cond_8

    iget-boolean v2, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mReturningToStart:Z

    if-nez v2, :cond_8

    invoke-virtual {p0}, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->canChildScrollUp()Z

    move-result v2

    if-nez v2, :cond_8

    iget-boolean v2, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mRefreshing:Z

    if-nez v2, :cond_8

    iget-boolean v2, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mNestedScrollInProgress:Z

    if-eqz v2, :cond_1

    goto/16 :goto_1

    .line 1240
    :cond_1
    const/high16 v2, 0x3f000000    # 0.5f

    const/4 v4, 0x1

    packed-switch v0, :pswitch_data_0

    :pswitch_0
    goto/16 :goto_0

    .line 1281
    :pswitch_1
    invoke-direct {p0, p1}, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->onSecondaryPointerUp(Landroid/view/MotionEvent;)V

    .line 1282
    goto/16 :goto_0

    .line 1270
    :pswitch_2
    invoke-virtual {p1}, Landroid/view/MotionEvent;->getActionIndex()I

    move-result v1

    .line 1271
    if-gez v1, :cond_2

    .line 1272
    sget-object v2, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->LOG_TAG:Ljava/lang/String;

    const-string v4, "Got ACTION_POINTER_DOWN event but have an invalid action index."

    invoke-static {v2, v4}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;)I

    .line 1274
    return v3

    .line 1276
    :cond_2
    invoke-virtual {p1, v1}, Landroid/view/MotionEvent;->getPointerId(I)I

    move-result v2

    iput v2, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mActivePointerId:I

    .line 1277
    goto :goto_0

    .line 1301
    :pswitch_3
    return v3

    .line 1247
    :pswitch_4
    iget v5, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mActivePointerId:I

    invoke-virtual {p1, v5}, Landroid/view/MotionEvent;->findPointerIndex(I)I

    move-result v1

    .line 1248
    if-gez v1, :cond_3

    .line 1249
    sget-object v2, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->LOG_TAG:Ljava/lang/String;

    const-string v4, "Got ACTION_MOVE event but have an invalid active pointer id."

    invoke-static {v2, v4}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;)I

    .line 1250
    return v3

    .line 1253
    :cond_3
    invoke-virtual {p1, v1}, Landroid/view/MotionEvent;->getY(I)F

    move-result v5

    .line 1254
    .local v5, "y":F
    invoke-direct {p0, v5}, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->startDragging(F)V

    .line 1256
    iget-boolean v6, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mIsBeingDragged:Z

    if-eqz v6, :cond_7

    .line 1257
    iget v6, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mInitialMotionY:F

    sub-float v6, v5, v6

    mul-float v6, v6, v2

    .line 1258
    .local v6, "overscrollTop":F
    const/4 v2, 0x0

    cmpl-float v2, v6, v2

    if-lez v2, :cond_4

    .line 1261
    invoke-virtual {p0}, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->getParent()Landroid/view/ViewParent;

    move-result-object v2

    invoke-interface {v2, v4}, Landroid/view/ViewParent;->requestDisallowInterceptTouchEvent(Z)V

    .line 1262
    invoke-direct {p0, v6}, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->moveSpinner(F)V

    .line 1266
    .end local v6    # "overscrollTop":F
    goto :goto_0

    .line 1264
    .restart local v6    # "overscrollTop":F
    :cond_4
    return v3

    .line 1285
    .end local v5    # "y":F
    .end local v6    # "overscrollTop":F
    :pswitch_5
    iget v4, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mActivePointerId:I

    invoke-virtual {p1, v4}, Landroid/view/MotionEvent;->findPointerIndex(I)I

    move-result v1

    .line 1286
    if-gez v1, :cond_5

    .line 1287
    sget-object v2, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->LOG_TAG:Ljava/lang/String;

    const-string v4, "Got ACTION_UP event but don\'t have an active pointer id."

    invoke-static {v2, v4}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;)I

    .line 1288
    return v3

    .line 1291
    :cond_5
    iget-boolean v4, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mIsBeingDragged:Z

    if-eqz v4, :cond_6

    .line 1292
    invoke-virtual {p1, v1}, Landroid/view/MotionEvent;->getY(I)F

    move-result v4

    .line 1293
    .local v4, "y":F
    iget v5, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mInitialMotionY:F

    sub-float v5, v4, v5

    mul-float v5, v5, v2

    .line 1294
    .local v5, "overscrollTop":F
    iput-boolean v3, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mIsBeingDragged:Z

    .line 1295
    invoke-direct {p0, v5}, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->finishSpinner(F)V

    .line 1297
    .end local v4    # "y":F
    .end local v5    # "overscrollTop":F
    :cond_6
    const/4 v2, -0x1

    iput v2, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mActivePointerId:I

    .line 1298
    return v3

    .line 1242
    :pswitch_6
    invoke-virtual {p1, v3}, Landroid/view/MotionEvent;->getPointerId(I)I

    move-result v2

    iput v2, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mActivePointerId:I

    .line 1243
    iput-boolean v3, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mIsBeingDragged:Z

    .line 1244
    nop

    .line 1304
    :cond_7
    :goto_0
    return v4

    .line 1237
    :cond_8
    :goto_1
    return v3

    nop

    :pswitch_data_0
    .packed-switch 0x0
        :pswitch_6
        :pswitch_5
        :pswitch_4
        :pswitch_3
        :pswitch_0
        :pswitch_2
        :pswitch_1
    .end packed-switch
.end method

.method public requestDisallowInterceptTouchEvent(Z)V
    .locals 2
    .param p1, "b"    # Z

    .line 843
    sget v0, Landroid/os/Build$VERSION;->SDK_INT:I

    const/16 v1, 0x15

    if-ge v0, v1, :cond_0

    iget-object v0, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mTarget:Landroid/view/View;

    instance-of v0, v0, Landroid/widget/AbsListView;

    if-nez v0, :cond_1

    :cond_0
    iget-object v0, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mTarget:Landroid/view/View;

    if-eqz v0, :cond_4

    .line 844
    invoke-static {v0}, Landroidx/core/view/ViewCompat;->isNestedScrollingEnabled(Landroid/view/View;)Z

    move-result v0

    if-nez v0, :cond_4

    .line 845
    :cond_1
    iget-boolean v0, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mEnableLegacyRequestDisallowInterceptTouch:Z

    if-eqz v0, :cond_2

    goto :goto_0

    .line 849
    :cond_2
    invoke-virtual {p0}, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->getParent()Landroid/view/ViewParent;

    move-result-object v0

    .line 850
    .local v0, "parent":Landroid/view/ViewParent;
    if-eqz v0, :cond_3

    .line 851
    invoke-interface {v0, p1}, Landroid/view/ViewParent;->requestDisallowInterceptTouchEvent(Z)V

    .line 853
    .end local v0    # "parent":Landroid/view/ViewParent;
    :cond_3
    goto :goto_0

    .line 855
    :cond_4
    invoke-super {p0, p1}, Landroid/view/ViewGroup;->requestDisallowInterceptTouchEvent(Z)V

    .line 857
    :goto_0
    return-void
.end method

.method reset()V
    .locals 2

    .line 219
    iget-object v0, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mCircleView:Landroidx/swiperefreshlayout/widget/CircleImageView;

    invoke-virtual {v0}, Landroidx/swiperefreshlayout/widget/CircleImageView;->clearAnimation()V

    .line 220
    iget-object v0, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mProgress:Landroidx/swiperefreshlayout/widget/CircularProgressDrawable;

    invoke-virtual {v0}, Landroidx/swiperefreshlayout/widget/CircularProgressDrawable;->stop()V

    .line 221
    iget-object v0, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mCircleView:Landroidx/swiperefreshlayout/widget/CircleImageView;

    const/16 v1, 0x8

    invoke-virtual {v0, v1}, Landroidx/swiperefreshlayout/widget/CircleImageView;->setVisibility(I)V

    .line 222
    const/16 v0, 0xff

    invoke-direct {p0, v0}, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->setColorViewAlpha(I)V

    .line 224
    iget-boolean v0, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mScale:Z

    if-eqz v0, :cond_0

    .line 225
    const/4 v0, 0x0

    invoke-virtual {p0, v0}, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->setAnimationProgress(F)V

    goto :goto_0

    .line 227
    :cond_0
    iget v0, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mOriginalOffsetTop:I

    iget v1, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mCurrentTargetOffsetTop:I

    sub-int/2addr v0, v1

    invoke-virtual {p0, v0}, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->setTargetOffsetTopAndBottom(I)V

    .line 229
    :goto_0
    iget-object v0, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mCircleView:Landroidx/swiperefreshlayout/widget/CircleImageView;

    invoke-virtual {v0}, Landroidx/swiperefreshlayout/widget/CircleImageView;->getTop()I

    move-result v0

    iput v0, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mCurrentTargetOffsetTop:I

    .line 230
    return-void
.end method

.method setAnimationProgress(F)V
    .locals 1
    .param p1, "progress"    # F

    .line 524
    iget-object v0, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mCircleView:Landroidx/swiperefreshlayout/widget/CircleImageView;

    invoke-virtual {v0, p1}, Landroidx/swiperefreshlayout/widget/CircleImageView;->setScaleX(F)V

    .line 525
    iget-object v0, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mCircleView:Landroidx/swiperefreshlayout/widget/CircleImageView;

    invoke-virtual {v0, p1}, Landroidx/swiperefreshlayout/widget/CircleImageView;->setScaleY(F)V

    .line 526
    return-void
.end method

.method public varargs setColorScheme([I)V
    .locals 0
    .param p1, "colors"    # [I
    .annotation runtime Ljava/lang/Deprecated;
    .end annotation

    .line 609
    invoke-virtual {p0, p1}, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->setColorSchemeResources([I)V

    .line 610
    return-void
.end method

.method public varargs setColorSchemeColors([I)V
    .locals 1
    .param p1, "colors"    # [I

    .line 636
    invoke-direct {p0}, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->ensureTarget()V

    .line 637
    iget-object v0, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mProgress:Landroidx/swiperefreshlayout/widget/CircularProgressDrawable;

    invoke-virtual {v0, p1}, Landroidx/swiperefreshlayout/widget/CircularProgressDrawable;->setColorSchemeColors([I)V

    .line 638
    return-void
.end method

.method public varargs setColorSchemeResources([I)V
    .locals 4
    .param p1, "colorResIds"    # [I

    .line 620
    invoke-virtual {p0}, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->getContext()Landroid/content/Context;

    move-result-object v0

    .line 621
    .local v0, "context":Landroid/content/Context;
    array-length v1, p1

    new-array v1, v1, [I

    .line 622
    .local v1, "colorRes":[I
    const/4 v2, 0x0

    .local v2, "i":I
    :goto_0
    array-length v3, p1

    if-ge v2, v3, :cond_0

    .line 623
    aget v3, p1, v2

    invoke-static {v0, v3}, Landroidx/core/content/ContextCompat;->getColor(Landroid/content/Context;I)I

    move-result v3

    aput v3, v1, v2

    .line 622
    add-int/lit8 v2, v2, 0x1

    goto :goto_0

    .line 625
    .end local v2    # "i":I
    :cond_0
    invoke-virtual {p0, v1}, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->setColorSchemeColors([I)V

    .line 626
    return-void
.end method

.method public setDistanceToTriggerSync(I)V
    .locals 1
    .param p1, "distance"    # I

    .line 668
    int-to-float v0, p1

    iput v0, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mTotalDragDistance:F

    .line 669
    return-void
.end method

.method public setEnabled(Z)V
    .locals 0
    .param p1, "enabled"    # Z

    .line 234
    invoke-super {p0, p1}, Landroid/view/ViewGroup;->setEnabled(Z)V

    .line 235
    if-nez p1, :cond_0

    .line 236
    invoke-virtual {p0}, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->reset()V

    .line 238
    :cond_0
    return-void
.end method

.method public setLegacyRequestDisallowInterceptTouchEventEnabled(Z)V
    .locals 0
    .param p1, "enabled"    # Z
    .annotation runtime Ljava/lang/Deprecated;
    .end annotation

    .line 835
    iput-boolean p1, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mEnableLegacyRequestDisallowInterceptTouch:Z

    .line 836
    return-void
.end method

.method public setNestedScrollingEnabled(Z)V
    .locals 1
    .param p1, "enabled"    # Z

    .line 1090
    iget-object v0, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mNestedScrollingChildHelper:Landroidx/core/view/NestedScrollingChildHelper;

    invoke-virtual {v0, p1}, Landroidx/core/view/NestedScrollingChildHelper;->setNestedScrollingEnabled(Z)V

    .line 1091
    return-void
.end method

.method public setOnChildScrollUpCallback(Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout$OnChildScrollUpCallback;)V
    .locals 0
    .param p1, "callback"    # Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout$OnChildScrollUpCallback;

    .line 751
    iput-object p1, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mChildScrollUpCallback:Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout$OnChildScrollUpCallback;

    .line 752
    return-void
.end method

.method public setOnRefreshListener(Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout$OnRefreshListener;)V
    .locals 0
    .param p1, "listener"    # Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout$OnRefreshListener;

    .line 475
    iput-object p1, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mListener:Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout$OnRefreshListener;

    .line 476
    return-void
.end method

.method public setProgressBackgroundColor(I)V
    .locals 0
    .param p1, "colorRes"    # I
    .annotation runtime Ljava/lang/Deprecated;
    .end annotation

    .line 583
    invoke-virtual {p0, p1}, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->setProgressBackgroundColorSchemeResource(I)V

    .line 584
    return-void
.end method

.method public setProgressBackgroundColorSchemeColor(I)V
    .locals 1
    .param p1, "color"    # I

    .line 601
    iget-object v0, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mCircleView:Landroidx/swiperefreshlayout/widget/CircleImageView;

    invoke-virtual {v0, p1}, Landroidx/swiperefreshlayout/widget/CircleImageView;->setBackgroundColor(I)V

    .line 602
    return-void
.end method

.method public setProgressBackgroundColorSchemeResource(I)V
    .locals 1
    .param p1, "colorRes"    # I

    .line 592
    invoke-virtual {p0}, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->getContext()Landroid/content/Context;

    move-result-object v0

    invoke-static {v0, p1}, Landroidx/core/content/ContextCompat;->getColor(Landroid/content/Context;I)I

    move-result v0

    invoke-virtual {p0, v0}, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->setProgressBackgroundColorSchemeColor(I)V

    .line 593
    return-void
.end method

.method public setProgressViewEndTarget(ZI)V
    .locals 1
    .param p1, "scale"    # Z
    .param p2, "end"    # I

    .line 361
    iput p2, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mSpinnerOffsetEnd:I

    .line 362
    iput-boolean p1, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mScale:Z

    .line 363
    iget-object v0, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mCircleView:Landroidx/swiperefreshlayout/widget/CircleImageView;

    invoke-virtual {v0}, Landroidx/swiperefreshlayout/widget/CircleImageView;->invalidate()V

    .line 364
    return-void
.end method

.method public setProgressViewOffset(ZII)V
    .locals 1
    .param p1, "scale"    # Z
    .param p2, "start"    # I
    .param p3, "end"    # I

    .line 323
    iput-boolean p1, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mScale:Z

    .line 324
    iput p2, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mOriginalOffsetTop:I

    .line 325
    iput p3, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mSpinnerOffsetEnd:I

    .line 326
    const/4 v0, 0x1

    iput-boolean v0, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mUsingCustomStart:Z

    .line 327
    invoke-virtual {p0}, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->reset()V

    .line 328
    const/4 v0, 0x0

    iput-boolean v0, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mRefreshing:Z

    .line 329
    return-void
.end method

.method public setRefreshing(Z)V
    .locals 4
    .param p1, "refreshing"    # Z

    .line 485
    const/4 v0, 0x0

    if-eqz p1, :cond_1

    iget-boolean v1, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mRefreshing:Z

    if-eq v1, p1, :cond_1

    .line 487
    iput-boolean p1, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mRefreshing:Z

    .line 488
    const/4 v1, 0x0

    .line 489
    .local v1, "endTarget":I
    iget-boolean v2, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mUsingCustomStart:Z

    if-nez v2, :cond_0

    .line 490
    iget v2, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mSpinnerOffsetEnd:I

    iget v3, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mOriginalOffsetTop:I

    add-int/2addr v2, v3

    .end local v1    # "endTarget":I
    .local v2, "endTarget":I
    goto :goto_0

    .line 492
    .end local v2    # "endTarget":I
    .restart local v1    # "endTarget":I
    :cond_0
    iget v2, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mSpinnerOffsetEnd:I

    .line 494
    .end local v1    # "endTarget":I
    .restart local v2    # "endTarget":I
    :goto_0
    iget v1, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mCurrentTargetOffsetTop:I

    sub-int v1, v2, v1

    invoke-virtual {p0, v1}, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->setTargetOffsetTopAndBottom(I)V

    .line 495
    iput-boolean v0, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mNotify:Z

    .line 496
    iget-object v0, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mRefreshListener:Landroid/view/animation/Animation$AnimationListener;

    invoke-direct {p0, v0}, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->startScaleUpAnimation(Landroid/view/animation/Animation$AnimationListener;)V

    .line 497
    .end local v2    # "endTarget":I
    goto :goto_1

    .line 498
    :cond_1
    invoke-direct {p0, p1, v0}, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->setRefreshing(ZZ)V

    .line 500
    :goto_1
    return-void
.end method

.method public setSize(I)V
    .locals 3
    .param p1, "size"    # I

    .line 381
    if-eqz p1, :cond_0

    const/4 v0, 0x1

    if-eq p1, v0, :cond_0

    .line 382
    return-void

    .line 384
    :cond_0
    invoke-virtual {p0}, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->getResources()Landroid/content/res/Resources;

    move-result-object v0

    invoke-virtual {v0}, Landroid/content/res/Resources;->getDisplayMetrics()Landroid/util/DisplayMetrics;

    move-result-object v0

    .line 385
    .local v0, "metrics":Landroid/util/DisplayMetrics;
    if-nez p1, :cond_1

    .line 386
    const/high16 v1, 0x42600000    # 56.0f

    iget v2, v0, Landroid/util/DisplayMetrics;->density:F

    mul-float v2, v2, v1

    float-to-int v1, v2

    iput v1, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mCircleDiameter:I

    goto :goto_0

    .line 388
    :cond_1
    const/high16 v1, 0x42200000    # 40.0f

    iget v2, v0, Landroid/util/DisplayMetrics;->density:F

    mul-float v2, v2, v1

    float-to-int v1, v2

    iput v1, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mCircleDiameter:I

    .line 393
    :goto_0
    iget-object v1, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mCircleView:Landroidx/swiperefreshlayout/widget/CircleImageView;

    const/4 v2, 0x0

    invoke-virtual {v1, v2}, Landroidx/swiperefreshlayout/widget/CircleImageView;->setImageDrawable(Landroid/graphics/drawable/Drawable;)V

    .line 394
    iget-object v1, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mProgress:Landroidx/swiperefreshlayout/widget/CircularProgressDrawable;

    invoke-virtual {v1, p1}, Landroidx/swiperefreshlayout/widget/CircularProgressDrawable;->setStyle(I)V

    .line 395
    iget-object v1, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mCircleView:Landroidx/swiperefreshlayout/widget/CircleImageView;

    iget-object v2, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mProgress:Landroidx/swiperefreshlayout/widget/CircularProgressDrawable;

    invoke-virtual {v1, v2}, Landroidx/swiperefreshlayout/widget/CircleImageView;->setImageDrawable(Landroid/graphics/drawable/Drawable;)V

    .line 396
    return-void
.end method

.method public setSlingshotDistance(I)V
    .locals 0
    .param p1, "slingshotDistance"    # I

    .line 374
    iput p1, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mCustomSlingshotDistance:I

    .line 375
    return-void
.end method

.method setTargetOffsetTopAndBottom(I)V
    .locals 1
    .param p1, "offset"    # I

    .line 1395
    iget-object v0, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mCircleView:Landroidx/swiperefreshlayout/widget/CircleImageView;

    invoke-virtual {v0}, Landroidx/swiperefreshlayout/widget/CircleImageView;->bringToFront()V

    .line 1396
    iget-object v0, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mCircleView:Landroidx/swiperefreshlayout/widget/CircleImageView;

    invoke-static {v0, p1}, Landroidx/core/view/ViewCompat;->offsetTopAndBottom(Landroid/view/View;I)V

    .line 1397
    iget-object v0, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mCircleView:Landroidx/swiperefreshlayout/widget/CircleImageView;

    invoke-virtual {v0}, Landroidx/swiperefreshlayout/widget/CircleImageView;->getTop()I

    move-result v0

    iput v0, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mCurrentTargetOffsetTop:I

    .line 1398
    return-void
.end method

.method public startNestedScroll(I)Z
    .locals 1
    .param p1, "axes"    # I

    .line 1100
    iget-object v0, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mNestedScrollingChildHelper:Landroidx/core/view/NestedScrollingChildHelper;

    invoke-virtual {v0, p1}, Landroidx/core/view/NestedScrollingChildHelper;->startNestedScroll(I)Z

    move-result v0

    return v0
.end method

.method public startNestedScroll(II)Z
    .locals 1
    .param p1, "axes"    # I
    .param p2, "type"    # I

    .line 1057
    if-nez p2, :cond_0

    invoke-virtual {p0, p1}, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->startNestedScroll(I)Z

    move-result v0

    if-eqz v0, :cond_0

    const/4 v0, 0x1

    goto :goto_0

    :cond_0
    const/4 v0, 0x0

    :goto_0
    return v0
.end method

.method startScaleDownAnimation(Landroid/view/animation/Animation$AnimationListener;)V
    .locals 3
    .param p1, "listener"    # Landroid/view/animation/Animation$AnimationListener;

    .line 542
    new-instance v0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout$3;

    invoke-direct {v0, p0}, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout$3;-><init>(Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;)V

    iput-object v0, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mScaleDownAnimation:Landroid/view/animation/Animation;

    .line 548
    const-wide/16 v1, 0x96

    invoke-virtual {v0, v1, v2}, Landroid/view/animation/Animation;->setDuration(J)V

    .line 549
    iget-object v0, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mCircleView:Landroidx/swiperefreshlayout/widget/CircleImageView;

    invoke-virtual {v0, p1}, Landroidx/swiperefreshlayout/widget/CircleImageView;->setAnimationListener(Landroid/view/animation/Animation$AnimationListener;)V

    .line 550
    iget-object v0, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mCircleView:Landroidx/swiperefreshlayout/widget/CircleImageView;

    invoke-virtual {v0}, Landroidx/swiperefreshlayout/widget/CircleImageView;->clearAnimation()V

    .line 551
    iget-object v0, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mCircleView:Landroidx/swiperefreshlayout/widget/CircleImageView;

    iget-object v1, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mScaleDownAnimation:Landroid/view/animation/Animation;

    invoke-virtual {v0, v1}, Landroidx/swiperefreshlayout/widget/CircleImageView;->startAnimation(Landroid/view/animation/Animation;)V

    .line 552
    return-void
.end method

.method public stopNestedScroll()V
    .locals 1

    .line 1105
    iget-object v0, p0, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->mNestedScrollingChildHelper:Landroidx/core/view/NestedScrollingChildHelper;

    invoke-virtual {v0}, Landroidx/core/view/NestedScrollingChildHelper;->stopNestedScroll()V

    .line 1106
    return-void
.end method

.method public stopNestedScroll(I)V
    .locals 0
    .param p1, "type"    # I

    .line 1062
    if-nez p1, :cond_0

    .line 1063
    invoke-virtual {p0}, Landroidx/swiperefreshlayout/widget/SwipeRefreshLayout;->stopNestedScroll()V

    .line 1065
    :cond_0
    return-void
.end method
