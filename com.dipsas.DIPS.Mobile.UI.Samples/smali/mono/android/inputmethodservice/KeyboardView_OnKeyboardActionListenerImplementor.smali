.class public Lmono/android/inputmethodservice/KeyboardView_OnKeyboardActionListenerImplementor;
.super Ljava/lang/Object;
.source "KeyboardView_OnKeyboardActionListenerImplementor.java"

# interfaces
.implements Lmono/android/IGCUserPeer;
.implements Landroid/inputmethodservice/KeyboardView$OnKeyboardActionListener;


# static fields
.field public static final __md_methods:Ljava/lang/String;


# instance fields
.field private refList:Ljava/util/ArrayList;


# direct methods
.method static constructor <clinit>()V
    .locals 3

    .line 13
    const-string v0, "n_onKey:(I[I)V:GetOnKey_IarrayIHandler:Android.InputMethodServices.KeyboardView/IOnKeyboardActionListenerInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\nn_onPress:(I)V:GetOnPress_IHandler:Android.InputMethodServices.KeyboardView/IOnKeyboardActionListenerInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\nn_onRelease:(I)V:GetOnRelease_IHandler:Android.InputMethodServices.KeyboardView/IOnKeyboardActionListenerInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\nn_onText:(Ljava/lang/CharSequence;)V:GetOnText_Ljava_lang_CharSequence_Handler:Android.InputMethodServices.KeyboardView/IOnKeyboardActionListenerInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\nn_swipeDown:()V:GetSwipeDownHandler:Android.InputMethodServices.KeyboardView/IOnKeyboardActionListenerInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\nn_swipeLeft:()V:GetSwipeLeftHandler:Android.InputMethodServices.KeyboardView/IOnKeyboardActionListenerInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\nn_swipeRight:()V:GetSwipeRightHandler:Android.InputMethodServices.KeyboardView/IOnKeyboardActionListenerInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\nn_swipeUp:()V:GetSwipeUpHandler:Android.InputMethodServices.KeyboardView/IOnKeyboardActionListenerInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n"

    sput-object v0, Lmono/android/inputmethodservice/KeyboardView_OnKeyboardActionListenerImplementor;->__md_methods:Ljava/lang/String;

    .line 23
    const-class v1, Lmono/android/inputmethodservice/KeyboardView_OnKeyboardActionListenerImplementor;

    const-string v2, "Android.InputMethodServices.KeyboardView+IOnKeyboardActionListenerImplementor, Mono.Android"

    invoke-static {v2, v1, v0}, Lmono/android/Runtime;->register(Ljava/lang/String;Ljava/lang/Class;Ljava/lang/String;)V

    .line 24
    return-void
.end method

.method public constructor <init>()V
    .locals 3

    .line 29
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 30
    invoke-virtual {p0}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    move-result-object v0

    const-class v1, Lmono/android/inputmethodservice/KeyboardView_OnKeyboardActionListenerImplementor;

    if-ne v0, v1, :cond_0

    .line 31
    const/4 v0, 0x0

    new-array v0, v0, [Ljava/lang/Object;

    const-string v1, "Android.InputMethodServices.KeyboardView+IOnKeyboardActionListenerImplementor, Mono.Android"

    const-string v2, ""

    invoke-static {v1, v2, p0, v0}, Lmono/android/TypeManager;->Activate(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Object;[Ljava/lang/Object;)V

    .line 33
    :cond_0
    return-void
.end method

.method private native n_onKey(I[I)V
.end method

.method private native n_onPress(I)V
.end method

.method private native n_onRelease(I)V
.end method

.method private native n_onText(Ljava/lang/CharSequence;)V
.end method

.method private native n_swipeDown()V
.end method

.method private native n_swipeLeft()V
.end method

.method private native n_swipeRight()V
.end method

.method private native n_swipeUp()V
.end method


# virtual methods
.method public monodroidAddReference(Ljava/lang/Object;)V
    .locals 1

    .line 102
    iget-object v0, p0, Lmono/android/inputmethodservice/KeyboardView_OnKeyboardActionListenerImplementor;->refList:Ljava/util/ArrayList;

    if-nez v0, :cond_0

    .line 103
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    iput-object v0, p0, Lmono/android/inputmethodservice/KeyboardView_OnKeyboardActionListenerImplementor;->refList:Ljava/util/ArrayList;

    .line 104
    :cond_0
    iget-object v0, p0, Lmono/android/inputmethodservice/KeyboardView_OnKeyboardActionListenerImplementor;->refList:Ljava/util/ArrayList;

    invoke-virtual {v0, p1}, Ljava/util/ArrayList;->add(Ljava/lang/Object;)Z

    .line 105
    return-void
.end method

.method public monodroidClearReferences()V
    .locals 1

    .line 109
    iget-object v0, p0, Lmono/android/inputmethodservice/KeyboardView_OnKeyboardActionListenerImplementor;->refList:Ljava/util/ArrayList;

    if-eqz v0, :cond_0

    .line 110
    invoke-virtual {v0}, Ljava/util/ArrayList;->clear()V

    .line 111
    :cond_0
    return-void
.end method

.method public onKey(I[I)V
    .locals 0

    .line 38
    invoke-direct {p0, p1, p2}, Lmono/android/inputmethodservice/KeyboardView_OnKeyboardActionListenerImplementor;->n_onKey(I[I)V

    .line 39
    return-void
.end method

.method public onPress(I)V
    .locals 0

    .line 46
    invoke-direct {p0, p1}, Lmono/android/inputmethodservice/KeyboardView_OnKeyboardActionListenerImplementor;->n_onPress(I)V

    .line 47
    return-void
.end method

.method public onRelease(I)V
    .locals 0

    .line 54
    invoke-direct {p0, p1}, Lmono/android/inputmethodservice/KeyboardView_OnKeyboardActionListenerImplementor;->n_onRelease(I)V

    .line 55
    return-void
.end method

.method public onText(Ljava/lang/CharSequence;)V
    .locals 0

    .line 62
    invoke-direct {p0, p1}, Lmono/android/inputmethodservice/KeyboardView_OnKeyboardActionListenerImplementor;->n_onText(Ljava/lang/CharSequence;)V

    .line 63
    return-void
.end method

.method public swipeDown()V
    .locals 0

    .line 70
    invoke-direct {p0}, Lmono/android/inputmethodservice/KeyboardView_OnKeyboardActionListenerImplementor;->n_swipeDown()V

    .line 71
    return-void
.end method

.method public swipeLeft()V
    .locals 0

    .line 78
    invoke-direct {p0}, Lmono/android/inputmethodservice/KeyboardView_OnKeyboardActionListenerImplementor;->n_swipeLeft()V

    .line 79
    return-void
.end method

.method public swipeRight()V
    .locals 0

    .line 86
    invoke-direct {p0}, Lmono/android/inputmethodservice/KeyboardView_OnKeyboardActionListenerImplementor;->n_swipeRight()V

    .line 87
    return-void
.end method

.method public swipeUp()V
    .locals 0

    .line 94
    invoke-direct {p0}, Lmono/android/inputmethodservice/KeyboardView_OnKeyboardActionListenerImplementor;->n_swipeUp()V

    .line 95
    return-void
.end method
