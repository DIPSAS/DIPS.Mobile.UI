.class public Landroidx/navigation/NavGraphNavigator;
.super Landroidx/navigation/Navigator;
.source "NavGraphNavigator.java"


# annotations
.annotation runtime Landroidx/navigation/Navigator$Name;
    value = "navigation"
.end annotation

.annotation system Ldalvik/annotation/Signature;
    value = {
        "Landroidx/navigation/Navigator<",
        "Landroidx/navigation/NavGraph;",
        ">;"
    }
.end annotation


# instance fields
.field private final mNavigatorProvider:Landroidx/navigation/NavigatorProvider;


# direct methods
.method public constructor <init>(Landroidx/navigation/NavigatorProvider;)V
    .locals 0
    .param p1, "navigatorProvider"    # Landroidx/navigation/NavigatorProvider;

    .line 39
    invoke-direct {p0}, Landroidx/navigation/Navigator;-><init>()V

    .line 40
    iput-object p1, p0, Landroidx/navigation/NavGraphNavigator;->mNavigatorProvider:Landroidx/navigation/NavigatorProvider;

    .line 41
    return-void
.end method


# virtual methods
.method public bridge synthetic createDestination()Landroidx/navigation/NavDestination;
    .locals 1

    .line 28
    invoke-virtual {p0}, Landroidx/navigation/NavGraphNavigator;->createDestination()Landroidx/navigation/NavGraph;

    move-result-object v0

    return-object v0
.end method

.method public createDestination()Landroidx/navigation/NavGraph;
    .locals 1

    .line 50
    new-instance v0, Landroidx/navigation/NavGraph;

    invoke-direct {v0, p0}, Landroidx/navigation/NavGraph;-><init>(Landroidx/navigation/Navigator;)V

    return-object v0
.end method

.method public bridge synthetic navigate(Landroidx/navigation/NavDestination;Landroid/os/Bundle;Landroidx/navigation/NavOptions;Landroidx/navigation/Navigator$Extras;)Landroidx/navigation/NavDestination;
    .locals 0

    .line 28
    check-cast p1, Landroidx/navigation/NavGraph;

    invoke-virtual {p0, p1, p2, p3, p4}, Landroidx/navigation/NavGraphNavigator;->navigate(Landroidx/navigation/NavGraph;Landroid/os/Bundle;Landroidx/navigation/NavOptions;Landroidx/navigation/Navigator$Extras;)Landroidx/navigation/NavDestination;

    move-result-object p1

    return-object p1
.end method

.method public navigate(Landroidx/navigation/NavGraph;Landroid/os/Bundle;Landroidx/navigation/NavOptions;Landroidx/navigation/Navigator$Extras;)Landroidx/navigation/NavDestination;
    .locals 6
    .param p1, "destination"    # Landroidx/navigation/NavGraph;
    .param p2, "args"    # Landroid/os/Bundle;
    .param p3, "navOptions"    # Landroidx/navigation/NavOptions;
    .param p4, "navigatorExtras"    # Landroidx/navigation/Navigator$Extras;

    .line 57
    invoke-virtual {p1}, Landroidx/navigation/NavGraph;->getStartDestination()I

    move-result v0

    .line 58
    .local v0, "startId":I
    if-eqz v0, :cond_1

    .line 63
    const/4 v1, 0x0

    invoke-virtual {p1, v0, v1}, Landroidx/navigation/NavGraph;->findNode(IZ)Landroidx/navigation/NavDestination;

    move-result-object v1

    .line 64
    .local v1, "startDestination":Landroidx/navigation/NavDestination;
    if-eqz v1, :cond_0

    .line 69
    iget-object v2, p0, Landroidx/navigation/NavGraphNavigator;->mNavigatorProvider:Landroidx/navigation/NavigatorProvider;

    .line 70
    invoke-virtual {v1}, Landroidx/navigation/NavDestination;->getNavigatorName()Ljava/lang/String;

    move-result-object v3

    .line 69
    invoke-virtual {v2, v3}, Landroidx/navigation/NavigatorProvider;->getNavigator(Ljava/lang/String;)Landroidx/navigation/Navigator;

    move-result-object v2

    .line 71
    .local v2, "navigator":Landroidx/navigation/Navigator;, "Landroidx/navigation/Navigator<Landroidx/navigation/NavDestination;>;"
    invoke-virtual {v1, p2}, Landroidx/navigation/NavDestination;->addInDefaultArgs(Landroid/os/Bundle;)Landroid/os/Bundle;

    move-result-object v3

    invoke-virtual {v2, v1, v3, p3, p4}, Landroidx/navigation/Navigator;->navigate(Landroidx/navigation/NavDestination;Landroid/os/Bundle;Landroidx/navigation/NavOptions;Landroidx/navigation/Navigator$Extras;)Landroidx/navigation/NavDestination;

    move-result-object v3

    return-object v3

    .line 65
    .end local v2    # "navigator":Landroidx/navigation/Navigator;, "Landroidx/navigation/Navigator<Landroidx/navigation/NavDestination;>;"
    :cond_0
    invoke-virtual {p1}, Landroidx/navigation/NavGraph;->getStartDestDisplayName()Ljava/lang/String;

    move-result-object v2

    .line 66
    .local v2, "dest":Ljava/lang/String;
    new-instance v3, Ljava/lang/IllegalArgumentException;

    new-instance v4, Ljava/lang/StringBuilder;

    invoke-direct {v4}, Ljava/lang/StringBuilder;-><init>()V

    const-string v5, "navigation destination "

    invoke-virtual {v4, v5}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v4

    invoke-virtual {v4, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v4

    const-string v5, " is not a direct child of this NavGraph"

    invoke-virtual {v4, v5}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v4

    invoke-virtual {v4}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v4

    invoke-direct {v3, v4}, Ljava/lang/IllegalArgumentException;-><init>(Ljava/lang/String;)V

    throw v3

    .line 59
    .end local v1    # "startDestination":Landroidx/navigation/NavDestination;
    .end local v2    # "dest":Ljava/lang/String;
    :cond_1
    new-instance v1, Ljava/lang/IllegalStateException;

    new-instance v2, Ljava/lang/StringBuilder;

    invoke-direct {v2}, Ljava/lang/StringBuilder;-><init>()V

    const-string v3, "no start destination defined via app:startDestination for "

    invoke-virtual {v2, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v2

    .line 61
    invoke-virtual {p1}, Landroidx/navigation/NavGraph;->getDisplayName()Ljava/lang/String;

    move-result-object v3

    invoke-virtual {v2, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v2

    invoke-virtual {v2}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v2

    invoke-direct {v1, v2}, Ljava/lang/IllegalStateException;-><init>(Ljava/lang/String;)V

    throw v1
.end method

.method public popBackStack()Z
    .locals 1

    .line 77
    const/4 v0, 0x1

    return v0
.end method
