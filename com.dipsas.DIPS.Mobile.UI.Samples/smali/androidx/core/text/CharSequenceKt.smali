.class public final Landroidx/core/text/CharSequenceKt;
.super Ljava/lang/Object;
.source "CharSequence.kt"


# annotations
.annotation runtime Lkotlin/Metadata;
    d1 = {
        "\u0000\u0012\n\u0000\n\u0002\u0010\u000b\n\u0002\u0010\r\n\u0000\n\u0002\u0010\u0008\n\u0000\u001a\r\u0010\u0000\u001a\u00020\u0001*\u00020\u0002H\u0086\u0008\u001a\r\u0010\u0003\u001a\u00020\u0004*\u00020\u0002H\u0086\u0008\u00a8\u0006\u0005"
    }
    d2 = {
        "isDigitsOnly",
        "",
        "",
        "trimmedLength",
        "",
        "core-ktx_release"
    }
    k = 0x2
    mv = {
        0x1,
        0x5,
        0x1
    }
    xi = 0x30
.end annotation


# direct methods
.method public static final isDigitsOnly(Ljava/lang/CharSequence;)Z
    .locals 2
    .param p0, "$this$isDigitsOnly"    # Ljava/lang/CharSequence;

    const-string v0, "<this>"

    invoke-static {p0, v0}, Lkotlin/jvm/internal/Intrinsics;->checkNotNullParameter(Ljava/lang/Object;Ljava/lang/String;)V

    const/4 v0, 0x0

    .line 28
    .local v0, "$i$f$isDigitsOnly":I
    invoke-static {p0}, Landroid/text/TextUtils;->isDigitsOnly(Ljava/lang/CharSequence;)Z

    move-result v1

    return v1
.end method

.method public static final trimmedLength(Ljava/lang/CharSequence;)I
    .locals 2
    .param p0, "$this$trimmedLength"    # Ljava/lang/CharSequence;

    const-string v0, "<this>"

    invoke-static {p0, v0}, Lkotlin/jvm/internal/Intrinsics;->checkNotNullParameter(Ljava/lang/Object;Ljava/lang/String;)V

    const/4 v0, 0x0

    .line 36
    .local v0, "$i$f$trimmedLength":I
    invoke-static {p0}, Landroid/text/TextUtils;->getTrimmedLength(Ljava/lang/CharSequence;)I

    move-result v1

    return v1
.end method
