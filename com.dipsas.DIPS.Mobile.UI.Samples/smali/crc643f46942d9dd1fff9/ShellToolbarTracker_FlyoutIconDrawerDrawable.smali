.class public Lcrc643f46942d9dd1fff9/ShellToolbarTracker_FlyoutIconDrawerDrawable;
.super Landroidx/appcompat/graphics/drawable/DrawerArrowDrawable;
.source "ShellToolbarTracker_FlyoutIconDrawerDrawable.java"

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
    const-string v0, "n_draw:(Landroid/graphics/Canvas;)V:GetDraw_Landroid_graphics_Canvas_Handler\n"

    sput-object v0, Lcrc643f46942d9dd1fff9/ShellToolbarTracker_FlyoutIconDrawerDrawable;->__md_methods:Ljava/lang/String;

    .line 15
    const-class v1, Lcrc643f46942d9dd1fff9/ShellToolbarTracker_FlyoutIconDrawerDrawable;

    const-string v2, "Xamarin.Forms.Platform.Android.ShellToolbarTracker+FlyoutIconDrawerDrawable, Xamarin.Forms.Platform.Android"

    invoke-static {v2, v1, v0}, Lmono/android/Runtime;->register(Ljava/lang/String;Ljava/lang/Class;Ljava/lang/String;)V

    .line 16
    return-void
.end method

.method public constructor <init>(Landroid/content/Context;)V
    .locals 2

    .line 21
    invoke-direct {p0, p1}, Landroidx/appcompat/graphics/drawable/DrawerArrowDrawable;-><init>(Landroid/content/Context;)V

    .line 22
    invoke-virtual {p0}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    move-result-object v0

    const-class v1, Lcrc643f46942d9dd1fff9/ShellToolbarTracker_FlyoutIconDrawerDrawable;

    if-ne v0, v1, :cond_0

    .line 23
    const/4 v0, 0x1

    new-array v0, v0, [Ljava/lang/Object;

    const/4 v1, 0x0

    aput-object p1, v0, v1

    const-string p1, "Xamarin.Forms.Platform.Android.ShellToolbarTracker+FlyoutIconDrawerDrawable, Xamarin.Forms.Platform.Android"

    const-string v1, "Android.Content.Context, Mono.Android"

    invoke-static {p1, v1, p0, v0}, Lmono/android/TypeManager;->Activate(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Object;[Ljava/lang/Object;)V

    .line 25
    :cond_0
    return-void
.end method

.method private native n_draw(Landroid/graphics/Canvas;)V
.end method


# virtual methods
.method public draw(Landroid/graphics/Canvas;)V
    .locals 0

    .line 30
    invoke-direct {p0, p1}, Lcrc643f46942d9dd1fff9/ShellToolbarTracker_FlyoutIconDrawerDrawable;->n_draw(Landroid/graphics/Canvas;)V

    .line 31
    return-void
.end method

.method public monodroidAddReference(Ljava/lang/Object;)V
    .locals 1

    .line 38
    iget-object v0, p0, Lcrc643f46942d9dd1fff9/ShellToolbarTracker_FlyoutIconDrawerDrawable;->refList:Ljava/util/ArrayList;

    if-nez v0, :cond_0

    .line 39
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    iput-object v0, p0, Lcrc643f46942d9dd1fff9/ShellToolbarTracker_FlyoutIconDrawerDrawable;->refList:Ljava/util/ArrayList;

    .line 40
    :cond_0
    iget-object v0, p0, Lcrc643f46942d9dd1fff9/ShellToolbarTracker_FlyoutIconDrawerDrawable;->refList:Ljava/util/ArrayList;

    invoke-virtual {v0, p1}, Ljava/util/ArrayList;->add(Ljava/lang/Object;)Z

    .line 41
    return-void
.end method

.method public monodroidClearReferences()V
    .locals 1

    .line 45
    iget-object v0, p0, Lcrc643f46942d9dd1fff9/ShellToolbarTracker_FlyoutIconDrawerDrawable;->refList:Ljava/util/ArrayList;

    if-eqz v0, :cond_0

    .line 46
    invoke-virtual {v0}, Ljava/util/ArrayList;->clear()V

    .line 47
    :cond_0
    return-void
.end method
