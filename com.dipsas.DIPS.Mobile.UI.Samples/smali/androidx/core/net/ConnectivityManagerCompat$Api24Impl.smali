.class Landroidx/core/net/ConnectivityManagerCompat$Api24Impl;
.super Ljava/lang/Object;
.source "ConnectivityManagerCompat.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Landroidx/core/net/ConnectivityManagerCompat;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x8
    name = "Api24Impl"
.end annotation


# direct methods
.method private constructor <init>()V
    .locals 0

    .line 187
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 189
    return-void
.end method

.method static getRestrictBackgroundStatus(Landroid/net/ConnectivityManager;)I
    .locals 1
    .param p0, "connectivityManager"    # Landroid/net/ConnectivityManager;

    .line 193
    invoke-virtual {p0}, Landroid/net/ConnectivityManager;->getRestrictBackgroundStatus()I

    move-result v0

    return v0
.end method
