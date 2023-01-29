.class public Lcrc641fb97271e3182cb7/ContextMenuButtonRenderer;
.super Lcrc643f46942d9dd1fff9/ButtonRenderer;
.source "ContextMenuButtonRenderer.java"

# interfaces
.implements Lmono/android/IGCUserPeer;
.implements Landroidx/appcompat/widget/PopupMenu$OnMenuItemClickListener;
.implements Landroid/app/Application$ActivityLifecycleCallbacks;


# static fields
.field public static final __md_methods:Ljava/lang/String;


# instance fields
.field private refList:Ljava/util/ArrayList;


# direct methods
.method static constructor <clinit>()V
    .locals 3

    .line 14
    const-string v0, "n_onMenuItemClick:(Landroid/view/MenuItem;)Z:GetOnMenuItemClick_Landroid_view_MenuItem_Handler:AndroidX.AppCompat.Widget.PopupMenu/IOnMenuItemClickListenerInvoker, Xamarin.AndroidX.AppCompat\nn_onActivityCreated:(Landroid/app/Activity;Landroid/os/Bundle;)V:GetOnActivityCreated_Landroid_app_Activity_Landroid_os_Bundle_Handler:Android.App.Application/IActivityLifecycleCallbacksInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\nn_onActivityDestroyed:(Landroid/app/Activity;)V:GetOnActivityDestroyed_Landroid_app_Activity_Handler:Android.App.Application/IActivityLifecycleCallbacksInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\nn_onActivityPaused:(Landroid/app/Activity;)V:GetOnActivityPaused_Landroid_app_Activity_Handler:Android.App.Application/IActivityLifecycleCallbacksInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\nn_onActivityResumed:(Landroid/app/Activity;)V:GetOnActivityResumed_Landroid_app_Activity_Handler:Android.App.Application/IActivityLifecycleCallbacksInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\nn_onActivitySaveInstanceState:(Landroid/app/Activity;Landroid/os/Bundle;)V:GetOnActivitySaveInstanceState_Landroid_app_Activity_Landroid_os_Bundle_Handler:Android.App.Application/IActivityLifecycleCallbacksInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\nn_onActivityStarted:(Landroid/app/Activity;)V:GetOnActivityStarted_Landroid_app_Activity_Handler:Android.App.Application/IActivityLifecycleCallbacksInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\nn_onActivityStopped:(Landroid/app/Activity;)V:GetOnActivityStopped_Landroid_app_Activity_Handler:Android.App.Application/IActivityLifecycleCallbacksInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\nn_onActivityPostCreated:(Landroid/app/Activity;Landroid/os/Bundle;)V:GetOnActivityPostCreated_Landroid_app_Activity_Landroid_os_Bundle_Handler:Android.App.Application/IActivityLifecycleCallbacks, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\nn_onActivityPostDestroyed:(Landroid/app/Activity;)V:GetOnActivityPostDestroyed_Landroid_app_Activity_Handler:Android.App.Application/IActivityLifecycleCallbacks, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\nn_onActivityPostPaused:(Landroid/app/Activity;)V:GetOnActivityPostPaused_Landroid_app_Activity_Handler:Android.App.Application/IActivityLifecycleCallbacks, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\nn_onActivityPostResumed:(Landroid/app/Activity;)V:GetOnActivityPostResumed_Landroid_app_Activity_Handler:Android.App.Application/IActivityLifecycleCallbacks, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\nn_onActivityPostSaveInstanceState:(Landroid/app/Activity;Landroid/os/Bundle;)V:GetOnActivityPostSaveInstanceState_Landroid_app_Activity_Landroid_os_Bundle_Handler:Android.App.Application/IActivityLifecycleCallbacks, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\nn_onActivityPostStarted:(Landroid/app/Activity;)V:GetOnActivityPostStarted_Landroid_app_Activity_Handler:Android.App.Application/IActivityLifecycleCallbacks, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\nn_onActivityPostStopped:(Landroid/app/Activity;)V:GetOnActivityPostStopped_Landroid_app_Activity_Handler:Android.App.Application/IActivityLifecycleCallbacks, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\nn_onActivityPreCreated:(Landroid/app/Activity;Landroid/os/Bundle;)V:GetOnActivityPreCreated_Landroid_app_Activity_Landroid_os_Bundle_Handler:Android.App.Application/IActivityLifecycleCallbacks, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\nn_onActivityPreDestroyed:(Landroid/app/Activity;)V:GetOnActivityPreDestroyed_Landroid_app_Activity_Handler:Android.App.Application/IActivityLifecycleCallbacks, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\nn_onActivityPrePaused:(Landroid/app/Activity;)V:GetOnActivityPrePaused_Landroid_app_Activity_Handler:Android.App.Application/IActivityLifecycleCallbacks, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\nn_onActivityPreResumed:(Landroid/app/Activity;)V:GetOnActivityPreResumed_Landroid_app_Activity_Handler:Android.App.Application/IActivityLifecycleCallbacks, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\nn_onActivityPreSaveInstanceState:(Landroid/app/Activity;Landroid/os/Bundle;)V:GetOnActivityPreSaveInstanceState_Landroid_app_Activity_Landroid_os_Bundle_Handler:Android.App.Application/IActivityLifecycleCallbacks, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\nn_onActivityPreStarted:(Landroid/app/Activity;)V:GetOnActivityPreStarted_Landroid_app_Activity_Handler:Android.App.Application/IActivityLifecycleCallbacks, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\nn_onActivityPreStopped:(Landroid/app/Activity;)V:GetOnActivityPreStopped_Landroid_app_Activity_Handler:Android.App.Application/IActivityLifecycleCallbacks, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n"

    sput-object v0, Lcrc641fb97271e3182cb7/ContextMenuButtonRenderer;->__md_methods:Ljava/lang/String;

    .line 38
    const-class v1, Lcrc641fb97271e3182cb7/ContextMenuButtonRenderer;

    const-string v2, "DIPS.Mobile.UI.Droid.Components.ContextMenu.ContextMenuButtonRenderer, DIPS.Mobile.UI.Droid"

    invoke-static {v2, v1, v0}, Lmono/android/Runtime;->register(Ljava/lang/String;Ljava/lang/Class;Ljava/lang/String;)V

    .line 39
    return-void
.end method

.method public constructor <init>(Landroid/content/Context;)V
    .locals 2

    .line 44
    invoke-direct {p0, p1}, Lcrc643f46942d9dd1fff9/ButtonRenderer;-><init>(Landroid/content/Context;)V

    .line 45
    invoke-virtual {p0}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    move-result-object v0

    const-class v1, Lcrc641fb97271e3182cb7/ContextMenuButtonRenderer;

    if-ne v0, v1, :cond_0

    .line 46
    const/4 v0, 0x1

    new-array v0, v0, [Ljava/lang/Object;

    const/4 v1, 0x0

    aput-object p1, v0, v1

    const-string p1, "DIPS.Mobile.UI.Droid.Components.ContextMenu.ContextMenuButtonRenderer, DIPS.Mobile.UI.Droid"

    const-string v1, "Android.Content.Context, Mono.Android"

    invoke-static {p1, v1, p0, v0}, Lmono/android/TypeManager;->Activate(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Object;[Ljava/lang/Object;)V

    .line 48
    :cond_0
    return-void
.end method

.method public constructor <init>(Landroid/content/Context;Landroid/util/AttributeSet;)V
    .locals 2

    .line 53
    invoke-direct {p0, p1, p2}, Lcrc643f46942d9dd1fff9/ButtonRenderer;-><init>(Landroid/content/Context;Landroid/util/AttributeSet;)V

    .line 54
    invoke-virtual {p0}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    move-result-object v0

    const-class v1, Lcrc641fb97271e3182cb7/ContextMenuButtonRenderer;

    if-ne v0, v1, :cond_0

    .line 55
    const/4 v0, 0x2

    new-array v0, v0, [Ljava/lang/Object;

    const/4 v1, 0x0

    aput-object p1, v0, v1

    const/4 p1, 0x1

    aput-object p2, v0, p1

    const-string p1, "DIPS.Mobile.UI.Droid.Components.ContextMenu.ContextMenuButtonRenderer, DIPS.Mobile.UI.Droid"

    const-string p2, "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android"

    invoke-static {p1, p2, p0, v0}, Lmono/android/TypeManager;->Activate(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Object;[Ljava/lang/Object;)V

    .line 57
    :cond_0
    return-void
.end method

.method public constructor <init>(Landroid/content/Context;Landroid/util/AttributeSet;I)V
    .locals 2

    .line 62
    invoke-direct {p0, p1, p2, p3}, Lcrc643f46942d9dd1fff9/ButtonRenderer;-><init>(Landroid/content/Context;Landroid/util/AttributeSet;I)V

    .line 63
    invoke-virtual {p0}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    move-result-object v0

    const-class v1, Lcrc641fb97271e3182cb7/ContextMenuButtonRenderer;

    if-ne v0, v1, :cond_0

    .line 64
    const/4 v0, 0x3

    new-array v0, v0, [Ljava/lang/Object;

    const/4 v1, 0x0

    aput-object p1, v0, v1

    const/4 p1, 0x1

    aput-object p2, v0, p1

    const/4 p1, 0x2

    invoke-static {p3}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object p2

    aput-object p2, v0, p1

    const-string p1, "DIPS.Mobile.UI.Droid.Components.ContextMenu.ContextMenuButtonRenderer, DIPS.Mobile.UI.Droid"

    const-string p2, "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android:System.Int32, mscorlib"

    invoke-static {p1, p2, p0, v0}, Lmono/android/TypeManager;->Activate(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Object;[Ljava/lang/Object;)V

    .line 66
    :cond_0
    return-void
.end method

.method private native n_onActivityCreated(Landroid/app/Activity;Landroid/os/Bundle;)V
.end method

.method private native n_onActivityDestroyed(Landroid/app/Activity;)V
.end method

.method private native n_onActivityPaused(Landroid/app/Activity;)V
.end method

.method private native n_onActivityPostCreated(Landroid/app/Activity;Landroid/os/Bundle;)V
.end method

.method private native n_onActivityPostDestroyed(Landroid/app/Activity;)V
.end method

.method private native n_onActivityPostPaused(Landroid/app/Activity;)V
.end method

.method private native n_onActivityPostResumed(Landroid/app/Activity;)V
.end method

.method private native n_onActivityPostSaveInstanceState(Landroid/app/Activity;Landroid/os/Bundle;)V
.end method

.method private native n_onActivityPostStarted(Landroid/app/Activity;)V
.end method

.method private native n_onActivityPostStopped(Landroid/app/Activity;)V
.end method

.method private native n_onActivityPreCreated(Landroid/app/Activity;Landroid/os/Bundle;)V
.end method

.method private native n_onActivityPreDestroyed(Landroid/app/Activity;)V
.end method

.method private native n_onActivityPrePaused(Landroid/app/Activity;)V
.end method

.method private native n_onActivityPreResumed(Landroid/app/Activity;)V
.end method

.method private native n_onActivityPreSaveInstanceState(Landroid/app/Activity;Landroid/os/Bundle;)V
.end method

.method private native n_onActivityPreStarted(Landroid/app/Activity;)V
.end method

.method private native n_onActivityPreStopped(Landroid/app/Activity;)V
.end method

.method private native n_onActivityResumed(Landroid/app/Activity;)V
.end method

.method private native n_onActivitySaveInstanceState(Landroid/app/Activity;Landroid/os/Bundle;)V
.end method

.method private native n_onActivityStarted(Landroid/app/Activity;)V
.end method

.method private native n_onActivityStopped(Landroid/app/Activity;)V
.end method

.method private native n_onMenuItemClick(Landroid/view/MenuItem;)Z
.end method


# virtual methods
.method public monodroidAddReference(Ljava/lang/Object;)V
    .locals 1

    .line 247
    iget-object v0, p0, Lcrc641fb97271e3182cb7/ContextMenuButtonRenderer;->refList:Ljava/util/ArrayList;

    if-nez v0, :cond_0

    .line 248
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    iput-object v0, p0, Lcrc641fb97271e3182cb7/ContextMenuButtonRenderer;->refList:Ljava/util/ArrayList;

    .line 249
    :cond_0
    iget-object v0, p0, Lcrc641fb97271e3182cb7/ContextMenuButtonRenderer;->refList:Ljava/util/ArrayList;

    invoke-virtual {v0, p1}, Ljava/util/ArrayList;->add(Ljava/lang/Object;)Z

    .line 250
    return-void
.end method

.method public monodroidClearReferences()V
    .locals 1

    .line 254
    iget-object v0, p0, Lcrc641fb97271e3182cb7/ContextMenuButtonRenderer;->refList:Ljava/util/ArrayList;

    if-eqz v0, :cond_0

    .line 255
    invoke-virtual {v0}, Ljava/util/ArrayList;->clear()V

    .line 256
    :cond_0
    return-void
.end method

.method public onActivityCreated(Landroid/app/Activity;Landroid/os/Bundle;)V
    .locals 0

    .line 79
    invoke-direct {p0, p1, p2}, Lcrc641fb97271e3182cb7/ContextMenuButtonRenderer;->n_onActivityCreated(Landroid/app/Activity;Landroid/os/Bundle;)V

    .line 80
    return-void
.end method

.method public onActivityDestroyed(Landroid/app/Activity;)V
    .locals 0

    .line 87
    invoke-direct {p0, p1}, Lcrc641fb97271e3182cb7/ContextMenuButtonRenderer;->n_onActivityDestroyed(Landroid/app/Activity;)V

    .line 88
    return-void
.end method

.method public onActivityPaused(Landroid/app/Activity;)V
    .locals 0

    .line 95
    invoke-direct {p0, p1}, Lcrc641fb97271e3182cb7/ContextMenuButtonRenderer;->n_onActivityPaused(Landroid/app/Activity;)V

    .line 96
    return-void
.end method

.method public onActivityPostCreated(Landroid/app/Activity;Landroid/os/Bundle;)V
    .locals 0

    .line 135
    invoke-direct {p0, p1, p2}, Lcrc641fb97271e3182cb7/ContextMenuButtonRenderer;->n_onActivityPostCreated(Landroid/app/Activity;Landroid/os/Bundle;)V

    .line 136
    return-void
.end method

.method public onActivityPostDestroyed(Landroid/app/Activity;)V
    .locals 0

    .line 143
    invoke-direct {p0, p1}, Lcrc641fb97271e3182cb7/ContextMenuButtonRenderer;->n_onActivityPostDestroyed(Landroid/app/Activity;)V

    .line 144
    return-void
.end method

.method public onActivityPostPaused(Landroid/app/Activity;)V
    .locals 0

    .line 151
    invoke-direct {p0, p1}, Lcrc641fb97271e3182cb7/ContextMenuButtonRenderer;->n_onActivityPostPaused(Landroid/app/Activity;)V

    .line 152
    return-void
.end method

.method public onActivityPostResumed(Landroid/app/Activity;)V
    .locals 0

    .line 159
    invoke-direct {p0, p1}, Lcrc641fb97271e3182cb7/ContextMenuButtonRenderer;->n_onActivityPostResumed(Landroid/app/Activity;)V

    .line 160
    return-void
.end method

.method public onActivityPostSaveInstanceState(Landroid/app/Activity;Landroid/os/Bundle;)V
    .locals 0

    .line 167
    invoke-direct {p0, p1, p2}, Lcrc641fb97271e3182cb7/ContextMenuButtonRenderer;->n_onActivityPostSaveInstanceState(Landroid/app/Activity;Landroid/os/Bundle;)V

    .line 168
    return-void
.end method

.method public onActivityPostStarted(Landroid/app/Activity;)V
    .locals 0

    .line 175
    invoke-direct {p0, p1}, Lcrc641fb97271e3182cb7/ContextMenuButtonRenderer;->n_onActivityPostStarted(Landroid/app/Activity;)V

    .line 176
    return-void
.end method

.method public onActivityPostStopped(Landroid/app/Activity;)V
    .locals 0

    .line 183
    invoke-direct {p0, p1}, Lcrc641fb97271e3182cb7/ContextMenuButtonRenderer;->n_onActivityPostStopped(Landroid/app/Activity;)V

    .line 184
    return-void
.end method

.method public onActivityPreCreated(Landroid/app/Activity;Landroid/os/Bundle;)V
    .locals 0

    .line 191
    invoke-direct {p0, p1, p2}, Lcrc641fb97271e3182cb7/ContextMenuButtonRenderer;->n_onActivityPreCreated(Landroid/app/Activity;Landroid/os/Bundle;)V

    .line 192
    return-void
.end method

.method public onActivityPreDestroyed(Landroid/app/Activity;)V
    .locals 0

    .line 199
    invoke-direct {p0, p1}, Lcrc641fb97271e3182cb7/ContextMenuButtonRenderer;->n_onActivityPreDestroyed(Landroid/app/Activity;)V

    .line 200
    return-void
.end method

.method public onActivityPrePaused(Landroid/app/Activity;)V
    .locals 0

    .line 207
    invoke-direct {p0, p1}, Lcrc641fb97271e3182cb7/ContextMenuButtonRenderer;->n_onActivityPrePaused(Landroid/app/Activity;)V

    .line 208
    return-void
.end method

.method public onActivityPreResumed(Landroid/app/Activity;)V
    .locals 0

    .line 215
    invoke-direct {p0, p1}, Lcrc641fb97271e3182cb7/ContextMenuButtonRenderer;->n_onActivityPreResumed(Landroid/app/Activity;)V

    .line 216
    return-void
.end method

.method public onActivityPreSaveInstanceState(Landroid/app/Activity;Landroid/os/Bundle;)V
    .locals 0

    .line 223
    invoke-direct {p0, p1, p2}, Lcrc641fb97271e3182cb7/ContextMenuButtonRenderer;->n_onActivityPreSaveInstanceState(Landroid/app/Activity;Landroid/os/Bundle;)V

    .line 224
    return-void
.end method

.method public onActivityPreStarted(Landroid/app/Activity;)V
    .locals 0

    .line 231
    invoke-direct {p0, p1}, Lcrc641fb97271e3182cb7/ContextMenuButtonRenderer;->n_onActivityPreStarted(Landroid/app/Activity;)V

    .line 232
    return-void
.end method

.method public onActivityPreStopped(Landroid/app/Activity;)V
    .locals 0

    .line 239
    invoke-direct {p0, p1}, Lcrc641fb97271e3182cb7/ContextMenuButtonRenderer;->n_onActivityPreStopped(Landroid/app/Activity;)V

    .line 240
    return-void
.end method

.method public onActivityResumed(Landroid/app/Activity;)V
    .locals 0

    .line 103
    invoke-direct {p0, p1}, Lcrc641fb97271e3182cb7/ContextMenuButtonRenderer;->n_onActivityResumed(Landroid/app/Activity;)V

    .line 104
    return-void
.end method

.method public onActivitySaveInstanceState(Landroid/app/Activity;Landroid/os/Bundle;)V
    .locals 0

    .line 111
    invoke-direct {p0, p1, p2}, Lcrc641fb97271e3182cb7/ContextMenuButtonRenderer;->n_onActivitySaveInstanceState(Landroid/app/Activity;Landroid/os/Bundle;)V

    .line 112
    return-void
.end method

.method public onActivityStarted(Landroid/app/Activity;)V
    .locals 0

    .line 119
    invoke-direct {p0, p1}, Lcrc641fb97271e3182cb7/ContextMenuButtonRenderer;->n_onActivityStarted(Landroid/app/Activity;)V

    .line 120
    return-void
.end method

.method public onActivityStopped(Landroid/app/Activity;)V
    .locals 0

    .line 127
    invoke-direct {p0, p1}, Lcrc641fb97271e3182cb7/ContextMenuButtonRenderer;->n_onActivityStopped(Landroid/app/Activity;)V

    .line 128
    return-void
.end method

.method public onMenuItemClick(Landroid/view/MenuItem;)Z
    .locals 0

    .line 71
    invoke-direct {p0, p1}, Lcrc641fb97271e3182cb7/ContextMenuButtonRenderer;->n_onMenuItemClick(Landroid/view/MenuItem;)Z

    move-result p1

    return p1
.end method
