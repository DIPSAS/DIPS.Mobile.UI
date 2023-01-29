.class Landroid/support/v4/media/session/MediaControllerCompat$TransportControlsApi24;
.super Landroid/support/v4/media/session/MediaControllerCompat$TransportControlsApi23;
.source "MediaControllerCompat.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Landroid/support/v4/media/session/MediaControllerCompat;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x8
    name = "TransportControlsApi24"
.end annotation


# direct methods
.method constructor <init>(Landroid/media/session/MediaController$TransportControls;)V
    .locals 0
    .param p1, "controlsFwk"    # Landroid/media/session/MediaController$TransportControls;
    .annotation system Ldalvik/annotation/MethodParameters;
        accessFlags = {
            0x0
        }
        names = {
            "controlsFwk"
        }
    .end annotation

    .line 2594
    invoke-direct {p0, p1}, Landroid/support/v4/media/session/MediaControllerCompat$TransportControlsApi23;-><init>(Landroid/media/session/MediaController$TransportControls;)V

    .line 2595
    return-void
.end method


# virtual methods
.method public prepare()V
    .locals 1

    .line 2599
    iget-object v0, p0, Landroid/support/v4/media/session/MediaControllerCompat$TransportControlsApi24;->mControlsFwk:Landroid/media/session/MediaController$TransportControls;

    invoke-virtual {v0}, Landroid/media/session/MediaController$TransportControls;->prepare()V

    .line 2600
    return-void
.end method

.method public prepareFromMediaId(Ljava/lang/String;Landroid/os/Bundle;)V
    .locals 1
    .param p1, "mediaId"    # Ljava/lang/String;
    .param p2, "extras"    # Landroid/os/Bundle;
    .annotation system Ldalvik/annotation/MethodParameters;
        accessFlags = {
            0x0,
            0x0
        }
        names = {
            "mediaId",
            "extras"
        }
    .end annotation

    .line 2604
    iget-object v0, p0, Landroid/support/v4/media/session/MediaControllerCompat$TransportControlsApi24;->mControlsFwk:Landroid/media/session/MediaController$TransportControls;

    invoke-virtual {v0, p1, p2}, Landroid/media/session/MediaController$TransportControls;->prepareFromMediaId(Ljava/lang/String;Landroid/os/Bundle;)V

    .line 2605
    return-void
.end method

.method public prepareFromSearch(Ljava/lang/String;Landroid/os/Bundle;)V
    .locals 1
    .param p1, "query"    # Ljava/lang/String;
    .param p2, "extras"    # Landroid/os/Bundle;
    .annotation system Ldalvik/annotation/MethodParameters;
        accessFlags = {
            0x0,
            0x0
        }
        names = {
            "query",
            "extras"
        }
    .end annotation

    .line 2609
    iget-object v0, p0, Landroid/support/v4/media/session/MediaControllerCompat$TransportControlsApi24;->mControlsFwk:Landroid/media/session/MediaController$TransportControls;

    invoke-virtual {v0, p1, p2}, Landroid/media/session/MediaController$TransportControls;->prepareFromSearch(Ljava/lang/String;Landroid/os/Bundle;)V

    .line 2610
    return-void
.end method

.method public prepareFromUri(Landroid/net/Uri;Landroid/os/Bundle;)V
    .locals 1
    .param p1, "uri"    # Landroid/net/Uri;
    .param p2, "extras"    # Landroid/os/Bundle;
    .annotation system Ldalvik/annotation/MethodParameters;
        accessFlags = {
            0x0,
            0x0
        }
        names = {
            "uri",
            "extras"
        }
    .end annotation

    .line 2614
    iget-object v0, p0, Landroid/support/v4/media/session/MediaControllerCompat$TransportControlsApi24;->mControlsFwk:Landroid/media/session/MediaController$TransportControls;

    invoke-virtual {v0, p1, p2}, Landroid/media/session/MediaController$TransportControls;->prepareFromUri(Landroid/net/Uri;Landroid/os/Bundle;)V

    .line 2615
    return-void
.end method
