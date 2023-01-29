.class Landroidx/media/MediaBrowserServiceCompat$3;
.super Landroidx/media/MediaBrowserServiceCompat$Result;
.source "MediaBrowserServiceCompat.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Landroidx/media/MediaBrowserServiceCompat;->performSearch(Ljava/lang/String;Landroid/os/Bundle;Landroidx/media/MediaBrowserServiceCompat$ConnectionRecord;Landroid/support/v4/os/ResultReceiver;)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation

.annotation system Ldalvik/annotation/Signature;
    value = {
        "Landroidx/media/MediaBrowserServiceCompat$Result<",
        "Ljava/util/List<",
        "Landroid/support/v4/media/MediaBrowserCompat$MediaItem;",
        ">;>;"
    }
.end annotation


# instance fields
.field final synthetic this$0:Landroidx/media/MediaBrowserServiceCompat;

.field final synthetic val$receiver:Landroid/support/v4/os/ResultReceiver;


# direct methods
.method constructor <init>(Landroidx/media/MediaBrowserServiceCompat;Ljava/lang/Object;Landroid/support/v4/os/ResultReceiver;)V
    .locals 0
    .param p1, "this$0"    # Landroidx/media/MediaBrowserServiceCompat;
    .param p2, "debug"    # Ljava/lang/Object;
    .annotation system Ldalvik/annotation/MethodParameters;
        accessFlags = {
            0x8010,
            0x0,
            0x1010
        }
        names = {
            "this$0",
            "debug",
            "val$receiver"
        }
    .end annotation

    .line 1811
    iput-object p1, p0, Landroidx/media/MediaBrowserServiceCompat$3;->this$0:Landroidx/media/MediaBrowserServiceCompat;

    iput-object p3, p0, Landroidx/media/MediaBrowserServiceCompat$3;->val$receiver:Landroid/support/v4/os/ResultReceiver;

    invoke-direct {p0, p2}, Landroidx/media/MediaBrowserServiceCompat$Result;-><init>(Ljava/lang/Object;)V

    return-void
.end method


# virtual methods
.method bridge synthetic onResultSent(Ljava/lang/Object;)V
    .locals 0
    .annotation system Ldalvik/annotation/MethodParameters;
        accessFlags = {
            0x1000
        }
        names = {
            "items"
        }
    .end annotation

    .line 1811
    check-cast p1, Ljava/util/List;

    invoke-virtual {p0, p1}, Landroidx/media/MediaBrowserServiceCompat$3;->onResultSent(Ljava/util/List;)V

    return-void
.end method

.method onResultSent(Ljava/util/List;)V
    .locals 4
    .annotation system Ldalvik/annotation/MethodParameters;
        accessFlags = {
            0x0
        }
        names = {
            "items"
        }
    .end annotation

    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/List<",
            "Landroid/support/v4/media/MediaBrowserCompat$MediaItem;",
            ">;)V"
        }
    .end annotation

    .line 1814
    .local p1, "items":Ljava/util/List;, "Ljava/util/List<Landroid/support/v4/media/MediaBrowserCompat$MediaItem;>;"
    invoke-virtual {p0}, Landroidx/media/MediaBrowserServiceCompat$3;->getFlags()I

    move-result v0

    and-int/lit8 v0, v0, 0x4

    if-nez v0, :cond_1

    if-nez p1, :cond_0

    goto :goto_0

    .line 1819
    :cond_0
    new-instance v0, Landroid/os/Bundle;

    invoke-direct {v0}, Landroid/os/Bundle;-><init>()V

    .line 1820
    .local v0, "bundle":Landroid/os/Bundle;
    const/4 v1, 0x0

    new-array v2, v1, [Landroid/support/v4/media/MediaBrowserCompat$MediaItem;

    .line 1821
    invoke-interface {p1, v2}, Ljava/util/List;->toArray([Ljava/lang/Object;)[Ljava/lang/Object;

    move-result-object v2

    check-cast v2, [Landroid/os/Parcelable;

    .line 1820
    const-string v3, "search_results"

    invoke-virtual {v0, v3, v2}, Landroid/os/Bundle;->putParcelableArray(Ljava/lang/String;[Landroid/os/Parcelable;)V

    .line 1822
    iget-object v2, p0, Landroidx/media/MediaBrowserServiceCompat$3;->val$receiver:Landroid/support/v4/os/ResultReceiver;

    invoke-virtual {v2, v1, v0}, Landroid/support/v4/os/ResultReceiver;->send(ILandroid/os/Bundle;)V

    .line 1823
    return-void

    .line 1816
    .end local v0    # "bundle":Landroid/os/Bundle;
    :cond_1
    :goto_0
    iget-object v0, p0, Landroidx/media/MediaBrowserServiceCompat$3;->val$receiver:Landroid/support/v4/os/ResultReceiver;

    const/4 v1, -0x1

    const/4 v2, 0x0

    invoke-virtual {v0, v1, v2}, Landroid/support/v4/os/ResultReceiver;->send(ILandroid/os/Bundle;)V

    .line 1817
    return-void
.end method
