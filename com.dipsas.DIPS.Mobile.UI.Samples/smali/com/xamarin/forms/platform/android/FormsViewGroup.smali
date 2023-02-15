.class public Lcom/xamarin/forms/platform/android/FormsViewGroup;
.super Landroid/view/ViewGroup;
.source "FormsViewGroup.java"


# instance fields
.field inputTransparent:Z


# direct methods
.method public constructor <init>(Landroid/content/Context;)V
    .locals 0
    .param p1, "context"    # Landroid/content/Context;

    .line 20
    invoke-direct {p0, p1}, Landroid/view/ViewGroup;-><init>(Landroid/content/Context;)V

    .line 22
    return-void
.end method

.method public constructor <init>(Landroid/content/Context;Landroid/util/AttributeSet;)V
    .locals 0
    .param p1, "context"    # Landroid/content/Context;
    .param p2, "attrs"    # Landroid/util/AttributeSet;

    .line 25
    invoke-direct {p0, p1, p2}, Landroid/view/ViewGroup;-><init>(Landroid/content/Context;Landroid/util/AttributeSet;)V

    .line 27
    return-void
.end method

.method public constructor <init>(Landroid/content/Context;Landroid/util/AttributeSet;I)V
    .locals 0
    .param p1, "context"    # Landroid/content/Context;
    .param p2, "attrs"    # Landroid/util/AttributeSet;
    .param p3, "defStyle"    # I

    .line 30
    invoke-direct {p0, p1, p2, p3}, Landroid/view/ViewGroup;-><init>(Landroid/content/Context;Landroid/util/AttributeSet;I)V

    .line 32
    return-void
.end method

.method public static sendViewBatchUpdate(Landroid/view/View;FFIZFFFFFFFF)V
    .locals 1
    .param p0, "view"    # Landroid/view/View;
    .param p1, "pivotX"    # F
    .param p2, "pivotY"    # F
    .param p3, "visibility"    # I
    .param p4, "enabled"    # Z
    .param p5, "opacity"    # F
    .param p6, "rotation"    # F
    .param p7, "rotationX"    # F
    .param p8, "rotationY"    # F
    .param p9, "scaleX"    # F
    .param p10, "scaleY"    # F
    .param p11, "translationX"    # F
    .param p12, "translationY"    # F

    .line 120
    invoke-virtual {p0, p1}, Landroid/view/View;->setPivotX(F)V

    .line 121
    invoke-virtual {p0, p2}, Landroid/view/View;->setPivotY(F)V

    .line 123
    invoke-virtual {p0}, Landroid/view/View;->getVisibility()I

    move-result v0

    if-eq v0, p3, :cond_0

    .line 124
    invoke-virtual {p0, p3}, Landroid/view/View;->setVisibility(I)V

    .line 126
    :cond_0
    invoke-virtual {p0}, Landroid/view/View;->isEnabled()Z

    move-result v0

    if-eq v0, p4, :cond_1

    .line 127
    invoke-virtual {p0, p4}, Landroid/view/View;->setEnabled(Z)V

    .line 129
    :cond_1
    invoke-virtual {p0, p5}, Landroid/view/View;->setAlpha(F)V

    .line 130
    invoke-virtual {p0, p6}, Landroid/view/View;->setRotation(F)V

    .line 131
    invoke-virtual {p0, p7}, Landroid/view/View;->setRotationX(F)V

    .line 132
    invoke-virtual {p0, p8}, Landroid/view/View;->setRotationY(F)V

    .line 133
    invoke-virtual {p0, p9}, Landroid/view/View;->setScaleX(F)V

    .line 134
    invoke-virtual {p0, p10}, Landroid/view/View;->setScaleY(F)V

    .line 135
    invoke-virtual {p0, p11}, Landroid/view/View;->setTranslationX(F)V

    .line 136
    invoke-virtual {p0, p12}, Landroid/view/View;->setTranslationY(F)V

    .line 137
    return-void
.end method


# virtual methods
.method protected getInputTransparent()Z
    .locals 1

    .line 53
    iget-boolean v0, p0, Lcom/xamarin/forms/platform/android/FormsViewGroup;->inputTransparent:Z

    return v0
.end method

.method public measureAndLayout(IIIIII)V
    .locals 0
    .param p1, "widthMeasureSpec"    # I
    .param p2, "heightMeasureSpec"    # I
    .param p3, "l"    # I
    .param p4, "t"    # I
    .param p5, "r"    # I
    .param p6, "b"    # I

    .line 36
    invoke-virtual {p0, p1, p2}, Lcom/xamarin/forms/platform/android/FormsViewGroup;->measure(II)V

    .line 37
    invoke-virtual {p0, p3, p4, p5, p6}, Lcom/xamarin/forms/platform/android/FormsViewGroup;->layout(IIII)V

    .line 38
    return-void
.end method

.method public onInterceptTouchEvent(Landroid/view/MotionEvent;)Z
    .locals 1
    .param p1, "ev"    # Landroid/view/MotionEvent;

    .line 59
    iget-boolean v0, p0, Lcom/xamarin/forms/platform/android/FormsViewGroup;->inputTransparent:Z

    if-eqz v0, :cond_0

    .line 60
    const/4 v0, 0x0

    return v0

    .line 62
    :cond_0
    invoke-super {p0, p1}, Landroid/view/ViewGroup;->onInterceptTouchEvent(Landroid/view/MotionEvent;)Z

    move-result v0

    return v0
.end method

.method protected onLayout(ZIIII)V
    .locals 0
    .param p1, "changed"    # Z
    .param p2, "l"    # I
    .param p3, "t"    # I
    .param p4, "r"    # I
    .param p5, "b"    # I

    .line 42
    return-void
.end method

.method public onTouchEvent(Landroid/view/MotionEvent;)Z
    .locals 1
    .param p1, "ev"    # Landroid/view/MotionEvent;

    .line 68
    iget-boolean v0, p0, Lcom/xamarin/forms/platform/android/FormsViewGroup;->inputTransparent:Z

    if-eqz v0, :cond_0

    .line 69
    const/4 v0, 0x0

    return v0

    .line 71
    :cond_0
    invoke-super {p0, p1}, Landroid/view/ViewGroup;->onTouchEvent(Landroid/view/MotionEvent;)Z

    move-result v0

    return v0
.end method

.method public sendBatchUpdate(FFIZFFFFFFFF)V
    .locals 1
    .param p1, "pivotX"    # F
    .param p2, "pivotY"    # F
    .param p3, "visibility"    # I
    .param p4, "enabled"    # Z
    .param p5, "opacity"    # F
    .param p6, "rotation"    # F
    .param p7, "rotationX"    # F
    .param p8, "rotationY"    # F
    .param p9, "scaleX"    # F
    .param p10, "scaleY"    # F
    .param p11, "translationX"    # F
    .param p12, "translationY"    # F

    .line 87
    invoke-virtual {p0, p1}, Lcom/xamarin/forms/platform/android/FormsViewGroup;->setPivotX(F)V

    .line 88
    invoke-virtual {p0, p2}, Lcom/xamarin/forms/platform/android/FormsViewGroup;->setPivotY(F)V

    .line 90
    invoke-virtual {p0}, Lcom/xamarin/forms/platform/android/FormsViewGroup;->getVisibility()I

    move-result v0

    if-eq v0, p3, :cond_0

    .line 91
    invoke-virtual {p0, p3}, Lcom/xamarin/forms/platform/android/FormsViewGroup;->setVisibility(I)V

    .line 93
    :cond_0
    invoke-virtual {p0}, Lcom/xamarin/forms/platform/android/FormsViewGroup;->isEnabled()Z

    move-result v0

    if-eq v0, p4, :cond_1

    .line 94
    invoke-virtual {p0, p4}, Lcom/xamarin/forms/platform/android/FormsViewGroup;->setEnabled(Z)V

    .line 96
    :cond_1
    invoke-virtual {p0, p5}, Lcom/xamarin/forms/platform/android/FormsViewGroup;->setAlpha(F)V

    .line 97
    invoke-virtual {p0, p6}, Lcom/xamarin/forms/platform/android/FormsViewGroup;->setRotation(F)V

    .line 98
    invoke-virtual {p0, p7}, Lcom/xamarin/forms/platform/android/FormsViewGroup;->setRotationX(F)V

    .line 99
    invoke-virtual {p0, p8}, Lcom/xamarin/forms/platform/android/FormsViewGroup;->setRotationY(F)V

    .line 100
    invoke-virtual {p0, p9}, Lcom/xamarin/forms/platform/android/FormsViewGroup;->setScaleX(F)V

    .line 101
    invoke-virtual {p0, p10}, Lcom/xamarin/forms/platform/android/FormsViewGroup;->setScaleY(F)V

    .line 102
    invoke-virtual {p0, p11}, Lcom/xamarin/forms/platform/android/FormsViewGroup;->setTranslationX(F)V

    .line 103
    invoke-virtual {p0, p12}, Lcom/xamarin/forms/platform/android/FormsViewGroup;->setTranslationY(F)V

    .line 104
    return-void
.end method

.method protected setInputTransparent(Z)V
    .locals 0
    .param p1, "value"    # Z

    .line 48
    iput-boolean p1, p0, Lcom/xamarin/forms/platform/android/FormsViewGroup;->inputTransparent:Z

    .line 49
    return-void
.end method
