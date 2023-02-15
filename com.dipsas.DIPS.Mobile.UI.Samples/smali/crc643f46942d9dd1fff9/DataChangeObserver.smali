.class public Lcrc643f46942d9dd1fff9/DataChangeObserver;
.super Landroidx/recyclerview/widget/RecyclerView$AdapterDataObserver;
.source "DataChangeObserver.java"

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
    const-string v0, "n_onChanged:()V:GetOnChangedHandler\nn_onItemRangeInserted:(II)V:GetOnItemRangeInserted_IIHandler\nn_onItemRangeChanged:(II)V:GetOnItemRangeChanged_IIHandler\nn_onItemRangeChanged:(IILjava/lang/Object;)V:GetOnItemRangeChanged_IILjava_lang_Object_Handler\nn_onItemRangeRemoved:(II)V:GetOnItemRangeRemoved_IIHandler\nn_onItemRangeMoved:(III)V:GetOnItemRangeMoved_IIIHandler\n"

    sput-object v0, Lcrc643f46942d9dd1fff9/DataChangeObserver;->__md_methods:Ljava/lang/String;

    .line 20
    const-class v1, Lcrc643f46942d9dd1fff9/DataChangeObserver;

    const-string v2, "Xamarin.Forms.Platform.Android.DataChangeObserver, Xamarin.Forms.Platform.Android"

    invoke-static {v2, v1, v0}, Lmono/android/Runtime;->register(Ljava/lang/String;Ljava/lang/Class;Ljava/lang/String;)V

    .line 21
    return-void
.end method

.method public constructor <init>()V
    .locals 3

    .line 26
    invoke-direct {p0}, Landroidx/recyclerview/widget/RecyclerView$AdapterDataObserver;-><init>()V

    .line 27
    invoke-virtual {p0}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    move-result-object v0

    const-class v1, Lcrc643f46942d9dd1fff9/DataChangeObserver;

    if-ne v0, v1, :cond_0

    .line 28
    const/4 v0, 0x0

    new-array v0, v0, [Ljava/lang/Object;

    const-string v1, "Xamarin.Forms.Platform.Android.DataChangeObserver, Xamarin.Forms.Platform.Android"

    const-string v2, ""

    invoke-static {v1, v2, p0, v0}, Lmono/android/TypeManager;->Activate(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Object;[Ljava/lang/Object;)V

    .line 30
    :cond_0
    return-void
.end method

.method private native n_onChanged()V
.end method

.method private native n_onItemRangeChanged(II)V
.end method

.method private native n_onItemRangeChanged(IILjava/lang/Object;)V
.end method

.method private native n_onItemRangeInserted(II)V
.end method

.method private native n_onItemRangeMoved(III)V
.end method

.method private native n_onItemRangeRemoved(II)V
.end method


# virtual methods
.method public monodroidAddReference(Ljava/lang/Object;)V
    .locals 1

    .line 83
    iget-object v0, p0, Lcrc643f46942d9dd1fff9/DataChangeObserver;->refList:Ljava/util/ArrayList;

    if-nez v0, :cond_0

    .line 84
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    iput-object v0, p0, Lcrc643f46942d9dd1fff9/DataChangeObserver;->refList:Ljava/util/ArrayList;

    .line 85
    :cond_0
    iget-object v0, p0, Lcrc643f46942d9dd1fff9/DataChangeObserver;->refList:Ljava/util/ArrayList;

    invoke-virtual {v0, p1}, Ljava/util/ArrayList;->add(Ljava/lang/Object;)Z

    .line 86
    return-void
.end method

.method public monodroidClearReferences()V
    .locals 1

    .line 90
    iget-object v0, p0, Lcrc643f46942d9dd1fff9/DataChangeObserver;->refList:Ljava/util/ArrayList;

    if-eqz v0, :cond_0

    .line 91
    invoke-virtual {v0}, Ljava/util/ArrayList;->clear()V

    .line 92
    :cond_0
    return-void
.end method

.method public onChanged()V
    .locals 0

    .line 35
    invoke-direct {p0}, Lcrc643f46942d9dd1fff9/DataChangeObserver;->n_onChanged()V

    .line 36
    return-void
.end method

.method public onItemRangeChanged(II)V
    .locals 0

    .line 51
    invoke-direct {p0, p1, p2}, Lcrc643f46942d9dd1fff9/DataChangeObserver;->n_onItemRangeChanged(II)V

    .line 52
    return-void
.end method

.method public onItemRangeChanged(IILjava/lang/Object;)V
    .locals 0

    .line 59
    invoke-direct {p0, p1, p2, p3}, Lcrc643f46942d9dd1fff9/DataChangeObserver;->n_onItemRangeChanged(IILjava/lang/Object;)V

    .line 60
    return-void
.end method

.method public onItemRangeInserted(II)V
    .locals 0

    .line 43
    invoke-direct {p0, p1, p2}, Lcrc643f46942d9dd1fff9/DataChangeObserver;->n_onItemRangeInserted(II)V

    .line 44
    return-void
.end method

.method public onItemRangeMoved(III)V
    .locals 0

    .line 75
    invoke-direct {p0, p1, p2, p3}, Lcrc643f46942d9dd1fff9/DataChangeObserver;->n_onItemRangeMoved(III)V

    .line 76
    return-void
.end method

.method public onItemRangeRemoved(II)V
    .locals 0

    .line 67
    invoke-direct {p0, p1, p2}, Lcrc643f46942d9dd1fff9/DataChangeObserver;->n_onItemRangeRemoved(II)V

    .line 68
    return-void
.end method
