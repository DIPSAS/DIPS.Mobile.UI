.class Landroidx/core/content/ContextCompat$Api19Impl;
.super Ljava/lang/Object;
.source "ContextCompat.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Landroidx/core/content/ContextCompat;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x8
    name = "Api19Impl"
.end annotation


# direct methods
.method private constructor <init>()V
    .locals 0

    .line 836
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 838
    return-void
.end method

.method static getExternalCacheDirs(Landroid/content/Context;)[Ljava/io/File;
    .locals 1
    .param p0, "obj"    # Landroid/content/Context;

    .line 842
    invoke-virtual {p0}, Landroid/content/Context;->getExternalCacheDirs()[Ljava/io/File;

    move-result-object v0

    return-object v0
.end method

.method static getExternalFilesDirs(Landroid/content/Context;Ljava/lang/String;)[Ljava/io/File;
    .locals 1
    .param p0, "obj"    # Landroid/content/Context;
    .param p1, "type"    # Ljava/lang/String;

    .line 847
    invoke-virtual {p0, p1}, Landroid/content/Context;->getExternalFilesDirs(Ljava/lang/String;)[Ljava/io/File;

    move-result-object v0

    return-object v0
.end method

.method static getObbDirs(Landroid/content/Context;)[Ljava/io/File;
    .locals 1
    .param p0, "obj"    # Landroid/content/Context;

    .line 852
    invoke-virtual {p0}, Landroid/content/Context;->getObbDirs()[Ljava/io/File;

    move-result-object v0

    return-object v0
.end method
