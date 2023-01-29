.class abstract Landroidx/navigation/ui/AbstractAppBarOnDestinationChangedListener;
.super Ljava/lang/Object;
.source "AbstractAppBarOnDestinationChangedListener.java"

# interfaces
.implements Landroidx/navigation/NavController$OnDestinationChangedListener;


# instance fields
.field private mAnimator:Landroid/animation/ValueAnimator;

.field private mArrowDrawable:Landroidx/appcompat/graphics/drawable/DrawerArrowDrawable;

.field private final mContext:Landroid/content/Context;

.field private final mOpenableLayoutWeakReference:Ljava/lang/ref/WeakReference;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/lang/ref/WeakReference<",
            "Landroidx/customview/widget/Openable;",
            ">;"
        }
    .end annotation
.end field

.field private final mTopLevelDestinations:Ljava/util/Set;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/Set<",
            "Ljava/lang/Integer;",
            ">;"
        }
    .end annotation
.end field


# direct methods
.method constructor <init>(Landroid/content/Context;Landroidx/navigation/ui/AppBarConfiguration;)V
    .locals 2
    .param p1, "context"    # Landroid/content/Context;
    .param p2, "configuration"    # Landroidx/navigation/ui/AppBarConfiguration;

    .line 57
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 58
    iput-object p1, p0, Landroidx/navigation/ui/AbstractAppBarOnDestinationChangedListener;->mContext:Landroid/content/Context;

    .line 59
    invoke-virtual {p2}, Landroidx/navigation/ui/AppBarConfiguration;->getTopLevelDestinations()Ljava/util/Set;

    move-result-object v0

    iput-object v0, p0, Landroidx/navigation/ui/AbstractAppBarOnDestinationChangedListener;->mTopLevelDestinations:Ljava/util/Set;

    .line 60
    invoke-virtual {p2}, Landroidx/navigation/ui/AppBarConfiguration;->getOpenableLayout()Landroidx/customview/widget/Openable;

    move-result-object v0

    .line 61
    .local v0, "openableLayout":Landroidx/customview/widget/Openable;
    if-eqz v0, :cond_0

    .line 62
    new-instance v1, Ljava/lang/ref/WeakReference;

    invoke-direct {v1, v0}, Ljava/lang/ref/WeakReference;-><init>(Ljava/lang/Object;)V

    iput-object v1, p0, Landroidx/navigation/ui/AbstractAppBarOnDestinationChangedListener;->mOpenableLayoutWeakReference:Ljava/lang/ref/WeakReference;

    goto :goto_0

    .line 64
    :cond_0
    const/4 v1, 0x0

    iput-object v1, p0, Landroidx/navigation/ui/AbstractAppBarOnDestinationChangedListener;->mOpenableLayoutWeakReference:Ljava/lang/ref/WeakReference;

    .line 66
    :goto_0
    return-void
.end method

.method private setActionBarUpIndicator(Z)V
    .locals 6
    .param p1, "showAsDrawerIndicator"    # Z

    .line 115
    const/4 v0, 0x1

    .line 116
    .local v0, "animate":Z
    iget-object v1, p0, Landroidx/navigation/ui/AbstractAppBarOnDestinationChangedListener;->mArrowDrawable:Landroidx/appcompat/graphics/drawable/DrawerArrowDrawable;

    if-nez v1, :cond_0

    .line 117
    new-instance v1, Landroidx/appcompat/graphics/drawable/DrawerArrowDrawable;

    iget-object v2, p0, Landroidx/navigation/ui/AbstractAppBarOnDestinationChangedListener;->mContext:Landroid/content/Context;

    invoke-direct {v1, v2}, Landroidx/appcompat/graphics/drawable/DrawerArrowDrawable;-><init>(Landroid/content/Context;)V

    iput-object v1, p0, Landroidx/navigation/ui/AbstractAppBarOnDestinationChangedListener;->mArrowDrawable:Landroidx/appcompat/graphics/drawable/DrawerArrowDrawable;

    .line 119
    const/4 v0, 0x0

    .line 121
    :cond_0
    iget-object v1, p0, Landroidx/navigation/ui/AbstractAppBarOnDestinationChangedListener;->mArrowDrawable:Landroidx/appcompat/graphics/drawable/DrawerArrowDrawable;

    if-eqz p1, :cond_1

    .line 122
    sget v2, Landroidx/navigation/ui/R$string;->nav_app_bar_open_drawer_description:I

    goto :goto_0

    .line 123
    :cond_1
    sget v2, Landroidx/navigation/ui/R$string;->nav_app_bar_navigate_up_description:I

    .line 121
    :goto_0
    invoke-virtual {p0, v1, v2}, Landroidx/navigation/ui/AbstractAppBarOnDestinationChangedListener;->setNavigationIcon(Landroid/graphics/drawable/Drawable;I)V

    .line 124
    if-eqz p1, :cond_2

    const/4 v1, 0x0

    goto :goto_1

    :cond_2
    const/high16 v1, 0x3f800000    # 1.0f

    .line 125
    .local v1, "endValue":F
    :goto_1
    if-eqz v0, :cond_4

    .line 126
    iget-object v2, p0, Landroidx/navigation/ui/AbstractAppBarOnDestinationChangedListener;->mArrowDrawable:Landroidx/appcompat/graphics/drawable/DrawerArrowDrawable;

    invoke-virtual {v2}, Landroidx/appcompat/graphics/drawable/DrawerArrowDrawable;->getProgress()F

    move-result v2

    .line 127
    .local v2, "startValue":F
    iget-object v3, p0, Landroidx/navigation/ui/AbstractAppBarOnDestinationChangedListener;->mAnimator:Landroid/animation/ValueAnimator;

    if-eqz v3, :cond_3

    .line 128
    invoke-virtual {v3}, Landroid/animation/ValueAnimator;->cancel()V

    .line 130
    :cond_3
    iget-object v3, p0, Landroidx/navigation/ui/AbstractAppBarOnDestinationChangedListener;->mArrowDrawable:Landroidx/appcompat/graphics/drawable/DrawerArrowDrawable;

    const/4 v4, 0x2

    new-array v4, v4, [F

    const/4 v5, 0x0

    aput v2, v4, v5

    const/4 v5, 0x1

    aput v1, v4, v5

    const-string v5, "progress"

    invoke-static {v3, v5, v4}, Landroid/animation/ObjectAnimator;->ofFloat(Ljava/lang/Object;Ljava/lang/String;[F)Landroid/animation/ObjectAnimator;

    move-result-object v3

    iput-object v3, p0, Landroidx/navigation/ui/AbstractAppBarOnDestinationChangedListener;->mAnimator:Landroid/animation/ValueAnimator;

    .line 132
    invoke-virtual {v3}, Landroid/animation/ValueAnimator;->start()V

    .line 133
    .end local v2    # "startValue":F
    goto :goto_2

    .line 134
    :cond_4
    iget-object v2, p0, Landroidx/navigation/ui/AbstractAppBarOnDestinationChangedListener;->mArrowDrawable:Landroidx/appcompat/graphics/drawable/DrawerArrowDrawable;

    invoke-virtual {v2, v1}, Landroidx/appcompat/graphics/drawable/DrawerArrowDrawable;->setProgress(F)V

    .line 136
    :goto_2
    return-void
.end method


# virtual methods
.method public onDestinationChanged(Landroidx/navigation/NavController;Landroidx/navigation/NavDestination;Landroid/os/Bundle;)V
    .locals 9
    .param p1, "controller"    # Landroidx/navigation/NavController;
    .param p2, "destination"    # Landroidx/navigation/NavDestination;
    .param p3, "arguments"    # Landroid/os/Bundle;

    .line 75
    instance-of v0, p2, Landroidx/navigation/FloatingWindow;

    if-eqz v0, :cond_0

    .line 76
    return-void

    .line 78
    :cond_0
    iget-object v0, p0, Landroidx/navigation/ui/AbstractAppBarOnDestinationChangedListener;->mOpenableLayoutWeakReference:Ljava/lang/ref/WeakReference;

    const/4 v1, 0x0

    if-eqz v0, :cond_1

    .line 79
    invoke-virtual {v0}, Ljava/lang/ref/WeakReference;->get()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Landroidx/customview/widget/Openable;

    goto :goto_0

    .line 80
    :cond_1
    move-object v0, v1

    :goto_0
    nop

    .line 81
    .local v0, "openableLayout":Landroidx/customview/widget/Openable;
    iget-object v2, p0, Landroidx/navigation/ui/AbstractAppBarOnDestinationChangedListener;->mOpenableLayoutWeakReference:Ljava/lang/ref/WeakReference;

    if-eqz v2, :cond_2

    if-nez v0, :cond_2

    .line 82
    invoke-virtual {p1, p0}, Landroidx/navigation/NavController;->removeOnDestinationChangedListener(Landroidx/navigation/NavController$OnDestinationChangedListener;)V

    .line 83
    return-void

    .line 85
    :cond_2
    invoke-virtual {p2}, Landroidx/navigation/NavDestination;->getLabel()Ljava/lang/CharSequence;

    move-result-object v2

    .line 86
    .local v2, "label":Ljava/lang/CharSequence;
    const/4 v3, 0x1

    if-eqz v2, :cond_5

    .line 88
    new-instance v4, Ljava/lang/StringBuffer;

    invoke-direct {v4}, Ljava/lang/StringBuffer;-><init>()V

    .line 89
    .local v4, "title":Ljava/lang/StringBuffer;
    const-string v5, "\\{(.+?)\\}"

    invoke-static {v5}, Ljava/util/regex/Pattern;->compile(Ljava/lang/String;)Ljava/util/regex/Pattern;

    move-result-object v5

    .line 90
    .local v5, "fillInPattern":Ljava/util/regex/Pattern;
    invoke-virtual {v5, v2}, Ljava/util/regex/Pattern;->matcher(Ljava/lang/CharSequence;)Ljava/util/regex/Matcher;

    move-result-object v6

    .line 91
    .local v6, "matcher":Ljava/util/regex/Matcher;
    :goto_1
    invoke-virtual {v6}, Ljava/util/regex/Matcher;->find()Z

    move-result v7

    if-eqz v7, :cond_4

    .line 92
    invoke-virtual {v6, v3}, Ljava/util/regex/Matcher;->group(I)Ljava/lang/String;

    move-result-object v7

    .line 93
    .local v7, "argName":Ljava/lang/String;
    if-eqz p3, :cond_3

    invoke-virtual {p3, v7}, Landroid/os/Bundle;->containsKey(Ljava/lang/String;)Z

    move-result v8

    if-eqz v8, :cond_3

    .line 94
    const-string v8, ""

    invoke-virtual {v6, v4, v8}, Ljava/util/regex/Matcher;->appendReplacement(Ljava/lang/StringBuffer;Ljava/lang/String;)Ljava/util/regex/Matcher;

    .line 96
    invoke-virtual {p3, v7}, Landroid/os/Bundle;->get(Ljava/lang/String;)Ljava/lang/Object;

    move-result-object v8

    invoke-virtual {v8}, Ljava/lang/Object;->toString()Ljava/lang/String;

    move-result-object v8

    invoke-virtual {v4, v8}, Ljava/lang/StringBuffer;->append(Ljava/lang/String;)Ljava/lang/StringBuffer;

    .line 101
    .end local v7    # "argName":Ljava/lang/String;
    goto :goto_1

    .line 98
    .restart local v7    # "argName":Ljava/lang/String;
    :cond_3
    new-instance v1, Ljava/lang/IllegalArgumentException;

    new-instance v3, Ljava/lang/StringBuilder;

    invoke-direct {v3}, Ljava/lang/StringBuilder;-><init>()V

    const-string v8, "Could not find "

    invoke-virtual {v3, v8}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v3

    invoke-virtual {v3, v7}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v3

    const-string v8, " in "

    invoke-virtual {v3, v8}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v3

    invoke-virtual {v3, p3}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    move-result-object v3

    const-string v8, " to fill label "

    invoke-virtual {v3, v8}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v3

    invoke-virtual {v3, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    move-result-object v3

    invoke-virtual {v3}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v3

    invoke-direct {v1, v3}, Ljava/lang/IllegalArgumentException;-><init>(Ljava/lang/String;)V

    throw v1

    .line 102
    .end local v7    # "argName":Ljava/lang/String;
    :cond_4
    invoke-virtual {v6, v4}, Ljava/util/regex/Matcher;->appendTail(Ljava/lang/StringBuffer;)Ljava/lang/StringBuffer;

    .line 103
    invoke-virtual {p0, v4}, Landroidx/navigation/ui/AbstractAppBarOnDestinationChangedListener;->setTitle(Ljava/lang/CharSequence;)V

    .line 105
    .end local v4    # "title":Ljava/lang/StringBuffer;
    .end local v5    # "fillInPattern":Ljava/util/regex/Pattern;
    .end local v6    # "matcher":Ljava/util/regex/Matcher;
    :cond_5
    iget-object v4, p0, Landroidx/navigation/ui/AbstractAppBarOnDestinationChangedListener;->mTopLevelDestinations:Ljava/util/Set;

    invoke-static {p2, v4}, Landroidx/navigation/ui/NavigationUI;->matchDestinations(Landroidx/navigation/NavDestination;Ljava/util/Set;)Z

    move-result v4

    .line 107
    .local v4, "isTopLevelDestination":Z
    const/4 v5, 0x0

    if-nez v0, :cond_6

    if-eqz v4, :cond_6

    .line 108
    invoke-virtual {p0, v1, v5}, Landroidx/navigation/ui/AbstractAppBarOnDestinationChangedListener;->setNavigationIcon(Landroid/graphics/drawable/Drawable;I)V

    goto :goto_3

    .line 110
    :cond_6
    if-eqz v0, :cond_7

    if-eqz v4, :cond_7

    goto :goto_2

    :cond_7
    const/4 v3, 0x0

    :goto_2
    invoke-direct {p0, v3}, Landroidx/navigation/ui/AbstractAppBarOnDestinationChangedListener;->setActionBarUpIndicator(Z)V

    .line 112
    :goto_3
    return-void
.end method

.method protected abstract setNavigationIcon(Landroid/graphics/drawable/Drawable;I)V
.end method

.method protected abstract setTitle(Ljava/lang/CharSequence;)V
.end method
