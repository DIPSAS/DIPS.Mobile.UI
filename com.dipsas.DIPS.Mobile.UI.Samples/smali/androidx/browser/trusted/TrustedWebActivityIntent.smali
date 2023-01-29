.class public final Landroidx/browser/trusted/TrustedWebActivityIntent;
.super Ljava/lang/Object;
.source "TrustedWebActivityIntent.java"


# instance fields
.field private final mIntent:Landroid/content/Intent;

.field private final mSharedFileUris:Ljava/util/List;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/List<",
            "Landroid/net/Uri;",
            ">;"
        }
    .end annotation
.end field


# direct methods
.method constructor <init>(Landroid/content/Intent;Ljava/util/List;)V
    .locals 0
    .param p1, "intent"    # Landroid/content/Intent;
    .annotation system Ldalvik/annotation/MethodParameters;
        accessFlags = {
            0x0,
            0x0
        }
        names = {
            "intent",
            "sharedFileUris"
        }
    .end annotation

    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Landroid/content/Intent;",
            "Ljava/util/List<",
            "Landroid/net/Uri;",
            ">;)V"
        }
    .end annotation

    .line 38
    .local p2, "sharedFileUris":Ljava/util/List;, "Ljava/util/List<Landroid/net/Uri;>;"
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 39
    iput-object p1, p0, Landroidx/browser/trusted/TrustedWebActivityIntent;->mIntent:Landroid/content/Intent;

    .line 40
    iput-object p2, p0, Landroidx/browser/trusted/TrustedWebActivityIntent;->mSharedFileUris:Ljava/util/List;

    .line 41
    return-void
.end method

.method private grantUriPermissionToProvider(Landroid/content/Context;)V
    .locals 4
    .param p1, "context"    # Landroid/content/Context;
    .annotation system Ldalvik/annotation/MethodParameters;
        accessFlags = {
            0x0
        }
        names = {
            "context"
        }
    .end annotation

    .line 52
    iget-object v0, p0, Landroidx/browser/trusted/TrustedWebActivityIntent;->mSharedFileUris:Ljava/util/List;

    invoke-interface {v0}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v0

    :goto_0
    invoke-interface {v0}, Ljava/util/Iterator;->hasNext()Z

    move-result v1

    if-eqz v1, :cond_0

    invoke-interface {v0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Landroid/net/Uri;

    .line 53
    .local v1, "uri":Landroid/net/Uri;
    iget-object v2, p0, Landroidx/browser/trusted/TrustedWebActivityIntent;->mIntent:Landroid/content/Intent;

    invoke-virtual {v2}, Landroid/content/Intent;->getPackage()Ljava/lang/String;

    move-result-object v2

    const/4 v3, 0x1

    invoke-virtual {p1, v2, v1, v3}, Landroid/content/Context;->grantUriPermission(Ljava/lang/String;Landroid/net/Uri;I)V

    .line 55
    .end local v1    # "uri":Landroid/net/Uri;
    goto :goto_0

    .line 56
    :cond_0
    return-void
.end method


# virtual methods
.method public getIntent()Landroid/content/Intent;
    .locals 1

    .line 64
    iget-object v0, p0, Landroidx/browser/trusted/TrustedWebActivityIntent;->mIntent:Landroid/content/Intent;

    return-object v0
.end method

.method public launchTrustedWebActivity(Landroid/content/Context;)V
    .locals 2
    .param p1, "context"    # Landroid/content/Context;
    .annotation system Ldalvik/annotation/MethodParameters;
        accessFlags = {
            0x0
        }
        names = {
            "context"
        }
    .end annotation

    .line 47
    invoke-direct {p0, p1}, Landroidx/browser/trusted/TrustedWebActivityIntent;->grantUriPermissionToProvider(Landroid/content/Context;)V

    .line 48
    iget-object v0, p0, Landroidx/browser/trusted/TrustedWebActivityIntent;->mIntent:Landroid/content/Intent;

    const/4 v1, 0x0

    invoke-static {p1, v0, v1}, Landroidx/core/content/ContextCompat;->startActivity(Landroid/content/Context;Landroid/content/Intent;Landroid/os/Bundle;)V

    .line 49
    return-void
.end method
