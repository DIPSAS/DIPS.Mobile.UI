.class Landroidx/core/content/ContextCompat$Api21Impl;
.super Ljava/lang/Object;
.source "ContextCompat.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Landroidx/core/content/ContextCompat;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x8
    name = "Api21Impl"
.end annotation


# direct methods
.method private constructor <init>()V
    .locals 0

    .line 858
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 860
    return-void
.end method

.method static getCodeCacheDir(Landroid/content/Context;)Ljava/io/File;
    .locals 1
    .param p0, "obj"    # Landroid/content/Context;

    .line 874
    invoke-virtual {p0}, Landroid/content/Context;->getCodeCacheDir()Ljava/io/File;

    move-result-object v0

    return-object v0
.end method

.method static getDrawable(Landroid/content/Context;I)Landroid/graphics/drawable/Drawable;
    .locals 1
    .param p0, "obj"    # Landroid/content/Context;
    .param p1, "id"    # I

    .line 864
    invoke-virtual {p0, p1}, Landroid/content/Context;->getDrawable(I)Landroid/graphics/drawable/Drawable;

    move-result-object v0

    return-object v0
.end method

.method static getNoBackupFilesDir(Landroid/content/Context;)Ljava/io/File;
    .locals 1
    .param p0, "obj"    # Landroid/content/Context;

    .line 869
    invoke-virtual {p0}, Landroid/content/Context;->getNoBackupFilesDir()Ljava/io/File;

    move-result-object v0

    return-object v0
.end method
