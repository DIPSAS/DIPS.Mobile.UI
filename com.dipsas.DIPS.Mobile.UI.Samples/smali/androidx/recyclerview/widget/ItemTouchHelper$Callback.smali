.class public abstract Landroidx/recyclerview/widget/ItemTouchHelper$Callback;
.super Ljava/lang/Object;
.source "ItemTouchHelper.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Landroidx/recyclerview/widget/ItemTouchHelper;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x409
    name = "Callback"
.end annotation


# static fields
.field private static final ABS_HORIZONTAL_DIR_FLAGS:I = 0xc0c0c

.field public static final DEFAULT_DRAG_ANIMATION_DURATION:I = 0xc8

.field public static final DEFAULT_SWIPE_ANIMATION_DURATION:I = 0xfa

.field private static final DRAG_SCROLL_ACCELERATION_LIMIT_TIME_MS:J = 0x7d0L

.field static final RELATIVE_DIR_FLAGS:I = 0x303030

.field private static final sDragScrollInterpolator:Landroid/view/animation/Interpolator;

.field private static final sDragViewScrollCapInterpolator:Landroid/view/animation/Interpolator;


# instance fields
.field private mCachedMaxScrollSpeed:I


# direct methods
.method static constructor <clinit>()V
    .locals 1

    .line 1405
    new-instance v0, Landroidx/recyclerview/widget/ItemTouchHelper$Callback$1;

    invoke-direct {v0}, Landroidx/recyclerview/widget/ItemTouchHelper$Callback$1;-><init>()V

    sput-object v0, Landroidx/recyclerview/widget/ItemTouchHelper$Callback;->sDragScrollInterpolator:Landroid/view/animation/Interpolator;

    .line 1412
    new-instance v0, Landroidx/recyclerview/widget/ItemTouchHelper$Callback$2;

    invoke-direct {v0}, Landroidx/recyclerview/widget/ItemTouchHelper$Callback$2;-><init>()V

    sput-object v0, Landroidx/recyclerview/widget/ItemTouchHelper$Callback;->sDragViewScrollCapInterpolator:Landroid/view/animation/Interpolator;

    return-void
.end method

.method public constructor <init>()V
    .locals 1

    .line 1389
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 1425
    const/4 v0, -0x1

    iput v0, p0, Landroidx/recyclerview/widget/ItemTouchHelper$Callback;->mCachedMaxScrollSpeed:I

    return-void
.end method

.method public static convertToRelativeDirection(II)I
    .locals 4
    .param p0, "flags"    # I
    .param p1, "layoutDirection"    # I

    .line 1486
    const v0, 0xc0c0c

    and-int v1, p0, v0

    .line 1487
    .local v1, "masked":I
    if-nez v1, :cond_0

    .line 1488
    return p0

    .line 1490
    :cond_0
    not-int v2, v1

    and-int/2addr p0, v2

    .line 1491
    if-nez p1, :cond_1

    .line 1493
    shl-int/lit8 v0, v1, 0x2

    or-int/2addr p0, v0

    .line 1494
    return p0

    .line 1497
    :cond_1
    shl-int/lit8 v2, v1, 0x1

    const v3, -0xc0c0d

    and-int/2addr v2, v3

    or-int/2addr p0, v2

    .line 1499
    shl-int/lit8 v2, v1, 0x1

    and-int/2addr v0, v2

    shl-int/lit8 v0, v0, 0x2

    or-int/2addr p0, v0

    .line 1501
    return p0
.end method

.method public static getDefaultUIUtil()Landroidx/recyclerview/widget/ItemTouchUIUtil;
    .locals 1

    .line 1470
    sget-object v0, Landroidx/recyclerview/widget/ItemTouchUIUtilImpl;->INSTANCE:Landroidx/recyclerview/widget/ItemTouchUIUtil;

    return-object v0
.end method

.method private getMaxDragScroll(Landroidx/recyclerview/widget/RecyclerView;)I
    .locals 2
    .param p1, "recyclerView"    # Landroidx/recyclerview/widget/RecyclerView;

    .line 1905
    iget v0, p0, Landroidx/recyclerview/widget/ItemTouchHelper$Callback;->mCachedMaxScrollSpeed:I

    const/4 v1, -0x1

    if-ne v0, v1, :cond_0

    .line 1906
    invoke-virtual {p1}, Landroidx/recyclerview/widget/RecyclerView;->getResources()Landroid/content/res/Resources;

    move-result-object v0

    sget v1, Landroidx/recyclerview/R$dimen;->item_touch_helper_max_drag_scroll_per_frame:I

    invoke-virtual {v0, v1}, Landroid/content/res/Resources;->getDimensionPixelSize(I)I

    move-result v0

    iput v0, p0, Landroidx/recyclerview/widget/ItemTouchHelper$Callback;->mCachedMaxScrollSpeed:I

    .line 1909
    :cond_0
    iget v0, p0, Landroidx/recyclerview/widget/ItemTouchHelper$Callback;->mCachedMaxScrollSpeed:I

    return v0
.end method

.method public static makeFlag(II)I
    .locals 1
    .param p0, "actionState"    # I
    .param p1, "directions"    # I

    .line 1533
    mul-int/lit8 v0, p0, 0x8

    shl-int v0, p1, v0

    return v0
.end method

.method public static makeMovementFlags(II)I
    .locals 2
    .param p0, "dragFlags"    # I
    .param p1, "swipeFlags"    # I

    .line 1516
    or-int v0, p1, p0

    const/4 v1, 0x0

    invoke-static {v1, v0}, Landroidx/recyclerview/widget/ItemTouchHelper$Callback;->makeFlag(II)I

    move-result v0

    .line 1517
    const/4 v1, 0x1

    invoke-static {v1, p1}, Landroidx/recyclerview/widget/ItemTouchHelper$Callback;->makeFlag(II)I

    move-result v1

    or-int/2addr v0, v1

    .line 1518
    const/4 v1, 0x2

    invoke-static {v1, p0}, Landroidx/recyclerview/widget/ItemTouchHelper$Callback;->makeFlag(II)I

    move-result v1

    or-int/2addr v0, v1

    .line 1516
    return v0
.end method


# virtual methods
.method public canDropOver(Landroidx/recyclerview/widget/RecyclerView;Landroidx/recyclerview/widget/RecyclerView$ViewHolder;Landroidx/recyclerview/widget/RecyclerView$ViewHolder;)Z
    .locals 1
    .param p1, "recyclerView"    # Landroidx/recyclerview/widget/RecyclerView;
    .param p2, "current"    # Landroidx/recyclerview/widget/RecyclerView$ViewHolder;
    .param p3, "target"    # Landroidx/recyclerview/widget/RecyclerView$ViewHolder;

    .line 1630
    const/4 v0, 0x1

    return v0
.end method

.method public chooseDropTarget(Landroidx/recyclerview/widget/RecyclerView$ViewHolder;Ljava/util/List;II)Landroidx/recyclerview/widget/RecyclerView$ViewHolder;
    .locals 14
    .param p1, "selected"    # Landroidx/recyclerview/widget/RecyclerView$ViewHolder;
    .param p3, "curX"    # I
    .param p4, "curY"    # I
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Landroidx/recyclerview/widget/RecyclerView$ViewHolder;",
            "Ljava/util/List<",
            "Landroidx/recyclerview/widget/RecyclerView$ViewHolder;",
            ">;II)",
            "Landroidx/recyclerview/widget/RecyclerView$ViewHolder;"
        }
    .end annotation

    .line 1806
    .local p2, "dropTargets":Ljava/util/List;, "Ljava/util/List<Landroidx/recyclerview/widget/RecyclerView$ViewHolder;>;"
    move-object v0, p1

    iget-object v1, v0, Landroidx/recyclerview/widget/RecyclerView$ViewHolder;->itemView:Landroid/view/View;

    invoke-virtual {v1}, Landroid/view/View;->getWidth()I

    move-result v1

    add-int v1, p3, v1

    .line 1807
    .local v1, "right":I
    iget-object v2, v0, Landroidx/recyclerview/widget/RecyclerView$ViewHolder;->itemView:Landroid/view/View;

    invoke-virtual {v2}, Landroid/view/View;->getHeight()I

    move-result v2

    add-int v2, p4, v2

    .line 1808
    .local v2, "bottom":I
    const/4 v3, 0x0

    .line 1809
    .local v3, "winner":Landroidx/recyclerview/widget/RecyclerView$ViewHolder;
    const/4 v4, -0x1

    .line 1810
    .local v4, "winnerScore":I
    iget-object v5, v0, Landroidx/recyclerview/widget/RecyclerView$ViewHolder;->itemView:Landroid/view/View;

    invoke-virtual {v5}, Landroid/view/View;->getLeft()I

    move-result v5

    sub-int v5, p3, v5

    .line 1811
    .local v5, "dx":I
    iget-object v6, v0, Landroidx/recyclerview/widget/RecyclerView$ViewHolder;->itemView:Landroid/view/View;

    invoke-virtual {v6}, Landroid/view/View;->getTop()I

    move-result v6

    sub-int v6, p4, v6

    .line 1812
    .local v6, "dy":I
    invoke-interface/range {p2 .. p2}, Ljava/util/List;->size()I

    move-result v7

    .line 1813
    .local v7, "targetsSize":I
    const/4 v8, 0x0

    .local v8, "i":I
    :goto_0
    if-ge v8, v7, :cond_4

    .line 1814
    move-object/from16 v9, p2

    invoke-interface {v9, v8}, Ljava/util/List;->get(I)Ljava/lang/Object;

    move-result-object v10

    check-cast v10, Landroidx/recyclerview/widget/RecyclerView$ViewHolder;

    .line 1815
    .local v10, "target":Landroidx/recyclerview/widget/RecyclerView$ViewHolder;
    if-lez v5, :cond_0

    .line 1816
    iget-object v11, v10, Landroidx/recyclerview/widget/RecyclerView$ViewHolder;->itemView:Landroid/view/View;

    invoke-virtual {v11}, Landroid/view/View;->getRight()I

    move-result v11

    sub-int/2addr v11, v1

    .line 1817
    .local v11, "diff":I
    if-gez v11, :cond_0

    iget-object v12, v10, Landroidx/recyclerview/widget/RecyclerView$ViewHolder;->itemView:Landroid/view/View;

    invoke-virtual {v12}, Landroid/view/View;->getRight()I

    move-result v12

    iget-object v13, v0, Landroidx/recyclerview/widget/RecyclerView$ViewHolder;->itemView:Landroid/view/View;

    invoke-virtual {v13}, Landroid/view/View;->getRight()I

    move-result v13

    if-le v12, v13, :cond_0

    .line 1818
    invoke-static {v11}, Ljava/lang/Math;->abs(I)I

    move-result v12

    .line 1819
    .local v12, "score":I
    if-le v12, v4, :cond_0

    .line 1820
    move v4, v12

    .line 1821
    move-object v3, v10

    .line 1825
    .end local v11    # "diff":I
    .end local v12    # "score":I
    :cond_0
    if-gez v5, :cond_1

    .line 1826
    iget-object v11, v10, Landroidx/recyclerview/widget/RecyclerView$ViewHolder;->itemView:Landroid/view/View;

    invoke-virtual {v11}, Landroid/view/View;->getLeft()I

    move-result v11

    sub-int v11, v11, p3

    .line 1827
    .restart local v11    # "diff":I
    if-lez v11, :cond_1

    iget-object v12, v10, Landroidx/recyclerview/widget/RecyclerView$ViewHolder;->itemView:Landroid/view/View;

    invoke-virtual {v12}, Landroid/view/View;->getLeft()I

    move-result v12

    iget-object v13, v0, Landroidx/recyclerview/widget/RecyclerView$ViewHolder;->itemView:Landroid/view/View;

    invoke-virtual {v13}, Landroid/view/View;->getLeft()I

    move-result v13

    if-ge v12, v13, :cond_1

    .line 1828
    invoke-static {v11}, Ljava/lang/Math;->abs(I)I

    move-result v12

    .line 1829
    .restart local v12    # "score":I
    if-le v12, v4, :cond_1

    .line 1830
    move v4, v12

    .line 1831
    move-object v3, v10

    .line 1835
    .end local v11    # "diff":I
    .end local v12    # "score":I
    :cond_1
    if-gez v6, :cond_2

    .line 1836
    iget-object v11, v10, Landroidx/recyclerview/widget/RecyclerView$ViewHolder;->itemView:Landroid/view/View;

    invoke-virtual {v11}, Landroid/view/View;->getTop()I

    move-result v11

    sub-int v11, v11, p4

    .line 1837
    .restart local v11    # "diff":I
    if-lez v11, :cond_2

    iget-object v12, v10, Landroidx/recyclerview/widget/RecyclerView$ViewHolder;->itemView:Landroid/view/View;

    invoke-virtual {v12}, Landroid/view/View;->getTop()I

    move-result v12

    iget-object v13, v0, Landroidx/recyclerview/widget/RecyclerView$ViewHolder;->itemView:Landroid/view/View;

    invoke-virtual {v13}, Landroid/view/View;->getTop()I

    move-result v13

    if-ge v12, v13, :cond_2

    .line 1838
    invoke-static {v11}, Ljava/lang/Math;->abs(I)I

    move-result v12

    .line 1839
    .restart local v12    # "score":I
    if-le v12, v4, :cond_2

    .line 1840
    move v4, v12

    .line 1841
    move-object v3, v10

    .line 1846
    .end local v11    # "diff":I
    .end local v12    # "score":I
    :cond_2
    if-lez v6, :cond_3

    .line 1847
    iget-object v11, v10, Landroidx/recyclerview/widget/RecyclerView$ViewHolder;->itemView:Landroid/view/View;

    invoke-virtual {v11}, Landroid/view/View;->getBottom()I

    move-result v11

    sub-int/2addr v11, v2

    .line 1848
    .restart local v11    # "diff":I
    if-gez v11, :cond_3

    iget-object v12, v10, Landroidx/recyclerview/widget/RecyclerView$ViewHolder;->itemView:Landroid/view/View;

    invoke-virtual {v12}, Landroid/view/View;->getBottom()I

    move-result v12

    iget-object v13, v0, Landroidx/recyclerview/widget/RecyclerView$ViewHolder;->itemView:Landroid/view/View;

    invoke-virtual {v13}, Landroid/view/View;->getBottom()I

    move-result v13

    if-le v12, v13, :cond_3

    .line 1849
    invoke-static {v11}, Ljava/lang/Math;->abs(I)I

    move-result v12

    .line 1850
    .restart local v12    # "score":I
    if-le v12, v4, :cond_3

    .line 1851
    move v4, v12

    .line 1852
    move-object v3, v10

    .line 1813
    .end local v10    # "target":Landroidx/recyclerview/widget/RecyclerView$ViewHolder;
    .end local v11    # "diff":I
    .end local v12    # "score":I
    :cond_3
    add-int/lit8 v8, v8, 0x1

    goto/16 :goto_0

    :cond_4
    move-object/from16 v9, p2

    .line 1857
    .end local v8    # "i":I
    return-object v3
.end method

.method public clearView(Landroidx/recyclerview/widget/RecyclerView;Landroidx/recyclerview/widget/RecyclerView$ViewHolder;)V
    .locals 2
    .param p1, "recyclerView"    # Landroidx/recyclerview/widget/RecyclerView;
    .param p2, "viewHolder"    # Landroidx/recyclerview/widget/RecyclerView$ViewHolder;

    .line 2042
    sget-object v0, Landroidx/recyclerview/widget/ItemTouchUIUtilImpl;->INSTANCE:Landroidx/recyclerview/widget/ItemTouchUIUtil;

    iget-object v1, p2, Landroidx/recyclerview/widget/RecyclerView$ViewHolder;->itemView:Landroid/view/View;

    invoke-interface {v0, v1}, Landroidx/recyclerview/widget/ItemTouchUIUtil;->clearView(Landroid/view/View;)V

    .line 2043
    return-void
.end method

.method public convertToAbsoluteDirection(II)I
    .locals 4
    .param p1, "flags"    # I
    .param p2, "layoutDirection"    # I

    .line 1577
    const v0, 0x303030

    and-int v1, p1, v0

    .line 1578
    .local v1, "masked":I
    if-nez v1, :cond_0

    .line 1579
    return p1

    .line 1581
    :cond_0
    not-int v2, v1

    and-int/2addr p1, v2

    .line 1582
    if-nez p2, :cond_1

    .line 1584
    shr-int/lit8 v0, v1, 0x2

    or-int/2addr p1, v0

    .line 1585
    return p1

    .line 1588
    :cond_1
    shr-int/lit8 v2, v1, 0x1

    const v3, -0x303031

    and-int/2addr v2, v3

    or-int/2addr p1, v2

    .line 1590
    shr-int/lit8 v2, v1, 0x1

    and-int/2addr v0, v2

    shr-int/lit8 v0, v0, 0x2

    or-int/2addr p1, v0

    .line 1592
    return p1
.end method

.method final getAbsoluteMovementFlags(Landroidx/recyclerview/widget/RecyclerView;Landroidx/recyclerview/widget/RecyclerView$ViewHolder;)I
    .locals 2
    .param p1, "recyclerView"    # Landroidx/recyclerview/widget/RecyclerView;
    .param p2, "viewHolder"    # Landroidx/recyclerview/widget/RecyclerView$ViewHolder;

    .line 1597
    invoke-virtual {p0, p1, p2}, Landroidx/recyclerview/widget/ItemTouchHelper$Callback;->getMovementFlags(Landroidx/recyclerview/widget/RecyclerView;Landroidx/recyclerview/widget/RecyclerView$ViewHolder;)I

    move-result v0

    .line 1598
    .local v0, "flags":I
    invoke-static {p1}, Landroidx/core/view/ViewCompat;->getLayoutDirection(Landroid/view/View;)I

    move-result v1

    invoke-virtual {p0, v0, v1}, Landroidx/recyclerview/widget/ItemTouchHelper$Callback;->convertToAbsoluteDirection(II)I

    move-result v1

    return v1
.end method

.method public getAnimationDuration(Landroidx/recyclerview/widget/RecyclerView;IFF)J
    .locals 3
    .param p1, "recyclerView"    # Landroidx/recyclerview/widget/RecyclerView;
    .param p2, "animationType"    # I
    .param p3, "animateDx"    # F
    .param p4, "animateDy"    # F

    .line 2136
    invoke-virtual {p1}, Landroidx/recyclerview/widget/RecyclerView;->getItemAnimator()Landroidx/recyclerview/widget/RecyclerView$ItemAnimator;

    move-result-object v0

    .line 2137
    .local v0, "itemAnimator":Landroidx/recyclerview/widget/RecyclerView$ItemAnimator;
    const/16 v1, 0x8

    if-nez v0, :cond_1

    .line 2138
    if-ne p2, v1, :cond_0

    const-wide/16 v1, 0xc8

    goto :goto_0

    .line 2139
    :cond_0
    const-wide/16 v1, 0xfa

    .line 2138
    :goto_0
    return-wide v1

    .line 2141
    :cond_1
    if-ne p2, v1, :cond_2

    invoke-virtual {v0}, Landroidx/recyclerview/widget/RecyclerView$ItemAnimator;->getMoveDuration()J

    move-result-wide v1

    goto :goto_1

    .line 2142
    :cond_2
    invoke-virtual {v0}, Landroidx/recyclerview/widget/RecyclerView$ItemAnimator;->getRemoveDuration()J

    move-result-wide v1

    .line 2141
    :goto_1
    return-wide v1
.end method

.method public getBoundingBoxMargin()I
    .locals 1

    .line 1694
    const/4 v0, 0x0

    return v0
.end method

.method public getMoveThreshold(Landroidx/recyclerview/widget/RecyclerView$ViewHolder;)F
    .locals 1
    .param p1, "viewHolder"    # Landroidx/recyclerview/widget/RecyclerView$ViewHolder;

    .line 1724
    const/high16 v0, 0x3f000000    # 0.5f

    return v0
.end method

.method public abstract getMovementFlags(Landroidx/recyclerview/widget/RecyclerView;Landroidx/recyclerview/widget/RecyclerView$ViewHolder;)I
.end method

.method public getSwipeEscapeVelocity(F)F
    .locals 0
    .param p1, "defaultValue"    # F

    .line 1750
    return p1
.end method

.method public getSwipeThreshold(Landroidx/recyclerview/widget/RecyclerView$ViewHolder;)F
    .locals 1
    .param p1, "viewHolder"    # Landroidx/recyclerview/widget/RecyclerView$ViewHolder;

    .line 1710
    const/high16 v0, 0x3f000000    # 0.5f

    return v0
.end method

.method public getSwipeVelocityThreshold(F)F
    .locals 0
    .param p1, "defaultValue"    # F

    .line 1774
    return p1
.end method

.method hasDragFlag(Landroidx/recyclerview/widget/RecyclerView;Landroidx/recyclerview/widget/RecyclerView$ViewHolder;)Z
    .locals 2
    .param p1, "recyclerView"    # Landroidx/recyclerview/widget/RecyclerView;
    .param p2, "viewHolder"    # Landroidx/recyclerview/widget/RecyclerView$ViewHolder;

    .line 1602
    invoke-virtual {p0, p1, p2}, Landroidx/recyclerview/widget/ItemTouchHelper$Callback;->getAbsoluteMovementFlags(Landroidx/recyclerview/widget/RecyclerView;Landroidx/recyclerview/widget/RecyclerView$ViewHolder;)I

    move-result v0

    .line 1603
    .local v0, "flags":I
    const/high16 v1, 0xff0000

    and-int/2addr v1, v0

    if-eqz v1, :cond_0

    const/4 v1, 0x1

    goto :goto_0

    :cond_0
    const/4 v1, 0x0

    :goto_0
    return v1
.end method

.method hasSwipeFlag(Landroidx/recyclerview/widget/RecyclerView;Landroidx/recyclerview/widget/RecyclerView$ViewHolder;)Z
    .locals 2
    .param p1, "recyclerView"    # Landroidx/recyclerview/widget/RecyclerView;
    .param p2, "viewHolder"    # Landroidx/recyclerview/widget/RecyclerView$ViewHolder;

    .line 1608
    invoke-virtual {p0, p1, p2}, Landroidx/recyclerview/widget/ItemTouchHelper$Callback;->getAbsoluteMovementFlags(Landroidx/recyclerview/widget/RecyclerView;Landroidx/recyclerview/widget/RecyclerView$ViewHolder;)I

    move-result v0

    .line 1609
    .local v0, "flags":I
    const v1, 0xff00

    and-int/2addr v1, v0

    if-eqz v1, :cond_0

    const/4 v1, 0x1

    goto :goto_0

    :cond_0
    const/4 v1, 0x0

    :goto_0
    return v1
.end method

.method public interpolateOutOfBoundsScroll(Landroidx/recyclerview/widget/RecyclerView;IIIJ)I
    .locals 8
    .param p1, "recyclerView"    # Landroidx/recyclerview/widget/RecyclerView;
    .param p2, "viewSize"    # I
    .param p3, "viewSizeOutOfBounds"    # I
    .param p4, "totalSize"    # I
    .param p5, "msSinceStartScroll"    # J

    .line 2170
    invoke-direct {p0, p1}, Landroidx/recyclerview/widget/ItemTouchHelper$Callback;->getMaxDragScroll(Landroidx/recyclerview/widget/RecyclerView;)I

    move-result v0

    .line 2171
    .local v0, "maxScroll":I
    invoke-static {p3}, Ljava/lang/Math;->abs(I)I

    move-result v1

    .line 2172
    .local v1, "absOutOfBounds":I
    int-to-float v2, p3

    invoke-static {v2}, Ljava/lang/Math;->signum(F)F

    move-result v2

    float-to-int v2, v2

    .line 2174
    .local v2, "direction":I
    int-to-float v3, v1

    const/high16 v4, 0x3f800000    # 1.0f

    mul-float v3, v3, v4

    int-to-float v5, p2

    div-float/2addr v3, v5

    invoke-static {v4, v3}, Ljava/lang/Math;->min(FF)F

    move-result v3

    .line 2175
    .local v3, "outOfBoundsRatio":F
    mul-int v4, v2, v0

    int-to-float v4, v4

    sget-object v5, Landroidx/recyclerview/widget/ItemTouchHelper$Callback;->sDragViewScrollCapInterpolator:Landroid/view/animation/Interpolator;

    .line 2176
    invoke-interface {v5, v3}, Landroid/view/animation/Interpolator;->getInterpolation(F)F

    move-result v5

    mul-float v4, v4, v5

    float-to-int v4, v4

    .line 2178
    .local v4, "cappedScroll":I
    const-wide/16 v5, 0x7d0

    cmp-long v7, p5, v5

    if-lez v7, :cond_0

    .line 2179
    const/high16 v5, 0x3f800000    # 1.0f

    .local v5, "timeRatio":F
    goto :goto_0

    .line 2181
    .end local v5    # "timeRatio":F
    :cond_0
    long-to-float v5, p5

    const/high16 v6, 0x44fa0000    # 2000.0f

    div-float/2addr v5, v6

    .line 2183
    .restart local v5    # "timeRatio":F
    :goto_0
    int-to-float v6, v4

    sget-object v7, Landroidx/recyclerview/widget/ItemTouchHelper$Callback;->sDragScrollInterpolator:Landroid/view/animation/Interpolator;

    .line 2184
    invoke-interface {v7, v5}, Landroid/view/animation/Interpolator;->getInterpolation(F)F

    move-result v7

    mul-float v6, v6, v7

    float-to-int v6, v6

    .line 2185
    .local v6, "value":I
    if-nez v6, :cond_2

    .line 2186
    if-lez p3, :cond_1

    const/4 v7, 0x1

    goto :goto_1

    :cond_1
    const/4 v7, -0x1

    :goto_1
    return v7

    .line 2188
    :cond_2
    return v6
.end method

.method public isItemViewSwipeEnabled()Z
    .locals 1

    .line 1682
    const/4 v0, 0x1

    return v0
.end method

.method public isLongPressDragEnabled()Z
    .locals 1

    .line 1667
    const/4 v0, 0x1

    return v0
.end method

.method public onChildDraw(Landroid/graphics/Canvas;Landroidx/recyclerview/widget/RecyclerView;Landroidx/recyclerview/widget/RecyclerView$ViewHolder;FFIZ)V
    .locals 8
    .param p1, "c"    # Landroid/graphics/Canvas;
    .param p2, "recyclerView"    # Landroidx/recyclerview/widget/RecyclerView;
    .param p3, "viewHolder"    # Landroidx/recyclerview/widget/RecyclerView$ViewHolder;
    .param p4, "dX"    # F
    .param p5, "dY"    # F
    .param p6, "actionState"    # I
    .param p7, "isCurrentlyActive"    # Z

    .line 2075
    sget-object v0, Landroidx/recyclerview/widget/ItemTouchUIUtilImpl;->INSTANCE:Landroidx/recyclerview/widget/ItemTouchUIUtil;

    iget-object v3, p3, Landroidx/recyclerview/widget/RecyclerView$ViewHolder;->itemView:Landroid/view/View;

    move-object v1, p1

    move-object v2, p2

    move v4, p4

    move v5, p5

    move v6, p6

    move v7, p7

    invoke-interface/range {v0 .. v7}, Landroidx/recyclerview/widget/ItemTouchUIUtil;->onDraw(Landroid/graphics/Canvas;Landroidx/recyclerview/widget/RecyclerView;Landroid/view/View;FFIZ)V

    .line 2077
    return-void
.end method

.method public onChildDrawOver(Landroid/graphics/Canvas;Landroidx/recyclerview/widget/RecyclerView;Landroidx/recyclerview/widget/RecyclerView$ViewHolder;FFIZ)V
    .locals 8
    .param p1, "c"    # Landroid/graphics/Canvas;
    .param p2, "recyclerView"    # Landroidx/recyclerview/widget/RecyclerView;
    .param p3, "viewHolder"    # Landroidx/recyclerview/widget/RecyclerView$ViewHolder;
    .param p4, "dX"    # F
    .param p5, "dY"    # F
    .param p6, "actionState"    # I
    .param p7, "isCurrentlyActive"    # Z

    .line 2109
    sget-object v0, Landroidx/recyclerview/widget/ItemTouchUIUtilImpl;->INSTANCE:Landroidx/recyclerview/widget/ItemTouchUIUtil;

    iget-object v3, p3, Landroidx/recyclerview/widget/RecyclerView$ViewHolder;->itemView:Landroid/view/View;

    move-object v1, p1

    move-object v2, p2

    move v4, p4

    move v5, p5

    move v6, p6

    move v7, p7

    invoke-interface/range {v0 .. v7}, Landroidx/recyclerview/widget/ItemTouchUIUtil;->onDrawOver(Landroid/graphics/Canvas;Landroidx/recyclerview/widget/RecyclerView;Landroid/view/View;FFIZ)V

    .line 2111
    return-void
.end method

.method onDraw(Landroid/graphics/Canvas;Landroidx/recyclerview/widget/RecyclerView;Landroidx/recyclerview/widget/RecyclerView$ViewHolder;Ljava/util/List;IFF)V
    .locals 14
    .param p1, "c"    # Landroid/graphics/Canvas;
    .param p2, "parent"    # Landroidx/recyclerview/widget/RecyclerView;
    .param p3, "selected"    # Landroidx/recyclerview/widget/RecyclerView$ViewHolder;
    .param p5, "actionState"    # I
    .param p6, "dX"    # F
    .param p7, "dY"    # F
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Landroid/graphics/Canvas;",
            "Landroidx/recyclerview/widget/RecyclerView;",
            "Landroidx/recyclerview/widget/RecyclerView$ViewHolder;",
            "Ljava/util/List<",
            "Landroidx/recyclerview/widget/ItemTouchHelper$RecoverAnimation;",
            ">;IFF)V"
        }
    .end annotation

    .line 1982
    .local p4, "recoverAnimationList":Ljava/util/List;, "Ljava/util/List<Landroidx/recyclerview/widget/ItemTouchHelper$RecoverAnimation;>;"
    move-object v8, p1

    invoke-interface/range {p4 .. p4}, Ljava/util/List;->size()I

    move-result v9

    .line 1983
    .local v9, "recoverAnimSize":I
    const/4 v0, 0x0

    move v10, v0

    .local v10, "i":I
    :goto_0
    if-ge v10, v9, :cond_0

    .line 1984
    move-object/from16 v11, p4

    invoke-interface {v11, v10}, Ljava/util/List;->get(I)Ljava/lang/Object;

    move-result-object v0

    move-object v12, v0

    check-cast v12, Landroidx/recyclerview/widget/ItemTouchHelper$RecoverAnimation;

    .line 1985
    .local v12, "anim":Landroidx/recyclerview/widget/ItemTouchHelper$RecoverAnimation;
    invoke-virtual {v12}, Landroidx/recyclerview/widget/ItemTouchHelper$RecoverAnimation;->update()V

    .line 1986
    invoke-virtual {p1}, Landroid/graphics/Canvas;->save()I

    move-result v13

    .line 1987
    .local v13, "count":I
    iget-object v3, v12, Landroidx/recyclerview/widget/ItemTouchHelper$RecoverAnimation;->mViewHolder:Landroidx/recyclerview/widget/RecyclerView$ViewHolder;

    iget v4, v12, Landroidx/recyclerview/widget/ItemTouchHelper$RecoverAnimation;->mX:F

    iget v5, v12, Landroidx/recyclerview/widget/ItemTouchHelper$RecoverAnimation;->mY:F

    iget v6, v12, Landroidx/recyclerview/widget/ItemTouchHelper$RecoverAnimation;->mActionState:I

    const/4 v7, 0x0

    move-object v0, p0

    move-object v1, p1

    move-object/from16 v2, p2

    invoke-virtual/range {v0 .. v7}, Landroidx/recyclerview/widget/ItemTouchHelper$Callback;->onChildDraw(Landroid/graphics/Canvas;Landroidx/recyclerview/widget/RecyclerView;Landroidx/recyclerview/widget/RecyclerView$ViewHolder;FFIZ)V

    .line 1989
    invoke-virtual {p1, v13}, Landroid/graphics/Canvas;->restoreToCount(I)V

    .line 1983
    .end local v12    # "anim":Landroidx/recyclerview/widget/ItemTouchHelper$RecoverAnimation;
    .end local v13    # "count":I
    add-int/lit8 v10, v10, 0x1

    goto :goto_0

    :cond_0
    move-object/from16 v11, p4

    .line 1991
    .end local v10    # "i":I
    if-eqz p3, :cond_1

    .line 1992
    invoke-virtual {p1}, Landroid/graphics/Canvas;->save()I

    move-result v10

    .line 1993
    .local v10, "count":I
    const/4 v7, 0x1

    move-object v0, p0

    move-object v1, p1

    move-object/from16 v2, p2

    move-object/from16 v3, p3

    move/from16 v4, p6

    move/from16 v5, p7

    move/from16 v6, p5

    invoke-virtual/range {v0 .. v7}, Landroidx/recyclerview/widget/ItemTouchHelper$Callback;->onChildDraw(Landroid/graphics/Canvas;Landroidx/recyclerview/widget/RecyclerView;Landroidx/recyclerview/widget/RecyclerView$ViewHolder;FFIZ)V

    .line 1994
    invoke-virtual {p1, v10}, Landroid/graphics/Canvas;->restoreToCount(I)V

    .line 1996
    .end local v10    # "count":I
    :cond_1
    return-void
.end method

.method onDrawOver(Landroid/graphics/Canvas;Landroidx/recyclerview/widget/RecyclerView;Landroidx/recyclerview/widget/RecyclerView$ViewHolder;Ljava/util/List;IFF)V
    .locals 14
    .param p1, "c"    # Landroid/graphics/Canvas;
    .param p2, "parent"    # Landroidx/recyclerview/widget/RecyclerView;
    .param p3, "selected"    # Landroidx/recyclerview/widget/RecyclerView$ViewHolder;
    .param p5, "actionState"    # I
    .param p6, "dX"    # F
    .param p7, "dY"    # F
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Landroid/graphics/Canvas;",
            "Landroidx/recyclerview/widget/RecyclerView;",
            "Landroidx/recyclerview/widget/RecyclerView$ViewHolder;",
            "Ljava/util/List<",
            "Landroidx/recyclerview/widget/ItemTouchHelper$RecoverAnimation;",
            ">;IFF)V"
        }
    .end annotation

    .line 2001
    .local p4, "recoverAnimationList":Ljava/util/List;, "Ljava/util/List<Landroidx/recyclerview/widget/ItemTouchHelper$RecoverAnimation;>;"
    move-object v8, p1

    move-object/from16 v9, p4

    invoke-interface/range {p4 .. p4}, Ljava/util/List;->size()I

    move-result v10

    .line 2002
    .local v10, "recoverAnimSize":I
    const/4 v0, 0x0

    move v11, v0

    .local v11, "i":I
    :goto_0
    if-ge v11, v10, :cond_0

    .line 2003
    invoke-interface {v9, v11}, Ljava/util/List;->get(I)Ljava/lang/Object;

    move-result-object v0

    move-object v12, v0

    check-cast v12, Landroidx/recyclerview/widget/ItemTouchHelper$RecoverAnimation;

    .line 2004
    .local v12, "anim":Landroidx/recyclerview/widget/ItemTouchHelper$RecoverAnimation;
    invoke-virtual {p1}, Landroid/graphics/Canvas;->save()I

    move-result v13

    .line 2005
    .local v13, "count":I
    iget-object v3, v12, Landroidx/recyclerview/widget/ItemTouchHelper$RecoverAnimation;->mViewHolder:Landroidx/recyclerview/widget/RecyclerView$ViewHolder;

    iget v4, v12, Landroidx/recyclerview/widget/ItemTouchHelper$RecoverAnimation;->mX:F

    iget v5, v12, Landroidx/recyclerview/widget/ItemTouchHelper$RecoverAnimation;->mY:F

    iget v6, v12, Landroidx/recyclerview/widget/ItemTouchHelper$RecoverAnimation;->mActionState:I

    const/4 v7, 0x0

    move-object v0, p0

    move-object v1, p1

    move-object/from16 v2, p2

    invoke-virtual/range {v0 .. v7}, Landroidx/recyclerview/widget/ItemTouchHelper$Callback;->onChildDrawOver(Landroid/graphics/Canvas;Landroidx/recyclerview/widget/RecyclerView;Landroidx/recyclerview/widget/RecyclerView$ViewHolder;FFIZ)V

    .line 2007
    invoke-virtual {p1, v13}, Landroid/graphics/Canvas;->restoreToCount(I)V

    .line 2002
    .end local v12    # "anim":Landroidx/recyclerview/widget/ItemTouchHelper$RecoverAnimation;
    .end local v13    # "count":I
    add-int/lit8 v11, v11, 0x1

    goto :goto_0

    .line 2009
    .end local v11    # "i":I
    :cond_0
    if-eqz p3, :cond_1

    .line 2010
    invoke-virtual {p1}, Landroid/graphics/Canvas;->save()I

    move-result v11

    .line 2011
    .local v11, "count":I
    const/4 v7, 0x1

    move-object v0, p0

    move-object v1, p1

    move-object/from16 v2, p2

    move-object/from16 v3, p3

    move/from16 v4, p6

    move/from16 v5, p7

    move/from16 v6, p5

    invoke-virtual/range {v0 .. v7}, Landroidx/recyclerview/widget/ItemTouchHelper$Callback;->onChildDrawOver(Landroid/graphics/Canvas;Landroidx/recyclerview/widget/RecyclerView;Landroidx/recyclerview/widget/RecyclerView$ViewHolder;FFIZ)V

    .line 2012
    invoke-virtual {p1, v11}, Landroid/graphics/Canvas;->restoreToCount(I)V

    .line 2014
    .end local v11    # "count":I
    :cond_1
    const/4 v0, 0x0

    .line 2015
    .local v0, "hasRunningAnimation":Z
    add-int/lit8 v1, v10, -0x1

    .local v1, "i":I
    :goto_1
    if-ltz v1, :cond_4

    .line 2016
    invoke-interface {v9, v1}, Ljava/util/List;->get(I)Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Landroidx/recyclerview/widget/ItemTouchHelper$RecoverAnimation;

    .line 2017
    .local v2, "anim":Landroidx/recyclerview/widget/ItemTouchHelper$RecoverAnimation;
    iget-boolean v3, v2, Landroidx/recyclerview/widget/ItemTouchHelper$RecoverAnimation;->mEnded:Z

    if-eqz v3, :cond_2

    iget-boolean v3, v2, Landroidx/recyclerview/widget/ItemTouchHelper$RecoverAnimation;->mIsPendingCleanup:Z

    if-nez v3, :cond_2

    .line 2018
    invoke-interface {v9, v1}, Ljava/util/List;->remove(I)Ljava/lang/Object;

    goto :goto_2

    .line 2019
    :cond_2
    iget-boolean v3, v2, Landroidx/recyclerview/widget/ItemTouchHelper$RecoverAnimation;->mEnded:Z

    if-nez v3, :cond_3

    .line 2020
    const/4 v0, 0x1

    .line 2015
    .end local v2    # "anim":Landroidx/recyclerview/widget/ItemTouchHelper$RecoverAnimation;
    :cond_3
    :goto_2
    add-int/lit8 v1, v1, -0x1

    goto :goto_1

    .line 2023
    .end local v1    # "i":I
    :cond_4
    if-eqz v0, :cond_5

    .line 2024
    invoke-virtual/range {p2 .. p2}, Landroidx/recyclerview/widget/RecyclerView;->invalidate()V

    .line 2026
    :cond_5
    return-void
.end method

.method public abstract onMove(Landroidx/recyclerview/widget/RecyclerView;Landroidx/recyclerview/widget/RecyclerView$ViewHolder;Landroidx/recyclerview/widget/RecyclerView$ViewHolder;)Z
.end method

.method public onMoved(Landroidx/recyclerview/widget/RecyclerView;Landroidx/recyclerview/widget/RecyclerView$ViewHolder;ILandroidx/recyclerview/widget/RecyclerView$ViewHolder;III)V
    .locals 5
    .param p1, "recyclerView"    # Landroidx/recyclerview/widget/RecyclerView;
    .param p2, "viewHolder"    # Landroidx/recyclerview/widget/RecyclerView$ViewHolder;
    .param p3, "fromPos"    # I
    .param p4, "target"    # Landroidx/recyclerview/widget/RecyclerView$ViewHolder;
    .param p5, "toPos"    # I
    .param p6, "x"    # I
    .param p7, "y"    # I

    .line 1948
    invoke-virtual {p1}, Landroidx/recyclerview/widget/RecyclerView;->getLayoutManager()Landroidx/recyclerview/widget/RecyclerView$LayoutManager;

    move-result-object v0

    .line 1949
    .local v0, "layoutManager":Landroidx/recyclerview/widget/RecyclerView$LayoutManager;
    instance-of v1, v0, Landroidx/recyclerview/widget/ItemTouchHelper$ViewDropHandler;

    if-eqz v1, :cond_0

    .line 1950
    move-object v1, v0

    check-cast v1, Landroidx/recyclerview/widget/ItemTouchHelper$ViewDropHandler;

    iget-object v2, p2, Landroidx/recyclerview/widget/RecyclerView$ViewHolder;->itemView:Landroid/view/View;

    iget-object v3, p4, Landroidx/recyclerview/widget/RecyclerView$ViewHolder;->itemView:Landroid/view/View;

    invoke-interface {v1, v2, v3, p6, p7}, Landroidx/recyclerview/widget/ItemTouchHelper$ViewDropHandler;->prepareForDrop(Landroid/view/View;Landroid/view/View;II)V

    .line 1952
    return-void

    .line 1956
    :cond_0
    invoke-virtual {v0}, Landroidx/recyclerview/widget/RecyclerView$LayoutManager;->canScrollHorizontally()Z

    move-result v1

    if-eqz v1, :cond_2

    .line 1957
    iget-object v1, p4, Landroidx/recyclerview/widget/RecyclerView$ViewHolder;->itemView:Landroid/view/View;

    invoke-virtual {v0, v1}, Landroidx/recyclerview/widget/RecyclerView$LayoutManager;->getDecoratedLeft(Landroid/view/View;)I

    move-result v1

    .line 1958
    .local v1, "minLeft":I
    invoke-virtual {p1}, Landroidx/recyclerview/widget/RecyclerView;->getPaddingLeft()I

    move-result v2

    if-gt v1, v2, :cond_1

    .line 1959
    invoke-virtual {p1, p5}, Landroidx/recyclerview/widget/RecyclerView;->scrollToPosition(I)V

    .line 1961
    :cond_1
    iget-object v2, p4, Landroidx/recyclerview/widget/RecyclerView$ViewHolder;->itemView:Landroid/view/View;

    invoke-virtual {v0, v2}, Landroidx/recyclerview/widget/RecyclerView$LayoutManager;->getDecoratedRight(Landroid/view/View;)I

    move-result v2

    .line 1962
    .local v2, "maxRight":I
    invoke-virtual {p1}, Landroidx/recyclerview/widget/RecyclerView;->getWidth()I

    move-result v3

    invoke-virtual {p1}, Landroidx/recyclerview/widget/RecyclerView;->getPaddingRight()I

    move-result v4

    sub-int/2addr v3, v4

    if-lt v2, v3, :cond_2

    .line 1963
    invoke-virtual {p1, p5}, Landroidx/recyclerview/widget/RecyclerView;->scrollToPosition(I)V

    .line 1967
    .end local v1    # "minLeft":I
    .end local v2    # "maxRight":I
    :cond_2
    invoke-virtual {v0}, Landroidx/recyclerview/widget/RecyclerView$LayoutManager;->canScrollVertically()Z

    move-result v1

    if-eqz v1, :cond_4

    .line 1968
    iget-object v1, p4, Landroidx/recyclerview/widget/RecyclerView$ViewHolder;->itemView:Landroid/view/View;

    invoke-virtual {v0, v1}, Landroidx/recyclerview/widget/RecyclerView$LayoutManager;->getDecoratedTop(Landroid/view/View;)I

    move-result v1

    .line 1969
    .local v1, "minTop":I
    invoke-virtual {p1}, Landroidx/recyclerview/widget/RecyclerView;->getPaddingTop()I

    move-result v2

    if-gt v1, v2, :cond_3

    .line 1970
    invoke-virtual {p1, p5}, Landroidx/recyclerview/widget/RecyclerView;->scrollToPosition(I)V

    .line 1972
    :cond_3
    iget-object v2, p4, Landroidx/recyclerview/widget/RecyclerView$ViewHolder;->itemView:Landroid/view/View;

    invoke-virtual {v0, v2}, Landroidx/recyclerview/widget/RecyclerView$LayoutManager;->getDecoratedBottom(Landroid/view/View;)I

    move-result v2

    .line 1973
    .local v2, "maxBottom":I
    invoke-virtual {p1}, Landroidx/recyclerview/widget/RecyclerView;->getHeight()I

    move-result v3

    invoke-virtual {p1}, Landroidx/recyclerview/widget/RecyclerView;->getPaddingBottom()I

    move-result v4

    sub-int/2addr v3, v4

    if-lt v2, v3, :cond_4

    .line 1974
    invoke-virtual {p1, p5}, Landroidx/recyclerview/widget/RecyclerView;->scrollToPosition(I)V

    .line 1977
    .end local v1    # "minTop":I
    .end local v2    # "maxBottom":I
    :cond_4
    return-void
.end method

.method public onSelectedChanged(Landroidx/recyclerview/widget/RecyclerView$ViewHolder;I)V
    .locals 2
    .param p1, "viewHolder"    # Landroidx/recyclerview/widget/RecyclerView$ViewHolder;
    .param p2, "actionState"    # I

    .line 1899
    if-eqz p1, :cond_0

    .line 1900
    sget-object v0, Landroidx/recyclerview/widget/ItemTouchUIUtilImpl;->INSTANCE:Landroidx/recyclerview/widget/ItemTouchUIUtil;

    iget-object v1, p1, Landroidx/recyclerview/widget/RecyclerView$ViewHolder;->itemView:Landroid/view/View;

    invoke-interface {v0, v1}, Landroidx/recyclerview/widget/ItemTouchUIUtil;->onSelected(Landroid/view/View;)V

    .line 1902
    :cond_0
    return-void
.end method

.method public abstract onSwiped(Landroidx/recyclerview/widget/RecyclerView$ViewHolder;I)V
.end method
