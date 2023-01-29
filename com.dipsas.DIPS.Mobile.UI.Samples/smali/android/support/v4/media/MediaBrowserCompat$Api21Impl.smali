.class Landroid/support/v4/media/MediaBrowserCompat$Api21Impl;
.super Ljava/lang/Object;
.source "MediaBrowserCompat.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Landroid/support/v4/media/MediaBrowserCompat;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0xa
    name = "Api21Impl"
.end annotation


# direct methods
.method private constructor <init>()V
    .locals 0

    .line 2372
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method

.method static getDescription(Landroid/media/browse/MediaBrowser$MediaItem;)Landroid/media/MediaDescription;
    .locals 1
    .param p0, "item"    # Landroid/media/browse/MediaBrowser$MediaItem;
    .annotation system Ldalvik/annotation/MethodParameters;
        accessFlags = {
            0x0
        }
        names = {
            "item"
        }
    .end annotation

    .line 2376
    invoke-virtual {p0}, Landroid/media/browse/MediaBrowser$MediaItem;->getDescription()Landroid/media/MediaDescription;

    move-result-object v0

    return-object v0
.end method

.method static getFlags(Landroid/media/browse/MediaBrowser$MediaItem;)I
    .locals 1
    .param p0, "item"    # Landroid/media/browse/MediaBrowser$MediaItem;
    .annotation system Ldalvik/annotation/MethodParameters;
        accessFlags = {
            0x0
        }
        names = {
            "item"
        }
    .end annotation

    .line 2381
    invoke-virtual {p0}, Landroid/media/browse/MediaBrowser$MediaItem;->getFlags()I

    move-result v0

    return v0
.end method
