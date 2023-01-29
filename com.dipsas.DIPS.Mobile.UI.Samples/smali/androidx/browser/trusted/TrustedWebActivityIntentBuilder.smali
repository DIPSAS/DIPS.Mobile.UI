.class public Landroidx/browser/trusted/TrustedWebActivityIntentBuilder;
.super Ljava/lang/Object;
.source "TrustedWebActivityIntentBuilder.java"


# static fields
.field public static final EXTRA_ADDITIONAL_TRUSTED_ORIGINS:Ljava/lang/String; = "android.support.customtabs.extra.ADDITIONAL_TRUSTED_ORIGINS"

.field public static final EXTRA_DISPLAY_MODE:Ljava/lang/String; = "androidx.browser.trusted.extra.DISPLAY_MODE"

.field public static final EXTRA_SCREEN_ORIENTATION:Ljava/lang/String; = "androidx.browser.trusted.extra.SCREEN_ORIENTATION"

.field public static final EXTRA_SHARE_DATA:Ljava/lang/String; = "androidx.browser.trusted.extra.SHARE_DATA"

.field public static final EXTRA_SHARE_TARGET:Ljava/lang/String; = "androidx.browser.trusted.extra.SHARE_TARGET"

.field public static final EXTRA_SPLASH_SCREEN_PARAMS:Ljava/lang/String; = "androidx.browser.trusted.EXTRA_SPLASH_SCREEN_PARAMS"


# instance fields
.field private mAdditionalTrustedOrigins:Ljava/util/List;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/List<",
            "Ljava/lang/String;",
            ">;"
        }
    .end annotation
.end field

.field private mDisplayMode:Landroidx/browser/trusted/TrustedWebActivityDisplayMode;

.field private final mIntentBuilder:Landroidx/browser/customtabs/CustomTabsIntent$Builder;

.field private mScreenOrientation:I

.field private mShareData:Landroidx/browser/trusted/sharing/ShareData;

.field private mShareTarget:Landroidx/browser/trusted/sharing/ShareTarget;

.field private mSplashScreenParams:Landroid/os/Bundle;

.field private final mUri:Landroid/net/Uri;


# direct methods
.method public constructor <init>(Landroid/net/Uri;)V
    .locals 1
    .param p1, "uri"    # Landroid/net/Uri;
    .annotation system Ldalvik/annotation/MethodParameters;
        accessFlags = {
            0x0
        }
        names = {
            "uri"
        }
    .end annotation

    .line 106
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 81
    new-instance v0, Landroidx/browser/customtabs/CustomTabsIntent$Builder;

    invoke-direct {v0}, Landroidx/browser/customtabs/CustomTabsIntent$Builder;-><init>()V

    iput-object v0, p0, Landroidx/browser/trusted/TrustedWebActivityIntentBuilder;->mIntentBuilder:Landroidx/browser/customtabs/CustomTabsIntent$Builder;

    .line 94
    new-instance v0, Landroidx/browser/trusted/TrustedWebActivityDisplayMode$DefaultMode;

    invoke-direct {v0}, Landroidx/browser/trusted/TrustedWebActivityDisplayMode$DefaultMode;-><init>()V

    iput-object v0, p0, Landroidx/browser/trusted/TrustedWebActivityIntentBuilder;->mDisplayMode:Landroidx/browser/trusted/TrustedWebActivityDisplayMode;

    .line 98
    const/4 v0, 0x0

    iput v0, p0, Landroidx/browser/trusted/TrustedWebActivityIntentBuilder;->mScreenOrientation:I

    .line 107
    iput-object p1, p0, Landroidx/browser/trusted/TrustedWebActivityIntentBuilder;->mUri:Landroid/net/Uri;

    .line 108
    return-void
.end method


# virtual methods
.method public build(Landroidx/browser/customtabs/CustomTabsSession;)Landroidx/browser/trusted/TrustedWebActivityIntent;
    .locals 4
    .param p1, "session"    # Landroidx/browser/customtabs/CustomTabsSession;
    .annotation system Ldalvik/annotation/MethodParameters;
        accessFlags = {
            0x0
        }
        names = {
            "session"
        }
    .end annotation

    .line 283
    if-eqz p1, :cond_3

    .line 287
    iget-object v0, p0, Landroidx/browser/trusted/TrustedWebActivityIntentBuilder;->mIntentBuilder:Landroidx/browser/customtabs/CustomTabsIntent$Builder;

    invoke-virtual {v0, p1}, Landroidx/browser/customtabs/CustomTabsIntent$Builder;->setSession(Landroidx/browser/customtabs/CustomTabsSession;)Landroidx/browser/customtabs/CustomTabsIntent$Builder;

    .line 288
    iget-object v0, p0, Landroidx/browser/trusted/TrustedWebActivityIntentBuilder;->mIntentBuilder:Landroidx/browser/customtabs/CustomTabsIntent$Builder;

    invoke-virtual {v0}, Landroidx/browser/customtabs/CustomTabsIntent$Builder;->build()Landroidx/browser/customtabs/CustomTabsIntent;

    move-result-object v0

    iget-object v0, v0, Landroidx/browser/customtabs/CustomTabsIntent;->intent:Landroid/content/Intent;

    .line 289
    .local v0, "intent":Landroid/content/Intent;
    iget-object v1, p0, Landroidx/browser/trusted/TrustedWebActivityIntentBuilder;->mUri:Landroid/net/Uri;

    invoke-virtual {v0, v1}, Landroid/content/Intent;->setData(Landroid/net/Uri;)Landroid/content/Intent;

    .line 290
    const/4 v1, 0x1

    const-string v2, "android.support.customtabs.extra.LAUNCH_AS_TRUSTED_WEB_ACTIVITY"

    invoke-virtual {v0, v2, v1}, Landroid/content/Intent;->putExtra(Ljava/lang/String;Z)Landroid/content/Intent;

    .line 291
    iget-object v1, p0, Landroidx/browser/trusted/TrustedWebActivityIntentBuilder;->mAdditionalTrustedOrigins:Ljava/util/List;

    if-eqz v1, :cond_0

    .line 292
    new-instance v1, Ljava/util/ArrayList;

    iget-object v2, p0, Landroidx/browser/trusted/TrustedWebActivityIntentBuilder;->mAdditionalTrustedOrigins:Ljava/util/List;

    invoke-direct {v1, v2}, Ljava/util/ArrayList;-><init>(Ljava/util/Collection;)V

    const-string v2, "android.support.customtabs.extra.ADDITIONAL_TRUSTED_ORIGINS"

    invoke-virtual {v0, v2, v1}, Landroid/content/Intent;->putExtra(Ljava/lang/String;Ljava/io/Serializable;)Landroid/content/Intent;

    .line 296
    :cond_0
    iget-object v1, p0, Landroidx/browser/trusted/TrustedWebActivityIntentBuilder;->mSplashScreenParams:Landroid/os/Bundle;

    if-eqz v1, :cond_1

    .line 297
    const-string v2, "androidx.browser.trusted.EXTRA_SPLASH_SCREEN_PARAMS"

    invoke-virtual {v0, v2, v1}, Landroid/content/Intent;->putExtra(Ljava/lang/String;Landroid/os/Bundle;)Landroid/content/Intent;

    .line 299
    :cond_1
    invoke-static {}, Ljava/util/Collections;->emptyList()Ljava/util/List;

    move-result-object v1

    .line 300
    .local v1, "sharedUris":Ljava/util/List;, "Ljava/util/List<Landroid/net/Uri;>;"
    iget-object v2, p0, Landroidx/browser/trusted/TrustedWebActivityIntentBuilder;->mShareTarget:Landroidx/browser/trusted/sharing/ShareTarget;

    if-eqz v2, :cond_2

    iget-object v3, p0, Landroidx/browser/trusted/TrustedWebActivityIntentBuilder;->mShareData:Landroidx/browser/trusted/sharing/ShareData;

    if-eqz v3, :cond_2

    .line 301
    invoke-virtual {v2}, Landroidx/browser/trusted/sharing/ShareTarget;->toBundle()Landroid/os/Bundle;

    move-result-object v2

    const-string v3, "androidx.browser.trusted.extra.SHARE_TARGET"

    invoke-virtual {v0, v3, v2}, Landroid/content/Intent;->putExtra(Ljava/lang/String;Landroid/os/Bundle;)Landroid/content/Intent;

    .line 302
    iget-object v2, p0, Landroidx/browser/trusted/TrustedWebActivityIntentBuilder;->mShareData:Landroidx/browser/trusted/sharing/ShareData;

    invoke-virtual {v2}, Landroidx/browser/trusted/sharing/ShareData;->toBundle()Landroid/os/Bundle;

    move-result-object v2

    const-string v3, "androidx.browser.trusted.extra.SHARE_DATA"

    invoke-virtual {v0, v3, v2}, Landroid/content/Intent;->putExtra(Ljava/lang/String;Landroid/os/Bundle;)Landroid/content/Intent;

    .line 303
    iget-object v2, p0, Landroidx/browser/trusted/TrustedWebActivityIntentBuilder;->mShareData:Landroidx/browser/trusted/sharing/ShareData;

    iget-object v2, v2, Landroidx/browser/trusted/sharing/ShareData;->uris:Ljava/util/List;

    if-eqz v2, :cond_2

    .line 304
    iget-object v2, p0, Landroidx/browser/trusted/TrustedWebActivityIntentBuilder;->mShareData:Landroidx/browser/trusted/sharing/ShareData;

    iget-object v1, v2, Landroidx/browser/trusted/sharing/ShareData;->uris:Ljava/util/List;

    .line 307
    :cond_2
    iget-object v2, p0, Landroidx/browser/trusted/TrustedWebActivityIntentBuilder;->mDisplayMode:Landroidx/browser/trusted/TrustedWebActivityDisplayMode;

    invoke-interface {v2}, Landroidx/browser/trusted/TrustedWebActivityDisplayMode;->toBundle()Landroid/os/Bundle;

    move-result-object v2

    const-string v3, "androidx.browser.trusted.extra.DISPLAY_MODE"

    invoke-virtual {v0, v3, v2}, Landroid/content/Intent;->putExtra(Ljava/lang/String;Landroid/os/Bundle;)Landroid/content/Intent;

    .line 308
    iget v2, p0, Landroidx/browser/trusted/TrustedWebActivityIntentBuilder;->mScreenOrientation:I

    const-string v3, "androidx.browser.trusted.extra.SCREEN_ORIENTATION"

    invoke-virtual {v0, v3, v2}, Landroid/content/Intent;->putExtra(Ljava/lang/String;I)Landroid/content/Intent;

    .line 309
    new-instance v2, Landroidx/browser/trusted/TrustedWebActivityIntent;

    invoke-direct {v2, v0, v1}, Landroidx/browser/trusted/TrustedWebActivityIntent;-><init>(Landroid/content/Intent;Ljava/util/List;)V

    return-object v2

    .line 284
    .end local v0    # "intent":Landroid/content/Intent;
    .end local v1    # "sharedUris":Ljava/util/List;, "Ljava/util/List<Landroid/net/Uri;>;"
    :cond_3
    new-instance v0, Ljava/lang/NullPointerException;

    const-string v1, "CustomTabsSession is required for launching a TWA"

    invoke-direct {v0, v1}, Ljava/lang/NullPointerException;-><init>(Ljava/lang/String;)V

    throw v0
.end method

.method public buildCustomTabsIntent()Landroidx/browser/customtabs/CustomTabsIntent;
    .locals 1

    .line 319
    iget-object v0, p0, Landroidx/browser/trusted/TrustedWebActivityIntentBuilder;->mIntentBuilder:Landroidx/browser/customtabs/CustomTabsIntent$Builder;

    invoke-virtual {v0}, Landroidx/browser/customtabs/CustomTabsIntent$Builder;->build()Landroidx/browser/customtabs/CustomTabsIntent;

    move-result-object v0

    return-object v0
.end method

.method public getDisplayMode()Landroidx/browser/trusted/TrustedWebActivityDisplayMode;
    .locals 1

    .line 335
    iget-object v0, p0, Landroidx/browser/trusted/TrustedWebActivityIntentBuilder;->mDisplayMode:Landroidx/browser/trusted/TrustedWebActivityDisplayMode;

    return-object v0
.end method

.method public getUri()Landroid/net/Uri;
    .locals 1

    .line 327
    iget-object v0, p0, Landroidx/browser/trusted/TrustedWebActivityIntentBuilder;->mUri:Landroid/net/Uri;

    return-object v0
.end method

.method public setAdditionalTrustedOrigins(Ljava/util/List;)Landroidx/browser/trusted/TrustedWebActivityIntentBuilder;
    .locals 0
    .annotation system Ldalvik/annotation/MethodParameters;
        accessFlags = {
            0x0
        }
        names = {
            "origins"
        }
    .end annotation

    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/List<",
            "Ljava/lang/String;",
            ">;)",
            "Landroidx/browser/trusted/TrustedWebActivityIntentBuilder;"
        }
    .end annotation

    .line 209
    .local p1, "origins":Ljava/util/List;, "Ljava/util/List<Ljava/lang/String;>;"
    iput-object p1, p0, Landroidx/browser/trusted/TrustedWebActivityIntentBuilder;->mAdditionalTrustedOrigins:Ljava/util/List;

    .line 210
    return-object p0
.end method

.method public setColorScheme(I)Landroidx/browser/trusted/TrustedWebActivityIntentBuilder;
    .locals 1
    .param p1, "colorScheme"    # I
    .annotation system Ldalvik/annotation/MethodParameters;
        accessFlags = {
            0x0
        }
        names = {
            "colorScheme"
        }
    .end annotation

    .line 161
    iget-object v0, p0, Landroidx/browser/trusted/TrustedWebActivityIntentBuilder;->mIntentBuilder:Landroidx/browser/customtabs/CustomTabsIntent$Builder;

    invoke-virtual {v0, p1}, Landroidx/browser/customtabs/CustomTabsIntent$Builder;->setColorScheme(I)Landroidx/browser/customtabs/CustomTabsIntent$Builder;

    .line 162
    return-object p0
.end method

.method public setColorSchemeParams(ILandroidx/browser/customtabs/CustomTabColorSchemeParams;)Landroidx/browser/trusted/TrustedWebActivityIntentBuilder;
    .locals 1
    .param p1, "colorScheme"    # I
    .param p2, "params"    # Landroidx/browser/customtabs/CustomTabColorSchemeParams;
    .annotation system Ldalvik/annotation/MethodParameters;
        accessFlags = {
            0x0,
            0x0
        }
        names = {
            "colorScheme",
            "params"
        }
    .end annotation

    .line 175
    iget-object v0, p0, Landroidx/browser/trusted/TrustedWebActivityIntentBuilder;->mIntentBuilder:Landroidx/browser/customtabs/CustomTabsIntent$Builder;

    invoke-virtual {v0, p1, p2}, Landroidx/browser/customtabs/CustomTabsIntent$Builder;->setColorSchemeParams(ILandroidx/browser/customtabs/CustomTabColorSchemeParams;)Landroidx/browser/customtabs/CustomTabsIntent$Builder;

    .line 176
    return-object p0
.end method

.method public setDefaultColorSchemeParams(Landroidx/browser/customtabs/CustomTabColorSchemeParams;)Landroidx/browser/trusted/TrustedWebActivityIntentBuilder;
    .locals 1
    .param p1, "params"    # Landroidx/browser/customtabs/CustomTabColorSchemeParams;
    .annotation system Ldalvik/annotation/MethodParameters;
        accessFlags = {
            0x0
        }
        names = {
            "params"
        }
    .end annotation

    .line 190
    iget-object v0, p0, Landroidx/browser/trusted/TrustedWebActivityIntentBuilder;->mIntentBuilder:Landroidx/browser/customtabs/CustomTabsIntent$Builder;

    invoke-virtual {v0, p1}, Landroidx/browser/customtabs/CustomTabsIntent$Builder;->setDefaultColorSchemeParams(Landroidx/browser/customtabs/CustomTabColorSchemeParams;)Landroidx/browser/customtabs/CustomTabsIntent$Builder;

    .line 191
    return-object p0
.end method

.method public setDisplayMode(Landroidx/browser/trusted/TrustedWebActivityDisplayMode;)Landroidx/browser/trusted/TrustedWebActivityIntentBuilder;
    .locals 0
    .param p1, "displayMode"    # Landroidx/browser/trusted/TrustedWebActivityDisplayMode;
    .annotation system Ldalvik/annotation/MethodParameters;
        accessFlags = {
            0x0
        }
        names = {
            "displayMode"
        }
    .end annotation

    .line 258
    iput-object p1, p0, Landroidx/browser/trusted/TrustedWebActivityIntentBuilder;->mDisplayMode:Landroidx/browser/trusted/TrustedWebActivityDisplayMode;

    .line 259
    return-object p0
.end method

.method public setNavigationBarColor(I)Landroidx/browser/trusted/TrustedWebActivityIntentBuilder;
    .locals 1
    .param p1, "color"    # I
    .annotation system Ldalvik/annotation/MethodParameters;
        accessFlags = {
            0x0
        }
        names = {
            "color"
        }
    .end annotation

    .line 133
    iget-object v0, p0, Landroidx/browser/trusted/TrustedWebActivityIntentBuilder;->mIntentBuilder:Landroidx/browser/customtabs/CustomTabsIntent$Builder;

    invoke-virtual {v0, p1}, Landroidx/browser/customtabs/CustomTabsIntent$Builder;->setNavigationBarColor(I)Landroidx/browser/customtabs/CustomTabsIntent$Builder;

    .line 134
    return-object p0
.end method

.method public setNavigationBarDividerColor(I)Landroidx/browser/trusted/TrustedWebActivityIntentBuilder;
    .locals 1
    .param p1, "color"    # I
    .annotation system Ldalvik/annotation/MethodParameters;
        accessFlags = {
            0x0
        }
        names = {
            "color"
        }
    .end annotation

    .line 145
    iget-object v0, p0, Landroidx/browser/trusted/TrustedWebActivityIntentBuilder;->mIntentBuilder:Landroidx/browser/customtabs/CustomTabsIntent$Builder;

    invoke-virtual {v0, p1}, Landroidx/browser/customtabs/CustomTabsIntent$Builder;->setNavigationBarDividerColor(I)Landroidx/browser/customtabs/CustomTabsIntent$Builder;

    .line 146
    return-object p0
.end method

.method public setScreenOrientation(I)Landroidx/browser/trusted/TrustedWebActivityIntentBuilder;
    .locals 0
    .param p1, "orientation"    # I
    .annotation system Ldalvik/annotation/MethodParameters;
        accessFlags = {
            0x0
        }
        names = {
            "orientation"
        }
    .end annotation

    .line 272
    iput p1, p0, Landroidx/browser/trusted/TrustedWebActivityIntentBuilder;->mScreenOrientation:I

    .line 273
    return-object p0
.end method

.method public setShareParams(Landroidx/browser/trusted/sharing/ShareTarget;Landroidx/browser/trusted/sharing/ShareData;)Landroidx/browser/trusted/TrustedWebActivityIntentBuilder;
    .locals 0
    .param p1, "shareTarget"    # Landroidx/browser/trusted/sharing/ShareTarget;
    .param p2, "shareData"    # Landroidx/browser/trusted/sharing/ShareData;
    .annotation system Ldalvik/annotation/MethodParameters;
        accessFlags = {
            0x0,
            0x0
        }
        names = {
            "shareTarget",
            "shareData"
        }
    .end annotation

    .line 245
    iput-object p1, p0, Landroidx/browser/trusted/TrustedWebActivityIntentBuilder;->mShareTarget:Landroidx/browser/trusted/sharing/ShareTarget;

    .line 246
    iput-object p2, p0, Landroidx/browser/trusted/TrustedWebActivityIntentBuilder;->mShareData:Landroidx/browser/trusted/sharing/ShareData;

    .line 247
    return-object p0
.end method

.method public setSplashScreenParams(Landroid/os/Bundle;)Landroidx/browser/trusted/TrustedWebActivityIntentBuilder;
    .locals 0
    .param p1, "splashScreenParams"    # Landroid/os/Bundle;
    .annotation system Ldalvik/annotation/MethodParameters;
        accessFlags = {
            0x0
        }
        names = {
            "splashScreenParams"
        }
    .end annotation

    .line 231
    iput-object p1, p0, Landroidx/browser/trusted/TrustedWebActivityIntentBuilder;->mSplashScreenParams:Landroid/os/Bundle;

    .line 232
    return-object p0
.end method

.method public setToolbarColor(I)Landroidx/browser/trusted/TrustedWebActivityIntentBuilder;
    .locals 1
    .param p1, "color"    # I
    .annotation system Ldalvik/annotation/MethodParameters;
        accessFlags = {
            0x0
        }
        names = {
            "color"
        }
    .end annotation

    .line 122
    iget-object v0, p0, Landroidx/browser/trusted/TrustedWebActivityIntentBuilder;->mIntentBuilder:Landroidx/browser/customtabs/CustomTabsIntent$Builder;

    invoke-virtual {v0, p1}, Landroidx/browser/customtabs/CustomTabsIntent$Builder;->setToolbarColor(I)Landroidx/browser/customtabs/CustomTabsIntent$Builder;

    .line 123
    return-object p0
.end method
