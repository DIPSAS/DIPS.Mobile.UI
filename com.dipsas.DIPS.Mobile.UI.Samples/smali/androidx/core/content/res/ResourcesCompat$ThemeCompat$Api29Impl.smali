.class Landroidx/core/content/res/ResourcesCompat$ThemeCompat$Api29Impl;
.super Ljava/lang/Object;
.source "ResourcesCompat.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Landroidx/core/content/res/ResourcesCompat$ThemeCompat;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x8
    name = "Api29Impl"
.end annotation


# direct methods
.method private constructor <init>()V
    .locals 0

    .line 762
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 764
    return-void
.end method

.method static rebase(Landroid/content/res/Resources$Theme;)V
    .locals 0
    .param p0, "theme"    # Landroid/content/res/Resources$Theme;

    .line 768
    invoke-virtual {p0}, Landroid/content/res/Resources$Theme;->rebase()V

    .line 769
    return-void
.end method
