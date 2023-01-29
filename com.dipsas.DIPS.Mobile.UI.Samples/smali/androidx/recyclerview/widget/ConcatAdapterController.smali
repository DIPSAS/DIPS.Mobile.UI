.class Landroidx/recyclerview/widget/ConcatAdapterController;
.super Ljava/lang/Object;
.source "ConcatAdapterController.java"

# interfaces
.implements Landroidx/recyclerview/widget/NestedAdapterWrapper$Callback;


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Landroidx/recyclerview/widget/ConcatAdapterController$WrapperAndLocalPosition;
    }
.end annotation


# instance fields
.field private mAttachedRecyclerViews:Ljava/util/List;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/List<",
            "Ljava/lang/ref/WeakReference<",
            "Landroidx/recyclerview/widget/RecyclerView;",
            ">;>;"
        }
    .end annotation
.end field

.field private final mBinderLookup:Ljava/util/IdentityHashMap;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/IdentityHashMap<",
            "Landroidx/recyclerview/widget/RecyclerView$ViewHolder;",
            "Landroidx/recyclerview/widget/NestedAdapterWrapper;",
            ">;"
        }
    .end annotation
.end field

.field private final mConcatAdapter:Landroidx/recyclerview/widget/ConcatAdapter;

.field private mReusableHolder:Landroidx/recyclerview/widget/ConcatAdapterController$WrapperAndLocalPosition;

.field private final mStableIdMode:Landroidx/recyclerview/widget/ConcatAdapter$Config$StableIdMode;

.field private final mStableIdStorage:Landroidx/recyclerview/widget/StableIdStorage;

.field private final mViewTypeStorage:Landroidx/recyclerview/widget/ViewTypeStorage;

.field private mWrappers:Ljava/util/List;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/List<",
            "Landroidx/recyclerview/widget/NestedAdapterWrapper;",
            ">;"
        }
    .end annotation
.end field


# direct methods
.method constructor <init>(Landroidx/recyclerview/widget/ConcatAdapter;Landroidx/recyclerview/widget/ConcatAdapter$Config;)V
    .locals 2
    .param p1, "concatAdapter"    # Landroidx/recyclerview/widget/ConcatAdapter;
    .param p2, "config"    # Landroidx/recyclerview/widget/ConcatAdapter$Config;

    .line 84
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 60
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    iput-object v0, p0, Landroidx/recyclerview/widget/ConcatAdapterController;->mAttachedRecyclerViews:Ljava/util/List;

    .line 66
    new-instance v0, Ljava/util/IdentityHashMap;

    invoke-direct {v0}, Ljava/util/IdentityHashMap;-><init>()V

    iput-object v0, p0, Landroidx/recyclerview/widget/ConcatAdapterController;->mBinderLookup:Ljava/util/IdentityHashMap;

    .line 69
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    iput-object v0, p0, Landroidx/recyclerview/widget/ConcatAdapterController;->mWrappers:Ljava/util/List;

    .line 72
    new-instance v0, Landroidx/recyclerview/widget/ConcatAdapterController$WrapperAndLocalPosition;

    invoke-direct {v0}, Landroidx/recyclerview/widget/ConcatAdapterController$WrapperAndLocalPosition;-><init>()V

    iput-object v0, p0, Landroidx/recyclerview/widget/ConcatAdapterController;->mReusableHolder:Landroidx/recyclerview/widget/ConcatAdapterController$WrapperAndLocalPosition;

    .line 85
    iput-object p1, p0, Landroidx/recyclerview/widget/ConcatAdapterController;->mConcatAdapter:Landroidx/recyclerview/widget/ConcatAdapter;

    .line 88
    iget-boolean v0, p2, Landroidx/recyclerview/widget/ConcatAdapter$Config;->isolateViewTypes:Z

    if-eqz v0, :cond_0

    .line 89
    new-instance v0, Landroidx/recyclerview/widget/ViewTypeStorage$IsolatedViewTypeStorage;

    invoke-direct {v0}, Landroidx/recyclerview/widget/ViewTypeStorage$IsolatedViewTypeStorage;-><init>()V

    iput-object v0, p0, Landroidx/recyclerview/widget/ConcatAdapterController;->mViewTypeStorage:Landroidx/recyclerview/widget/ViewTypeStorage;

    goto :goto_0

    .line 91
    :cond_0
    new-instance v0, Landroidx/recyclerview/widget/ViewTypeStorage$SharedIdRangeViewTypeStorage;

    invoke-direct {v0}, Landroidx/recyclerview/widget/ViewTypeStorage$SharedIdRangeViewTypeStorage;-><init>()V

    iput-object v0, p0, Landroidx/recyclerview/widget/ConcatAdapterController;->mViewTypeStorage:Landroidx/recyclerview/widget/ViewTypeStorage;

    .line 95
    :goto_0
    iget-object v0, p2, Landroidx/recyclerview/widget/ConcatAdapter$Config;->stableIdMode:Landroidx/recyclerview/widget/ConcatAdapter$Config$StableIdMode;

    iput-object v0, p0, Landroidx/recyclerview/widget/ConcatAdapterController;->mStableIdMode:Landroidx/recyclerview/widget/ConcatAdapter$Config$StableIdMode;

    .line 96
    iget-object v0, p2, Landroidx/recyclerview/widget/ConcatAdapter$Config;->stableIdMode:Landroidx/recyclerview/widget/ConcatAdapter$Config$StableIdMode;

    sget-object v1, Landroidx/recyclerview/widget/ConcatAdapter$Config$StableIdMode;->NO_STABLE_IDS:Landroidx/recyclerview/widget/ConcatAdapter$Config$StableIdMode;

    if-ne v0, v1, :cond_1

    .line 97
    new-instance v0, Landroidx/recyclerview/widget/StableIdStorage$NoStableIdStorage;

    invoke-direct {v0}, Landroidx/recyclerview/widget/StableIdStorage$NoStableIdStorage;-><init>()V

    iput-object v0, p0, Landroidx/recyclerview/widget/ConcatAdapterController;->mStableIdStorage:Landroidx/recyclerview/widget/StableIdStorage;

    goto :goto_1

    .line 98
    :cond_1
    iget-object v0, p2, Landroidx/recyclerview/widget/ConcatAdapter$Config;->stableIdMode:Landroidx/recyclerview/widget/ConcatAdapter$Config$StableIdMode;

    sget-object v1, Landroidx/recyclerview/widget/ConcatAdapter$Config$StableIdMode;->ISOLATED_STABLE_IDS:Landroidx/recyclerview/widget/ConcatAdapter$Config$StableIdMode;

    if-ne v0, v1, :cond_2

    .line 99
    new-instance v0, Landroidx/recyclerview/widget/StableIdStorage$IsolatedStableIdStorage;

    invoke-direct {v0}, Landroidx/recyclerview/widget/StableIdStorage$IsolatedStableIdStorage;-><init>()V

    iput-object v0, p0, Landroidx/recyclerview/widget/ConcatAdapterController;->mStableIdStorage:Landroidx/recyclerview/widget/StableIdStorage;

    goto :goto_1

    .line 100
    :cond_2
    iget-object v0, p2, Landroidx/recyclerview/widget/ConcatAdapter$Config;->stableIdMode:Landroidx/recyclerview/widget/ConcatAdapter$Config$StableIdMode;

    sget-object v1, Landroidx/recyclerview/widget/ConcatAdapter$Config$StableIdMode;->SHARED_STABLE_IDS:Landroidx/recyclerview/widget/ConcatAdapter$Config$StableIdMode;

    if-ne v0, v1, :cond_3

    .line 101
    new-instance v0, Landroidx/recyclerview/widget/StableIdStorage$SharedPoolStableIdStorage;

    invoke-direct {v0}, Landroidx/recyclerview/widget/StableIdStorage$SharedPoolStableIdStorage;-><init>()V

    iput-object v0, p0, Landroidx/recyclerview/widget/ConcatAdapterController;->mStableIdStorage:Landroidx/recyclerview/widget/StableIdStorage;

    .line 105
    :goto_1
    return-void

    .line 103
    :cond_3
    new-instance v0, Ljava/lang/IllegalArgumentException;

    const-string v1, "unknown stable id mode"

    invoke-direct {v0, v1}, Ljava/lang/IllegalArgumentException;-><init>(Ljava/lang/String;)V

    throw v0
.end method

.method private calculateAndUpdateStateRestorationPolicy()V
    .locals 2

    .line 286
    invoke-direct {p0}, Landroidx/recyclerview/widget/ConcatAdapterController;->computeStateRestorationPolicy()Landroidx/recyclerview/widget/RecyclerView$Adapter$StateRestorationPolicy;

    move-result-object v0

    .line 287
    .local v0, "newPolicy":Landroidx/recyclerview/widget/RecyclerView$Adapter$StateRestorationPolicy;
    iget-object v1, p0, Landroidx/recyclerview/widget/ConcatAdapterController;->mConcatAdapter:Landroidx/recyclerview/widget/ConcatAdapter;

    invoke-virtual {v1}, Landroidx/recyclerview/widget/ConcatAdapter;->getStateRestorationPolicy()Landroidx/recyclerview/widget/RecyclerView$Adapter$StateRestorationPolicy;

    move-result-object v1

    if-eq v0, v1, :cond_0

    .line 288
    iget-object v1, p0, Landroidx/recyclerview/widget/ConcatAdapterController;->mConcatAdapter:Landroidx/recyclerview/widget/ConcatAdapter;

    invoke-virtual {v1, v0}, Landroidx/recyclerview/widget/ConcatAdapter;->internalSetStateRestorationPolicy(Landroidx/recyclerview/widget/RecyclerView$Adapter$StateRestorationPolicy;)V

    .line 290
    :cond_0
    return-void
.end method

.method private computeStateRestorationPolicy()Landroidx/recyclerview/widget/RecyclerView$Adapter$StateRestorationPolicy;
    .locals 4

    .line 293
    iget-object v0, p0, Landroidx/recyclerview/widget/ConcatAdapterController;->mWrappers:Ljava/util/List;

    invoke-interface {v0}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v0

    :goto_0
    invoke-interface {v0}, Ljava/util/Iterator;->hasNext()Z

    move-result v1

    if-eqz v1, :cond_2

    invoke-interface {v0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Landroidx/recyclerview/widget/NestedAdapterWrapper;

    .line 294
    .local v1, "wrapper":Landroidx/recyclerview/widget/NestedAdapterWrapper;
    iget-object v2, v1, Landroidx/recyclerview/widget/NestedAdapterWrapper;->adapter:Landroidx/recyclerview/widget/RecyclerView$Adapter;

    .line 295
    invoke-virtual {v2}, Landroidx/recyclerview/widget/RecyclerView$Adapter;->getStateRestorationPolicy()Landroidx/recyclerview/widget/RecyclerView$Adapter$StateRestorationPolicy;

    move-result-object v2

    .line 296
    .local v2, "strategy":Landroidx/recyclerview/widget/RecyclerView$Adapter$StateRestorationPolicy;
    sget-object v3, Landroidx/recyclerview/widget/RecyclerView$Adapter$StateRestorationPolicy;->PREVENT:Landroidx/recyclerview/widget/RecyclerView$Adapter$StateRestorationPolicy;

    if-ne v2, v3, :cond_0

    .line 298
    sget-object v0, Landroidx/recyclerview/widget/RecyclerView$Adapter$StateRestorationPolicy;->PREVENT:Landroidx/recyclerview/widget/RecyclerView$Adapter$StateRestorationPolicy;

    return-object v0

    .line 299
    :cond_0
    sget-object v3, Landroidx/recyclerview/widget/RecyclerView$Adapter$StateRestorationPolicy;->PREVENT_WHEN_EMPTY:Landroidx/recyclerview/widget/RecyclerView$Adapter$StateRestorationPolicy;

    if-ne v2, v3, :cond_1

    invoke-virtual {v1}, Landroidx/recyclerview/widget/NestedAdapterWrapper;->getCachedItemCount()I

    move-result v3

    if-nez v3, :cond_1

    .line 301
    sget-object v0, Landroidx/recyclerview/widget/RecyclerView$Adapter$StateRestorationPolicy;->PREVENT:Landroidx/recyclerview/widget/RecyclerView$Adapter$StateRestorationPolicy;

    return-object v0

    .line 303
    .end local v1    # "wrapper":Landroidx/recyclerview/widget/NestedAdapterWrapper;
    .end local v2    # "strategy":Landroidx/recyclerview/widget/RecyclerView$Adapter$StateRestorationPolicy;
    :cond_1
    goto :goto_0

    .line 304
    :cond_2
    sget-object v0, Landroidx/recyclerview/widget/RecyclerView$Adapter$StateRestorationPolicy;->ALLOW:Landroidx/recyclerview/widget/RecyclerView$Adapter$StateRestorationPolicy;

    return-object v0
.end method

.method private countItemsBefore(Landroidx/recyclerview/widget/NestedAdapterWrapper;)I
    .locals 4
    .param p1, "wrapper"    # Landroidx/recyclerview/widget/NestedAdapterWrapper;

    .line 204
    const/4 v0, 0x0

    .line 205
    .local v0, "count":I
    iget-object v1, p0, Landroidx/recyclerview/widget/ConcatAdapterController;->mWrappers:Ljava/util/List;

    invoke-interface {v1}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v1

    :goto_0
    invoke-interface {v1}, Ljava/util/Iterator;->hasNext()Z

    move-result v2

    if-eqz v2, :cond_0

    invoke-interface {v1}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Landroidx/recyclerview/widget/NestedAdapterWrapper;

    .line 206
    .local v2, "item":Landroidx/recyclerview/widget/NestedAdapterWrapper;
    if-eq v2, p1, :cond_0

    .line 207
    invoke-virtual {v2}, Landroidx/recyclerview/widget/NestedAdapterWrapper;->getCachedItemCount()I

    move-result v3

    add-int/2addr v0, v3

    .line 211
    .end local v2    # "item":Landroidx/recyclerview/widget/NestedAdapterWrapper;
    goto :goto_0

    .line 212
    :cond_0
    return v0
.end method

.method private findWrapperAndLocalPosition(I)Landroidx/recyclerview/widget/ConcatAdapterController$WrapperAndLocalPosition;
    .locals 5
    .param p1, "globalPosition"    # I

    .line 337
    iget-object v0, p0, Landroidx/recyclerview/widget/ConcatAdapterController;->mReusableHolder:Landroidx/recyclerview/widget/ConcatAdapterController$WrapperAndLocalPosition;

    iget-boolean v0, v0, Landroidx/recyclerview/widget/ConcatAdapterController$WrapperAndLocalPosition;->mInUse:Z

    if-eqz v0, :cond_0

    .line 338
    new-instance v0, Landroidx/recyclerview/widget/ConcatAdapterController$WrapperAndLocalPosition;

    invoke-direct {v0}, Landroidx/recyclerview/widget/ConcatAdapterController$WrapperAndLocalPosition;-><init>()V

    .local v0, "result":Landroidx/recyclerview/widget/ConcatAdapterController$WrapperAndLocalPosition;
    goto :goto_0

    .line 340
    .end local v0    # "result":Landroidx/recyclerview/widget/ConcatAdapterController$WrapperAndLocalPosition;
    :cond_0
    iget-object v0, p0, Landroidx/recyclerview/widget/ConcatAdapterController;->mReusableHolder:Landroidx/recyclerview/widget/ConcatAdapterController$WrapperAndLocalPosition;

    const/4 v1, 0x1

    iput-boolean v1, v0, Landroidx/recyclerview/widget/ConcatAdapterController$WrapperAndLocalPosition;->mInUse:Z

    .line 341
    iget-object v0, p0, Landroidx/recyclerview/widget/ConcatAdapterController;->mReusableHolder:Landroidx/recyclerview/widget/ConcatAdapterController$WrapperAndLocalPosition;

    .line 343
    .restart local v0    # "result":Landroidx/recyclerview/widget/ConcatAdapterController$WrapperAndLocalPosition;
    :goto_0
    move v1, p1

    .line 344
    .local v1, "localPosition":I
    iget-object v2, p0, Landroidx/recyclerview/widget/ConcatAdapterController;->mWrappers:Ljava/util/List;

    invoke-interface {v2}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v2

    :goto_1
    invoke-interface {v2}, Ljava/util/Iterator;->hasNext()Z

    move-result v3

    if-eqz v3, :cond_2

    invoke-interface {v2}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v3

    check-cast v3, Landroidx/recyclerview/widget/NestedAdapterWrapper;

    .line 345
    .local v3, "wrapper":Landroidx/recyclerview/widget/NestedAdapterWrapper;
    invoke-virtual {v3}, Landroidx/recyclerview/widget/NestedAdapterWrapper;->getCachedItemCount()I

    move-result v4

    if-le v4, v1, :cond_1

    .line 346
    iput-object v3, v0, Landroidx/recyclerview/widget/ConcatAdapterController$WrapperAndLocalPosition;->mWrapper:Landroidx/recyclerview/widget/NestedAdapterWrapper;

    .line 347
    iput v1, v0, Landroidx/recyclerview/widget/ConcatAdapterController$WrapperAndLocalPosition;->mLocalPosition:I

    .line 348
    goto :goto_2

    .line 350
    :cond_1
    invoke-virtual {v3}, Landroidx/recyclerview/widget/NestedAdapterWrapper;->getCachedItemCount()I

    move-result v4

    sub-int/2addr v1, v4

    .line 351
    .end local v3    # "wrapper":Landroidx/recyclerview/widget/NestedAdapterWrapper;
    goto :goto_1

    .line 352
    :cond_2
    :goto_2
    iget-object v2, v0, Landroidx/recyclerview/widget/ConcatAdapterController$WrapperAndLocalPosition;->mWrapper:Landroidx/recyclerview/widget/NestedAdapterWrapper;

    if-eqz v2, :cond_3

    .line 355
    return-object v0

    .line 353
    :cond_3
    new-instance v2, Ljava/lang/IllegalArgumentException;

    new-instance v3, Ljava/lang/StringBuilder;

    invoke-direct {v3}, Ljava/lang/StringBuilder;-><init>()V

    const-string v4, "Cannot find wrapper for "

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v3

    invoke-virtual {v3, p1}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    move-result-object v3

    invoke-virtual {v3}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v3

    invoke-direct {v2, v3}, Ljava/lang/IllegalArgumentException;-><init>(Ljava/lang/String;)V

    throw v2
.end method

.method private findWrapperFor(Landroidx/recyclerview/widget/RecyclerView$Adapter;)Landroidx/recyclerview/widget/NestedAdapterWrapper;
    .locals 2
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Landroidx/recyclerview/widget/RecyclerView$Adapter<",
            "Landroidx/recyclerview/widget/RecyclerView$ViewHolder;",
            ">;)",
            "Landroidx/recyclerview/widget/NestedAdapterWrapper;"
        }
    .end annotation

    .line 109
    .local p1, "adapter":Landroidx/recyclerview/widget/RecyclerView$Adapter;, "Landroidx/recyclerview/widget/RecyclerView$Adapter<Landroidx/recyclerview/widget/RecyclerView$ViewHolder;>;"
    invoke-direct {p0, p1}, Landroidx/recyclerview/widget/ConcatAdapterController;->indexOfWrapper(Landroidx/recyclerview/widget/RecyclerView$Adapter;)I

    move-result v0

    .line 110
    .local v0, "index":I
    const/4 v1, -0x1

    if-ne v0, v1, :cond_0

    .line 111
    const/4 v1, 0x0

    return-object v1

    .line 113
    :cond_0
    iget-object v1, p0, Landroidx/recyclerview/widget/ConcatAdapterController;->mWrappers:Ljava/util/List;

    invoke-interface {v1, v0}, Ljava/util/List;->get(I)Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Landroidx/recyclerview/widget/NestedAdapterWrapper;

    return-object v1
.end method

.method private getWrapper(Landroidx/recyclerview/widget/RecyclerView$ViewHolder;)Landroidx/recyclerview/widget/NestedAdapterWrapper;
    .locals 4
    .param p1, "holder"    # Landroidx/recyclerview/widget/RecyclerView$ViewHolder;

    .line 414
    iget-object v0, p0, Landroidx/recyclerview/widget/ConcatAdapterController;->mBinderLookup:Ljava/util/IdentityHashMap;

    invoke-virtual {v0, p1}, Ljava/util/IdentityHashMap;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Landroidx/recyclerview/widget/NestedAdapterWrapper;

    .line 415
    .local v0, "wrapper":Landroidx/recyclerview/widget/NestedAdapterWrapper;
    if-eqz v0, :cond_0

    .line 419
    return-object v0

    .line 416
    :cond_0
    new-instance v1, Ljava/lang/IllegalStateException;

    new-instance v2, Ljava/lang/StringBuilder;

    invoke-direct {v2}, Ljava/lang/StringBuilder;-><init>()V

    const-string v3, "Cannot find wrapper for "

    invoke-virtual {v2, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v2

    invoke-virtual {v2, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    move-result-object v2

    const-string v3, ", seems like it is not bound by this adapter: "

    invoke-virtual {v2, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v2

    invoke-virtual {v2, p0}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    move-result-object v2

    invoke-virtual {v2}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v2

    invoke-direct {v1, v2}, Ljava/lang/IllegalStateException;-><init>(Ljava/lang/String;)V

    throw v1
.end method

.method private indexOfWrapper(Landroidx/recyclerview/widget/RecyclerView$Adapter;)I
    .locals 3
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Landroidx/recyclerview/widget/RecyclerView$Adapter<",
            "Landroidx/recyclerview/widget/RecyclerView$ViewHolder;",
            ">;)I"
        }
    .end annotation

    .line 117
    .local p1, "adapter":Landroidx/recyclerview/widget/RecyclerView$Adapter;, "Landroidx/recyclerview/widget/RecyclerView$Adapter<Landroidx/recyclerview/widget/RecyclerView$ViewHolder;>;"
    iget-object v0, p0, Landroidx/recyclerview/widget/ConcatAdapterController;->mWrappers:Ljava/util/List;

    invoke-interface {v0}, Ljava/util/List;->size()I

    move-result v0

    .line 118
    .local v0, "limit":I
    const/4 v1, 0x0

    .local v1, "i":I
    :goto_0
    if-ge v1, v0, :cond_1

    .line 119
    iget-object v2, p0, Landroidx/recyclerview/widget/ConcatAdapterController;->mWrappers:Ljava/util/List;

    invoke-interface {v2, v1}, Ljava/util/List;->get(I)Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Landroidx/recyclerview/widget/NestedAdapterWrapper;

    iget-object v2, v2, Landroidx/recyclerview/widget/NestedAdapterWrapper;->adapter:Landroidx/recyclerview/widget/RecyclerView$Adapter;

    if-ne v2, p1, :cond_0

    .line 120
    return v1

    .line 118
    :cond_0
    add-int/lit8 v1, v1, 0x1

    goto :goto_0

    .line 123
    .end local v1    # "i":I
    :cond_1
    const/4 v1, -0x1

    return v1
.end method

.method private isAttachedTo(Landroidx/recyclerview/widget/RecyclerView;)Z
    .locals 3
    .param p1, "recyclerView"    # Landroidx/recyclerview/widget/RecyclerView;

    .line 423
    iget-object v0, p0, Landroidx/recyclerview/widget/ConcatAdapterController;->mAttachedRecyclerViews:Ljava/util/List;

    invoke-interface {v0}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v0

    :goto_0
    invoke-interface {v0}, Ljava/util/Iterator;->hasNext()Z

    move-result v1

    if-eqz v1, :cond_1

    invoke-interface {v0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Ljava/lang/ref/WeakReference;

    .line 424
    .local v1, "reference":Ljava/lang/ref/WeakReference;, "Ljava/lang/ref/WeakReference<Landroidx/recyclerview/widget/RecyclerView;>;"
    invoke-virtual {v1}, Ljava/lang/ref/WeakReference;->get()Ljava/lang/Object;

    move-result-object v2

    if-ne v2, p1, :cond_0

    .line 425
    const/4 v0, 0x1

    return v0

    .line 427
    .end local v1    # "reference":Ljava/lang/ref/WeakReference;, "Ljava/lang/ref/WeakReference<Landroidx/recyclerview/widget/RecyclerView;>;"
    :cond_0
    goto :goto_0

    .line 428
    :cond_1
    const/4 v0, 0x0

    return v0
.end method

.method private releaseWrapperAndLocalPosition(Landroidx/recyclerview/widget/ConcatAdapterController$WrapperAndLocalPosition;)V
    .locals 1
    .param p1, "wrapperAndLocalPosition"    # Landroidx/recyclerview/widget/ConcatAdapterController$WrapperAndLocalPosition;

    .line 359
    const/4 v0, 0x0

    iput-boolean v0, p1, Landroidx/recyclerview/widget/ConcatAdapterController$WrapperAndLocalPosition;->mInUse:Z

    .line 360
    const/4 v0, 0x0

    iput-object v0, p1, Landroidx/recyclerview/widget/ConcatAdapterController$WrapperAndLocalPosition;->mWrapper:Landroidx/recyclerview/widget/NestedAdapterWrapper;

    .line 361
    const/4 v0, -0x1

    iput v0, p1, Landroidx/recyclerview/widget/ConcatAdapterController$WrapperAndLocalPosition;->mLocalPosition:I

    .line 362
    iput-object p1, p0, Landroidx/recyclerview/widget/ConcatAdapterController;->mReusableHolder:Landroidx/recyclerview/widget/ConcatAdapterController$WrapperAndLocalPosition;

    .line 363
    return-void
.end method


# virtual methods
.method addAdapter(ILandroidx/recyclerview/widget/RecyclerView$Adapter;)Z
    .locals 5
    .param p1, "index"    # I
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(I",
            "Landroidx/recyclerview/widget/RecyclerView$Adapter<",
            "Landroidx/recyclerview/widget/RecyclerView$ViewHolder;",
            ">;)Z"
        }
    .end annotation

    .line 142
    .local p2, "adapter":Landroidx/recyclerview/widget/RecyclerView$Adapter;, "Landroidx/recyclerview/widget/RecyclerView$Adapter<Landroidx/recyclerview/widget/RecyclerView$ViewHolder;>;"
    if-ltz p1, :cond_6

    iget-object v0, p0, Landroidx/recyclerview/widget/ConcatAdapterController;->mWrappers:Ljava/util/List;

    invoke-interface {v0}, Ljava/util/List;->size()I

    move-result v0

    if-gt p1, v0, :cond_6

    .line 146
    invoke-virtual {p0}, Landroidx/recyclerview/widget/ConcatAdapterController;->hasStableIds()Z

    move-result v0

    if-eqz v0, :cond_0

    .line 147
    invoke-virtual {p2}, Landroidx/recyclerview/widget/RecyclerView$Adapter;->hasStableIds()Z

    move-result v0

    const-string v1, "All sub adapters must have stable ids when stable id mode is ISOLATED_STABLE_IDS or SHARED_STABLE_IDS"

    invoke-static {v0, v1}, Landroidx/core/util/Preconditions;->checkArgument(ZLjava/lang/Object;)V

    goto :goto_0

    .line 151
    :cond_0
    invoke-virtual {p2}, Landroidx/recyclerview/widget/RecyclerView$Adapter;->hasStableIds()Z

    move-result v0

    if-eqz v0, :cond_1

    .line 152
    const-string v0, "ConcatAdapter"

    const-string v1, "Stable ids in the adapter will be ignored as the ConcatAdapter is configured not to have stable ids"

    invoke-static {v0, v1}, Landroid/util/Log;->w(Ljava/lang/String;Ljava/lang/String;)I

    .line 156
    :cond_1
    :goto_0
    invoke-direct {p0, p2}, Landroidx/recyclerview/widget/ConcatAdapterController;->findWrapperFor(Landroidx/recyclerview/widget/RecyclerView$Adapter;)Landroidx/recyclerview/widget/NestedAdapterWrapper;

    move-result-object v0

    .line 157
    .local v0, "existing":Landroidx/recyclerview/widget/NestedAdapterWrapper;
    if-eqz v0, :cond_2

    .line 158
    const/4 v1, 0x0

    return v1

    .line 160
    :cond_2
    new-instance v1, Landroidx/recyclerview/widget/NestedAdapterWrapper;

    iget-object v2, p0, Landroidx/recyclerview/widget/ConcatAdapterController;->mViewTypeStorage:Landroidx/recyclerview/widget/ViewTypeStorage;

    iget-object v3, p0, Landroidx/recyclerview/widget/ConcatAdapterController;->mStableIdStorage:Landroidx/recyclerview/widget/StableIdStorage;

    .line 161
    invoke-interface {v3}, Landroidx/recyclerview/widget/StableIdStorage;->createStableIdLookup()Landroidx/recyclerview/widget/StableIdStorage$StableIdLookup;

    move-result-object v3

    invoke-direct {v1, p2, p0, v2, v3}, Landroidx/recyclerview/widget/NestedAdapterWrapper;-><init>(Landroidx/recyclerview/widget/RecyclerView$Adapter;Landroidx/recyclerview/widget/NestedAdapterWrapper$Callback;Landroidx/recyclerview/widget/ViewTypeStorage;Landroidx/recyclerview/widget/StableIdStorage$StableIdLookup;)V

    .line 162
    .local v1, "wrapper":Landroidx/recyclerview/widget/NestedAdapterWrapper;
    iget-object v2, p0, Landroidx/recyclerview/widget/ConcatAdapterController;->mWrappers:Ljava/util/List;

    invoke-interface {v2, p1, v1}, Ljava/util/List;->add(ILjava/lang/Object;)V

    .line 164
    iget-object v2, p0, Landroidx/recyclerview/widget/ConcatAdapterController;->mAttachedRecyclerViews:Ljava/util/List;

    invoke-interface {v2}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v2

    :goto_1
    invoke-interface {v2}, Ljava/util/Iterator;->hasNext()Z

    move-result v3

    if-eqz v3, :cond_4

    invoke-interface {v2}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v3

    check-cast v3, Ljava/lang/ref/WeakReference;

    .line 165
    .local v3, "reference":Ljava/lang/ref/WeakReference;, "Ljava/lang/ref/WeakReference<Landroidx/recyclerview/widget/RecyclerView;>;"
    invoke-virtual {v3}, Ljava/lang/ref/WeakReference;->get()Ljava/lang/Object;

    move-result-object v4

    check-cast v4, Landroidx/recyclerview/widget/RecyclerView;

    .line 166
    .local v4, "recyclerView":Landroidx/recyclerview/widget/RecyclerView;
    if-eqz v4, :cond_3

    .line 167
    invoke-virtual {p2, v4}, Landroidx/recyclerview/widget/RecyclerView$Adapter;->onAttachedToRecyclerView(Landroidx/recyclerview/widget/RecyclerView;)V

    .line 169
    .end local v3    # "reference":Ljava/lang/ref/WeakReference;, "Ljava/lang/ref/WeakReference<Landroidx/recyclerview/widget/RecyclerView;>;"
    .end local v4    # "recyclerView":Landroidx/recyclerview/widget/RecyclerView;
    :cond_3
    goto :goto_1

    .line 171
    :cond_4
    invoke-virtual {v1}, Landroidx/recyclerview/widget/NestedAdapterWrapper;->getCachedItemCount()I

    move-result v2

    if-lez v2, :cond_5

    .line 172
    iget-object v2, p0, Landroidx/recyclerview/widget/ConcatAdapterController;->mConcatAdapter:Landroidx/recyclerview/widget/ConcatAdapter;

    .line 173
    invoke-direct {p0, v1}, Landroidx/recyclerview/widget/ConcatAdapterController;->countItemsBefore(Landroidx/recyclerview/widget/NestedAdapterWrapper;)I

    move-result v3

    .line 174
    invoke-virtual {v1}, Landroidx/recyclerview/widget/NestedAdapterWrapper;->getCachedItemCount()I

    move-result v4

    .line 172
    invoke-virtual {v2, v3, v4}, Landroidx/recyclerview/widget/ConcatAdapter;->notifyItemRangeInserted(II)V

    .line 178
    :cond_5
    invoke-direct {p0}, Landroidx/recyclerview/widget/ConcatAdapterController;->calculateAndUpdateStateRestorationPolicy()V

    .line 179
    const/4 v2, 0x1

    return v2

    .line 143
    .end local v0    # "existing":Landroidx/recyclerview/widget/NestedAdapterWrapper;
    .end local v1    # "wrapper":Landroidx/recyclerview/widget/NestedAdapterWrapper;
    :cond_6
    new-instance v0, Ljava/lang/IndexOutOfBoundsException;

    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "Index must be between 0 and "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v1

    iget-object v2, p0, Landroidx/recyclerview/widget/ConcatAdapterController;->mWrappers:Ljava/util/List;

    .line 144
    invoke-interface {v2}, Ljava/util/List;->size()I

    move-result v2

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    move-result-object v1

    const-string v2, ". Given:"

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v1

    invoke-virtual {v1, p1}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    move-result-object v1

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-direct {v0, v1}, Ljava/lang/IndexOutOfBoundsException;-><init>(Ljava/lang/String;)V

    throw v0
.end method

.method addAdapter(Landroidx/recyclerview/widget/RecyclerView$Adapter;)Z
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Landroidx/recyclerview/widget/RecyclerView$Adapter<",
            "Landroidx/recyclerview/widget/RecyclerView$ViewHolder;",
            ">;)Z"
        }
    .end annotation

    .line 132
    .local p1, "adapter":Landroidx/recyclerview/widget/RecyclerView$Adapter;, "Landroidx/recyclerview/widget/RecyclerView$Adapter<Landroidx/recyclerview/widget/RecyclerView$ViewHolder;>;"
    iget-object v0, p0, Landroidx/recyclerview/widget/ConcatAdapterController;->mWrappers:Ljava/util/List;

    invoke-interface {v0}, Ljava/util/List;->size()I

    move-result v0

    invoke-virtual {p0, v0, p1}, Landroidx/recyclerview/widget/ConcatAdapterController;->addAdapter(ILandroidx/recyclerview/widget/RecyclerView$Adapter;)Z

    move-result v0

    return v0
.end method

.method public canRestoreState()Z
    .locals 3

    .line 373
    iget-object v0, p0, Landroidx/recyclerview/widget/ConcatAdapterController;->mWrappers:Ljava/util/List;

    invoke-interface {v0}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v0

    :goto_0
    invoke-interface {v0}, Ljava/util/Iterator;->hasNext()Z

    move-result v1

    if-eqz v1, :cond_1

    invoke-interface {v0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Landroidx/recyclerview/widget/NestedAdapterWrapper;

    .line 374
    .local v1, "wrapper":Landroidx/recyclerview/widget/NestedAdapterWrapper;
    iget-object v2, v1, Landroidx/recyclerview/widget/NestedAdapterWrapper;->adapter:Landroidx/recyclerview/widget/RecyclerView$Adapter;

    invoke-virtual {v2}, Landroidx/recyclerview/widget/RecyclerView$Adapter;->canRestoreState()Z

    move-result v2

    if-nez v2, :cond_0

    .line 375
    const/4 v0, 0x0

    return v0

    .line 377
    .end local v1    # "wrapper":Landroidx/recyclerview/widget/NestedAdapterWrapper;
    :cond_0
    goto :goto_0

    .line 378
    :cond_1
    const/4 v0, 0x1

    return v0
.end method

.method public getBoundAdapter(Landroidx/recyclerview/widget/RecyclerView$ViewHolder;)Landroidx/recyclerview/widget/RecyclerView$Adapter;
    .locals 2
    .param p1, "viewHolder"    # Landroidx/recyclerview/widget/RecyclerView$ViewHolder;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Landroidx/recyclerview/widget/RecyclerView$ViewHolder;",
            ")",
            "Landroidx/recyclerview/widget/RecyclerView$Adapter<",
            "+",
            "Landroidx/recyclerview/widget/RecyclerView$ViewHolder;",
            ">;"
        }
    .end annotation

    .line 486
    iget-object v0, p0, Landroidx/recyclerview/widget/ConcatAdapterController;->mBinderLookup:Ljava/util/IdentityHashMap;

    invoke-virtual {v0, p1}, Ljava/util/IdentityHashMap;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Landroidx/recyclerview/widget/NestedAdapterWrapper;

    .line 487
    .local v0, "wrapper":Landroidx/recyclerview/widget/NestedAdapterWrapper;
    if-nez v0, :cond_0

    .line 488
    const/4 v1, 0x0

    return-object v1

    .line 490
    :cond_0
    iget-object v1, v0, Landroidx/recyclerview/widget/NestedAdapterWrapper;->adapter:Landroidx/recyclerview/widget/RecyclerView$Adapter;

    return-object v1
.end method

.method public getCopyOfAdapters()Ljava/util/List;
    .locals 4
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()",
            "Ljava/util/List<",
            "Landroidx/recyclerview/widget/RecyclerView$Adapter<",
            "+",
            "Landroidx/recyclerview/widget/RecyclerView$ViewHolder;",
            ">;>;"
        }
    .end annotation

    .line 495
    iget-object v0, p0, Landroidx/recyclerview/widget/ConcatAdapterController;->mWrappers:Ljava/util/List;

    invoke-interface {v0}, Ljava/util/List;->isEmpty()Z

    move-result v0

    if-eqz v0, :cond_0

    .line 496
    invoke-static {}, Ljava/util/Collections;->emptyList()Ljava/util/List;

    move-result-object v0

    return-object v0

    .line 498
    :cond_0
    new-instance v0, Ljava/util/ArrayList;

    iget-object v1, p0, Landroidx/recyclerview/widget/ConcatAdapterController;->mWrappers:Ljava/util/List;

    invoke-interface {v1}, Ljava/util/List;->size()I

    move-result v1

    invoke-direct {v0, v1}, Ljava/util/ArrayList;-><init>(I)V

    .line 499
    .local v0, "adapters":Ljava/util/List;, "Ljava/util/List<Landroidx/recyclerview/widget/RecyclerView$Adapter<+Landroidx/recyclerview/widget/RecyclerView$ViewHolder;>;>;"
    iget-object v1, p0, Landroidx/recyclerview/widget/ConcatAdapterController;->mWrappers:Ljava/util/List;

    invoke-interface {v1}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v1

    :goto_0
    invoke-interface {v1}, Ljava/util/Iterator;->hasNext()Z

    move-result v2

    if-eqz v2, :cond_1

    invoke-interface {v1}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Landroidx/recyclerview/widget/NestedAdapterWrapper;

    .line 500
    .local v2, "wrapper":Landroidx/recyclerview/widget/NestedAdapterWrapper;
    iget-object v3, v2, Landroidx/recyclerview/widget/NestedAdapterWrapper;->adapter:Landroidx/recyclerview/widget/RecyclerView$Adapter;

    invoke-interface {v0, v3}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    .line 501
    .end local v2    # "wrapper":Landroidx/recyclerview/widget/NestedAdapterWrapper;
    goto :goto_0

    .line 502
    :cond_1
    return-object v0
.end method

.method public getItemId(I)J
    .locals 3
    .param p1, "globalPosition"    # I

    .line 216
    invoke-direct {p0, p1}, Landroidx/recyclerview/widget/ConcatAdapterController;->findWrapperAndLocalPosition(I)Landroidx/recyclerview/widget/ConcatAdapterController$WrapperAndLocalPosition;

    move-result-object v0

    .line 217
    .local v0, "wrapperAndPos":Landroidx/recyclerview/widget/ConcatAdapterController$WrapperAndLocalPosition;
    iget-object v1, v0, Landroidx/recyclerview/widget/ConcatAdapterController$WrapperAndLocalPosition;->mWrapper:Landroidx/recyclerview/widget/NestedAdapterWrapper;

    iget v2, v0, Landroidx/recyclerview/widget/ConcatAdapterController$WrapperAndLocalPosition;->mLocalPosition:I

    invoke-virtual {v1, v2}, Landroidx/recyclerview/widget/NestedAdapterWrapper;->getItemId(I)J

    move-result-wide v1

    .line 218
    .local v1, "globalItemId":J
    invoke-direct {p0, v0}, Landroidx/recyclerview/widget/ConcatAdapterController;->releaseWrapperAndLocalPosition(Landroidx/recyclerview/widget/ConcatAdapterController$WrapperAndLocalPosition;)V

    .line 219
    return-wide v1
.end method

.method public getItemViewType(I)I
    .locals 3
    .param p1, "globalPosition"    # I

    .line 317
    invoke-direct {p0, p1}, Landroidx/recyclerview/widget/ConcatAdapterController;->findWrapperAndLocalPosition(I)Landroidx/recyclerview/widget/ConcatAdapterController$WrapperAndLocalPosition;

    move-result-object v0

    .line 318
    .local v0, "wrapperAndPos":Landroidx/recyclerview/widget/ConcatAdapterController$WrapperAndLocalPosition;
    iget-object v1, v0, Landroidx/recyclerview/widget/ConcatAdapterController$WrapperAndLocalPosition;->mWrapper:Landroidx/recyclerview/widget/NestedAdapterWrapper;

    iget v2, v0, Landroidx/recyclerview/widget/ConcatAdapterController$WrapperAndLocalPosition;->mLocalPosition:I

    invoke-virtual {v1, v2}, Landroidx/recyclerview/widget/NestedAdapterWrapper;->getItemViewType(I)I

    move-result v1

    .line 319
    .local v1, "itemViewType":I
    invoke-direct {p0, v0}, Landroidx/recyclerview/widget/ConcatAdapterController;->releaseWrapperAndLocalPosition(Landroidx/recyclerview/widget/ConcatAdapterController$WrapperAndLocalPosition;)V

    .line 320
    return v1
.end method

.method public getLocalAdapterPosition(Landroidx/recyclerview/widget/RecyclerView$Adapter;Landroidx/recyclerview/widget/RecyclerView$ViewHolder;I)I
    .locals 7
    .param p2, "viewHolder"    # Landroidx/recyclerview/widget/RecyclerView$ViewHolder;
    .param p3, "globalPosition"    # I
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Landroidx/recyclerview/widget/RecyclerView$Adapter<",
            "+",
            "Landroidx/recyclerview/widget/RecyclerView$ViewHolder;",
            ">;",
            "Landroidx/recyclerview/widget/RecyclerView$ViewHolder;",
            "I)I"
        }
    .end annotation

    .line 461
    .local p1, "adapter":Landroidx/recyclerview/widget/RecyclerView$Adapter;, "Landroidx/recyclerview/widget/RecyclerView$Adapter<+Landroidx/recyclerview/widget/RecyclerView$ViewHolder;>;"
    iget-object v0, p0, Landroidx/recyclerview/widget/ConcatAdapterController;->mBinderLookup:Ljava/util/IdentityHashMap;

    invoke-virtual {v0, p2}, Ljava/util/IdentityHashMap;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Landroidx/recyclerview/widget/NestedAdapterWrapper;

    .line 462
    .local v0, "wrapper":Landroidx/recyclerview/widget/NestedAdapterWrapper;
    if-nez v0, :cond_0

    .line 463
    const/4 v1, -0x1

    return v1

    .line 465
    :cond_0
    invoke-direct {p0, v0}, Landroidx/recyclerview/widget/ConcatAdapterController;->countItemsBefore(Landroidx/recyclerview/widget/NestedAdapterWrapper;)I

    move-result v1

    .line 467
    .local v1, "itemsBefore":I
    sub-int v2, p3, v1

    .line 469
    .local v2, "localPosition":I
    iget-object v3, v0, Landroidx/recyclerview/widget/NestedAdapterWrapper;->adapter:Landroidx/recyclerview/widget/RecyclerView$Adapter;

    invoke-virtual {v3}, Landroidx/recyclerview/widget/RecyclerView$Adapter;->getItemCount()I

    move-result v3

    .line 470
    .local v3, "itemCount":I
    if-ltz v2, :cond_1

    if-ge v2, v3, :cond_1

    .line 480
    iget-object v4, v0, Landroidx/recyclerview/widget/NestedAdapterWrapper;->adapter:Landroidx/recyclerview/widget/RecyclerView$Adapter;

    invoke-virtual {v4, p1, p2, v2}, Landroidx/recyclerview/widget/RecyclerView$Adapter;->findRelativeAdapterPositionIn(Landroidx/recyclerview/widget/RecyclerView$Adapter;Landroidx/recyclerview/widget/RecyclerView$ViewHolder;I)I

    move-result v4

    return v4

    .line 471
    :cond_1
    new-instance v4, Ljava/lang/IllegalStateException;

    new-instance v5, Ljava/lang/StringBuilder;

    invoke-direct {v5}, Ljava/lang/StringBuilder;-><init>()V

    const-string v6, "Detected inconsistent adapter updates. The local position of the view holder maps to "

    invoke-virtual {v5, v6}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v5

    invoke-virtual {v5, v2}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    move-result-object v5

    const-string v6, " which is out of bounds for the adapter with size "

    invoke-virtual {v5, v6}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v5

    invoke-virtual {v5, v3}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    move-result-object v5

    const-string v6, ".Make sure to immediately call notify methods in your adapter when you change the backing dataviewHolder:"

    invoke-virtual {v5, v6}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v5

    invoke-virtual {v5, p2}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    move-result-object v5

    const-string v6, "adapter:"

    invoke-virtual {v5, v6}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v5

    invoke-virtual {v5, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    move-result-object v5

    invoke-virtual {v5}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v5

    invoke-direct {v4, v5}, Ljava/lang/IllegalStateException;-><init>(Ljava/lang/String;)V

    throw v4
.end method

.method public getTotalCount()I
    .locals 4

    .line 309
    const/4 v0, 0x0

    .line 310
    .local v0, "total":I
    iget-object v1, p0, Landroidx/recyclerview/widget/ConcatAdapterController;->mWrappers:Ljava/util/List;

    invoke-interface {v1}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v1

    :goto_0
    invoke-interface {v1}, Ljava/util/Iterator;->hasNext()Z

    move-result v2

    if-eqz v2, :cond_0

    invoke-interface {v1}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Landroidx/recyclerview/widget/NestedAdapterWrapper;

    .line 311
    .local v2, "wrapper":Landroidx/recyclerview/widget/NestedAdapterWrapper;
    invoke-virtual {v2}, Landroidx/recyclerview/widget/NestedAdapterWrapper;->getCachedItemCount()I

    move-result v3

    add-int/2addr v0, v3

    .line 312
    .end local v2    # "wrapper":Landroidx/recyclerview/widget/NestedAdapterWrapper;
    goto :goto_0

    .line 313
    :cond_0
    return v0
.end method

.method public hasStableIds()Z
    .locals 2

    .line 506
    iget-object v0, p0, Landroidx/recyclerview/widget/ConcatAdapterController;->mStableIdMode:Landroidx/recyclerview/widget/ConcatAdapter$Config$StableIdMode;

    sget-object v1, Landroidx/recyclerview/widget/ConcatAdapter$Config$StableIdMode;->NO_STABLE_IDS:Landroidx/recyclerview/widget/ConcatAdapter$Config$StableIdMode;

    if-eq v0, v1, :cond_0

    const/4 v0, 0x1

    goto :goto_0

    :cond_0
    const/4 v0, 0x0

    :goto_0
    return v0
.end method

.method public onAttachedToRecyclerView(Landroidx/recyclerview/widget/RecyclerView;)V
    .locals 3
    .param p1, "recyclerView"    # Landroidx/recyclerview/widget/RecyclerView;

    .line 432
    invoke-direct {p0, p1}, Landroidx/recyclerview/widget/ConcatAdapterController;->isAttachedTo(Landroidx/recyclerview/widget/RecyclerView;)Z

    move-result v0

    if-eqz v0, :cond_0

    .line 433
    return-void

    .line 435
    :cond_0
    iget-object v0, p0, Landroidx/recyclerview/widget/ConcatAdapterController;->mAttachedRecyclerViews:Ljava/util/List;

    new-instance v1, Ljava/lang/ref/WeakReference;

    invoke-direct {v1, p1}, Ljava/lang/ref/WeakReference;-><init>(Ljava/lang/Object;)V

    invoke-interface {v0, v1}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    .line 436
    iget-object v0, p0, Landroidx/recyclerview/widget/ConcatAdapterController;->mWrappers:Ljava/util/List;

    invoke-interface {v0}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v0

    :goto_0
    invoke-interface {v0}, Ljava/util/Iterator;->hasNext()Z

    move-result v1

    if-eqz v1, :cond_1

    invoke-interface {v0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Landroidx/recyclerview/widget/NestedAdapterWrapper;

    .line 437
    .local v1, "wrapper":Landroidx/recyclerview/widget/NestedAdapterWrapper;
    iget-object v2, v1, Landroidx/recyclerview/widget/NestedAdapterWrapper;->adapter:Landroidx/recyclerview/widget/RecyclerView$Adapter;

    invoke-virtual {v2, p1}, Landroidx/recyclerview/widget/RecyclerView$Adapter;->onAttachedToRecyclerView(Landroidx/recyclerview/widget/RecyclerView;)V

    .line 438
    .end local v1    # "wrapper":Landroidx/recyclerview/widget/NestedAdapterWrapper;
    goto :goto_0

    .line 439
    :cond_1
    return-void
.end method

.method public onBindViewHolder(Landroidx/recyclerview/widget/RecyclerView$ViewHolder;I)V
    .locals 3
    .param p1, "holder"    # Landroidx/recyclerview/widget/RecyclerView$ViewHolder;
    .param p2, "globalPosition"    # I

    .line 366
    invoke-direct {p0, p2}, Landroidx/recyclerview/widget/ConcatAdapterController;->findWrapperAndLocalPosition(I)Landroidx/recyclerview/widget/ConcatAdapterController$WrapperAndLocalPosition;

    move-result-object v0

    .line 367
    .local v0, "wrapperAndPos":Landroidx/recyclerview/widget/ConcatAdapterController$WrapperAndLocalPosition;
    iget-object v1, p0, Landroidx/recyclerview/widget/ConcatAdapterController;->mBinderLookup:Ljava/util/IdentityHashMap;

    iget-object v2, v0, Landroidx/recyclerview/widget/ConcatAdapterController$WrapperAndLocalPosition;->mWrapper:Landroidx/recyclerview/widget/NestedAdapterWrapper;

    invoke-virtual {v1, p1, v2}, Ljava/util/IdentityHashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 368
    iget-object v1, v0, Landroidx/recyclerview/widget/ConcatAdapterController$WrapperAndLocalPosition;->mWrapper:Landroidx/recyclerview/widget/NestedAdapterWrapper;

    iget v2, v0, Landroidx/recyclerview/widget/ConcatAdapterController$WrapperAndLocalPosition;->mLocalPosition:I

    invoke-virtual {v1, p1, v2}, Landroidx/recyclerview/widget/NestedAdapterWrapper;->onBindViewHolder(Landroidx/recyclerview/widget/RecyclerView$ViewHolder;I)V

    .line 369
    invoke-direct {p0, v0}, Landroidx/recyclerview/widget/ConcatAdapterController;->releaseWrapperAndLocalPosition(Landroidx/recyclerview/widget/ConcatAdapterController$WrapperAndLocalPosition;)V

    .line 370
    return-void
.end method

.method public onChanged(Landroidx/recyclerview/widget/NestedAdapterWrapper;)V
    .locals 1
    .param p1, "wrapper"    # Landroidx/recyclerview/widget/NestedAdapterWrapper;

    .line 225
    iget-object v0, p0, Landroidx/recyclerview/widget/ConcatAdapterController;->mConcatAdapter:Landroidx/recyclerview/widget/ConcatAdapter;

    invoke-virtual {v0}, Landroidx/recyclerview/widget/ConcatAdapter;->notifyDataSetChanged()V

    .line 226
    invoke-direct {p0}, Landroidx/recyclerview/widget/ConcatAdapterController;->calculateAndUpdateStateRestorationPolicy()V

    .line 227
    return-void
.end method

.method public onCreateViewHolder(Landroid/view/ViewGroup;I)Landroidx/recyclerview/widget/RecyclerView$ViewHolder;
    .locals 2
    .param p1, "parent"    # Landroid/view/ViewGroup;
    .param p2, "globalViewType"    # I

    .line 324
    iget-object v0, p0, Landroidx/recyclerview/widget/ConcatAdapterController;->mViewTypeStorage:Landroidx/recyclerview/widget/ViewTypeStorage;

    invoke-interface {v0, p2}, Landroidx/recyclerview/widget/ViewTypeStorage;->getWrapperForGlobalType(I)Landroidx/recyclerview/widget/NestedAdapterWrapper;

    move-result-object v0

    .line 325
    .local v0, "wrapper":Landroidx/recyclerview/widget/NestedAdapterWrapper;
    invoke-virtual {v0, p1, p2}, Landroidx/recyclerview/widget/NestedAdapterWrapper;->onCreateViewHolder(Landroid/view/ViewGroup;I)Landroidx/recyclerview/widget/RecyclerView$ViewHolder;

    move-result-object v1

    return-object v1
.end method

.method public onDetachedFromRecyclerView(Landroidx/recyclerview/widget/RecyclerView;)V
    .locals 3
    .param p1, "recyclerView"    # Landroidx/recyclerview/widget/RecyclerView;

    .line 442
    iget-object v0, p0, Landroidx/recyclerview/widget/ConcatAdapterController;->mAttachedRecyclerViews:Ljava/util/List;

    invoke-interface {v0}, Ljava/util/List;->size()I

    move-result v0

    add-int/lit8 v0, v0, -0x1

    .local v0, "i":I
    :goto_0
    if-ltz v0, :cond_2

    .line 443
    iget-object v1, p0, Landroidx/recyclerview/widget/ConcatAdapterController;->mAttachedRecyclerViews:Ljava/util/List;

    invoke-interface {v1, v0}, Ljava/util/List;->get(I)Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Ljava/lang/ref/WeakReference;

    .line 444
    .local v1, "reference":Ljava/lang/ref/WeakReference;, "Ljava/lang/ref/WeakReference<Landroidx/recyclerview/widget/RecyclerView;>;"
    invoke-virtual {v1}, Ljava/lang/ref/WeakReference;->get()Ljava/lang/Object;

    move-result-object v2

    if-nez v2, :cond_0

    .line 445
    iget-object v2, p0, Landroidx/recyclerview/widget/ConcatAdapterController;->mAttachedRecyclerViews:Ljava/util/List;

    invoke-interface {v2, v0}, Ljava/util/List;->remove(I)Ljava/lang/Object;

    goto :goto_1

    .line 446
    :cond_0
    invoke-virtual {v1}, Ljava/lang/ref/WeakReference;->get()Ljava/lang/Object;

    move-result-object v2

    if-ne v2, p1, :cond_1

    .line 447
    iget-object v2, p0, Landroidx/recyclerview/widget/ConcatAdapterController;->mAttachedRecyclerViews:Ljava/util/List;

    invoke-interface {v2, v0}, Ljava/util/List;->remove(I)Ljava/lang/Object;

    .line 448
    goto :goto_2

    .line 442
    .end local v1    # "reference":Ljava/lang/ref/WeakReference;, "Ljava/lang/ref/WeakReference<Landroidx/recyclerview/widget/RecyclerView;>;"
    :cond_1
    :goto_1
    add-int/lit8 v0, v0, -0x1

    goto :goto_0

    .line 451
    .end local v0    # "i":I
    :cond_2
    :goto_2
    iget-object v0, p0, Landroidx/recyclerview/widget/ConcatAdapterController;->mWrappers:Ljava/util/List;

    invoke-interface {v0}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v0

    :goto_3
    invoke-interface {v0}, Ljava/util/Iterator;->hasNext()Z

    move-result v1

    if-eqz v1, :cond_3

    invoke-interface {v0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Landroidx/recyclerview/widget/NestedAdapterWrapper;

    .line 452
    .local v1, "wrapper":Landroidx/recyclerview/widget/NestedAdapterWrapper;
    iget-object v2, v1, Landroidx/recyclerview/widget/NestedAdapterWrapper;->adapter:Landroidx/recyclerview/widget/RecyclerView$Adapter;

    invoke-virtual {v2, p1}, Landroidx/recyclerview/widget/RecyclerView$Adapter;->onDetachedFromRecyclerView(Landroidx/recyclerview/widget/RecyclerView;)V

    .line 453
    .end local v1    # "wrapper":Landroidx/recyclerview/widget/NestedAdapterWrapper;
    goto :goto_3

    .line 454
    :cond_3
    return-void
.end method

.method public onFailedToRecycleView(Landroidx/recyclerview/widget/RecyclerView$ViewHolder;)Z
    .locals 4
    .param p1, "holder"    # Landroidx/recyclerview/widget/RecyclerView$ViewHolder;

    .line 402
    iget-object v0, p0, Landroidx/recyclerview/widget/ConcatAdapterController;->mBinderLookup:Ljava/util/IdentityHashMap;

    invoke-virtual {v0, p1}, Ljava/util/IdentityHashMap;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Landroidx/recyclerview/widget/NestedAdapterWrapper;

    .line 403
    .local v0, "wrapper":Landroidx/recyclerview/widget/NestedAdapterWrapper;
    if-eqz v0, :cond_0

    .line 407
    iget-object v1, v0, Landroidx/recyclerview/widget/NestedAdapterWrapper;->adapter:Landroidx/recyclerview/widget/RecyclerView$Adapter;

    invoke-virtual {v1, p1}, Landroidx/recyclerview/widget/RecyclerView$Adapter;->onFailedToRecycleView(Landroidx/recyclerview/widget/RecyclerView$ViewHolder;)Z

    move-result v1

    .line 408
    .local v1, "result":Z
    iget-object v2, p0, Landroidx/recyclerview/widget/ConcatAdapterController;->mBinderLookup:Ljava/util/IdentityHashMap;

    invoke-virtual {v2, p1}, Ljava/util/IdentityHashMap;->remove(Ljava/lang/Object;)Ljava/lang/Object;

    .line 409
    return v1

    .line 404
    .end local v1    # "result":Z
    :cond_0
    new-instance v1, Ljava/lang/IllegalStateException;

    new-instance v2, Ljava/lang/StringBuilder;

    invoke-direct {v2}, Ljava/lang/StringBuilder;-><init>()V

    const-string v3, "Cannot find wrapper for "

    invoke-virtual {v2, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v2

    invoke-virtual {v2, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    move-result-object v2

    const-string v3, ", seems like it is not bound by this adapter: "

    invoke-virtual {v2, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v2

    invoke-virtual {v2, p0}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    move-result-object v2

    invoke-virtual {v2}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v2

    invoke-direct {v1, v2}, Ljava/lang/IllegalStateException;-><init>(Ljava/lang/String;)V

    throw v1
.end method

.method public onItemRangeChanged(Landroidx/recyclerview/widget/NestedAdapterWrapper;II)V
    .locals 3
    .param p1, "nestedAdapterWrapper"    # Landroidx/recyclerview/widget/NestedAdapterWrapper;
    .param p2, "positionStart"    # I
    .param p3, "itemCount"    # I

    .line 232
    invoke-direct {p0, p1}, Landroidx/recyclerview/widget/ConcatAdapterController;->countItemsBefore(Landroidx/recyclerview/widget/NestedAdapterWrapper;)I

    move-result v0

    .line 233
    .local v0, "offset":I
    iget-object v1, p0, Landroidx/recyclerview/widget/ConcatAdapterController;->mConcatAdapter:Landroidx/recyclerview/widget/ConcatAdapter;

    add-int v2, p2, v0

    invoke-virtual {v1, v2, p3}, Landroidx/recyclerview/widget/ConcatAdapter;->notifyItemRangeChanged(II)V

    .line 237
    return-void
.end method

.method public onItemRangeChanged(Landroidx/recyclerview/widget/NestedAdapterWrapper;IILjava/lang/Object;)V
    .locals 3
    .param p1, "nestedAdapterWrapper"    # Landroidx/recyclerview/widget/NestedAdapterWrapper;
    .param p2, "positionStart"    # I
    .param p3, "itemCount"    # I
    .param p4, "payload"    # Ljava/lang/Object;

    .line 242
    invoke-direct {p0, p1}, Landroidx/recyclerview/widget/ConcatAdapterController;->countItemsBefore(Landroidx/recyclerview/widget/NestedAdapterWrapper;)I

    move-result v0

    .line 243
    .local v0, "offset":I
    iget-object v1, p0, Landroidx/recyclerview/widget/ConcatAdapterController;->mConcatAdapter:Landroidx/recyclerview/widget/ConcatAdapter;

    add-int v2, p2, v0

    invoke-virtual {v1, v2, p3, p4}, Landroidx/recyclerview/widget/ConcatAdapter;->notifyItemRangeChanged(IILjava/lang/Object;)V

    .line 248
    return-void
.end method

.method public onItemRangeInserted(Landroidx/recyclerview/widget/NestedAdapterWrapper;II)V
    .locals 3
    .param p1, "nestedAdapterWrapper"    # Landroidx/recyclerview/widget/NestedAdapterWrapper;
    .param p2, "positionStart"    # I
    .param p3, "itemCount"    # I

    .line 253
    invoke-direct {p0, p1}, Landroidx/recyclerview/widget/ConcatAdapterController;->countItemsBefore(Landroidx/recyclerview/widget/NestedAdapterWrapper;)I

    move-result v0

    .line 254
    .local v0, "offset":I
    iget-object v1, p0, Landroidx/recyclerview/widget/ConcatAdapterController;->mConcatAdapter:Landroidx/recyclerview/widget/ConcatAdapter;

    add-int v2, p2, v0

    invoke-virtual {v1, v2, p3}, Landroidx/recyclerview/widget/ConcatAdapter;->notifyItemRangeInserted(II)V

    .line 258
    return-void
.end method

.method public onItemRangeMoved(Landroidx/recyclerview/widget/NestedAdapterWrapper;II)V
    .locals 4
    .param p1, "nestedAdapterWrapper"    # Landroidx/recyclerview/widget/NestedAdapterWrapper;
    .param p2, "fromPosition"    # I
    .param p3, "toPosition"    # I

    .line 273
    invoke-direct {p0, p1}, Landroidx/recyclerview/widget/ConcatAdapterController;->countItemsBefore(Landroidx/recyclerview/widget/NestedAdapterWrapper;)I

    move-result v0

    .line 274
    .local v0, "offset":I
    iget-object v1, p0, Landroidx/recyclerview/widget/ConcatAdapterController;->mConcatAdapter:Landroidx/recyclerview/widget/ConcatAdapter;

    add-int v2, p2, v0

    add-int v3, p3, v0

    invoke-virtual {v1, v2, v3}, Landroidx/recyclerview/widget/ConcatAdapter;->notifyItemMoved(II)V

    .line 278
    return-void
.end method

.method public onItemRangeRemoved(Landroidx/recyclerview/widget/NestedAdapterWrapper;II)V
    .locals 3
    .param p1, "nestedAdapterWrapper"    # Landroidx/recyclerview/widget/NestedAdapterWrapper;
    .param p2, "positionStart"    # I
    .param p3, "itemCount"    # I

    .line 263
    invoke-direct {p0, p1}, Landroidx/recyclerview/widget/ConcatAdapterController;->countItemsBefore(Landroidx/recyclerview/widget/NestedAdapterWrapper;)I

    move-result v0

    .line 264
    .local v0, "offset":I
    iget-object v1, p0, Landroidx/recyclerview/widget/ConcatAdapterController;->mConcatAdapter:Landroidx/recyclerview/widget/ConcatAdapter;

    add-int v2, p2, v0

    invoke-virtual {v1, v2, p3}, Landroidx/recyclerview/widget/ConcatAdapter;->notifyItemRangeRemoved(II)V

    .line 268
    return-void
.end method

.method public onStateRestorationPolicyChanged(Landroidx/recyclerview/widget/NestedAdapterWrapper;)V
    .locals 0
    .param p1, "nestedAdapterWrapper"    # Landroidx/recyclerview/widget/NestedAdapterWrapper;

    .line 282
    invoke-direct {p0}, Landroidx/recyclerview/widget/ConcatAdapterController;->calculateAndUpdateStateRestorationPolicy()V

    .line 283
    return-void
.end method

.method public onViewAttachedToWindow(Landroidx/recyclerview/widget/RecyclerView$ViewHolder;)V
    .locals 2
    .param p1, "holder"    # Landroidx/recyclerview/widget/RecyclerView$ViewHolder;

    .line 382
    invoke-direct {p0, p1}, Landroidx/recyclerview/widget/ConcatAdapterController;->getWrapper(Landroidx/recyclerview/widget/RecyclerView$ViewHolder;)Landroidx/recyclerview/widget/NestedAdapterWrapper;

    move-result-object v0

    .line 383
    .local v0, "wrapper":Landroidx/recyclerview/widget/NestedAdapterWrapper;
    iget-object v1, v0, Landroidx/recyclerview/widget/NestedAdapterWrapper;->adapter:Landroidx/recyclerview/widget/RecyclerView$Adapter;

    invoke-virtual {v1, p1}, Landroidx/recyclerview/widget/RecyclerView$Adapter;->onViewAttachedToWindow(Landroidx/recyclerview/widget/RecyclerView$ViewHolder;)V

    .line 384
    return-void
.end method

.method public onViewDetachedFromWindow(Landroidx/recyclerview/widget/RecyclerView$ViewHolder;)V
    .locals 2
    .param p1, "holder"    # Landroidx/recyclerview/widget/RecyclerView$ViewHolder;

    .line 387
    invoke-direct {p0, p1}, Landroidx/recyclerview/widget/ConcatAdapterController;->getWrapper(Landroidx/recyclerview/widget/RecyclerView$ViewHolder;)Landroidx/recyclerview/widget/NestedAdapterWrapper;

    move-result-object v0

    .line 388
    .local v0, "wrapper":Landroidx/recyclerview/widget/NestedAdapterWrapper;
    iget-object v1, v0, Landroidx/recyclerview/widget/NestedAdapterWrapper;->adapter:Landroidx/recyclerview/widget/RecyclerView$Adapter;

    invoke-virtual {v1, p1}, Landroidx/recyclerview/widget/RecyclerView$Adapter;->onViewDetachedFromWindow(Landroidx/recyclerview/widget/RecyclerView$ViewHolder;)V

    .line 389
    return-void
.end method

.method public onViewRecycled(Landroidx/recyclerview/widget/RecyclerView$ViewHolder;)V
    .locals 4
    .param p1, "holder"    # Landroidx/recyclerview/widget/RecyclerView$ViewHolder;

    .line 392
    iget-object v0, p0, Landroidx/recyclerview/widget/ConcatAdapterController;->mBinderLookup:Ljava/util/IdentityHashMap;

    invoke-virtual {v0, p1}, Ljava/util/IdentityHashMap;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Landroidx/recyclerview/widget/NestedAdapterWrapper;

    .line 393
    .local v0, "wrapper":Landroidx/recyclerview/widget/NestedAdapterWrapper;
    if-eqz v0, :cond_0

    .line 397
    iget-object v1, v0, Landroidx/recyclerview/widget/NestedAdapterWrapper;->adapter:Landroidx/recyclerview/widget/RecyclerView$Adapter;

    invoke-virtual {v1, p1}, Landroidx/recyclerview/widget/RecyclerView$Adapter;->onViewRecycled(Landroidx/recyclerview/widget/RecyclerView$ViewHolder;)V

    .line 398
    iget-object v1, p0, Landroidx/recyclerview/widget/ConcatAdapterController;->mBinderLookup:Ljava/util/IdentityHashMap;

    invoke-virtual {v1, p1}, Ljava/util/IdentityHashMap;->remove(Ljava/lang/Object;)Ljava/lang/Object;

    .line 399
    return-void

    .line 394
    :cond_0
    new-instance v1, Ljava/lang/IllegalStateException;

    new-instance v2, Ljava/lang/StringBuilder;

    invoke-direct {v2}, Ljava/lang/StringBuilder;-><init>()V

    const-string v3, "Cannot find wrapper for "

    invoke-virtual {v2, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v2

    invoke-virtual {v2, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    move-result-object v2

    const-string v3, ", seems like it is not bound by this adapter: "

    invoke-virtual {v2, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v2

    invoke-virtual {v2, p0}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    move-result-object v2

    invoke-virtual {v2}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v2

    invoke-direct {v1, v2}, Ljava/lang/IllegalStateException;-><init>(Ljava/lang/String;)V

    throw v1
.end method

.method removeAdapter(Landroidx/recyclerview/widget/RecyclerView$Adapter;)Z
    .locals 6
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Landroidx/recyclerview/widget/RecyclerView$Adapter<",
            "Landroidx/recyclerview/widget/RecyclerView$ViewHolder;",
            ">;)Z"
        }
    .end annotation

    .line 183
    .local p1, "adapter":Landroidx/recyclerview/widget/RecyclerView$Adapter;, "Landroidx/recyclerview/widget/RecyclerView$Adapter<Landroidx/recyclerview/widget/RecyclerView$ViewHolder;>;"
    invoke-direct {p0, p1}, Landroidx/recyclerview/widget/ConcatAdapterController;->indexOfWrapper(Landroidx/recyclerview/widget/RecyclerView$Adapter;)I

    move-result v0

    .line 184
    .local v0, "index":I
    const/4 v1, -0x1

    if-ne v0, v1, :cond_0

    .line 185
    const/4 v1, 0x0

    return v1

    .line 187
    :cond_0
    iget-object v1, p0, Landroidx/recyclerview/widget/ConcatAdapterController;->mWrappers:Ljava/util/List;

    invoke-interface {v1, v0}, Ljava/util/List;->get(I)Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Landroidx/recyclerview/widget/NestedAdapterWrapper;

    .line 188
    .local v1, "wrapper":Landroidx/recyclerview/widget/NestedAdapterWrapper;
    invoke-direct {p0, v1}, Landroidx/recyclerview/widget/ConcatAdapterController;->countItemsBefore(Landroidx/recyclerview/widget/NestedAdapterWrapper;)I

    move-result v2

    .line 189
    .local v2, "offset":I
    iget-object v3, p0, Landroidx/recyclerview/widget/ConcatAdapterController;->mWrappers:Ljava/util/List;

    invoke-interface {v3, v0}, Ljava/util/List;->remove(I)Ljava/lang/Object;

    .line 190
    iget-object v3, p0, Landroidx/recyclerview/widget/ConcatAdapterController;->mConcatAdapter:Landroidx/recyclerview/widget/ConcatAdapter;

    invoke-virtual {v1}, Landroidx/recyclerview/widget/NestedAdapterWrapper;->getCachedItemCount()I

    move-result v4

    invoke-virtual {v3, v2, v4}, Landroidx/recyclerview/widget/ConcatAdapter;->notifyItemRangeRemoved(II)V

    .line 192
    iget-object v3, p0, Landroidx/recyclerview/widget/ConcatAdapterController;->mAttachedRecyclerViews:Ljava/util/List;

    invoke-interface {v3}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v3

    :goto_0
    invoke-interface {v3}, Ljava/util/Iterator;->hasNext()Z

    move-result v4

    if-eqz v4, :cond_2

    invoke-interface {v3}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v4

    check-cast v4, Ljava/lang/ref/WeakReference;

    .line 193
    .local v4, "reference":Ljava/lang/ref/WeakReference;, "Ljava/lang/ref/WeakReference<Landroidx/recyclerview/widget/RecyclerView;>;"
    invoke-virtual {v4}, Ljava/lang/ref/WeakReference;->get()Ljava/lang/Object;

    move-result-object v5

    check-cast v5, Landroidx/recyclerview/widget/RecyclerView;

    .line 194
    .local v5, "recyclerView":Landroidx/recyclerview/widget/RecyclerView;
    if-eqz v5, :cond_1

    .line 195
    invoke-virtual {p1, v5}, Landroidx/recyclerview/widget/RecyclerView$Adapter;->onDetachedFromRecyclerView(Landroidx/recyclerview/widget/RecyclerView;)V

    .line 197
    .end local v4    # "reference":Ljava/lang/ref/WeakReference;, "Ljava/lang/ref/WeakReference<Landroidx/recyclerview/widget/RecyclerView;>;"
    .end local v5    # "recyclerView":Landroidx/recyclerview/widget/RecyclerView;
    :cond_1
    goto :goto_0

    .line 198
    :cond_2
    invoke-virtual {v1}, Landroidx/recyclerview/widget/NestedAdapterWrapper;->dispose()V

    .line 199
    invoke-direct {p0}, Landroidx/recyclerview/widget/ConcatAdapterController;->calculateAndUpdateStateRestorationPolicy()V

    .line 200
    const/4 v3, 0x1

    return v3
.end method
