.class public Landroidx/preference/PreferenceManager$SimplePreferenceComparisonCallback;
.super Landroidx/preference/PreferenceManager$PreferenceComparisonCallback;
.source "PreferenceManager.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Landroidx/preference/PreferenceManager;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x9
    name = "SimplePreferenceComparisonCallback"
.end annotation


# direct methods
.method public constructor <init>()V
    .locals 0

    .line 647
    invoke-direct {p0}, Landroidx/preference/PreferenceManager$PreferenceComparisonCallback;-><init>()V

    return-void
.end method


# virtual methods
.method public arePreferenceContentsTheSame(Landroidx/preference/Preference;Landroidx/preference/Preference;)Z
    .locals 5
    .param p1, "p1"    # Landroidx/preference/Preference;
    .param p2, "p2"    # Landroidx/preference/Preference;

    .line 672
    invoke-virtual {p1}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    move-result-object v0

    invoke-virtual {p2}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    move-result-object v1

    const/4 v2, 0x0

    if-eq v0, v1, :cond_0

    .line 673
    return v2

    .line 675
    :cond_0
    if-ne p1, p2, :cond_1

    invoke-virtual {p1}, Landroidx/preference/Preference;->wasDetached()Z

    move-result v0

    if-eqz v0, :cond_1

    .line 678
    return v2

    .line 680
    :cond_1
    invoke-virtual {p1}, Landroidx/preference/Preference;->getTitle()Ljava/lang/CharSequence;

    move-result-object v0

    invoke-virtual {p2}, Landroidx/preference/Preference;->getTitle()Ljava/lang/CharSequence;

    move-result-object v1

    invoke-static {v0, v1}, Landroid/text/TextUtils;->equals(Ljava/lang/CharSequence;Ljava/lang/CharSequence;)Z

    move-result v0

    if-nez v0, :cond_2

    .line 681
    return v2

    .line 683
    :cond_2
    invoke-virtual {p1}, Landroidx/preference/Preference;->getSummary()Ljava/lang/CharSequence;

    move-result-object v0

    invoke-virtual {p2}, Landroidx/preference/Preference;->getSummary()Ljava/lang/CharSequence;

    move-result-object v1

    invoke-static {v0, v1}, Landroid/text/TextUtils;->equals(Ljava/lang/CharSequence;Ljava/lang/CharSequence;)Z

    move-result v0

    if-nez v0, :cond_3

    .line 684
    return v2

    .line 686
    :cond_3
    invoke-virtual {p1}, Landroidx/preference/Preference;->getIcon()Landroid/graphics/drawable/Drawable;

    move-result-object v0

    .line 687
    .local v0, "p1Icon":Landroid/graphics/drawable/Drawable;
    invoke-virtual {p2}, Landroidx/preference/Preference;->getIcon()Landroid/graphics/drawable/Drawable;

    move-result-object v1

    .line 688
    .local v1, "p2Icon":Landroid/graphics/drawable/Drawable;
    if-eq v0, v1, :cond_5

    if-eqz v0, :cond_4

    invoke-virtual {v0, v1}, Ljava/lang/Object;->equals(Ljava/lang/Object;)Z

    move-result v3

    if-nez v3, :cond_5

    .line 689
    :cond_4
    return v2

    .line 691
    :cond_5
    invoke-virtual {p1}, Landroidx/preference/Preference;->isEnabled()Z

    move-result v3

    invoke-virtual {p2}, Landroidx/preference/Preference;->isEnabled()Z

    move-result v4

    if-eq v3, v4, :cond_6

    .line 692
    return v2

    .line 694
    :cond_6
    invoke-virtual {p1}, Landroidx/preference/Preference;->isSelectable()Z

    move-result v3

    invoke-virtual {p2}, Landroidx/preference/Preference;->isSelectable()Z

    move-result v4

    if-eq v3, v4, :cond_7

    .line 695
    return v2

    .line 697
    :cond_7
    instance-of v3, p1, Landroidx/preference/TwoStatePreference;

    if-eqz v3, :cond_8

    .line 698
    move-object v3, p1

    check-cast v3, Landroidx/preference/TwoStatePreference;

    invoke-virtual {v3}, Landroidx/preference/TwoStatePreference;->isChecked()Z

    move-result v3

    move-object v4, p2

    check-cast v4, Landroidx/preference/TwoStatePreference;

    .line 699
    invoke-virtual {v4}, Landroidx/preference/TwoStatePreference;->isChecked()Z

    move-result v4

    if-eq v3, v4, :cond_8

    .line 700
    return v2

    .line 703
    :cond_8
    instance-of v3, p1, Landroidx/preference/DropDownPreference;

    if-eqz v3, :cond_9

    if-eq p1, p2, :cond_9

    .line 705
    return v2

    .line 708
    :cond_9
    const/4 v2, 0x1

    return v2
.end method

.method public arePreferenceItemsTheSame(Landroidx/preference/Preference;Landroidx/preference/Preference;)Z
    .locals 5
    .param p1, "p1"    # Landroidx/preference/Preference;
    .param p2, "p2"    # Landroidx/preference/Preference;

    .line 658
    invoke-virtual {p1}, Landroidx/preference/Preference;->getId()J

    move-result-wide v0

    invoke-virtual {p2}, Landroidx/preference/Preference;->getId()J

    move-result-wide v2

    cmp-long v4, v0, v2

    if-nez v4, :cond_0

    const/4 v0, 0x1

    goto :goto_0

    :cond_0
    const/4 v0, 0x0

    :goto_0
    return v0
.end method
