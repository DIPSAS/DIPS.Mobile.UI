.class Landroidx/navigation/NavType$9;
.super Landroidx/navigation/NavType;
.source "NavType.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Landroidx/navigation/NavType;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation

.annotation system Ldalvik/annotation/Signature;
    value = {
        "Landroidx/navigation/NavType<",
        "[Z>;"
    }
.end annotation


# direct methods
.method constructor <init>(Z)V
    .locals 0
    .param p1, "nullableAllowed"    # Z

    .line 549
    invoke-direct {p0, p1}, Landroidx/navigation/NavType;-><init>(Z)V

    return-void
.end method


# virtual methods
.method public bridge synthetic get(Landroid/os/Bundle;Ljava/lang/String;)Ljava/lang/Object;
    .locals 0

    .line 549
    invoke-virtual {p0, p1, p2}, Landroidx/navigation/NavType$9;->get(Landroid/os/Bundle;Ljava/lang/String;)[Z

    move-result-object p1

    return-object p1
.end method

.method public get(Landroid/os/Bundle;Ljava/lang/String;)[Z
    .locals 1
    .param p1, "bundle"    # Landroid/os/Bundle;
    .param p2, "key"    # Ljava/lang/String;

    .line 557
    invoke-virtual {p1, p2}, Landroid/os/Bundle;->get(Ljava/lang/String;)Ljava/lang/Object;

    move-result-object v0

    check-cast v0, [Z

    return-object v0
.end method

.method public getName()Ljava/lang/String;
    .locals 1

    .line 569
    const-string v0, "boolean[]"

    return-object v0
.end method

.method public bridge synthetic parseValue(Ljava/lang/String;)Ljava/lang/Object;
    .locals 0

    .line 549
    invoke-virtual {p0, p1}, Landroidx/navigation/NavType$9;->parseValue(Ljava/lang/String;)[Z

    move-result-object p1

    return-object p1
.end method

.method public parseValue(Ljava/lang/String;)[Z
    .locals 2
    .param p1, "value"    # Ljava/lang/String;

    .line 563
    new-instance v0, Ljava/lang/UnsupportedOperationException;

    const-string v1, "Arrays don\'t support default values."

    invoke-direct {v0, v1}, Ljava/lang/UnsupportedOperationException;-><init>(Ljava/lang/String;)V

    throw v0
.end method

.method public bridge synthetic put(Landroid/os/Bundle;Ljava/lang/String;Ljava/lang/Object;)V
    .locals 0

    .line 549
    check-cast p3, [Z

    invoke-virtual {p0, p1, p2, p3}, Landroidx/navigation/NavType$9;->put(Landroid/os/Bundle;Ljava/lang/String;[Z)V

    return-void
.end method

.method public put(Landroid/os/Bundle;Ljava/lang/String;[Z)V
    .locals 0
    .param p1, "bundle"    # Landroid/os/Bundle;
    .param p2, "key"    # Ljava/lang/String;
    .param p3, "value"    # [Z

    .line 552
    invoke-virtual {p1, p2, p3}, Landroid/os/Bundle;->putBooleanArray(Ljava/lang/String;[Z)V

    .line 553
    return-void
.end method
