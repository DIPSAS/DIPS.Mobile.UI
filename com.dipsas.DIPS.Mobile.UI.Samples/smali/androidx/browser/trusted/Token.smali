.class public final Landroidx/browser/trusted/Token;
.super Ljava/lang/Object;
.source "Token.java"


# static fields
.field private static final TAG:Ljava/lang/String; = "Token"


# instance fields
.field private final mContents:Landroidx/browser/trusted/TokenContents;


# direct methods
.method private constructor <init>(Landroidx/browser/trusted/TokenContents;)V
    .locals 0
    .param p1, "contents"    # Landroidx/browser/trusted/TokenContents;
    .annotation system Ldalvik/annotation/MethodParameters;
        accessFlags = {
            0x0
        }
        names = {
            "contents"
        }
    .end annotation

    .line 84
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 85
    iput-object p1, p0, Landroidx/browser/trusted/Token;->mContents:Landroidx/browser/trusted/TokenContents;

    .line 86
    return-void
.end method

.method public static create(Ljava/lang/String;Landroid/content/pm/PackageManager;)Landroidx/browser/trusted/Token;
    .locals 5
    .param p0, "packageName"    # Ljava/lang/String;
    .param p1, "packageManager"    # Landroid/content/pm/PackageManager;
    .annotation system Ldalvik/annotation/MethodParameters;
        accessFlags = {
            0x0,
            0x0
        }
        names = {
            "packageName",
            "packageManager"
        }
    .end annotation

    .line 62
    nop

    .line 63
    invoke-static {p0, p1}, Landroidx/browser/trusted/PackageIdentityUtils;->getFingerprintsForPackage(Ljava/lang/String;Landroid/content/pm/PackageManager;)Ljava/util/List;

    move-result-object v0

    .line 64
    .local v0, "fingerprints":Ljava/util/List;, "Ljava/util/List<[B>;"
    const/4 v1, 0x0

    if-nez v0, :cond_0

    return-object v1

    .line 67
    :cond_0
    :try_start_0
    new-instance v2, Landroidx/browser/trusted/Token;

    invoke-static {p0, v0}, Landroidx/browser/trusted/TokenContents;->create(Ljava/lang/String;Ljava/util/List;)Landroidx/browser/trusted/TokenContents;

    move-result-object v3

    invoke-direct {v2, v3}, Landroidx/browser/trusted/Token;-><init>(Landroidx/browser/trusted/TokenContents;)V
    :try_end_0
    .catch Ljava/io/IOException; {:try_start_0 .. :try_end_0} :catch_0

    return-object v2

    .line 68
    :catch_0
    move-exception v2

    .line 69
    .local v2, "e":Ljava/io/IOException;
    const-string v3, "Token"

    const-string v4, "Exception when creating token."

    invoke-static {v3, v4, v2}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)I

    .line 70
    return-object v1
.end method

.method public static deserialize([B)Landroidx/browser/trusted/Token;
    .locals 2
    .param p0, "serialized"    # [B
    .annotation system Ldalvik/annotation/MethodParameters;
        accessFlags = {
            0x0
        }
        names = {
            "serialized"
        }
    .end annotation

    .line 81
    new-instance v0, Landroidx/browser/trusted/Token;

    invoke-static {p0}, Landroidx/browser/trusted/TokenContents;->deserialize([B)Landroidx/browser/trusted/TokenContents;

    move-result-object v1

    invoke-direct {v0, v1}, Landroidx/browser/trusted/Token;-><init>(Landroidx/browser/trusted/TokenContents;)V

    return-object v0
.end method


# virtual methods
.method public matches(Ljava/lang/String;Landroid/content/pm/PackageManager;)Z
    .locals 1
    .param p1, "packageName"    # Ljava/lang/String;
    .param p2, "packageManager"    # Landroid/content/pm/PackageManager;
    .annotation system Ldalvik/annotation/MethodParameters;
        accessFlags = {
            0x0,
            0x0
        }
        names = {
            "packageName",
            "packageManager"
        }
    .end annotation

    .line 105
    iget-object v0, p0, Landroidx/browser/trusted/Token;->mContents:Landroidx/browser/trusted/TokenContents;

    invoke-static {p1, p2, v0}, Landroidx/browser/trusted/PackageIdentityUtils;->packageMatchesToken(Ljava/lang/String;Landroid/content/pm/PackageManager;Landroidx/browser/trusted/TokenContents;)Z

    move-result v0

    return v0
.end method

.method public serialize()[B
    .locals 1

    .line 95
    iget-object v0, p0, Landroidx/browser/trusted/Token;->mContents:Landroidx/browser/trusted/TokenContents;

    invoke-virtual {v0}, Landroidx/browser/trusted/TokenContents;->serialize()[B

    move-result-object v0

    return-object v0
.end method
