.class Landroidx/browser/trusted/NotificationApiHelperForO;
.super Ljava/lang/Object;
.source "NotificationApiHelperForO.java"


# direct methods
.method private constructor <init>()V
    .locals 0

    .line 66
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method

.method static copyNotificationOntoChannel(Landroid/content/Context;Landroid/app/NotificationManager;Landroid/app/Notification;Ljava/lang/String;Ljava/lang/String;)Landroid/app/Notification;
    .locals 2
    .param p0, "context"    # Landroid/content/Context;
    .param p1, "manager"    # Landroid/app/NotificationManager;
    .param p2, "notification"    # Landroid/app/Notification;
    .param p3, "channelId"    # Ljava/lang/String;
    .param p4, "channelName"    # Ljava/lang/String;
    .annotation system Ldalvik/annotation/MethodParameters;
        accessFlags = {
            0x0,
            0x0,
            0x0,
            0x0,
            0x0
        }
        names = {
            "context",
            "manager",
            "notification",
            "channelId",
            "channelName"
        }
    .end annotation

    .line 51
    new-instance v0, Landroid/app/NotificationChannel;

    const/4 v1, 0x3

    invoke-direct {v0, p3, p4, v1}, Landroid/app/NotificationChannel;-><init>(Ljava/lang/String;Ljava/lang/CharSequence;I)V

    invoke-virtual {p1, v0}, Landroid/app/NotificationManager;->createNotificationChannel(Landroid/app/NotificationChannel;)V

    .line 55
    invoke-virtual {p1, p3}, Landroid/app/NotificationManager;->getNotificationChannel(Ljava/lang/String;)Landroid/app/NotificationChannel;

    move-result-object v0

    invoke-virtual {v0}, Landroid/app/NotificationChannel;->getImportance()I

    move-result v0

    if-nez v0, :cond_0

    .line 57
    const/4 v0, 0x0

    return-object v0

    .line 61
    :cond_0
    invoke-static {p0, p2}, Landroid/app/Notification$Builder;->recoverBuilder(Landroid/content/Context;Landroid/app/Notification;)Landroid/app/Notification$Builder;

    move-result-object v0

    .line 62
    .local v0, "builder":Landroid/app/Notification$Builder;
    invoke-virtual {v0, p3}, Landroid/app/Notification$Builder;->setChannelId(Ljava/lang/String;)Landroid/app/Notification$Builder;

    .line 63
    invoke-virtual {v0}, Landroid/app/Notification$Builder;->build()Landroid/app/Notification;

    move-result-object v1

    return-object v1
.end method

.method static isChannelEnabled(Landroid/app/NotificationManager;Ljava/lang/String;)Z
    .locals 2
    .param p0, "manager"    # Landroid/app/NotificationManager;
    .param p1, "channelId"    # Ljava/lang/String;
    .annotation system Ldalvik/annotation/MethodParameters;
        accessFlags = {
            0x0,
            0x0
        }
        names = {
            "manager",
            "channelId"
        }
    .end annotation

    .line 41
    invoke-virtual {p0, p1}, Landroid/app/NotificationManager;->getNotificationChannel(Ljava/lang/String;)Landroid/app/NotificationChannel;

    move-result-object v0

    .line 43
    .local v0, "channel":Landroid/app/NotificationChannel;
    if-eqz v0, :cond_1

    invoke-virtual {v0}, Landroid/app/NotificationChannel;->getImportance()I

    move-result v1

    if-eqz v1, :cond_0

    goto :goto_0

    :cond_0
    const/4 v1, 0x0

    goto :goto_1

    :cond_1
    :goto_0
    const/4 v1, 0x1

    :goto_1
    return v1
.end method
