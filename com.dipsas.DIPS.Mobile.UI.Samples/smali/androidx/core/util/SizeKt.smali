.class public final Landroidx/core/util/SizeKt;
.super Ljava/lang/Object;
.source "Size.kt"


# annotations
.annotation runtime Lkotlin/Metadata;
    d1 = {
        "\u0000\u0016\n\u0000\n\u0002\u0010\u0008\n\u0002\u0018\u0002\n\u0002\u0010\u0007\n\u0002\u0018\u0002\n\u0002\u0008\u0002\u001a\r\u0010\u0000\u001a\u00020\u0001*\u00020\u0002H\u0087\n\u001a\r\u0010\u0000\u001a\u00020\u0003*\u00020\u0004H\u0087\n\u001a\r\u0010\u0005\u001a\u00020\u0001*\u00020\u0002H\u0087\n\u001a\r\u0010\u0005\u001a\u00020\u0003*\u00020\u0004H\u0087\n\u00a8\u0006\u0006"
    }
    d2 = {
        "component1",
        "",
        "Landroid/util/Size;",
        "",
        "Landroid/util/SizeF;",
        "component2",
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
.method public static final component1(Landroid/util/SizeF;)F
    .locals 2
    .param p0, "$this$component1"    # Landroid/util/SizeF;

    const-string v0, "<this>"

    invoke-static {p0, v0}, Lkotlin/jvm/internal/Intrinsics;->checkNotNullParameter(Ljava/lang/Object;Ljava/lang/String;)V

    const/4 v0, 0x0

    .line 59
    .local v0, "$i$f$component1":I
    invoke-virtual {p0}, Landroid/util/SizeF;->getWidth()F

    move-result v1

    return v1
.end method

.method public static final component1(Landroid/util/Size;)I
    .locals 2
    .param p0, "$this$component1"    # Landroid/util/Size;

    const-string v0, "<this>"

    invoke-static {p0, v0}, Lkotlin/jvm/internal/Intrinsics;->checkNotNullParameter(Ljava/lang/Object;Ljava/lang/String;)V

    const/4 v0, 0x0

    .line 35
    .local v0, "$i$f$component1":I
    invoke-virtual {p0}, Landroid/util/Size;->getWidth()I

    move-result v1

    return v1
.end method

.method public static final component2(Landroid/util/SizeF;)F
    .locals 2
    .param p0, "$this$component2"    # Landroid/util/SizeF;

    const-string v0, "<this>"

    invoke-static {p0, v0}, Lkotlin/jvm/internal/Intrinsics;->checkNotNullParameter(Ljava/lang/Object;Ljava/lang/String;)V

    const/4 v0, 0x0

    .line 71
    .local v0, "$i$f$component2":I
    invoke-virtual {p0}, Landroid/util/SizeF;->getHeight()F

    move-result v1

    return v1
.end method

.method public static final component2(Landroid/util/Size;)I
    .locals 2
    .param p0, "$this$component2"    # Landroid/util/Size;

    const-string v0, "<this>"

    invoke-static {p0, v0}, Lkotlin/jvm/internal/Intrinsics;->checkNotNullParameter(Ljava/lang/Object;Ljava/lang/String;)V

    const/4 v0, 0x0

    .line 47
    .local v0, "$i$f$component2":I
    invoke-virtual {p0}, Landroid/util/Size;->getHeight()I

    move-result v1

    return v1
.end method
