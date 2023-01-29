.class public Landroidx/media/AudioAttributesImplApi26;
.super Landroidx/media/AudioAttributesImplApi21;
.source "AudioAttributesImplApi26.java"


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Landroidx/media/AudioAttributesImplApi26$Builder;
    }
.end annotation


# direct methods
.method public constructor <init>()V
    .locals 0

    .line 41
    invoke-direct {p0}, Landroidx/media/AudioAttributesImplApi21;-><init>()V

    .line 42
    return-void
.end method

.method constructor <init>(Landroid/media/AudioAttributes;)V
    .locals 1
    .param p1, "audioAttributes"    # Landroid/media/AudioAttributes;
    .annotation system Ldalvik/annotation/MethodParameters;
        accessFlags = {
            0x0
        }
        names = {
            "audioAttributes"
        }
    .end annotation

    .line 45
    const/4 v0, -0x1

    invoke-direct {p0, p1, v0}, Landroidx/media/AudioAttributesImplApi21;-><init>(Landroid/media/AudioAttributes;I)V

    .line 46
    return-void
.end method


# virtual methods
.method public getVolumeControlStream()I
    .locals 1

    .line 50
    iget-object v0, p0, Landroidx/media/AudioAttributesImplApi26;->mAudioAttributes:Landroid/media/AudioAttributes;

    invoke-virtual {v0}, Landroid/media/AudioAttributes;->getVolumeControlStream()I

    move-result v0

    return v0
.end method
