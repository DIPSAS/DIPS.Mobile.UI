.class public Landroidx/browser/customtabs/TrustedWebUtils;
.super Ljava/lang/Object;
.source "TrustedWebUtils.java"


# static fields
.field public static final ACTION_MANAGE_TRUSTED_WEB_ACTIVITY_DATA:Ljava/lang/String; = "android.support.customtabs.action.ACTION_MANAGE_TRUSTED_WEB_ACTIVITY_DATA"

.field public static final EXTRA_LAUNCH_AS_TRUSTED_WEB_ACTIVITY:Ljava/lang/String; = "android.support.customtabs.extra.LAUNCH_AS_TRUSTED_WEB_ACTIVITY"


# direct methods
.method private constructor <init>()V
    .locals 0

    .line 76
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method

.method public static areSplashScreensSupported(Landroid/content/Context;Ljava/lang/String;Ljava/lang/String;)Z
    .locals 3
    .param p0, "context"    # Landroid/content/Context;
    .param p1, "packageName"    # Ljava/lang/String;
    .param p2, "version"    # Ljava/lang/String;
    .annotation system Ldalvik/annotation/MethodParameters;
        accessFlags = {
            0x0,
            0x0,
            0x0
        }
        names = {
            "context",
            "packageName",
            "version"
        }
    .end annotation

    .line 124
    new-instance v0, Landroid/content/Intent;

    invoke-direct {v0}, Landroid/content/Intent;-><init>()V

    .line 125
    const-string v1, "android.support.customtabs.action.CustomTabsService"

    invoke-virtual {v0, v1}, Landroid/content/Intent;->setAction(Ljava/lang/String;)Landroid/content/Intent;

    move-result-object v0

    .line 126
    invoke-virtual {v0, p1}, Landroid/content/Intent;->setPackage(Ljava/lang/String;)Landroid/content/Intent;

    move-result-object v0

    .line 127
    .local v0, "serviceIntent":Landroid/content/Intent;
    invoke-virtual {p0}, Landroid/content/Context;->getPackageManager()Landroid/content/pm/PackageManager;

    move-result-object v1

    .line 128
    const/16 v2, 0x40

    invoke-virtual {v1, v0, v2}, Landroid/content/pm/PackageManager;->resolveService(Landroid/content/Intent;I)Landroid/content/pm/ResolveInfo;

    move-result-object v1

    .line 129
    .local v1, "resolveInfo":Landroid/content/pm/ResolveInfo;
    if-eqz v1, :cond_1

    iget-object v2, v1, Landroid/content/pm/ResolveInfo;->filter:Landroid/content/IntentFilter;

    if-nez v2, :cond_0

    goto :goto_0

    .line 130
    :cond_0
    iget-object v2, v1, Landroid/content/pm/ResolveInfo;->filter:Landroid/content/IntentFilter;

    invoke-virtual {v2, p2}, Landroid/content/IntentFilter;->hasCategory(Ljava/lang/String;)Z

    move-result v2

    return v2

    .line 129
    :cond_1
    :goto_0
    const/4 v2, 0x0

    return v2
.end method

.method public static launchAsTrustedWebActivity(Landroid/content/Context;Landroidx/browser/customtabs/CustomTabsIntent;Landroid/net/Uri;)V
    .locals 3
    .param p0, "context"    # Landroid/content/Context;
    .param p1, "customTabsIntent"    # Landroidx/browser/customtabs/CustomTabsIntent;
    .param p2, "uri"    # Landroid/net/Uri;
    .annotation system Ldalvik/annotation/MethodParameters;
        accessFlags = {
            0x0,
            0x0,
            0x0
        }
        names = {
            "context",
            "customTabsIntent",
            "uri"
        }
    .end annotation

    .annotation runtime Ljava/lang/Deprecated;
    .end annotation

    .line 177
    iget-object v0, p1, Landroidx/browser/customtabs/CustomTabsIntent;->intent:Landroid/content/Intent;

    .line 178
    invoke-virtual {v0}, Landroid/content/Intent;->getExtras()Landroid/os/Bundle;

    move-result-object v0

    .line 177
    const-string v1, "android.support.customtabs.extra.SESSION"

    invoke-static {v0, v1}, Landroidx/core/app/BundleCompat;->getBinder(Landroid/os/Bundle;Ljava/lang/String;)Landroid/os/IBinder;

    move-result-object v0

    if-eqz v0, :cond_0

    .line 182
    iget-object v0, p1, Landroidx/browser/customtabs/CustomTabsIntent;->intent:Landroid/content/Intent;

    const/4 v1, 0x1

    const-string v2, "android.support.customtabs.extra.LAUNCH_AS_TRUSTED_WEB_ACTIVITY"

    invoke-virtual {v0, v2, v1}, Landroid/content/Intent;->putExtra(Ljava/lang/String;Z)Landroid/content/Intent;

    .line 183
    invoke-virtual {p1, p0, p2}, Landroidx/browser/customtabs/CustomTabsIntent;->launchUrl(Landroid/content/Context;Landroid/net/Uri;)V

    .line 184
    return-void

    .line 179
    :cond_0
    new-instance v0, Ljava/lang/IllegalArgumentException;

    const-string v1, "Given CustomTabsIntent should be associated with a valid CustomTabsSession"

    invoke-direct {v0, v1}, Ljava/lang/IllegalArgumentException;-><init>(Ljava/lang/String;)V

    throw v0
.end method

.method public static launchBrowserSiteSettings(Landroid/content/Context;Landroidx/browser/customtabs/CustomTabsSession;Landroid/net/Uri;)V
    .locals 4
    .param p0, "context"    # Landroid/content/Context;
    .param p1, "session"    # Landroidx/browser/customtabs/CustomTabsSession;
    .param p2, "uri"    # Landroid/net/Uri;
    .annotation system Ldalvik/annotation/MethodParameters;
        accessFlags = {
            0x0,
            0x0,
            0x0
        }
        names = {
            "context",
            "session",
            "uri"
        }
    .end annotation

    .line 96
    new-instance v0, Landroid/content/Intent;

    const-string v1, "android.support.customtabs.action.ACTION_MANAGE_TRUSTED_WEB_ACTIVITY_DATA"

    invoke-direct {v0, v1}, Landroid/content/Intent;-><init>(Ljava/lang/String;)V

    .line 97
    .local v0, "intent":Landroid/content/Intent;
    invoke-virtual {p1}, Landroidx/browser/customtabs/CustomTabsSession;->getComponentName()Landroid/content/ComponentName;

    move-result-object v1

    invoke-virtual {v1}, Landroid/content/ComponentName;->getPackageName()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Landroid/content/Intent;->setPackage(Ljava/lang/String;)Landroid/content/Intent;

    .line 98
    invoke-virtual {v0, p2}, Landroid/content/Intent;->setData(Landroid/net/Uri;)Landroid/content/Intent;

    .line 100
    new-instance v1, Landroid/os/Bundle;

    invoke-direct {v1}, Landroid/os/Bundle;-><init>()V

    .line 101
    .local v1, "bundle":Landroid/os/Bundle;
    invoke-virtual {p1}, Landroidx/browser/customtabs/CustomTabsSession;->getBinder()Landroid/os/IBinder;

    move-result-object v2

    const-string v3, "android.support.customtabs.extra.SESSION"

    invoke-static {v1, v3, v2}, Landroidx/core/app/BundleCompat;->putBinder(Landroid/os/Bundle;Ljava/lang/String;Landroid/os/IBinder;)V

    .line 102
    invoke-virtual {v0, v1}, Landroid/content/Intent;->putExtras(Landroid/os/Bundle;)Landroid/content/Intent;

    .line 103
    invoke-virtual {p1}, Landroidx/browser/customtabs/CustomTabsSession;->getId()Landroid/app/PendingIntent;

    move-result-object v2

    .line 104
    .local v2, "id":Landroid/app/PendingIntent;
    if-eqz v2, :cond_0

    .line 105
    const-string v3, "android.support.customtabs.extra.SESSION_ID"

    invoke-virtual {v0, v3, v2}, Landroid/content/Intent;->putExtra(Ljava/lang/String;Landroid/os/Parcelable;)Landroid/content/Intent;

    .line 107
    :cond_0
    invoke-virtual {p0, v0}, Landroid/content/Context;->startActivity(Landroid/content/Intent;)V

    .line 108
    return-void
.end method

.method public static transferSplashImage(Landroid/content/Context;Ljava/io/File;Ljava/lang/String;Ljava/lang/String;Landroidx/browser/customtabs/CustomTabsSession;)Z
    .locals 3
    .param p0, "context"    # Landroid/content/Context;
    .param p1, "file"    # Ljava/io/File;
    .param p2, "fileProviderAuthority"    # Ljava/lang/String;
    .param p3, "packageName"    # Ljava/lang/String;
    .param p4, "session"    # Landroidx/browser/customtabs/CustomTabsSession;
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
            "file",
            "fileProviderAuthority",
            "packageName",
            "session"
        }
    .end annotation

    .line 153
    invoke-static {p0, p2, p1}, Landroidx/core/content/FileProvider;->getUriForFile(Landroid/content/Context;Ljava/lang/String;Ljava/io/File;)Landroid/net/Uri;

    move-result-object v0

    .line 154
    .local v0, "uri":Landroid/net/Uri;
    const/4 v1, 0x1

    invoke-virtual {p0, p3, v0, v1}, Landroid/content/Context;->grantUriPermission(Ljava/lang/String;Landroid/net/Uri;I)V

    .line 155
    const/4 v2, 0x0

    invoke-virtual {p4, v0, v1, v2}, Landroidx/browser/customtabs/CustomTabsSession;->receiveFile(Landroid/net/Uri;ILandroid/os/Bundle;)Z

    move-result v1

    return v1
.end method
