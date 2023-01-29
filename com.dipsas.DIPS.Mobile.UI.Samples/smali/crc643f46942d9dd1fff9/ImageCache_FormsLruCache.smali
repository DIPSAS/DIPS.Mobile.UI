.class public Lcrc643f46942d9dd1fff9/ImageCache_FormsLruCache;
.super Landroid/util/LruCache;
.source "ImageCache_FormsLruCache.java"

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
    const-string v0, "n_sizeOf:(Ljava/lang/Object;Ljava/lang/Object;)I:GetSizeOf_Ljava_lang_Object_Ljava_lang_Object_Handler\n"

    sput-object v0, Lcrc643f46942d9dd1fff9/ImageCache_FormsLruCache;->__md_methods:Ljava/lang/String;

    .line 15
    const-class v1, Lcrc643f46942d9dd1fff9/ImageCache_FormsLruCache;

    const-string v2, "Xamarin.Forms.Platform.Android.ImageCache+FormsLruCache, Xamarin.Forms.Platform.Android"

    invoke-static {v2, v1, v0}, Lmono/android/Runtime;->register(Ljava/lang/String;Ljava/lang/Class;Ljava/lang/String;)V

    .line 16
    return-void
.end method

.method public constructor <init>(I)V
    .locals 2

    .line 21
    invoke-direct {p0, p1}, Landroid/util/LruCache;-><init>(I)V

    .line 22
    invoke-virtual {p0}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    move-result-object v0

    const-class v1, Lcrc643f46942d9dd1fff9/ImageCache_FormsLruCache;

    if-ne v0, v1, :cond_0

    .line 23
    const/4 v0, 0x1

    new-array v0, v0, [Ljava/lang/Object;

    const/4 v1, 0x0

    invoke-static {p1}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object p1

    aput-object p1, v0, v1

    const-string p1, "Xamarin.Forms.Platform.Android.ImageCache+FormsLruCache, Xamarin.Forms.Platform.Android"

    const-string v1, "System.Int32, mscorlib"

    invoke-static {p1, v1, p0, v0}, Lmono/android/TypeManager;->Activate(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Object;[Ljava/lang/Object;)V

    .line 25
    :cond_0
    return-void
.end method

.method private native n_sizeOf(Ljava/lang/Object;Ljava/lang/Object;)I
.end method


# virtual methods
.method public monodroidAddReference(Ljava/lang/Object;)V
    .locals 1

    .line 38
    iget-object v0, p0, Lcrc643f46942d9dd1fff9/ImageCache_FormsLruCache;->refList:Ljava/util/ArrayList;

    if-nez v0, :cond_0

    .line 39
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    iput-object v0, p0, Lcrc643f46942d9dd1fff9/ImageCache_FormsLruCache;->refList:Ljava/util/ArrayList;

    .line 40
    :cond_0
    iget-object v0, p0, Lcrc643f46942d9dd1fff9/ImageCache_FormsLruCache;->refList:Ljava/util/ArrayList;

    invoke-virtual {v0, p1}, Ljava/util/ArrayList;->add(Ljava/lang/Object;)Z

    .line 41
    return-void
.end method

.method public monodroidClearReferences()V
    .locals 1

    .line 45
    iget-object v0, p0, Lcrc643f46942d9dd1fff9/ImageCache_FormsLruCache;->refList:Ljava/util/ArrayList;

    if-eqz v0, :cond_0

    .line 46
    invoke-virtual {v0}, Ljava/util/ArrayList;->clear()V

    .line 47
    :cond_0
    return-void
.end method

.method public sizeOf(Ljava/lang/Object;Ljava/lang/Object;)I
    .locals 0

    .line 30
    invoke-direct {p0, p1, p2}, Lcrc643f46942d9dd1fff9/ImageCache_FormsLruCache;->n_sizeOf(Ljava/lang/Object;Ljava/lang/Object;)I

    move-result p1

    return p1
.end method
