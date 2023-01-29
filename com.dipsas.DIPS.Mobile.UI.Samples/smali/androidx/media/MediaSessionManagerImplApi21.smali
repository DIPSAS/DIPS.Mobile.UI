.class Landroidx/media/MediaSessionManagerImplApi21;
.super Landroidx/media/MediaSessionManagerImplBase;
.source "MediaSessionManagerImplApi21.java"


# direct methods
.method constructor <init>(Landroid/content/Context;)V
    .locals 0
    .param p1, "context"    # Landroid/content/Context;
    .annotation system Ldalvik/annotation/MethodParameters;
        accessFlags = {
            0x0
        }
        names = {
            "context"
        }
    .end annotation

    .line 28
    invoke-direct {p0, p1}, Landroidx/media/MediaSessionManagerImplBase;-><init>(Landroid/content/Context;)V

    .line 29
    iput-object p1, p0, Landroidx/media/MediaSessionManagerImplApi21;->mContext:Landroid/content/Context;

    .line 30
    return-void
.end method

.method private hasMediaControlPermission(Landroidx/media/MediaSessionManager$RemoteUserInfoImpl;)Z
    .locals 4
    .param p1, "userInfo"    # Landroidx/media/MediaSessionManager$RemoteUserInfoImpl;
    .annotation system Ldalvik/annotation/MethodParameters;
        accessFlags = {
            0x0
        }
        names = {
            "userInfo"
        }
    .end annotation

    .line 44
    invoke-virtual {p0}, Landroidx/media/MediaSessionManagerImplApi21;->getContext()Landroid/content/Context;

    move-result-object v0

    .line 46
    invoke-interface {p1}, Landroidx/media/MediaSessionManager$RemoteUserInfoImpl;->getPid()I

    move-result v1

    invoke-interface {p1}, Landroidx/media/MediaSessionManager$RemoteUserInfoImpl;->getUid()I

    move-result v2

    .line 44
    const-string v3, "android.permission.MEDIA_CONTENT_CONTROL"

    invoke-virtual {v0, v3, v1, v2}, Landroid/content/Context;->checkPermission(Ljava/lang/String;II)I

    move-result v0

    if-nez v0, :cond_0

    const/4 v0, 0x1

    goto :goto_0

    :cond_0
    const/4 v0, 0x0

    :goto_0
    return v0
.end method


# virtual methods
.method public isTrustedForMediaControl(Landroidx/media/MediaSessionManager$RemoteUserInfoImpl;)Z
    .locals 1
    .param p1, "userInfo"    # Landroidx/media/MediaSessionManager$RemoteUserInfoImpl;
    .annotation system Ldalvik/annotation/MethodParameters;
        accessFlags = {
            0x0
        }
        names = {
            "userInfo"
        }
    .end annotation

    .line 36
    invoke-direct {p0, p1}, Landroidx/media/MediaSessionManagerImplApi21;->hasMediaControlPermission(Landroidx/media/MediaSessionManager$RemoteUserInfoImpl;)Z

    move-result v0

    if-nez v0, :cond_1

    invoke-super {p0, p1}, Landroidx/media/MediaSessionManagerImplBase;->isTrustedForMediaControl(Landroidx/media/MediaSessionManager$RemoteUserInfoImpl;)Z

    move-result v0

    if-eqz v0, :cond_0

    goto :goto_0

    :cond_0
    const/4 v0, 0x0

    goto :goto_1

    :cond_1
    :goto_0
    const/4 v0, 0x1

    :goto_1
    return v0
.end method
