.class public Landroidx/recyclerview/widget/DiffUtil$DiffResult;
.super Ljava/lang/Object;
.source "DiffUtil.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Landroidx/recyclerview/widget/DiffUtil;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x9
    name = "DiffResult"
.end annotation


# static fields
.field private static final FLAG_CHANGED:I = 0x2

.field private static final FLAG_MASK:I = 0xf

.field private static final FLAG_MOVED:I = 0xc

.field private static final FLAG_MOVED_CHANGED:I = 0x4

.field private static final FLAG_MOVED_NOT_CHANGED:I = 0x8

.field private static final FLAG_NOT_CHANGED:I = 0x1

.field private static final FLAG_OFFSET:I = 0x4

.field public static final NO_POSITION:I = -0x1


# instance fields
.field private final mCallback:Landroidx/recyclerview/widget/DiffUtil$Callback;

.field private final mDetectMoves:Z

.field private final mDiagonals:Ljava/util/List;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/List<",
            "Landroidx/recyclerview/widget/DiffUtil$Diagonal;",
            ">;"
        }
    .end annotation
.end field

.field private final mNewItemStatuses:[I

.field private final mNewListSize:I

.field private final mOldItemStatuses:[I

.field private final mOldListSize:I


# direct methods
.method constructor <init>(Landroidx/recyclerview/widget/DiffUtil$Callback;Ljava/util/List;[I[IZ)V
    .locals 1
    .param p1, "callback"    # Landroidx/recyclerview/widget/DiffUtil$Callback;
    .param p3, "oldItemStatuses"    # [I
    .param p4, "newItemStatuses"    # [I
    .param p5, "detectMoves"    # Z
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Landroidx/recyclerview/widget/DiffUtil$Callback;",
            "Ljava/util/List<",
            "Landroidx/recyclerview/widget/DiffUtil$Diagonal;",
            ">;[I[IZ)V"
        }
    .end annotation

    .line 664
    .local p2, "diagonals":Ljava/util/List;, "Ljava/util/List<Landroidx/recyclerview/widget/DiffUtil$Diagonal;>;"
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 665
    iput-object p2, p0, Landroidx/recyclerview/widget/DiffUtil$DiffResult;->mDiagonals:Ljava/util/List;

    .line 666
    iput-object p3, p0, Landroidx/recyclerview/widget/DiffUtil$DiffResult;->mOldItemStatuses:[I

    .line 667
    iput-object p4, p0, Landroidx/recyclerview/widget/DiffUtil$DiffResult;->mNewItemStatuses:[I

    .line 668
    const/4 v0, 0x0

    invoke-static {p3, v0}, Ljava/util/Arrays;->fill([II)V

    .line 669
    invoke-static {p4, v0}, Ljava/util/Arrays;->fill([II)V

    .line 670
    iput-object p1, p0, Landroidx/recyclerview/widget/DiffUtil$DiffResult;->mCallback:Landroidx/recyclerview/widget/DiffUtil$Callback;

    .line 671
    invoke-virtual {p1}, Landroidx/recyclerview/widget/DiffUtil$Callback;->getOldListSize()I

    move-result v0

    iput v0, p0, Landroidx/recyclerview/widget/DiffUtil$DiffResult;->mOldListSize:I

    .line 672
    invoke-virtual {p1}, Landroidx/recyclerview/widget/DiffUtil$Callback;->getNewListSize()I

    move-result v0

    iput v0, p0, Landroidx/recyclerview/widget/DiffUtil$DiffResult;->mNewListSize:I

    .line 673
    iput-boolean p5, p0, Landroidx/recyclerview/widget/DiffUtil$DiffResult;->mDetectMoves:Z

    .line 674
    invoke-direct {p0}, Landroidx/recyclerview/widget/DiffUtil$DiffResult;->addEdgeDiagonals()V

    .line 675
    invoke-direct {p0}, Landroidx/recyclerview/widget/DiffUtil$DiffResult;->findMatchingItems()V

    .line 676
    return-void
.end method

.method private addEdgeDiagonals()V
    .locals 6

    .line 683
    iget-object v0, p0, Landroidx/recyclerview/widget/DiffUtil$DiffResult;->mDiagonals:Ljava/util/List;

    invoke-interface {v0}, Ljava/util/List;->isEmpty()Z

    move-result v0

    const/4 v1, 0x0

    if-eqz v0, :cond_0

    const/4 v0, 0x0

    goto :goto_0

    :cond_0
    iget-object v0, p0, Landroidx/recyclerview/widget/DiffUtil$DiffResult;->mDiagonals:Ljava/util/List;

    invoke-interface {v0, v1}, Ljava/util/List;->get(I)Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Landroidx/recyclerview/widget/DiffUtil$Diagonal;

    .line 685
    .local v0, "first":Landroidx/recyclerview/widget/DiffUtil$Diagonal;
    :goto_0
    if-eqz v0, :cond_1

    iget v2, v0, Landroidx/recyclerview/widget/DiffUtil$Diagonal;->x:I

    if-nez v2, :cond_1

    iget v2, v0, Landroidx/recyclerview/widget/DiffUtil$Diagonal;->y:I

    if-eqz v2, :cond_2

    .line 686
    :cond_1
    iget-object v2, p0, Landroidx/recyclerview/widget/DiffUtil$DiffResult;->mDiagonals:Ljava/util/List;

    new-instance v3, Landroidx/recyclerview/widget/DiffUtil$Diagonal;

    invoke-direct {v3, v1, v1, v1}, Landroidx/recyclerview/widget/DiffUtil$Diagonal;-><init>(III)V

    invoke-interface {v2, v1, v3}, Ljava/util/List;->add(ILjava/lang/Object;)V

    .line 689
    :cond_2
    iget-object v2, p0, Landroidx/recyclerview/widget/DiffUtil$DiffResult;->mDiagonals:Ljava/util/List;

    new-instance v3, Landroidx/recyclerview/widget/DiffUtil$Diagonal;

    iget v4, p0, Landroidx/recyclerview/widget/DiffUtil$DiffResult;->mOldListSize:I

    iget v5, p0, Landroidx/recyclerview/widget/DiffUtil$DiffResult;->mNewListSize:I

    invoke-direct {v3, v4, v5, v1}, Landroidx/recyclerview/widget/DiffUtil$Diagonal;-><init>(III)V

    invoke-interface {v2, v3}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    .line 690
    return-void
.end method

.method private findMatchingAddition(I)V
    .locals 9
    .param p1, "posX"    # I

    .line 738
    const/4 v0, 0x0

    .line 739
    .local v0, "posY":I
    iget-object v1, p0, Landroidx/recyclerview/widget/DiffUtil$DiffResult;->mDiagonals:Ljava/util/List;

    invoke-interface {v1}, Ljava/util/List;->size()I

    move-result v1

    .line 740
    .local v1, "diagonalsSize":I
    const/4 v2, 0x0

    .local v2, "i":I
    :goto_0
    if-ge v2, v1, :cond_3

    .line 741
    iget-object v3, p0, Landroidx/recyclerview/widget/DiffUtil$DiffResult;->mDiagonals:Ljava/util/List;

    invoke-interface {v3, v2}, Ljava/util/List;->get(I)Ljava/lang/Object;

    move-result-object v3

    check-cast v3, Landroidx/recyclerview/widget/DiffUtil$Diagonal;

    .line 742
    .local v3, "diagonal":Landroidx/recyclerview/widget/DiffUtil$Diagonal;
    :goto_1
    iget v4, v3, Landroidx/recyclerview/widget/DiffUtil$Diagonal;->y:I

    if-ge v0, v4, :cond_2

    .line 744
    iget-object v4, p0, Landroidx/recyclerview/widget/DiffUtil$DiffResult;->mNewItemStatuses:[I

    aget v4, v4, v0

    if-nez v4, :cond_1

    .line 745
    iget-object v4, p0, Landroidx/recyclerview/widget/DiffUtil$DiffResult;->mCallback:Landroidx/recyclerview/widget/DiffUtil$Callback;

    invoke-virtual {v4, p1, v0}, Landroidx/recyclerview/widget/DiffUtil$Callback;->areItemsTheSame(II)Z

    move-result v4

    .line 746
    .local v4, "matching":Z
    if-eqz v4, :cond_1

    .line 748
    iget-object v5, p0, Landroidx/recyclerview/widget/DiffUtil$DiffResult;->mCallback:Landroidx/recyclerview/widget/DiffUtil$Callback;

    invoke-virtual {v5, p1, v0}, Landroidx/recyclerview/widget/DiffUtil$Callback;->areContentsTheSame(II)Z

    move-result v5

    .line 749
    .local v5, "contentsMatching":Z
    if-eqz v5, :cond_0

    const/16 v6, 0x8

    goto :goto_2

    .line 750
    :cond_0
    const/4 v6, 0x4

    :goto_2
    nop

    .line 752
    .local v6, "changeFlag":I
    iget-object v7, p0, Landroidx/recyclerview/widget/DiffUtil$DiffResult;->mOldItemStatuses:[I

    shl-int/lit8 v8, v0, 0x4

    or-int/2addr v8, v6

    aput v8, v7, p1

    .line 753
    iget-object v7, p0, Landroidx/recyclerview/widget/DiffUtil$DiffResult;->mNewItemStatuses:[I

    shl-int/lit8 v8, p1, 0x4

    or-int/2addr v8, v6

    aput v8, v7, v0

    .line 754
    return-void

    .line 757
    .end local v4    # "matching":Z
    .end local v5    # "contentsMatching":Z
    .end local v6    # "changeFlag":I
    :cond_1
    add-int/lit8 v0, v0, 0x1

    goto :goto_1

    .line 759
    :cond_2
    invoke-virtual {v3}, Landroidx/recyclerview/widget/DiffUtil$Diagonal;->endY()I

    move-result v0

    .line 740
    .end local v3    # "diagonal":Landroidx/recyclerview/widget/DiffUtil$Diagonal;
    add-int/lit8 v2, v2, 0x1

    goto :goto_0

    .line 761
    .end local v2    # "i":I
    :cond_3
    return-void
.end method

.method private findMatchingItems()V
    .locals 9

    .line 698
    iget-object v0, p0, Landroidx/recyclerview/widget/DiffUtil$DiffResult;->mDiagonals:Ljava/util/List;

    invoke-interface {v0}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v0

    :goto_0
    invoke-interface {v0}, Ljava/util/Iterator;->hasNext()Z

    move-result v1

    if-eqz v1, :cond_2

    invoke-interface {v0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Landroidx/recyclerview/widget/DiffUtil$Diagonal;

    .line 699
    .local v1, "diagonal":Landroidx/recyclerview/widget/DiffUtil$Diagonal;
    const/4 v2, 0x0

    .local v2, "offset":I
    :goto_1
    iget v3, v1, Landroidx/recyclerview/widget/DiffUtil$Diagonal;->size:I

    if-ge v2, v3, :cond_1

    .line 700
    iget v3, v1, Landroidx/recyclerview/widget/DiffUtil$Diagonal;->x:I

    add-int/2addr v3, v2

    .line 701
    .local v3, "posX":I
    iget v4, v1, Landroidx/recyclerview/widget/DiffUtil$Diagonal;->y:I

    add-int/2addr v4, v2

    .line 702
    .local v4, "posY":I
    iget-object v5, p0, Landroidx/recyclerview/widget/DiffUtil$DiffResult;->mCallback:Landroidx/recyclerview/widget/DiffUtil$Callback;

    invoke-virtual {v5, v3, v4}, Landroidx/recyclerview/widget/DiffUtil$Callback;->areContentsTheSame(II)Z

    move-result v5

    .line 703
    .local v5, "theSame":Z
    if-eqz v5, :cond_0

    const/4 v6, 0x1

    goto :goto_2

    :cond_0
    const/4 v6, 0x2

    .line 704
    .local v6, "changeFlag":I
    :goto_2
    iget-object v7, p0, Landroidx/recyclerview/widget/DiffUtil$DiffResult;->mOldItemStatuses:[I

    shl-int/lit8 v8, v4, 0x4

    or-int/2addr v8, v6

    aput v8, v7, v3

    .line 705
    iget-object v7, p0, Landroidx/recyclerview/widget/DiffUtil$DiffResult;->mNewItemStatuses:[I

    shl-int/lit8 v8, v3, 0x4

    or-int/2addr v8, v6

    aput v8, v7, v4

    .line 699
    .end local v3    # "posX":I
    .end local v4    # "posY":I
    .end local v5    # "theSame":Z
    .end local v6    # "changeFlag":I
    add-int/lit8 v2, v2, 0x1

    goto :goto_1

    .line 707
    .end local v1    # "diagonal":Landroidx/recyclerview/widget/DiffUtil$Diagonal;
    .end local v2    # "offset":I
    :cond_1
    goto :goto_0

    .line 709
    :cond_2
    iget-boolean v0, p0, Landroidx/recyclerview/widget/DiffUtil$DiffResult;->mDetectMoves:Z

    if-eqz v0, :cond_3

    .line 712
    invoke-direct {p0}, Landroidx/recyclerview/widget/DiffUtil$DiffResult;->findMoveMatches()V

    .line 714
    :cond_3
    return-void
.end method

.method private findMoveMatches()V
    .locals 4

    .line 718
    const/4 v0, 0x0

    .line 719
    .local v0, "posX":I
    iget-object v1, p0, Landroidx/recyclerview/widget/DiffUtil$DiffResult;->mDiagonals:Ljava/util/List;

    invoke-interface {v1}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v1

    :goto_0
    invoke-interface {v1}, Ljava/util/Iterator;->hasNext()Z

    move-result v2

    if-eqz v2, :cond_2

    invoke-interface {v1}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Landroidx/recyclerview/widget/DiffUtil$Diagonal;

    .line 720
    .local v2, "diagonal":Landroidx/recyclerview/widget/DiffUtil$Diagonal;
    :goto_1
    iget v3, v2, Landroidx/recyclerview/widget/DiffUtil$Diagonal;->x:I

    if-ge v0, v3, :cond_1

    .line 721
    iget-object v3, p0, Landroidx/recyclerview/widget/DiffUtil$DiffResult;->mOldItemStatuses:[I

    aget v3, v3, v0

    if-nez v3, :cond_0

    .line 723
    invoke-direct {p0, v0}, Landroidx/recyclerview/widget/DiffUtil$DiffResult;->findMatchingAddition(I)V

    .line 725
    :cond_0
    add-int/lit8 v0, v0, 0x1

    goto :goto_1

    .line 728
    :cond_1
    invoke-virtual {v2}, Landroidx/recyclerview/widget/DiffUtil$Diagonal;->endX()I

    move-result v0

    .line 729
    .end local v2    # "diagonal":Landroidx/recyclerview/widget/DiffUtil$Diagonal;
    goto :goto_0

    .line 730
    :cond_2
    return-void
.end method

.method private static getPostponedUpdate(Ljava/util/Collection;IZ)Landroidx/recyclerview/widget/DiffUtil$PostponedUpdate;
    .locals 4
    .param p1, "posInList"    # I
    .param p2, "removal"    # Z
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/Collection<",
            "Landroidx/recyclerview/widget/DiffUtil$PostponedUpdate;",
            ">;IZ)",
            "Landroidx/recyclerview/widget/DiffUtil$PostponedUpdate;"
        }
    .end annotation

    .line 975
    .local p0, "postponedUpdates":Ljava/util/Collection;, "Ljava/util/Collection<Landroidx/recyclerview/widget/DiffUtil$PostponedUpdate;>;"
    const/4 v0, 0x0

    .line 976
    .local v0, "postponedUpdate":Landroidx/recyclerview/widget/DiffUtil$PostponedUpdate;
    invoke-interface {p0}, Ljava/util/Collection;->iterator()Ljava/util/Iterator;

    move-result-object v1

    .line 977
    .local v1, "itr":Ljava/util/Iterator;, "Ljava/util/Iterator<Landroidx/recyclerview/widget/DiffUtil$PostponedUpdate;>;"
    :goto_0
    invoke-interface {v1}, Ljava/util/Iterator;->hasNext()Z

    move-result v2

    if-eqz v2, :cond_1

    .line 978
    invoke-interface {v1}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Landroidx/recyclerview/widget/DiffUtil$PostponedUpdate;

    .line 979
    .local v2, "update":Landroidx/recyclerview/widget/DiffUtil$PostponedUpdate;
    iget v3, v2, Landroidx/recyclerview/widget/DiffUtil$PostponedUpdate;->posInOwnerList:I

    if-ne v3, p1, :cond_0

    iget-boolean v3, v2, Landroidx/recyclerview/widget/DiffUtil$PostponedUpdate;->removal:Z

    if-ne v3, p2, :cond_0

    .line 980
    move-object v0, v2

    .line 981
    invoke-interface {v1}, Ljava/util/Iterator;->remove()V

    .line 982
    goto :goto_1

    .line 984
    .end local v2    # "update":Landroidx/recyclerview/widget/DiffUtil$PostponedUpdate;
    :cond_0
    goto :goto_0

    .line 985
    :cond_1
    :goto_1
    invoke-interface {v1}, Ljava/util/Iterator;->hasNext()Z

    move-result v2

    if-eqz v2, :cond_3

    .line 987
    invoke-interface {v1}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Landroidx/recyclerview/widget/DiffUtil$PostponedUpdate;

    .line 988
    .restart local v2    # "update":Landroidx/recyclerview/widget/DiffUtil$PostponedUpdate;
    if-eqz p2, :cond_2

    .line 989
    iget v3, v2, Landroidx/recyclerview/widget/DiffUtil$PostponedUpdate;->currentPos:I

    add-int/lit8 v3, v3, -0x1

    iput v3, v2, Landroidx/recyclerview/widget/DiffUtil$PostponedUpdate;->currentPos:I

    goto :goto_2

    .line 991
    :cond_2
    iget v3, v2, Landroidx/recyclerview/widget/DiffUtil$PostponedUpdate;->currentPos:I

    add-int/lit8 v3, v3, 0x1

    iput v3, v2, Landroidx/recyclerview/widget/DiffUtil$PostponedUpdate;->currentPos:I

    .line 993
    .end local v2    # "update":Landroidx/recyclerview/widget/DiffUtil$PostponedUpdate;
    :goto_2
    goto :goto_1

    .line 994
    :cond_3
    return-object v0
.end method


# virtual methods
.method public convertNewPositionToOld(I)I
    .locals 3
    .param p1, "newListPosition"    # I

    .line 795
    if-ltz p1, :cond_1

    iget v0, p0, Landroidx/recyclerview/widget/DiffUtil$DiffResult;->mNewListSize:I

    if-ge p1, v0, :cond_1

    .line 799
    iget-object v0, p0, Landroidx/recyclerview/widget/DiffUtil$DiffResult;->mNewItemStatuses:[I

    aget v0, v0, p1

    .line 800
    .local v0, "status":I
    and-int/lit8 v1, v0, 0xf

    if-nez v1, :cond_0

    .line 801
    const/4 v1, -0x1

    return v1

    .line 803
    :cond_0
    shr-int/lit8 v1, v0, 0x4

    return v1

    .line 796
    .end local v0    # "status":I
    :cond_1
    new-instance v0, Ljava/lang/IndexOutOfBoundsException;

    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "Index out of bounds - passed position = "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v1

    invoke-virtual {v1, p1}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    move-result-object v1

    const-string v2, ", new list size = "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v1

    iget v2, p0, Landroidx/recyclerview/widget/DiffUtil$DiffResult;->mNewListSize:I

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    move-result-object v1

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-direct {v0, v1}, Ljava/lang/IndexOutOfBoundsException;-><init>(Ljava/lang/String;)V

    throw v0
.end method

.method public convertOldPositionToNew(I)I
    .locals 3
    .param p1, "oldListPosition"    # I

    .line 773
    if-ltz p1, :cond_1

    iget v0, p0, Landroidx/recyclerview/widget/DiffUtil$DiffResult;->mOldListSize:I

    if-ge p1, v0, :cond_1

    .line 777
    iget-object v0, p0, Landroidx/recyclerview/widget/DiffUtil$DiffResult;->mOldItemStatuses:[I

    aget v0, v0, p1

    .line 778
    .local v0, "status":I
    and-int/lit8 v1, v0, 0xf

    if-nez v1, :cond_0

    .line 779
    const/4 v1, -0x1

    return v1

    .line 781
    :cond_0
    shr-int/lit8 v1, v0, 0x4

    return v1

    .line 774
    .end local v0    # "status":I
    :cond_1
    new-instance v0, Ljava/lang/IndexOutOfBoundsException;

    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "Index out of bounds - passed position = "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v1

    invoke-virtual {v1, p1}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    move-result-object v1

    const-string v2, ", old list size = "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v1

    iget v2, p0, Landroidx/recyclerview/widget/DiffUtil$DiffResult;->mOldListSize:I

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    move-result-object v1

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-direct {v0, v1}, Ljava/lang/IndexOutOfBoundsException;-><init>(Ljava/lang/String;)V

    throw v0
.end method

.method public dispatchUpdatesTo(Landroidx/recyclerview/widget/ListUpdateCallback;)V
    .locals 18
    .param p1, "updateCallback"    # Landroidx/recyclerview/widget/ListUpdateCallback;

    .line 855
    move-object/from16 v0, p0

    move-object/from16 v1, p1

    instance-of v2, v1, Landroidx/recyclerview/widget/BatchingListUpdateCallback;

    if-eqz v2, :cond_0

    .line 856
    move-object v2, v1

    check-cast v2, Landroidx/recyclerview/widget/BatchingListUpdateCallback;

    .local v2, "batchingCallback":Landroidx/recyclerview/widget/BatchingListUpdateCallback;
    goto :goto_0

    .line 858
    .end local v2    # "batchingCallback":Landroidx/recyclerview/widget/BatchingListUpdateCallback;
    :cond_0
    new-instance v2, Landroidx/recyclerview/widget/BatchingListUpdateCallback;

    invoke-direct {v2, v1}, Landroidx/recyclerview/widget/BatchingListUpdateCallback;-><init>(Landroidx/recyclerview/widget/ListUpdateCallback;)V

    .line 862
    .restart local v2    # "batchingCallback":Landroidx/recyclerview/widget/BatchingListUpdateCallback;
    move-object v1, v2

    .line 868
    .end local p1    # "updateCallback":Landroidx/recyclerview/widget/ListUpdateCallback;
    .local v1, "updateCallback":Landroidx/recyclerview/widget/ListUpdateCallback;
    :goto_0
    iget v3, v0, Landroidx/recyclerview/widget/DiffUtil$DiffResult;->mOldListSize:I

    .line 870
    .local v3, "currentListSize":I
    new-instance v4, Ljava/util/ArrayDeque;

    invoke-direct {v4}, Ljava/util/ArrayDeque;-><init>()V

    .line 872
    .local v4, "postponedUpdates":Ljava/util/Collection;, "Ljava/util/Collection<Landroidx/recyclerview/widget/DiffUtil$PostponedUpdate;>;"
    iget v5, v0, Landroidx/recyclerview/widget/DiffUtil$DiffResult;->mOldListSize:I

    .line 873
    .local v5, "posX":I
    iget v6, v0, Landroidx/recyclerview/widget/DiffUtil$DiffResult;->mNewListSize:I

    .line 877
    .local v6, "posY":I
    iget-object v7, v0, Landroidx/recyclerview/widget/DiffUtil$DiffResult;->mDiagonals:Ljava/util/List;

    invoke-interface {v7}, Ljava/util/List;->size()I

    move-result v7

    const/4 v8, 0x1

    sub-int/2addr v7, v8

    .local v7, "diagonalIndex":I
    :goto_1
    if-ltz v7, :cond_b

    .line 878
    iget-object v9, v0, Landroidx/recyclerview/widget/DiffUtil$DiffResult;->mDiagonals:Ljava/util/List;

    invoke-interface {v9, v7}, Ljava/util/List;->get(I)Ljava/lang/Object;

    move-result-object v9

    check-cast v9, Landroidx/recyclerview/widget/DiffUtil$Diagonal;

    .line 879
    .local v9, "diagonal":Landroidx/recyclerview/widget/DiffUtil$Diagonal;
    invoke-virtual {v9}, Landroidx/recyclerview/widget/DiffUtil$Diagonal;->endX()I

    move-result v10

    .line 880
    .local v10, "endX":I
    invoke-virtual {v9}, Landroidx/recyclerview/widget/DiffUtil$Diagonal;->endY()I

    move-result v11

    .line 884
    .local v11, "endY":I
    :goto_2
    const/4 v12, 0x0

    if-le v5, v10, :cond_4

    .line 885
    add-int/lit8 v5, v5, -0x1

    .line 887
    iget-object v13, v0, Landroidx/recyclerview/widget/DiffUtil$DiffResult;->mOldItemStatuses:[I

    aget v13, v13, v5

    .line 888
    .local v13, "status":I
    and-int/lit8 v14, v13, 0xc

    if-eqz v14, :cond_3

    .line 889
    shr-int/lit8 v14, v13, 0x4

    .line 891
    .local v14, "newPos":I
    invoke-static {v4, v14, v12}, Landroidx/recyclerview/widget/DiffUtil$DiffResult;->getPostponedUpdate(Ljava/util/Collection;IZ)Landroidx/recyclerview/widget/DiffUtil$PostponedUpdate;

    move-result-object v12

    .line 893
    .local v12, "postponedUpdate":Landroidx/recyclerview/widget/DiffUtil$PostponedUpdate;
    if-eqz v12, :cond_2

    .line 895
    iget v15, v12, Landroidx/recyclerview/widget/DiffUtil$PostponedUpdate;->currentPos:I

    sub-int v15, v3, v15

    .line 896
    .local v15, "updatedNewPos":I
    add-int/lit8 v8, v15, -0x1

    invoke-virtual {v2, v5, v8}, Landroidx/recyclerview/widget/BatchingListUpdateCallback;->onMoved(II)V

    .line 897
    and-int/lit8 v8, v13, 0x4

    if-eqz v8, :cond_1

    .line 898
    iget-object v8, v0, Landroidx/recyclerview/widget/DiffUtil$DiffResult;->mCallback:Landroidx/recyclerview/widget/DiffUtil$Callback;

    invoke-virtual {v8, v5, v14}, Landroidx/recyclerview/widget/DiffUtil$Callback;->getChangePayload(II)Ljava/lang/Object;

    move-result-object v8

    .line 899
    .local v8, "changePayload":Ljava/lang/Object;
    move-object/from16 v16, v1

    .end local v1    # "updateCallback":Landroidx/recyclerview/widget/ListUpdateCallback;
    .local v16, "updateCallback":Landroidx/recyclerview/widget/ListUpdateCallback;
    add-int/lit8 v1, v15, -0x1

    move/from16 v17, v6

    const/4 v6, 0x1

    .end local v6    # "posY":I
    .local v17, "posY":I
    invoke-virtual {v2, v1, v6, v8}, Landroidx/recyclerview/widget/BatchingListUpdateCallback;->onChanged(IILjava/lang/Object;)V

    goto :goto_3

    .line 897
    .end local v8    # "changePayload":Ljava/lang/Object;
    .end local v16    # "updateCallback":Landroidx/recyclerview/widget/ListUpdateCallback;
    .end local v17    # "posY":I
    .restart local v1    # "updateCallback":Landroidx/recyclerview/widget/ListUpdateCallback;
    .restart local v6    # "posY":I
    :cond_1
    move-object/from16 v16, v1

    move/from16 v17, v6

    const/4 v6, 0x1

    .line 901
    .end local v1    # "updateCallback":Landroidx/recyclerview/widget/ListUpdateCallback;
    .end local v6    # "posY":I
    .end local v15    # "updatedNewPos":I
    .restart local v16    # "updateCallback":Landroidx/recyclerview/widget/ListUpdateCallback;
    .restart local v17    # "posY":I
    :goto_3
    goto :goto_4

    .line 903
    .end local v16    # "updateCallback":Landroidx/recyclerview/widget/ListUpdateCallback;
    .end local v17    # "posY":I
    .restart local v1    # "updateCallback":Landroidx/recyclerview/widget/ListUpdateCallback;
    .restart local v6    # "posY":I
    :cond_2
    move-object/from16 v16, v1

    move/from16 v17, v6

    const/4 v6, 0x1

    .end local v1    # "updateCallback":Landroidx/recyclerview/widget/ListUpdateCallback;
    .end local v6    # "posY":I
    .restart local v16    # "updateCallback":Landroidx/recyclerview/widget/ListUpdateCallback;
    .restart local v17    # "posY":I
    new-instance v1, Landroidx/recyclerview/widget/DiffUtil$PostponedUpdate;

    sub-int v8, v3, v5

    sub-int/2addr v8, v6

    invoke-direct {v1, v5, v8, v6}, Landroidx/recyclerview/widget/DiffUtil$PostponedUpdate;-><init>(IIZ)V

    invoke-interface {v4, v1}, Ljava/util/Collection;->add(Ljava/lang/Object;)Z

    .line 909
    .end local v12    # "postponedUpdate":Landroidx/recyclerview/widget/DiffUtil$PostponedUpdate;
    .end local v14    # "newPos":I
    :goto_4
    goto :goto_5

    .line 911
    .end local v16    # "updateCallback":Landroidx/recyclerview/widget/ListUpdateCallback;
    .end local v17    # "posY":I
    .restart local v1    # "updateCallback":Landroidx/recyclerview/widget/ListUpdateCallback;
    .restart local v6    # "posY":I
    :cond_3
    move-object/from16 v16, v1

    move/from16 v17, v6

    const/4 v6, 0x1

    .end local v1    # "updateCallback":Landroidx/recyclerview/widget/ListUpdateCallback;
    .end local v6    # "posY":I
    .restart local v16    # "updateCallback":Landroidx/recyclerview/widget/ListUpdateCallback;
    .restart local v17    # "posY":I
    invoke-virtual {v2, v5, v6}, Landroidx/recyclerview/widget/BatchingListUpdateCallback;->onRemoved(II)V

    .line 912
    add-int/lit8 v3, v3, -0x1

    .line 914
    .end local v13    # "status":I
    :goto_5
    move-object/from16 v1, v16

    move/from16 v6, v17

    const/4 v8, 0x1

    goto :goto_2

    .line 884
    .end local v16    # "updateCallback":Landroidx/recyclerview/widget/ListUpdateCallback;
    .end local v17    # "posY":I
    .restart local v1    # "updateCallback":Landroidx/recyclerview/widget/ListUpdateCallback;
    .restart local v6    # "posY":I
    :cond_4
    move-object/from16 v16, v1

    move/from16 v17, v6

    .line 915
    .end local v1    # "updateCallback":Landroidx/recyclerview/widget/ListUpdateCallback;
    .restart local v16    # "updateCallback":Landroidx/recyclerview/widget/ListUpdateCallback;
    :goto_6
    if-le v6, v11, :cond_8

    .line 916
    add-int/lit8 v6, v6, -0x1

    .line 918
    iget-object v1, v0, Landroidx/recyclerview/widget/DiffUtil$DiffResult;->mNewItemStatuses:[I

    aget v1, v1, v6

    .line 919
    .local v1, "status":I
    and-int/lit8 v8, v1, 0xc

    if-eqz v8, :cond_7

    .line 922
    shr-int/lit8 v8, v1, 0x4

    .line 924
    .local v8, "oldPos":I
    const/4 v13, 0x1

    invoke-static {v4, v8, v13}, Landroidx/recyclerview/widget/DiffUtil$DiffResult;->getPostponedUpdate(Ljava/util/Collection;IZ)Landroidx/recyclerview/widget/DiffUtil$PostponedUpdate;

    move-result-object v14

    .line 927
    .local v14, "postponedUpdate":Landroidx/recyclerview/widget/DiffUtil$PostponedUpdate;
    if-nez v14, :cond_5

    .line 929
    new-instance v13, Landroidx/recyclerview/widget/DiffUtil$PostponedUpdate;

    sub-int v15, v3, v5

    invoke-direct {v13, v6, v15, v12}, Landroidx/recyclerview/widget/DiffUtil$PostponedUpdate;-><init>(IIZ)V

    invoke-interface {v4, v13}, Ljava/util/Collection;->add(Ljava/lang/Object;)Z

    const/4 v15, 0x1

    goto :goto_7

    .line 938
    :cond_5
    iget v13, v14, Landroidx/recyclerview/widget/DiffUtil$PostponedUpdate;->currentPos:I

    sub-int v13, v3, v13

    const/4 v15, 0x1

    sub-int/2addr v13, v15

    .line 939
    .local v13, "updatedOldPos":I
    invoke-virtual {v2, v13, v5}, Landroidx/recyclerview/widget/BatchingListUpdateCallback;->onMoved(II)V

    .line 940
    and-int/lit8 v17, v1, 0x4

    if-eqz v17, :cond_6

    .line 941
    iget-object v12, v0, Landroidx/recyclerview/widget/DiffUtil$DiffResult;->mCallback:Landroidx/recyclerview/widget/DiffUtil$Callback;

    invoke-virtual {v12, v8, v6}, Landroidx/recyclerview/widget/DiffUtil$Callback;->getChangePayload(II)Ljava/lang/Object;

    move-result-object v12

    .line 942
    .local v12, "changePayload":Ljava/lang/Object;
    invoke-virtual {v2, v5, v15, v12}, Landroidx/recyclerview/widget/BatchingListUpdateCallback;->onChanged(IILjava/lang/Object;)V

    .line 945
    .end local v8    # "oldPos":I
    .end local v12    # "changePayload":Ljava/lang/Object;
    .end local v13    # "updatedOldPos":I
    .end local v14    # "postponedUpdate":Landroidx/recyclerview/widget/DiffUtil$PostponedUpdate;
    :cond_6
    :goto_7
    goto :goto_8

    .line 947
    :cond_7
    const/4 v15, 0x1

    invoke-virtual {v2, v5, v15}, Landroidx/recyclerview/widget/BatchingListUpdateCallback;->onInserted(II)V

    .line 948
    add-int/lit8 v3, v3, 0x1

    .line 950
    .end local v1    # "status":I
    :goto_8
    const/4 v12, 0x0

    goto :goto_6

    .line 952
    :cond_8
    iget v1, v9, Landroidx/recyclerview/widget/DiffUtil$Diagonal;->x:I

    .line 953
    .end local v5    # "posX":I
    .local v1, "posX":I
    iget v5, v9, Landroidx/recyclerview/widget/DiffUtil$Diagonal;->y:I

    .line 954
    .end local v6    # "posY":I
    .local v5, "posY":I
    const/4 v6, 0x0

    .local v6, "i":I
    :goto_9
    iget v8, v9, Landroidx/recyclerview/widget/DiffUtil$Diagonal;->size:I

    if-ge v6, v8, :cond_a

    .line 956
    iget-object v8, v0, Landroidx/recyclerview/widget/DiffUtil$DiffResult;->mOldItemStatuses:[I

    aget v8, v8, v1

    and-int/lit8 v8, v8, 0xf

    const/4 v12, 0x2

    if-ne v8, v12, :cond_9

    .line 957
    iget-object v8, v0, Landroidx/recyclerview/widget/DiffUtil$DiffResult;->mCallback:Landroidx/recyclerview/widget/DiffUtil$Callback;

    invoke-virtual {v8, v1, v5}, Landroidx/recyclerview/widget/DiffUtil$Callback;->getChangePayload(II)Ljava/lang/Object;

    move-result-object v8

    .line 958
    .local v8, "changePayload":Ljava/lang/Object;
    const/4 v12, 0x1

    invoke-virtual {v2, v1, v12, v8}, Landroidx/recyclerview/widget/BatchingListUpdateCallback;->onChanged(IILjava/lang/Object;)V

    goto :goto_a

    .line 956
    .end local v8    # "changePayload":Ljava/lang/Object;
    :cond_9
    const/4 v12, 0x1

    .line 960
    :goto_a
    add-int/lit8 v1, v1, 0x1

    .line 961
    add-int/lit8 v5, v5, 0x1

    .line 954
    add-int/lit8 v6, v6, 0x1

    goto :goto_9

    :cond_a
    const/4 v12, 0x1

    .line 964
    .end local v6    # "i":I
    iget v1, v9, Landroidx/recyclerview/widget/DiffUtil$Diagonal;->x:I

    .line 965
    iget v6, v9, Landroidx/recyclerview/widget/DiffUtil$Diagonal;->y:I

    .line 877
    .end local v5    # "posY":I
    .end local v9    # "diagonal":Landroidx/recyclerview/widget/DiffUtil$Diagonal;
    .end local v10    # "endX":I
    .end local v11    # "endY":I
    .local v6, "posY":I
    add-int/lit8 v7, v7, -0x1

    move v5, v1

    move-object/from16 v1, v16

    const/4 v8, 0x1

    goto/16 :goto_1

    .line 967
    .end local v7    # "diagonalIndex":I
    .end local v16    # "updateCallback":Landroidx/recyclerview/widget/ListUpdateCallback;
    .local v1, "updateCallback":Landroidx/recyclerview/widget/ListUpdateCallback;
    .local v5, "posX":I
    :cond_b
    invoke-virtual {v2}, Landroidx/recyclerview/widget/BatchingListUpdateCallback;->dispatchLastEvent()V

    .line 968
    return-void
.end method

.method public dispatchUpdatesTo(Landroidx/recyclerview/widget/RecyclerView$Adapter;)V
    .locals 1
    .param p1, "adapter"    # Landroidx/recyclerview/widget/RecyclerView$Adapter;

    .line 840
    new-instance v0, Landroidx/recyclerview/widget/AdapterListUpdateCallback;

    invoke-direct {v0, p1}, Landroidx/recyclerview/widget/AdapterListUpdateCallback;-><init>(Landroidx/recyclerview/widget/RecyclerView$Adapter;)V

    invoke-virtual {p0, v0}, Landroidx/recyclerview/widget/DiffUtil$DiffResult;->dispatchUpdatesTo(Landroidx/recyclerview/widget/ListUpdateCallback;)V

    .line 841
    return-void
.end method
