.class Landroidx/browser/browseractions/BrowserServiceFileProvider$FileCleanupTask;
.super Landroid/os/AsyncTask;
.source "BrowserServiceFileProvider.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Landroidx/browser/browseractions/BrowserServiceFileProvider;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0xa
    name = "FileCleanupTask"
.end annotation

.annotation system Ldalvik/annotation/Signature;
    value = {
        "Landroid/os/AsyncTask<",
        "Ljava/lang/Void;",
        "Ljava/lang/Void;",
        "Ljava/lang/Void;",
        ">;"
    }
.end annotation


# static fields
.field private static final CLEANUP_REQUIRED_TIME_SPAN:J

.field private static final DELETION_FAILED_REATTEMPT_DURATION:J

.field private static final IMAGE_RETENTION_DURATION:J


# instance fields
.field private final mAppContext:Landroid/content/Context;


# direct methods
.method static constructor <clinit>()V
    .locals 5

    .line 79
    sget-object v0, Ljava/util/concurrent/TimeUnit;->DAYS:Ljava/util/concurrent/TimeUnit;

    const-wide/16 v1, 0x7

    invoke-virtual {v0, v1, v2}, Ljava/util/concurrent/TimeUnit;->toMillis(J)J

    move-result-wide v3

    sput-wide v3, Landroidx/browser/browseractions/BrowserServiceFileProvider$FileCleanupTask;->IMAGE_RETENTION_DURATION:J

    .line 80
    sget-object v0, Ljava/util/concurrent/TimeUnit;->DAYS:Ljava/util/concurrent/TimeUnit;

    invoke-virtual {v0, v1, v2}, Ljava/util/concurrent/TimeUnit;->toMillis(J)J

    move-result-wide v0

    sput-wide v0, Landroidx/browser/browseractions/BrowserServiceFileProvider$FileCleanupTask;->CLEANUP_REQUIRED_TIME_SPAN:J

    .line 81
    sget-object v0, Ljava/util/concurrent/TimeUnit;->DAYS:Ljava/util/concurrent/TimeUnit;

    const-wide/16 v1, 0x1

    invoke-virtual {v0, v1, v2}, Ljava/util/concurrent/TimeUnit;->toMillis(J)J

    move-result-wide v0

    sput-wide v0, Landroidx/browser/browseractions/BrowserServiceFileProvider$FileCleanupTask;->DELETION_FAILED_REATTEMPT_DURATION:J

    return-void
.end method

.method constructor <init>(Landroid/content/Context;)V
    .locals 1
    .param p1, "context"    # Landroid/content/Context;
    .annotation system Ldalvik/annotation/MethodParameters;
        accessFlags = {
            0x0
        }
        names = {
            "context"
        }
    .end annotation

    .line 84
    invoke-direct {p0}, Landroid/os/AsyncTask;-><init>()V

    .line 85
    invoke-virtual {p1}, Landroid/content/Context;->getApplicationContext()Landroid/content/Context;

    move-result-object v0

    iput-object v0, p0, Landroidx/browser/browseractions/BrowserServiceFileProvider$FileCleanupTask;->mAppContext:Landroid/content/Context;

    .line 86
    return-void
.end method

.method private static isImageFile(Ljava/io/File;)Z
    .locals 2
    .param p0, "file"    # Ljava/io/File;
    .annotation system Ldalvik/annotation/MethodParameters;
        accessFlags = {
            0x0
        }
        names = {
            "file"
        }
    .end annotation

    .line 123
    invoke-virtual {p0}, Ljava/io/File;->getName()Ljava/lang/String;

    move-result-object v0

    .line 124
    .local v0, "filename":Ljava/lang/String;
    const-string v1, "..png"

    invoke-virtual {v0, v1}, Ljava/lang/String;->endsWith(Ljava/lang/String;)Z

    move-result v1

    return v1
.end method

.method private static shouldCleanUp(Landroid/content/SharedPreferences;)Z
    .locals 7
    .param p0, "prefs"    # Landroid/content/SharedPreferences;
    .annotation system Ldalvik/annotation/MethodParameters;
        accessFlags = {
            0x0
        }
        names = {
            "prefs"
        }
    .end annotation

    .line 128
    invoke-static {}, Ljava/lang/System;->currentTimeMillis()J

    move-result-wide v0

    const-string v2, "last_cleanup_time"

    invoke-interface {p0, v2, v0, v1}, Landroid/content/SharedPreferences;->getLong(Ljava/lang/String;J)J

    move-result-wide v0

    .line 129
    .local v0, "lastCleanup":J
    invoke-static {}, Ljava/lang/System;->currentTimeMillis()J

    move-result-wide v2

    sget-wide v4, Landroidx/browser/browseractions/BrowserServiceFileProvider$FileCleanupTask;->CLEANUP_REQUIRED_TIME_SPAN:J

    add-long/2addr v4, v0

    cmp-long v6, v2, v4

    if-lez v6, :cond_0

    const/4 v2, 0x1

    goto :goto_0

    :cond_0
    const/4 v2, 0x0

    :goto_0
    return v2
.end method


# virtual methods
.method protected bridge synthetic doInBackground([Ljava/lang/Object;)Ljava/lang/Object;
    .locals 0
    .annotation system Ldalvik/annotation/MethodParameters;
        accessFlags = {
            0x1000
        }
        names = {
            "params"
        }
    .end annotation

    .line 77
    check-cast p1, [Ljava/lang/Void;

    invoke-virtual {p0, p1}, Landroidx/browser/browseractions/BrowserServiceFileProvider$FileCleanupTask;->doInBackground([Ljava/lang/Void;)Ljava/lang/Void;

    move-result-object p1

    return-object p1
.end method

.method protected varargs doInBackground([Ljava/lang/Void;)Ljava/lang/Void;
    .locals 16
    .param p1, "params"    # [Ljava/lang/Void;
    .annotation system Ldalvik/annotation/MethodParameters;
        accessFlags = {
            0x0
        }
        names = {
            "params"
        }
    .end annotation

    .line 90
    move-object/from16 v1, p0

    iget-object v0, v1, Landroidx/browser/browseractions/BrowserServiceFileProvider$FileCleanupTask;->mAppContext:Landroid/content/Context;

    new-instance v2, Ljava/lang/StringBuilder;

    invoke-direct {v2}, Ljava/lang/StringBuilder;-><init>()V

    iget-object v3, v1, Landroidx/browser/browseractions/BrowserServiceFileProvider$FileCleanupTask;->mAppContext:Landroid/content/Context;

    .line 91
    invoke-virtual {v3}, Landroid/content/Context;->getPackageName()Ljava/lang/String;

    move-result-object v3

    invoke-virtual {v2, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v2

    const-string v3, ".image_provider"

    invoke-virtual {v2, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v2

    invoke-virtual {v2}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v2

    .line 90
    const/4 v3, 0x0

    invoke-virtual {v0, v2, v3}, Landroid/content/Context;->getSharedPreferences(Ljava/lang/String;I)Landroid/content/SharedPreferences;

    move-result-object v2

    .line 92
    .local v2, "prefs":Landroid/content/SharedPreferences;
    invoke-static {v2}, Landroidx/browser/browseractions/BrowserServiceFileProvider$FileCleanupTask;->shouldCleanUp(Landroid/content/SharedPreferences;)Z

    move-result v0

    const/4 v4, 0x0

    if-nez v0, :cond_0

    return-object v4

    .line 93
    :cond_0
    sget-object v5, Landroidx/browser/browseractions/BrowserServiceFileProvider;->sFileCleanupLock:Ljava/lang/Object;

    monitor-enter v5

    .line 94
    const/4 v0, 0x1

    .line 95
    .local v0, "allFilesDeletedSuccessfully":Z
    :try_start_0
    new-instance v6, Ljava/io/File;

    iget-object v7, v1, Landroidx/browser/browseractions/BrowserServiceFileProvider$FileCleanupTask;->mAppContext:Landroid/content/Context;

    invoke-virtual {v7}, Landroid/content/Context;->getFilesDir()Ljava/io/File;

    move-result-object v7

    const-string v8, "image_provider"

    invoke-direct {v6, v7, v8}, Ljava/io/File;-><init>(Ljava/io/File;Ljava/lang/String;)V

    .line 96
    .local v6, "path":Ljava/io/File;
    invoke-virtual {v6}, Ljava/io/File;->exists()Z

    move-result v7

    if-nez v7, :cond_1

    monitor-exit v5

    return-object v4

    .line 97
    :cond_1
    invoke-virtual {v6}, Ljava/io/File;->listFiles()[Ljava/io/File;

    move-result-object v7

    .line 98
    .local v7, "files":[Ljava/io/File;
    invoke-static {}, Ljava/lang/System;->currentTimeMillis()J

    move-result-wide v8

    sget-wide v10, Landroidx/browser/browseractions/BrowserServiceFileProvider$FileCleanupTask;->IMAGE_RETENTION_DURATION:J

    sub-long/2addr v8, v10

    .line 99
    .local v8, "retentionDate":J
    array-length v10, v7

    :goto_0
    if-ge v3, v10, :cond_4

    aget-object v11, v7, v3

    .line 100
    .local v11, "file":Ljava/io/File;
    invoke-static {v11}, Landroidx/browser/browseractions/BrowserServiceFileProvider$FileCleanupTask;->isImageFile(Ljava/io/File;)Z

    move-result v12

    if-nez v12, :cond_2

    goto :goto_1

    .line 101
    :cond_2
    invoke-virtual {v11}, Ljava/io/File;->lastModified()J

    move-result-wide v12

    .line 102
    .local v12, "lastModified":J
    cmp-long v14, v12, v8

    if-gez v14, :cond_3

    invoke-virtual {v11}, Ljava/io/File;->delete()Z

    move-result v14

    if-nez v14, :cond_3

    .line 103
    const-string v14, "BrowserServiceFP"

    new-instance v15, Ljava/lang/StringBuilder;

    invoke-direct {v15}, Ljava/lang/StringBuilder;-><init>()V

    const-string v4, "Fail to delete image: "

    invoke-virtual {v15, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v4

    invoke-virtual {v11}, Ljava/io/File;->getAbsoluteFile()Ljava/io/File;

    move-result-object v15

    invoke-virtual {v4, v15}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    move-result-object v4

    invoke-virtual {v4}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v4

    invoke-static {v14, v4}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;)I

    .line 104
    const/4 v0, 0x0

    .line 99
    .end local v11    # "file":Ljava/io/File;
    .end local v12    # "lastModified":J
    :cond_3
    :goto_1
    add-int/lit8 v3, v3, 0x1

    const/4 v4, 0x0

    goto :goto_0

    .line 109
    :cond_4
    if-eqz v0, :cond_5

    .line 110
    invoke-static {}, Ljava/lang/System;->currentTimeMillis()J

    move-result-wide v3

    .local v3, "lastCleanUpTime":J
    goto :goto_2

    .line 112
    .end local v3    # "lastCleanUpTime":J
    :cond_5
    invoke-static {}, Ljava/lang/System;->currentTimeMillis()J

    move-result-wide v3

    sget-wide v10, Landroidx/browser/browseractions/BrowserServiceFileProvider$FileCleanupTask;->CLEANUP_REQUIRED_TIME_SPAN:J

    sub-long/2addr v3, v10

    sget-wide v10, Landroidx/browser/browseractions/BrowserServiceFileProvider$FileCleanupTask;->DELETION_FAILED_REATTEMPT_DURATION:J

    add-long/2addr v3, v10

    .line 115
    .restart local v3    # "lastCleanUpTime":J
    :goto_2
    invoke-interface {v2}, Landroid/content/SharedPreferences;->edit()Landroid/content/SharedPreferences$Editor;

    move-result-object v10

    .line 116
    .local v10, "editor":Landroid/content/SharedPreferences$Editor;
    const-string v11, "last_cleanup_time"

    invoke-interface {v10, v11, v3, v4}, Landroid/content/SharedPreferences$Editor;->putLong(Ljava/lang/String;J)Landroid/content/SharedPreferences$Editor;

    .line 117
    invoke-interface {v10}, Landroid/content/SharedPreferences$Editor;->apply()V

    .line 118
    .end local v0    # "allFilesDeletedSuccessfully":Z
    .end local v3    # "lastCleanUpTime":J
    .end local v6    # "path":Ljava/io/File;
    .end local v7    # "files":[Ljava/io/File;
    .end local v8    # "retentionDate":J
    .end local v10    # "editor":Landroid/content/SharedPreferences$Editor;
    monitor-exit v5

    .line 119
    const/4 v0, 0x0

    return-object v0

    .line 118
    :catchall_0
    move-exception v0

    monitor-exit v5
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    throw v0
.end method
