.class public Lcrc643f46942d9dd1fff9/TemplatedItemViewHolder;
.super Lcrc643f46942d9dd1fff9/SelectableViewHolder;
.source "TemplatedItemViewHolder.java"

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
    const-string v0, ""

    sput-object v0, Lcrc643f46942d9dd1fff9/TemplatedItemViewHolder;->__md_methods:Ljava/lang/String;

    .line 14
    const-class v1, Lcrc643f46942d9dd1fff9/TemplatedItemViewHolder;

    const-string v2, "Xamarin.Forms.Platform.Android.TemplatedItemViewHolder, Xamarin.Forms.Platform.Android"

    invoke-static {v2, v1, v0}, Lmono/android/Runtime;->register(Ljava/lang/String;Ljava/lang/Class;Ljava/lang/String;)V

    .line 15
    return-void
.end method

.method public constructor <init>(Landroid/view/View;)V
    .locals 2

    .line 20
    invoke-direct {p0, p1}, Lcrc643f46942d9dd1fff9/SelectableViewHolder;-><init>(Landroid/view/View;)V

    .line 21
    invoke-virtual {p0}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    move-result-object v0

    const-class v1, Lcrc643f46942d9dd1fff9/TemplatedItemViewHolder;

    if-ne v0, v1, :cond_0

    .line 22
    const/4 v0, 0x1

    new-array v0, v0, [Ljava/lang/Object;

    const/4 v1, 0x0

    aput-object p1, v0, v1

    const-string p1, "Xamarin.Forms.Platform.Android.TemplatedItemViewHolder, Xamarin.Forms.Platform.Android"

    const-string v1, "Android.Views.View, Mono.Android"

    invoke-static {p1, v1, p0, v0}, Lmono/android/TypeManager;->Activate(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Object;[Ljava/lang/Object;)V

    .line 24
    :cond_0
    return-void
.end method


# virtual methods
.method public monodroidAddReference(Ljava/lang/Object;)V
    .locals 1

    .line 29
    iget-object v0, p0, Lcrc643f46942d9dd1fff9/TemplatedItemViewHolder;->refList:Ljava/util/ArrayList;

    if-nez v0, :cond_0

    .line 30
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    iput-object v0, p0, Lcrc643f46942d9dd1fff9/TemplatedItemViewHolder;->refList:Ljava/util/ArrayList;

    .line 31
    :cond_0
    iget-object v0, p0, Lcrc643f46942d9dd1fff9/TemplatedItemViewHolder;->refList:Ljava/util/ArrayList;

    invoke-virtual {v0, p1}, Ljava/util/ArrayList;->add(Ljava/lang/Object;)Z

    .line 32
    return-void
.end method

.method public monodroidClearReferences()V
    .locals 1

    .line 36
    iget-object v0, p0, Lcrc643f46942d9dd1fff9/TemplatedItemViewHolder;->refList:Ljava/util/ArrayList;

    if-eqz v0, :cond_0

    .line 37
    invoke-virtual {v0}, Ljava/util/ArrayList;->clear()V

    .line 38
    :cond_0
    return-void
.end method
