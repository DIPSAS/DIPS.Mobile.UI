.class Landroidx/navigation/NavDestination$DeepLinkMatch;
.super Ljava/lang/Object;
.source "NavDestination.java"

# interfaces
.implements Ljava/lang/Comparable;


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Landroidx/navigation/NavDestination;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x8
    name = "DeepLinkMatch"
.end annotation

.annotation system Ldalvik/annotation/Signature;
    value = {
        "Ljava/lang/Object;",
        "Ljava/lang/Comparable<",
        "Landroidx/navigation/NavDestination$DeepLinkMatch;",
        ">;"
    }
.end annotation


# instance fields
.field private final mDestination:Landroidx/navigation/NavDestination;

.field private final mHasMatchingAction:Z

.field private final mIsExactDeepLink:Z

.field private final mMatchingArgs:Landroid/os/Bundle;

.field private final mMimeTypeMatchLevel:I


# direct methods
.method constructor <init>(Landroidx/navigation/NavDestination;Landroid/os/Bundle;ZZI)V
    .locals 0
    .param p1, "destination"    # Landroidx/navigation/NavDestination;
    .param p2, "matchingArgs"    # Landroid/os/Bundle;
    .param p3, "isExactDeepLink"    # Z
    .param p4, "hasMatchingAction"    # Z
    .param p5, "mimeTypeMatchLevel"    # I

    .line 86
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 87
    iput-object p1, p0, Landroidx/navigation/NavDestination$DeepLinkMatch;->mDestination:Landroidx/navigation/NavDestination;

    .line 88
    iput-object p2, p0, Landroidx/navigation/NavDestination$DeepLinkMatch;->mMatchingArgs:Landroid/os/Bundle;

    .line 89
    iput-boolean p3, p0, Landroidx/navigation/NavDestination$DeepLinkMatch;->mIsExactDeepLink:Z

    .line 90
    iput-boolean p4, p0, Landroidx/navigation/NavDestination$DeepLinkMatch;->mHasMatchingAction:Z

    .line 91
    iput p5, p0, Landroidx/navigation/NavDestination$DeepLinkMatch;->mMimeTypeMatchLevel:I

    .line 92
    return-void
.end method


# virtual methods
.method public compareTo(Landroidx/navigation/NavDestination$DeepLinkMatch;)I
    .locals 4
    .param p1, "other"    # Landroidx/navigation/NavDestination$DeepLinkMatch;

    .line 107
    iget-boolean v0, p0, Landroidx/navigation/NavDestination$DeepLinkMatch;->mIsExactDeepLink:Z

    const/4 v1, 0x1

    if-eqz v0, :cond_0

    iget-boolean v2, p1, Landroidx/navigation/NavDestination$DeepLinkMatch;->mIsExactDeepLink:Z

    if-nez v2, :cond_0

    .line 108
    return v1

    .line 109
    :cond_0
    const/4 v2, -0x1

    if-nez v0, :cond_1

    iget-boolean v0, p1, Landroidx/navigation/NavDestination$DeepLinkMatch;->mIsExactDeepLink:Z

    if-eqz v0, :cond_1

    .line 110
    return v2

    .line 113
    :cond_1
    iget-object v0, p0, Landroidx/navigation/NavDestination$DeepLinkMatch;->mMatchingArgs:Landroid/os/Bundle;

    if-eqz v0, :cond_2

    iget-object v3, p1, Landroidx/navigation/NavDestination$DeepLinkMatch;->mMatchingArgs:Landroid/os/Bundle;

    if-nez v3, :cond_2

    .line 114
    return v1

    .line 115
    :cond_2
    if-nez v0, :cond_3

    iget-object v3, p1, Landroidx/navigation/NavDestination$DeepLinkMatch;->mMatchingArgs:Landroid/os/Bundle;

    if-eqz v3, :cond_3

    .line 116
    return v2

    .line 119
    :cond_3
    if-eqz v0, :cond_5

    .line 120
    invoke-virtual {v0}, Landroid/os/Bundle;->size()I

    move-result v0

    iget-object v3, p1, Landroidx/navigation/NavDestination$DeepLinkMatch;->mMatchingArgs:Landroid/os/Bundle;

    invoke-virtual {v3}, Landroid/os/Bundle;->size()I

    move-result v3

    sub-int/2addr v0, v3

    .line 121
    .local v0, "sizeDifference":I
    if-lez v0, :cond_4

    .line 122
    return v1

    .line 123
    :cond_4
    if-gez v0, :cond_5

    .line 124
    return v2

    .line 128
    .end local v0    # "sizeDifference":I
    :cond_5
    iget-boolean v0, p0, Landroidx/navigation/NavDestination$DeepLinkMatch;->mHasMatchingAction:Z

    if-eqz v0, :cond_6

    iget-boolean v3, p1, Landroidx/navigation/NavDestination$DeepLinkMatch;->mHasMatchingAction:Z

    if-nez v3, :cond_6

    .line 129
    return v1

    .line 130
    :cond_6
    if-nez v0, :cond_7

    iget-boolean v0, p1, Landroidx/navigation/NavDestination$DeepLinkMatch;->mHasMatchingAction:Z

    if-eqz v0, :cond_7

    .line 131
    return v2

    .line 134
    :cond_7
    iget v0, p0, Landroidx/navigation/NavDestination$DeepLinkMatch;->mMimeTypeMatchLevel:I

    iget v1, p1, Landroidx/navigation/NavDestination$DeepLinkMatch;->mMimeTypeMatchLevel:I

    sub-int/2addr v0, v1

    return v0
.end method

.method public bridge synthetic compareTo(Ljava/lang/Object;)I
    .locals 0

    .line 75
    check-cast p1, Landroidx/navigation/NavDestination$DeepLinkMatch;

    invoke-virtual {p0, p1}, Landroidx/navigation/NavDestination$DeepLinkMatch;->compareTo(Landroidx/navigation/NavDestination$DeepLinkMatch;)I

    move-result p1

    return p1
.end method

.method getDestination()Landroidx/navigation/NavDestination;
    .locals 1

    .line 96
    iget-object v0, p0, Landroidx/navigation/NavDestination$DeepLinkMatch;->mDestination:Landroidx/navigation/NavDestination;

    return-object v0
.end method

.method getMatchingArgs()Landroid/os/Bundle;
    .locals 1

    .line 101
    iget-object v0, p0, Landroidx/navigation/NavDestination$DeepLinkMatch;->mMatchingArgs:Landroid/os/Bundle;

    return-object v0
.end method
