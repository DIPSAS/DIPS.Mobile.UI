.class Landroid/support/v4/media/session/MediaControllerCompat$TransportControlsApi29;
.super Landroid/support/v4/media/session/MediaControllerCompat$TransportControlsApi24;
.source "MediaControllerCompat.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Landroid/support/v4/media/session/MediaControllerCompat;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x8
    name = "TransportControlsApi29"
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

    .line 2621
    invoke-direct {p0, p1}, Landroid/support/v4/media/session/MediaControllerCompat$TransportControlsApi24;-><init>(Landroid/media/session/MediaController$TransportControls;)V

    .line 2622
    return-void
.end method


# virtual methods
.method public setPlaybackSpeed(F)V
    .locals 2
    .param p1, "speed"    # F
    .annotation system Ldalvik/annotation/MethodParameters;
        accessFlags = {
            0x0
        }
        names = {
            "speed"
        }
    .end annotation

    .line 2626
    const/4 v0, 0x0

    cmpl-float v0, p1, v0

    if-eqz v0, :cond_0

    .line 2629
    iget-object v0, p0, Landroid/support/v4/media/session/MediaControllerCompat$TransportControlsApi29;->mControlsFwk:Landroid/media/session/MediaController$TransportControls;

    invoke-virtual {v0, p1}, Landroid/media/session/MediaController$TransportControls;->setPlaybackSpeed(F)V

    .line 2630
    return-void

    .line 2627
    :cond_0
    new-instance v0, Ljava/lang/IllegalArgumentException;

    const-string v1, "speed must not be zero"

    invoke-direct {v0, v1}, Ljava/lang/IllegalArgumentException;-><init>(Ljava/lang/String;)V

    throw v0
.end method
