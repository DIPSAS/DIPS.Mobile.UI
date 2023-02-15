.class public Lcrc643f46942d9dd1fff9/ShellSearchViewAdapter_CustomFilter;
.super Landroid/widget/Filter;
.source "ShellSearchViewAdapter_CustomFilter.java"

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
    const-string v0, "n_performFiltering:(Ljava/lang/CharSequence;)Landroid/widget/Filter$FilterResults;:GetPerformFiltering_Ljava_lang_CharSequence_Handler\nn_publishResults:(Ljava/lang/CharSequence;Landroid/widget/Filter$FilterResults;)V:GetPublishResults_Ljava_lang_CharSequence_Landroid_widget_Filter_FilterResults_Handler\n"

    sput-object v0, Lcrc643f46942d9dd1fff9/ShellSearchViewAdapter_CustomFilter;->__md_methods:Ljava/lang/String;

    .line 16
    const-class v1, Lcrc643f46942d9dd1fff9/ShellSearchViewAdapter_CustomFilter;

    const-string v2, "Xamarin.Forms.Platform.Android.ShellSearchViewAdapter+CustomFilter, Xamarin.Forms.Platform.Android"

    invoke-static {v2, v1, v0}, Lmono/android/Runtime;->register(Ljava/lang/String;Ljava/lang/Class;Ljava/lang/String;)V

    .line 17
    return-void
.end method

.method public constructor <init>()V
    .locals 3

    .line 22
    invoke-direct {p0}, Landroid/widget/Filter;-><init>()V

    .line 23
    invoke-virtual {p0}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    move-result-object v0

    const-class v1, Lcrc643f46942d9dd1fff9/ShellSearchViewAdapter_CustomFilter;

    if-ne v0, v1, :cond_0

    .line 24
    const/4 v0, 0x0

    new-array v0, v0, [Ljava/lang/Object;

    const-string v1, "Xamarin.Forms.Platform.Android.ShellSearchViewAdapter+CustomFilter, Xamarin.Forms.Platform.Android"

    const-string v2, ""

    invoke-static {v1, v2, p0, v0}, Lmono/android/TypeManager;->Activate(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Object;[Ljava/lang/Object;)V

    .line 26
    :cond_0
    return-void
.end method

.method public constructor <init>(Landroid/widget/BaseAdapter;)V
    .locals 2

    .line 30
    invoke-direct {p0}, Landroid/widget/Filter;-><init>()V

    .line 31
    invoke-virtual {p0}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    move-result-object v0

    const-class v1, Lcrc643f46942d9dd1fff9/ShellSearchViewAdapter_CustomFilter;

    if-ne v0, v1, :cond_0

    .line 32
    const/4 v0, 0x1

    new-array v0, v0, [Ljava/lang/Object;

    const/4 v1, 0x0

    aput-object p1, v0, v1

    const-string p1, "Xamarin.Forms.Platform.Android.ShellSearchViewAdapter+CustomFilter, Xamarin.Forms.Platform.Android"

    const-string v1, "Android.Widget.BaseAdapter, Mono.Android"

    invoke-static {p1, v1, p0, v0}, Lmono/android/TypeManager;->Activate(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Object;[Ljava/lang/Object;)V

    .line 34
    :cond_0
    return-void
.end method

.method private native n_performFiltering(Ljava/lang/CharSequence;)Landroid/widget/Filter$FilterResults;
.end method

.method private native n_publishResults(Ljava/lang/CharSequence;Landroid/widget/Filter$FilterResults;)V
.end method


# virtual methods
.method public monodroidAddReference(Ljava/lang/Object;)V
    .locals 1

    .line 55
    iget-object v0, p0, Lcrc643f46942d9dd1fff9/ShellSearchViewAdapter_CustomFilter;->refList:Ljava/util/ArrayList;

    if-nez v0, :cond_0

    .line 56
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    iput-object v0, p0, Lcrc643f46942d9dd1fff9/ShellSearchViewAdapter_CustomFilter;->refList:Ljava/util/ArrayList;

    .line 57
    :cond_0
    iget-object v0, p0, Lcrc643f46942d9dd1fff9/ShellSearchViewAdapter_CustomFilter;->refList:Ljava/util/ArrayList;

    invoke-virtual {v0, p1}, Ljava/util/ArrayList;->add(Ljava/lang/Object;)Z

    .line 58
    return-void
.end method

.method public monodroidClearReferences()V
    .locals 1

    .line 62
    iget-object v0, p0, Lcrc643f46942d9dd1fff9/ShellSearchViewAdapter_CustomFilter;->refList:Ljava/util/ArrayList;

    if-eqz v0, :cond_0

    .line 63
    invoke-virtual {v0}, Ljava/util/ArrayList;->clear()V

    .line 64
    :cond_0
    return-void
.end method

.method public performFiltering(Ljava/lang/CharSequence;)Landroid/widget/Filter$FilterResults;
    .locals 0

    .line 39
    invoke-direct {p0, p1}, Lcrc643f46942d9dd1fff9/ShellSearchViewAdapter_CustomFilter;->n_performFiltering(Ljava/lang/CharSequence;)Landroid/widget/Filter$FilterResults;

    move-result-object p1

    return-object p1
.end method

.method public publishResults(Ljava/lang/CharSequence;Landroid/widget/Filter$FilterResults;)V
    .locals 0

    .line 47
    invoke-direct {p0, p1, p2}, Lcrc643f46942d9dd1fff9/ShellSearchViewAdapter_CustomFilter;->n_publishResults(Ljava/lang/CharSequence;Landroid/widget/Filter$FilterResults;)V

    .line 48
    return-void
.end method
