.class public Lcrc643f46942d9dd1fff9/GroupedListViewAdapter;
.super Lcrc643f46942d9dd1fff9/ListViewAdapter;
.source "GroupedListViewAdapter.java"

# interfaces
.implements Lmono/android/IGCUserPeer;
.implements Landroid/widget/SectionIndexer;


# static fields
.field public static final __md_methods:Ljava/lang/String;


# instance fields
.field private refList:Ljava/util/ArrayList;


# direct methods
.method static constructor <clinit>()V
    .locals 3

    .line 13
    const-string v0, "n_getPositionForSection:(I)I:GetGetPositionForSection_IHandler:Android.Widget.ISectionIndexerInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\nn_getSectionForPosition:(I)I:GetGetSectionForPosition_IHandler:Android.Widget.ISectionIndexerInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\nn_getSections:()[Ljava/lang/Object;:GetGetSectionsHandler:Android.Widget.ISectionIndexerInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n"

    sput-object v0, Lcrc643f46942d9dd1fff9/GroupedListViewAdapter;->__md_methods:Ljava/lang/String;

    .line 18
    const-class v1, Lcrc643f46942d9dd1fff9/GroupedListViewAdapter;

    const-string v2, "Xamarin.Forms.Platform.Android.GroupedListViewAdapter, Xamarin.Forms.Platform.Android"

    invoke-static {v2, v1, v0}, Lmono/android/Runtime;->register(Ljava/lang/String;Ljava/lang/Class;Ljava/lang/String;)V

    .line 19
    return-void
.end method

.method public constructor <init>()V
    .locals 3

    .line 24
    invoke-direct {p0}, Lcrc643f46942d9dd1fff9/ListViewAdapter;-><init>()V

    .line 25
    invoke-virtual {p0}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    move-result-object v0

    const-class v1, Lcrc643f46942d9dd1fff9/GroupedListViewAdapter;

    if-ne v0, v1, :cond_0

    .line 26
    const/4 v0, 0x0

    new-array v0, v0, [Ljava/lang/Object;

    const-string v1, "Xamarin.Forms.Platform.Android.GroupedListViewAdapter, Xamarin.Forms.Platform.Android"

    const-string v2, ""

    invoke-static {v1, v2, p0, v0}, Lmono/android/TypeManager;->Activate(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Object;[Ljava/lang/Object;)V

    .line 28
    :cond_0
    return-void
.end method

.method public constructor <init>(Landroid/content/Context;)V
    .locals 2

    .line 32
    invoke-direct {p0}, Lcrc643f46942d9dd1fff9/ListViewAdapter;-><init>()V

    .line 33
    invoke-virtual {p0}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    move-result-object v0

    const-class v1, Lcrc643f46942d9dd1fff9/GroupedListViewAdapter;

    if-ne v0, v1, :cond_0

    .line 34
    const/4 v0, 0x1

    new-array v0, v0, [Ljava/lang/Object;

    const/4 v1, 0x0

    aput-object p1, v0, v1

    const-string p1, "Xamarin.Forms.Platform.Android.GroupedListViewAdapter, Xamarin.Forms.Platform.Android"

    const-string v1, "Android.Content.Context, Mono.Android"

    invoke-static {p1, v1, p0, v0}, Lmono/android/TypeManager;->Activate(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Object;[Ljava/lang/Object;)V

    .line 36
    :cond_0
    return-void
.end method

.method private native n_getPositionForSection(I)I
.end method

.method private native n_getSectionForPosition(I)I
.end method

.method private native n_getSections()[Ljava/lang/Object;
.end method


# virtual methods
.method public getPositionForSection(I)I
    .locals 0

    .line 41
    invoke-direct {p0, p1}, Lcrc643f46942d9dd1fff9/GroupedListViewAdapter;->n_getPositionForSection(I)I

    move-result p1

    return p1
.end method

.method public getSectionForPosition(I)I
    .locals 0

    .line 49
    invoke-direct {p0, p1}, Lcrc643f46942d9dd1fff9/GroupedListViewAdapter;->n_getSectionForPosition(I)I

    move-result p1

    return p1
.end method

.method public getSections()[Ljava/lang/Object;
    .locals 1

    .line 57
    invoke-direct {p0}, Lcrc643f46942d9dd1fff9/GroupedListViewAdapter;->n_getSections()[Ljava/lang/Object;

    move-result-object v0

    return-object v0
.end method

.method public monodroidAddReference(Ljava/lang/Object;)V
    .locals 1

    .line 65
    iget-object v0, p0, Lcrc643f46942d9dd1fff9/GroupedListViewAdapter;->refList:Ljava/util/ArrayList;

    if-nez v0, :cond_0

    .line 66
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    iput-object v0, p0, Lcrc643f46942d9dd1fff9/GroupedListViewAdapter;->refList:Ljava/util/ArrayList;

    .line 67
    :cond_0
    iget-object v0, p0, Lcrc643f46942d9dd1fff9/GroupedListViewAdapter;->refList:Ljava/util/ArrayList;

    invoke-virtual {v0, p1}, Ljava/util/ArrayList;->add(Ljava/lang/Object;)Z

    .line 68
    return-void
.end method

.method public monodroidClearReferences()V
    .locals 1

    .line 72
    iget-object v0, p0, Lcrc643f46942d9dd1fff9/GroupedListViewAdapter;->refList:Ljava/util/ArrayList;

    if-eqz v0, :cond_0

    .line 73
    invoke-virtual {v0}, Ljava/util/ArrayList;->clear()V

    .line 74
    :cond_0
    return-void
.end method
