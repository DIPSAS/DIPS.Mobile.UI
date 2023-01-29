.class public Landroidx/slidingpanelayout/widget/SlidingPaneLayout;
.super Landroid/view/ViewGroup;
.source "SlidingPaneLayout.java"

# interfaces
.implements Landroidx/customview/widget/Openable;


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Landroidx/slidingpanelayout/widget/SlidingPaneLayout$DisableLayerRunnable;,
        Landroidx/slidingpanelayout/widget/SlidingPaneLayout$AccessibilityDelegate;,
        Landroidx/slidingpanelayout/widget/SlidingPaneLayout$SavedState;,
        Landroidx/slidingpanelayout/widget/SlidingPaneLayout$LayoutParams;,
        Landroidx/slidingpanelayout/widget/SlidingPaneLayout$DragHelperCallback;,
        Landroidx/slidingpanelayout/widget/SlidingPaneLayout$SimplePanelSlideListener;,
        Landroidx/slidingpanelayout/widget/SlidingPaneLayout$PanelSlideListener;
    }
.end annotation


# static fields
.field private static final ACCESSIBILITY_CLASS_NAME:Ljava/lang/String; = "androidx.slidingpanelayout.widget.SlidingPaneLayout"

.field private static final DEFAULT_FADE_COLOR:I = -0x33333334

.field private static final DEFAULT_OVERHANG_SIZE:I = 0x20

.field private static final MIN_FLING_VELOCITY:I = 0x190

.field private static final TAG:Ljava/lang/String; = "SlidingPaneLayout"


# instance fields
.field private mCanSlide:Z

.field private mCoveredFadeColor:I

.field private mDisplayListReflectionLoaded:Z

.field final mDragHelper:Landroidx/customview/widget/ViewDragHelper;

.field private mFirstLayout:Z

.field private mGetDisplayList:Ljava/lang/reflect/Method;

.field private mInitialMotionX:F

.field private mInitialMotionY:F

.field mIsUnableToDrag:Z

.field private final mOverhangSize:I

.field private mPanelSlideListener:Landroidx/slidingpanelayout/widget/SlidingPaneLayout$PanelSlideListener;

.field private mParallaxBy:I

.field private mParallaxOffset:F

.field final mPostedRunnables:Ljava/util/ArrayList;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/ArrayList<",
            "Landroidx/slidingpanelayout/widget/SlidingPaneLayout$DisableLayerRunnable;",
            ">;"
        }
    .end annotation
.end field

.field mPreservedOpenState:Z

.field private mRecreateDisplayList:Ljava/lang/reflect/Field;

.field private mShadowDrawableLeft:Landroid/graphics/drawable/Drawable;

.field private mShadowDrawableRight:Landroid/graphics/drawable/Drawable;

.field mSlideOffset:F

.field mSlideRange:I

.field mSlideableView:Landroid/view/View;

.field private mSliderFadeColor:I

.field private final mTmpRect:Landroid/graphics/Rect;


# direct methods
.method public constructor <init>(Landroid/content/Context;)V
    .locals 1
    .param p1, "context"    # Landroid/content/Context;

    .line 245
    const/4 v0, 0x0

    invoke-direct {p0, p1, v0}, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;-><init>(Landroid/content/Context;Landroid/util/AttributeSet;)V

    .line 246
    return-void
.end method

.method public constructor <init>(Landroid/content/Context;Landroid/util/AttributeSet;)V
    .locals 1
    .param p1, "context"    # Landroid/content/Context;
    .param p2, "attrs"    # Landroid/util/AttributeSet;

    .line 249
    const/4 v0, 0x0

    invoke-direct {p0, p1, p2, v0}, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;-><init>(Landroid/content/Context;Landroid/util/AttributeSet;I)V

    .line 250
    return-void
.end method

.method public constructor <init>(Landroid/content/Context;Landroid/util/AttributeSet;I)V
    .locals 4
    .param p1, "context"    # Landroid/content/Context;
    .param p2, "attrs"    # Landroid/util/AttributeSet;
    .param p3, "defStyle"    # I

    .line 253
    invoke-direct {p0, p1, p2, p3}, Landroid/view/ViewGroup;-><init>(Landroid/content/Context;Landroid/util/AttributeSet;I)V

    .line 113
    const v0, -0x33333334

    iput v0, p0, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->mSliderFadeColor:I

    .line 197
    const/4 v0, 0x1

    iput-boolean v0, p0, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->mFirstLayout:Z

    .line 199
    new-instance v1, Landroid/graphics/Rect;

    invoke-direct {v1}, Landroid/graphics/Rect;-><init>()V

    iput-object v1, p0, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->mTmpRect:Landroid/graphics/Rect;

    .line 201
    new-instance v1, Ljava/util/ArrayList;

    invoke-direct {v1}, Ljava/util/ArrayList;-><init>()V

    iput-object v1, p0, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->mPostedRunnables:Ljava/util/ArrayList;

    .line 255
    invoke-virtual {p1}, Landroid/content/Context;->getResources()Landroid/content/res/Resources;

    move-result-object v1

    invoke-virtual {v1}, Landroid/content/res/Resources;->getDisplayMetrics()Landroid/util/DisplayMetrics;

    move-result-object v1

    iget v1, v1, Landroid/util/DisplayMetrics;->density:F

    .line 256
    .local v1, "density":F
    const/high16 v2, 0x42000000    # 32.0f

    mul-float v2, v2, v1

    const/high16 v3, 0x3f000000    # 0.5f

    add-float/2addr v2, v3

    float-to-int v2, v2

    iput v2, p0, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->mOverhangSize:I

    .line 258
    const/4 v2, 0x0

    invoke-virtual {p0, v2}, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->setWillNotDraw(Z)V

    .line 260
    new-instance v2, Landroidx/slidingpanelayout/widget/SlidingPaneLayout$AccessibilityDelegate;

    invoke-direct {v2, p0}, Landroidx/slidingpanelayout/widget/SlidingPaneLayout$AccessibilityDelegate;-><init>(Landroidx/slidingpanelayout/widget/SlidingPaneLayout;)V

    invoke-static {p0, v2}, Landroidx/core/view/ViewCompat;->setAccessibilityDelegate(Landroid/view/View;Landroidx/core/view/AccessibilityDelegateCompat;)V

    .line 261
    invoke-static {p0, v0}, Landroidx/core/view/ViewCompat;->setImportantForAccessibility(Landroid/view/View;I)V

    .line 263
    new-instance v0, Landroidx/slidingpanelayout/widget/SlidingPaneLayout$DragHelperCallback;

    invoke-direct {v0, p0}, Landroidx/slidingpanelayout/widget/SlidingPaneLayout$DragHelperCallback;-><init>(Landroidx/slidingpanelayout/widget/SlidingPaneLayout;)V

    invoke-static {p0, v3, v0}, Landroidx/customview/widget/ViewDragHelper;->create(Landroid/view/ViewGroup;FLandroidx/customview/widget/ViewDragHelper$Callback;)Landroidx/customview/widget/ViewDragHelper;

    move-result-object v0

    iput-object v0, p0, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->mDragHelper:Landroidx/customview/widget/ViewDragHelper;

    .line 264
    const/high16 v2, 0x43c80000    # 400.0f

    mul-float v2, v2, v1

    invoke-virtual {v0, v2}, Landroidx/customview/widget/ViewDragHelper;->setMinVelocity(F)V

    .line 265
    return-void
.end method

.method private closePane(I)Z
    .locals 2
    .param p1, "initialVelocity"    # I

    .line 859
    iget-boolean v0, p0, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->mFirstLayout:Z

    const/4 v1, 0x0

    if-nez v0, :cond_1

    const/4 v0, 0x0

    invoke-virtual {p0, v0, p1}, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->smoothSlideTo(FI)Z

    move-result v0

    if-eqz v0, :cond_0

    goto :goto_0

    .line 863
    :cond_0
    return v1

    .line 860
    :cond_1
    :goto_0
    iput-boolean v1, p0, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->mPreservedOpenState:Z

    .line 861
    const/4 v0, 0x1

    return v0
.end method

.method private dimChildView(Landroid/view/View;FI)V
    .locals 7
    .param p1, "v"    # Landroid/view/View;
    .param p2, "mag"    # F
    .param p3, "fadeColor"    # I

    .line 988
    invoke-virtual {p1}, Landroid/view/View;->getLayoutParams()Landroid/view/ViewGroup$LayoutParams;

    move-result-object v0

    check-cast v0, Landroidx/slidingpanelayout/widget/SlidingPaneLayout$LayoutParams;

    .line 990
    .local v0, "lp":Landroidx/slidingpanelayout/widget/SlidingPaneLayout$LayoutParams;
    const/4 v1, 0x0

    cmpl-float v1, p2, v1

    if-lez v1, :cond_2

    if-eqz p3, :cond_2

    .line 991
    const/high16 v1, -0x1000000

    and-int/2addr v1, p3

    ushr-int/lit8 v1, v1, 0x18

    .line 992
    .local v1, "baseAlpha":I
    int-to-float v2, v1

    mul-float v2, v2, p2

    float-to-int v2, v2

    .line 993
    .local v2, "imag":I
    shl-int/lit8 v3, v2, 0x18

    const v4, 0xffffff

    and-int/2addr v4, p3

    or-int/2addr v3, v4

    .line 994
    .local v3, "color":I
    iget-object v4, v0, Landroidx/slidingpanelayout/widget/SlidingPaneLayout$LayoutParams;->dimPaint:Landroid/graphics/Paint;

    if-nez v4, :cond_0

    .line 995
    new-instance v4, Landroid/graphics/Paint;

    invoke-direct {v4}, Landroid/graphics/Paint;-><init>()V

    iput-object v4, v0, Landroidx/slidingpanelayout/widget/SlidingPaneLayout$LayoutParams;->dimPaint:Landroid/graphics/Paint;

    .line 997
    :cond_0
    iget-object v4, v0, Landroidx/slidingpanelayout/widget/SlidingPaneLayout$LayoutParams;->dimPaint:Landroid/graphics/Paint;

    new-instance v5, Landroid/graphics/PorterDuffColorFilter;

    sget-object v6, Landroid/graphics/PorterDuff$Mode;->SRC_OVER:Landroid/graphics/PorterDuff$Mode;

    invoke-direct {v5, v3, v6}, Landroid/graphics/PorterDuffColorFilter;-><init>(ILandroid/graphics/PorterDuff$Mode;)V

    invoke-virtual {v4, v5}, Landroid/graphics/Paint;->setColorFilter(Landroid/graphics/ColorFilter;)Landroid/graphics/ColorFilter;

    .line 999
    invoke-virtual {p1}, Landroid/view/View;->getLayerType()I

    move-result v4

    const/4 v5, 0x2

    if-eq v4, v5, :cond_1

    .line 1000
    iget-object v4, v0, Landroidx/slidingpanelayout/widget/SlidingPaneLayout$LayoutParams;->dimPaint:Landroid/graphics/Paint;

    invoke-virtual {p1, v5, v4}, Landroid/view/View;->setLayerType(ILandroid/graphics/Paint;)V

    .line 1002
    :cond_1
    invoke-virtual {p0, p1}, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->invalidateChildRegion(Landroid/view/View;)V

    .end local v1    # "baseAlpha":I
    .end local v2    # "imag":I
    .end local v3    # "color":I
    goto :goto_0

    .line 1003
    :cond_2
    invoke-virtual {p1}, Landroid/view/View;->getLayerType()I

    move-result v1

    if-eqz v1, :cond_4

    .line 1004
    iget-object v1, v0, Landroidx/slidingpanelayout/widget/SlidingPaneLayout$LayoutParams;->dimPaint:Landroid/graphics/Paint;

    if-eqz v1, :cond_3

    .line 1005
    iget-object v1, v0, Landroidx/slidingpanelayout/widget/SlidingPaneLayout$LayoutParams;->dimPaint:Landroid/graphics/Paint;

    const/4 v2, 0x0

    invoke-virtual {v1, v2}, Landroid/graphics/Paint;->setColorFilter(Landroid/graphics/ColorFilter;)Landroid/graphics/ColorFilter;

    .line 1007
    :cond_3
    new-instance v1, Landroidx/slidingpanelayout/widget/SlidingPaneLayout$DisableLayerRunnable;

    invoke-direct {v1, p0, p1}, Landroidx/slidingpanelayout/widget/SlidingPaneLayout$DisableLayerRunnable;-><init>(Landroidx/slidingpanelayout/widget/SlidingPaneLayout;Landroid/view/View;)V

    .line 1008
    .local v1, "dlr":Landroidx/slidingpanelayout/widget/SlidingPaneLayout$DisableLayerRunnable;
    iget-object v2, p0, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->mPostedRunnables:Ljava/util/ArrayList;

    invoke-virtual {v2, v1}, Ljava/util/ArrayList;->add(Ljava/lang/Object;)Z

    .line 1009
    invoke-static {p0, v1}, Landroidx/core/view/ViewCompat;->postOnAnimation(Landroid/view/View;Ljava/lang/Runnable;)V

    goto :goto_1

    .line 1003
    .end local v1    # "dlr":Landroidx/slidingpanelayout/widget/SlidingPaneLayout$DisableLayerRunnable;
    :cond_4
    :goto_0
    nop

    .line 1011
    :goto_1
    return-void
.end method

.method private openPane(I)Z
    .locals 1
    .param p1, "initialVelocity"    # I

    .line 867
    iget-boolean v0, p0, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->mFirstLayout:Z

    if-nez v0, :cond_1

    const/high16 v0, 0x3f800000    # 1.0f

    invoke-virtual {p0, v0, p1}, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->smoothSlideTo(FI)Z

    move-result v0

    if-eqz v0, :cond_0

    goto :goto_0

    .line 871
    :cond_0
    const/4 v0, 0x0

    return v0

    .line 868
    :cond_1
    :goto_0
    const/4 v0, 0x1

    iput-boolean v0, p0, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->mPreservedOpenState:Z

    .line 869
    return v0
.end method

.method private parallaxOtherViews(F)V
    .locals 11
    .param p1, "slideOffset"    # F

    .line 1236
    invoke-virtual {p0}, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->isLayoutRtlSupport()Z

    move-result v0

    .line 1237
    .local v0, "isLayoutRtl":Z
    iget-object v1, p0, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->mSlideableView:Landroid/view/View;

    invoke-virtual {v1}, Landroid/view/View;->getLayoutParams()Landroid/view/ViewGroup$LayoutParams;

    move-result-object v1

    check-cast v1, Landroidx/slidingpanelayout/widget/SlidingPaneLayout$LayoutParams;

    .line 1238
    .local v1, "slideLp":Landroidx/slidingpanelayout/widget/SlidingPaneLayout$LayoutParams;
    iget-boolean v2, v1, Landroidx/slidingpanelayout/widget/SlidingPaneLayout$LayoutParams;->dimWhenOffset:Z

    if-eqz v2, :cond_1

    .line 1239
    if-eqz v0, :cond_0

    iget v2, v1, Landroidx/slidingpanelayout/widget/SlidingPaneLayout$LayoutParams;->rightMargin:I

    goto :goto_0

    :cond_0
    iget v2, v1, Landroidx/slidingpanelayout/widget/SlidingPaneLayout$LayoutParams;->leftMargin:I

    :goto_0
    if-gtz v2, :cond_1

    const/4 v2, 0x1

    goto :goto_1

    :cond_1
    const/4 v2, 0x0

    .line 1240
    .local v2, "dimViews":Z
    :goto_1
    invoke-virtual {p0}, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->getChildCount()I

    move-result v3

    .line 1241
    .local v3, "childCount":I
    const/4 v4, 0x0

    .local v4, "i":I
    :goto_2
    if-ge v4, v3, :cond_6

    .line 1242
    invoke-virtual {p0, v4}, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->getChildAt(I)Landroid/view/View;

    move-result-object v5

    .line 1243
    .local v5, "v":Landroid/view/View;
    iget-object v6, p0, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->mSlideableView:Landroid/view/View;

    if-ne v5, v6, :cond_2

    goto :goto_5

    .line 1245
    :cond_2
    iget v6, p0, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->mParallaxOffset:F

    const/high16 v7, 0x3f800000    # 1.0f

    sub-float v6, v7, v6

    iget v8, p0, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->mParallaxBy:I

    int-to-float v9, v8

    mul-float v6, v6, v9

    float-to-int v6, v6

    .line 1246
    .local v6, "oldOffset":I
    iput p1, p0, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->mParallaxOffset:F

    .line 1247
    sub-float v9, v7, p1

    int-to-float v8, v8

    mul-float v9, v9, v8

    float-to-int v8, v9

    .line 1248
    .local v8, "newOffset":I
    sub-int v9, v6, v8

    .line 1250
    .local v9, "dx":I
    if-eqz v0, :cond_3

    neg-int v10, v9

    goto :goto_3

    :cond_3
    move v10, v9

    :goto_3
    invoke-virtual {v5, v10}, Landroid/view/View;->offsetLeftAndRight(I)V

    .line 1252
    if-eqz v2, :cond_5

    .line 1253
    if-eqz v0, :cond_4

    iget v10, p0, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->mParallaxOffset:F

    sub-float/2addr v10, v7

    goto :goto_4

    .line 1254
    :cond_4
    iget v10, p0, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->mParallaxOffset:F

    sub-float v10, v7, v10

    :goto_4
    iget v7, p0, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->mCoveredFadeColor:I

    .line 1253
    invoke-direct {p0, v5, v10, v7}, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->dimChildView(Landroid/view/View;FI)V

    .line 1241
    .end local v5    # "v":Landroid/view/View;
    .end local v6    # "oldOffset":I
    .end local v8    # "newOffset":I
    .end local v9    # "dx":I
    :cond_5
    :goto_5
    add-int/lit8 v4, v4, 0x1

    goto :goto_2

    .line 1257
    .end local v4    # "i":I
    :cond_6
    return-void
.end method

.method private static viewIsOpaque(Landroid/view/View;)Z
    .locals 5
    .param p0, "v"    # Landroid/view/View;

    .line 406
    invoke-virtual {p0}, Landroid/view/View;->isOpaque()Z

    move-result v0

    const/4 v1, 0x1

    if-eqz v0, :cond_0

    .line 407
    return v1

    .line 413
    :cond_0
    sget v0, Landroid/os/Build$VERSION;->SDK_INT:I

    const/16 v2, 0x12

    const/4 v3, 0x0

    if-lt v0, v2, :cond_1

    .line 414
    return v3

    .line 417
    :cond_1
    invoke-virtual {p0}, Landroid/view/View;->getBackground()Landroid/graphics/drawable/Drawable;

    move-result-object v0

    .line 418
    .local v0, "bg":Landroid/graphics/drawable/Drawable;
    if-eqz v0, :cond_3

    .line 419
    invoke-virtual {v0}, Landroid/graphics/drawable/Drawable;->getOpacity()I

    move-result v2

    const/4 v4, -0x1

    if-ne v2, v4, :cond_2

    goto :goto_0

    :cond_2
    const/4 v1, 0x0

    :goto_0
    return v1

    .line 421
    :cond_3
    return v3
.end method


# virtual methods
.method protected canScroll(Landroid/view/View;ZIII)Z
    .locals 14
    .param p1, "v"    # Landroid/view/View;
    .param p2, "checkV"    # Z
    .param p3, "dx"    # I
    .param p4, "x"    # I
    .param p5, "y"    # I

    .line 1271
    move-object v0, p1

    instance-of v1, v0, Landroid/view/ViewGroup;

    const/4 v2, 0x1

    if-eqz v1, :cond_1

    .line 1272
    move-object v1, v0

    check-cast v1, Landroid/view/ViewGroup;

    .line 1273
    .local v1, "group":Landroid/view/ViewGroup;
    invoke-virtual {p1}, Landroid/view/View;->getScrollX()I

    move-result v3

    .line 1274
    .local v3, "scrollX":I
    invoke-virtual {p1}, Landroid/view/View;->getScrollY()I

    move-result v4

    .line 1275
    .local v4, "scrollY":I
    invoke-virtual {v1}, Landroid/view/ViewGroup;->getChildCount()I

    move-result v5

    .line 1277
    .local v5, "count":I
    add-int/lit8 v6, v5, -0x1

    .local v6, "i":I
    :goto_0
    if-ltz v6, :cond_1

    .line 1280
    invoke-virtual {v1, v6}, Landroid/view/ViewGroup;->getChildAt(I)Landroid/view/View;

    move-result-object v13

    .line 1281
    .local v13, "child":Landroid/view/View;
    add-int v7, p4, v3

    invoke-virtual {v13}, Landroid/view/View;->getLeft()I

    move-result v8

    if-lt v7, v8, :cond_0

    add-int v7, p4, v3

    invoke-virtual {v13}, Landroid/view/View;->getRight()I

    move-result v8

    if-ge v7, v8, :cond_0

    add-int v7, p5, v4

    .line 1282
    invoke-virtual {v13}, Landroid/view/View;->getTop()I

    move-result v8

    if-lt v7, v8, :cond_0

    add-int v7, p5, v4

    invoke-virtual {v13}, Landroid/view/View;->getBottom()I

    move-result v8

    if-ge v7, v8, :cond_0

    const/4 v9, 0x1

    add-int v7, p4, v3

    .line 1283
    invoke-virtual {v13}, Landroid/view/View;->getLeft()I

    move-result v8

    sub-int v11, v7, v8

    add-int v7, p5, v4

    .line 1284
    invoke-virtual {v13}, Landroid/view/View;->getTop()I

    move-result v8

    sub-int v12, v7, v8

    .line 1283
    move-object v7, p0

    move-object v8, v13

    move/from16 v10, p3

    invoke-virtual/range {v7 .. v12}, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->canScroll(Landroid/view/View;ZIII)Z

    move-result v7

    if-eqz v7, :cond_0

    .line 1285
    return v2

    .line 1277
    .end local v13    # "child":Landroid/view/View;
    :cond_0
    add-int/lit8 v6, v6, -0x1

    goto :goto_0

    .line 1290
    .end local v1    # "group":Landroid/view/ViewGroup;
    .end local v3    # "scrollX":I
    .end local v4    # "scrollY":I
    .end local v5    # "count":I
    .end local v6    # "i":I
    :cond_1
    if-eqz p2, :cond_3

    invoke-virtual {p0}, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->isLayoutRtlSupport()Z

    move-result v1

    if-eqz v1, :cond_2

    move/from16 v1, p3

    move v3, v1

    goto :goto_1

    :cond_2
    move/from16 v1, p3

    neg-int v3, v1

    :goto_1
    invoke-virtual {p1, v3}, Landroid/view/View;->canScrollHorizontally(I)Z

    move-result v3

    if-eqz v3, :cond_4

    goto :goto_2

    :cond_3
    move/from16 v1, p3

    :cond_4
    const/4 v2, 0x0

    :goto_2
    return v2
.end method

.method public canSlide()Z
    .locals 1
    .annotation runtime Ljava/lang/Deprecated;
    .end annotation

    .line 945
    iget-boolean v0, p0, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->mCanSlide:Z

    return v0
.end method

.method protected checkLayoutParams(Landroid/view/ViewGroup$LayoutParams;)Z
    .locals 1
    .param p1, "p"    # Landroid/view/ViewGroup$LayoutParams;

    .line 1315
    instance-of v0, p1, Landroidx/slidingpanelayout/widget/SlidingPaneLayout$LayoutParams;

    if-eqz v0, :cond_0

    invoke-super {p0, p1}, Landroid/view/ViewGroup;->checkLayoutParams(Landroid/view/ViewGroup$LayoutParams;)Z

    move-result v0

    if-eqz v0, :cond_0

    const/4 v0, 0x1

    goto :goto_0

    :cond_0
    const/4 v0, 0x0

    :goto_0
    return v0
.end method

.method public close()V
    .locals 0

    .line 915
    invoke-virtual {p0}, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->closePane()Z

    .line 916
    return-void
.end method

.method public closePane()Z
    .locals 1

    .line 925
    const/4 v0, 0x0

    invoke-direct {p0, v0}, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->closePane(I)Z

    move-result v0

    return v0
.end method

.method public computeScroll()V
    .locals 2

    .line 1124
    iget-object v0, p0, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->mDragHelper:Landroidx/customview/widget/ViewDragHelper;

    const/4 v1, 0x1

    invoke-virtual {v0, v1}, Landroidx/customview/widget/ViewDragHelper;->continueSettling(Z)Z

    move-result v0

    if-eqz v0, :cond_1

    .line 1125
    iget-boolean v0, p0, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->mCanSlide:Z

    if-nez v0, :cond_0

    .line 1126
    iget-object v0, p0, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->mDragHelper:Landroidx/customview/widget/ViewDragHelper;

    invoke-virtual {v0}, Landroidx/customview/widget/ViewDragHelper;->abort()V

    .line 1127
    return-void

    .line 1130
    :cond_0
    invoke-static {p0}, Landroidx/core/view/ViewCompat;->postInvalidateOnAnimation(Landroid/view/View;)V

    .line 1132
    :cond_1
    return-void
.end method

.method dispatchOnPanelClosed(Landroid/view/View;)V
    .locals 1
    .param p1, "panel"    # Landroid/view/View;

    .line 342
    iget-object v0, p0, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->mPanelSlideListener:Landroidx/slidingpanelayout/widget/SlidingPaneLayout$PanelSlideListener;

    if-eqz v0, :cond_0

    .line 343
    invoke-interface {v0, p1}, Landroidx/slidingpanelayout/widget/SlidingPaneLayout$PanelSlideListener;->onPanelClosed(Landroid/view/View;)V

    .line 345
    :cond_0
    const/16 v0, 0x20

    invoke-virtual {p0, v0}, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->sendAccessibilityEvent(I)V

    .line 346
    return-void
.end method

.method dispatchOnPanelOpened(Landroid/view/View;)V
    .locals 1
    .param p1, "panel"    # Landroid/view/View;

    .line 335
    iget-object v0, p0, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->mPanelSlideListener:Landroidx/slidingpanelayout/widget/SlidingPaneLayout$PanelSlideListener;

    if-eqz v0, :cond_0

    .line 336
    invoke-interface {v0, p1}, Landroidx/slidingpanelayout/widget/SlidingPaneLayout$PanelSlideListener;->onPanelOpened(Landroid/view/View;)V

    .line 338
    :cond_0
    const/16 v0, 0x20

    invoke-virtual {p0, v0}, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->sendAccessibilityEvent(I)V

    .line 339
    return-void
.end method

.method dispatchOnPanelSlide(Landroid/view/View;)V
    .locals 2
    .param p1, "panel"    # Landroid/view/View;

    .line 329
    iget-object v0, p0, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->mPanelSlideListener:Landroidx/slidingpanelayout/widget/SlidingPaneLayout$PanelSlideListener;

    if-eqz v0, :cond_0

    .line 330
    iget v1, p0, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->mSlideOffset:F

    invoke-interface {v0, p1, v1}, Landroidx/slidingpanelayout/widget/SlidingPaneLayout$PanelSlideListener;->onPanelSlide(Landroid/view/View;F)V

    .line 332
    :cond_0
    return-void
.end method

.method public draw(Landroid/graphics/Canvas;)V
    .locals 8
    .param p1, "c"    # Landroid/graphics/Canvas;

    .line 1202
    invoke-super {p0, p1}, Landroid/view/ViewGroup;->draw(Landroid/graphics/Canvas;)V

    .line 1203
    invoke-virtual {p0}, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->isLayoutRtlSupport()Z

    move-result v0

    .line 1205
    .local v0, "isLayoutRtl":Z
    if-eqz v0, :cond_0

    .line 1206
    iget-object v1, p0, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->mShadowDrawableRight:Landroid/graphics/drawable/Drawable;

    .local v1, "shadowDrawable":Landroid/graphics/drawable/Drawable;
    goto :goto_0

    .line 1208
    .end local v1    # "shadowDrawable":Landroid/graphics/drawable/Drawable;
    :cond_0
    iget-object v1, p0, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->mShadowDrawableLeft:Landroid/graphics/drawable/Drawable;

    .line 1211
    .restart local v1    # "shadowDrawable":Landroid/graphics/drawable/Drawable;
    :goto_0
    invoke-virtual {p0}, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->getChildCount()I

    move-result v2

    const/4 v3, 0x1

    if-le v2, v3, :cond_1

    invoke-virtual {p0, v3}, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->getChildAt(I)Landroid/view/View;

    move-result-object v2

    goto :goto_1

    :cond_1
    const/4 v2, 0x0

    .line 1212
    .local v2, "shadowView":Landroid/view/View;
    :goto_1
    if-eqz v2, :cond_4

    if-nez v1, :cond_2

    goto :goto_3

    .line 1217
    :cond_2
    invoke-virtual {v2}, Landroid/view/View;->getTop()I

    move-result v3

    .line 1218
    .local v3, "top":I
    invoke-virtual {v2}, Landroid/view/View;->getBottom()I

    move-result v4

    .line 1220
    .local v4, "bottom":I
    invoke-virtual {v1}, Landroid/graphics/drawable/Drawable;->getIntrinsicWidth()I

    move-result v5

    .line 1223
    .local v5, "shadowWidth":I
    invoke-virtual {p0}, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->isLayoutRtlSupport()Z

    move-result v6

    if-eqz v6, :cond_3

    .line 1224
    invoke-virtual {v2}, Landroid/view/View;->getRight()I

    move-result v6

    .line 1225
    .local v6, "left":I
    add-int v7, v6, v5

    .local v7, "right":I
    goto :goto_2

    .line 1227
    .end local v6    # "left":I
    .end local v7    # "right":I
    :cond_3
    invoke-virtual {v2}, Landroid/view/View;->getLeft()I

    move-result v7

    .line 1228
    .restart local v7    # "right":I
    sub-int v6, v7, v5

    .line 1231
    .restart local v6    # "left":I
    :goto_2
    invoke-virtual {v1, v6, v3, v7, v4}, Landroid/graphics/drawable/Drawable;->setBounds(IIII)V

    .line 1232
    invoke-virtual {v1, p1}, Landroid/graphics/drawable/Drawable;->draw(Landroid/graphics/Canvas;)V

    .line 1233
    return-void

    .line 1214
    .end local v3    # "top":I
    .end local v4    # "bottom":I
    .end local v5    # "shadowWidth":I
    .end local v6    # "left":I
    .end local v7    # "right":I
    :cond_4
    :goto_3
    return-void
.end method

.method protected drawChild(Landroid/graphics/Canvas;Landroid/view/View;J)Z
    .locals 5
    .param p1, "canvas"    # Landroid/graphics/Canvas;
    .param p2, "child"    # Landroid/view/View;
    .param p3, "drawingTime"    # J

    .line 1015
    invoke-virtual {p2}, Landroid/view/View;->getLayoutParams()Landroid/view/ViewGroup$LayoutParams;

    move-result-object v0

    check-cast v0, Landroidx/slidingpanelayout/widget/SlidingPaneLayout$LayoutParams;

    .line 1017
    .local v0, "lp":Landroidx/slidingpanelayout/widget/SlidingPaneLayout$LayoutParams;
    invoke-virtual {p1}, Landroid/graphics/Canvas;->save()I

    move-result v1

    .line 1019
    .local v1, "save":I
    iget-boolean v2, p0, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->mCanSlide:Z

    if-eqz v2, :cond_1

    iget-boolean v2, v0, Landroidx/slidingpanelayout/widget/SlidingPaneLayout$LayoutParams;->slideable:Z

    if-nez v2, :cond_1

    iget-object v2, p0, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->mSlideableView:Landroid/view/View;

    if-eqz v2, :cond_1

    .line 1021
    iget-object v2, p0, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->mTmpRect:Landroid/graphics/Rect;

    invoke-virtual {p1, v2}, Landroid/graphics/Canvas;->getClipBounds(Landroid/graphics/Rect;)Z

    .line 1022
    invoke-virtual {p0}, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->isLayoutRtlSupport()Z

    move-result v2

    if-eqz v2, :cond_0

    .line 1023
    iget-object v2, p0, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->mTmpRect:Landroid/graphics/Rect;

    iget v3, v2, Landroid/graphics/Rect;->left:I

    iget-object v4, p0, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->mSlideableView:Landroid/view/View;

    invoke-virtual {v4}, Landroid/view/View;->getRight()I

    move-result v4

    invoke-static {v3, v4}, Ljava/lang/Math;->max(II)I

    move-result v3

    iput v3, v2, Landroid/graphics/Rect;->left:I

    goto :goto_0

    .line 1025
    :cond_0
    iget-object v2, p0, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->mTmpRect:Landroid/graphics/Rect;

    iget v3, v2, Landroid/graphics/Rect;->right:I

    iget-object v4, p0, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->mSlideableView:Landroid/view/View;

    invoke-virtual {v4}, Landroid/view/View;->getLeft()I

    move-result v4

    invoke-static {v3, v4}, Ljava/lang/Math;->min(II)I

    move-result v3

    iput v3, v2, Landroid/graphics/Rect;->right:I

    .line 1027
    :goto_0
    iget-object v2, p0, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->mTmpRect:Landroid/graphics/Rect;

    invoke-virtual {p1, v2}, Landroid/graphics/Canvas;->clipRect(Landroid/graphics/Rect;)Z

    .line 1030
    :cond_1
    invoke-super {p0, p1, p2, p3, p4}, Landroid/view/ViewGroup;->drawChild(Landroid/graphics/Canvas;Landroid/view/View;J)Z

    move-result v2

    .line 1032
    .local v2, "result":Z
    invoke-virtual {p1, v1}, Landroid/graphics/Canvas;->restoreToCount(I)V

    .line 1034
    return v2
.end method

.method protected generateDefaultLayoutParams()Landroid/view/ViewGroup$LayoutParams;
    .locals 1

    .line 1303
    new-instance v0, Landroidx/slidingpanelayout/widget/SlidingPaneLayout$LayoutParams;

    invoke-direct {v0}, Landroidx/slidingpanelayout/widget/SlidingPaneLayout$LayoutParams;-><init>()V

    return-object v0
.end method

.method public generateLayoutParams(Landroid/util/AttributeSet;)Landroid/view/ViewGroup$LayoutParams;
    .locals 2
    .param p1, "attrs"    # Landroid/util/AttributeSet;

    .line 1320
    new-instance v0, Landroidx/slidingpanelayout/widget/SlidingPaneLayout$LayoutParams;

    invoke-virtual {p0}, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->getContext()Landroid/content/Context;

    move-result-object v1

    invoke-direct {v0, v1, p1}, Landroidx/slidingpanelayout/widget/SlidingPaneLayout$LayoutParams;-><init>(Landroid/content/Context;Landroid/util/AttributeSet;)V

    return-object v0
.end method

.method protected generateLayoutParams(Landroid/view/ViewGroup$LayoutParams;)Landroid/view/ViewGroup$LayoutParams;
    .locals 2
    .param p1, "p"    # Landroid/view/ViewGroup$LayoutParams;

    .line 1308
    instance-of v0, p1, Landroid/view/ViewGroup$MarginLayoutParams;

    if-eqz v0, :cond_0

    .line 1309
    new-instance v0, Landroidx/slidingpanelayout/widget/SlidingPaneLayout$LayoutParams;

    move-object v1, p1

    check-cast v1, Landroid/view/ViewGroup$MarginLayoutParams;

    invoke-direct {v0, v1}, Landroidx/slidingpanelayout/widget/SlidingPaneLayout$LayoutParams;-><init>(Landroid/view/ViewGroup$MarginLayoutParams;)V

    goto :goto_0

    .line 1310
    :cond_0
    new-instance v0, Landroidx/slidingpanelayout/widget/SlidingPaneLayout$LayoutParams;

    invoke-direct {v0, p1}, Landroidx/slidingpanelayout/widget/SlidingPaneLayout$LayoutParams;-><init>(Landroid/view/ViewGroup$LayoutParams;)V

    .line 1308
    :goto_0
    return-object v0
.end method

.method public getCoveredFadeColor()I
    .locals 1

    .line 321
    iget v0, p0, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->mCoveredFadeColor:I

    return v0
.end method

.method public getParallaxDistance()I
    .locals 1

    .line 286
    iget v0, p0, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->mParallaxBy:I

    return v0
.end method

.method public getSliderFadeColor()I
    .locals 1

    .line 303
    iget v0, p0, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->mSliderFadeColor:I

    return v0
.end method

.method invalidateChildRegion(Landroid/view/View;)V
    .locals 6
    .param p1, "v"    # Landroid/view/View;

    .line 1042
    sget v0, Landroid/os/Build$VERSION;->SDK_INT:I

    const/16 v1, 0x11

    if-lt v0, v1, :cond_0

    .line 1043
    invoke-virtual {p1}, Landroid/view/View;->getLayoutParams()Landroid/view/ViewGroup$LayoutParams;

    move-result-object v0

    check-cast v0, Landroidx/slidingpanelayout/widget/SlidingPaneLayout$LayoutParams;

    iget-object v0, v0, Landroidx/slidingpanelayout/widget/SlidingPaneLayout$LayoutParams;->dimPaint:Landroid/graphics/Paint;

    invoke-static {p1, v0}, Landroidx/core/view/ViewCompat;->setLayerPaint(Landroid/view/View;Landroid/graphics/Paint;)V

    .line 1044
    return-void

    .line 1047
    :cond_0
    sget v0, Landroid/os/Build$VERSION;->SDK_INT:I

    const/16 v1, 0x10

    if-lt v0, v1, :cond_4

    .line 1054
    iget-boolean v0, p0, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->mDisplayListReflectionLoaded:Z

    const/4 v1, 0x0

    const/4 v2, 0x1

    const-string v3, "SlidingPaneLayout"

    if-nez v0, :cond_1

    .line 1056
    :try_start_0
    const-class v0, Landroid/view/View;

    const-string v4, "getDisplayList"

    move-object v5, v1

    check-cast v5, [Ljava/lang/Class;

    invoke-virtual {v0, v4, v1}, Ljava/lang/Class;->getDeclaredMethod(Ljava/lang/String;[Ljava/lang/Class;)Ljava/lang/reflect/Method;

    move-result-object v0

    iput-object v0, p0, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->mGetDisplayList:Ljava/lang/reflect/Method;
    :try_end_0
    .catch Ljava/lang/NoSuchMethodException; {:try_start_0 .. :try_end_0} :catch_0

    .line 1061
    goto :goto_0

    .line 1058
    :catch_0
    move-exception v0

    .line 1059
    .local v0, "e":Ljava/lang/NoSuchMethodException;
    const-string v4, "Couldn\'t fetch getDisplayList method; dimming won\'t work right."

    invoke-static {v3, v4, v0}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)I

    .line 1063
    .end local v0    # "e":Ljava/lang/NoSuchMethodException;
    :goto_0
    :try_start_1
    const-class v0, Landroid/view/View;

    const-string v4, "mRecreateDisplayList"

    invoke-virtual {v0, v4}, Ljava/lang/Class;->getDeclaredField(Ljava/lang/String;)Ljava/lang/reflect/Field;

    move-result-object v0

    iput-object v0, p0, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->mRecreateDisplayList:Ljava/lang/reflect/Field;

    .line 1064
    invoke-virtual {v0, v2}, Ljava/lang/reflect/Field;->setAccessible(Z)V
    :try_end_1
    .catch Ljava/lang/NoSuchFieldException; {:try_start_1 .. :try_end_1} :catch_1

    .line 1068
    goto :goto_1

    .line 1065
    :catch_1
    move-exception v0

    .line 1066
    .local v0, "e":Ljava/lang/NoSuchFieldException;
    const-string v4, "Couldn\'t fetch mRecreateDisplayList field; dimming will be slow."

    invoke-static {v3, v4, v0}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)I

    .line 1069
    .end local v0    # "e":Ljava/lang/NoSuchFieldException;
    :goto_1
    iput-boolean v2, p0, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->mDisplayListReflectionLoaded:Z

    .line 1071
    :cond_1
    iget-object v0, p0, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->mGetDisplayList:Ljava/lang/reflect/Method;

    if-eqz v0, :cond_3

    iget-object v0, p0, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->mRecreateDisplayList:Ljava/lang/reflect/Field;

    if-nez v0, :cond_2

    goto :goto_2

    .line 1078
    :cond_2
    :try_start_2
    invoke-virtual {v0, p1, v2}, Ljava/lang/reflect/Field;->setBoolean(Ljava/lang/Object;Z)V

    .line 1079
    iget-object v0, p0, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->mGetDisplayList:Ljava/lang/reflect/Method;

    move-object v2, v1

    check-cast v2, [Ljava/lang/Object;

    invoke-virtual {v0, p1, v1}, Ljava/lang/reflect/Method;->invoke(Ljava/lang/Object;[Ljava/lang/Object;)Ljava/lang/Object;
    :try_end_2
    .catch Ljava/lang/Exception; {:try_start_2 .. :try_end_2} :catch_2

    .line 1082
    goto :goto_3

    .line 1080
    :catch_2
    move-exception v0

    .line 1081
    .local v0, "e":Ljava/lang/Exception;
    const-string v1, "Error refreshing display list state"

    invoke-static {v3, v1, v0}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)I

    goto :goto_3

    .line 1073
    .end local v0    # "e":Ljava/lang/Exception;
    :cond_3
    :goto_2
    invoke-virtual {p1}, Landroid/view/View;->invalidate()V

    .line 1074
    return-void

    .line 1085
    :cond_4
    :goto_3
    invoke-virtual {p1}, Landroid/view/View;->getLeft()I

    move-result v0

    invoke-virtual {p1}, Landroid/view/View;->getTop()I

    move-result v1

    invoke-virtual {p1}, Landroid/view/View;->getRight()I

    move-result v2

    .line 1086
    invoke-virtual {p1}, Landroid/view/View;->getBottom()I

    move-result v3

    .line 1085
    invoke-static {p0, v0, v1, v2, v3}, Landroidx/core/view/ViewCompat;->postInvalidateOnAnimation(Landroid/view/View;IIII)V

    .line 1087
    return-void
.end method

.method isDimmed(Landroid/view/View;)Z
    .locals 4
    .param p1, "child"    # Landroid/view/View;

    .line 1294
    const/4 v0, 0x0

    if-nez p1, :cond_0

    .line 1295
    return v0

    .line 1297
    :cond_0
    invoke-virtual {p1}, Landroid/view/View;->getLayoutParams()Landroid/view/ViewGroup$LayoutParams;

    move-result-object v1

    check-cast v1, Landroidx/slidingpanelayout/widget/SlidingPaneLayout$LayoutParams;

    .line 1298
    .local v1, "lp":Landroidx/slidingpanelayout/widget/SlidingPaneLayout$LayoutParams;
    iget-boolean v2, p0, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->mCanSlide:Z

    if-eqz v2, :cond_1

    iget-boolean v2, v1, Landroidx/slidingpanelayout/widget/SlidingPaneLayout$LayoutParams;->dimWhenOffset:Z

    if-eqz v2, :cond_1

    iget v2, p0, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->mSlideOffset:F

    const/4 v3, 0x0

    cmpl-float v2, v2, v3

    if-lez v2, :cond_1

    const/4 v0, 0x1

    :cond_1
    return v0
.end method

.method isLayoutRtlSupport()Z
    .locals 2

    .line 1641
    invoke-static {p0}, Landroidx/core/view/ViewCompat;->getLayoutDirection(Landroid/view/View;)I

    move-result v0

    const/4 v1, 0x1

    if-ne v0, v1, :cond_0

    goto :goto_0

    :cond_0
    const/4 v1, 0x0

    :goto_0
    return v1
.end method

.method public isOpen()Z
    .locals 2

    .line 936
    iget-boolean v0, p0, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->mCanSlide:Z

    if-eqz v0, :cond_1

    iget v0, p0, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->mSlideOffset:F

    const/high16 v1, 0x3f800000    # 1.0f

    cmpl-float v0, v0, v1

    if-nez v0, :cond_0

    goto :goto_0

    :cond_0
    const/4 v0, 0x0

    goto :goto_1

    :cond_1
    :goto_0
    const/4 v0, 0x1

    :goto_1
    return v0
.end method

.method public isSlideable()Z
    .locals 1

    .line 955
    iget-boolean v0, p0, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->mCanSlide:Z

    return v0
.end method

.method protected onAttachedToWindow()V
    .locals 1

    .line 426
    invoke-super {p0}, Landroid/view/ViewGroup;->onAttachedToWindow()V

    .line 427
    const/4 v0, 0x1

    iput-boolean v0, p0, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->mFirstLayout:Z

    .line 428
    return-void
.end method

.method protected onDetachedFromWindow()V
    .locals 3

    .line 432
    invoke-super {p0}, Landroid/view/ViewGroup;->onDetachedFromWindow()V

    .line 433
    const/4 v0, 0x1

    iput-boolean v0, p0, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->mFirstLayout:Z

    .line 435
    const/4 v0, 0x0

    .local v0, "i":I
    iget-object v1, p0, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->mPostedRunnables:Ljava/util/ArrayList;

    invoke-virtual {v1}, Ljava/util/ArrayList;->size()I

    move-result v1

    .local v1, "count":I
    :goto_0
    if-ge v0, v1, :cond_0

    .line 436
    iget-object v2, p0, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->mPostedRunnables:Ljava/util/ArrayList;

    invoke-virtual {v2, v0}, Ljava/util/ArrayList;->get(I)Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Landroidx/slidingpanelayout/widget/SlidingPaneLayout$DisableLayerRunnable;

    .line 437
    .local v2, "dlr":Landroidx/slidingpanelayout/widget/SlidingPaneLayout$DisableLayerRunnable;
    invoke-virtual {v2}, Landroidx/slidingpanelayout/widget/SlidingPaneLayout$DisableLayerRunnable;->run()V

    .line 435
    .end local v2    # "dlr":Landroidx/slidingpanelayout/widget/SlidingPaneLayout$DisableLayerRunnable;
    add-int/lit8 v0, v0, 0x1

    goto :goto_0

    .line 439
    .end local v0    # "i":I
    .end local v1    # "count":I
    :cond_0
    iget-object v0, p0, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->mPostedRunnables:Ljava/util/ArrayList;

    invoke-virtual {v0}, Ljava/util/ArrayList;->clear()V

    .line 440
    return-void
.end method

.method public onInterceptTouchEvent(Landroid/view/MotionEvent;)Z
    .locals 10
    .param p1, "ev"    # Landroid/view/MotionEvent;

    .line 760
    invoke-virtual {p1}, Landroid/view/MotionEvent;->getActionMasked()I

    move-result v0

    .line 763
    .local v0, "action":I
    iget-boolean v1, p0, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->mCanSlide:Z

    const/4 v2, 0x1

    if-nez v1, :cond_0

    if-nez v0, :cond_0

    invoke-virtual {p0}, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->getChildCount()I

    move-result v1

    if-le v1, v2, :cond_0

    .line 765
    invoke-virtual {p0, v2}, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->getChildAt(I)Landroid/view/View;

    move-result-object v1

    .line 766
    .local v1, "secondChild":Landroid/view/View;
    if-eqz v1, :cond_0

    .line 767
    iget-object v3, p0, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->mDragHelper:Landroidx/customview/widget/ViewDragHelper;

    .line 768
    invoke-virtual {p1}, Landroid/view/MotionEvent;->getX()F

    move-result v4

    float-to-int v4, v4

    invoke-virtual {p1}, Landroid/view/MotionEvent;->getY()F

    move-result v5

    float-to-int v5, v5

    .line 767
    invoke-virtual {v3, v1, v4, v5}, Landroidx/customview/widget/ViewDragHelper;->isViewUnder(Landroid/view/View;II)Z

    move-result v3

    xor-int/2addr v3, v2

    iput-boolean v3, p0, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->mPreservedOpenState:Z

    .line 772
    .end local v1    # "secondChild":Landroid/view/View;
    :cond_0
    iget-boolean v1, p0, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->mCanSlide:Z

    if-eqz v1, :cond_7

    iget-boolean v1, p0, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->mIsUnableToDrag:Z

    if-eqz v1, :cond_1

    if-eqz v0, :cond_1

    goto/16 :goto_3

    .line 777
    :cond_1
    const/4 v1, 0x3

    const/4 v3, 0x0

    if-eq v0, v1, :cond_6

    if-ne v0, v2, :cond_2

    goto :goto_2

    .line 782
    :cond_2
    const/4 v1, 0x0

    .line 784
    .local v1, "interceptTap":Z
    packed-switch v0, :pswitch_data_0

    :pswitch_0
    goto :goto_0

    .line 800
    :pswitch_1
    invoke-virtual {p1}, Landroid/view/MotionEvent;->getX()F

    move-result v4

    .line 801
    .local v4, "x":F
    invoke-virtual {p1}, Landroid/view/MotionEvent;->getY()F

    move-result v5

    .line 802
    .local v5, "y":F
    iget v6, p0, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->mInitialMotionX:F

    sub-float v6, v4, v6

    invoke-static {v6}, Ljava/lang/Math;->abs(F)F

    move-result v6

    .line 803
    .local v6, "adx":F
    iget v7, p0, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->mInitialMotionY:F

    sub-float v7, v5, v7

    invoke-static {v7}, Ljava/lang/Math;->abs(F)F

    move-result v7

    .line 804
    .local v7, "ady":F
    iget-object v8, p0, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->mDragHelper:Landroidx/customview/widget/ViewDragHelper;

    invoke-virtual {v8}, Landroidx/customview/widget/ViewDragHelper;->getTouchSlop()I

    move-result v8

    .line 805
    .local v8, "slop":I
    int-to-float v9, v8

    cmpl-float v9, v6, v9

    if-lez v9, :cond_3

    cmpl-float v9, v7, v6

    if-lez v9, :cond_3

    .line 806
    iget-object v9, p0, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->mDragHelper:Landroidx/customview/widget/ViewDragHelper;

    invoke-virtual {v9}, Landroidx/customview/widget/ViewDragHelper;->cancel()V

    .line 807
    iput-boolean v2, p0, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->mIsUnableToDrag:Z

    .line 808
    return v3

    .line 786
    .end local v4    # "x":F
    .end local v5    # "y":F
    .end local v6    # "adx":F
    .end local v7    # "ady":F
    .end local v8    # "slop":I
    :pswitch_2
    iput-boolean v3, p0, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->mIsUnableToDrag:Z

    .line 787
    invoke-virtual {p1}, Landroid/view/MotionEvent;->getX()F

    move-result v4

    .line 788
    .restart local v4    # "x":F
    invoke-virtual {p1}, Landroid/view/MotionEvent;->getY()F

    move-result v5

    .line 789
    .restart local v5    # "y":F
    iput v4, p0, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->mInitialMotionX:F

    .line 790
    iput v5, p0, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->mInitialMotionY:F

    .line 792
    iget-object v6, p0, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->mDragHelper:Landroidx/customview/widget/ViewDragHelper;

    iget-object v7, p0, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->mSlideableView:Landroid/view/View;

    float-to-int v8, v4

    float-to-int v9, v5

    invoke-virtual {v6, v7, v8, v9}, Landroidx/customview/widget/ViewDragHelper;->isViewUnder(Landroid/view/View;II)Z

    move-result v6

    if-eqz v6, :cond_3

    iget-object v6, p0, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->mSlideableView:Landroid/view/View;

    .line 793
    invoke-virtual {p0, v6}, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->isDimmed(Landroid/view/View;)Z

    move-result v6

    if-eqz v6, :cond_3

    .line 794
    const/4 v1, 0x1

    .line 813
    .end local v4    # "x":F
    .end local v5    # "y":F
    :cond_3
    :goto_0
    iget-object v4, p0, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->mDragHelper:Landroidx/customview/widget/ViewDragHelper;

    invoke-virtual {v4, p1}, Landroidx/customview/widget/ViewDragHelper;->shouldInterceptTouchEvent(Landroid/view/MotionEvent;)Z

    move-result v4

    .line 815
    .local v4, "interceptForDrag":Z
    if-nez v4, :cond_5

    if-eqz v1, :cond_4

    goto :goto_1

    :cond_4
    const/4 v2, 0x0

    :cond_5
    :goto_1
    return v2

    .line 778
    .end local v1    # "interceptTap":Z
    .end local v4    # "interceptForDrag":Z
    :cond_6
    :goto_2
    iget-object v1, p0, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->mDragHelper:Landroidx/customview/widget/ViewDragHelper;

    invoke-virtual {v1}, Landroidx/customview/widget/ViewDragHelper;->cancel()V

    .line 779
    return v3

    .line 773
    :cond_7
    :goto_3
    iget-object v1, p0, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->mDragHelper:Landroidx/customview/widget/ViewDragHelper;

    invoke-virtual {v1}, Landroidx/customview/widget/ViewDragHelper;->cancel()V

    .line 774
    invoke-super {p0, p1}, Landroid/view/ViewGroup;->onInterceptTouchEvent(Landroid/view/MotionEvent;)Z

    move-result v1

    return v1

    nop

    :pswitch_data_0
    .packed-switch 0x0
        :pswitch_2
        :pswitch_0
        :pswitch_1
    .end packed-switch
.end method

.method protected onLayout(ZIIII)V
    .locals 21
    .param p1, "changed"    # Z
    .param p2, "l"    # I
    .param p3, "t"    # I
    .param p4, "r"    # I
    .param p5, "b"    # I

    .line 656
    move-object/from16 v0, p0

    invoke-virtual/range {p0 .. p0}, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->isLayoutRtlSupport()Z

    move-result v1

    .line 657
    .local v1, "isLayoutRtl":Z
    const/4 v2, 0x1

    if-eqz v1, :cond_0

    .line 658
    iget-object v3, v0, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->mDragHelper:Landroidx/customview/widget/ViewDragHelper;

    const/4 v4, 0x2

    invoke-virtual {v3, v4}, Landroidx/customview/widget/ViewDragHelper;->setEdgeTrackingEnabled(I)V

    goto :goto_0

    .line 660
    :cond_0
    iget-object v3, v0, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->mDragHelper:Landroidx/customview/widget/ViewDragHelper;

    invoke-virtual {v3, v2}, Landroidx/customview/widget/ViewDragHelper;->setEdgeTrackingEnabled(I)V

    .line 662
    :goto_0
    sub-int v3, p4, p2

    .line 663
    .local v3, "width":I
    if-eqz v1, :cond_1

    invoke-virtual/range {p0 .. p0}, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->getPaddingRight()I

    move-result v4

    goto :goto_1

    :cond_1
    invoke-virtual/range {p0 .. p0}, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->getPaddingLeft()I

    move-result v4

    .line 664
    .local v4, "paddingStart":I
    :goto_1
    if-eqz v1, :cond_2

    invoke-virtual/range {p0 .. p0}, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->getPaddingLeft()I

    move-result v5

    goto :goto_2

    :cond_2
    invoke-virtual/range {p0 .. p0}, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->getPaddingRight()I

    move-result v5

    .line 665
    .local v5, "paddingEnd":I
    :goto_2
    invoke-virtual/range {p0 .. p0}, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->getPaddingTop()I

    move-result v6

    .line 667
    .local v6, "paddingTop":I
    invoke-virtual/range {p0 .. p0}, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->getChildCount()I

    move-result v7

    .line 668
    .local v7, "childCount":I
    move v8, v4

    .line 669
    .local v8, "xStart":I
    move v9, v8

    .line 671
    .local v9, "nextXStart":I
    iget-boolean v10, v0, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->mFirstLayout:Z

    if-eqz v10, :cond_4

    .line 672
    iget-boolean v10, v0, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->mCanSlide:Z

    if-eqz v10, :cond_3

    iget-boolean v10, v0, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->mPreservedOpenState:Z

    if-eqz v10, :cond_3

    const/high16 v10, 0x3f800000    # 1.0f

    goto :goto_3

    :cond_3
    const/4 v10, 0x0

    :goto_3
    iput v10, v0, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->mSlideOffset:F

    .line 675
    :cond_4
    const/4 v10, 0x0

    .local v10, "i":I
    :goto_4
    if-ge v10, v7, :cond_b

    .line 676
    invoke-virtual {v0, v10}, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->getChildAt(I)Landroid/view/View;

    move-result-object v14

    .line 678
    .local v14, "child":Landroid/view/View;
    invoke-virtual {v14}, Landroid/view/View;->getVisibility()I

    move-result v15

    const/16 v2, 0x8

    if-ne v15, v2, :cond_5

    .line 679
    move/from16 v20, v4

    const/high16 v11, 0x3f800000    # 1.0f

    goto/16 :goto_9

    .line 682
    :cond_5
    invoke-virtual {v14}, Landroid/view/View;->getLayoutParams()Landroid/view/ViewGroup$LayoutParams;

    move-result-object v2

    check-cast v2, Landroidx/slidingpanelayout/widget/SlidingPaneLayout$LayoutParams;

    .line 684
    .local v2, "lp":Landroidx/slidingpanelayout/widget/SlidingPaneLayout$LayoutParams;
    invoke-virtual {v14}, Landroid/view/View;->getMeasuredWidth()I

    move-result v15

    .line 685
    .local v15, "childWidth":I
    const/16 v16, 0x0

    .line 687
    .local v16, "offset":I
    iget-boolean v13, v2, Landroidx/slidingpanelayout/widget/SlidingPaneLayout$LayoutParams;->slideable:Z

    if-eqz v13, :cond_8

    .line 688
    iget v13, v2, Landroidx/slidingpanelayout/widget/SlidingPaneLayout$LayoutParams;->leftMargin:I

    iget v12, v2, Landroidx/slidingpanelayout/widget/SlidingPaneLayout$LayoutParams;->rightMargin:I

    add-int/2addr v13, v12

    .line 689
    .local v13, "margin":I
    sub-int v12, v3, v5

    iget v11, v0, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->mOverhangSize:I

    sub-int/2addr v12, v11

    invoke-static {v9, v12}, Ljava/lang/Math;->min(II)I

    move-result v11

    sub-int/2addr v11, v8

    sub-int/2addr v11, v13

    .line 691
    .local v11, "range":I
    iput v11, v0, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->mSlideRange:I

    .line 692
    if-eqz v1, :cond_6

    iget v12, v2, Landroidx/slidingpanelayout/widget/SlidingPaneLayout$LayoutParams;->rightMargin:I

    goto :goto_5

    :cond_6
    iget v12, v2, Landroidx/slidingpanelayout/widget/SlidingPaneLayout$LayoutParams;->leftMargin:I

    .line 693
    .local v12, "lpMargin":I
    :goto_5
    add-int v18, v8, v12

    add-int v18, v18, v11

    div-int/lit8 v19, v15, 0x2

    move/from16 v20, v4

    .end local v4    # "paddingStart":I
    .local v20, "paddingStart":I
    add-int v4, v18, v19

    move/from16 v18, v13

    .end local v13    # "margin":I
    .local v18, "margin":I
    sub-int v13, v3, v5

    if-le v4, v13, :cond_7

    const/4 v13, 0x1

    goto :goto_6

    :cond_7
    const/4 v13, 0x0

    :goto_6
    iput-boolean v13, v2, Landroidx/slidingpanelayout/widget/SlidingPaneLayout$LayoutParams;->dimWhenOffset:Z

    .line 694
    int-to-float v4, v11

    iget v13, v0, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->mSlideOffset:F

    mul-float v4, v4, v13

    float-to-int v4, v4

    .line 695
    .local v4, "pos":I
    add-int v13, v4, v12

    add-int/2addr v8, v13

    .line 696
    int-to-float v13, v4

    move-object/from16 v19, v2

    .end local v2    # "lp":Landroidx/slidingpanelayout/widget/SlidingPaneLayout$LayoutParams;
    .local v19, "lp":Landroidx/slidingpanelayout/widget/SlidingPaneLayout$LayoutParams;
    iget v2, v0, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->mSlideRange:I

    int-to-float v2, v2

    div-float/2addr v13, v2

    iput v13, v0, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->mSlideOffset:F

    .line 697
    .end local v4    # "pos":I
    .end local v11    # "range":I
    .end local v12    # "lpMargin":I
    .end local v18    # "margin":I
    const/high16 v11, 0x3f800000    # 1.0f

    goto :goto_7

    .end local v19    # "lp":Landroidx/slidingpanelayout/widget/SlidingPaneLayout$LayoutParams;
    .end local v20    # "paddingStart":I
    .restart local v2    # "lp":Landroidx/slidingpanelayout/widget/SlidingPaneLayout$LayoutParams;
    .local v4, "paddingStart":I
    :cond_8
    move-object/from16 v19, v2

    move/from16 v20, v4

    .end local v2    # "lp":Landroidx/slidingpanelayout/widget/SlidingPaneLayout$LayoutParams;
    .end local v4    # "paddingStart":I
    .restart local v19    # "lp":Landroidx/slidingpanelayout/widget/SlidingPaneLayout$LayoutParams;
    .restart local v20    # "paddingStart":I
    iget-boolean v2, v0, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->mCanSlide:Z

    if-eqz v2, :cond_9

    iget v2, v0, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->mParallaxBy:I

    if-eqz v2, :cond_9

    .line 698
    iget v4, v0, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->mSlideOffset:F

    const/high16 v11, 0x3f800000    # 1.0f

    sub-float v4, v11, v4

    int-to-float v2, v2

    mul-float v4, v4, v2

    float-to-int v2, v4

    .line 699
    .end local v16    # "offset":I
    .local v2, "offset":I
    move v8, v9

    move/from16 v16, v2

    goto :goto_7

    .line 697
    .end local v2    # "offset":I
    .restart local v16    # "offset":I
    :cond_9
    const/high16 v11, 0x3f800000    # 1.0f

    .line 701
    move v8, v9

    .line 706
    :goto_7
    if-eqz v1, :cond_a

    .line 707
    sub-int v2, v3, v8

    add-int v2, v2, v16

    .line 708
    .local v2, "childRight":I
    sub-int v4, v2, v15

    .local v4, "childLeft":I
    goto :goto_8

    .line 710
    .end local v2    # "childRight":I
    .end local v4    # "childLeft":I
    :cond_a
    sub-int v4, v8, v16

    .line 711
    .restart local v4    # "childLeft":I
    add-int v2, v4, v15

    .line 714
    .restart local v2    # "childRight":I
    :goto_8
    move v12, v6

    .line 715
    .local v12, "childTop":I
    invoke-virtual {v14}, Landroid/view/View;->getMeasuredHeight()I

    move-result v13

    add-int/2addr v13, v12

    .line 716
    .local v13, "childBottom":I
    invoke-virtual {v14, v4, v6, v2, v13}, Landroid/view/View;->layout(IIII)V

    .line 718
    invoke-virtual {v14}, Landroid/view/View;->getWidth()I

    move-result v17

    add-int v9, v9, v17

    .line 675
    .end local v2    # "childRight":I
    .end local v4    # "childLeft":I
    .end local v12    # "childTop":I
    .end local v13    # "childBottom":I
    .end local v14    # "child":Landroid/view/View;
    .end local v15    # "childWidth":I
    .end local v16    # "offset":I
    .end local v19    # "lp":Landroidx/slidingpanelayout/widget/SlidingPaneLayout$LayoutParams;
    :goto_9
    add-int/lit8 v10, v10, 0x1

    move/from16 v4, v20

    const/4 v2, 0x1

    goto/16 :goto_4

    .end local v20    # "paddingStart":I
    .local v4, "paddingStart":I
    :cond_b
    move/from16 v20, v4

    .line 721
    .end local v4    # "paddingStart":I
    .end local v10    # "i":I
    .restart local v20    # "paddingStart":I
    iget-boolean v2, v0, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->mFirstLayout:Z

    if-eqz v2, :cond_f

    .line 722
    iget-boolean v2, v0, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->mCanSlide:Z

    if-eqz v2, :cond_d

    .line 723
    iget v2, v0, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->mParallaxBy:I

    if-eqz v2, :cond_c

    .line 724
    iget v2, v0, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->mSlideOffset:F

    invoke-direct {v0, v2}, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->parallaxOtherViews(F)V

    .line 726
    :cond_c
    iget-object v2, v0, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->mSlideableView:Landroid/view/View;

    invoke-virtual {v2}, Landroid/view/View;->getLayoutParams()Landroid/view/ViewGroup$LayoutParams;

    move-result-object v2

    check-cast v2, Landroidx/slidingpanelayout/widget/SlidingPaneLayout$LayoutParams;

    iget-boolean v2, v2, Landroidx/slidingpanelayout/widget/SlidingPaneLayout$LayoutParams;->dimWhenOffset:Z

    if-eqz v2, :cond_e

    .line 727
    iget-object v2, v0, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->mSlideableView:Landroid/view/View;

    iget v4, v0, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->mSlideOffset:F

    iget v10, v0, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->mSliderFadeColor:I

    invoke-direct {v0, v2, v4, v10}, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->dimChildView(Landroid/view/View;FI)V

    goto :goto_b

    .line 731
    :cond_d
    const/4 v2, 0x0

    .local v2, "i":I
    :goto_a
    if-ge v2, v7, :cond_e

    .line 732
    invoke-virtual {v0, v2}, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->getChildAt(I)Landroid/view/View;

    move-result-object v4

    iget v10, v0, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->mSliderFadeColor:I

    const/4 v11, 0x0

    invoke-direct {v0, v4, v11, v10}, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->dimChildView(Landroid/view/View;FI)V

    .line 731
    add-int/lit8 v2, v2, 0x1

    goto :goto_a

    .line 735
    .end local v2    # "i":I
    :cond_e
    :goto_b
    iget-object v2, v0, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->mSlideableView:Landroid/view/View;

    invoke-virtual {v0, v2}, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->updateObscuredViewsVisibility(Landroid/view/View;)V

    .line 738
    :cond_f
    const/4 v2, 0x0

    iput-boolean v2, v0, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->mFirstLayout:Z

    .line 739
    return-void
.end method

.method protected onMeasure(II)V
    .locals 29
    .param p1, "widthMeasureSpec"    # I
    .param p2, "heightMeasureSpec"    # I

    .line 444
    move-object/from16 v0, p0

    invoke-static/range {p1 .. p1}, Landroid/view/View$MeasureSpec;->getMode(I)I

    move-result v1

    .line 445
    .local v1, "widthMode":I
    invoke-static/range {p1 .. p1}, Landroid/view/View$MeasureSpec;->getSize(I)I

    move-result v2

    .line 446
    .local v2, "widthSize":I
    invoke-static/range {p2 .. p2}, Landroid/view/View$MeasureSpec;->getMode(I)I

    move-result v3

    .line 447
    .local v3, "heightMode":I
    invoke-static/range {p2 .. p2}, Landroid/view/View$MeasureSpec;->getSize(I)I

    move-result v4

    .line 449
    .local v4, "heightSize":I
    const/high16 v5, -0x80000000

    const/high16 v6, 0x40000000    # 2.0f

    if-eq v1, v6, :cond_2

    .line 450
    invoke-virtual/range {p0 .. p0}, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->isInEditMode()Z

    move-result v7

    if-eqz v7, :cond_1

    .line 455
    if-ne v1, v5, :cond_0

    .line 456
    const/high16 v1, 0x40000000    # 2.0f

    goto :goto_0

    .line 457
    :cond_0
    if-nez v1, :cond_4

    .line 458
    const/high16 v1, 0x40000000    # 2.0f

    .line 459
    const/16 v2, 0x12c

    goto :goto_0

    .line 462
    :cond_1
    new-instance v5, Ljava/lang/IllegalStateException;

    const-string v6, "Width must have an exact value or MATCH_PARENT"

    invoke-direct {v5, v6}, Ljava/lang/IllegalStateException;-><init>(Ljava/lang/String;)V

    throw v5

    .line 464
    :cond_2
    if-nez v3, :cond_4

    .line 465
    invoke-virtual/range {p0 .. p0}, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->isInEditMode()Z

    move-result v7

    if-eqz v7, :cond_3

    .line 469
    if-nez v3, :cond_4

    .line 470
    const/high16 v3, -0x80000000

    .line 471
    const/16 v4, 0x12c

    goto :goto_0

    .line 474
    :cond_3
    new-instance v5, Ljava/lang/IllegalStateException;

    const-string v6, "Height must not be UNSPECIFIED"

    invoke-direct {v5, v6}, Ljava/lang/IllegalStateException;-><init>(Ljava/lang/String;)V

    throw v5

    .line 478
    :cond_4
    :goto_0
    const/4 v7, 0x0

    .line 479
    .local v7, "layoutHeight":I
    const/4 v8, 0x0

    .line 480
    .local v8, "maxLayoutHeight":I
    sparse-switch v3, :sswitch_data_0

    goto :goto_1

    .line 482
    :sswitch_0
    invoke-virtual/range {p0 .. p0}, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->getPaddingTop()I

    move-result v9

    sub-int v9, v4, v9

    invoke-virtual/range {p0 .. p0}, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->getPaddingBottom()I

    move-result v10

    sub-int/2addr v9, v10

    move v8, v9

    move v7, v9

    .line 483
    goto :goto_1

    .line 485
    :sswitch_1
    invoke-virtual/range {p0 .. p0}, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->getPaddingTop()I

    move-result v9

    sub-int v9, v4, v9

    invoke-virtual/range {p0 .. p0}, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->getPaddingBottom()I

    move-result v10

    sub-int v8, v9, v10

    .line 489
    :goto_1
    const/4 v9, 0x0

    .line 490
    .local v9, "weightSum":F
    const/4 v10, 0x0

    .line 491
    .local v10, "canSlide":Z
    invoke-virtual/range {p0 .. p0}, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->getPaddingLeft()I

    move-result v11

    sub-int v11, v2, v11

    invoke-virtual/range {p0 .. p0}, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->getPaddingRight()I

    move-result v12

    sub-int/2addr v11, v12

    .line 492
    .local v11, "widthAvailable":I
    move v12, v11

    .line 493
    .local v12, "widthRemaining":I
    invoke-virtual/range {p0 .. p0}, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->getChildCount()I

    move-result v13

    .line 495
    .local v13, "childCount":I
    const/4 v14, 0x2

    if-le v13, v14, :cond_5

    .line 496
    const-string v14, "SlidingPaneLayout"

    const-string v15, "onMeasure: More than two child views are not supported."

    invoke-static {v14, v15}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;)I

    .line 500
    :cond_5
    const/4 v14, 0x0

    iput-object v14, v0, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->mSlideableView:Landroid/view/View;

    .line 504
    const/4 v14, 0x0

    .local v14, "i":I
    :goto_2
    const/16 v15, 0x8

    const/16 v17, 0x1

    const/16 v19, 0x0

    if-ge v14, v13, :cond_f

    .line 505
    invoke-virtual {v0, v14}, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->getChildAt(I)Landroid/view/View;

    move-result-object v6

    .line 506
    .local v6, "child":Landroid/view/View;
    invoke-virtual {v6}, Landroid/view/View;->getLayoutParams()Landroid/view/ViewGroup$LayoutParams;

    move-result-object v21

    move-object/from16 v5, v21

    check-cast v5, Landroidx/slidingpanelayout/widget/SlidingPaneLayout$LayoutParams;

    .line 508
    .local v5, "lp":Landroidx/slidingpanelayout/widget/SlidingPaneLayout$LayoutParams;
    move/from16 v21, v1

    .end local v1    # "widthMode":I
    .local v21, "widthMode":I
    invoke-virtual {v6}, Landroid/view/View;->getVisibility()I

    move-result v1

    if-ne v1, v15, :cond_6

    .line 509
    const/4 v1, 0x0

    iput-boolean v1, v5, Landroidx/slidingpanelayout/widget/SlidingPaneLayout$LayoutParams;->dimWhenOffset:Z

    .line 510
    move/from16 v22, v4

    goto/16 :goto_6

    .line 513
    :cond_6
    iget v1, v5, Landroidx/slidingpanelayout/widget/SlidingPaneLayout$LayoutParams;->weight:F

    cmpl-float v1, v1, v19

    if-lez v1, :cond_7

    .line 514
    iget v1, v5, Landroidx/slidingpanelayout/widget/SlidingPaneLayout$LayoutParams;->weight:F

    add-float/2addr v9, v1

    .line 518
    iget v1, v5, Landroidx/slidingpanelayout/widget/SlidingPaneLayout$LayoutParams;->width:I

    if-nez v1, :cond_7

    move/from16 v22, v4

    goto/16 :goto_6

    .line 522
    :cond_7
    iget v1, v5, Landroidx/slidingpanelayout/widget/SlidingPaneLayout$LayoutParams;->leftMargin:I

    iget v15, v5, Landroidx/slidingpanelayout/widget/SlidingPaneLayout$LayoutParams;->rightMargin:I

    add-int/2addr v1, v15

    .line 523
    .local v1, "horizontalMargin":I
    iget v15, v5, Landroidx/slidingpanelayout/widget/SlidingPaneLayout$LayoutParams;->width:I

    move/from16 v22, v4

    const/4 v4, -0x2

    .end local v4    # "heightSize":I
    .local v22, "heightSize":I
    if-ne v15, v4, :cond_8

    .line 524
    sub-int v4, v11, v1

    const/high16 v15, -0x80000000

    invoke-static {v4, v15}, Landroid/view/View$MeasureSpec;->makeMeasureSpec(II)I

    move-result v4

    .local v4, "childWidthSpec":I
    goto :goto_3

    .line 526
    .end local v4    # "childWidthSpec":I
    :cond_8
    iget v4, v5, Landroidx/slidingpanelayout/widget/SlidingPaneLayout$LayoutParams;->width:I

    const/4 v15, -0x1

    if-ne v4, v15, :cond_9

    .line 527
    sub-int v4, v11, v1

    const/high16 v15, 0x40000000    # 2.0f

    invoke-static {v4, v15}, Landroid/view/View$MeasureSpec;->makeMeasureSpec(II)I

    move-result v4

    .restart local v4    # "childWidthSpec":I
    goto :goto_3

    .line 530
    .end local v4    # "childWidthSpec":I
    :cond_9
    const/high16 v15, 0x40000000    # 2.0f

    iget v4, v5, Landroidx/slidingpanelayout/widget/SlidingPaneLayout$LayoutParams;->width:I

    invoke-static {v4, v15}, Landroid/view/View$MeasureSpec;->makeMeasureSpec(II)I

    move-result v4

    .line 534
    .restart local v4    # "childWidthSpec":I
    :goto_3
    iget v15, v5, Landroidx/slidingpanelayout/widget/SlidingPaneLayout$LayoutParams;->height:I

    move/from16 v19, v1

    const/4 v1, -0x2

    .end local v1    # "horizontalMargin":I
    .local v19, "horizontalMargin":I
    if-ne v15, v1, :cond_a

    .line 535
    const/high16 v1, -0x80000000

    invoke-static {v8, v1}, Landroid/view/View$MeasureSpec;->makeMeasureSpec(II)I

    move-result v15

    .local v15, "childHeightSpec":I
    goto :goto_4

    .line 536
    .end local v15    # "childHeightSpec":I
    :cond_a
    iget v1, v5, Landroidx/slidingpanelayout/widget/SlidingPaneLayout$LayoutParams;->height:I

    const/4 v15, -0x1

    if-ne v1, v15, :cond_b

    .line 537
    const/high16 v1, 0x40000000    # 2.0f

    invoke-static {v8, v1}, Landroid/view/View$MeasureSpec;->makeMeasureSpec(II)I

    move-result v15

    .restart local v15    # "childHeightSpec":I
    goto :goto_4

    .line 539
    .end local v15    # "childHeightSpec":I
    :cond_b
    const/high16 v1, 0x40000000    # 2.0f

    iget v15, v5, Landroidx/slidingpanelayout/widget/SlidingPaneLayout$LayoutParams;->height:I

    invoke-static {v15, v1}, Landroid/view/View$MeasureSpec;->makeMeasureSpec(II)I

    move-result v15

    .line 542
    .restart local v15    # "childHeightSpec":I
    :goto_4
    invoke-virtual {v6, v4, v15}, Landroid/view/View;->measure(II)V

    .line 543
    invoke-virtual {v6}, Landroid/view/View;->getMeasuredWidth()I

    move-result v1

    .line 544
    .local v1, "childWidth":I
    move/from16 v18, v4

    .end local v4    # "childWidthSpec":I
    .local v18, "childWidthSpec":I
    invoke-virtual {v6}, Landroid/view/View;->getMeasuredHeight()I

    move-result v4

    .line 546
    .local v4, "childHeight":I
    move/from16 v20, v9

    const/high16 v9, -0x80000000

    .end local v9    # "weightSum":F
    .local v20, "weightSum":F
    if-ne v3, v9, :cond_c

    if-le v4, v7, :cond_c

    .line 547
    invoke-static {v4, v8}, Ljava/lang/Math;->min(II)I

    move-result v7

    .line 550
    :cond_c
    sub-int/2addr v12, v1

    .line 551
    if-gez v12, :cond_d

    const/4 v9, 0x1

    goto :goto_5

    :cond_d
    const/4 v9, 0x0

    :goto_5
    iput-boolean v9, v5, Landroidx/slidingpanelayout/widget/SlidingPaneLayout$LayoutParams;->slideable:Z

    or-int/2addr v9, v10

    .line 552
    .end local v10    # "canSlide":Z
    .local v9, "canSlide":Z
    iget-boolean v10, v5, Landroidx/slidingpanelayout/widget/SlidingPaneLayout$LayoutParams;->slideable:Z

    if-eqz v10, :cond_e

    .line 553
    iput-object v6, v0, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->mSlideableView:Landroid/view/View;

    .line 504
    .end local v1    # "childWidth":I
    .end local v4    # "childHeight":I
    .end local v5    # "lp":Landroidx/slidingpanelayout/widget/SlidingPaneLayout$LayoutParams;
    .end local v6    # "child":Landroid/view/View;
    .end local v15    # "childHeightSpec":I
    .end local v18    # "childWidthSpec":I
    .end local v19    # "horizontalMargin":I
    :cond_e
    move v10, v9

    move/from16 v9, v20

    .end local v20    # "weightSum":F
    .local v9, "weightSum":F
    .restart local v10    # "canSlide":Z
    :goto_6
    add-int/lit8 v14, v14, 0x1

    move/from16 v1, v21

    move/from16 v4, v22

    const/high16 v5, -0x80000000

    const/high16 v6, 0x40000000    # 2.0f

    goto/16 :goto_2

    .end local v21    # "widthMode":I
    .end local v22    # "heightSize":I
    .local v1, "widthMode":I
    .local v4, "heightSize":I
    :cond_f
    move/from16 v21, v1

    move/from16 v22, v4

    .line 558
    .end local v1    # "widthMode":I
    .end local v4    # "heightSize":I
    .end local v14    # "i":I
    .restart local v21    # "widthMode":I
    .restart local v22    # "heightSize":I
    if-nez v10, :cond_11

    cmpl-float v1, v9, v19

    if-lez v1, :cond_10

    goto :goto_7

    :cond_10
    move/from16 v24, v3

    move/from16 v28, v8

    move/from16 v25, v13

    goto/16 :goto_f

    .line 559
    :cond_11
    :goto_7
    iget v1, v0, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->mOverhangSize:I

    sub-int v1, v11, v1

    .line 561
    .local v1, "fixedPanelWidthLimit":I
    const/4 v4, 0x0

    .local v4, "i":I
    :goto_8
    if-ge v4, v13, :cond_23

    .line 562
    invoke-virtual {v0, v4}, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->getChildAt(I)Landroid/view/View;

    move-result-object v5

    .line 564
    .local v5, "child":Landroid/view/View;
    invoke-virtual {v5}, Landroid/view/View;->getVisibility()I

    move-result v6

    if-ne v6, v15, :cond_12

    .line 565
    move/from16 v27, v1

    move/from16 v24, v3

    move/from16 v28, v8

    move/from16 v25, v13

    const/high16 v1, 0x40000000    # 2.0f

    goto/16 :goto_e

    .line 568
    :cond_12
    invoke-virtual {v5}, Landroid/view/View;->getLayoutParams()Landroid/view/ViewGroup$LayoutParams;

    move-result-object v6

    check-cast v6, Landroidx/slidingpanelayout/widget/SlidingPaneLayout$LayoutParams;

    .line 570
    .local v6, "lp":Landroidx/slidingpanelayout/widget/SlidingPaneLayout$LayoutParams;
    invoke-virtual {v5}, Landroid/view/View;->getVisibility()I

    move-result v14

    if-ne v14, v15, :cond_13

    .line 571
    move/from16 v27, v1

    move/from16 v24, v3

    move/from16 v28, v8

    move/from16 v25, v13

    const/high16 v1, 0x40000000    # 2.0f

    goto/16 :goto_e

    .line 574
    :cond_13
    iget v14, v6, Landroidx/slidingpanelayout/widget/SlidingPaneLayout$LayoutParams;->width:I

    if-nez v14, :cond_14

    iget v14, v6, Landroidx/slidingpanelayout/widget/SlidingPaneLayout$LayoutParams;->weight:F

    cmpl-float v14, v14, v19

    if-lez v14, :cond_14

    const/4 v14, 0x1

    goto :goto_9

    :cond_14
    const/4 v14, 0x0

    .line 575
    .local v14, "skippedFirstPass":Z
    :goto_9
    if-eqz v14, :cond_15

    const/16 v23, 0x0

    goto :goto_a

    :cond_15
    invoke-virtual {v5}, Landroid/view/View;->getMeasuredWidth()I

    move-result v23

    :goto_a
    move/from16 v24, v23

    .line 576
    .local v24, "measuredWidth":I
    if-eqz v10, :cond_1c

    iget-object v15, v0, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->mSlideableView:Landroid/view/View;

    if-eq v5, v15, :cond_1c

    .line 577
    iget v15, v6, Landroidx/slidingpanelayout/widget/SlidingPaneLayout$LayoutParams;->width:I

    if-gez v15, :cond_1b

    move/from16 v15, v24

    .end local v24    # "measuredWidth":I
    .local v15, "measuredWidth":I
    if-gt v15, v1, :cond_17

    move/from16 v24, v3

    .end local v3    # "heightMode":I
    .local v24, "heightMode":I
    iget v3, v6, Landroidx/slidingpanelayout/widget/SlidingPaneLayout$LayoutParams;->weight:F

    cmpl-float v3, v3, v19

    if-lez v3, :cond_16

    goto :goto_b

    :cond_16
    move/from16 v27, v1

    move/from16 v28, v8

    move/from16 v25, v13

    const/high16 v1, 0x40000000    # 2.0f

    goto/16 :goto_e

    .end local v24    # "heightMode":I
    .restart local v3    # "heightMode":I
    :cond_17
    move/from16 v24, v3

    .line 581
    .end local v3    # "heightMode":I
    .restart local v24    # "heightMode":I
    :goto_b
    if-eqz v14, :cond_1a

    .line 584
    iget v3, v6, Landroidx/slidingpanelayout/widget/SlidingPaneLayout$LayoutParams;->height:I

    move/from16 v25, v13

    const/4 v13, -0x2

    .end local v13    # "childCount":I
    .local v25, "childCount":I
    if-ne v3, v13, :cond_18

    .line 585
    const/high16 v3, -0x80000000

    invoke-static {v8, v3}, Landroid/view/View$MeasureSpec;->makeMeasureSpec(II)I

    move-result v13

    const/high16 v3, 0x40000000    # 2.0f

    .local v13, "childHeightSpec":I
    goto :goto_c

    .line 587
    .end local v13    # "childHeightSpec":I
    :cond_18
    iget v3, v6, Landroidx/slidingpanelayout/widget/SlidingPaneLayout$LayoutParams;->height:I

    const/4 v13, -0x1

    if-ne v3, v13, :cond_19

    .line 588
    const/high16 v3, 0x40000000    # 2.0f

    invoke-static {v8, v3}, Landroid/view/View$MeasureSpec;->makeMeasureSpec(II)I

    move-result v13

    .restart local v13    # "childHeightSpec":I
    goto :goto_c

    .line 591
    .end local v13    # "childHeightSpec":I
    :cond_19
    const/high16 v3, 0x40000000    # 2.0f

    iget v13, v6, Landroidx/slidingpanelayout/widget/SlidingPaneLayout$LayoutParams;->height:I

    invoke-static {v13, v3}, Landroid/view/View$MeasureSpec;->makeMeasureSpec(II)I

    move-result v13

    .restart local v13    # "childHeightSpec":I
    goto :goto_c

    .line 595
    .end local v25    # "childCount":I
    .local v13, "childCount":I
    :cond_1a
    move/from16 v25, v13

    const/high16 v3, 0x40000000    # 2.0f

    .line 596
    .end local v13    # "childCount":I
    .restart local v25    # "childCount":I
    invoke-virtual {v5}, Landroid/view/View;->getMeasuredHeight()I

    move-result v13

    .line 595
    invoke-static {v13, v3}, Landroid/view/View$MeasureSpec;->makeMeasureSpec(II)I

    move-result v13

    .line 598
    .local v13, "childHeightSpec":I
    :goto_c
    move/from16 v26, v14

    .end local v14    # "skippedFirstPass":Z
    .local v26, "skippedFirstPass":Z
    invoke-static {v1, v3}, Landroid/view/View$MeasureSpec;->makeMeasureSpec(II)I

    move-result v14

    .line 600
    .local v14, "childWidthSpec":I
    invoke-virtual {v5, v14, v13}, Landroid/view/View;->measure(II)V

    .line 601
    .end local v13    # "childHeightSpec":I
    .end local v14    # "childWidthSpec":I
    move/from16 v27, v1

    move/from16 v28, v8

    const/high16 v1, 0x40000000    # 2.0f

    goto/16 :goto_e

    .line 577
    .end local v15    # "measuredWidth":I
    .end local v25    # "childCount":I
    .end local v26    # "skippedFirstPass":Z
    .restart local v3    # "heightMode":I
    .local v13, "childCount":I
    .local v14, "skippedFirstPass":Z
    .local v24, "measuredWidth":I
    :cond_1b
    move/from16 v25, v13

    move/from16 v26, v14

    move/from16 v15, v24

    move/from16 v24, v3

    .end local v3    # "heightMode":I
    .end local v13    # "childCount":I
    .end local v14    # "skippedFirstPass":Z
    .restart local v15    # "measuredWidth":I
    .local v24, "heightMode":I
    .restart local v25    # "childCount":I
    .restart local v26    # "skippedFirstPass":Z
    move/from16 v27, v1

    move/from16 v28, v8

    const/high16 v1, 0x40000000    # 2.0f

    goto/16 :goto_e

    .line 576
    .end local v15    # "measuredWidth":I
    .end local v25    # "childCount":I
    .end local v26    # "skippedFirstPass":Z
    .restart local v3    # "heightMode":I
    .restart local v13    # "childCount":I
    .restart local v14    # "skippedFirstPass":Z
    .local v24, "measuredWidth":I
    :cond_1c
    move/from16 v25, v13

    move/from16 v26, v14

    move/from16 v15, v24

    move/from16 v24, v3

    .line 602
    .end local v3    # "heightMode":I
    .end local v13    # "childCount":I
    .end local v14    # "skippedFirstPass":Z
    .restart local v15    # "measuredWidth":I
    .local v24, "heightMode":I
    .restart local v25    # "childCount":I
    .restart local v26    # "skippedFirstPass":Z
    iget v3, v6, Landroidx/slidingpanelayout/widget/SlidingPaneLayout$LayoutParams;->weight:F

    cmpl-float v3, v3, v19

    if-lez v3, :cond_22

    .line 604
    iget v3, v6, Landroidx/slidingpanelayout/widget/SlidingPaneLayout$LayoutParams;->width:I

    if-nez v3, :cond_1f

    .line 606
    iget v3, v6, Landroidx/slidingpanelayout/widget/SlidingPaneLayout$LayoutParams;->height:I

    const/4 v13, -0x2

    if-ne v3, v13, :cond_1d

    .line 607
    const/high16 v3, -0x80000000

    invoke-static {v8, v3}, Landroid/view/View$MeasureSpec;->makeMeasureSpec(II)I

    move-result v14

    move v3, v14

    const/high16 v14, 0x40000000    # 2.0f

    .local v14, "childHeightSpec":I
    goto :goto_d

    .line 609
    .end local v14    # "childHeightSpec":I
    :cond_1d
    const/high16 v3, -0x80000000

    iget v14, v6, Landroidx/slidingpanelayout/widget/SlidingPaneLayout$LayoutParams;->height:I

    const/4 v3, -0x1

    if-ne v14, v3, :cond_1e

    .line 610
    const/high16 v14, 0x40000000    # 2.0f

    invoke-static {v8, v14}, Landroid/view/View$MeasureSpec;->makeMeasureSpec(II)I

    move-result v16

    move/from16 v3, v16

    .local v16, "childHeightSpec":I
    goto :goto_d

    .line 613
    .end local v16    # "childHeightSpec":I
    :cond_1e
    const/high16 v14, 0x40000000    # 2.0f

    iget v3, v6, Landroidx/slidingpanelayout/widget/SlidingPaneLayout$LayoutParams;->height:I

    invoke-static {v3, v14}, Landroid/view/View$MeasureSpec;->makeMeasureSpec(II)I

    move-result v3

    .local v3, "childHeightSpec":I
    goto :goto_d

    .line 617
    .end local v3    # "childHeightSpec":I
    :cond_1f
    const/4 v13, -0x2

    const/high16 v14, 0x40000000    # 2.0f

    .line 618
    invoke-virtual {v5}, Landroid/view/View;->getMeasuredHeight()I

    move-result v3

    .line 617
    invoke-static {v3, v14}, Landroid/view/View$MeasureSpec;->makeMeasureSpec(II)I

    move-result v3

    .line 621
    .restart local v3    # "childHeightSpec":I
    :goto_d
    if-eqz v10, :cond_21

    .line 623
    iget v13, v6, Landroidx/slidingpanelayout/widget/SlidingPaneLayout$LayoutParams;->leftMargin:I

    iget v14, v6, Landroidx/slidingpanelayout/widget/SlidingPaneLayout$LayoutParams;->rightMargin:I

    add-int/2addr v13, v14

    .line 624
    .local v13, "horizontalMargin":I
    sub-int v14, v11, v13

    .line 625
    .local v14, "newWidth":I
    move/from16 v27, v1

    move/from16 v28, v8

    const/high16 v1, 0x40000000    # 2.0f

    .end local v1    # "fixedPanelWidthLimit":I
    .end local v8    # "maxLayoutHeight":I
    .local v27, "fixedPanelWidthLimit":I
    .local v28, "maxLayoutHeight":I
    invoke-static {v14, v1}, Landroid/view/View$MeasureSpec;->makeMeasureSpec(II)I

    move-result v8

    .line 627
    .local v8, "childWidthSpec":I
    if-eq v15, v14, :cond_20

    .line 628
    invoke-virtual {v5, v8, v3}, Landroid/view/View;->measure(II)V

    .line 630
    .end local v8    # "childWidthSpec":I
    .end local v13    # "horizontalMargin":I
    .end local v14    # "newWidth":I
    :cond_20
    const/high16 v1, 0x40000000    # 2.0f

    goto :goto_e

    .line 632
    .end local v27    # "fixedPanelWidthLimit":I
    .end local v28    # "maxLayoutHeight":I
    .restart local v1    # "fixedPanelWidthLimit":I
    .local v8, "maxLayoutHeight":I
    :cond_21
    move/from16 v27, v1

    move/from16 v28, v8

    .end local v1    # "fixedPanelWidthLimit":I
    .end local v8    # "maxLayoutHeight":I
    .restart local v27    # "fixedPanelWidthLimit":I
    .restart local v28    # "maxLayoutHeight":I
    const/4 v1, 0x0

    invoke-static {v1, v12}, Ljava/lang/Math;->max(II)I

    move-result v8

    .line 633
    .local v8, "widthToDistribute":I
    iget v13, v6, Landroidx/slidingpanelayout/widget/SlidingPaneLayout$LayoutParams;->weight:F

    int-to-float v14, v8

    mul-float v13, v13, v14

    div-float/2addr v13, v9

    float-to-int v13, v13

    .line 634
    .local v13, "addedWidth":I
    add-int v14, v15, v13

    const/high16 v1, 0x40000000    # 2.0f

    invoke-static {v14, v1}, Landroid/view/View$MeasureSpec;->makeMeasureSpec(II)I

    move-result v14

    .line 636
    .local v14, "childWidthSpec":I
    invoke-virtual {v5, v14, v3}, Landroid/view/View;->measure(II)V

    goto :goto_e

    .line 602
    .end local v3    # "childHeightSpec":I
    .end local v13    # "addedWidth":I
    .end local v14    # "childWidthSpec":I
    .end local v27    # "fixedPanelWidthLimit":I
    .end local v28    # "maxLayoutHeight":I
    .restart local v1    # "fixedPanelWidthLimit":I
    .local v8, "maxLayoutHeight":I
    :cond_22
    move/from16 v27, v1

    move/from16 v28, v8

    const/high16 v1, 0x40000000    # 2.0f

    .line 561
    .end local v1    # "fixedPanelWidthLimit":I
    .end local v5    # "child":Landroid/view/View;
    .end local v6    # "lp":Landroidx/slidingpanelayout/widget/SlidingPaneLayout$LayoutParams;
    .end local v8    # "maxLayoutHeight":I
    .end local v15    # "measuredWidth":I
    .end local v26    # "skippedFirstPass":Z
    .restart local v27    # "fixedPanelWidthLimit":I
    .restart local v28    # "maxLayoutHeight":I
    :goto_e
    add-int/lit8 v4, v4, 0x1

    move/from16 v3, v24

    move/from16 v13, v25

    move/from16 v1, v27

    move/from16 v8, v28

    const/16 v15, 0x8

    goto/16 :goto_8

    .end local v24    # "heightMode":I
    .end local v25    # "childCount":I
    .end local v27    # "fixedPanelWidthLimit":I
    .end local v28    # "maxLayoutHeight":I
    .restart local v1    # "fixedPanelWidthLimit":I
    .local v3, "heightMode":I
    .restart local v8    # "maxLayoutHeight":I
    .local v13, "childCount":I
    :cond_23
    move/from16 v27, v1

    move/from16 v24, v3

    move/from16 v28, v8

    move/from16 v25, v13

    .line 642
    .end local v1    # "fixedPanelWidthLimit":I
    .end local v3    # "heightMode":I
    .end local v4    # "i":I
    .end local v8    # "maxLayoutHeight":I
    .end local v13    # "childCount":I
    .restart local v24    # "heightMode":I
    .restart local v25    # "childCount":I
    .restart local v28    # "maxLayoutHeight":I
    :goto_f
    move v1, v2

    .line 643
    .local v1, "measuredWidth":I
    invoke-virtual/range {p0 .. p0}, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->getPaddingTop()I

    move-result v3

    add-int/2addr v3, v7

    invoke-virtual/range {p0 .. p0}, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->getPaddingBottom()I

    move-result v4

    add-int/2addr v3, v4

    .line 645
    .local v3, "measuredHeight":I
    invoke-virtual {v0, v1, v3}, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->setMeasuredDimension(II)V

    .line 646
    iput-boolean v10, v0, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->mCanSlide:Z

    .line 648
    iget-object v4, v0, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->mDragHelper:Landroidx/customview/widget/ViewDragHelper;

    invoke-virtual {v4}, Landroidx/customview/widget/ViewDragHelper;->getViewDragState()I

    move-result v4

    if-eqz v4, :cond_24

    if-nez v10, :cond_24

    .line 650
    iget-object v4, v0, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->mDragHelper:Landroidx/customview/widget/ViewDragHelper;

    invoke-virtual {v4}, Landroidx/customview/widget/ViewDragHelper;->abort()V

    .line 652
    :cond_24
    return-void

    :sswitch_data_0
    .sparse-switch
        -0x80000000 -> :sswitch_1
        0x40000000 -> :sswitch_0
    .end sparse-switch
.end method

.method onPanelDragged(I)V
    .locals 10
    .param p1, "newLeft"    # I

    .line 959
    iget-object v0, p0, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->mSlideableView:Landroid/view/View;

    if-nez v0, :cond_0

    .line 961
    const/4 v0, 0x0

    iput v0, p0, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->mSlideOffset:F

    .line 962
    return-void

    .line 964
    :cond_0
    invoke-virtual {p0}, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->isLayoutRtlSupport()Z

    move-result v0

    .line 965
    .local v0, "isLayoutRtl":Z
    iget-object v1, p0, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->mSlideableView:Landroid/view/View;

    invoke-virtual {v1}, Landroid/view/View;->getLayoutParams()Landroid/view/ViewGroup$LayoutParams;

    move-result-object v1

    check-cast v1, Landroidx/slidingpanelayout/widget/SlidingPaneLayout$LayoutParams;

    .line 967
    .local v1, "lp":Landroidx/slidingpanelayout/widget/SlidingPaneLayout$LayoutParams;
    iget-object v2, p0, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->mSlideableView:Landroid/view/View;

    invoke-virtual {v2}, Landroid/view/View;->getWidth()I

    move-result v2

    .line 968
    .local v2, "childWidth":I
    if-eqz v0, :cond_1

    invoke-virtual {p0}, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->getWidth()I

    move-result v3

    sub-int/2addr v3, p1

    sub-int/2addr v3, v2

    goto :goto_0

    :cond_1
    move v3, p1

    .line 970
    .local v3, "newStart":I
    :goto_0
    if-eqz v0, :cond_2

    invoke-virtual {p0}, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->getPaddingRight()I

    move-result v4

    goto :goto_1

    :cond_2
    invoke-virtual {p0}, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->getPaddingLeft()I

    move-result v4

    .line 971
    .local v4, "paddingStart":I
    :goto_1
    if-eqz v0, :cond_3

    iget v5, v1, Landroidx/slidingpanelayout/widget/SlidingPaneLayout$LayoutParams;->rightMargin:I

    goto :goto_2

    :cond_3
    iget v5, v1, Landroidx/slidingpanelayout/widget/SlidingPaneLayout$LayoutParams;->leftMargin:I

    .line 972
    .local v5, "lpMargin":I
    :goto_2
    add-int v6, v4, v5

    .line 974
    .local v6, "startBound":I
    sub-int v7, v3, v6

    int-to-float v7, v7

    iget v8, p0, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->mSlideRange:I

    int-to-float v8, v8

    div-float/2addr v7, v8

    iput v7, p0, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->mSlideOffset:F

    .line 976
    iget v8, p0, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->mParallaxBy:I

    if-eqz v8, :cond_4

    .line 977
    invoke-direct {p0, v7}, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->parallaxOtherViews(F)V

    .line 980
    :cond_4
    iget-boolean v7, v1, Landroidx/slidingpanelayout/widget/SlidingPaneLayout$LayoutParams;->dimWhenOffset:Z

    if-eqz v7, :cond_5

    .line 981
    iget-object v7, p0, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->mSlideableView:Landroid/view/View;

    iget v8, p0, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->mSlideOffset:F

    iget v9, p0, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->mSliderFadeColor:I

    invoke-direct {p0, v7, v8, v9}, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->dimChildView(Landroid/view/View;FI)V

    .line 983
    :cond_5
    iget-object v7, p0, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->mSlideableView:Landroid/view/View;

    invoke-virtual {p0, v7}, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->dispatchOnPanelSlide(Landroid/view/View;)V

    .line 984
    return-void
.end method

.method protected onRestoreInstanceState(Landroid/os/Parcelable;)V
    .locals 2
    .param p1, "state"    # Landroid/os/Parcelable;

    .line 1335
    instance-of v0, p1, Landroidx/slidingpanelayout/widget/SlidingPaneLayout$SavedState;

    if-nez v0, :cond_0

    .line 1336
    invoke-super {p0, p1}, Landroid/view/ViewGroup;->onRestoreInstanceState(Landroid/os/Parcelable;)V

    .line 1337
    return-void

    .line 1340
    :cond_0
    move-object v0, p1

    check-cast v0, Landroidx/slidingpanelayout/widget/SlidingPaneLayout$SavedState;

    .line 1341
    .local v0, "ss":Landroidx/slidingpanelayout/widget/SlidingPaneLayout$SavedState;
    invoke-virtual {v0}, Landroidx/slidingpanelayout/widget/SlidingPaneLayout$SavedState;->getSuperState()Landroid/os/Parcelable;

    move-result-object v1

    invoke-super {p0, v1}, Landroid/view/ViewGroup;->onRestoreInstanceState(Landroid/os/Parcelable;)V

    .line 1343
    iget-boolean v1, v0, Landroidx/slidingpanelayout/widget/SlidingPaneLayout$SavedState;->isOpen:Z

    if-eqz v1, :cond_1

    .line 1344
    invoke-virtual {p0}, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->openPane()Z

    goto :goto_0

    .line 1346
    :cond_1
    invoke-virtual {p0}, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->closePane()Z

    .line 1348
    :goto_0
    iget-boolean v1, v0, Landroidx/slidingpanelayout/widget/SlidingPaneLayout$SavedState;->isOpen:Z

    iput-boolean v1, p0, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->mPreservedOpenState:Z

    .line 1349
    return-void
.end method

.method protected onSaveInstanceState()Landroid/os/Parcelable;
    .locals 3

    .line 1325
    invoke-super {p0}, Landroid/view/ViewGroup;->onSaveInstanceState()Landroid/os/Parcelable;

    move-result-object v0

    .line 1327
    .local v0, "superState":Landroid/os/Parcelable;
    new-instance v1, Landroidx/slidingpanelayout/widget/SlidingPaneLayout$SavedState;

    invoke-direct {v1, v0}, Landroidx/slidingpanelayout/widget/SlidingPaneLayout$SavedState;-><init>(Landroid/os/Parcelable;)V

    .line 1328
    .local v1, "ss":Landroidx/slidingpanelayout/widget/SlidingPaneLayout$SavedState;
    invoke-virtual {p0}, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->isSlideable()Z

    move-result v2

    if-eqz v2, :cond_0

    invoke-virtual {p0}, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->isOpen()Z

    move-result v2

    goto :goto_0

    :cond_0
    iget-boolean v2, p0, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->mPreservedOpenState:Z

    :goto_0
    iput-boolean v2, v1, Landroidx/slidingpanelayout/widget/SlidingPaneLayout$SavedState;->isOpen:Z

    .line 1330
    return-object v1
.end method

.method protected onSizeChanged(IIII)V
    .locals 1
    .param p1, "w"    # I
    .param p2, "h"    # I
    .param p3, "oldw"    # I
    .param p4, "oldh"    # I

    .line 743
    invoke-super {p0, p1, p2, p3, p4}, Landroid/view/ViewGroup;->onSizeChanged(IIII)V

    .line 745
    if-eq p1, p3, :cond_0

    .line 746
    const/4 v0, 0x1

    iput-boolean v0, p0, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->mFirstLayout:Z

    .line 748
    :cond_0
    return-void
.end method

.method public onTouchEvent(Landroid/view/MotionEvent;)Z
    .locals 10
    .param p1, "ev"    # Landroid/view/MotionEvent;

    .line 820
    iget-boolean v0, p0, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->mCanSlide:Z

    if-nez v0, :cond_0

    .line 821
    invoke-super {p0, p1}, Landroid/view/ViewGroup;->onTouchEvent(Landroid/view/MotionEvent;)Z

    move-result v0

    return v0

    .line 824
    :cond_0
    iget-object v0, p0, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->mDragHelper:Landroidx/customview/widget/ViewDragHelper;

    invoke-virtual {v0, p1}, Landroidx/customview/widget/ViewDragHelper;->processTouchEvent(Landroid/view/MotionEvent;)V

    .line 826
    const/4 v0, 0x1

    .line 828
    .local v0, "wantTouchEvents":Z
    invoke-virtual {p1}, Landroid/view/MotionEvent;->getActionMasked()I

    move-result v1

    packed-switch v1, :pswitch_data_0

    goto :goto_0

    .line 838
    :pswitch_0
    iget-object v1, p0, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->mSlideableView:Landroid/view/View;

    invoke-virtual {p0, v1}, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->isDimmed(Landroid/view/View;)Z

    move-result v1

    if-eqz v1, :cond_1

    .line 839
    invoke-virtual {p1}, Landroid/view/MotionEvent;->getX()F

    move-result v1

    .line 840
    .local v1, "x":F
    invoke-virtual {p1}, Landroid/view/MotionEvent;->getY()F

    move-result v2

    .line 841
    .local v2, "y":F
    iget v3, p0, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->mInitialMotionX:F

    sub-float v3, v1, v3

    .line 842
    .local v3, "dx":F
    iget v4, p0, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->mInitialMotionY:F

    sub-float v4, v2, v4

    .line 843
    .local v4, "dy":F
    iget-object v5, p0, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->mDragHelper:Landroidx/customview/widget/ViewDragHelper;

    invoke-virtual {v5}, Landroidx/customview/widget/ViewDragHelper;->getTouchSlop()I

    move-result v5

    .line 844
    .local v5, "slop":I
    mul-float v6, v3, v3

    mul-float v7, v4, v4

    add-float/2addr v6, v7

    mul-int v7, v5, v5

    int-to-float v7, v7

    cmpg-float v6, v6, v7

    if-gez v6, :cond_1

    iget-object v6, p0, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->mDragHelper:Landroidx/customview/widget/ViewDragHelper;

    iget-object v7, p0, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->mSlideableView:Landroid/view/View;

    float-to-int v8, v1

    float-to-int v9, v2

    .line 845
    invoke-virtual {v6, v7, v8, v9}, Landroidx/customview/widget/ViewDragHelper;->isViewUnder(Landroid/view/View;II)Z

    move-result v6

    if-eqz v6, :cond_1

    .line 847
    const/4 v6, 0x0

    invoke-direct {p0, v6}, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->closePane(I)Z

    .line 848
    goto :goto_0

    .line 830
    .end local v1    # "x":F
    .end local v2    # "y":F
    .end local v3    # "dx":F
    .end local v4    # "dy":F
    .end local v5    # "slop":I
    :pswitch_1
    invoke-virtual {p1}, Landroid/view/MotionEvent;->getX()F

    move-result v1

    .line 831
    .restart local v1    # "x":F
    invoke-virtual {p1}, Landroid/view/MotionEvent;->getY()F

    move-result v2

    .line 832
    .restart local v2    # "y":F
    iput v1, p0, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->mInitialMotionX:F

    .line 833
    iput v2, p0, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->mInitialMotionY:F

    .line 834
    nop

    .line 855
    .end local v1    # "x":F
    .end local v2    # "y":F
    :cond_1
    :goto_0
    return v0

    :pswitch_data_0
    .packed-switch 0x0
        :pswitch_1
        :pswitch_0
    .end packed-switch
.end method

.method public open()V
    .locals 0

    .line 888
    invoke-virtual {p0}, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->openPane()Z

    .line 889
    return-void
.end method

.method public openPane()Z
    .locals 1

    .line 898
    const/4 v0, 0x0

    invoke-direct {p0, v0}, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->openPane(I)Z

    move-result v0

    return v0
.end method

.method public requestChildFocus(Landroid/view/View;Landroid/view/View;)V
    .locals 1
    .param p1, "child"    # Landroid/view/View;
    .param p2, "focused"    # Landroid/view/View;

    .line 752
    invoke-super {p0, p1, p2}, Landroid/view/ViewGroup;->requestChildFocus(Landroid/view/View;Landroid/view/View;)V

    .line 753
    invoke-virtual {p0}, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->isInTouchMode()Z

    move-result v0

    if-nez v0, :cond_1

    iget-boolean v0, p0, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->mCanSlide:Z

    if-nez v0, :cond_1

    .line 754
    iget-object v0, p0, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->mSlideableView:Landroid/view/View;

    if-ne p1, v0, :cond_0

    const/4 v0, 0x1

    goto :goto_0

    :cond_0
    const/4 v0, 0x0

    :goto_0
    iput-boolean v0, p0, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->mPreservedOpenState:Z

    .line 756
    :cond_1
    return-void
.end method

.method setAllChildrenVisible()V
    .locals 5

    .line 395
    const/4 v0, 0x0

    .local v0, "i":I
    invoke-virtual {p0}, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->getChildCount()I

    move-result v1

    .local v1, "childCount":I
    :goto_0
    if-ge v0, v1, :cond_1

    .line 396
    invoke-virtual {p0, v0}, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->getChildAt(I)Landroid/view/View;

    move-result-object v2

    .line 397
    .local v2, "child":Landroid/view/View;
    invoke-virtual {v2}, Landroid/view/View;->getVisibility()I

    move-result v3

    const/4 v4, 0x4

    if-ne v3, v4, :cond_0

    .line 398
    const/4 v3, 0x0

    invoke-virtual {v2, v3}, Landroid/view/View;->setVisibility(I)V

    .line 395
    .end local v2    # "child":Landroid/view/View;
    :cond_0
    add-int/lit8 v0, v0, 0x1

    goto :goto_0

    .line 401
    .end local v0    # "i":I
    .end local v1    # "childCount":I
    :cond_1
    return-void
.end method

.method public setCoveredFadeColor(I)V
    .locals 0
    .param p1, "color"    # I

    .line 313
    iput p1, p0, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->mCoveredFadeColor:I

    .line 314
    return-void
.end method

.method public setPanelSlideListener(Landroidx/slidingpanelayout/widget/SlidingPaneLayout$PanelSlideListener;)V
    .locals 0
    .param p1, "listener"    # Landroidx/slidingpanelayout/widget/SlidingPaneLayout$PanelSlideListener;

    .line 325
    iput-object p1, p0, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->mPanelSlideListener:Landroidx/slidingpanelayout/widget/SlidingPaneLayout$PanelSlideListener;

    .line 326
    return-void
.end method

.method public setParallaxDistance(I)V
    .locals 0
    .param p1, "parallaxBy"    # I

    .line 275
    iput p1, p0, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->mParallaxBy:I

    .line 276
    invoke-virtual {p0}, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->requestLayout()V

    .line 277
    return-void
.end method

.method public setShadowDrawable(Landroid/graphics/drawable/Drawable;)V
    .locals 0
    .param p1, "d"    # Landroid/graphics/drawable/Drawable;
    .annotation runtime Ljava/lang/Deprecated;
    .end annotation

    .line 1143
    invoke-virtual {p0, p1}, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->setShadowDrawableLeft(Landroid/graphics/drawable/Drawable;)V

    .line 1144
    return-void
.end method

.method public setShadowDrawableLeft(Landroid/graphics/drawable/Drawable;)V
    .locals 0
    .param p1, "d"    # Landroid/graphics/drawable/Drawable;

    .line 1153
    iput-object p1, p0, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->mShadowDrawableLeft:Landroid/graphics/drawable/Drawable;

    .line 1154
    return-void
.end method

.method public setShadowDrawableRight(Landroid/graphics/drawable/Drawable;)V
    .locals 0
    .param p1, "d"    # Landroid/graphics/drawable/Drawable;

    .line 1163
    iput-object p1, p0, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->mShadowDrawableRight:Landroid/graphics/drawable/Drawable;

    .line 1164
    return-void
.end method

.method public setShadowResource(I)V
    .locals 1
    .param p1, "resId"    # I
    .annotation runtime Ljava/lang/Deprecated;
    .end annotation

    .line 1177
    invoke-virtual {p0}, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->getResources()Landroid/content/res/Resources;

    move-result-object v0

    invoke-virtual {v0, p1}, Landroid/content/res/Resources;->getDrawable(I)Landroid/graphics/drawable/Drawable;

    move-result-object v0

    invoke-virtual {p0, v0}, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->setShadowDrawableLeft(Landroid/graphics/drawable/Drawable;)V

    .line 1178
    return-void
.end method

.method public setShadowResourceLeft(I)V
    .locals 1
    .param p1, "resId"    # I

    .line 1187
    invoke-virtual {p0}, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->getContext()Landroid/content/Context;

    move-result-object v0

    invoke-static {v0, p1}, Landroidx/core/content/ContextCompat;->getDrawable(Landroid/content/Context;I)Landroid/graphics/drawable/Drawable;

    move-result-object v0

    invoke-virtual {p0, v0}, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->setShadowDrawableLeft(Landroid/graphics/drawable/Drawable;)V

    .line 1188
    return-void
.end method

.method public setShadowResourceRight(I)V
    .locals 1
    .param p1, "resId"    # I

    .line 1197
    invoke-virtual {p0}, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->getContext()Landroid/content/Context;

    move-result-object v0

    invoke-static {v0, p1}, Landroidx/core/content/ContextCompat;->getDrawable(Landroid/content/Context;I)Landroid/graphics/drawable/Drawable;

    move-result-object v0

    invoke-virtual {p0, v0}, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->setShadowDrawableRight(Landroid/graphics/drawable/Drawable;)V

    .line 1198
    return-void
.end method

.method public setSliderFadeColor(I)V
    .locals 0
    .param p1, "color"    # I

    .line 295
    iput p1, p0, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->mSliderFadeColor:I

    .line 296
    return-void
.end method

.method public smoothSlideClosed()V
    .locals 0
    .annotation runtime Ljava/lang/Deprecated;
    .end annotation

    .line 906
    invoke-virtual {p0}, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->closePane()Z

    .line 907
    return-void
.end method

.method public smoothSlideOpen()V
    .locals 0
    .annotation runtime Ljava/lang/Deprecated;
    .end annotation

    .line 879
    invoke-virtual {p0}, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->openPane()Z

    .line 880
    return-void
.end method

.method smoothSlideTo(FI)Z
    .locals 8
    .param p1, "slideOffset"    # F
    .param p2, "velocity"    # I

    .line 1096
    iget-boolean v0, p0, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->mCanSlide:Z

    const/4 v1, 0x0

    if-nez v0, :cond_0

    .line 1098
    return v1

    .line 1101
    :cond_0
    invoke-virtual {p0}, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->isLayoutRtlSupport()Z

    move-result v0

    .line 1102
    .local v0, "isLayoutRtl":Z
    iget-object v2, p0, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->mSlideableView:Landroid/view/View;

    invoke-virtual {v2}, Landroid/view/View;->getLayoutParams()Landroid/view/ViewGroup$LayoutParams;

    move-result-object v2

    check-cast v2, Landroidx/slidingpanelayout/widget/SlidingPaneLayout$LayoutParams;

    .line 1105
    .local v2, "lp":Landroidx/slidingpanelayout/widget/SlidingPaneLayout$LayoutParams;
    if-eqz v0, :cond_1

    .line 1106
    invoke-virtual {p0}, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->getPaddingRight()I

    move-result v3

    iget v4, v2, Landroidx/slidingpanelayout/widget/SlidingPaneLayout$LayoutParams;->rightMargin:I

    add-int/2addr v3, v4

    .line 1107
    .local v3, "startBound":I
    iget-object v4, p0, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->mSlideableView:Landroid/view/View;

    invoke-virtual {v4}, Landroid/view/View;->getWidth()I

    move-result v4

    .line 1108
    .local v4, "childWidth":I
    invoke-virtual {p0}, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->getWidth()I

    move-result v5

    int-to-float v5, v5

    int-to-float v6, v3

    iget v7, p0, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->mSlideRange:I

    int-to-float v7, v7

    mul-float v7, v7, p1

    add-float/2addr v6, v7

    int-to-float v7, v4

    add-float/2addr v6, v7

    sub-float/2addr v5, v6

    float-to-int v3, v5

    .line 1109
    .end local v4    # "childWidth":I
    .local v3, "x":I
    goto :goto_0

    .line 1110
    .end local v3    # "x":I
    :cond_1
    invoke-virtual {p0}, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->getPaddingLeft()I

    move-result v3

    iget v4, v2, Landroidx/slidingpanelayout/widget/SlidingPaneLayout$LayoutParams;->leftMargin:I

    add-int/2addr v3, v4

    .line 1111
    .local v3, "startBound":I
    int-to-float v4, v3

    iget v5, p0, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->mSlideRange:I

    int-to-float v5, v5

    mul-float v5, v5, p1

    add-float/2addr v4, v5

    float-to-int v4, v4

    move v3, v4

    .line 1114
    .local v3, "x":I
    :goto_0
    iget-object v4, p0, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->mDragHelper:Landroidx/customview/widget/ViewDragHelper;

    iget-object v5, p0, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->mSlideableView:Landroid/view/View;

    invoke-virtual {v5}, Landroid/view/View;->getTop()I

    move-result v6

    invoke-virtual {v4, v5, v3, v6}, Landroidx/customview/widget/ViewDragHelper;->smoothSlideViewTo(Landroid/view/View;II)Z

    move-result v4

    if-eqz v4, :cond_2

    .line 1115
    invoke-virtual {p0}, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->setAllChildrenVisible()V

    .line 1116
    invoke-static {p0}, Landroidx/core/view/ViewCompat;->postInvalidateOnAnimation(Landroid/view/View;)V

    .line 1117
    const/4 v1, 0x1

    return v1

    .line 1119
    :cond_2
    return v1
.end method

.method updateObscuredViewsVisibility(Landroid/view/View;)V
    .locals 19
    .param p1, "panel"    # Landroid/view/View;

    .line 349
    move-object/from16 v0, p1

    invoke-virtual/range {p0 .. p0}, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->isLayoutRtlSupport()Z

    move-result v1

    .line 350
    .local v1, "isLayoutRtl":Z
    if-eqz v1, :cond_0

    invoke-virtual/range {p0 .. p0}, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->getWidth()I

    move-result v2

    invoke-virtual/range {p0 .. p0}, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->getPaddingRight()I

    move-result v3

    sub-int/2addr v2, v3

    goto :goto_0

    :cond_0
    invoke-virtual/range {p0 .. p0}, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->getPaddingLeft()I

    move-result v2

    .line 351
    .local v2, "startBound":I
    :goto_0
    if-eqz v1, :cond_1

    invoke-virtual/range {p0 .. p0}, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->getPaddingLeft()I

    move-result v3

    goto :goto_1

    :cond_1
    invoke-virtual/range {p0 .. p0}, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->getWidth()I

    move-result v3

    invoke-virtual/range {p0 .. p0}, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->getPaddingRight()I

    move-result v4

    sub-int/2addr v3, v4

    .line 352
    .local v3, "endBound":I
    :goto_1
    invoke-virtual/range {p0 .. p0}, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->getPaddingTop()I

    move-result v4

    .line 353
    .local v4, "topBound":I
    invoke-virtual/range {p0 .. p0}, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->getHeight()I

    move-result v5

    invoke-virtual/range {p0 .. p0}, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->getPaddingBottom()I

    move-result v6

    sub-int/2addr v5, v6

    .line 358
    .local v5, "bottomBound":I
    if-eqz v0, :cond_2

    invoke-static/range {p1 .. p1}, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->viewIsOpaque(Landroid/view/View;)Z

    move-result v6

    if-eqz v6, :cond_2

    .line 359
    invoke-virtual/range {p1 .. p1}, Landroid/view/View;->getLeft()I

    move-result v6

    .line 360
    .local v6, "left":I
    invoke-virtual/range {p1 .. p1}, Landroid/view/View;->getRight()I

    move-result v7

    .line 361
    .local v7, "right":I
    invoke-virtual/range {p1 .. p1}, Landroid/view/View;->getTop()I

    move-result v8

    .line 362
    .local v8, "top":I
    invoke-virtual/range {p1 .. p1}, Landroid/view/View;->getBottom()I

    move-result v9

    .local v9, "bottom":I
    goto :goto_2

    .line 364
    .end local v6    # "left":I
    .end local v7    # "right":I
    .end local v8    # "top":I
    .end local v9    # "bottom":I
    :cond_2
    const/4 v6, 0x0

    move v9, v6

    .restart local v9    # "bottom":I
    move v8, v6

    .restart local v8    # "top":I
    move v7, v6

    .line 367
    .restart local v6    # "left":I
    .restart local v7    # "right":I
    :goto_2
    const/4 v10, 0x0

    .local v10, "i":I
    invoke-virtual/range {p0 .. p0}, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->getChildCount()I

    move-result v11

    .local v11, "childCount":I
    :goto_3
    if-ge v10, v11, :cond_8

    .line 368
    move-object/from16 v12, p0

    invoke-virtual {v12, v10}, Landroidx/slidingpanelayout/widget/SlidingPaneLayout;->getChildAt(I)Landroid/view/View;

    move-result-object v13

    .line 370
    .local v13, "child":Landroid/view/View;
    if-ne v13, v0, :cond_3

    .line 372
    move/from16 v16, v1

    goto/16 :goto_8

    .line 373
    :cond_3
    invoke-virtual {v13}, Landroid/view/View;->getVisibility()I

    move-result v14

    const/16 v15, 0x8

    if-ne v14, v15, :cond_4

    .line 374
    move/from16 v16, v1

    goto :goto_7

    .line 378
    :cond_4
    if-eqz v1, :cond_5

    move v14, v3

    goto :goto_4

    :cond_5
    move v14, v2

    :goto_4
    invoke-virtual {v13}, Landroid/view/View;->getLeft()I

    move-result v15

    .line 377
    invoke-static {v14, v15}, Ljava/lang/Math;->max(II)I

    move-result v14

    .line 379
    .local v14, "clampedChildLeft":I
    invoke-virtual {v13}, Landroid/view/View;->getTop()I

    move-result v15

    invoke-static {v4, v15}, Ljava/lang/Math;->max(II)I

    move-result v15

    .line 381
    .local v15, "clampedChildTop":I
    if-eqz v1, :cond_6

    move v0, v2

    goto :goto_5

    :cond_6
    move v0, v3

    :goto_5
    move/from16 v16, v1

    .end local v1    # "isLayoutRtl":Z
    .local v16, "isLayoutRtl":Z
    invoke-virtual {v13}, Landroid/view/View;->getRight()I

    move-result v1

    .line 380
    invoke-static {v0, v1}, Ljava/lang/Math;->min(II)I

    move-result v0

    .line 382
    .local v0, "clampedChildRight":I
    invoke-virtual {v13}, Landroid/view/View;->getBottom()I

    move-result v1

    invoke-static {v5, v1}, Ljava/lang/Math;->min(II)I

    move-result v1

    .line 384
    .local v1, "clampedChildBottom":I
    if-lt v14, v6, :cond_7

    if-lt v15, v8, :cond_7

    if-gt v0, v7, :cond_7

    if-gt v1, v9, :cond_7

    .line 386
    const/16 v17, 0x4

    move/from16 v18, v0

    move/from16 v0, v17

    .local v17, "vis":I
    goto :goto_6

    .line 388
    .end local v17    # "vis":I
    :cond_7
    const/16 v17, 0x0

    move/from16 v18, v0

    move/from16 v0, v17

    .line 390
    .local v0, "vis":I
    .local v18, "clampedChildRight":I
    :goto_6
    invoke-virtual {v13, v0}, Landroid/view/View;->setVisibility(I)V

    .line 367
    .end local v0    # "vis":I
    .end local v1    # "clampedChildBottom":I
    .end local v13    # "child":Landroid/view/View;
    .end local v14    # "clampedChildLeft":I
    .end local v15    # "clampedChildTop":I
    .end local v18    # "clampedChildRight":I
    :goto_7
    add-int/lit8 v10, v10, 0x1

    move-object/from16 v0, p1

    move/from16 v1, v16

    goto :goto_3

    .end local v16    # "isLayoutRtl":Z
    .local v1, "isLayoutRtl":Z
    :cond_8
    move-object/from16 v12, p0

    move/from16 v16, v1

    .line 392
    .end local v1    # "isLayoutRtl":Z
    .end local v10    # "i":I
    .end local v11    # "childCount":I
    .restart local v16    # "isLayoutRtl":Z
    :goto_8
    return-void
.end method
