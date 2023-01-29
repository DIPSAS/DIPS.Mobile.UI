.class public Lmono/android/incrementaldeployment/IncrementalClassLoader;
.super Ljava/lang/ClassLoader;
.source "IncrementalClassLoader.java"


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lmono/android/incrementaldeployment/IncrementalClassLoader$DelegateClassLoader;
    }
.end annotation


# instance fields
.field private final delegateClassLoader:Lmono/android/incrementaldeployment/IncrementalClassLoader$DelegateClassLoader;


# direct methods
.method public constructor <init>(Ljava/lang/ClassLoader;Ljava/lang/String;Ljava/io/File;Ljava/lang/String;Ljava/util/List;)V
    .locals 0
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/lang/ClassLoader;",
            "Ljava/lang/String;",
            "Ljava/io/File;",
            "Ljava/lang/String;",
            "Ljava/util/List<",
            "Ljava/lang/String;",
            ">;)V"
        }
    .end annotation

    .line 35
    invoke-virtual {p1}, Ljava/lang/ClassLoader;->getParent()Ljava/lang/ClassLoader;

    move-result-object p2

    invoke-direct {p0, p2}, Ljava/lang/ClassLoader;-><init>(Ljava/lang/ClassLoader;)V

    .line 40
    invoke-static {p3, p4, p5, p1}, Lmono/android/incrementaldeployment/IncrementalClassLoader;->createDelegateClassLoader(Ljava/io/File;Ljava/lang/String;Ljava/util/List;Ljava/lang/ClassLoader;)Lmono/android/incrementaldeployment/IncrementalClassLoader$DelegateClassLoader;

    move-result-object p1

    iput-object p1, p0, Lmono/android/incrementaldeployment/IncrementalClassLoader;->delegateClassLoader:Lmono/android/incrementaldeployment/IncrementalClassLoader$DelegateClassLoader;

    .line 41
    return-void
.end method

.method private static createDelegateClassLoader(Ljava/io/File;Ljava/lang/String;Ljava/util/List;Ljava/lang/ClassLoader;)Lmono/android/incrementaldeployment/IncrementalClassLoader$DelegateClassLoader;
    .locals 8
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/io/File;",
            "Ljava/lang/String;",
            "Ljava/util/List<",
            "Ljava/lang/String;",
            ">;",
            "Ljava/lang/ClassLoader;",
            ")",
            "Lmono/android/incrementaldeployment/IncrementalClassLoader$DelegateClassLoader;"
        }
    .end annotation

    .line 65
    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    .line 66
    nop

    .line 67
    invoke-interface {p2}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object p2

    const/4 v1, 0x1

    :goto_0
    invoke-interface {p2}, Ljava/util/Iterator;->hasNext()Z

    move-result v2

    if-eqz v2, :cond_1

    invoke-interface {p2}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Ljava/lang/String;

    .line 68
    if-eqz v1, :cond_0

    .line 69
    const/4 v1, 0x0

    goto :goto_1

    .line 71
    :cond_0
    sget-object v3, Ljava/io/File;->pathSeparator:Ljava/lang/String;

    invoke-virtual {v0, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    .line 74
    :goto_1
    invoke-virtual {v0, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    .line 75
    goto :goto_0

    .line 77
    :cond_1
    new-instance p2, Ljava/lang/StringBuilder;

    invoke-direct {p2}, Ljava/lang/StringBuilder;-><init>()V

    const-string v1, "Incremental dex path is "

    invoke-virtual {p2, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object p2

    invoke-virtual {p2, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    move-result-object p2

    invoke-virtual {p2}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p2

    const-string v1, "IncrementalClassLoader"

    invoke-static {v1, p2}, Landroid/util/Log;->v(Ljava/lang/String;Ljava/lang/String;)I

    .line 78
    new-instance p2, Ljava/lang/StringBuilder;

    invoke-direct {p2}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "Native lib dir is "

    invoke-virtual {p2, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object p2

    invoke-virtual {p2, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object p2

    invoke-virtual {p2}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p2

    invoke-static {v1, p2}, Landroid/util/Log;->v(Ljava/lang/String;Ljava/lang/String;)I

    .line 79
    new-instance p2, Lmono/android/incrementaldeployment/IncrementalClassLoader$DelegateClassLoader;

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v3

    const/4 v7, 0x0

    move-object v2, p2

    move-object v4, p0

    move-object v5, p1

    move-object v6, p3

    invoke-direct/range {v2 .. v7}, Lmono/android/incrementaldeployment/IncrementalClassLoader$DelegateClassLoader;-><init>(Ljava/lang/String;Ljava/io/File;Ljava/lang/String;Ljava/lang/ClassLoader;Lmono/android/incrementaldeployment/IncrementalClassLoader$1;)V

    return-object p2
.end method

.method public static inject(Ljava/lang/ClassLoader;Ljava/lang/String;Ljava/io/File;Ljava/lang/String;Ljava/util/List;)V
    .locals 7
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/lang/ClassLoader;",
            "Ljava/lang/String;",
            "Ljava/io/File;",
            "Ljava/lang/String;",
            "Ljava/util/List<",
            "Ljava/lang/String;",
            ">;)V"
        }
    .end annotation

    .line 100
    new-instance v6, Lmono/android/incrementaldeployment/IncrementalClassLoader;

    move-object v0, v6

    move-object v1, p0

    move-object v2, p1

    move-object v3, p2

    move-object v4, p3

    move-object v5, p4

    invoke-direct/range {v0 .. v5}, Lmono/android/incrementaldeployment/IncrementalClassLoader;-><init>(Ljava/lang/ClassLoader;Ljava/lang/String;Ljava/io/File;Ljava/lang/String;Ljava/util/List;)V

    .line 102
    invoke-static {p0, v6}, Lmono/android/incrementaldeployment/IncrementalClassLoader;->setParent(Ljava/lang/ClassLoader;Ljava/lang/ClassLoader;)V

    .line 103
    return-void
.end method

.method private static setParent(Ljava/lang/ClassLoader;Ljava/lang/ClassLoader;)V
    .locals 2

    .line 85
    :try_start_0
    const-class v0, Ljava/lang/ClassLoader;

    const-string v1, "parent"

    invoke-virtual {v0, v1}, Ljava/lang/Class;->getDeclaredField(Ljava/lang/String;)Ljava/lang/reflect/Field;

    move-result-object v0

    .line 86
    const/4 v1, 0x1

    invoke-virtual {v0, v1}, Ljava/lang/reflect/Field;->setAccessible(Z)V

    .line 87
    invoke-virtual {v0, p0, p1}, Ljava/lang/reflect/Field;->set(Ljava/lang/Object;Ljava/lang/Object;)V
    :try_end_0
    .catch Ljava/lang/IllegalArgumentException; {:try_start_0 .. :try_end_0} :catch_2
    .catch Ljava/lang/IllegalAccessException; {:try_start_0 .. :try_end_0} :catch_1
    .catch Ljava/lang/NoSuchFieldException; {:try_start_0 .. :try_end_0} :catch_0

    .line 94
    nop

    .line 95
    return-void

    .line 92
    :catch_0
    move-exception p0

    .line 93
    new-instance p1, Ljava/lang/RuntimeException;

    invoke-direct {p1, p0}, Ljava/lang/RuntimeException;-><init>(Ljava/lang/Throwable;)V

    throw p1

    .line 90
    :catch_1
    move-exception p0

    .line 91
    new-instance p1, Ljava/lang/RuntimeException;

    invoke-direct {p1, p0}, Ljava/lang/RuntimeException;-><init>(Ljava/lang/Throwable;)V

    throw p1

    .line 88
    :catch_2
    move-exception p0

    .line 89
    new-instance p1, Ljava/lang/RuntimeException;

    invoke-direct {p1, p0}, Ljava/lang/RuntimeException;-><init>(Ljava/lang/Throwable;)V

    throw p1
.end method


# virtual methods
.method public findClass(Ljava/lang/String;)Ljava/lang/Class;
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/lang/String;",
            ")",
            "Ljava/lang/Class<",
            "*>;"
        }
    .end annotation

    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/lang/ClassNotFoundException;
        }
    .end annotation

    .line 45
    iget-object v0, p0, Lmono/android/incrementaldeployment/IncrementalClassLoader;->delegateClassLoader:Lmono/android/incrementaldeployment/IncrementalClassLoader$DelegateClassLoader;

    invoke-virtual {v0, p1}, Lmono/android/incrementaldeployment/IncrementalClassLoader$DelegateClassLoader;->findClass(Ljava/lang/String;)Ljava/lang/Class;

    move-result-object p1

    return-object p1
.end method
