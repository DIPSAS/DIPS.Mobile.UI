.class public final Landroidx/navigation/ui/AppBarConfiguration$Builder;
.super Ljava/lang/Object;
.source "AppBarConfiguration.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Landroidx/navigation/ui/AppBarConfiguration;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x19
    name = "Builder"
.end annotation


# instance fields
.field private mFallbackOnNavigateUpListener:Landroidx/navigation/ui/AppBarConfiguration$OnNavigateUpListener;

.field private mOpenableLayout:Landroidx/customview/widget/Openable;

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
.method public constructor <init>(Landroid/view/Menu;)V
    .locals 5
    .param p1, "topLevelMenu"    # Landroid/view/Menu;

    .line 153
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 121
    new-instance v0, Ljava/util/HashSet;

    invoke-direct {v0}, Ljava/util/HashSet;-><init>()V

    iput-object v0, p0, Landroidx/navigation/ui/AppBarConfiguration$Builder;->mTopLevelDestinations:Ljava/util/Set;

    .line 154
    invoke-interface {p1}, Landroid/view/Menu;->size()I

    move-result v0

    .line 155
    .local v0, "size":I
    const/4 v1, 0x0

    .local v1, "index":I
    :goto_0
    if-ge v1, v0, :cond_0

    .line 156
    invoke-interface {p1, v1}, Landroid/view/Menu;->getItem(I)Landroid/view/MenuItem;

    move-result-object v2

    .line 157
    .local v2, "item":Landroid/view/MenuItem;
    iget-object v3, p0, Landroidx/navigation/ui/AppBarConfiguration$Builder;->mTopLevelDestinations:Ljava/util/Set;

    invoke-interface {v2}, Landroid/view/MenuItem;->getItemId()I

    move-result v4

    invoke-static {v4}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v4

    invoke-interface {v3, v4}, Ljava/util/Set;->add(Ljava/lang/Object;)Z

    .line 155
    .end local v2    # "item":Landroid/view/MenuItem;
    add-int/lit8 v1, v1, 0x1

    goto :goto_0

    .line 159
    .end local v1    # "index":I
    :cond_0
    return-void
.end method

.method public constructor <init>(Landroidx/navigation/NavGraph;)V
    .locals 2
    .param p1, "navGraph"    # Landroidx/navigation/NavGraph;

    .line 139
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 121
    new-instance v0, Ljava/util/HashSet;

    invoke-direct {v0}, Ljava/util/HashSet;-><init>()V

    iput-object v0, p0, Landroidx/navigation/ui/AppBarConfiguration$Builder;->mTopLevelDestinations:Ljava/util/Set;

    .line 140
    invoke-static {p1}, Landroidx/navigation/ui/NavigationUI;->findStartDestination(Landroidx/navigation/NavGraph;)Landroidx/navigation/NavDestination;

    move-result-object v1

    invoke-virtual {v1}, Landroidx/navigation/NavDestination;->getId()I

    move-result v1

    invoke-static {v1}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v1

    invoke-interface {v0, v1}, Ljava/util/Set;->add(Ljava/lang/Object;)Z

    .line 141
    return-void
.end method

.method public constructor <init>(Ljava/util/Set;)V
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/Set<",
            "Ljava/lang/Integer;",
            ">;)V"
        }
    .end annotation

    .line 183
    .local p1, "topLevelDestinationIds":Ljava/util/Set;, "Ljava/util/Set<Ljava/lang/Integer;>;"
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 121
    new-instance v0, Ljava/util/HashSet;

    invoke-direct {v0}, Ljava/util/HashSet;-><init>()V

    iput-object v0, p0, Landroidx/navigation/ui/AppBarConfiguration$Builder;->mTopLevelDestinations:Ljava/util/Set;

    .line 184
    invoke-interface {v0, p1}, Ljava/util/Set;->addAll(Ljava/util/Collection;)Z

    .line 185
    return-void
.end method

.method public varargs constructor <init>([I)V
    .locals 5
    .param p1, "topLevelDestinationIds"    # [I

    .line 169
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 121
    new-instance v0, Ljava/util/HashSet;

    invoke-direct {v0}, Ljava/util/HashSet;-><init>()V

    iput-object v0, p0, Landroidx/navigation/ui/AppBarConfiguration$Builder;->mTopLevelDestinations:Ljava/util/Set;

    .line 170
    array-length v0, p1

    const/4 v1, 0x0

    :goto_0
    if-ge v1, v0, :cond_0

    aget v2, p1, v1

    .line 171
    .local v2, "destinationId":I
    iget-object v3, p0, Landroidx/navigation/ui/AppBarConfiguration$Builder;->mTopLevelDestinations:Ljava/util/Set;

    invoke-static {v2}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v4

    invoke-interface {v3, v4}, Ljava/util/Set;->add(Ljava/lang/Object;)Z

    .line 170
    .end local v2    # "destinationId":I
    add-int/lit8 v1, v1, 0x1

    goto :goto_0

    .line 173
    :cond_0
    return-void
.end method


# virtual methods
.method public build()Landroidx/navigation/ui/AppBarConfiguration;
    .locals 5

    .line 240
    new-instance v0, Landroidx/navigation/ui/AppBarConfiguration;

    iget-object v1, p0, Landroidx/navigation/ui/AppBarConfiguration$Builder;->mTopLevelDestinations:Ljava/util/Set;

    iget-object v2, p0, Landroidx/navigation/ui/AppBarConfiguration$Builder;->mOpenableLayout:Landroidx/customview/widget/Openable;

    iget-object v3, p0, Landroidx/navigation/ui/AppBarConfiguration$Builder;->mFallbackOnNavigateUpListener:Landroidx/navigation/ui/AppBarConfiguration$OnNavigateUpListener;

    const/4 v4, 0x0

    invoke-direct {v0, v1, v2, v3, v4}, Landroidx/navigation/ui/AppBarConfiguration;-><init>(Ljava/util/Set;Landroidx/customview/widget/Openable;Landroidx/navigation/ui/AppBarConfiguration$OnNavigateUpListener;Landroidx/navigation/ui/AppBarConfiguration$1;)V

    return-object v0
.end method

.method public setDrawerLayout(Landroidx/drawerlayout/widget/DrawerLayout;)Landroidx/navigation/ui/AppBarConfiguration$Builder;
    .locals 0
    .param p1, "drawerLayout"    # Landroidx/drawerlayout/widget/DrawerLayout;
    .annotation runtime Ljava/lang/Deprecated;
    .end annotation

    .line 197
    iput-object p1, p0, Landroidx/navigation/ui/AppBarConfiguration$Builder;->mOpenableLayout:Landroidx/customview/widget/Openable;

    .line 198
    return-object p0
.end method

.method public setFallbackOnNavigateUpListener(Landroidx/navigation/ui/AppBarConfiguration$OnNavigateUpListener;)Landroidx/navigation/ui/AppBarConfiguration$Builder;
    .locals 0
    .param p1, "fallbackOnNavigateUpListener"    # Landroidx/navigation/ui/AppBarConfiguration$OnNavigateUpListener;

    .line 227
    iput-object p1, p0, Landroidx/navigation/ui/AppBarConfiguration$Builder;->mFallbackOnNavigateUpListener:Landroidx/navigation/ui/AppBarConfiguration$OnNavigateUpListener;

    .line 228
    return-object p0
.end method

.method public setOpenableLayout(Landroidx/customview/widget/Openable;)Landroidx/navigation/ui/AppBarConfiguration$Builder;
    .locals 0
    .param p1, "openableLayout"    # Landroidx/customview/widget/Openable;

    .line 210
    iput-object p1, p0, Landroidx/navigation/ui/AppBarConfiguration$Builder;->mOpenableLayout:Landroidx/customview/widget/Openable;

    .line 211
    return-object p0
.end method
