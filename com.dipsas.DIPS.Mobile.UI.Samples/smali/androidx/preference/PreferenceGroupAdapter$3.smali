.class Landroidx/preference/PreferenceGroupAdapter$3;
.super Ljava/lang/Object;
.source "PreferenceGroupAdapter.java"

# interfaces
.implements Landroidx/preference/Preference$OnPreferenceClickListener;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Landroidx/preference/PreferenceGroupAdapter;->createExpandButton(Landroidx/preference/PreferenceGroup;Ljava/util/List;)Landroidx/preference/ExpandButton;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Landroidx/preference/PreferenceGroupAdapter;

.field final synthetic val$group:Landroidx/preference/PreferenceGroup;


# direct methods
.method constructor <init>(Landroidx/preference/PreferenceGroupAdapter;Landroidx/preference/PreferenceGroup;)V
    .locals 0
    .param p1, "this$0"    # Landroidx/preference/PreferenceGroupAdapter;

    .line 293
    iput-object p1, p0, Landroidx/preference/PreferenceGroupAdapter$3;->this$0:Landroidx/preference/PreferenceGroupAdapter;

    iput-object p2, p0, Landroidx/preference/PreferenceGroupAdapter$3;->val$group:Landroidx/preference/PreferenceGroup;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public onPreferenceClick(Landroidx/preference/Preference;)Z
    .locals 2
    .param p1, "preference"    # Landroidx/preference/Preference;

    .line 296
    iget-object v0, p0, Landroidx/preference/PreferenceGroupAdapter$3;->val$group:Landroidx/preference/PreferenceGroup;

    const v1, 0x7fffffff

    invoke-virtual {v0, v1}, Landroidx/preference/PreferenceGroup;->setInitialExpandedChildrenCount(I)V

    .line 297
    iget-object v0, p0, Landroidx/preference/PreferenceGroupAdapter$3;->this$0:Landroidx/preference/PreferenceGroupAdapter;

    invoke-virtual {v0, p1}, Landroidx/preference/PreferenceGroupAdapter;->onPreferenceHierarchyChange(Landroidx/preference/Preference;)V

    .line 298
    iget-object v0, p0, Landroidx/preference/PreferenceGroupAdapter$3;->val$group:Landroidx/preference/PreferenceGroup;

    .line 299
    invoke-virtual {v0}, Landroidx/preference/PreferenceGroup;->getOnExpandButtonClickListener()Landroidx/preference/PreferenceGroup$OnExpandButtonClickListener;

    move-result-object v0

    .line 300
    .local v0, "listener":Landroidx/preference/PreferenceGroup$OnExpandButtonClickListener;
    if-eqz v0, :cond_0

    .line 301
    invoke-interface {v0}, Landroidx/preference/PreferenceGroup$OnExpandButtonClickListener;->onExpandButtonClick()V

    .line 303
    :cond_0
    const/4 v1, 0x1

    return v1
.end method
