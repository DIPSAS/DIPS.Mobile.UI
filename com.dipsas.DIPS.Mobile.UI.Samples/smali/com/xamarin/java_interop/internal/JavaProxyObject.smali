.class final Lcom/xamarin/java_interop/internal/JavaProxyObject;
.super Ljava/lang/Object;
.source "JavaProxyObject.java"

# interfaces
.implements Lcom/xamarin/java_interop/GCUserPeerable;


# static fields
.field static final assemblyQualifiedName:Ljava/lang/String; = "Java.Interop.JavaProxyObject, Java.Interop"


# instance fields
.field managedReferences:Ljava/util/ArrayList;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/ArrayList<",
            "Ljava/lang/Object;",
            ">;"
        }
    .end annotation
.end field


# direct methods
.method static constructor <clinit>()V
    .locals 3

    .line 13
    const-class v0, Lcom/xamarin/java_interop/internal/JavaProxyObject;

    const-string v1, "Java.Interop.JavaProxyObject, Java.Interop"

    const-string v2, ""

    invoke-static {v0, v1, v2}, Lcom/xamarin/java_interop/ManagedPeer;->registerNativeMembers(Ljava/lang/Class;Ljava/lang/String;Ljava/lang/String;)V

    .line 17
    return-void
.end method

.method constructor <init>()V
    .locals 1

    .line 7
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 19
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    iput-object v0, p0, Lcom/xamarin/java_interop/internal/JavaProxyObject;->managedReferences:Ljava/util/ArrayList;

    return-void
.end method


# virtual methods
.method public native equals(Ljava/lang/Object;)Z
.end method

.method public native hashCode()I
.end method

.method public jiAddManagedReference(Ljava/lang/Object;)V
    .locals 1

    .line 32
    iget-object v0, p0, Lcom/xamarin/java_interop/internal/JavaProxyObject;->managedReferences:Ljava/util/ArrayList;

    invoke-virtual {v0, p1}, Ljava/util/ArrayList;->add(Ljava/lang/Object;)Z

    .line 33
    return-void
.end method

.method public jiClearManagedReferences()V
    .locals 1

    .line 37
    iget-object v0, p0, Lcom/xamarin/java_interop/internal/JavaProxyObject;->managedReferences:Ljava/util/ArrayList;

    invoke-virtual {v0}, Ljava/util/ArrayList;->clear()V

    .line 38
    return-void
.end method

.method public native toString()Ljava/lang/String;
.end method
