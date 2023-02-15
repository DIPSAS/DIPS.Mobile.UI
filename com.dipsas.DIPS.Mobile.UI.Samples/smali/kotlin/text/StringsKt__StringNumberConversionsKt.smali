.class Lkotlin/text/StringsKt__StringNumberConversionsKt;
.super Lkotlin/text/StringsKt__StringNumberConversionsJVMKt;
.source "StringNumberConversions.kt"


# annotations
.annotation runtime Lkotlin/Metadata;
    d1 = {
        "\u0000.\n\u0000\n\u0002\u0010\u0001\n\u0000\n\u0002\u0010\u000e\n\u0000\n\u0002\u0010\u0005\n\u0002\u0008\u0002\n\u0002\u0010\u0008\n\u0002\u0008\u0005\n\u0002\u0010\t\n\u0002\u0008\u0003\n\u0002\u0010\n\n\u0002\u0008\u0003\u001a\u0010\u0010\u0000\u001a\u00020\u00012\u0006\u0010\u0002\u001a\u00020\u0003H\u0000\u001a\u0013\u0010\u0004\u001a\u0004\u0018\u00010\u0005*\u00020\u0003H\u0007\u00a2\u0006\u0002\u0010\u0006\u001a\u001b\u0010\u0004\u001a\u0004\u0018\u00010\u0005*\u00020\u00032\u0006\u0010\u0007\u001a\u00020\u0008H\u0007\u00a2\u0006\u0002\u0010\t\u001a\u0013\u0010\n\u001a\u0004\u0018\u00010\u0008*\u00020\u0003H\u0007\u00a2\u0006\u0002\u0010\u000b\u001a\u001b\u0010\n\u001a\u0004\u0018\u00010\u0008*\u00020\u00032\u0006\u0010\u0007\u001a\u00020\u0008H\u0007\u00a2\u0006\u0002\u0010\u000c\u001a\u0013\u0010\r\u001a\u0004\u0018\u00010\u000e*\u00020\u0003H\u0007\u00a2\u0006\u0002\u0010\u000f\u001a\u001b\u0010\r\u001a\u0004\u0018\u00010\u000e*\u00020\u00032\u0006\u0010\u0007\u001a\u00020\u0008H\u0007\u00a2\u0006\u0002\u0010\u0010\u001a\u0013\u0010\u0011\u001a\u0004\u0018\u00010\u0012*\u00020\u0003H\u0007\u00a2\u0006\u0002\u0010\u0013\u001a\u001b\u0010\u0011\u001a\u0004\u0018\u00010\u0012*\u00020\u00032\u0006\u0010\u0007\u001a\u00020\u0008H\u0007\u00a2\u0006\u0002\u0010\u0014\u00a8\u0006\u0015"
    }
    d2 = {
        "numberFormatError",
        "",
        "input",
        "",
        "toByteOrNull",
        "",
        "(Ljava/lang/String;)Ljava/lang/Byte;",
        "radix",
        "",
        "(Ljava/lang/String;I)Ljava/lang/Byte;",
        "toIntOrNull",
        "(Ljava/lang/String;)Ljava/lang/Integer;",
        "(Ljava/lang/String;I)Ljava/lang/Integer;",
        "toLongOrNull",
        "",
        "(Ljava/lang/String;)Ljava/lang/Long;",
        "(Ljava/lang/String;I)Ljava/lang/Long;",
        "toShortOrNull",
        "",
        "(Ljava/lang/String;)Ljava/lang/Short;",
        "(Ljava/lang/String;I)Ljava/lang/Short;",
        "kotlin-stdlib"
    }
    k = 0x5
    mv = {
        0x1,
        0x6,
        0x0
    }
    xi = 0x31
    xs = "kotlin/text/StringsKt"
.end annotation


# direct methods
.method public constructor <init>()V
    .locals 0

    invoke-direct {p0}, Lkotlin/text/StringsKt__StringNumberConversionsJVMKt;-><init>()V

    return-void
.end method

.method public static final numberFormatError(Ljava/lang/String;)Ljava/lang/Void;
    .locals 3
    .param p0, "input"    # Ljava/lang/String;

    const-string v0, "input"

    invoke-static {p0, v0}, Lkotlin/jvm/internal/Intrinsics;->checkNotNullParameter(Ljava/lang/Object;Ljava/lang/String;)V

    .line 203
    new-instance v0, Ljava/lang/NumberFormatException;

    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "Invalid number format: \'"

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v1

    invoke-virtual {v1, p0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v1

    const/16 v2, 0x27

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    move-result-object v1

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-direct {v0, v1}, Ljava/lang/NumberFormatException;-><init>(Ljava/lang/String;)V

    throw v0
.end method

.method public static final toByteOrNull(Ljava/lang/String;)Ljava/lang/Byte;
    .locals 1
    .param p0, "$this$toByteOrNull"    # Ljava/lang/String;

    const-string v0, "<this>"

    invoke-static {p0, v0}, Lkotlin/jvm/internal/Intrinsics;->checkNotNullParameter(Ljava/lang/Object;Ljava/lang/String;)V

    .line 17
    const/16 v0, 0xa

    invoke-static {p0, v0}, Lkotlin/text/StringsKt;->toByteOrNull(Ljava/lang/String;I)Ljava/lang/Byte;

    move-result-object v0

    return-object v0
.end method

.method public static final toByteOrNull(Ljava/lang/String;I)Ljava/lang/Byte;
    .locals 3
    .param p0, "$this$toByteOrNull"    # Ljava/lang/String;
    .param p1, "radix"    # I

    const-string v0, "<this>"

    invoke-static {p0, v0}, Lkotlin/jvm/internal/Intrinsics;->checkNotNullParameter(Ljava/lang/Object;Ljava/lang/String;)V

    .line 27
    invoke-static {p0, p1}, Lkotlin/text/StringsKt;->toIntOrNull(Ljava/lang/String;I)Ljava/lang/Integer;

    move-result-object v0

    const/4 v1, 0x0

    if-nez v0, :cond_0

    return-object v1

    :cond_0
    invoke-virtual {v0}, Ljava/lang/Integer;->intValue()I

    move-result v0

    .line 28
    .local v0, "int":I
    const/16 v2, -0x80

    if-lt v0, v2, :cond_2

    const/16 v2, 0x7f

    if-le v0, v2, :cond_1

    goto :goto_0

    .line 29
    :cond_1
    int-to-byte v1, v0

    invoke-static {v1}, Ljava/lang/Byte;->valueOf(B)Ljava/lang/Byte;

    move-result-object v1

    return-object v1

    .line 28
    :cond_2
    :goto_0
    return-object v1
.end method

.method public static final toIntOrNull(Ljava/lang/String;)Ljava/lang/Integer;
    .locals 1
    .param p0, "$this$toIntOrNull"    # Ljava/lang/String;

    const-string v0, "<this>"

    invoke-static {p0, v0}, Lkotlin/jvm/internal/Intrinsics;->checkNotNullParameter(Ljava/lang/Object;Ljava/lang/String;)V

    .line 57
    const/16 v0, 0xa

    invoke-static {p0, v0}, Lkotlin/text/StringsKt;->toIntOrNull(Ljava/lang/String;I)Ljava/lang/Integer;

    move-result-object v0

    return-object v0
.end method

.method public static final toIntOrNull(Ljava/lang/String;I)Ljava/lang/Integer;
    .locals 13
    .param p0, "$this$toIntOrNull"    # Ljava/lang/String;
    .param p1, "radix"    # I

    const-string v0, "<this>"

    invoke-static {p0, v0}, Lkotlin/jvm/internal/Intrinsics;->checkNotNullParameter(Ljava/lang/Object;Ljava/lang/String;)V

    .line 67
    invoke-static {p1}, Lkotlin/text/CharsKt;->checkRadix(I)I

    .line 69
    invoke-virtual {p0}, Ljava/lang/String;->length()I

    move-result v0

    .line 70
    .local v0, "length":I
    const/4 v1, 0x0

    if-nez v0, :cond_0

    return-object v1

    :cond_0
    const/4 v2, 0x0

    .local v2, "start":I
    const/4 v3, 0x0

    .local v3, "isNegative":Z
    const/4 v4, 0x0

    .line 76
    .local v4, "limit":I
    const/4 v5, 0x0

    invoke-virtual {p0, v5}, Ljava/lang/String;->charAt(I)C

    move-result v5

    .line 77
    .local v5, "firstChar":C
    const/16 v6, 0x30

    invoke-static {v5, v6}, Lkotlin/jvm/internal/Intrinsics;->compare(II)I

    move-result v6

    if-gez v6, :cond_4

    .line 78
    const/4 v6, 0x1

    if-ne v0, v6, :cond_1

    return-object v1

    .line 80
    :cond_1
    const/4 v2, 0x1

    .line 82
    const/16 v6, 0x2d

    if-ne v5, v6, :cond_2

    .line 83
    const/4 v3, 0x1

    .line 84
    const/high16 v4, -0x80000000

    goto :goto_0

    .line 85
    :cond_2
    const/16 v6, 0x2b

    if-ne v5, v6, :cond_3

    .line 86
    const/4 v3, 0x0

    .line 87
    const v4, -0x7fffffff

    goto :goto_0

    .line 89
    :cond_3
    return-object v1

    .line 91
    :cond_4
    const/4 v2, 0x0

    .line 92
    const/4 v3, 0x0

    .line 93
    const v4, -0x7fffffff

    .line 97
    :goto_0
    const v6, -0x38e38e3

    .line 99
    .local v6, "limitForMaxRadix":I
    move v7, v6

    .line 100
    .local v7, "limitBeforeMul":I
    const/4 v8, 0x0

    .line 101
    .local v8, "result":I
    move v9, v2

    :goto_1
    if-ge v9, v0, :cond_9

    move v10, v9

    .local v10, "i":I
    add-int/lit8 v9, v9, 0x1

    .line 102
    invoke-virtual {p0, v10}, Ljava/lang/String;->charAt(I)C

    move-result v11

    invoke-static {v11, p1}, Lkotlin/text/CharsKt;->digitOf(CI)I

    move-result v11

    .line 104
    .local v11, "digit":I
    if-gez v11, :cond_5

    return-object v1

    .line 105
    :cond_5
    if-ge v8, v7, :cond_7

    .line 106
    if-ne v7, v6, :cond_6

    .line 107
    div-int v7, v4, p1

    .line 109
    if-ge v8, v7, :cond_7

    .line 110
    return-object v1

    .line 113
    :cond_6
    return-object v1

    .line 117
    :cond_7
    mul-int v8, v8, p1

    .line 119
    add-int v12, v4, v11

    if-ge v8, v12, :cond_8

    return-object v1

    .line 121
    :cond_8
    sub-int/2addr v8, v11

    .end local v11    # "digit":I
    goto :goto_1

    .line 124
    .end local v10    # "i":I
    :cond_9
    if-eqz v3, :cond_a

    invoke-static {v8}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v1

    goto :goto_2

    :cond_a
    neg-int v1, v8

    invoke-static {v1}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v1

    :goto_2
    return-object v1
.end method

.method public static final toLongOrNull(Ljava/lang/String;)Ljava/lang/Long;
    .locals 1
    .param p0, "$this$toLongOrNull"    # Ljava/lang/String;

    const-string v0, "<this>"

    invoke-static {p0, v0}, Lkotlin/jvm/internal/Intrinsics;->checkNotNullParameter(Ljava/lang/Object;Ljava/lang/String;)V

    .line 132
    const/16 v0, 0xa

    invoke-static {p0, v0}, Lkotlin/text/StringsKt;->toLongOrNull(Ljava/lang/String;I)Ljava/lang/Long;

    move-result-object v0

    return-object v0
.end method

.method public static final toLongOrNull(Ljava/lang/String;I)Ljava/lang/Long;
    .locals 21
    .param p0, "$this$toLongOrNull"    # Ljava/lang/String;
    .param p1, "radix"    # I

    move-object/from16 v0, p0

    move/from16 v1, p1

    const-string v2, "<this>"

    invoke-static {v0, v2}, Lkotlin/jvm/internal/Intrinsics;->checkNotNullParameter(Ljava/lang/Object;Ljava/lang/String;)V

    .line 142
    invoke-static/range {p1 .. p1}, Lkotlin/text/CharsKt;->checkRadix(I)I

    .line 144
    invoke-virtual/range {p0 .. p0}, Ljava/lang/String;->length()I

    move-result v2

    .line 145
    .local v2, "length":I
    const/4 v3, 0x0

    if-nez v2, :cond_0

    return-object v3

    :cond_0
    const/4 v4, 0x0

    .local v4, "start":I
    const/4 v5, 0x0

    .local v5, "isNegative":Z
    const-wide/16 v6, 0x0

    .line 151
    .local v6, "limit":J
    const/4 v8, 0x0

    invoke-virtual {v0, v8}, Ljava/lang/String;->charAt(I)C

    move-result v8

    .line 152
    .local v8, "firstChar":C
    const/16 v9, 0x30

    invoke-static {v8, v9}, Lkotlin/jvm/internal/Intrinsics;->compare(II)I

    move-result v9

    if-gez v9, :cond_4

    .line 153
    const/4 v9, 0x1

    if-ne v2, v9, :cond_1

    return-object v3

    .line 155
    :cond_1
    const/4 v4, 0x1

    .line 157
    const/16 v9, 0x2d

    if-ne v8, v9, :cond_2

    .line 158
    const/4 v5, 0x1

    .line 159
    const-wide/high16 v6, -0x8000000000000000L

    goto :goto_0

    .line 160
    :cond_2
    const/16 v9, 0x2b

    if-ne v8, v9, :cond_3

    .line 161
    const/4 v5, 0x0

    .line 162
    const-wide v6, -0x7fffffffffffffffL    # -4.9E-324

    goto :goto_0

    .line 164
    :cond_3
    return-object v3

    .line 166
    :cond_4
    const/4 v4, 0x0

    .line 167
    const/4 v5, 0x0

    .line 168
    const-wide v6, -0x7fffffffffffffffL    # -4.9E-324

    .line 172
    :goto_0
    const-wide v9, -0x38e38e38e38e38eL    # -2.772000429909333E291

    .line 174
    .local v9, "limitForMaxRadix":J
    move-wide v11, v9

    .line 175
    .local v11, "limitBeforeMul":J
    const-wide/16 v13, 0x0

    .line 176
    .local v13, "result":J
    move v15, v4

    :goto_1
    if-ge v15, v2, :cond_a

    move/from16 v16, v15

    .local v16, "i":I
    add-int/lit8 v15, v15, 0x1

    .line 177
    move/from16 v3, v16

    move/from16 v16, v2

    .end local v2    # "length":I
    .local v3, "i":I
    .local v16, "length":I
    invoke-virtual {v0, v3}, Ljava/lang/String;->charAt(I)C

    move-result v2

    invoke-static {v2, v1}, Lkotlin/text/CharsKt;->digitOf(CI)I

    move-result v2

    .line 179
    .local v2, "digit":I
    if-gez v2, :cond_5

    const/4 v15, 0x0

    return-object v15

    .line 180
    :cond_5
    cmp-long v18, v13, v11

    if-gez v18, :cond_8

    .line 181
    cmp-long v18, v11, v9

    if-nez v18, :cond_7

    .line 182
    move/from16 v19, v3

    move/from16 v18, v4

    .end local v3    # "i":I
    .end local v4    # "start":I
    .local v18, "start":I
    .local v19, "i":I
    int-to-long v3, v1

    div-long v3, v6, v3

    .line 184
    .end local v11    # "limitBeforeMul":J
    .local v3, "limitBeforeMul":J
    cmp-long v11, v13, v3

    if-gez v11, :cond_6

    .line 185
    const/16 v17, 0x0

    return-object v17

    .line 184
    :cond_6
    const/16 v17, 0x0

    move-wide v11, v3

    goto :goto_2

    .line 188
    .end local v18    # "start":I
    .end local v19    # "i":I
    .local v3, "i":I
    .restart local v4    # "start":I
    .restart local v11    # "limitBeforeMul":J
    :cond_7
    const/16 v17, 0x0

    return-object v17

    .line 180
    :cond_8
    move/from16 v19, v3

    move/from16 v18, v4

    .line 192
    .end local v3    # "i":I
    .end local v4    # "start":I
    .restart local v18    # "start":I
    .restart local v19    # "i":I
    :goto_2
    int-to-long v3, v1

    mul-long v13, v13, v3

    .line 194
    int-to-long v3, v2

    add-long/2addr v3, v6

    cmp-long v20, v13, v3

    if-gez v20, :cond_9

    const/4 v3, 0x0

    return-object v3

    .line 196
    :cond_9
    const/4 v3, 0x0

    int-to-long v3, v2

    sub-long/2addr v13, v3

    move/from16 v2, v16

    move/from16 v4, v18

    const/4 v3, 0x0

    .end local v2    # "digit":I
    goto :goto_1

    .line 199
    .end local v16    # "length":I
    .end local v18    # "start":I
    .end local v19    # "i":I
    .local v2, "length":I
    .restart local v4    # "start":I
    :cond_a
    move/from16 v16, v2

    move/from16 v18, v4

    .end local v2    # "length":I
    .end local v4    # "start":I
    .restart local v16    # "length":I
    .restart local v18    # "start":I
    if-eqz v5, :cond_b

    invoke-static {v13, v14}, Ljava/lang/Long;->valueOf(J)Ljava/lang/Long;

    move-result-object v2

    goto :goto_3

    :cond_b
    neg-long v2, v13

    invoke-static {v2, v3}, Ljava/lang/Long;->valueOf(J)Ljava/lang/Long;

    move-result-object v2

    :goto_3
    return-object v2
.end method

.method public static final toShortOrNull(Ljava/lang/String;)Ljava/lang/Short;
    .locals 1
    .param p0, "$this$toShortOrNull"    # Ljava/lang/String;

    const-string v0, "<this>"

    invoke-static {p0, v0}, Lkotlin/jvm/internal/Intrinsics;->checkNotNullParameter(Ljava/lang/Object;Ljava/lang/String;)V

    .line 37
    const/16 v0, 0xa

    invoke-static {p0, v0}, Lkotlin/text/StringsKt;->toShortOrNull(Ljava/lang/String;I)Ljava/lang/Short;

    move-result-object v0

    return-object v0
.end method

.method public static final toShortOrNull(Ljava/lang/String;I)Ljava/lang/Short;
    .locals 3
    .param p0, "$this$toShortOrNull"    # Ljava/lang/String;
    .param p1, "radix"    # I

    const-string v0, "<this>"

    invoke-static {p0, v0}, Lkotlin/jvm/internal/Intrinsics;->checkNotNullParameter(Ljava/lang/Object;Ljava/lang/String;)V

    .line 47
    invoke-static {p0, p1}, Lkotlin/text/StringsKt;->toIntOrNull(Ljava/lang/String;I)Ljava/lang/Integer;

    move-result-object v0

    const/4 v1, 0x0

    if-nez v0, :cond_0

    return-object v1

    :cond_0
    invoke-virtual {v0}, Ljava/lang/Integer;->intValue()I

    move-result v0

    .line 48
    .local v0, "int":I
    const/16 v2, -0x8000

    if-lt v0, v2, :cond_2

    const/16 v2, 0x7fff

    if-le v0, v2, :cond_1

    goto :goto_0

    .line 49
    :cond_1
    int-to-short v1, v0

    invoke-static {v1}, Ljava/lang/Short;->valueOf(S)Ljava/lang/Short;

    move-result-object v1

    return-object v1

    .line 48
    :cond_2
    :goto_0
    return-object v1
.end method
