.class Landroidx/media/MediaBrowserServiceCompat$ResultWrapper;
.super Ljava/lang/Object;
.source "MediaBrowserServiceCompat.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Landroidx/media/MediaBrowserServiceCompat;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x8
    name = "ResultWrapper"
.end annotation

.annotation system Ldalvik/annotation/Signature;
    value = {
        "<T:",
        "Ljava/lang/Object;",
        ">",
        "Ljava/lang/Object;"
    }
.end annotation


# instance fields
.field mResultFwk:Landroid/service/media/MediaBrowserService$Result;


# direct methods
.method constructor <init>(Landroid/service/media/MediaBrowserService$Result;)V
    .locals 0
    .param p1, "result"    # Landroid/service/media/MediaBrowserService$Result;
    .annotation system Ldalvik/annotation/MethodParameters;
        accessFlags = {
            0x0
        }
        names = {
            "result"
        }
    .end annotation

    .line 1264
    .local p0, "this":Landroidx/media/MediaBrowserServiceCompat$ResultWrapper;, "Landroidx/media/MediaBrowserServiceCompat$ResultWrapper<TT;>;"
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 1265
    iput-object p1, p0, Landroidx/media/MediaBrowserServiceCompat$ResultWrapper;->mResultFwk:Landroid/service/media/MediaBrowserService$Result;

    .line 1266
    return-void
.end method


# virtual methods
.method public detach()V
    .locals 1

    .line 1283
    .local p0, "this":Landroidx/media/MediaBrowserServiceCompat$ResultWrapper;, "Landroidx/media/MediaBrowserServiceCompat$ResultWrapper<TT;>;"
    iget-object v0, p0, Landroidx/media/MediaBrowserServiceCompat$ResultWrapper;->mResultFwk:Landroid/service/media/MediaBrowserService$Result;

    invoke-virtual {v0}, Landroid/service/media/MediaBrowserService$Result;->detach()V

    .line 1284
    return-void
.end method

.method parcelListToItemList(Ljava/util/List;)Ljava/util/List;
    .locals 4
    .annotation system Ldalvik/annotation/MethodParameters;
        accessFlags = {
            0x0
        }
        names = {
            "parcelList"
        }
    .end annotation

    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/List<",
            "Landroid/os/Parcel;",
            ">;)",
            "Ljava/util/List<",
            "Landroid/media/browse/MediaBrowser$MediaItem;",
            ">;"
        }
    .end annotation

    .line 1287
    .local p0, "this":Landroidx/media/MediaBrowserServiceCompat$ResultWrapper;, "Landroidx/media/MediaBrowserServiceCompat$ResultWrapper<TT;>;"
    .local p1, "parcelList":Ljava/util/List;, "Ljava/util/List<Landroid/os/Parcel;>;"
    if-nez p1, :cond_0

    .line 1288
    const/4 v0, 0x0

    return-object v0

    .line 1290
    :cond_0
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    .line 1291
    .local v0, "items":Ljava/util/List;, "Ljava/util/List<Landroid/media/browse/MediaBrowser$MediaItem;>;"
    invoke-interface {p1}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v1

    :goto_0
    invoke-interface {v1}, Ljava/util/Iterator;->hasNext()Z

    move-result v2

    if-eqz v2, :cond_1

    invoke-interface {v1}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Landroid/os/Parcel;

    .line 1292
    .local v2, "parcel":Landroid/os/Parcel;
    const/4 v3, 0x0

    invoke-virtual {v2, v3}, Landroid/os/Parcel;->setDataPosition(I)V

    .line 1293
    sget-object v3, Landroid/media/browse/MediaBrowser$MediaItem;->CREATOR:Landroid/os/Parcelable$Creator;

    invoke-interface {v3, v2}, Landroid/os/Parcelable$Creator;->createFromParcel(Landroid/os/Parcel;)Ljava/lang/Object;

    move-result-object v3

    check-cast v3, Landroid/media/browse/MediaBrowser$MediaItem;

    invoke-interface {v0, v3}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    .line 1294
    invoke-virtual {v2}, Landroid/os/Parcel;->recycle()V

    .line 1295
    .end local v2    # "parcel":Landroid/os/Parcel;
    goto :goto_0

    .line 1296
    :cond_1
    return-object v0
.end method

.method public sendResult(Ljava/lang/Object;)V
    .locals 3
    .annotation system Ldalvik/annotation/MethodParameters;
        accessFlags = {
            0x0
        }
        names = {
            "result"
        }
    .end annotation

    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(TT;)V"
        }
    .end annotation

    .line 1269
    .local p0, "this":Landroidx/media/MediaBrowserServiceCompat$ResultWrapper;, "Landroidx/media/MediaBrowserServiceCompat$ResultWrapper<TT;>;"
    .local p1, "result":Ljava/lang/Object;, "TT;"
    instance-of v0, p1, Ljava/util/List;

    if-eqz v0, :cond_0

    .line 1270
    iget-object v0, p0, Landroidx/media/MediaBrowserServiceCompat$ResultWrapper;->mResultFwk:Landroid/service/media/MediaBrowserService$Result;

    move-object v1, p1

    check-cast v1, Ljava/util/List;

    invoke-virtual {p0, v1}, Landroidx/media/MediaBrowserServiceCompat$ResultWrapper;->parcelListToItemList(Ljava/util/List;)Ljava/util/List;

    move-result-object v1

    invoke-virtual {v0, v1}, Landroid/service/media/MediaBrowserService$Result;->sendResult(Ljava/lang/Object;)V

    goto :goto_0

    .line 1271
    :cond_0
    instance-of v0, p1, Landroid/os/Parcel;

    if-eqz v0, :cond_1

    .line 1272
    move-object v0, p1

    check-cast v0, Landroid/os/Parcel;

    .line 1273
    .local v0, "parcel":Landroid/os/Parcel;
    const/4 v1, 0x0

    invoke-virtual {v0, v1}, Landroid/os/Parcel;->setDataPosition(I)V

    .line 1274
    iget-object v1, p0, Landroidx/media/MediaBrowserServiceCompat$ResultWrapper;->mResultFwk:Landroid/service/media/MediaBrowserService$Result;

    sget-object v2, Landroid/media/browse/MediaBrowser$MediaItem;->CREATOR:Landroid/os/Parcelable$Creator;

    invoke-interface {v2, v0}, Landroid/os/Parcelable$Creator;->createFromParcel(Landroid/os/Parcel;)Ljava/lang/Object;

    move-result-object v2

    invoke-virtual {v1, v2}, Landroid/service/media/MediaBrowserService$Result;->sendResult(Ljava/lang/Object;)V

    .line 1275
    invoke-virtual {v0}, Landroid/os/Parcel;->recycle()V

    .line 1276
    .end local v0    # "parcel":Landroid/os/Parcel;
    goto :goto_0

    .line 1278
    :cond_1
    iget-object v0, p0, Landroidx/media/MediaBrowserServiceCompat$ResultWrapper;->mResultFwk:Landroid/service/media/MediaBrowserService$Result;

    const/4 v1, 0x0

    invoke-virtual {v0, v1}, Landroid/service/media/MediaBrowserService$Result;->sendResult(Ljava/lang/Object;)V

    .line 1280
    :goto_0
    return-void
.end method
