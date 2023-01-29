.class public abstract Lcrc643f46942d9dd1fff9/CellAdapter;
.super Landroid/widget/BaseAdapter;
.source "CellAdapter.java"

# interfaces
.implements Lmono/android/IGCUserPeer;
.implements Landroid/widget/AdapterView$OnItemLongClickListener;
.implements Landroid/view/ActionMode$Callback;
.implements Landroid/widget/AdapterView$OnItemClickListener;
.implements Landroidx/appcompat/view/ActionMode$Callback;


# static fields
.field public static final __md_methods:Ljava/lang/String;


# instance fields
.field private refList:Ljava/util/ArrayList;


# direct methods
.method static constructor <clinit>()V
    .locals 3

    .line 16
    const-string v0, "n_onItemLongClick:(Landroid/widget/AdapterView;Landroid/view/View;IJ)Z:GetOnItemLongClick_Landroid_widget_AdapterView_Landroid_view_View_IJHandler:Android.Widget.AdapterView/IOnItemLongClickListenerInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\nn_onActionItemClicked:(Landroid/view/ActionMode;Landroid/view/MenuItem;)Z:GetOnActionItemClicked_Landroid_view_ActionMode_Landroid_view_MenuItem_Handler:Android.Views.ActionMode/ICallbackInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\nn_onCreateActionMode:(Landroid/view/ActionMode;Landroid/view/Menu;)Z:GetOnCreateActionMode_Landroid_view_ActionMode_Landroid_view_Menu_Handler:Android.Views.ActionMode/ICallbackInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\nn_onDestroyActionMode:(Landroid/view/ActionMode;)V:GetOnDestroyActionMode_Landroid_view_ActionMode_Handler:Android.Views.ActionMode/ICallbackInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\nn_onPrepareActionMode:(Landroid/view/ActionMode;Landroid/view/Menu;)Z:GetOnPrepareActionMode_Landroid_view_ActionMode_Landroid_view_Menu_Handler:Android.Views.ActionMode/ICallbackInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\nn_onItemClick:(Landroid/widget/AdapterView;Landroid/view/View;IJ)V:GetOnItemClick_Landroid_widget_AdapterView_Landroid_view_View_IJHandler:Android.Widget.AdapterView/IOnItemClickListenerInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\nn_onActionItemClicked:(Landroidx/appcompat/view/ActionMode;Landroid/view/MenuItem;)Z:GetOnActionItemClicked_Landroidx_appcompat_view_ActionMode_Landroid_view_MenuItem_Handler:AndroidX.AppCompat.View.ActionMode/ICallbackInvoker, Xamarin.AndroidX.AppCompat\nn_onCreateActionMode:(Landroidx/appcompat/view/ActionMode;Landroid/view/Menu;)Z:GetOnCreateActionMode_Landroidx_appcompat_view_ActionMode_Landroid_view_Menu_Handler:AndroidX.AppCompat.View.ActionMode/ICallbackInvoker, Xamarin.AndroidX.AppCompat\nn_onDestroyActionMode:(Landroidx/appcompat/view/ActionMode;)V:GetOnDestroyActionMode_Landroidx_appcompat_view_ActionMode_Handler:AndroidX.AppCompat.View.ActionMode/ICallbackInvoker, Xamarin.AndroidX.AppCompat\nn_onPrepareActionMode:(Landroidx/appcompat/view/ActionMode;Landroid/view/Menu;)Z:GetOnPrepareActionMode_Landroidx_appcompat_view_ActionMode_Landroid_view_Menu_Handler:AndroidX.AppCompat.View.ActionMode/ICallbackInvoker, Xamarin.AndroidX.AppCompat\n"

    sput-object v0, Lcrc643f46942d9dd1fff9/CellAdapter;->__md_methods:Ljava/lang/String;

    .line 28
    const-class v1, Lcrc643f46942d9dd1fff9/CellAdapter;

    const-string v2, "Xamarin.Forms.Platform.Android.CellAdapter, Xamarin.Forms.Platform.Android"

    invoke-static {v2, v1, v0}, Lmono/android/Runtime;->register(Ljava/lang/String;Ljava/lang/Class;Ljava/lang/String;)V

    .line 29
    return-void
.end method

.method public constructor <init>()V
    .locals 3

    .line 34
    invoke-direct {p0}, Landroid/widget/BaseAdapter;-><init>()V

    .line 35
    invoke-virtual {p0}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    move-result-object v0

    const-class v1, Lcrc643f46942d9dd1fff9/CellAdapter;

    if-ne v0, v1, :cond_0

    .line 36
    const/4 v0, 0x0

    new-array v0, v0, [Ljava/lang/Object;

    const-string v1, "Xamarin.Forms.Platform.Android.CellAdapter, Xamarin.Forms.Platform.Android"

    const-string v2, ""

    invoke-static {v1, v2, p0, v0}, Lmono/android/TypeManager;->Activate(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Object;[Ljava/lang/Object;)V

    .line 38
    :cond_0
    return-void
.end method

.method public constructor <init>(Landroid/content/Context;)V
    .locals 2

    .line 42
    invoke-direct {p0}, Landroid/widget/BaseAdapter;-><init>()V

    .line 43
    invoke-virtual {p0}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    move-result-object v0

    const-class v1, Lcrc643f46942d9dd1fff9/CellAdapter;

    if-ne v0, v1, :cond_0

    .line 44
    const/4 v0, 0x1

    new-array v0, v0, [Ljava/lang/Object;

    const/4 v1, 0x0

    aput-object p1, v0, v1

    const-string p1, "Xamarin.Forms.Platform.Android.CellAdapter, Xamarin.Forms.Platform.Android"

    const-string v1, "Android.Content.Context, Mono.Android"

    invoke-static {p1, v1, p0, v0}, Lmono/android/TypeManager;->Activate(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Object;[Ljava/lang/Object;)V

    .line 46
    :cond_0
    return-void
.end method

.method private native n_onActionItemClicked(Landroid/view/ActionMode;Landroid/view/MenuItem;)Z
.end method

.method private native n_onActionItemClicked(Landroidx/appcompat/view/ActionMode;Landroid/view/MenuItem;)Z
.end method

.method private native n_onCreateActionMode(Landroid/view/ActionMode;Landroid/view/Menu;)Z
.end method

.method private native n_onCreateActionMode(Landroidx/appcompat/view/ActionMode;Landroid/view/Menu;)Z
.end method

.method private native n_onDestroyActionMode(Landroid/view/ActionMode;)V
.end method

.method private native n_onDestroyActionMode(Landroidx/appcompat/view/ActionMode;)V
.end method

.method private native n_onItemClick(Landroid/widget/AdapterView;Landroid/view/View;IJ)V
.end method

.method private native n_onItemLongClick(Landroid/widget/AdapterView;Landroid/view/View;IJ)Z
.end method

.method private native n_onPrepareActionMode(Landroid/view/ActionMode;Landroid/view/Menu;)Z
.end method

.method private native n_onPrepareActionMode(Landroidx/appcompat/view/ActionMode;Landroid/view/Menu;)Z
.end method


# virtual methods
.method public monodroidAddReference(Ljava/lang/Object;)V
    .locals 1

    .line 131
    iget-object v0, p0, Lcrc643f46942d9dd1fff9/CellAdapter;->refList:Ljava/util/ArrayList;

    if-nez v0, :cond_0

    .line 132
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    iput-object v0, p0, Lcrc643f46942d9dd1fff9/CellAdapter;->refList:Ljava/util/ArrayList;

    .line 133
    :cond_0
    iget-object v0, p0, Lcrc643f46942d9dd1fff9/CellAdapter;->refList:Ljava/util/ArrayList;

    invoke-virtual {v0, p1}, Ljava/util/ArrayList;->add(Ljava/lang/Object;)Z

    .line 134
    return-void
.end method

.method public monodroidClearReferences()V
    .locals 1

    .line 138
    iget-object v0, p0, Lcrc643f46942d9dd1fff9/CellAdapter;->refList:Ljava/util/ArrayList;

    if-eqz v0, :cond_0

    .line 139
    invoke-virtual {v0}, Ljava/util/ArrayList;->clear()V

    .line 140
    :cond_0
    return-void
.end method

.method public onActionItemClicked(Landroid/view/ActionMode;Landroid/view/MenuItem;)Z
    .locals 0

    .line 59
    invoke-direct {p0, p1, p2}, Lcrc643f46942d9dd1fff9/CellAdapter;->n_onActionItemClicked(Landroid/view/ActionMode;Landroid/view/MenuItem;)Z

    move-result p1

    return p1
.end method

.method public onActionItemClicked(Landroidx/appcompat/view/ActionMode;Landroid/view/MenuItem;)Z
    .locals 0

    .line 99
    invoke-direct {p0, p1, p2}, Lcrc643f46942d9dd1fff9/CellAdapter;->n_onActionItemClicked(Landroidx/appcompat/view/ActionMode;Landroid/view/MenuItem;)Z

    move-result p1

    return p1
.end method

.method public onCreateActionMode(Landroid/view/ActionMode;Landroid/view/Menu;)Z
    .locals 0

    .line 67
    invoke-direct {p0, p1, p2}, Lcrc643f46942d9dd1fff9/CellAdapter;->n_onCreateActionMode(Landroid/view/ActionMode;Landroid/view/Menu;)Z

    move-result p1

    return p1
.end method

.method public onCreateActionMode(Landroidx/appcompat/view/ActionMode;Landroid/view/Menu;)Z
    .locals 0

    .line 107
    invoke-direct {p0, p1, p2}, Lcrc643f46942d9dd1fff9/CellAdapter;->n_onCreateActionMode(Landroidx/appcompat/view/ActionMode;Landroid/view/Menu;)Z

    move-result p1

    return p1
.end method

.method public onDestroyActionMode(Landroid/view/ActionMode;)V
    .locals 0

    .line 75
    invoke-direct {p0, p1}, Lcrc643f46942d9dd1fff9/CellAdapter;->n_onDestroyActionMode(Landroid/view/ActionMode;)V

    .line 76
    return-void
.end method

.method public onDestroyActionMode(Landroidx/appcompat/view/ActionMode;)V
    .locals 0

    .line 115
    invoke-direct {p0, p1}, Lcrc643f46942d9dd1fff9/CellAdapter;->n_onDestroyActionMode(Landroidx/appcompat/view/ActionMode;)V

    .line 116
    return-void
.end method

.method public onItemClick(Landroid/widget/AdapterView;Landroid/view/View;IJ)V
    .locals 0

    .line 91
    invoke-direct/range {p0 .. p5}, Lcrc643f46942d9dd1fff9/CellAdapter;->n_onItemClick(Landroid/widget/AdapterView;Landroid/view/View;IJ)V

    .line 92
    return-void
.end method

.method public onItemLongClick(Landroid/widget/AdapterView;Landroid/view/View;IJ)Z
    .locals 0

    .line 51
    invoke-direct/range {p0 .. p5}, Lcrc643f46942d9dd1fff9/CellAdapter;->n_onItemLongClick(Landroid/widget/AdapterView;Landroid/view/View;IJ)Z

    move-result p1

    return p1
.end method

.method public onPrepareActionMode(Landroid/view/ActionMode;Landroid/view/Menu;)Z
    .locals 0

    .line 83
    invoke-direct {p0, p1, p2}, Lcrc643f46942d9dd1fff9/CellAdapter;->n_onPrepareActionMode(Landroid/view/ActionMode;Landroid/view/Menu;)Z

    move-result p1

    return p1
.end method

.method public onPrepareActionMode(Landroidx/appcompat/view/ActionMode;Landroid/view/Menu;)Z
    .locals 0

    .line 123
    invoke-direct {p0, p1, p2}, Lcrc643f46942d9dd1fff9/CellAdapter;->n_onPrepareActionMode(Landroidx/appcompat/view/ActionMode;Landroid/view/Menu;)Z

    move-result p1

    return p1
.end method
