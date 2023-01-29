.class public Lmono/MonoRuntimeProvider;
.super Landroid/content/ContentProvider;
.source "MonoRuntimeProvider.java"


# direct methods
.method public constructor <init>()V
    .locals 0

    .line 9
    invoke-direct {p0}, Landroid/content/ContentProvider;-><init>()V

    .line 10
    return-void
.end method


# virtual methods
.method public attachInfo(Landroid/content/Context;Landroid/content/pm/ProviderInfo;)V
    .locals 6

    .line 22
    invoke-virtual {p1}, Landroid/content/Context;->getApplicationInfo()Landroid/content/pm/ApplicationInfo;

    move-result-object v0

    .line 23
    nop

    .line 24
    sget v1, Landroid/os/Build$VERSION;->SDK_INT:I

    const/4 v2, 0x0

    const/4 v3, 0x1

    const/16 v4, 0x15

    if-lt v1, v4, :cond_0

    .line 25
    iget-object v1, v0, Landroid/content/pm/ApplicationInfo;->splitPublicSourceDirs:[Ljava/lang/String;

    .line 26
    if-eqz v1, :cond_0

    array-length v4, v1

    if-lez v4, :cond_0

    .line 27
    array-length v4, v1

    add-int/2addr v4, v3

    new-array v4, v4, [Ljava/lang/String;

    .line 28
    iget-object v5, v0, Landroid/content/pm/ApplicationInfo;->sourceDir:Ljava/lang/String;

    aput-object v5, v4, v2

    .line 29
    array-length v5, v1

    invoke-static {v1, v2, v4, v3, v5}, Ljava/lang/System;->arraycopy(Ljava/lang/Object;ILjava/lang/Object;II)V

    goto :goto_0

    .line 32
    :cond_0
    const/4 v4, 0x0

    :goto_0
    if-nez v4, :cond_1

    .line 33
    new-array v4, v3, [Ljava/lang/String;

    iget-object v1, v0, Landroid/content/pm/ApplicationInfo;->sourceDir:Ljava/lang/String;

    aput-object v1, v4, v2

    .line 35
    :cond_1
    invoke-static {p1, v0, v4}, Lmono/MonoPackageManager;->LoadApplication(Landroid/content/Context;Landroid/content/pm/ApplicationInfo;[Ljava/lang/String;)V

    .line 37
    invoke-super {p0, p1, p2}, Landroid/content/ContentProvider;->attachInfo(Landroid/content/Context;Landroid/content/pm/ProviderInfo;)V

    .line 38
    return-void
.end method

.method public delete(Landroid/net/Uri;Ljava/lang/String;[Ljava/lang/String;)I
    .locals 0

    .line 61
    new-instance p1, Ljava/lang/RuntimeException;

    const-string p2, "This operation is not supported."

    invoke-direct {p1, p2}, Ljava/lang/RuntimeException;-><init>(Ljava/lang/String;)V

    throw p1
.end method

.method public getType(Landroid/net/Uri;)Ljava/lang/String;
    .locals 1

    .line 49
    new-instance p1, Ljava/lang/RuntimeException;

    const-string v0, "This operation is not supported."

    invoke-direct {p1, v0}, Ljava/lang/RuntimeException;-><init>(Ljava/lang/String;)V

    throw p1
.end method

.method public insert(Landroid/net/Uri;Landroid/content/ContentValues;)Landroid/net/Uri;
    .locals 0

    .line 55
    new-instance p1, Ljava/lang/RuntimeException;

    const-string p2, "This operation is not supported."

    invoke-direct {p1, p2}, Ljava/lang/RuntimeException;-><init>(Ljava/lang/String;)V

    throw p1
.end method

.method public onCreate()Z
    .locals 1

    .line 15
    const/4 v0, 0x1

    return v0
.end method

.method public query(Landroid/net/Uri;[Ljava/lang/String;Ljava/lang/String;[Ljava/lang/String;Ljava/lang/String;)Landroid/database/Cursor;
    .locals 0

    .line 43
    new-instance p1, Ljava/lang/RuntimeException;

    const-string p2, "This operation is not supported."

    invoke-direct {p1, p2}, Ljava/lang/RuntimeException;-><init>(Ljava/lang/String;)V

    throw p1
.end method

.method public update(Landroid/net/Uri;Landroid/content/ContentValues;Ljava/lang/String;[Ljava/lang/String;)I
    .locals 0

    .line 67
    new-instance p1, Ljava/lang/RuntimeException;

    const-string p2, "This operation is not supported."

    invoke-direct {p1, p2}, Ljava/lang/RuntimeException;-><init>(Ljava/lang/String;)V

    throw p1
.end method
