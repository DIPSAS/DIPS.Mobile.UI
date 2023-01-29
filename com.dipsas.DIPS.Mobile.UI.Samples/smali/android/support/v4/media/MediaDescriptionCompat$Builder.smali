.class public final Landroid/support/v4/media/MediaDescriptionCompat$Builder;
.super Ljava/lang/Object;
.source "MediaDescriptionCompat.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Landroid/support/v4/media/MediaDescriptionCompat;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x19
    name = "Builder"
.end annotation


# instance fields
.field private mDescription:Ljava/lang/CharSequence;

.field private mExtras:Landroid/os/Bundle;

.field private mIcon:Landroid/graphics/Bitmap;

.field private mIconUri:Landroid/net/Uri;

.field private mMediaId:Ljava/lang/String;

.field private mMediaUri:Landroid/net/Uri;

.field private mSubtitle:Ljava/lang/CharSequence;

.field private mTitle:Ljava/lang/CharSequence;


# direct methods
.method public constructor <init>()V
    .locals 0

    .line 461
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 462
    return-void
.end method


# virtual methods
.method public build()Landroid/support/v4/media/MediaDescriptionCompat;
    .locals 10

    .line 562
    new-instance v9, Landroid/support/v4/media/MediaDescriptionCompat;

    iget-object v1, p0, Landroid/support/v4/media/MediaDescriptionCompat$Builder;->mMediaId:Ljava/lang/String;

    iget-object v2, p0, Landroid/support/v4/media/MediaDescriptionCompat$Builder;->mTitle:Ljava/lang/CharSequence;

    iget-object v3, p0, Landroid/support/v4/media/MediaDescriptionCompat$Builder;->mSubtitle:Ljava/lang/CharSequence;

    iget-object v4, p0, Landroid/support/v4/media/MediaDescriptionCompat$Builder;->mDescription:Ljava/lang/CharSequence;

    iget-object v5, p0, Landroid/support/v4/media/MediaDescriptionCompat$Builder;->mIcon:Landroid/graphics/Bitmap;

    iget-object v6, p0, Landroid/support/v4/media/MediaDescriptionCompat$Builder;->mIconUri:Landroid/net/Uri;

    iget-object v7, p0, Landroid/support/v4/media/MediaDescriptionCompat$Builder;->mExtras:Landroid/os/Bundle;

    iget-object v8, p0, Landroid/support/v4/media/MediaDescriptionCompat$Builder;->mMediaUri:Landroid/net/Uri;

    move-object v0, v9

    invoke-direct/range {v0 .. v8}, Landroid/support/v4/media/MediaDescriptionCompat;-><init>(Ljava/lang/String;Ljava/lang/CharSequence;Ljava/lang/CharSequence;Ljava/lang/CharSequence;Landroid/graphics/Bitmap;Landroid/net/Uri;Landroid/os/Bundle;Landroid/net/Uri;)V

    return-object v9
.end method

.method public setDescription(Ljava/lang/CharSequence;)Landroid/support/v4/media/MediaDescriptionCompat$Builder;
    .locals 0
    .param p1, "description"    # Ljava/lang/CharSequence;
    .annotation system Ldalvik/annotation/MethodParameters;
        accessFlags = {
            0x0
        }
        names = {
            "description"
        }
    .end annotation

    .line 505
    iput-object p1, p0, Landroid/support/v4/media/MediaDescriptionCompat$Builder;->mDescription:Ljava/lang/CharSequence;

    .line 506
    return-object p0
.end method

.method public setExtras(Landroid/os/Bundle;)Landroid/support/v4/media/MediaDescriptionCompat$Builder;
    .locals 0
    .param p1, "extras"    # Landroid/os/Bundle;
    .annotation system Ldalvik/annotation/MethodParameters;
        accessFlags = {
            0x0
        }
        names = {
            "extras"
        }
    .end annotation

    .line 540
    iput-object p1, p0, Landroid/support/v4/media/MediaDescriptionCompat$Builder;->mExtras:Landroid/os/Bundle;

    .line 541
    return-object p0
.end method

.method public setIconBitmap(Landroid/graphics/Bitmap;)Landroid/support/v4/media/MediaDescriptionCompat$Builder;
    .locals 0
    .param p1, "icon"    # Landroid/graphics/Bitmap;
    .annotation system Ldalvik/annotation/MethodParameters;
        accessFlags = {
            0x0
        }
        names = {
            "icon"
        }
    .end annotation

    .line 517
    iput-object p1, p0, Landroid/support/v4/media/MediaDescriptionCompat$Builder;->mIcon:Landroid/graphics/Bitmap;

    .line 518
    return-object p0
.end method

.method public setIconUri(Landroid/net/Uri;)Landroid/support/v4/media/MediaDescriptionCompat$Builder;
    .locals 0
    .param p1, "iconUri"    # Landroid/net/Uri;
    .annotation system Ldalvik/annotation/MethodParameters;
        accessFlags = {
            0x0
        }
        names = {
            "iconUri"
        }
    .end annotation

    .line 529
    iput-object p1, p0, Landroid/support/v4/media/MediaDescriptionCompat$Builder;->mIconUri:Landroid/net/Uri;

    .line 530
    return-object p0
.end method

.method public setMediaId(Ljava/lang/String;)Landroid/support/v4/media/MediaDescriptionCompat$Builder;
    .locals 0
    .param p1, "mediaId"    # Ljava/lang/String;
    .annotation system Ldalvik/annotation/MethodParameters;
        accessFlags = {
            0x0
        }
        names = {
            "mediaId"
        }
    .end annotation

    .line 471
    iput-object p1, p0, Landroid/support/v4/media/MediaDescriptionCompat$Builder;->mMediaId:Ljava/lang/String;

    .line 472
    return-object p0
.end method

.method public setMediaUri(Landroid/net/Uri;)Landroid/support/v4/media/MediaDescriptionCompat$Builder;
    .locals 0
    .param p1, "mediaUri"    # Landroid/net/Uri;
    .annotation system Ldalvik/annotation/MethodParameters;
        accessFlags = {
            0x0
        }
        names = {
            "mediaUri"
        }
    .end annotation

    .line 551
    iput-object p1, p0, Landroid/support/v4/media/MediaDescriptionCompat$Builder;->mMediaUri:Landroid/net/Uri;

    .line 552
    return-object p0
.end method

.method public setSubtitle(Ljava/lang/CharSequence;)Landroid/support/v4/media/MediaDescriptionCompat$Builder;
    .locals 0
    .param p1, "subtitle"    # Ljava/lang/CharSequence;
    .annotation system Ldalvik/annotation/MethodParameters;
        accessFlags = {
            0x0
        }
        names = {
            "subtitle"
        }
    .end annotation

    .line 493
    iput-object p1, p0, Landroid/support/v4/media/MediaDescriptionCompat$Builder;->mSubtitle:Ljava/lang/CharSequence;

    .line 494
    return-object p0
.end method

.method public setTitle(Ljava/lang/CharSequence;)Landroid/support/v4/media/MediaDescriptionCompat$Builder;
    .locals 0
    .param p1, "title"    # Ljava/lang/CharSequence;
    .annotation system Ldalvik/annotation/MethodParameters;
        accessFlags = {
            0x0
        }
        names = {
            "title"
        }
    .end annotation

    .line 482
    iput-object p1, p0, Landroid/support/v4/media/MediaDescriptionCompat$Builder;->mTitle:Ljava/lang/CharSequence;

    .line 483
    return-object p0
.end method
