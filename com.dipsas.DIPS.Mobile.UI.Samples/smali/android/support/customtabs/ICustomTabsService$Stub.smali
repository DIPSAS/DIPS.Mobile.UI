.class public abstract Landroid/support/customtabs/ICustomTabsService$Stub;
.super Landroid/os/Binder;
.source "ICustomTabsService.java"

# interfaces
.implements Landroid/support/customtabs/ICustomTabsService;


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Landroid/support/customtabs/ICustomTabsService;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x409
    name = "Stub"
.end annotation

.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Landroid/support/customtabs/ICustomTabsService$Stub$Proxy;
    }
.end annotation


# static fields
.field private static final DESCRIPTOR:Ljava/lang/String; = "android.support.customtabs.ICustomTabsService"

.field static final TRANSACTION_extraCommand:I = 0x5

.field static final TRANSACTION_mayLaunchUrl:I = 0x4

.field static final TRANSACTION_newSession:I = 0x3

.field static final TRANSACTION_newSessionWithExtras:I = 0xa

.field static final TRANSACTION_postMessage:I = 0x8

.field static final TRANSACTION_receiveFile:I = 0xc

.field static final TRANSACTION_requestPostMessageChannel:I = 0x7

.field static final TRANSACTION_requestPostMessageChannelWithExtras:I = 0xb

.field static final TRANSACTION_updateVisuals:I = 0x6

.field static final TRANSACTION_validateRelationship:I = 0x9

.field static final TRANSACTION_warmup:I = 0x2


# direct methods
.method public constructor <init>()V
    .locals 1

    .line 69
    invoke-direct {p0}, Landroid/os/Binder;-><init>()V

    .line 70
    const-string v0, "android.support.customtabs.ICustomTabsService"

    invoke-virtual {p0, p0, v0}, Landroid/support/customtabs/ICustomTabsService$Stub;->attachInterface(Landroid/os/IInterface;Ljava/lang/String;)V

    .line 71
    return-void
.end method

.method public static asInterface(Landroid/os/IBinder;)Landroid/support/customtabs/ICustomTabsService;
    .locals 2
    .param p0, "obj"    # Landroid/os/IBinder;
    .annotation system Ldalvik/annotation/MethodParameters;
        accessFlags = {
            0x0
        }
        names = {
            "obj"
        }
    .end annotation

    .line 78
    if-nez p0, :cond_0

    .line 79
    const/4 v0, 0x0

    return-object v0

    .line 81
    :cond_0
    const-string v0, "android.support.customtabs.ICustomTabsService"

    invoke-interface {p0, v0}, Landroid/os/IBinder;->queryLocalInterface(Ljava/lang/String;)Landroid/os/IInterface;

    move-result-object v0

    .line 82
    .local v0, "iin":Landroid/os/IInterface;
    if-eqz v0, :cond_1

    instance-of v1, v0, Landroid/support/customtabs/ICustomTabsService;

    if-eqz v1, :cond_1

    .line 83
    move-object v1, v0

    check-cast v1, Landroid/support/customtabs/ICustomTabsService;

    return-object v1

    .line 85
    :cond_1
    new-instance v1, Landroid/support/customtabs/ICustomTabsService$Stub$Proxy;

    invoke-direct {v1, p0}, Landroid/support/customtabs/ICustomTabsService$Stub$Proxy;-><init>(Landroid/os/IBinder;)V

    return-object v1
.end method

.method public static getDefaultImpl()Landroid/support/customtabs/ICustomTabsService;
    .locals 1

    .line 695
    sget-object v0, Landroid/support/customtabs/ICustomTabsService$Stub$Proxy;->sDefaultImpl:Landroid/support/customtabs/ICustomTabsService;

    return-object v0
.end method

.method public static setDefaultImpl(Landroid/support/customtabs/ICustomTabsService;)Z
    .locals 2
    .param p0, "impl"    # Landroid/support/customtabs/ICustomTabsService;
    .annotation system Ldalvik/annotation/MethodParameters;
        accessFlags = {
            0x0
        }
        names = {
            "impl"
        }
    .end annotation

    .line 685
    sget-object v0, Landroid/support/customtabs/ICustomTabsService$Stub$Proxy;->sDefaultImpl:Landroid/support/customtabs/ICustomTabsService;

    if-nez v0, :cond_1

    .line 688
    if-eqz p0, :cond_0

    .line 689
    sput-object p0, Landroid/support/customtabs/ICustomTabsService$Stub$Proxy;->sDefaultImpl:Landroid/support/customtabs/ICustomTabsService;

    .line 690
    const/4 v0, 0x1

    return v0

    .line 692
    :cond_0
    const/4 v0, 0x0

    return v0

    .line 686
    :cond_1
    new-instance v0, Ljava/lang/IllegalStateException;

    const-string v1, "setDefaultImpl() called twice"

    invoke-direct {v0, v1}, Ljava/lang/IllegalStateException;-><init>(Ljava/lang/String;)V

    throw v0
.end method


# virtual methods
.method public asBinder()Landroid/os/IBinder;
    .locals 0

    .line 89
    return-object p0
.end method

.method public onTransact(ILandroid/os/Parcel;Landroid/os/Parcel;I)Z
    .locals 7
    .param p1, "code"    # I
    .param p2, "data"    # Landroid/os/Parcel;
    .param p3, "reply"    # Landroid/os/Parcel;
    .param p4, "flags"    # I
    .annotation system Ldalvik/annotation/MethodParameters;
        accessFlags = {
            0x0,
            0x0,
            0x0,
            0x0
        }
        names = {
            "code",
            "data",
            "reply",
            "flags"
        }
    .end annotation

    .annotation system Ldalvik/annotation/Throws;
        value = {
            Landroid/os/RemoteException;
        }
    .end annotation

    .line 93
    const-string v0, "android.support.customtabs.ICustomTabsService"

    .line 94
    .local v0, "descriptor":Ljava/lang/String;
    const/4 v1, 0x1

    sparse-switch p1, :sswitch_data_0

    .line 318
    invoke-super {p0, p1, p2, p3, p4}, Landroid/os/Binder;->onTransact(ILandroid/os/Parcel;Landroid/os/Parcel;I)Z

    move-result v1

    return v1

    .line 98
    :sswitch_0
    invoke-virtual {p3, v0}, Landroid/os/Parcel;->writeString(Ljava/lang/String;)V

    .line 99
    return v1

    .line 292
    :sswitch_1
    invoke-virtual {p2, v0}, Landroid/os/Parcel;->enforceInterface(Ljava/lang/String;)V

    .line 294
    invoke-virtual {p2}, Landroid/os/Parcel;->readStrongBinder()Landroid/os/IBinder;

    move-result-object v2

    invoke-static {v2}, Landroid/support/customtabs/ICustomTabsCallback$Stub;->asInterface(Landroid/os/IBinder;)Landroid/support/customtabs/ICustomTabsCallback;

    move-result-object v2

    .line 296
    .local v2, "_arg0":Landroid/support/customtabs/ICustomTabsCallback;
    invoke-virtual {p2}, Landroid/os/Parcel;->readInt()I

    move-result v3

    if-eqz v3, :cond_0

    .line 297
    sget-object v3, Landroid/net/Uri;->CREATOR:Landroid/os/Parcelable$Creator;

    invoke-interface {v3, p2}, Landroid/os/Parcelable$Creator;->createFromParcel(Landroid/os/Parcel;)Ljava/lang/Object;

    move-result-object v3

    check-cast v3, Landroid/net/Uri;

    .local v3, "_arg1":Landroid/net/Uri;
    goto :goto_0

    .line 300
    .end local v3    # "_arg1":Landroid/net/Uri;
    :cond_0
    const/4 v3, 0x0

    .line 303
    .restart local v3    # "_arg1":Landroid/net/Uri;
    :goto_0
    invoke-virtual {p2}, Landroid/os/Parcel;->readInt()I

    move-result v4

    .line 305
    .local v4, "_arg2":I
    invoke-virtual {p2}, Landroid/os/Parcel;->readInt()I

    move-result v5

    if-eqz v5, :cond_1

    .line 306
    sget-object v5, Landroid/os/Bundle;->CREATOR:Landroid/os/Parcelable$Creator;

    invoke-interface {v5, p2}, Landroid/os/Parcelable$Creator;->createFromParcel(Landroid/os/Parcel;)Ljava/lang/Object;

    move-result-object v5

    check-cast v5, Landroid/os/Bundle;

    .local v5, "_arg3":Landroid/os/Bundle;
    goto :goto_1

    .line 309
    .end local v5    # "_arg3":Landroid/os/Bundle;
    :cond_1
    const/4 v5, 0x0

    .line 311
    .restart local v5    # "_arg3":Landroid/os/Bundle;
    :goto_1
    invoke-virtual {p0, v2, v3, v4, v5}, Landroid/support/customtabs/ICustomTabsService$Stub;->receiveFile(Landroid/support/customtabs/ICustomTabsCallback;Landroid/net/Uri;ILandroid/os/Bundle;)Z

    move-result v6

    .line 312
    .local v6, "_result":Z
    invoke-virtual {p3}, Landroid/os/Parcel;->writeNoException()V

    .line 313
    invoke-virtual {p3, v6}, Landroid/os/Parcel;->writeInt(I)V

    .line 314
    return v1

    .line 223
    .end local v2    # "_arg0":Landroid/support/customtabs/ICustomTabsCallback;
    .end local v3    # "_arg1":Landroid/net/Uri;
    .end local v4    # "_arg2":I
    .end local v5    # "_arg3":Landroid/os/Bundle;
    .end local v6    # "_result":Z
    :sswitch_2
    invoke-virtual {p2, v0}, Landroid/os/Parcel;->enforceInterface(Ljava/lang/String;)V

    .line 225
    invoke-virtual {p2}, Landroid/os/Parcel;->readStrongBinder()Landroid/os/IBinder;

    move-result-object v2

    invoke-static {v2}, Landroid/support/customtabs/ICustomTabsCallback$Stub;->asInterface(Landroid/os/IBinder;)Landroid/support/customtabs/ICustomTabsCallback;

    move-result-object v2

    .line 227
    .restart local v2    # "_arg0":Landroid/support/customtabs/ICustomTabsCallback;
    invoke-virtual {p2}, Landroid/os/Parcel;->readInt()I

    move-result v3

    if-eqz v3, :cond_2

    .line 228
    sget-object v3, Landroid/net/Uri;->CREATOR:Landroid/os/Parcelable$Creator;

    invoke-interface {v3, p2}, Landroid/os/Parcelable$Creator;->createFromParcel(Landroid/os/Parcel;)Ljava/lang/Object;

    move-result-object v3

    check-cast v3, Landroid/net/Uri;

    .restart local v3    # "_arg1":Landroid/net/Uri;
    goto :goto_2

    .line 231
    .end local v3    # "_arg1":Landroid/net/Uri;
    :cond_2
    const/4 v3, 0x0

    .line 234
    .restart local v3    # "_arg1":Landroid/net/Uri;
    :goto_2
    invoke-virtual {p2}, Landroid/os/Parcel;->readInt()I

    move-result v4

    if-eqz v4, :cond_3

    .line 235
    sget-object v4, Landroid/os/Bundle;->CREATOR:Landroid/os/Parcelable$Creator;

    invoke-interface {v4, p2}, Landroid/os/Parcelable$Creator;->createFromParcel(Landroid/os/Parcel;)Ljava/lang/Object;

    move-result-object v4

    check-cast v4, Landroid/os/Bundle;

    .local v4, "_arg2":Landroid/os/Bundle;
    goto :goto_3

    .line 238
    .end local v4    # "_arg2":Landroid/os/Bundle;
    :cond_3
    const/4 v4, 0x0

    .line 240
    .restart local v4    # "_arg2":Landroid/os/Bundle;
    :goto_3
    invoke-virtual {p0, v2, v3, v4}, Landroid/support/customtabs/ICustomTabsService$Stub;->requestPostMessageChannelWithExtras(Landroid/support/customtabs/ICustomTabsCallback;Landroid/net/Uri;Landroid/os/Bundle;)Z

    move-result v5

    .line 241
    .local v5, "_result":Z
    invoke-virtual {p3}, Landroid/os/Parcel;->writeNoException()V

    .line 242
    invoke-virtual {p3, v5}, Landroid/os/Parcel;->writeInt(I)V

    .line 243
    return v1

    .line 123
    .end local v2    # "_arg0":Landroid/support/customtabs/ICustomTabsCallback;
    .end local v3    # "_arg1":Landroid/net/Uri;
    .end local v4    # "_arg2":Landroid/os/Bundle;
    .end local v5    # "_result":Z
    :sswitch_3
    invoke-virtual {p2, v0}, Landroid/os/Parcel;->enforceInterface(Ljava/lang/String;)V

    .line 125
    invoke-virtual {p2}, Landroid/os/Parcel;->readStrongBinder()Landroid/os/IBinder;

    move-result-object v2

    invoke-static {v2}, Landroid/support/customtabs/ICustomTabsCallback$Stub;->asInterface(Landroid/os/IBinder;)Landroid/support/customtabs/ICustomTabsCallback;

    move-result-object v2

    .line 127
    .restart local v2    # "_arg0":Landroid/support/customtabs/ICustomTabsCallback;
    invoke-virtual {p2}, Landroid/os/Parcel;->readInt()I

    move-result v3

    if-eqz v3, :cond_4

    .line 128
    sget-object v3, Landroid/os/Bundle;->CREATOR:Landroid/os/Parcelable$Creator;

    invoke-interface {v3, p2}, Landroid/os/Parcelable$Creator;->createFromParcel(Landroid/os/Parcel;)Ljava/lang/Object;

    move-result-object v3

    check-cast v3, Landroid/os/Bundle;

    .local v3, "_arg1":Landroid/os/Bundle;
    goto :goto_4

    .line 131
    .end local v3    # "_arg1":Landroid/os/Bundle;
    :cond_4
    const/4 v3, 0x0

    .line 133
    .restart local v3    # "_arg1":Landroid/os/Bundle;
    :goto_4
    invoke-virtual {p0, v2, v3}, Landroid/support/customtabs/ICustomTabsService$Stub;->newSessionWithExtras(Landroid/support/customtabs/ICustomTabsCallback;Landroid/os/Bundle;)Z

    move-result v4

    .line 134
    .local v4, "_result":Z
    invoke-virtual {p3}, Landroid/os/Parcel;->writeNoException()V

    .line 135
    invoke-virtual {p3, v4}, Landroid/os/Parcel;->writeInt(I)V

    .line 136
    return v1

    .line 266
    .end local v2    # "_arg0":Landroid/support/customtabs/ICustomTabsCallback;
    .end local v3    # "_arg1":Landroid/os/Bundle;
    .end local v4    # "_result":Z
    :sswitch_4
    invoke-virtual {p2, v0}, Landroid/os/Parcel;->enforceInterface(Ljava/lang/String;)V

    .line 268
    invoke-virtual {p2}, Landroid/os/Parcel;->readStrongBinder()Landroid/os/IBinder;

    move-result-object v2

    invoke-static {v2}, Landroid/support/customtabs/ICustomTabsCallback$Stub;->asInterface(Landroid/os/IBinder;)Landroid/support/customtabs/ICustomTabsCallback;

    move-result-object v2

    .line 270
    .restart local v2    # "_arg0":Landroid/support/customtabs/ICustomTabsCallback;
    invoke-virtual {p2}, Landroid/os/Parcel;->readInt()I

    move-result v3

    .line 272
    .local v3, "_arg1":I
    invoke-virtual {p2}, Landroid/os/Parcel;->readInt()I

    move-result v4

    if-eqz v4, :cond_5

    .line 273
    sget-object v4, Landroid/net/Uri;->CREATOR:Landroid/os/Parcelable$Creator;

    invoke-interface {v4, p2}, Landroid/os/Parcelable$Creator;->createFromParcel(Landroid/os/Parcel;)Ljava/lang/Object;

    move-result-object v4

    check-cast v4, Landroid/net/Uri;

    .local v4, "_arg2":Landroid/net/Uri;
    goto :goto_5

    .line 276
    .end local v4    # "_arg2":Landroid/net/Uri;
    :cond_5
    const/4 v4, 0x0

    .line 279
    .restart local v4    # "_arg2":Landroid/net/Uri;
    :goto_5
    invoke-virtual {p2}, Landroid/os/Parcel;->readInt()I

    move-result v5

    if-eqz v5, :cond_6

    .line 280
    sget-object v5, Landroid/os/Bundle;->CREATOR:Landroid/os/Parcelable$Creator;

    invoke-interface {v5, p2}, Landroid/os/Parcelable$Creator;->createFromParcel(Landroid/os/Parcel;)Ljava/lang/Object;

    move-result-object v5

    check-cast v5, Landroid/os/Bundle;

    .local v5, "_arg3":Landroid/os/Bundle;
    goto :goto_6

    .line 283
    .end local v5    # "_arg3":Landroid/os/Bundle;
    :cond_6
    const/4 v5, 0x0

    .line 285
    .restart local v5    # "_arg3":Landroid/os/Bundle;
    :goto_6
    invoke-virtual {p0, v2, v3, v4, v5}, Landroid/support/customtabs/ICustomTabsService$Stub;->validateRelationship(Landroid/support/customtabs/ICustomTabsCallback;ILandroid/net/Uri;Landroid/os/Bundle;)Z

    move-result v6

    .line 286
    .restart local v6    # "_result":Z
    invoke-virtual {p3}, Landroid/os/Parcel;->writeNoException()V

    .line 287
    invoke-virtual {p3, v6}, Landroid/os/Parcel;->writeInt(I)V

    .line 288
    return v1

    .line 247
    .end local v2    # "_arg0":Landroid/support/customtabs/ICustomTabsCallback;
    .end local v3    # "_arg1":I
    .end local v4    # "_arg2":Landroid/net/Uri;
    .end local v5    # "_arg3":Landroid/os/Bundle;
    .end local v6    # "_result":Z
    :sswitch_5
    invoke-virtual {p2, v0}, Landroid/os/Parcel;->enforceInterface(Ljava/lang/String;)V

    .line 249
    invoke-virtual {p2}, Landroid/os/Parcel;->readStrongBinder()Landroid/os/IBinder;

    move-result-object v2

    invoke-static {v2}, Landroid/support/customtabs/ICustomTabsCallback$Stub;->asInterface(Landroid/os/IBinder;)Landroid/support/customtabs/ICustomTabsCallback;

    move-result-object v2

    .line 251
    .restart local v2    # "_arg0":Landroid/support/customtabs/ICustomTabsCallback;
    invoke-virtual {p2}, Landroid/os/Parcel;->readString()Ljava/lang/String;

    move-result-object v3

    .line 253
    .local v3, "_arg1":Ljava/lang/String;
    invoke-virtual {p2}, Landroid/os/Parcel;->readInt()I

    move-result v4

    if-eqz v4, :cond_7

    .line 254
    sget-object v4, Landroid/os/Bundle;->CREATOR:Landroid/os/Parcelable$Creator;

    invoke-interface {v4, p2}, Landroid/os/Parcelable$Creator;->createFromParcel(Landroid/os/Parcel;)Ljava/lang/Object;

    move-result-object v4

    check-cast v4, Landroid/os/Bundle;

    .local v4, "_arg2":Landroid/os/Bundle;
    goto :goto_7

    .line 257
    .end local v4    # "_arg2":Landroid/os/Bundle;
    :cond_7
    const/4 v4, 0x0

    .line 259
    .restart local v4    # "_arg2":Landroid/os/Bundle;
    :goto_7
    invoke-virtual {p0, v2, v3, v4}, Landroid/support/customtabs/ICustomTabsService$Stub;->postMessage(Landroid/support/customtabs/ICustomTabsCallback;Ljava/lang/String;Landroid/os/Bundle;)I

    move-result v5

    .line 260
    .local v5, "_result":I
    invoke-virtual {p3}, Landroid/os/Parcel;->writeNoException()V

    .line 261
    invoke-virtual {p3, v5}, Landroid/os/Parcel;->writeInt(I)V

    .line 262
    return v1

    .line 206
    .end local v2    # "_arg0":Landroid/support/customtabs/ICustomTabsCallback;
    .end local v3    # "_arg1":Ljava/lang/String;
    .end local v4    # "_arg2":Landroid/os/Bundle;
    .end local v5    # "_result":I
    :sswitch_6
    invoke-virtual {p2, v0}, Landroid/os/Parcel;->enforceInterface(Ljava/lang/String;)V

    .line 208
    invoke-virtual {p2}, Landroid/os/Parcel;->readStrongBinder()Landroid/os/IBinder;

    move-result-object v2

    invoke-static {v2}, Landroid/support/customtabs/ICustomTabsCallback$Stub;->asInterface(Landroid/os/IBinder;)Landroid/support/customtabs/ICustomTabsCallback;

    move-result-object v2

    .line 210
    .restart local v2    # "_arg0":Landroid/support/customtabs/ICustomTabsCallback;
    invoke-virtual {p2}, Landroid/os/Parcel;->readInt()I

    move-result v3

    if-eqz v3, :cond_8

    .line 211
    sget-object v3, Landroid/net/Uri;->CREATOR:Landroid/os/Parcelable$Creator;

    invoke-interface {v3, p2}, Landroid/os/Parcelable$Creator;->createFromParcel(Landroid/os/Parcel;)Ljava/lang/Object;

    move-result-object v3

    check-cast v3, Landroid/net/Uri;

    .local v3, "_arg1":Landroid/net/Uri;
    goto :goto_8

    .line 214
    .end local v3    # "_arg1":Landroid/net/Uri;
    :cond_8
    const/4 v3, 0x0

    .line 216
    .restart local v3    # "_arg1":Landroid/net/Uri;
    :goto_8
    invoke-virtual {p0, v2, v3}, Landroid/support/customtabs/ICustomTabsService$Stub;->requestPostMessageChannel(Landroid/support/customtabs/ICustomTabsCallback;Landroid/net/Uri;)Z

    move-result v4

    .line 217
    .local v4, "_result":Z
    invoke-virtual {p3}, Landroid/os/Parcel;->writeNoException()V

    .line 218
    invoke-virtual {p3, v4}, Landroid/os/Parcel;->writeInt(I)V

    .line 219
    return v1

    .line 189
    .end local v2    # "_arg0":Landroid/support/customtabs/ICustomTabsCallback;
    .end local v3    # "_arg1":Landroid/net/Uri;
    .end local v4    # "_result":Z
    :sswitch_7
    invoke-virtual {p2, v0}, Landroid/os/Parcel;->enforceInterface(Ljava/lang/String;)V

    .line 191
    invoke-virtual {p2}, Landroid/os/Parcel;->readStrongBinder()Landroid/os/IBinder;

    move-result-object v2

    invoke-static {v2}, Landroid/support/customtabs/ICustomTabsCallback$Stub;->asInterface(Landroid/os/IBinder;)Landroid/support/customtabs/ICustomTabsCallback;

    move-result-object v2

    .line 193
    .restart local v2    # "_arg0":Landroid/support/customtabs/ICustomTabsCallback;
    invoke-virtual {p2}, Landroid/os/Parcel;->readInt()I

    move-result v3

    if-eqz v3, :cond_9

    .line 194
    sget-object v3, Landroid/os/Bundle;->CREATOR:Landroid/os/Parcelable$Creator;

    invoke-interface {v3, p2}, Landroid/os/Parcelable$Creator;->createFromParcel(Landroid/os/Parcel;)Ljava/lang/Object;

    move-result-object v3

    check-cast v3, Landroid/os/Bundle;

    .local v3, "_arg1":Landroid/os/Bundle;
    goto :goto_9

    .line 197
    .end local v3    # "_arg1":Landroid/os/Bundle;
    :cond_9
    const/4 v3, 0x0

    .line 199
    .restart local v3    # "_arg1":Landroid/os/Bundle;
    :goto_9
    invoke-virtual {p0, v2, v3}, Landroid/support/customtabs/ICustomTabsService$Stub;->updateVisuals(Landroid/support/customtabs/ICustomTabsCallback;Landroid/os/Bundle;)Z

    move-result v4

    .line 200
    .restart local v4    # "_result":Z
    invoke-virtual {p3}, Landroid/os/Parcel;->writeNoException()V

    .line 201
    invoke-virtual {p3, v4}, Landroid/os/Parcel;->writeInt(I)V

    .line 202
    return v1

    .line 166
    .end local v2    # "_arg0":Landroid/support/customtabs/ICustomTabsCallback;
    .end local v3    # "_arg1":Landroid/os/Bundle;
    .end local v4    # "_result":Z
    :sswitch_8
    invoke-virtual {p2, v0}, Landroid/os/Parcel;->enforceInterface(Ljava/lang/String;)V

    .line 168
    invoke-virtual {p2}, Landroid/os/Parcel;->readString()Ljava/lang/String;

    move-result-object v2

    .line 170
    .local v2, "_arg0":Ljava/lang/String;
    invoke-virtual {p2}, Landroid/os/Parcel;->readInt()I

    move-result v3

    if-eqz v3, :cond_a

    .line 171
    sget-object v3, Landroid/os/Bundle;->CREATOR:Landroid/os/Parcelable$Creator;

    invoke-interface {v3, p2}, Landroid/os/Parcelable$Creator;->createFromParcel(Landroid/os/Parcel;)Ljava/lang/Object;

    move-result-object v3

    check-cast v3, Landroid/os/Bundle;

    .restart local v3    # "_arg1":Landroid/os/Bundle;
    goto :goto_a

    .line 174
    .end local v3    # "_arg1":Landroid/os/Bundle;
    :cond_a
    const/4 v3, 0x0

    .line 176
    .restart local v3    # "_arg1":Landroid/os/Bundle;
    :goto_a
    invoke-virtual {p0, v2, v3}, Landroid/support/customtabs/ICustomTabsService$Stub;->extraCommand(Ljava/lang/String;Landroid/os/Bundle;)Landroid/os/Bundle;

    move-result-object v4

    .line 177
    .local v4, "_result":Landroid/os/Bundle;
    invoke-virtual {p3}, Landroid/os/Parcel;->writeNoException()V

    .line 178
    if-eqz v4, :cond_b

    .line 179
    invoke-virtual {p3, v1}, Landroid/os/Parcel;->writeInt(I)V

    .line 180
    invoke-virtual {v4, p3, v1}, Landroid/os/Bundle;->writeToParcel(Landroid/os/Parcel;I)V

    goto :goto_b

    .line 183
    :cond_b
    const/4 v5, 0x0

    invoke-virtual {p3, v5}, Landroid/os/Parcel;->writeInt(I)V

    .line 185
    :goto_b
    return v1

    .line 140
    .end local v2    # "_arg0":Ljava/lang/String;
    .end local v3    # "_arg1":Landroid/os/Bundle;
    .end local v4    # "_result":Landroid/os/Bundle;
    :sswitch_9
    invoke-virtual {p2, v0}, Landroid/os/Parcel;->enforceInterface(Ljava/lang/String;)V

    .line 142
    invoke-virtual {p2}, Landroid/os/Parcel;->readStrongBinder()Landroid/os/IBinder;

    move-result-object v2

    invoke-static {v2}, Landroid/support/customtabs/ICustomTabsCallback$Stub;->asInterface(Landroid/os/IBinder;)Landroid/support/customtabs/ICustomTabsCallback;

    move-result-object v2

    .line 144
    .local v2, "_arg0":Landroid/support/customtabs/ICustomTabsCallback;
    invoke-virtual {p2}, Landroid/os/Parcel;->readInt()I

    move-result v3

    if-eqz v3, :cond_c

    .line 145
    sget-object v3, Landroid/net/Uri;->CREATOR:Landroid/os/Parcelable$Creator;

    invoke-interface {v3, p2}, Landroid/os/Parcelable$Creator;->createFromParcel(Landroid/os/Parcel;)Ljava/lang/Object;

    move-result-object v3

    check-cast v3, Landroid/net/Uri;

    .local v3, "_arg1":Landroid/net/Uri;
    goto :goto_c

    .line 148
    .end local v3    # "_arg1":Landroid/net/Uri;
    :cond_c
    const/4 v3, 0x0

    .line 151
    .restart local v3    # "_arg1":Landroid/net/Uri;
    :goto_c
    invoke-virtual {p2}, Landroid/os/Parcel;->readInt()I

    move-result v4

    if-eqz v4, :cond_d

    .line 152
    sget-object v4, Landroid/os/Bundle;->CREATOR:Landroid/os/Parcelable$Creator;

    invoke-interface {v4, p2}, Landroid/os/Parcelable$Creator;->createFromParcel(Landroid/os/Parcel;)Ljava/lang/Object;

    move-result-object v4

    check-cast v4, Landroid/os/Bundle;

    .local v4, "_arg2":Landroid/os/Bundle;
    goto :goto_d

    .line 155
    .end local v4    # "_arg2":Landroid/os/Bundle;
    :cond_d
    const/4 v4, 0x0

    .line 158
    .restart local v4    # "_arg2":Landroid/os/Bundle;
    :goto_d
    sget-object v5, Landroid/os/Bundle;->CREATOR:Landroid/os/Parcelable$Creator;

    invoke-virtual {p2, v5}, Landroid/os/Parcel;->createTypedArrayList(Landroid/os/Parcelable$Creator;)Ljava/util/ArrayList;

    move-result-object v5

    .line 159
    .local v5, "_arg3":Ljava/util/List;, "Ljava/util/List<Landroid/os/Bundle;>;"
    invoke-virtual {p0, v2, v3, v4, v5}, Landroid/support/customtabs/ICustomTabsService$Stub;->mayLaunchUrl(Landroid/support/customtabs/ICustomTabsCallback;Landroid/net/Uri;Landroid/os/Bundle;Ljava/util/List;)Z

    move-result v6

    .line 160
    .restart local v6    # "_result":Z
    invoke-virtual {p3}, Landroid/os/Parcel;->writeNoException()V

    .line 161
    invoke-virtual {p3, v6}, Landroid/os/Parcel;->writeInt(I)V

    .line 162
    return v1

    .line 113
    .end local v2    # "_arg0":Landroid/support/customtabs/ICustomTabsCallback;
    .end local v3    # "_arg1":Landroid/net/Uri;
    .end local v4    # "_arg2":Landroid/os/Bundle;
    .end local v5    # "_arg3":Ljava/util/List;, "Ljava/util/List<Landroid/os/Bundle;>;"
    .end local v6    # "_result":Z
    :sswitch_a
    invoke-virtual {p2, v0}, Landroid/os/Parcel;->enforceInterface(Ljava/lang/String;)V

    .line 115
    invoke-virtual {p2}, Landroid/os/Parcel;->readStrongBinder()Landroid/os/IBinder;

    move-result-object v2

    invoke-static {v2}, Landroid/support/customtabs/ICustomTabsCallback$Stub;->asInterface(Landroid/os/IBinder;)Landroid/support/customtabs/ICustomTabsCallback;

    move-result-object v2

    .line 116
    .restart local v2    # "_arg0":Landroid/support/customtabs/ICustomTabsCallback;
    invoke-virtual {p0, v2}, Landroid/support/customtabs/ICustomTabsService$Stub;->newSession(Landroid/support/customtabs/ICustomTabsCallback;)Z

    move-result v3

    .line 117
    .local v3, "_result":Z
    invoke-virtual {p3}, Landroid/os/Parcel;->writeNoException()V

    .line 118
    invoke-virtual {p3, v3}, Landroid/os/Parcel;->writeInt(I)V

    .line 119
    return v1

    .line 103
    .end local v2    # "_arg0":Landroid/support/customtabs/ICustomTabsCallback;
    .end local v3    # "_result":Z
    :sswitch_b
    invoke-virtual {p2, v0}, Landroid/os/Parcel;->enforceInterface(Ljava/lang/String;)V

    .line 105
    invoke-virtual {p2}, Landroid/os/Parcel;->readLong()J

    move-result-wide v2

    .line 106
    .local v2, "_arg0":J
    invoke-virtual {p0, v2, v3}, Landroid/support/customtabs/ICustomTabsService$Stub;->warmup(J)Z

    move-result v4

    .line 107
    .local v4, "_result":Z
    invoke-virtual {p3}, Landroid/os/Parcel;->writeNoException()V

    .line 108
    invoke-virtual {p3, v4}, Landroid/os/Parcel;->writeInt(I)V

    .line 109
    return v1

    nop

    :sswitch_data_0
    .sparse-switch
        0x2 -> :sswitch_b
        0x3 -> :sswitch_a
        0x4 -> :sswitch_9
        0x5 -> :sswitch_8
        0x6 -> :sswitch_7
        0x7 -> :sswitch_6
        0x8 -> :sswitch_5
        0x9 -> :sswitch_4
        0xa -> :sswitch_3
        0xb -> :sswitch_2
        0xc -> :sswitch_1
        0x5f4e5446 -> :sswitch_0
    .end sparse-switch
.end method
