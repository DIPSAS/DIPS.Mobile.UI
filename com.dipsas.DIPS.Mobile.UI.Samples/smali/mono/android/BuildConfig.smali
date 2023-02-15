.class public Lmono/android/BuildConfig;
.super Ljava/lang/Object;
.source "BuildConfig.java"


# static fields
.field public static Debug:Z

.field public static DotNetRuntime:Z


# direct methods
.method static constructor <clinit>()V
    .locals 1

    .line 4
    const/4 v0, 0x1

    sput-boolean v0, Lmono/android/BuildConfig;->Debug:Z

    .line 5
    const/4 v0, 0x0

    sput-boolean v0, Lmono/android/BuildConfig;->DotNetRuntime:Z

    return-void
.end method

.method public constructor <init>()V
    .locals 0

    .line 3
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method
