.class Landroidx/media/MediaBrowserServiceCompat$2;
.super Landroidx/media/MediaBrowserServiceCompat$Result;
.source "MediaBrowserServiceCompat.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Landroidx/media/MediaBrowserServiceCompat;->performLoadItem(Ljava/lang/String;Landroidx/media/MediaBrowserServiceCompat$ConnectionRecord;Landroid/support/v4/os/ResultReceiver;)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation

.annotation system Ldalvik/annotation/Signature;
    value = {
        "Landroidx/media/MediaBrowserServiceCompat$Result<",
        "Landroid/support/v4/media/MediaBrowserCompat$MediaItem;",
        ">;"
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

    .line 1785
    iput-object p1, p0, Landroidx/media/MediaBrowserServiceCompat$2;->this$0:Landroidx/media/MediaBrowserServiceCompat;

    iput-object p3, p0, Landroidx/media/MediaBrowserServiceCompat$2;->val$receiver:Landroid/support/v4/os/ResultReceiver;

    invoke-direct {p0, p2}, Landroidx/media/MediaBrowserServiceCompat$Result;-><init>(Ljava/lang/Object;)V

    return-void
.end method


# virtual methods
.method onResultSent(Landroid/support/v4/media/MediaBrowserCompat$MediaItem;)V
    .locals 3
    .param p1, "item"    # Landroid/support/v4/media/MediaBrowserCompat$MediaItem;
    .annotation system Ldalvik/annotation/MethodParameters;
        accessFlags = {
            0x0
        }
        names = {
            "item"
        }
    .end annotation

    .line 1788
    invoke-virtual {p0}, Landroidx/media/MediaBrowserServiceCompat$2;->getFlags()I

    move-result v0

    and-int/lit8 v0, v0, 0x2

    if-eqz v0, :cond_0

    .line 1789
    iget-object v0, p0, Landroidx/media/MediaBrowserServiceCompat$2;->val$receiver:Landroid/support/v4/os/ResultReceiver;

    const/4 v1, -0x1

    const/4 v2, 0x0

    invoke-virtual {v0, v1, v2}, Landroid/support/v4/os/ResultReceiver;->send(ILandroid/os/Bundle;)V

    .line 1790
    return-void

    .line 1792
    :cond_0
    new-instance v0, Landroid/os/Bundle;

    invoke-direct {v0}, Landroid/os/Bundle;-><init>()V

    .line 1793
    .local v0, "bundle":Landroid/os/Bundle;
    const-string v1, "media_item"

    invoke-virtual {v0, v1, p1}, Landroid/os/Bundle;->putParcelable(Ljava/lang/String;Landroid/os/Parcelable;)V

    .line 1794
    iget-object v1, p0, Landroidx/media/MediaBrowserServiceCompat$2;->val$receiver:Landroid/support/v4/os/ResultReceiver;

    const/4 v2, 0x0

    invoke-virtual {v1, v2, v0}, Landroid/support/v4/os/ResultReceiver;->send(ILandroid/os/Bundle;)V

    .line 1795
    return-void
.end method

.method bridge synthetic onResultSent(Ljava/lang/Object;)V
    .locals 0
    .annotation system Ldalvik/annotation/MethodParameters;
        accessFlags = {
            0x1000
        }
        names = {
            "item"
        }
    .end annotation

    .line 1785
    check-cast p1, Landroid/support/v4/media/MediaBrowserCompat$MediaItem;

    invoke-virtual {p0, p1}, Landroidx/media/MediaBrowserServiceCompat$2;->onResultSent(Landroid/support/v4/media/MediaBrowserCompat$MediaItem;)V

    return-void
.end method
