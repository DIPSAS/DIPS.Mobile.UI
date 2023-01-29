.class public Lmono/android/runtime/InputStreamAdapter;
.super Ljava/io/InputStream;
.source "InputStreamAdapter.java"

# interfaces
.implements Lmono/android/IGCUserPeer;


# static fields
.field public static final __md_methods:Ljava/lang/String;


# instance fields
.field private refList:Ljava/util/ArrayList;


# direct methods
.method static constructor <clinit>()V
    .locals 3

    .line 12
    const-string v0, "n_close:()V:GetCloseHandler\nn_read:()I:GetReadHandler\nn_read:([B)I:GetRead_arrayBHandler\nn_read:([BII)I:GetRead_arrayBIIHandler\n"

    sput-object v0, Lmono/android/runtime/InputStreamAdapter;->__md_methods:Ljava/lang/String;

    .line 18
    const-class v1, Lmono/android/runtime/InputStreamAdapter;

    const-string v2, "Android.Runtime.InputStreamAdapter, Mono.Android"

    invoke-static {v2, v1, v0}, Lmono/android/Runtime;->register(Ljava/lang/String;Ljava/lang/Class;Ljava/lang/String;)V

    .line 19
    return-void
.end method

.method public constructor <init>()V
    .locals 3

    .line 24
    invoke-direct {p0}, Ljava/io/InputStream;-><init>()V

    .line 25
    invoke-virtual {p0}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    move-result-object v0

    const-class v1, Lmono/android/runtime/InputStreamAdapter;

    if-ne v0, v1, :cond_0

    .line 26
    const/4 v0, 0x0

    new-array v0, v0, [Ljava/lang/Object;

    const-string v1, "Android.Runtime.InputStreamAdapter, Mono.Android"

    const-string v2, ""

    invoke-static {v1, v2, p0, v0}, Lmono/android/TypeManager;->Activate(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Object;[Ljava/lang/Object;)V

    .line 28
    :cond_0
    return-void
.end method

.method private native n_close()V
.end method

.method private native n_read()I
.end method

.method private native n_read([B)I
.end method

.method private native n_read([BII)I
.end method


# virtual methods
.method public close()V
    .locals 0

    .line 33
    invoke-direct {p0}, Lmono/android/runtime/InputStreamAdapter;->n_close()V

    .line 34
    return-void
.end method

.method public monodroidAddReference(Ljava/lang/Object;)V
    .locals 1

    .line 65
    iget-object v0, p0, Lmono/android/runtime/InputStreamAdapter;->refList:Ljava/util/ArrayList;

    if-nez v0, :cond_0

    .line 66
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    iput-object v0, p0, Lmono/android/runtime/InputStreamAdapter;->refList:Ljava/util/ArrayList;

    .line 67
    :cond_0
    iget-object v0, p0, Lmono/android/runtime/InputStreamAdapter;->refList:Ljava/util/ArrayList;

    invoke-virtual {v0, p1}, Ljava/util/ArrayList;->add(Ljava/lang/Object;)Z

    .line 68
    return-void
.end method

.method public monodroidClearReferences()V
    .locals 1

    .line 72
    iget-object v0, p0, Lmono/android/runtime/InputStreamAdapter;->refList:Ljava/util/ArrayList;

    if-eqz v0, :cond_0

    .line 73
    invoke-virtual {v0}, Ljava/util/ArrayList;->clear()V

    .line 74
    :cond_0
    return-void
.end method

.method public read()I
    .locals 1

    .line 41
    invoke-direct {p0}, Lmono/android/runtime/InputStreamAdapter;->n_read()I

    move-result v0

    return v0
.end method

.method public read([B)I
    .locals 0

    .line 49
    invoke-direct {p0, p1}, Lmono/android/runtime/InputStreamAdapter;->n_read([B)I

    move-result p1

    return p1
.end method

.method public read([BII)I
    .locals 0

    .line 57
    invoke-direct {p0, p1, p2, p3}, Lmono/android/runtime/InputStreamAdapter;->n_read([BII)I

    move-result p1

    return p1
.end method
