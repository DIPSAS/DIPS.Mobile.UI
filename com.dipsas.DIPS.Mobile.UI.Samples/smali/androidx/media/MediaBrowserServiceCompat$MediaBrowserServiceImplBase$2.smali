.class Landroidx/media/MediaBrowserServiceCompat$MediaBrowserServiceImplBase$2;
.super Ljava/lang/Object;
.source "MediaBrowserServiceCompat.java"

# interfaces
.implements Ljava/lang/Runnable;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Landroidx/media/MediaBrowserServiceCompat$MediaBrowserServiceImplBase;->notifyChildrenChanged(Ljava/lang/String;Landroid/os/Bundle;)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$1:Landroidx/media/MediaBrowserServiceCompat$MediaBrowserServiceImplBase;

.field final synthetic val$options:Landroid/os/Bundle;

.field final synthetic val$parentId:Ljava/lang/String;


# direct methods
.method constructor <init>(Landroidx/media/MediaBrowserServiceCompat$MediaBrowserServiceImplBase;Ljava/lang/String;Landroid/os/Bundle;)V
    .locals 0
    .param p1, "this$1"    # Landroidx/media/MediaBrowserServiceCompat$MediaBrowserServiceImplBase;
    .annotation system Ldalvik/annotation/MethodParameters;
        accessFlags = {
            0x8010,
            0x1010,
            0x1010
        }
        names = {
            "this$1",
            "val$parentId",
            "val$options"
        }
    .end annotation

    .line 241
    iput-object p1, p0, Landroidx/media/MediaBrowserServiceCompat$MediaBrowserServiceImplBase$2;->this$1:Landroidx/media/MediaBrowserServiceCompat$MediaBrowserServiceImplBase;

    iput-object p2, p0, Landroidx/media/MediaBrowserServiceCompat$MediaBrowserServiceImplBase$2;->val$parentId:Ljava/lang/String;

    iput-object p3, p0, Landroidx/media/MediaBrowserServiceCompat$MediaBrowserServiceImplBase$2;->val$options:Landroid/os/Bundle;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public run()V
    .locals 6

    .line 244
    iget-object v0, p0, Landroidx/media/MediaBrowserServiceCompat$MediaBrowserServiceImplBase$2;->this$1:Landroidx/media/MediaBrowserServiceCompat$MediaBrowserServiceImplBase;

    iget-object v0, v0, Landroidx/media/MediaBrowserServiceCompat$MediaBrowserServiceImplBase;->this$0:Landroidx/media/MediaBrowserServiceCompat;

    iget-object v0, v0, Landroidx/media/MediaBrowserServiceCompat;->mConnections:Landroidx/collection/ArrayMap;

    invoke-virtual {v0}, Landroidx/collection/ArrayMap;->keySet()Ljava/util/Set;

    move-result-object v0

    invoke-interface {v0}, Ljava/util/Set;->iterator()Ljava/util/Iterator;

    move-result-object v0

    :goto_0
    invoke-interface {v0}, Ljava/util/Iterator;->hasNext()Z

    move-result v1

    if-eqz v1, :cond_0

    invoke-interface {v0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Landroid/os/IBinder;

    .line 245
    .local v1, "binder":Landroid/os/IBinder;
    iget-object v2, p0, Landroidx/media/MediaBrowserServiceCompat$MediaBrowserServiceImplBase$2;->this$1:Landroidx/media/MediaBrowserServiceCompat$MediaBrowserServiceImplBase;

    iget-object v2, v2, Landroidx/media/MediaBrowserServiceCompat$MediaBrowserServiceImplBase;->this$0:Landroidx/media/MediaBrowserServiceCompat;

    iget-object v2, v2, Landroidx/media/MediaBrowserServiceCompat;->mConnections:Landroidx/collection/ArrayMap;

    invoke-virtual {v2, v1}, Landroidx/collection/ArrayMap;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Landroidx/media/MediaBrowserServiceCompat$ConnectionRecord;

    .line 246
    .local v2, "connection":Landroidx/media/MediaBrowserServiceCompat$ConnectionRecord;
    iget-object v3, p0, Landroidx/media/MediaBrowserServiceCompat$MediaBrowserServiceImplBase$2;->this$1:Landroidx/media/MediaBrowserServiceCompat$MediaBrowserServiceImplBase;

    iget-object v4, p0, Landroidx/media/MediaBrowserServiceCompat$MediaBrowserServiceImplBase$2;->val$parentId:Ljava/lang/String;

    iget-object v5, p0, Landroidx/media/MediaBrowserServiceCompat$MediaBrowserServiceImplBase$2;->val$options:Landroid/os/Bundle;

    invoke-virtual {v3, v2, v4, v5}, Landroidx/media/MediaBrowserServiceCompat$MediaBrowserServiceImplBase;->notifyChildrenChangedOnHandler(Landroidx/media/MediaBrowserServiceCompat$ConnectionRecord;Ljava/lang/String;Landroid/os/Bundle;)V

    .line 247
    .end local v1    # "binder":Landroid/os/IBinder;
    .end local v2    # "connection":Landroidx/media/MediaBrowserServiceCompat$ConnectionRecord;
    goto :goto_0

    .line 248
    :cond_0
    return-void
.end method
