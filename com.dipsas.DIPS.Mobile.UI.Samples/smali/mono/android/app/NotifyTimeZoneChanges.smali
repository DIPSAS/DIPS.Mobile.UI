.class public Lmono/android/app/NotifyTimeZoneChanges;
.super Landroid/content/BroadcastReceiver;
.source "NotifyTimeZoneChanges.java"


# direct methods
.method public constructor <init>()V
    .locals 0

    .line 3
    invoke-direct {p0}, Landroid/content/BroadcastReceiver;-><init>()V

    return-void
.end method


# virtual methods
.method public onReceive(Landroid/content/Context;Landroid/content/Intent;)V
    .locals 0

    .line 6
    invoke-static {}, Lmono/android/Runtime;->notifyTimeZoneChanged()V

    .line 7
    return-void
.end method
