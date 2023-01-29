.class public final Landroidx/navigation/NavAction;
.super Ljava/lang/Object;
.source "NavAction.java"


# instance fields
.field private mDefaultArguments:Landroid/os/Bundle;

.field private final mDestinationId:I

.field private mNavOptions:Landroidx/navigation/NavOptions;


# direct methods
.method public constructor <init>(I)V
    .locals 1
    .param p1, "destinationId"    # I

    .line 49
    const/4 v0, 0x0

    invoke-direct {p0, p1, v0}, Landroidx/navigation/NavAction;-><init>(ILandroidx/navigation/NavOptions;)V

    .line 50
    return-void
.end method

.method public constructor <init>(ILandroidx/navigation/NavOptions;)V
    .locals 1
    .param p1, "destinationId"    # I
    .param p2, "navOptions"    # Landroidx/navigation/NavOptions;

    .line 60
    const/4 v0, 0x0

    invoke-direct {p0, p1, p2, v0}, Landroidx/navigation/NavAction;-><init>(ILandroidx/navigation/NavOptions;Landroid/os/Bundle;)V

    .line 61
    return-void
.end method

.method public constructor <init>(ILandroidx/navigation/NavOptions;Landroid/os/Bundle;)V
    .locals 0
    .param p1, "destinationId"    # I
    .param p2, "navOptions"    # Landroidx/navigation/NavOptions;
    .param p3, "defaultArgs"    # Landroid/os/Bundle;

    .line 73
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 74
    iput p1, p0, Landroidx/navigation/NavAction;->mDestinationId:I

    .line 75
    iput-object p2, p0, Landroidx/navigation/NavAction;->mNavOptions:Landroidx/navigation/NavOptions;

    .line 76
    iput-object p3, p0, Landroidx/navigation/NavAction;->mDefaultArguments:Landroid/os/Bundle;

    .line 77
    return-void
.end method


# virtual methods
.method public getDefaultArguments()Landroid/os/Bundle;
    .locals 1

    .line 109
    iget-object v0, p0, Landroidx/navigation/NavAction;->mDefaultArguments:Landroid/os/Bundle;

    return-object v0
.end method

.method public getDestinationId()I
    .locals 1

    .line 83
    iget v0, p0, Landroidx/navigation/NavAction;->mDestinationId:I

    return v0
.end method

.method public getNavOptions()Landroidx/navigation/NavOptions;
    .locals 1

    .line 100
    iget-object v0, p0, Landroidx/navigation/NavAction;->mNavOptions:Landroidx/navigation/NavOptions;

    return-object v0
.end method

.method public setDefaultArguments(Landroid/os/Bundle;)V
    .locals 0
    .param p1, "defaultArgs"    # Landroid/os/Bundle;

    .line 118
    iput-object p1, p0, Landroidx/navigation/NavAction;->mDefaultArguments:Landroid/os/Bundle;

    .line 119
    return-void
.end method

.method public setNavOptions(Landroidx/navigation/NavOptions;)V
    .locals 0
    .param p1, "navOptions"    # Landroidx/navigation/NavOptions;

    .line 92
    iput-object p1, p0, Landroidx/navigation/NavAction;->mNavOptions:Landroidx/navigation/NavOptions;

    .line 93
    return-void
.end method
