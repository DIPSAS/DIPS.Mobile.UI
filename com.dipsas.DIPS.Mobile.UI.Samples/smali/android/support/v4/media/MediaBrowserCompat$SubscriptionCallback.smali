.class public abstract Landroid/support/v4/media/MediaBrowserCompat$SubscriptionCallback;
.super Ljava/lang/Object;
.source "MediaBrowserCompat.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Landroid/support/v4/media/MediaBrowserCompat;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x409
    name = "SubscriptionCallback"
.end annotation

.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Landroid/support/v4/media/MediaBrowserCompat$SubscriptionCallback$SubscriptionCallbackApi26;,
        Landroid/support/v4/media/MediaBrowserCompat$SubscriptionCallback$SubscriptionCallbackApi21;
    }
.end annotation


# instance fields
.field final mSubscriptionCallbackFwk:Landroid/media/browse/MediaBrowser$SubscriptionCallback;

.field mSubscriptionRef:Ljava/lang/ref/WeakReference;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/lang/ref/WeakReference<",
            "Landroid/support/v4/media/MediaBrowserCompat$Subscription;",
            ">;"
        }
    .end annotation
.end field

.field final mToken:Landroid/os/IBinder;


# direct methods
.method public constructor <init>()V
    .locals 2

    .line 723
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 724
    new-instance v0, Landroid/os/Binder;

    invoke-direct {v0}, Landroid/os/Binder;-><init>()V

    iput-object v0, p0, Landroid/support/v4/media/MediaBrowserCompat$SubscriptionCallback;->mToken:Landroid/os/IBinder;

    .line 725
    sget v0, Landroid/os/Build$VERSION;->SDK_INT:I

    const/16 v1, 0x1a

    if-lt v0, v1, :cond_0

    .line 726
    new-instance v0, Landroid/support/v4/media/MediaBrowserCompat$SubscriptionCallback$SubscriptionCallbackApi26;

    invoke-direct {v0, p0}, Landroid/support/v4/media/MediaBrowserCompat$SubscriptionCallback$SubscriptionCallbackApi26;-><init>(Landroid/support/v4/media/MediaBrowserCompat$SubscriptionCallback;)V

    iput-object v0, p0, Landroid/support/v4/media/MediaBrowserCompat$SubscriptionCallback;->mSubscriptionCallbackFwk:Landroid/media/browse/MediaBrowser$SubscriptionCallback;

    goto :goto_0

    .line 727
    :cond_0
    sget v0, Landroid/os/Build$VERSION;->SDK_INT:I

    const/16 v1, 0x15

    if-lt v0, v1, :cond_1

    .line 728
    new-instance v0, Landroid/support/v4/media/MediaBrowserCompat$SubscriptionCallback$SubscriptionCallbackApi21;

    invoke-direct {v0, p0}, Landroid/support/v4/media/MediaBrowserCompat$SubscriptionCallback$SubscriptionCallbackApi21;-><init>(Landroid/support/v4/media/MediaBrowserCompat$SubscriptionCallback;)V

    iput-object v0, p0, Landroid/support/v4/media/MediaBrowserCompat$SubscriptionCallback;->mSubscriptionCallbackFwk:Landroid/media/browse/MediaBrowser$SubscriptionCallback;

    goto :goto_0

    .line 730
    :cond_1
    const/4 v0, 0x0

    iput-object v0, p0, Landroid/support/v4/media/MediaBrowserCompat$SubscriptionCallback;->mSubscriptionCallbackFwk:Landroid/media/browse/MediaBrowser$SubscriptionCallback;

    .line 732
    :goto_0
    return-void
.end method


# virtual methods
.method public onChildrenLoaded(Ljava/lang/String;Ljava/util/List;)V
    .locals 0
    .param p1, "parentId"    # Ljava/lang/String;
    .annotation system Ldalvik/annotation/MethodParameters;
        accessFlags = {
            0x0,
            0x0
        }
        names = {
            "parentId",
            "children"
        }
    .end annotation

    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/lang/String;",
            "Ljava/util/List<",
            "Landroid/support/v4/media/MediaBrowserCompat$MediaItem;",
            ">;)V"
        }
    .end annotation

    .line 741
    .local p2, "children":Ljava/util/List;, "Ljava/util/List<Landroid/support/v4/media/MediaBrowserCompat$MediaItem;>;"
    return-void
.end method

.method public onChildrenLoaded(Ljava/lang/String;Ljava/util/List;Landroid/os/Bundle;)V
    .locals 0
    .param p1, "parentId"    # Ljava/lang/String;
    .param p3, "options"    # Landroid/os/Bundle;
    .annotation system Ldalvik/annotation/MethodParameters;
        accessFlags = {
            0x0,
            0x0,
            0x0
        }
        names = {
            "parentId",
            "children",
            "options"
        }
    .end annotation

    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/lang/String;",
            "Ljava/util/List<",
            "Landroid/support/v4/media/MediaBrowserCompat$MediaItem;",
            ">;",
            "Landroid/os/Bundle;",
            ")V"
        }
    .end annotation

    .line 754
    .local p2, "children":Ljava/util/List;, "Ljava/util/List<Landroid/support/v4/media/MediaBrowserCompat$MediaItem;>;"
    return-void
.end method

.method public onError(Ljava/lang/String;)V
    .locals 0
    .param p1, "parentId"    # Ljava/lang/String;
    .annotation system Ldalvik/annotation/MethodParameters;
        accessFlags = {
            0x0
        }
        names = {
            "parentId"
        }
    .end annotation

    .line 766
    return-void
.end method

.method public onError(Ljava/lang/String;Landroid/os/Bundle;)V
    .locals 0
    .param p1, "parentId"    # Ljava/lang/String;
    .param p2, "options"    # Landroid/os/Bundle;
    .annotation system Ldalvik/annotation/MethodParameters;
        accessFlags = {
            0x0,
            0x0
        }
        names = {
            "parentId",
            "options"
        }
    .end annotation

    .line 781
    return-void
.end method

.method setSubscription(Landroid/support/v4/media/MediaBrowserCompat$Subscription;)V
    .locals 1
    .param p1, "subscription"    # Landroid/support/v4/media/MediaBrowserCompat$Subscription;
    .annotation system Ldalvik/annotation/MethodParameters;
        accessFlags = {
            0x0
        }
        names = {
            "subscription"
        }
    .end annotation

    .line 784
    new-instance v0, Ljava/lang/ref/WeakReference;

    invoke-direct {v0, p1}, Ljava/lang/ref/WeakReference;-><init>(Ljava/lang/Object;)V

    iput-object v0, p0, Landroid/support/v4/media/MediaBrowserCompat$SubscriptionCallback;->mSubscriptionRef:Ljava/lang/ref/WeakReference;

    .line 785
    return-void
.end method
