.class Landroid/support/v4/media/session/PlaybackStateCompat$Api21Impl;
.super Ljava/lang/Object;
.source "PlaybackStateCompat.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Landroid/support/v4/media/session/PlaybackStateCompat;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0xa
    name = "Api21Impl"
.end annotation


# direct methods
.method private constructor <init>()V
    .locals 0

    .line 1386
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method

.method static addCustomAction(Landroid/media/session/PlaybackState$Builder;Landroid/media/session/PlaybackState$CustomAction;)V
    .locals 0
    .param p0, "builder"    # Landroid/media/session/PlaybackState$Builder;
    .param p1, "customAction"    # Landroid/media/session/PlaybackState$CustomAction;
    .annotation system Ldalvik/annotation/MethodParameters;
        accessFlags = {
            0x0,
            0x0
        }
        names = {
            "builder",
            "customAction"
        }
    .end annotation

    .line 1417
    invoke-virtual {p0, p1}, Landroid/media/session/PlaybackState$Builder;->addCustomAction(Landroid/media/session/PlaybackState$CustomAction;)Landroid/media/session/PlaybackState$Builder;

    .line 1418
    return-void
.end method

.method static build(Landroid/media/session/PlaybackState$CustomAction$Builder;)Landroid/media/session/PlaybackState$CustomAction;
    .locals 1
    .param p0, "builder"    # Landroid/media/session/PlaybackState$CustomAction$Builder;
    .annotation system Ldalvik/annotation/MethodParameters;
        accessFlags = {
            0x0
        }
        names = {
            "builder"
        }
    .end annotation

    .line 1488
    invoke-virtual {p0}, Landroid/media/session/PlaybackState$CustomAction$Builder;->build()Landroid/media/session/PlaybackState$CustomAction;

    move-result-object v0

    return-object v0
.end method

.method static build(Landroid/media/session/PlaybackState$Builder;)Landroid/media/session/PlaybackState;
    .locals 1
    .param p0, "builder"    # Landroid/media/session/PlaybackState$Builder;
    .annotation system Ldalvik/annotation/MethodParameters;
        accessFlags = {
            0x0
        }
        names = {
            "builder"
        }
    .end annotation

    .line 1432
    invoke-virtual {p0}, Landroid/media/session/PlaybackState$Builder;->build()Landroid/media/session/PlaybackState;

    move-result-object v0

    return-object v0
.end method

.method static createBuilder()Landroid/media/session/PlaybackState$Builder;
    .locals 1

    .line 1390
    new-instance v0, Landroid/media/session/PlaybackState$Builder;

    invoke-direct {v0}, Landroid/media/session/PlaybackState$Builder;-><init>()V

    return-object v0
.end method

.method static createCustomActionBuilder(Ljava/lang/String;Ljava/lang/CharSequence;I)Landroid/media/session/PlaybackState$CustomAction$Builder;
    .locals 1
    .param p0, "action"    # Ljava/lang/String;
    .param p1, "name"    # Ljava/lang/CharSequence;
    .param p2, "icon"    # I
    .annotation system Ldalvik/annotation/MethodParameters;
        accessFlags = {
            0x0,
            0x0,
            0x0
        }
        names = {
            "action",
            "name",
            "icon"
        }
    .end annotation

    .line 1478
    new-instance v0, Landroid/media/session/PlaybackState$CustomAction$Builder;

    invoke-direct {v0, p0, p1, p2}, Landroid/media/session/PlaybackState$CustomAction$Builder;-><init>(Ljava/lang/String;Ljava/lang/CharSequence;I)V

    return-object v0
.end method

.method static getAction(Landroid/media/session/PlaybackState$CustomAction;)Ljava/lang/String;
    .locals 1
    .param p0, "customAction"    # Landroid/media/session/PlaybackState$CustomAction;
    .annotation system Ldalvik/annotation/MethodParameters;
        accessFlags = {
            0x0
        }
        names = {
            "customAction"
        }
    .end annotation

    .line 1498
    invoke-virtual {p0}, Landroid/media/session/PlaybackState$CustomAction;->getAction()Ljava/lang/String;

    move-result-object v0

    return-object v0
.end method

.method static getActions(Landroid/media/session/PlaybackState;)J
    .locals 2
    .param p0, "state"    # Landroid/media/session/PlaybackState;
    .annotation system Ldalvik/annotation/MethodParameters;
        accessFlags = {
            0x0
        }
        names = {
            "state"
        }
    .end annotation

    .line 1457
    invoke-virtual {p0}, Landroid/media/session/PlaybackState;->getActions()J

    move-result-wide v0

    return-wide v0
.end method

.method static getActiveQueueItemId(Landroid/media/session/PlaybackState;)J
    .locals 2
    .param p0, "state"    # Landroid/media/session/PlaybackState;
    .annotation system Ldalvik/annotation/MethodParameters;
        accessFlags = {
            0x0
        }
        names = {
            "state"
        }
    .end annotation

    .line 1472
    invoke-virtual {p0}, Landroid/media/session/PlaybackState;->getActiveQueueItemId()J

    move-result-wide v0

    return-wide v0
.end method

.method static getBufferedPosition(Landroid/media/session/PlaybackState;)J
    .locals 2
    .param p0, "state"    # Landroid/media/session/PlaybackState;
    .annotation system Ldalvik/annotation/MethodParameters;
        accessFlags = {
            0x0
        }
        names = {
            "state"
        }
    .end annotation

    .line 1447
    invoke-virtual {p0}, Landroid/media/session/PlaybackState;->getBufferedPosition()J

    move-result-wide v0

    return-wide v0
.end method

.method static getCustomActions(Landroid/media/session/PlaybackState;)Ljava/util/List;
    .locals 1
    .param p0, "state"    # Landroid/media/session/PlaybackState;
    .annotation system Ldalvik/annotation/MethodParameters;
        accessFlags = {
            0x0
        }
        names = {
            "state"
        }
    .end annotation

    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Landroid/media/session/PlaybackState;",
            ")",
            "Ljava/util/List<",
            "Landroid/media/session/PlaybackState$CustomAction;",
            ">;"
        }
    .end annotation

    .line 1427
    invoke-virtual {p0}, Landroid/media/session/PlaybackState;->getCustomActions()Ljava/util/List;

    move-result-object v0

    return-object v0
.end method

.method static getErrorMessage(Landroid/media/session/PlaybackState;)Ljava/lang/CharSequence;
    .locals 1
    .param p0, "state"    # Landroid/media/session/PlaybackState;
    .annotation system Ldalvik/annotation/MethodParameters;
        accessFlags = {
            0x0
        }
        names = {
            "state"
        }
    .end annotation

    .line 1462
    invoke-virtual {p0}, Landroid/media/session/PlaybackState;->getErrorMessage()Ljava/lang/CharSequence;

    move-result-object v0

    return-object v0
.end method

.method static getExtras(Landroid/media/session/PlaybackState$CustomAction;)Landroid/os/Bundle;
    .locals 1
    .param p0, "customAction"    # Landroid/media/session/PlaybackState$CustomAction;
    .annotation system Ldalvik/annotation/MethodParameters;
        accessFlags = {
            0x0
        }
        names = {
            "customAction"
        }
    .end annotation

    .line 1493
    invoke-virtual {p0}, Landroid/media/session/PlaybackState$CustomAction;->getExtras()Landroid/os/Bundle;

    move-result-object v0

    return-object v0
.end method

.method static getIcon(Landroid/media/session/PlaybackState$CustomAction;)I
    .locals 1
    .param p0, "customAction"    # Landroid/media/session/PlaybackState$CustomAction;
    .annotation system Ldalvik/annotation/MethodParameters;
        accessFlags = {
            0x0
        }
        names = {
            "customAction"
        }
    .end annotation

    .line 1508
    invoke-virtual {p0}, Landroid/media/session/PlaybackState$CustomAction;->getIcon()I

    move-result v0

    return v0
.end method

.method static getLastPositionUpdateTime(Landroid/media/session/PlaybackState;)J
    .locals 2
    .param p0, "state"    # Landroid/media/session/PlaybackState;
    .annotation system Ldalvik/annotation/MethodParameters;
        accessFlags = {
            0x0
        }
        names = {
            "state"
        }
    .end annotation

    .line 1467
    invoke-virtual {p0}, Landroid/media/session/PlaybackState;->getLastPositionUpdateTime()J

    move-result-wide v0

    return-wide v0
.end method

.method static getName(Landroid/media/session/PlaybackState$CustomAction;)Ljava/lang/CharSequence;
    .locals 1
    .param p0, "customAction"    # Landroid/media/session/PlaybackState$CustomAction;
    .annotation system Ldalvik/annotation/MethodParameters;
        accessFlags = {
            0x0
        }
        names = {
            "customAction"
        }
    .end annotation

    .line 1503
    invoke-virtual {p0}, Landroid/media/session/PlaybackState$CustomAction;->getName()Ljava/lang/CharSequence;

    move-result-object v0

    return-object v0
.end method

.method static getPlaybackSpeed(Landroid/media/session/PlaybackState;)F
    .locals 1
    .param p0, "state"    # Landroid/media/session/PlaybackState;
    .annotation system Ldalvik/annotation/MethodParameters;
        accessFlags = {
            0x0
        }
        names = {
            "state"
        }
    .end annotation

    .line 1452
    invoke-virtual {p0}, Landroid/media/session/PlaybackState;->getPlaybackSpeed()F

    move-result v0

    return v0
.end method

.method static getPosition(Landroid/media/session/PlaybackState;)J
    .locals 2
    .param p0, "state"    # Landroid/media/session/PlaybackState;
    .annotation system Ldalvik/annotation/MethodParameters;
        accessFlags = {
            0x0
        }
        names = {
            "state"
        }
    .end annotation

    .line 1442
    invoke-virtual {p0}, Landroid/media/session/PlaybackState;->getPosition()J

    move-result-wide v0

    return-wide v0
.end method

.method static getState(Landroid/media/session/PlaybackState;)I
    .locals 1
    .param p0, "state"    # Landroid/media/session/PlaybackState;
    .annotation system Ldalvik/annotation/MethodParameters;
        accessFlags = {
            0x0
        }
        names = {
            "state"
        }
    .end annotation

    .line 1437
    invoke-virtual {p0}, Landroid/media/session/PlaybackState;->getState()I

    move-result v0

    return v0
.end method

.method static setActions(Landroid/media/session/PlaybackState$Builder;J)V
    .locals 0
    .param p0, "builder"    # Landroid/media/session/PlaybackState$Builder;
    .param p1, "actions"    # J
    .annotation system Ldalvik/annotation/MethodParameters;
        accessFlags = {
            0x0,
            0x0
        }
        names = {
            "builder",
            "actions"
        }
    .end annotation

    .line 1406
    invoke-virtual {p0, p1, p2}, Landroid/media/session/PlaybackState$Builder;->setActions(J)Landroid/media/session/PlaybackState$Builder;

    .line 1407
    return-void
.end method

.method static setActiveQueueItemId(Landroid/media/session/PlaybackState$Builder;J)V
    .locals 0
    .param p0, "builder"    # Landroid/media/session/PlaybackState$Builder;
    .param p1, "id"    # J
    .annotation system Ldalvik/annotation/MethodParameters;
        accessFlags = {
            0x0,
            0x0
        }
        names = {
            "builder",
            "id"
        }
    .end annotation

    .line 1422
    invoke-virtual {p0, p1, p2}, Landroid/media/session/PlaybackState$Builder;->setActiveQueueItemId(J)Landroid/media/session/PlaybackState$Builder;

    .line 1423
    return-void
.end method

.method static setBufferedPosition(Landroid/media/session/PlaybackState$Builder;J)V
    .locals 0
    .param p0, "builder"    # Landroid/media/session/PlaybackState$Builder;
    .param p1, "bufferedPosition"    # J
    .annotation system Ldalvik/annotation/MethodParameters;
        accessFlags = {
            0x0,
            0x0
        }
        names = {
            "builder",
            "bufferedPosition"
        }
    .end annotation

    .line 1401
    invoke-virtual {p0, p1, p2}, Landroid/media/session/PlaybackState$Builder;->setBufferedPosition(J)Landroid/media/session/PlaybackState$Builder;

    .line 1402
    return-void
.end method

.method static setErrorMessage(Landroid/media/session/PlaybackState$Builder;Ljava/lang/CharSequence;)V
    .locals 0
    .param p0, "builder"    # Landroid/media/session/PlaybackState$Builder;
    .param p1, "error"    # Ljava/lang/CharSequence;
    .annotation system Ldalvik/annotation/MethodParameters;
        accessFlags = {
            0x0,
            0x0
        }
        names = {
            "builder",
            "error"
        }
    .end annotation

    .line 1411
    invoke-virtual {p0, p1}, Landroid/media/session/PlaybackState$Builder;->setErrorMessage(Ljava/lang/CharSequence;)Landroid/media/session/PlaybackState$Builder;

    .line 1412
    return-void
.end method

.method static setExtras(Landroid/media/session/PlaybackState$CustomAction$Builder;Landroid/os/Bundle;)V
    .locals 0
    .param p0, "builder"    # Landroid/media/session/PlaybackState$CustomAction$Builder;
    .param p1, "extras"    # Landroid/os/Bundle;
    .annotation system Ldalvik/annotation/MethodParameters;
        accessFlags = {
            0x0,
            0x0
        }
        names = {
            "builder",
            "extras"
        }
    .end annotation

    .line 1483
    invoke-virtual {p0, p1}, Landroid/media/session/PlaybackState$CustomAction$Builder;->setExtras(Landroid/os/Bundle;)Landroid/media/session/PlaybackState$CustomAction$Builder;

    .line 1484
    return-void
.end method

.method static setState(Landroid/media/session/PlaybackState$Builder;IJFJ)V
    .locals 0
    .param p0, "builder"    # Landroid/media/session/PlaybackState$Builder;
    .param p1, "state"    # I
    .param p2, "position"    # J
    .param p4, "playbackSpeed"    # F
    .param p5, "updateTime"    # J
    .annotation system Ldalvik/annotation/MethodParameters;
        accessFlags = {
            0x0,
            0x0,
            0x0,
            0x0,
            0x0
        }
        names = {
            "builder",
            "state",
            "position",
            "playbackSpeed",
            "updateTime"
        }
    .end annotation

    .line 1396
    invoke-virtual/range {p0 .. p6}, Landroid/media/session/PlaybackState$Builder;->setState(IJFJ)Landroid/media/session/PlaybackState$Builder;

    .line 1397
    return-void
.end method
