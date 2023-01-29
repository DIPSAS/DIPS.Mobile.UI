.class public Lmono/android/Runtime;
.super Ljava/lang/Object;
.source "Runtime.java"


# static fields
.field static java_lang_Class:Ljava/lang/Class;

.field static java_lang_System:Ljava/lang/Class;

.field static java_util_TimeZone:Ljava/lang/Class;

.field static mono_android_GCUserPeer:Ljava/lang/Class;

.field static mono_android_IGCUserPeer:Ljava/lang/Class;


# direct methods
.method static constructor <clinit>()V
    .locals 2

    .line 7
    const-class v0, Ljava/lang/Class;

    sput-object v0, Lmono/android/Runtime;->java_lang_Class:Ljava/lang/Class;

    .line 8
    const-class v0, Ljava/lang/System;

    sput-object v0, Lmono/android/Runtime;->java_lang_System:Ljava/lang/Class;

    .line 9
    const-class v0, Ljava/util/TimeZone;

    sput-object v0, Lmono/android/Runtime;->java_util_TimeZone:Ljava/lang/Class;

    .line 10
    const-class v0, Lmono/android/IGCUserPeer;

    sput-object v0, Lmono/android/Runtime;->mono_android_IGCUserPeer:Ljava/lang/Class;

    .line 11
    const-class v0, Lmono/android/GCUserPeer;

    sput-object v0, Lmono/android/Runtime;->mono_android_GCUserPeer:Ljava/lang/Class;

    .line 14
    new-instance v0, Lmono/android/XamarinUncaughtExceptionHandler;

    invoke-static {}, Ljava/lang/Thread;->getDefaultUncaughtExceptionHandler()Ljava/lang/Thread$UncaughtExceptionHandler;

    move-result-object v1

    invoke-direct {v0, v1}, Lmono/android/XamarinUncaughtExceptionHandler;-><init>(Ljava/lang/Thread$UncaughtExceptionHandler;)V

    invoke-static {v0}, Ljava/lang/Thread;->setDefaultUncaughtExceptionHandler(Ljava/lang/Thread$UncaughtExceptionHandler;)V

    .line 15
    return-void
.end method

.method public constructor <init>()V
    .locals 0

    .line 6
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method

.method public static native createNewContext([Ljava/lang/String;[Ljava/lang/String;Ljava/lang/ClassLoader;)I
.end method

.method public static native createNewContextWithData([Ljava/lang/String;[Ljava/lang/String;[[B[Ljava/lang/String;Ljava/lang/ClassLoader;Z)I
.end method

.method public static native destroyContexts([I)V
.end method

.method public static native dumpTimingData()V
.end method

.method public static native init(Ljava/lang/String;[Ljava/lang/String;Ljava/lang/String;[Ljava/lang/String;Ljava/lang/ClassLoader;[Ljava/lang/String;[Ljava/lang/String;Ljava/lang/String;I[Ljava/lang/String;)V
.end method

.method public static native initInternal(Ljava/lang/String;[Ljava/lang/String;Ljava/lang/String;[Ljava/lang/String;Ljava/lang/ClassLoader;[Ljava/lang/String;IZZ)V
.end method

.method public static native notifyTimeZoneChanged()V
.end method

.method public static native propagateUncaughtException(Ljava/lang/Thread;Ljava/lang/Throwable;)V
.end method

.method public static native register(Ljava/lang/String;Ljava/lang/Class;Ljava/lang/String;)V
.end method

.method public static native switchToContext(I)V
.end method
