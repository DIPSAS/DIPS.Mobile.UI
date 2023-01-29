.class public abstract Lcrc643f46942d9dd1fff9/EntryRendererBase_1;
.super Lcrc643f46942d9dd1fff9/ViewRenderer_2;
.source "EntryRendererBase_1.java"

# interfaces
.implements Lmono/android/IGCUserPeer;
.implements Landroid/text/TextWatcher;
.implements Landroid/text/NoCopySpan;
.implements Landroid/widget/TextView$OnEditorActionListener;


# static fields
.field public static final __md_methods:Ljava/lang/String;


# instance fields
.field private refList:Ljava/util/ArrayList;


# direct methods
.method static constructor <clinit>()V
    .locals 3

    .line 15
    const-string v0, "n_afterTextChanged:(Landroid/text/Editable;)V:GetAfterTextChanged_Landroid_text_Editable_Handler:Android.Text.ITextWatcherInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\nn_beforeTextChanged:(Ljava/lang/CharSequence;III)V:GetBeforeTextChanged_Ljava_lang_CharSequence_IIIHandler:Android.Text.ITextWatcherInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\nn_onTextChanged:(Ljava/lang/CharSequence;III)V:GetOnTextChanged_Ljava_lang_CharSequence_IIIHandler:Android.Text.ITextWatcherInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\nn_onEditorAction:(Landroid/widget/TextView;ILandroid/view/KeyEvent;)Z:GetOnEditorAction_Landroid_widget_TextView_ILandroid_view_KeyEvent_Handler:Android.Widget.TextView/IOnEditorActionListenerInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n"

    sput-object v0, Lcrc643f46942d9dd1fff9/EntryRendererBase_1;->__md_methods:Ljava/lang/String;

    .line 21
    const-class v1, Lcrc643f46942d9dd1fff9/EntryRendererBase_1;

    const-string v2, "Xamarin.Forms.Platform.Android.EntryRendererBase`1, Xamarin.Forms.Platform.Android"

    invoke-static {v2, v1, v0}, Lmono/android/Runtime;->register(Ljava/lang/String;Ljava/lang/Class;Ljava/lang/String;)V

    .line 22
    return-void
.end method

.method public constructor <init>(Landroid/content/Context;)V
    .locals 2

    .line 27
    invoke-direct {p0, p1}, Lcrc643f46942d9dd1fff9/ViewRenderer_2;-><init>(Landroid/content/Context;)V

    .line 28
    invoke-virtual {p0}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    move-result-object v0

    const-class v1, Lcrc643f46942d9dd1fff9/EntryRendererBase_1;

    if-ne v0, v1, :cond_0

    .line 29
    const/4 v0, 0x1

    new-array v0, v0, [Ljava/lang/Object;

    const/4 v1, 0x0

    aput-object p1, v0, v1

    const-string p1, "Xamarin.Forms.Platform.Android.EntryRendererBase`1, Xamarin.Forms.Platform.Android"

    const-string v1, "Android.Content.Context, Mono.Android"

    invoke-static {p1, v1, p0, v0}, Lmono/android/TypeManager;->Activate(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Object;[Ljava/lang/Object;)V

    .line 31
    :cond_0
    return-void
.end method

.method public constructor <init>(Landroid/content/Context;Landroid/util/AttributeSet;)V
    .locals 2

    .line 36
    invoke-direct {p0, p1, p2}, Lcrc643f46942d9dd1fff9/ViewRenderer_2;-><init>(Landroid/content/Context;Landroid/util/AttributeSet;)V

    .line 37
    invoke-virtual {p0}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    move-result-object v0

    const-class v1, Lcrc643f46942d9dd1fff9/EntryRendererBase_1;

    if-ne v0, v1, :cond_0

    .line 38
    const/4 v0, 0x2

    new-array v0, v0, [Ljava/lang/Object;

    const/4 v1, 0x0

    aput-object p1, v0, v1

    const/4 p1, 0x1

    aput-object p2, v0, p1

    const-string p1, "Xamarin.Forms.Platform.Android.EntryRendererBase`1, Xamarin.Forms.Platform.Android"

    const-string p2, "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android"

    invoke-static {p1, p2, p0, v0}, Lmono/android/TypeManager;->Activate(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Object;[Ljava/lang/Object;)V

    .line 40
    :cond_0
    return-void
.end method

.method public constructor <init>(Landroid/content/Context;Landroid/util/AttributeSet;I)V
    .locals 2

    .line 45
    invoke-direct {p0, p1, p2, p3}, Lcrc643f46942d9dd1fff9/ViewRenderer_2;-><init>(Landroid/content/Context;Landroid/util/AttributeSet;I)V

    .line 46
    invoke-virtual {p0}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    move-result-object v0

    const-class v1, Lcrc643f46942d9dd1fff9/EntryRendererBase_1;

    if-ne v0, v1, :cond_0

    .line 47
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

    const-string p1, "Xamarin.Forms.Platform.Android.EntryRendererBase`1, Xamarin.Forms.Platform.Android"

    const-string p2, "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android:System.Int32, mscorlib"

    invoke-static {p1, p2, p0, v0}, Lmono/android/TypeManager;->Activate(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Object;[Ljava/lang/Object;)V

    .line 49
    :cond_0
    return-void
.end method

.method private native n_afterTextChanged(Landroid/text/Editable;)V
.end method

.method private native n_beforeTextChanged(Ljava/lang/CharSequence;III)V
.end method

.method private native n_onEditorAction(Landroid/widget/TextView;ILandroid/view/KeyEvent;)Z
.end method

.method private native n_onTextChanged(Ljava/lang/CharSequence;III)V
.end method


# virtual methods
.method public afterTextChanged(Landroid/text/Editable;)V
    .locals 0

    .line 54
    invoke-direct {p0, p1}, Lcrc643f46942d9dd1fff9/EntryRendererBase_1;->n_afterTextChanged(Landroid/text/Editable;)V

    .line 55
    return-void
.end method

.method public beforeTextChanged(Ljava/lang/CharSequence;III)V
    .locals 0

    .line 62
    invoke-direct {p0, p1, p2, p3, p4}, Lcrc643f46942d9dd1fff9/EntryRendererBase_1;->n_beforeTextChanged(Ljava/lang/CharSequence;III)V

    .line 63
    return-void
.end method

.method public monodroidAddReference(Ljava/lang/Object;)V
    .locals 1

    .line 86
    iget-object v0, p0, Lcrc643f46942d9dd1fff9/EntryRendererBase_1;->refList:Ljava/util/ArrayList;

    if-nez v0, :cond_0

    .line 87
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    iput-object v0, p0, Lcrc643f46942d9dd1fff9/EntryRendererBase_1;->refList:Ljava/util/ArrayList;

    .line 88
    :cond_0
    iget-object v0, p0, Lcrc643f46942d9dd1fff9/EntryRendererBase_1;->refList:Ljava/util/ArrayList;

    invoke-virtual {v0, p1}, Ljava/util/ArrayList;->add(Ljava/lang/Object;)Z

    .line 89
    return-void
.end method

.method public monodroidClearReferences()V
    .locals 1

    .line 93
    iget-object v0, p0, Lcrc643f46942d9dd1fff9/EntryRendererBase_1;->refList:Ljava/util/ArrayList;

    if-eqz v0, :cond_0

    .line 94
    invoke-virtual {v0}, Ljava/util/ArrayList;->clear()V

    .line 95
    :cond_0
    return-void
.end method

.method public onEditorAction(Landroid/widget/TextView;ILandroid/view/KeyEvent;)Z
    .locals 0

    .line 78
    invoke-direct {p0, p1, p2, p3}, Lcrc643f46942d9dd1fff9/EntryRendererBase_1;->n_onEditorAction(Landroid/widget/TextView;ILandroid/view/KeyEvent;)Z

    move-result p1

    return p1
.end method

.method public onTextChanged(Ljava/lang/CharSequence;III)V
    .locals 0

    .line 70
    invoke-direct {p0, p1, p2, p3, p4}, Lcrc643f46942d9dd1fff9/EntryRendererBase_1;->n_onTextChanged(Ljava/lang/CharSequence;III)V

    .line 71
    return-void
.end method
