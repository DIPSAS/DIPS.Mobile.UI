.class public final Landroidx/core/location/LocationKt;
.super Ljava/lang/Object;
.source "Location.kt"


# annotations
.annotation runtime Lkotlin/Metadata;
    d1 = {
        "\u0000\u000e\n\u0000\n\u0002\u0010\u0006\n\u0002\u0018\u0002\n\u0002\u0008\u0002\u001a\r\u0010\u0000\u001a\u00020\u0001*\u00020\u0002H\u0086\n\u001a\r\u0010\u0003\u001a\u00020\u0001*\u00020\u0002H\u0086\n\u00a8\u0006\u0004"
    }
    d2 = {
        "component1",
        "",
        "Landroid/location/Location;",
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
.method public static final component1(Landroid/location/Location;)D
    .locals 3
    .param p0, "$this$component1"    # Landroid/location/Location;

    const-string v0, "<this>"

    invoke-static {p0, v0}, Lkotlin/jvm/internal/Intrinsics;->checkNotNullParameter(Ljava/lang/Object;Ljava/lang/String;)V

    const/4 v0, 0x0

    .line 32
    .local v0, "$i$f$component1":I
    invoke-virtual {p0}, Landroid/location/Location;->getLatitude()D

    move-result-wide v1

    return-wide v1
.end method

.method public static final component2(Landroid/location/Location;)D
    .locals 3
    .param p0, "$this$component2"    # Landroid/location/Location;

    const-string v0, "<this>"

    invoke-static {p0, v0}, Lkotlin/jvm/internal/Intrinsics;->checkNotNullParameter(Ljava/lang/Object;Ljava/lang/String;)V

    const/4 v0, 0x0

    .line 43
    .local v0, "$i$f$component2":I
    invoke-virtual {p0}, Landroid/location/Location;->getLongitude()D

    move-result-wide v1

    return-wide v1
.end method
