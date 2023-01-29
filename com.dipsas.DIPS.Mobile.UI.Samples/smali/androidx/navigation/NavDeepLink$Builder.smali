.class public final Landroidx/navigation/NavDeepLink$Builder;
.super Ljava/lang/Object;
.source "NavDeepLink.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Landroidx/navigation/NavDeepLink;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x19
    name = "Builder"
.end annotation


# instance fields
.field private mAction:Ljava/lang/String;

.field private mMimeType:Ljava/lang/String;

.field private mUriPattern:Ljava/lang/String;


# direct methods
.method constructor <init>()V
    .locals 0

    .line 382
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method

.method public static fromAction(Ljava/lang/String;)Landroidx/navigation/NavDeepLink$Builder;
    .locals 2
    .param p0, "action"    # Ljava/lang/String;

    .line 408
    invoke-virtual {p0}, Ljava/lang/String;->isEmpty()Z

    move-result v0

    if-nez v0, :cond_0

    .line 411
    new-instance v0, Landroidx/navigation/NavDeepLink$Builder;

    invoke-direct {v0}, Landroidx/navigation/NavDeepLink$Builder;-><init>()V

    .line 412
    .local v0, "builder":Landroidx/navigation/NavDeepLink$Builder;
    invoke-virtual {v0, p0}, Landroidx/navigation/NavDeepLink$Builder;->setAction(Ljava/lang/String;)Landroidx/navigation/NavDeepLink$Builder;

    .line 413
    return-object v0

    .line 409
    .end local v0    # "builder":Landroidx/navigation/NavDeepLink$Builder;
    :cond_0
    new-instance v0, Ljava/lang/IllegalArgumentException;

    const-string v1, "The NavDeepLink cannot have an empty action."

    invoke-direct {v0, v1}, Ljava/lang/IllegalArgumentException;-><init>(Ljava/lang/String;)V

    throw v0
.end method

.method public static fromMimeType(Ljava/lang/String;)Landroidx/navigation/NavDeepLink$Builder;
    .locals 1
    .param p0, "mimeType"    # Ljava/lang/String;

    .line 424
    new-instance v0, Landroidx/navigation/NavDeepLink$Builder;

    invoke-direct {v0}, Landroidx/navigation/NavDeepLink$Builder;-><init>()V

    .line 425
    .local v0, "builder":Landroidx/navigation/NavDeepLink$Builder;
    invoke-virtual {v0, p0}, Landroidx/navigation/NavDeepLink$Builder;->setMimeType(Ljava/lang/String;)Landroidx/navigation/NavDeepLink$Builder;

    .line 426
    return-object v0
.end method

.method public static fromUriPattern(Ljava/lang/String;)Landroidx/navigation/NavDeepLink$Builder;
    .locals 1
    .param p0, "uriPattern"    # Ljava/lang/String;

    .line 392
    new-instance v0, Landroidx/navigation/NavDeepLink$Builder;

    invoke-direct {v0}, Landroidx/navigation/NavDeepLink$Builder;-><init>()V

    .line 393
    .local v0, "builder":Landroidx/navigation/NavDeepLink$Builder;
    invoke-virtual {v0, p0}, Landroidx/navigation/NavDeepLink$Builder;->setUriPattern(Ljava/lang/String;)Landroidx/navigation/NavDeepLink$Builder;

    .line 394
    return-object v0
.end method


# virtual methods
.method public build()Landroidx/navigation/NavDeepLink;
    .locals 4

    .line 481
    new-instance v0, Landroidx/navigation/NavDeepLink;

    iget-object v1, p0, Landroidx/navigation/NavDeepLink$Builder;->mUriPattern:Ljava/lang/String;

    iget-object v2, p0, Landroidx/navigation/NavDeepLink$Builder;->mAction:Ljava/lang/String;

    iget-object v3, p0, Landroidx/navigation/NavDeepLink$Builder;->mMimeType:Ljava/lang/String;

    invoke-direct {v0, v1, v2, v3}, Landroidx/navigation/NavDeepLink;-><init>(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V

    return-object v0
.end method

.method public setAction(Ljava/lang/String;)Landroidx/navigation/NavDeepLink$Builder;
    .locals 2
    .param p1, "action"    # Ljava/lang/String;

    .line 454
    invoke-virtual {p1}, Ljava/lang/String;->isEmpty()Z

    move-result v0

    if-nez v0, :cond_0

    .line 457
    iput-object p1, p0, Landroidx/navigation/NavDeepLink$Builder;->mAction:Ljava/lang/String;

    .line 458
    return-object p0

    .line 455
    :cond_0
    new-instance v0, Ljava/lang/IllegalArgumentException;

    const-string v1, "The NavDeepLink cannot have an empty action."

    invoke-direct {v0, v1}, Ljava/lang/IllegalArgumentException;-><init>(Ljava/lang/String;)V

    throw v0
.end method

.method public setMimeType(Ljava/lang/String;)Landroidx/navigation/NavDeepLink$Builder;
    .locals 0
    .param p1, "mimeType"    # Ljava/lang/String;

    .line 470
    iput-object p1, p0, Landroidx/navigation/NavDeepLink$Builder;->mMimeType:Ljava/lang/String;

    .line 471
    return-object p0
.end method

.method public setUriPattern(Ljava/lang/String;)Landroidx/navigation/NavDeepLink$Builder;
    .locals 0
    .param p1, "uriPattern"    # Ljava/lang/String;

    .line 438
    iput-object p1, p0, Landroidx/navigation/NavDeepLink$Builder;->mUriPattern:Ljava/lang/String;

    .line 439
    return-object p0
.end method
