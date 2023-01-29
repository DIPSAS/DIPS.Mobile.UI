.class public Landroidx/loader/content/Loader;
.super Ljava/lang/Object;
.source "Loader.java"


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Landroidx/loader/content/Loader$OnLoadCanceledListener;,
        Landroidx/loader/content/Loader$OnLoadCompleteListener;,
        Landroidx/loader/content/Loader$ForceLoadContentObserver;
    }
.end annotation

.annotation system Ldalvik/annotation/Signature;
    value = {
        "<D:",
        "Ljava/lang/Object;",
        ">",
        "Ljava/lang/Object;"
    }
.end annotation


# instance fields
.field private mAbandoned:Z

.field private mContentChanged:Z

.field private mContext:Landroid/content/Context;

.field private mId:I

.field private mListener:Landroidx/loader/content/Loader$OnLoadCompleteListener;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Landroidx/loader/content/Loader$OnLoadCompleteListener<",
            "TD;>;"
        }
    .end annotation
.end field

.field private mOnLoadCanceledListener:Landroidx/loader/content/Loader$OnLoadCanceledListener;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Landroidx/loader/content/Loader$OnLoadCanceledListener<",
            "TD;>;"
        }
    .end annotation
.end field

.field private mProcessingChange:Z

.field private mReset:Z

.field private mStarted:Z


# direct methods
.method public constructor <init>(Landroid/content/Context;)V
    .locals 2
    .param p1, "context"    # Landroid/content/Context;

    .line 117
    .local p0, "this":Landroidx/loader/content/Loader;, "Landroidx/loader/content/Loader<TD;>;"
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 43
    const/4 v0, 0x0

    iput-boolean v0, p0, Landroidx/loader/content/Loader;->mStarted:Z

    .line 44
    iput-boolean v0, p0, Landroidx/loader/content/Loader;->mAbandoned:Z

    .line 45
    const/4 v1, 0x1

    iput-boolean v1, p0, Landroidx/loader/content/Loader;->mReset:Z

    .line 46
    iput-boolean v0, p0, Landroidx/loader/content/Loader;->mContentChanged:Z

    .line 47
    iput-boolean v0, p0, Landroidx/loader/content/Loader;->mProcessingChange:Z

    .line 118
    invoke-virtual {p1}, Landroid/content/Context;->getApplicationContext()Landroid/content/Context;

    move-result-object v0

    iput-object v0, p0, Landroidx/loader/content/Loader;->mContext:Landroid/content/Context;

    .line 119
    return-void
.end method


# virtual methods
.method public abandon()V
    .locals 1

    .line 408
    .local p0, "this":Landroidx/loader/content/Loader;, "Landroidx/loader/content/Loader<TD;>;"
    const/4 v0, 0x1

    iput-boolean v0, p0, Landroidx/loader/content/Loader;->mAbandoned:Z

    .line 409
    invoke-virtual {p0}, Landroidx/loader/content/Loader;->onAbandon()V

    .line 410
    return-void
.end method

.method public cancelLoad()Z
    .locals 1

    .line 317
    .local p0, "this":Landroidx/loader/content/Loader;, "Landroidx/loader/content/Loader<TD;>;"
    invoke-virtual {p0}, Landroidx/loader/content/Loader;->onCancelLoad()Z

    move-result v0

    return v0
.end method

.method public commitContentChanged()V
    .locals 1

    .line 484
    .local p0, "this":Landroidx/loader/content/Loader;, "Landroidx/loader/content/Loader<TD;>;"
    const/4 v0, 0x0

    iput-boolean v0, p0, Landroidx/loader/content/Loader;->mProcessingChange:Z

    .line 485
    return-void
.end method

.method public dataToString(Ljava/lang/Object;)Ljava/lang/String;
    .locals 3
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(TD;)",
            "Ljava/lang/String;"
        }
    .end annotation

    .line 526
    .local p0, "this":Landroidx/loader/content/Loader;, "Landroidx/loader/content/Loader<TD;>;"
    .local p1, "data":Ljava/lang/Object;, "TD;"
    new-instance v0, Ljava/lang/StringBuilder;

    const/16 v1, 0x40

    invoke-direct {v0, v1}, Ljava/lang/StringBuilder;-><init>(I)V

    .line 527
    .local v0, "sb":Ljava/lang/StringBuilder;
    if-nez p1, :cond_0

    .line 528
    const-string v1, "null"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    goto :goto_0

    .line 530
    :cond_0
    invoke-virtual {p1}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    move-result-object v1

    .line 531
    .local v1, "cls":Ljava/lang/Class;
    invoke-virtual {v1}, Ljava/lang/Class;->getSimpleName()Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v0, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    .line 532
    const-string v2, "{"

    invoke-virtual {v0, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    .line 533
    invoke-static {v1}, Ljava/lang/System;->identityHashCode(Ljava/lang/Object;)I

    move-result v2

    invoke-static {v2}, Ljava/lang/Integer;->toHexString(I)Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v0, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    .line 534
    const-string v2, "}"

    invoke-virtual {v0, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    .line 536
    .end local v1    # "cls":Ljava/lang/Class;
    :goto_0
    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    return-object v1
.end method

.method public deliverCancellation()V
    .locals 1

    .line 143
    .local p0, "this":Landroidx/loader/content/Loader;, "Landroidx/loader/content/Loader<TD;>;"
    iget-object v0, p0, Landroidx/loader/content/Loader;->mOnLoadCanceledListener:Landroidx/loader/content/Loader$OnLoadCanceledListener;

    if-eqz v0, :cond_0

    .line 144
    invoke-interface {v0, p0}, Landroidx/loader/content/Loader$OnLoadCanceledListener;->onLoadCanceled(Landroidx/loader/content/Loader;)V

    .line 146
    :cond_0
    return-void
.end method

.method public deliverResult(Ljava/lang/Object;)V
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(TD;)V"
        }
    .end annotation

    .line 130
    .local p0, "this":Landroidx/loader/content/Loader;, "Landroidx/loader/content/Loader<TD;>;"
    .local p1, "data":Ljava/lang/Object;, "TD;"
    iget-object v0, p0, Landroidx/loader/content/Loader;->mListener:Landroidx/loader/content/Loader$OnLoadCompleteListener;

    if-eqz v0, :cond_0

    .line 131
    invoke-interface {v0, p0, p1}, Landroidx/loader/content/Loader$OnLoadCompleteListener;->onLoadComplete(Landroidx/loader/content/Loader;Ljava/lang/Object;)V

    .line 133
    :cond_0
    return-void
.end method

.method public dump(Ljava/lang/String;Ljava/io/FileDescriptor;Ljava/io/PrintWriter;[Ljava/lang/String;)V
    .locals 1
    .param p1, "prefix"    # Ljava/lang/String;
    .param p2, "fd"    # Ljava/io/FileDescriptor;
    .param p3, "writer"    # Ljava/io/PrintWriter;
    .param p4, "args"    # [Ljava/lang/String;
    .annotation runtime Ljava/lang/Deprecated;
    .end annotation

    .line 566
    .local p0, "this":Landroidx/loader/content/Loader;, "Landroidx/loader/content/Loader<TD;>;"
    invoke-virtual {p3, p1}, Ljava/io/PrintWriter;->print(Ljava/lang/String;)V

    const-string v0, "mId="

    invoke-virtual {p3, v0}, Ljava/io/PrintWriter;->print(Ljava/lang/String;)V

    iget v0, p0, Landroidx/loader/content/Loader;->mId:I

    invoke-virtual {p3, v0}, Ljava/io/PrintWriter;->print(I)V

    .line 567
    const-string v0, " mListener="

    invoke-virtual {p3, v0}, Ljava/io/PrintWriter;->print(Ljava/lang/String;)V

    iget-object v0, p0, Landroidx/loader/content/Loader;->mListener:Landroidx/loader/content/Loader$OnLoadCompleteListener;

    invoke-virtual {p3, v0}, Ljava/io/PrintWriter;->println(Ljava/lang/Object;)V

    .line 568
    iget-boolean v0, p0, Landroidx/loader/content/Loader;->mStarted:Z

    if-nez v0, :cond_0

    iget-boolean v0, p0, Landroidx/loader/content/Loader;->mContentChanged:Z

    if-nez v0, :cond_0

    iget-boolean v0, p0, Landroidx/loader/content/Loader;->mProcessingChange:Z

    if-eqz v0, :cond_1

    .line 569
    :cond_0
    invoke-virtual {p3, p1}, Ljava/io/PrintWriter;->print(Ljava/lang/String;)V

    const-string v0, "mStarted="

    invoke-virtual {p3, v0}, Ljava/io/PrintWriter;->print(Ljava/lang/String;)V

    iget-boolean v0, p0, Landroidx/loader/content/Loader;->mStarted:Z

    invoke-virtual {p3, v0}, Ljava/io/PrintWriter;->print(Z)V

    .line 570
    const-string v0, " mContentChanged="

    invoke-virtual {p3, v0}, Ljava/io/PrintWriter;->print(Ljava/lang/String;)V

    iget-boolean v0, p0, Landroidx/loader/content/Loader;->mContentChanged:Z

    invoke-virtual {p3, v0}, Ljava/io/PrintWriter;->print(Z)V

    .line 571
    const-string v0, " mProcessingChange="

    invoke-virtual {p3, v0}, Ljava/io/PrintWriter;->print(Ljava/lang/String;)V

    iget-boolean v0, p0, Landroidx/loader/content/Loader;->mProcessingChange:Z

    invoke-virtual {p3, v0}, Ljava/io/PrintWriter;->println(Z)V

    .line 573
    :cond_1
    iget-boolean v0, p0, Landroidx/loader/content/Loader;->mAbandoned:Z

    if-nez v0, :cond_2

    iget-boolean v0, p0, Landroidx/loader/content/Loader;->mReset:Z

    if-eqz v0, :cond_3

    .line 574
    :cond_2
    invoke-virtual {p3, p1}, Ljava/io/PrintWriter;->print(Ljava/lang/String;)V

    const-string v0, "mAbandoned="

    invoke-virtual {p3, v0}, Ljava/io/PrintWriter;->print(Ljava/lang/String;)V

    iget-boolean v0, p0, Landroidx/loader/content/Loader;->mAbandoned:Z

    invoke-virtual {p3, v0}, Ljava/io/PrintWriter;->print(Z)V

    .line 575
    const-string v0, " mReset="

    invoke-virtual {p3, v0}, Ljava/io/PrintWriter;->print(Ljava/lang/String;)V

    iget-boolean v0, p0, Landroidx/loader/content/Loader;->mReset:Z

    invoke-virtual {p3, v0}, Ljava/io/PrintWriter;->println(Z)V

    .line 577
    :cond_3
    return-void
.end method

.method public forceLoad()V
    .locals 0

    .line 346
    .local p0, "this":Landroidx/loader/content/Loader;, "Landroidx/loader/content/Loader<TD;>;"
    invoke-virtual {p0}, Landroidx/loader/content/Loader;->onForceLoad()V

    .line 347
    return-void
.end method

.method public getContext()Landroid/content/Context;
    .locals 1

    .line 153
    .local p0, "this":Landroidx/loader/content/Loader;, "Landroidx/loader/content/Loader<TD;>;"
    iget-object v0, p0, Landroidx/loader/content/Loader;->mContext:Landroid/content/Context;

    return-object v0
.end method

.method public getId()I
    .locals 1

    .line 160
    .local p0, "this":Landroidx/loader/content/Loader;, "Landroidx/loader/content/Loader<TD;>;"
    iget v0, p0, Landroidx/loader/content/Loader;->mId:I

    return v0
.end method

.method public isAbandoned()Z
    .locals 1

    .line 246
    .local p0, "this":Landroidx/loader/content/Loader;, "Landroidx/loader/content/Loader<TD;>;"
    iget-boolean v0, p0, Landroidx/loader/content/Loader;->mAbandoned:Z

    return v0
.end method

.method public isReset()Z
    .locals 1

    .line 255
    .local p0, "this":Landroidx/loader/content/Loader;, "Landroidx/loader/content/Loader<TD;>;"
    iget-boolean v0, p0, Landroidx/loader/content/Loader;->mReset:Z

    return v0
.end method

.method public isStarted()Z
    .locals 1

    .line 237
    .local p0, "this":Landroidx/loader/content/Loader;, "Landroidx/loader/content/Loader<TD;>;"
    iget-boolean v0, p0, Landroidx/loader/content/Loader;->mStarted:Z

    return v0
.end method

.method protected onAbandon()V
    .locals 0

    .line 424
    .local p0, "this":Landroidx/loader/content/Loader;, "Landroidx/loader/content/Loader<TD;>;"
    return-void
.end method

.method protected onCancelLoad()Z
    .locals 1

    .line 333
    .local p0, "this":Landroidx/loader/content/Loader;, "Landroidx/loader/content/Loader<TD;>;"
    const/4 v0, 0x0

    return v0
.end method

.method public onContentChanged()V
    .locals 1

    .line 510
    .local p0, "this":Landroidx/loader/content/Loader;, "Landroidx/loader/content/Loader<TD;>;"
    iget-boolean v0, p0, Landroidx/loader/content/Loader;->mStarted:Z

    if-eqz v0, :cond_0

    .line 511
    invoke-virtual {p0}, Landroidx/loader/content/Loader;->forceLoad()V

    goto :goto_0

    .line 516
    :cond_0
    const/4 v0, 0x1

    iput-boolean v0, p0, Landroidx/loader/content/Loader;->mContentChanged:Z

    .line 518
    :goto_0
    return-void
.end method

.method protected onForceLoad()V
    .locals 0

    .line 355
    .local p0, "this":Landroidx/loader/content/Loader;, "Landroidx/loader/content/Loader<TD;>;"
    return-void
.end method

.method protected onReset()V
    .locals 0

    .line 462
    .local p0, "this":Landroidx/loader/content/Loader;, "Landroidx/loader/content/Loader<TD;>;"
    return-void
.end method

.method protected onStartLoading()V
    .locals 0

    .line 295
    .local p0, "this":Landroidx/loader/content/Loader;, "Landroidx/loader/content/Loader<TD;>;"
    return-void
.end method

.method protected onStopLoading()V
    .locals 0

    .line 391
    .local p0, "this":Landroidx/loader/content/Loader;, "Landroidx/loader/content/Loader<TD;>;"
    return-void
.end method

.method public registerListener(ILandroidx/loader/content/Loader$OnLoadCompleteListener;)V
    .locals 2
    .param p1, "id"    # I
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(I",
            "Landroidx/loader/content/Loader$OnLoadCompleteListener<",
            "TD;>;)V"
        }
    .end annotation

    .line 172
    .local p0, "this":Landroidx/loader/content/Loader;, "Landroidx/loader/content/Loader<TD;>;"
    .local p2, "listener":Landroidx/loader/content/Loader$OnLoadCompleteListener;, "Landroidx/loader/content/Loader$OnLoadCompleteListener<TD;>;"
    iget-object v0, p0, Landroidx/loader/content/Loader;->mListener:Landroidx/loader/content/Loader$OnLoadCompleteListener;

    if-nez v0, :cond_0

    .line 175
    iput-object p2, p0, Landroidx/loader/content/Loader;->mListener:Landroidx/loader/content/Loader$OnLoadCompleteListener;

    .line 176
    iput p1, p0, Landroidx/loader/content/Loader;->mId:I

    .line 177
    return-void

    .line 173
    :cond_0
    new-instance v0, Ljava/lang/IllegalStateException;

    const-string v1, "There is already a listener registered"

    invoke-direct {v0, v1}, Ljava/lang/IllegalStateException;-><init>(Ljava/lang/String;)V

    throw v0
.end method

.method public registerOnLoadCanceledListener(Landroidx/loader/content/Loader$OnLoadCanceledListener;)V
    .locals 2
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Landroidx/loader/content/Loader$OnLoadCanceledListener<",
            "TD;>;)V"
        }
    .end annotation

    .line 206
    .local p0, "this":Landroidx/loader/content/Loader;, "Landroidx/loader/content/Loader<TD;>;"
    .local p1, "listener":Landroidx/loader/content/Loader$OnLoadCanceledListener;, "Landroidx/loader/content/Loader$OnLoadCanceledListener<TD;>;"
    iget-object v0, p0, Landroidx/loader/content/Loader;->mOnLoadCanceledListener:Landroidx/loader/content/Loader$OnLoadCanceledListener;

    if-nez v0, :cond_0

    .line 209
    iput-object p1, p0, Landroidx/loader/content/Loader;->mOnLoadCanceledListener:Landroidx/loader/content/Loader$OnLoadCanceledListener;

    .line 210
    return-void

    .line 207
    :cond_0
    new-instance v0, Ljava/lang/IllegalStateException;

    const-string v1, "There is already a listener registered"

    invoke-direct {v0, v1}, Ljava/lang/IllegalStateException;-><init>(Ljava/lang/String;)V

    throw v0
.end method

.method public reset()V
    .locals 1

    .line 446
    .local p0, "this":Landroidx/loader/content/Loader;, "Landroidx/loader/content/Loader<TD;>;"
    invoke-virtual {p0}, Landroidx/loader/content/Loader;->onReset()V

    .line 447
    const/4 v0, 0x1

    iput-boolean v0, p0, Landroidx/loader/content/Loader;->mReset:Z

    .line 448
    const/4 v0, 0x0

    iput-boolean v0, p0, Landroidx/loader/content/Loader;->mStarted:Z

    .line 449
    iput-boolean v0, p0, Landroidx/loader/content/Loader;->mAbandoned:Z

    .line 450
    iput-boolean v0, p0, Landroidx/loader/content/Loader;->mContentChanged:Z

    .line 451
    iput-boolean v0, p0, Landroidx/loader/content/Loader;->mProcessingChange:Z

    .line 452
    return-void
.end method

.method public rollbackContentChanged()V
    .locals 1

    .line 495
    .local p0, "this":Landroidx/loader/content/Loader;, "Landroidx/loader/content/Loader<TD;>;"
    iget-boolean v0, p0, Landroidx/loader/content/Loader;->mProcessingChange:Z

    if-eqz v0, :cond_0

    .line 496
    invoke-virtual {p0}, Landroidx/loader/content/Loader;->onContentChanged()V

    .line 498
    :cond_0
    return-void
.end method

.method public final startLoading()V
    .locals 1

    .line 281
    .local p0, "this":Landroidx/loader/content/Loader;, "Landroidx/loader/content/Loader<TD;>;"
    const/4 v0, 0x1

    iput-boolean v0, p0, Landroidx/loader/content/Loader;->mStarted:Z

    .line 282
    const/4 v0, 0x0

    iput-boolean v0, p0, Landroidx/loader/content/Loader;->mReset:Z

    .line 283
    iput-boolean v0, p0, Landroidx/loader/content/Loader;->mAbandoned:Z

    .line 284
    invoke-virtual {p0}, Landroidx/loader/content/Loader;->onStartLoading()V

    .line 285
    return-void
.end method

.method public stopLoading()V
    .locals 1

    .line 379
    .local p0, "this":Landroidx/loader/content/Loader;, "Landroidx/loader/content/Loader<TD;>;"
    const/4 v0, 0x0

    iput-boolean v0, p0, Landroidx/loader/content/Loader;->mStarted:Z

    .line 380
    invoke-virtual {p0}, Landroidx/loader/content/Loader;->onStopLoading()V

    .line 381
    return-void
.end method

.method public takeContentChanged()Z
    .locals 2

    .line 470
    .local p0, "this":Landroidx/loader/content/Loader;, "Landroidx/loader/content/Loader<TD;>;"
    iget-boolean v0, p0, Landroidx/loader/content/Loader;->mContentChanged:Z

    .line 471
    .local v0, "res":Z
    const/4 v1, 0x0

    iput-boolean v1, p0, Landroidx/loader/content/Loader;->mContentChanged:Z

    .line 472
    iget-boolean v1, p0, Landroidx/loader/content/Loader;->mProcessingChange:Z

    or-int/2addr v1, v0

    iput-boolean v1, p0, Landroidx/loader/content/Loader;->mProcessingChange:Z

    .line 473
    return v0
.end method

.method public toString()Ljava/lang/String;
    .locals 3

    .line 542
    .local p0, "this":Landroidx/loader/content/Loader;, "Landroidx/loader/content/Loader<TD;>;"
    new-instance v0, Ljava/lang/StringBuilder;

    const/16 v1, 0x40

    invoke-direct {v0, v1}, Ljava/lang/StringBuilder;-><init>(I)V

    .line 543
    .local v0, "sb":Ljava/lang/StringBuilder;
    invoke-virtual {p0}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    move-result-object v1

    .line 544
    .local v1, "cls":Ljava/lang/Class;
    invoke-virtual {v1}, Ljava/lang/Class;->getSimpleName()Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v0, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    .line 545
    const-string v2, "{"

    invoke-virtual {v0, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    .line 546
    invoke-static {v1}, Ljava/lang/System;->identityHashCode(Ljava/lang/Object;)I

    move-result v2

    invoke-static {v2}, Ljava/lang/Integer;->toHexString(I)Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v0, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    .line 547
    const-string v2, " id="

    invoke-virtual {v0, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    .line 548
    iget v2, p0, Landroidx/loader/content/Loader;->mId:I

    invoke-virtual {v0, v2}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    .line 549
    const-string v2, "}"

    invoke-virtual {v0, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    .line 550
    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v2

    return-object v2
.end method

.method public unregisterListener(Landroidx/loader/content/Loader$OnLoadCompleteListener;)V
    .locals 2
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Landroidx/loader/content/Loader$OnLoadCompleteListener<",
            "TD;>;)V"
        }
    .end annotation

    .line 186
    .local p0, "this":Landroidx/loader/content/Loader;, "Landroidx/loader/content/Loader<TD;>;"
    .local p1, "listener":Landroidx/loader/content/Loader$OnLoadCompleteListener;, "Landroidx/loader/content/Loader$OnLoadCompleteListener<TD;>;"
    iget-object v0, p0, Landroidx/loader/content/Loader;->mListener:Landroidx/loader/content/Loader$OnLoadCompleteListener;

    if-eqz v0, :cond_1

    .line 189
    if-ne v0, p1, :cond_0

    .line 192
    const/4 v0, 0x0

    iput-object v0, p0, Landroidx/loader/content/Loader;->mListener:Landroidx/loader/content/Loader$OnLoadCompleteListener;

    .line 193
    return-void

    .line 190
    :cond_0
    new-instance v0, Ljava/lang/IllegalArgumentException;

    const-string v1, "Attempting to unregister the wrong listener"

    invoke-direct {v0, v1}, Ljava/lang/IllegalArgumentException;-><init>(Ljava/lang/String;)V

    throw v0

    .line 187
    :cond_1
    new-instance v0, Ljava/lang/IllegalStateException;

    const-string v1, "No listener register"

    invoke-direct {v0, v1}, Ljava/lang/IllegalStateException;-><init>(Ljava/lang/String;)V

    throw v0
.end method

.method public unregisterOnLoadCanceledListener(Landroidx/loader/content/Loader$OnLoadCanceledListener;)V
    .locals 2
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Landroidx/loader/content/Loader$OnLoadCanceledListener<",
            "TD;>;)V"
        }
    .end annotation

    .line 222
    .local p0, "this":Landroidx/loader/content/Loader;, "Landroidx/loader/content/Loader<TD;>;"
    .local p1, "listener":Landroidx/loader/content/Loader$OnLoadCanceledListener;, "Landroidx/loader/content/Loader$OnLoadCanceledListener<TD;>;"
    iget-object v0, p0, Landroidx/loader/content/Loader;->mOnLoadCanceledListener:Landroidx/loader/content/Loader$OnLoadCanceledListener;

    if-eqz v0, :cond_1

    .line 225
    if-ne v0, p1, :cond_0

    .line 228
    const/4 v0, 0x0

    iput-object v0, p0, Landroidx/loader/content/Loader;->mOnLoadCanceledListener:Landroidx/loader/content/Loader$OnLoadCanceledListener;

    .line 229
    return-void

    .line 226
    :cond_0
    new-instance v0, Ljava/lang/IllegalArgumentException;

    const-string v1, "Attempting to unregister the wrong listener"

    invoke-direct {v0, v1}, Ljava/lang/IllegalArgumentException;-><init>(Ljava/lang/String;)V

    throw v0

    .line 223
    :cond_1
    new-instance v0, Ljava/lang/IllegalStateException;

    const-string v1, "No listener register"

    invoke-direct {v0, v1}, Ljava/lang/IllegalStateException;-><init>(Ljava/lang/String;)V

    throw v0
.end method
