.class public abstract Landroid/support/customtabs/trusted/ITrustedWebActivityService$Stub;
.super Landroid/os/Binder;
.source "ITrustedWebActivityService.java"

# interfaces
.implements Landroid/support/customtabs/trusted/ITrustedWebActivityService;


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Landroid/support/customtabs/trusted/ITrustedWebActivityService;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x409
    name = "Stub"
.end annotation

.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Landroid/support/customtabs/trusted/ITrustedWebActivityService$Stub$Proxy;
    }
.end annotation


# static fields
.field private static final DESCRIPTOR:Ljava/lang/String; = "android.support.customtabs.trusted.ITrustedWebActivityService"

.field static final TRANSACTION_areNotificationsEnabled:I = 0x6

.field static final TRANSACTION_cancelNotification:I = 0x3

.field static final TRANSACTION_extraCommand:I = 0x9

.field static final TRANSACTION_getActiveNotifications:I = 0x5

.field static final TRANSACTION_getSmallIconBitmap:I = 0x7

.field static final TRANSACTION_getSmallIconId:I = 0x4

.field static final TRANSACTION_notifyNotificationWithChannel:I = 0x2


# direct methods
.method public constructor <init>()V
    .locals 1

    .line 52
    invoke-direct {p0}, Landroid/os/Binder;-><init>()V

    .line 53
    const-string v0, "android.support.customtabs.trusted.ITrustedWebActivityService"

    invoke-virtual {p0, p0, v0}, Landroid/support/customtabs/trusted/ITrustedWebActivityService$Stub;->attachInterface(Landroid/os/IInterface;Ljava/lang/String;)V

    .line 54
    return-void
.end method

.method public static asInterface(Landroid/os/IBinder;)Landroid/support/customtabs/trusted/ITrustedWebActivityService;
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

    .line 61
    if-nez p0, :cond_0

    .line 62
    const/4 v0, 0x0

    return-object v0

    .line 64
    :cond_0
    const-string v0, "android.support.customtabs.trusted.ITrustedWebActivityService"

    invoke-interface {p0, v0}, Landroid/os/IBinder;->queryLocalInterface(Ljava/lang/String;)Landroid/os/IInterface;

    move-result-object v0

    .line 65
    .local v0, "iin":Landroid/os/IInterface;
    if-eqz v0, :cond_1

    instance-of v1, v0, Landroid/support/customtabs/trusted/ITrustedWebActivityService;

    if-eqz v1, :cond_1

    .line 66
    move-object v1, v0

    check-cast v1, Landroid/support/customtabs/trusted/ITrustedWebActivityService;

    return-object v1

    .line 68
    :cond_1
    new-instance v1, Landroid/support/customtabs/trusted/ITrustedWebActivityService$Stub$Proxy;

    invoke-direct {v1, p0}, Landroid/support/customtabs/trusted/ITrustedWebActivityService$Stub$Proxy;-><init>(Landroid/os/IBinder;)V

    return-object v1
.end method

.method public static getDefaultImpl()Landroid/support/customtabs/trusted/ITrustedWebActivityService;
    .locals 1

    .line 438
    sget-object v0, Landroid/support/customtabs/trusted/ITrustedWebActivityService$Stub$Proxy;->sDefaultImpl:Landroid/support/customtabs/trusted/ITrustedWebActivityService;

    return-object v0
.end method

.method public static setDefaultImpl(Landroid/support/customtabs/trusted/ITrustedWebActivityService;)Z
    .locals 2
    .param p0, "impl"    # Landroid/support/customtabs/trusted/ITrustedWebActivityService;
    .annotation system Ldalvik/annotation/MethodParameters;
        accessFlags = {
            0x0
        }
        names = {
            "impl"
        }
    .end annotation

    .line 428
    sget-object v0, Landroid/support/customtabs/trusted/ITrustedWebActivityService$Stub$Proxy;->sDefaultImpl:Landroid/support/customtabs/trusted/ITrustedWebActivityService;

    if-nez v0, :cond_1

    .line 431
    if-eqz p0, :cond_0

    .line 432
    sput-object p0, Landroid/support/customtabs/trusted/ITrustedWebActivityService$Stub$Proxy;->sDefaultImpl:Landroid/support/customtabs/trusted/ITrustedWebActivityService;

    .line 433
    const/4 v0, 0x1

    return v0

    .line 435
    :cond_0
    const/4 v0, 0x0

    return v0

    .line 429
    :cond_1
    new-instance v0, Ljava/lang/IllegalStateException;

    const-string v1, "setDefaultImpl() called twice"

    invoke-direct {v0, v1}, Ljava/lang/IllegalStateException;-><init>(Ljava/lang/String;)V

    throw v0
.end method


# virtual methods
.method public asBinder()Landroid/os/IBinder;
    .locals 0

    .line 72
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

    .line 76
    const-string v0, "android.support.customtabs.trusted.ITrustedWebActivityService"

    .line 77
    .local v0, "descriptor":Ljava/lang/String;
    const/4 v1, 0x0

    const/4 v2, 0x1

    sparse-switch p1, :sswitch_data_0

    .line 203
    invoke-super {p0, p1, p2, p3, p4}, Landroid/os/Binder;->onTransact(ILandroid/os/Parcel;Landroid/os/Parcel;I)Z

    move-result v1

    return v1

    .line 81
    :sswitch_0
    invoke-virtual {p3, v0}, Landroid/os/Parcel;->writeString(Ljava/lang/String;)V

    .line 82
    return v2

    .line 178
    :sswitch_1
    invoke-virtual {p2, v0}, Landroid/os/Parcel;->enforceInterface(Ljava/lang/String;)V

    .line 180
    invoke-virtual {p2}, Landroid/os/Parcel;->readString()Ljava/lang/String;

    move-result-object v3

    .line 182
    .local v3, "_arg0":Ljava/lang/String;
    invoke-virtual {p2}, Landroid/os/Parcel;->readInt()I

    move-result v4

    if-eqz v4, :cond_0

    .line 183
    sget-object v4, Landroid/os/Bundle;->CREATOR:Landroid/os/Parcelable$Creator;

    invoke-interface {v4, p2}, Landroid/os/Parcelable$Creator;->createFromParcel(Landroid/os/Parcel;)Ljava/lang/Object;

    move-result-object v4

    check-cast v4, Landroid/os/Bundle;

    .local v4, "_arg1":Landroid/os/Bundle;
    goto :goto_0

    .line 186
    .end local v4    # "_arg1":Landroid/os/Bundle;
    :cond_0
    const/4 v4, 0x0

    .line 189
    .restart local v4    # "_arg1":Landroid/os/Bundle;
    :goto_0
    invoke-virtual {p2}, Landroid/os/Parcel;->readStrongBinder()Landroid/os/IBinder;

    move-result-object v5

    .line 190
    .local v5, "_arg2":Landroid/os/IBinder;
    invoke-virtual {p0, v3, v4, v5}, Landroid/support/customtabs/trusted/ITrustedWebActivityService$Stub;->extraCommand(Ljava/lang/String;Landroid/os/Bundle;Landroid/os/IBinder;)Landroid/os/Bundle;

    move-result-object v6

    .line 191
    .local v6, "_result":Landroid/os/Bundle;
    invoke-virtual {p3}, Landroid/os/Parcel;->writeNoException()V

    .line 192
    if-eqz v6, :cond_1

    .line 193
    invoke-virtual {p3, v2}, Landroid/os/Parcel;->writeInt(I)V

    .line 194
    invoke-virtual {v6, p3, v2}, Landroid/os/Bundle;->writeToParcel(Landroid/os/Parcel;I)V

    goto :goto_1

    .line 197
    :cond_1
    invoke-virtual {p3, v1}, Landroid/os/Parcel;->writeInt(I)V

    .line 199
    :goto_1
    return v2

    .line 164
    .end local v3    # "_arg0":Ljava/lang/String;
    .end local v4    # "_arg1":Landroid/os/Bundle;
    .end local v5    # "_arg2":Landroid/os/IBinder;
    .end local v6    # "_result":Landroid/os/Bundle;
    :sswitch_2
    invoke-virtual {p2, v0}, Landroid/os/Parcel;->enforceInterface(Ljava/lang/String;)V

    .line 165
    invoke-virtual {p0}, Landroid/support/customtabs/trusted/ITrustedWebActivityService$Stub;->getSmallIconBitmap()Landroid/os/Bundle;

    move-result-object v3

    .line 166
    .local v3, "_result":Landroid/os/Bundle;
    invoke-virtual {p3}, Landroid/os/Parcel;->writeNoException()V

    .line 167
    if-eqz v3, :cond_2

    .line 168
    invoke-virtual {p3, v2}, Landroid/os/Parcel;->writeInt(I)V

    .line 169
    invoke-virtual {v3, p3, v2}, Landroid/os/Bundle;->writeToParcel(Landroid/os/Parcel;I)V

    goto :goto_2

    .line 172
    :cond_2
    invoke-virtual {p3, v1}, Landroid/os/Parcel;->writeInt(I)V

    .line 174
    :goto_2
    return v2

    .line 86
    .end local v3    # "_result":Landroid/os/Bundle;
    :sswitch_3
    invoke-virtual {p2, v0}, Landroid/os/Parcel;->enforceInterface(Ljava/lang/String;)V

    .line 88
    invoke-virtual {p2}, Landroid/os/Parcel;->readInt()I

    move-result v3

    if-eqz v3, :cond_3

    .line 89
    sget-object v3, Landroid/os/Bundle;->CREATOR:Landroid/os/Parcelable$Creator;

    invoke-interface {v3, p2}, Landroid/os/Parcelable$Creator;->createFromParcel(Landroid/os/Parcel;)Ljava/lang/Object;

    move-result-object v3

    check-cast v3, Landroid/os/Bundle;

    .local v3, "_arg0":Landroid/os/Bundle;
    goto :goto_3

    .line 92
    .end local v3    # "_arg0":Landroid/os/Bundle;
    :cond_3
    const/4 v3, 0x0

    .line 94
    .restart local v3    # "_arg0":Landroid/os/Bundle;
    :goto_3
    invoke-virtual {p0, v3}, Landroid/support/customtabs/trusted/ITrustedWebActivityService$Stub;->areNotificationsEnabled(Landroid/os/Bundle;)Landroid/os/Bundle;

    move-result-object v4

    .line 95
    .local v4, "_result":Landroid/os/Bundle;
    invoke-virtual {p3}, Landroid/os/Parcel;->writeNoException()V

    .line 96
    if-eqz v4, :cond_4

    .line 97
    invoke-virtual {p3, v2}, Landroid/os/Parcel;->writeInt(I)V

    .line 98
    invoke-virtual {v4, p3, v2}, Landroid/os/Bundle;->writeToParcel(Landroid/os/Parcel;I)V

    goto :goto_4

    .line 101
    :cond_4
    invoke-virtual {p3, v1}, Landroid/os/Parcel;->writeInt(I)V

    .line 103
    :goto_4
    return v2

    .line 142
    .end local v3    # "_arg0":Landroid/os/Bundle;
    .end local v4    # "_result":Landroid/os/Bundle;
    :sswitch_4
    invoke-virtual {p2, v0}, Landroid/os/Parcel;->enforceInterface(Ljava/lang/String;)V

    .line 143
    invoke-virtual {p0}, Landroid/support/customtabs/trusted/ITrustedWebActivityService$Stub;->getActiveNotifications()Landroid/os/Bundle;

    move-result-object v3

    .line 144
    .local v3, "_result":Landroid/os/Bundle;
    invoke-virtual {p3}, Landroid/os/Parcel;->writeNoException()V

    .line 145
    if-eqz v3, :cond_5

    .line 146
    invoke-virtual {p3, v2}, Landroid/os/Parcel;->writeInt(I)V

    .line 147
    invoke-virtual {v3, p3, v2}, Landroid/os/Bundle;->writeToParcel(Landroid/os/Parcel;I)V

    goto :goto_5

    .line 150
    :cond_5
    invoke-virtual {p3, v1}, Landroid/os/Parcel;->writeInt(I)V

    .line 152
    :goto_5
    return v2

    .line 156
    .end local v3    # "_result":Landroid/os/Bundle;
    :sswitch_5
    invoke-virtual {p2, v0}, Landroid/os/Parcel;->enforceInterface(Ljava/lang/String;)V

    .line 157
    invoke-virtual {p0}, Landroid/support/customtabs/trusted/ITrustedWebActivityService$Stub;->getSmallIconId()I

    move-result v1

    .line 158
    .local v1, "_result":I
    invoke-virtual {p3}, Landroid/os/Parcel;->writeNoException()V

    .line 159
    invoke-virtual {p3, v1}, Landroid/os/Parcel;->writeInt(I)V

    .line 160
    return v2

    .line 128
    .end local v1    # "_result":I
    :sswitch_6
    invoke-virtual {p2, v0}, Landroid/os/Parcel;->enforceInterface(Ljava/lang/String;)V

    .line 130
    invoke-virtual {p2}, Landroid/os/Parcel;->readInt()I

    move-result v1

    if-eqz v1, :cond_6

    .line 131
    sget-object v1, Landroid/os/Bundle;->CREATOR:Landroid/os/Parcelable$Creator;

    invoke-interface {v1, p2}, Landroid/os/Parcelable$Creator;->createFromParcel(Landroid/os/Parcel;)Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Landroid/os/Bundle;

    .local v1, "_arg0":Landroid/os/Bundle;
    goto :goto_6

    .line 134
    .end local v1    # "_arg0":Landroid/os/Bundle;
    :cond_6
    const/4 v1, 0x0

    .line 136
    .restart local v1    # "_arg0":Landroid/os/Bundle;
    :goto_6
    invoke-virtual {p0, v1}, Landroid/support/customtabs/trusted/ITrustedWebActivityService$Stub;->cancelNotification(Landroid/os/Bundle;)V

    .line 137
    invoke-virtual {p3}, Landroid/os/Parcel;->writeNoException()V

    .line 138
    return v2

    .line 107
    .end local v1    # "_arg0":Landroid/os/Bundle;
    :sswitch_7
    invoke-virtual {p2, v0}, Landroid/os/Parcel;->enforceInterface(Ljava/lang/String;)V

    .line 109
    invoke-virtual {p2}, Landroid/os/Parcel;->readInt()I

    move-result v3

    if-eqz v3, :cond_7

    .line 110
    sget-object v3, Landroid/os/Bundle;->CREATOR:Landroid/os/Parcelable$Creator;

    invoke-interface {v3, p2}, Landroid/os/Parcelable$Creator;->createFromParcel(Landroid/os/Parcel;)Ljava/lang/Object;

    move-result-object v3

    check-cast v3, Landroid/os/Bundle;

    .local v3, "_arg0":Landroid/os/Bundle;
    goto :goto_7

    .line 113
    .end local v3    # "_arg0":Landroid/os/Bundle;
    :cond_7
    const/4 v3, 0x0

    .line 115
    .restart local v3    # "_arg0":Landroid/os/Bundle;
    :goto_7
    invoke-virtual {p0, v3}, Landroid/support/customtabs/trusted/ITrustedWebActivityService$Stub;->notifyNotificationWithChannel(Landroid/os/Bundle;)Landroid/os/Bundle;

    move-result-object v4

    .line 116
    .restart local v4    # "_result":Landroid/os/Bundle;
    invoke-virtual {p3}, Landroid/os/Parcel;->writeNoException()V

    .line 117
    if-eqz v4, :cond_8

    .line 118
    invoke-virtual {p3, v2}, Landroid/os/Parcel;->writeInt(I)V

    .line 119
    invoke-virtual {v4, p3, v2}, Landroid/os/Bundle;->writeToParcel(Landroid/os/Parcel;I)V

    goto :goto_8

    .line 122
    :cond_8
    invoke-virtual {p3, v1}, Landroid/os/Parcel;->writeInt(I)V

    .line 124
    :goto_8
    return v2

    nop

    :sswitch_data_0
    .sparse-switch
        0x2 -> :sswitch_7
        0x3 -> :sswitch_6
        0x4 -> :sswitch_5
        0x5 -> :sswitch_4
        0x6 -> :sswitch_3
        0x7 -> :sswitch_2
        0x9 -> :sswitch_1
        0x5f4e5446 -> :sswitch_0
    .end sparse-switch
.end method
