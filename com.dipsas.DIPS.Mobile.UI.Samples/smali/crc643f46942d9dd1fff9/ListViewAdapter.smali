.class public Lcrc643f46942d9dd1fff9/ListViewAdapter;
.super Lcrc643f46942d9dd1fff9/CellAdapter;
.source "ListViewAdapter.java"

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
    const-string v0, "n_getCount:()I:GetGetCountHandler\nn_hasStableIds:()Z:GetHasStableIdsHandler\nn_getItem:(I)Ljava/lang/Object;:GetGetItem_IHandler\nn_getViewTypeCount:()I:GetGetViewTypeCountHandler\nn_areAllItemsEnabled:()Z:GetAreAllItemsEnabledHandler\nn_getItemId:(I)J:GetGetItemId_IHandler\nn_getItemViewType:(I)I:GetGetItemViewType_IHandler\nn_getView:(ILandroid/view/View;Landroid/view/ViewGroup;)Landroid/view/View;:GetGetView_ILandroid_view_View_Landroid_view_ViewGroup_Handler\nn_isEnabled:(I)Z:GetIsEnabled_IHandler\n"

    sput-object v0, Lcrc643f46942d9dd1fff9/ListViewAdapter;->__md_methods:Ljava/lang/String;

    .line 23
    const-class v1, Lcrc643f46942d9dd1fff9/ListViewAdapter;

    const-string v2, "Xamarin.Forms.Platform.Android.ListViewAdapter, Xamarin.Forms.Platform.Android"

    invoke-static {v2, v1, v0}, Lmono/android/Runtime;->register(Ljava/lang/String;Ljava/lang/Class;Ljava/lang/String;)V

    .line 24
    return-void
.end method

.method public constructor <init>()V
    .locals 3

    .line 29
    invoke-direct {p0}, Lcrc643f46942d9dd1fff9/CellAdapter;-><init>()V

    .line 30
    invoke-virtual {p0}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    move-result-object v0

    const-class v1, Lcrc643f46942d9dd1fff9/ListViewAdapter;

    if-ne v0, v1, :cond_0

    .line 31
    const/4 v0, 0x0

    new-array v0, v0, [Ljava/lang/Object;

    const-string v1, "Xamarin.Forms.Platform.Android.ListViewAdapter, Xamarin.Forms.Platform.Android"

    const-string v2, ""

    invoke-static {v1, v2, p0, v0}, Lmono/android/TypeManager;->Activate(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Object;[Ljava/lang/Object;)V

    .line 33
    :cond_0
    return-void
.end method

.method public constructor <init>(Landroid/content/Context;)V
    .locals 2

    .line 37
    invoke-direct {p0}, Lcrc643f46942d9dd1fff9/CellAdapter;-><init>()V

    .line 38
    invoke-virtual {p0}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    move-result-object v0

    const-class v1, Lcrc643f46942d9dd1fff9/ListViewAdapter;

    if-ne v0, v1, :cond_0

    .line 39
    const/4 v0, 0x1

    new-array v0, v0, [Ljava/lang/Object;

    const/4 v1, 0x0

    aput-object p1, v0, v1

    const-string p1, "Xamarin.Forms.Platform.Android.ListViewAdapter, Xamarin.Forms.Platform.Android"

    const-string v1, "Android.Content.Context, Mono.Android"

    invoke-static {p1, v1, p0, v0}, Lmono/android/TypeManager;->Activate(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Object;[Ljava/lang/Object;)V

    .line 41
    :cond_0
    return-void
.end method

.method private native n_areAllItemsEnabled()Z
.end method

.method private native n_getCount()I
.end method

.method private native n_getItem(I)Ljava/lang/Object;
.end method

.method private native n_getItemId(I)J
.end method

.method private native n_getItemViewType(I)I
.end method

.method private native n_getView(ILandroid/view/View;Landroid/view/ViewGroup;)Landroid/view/View;
.end method

.method private native n_getViewTypeCount()I
.end method

.method private native n_hasStableIds()Z
.end method

.method private native n_isEnabled(I)Z
.end method


# virtual methods
.method public areAllItemsEnabled()Z
    .locals 1

    .line 78
    invoke-direct {p0}, Lcrc643f46942d9dd1fff9/ListViewAdapter;->n_areAllItemsEnabled()Z

    move-result v0

    return v0
.end method

.method public getCount()I
    .locals 1

    .line 46
    invoke-direct {p0}, Lcrc643f46942d9dd1fff9/ListViewAdapter;->n_getCount()I

    move-result v0

    return v0
.end method

.method public getItem(I)Ljava/lang/Object;
    .locals 0

    .line 62
    invoke-direct {p0, p1}, Lcrc643f46942d9dd1fff9/ListViewAdapter;->n_getItem(I)Ljava/lang/Object;

    move-result-object p1

    return-object p1
.end method

.method public getItemId(I)J
    .locals 2

    .line 86
    invoke-direct {p0, p1}, Lcrc643f46942d9dd1fff9/ListViewAdapter;->n_getItemId(I)J

    move-result-wide v0

    return-wide v0
.end method

.method public getItemViewType(I)I
    .locals 0

    .line 94
    invoke-direct {p0, p1}, Lcrc643f46942d9dd1fff9/ListViewAdapter;->n_getItemViewType(I)I

    move-result p1

    return p1
.end method

.method public getView(ILandroid/view/View;Landroid/view/ViewGroup;)Landroid/view/View;
    .locals 0

    .line 102
    invoke-direct {p0, p1, p2, p3}, Lcrc643f46942d9dd1fff9/ListViewAdapter;->n_getView(ILandroid/view/View;Landroid/view/ViewGroup;)Landroid/view/View;

    move-result-object p1

    return-object p1
.end method

.method public getViewTypeCount()I
    .locals 1

    .line 70
    invoke-direct {p0}, Lcrc643f46942d9dd1fff9/ListViewAdapter;->n_getViewTypeCount()I

    move-result v0

    return v0
.end method

.method public hasStableIds()Z
    .locals 1

    .line 54
    invoke-direct {p0}, Lcrc643f46942d9dd1fff9/ListViewAdapter;->n_hasStableIds()Z

    move-result v0

    return v0
.end method

.method public isEnabled(I)Z
    .locals 0

    .line 110
    invoke-direct {p0, p1}, Lcrc643f46942d9dd1fff9/ListViewAdapter;->n_isEnabled(I)Z

    move-result p1

    return p1
.end method

.method public monodroidAddReference(Ljava/lang/Object;)V
    .locals 1

    .line 118
    iget-object v0, p0, Lcrc643f46942d9dd1fff9/ListViewAdapter;->refList:Ljava/util/ArrayList;

    if-nez v0, :cond_0

    .line 119
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    iput-object v0, p0, Lcrc643f46942d9dd1fff9/ListViewAdapter;->refList:Ljava/util/ArrayList;

    .line 120
    :cond_0
    iget-object v0, p0, Lcrc643f46942d9dd1fff9/ListViewAdapter;->refList:Ljava/util/ArrayList;

    invoke-virtual {v0, p1}, Ljava/util/ArrayList;->add(Ljava/lang/Object;)Z

    .line 121
    return-void
.end method

.method public monodroidClearReferences()V
    .locals 1

    .line 125
    iget-object v0, p0, Lcrc643f46942d9dd1fff9/ListViewAdapter;->refList:Ljava/util/ArrayList;

    if-eqz v0, :cond_0

    .line 126
    invoke-virtual {v0}, Ljava/util/ArrayList;->clear()V

    .line 127
    :cond_0
    return-void
.end method
