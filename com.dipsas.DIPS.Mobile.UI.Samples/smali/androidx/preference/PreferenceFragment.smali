.class public abstract Landroidx/preference/PreferenceFragment;
.super Landroid/app/Fragment;
.source "PreferenceFragment.java"

# interfaces
.implements Landroidx/preference/PreferenceManager$OnPreferenceTreeClickListener;
.implements Landroidx/preference/PreferenceManager$OnDisplayPreferenceDialogListener;
.implements Landroidx/preference/PreferenceManager$OnNavigateToScreenListener;
.implements Landroidx/preference/DialogPreference$TargetFragment;


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Landroidx/preference/PreferenceFragment$DividerDecoration;,
        Landroidx/preference/PreferenceFragment$ScrollToPreferenceObserver;,
        Landroidx/preference/PreferenceFragment$OnPreferenceDisplayDialogCallback;,
        Landroidx/preference/PreferenceFragment$OnPreferenceStartScreenCallback;,
        Landroidx/preference/PreferenceFragment$OnPreferenceStartFragmentCallback;
    }
.end annotation

.annotation runtime Ljava/lang/Deprecated;
.end annotation


# static fields
.field public static final ARG_PREFERENCE_ROOT:Ljava/lang/String; = "androidx.preference.PreferenceFragmentCompat.PREFERENCE_ROOT"
    .annotation runtime Ljava/lang/Deprecated;
    .end annotation
.end field

.field private static final DIALOG_FRAGMENT_TAG:Ljava/lang/String; = "androidx.preference.PreferenceFragment.DIALOG"

.field private static final MSG_BIND_PREFERENCES:I = 0x1

.field private static final PREFERENCES_TAG:Ljava/lang/String; = "android:preferences"


# instance fields
.field private final mDividerDecoration:Landroidx/preference/PreferenceFragment$DividerDecoration;

.field private final mHandler:Landroid/os/Handler;

.field private mHavePrefs:Z

.field private mInitDone:Z

.field private mLayoutResId:I

.field mList:Landroidx/recyclerview/widget/RecyclerView;

.field private mPreferenceManager:Landroidx/preference/PreferenceManager;

.field private final mRequestFocus:Ljava/lang/Runnable;

.field private mSelectPreferenceRunnable:Ljava/lang/Runnable;

.field private mStyledContext:Landroid/content/Context;


# direct methods
.method public constructor <init>()V
    .locals 1

    .line 96
    invoke-direct {p0}, Landroid/app/Fragment;-><init>()V

    .line 119
    new-instance v0, Landroidx/preference/PreferenceFragment$DividerDecoration;

    invoke-direct {v0, p0}, Landroidx/preference/PreferenceFragment$DividerDecoration;-><init>(Landroidx/preference/PreferenceFragment;)V

    iput-object v0, p0, Landroidx/preference/PreferenceFragment;->mDividerDecoration:Landroidx/preference/PreferenceFragment$DividerDecoration;

    .line 125
    sget v0, Landroidx/preference/R$layout;->preference_list_fragment:I

    iput v0, p0, Landroidx/preference/PreferenceFragment;->mLayoutResId:I

    .line 128
    new-instance v0, Landroidx/preference/PreferenceFragment$1;

    invoke-direct {v0, p0}, Landroidx/preference/PreferenceFragment$1;-><init>(Landroidx/preference/PreferenceFragment;)V

    iput-object v0, p0, Landroidx/preference/PreferenceFragment;->mHandler:Landroid/os/Handler;

    .line 139
    new-instance v0, Landroidx/preference/PreferenceFragment$2;

    invoke-direct {v0, p0}, Landroidx/preference/PreferenceFragment$2;-><init>(Landroidx/preference/PreferenceFragment;)V

    iput-object v0, p0, Landroidx/preference/PreferenceFragment;->mRequestFocus:Ljava/lang/Runnable;

    return-void
.end method

.method private postBindPreferences()V
    .locals 2

    .line 495
    iget-object v0, p0, Landroidx/preference/PreferenceFragment;->mHandler:Landroid/os/Handler;

    const/4 v1, 0x1

    invoke-virtual {v0, v1}, Landroid/os/Handler;->hasMessages(I)Z

    move-result v0

    if-eqz v0, :cond_0

    return-void

    .line 496
    :cond_0
    iget-object v0, p0, Landroidx/preference/PreferenceFragment;->mHandler:Landroid/os/Handler;

    invoke-virtual {v0, v1}, Landroid/os/Handler;->obtainMessage(I)Landroid/os/Message;

    move-result-object v0

    invoke-virtual {v0}, Landroid/os/Message;->sendToTarget()V

    .line 497
    return-void
.end method

.method private requirePreferenceManager()V
    .locals 2

    .line 489
    iget-object v0, p0, Landroidx/preference/PreferenceFragment;->mPreferenceManager:Landroidx/preference/PreferenceManager;

    if-eqz v0, :cond_0

    .line 492
    return-void

    .line 490
    :cond_0
    new-instance v0, Ljava/lang/RuntimeException;

    const-string v1, "This should be called after super.onCreate."

    invoke-direct {v0, v1}, Ljava/lang/RuntimeException;-><init>(Ljava/lang/String;)V

    throw v0
.end method

.method private scrollToPreferenceInternal(Landroidx/preference/Preference;Ljava/lang/String;)V
    .locals 2
    .param p1, "preference"    # Landroidx/preference/Preference;
    .param p2, "key"    # Ljava/lang/String;

    .line 670
    new-instance v0, Landroidx/preference/PreferenceFragment$3;

    invoke-direct {v0, p0, p1, p2}, Landroidx/preference/PreferenceFragment$3;-><init>(Landroidx/preference/PreferenceFragment;Landroidx/preference/Preference;Ljava/lang/String;)V

    .line 700
    .local v0, "r":Ljava/lang/Runnable;
    iget-object v1, p0, Landroidx/preference/PreferenceFragment;->mList:Landroidx/recyclerview/widget/RecyclerView;

    if-nez v1, :cond_0

    .line 701
    iput-object v0, p0, Landroidx/preference/PreferenceFragment;->mSelectPreferenceRunnable:Ljava/lang/Runnable;

    goto :goto_0

    .line 703
    :cond_0
    invoke-interface {v0}, Ljava/lang/Runnable;->run()V

    .line 705
    :goto_0
    return-void
.end method

.method private unbindPreferences()V
    .locals 1

    .line 509
    invoke-virtual {p0}, Landroidx/preference/PreferenceFragment;->getPreferenceScreen()Landroidx/preference/PreferenceScreen;

    move-result-object v0

    .line 510
    .local v0, "preferenceScreen":Landroidx/preference/PreferenceScreen;
    if-eqz v0, :cond_0

    .line 511
    invoke-virtual {v0}, Landroidx/preference/PreferenceScreen;->onDetached()V

    .line 513
    :cond_0
    invoke-virtual {p0}, Landroidx/preference/PreferenceFragment;->onUnbindPreferences()V

    .line 514
    return-void
.end method


# virtual methods
.method public addPreferencesFromResource(I)V
    .locals 3
    .param p1, "preferencesResId"    # I
    .annotation runtime Ljava/lang/Deprecated;
    .end annotation

    .line 384
    invoke-direct {p0}, Landroidx/preference/PreferenceFragment;->requirePreferenceManager()V

    .line 386
    iget-object v0, p0, Landroidx/preference/PreferenceFragment;->mPreferenceManager:Landroidx/preference/PreferenceManager;

    iget-object v1, p0, Landroidx/preference/PreferenceFragment;->mStyledContext:Landroid/content/Context;

    .line 387
    invoke-virtual {p0}, Landroidx/preference/PreferenceFragment;->getPreferenceScreen()Landroidx/preference/PreferenceScreen;

    move-result-object v2

    .line 386
    invoke-virtual {v0, v1, p1, v2}, Landroidx/preference/PreferenceManager;->inflateFromResource(Landroid/content/Context;ILandroidx/preference/PreferenceScreen;)Landroidx/preference/PreferenceScreen;

    move-result-object v0

    invoke-virtual {p0, v0}, Landroidx/preference/PreferenceFragment;->setPreferenceScreen(Landroidx/preference/PreferenceScreen;)V

    .line 388
    return-void
.end method

.method bindPreferences()V
    .locals 3

    .line 500
    invoke-virtual {p0}, Landroidx/preference/PreferenceFragment;->getPreferenceScreen()Landroidx/preference/PreferenceScreen;

    move-result-object v0

    .line 501
    .local v0, "preferenceScreen":Landroidx/preference/PreferenceScreen;
    if-eqz v0, :cond_0

    .line 502
    invoke-virtual {p0}, Landroidx/preference/PreferenceFragment;->getListView()Landroidx/recyclerview/widget/RecyclerView;

    move-result-object v1

    invoke-virtual {p0, v0}, Landroidx/preference/PreferenceFragment;->onCreateAdapter(Landroidx/preference/PreferenceScreen;)Landroidx/recyclerview/widget/RecyclerView$Adapter;

    move-result-object v2

    invoke-virtual {v1, v2}, Landroidx/recyclerview/widget/RecyclerView;->setAdapter(Landroidx/recyclerview/widget/RecyclerView$Adapter;)V

    .line 503
    invoke-virtual {v0}, Landroidx/preference/PreferenceScreen;->onAttached()V

    .line 505
    :cond_0
    invoke-virtual {p0}, Landroidx/preference/PreferenceFragment;->onBindPreferences()V

    .line 506
    return-void
.end method

.method public findPreference(Ljava/lang/CharSequence;)Landroidx/preference/Preference;
    .locals 1
    .param p1, "key"    # Ljava/lang/CharSequence;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "<T:",
            "Landroidx/preference/Preference;",
            ">(",
            "Ljava/lang/CharSequence;",
            ")TT;"
        }
    .end annotation

    .annotation runtime Ljava/lang/Deprecated;
    .end annotation

    .line 482
    iget-object v0, p0, Landroidx/preference/PreferenceFragment;->mPreferenceManager:Landroidx/preference/PreferenceManager;

    if-nez v0, :cond_0

    .line 483
    const/4 v0, 0x0

    return-object v0

    .line 485
    :cond_0
    invoke-virtual {v0, p1}, Landroidx/preference/PreferenceManager;->findPreference(Ljava/lang/CharSequence;)Landroidx/preference/Preference;

    move-result-object v0

    return-object v0
.end method

.method public getCallbackFragment()Landroid/app/Fragment;
    .locals 1

    .line 650
    const/4 v0, 0x0

    return-object v0
.end method

.method public final getListView()Landroidx/recyclerview/widget/RecyclerView;
    .locals 1
    .annotation runtime Ljava/lang/Deprecated;
    .end annotation

    .line 529
    iget-object v0, p0, Landroidx/preference/PreferenceFragment;->mList:Landroidx/recyclerview/widget/RecyclerView;

    return-object v0
.end method

.method public getPreferenceManager()Landroidx/preference/PreferenceManager;
    .locals 1
    .annotation runtime Ljava/lang/Deprecated;
    .end annotation

    .line 341
    iget-object v0, p0, Landroidx/preference/PreferenceFragment;->mPreferenceManager:Landroidx/preference/PreferenceManager;

    return-object v0
.end method

.method public getPreferenceScreen()Landroidx/preference/PreferenceScreen;
    .locals 1
    .annotation runtime Ljava/lang/Deprecated;
    .end annotation

    .line 371
    iget-object v0, p0, Landroidx/preference/PreferenceFragment;->mPreferenceManager:Landroidx/preference/PreferenceManager;

    invoke-virtual {v0}, Landroidx/preference/PreferenceManager;->getPreferenceScreen()Landroidx/preference/PreferenceScreen;

    move-result-object v0

    return-object v0
.end method

.method protected onBindPreferences()V
    .locals 0

    .line 518
    return-void
.end method

.method public onCreate(Landroid/os/Bundle;)V
    .locals 5
    .param p1, "savedInstanceState"    # Landroid/os/Bundle;

    .line 148
    invoke-super {p0, p1}, Landroid/app/Fragment;->onCreate(Landroid/os/Bundle;)V

    .line 149
    new-instance v0, Landroid/util/TypedValue;

    invoke-direct {v0}, Landroid/util/TypedValue;-><init>()V

    .line 150
    .local v0, "tv":Landroid/util/TypedValue;
    invoke-virtual {p0}, Landroidx/preference/PreferenceFragment;->getActivity()Landroid/app/Activity;

    move-result-object v1

    invoke-virtual {v1}, Landroid/app/Activity;->getTheme()Landroid/content/res/Resources$Theme;

    move-result-object v1

    sget v2, Landroidx/preference/R$attr;->preferenceTheme:I

    const/4 v3, 0x1

    invoke-virtual {v1, v2, v0, v3}, Landroid/content/res/Resources$Theme;->resolveAttribute(ILandroid/util/TypedValue;Z)Z

    .line 151
    iget v1, v0, Landroid/util/TypedValue;->resourceId:I

    .line 152
    .local v1, "theme":I
    if-nez v1, :cond_0

    .line 154
    sget v1, Landroidx/preference/R$style;->PreferenceThemeOverlay:I

    .line 156
    :cond_0
    new-instance v2, Landroid/view/ContextThemeWrapper;

    invoke-virtual {p0}, Landroidx/preference/PreferenceFragment;->getActivity()Landroid/app/Activity;

    move-result-object v3

    invoke-direct {v2, v3, v1}, Landroid/view/ContextThemeWrapper;-><init>(Landroid/content/Context;I)V

    iput-object v2, p0, Landroidx/preference/PreferenceFragment;->mStyledContext:Landroid/content/Context;

    .line 158
    new-instance v3, Landroidx/preference/PreferenceManager;

    invoke-direct {v3, v2}, Landroidx/preference/PreferenceManager;-><init>(Landroid/content/Context;)V

    iput-object v3, p0, Landroidx/preference/PreferenceFragment;->mPreferenceManager:Landroidx/preference/PreferenceManager;

    .line 159
    invoke-virtual {v3, p0}, Landroidx/preference/PreferenceManager;->setOnNavigateToScreenListener(Landroidx/preference/PreferenceManager$OnNavigateToScreenListener;)V

    .line 160
    invoke-virtual {p0}, Landroidx/preference/PreferenceFragment;->getArguments()Landroid/os/Bundle;

    move-result-object v2

    .line 162
    .local v2, "args":Landroid/os/Bundle;
    if-eqz v2, :cond_1

    .line 163
    invoke-virtual {p0}, Landroidx/preference/PreferenceFragment;->getArguments()Landroid/os/Bundle;

    move-result-object v3

    const-string v4, "androidx.preference.PreferenceFragmentCompat.PREFERENCE_ROOT"

    invoke-virtual {v3, v4}, Landroid/os/Bundle;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v3

    .local v3, "rootKey":Ljava/lang/String;
    goto :goto_0

    .line 165
    .end local v3    # "rootKey":Ljava/lang/String;
    :cond_1
    const/4 v3, 0x0

    .line 167
    .restart local v3    # "rootKey":Ljava/lang/String;
    :goto_0
    invoke-virtual {p0, p1, v3}, Landroidx/preference/PreferenceFragment;->onCreatePreferences(Landroid/os/Bundle;Ljava/lang/String;)V

    .line 168
    return-void
.end method

.method protected onCreateAdapter(Landroidx/preference/PreferenceScreen;)Landroidx/recyclerview/widget/RecyclerView$Adapter;
    .locals 1
    .param p1, "preferenceScreen"    # Landroidx/preference/PreferenceScreen;
    .annotation runtime Ljava/lang/Deprecated;
    .end annotation

    .line 592
    new-instance v0, Landroidx/preference/PreferenceGroupAdapter;

    invoke-direct {v0, p1}, Landroidx/preference/PreferenceGroupAdapter;-><init>(Landroidx/preference/PreferenceGroup;)V

    return-object v0
.end method

.method public onCreateLayoutManager()Landroidx/recyclerview/widget/RecyclerView$LayoutManager;
    .locals 2
    .annotation runtime Ljava/lang/Deprecated;
    .end annotation

    .line 579
    new-instance v0, Landroidx/recyclerview/widget/LinearLayoutManager;

    invoke-virtual {p0}, Landroidx/preference/PreferenceFragment;->getActivity()Landroid/app/Activity;

    move-result-object v1

    invoke-direct {v0, v1}, Landroidx/recyclerview/widget/LinearLayoutManager;-><init>(Landroid/content/Context;)V

    return-object v0
.end method

.method public abstract onCreatePreferences(Landroid/os/Bundle;Ljava/lang/String;)V
    .annotation runtime Ljava/lang/Deprecated;
    .end annotation
.end method

.method public onCreateRecyclerView(Landroid/view/LayoutInflater;Landroid/view/ViewGroup;Landroid/os/Bundle;)Landroidx/recyclerview/widget/RecyclerView;
    .locals 2
    .param p1, "inflater"    # Landroid/view/LayoutInflater;
    .param p2, "parent"    # Landroid/view/ViewGroup;
    .param p3, "savedInstanceState"    # Landroid/os/Bundle;
    .annotation runtime Ljava/lang/Deprecated;
    .end annotation

    .line 552
    iget-object v0, p0, Landroidx/preference/PreferenceFragment;->mStyledContext:Landroid/content/Context;

    invoke-virtual {v0}, Landroid/content/Context;->getPackageManager()Landroid/content/pm/PackageManager;

    move-result-object v0

    const-string v1, "android.hardware.type.automotive"

    invoke-virtual {v0, v1}, Landroid/content/pm/PackageManager;->hasSystemFeature(Ljava/lang/String;)Z

    move-result v0

    if-eqz v0, :cond_0

    .line 554
    sget v0, Landroidx/preference/R$id;->recycler_view:I

    invoke-virtual {p2, v0}, Landroid/view/ViewGroup;->findViewById(I)Landroid/view/View;

    move-result-object v0

    check-cast v0, Landroidx/recyclerview/widget/RecyclerView;

    .line 555
    .local v0, "recyclerView":Landroidx/recyclerview/widget/RecyclerView;
    if-eqz v0, :cond_0

    .line 556
    return-object v0

    .line 559
    .end local v0    # "recyclerView":Landroidx/recyclerview/widget/RecyclerView;
    :cond_0
    sget v0, Landroidx/preference/R$layout;->preference_recyclerview:I

    const/4 v1, 0x0

    invoke-virtual {p1, v0, p2, v1}, Landroid/view/LayoutInflater;->inflate(ILandroid/view/ViewGroup;Z)Landroid/view/View;

    move-result-object v0

    check-cast v0, Landroidx/recyclerview/widget/RecyclerView;

    .line 562
    .restart local v0    # "recyclerView":Landroidx/recyclerview/widget/RecyclerView;
    invoke-virtual {p0}, Landroidx/preference/PreferenceFragment;->onCreateLayoutManager()Landroidx/recyclerview/widget/RecyclerView$LayoutManager;

    move-result-object v1

    invoke-virtual {v0, v1}, Landroidx/recyclerview/widget/RecyclerView;->setLayoutManager(Landroidx/recyclerview/widget/RecyclerView$LayoutManager;)V

    .line 563
    new-instance v1, Landroidx/preference/PreferenceRecyclerViewAccessibilityDelegate;

    invoke-direct {v1, v0}, Landroidx/preference/PreferenceRecyclerViewAccessibilityDelegate;-><init>(Landroidx/recyclerview/widget/RecyclerView;)V

    invoke-virtual {v0, v1}, Landroidx/recyclerview/widget/RecyclerView;->setAccessibilityDelegateCompat(Landroidx/recyclerview/widget/RecyclerViewAccessibilityDelegate;)V

    .line 566
    return-object v0
.end method

.method public onCreateView(Landroid/view/LayoutInflater;Landroid/view/ViewGroup;Landroid/os/Bundle;)Landroid/view/View;
    .locals 11
    .param p1, "inflater"    # Landroid/view/LayoutInflater;
    .param p2, "container"    # Landroid/view/ViewGroup;
    .param p3, "savedInstanceState"    # Landroid/os/Bundle;

    .line 189
    iget-object v0, p0, Landroidx/preference/PreferenceFragment;->mStyledContext:Landroid/content/Context;

    sget-object v1, Landroidx/preference/R$styleable;->PreferenceFragment:[I

    iget-object v2, p0, Landroidx/preference/PreferenceFragment;->mStyledContext:Landroid/content/Context;

    sget v3, Landroidx/preference/R$attr;->preferenceFragmentStyle:I

    .line 191
    const v4, 0x1010506

    invoke-static {v2, v3, v4}, Landroidx/core/content/res/TypedArrayUtils;->getAttr(Landroid/content/Context;II)I

    move-result v2

    .line 189
    const/4 v3, 0x0

    const/4 v4, 0x0

    invoke-virtual {v0, v3, v1, v2, v4}, Landroid/content/Context;->obtainStyledAttributes(Landroid/util/AttributeSet;[III)Landroid/content/res/TypedArray;

    move-result-object v0

    .line 194
    .local v0, "a":Landroid/content/res/TypedArray;
    sget v1, Landroidx/preference/R$styleable;->PreferenceFragment_android_layout:I

    iget v2, p0, Landroidx/preference/PreferenceFragment;->mLayoutResId:I

    invoke-virtual {v0, v1, v2}, Landroid/content/res/TypedArray;->getResourceId(II)I

    move-result v1

    iput v1, p0, Landroidx/preference/PreferenceFragment;->mLayoutResId:I

    .line 196
    sget v1, Landroidx/preference/R$styleable;->PreferenceFragment_android_divider:I

    invoke-virtual {v0, v1}, Landroid/content/res/TypedArray;->getDrawable(I)Landroid/graphics/drawable/Drawable;

    move-result-object v1

    .line 197
    .local v1, "divider":Landroid/graphics/drawable/Drawable;
    sget v2, Landroidx/preference/R$styleable;->PreferenceFragment_android_dividerHeight:I

    const/4 v3, -0x1

    invoke-virtual {v0, v2, v3}, Landroid/content/res/TypedArray;->getDimensionPixelSize(II)I

    move-result v2

    .line 199
    .local v2, "dividerHeight":I
    sget v5, Landroidx/preference/R$styleable;->PreferenceFragment_allowDividerAfterLastItem:I

    const/4 v6, 0x1

    invoke-virtual {v0, v5, v6}, Landroid/content/res/TypedArray;->getBoolean(IZ)Z

    move-result v5

    .line 201
    .local v5, "allowDividerAfterLastItem":Z
    invoke-virtual {v0}, Landroid/content/res/TypedArray;->recycle()V

    .line 203
    iget-object v6, p0, Landroidx/preference/PreferenceFragment;->mStyledContext:Landroid/content/Context;

    invoke-virtual {p1, v6}, Landroid/view/LayoutInflater;->cloneInContext(Landroid/content/Context;)Landroid/view/LayoutInflater;

    move-result-object v6

    .line 205
    .local v6, "themedInflater":Landroid/view/LayoutInflater;
    iget v7, p0, Landroidx/preference/PreferenceFragment;->mLayoutResId:I

    invoke-virtual {v6, v7, p2, v4}, Landroid/view/LayoutInflater;->inflate(ILandroid/view/ViewGroup;Z)Landroid/view/View;

    move-result-object v4

    .line 207
    .local v4, "view":Landroid/view/View;
    const v7, 0x102003f

    invoke-virtual {v4, v7}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object v7

    .line 208
    .local v7, "rawListContainer":Landroid/view/View;
    instance-of v8, v7, Landroid/view/ViewGroup;

    if-eqz v8, :cond_3

    .line 213
    move-object v8, v7

    check-cast v8, Landroid/view/ViewGroup;

    .line 215
    .local v8, "listContainer":Landroid/view/ViewGroup;
    invoke-virtual {p0, v6, v8, p3}, Landroidx/preference/PreferenceFragment;->onCreateRecyclerView(Landroid/view/LayoutInflater;Landroid/view/ViewGroup;Landroid/os/Bundle;)Landroidx/recyclerview/widget/RecyclerView;

    move-result-object v9

    .line 217
    .local v9, "listView":Landroidx/recyclerview/widget/RecyclerView;
    if-eqz v9, :cond_2

    .line 221
    iput-object v9, p0, Landroidx/preference/PreferenceFragment;->mList:Landroidx/recyclerview/widget/RecyclerView;

    .line 223
    iget-object v10, p0, Landroidx/preference/PreferenceFragment;->mDividerDecoration:Landroidx/preference/PreferenceFragment$DividerDecoration;

    invoke-virtual {v9, v10}, Landroidx/recyclerview/widget/RecyclerView;->addItemDecoration(Landroidx/recyclerview/widget/RecyclerView$ItemDecoration;)V

    .line 224
    invoke-virtual {p0, v1}, Landroidx/preference/PreferenceFragment;->setDivider(Landroid/graphics/drawable/Drawable;)V

    .line 225
    if-eq v2, v3, :cond_0

    .line 226
    invoke-virtual {p0, v2}, Landroidx/preference/PreferenceFragment;->setDividerHeight(I)V

    .line 228
    :cond_0
    iget-object v3, p0, Landroidx/preference/PreferenceFragment;->mDividerDecoration:Landroidx/preference/PreferenceFragment$DividerDecoration;

    invoke-virtual {v3, v5}, Landroidx/preference/PreferenceFragment$DividerDecoration;->setAllowDividerAfterLastItem(Z)V

    .line 232
    iget-object v3, p0, Landroidx/preference/PreferenceFragment;->mList:Landroidx/recyclerview/widget/RecyclerView;

    invoke-virtual {v3}, Landroidx/recyclerview/widget/RecyclerView;->getParent()Landroid/view/ViewParent;

    move-result-object v3

    if-nez v3, :cond_1

    .line 233
    iget-object v3, p0, Landroidx/preference/PreferenceFragment;->mList:Landroidx/recyclerview/widget/RecyclerView;

    invoke-virtual {v8, v3}, Landroid/view/ViewGroup;->addView(Landroid/view/View;)V

    .line 235
    :cond_1
    iget-object v3, p0, Landroidx/preference/PreferenceFragment;->mHandler:Landroid/os/Handler;

    iget-object v10, p0, Landroidx/preference/PreferenceFragment;->mRequestFocus:Ljava/lang/Runnable;

    invoke-virtual {v3, v10}, Landroid/os/Handler;->post(Ljava/lang/Runnable;)Z

    .line 237
    return-object v4

    .line 218
    :cond_2
    new-instance v3, Ljava/lang/RuntimeException;

    const-string v10, "Could not create RecyclerView"

    invoke-direct {v3, v10}, Ljava/lang/RuntimeException;-><init>(Ljava/lang/String;)V

    throw v3

    .line 209
    .end local v8    # "listContainer":Landroid/view/ViewGroup;
    .end local v9    # "listView":Landroidx/recyclerview/widget/RecyclerView;
    :cond_3
    new-instance v3, Ljava/lang/RuntimeException;

    const-string v8, "Content has view with id attribute \'android.R.id.list_container\' that is not a ViewGroup class"

    invoke-direct {v3, v8}, Ljava/lang/RuntimeException;-><init>(Ljava/lang/String;)V

    throw v3
.end method

.method public onDestroyView()V
    .locals 2

    .line 311
    iget-object v0, p0, Landroidx/preference/PreferenceFragment;->mHandler:Landroid/os/Handler;

    iget-object v1, p0, Landroidx/preference/PreferenceFragment;->mRequestFocus:Ljava/lang/Runnable;

    invoke-virtual {v0, v1}, Landroid/os/Handler;->removeCallbacks(Ljava/lang/Runnable;)V

    .line 312
    iget-object v0, p0, Landroidx/preference/PreferenceFragment;->mHandler:Landroid/os/Handler;

    const/4 v1, 0x1

    invoke-virtual {v0, v1}, Landroid/os/Handler;->removeMessages(I)V

    .line 313
    iget-boolean v0, p0, Landroidx/preference/PreferenceFragment;->mHavePrefs:Z

    if-eqz v0, :cond_0

    .line 314
    invoke-direct {p0}, Landroidx/preference/PreferenceFragment;->unbindPreferences()V

    .line 316
    :cond_0
    const/4 v0, 0x0

    iput-object v0, p0, Landroidx/preference/PreferenceFragment;->mList:Landroidx/recyclerview/widget/RecyclerView;

    .line 317
    invoke-super {p0}, Landroid/app/Fragment;->onDestroyView()V

    .line 318
    return-void
.end method

.method public onDisplayPreferenceDialog(Landroidx/preference/Preference;)V
    .locals 4
    .param p1, "preference"    # Landroidx/preference/Preference;
    .annotation runtime Ljava/lang/Deprecated;
    .end annotation

    .line 608
    const/4 v0, 0x0

    .line 609
    .local v0, "handled":Z
    invoke-virtual {p0}, Landroidx/preference/PreferenceFragment;->getCallbackFragment()Landroid/app/Fragment;

    move-result-object v1

    instance-of v1, v1, Landroidx/preference/PreferenceFragment$OnPreferenceDisplayDialogCallback;

    if-eqz v1, :cond_0

    .line 610
    invoke-virtual {p0}, Landroidx/preference/PreferenceFragment;->getCallbackFragment()Landroid/app/Fragment;

    move-result-object v1

    check-cast v1, Landroidx/preference/PreferenceFragment$OnPreferenceDisplayDialogCallback;

    .line 611
    invoke-interface {v1, p0, p1}, Landroidx/preference/PreferenceFragment$OnPreferenceDisplayDialogCallback;->onPreferenceDisplayDialog(Landroidx/preference/PreferenceFragment;Landroidx/preference/Preference;)Z

    move-result v0

    .line 613
    :cond_0
    if-nez v0, :cond_1

    invoke-virtual {p0}, Landroidx/preference/PreferenceFragment;->getActivity()Landroid/app/Activity;

    move-result-object v1

    instance-of v1, v1, Landroidx/preference/PreferenceFragment$OnPreferenceDisplayDialogCallback;

    if-eqz v1, :cond_1

    .line 614
    invoke-virtual {p0}, Landroidx/preference/PreferenceFragment;->getActivity()Landroid/app/Activity;

    move-result-object v1

    check-cast v1, Landroidx/preference/PreferenceFragment$OnPreferenceDisplayDialogCallback;

    .line 615
    invoke-interface {v1, p0, p1}, Landroidx/preference/PreferenceFragment$OnPreferenceDisplayDialogCallback;->onPreferenceDisplayDialog(Landroidx/preference/PreferenceFragment;Landroidx/preference/Preference;)Z

    move-result v0

    .line 618
    :cond_1
    if-eqz v0, :cond_2

    .line 619
    return-void

    .line 623
    :cond_2
    invoke-virtual {p0}, Landroidx/preference/PreferenceFragment;->getFragmentManager()Landroid/app/FragmentManager;

    move-result-object v1

    const-string v2, "androidx.preference.PreferenceFragment.DIALOG"

    invoke-virtual {v1, v2}, Landroid/app/FragmentManager;->findFragmentByTag(Ljava/lang/String;)Landroid/app/Fragment;

    move-result-object v1

    if-eqz v1, :cond_3

    .line 624
    return-void

    .line 628
    :cond_3
    instance-of v1, p1, Landroidx/preference/EditTextPreference;

    if-eqz v1, :cond_4

    .line 629
    invoke-virtual {p1}, Landroidx/preference/Preference;->getKey()Ljava/lang/String;

    move-result-object v1

    invoke-static {v1}, Landroidx/preference/EditTextPreferenceDialogFragment;->newInstance(Ljava/lang/String;)Landroidx/preference/EditTextPreferenceDialogFragment;

    move-result-object v1

    .local v1, "f":Landroid/app/DialogFragment;
    goto :goto_0

    .line 630
    .end local v1    # "f":Landroid/app/DialogFragment;
    :cond_4
    instance-of v1, p1, Landroidx/preference/ListPreference;

    if-eqz v1, :cond_5

    .line 631
    invoke-virtual {p1}, Landroidx/preference/Preference;->getKey()Ljava/lang/String;

    move-result-object v1

    invoke-static {v1}, Landroidx/preference/ListPreferenceDialogFragment;->newInstance(Ljava/lang/String;)Landroidx/preference/ListPreferenceDialogFragment;

    move-result-object v1

    .restart local v1    # "f":Landroid/app/DialogFragment;
    goto :goto_0

    .line 632
    .end local v1    # "f":Landroid/app/DialogFragment;
    :cond_5
    instance-of v1, p1, Landroidx/preference/MultiSelectListPreference;

    if-eqz v1, :cond_6

    .line 633
    invoke-virtual {p1}, Landroidx/preference/Preference;->getKey()Ljava/lang/String;

    move-result-object v1

    invoke-static {v1}, Landroidx/preference/MultiSelectListPreferenceDialogFragment;->newInstance(Ljava/lang/String;)Landroidx/preference/MultiSelectListPreferenceDialogFragment;

    move-result-object v1

    .line 638
    .restart local v1    # "f":Landroid/app/DialogFragment;
    :goto_0
    const/4 v3, 0x0

    invoke-virtual {v1, p0, v3}, Landroid/app/DialogFragment;->setTargetFragment(Landroid/app/Fragment;I)V

    .line 639
    invoke-virtual {p0}, Landroidx/preference/PreferenceFragment;->getFragmentManager()Landroid/app/FragmentManager;

    move-result-object v3

    invoke-virtual {v1, v3, v2}, Landroid/app/DialogFragment;->show(Landroid/app/FragmentManager;Ljava/lang/String;)V

    .line 640
    return-void

    .line 635
    .end local v1    # "f":Landroid/app/DialogFragment;
    :cond_6
    new-instance v1, Ljava/lang/IllegalArgumentException;

    const-string v2, "Tried to display dialog for unknown preference type. Did you forget to override onDisplayPreferenceDialog()?"

    invoke-direct {v1, v2}, Ljava/lang/IllegalArgumentException;-><init>(Ljava/lang/String;)V

    throw v1
.end method

.method public onNavigateToScreen(Landroidx/preference/PreferenceScreen;)V
    .locals 2
    .param p1, "preferenceScreen"    # Landroidx/preference/PreferenceScreen;
    .annotation runtime Ljava/lang/Deprecated;
    .end annotation

    .line 458
    const/4 v0, 0x0

    .line 459
    .local v0, "handled":Z
    invoke-virtual {p0}, Landroidx/preference/PreferenceFragment;->getCallbackFragment()Landroid/app/Fragment;

    move-result-object v1

    instance-of v1, v1, Landroidx/preference/PreferenceFragment$OnPreferenceStartScreenCallback;

    if-eqz v1, :cond_0

    .line 460
    invoke-virtual {p0}, Landroidx/preference/PreferenceFragment;->getCallbackFragment()Landroid/app/Fragment;

    move-result-object v1

    check-cast v1, Landroidx/preference/PreferenceFragment$OnPreferenceStartScreenCallback;

    .line 461
    invoke-interface {v1, p0, p1}, Landroidx/preference/PreferenceFragment$OnPreferenceStartScreenCallback;->onPreferenceStartScreen(Landroidx/preference/PreferenceFragment;Landroidx/preference/PreferenceScreen;)Z

    move-result v0

    .line 463
    :cond_0
    if-nez v0, :cond_1

    invoke-virtual {p0}, Landroidx/preference/PreferenceFragment;->getActivity()Landroid/app/Activity;

    move-result-object v1

    instance-of v1, v1, Landroidx/preference/PreferenceFragment$OnPreferenceStartScreenCallback;

    if-eqz v1, :cond_1

    .line 464
    invoke-virtual {p0}, Landroidx/preference/PreferenceFragment;->getActivity()Landroid/app/Activity;

    move-result-object v1

    check-cast v1, Landroidx/preference/PreferenceFragment$OnPreferenceStartScreenCallback;

    .line 465
    invoke-interface {v1, p0, p1}, Landroidx/preference/PreferenceFragment$OnPreferenceStartScreenCallback;->onPreferenceStartScreen(Landroidx/preference/PreferenceFragment;Landroidx/preference/PreferenceScreen;)Z

    .line 467
    :cond_1
    return-void
.end method

.method public onPreferenceTreeClick(Landroidx/preference/Preference;)Z
    .locals 2
    .param p1, "preference"    # Landroidx/preference/Preference;
    .annotation runtime Ljava/lang/Deprecated;
    .end annotation

    .line 429
    invoke-virtual {p1}, Landroidx/preference/Preference;->getFragment()Ljava/lang/String;

    move-result-object v0

    if-eqz v0, :cond_2

    .line 430
    const/4 v0, 0x0

    .line 431
    .local v0, "handled":Z
    invoke-virtual {p0}, Landroidx/preference/PreferenceFragment;->getCallbackFragment()Landroid/app/Fragment;

    move-result-object v1

    instance-of v1, v1, Landroidx/preference/PreferenceFragment$OnPreferenceStartFragmentCallback;

    if-eqz v1, :cond_0

    .line 432
    invoke-virtual {p0}, Landroidx/preference/PreferenceFragment;->getCallbackFragment()Landroid/app/Fragment;

    move-result-object v1

    check-cast v1, Landroidx/preference/PreferenceFragment$OnPreferenceStartFragmentCallback;

    .line 433
    invoke-interface {v1, p0, p1}, Landroidx/preference/PreferenceFragment$OnPreferenceStartFragmentCallback;->onPreferenceStartFragment(Landroidx/preference/PreferenceFragment;Landroidx/preference/Preference;)Z

    move-result v0

    .line 435
    :cond_0
    if-nez v0, :cond_1

    invoke-virtual {p0}, Landroidx/preference/PreferenceFragment;->getActivity()Landroid/app/Activity;

    move-result-object v1

    instance-of v1, v1, Landroidx/preference/PreferenceFragment$OnPreferenceStartFragmentCallback;

    if-eqz v1, :cond_1

    .line 436
    invoke-virtual {p0}, Landroidx/preference/PreferenceFragment;->getActivity()Landroid/app/Activity;

    move-result-object v1

    check-cast v1, Landroidx/preference/PreferenceFragment$OnPreferenceStartFragmentCallback;

    .line 437
    invoke-interface {v1, p0, p1}, Landroidx/preference/PreferenceFragment$OnPreferenceStartFragmentCallback;->onPreferenceStartFragment(Landroidx/preference/PreferenceFragment;Landroidx/preference/Preference;)Z

    move-result v0

    .line 439
    :cond_1
    return v0

    .line 441
    .end local v0    # "handled":Z
    :cond_2
    const/4 v0, 0x0

    return v0
.end method

.method public onSaveInstanceState(Landroid/os/Bundle;)V
    .locals 3
    .param p1, "outState"    # Landroid/os/Bundle;

    .line 322
    invoke-super {p0, p1}, Landroid/app/Fragment;->onSaveInstanceState(Landroid/os/Bundle;)V

    .line 324
    invoke-virtual {p0}, Landroidx/preference/PreferenceFragment;->getPreferenceScreen()Landroidx/preference/PreferenceScreen;

    move-result-object v0

    .line 325
    .local v0, "preferenceScreen":Landroidx/preference/PreferenceScreen;
    if-eqz v0, :cond_0

    .line 326
    new-instance v1, Landroid/os/Bundle;

    invoke-direct {v1}, Landroid/os/Bundle;-><init>()V

    .line 327
    .local v1, "container":Landroid/os/Bundle;
    invoke-virtual {v0, v1}, Landroidx/preference/PreferenceScreen;->saveHierarchyState(Landroid/os/Bundle;)V

    .line 328
    const-string v2, "android:preferences"

    invoke-virtual {p1, v2, v1}, Landroid/os/Bundle;->putBundle(Ljava/lang/String;Landroid/os/Bundle;)V

    .line 330
    .end local v1    # "container":Landroid/os/Bundle;
    :cond_0
    return-void
.end method

.method public onStart()V
    .locals 1

    .line 297
    invoke-super {p0}, Landroid/app/Fragment;->onStart()V

    .line 298
    iget-object v0, p0, Landroidx/preference/PreferenceFragment;->mPreferenceManager:Landroidx/preference/PreferenceManager;

    invoke-virtual {v0, p0}, Landroidx/preference/PreferenceManager;->setOnPreferenceTreeClickListener(Landroidx/preference/PreferenceManager$OnPreferenceTreeClickListener;)V

    .line 299
    iget-object v0, p0, Landroidx/preference/PreferenceFragment;->mPreferenceManager:Landroidx/preference/PreferenceManager;

    invoke-virtual {v0, p0}, Landroidx/preference/PreferenceManager;->setOnDisplayPreferenceDialogListener(Landroidx/preference/PreferenceManager$OnDisplayPreferenceDialogListener;)V

    .line 300
    return-void
.end method

.method public onStop()V
    .locals 2

    .line 304
    invoke-super {p0}, Landroid/app/Fragment;->onStop()V

    .line 305
    iget-object v0, p0, Landroidx/preference/PreferenceFragment;->mPreferenceManager:Landroidx/preference/PreferenceManager;

    const/4 v1, 0x0

    invoke-virtual {v0, v1}, Landroidx/preference/PreferenceManager;->setOnPreferenceTreeClickListener(Landroidx/preference/PreferenceManager$OnPreferenceTreeClickListener;)V

    .line 306
    iget-object v0, p0, Landroidx/preference/PreferenceFragment;->mPreferenceManager:Landroidx/preference/PreferenceManager;

    invoke-virtual {v0, v1}, Landroidx/preference/PreferenceManager;->setOnDisplayPreferenceDialogListener(Landroidx/preference/PreferenceManager$OnDisplayPreferenceDialogListener;)V

    .line 307
    return-void
.end method

.method protected onUnbindPreferences()V
    .locals 0

    .line 522
    return-void
.end method

.method public onViewCreated(Landroid/view/View;Landroid/os/Bundle;)V
    .locals 2
    .param p1, "view"    # Landroid/view/View;
    .param p2, "savedInstanceState"    # Landroid/os/Bundle;

    .line 272
    invoke-super {p0, p1, p2}, Landroid/app/Fragment;->onViewCreated(Landroid/view/View;Landroid/os/Bundle;)V

    .line 274
    if-eqz p2, :cond_0

    .line 275
    const-string v0, "android:preferences"

    invoke-virtual {p2, v0}, Landroid/os/Bundle;->getBundle(Ljava/lang/String;)Landroid/os/Bundle;

    move-result-object v0

    .line 276
    .local v0, "container":Landroid/os/Bundle;
    if-eqz v0, :cond_0

    .line 277
    invoke-virtual {p0}, Landroidx/preference/PreferenceFragment;->getPreferenceScreen()Landroidx/preference/PreferenceScreen;

    move-result-object v1

    .line 278
    .local v1, "preferenceScreen":Landroidx/preference/PreferenceScreen;
    if-eqz v1, :cond_0

    .line 279
    invoke-virtual {v1, v0}, Landroidx/preference/PreferenceScreen;->restoreHierarchyState(Landroid/os/Bundle;)V

    .line 284
    .end local v0    # "container":Landroid/os/Bundle;
    .end local v1    # "preferenceScreen":Landroidx/preference/PreferenceScreen;
    :cond_0
    iget-boolean v0, p0, Landroidx/preference/PreferenceFragment;->mHavePrefs:Z

    if-eqz v0, :cond_1

    .line 285
    invoke-virtual {p0}, Landroidx/preference/PreferenceFragment;->bindPreferences()V

    .line 286
    iget-object v0, p0, Landroidx/preference/PreferenceFragment;->mSelectPreferenceRunnable:Ljava/lang/Runnable;

    if-eqz v0, :cond_1

    .line 287
    invoke-interface {v0}, Ljava/lang/Runnable;->run()V

    .line 288
    const/4 v0, 0x0

    iput-object v0, p0, Landroidx/preference/PreferenceFragment;->mSelectPreferenceRunnable:Ljava/lang/Runnable;

    .line 292
    :cond_1
    const/4 v0, 0x1

    iput-boolean v0, p0, Landroidx/preference/PreferenceFragment;->mInitDone:Z

    .line 293
    return-void
.end method

.method public scrollToPreference(Landroidx/preference/Preference;)V
    .locals 1
    .param p1, "preference"    # Landroidx/preference/Preference;
    .annotation runtime Ljava/lang/Deprecated;
    .end annotation

    .line 666
    const/4 v0, 0x0

    invoke-direct {p0, p1, v0}, Landroidx/preference/PreferenceFragment;->scrollToPreferenceInternal(Landroidx/preference/Preference;Ljava/lang/String;)V

    .line 667
    return-void
.end method

.method public scrollToPreference(Ljava/lang/String;)V
    .locals 1
    .param p1, "key"    # Ljava/lang/String;
    .annotation runtime Ljava/lang/Deprecated;
    .end annotation

    .line 658
    const/4 v0, 0x0

    invoke-direct {p0, v0, p1}, Landroidx/preference/PreferenceFragment;->scrollToPreferenceInternal(Landroidx/preference/Preference;Ljava/lang/String;)V

    .line 659
    return-void
.end method

.method public setDivider(Landroid/graphics/drawable/Drawable;)V
    .locals 1
    .param p1, "divider"    # Landroid/graphics/drawable/Drawable;
    .annotation runtime Ljava/lang/Deprecated;
    .end annotation

    .line 253
    iget-object v0, p0, Landroidx/preference/PreferenceFragment;->mDividerDecoration:Landroidx/preference/PreferenceFragment$DividerDecoration;

    invoke-virtual {v0, p1}, Landroidx/preference/PreferenceFragment$DividerDecoration;->setDivider(Landroid/graphics/drawable/Drawable;)V

    .line 254
    return-void
.end method

.method public setDividerHeight(I)V
    .locals 1
    .param p1, "height"    # I
    .annotation runtime Ljava/lang/Deprecated;
    .end annotation

    .line 267
    iget-object v0, p0, Landroidx/preference/PreferenceFragment;->mDividerDecoration:Landroidx/preference/PreferenceFragment$DividerDecoration;

    invoke-virtual {v0, p1}, Landroidx/preference/PreferenceFragment$DividerDecoration;->setDividerHeight(I)V

    .line 268
    return-void
.end method

.method public setPreferenceScreen(Landroidx/preference/PreferenceScreen;)V
    .locals 1
    .param p1, "preferenceScreen"    # Landroidx/preference/PreferenceScreen;
    .annotation runtime Ljava/lang/Deprecated;
    .end annotation

    .line 353
    iget-object v0, p0, Landroidx/preference/PreferenceFragment;->mPreferenceManager:Landroidx/preference/PreferenceManager;

    invoke-virtual {v0, p1}, Landroidx/preference/PreferenceManager;->setPreferences(Landroidx/preference/PreferenceScreen;)Z

    move-result v0

    if-eqz v0, :cond_0

    if-eqz p1, :cond_0

    .line 354
    invoke-virtual {p0}, Landroidx/preference/PreferenceFragment;->onUnbindPreferences()V

    .line 355
    const/4 v0, 0x1

    iput-boolean v0, p0, Landroidx/preference/PreferenceFragment;->mHavePrefs:Z

    .line 356
    iget-boolean v0, p0, Landroidx/preference/PreferenceFragment;->mInitDone:Z

    if-eqz v0, :cond_0

    .line 357
    invoke-direct {p0}, Landroidx/preference/PreferenceFragment;->postBindPreferences()V

    .line 360
    :cond_0
    return-void
.end method

.method public setPreferencesFromResource(ILjava/lang/String;)V
    .locals 5
    .param p1, "preferencesResId"    # I
    .param p2, "key"    # Ljava/lang/String;
    .annotation runtime Ljava/lang/Deprecated;
    .end annotation

    .line 403
    invoke-direct {p0}, Landroidx/preference/PreferenceFragment;->requirePreferenceManager()V

    .line 405
    iget-object v0, p0, Landroidx/preference/PreferenceFragment;->mPreferenceManager:Landroidx/preference/PreferenceManager;

    iget-object v1, p0, Landroidx/preference/PreferenceFragment;->mStyledContext:Landroid/content/Context;

    const/4 v2, 0x0

    invoke-virtual {v0, v1, p1, v2}, Landroidx/preference/PreferenceManager;->inflateFromResource(Landroid/content/Context;ILandroidx/preference/PreferenceScreen;)Landroidx/preference/PreferenceScreen;

    move-result-object v0

    .line 409
    .local v0, "xmlRoot":Landroidx/preference/PreferenceScreen;
    if-eqz p2, :cond_1

    .line 410
    invoke-virtual {v0, p2}, Landroidx/preference/PreferenceScreen;->findPreference(Ljava/lang/CharSequence;)Landroidx/preference/Preference;

    move-result-object v1

    .line 411
    .local v1, "root":Landroidx/preference/Preference;
    instance-of v2, v1, Landroidx/preference/PreferenceScreen;

    if-eqz v2, :cond_0

    goto :goto_0

    .line 412
    :cond_0
    new-instance v2, Ljava/lang/IllegalArgumentException;

    new-instance v3, Ljava/lang/StringBuilder;

    invoke-direct {v3}, Ljava/lang/StringBuilder;-><init>()V

    const-string v4, "Preference object with key "

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v3

    invoke-virtual {v3, p2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v3

    const-string v4, " is not a PreferenceScreen"

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v3

    invoke-virtual {v3}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v3

    invoke-direct {v2, v3}, Ljava/lang/IllegalArgumentException;-><init>(Ljava/lang/String;)V

    throw v2

    .line 416
    .end local v1    # "root":Landroidx/preference/Preference;
    :cond_1
    move-object v1, v0

    .line 419
    .restart local v1    # "root":Landroidx/preference/Preference;
    :goto_0
    move-object v2, v1

    check-cast v2, Landroidx/preference/PreferenceScreen;

    invoke-virtual {p0, v2}, Landroidx/preference/PreferenceFragment;->setPreferenceScreen(Landroidx/preference/PreferenceScreen;)V

    .line 420
    return-void
.end method
