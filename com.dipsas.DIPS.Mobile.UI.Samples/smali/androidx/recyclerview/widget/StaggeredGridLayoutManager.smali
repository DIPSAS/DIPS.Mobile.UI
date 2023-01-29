.class public Landroidx/recyclerview/widget/StaggeredGridLayoutManager;
.super Landroidx/recyclerview/widget/RecyclerView$LayoutManager;
.source "StaggeredGridLayoutManager.java"

# interfaces
.implements Landroidx/recyclerview/widget/RecyclerView$SmoothScroller$ScrollVectorProvider;


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Landroidx/recyclerview/widget/StaggeredGridLayoutManager$AnchorInfo;,
        Landroidx/recyclerview/widget/StaggeredGridLayoutManager$SavedState;,
        Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup;,
        Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;,
        Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LayoutParams;
    }
.end annotation


# static fields
.field static final DEBUG:Z = false

.field public static final GAP_HANDLING_LAZY:I = 0x1
    .annotation runtime Ljava/lang/Deprecated;
    .end annotation
.end field

.field public static final GAP_HANDLING_MOVE_ITEMS_BETWEEN_SPANS:I = 0x2

.field public static final GAP_HANDLING_NONE:I = 0x0

.field public static final HORIZONTAL:I = 0x0

.field static final INVALID_OFFSET:I = -0x80000000

.field private static final MAX_SCROLL_FACTOR:F = 0.33333334f

.field private static final TAG:Ljava/lang/String; = "StaggeredGridLManager"

.field public static final VERTICAL:I = 0x1


# instance fields
.field private final mAnchorInfo:Landroidx/recyclerview/widget/StaggeredGridLayoutManager$AnchorInfo;

.field private final mCheckForGapsRunnable:Ljava/lang/Runnable;

.field private mFullSizeSpec:I

.field private mGapStrategy:I

.field private mLaidOutInvalidFullSpan:Z

.field private mLastLayoutFromEnd:Z

.field private mLastLayoutRTL:Z

.field private final mLayoutState:Landroidx/recyclerview/widget/LayoutState;

.field mLazySpanLookup:Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup;

.field private mOrientation:I

.field private mPendingSavedState:Landroidx/recyclerview/widget/StaggeredGridLayoutManager$SavedState;

.field mPendingScrollPosition:I

.field mPendingScrollPositionOffset:I

.field private mPrefetchDistances:[I

.field mPrimaryOrientation:Landroidx/recyclerview/widget/OrientationHelper;

.field private mRemainingSpans:Ljava/util/BitSet;

.field mReverseLayout:Z

.field mSecondaryOrientation:Landroidx/recyclerview/widget/OrientationHelper;

.field mShouldReverseLayout:Z

.field private mSizePerSpan:I

.field private mSmoothScrollbarEnabled:Z

.field private mSpanCount:I

.field mSpans:[Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;

.field private final mTmpRect:Landroid/graphics/Rect;


# direct methods
.method public constructor <init>(II)V
    .locals 2
    .param p1, "spanCount"    # I
    .param p2, "orientation"    # I

    .line 244
    invoke-direct {p0}, Landroidx/recyclerview/widget/RecyclerView$LayoutManager;-><init>()V

    .line 106
    const/4 v0, -0x1

    iput v0, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mSpanCount:I

    .line 129
    const/4 v1, 0x0

    iput-boolean v1, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mReverseLayout:Z

    .line 134
    iput-boolean v1, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mShouldReverseLayout:Z

    .line 145
    iput v0, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mPendingScrollPosition:I

    .line 151
    const/high16 v0, -0x80000000

    iput v0, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mPendingScrollPositionOffset:I

    .line 157
    new-instance v0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup;

    invoke-direct {v0}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup;-><init>()V

    iput-object v0, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mLazySpanLookup:Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup;

    .line 162
    const/4 v0, 0x2

    iput v0, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mGapStrategy:I

    .line 188
    new-instance v0, Landroid/graphics/Rect;

    invoke-direct {v0}, Landroid/graphics/Rect;-><init>()V

    iput-object v0, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mTmpRect:Landroid/graphics/Rect;

    .line 193
    new-instance v0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$AnchorInfo;

    invoke-direct {v0, p0}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$AnchorInfo;-><init>(Landroidx/recyclerview/widget/StaggeredGridLayoutManager;)V

    iput-object v0, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mAnchorInfo:Landroidx/recyclerview/widget/StaggeredGridLayoutManager$AnchorInfo;

    .line 201
    iput-boolean v1, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mLaidOutInvalidFullSpan:Z

    .line 207
    const/4 v0, 0x1

    iput-boolean v0, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mSmoothScrollbarEnabled:Z

    .line 215
    new-instance v0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$1;

    invoke-direct {v0, p0}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$1;-><init>(Landroidx/recyclerview/widget/StaggeredGridLayoutManager;)V

    iput-object v0, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mCheckForGapsRunnable:Ljava/lang/Runnable;

    .line 245
    iput p2, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mOrientation:I

    .line 246
    invoke-virtual {p0, p1}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->setSpanCount(I)V

    .line 247
    new-instance v0, Landroidx/recyclerview/widget/LayoutState;

    invoke-direct {v0}, Landroidx/recyclerview/widget/LayoutState;-><init>()V

    iput-object v0, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mLayoutState:Landroidx/recyclerview/widget/LayoutState;

    .line 248
    invoke-direct {p0}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->createOrientationHelpers()V

    .line 249
    return-void
.end method

.method public constructor <init>(Landroid/content/Context;Landroid/util/AttributeSet;II)V
    .locals 2
    .param p1, "context"    # Landroid/content/Context;
    .param p2, "attrs"    # Landroid/util/AttributeSet;
    .param p3, "defStyleAttr"    # I
    .param p4, "defStyleRes"    # I

    .line 228
    invoke-direct {p0}, Landroidx/recyclerview/widget/RecyclerView$LayoutManager;-><init>()V

    .line 106
    const/4 v0, -0x1

    iput v0, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mSpanCount:I

    .line 129
    const/4 v1, 0x0

    iput-boolean v1, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mReverseLayout:Z

    .line 134
    iput-boolean v1, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mShouldReverseLayout:Z

    .line 145
    iput v0, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mPendingScrollPosition:I

    .line 151
    const/high16 v0, -0x80000000

    iput v0, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mPendingScrollPositionOffset:I

    .line 157
    new-instance v0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup;

    invoke-direct {v0}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup;-><init>()V

    iput-object v0, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mLazySpanLookup:Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup;

    .line 162
    const/4 v0, 0x2

    iput v0, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mGapStrategy:I

    .line 188
    new-instance v0, Landroid/graphics/Rect;

    invoke-direct {v0}, Landroid/graphics/Rect;-><init>()V

    iput-object v0, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mTmpRect:Landroid/graphics/Rect;

    .line 193
    new-instance v0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$AnchorInfo;

    invoke-direct {v0, p0}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$AnchorInfo;-><init>(Landroidx/recyclerview/widget/StaggeredGridLayoutManager;)V

    iput-object v0, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mAnchorInfo:Landroidx/recyclerview/widget/StaggeredGridLayoutManager$AnchorInfo;

    .line 201
    iput-boolean v1, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mLaidOutInvalidFullSpan:Z

    .line 207
    const/4 v0, 0x1

    iput-boolean v0, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mSmoothScrollbarEnabled:Z

    .line 215
    new-instance v0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$1;

    invoke-direct {v0, p0}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$1;-><init>(Landroidx/recyclerview/widget/StaggeredGridLayoutManager;)V

    iput-object v0, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mCheckForGapsRunnable:Ljava/lang/Runnable;

    .line 229
    invoke-static {p1, p2, p3, p4}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->getProperties(Landroid/content/Context;Landroid/util/AttributeSet;II)Landroidx/recyclerview/widget/RecyclerView$LayoutManager$Properties;

    move-result-object v0

    .line 230
    .local v0, "properties":Landroidx/recyclerview/widget/RecyclerView$LayoutManager$Properties;
    iget v1, v0, Landroidx/recyclerview/widget/RecyclerView$LayoutManager$Properties;->orientation:I

    invoke-virtual {p0, v1}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->setOrientation(I)V

    .line 231
    iget v1, v0, Landroidx/recyclerview/widget/RecyclerView$LayoutManager$Properties;->spanCount:I

    invoke-virtual {p0, v1}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->setSpanCount(I)V

    .line 232
    iget-boolean v1, v0, Landroidx/recyclerview/widget/RecyclerView$LayoutManager$Properties;->reverseLayout:Z

    invoke-virtual {p0, v1}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->setReverseLayout(Z)V

    .line 233
    new-instance v1, Landroidx/recyclerview/widget/LayoutState;

    invoke-direct {v1}, Landroidx/recyclerview/widget/LayoutState;-><init>()V

    iput-object v1, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mLayoutState:Landroidx/recyclerview/widget/LayoutState;

    .line 234
    invoke-direct {p0}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->createOrientationHelpers()V

    .line 235
    return-void
.end method

.method private appendViewToAllSpans(Landroid/view/View;)V
    .locals 2
    .param p1, "view"    # Landroid/view/View;

    .line 1780
    iget v0, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mSpanCount:I

    add-int/lit8 v0, v0, -0x1

    .local v0, "i":I
    :goto_0
    if-ltz v0, :cond_0

    .line 1781
    iget-object v1, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mSpans:[Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;

    aget-object v1, v1, v0

    invoke-virtual {v1, p1}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;->appendToSpan(Landroid/view/View;)V

    .line 1780
    add-int/lit8 v0, v0, -0x1

    goto :goto_0

    .line 1783
    .end local v0    # "i":I
    :cond_0
    return-void
.end method

.method private applyPendingSavedState(Landroidx/recyclerview/widget/StaggeredGridLayoutManager$AnchorInfo;)V
    .locals 3
    .param p1, "anchorInfo"    # Landroidx/recyclerview/widget/StaggeredGridLayoutManager$AnchorInfo;

    .line 794
    iget-object v0, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mPendingSavedState:Landroidx/recyclerview/widget/StaggeredGridLayoutManager$SavedState;

    iget v0, v0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$SavedState;->mSpanOffsetsSize:I

    if-lez v0, :cond_4

    .line 795
    iget-object v0, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mPendingSavedState:Landroidx/recyclerview/widget/StaggeredGridLayoutManager$SavedState;

    iget v0, v0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$SavedState;->mSpanOffsetsSize:I

    iget v1, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mSpanCount:I

    if-ne v0, v1, :cond_3

    .line 796
    const/4 v0, 0x0

    .local v0, "i":I
    :goto_0
    iget v1, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mSpanCount:I

    if-ge v0, v1, :cond_2

    .line 797
    iget-object v1, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mSpans:[Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;

    aget-object v1, v1, v0

    invoke-virtual {v1}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;->clear()V

    .line 798
    iget-object v1, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mPendingSavedState:Landroidx/recyclerview/widget/StaggeredGridLayoutManager$SavedState;

    iget-object v1, v1, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$SavedState;->mSpanOffsets:[I

    aget v1, v1, v0

    .line 799
    .local v1, "line":I
    const/high16 v2, -0x80000000

    if-eq v1, v2, :cond_1

    .line 800
    iget-object v2, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mPendingSavedState:Landroidx/recyclerview/widget/StaggeredGridLayoutManager$SavedState;

    iget-boolean v2, v2, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$SavedState;->mAnchorLayoutFromEnd:Z

    if-eqz v2, :cond_0

    .line 801
    iget-object v2, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mPrimaryOrientation:Landroidx/recyclerview/widget/OrientationHelper;

    invoke-virtual {v2}, Landroidx/recyclerview/widget/OrientationHelper;->getEndAfterPadding()I

    move-result v2

    add-int/2addr v1, v2

    goto :goto_1

    .line 803
    :cond_0
    iget-object v2, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mPrimaryOrientation:Landroidx/recyclerview/widget/OrientationHelper;

    invoke-virtual {v2}, Landroidx/recyclerview/widget/OrientationHelper;->getStartAfterPadding()I

    move-result v2

    add-int/2addr v1, v2

    .line 806
    :cond_1
    :goto_1
    iget-object v2, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mSpans:[Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;

    aget-object v2, v2, v0

    invoke-virtual {v2, v1}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;->setLine(I)V

    .line 796
    .end local v1    # "line":I
    add-int/lit8 v0, v0, 0x1

    goto :goto_0

    .end local v0    # "i":I
    :cond_2
    goto :goto_2

    .line 809
    :cond_3
    iget-object v0, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mPendingSavedState:Landroidx/recyclerview/widget/StaggeredGridLayoutManager$SavedState;

    invoke-virtual {v0}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$SavedState;->invalidateSpanInfo()V

    .line 810
    iget-object v0, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mPendingSavedState:Landroidx/recyclerview/widget/StaggeredGridLayoutManager$SavedState;

    iget v1, v0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$SavedState;->mVisibleAnchorPosition:I

    iput v1, v0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$SavedState;->mAnchorPosition:I

    .line 813
    :cond_4
    :goto_2
    iget-object v0, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mPendingSavedState:Landroidx/recyclerview/widget/StaggeredGridLayoutManager$SavedState;

    iget-boolean v0, v0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$SavedState;->mLastLayoutRTL:Z

    iput-boolean v0, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mLastLayoutRTL:Z

    .line 814
    iget-object v0, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mPendingSavedState:Landroidx/recyclerview/widget/StaggeredGridLayoutManager$SavedState;

    iget-boolean v0, v0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$SavedState;->mReverseLayout:Z

    invoke-virtual {p0, v0}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->setReverseLayout(Z)V

    .line 815
    invoke-direct {p0}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->resolveShouldLayoutReverse()V

    .line 817
    iget-object v0, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mPendingSavedState:Landroidx/recyclerview/widget/StaggeredGridLayoutManager$SavedState;

    iget v0, v0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$SavedState;->mAnchorPosition:I

    const/4 v1, -0x1

    if-eq v0, v1, :cond_5

    .line 818
    iget-object v0, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mPendingSavedState:Landroidx/recyclerview/widget/StaggeredGridLayoutManager$SavedState;

    iget v0, v0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$SavedState;->mAnchorPosition:I

    iput v0, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mPendingScrollPosition:I

    .line 819
    iget-object v0, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mPendingSavedState:Landroidx/recyclerview/widget/StaggeredGridLayoutManager$SavedState;

    iget-boolean v0, v0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$SavedState;->mAnchorLayoutFromEnd:Z

    iput-boolean v0, p1, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$AnchorInfo;->mLayoutFromEnd:Z

    goto :goto_3

    .line 821
    :cond_5
    iget-boolean v0, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mShouldReverseLayout:Z

    iput-boolean v0, p1, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$AnchorInfo;->mLayoutFromEnd:Z

    .line 823
    :goto_3
    iget-object v0, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mPendingSavedState:Landroidx/recyclerview/widget/StaggeredGridLayoutManager$SavedState;

    iget v0, v0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$SavedState;->mSpanLookupSize:I

    const/4 v1, 0x1

    if-le v0, v1, :cond_6

    .line 824
    iget-object v0, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mLazySpanLookup:Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup;

    iget-object v1, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mPendingSavedState:Landroidx/recyclerview/widget/StaggeredGridLayoutManager$SavedState;

    iget-object v1, v1, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$SavedState;->mSpanLookup:[I

    iput-object v1, v0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup;->mData:[I

    .line 825
    iget-object v0, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mLazySpanLookup:Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup;

    iget-object v1, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mPendingSavedState:Landroidx/recyclerview/widget/StaggeredGridLayoutManager$SavedState;

    iget-object v1, v1, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$SavedState;->mFullSpanItems:Ljava/util/List;

    iput-object v1, v0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup;->mFullSpanItems:Ljava/util/List;

    .line 827
    :cond_6
    return-void
.end method

.method private attachViewToSpans(Landroid/view/View;Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LayoutParams;Landroidx/recyclerview/widget/LayoutState;)V
    .locals 2
    .param p1, "view"    # Landroid/view/View;
    .param p2, "lp"    # Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LayoutParams;
    .param p3, "layoutState"    # Landroidx/recyclerview/widget/LayoutState;

    .line 1724
    iget v0, p3, Landroidx/recyclerview/widget/LayoutState;->mLayoutDirection:I

    const/4 v1, 0x1

    if-ne v0, v1, :cond_1

    .line 1725
    iget-boolean v0, p2, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LayoutParams;->mFullSpan:Z

    if-eqz v0, :cond_0

    .line 1726
    invoke-direct {p0, p1}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->appendViewToAllSpans(Landroid/view/View;)V

    goto :goto_0

    .line 1728
    :cond_0
    iget-object v0, p2, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LayoutParams;->mSpan:Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;

    invoke-virtual {v0, p1}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;->appendToSpan(Landroid/view/View;)V

    goto :goto_0

    .line 1731
    :cond_1
    iget-boolean v0, p2, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LayoutParams;->mFullSpan:Z

    if-eqz v0, :cond_2

    .line 1732
    invoke-direct {p0, p1}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->prependViewToAllSpans(Landroid/view/View;)V

    goto :goto_0

    .line 1734
    :cond_2
    iget-object v0, p2, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LayoutParams;->mSpan:Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;

    invoke-virtual {v0, p1}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;->prependToSpan(Landroid/view/View;)V

    .line 1737
    :goto_0
    return-void
.end method

.method private calculateScrollDirectionForPosition(I)I
    .locals 5
    .param p1, "position"    # I

    .line 2017
    invoke-virtual {p0}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->getChildCount()I

    move-result v0

    const/4 v1, -0x1

    const/4 v2, 0x1

    if-nez v0, :cond_1

    .line 2018
    iget-boolean v0, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mShouldReverseLayout:Z

    if-eqz v0, :cond_0

    const/4 v1, 0x1

    :cond_0
    return v1

    .line 2020
    :cond_1
    invoke-virtual {p0}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->getFirstChildPosition()I

    move-result v0

    .line 2021
    .local v0, "firstChildPos":I
    if-ge p1, v0, :cond_2

    const/4 v3, 0x1

    goto :goto_0

    :cond_2
    const/4 v3, 0x0

    :goto_0
    iget-boolean v4, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mShouldReverseLayout:Z

    if-eq v3, v4, :cond_3

    goto :goto_1

    :cond_3
    const/4 v1, 0x1

    :goto_1
    return v1
.end method

.method private checkSpanForGap(Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;)Z
    .locals 3
    .param p1, "span"    # Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;

    .line 402
    iget-boolean v0, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mShouldReverseLayout:Z

    const/4 v1, 0x0

    if-eqz v0, :cond_0

    .line 403
    invoke-virtual {p1}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;->getEndLine()I

    move-result v0

    iget-object v2, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mPrimaryOrientation:Landroidx/recyclerview/widget/OrientationHelper;

    invoke-virtual {v2}, Landroidx/recyclerview/widget/OrientationHelper;->getEndAfterPadding()I

    move-result v2

    if-ge v0, v2, :cond_1

    .line 405
    iget-object v0, p1, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;->mViews:Ljava/util/ArrayList;

    iget-object v1, p1, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;->mViews:Ljava/util/ArrayList;

    invoke-virtual {v1}, Ljava/util/ArrayList;->size()I

    move-result v1

    add-int/lit8 v1, v1, -0x1

    invoke-virtual {v0, v1}, Ljava/util/ArrayList;->get(I)Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Landroid/view/View;

    .line 406
    .local v0, "endView":Landroid/view/View;
    invoke-virtual {p1, v0}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;->getLayoutParams(Landroid/view/View;)Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LayoutParams;

    move-result-object v1

    .line 407
    .local v1, "lp":Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LayoutParams;
    iget-boolean v2, v1, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LayoutParams;->mFullSpan:Z

    xor-int/lit8 v2, v2, 0x1

    return v2

    .line 409
    .end local v0    # "endView":Landroid/view/View;
    .end local v1    # "lp":Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LayoutParams;
    :cond_0
    invoke-virtual {p1}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;->getStartLine()I

    move-result v0

    iget-object v2, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mPrimaryOrientation:Landroidx/recyclerview/widget/OrientationHelper;

    invoke-virtual {v2}, Landroidx/recyclerview/widget/OrientationHelper;->getStartAfterPadding()I

    move-result v2

    if-le v0, v2, :cond_1

    .line 411
    iget-object v0, p1, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;->mViews:Ljava/util/ArrayList;

    invoke-virtual {v0, v1}, Ljava/util/ArrayList;->get(I)Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Landroid/view/View;

    .line 412
    .local v0, "startView":Landroid/view/View;
    invoke-virtual {p1, v0}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;->getLayoutParams(Landroid/view/View;)Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LayoutParams;

    move-result-object v1

    .line 413
    .restart local v1    # "lp":Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LayoutParams;
    iget-boolean v2, v1, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LayoutParams;->mFullSpan:Z

    xor-int/lit8 v2, v2, 0x1

    return v2

    .line 415
    .end local v0    # "startView":Landroid/view/View;
    .end local v1    # "lp":Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LayoutParams;
    :cond_1
    return v1
.end method

.method private computeScrollExtent(Landroidx/recyclerview/widget/RecyclerView$State;)I
    .locals 7
    .param p1, "state"    # Landroidx/recyclerview/widget/RecyclerView$State;

    .line 1100
    invoke-virtual {p0}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->getChildCount()I

    move-result v0

    if-nez v0, :cond_0

    .line 1101
    const/4 v0, 0x0

    return v0

    .line 1103
    :cond_0
    iget-object v2, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mPrimaryOrientation:Landroidx/recyclerview/widget/OrientationHelper;

    iget-boolean v0, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mSmoothScrollbarEnabled:Z

    .line 1104
    xor-int/lit8 v0, v0, 0x1

    invoke-virtual {p0, v0}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->findFirstVisibleItemClosestToStart(Z)Landroid/view/View;

    move-result-object v3

    iget-boolean v0, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mSmoothScrollbarEnabled:Z

    .line 1105
    xor-int/lit8 v0, v0, 0x1

    invoke-virtual {p0, v0}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->findFirstVisibleItemClosestToEnd(Z)Landroid/view/View;

    move-result-object v4

    iget-boolean v6, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mSmoothScrollbarEnabled:Z

    .line 1103
    move-object v1, p1

    move-object v5, p0

    invoke-static/range {v1 .. v6}, Landroidx/recyclerview/widget/ScrollbarHelper;->computeScrollExtent(Landroidx/recyclerview/widget/RecyclerView$State;Landroidx/recyclerview/widget/OrientationHelper;Landroid/view/View;Landroid/view/View;Landroidx/recyclerview/widget/RecyclerView$LayoutManager;Z)I

    move-result v0

    return v0
.end method

.method private computeScrollOffset(Landroidx/recyclerview/widget/RecyclerView$State;)I
    .locals 8
    .param p1, "state"    # Landroidx/recyclerview/widget/RecyclerView$State;

    .line 1080
    invoke-virtual {p0}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->getChildCount()I

    move-result v0

    if-nez v0, :cond_0

    .line 1081
    const/4 v0, 0x0

    return v0

    .line 1083
    :cond_0
    iget-object v2, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mPrimaryOrientation:Landroidx/recyclerview/widget/OrientationHelper;

    iget-boolean v0, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mSmoothScrollbarEnabled:Z

    .line 1084
    xor-int/lit8 v0, v0, 0x1

    invoke-virtual {p0, v0}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->findFirstVisibleItemClosestToStart(Z)Landroid/view/View;

    move-result-object v3

    iget-boolean v0, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mSmoothScrollbarEnabled:Z

    .line 1085
    xor-int/lit8 v0, v0, 0x1

    invoke-virtual {p0, v0}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->findFirstVisibleItemClosestToEnd(Z)Landroid/view/View;

    move-result-object v4

    iget-boolean v6, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mSmoothScrollbarEnabled:Z

    iget-boolean v7, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mShouldReverseLayout:Z

    .line 1083
    move-object v1, p1

    move-object v5, p0

    invoke-static/range {v1 .. v7}, Landroidx/recyclerview/widget/ScrollbarHelper;->computeScrollOffset(Landroidx/recyclerview/widget/RecyclerView$State;Landroidx/recyclerview/widget/OrientationHelper;Landroid/view/View;Landroid/view/View;Landroidx/recyclerview/widget/RecyclerView$LayoutManager;ZZ)I

    move-result v0

    return v0
.end method

.method private computeScrollRange(Landroidx/recyclerview/widget/RecyclerView$State;)I
    .locals 7
    .param p1, "state"    # Landroidx/recyclerview/widget/RecyclerView$State;

    .line 1120
    invoke-virtual {p0}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->getChildCount()I

    move-result v0

    if-nez v0, :cond_0

    .line 1121
    const/4 v0, 0x0

    return v0

    .line 1123
    :cond_0
    iget-object v2, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mPrimaryOrientation:Landroidx/recyclerview/widget/OrientationHelper;

    iget-boolean v0, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mSmoothScrollbarEnabled:Z

    .line 1124
    xor-int/lit8 v0, v0, 0x1

    invoke-virtual {p0, v0}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->findFirstVisibleItemClosestToStart(Z)Landroid/view/View;

    move-result-object v3

    iget-boolean v0, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mSmoothScrollbarEnabled:Z

    .line 1125
    xor-int/lit8 v0, v0, 0x1

    invoke-virtual {p0, v0}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->findFirstVisibleItemClosestToEnd(Z)Landroid/view/View;

    move-result-object v4

    iget-boolean v6, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mSmoothScrollbarEnabled:Z

    .line 1123
    move-object v1, p1

    move-object v5, p0

    invoke-static/range {v1 .. v6}, Landroidx/recyclerview/widget/ScrollbarHelper;->computeScrollRange(Landroidx/recyclerview/widget/RecyclerView$State;Landroidx/recyclerview/widget/OrientationHelper;Landroid/view/View;Landroid/view/View;Landroidx/recyclerview/widget/RecyclerView$LayoutManager;Z)I

    move-result v0

    return v0
.end method

.method private convertFocusDirectionToLayoutDirection(I)I
    .locals 4
    .param p1, "focusDirection"    # I

    .line 2366
    const/4 v0, -0x1

    const/high16 v1, -0x80000000

    const/4 v2, 0x1

    sparse-switch p1, :sswitch_data_0

    .line 2399
    return v1

    .line 2387
    :sswitch_0
    iget v0, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mOrientation:I

    if-ne v0, v2, :cond_0

    const/4 v1, 0x1

    goto :goto_0

    .line 2388
    :cond_0
    nop

    .line 2387
    :goto_0
    return v1

    .line 2393
    :sswitch_1
    iget v0, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mOrientation:I

    if-nez v0, :cond_1

    const/4 v1, 0x1

    goto :goto_1

    .line 2394
    :cond_1
    nop

    .line 2393
    :goto_1
    return v1

    .line 2384
    :sswitch_2
    iget v3, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mOrientation:I

    if-ne v3, v2, :cond_2

    goto :goto_2

    .line 2385
    :cond_2
    const/high16 v0, -0x80000000

    .line 2384
    :goto_2
    return v0

    .line 2390
    :sswitch_3
    iget v2, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mOrientation:I

    if-nez v2, :cond_3

    goto :goto_3

    .line 2391
    :cond_3
    const/high16 v0, -0x80000000

    .line 2390
    :goto_3
    return v0

    .line 2376
    :sswitch_4
    iget v1, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mOrientation:I

    if-ne v1, v2, :cond_4

    .line 2377
    return v2

    .line 2378
    :cond_4
    invoke-virtual {p0}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->isLayoutRTL()Z

    move-result v1

    if-eqz v1, :cond_5

    .line 2379
    return v0

    .line 2381
    :cond_5
    return v2

    .line 2368
    :sswitch_5
    iget v1, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mOrientation:I

    if-ne v1, v2, :cond_6

    .line 2369
    return v0

    .line 2370
    :cond_6
    invoke-virtual {p0}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->isLayoutRTL()Z

    move-result v1

    if-eqz v1, :cond_7

    .line 2371
    return v2

    .line 2373
    :cond_7
    return v0

    :sswitch_data_0
    .sparse-switch
        0x1 -> :sswitch_5
        0x2 -> :sswitch_4
        0x11 -> :sswitch_3
        0x21 -> :sswitch_2
        0x42 -> :sswitch_1
        0x82 -> :sswitch_0
    .end sparse-switch
.end method

.method private createFullSpanItemFromEnd(I)Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup$FullSpanItem;
    .locals 4
    .param p1, "newItemTop"    # I

    .line 1706
    new-instance v0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup$FullSpanItem;

    invoke-direct {v0}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup$FullSpanItem;-><init>()V

    .line 1707
    .local v0, "fsi":Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup$FullSpanItem;
    iget v1, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mSpanCount:I

    new-array v1, v1, [I

    iput-object v1, v0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup$FullSpanItem;->mGapPerSpan:[I

    .line 1708
    const/4 v1, 0x0

    .local v1, "i":I
    :goto_0
    iget v2, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mSpanCount:I

    if-ge v1, v2, :cond_0

    .line 1709
    iget-object v2, v0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup$FullSpanItem;->mGapPerSpan:[I

    iget-object v3, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mSpans:[Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;

    aget-object v3, v3, v1

    invoke-virtual {v3, p1}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;->getEndLine(I)I

    move-result v3

    sub-int v3, p1, v3

    aput v3, v2, v1

    .line 1708
    add-int/lit8 v1, v1, 0x1

    goto :goto_0

    .line 1711
    .end local v1    # "i":I
    :cond_0
    return-object v0
.end method

.method private createFullSpanItemFromStart(I)Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup$FullSpanItem;
    .locals 4
    .param p1, "newItemBottom"    # I

    .line 1715
    new-instance v0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup$FullSpanItem;

    invoke-direct {v0}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup$FullSpanItem;-><init>()V

    .line 1716
    .local v0, "fsi":Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup$FullSpanItem;
    iget v1, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mSpanCount:I

    new-array v1, v1, [I

    iput-object v1, v0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup$FullSpanItem;->mGapPerSpan:[I

    .line 1717
    const/4 v1, 0x0

    .local v1, "i":I
    :goto_0
    iget v2, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mSpanCount:I

    if-ge v1, v2, :cond_0

    .line 1718
    iget-object v2, v0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup$FullSpanItem;->mGapPerSpan:[I

    iget-object v3, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mSpans:[Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;

    aget-object v3, v3, v1

    invoke-virtual {v3, p1}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;->getStartLine(I)I

    move-result v3

    sub-int/2addr v3, p1

    aput v3, v2, v1

    .line 1717
    add-int/lit8 v1, v1, 0x1

    goto :goto_0

    .line 1720
    .end local v1    # "i":I
    :cond_0
    return-object v0
.end method

.method private createOrientationHelpers()V
    .locals 1

    .line 257
    iget v0, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mOrientation:I

    invoke-static {p0, v0}, Landroidx/recyclerview/widget/OrientationHelper;->createOrientationHelper(Landroidx/recyclerview/widget/RecyclerView$LayoutManager;I)Landroidx/recyclerview/widget/OrientationHelper;

    move-result-object v0

    iput-object v0, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mPrimaryOrientation:Landroidx/recyclerview/widget/OrientationHelper;

    .line 258
    iget v0, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mOrientation:I

    rsub-int/lit8 v0, v0, 0x1

    .line 259
    invoke-static {p0, v0}, Landroidx/recyclerview/widget/OrientationHelper;->createOrientationHelper(Landroidx/recyclerview/widget/RecyclerView$LayoutManager;I)Landroidx/recyclerview/widget/OrientationHelper;

    move-result-object v0

    iput-object v0, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mSecondaryOrientation:Landroidx/recyclerview/widget/OrientationHelper;

    .line 260
    return-void
.end method

.method private fill(Landroidx/recyclerview/widget/RecyclerView$Recycler;Landroidx/recyclerview/widget/LayoutState;Landroidx/recyclerview/widget/RecyclerView$State;)I
    .locals 22
    .param p1, "recycler"    # Landroidx/recyclerview/widget/RecyclerView$Recycler;
    .param p2, "layoutState"    # Landroidx/recyclerview/widget/LayoutState;
    .param p3, "state"    # Landroidx/recyclerview/widget/RecyclerView$State;

    .line 1549
    move-object/from16 v6, p0

    move-object/from16 v7, p1

    move-object/from16 v8, p2

    iget-object v0, v6, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mRemainingSpans:Ljava/util/BitSet;

    iget v1, v6, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mSpanCount:I

    const/4 v9, 0x0

    const/4 v10, 0x1

    invoke-virtual {v0, v9, v1, v10}, Ljava/util/BitSet;->set(IIZ)V

    .line 1554
    iget-object v0, v6, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mLayoutState:Landroidx/recyclerview/widget/LayoutState;

    iget-boolean v0, v0, Landroidx/recyclerview/widget/LayoutState;->mInfinite:Z

    if-eqz v0, :cond_1

    .line 1555
    iget v0, v8, Landroidx/recyclerview/widget/LayoutState;->mLayoutDirection:I

    if-ne v0, v10, :cond_0

    .line 1556
    const v0, 0x7fffffff

    move v11, v0

    .local v0, "targetLine":I
    goto :goto_0

    .line 1558
    .end local v0    # "targetLine":I
    :cond_0
    const/high16 v0, -0x80000000

    move v11, v0

    .restart local v0    # "targetLine":I
    goto :goto_0

    .line 1561
    .end local v0    # "targetLine":I
    :cond_1
    iget v0, v8, Landroidx/recyclerview/widget/LayoutState;->mLayoutDirection:I

    if-ne v0, v10, :cond_2

    .line 1562
    iget v0, v8, Landroidx/recyclerview/widget/LayoutState;->mEndLine:I

    iget v1, v8, Landroidx/recyclerview/widget/LayoutState;->mAvailable:I

    add-int/2addr v0, v1

    move v11, v0

    .restart local v0    # "targetLine":I
    goto :goto_0

    .line 1564
    .end local v0    # "targetLine":I
    :cond_2
    iget v0, v8, Landroidx/recyclerview/widget/LayoutState;->mStartLine:I

    iget v1, v8, Landroidx/recyclerview/widget/LayoutState;->mAvailable:I

    sub-int/2addr v0, v1

    move v11, v0

    .line 1568
    .local v11, "targetLine":I
    :goto_0
    iget v0, v8, Landroidx/recyclerview/widget/LayoutState;->mLayoutDirection:I

    invoke-direct {v6, v0, v11}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->updateAllRemainingSpans(II)V

    .line 1575
    iget-boolean v0, v6, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mShouldReverseLayout:Z

    if-eqz v0, :cond_3

    .line 1576
    iget-object v0, v6, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mPrimaryOrientation:Landroidx/recyclerview/widget/OrientationHelper;

    invoke-virtual {v0}, Landroidx/recyclerview/widget/OrientationHelper;->getEndAfterPadding()I

    move-result v0

    goto :goto_1

    .line 1577
    :cond_3
    iget-object v0, v6, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mPrimaryOrientation:Landroidx/recyclerview/widget/OrientationHelper;

    invoke-virtual {v0}, Landroidx/recyclerview/widget/OrientationHelper;->getStartAfterPadding()I

    move-result v0

    :goto_1
    move v12, v0

    .line 1578
    .local v12, "defaultNewViewLine":I
    const/4 v0, 0x0

    move v13, v0

    .line 1579
    .local v13, "added":Z
    :goto_2
    invoke-virtual/range {p2 .. p3}, Landroidx/recyclerview/widget/LayoutState;->hasMore(Landroidx/recyclerview/widget/RecyclerView$State;)Z

    move-result v0

    const/4 v1, -0x1

    if-eqz v0, :cond_19

    iget-object v0, v6, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mLayoutState:Landroidx/recyclerview/widget/LayoutState;

    iget-boolean v0, v0, Landroidx/recyclerview/widget/LayoutState;->mInfinite:Z

    if-nez v0, :cond_5

    iget-object v0, v6, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mRemainingSpans:Ljava/util/BitSet;

    .line 1580
    invoke-virtual {v0}, Ljava/util/BitSet;->isEmpty()Z

    move-result v0

    if-nez v0, :cond_4

    goto :goto_3

    :cond_4
    const/4 v2, 0x0

    goto/16 :goto_13

    .line 1581
    :cond_5
    :goto_3
    invoke-virtual {v8, v7}, Landroidx/recyclerview/widget/LayoutState;->next(Landroidx/recyclerview/widget/RecyclerView$Recycler;)Landroid/view/View;

    move-result-object v14

    .line 1582
    .local v14, "view":Landroid/view/View;
    invoke-virtual {v14}, Landroid/view/View;->getLayoutParams()Landroid/view/ViewGroup$LayoutParams;

    move-result-object v0

    move-object v15, v0

    check-cast v15, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LayoutParams;

    .line 1583
    .local v15, "lp":Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LayoutParams;
    invoke-virtual {v15}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LayoutParams;->getViewLayoutPosition()I

    move-result v5

    .line 1584
    .local v5, "position":I
    iget-object v0, v6, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mLazySpanLookup:Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup;

    invoke-virtual {v0, v5}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup;->getSpan(I)I

    move-result v4

    .line 1586
    .local v4, "spanIndex":I
    if-ne v4, v1, :cond_6

    const/4 v0, 0x1

    goto :goto_4

    :cond_6
    const/4 v0, 0x0

    :goto_4
    move/from16 v16, v0

    .line 1587
    .local v16, "assignSpan":Z
    if-eqz v16, :cond_8

    .line 1588
    iget-boolean v0, v15, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LayoutParams;->mFullSpan:Z

    if-eqz v0, :cond_7

    iget-object v0, v6, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mSpans:[Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;

    aget-object v0, v0, v9

    goto :goto_5

    :cond_7
    invoke-direct {v6, v8}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->getNextSpan(Landroidx/recyclerview/widget/LayoutState;)Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;

    move-result-object v0

    .line 1589
    .local v0, "currentSpan":Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;
    :goto_5
    iget-object v2, v6, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mLazySpanLookup:Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup;

    invoke-virtual {v2, v5, v0}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup;->setSpan(ILandroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;)V

    move-object v3, v0

    goto :goto_6

    .line 1597
    .end local v0    # "currentSpan":Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;
    :cond_8
    iget-object v0, v6, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mSpans:[Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;

    aget-object v0, v0, v4

    move-object v3, v0

    .line 1600
    .local v3, "currentSpan":Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;
    :goto_6
    iput-object v3, v15, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LayoutParams;->mSpan:Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;

    .line 1601
    iget v0, v8, Landroidx/recyclerview/widget/LayoutState;->mLayoutDirection:I

    if-ne v0, v10, :cond_9

    .line 1602
    invoke-virtual {v6, v14}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->addView(Landroid/view/View;)V

    goto :goto_7

    .line 1604
    :cond_9
    invoke-virtual {v6, v14, v9}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->addView(Landroid/view/View;I)V

    .line 1606
    :goto_7
    invoke-direct {v6, v14, v15, v9}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->measureChildWithDecorationsAndMargin(Landroid/view/View;Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LayoutParams;Z)V

    .line 1610
    iget v0, v8, Landroidx/recyclerview/widget/LayoutState;->mLayoutDirection:I

    if-ne v0, v10, :cond_b

    .line 1611
    iget-boolean v0, v15, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LayoutParams;->mFullSpan:Z

    if-eqz v0, :cond_a

    invoke-direct {v6, v12}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->getMaxEnd(I)I

    move-result v0

    goto :goto_8

    .line 1612
    :cond_a
    invoke-virtual {v3, v12}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;->getEndLine(I)I

    move-result v0

    :goto_8
    nop

    .line 1613
    .local v0, "start":I
    iget-object v2, v6, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mPrimaryOrientation:Landroidx/recyclerview/widget/OrientationHelper;

    invoke-virtual {v2, v14}, Landroidx/recyclerview/widget/OrientationHelper;->getDecoratedMeasurement(Landroid/view/View;)I

    move-result v2

    add-int/2addr v2, v0

    .line 1614
    .local v2, "end":I
    if-eqz v16, :cond_d

    iget-boolean v9, v15, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LayoutParams;->mFullSpan:Z

    if-eqz v9, :cond_d

    .line 1616
    invoke-direct {v6, v0}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->createFullSpanItemFromEnd(I)Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup$FullSpanItem;

    move-result-object v9

    .line 1617
    .local v9, "fullSpanItem":Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup$FullSpanItem;
    iput v1, v9, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup$FullSpanItem;->mGapDir:I

    .line 1618
    iput v5, v9, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup$FullSpanItem;->mPosition:I

    .line 1619
    iget-object v1, v6, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mLazySpanLookup:Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup;

    invoke-virtual {v1, v9}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup;->addFullSpanItem(Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup$FullSpanItem;)V

    .line 1620
    .end local v9    # "fullSpanItem":Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup$FullSpanItem;
    goto :goto_a

    .line 1622
    .end local v0    # "start":I
    .end local v2    # "end":I
    :cond_b
    iget-boolean v0, v15, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LayoutParams;->mFullSpan:Z

    if-eqz v0, :cond_c

    invoke-direct {v6, v12}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->getMinStart(I)I

    move-result v0

    goto :goto_9

    .line 1623
    :cond_c
    invoke-virtual {v3, v12}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;->getStartLine(I)I

    move-result v0

    :goto_9
    move v2, v0

    .line 1624
    .restart local v2    # "end":I
    iget-object v0, v6, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mPrimaryOrientation:Landroidx/recyclerview/widget/OrientationHelper;

    invoke-virtual {v0, v14}, Landroidx/recyclerview/widget/OrientationHelper;->getDecoratedMeasurement(Landroid/view/View;)I

    move-result v0

    sub-int v0, v2, v0

    .line 1625
    .restart local v0    # "start":I
    if-eqz v16, :cond_d

    iget-boolean v1, v15, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LayoutParams;->mFullSpan:Z

    if-eqz v1, :cond_d

    .line 1627
    invoke-direct {v6, v2}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->createFullSpanItemFromStart(I)Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup$FullSpanItem;

    move-result-object v1

    .line 1628
    .local v1, "fullSpanItem":Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup$FullSpanItem;
    iput v10, v1, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup$FullSpanItem;->mGapDir:I

    .line 1629
    iput v5, v1, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup$FullSpanItem;->mPosition:I

    .line 1630
    iget-object v9, v6, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mLazySpanLookup:Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup;

    invoke-virtual {v9, v1}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup;->addFullSpanItem(Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup$FullSpanItem;)V

    .line 1635
    .end local v1    # "fullSpanItem":Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup$FullSpanItem;
    :cond_d
    :goto_a
    move v9, v0

    move/from16 v18, v2

    .end local v0    # "start":I
    .end local v2    # "end":I
    .local v9, "start":I
    .local v18, "end":I
    iget-boolean v0, v15, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LayoutParams;->mFullSpan:Z

    if-eqz v0, :cond_11

    iget v0, v8, Landroidx/recyclerview/widget/LayoutState;->mItemDirection:I

    const/4 v1, -0x1

    if-ne v0, v1, :cond_11

    .line 1636
    if-eqz v16, :cond_e

    .line 1637
    iput-boolean v10, v6, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mLaidOutInvalidFullSpan:Z

    goto :goto_c

    .line 1640
    :cond_e
    iget v0, v8, Landroidx/recyclerview/widget/LayoutState;->mLayoutDirection:I

    if-ne v0, v10, :cond_f

    .line 1641
    invoke-virtual/range {p0 .. p0}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->areAllEndsEqual()Z

    move-result v0

    xor-int/2addr v0, v10

    .local v0, "hasInvalidGap":Z
    goto :goto_b

    .line 1643
    .end local v0    # "hasInvalidGap":Z
    :cond_f
    invoke-virtual/range {p0 .. p0}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->areAllStartsEqual()Z

    move-result v0

    xor-int/2addr v0, v10

    .line 1645
    .restart local v0    # "hasInvalidGap":Z
    :goto_b
    if-eqz v0, :cond_11

    .line 1646
    iget-object v1, v6, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mLazySpanLookup:Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup;

    .line 1647
    invoke-virtual {v1, v5}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup;->getFullSpanItem(I)Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup$FullSpanItem;

    move-result-object v1

    .line 1648
    .restart local v1    # "fullSpanItem":Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup$FullSpanItem;
    if-eqz v1, :cond_10

    .line 1649
    iput-boolean v10, v1, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup$FullSpanItem;->mHasUnwantedGapAfter:Z

    .line 1651
    :cond_10
    iput-boolean v10, v6, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mLaidOutInvalidFullSpan:Z

    .line 1655
    .end local v0    # "hasInvalidGap":Z
    .end local v1    # "fullSpanItem":Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup$FullSpanItem;
    :cond_11
    :goto_c
    invoke-direct {v6, v14, v15, v8}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->attachViewToSpans(Landroid/view/View;Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LayoutParams;Landroidx/recyclerview/widget/LayoutState;)V

    .line 1658
    invoke-virtual/range {p0 .. p0}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->isLayoutRTL()Z

    move-result v0

    if-eqz v0, :cond_13

    iget v0, v6, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mOrientation:I

    if-ne v0, v10, :cond_13

    .line 1659
    iget-boolean v0, v15, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LayoutParams;->mFullSpan:Z

    if-eqz v0, :cond_12

    iget-object v0, v6, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mSecondaryOrientation:Landroidx/recyclerview/widget/OrientationHelper;

    invoke-virtual {v0}, Landroidx/recyclerview/widget/OrientationHelper;->getEndAfterPadding()I

    move-result v0

    goto :goto_d

    .line 1661
    :cond_12
    iget-object v0, v6, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mSecondaryOrientation:Landroidx/recyclerview/widget/OrientationHelper;

    .line 1660
    invoke-virtual {v0}, Landroidx/recyclerview/widget/OrientationHelper;->getEndAfterPadding()I

    move-result v0

    iget v1, v6, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mSpanCount:I

    sub-int/2addr v1, v10

    iget v2, v3, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;->mIndex:I

    sub-int/2addr v1, v2

    iget v2, v6, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mSizePerSpan:I

    mul-int v1, v1, v2

    sub-int/2addr v0, v1

    :goto_d
    nop

    .line 1662
    .local v0, "otherEnd":I
    iget-object v1, v6, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mSecondaryOrientation:Landroidx/recyclerview/widget/OrientationHelper;

    invoke-virtual {v1, v14}, Landroidx/recyclerview/widget/OrientationHelper;->getDecoratedMeasurement(Landroid/view/View;)I

    move-result v1

    sub-int v1, v0, v1

    move/from16 v17, v0

    move/from16 v19, v1

    .local v1, "otherStart":I
    goto :goto_f

    .line 1664
    .end local v0    # "otherEnd":I
    .end local v1    # "otherStart":I
    :cond_13
    iget-boolean v0, v15, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LayoutParams;->mFullSpan:Z

    if-eqz v0, :cond_14

    iget-object v0, v6, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mSecondaryOrientation:Landroidx/recyclerview/widget/OrientationHelper;

    invoke-virtual {v0}, Landroidx/recyclerview/widget/OrientationHelper;->getStartAfterPadding()I

    move-result v0

    goto :goto_e

    .line 1666
    :cond_14
    iget v0, v3, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;->mIndex:I

    iget v1, v6, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mSizePerSpan:I

    mul-int v0, v0, v1

    iget-object v1, v6, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mSecondaryOrientation:Landroidx/recyclerview/widget/OrientationHelper;

    invoke-virtual {v1}, Landroidx/recyclerview/widget/OrientationHelper;->getStartAfterPadding()I

    move-result v1

    add-int/2addr v0, v1

    :goto_e
    move v1, v0

    .line 1667
    .restart local v1    # "otherStart":I
    iget-object v0, v6, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mSecondaryOrientation:Landroidx/recyclerview/widget/OrientationHelper;

    invoke-virtual {v0, v14}, Landroidx/recyclerview/widget/OrientationHelper;->getDecoratedMeasurement(Landroid/view/View;)I

    move-result v0

    add-int/2addr v0, v1

    move/from16 v17, v0

    move/from16 v19, v1

    .line 1670
    .end local v1    # "otherStart":I
    .local v17, "otherEnd":I
    .local v19, "otherStart":I
    :goto_f
    iget v0, v6, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mOrientation:I

    if-ne v0, v10, :cond_15

    .line 1671
    move-object/from16 v0, p0

    move-object v1, v14

    move/from16 v2, v19

    move-object v10, v3

    .end local v3    # "currentSpan":Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;
    .local v10, "currentSpan":Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;
    move v3, v9

    move/from16 v20, v4

    .end local v4    # "spanIndex":I
    .local v20, "spanIndex":I
    move/from16 v4, v17

    move/from16 v21, v5

    .end local v5    # "position":I
    .local v21, "position":I
    move/from16 v5, v18

    invoke-virtual/range {v0 .. v5}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->layoutDecoratedWithMargins(Landroid/view/View;IIII)V

    goto :goto_10

    .line 1673
    .end local v10    # "currentSpan":Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;
    .end local v20    # "spanIndex":I
    .end local v21    # "position":I
    .restart local v3    # "currentSpan":Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;
    .restart local v4    # "spanIndex":I
    .restart local v5    # "position":I
    :cond_15
    move-object v10, v3

    move/from16 v20, v4

    move/from16 v21, v5

    .end local v3    # "currentSpan":Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;
    .end local v4    # "spanIndex":I
    .end local v5    # "position":I
    .restart local v10    # "currentSpan":Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;
    .restart local v20    # "spanIndex":I
    .restart local v21    # "position":I
    move-object/from16 v0, p0

    move-object v1, v14

    move v2, v9

    move/from16 v3, v19

    move/from16 v4, v18

    move/from16 v5, v17

    invoke-virtual/range {v0 .. v5}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->layoutDecoratedWithMargins(Landroid/view/View;IIII)V

    .line 1676
    :goto_10
    iget-boolean v0, v15, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LayoutParams;->mFullSpan:Z

    if-eqz v0, :cond_16

    .line 1677
    iget-object v0, v6, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mLayoutState:Landroidx/recyclerview/widget/LayoutState;

    iget v0, v0, Landroidx/recyclerview/widget/LayoutState;->mLayoutDirection:I

    invoke-direct {v6, v0, v11}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->updateAllRemainingSpans(II)V

    goto :goto_11

    .line 1679
    :cond_16
    iget-object v0, v6, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mLayoutState:Landroidx/recyclerview/widget/LayoutState;

    iget v0, v0, Landroidx/recyclerview/widget/LayoutState;->mLayoutDirection:I

    invoke-direct {v6, v10, v0, v11}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->updateRemainingSpans(Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;II)V

    .line 1681
    :goto_11
    iget-object v0, v6, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mLayoutState:Landroidx/recyclerview/widget/LayoutState;

    invoke-direct {v6, v7, v0}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->recycle(Landroidx/recyclerview/widget/RecyclerView$Recycler;Landroidx/recyclerview/widget/LayoutState;)V

    .line 1682
    iget-object v0, v6, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mLayoutState:Landroidx/recyclerview/widget/LayoutState;

    iget-boolean v0, v0, Landroidx/recyclerview/widget/LayoutState;->mStopInFocusable:Z

    if-eqz v0, :cond_18

    invoke-virtual {v14}, Landroid/view/View;->hasFocusable()Z

    move-result v0

    if-eqz v0, :cond_18

    .line 1683
    iget-boolean v0, v15, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LayoutParams;->mFullSpan:Z

    if-eqz v0, :cond_17

    .line 1684
    iget-object v0, v6, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mRemainingSpans:Ljava/util/BitSet;

    invoke-virtual {v0}, Ljava/util/BitSet;->clear()V

    const/4 v2, 0x0

    goto :goto_12

    .line 1686
    :cond_17
    iget-object v0, v6, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mRemainingSpans:Ljava/util/BitSet;

    iget v1, v10, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;->mIndex:I

    const/4 v2, 0x0

    invoke-virtual {v0, v1, v2}, Ljava/util/BitSet;->set(IZ)V

    goto :goto_12

    .line 1682
    :cond_18
    const/4 v2, 0x0

    .line 1689
    :goto_12
    const/4 v13, 0x1

    .line 1690
    .end local v9    # "start":I
    .end local v10    # "currentSpan":Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;
    .end local v14    # "view":Landroid/view/View;
    .end local v15    # "lp":Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LayoutParams;
    .end local v16    # "assignSpan":Z
    .end local v17    # "otherEnd":I
    .end local v18    # "end":I
    .end local v19    # "otherStart":I
    .end local v20    # "spanIndex":I
    .end local v21    # "position":I
    const/4 v9, 0x0

    const/4 v10, 0x1

    goto/16 :goto_2

    .line 1579
    :cond_19
    const/4 v2, 0x0

    .line 1691
    :goto_13
    if-nez v13, :cond_1a

    .line 1692
    iget-object v0, v6, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mLayoutState:Landroidx/recyclerview/widget/LayoutState;

    invoke-direct {v6, v7, v0}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->recycle(Landroidx/recyclerview/widget/RecyclerView$Recycler;Landroidx/recyclerview/widget/LayoutState;)V

    .line 1695
    :cond_1a
    iget-object v0, v6, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mLayoutState:Landroidx/recyclerview/widget/LayoutState;

    iget v0, v0, Landroidx/recyclerview/widget/LayoutState;->mLayoutDirection:I

    const/4 v1, -0x1

    if-ne v0, v1, :cond_1b

    .line 1696
    iget-object v0, v6, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mPrimaryOrientation:Landroidx/recyclerview/widget/OrientationHelper;

    invoke-virtual {v0}, Landroidx/recyclerview/widget/OrientationHelper;->getStartAfterPadding()I

    move-result v0

    invoke-direct {v6, v0}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->getMinStart(I)I

    move-result v0

    .line 1697
    .local v0, "minStart":I
    iget-object v1, v6, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mPrimaryOrientation:Landroidx/recyclerview/widget/OrientationHelper;

    invoke-virtual {v1}, Landroidx/recyclerview/widget/OrientationHelper;->getStartAfterPadding()I

    move-result v1

    sub-int/2addr v1, v0

    .line 1698
    .end local v0    # "minStart":I
    .local v1, "diff":I
    goto :goto_14

    .line 1699
    .end local v1    # "diff":I
    :cond_1b
    iget-object v0, v6, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mPrimaryOrientation:Landroidx/recyclerview/widget/OrientationHelper;

    invoke-virtual {v0}, Landroidx/recyclerview/widget/OrientationHelper;->getEndAfterPadding()I

    move-result v0

    invoke-direct {v6, v0}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->getMaxEnd(I)I

    move-result v0

    .line 1700
    .local v0, "maxEnd":I
    iget-object v1, v6, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mPrimaryOrientation:Landroidx/recyclerview/widget/OrientationHelper;

    invoke-virtual {v1}, Landroidx/recyclerview/widget/OrientationHelper;->getEndAfterPadding()I

    move-result v1

    sub-int v1, v0, v1

    .line 1702
    .end local v0    # "maxEnd":I
    .restart local v1    # "diff":I
    :goto_14
    if-lez v1, :cond_1c

    iget v0, v8, Landroidx/recyclerview/widget/LayoutState;->mAvailable:I

    invoke-static {v0, v1}, Ljava/lang/Math;->min(II)I

    move-result v9

    goto :goto_15

    :cond_1c
    const/4 v9, 0x0

    :goto_15
    return v9
.end method

.method private findFirstReferenceChildPosition(I)I
    .locals 4
    .param p1, "itemCount"    # I

    .line 2192
    invoke-virtual {p0}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->getChildCount()I

    move-result v0

    .line 2193
    .local v0, "limit":I
    const/4 v1, 0x0

    .local v1, "i":I
    :goto_0
    if-ge v1, v0, :cond_1

    .line 2194
    invoke-virtual {p0, v1}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->getChildAt(I)Landroid/view/View;

    move-result-object v2

    .line 2195
    .local v2, "view":Landroid/view/View;
    invoke-virtual {p0, v2}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->getPosition(Landroid/view/View;)I

    move-result v3

    .line 2196
    .local v3, "position":I
    if-ltz v3, :cond_0

    if-ge v3, p1, :cond_0

    .line 2197
    return v3

    .line 2193
    .end local v2    # "view":Landroid/view/View;
    .end local v3    # "position":I
    :cond_0
    add-int/lit8 v1, v1, 0x1

    goto :goto_0

    .line 2200
    .end local v1    # "i":I
    :cond_1
    const/4 v1, 0x0

    return v1
.end method

.method private findLastReferenceChildPosition(I)I
    .locals 3
    .param p1, "itemCount"    # I

    .line 2209
    invoke-virtual {p0}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->getChildCount()I

    move-result v0

    add-int/lit8 v0, v0, -0x1

    .local v0, "i":I
    :goto_0
    if-ltz v0, :cond_1

    .line 2210
    invoke-virtual {p0, v0}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->getChildAt(I)Landroid/view/View;

    move-result-object v1

    .line 2211
    .local v1, "view":Landroid/view/View;
    invoke-virtual {p0, v1}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->getPosition(Landroid/view/View;)I

    move-result v2

    .line 2212
    .local v2, "position":I
    if-ltz v2, :cond_0

    if-ge v2, p1, :cond_0

    .line 2213
    return v2

    .line 2209
    .end local v1    # "view":Landroid/view/View;
    .end local v2    # "position":I
    :cond_0
    add-int/lit8 v0, v0, -0x1

    goto :goto_0

    .line 2216
    .end local v0    # "i":I
    :cond_1
    const/4 v0, 0x0

    return v0
.end method

.method private fixEndGap(Landroidx/recyclerview/widget/RecyclerView$Recycler;Landroidx/recyclerview/widget/RecyclerView$State;Z)V
    .locals 4
    .param p1, "recycler"    # Landroidx/recyclerview/widget/RecyclerView$Recycler;
    .param p2, "state"    # Landroidx/recyclerview/widget/RecyclerView$State;
    .param p3, "canOffsetChildren"    # Z

    .line 1385
    const/high16 v0, -0x80000000

    invoke-direct {p0, v0}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->getMaxEnd(I)I

    move-result v1

    .line 1386
    .local v1, "maxEndLine":I
    if-ne v1, v0, :cond_0

    .line 1387
    return-void

    .line 1389
    :cond_0
    iget-object v0, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mPrimaryOrientation:Landroidx/recyclerview/widget/OrientationHelper;

    invoke-virtual {v0}, Landroidx/recyclerview/widget/OrientationHelper;->getEndAfterPadding()I

    move-result v0

    sub-int/2addr v0, v1

    .line 1391
    .local v0, "gap":I
    if-lez v0, :cond_2

    .line 1392
    neg-int v2, v0

    invoke-virtual {p0, v2, p1, p2}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->scrollBy(ILandroidx/recyclerview/widget/RecyclerView$Recycler;Landroidx/recyclerview/widget/RecyclerView$State;)I

    move-result v2

    neg-int v2, v2

    .line 1396
    .local v2, "fixOffset":I
    sub-int/2addr v0, v2

    .line 1397
    if-eqz p3, :cond_1

    if-lez v0, :cond_1

    .line 1398
    iget-object v3, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mPrimaryOrientation:Landroidx/recyclerview/widget/OrientationHelper;

    invoke-virtual {v3, v0}, Landroidx/recyclerview/widget/OrientationHelper;->offsetChildren(I)V

    .line 1400
    :cond_1
    return-void

    .line 1394
    .end local v2    # "fixOffset":I
    :cond_2
    return-void
.end method

.method private fixStartGap(Landroidx/recyclerview/widget/RecyclerView$Recycler;Landroidx/recyclerview/widget/RecyclerView$State;Z)V
    .locals 5
    .param p1, "recycler"    # Landroidx/recyclerview/widget/RecyclerView$Recycler;
    .param p2, "state"    # Landroidx/recyclerview/widget/RecyclerView$State;
    .param p3, "canOffsetChildren"    # Z

    .line 1404
    const v0, 0x7fffffff

    invoke-direct {p0, v0}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->getMinStart(I)I

    move-result v1

    .line 1405
    .local v1, "minStartLine":I
    if-ne v1, v0, :cond_0

    .line 1406
    return-void

    .line 1408
    :cond_0
    iget-object v0, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mPrimaryOrientation:Landroidx/recyclerview/widget/OrientationHelper;

    invoke-virtual {v0}, Landroidx/recyclerview/widget/OrientationHelper;->getStartAfterPadding()I

    move-result v0

    sub-int v0, v1, v0

    .line 1410
    .local v0, "gap":I
    if-lez v0, :cond_2

    .line 1411
    invoke-virtual {p0, v0, p1, p2}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->scrollBy(ILandroidx/recyclerview/widget/RecyclerView$Recycler;Landroidx/recyclerview/widget/RecyclerView$State;)I

    move-result v2

    .line 1415
    .local v2, "fixOffset":I
    sub-int/2addr v0, v2

    .line 1416
    if-eqz p3, :cond_1

    if-lez v0, :cond_1

    .line 1417
    iget-object v3, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mPrimaryOrientation:Landroidx/recyclerview/widget/OrientationHelper;

    neg-int v4, v0

    invoke-virtual {v3, v4}, Landroidx/recyclerview/widget/OrientationHelper;->offsetChildren(I)V

    .line 1419
    :cond_1
    return-void

    .line 1413
    .end local v2    # "fixOffset":I
    :cond_2
    return-void
.end method

.method private getMaxEnd(I)I
    .locals 3
    .param p1, "def"    # I

    .line 1859
    iget-object v0, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mSpans:[Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;

    const/4 v1, 0x0

    aget-object v0, v0, v1

    invoke-virtual {v0, p1}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;->getEndLine(I)I

    move-result v0

    .line 1860
    .local v0, "maxEnd":I
    const/4 v1, 0x1

    .local v1, "i":I
    :goto_0
    iget v2, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mSpanCount:I

    if-ge v1, v2, :cond_1

    .line 1861
    iget-object v2, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mSpans:[Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;

    aget-object v2, v2, v1

    invoke-virtual {v2, p1}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;->getEndLine(I)I

    move-result v2

    .line 1862
    .local v2, "spanEnd":I
    if-le v2, v0, :cond_0

    .line 1863
    move v0, v2

    .line 1860
    .end local v2    # "spanEnd":I
    :cond_0
    add-int/lit8 v1, v1, 0x1

    goto :goto_0

    .line 1866
    .end local v1    # "i":I
    :cond_1
    return v0
.end method

.method private getMaxStart(I)I
    .locals 3
    .param p1, "def"    # I

    .line 1817
    iget-object v0, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mSpans:[Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;

    const/4 v1, 0x0

    aget-object v0, v0, v1

    invoke-virtual {v0, p1}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;->getStartLine(I)I

    move-result v0

    .line 1818
    .local v0, "maxStart":I
    const/4 v1, 0x1

    .local v1, "i":I
    :goto_0
    iget v2, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mSpanCount:I

    if-ge v1, v2, :cond_1

    .line 1819
    iget-object v2, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mSpans:[Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;

    aget-object v2, v2, v1

    invoke-virtual {v2, p1}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;->getStartLine(I)I

    move-result v2

    .line 1820
    .local v2, "spanStart":I
    if-le v2, v0, :cond_0

    .line 1821
    move v0, v2

    .line 1818
    .end local v2    # "spanStart":I
    :cond_0
    add-int/lit8 v1, v1, 0x1

    goto :goto_0

    .line 1824
    .end local v1    # "i":I
    :cond_1
    return v0
.end method

.method private getMinEnd(I)I
    .locals 3
    .param p1, "def"    # I

    .line 1870
    iget-object v0, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mSpans:[Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;

    const/4 v1, 0x0

    aget-object v0, v0, v1

    invoke-virtual {v0, p1}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;->getEndLine(I)I

    move-result v0

    .line 1871
    .local v0, "minEnd":I
    const/4 v1, 0x1

    .local v1, "i":I
    :goto_0
    iget v2, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mSpanCount:I

    if-ge v1, v2, :cond_1

    .line 1872
    iget-object v2, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mSpans:[Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;

    aget-object v2, v2, v1

    invoke-virtual {v2, p1}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;->getEndLine(I)I

    move-result v2

    .line 1873
    .local v2, "spanEnd":I
    if-ge v2, v0, :cond_0

    .line 1874
    move v0, v2

    .line 1871
    .end local v2    # "spanEnd":I
    :cond_0
    add-int/lit8 v1, v1, 0x1

    goto :goto_0

    .line 1877
    .end local v1    # "i":I
    :cond_1
    return v0
.end method

.method private getMinStart(I)I
    .locals 3
    .param p1, "def"    # I

    .line 1828
    iget-object v0, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mSpans:[Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;

    const/4 v1, 0x0

    aget-object v0, v0, v1

    invoke-virtual {v0, p1}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;->getStartLine(I)I

    move-result v0

    .line 1829
    .local v0, "minStart":I
    const/4 v1, 0x1

    .local v1, "i":I
    :goto_0
    iget v2, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mSpanCount:I

    if-ge v1, v2, :cond_1

    .line 1830
    iget-object v2, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mSpans:[Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;

    aget-object v2, v2, v1

    invoke-virtual {v2, p1}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;->getStartLine(I)I

    move-result v2

    .line 1831
    .local v2, "spanStart":I
    if-ge v2, v0, :cond_0

    .line 1832
    move v0, v2

    .line 1829
    .end local v2    # "spanStart":I
    :cond_0
    add-int/lit8 v1, v1, 0x1

    goto :goto_0

    .line 1835
    .end local v1    # "i":I
    :cond_1
    return v0
.end method

.method private getNextSpan(Landroidx/recyclerview/widget/LayoutState;)Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;
    .locals 10
    .param p1, "layoutState"    # Landroidx/recyclerview/widget/LayoutState;

    .line 1954
    iget v0, p1, Landroidx/recyclerview/widget/LayoutState;->mLayoutDirection:I

    invoke-direct {p0, v0}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->preferLastSpan(I)Z

    move-result v0

    .line 1956
    .local v0, "preferLastSpan":Z
    const/4 v1, 0x1

    if-eqz v0, :cond_0

    .line 1957
    iget v2, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mSpanCount:I

    sub-int/2addr v2, v1

    .line 1958
    .local v2, "startIndex":I
    const/4 v3, -0x1

    .line 1959
    .local v3, "endIndex":I
    const/4 v4, -0x1

    .local v4, "diff":I
    goto :goto_0

    .line 1961
    .end local v2    # "startIndex":I
    .end local v3    # "endIndex":I
    .end local v4    # "diff":I
    :cond_0
    const/4 v2, 0x0

    .line 1962
    .restart local v2    # "startIndex":I
    iget v3, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mSpanCount:I

    .line 1963
    .restart local v3    # "endIndex":I
    const/4 v4, 0x1

    .line 1965
    .restart local v4    # "diff":I
    :goto_0
    iget v5, p1, Landroidx/recyclerview/widget/LayoutState;->mLayoutDirection:I

    if-ne v5, v1, :cond_3

    .line 1966
    const/4 v1, 0x0

    .line 1967
    .local v1, "min":Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;
    const v5, 0x7fffffff

    .line 1968
    .local v5, "minLine":I
    iget-object v6, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mPrimaryOrientation:Landroidx/recyclerview/widget/OrientationHelper;

    invoke-virtual {v6}, Landroidx/recyclerview/widget/OrientationHelper;->getStartAfterPadding()I

    move-result v6

    .line 1969
    .local v6, "defaultLine":I
    move v7, v2

    .local v7, "i":I
    :goto_1
    if-eq v7, v3, :cond_2

    .line 1970
    iget-object v8, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mSpans:[Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;

    aget-object v8, v8, v7

    .line 1971
    .local v8, "other":Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;
    invoke-virtual {v8, v6}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;->getEndLine(I)I

    move-result v9

    .line 1972
    .local v9, "otherLine":I
    if-ge v9, v5, :cond_1

    .line 1973
    move-object v1, v8

    .line 1974
    move v5, v9

    .line 1969
    .end local v8    # "other":Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;
    .end local v9    # "otherLine":I
    :cond_1
    add-int/2addr v7, v4

    goto :goto_1

    .line 1977
    .end local v7    # "i":I
    :cond_2
    return-object v1

    .line 1979
    .end local v1    # "min":Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;
    .end local v5    # "minLine":I
    .end local v6    # "defaultLine":I
    :cond_3
    const/4 v1, 0x0

    .line 1980
    .local v1, "max":Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;
    const/high16 v5, -0x80000000

    .line 1981
    .local v5, "maxLine":I
    iget-object v6, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mPrimaryOrientation:Landroidx/recyclerview/widget/OrientationHelper;

    invoke-virtual {v6}, Landroidx/recyclerview/widget/OrientationHelper;->getEndAfterPadding()I

    move-result v6

    .line 1982
    .restart local v6    # "defaultLine":I
    move v7, v2

    .restart local v7    # "i":I
    :goto_2
    if-eq v7, v3, :cond_5

    .line 1983
    iget-object v8, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mSpans:[Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;

    aget-object v8, v8, v7

    .line 1984
    .restart local v8    # "other":Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;
    invoke-virtual {v8, v6}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;->getStartLine(I)I

    move-result v9

    .line 1985
    .restart local v9    # "otherLine":I
    if-le v9, v5, :cond_4

    .line 1986
    move-object v1, v8

    .line 1987
    move v5, v9

    .line 1982
    .end local v8    # "other":Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;
    .end local v9    # "otherLine":I
    :cond_4
    add-int/2addr v7, v4

    goto :goto_2

    .line 1990
    .end local v7    # "i":I
    :cond_5
    return-object v1
.end method

.method private handleUpdate(III)V
    .locals 5
    .param p1, "positionStart"    # I
    .param p2, "itemCountOrToPosition"    # I
    .param p3, "cmd"    # I

    .line 1505
    iget-boolean v0, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mShouldReverseLayout:Z

    if-eqz v0, :cond_0

    invoke-virtual {p0}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->getLastChildPosition()I

    move-result v0

    goto :goto_0

    :cond_0
    invoke-virtual {p0}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->getFirstChildPosition()I

    move-result v0

    .line 1509
    .local v0, "minPosition":I
    :goto_0
    const/16 v1, 0x8

    if-ne p3, v1, :cond_2

    .line 1510
    if-ge p1, p2, :cond_1

    .line 1511
    add-int/lit8 v1, p2, 0x1

    .line 1512
    .local v1, "affectedRangeEnd":I
    move v2, p1

    .local v2, "affectedRangeStart":I
    goto :goto_1

    .line 1514
    .end local v1    # "affectedRangeEnd":I
    .end local v2    # "affectedRangeStart":I
    :cond_1
    add-int/lit8 v1, p1, 0x1

    .line 1515
    .restart local v1    # "affectedRangeEnd":I
    move v2, p2

    .restart local v2    # "affectedRangeStart":I
    goto :goto_1

    .line 1518
    .end local v1    # "affectedRangeEnd":I
    .end local v2    # "affectedRangeStart":I
    :cond_2
    move v2, p1

    .line 1519
    .restart local v2    # "affectedRangeStart":I
    add-int v1, p1, p2

    .line 1522
    .restart local v1    # "affectedRangeEnd":I
    :goto_1
    iget-object v3, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mLazySpanLookup:Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup;

    invoke-virtual {v3, v2}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup;->invalidateAfter(I)I

    .line 1523
    sparse-switch p3, :sswitch_data_0

    goto :goto_2

    .line 1532
    :sswitch_0
    iget-object v3, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mLazySpanLookup:Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup;

    const/4 v4, 0x1

    invoke-virtual {v3, p1, v4}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup;->offsetForRemoval(II)V

    .line 1533
    iget-object v3, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mLazySpanLookup:Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup;

    invoke-virtual {v3, p2, v4}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup;->offsetForAddition(II)V

    goto :goto_2

    .line 1528
    :sswitch_1
    iget-object v3, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mLazySpanLookup:Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup;

    invoke-virtual {v3, p1, p2}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup;->offsetForRemoval(II)V

    .line 1529
    goto :goto_2

    .line 1525
    :sswitch_2
    iget-object v3, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mLazySpanLookup:Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup;

    invoke-virtual {v3, p1, p2}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup;->offsetForAddition(II)V

    .line 1526
    nop

    .line 1537
    :goto_2
    if-gt v1, v0, :cond_3

    .line 1538
    return-void

    .line 1541
    :cond_3
    iget-boolean v3, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mShouldReverseLayout:Z

    if-eqz v3, :cond_4

    invoke-virtual {p0}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->getFirstChildPosition()I

    move-result v3

    goto :goto_3

    :cond_4
    invoke-virtual {p0}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->getLastChildPosition()I

    move-result v3

    .line 1542
    .local v3, "maxPosition":I
    :goto_3
    if-gt v2, v3, :cond_5

    .line 1543
    invoke-virtual {p0}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->requestLayout()V

    .line 1545
    :cond_5
    return-void

    nop

    :sswitch_data_0
    .sparse-switch
        0x1 -> :sswitch_2
        0x2 -> :sswitch_1
        0x8 -> :sswitch_0
    .end sparse-switch
.end method

.method private measureChildWithDecorationsAndMargin(Landroid/view/View;IIZ)V
    .locals 4
    .param p1, "child"    # Landroid/view/View;
    .param p2, "widthSpec"    # I
    .param p3, "heightSpec"    # I
    .param p4, "alreadyMeasured"    # Z

    .line 1201
    iget-object v0, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mTmpRect:Landroid/graphics/Rect;

    invoke-virtual {p0, p1, v0}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->calculateItemDecorationsForChild(Landroid/view/View;Landroid/graphics/Rect;)V

    .line 1202
    invoke-virtual {p1}, Landroid/view/View;->getLayoutParams()Landroid/view/ViewGroup$LayoutParams;

    move-result-object v0

    check-cast v0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LayoutParams;

    .line 1203
    .local v0, "lp":Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LayoutParams;
    iget v1, v0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LayoutParams;->leftMargin:I

    iget-object v2, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mTmpRect:Landroid/graphics/Rect;

    iget v2, v2, Landroid/graphics/Rect;->left:I

    add-int/2addr v1, v2

    iget v2, v0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LayoutParams;->rightMargin:I

    iget-object v3, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mTmpRect:Landroid/graphics/Rect;

    iget v3, v3, Landroid/graphics/Rect;->right:I

    add-int/2addr v2, v3

    invoke-direct {p0, p2, v1, v2}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->updateSpecWithExtra(III)I

    move-result p2

    .line 1205
    iget v1, v0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LayoutParams;->topMargin:I

    iget-object v2, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mTmpRect:Landroid/graphics/Rect;

    iget v2, v2, Landroid/graphics/Rect;->top:I

    add-int/2addr v1, v2

    iget v2, v0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LayoutParams;->bottomMargin:I

    iget-object v3, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mTmpRect:Landroid/graphics/Rect;

    iget v3, v3, Landroid/graphics/Rect;->bottom:I

    add-int/2addr v2, v3

    invoke-direct {p0, p3, v1, v2}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->updateSpecWithExtra(III)I

    move-result p3

    .line 1207
    if-eqz p4, :cond_0

    .line 1208
    invoke-virtual {p0, p1, p2, p3, v0}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->shouldReMeasureChild(Landroid/view/View;IILandroidx/recyclerview/widget/RecyclerView$LayoutParams;)Z

    move-result v1

    goto :goto_0

    .line 1209
    :cond_0
    invoke-virtual {p0, p1, p2, p3, v0}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->shouldMeasureChild(Landroid/view/View;IILandroidx/recyclerview/widget/RecyclerView$LayoutParams;)Z

    move-result v1

    :goto_0
    nop

    .line 1210
    .local v1, "measure":Z
    if-eqz v1, :cond_1

    .line 1211
    invoke-virtual {p1, p2, p3}, Landroid/view/View;->measure(II)V

    .line 1214
    :cond_1
    return-void
.end method

.method private measureChildWithDecorationsAndMargin(Landroid/view/View;Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LayoutParams;Z)V
    .locals 6
    .param p1, "child"    # Landroid/view/View;
    .param p2, "lp"    # Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LayoutParams;
    .param p3, "alreadyMeasured"    # Z

    .line 1136
    iget-boolean v0, p2, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LayoutParams;->mFullSpan:Z

    const/4 v1, 0x1

    if-eqz v0, :cond_1

    .line 1137
    iget v0, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mOrientation:I

    if-ne v0, v1, :cond_0

    .line 1138
    iget v0, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mFullSizeSpec:I

    .line 1140
    invoke-virtual {p0}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->getHeight()I

    move-result v2

    .line 1141
    invoke-virtual {p0}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->getHeightMode()I

    move-result v3

    .line 1142
    invoke-virtual {p0}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->getPaddingTop()I

    move-result v4

    invoke-virtual {p0}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->getPaddingBottom()I

    move-result v5

    add-int/2addr v4, v5

    iget v5, p2, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LayoutParams;->height:I

    .line 1139
    invoke-static {v2, v3, v4, v5, v1}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->getChildMeasureSpec(IIIIZ)I

    move-result v1

    .line 1138
    invoke-direct {p0, p1, v0, v1, p3}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->measureChildWithDecorationsAndMargin(Landroid/view/View;IIZ)V

    goto :goto_0

    .line 1147
    :cond_0
    nop

    .line 1150
    invoke-virtual {p0}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->getWidth()I

    move-result v0

    .line 1151
    invoke-virtual {p0}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->getWidthMode()I

    move-result v2

    .line 1152
    invoke-virtual {p0}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->getPaddingLeft()I

    move-result v3

    invoke-virtual {p0}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->getPaddingRight()I

    move-result v4

    add-int/2addr v3, v4

    iget v4, p2, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LayoutParams;->width:I

    .line 1149
    invoke-static {v0, v2, v3, v4, v1}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->getChildMeasureSpec(IIIIZ)I

    move-result v0

    iget v1, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mFullSizeSpec:I

    .line 1147
    invoke-direct {p0, p1, v0, v1, p3}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->measureChildWithDecorationsAndMargin(Landroid/view/View;IIZ)V

    goto :goto_0

    .line 1159
    :cond_1
    iget v0, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mOrientation:I

    const/4 v2, 0x0

    if-ne v0, v1, :cond_2

    .line 1162
    iget v0, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mSizePerSpan:I

    .line 1166
    invoke-virtual {p0}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->getWidthMode()I

    move-result v3

    iget v4, p2, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LayoutParams;->width:I

    .line 1164
    invoke-static {v0, v3, v2, v4, v2}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->getChildMeasureSpec(IIIIZ)I

    move-result v0

    .line 1171
    invoke-virtual {p0}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->getHeight()I

    move-result v2

    .line 1172
    invoke-virtual {p0}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->getHeightMode()I

    move-result v3

    .line 1173
    invoke-virtual {p0}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->getPaddingTop()I

    move-result v4

    invoke-virtual {p0}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->getPaddingBottom()I

    move-result v5

    add-int/2addr v4, v5

    iget v5, p2, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LayoutParams;->height:I

    .line 1170
    invoke-static {v2, v3, v4, v5, v1}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->getChildMeasureSpec(IIIIZ)I

    move-result v1

    .line 1162
    invoke-direct {p0, p1, v0, v1, p3}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->measureChildWithDecorationsAndMargin(Landroid/view/View;IIZ)V

    goto :goto_0

    .line 1180
    :cond_2
    nop

    .line 1183
    invoke-virtual {p0}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->getWidth()I

    move-result v0

    .line 1184
    invoke-virtual {p0}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->getWidthMode()I

    move-result v3

    .line 1185
    invoke-virtual {p0}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->getPaddingLeft()I

    move-result v4

    invoke-virtual {p0}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->getPaddingRight()I

    move-result v5

    add-int/2addr v4, v5

    iget v5, p2, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LayoutParams;->width:I

    .line 1182
    invoke-static {v0, v3, v4, v5, v1}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->getChildMeasureSpec(IIIIZ)I

    move-result v0

    iget v1, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mSizePerSpan:I

    .line 1190
    invoke-virtual {p0}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->getHeightMode()I

    move-result v3

    iget v4, p2, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LayoutParams;->height:I

    .line 1188
    invoke-static {v1, v3, v2, v4, v2}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->getChildMeasureSpec(IIIIZ)I

    move-result v1

    .line 1180
    invoke-direct {p0, p1, v0, v1, p3}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->measureChildWithDecorationsAndMargin(Landroid/view/View;IIZ)V

    .line 1197
    :goto_0
    return-void
.end method

.method private onLayoutChildren(Landroidx/recyclerview/widget/RecyclerView$Recycler;Landroidx/recyclerview/widget/RecyclerView$State;Z)V
    .locals 9
    .param p1, "recycler"    # Landroidx/recyclerview/widget/RecyclerView$Recycler;
    .param p2, "state"    # Landroidx/recyclerview/widget/RecyclerView$State;
    .param p3, "shouldCheckForGaps"    # Z

    .line 619
    iget-object v0, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mAnchorInfo:Landroidx/recyclerview/widget/StaggeredGridLayoutManager$AnchorInfo;

    .line 620
    .local v0, "anchorInfo":Landroidx/recyclerview/widget/StaggeredGridLayoutManager$AnchorInfo;
    iget-object v1, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mPendingSavedState:Landroidx/recyclerview/widget/StaggeredGridLayoutManager$SavedState;

    const/4 v2, -0x1

    if-nez v1, :cond_0

    iget v1, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mPendingScrollPosition:I

    if-eq v1, v2, :cond_1

    .line 621
    :cond_0
    invoke-virtual {p2}, Landroidx/recyclerview/widget/RecyclerView$State;->getItemCount()I

    move-result v1

    if-nez v1, :cond_1

    .line 622
    invoke-virtual {p0, p1}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->removeAndRecycleAllViews(Landroidx/recyclerview/widget/RecyclerView$Recycler;)V

    .line 623
    invoke-virtual {v0}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$AnchorInfo;->reset()V

    .line 624
    return-void

    .line 628
    :cond_1
    iget-boolean v1, v0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$AnchorInfo;->mValid:Z

    const/4 v3, 0x0

    const/4 v4, 0x1

    if-eqz v1, :cond_3

    iget v1, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mPendingScrollPosition:I

    if-ne v1, v2, :cond_3

    iget-object v1, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mPendingSavedState:Landroidx/recyclerview/widget/StaggeredGridLayoutManager$SavedState;

    if-eqz v1, :cond_2

    goto :goto_0

    :cond_2
    const/4 v1, 0x0

    goto :goto_1

    :cond_3
    :goto_0
    const/4 v1, 0x1

    .line 630
    .local v1, "recalculateAnchor":Z
    :goto_1
    if-eqz v1, :cond_5

    .line 631
    invoke-virtual {v0}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$AnchorInfo;->reset()V

    .line 632
    iget-object v5, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mPendingSavedState:Landroidx/recyclerview/widget/StaggeredGridLayoutManager$SavedState;

    if-eqz v5, :cond_4

    .line 633
    invoke-direct {p0, v0}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->applyPendingSavedState(Landroidx/recyclerview/widget/StaggeredGridLayoutManager$AnchorInfo;)V

    goto :goto_2

    .line 635
    :cond_4
    invoke-direct {p0}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->resolveShouldLayoutReverse()V

    .line 636
    iget-boolean v5, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mShouldReverseLayout:Z

    iput-boolean v5, v0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$AnchorInfo;->mLayoutFromEnd:Z

    .line 638
    :goto_2
    invoke-virtual {p0, p2, v0}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->updateAnchorInfoForLayout(Landroidx/recyclerview/widget/RecyclerView$State;Landroidx/recyclerview/widget/StaggeredGridLayoutManager$AnchorInfo;)V

    .line 639
    iput-boolean v4, v0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$AnchorInfo;->mValid:Z

    .line 641
    :cond_5
    iget-object v5, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mPendingSavedState:Landroidx/recyclerview/widget/StaggeredGridLayoutManager$SavedState;

    if-nez v5, :cond_7

    iget v5, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mPendingScrollPosition:I

    if-ne v5, v2, :cond_7

    .line 642
    iget-boolean v5, v0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$AnchorInfo;->mLayoutFromEnd:Z

    iget-boolean v6, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mLastLayoutFromEnd:Z

    if-ne v5, v6, :cond_6

    .line 643
    invoke-virtual {p0}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->isLayoutRTL()Z

    move-result v5

    iget-boolean v6, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mLastLayoutRTL:Z

    if-eq v5, v6, :cond_7

    .line 644
    :cond_6
    iget-object v5, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mLazySpanLookup:Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup;

    invoke-virtual {v5}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup;->clear()V

    .line 645
    iput-boolean v4, v0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$AnchorInfo;->mInvalidateOffsets:Z

    .line 649
    :cond_7
    invoke-virtual {p0}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->getChildCount()I

    move-result v5

    if-lez v5, :cond_f

    iget-object v5, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mPendingSavedState:Landroidx/recyclerview/widget/StaggeredGridLayoutManager$SavedState;

    if-eqz v5, :cond_8

    iget v5, v5, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$SavedState;->mSpanOffsetsSize:I

    if-ge v5, v4, :cond_f

    .line 651
    :cond_8
    iget-boolean v5, v0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$AnchorInfo;->mInvalidateOffsets:Z

    if-eqz v5, :cond_b

    .line 652
    const/4 v5, 0x0

    .local v5, "i":I
    :goto_3
    iget v6, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mSpanCount:I

    if-ge v5, v6, :cond_a

    .line 654
    iget-object v6, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mSpans:[Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;

    aget-object v6, v6, v5

    invoke-virtual {v6}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;->clear()V

    .line 655
    iget v6, v0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$AnchorInfo;->mOffset:I

    const/high16 v7, -0x80000000

    if-eq v6, v7, :cond_9

    .line 656
    iget-object v6, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mSpans:[Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;

    aget-object v6, v6, v5

    iget v7, v0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$AnchorInfo;->mOffset:I

    invoke-virtual {v6, v7}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;->setLine(I)V

    .line 652
    :cond_9
    add-int/lit8 v5, v5, 0x1

    goto :goto_3

    .end local v5    # "i":I
    :cond_a
    goto :goto_7

    .line 660
    :cond_b
    if-nez v1, :cond_d

    iget-object v5, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mAnchorInfo:Landroidx/recyclerview/widget/StaggeredGridLayoutManager$AnchorInfo;

    iget-object v5, v5, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$AnchorInfo;->mSpanReferenceLines:[I

    if-nez v5, :cond_c

    goto :goto_5

    .line 667
    :cond_c
    const/4 v5, 0x0

    .restart local v5    # "i":I
    :goto_4
    iget v6, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mSpanCount:I

    if-ge v5, v6, :cond_f

    .line 668
    iget-object v6, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mSpans:[Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;

    aget-object v6, v6, v5

    .line 669
    .local v6, "span":Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;
    invoke-virtual {v6}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;->clear()V

    .line 670
    iget-object v7, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mAnchorInfo:Landroidx/recyclerview/widget/StaggeredGridLayoutManager$AnchorInfo;

    iget-object v7, v7, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$AnchorInfo;->mSpanReferenceLines:[I

    aget v7, v7, v5

    invoke-virtual {v6, v7}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;->setLine(I)V

    .line 667
    .end local v6    # "span":Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;
    add-int/lit8 v5, v5, 0x1

    goto :goto_4

    .line 661
    .end local v5    # "i":I
    :cond_d
    :goto_5
    const/4 v5, 0x0

    .restart local v5    # "i":I
    :goto_6
    iget v6, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mSpanCount:I

    if-ge v5, v6, :cond_e

    .line 662
    iget-object v6, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mSpans:[Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;

    aget-object v6, v6, v5

    iget-boolean v7, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mShouldReverseLayout:Z

    iget v8, v0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$AnchorInfo;->mOffset:I

    invoke-virtual {v6, v7, v8}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;->cacheReferenceLineAndClear(ZI)V

    .line 661
    add-int/lit8 v5, v5, 0x1

    goto :goto_6

    .line 665
    .end local v5    # "i":I
    :cond_e
    iget-object v5, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mAnchorInfo:Landroidx/recyclerview/widget/StaggeredGridLayoutManager$AnchorInfo;

    iget-object v6, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mSpans:[Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;

    invoke-virtual {v5, v6}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$AnchorInfo;->saveSpanReferenceLines([Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;)V

    .line 675
    :cond_f
    :goto_7
    invoke-virtual {p0, p1}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->detachAndScrapAttachedViews(Landroidx/recyclerview/widget/RecyclerView$Recycler;)V

    .line 676
    iget-object v5, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mLayoutState:Landroidx/recyclerview/widget/LayoutState;

    iput-boolean v3, v5, Landroidx/recyclerview/widget/LayoutState;->mRecycle:Z

    .line 677
    iput-boolean v3, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mLaidOutInvalidFullSpan:Z

    .line 678
    iget-object v5, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mSecondaryOrientation:Landroidx/recyclerview/widget/OrientationHelper;

    invoke-virtual {v5}, Landroidx/recyclerview/widget/OrientationHelper;->getTotalSpace()I

    move-result v5

    invoke-virtual {p0, v5}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->updateMeasureSpecs(I)V

    .line 679
    iget v5, v0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$AnchorInfo;->mPosition:I

    invoke-direct {p0, v5, p2}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->updateLayoutState(ILandroidx/recyclerview/widget/RecyclerView$State;)V

    .line 680
    iget-boolean v5, v0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$AnchorInfo;->mLayoutFromEnd:Z

    if-eqz v5, :cond_10

    .line 682
    invoke-direct {p0, v2}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->setLayoutStateDirection(I)V

    .line 683
    iget-object v2, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mLayoutState:Landroidx/recyclerview/widget/LayoutState;

    invoke-direct {p0, p1, v2, p2}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->fill(Landroidx/recyclerview/widget/RecyclerView$Recycler;Landroidx/recyclerview/widget/LayoutState;Landroidx/recyclerview/widget/RecyclerView$State;)I

    .line 685
    invoke-direct {p0, v4}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->setLayoutStateDirection(I)V

    .line 686
    iget-object v2, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mLayoutState:Landroidx/recyclerview/widget/LayoutState;

    iget v5, v0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$AnchorInfo;->mPosition:I

    iget-object v6, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mLayoutState:Landroidx/recyclerview/widget/LayoutState;

    iget v6, v6, Landroidx/recyclerview/widget/LayoutState;->mItemDirection:I

    add-int/2addr v5, v6

    iput v5, v2, Landroidx/recyclerview/widget/LayoutState;->mCurrentPosition:I

    .line 687
    iget-object v2, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mLayoutState:Landroidx/recyclerview/widget/LayoutState;

    invoke-direct {p0, p1, v2, p2}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->fill(Landroidx/recyclerview/widget/RecyclerView$Recycler;Landroidx/recyclerview/widget/LayoutState;Landroidx/recyclerview/widget/RecyclerView$State;)I

    goto :goto_8

    .line 690
    :cond_10
    invoke-direct {p0, v4}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->setLayoutStateDirection(I)V

    .line 691
    iget-object v5, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mLayoutState:Landroidx/recyclerview/widget/LayoutState;

    invoke-direct {p0, p1, v5, p2}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->fill(Landroidx/recyclerview/widget/RecyclerView$Recycler;Landroidx/recyclerview/widget/LayoutState;Landroidx/recyclerview/widget/RecyclerView$State;)I

    .line 693
    invoke-direct {p0, v2}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->setLayoutStateDirection(I)V

    .line 694
    iget-object v2, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mLayoutState:Landroidx/recyclerview/widget/LayoutState;

    iget v5, v0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$AnchorInfo;->mPosition:I

    iget-object v6, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mLayoutState:Landroidx/recyclerview/widget/LayoutState;

    iget v6, v6, Landroidx/recyclerview/widget/LayoutState;->mItemDirection:I

    add-int/2addr v5, v6

    iput v5, v2, Landroidx/recyclerview/widget/LayoutState;->mCurrentPosition:I

    .line 695
    iget-object v2, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mLayoutState:Landroidx/recyclerview/widget/LayoutState;

    invoke-direct {p0, p1, v2, p2}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->fill(Landroidx/recyclerview/widget/RecyclerView$Recycler;Landroidx/recyclerview/widget/LayoutState;Landroidx/recyclerview/widget/RecyclerView$State;)I

    .line 698
    :goto_8
    invoke-direct {p0}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->repositionToWrapContentIfNecessary()V

    .line 700
    invoke-virtual {p0}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->getChildCount()I

    move-result v2

    if-lez v2, :cond_12

    .line 701
    iget-boolean v2, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mShouldReverseLayout:Z

    if-eqz v2, :cond_11

    .line 702
    invoke-direct {p0, p1, p2, v4}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->fixEndGap(Landroidx/recyclerview/widget/RecyclerView$Recycler;Landroidx/recyclerview/widget/RecyclerView$State;Z)V

    .line 703
    invoke-direct {p0, p1, p2, v3}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->fixStartGap(Landroidx/recyclerview/widget/RecyclerView$Recycler;Landroidx/recyclerview/widget/RecyclerView$State;Z)V

    goto :goto_9

    .line 705
    :cond_11
    invoke-direct {p0, p1, p2, v4}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->fixStartGap(Landroidx/recyclerview/widget/RecyclerView$Recycler;Landroidx/recyclerview/widget/RecyclerView$State;Z)V

    .line 706
    invoke-direct {p0, p1, p2, v3}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->fixEndGap(Landroidx/recyclerview/widget/RecyclerView$Recycler;Landroidx/recyclerview/widget/RecyclerView$State;Z)V

    .line 709
    :cond_12
    :goto_9
    const/4 v2, 0x0

    .line 710
    .local v2, "hasGaps":Z
    if-eqz p3, :cond_15

    invoke-virtual {p2}, Landroidx/recyclerview/widget/RecyclerView$State;->isPreLayout()Z

    move-result v5

    if-nez v5, :cond_15

    .line 711
    iget v5, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mGapStrategy:I

    if-eqz v5, :cond_14

    .line 712
    invoke-virtual {p0}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->getChildCount()I

    move-result v5

    if-lez v5, :cond_14

    iget-boolean v5, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mLaidOutInvalidFullSpan:Z

    if-nez v5, :cond_13

    .line 713
    invoke-virtual {p0}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->hasGapsToFix()Landroid/view/View;

    move-result-object v5

    if-eqz v5, :cond_14

    :cond_13
    goto :goto_a

    :cond_14
    const/4 v4, 0x0

    .line 714
    .local v4, "needToCheckForGaps":Z
    :goto_a
    if-eqz v4, :cond_15

    .line 715
    iget-object v5, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mCheckForGapsRunnable:Ljava/lang/Runnable;

    invoke-virtual {p0, v5}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->removeCallbacks(Ljava/lang/Runnable;)Z

    .line 716
    invoke-virtual {p0}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->checkForGaps()Z

    move-result v5

    if-eqz v5, :cond_15

    .line 717
    const/4 v2, 0x1

    .line 721
    .end local v4    # "needToCheckForGaps":Z
    :cond_15
    invoke-virtual {p2}, Landroidx/recyclerview/widget/RecyclerView$State;->isPreLayout()Z

    move-result v4

    if-eqz v4, :cond_16

    .line 722
    iget-object v4, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mAnchorInfo:Landroidx/recyclerview/widget/StaggeredGridLayoutManager$AnchorInfo;

    invoke-virtual {v4}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$AnchorInfo;->reset()V

    .line 724
    :cond_16
    iget-boolean v4, v0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$AnchorInfo;->mLayoutFromEnd:Z

    iput-boolean v4, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mLastLayoutFromEnd:Z

    .line 725
    invoke-virtual {p0}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->isLayoutRTL()Z

    move-result v4

    iput-boolean v4, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mLastLayoutRTL:Z

    .line 726
    if-eqz v2, :cond_17

    .line 727
    iget-object v4, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mAnchorInfo:Landroidx/recyclerview/widget/StaggeredGridLayoutManager$AnchorInfo;

    invoke-virtual {v4}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$AnchorInfo;->reset()V

    .line 728
    invoke-direct {p0, p1, p2, v3}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->onLayoutChildren(Landroidx/recyclerview/widget/RecyclerView$Recycler;Landroidx/recyclerview/widget/RecyclerView$State;Z)V

    .line 730
    :cond_17
    return-void
.end method

.method private preferLastSpan(I)Z
    .locals 4
    .param p1, "layoutDir"    # I

    .line 1944
    iget v0, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mOrientation:I

    const/4 v1, -0x1

    const/4 v2, 0x1

    const/4 v3, 0x0

    if-nez v0, :cond_2

    .line 1945
    if-ne p1, v1, :cond_0

    const/4 v0, 0x1

    goto :goto_0

    :cond_0
    const/4 v0, 0x0

    :goto_0
    iget-boolean v1, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mShouldReverseLayout:Z

    if-eq v0, v1, :cond_1

    goto :goto_1

    :cond_1
    const/4 v2, 0x0

    :goto_1
    return v2

    .line 1947
    :cond_2
    if-ne p1, v1, :cond_3

    const/4 v0, 0x1

    goto :goto_2

    :cond_3
    const/4 v0, 0x0

    :goto_2
    iget-boolean v1, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mShouldReverseLayout:Z

    if-ne v0, v1, :cond_4

    const/4 v0, 0x1

    goto :goto_3

    :cond_4
    const/4 v0, 0x0

    :goto_3
    invoke-virtual {p0}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->isLayoutRTL()Z

    move-result v1

    if-ne v0, v1, :cond_5

    goto :goto_4

    :cond_5
    const/4 v2, 0x0

    :goto_4
    return v2
.end method

.method private prependViewToAllSpans(Landroid/view/View;)V
    .locals 2
    .param p1, "view"    # Landroid/view/View;

    .line 1787
    iget v0, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mSpanCount:I

    add-int/lit8 v0, v0, -0x1

    .local v0, "i":I
    :goto_0
    if-ltz v0, :cond_0

    .line 1788
    iget-object v1, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mSpans:[Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;

    aget-object v1, v1, v0

    invoke-virtual {v1, p1}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;->prependToSpan(Landroid/view/View;)V

    .line 1787
    add-int/lit8 v0, v0, -0x1

    goto :goto_0

    .line 1790
    .end local v0    # "i":I
    :cond_0
    return-void
.end method

.method private recycle(Landroidx/recyclerview/widget/RecyclerView$Recycler;Landroidx/recyclerview/widget/LayoutState;)V
    .locals 3
    .param p1, "recycler"    # Landroidx/recyclerview/widget/RecyclerView$Recycler;
    .param p2, "layoutState"    # Landroidx/recyclerview/widget/LayoutState;

    .line 1740
    iget-boolean v0, p2, Landroidx/recyclerview/widget/LayoutState;->mRecycle:Z

    if-eqz v0, :cond_6

    iget-boolean v0, p2, Landroidx/recyclerview/widget/LayoutState;->mInfinite:Z

    if-eqz v0, :cond_0

    goto :goto_3

    .line 1743
    :cond_0
    iget v0, p2, Landroidx/recyclerview/widget/LayoutState;->mAvailable:I

    const/4 v1, -0x1

    if-nez v0, :cond_2

    .line 1745
    iget v0, p2, Landroidx/recyclerview/widget/LayoutState;->mLayoutDirection:I

    if-ne v0, v1, :cond_1

    .line 1746
    iget v0, p2, Landroidx/recyclerview/widget/LayoutState;->mEndLine:I

    invoke-direct {p0, p1, v0}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->recycleFromEnd(Landroidx/recyclerview/widget/RecyclerView$Recycler;I)V

    goto :goto_2

    .line 1748
    :cond_1
    iget v0, p2, Landroidx/recyclerview/widget/LayoutState;->mStartLine:I

    invoke-direct {p0, p1, v0}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->recycleFromStart(Landroidx/recyclerview/widget/RecyclerView$Recycler;I)V

    goto :goto_2

    .line 1753
    :cond_2
    iget v0, p2, Landroidx/recyclerview/widget/LayoutState;->mLayoutDirection:I

    if-ne v0, v1, :cond_4

    .line 1755
    iget v0, p2, Landroidx/recyclerview/widget/LayoutState;->mStartLine:I

    iget v1, p2, Landroidx/recyclerview/widget/LayoutState;->mStartLine:I

    invoke-direct {p0, v1}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->getMaxStart(I)I

    move-result v1

    sub-int/2addr v0, v1

    .line 1757
    .local v0, "scrolled":I
    if-gez v0, :cond_3

    .line 1758
    iget v1, p2, Landroidx/recyclerview/widget/LayoutState;->mEndLine:I

    .local v1, "line":I
    goto :goto_0

    .line 1760
    .end local v1    # "line":I
    :cond_3
    iget v1, p2, Landroidx/recyclerview/widget/LayoutState;->mEndLine:I

    iget v2, p2, Landroidx/recyclerview/widget/LayoutState;->mAvailable:I

    invoke-static {v0, v2}, Ljava/lang/Math;->min(II)I

    move-result v2

    sub-int/2addr v1, v2

    .line 1762
    .restart local v1    # "line":I
    :goto_0
    invoke-direct {p0, p1, v1}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->recycleFromEnd(Landroidx/recyclerview/widget/RecyclerView$Recycler;I)V

    .line 1763
    .end local v0    # "scrolled":I
    .end local v1    # "line":I
    goto :goto_2

    .line 1765
    :cond_4
    iget v0, p2, Landroidx/recyclerview/widget/LayoutState;->mEndLine:I

    invoke-direct {p0, v0}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->getMinEnd(I)I

    move-result v0

    iget v1, p2, Landroidx/recyclerview/widget/LayoutState;->mEndLine:I

    sub-int/2addr v0, v1

    .line 1767
    .restart local v0    # "scrolled":I
    if-gez v0, :cond_5

    .line 1768
    iget v1, p2, Landroidx/recyclerview/widget/LayoutState;->mStartLine:I

    .restart local v1    # "line":I
    goto :goto_1

    .line 1770
    .end local v1    # "line":I
    :cond_5
    iget v1, p2, Landroidx/recyclerview/widget/LayoutState;->mStartLine:I

    iget v2, p2, Landroidx/recyclerview/widget/LayoutState;->mAvailable:I

    invoke-static {v0, v2}, Ljava/lang/Math;->min(II)I

    move-result v2

    add-int/2addr v1, v2

    .line 1772
    .restart local v1    # "line":I
    :goto_1
    invoke-direct {p0, p1, v1}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->recycleFromStart(Landroidx/recyclerview/widget/RecyclerView$Recycler;I)V

    .line 1776
    .end local v0    # "scrolled":I
    .end local v1    # "line":I
    :goto_2
    return-void

    .line 1741
    :cond_6
    :goto_3
    return-void
.end method

.method private recycleFromEnd(Landroidx/recyclerview/widget/RecyclerView$Recycler;I)V
    .locals 7
    .param p1, "recycler"    # Landroidx/recyclerview/widget/RecyclerView$Recycler;
    .param p2, "line"    # I

    .line 1910
    invoke-virtual {p0}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->getChildCount()I

    move-result v0

    .line 1912
    .local v0, "childCount":I
    add-int/lit8 v1, v0, -0x1

    .local v1, "i":I
    :goto_0
    if-ltz v1, :cond_6

    .line 1913
    invoke-virtual {p0, v1}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->getChildAt(I)Landroid/view/View;

    move-result-object v2

    .line 1914
    .local v2, "child":Landroid/view/View;
    iget-object v3, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mPrimaryOrientation:Landroidx/recyclerview/widget/OrientationHelper;

    invoke-virtual {v3, v2}, Landroidx/recyclerview/widget/OrientationHelper;->getDecoratedStart(Landroid/view/View;)I

    move-result v3

    if-lt v3, p2, :cond_5

    iget-object v3, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mPrimaryOrientation:Landroidx/recyclerview/widget/OrientationHelper;

    .line 1915
    invoke-virtual {v3, v2}, Landroidx/recyclerview/widget/OrientationHelper;->getTransformedStartWithDecoration(Landroid/view/View;)I

    move-result v3

    if-lt v3, p2, :cond_5

    .line 1916
    invoke-virtual {v2}, Landroid/view/View;->getLayoutParams()Landroid/view/ViewGroup$LayoutParams;

    move-result-object v3

    check-cast v3, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LayoutParams;

    .line 1918
    .local v3, "lp":Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LayoutParams;
    iget-boolean v4, v3, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LayoutParams;->mFullSpan:Z

    const/4 v5, 0x1

    if-eqz v4, :cond_3

    .line 1919
    const/4 v4, 0x0

    .local v4, "j":I
    :goto_1
    iget v6, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mSpanCount:I

    if-ge v4, v6, :cond_1

    .line 1920
    iget-object v6, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mSpans:[Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;

    aget-object v6, v6, v4

    iget-object v6, v6, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;->mViews:Ljava/util/ArrayList;

    invoke-virtual {v6}, Ljava/util/ArrayList;->size()I

    move-result v6

    if-ne v6, v5, :cond_0

    .line 1921
    return-void

    .line 1919
    :cond_0
    add-int/lit8 v4, v4, 0x1

    goto :goto_1

    .line 1924
    .end local v4    # "j":I
    :cond_1
    const/4 v4, 0x0

    .restart local v4    # "j":I
    :goto_2
    iget v5, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mSpanCount:I

    if-ge v4, v5, :cond_2

    .line 1925
    iget-object v5, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mSpans:[Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;

    aget-object v5, v5, v4

    invoke-virtual {v5}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;->popEnd()V

    .line 1924
    add-int/lit8 v4, v4, 0x1

    goto :goto_2

    .end local v4    # "j":I
    :cond_2
    goto :goto_3

    .line 1928
    :cond_3
    iget-object v4, v3, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LayoutParams;->mSpan:Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;

    iget-object v4, v4, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;->mViews:Ljava/util/ArrayList;

    invoke-virtual {v4}, Ljava/util/ArrayList;->size()I

    move-result v4

    if-ne v4, v5, :cond_4

    .line 1929
    return-void

    .line 1931
    :cond_4
    iget-object v4, v3, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LayoutParams;->mSpan:Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;

    invoke-virtual {v4}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;->popEnd()V

    .line 1933
    :goto_3
    invoke-virtual {p0, v2, p1}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->removeAndRecycleView(Landroid/view/View;Landroidx/recyclerview/widget/RecyclerView$Recycler;)V

    .line 1934
    .end local v3    # "lp":Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LayoutParams;
    nop

    .line 1912
    .end local v2    # "child":Landroid/view/View;
    add-int/lit8 v1, v1, -0x1

    goto :goto_0

    .line 1935
    .restart local v2    # "child":Landroid/view/View;
    :cond_5
    return-void

    .line 1938
    .end local v2    # "child":Landroid/view/View;
    :cond_6
    return-void
.end method

.method private recycleFromStart(Landroidx/recyclerview/widget/RecyclerView$Recycler;I)V
    .locals 5
    .param p1, "recycler"    # Landroidx/recyclerview/widget/RecyclerView$Recycler;
    .param p2, "line"    # I

    .line 1881
    :goto_0
    invoke-virtual {p0}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->getChildCount()I

    move-result v0

    if-lez v0, :cond_6

    .line 1882
    const/4 v0, 0x0

    invoke-virtual {p0, v0}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->getChildAt(I)Landroid/view/View;

    move-result-object v0

    .line 1883
    .local v0, "child":Landroid/view/View;
    iget-object v1, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mPrimaryOrientation:Landroidx/recyclerview/widget/OrientationHelper;

    invoke-virtual {v1, v0}, Landroidx/recyclerview/widget/OrientationHelper;->getDecoratedEnd(Landroid/view/View;)I

    move-result v1

    if-gt v1, p2, :cond_5

    iget-object v1, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mPrimaryOrientation:Landroidx/recyclerview/widget/OrientationHelper;

    .line 1884
    invoke-virtual {v1, v0}, Landroidx/recyclerview/widget/OrientationHelper;->getTransformedEndWithDecoration(Landroid/view/View;)I

    move-result v1

    if-gt v1, p2, :cond_5

    .line 1885
    invoke-virtual {v0}, Landroid/view/View;->getLayoutParams()Landroid/view/ViewGroup$LayoutParams;

    move-result-object v1

    check-cast v1, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LayoutParams;

    .line 1887
    .local v1, "lp":Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LayoutParams;
    iget-boolean v2, v1, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LayoutParams;->mFullSpan:Z

    const/4 v3, 0x1

    if-eqz v2, :cond_3

    .line 1888
    const/4 v2, 0x0

    .local v2, "j":I
    :goto_1
    iget v4, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mSpanCount:I

    if-ge v2, v4, :cond_1

    .line 1889
    iget-object v4, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mSpans:[Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;

    aget-object v4, v4, v2

    iget-object v4, v4, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;->mViews:Ljava/util/ArrayList;

    invoke-virtual {v4}, Ljava/util/ArrayList;->size()I

    move-result v4

    if-ne v4, v3, :cond_0

    .line 1890
    return-void

    .line 1888
    :cond_0
    add-int/lit8 v2, v2, 0x1

    goto :goto_1

    .line 1893
    .end local v2    # "j":I
    :cond_1
    const/4 v2, 0x0

    .restart local v2    # "j":I
    :goto_2
    iget v3, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mSpanCount:I

    if-ge v2, v3, :cond_2

    .line 1894
    iget-object v3, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mSpans:[Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;

    aget-object v3, v3, v2

    invoke-virtual {v3}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;->popStart()V

    .line 1893
    add-int/lit8 v2, v2, 0x1

    goto :goto_2

    .end local v2    # "j":I
    :cond_2
    goto :goto_3

    .line 1897
    :cond_3
    iget-object v2, v1, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LayoutParams;->mSpan:Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;

    iget-object v2, v2, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;->mViews:Ljava/util/ArrayList;

    invoke-virtual {v2}, Ljava/util/ArrayList;->size()I

    move-result v2

    if-ne v2, v3, :cond_4

    .line 1898
    return-void

    .line 1900
    :cond_4
    iget-object v2, v1, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LayoutParams;->mSpan:Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;

    invoke-virtual {v2}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;->popStart()V

    .line 1902
    :goto_3
    invoke-virtual {p0, v0, p1}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->removeAndRecycleView(Landroid/view/View;Landroidx/recyclerview/widget/RecyclerView$Recycler;)V

    .line 1903
    .end local v1    # "lp":Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LayoutParams;
    nop

    .line 1906
    .end local v0    # "child":Landroid/view/View;
    goto :goto_0

    .line 1904
    .restart local v0    # "child":Landroid/view/View;
    :cond_5
    return-void

    .line 1907
    .end local v0    # "child":Landroid/view/View;
    :cond_6
    return-void
.end method

.method private repositionToWrapContentIfNecessary()V
    .locals 11

    .line 742
    iget-object v0, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mSecondaryOrientation:Landroidx/recyclerview/widget/OrientationHelper;

    invoke-virtual {v0}, Landroidx/recyclerview/widget/OrientationHelper;->getMode()I

    move-result v0

    const/high16 v1, 0x40000000    # 2.0f

    if-ne v0, v1, :cond_0

    .line 743
    return-void

    .line 745
    :cond_0
    const/4 v0, 0x0

    .line 746
    .local v0, "maxSize":F
    invoke-virtual {p0}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->getChildCount()I

    move-result v1

    .line 747
    .local v1, "childCount":I
    const/4 v2, 0x0

    .local v2, "i":I
    :goto_0
    if-ge v2, v1, :cond_3

    .line 748
    invoke-virtual {p0, v2}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->getChildAt(I)Landroid/view/View;

    move-result-object v3

    .line 749
    .local v3, "child":Landroid/view/View;
    iget-object v4, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mSecondaryOrientation:Landroidx/recyclerview/widget/OrientationHelper;

    invoke-virtual {v4, v3}, Landroidx/recyclerview/widget/OrientationHelper;->getDecoratedMeasurement(Landroid/view/View;)I

    move-result v4

    int-to-float v4, v4

    .line 750
    .local v4, "size":F
    cmpg-float v5, v4, v0

    if-gez v5, :cond_1

    .line 751
    goto :goto_1

    .line 753
    :cond_1
    invoke-virtual {v3}, Landroid/view/View;->getLayoutParams()Landroid/view/ViewGroup$LayoutParams;

    move-result-object v5

    check-cast v5, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LayoutParams;

    .line 754
    .local v5, "layoutParams":Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LayoutParams;
    invoke-virtual {v5}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LayoutParams;->isFullSpan()Z

    move-result v6

    if-eqz v6, :cond_2

    .line 755
    const/high16 v6, 0x3f800000    # 1.0f

    mul-float v6, v6, v4

    iget v7, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mSpanCount:I

    int-to-float v7, v7

    div-float v4, v6, v7

    .line 757
    :cond_2
    invoke-static {v0, v4}, Ljava/lang/Math;->max(FF)F

    move-result v0

    .line 747
    .end local v3    # "child":Landroid/view/View;
    .end local v4    # "size":F
    .end local v5    # "layoutParams":Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LayoutParams;
    :goto_1
    add-int/lit8 v2, v2, 0x1

    goto :goto_0

    .line 759
    .end local v2    # "i":I
    :cond_3
    iget v2, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mSizePerSpan:I

    .line 760
    .local v2, "before":I
    iget v3, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mSpanCount:I

    int-to-float v3, v3

    mul-float v3, v3, v0

    invoke-static {v3}, Ljava/lang/Math;->round(F)I

    move-result v3

    .line 761
    .local v3, "desired":I
    iget-object v4, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mSecondaryOrientation:Landroidx/recyclerview/widget/OrientationHelper;

    invoke-virtual {v4}, Landroidx/recyclerview/widget/OrientationHelper;->getMode()I

    move-result v4

    const/high16 v5, -0x80000000

    if-ne v4, v5, :cond_4

    .line 762
    iget-object v4, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mSecondaryOrientation:Landroidx/recyclerview/widget/OrientationHelper;

    invoke-virtual {v4}, Landroidx/recyclerview/widget/OrientationHelper;->getTotalSpace()I

    move-result v4

    invoke-static {v3, v4}, Ljava/lang/Math;->min(II)I

    move-result v3

    .line 764
    :cond_4
    invoke-virtual {p0, v3}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->updateMeasureSpecs(I)V

    .line 765
    iget v4, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mSizePerSpan:I

    if-ne v4, v2, :cond_5

    .line 766
    return-void

    .line 768
    :cond_5
    const/4 v4, 0x0

    .local v4, "i":I
    :goto_2
    if-ge v4, v1, :cond_9

    .line 769
    invoke-virtual {p0, v4}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->getChildAt(I)Landroid/view/View;

    move-result-object v5

    .line 770
    .local v5, "child":Landroid/view/View;
    invoke-virtual {v5}, Landroid/view/View;->getLayoutParams()Landroid/view/ViewGroup$LayoutParams;

    move-result-object v6

    check-cast v6, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LayoutParams;

    .line 771
    .local v6, "lp":Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LayoutParams;
    iget-boolean v7, v6, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LayoutParams;->mFullSpan:Z

    if-eqz v7, :cond_6

    .line 772
    goto :goto_3

    .line 774
    :cond_6
    invoke-virtual {p0}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->isLayoutRTL()Z

    move-result v7

    const/4 v8, 0x1

    if-eqz v7, :cond_7

    iget v7, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mOrientation:I

    if-ne v7, v8, :cond_7

    .line 775
    iget v7, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mSpanCount:I

    sub-int/2addr v7, v8

    iget-object v9, v6, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LayoutParams;->mSpan:Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;

    iget v9, v9, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;->mIndex:I

    sub-int/2addr v7, v9

    neg-int v7, v7

    iget v9, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mSizePerSpan:I

    mul-int v7, v7, v9

    .line 776
    .local v7, "newOffset":I
    iget v9, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mSpanCount:I

    sub-int/2addr v9, v8

    iget-object v8, v6, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LayoutParams;->mSpan:Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;

    iget v8, v8, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;->mIndex:I

    sub-int/2addr v9, v8

    neg-int v8, v9

    mul-int v8, v8, v2

    .line 777
    .local v8, "prevOffset":I
    sub-int v9, v7, v8

    invoke-virtual {v5, v9}, Landroid/view/View;->offsetLeftAndRight(I)V

    .line 778
    .end local v7    # "newOffset":I
    .end local v8    # "prevOffset":I
    goto :goto_3

    .line 779
    :cond_7
    iget-object v7, v6, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LayoutParams;->mSpan:Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;

    iget v7, v7, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;->mIndex:I

    iget v9, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mSizePerSpan:I

    mul-int v7, v7, v9

    .line 780
    .restart local v7    # "newOffset":I
    iget-object v9, v6, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LayoutParams;->mSpan:Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;

    iget v9, v9, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;->mIndex:I

    mul-int v9, v9, v2

    .line 781
    .local v9, "prevOffset":I
    iget v10, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mOrientation:I

    if-ne v10, v8, :cond_8

    .line 782
    sub-int v8, v7, v9

    invoke-virtual {v5, v8}, Landroid/view/View;->offsetLeftAndRight(I)V

    goto :goto_3

    .line 784
    :cond_8
    sub-int v8, v7, v9

    invoke-virtual {v5, v8}, Landroid/view/View;->offsetTopAndBottom(I)V

    .line 768
    .end local v5    # "child":Landroid/view/View;
    .end local v6    # "lp":Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LayoutParams;
    .end local v7    # "newOffset":I
    .end local v9    # "prevOffset":I
    :goto_3
    add-int/lit8 v4, v4, 0x1

    goto :goto_2

    .line 788
    .end local v4    # "i":I
    :cond_9
    return-void
.end method

.method private resolveShouldLayoutReverse()V
    .locals 2

    .line 559
    iget v0, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mOrientation:I

    const/4 v1, 0x1

    if-eq v0, v1, :cond_1

    invoke-virtual {p0}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->isLayoutRTL()Z

    move-result v0

    if-nez v0, :cond_0

    goto :goto_0

    .line 562
    :cond_0
    iget-boolean v0, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mReverseLayout:Z

    xor-int/2addr v0, v1

    iput-boolean v0, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mShouldReverseLayout:Z

    goto :goto_1

    .line 560
    :cond_1
    :goto_0
    iget-boolean v0, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mReverseLayout:Z

    iput-boolean v0, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mShouldReverseLayout:Z

    .line 564
    :goto_1
    return-void
.end method

.method private setLayoutStateDirection(I)V
    .locals 5
    .param p1, "direction"    # I

    .line 1453
    iget-object v0, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mLayoutState:Landroidx/recyclerview/widget/LayoutState;

    iput p1, v0, Landroidx/recyclerview/widget/LayoutState;->mLayoutDirection:I

    .line 1454
    iget-object v0, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mLayoutState:Landroidx/recyclerview/widget/LayoutState;

    iget-boolean v1, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mShouldReverseLayout:Z

    const/4 v2, 0x1

    const/4 v3, -0x1

    if-ne p1, v3, :cond_0

    const/4 v4, 0x1

    goto :goto_0

    :cond_0
    const/4 v4, 0x0

    :goto_0
    if-ne v1, v4, :cond_1

    .line 1455
    goto :goto_1

    :cond_1
    const/4 v2, -0x1

    :goto_1
    iput v2, v0, Landroidx/recyclerview/widget/LayoutState;->mItemDirection:I

    .line 1456
    return-void
.end method

.method private updateAllRemainingSpans(II)V
    .locals 2
    .param p1, "layoutDir"    # I
    .param p2, "targetLine"    # I

    .line 1793
    const/4 v0, 0x0

    .local v0, "i":I
    :goto_0
    iget v1, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mSpanCount:I

    if-ge v0, v1, :cond_1

    .line 1794
    iget-object v1, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mSpans:[Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;

    aget-object v1, v1, v0

    iget-object v1, v1, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;->mViews:Ljava/util/ArrayList;

    invoke-virtual {v1}, Ljava/util/ArrayList;->isEmpty()Z

    move-result v1

    if-eqz v1, :cond_0

    .line 1795
    goto :goto_1

    .line 1797
    :cond_0
    iget-object v1, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mSpans:[Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;

    aget-object v1, v1, v0

    invoke-direct {p0, v1, p1, p2}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->updateRemainingSpans(Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;II)V

    .line 1793
    :goto_1
    add-int/lit8 v0, v0, 0x1

    goto :goto_0

    .line 1799
    .end local v0    # "i":I
    :cond_1
    return-void
.end method

.method private updateAnchorFromChildren(Landroidx/recyclerview/widget/RecyclerView$State;Landroidx/recyclerview/widget/StaggeredGridLayoutManager$AnchorInfo;)Z
    .locals 1
    .param p1, "state"    # Landroidx/recyclerview/widget/RecyclerView$State;
    .param p2, "anchorInfo"    # Landroidx/recyclerview/widget/StaggeredGridLayoutManager$AnchorInfo;

    .line 848
    iget-boolean v0, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mLastLayoutFromEnd:Z

    if-eqz v0, :cond_0

    .line 849
    invoke-virtual {p1}, Landroidx/recyclerview/widget/RecyclerView$State;->getItemCount()I

    move-result v0

    invoke-direct {p0, v0}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->findLastReferenceChildPosition(I)I

    move-result v0

    goto :goto_0

    .line 850
    :cond_0
    invoke-virtual {p1}, Landroidx/recyclerview/widget/RecyclerView$State;->getItemCount()I

    move-result v0

    invoke-direct {p0, v0}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->findFirstReferenceChildPosition(I)I

    move-result v0

    :goto_0
    iput v0, p2, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$AnchorInfo;->mPosition:I

    .line 851
    const/high16 v0, -0x80000000

    iput v0, p2, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$AnchorInfo;->mOffset:I

    .line 852
    const/4 v0, 0x1

    return v0
.end method

.method private updateLayoutState(ILandroidx/recyclerview/widget/RecyclerView$State;)V
    .locals 7
    .param p1, "anchorPosition"    # I
    .param p2, "state"    # Landroidx/recyclerview/widget/RecyclerView$State;

    .line 1422
    iget-object v0, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mLayoutState:Landroidx/recyclerview/widget/LayoutState;

    const/4 v1, 0x0

    iput v1, v0, Landroidx/recyclerview/widget/LayoutState;->mAvailable:I

    .line 1423
    iget-object v0, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mLayoutState:Landroidx/recyclerview/widget/LayoutState;

    iput p1, v0, Landroidx/recyclerview/widget/LayoutState;->mCurrentPosition:I

    .line 1424
    const/4 v0, 0x0

    .line 1425
    .local v0, "startExtra":I
    const/4 v2, 0x0

    .line 1426
    .local v2, "endExtra":I
    invoke-virtual {p0}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->isSmoothScrolling()Z

    move-result v3

    const/4 v4, 0x1

    if-eqz v3, :cond_2

    .line 1427
    invoke-virtual {p2}, Landroidx/recyclerview/widget/RecyclerView$State;->getTargetScrollPosition()I

    move-result v3

    .line 1428
    .local v3, "targetPos":I
    const/4 v5, -0x1

    if-eq v3, v5, :cond_2

    .line 1429
    iget-boolean v5, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mShouldReverseLayout:Z

    if-ge v3, p1, :cond_0

    const/4 v6, 0x1

    goto :goto_0

    :cond_0
    const/4 v6, 0x0

    :goto_0
    if-ne v5, v6, :cond_1

    .line 1430
    iget-object v5, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mPrimaryOrientation:Landroidx/recyclerview/widget/OrientationHelper;

    invoke-virtual {v5}, Landroidx/recyclerview/widget/OrientationHelper;->getTotalSpace()I

    move-result v2

    goto :goto_1

    .line 1432
    :cond_1
    iget-object v5, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mPrimaryOrientation:Landroidx/recyclerview/widget/OrientationHelper;

    invoke-virtual {v5}, Landroidx/recyclerview/widget/OrientationHelper;->getTotalSpace()I

    move-result v0

    .line 1438
    .end local v3    # "targetPos":I
    :cond_2
    :goto_1
    invoke-virtual {p0}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->getClipToPadding()Z

    move-result v3

    .line 1439
    .local v3, "clipToPadding":Z
    if-eqz v3, :cond_3

    .line 1440
    iget-object v5, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mLayoutState:Landroidx/recyclerview/widget/LayoutState;

    iget-object v6, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mPrimaryOrientation:Landroidx/recyclerview/widget/OrientationHelper;

    invoke-virtual {v6}, Landroidx/recyclerview/widget/OrientationHelper;->getStartAfterPadding()I

    move-result v6

    sub-int/2addr v6, v0

    iput v6, v5, Landroidx/recyclerview/widget/LayoutState;->mStartLine:I

    .line 1441
    iget-object v5, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mLayoutState:Landroidx/recyclerview/widget/LayoutState;

    iget-object v6, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mPrimaryOrientation:Landroidx/recyclerview/widget/OrientationHelper;

    invoke-virtual {v6}, Landroidx/recyclerview/widget/OrientationHelper;->getEndAfterPadding()I

    move-result v6

    add-int/2addr v6, v2

    iput v6, v5, Landroidx/recyclerview/widget/LayoutState;->mEndLine:I

    goto :goto_2

    .line 1443
    :cond_3
    iget-object v5, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mLayoutState:Landroidx/recyclerview/widget/LayoutState;

    iget-object v6, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mPrimaryOrientation:Landroidx/recyclerview/widget/OrientationHelper;

    invoke-virtual {v6}, Landroidx/recyclerview/widget/OrientationHelper;->getEnd()I

    move-result v6

    add-int/2addr v6, v2

    iput v6, v5, Landroidx/recyclerview/widget/LayoutState;->mEndLine:I

    .line 1444
    iget-object v5, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mLayoutState:Landroidx/recyclerview/widget/LayoutState;

    neg-int v6, v0

    iput v6, v5, Landroidx/recyclerview/widget/LayoutState;->mStartLine:I

    .line 1446
    :goto_2
    iget-object v5, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mLayoutState:Landroidx/recyclerview/widget/LayoutState;

    iput-boolean v1, v5, Landroidx/recyclerview/widget/LayoutState;->mStopInFocusable:Z

    .line 1447
    iget-object v5, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mLayoutState:Landroidx/recyclerview/widget/LayoutState;

    iput-boolean v4, v5, Landroidx/recyclerview/widget/LayoutState;->mRecycle:Z

    .line 1448
    iget-object v5, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mLayoutState:Landroidx/recyclerview/widget/LayoutState;

    iget-object v6, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mPrimaryOrientation:Landroidx/recyclerview/widget/OrientationHelper;

    invoke-virtual {v6}, Landroidx/recyclerview/widget/OrientationHelper;->getMode()I

    move-result v6

    if-nez v6, :cond_4

    iget-object v6, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mPrimaryOrientation:Landroidx/recyclerview/widget/OrientationHelper;

    .line 1449
    invoke-virtual {v6}, Landroidx/recyclerview/widget/OrientationHelper;->getEnd()I

    move-result v6

    if-nez v6, :cond_4

    const/4 v1, 0x1

    goto :goto_3

    :cond_4
    nop

    :goto_3
    iput-boolean v1, v5, Landroidx/recyclerview/widget/LayoutState;->mInfinite:Z

    .line 1450
    return-void
.end method

.method private updateRemainingSpans(Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;II)V
    .locals 5
    .param p1, "span"    # Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;
    .param p2, "layoutDir"    # I
    .param p3, "targetLine"    # I

    .line 1802
    invoke-virtual {p1}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;->getDeletedSize()I

    move-result v0

    .line 1803
    .local v0, "deletedSize":I
    const/4 v1, 0x0

    const/4 v2, -0x1

    if-ne p2, v2, :cond_1

    .line 1804
    invoke-virtual {p1}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;->getStartLine()I

    move-result v2

    .line 1805
    .local v2, "line":I
    add-int v3, v2, v0

    if-gt v3, p3, :cond_0

    .line 1806
    iget-object v3, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mRemainingSpans:Ljava/util/BitSet;

    iget v4, p1, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;->mIndex:I

    invoke-virtual {v3, v4, v1}, Ljava/util/BitSet;->set(IZ)V

    .line 1808
    .end local v2    # "line":I
    :cond_0
    goto :goto_0

    .line 1809
    :cond_1
    invoke-virtual {p1}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;->getEndLine()I

    move-result v2

    .line 1810
    .restart local v2    # "line":I
    sub-int v3, v2, v0

    if-lt v3, p3, :cond_2

    .line 1811
    iget-object v3, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mRemainingSpans:Ljava/util/BitSet;

    iget v4, p1, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;->mIndex:I

    invoke-virtual {v3, v4, v1}, Ljava/util/BitSet;->set(IZ)V

    .line 1814
    .end local v2    # "line":I
    :cond_2
    :goto_0
    return-void
.end method

.method private updateSpecWithExtra(III)I
    .locals 3
    .param p1, "spec"    # I
    .param p2, "startInset"    # I
    .param p3, "endInset"    # I

    .line 1217
    if-nez p2, :cond_0

    if-nez p3, :cond_0

    .line 1218
    return p1

    .line 1220
    :cond_0
    invoke-static {p1}, Landroid/view/View$MeasureSpec;->getMode(I)I

    move-result v0

    .line 1221
    .local v0, "mode":I
    const/high16 v1, -0x80000000

    if-eq v0, v1, :cond_2

    const/high16 v1, 0x40000000    # 2.0f

    if-ne v0, v1, :cond_1

    goto :goto_0

    .line 1225
    :cond_1
    return p1

    .line 1222
    :cond_2
    :goto_0
    const/4 v1, 0x0

    .line 1223
    invoke-static {p1}, Landroid/view/View$MeasureSpec;->getSize(I)I

    move-result v2

    sub-int/2addr v2, p2

    sub-int/2addr v2, p3

    invoke-static {v1, v2}, Ljava/lang/Math;->max(II)I

    move-result v1

    .line 1222
    invoke-static {v1, v0}, Landroid/view/View$MeasureSpec;->makeMeasureSpec(II)I

    move-result v1

    return v1
.end method


# virtual methods
.method areAllEndsEqual()Z
    .locals 5

    .line 1839
    iget-object v0, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mSpans:[Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;

    const/4 v1, 0x0

    aget-object v0, v0, v1

    const/high16 v2, -0x80000000

    invoke-virtual {v0, v2}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;->getEndLine(I)I

    move-result v0

    .line 1840
    .local v0, "end":I
    const/4 v3, 0x1

    .local v3, "i":I
    :goto_0
    iget v4, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mSpanCount:I

    if-ge v3, v4, :cond_1

    .line 1841
    iget-object v4, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mSpans:[Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;

    aget-object v4, v4, v3

    invoke-virtual {v4, v2}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;->getEndLine(I)I

    move-result v4

    if-eq v4, v0, :cond_0

    .line 1842
    return v1

    .line 1840
    :cond_0
    add-int/lit8 v3, v3, 0x1

    goto :goto_0

    .line 1845
    .end local v3    # "i":I
    :cond_1
    const/4 v1, 0x1

    return v1
.end method

.method areAllStartsEqual()Z
    .locals 5

    .line 1849
    iget-object v0, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mSpans:[Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;

    const/4 v1, 0x0

    aget-object v0, v0, v1

    const/high16 v2, -0x80000000

    invoke-virtual {v0, v2}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;->getStartLine(I)I

    move-result v0

    .line 1850
    .local v0, "start":I
    const/4 v3, 0x1

    .local v3, "i":I
    :goto_0
    iget v4, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mSpanCount:I

    if-ge v3, v4, :cond_1

    .line 1851
    iget-object v4, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mSpans:[Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;

    aget-object v4, v4, v3

    invoke-virtual {v4, v2}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;->getStartLine(I)I

    move-result v4

    if-eq v4, v0, :cond_0

    .line 1852
    return v1

    .line 1850
    :cond_0
    add-int/lit8 v3, v3, 0x1

    goto :goto_0

    .line 1855
    .end local v3    # "i":I
    :cond_1
    const/4 v1, 0x1

    return v1
.end method

.method public assertNotInLayoutOrScroll(Ljava/lang/String;)V
    .locals 1
    .param p1, "message"    # Ljava/lang/String;

    .line 527
    iget-object v0, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mPendingSavedState:Landroidx/recyclerview/widget/StaggeredGridLayoutManager$SavedState;

    if-nez v0, :cond_0

    .line 528
    invoke-super {p0, p1}, Landroidx/recyclerview/widget/RecyclerView$LayoutManager;->assertNotInLayoutOrScroll(Ljava/lang/String;)V

    .line 530
    :cond_0
    return-void
.end method

.method public canScrollHorizontally()Z
    .locals 1

    .line 2001
    iget v0, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mOrientation:I

    if-nez v0, :cond_0

    const/4 v0, 0x1

    goto :goto_0

    :cond_0
    const/4 v0, 0x0

    :goto_0
    return v0
.end method

.method public canScrollVertically()Z
    .locals 2

    .line 1996
    iget v0, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mOrientation:I

    const/4 v1, 0x1

    if-ne v0, v1, :cond_0

    goto :goto_0

    :cond_0
    const/4 v1, 0x0

    :goto_0
    return v1
.end method

.method checkForGaps()Z
    .locals 8

    .line 269
    invoke-virtual {p0}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->getChildCount()I

    move-result v0

    const/4 v1, 0x0

    if-eqz v0, :cond_7

    iget v0, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mGapStrategy:I

    if-eqz v0, :cond_7

    invoke-virtual {p0}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->isAttachedToWindow()Z

    move-result v0

    if-nez v0, :cond_0

    goto :goto_3

    .line 273
    :cond_0
    iget-boolean v0, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mShouldReverseLayout:Z

    if-eqz v0, :cond_1

    .line 274
    invoke-virtual {p0}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->getLastChildPosition()I

    move-result v0

    .line 275
    .local v0, "minPos":I
    invoke-virtual {p0}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->getFirstChildPosition()I

    move-result v2

    .local v2, "maxPos":I
    goto :goto_0

    .line 277
    .end local v0    # "minPos":I
    .end local v2    # "maxPos":I
    :cond_1
    invoke-virtual {p0}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->getFirstChildPosition()I

    move-result v0

    .line 278
    .restart local v0    # "minPos":I
    invoke-virtual {p0}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->getLastChildPosition()I

    move-result v2

    .line 280
    .restart local v2    # "maxPos":I
    :goto_0
    const/4 v3, 0x1

    if-nez v0, :cond_2

    .line 281
    invoke-virtual {p0}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->hasGapsToFix()Landroid/view/View;

    move-result-object v4

    .line 282
    .local v4, "gapView":Landroid/view/View;
    if-eqz v4, :cond_2

    .line 283
    iget-object v1, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mLazySpanLookup:Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup;

    invoke-virtual {v1}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup;->clear()V

    .line 284
    invoke-virtual {p0}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->requestSimpleAnimationsInNextLayout()V

    .line 285
    invoke-virtual {p0}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->requestLayout()V

    .line 286
    return v3

    .line 289
    .end local v4    # "gapView":Landroid/view/View;
    :cond_2
    iget-boolean v4, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mLaidOutInvalidFullSpan:Z

    if-nez v4, :cond_3

    .line 290
    return v1

    .line 292
    :cond_3
    iget-boolean v4, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mShouldReverseLayout:Z

    if-eqz v4, :cond_4

    const/4 v4, -0x1

    goto :goto_1

    :cond_4
    const/4 v4, 0x1

    .line 293
    .local v4, "invalidGapDir":I
    :goto_1
    iget-object v5, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mLazySpanLookup:Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup;

    add-int/lit8 v6, v2, 0x1

    .line 294
    invoke-virtual {v5, v0, v6, v4, v3}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup;->getFirstFullSpanItemInRange(IIIZ)Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup$FullSpanItem;

    move-result-object v5

    .line 295
    .local v5, "invalidFsi":Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup$FullSpanItem;
    if-nez v5, :cond_5

    .line 296
    iput-boolean v1, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mLaidOutInvalidFullSpan:Z

    .line 297
    iget-object v3, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mLazySpanLookup:Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup;

    add-int/lit8 v6, v2, 0x1

    invoke-virtual {v3, v6}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup;->forceInvalidateAfter(I)I

    .line 298
    return v1

    .line 300
    :cond_5
    iget-object v1, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mLazySpanLookup:Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup;

    iget v6, v5, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup$FullSpanItem;->mPosition:I

    mul-int/lit8 v7, v4, -0x1

    .line 301
    invoke-virtual {v1, v0, v6, v7, v3}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup;->getFirstFullSpanItemInRange(IIIZ)Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup$FullSpanItem;

    move-result-object v1

    .line 303
    .local v1, "validFsi":Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup$FullSpanItem;
    if-nez v1, :cond_6

    .line 304
    iget-object v6, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mLazySpanLookup:Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup;

    iget v7, v5, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup$FullSpanItem;->mPosition:I

    invoke-virtual {v6, v7}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup;->forceInvalidateAfter(I)I

    goto :goto_2

    .line 306
    :cond_6
    iget-object v6, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mLazySpanLookup:Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup;

    iget v7, v1, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup$FullSpanItem;->mPosition:I

    add-int/2addr v7, v3

    invoke-virtual {v6, v7}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup;->forceInvalidateAfter(I)I

    .line 308
    :goto_2
    invoke-virtual {p0}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->requestSimpleAnimationsInNextLayout()V

    .line 309
    invoke-virtual {p0}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->requestLayout()V

    .line 310
    return v3

    .line 270
    .end local v0    # "minPos":I
    .end local v1    # "validFsi":Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup$FullSpanItem;
    .end local v2    # "maxPos":I
    .end local v4    # "invalidGapDir":I
    .end local v5    # "invalidFsi":Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup$FullSpanItem;
    :cond_7
    :goto_3
    return v1
.end method

.method public checkLayoutParams(Landroidx/recyclerview/widget/RecyclerView$LayoutParams;)Z
    .locals 1
    .param p1, "lp"    # Landroidx/recyclerview/widget/RecyclerView$LayoutParams;

    .line 2247
    instance-of v0, p1, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LayoutParams;

    return v0
.end method

.method public collectAdjacentPrefetchPositions(IILandroidx/recyclerview/widget/RecyclerView$State;Landroidx/recyclerview/widget/RecyclerView$LayoutManager$LayoutPrefetchRegistry;)V
    .locals 6
    .param p1, "dx"    # I
    .param p2, "dy"    # I
    .param p3, "state"    # Landroidx/recyclerview/widget/RecyclerView$State;
    .param p4, "layoutPrefetchRegistry"    # Landroidx/recyclerview/widget/RecyclerView$LayoutManager$LayoutPrefetchRegistry;

    .line 2097
    iget v0, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mOrientation:I

    if-nez v0, :cond_0

    move v0, p1

    goto :goto_0

    :cond_0
    move v0, p2

    .line 2098
    .local v0, "delta":I
    :goto_0
    invoke-virtual {p0}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->getChildCount()I

    move-result v1

    if-eqz v1, :cond_8

    if-nez v0, :cond_1

    goto/16 :goto_4

    .line 2102
    :cond_1
    invoke-virtual {p0, v0, p3}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->prepareLayoutStateForDelta(ILandroidx/recyclerview/widget/RecyclerView$State;)V

    .line 2105
    iget-object v1, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mPrefetchDistances:[I

    if-eqz v1, :cond_2

    array-length v1, v1

    iget v2, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mSpanCount:I

    if-ge v1, v2, :cond_3

    .line 2106
    :cond_2
    iget v1, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mSpanCount:I

    new-array v1, v1, [I

    iput-object v1, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mPrefetchDistances:[I

    .line 2109
    :cond_3
    const/4 v1, 0x0

    .line 2110
    .local v1, "itemPrefetchCount":I
    const/4 v2, 0x0

    .local v2, "i":I
    :goto_1
    iget v3, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mSpanCount:I

    if-ge v2, v3, :cond_6

    .line 2112
    iget-object v3, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mLayoutState:Landroidx/recyclerview/widget/LayoutState;

    iget v3, v3, Landroidx/recyclerview/widget/LayoutState;->mItemDirection:I

    const/4 v4, -0x1

    if-ne v3, v4, :cond_4

    .line 2113
    iget-object v3, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mLayoutState:Landroidx/recyclerview/widget/LayoutState;

    iget v3, v3, Landroidx/recyclerview/widget/LayoutState;->mStartLine:I

    iget-object v4, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mSpans:[Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;

    aget-object v4, v4, v2

    iget-object v5, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mLayoutState:Landroidx/recyclerview/widget/LayoutState;

    iget v5, v5, Landroidx/recyclerview/widget/LayoutState;->mStartLine:I

    invoke-virtual {v4, v5}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;->getStartLine(I)I

    move-result v4

    sub-int/2addr v3, v4

    goto :goto_2

    .line 2114
    :cond_4
    iget-object v3, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mSpans:[Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;

    aget-object v3, v3, v2

    iget-object v4, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mLayoutState:Landroidx/recyclerview/widget/LayoutState;

    iget v4, v4, Landroidx/recyclerview/widget/LayoutState;->mEndLine:I

    invoke-virtual {v3, v4}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;->getEndLine(I)I

    move-result v3

    iget-object v4, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mLayoutState:Landroidx/recyclerview/widget/LayoutState;

    iget v4, v4, Landroidx/recyclerview/widget/LayoutState;->mEndLine:I

    sub-int/2addr v3, v4

    :goto_2
    nop

    .line 2115
    .local v3, "distance":I
    if-ltz v3, :cond_5

    .line 2117
    iget-object v4, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mPrefetchDistances:[I

    aput v3, v4, v1

    .line 2118
    add-int/lit8 v1, v1, 0x1

    .line 2110
    .end local v3    # "distance":I
    :cond_5
    add-int/lit8 v2, v2, 0x1

    goto :goto_1

    .line 2121
    .end local v2    # "i":I
    :cond_6
    iget-object v2, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mPrefetchDistances:[I

    const/4 v3, 0x0

    invoke-static {v2, v3, v1}, Ljava/util/Arrays;->sort([III)V

    .line 2124
    const/4 v2, 0x0

    .restart local v2    # "i":I
    :goto_3
    if-ge v2, v1, :cond_7

    iget-object v3, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mLayoutState:Landroidx/recyclerview/widget/LayoutState;

    invoke-virtual {v3, p3}, Landroidx/recyclerview/widget/LayoutState;->hasMore(Landroidx/recyclerview/widget/RecyclerView$State;)Z

    move-result v3

    if-eqz v3, :cond_7

    .line 2125
    iget-object v3, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mLayoutState:Landroidx/recyclerview/widget/LayoutState;

    iget v3, v3, Landroidx/recyclerview/widget/LayoutState;->mCurrentPosition:I

    iget-object v4, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mPrefetchDistances:[I

    aget v4, v4, v2

    invoke-interface {p4, v3, v4}, Landroidx/recyclerview/widget/RecyclerView$LayoutManager$LayoutPrefetchRegistry;->addPosition(II)V

    .line 2127
    iget-object v3, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mLayoutState:Landroidx/recyclerview/widget/LayoutState;

    iget v4, v3, Landroidx/recyclerview/widget/LayoutState;->mCurrentPosition:I

    iget-object v5, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mLayoutState:Landroidx/recyclerview/widget/LayoutState;

    iget v5, v5, Landroidx/recyclerview/widget/LayoutState;->mItemDirection:I

    add-int/2addr v4, v5

    iput v4, v3, Landroidx/recyclerview/widget/LayoutState;->mCurrentPosition:I

    .line 2124
    add-int/lit8 v2, v2, 0x1

    goto :goto_3

    .line 2129
    .end local v2    # "i":I
    :cond_7
    return-void

    .line 2100
    .end local v1    # "itemPrefetchCount":I
    :cond_8
    :goto_4
    return-void
.end method

.method public computeHorizontalScrollExtent(Landroidx/recyclerview/widget/RecyclerView$State;)I
    .locals 1
    .param p1, "state"    # Landroidx/recyclerview/widget/RecyclerView$State;

    .line 1096
    invoke-direct {p0, p1}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->computeScrollExtent(Landroidx/recyclerview/widget/RecyclerView$State;)I

    move-result v0

    return v0
.end method

.method public computeHorizontalScrollOffset(Landroidx/recyclerview/widget/RecyclerView$State;)I
    .locals 1
    .param p1, "state"    # Landroidx/recyclerview/widget/RecyclerView$State;

    .line 1076
    invoke-direct {p0, p1}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->computeScrollOffset(Landroidx/recyclerview/widget/RecyclerView$State;)I

    move-result v0

    return v0
.end method

.method public computeHorizontalScrollRange(Landroidx/recyclerview/widget/RecyclerView$State;)I
    .locals 1
    .param p1, "state"    # Landroidx/recyclerview/widget/RecyclerView$State;

    .line 1116
    invoke-direct {p0, p1}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->computeScrollRange(Landroidx/recyclerview/widget/RecyclerView$State;)I

    move-result v0

    return v0
.end method

.method public computeScrollVectorForPosition(I)Landroid/graphics/PointF;
    .locals 4
    .param p1, "targetPosition"    # I

    .line 2026
    invoke-direct {p0, p1}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->calculateScrollDirectionForPosition(I)I

    move-result v0

    .line 2027
    .local v0, "direction":I
    new-instance v1, Landroid/graphics/PointF;

    invoke-direct {v1}, Landroid/graphics/PointF;-><init>()V

    .line 2028
    .local v1, "outVector":Landroid/graphics/PointF;
    if-nez v0, :cond_0

    .line 2029
    const/4 v2, 0x0

    return-object v2

    .line 2031
    :cond_0
    iget v2, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mOrientation:I

    const/4 v3, 0x0

    if-nez v2, :cond_1

    .line 2032
    int-to-float v2, v0

    iput v2, v1, Landroid/graphics/PointF;->x:F

    .line 2033
    iput v3, v1, Landroid/graphics/PointF;->y:F

    goto :goto_0

    .line 2035
    :cond_1
    iput v3, v1, Landroid/graphics/PointF;->x:F

    .line 2036
    int-to-float v2, v0

    iput v2, v1, Landroid/graphics/PointF;->y:F

    .line 2038
    :goto_0
    return-object v1
.end method

.method public computeVerticalScrollExtent(Landroidx/recyclerview/widget/RecyclerView$State;)I
    .locals 1
    .param p1, "state"    # Landroidx/recyclerview/widget/RecyclerView$State;

    .line 1111
    invoke-direct {p0, p1}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->computeScrollExtent(Landroidx/recyclerview/widget/RecyclerView$State;)I

    move-result v0

    return v0
.end method

.method public computeVerticalScrollOffset(Landroidx/recyclerview/widget/RecyclerView$State;)I
    .locals 1
    .param p1, "state"    # Landroidx/recyclerview/widget/RecyclerView$State;

    .line 1091
    invoke-direct {p0, p1}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->computeScrollOffset(Landroidx/recyclerview/widget/RecyclerView$State;)I

    move-result v0

    return v0
.end method

.method public computeVerticalScrollRange(Landroidx/recyclerview/widget/RecyclerView$State;)I
    .locals 1
    .param p1, "state"    # Landroidx/recyclerview/widget/RecyclerView$State;

    .line 1131
    invoke-direct {p0, p1}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->computeScrollRange(Landroidx/recyclerview/widget/RecyclerView$State;)I

    move-result v0

    return v0
.end method

.method public findFirstCompletelyVisibleItemPositions([I)[I
    .locals 3
    .param p1, "into"    # [I

    .line 998
    if-nez p1, :cond_0

    .line 999
    iget v0, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mSpanCount:I

    new-array p1, v0, [I

    goto :goto_0

    .line 1000
    :cond_0
    array-length v0, p1

    iget v1, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mSpanCount:I

    if-lt v0, v1, :cond_2

    .line 1004
    :goto_0
    const/4 v0, 0x0

    .local v0, "i":I
    :goto_1
    iget v1, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mSpanCount:I

    if-ge v0, v1, :cond_1

    .line 1005
    iget-object v1, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mSpans:[Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;

    aget-object v1, v1, v0

    invoke-virtual {v1}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;->findFirstCompletelyVisibleItemPosition()I

    move-result v1

    aput v1, p1, v0

    .line 1004
    add-int/lit8 v0, v0, 0x1

    goto :goto_1

    .line 1007
    .end local v0    # "i":I
    :cond_1
    return-object p1

    .line 1001
    :cond_2
    new-instance v0, Ljava/lang/IllegalArgumentException;

    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "Provided int[]\'s size must be more than or equal to span count. Expected:"

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v1

    iget v2, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mSpanCount:I

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    move-result-object v1

    const-string v2, ", array size:"

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v1

    array-length v2, p1

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    move-result-object v1

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-direct {v0, v1}, Ljava/lang/IllegalArgumentException;-><init>(Ljava/lang/String;)V

    throw v0
.end method

.method findFirstVisibleItemClosestToEnd(Z)Landroid/view/View;
    .locals 7
    .param p1, "fullyVisible"    # Z

    .line 1361
    iget-object v0, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mPrimaryOrientation:Landroidx/recyclerview/widget/OrientationHelper;

    invoke-virtual {v0}, Landroidx/recyclerview/widget/OrientationHelper;->getStartAfterPadding()I

    move-result v0

    .line 1362
    .local v0, "boundsStart":I
    iget-object v1, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mPrimaryOrientation:Landroidx/recyclerview/widget/OrientationHelper;

    invoke-virtual {v1}, Landroidx/recyclerview/widget/OrientationHelper;->getEndAfterPadding()I

    move-result v1

    .line 1363
    .local v1, "boundsEnd":I
    const/4 v2, 0x0

    .line 1364
    .local v2, "partiallyVisible":Landroid/view/View;
    invoke-virtual {p0}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->getChildCount()I

    move-result v3

    add-int/lit8 v3, v3, -0x1

    .local v3, "i":I
    :goto_0
    if-ltz v3, :cond_4

    .line 1365
    invoke-virtual {p0, v3}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->getChildAt(I)Landroid/view/View;

    move-result-object v4

    .line 1366
    .local v4, "child":Landroid/view/View;
    iget-object v5, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mPrimaryOrientation:Landroidx/recyclerview/widget/OrientationHelper;

    invoke-virtual {v5, v4}, Landroidx/recyclerview/widget/OrientationHelper;->getDecoratedStart(Landroid/view/View;)I

    move-result v5

    .line 1367
    .local v5, "childStart":I
    iget-object v6, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mPrimaryOrientation:Landroidx/recyclerview/widget/OrientationHelper;

    invoke-virtual {v6, v4}, Landroidx/recyclerview/widget/OrientationHelper;->getDecoratedEnd(Landroid/view/View;)I

    move-result v6

    .line 1368
    .local v6, "childEnd":I
    if-le v6, v0, :cond_3

    if-lt v5, v1, :cond_0

    .line 1369
    goto :goto_2

    .line 1371
    :cond_0
    if-le v6, v1, :cond_2

    if-nez p1, :cond_1

    goto :goto_1

    .line 1376
    :cond_1
    if-nez v2, :cond_3

    .line 1377
    move-object v2, v4

    goto :goto_2

    .line 1374
    :cond_2
    :goto_1
    return-object v4

    .line 1364
    .end local v4    # "child":Landroid/view/View;
    .end local v5    # "childStart":I
    .end local v6    # "childEnd":I
    :cond_3
    :goto_2
    add-int/lit8 v3, v3, -0x1

    goto :goto_0

    .line 1380
    .end local v3    # "i":I
    :cond_4
    return-object v2
.end method

.method findFirstVisibleItemClosestToStart(Z)Landroid/view/View;
    .locals 8
    .param p1, "fullyVisible"    # Z

    .line 1331
    iget-object v0, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mPrimaryOrientation:Landroidx/recyclerview/widget/OrientationHelper;

    invoke-virtual {v0}, Landroidx/recyclerview/widget/OrientationHelper;->getStartAfterPadding()I

    move-result v0

    .line 1332
    .local v0, "boundsStart":I
    iget-object v1, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mPrimaryOrientation:Landroidx/recyclerview/widget/OrientationHelper;

    invoke-virtual {v1}, Landroidx/recyclerview/widget/OrientationHelper;->getEndAfterPadding()I

    move-result v1

    .line 1333
    .local v1, "boundsEnd":I
    invoke-virtual {p0}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->getChildCount()I

    move-result v2

    .line 1334
    .local v2, "limit":I
    const/4 v3, 0x0

    .line 1335
    .local v3, "partiallyVisible":Landroid/view/View;
    const/4 v4, 0x0

    .local v4, "i":I
    :goto_0
    if-ge v4, v2, :cond_4

    .line 1336
    invoke-virtual {p0, v4}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->getChildAt(I)Landroid/view/View;

    move-result-object v5

    .line 1337
    .local v5, "child":Landroid/view/View;
    iget-object v6, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mPrimaryOrientation:Landroidx/recyclerview/widget/OrientationHelper;

    invoke-virtual {v6, v5}, Landroidx/recyclerview/widget/OrientationHelper;->getDecoratedStart(Landroid/view/View;)I

    move-result v6

    .line 1338
    .local v6, "childStart":I
    iget-object v7, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mPrimaryOrientation:Landroidx/recyclerview/widget/OrientationHelper;

    invoke-virtual {v7, v5}, Landroidx/recyclerview/widget/OrientationHelper;->getDecoratedEnd(Landroid/view/View;)I

    move-result v7

    .line 1339
    .local v7, "childEnd":I
    if-le v7, v0, :cond_3

    if-lt v6, v1, :cond_0

    .line 1340
    goto :goto_2

    .line 1342
    :cond_0
    if-ge v6, v0, :cond_2

    if-nez p1, :cond_1

    goto :goto_1

    .line 1347
    :cond_1
    if-nez v3, :cond_3

    .line 1348
    move-object v3, v5

    goto :goto_2

    .line 1345
    :cond_2
    :goto_1
    return-object v5

    .line 1335
    .end local v5    # "child":Landroid/view/View;
    .end local v6    # "childStart":I
    .end local v7    # "childEnd":I
    :cond_3
    :goto_2
    add-int/lit8 v4, v4, 0x1

    goto :goto_0

    .line 1351
    .end local v4    # "i":I
    :cond_4
    return-object v3
.end method

.method findFirstVisibleItemPositionInt()I
    .locals 2

    .line 1319
    iget-boolean v0, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mShouldReverseLayout:Z

    const/4 v1, 0x1

    if-eqz v0, :cond_0

    invoke-virtual {p0, v1}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->findFirstVisibleItemClosestToEnd(Z)Landroid/view/View;

    move-result-object v0

    goto :goto_0

    .line 1320
    :cond_0
    invoke-virtual {p0, v1}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->findFirstVisibleItemClosestToStart(Z)Landroid/view/View;

    move-result-object v0

    :goto_0
    nop

    .line 1321
    .local v0, "first":Landroid/view/View;
    if-nez v0, :cond_1

    const/4 v1, -0x1

    goto :goto_1

    :cond_1
    invoke-virtual {p0, v0}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->getPosition(Landroid/view/View;)I

    move-result v1

    :goto_1
    return v1
.end method

.method public findFirstVisibleItemPositions([I)[I
    .locals 3
    .param p1, "into"    # [I

    .line 966
    if-nez p1, :cond_0

    .line 967
    iget v0, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mSpanCount:I

    new-array p1, v0, [I

    goto :goto_0

    .line 968
    :cond_0
    array-length v0, p1

    iget v1, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mSpanCount:I

    if-lt v0, v1, :cond_2

    .line 972
    :goto_0
    const/4 v0, 0x0

    .local v0, "i":I
    :goto_1
    iget v1, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mSpanCount:I

    if-ge v0, v1, :cond_1

    .line 973
    iget-object v1, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mSpans:[Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;

    aget-object v1, v1, v0

    invoke-virtual {v1}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;->findFirstVisibleItemPosition()I

    move-result v1

    aput v1, p1, v0

    .line 972
    add-int/lit8 v0, v0, 0x1

    goto :goto_1

    .line 975
    .end local v0    # "i":I
    :cond_1
    return-object p1

    .line 969
    :cond_2
    new-instance v0, Ljava/lang/IllegalArgumentException;

    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "Provided int[]\'s size must be more than or equal to span count. Expected:"

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v1

    iget v2, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mSpanCount:I

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    move-result-object v1

    const-string v2, ", array size:"

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v1

    array-length v2, p1

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    move-result-object v1

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-direct {v0, v1}, Ljava/lang/IllegalArgumentException;-><init>(Ljava/lang/String;)V

    throw v0
.end method

.method public findLastCompletelyVisibleItemPositions([I)[I
    .locals 3
    .param p1, "into"    # [I

    .line 1062
    if-nez p1, :cond_0

    .line 1063
    iget v0, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mSpanCount:I

    new-array p1, v0, [I

    goto :goto_0

    .line 1064
    :cond_0
    array-length v0, p1

    iget v1, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mSpanCount:I

    if-lt v0, v1, :cond_2

    .line 1068
    :goto_0
    const/4 v0, 0x0

    .local v0, "i":I
    :goto_1
    iget v1, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mSpanCount:I

    if-ge v0, v1, :cond_1

    .line 1069
    iget-object v1, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mSpans:[Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;

    aget-object v1, v1, v0

    invoke-virtual {v1}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;->findLastCompletelyVisibleItemPosition()I

    move-result v1

    aput v1, p1, v0

    .line 1068
    add-int/lit8 v0, v0, 0x1

    goto :goto_1

    .line 1071
    .end local v0    # "i":I
    :cond_1
    return-object p1

    .line 1065
    :cond_2
    new-instance v0, Ljava/lang/IllegalArgumentException;

    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "Provided int[]\'s size must be more than or equal to span count. Expected:"

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v1

    iget v2, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mSpanCount:I

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    move-result-object v1

    const-string v2, ", array size:"

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v1

    array-length v2, p1

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    move-result-object v1

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-direct {v0, v1}, Ljava/lang/IllegalArgumentException;-><init>(Ljava/lang/String;)V

    throw v0
.end method

.method public findLastVisibleItemPositions([I)[I
    .locals 3
    .param p1, "into"    # [I

    .line 1030
    if-nez p1, :cond_0

    .line 1031
    iget v0, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mSpanCount:I

    new-array p1, v0, [I

    goto :goto_0

    .line 1032
    :cond_0
    array-length v0, p1

    iget v1, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mSpanCount:I

    if-lt v0, v1, :cond_2

    .line 1036
    :goto_0
    const/4 v0, 0x0

    .local v0, "i":I
    :goto_1
    iget v1, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mSpanCount:I

    if-ge v0, v1, :cond_1

    .line 1037
    iget-object v1, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mSpans:[Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;

    aget-object v1, v1, v0

    invoke-virtual {v1}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;->findLastVisibleItemPosition()I

    move-result v1

    aput v1, p1, v0

    .line 1036
    add-int/lit8 v0, v0, 0x1

    goto :goto_1

    .line 1039
    .end local v0    # "i":I
    :cond_1
    return-object p1

    .line 1033
    :cond_2
    new-instance v0, Ljava/lang/IllegalArgumentException;

    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "Provided int[]\'s size must be more than or equal to span count. Expected:"

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v1

    iget v2, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mSpanCount:I

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    move-result-object v1

    const-string v2, ", array size:"

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v1

    array-length v2, p1

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    move-result-object v1

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-direct {v0, v1}, Ljava/lang/IllegalArgumentException;-><init>(Ljava/lang/String;)V

    throw v0
.end method

.method public generateDefaultLayoutParams()Landroidx/recyclerview/widget/RecyclerView$LayoutParams;
    .locals 3

    .line 2222
    iget v0, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mOrientation:I

    const/4 v1, -0x2

    const/4 v2, -0x1

    if-nez v0, :cond_0

    .line 2223
    new-instance v0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LayoutParams;

    invoke-direct {v0, v1, v2}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LayoutParams;-><init>(II)V

    return-object v0

    .line 2226
    :cond_0
    new-instance v0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LayoutParams;

    invoke-direct {v0, v2, v1}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LayoutParams;-><init>(II)V

    return-object v0
.end method

.method public generateLayoutParams(Landroid/content/Context;Landroid/util/AttributeSet;)Landroidx/recyclerview/widget/RecyclerView$LayoutParams;
    .locals 1
    .param p1, "c"    # Landroid/content/Context;
    .param p2, "attrs"    # Landroid/util/AttributeSet;

    .line 2233
    new-instance v0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LayoutParams;

    invoke-direct {v0, p1, p2}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LayoutParams;-><init>(Landroid/content/Context;Landroid/util/AttributeSet;)V

    return-object v0
.end method

.method public generateLayoutParams(Landroid/view/ViewGroup$LayoutParams;)Landroidx/recyclerview/widget/RecyclerView$LayoutParams;
    .locals 2
    .param p1, "lp"    # Landroid/view/ViewGroup$LayoutParams;

    .line 2238
    instance-of v0, p1, Landroid/view/ViewGroup$MarginLayoutParams;

    if-eqz v0, :cond_0

    .line 2239
    new-instance v0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LayoutParams;

    move-object v1, p1

    check-cast v1, Landroid/view/ViewGroup$MarginLayoutParams;

    invoke-direct {v0, v1}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LayoutParams;-><init>(Landroid/view/ViewGroup$MarginLayoutParams;)V

    return-object v0

    .line 2241
    :cond_0
    new-instance v0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LayoutParams;

    invoke-direct {v0, p1}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LayoutParams;-><init>(Landroid/view/ViewGroup$LayoutParams;)V

    return-object v0
.end method

.method getFirstChildPosition()I
    .locals 2

    .line 2182
    invoke-virtual {p0}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->getChildCount()I

    move-result v0

    .line 2183
    .local v0, "childCount":I
    const/4 v1, 0x0

    if-nez v0, :cond_0

    goto :goto_0

    :cond_0
    invoke-virtual {p0, v1}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->getChildAt(I)Landroid/view/View;

    move-result-object v1

    invoke-virtual {p0, v1}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->getPosition(Landroid/view/View;)I

    move-result v1

    :goto_0
    return v1
.end method

.method public getGapStrategy()I
    .locals 1

    .line 499
    iget v0, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mGapStrategy:I

    return v0
.end method

.method getLastChildPosition()I
    .locals 2

    .line 2177
    invoke-virtual {p0}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->getChildCount()I

    move-result v0

    .line 2178
    .local v0, "childCount":I
    if-nez v0, :cond_0

    const/4 v1, 0x0

    goto :goto_0

    :cond_0
    add-int/lit8 v1, v0, -0x1

    invoke-virtual {p0, v1}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->getChildAt(I)Landroid/view/View;

    move-result-object v1

    invoke-virtual {p0, v1}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->getPosition(Landroid/view/View;)I

    move-result v1

    :goto_0
    return v1
.end method

.method public getOrientation()I
    .locals 1

    .line 2251
    iget v0, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mOrientation:I

    return v0
.end method

.method public getReverseLayout()Z
    .locals 1

    .line 579
    iget-boolean v0, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mReverseLayout:Z

    return v0
.end method

.method public getSpanCount()I
    .locals 1

    .line 538
    iget v0, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mSpanCount:I

    return v0
.end method

.method hasGapsToFix()Landroid/view/View;
    .locals 16

    .line 338
    move-object/from16 v0, p0

    const/4 v1, 0x0

    .line 339
    .local v1, "startChildIndex":I
    invoke-virtual/range {p0 .. p0}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->getChildCount()I

    move-result v2

    const/4 v3, 0x1

    sub-int/2addr v2, v3

    .line 340
    .local v2, "endChildIndex":I
    new-instance v4, Ljava/util/BitSet;

    iget v5, v0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mSpanCount:I

    invoke-direct {v4, v5}, Ljava/util/BitSet;-><init>(I)V

    .line 341
    .local v4, "mSpansToCheck":Ljava/util/BitSet;
    iget v5, v0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mSpanCount:I

    const/4 v6, 0x0

    invoke-virtual {v4, v6, v5, v3}, Ljava/util/BitSet;->set(IIZ)V

    .line 344
    iget v5, v0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mOrientation:I

    const/4 v7, -0x1

    if-ne v5, v3, :cond_0

    invoke-virtual/range {p0 .. p0}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->isLayoutRTL()Z

    move-result v5

    if-eqz v5, :cond_0

    const/4 v5, 0x1

    goto :goto_0

    :cond_0
    const/4 v5, -0x1

    .line 346
    .local v5, "preferredSpanDir":I
    :goto_0
    iget-boolean v8, v0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mShouldReverseLayout:Z

    if-eqz v8, :cond_1

    .line 347
    move v8, v2

    .line 348
    .local v8, "firstChildIndex":I
    add-int/lit8 v9, v1, -0x1

    .local v9, "childLimit":I
    goto :goto_1

    .line 350
    .end local v8    # "firstChildIndex":I
    .end local v9    # "childLimit":I
    :cond_1
    move v8, v1

    .line 351
    .restart local v8    # "firstChildIndex":I
    add-int/lit8 v9, v2, 0x1

    .line 353
    .restart local v9    # "childLimit":I
    :goto_1
    if-ge v8, v9, :cond_2

    const/4 v7, 0x1

    .line 354
    .local v7, "nextChildDiff":I
    :cond_2
    move v10, v8

    .local v10, "i":I
    :goto_2
    if-eq v10, v9, :cond_e

    .line 355
    invoke-virtual {v0, v10}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->getChildAt(I)Landroid/view/View;

    move-result-object v11

    .line 356
    .local v11, "child":Landroid/view/View;
    invoke-virtual {v11}, Landroid/view/View;->getLayoutParams()Landroid/view/ViewGroup$LayoutParams;

    move-result-object v12

    check-cast v12, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LayoutParams;

    .line 357
    .local v12, "lp":Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LayoutParams;
    iget-object v13, v12, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LayoutParams;->mSpan:Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;

    iget v13, v13, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;->mIndex:I

    invoke-virtual {v4, v13}, Ljava/util/BitSet;->get(I)Z

    move-result v13

    if-eqz v13, :cond_4

    .line 358
    iget-object v13, v12, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LayoutParams;->mSpan:Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;

    invoke-direct {v0, v13}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->checkSpanForGap(Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;)Z

    move-result v13

    if-eqz v13, :cond_3

    .line 359
    return-object v11

    .line 361
    :cond_3
    iget-object v13, v12, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LayoutParams;->mSpan:Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;

    iget v13, v13, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;->mIndex:I

    invoke-virtual {v4, v13}, Ljava/util/BitSet;->clear(I)V

    .line 363
    :cond_4
    iget-boolean v13, v12, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LayoutParams;->mFullSpan:Z

    if-eqz v13, :cond_5

    .line 364
    goto :goto_6

    .line 367
    :cond_5
    add-int v13, v10, v7

    if-eq v13, v9, :cond_d

    .line 368
    add-int v13, v10, v7

    invoke-virtual {v0, v13}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->getChildAt(I)Landroid/view/View;

    move-result-object v13

    .line 369
    .local v13, "nextChild":Landroid/view/View;
    const/4 v14, 0x0

    .line 370
    .local v14, "compareSpans":Z
    iget-boolean v15, v0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mShouldReverseLayout:Z

    if-eqz v15, :cond_8

    .line 372
    iget-object v15, v0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mPrimaryOrientation:Landroidx/recyclerview/widget/OrientationHelper;

    invoke-virtual {v15, v11}, Landroidx/recyclerview/widget/OrientationHelper;->getDecoratedEnd(Landroid/view/View;)I

    move-result v15

    .line 373
    .local v15, "myEnd":I
    iget-object v3, v0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mPrimaryOrientation:Landroidx/recyclerview/widget/OrientationHelper;

    invoke-virtual {v3, v13}, Landroidx/recyclerview/widget/OrientationHelper;->getDecoratedEnd(Landroid/view/View;)I

    move-result v3

    .line 374
    .local v3, "nextEnd":I
    if-ge v15, v3, :cond_6

    .line 375
    return-object v11

    .line 376
    :cond_6
    if-ne v15, v3, :cond_7

    .line 377
    const/4 v14, 0x1

    .line 379
    .end local v3    # "nextEnd":I
    .end local v15    # "myEnd":I
    :cond_7
    goto :goto_3

    .line 380
    :cond_8
    iget-object v3, v0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mPrimaryOrientation:Landroidx/recyclerview/widget/OrientationHelper;

    invoke-virtual {v3, v11}, Landroidx/recyclerview/widget/OrientationHelper;->getDecoratedStart(Landroid/view/View;)I

    move-result v3

    .line 381
    .local v3, "myStart":I
    iget-object v15, v0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mPrimaryOrientation:Landroidx/recyclerview/widget/OrientationHelper;

    invoke-virtual {v15, v13}, Landroidx/recyclerview/widget/OrientationHelper;->getDecoratedStart(Landroid/view/View;)I

    move-result v15

    .line 382
    .local v15, "nextStart":I
    if-le v3, v15, :cond_9

    .line 383
    return-object v11

    .line 384
    :cond_9
    if-ne v3, v15, :cond_a

    .line 385
    const/4 v14, 0x1

    .line 388
    .end local v3    # "myStart":I
    .end local v15    # "nextStart":I
    :cond_a
    :goto_3
    if-eqz v14, :cond_d

    .line 390
    invoke-virtual {v13}, Landroid/view/View;->getLayoutParams()Landroid/view/ViewGroup$LayoutParams;

    move-result-object v3

    check-cast v3, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LayoutParams;

    .line 391
    .local v3, "nextLp":Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LayoutParams;
    iget-object v15, v12, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LayoutParams;->mSpan:Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;

    iget v15, v15, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;->mIndex:I

    iget-object v6, v3, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LayoutParams;->mSpan:Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;

    iget v6, v6, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;->mIndex:I

    sub-int/2addr v15, v6

    if-gez v15, :cond_b

    const/4 v6, 0x1

    goto :goto_4

    :cond_b
    const/4 v6, 0x0

    :goto_4
    if-gez v5, :cond_c

    const/4 v15, 0x1

    goto :goto_5

    :cond_c
    const/4 v15, 0x0

    :goto_5
    if-eq v6, v15, :cond_d

    .line 392
    return-object v11

    .line 354
    .end local v3    # "nextLp":Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LayoutParams;
    .end local v11    # "child":Landroid/view/View;
    .end local v12    # "lp":Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LayoutParams;
    .end local v13    # "nextChild":Landroid/view/View;
    .end local v14    # "compareSpans":Z
    :cond_d
    :goto_6
    add-int/2addr v10, v7

    const/4 v3, 0x1

    const/4 v6, 0x0

    goto/16 :goto_2

    .line 398
    .end local v10    # "i":I
    :cond_e
    const/4 v3, 0x0

    return-object v3
.end method

.method public invalidateSpanAssignments()V
    .locals 1

    .line 548
    iget-object v0, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mLazySpanLookup:Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup;

    invoke-virtual {v0}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup;->clear()V

    .line 549
    invoke-virtual {p0}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->requestLayout()V

    .line 550
    return-void
.end method

.method public isAutoMeasureEnabled()Z
    .locals 1

    .line 253
    iget v0, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mGapStrategy:I

    if-eqz v0, :cond_0

    const/4 v0, 0x1

    goto :goto_0

    :cond_0
    const/4 v0, 0x0

    :goto_0
    return v0
.end method

.method isLayoutRTL()Z
    .locals 2

    .line 567
    invoke-virtual {p0}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->getLayoutDirection()I

    move-result v0

    const/4 v1, 0x1

    if-ne v0, v1, :cond_0

    goto :goto_0

    :cond_0
    const/4 v1, 0x0

    :goto_0
    return v1
.end method

.method public offsetChildrenHorizontal(I)V
    .locals 2
    .param p1, "dx"    # I

    .line 1460
    invoke-super {p0, p1}, Landroidx/recyclerview/widget/RecyclerView$LayoutManager;->offsetChildrenHorizontal(I)V

    .line 1461
    const/4 v0, 0x0

    .local v0, "i":I
    :goto_0
    iget v1, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mSpanCount:I

    if-ge v0, v1, :cond_0

    .line 1462
    iget-object v1, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mSpans:[Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;

    aget-object v1, v1, v0

    invoke-virtual {v1, p1}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;->onOffset(I)V

    .line 1461
    add-int/lit8 v0, v0, 0x1

    goto :goto_0

    .line 1464
    .end local v0    # "i":I
    :cond_0
    return-void
.end method

.method public offsetChildrenVertical(I)V
    .locals 2
    .param p1, "dy"    # I

    .line 1468
    invoke-super {p0, p1}, Landroidx/recyclerview/widget/RecyclerView$LayoutManager;->offsetChildrenVertical(I)V

    .line 1469
    const/4 v0, 0x0

    .local v0, "i":I
    :goto_0
    iget v1, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mSpanCount:I

    if-ge v0, v1, :cond_0

    .line 1470
    iget-object v1, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mSpans:[Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;

    aget-object v1, v1, v0

    invoke-virtual {v1, p1}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;->onOffset(I)V

    .line 1469
    add-int/lit8 v0, v0, 0x1

    goto :goto_0

    .line 1472
    .end local v0    # "i":I
    :cond_0
    return-void
.end method

.method public onAdapterChanged(Landroidx/recyclerview/widget/RecyclerView$Adapter;Landroidx/recyclerview/widget/RecyclerView$Adapter;)V
    .locals 2
    .param p1, "oldAdapter"    # Landroidx/recyclerview/widget/RecyclerView$Adapter;
    .param p2, "newAdapter"    # Landroidx/recyclerview/widget/RecyclerView$Adapter;

    .line 611
    iget-object v0, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mLazySpanLookup:Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup;

    invoke-virtual {v0}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup;->clear()V

    .line 612
    const/4 v0, 0x0

    .local v0, "i":I
    :goto_0
    iget v1, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mSpanCount:I

    if-ge v0, v1, :cond_0

    .line 613
    iget-object v1, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mSpans:[Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;

    aget-object v1, v1, v0

    invoke-virtual {v1}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;->clear()V

    .line 612
    add-int/lit8 v0, v0, 0x1

    goto :goto_0

    .line 615
    .end local v0    # "i":I
    :cond_0
    return-void
.end method

.method public onDetachedFromWindow(Landroidx/recyclerview/widget/RecyclerView;Landroidx/recyclerview/widget/RecyclerView$Recycler;)V
    .locals 2
    .param p1, "view"    # Landroidx/recyclerview/widget/RecyclerView;
    .param p2, "recycler"    # Landroidx/recyclerview/widget/RecyclerView$Recycler;

    .line 322
    invoke-super {p0, p1, p2}, Landroidx/recyclerview/widget/RecyclerView$LayoutManager;->onDetachedFromWindow(Landroidx/recyclerview/widget/RecyclerView;Landroidx/recyclerview/widget/RecyclerView$Recycler;)V

    .line 324
    iget-object v0, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mCheckForGapsRunnable:Ljava/lang/Runnable;

    invoke-virtual {p0, v0}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->removeCallbacks(Ljava/lang/Runnable;)Z

    .line 325
    const/4 v0, 0x0

    .local v0, "i":I
    :goto_0
    iget v1, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mSpanCount:I

    if-ge v0, v1, :cond_0

    .line 326
    iget-object v1, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mSpans:[Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;

    aget-object v1, v1, v0

    invoke-virtual {v1}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;->clear()V

    .line 325
    add-int/lit8 v0, v0, 0x1

    goto :goto_0

    .line 329
    .end local v0    # "i":I
    :cond_0
    invoke-virtual {p1}, Landroidx/recyclerview/widget/RecyclerView;->requestLayout()V

    .line 330
    return-void
.end method

.method public onFocusSearchFailed(Landroid/view/View;ILandroidx/recyclerview/widget/RecyclerView$Recycler;Landroidx/recyclerview/widget/RecyclerView$State;)Landroid/view/View;
    .locals 11
    .param p1, "focused"    # Landroid/view/View;
    .param p2, "direction"    # I
    .param p3, "recycler"    # Landroidx/recyclerview/widget/RecyclerView$Recycler;
    .param p4, "state"    # Landroidx/recyclerview/widget/RecyclerView$State;

    .line 2258
    invoke-virtual {p0}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->getChildCount()I

    move-result v0

    const/4 v1, 0x0

    if-nez v0, :cond_0

    .line 2259
    return-object v1

    .line 2262
    :cond_0
    invoke-virtual {p0, p1}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->findContainingItemView(Landroid/view/View;)Landroid/view/View;

    move-result-object v0

    .line 2263
    .local v0, "directChild":Landroid/view/View;
    if-nez v0, :cond_1

    .line 2264
    return-object v1

    .line 2267
    :cond_1
    invoke-direct {p0}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->resolveShouldLayoutReverse()V

    .line 2268
    invoke-direct {p0, p2}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->convertFocusDirectionToLayoutDirection(I)I

    move-result v2

    .line 2269
    .local v2, "layoutDir":I
    const/high16 v3, -0x80000000

    if-ne v2, v3, :cond_2

    .line 2270
    return-object v1

    .line 2272
    :cond_2
    invoke-virtual {v0}, Landroid/view/View;->getLayoutParams()Landroid/view/ViewGroup$LayoutParams;

    move-result-object v3

    check-cast v3, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LayoutParams;

    .line 2273
    .local v3, "prevFocusLayoutParams":Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LayoutParams;
    iget-boolean v4, v3, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LayoutParams;->mFullSpan:Z

    .line 2274
    .local v4, "prevFocusFullSpan":Z
    iget-object v5, v3, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LayoutParams;->mSpan:Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;

    .line 2276
    .local v5, "prevFocusSpan":Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;
    const/4 v6, 0x1

    if-ne v2, v6, :cond_3

    .line 2277
    invoke-virtual {p0}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->getLastChildPosition()I

    move-result v7

    .local v7, "referenceChildPosition":I
    goto :goto_0

    .line 2279
    .end local v7    # "referenceChildPosition":I
    :cond_3
    invoke-virtual {p0}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->getFirstChildPosition()I

    move-result v7

    .line 2281
    .restart local v7    # "referenceChildPosition":I
    :goto_0
    invoke-direct {p0, v7, p4}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->updateLayoutState(ILandroidx/recyclerview/widget/RecyclerView$State;)V

    .line 2282
    invoke-direct {p0, v2}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->setLayoutStateDirection(I)V

    .line 2284
    iget-object v8, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mLayoutState:Landroidx/recyclerview/widget/LayoutState;

    iget v9, v8, Landroidx/recyclerview/widget/LayoutState;->mItemDirection:I

    add-int/2addr v9, v7

    iput v9, v8, Landroidx/recyclerview/widget/LayoutState;->mCurrentPosition:I

    .line 2285
    iget-object v8, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mLayoutState:Landroidx/recyclerview/widget/LayoutState;

    const v9, 0x3eaaaaab

    iget-object v10, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mPrimaryOrientation:Landroidx/recyclerview/widget/OrientationHelper;

    invoke-virtual {v10}, Landroidx/recyclerview/widget/OrientationHelper;->getTotalSpace()I

    move-result v10

    int-to-float v10, v10

    mul-float v10, v10, v9

    float-to-int v9, v10

    iput v9, v8, Landroidx/recyclerview/widget/LayoutState;->mAvailable:I

    .line 2286
    iget-object v8, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mLayoutState:Landroidx/recyclerview/widget/LayoutState;

    iput-boolean v6, v8, Landroidx/recyclerview/widget/LayoutState;->mStopInFocusable:Z

    .line 2287
    iget-object v8, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mLayoutState:Landroidx/recyclerview/widget/LayoutState;

    const/4 v9, 0x0

    iput-boolean v9, v8, Landroidx/recyclerview/widget/LayoutState;->mRecycle:Z

    .line 2288
    iget-object v8, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mLayoutState:Landroidx/recyclerview/widget/LayoutState;

    invoke-direct {p0, p3, v8, p4}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->fill(Landroidx/recyclerview/widget/RecyclerView$Recycler;Landroidx/recyclerview/widget/LayoutState;Landroidx/recyclerview/widget/RecyclerView$State;)I

    .line 2289
    iget-boolean v8, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mShouldReverseLayout:Z

    iput-boolean v8, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mLastLayoutFromEnd:Z

    .line 2290
    if-nez v4, :cond_4

    .line 2291
    invoke-virtual {v5, v7, v2}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;->getFocusableViewAfter(II)Landroid/view/View;

    move-result-object v8

    .line 2292
    .local v8, "view":Landroid/view/View;
    if-eqz v8, :cond_4

    if-eq v8, v0, :cond_4

    .line 2293
    return-object v8

    .line 2299
    .end local v8    # "view":Landroid/view/View;
    :cond_4
    invoke-direct {p0, v2}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->preferLastSpan(I)Z

    move-result v8

    if-eqz v8, :cond_7

    .line 2300
    iget v8, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mSpanCount:I

    sub-int/2addr v8, v6

    .local v8, "i":I
    :goto_1
    if-ltz v8, :cond_6

    .line 2301
    iget-object v10, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mSpans:[Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;

    aget-object v10, v10, v8

    invoke-virtual {v10, v7, v2}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;->getFocusableViewAfter(II)Landroid/view/View;

    move-result-object v10

    .line 2302
    .local v10, "view":Landroid/view/View;
    if-eqz v10, :cond_5

    if-eq v10, v0, :cond_5

    .line 2303
    return-object v10

    .line 2300
    .end local v10    # "view":Landroid/view/View;
    :cond_5
    add-int/lit8 v8, v8, -0x1

    goto :goto_1

    .end local v8    # "i":I
    :cond_6
    goto :goto_3

    .line 2307
    :cond_7
    const/4 v8, 0x0

    .restart local v8    # "i":I
    :goto_2
    iget v10, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mSpanCount:I

    if-ge v8, v10, :cond_9

    .line 2308
    iget-object v10, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mSpans:[Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;

    aget-object v10, v10, v8

    invoke-virtual {v10, v7, v2}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;->getFocusableViewAfter(II)Landroid/view/View;

    move-result-object v10

    .line 2309
    .restart local v10    # "view":Landroid/view/View;
    if-eqz v10, :cond_8

    if-eq v10, v0, :cond_8

    .line 2310
    return-object v10

    .line 2307
    .end local v10    # "view":Landroid/view/View;
    :cond_8
    add-int/lit8 v8, v8, 0x1

    goto :goto_2

    .line 2319
    .end local v8    # "i":I
    :cond_9
    :goto_3
    iget-boolean v8, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mReverseLayout:Z

    xor-int/2addr v8, v6

    const/4 v10, -0x1

    if-ne v2, v10, :cond_a

    const/4 v10, 0x1

    goto :goto_4

    :cond_a
    const/4 v10, 0x0

    :goto_4
    if-ne v8, v10, :cond_b

    const/4 v9, 0x1

    :cond_b
    move v8, v9

    .line 2320
    .local v8, "shouldSearchFromStart":Z
    const/4 v9, 0x0

    .line 2321
    .local v9, "unfocusableCandidate":Landroid/view/View;
    if-nez v4, :cond_d

    .line 2322
    if-eqz v8, :cond_c

    .line 2323
    invoke-virtual {v5}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;->findFirstPartiallyVisibleItemPosition()I

    move-result v10

    goto :goto_5

    .line 2324
    :cond_c
    invoke-virtual {v5}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;->findLastPartiallyVisibleItemPosition()I

    move-result v10

    .line 2322
    :goto_5
    invoke-virtual {p0, v10}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->findViewByPosition(I)Landroid/view/View;

    move-result-object v9

    .line 2325
    if-eqz v9, :cond_d

    if-eq v9, v0, :cond_d

    .line 2326
    return-object v9

    .line 2330
    :cond_d
    invoke-direct {p0, v2}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->preferLastSpan(I)Z

    move-result v10

    if-eqz v10, :cond_12

    .line 2331
    iget v10, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mSpanCount:I

    sub-int/2addr v10, v6

    .local v10, "i":I
    :goto_6
    if-ltz v10, :cond_11

    .line 2332
    iget v6, v5, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;->mIndex:I

    if-ne v10, v6, :cond_e

    .line 2333
    goto :goto_8

    .line 2335
    :cond_e
    if-eqz v8, :cond_f

    .line 2336
    iget-object v6, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mSpans:[Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;

    aget-object v6, v6, v10

    invoke-virtual {v6}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;->findFirstPartiallyVisibleItemPosition()I

    move-result v6

    goto :goto_7

    .line 2337
    :cond_f
    iget-object v6, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mSpans:[Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;

    aget-object v6, v6, v10

    invoke-virtual {v6}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;->findLastPartiallyVisibleItemPosition()I

    move-result v6

    .line 2335
    :goto_7
    invoke-virtual {p0, v6}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->findViewByPosition(I)Landroid/view/View;

    move-result-object v6

    .line 2338
    .end local v9    # "unfocusableCandidate":Landroid/view/View;
    .local v6, "unfocusableCandidate":Landroid/view/View;
    if-eqz v6, :cond_10

    if-eq v6, v0, :cond_10

    .line 2339
    return-object v6

    .line 2331
    :cond_10
    move-object v9, v6

    .end local v6    # "unfocusableCandidate":Landroid/view/View;
    .restart local v9    # "unfocusableCandidate":Landroid/view/View;
    :goto_8
    add-int/lit8 v10, v10, -0x1

    goto :goto_6

    .end local v10    # "i":I
    :cond_11
    goto :goto_b

    .line 2343
    :cond_12
    const/4 v6, 0x0

    .local v6, "i":I
    :goto_9
    iget v10, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mSpanCount:I

    if-ge v6, v10, :cond_15

    .line 2344
    if-eqz v8, :cond_13

    .line 2345
    iget-object v10, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mSpans:[Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;

    aget-object v10, v10, v6

    invoke-virtual {v10}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;->findFirstPartiallyVisibleItemPosition()I

    move-result v10

    goto :goto_a

    .line 2346
    :cond_13
    iget-object v10, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mSpans:[Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;

    aget-object v10, v10, v6

    invoke-virtual {v10}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;->findLastPartiallyVisibleItemPosition()I

    move-result v10

    .line 2344
    :goto_a
    invoke-virtual {p0, v10}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->findViewByPosition(I)Landroid/view/View;

    move-result-object v9

    .line 2347
    if-eqz v9, :cond_14

    if-eq v9, v0, :cond_14

    .line 2348
    return-object v9

    .line 2343
    :cond_14
    add-int/lit8 v6, v6, 0x1

    goto :goto_9

    .line 2352
    .end local v6    # "i":I
    :cond_15
    :goto_b
    return-object v1
.end method

.method public onInitializeAccessibilityEvent(Landroid/view/accessibility/AccessibilityEvent;)V
    .locals 4
    .param p1, "event"    # Landroid/view/accessibility/AccessibilityEvent;

    .line 1294
    invoke-super {p0, p1}, Landroidx/recyclerview/widget/RecyclerView$LayoutManager;->onInitializeAccessibilityEvent(Landroid/view/accessibility/AccessibilityEvent;)V

    .line 1295
    invoke-virtual {p0}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->getChildCount()I

    move-result v0

    if-lez v0, :cond_3

    .line 1296
    const/4 v0, 0x0

    invoke-virtual {p0, v0}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->findFirstVisibleItemClosestToStart(Z)Landroid/view/View;

    move-result-object v1

    .line 1297
    .local v1, "start":Landroid/view/View;
    invoke-virtual {p0, v0}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->findFirstVisibleItemClosestToEnd(Z)Landroid/view/View;

    move-result-object v0

    .line 1298
    .local v0, "end":Landroid/view/View;
    if-eqz v1, :cond_2

    if-nez v0, :cond_0

    goto :goto_0

    .line 1301
    :cond_0
    invoke-virtual {p0, v1}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->getPosition(Landroid/view/View;)I

    move-result v2

    .line 1302
    .local v2, "startPos":I
    invoke-virtual {p0, v0}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->getPosition(Landroid/view/View;)I

    move-result v3

    .line 1303
    .local v3, "endPos":I
    if-ge v2, v3, :cond_1

    .line 1304
    invoke-virtual {p1, v2}, Landroid/view/accessibility/AccessibilityEvent;->setFromIndex(I)V

    .line 1305
    invoke-virtual {p1, v3}, Landroid/view/accessibility/AccessibilityEvent;->setToIndex(I)V

    goto :goto_1

    .line 1307
    :cond_1
    invoke-virtual {p1, v3}, Landroid/view/accessibility/AccessibilityEvent;->setFromIndex(I)V

    .line 1308
    invoke-virtual {p1, v2}, Landroid/view/accessibility/AccessibilityEvent;->setToIndex(I)V

    goto :goto_1

    .line 1299
    .end local v2    # "startPos":I
    .end local v3    # "endPos":I
    :cond_2
    :goto_0
    return-void

    .line 1311
    .end local v0    # "end":Landroid/view/View;
    .end local v1    # "start":Landroid/view/View;
    :cond_3
    :goto_1
    return-void
.end method

.method public onItemsAdded(Landroidx/recyclerview/widget/RecyclerView;II)V
    .locals 1
    .param p1, "recyclerView"    # Landroidx/recyclerview/widget/RecyclerView;
    .param p2, "positionStart"    # I
    .param p3, "itemCount"    # I

    .line 1481
    const/4 v0, 0x1

    invoke-direct {p0, p2, p3, v0}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->handleUpdate(III)V

    .line 1482
    return-void
.end method

.method public onItemsChanged(Landroidx/recyclerview/widget/RecyclerView;)V
    .locals 1
    .param p1, "recyclerView"    # Landroidx/recyclerview/widget/RecyclerView;

    .line 1486
    iget-object v0, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mLazySpanLookup:Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup;

    invoke-virtual {v0}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup;->clear()V

    .line 1487
    invoke-virtual {p0}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->requestLayout()V

    .line 1488
    return-void
.end method

.method public onItemsMoved(Landroidx/recyclerview/widget/RecyclerView;III)V
    .locals 1
    .param p1, "recyclerView"    # Landroidx/recyclerview/widget/RecyclerView;
    .param p2, "from"    # I
    .param p3, "to"    # I
    .param p4, "itemCount"    # I

    .line 1492
    const/16 v0, 0x8

    invoke-direct {p0, p2, p3, v0}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->handleUpdate(III)V

    .line 1493
    return-void
.end method

.method public onItemsRemoved(Landroidx/recyclerview/widget/RecyclerView;II)V
    .locals 1
    .param p1, "recyclerView"    # Landroidx/recyclerview/widget/RecyclerView;
    .param p2, "positionStart"    # I
    .param p3, "itemCount"    # I

    .line 1476
    const/4 v0, 0x2

    invoke-direct {p0, p2, p3, v0}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->handleUpdate(III)V

    .line 1477
    return-void
.end method

.method public onItemsUpdated(Landroidx/recyclerview/widget/RecyclerView;IILjava/lang/Object;)V
    .locals 1
    .param p1, "recyclerView"    # Landroidx/recyclerview/widget/RecyclerView;
    .param p2, "positionStart"    # I
    .param p3, "itemCount"    # I
    .param p4, "payload"    # Ljava/lang/Object;

    .line 1498
    const/4 v0, 0x4

    invoke-direct {p0, p2, p3, v0}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->handleUpdate(III)V

    .line 1499
    return-void
.end method

.method public onLayoutChildren(Landroidx/recyclerview/widget/RecyclerView$Recycler;Landroidx/recyclerview/widget/RecyclerView$State;)V
    .locals 1
    .param p1, "recycler"    # Landroidx/recyclerview/widget/RecyclerView$Recycler;
    .param p2, "state"    # Landroidx/recyclerview/widget/RecyclerView$State;

    .line 604
    const/4 v0, 0x1

    invoke-direct {p0, p1, p2, v0}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->onLayoutChildren(Landroidx/recyclerview/widget/RecyclerView$Recycler;Landroidx/recyclerview/widget/RecyclerView$State;Z)V

    .line 605
    return-void
.end method

.method public onLayoutCompleted(Landroidx/recyclerview/widget/RecyclerView$State;)V
    .locals 1
    .param p1, "state"    # Landroidx/recyclerview/widget/RecyclerView$State;

    .line 734
    invoke-super {p0, p1}, Landroidx/recyclerview/widget/RecyclerView$LayoutManager;->onLayoutCompleted(Landroidx/recyclerview/widget/RecyclerView$State;)V

    .line 735
    const/4 v0, -0x1

    iput v0, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mPendingScrollPosition:I

    .line 736
    const/high16 v0, -0x80000000

    iput v0, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mPendingScrollPositionOffset:I

    .line 737
    const/4 v0, 0x0

    iput-object v0, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mPendingSavedState:Landroidx/recyclerview/widget/StaggeredGridLayoutManager$SavedState;

    .line 738
    iget-object v0, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mAnchorInfo:Landroidx/recyclerview/widget/StaggeredGridLayoutManager$AnchorInfo;

    invoke-virtual {v0}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$AnchorInfo;->reset()V

    .line 739
    return-void
.end method

.method public onRestoreInstanceState(Landroid/os/Parcelable;)V
    .locals 3
    .param p1, "state"    # Landroid/os/Parcelable;

    .line 1230
    instance-of v0, p1, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$SavedState;

    if-eqz v0, :cond_1

    .line 1231
    move-object v0, p1

    check-cast v0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$SavedState;

    iput-object v0, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mPendingSavedState:Landroidx/recyclerview/widget/StaggeredGridLayoutManager$SavedState;

    .line 1232
    iget v1, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mPendingScrollPosition:I

    const/4 v2, -0x1

    if-eq v1, v2, :cond_0

    .line 1233
    invoke-virtual {v0}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$SavedState;->invalidateAnchorPositionInfo()V

    .line 1234
    iget-object v0, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mPendingSavedState:Landroidx/recyclerview/widget/StaggeredGridLayoutManager$SavedState;

    invoke-virtual {v0}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$SavedState;->invalidateSpanInfo()V

    .line 1236
    :cond_0
    invoke-virtual {p0}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->requestLayout()V

    .line 1240
    :cond_1
    return-void
.end method

.method public onSaveInstanceState()Landroid/os/Parcelable;
    .locals 4

    .line 1244
    iget-object v0, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mPendingSavedState:Landroidx/recyclerview/widget/StaggeredGridLayoutManager$SavedState;

    if-eqz v0, :cond_0

    .line 1245
    new-instance v0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$SavedState;

    iget-object v1, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mPendingSavedState:Landroidx/recyclerview/widget/StaggeredGridLayoutManager$SavedState;

    invoke-direct {v0, v1}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$SavedState;-><init>(Landroidx/recyclerview/widget/StaggeredGridLayoutManager$SavedState;)V

    return-object v0

    .line 1247
    :cond_0
    new-instance v0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$SavedState;

    invoke-direct {v0}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$SavedState;-><init>()V

    .line 1248
    .local v0, "state":Landroidx/recyclerview/widget/StaggeredGridLayoutManager$SavedState;
    iget-boolean v1, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mReverseLayout:Z

    iput-boolean v1, v0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$SavedState;->mReverseLayout:Z

    .line 1249
    iget-boolean v1, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mLastLayoutFromEnd:Z

    iput-boolean v1, v0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$SavedState;->mAnchorLayoutFromEnd:Z

    .line 1250
    iget-boolean v1, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mLastLayoutRTL:Z

    iput-boolean v1, v0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$SavedState;->mLastLayoutRTL:Z

    .line 1252
    iget-object v1, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mLazySpanLookup:Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup;

    const/4 v2, 0x0

    if-eqz v1, :cond_1

    iget-object v1, v1, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup;->mData:[I

    if-eqz v1, :cond_1

    .line 1253
    iget-object v1, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mLazySpanLookup:Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup;

    iget-object v1, v1, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup;->mData:[I

    iput-object v1, v0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$SavedState;->mSpanLookup:[I

    .line 1254
    iget-object v1, v0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$SavedState;->mSpanLookup:[I

    array-length v1, v1

    iput v1, v0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$SavedState;->mSpanLookupSize:I

    .line 1255
    iget-object v1, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mLazySpanLookup:Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup;

    iget-object v1, v1, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup;->mFullSpanItems:Ljava/util/List;

    iput-object v1, v0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$SavedState;->mFullSpanItems:Ljava/util/List;

    goto :goto_0

    .line 1257
    :cond_1
    iput v2, v0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$SavedState;->mSpanLookupSize:I

    .line 1260
    :goto_0
    invoke-virtual {p0}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->getChildCount()I

    move-result v1

    if-lez v1, :cond_6

    .line 1261
    iget-boolean v1, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mLastLayoutFromEnd:Z

    if-eqz v1, :cond_2

    invoke-virtual {p0}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->getLastChildPosition()I

    move-result v1

    goto :goto_1

    .line 1262
    :cond_2
    invoke-virtual {p0}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->getFirstChildPosition()I

    move-result v1

    :goto_1
    iput v1, v0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$SavedState;->mAnchorPosition:I

    .line 1263
    invoke-virtual {p0}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->findFirstVisibleItemPositionInt()I

    move-result v1

    iput v1, v0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$SavedState;->mVisibleAnchorPosition:I

    .line 1264
    iget v1, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mSpanCount:I

    iput v1, v0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$SavedState;->mSpanOffsetsSize:I

    .line 1265
    iget v1, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mSpanCount:I

    new-array v1, v1, [I

    iput-object v1, v0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$SavedState;->mSpanOffsets:[I

    .line 1266
    const/4 v1, 0x0

    .local v1, "i":I
    :goto_2
    iget v2, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mSpanCount:I

    if-ge v1, v2, :cond_5

    .line 1268
    iget-boolean v2, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mLastLayoutFromEnd:Z

    const/high16 v3, -0x80000000

    if-eqz v2, :cond_3

    .line 1269
    iget-object v2, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mSpans:[Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;

    aget-object v2, v2, v1

    invoke-virtual {v2, v3}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;->getEndLine(I)I

    move-result v2

    .line 1270
    .local v2, "line":I
    if-eq v2, v3, :cond_4

    .line 1271
    iget-object v3, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mPrimaryOrientation:Landroidx/recyclerview/widget/OrientationHelper;

    invoke-virtual {v3}, Landroidx/recyclerview/widget/OrientationHelper;->getEndAfterPadding()I

    move-result v3

    sub-int/2addr v2, v3

    goto :goto_3

    .line 1274
    .end local v2    # "line":I
    :cond_3
    iget-object v2, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mSpans:[Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;

    aget-object v2, v2, v1

    invoke-virtual {v2, v3}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;->getStartLine(I)I

    move-result v2

    .line 1275
    .restart local v2    # "line":I
    if-eq v2, v3, :cond_4

    .line 1276
    iget-object v3, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mPrimaryOrientation:Landroidx/recyclerview/widget/OrientationHelper;

    invoke-virtual {v3}, Landroidx/recyclerview/widget/OrientationHelper;->getStartAfterPadding()I

    move-result v3

    sub-int/2addr v2, v3

    .line 1279
    :cond_4
    :goto_3
    iget-object v3, v0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$SavedState;->mSpanOffsets:[I

    aput v2, v3, v1

    .line 1266
    .end local v2    # "line":I
    add-int/lit8 v1, v1, 0x1

    goto :goto_2

    .end local v1    # "i":I
    :cond_5
    goto :goto_4

    .line 1282
    :cond_6
    const/4 v1, -0x1

    iput v1, v0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$SavedState;->mAnchorPosition:I

    .line 1283
    iput v1, v0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$SavedState;->mVisibleAnchorPosition:I

    .line 1284
    iput v2, v0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$SavedState;->mSpanOffsetsSize:I

    .line 1289
    :goto_4
    return-object v0
.end method

.method public onScrollStateChanged(I)V
    .locals 0
    .param p1, "state"    # I

    .line 315
    if-nez p1, :cond_0

    .line 316
    invoke-virtual {p0}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->checkForGaps()Z

    .line 318
    :cond_0
    return-void
.end method

.method prepareLayoutStateForDelta(ILandroidx/recyclerview/widget/RecyclerView$State;)V
    .locals 4
    .param p1, "delta"    # I
    .param p2, "state"    # Landroidx/recyclerview/widget/RecyclerView$State;

    .line 2134
    if-lez p1, :cond_0

    .line 2135
    const/4 v0, 0x1

    .line 2136
    .local v0, "layoutDir":I
    invoke-virtual {p0}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->getLastChildPosition()I

    move-result v1

    .local v1, "referenceChildPosition":I
    goto :goto_0

    .line 2138
    .end local v0    # "layoutDir":I
    .end local v1    # "referenceChildPosition":I
    :cond_0
    const/4 v0, -0x1

    .line 2139
    .restart local v0    # "layoutDir":I
    invoke-virtual {p0}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->getFirstChildPosition()I

    move-result v1

    .line 2141
    .restart local v1    # "referenceChildPosition":I
    :goto_0
    iget-object v2, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mLayoutState:Landroidx/recyclerview/widget/LayoutState;

    const/4 v3, 0x1

    iput-boolean v3, v2, Landroidx/recyclerview/widget/LayoutState;->mRecycle:Z

    .line 2142
    invoke-direct {p0, v1, p2}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->updateLayoutState(ILandroidx/recyclerview/widget/RecyclerView$State;)V

    .line 2143
    invoke-direct {p0, v0}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->setLayoutStateDirection(I)V

    .line 2144
    iget-object v2, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mLayoutState:Landroidx/recyclerview/widget/LayoutState;

    iget v3, v2, Landroidx/recyclerview/widget/LayoutState;->mItemDirection:I

    add-int/2addr v3, v1

    iput v3, v2, Landroidx/recyclerview/widget/LayoutState;->mCurrentPosition:I

    .line 2145
    iget-object v2, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mLayoutState:Landroidx/recyclerview/widget/LayoutState;

    invoke-static {p1}, Ljava/lang/Math;->abs(I)I

    move-result v3

    iput v3, v2, Landroidx/recyclerview/widget/LayoutState;->mAvailable:I

    .line 2146
    return-void
.end method

.method scrollBy(ILandroidx/recyclerview/widget/RecyclerView$Recycler;Landroidx/recyclerview/widget/RecyclerView$State;)I
    .locals 6
    .param p1, "dt"    # I
    .param p2, "recycler"    # Landroidx/recyclerview/widget/RecyclerView$Recycler;
    .param p3, "state"    # Landroidx/recyclerview/widget/RecyclerView$State;

    .line 2149
    invoke-virtual {p0}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->getChildCount()I

    move-result v0

    const/4 v1, 0x0

    if-eqz v0, :cond_3

    if-nez p1, :cond_0

    goto :goto_1

    .line 2153
    :cond_0
    invoke-virtual {p0, p1, p3}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->prepareLayoutStateForDelta(ILandroidx/recyclerview/widget/RecyclerView$State;)V

    .line 2154
    iget-object v0, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mLayoutState:Landroidx/recyclerview/widget/LayoutState;

    invoke-direct {p0, p2, v0, p3}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->fill(Landroidx/recyclerview/widget/RecyclerView$Recycler;Landroidx/recyclerview/widget/LayoutState;Landroidx/recyclerview/widget/RecyclerView$State;)I

    move-result v0

    .line 2155
    .local v0, "consumed":I
    iget-object v2, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mLayoutState:Landroidx/recyclerview/widget/LayoutState;

    iget v2, v2, Landroidx/recyclerview/widget/LayoutState;->mAvailable:I

    .line 2157
    .local v2, "available":I
    if-ge v2, v0, :cond_1

    .line 2158
    move v3, p1

    .local v3, "totalScroll":I
    goto :goto_0

    .line 2159
    .end local v3    # "totalScroll":I
    :cond_1
    if-gez p1, :cond_2

    .line 2160
    neg-int v3, v0

    .restart local v3    # "totalScroll":I
    goto :goto_0

    .line 2162
    .end local v3    # "totalScroll":I
    :cond_2
    move v3, v0

    .line 2168
    .restart local v3    # "totalScroll":I
    :goto_0
    iget-object v4, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mPrimaryOrientation:Landroidx/recyclerview/widget/OrientationHelper;

    neg-int v5, v3

    invoke-virtual {v4, v5}, Landroidx/recyclerview/widget/OrientationHelper;->offsetChildren(I)V

    .line 2170
    iget-boolean v4, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mShouldReverseLayout:Z

    iput-boolean v4, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mLastLayoutFromEnd:Z

    .line 2171
    iget-object v4, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mLayoutState:Landroidx/recyclerview/widget/LayoutState;

    iput v1, v4, Landroidx/recyclerview/widget/LayoutState;->mAvailable:I

    .line 2172
    iget-object v1, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mLayoutState:Landroidx/recyclerview/widget/LayoutState;

    invoke-direct {p0, p2, v1}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->recycle(Landroidx/recyclerview/widget/RecyclerView$Recycler;Landroidx/recyclerview/widget/LayoutState;)V

    .line 2173
    return v3

    .line 2150
    .end local v0    # "consumed":I
    .end local v2    # "available":I
    .end local v3    # "totalScroll":I
    :cond_3
    :goto_1
    return v1
.end method

.method public scrollHorizontallyBy(ILandroidx/recyclerview/widget/RecyclerView$Recycler;Landroidx/recyclerview/widget/RecyclerView$State;)I
    .locals 1
    .param p1, "dx"    # I
    .param p2, "recycler"    # Landroidx/recyclerview/widget/RecyclerView$Recycler;
    .param p3, "state"    # Landroidx/recyclerview/widget/RecyclerView$State;

    .line 2007
    invoke-virtual {p0, p1, p2, p3}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->scrollBy(ILandroidx/recyclerview/widget/RecyclerView$Recycler;Landroidx/recyclerview/widget/RecyclerView$State;)I

    move-result v0

    return v0
.end method

.method public scrollToPosition(I)V
    .locals 1
    .param p1, "position"    # I

    .line 2051
    iget-object v0, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mPendingSavedState:Landroidx/recyclerview/widget/StaggeredGridLayoutManager$SavedState;

    if-eqz v0, :cond_0

    iget v0, v0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$SavedState;->mAnchorPosition:I

    if-eq v0, p1, :cond_0

    .line 2052
    iget-object v0, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mPendingSavedState:Landroidx/recyclerview/widget/StaggeredGridLayoutManager$SavedState;

    invoke-virtual {v0}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$SavedState;->invalidateAnchorPositionInfo()V

    .line 2054
    :cond_0
    iput p1, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mPendingScrollPosition:I

    .line 2055
    const/high16 v0, -0x80000000

    iput v0, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mPendingScrollPositionOffset:I

    .line 2056
    invoke-virtual {p0}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->requestLayout()V

    .line 2057
    return-void
.end method

.method public scrollToPositionWithOffset(II)V
    .locals 1
    .param p1, "position"    # I
    .param p2, "offset"    # I

    .line 2073
    iget-object v0, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mPendingSavedState:Landroidx/recyclerview/widget/StaggeredGridLayoutManager$SavedState;

    if-eqz v0, :cond_0

    .line 2074
    invoke-virtual {v0}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$SavedState;->invalidateAnchorPositionInfo()V

    .line 2076
    :cond_0
    iput p1, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mPendingScrollPosition:I

    .line 2077
    iput p2, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mPendingScrollPositionOffset:I

    .line 2078
    invoke-virtual {p0}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->requestLayout()V

    .line 2079
    return-void
.end method

.method public scrollVerticallyBy(ILandroidx/recyclerview/widget/RecyclerView$Recycler;Landroidx/recyclerview/widget/RecyclerView$State;)I
    .locals 1
    .param p1, "dy"    # I
    .param p2, "recycler"    # Landroidx/recyclerview/widget/RecyclerView$Recycler;
    .param p3, "state"    # Landroidx/recyclerview/widget/RecyclerView$State;

    .line 2013
    invoke-virtual {p0, p1, p2, p3}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->scrollBy(ILandroidx/recyclerview/widget/RecyclerView$Recycler;Landroidx/recyclerview/widget/RecyclerView$State;)I

    move-result v0

    return v0
.end method

.method public setGapStrategy(I)V
    .locals 2
    .param p1, "gapStrategy"    # I

    .line 512
    const/4 v0, 0x0

    invoke-virtual {p0, v0}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->assertNotInLayoutOrScroll(Ljava/lang/String;)V

    .line 513
    iget v0, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mGapStrategy:I

    if-ne p1, v0, :cond_0

    .line 514
    return-void

    .line 516
    :cond_0
    if-eqz p1, :cond_2

    const/4 v0, 0x2

    if-ne p1, v0, :cond_1

    goto :goto_0

    .line 518
    :cond_1
    new-instance v0, Ljava/lang/IllegalArgumentException;

    const-string v1, "invalid gap strategy. Must be GAP_HANDLING_NONE or GAP_HANDLING_MOVE_ITEMS_BETWEEN_SPANS"

    invoke-direct {v0, v1}, Ljava/lang/IllegalArgumentException;-><init>(Ljava/lang/String;)V

    throw v0

    .line 521
    :cond_2
    :goto_0
    iput p1, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mGapStrategy:I

    .line 522
    invoke-virtual {p0}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->requestLayout()V

    .line 523
    return-void
.end method

.method public setMeasuredDimension(Landroid/graphics/Rect;II)V
    .locals 6
    .param p1, "childrenBounds"    # Landroid/graphics/Rect;
    .param p2, "wSpec"    # I
    .param p3, "hSpec"    # I

    .line 586
    invoke-virtual {p0}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->getPaddingLeft()I

    move-result v0

    invoke-virtual {p0}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->getPaddingRight()I

    move-result v1

    add-int/2addr v0, v1

    .line 587
    .local v0, "horizontalPadding":I
    invoke-virtual {p0}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->getPaddingTop()I

    move-result v1

    invoke-virtual {p0}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->getPaddingBottom()I

    move-result v2

    add-int/2addr v1, v2

    .line 588
    .local v1, "verticalPadding":I
    iget v2, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mOrientation:I

    const/4 v3, 0x1

    if-ne v2, v3, :cond_0

    .line 589
    invoke-virtual {p1}, Landroid/graphics/Rect;->height()I

    move-result v2

    add-int/2addr v2, v1

    .line 590
    .local v2, "usedHeight":I
    invoke-virtual {p0}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->getMinimumHeight()I

    move-result v3

    invoke-static {p3, v2, v3}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->chooseSize(III)I

    move-result v3

    .line 591
    .local v3, "height":I
    iget v4, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mSizePerSpan:I

    iget v5, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mSpanCount:I

    mul-int v4, v4, v5

    add-int/2addr v4, v0

    .line 592
    invoke-virtual {p0}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->getMinimumWidth()I

    move-result v5

    .line 591
    invoke-static {p2, v4, v5}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->chooseSize(III)I

    move-result v2

    .line 593
    .local v2, "width":I
    goto :goto_0

    .line 594
    .end local v2    # "width":I
    .end local v3    # "height":I
    :cond_0
    invoke-virtual {p1}, Landroid/graphics/Rect;->width()I

    move-result v2

    add-int/2addr v2, v0

    .line 595
    .local v2, "usedWidth":I
    invoke-virtual {p0}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->getMinimumWidth()I

    move-result v3

    invoke-static {p2, v2, v3}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->chooseSize(III)I

    move-result v3

    .line 596
    .local v3, "width":I
    iget v4, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mSizePerSpan:I

    iget v5, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mSpanCount:I

    mul-int v4, v4, v5

    add-int/2addr v4, v1

    .line 597
    invoke-virtual {p0}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->getMinimumHeight()I

    move-result v5

    .line 596
    invoke-static {p3, v4, v5}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->chooseSize(III)I

    move-result v4

    move v2, v3

    move v3, v4

    .line 599
    .local v2, "width":I
    .local v3, "height":I
    :goto_0
    invoke-virtual {p0, v2, v3}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->setMeasuredDimension(II)V

    .line 600
    return-void
.end method

.method public setOrientation(I)V
    .locals 2
    .param p1, "orientation"    # I

    .line 448
    if-eqz p1, :cond_1

    const/4 v0, 0x1

    if-ne p1, v0, :cond_0

    goto :goto_0

    .line 449
    :cond_0
    new-instance v0, Ljava/lang/IllegalArgumentException;

    const-string v1, "invalid orientation."

    invoke-direct {v0, v1}, Ljava/lang/IllegalArgumentException;-><init>(Ljava/lang/String;)V

    throw v0

    .line 451
    :cond_1
    :goto_0
    const/4 v0, 0x0

    invoke-virtual {p0, v0}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->assertNotInLayoutOrScroll(Ljava/lang/String;)V

    .line 452
    iget v0, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mOrientation:I

    if-ne p1, v0, :cond_2

    .line 453
    return-void

    .line 455
    :cond_2
    iput p1, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mOrientation:I

    .line 456
    iget-object v0, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mPrimaryOrientation:Landroidx/recyclerview/widget/OrientationHelper;

    .line 457
    .local v0, "tmp":Landroidx/recyclerview/widget/OrientationHelper;
    iget-object v1, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mSecondaryOrientation:Landroidx/recyclerview/widget/OrientationHelper;

    iput-object v1, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mPrimaryOrientation:Landroidx/recyclerview/widget/OrientationHelper;

    .line 458
    iput-object v0, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mSecondaryOrientation:Landroidx/recyclerview/widget/OrientationHelper;

    .line 459
    invoke-virtual {p0}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->requestLayout()V

    .line 460
    return-void
.end method

.method public setReverseLayout(Z)V
    .locals 1
    .param p1, "reverseLayout"    # Z

    .line 476
    const/4 v0, 0x0

    invoke-virtual {p0, v0}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->assertNotInLayoutOrScroll(Ljava/lang/String;)V

    .line 477
    iget-object v0, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mPendingSavedState:Landroidx/recyclerview/widget/StaggeredGridLayoutManager$SavedState;

    if-eqz v0, :cond_0

    iget-boolean v0, v0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$SavedState;->mReverseLayout:Z

    if-eq v0, p1, :cond_0

    .line 478
    iget-object v0, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mPendingSavedState:Landroidx/recyclerview/widget/StaggeredGridLayoutManager$SavedState;

    iput-boolean p1, v0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$SavedState;->mReverseLayout:Z

    .line 480
    :cond_0
    iput-boolean p1, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mReverseLayout:Z

    .line 481
    invoke-virtual {p0}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->requestLayout()V

    .line 482
    return-void
.end method

.method public setSpanCount(I)V
    .locals 3
    .param p1, "spanCount"    # I

    .line 428
    const/4 v0, 0x0

    invoke-virtual {p0, v0}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->assertNotInLayoutOrScroll(Ljava/lang/String;)V

    .line 429
    iget v0, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mSpanCount:I

    if-eq p1, v0, :cond_1

    .line 430
    invoke-virtual {p0}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->invalidateSpanAssignments()V

    .line 431
    iput p1, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mSpanCount:I

    .line 432
    new-instance v0, Ljava/util/BitSet;

    iget v1, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mSpanCount:I

    invoke-direct {v0, v1}, Ljava/util/BitSet;-><init>(I)V

    iput-object v0, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mRemainingSpans:Ljava/util/BitSet;

    .line 433
    iget v0, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mSpanCount:I

    new-array v0, v0, [Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;

    iput-object v0, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mSpans:[Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;

    .line 434
    const/4 v0, 0x0

    .local v0, "i":I
    :goto_0
    iget v1, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mSpanCount:I

    if-ge v0, v1, :cond_0

    .line 435
    iget-object v1, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mSpans:[Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;

    new-instance v2, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;

    invoke-direct {v2, p0, v0}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;-><init>(Landroidx/recyclerview/widget/StaggeredGridLayoutManager;I)V

    aput-object v2, v1, v0

    .line 434
    add-int/lit8 v0, v0, 0x1

    goto :goto_0

    .line 437
    .end local v0    # "i":I
    :cond_0
    invoke-virtual {p0}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->requestLayout()V

    .line 439
    :cond_1
    return-void
.end method

.method public smoothScrollToPosition(Landroidx/recyclerview/widget/RecyclerView;Landroidx/recyclerview/widget/RecyclerView$State;I)V
    .locals 2
    .param p1, "recyclerView"    # Landroidx/recyclerview/widget/RecyclerView;
    .param p2, "state"    # Landroidx/recyclerview/widget/RecyclerView$State;
    .param p3, "position"    # I

    .line 2044
    new-instance v0, Landroidx/recyclerview/widget/LinearSmoothScroller;

    invoke-virtual {p1}, Landroidx/recyclerview/widget/RecyclerView;->getContext()Landroid/content/Context;

    move-result-object v1

    invoke-direct {v0, v1}, Landroidx/recyclerview/widget/LinearSmoothScroller;-><init>(Landroid/content/Context;)V

    .line 2045
    .local v0, "scroller":Landroidx/recyclerview/widget/LinearSmoothScroller;
    invoke-virtual {v0, p3}, Landroidx/recyclerview/widget/LinearSmoothScroller;->setTargetPosition(I)V

    .line 2046
    invoke-virtual {p0, v0}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->startSmoothScroll(Landroidx/recyclerview/widget/RecyclerView$SmoothScroller;)V

    .line 2047
    return-void
.end method

.method public supportsPredictiveItemAnimations()Z
    .locals 1

    .line 943
    iget-object v0, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mPendingSavedState:Landroidx/recyclerview/widget/StaggeredGridLayoutManager$SavedState;

    if-nez v0, :cond_0

    const/4 v0, 0x1

    goto :goto_0

    :cond_0
    const/4 v0, 0x0

    :goto_0
    return v0
.end method

.method updateAnchorFromPendingData(Landroidx/recyclerview/widget/RecyclerView$State;Landroidx/recyclerview/widget/StaggeredGridLayoutManager$AnchorInfo;)Z
    .locals 7
    .param p1, "state"    # Landroidx/recyclerview/widget/RecyclerView$State;
    .param p2, "anchorInfo"    # Landroidx/recyclerview/widget/StaggeredGridLayoutManager$AnchorInfo;

    .line 857
    invoke-virtual {p1}, Landroidx/recyclerview/widget/RecyclerView$State;->isPreLayout()Z

    move-result v0

    const/4 v1, 0x0

    if-nez v0, :cond_f

    iget v0, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mPendingScrollPosition:I

    const/4 v2, -0x1

    if-ne v0, v2, :cond_0

    goto/16 :goto_8

    .line 861
    :cond_0
    const/high16 v3, -0x80000000

    if-ltz v0, :cond_e

    invoke-virtual {p1}, Landroidx/recyclerview/widget/RecyclerView$State;->getItemCount()I

    move-result v4

    if-lt v0, v4, :cond_1

    goto/16 :goto_7

    .line 867
    :cond_1
    iget-object v0, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mPendingSavedState:Landroidx/recyclerview/widget/StaggeredGridLayoutManager$SavedState;

    const/4 v4, 0x1

    if-eqz v0, :cond_3

    iget v0, v0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$SavedState;->mAnchorPosition:I

    if-eq v0, v2, :cond_3

    iget-object v0, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mPendingSavedState:Landroidx/recyclerview/widget/StaggeredGridLayoutManager$SavedState;

    iget v0, v0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$SavedState;->mSpanOffsetsSize:I

    if-ge v0, v4, :cond_2

    goto :goto_0

    .line 928
    :cond_2
    iput v3, p2, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$AnchorInfo;->mOffset:I

    .line 929
    iget v0, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mPendingScrollPosition:I

    iput v0, p2, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$AnchorInfo;->mPosition:I

    goto/16 :goto_6

    .line 870
    :cond_3
    :goto_0
    iget v0, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mPendingScrollPosition:I

    invoke-virtual {p0, v0}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->findViewByPosition(I)Landroid/view/View;

    move-result-object v0

    .line 871
    .local v0, "child":Landroid/view/View;
    if-eqz v0, :cond_b

    .line 874
    iget-boolean v1, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mShouldReverseLayout:Z

    if-eqz v1, :cond_4

    invoke-virtual {p0}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->getLastChildPosition()I

    move-result v1

    goto :goto_1

    .line 875
    :cond_4
    invoke-virtual {p0}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->getFirstChildPosition()I

    move-result v1

    :goto_1
    iput v1, p2, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$AnchorInfo;->mPosition:I

    .line 876
    iget v1, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mPendingScrollPositionOffset:I

    if-eq v1, v3, :cond_6

    .line 877
    iget-boolean v1, p2, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$AnchorInfo;->mLayoutFromEnd:Z

    if-eqz v1, :cond_5

    .line 878
    iget-object v1, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mPrimaryOrientation:Landroidx/recyclerview/widget/OrientationHelper;

    invoke-virtual {v1}, Landroidx/recyclerview/widget/OrientationHelper;->getEndAfterPadding()I

    move-result v1

    iget v2, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mPendingScrollPositionOffset:I

    sub-int/2addr v1, v2

    .line 880
    .local v1, "target":I
    iget-object v2, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mPrimaryOrientation:Landroidx/recyclerview/widget/OrientationHelper;

    invoke-virtual {v2, v0}, Landroidx/recyclerview/widget/OrientationHelper;->getDecoratedEnd(Landroid/view/View;)I

    move-result v2

    sub-int v2, v1, v2

    iput v2, p2, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$AnchorInfo;->mOffset:I

    .line 881
    .end local v1    # "target":I
    goto :goto_2

    .line 882
    :cond_5
    iget-object v1, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mPrimaryOrientation:Landroidx/recyclerview/widget/OrientationHelper;

    invoke-virtual {v1}, Landroidx/recyclerview/widget/OrientationHelper;->getStartAfterPadding()I

    move-result v1

    iget v2, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mPendingScrollPositionOffset:I

    add-int/2addr v1, v2

    .line 884
    .restart local v1    # "target":I
    iget-object v2, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mPrimaryOrientation:Landroidx/recyclerview/widget/OrientationHelper;

    invoke-virtual {v2, v0}, Landroidx/recyclerview/widget/OrientationHelper;->getDecoratedStart(Landroid/view/View;)I

    move-result v2

    sub-int v2, v1, v2

    iput v2, p2, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$AnchorInfo;->mOffset:I

    .line 886
    .end local v1    # "target":I
    :goto_2
    return v4

    .line 890
    :cond_6
    iget-object v1, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mPrimaryOrientation:Landroidx/recyclerview/widget/OrientationHelper;

    invoke-virtual {v1, v0}, Landroidx/recyclerview/widget/OrientationHelper;->getDecoratedMeasurement(Landroid/view/View;)I

    move-result v1

    .line 891
    .local v1, "childSize":I
    iget-object v2, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mPrimaryOrientation:Landroidx/recyclerview/widget/OrientationHelper;

    invoke-virtual {v2}, Landroidx/recyclerview/widget/OrientationHelper;->getTotalSpace()I

    move-result v2

    if-le v1, v2, :cond_8

    .line 893
    iget-boolean v2, p2, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$AnchorInfo;->mLayoutFromEnd:Z

    if-eqz v2, :cond_7

    .line 894
    iget-object v2, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mPrimaryOrientation:Landroidx/recyclerview/widget/OrientationHelper;

    invoke-virtual {v2}, Landroidx/recyclerview/widget/OrientationHelper;->getEndAfterPadding()I

    move-result v2

    goto :goto_3

    .line 895
    :cond_7
    iget-object v2, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mPrimaryOrientation:Landroidx/recyclerview/widget/OrientationHelper;

    invoke-virtual {v2}, Landroidx/recyclerview/widget/OrientationHelper;->getStartAfterPadding()I

    move-result v2

    :goto_3
    iput v2, p2, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$AnchorInfo;->mOffset:I

    .line 896
    return v4

    .line 899
    :cond_8
    iget-object v2, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mPrimaryOrientation:Landroidx/recyclerview/widget/OrientationHelper;

    invoke-virtual {v2, v0}, Landroidx/recyclerview/widget/OrientationHelper;->getDecoratedStart(Landroid/view/View;)I

    move-result v2

    iget-object v5, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mPrimaryOrientation:Landroidx/recyclerview/widget/OrientationHelper;

    .line 900
    invoke-virtual {v5}, Landroidx/recyclerview/widget/OrientationHelper;->getStartAfterPadding()I

    move-result v5

    sub-int/2addr v2, v5

    .line 901
    .local v2, "startGap":I
    if-gez v2, :cond_9

    .line 902
    neg-int v3, v2

    iput v3, p2, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$AnchorInfo;->mOffset:I

    .line 903
    return v4

    .line 905
    :cond_9
    iget-object v5, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mPrimaryOrientation:Landroidx/recyclerview/widget/OrientationHelper;

    invoke-virtual {v5}, Landroidx/recyclerview/widget/OrientationHelper;->getEndAfterPadding()I

    move-result v5

    iget-object v6, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mPrimaryOrientation:Landroidx/recyclerview/widget/OrientationHelper;

    .line 906
    invoke-virtual {v6, v0}, Landroidx/recyclerview/widget/OrientationHelper;->getDecoratedEnd(Landroid/view/View;)I

    move-result v6

    sub-int/2addr v5, v6

    .line 907
    .local v5, "endGap":I
    if-gez v5, :cond_a

    .line 908
    iput v5, p2, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$AnchorInfo;->mOffset:I

    .line 909
    return v4

    .line 912
    :cond_a
    iput v3, p2, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$AnchorInfo;->mOffset:I

    .line 913
    .end local v1    # "childSize":I
    .end local v2    # "startGap":I
    .end local v5    # "endGap":I
    goto :goto_5

    .line 916
    :cond_b
    iget v2, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mPendingScrollPosition:I

    iput v2, p2, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$AnchorInfo;->mPosition:I

    .line 917
    iget v2, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mPendingScrollPositionOffset:I

    if-ne v2, v3, :cond_d

    .line 918
    iget v2, p2, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$AnchorInfo;->mPosition:I

    invoke-direct {p0, v2}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->calculateScrollDirectionForPosition(I)I

    move-result v2

    .line 920
    .local v2, "position":I
    if-ne v2, v4, :cond_c

    const/4 v1, 0x1

    :cond_c
    iput-boolean v1, p2, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$AnchorInfo;->mLayoutFromEnd:Z

    .line 921
    invoke-virtual {p2}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$AnchorInfo;->assignCoordinateFromPadding()V

    .line 922
    .end local v2    # "position":I
    goto :goto_4

    .line 923
    :cond_d
    invoke-virtual {p2, v2}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$AnchorInfo;->assignCoordinateFromPadding(I)V

    .line 925
    :goto_4
    iput-boolean v4, p2, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$AnchorInfo;->mInvalidateOffsets:Z

    .line 927
    .end local v0    # "child":Landroid/view/View;
    :goto_5
    nop

    .line 931
    :goto_6
    return v4

    .line 862
    :cond_e
    :goto_7
    iput v2, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mPendingScrollPosition:I

    .line 863
    iput v3, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mPendingScrollPositionOffset:I

    .line 864
    return v1

    .line 858
    :cond_f
    :goto_8
    return v1
.end method

.method updateAnchorInfoForLayout(Landroidx/recyclerview/widget/RecyclerView$State;Landroidx/recyclerview/widget/StaggeredGridLayoutManager$AnchorInfo;)V
    .locals 1
    .param p1, "state"    # Landroidx/recyclerview/widget/RecyclerView$State;
    .param p2, "anchorInfo"    # Landroidx/recyclerview/widget/StaggeredGridLayoutManager$AnchorInfo;

    .line 830
    invoke-virtual {p0, p1, p2}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->updateAnchorFromPendingData(Landroidx/recyclerview/widget/RecyclerView$State;Landroidx/recyclerview/widget/StaggeredGridLayoutManager$AnchorInfo;)Z

    move-result v0

    if-eqz v0, :cond_0

    .line 831
    return-void

    .line 833
    :cond_0
    invoke-direct {p0, p1, p2}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->updateAnchorFromChildren(Landroidx/recyclerview/widget/RecyclerView$State;Landroidx/recyclerview/widget/StaggeredGridLayoutManager$AnchorInfo;)Z

    move-result v0

    if-eqz v0, :cond_1

    .line 834
    return-void

    .line 839
    :cond_1
    invoke-virtual {p2}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$AnchorInfo;->assignCoordinateFromPadding()V

    .line 840
    const/4 v0, 0x0

    iput v0, p2, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$AnchorInfo;->mPosition:I

    .line 841
    return-void
.end method

.method updateMeasureSpecs(I)V
    .locals 1
    .param p1, "totalSpace"    # I

    .line 935
    iget v0, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mSpanCount:I

    div-int v0, p1, v0

    iput v0, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mSizePerSpan:I

    .line 937
    iget-object v0, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mSecondaryOrientation:Landroidx/recyclerview/widget/OrientationHelper;

    .line 938
    invoke-virtual {v0}, Landroidx/recyclerview/widget/OrientationHelper;->getMode()I

    move-result v0

    .line 937
    invoke-static {p1, v0}, Landroid/view/View$MeasureSpec;->makeMeasureSpec(II)I

    move-result v0

    iput v0, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager;->mFullSizeSpec:I

    .line 939
    return-void
.end method
