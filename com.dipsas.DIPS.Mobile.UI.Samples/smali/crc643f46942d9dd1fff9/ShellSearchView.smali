.class public Lcrc643f46942d9dd1fff9/ShellSearchView;
.super Landroid/widget/FrameLayout;
.source "ShellSearchView.java"

# interfaces
.implements Lmono/android/IGCUserPeer;
.implements Landroid/widget/TextView$OnEditorActionListener;
.implements Landroid/text/TextWatcher;
.implements Landroid/text/NoCopySpan;


# static fields
.field public static final __md_methods:Ljava/lang/String;


# instance fields
.field private refList:Ljava/util/ArrayList;


# direct methods
.method static constructor <clinit>()V
    .locals 3

    .line 15
    const-string v0, "n_onAttachedToWindow:()V:GetOnAttachedToWindowHandler\nn_onLayout:(ZIIII)V:GetOnLayout_ZIIIIHandler\nn_onMeasure:(II)V:GetOnMeasure_IIHandler\nn_onEditorAction:(Landroid/widget/TextView;ILandroid/view/KeyEvent;)Z:GetOnEditorAction_Landroid_widget_TextView_ILandroid_view_KeyEvent_Handler:Android.Widget.TextView/IOnEditorActionListenerInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\nn_afterTextChanged:(Landroid/text/Editable;)V:GetAfterTextChanged_Landroid_text_Editable_Handler:Android.Text.ITextWatcherInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\nn_beforeTextChanged:(Ljava/lang/CharSequence;III)V:GetBeforeTextChanged_Ljava_lang_CharSequence_IIIHandler:Android.Text.ITextWatcherInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\nn_onTextChanged:(Ljava/lang/CharSequence;III)V:GetOnTextChanged_Ljava_lang_CharSequence_IIIHandler:Android.Text.ITextWatcherInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n"

    sput-object v0, Lcrc643f46942d9dd1fff9/ShellSearchView;->__md_methods:Ljava/lang/String;

    .line 24
    const-class v1, Lcrc643f46942d9dd1fff9/ShellSearchView;

    const-string v2, "Xamarin.Forms.Platform.Android.ShellSearchView, Xamarin.Forms.Platform.Android"

    invoke-static {v2, v1, v0}, Lmono/android/Runtime;->register(Ljava/lang/String;Ljava/lang/Class;Ljava/lang/String;)V

    .line 25
    return-void
.end method

.method public constructor <init>(Landroid/content/Context;)V
    .locals 2

    .line 30
    invoke-direct {p0, p1}, Landroid/widget/FrameLayout;-><init>(Landroid/content/Context;)V

    .line 31
    invoke-virtual {p0}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    move-result-object v0

    const-class v1, Lcrc643f46942d9dd1fff9/ShellSearchView;

    if-ne v0, v1, :cond_0

    .line 32
    const/4 v0, 0x1

    new-array v0, v0, [Ljava/lang/Object;

    const/4 v1, 0x0

    aput-object p1, v0, v1

    const-string p1, "Xamarin.Forms.Platform.Android.ShellSearchView, Xamarin.Forms.Platform.Android"

    const-string v1, "Android.Content.Context, Mono.Android"

    invoke-static {p1, v1, p0, v0}, Lmono/android/TypeManager;->Activate(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Object;[Ljava/lang/Object;)V

    .line 34
    :cond_0
    return-void
.end method

.method public constructor <init>(Landroid/content/Context;Landroid/util/AttributeSet;)V
    .locals 2

    .line 39
    invoke-direct {p0, p1, p2}, Landroid/widget/FrameLayout;-><init>(Landroid/content/Context;Landroid/util/AttributeSet;)V

    .line 40
    invoke-virtual {p0}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    move-result-object v0

    const-class v1, Lcrc643f46942d9dd1fff9/ShellSearchView;

    if-ne v0, v1, :cond_0

    .line 41
    const/4 v0, 0x2

    new-array v0, v0, [Ljava/lang/Object;

    const/4 v1, 0x0

    aput-object p1, v0, v1

    const/4 p1, 0x1

    aput-object p2, v0, p1

    const-string p1, "Xamarin.Forms.Platform.Android.ShellSearchView, Xamarin.Forms.Platform.Android"

    const-string p2, "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android"

    invoke-static {p1, p2, p0, v0}, Lmono/android/TypeManager;->Activate(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Object;[Ljava/lang/Object;)V

    .line 43
    :cond_0
    return-void
.end method

.method public constructor <init>(Landroid/content/Context;Landroid/util/AttributeSet;I)V
    .locals 2

    .line 48
    invoke-direct {p0, p1, p2, p3}, Landroid/widget/FrameLayout;-><init>(Landroid/content/Context;Landroid/util/AttributeSet;I)V

    .line 49
    invoke-virtual {p0}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    move-result-object v0

    const-class v1, Lcrc643f46942d9dd1fff9/ShellSearchView;

    if-ne v0, v1, :cond_0

    .line 50
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

    const-string p1, "Xamarin.Forms.Platform.Android.ShellSearchView, Xamarin.Forms.Platform.Android"

    const-string p2, "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android:System.Int32, mscorlib"

    invoke-static {p1, p2, p0, v0}, Lmono/android/TypeManager;->Activate(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Object;[Ljava/lang/Object;)V

    .line 52
    :cond_0
    return-void
.end method

.method public constructor <init>(Landroid/content/Context;Landroid/util/AttributeSet;II)V
    .locals 2

    .line 57
    invoke-direct {p0, p1, p2, p3, p4}, Landroid/widget/FrameLayout;-><init>(Landroid/content/Context;Landroid/util/AttributeSet;II)V

    .line 58
    invoke-virtual {p0}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    move-result-object v0

    const-class v1, Lcrc643f46942d9dd1fff9/ShellSearchView;

    if-ne v0, v1, :cond_0

    .line 59
    const/4 v0, 0x4

    new-array v0, v0, [Ljava/lang/Object;

    const/4 v1, 0x0

    aput-object p1, v0, v1

    const/4 p1, 0x1

    aput-object p2, v0, p1

    const/4 p1, 0x2

    invoke-static {p3}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object p2

    aput-object p2, v0, p1

    const/4 p1, 0x3

    invoke-static {p4}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object p2

    aput-object p2, v0, p1

    const-string p1, "Xamarin.Forms.Platform.Android.ShellSearchView, Xamarin.Forms.Platform.Android"

    const-string p2, "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android:System.Int32, mscorlib:System.Int32, mscorlib"

    invoke-static {p1, p2, p0, v0}, Lmono/android/TypeManager;->Activate(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Object;[Ljava/lang/Object;)V

    .line 61
    :cond_0
    return-void
.end method

.method private native n_afterTextChanged(Landroid/text/Editable;)V
.end method

.method private native n_beforeTextChanged(Ljava/lang/CharSequence;III)V
.end method

.method private native n_onAttachedToWindow()V
.end method

.method private native n_onEditorAction(Landroid/widget/TextView;ILandroid/view/KeyEvent;)Z
.end method

.method private native n_onLayout(ZIIII)V
.end method

.method private native n_onMeasure(II)V
.end method

.method private native n_onTextChanged(Ljava/lang/CharSequence;III)V
.end method


# virtual methods
.method public afterTextChanged(Landroid/text/Editable;)V
    .locals 0

    .line 98
    invoke-direct {p0, p1}, Lcrc643f46942d9dd1fff9/ShellSearchView;->n_afterTextChanged(Landroid/text/Editable;)V

    .line 99
    return-void
.end method

.method public beforeTextChanged(Ljava/lang/CharSequence;III)V
    .locals 0

    .line 106
    invoke-direct {p0, p1, p2, p3, p4}, Lcrc643f46942d9dd1fff9/ShellSearchView;->n_beforeTextChanged(Ljava/lang/CharSequence;III)V

    .line 107
    return-void
.end method

.method public monodroidAddReference(Ljava/lang/Object;)V
    .locals 1

    .line 122
    iget-object v0, p0, Lcrc643f46942d9dd1fff9/ShellSearchView;->refList:Ljava/util/ArrayList;

    if-nez v0, :cond_0

    .line 123
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    iput-object v0, p0, Lcrc643f46942d9dd1fff9/ShellSearchView;->refList:Ljava/util/ArrayList;

    .line 124
    :cond_0
    iget-object v0, p0, Lcrc643f46942d9dd1fff9/ShellSearchView;->refList:Ljava/util/ArrayList;

    invoke-virtual {v0, p1}, Ljava/util/ArrayList;->add(Ljava/lang/Object;)Z

    .line 125
    return-void
.end method

.method public monodroidClearReferences()V
    .locals 1

    .line 129
    iget-object v0, p0, Lcrc643f46942d9dd1fff9/ShellSearchView;->refList:Ljava/util/ArrayList;

    if-eqz v0, :cond_0

    .line 130
    invoke-virtual {v0}, Ljava/util/ArrayList;->clear()V

    .line 131
    :cond_0
    return-void
.end method

.method public onAttachedToWindow()V
    .locals 0

    .line 66
    invoke-direct {p0}, Lcrc643f46942d9dd1fff9/ShellSearchView;->n_onAttachedToWindow()V

    .line 67
    return-void
.end method

.method public onEditorAction(Landroid/widget/TextView;ILandroid/view/KeyEvent;)Z
    .locals 0

    .line 90
    invoke-direct {p0, p1, p2, p3}, Lcrc643f46942d9dd1fff9/ShellSearchView;->n_onEditorAction(Landroid/widget/TextView;ILandroid/view/KeyEvent;)Z

    move-result p1

    return p1
.end method

.method public onLayout(ZIIII)V
    .locals 0

    .line 74
    invoke-direct/range {p0 .. p5}, Lcrc643f46942d9dd1fff9/ShellSearchView;->n_onLayout(ZIIII)V

    .line 75
    return-void
.end method

.method public onMeasure(II)V
    .locals 0

    .line 82
    invoke-direct {p0, p1, p2}, Lcrc643f46942d9dd1fff9/ShellSearchView;->n_onMeasure(II)V

    .line 83
    return-void
.end method

.method public onTextChanged(Ljava/lang/CharSequence;III)V
    .locals 0

    .line 114
    invoke-direct {p0, p1, p2, p3, p4}, Lcrc643f46942d9dd1fff9/ShellSearchView;->n_onTextChanged(Ljava/lang/CharSequence;III)V

    .line 115
    return-void
.end method
