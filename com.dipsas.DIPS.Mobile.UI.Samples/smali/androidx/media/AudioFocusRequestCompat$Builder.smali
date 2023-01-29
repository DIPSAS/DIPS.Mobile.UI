.class public final Landroidx/media/AudioFocusRequestCompat$Builder;
.super Ljava/lang/Object;
.source "AudioFocusRequestCompat.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Landroidx/media/AudioFocusRequestCompat;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x19
    name = "Builder"
.end annotation


# instance fields
.field private mAudioAttributesCompat:Landroidx/media/AudioAttributesCompat;

.field private mFocusChangeHandler:Landroid/os/Handler;

.field private mFocusGain:I

.field private mOnAudioFocusChangeListener:Landroid/media/AudioManager$OnAudioFocusChangeListener;

.field private mPauseOnDuck:Z


# direct methods
.method public constructor <init>(I)V
    .locals 1
    .param p1, "focusGain"    # I
    .annotation system Ldalvik/annotation/MethodParameters;
        accessFlags = {
            0x0
        }
        names = {
            "focusGain"
        }
    .end annotation

    .line 226
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 205
    sget-object v0, Landroidx/media/AudioFocusRequestCompat;->FOCUS_DEFAULT_ATTR:Landroidx/media/AudioAttributesCompat;

    iput-object v0, p0, Landroidx/media/AudioFocusRequestCompat$Builder;->mAudioAttributesCompat:Landroidx/media/AudioAttributesCompat;

    .line 227
    invoke-virtual {p0, p1}, Landroidx/media/AudioFocusRequestCompat$Builder;->setFocusGain(I)Landroidx/media/AudioFocusRequestCompat$Builder;

    .line 228
    return-void
.end method

.method public constructor <init>(Landroidx/media/AudioFocusRequestCompat;)V
    .locals 2
    .param p1, "requestToCopy"    # Landroidx/media/AudioFocusRequestCompat;
    .annotation system Ldalvik/annotation/MethodParameters;
        accessFlags = {
            0x0
        }
        names = {
            "requestToCopy"
        }
    .end annotation

    .line 239
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 205
    sget-object v0, Landroidx/media/AudioFocusRequestCompat;->FOCUS_DEFAULT_ATTR:Landroidx/media/AudioAttributesCompat;

    iput-object v0, p0, Landroidx/media/AudioFocusRequestCompat$Builder;->mAudioAttributesCompat:Landroidx/media/AudioAttributesCompat;

    .line 240
    if-eqz p1, :cond_0

    .line 244
    invoke-virtual {p1}, Landroidx/media/AudioFocusRequestCompat;->getFocusGain()I

    move-result v0

    iput v0, p0, Landroidx/media/AudioFocusRequestCompat$Builder;->mFocusGain:I

    .line 245
    invoke-virtual {p1}, Landroidx/media/AudioFocusRequestCompat;->getOnAudioFocusChangeListener()Landroid/media/AudioManager$OnAudioFocusChangeListener;

    move-result-object v0

    iput-object v0, p0, Landroidx/media/AudioFocusRequestCompat$Builder;->mOnAudioFocusChangeListener:Landroid/media/AudioManager$OnAudioFocusChangeListener;

    .line 246
    invoke-virtual {p1}, Landroidx/media/AudioFocusRequestCompat;->getFocusChangeHandler()Landroid/os/Handler;

    move-result-object v0

    iput-object v0, p0, Landroidx/media/AudioFocusRequestCompat$Builder;->mFocusChangeHandler:Landroid/os/Handler;

    .line 247
    invoke-virtual {p1}, Landroidx/media/AudioFocusRequestCompat;->getAudioAttributesCompat()Landroidx/media/AudioAttributesCompat;

    move-result-object v0

    iput-object v0, p0, Landroidx/media/AudioFocusRequestCompat$Builder;->mAudioAttributesCompat:Landroidx/media/AudioAttributesCompat;

    .line 248
    invoke-virtual {p1}, Landroidx/media/AudioFocusRequestCompat;->willPauseWhenDucked()Z

    move-result v0

    iput-boolean v0, p0, Landroidx/media/AudioFocusRequestCompat$Builder;->mPauseOnDuck:Z

    .line 249
    return-void

    .line 241
    :cond_0
    new-instance v0, Ljava/lang/IllegalArgumentException;

    const-string v1, "AudioFocusRequestCompat to copy must not be null"

    invoke-direct {v0, v1}, Ljava/lang/IllegalArgumentException;-><init>(Ljava/lang/String;)V

    throw v0
.end method

.method private static isValidFocusGain(I)Z
    .locals 1
    .param p0, "focusGain"    # I
    .annotation system Ldalvik/annotation/MethodParameters;
        accessFlags = {
            0x0
        }
        names = {
            "focusGain"
        }
    .end annotation

    .line 387
    packed-switch p0, :pswitch_data_0

    .line 394
    const/4 v0, 0x0

    return v0

    .line 392
    :pswitch_0
    const/4 v0, 0x1

    return v0

    nop

    :pswitch_data_0
    .packed-switch 0x1
        :pswitch_0
        :pswitch_0
        :pswitch_0
        :pswitch_0
    .end packed-switch
.end method


# virtual methods
.method public build()Landroidx/media/AudioFocusRequestCompat;
    .locals 7

    .line 367
    iget-object v0, p0, Landroidx/media/AudioFocusRequestCompat$Builder;->mOnAudioFocusChangeListener:Landroid/media/AudioManager$OnAudioFocusChangeListener;

    if-eqz v0, :cond_0

    .line 372
    new-instance v0, Landroidx/media/AudioFocusRequestCompat;

    iget v2, p0, Landroidx/media/AudioFocusRequestCompat$Builder;->mFocusGain:I

    iget-object v3, p0, Landroidx/media/AudioFocusRequestCompat$Builder;->mOnAudioFocusChangeListener:Landroid/media/AudioManager$OnAudioFocusChangeListener;

    iget-object v4, p0, Landroidx/media/AudioFocusRequestCompat$Builder;->mFocusChangeHandler:Landroid/os/Handler;

    iget-object v5, p0, Landroidx/media/AudioFocusRequestCompat$Builder;->mAudioAttributesCompat:Landroidx/media/AudioAttributesCompat;

    iget-boolean v6, p0, Landroidx/media/AudioFocusRequestCompat$Builder;->mPauseOnDuck:Z

    move-object v1, v0

    invoke-direct/range {v1 .. v6}, Landroidx/media/AudioFocusRequestCompat;-><init>(ILandroid/media/AudioManager$OnAudioFocusChangeListener;Landroid/os/Handler;Landroidx/media/AudioAttributesCompat;Z)V

    return-object v0

    .line 368
    :cond_0
    new-instance v0, Ljava/lang/IllegalStateException;

    const-string v1, "Can\'t build an AudioFocusRequestCompat instance without a listener"

    invoke-direct {v0, v1}, Ljava/lang/IllegalStateException;-><init>(Ljava/lang/String;)V

    throw v0
.end method

.method public setAudioAttributes(Landroidx/media/AudioAttributesCompat;)Landroidx/media/AudioFocusRequestCompat$Builder;
    .locals 2
    .param p1, "attributes"    # Landroidx/media/AudioAttributesCompat;
    .annotation system Ldalvik/annotation/MethodParameters;
        accessFlags = {
            0x0
        }
        names = {
            "attributes"
        }
    .end annotation

    .line 335
    if-eqz p1, :cond_0

    .line 338
    iput-object p1, p0, Landroidx/media/AudioFocusRequestCompat$Builder;->mAudioAttributesCompat:Landroidx/media/AudioAttributesCompat;

    .line 339
    return-object p0

    .line 336
    :cond_0
    new-instance v0, Ljava/lang/NullPointerException;

    const-string v1, "Illegal null AudioAttributes"

    invoke-direct {v0, v1}, Ljava/lang/NullPointerException;-><init>(Ljava/lang/String;)V

    throw v0
.end method

.method public setFocusGain(I)Landroidx/media/AudioFocusRequestCompat$Builder;
    .locals 3
    .param p1, "focusGain"    # I
    .annotation system Ldalvik/annotation/MethodParameters;
        accessFlags = {
            0x0
        }
        names = {
            "focusGain"
        }
    .end annotation

    .line 262
    invoke-static {p1}, Landroidx/media/AudioFocusRequestCompat$Builder;->isValidFocusGain(I)Z

    move-result v0

    if-eqz v0, :cond_1

    .line 266
    sget v0, Landroid/os/Build$VERSION;->SDK_INT:I

    const/16 v1, 0x13

    if-ge v0, v1, :cond_0

    const/4 v0, 0x4

    if-ne p1, v0, :cond_0

    .line 268
    const/4 p1, 0x2

    .line 270
    :cond_0
    iput p1, p0, Landroidx/media/AudioFocusRequestCompat$Builder;->mFocusGain:I

    .line 271
    return-object p0

    .line 263
    :cond_1
    new-instance v0, Ljava/lang/IllegalArgumentException;

    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "Illegal audio focus gain type "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v1

    invoke-virtual {v1, p1}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    move-result-object v1

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-direct {v0, v1}, Ljava/lang/IllegalArgumentException;-><init>(Ljava/lang/String;)V

    throw v0
.end method

.method public setOnAudioFocusChangeListener(Landroid/media/AudioManager$OnAudioFocusChangeListener;)Landroidx/media/AudioFocusRequestCompat$Builder;
    .locals 2
    .param p1, "listener"    # Landroid/media/AudioManager$OnAudioFocusChangeListener;
    .annotation system Ldalvik/annotation/MethodParameters;
        accessFlags = {
            0x0
        }
        names = {
            "listener"
        }
    .end annotation

    .line 289
    new-instance v0, Landroid/os/Handler;

    invoke-static {}, Landroid/os/Looper;->getMainLooper()Landroid/os/Looper;

    move-result-object v1

    invoke-direct {v0, v1}, Landroid/os/Handler;-><init>(Landroid/os/Looper;)V

    invoke-virtual {p0, p1, v0}, Landroidx/media/AudioFocusRequestCompat$Builder;->setOnAudioFocusChangeListener(Landroid/media/AudioManager$OnAudioFocusChangeListener;Landroid/os/Handler;)Landroidx/media/AudioFocusRequestCompat$Builder;

    move-result-object v0

    return-object v0
.end method

.method public setOnAudioFocusChangeListener(Landroid/media/AudioManager$OnAudioFocusChangeListener;Landroid/os/Handler;)Landroidx/media/AudioFocusRequestCompat$Builder;
    .locals 2
    .param p1, "listener"    # Landroid/media/AudioManager$OnAudioFocusChangeListener;
    .param p2, "handler"    # Landroid/os/Handler;
    .annotation system Ldalvik/annotation/MethodParameters;
        accessFlags = {
            0x0,
            0x0
        }
        names = {
            "listener",
            "handler"
        }
    .end annotation

    .line 307
    if-eqz p1, :cond_1

    .line 310
    if-eqz p2, :cond_0

    .line 314
    iput-object p1, p0, Landroidx/media/AudioFocusRequestCompat$Builder;->mOnAudioFocusChangeListener:Landroid/media/AudioManager$OnAudioFocusChangeListener;

    .line 315
    iput-object p2, p0, Landroidx/media/AudioFocusRequestCompat$Builder;->mFocusChangeHandler:Landroid/os/Handler;

    .line 316
    return-object p0

    .line 311
    :cond_0
    new-instance v0, Ljava/lang/IllegalArgumentException;

    const-string v1, "Handler must not be null"

    invoke-direct {v0, v1}, Ljava/lang/IllegalArgumentException;-><init>(Ljava/lang/String;)V

    throw v0

    .line 308
    :cond_1
    new-instance v0, Ljava/lang/IllegalArgumentException;

    const-string v1, "OnAudioFocusChangeListener must not be null"

    invoke-direct {v0, v1}, Ljava/lang/IllegalArgumentException;-><init>(Ljava/lang/String;)V

    throw v0
.end method

.method public setWillPauseWhenDucked(Z)Landroidx/media/AudioFocusRequestCompat$Builder;
    .locals 0
    .param p1, "pauseOnDuck"    # Z
    .annotation system Ldalvik/annotation/MethodParameters;
        accessFlags = {
            0x0
        }
        names = {
            "pauseOnDuck"
        }
    .end annotation

    .line 353
    iput-boolean p1, p0, Landroidx/media/AudioFocusRequestCompat$Builder;->mPauseOnDuck:Z

    .line 354
    return-object p0
.end method
