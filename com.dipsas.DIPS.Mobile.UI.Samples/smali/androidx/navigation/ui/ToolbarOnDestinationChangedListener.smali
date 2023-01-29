.class Landroidx/navigation/ui/ToolbarOnDestinationChangedListener;
.super Landroidx/navigation/ui/AbstractAppBarOnDestinationChangedListener;
.source "ToolbarOnDestinationChangedListener.java"


# instance fields
.field private final mToolbarWeakReference:Ljava/lang/ref/WeakReference;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/lang/ref/WeakReference<",
            "Landroidx/appcompat/widget/Toolbar;",
            ">;"
        }
    .end annotation
.end field


# direct methods
.method constructor <init>(Landroidx/appcompat/widget/Toolbar;Landroidx/navigation/ui/AppBarConfiguration;)V
    .locals 1
    .param p1, "toolbar"    # Landroidx/appcompat/widget/Toolbar;
    .param p2, "configuration"    # Landroidx/navigation/ui/AppBarConfiguration;

    .line 46
    invoke-virtual {p1}, Landroidx/appcompat/widget/Toolbar;->getContext()Landroid/content/Context;

    move-result-object v0

    invoke-direct {p0, v0, p2}, Landroidx/navigation/ui/AbstractAppBarOnDestinationChangedListener;-><init>(Landroid/content/Context;Landroidx/navigation/ui/AppBarConfiguration;)V

    .line 47
    new-instance v0, Ljava/lang/ref/WeakReference;

    invoke-direct {v0, p1}, Ljava/lang/ref/WeakReference;-><init>(Ljava/lang/Object;)V

    iput-object v0, p0, Landroidx/navigation/ui/ToolbarOnDestinationChangedListener;->mToolbarWeakReference:Ljava/lang/ref/WeakReference;

    .line 48
    return-void
.end method


# virtual methods
.method public onDestinationChanged(Landroidx/navigation/NavController;Landroidx/navigation/NavDestination;Landroid/os/Bundle;)V
    .locals 1
    .param p1, "controller"    # Landroidx/navigation/NavController;
    .param p2, "destination"    # Landroidx/navigation/NavDestination;
    .param p3, "arguments"    # Landroid/os/Bundle;

    .line 53
    iget-object v0, p0, Landroidx/navigation/ui/ToolbarOnDestinationChangedListener;->mToolbarWeakReference:Ljava/lang/ref/WeakReference;

    invoke-virtual {v0}, Ljava/lang/ref/WeakReference;->get()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Landroidx/appcompat/widget/Toolbar;

    .line 54
    .local v0, "toolbar":Landroidx/appcompat/widget/Toolbar;
    if-nez v0, :cond_0

    .line 55
    invoke-virtual {p1, p0}, Landroidx/navigation/NavController;->removeOnDestinationChangedListener(Landroidx/navigation/NavController$OnDestinationChangedListener;)V

    .line 56
    return-void

    .line 58
    :cond_0
    invoke-super {p0, p1, p2, p3}, Landroidx/navigation/ui/AbstractAppBarOnDestinationChangedListener;->onDestinationChanged(Landroidx/navigation/NavController;Landroidx/navigation/NavDestination;Landroid/os/Bundle;)V

    .line 59
    return-void
.end method

.method protected setNavigationIcon(Landroid/graphics/drawable/Drawable;I)V
    .locals 2
    .param p1, "icon"    # Landroid/graphics/drawable/Drawable;
    .param p2, "contentDescription"    # I

    .line 69
    iget-object v0, p0, Landroidx/navigation/ui/ToolbarOnDestinationChangedListener;->mToolbarWeakReference:Ljava/lang/ref/WeakReference;

    invoke-virtual {v0}, Ljava/lang/ref/WeakReference;->get()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Landroidx/appcompat/widget/Toolbar;

    .line 70
    .local v0, "toolbar":Landroidx/appcompat/widget/Toolbar;
    if-eqz v0, :cond_1

    .line 71
    if-nez p1, :cond_0

    invoke-virtual {v0}, Landroidx/appcompat/widget/Toolbar;->getNavigationIcon()Landroid/graphics/drawable/Drawable;

    move-result-object v1

    if-eqz v1, :cond_0

    const/4 v1, 0x1

    goto :goto_0

    :cond_0
    const/4 v1, 0x0

    .line 72
    .local v1, "useTransition":Z
    :goto_0
    invoke-virtual {v0, p1}, Landroidx/appcompat/widget/Toolbar;->setNavigationIcon(Landroid/graphics/drawable/Drawable;)V

    .line 73
    invoke-virtual {v0, p2}, Landroidx/appcompat/widget/Toolbar;->setNavigationContentDescription(I)V

    .line 74
    if-eqz v1, :cond_1

    .line 75
    invoke-static {v0}, Landroidx/transition/TransitionManager;->beginDelayedTransition(Landroid/view/ViewGroup;)V

    .line 78
    .end local v1    # "useTransition":Z
    :cond_1
    return-void
.end method

.method protected setTitle(Ljava/lang/CharSequence;)V
    .locals 1
    .param p1, "title"    # Ljava/lang/CharSequence;

    .line 63
    iget-object v0, p0, Landroidx/navigation/ui/ToolbarOnDestinationChangedListener;->mToolbarWeakReference:Ljava/lang/ref/WeakReference;

    invoke-virtual {v0}, Ljava/lang/ref/WeakReference;->get()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Landroidx/appcompat/widget/Toolbar;

    invoke-virtual {v0, p1}, Landroidx/appcompat/widget/Toolbar;->setTitle(Ljava/lang/CharSequence;)V

    .line 64
    return-void
.end method
