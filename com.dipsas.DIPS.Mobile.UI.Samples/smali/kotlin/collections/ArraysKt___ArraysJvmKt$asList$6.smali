.class public final Lkotlin/collections/ArraysKt___ArraysJvmKt$asList$6;
.super Lkotlin/collections/AbstractList;
.source "_ArraysJvm.kt"

# interfaces
.implements Ljava/util/RandomAccess;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lkotlin/collections/ArraysKt___ArraysJvmKt;->asList([D)Ljava/util/List;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x19
    name = null
.end annotation

.annotation system Ldalvik/annotation/Signature;
    value = {
        "Lkotlin/collections/AbstractList<",
        "Ljava/lang/Double;",
        ">;",
        "Ljava/util/RandomAccess;"
    }
.end annotation

.annotation system Ldalvik/annotation/SourceDebugExtension;
    value = "SMAP\n_ArraysJvm.kt\nKotlin\n*S Kotlin\n*F\n+ 1 _ArraysJvm.kt\nkotlin/collections/ArraysKt___ArraysJvmKt$asList$6\n+ 2 _Arrays.kt\nkotlin/collections/ArraysKt___ArraysKt\n*L\n1#1,3024:1\n12761#2,2:3025\n1692#2,6:3027\n1800#2,6:3033\n*S KotlinDebug\n*F\n+ 1 _ArraysJvm.kt\nkotlin/collections/ArraysKt___ArraysJvmKt$asList$6\n*L\n213#1:3025,2\n215#1:3027,6\n216#1:3033,6\n*E\n"
.end annotation

.annotation runtime Lkotlin/Metadata;
    d1 = {
        "\u0000\'\n\u0000\n\u0002\u0018\u0002\n\u0002\u0010\u0006\n\u0002\u0018\u0002\n\u0002\u0018\u0002\n\u0000\n\u0002\u0010\u0008\n\u0002\u0008\u0003\n\u0002\u0010\u000b\n\u0002\u0008\u0008*\u0001\u0000\u0008\n\u0018\u00002\u0008\u0012\u0004\u0012\u00020\u00020\u00012\u00060\u0003j\u0002`\u0004J\u0011\u0010\t\u001a\u00020\n2\u0006\u0010\u000b\u001a\u00020\u0002H\u0096\u0002J\u0016\u0010\u000c\u001a\u00020\u00022\u0006\u0010\r\u001a\u00020\u0006H\u0096\u0002\u00a2\u0006\u0002\u0010\u000eJ\u0010\u0010\u000f\u001a\u00020\u00062\u0006\u0010\u000b\u001a\u00020\u0002H\u0016J\u0008\u0010\u0010\u001a\u00020\nH\u0016J\u0010\u0010\u0011\u001a\u00020\u00062\u0006\u0010\u000b\u001a\u00020\u0002H\u0016R\u0014\u0010\u0005\u001a\u00020\u00068VX\u0096\u0004\u00a2\u0006\u0006\u001a\u0004\u0008\u0007\u0010\u0008\u00a8\u0006\u0012"
    }
    d2 = {
        "kotlin/collections/ArraysKt___ArraysJvmKt$asList$6",
        "Lkotlin/collections/AbstractList;",
        "",
        "Ljava/util/RandomAccess;",
        "Lkotlin/collections/RandomAccess;",
        "size",
        "",
        "getSize",
        "()I",
        "contains",
        "",
        "element",
        "get",
        "index",
        "(I)Ljava/lang/Double;",
        "indexOf",
        "isEmpty",
        "lastIndexOf",
        "kotlin-stdlib"
    }
    k = 0x1
    mv = {
        0x1,
        0x6,
        0x0
    }
    xi = 0x30
.end annotation


# instance fields
.field final synthetic $this_asList:[D


# direct methods
.method constructor <init>([D)V
    .locals 0
    .param p1, "$receiver"    # [D

    iput-object p1, p0, Lkotlin/collections/ArraysKt___ArraysJvmKt$asList$6;->$this_asList:[D

    .line 210
    invoke-direct {p0}, Lkotlin/collections/AbstractList;-><init>()V

    return-void
.end method


# virtual methods
.method public contains(D)Z
    .locals 17
    .param p1, "element"    # D

    .line 213
    move-object/from16 v0, p0

    iget-object v1, v0, Lkotlin/collections/ArraysKt___ArraysJvmKt$asList$6;->$this_asList:[D

    .local v1, "$this$any$iv":[D
    const/4 v2, 0x0

    .line 3025
    .local v2, "$i$f$any":I
    array-length v3, v1

    const/4 v4, 0x0

    const/4 v5, 0x0

    :cond_0
    const/4 v6, 0x1

    if-ge v5, v3, :cond_2

    aget-wide v7, v1, v5

    .local v7, "element$iv":D
    add-int/lit8 v5, v5, 0x1

    move-wide v9, v7

    .local v9, "it":D
    const/4 v11, 0x0

    .line 213
    .local v11, "$i$a$-any-ArraysKt___ArraysJvmKt$asList$6$contains$1":I
    invoke-static {v9, v10}, Ljava/lang/Double;->doubleToLongBits(D)J

    move-result-wide v12

    invoke-static/range {p1 .. p2}, Ljava/lang/Double;->doubleToLongBits(D)J

    move-result-wide v14

    cmp-long v16, v12, v14

    if-nez v16, :cond_1

    const/4 v12, 0x1

    goto :goto_0

    :cond_1
    const/4 v12, 0x0

    .end local v9    # "it":D
    .end local v11    # "$i$a$-any-ArraysKt___ArraysJvmKt$asList$6$contains$1":I
    :goto_0
    if-eqz v12, :cond_0

    const/4 v4, 0x1

    goto :goto_1

    .line 3026
    .end local v7    # "element$iv":D
    :cond_2
    nop

    .line 213
    .end local v1    # "$this$any$iv":[D
    .end local v2    # "$i$f$any":I
    :goto_1
    return v4
.end method

.method public final bridge contains(Ljava/lang/Object;)Z
    .locals 2
    .param p1, "element"    # Ljava/lang/Object;

    .line 210
    instance-of v0, p1, Ljava/lang/Double;

    if-nez v0, :cond_0

    const/4 v0, 0x0

    return v0

    :cond_0
    move-object v0, p1

    check-cast v0, Ljava/lang/Number;

    invoke-virtual {v0}, Ljava/lang/Number;->doubleValue()D

    move-result-wide v0

    invoke-virtual {p0, v0, v1}, Lkotlin/collections/ArraysKt___ArraysJvmKt$asList$6;->contains(D)Z

    move-result v0

    return v0
.end method

.method public get(I)Ljava/lang/Double;
    .locals 3
    .param p1, "index"    # I

    .line 214
    iget-object v0, p0, Lkotlin/collections/ArraysKt___ArraysJvmKt$asList$6;->$this_asList:[D

    aget-wide v1, v0, p1

    invoke-static {v1, v2}, Ljava/lang/Double;->valueOf(D)Ljava/lang/Double;

    move-result-object v0

    return-object v0
.end method

.method public bridge synthetic get(I)Ljava/lang/Object;
    .locals 1
    .param p1, "index"    # I

    .line 210
    invoke-virtual {p0, p1}, Lkotlin/collections/ArraysKt___ArraysJvmKt$asList$6;->get(I)Ljava/lang/Double;

    move-result-object v0

    return-object v0
.end method

.method public getSize()I
    .locals 1

    .line 211
    iget-object v0, p0, Lkotlin/collections/ArraysKt___ArraysJvmKt$asList$6;->$this_asList:[D

    array-length v0, v0

    return v0
.end method

.method public indexOf(D)I
    .locals 15
    .param p1, "element"    # D

    .line 215
    move-object v0, p0

    iget-object v1, v0, Lkotlin/collections/ArraysKt___ArraysJvmKt$asList$6;->$this_asList:[D

    .local v1, "$this$indexOfFirst$iv":[D
    const/4 v2, 0x0

    .line 3027
    .local v2, "$i$f$indexOfFirst":I
    array-length v3, v1

    const/4 v4, 0x0

    const/4 v5, 0x0

    :cond_0
    if-ge v5, v3, :cond_2

    move v6, v5

    .local v6, "index$iv":I
    add-int/lit8 v5, v5, 0x1

    .line 3028
    aget-wide v7, v1, v6

    .local v7, "it":D
    const/4 v9, 0x0

    .line 215
    .local v9, "$i$a$-indexOfFirst-ArraysKt___ArraysJvmKt$asList$6$indexOf$1":I
    invoke-static {v7, v8}, Ljava/lang/Double;->doubleToLongBits(D)J

    move-result-wide v10

    invoke-static/range {p1 .. p2}, Ljava/lang/Double;->doubleToLongBits(D)J

    move-result-wide v12

    cmp-long v14, v10, v12

    if-nez v14, :cond_1

    const/4 v10, 0x1

    goto :goto_0

    :cond_1
    const/4 v10, 0x0

    .end local v7    # "it":D
    .end local v9    # "$i$a$-indexOfFirst-ArraysKt___ArraysJvmKt$asList$6$indexOf$1":I
    :goto_0
    if-eqz v10, :cond_0

    .line 3029
    goto :goto_1

    .line 3032
    .end local v6    # "index$iv":I
    :cond_2
    const/4 v6, -0x1

    .line 215
    .end local v1    # "$this$indexOfFirst$iv":[D
    .end local v2    # "$i$f$indexOfFirst":I
    :goto_1
    return v6
.end method

.method public final bridge indexOf(Ljava/lang/Object;)I
    .locals 2
    .param p1, "element"    # Ljava/lang/Object;

    .line 210
    instance-of v0, p1, Ljava/lang/Double;

    if-nez v0, :cond_0

    const/4 v0, -0x1

    return v0

    :cond_0
    move-object v0, p1

    check-cast v0, Ljava/lang/Number;

    invoke-virtual {v0}, Ljava/lang/Number;->doubleValue()D

    move-result-wide v0

    invoke-virtual {p0, v0, v1}, Lkotlin/collections/ArraysKt___ArraysJvmKt$asList$6;->indexOf(D)I

    move-result v0

    return v0
.end method

.method public isEmpty()Z
    .locals 1

    .line 212
    iget-object v0, p0, Lkotlin/collections/ArraysKt___ArraysJvmKt$asList$6;->$this_asList:[D

    array-length v0, v0

    if-nez v0, :cond_0

    const/4 v0, 0x1

    goto :goto_0

    :cond_0
    const/4 v0, 0x0

    :goto_0
    return v0
.end method

.method public lastIndexOf(D)I
    .locals 13
    .param p1, "element"    # D

    .line 216
    iget-object v0, p0, Lkotlin/collections/ArraysKt___ArraysJvmKt$asList$6;->$this_asList:[D

    .local v0, "$this$indexOfLast$iv":[D
    const/4 v1, 0x0

    .line 3033
    .local v1, "$i$f$indexOfLast":I
    array-length v2, v0

    const/4 v3, -0x1

    add-int/2addr v2, v3

    if-ltz v2, :cond_3

    :cond_0
    move v4, v2

    .local v4, "index$iv":I
    add-int/2addr v2, v3

    .line 3034
    aget-wide v5, v0, v4

    .local v5, "it":D
    const/4 v7, 0x0

    .line 216
    .local v7, "$i$a$-indexOfLast-ArraysKt___ArraysJvmKt$asList$6$lastIndexOf$1":I
    invoke-static {v5, v6}, Ljava/lang/Double;->doubleToLongBits(D)J

    move-result-wide v8

    invoke-static {p1, p2}, Ljava/lang/Double;->doubleToLongBits(D)J

    move-result-wide v10

    cmp-long v12, v8, v10

    if-nez v12, :cond_1

    const/4 v8, 0x1

    goto :goto_0

    :cond_1
    const/4 v8, 0x0

    .end local v5    # "it":D
    .end local v7    # "$i$a$-indexOfLast-ArraysKt___ArraysJvmKt$asList$6$lastIndexOf$1":I
    :goto_0
    if-eqz v8, :cond_2

    .line 3035
    move v3, v4

    goto :goto_1

    .line 3033
    :cond_2
    if-gez v2, :cond_0

    .line 3038
    .end local v4    # "index$iv":I
    :cond_3
    nop

    .line 216
    .end local v0    # "$this$indexOfLast$iv":[D
    .end local v1    # "$i$f$indexOfLast":I
    :goto_1
    return v3
.end method

.method public final bridge lastIndexOf(Ljava/lang/Object;)I
    .locals 2
    .param p1, "element"    # Ljava/lang/Object;

    .line 210
    instance-of v0, p1, Ljava/lang/Double;

    if-nez v0, :cond_0

    const/4 v0, -0x1

    return v0

    :cond_0
    move-object v0, p1

    check-cast v0, Ljava/lang/Number;

    invoke-virtual {v0}, Ljava/lang/Number;->doubleValue()D

    move-result-wide v0

    invoke-virtual {p0, v0, v1}, Lkotlin/collections/ArraysKt___ArraysJvmKt$asList$6;->lastIndexOf(D)I

    move-result v0

    return v0
.end method
