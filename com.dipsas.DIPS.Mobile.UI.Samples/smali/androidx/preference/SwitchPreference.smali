.class public Landroidx/preference/SwitchPreference;
.super Landroidx/preference/TwoStatePreference;
.source "SwitchPreference.java"


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Landroidx/preference/SwitchPreference$Listener;
    }
.end annotation


# instance fields
.field private final mListener:Landroidx/preference/SwitchPreference$Listener;

.field private mSwitchOff:Ljava/lang/CharSequence;

.field private mSwitchOn:Ljava/lang/CharSequence;


# direct methods
.method public constructor <init>(Landroid/content/Context;)V
    .locals 1
    .param p1, "context"    # Landroid/content/Context;

    .line 122
    const/4 v0, 0x0

    invoke-direct {p0, p1, v0}, Landroidx/preference/SwitchPreference;-><init>(Landroid/content/Context;Landroid/util/AttributeSet;)V

    .line 123
    return-void
.end method

.method public constructor <init>(Landroid/content/Context;Landroid/util/AttributeSet;)V
    .locals 2
    .param p1, "context"    # Landroid/content/Context;
    .param p2, "attrs"    # Landroid/util/AttributeSet;

    .line 111
    sget v0, Landroidx/preference/R$attr;->switchPreferenceStyle:I

    const v1, 0x101036d

    invoke-static {p1, v0, v1}, Landroidx/core/content/res/TypedArrayUtils;->getAttr(Landroid/content/Context;II)I

    move-result v0

    invoke-direct {p0, p1, p2, v0}, Landroidx/preference/SwitchPreference;-><init>(Landroid/content/Context;Landroid/util/AttributeSet;I)V

    .line 114
    return-void
.end method

.method public constructor <init>(Landroid/content/Context;Landroid/util/AttributeSet;I)V
    .locals 1
    .param p1, "context"    # Landroid/content/Context;
    .param p2, "attrs"    # Landroid/util/AttributeSet;
    .param p3, "defStyleAttr"    # I

    .line 101
    const/4 v0, 0x0

    invoke-direct {p0, p1, p2, p3, v0}, Landroidx/preference/SwitchPreference;-><init>(Landroid/content/Context;Landroid/util/AttributeSet;II)V

    .line 102
    return-void
.end method

.method public constructor <init>(Landroid/content/Context;Landroid/util/AttributeSet;II)V
    .locals 4
    .param p1, "context"    # Landroid/content/Context;
    .param p2, "attrs"    # Landroid/util/AttributeSet;
    .param p3, "defStyleAttr"    # I
    .param p4, "defStyleRes"    # I

    .line 65
    invoke-direct {p0, p1, p2, p3, p4}, Landroidx/preference/TwoStatePreference;-><init>(Landroid/content/Context;Landroid/util/AttributeSet;II)V

    .line 45
    new-instance v0, Landroidx/preference/SwitchPreference$Listener;

    invoke-direct {v0, p0}, Landroidx/preference/SwitchPreference$Listener;-><init>(Landroidx/preference/SwitchPreference;)V

    iput-object v0, p0, Landroidx/preference/SwitchPreference;->mListener:Landroidx/preference/SwitchPreference$Listener;

    .line 67
    sget-object v0, Landroidx/preference/R$styleable;->SwitchPreference:[I

    invoke-virtual {p1, p2, v0, p3, p4}, Landroid/content/Context;->obtainStyledAttributes(Landroid/util/AttributeSet;[III)Landroid/content/res/TypedArray;

    move-result-object v0

    .line 70
    .local v0, "a":Landroid/content/res/TypedArray;
    sget v1, Landroidx/preference/R$styleable;->SwitchPreference_summaryOn:I

    sget v2, Landroidx/preference/R$styleable;->SwitchPreference_android_summaryOn:I

    invoke-static {v0, v1, v2}, Landroidx/core/content/res/TypedArrayUtils;->getString(Landroid/content/res/TypedArray;II)Ljava/lang/String;

    move-result-object v1

    invoke-virtual {p0, v1}, Landroidx/preference/SwitchPreference;->setSummaryOn(Ljava/lang/CharSequence;)V

    .line 73
    sget v1, Landroidx/preference/R$styleable;->SwitchPreference_summaryOff:I

    sget v2, Landroidx/preference/R$styleable;->SwitchPreference_android_summaryOff:I

    invoke-static {v0, v1, v2}, Landroidx/core/content/res/TypedArrayUtils;->getString(Landroid/content/res/TypedArray;II)Ljava/lang/String;

    move-result-object v1

    invoke-virtual {p0, v1}, Landroidx/preference/SwitchPreference;->setSummaryOff(Ljava/lang/CharSequence;)V

    .line 76
    sget v1, Landroidx/preference/R$styleable;->SwitchPreference_switchTextOn:I

    sget v2, Landroidx/preference/R$styleable;->SwitchPreference_android_switchTextOn:I

    invoke-static {v0, v1, v2}, Landroidx/core/content/res/TypedArrayUtils;->getString(Landroid/content/res/TypedArray;II)Ljava/lang/String;

    move-result-object v1

    invoke-virtual {p0, v1}, Landroidx/preference/SwitchPreference;->setSwitchTextOn(Ljava/lang/CharSequence;)V

    .line 80
    sget v1, Landroidx/preference/R$styleable;->SwitchPreference_switchTextOff:I

    sget v2, Landroidx/preference/R$styleable;->SwitchPreference_android_switchTextOff:I

    invoke-static {v0, v1, v2}, Landroidx/core/content/res/TypedArrayUtils;->getString(Landroid/content/res/TypedArray;II)Ljava/lang/String;

    move-result-object v1

    invoke-virtual {p0, v1}, Landroidx/preference/SwitchPreference;->setSwitchTextOff(Ljava/lang/CharSequence;)V

    .line 84
    sget v1, Landroidx/preference/R$styleable;->SwitchPreference_disableDependentsState:I

    sget v2, Landroidx/preference/R$styleable;->SwitchPreference_android_disableDependentsState:I

    const/4 v3, 0x0

    invoke-static {v0, v1, v2, v3}, Landroidx/core/content/res/TypedArrayUtils;->getBoolean(Landroid/content/res/TypedArray;IIZ)Z

    move-result v1

    invoke-virtual {p0, v1}, Landroidx/preference/SwitchPreference;->setDisableDependentsState(Z)V

    .line 88
    invoke-virtual {v0}, Landroid/content/res/TypedArray;->recycle()V

    .line 89
    return-void
.end method

.method private syncSwitchView(Landroid/view/View;)V
    .locals 2
    .param p1, "view"    # Landroid/view/View;

    .line 214
    instance-of v0, p1, Landroid/widget/Switch;

    if-eqz v0, :cond_0

    .line 215
    move-object v0, p1

    check-cast v0, Landroid/widget/Switch;

    .line 216
    .local v0, "switchView":Landroid/widget/Switch;
    const/4 v1, 0x0

    invoke-virtual {v0, v1}, Landroid/widget/Switch;->setOnCheckedChangeListener(Landroid/widget/CompoundButton$OnCheckedChangeListener;)V

    .line 218
    .end local v0    # "switchView":Landroid/widget/Switch;
    :cond_0
    instance-of v0, p1, Landroid/widget/Checkable;

    if-eqz v0, :cond_1

    .line 219
    move-object v0, p1

    check-cast v0, Landroid/widget/Checkable;

    iget-boolean v1, p0, Landroidx/preference/SwitchPreference;->mChecked:Z

    invoke-interface {v0, v1}, Landroid/widget/Checkable;->setChecked(Z)V

    .line 221
    :cond_1
    instance-of v0, p1, Landroid/widget/Switch;

    if-eqz v0, :cond_2

    .line 222
    move-object v0, p1

    check-cast v0, Landroid/widget/Switch;

    .line 223
    .restart local v0    # "switchView":Landroid/widget/Switch;
    iget-object v1, p0, Landroidx/preference/SwitchPreference;->mSwitchOn:Ljava/lang/CharSequence;

    invoke-virtual {v0, v1}, Landroid/widget/Switch;->setTextOn(Ljava/lang/CharSequence;)V

    .line 224
    iget-object v1, p0, Landroidx/preference/SwitchPreference;->mSwitchOff:Ljava/lang/CharSequence;

    invoke-virtual {v0, v1}, Landroid/widget/Switch;->setTextOff(Ljava/lang/CharSequence;)V

    .line 225
    iget-object v1, p0, Landroidx/preference/SwitchPreference;->mListener:Landroidx/preference/SwitchPreference$Listener;

    invoke-virtual {v0, v1}, Landroid/widget/Switch;->setOnCheckedChangeListener(Landroid/widget/CompoundButton$OnCheckedChangeListener;)V

    .line 227
    .end local v0    # "switchView":Landroid/widget/Switch;
    :cond_2
    return-void
.end method

.method private syncViewIfAccessibilityEnabled(Landroid/view/View;)V
    .locals 3
    .param p1, "view"    # Landroid/view/View;

    .line 200
    nop

    .line 201
    invoke-virtual {p0}, Landroidx/preference/SwitchPreference;->getContext()Landroid/content/Context;

    move-result-object v0

    const-string v1, "accessibility"

    invoke-virtual {v0, v1}, Landroid/content/Context;->getSystemService(Ljava/lang/String;)Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Landroid/view/accessibility/AccessibilityManager;

    .line 202
    .local v0, "accessibilityManager":Landroid/view/accessibility/AccessibilityManager;
    invoke-virtual {v0}, Landroid/view/accessibility/AccessibilityManager;->isEnabled()Z

    move-result v1

    if-nez v1, :cond_0

    .line 203
    return-void

    .line 206
    :cond_0
    const v1, 0x1020040

    invoke-virtual {p1, v1}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object v1

    .line 207
    .local v1, "switchView":Landroid/view/View;
    invoke-direct {p0, v1}, Landroidx/preference/SwitchPreference;->syncSwitchView(Landroid/view/View;)V

    .line 209
    const v2, 0x1020010

    invoke-virtual {p1, v2}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object v2

    .line 210
    .local v2, "summaryView":Landroid/view/View;
    invoke-virtual {p0, v2}, Landroidx/preference/SwitchPreference;->syncSummaryView(Landroid/view/View;)V

    .line 211
    return-void
.end method


# virtual methods
.method public getSwitchTextOff()Ljava/lang/CharSequence;
    .locals 1

    .line 176
    iget-object v0, p0, Landroidx/preference/SwitchPreference;->mSwitchOff:Ljava/lang/CharSequence;

    return-object v0
.end method

.method public getSwitchTextOn()Ljava/lang/CharSequence;
    .locals 1

    .line 159
    iget-object v0, p0, Landroidx/preference/SwitchPreference;->mSwitchOn:Ljava/lang/CharSequence;

    return-object v0
.end method

.method public onBindViewHolder(Landroidx/preference/PreferenceViewHolder;)V
    .locals 1
    .param p1, "holder"    # Landroidx/preference/PreferenceViewHolder;

    .line 127
    invoke-super {p0, p1}, Landroidx/preference/TwoStatePreference;->onBindViewHolder(Landroidx/preference/PreferenceViewHolder;)V

    .line 128
    const v0, 0x1020040

    invoke-virtual {p1, v0}, Landroidx/preference/PreferenceViewHolder;->findViewById(I)Landroid/view/View;

    move-result-object v0

    .line 129
    .local v0, "switchView":Landroid/view/View;
    invoke-direct {p0, v0}, Landroidx/preference/SwitchPreference;->syncSwitchView(Landroid/view/View;)V

    .line 130
    invoke-virtual {p0, p1}, Landroidx/preference/SwitchPreference;->syncSummaryView(Landroidx/preference/PreferenceViewHolder;)V

    .line 131
    return-void
.end method

.method protected performClick(Landroid/view/View;)V
    .locals 0
    .param p1, "view"    # Landroid/view/View;

    .line 195
    invoke-super {p0, p1}, Landroidx/preference/TwoStatePreference;->performClick(Landroid/view/View;)V

    .line 196
    invoke-direct {p0, p1}, Landroidx/preference/SwitchPreference;->syncViewIfAccessibilityEnabled(Landroid/view/View;)V

    .line 197
    return-void
.end method

.method public setSwitchTextOff(I)V
    .locals 1
    .param p1, "resId"    # I

    .line 186
    invoke-virtual {p0}, Landroidx/preference/SwitchPreference;->getContext()Landroid/content/Context;

    move-result-object v0

    invoke-virtual {v0, p1}, Landroid/content/Context;->getString(I)Ljava/lang/String;

    move-result-object v0

    invoke-virtual {p0, v0}, Landroidx/preference/SwitchPreference;->setSwitchTextOff(Ljava/lang/CharSequence;)V

    .line 187
    return-void
.end method

.method public setSwitchTextOff(Ljava/lang/CharSequence;)V
    .locals 0
    .param p1, "offText"    # Ljava/lang/CharSequence;

    .line 151
    iput-object p1, p0, Landroidx/preference/SwitchPreference;->mSwitchOff:Ljava/lang/CharSequence;

    .line 152
    invoke-virtual {p0}, Landroidx/preference/SwitchPreference;->notifyChanged()V

    .line 153
    return-void
.end method

.method public setSwitchTextOn(I)V
    .locals 1
    .param p1, "resId"    # I

    .line 169
    invoke-virtual {p0}, Landroidx/preference/SwitchPreference;->getContext()Landroid/content/Context;

    move-result-object v0

    invoke-virtual {v0, p1}, Landroid/content/Context;->getString(I)Ljava/lang/String;

    move-result-object v0

    invoke-virtual {p0, v0}, Landroidx/preference/SwitchPreference;->setSwitchTextOn(Ljava/lang/CharSequence;)V

    .line 170
    return-void
.end method

.method public setSwitchTextOn(Ljava/lang/CharSequence;)V
    .locals 0
    .param p1, "onText"    # Ljava/lang/CharSequence;

    .line 140
    iput-object p1, p0, Landroidx/preference/SwitchPreference;->mSwitchOn:Ljava/lang/CharSequence;

    .line 141
    invoke-virtual {p0}, Landroidx/preference/SwitchPreference;->notifyChanged()V

    .line 142
    return-void
.end method
