.class public final Landroidx/navigation/NavDeepLinkBuilder;
.super Ljava/lang/Object;
.source "NavDeepLinkBuilder.java"


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Landroidx/navigation/NavDeepLinkBuilder$PermissiveNavigatorProvider;
    }
.end annotation


# instance fields
.field private mArgs:Landroid/os/Bundle;

.field private final mContext:Landroid/content/Context;

.field private mDestId:I

.field private mGraph:Landroidx/navigation/NavGraph;

.field private final mIntent:Landroid/content/Intent;


# direct methods
.method public constructor <init>(Landroid/content/Context;)V
    .locals 2
    .param p1, "context"    # Landroid/content/Context;

    .line 71
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 72
    iput-object p1, p0, Landroidx/navigation/NavDeepLinkBuilder;->mContext:Landroid/content/Context;

    .line 73
    instance-of v0, p1, Landroid/app/Activity;

    if-eqz v0, :cond_0

    .line 74
    new-instance v0, Landroid/content/Intent;

    invoke-virtual {p1}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    move-result-object v1

    invoke-direct {v0, p1, v1}, Landroid/content/Intent;-><init>(Landroid/content/Context;Ljava/lang/Class;)V

    iput-object v0, p0, Landroidx/navigation/NavDeepLinkBuilder;->mIntent:Landroid/content/Intent;

    goto :goto_1

    .line 76
    :cond_0
    invoke-virtual {p1}, Landroid/content/Context;->getPackageManager()Landroid/content/pm/PackageManager;

    move-result-object v0

    .line 77
    invoke-virtual {p1}, Landroid/content/Context;->getPackageName()Ljava/lang/String;

    move-result-object v1

    .line 76
    invoke-virtual {v0, v1}, Landroid/content/pm/PackageManager;->getLaunchIntentForPackage(Ljava/lang/String;)Landroid/content/Intent;

    move-result-object v0

    .line 78
    .local v0, "launchIntent":Landroid/content/Intent;
    if-eqz v0, :cond_1

    move-object v1, v0

    goto :goto_0

    :cond_1
    new-instance v1, Landroid/content/Intent;

    invoke-direct {v1}, Landroid/content/Intent;-><init>()V

    :goto_0
    iput-object v1, p0, Landroidx/navigation/NavDeepLinkBuilder;->mIntent:Landroid/content/Intent;

    .line 80
    .end local v0    # "launchIntent":Landroid/content/Intent;
    :goto_1
    iget-object v0, p0, Landroidx/navigation/NavDeepLinkBuilder;->mIntent:Landroid/content/Intent;

    const v1, 0x10008000

    invoke-virtual {v0, v1}, Landroid/content/Intent;->addFlags(I)Landroid/content/Intent;

    .line 81
    return-void
.end method

.method constructor <init>(Landroidx/navigation/NavController;)V
    .locals 1
    .param p1, "navController"    # Landroidx/navigation/NavController;

    .line 87
    invoke-virtual {p1}, Landroidx/navigation/NavController;->getContext()Landroid/content/Context;

    move-result-object v0

    invoke-direct {p0, v0}, Landroidx/navigation/NavDeepLinkBuilder;-><init>(Landroid/content/Context;)V

    .line 88
    invoke-virtual {p1}, Landroidx/navigation/NavController;->getGraph()Landroidx/navigation/NavGraph;

    move-result-object v0

    iput-object v0, p0, Landroidx/navigation/NavDeepLinkBuilder;->mGraph:Landroidx/navigation/NavGraph;

    .line 89
    return-void
.end method

.method private fillInIntent()V
    .locals 6

    .line 165
    const/4 v0, 0x0

    .line 166
    .local v0, "node":Landroidx/navigation/NavDestination;
    new-instance v1, Ljava/util/ArrayDeque;

    invoke-direct {v1}, Ljava/util/ArrayDeque;-><init>()V

    .line 167
    .local v1, "possibleDestinations":Ljava/util/ArrayDeque;, "Ljava/util/ArrayDeque<Landroidx/navigation/NavDestination;>;"
    iget-object v2, p0, Landroidx/navigation/NavDeepLinkBuilder;->mGraph:Landroidx/navigation/NavGraph;

    invoke-virtual {v1, v2}, Ljava/util/ArrayDeque;->add(Ljava/lang/Object;)Z

    .line 168
    :goto_0
    invoke-virtual {v1}, Ljava/util/ArrayDeque;->isEmpty()Z

    move-result v2

    if-nez v2, :cond_2

    if-nez v0, :cond_2

    .line 169
    invoke-virtual {v1}, Ljava/util/ArrayDeque;->poll()Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Landroidx/navigation/NavDestination;

    .line 170
    .local v2, "destination":Landroidx/navigation/NavDestination;
    invoke-virtual {v2}, Landroidx/navigation/NavDestination;->getId()I

    move-result v3

    iget v4, p0, Landroidx/navigation/NavDeepLinkBuilder;->mDestId:I

    if-ne v3, v4, :cond_0

    .line 171
    move-object v0, v2

    goto :goto_2

    .line 172
    :cond_0
    instance-of v3, v2, Landroidx/navigation/NavGraph;

    if-eqz v3, :cond_1

    .line 173
    move-object v3, v2

    check-cast v3, Landroidx/navigation/NavGraph;

    invoke-virtual {v3}, Landroidx/navigation/NavGraph;->iterator()Ljava/util/Iterator;

    move-result-object v3

    :goto_1
    invoke-interface {v3}, Ljava/util/Iterator;->hasNext()Z

    move-result v4

    if-eqz v4, :cond_1

    invoke-interface {v3}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v4

    check-cast v4, Landroidx/navigation/NavDestination;

    .line 174
    .local v4, "child":Landroidx/navigation/NavDestination;
    invoke-virtual {v1, v4}, Ljava/util/ArrayDeque;->add(Ljava/lang/Object;)Z

    .line 175
    .end local v4    # "child":Landroidx/navigation/NavDestination;
    goto :goto_1

    .line 177
    .end local v2    # "destination":Landroidx/navigation/NavDestination;
    :cond_1
    :goto_2
    goto :goto_0

    .line 178
    :cond_2
    if-eqz v0, :cond_3

    .line 183
    iget-object v2, p0, Landroidx/navigation/NavDeepLinkBuilder;->mIntent:Landroid/content/Intent;

    invoke-virtual {v0}, Landroidx/navigation/NavDestination;->buildDeepLinkIds()[I

    move-result-object v3

    const-string v4, "android-support-nav:controller:deepLinkIds"

    invoke-virtual {v2, v4, v3}, Landroid/content/Intent;->putExtra(Ljava/lang/String;[I)Landroid/content/Intent;

    .line 184
    return-void

    .line 179
    :cond_3
    iget-object v2, p0, Landroidx/navigation/NavDeepLinkBuilder;->mContext:Landroid/content/Context;

    iget v3, p0, Landroidx/navigation/NavDeepLinkBuilder;->mDestId:I

    invoke-static {v2, v3}, Landroidx/navigation/NavDestination;->getDisplayName(Landroid/content/Context;I)Ljava/lang/String;

    move-result-object v2

    .line 180
    .local v2, "dest":Ljava/lang/String;
    new-instance v3, Ljava/lang/IllegalArgumentException;

    new-instance v4, Ljava/lang/StringBuilder;

    invoke-direct {v4}, Ljava/lang/StringBuilder;-><init>()V

    const-string v5, "Navigation destination "

    invoke-virtual {v4, v5}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v4

    invoke-virtual {v4, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v4

    const-string v5, " cannot be found in the navigation graph "

    invoke-virtual {v4, v5}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v4

    iget-object v5, p0, Landroidx/navigation/NavDeepLinkBuilder;->mGraph:Landroidx/navigation/NavGraph;

    invoke-virtual {v4, v5}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    move-result-object v4

    invoke-virtual {v4}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v4

    invoke-direct {v3, v4}, Ljava/lang/IllegalArgumentException;-><init>(Ljava/lang/String;)V

    throw v3
.end method


# virtual methods
.method public createPendingIntent()Landroid/app/PendingIntent;
    .locals 6

    .line 251
    const/4 v0, 0x0

    .line 252
    .local v0, "requestCode":I
    iget-object v1, p0, Landroidx/navigation/NavDeepLinkBuilder;->mArgs:Landroid/os/Bundle;

    if-eqz v1, :cond_1

    .line 253
    invoke-virtual {v1}, Landroid/os/Bundle;->keySet()Ljava/util/Set;

    move-result-object v1

    invoke-interface {v1}, Ljava/util/Set;->iterator()Ljava/util/Iterator;

    move-result-object v1

    :goto_0
    invoke-interface {v1}, Ljava/util/Iterator;->hasNext()Z

    move-result v2

    if-eqz v2, :cond_1

    invoke-interface {v1}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Ljava/lang/String;

    .line 254
    .local v2, "key":Ljava/lang/String;
    iget-object v3, p0, Landroidx/navigation/NavDeepLinkBuilder;->mArgs:Landroid/os/Bundle;

    invoke-virtual {v3, v2}, Landroid/os/Bundle;->get(Ljava/lang/String;)Ljava/lang/Object;

    move-result-object v3

    .line 255
    .local v3, "value":Ljava/lang/Object;
    mul-int/lit8 v4, v0, 0x1f

    if-eqz v3, :cond_0

    invoke-virtual {v3}, Ljava/lang/Object;->hashCode()I

    move-result v5

    goto :goto_1

    :cond_0
    const/4 v5, 0x0

    :goto_1
    add-int v0, v4, v5

    .line 256
    .end local v2    # "key":Ljava/lang/String;
    .end local v3    # "value":Ljava/lang/Object;
    goto :goto_0

    .line 258
    :cond_1
    mul-int/lit8 v1, v0, 0x1f

    iget v2, p0, Landroidx/navigation/NavDeepLinkBuilder;->mDestId:I

    add-int/2addr v1, v2

    .line 259
    .end local v0    # "requestCode":I
    .local v1, "requestCode":I
    invoke-virtual {p0}, Landroidx/navigation/NavDeepLinkBuilder;->createTaskStackBuilder()Landroidx/core/app/TaskStackBuilder;

    move-result-object v0

    const/high16 v2, 0x8000000

    .line 260
    invoke-virtual {v0, v1, v2}, Landroidx/core/app/TaskStackBuilder;->getPendingIntent(II)Landroid/app/PendingIntent;

    move-result-object v0

    .line 259
    return-object v0
.end method

.method public createTaskStackBuilder()Landroidx/core/app/TaskStackBuilder;
    .locals 5

    .line 213
    iget-object v0, p0, Landroidx/navigation/NavDeepLinkBuilder;->mIntent:Landroid/content/Intent;

    const-string v1, "android-support-nav:controller:deepLinkIds"

    invoke-virtual {v0, v1}, Landroid/content/Intent;->getIntArrayExtra(Ljava/lang/String;)[I

    move-result-object v0

    if-nez v0, :cond_1

    .line 214
    iget-object v0, p0, Landroidx/navigation/NavDeepLinkBuilder;->mGraph:Landroidx/navigation/NavGraph;

    if-nez v0, :cond_0

    .line 215
    new-instance v0, Ljava/lang/IllegalStateException;

    const-string v1, "You must call setGraph() before constructing the deep link"

    invoke-direct {v0, v1}, Ljava/lang/IllegalStateException;-><init>(Ljava/lang/String;)V

    throw v0

    .line 218
    :cond_0
    new-instance v0, Ljava/lang/IllegalStateException;

    const-string v1, "You must call setDestination() before constructing the deep link"

    invoke-direct {v0, v1}, Ljava/lang/IllegalStateException;-><init>(Ljava/lang/String;)V

    throw v0

    .line 225
    :cond_1
    iget-object v0, p0, Landroidx/navigation/NavDeepLinkBuilder;->mContext:Landroid/content/Context;

    invoke-static {v0}, Landroidx/core/app/TaskStackBuilder;->create(Landroid/content/Context;)Landroidx/core/app/TaskStackBuilder;

    move-result-object v0

    new-instance v1, Landroid/content/Intent;

    iget-object v2, p0, Landroidx/navigation/NavDeepLinkBuilder;->mIntent:Landroid/content/Intent;

    invoke-direct {v1, v2}, Landroid/content/Intent;-><init>(Landroid/content/Intent;)V

    .line 226
    invoke-virtual {v0, v1}, Landroidx/core/app/TaskStackBuilder;->addNextIntentWithParentStack(Landroid/content/Intent;)Landroidx/core/app/TaskStackBuilder;

    move-result-object v0

    .line 227
    .local v0, "taskStackBuilder":Landroidx/core/app/TaskStackBuilder;
    const/4 v1, 0x0

    .local v1, "index":I
    :goto_0
    invoke-virtual {v0}, Landroidx/core/app/TaskStackBuilder;->getIntentCount()I

    move-result v2

    if-ge v1, v2, :cond_2

    .line 230
    invoke-virtual {v0, v1}, Landroidx/core/app/TaskStackBuilder;->editIntentAt(I)Landroid/content/Intent;

    move-result-object v2

    iget-object v3, p0, Landroidx/navigation/NavDeepLinkBuilder;->mIntent:Landroid/content/Intent;

    .line 231
    const-string v4, "android-support-nav:controller:deepLinkIntent"

    invoke-virtual {v2, v4, v3}, Landroid/content/Intent;->putExtra(Ljava/lang/String;Landroid/os/Parcelable;)Landroid/content/Intent;

    .line 227
    add-int/lit8 v1, v1, 0x1

    goto :goto_0

    .line 233
    .end local v1    # "index":I
    :cond_2
    return-object v0
.end method

.method public setArguments(Landroid/os/Bundle;)Landroidx/navigation/NavDeepLinkBuilder;
    .locals 2
    .param p1, "args"    # Landroid/os/Bundle;

    .line 193
    iput-object p1, p0, Landroidx/navigation/NavDeepLinkBuilder;->mArgs:Landroid/os/Bundle;

    .line 194
    iget-object v0, p0, Landroidx/navigation/NavDeepLinkBuilder;->mIntent:Landroid/content/Intent;

    const-string v1, "android-support-nav:controller:deepLinkExtras"

    invoke-virtual {v0, v1, p1}, Landroid/content/Intent;->putExtra(Ljava/lang/String;Landroid/os/Bundle;)Landroid/content/Intent;

    .line 195
    return-object p0
.end method

.method public setComponentName(Landroid/content/ComponentName;)Landroidx/navigation/NavDeepLinkBuilder;
    .locals 1
    .param p1, "componentName"    # Landroid/content/ComponentName;

    .line 114
    iget-object v0, p0, Landroidx/navigation/NavDeepLinkBuilder;->mIntent:Landroid/content/Intent;

    invoke-virtual {v0, p1}, Landroid/content/Intent;->setComponent(Landroid/content/ComponentName;)Landroid/content/Intent;

    .line 115
    return-object p0
.end method

.method public setComponentName(Ljava/lang/Class;)Landroidx/navigation/NavDeepLinkBuilder;
    .locals 2
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/lang/Class<",
            "+",
            "Landroid/app/Activity;",
            ">;)",
            "Landroidx/navigation/NavDeepLinkBuilder;"
        }
    .end annotation

    .line 101
    .local p1, "activityClass":Ljava/lang/Class;, "Ljava/lang/Class<+Landroid/app/Activity;>;"
    new-instance v0, Landroid/content/ComponentName;

    iget-object v1, p0, Landroidx/navigation/NavDeepLinkBuilder;->mContext:Landroid/content/Context;

    invoke-direct {v0, v1, p1}, Landroid/content/ComponentName;-><init>(Landroid/content/Context;Ljava/lang/Class;)V

    invoke-virtual {p0, v0}, Landroidx/navigation/NavDeepLinkBuilder;->setComponentName(Landroid/content/ComponentName;)Landroidx/navigation/NavDeepLinkBuilder;

    move-result-object v0

    return-object v0
.end method

.method public setDestination(I)Landroidx/navigation/NavDeepLinkBuilder;
    .locals 1
    .param p1, "destId"    # I

    .line 157
    iput p1, p0, Landroidx/navigation/NavDeepLinkBuilder;->mDestId:I

    .line 158
    iget-object v0, p0, Landroidx/navigation/NavDeepLinkBuilder;->mGraph:Landroidx/navigation/NavGraph;

    if-eqz v0, :cond_0

    .line 159
    invoke-direct {p0}, Landroidx/navigation/NavDeepLinkBuilder;->fillInIntent()V

    .line 161
    :cond_0
    return-object p0
.end method

.method public setGraph(I)Landroidx/navigation/NavDeepLinkBuilder;
    .locals 3
    .param p1, "navGraphId"    # I

    .line 126
    new-instance v0, Landroidx/navigation/NavInflater;

    iget-object v1, p0, Landroidx/navigation/NavDeepLinkBuilder;->mContext:Landroid/content/Context;

    new-instance v2, Landroidx/navigation/NavDeepLinkBuilder$PermissiveNavigatorProvider;

    invoke-direct {v2}, Landroidx/navigation/NavDeepLinkBuilder$PermissiveNavigatorProvider;-><init>()V

    invoke-direct {v0, v1, v2}, Landroidx/navigation/NavInflater;-><init>(Landroid/content/Context;Landroidx/navigation/NavigatorProvider;)V

    .line 127
    invoke-virtual {v0, p1}, Landroidx/navigation/NavInflater;->inflate(I)Landroidx/navigation/NavGraph;

    move-result-object v0

    .line 126
    invoke-virtual {p0, v0}, Landroidx/navigation/NavDeepLinkBuilder;->setGraph(Landroidx/navigation/NavGraph;)Landroidx/navigation/NavDeepLinkBuilder;

    move-result-object v0

    return-object v0
.end method

.method public setGraph(Landroidx/navigation/NavGraph;)Landroidx/navigation/NavDeepLinkBuilder;
    .locals 1
    .param p1, "navGraph"    # Landroidx/navigation/NavGraph;

    .line 142
    iput-object p1, p0, Landroidx/navigation/NavDeepLinkBuilder;->mGraph:Landroidx/navigation/NavGraph;

    .line 143
    iget v0, p0, Landroidx/navigation/NavDeepLinkBuilder;->mDestId:I

    if-eqz v0, :cond_0

    .line 144
    invoke-direct {p0}, Landroidx/navigation/NavDeepLinkBuilder;->fillInIntent()V

    .line 146
    :cond_0
    return-object p0
.end method
