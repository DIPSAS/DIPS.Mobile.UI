.class final Landroidx/preference/ExpandButton;
.super Landroidx/preference/Preference;
.source "ExpandButton.java"


# instance fields
.field private mId:J


# direct methods
.method constructor <init>(Landroid/content/Context;Ljava/util/List;J)V
    .locals 2
    .param p1, "context"    # Landroid/content/Context;
    .param p3, "parentId"    # J
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Landroid/content/Context;",
            "Ljava/util/List<",
            "Landroidx/preference/Preference;",
            ">;J)V"
        }
    .end annotation

    .line 35
    .local p2, "collapsedPreferences":Ljava/util/List;, "Ljava/util/List<Landroidx/preference/Preference;>;"
    invoke-direct {p0, p1}, Landroidx/preference/Preference;-><init>(Landroid/content/Context;)V

    .line 36
    invoke-direct {p0}, Landroidx/preference/ExpandButton;->initLayout()V

    .line 37
    invoke-direct {p0, p2}, Landroidx/preference/ExpandButton;->setSummary(Ljava/util/List;)V

    .line 41
    const-wide/32 v0, 0xf4240

    add-long/2addr v0, p3

    iput-wide v0, p0, Landroidx/preference/ExpandButton;->mId:J

    .line 42
    return-void
.end method

.method private initLayout()V
    .locals 1

    .line 45
    sget v0, Landroidx/preference/R$layout;->expand_button:I

    invoke-virtual {p0, v0}, Landroidx/preference/ExpandButton;->setLayoutResource(I)V

    .line 46
    sget v0, Landroidx/preference/R$drawable;->ic_arrow_down_24dp:I

    invoke-virtual {p0, v0}, Landroidx/preference/ExpandButton;->setIcon(I)V

    .line 47
    sget v0, Landroidx/preference/R$string;->expand_button_title:I

    invoke-virtual {p0, v0}, Landroidx/preference/ExpandButton;->setTitle(I)V

    .line 49
    const/16 v0, 0x3e7

    invoke-virtual {p0, v0}, Landroidx/preference/ExpandButton;->setOrder(I)V

    .line 50
    return-void
.end method

.method private setSummary(Ljava/util/List;)V
    .locals 9
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/List<",
            "Landroidx/preference/Preference;",
            ">;)V"
        }
    .end annotation

    .line 58
    .local p1, "collapsedPreferences":Ljava/util/List;, "Ljava/util/List<Landroidx/preference/Preference;>;"
    const/4 v0, 0x0

    .line 59
    .local v0, "summary":Ljava/lang/CharSequence;
    new-instance v1, Ljava/util/ArrayList;

    invoke-direct {v1}, Ljava/util/ArrayList;-><init>()V

    .line 61
    .local v1, "parents":Ljava/util/List;, "Ljava/util/List<Landroidx/preference/PreferenceGroup;>;"
    invoke-interface {p1}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v2

    :cond_0
    :goto_0
    invoke-interface {v2}, Ljava/util/Iterator;->hasNext()Z

    move-result v3

    if-eqz v3, :cond_5

    invoke-interface {v2}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v3

    check-cast v3, Landroidx/preference/Preference;

    .line 62
    .local v3, "preference":Landroidx/preference/Preference;
    invoke-virtual {v3}, Landroidx/preference/Preference;->getTitle()Ljava/lang/CharSequence;

    move-result-object v4

    .line 63
    .local v4, "title":Ljava/lang/CharSequence;
    instance-of v5, v3, Landroidx/preference/PreferenceGroup;

    if-eqz v5, :cond_1

    invoke-static {v4}, Landroid/text/TextUtils;->isEmpty(Ljava/lang/CharSequence;)Z

    move-result v5

    if-nez v5, :cond_1

    .line 64
    move-object v5, v3

    check-cast v5, Landroidx/preference/PreferenceGroup;

    invoke-interface {v1, v5}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    .line 66
    :cond_1
    invoke-virtual {v3}, Landroidx/preference/Preference;->getParent()Landroidx/preference/PreferenceGroup;

    move-result-object v5

    invoke-interface {v1, v5}, Ljava/util/List;->contains(Ljava/lang/Object;)Z

    move-result v5

    if-eqz v5, :cond_2

    .line 67
    instance-of v5, v3, Landroidx/preference/PreferenceGroup;

    if-eqz v5, :cond_0

    .line 68
    move-object v5, v3

    check-cast v5, Landroidx/preference/PreferenceGroup;

    invoke-interface {v1, v5}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    goto :goto_0

    .line 72
    :cond_2
    invoke-static {v4}, Landroid/text/TextUtils;->isEmpty(Ljava/lang/CharSequence;)Z

    move-result v5

    if-nez v5, :cond_4

    .line 73
    if-nez v0, :cond_3

    .line 74
    move-object v0, v4

    goto :goto_1

    .line 76
    :cond_3
    invoke-virtual {p0}, Landroidx/preference/ExpandButton;->getContext()Landroid/content/Context;

    move-result-object v5

    sget v6, Landroidx/preference/R$string;->summary_collapsed_preference_list:I

    const/4 v7, 0x2

    new-array v7, v7, [Ljava/lang/Object;

    const/4 v8, 0x0

    aput-object v0, v7, v8

    const/4 v8, 0x1

    aput-object v4, v7, v8

    invoke-virtual {v5, v6, v7}, Landroid/content/Context;->getString(I[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v0

    .line 80
    .end local v3    # "preference":Landroidx/preference/Preference;
    .end local v4    # "title":Ljava/lang/CharSequence;
    :cond_4
    :goto_1
    goto :goto_0

    .line 81
    :cond_5
    invoke-virtual {p0, v0}, Landroidx/preference/ExpandButton;->setSummary(Ljava/lang/CharSequence;)V

    .line 82
    return-void
.end method


# virtual methods
.method getId()J
    .locals 2

    .line 92
    iget-wide v0, p0, Landroidx/preference/ExpandButton;->mId:J

    return-wide v0
.end method

.method public onBindViewHolder(Landroidx/preference/PreferenceViewHolder;)V
    .locals 1
    .param p1, "holder"    # Landroidx/preference/PreferenceViewHolder;

    .line 86
    invoke-super {p0, p1}, Landroidx/preference/Preference;->onBindViewHolder(Landroidx/preference/PreferenceViewHolder;)V

    .line 87
    const/4 v0, 0x0

    invoke-virtual {p1, v0}, Landroidx/preference/PreferenceViewHolder;->setDividerAllowedAbove(Z)V

    .line 88
    return-void
.end method
