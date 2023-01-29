.class public Landroidx/navigation/ActivityNavigator$Destination;
.super Landroidx/navigation/NavDestination;
.source "ActivityNavigator.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Landroidx/navigation/ActivityNavigator;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x9
    name = "Destination"
.end annotation


# instance fields
.field private mDataPattern:Ljava/lang/String;

.field private mIntent:Landroid/content/Intent;


# direct methods
.method public constructor <init>(Landroidx/navigation/Navigator;)V
    .locals 0
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Landroidx/navigation/Navigator<",
            "+",
            "Landroidx/navigation/ActivityNavigator$Destination;",
            ">;)V"
        }
    .end annotation

    .line 248
    .local p1, "activityNavigator":Landroidx/navigation/Navigator;, "Landroidx/navigation/Navigator<+Landroidx/navigation/ActivityNavigator$Destination;>;"
    invoke-direct {p0, p1}, Landroidx/navigation/NavDestination;-><init>(Landroidx/navigation/Navigator;)V

    .line 249
    return-void
.end method

.method public constructor <init>(Landroidx/navigation/NavigatorProvider;)V
    .locals 1
    .param p1, "navigatorProvider"    # Landroidx/navigation/NavigatorProvider;

    .line 235
    const-class v0, Landroidx/navigation/ActivityNavigator;

    invoke-virtual {p1, v0}, Landroidx/navigation/NavigatorProvider;->getNavigator(Ljava/lang/Class;)Landroidx/navigation/Navigator;

    move-result-object v0

    invoke-direct {p0, v0}, Landroidx/navigation/ActivityNavigator$Destination;-><init>(Landroidx/navigation/Navigator;)V

    .line 236
    return-void
.end method


# virtual methods
.method public final getAction()Ljava/lang/String;
    .locals 1

    .line 375
    iget-object v0, p0, Landroidx/navigation/ActivityNavigator$Destination;->mIntent:Landroid/content/Intent;

    if-nez v0, :cond_0

    .line 376
    const/4 v0, 0x0

    return-object v0

    .line 378
    :cond_0
    invoke-virtual {v0}, Landroid/content/Intent;->getAction()Ljava/lang/String;

    move-result-object v0

    return-object v0
.end method

.method public final getComponent()Landroid/content/ComponentName;
    .locals 1

    .line 350
    iget-object v0, p0, Landroidx/navigation/ActivityNavigator$Destination;->mIntent:Landroid/content/Intent;

    if-nez v0, :cond_0

    .line 351
    const/4 v0, 0x0

    return-object v0

    .line 353
    :cond_0
    invoke-virtual {v0}, Landroid/content/Intent;->getComponent()Landroid/content/ComponentName;

    move-result-object v0

    return-object v0
.end method

.method public final getData()Landroid/net/Uri;
    .locals 1

    .line 406
    iget-object v0, p0, Landroidx/navigation/ActivityNavigator$Destination;->mIntent:Landroid/content/Intent;

    if-nez v0, :cond_0

    .line 407
    const/4 v0, 0x0

    return-object v0

    .line 409
    :cond_0
    invoke-virtual {v0}, Landroid/content/Intent;->getData()Landroid/net/Uri;

    move-result-object v0

    return-object v0
.end method

.method public final getDataPattern()Ljava/lang/String;
    .locals 1

    .line 434
    iget-object v0, p0, Landroidx/navigation/ActivityNavigator$Destination;->mDataPattern:Ljava/lang/String;

    return-object v0
.end method

.method public final getIntent()Landroid/content/Intent;
    .locals 1

    .line 296
    iget-object v0, p0, Landroidx/navigation/ActivityNavigator$Destination;->mIntent:Landroid/content/Intent;

    return-object v0
.end method

.method public final getTargetPackage()Ljava/lang/String;
    .locals 1

    .line 323
    iget-object v0, p0, Landroidx/navigation/ActivityNavigator$Destination;->mIntent:Landroid/content/Intent;

    if-nez v0, :cond_0

    .line 324
    const/4 v0, 0x0

    return-object v0

    .line 326
    :cond_0
    invoke-virtual {v0}, Landroid/content/Intent;->getPackage()Ljava/lang/String;

    move-result-object v0

    return-object v0
.end method

.method public onInflate(Landroid/content/Context;Landroid/util/AttributeSet;)V
    .locals 5
    .param p1, "context"    # Landroid/content/Context;
    .param p2, "attrs"    # Landroid/util/AttributeSet;

    .line 254
    invoke-super {p0, p1, p2}, Landroidx/navigation/NavDestination;->onInflate(Landroid/content/Context;Landroid/util/AttributeSet;)V

    .line 255
    invoke-virtual {p1}, Landroid/content/Context;->getResources()Landroid/content/res/Resources;

    move-result-object v0

    sget-object v1, Landroidx/navigation/R$styleable;->ActivityNavigator:[I

    invoke-virtual {v0, p2, v1}, Landroid/content/res/Resources;->obtainAttributes(Landroid/util/AttributeSet;[I)Landroid/content/res/TypedArray;

    move-result-object v0

    .line 257
    .local v0, "a":Landroid/content/res/TypedArray;
    sget v1, Landroidx/navigation/R$styleable;->ActivityNavigator_targetPackage:I

    invoke-virtual {v0, v1}, Landroid/content/res/TypedArray;->getString(I)Ljava/lang/String;

    move-result-object v1

    .line 258
    .local v1, "targetPackage":Ljava/lang/String;
    if-eqz v1, :cond_0

    .line 259
    nop

    .line 260
    invoke-virtual {p1}, Landroid/content/Context;->getPackageName()Ljava/lang/String;

    move-result-object v2

    .line 259
    const-string v3, "${applicationId}"

    invoke-virtual {v1, v3, v2}, Ljava/lang/String;->replace(Ljava/lang/CharSequence;Ljava/lang/CharSequence;)Ljava/lang/String;

    move-result-object v1

    .line 262
    :cond_0
    invoke-virtual {p0, v1}, Landroidx/navigation/ActivityNavigator$Destination;->setTargetPackage(Ljava/lang/String;)Landroidx/navigation/ActivityNavigator$Destination;

    .line 263
    sget v2, Landroidx/navigation/R$styleable;->ActivityNavigator_android_name:I

    invoke-virtual {v0, v2}, Landroid/content/res/TypedArray;->getString(I)Ljava/lang/String;

    move-result-object v2

    .line 264
    .local v2, "className":Ljava/lang/String;
    if-eqz v2, :cond_2

    .line 265
    const/4 v3, 0x0

    invoke-virtual {v2, v3}, Ljava/lang/String;->charAt(I)C

    move-result v3

    const/16 v4, 0x2e

    if-ne v3, v4, :cond_1

    .line 266
    new-instance v3, Ljava/lang/StringBuilder;

    invoke-direct {v3}, Ljava/lang/StringBuilder;-><init>()V

    invoke-virtual {p1}, Landroid/content/Context;->getPackageName()Ljava/lang/String;

    move-result-object v4

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v3

    invoke-virtual {v3, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v3

    invoke-virtual {v3}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v2

    .line 268
    :cond_1
    new-instance v3, Landroid/content/ComponentName;

    invoke-direct {v3, p1, v2}, Landroid/content/ComponentName;-><init>(Landroid/content/Context;Ljava/lang/String;)V

    invoke-virtual {p0, v3}, Landroidx/navigation/ActivityNavigator$Destination;->setComponentName(Landroid/content/ComponentName;)Landroidx/navigation/ActivityNavigator$Destination;

    .line 270
    :cond_2
    sget v3, Landroidx/navigation/R$styleable;->ActivityNavigator_action:I

    invoke-virtual {v0, v3}, Landroid/content/res/TypedArray;->getString(I)Ljava/lang/String;

    move-result-object v3

    invoke-virtual {p0, v3}, Landroidx/navigation/ActivityNavigator$Destination;->setAction(Ljava/lang/String;)Landroidx/navigation/ActivityNavigator$Destination;

    .line 271
    sget v3, Landroidx/navigation/R$styleable;->ActivityNavigator_data:I

    invoke-virtual {v0, v3}, Landroid/content/res/TypedArray;->getString(I)Ljava/lang/String;

    move-result-object v3

    .line 272
    .local v3, "data":Ljava/lang/String;
    if-eqz v3, :cond_3

    .line 273
    invoke-static {v3}, Landroid/net/Uri;->parse(Ljava/lang/String;)Landroid/net/Uri;

    move-result-object v4

    invoke-virtual {p0, v4}, Landroidx/navigation/ActivityNavigator$Destination;->setData(Landroid/net/Uri;)Landroidx/navigation/ActivityNavigator$Destination;

    .line 275
    :cond_3
    sget v4, Landroidx/navigation/R$styleable;->ActivityNavigator_dataPattern:I

    invoke-virtual {v0, v4}, Landroid/content/res/TypedArray;->getString(I)Ljava/lang/String;

    move-result-object v4

    invoke-virtual {p0, v4}, Landroidx/navigation/ActivityNavigator$Destination;->setDataPattern(Ljava/lang/String;)Landroidx/navigation/ActivityNavigator$Destination;

    .line 276
    invoke-virtual {v0}, Landroid/content/res/TypedArray;->recycle()V

    .line 277
    return-void
.end method

.method public final setAction(Ljava/lang/String;)Landroidx/navigation/ActivityNavigator$Destination;
    .locals 1
    .param p1, "action"    # Ljava/lang/String;

    .line 363
    iget-object v0, p0, Landroidx/navigation/ActivityNavigator$Destination;->mIntent:Landroid/content/Intent;

    if-nez v0, :cond_0

    .line 364
    new-instance v0, Landroid/content/Intent;

    invoke-direct {v0}, Landroid/content/Intent;-><init>()V

    iput-object v0, p0, Landroidx/navigation/ActivityNavigator$Destination;->mIntent:Landroid/content/Intent;

    .line 366
    :cond_0
    iget-object v0, p0, Landroidx/navigation/ActivityNavigator$Destination;->mIntent:Landroid/content/Intent;

    invoke-virtual {v0, p1}, Landroid/content/Intent;->setAction(Ljava/lang/String;)Landroid/content/Intent;

    .line 367
    return-object p0
.end method

.method public final setComponentName(Landroid/content/ComponentName;)Landroidx/navigation/ActivityNavigator$Destination;
    .locals 1
    .param p1, "name"    # Landroid/content/ComponentName;

    .line 337
    iget-object v0, p0, Landroidx/navigation/ActivityNavigator$Destination;->mIntent:Landroid/content/Intent;

    if-nez v0, :cond_0

    .line 338
    new-instance v0, Landroid/content/Intent;

    invoke-direct {v0}, Landroid/content/Intent;-><init>()V

    iput-object v0, p0, Landroidx/navigation/ActivityNavigator$Destination;->mIntent:Landroid/content/Intent;

    .line 340
    :cond_0
    iget-object v0, p0, Landroidx/navigation/ActivityNavigator$Destination;->mIntent:Landroid/content/Intent;

    invoke-virtual {v0, p1}, Landroid/content/Intent;->setComponent(Landroid/content/ComponentName;)Landroid/content/Intent;

    .line 341
    return-object p0
.end method

.method public final setData(Landroid/net/Uri;)Landroidx/navigation/ActivityNavigator$Destination;
    .locals 1
    .param p1, "data"    # Landroid/net/Uri;

    .line 394
    iget-object v0, p0, Landroidx/navigation/ActivityNavigator$Destination;->mIntent:Landroid/content/Intent;

    if-nez v0, :cond_0

    .line 395
    new-instance v0, Landroid/content/Intent;

    invoke-direct {v0}, Landroid/content/Intent;-><init>()V

    iput-object v0, p0, Landroidx/navigation/ActivityNavigator$Destination;->mIntent:Landroid/content/Intent;

    .line 397
    :cond_0
    iget-object v0, p0, Landroidx/navigation/ActivityNavigator$Destination;->mIntent:Landroid/content/Intent;

    invoke-virtual {v0, p1}, Landroid/content/Intent;->setData(Landroid/net/Uri;)Landroid/content/Intent;

    .line 398
    return-object p0
.end method

.method public final setDataPattern(Ljava/lang/String;)Landroidx/navigation/ActivityNavigator$Destination;
    .locals 0
    .param p1, "dataPattern"    # Ljava/lang/String;

    .line 425
    iput-object p1, p0, Landroidx/navigation/ActivityNavigator$Destination;->mDataPattern:Ljava/lang/String;

    .line 426
    return-object p0
.end method

.method public final setIntent(Landroid/content/Intent;)Landroidx/navigation/ActivityNavigator$Destination;
    .locals 0
    .param p1, "intent"    # Landroid/content/Intent;

    .line 286
    iput-object p1, p0, Landroidx/navigation/ActivityNavigator$Destination;->mIntent:Landroid/content/Intent;

    .line 287
    return-object p0
.end method

.method public final setTargetPackage(Ljava/lang/String;)Landroidx/navigation/ActivityNavigator$Destination;
    .locals 1
    .param p1, "packageName"    # Ljava/lang/String;

    .line 311
    iget-object v0, p0, Landroidx/navigation/ActivityNavigator$Destination;->mIntent:Landroid/content/Intent;

    if-nez v0, :cond_0

    .line 312
    new-instance v0, Landroid/content/Intent;

    invoke-direct {v0}, Landroid/content/Intent;-><init>()V

    iput-object v0, p0, Landroidx/navigation/ActivityNavigator$Destination;->mIntent:Landroid/content/Intent;

    .line 314
    :cond_0
    iget-object v0, p0, Landroidx/navigation/ActivityNavigator$Destination;->mIntent:Landroid/content/Intent;

    invoke-virtual {v0, p1}, Landroid/content/Intent;->setPackage(Ljava/lang/String;)Landroid/content/Intent;

    .line 315
    return-object p0
.end method

.method supportsActions()Z
    .locals 1

    .line 439
    const/4 v0, 0x0

    return v0
.end method

.method public toString()Ljava/lang/String;
    .locals 4

    .line 445
    invoke-virtual {p0}, Landroidx/navigation/ActivityNavigator$Destination;->getComponent()Landroid/content/ComponentName;

    move-result-object v0

    .line 446
    .local v0, "componentName":Landroid/content/ComponentName;
    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    .line 447
    .local v1, "sb":Ljava/lang/StringBuilder;
    invoke-super {p0}, Landroidx/navigation/NavDestination;->toString()Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    .line 448
    if-eqz v0, :cond_0

    .line 449
    const-string v2, " class="

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    .line 450
    invoke-virtual {v0}, Landroid/content/ComponentName;->getClassName()Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    goto :goto_0

    .line 452
    :cond_0
    invoke-virtual {p0}, Landroidx/navigation/ActivityNavigator$Destination;->getAction()Ljava/lang/String;

    move-result-object v2

    .line 453
    .local v2, "action":Ljava/lang/String;
    if-eqz v2, :cond_1

    .line 454
    const-string v3, " action="

    invoke-virtual {v1, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    .line 455
    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    .line 458
    .end local v2    # "action":Ljava/lang/String;
    :cond_1
    :goto_0
    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v2

    return-object v2
.end method
