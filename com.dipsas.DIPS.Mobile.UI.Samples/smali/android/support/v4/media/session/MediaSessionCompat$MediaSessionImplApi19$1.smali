.class Landroid/support/v4/media/session/MediaSessionCompat$MediaSessionImplApi19$1;
.super Ljava/lang/Object;
.source "MediaSessionCompat.java"

# interfaces
.implements Landroid/media/RemoteControlClient$OnMetadataUpdateListener;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Landroid/support/v4/media/session/MediaSessionCompat$MediaSessionImplApi19;->setCallback(Landroid/support/v4/media/session/MediaSessionCompat$Callback;Landroid/os/Handler;)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Landroid/support/v4/media/session/MediaSessionCompat$MediaSessionImplApi19;


# direct methods
.method constructor <init>(Landroid/support/v4/media/session/MediaSessionCompat$MediaSessionImplApi19;)V
    .locals 0
    .param p1, "this$0"    # Landroid/support/v4/media/session/MediaSessionCompat$MediaSessionImplApi19;
    .annotation system Ldalvik/annotation/MethodParameters;
        accessFlags = {
            0x8010
        }
        names = {
            "this$0"
        }
    .end annotation

    .line 3741
    iput-object p1, p0, Landroid/support/v4/media/session/MediaSessionCompat$MediaSessionImplApi19$1;->this$0:Landroid/support/v4/media/session/MediaSessionCompat$MediaSessionImplApi19;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public onMetadataUpdate(ILjava/lang/Object;)V
    .locals 7
    .param p1, "key"    # I
    .param p2, "newValue"    # Ljava/lang/Object;
    .annotation system Ldalvik/annotation/MethodParameters;
        accessFlags = {
            0x0,
            0x0
        }
        names = {
            "key",
            "newValue"
        }
    .end annotation

    .line 3744
    const v0, 0x10000001

    if-ne p1, v0, :cond_0

    instance-of v0, p2, Landroid/media/Rating;

    if-eqz v0, :cond_0

    .line 3746
    iget-object v1, p0, Landroid/support/v4/media/session/MediaSessionCompat$MediaSessionImplApi19$1;->this$0:Landroid/support/v4/media/session/MediaSessionCompat$MediaSessionImplApi19;

    const/16 v2, 0x13

    const/4 v3, -0x1

    const/4 v4, -0x1

    .line 3747
    invoke-static {p2}, Landroid/support/v4/media/RatingCompat;->fromRating(Ljava/lang/Object;)Landroid/support/v4/media/RatingCompat;

    move-result-object v5

    const/4 v6, 0x0

    .line 3746
    invoke-virtual/range {v1 .. v6}, Landroid/support/v4/media/session/MediaSessionCompat$MediaSessionImplApi19;->postToHandler(IIILjava/lang/Object;Landroid/os/Bundle;)V

    .line 3749
    :cond_0
    return-void
.end method
