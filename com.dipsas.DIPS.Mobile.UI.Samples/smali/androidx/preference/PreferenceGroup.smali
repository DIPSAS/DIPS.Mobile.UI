.class public abstract Landroidx/preference/PreferenceGroup;
.super Landroidx/preference/Preference;
.source "PreferenceGroup.java"


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Landroidx/preference/PreferenceGroup$SavedState;,
        Landroidx/preference/PreferenceGroup$OnExpandButtonClickListener;,
        Landroidx/preference/PreferenceGroup$PreferencePositionCallback;
    }
.end annotation


# static fields
.field private static final TAG:Ljava/lang/String; = "PreferenceGroup"


# instance fields
.field private mAttachedToHierarchy:Z

.field private final mClearRecycleCacheRunnable:Ljava/lang/Runnable;

.field private mCurrentPreferenceOrder:I

.field private final mHandler:Landroid/os/Handler;

.field final mIdRecycleCache:Landroidx/collection/SimpleArrayMap;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Landroidx/collection/SimpleArrayMap<",
            "Ljava/lang/String;",
            "Ljava/lang/Long;",
            ">;"
        }
    .end annotation
.end field

.field private mInitialExpandedChildrenCount:I

.field private mOnExpandButtonClickListener:Landroidx/preference/PreferenceGroup$OnExpandButtonClickListener;

.field private mOrderingAsAdded:Z

.field private mPreferences:Ljava/util/List;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/List<",
            "Landroidx/preference/Preference;",
            ">;"
        }
    .end annotation
.end field


# direct methods
.method public constructor <init>(Landroid/content/Context;Landroid/util/AttributeSet;)V
    .locals 1
    .param p1, "context"    # Landroid/content/Context;
    .param p2, "attrs"    # Landroid/util/AttributeSet;

    .line 106
    const/4 v0, 0x0

    invoke-direct {p0, p1, p2, v0}, Landroidx/preference/PreferenceGroup;-><init>(Landroid/content/Context;Landroid/util/AttributeSet;I)V

    .line 107
    return-void
.end method

.method public constructor <init>(Landroid/content/Context;Landroid/util/AttributeSet;I)V
    .locals 1
    .param p1, "context"    # Landroid/content/Context;
    .param p2, "attrs"    # Landroid/util/AttributeSet;
    .param p3, "defStyleAttr"    # I

    .line 102
    const/4 v0, 0x0

    invoke-direct {p0, p1, p2, p3, v0}, Landroidx/preference/PreferenceGroup;-><init>(Landroid/content/Context;Landroid/util/AttributeSet;II)V

    .line 103
    return-void
.end method

.method public constructor <init>(Landroid/content/Context;Landroid/util/AttributeSet;II)V
    .locals 5
    .param p1, "context"    # Landroid/content/Context;
    .param p2, "attrs"    # Landroid/util/AttributeSet;
    .param p3, "defStyleAttr"    # I
    .param p4, "defStyleRes"    # I

    .line 82
    invoke-direct {p0, p1, p2, p3, p4}, Landroidx/preference/Preference;-><init>(Landroid/content/Context;Landroid/util/AttributeSet;II)V

    .line 58
    new-instance v0, Landroidx/collection/SimpleArrayMap;

    invoke-direct {v0}, Landroidx/collection/SimpleArrayMap;-><init>()V

    iput-object v0, p0, Landroidx/preference/PreferenceGroup;->mIdRecycleCache:Landroidx/collection/SimpleArrayMap;

    .line 60
    new-instance v0, Landroid/os/Handler;

    invoke-direct {v0}, Landroid/os/Handler;-><init>()V

    iput-object v0, p0, Landroidx/preference/PreferenceGroup;->mHandler:Landroid/os/Handler;

    .line 66
    const/4 v0, 0x1

    iput-boolean v0, p0, Landroidx/preference/PreferenceGroup;->mOrderingAsAdded:Z

    .line 67
    const/4 v1, 0x0

    iput v1, p0, Landroidx/preference/PreferenceGroup;->mCurrentPreferenceOrder:I

    .line 68
    iput-boolean v1, p0, Landroidx/preference/PreferenceGroup;->mAttachedToHierarchy:Z

    .line 69
    const v1, 0x7fffffff

    iput v1, p0, Landroidx/preference/PreferenceGroup;->mInitialExpandedChildrenCount:I

    .line 70
    const/4 v2, 0x0

    iput-object v2, p0, Landroidx/preference/PreferenceGroup;->mOnExpandButtonClickListener:Landroidx/preference/PreferenceGroup$OnExpandButtonClickListener;

    .line 72
    new-instance v2, Landroidx/preference/PreferenceGroup$1;

    invoke-direct {v2, p0}, Landroidx/preference/PreferenceGroup$1;-><init>(Landroidx/preference/PreferenceGroup;)V

    iput-object v2, p0, Landroidx/preference/PreferenceGroup;->mClearRecycleCacheRunnable:Ljava/lang/Runnable;

    .line 84
    new-instance v2, Ljava/util/ArrayList;

    invoke-direct {v2}, Ljava/util/ArrayList;-><init>()V

    iput-object v2, p0, Landroidx/preference/PreferenceGroup;->mPreferences:Ljava/util/List;

    .line 86
    sget-object v2, Landroidx/preference/R$styleable;->PreferenceGroup:[I

    invoke-virtual {p1, p2, v2, p3, p4}, Landroid/content/Context;->obtainStyledAttributes(Landroid/util/AttributeSet;[III)Landroid/content/res/TypedArray;

    move-result-object v2

    .line 89
    .local v2, "a":Landroid/content/res/TypedArray;
    sget v3, Landroidx/preference/R$styleable;->PreferenceGroup_orderingFromXml:I

    sget v4, Landroidx/preference/R$styleable;->PreferenceGroup_orderingFromXml:I

    .line 90
    invoke-static {v2, v3, v4, v0}, Landroidx/core/content/res/TypedArrayUtils;->getBoolean(Landroid/content/res/TypedArray;IIZ)Z

    move-result v0

    iput-boolean v0, p0, Landroidx/preference/PreferenceGroup;->mOrderingAsAdded:Z

    .line 93
    sget v0, Landroidx/preference/R$styleable;->PreferenceGroup_initialExpandedChildrenCount:I

    invoke-virtual {v2, v0}, Landroid/content/res/TypedArray;->hasValue(I)Z

    move-result v0

    if-eqz v0, :cond_0

    .line 94
    sget v0, Landroidx/preference/R$styleable;->PreferenceGroup_initialExpandedChildrenCount:I

    sget v3, Landroidx/preference/R$styleable;->PreferenceGroup_initialExpandedChildrenCount:I

    invoke-static {v2, v0, v3, v1}, Landroidx/core/content/res/TypedArrayUtils;->getInt(Landroid/content/res/TypedArray;III)I

    move-result v0

    invoke-virtual {p0, v0}, Landroidx/preference/PreferenceGroup;->setInitialExpandedChildrenCount(I)V

    .line 98
    :cond_0
    invoke-virtual {v2}, Landroid/content/res/TypedArray;->recycle()V

    .line 99
    return-void
.end method

.method private removePreferenceInt(Landroidx/preference/Preference;)Z
    .locals 5
    .param p1, "preference"    # Landroidx/preference/Preference;

    .line 297
    monitor-enter p0

    .line 298
    :try_start_0
    invoke-virtual {p1}, Landroidx/preference/Preference;->onPrepareForRemoval()V

    .line 299
    invoke-virtual {p1}, Landroidx/preference/Preference;->getParent()Landroidx/preference/PreferenceGroup;

    move-result-object v0

    if-ne v0, p0, :cond_0

    .line 300
    const/4 v0, 0x0

    invoke-virtual {p1, v0}, Landroidx/preference/Preference;->assignParent(Landroidx/preference/PreferenceGroup;)V

    .line 302
    :cond_0
    iget-object v0, p0, Landroidx/preference/PreferenceGroup;->mPreferences:Ljava/util/List;

    invoke-interface {v0, p1}, Ljava/util/List;->remove(Ljava/lang/Object;)Z

    move-result v0

    .line 303
    .local v0, "success":Z
    if-eqz v0, :cond_2

    .line 315
    invoke-virtual {p1}, Landroidx/preference/Preference;->getKey()Ljava/lang/String;

    move-result-object v1

    .line 316
    .local v1, "key":Ljava/lang/String;
    if-eqz v1, :cond_1

    .line 317
    iget-object v2, p0, Landroidx/preference/PreferenceGroup;->mIdRecycleCache:Landroidx/collection/SimpleArrayMap;

    invoke-virtual {p1}, Landroidx/preference/Preference;->getId()J

    move-result-wide v3

    invoke-static {v3, v4}, Ljava/lang/Long;->valueOf(J)Ljava/lang/Long;

    move-result-object v3

    invoke-virtual {v2, v1, v3}, Landroidx/collection/SimpleArrayMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 318
    iget-object v2, p0, Landroidx/preference/PreferenceGroup;->mHandler:Landroid/os/Handler;

    iget-object v3, p0, Landroidx/preference/PreferenceGroup;->mClearRecycleCacheRunnable:Ljava/lang/Runnable;

    invoke-virtual {v2, v3}, Landroid/os/Handler;->removeCallbacks(Ljava/lang/Runnable;)V

    .line 319
    iget-object v2, p0, Landroidx/preference/PreferenceGroup;->mHandler:Landroid/os/Handler;

    iget-object v3, p0, Landroidx/preference/PreferenceGroup;->mClearRecycleCacheRunnable:Ljava/lang/Runnable;

    invoke-virtual {v2, v3}, Landroid/os/Handler;->post(Ljava/lang/Runnable;)Z

    .line 321
    :cond_1
    iget-boolean v2, p0, Landroidx/preference/PreferenceGroup;->mAttachedToHierarchy:Z

    if-eqz v2, :cond_2

    .line 322
    invoke-virtual {p1}, Landroidx/preference/Preference;->onDetached()V

    .line 326
    .end local v1    # "key":Ljava/lang/String;
    :cond_2
    monitor-exit p0

    return v0

    .line 327
    .end local v0    # "success":Z
    :catchall_0
    move-exception v0

    monitor-exit p0
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    throw v0
.end method


# virtual methods
.method public addItemFromInflater(Landroidx/preference/Preference;)V
    .locals 0
    .param p1, "preference"    # Landroidx/preference/Preference;

    .line 170
    invoke-virtual {p0, p1}, Landroidx/preference/PreferenceGroup;->addPreference(Landroidx/preference/Preference;)Z

    .line 171
    return-void
.end method

.method public addPreference(Landroidx/preference/Preference;)Z
    .locals 7
    .param p1, "preference"    # Landroidx/preference/Preference;

    .line 199
    iget-object v0, p0, Landroidx/preference/PreferenceGroup;->mPreferences:Ljava/util/List;

    invoke-interface {v0, p1}, Ljava/util/List;->contains(Ljava/lang/Object;)Z

    move-result v0

    const/4 v1, 0x1

    if-eqz v0, :cond_0

    .line 200
    return v1

    .line 202
    :cond_0
    invoke-virtual {p1}, Landroidx/preference/Preference;->getKey()Ljava/lang/String;

    move-result-object v0

    if-eqz v0, :cond_2

    .line 203
    move-object v0, p0

    .line 204
    .local v0, "root":Landroidx/preference/PreferenceGroup;
    :goto_0
    invoke-virtual {v0}, Landroidx/preference/PreferenceGroup;->getParent()Landroidx/preference/PreferenceGroup;

    move-result-object v2

    if-eqz v2, :cond_1

    .line 205
    invoke-virtual {v0}, Landroidx/preference/PreferenceGroup;->getParent()Landroidx/preference/PreferenceGroup;

    move-result-object v0

    goto :goto_0

    .line 207
    :cond_1
    invoke-virtual {p1}, Landroidx/preference/Preference;->getKey()Ljava/lang/String;

    move-result-object v2

    .line 208
    .local v2, "key":Ljava/lang/String;
    invoke-virtual {v0, v2}, Landroidx/preference/PreferenceGroup;->findPreference(Ljava/lang/CharSequence;)Landroidx/preference/Preference;

    move-result-object v3

    if-eqz v3, :cond_2

    .line 209
    const-string v3, "PreferenceGroup"

    new-instance v4, Ljava/lang/StringBuilder;

    invoke-direct {v4}, Ljava/lang/StringBuilder;-><init>()V

    const-string v5, "Found duplicated key: \""

    invoke-virtual {v4, v5}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v4

    invoke-virtual {v4, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v4

    const-string v5, "\". This can cause unintended behaviour, please use unique keys for every preference."

    invoke-virtual {v4, v5}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v4

    invoke-virtual {v4}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v4

    invoke-static {v3, v4}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;)I

    .line 215
    .end local v0    # "root":Landroidx/preference/PreferenceGroup;
    .end local v2    # "key":Ljava/lang/String;
    :cond_2
    invoke-virtual {p1}, Landroidx/preference/Preference;->getOrder()I

    move-result v0

    const v2, 0x7fffffff

    if-ne v0, v2, :cond_4

    .line 216
    iget-boolean v0, p0, Landroidx/preference/PreferenceGroup;->mOrderingAsAdded:Z

    if-eqz v0, :cond_3

    .line 217
    iget v0, p0, Landroidx/preference/PreferenceGroup;->mCurrentPreferenceOrder:I

    add-int/lit8 v2, v0, 0x1

    iput v2, p0, Landroidx/preference/PreferenceGroup;->mCurrentPreferenceOrder:I

    invoke-virtual {p1, v0}, Landroidx/preference/Preference;->setOrder(I)V

    .line 220
    :cond_3
    instance-of v0, p1, Landroidx/preference/PreferenceGroup;

    if-eqz v0, :cond_4

    .line 223
    move-object v0, p1

    check-cast v0, Landroidx/preference/PreferenceGroup;

    iget-boolean v2, p0, Landroidx/preference/PreferenceGroup;->mOrderingAsAdded:Z

    invoke-virtual {v0, v2}, Landroidx/preference/PreferenceGroup;->setOrderingAsAdded(Z)V

    .line 227
    :cond_4
    iget-object v0, p0, Landroidx/preference/PreferenceGroup;->mPreferences:Ljava/util/List;

    invoke-static {v0, p1}, Ljava/util/Collections;->binarySearch(Ljava/util/List;Ljava/lang/Object;)I

    move-result v0

    .line 228
    .local v0, "insertionIndex":I
    if-gez v0, :cond_5

    .line 229
    mul-int/lit8 v2, v0, -0x1

    add-int/lit8 v0, v2, -0x1

    .line 232
    :cond_5
    invoke-virtual {p0, p1}, Landroidx/preference/PreferenceGroup;->onPrepareAddPreference(Landroidx/preference/Preference;)Z

    move-result v2

    if-nez v2, :cond_6

    .line 233
    const/4 v1, 0x0

    return v1

    .line 236
    :cond_6
    monitor-enter p0

    .line 237
    :try_start_0
    iget-object v2, p0, Landroidx/preference/PreferenceGroup;->mPreferences:Ljava/util/List;

    invoke-interface {v2, v0, p1}, Ljava/util/List;->add(ILjava/lang/Object;)V

    .line 238
    monitor-exit p0
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    .line 240
    invoke-virtual {p0}, Landroidx/preference/PreferenceGroup;->getPreferenceManager()Landroidx/preference/PreferenceManager;

    move-result-object v2

    .line 241
    .local v2, "preferenceManager":Landroidx/preference/PreferenceManager;
    invoke-virtual {p1}, Landroidx/preference/Preference;->getKey()Ljava/lang/String;

    move-result-object v3

    .line 243
    .local v3, "key":Ljava/lang/String;
    if-eqz v3, :cond_7

    iget-object v4, p0, Landroidx/preference/PreferenceGroup;->mIdRecycleCache:Landroidx/collection/SimpleArrayMap;

    invoke-virtual {v4, v3}, Landroidx/collection/SimpleArrayMap;->containsKey(Ljava/lang/Object;)Z

    move-result v4

    if-eqz v4, :cond_7

    .line 244
    iget-object v4, p0, Landroidx/preference/PreferenceGroup;->mIdRecycleCache:Landroidx/collection/SimpleArrayMap;

    invoke-virtual {v4, v3}, Landroidx/collection/SimpleArrayMap;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v4

    check-cast v4, Ljava/lang/Long;

    invoke-virtual {v4}, Ljava/lang/Long;->longValue()J

    move-result-wide v4

    .line 245
    .local v4, "id":J
    iget-object v6, p0, Landroidx/preference/PreferenceGroup;->mIdRecycleCache:Landroidx/collection/SimpleArrayMap;

    invoke-virtual {v6, v3}, Landroidx/collection/SimpleArrayMap;->remove(Ljava/lang/Object;)Ljava/lang/Object;

    goto :goto_1

    .line 247
    .end local v4    # "id":J
    :cond_7
    invoke-virtual {v2}, Landroidx/preference/PreferenceManager;->getNextId()J

    move-result-wide v4

    .line 249
    .restart local v4    # "id":J
    :goto_1
    invoke-virtual {p1, v2, v4, v5}, Landroidx/preference/Preference;->onAttachedToHierarchy(Landroidx/preference/PreferenceManager;J)V

    .line 250
    invoke-virtual {p1, p0}, Landroidx/preference/Preference;->assignParent(Landroidx/preference/PreferenceGroup;)V

    .line 252
    iget-boolean v6, p0, Landroidx/preference/PreferenceGroup;->mAttachedToHierarchy:Z

    if-eqz v6, :cond_8

    .line 253
    invoke-virtual {p1}, Landroidx/preference/Preference;->onAttached()V

    .line 256
    :cond_8
    invoke-virtual {p0}, Landroidx/preference/PreferenceGroup;->notifyHierarchyChanged()V

    .line 258
    return v1

    .line 238
    .end local v2    # "preferenceManager":Landroidx/preference/PreferenceManager;
    .end local v3    # "key":Ljava/lang/String;
    .end local v4    # "id":J
    :catchall_0
    move-exception v1

    :try_start_1
    monitor-exit p0
    :try_end_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    throw v1
.end method

.method protected dispatchRestoreInstanceState(Landroid/os/Bundle;)V
    .locals 3
    .param p1, "container"    # Landroid/os/Bundle;

    .line 502
    invoke-super {p0, p1}, Landroidx/preference/Preference;->dispatchRestoreInstanceState(Landroid/os/Bundle;)V

    .line 505
    invoke-virtual {p0}, Landroidx/preference/PreferenceGroup;->getPreferenceCount()I

    move-result v0

    .line 506
    .local v0, "preferenceCount":I
    const/4 v1, 0x0

    .local v1, "i":I
    :goto_0
    if-ge v1, v0, :cond_0

    .line 507
    invoke-virtual {p0, v1}, Landroidx/preference/PreferenceGroup;->getPreference(I)Landroidx/preference/Preference;

    move-result-object v2

    invoke-virtual {v2, p1}, Landroidx/preference/Preference;->dispatchRestoreInstanceState(Landroid/os/Bundle;)V

    .line 506
    add-int/lit8 v1, v1, 0x1

    goto :goto_0

    .line 509
    .end local v1    # "i":I
    :cond_0
    return-void
.end method

.method protected dispatchSaveInstanceState(Landroid/os/Bundle;)V
    .locals 3
    .param p1, "container"    # Landroid/os/Bundle;

    .line 491
    invoke-super {p0, p1}, Landroidx/preference/Preference;->dispatchSaveInstanceState(Landroid/os/Bundle;)V

    .line 494
    invoke-virtual {p0}, Landroidx/preference/PreferenceGroup;->getPreferenceCount()I

    move-result v0

    .line 495
    .local v0, "preferenceCount":I
    const/4 v1, 0x0

    .local v1, "i":I
    :goto_0
    if-ge v1, v0, :cond_0

    .line 496
    invoke-virtual {p0, v1}, Landroidx/preference/PreferenceGroup;->getPreference(I)Landroidx/preference/Preference;

    move-result-object v2

    invoke-virtual {v2, p1}, Landroidx/preference/Preference;->dispatchSaveInstanceState(Landroid/os/Bundle;)V

    .line 495
    add-int/lit8 v1, v1, 0x1

    goto :goto_0

    .line 498
    .end local v1    # "i":I
    :cond_0
    return-void
.end method

.method public findPreference(Ljava/lang/CharSequence;)Landroidx/preference/Preference;
    .locals 5
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

    .line 367
    if-eqz p1, :cond_4

    .line 370
    invoke-virtual {p0}, Landroidx/preference/PreferenceGroup;->getKey()Ljava/lang/String;

    move-result-object v0

    invoke-static {v0, p1}, Landroid/text/TextUtils;->equals(Ljava/lang/CharSequence;Ljava/lang/CharSequence;)Z

    move-result v0

    if-eqz v0, :cond_0

    .line 371
    return-object p0

    .line 373
    :cond_0
    invoke-virtual {p0}, Landroidx/preference/PreferenceGroup;->getPreferenceCount()I

    move-result v0

    .line 374
    .local v0, "preferenceCount":I
    const/4 v1, 0x0

    .local v1, "i":I
    :goto_0
    if-ge v1, v0, :cond_3

    .line 375
    invoke-virtual {p0, v1}, Landroidx/preference/PreferenceGroup;->getPreference(I)Landroidx/preference/Preference;

    move-result-object v2

    .line 376
    .local v2, "preference":Landroidx/preference/Preference;
    invoke-virtual {v2}, Landroidx/preference/Preference;->getKey()Ljava/lang/String;

    move-result-object v3

    .line 378
    .local v3, "curKey":Ljava/lang/String;
    invoke-static {v3, p1}, Landroid/text/TextUtils;->equals(Ljava/lang/CharSequence;Ljava/lang/CharSequence;)Z

    move-result v4

    if-eqz v4, :cond_1

    .line 379
    return-object v2

    .line 382
    :cond_1
    instance-of v4, v2, Landroidx/preference/PreferenceGroup;

    if-eqz v4, :cond_2

    .line 383
    move-object v4, v2

    check-cast v4, Landroidx/preference/PreferenceGroup;

    invoke-virtual {v4, p1}, Landroidx/preference/PreferenceGroup;->findPreference(Ljava/lang/CharSequence;)Landroidx/preference/Preference;

    move-result-object v4

    .line 384
    .local v4, "returnedPreference":Landroidx/preference/Preference;, "TT;"
    if-eqz v4, :cond_2

    .line 385
    return-object v4

    .line 374
    .end local v2    # "preference":Landroidx/preference/Preference;
    .end local v3    # "curKey":Ljava/lang/String;
    .end local v4    # "returnedPreference":Landroidx/preference/Preference;, "TT;"
    :cond_2
    add-int/lit8 v1, v1, 0x1

    goto :goto_0

    .line 389
    .end local v1    # "i":I
    :cond_3
    const/4 v1, 0x0

    return-object v1

    .line 368
    .end local v0    # "preferenceCount":I
    :cond_4
    new-instance v0, Ljava/lang/IllegalArgumentException;

    const-string v1, "Key cannot be null"

    invoke-direct {v0, v1}, Ljava/lang/IllegalArgumentException;-><init>(Ljava/lang/String;)V

    throw v0
.end method

.method public getInitialExpandedChildrenCount()I
    .locals 1

    .line 163
    iget v0, p0, Landroidx/preference/PreferenceGroup;->mInitialExpandedChildrenCount:I

    return v0
.end method

.method public getOnExpandButtonClickListener()Landroidx/preference/PreferenceGroup$OnExpandButtonClickListener;
    .locals 1

    .line 439
    iget-object v0, p0, Landroidx/preference/PreferenceGroup;->mOnExpandButtonClickListener:Landroidx/preference/PreferenceGroup$OnExpandButtonClickListener;

    return-object v0
.end method

.method public getPreference(I)Landroidx/preference/Preference;
    .locals 1
    .param p1, "index"    # I

    .line 189
    iget-object v0, p0, Landroidx/preference/PreferenceGroup;->mPreferences:Ljava/util/List;

    invoke-interface {v0, p1}, Ljava/util/List;->get(I)Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Landroidx/preference/Preference;

    return-object v0
.end method

.method public getPreferenceCount()I
    .locals 1

    .line 179
    iget-object v0, p0, Landroidx/preference/PreferenceGroup;->mPreferences:Ljava/util/List;

    invoke-interface {v0}, Ljava/util/List;->size()I

    move-result v0

    return v0
.end method

.method public isAttached()Z
    .locals 1

    .line 410
    iget-boolean v0, p0, Landroidx/preference/PreferenceGroup;->mAttachedToHierarchy:Z

    return v0
.end method

.method protected isOnSameScreenAsChildren()Z
    .locals 1

    .line 400
    const/4 v0, 0x1

    return v0
.end method

.method public isOrderingAsAdded()Z
    .locals 1

    .line 131
    iget-boolean v0, p0, Landroidx/preference/PreferenceGroup;->mOrderingAsAdded:Z

    return v0
.end method

.method public notifyDependencyChange(Z)V
    .locals 3
    .param p1, "disableDependents"    # Z

    .line 473
    invoke-super {p0, p1}, Landroidx/preference/Preference;->notifyDependencyChange(Z)V

    .line 477
    invoke-virtual {p0}, Landroidx/preference/PreferenceGroup;->getPreferenceCount()I

    move-result v0

    .line 478
    .local v0, "preferenceCount":I
    const/4 v1, 0x0

    .local v1, "i":I
    :goto_0
    if-ge v1, v0, :cond_0

    .line 479
    invoke-virtual {p0, v1}, Landroidx/preference/PreferenceGroup;->getPreference(I)Landroidx/preference/Preference;

    move-result-object v2

    invoke-virtual {v2, p0, p1}, Landroidx/preference/Preference;->onParentChanged(Landroidx/preference/Preference;Z)V

    .line 478
    add-int/lit8 v1, v1, 0x1

    goto :goto_0

    .line 481
    .end local v1    # "i":I
    :cond_0
    return-void
.end method

.method public onAttached()V
    .locals 3

    .line 444
    invoke-super {p0}, Landroidx/preference/Preference;->onAttached()V

    .line 448
    const/4 v0, 0x1

    iput-boolean v0, p0, Landroidx/preference/PreferenceGroup;->mAttachedToHierarchy:Z

    .line 451
    invoke-virtual {p0}, Landroidx/preference/PreferenceGroup;->getPreferenceCount()I

    move-result v0

    .line 452
    .local v0, "preferenceCount":I
    const/4 v1, 0x0

    .local v1, "i":I
    :goto_0
    if-ge v1, v0, :cond_0

    .line 453
    invoke-virtual {p0, v1}, Landroidx/preference/PreferenceGroup;->getPreference(I)Landroidx/preference/Preference;

    move-result-object v2

    invoke-virtual {v2}, Landroidx/preference/Preference;->onAttached()V

    .line 452
    add-int/lit8 v1, v1, 0x1

    goto :goto_0

    .line 455
    .end local v1    # "i":I
    :cond_0
    return-void
.end method

.method public onDetached()V
    .locals 3

    .line 459
    invoke-super {p0}, Landroidx/preference/Preference;->onDetached()V

    .line 462
    const/4 v0, 0x0

    iput-boolean v0, p0, Landroidx/preference/PreferenceGroup;->mAttachedToHierarchy:Z

    .line 465
    invoke-virtual {p0}, Landroidx/preference/PreferenceGroup;->getPreferenceCount()I

    move-result v0

    .line 466
    .local v0, "preferenceCount":I
    const/4 v1, 0x0

    .local v1, "i":I
    :goto_0
    if-ge v1, v0, :cond_0

    .line 467
    invoke-virtual {p0, v1}, Landroidx/preference/PreferenceGroup;->getPreference(I)Landroidx/preference/Preference;

    move-result-object v2

    invoke-virtual {v2}, Landroidx/preference/Preference;->onDetached()V

    .line 466
    add-int/lit8 v1, v1, 0x1

    goto :goto_0

    .line 469
    .end local v1    # "i":I
    :cond_0
    return-void
.end method

.method protected onPrepareAddPreference(Landroidx/preference/Preference;)Z
    .locals 1
    .param p1, "preference"    # Landroidx/preference/Preference;

    .line 350
    invoke-virtual {p0}, Landroidx/preference/PreferenceGroup;->shouldDisableDependents()Z

    move-result v0

    invoke-virtual {p1, p0, v0}, Landroidx/preference/Preference;->onParentChanged(Landroidx/preference/Preference;Z)V

    .line 351
    const/4 v0, 0x1

    return v0
.end method

.method protected onRestoreInstanceState(Landroid/os/Parcelable;)V
    .locals 2
    .param p1, "state"    # Landroid/os/Parcelable;

    .line 519
    if-eqz p1, :cond_1

    invoke-virtual {p1}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    move-result-object v0

    const-class v1, Landroidx/preference/PreferenceGroup$SavedState;

    invoke-virtual {v0, v1}, Ljava/lang/Object;->equals(Ljava/lang/Object;)Z

    move-result v0

    if-nez v0, :cond_0

    goto :goto_0

    .line 524
    :cond_0
    move-object v0, p1

    check-cast v0, Landroidx/preference/PreferenceGroup$SavedState;

    .line 525
    .local v0, "groupState":Landroidx/preference/PreferenceGroup$SavedState;
    iget v1, v0, Landroidx/preference/PreferenceGroup$SavedState;->mInitialExpandedChildrenCount:I

    iput v1, p0, Landroidx/preference/PreferenceGroup;->mInitialExpandedChildrenCount:I

    .line 526
    invoke-virtual {v0}, Landroidx/preference/PreferenceGroup$SavedState;->getSuperState()Landroid/os/Parcelable;

    move-result-object v1

    invoke-super {p0, v1}, Landroidx/preference/Preference;->onRestoreInstanceState(Landroid/os/Parcelable;)V

    .line 527
    return-void

    .line 521
    .end local v0    # "groupState":Landroidx/preference/PreferenceGroup$SavedState;
    :cond_1
    :goto_0
    invoke-super {p0, p1}, Landroidx/preference/Preference;->onRestoreInstanceState(Landroid/os/Parcelable;)V

    .line 522
    return-void
.end method

.method protected onSaveInstanceState()Landroid/os/Parcelable;
    .locals 3

    .line 513
    invoke-super {p0}, Landroidx/preference/Preference;->onSaveInstanceState()Landroid/os/Parcelable;

    move-result-object v0

    .line 514
    .local v0, "superState":Landroid/os/Parcelable;
    new-instance v1, Landroidx/preference/PreferenceGroup$SavedState;

    iget v2, p0, Landroidx/preference/PreferenceGroup;->mInitialExpandedChildrenCount:I

    invoke-direct {v1, v0, v2}, Landroidx/preference/PreferenceGroup$SavedState;-><init>(Landroid/os/Parcelable;I)V

    return-object v1
.end method

.method public removeAll()V
    .locals 3

    .line 334
    monitor-enter p0

    .line 335
    :try_start_0
    iget-object v0, p0, Landroidx/preference/PreferenceGroup;->mPreferences:Ljava/util/List;

    .line 336
    .local v0, "preferences":Ljava/util/List;, "Ljava/util/List<Landroidx/preference/Preference;>;"
    invoke-interface {v0}, Ljava/util/List;->size()I

    move-result v1

    add-int/lit8 v1, v1, -0x1

    .local v1, "i":I
    :goto_0
    if-ltz v1, :cond_0

    .line 337
    const/4 v2, 0x0

    invoke-interface {v0, v2}, Ljava/util/List;->get(I)Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Landroidx/preference/Preference;

    invoke-direct {p0, v2}, Landroidx/preference/PreferenceGroup;->removePreferenceInt(Landroidx/preference/Preference;)Z

    .line 336
    add-int/lit8 v1, v1, -0x1

    goto :goto_0

    .line 339
    .end local v0    # "preferences":Ljava/util/List;, "Ljava/util/List<Landroidx/preference/Preference;>;"
    .end local v1    # "i":I
    :cond_0
    monitor-exit p0
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    .line 340
    invoke-virtual {p0}, Landroidx/preference/PreferenceGroup;->notifyHierarchyChanged()V

    .line 341
    return-void

    .line 339
    :catchall_0
    move-exception v0

    :try_start_1
    monitor-exit p0
    :try_end_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    throw v0
.end method

.method public removePreference(Landroidx/preference/Preference;)Z
    .locals 1
    .param p1, "preference"    # Landroidx/preference/Preference;

    .line 274
    invoke-direct {p0, p1}, Landroidx/preference/PreferenceGroup;->removePreferenceInt(Landroidx/preference/Preference;)Z

    move-result v0

    .line 275
    .local v0, "returnValue":Z
    invoke-virtual {p0}, Landroidx/preference/PreferenceGroup;->notifyHierarchyChanged()V

    .line 276
    return v0
.end method

.method public removePreferenceRecursively(Ljava/lang/CharSequence;)Z
    .locals 2
    .param p1, "key"    # Ljava/lang/CharSequence;

    .line 289
    invoke-virtual {p0, p1}, Landroidx/preference/PreferenceGroup;->findPreference(Ljava/lang/CharSequence;)Landroidx/preference/Preference;

    move-result-object v0

    .line 290
    .local v0, "preference":Landroidx/preference/Preference;
    if-nez v0, :cond_0

    .line 291
    const/4 v1, 0x0

    return v1

    .line 293
    :cond_0
    invoke-virtual {v0}, Landroidx/preference/Preference;->getParent()Landroidx/preference/PreferenceGroup;

    move-result-object v1

    invoke-virtual {v1, v0}, Landroidx/preference/PreferenceGroup;->removePreference(Landroidx/preference/Preference;)Z

    move-result v1

    return v1
.end method

.method public setInitialExpandedChildrenCount(I)V
    .locals 2
    .param p1, "expandedCount"    # I

    .line 149
    const v0, 0x7fffffff

    if-eq p1, v0, :cond_0

    invoke-virtual {p0}, Landroidx/preference/PreferenceGroup;->hasKey()Z

    move-result v0

    if-nez v0, :cond_0

    .line 150
    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    invoke-virtual {p0}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    move-result-object v1

    invoke-virtual {v1}, Ljava/lang/Class;->getSimpleName()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v0

    const-string v1, " should have a key defined if it contains an expandable preference"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v0

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v0

    const-string v1, "PreferenceGroup"

    invoke-static {v1, v0}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;)I

    .line 153
    :cond_0
    iput p1, p0, Landroidx/preference/PreferenceGroup;->mInitialExpandedChildrenCount:I

    .line 154
    return-void
.end method

.method public setOnExpandButtonClickListener(Landroidx/preference/PreferenceGroup$OnExpandButtonClickListener;)V
    .locals 0
    .param p1, "onExpandButtonClickListener"    # Landroidx/preference/PreferenceGroup$OnExpandButtonClickListener;

    .line 425
    iput-object p1, p0, Landroidx/preference/PreferenceGroup;->mOnExpandButtonClickListener:Landroidx/preference/PreferenceGroup$OnExpandButtonClickListener;

    .line 426
    return-void
.end method

.method public setOrderingAsAdded(Z)V
    .locals 0
    .param p1, "orderingAsAdded"    # Z

    .line 121
    iput-boolean p1, p0, Landroidx/preference/PreferenceGroup;->mOrderingAsAdded:Z

    .line 122
    return-void
.end method

.method sortPreferences()V
    .locals 1

    .line 484
    monitor-enter p0

    .line 485
    :try_start_0
    iget-object v0, p0, Landroidx/preference/PreferenceGroup;->mPreferences:Ljava/util/List;

    invoke-static {v0}, Ljava/util/Collections;->sort(Ljava/util/List;)V

    .line 486
    monitor-exit p0

    .line 487
    return-void

    .line 486
    :catchall_0
    move-exception v0

    monitor-exit p0
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    throw v0
.end method
