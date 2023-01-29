.class public Landroidx/browser/browseractions/BrowserActionsIntent;
.super Ljava/lang/Object;
.source "BrowserActionsIntent.java"


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Landroidx/browser/browseractions/BrowserActionsIntent$Builder;,
        Landroidx/browser/browseractions/BrowserActionsIntent$BrowserActionsFallDialogListener;,
        Landroidx/browser/browseractions/BrowserActionsIntent$BrowserActionsItemId;,
        Landroidx/browser/browseractions/BrowserActionsIntent$BrowserActionsUrlType;
    }
.end annotation

.annotation runtime Ljava/lang/Deprecated;
.end annotation


# static fields
.field public static final ACTION_BROWSER_ACTIONS_OPEN:Ljava/lang/String; = "androidx.browser.browseractions.browser_action_open"

.field public static final EXTRA_APP_ID:Ljava/lang/String; = "androidx.browser.browseractions.APP_ID"

.field public static final EXTRA_MENU_ITEMS:Ljava/lang/String; = "androidx.browser.browseractions.extra.MENU_ITEMS"

.field public static final EXTRA_SELECTED_ACTION_PENDING_INTENT:Ljava/lang/String; = "androidx.browser.browseractions.extra.SELECTED_ACTION_PENDING_INTENT"

.field public static final EXTRA_TYPE:Ljava/lang/String; = "androidx.browser.browseractions.extra.TYPE"

.field public static final ITEM_COPY:I = 0x3

.field public static final ITEM_DOWNLOAD:I = 0x2

.field public static final ITEM_INVALID_ITEM:I = -0x1

.field public static final ITEM_OPEN_IN_INCOGNITO:I = 0x1

.field public static final ITEM_OPEN_IN_NEW_TAB:I = 0x0

.field public static final ITEM_SHARE:I = 0x4

.field public static final KEY_ACTION:Ljava/lang/String; = "androidx.browser.browseractions.ACTION"

.field public static final KEY_ICON_ID:Ljava/lang/String; = "androidx.browser.browseractions.ICON_ID"

.field private static final KEY_ICON_URI:Ljava/lang/String; = "androidx.browser.browseractions.ICON_URI"

.field public static final KEY_TITLE:Ljava/lang/String; = "androidx.browser.browseractions.TITLE"

.field public static final MAX_CUSTOM_ITEMS:I = 0x5

.field private static final TAG:Ljava/lang/String; = "BrowserActions"

.field private static final TEST_URL:Ljava/lang/String; = "https://www.example.com"

.field public static final URL_TYPE_AUDIO:I = 0x3

.field public static final URL_TYPE_FILE:I = 0x4

.field public static final URL_TYPE_IMAGE:I = 0x1

.field public static final URL_TYPE_NONE:I = 0x0

.field public static final URL_TYPE_PLUGIN:I = 0x5

.field public static final URL_TYPE_VIDEO:I = 0x2

.field private static sDialogListenter:Landroidx/browser/browseractions/BrowserActionsIntent$BrowserActionsFallDialogListener;


# instance fields
.field private final mIntent:Landroid/content/Intent;


# direct methods
.method constructor <init>(Landroid/content/Intent;)V
    .locals 0
    .param p1, "intent"    # Landroid/content/Intent;
    .annotation system Ldalvik/annotation/MethodParameters;
        accessFlags = {
            0x0
        }
        names = {
            "intent"
        }
    .end annotation

    .line 172
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 173
    iput-object p1, p0, Landroidx/browser/browseractions/BrowserActionsIntent;->mIntent:Landroid/content/Intent;

    .line 174
    return-void
.end method

.method public static getBrowserActionsIntentHandlers(Landroid/content/Context;)Ljava/util/List;
    .locals 3
    .param p0, "context"    # Landroid/content/Context;
    .annotation system Ldalvik/annotation/MethodParameters;
        accessFlags = {
            0x0
        }
        names = {
            "context"
        }
    .end annotation

    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Landroid/content/Context;",
            ")",
            "Ljava/util/List<",
            "Landroid/content/pm/ResolveInfo;",
            ">;"
        }
    .end annotation

    .line 385
    new-instance v0, Landroid/content/Intent;

    .line 386
    const-string v1, "https://www.example.com"

    invoke-static {v1}, Landroid/net/Uri;->parse(Ljava/lang/String;)Landroid/net/Uri;

    move-result-object v1

    const-string v2, "androidx.browser.browseractions.browser_action_open"

    invoke-direct {v0, v2, v1}, Landroid/content/Intent;-><init>(Ljava/lang/String;Landroid/net/Uri;)V

    .line 387
    .local v0, "intent":Landroid/content/Intent;
    invoke-virtual {p0}, Landroid/content/Context;->getPackageManager()Landroid/content/pm/PackageManager;

    move-result-object v1

    .line 388
    .local v1, "pm":Landroid/content/pm/PackageManager;
    const/high16 v2, 0x20000

    invoke-virtual {v1, v0, v2}, Landroid/content/pm/PackageManager;->queryIntentActivities(Landroid/content/Intent;I)Ljava/util/List;

    move-result-object v2

    return-object v2
.end method

.method public static getCreatorPackageName(Landroid/content/Intent;)Ljava/lang/String;
    .locals 1
    .param p0, "intent"    # Landroid/content/Intent;
    .annotation system Ldalvik/annotation/MethodParameters;
        accessFlags = {
            0x0
        }
        names = {
            "intent"
        }
    .end annotation

    .annotation runtime Ljava/lang/Deprecated;
    .end annotation

    .line 485
    invoke-static {p0}, Landroidx/browser/browseractions/BrowserActionsIntent;->getUntrustedCreatorPackageName(Landroid/content/Intent;)Ljava/lang/String;

    move-result-object v0

    return-object v0
.end method

.method public static getUntrustedCreatorPackageName(Landroid/content/Intent;)Ljava/lang/String;
    .locals 3
    .param p0, "intent"    # Landroid/content/Intent;
    .annotation system Ldalvik/annotation/MethodParameters;
        accessFlags = {
            0x0
        }
        names = {
            "intent"
        }
    .end annotation

    .line 466
    const-string v0, "androidx.browser.browseractions.APP_ID"

    invoke-virtual {p0, v0}, Landroid/content/Intent;->getParcelableExtra(Ljava/lang/String;)Landroid/os/Parcelable;

    move-result-object v0

    check-cast v0, Landroid/app/PendingIntent;

    .line 467
    .local v0, "pendingIntent":Landroid/app/PendingIntent;
    if-eqz v0, :cond_1

    .line 468
    sget v1, Landroid/os/Build$VERSION;->SDK_INT:I

    const/16 v2, 0x11

    if-lt v1, v2, :cond_0

    .line 469
    invoke-virtual {v0}, Landroid/app/PendingIntent;->getCreatorPackage()Ljava/lang/String;

    move-result-object v1

    return-object v1

    .line 471
    :cond_0
    invoke-virtual {v0}, Landroid/app/PendingIntent;->getTargetPackage()Ljava/lang/String;

    move-result-object v1

    return-object v1

    .line 474
    :cond_1
    const/4 v1, 0x0

    return-object v1
.end method

.method public static launchIntent(Landroid/content/Context;Landroid/content/Intent;)V
    .locals 1
    .param p0, "context"    # Landroid/content/Context;
    .param p1, "intent"    # Landroid/content/Intent;
    .annotation system Ldalvik/annotation/MethodParameters;
        accessFlags = {
            0x0,
            0x0
        }
        names = {
            "context",
            "intent"
        }
    .end annotation

    .line 344
    invoke-static {p0}, Landroidx/browser/browseractions/BrowserActionsIntent;->getBrowserActionsIntentHandlers(Landroid/content/Context;)Ljava/util/List;

    move-result-object v0

    .line 345
    .local v0, "handlers":Ljava/util/List;, "Ljava/util/List<Landroid/content/pm/ResolveInfo;>;"
    invoke-static {p0, p1, v0}, Landroidx/browser/browseractions/BrowserActionsIntent;->launchIntent(Landroid/content/Context;Landroid/content/Intent;Ljava/util/List;)V

    .line 346
    return-void
.end method

.method static launchIntent(Landroid/content/Context;Landroid/content/Intent;Ljava/util/List;)V
    .locals 6
    .param p0, "context"    # Landroid/content/Context;
    .param p1, "intent"    # Landroid/content/Intent;
    .annotation system Ldalvik/annotation/MethodParameters;
        accessFlags = {
            0x0,
            0x0,
            0x0
        }
        names = {
            "context",
            "intent",
            "handlers"
        }
    .end annotation

    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Landroid/content/Context;",
            "Landroid/content/Intent;",
            "Ljava/util/List<",
            "Landroid/content/pm/ResolveInfo;",
            ">;)V"
        }
    .end annotation

    .line 352
    .local p2, "handlers":Ljava/util/List;, "Ljava/util/List<Landroid/content/pm/ResolveInfo;>;"
    if-eqz p2, :cond_4

    invoke-interface {p2}, Ljava/util/List;->size()I

    move-result v0

    if-nez v0, :cond_0

    goto :goto_2

    .line 355
    :cond_0
    invoke-interface {p2}, Ljava/util/List;->size()I

    move-result v0

    const/4 v1, 0x1

    if-ne v0, v1, :cond_1

    .line 356
    const/4 v0, 0x0

    invoke-interface {p2, v0}, Ljava/util/List;->get(I)Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Landroid/content/pm/ResolveInfo;

    iget-object v0, v0, Landroid/content/pm/ResolveInfo;->activityInfo:Landroid/content/pm/ActivityInfo;

    iget-object v0, v0, Landroid/content/pm/ActivityInfo;->packageName:Ljava/lang/String;

    invoke-virtual {p1, v0}, Landroid/content/Intent;->setPackage(Ljava/lang/String;)Landroid/content/Intent;

    goto :goto_1

    .line 358
    :cond_1
    new-instance v0, Landroid/content/Intent;

    const-string v1, "https://www.example.com"

    invoke-static {v1}, Landroid/net/Uri;->parse(Ljava/lang/String;)Landroid/net/Uri;

    move-result-object v1

    const-string v2, "android.intent.action.VIEW"

    invoke-direct {v0, v2, v1}, Landroid/content/Intent;-><init>(Ljava/lang/String;Landroid/net/Uri;)V

    .line 359
    .local v0, "viewIntent":Landroid/content/Intent;
    invoke-virtual {p0}, Landroid/content/Context;->getPackageManager()Landroid/content/pm/PackageManager;

    move-result-object v1

    .line 360
    .local v1, "pm":Landroid/content/pm/PackageManager;
    const/high16 v2, 0x10000

    .line 361
    invoke-virtual {v1, v0, v2}, Landroid/content/pm/PackageManager;->resolveActivity(Landroid/content/Intent;I)Landroid/content/pm/ResolveInfo;

    move-result-object v2

    .line 362
    .local v2, "defaultHandler":Landroid/content/pm/ResolveInfo;
    if-eqz v2, :cond_3

    .line 363
    iget-object v3, v2, Landroid/content/pm/ResolveInfo;->activityInfo:Landroid/content/pm/ActivityInfo;

    iget-object v3, v3, Landroid/content/pm/ActivityInfo;->packageName:Ljava/lang/String;

    .line 364
    .local v3, "defaultPackageName":Ljava/lang/String;
    const/4 v4, 0x0

    .local v4, "i":I
    :goto_0
    invoke-interface {p2}, Ljava/util/List;->size()I

    move-result v5

    if-ge v4, v5, :cond_3

    .line 365
    invoke-interface {p2, v4}, Ljava/util/List;->get(I)Ljava/lang/Object;

    move-result-object v5

    check-cast v5, Landroid/content/pm/ResolveInfo;

    iget-object v5, v5, Landroid/content/pm/ResolveInfo;->activityInfo:Landroid/content/pm/ActivityInfo;

    iget-object v5, v5, Landroid/content/pm/ActivityInfo;->packageName:Ljava/lang/String;

    invoke-virtual {v3, v5}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v5

    if-eqz v5, :cond_2

    .line 366
    invoke-virtual {p1, v3}, Landroid/content/Intent;->setPackage(Ljava/lang/String;)Landroid/content/Intent;

    .line 367
    goto :goto_1

    .line 364
    :cond_2
    add-int/lit8 v4, v4, 0x1

    goto :goto_0

    .line 372
    .end local v0    # "viewIntent":Landroid/content/Intent;
    .end local v1    # "pm":Landroid/content/pm/PackageManager;
    .end local v2    # "defaultHandler":Landroid/content/pm/ResolveInfo;
    .end local v3    # "defaultPackageName":Ljava/lang/String;
    .end local v4    # "i":I
    :cond_3
    :goto_1
    const/4 v0, 0x0

    invoke-static {p0, p1, v0}, Landroidx/core/content/ContextCompat;->startActivity(Landroid/content/Context;Landroid/content/Intent;Landroid/os/Bundle;)V

    .line 373
    return-void

    .line 353
    :cond_4
    :goto_2
    invoke-static {p0, p1}, Landroidx/browser/browseractions/BrowserActionsIntent;->openFallbackBrowserActionsMenu(Landroid/content/Context;Landroid/content/Intent;)V

    .line 354
    return-void
.end method

.method public static openBrowserAction(Landroid/content/Context;Landroid/net/Uri;)V
    .locals 2
    .param p0, "context"    # Landroid/content/Context;
    .param p1, "uri"    # Landroid/net/Uri;
    .annotation system Ldalvik/annotation/MethodParameters;
        accessFlags = {
            0x0,
            0x0
        }
        names = {
            "context",
            "uri"
        }
    .end annotation

    .line 310
    new-instance v0, Landroidx/browser/browseractions/BrowserActionsIntent$Builder;

    invoke-direct {v0, p0, p1}, Landroidx/browser/browseractions/BrowserActionsIntent$Builder;-><init>(Landroid/content/Context;Landroid/net/Uri;)V

    invoke-virtual {v0}, Landroidx/browser/browseractions/BrowserActionsIntent$Builder;->build()Landroidx/browser/browseractions/BrowserActionsIntent;

    move-result-object v0

    .line 311
    .local v0, "intent":Landroidx/browser/browseractions/BrowserActionsIntent;
    invoke-virtual {v0}, Landroidx/browser/browseractions/BrowserActionsIntent;->getIntent()Landroid/content/Intent;

    move-result-object v1

    invoke-static {p0, v1}, Landroidx/browser/browseractions/BrowserActionsIntent;->launchIntent(Landroid/content/Context;Landroid/content/Intent;)V

    .line 312
    return-void
.end method

.method public static openBrowserAction(Landroid/content/Context;Landroid/net/Uri;ILjava/util/ArrayList;Landroid/app/PendingIntent;)V
    .locals 2
    .param p0, "context"    # Landroid/content/Context;
    .param p1, "uri"    # Landroid/net/Uri;
    .param p2, "type"    # I
    .param p4, "pendingIntent"    # Landroid/app/PendingIntent;
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
            "uri",
            "type",
            "items",
            "pendingIntent"
        }
    .end annotation

    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Landroid/content/Context;",
            "Landroid/net/Uri;",
            "I",
            "Ljava/util/ArrayList<",
            "Landroidx/browser/browseractions/BrowserActionItem;",
            ">;",
            "Landroid/app/PendingIntent;",
            ")V"
        }
    .end annotation

    .line 326
    .local p3, "items":Ljava/util/ArrayList;, "Ljava/util/ArrayList<Landroidx/browser/browseractions/BrowserActionItem;>;"
    new-instance v0, Landroidx/browser/browseractions/BrowserActionsIntent$Builder;

    invoke-direct {v0, p0, p1}, Landroidx/browser/browseractions/BrowserActionsIntent$Builder;-><init>(Landroid/content/Context;Landroid/net/Uri;)V

    .line 327
    invoke-virtual {v0, p2}, Landroidx/browser/browseractions/BrowserActionsIntent$Builder;->setUrlType(I)Landroidx/browser/browseractions/BrowserActionsIntent$Builder;

    move-result-object v0

    .line 328
    invoke-virtual {v0, p3}, Landroidx/browser/browseractions/BrowserActionsIntent$Builder;->setCustomItems(Ljava/util/ArrayList;)Landroidx/browser/browseractions/BrowserActionsIntent$Builder;

    move-result-object v0

    .line 329
    invoke-virtual {v0, p4}, Landroidx/browser/browseractions/BrowserActionsIntent$Builder;->setOnItemSelectedAction(Landroid/app/PendingIntent;)Landroidx/browser/browseractions/BrowserActionsIntent$Builder;

    move-result-object v0

    .line 330
    invoke-virtual {v0}, Landroidx/browser/browseractions/BrowserActionsIntent$Builder;->build()Landroidx/browser/browseractions/BrowserActionsIntent;

    move-result-object v0

    .line 331
    .local v0, "intent":Landroidx/browser/browseractions/BrowserActionsIntent;
    invoke-virtual {v0}, Landroidx/browser/browseractions/BrowserActionsIntent;->getIntent()Landroid/content/Intent;

    move-result-object v1

    invoke-static {p0, v1}, Landroidx/browser/browseractions/BrowserActionsIntent;->launchIntent(Landroid/content/Context;Landroid/content/Intent;)V

    .line 332
    return-void
.end method

.method private static openFallbackBrowserActionsMenu(Landroid/content/Context;Landroid/content/Intent;)V
    .locals 3
    .param p0, "context"    # Landroid/content/Context;
    .param p1, "intent"    # Landroid/content/Intent;
    .annotation system Ldalvik/annotation/MethodParameters;
        accessFlags = {
            0x0,
            0x0
        }
        names = {
            "context",
            "intent"
        }
    .end annotation

    .line 393
    invoke-virtual {p1}, Landroid/content/Intent;->getData()Landroid/net/Uri;

    move-result-object v0

    .line 394
    .local v0, "uri":Landroid/net/Uri;
    const-string v1, "androidx.browser.browseractions.extra.MENU_ITEMS"

    invoke-virtual {p1, v1}, Landroid/content/Intent;->getParcelableArrayListExtra(Ljava/lang/String;)Ljava/util/ArrayList;

    move-result-object v1

    .line 395
    .local v1, "bundles":Ljava/util/ArrayList;, "Ljava/util/ArrayList<Landroid/os/Bundle;>;"
    if-eqz v1, :cond_0

    invoke-static {v1}, Landroidx/browser/browseractions/BrowserActionsIntent;->parseBrowserActionItems(Ljava/util/ArrayList;)Ljava/util/List;

    move-result-object v2

    goto :goto_0

    :cond_0
    const/4 v2, 0x0

    .line 396
    .local v2, "items":Ljava/util/List;, "Ljava/util/List<Landroidx/browser/browseractions/BrowserActionItem;>;"
    :goto_0
    invoke-static {p0, v0, v2}, Landroidx/browser/browseractions/BrowserActionsIntent;->openFallbackBrowserActionsMenu(Landroid/content/Context;Landroid/net/Uri;Ljava/util/List;)V

    .line 397
    return-void
.end method

.method private static openFallbackBrowserActionsMenu(Landroid/content/Context;Landroid/net/Uri;Ljava/util/List;)V
    .locals 2
    .param p0, "context"    # Landroid/content/Context;
    .param p1, "uri"    # Landroid/net/Uri;
    .annotation system Ldalvik/annotation/MethodParameters;
        accessFlags = {
            0x0,
            0x0,
            0x0
        }
        names = {
            "context",
            "uri",
            "menuItems"
        }
    .end annotation

    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Landroid/content/Context;",
            "Landroid/net/Uri;",
            "Ljava/util/List<",
            "Landroidx/browser/browseractions/BrowserActionItem;",
            ">;)V"
        }
    .end annotation

    .line 414
    .local p2, "menuItems":Ljava/util/List;, "Ljava/util/List<Landroidx/browser/browseractions/BrowserActionItem;>;"
    new-instance v0, Landroidx/browser/browseractions/BrowserActionsFallbackMenuUi;

    invoke-direct {v0, p0, p1, p2}, Landroidx/browser/browseractions/BrowserActionsFallbackMenuUi;-><init>(Landroid/content/Context;Landroid/net/Uri;Ljava/util/List;)V

    .line 416
    .local v0, "menuUi":Landroidx/browser/browseractions/BrowserActionsFallbackMenuUi;
    invoke-virtual {v0}, Landroidx/browser/browseractions/BrowserActionsFallbackMenuUi;->displayMenu()V

    .line 417
    sget-object v1, Landroidx/browser/browseractions/BrowserActionsIntent;->sDialogListenter:Landroidx/browser/browseractions/BrowserActionsIntent$BrowserActionsFallDialogListener;

    if-eqz v1, :cond_0

    .line 418
    invoke-interface {v1}, Landroidx/browser/browseractions/BrowserActionsIntent$BrowserActionsFallDialogListener;->onDialogShown()V

    .line 420
    :cond_0
    return-void
.end method

.method public static parseBrowserActionItems(Ljava/util/ArrayList;)Ljava/util/List;
    .locals 9
    .annotation system Ldalvik/annotation/MethodParameters;
        accessFlags = {
            0x0
        }
        names = {
            "bundles"
        }
    .end annotation

    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/ArrayList<",
            "Landroid/os/Bundle;",
            ">;)",
            "Ljava/util/List<",
            "Landroidx/browser/browseractions/BrowserActionItem;",
            ">;"
        }
    .end annotation

    .line 430
    .local p0, "bundles":Ljava/util/ArrayList;, "Ljava/util/ArrayList<Landroid/os/Bundle;>;"
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    .line 431
    .local v0, "mActions":Ljava/util/List;, "Ljava/util/List<Landroidx/browser/browseractions/BrowserActionItem;>;"
    const/4 v1, 0x0

    .local v1, "i":I
    :goto_0
    invoke-virtual {p0}, Ljava/util/ArrayList;->size()I

    move-result v2

    if-ge v1, v2, :cond_2

    .line 432
    invoke-virtual {p0, v1}, Ljava/util/ArrayList;->get(I)Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Landroid/os/Bundle;

    .line 433
    .local v2, "bundle":Landroid/os/Bundle;
    const-string v3, "androidx.browser.browseractions.TITLE"

    invoke-virtual {v2, v3}, Landroid/os/Bundle;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v3

    .line 434
    .local v3, "title":Ljava/lang/String;
    const-string v4, "androidx.browser.browseractions.ACTION"

    invoke-virtual {v2, v4}, Landroid/os/Bundle;->getParcelable(Ljava/lang/String;)Landroid/os/Parcelable;

    move-result-object v4

    check-cast v4, Landroid/app/PendingIntent;

    .line 436
    .local v4, "action":Landroid/app/PendingIntent;
    const-string v5, "androidx.browser.browseractions.ICON_ID"

    invoke-virtual {v2, v5}, Landroid/os/Bundle;->getInt(Ljava/lang/String;)I

    move-result v5

    .line 437
    .local v5, "iconId":I
    const-string v6, "androidx.browser.browseractions.ICON_URI"

    invoke-virtual {v2, v6}, Landroid/os/Bundle;->getParcelable(Ljava/lang/String;)Landroid/os/Parcelable;

    move-result-object v6

    check-cast v6, Landroid/net/Uri;

    .line 438
    .local v6, "iconUri":Landroid/net/Uri;
    invoke-static {v3}, Landroid/text/TextUtils;->isEmpty(Ljava/lang/CharSequence;)Z

    move-result v7

    if-nez v7, :cond_1

    if-eqz v4, :cond_1

    .line 440
    if-eqz v5, :cond_0

    .line 441
    new-instance v7, Landroidx/browser/browseractions/BrowserActionItem;

    invoke-direct {v7, v3, v4, v5}, Landroidx/browser/browseractions/BrowserActionItem;-><init>(Ljava/lang/String;Landroid/app/PendingIntent;I)V

    .local v7, "item":Landroidx/browser/browseractions/BrowserActionItem;
    goto :goto_1

    .line 443
    .end local v7    # "item":Landroidx/browser/browseractions/BrowserActionItem;
    :cond_0
    new-instance v7, Landroidx/browser/browseractions/BrowserActionItem;

    invoke-direct {v7, v3, v4, v6}, Landroidx/browser/browseractions/BrowserActionItem;-><init>(Ljava/lang/String;Landroid/app/PendingIntent;Landroid/net/Uri;)V

    .line 445
    .restart local v7    # "item":Landroidx/browser/browseractions/BrowserActionItem;
    :goto_1
    invoke-interface {v0, v7}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    .line 446
    .end local v7    # "item":Landroidx/browser/browseractions/BrowserActionItem;
    nop

    .line 431
    .end local v2    # "bundle":Landroid/os/Bundle;
    .end local v3    # "title":Ljava/lang/String;
    .end local v4    # "action":Landroid/app/PendingIntent;
    .end local v5    # "iconId":I
    .end local v6    # "iconUri":Landroid/net/Uri;
    add-int/lit8 v1, v1, 0x1

    goto :goto_0

    .line 447
    .restart local v2    # "bundle":Landroid/os/Bundle;
    .restart local v3    # "title":Ljava/lang/String;
    .restart local v4    # "action":Landroid/app/PendingIntent;
    .restart local v5    # "iconId":I
    .restart local v6    # "iconUri":Landroid/net/Uri;
    :cond_1
    new-instance v7, Ljava/lang/IllegalArgumentException;

    const-string v8, "Custom item should contain a non-empty title and non-null intent."

    invoke-direct {v7, v8}, Ljava/lang/IllegalArgumentException;-><init>(Ljava/lang/String;)V

    throw v7

    .line 451
    .end local v1    # "i":I
    .end local v2    # "bundle":Landroid/os/Bundle;
    .end local v3    # "title":Ljava/lang/String;
    .end local v4    # "action":Landroid/app/PendingIntent;
    .end local v5    # "iconId":I
    .end local v6    # "iconUri":Landroid/net/Uri;
    :cond_2
    return-object v0
.end method

.method static setDialogShownListenter(Landroidx/browser/browseractions/BrowserActionsIntent$BrowserActionsFallDialogListener;)V
    .locals 0
    .param p0, "dialogListener"    # Landroidx/browser/browseractions/BrowserActionsIntent$BrowserActionsFallDialogListener;
    .annotation system Ldalvik/annotation/MethodParameters;
        accessFlags = {
            0x0
        }
        names = {
            "dialogListener"
        }
    .end annotation

    .line 403
    sput-object p0, Landroidx/browser/browseractions/BrowserActionsIntent;->sDialogListenter:Landroidx/browser/browseractions/BrowserActionsIntent$BrowserActionsFallDialogListener;

    .line 404
    return-void
.end method


# virtual methods
.method public getIntent()Landroid/content/Intent;
    .locals 1

    .line 168
    iget-object v0, p0, Landroidx/browser/browseractions/BrowserActionsIntent;->mIntent:Landroid/content/Intent;

    return-object v0
.end method
