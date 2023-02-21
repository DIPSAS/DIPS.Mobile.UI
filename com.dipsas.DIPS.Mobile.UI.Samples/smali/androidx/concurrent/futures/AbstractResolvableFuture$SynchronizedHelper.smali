.class final Landroidx/concurrent/futures/AbstractResolvableFuture$SynchronizedHelper;
.super Landroidx/concurrent/futures/AbstractResolvableFuture$AtomicHelper;
.source "AbstractResolvableFuture.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Landroidx/concurrent/futures/AbstractResolvableFuture;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x1a
    name = "SynchronizedHelper"
.end annotation


# direct methods
.method constructor <init>()V
    .locals 1

    .line 1146
    const/4 v0, 0x0

    invoke-direct {p0, v0}, Landroidx/concurrent/futures/AbstractResolvableFuture$AtomicHelper;-><init>(Landroidx/concurrent/futures/AbstractResolvableFuture$1;)V

    .line 1147
    return-void
.end method


# virtual methods
.method casListeners(Landroidx/concurrent/futures/AbstractResolvableFuture;Landroidx/concurrent/futures/AbstractResolvableFuture$Listener;Landroidx/concurrent/futures/AbstractResolvableFuture$Listener;)Z
    .locals 1
    .param p2, "expect"    # Landroidx/concurrent/futures/AbstractResolvableFuture$Listener;
    .param p3, "update"    # Landroidx/concurrent/futures/AbstractResolvableFuture$Listener;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Landroidx/concurrent/futures/AbstractResolvableFuture<",
            "*>;",
            "Landroidx/concurrent/futures/AbstractResolvableFuture$Listener;",
            "Landroidx/concurrent/futures/AbstractResolvableFuture$Listener;",
            ")Z"
        }
    .end annotation

    .line 1172
    .local p1, "future":Landroidx/concurrent/futures/AbstractResolvableFuture;, "Landroidx/concurrent/futures/AbstractResolvableFuture<*>;"
    monitor-enter p1

    .line 1173
    :try_start_0
    iget-object v0, p1, Landroidx/concurrent/futures/AbstractResolvableFuture;->listeners:Landroidx/concurrent/futures/AbstractResolvableFuture$Listener;

    if-ne v0, p2, :cond_0

    .line 1174
    iput-object p3, p1, Landroidx/concurrent/futures/AbstractResolvableFuture;->listeners:Landroidx/concurrent/futures/AbstractResolvableFuture$Listener;

    .line 1175
    const/4 v0, 0x1

    monitor-exit p1

    return v0

    .line 1177
    :cond_0
    const/4 v0, 0x0

    monitor-exit p1

    return v0

    .line 1178
    :catchall_0
    move-exception v0

    monitor-exit p1
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    throw v0
.end method

.method casValue(Landroidx/concurrent/futures/AbstractResolvableFuture;Ljava/lang/Object;Ljava/lang/Object;)Z
    .locals 1
    .param p2, "expect"    # Ljava/lang/Object;
    .param p3, "update"    # Ljava/lang/Object;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Landroidx/concurrent/futures/AbstractResolvableFuture<",
            "*>;",
            "Ljava/lang/Object;",
            "Ljava/lang/Object;",
            ")Z"
        }
    .end annotation

    .line 1183
    .local p1, "future":Landroidx/concurrent/futures/AbstractResolvableFuture;, "Landroidx/concurrent/futures/AbstractResolvableFuture<*>;"
    monitor-enter p1

    .line 1184
    :try_start_0
    iget-object v0, p1, Landroidx/concurrent/futures/AbstractResolvableFuture;->value:Ljava/lang/Object;

    if-ne v0, p2, :cond_0

    .line 1185
    iput-object p3, p1, Landroidx/concurrent/futures/AbstractResolvableFuture;->value:Ljava/lang/Object;

    .line 1186
    const/4 v0, 0x1

    monitor-exit p1

    return v0

    .line 1188
    :cond_0
    const/4 v0, 0x0

    monitor-exit p1

    return v0

    .line 1189
    :catchall_0
    move-exception v0

    monitor-exit p1
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    throw v0
.end method

.method casWaiters(Landroidx/concurrent/futures/AbstractResolvableFuture;Landroidx/concurrent/futures/AbstractResolvableFuture$Waiter;Landroidx/concurrent/futures/AbstractResolvableFuture$Waiter;)Z
    .locals 1
    .param p2, "expect"    # Landroidx/concurrent/futures/AbstractResolvableFuture$Waiter;
    .param p3, "update"    # Landroidx/concurrent/futures/AbstractResolvableFuture$Waiter;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Landroidx/concurrent/futures/AbstractResolvableFuture<",
            "*>;",
            "Landroidx/concurrent/futures/AbstractResolvableFuture$Waiter;",
            "Landroidx/concurrent/futures/AbstractResolvableFuture$Waiter;",
            ")Z"
        }
    .end annotation

    .line 1161
    .local p1, "future":Landroidx/concurrent/futures/AbstractResolvableFuture;, "Landroidx/concurrent/futures/AbstractResolvableFuture<*>;"
    monitor-enter p1

    .line 1162
    :try_start_0
    iget-object v0, p1, Landroidx/concurrent/futures/AbstractResolvableFuture;->waiters:Landroidx/concurrent/futures/AbstractResolvableFuture$Waiter;

    if-ne v0, p2, :cond_0

    .line 1163
    iput-object p3, p1, Landroidx/concurrent/futures/AbstractResolvableFuture;->waiters:Landroidx/concurrent/futures/AbstractResolvableFuture$Waiter;

    .line 1164
    const/4 v0, 0x1

    monitor-exit p1

    return v0

    .line 1166
    :cond_0
    const/4 v0, 0x0

    monitor-exit p1

    return v0

    .line 1167
    :catchall_0
    move-exception v0

    monitor-exit p1
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    throw v0
.end method

.method putNext(Landroidx/concurrent/futures/AbstractResolvableFuture$Waiter;Landroidx/concurrent/futures/AbstractResolvableFuture$Waiter;)V
    .locals 0
    .param p1, "waiter"    # Landroidx/concurrent/futures/AbstractResolvableFuture$Waiter;
    .param p2, "newValue"    # Landroidx/concurrent/futures/AbstractResolvableFuture$Waiter;

    .line 1156
    iput-object p2, p1, Landroidx/concurrent/futures/AbstractResolvableFuture$Waiter;->next:Landroidx/concurrent/futures/AbstractResolvableFuture$Waiter;

    .line 1157
    return-void
.end method

.method putThread(Landroidx/concurrent/futures/AbstractResolvableFuture$Waiter;Ljava/lang/Thread;)V
    .locals 0
    .param p1, "waiter"    # Landroidx/concurrent/futures/AbstractResolvableFuture$Waiter;
    .param p2, "newValue"    # Ljava/lang/Thread;

    .line 1151
    iput-object p2, p1, Landroidx/concurrent/futures/AbstractResolvableFuture$Waiter;->thread:Ljava/lang/Thread;

    .line 1152
    return-void
.end method
