.class public Landroidx/media/AudioAttributesImplBase;
.super Ljava/lang/Object;
.source "AudioAttributesImplBase.java"

# interfaces
.implements Landroidx/media/AudioAttributesImpl;


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Landroidx/media/AudioAttributesImplBase$Builder;
    }
.end annotation


# instance fields
.field public mContentType:I

.field public mFlags:I

.field public mLegacyStream:I

.field public mUsage:I


# direct methods
.method public constructor <init>()V
    .locals 1

    .line 91
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 63
    const/4 v0, 0x0

    iput v0, p0, Landroidx/media/AudioAttributesImplBase;->mUsage:I

    .line 69
    iput v0, p0, Landroidx/media/AudioAttributesImplBase;->mContentType:I

    .line 75
    iput v0, p0, Landroidx/media/AudioAttributesImplBase;->mFlags:I

    .line 81
    const/4 v0, -0x1

    iput v0, p0, Landroidx/media/AudioAttributesImplBase;->mLegacyStream:I

    .line 91
    return-void
.end method

.method constructor <init>(IIII)V
    .locals 1
    .param p1, "contentType"    # I
    .param p2, "flags"    # I
    .param p3, "usage"    # I
    .param p4, "legacyStream"    # I
    .annotation system Ldalvik/annotation/MethodParameters;
        accessFlags = {
            0x0,
            0x0,
            0x0,
            0x0
        }
        names = {
            "contentType",
            "flags",
            "usage",
            "legacyStream"
        }
    .end annotation

    .line 93
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 63
    const/4 v0, 0x0

    iput v0, p0, Landroidx/media/AudioAttributesImplBase;->mUsage:I

    .line 69
    iput v0, p0, Landroidx/media/AudioAttributesImplBase;->mContentType:I

    .line 75
    iput v0, p0, Landroidx/media/AudioAttributesImplBase;->mFlags:I

    .line 81
    const/4 v0, -0x1

    iput v0, p0, Landroidx/media/AudioAttributesImplBase;->mLegacyStream:I

    .line 94
    iput p1, p0, Landroidx/media/AudioAttributesImplBase;->mContentType:I

    .line 95
    iput p2, p0, Landroidx/media/AudioAttributesImplBase;->mFlags:I

    .line 96
    iput p3, p0, Landroidx/media/AudioAttributesImplBase;->mUsage:I

    .line 97
    iput p4, p0, Landroidx/media/AudioAttributesImplBase;->mLegacyStream:I

    .line 98
    return-void
.end method

.method static usageForStreamType(I)I
    .locals 1
    .param p0, "streamType"    # I
    .annotation system Ldalvik/annotation/MethodParameters;
        accessFlags = {
            0x0
        }
        names = {
            "streamType"
        }
    .end annotation

    .line 320
    const/4 v0, 0x2

    packed-switch p0, :pswitch_data_0

    .line 342
    :pswitch_0
    const/4 v0, 0x0

    return v0

    .line 339
    :pswitch_1
    const/16 v0, 0xb

    return v0

    .line 337
    :pswitch_2
    const/4 v0, 0x3

    return v0

    .line 335
    :pswitch_3
    return v0

    .line 333
    :pswitch_4
    const/4 v0, 0x5

    return v0

    .line 331
    :pswitch_5
    const/4 v0, 0x4

    return v0

    .line 329
    :pswitch_6
    const/4 v0, 0x1

    return v0

    .line 327
    :pswitch_7
    const/4 v0, 0x6

    return v0

    .line 325
    :pswitch_8
    const/16 v0, 0xd

    return v0

    .line 322
    :pswitch_9
    return v0

    :pswitch_data_0
    .packed-switch 0x0
        :pswitch_9
        :pswitch_8
        :pswitch_7
        :pswitch_6
        :pswitch_5
        :pswitch_4
        :pswitch_3
        :pswitch_8
        :pswitch_2
        :pswitch_0
        :pswitch_1
    .end packed-switch
.end method


# virtual methods
.method public equals(Ljava/lang/Object;)Z
    .locals 4
    .param p1, "o"    # Ljava/lang/Object;
    .annotation system Ldalvik/annotation/MethodParameters;
        accessFlags = {
            0x0
        }
        names = {
            "o"
        }
    .end annotation

    .line 156
    instance-of v0, p1, Landroidx/media/AudioAttributesImplBase;

    const/4 v1, 0x0

    if-nez v0, :cond_0

    .line 157
    return v1

    .line 159
    :cond_0
    move-object v0, p1

    check-cast v0, Landroidx/media/AudioAttributesImplBase;

    .line 160
    .local v0, "that":Landroidx/media/AudioAttributesImplBase;
    iget v2, p0, Landroidx/media/AudioAttributesImplBase;->mContentType:I

    invoke-virtual {v0}, Landroidx/media/AudioAttributesImplBase;->getContentType()I

    move-result v3

    if-ne v2, v3, :cond_1

    iget v2, p0, Landroidx/media/AudioAttributesImplBase;->mFlags:I

    .line 161
    invoke-virtual {v0}, Landroidx/media/AudioAttributesImplBase;->getFlags()I

    move-result v3

    if-ne v2, v3, :cond_1

    iget v2, p0, Landroidx/media/AudioAttributesImplBase;->mUsage:I

    .line 162
    invoke-virtual {v0}, Landroidx/media/AudioAttributesImplBase;->getUsage()I

    move-result v3

    if-ne v2, v3, :cond_1

    iget v2, p0, Landroidx/media/AudioAttributesImplBase;->mLegacyStream:I

    iget v3, v0, Landroidx/media/AudioAttributesImplBase;->mLegacyStream:I

    if-ne v2, v3, :cond_1

    const/4 v1, 0x1

    goto :goto_0

    :cond_1
    nop

    .line 160
    :goto_0
    return v1
.end method

.method public getAudioAttributes()Ljava/lang/Object;
    .locals 1

    .line 103
    const/4 v0, 0x0

    return-object v0
.end method

.method public getContentType()I
    .locals 1

    .line 126
    iget v0, p0, Landroidx/media/AudioAttributesImplBase;->mContentType:I

    return v0
.end method

.method public getFlags()I
    .locals 3

    .line 136
    iget v0, p0, Landroidx/media/AudioAttributesImplBase;->mFlags:I

    .line 137
    .local v0, "flags":I
    invoke-virtual {p0}, Landroidx/media/AudioAttributesImplBase;->getLegacyStreamType()I

    move-result v1

    .line 138
    .local v1, "legacyStream":I
    const/4 v2, 0x6

    if-ne v1, v2, :cond_0

    .line 139
    or-int/lit8 v0, v0, 0x4

    goto :goto_0

    .line 140
    :cond_0
    const/4 v2, 0x7

    if-ne v1, v2, :cond_1

    .line 141
    or-int/lit8 v0, v0, 0x1

    .line 143
    :cond_1
    :goto_0
    and-int/lit16 v2, v0, 0x111

    return v2
.end method

.method public getLegacyStreamType()I
    .locals 3

    .line 113
    iget v0, p0, Landroidx/media/AudioAttributesImplBase;->mLegacyStream:I

    const/4 v1, -0x1

    if-eq v0, v1, :cond_0

    .line 114
    return v0

    .line 116
    :cond_0
    const/4 v0, 0x0

    iget v1, p0, Landroidx/media/AudioAttributesImplBase;->mFlags:I

    iget v2, p0, Landroidx/media/AudioAttributesImplBase;->mUsage:I

    invoke-static {v0, v1, v2}, Landroidx/media/AudioAttributesCompat;->toVolumeStreamType(ZII)I

    move-result v0

    return v0
.end method

.method public getRawLegacyStreamType()I
    .locals 1

    .line 121
    iget v0, p0, Landroidx/media/AudioAttributesImplBase;->mLegacyStream:I

    return v0
.end method

.method public getUsage()I
    .locals 1

    .line 131
    iget v0, p0, Landroidx/media/AudioAttributesImplBase;->mUsage:I

    return v0
.end method

.method public getVolumeControlStream()I
    .locals 3

    .line 108
    iget v0, p0, Landroidx/media/AudioAttributesImplBase;->mFlags:I

    iget v1, p0, Landroidx/media/AudioAttributesImplBase;->mUsage:I

    const/4 v2, 0x1

    invoke-static {v2, v0, v1}, Landroidx/media/AudioAttributesCompat;->toVolumeStreamType(ZII)I

    move-result v0

    return v0
.end method

.method public hashCode()I
    .locals 3

    .line 151
    const/4 v0, 0x4

    new-array v0, v0, [Ljava/lang/Object;

    iget v1, p0, Landroidx/media/AudioAttributesImplBase;->mContentType:I

    invoke-static {v1}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v1

    const/4 v2, 0x0

    aput-object v1, v0, v2

    iget v1, p0, Landroidx/media/AudioAttributesImplBase;->mFlags:I

    invoke-static {v1}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v1

    const/4 v2, 0x1

    aput-object v1, v0, v2

    iget v1, p0, Landroidx/media/AudioAttributesImplBase;->mUsage:I

    invoke-static {v1}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v1

    const/4 v2, 0x2

    aput-object v1, v0, v2

    iget v1, p0, Landroidx/media/AudioAttributesImplBase;->mLegacyStream:I

    invoke-static {v1}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v1

    const/4 v2, 0x3

    aput-object v1, v0, v2

    invoke-static {v0}, Ljava/util/Arrays;->hashCode([Ljava/lang/Object;)I

    move-result v0

    return v0
.end method

.method public toString()Ljava/lang/String;
    .locals 3

    .line 169
    new-instance v0, Ljava/lang/StringBuilder;

    const-string v1, "AudioAttributesCompat:"

    invoke-direct {v0, v1}, Ljava/lang/StringBuilder;-><init>(Ljava/lang/String;)V

    .line 170
    .local v0, "sb":Ljava/lang/StringBuilder;
    iget v1, p0, Landroidx/media/AudioAttributesImplBase;->mLegacyStream:I

    const/4 v2, -0x1

    if-eq v1, v2, :cond_0

    .line 171
    const-string v1, " stream="

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v1

    iget v2, p0, Landroidx/media/AudioAttributesImplBase;->mLegacyStream:I

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    .line 172
    const-string v1, " derived"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    .line 174
    :cond_0
    const-string v1, " usage="

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v1

    iget v2, p0, Landroidx/media/AudioAttributesImplBase;->mUsage:I

    .line 175
    invoke-static {v2}, Landroidx/media/AudioAttributesCompat;->usageToString(I)Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v1

    .line 176
    const-string v2, " content="

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v1

    iget v2, p0, Landroidx/media/AudioAttributesImplBase;->mContentType:I

    .line 177
    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    move-result-object v1

    .line 178
    const-string v2, " flags=0x"

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v1

    iget v2, p0, Landroidx/media/AudioAttributesImplBase;->mFlags:I

    .line 179
    invoke-static {v2}, Ljava/lang/Integer;->toHexString(I)Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v2}, Ljava/lang/String;->toUpperCase()Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    .line 180
    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    return-object v1
.end method
