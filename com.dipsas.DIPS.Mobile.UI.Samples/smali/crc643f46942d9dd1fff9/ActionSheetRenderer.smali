.class public Lcrc643f46942d9dd1fff9/ActionSheetRenderer;
.super Landroid/app/Dialog;
.source "ActionSheetRenderer.java"

# interfaces
.implements Lmono/android/IGCUserPeer;
.implements Landroid/view/View$OnClickListener;


# static fields
.field public static final __md_methods:Ljava/lang/String;


# instance fields
.field private refList:Ljava/util/ArrayList;


# direct methods
.method static constructor <clinit>()V
    .locals 3

    .line 13
    const-string v0, "n_cancel:()V:GetCancelHandler\nn_onAttachedToWindow:()V:GetOnAttachedToWindowHandler\nn_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\nn_onClick:(Landroid/view/View;)V:GetOnClick_Landroid_view_View_Handler:Android.Views.View/IOnClickListenerInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n"

    sput-object v0, Lcrc643f46942d9dd1fff9/ActionSheetRenderer;->__md_methods:Ljava/lang/String;

    .line 19
    const-class v1, Lcrc643f46942d9dd1fff9/ActionSheetRenderer;

    const-string v2, "Xamarin.Forms.Platform.Android.ActionSheetRenderer, Xamarin.Forms.Platform.Android"

    invoke-static {v2, v1, v0}, Lmono/android/Runtime;->register(Ljava/lang/String;Ljava/lang/Class;Ljava/lang/String;)V

    .line 20
    return-void
.end method

.method public constructor <init>(Landroid/content/Context;)V
    .locals 2

    .line 25
    invoke-direct {p0, p1}, Landroid/app/Dialog;-><init>(Landroid/content/Context;)V

    .line 26
    invoke-virtual {p0}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    move-result-object v0

    const-class v1, Lcrc643f46942d9dd1fff9/ActionSheetRenderer;

    if-ne v0, v1, :cond_0

    .line 27
    const/4 v0, 0x1

    new-array v0, v0, [Ljava/lang/Object;

    const/4 v1, 0x0

    aput-object p1, v0, v1

    const-string p1, "Xamarin.Forms.Platform.Android.ActionSheetRenderer, Xamarin.Forms.Platform.Android"

    const-string v1, "Android.Content.Context, Mono.Android"

    invoke-static {p1, v1, p0, v0}, Lmono/android/TypeManager;->Activate(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Object;[Ljava/lang/Object;)V

    .line 29
    :cond_0
    return-void
.end method

.method public constructor <init>(Landroid/content/Context;I)V
    .locals 2

    .line 43
    invoke-direct {p0, p1, p2}, Landroid/app/Dialog;-><init>(Landroid/content/Context;I)V

    .line 44
    invoke-virtual {p0}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    move-result-object v0

    const-class v1, Lcrc643f46942d9dd1fff9/ActionSheetRenderer;

    if-ne v0, v1, :cond_0

    .line 45
    const/4 v0, 0x2

    new-array v0, v0, [Ljava/lang/Object;

    const/4 v1, 0x0

    aput-object p1, v0, v1

    const/4 p1, 0x1

    invoke-static {p2}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object p2

    aput-object p2, v0, p1

    const-string p1, "Xamarin.Forms.Platform.Android.ActionSheetRenderer, Xamarin.Forms.Platform.Android"

    const-string p2, "Android.Content.Context, Mono.Android:System.Int32, mscorlib"

    invoke-static {p1, p2, p0, v0}, Lmono/android/TypeManager;->Activate(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Object;[Ljava/lang/Object;)V

    .line 47
    :cond_0
    return-void
.end method

.method public constructor <init>(Landroid/content/Context;ZLandroid/content/DialogInterface$OnCancelListener;)V
    .locals 2

    .line 34
    invoke-direct {p0, p1, p2, p3}, Landroid/app/Dialog;-><init>(Landroid/content/Context;ZLandroid/content/DialogInterface$OnCancelListener;)V

    .line 35
    invoke-virtual {p0}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    move-result-object v0

    const-class v1, Lcrc643f46942d9dd1fff9/ActionSheetRenderer;

    if-ne v0, v1, :cond_0

    .line 36
    const/4 v0, 0x3

    new-array v0, v0, [Ljava/lang/Object;

    const/4 v1, 0x0

    aput-object p1, v0, v1

    const/4 p1, 0x1

    invoke-static {p2}, Ljava/lang/Boolean;->valueOf(Z)Ljava/lang/Boolean;

    move-result-object p2

    aput-object p2, v0, p1

    const/4 p1, 0x2

    aput-object p3, v0, p1

    const-string p1, "Xamarin.Forms.Platform.Android.ActionSheetRenderer, Xamarin.Forms.Platform.Android"

    const-string p2, "Android.Content.Context, Mono.Android:System.Boolean, mscorlib:Android.Content.IDialogInterfaceOnCancelListener, Mono.Android"

    invoke-static {p1, p2, p0, v0}, Lmono/android/TypeManager;->Activate(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Object;[Ljava/lang/Object;)V

    .line 38
    :cond_0
    return-void
.end method

.method private native n_cancel()V
.end method

.method private native n_onAttachedToWindow()V
.end method

.method private native n_onClick(Landroid/view/View;)V
.end method

.method private native n_onCreate(Landroid/os/Bundle;)V
.end method


# virtual methods
.method public cancel()V
    .locals 0

    .line 52
    invoke-direct {p0}, Lcrc643f46942d9dd1fff9/ActionSheetRenderer;->n_cancel()V

    .line 53
    return-void
.end method

.method public monodroidAddReference(Ljava/lang/Object;)V
    .locals 1

    .line 84
    iget-object v0, p0, Lcrc643f46942d9dd1fff9/ActionSheetRenderer;->refList:Ljava/util/ArrayList;

    if-nez v0, :cond_0

    .line 85
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    iput-object v0, p0, Lcrc643f46942d9dd1fff9/ActionSheetRenderer;->refList:Ljava/util/ArrayList;

    .line 86
    :cond_0
    iget-object v0, p0, Lcrc643f46942d9dd1fff9/ActionSheetRenderer;->refList:Ljava/util/ArrayList;

    invoke-virtual {v0, p1}, Ljava/util/ArrayList;->add(Ljava/lang/Object;)Z

    .line 87
    return-void
.end method

.method public monodroidClearReferences()V
    .locals 1

    .line 91
    iget-object v0, p0, Lcrc643f46942d9dd1fff9/ActionSheetRenderer;->refList:Ljava/util/ArrayList;

    if-eqz v0, :cond_0

    .line 92
    invoke-virtual {v0}, Ljava/util/ArrayList;->clear()V

    .line 93
    :cond_0
    return-void
.end method

.method public onAttachedToWindow()V
    .locals 0

    .line 60
    invoke-direct {p0}, Lcrc643f46942d9dd1fff9/ActionSheetRenderer;->n_onAttachedToWindow()V

    .line 61
    return-void
.end method

.method public onClick(Landroid/view/View;)V
    .locals 0

    .line 76
    invoke-direct {p0, p1}, Lcrc643f46942d9dd1fff9/ActionSheetRenderer;->n_onClick(Landroid/view/View;)V

    .line 77
    return-void
.end method

.method public onCreate(Landroid/os/Bundle;)V
    .locals 0

    .line 68
    invoke-direct {p0, p1}, Lcrc643f46942d9dd1fff9/ActionSheetRenderer;->n_onCreate(Landroid/os/Bundle;)V

    .line 69
    return-void
.end method
