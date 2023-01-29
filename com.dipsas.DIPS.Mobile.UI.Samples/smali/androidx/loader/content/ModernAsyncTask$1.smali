.class Landroidx/loader/content/ModernAsyncTask$1;
.super Ljava/lang/Object;
.source "ModernAsyncTask.java"

# interfaces
.implements Ljava/util/concurrent/Callable;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Landroidx/loader/content/ModernAsyncTask;-><init>()V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation

.annotation system Ldalvik/annotation/Signature;
    value = {
        "Ljava/lang/Object;",
        "Ljava/util/concurrent/Callable<",
        "TResult;>;"
    }
.end annotation


# instance fields
.field final synthetic this$0:Landroidx/loader/content/ModernAsyncTask;


# direct methods
.method constructor <init>(Landroidx/loader/content/ModernAsyncTask;)V
    .locals 0
    .param p1, "this$0"    # Landroidx/loader/content/ModernAsyncTask;

    .line 89
    .local p0, "this":Landroidx/loader/content/ModernAsyncTask$1;, "Landroidx/loader/content/ModernAsyncTask$1;"
    iput-object p1, p0, Landroidx/loader/content/ModernAsyncTask$1;->this$0:Landroidx/loader/content/ModernAsyncTask;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public call()Ljava/lang/Object;
    .locals 4
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()TResult;"
        }
    .end annotation

    .line 92
    .local p0, "this":Landroidx/loader/content/ModernAsyncTask$1;, "Landroidx/loader/content/ModernAsyncTask$1;"
    iget-object v0, p0, Landroidx/loader/content/ModernAsyncTask$1;->this$0:Landroidx/loader/content/ModernAsyncTask;

    iget-object v0, v0, Landroidx/loader/content/ModernAsyncTask;->mTaskInvoked:Ljava/util/concurrent/atomic/AtomicBoolean;

    const/4 v1, 0x1

    invoke-virtual {v0, v1}, Ljava/util/concurrent/atomic/AtomicBoolean;->set(Z)V

    .line 93
    const/4 v0, 0x0

    .line 95
    .local v0, "result":Ljava/lang/Object;, "TResult;"
    const/16 v2, 0xa

    :try_start_0
    invoke-static {v2}, Landroid/os/Process;->setThreadPriority(I)V

    .line 96
    iget-object v2, p0, Landroidx/loader/content/ModernAsyncTask$1;->this$0:Landroidx/loader/content/ModernAsyncTask;

    invoke-virtual {v2}, Landroidx/loader/content/ModernAsyncTask;->doInBackground()Ljava/lang/Object;

    move-result-object v2

    move-object v0, v2

    .line 97
    invoke-static {}, Landroid/os/Binder;->flushPendingCommands()V
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    .line 102
    iget-object v1, p0, Landroidx/loader/content/ModernAsyncTask$1;->this$0:Landroidx/loader/content/ModernAsyncTask;

    invoke-virtual {v1, v0}, Landroidx/loader/content/ModernAsyncTask;->postResult(Ljava/lang/Object;)V

    .line 103
    nop

    .line 104
    return-object v0

    .line 98
    :catchall_0
    move-exception v2

    .line 99
    .local v2, "tr":Ljava/lang/Throwable;
    :try_start_1
    iget-object v3, p0, Landroidx/loader/content/ModernAsyncTask$1;->this$0:Landroidx/loader/content/ModernAsyncTask;

    iget-object v3, v3, Landroidx/loader/content/ModernAsyncTask;->mCancelled:Ljava/util/concurrent/atomic/AtomicBoolean;

    invoke-virtual {v3, v1}, Ljava/util/concurrent/atomic/AtomicBoolean;->set(Z)V

    .line 100
    nop

    .end local v0    # "result":Ljava/lang/Object;, "TResult;"
    throw v2
    :try_end_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_1

    .line 102
    .end local v2    # "tr":Ljava/lang/Throwable;
    .restart local v0    # "result":Ljava/lang/Object;, "TResult;"
    :catchall_1
    move-exception v1

    iget-object v2, p0, Landroidx/loader/content/ModernAsyncTask$1;->this$0:Landroidx/loader/content/ModernAsyncTask;

    invoke-virtual {v2, v0}, Landroidx/loader/content/ModernAsyncTask;->postResult(Ljava/lang/Object;)V

    throw v1
.end method
