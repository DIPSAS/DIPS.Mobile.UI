.class Landroidx/browser/trusted/TrustedWebActivityService$1;
.super Landroid/support/customtabs/trusted/ITrustedWebActivityService$Stub;
.source "TrustedWebActivityService.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Landroidx/browser/trusted/TrustedWebActivityService;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Landroidx/browser/trusted/TrustedWebActivityService;


# direct methods
.method constructor <init>(Landroidx/browser/trusted/TrustedWebActivityService;)V
    .locals 0
    .param p1, "this$0"    # Landroidx/browser/trusted/TrustedWebActivityService;
    .annotation system Ldalvik/annotation/MethodParameters;
        accessFlags = {
            0x8010
        }
        names = {
            "this$0"
        }
    .end annotation

    .line 128
    iput-object p1, p0, Landroidx/browser/trusted/TrustedWebActivityService$1;->this$0:Landroidx/browser/trusted/TrustedWebActivityService;

    invoke-direct {p0}, Landroid/support/customtabs/trusted/ITrustedWebActivityService$Stub;-><init>()V

    return-void
.end method

.method private checkCaller()V
    .locals 7

    .line 193
    iget-object v0, p0, Landroidx/browser/trusted/TrustedWebActivityService$1;->this$0:Landroidx/browser/trusted/TrustedWebActivityService;

    iget v0, v0, Landroidx/browser/trusted/TrustedWebActivityService;->mVerifiedUid:I

    const/4 v1, -0x1

    if-ne v0, v1, :cond_2

    .line 194
    iget-object v0, p0, Landroidx/browser/trusted/TrustedWebActivityService$1;->this$0:Landroidx/browser/trusted/TrustedWebActivityService;

    invoke-virtual {v0}, Landroidx/browser/trusted/TrustedWebActivityService;->getPackageManager()Landroid/content/pm/PackageManager;

    move-result-object v0

    invoke-static {}, Landroidx/browser/trusted/TrustedWebActivityService$1;->getCallingUid()I

    move-result v1

    invoke-virtual {v0, v1}, Landroid/content/pm/PackageManager;->getPackagesForUid(I)[Ljava/lang/String;

    move-result-object v0

    .line 196
    .local v0, "packages":[Ljava/lang/String;
    const/4 v1, 0x0

    if-nez v0, :cond_0

    .line 197
    new-array v0, v1, [Ljava/lang/String;

    .line 200
    :cond_0
    iget-object v2, p0, Landroidx/browser/trusted/TrustedWebActivityService$1;->this$0:Landroidx/browser/trusted/TrustedWebActivityService;

    invoke-virtual {v2}, Landroidx/browser/trusted/TrustedWebActivityService;->getTokenStore()Landroidx/browser/trusted/TokenStore;

    move-result-object v2

    invoke-interface {v2}, Landroidx/browser/trusted/TokenStore;->load()Landroidx/browser/trusted/Token;

    move-result-object v2

    .line 201
    .local v2, "verifiedProvider":Landroidx/browser/trusted/Token;
    iget-object v3, p0, Landroidx/browser/trusted/TrustedWebActivityService$1;->this$0:Landroidx/browser/trusted/TrustedWebActivityService;

    invoke-virtual {v3}, Landroidx/browser/trusted/TrustedWebActivityService;->getPackageManager()Landroid/content/pm/PackageManager;

    move-result-object v3

    .line 203
    .local v3, "pm":Landroid/content/pm/PackageManager;
    if-eqz v2, :cond_2

    .line 204
    array-length v4, v0

    :goto_0
    if-ge v1, v4, :cond_2

    aget-object v5, v0, v1

    .line 205
    .local v5, "packageName":Ljava/lang/String;
    invoke-virtual {v2, v5, v3}, Landroidx/browser/trusted/Token;->matches(Ljava/lang/String;Landroid/content/pm/PackageManager;)Z

    move-result v6

    if-eqz v6, :cond_1

    .line 206
    iget-object v1, p0, Landroidx/browser/trusted/TrustedWebActivityService$1;->this$0:Landroidx/browser/trusted/TrustedWebActivityService;

    invoke-static {}, Landroidx/browser/trusted/TrustedWebActivityService$1;->getCallingUid()I

    move-result v4

    iput v4, v1, Landroidx/browser/trusted/TrustedWebActivityService;->mVerifiedUid:I

    .line 207
    goto :goto_1

    .line 204
    .end local v5    # "packageName":Ljava/lang/String;
    :cond_1
    add-int/lit8 v1, v1, 0x1

    goto :goto_0

    .line 213
    .end local v0    # "packages":[Ljava/lang/String;
    .end local v2    # "verifiedProvider":Landroidx/browser/trusted/Token;
    .end local v3    # "pm":Landroid/content/pm/PackageManager;
    :cond_2
    :goto_1
    iget-object v0, p0, Landroidx/browser/trusted/TrustedWebActivityService$1;->this$0:Landroidx/browser/trusted/TrustedWebActivityService;

    iget v0, v0, Landroidx/browser/trusted/TrustedWebActivityService;->mVerifiedUid:I

    invoke-static {}, Landroidx/browser/trusted/TrustedWebActivityService$1;->getCallingUid()I

    move-result v1

    if-ne v0, v1, :cond_3

    return-void

    .line 215
    :cond_3
    new-instance v0, Ljava/lang/SecurityException;

    const-string v1, "Caller is not verified as Trusted Web Activity provider."

    invoke-direct {v0, v1}, Ljava/lang/SecurityException;-><init>(Ljava/lang/String;)V

    throw v0
.end method


# virtual methods
.method public areNotificationsEnabled(Landroid/os/Bundle;)Landroid/os/Bundle;
    .locals 3
    .param p1, "bundle"    # Landroid/os/Bundle;
    .annotation system Ldalvik/annotation/MethodParameters;
        accessFlags = {
            0x0
        }
        names = {
            "bundle"
        }
    .end annotation

    .line 131
    invoke-direct {p0}, Landroidx/browser/trusted/TrustedWebActivityService$1;->checkCaller()V

    .line 133
    invoke-static {p1}, Landroidx/browser/trusted/TrustedWebActivityServiceConnection$NotificationsEnabledArgs;->fromBundle(Landroid/os/Bundle;)Landroidx/browser/trusted/TrustedWebActivityServiceConnection$NotificationsEnabledArgs;

    move-result-object v0

    .line 134
    .local v0, "args":Landroidx/browser/trusted/TrustedWebActivityServiceConnection$NotificationsEnabledArgs;
    iget-object v1, p0, Landroidx/browser/trusted/TrustedWebActivityService$1;->this$0:Landroidx/browser/trusted/TrustedWebActivityService;

    iget-object v2, v0, Landroidx/browser/trusted/TrustedWebActivityServiceConnection$NotificationsEnabledArgs;->channelName:Ljava/lang/String;

    .line 135
    invoke-virtual {v1, v2}, Landroidx/browser/trusted/TrustedWebActivityService;->onAreNotificationsEnabled(Ljava/lang/String;)Z

    move-result v1

    .line 137
    .local v1, "result":Z
    new-instance v2, Landroidx/browser/trusted/TrustedWebActivityServiceConnection$ResultArgs;

    invoke-direct {v2, v1}, Landroidx/browser/trusted/TrustedWebActivityServiceConnection$ResultArgs;-><init>(Z)V

    invoke-virtual {v2}, Landroidx/browser/trusted/TrustedWebActivityServiceConnection$ResultArgs;->toBundle()Landroid/os/Bundle;

    move-result-object v2

    return-object v2
.end method

.method public cancelNotification(Landroid/os/Bundle;)V
    .locals 4
    .param p1, "bundle"    # Landroid/os/Bundle;
    .annotation system Ldalvik/annotation/MethodParameters;
        accessFlags = {
            0x0
        }
        names = {
            "bundle"
        }
    .end annotation

    .line 154
    invoke-direct {p0}, Landroidx/browser/trusted/TrustedWebActivityService$1;->checkCaller()V

    .line 156
    invoke-static {p1}, Landroidx/browser/trusted/TrustedWebActivityServiceConnection$CancelNotificationArgs;->fromBundle(Landroid/os/Bundle;)Landroidx/browser/trusted/TrustedWebActivityServiceConnection$CancelNotificationArgs;

    move-result-object v0

    .line 158
    .local v0, "args":Landroidx/browser/trusted/TrustedWebActivityServiceConnection$CancelNotificationArgs;
    iget-object v1, p0, Landroidx/browser/trusted/TrustedWebActivityService$1;->this$0:Landroidx/browser/trusted/TrustedWebActivityService;

    iget-object v2, v0, Landroidx/browser/trusted/TrustedWebActivityServiceConnection$CancelNotificationArgs;->platformTag:Ljava/lang/String;

    iget v3, v0, Landroidx/browser/trusted/TrustedWebActivityServiceConnection$CancelNotificationArgs;->platformId:I

    invoke-virtual {v1, v2, v3}, Landroidx/browser/trusted/TrustedWebActivityService;->onCancelNotification(Ljava/lang/String;I)V

    .line 159
    return-void
.end method

.method public extraCommand(Ljava/lang/String;Landroid/os/Bundle;Landroid/os/IBinder;)Landroid/os/Bundle;
    .locals 2
    .param p1, "commandName"    # Ljava/lang/String;
    .param p2, "args"    # Landroid/os/Bundle;
    .param p3, "callback"    # Landroid/os/IBinder;
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

    .line 186
    invoke-direct {p0}, Landroidx/browser/trusted/TrustedWebActivityService$1;->checkCaller()V

    .line 188
    iget-object v0, p0, Landroidx/browser/trusted/TrustedWebActivityService$1;->this$0:Landroidx/browser/trusted/TrustedWebActivityService;

    .line 189
    invoke-static {p3}, Landroidx/browser/trusted/TrustedWebActivityCallbackRemote;->fromBinder(Landroid/os/IBinder;)Landroidx/browser/trusted/TrustedWebActivityCallbackRemote;

    move-result-object v1

    .line 188
    invoke-virtual {v0, p1, p2, v1}, Landroidx/browser/trusted/TrustedWebActivityService;->onExtraCommand(Ljava/lang/String;Landroid/os/Bundle;Landroidx/browser/trusted/TrustedWebActivityCallbackRemote;)Landroid/os/Bundle;

    move-result-object v0

    return-object v0
.end method

.method public getActiveNotifications()Landroid/os/Bundle;
    .locals 2

    .line 163
    invoke-direct {p0}, Landroidx/browser/trusted/TrustedWebActivityService$1;->checkCaller()V

    .line 165
    new-instance v0, Landroidx/browser/trusted/TrustedWebActivityServiceConnection$ActiveNotificationsArgs;

    iget-object v1, p0, Landroidx/browser/trusted/TrustedWebActivityService$1;->this$0:Landroidx/browser/trusted/TrustedWebActivityService;

    .line 166
    invoke-virtual {v1}, Landroidx/browser/trusted/TrustedWebActivityService;->onGetActiveNotifications()[Landroid/os/Parcelable;

    move-result-object v1

    invoke-direct {v0, v1}, Landroidx/browser/trusted/TrustedWebActivityServiceConnection$ActiveNotificationsArgs;-><init>([Landroid/os/Parcelable;)V

    invoke-virtual {v0}, Landroidx/browser/trusted/TrustedWebActivityServiceConnection$ActiveNotificationsArgs;->toBundle()Landroid/os/Bundle;

    move-result-object v0

    .line 165
    return-object v0
.end method

.method public getSmallIconBitmap()Landroid/os/Bundle;
    .locals 1

    .line 178
    invoke-direct {p0}, Landroidx/browser/trusted/TrustedWebActivityService$1;->checkCaller()V

    .line 180
    iget-object v0, p0, Landroidx/browser/trusted/TrustedWebActivityService$1;->this$0:Landroidx/browser/trusted/TrustedWebActivityService;

    invoke-virtual {v0}, Landroidx/browser/trusted/TrustedWebActivityService;->onGetSmallIconBitmap()Landroid/os/Bundle;

    move-result-object v0

    return-object v0
.end method

.method public getSmallIconId()I
    .locals 1

    .line 171
    invoke-direct {p0}, Landroidx/browser/trusted/TrustedWebActivityService$1;->checkCaller()V

    .line 173
    iget-object v0, p0, Landroidx/browser/trusted/TrustedWebActivityService$1;->this$0:Landroidx/browser/trusted/TrustedWebActivityService;

    invoke-virtual {v0}, Landroidx/browser/trusted/TrustedWebActivityService;->onGetSmallIconId()I

    move-result v0

    return v0
.end method

.method public notifyNotificationWithChannel(Landroid/os/Bundle;)Landroid/os/Bundle;
    .locals 6
    .param p1, "bundle"    # Landroid/os/Bundle;
    .annotation system Ldalvik/annotation/MethodParameters;
        accessFlags = {
            0x0
        }
        names = {
            "bundle"
        }
    .end annotation

    .line 142
    invoke-direct {p0}, Landroidx/browser/trusted/TrustedWebActivityService$1;->checkCaller()V

    .line 144
    invoke-static {p1}, Landroidx/browser/trusted/TrustedWebActivityServiceConnection$NotifyNotificationArgs;->fromBundle(Landroid/os/Bundle;)Landroidx/browser/trusted/TrustedWebActivityServiceConnection$NotifyNotificationArgs;

    move-result-object v0

    .line 146
    .local v0, "args":Landroidx/browser/trusted/TrustedWebActivityServiceConnection$NotifyNotificationArgs;
    iget-object v1, p0, Landroidx/browser/trusted/TrustedWebActivityService$1;->this$0:Landroidx/browser/trusted/TrustedWebActivityService;

    iget-object v2, v0, Landroidx/browser/trusted/TrustedWebActivityServiceConnection$NotifyNotificationArgs;->platformTag:Ljava/lang/String;

    iget v3, v0, Landroidx/browser/trusted/TrustedWebActivityServiceConnection$NotifyNotificationArgs;->platformId:I

    iget-object v4, v0, Landroidx/browser/trusted/TrustedWebActivityServiceConnection$NotifyNotificationArgs;->notification:Landroid/app/Notification;

    iget-object v5, v0, Landroidx/browser/trusted/TrustedWebActivityServiceConnection$NotifyNotificationArgs;->channelName:Ljava/lang/String;

    invoke-virtual {v1, v2, v3, v4, v5}, Landroidx/browser/trusted/TrustedWebActivityService;->onNotifyNotificationWithChannel(Ljava/lang/String;ILandroid/app/Notification;Ljava/lang/String;)Z

    move-result v1

    .line 149
    .local v1, "success":Z
    new-instance v2, Landroidx/browser/trusted/TrustedWebActivityServiceConnection$ResultArgs;

    invoke-direct {v2, v1}, Landroidx/browser/trusted/TrustedWebActivityServiceConnection$ResultArgs;-><init>(Z)V

    invoke-virtual {v2}, Landroidx/browser/trusted/TrustedWebActivityServiceConnection$ResultArgs;->toBundle()Landroid/os/Bundle;

    move-result-object v2

    return-object v2
.end method
