.class Landroidx/core/os/TraceCompat$Api18Impl;
.super Ljava/lang/Object;
.source "TraceCompat.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Landroidx/core/os/TraceCompat;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x8
    name = "Api18Impl"
.end annotation


# direct methods
.method private constructor <init>()V
    .locals 0

    .line 219
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 221
    return-void
.end method

.method static beginSection(Ljava/lang/String;)V
    .locals 0
    .param p0, "sectionName"    # Ljava/lang/String;

    .line 225
    invoke-static {p0}, Landroid/os/Trace;->beginSection(Ljava/lang/String;)V

    .line 226
    return-void
.end method

.method static endSection()V
    .locals 0

    .line 230
    invoke-static {}, Landroid/os/Trace;->endSection()V

    .line 231
    return-void
.end method
