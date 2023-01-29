.class public final Landroidx/browser/trusted/TrustedWebActivityServiceConnection;
.super Ljava/lang/Object;
.source "TrustedWebActivityServiceConnection.java"


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Landroidx/browser/trusted/TrustedWebActivityServiceConnection$NotificationsEnabledArgs;,
        Landroidx/browser/trusted/TrustedWebActivityServiceConnection$ActiveNotificationsArgs;,
        Landroidx/browser/trusted/TrustedWebActivityServiceConnection$ResultArgs;,
        Landroidx/browser/trusted/TrustedWebActivityServiceConnection$CancelNotificationArgs;,
        Landroidx/browser/trusted/TrustedWebActivityServiceConnection$NotifyNotificationArgs;
    }
.end annotation


# static fields
.field private static final KEY_ACTIVE_NOTIFICATIONS:Ljava/lang/String; = "android.support.customtabs.trusted.ACTIVE_NOTIFICATIONS"

.field private static final KEY_CHANNEL_NAME:Ljava/lang/String; = "android.support.customtabs.trusted.CHANNEL_NAME"

.field private static final KEY_NOTIFICATION:Ljava/lang/String; = "android.support.customtabs.trusted.NOTIFICATION"

.field private static final KEY_NOTIFICATION_SUCCESS:Ljava/lang/String; = "android.support.customtabs.trusted.NOTIFICATION_SUCCESS"

.field private static final KEY_PLATFORM_ID:Ljava/lang/String; = "android.support.customtabs.trusted.PLATFORM_ID"

.field private static final KEY_PLATFORM_TAG:Ljava/lang/String; = "android.support.customtabs.trusted.PLATFORM_TAG"


# instance fields
.field private final mComponentName:Landroid/content/ComponentName;

.field private final mService:Landroid/support/customtabs/trusted/ITrustedWebActivityService;


# direct methods
.method constructor <init>(Landroid/support/customtabs/trusted/ITrustedWebActivityService;Landroid/content/ComponentName;)V
    .locals 0
    .param p1, "service"    # Landroid/support/customtabs/trusted/ITrustedWebActivityService;
    .param p2, "componentName"    # Landroid/content/ComponentName;
    .annotation system Ldalvik/annotation/MethodParameters;
        accessFlags = {
            0x0,
            0x0
        }
        names = {
            "service",
            "componentName"
        }
    .end annotation

    .line 66
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 67
    iput-object p1, p0, Landroidx/browser/trusted/TrustedWebActivityServiceConnection;->mService:Landroid/support/customtabs/trusted/ITrustedWebActivityService;

    .line 68
    iput-object p2, p0, Landroidx/browser/trusted/TrustedWebActivityServiceConnection;->mComponentName:Landroid/content/ComponentName;

    .line 69
    return-void
.end method

.method static ensureBundleContains(Landroid/os/Bundle;Ljava/lang/String;)V
    .locals 3
    .param p0, "args"    # Landroid/os/Bundle;
    .param p1, "key"    # Ljava/lang/String;
    .annotation system Ldalvik/annotation/MethodParameters;
        accessFlags = {
            0x0,
            0x0
        }
        names = {
            "args",
            "key"
        }
    .end annotation

    .line 307
    invoke-virtual {p0, p1}, Landroid/os/Bundle;->containsKey(Ljava/lang/String;)Z

    move-result v0

    if-eqz v0, :cond_0

    return-void

    .line 308
    :cond_0
    new-instance v0, Ljava/lang/IllegalArgumentException;

    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "Bundle must contain "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v1

    invoke-virtual {v1, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v1

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-direct {v0, v1}, Ljava/lang/IllegalArgumentException;-><init>(Ljava/lang/String;)V

    throw v0
.end method

.method private static wrapCallback(Landroidx/browser/trusted/TrustedWebActivityCallback;)Landroid/support/customtabs/trusted/ITrustedWebActivityCallback;
    .locals 1
    .param p0, "callback"    # Landroidx/browser/trusted/TrustedWebActivityCallback;
    .annotation system Ldalvik/annotation/MethodParameters;
        accessFlags = {
            0x0
        }
        names = {
            "callback"
        }
    .end annotation

    .line 314
    if-nez p0, :cond_0

    const/4 v0, 0x0

    return-object v0

    .line 315
    :cond_0
    new-instance v0, Landroidx/browser/trusted/TrustedWebActivityServiceConnection$1;

    invoke-direct {v0, p0}, Landroidx/browser/trusted/TrustedWebActivityServiceConnection$1;-><init>(Landroidx/browser/trusted/TrustedWebActivityCallback;)V

    return-object v0
.end method


# virtual methods
.method public areNotificationsEnabled(Ljava/lang/String;)Z
    .locals 2
    .param p1, "channelName"    # Ljava/lang/String;
    .annotation system Ldalvik/annotation/MethodParameters;
        accessFlags = {
            0x0
        }
        names = {
            "channelName"
        }
    .end annotation

    .annotation system Ldalvik/annotation/Throws;
        value = {
            Landroid/os/RemoteException;
        }
    .end annotation

    .line 79
    new-instance v0, Landroidx/browser/trusted/TrustedWebActivityServiceConnection$NotificationsEnabledArgs;

    invoke-direct {v0, p1}, Landroidx/browser/trusted/TrustedWebActivityServiceConnection$NotificationsEnabledArgs;-><init>(Ljava/lang/String;)V

    invoke-virtual {v0}, Landroidx/browser/trusted/TrustedWebActivityServiceConnection$NotificationsEnabledArgs;->toBundle()Landroid/os/Bundle;

    move-result-object v0

    .line 80
    .local v0, "args":Landroid/os/Bundle;
    iget-object v1, p0, Landroidx/browser/trusted/TrustedWebActivityServiceConnection;->mService:Landroid/support/customtabs/trusted/ITrustedWebActivityService;

    invoke-interface {v1, v0}, Landroid/support/customtabs/trusted/ITrustedWebActivityService;->areNotificationsEnabled(Landroid/os/Bundle;)Landroid/os/Bundle;

    move-result-object v1

    invoke-static {v1}, Landroidx/browser/trusted/TrustedWebActivityServiceConnection$ResultArgs;->fromBundle(Landroid/os/Bundle;)Landroidx/browser/trusted/TrustedWebActivityServiceConnection$ResultArgs;

    move-result-object v1

    iget-boolean v1, v1, Landroidx/browser/trusted/TrustedWebActivityServiceConnection$ResultArgs;->success:Z

    return v1
.end method

.method public cancel(Ljava/lang/String;I)V
    .locals 2
    .param p1, "platformTag"    # Ljava/lang/String;
    .param p2, "platformId"    # I
    .annotation system Ldalvik/annotation/MethodParameters;
        accessFlags = {
            0x0,
            0x0
        }
        names = {
            "platformTag",
            "platformId"
        }
    .end annotation

    .annotation system Ldalvik/annotation/Throws;
        value = {
            Landroid/os/RemoteException;
        }
    .end annotation

    .line 109
    new-instance v0, Landroidx/browser/trusted/TrustedWebActivityServiceConnection$CancelNotificationArgs;

    invoke-direct {v0, p1, p2}, Landroidx/browser/trusted/TrustedWebActivityServiceConnection$CancelNotificationArgs;-><init>(Ljava/lang/String;I)V

    invoke-virtual {v0}, Landroidx/browser/trusted/TrustedWebActivityServiceConnection$CancelNotificationArgs;->toBundle()Landroid/os/Bundle;

    move-result-object v0

    .line 110
    .local v0, "args":Landroid/os/Bundle;
    iget-object v1, p0, Landroidx/browser/trusted/TrustedWebActivityServiceConnection;->mService:Landroid/support/customtabs/trusted/ITrustedWebActivityService;

    invoke-interface {v1, v0}, Landroid/support/customtabs/trusted/ITrustedWebActivityService;->cancelNotification(Landroid/os/Bundle;)V

    .line 111
    return-void
.end method

.method public getActiveNotifications()[Landroid/os/Parcelable;
    .locals 2
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Landroid/os/RemoteException;
        }
    .end annotation

    .line 128
    iget-object v0, p0, Landroidx/browser/trusted/TrustedWebActivityServiceConnection;->mService:Landroid/support/customtabs/trusted/ITrustedWebActivityService;

    invoke-interface {v0}, Landroid/support/customtabs/trusted/ITrustedWebActivityService;->getActiveNotifications()Landroid/os/Bundle;

    move-result-object v0

    .line 129
    .local v0, "notifications":Landroid/os/Bundle;
    invoke-static {v0}, Landroidx/browser/trusted/TrustedWebActivityServiceConnection$ActiveNotificationsArgs;->fromBundle(Landroid/os/Bundle;)Landroidx/browser/trusted/TrustedWebActivityServiceConnection$ActiveNotificationsArgs;

    move-result-object v1

    iget-object v1, v1, Landroidx/browser/trusted/TrustedWebActivityServiceConnection$ActiveNotificationsArgs;->notifications:[Landroid/os/Parcelable;

    return-object v1
.end method

.method public getComponentName()Landroid/content/ComponentName;
    .locals 1

    .line 162
    iget-object v0, p0, Landroidx/browser/trusted/TrustedWebActivityServiceConnection;->mComponentName:Landroid/content/ComponentName;

    return-object v0
.end method

.method public getSmallIconBitmap()Landroid/graphics/Bitmap;
    .locals 2
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Landroid/os/RemoteException;
        }
    .end annotation

    .line 152
    iget-object v0, p0, Landroidx/browser/trusted/TrustedWebActivityServiceConnection;->mService:Landroid/support/customtabs/trusted/ITrustedWebActivityService;

    invoke-interface {v0}, Landroid/support/customtabs/trusted/ITrustedWebActivityService;->getSmallIconBitmap()Landroid/os/Bundle;

    move-result-object v0

    .line 153
    const-string v1, "android.support.customtabs.trusted.SMALL_ICON_BITMAP"

    invoke-virtual {v0, v1}, Landroid/os/Bundle;->getParcelable(Ljava/lang/String;)Landroid/os/Parcelable;

    move-result-object v0

    check-cast v0, Landroid/graphics/Bitmap;

    .line 152
    return-object v0
.end method

.method public getSmallIconId()I
    .locals 1
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Landroid/os/RemoteException;
        }
    .end annotation

    .line 139
    iget-object v0, p0, Landroidx/browser/trusted/TrustedWebActivityServiceConnection;->mService:Landroid/support/customtabs/trusted/ITrustedWebActivityService;

    invoke-interface {v0}, Landroid/support/customtabs/trusted/ITrustedWebActivityService;->getSmallIconId()I

    move-result v0

    return v0
.end method

.method public notify(Ljava/lang/String;ILandroid/app/Notification;Ljava/lang/String;)Z
    .locals 2
    .param p1, "platformTag"    # Ljava/lang/String;
    .param p2, "platformId"    # I
    .param p3, "notification"    # Landroid/app/Notification;
    .param p4, "channel"    # Ljava/lang/String;
    .annotation system Ldalvik/annotation/MethodParameters;
        accessFlags = {
            0x0,
            0x0,
            0x0,
            0x0
        }
        names = {
            "platformTag",
            "platformId",
            "notification",
            "channel"
        }
    .end annotation

    .annotation system Ldalvik/annotation/Throws;
        value = {
            Landroid/os/RemoteException;
        }
    .end annotation

    .line 96
    new-instance v0, Landroidx/browser/trusted/TrustedWebActivityServiceConnection$NotifyNotificationArgs;

    invoke-direct {v0, p1, p2, p3, p4}, Landroidx/browser/trusted/TrustedWebActivityServiceConnection$NotifyNotificationArgs;-><init>(Ljava/lang/String;ILandroid/app/Notification;Ljava/lang/String;)V

    .line 97
    invoke-virtual {v0}, Landroidx/browser/trusted/TrustedWebActivityServiceConnection$NotifyNotificationArgs;->toBundle()Landroid/os/Bundle;

    move-result-object v0

    .line 98
    .local v0, "args":Landroid/os/Bundle;
    iget-object v1, p0, Landroidx/browser/trusted/TrustedWebActivityServiceConnection;->mService:Landroid/support/customtabs/trusted/ITrustedWebActivityService;

    invoke-interface {v1, v0}, Landroid/support/customtabs/trusted/ITrustedWebActivityService;->notifyNotificationWithChannel(Landroid/os/Bundle;)Landroid/os/Bundle;

    move-result-object v1

    invoke-static {v1}, Landroidx/browser/trusted/TrustedWebActivityServiceConnection$ResultArgs;->fromBundle(Landroid/os/Bundle;)Landroidx/browser/trusted/TrustedWebActivityServiceConnection$ResultArgs;

    move-result-object v1

    iget-boolean v1, v1, Landroidx/browser/trusted/TrustedWebActivityServiceConnection$ResultArgs;->success:Z

    return v1
.end method

.method public sendExtraCommand(Ljava/lang/String;Landroid/os/Bundle;Landroidx/browser/trusted/TrustedWebActivityCallback;)Landroid/os/Bundle;
    .locals 3
    .param p1, "commandName"    # Ljava/lang/String;
    .param p2, "args"    # Landroid/os/Bundle;
    .param p3, "callback"    # Landroidx/browser/trusted/TrustedWebActivityCallback;
    .annotation system Ldalvik/annotation/MethodParameters;
        accessFlags = {
            0x0,
            0x0,
            0x0
        }
        names = {
            "commandName",
            "args",
            "callback"
        }
    .end annotation

    .annotation system Ldalvik/annotation/Throws;
        value = {
            Landroid/os/RemoteException;
        }
    .end annotation

    .line 182
    invoke-static {p3}, Landroidx/browser/trusted/TrustedWebActivityServiceConnection;->wrapCallback(Landroidx/browser/trusted/TrustedWebActivityCallback;)Landroid/support/customtabs/trusted/ITrustedWebActivityCallback;

    move-result-object v0

    .line 183
    .local v0, "callbackBinder":Landroid/support/customtabs/trusted/ITrustedWebActivityCallback;
    if-nez v0, :cond_0

    const/4 v1, 0x0

    goto :goto_0

    :cond_0
    invoke-interface {v0}, Landroid/support/customtabs/trusted/ITrustedWebActivityCallback;->asBinder()Landroid/os/IBinder;

    move-result-object v1

    .line 184
    .local v1, "binder":Landroid/os/IBinder;
    :goto_0
    iget-object v2, p0, Landroidx/browser/trusted/TrustedWebActivityServiceConnection;->mService:Landroid/support/customtabs/trusted/ITrustedWebActivityService;

    invoke-interface {v2, p1, p2, v1}, Landroid/support/customtabs/trusted/ITrustedWebActivityService;->extraCommand(Ljava/lang/String;Landroid/os/Bundle;Landroid/os/IBinder;)Landroid/os/Bundle;

    move-result-object v2

    return-object v2
.end method
