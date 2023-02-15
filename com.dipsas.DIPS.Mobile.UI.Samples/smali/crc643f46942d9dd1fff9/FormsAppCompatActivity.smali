.class public Lcrc643f46942d9dd1fff9/FormsAppCompatActivity;
.super Landroidx/appcompat/app/AppCompatActivity;
.source "FormsAppCompatActivity.java"

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
    const-string v0, "n_onBackPressed:()V:GetOnBackPressedHandler\nn_onConfigurationChanged:(Landroid/content/res/Configuration;)V:GetOnConfigurationChanged_Landroid_content_res_Configuration_Handler\nn_onOptionsItemSelected:(Landroid/view/MenuItem;)Z:GetOnOptionsItemSelected_Landroid_view_MenuItem_Handler\nn_onActivityResult:(IILandroid/content/Intent;)V:GetOnActivityResult_IILandroid_content_Intent_Handler\nn_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\nn_onDestroy:()V:GetOnDestroyHandler\nn_onNewIntent:(Landroid/content/Intent;)V:GetOnNewIntent_Landroid_content_Intent_Handler\nn_onPause:()V:GetOnPauseHandler\nn_onRestart:()V:GetOnRestartHandler\nn_onResume:()V:GetOnResumeHandler\nn_onStart:()V:GetOnStartHandler\nn_onStop:()V:GetOnStopHandler\n"

    sput-object v0, Lcrc643f46942d9dd1fff9/FormsAppCompatActivity;->__md_methods:Ljava/lang/String;

    .line 26
    const-class v1, Lcrc643f46942d9dd1fff9/FormsAppCompatActivity;

    const-string v2, "Xamarin.Forms.Platform.Android.FormsAppCompatActivity, Xamarin.Forms.Platform.Android"

    invoke-static {v2, v1, v0}, Lmono/android/Runtime;->register(Ljava/lang/String;Ljava/lang/Class;Ljava/lang/String;)V

    .line 27
    return-void
.end method

.method public constructor <init>()V
    .locals 3

    .line 32
    invoke-direct {p0}, Landroidx/appcompat/app/AppCompatActivity;-><init>()V

    .line 33
    invoke-virtual {p0}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    move-result-object v0

    const-class v1, Lcrc643f46942d9dd1fff9/FormsAppCompatActivity;

    if-ne v0, v1, :cond_0

    .line 34
    const/4 v0, 0x0

    new-array v0, v0, [Ljava/lang/Object;

    const-string v1, "Xamarin.Forms.Platform.Android.FormsAppCompatActivity, Xamarin.Forms.Platform.Android"

    const-string v2, ""

    invoke-static {v1, v2, p0, v0}, Lmono/android/TypeManager;->Activate(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Object;[Ljava/lang/Object;)V

    .line 36
    :cond_0
    return-void
.end method

.method public constructor <init>(I)V
    .locals 2

    .line 41
    invoke-direct {p0, p1}, Landroidx/appcompat/app/AppCompatActivity;-><init>(I)V

    .line 42
    invoke-virtual {p0}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    move-result-object v0

    const-class v1, Lcrc643f46942d9dd1fff9/FormsAppCompatActivity;

    if-ne v0, v1, :cond_0

    .line 43
    const/4 v0, 0x1

    new-array v0, v0, [Ljava/lang/Object;

    const/4 v1, 0x0

    invoke-static {p1}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object p1

    aput-object p1, v0, v1

    const-string p1, "Xamarin.Forms.Platform.Android.FormsAppCompatActivity, Xamarin.Forms.Platform.Android"

    const-string v1, "System.Int32, mscorlib"

    invoke-static {p1, v1, p0, v0}, Lmono/android/TypeManager;->Activate(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Object;[Ljava/lang/Object;)V

    .line 45
    :cond_0
    return-void
.end method

.method private native n_onActivityResult(IILandroid/content/Intent;)V
.end method

.method private native n_onBackPressed()V
.end method

.method private native n_onConfigurationChanged(Landroid/content/res/Configuration;)V
.end method

.method private native n_onCreate(Landroid/os/Bundle;)V
.end method

.method private native n_onDestroy()V
.end method

.method private native n_onNewIntent(Landroid/content/Intent;)V
.end method

.method private native n_onOptionsItemSelected(Landroid/view/MenuItem;)Z
.end method

.method private native n_onPause()V
.end method

.method private native n_onRestart()V
.end method

.method private native n_onResume()V
.end method

.method private native n_onStart()V
.end method

.method private native n_onStop()V
.end method


# virtual methods
.method public monodroidAddReference(Ljava/lang/Object;)V
    .locals 1

    .line 146
    iget-object v0, p0, Lcrc643f46942d9dd1fff9/FormsAppCompatActivity;->refList:Ljava/util/ArrayList;

    if-nez v0, :cond_0

    .line 147
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    iput-object v0, p0, Lcrc643f46942d9dd1fff9/FormsAppCompatActivity;->refList:Ljava/util/ArrayList;

    .line 148
    :cond_0
    iget-object v0, p0, Lcrc643f46942d9dd1fff9/FormsAppCompatActivity;->refList:Ljava/util/ArrayList;

    invoke-virtual {v0, p1}, Ljava/util/ArrayList;->add(Ljava/lang/Object;)Z

    .line 149
    return-void
.end method

.method public monodroidClearReferences()V
    .locals 1

    .line 153
    iget-object v0, p0, Lcrc643f46942d9dd1fff9/FormsAppCompatActivity;->refList:Ljava/util/ArrayList;

    if-eqz v0, :cond_0

    .line 154
    invoke-virtual {v0}, Ljava/util/ArrayList;->clear()V

    .line 155
    :cond_0
    return-void
.end method

.method public onActivityResult(IILandroid/content/Intent;)V
    .locals 0

    .line 74
    invoke-direct {p0, p1, p2, p3}, Lcrc643f46942d9dd1fff9/FormsAppCompatActivity;->n_onActivityResult(IILandroid/content/Intent;)V

    .line 75
    return-void
.end method

.method public onBackPressed()V
    .locals 0

    .line 50
    invoke-direct {p0}, Lcrc643f46942d9dd1fff9/FormsAppCompatActivity;->n_onBackPressed()V

    .line 51
    return-void
.end method

.method public onConfigurationChanged(Landroid/content/res/Configuration;)V
    .locals 0

    .line 58
    invoke-direct {p0, p1}, Lcrc643f46942d9dd1fff9/FormsAppCompatActivity;->n_onConfigurationChanged(Landroid/content/res/Configuration;)V

    .line 59
    return-void
.end method

.method public onCreate(Landroid/os/Bundle;)V
    .locals 0

    .line 82
    invoke-direct {p0, p1}, Lcrc643f46942d9dd1fff9/FormsAppCompatActivity;->n_onCreate(Landroid/os/Bundle;)V

    .line 83
    return-void
.end method

.method public onDestroy()V
    .locals 0

    .line 90
    invoke-direct {p0}, Lcrc643f46942d9dd1fff9/FormsAppCompatActivity;->n_onDestroy()V

    .line 91
    return-void
.end method

.method public onNewIntent(Landroid/content/Intent;)V
    .locals 0

    .line 98
    invoke-direct {p0, p1}, Lcrc643f46942d9dd1fff9/FormsAppCompatActivity;->n_onNewIntent(Landroid/content/Intent;)V

    .line 99
    return-void
.end method

.method public onOptionsItemSelected(Landroid/view/MenuItem;)Z
    .locals 0

    .line 66
    invoke-direct {p0, p1}, Lcrc643f46942d9dd1fff9/FormsAppCompatActivity;->n_onOptionsItemSelected(Landroid/view/MenuItem;)Z

    move-result p1

    return p1
.end method

.method public onPause()V
    .locals 0

    .line 106
    invoke-direct {p0}, Lcrc643f46942d9dd1fff9/FormsAppCompatActivity;->n_onPause()V

    .line 107
    return-void
.end method

.method public onRestart()V
    .locals 0

    .line 114
    invoke-direct {p0}, Lcrc643f46942d9dd1fff9/FormsAppCompatActivity;->n_onRestart()V

    .line 115
    return-void
.end method

.method public onResume()V
    .locals 0

    .line 122
    invoke-direct {p0}, Lcrc643f46942d9dd1fff9/FormsAppCompatActivity;->n_onResume()V

    .line 123
    return-void
.end method

.method public onStart()V
    .locals 0

    .line 130
    invoke-direct {p0}, Lcrc643f46942d9dd1fff9/FormsAppCompatActivity;->n_onStart()V

    .line 131
    return-void
.end method

.method public onStop()V
    .locals 0

    .line 138
    invoke-direct {p0}, Lcrc643f46942d9dd1fff9/FormsAppCompatActivity;->n_onStop()V

    .line 139
    return-void
.end method
