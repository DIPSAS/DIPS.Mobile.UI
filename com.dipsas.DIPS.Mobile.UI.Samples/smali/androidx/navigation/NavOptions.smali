.class public final Landroidx/navigation/NavOptions;
.super Ljava/lang/Object;
.source "NavOptions.java"


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Landroidx/navigation/NavOptions$Builder;
    }
.end annotation


# instance fields
.field private mEnterAnim:I

.field private mExitAnim:I

.field private mPopEnterAnim:I

.field private mPopExitAnim:I

.field private mPopUpTo:I

.field private mPopUpToInclusive:Z

.field private mSingleTop:Z


# direct methods
.method constructor <init>(ZIZIIII)V
    .locals 0
    .param p1, "singleTop"    # Z
    .param p2, "popUpTo"    # I
    .param p3, "popUpToInclusive"    # Z
    .param p4, "enterAnim"    # I
    .param p5, "exitAnim"    # I
    .param p6, "popEnterAnim"    # I
    .param p7, "popExitAnim"    # I

    .line 43
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 44
    iput-boolean p1, p0, Landroidx/navigation/NavOptions;->mSingleTop:Z

    .line 45
    iput p2, p0, Landroidx/navigation/NavOptions;->mPopUpTo:I

    .line 46
    iput-boolean p3, p0, Landroidx/navigation/NavOptions;->mPopUpToInclusive:Z

    .line 47
    iput p4, p0, Landroidx/navigation/NavOptions;->mEnterAnim:I

    .line 48
    iput p5, p0, Landroidx/navigation/NavOptions;->mExitAnim:I

    .line 49
    iput p6, p0, Landroidx/navigation/NavOptions;->mPopEnterAnim:I

    .line 50
    iput p7, p0, Landroidx/navigation/NavOptions;->mPopExitAnim:I

    .line 51
    return-void
.end method


# virtual methods
.method public equals(Ljava/lang/Object;)Z
    .locals 5
    .param p1, "o"    # Ljava/lang/Object;

    .line 125
    const/4 v0, 0x1

    if-ne p0, p1, :cond_0

    return v0

    .line 126
    :cond_0
    const/4 v1, 0x0

    if-eqz p1, :cond_3

    invoke-virtual {p0}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    move-result-object v2

    invoke-virtual {p1}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    move-result-object v3

    if-eq v2, v3, :cond_1

    goto :goto_1

    .line 127
    :cond_1
    move-object v2, p1

    check-cast v2, Landroidx/navigation/NavOptions;

    .line 128
    .local v2, "that":Landroidx/navigation/NavOptions;
    iget-boolean v3, p0, Landroidx/navigation/NavOptions;->mSingleTop:Z

    iget-boolean v4, v2, Landroidx/navigation/NavOptions;->mSingleTop:Z

    if-ne v3, v4, :cond_2

    iget v3, p0, Landroidx/navigation/NavOptions;->mPopUpTo:I

    iget v4, v2, Landroidx/navigation/NavOptions;->mPopUpTo:I

    if-ne v3, v4, :cond_2

    iget-boolean v3, p0, Landroidx/navigation/NavOptions;->mPopUpToInclusive:Z

    iget-boolean v4, v2, Landroidx/navigation/NavOptions;->mPopUpToInclusive:Z

    if-ne v3, v4, :cond_2

    iget v3, p0, Landroidx/navigation/NavOptions;->mEnterAnim:I

    iget v4, v2, Landroidx/navigation/NavOptions;->mEnterAnim:I

    if-ne v3, v4, :cond_2

    iget v3, p0, Landroidx/navigation/NavOptions;->mExitAnim:I

    iget v4, v2, Landroidx/navigation/NavOptions;->mExitAnim:I

    if-ne v3, v4, :cond_2

    iget v3, p0, Landroidx/navigation/NavOptions;->mPopEnterAnim:I

    iget v4, v2, Landroidx/navigation/NavOptions;->mPopEnterAnim:I

    if-ne v3, v4, :cond_2

    iget v3, p0, Landroidx/navigation/NavOptions;->mPopExitAnim:I

    iget v4, v2, Landroidx/navigation/NavOptions;->mPopExitAnim:I

    if-ne v3, v4, :cond_2

    goto :goto_0

    :cond_2
    const/4 v0, 0x0

    :goto_0
    return v0

    .line 126
    .end local v2    # "that":Landroidx/navigation/NavOptions;
    :cond_3
    :goto_1
    return v1
.end method

.method public getEnterAnim()I
    .locals 1

    .line 91
    iget v0, p0, Landroidx/navigation/NavOptions;->mEnterAnim:I

    return v0
.end method

.method public getExitAnim()I
    .locals 1

    .line 100
    iget v0, p0, Landroidx/navigation/NavOptions;->mExitAnim:I

    return v0
.end method

.method public getPopEnterAnim()I
    .locals 1

    .line 110
    iget v0, p0, Landroidx/navigation/NavOptions;->mPopEnterAnim:I

    return v0
.end method

.method public getPopExitAnim()I
    .locals 1

    .line 120
    iget v0, p0, Landroidx/navigation/NavOptions;->mPopExitAnim:I

    return v0
.end method

.method public getPopUpTo()I
    .locals 1

    .line 73
    iget v0, p0, Landroidx/navigation/NavOptions;->mPopUpTo:I

    return v0
.end method

.method public hashCode()I
    .locals 3

    .line 139
    invoke-virtual {p0}, Landroidx/navigation/NavOptions;->shouldLaunchSingleTop()Z

    move-result v0

    .line 140
    .local v0, "result":I
    mul-int/lit8 v1, v0, 0x1f

    invoke-virtual {p0}, Landroidx/navigation/NavOptions;->getPopUpTo()I

    move-result v2

    add-int/2addr v1, v2

    .line 141
    .end local v0    # "result":I
    .local v1, "result":I
    mul-int/lit8 v0, v1, 0x1f

    invoke-virtual {p0}, Landroidx/navigation/NavOptions;->isPopUpToInclusive()Z

    move-result v2

    add-int/2addr v0, v2

    .line 142
    .end local v1    # "result":I
    .restart local v0    # "result":I
    mul-int/lit8 v1, v0, 0x1f

    invoke-virtual {p0}, Landroidx/navigation/NavOptions;->getEnterAnim()I

    move-result v2

    add-int/2addr v1, v2

    .line 143
    .end local v0    # "result":I
    .restart local v1    # "result":I
    mul-int/lit8 v0, v1, 0x1f

    invoke-virtual {p0}, Landroidx/navigation/NavOptions;->getExitAnim()I

    move-result v2

    add-int/2addr v0, v2

    .line 144
    .end local v1    # "result":I
    .restart local v0    # "result":I
    mul-int/lit8 v1, v0, 0x1f

    invoke-virtual {p0}, Landroidx/navigation/NavOptions;->getPopEnterAnim()I

    move-result v2

    add-int/2addr v1, v2

    .line 145
    .end local v0    # "result":I
    .restart local v1    # "result":I
    mul-int/lit8 v0, v1, 0x1f

    invoke-virtual {p0}, Landroidx/navigation/NavOptions;->getPopExitAnim()I

    move-result v2

    add-int/2addr v0, v2

    .line 146
    .end local v1    # "result":I
    .restart local v0    # "result":I
    return v0
.end method

.method public isPopUpToInclusive()Z
    .locals 1

    .line 82
    iget-boolean v0, p0, Landroidx/navigation/NavOptions;->mPopUpToInclusive:Z

    return v0
.end method

.method public shouldLaunchSingleTop()Z
    .locals 1

    .line 61
    iget-boolean v0, p0, Landroidx/navigation/NavOptions;->mSingleTop:Z

    return v0
.end method
