.class public Lcrc643f46942d9dd1fff9/InnerGestureListener;
.super Ljava/lang/Object;
.source "InnerGestureListener.java"

# interfaces
.implements Lmono/android/IGCUserPeer;
.implements Landroid/view/GestureDetector$OnGestureListener;
.implements Landroid/view/GestureDetector$OnDoubleTapListener;


# static fields
.field public static final __md_methods:Ljava/lang/String;


# instance fields
.field private refList:Ljava/util/ArrayList;


# direct methods
.method static constructor <clinit>()V
    .locals 3

    .line 14
    const-string v0, "n_onDown:(Landroid/view/MotionEvent;)Z:GetOnDown_Landroid_view_MotionEvent_Handler:Android.Views.GestureDetector/IOnGestureListenerInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\nn_onFling:(Landroid/view/MotionEvent;Landroid/view/MotionEvent;FF)Z:GetOnFling_Landroid_view_MotionEvent_Landroid_view_MotionEvent_FFHandler:Android.Views.GestureDetector/IOnGestureListenerInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\nn_onLongPress:(Landroid/view/MotionEvent;)V:GetOnLongPress_Landroid_view_MotionEvent_Handler:Android.Views.GestureDetector/IOnGestureListenerInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\nn_onScroll:(Landroid/view/MotionEvent;Landroid/view/MotionEvent;FF)Z:GetOnScroll_Landroid_view_MotionEvent_Landroid_view_MotionEvent_FFHandler:Android.Views.GestureDetector/IOnGestureListenerInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\nn_onShowPress:(Landroid/view/MotionEvent;)V:GetOnShowPress_Landroid_view_MotionEvent_Handler:Android.Views.GestureDetector/IOnGestureListenerInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\nn_onSingleTapUp:(Landroid/view/MotionEvent;)Z:GetOnSingleTapUp_Landroid_view_MotionEvent_Handler:Android.Views.GestureDetector/IOnGestureListenerInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\nn_onDoubleTap:(Landroid/view/MotionEvent;)Z:GetOnDoubleTap_Landroid_view_MotionEvent_Handler:Android.Views.GestureDetector/IOnDoubleTapListenerInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\nn_onDoubleTapEvent:(Landroid/view/MotionEvent;)Z:GetOnDoubleTapEvent_Landroid_view_MotionEvent_Handler:Android.Views.GestureDetector/IOnDoubleTapListenerInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\nn_onSingleTapConfirmed:(Landroid/view/MotionEvent;)Z:GetOnSingleTapConfirmed_Landroid_view_MotionEvent_Handler:Android.Views.GestureDetector/IOnDoubleTapListenerInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n"

    sput-object v0, Lcrc643f46942d9dd1fff9/InnerGestureListener;->__md_methods:Ljava/lang/String;

    .line 25
    const-class v1, Lcrc643f46942d9dd1fff9/InnerGestureListener;

    const-string v2, "Xamarin.Forms.Platform.Android.InnerGestureListener, Xamarin.Forms.Platform.Android"

    invoke-static {v2, v1, v0}, Lmono/android/Runtime;->register(Ljava/lang/String;Ljava/lang/Class;Ljava/lang/String;)V

    .line 26
    return-void
.end method

.method public constructor <init>()V
    .locals 3

    .line 31
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 32
    invoke-virtual {p0}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    move-result-object v0

    const-class v1, Lcrc643f46942d9dd1fff9/InnerGestureListener;

    if-ne v0, v1, :cond_0

    .line 33
    const/4 v0, 0x0

    new-array v0, v0, [Ljava/lang/Object;

    const-string v1, "Xamarin.Forms.Platform.Android.InnerGestureListener, Xamarin.Forms.Platform.Android"

    const-string v2, ""

    invoke-static {v1, v2, p0, v0}, Lmono/android/TypeManager;->Activate(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Object;[Ljava/lang/Object;)V

    .line 35
    :cond_0
    return-void
.end method

.method private native n_onDoubleTap(Landroid/view/MotionEvent;)Z
.end method

.method private native n_onDoubleTapEvent(Landroid/view/MotionEvent;)Z
.end method

.method private native n_onDown(Landroid/view/MotionEvent;)Z
.end method

.method private native n_onFling(Landroid/view/MotionEvent;Landroid/view/MotionEvent;FF)Z
.end method

.method private native n_onLongPress(Landroid/view/MotionEvent;)V
.end method

.method private native n_onScroll(Landroid/view/MotionEvent;Landroid/view/MotionEvent;FF)Z
.end method

.method private native n_onShowPress(Landroid/view/MotionEvent;)V
.end method

.method private native n_onSingleTapConfirmed(Landroid/view/MotionEvent;)Z
.end method

.method private native n_onSingleTapUp(Landroid/view/MotionEvent;)Z
.end method


# virtual methods
.method public monodroidAddReference(Ljava/lang/Object;)V
    .locals 1

    .line 112
    iget-object v0, p0, Lcrc643f46942d9dd1fff9/InnerGestureListener;->refList:Ljava/util/ArrayList;

    if-nez v0, :cond_0

    .line 113
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    iput-object v0, p0, Lcrc643f46942d9dd1fff9/InnerGestureListener;->refList:Ljava/util/ArrayList;

    .line 114
    :cond_0
    iget-object v0, p0, Lcrc643f46942d9dd1fff9/InnerGestureListener;->refList:Ljava/util/ArrayList;

    invoke-virtual {v0, p1}, Ljava/util/ArrayList;->add(Ljava/lang/Object;)Z

    .line 115
    return-void
.end method

.method public monodroidClearReferences()V
    .locals 1

    .line 119
    iget-object v0, p0, Lcrc643f46942d9dd1fff9/InnerGestureListener;->refList:Ljava/util/ArrayList;

    if-eqz v0, :cond_0

    .line 120
    invoke-virtual {v0}, Ljava/util/ArrayList;->clear()V

    .line 121
    :cond_0
    return-void
.end method

.method public onDoubleTap(Landroid/view/MotionEvent;)Z
    .locals 0

    .line 88
    invoke-direct {p0, p1}, Lcrc643f46942d9dd1fff9/InnerGestureListener;->n_onDoubleTap(Landroid/view/MotionEvent;)Z

    move-result p1

    return p1
.end method

.method public onDoubleTapEvent(Landroid/view/MotionEvent;)Z
    .locals 0

    .line 96
    invoke-direct {p0, p1}, Lcrc643f46942d9dd1fff9/InnerGestureListener;->n_onDoubleTapEvent(Landroid/view/MotionEvent;)Z

    move-result p1

    return p1
.end method

.method public onDown(Landroid/view/MotionEvent;)Z
    .locals 0

    .line 40
    invoke-direct {p0, p1}, Lcrc643f46942d9dd1fff9/InnerGestureListener;->n_onDown(Landroid/view/MotionEvent;)Z

    move-result p1

    return p1
.end method

.method public onFling(Landroid/view/MotionEvent;Landroid/view/MotionEvent;FF)Z
    .locals 0

    .line 48
    invoke-direct {p0, p1, p2, p3, p4}, Lcrc643f46942d9dd1fff9/InnerGestureListener;->n_onFling(Landroid/view/MotionEvent;Landroid/view/MotionEvent;FF)Z

    move-result p1

    return p1
.end method

.method public onLongPress(Landroid/view/MotionEvent;)V
    .locals 0

    .line 56
    invoke-direct {p0, p1}, Lcrc643f46942d9dd1fff9/InnerGestureListener;->n_onLongPress(Landroid/view/MotionEvent;)V

    .line 57
    return-void
.end method

.method public onScroll(Landroid/view/MotionEvent;Landroid/view/MotionEvent;FF)Z
    .locals 0

    .line 64
    invoke-direct {p0, p1, p2, p3, p4}, Lcrc643f46942d9dd1fff9/InnerGestureListener;->n_onScroll(Landroid/view/MotionEvent;Landroid/view/MotionEvent;FF)Z

    move-result p1

    return p1
.end method

.method public onShowPress(Landroid/view/MotionEvent;)V
    .locals 0

    .line 72
    invoke-direct {p0, p1}, Lcrc643f46942d9dd1fff9/InnerGestureListener;->n_onShowPress(Landroid/view/MotionEvent;)V

    .line 73
    return-void
.end method

.method public onSingleTapConfirmed(Landroid/view/MotionEvent;)Z
    .locals 0

    .line 104
    invoke-direct {p0, p1}, Lcrc643f46942d9dd1fff9/InnerGestureListener;->n_onSingleTapConfirmed(Landroid/view/MotionEvent;)Z

    move-result p1

    return p1
.end method

.method public onSingleTapUp(Landroid/view/MotionEvent;)Z
    .locals 0

    .line 80
    invoke-direct {p0, p1}, Lcrc643f46942d9dd1fff9/InnerGestureListener;->n_onSingleTapUp(Landroid/view/MotionEvent;)Z

    move-result p1

    return p1
.end method
