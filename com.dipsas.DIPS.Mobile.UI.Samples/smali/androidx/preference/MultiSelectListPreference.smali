.class public Landroidx/preference/MultiSelectListPreference;
.super Landroidx/preference/DialogPreference;
.source "MultiSelectListPreference.java"


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Landroidx/preference/MultiSelectListPreference$SavedState;
    }
.end annotation


# instance fields
.field private mEntries:[Ljava/lang/CharSequence;

.field private mEntryValues:[Ljava/lang/CharSequence;

.field private mValues:Ljava/util/Set;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/Set<",
            "Ljava/lang/String;",
            ">;"
        }
    .end annotation
.end field


# direct methods
.method public constructor <init>(Landroid/content/Context;)V
    .locals 1
    .param p1, "context"    # Landroid/content/Context;

    .line 77
    const/4 v0, 0x0

    invoke-direct {p0, p1, v0}, Landroidx/preference/MultiSelectListPreference;-><init>(Landroid/content/Context;Landroid/util/AttributeSet;)V

    .line 78
    return-void
.end method

.method public constructor <init>(Landroid/content/Context;Landroid/util/AttributeSet;)V
    .locals 2
    .param p1, "context"    # Landroid/content/Context;
    .param p2, "attrs"    # Landroid/util/AttributeSet;

    .line 71
    sget v0, Landroidx/preference/R$attr;->dialogPreferenceStyle:I

    const v1, 0x1010091

    invoke-static {p1, v0, v1}, Landroidx/core/content/res/TypedArrayUtils;->getAttr(Landroid/content/Context;II)I

    move-result v0

    invoke-direct {p0, p1, p2, v0}, Landroidx/preference/MultiSelectListPreference;-><init>(Landroid/content/Context;Landroid/util/AttributeSet;I)V

    .line 74
    return-void
.end method

.method public constructor <init>(Landroid/content/Context;Landroid/util/AttributeSet;I)V
    .locals 1
    .param p1, "context"    # Landroid/content/Context;
    .param p2, "attrs"    # Landroid/util/AttributeSet;
    .param p3, "defStyleAttr"    # I

    .line 67
    const/4 v0, 0x0

    invoke-direct {p0, p1, p2, p3, v0}, Landroidx/preference/MultiSelectListPreference;-><init>(Landroid/content/Context;Landroid/util/AttributeSet;II)V

    .line 68
    return-void
.end method

.method public constructor <init>(Landroid/content/Context;Landroid/util/AttributeSet;II)V
    .locals 3
    .param p1, "context"    # Landroid/content/Context;
    .param p2, "attrs"    # Landroid/util/AttributeSet;
    .param p3, "defStyleAttr"    # I
    .param p4, "defStyleRes"    # I

    .line 49
    invoke-direct {p0, p1, p2, p3, p4}, Landroidx/preference/DialogPreference;-><init>(Landroid/content/Context;Landroid/util/AttributeSet;II)V

    .line 45
    new-instance v0, Ljava/util/HashSet;

    invoke-direct {v0}, Ljava/util/HashSet;-><init>()V

    iput-object v0, p0, Landroidx/preference/MultiSelectListPreference;->mValues:Ljava/util/Set;

    .line 51
    sget-object v0, Landroidx/preference/R$styleable;->MultiSelectListPreference:[I

    invoke-virtual {p1, p2, v0, p3, p4}, Landroid/content/Context;->obtainStyledAttributes(Landroid/util/AttributeSet;[III)Landroid/content/res/TypedArray;

    move-result-object v0

    .line 55
    .local v0, "a":Landroid/content/res/TypedArray;
    sget v1, Landroidx/preference/R$styleable;->MultiSelectListPreference_entries:I

    sget v2, Landroidx/preference/R$styleable;->MultiSelectListPreference_android_entries:I

    invoke-static {v0, v1, v2}, Landroidx/core/content/res/TypedArrayUtils;->getTextArray(Landroid/content/res/TypedArray;II)[Ljava/lang/CharSequence;

    move-result-object v1

    iput-object v1, p0, Landroidx/preference/MultiSelectListPreference;->mEntries:[Ljava/lang/CharSequence;

    .line 59
    sget v1, Landroidx/preference/R$styleable;->MultiSelectListPreference_entryValues:I

    sget v2, Landroidx/preference/R$styleable;->MultiSelectListPreference_android_entryValues:I

    invoke-static {v0, v1, v2}, Landroidx/core/content/res/TypedArrayUtils;->getTextArray(Landroid/content/res/TypedArray;II)[Ljava/lang/CharSequence;

    move-result-object v1

    iput-object v1, p0, Landroidx/preference/MultiSelectListPreference;->mEntryValues:[Ljava/lang/CharSequence;

    .line 63
    invoke-virtual {v0}, Landroid/content/res/TypedArray;->recycle()V

    .line 64
    return-void
.end method


# virtual methods
.method public findIndexOfValue(Ljava/lang/String;)I
    .locals 2
    .param p1, "value"    # Ljava/lang/String;

    .line 167
    if-eqz p1, :cond_1

    iget-object v0, p0, Landroidx/preference/MultiSelectListPreference;->mEntryValues:[Ljava/lang/CharSequence;

    if-eqz v0, :cond_1

    .line 168
    array-length v0, v0

    add-int/lit8 v0, v0, -0x1

    .local v0, "i":I
    :goto_0
    if-ltz v0, :cond_1

    .line 169
    iget-object v1, p0, Landroidx/preference/MultiSelectListPreference;->mEntryValues:[Ljava/lang/CharSequence;

    aget-object v1, v1, v0

    invoke-virtual {v1, p1}, Ljava/lang/Object;->equals(Ljava/lang/Object;)Z

    move-result v1

    if-eqz v1, :cond_0

    .line 170
    return v0

    .line 168
    :cond_0
    add-int/lit8 v0, v0, -0x1

    goto :goto_0

    .line 174
    .end local v0    # "i":I
    :cond_1
    const/4 v0, -0x1

    return v0
.end method

.method public getEntries()[Ljava/lang/CharSequence;
    .locals 1

    .line 107
    iget-object v0, p0, Landroidx/preference/MultiSelectListPreference;->mEntries:[Ljava/lang/CharSequence;

    return-object v0
.end method

.method public getEntryValues()[Ljava/lang/CharSequence;
    .locals 1

    .line 135
    iget-object v0, p0, Landroidx/preference/MultiSelectListPreference;->mEntryValues:[Ljava/lang/CharSequence;

    return-object v0
.end method

.method protected getSelectedItems()[Z
    .locals 6

    .line 178
    iget-object v0, p0, Landroidx/preference/MultiSelectListPreference;->mEntryValues:[Ljava/lang/CharSequence;

    .line 179
    .local v0, "entries":[Ljava/lang/CharSequence;
    array-length v1, v0

    .line 180
    .local v1, "entryCount":I
    iget-object v2, p0, Landroidx/preference/MultiSelectListPreference;->mValues:Ljava/util/Set;

    .line 181
    .local v2, "values":Ljava/util/Set;, "Ljava/util/Set<Ljava/lang/String;>;"
    new-array v3, v1, [Z

    .line 183
    .local v3, "result":[Z
    const/4 v4, 0x0

    .local v4, "i":I
    :goto_0
    if-ge v4, v1, :cond_0

    .line 184
    aget-object v5, v0, v4

    invoke-interface {v5}, Ljava/lang/CharSequence;->toString()Ljava/lang/String;

    move-result-object v5

    invoke-interface {v2, v5}, Ljava/util/Set;->contains(Ljava/lang/Object;)Z

    move-result v5

    aput-boolean v5, v3, v4

    .line 183
    add-int/lit8 v4, v4, 0x1

    goto :goto_0

    .line 187
    .end local v4    # "i":I
    :cond_0
    return-object v3
.end method

.method public getValues()Ljava/util/Set;
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()",
            "Ljava/util/Set<",
            "Ljava/lang/String;",
            ">;"
        }
    .end annotation

    .line 157
    iget-object v0, p0, Landroidx/preference/MultiSelectListPreference;->mValues:Ljava/util/Set;

    return-object v0
.end method

.method protected onGetDefaultValue(Landroid/content/res/TypedArray;I)Ljava/lang/Object;
    .locals 6
    .param p1, "a"    # Landroid/content/res/TypedArray;
    .param p2, "index"    # I

    .line 192
    invoke-virtual {p1, p2}, Landroid/content/res/TypedArray;->getTextArray(I)[Ljava/lang/CharSequence;

    move-result-object v0

    .line 193
    .local v0, "defaultValues":[Ljava/lang/CharSequence;
    new-instance v1, Ljava/util/HashSet;

    invoke-direct {v1}, Ljava/util/HashSet;-><init>()V

    .line 195
    .local v1, "result":Ljava/util/Set;, "Ljava/util/Set<Ljava/lang/String;>;"
    array-length v2, v0

    const/4 v3, 0x0

    :goto_0
    if-ge v3, v2, :cond_0

    aget-object v4, v0, v3

    .line 196
    .local v4, "defaultValue":Ljava/lang/CharSequence;
    invoke-interface {v4}, Ljava/lang/CharSequence;->toString()Ljava/lang/String;

    move-result-object v5

    invoke-interface {v1, v5}, Ljava/util/Set;->add(Ljava/lang/Object;)Z

    .line 195
    .end local v4    # "defaultValue":Ljava/lang/CharSequence;
    add-int/lit8 v3, v3, 0x1

    goto :goto_0

    .line 199
    :cond_0
    return-object v1
.end method

.method protected onRestoreInstanceState(Landroid/os/Parcelable;)V
    .locals 2
    .param p1, "state"    # Landroid/os/Parcelable;

    .line 223
    if-eqz p1, :cond_1

    invoke-virtual {p1}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    move-result-object v0

    const-class v1, Landroidx/preference/MultiSelectListPreference$SavedState;

    invoke-virtual {v0, v1}, Ljava/lang/Object;->equals(Ljava/lang/Object;)Z

    move-result v0

    if-nez v0, :cond_0

    goto :goto_0

    .line 229
    :cond_0
    move-object v0, p1

    check-cast v0, Landroidx/preference/MultiSelectListPreference$SavedState;

    .line 230
    .local v0, "myState":Landroidx/preference/MultiSelectListPreference$SavedState;
    invoke-virtual {v0}, Landroidx/preference/MultiSelectListPreference$SavedState;->getSuperState()Landroid/os/Parcelable;

    move-result-object v1

    invoke-super {p0, v1}, Landroidx/preference/DialogPreference;->onRestoreInstanceState(Landroid/os/Parcelable;)V

    .line 231
    iget-object v1, v0, Landroidx/preference/MultiSelectListPreference$SavedState;->mValues:Ljava/util/Set;

    invoke-virtual {p0, v1}, Landroidx/preference/MultiSelectListPreference;->setValues(Ljava/util/Set;)V

    .line 232
    return-void

    .line 225
    .end local v0    # "myState":Landroidx/preference/MultiSelectListPreference$SavedState;
    :cond_1
    :goto_0
    invoke-super {p0, p1}, Landroidx/preference/DialogPreference;->onRestoreInstanceState(Landroid/os/Parcelable;)V

    .line 226
    return-void
.end method

.method protected onSaveInstanceState()Landroid/os/Parcelable;
    .locals 3

    .line 210
    invoke-super {p0}, Landroidx/preference/DialogPreference;->onSaveInstanceState()Landroid/os/Parcelable;

    move-result-object v0

    .line 211
    .local v0, "superState":Landroid/os/Parcelable;
    invoke-virtual {p0}, Landroidx/preference/MultiSelectListPreference;->isPersistent()Z

    move-result v1

    if-eqz v1, :cond_0

    .line 213
    return-object v0

    .line 216
    :cond_0
    new-instance v1, Landroidx/preference/MultiSelectListPreference$SavedState;

    invoke-direct {v1, v0}, Landroidx/preference/MultiSelectListPreference$SavedState;-><init>(Landroid/os/Parcelable;)V

    .line 217
    .local v1, "myState":Landroidx/preference/MultiSelectListPreference$SavedState;
    invoke-virtual {p0}, Landroidx/preference/MultiSelectListPreference;->getValues()Ljava/util/Set;

    move-result-object v2

    iput-object v2, v1, Landroidx/preference/MultiSelectListPreference$SavedState;->mValues:Ljava/util/Set;

    .line 218
    return-object v1
.end method

.method protected onSetInitialValue(Ljava/lang/Object;)V
    .locals 1
    .param p1, "defaultValue"    # Ljava/lang/Object;

    .line 205
    move-object v0, p1

    check-cast v0, Ljava/util/Set;

    invoke-virtual {p0, v0}, Landroidx/preference/MultiSelectListPreference;->getPersistedStringSet(Ljava/util/Set;)Ljava/util/Set;

    move-result-object v0

    invoke-virtual {p0, v0}, Landroidx/preference/MultiSelectListPreference;->setValues(Ljava/util/Set;)V

    .line 206
    return-void
.end method

.method public setEntries(I)V
    .locals 1
    .param p1, "entriesResId"    # I

    .line 98
    invoke-virtual {p0}, Landroidx/preference/MultiSelectListPreference;->getContext()Landroid/content/Context;

    move-result-object v0

    invoke-virtual {v0}, Landroid/content/Context;->getResources()Landroid/content/res/Resources;

    move-result-object v0

    invoke-virtual {v0, p1}, Landroid/content/res/Resources;->getTextArray(I)[Ljava/lang/CharSequence;

    move-result-object v0

    invoke-virtual {p0, v0}, Landroidx/preference/MultiSelectListPreference;->setEntries([Ljava/lang/CharSequence;)V

    .line 99
    return-void
.end method

.method public setEntries([Ljava/lang/CharSequence;)V
    .locals 0
    .param p1, "entries"    # [Ljava/lang/CharSequence;

    .line 90
    iput-object p1, p0, Landroidx/preference/MultiSelectListPreference;->mEntries:[Ljava/lang/CharSequence;

    .line 91
    return-void
.end method

.method public setEntryValues(I)V
    .locals 1
    .param p1, "entryValuesResId"    # I

    .line 126
    invoke-virtual {p0}, Landroidx/preference/MultiSelectListPreference;->getContext()Landroid/content/Context;

    move-result-object v0

    invoke-virtual {v0}, Landroid/content/Context;->getResources()Landroid/content/res/Resources;

    move-result-object v0

    invoke-virtual {v0, p1}, Landroid/content/res/Resources;->getTextArray(I)[Ljava/lang/CharSequence;

    move-result-object v0

    invoke-virtual {p0, v0}, Landroidx/preference/MultiSelectListPreference;->setEntryValues([Ljava/lang/CharSequence;)V

    .line 127
    return-void
.end method

.method public setEntryValues([Ljava/lang/CharSequence;)V
    .locals 0
    .param p1, "entryValues"    # [Ljava/lang/CharSequence;

    .line 118
    iput-object p1, p0, Landroidx/preference/MultiSelectListPreference;->mEntryValues:[Ljava/lang/CharSequence;

    .line 119
    return-void
.end method

.method public setValues(Ljava/util/Set;)V
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/Set<",
            "Ljava/lang/String;",
            ">;)V"
        }
    .end annotation

    .line 144
    .local p1, "values":Ljava/util/Set;, "Ljava/util/Set<Ljava/lang/String;>;"
    iget-object v0, p0, Landroidx/preference/MultiSelectListPreference;->mValues:Ljava/util/Set;

    invoke-interface {v0}, Ljava/util/Set;->clear()V

    .line 145
    iget-object v0, p0, Landroidx/preference/MultiSelectListPreference;->mValues:Ljava/util/Set;

    invoke-interface {v0, p1}, Ljava/util/Set;->addAll(Ljava/util/Collection;)Z

    .line 147
    invoke-virtual {p0, p1}, Landroidx/preference/MultiSelectListPreference;->persistStringSet(Ljava/util/Set;)Z

    .line 148
    invoke-virtual {p0}, Landroidx/preference/MultiSelectListPreference;->notifyChanged()V

    .line 149
    return-void
.end method
