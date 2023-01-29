.class Landroidx/preference/PreferenceRecyclerViewAccessibilityDelegate$1;
.super Landroidx/core/view/AccessibilityDelegateCompat;
.source "PreferenceRecyclerViewAccessibilityDelegate.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Landroidx/preference/PreferenceRecyclerViewAccessibilityDelegate;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Landroidx/preference/PreferenceRecyclerViewAccessibilityDelegate;


# direct methods
.method constructor <init>(Landroidx/preference/PreferenceRecyclerViewAccessibilityDelegate;)V
    .locals 0
    .param p1, "this$0"    # Landroidx/preference/PreferenceRecyclerViewAccessibilityDelegate;

    .line 61
    iput-object p1, p0, Landroidx/preference/PreferenceRecyclerViewAccessibilityDelegate$1;->this$0:Landroidx/preference/PreferenceRecyclerViewAccessibilityDelegate;

    invoke-direct {p0}, Landroidx/core/view/AccessibilityDelegateCompat;-><init>()V

    return-void
.end method


# virtual methods
.method public onInitializeAccessibilityNodeInfo(Landroid/view/View;Landroidx/core/view/accessibility/AccessibilityNodeInfoCompat;)V
    .locals 4
    .param p1, "host"    # Landroid/view/View;
    .param p2, "info"    # Landroidx/core/view/accessibility/AccessibilityNodeInfoCompat;

    .line 64
    iget-object v0, p0, Landroidx/preference/PreferenceRecyclerViewAccessibilityDelegate$1;->this$0:Landroidx/preference/PreferenceRecyclerViewAccessibilityDelegate;

    iget-object v0, v0, Landroidx/preference/PreferenceRecyclerViewAccessibilityDelegate;->mDefaultItemDelegate:Landroidx/core/view/AccessibilityDelegateCompat;

    invoke-virtual {v0, p1, p2}, Landroidx/core/view/AccessibilityDelegateCompat;->onInitializeAccessibilityNodeInfo(Landroid/view/View;Landroidx/core/view/accessibility/AccessibilityNodeInfoCompat;)V

    .line 65
    iget-object v0, p0, Landroidx/preference/PreferenceRecyclerViewAccessibilityDelegate$1;->this$0:Landroidx/preference/PreferenceRecyclerViewAccessibilityDelegate;

    iget-object v0, v0, Landroidx/preference/PreferenceRecyclerViewAccessibilityDelegate;->mRecyclerView:Landroidx/recyclerview/widget/RecyclerView;

    invoke-virtual {v0, p1}, Landroidx/recyclerview/widget/RecyclerView;->getChildAdapterPosition(Landroid/view/View;)I

    move-result v0

    .line 67
    .local v0, "position":I
    iget-object v1, p0, Landroidx/preference/PreferenceRecyclerViewAccessibilityDelegate$1;->this$0:Landroidx/preference/PreferenceRecyclerViewAccessibilityDelegate;

    iget-object v1, v1, Landroidx/preference/PreferenceRecyclerViewAccessibilityDelegate;->mRecyclerView:Landroidx/recyclerview/widget/RecyclerView;

    invoke-virtual {v1}, Landroidx/recyclerview/widget/RecyclerView;->getAdapter()Landroidx/recyclerview/widget/RecyclerView$Adapter;

    move-result-object v1

    .line 68
    .local v1, "adapter":Landroidx/recyclerview/widget/RecyclerView$Adapter;
    instance-of v2, v1, Landroidx/preference/PreferenceGroupAdapter;

    if-nez v2, :cond_0

    .line 69
    return-void

    .line 72
    :cond_0
    move-object v2, v1

    check-cast v2, Landroidx/preference/PreferenceGroupAdapter;

    .line 73
    .local v2, "preferenceGroupAdapter":Landroidx/preference/PreferenceGroupAdapter;
    invoke-virtual {v2, v0}, Landroidx/preference/PreferenceGroupAdapter;->getItem(I)Landroidx/preference/Preference;

    move-result-object v3

    .line 74
    .local v3, "preference":Landroidx/preference/Preference;
    if-nez v3, :cond_1

    .line 75
    return-void

    .line 78
    :cond_1
    invoke-virtual {v3, p2}, Landroidx/preference/Preference;->onInitializeAccessibilityNodeInfo(Landroidx/core/view/accessibility/AccessibilityNodeInfoCompat;)V

    .line 79
    return-void
.end method

.method public performAccessibilityAction(Landroid/view/View;ILandroid/os/Bundle;)Z
    .locals 1
    .param p1, "host"    # Landroid/view/View;
    .param p2, "action"    # I
    .param p3, "args"    # Landroid/os/Bundle;

    .line 84
    iget-object v0, p0, Landroidx/preference/PreferenceRecyclerViewAccessibilityDelegate$1;->this$0:Landroidx/preference/PreferenceRecyclerViewAccessibilityDelegate;

    iget-object v0, v0, Landroidx/preference/PreferenceRecyclerViewAccessibilityDelegate;->mDefaultItemDelegate:Landroidx/core/view/AccessibilityDelegateCompat;

    invoke-virtual {v0, p1, p2, p3}, Landroidx/core/view/AccessibilityDelegateCompat;->performAccessibilityAction(Landroid/view/View;ILandroid/os/Bundle;)Z

    move-result v0

    return v0
.end method
