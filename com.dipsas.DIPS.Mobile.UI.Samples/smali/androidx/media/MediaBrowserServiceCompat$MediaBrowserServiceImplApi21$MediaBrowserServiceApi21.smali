.class Landroidx/media/MediaBrowserServiceCompat$MediaBrowserServiceImplApi21$MediaBrowserServiceApi21;
.super Landroid/service/media/MediaBrowserService;
.source "MediaBrowserServiceCompat.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Landroidx/media/MediaBrowserServiceCompat$MediaBrowserServiceImplApi21;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = "MediaBrowserServiceApi21"
.end annotation


# instance fields
.field final synthetic this$1:Landroidx/media/MediaBrowserServiceCompat$MediaBrowserServiceImplApi21;


# direct methods
.method constructor <init>(Landroidx/media/MediaBrowserServiceCompat$MediaBrowserServiceImplApi21;Landroid/content/Context;)V
    .locals 0
    .param p1, "this$1"    # Landroidx/media/MediaBrowserServiceCompat$MediaBrowserServiceImplApi21;
    .param p2, "context"    # Landroid/content/Context;
    .annotation system Ldalvik/annotation/MethodParameters;
        accessFlags = {
            0x8010,
            0x0
        }
        names = {
            "this$1",
            "context"
        }
    .end annotation

    .line 499
    iput-object p1, p0, Landroidx/media/MediaBrowserServiceCompat$MediaBrowserServiceImplApi21$MediaBrowserServiceApi21;->this$1:Landroidx/media/MediaBrowserServiceCompat$MediaBrowserServiceImplApi21;

    invoke-direct {p0}, Landroid/service/media/MediaBrowserService;-><init>()V

    .line 500
    invoke-virtual {p0, p2}, Landroidx/media/MediaBrowserServiceCompat$MediaBrowserServiceImplApi21$MediaBrowserServiceApi21;->attachBaseContext(Landroid/content/Context;)V

    .line 501
    return-void
.end method


# virtual methods
.method public onGetRoot(Ljava/lang/String;ILandroid/os/Bundle;)Landroid/service/media/MediaBrowserService$BrowserRoot;
    .locals 4
    .param p1, "clientPackageName"    # Ljava/lang/String;
    .param p2, "clientUid"    # I
    .param p3, "rootHints"    # Landroid/os/Bundle;
    .annotation system Ldalvik/annotation/MethodParameters;
        accessFlags = {
            0x0,
            0x0,
            0x0
        }
        names = {
            "clientPackageName",
            "clientUid",
            "rootHints"
        }
    .end annotation

    .line 507
    invoke-static {p3}, Landroid/support/v4/media/session/MediaSessionCompat;->ensureClassLoader(Landroid/os/Bundle;)V

    .line 508
    iget-object v0, p0, Landroidx/media/MediaBrowserServiceCompat$MediaBrowserServiceImplApi21$MediaBrowserServiceApi21;->this$1:Landroidx/media/MediaBrowserServiceCompat$MediaBrowserServiceImplApi21;

    .line 510
    const/4 v1, 0x0

    if-nez p3, :cond_0

    move-object v2, v1

    goto :goto_0

    :cond_0
    new-instance v2, Landroid/os/Bundle;

    invoke-direct {v2, p3}, Landroid/os/Bundle;-><init>(Landroid/os/Bundle;)V

    .line 509
    :goto_0
    invoke-virtual {v0, p1, p2, v2}, Landroidx/media/MediaBrowserServiceCompat$MediaBrowserServiceImplApi21;->onGetRoot(Ljava/lang/String;ILandroid/os/Bundle;)Landroidx/media/MediaBrowserServiceCompat$BrowserRoot;

    move-result-object v0

    .line 511
    .local v0, "browserRootCompat":Landroidx/media/MediaBrowserServiceCompat$BrowserRoot;
    if-nez v0, :cond_1

    goto :goto_1

    :cond_1
    new-instance v1, Landroid/service/media/MediaBrowserService$BrowserRoot;

    .line 512
    invoke-static {v0}, Landroidx/media/MediaBrowserServiceCompat$BrowserRoot;->access$000(Landroidx/media/MediaBrowserServiceCompat$BrowserRoot;)Ljava/lang/String;

    move-result-object v2

    invoke-static {v0}, Landroidx/media/MediaBrowserServiceCompat$BrowserRoot;->access$100(Landroidx/media/MediaBrowserServiceCompat$BrowserRoot;)Landroid/os/Bundle;

    move-result-object v3

    invoke-direct {v1, v2, v3}, Landroid/service/media/MediaBrowserService$BrowserRoot;-><init>(Ljava/lang/String;Landroid/os/Bundle;)V

    .line 511
    :goto_1
    return-object v1
.end method

.method public onLoadChildren(Ljava/lang/String;Landroid/service/media/MediaBrowserService$Result;)V
    .locals 2
    .param p1, "parentId"    # Ljava/lang/String;
    .annotation system Ldalvik/annotation/MethodParameters;
        accessFlags = {
            0x0,
            0x0
        }
        names = {
            "parentId",
            "result"
        }
    .end annotation

    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/lang/String;",
            "Landroid/service/media/MediaBrowserService$Result<",
            "Ljava/util/List<",
            "Landroid/media/browse/MediaBrowser$MediaItem;",
            ">;>;)V"
        }
    .end annotation

    .line 518
    .local p2, "result":Landroid/service/media/MediaBrowserService$Result;, "Landroid/service/media/MediaBrowserService$Result<Ljava/util/List<Landroid/media/browse/MediaBrowser$MediaItem;>;>;"
    iget-object v0, p0, Landroidx/media/MediaBrowserServiceCompat$MediaBrowserServiceImplApi21$MediaBrowserServiceApi21;->this$1:Landroidx/media/MediaBrowserServiceCompat$MediaBrowserServiceImplApi21;

    new-instance v1, Landroidx/media/MediaBrowserServiceCompat$ResultWrapper;

    invoke-direct {v1, p2}, Landroidx/media/MediaBrowserServiceCompat$ResultWrapper;-><init>(Landroid/service/media/MediaBrowserService$Result;)V

    invoke-virtual {v0, p1, v1}, Landroidx/media/MediaBrowserServiceCompat$MediaBrowserServiceImplApi21;->onLoadChildren(Ljava/lang/String;Landroidx/media/MediaBrowserServiceCompat$ResultWrapper;)V

    .line 520
    return-void
.end method
