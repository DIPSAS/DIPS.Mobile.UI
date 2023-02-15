.class final Landroidx/core/view/ViewGroupKt$descendants$1;
.super Lkotlin/coroutines/jvm/internal/RestrictedSuspendLambda;
.source "ViewGroup.kt"

# interfaces
.implements Lkotlin/jvm/functions/Function2;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Landroidx/core/view/ViewGroupKt;->getDescendants(Landroid/view/ViewGroup;)Lkotlin/sequences/Sequence;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x18
    name = null
.end annotation

.annotation system Ldalvik/annotation/Signature;
    value = {
        "Lkotlin/coroutines/jvm/internal/RestrictedSuspendLambda;",
        "Lkotlin/jvm/functions/Function2<",
        "Lkotlin/sequences/SequenceScope<",
        "-",
        "Landroid/view/View;",
        ">;",
        "Lkotlin/coroutines/Continuation<",
        "-",
        "Lkotlin/Unit;",
        ">;",
        "Ljava/lang/Object;",
        ">;"
    }
.end annotation

.annotation system Ldalvik/annotation/SourceDebugExtension;
    value = "SMAP\nViewGroup.kt\nKotlin\n*S Kotlin\n*F\n+ 1 ViewGroup.kt\nandroidx/core/view/ViewGroupKt$descendants$1\n+ 2 ViewGroup.kt\nandroidx/core/view/ViewGroupKt\n*L\n1#1,147:1\n54#2,4:148\n*S KotlinDebug\n*F\n+ 1 ViewGroup.kt\nandroidx/core/view/ViewGroupKt$descendants$1\n*L\n96#1:148,4\n*E\n"
.end annotation

.annotation runtime Lkotlin/Metadata;
    d1 = {
        "\u0000\u000e\n\u0000\n\u0002\u0010\u0002\n\u0002\u0018\u0002\n\u0002\u0018\u0002\u0010\u0000\u001a\u00020\u0001*\u0008\u0012\u0004\u0012\u00020\u00030\u0002H\u008a@"
    }
    d2 = {
        "<anonymous>",
        "",
        "Lkotlin/sequences/SequenceScope;",
        "Landroid/view/View;"
    }
    k = 0x3
    mv = {
        0x1,
        0x5,
        0x1
    }
    xi = 0x30
.end annotation

.annotation runtime Lkotlin/coroutines/jvm/internal/DebugMetadata;
    c = "androidx.core.view.ViewGroupKt$descendants$1"
    f = "ViewGroup.kt"
    i = {
        0x0,
        0x0,
        0x0,
        0x1,
        0x1
    }
    l = {
        0x61,
        0x63
    }
    m = "invokeSuspend"
    n = {
        "$this$sequence",
        "$this$forEach$iv",
        "child",
        "$this$sequence",
        "$this$forEach$iv"
    }
    s = {
        "L$0",
        "L$1",
        "L$2",
        "L$0",
        "L$1"
    }
.end annotation


# instance fields
.field final synthetic $this_descendants:Landroid/view/ViewGroup;

.field I$0:I

.field I$1:I

.field private synthetic L$0:Ljava/lang/Object;

.field L$1:Ljava/lang/Object;

.field L$2:Ljava/lang/Object;

.field label:I


# direct methods
.method constructor <init>(Landroid/view/ViewGroup;Lkotlin/coroutines/Continuation;)V
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Landroid/view/ViewGroup;",
            "Lkotlin/coroutines/Continuation<",
            "-",
            "Landroidx/core/view/ViewGroupKt$descendants$1;",
            ">;)V"
        }
    .end annotation

    iput-object p1, p0, Landroidx/core/view/ViewGroupKt$descendants$1;->$this_descendants:Landroid/view/ViewGroup;

    const/4 v0, 0x2

    invoke-direct {p0, v0, p2}, Lkotlin/coroutines/jvm/internal/RestrictedSuspendLambda;-><init>(ILkotlin/coroutines/Continuation;)V

    return-void
.end method


# virtual methods
.method public final create(Ljava/lang/Object;Lkotlin/coroutines/Continuation;)Lkotlin/coroutines/Continuation;
    .locals 2
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/lang/Object;",
            "Lkotlin/coroutines/Continuation<",
            "*>;)",
            "Lkotlin/coroutines/Continuation<",
            "Lkotlin/Unit;",
            ">;"
        }
    .end annotation

    new-instance v0, Landroidx/core/view/ViewGroupKt$descendants$1;

    iget-object v1, p0, Landroidx/core/view/ViewGroupKt$descendants$1;->$this_descendants:Landroid/view/ViewGroup;

    invoke-direct {v0, v1, p2}, Landroidx/core/view/ViewGroupKt$descendants$1;-><init>(Landroid/view/ViewGroup;Lkotlin/coroutines/Continuation;)V

    iput-object p1, v0, Landroidx/core/view/ViewGroupKt$descendants$1;->L$0:Ljava/lang/Object;

    check-cast v0, Lkotlin/coroutines/Continuation;

    return-object v0
.end method

.method public bridge synthetic invoke(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;
    .locals 1

    check-cast p1, Lkotlin/sequences/SequenceScope;

    check-cast p2, Lkotlin/coroutines/Continuation;

    invoke-virtual {p0, p1, p2}, Landroidx/core/view/ViewGroupKt$descendants$1;->invoke(Lkotlin/sequences/SequenceScope;Lkotlin/coroutines/Continuation;)Ljava/lang/Object;

    move-result-object v0

    return-object v0
.end method

.method public final invoke(Lkotlin/sequences/SequenceScope;Lkotlin/coroutines/Continuation;)Ljava/lang/Object;
    .locals 2
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Lkotlin/sequences/SequenceScope<",
            "-",
            "Landroid/view/View;",
            ">;",
            "Lkotlin/coroutines/Continuation<",
            "-",
            "Lkotlin/Unit;",
            ">;)",
            "Ljava/lang/Object;"
        }
    .end annotation

    invoke-virtual {p0, p1, p2}, Landroidx/core/view/ViewGroupKt$descendants$1;->create(Ljava/lang/Object;Lkotlin/coroutines/Continuation;)Lkotlin/coroutines/Continuation;

    move-result-object v0

    check-cast v0, Landroidx/core/view/ViewGroupKt$descendants$1;

    sget-object v1, Lkotlin/Unit;->INSTANCE:Lkotlin/Unit;

    invoke-virtual {v0, v1}, Landroidx/core/view/ViewGroupKt$descendants$1;->invokeSuspend(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v0

    return-object v0
.end method

.method public final invokeSuspend(Ljava/lang/Object;)Ljava/lang/Object;
    .locals 11

    invoke-static {}, Lkotlin/coroutines/intrinsics/IntrinsicsKt;->getCOROUTINE_SUSPENDED()Ljava/lang/Object;

    move-result-object v0

    .line 95
    iget v1, p0, Landroidx/core/view/ViewGroupKt$descendants$1;->label:I

    packed-switch v1, :pswitch_data_0

    .line 102
    new-instance p1, Ljava/lang/IllegalStateException;

    const-string v0, "call to \'resume\' before \'invoke\' with coroutine"

    invoke-direct {p1, v0}, Ljava/lang/IllegalStateException;-><init>(Ljava/lang/String;)V

    throw p1

    .line 95
    :pswitch_0
    move-object v1, p0

    .local v1, "this":Landroidx/core/view/ViewGroupKt$descendants$1;
    .local p1, "$result":Ljava/lang/Object;
    const/4 v2, 0x0

    .local v2, "$i$f$forEach":I
    const/4 v3, 0x0

    .local v3, "$i$a$-forEach-ViewGroupKt$descendants$1$1":I
    iget v4, v1, Landroidx/core/view/ViewGroupKt$descendants$1;->I$1:I

    iget v5, v1, Landroidx/core/view/ViewGroupKt$descendants$1;->I$0:I

    iget-object v6, v1, Landroidx/core/view/ViewGroupKt$descendants$1;->L$1:Ljava/lang/Object;

    check-cast v6, Landroid/view/ViewGroup;

    .local v6, "$this$forEach$iv":Landroid/view/ViewGroup;
    iget-object v7, v1, Landroidx/core/view/ViewGroupKt$descendants$1;->L$0:Ljava/lang/Object;

    check-cast v7, Lkotlin/sequences/SequenceScope;

    .local v7, "$this$sequence":Lkotlin/sequences/SequenceScope;
    invoke-static {p1}, Lkotlin/ResultKt;->throwOnFailure(Ljava/lang/Object;)V

    goto/16 :goto_1

    .end local v1    # "this":Landroidx/core/view/ViewGroupKt$descendants$1;
    .end local v2    # "$i$f$forEach":I
    .end local v3    # "$i$a$-forEach-ViewGroupKt$descendants$1$1":I
    .end local v6    # "$this$forEach$iv":Landroid/view/ViewGroup;
    .end local v7    # "$this$sequence":Lkotlin/sequences/SequenceScope;
    .end local p1    # "$result":Ljava/lang/Object;
    :pswitch_1
    move-object v1, p0

    .restart local v1    # "this":Landroidx/core/view/ViewGroupKt$descendants$1;
    .restart local p1    # "$result":Ljava/lang/Object;
    const/4 v2, 0x0

    .restart local v2    # "$i$f$forEach":I
    const/4 v3, 0x0

    .restart local v3    # "$i$a$-forEach-ViewGroupKt$descendants$1$1":I
    iget v4, v1, Landroidx/core/view/ViewGroupKt$descendants$1;->I$1:I

    iget v5, v1, Landroidx/core/view/ViewGroupKt$descendants$1;->I$0:I

    iget-object v6, v1, Landroidx/core/view/ViewGroupKt$descendants$1;->L$2:Ljava/lang/Object;

    check-cast v6, Landroid/view/View;

    .local v6, "child":Landroid/view/View;
    iget-object v7, v1, Landroidx/core/view/ViewGroupKt$descendants$1;->L$1:Ljava/lang/Object;

    check-cast v7, Landroid/view/ViewGroup;

    .local v7, "$this$forEach$iv":Landroid/view/ViewGroup;
    iget-object v8, v1, Landroidx/core/view/ViewGroupKt$descendants$1;->L$0:Ljava/lang/Object;

    check-cast v8, Lkotlin/sequences/SequenceScope;

    .local v8, "$this$sequence":Lkotlin/sequences/SequenceScope;
    invoke-static {p1}, Lkotlin/ResultKt;->throwOnFailure(Ljava/lang/Object;)V

    goto :goto_0

    .end local v1    # "this":Landroidx/core/view/ViewGroupKt$descendants$1;
    .end local v2    # "$i$f$forEach":I
    .end local v3    # "$i$a$-forEach-ViewGroupKt$descendants$1$1":I
    .end local v6    # "child":Landroid/view/View;
    .end local v7    # "$this$forEach$iv":Landroid/view/ViewGroup;
    .end local v8    # "$this$sequence":Lkotlin/sequences/SequenceScope;
    .end local p1    # "$result":Ljava/lang/Object;
    :pswitch_2
    invoke-static {p1}, Lkotlin/ResultKt;->throwOnFailure(Ljava/lang/Object;)V

    move-object v1, p0

    .restart local v1    # "this":Landroidx/core/view/ViewGroupKt$descendants$1;
    .restart local p1    # "$result":Ljava/lang/Object;
    iget-object v2, v1, Landroidx/core/view/ViewGroupKt$descendants$1;->L$0:Ljava/lang/Object;

    check-cast v2, Lkotlin/sequences/SequenceScope;

    .line 96
    .local v2, "$this$sequence":Lkotlin/sequences/SequenceScope;
    iget-object v3, v1, Landroidx/core/view/ViewGroupKt$descendants$1;->$this_descendants:Landroid/view/ViewGroup;

    .local v3, "$this$forEach$iv":Landroid/view/ViewGroup;
    const/4 v4, 0x0

    .line 148
    .local v4, "$i$f$forEach":I
    const/4 v5, 0x0

    invoke-virtual {v3}, Landroid/view/ViewGroup;->getChildCount()I

    move-result v6

    if-lez v6, :cond_4

    :cond_0
    move v7, v5

    .local v7, "index$iv":I
    const/4 v8, 0x1

    add-int/2addr v5, v8

    .line 149
    invoke-virtual {v3, v7}, Landroid/view/ViewGroup;->getChildAt(I)Landroid/view/View;

    move-result-object v9

    .end local v7    # "index$iv":I
    const-string v7, "getChildAt(index)"

    invoke-static {v9, v7}, Lkotlin/jvm/internal/Intrinsics;->checkNotNullExpressionValue(Ljava/lang/Object;Ljava/lang/String;)V

    move-object v7, v9

    .local v7, "child":Landroid/view/View;
    const/4 v9, 0x0

    .line 97
    .local v9, "$i$a$-forEach-ViewGroupKt$descendants$1$1":I
    iput-object v2, v1, Landroidx/core/view/ViewGroupKt$descendants$1;->L$0:Ljava/lang/Object;

    iput-object v3, v1, Landroidx/core/view/ViewGroupKt$descendants$1;->L$1:Ljava/lang/Object;

    iput-object v7, v1, Landroidx/core/view/ViewGroupKt$descendants$1;->L$2:Ljava/lang/Object;

    iput v5, v1, Landroidx/core/view/ViewGroupKt$descendants$1;->I$0:I

    iput v6, v1, Landroidx/core/view/ViewGroupKt$descendants$1;->I$1:I

    iput v8, v1, Landroidx/core/view/ViewGroupKt$descendants$1;->label:I

    invoke-virtual {v2, v7, v1}, Lkotlin/sequences/SequenceScope;->yield(Ljava/lang/Object;Lkotlin/coroutines/Continuation;)Ljava/lang/Object;

    move-result-object v8

    if-ne v8, v0, :cond_1

    .line 95
    .end local v2    # "$this$sequence":Lkotlin/sequences/SequenceScope;
    .end local v3    # "$this$forEach$iv":Landroid/view/ViewGroup;
    .end local v7    # "child":Landroid/view/View;
    return-object v0

    .line 97
    .restart local v2    # "$this$sequence":Lkotlin/sequences/SequenceScope;
    .restart local v3    # "$this$forEach$iv":Landroid/view/ViewGroup;
    .restart local v7    # "child":Landroid/view/View;
    :cond_1
    move-object v8, v2

    move v2, v4

    move v4, v6

    move-object v6, v7

    move-object v7, v3

    move v3, v9

    .line 98
    .end local v4    # "$i$f$forEach":I
    .end local v9    # "$i$a$-forEach-ViewGroupKt$descendants$1$1":I
    .local v2, "$i$f$forEach":I
    .local v3, "$i$a$-forEach-ViewGroupKt$descendants$1$1":I
    .restart local v6    # "child":Landroid/view/View;
    .local v7, "$this$forEach$iv":Landroid/view/ViewGroup;
    .restart local v8    # "$this$sequence":Lkotlin/sequences/SequenceScope;
    :goto_0
    instance-of v9, v6, Landroid/view/ViewGroup;

    if-eqz v9, :cond_3

    .line 99
    move-object v9, v6

    check-cast v9, Landroid/view/ViewGroup;

    invoke-static {v9}, Landroidx/core/view/ViewGroupKt;->getDescendants(Landroid/view/ViewGroup;)Lkotlin/sequences/Sequence;

    move-result-object v9

    iput-object v8, v1, Landroidx/core/view/ViewGroupKt$descendants$1;->L$0:Ljava/lang/Object;

    iput-object v7, v1, Landroidx/core/view/ViewGroupKt$descendants$1;->L$1:Ljava/lang/Object;

    const/4 v10, 0x0

    iput-object v10, v1, Landroidx/core/view/ViewGroupKt$descendants$1;->L$2:Ljava/lang/Object;

    iput v5, v1, Landroidx/core/view/ViewGroupKt$descendants$1;->I$0:I

    iput v4, v1, Landroidx/core/view/ViewGroupKt$descendants$1;->I$1:I

    const/4 v10, 0x2

    iput v10, v1, Landroidx/core/view/ViewGroupKt$descendants$1;->label:I

    invoke-virtual {v8, v9, v1}, Lkotlin/sequences/SequenceScope;->yieldAll(Lkotlin/sequences/Sequence;Lkotlin/coroutines/Continuation;)Ljava/lang/Object;

    move-result-object v6

    .end local v6    # "child":Landroid/view/View;
    if-ne v6, v0, :cond_2

    .line 95
    .end local v7    # "$this$forEach$iv":Landroid/view/ViewGroup;
    .end local v8    # "$this$sequence":Lkotlin/sequences/SequenceScope;
    return-object v0

    .line 99
    .restart local v7    # "$this$forEach$iv":Landroid/view/ViewGroup;
    .restart local v8    # "$this$sequence":Lkotlin/sequences/SequenceScope;
    :cond_2
    move-object v6, v7

    move-object v7, v8

    .line 101
    .end local v8    # "$this$sequence":Lkotlin/sequences/SequenceScope;
    .local v6, "$this$forEach$iv":Landroid/view/ViewGroup;
    .local v7, "$this$sequence":Lkotlin/sequences/SequenceScope;
    :goto_1
    move-object v3, v6

    move v6, v4

    move v4, v2

    move-object v2, v7

    goto :goto_2

    .line 98
    .local v6, "child":Landroid/view/View;
    .local v7, "$this$forEach$iv":Landroid/view/ViewGroup;
    .restart local v8    # "$this$sequence":Lkotlin/sequences/SequenceScope;
    :cond_3
    move v6, v4

    move-object v3, v7

    move v4, v2

    move-object v2, v8

    .line 148
    .end local v6    # "child":Landroid/view/View;
    .end local v7    # "$this$forEach$iv":Landroid/view/ViewGroup;
    .end local v8    # "$this$sequence":Lkotlin/sequences/SequenceScope;
    .local v2, "$this$sequence":Lkotlin/sequences/SequenceScope;
    .local v3, "$this$forEach$iv":Landroid/view/ViewGroup;
    .restart local v4    # "$i$f$forEach":I
    :goto_2
    if-lt v5, v6, :cond_0

    .line 151
    .end local v2    # "$this$sequence":Lkotlin/sequences/SequenceScope;
    .end local v3    # "$this$forEach$iv":Landroid/view/ViewGroup;
    :cond_4
    nop

    .line 102
    .end local v4    # "$i$f$forEach":I
    sget-object v0, Lkotlin/Unit;->INSTANCE:Lkotlin/Unit;

    return-object v0

    :pswitch_data_0
    .packed-switch 0x0
        :pswitch_2
        :pswitch_1
        :pswitch_0
    .end packed-switch
.end method
