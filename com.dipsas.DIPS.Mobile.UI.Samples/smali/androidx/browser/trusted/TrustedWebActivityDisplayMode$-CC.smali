.class public final synthetic Landroidx/browser/trusted/TrustedWebActivityDisplayMode$-CC;
.super Ljava/lang/Object;
.source "TrustedWebActivityDisplayMode.java"


# annotations
.annotation system Ldalvik/annotation/Signature;
    value = {
        "Ljava/lang/Object;"
    }
.end annotation


# direct methods
.method public static fromBundle(Landroid/os/Bundle;)Landroidx/browser/trusted/TrustedWebActivityDisplayMode;
    .locals 1
    .param p0, "bundle"    # Landroid/os/Bundle;
    .annotation system Ldalvik/annotation/MethodParameters;
        accessFlags = {
            0x0
        }
        names = {
            "bundle"
        }
    .end annotation

    .line 34
    const-string v0, "androidx.browser.trusted.displaymode.KEY_ID"

    invoke-virtual {p0, v0}, Landroid/os/Bundle;->getInt(Ljava/lang/String;)I

    move-result v0

    packed-switch v0, :pswitch_data_0

    .line 39
    new-instance v0, Landroidx/browser/trusted/TrustedWebActivityDisplayMode$DefaultMode;

    invoke-direct {v0}, Landroidx/browser/trusted/TrustedWebActivityDisplayMode$DefaultMode;-><init>()V

    return-object v0

    .line 36
    :pswitch_0
    invoke-static {p0}, Landroidx/browser/trusted/TrustedWebActivityDisplayMode$ImmersiveMode;->fromBundle(Landroid/os/Bundle;)Landroidx/browser/trusted/TrustedWebActivityDisplayMode;

    move-result-object v0

    return-object v0

    :pswitch_data_0
    .packed-switch 0x1
        :pswitch_0
    .end packed-switch
.end method
