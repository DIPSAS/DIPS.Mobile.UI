.class public Lmono/MonoPackageManager;
.super Ljava/lang/Object;
.source "MonoPackageManager.java"


# static fields
.field static Context:Landroid/content/Context;

.field static initialized:Z

.field static lock:Ljava/lang/Object;


# direct methods
.method static constructor <clinit>()V
    .locals 1

    .line 21
    new-instance v0, Ljava/lang/Object;

    invoke-direct {v0}, Ljava/lang/Object;-><init>()V

    sput-object v0, Lmono/MonoPackageManager;->lock:Ljava/lang/Object;

    return-void
.end method

.method public constructor <init>()V
    .locals 0

    .line 19
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method

.method public static LoadApplication(Landroid/content/Context;Landroid/content/pm/ApplicationInfo;[Ljava/lang/String;)V
    .locals 11

    .line 28
    sget-object v0, Lmono/MonoPackageManager;->lock:Ljava/lang/Object;

    monitor-enter v0

    .line 29
    :try_start_0
    instance-of v1, p0, Landroid/app/Application;

    if-eqz v1, :cond_0

    .line 30
    sput-object p0, Lmono/MonoPackageManager;->Context:Landroid/content/Context;

    .line 32
    :cond_0
    sget-boolean v1, Lmono/MonoPackageManager;->initialized:Z

    if-nez v1, :cond_5

    .line 33
    new-instance v1, Landroid/content/IntentFilter;

    const-string v2, "android.intent.action.TIMEZONE_CHANGED"

    invoke-direct {v1, v2}, Landroid/content/IntentFilter;-><init>(Ljava/lang/String;)V

    .line 36
    new-instance v2, Lmono/android/app/NotifyTimeZoneChanges;

    invoke-direct {v2}, Lmono/android/app/NotifyTimeZoneChanges;-><init>()V

    invoke-virtual {p0, v2, v1}, Landroid/content/Context;->registerReceiver(Landroid/content/BroadcastReceiver;Landroid/content/IntentFilter;)Landroid/content/Intent;

    .line 38
    invoke-static {}, Ljava/util/Locale;->getDefault()Ljava/util/Locale;

    move-result-object v1

    .line 39
    new-instance v2, Ljava/lang/StringBuilder;

    invoke-direct {v2}, Ljava/lang/StringBuilder;-><init>()V

    invoke-virtual {v1}, Ljava/util/Locale;->getLanguage()Ljava/lang/String;

    move-result-object v3

    invoke-virtual {v2, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v2

    const-string v3, "-"

    invoke-virtual {v2, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v2

    invoke-virtual {v1}, Ljava/util/Locale;->getCountry()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v2, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v1

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v2

    .line 40
    invoke-virtual {p0}, Landroid/content/Context;->getFilesDir()Ljava/io/File;

    move-result-object v1

    invoke-virtual {v1}, Ljava/io/File;->getAbsolutePath()Ljava/lang/String;

    move-result-object v1

    .line 41
    invoke-virtual {p0}, Landroid/content/Context;->getCacheDir()Ljava/io/File;

    move-result-object v3

    invoke-virtual {v3}, Ljava/io/File;->getAbsolutePath()Ljava/lang/String;

    move-result-object v3

    .line 42
    invoke-static {p0}, Lmono/MonoPackageManager;->getNativeLibraryPath(Landroid/content/Context;)Ljava/lang/String;

    move-result-object v4

    .line 43
    invoke-virtual {p0}, Landroid/content/Context;->getClassLoader()Ljava/lang/ClassLoader;

    move-result-object v6

    .line 44
    invoke-static {p1}, Lmono/MonoPackageManager;->getNativeLibraryPath(Landroid/content/pm/ApplicationInfo;)Ljava/lang/String;

    move-result-object p0

    .line 49
    const/4 v5, 0x3

    new-array v5, v5, [Ljava/lang/String;

    const/4 v7, 0x0

    aput-object v1, v5, v7

    const/4 v1, 0x1

    aput-object v3, v5, v1

    const/4 v3, 0x2

    aput-object v4, v5, v3

    .line 50
    nop

    .line 52
    sget v3, Landroid/os/Build$VERSION;->SDK_INT:I

    const/16 v4, 0x15

    if-lt v3, v4, :cond_2

    .line 53
    iget-object v3, p1, Landroid/content/pm/ApplicationInfo;->splitSourceDirs:[Ljava/lang/String;

    if-eqz v3, :cond_2

    .line 54
    iget-object p1, p1, Landroid/content/pm/ApplicationInfo;->splitSourceDirs:[Ljava/lang/String;

    array-length p1, p1

    if-le p1, v1, :cond_1

    const/4 v7, 0x1

    :cond_1
    move v10, v7

    goto :goto_0

    .line 85
    :cond_2
    const/4 v10, 0x0

    :goto_0
    sget-boolean p1, Lmono/android/BuildConfig;->Debug:Z

    if-eqz p1, :cond_3

    .line 86
    const-string p1, "xamarin-debug-app-helper"

    invoke-static {p1}, Ljava/lang/System;->loadLibrary(Ljava/lang/String;)V

    .line 87
    invoke-static {p2, p0, v5, v10}, Lmono/android/DebugRuntime;->init([Ljava/lang/String;Ljava/lang/String;[Ljava/lang/String;Z)V

    goto :goto_1

    .line 89
    :cond_3
    const-string p1, "monosgen-2.0"

    invoke-static {p1}, Ljava/lang/System;->loadLibrary(Ljava/lang/String;)V

    .line 91
    :goto_1
    const-string p1, "xamarin-app"

    invoke-static {p1}, Ljava/lang/System;->loadLibrary(Ljava/lang/String;)V

    .line 93
    sget-boolean p1, Lmono/android/BuildConfig;->DotNetRuntime:Z

    if-nez p1, :cond_4

    .line 95
    const-string p1, "mono-native"

    invoke-static {p1}, Ljava/lang/System;->loadLibrary(Ljava/lang/String;)V

    goto :goto_2

    .line 99
    :cond_4
    const-string p1, "System.Security.Cryptography.Native.Android"

    invoke-static {p1}, Ljava/lang/System;->loadLibrary(Ljava/lang/String;)V

    .line 102
    :goto_2
    const-string p1, "monodroid"

    invoke-static {p1}, Ljava/lang/System;->loadLibrary(Ljava/lang/String;)V

    .line 104
    sget-object v7, Lmono/MonoPackageManager_Resources;->Assemblies:[Ljava/lang/String;

    sget v8, Landroid/os/Build$VERSION;->SDK_INT:I

    .line 112
    invoke-static {}, Lmono/MonoPackageManager;->isEmulator()Z

    move-result v9

    .line 104
    move-object v3, p2

    move-object v4, p0

    invoke-static/range {v2 .. v10}, Lmono/android/Runtime;->initInternal(Ljava/lang/String;[Ljava/lang/String;Ljava/lang/String;[Ljava/lang/String;Ljava/lang/ClassLoader;[Ljava/lang/String;IZZ)V

    .line 116
    invoke-static {}, Lmono/android/app/ApplicationRegistration;->registerApplications()V

    .line 118
    sput-boolean v1, Lmono/MonoPackageManager;->initialized:Z

    .line 120
    :cond_5
    monitor-exit v0

    .line 121
    return-void

    .line 120
    :catchall_0
    move-exception p0

    monitor-exit v0
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    throw p0
.end method

.method public static getAssemblies()[Ljava/lang/String;
    .locals 1

    .line 158
    sget-object v0, Lmono/MonoPackageManager_Resources;->Assemblies:[Ljava/lang/String;

    return-object v0
.end method

.method public static getDependencies()[Ljava/lang/String;
    .locals 1

    .line 163
    sget-object v0, Lmono/MonoPackageManager_Resources;->Dependencies:[Ljava/lang/String;

    return-object v0
.end method

.method static getNativeLibraryPath(Landroid/content/Context;)Ljava/lang/String;
    .locals 0

    .line 146
    invoke-virtual {p0}, Landroid/content/Context;->getApplicationInfo()Landroid/content/pm/ApplicationInfo;

    move-result-object p0

    invoke-static {p0}, Lmono/MonoPackageManager;->getNativeLibraryPath(Landroid/content/pm/ApplicationInfo;)Ljava/lang/String;

    move-result-object p0

    return-object p0
.end method

.method static getNativeLibraryPath(Landroid/content/pm/ApplicationInfo;)Ljava/lang/String;
    .locals 2

    .line 151
    sget v0, Landroid/os/Build$VERSION;->SDK_INT:I

    const/16 v1, 0x9

    if-lt v0, v1, :cond_0

    .line 152
    iget-object p0, p0, Landroid/content/pm/ApplicationInfo;->nativeLibraryDir:Ljava/lang/String;

    return-object p0

    .line 153
    :cond_0
    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    iget-object p0, p0, Landroid/content/pm/ApplicationInfo;->dataDir:Ljava/lang/String;

    invoke-virtual {v0, p0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object p0

    const-string v0, "/lib"

    invoke-virtual {p0, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object p0

    invoke-virtual {p0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p0

    return-object p0
.end method

.method static isEmulator()Z
    .locals 2

    .line 130
    sget-object v0, Landroid/os/Build;->HARDWARE:Ljava/lang/String;

    .line 133
    const-string v1, "ranchu"

    invoke-virtual {v0, v1}, Ljava/lang/String;->contains(Ljava/lang/CharSequence;)Z

    move-result v1

    if-nez v1, :cond_1

    const-string v1, "goldfish"

    invoke-virtual {v0, v1}, Ljava/lang/String;->contains(Ljava/lang/CharSequence;)Z

    move-result v0

    if-eqz v0, :cond_0

    goto :goto_0

    .line 136
    :cond_0
    const/4 v0, 0x0

    return v0

    .line 134
    :cond_1
    :goto_0
    const/4 v0, 0x1

    return v0
.end method

.method public static setContext(Landroid/content/Context;)V
    .locals 0

    .line 142
    return-void
.end method
