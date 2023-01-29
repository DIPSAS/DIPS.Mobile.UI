.class Landroid/support/v4/media/session/MediaSessionCompat$Callback$CallbackHandler;
.super Landroid/os/Handler;
.source "MediaSessionCompat.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Landroid/support/v4/media/session/MediaSessionCompat$Callback;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x2
    name = "CallbackHandler"
.end annotation


# static fields
.field private static final MSG_MEDIA_PLAY_PAUSE_KEY_DOUBLE_TAP_TIMEOUT:I = 0x1


# instance fields
.field final synthetic this$0:Landroid/support/v4/media/session/MediaSessionCompat$Callback;


# direct methods
.method constructor <init>(Landroid/support/v4/media/session/MediaSessionCompat$Callback;Landroid/os/Looper;)V
    .locals 0
    .param p2, "looper"    # Landroid/os/Looper;
    .annotation system Ldalvik/annotation/MethodParameters;
        accessFlags = {
            0x1010,
            0x0
        }
        names = {
            "this$0",
            "looper"
        }
    .end annotation

    .line 1512
    iput-object p1, p0, Landroid/support/v4/media/session/MediaSessionCompat$Callback$CallbackHandler;->this$0:Landroid/support/v4/media/session/MediaSessionCompat$Callback;

    .line 1513
    invoke-direct {p0, p2}, Landroid/os/Handler;-><init>(Landroid/os/Looper;)V

    .line 1514
    return-void
.end method


# virtual methods
.method public handleMessage(Landroid/os/Message;)V
    .locals 4
    .param p1, "msg"    # Landroid/os/Message;
    .annotation system Ldalvik/annotation/MethodParameters;
        accessFlags = {
            0x0
        }
        names = {
            "msg"
        }
    .end annotation

    .line 1518
    iget v0, p1, Landroid/os/Message;->what:I

    const/4 v1, 0x1

    if-ne v0, v1, :cond_2

    .line 1523
    iget-object v0, p0, Landroid/support/v4/media/session/MediaSessionCompat$Callback$CallbackHandler;->this$0:Landroid/support/v4/media/session/MediaSessionCompat$Callback;

    iget-object v0, v0, Landroid/support/v4/media/session/MediaSessionCompat$Callback;->mLock:Ljava/lang/Object;

    monitor-enter v0

    .line 1524
    :try_start_0
    iget-object v1, p0, Landroid/support/v4/media/session/MediaSessionCompat$Callback$CallbackHandler;->this$0:Landroid/support/v4/media/session/MediaSessionCompat$Callback;

    iget-object v1, v1, Landroid/support/v4/media/session/MediaSessionCompat$Callback;->mSessionImpl:Ljava/lang/ref/WeakReference;

    invoke-virtual {v1}, Ljava/lang/ref/WeakReference;->get()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Landroid/support/v4/media/session/MediaSessionCompat$MediaSessionImpl;

    .line 1525
    .local v1, "impl":Landroid/support/v4/media/session/MediaSessionCompat$MediaSessionImpl;
    iget-object v2, p0, Landroid/support/v4/media/session/MediaSessionCompat$Callback$CallbackHandler;->this$0:Landroid/support/v4/media/session/MediaSessionCompat$Callback;

    iget-object v2, v2, Landroid/support/v4/media/session/MediaSessionCompat$Callback;->mCallbackHandler:Landroid/support/v4/media/session/MediaSessionCompat$Callback$CallbackHandler;

    .line 1526
    .local v2, "callbackHandler":Landroid/os/Handler;
    monitor-exit v0
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    .line 1527
    if-eqz v1, :cond_1

    iget-object v0, p0, Landroid/support/v4/media/session/MediaSessionCompat$Callback$CallbackHandler;->this$0:Landroid/support/v4/media/session/MediaSessionCompat$Callback;

    .line 1528
    invoke-interface {v1}, Landroid/support/v4/media/session/MediaSessionCompat$MediaSessionImpl;->getCallback()Landroid/support/v4/media/session/MediaSessionCompat$Callback;

    move-result-object v3

    if-ne v0, v3, :cond_1

    if-nez v2, :cond_0

    goto :goto_0

    .line 1532
    :cond_0
    iget-object v0, p1, Landroid/os/Message;->obj:Ljava/lang/Object;

    check-cast v0, Landroidx/media/MediaSessionManager$RemoteUserInfo;

    .line 1533
    .local v0, "info":Landroidx/media/MediaSessionManager$RemoteUserInfo;
    invoke-interface {v1, v0}, Landroid/support/v4/media/session/MediaSessionCompat$MediaSessionImpl;->setCurrentControllerInfo(Landroidx/media/MediaSessionManager$RemoteUserInfo;)V

    .line 1534
    iget-object v3, p0, Landroid/support/v4/media/session/MediaSessionCompat$Callback$CallbackHandler;->this$0:Landroid/support/v4/media/session/MediaSessionCompat$Callback;

    invoke-virtual {v3, v1, v2}, Landroid/support/v4/media/session/MediaSessionCompat$Callback;->handleMediaPlayPauseIfPendingOnHandler(Landroid/support/v4/media/session/MediaSessionCompat$MediaSessionImpl;Landroid/os/Handler;)V

    .line 1535
    const/4 v3, 0x0

    invoke-interface {v1, v3}, Landroid/support/v4/media/session/MediaSessionCompat$MediaSessionImpl;->setCurrentControllerInfo(Landroidx/media/MediaSessionManager$RemoteUserInfo;)V

    goto :goto_1

    .line 1530
    .end local v0    # "info":Landroidx/media/MediaSessionManager$RemoteUserInfo;
    :cond_1
    :goto_0
    return-void

    .line 1526
    .end local v1    # "impl":Landroid/support/v4/media/session/MediaSessionCompat$MediaSessionImpl;
    .end local v2    # "callbackHandler":Landroid/os/Handler;
    :catchall_0
    move-exception v1

    :try_start_1
    monitor-exit v0
    :try_end_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    throw v1

    .line 1537
    :cond_2
    :goto_1
    return-void
.end method
