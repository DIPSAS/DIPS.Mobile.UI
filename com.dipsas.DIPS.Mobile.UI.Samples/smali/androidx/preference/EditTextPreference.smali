.class public Landroidx/preference/EditTextPreference;
.super Landroidx/preference/DialogPreference;
.source "EditTextPreference.java"


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Landroidx/preference/EditTextPreference$SimpleSummaryProvider;,
        Landroidx/preference/EditTextPreference$SavedState;,
        Landroidx/preference/EditTextPreference$OnBindEditTextListener;
    }
.end annotation


# instance fields
.field private mOnBindEditTextListener:Landroidx/preference/EditTextPreference$OnBindEditTextListener;

.field private mText:Ljava/lang/String;


# direct methods
.method public constructor <init>(Landroid/content/Context;)V
    .locals 1
    .param p1, "context"    # Landroid/content/Context;

    .line 67
    const/4 v0, 0x0

    invoke-direct {p0, p1, v0}, Landroidx/preference/EditTextPreference;-><init>(Landroid/content/Context;Landroid/util/AttributeSet;)V

    .line 68
    return-void
.end method

.method public constructor <init>(Landroid/content/Context;Landroid/util/AttributeSet;)V
    .locals 2
    .param p1, "context"    # Landroid/content/Context;
    .param p2, "attrs"    # Landroid/util/AttributeSet;

    .line 62
    sget v0, Landroidx/preference/R$attr;->editTextPreferenceStyle:I

    const v1, 0x1010092

    invoke-static {p1, v0, v1}, Landroidx/core/content/res/TypedArrayUtils;->getAttr(Landroid/content/Context;II)I

    move-result v0

    invoke-direct {p0, p1, p2, v0}, Landroidx/preference/EditTextPreference;-><init>(Landroid/content/Context;Landroid/util/AttributeSet;I)V

    .line 64
    return-void
.end method

.method public constructor <init>(Landroid/content/Context;Landroid/util/AttributeSet;I)V
    .locals 1
    .param p1, "context"    # Landroid/content/Context;
    .param p2, "attrs"    # Landroid/util/AttributeSet;
    .param p3, "defStyleAttr"    # I

    .line 58
    const/4 v0, 0x0

    invoke-direct {p0, p1, p2, p3, v0}, Landroidx/preference/EditTextPreference;-><init>(Landroid/content/Context;Landroid/util/AttributeSet;II)V

    .line 59
    return-void
.end method

.method public constructor <init>(Landroid/content/Context;Landroid/util/AttributeSet;II)V
    .locals 4
    .param p1, "context"    # Landroid/content/Context;
    .param p2, "attrs"    # Landroid/util/AttributeSet;
    .param p3, "defStyleAttr"    # I
    .param p4, "defStyleRes"    # I

    .line 44
    invoke-direct {p0, p1, p2, p3, p4}, Landroidx/preference/DialogPreference;-><init>(Landroid/content/Context;Landroid/util/AttributeSet;II)V

    .line 46
    sget-object v0, Landroidx/preference/R$styleable;->EditTextPreference:[I

    invoke-virtual {p1, p2, v0, p3, p4}, Landroid/content/Context;->obtainStyledAttributes(Landroid/util/AttributeSet;[III)Landroid/content/res/TypedArray;

    move-result-object v0

    .line 49
    .local v0, "a":Landroid/content/res/TypedArray;
    sget v1, Landroidx/preference/R$styleable;->EditTextPreference_useSimpleSummaryProvider:I

    sget v2, Landroidx/preference/R$styleable;->EditTextPreference_useSimpleSummaryProvider:I

    const/4 v3, 0x0

    invoke-static {v0, v1, v2, v3}, Landroidx/core/content/res/TypedArrayUtils;->getBoolean(Landroid/content/res/TypedArray;IIZ)Z

    move-result v1

    if-eqz v1, :cond_0

    .line 51
    invoke-static {}, Landroidx/preference/EditTextPreference$SimpleSummaryProvider;->getInstance()Landroidx/preference/EditTextPreference$SimpleSummaryProvider;

    move-result-object v1

    invoke-virtual {p0, v1}, Landroidx/preference/EditTextPreference;->setSummaryProvider(Landroidx/preference/Preference$SummaryProvider;)V

    .line 54
    :cond_0
    invoke-virtual {v0}, Landroid/content/res/TypedArray;->recycle()V

    .line 55
    return-void
.end method


# virtual methods
.method getOnBindEditTextListener()Landroidx/preference/EditTextPreference$OnBindEditTextListener;
    .locals 1

    .line 162
    iget-object v0, p0, Landroidx/preference/EditTextPreference;->mOnBindEditTextListener:Landroidx/preference/EditTextPreference$OnBindEditTextListener;

    return-object v0
.end method

.method public getText()Ljava/lang/String;
    .locals 1

    .line 96
    iget-object v0, p0, Landroidx/preference/EditTextPreference;->mText:Ljava/lang/String;

    return-object v0
.end method

.method protected onGetDefaultValue(Landroid/content/res/TypedArray;I)Ljava/lang/Object;
    .locals 1
    .param p1, "a"    # Landroid/content/res/TypedArray;
    .param p2, "index"    # I

    .line 101
    invoke-virtual {p1, p2}, Landroid/content/res/TypedArray;->getString(I)Ljava/lang/String;

    move-result-object v0

    return-object v0
.end method

.method protected onRestoreInstanceState(Landroid/os/Parcelable;)V
    .locals 2
    .param p1, "state"    # Landroid/os/Parcelable;

    .line 129
    if-eqz p1, :cond_1

    invoke-virtual {p1}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    move-result-object v0

    const-class v1, Landroidx/preference/EditTextPreference$SavedState;

    invoke-virtual {v0, v1}, Ljava/lang/Object;->equals(Ljava/lang/Object;)Z

    move-result v0

    if-nez v0, :cond_0

    goto :goto_0

    .line 135
    :cond_0
    move-object v0, p1

    check-cast v0, Landroidx/preference/EditTextPreference$SavedState;

    .line 136
    .local v0, "myState":Landroidx/preference/EditTextPreference$SavedState;
    invoke-virtual {v0}, Landroidx/preference/EditTextPreference$SavedState;->getSuperState()Landroid/os/Parcelable;

    move-result-object v1

    invoke-super {p0, v1}, Landroidx/preference/DialogPreference;->onRestoreInstanceState(Landroid/os/Parcelable;)V

    .line 137
    iget-object v1, v0, Landroidx/preference/EditTextPreference$SavedState;->mText:Ljava/lang/String;

    invoke-virtual {p0, v1}, Landroidx/preference/EditTextPreference;->setText(Ljava/lang/String;)V

    .line 138
    return-void

    .line 131
    .end local v0    # "myState":Landroidx/preference/EditTextPreference$SavedState;
    :cond_1
    :goto_0
    invoke-super {p0, p1}, Landroidx/preference/DialogPreference;->onRestoreInstanceState(Landroid/os/Parcelable;)V

    .line 132
    return-void
.end method

.method protected onSaveInstanceState()Landroid/os/Parcelable;
    .locals 3

    .line 116
    invoke-super {p0}, Landroidx/preference/DialogPreference;->onSaveInstanceState()Landroid/os/Parcelable;

    move-result-object v0

    .line 117
    .local v0, "superState":Landroid/os/Parcelable;
    invoke-virtual {p0}, Landroidx/preference/EditTextPreference;->isPersistent()Z

    move-result v1

    if-eqz v1, :cond_0

    .line 119
    return-object v0

    .line 122
    :cond_0
    new-instance v1, Landroidx/preference/EditTextPreference$SavedState;

    invoke-direct {v1, v0}, Landroidx/preference/EditTextPreference$SavedState;-><init>(Landroid/os/Parcelable;)V

    .line 123
    .local v1, "myState":Landroidx/preference/EditTextPreference$SavedState;
    invoke-virtual {p0}, Landroidx/preference/EditTextPreference;->getText()Ljava/lang/String;

    move-result-object v2

    iput-object v2, v1, Landroidx/preference/EditTextPreference$SavedState;->mText:Ljava/lang/String;

    .line 124
    return-object v1
.end method

.method protected onSetInitialValue(Ljava/lang/Object;)V
    .locals 1
    .param p1, "defaultValue"    # Ljava/lang/Object;

    .line 106
    move-object v0, p1

    check-cast v0, Ljava/lang/String;

    invoke-virtual {p0, v0}, Landroidx/preference/EditTextPreference;->getPersistedString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v0

    invoke-virtual {p0, v0}, Landroidx/preference/EditTextPreference;->setText(Ljava/lang/String;)V

    .line 107
    return-void
.end method

.method public setOnBindEditTextListener(Landroidx/preference/EditTextPreference$OnBindEditTextListener;)V
    .locals 0
    .param p1, "onBindEditTextListener"    # Landroidx/preference/EditTextPreference$OnBindEditTextListener;

    .line 150
    iput-object p1, p0, Landroidx/preference/EditTextPreference;->mOnBindEditTextListener:Landroidx/preference/EditTextPreference$OnBindEditTextListener;

    .line 151
    return-void
.end method

.method public setText(Ljava/lang/String;)V
    .locals 2
    .param p1, "text"    # Ljava/lang/String;

    .line 76
    invoke-virtual {p0}, Landroidx/preference/EditTextPreference;->shouldDisableDependents()Z

    move-result v0

    .line 78
    .local v0, "wasBlocking":Z
    iput-object p1, p0, Landroidx/preference/EditTextPreference;->mText:Ljava/lang/String;

    .line 80
    invoke-virtual {p0, p1}, Landroidx/preference/EditTextPreference;->persistString(Ljava/lang/String;)Z

    .line 82
    invoke-virtual {p0}, Landroidx/preference/EditTextPreference;->shouldDisableDependents()Z

    move-result v1

    .line 83
    .local v1, "isBlocking":Z
    if-eq v1, v0, :cond_0

    .line 84
    invoke-virtual {p0, v1}, Landroidx/preference/EditTextPreference;->notifyDependencyChange(Z)V

    .line 87
    :cond_0
    invoke-virtual {p0}, Landroidx/preference/EditTextPreference;->notifyChanged()V

    .line 88
    return-void
.end method

.method public shouldDisableDependents()Z
    .locals 1

    .line 111
    iget-object v0, p0, Landroidx/preference/EditTextPreference;->mText:Ljava/lang/String;

    invoke-static {v0}, Landroid/text/TextUtils;->isEmpty(Ljava/lang/CharSequence;)Z

    move-result v0

    if-nez v0, :cond_1

    invoke-super {p0}, Landroidx/preference/DialogPreference;->shouldDisableDependents()Z

    move-result v0

    if-eqz v0, :cond_0

    goto :goto_0

    :cond_0
    const/4 v0, 0x0

    goto :goto_1

    :cond_1
    :goto_0
    const/4 v0, 0x1

    :goto_1
    return v0
.end method
