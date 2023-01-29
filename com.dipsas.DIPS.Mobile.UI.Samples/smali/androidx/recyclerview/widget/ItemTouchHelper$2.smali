.class Landroidx/recyclerview/widget/ItemTouchHelper$2;
.super Ljava/lang/Object;
.source "ItemTouchHelper.java"

# interfaces
.implements Landroidx/recyclerview/widget/RecyclerView$OnItemTouchListener;


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Landroidx/recyclerview/widget/ItemTouchHelper;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Landroidx/recyclerview/widget/ItemTouchHelper;


# direct methods
.method constructor <init>(Landroidx/recyclerview/widget/ItemTouchHelper;)V
    .locals 0
    .param p1, "this$0"    # Landroidx/recyclerview/widget/ItemTouchHelper;

    .line 315
    iput-object p1, p0, Landroidx/recyclerview/widget/ItemTouchHelper$2;->this$0:Landroidx/recyclerview/widget/ItemTouchHelper;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public onInterceptTouchEvent(Landroidx/recyclerview/widget/RecyclerView;Landroid/view/MotionEvent;)Z
    .locals 7
    .param p1, "recyclerView"    # Landroidx/recyclerview/widget/RecyclerView;
    .param p2, "event"    # Landroid/view/MotionEvent;

    .line 319
    iget-object v0, p0, Landroidx/recyclerview/widget/ItemTouchHelper$2;->this$0:Landroidx/recyclerview/widget/ItemTouchHelper;

    iget-object v0, v0, Landroidx/recyclerview/widget/ItemTouchHelper;->mGestureDetector:Landroidx/core/view/GestureDetectorCompat;

    invoke-virtual {v0, p2}, Landroidx/core/view/GestureDetectorCompat;->onTouchEvent(Landroid/view/MotionEvent;)Z

    .line 323
    invoke-virtual {p2}, Landroid/view/MotionEvent;->getActionMasked()I

    move-result v0

    .line 324
    .local v0, "action":I
    const/4 v1, 0x1

    const/4 v2, 0x0

    if-nez v0, :cond_2

    .line 325
    iget-object v3, p0, Landroidx/recyclerview/widget/ItemTouchHelper$2;->this$0:Landroidx/recyclerview/widget/ItemTouchHelper;

    invoke-virtual {p2, v2}, Landroid/view/MotionEvent;->getPointerId(I)I

    move-result v4

    iput v4, v3, Landroidx/recyclerview/widget/ItemTouchHelper;->mActivePointerId:I

    .line 326
    iget-object v3, p0, Landroidx/recyclerview/widget/ItemTouchHelper$2;->this$0:Landroidx/recyclerview/widget/ItemTouchHelper;

    invoke-virtual {p2}, Landroid/view/MotionEvent;->getX()F

    move-result v4

    iput v4, v3, Landroidx/recyclerview/widget/ItemTouchHelper;->mInitialTouchX:F

    .line 327
    iget-object v3, p0, Landroidx/recyclerview/widget/ItemTouchHelper$2;->this$0:Landroidx/recyclerview/widget/ItemTouchHelper;

    invoke-virtual {p2}, Landroid/view/MotionEvent;->getY()F

    move-result v4

    iput v4, v3, Landroidx/recyclerview/widget/ItemTouchHelper;->mInitialTouchY:F

    .line 328
    iget-object v3, p0, Landroidx/recyclerview/widget/ItemTouchHelper$2;->this$0:Landroidx/recyclerview/widget/ItemTouchHelper;

    invoke-virtual {v3}, Landroidx/recyclerview/widget/ItemTouchHelper;->obtainVelocityTracker()V

    .line 329
    iget-object v3, p0, Landroidx/recyclerview/widget/ItemTouchHelper$2;->this$0:Landroidx/recyclerview/widget/ItemTouchHelper;

    iget-object v3, v3, Landroidx/recyclerview/widget/ItemTouchHelper;->mSelected:Landroidx/recyclerview/widget/RecyclerView$ViewHolder;

    if-nez v3, :cond_5

    .line 330
    iget-object v3, p0, Landroidx/recyclerview/widget/ItemTouchHelper$2;->this$0:Landroidx/recyclerview/widget/ItemTouchHelper;

    invoke-virtual {v3, p2}, Landroidx/recyclerview/widget/ItemTouchHelper;->findAnimation(Landroid/view/MotionEvent;)Landroidx/recyclerview/widget/ItemTouchHelper$RecoverAnimation;

    move-result-object v3

    .line 331
    .local v3, "animation":Landroidx/recyclerview/widget/ItemTouchHelper$RecoverAnimation;
    if-eqz v3, :cond_1

    .line 332
    iget-object v4, p0, Landroidx/recyclerview/widget/ItemTouchHelper$2;->this$0:Landroidx/recyclerview/widget/ItemTouchHelper;

    iget v5, v4, Landroidx/recyclerview/widget/ItemTouchHelper;->mInitialTouchX:F

    iget v6, v3, Landroidx/recyclerview/widget/ItemTouchHelper$RecoverAnimation;->mX:F

    sub-float/2addr v5, v6

    iput v5, v4, Landroidx/recyclerview/widget/ItemTouchHelper;->mInitialTouchX:F

    .line 333
    iget-object v4, p0, Landroidx/recyclerview/widget/ItemTouchHelper$2;->this$0:Landroidx/recyclerview/widget/ItemTouchHelper;

    iget v5, v4, Landroidx/recyclerview/widget/ItemTouchHelper;->mInitialTouchY:F

    iget v6, v3, Landroidx/recyclerview/widget/ItemTouchHelper$RecoverAnimation;->mY:F

    sub-float/2addr v5, v6

    iput v5, v4, Landroidx/recyclerview/widget/ItemTouchHelper;->mInitialTouchY:F

    .line 334
    iget-object v4, p0, Landroidx/recyclerview/widget/ItemTouchHelper$2;->this$0:Landroidx/recyclerview/widget/ItemTouchHelper;

    iget-object v5, v3, Landroidx/recyclerview/widget/ItemTouchHelper$RecoverAnimation;->mViewHolder:Landroidx/recyclerview/widget/RecyclerView$ViewHolder;

    invoke-virtual {v4, v5, v1}, Landroidx/recyclerview/widget/ItemTouchHelper;->endRecoverAnimation(Landroidx/recyclerview/widget/RecyclerView$ViewHolder;Z)V

    .line 335
    iget-object v4, p0, Landroidx/recyclerview/widget/ItemTouchHelper$2;->this$0:Landroidx/recyclerview/widget/ItemTouchHelper;

    iget-object v4, v4, Landroidx/recyclerview/widget/ItemTouchHelper;->mPendingCleanup:Ljava/util/List;

    iget-object v5, v3, Landroidx/recyclerview/widget/ItemTouchHelper$RecoverAnimation;->mViewHolder:Landroidx/recyclerview/widget/RecyclerView$ViewHolder;

    iget-object v5, v5, Landroidx/recyclerview/widget/RecyclerView$ViewHolder;->itemView:Landroid/view/View;

    invoke-interface {v4, v5}, Ljava/util/List;->remove(Ljava/lang/Object;)Z

    move-result v4

    if-eqz v4, :cond_0

    .line 336
    iget-object v4, p0, Landroidx/recyclerview/widget/ItemTouchHelper$2;->this$0:Landroidx/recyclerview/widget/ItemTouchHelper;

    iget-object v4, v4, Landroidx/recyclerview/widget/ItemTouchHelper;->mCallback:Landroidx/recyclerview/widget/ItemTouchHelper$Callback;

    iget-object v5, p0, Landroidx/recyclerview/widget/ItemTouchHelper$2;->this$0:Landroidx/recyclerview/widget/ItemTouchHelper;

    iget-object v5, v5, Landroidx/recyclerview/widget/ItemTouchHelper;->mRecyclerView:Landroidx/recyclerview/widget/RecyclerView;

    iget-object v6, v3, Landroidx/recyclerview/widget/ItemTouchHelper$RecoverAnimation;->mViewHolder:Landroidx/recyclerview/widget/RecyclerView$ViewHolder;

    invoke-virtual {v4, v5, v6}, Landroidx/recyclerview/widget/ItemTouchHelper$Callback;->clearView(Landroidx/recyclerview/widget/RecyclerView;Landroidx/recyclerview/widget/RecyclerView$ViewHolder;)V

    .line 338
    :cond_0
    iget-object v4, p0, Landroidx/recyclerview/widget/ItemTouchHelper$2;->this$0:Landroidx/recyclerview/widget/ItemTouchHelper;

    iget-object v5, v3, Landroidx/recyclerview/widget/ItemTouchHelper$RecoverAnimation;->mViewHolder:Landroidx/recyclerview/widget/RecyclerView$ViewHolder;

    iget v6, v3, Landroidx/recyclerview/widget/ItemTouchHelper$RecoverAnimation;->mActionState:I

    invoke-virtual {v4, v5, v6}, Landroidx/recyclerview/widget/ItemTouchHelper;->select(Landroidx/recyclerview/widget/RecyclerView$ViewHolder;I)V

    .line 339
    iget-object v4, p0, Landroidx/recyclerview/widget/ItemTouchHelper$2;->this$0:Landroidx/recyclerview/widget/ItemTouchHelper;

    iget v5, v4, Landroidx/recyclerview/widget/ItemTouchHelper;->mSelectedFlags:I

    invoke-virtual {v4, p2, v5, v2}, Landroidx/recyclerview/widget/ItemTouchHelper;->updateDxDy(Landroid/view/MotionEvent;II)V

    .line 341
    .end local v3    # "animation":Landroidx/recyclerview/widget/ItemTouchHelper$RecoverAnimation;
    :cond_1
    goto :goto_1

    .line 342
    :cond_2
    const/4 v3, 0x3

    const/4 v4, -0x1

    if-eq v0, v3, :cond_4

    if-ne v0, v1, :cond_3

    goto :goto_0

    .line 345
    :cond_3
    iget-object v3, p0, Landroidx/recyclerview/widget/ItemTouchHelper$2;->this$0:Landroidx/recyclerview/widget/ItemTouchHelper;

    iget v3, v3, Landroidx/recyclerview/widget/ItemTouchHelper;->mActivePointerId:I

    if-eq v3, v4, :cond_5

    .line 348
    iget-object v3, p0, Landroidx/recyclerview/widget/ItemTouchHelper$2;->this$0:Landroidx/recyclerview/widget/ItemTouchHelper;

    iget v3, v3, Landroidx/recyclerview/widget/ItemTouchHelper;->mActivePointerId:I

    invoke-virtual {p2, v3}, Landroid/view/MotionEvent;->findPointerIndex(I)I

    move-result v3

    .line 352
    .local v3, "index":I
    if-ltz v3, :cond_5

    .line 353
    iget-object v4, p0, Landroidx/recyclerview/widget/ItemTouchHelper$2;->this$0:Landroidx/recyclerview/widget/ItemTouchHelper;

    invoke-virtual {v4, v0, p2, v3}, Landroidx/recyclerview/widget/ItemTouchHelper;->checkSelectForSwipe(ILandroid/view/MotionEvent;I)V

    goto :goto_1

    .line 343
    .end local v3    # "index":I
    :cond_4
    :goto_0
    iget-object v3, p0, Landroidx/recyclerview/widget/ItemTouchHelper$2;->this$0:Landroidx/recyclerview/widget/ItemTouchHelper;

    iput v4, v3, Landroidx/recyclerview/widget/ItemTouchHelper;->mActivePointerId:I

    .line 344
    iget-object v3, p0, Landroidx/recyclerview/widget/ItemTouchHelper$2;->this$0:Landroidx/recyclerview/widget/ItemTouchHelper;

    const/4 v4, 0x0

    invoke-virtual {v3, v4, v2}, Landroidx/recyclerview/widget/ItemTouchHelper;->select(Landroidx/recyclerview/widget/RecyclerView$ViewHolder;I)V

    .line 356
    :cond_5
    :goto_1
    iget-object v3, p0, Landroidx/recyclerview/widget/ItemTouchHelper$2;->this$0:Landroidx/recyclerview/widget/ItemTouchHelper;

    iget-object v3, v3, Landroidx/recyclerview/widget/ItemTouchHelper;->mVelocityTracker:Landroid/view/VelocityTracker;

    if-eqz v3, :cond_6

    .line 357
    iget-object v3, p0, Landroidx/recyclerview/widget/ItemTouchHelper$2;->this$0:Landroidx/recyclerview/widget/ItemTouchHelper;

    iget-object v3, v3, Landroidx/recyclerview/widget/ItemTouchHelper;->mVelocityTracker:Landroid/view/VelocityTracker;

    invoke-virtual {v3, p2}, Landroid/view/VelocityTracker;->addMovement(Landroid/view/MotionEvent;)V

    .line 359
    :cond_6
    iget-object v3, p0, Landroidx/recyclerview/widget/ItemTouchHelper$2;->this$0:Landroidx/recyclerview/widget/ItemTouchHelper;

    iget-object v3, v3, Landroidx/recyclerview/widget/ItemTouchHelper;->mSelected:Landroidx/recyclerview/widget/RecyclerView$ViewHolder;

    if-eqz v3, :cond_7

    goto :goto_2

    :cond_7
    const/4 v1, 0x0

    :goto_2
    return v1
.end method

.method public onRequestDisallowInterceptTouchEvent(Z)V
    .locals 3
    .param p1, "disallowIntercept"    # Z

    .line 422
    if-nez p1, :cond_0

    .line 423
    return-void

    .line 425
    :cond_0
    iget-object v0, p0, Landroidx/recyclerview/widget/ItemTouchHelper$2;->this$0:Landroidx/recyclerview/widget/ItemTouchHelper;

    const/4 v1, 0x0

    const/4 v2, 0x0

    invoke-virtual {v0, v1, v2}, Landroidx/recyclerview/widget/ItemTouchHelper;->select(Landroidx/recyclerview/widget/RecyclerView$ViewHolder;I)V

    .line 426
    return-void
.end method

.method public onTouchEvent(Landroidx/recyclerview/widget/RecyclerView;Landroid/view/MotionEvent;)V
    .locals 8
    .param p1, "recyclerView"    # Landroidx/recyclerview/widget/RecyclerView;
    .param p2, "event"    # Landroid/view/MotionEvent;

    .line 364
    iget-object v0, p0, Landroidx/recyclerview/widget/ItemTouchHelper$2;->this$0:Landroidx/recyclerview/widget/ItemTouchHelper;

    iget-object v0, v0, Landroidx/recyclerview/widget/ItemTouchHelper;->mGestureDetector:Landroidx/core/view/GestureDetectorCompat;

    invoke-virtual {v0, p2}, Landroidx/core/view/GestureDetectorCompat;->onTouchEvent(Landroid/view/MotionEvent;)Z

    .line 369
    iget-object v0, p0, Landroidx/recyclerview/widget/ItemTouchHelper$2;->this$0:Landroidx/recyclerview/widget/ItemTouchHelper;

    iget-object v0, v0, Landroidx/recyclerview/widget/ItemTouchHelper;->mVelocityTracker:Landroid/view/VelocityTracker;

    if-eqz v0, :cond_0

    .line 370
    iget-object v0, p0, Landroidx/recyclerview/widget/ItemTouchHelper$2;->this$0:Landroidx/recyclerview/widget/ItemTouchHelper;

    iget-object v0, v0, Landroidx/recyclerview/widget/ItemTouchHelper;->mVelocityTracker:Landroid/view/VelocityTracker;

    invoke-virtual {v0, p2}, Landroid/view/VelocityTracker;->addMovement(Landroid/view/MotionEvent;)V

    .line 372
    :cond_0
    iget-object v0, p0, Landroidx/recyclerview/widget/ItemTouchHelper$2;->this$0:Landroidx/recyclerview/widget/ItemTouchHelper;

    iget v0, v0, Landroidx/recyclerview/widget/ItemTouchHelper;->mActivePointerId:I

    const/4 v1, -0x1

    if-ne v0, v1, :cond_1

    .line 373
    return-void

    .line 375
    :cond_1
    invoke-virtual {p2}, Landroid/view/MotionEvent;->getActionMasked()I

    move-result v0

    .line 376
    .local v0, "action":I
    iget-object v2, p0, Landroidx/recyclerview/widget/ItemTouchHelper$2;->this$0:Landroidx/recyclerview/widget/ItemTouchHelper;

    iget v2, v2, Landroidx/recyclerview/widget/ItemTouchHelper;->mActivePointerId:I

    invoke-virtual {p2, v2}, Landroid/view/MotionEvent;->findPointerIndex(I)I

    move-result v2

    .line 377
    .local v2, "activePointerIndex":I
    if-ltz v2, :cond_2

    .line 378
    iget-object v3, p0, Landroidx/recyclerview/widget/ItemTouchHelper$2;->this$0:Landroidx/recyclerview/widget/ItemTouchHelper;

    invoke-virtual {v3, v0, p2, v2}, Landroidx/recyclerview/widget/ItemTouchHelper;->checkSelectForSwipe(ILandroid/view/MotionEvent;I)V

    .line 380
    :cond_2
    iget-object v3, p0, Landroidx/recyclerview/widget/ItemTouchHelper$2;->this$0:Landroidx/recyclerview/widget/ItemTouchHelper;

    iget-object v3, v3, Landroidx/recyclerview/widget/ItemTouchHelper;->mSelected:Landroidx/recyclerview/widget/RecyclerView$ViewHolder;

    .line 381
    .local v3, "viewHolder":Landroidx/recyclerview/widget/RecyclerView$ViewHolder;
    if-nez v3, :cond_3

    .line 382
    return-void

    .line 384
    :cond_3
    const/4 v4, 0x0

    packed-switch v0, :pswitch_data_0

    :pswitch_0
    goto :goto_1

    .line 406
    :pswitch_1
    invoke-virtual {p2}, Landroid/view/MotionEvent;->getActionIndex()I

    move-result v1

    .line 407
    .local v1, "pointerIndex":I
    invoke-virtual {p2, v1}, Landroid/view/MotionEvent;->getPointerId(I)I

    move-result v5

    .line 408
    .local v5, "pointerId":I
    iget-object v6, p0, Landroidx/recyclerview/widget/ItemTouchHelper$2;->this$0:Landroidx/recyclerview/widget/ItemTouchHelper;

    iget v6, v6, Landroidx/recyclerview/widget/ItemTouchHelper;->mActivePointerId:I

    if-ne v5, v6, :cond_6

    .line 411
    if-nez v1, :cond_4

    const/4 v4, 0x1

    .line 412
    .local v4, "newPointerIndex":I
    :cond_4
    iget-object v6, p0, Landroidx/recyclerview/widget/ItemTouchHelper$2;->this$0:Landroidx/recyclerview/widget/ItemTouchHelper;

    invoke-virtual {p2, v4}, Landroid/view/MotionEvent;->getPointerId(I)I

    move-result v7

    iput v7, v6, Landroidx/recyclerview/widget/ItemTouchHelper;->mActivePointerId:I

    .line 413
    iget-object v6, p0, Landroidx/recyclerview/widget/ItemTouchHelper$2;->this$0:Landroidx/recyclerview/widget/ItemTouchHelper;

    iget v7, v6, Landroidx/recyclerview/widget/ItemTouchHelper;->mSelectedFlags:I

    invoke-virtual {v6, p2, v7, v1}, Landroidx/recyclerview/widget/ItemTouchHelper;->updateDxDy(Landroid/view/MotionEvent;II)V

    .line 414
    .end local v4    # "newPointerIndex":I
    goto :goto_1

    .line 397
    .end local v1    # "pointerIndex":I
    .end local v5    # "pointerId":I
    :pswitch_2
    iget-object v5, p0, Landroidx/recyclerview/widget/ItemTouchHelper$2;->this$0:Landroidx/recyclerview/widget/ItemTouchHelper;

    iget-object v5, v5, Landroidx/recyclerview/widget/ItemTouchHelper;->mVelocityTracker:Landroid/view/VelocityTracker;

    if-eqz v5, :cond_5

    .line 398
    iget-object v5, p0, Landroidx/recyclerview/widget/ItemTouchHelper$2;->this$0:Landroidx/recyclerview/widget/ItemTouchHelper;

    iget-object v5, v5, Landroidx/recyclerview/widget/ItemTouchHelper;->mVelocityTracker:Landroid/view/VelocityTracker;

    invoke-virtual {v5}, Landroid/view/VelocityTracker;->clear()V

    goto :goto_0

    .line 387
    :pswitch_3
    if-ltz v2, :cond_6

    .line 388
    iget-object v1, p0, Landroidx/recyclerview/widget/ItemTouchHelper$2;->this$0:Landroidx/recyclerview/widget/ItemTouchHelper;

    iget v4, v1, Landroidx/recyclerview/widget/ItemTouchHelper;->mSelectedFlags:I

    invoke-virtual {v1, p2, v4, v2}, Landroidx/recyclerview/widget/ItemTouchHelper;->updateDxDy(Landroid/view/MotionEvent;II)V

    .line 389
    iget-object v1, p0, Landroidx/recyclerview/widget/ItemTouchHelper$2;->this$0:Landroidx/recyclerview/widget/ItemTouchHelper;

    invoke-virtual {v1, v3}, Landroidx/recyclerview/widget/ItemTouchHelper;->moveIfNecessary(Landroidx/recyclerview/widget/RecyclerView$ViewHolder;)V

    .line 390
    iget-object v1, p0, Landroidx/recyclerview/widget/ItemTouchHelper$2;->this$0:Landroidx/recyclerview/widget/ItemTouchHelper;

    iget-object v1, v1, Landroidx/recyclerview/widget/ItemTouchHelper;->mRecyclerView:Landroidx/recyclerview/widget/RecyclerView;

    iget-object v4, p0, Landroidx/recyclerview/widget/ItemTouchHelper$2;->this$0:Landroidx/recyclerview/widget/ItemTouchHelper;

    iget-object v4, v4, Landroidx/recyclerview/widget/ItemTouchHelper;->mScrollRunnable:Ljava/lang/Runnable;

    invoke-virtual {v1, v4}, Landroidx/recyclerview/widget/RecyclerView;->removeCallbacks(Ljava/lang/Runnable;)Z

    .line 391
    iget-object v1, p0, Landroidx/recyclerview/widget/ItemTouchHelper$2;->this$0:Landroidx/recyclerview/widget/ItemTouchHelper;

    iget-object v1, v1, Landroidx/recyclerview/widget/ItemTouchHelper;->mScrollRunnable:Ljava/lang/Runnable;

    invoke-interface {v1}, Ljava/lang/Runnable;->run()V

    .line 392
    iget-object v1, p0, Landroidx/recyclerview/widget/ItemTouchHelper$2;->this$0:Landroidx/recyclerview/widget/ItemTouchHelper;

    iget-object v1, v1, Landroidx/recyclerview/widget/ItemTouchHelper;->mRecyclerView:Landroidx/recyclerview/widget/RecyclerView;

    invoke-virtual {v1}, Landroidx/recyclerview/widget/RecyclerView;->invalidate()V

    goto :goto_1

    .line 402
    :cond_5
    :goto_0
    :pswitch_4
    iget-object v5, p0, Landroidx/recyclerview/widget/ItemTouchHelper$2;->this$0:Landroidx/recyclerview/widget/ItemTouchHelper;

    const/4 v6, 0x0

    invoke-virtual {v5, v6, v4}, Landroidx/recyclerview/widget/ItemTouchHelper;->select(Landroidx/recyclerview/widget/RecyclerView$ViewHolder;I)V

    .line 403
    iget-object v4, p0, Landroidx/recyclerview/widget/ItemTouchHelper$2;->this$0:Landroidx/recyclerview/widget/ItemTouchHelper;

    iput v1, v4, Landroidx/recyclerview/widget/ItemTouchHelper;->mActivePointerId:I

    .line 404
    nop

    .line 418
    :cond_6
    :goto_1
    return-void

    :pswitch_data_0
    .packed-switch 0x1
        :pswitch_4
        :pswitch_3
        :pswitch_2
        :pswitch_0
        :pswitch_0
        :pswitch_1
    .end packed-switch
.end method
