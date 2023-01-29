.class Landroid/support/customtabs/ICustomTabsCallback$Stub$Proxy;
.super Ljava/lang/Object;
.source "ICustomTabsCallback.java"

# interfaces
.implements Landroid/support/customtabs/ICustomTabsCallback;


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Landroid/support/customtabs/ICustomTabsCallback$Stub;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0xa
    name = "Proxy"
.end annotation


# static fields
.field public static sDefaultImpl:Landroid/support/customtabs/ICustomTabsCallback;


# instance fields
.field private mRemote:Landroid/os/IBinder;


# direct methods
.method constructor <init>(Landroid/os/IBinder;)V
    .locals 0
    .param p1, "remote"    # Landroid/os/IBinder;
    .annotation system Ldalvik/annotation/MethodParameters;
        accessFlags = {
            0x0
        }
        names = {
            "remote"
        }
    .end annotation

    .line 196
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 197
    iput-object p1, p0, Landroid/support/customtabs/ICustomTabsCallback$Stub$Proxy;->mRemote:Landroid/os/IBinder;

    .line 198
    return-void
.end method


# virtual methods
.method public asBinder()Landroid/os/IBinder;
    .locals 1

    .line 201
    iget-object v0, p0, Landroid/support/customtabs/ICustomTabsCallback$Stub$Proxy;->mRemote:Landroid/os/IBinder;

    return-object v0
.end method

.method public extraCallback(Ljava/lang/String;Landroid/os/Bundle;)V
    .locals 5
    .param p1, "callbackName"    # Ljava/lang/String;
    .param p2, "args"    # Landroid/os/Bundle;
    .annotation system Ldalvik/annotation/MethodParameters;
        accessFlags = {
            0x0,
            0x0
        }
        names = {
            "callbackName",
            "args"
        }
    .end annotation

    .annotation system Ldalvik/annotation/Throws;
        value = {
            Landroid/os/RemoteException;
        }
    .end annotation

    .line 235
    invoke-static {}, Landroid/os/Parcel;->obtain()Landroid/os/Parcel;

    move-result-object v0

    .line 236
    .local v0, "_data":Landroid/os/Parcel;
    invoke-static {}, Landroid/os/Parcel;->obtain()Landroid/os/Parcel;

    move-result-object v1

    .line 238
    .local v1, "_reply":Landroid/os/Parcel;
    :try_start_0
    const-string v2, "android.support.customtabs.ICustomTabsCallback"

    invoke-virtual {v0, v2}, Landroid/os/Parcel;->writeInterfaceToken(Ljava/lang/String;)V

    .line 239
    invoke-virtual {v0, p1}, Landroid/os/Parcel;->writeString(Ljava/lang/String;)V

    .line 240
    const/4 v2, 0x0

    if-eqz p2, :cond_0

    .line 241
    const/4 v3, 0x1

    invoke-virtual {v0, v3}, Landroid/os/Parcel;->writeInt(I)V

    .line 242
    invoke-virtual {p2, v0, v2}, Landroid/os/Bundle;->writeToParcel(Landroid/os/Parcel;I)V

    goto :goto_0

    .line 245
    :cond_0
    invoke-virtual {v0, v2}, Landroid/os/Parcel;->writeInt(I)V

    .line 247
    :goto_0
    iget-object v3, p0, Landroid/support/customtabs/ICustomTabsCallback$Stub$Proxy;->mRemote:Landroid/os/IBinder;

    const/4 v4, 0x3

    invoke-interface {v3, v4, v0, v1, v2}, Landroid/os/IBinder;->transact(ILandroid/os/Parcel;Landroid/os/Parcel;I)Z

    move-result v2

    .line 248
    .local v2, "_status":Z
    if-nez v2, :cond_1

    invoke-static {}, Landroid/support/customtabs/ICustomTabsCallback$Stub;->getDefaultImpl()Landroid/support/customtabs/ICustomTabsCallback;

    move-result-object v3

    if-eqz v3, :cond_1

    .line 249
    invoke-static {}, Landroid/support/customtabs/ICustomTabsCallback$Stub;->getDefaultImpl()Landroid/support/customtabs/ICustomTabsCallback;

    move-result-object v3

    invoke-interface {v3, p1, p2}, Landroid/support/customtabs/ICustomTabsCallback;->extraCallback(Ljava/lang/String;Landroid/os/Bundle;)V
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    .line 255
    invoke-virtual {v1}, Landroid/os/Parcel;->recycle()V

    .line 256
    invoke-virtual {v0}, Landroid/os/Parcel;->recycle()V

    .line 250
    return-void

    .line 252
    :cond_1
    :try_start_1
    invoke-virtual {v1}, Landroid/os/Parcel;->readException()V
    :try_end_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    .line 255
    .end local v2    # "_status":Z
    invoke-virtual {v1}, Landroid/os/Parcel;->recycle()V

    .line 256
    invoke-virtual {v0}, Landroid/os/Parcel;->recycle()V

    .line 257
    nop

    .line 258
    return-void

    .line 255
    :catchall_0
    move-exception v2

    invoke-virtual {v1}, Landroid/os/Parcel;->recycle()V

    .line 256
    invoke-virtual {v0}, Landroid/os/Parcel;->recycle()V

    .line 257
    throw v2
.end method

.method public extraCallbackWithResult(Ljava/lang/String;Landroid/os/Bundle;)Landroid/os/Bundle;
    .locals 5
    .param p1, "callbackName"    # Ljava/lang/String;
    .param p2, "args"    # Landroid/os/Bundle;
    .annotation system Ldalvik/annotation/MethodParameters;
        accessFlags = {
            0x0,
            0x0
        }
        names = {
            "callbackName",
            "args"
        }
    .end annotation

    .annotation system Ldalvik/annotation/Throws;
        value = {
            Landroid/os/RemoteException;
        }
    .end annotation

    .line 346
    invoke-static {}, Landroid/os/Parcel;->obtain()Landroid/os/Parcel;

    move-result-object v0

    .line 347
    .local v0, "_data":Landroid/os/Parcel;
    invoke-static {}, Landroid/os/Parcel;->obtain()Landroid/os/Parcel;

    move-result-object v1

    .line 350
    .local v1, "_reply":Landroid/os/Parcel;
    :try_start_0
    const-string v2, "android.support.customtabs.ICustomTabsCallback"

    invoke-virtual {v0, v2}, Landroid/os/Parcel;->writeInterfaceToken(Ljava/lang/String;)V

    .line 351
    invoke-virtual {v0, p1}, Landroid/os/Parcel;->writeString(Ljava/lang/String;)V

    .line 352
    const/4 v2, 0x0

    if-eqz p2, :cond_0

    .line 353
    const/4 v3, 0x1

    invoke-virtual {v0, v3}, Landroid/os/Parcel;->writeInt(I)V

    .line 354
    invoke-virtual {p2, v0, v2}, Landroid/os/Bundle;->writeToParcel(Landroid/os/Parcel;I)V

    goto :goto_0

    .line 357
    :cond_0
    invoke-virtual {v0, v2}, Landroid/os/Parcel;->writeInt(I)V

    .line 359
    :goto_0
    iget-object v3, p0, Landroid/support/customtabs/ICustomTabsCallback$Stub$Proxy;->mRemote:Landroid/os/IBinder;

    const/4 v4, 0x7

    invoke-interface {v3, v4, v0, v1, v2}, Landroid/os/IBinder;->transact(ILandroid/os/Parcel;Landroid/os/Parcel;I)Z

    move-result v2

    .line 360
    .local v2, "_status":Z
    if-nez v2, :cond_1

    invoke-static {}, Landroid/support/customtabs/ICustomTabsCallback$Stub;->getDefaultImpl()Landroid/support/customtabs/ICustomTabsCallback;

    move-result-object v3

    if-eqz v3, :cond_1

    .line 361
    invoke-static {}, Landroid/support/customtabs/ICustomTabsCallback$Stub;->getDefaultImpl()Landroid/support/customtabs/ICustomTabsCallback;

    move-result-object v3

    invoke-interface {v3, p1, p2}, Landroid/support/customtabs/ICustomTabsCallback;->extraCallbackWithResult(Ljava/lang/String;Landroid/os/Bundle;)Landroid/os/Bundle;

    move-result-object v3
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    .line 372
    invoke-virtual {v1}, Landroid/os/Parcel;->recycle()V

    .line 373
    invoke-virtual {v0}, Landroid/os/Parcel;->recycle()V

    .line 361
    return-object v3

    .line 363
    :cond_1
    :try_start_1
    invoke-virtual {v1}, Landroid/os/Parcel;->readException()V

    .line 364
    invoke-virtual {v1}, Landroid/os/Parcel;->readInt()I

    move-result v3

    if-eqz v3, :cond_2

    .line 365
    sget-object v3, Landroid/os/Bundle;->CREATOR:Landroid/os/Parcelable$Creator;

    invoke-interface {v3, v1}, Landroid/os/Parcelable$Creator;->createFromParcel(Landroid/os/Parcel;)Ljava/lang/Object;

    move-result-object v3

    check-cast v3, Landroid/os/Bundle;
    :try_end_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    .local v3, "_result":Landroid/os/Bundle;
    goto :goto_1

    .line 368
    .end local v3    # "_result":Landroid/os/Bundle;
    :cond_2
    const/4 v3, 0x0

    .line 372
    .end local v2    # "_status":Z
    .restart local v3    # "_result":Landroid/os/Bundle;
    :goto_1
    invoke-virtual {v1}, Landroid/os/Parcel;->recycle()V

    .line 373
    invoke-virtual {v0}, Landroid/os/Parcel;->recycle()V

    .line 374
    nop

    .line 375
    return-object v3

    .line 372
    .end local v3    # "_result":Landroid/os/Bundle;
    :catchall_0
    move-exception v2

    invoke-virtual {v1}, Landroid/os/Parcel;->recycle()V

    .line 373
    invoke-virtual {v0}, Landroid/os/Parcel;->recycle()V

    .line 374
    throw v2
.end method

.method public getInterfaceDescriptor()Ljava/lang/String;
    .locals 1

    .line 205
    const-string v0, "android.support.customtabs.ICustomTabsCallback"

    return-object v0
.end method

.method public onMessageChannelReady(Landroid/os/Bundle;)V
    .locals 5
    .param p1, "extras"    # Landroid/os/Bundle;
    .annotation system Ldalvik/annotation/MethodParameters;
        accessFlags = {
            0x0
        }
        names = {
            "extras"
        }
    .end annotation

    .annotation system Ldalvik/annotation/Throws;
        value = {
            Landroid/os/RemoteException;
        }
    .end annotation

    .line 261
    invoke-static {}, Landroid/os/Parcel;->obtain()Landroid/os/Parcel;

    move-result-object v0

    .line 262
    .local v0, "_data":Landroid/os/Parcel;
    invoke-static {}, Landroid/os/Parcel;->obtain()Landroid/os/Parcel;

    move-result-object v1

    .line 264
    .local v1, "_reply":Landroid/os/Parcel;
    :try_start_0
    const-string v2, "android.support.customtabs.ICustomTabsCallback"

    invoke-virtual {v0, v2}, Landroid/os/Parcel;->writeInterfaceToken(Ljava/lang/String;)V

    .line 265
    const/4 v2, 0x0

    if-eqz p1, :cond_0

    .line 266
    const/4 v3, 0x1

    invoke-virtual {v0, v3}, Landroid/os/Parcel;->writeInt(I)V

    .line 267
    invoke-virtual {p1, v0, v2}, Landroid/os/Bundle;->writeToParcel(Landroid/os/Parcel;I)V

    goto :goto_0

    .line 270
    :cond_0
    invoke-virtual {v0, v2}, Landroid/os/Parcel;->writeInt(I)V

    .line 272
    :goto_0
    iget-object v3, p0, Landroid/support/customtabs/ICustomTabsCallback$Stub$Proxy;->mRemote:Landroid/os/IBinder;

    const/4 v4, 0x4

    invoke-interface {v3, v4, v0, v1, v2}, Landroid/os/IBinder;->transact(ILandroid/os/Parcel;Landroid/os/Parcel;I)Z

    move-result v2

    .line 273
    .local v2, "_status":Z
    if-nez v2, :cond_1

    invoke-static {}, Landroid/support/customtabs/ICustomTabsCallback$Stub;->getDefaultImpl()Landroid/support/customtabs/ICustomTabsCallback;

    move-result-object v3

    if-eqz v3, :cond_1

    .line 274
    invoke-static {}, Landroid/support/customtabs/ICustomTabsCallback$Stub;->getDefaultImpl()Landroid/support/customtabs/ICustomTabsCallback;

    move-result-object v3

    invoke-interface {v3, p1}, Landroid/support/customtabs/ICustomTabsCallback;->onMessageChannelReady(Landroid/os/Bundle;)V
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    .line 280
    invoke-virtual {v1}, Landroid/os/Parcel;->recycle()V

    .line 281
    invoke-virtual {v0}, Landroid/os/Parcel;->recycle()V

    .line 275
    return-void

    .line 277
    :cond_1
    :try_start_1
    invoke-virtual {v1}, Landroid/os/Parcel;->readException()V
    :try_end_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    .line 280
    .end local v2    # "_status":Z
    invoke-virtual {v1}, Landroid/os/Parcel;->recycle()V

    .line 281
    invoke-virtual {v0}, Landroid/os/Parcel;->recycle()V

    .line 282
    nop

    .line 283
    return-void

    .line 280
    :catchall_0
    move-exception v2

    invoke-virtual {v1}, Landroid/os/Parcel;->recycle()V

    .line 281
    invoke-virtual {v0}, Landroid/os/Parcel;->recycle()V

    .line 282
    throw v2
.end method

.method public onNavigationEvent(ILandroid/os/Bundle;)V
    .locals 5
    .param p1, "navigationEvent"    # I
    .param p2, "extras"    # Landroid/os/Bundle;
    .annotation system Ldalvik/annotation/MethodParameters;
        accessFlags = {
            0x0,
            0x0
        }
        names = {
            "navigationEvent",
            "extras"
        }
    .end annotation

    .annotation system Ldalvik/annotation/Throws;
        value = {
            Landroid/os/RemoteException;
        }
    .end annotation

    .line 209
    invoke-static {}, Landroid/os/Parcel;->obtain()Landroid/os/Parcel;

    move-result-object v0

    .line 210
    .local v0, "_data":Landroid/os/Parcel;
    invoke-static {}, Landroid/os/Parcel;->obtain()Landroid/os/Parcel;

    move-result-object v1

    .line 212
    .local v1, "_reply":Landroid/os/Parcel;
    :try_start_0
    const-string v2, "android.support.customtabs.ICustomTabsCallback"

    invoke-virtual {v0, v2}, Landroid/os/Parcel;->writeInterfaceToken(Ljava/lang/String;)V

    .line 213
    invoke-virtual {v0, p1}, Landroid/os/Parcel;->writeInt(I)V

    .line 214
    const/4 v2, 0x0

    if-eqz p2, :cond_0

    .line 215
    const/4 v3, 0x1

    invoke-virtual {v0, v3}, Landroid/os/Parcel;->writeInt(I)V

    .line 216
    invoke-virtual {p2, v0, v2}, Landroid/os/Bundle;->writeToParcel(Landroid/os/Parcel;I)V

    goto :goto_0

    .line 219
    :cond_0
    invoke-virtual {v0, v2}, Landroid/os/Parcel;->writeInt(I)V

    .line 221
    :goto_0
    iget-object v3, p0, Landroid/support/customtabs/ICustomTabsCallback$Stub$Proxy;->mRemote:Landroid/os/IBinder;

    const/4 v4, 0x2

    invoke-interface {v3, v4, v0, v1, v2}, Landroid/os/IBinder;->transact(ILandroid/os/Parcel;Landroid/os/Parcel;I)Z

    move-result v2

    .line 222
    .local v2, "_status":Z
    if-nez v2, :cond_1

    invoke-static {}, Landroid/support/customtabs/ICustomTabsCallback$Stub;->getDefaultImpl()Landroid/support/customtabs/ICustomTabsCallback;

    move-result-object v3

    if-eqz v3, :cond_1

    .line 223
    invoke-static {}, Landroid/support/customtabs/ICustomTabsCallback$Stub;->getDefaultImpl()Landroid/support/customtabs/ICustomTabsCallback;

    move-result-object v3

    invoke-interface {v3, p1, p2}, Landroid/support/customtabs/ICustomTabsCallback;->onNavigationEvent(ILandroid/os/Bundle;)V
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    .line 229
    invoke-virtual {v1}, Landroid/os/Parcel;->recycle()V

    .line 230
    invoke-virtual {v0}, Landroid/os/Parcel;->recycle()V

    .line 224
    return-void

    .line 226
    :cond_1
    :try_start_1
    invoke-virtual {v1}, Landroid/os/Parcel;->readException()V
    :try_end_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    .line 229
    .end local v2    # "_status":Z
    invoke-virtual {v1}, Landroid/os/Parcel;->recycle()V

    .line 230
    invoke-virtual {v0}, Landroid/os/Parcel;->recycle()V

    .line 231
    nop

    .line 232
    return-void

    .line 229
    :catchall_0
    move-exception v2

    invoke-virtual {v1}, Landroid/os/Parcel;->recycle()V

    .line 230
    invoke-virtual {v0}, Landroid/os/Parcel;->recycle()V

    .line 231
    throw v2
.end method

.method public onPostMessage(Ljava/lang/String;Landroid/os/Bundle;)V
    .locals 5
    .param p1, "message"    # Ljava/lang/String;
    .param p2, "extras"    # Landroid/os/Bundle;
    .annotation system Ldalvik/annotation/MethodParameters;
        accessFlags = {
            0x0,
            0x0
        }
        names = {
            "message",
            "extras"
        }
    .end annotation

    .annotation system Ldalvik/annotation/Throws;
        value = {
            Landroid/os/RemoteException;
        }
    .end annotation

    .line 286
    invoke-static {}, Landroid/os/Parcel;->obtain()Landroid/os/Parcel;

    move-result-object v0

    .line 287
    .local v0, "_data":Landroid/os/Parcel;
    invoke-static {}, Landroid/os/Parcel;->obtain()Landroid/os/Parcel;

    move-result-object v1

    .line 289
    .local v1, "_reply":Landroid/os/Parcel;
    :try_start_0
    const-string v2, "android.support.customtabs.ICustomTabsCallback"

    invoke-virtual {v0, v2}, Landroid/os/Parcel;->writeInterfaceToken(Ljava/lang/String;)V

    .line 290
    invoke-virtual {v0, p1}, Landroid/os/Parcel;->writeString(Ljava/lang/String;)V

    .line 291
    const/4 v2, 0x0

    if-eqz p2, :cond_0

    .line 292
    const/4 v3, 0x1

    invoke-virtual {v0, v3}, Landroid/os/Parcel;->writeInt(I)V

    .line 293
    invoke-virtual {p2, v0, v2}, Landroid/os/Bundle;->writeToParcel(Landroid/os/Parcel;I)V

    goto :goto_0

    .line 296
    :cond_0
    invoke-virtual {v0, v2}, Landroid/os/Parcel;->writeInt(I)V

    .line 298
    :goto_0
    iget-object v3, p0, Landroid/support/customtabs/ICustomTabsCallback$Stub$Proxy;->mRemote:Landroid/os/IBinder;

    const/4 v4, 0x5

    invoke-interface {v3, v4, v0, v1, v2}, Landroid/os/IBinder;->transact(ILandroid/os/Parcel;Landroid/os/Parcel;I)Z

    move-result v2

    .line 299
    .local v2, "_status":Z
    if-nez v2, :cond_1

    invoke-static {}, Landroid/support/customtabs/ICustomTabsCallback$Stub;->getDefaultImpl()Landroid/support/customtabs/ICustomTabsCallback;

    move-result-object v3

    if-eqz v3, :cond_1

    .line 300
    invoke-static {}, Landroid/support/customtabs/ICustomTabsCallback$Stub;->getDefaultImpl()Landroid/support/customtabs/ICustomTabsCallback;

    move-result-object v3

    invoke-interface {v3, p1, p2}, Landroid/support/customtabs/ICustomTabsCallback;->onPostMessage(Ljava/lang/String;Landroid/os/Bundle;)V
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    .line 306
    invoke-virtual {v1}, Landroid/os/Parcel;->recycle()V

    .line 307
    invoke-virtual {v0}, Landroid/os/Parcel;->recycle()V

    .line 301
    return-void

    .line 303
    :cond_1
    :try_start_1
    invoke-virtual {v1}, Landroid/os/Parcel;->readException()V
    :try_end_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    .line 306
    .end local v2    # "_status":Z
    invoke-virtual {v1}, Landroid/os/Parcel;->recycle()V

    .line 307
    invoke-virtual {v0}, Landroid/os/Parcel;->recycle()V

    .line 308
    nop

    .line 309
    return-void

    .line 306
    :catchall_0
    move-exception v2

    invoke-virtual {v1}, Landroid/os/Parcel;->recycle()V

    .line 307
    invoke-virtual {v0}, Landroid/os/Parcel;->recycle()V

    .line 308
    throw v2
.end method

.method public onRelationshipValidationResult(ILandroid/net/Uri;ZLandroid/os/Bundle;)V
    .locals 5
    .param p1, "relation"    # I
    .param p2, "origin"    # Landroid/net/Uri;
    .param p3, "result"    # Z
    .param p4, "extras"    # Landroid/os/Bundle;
    .annotation system Ldalvik/annotation/MethodParameters;
        accessFlags = {
            0x0,
            0x0,
            0x0,
            0x0
        }
        names = {
            "relation",
            "origin",
            "result",
            "extras"
        }
    .end annotation

    .annotation system Ldalvik/annotation/Throws;
        value = {
            Landroid/os/RemoteException;
        }
    .end annotation

    .line 312
    invoke-static {}, Landroid/os/Parcel;->obtain()Landroid/os/Parcel;

    move-result-object v0

    .line 313
    .local v0, "_data":Landroid/os/Parcel;
    invoke-static {}, Landroid/os/Parcel;->obtain()Landroid/os/Parcel;

    move-result-object v1

    .line 315
    .local v1, "_reply":Landroid/os/Parcel;
    :try_start_0
    const-string v2, "android.support.customtabs.ICustomTabsCallback"

    invoke-virtual {v0, v2}, Landroid/os/Parcel;->writeInterfaceToken(Ljava/lang/String;)V

    .line 316
    invoke-virtual {v0, p1}, Landroid/os/Parcel;->writeInt(I)V

    .line 317
    const/4 v2, 0x1

    const/4 v3, 0x0

    if-eqz p2, :cond_0

    .line 318
    invoke-virtual {v0, v2}, Landroid/os/Parcel;->writeInt(I)V

    .line 319
    invoke-virtual {p2, v0, v3}, Landroid/net/Uri;->writeToParcel(Landroid/os/Parcel;I)V

    goto :goto_0

    .line 322
    :cond_0
    invoke-virtual {v0, v3}, Landroid/os/Parcel;->writeInt(I)V

    .line 324
    :goto_0
    if-eqz p3, :cond_1

    const/4 v4, 0x1

    goto :goto_1

    :cond_1
    const/4 v4, 0x0

    :goto_1
    invoke-virtual {v0, v4}, Landroid/os/Parcel;->writeInt(I)V

    .line 325
    if-eqz p4, :cond_2

    .line 326
    invoke-virtual {v0, v2}, Landroid/os/Parcel;->writeInt(I)V

    .line 327
    invoke-virtual {p4, v0, v3}, Landroid/os/Bundle;->writeToParcel(Landroid/os/Parcel;I)V

    goto :goto_2

    .line 330
    :cond_2
    invoke-virtual {v0, v3}, Landroid/os/Parcel;->writeInt(I)V

    .line 332
    :goto_2
    iget-object v2, p0, Landroid/support/customtabs/ICustomTabsCallback$Stub$Proxy;->mRemote:Landroid/os/IBinder;

    const/4 v4, 0x6

    invoke-interface {v2, v4, v0, v1, v3}, Landroid/os/IBinder;->transact(ILandroid/os/Parcel;Landroid/os/Parcel;I)Z

    move-result v2

    .line 333
    .local v2, "_status":Z
    if-nez v2, :cond_3

    invoke-static {}, Landroid/support/customtabs/ICustomTabsCallback$Stub;->getDefaultImpl()Landroid/support/customtabs/ICustomTabsCallback;

    move-result-object v3

    if-eqz v3, :cond_3

    .line 334
    invoke-static {}, Landroid/support/customtabs/ICustomTabsCallback$Stub;->getDefaultImpl()Landroid/support/customtabs/ICustomTabsCallback;

    move-result-object v3

    invoke-interface {v3, p1, p2, p3, p4}, Landroid/support/customtabs/ICustomTabsCallback;->onRelationshipValidationResult(ILandroid/net/Uri;ZLandroid/os/Bundle;)V
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    .line 340
    invoke-virtual {v1}, Landroid/os/Parcel;->recycle()V

    .line 341
    invoke-virtual {v0}, Landroid/os/Parcel;->recycle()V

    .line 335
    return-void

    .line 337
    :cond_3
    :try_start_1
    invoke-virtual {v1}, Landroid/os/Parcel;->readException()V
    :try_end_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    .line 340
    .end local v2    # "_status":Z
    invoke-virtual {v1}, Landroid/os/Parcel;->recycle()V

    .line 341
    invoke-virtual {v0}, Landroid/os/Parcel;->recycle()V

    .line 342
    nop

    .line 343
    return-void

    .line 340
    :catchall_0
    move-exception v2

    invoke-virtual {v1}, Landroid/os/Parcel;->recycle()V

    .line 341
    invoke-virtual {v0}, Landroid/os/Parcel;->recycle()V

    .line 342
    throw v2
.end method
