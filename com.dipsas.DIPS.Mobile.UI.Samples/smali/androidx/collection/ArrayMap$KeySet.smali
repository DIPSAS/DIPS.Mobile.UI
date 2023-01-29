.class final Landroidx/collection/ArrayMap$KeySet;
.super Ljava/lang/Object;
.source "ArrayMap.java"

# interfaces
.implements Ljava/util/Set;


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Landroidx/collection/ArrayMap;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x10
    name = "KeySet"
.end annotation

.annotation system Ldalvik/annotation/Signature;
    value = {
        "Ljava/lang/Object;",
        "Ljava/util/Set<",
        "TK;>;"
    }
.end annotation


# instance fields
.field final synthetic this$0:Landroidx/collection/ArrayMap;


# direct methods
.method constructor <init>(Landroidx/collection/ArrayMap;)V
    .locals 0
    .param p1, "this$0"    # Landroidx/collection/ArrayMap;

    .line 206
    .local p0, "this":Landroidx/collection/ArrayMap$KeySet;, "Landroidx/collection/ArrayMap<TK;TV;>.KeySet;"
    iput-object p1, p0, Landroidx/collection/ArrayMap$KeySet;->this$0:Landroidx/collection/ArrayMap;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public add(Ljava/lang/Object;)Z
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(TK;)Z"
        }
    .end annotation

    .line 209
    .local p0, "this":Landroidx/collection/ArrayMap$KeySet;, "Landroidx/collection/ArrayMap<TK;TV;>.KeySet;"
    .local p1, "object":Ljava/lang/Object;, "TK;"
    new-instance v0, Ljava/lang/UnsupportedOperationException;

    invoke-direct {v0}, Ljava/lang/UnsupportedOperationException;-><init>()V

    throw v0
.end method

.method public addAll(Ljava/util/Collection;)Z
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/Collection<",
            "+TK;>;)Z"
        }
    .end annotation

    .line 214
    .local p0, "this":Landroidx/collection/ArrayMap$KeySet;, "Landroidx/collection/ArrayMap<TK;TV;>.KeySet;"
    .local p1, "collection":Ljava/util/Collection;, "Ljava/util/Collection<+TK;>;"
    new-instance v0, Ljava/lang/UnsupportedOperationException;

    invoke-direct {v0}, Ljava/lang/UnsupportedOperationException;-><init>()V

    throw v0
.end method

.method public clear()V
    .locals 1

    .line 219
    .local p0, "this":Landroidx/collection/ArrayMap$KeySet;, "Landroidx/collection/ArrayMap<TK;TV;>.KeySet;"
    iget-object v0, p0, Landroidx/collection/ArrayMap$KeySet;->this$0:Landroidx/collection/ArrayMap;

    invoke-virtual {v0}, Landroidx/collection/ArrayMap;->clear()V

    .line 220
    return-void
.end method

.method public contains(Ljava/lang/Object;)Z
    .locals 1
    .param p1, "object"    # Ljava/lang/Object;

    .line 224
    .local p0, "this":Landroidx/collection/ArrayMap$KeySet;, "Landroidx/collection/ArrayMap<TK;TV;>.KeySet;"
    iget-object v0, p0, Landroidx/collection/ArrayMap$KeySet;->this$0:Landroidx/collection/ArrayMap;

    invoke-virtual {v0, p1}, Landroidx/collection/ArrayMap;->containsKey(Ljava/lang/Object;)Z

    move-result v0

    return v0
.end method

.method public containsAll(Ljava/util/Collection;)Z
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/Collection<",
            "*>;)Z"
        }
    .end annotation

    .line 229
    .local p0, "this":Landroidx/collection/ArrayMap$KeySet;, "Landroidx/collection/ArrayMap<TK;TV;>.KeySet;"
    .local p1, "collection":Ljava/util/Collection;, "Ljava/util/Collection<*>;"
    iget-object v0, p0, Landroidx/collection/ArrayMap$KeySet;->this$0:Landroidx/collection/ArrayMap;

    invoke-virtual {v0, p1}, Landroidx/collection/ArrayMap;->containsAll(Ljava/util/Collection;)Z

    move-result v0

    return v0
.end method

.method public equals(Ljava/lang/Object;)Z
    .locals 1
    .param p1, "object"    # Ljava/lang/Object;

    .line 284
    .local p0, "this":Landroidx/collection/ArrayMap$KeySet;, "Landroidx/collection/ArrayMap<TK;TV;>.KeySet;"
    invoke-static {p0, p1}, Landroidx/collection/ArrayMap;->equalsSetHelper(Ljava/util/Set;Ljava/lang/Object;)Z

    move-result v0

    return v0
.end method

.method public hashCode()I
    .locals 4

    .line 289
    .local p0, "this":Landroidx/collection/ArrayMap$KeySet;, "Landroidx/collection/ArrayMap<TK;TV;>.KeySet;"
    const/4 v0, 0x0

    .line 290
    .local v0, "result":I
    iget-object v1, p0, Landroidx/collection/ArrayMap$KeySet;->this$0:Landroidx/collection/ArrayMap;

    iget v1, v1, Landroidx/collection/ArrayMap;->mSize:I

    add-int/lit8 v1, v1, -0x1

    .local v1, "i":I
    :goto_0
    if-ltz v1, :cond_1

    .line 291
    iget-object v2, p0, Landroidx/collection/ArrayMap$KeySet;->this$0:Landroidx/collection/ArrayMap;

    invoke-virtual {v2, v1}, Landroidx/collection/ArrayMap;->keyAt(I)Ljava/lang/Object;

    move-result-object v2

    .line 292
    .local v2, "obj":Ljava/lang/Object;, "TK;"
    if-nez v2, :cond_0

    const/4 v3, 0x0

    goto :goto_1

    :cond_0
    invoke-virtual {v2}, Ljava/lang/Object;->hashCode()I

    move-result v3

    :goto_1
    add-int/2addr v0, v3

    .line 290
    .end local v2    # "obj":Ljava/lang/Object;, "TK;"
    add-int/lit8 v1, v1, -0x1

    goto :goto_0

    .line 294
    .end local v1    # "i":I
    :cond_1
    return v0
.end method

.method public isEmpty()Z
    .locals 1

    .line 234
    .local p0, "this":Landroidx/collection/ArrayMap$KeySet;, "Landroidx/collection/ArrayMap<TK;TV;>.KeySet;"
    iget-object v0, p0, Landroidx/collection/ArrayMap$KeySet;->this$0:Landroidx/collection/ArrayMap;

    invoke-virtual {v0}, Landroidx/collection/ArrayMap;->isEmpty()Z

    move-result v0

    return v0
.end method

.method public iterator()Ljava/util/Iterator;
    .locals 2
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()",
            "Ljava/util/Iterator<",
            "TK;>;"
        }
    .end annotation

    .line 239
    .local p0, "this":Landroidx/collection/ArrayMap$KeySet;, "Landroidx/collection/ArrayMap<TK;TV;>.KeySet;"
    new-instance v0, Landroidx/collection/ArrayMap$KeyIterator;

    iget-object v1, p0, Landroidx/collection/ArrayMap$KeySet;->this$0:Landroidx/collection/ArrayMap;

    invoke-direct {v0, v1}, Landroidx/collection/ArrayMap$KeyIterator;-><init>(Landroidx/collection/ArrayMap;)V

    return-object v0
.end method

.method public remove(Ljava/lang/Object;)Z
    .locals 2
    .param p1, "object"    # Ljava/lang/Object;

    .line 244
    .local p0, "this":Landroidx/collection/ArrayMap$KeySet;, "Landroidx/collection/ArrayMap<TK;TV;>.KeySet;"
    iget-object v0, p0, Landroidx/collection/ArrayMap$KeySet;->this$0:Landroidx/collection/ArrayMap;

    invoke-virtual {v0, p1}, Landroidx/collection/ArrayMap;->indexOfKey(Ljava/lang/Object;)I

    move-result v0

    .line 245
    .local v0, "index":I
    if-ltz v0, :cond_0

    .line 246
    iget-object v1, p0, Landroidx/collection/ArrayMap$KeySet;->this$0:Landroidx/collection/ArrayMap;

    invoke-virtual {v1, v0}, Landroidx/collection/ArrayMap;->removeAt(I)Ljava/lang/Object;

    .line 247
    const/4 v1, 0x1

    return v1

    .line 249
    :cond_0
    const/4 v1, 0x0

    return v1
.end method

.method public removeAll(Ljava/util/Collection;)Z
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/Collection<",
            "*>;)Z"
        }
    .end annotation

    .line 254
    .local p0, "this":Landroidx/collection/ArrayMap$KeySet;, "Landroidx/collection/ArrayMap<TK;TV;>.KeySet;"
    .local p1, "collection":Ljava/util/Collection;, "Ljava/util/Collection<*>;"
    iget-object v0, p0, Landroidx/collection/ArrayMap$KeySet;->this$0:Landroidx/collection/ArrayMap;

    invoke-virtual {v0, p1}, Landroidx/collection/ArrayMap;->removeAll(Ljava/util/Collection;)Z

    move-result v0

    return v0
.end method

.method public retainAll(Ljava/util/Collection;)Z
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/Collection<",
            "*>;)Z"
        }
    .end annotation

    .line 259
    .local p0, "this":Landroidx/collection/ArrayMap$KeySet;, "Landroidx/collection/ArrayMap<TK;TV;>.KeySet;"
    .local p1, "collection":Ljava/util/Collection;, "Ljava/util/Collection<*>;"
    iget-object v0, p0, Landroidx/collection/ArrayMap$KeySet;->this$0:Landroidx/collection/ArrayMap;

    invoke-virtual {v0, p1}, Landroidx/collection/ArrayMap;->retainAll(Ljava/util/Collection;)Z

    move-result v0

    return v0
.end method

.method public size()I
    .locals 1

    .line 264
    .local p0, "this":Landroidx/collection/ArrayMap$KeySet;, "Landroidx/collection/ArrayMap<TK;TV;>.KeySet;"
    iget-object v0, p0, Landroidx/collection/ArrayMap$KeySet;->this$0:Landroidx/collection/ArrayMap;

    iget v0, v0, Landroidx/collection/ArrayMap;->mSize:I

    return v0
.end method

.method public toArray()[Ljava/lang/Object;
    .locals 4

    .line 269
    .local p0, "this":Landroidx/collection/ArrayMap$KeySet;, "Landroidx/collection/ArrayMap<TK;TV;>.KeySet;"
    iget-object v0, p0, Landroidx/collection/ArrayMap$KeySet;->this$0:Landroidx/collection/ArrayMap;

    iget v0, v0, Landroidx/collection/ArrayMap;->mSize:I

    .line 270
    .local v0, "N":I
    new-array v1, v0, [Ljava/lang/Object;

    .line 271
    .local v1, "result":[Ljava/lang/Object;
    const/4 v2, 0x0

    .local v2, "i":I
    :goto_0
    if-ge v2, v0, :cond_0

    .line 272
    iget-object v3, p0, Landroidx/collection/ArrayMap$KeySet;->this$0:Landroidx/collection/ArrayMap;

    invoke-virtual {v3, v2}, Landroidx/collection/ArrayMap;->keyAt(I)Ljava/lang/Object;

    move-result-object v3

    aput-object v3, v1, v2

    .line 271
    add-int/lit8 v2, v2, 0x1

    goto :goto_0

    .line 274
    .end local v2    # "i":I
    :cond_0
    return-object v1
.end method

.method public toArray([Ljava/lang/Object;)[Ljava/lang/Object;
    .locals 2
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "<T:",
            "Ljava/lang/Object;",
            ">([TT;)[TT;"
        }
    .end annotation

    .line 279
    .local p0, "this":Landroidx/collection/ArrayMap$KeySet;, "Landroidx/collection/ArrayMap<TK;TV;>.KeySet;"
    .local p1, "array":[Ljava/lang/Object;, "[TT;"
    iget-object v0, p0, Landroidx/collection/ArrayMap$KeySet;->this$0:Landroidx/collection/ArrayMap;

    const/4 v1, 0x0

    invoke-virtual {v0, p1, v1}, Landroidx/collection/ArrayMap;->toArrayHelper([Ljava/lang/Object;I)[Ljava/lang/Object;

    move-result-object v0

    return-object v0
.end method
