.class Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup;
.super Ljava/lang/Object;
.source "StaggeredGridLayoutManager.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Landroidx/recyclerview/widget/StaggeredGridLayoutManager;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x8
    name = "LazySpanLookup"
.end annotation

.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup$FullSpanItem;
    }
.end annotation


# static fields
.field private static final MIN_SIZE:I = 0xa


# instance fields
.field mData:[I

.field mFullSpanItems:Ljava/util/List;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/List<",
            "Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup$FullSpanItem;",
            ">;"
        }
    .end annotation
.end field


# direct methods
.method constructor <init>()V
    .locals 0

    .line 2812
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method

.method private invalidateFullSpansAfter(I)I
    .locals 7
    .param p1, "position"    # I

    .line 2957
    iget-object v0, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup;->mFullSpanItems:Ljava/util/List;

    const/4 v1, -0x1

    if-nez v0, :cond_0

    .line 2958
    return v1

    .line 2960
    :cond_0
    invoke-virtual {p0, p1}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup;->getFullSpanItem(I)Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup$FullSpanItem;

    move-result-object v0

    .line 2962
    .local v0, "item":Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup$FullSpanItem;
    if-eqz v0, :cond_1

    .line 2963
    iget-object v2, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup;->mFullSpanItems:Ljava/util/List;

    invoke-interface {v2, v0}, Ljava/util/List;->remove(Ljava/lang/Object;)Z

    .line 2965
    :cond_1
    const/4 v2, -0x1

    .line 2966
    .local v2, "nextFsiIndex":I
    iget-object v3, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup;->mFullSpanItems:Ljava/util/List;

    invoke-interface {v3}, Ljava/util/List;->size()I

    move-result v3

    .line 2967
    .local v3, "count":I
    const/4 v4, 0x0

    .local v4, "i":I
    :goto_0
    if-ge v4, v3, :cond_3

    .line 2968
    iget-object v5, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup;->mFullSpanItems:Ljava/util/List;

    invoke-interface {v5, v4}, Ljava/util/List;->get(I)Ljava/lang/Object;

    move-result-object v5

    check-cast v5, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup$FullSpanItem;

    .line 2969
    .local v5, "fsi":Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup$FullSpanItem;
    iget v6, v5, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup$FullSpanItem;->mPosition:I

    if-lt v6, p1, :cond_2

    .line 2970
    move v2, v4

    .line 2971
    goto :goto_1

    .line 2967
    .end local v5    # "fsi":Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup$FullSpanItem;
    :cond_2
    add-int/lit8 v4, v4, 0x1

    goto :goto_0

    .line 2974
    .end local v4    # "i":I
    :cond_3
    :goto_1
    if-eq v2, v1, :cond_4

    .line 2975
    iget-object v1, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup;->mFullSpanItems:Ljava/util/List;

    invoke-interface {v1, v2}, Ljava/util/List;->get(I)Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup$FullSpanItem;

    .line 2976
    .local v1, "fsi":Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup$FullSpanItem;
    iget-object v4, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup;->mFullSpanItems:Ljava/util/List;

    invoke-interface {v4, v2}, Ljava/util/List;->remove(I)Ljava/lang/Object;

    .line 2977
    iget v4, v1, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup$FullSpanItem;->mPosition:I

    return v4

    .line 2979
    .end local v1    # "fsi":Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup$FullSpanItem;
    :cond_4
    return v1
.end method

.method private offsetFullSpansForAddition(II)V
    .locals 3
    .param p1, "positionStart"    # I
    .param p2, "itemCount"    # I

    .line 2940
    iget-object v0, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup;->mFullSpanItems:Ljava/util/List;

    if-nez v0, :cond_0

    .line 2941
    return-void

    .line 2943
    :cond_0
    invoke-interface {v0}, Ljava/util/List;->size()I

    move-result v0

    add-int/lit8 v0, v0, -0x1

    .local v0, "i":I
    :goto_0
    if-ltz v0, :cond_2

    .line 2944
    iget-object v1, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup;->mFullSpanItems:Ljava/util/List;

    invoke-interface {v1, v0}, Ljava/util/List;->get(I)Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup$FullSpanItem;

    .line 2945
    .local v1, "fsi":Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup$FullSpanItem;
    iget v2, v1, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup$FullSpanItem;->mPosition:I

    if-ge v2, p1, :cond_1

    .line 2946
    goto :goto_1

    .line 2948
    :cond_1
    iget v2, v1, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup$FullSpanItem;->mPosition:I

    add-int/2addr v2, p2

    iput v2, v1, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup$FullSpanItem;->mPosition:I

    .line 2943
    .end local v1    # "fsi":Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup$FullSpanItem;
    :goto_1
    add-int/lit8 v0, v0, -0x1

    goto :goto_0

    .line 2950
    .end local v0    # "i":I
    :cond_2
    return-void
.end method

.method private offsetFullSpansForRemoval(II)V
    .locals 4
    .param p1, "positionStart"    # I
    .param p2, "itemCount"    # I

    .line 2910
    iget-object v0, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup;->mFullSpanItems:Ljava/util/List;

    if-nez v0, :cond_0

    .line 2911
    return-void

    .line 2913
    :cond_0
    add-int v1, p1, p2

    .line 2914
    .local v1, "end":I
    invoke-interface {v0}, Ljava/util/List;->size()I

    move-result v0

    add-int/lit8 v0, v0, -0x1

    .local v0, "i":I
    :goto_0
    if-ltz v0, :cond_3

    .line 2915
    iget-object v2, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup;->mFullSpanItems:Ljava/util/List;

    invoke-interface {v2, v0}, Ljava/util/List;->get(I)Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup$FullSpanItem;

    .line 2916
    .local v2, "fsi":Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup$FullSpanItem;
    iget v3, v2, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup$FullSpanItem;->mPosition:I

    if-ge v3, p1, :cond_1

    .line 2917
    goto :goto_1

    .line 2919
    :cond_1
    iget v3, v2, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup$FullSpanItem;->mPosition:I

    if-ge v3, v1, :cond_2

    .line 2920
    iget-object v3, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup;->mFullSpanItems:Ljava/util/List;

    invoke-interface {v3, v0}, Ljava/util/List;->remove(I)Ljava/lang/Object;

    goto :goto_1

    .line 2922
    :cond_2
    iget v3, v2, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup$FullSpanItem;->mPosition:I

    sub-int/2addr v3, p2

    iput v3, v2, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup$FullSpanItem;->mPosition:I

    .line 2914
    .end local v2    # "fsi":Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup$FullSpanItem;
    :goto_1
    add-int/lit8 v0, v0, -0x1

    goto :goto_0

    .line 2925
    .end local v0    # "i":I
    :cond_3
    return-void
.end method


# virtual methods
.method public addFullSpanItem(Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup$FullSpanItem;)V
    .locals 5
    .param p1, "fullSpanItem"    # Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup$FullSpanItem;

    .line 2983
    iget-object v0, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup;->mFullSpanItems:Ljava/util/List;

    if-nez v0, :cond_0

    .line 2984
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    iput-object v0, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup;->mFullSpanItems:Ljava/util/List;

    .line 2986
    :cond_0
    iget-object v0, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup;->mFullSpanItems:Ljava/util/List;

    invoke-interface {v0}, Ljava/util/List;->size()I

    move-result v0

    .line 2987
    .local v0, "size":I
    const/4 v1, 0x0

    .local v1, "i":I
    :goto_0
    if-ge v1, v0, :cond_3

    .line 2988
    iget-object v2, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup;->mFullSpanItems:Ljava/util/List;

    invoke-interface {v2, v1}, Ljava/util/List;->get(I)Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup$FullSpanItem;

    .line 2989
    .local v2, "other":Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup$FullSpanItem;
    iget v3, v2, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup$FullSpanItem;->mPosition:I

    iget v4, p1, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup$FullSpanItem;->mPosition:I

    if-ne v3, v4, :cond_1

    .line 2993
    iget-object v3, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup;->mFullSpanItems:Ljava/util/List;

    invoke-interface {v3, v1}, Ljava/util/List;->remove(I)Ljava/lang/Object;

    .line 2996
    :cond_1
    iget v3, v2, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup$FullSpanItem;->mPosition:I

    iget v4, p1, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup$FullSpanItem;->mPosition:I

    if-lt v3, v4, :cond_2

    .line 2997
    iget-object v3, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup;->mFullSpanItems:Ljava/util/List;

    invoke-interface {v3, v1, p1}, Ljava/util/List;->add(ILjava/lang/Object;)V

    .line 2998
    return-void

    .line 2987
    .end local v2    # "other":Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup$FullSpanItem;
    :cond_2
    add-int/lit8 v1, v1, 0x1

    goto :goto_0

    .line 3002
    .end local v1    # "i":I
    :cond_3
    iget-object v1, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup;->mFullSpanItems:Ljava/util/List;

    invoke-interface {v1, p1}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    .line 3003
    return-void
.end method

.method clear()V
    .locals 2

    .line 2891
    iget-object v0, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup;->mData:[I

    if-eqz v0, :cond_0

    .line 2892
    const/4 v1, -0x1

    invoke-static {v0, v1}, Ljava/util/Arrays;->fill([II)V

    .line 2894
    :cond_0
    const/4 v0, 0x0

    iput-object v0, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup;->mFullSpanItems:Ljava/util/List;

    .line 2895
    return-void
.end method

.method ensureSize(I)V
    .locals 5
    .param p1, "position"    # I

    .line 2879
    iget-object v0, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup;->mData:[I

    const/4 v1, -0x1

    if-nez v0, :cond_0

    .line 2880
    const/16 v0, 0xa

    invoke-static {p1, v0}, Ljava/lang/Math;->max(II)I

    move-result v0

    add-int/lit8 v0, v0, 0x1

    new-array v0, v0, [I

    iput-object v0, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup;->mData:[I

    .line 2881
    invoke-static {v0, v1}, Ljava/util/Arrays;->fill([II)V

    goto :goto_0

    .line 2882
    :cond_0
    array-length v0, v0

    if-lt p1, v0, :cond_1

    .line 2883
    iget-object v0, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup;->mData:[I

    .line 2884
    .local v0, "old":[I
    invoke-virtual {p0, p1}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup;->sizeForPosition(I)I

    move-result v2

    new-array v2, v2, [I

    iput-object v2, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup;->mData:[I

    .line 2885
    array-length v3, v0

    const/4 v4, 0x0

    invoke-static {v0, v4, v2, v4, v3}, Ljava/lang/System;->arraycopy(Ljava/lang/Object;ILjava/lang/Object;II)V

    .line 2886
    iget-object v2, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup;->mData:[I

    array-length v3, v0

    array-length v4, v2

    invoke-static {v2, v3, v4, v1}, Ljava/util/Arrays;->fill([IIII)V

    .line 2888
    .end local v0    # "old":[I
    :cond_1
    :goto_0
    return-void
.end method

.method forceInvalidateAfter(I)I
    .locals 3
    .param p1, "position"    # I

    .line 2823
    iget-object v0, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup;->mFullSpanItems:Ljava/util/List;

    if-eqz v0, :cond_1

    .line 2824
    invoke-interface {v0}, Ljava/util/List;->size()I

    move-result v0

    add-int/lit8 v0, v0, -0x1

    .local v0, "i":I
    :goto_0
    if-ltz v0, :cond_1

    .line 2825
    iget-object v1, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup;->mFullSpanItems:Ljava/util/List;

    invoke-interface {v1, v0}, Ljava/util/List;->get(I)Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup$FullSpanItem;

    .line 2826
    .local v1, "fsi":Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup$FullSpanItem;
    iget v2, v1, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup$FullSpanItem;->mPosition:I

    if-lt v2, p1, :cond_0

    .line 2827
    iget-object v2, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup;->mFullSpanItems:Ljava/util/List;

    invoke-interface {v2, v0}, Ljava/util/List;->remove(I)Ljava/lang/Object;

    .line 2824
    .end local v1    # "fsi":Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup$FullSpanItem;
    :cond_0
    add-int/lit8 v0, v0, -0x1

    goto :goto_0

    .line 2831
    .end local v0    # "i":I
    :cond_1
    invoke-virtual {p0, p1}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup;->invalidateAfter(I)I

    move-result v0

    return v0
.end method

.method public getFirstFullSpanItemInRange(IIIZ)Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup$FullSpanItem;
    .locals 5
    .param p1, "minPos"    # I
    .param p2, "maxPos"    # I
    .param p3, "gapDir"    # I
    .param p4, "hasUnwantedGapAfter"    # Z

    .line 3027
    iget-object v0, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup;->mFullSpanItems:Ljava/util/List;

    const/4 v1, 0x0

    if-nez v0, :cond_0

    .line 3028
    return-object v1

    .line 3030
    :cond_0
    invoke-interface {v0}, Ljava/util/List;->size()I

    move-result v0

    .line 3031
    .local v0, "limit":I
    const/4 v2, 0x0

    .local v2, "i":I
    :goto_0
    if-ge v2, v0, :cond_4

    .line 3032
    iget-object v3, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup;->mFullSpanItems:Ljava/util/List;

    invoke-interface {v3, v2}, Ljava/util/List;->get(I)Ljava/lang/Object;

    move-result-object v3

    check-cast v3, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup$FullSpanItem;

    .line 3033
    .local v3, "fsi":Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup$FullSpanItem;
    iget v4, v3, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup$FullSpanItem;->mPosition:I

    if-lt v4, p2, :cond_1

    .line 3034
    return-object v1

    .line 3036
    :cond_1
    iget v4, v3, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup$FullSpanItem;->mPosition:I

    if-lt v4, p1, :cond_3

    if-eqz p3, :cond_2

    iget v4, v3, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup$FullSpanItem;->mGapDir:I

    if-eq v4, p3, :cond_2

    if-eqz p4, :cond_3

    iget-boolean v4, v3, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup$FullSpanItem;->mHasUnwantedGapAfter:Z

    if-eqz v4, :cond_3

    .line 3039
    :cond_2
    return-object v3

    .line 3031
    .end local v3    # "fsi":Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup$FullSpanItem;
    :cond_3
    add-int/lit8 v2, v2, 0x1

    goto :goto_0

    .line 3042
    .end local v2    # "i":I
    :cond_4
    return-object v1
.end method

.method public getFullSpanItem(I)Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup$FullSpanItem;
    .locals 4
    .param p1, "position"    # I

    .line 3006
    iget-object v0, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup;->mFullSpanItems:Ljava/util/List;

    const/4 v1, 0x0

    if-nez v0, :cond_0

    .line 3007
    return-object v1

    .line 3009
    :cond_0
    invoke-interface {v0}, Ljava/util/List;->size()I

    move-result v0

    add-int/lit8 v0, v0, -0x1

    .local v0, "i":I
    :goto_0
    if-ltz v0, :cond_2

    .line 3010
    iget-object v2, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup;->mFullSpanItems:Ljava/util/List;

    invoke-interface {v2, v0}, Ljava/util/List;->get(I)Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup$FullSpanItem;

    .line 3011
    .local v2, "fsi":Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup$FullSpanItem;
    iget v3, v2, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup$FullSpanItem;->mPosition:I

    if-ne v3, p1, :cond_1

    .line 3012
    return-object v2

    .line 3009
    .end local v2    # "fsi":Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup$FullSpanItem;
    :cond_1
    add-int/lit8 v0, v0, -0x1

    goto :goto_0

    .line 3015
    .end local v0    # "i":I
    :cond_2
    return-object v1
.end method

.method getSpan(I)I
    .locals 2
    .param p1, "position"    # I

    .line 2858
    iget-object v0, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup;->mData:[I

    if-eqz v0, :cond_1

    array-length v1, v0

    if-lt p1, v1, :cond_0

    goto :goto_0

    .line 2861
    :cond_0
    aget v0, v0, p1

    return v0

    .line 2859
    :cond_1
    :goto_0
    const/4 v0, -0x1

    return v0
.end method

.method invalidateAfter(I)I
    .locals 4
    .param p1, "position"    # I

    .line 2838
    iget-object v0, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup;->mData:[I

    const/4 v1, -0x1

    if-nez v0, :cond_0

    .line 2839
    return v1

    .line 2841
    :cond_0
    array-length v0, v0

    if-lt p1, v0, :cond_1

    .line 2842
    return v1

    .line 2844
    :cond_1
    invoke-direct {p0, p1}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup;->invalidateFullSpansAfter(I)I

    move-result v0

    .line 2845
    .local v0, "endPosition":I
    if-ne v0, v1, :cond_2

    .line 2846
    iget-object v2, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup;->mData:[I

    array-length v3, v2

    invoke-static {v2, p1, v3, v1}, Ljava/util/Arrays;->fill([IIII)V

    .line 2847
    iget-object v1, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup;->mData:[I

    array-length v1, v1

    return v1

    .line 2851
    :cond_2
    add-int/lit8 v2, v0, 0x1

    iget-object v3, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup;->mData:[I

    array-length v3, v3

    invoke-static {v2, v3}, Ljava/lang/Math;->min(II)I

    move-result v2

    .line 2852
    .local v2, "invalidateToIndex":I
    iget-object v3, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup;->mData:[I

    invoke-static {v3, p1, v2, v1}, Ljava/util/Arrays;->fill([IIII)V

    .line 2853
    return v2
.end method

.method offsetForAddition(II)V
    .locals 3
    .param p1, "positionStart"    # I
    .param p2, "itemCount"    # I

    .line 2928
    iget-object v0, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup;->mData:[I

    if-eqz v0, :cond_1

    array-length v0, v0

    if-lt p1, v0, :cond_0

    goto :goto_0

    .line 2931
    :cond_0
    add-int v0, p1, p2

    invoke-virtual {p0, v0}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup;->ensureSize(I)V

    .line 2932
    iget-object v0, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup;->mData:[I

    add-int v1, p1, p2

    array-length v2, v0

    sub-int/2addr v2, p1

    sub-int/2addr v2, p2

    invoke-static {v0, p1, v0, v1, v2}, Ljava/lang/System;->arraycopy(Ljava/lang/Object;ILjava/lang/Object;II)V

    .line 2934
    iget-object v0, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup;->mData:[I

    add-int v1, p1, p2

    const/4 v2, -0x1

    invoke-static {v0, p1, v1, v2}, Ljava/util/Arrays;->fill([IIII)V

    .line 2936
    invoke-direct {p0, p1, p2}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup;->offsetFullSpansForAddition(II)V

    .line 2937
    return-void

    .line 2929
    :cond_1
    :goto_0
    return-void
.end method

.method offsetForRemoval(II)V
    .locals 4
    .param p1, "positionStart"    # I
    .param p2, "itemCount"    # I

    .line 2898
    iget-object v0, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup;->mData:[I

    if-eqz v0, :cond_1

    array-length v0, v0

    if-lt p1, v0, :cond_0

    goto :goto_0

    .line 2901
    :cond_0
    add-int v0, p1, p2

    invoke-virtual {p0, v0}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup;->ensureSize(I)V

    .line 2902
    iget-object v0, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup;->mData:[I

    add-int v1, p1, p2

    array-length v2, v0

    sub-int/2addr v2, p1

    sub-int/2addr v2, p2

    invoke-static {v0, v1, v0, p1, v2}, Ljava/lang/System;->arraycopy(Ljava/lang/Object;ILjava/lang/Object;II)V

    .line 2904
    iget-object v0, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup;->mData:[I

    array-length v1, v0

    sub-int/2addr v1, p2

    array-length v2, v0

    const/4 v3, -0x1

    invoke-static {v0, v1, v2, v3}, Ljava/util/Arrays;->fill([IIII)V

    .line 2906
    invoke-direct {p0, p1, p2}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup;->offsetFullSpansForRemoval(II)V

    .line 2907
    return-void

    .line 2899
    :cond_1
    :goto_0
    return-void
.end method

.method setSpan(ILandroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;)V
    .locals 2
    .param p1, "position"    # I
    .param p2, "span"    # Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;

    .line 2866
    invoke-virtual {p0, p1}, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup;->ensureSize(I)V

    .line 2867
    iget-object v0, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup;->mData:[I

    iget v1, p2, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$Span;->mIndex:I

    aput v1, v0, p1

    .line 2868
    return-void
.end method

.method sizeForPosition(I)I
    .locals 1
    .param p1, "position"    # I

    .line 2871
    iget-object v0, p0, Landroidx/recyclerview/widget/StaggeredGridLayoutManager$LazySpanLookup;->mData:[I

    array-length v0, v0

    .line 2872
    .local v0, "len":I
    :goto_0
    if-gt v0, p1, :cond_0

    .line 2873
    mul-int/lit8 v0, v0, 0x2

    goto :goto_0

    .line 2875
    :cond_0
    return v0
.end method
