.class abstract Lcom/google/android/material/textfield/EndIconDelegate;
.super Ljava/lang/Object;
.source "EndIconDelegate.java"


# instance fields
.field context:Landroid/content/Context;

.field final customEndIcon:I

.field endIconView:Lcom/google/android/material/internal/CheckableImageButton;

.field textInputLayout:Lcom/google/android/material/textfield/TextInputLayout;


# direct methods
.method constructor <init>(Lcom/google/android/material/textfield/TextInputLayout;I)V
    .locals 1
    .param p1, "textInputLayout"    # Lcom/google/android/material/textfield/TextInputLayout;
    .param p2, "customEndIcon"    # I

    .line 40
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 41
    iput-object p1, p0, Lcom/google/android/material/textfield/EndIconDelegate;->textInputLayout:Lcom/google/android/material/textfield/TextInputLayout;

    .line 42
    invoke-virtual {p1}, Lcom/google/android/material/textfield/TextInputLayout;->getContext()Landroid/content/Context;

    move-result-object v0

    iput-object v0, p0, Lcom/google/android/material/textfield/EndIconDelegate;->context:Landroid/content/Context;

    .line 43
    invoke-virtual {p1}, Lcom/google/android/material/textfield/TextInputLayout;->getEndIconView()Lcom/google/android/material/internal/CheckableImageButton;

    move-result-object v0

    iput-object v0, p0, Lcom/google/android/material/textfield/EndIconDelegate;->endIconView:Lcom/google/android/material/internal/CheckableImageButton;

    .line 44
    iput p2, p0, Lcom/google/android/material/textfield/EndIconDelegate;->customEndIcon:I

    .line 45
    return-void
.end method


# virtual methods
.method abstract initialize()V
.end method

.method isBoxBackgroundModeSupported(I)Z
    .locals 1
    .param p1, "boxBackgroundMode"    # I

    .line 66
    const/4 v0, 0x1

    return v0
.end method

.method onSuffixVisibilityChanged(Z)V
    .locals 0
    .param p1, "visible"    # Z

    .line 75
    return-void
.end method

.method shouldTintIconOnError()Z
    .locals 1

    .line 57
    const/4 v0, 0x0

    return v0
.end method
