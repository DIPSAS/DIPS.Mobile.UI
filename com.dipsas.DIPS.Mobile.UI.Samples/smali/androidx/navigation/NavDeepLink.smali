.class public final Landroidx/navigation/NavDeepLink;
.super Ljava/lang/Object;
.source "NavDeepLink.java"


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Landroidx/navigation/NavDeepLink$Builder;,
        Landroidx/navigation/NavDeepLink$MimeType;,
        Landroidx/navigation/NavDeepLink$ParamQuery;
    }
.end annotation


# static fields
.field private static final SCHEME_PATTERN:Ljava/util/regex/Pattern;


# instance fields
.field private final mAction:Ljava/lang/String;

.field private final mArguments:Ljava/util/ArrayList;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/ArrayList<",
            "Ljava/lang/String;",
            ">;"
        }
    .end annotation
.end field

.field private mExactDeepLink:Z

.field private mIsParameterizedQuery:Z

.field private final mMimeType:Ljava/lang/String;

.field private mMimeTypePattern:Ljava/util/regex/Pattern;

.field private final mParamArgMap:Ljava/util/Map;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Landroidx/navigation/NavDeepLink$ParamQuery;",
            ">;"
        }
    .end annotation
.end field

.field private mPattern:Ljava/util/regex/Pattern;

.field private final mUri:Ljava/lang/String;


# direct methods
.method static constructor <clinit>()V
    .locals 1

    .line 38
    const-string v0, "^[a-zA-Z]+[+\\w\\-.]*:"

    invoke-static {v0}, Ljava/util/regex/Pattern;->compile(Ljava/lang/String;)Ljava/util/regex/Pattern;

    move-result-object v0

    sput-object v0, Landroidx/navigation/NavDeepLink;->SCHEME_PATTERN:Ljava/util/regex/Pattern;

    return-void
.end method

.method constructor <init>(Ljava/lang/String;)V
    .locals 1
    .param p1, "uri"    # Ljava/lang/String;

    .line 127
    const/4 v0, 0x0

    invoke-direct {p0, p1, v0, v0}, Landroidx/navigation/NavDeepLink;-><init>(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V

    .line 128
    return-void
.end method

.method constructor <init>(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V
    .locals 18
    .param p1, "uri"    # Ljava/lang/String;
    .param p2, "action"    # Ljava/lang/String;
    .param p3, "mimeType"    # Ljava/lang/String;

    .line 53
    move-object/from16 v0, p0

    move-object/from16 v1, p1

    move-object/from16 v2, p3

    invoke-direct/range {p0 .. p0}, Ljava/lang/Object;-><init>()V

    .line 40
    new-instance v3, Ljava/util/ArrayList;

    invoke-direct {v3}, Ljava/util/ArrayList;-><init>()V

    iput-object v3, v0, Landroidx/navigation/NavDeepLink;->mArguments:Ljava/util/ArrayList;

    .line 41
    new-instance v3, Ljava/util/HashMap;

    invoke-direct {v3}, Ljava/util/HashMap;-><init>()V

    iput-object v3, v0, Landroidx/navigation/NavDeepLink;->mParamArgMap:Ljava/util/Map;

    .line 43
    const/4 v3, 0x0

    iput-object v3, v0, Landroidx/navigation/NavDeepLink;->mPattern:Ljava/util/regex/Pattern;

    .line 44
    const/4 v4, 0x0

    iput-boolean v4, v0, Landroidx/navigation/NavDeepLink;->mExactDeepLink:Z

    .line 45
    iput-boolean v4, v0, Landroidx/navigation/NavDeepLink;->mIsParameterizedQuery:Z

    .line 50
    iput-object v3, v0, Landroidx/navigation/NavDeepLink;->mMimeTypePattern:Ljava/util/regex/Pattern;

    .line 54
    iput-object v1, v0, Landroidx/navigation/NavDeepLink;->mUri:Ljava/lang/String;

    .line 55
    move-object/from16 v3, p2

    iput-object v3, v0, Landroidx/navigation/NavDeepLink;->mAction:Ljava/lang/String;

    .line 56
    iput-object v2, v0, Landroidx/navigation/NavDeepLink;->mMimeType:Ljava/lang/String;

    .line 57
    if-eqz v1, :cond_7

    .line 58
    invoke-static/range {p1 .. p1}, Landroid/net/Uri;->parse(Ljava/lang/String;)Landroid/net/Uri;

    move-result-object v5

    .line 59
    .local v5, "parameterizedUri":Landroid/net/Uri;
    invoke-virtual {v5}, Landroid/net/Uri;->getQuery()Ljava/lang/String;

    move-result-object v6

    const/4 v7, 0x1

    if-eqz v6, :cond_0

    const/4 v6, 0x1

    goto :goto_0

    :cond_0
    const/4 v6, 0x0

    :goto_0
    iput-boolean v6, v0, Landroidx/navigation/NavDeepLink;->mIsParameterizedQuery:Z

    .line 60
    new-instance v6, Ljava/lang/StringBuilder;

    const-string v8, "^"

    invoke-direct {v6, v8}, Ljava/lang/StringBuilder;-><init>(Ljava/lang/String;)V

    .line 62
    .local v6, "uriRegex":Ljava/lang/StringBuilder;
    sget-object v8, Landroidx/navigation/NavDeepLink;->SCHEME_PATTERN:Ljava/util/regex/Pattern;

    invoke-virtual {v8, v1}, Ljava/util/regex/Pattern;->matcher(Ljava/lang/CharSequence;)Ljava/util/regex/Matcher;

    move-result-object v8

    invoke-virtual {v8}, Ljava/util/regex/Matcher;->find()Z

    move-result v8

    if-nez v8, :cond_1

    .line 63
    const-string v8, "http[s]?://"

    invoke-virtual {v6, v8}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    .line 65
    :cond_1
    const-string v8, "\\{(.+?)\\}"

    invoke-static {v8}, Ljava/util/regex/Pattern;->compile(Ljava/lang/String;)Ljava/util/regex/Pattern;

    move-result-object v8

    .line 66
    .local v8, "fillInPattern":Ljava/util/regex/Pattern;
    iget-boolean v9, v0, Landroidx/navigation/NavDeepLink;->mIsParameterizedQuery:Z

    const-string v10, "\\E.*\\Q"

    const-string v11, ".*"

    if-eqz v9, :cond_6

    .line 67
    const-string v9, "(\\?)"

    invoke-static {v9}, Ljava/util/regex/Pattern;->compile(Ljava/lang/String;)Ljava/util/regex/Pattern;

    move-result-object v9

    invoke-virtual {v9, v1}, Ljava/util/regex/Pattern;->matcher(Ljava/lang/CharSequence;)Ljava/util/regex/Matcher;

    move-result-object v9

    .line 68
    .local v9, "matcher":Ljava/util/regex/Matcher;
    invoke-virtual {v9}, Ljava/util/regex/Matcher;->find()Z

    move-result v12

    if-eqz v12, :cond_2

    .line 69
    invoke-virtual {v9}, Ljava/util/regex/Matcher;->start()I

    move-result v12

    invoke-virtual {v1, v4, v12}, Ljava/lang/String;->substring(II)Ljava/lang/String;

    move-result-object v12

    invoke-direct {v0, v12, v6, v8}, Landroidx/navigation/NavDeepLink;->buildPathRegex(Ljava/lang/String;Ljava/lang/StringBuilder;Ljava/util/regex/Pattern;)Z

    .line 71
    :cond_2
    iput-boolean v4, v0, Landroidx/navigation/NavDeepLink;->mExactDeepLink:Z

    .line 72
    invoke-virtual {v5}, Landroid/net/Uri;->getQueryParameterNames()Ljava/util/Set;

    move-result-object v4

    invoke-interface {v4}, Ljava/util/Set;->iterator()Ljava/util/Iterator;

    move-result-object v4

    :goto_1
    invoke-interface {v4}, Ljava/util/Iterator;->hasNext()Z

    move-result v12

    if-eqz v12, :cond_5

    invoke-interface {v4}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v12

    check-cast v12, Ljava/lang/String;

    .line 73
    .local v12, "paramName":Ljava/lang/String;
    new-instance v13, Ljava/lang/StringBuilder;

    invoke-direct {v13}, Ljava/lang/StringBuilder;-><init>()V

    .line 74
    .local v13, "argRegex":Ljava/lang/StringBuilder;
    invoke-virtual {v5, v12}, Landroid/net/Uri;->getQueryParameter(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v14

    .line 75
    .local v14, "queryParam":Ljava/lang/String;
    invoke-virtual {v8, v14}, Ljava/util/regex/Pattern;->matcher(Ljava/lang/CharSequence;)Ljava/util/regex/Matcher;

    move-result-object v9

    .line 76
    const/4 v15, 0x0

    .line 77
    .local v15, "appendPos":I
    new-instance v16, Landroidx/navigation/NavDeepLink$ParamQuery;

    invoke-direct/range {v16 .. v16}, Landroidx/navigation/NavDeepLink$ParamQuery;-><init>()V

    move-object/from16 v17, v16

    .line 79
    .local v17, "param":Landroidx/navigation/NavDeepLink$ParamQuery;
    :goto_2
    invoke-virtual {v9}, Ljava/util/regex/Matcher;->find()Z

    move-result v16

    if-eqz v16, :cond_3

    .line 80
    invoke-virtual {v9, v7}, Ljava/util/regex/Matcher;->group(I)Ljava/lang/String;

    move-result-object v3

    move-object/from16 v7, v17

    .end local v17    # "param":Landroidx/navigation/NavDeepLink$ParamQuery;
    .local v7, "param":Landroidx/navigation/NavDeepLink$ParamQuery;
    invoke-virtual {v7, v3}, Landroidx/navigation/NavDeepLink$ParamQuery;->addArgumentName(Ljava/lang/String;)V

    .line 81
    nop

    .line 82
    invoke-virtual {v9}, Ljava/util/regex/Matcher;->start()I

    move-result v3

    .line 81
    invoke-virtual {v14, v15, v3}, Ljava/lang/String;->substring(II)Ljava/lang/String;

    move-result-object v3

    invoke-static {v3}, Ljava/util/regex/Pattern;->quote(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v3

    invoke-virtual {v13, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    .line 83
    const-string v3, "(.+?)?"

    invoke-virtual {v13, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    .line 84
    invoke-virtual {v9}, Ljava/util/regex/Matcher;->end()I

    move-result v15

    move-object/from16 v3, p2

    const/4 v7, 0x1

    goto :goto_2

    .line 86
    .end local v7    # "param":Landroidx/navigation/NavDeepLink$ParamQuery;
    .restart local v17    # "param":Landroidx/navigation/NavDeepLink$ParamQuery;
    :cond_3
    move-object/from16 v7, v17

    .end local v17    # "param":Landroidx/navigation/NavDeepLink$ParamQuery;
    .restart local v7    # "param":Landroidx/navigation/NavDeepLink$ParamQuery;
    invoke-virtual {v14}, Ljava/lang/String;->length()I

    move-result v3

    if-ge v15, v3, :cond_4

    .line 87
    invoke-virtual {v14, v15}, Ljava/lang/String;->substring(I)Ljava/lang/String;

    move-result-object v3

    invoke-static {v3}, Ljava/util/regex/Pattern;->quote(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v3

    invoke-virtual {v13, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    .line 91
    :cond_4
    invoke-virtual {v13}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v3

    invoke-virtual {v3, v11, v10}, Ljava/lang/String;->replace(Ljava/lang/CharSequence;Ljava/lang/CharSequence;)Ljava/lang/String;

    move-result-object v3

    invoke-virtual {v7, v3}, Landroidx/navigation/NavDeepLink$ParamQuery;->setParamRegex(Ljava/lang/String;)V

    .line 92
    iget-object v3, v0, Landroidx/navigation/NavDeepLink;->mParamArgMap:Ljava/util/Map;

    invoke-interface {v3, v12, v7}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 93
    .end local v7    # "param":Landroidx/navigation/NavDeepLink$ParamQuery;
    .end local v12    # "paramName":Ljava/lang/String;
    .end local v13    # "argRegex":Ljava/lang/StringBuilder;
    .end local v14    # "queryParam":Ljava/lang/String;
    .end local v15    # "appendPos":I
    move-object/from16 v3, p2

    const/4 v7, 0x1

    goto :goto_1

    .line 94
    .end local v9    # "matcher":Ljava/util/regex/Matcher;
    :cond_5
    goto :goto_3

    .line 95
    :cond_6
    invoke-direct {v0, v1, v6, v8}, Landroidx/navigation/NavDeepLink;->buildPathRegex(Ljava/lang/String;Ljava/lang/StringBuilder;Ljava/util/regex/Pattern;)Z

    move-result v3

    iput-boolean v3, v0, Landroidx/navigation/NavDeepLink;->mExactDeepLink:Z

    .line 100
    :goto_3
    invoke-virtual {v6}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v3

    invoke-virtual {v3, v11, v10}, Ljava/lang/String;->replace(Ljava/lang/CharSequence;Ljava/lang/CharSequence;)Ljava/lang/String;

    move-result-object v3

    .line 101
    .local v3, "finalRegex":Ljava/lang/String;
    const/4 v4, 0x2

    invoke-static {v3, v4}, Ljava/util/regex/Pattern;->compile(Ljava/lang/String;I)Ljava/util/regex/Pattern;

    move-result-object v4

    iput-object v4, v0, Landroidx/navigation/NavDeepLink;->mPattern:Ljava/util/regex/Pattern;

    .line 104
    .end local v3    # "finalRegex":Ljava/lang/String;
    .end local v5    # "parameterizedUri":Landroid/net/Uri;
    .end local v6    # "uriRegex":Ljava/lang/StringBuilder;
    .end local v8    # "fillInPattern":Ljava/util/regex/Pattern;
    :cond_7
    if-eqz v2, :cond_9

    .line 105
    const-string v3, "^[\\s\\S]+/[\\s\\S]+$"

    invoke-static {v3}, Ljava/util/regex/Pattern;->compile(Ljava/lang/String;)Ljava/util/regex/Pattern;

    move-result-object v3

    .line 106
    .local v3, "mimeTypePattern":Ljava/util/regex/Pattern;
    invoke-virtual {v3, v2}, Ljava/util/regex/Pattern;->matcher(Ljava/lang/CharSequence;)Ljava/util/regex/Matcher;

    move-result-object v4

    .line 108
    .local v4, "mimeTypeMatcher":Ljava/util/regex/Matcher;
    invoke-virtual {v4}, Ljava/util/regex/Matcher;->matches()Z

    move-result v5

    if-eqz v5, :cond_8

    .line 114
    new-instance v5, Landroidx/navigation/NavDeepLink$MimeType;

    invoke-direct {v5, v2}, Landroidx/navigation/NavDeepLink$MimeType;-><init>(Ljava/lang/String;)V

    .line 117
    .local v5, "splitMimeType":Landroidx/navigation/NavDeepLink$MimeType;
    new-instance v6, Ljava/lang/StringBuilder;

    invoke-direct {v6}, Ljava/lang/StringBuilder;-><init>()V

    const-string v7, "^("

    invoke-virtual {v6, v7}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v6

    iget-object v7, v5, Landroidx/navigation/NavDeepLink$MimeType;->mType:Ljava/lang/String;

    invoke-virtual {v6, v7}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v6

    const-string v7, "|[*]+)/("

    invoke-virtual {v6, v7}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v6

    iget-object v7, v5, Landroidx/navigation/NavDeepLink$MimeType;->mSubType:Ljava/lang/String;

    invoke-virtual {v6, v7}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v6

    const-string v7, "|[*]+)$"

    invoke-virtual {v6, v7}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v6

    invoke-virtual {v6}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v6

    .line 121
    .local v6, "mimeTypeRegex":Ljava/lang/String;
    const-string v7, "*|[*]"

    const-string v8, "[\\s\\S]"

    invoke-virtual {v6, v7, v8}, Ljava/lang/String;->replace(Ljava/lang/CharSequence;Ljava/lang/CharSequence;)Ljava/lang/String;

    move-result-object v7

    .line 122
    .local v7, "finalRegex":Ljava/lang/String;
    invoke-static {v7}, Ljava/util/regex/Pattern;->compile(Ljava/lang/String;)Ljava/util/regex/Pattern;

    move-result-object v8

    iput-object v8, v0, Landroidx/navigation/NavDeepLink;->mMimeTypePattern:Ljava/util/regex/Pattern;

    goto :goto_4

    .line 109
    .end local v5    # "splitMimeType":Landroidx/navigation/NavDeepLink$MimeType;
    .end local v6    # "mimeTypeRegex":Ljava/lang/String;
    .end local v7    # "finalRegex":Ljava/lang/String;
    :cond_8
    new-instance v5, Ljava/lang/IllegalArgumentException;

    new-instance v6, Ljava/lang/StringBuilder;

    invoke-direct {v6}, Ljava/lang/StringBuilder;-><init>()V

    const-string v7, "The given mimeType "

    invoke-virtual {v6, v7}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v6

    invoke-virtual {v6, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v6

    const-string v7, " does not match to required \"type/subtype\" format"

    invoke-virtual {v6, v7}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v6

    invoke-virtual {v6}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v6

    invoke-direct {v5, v6}, Ljava/lang/IllegalArgumentException;-><init>(Ljava/lang/String;)V

    throw v5

    .line 124
    .end local v3    # "mimeTypePattern":Ljava/util/regex/Pattern;
    .end local v4    # "mimeTypeMatcher":Ljava/util/regex/Matcher;
    :cond_9
    :goto_4
    return-void
.end method

.method private buildPathRegex(Ljava/lang/String;Ljava/lang/StringBuilder;Ljava/util/regex/Pattern;)Z
    .locals 6
    .param p1, "uri"    # Ljava/lang/String;
    .param p2, "uriRegex"    # Ljava/lang/StringBuilder;
    .param p3, "fillInPattern"    # Ljava/util/regex/Pattern;

    .line 132
    invoke-virtual {p3, p1}, Ljava/util/regex/Pattern;->matcher(Ljava/lang/CharSequence;)Ljava/util/regex/Matcher;

    move-result-object v0

    .line 133
    .local v0, "matcher":Ljava/util/regex/Matcher;
    const/4 v1, 0x0

    .line 135
    .local v1, "appendPos":I
    const-string v2, ".*"

    invoke-virtual {p1, v2}, Ljava/lang/String;->contains(Ljava/lang/CharSequence;)Z

    move-result v2

    const/4 v3, 0x1

    xor-int/2addr v2, v3

    .line 136
    .local v2, "exactDeepLink":Z
    :goto_0
    invoke-virtual {v0}, Ljava/util/regex/Matcher;->find()Z

    move-result v4

    if-eqz v4, :cond_0

    .line 137
    invoke-virtual {v0, v3}, Ljava/util/regex/Matcher;->group(I)Ljava/lang/String;

    move-result-object v4

    .line 138
    .local v4, "argName":Ljava/lang/String;
    iget-object v5, p0, Landroidx/navigation/NavDeepLink;->mArguments:Ljava/util/ArrayList;

    invoke-virtual {v5, v4}, Ljava/util/ArrayList;->add(Ljava/lang/Object;)Z

    .line 140
    invoke-virtual {v0}, Ljava/util/regex/Matcher;->start()I

    move-result v5

    invoke-virtual {p1, v1, v5}, Ljava/lang/String;->substring(II)Ljava/lang/String;

    move-result-object v5

    invoke-static {v5}, Ljava/util/regex/Pattern;->quote(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v5

    invoke-virtual {p2, v5}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    .line 141
    const-string v5, "(.+?)"

    invoke-virtual {p2, v5}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    .line 142
    invoke-virtual {v0}, Ljava/util/regex/Matcher;->end()I

    move-result v1

    .line 143
    const/4 v2, 0x0

    .line 144
    .end local v4    # "argName":Ljava/lang/String;
    goto :goto_0

    .line 145
    :cond_0
    invoke-virtual {p1}, Ljava/lang/String;->length()I

    move-result v3

    if-ge v1, v3, :cond_1

    .line 147
    invoke-virtual {p1, v1}, Ljava/lang/String;->substring(I)Ljava/lang/String;

    move-result-object v3

    invoke-static {v3}, Ljava/util/regex/Pattern;->quote(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v3

    invoke-virtual {p2, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    .line 153
    :cond_1
    const-string v3, "($|(\\?(.)*))"

    invoke-virtual {p2, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    .line 154
    return v2
.end method

.method private matchAction(Ljava/lang/String;)Z
    .locals 5
    .param p1, "action"    # Ljava/lang/String;

    .line 184
    const/4 v0, 0x1

    const/4 v1, 0x0

    if-nez p1, :cond_0

    const/4 v2, 0x1

    goto :goto_0

    :cond_0
    const/4 v2, 0x0

    :goto_0
    iget-object v3, p0, Landroidx/navigation/NavDeepLink;->mAction:Ljava/lang/String;

    if-eqz v3, :cond_1

    const/4 v4, 0x1

    goto :goto_1

    :cond_1
    const/4 v4, 0x0

    :goto_1
    if-ne v2, v4, :cond_2

    .line 185
    return v1

    .line 188
    :cond_2
    if-eqz p1, :cond_4

    invoke-virtual {v3, p1}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v2

    if-eqz v2, :cond_3

    goto :goto_2

    :cond_3
    const/4 v0, 0x0

    :cond_4
    :goto_2
    return v0
.end method

.method private matchMimeType(Ljava/lang/String;)Z
    .locals 4
    .param p1, "mimeType"    # Ljava/lang/String;

    .line 193
    const/4 v0, 0x1

    const/4 v1, 0x0

    if-nez p1, :cond_0

    const/4 v2, 0x1

    goto :goto_0

    :cond_0
    const/4 v2, 0x0

    :goto_0
    iget-object v3, p0, Landroidx/navigation/NavDeepLink;->mMimeType:Ljava/lang/String;

    if-eqz v3, :cond_1

    const/4 v3, 0x1

    goto :goto_1

    :cond_1
    const/4 v3, 0x0

    :goto_1
    if-ne v2, v3, :cond_2

    .line 194
    return v1

    .line 198
    :cond_2
    if-eqz p1, :cond_4

    iget-object v2, p0, Landroidx/navigation/NavDeepLink;->mMimeTypePattern:Ljava/util/regex/Pattern;

    invoke-virtual {v2, p1}, Ljava/util/regex/Pattern;->matcher(Ljava/lang/CharSequence;)Ljava/util/regex/Matcher;

    move-result-object v2

    invoke-virtual {v2}, Ljava/util/regex/Matcher;->matches()Z

    move-result v2

    if-eqz v2, :cond_3

    goto :goto_2

    :cond_3
    const/4 v0, 0x0

    :cond_4
    :goto_2
    return v0
.end method

.method private matchUri(Landroid/net/Uri;)Z
    .locals 5
    .param p1, "uri"    # Landroid/net/Uri;

    .line 175
    const/4 v0, 0x1

    const/4 v1, 0x0

    if-nez p1, :cond_0

    const/4 v2, 0x1

    goto :goto_0

    :cond_0
    const/4 v2, 0x0

    :goto_0
    iget-object v3, p0, Landroidx/navigation/NavDeepLink;->mPattern:Ljava/util/regex/Pattern;

    if-eqz v3, :cond_1

    const/4 v4, 0x1

    goto :goto_1

    :cond_1
    const/4 v4, 0x0

    :goto_1
    if-ne v2, v4, :cond_2

    .line 176
    return v1

    .line 179
    :cond_2
    if-eqz p1, :cond_4

    invoke-virtual {p1}, Landroid/net/Uri;->toString()Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v3, v2}, Ljava/util/regex/Pattern;->matcher(Ljava/lang/CharSequence;)Ljava/util/regex/Matcher;

    move-result-object v2

    invoke-virtual {v2}, Ljava/util/regex/Matcher;->matches()Z

    move-result v2

    if-eqz v2, :cond_3

    goto :goto_2

    :cond_3
    const/4 v0, 0x0

    :cond_4
    :goto_2
    return v0
.end method

.method private parseArgument(Landroid/os/Bundle;Ljava/lang/String;Ljava/lang/String;Landroidx/navigation/NavArgument;)Z
    .locals 3
    .param p1, "bundle"    # Landroid/os/Bundle;
    .param p2, "name"    # Ljava/lang/String;
    .param p3, "value"    # Ljava/lang/String;
    .param p4, "argument"    # Landroidx/navigation/NavArgument;

    .line 296
    if-eqz p4, :cond_0

    .line 297
    invoke-virtual {p4}, Landroidx/navigation/NavArgument;->getType()Landroidx/navigation/NavType;

    move-result-object v0

    .line 299
    .local v0, "type":Landroidx/navigation/NavType;, "Landroidx/navigation/NavType<*>;"
    :try_start_0
    invoke-virtual {v0, p1, p2, p3}, Landroidx/navigation/NavType;->parseAndPut(Landroid/os/Bundle;Ljava/lang/String;Ljava/lang/String;)Ljava/lang/Object;
    :try_end_0
    .catch Ljava/lang/IllegalArgumentException; {:try_start_0 .. :try_end_0} :catch_0

    .line 305
    nop

    .line 306
    .end local v0    # "type":Landroidx/navigation/NavType;, "Landroidx/navigation/NavType<*>;"
    goto :goto_0

    .line 300
    .restart local v0    # "type":Landroidx/navigation/NavType;, "Landroidx/navigation/NavType<*>;"
    :catch_0
    move-exception v1

    .line 304
    .local v1, "e":Ljava/lang/IllegalArgumentException;
    const/4 v2, 0x1

    return v2

    .line 307
    .end local v0    # "type":Landroidx/navigation/NavType;, "Landroidx/navigation/NavType<*>;"
    .end local v1    # "e":Ljava/lang/IllegalArgumentException;
    :cond_0
    invoke-virtual {p1, p2, p3}, Landroid/os/Bundle;->putString(Ljava/lang/String;Ljava/lang/String;)V

    .line 309
    :goto_0
    const/4 v0, 0x0

    return v0
.end method


# virtual methods
.method public getAction()Ljava/lang/String;
    .locals 1

    .line 225
    iget-object v0, p0, Landroidx/navigation/NavDeepLink;->mAction:Ljava/lang/String;

    return-object v0
.end method

.method getMatchingArguments(Landroid/net/Uri;Ljava/util/Map;)Landroid/os/Bundle;
    .locals 16
    .param p1, "deepLink"    # Landroid/net/Uri;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Landroid/net/Uri;",
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Landroidx/navigation/NavArgument;",
            ">;)",
            "Landroid/os/Bundle;"
        }
    .end annotation

    .line 250
    .local p2, "arguments":Ljava/util/Map;, "Ljava/util/Map<Ljava/lang/String;Landroidx/navigation/NavArgument;>;"
    move-object/from16 v0, p0

    move-object/from16 v1, p2

    iget-object v2, v0, Landroidx/navigation/NavDeepLink;->mPattern:Ljava/util/regex/Pattern;

    invoke-virtual/range {p1 .. p1}, Landroid/net/Uri;->toString()Ljava/lang/String;

    move-result-object v3

    invoke-virtual {v2, v3}, Ljava/util/regex/Pattern;->matcher(Ljava/lang/CharSequence;)Ljava/util/regex/Matcher;

    move-result-object v2

    .line 251
    .local v2, "matcher":Ljava/util/regex/Matcher;
    invoke-virtual {v2}, Ljava/util/regex/Matcher;->matches()Z

    move-result v3

    const/4 v4, 0x0

    if-nez v3, :cond_0

    .line 252
    return-object v4

    .line 254
    :cond_0
    new-instance v3, Landroid/os/Bundle;

    invoke-direct {v3}, Landroid/os/Bundle;-><init>()V

    .line 255
    .local v3, "bundle":Landroid/os/Bundle;
    iget-object v5, v0, Landroidx/navigation/NavDeepLink;->mArguments:Ljava/util/ArrayList;

    invoke-virtual {v5}, Ljava/util/ArrayList;->size()I

    move-result v5

    .line 256
    .local v5, "size":I
    const/4 v6, 0x0

    .local v6, "index":I
    :goto_0
    if-ge v6, v5, :cond_2

    .line 257
    iget-object v7, v0, Landroidx/navigation/NavDeepLink;->mArguments:Ljava/util/ArrayList;

    invoke-virtual {v7, v6}, Ljava/util/ArrayList;->get(I)Ljava/lang/Object;

    move-result-object v7

    check-cast v7, Ljava/lang/String;

    .line 258
    .local v7, "argumentName":Ljava/lang/String;
    add-int/lit8 v8, v6, 0x1

    invoke-virtual {v2, v8}, Ljava/util/regex/Matcher;->group(I)Ljava/lang/String;

    move-result-object v8

    invoke-static {v8}, Landroid/net/Uri;->decode(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v8

    .line 259
    .local v8, "value":Ljava/lang/String;
    invoke-interface {v1, v7}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v9

    check-cast v9, Landroidx/navigation/NavArgument;

    .line 260
    .local v9, "argument":Landroidx/navigation/NavArgument;
    invoke-direct {v0, v3, v7, v8, v9}, Landroidx/navigation/NavDeepLink;->parseArgument(Landroid/os/Bundle;Ljava/lang/String;Ljava/lang/String;Landroidx/navigation/NavArgument;)Z

    move-result v10

    if-eqz v10, :cond_1

    .line 261
    return-object v4

    .line 256
    .end local v7    # "argumentName":Ljava/lang/String;
    .end local v8    # "value":Ljava/lang/String;
    .end local v9    # "argument":Landroidx/navigation/NavArgument;
    :cond_1
    add-int/lit8 v6, v6, 0x1

    goto :goto_0

    .line 264
    .end local v6    # "index":I
    :cond_2
    iget-boolean v6, v0, Landroidx/navigation/NavDeepLink;->mIsParameterizedQuery:Z

    if-eqz v6, :cond_a

    .line 265
    iget-object v6, v0, Landroidx/navigation/NavDeepLink;->mParamArgMap:Ljava/util/Map;

    invoke-interface {v6}, Ljava/util/Map;->keySet()Ljava/util/Set;

    move-result-object v6

    invoke-interface {v6}, Ljava/util/Set;->iterator()Ljava/util/Iterator;

    move-result-object v6

    :goto_1
    invoke-interface {v6}, Ljava/util/Iterator;->hasNext()Z

    move-result v7

    if-eqz v7, :cond_9

    invoke-interface {v6}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v7

    check-cast v7, Ljava/lang/String;

    .line 266
    .local v7, "paramName":Ljava/lang/String;
    const/4 v8, 0x0

    .line 267
    .local v8, "argMatcher":Ljava/util/regex/Matcher;
    iget-object v9, v0, Landroidx/navigation/NavDeepLink;->mParamArgMap:Ljava/util/Map;

    invoke-interface {v9, v7}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v9

    check-cast v9, Landroidx/navigation/NavDeepLink$ParamQuery;

    .line 268
    .local v9, "storedParam":Landroidx/navigation/NavDeepLink$ParamQuery;
    move-object/from16 v10, p1

    invoke-virtual {v10, v7}, Landroid/net/Uri;->getQueryParameter(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v11

    .line 269
    .local v11, "inputParams":Ljava/lang/String;
    if-eqz v11, :cond_3

    .line 271
    invoke-virtual {v9}, Landroidx/navigation/NavDeepLink$ParamQuery;->getParamRegex()Ljava/lang/String;

    move-result-object v12

    invoke-static {v12}, Ljava/util/regex/Pattern;->compile(Ljava/lang/String;)Ljava/util/regex/Pattern;

    move-result-object v12

    invoke-virtual {v12, v11}, Ljava/util/regex/Pattern;->matcher(Ljava/lang/CharSequence;)Ljava/util/regex/Matcher;

    move-result-object v8

    .line 272
    invoke-virtual {v8}, Ljava/util/regex/Matcher;->matches()Z

    move-result v12

    if-nez v12, :cond_3

    .line 273
    return-object v4

    .line 277
    :cond_3
    const/4 v12, 0x0

    .local v12, "index":I
    :goto_2
    invoke-virtual {v9}, Landroidx/navigation/NavDeepLink$ParamQuery;->size()I

    move-result v13

    if-ge v12, v13, :cond_8

    .line 278
    const/4 v13, 0x0

    .line 279
    .local v13, "value":Ljava/lang/String;
    if-eqz v8, :cond_4

    .line 280
    add-int/lit8 v14, v12, 0x1

    invoke-virtual {v8, v14}, Ljava/util/regex/Matcher;->group(I)Ljava/lang/String;

    move-result-object v14

    invoke-static {v14}, Landroid/net/Uri;->decode(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v13

    .line 282
    :cond_4
    invoke-virtual {v9, v12}, Landroidx/navigation/NavDeepLink$ParamQuery;->getArgumentName(I)Ljava/lang/String;

    move-result-object v14

    .line 283
    .local v14, "argName":Ljava/lang/String;
    invoke-interface {v1, v14}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v15

    check-cast v15, Landroidx/navigation/NavArgument;

    .line 284
    .local v15, "argument":Landroidx/navigation/NavArgument;
    if-eqz v13, :cond_7

    .line 285
    const-string v4, "[{}]"

    const-string v1, ""

    invoke-virtual {v13, v4, v1}, Ljava/lang/String;->replaceAll(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v1, v14}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v1

    if-nez v1, :cond_6

    .line 286
    invoke-direct {v0, v3, v14, v13, v15}, Landroidx/navigation/NavDeepLink;->parseArgument(Landroid/os/Bundle;Ljava/lang/String;Ljava/lang/String;Landroidx/navigation/NavArgument;)Z

    move-result v1

    if-eqz v1, :cond_5

    .line 287
    const/4 v1, 0x0

    return-object v1

    .line 286
    :cond_5
    const/4 v1, 0x0

    goto :goto_3

    .line 285
    :cond_6
    const/4 v1, 0x0

    goto :goto_3

    .line 284
    :cond_7
    move-object v1, v4

    .line 277
    .end local v13    # "value":Ljava/lang/String;
    .end local v14    # "argName":Ljava/lang/String;
    .end local v15    # "argument":Landroidx/navigation/NavArgument;
    :goto_3
    add-int/lit8 v12, v12, 0x1

    move-object v4, v1

    move-object/from16 v1, p2

    goto :goto_2

    :cond_8
    move-object v1, v4

    .line 290
    .end local v7    # "paramName":Ljava/lang/String;
    .end local v8    # "argMatcher":Ljava/util/regex/Matcher;
    .end local v9    # "storedParam":Landroidx/navigation/NavDeepLink$ParamQuery;
    .end local v11    # "inputParams":Ljava/lang/String;
    .end local v12    # "index":I
    move-object/from16 v1, p2

    goto :goto_1

    .line 265
    :cond_9
    move-object/from16 v10, p1

    goto :goto_4

    .line 264
    :cond_a
    move-object/from16 v10, p1

    .line 292
    :goto_4
    return-object v3
.end method

.method public getMimeType()Ljava/lang/String;
    .locals 1

    .line 236
    iget-object v0, p0, Landroidx/navigation/NavDeepLink;->mMimeType:Ljava/lang/String;

    return-object v0
.end method

.method getMimeTypeMatchRating(Ljava/lang/String;)I
    .locals 2
    .param p1, "mimeType"    # Ljava/lang/String;

    .line 240
    iget-object v0, p0, Landroidx/navigation/NavDeepLink;->mMimeType:Ljava/lang/String;

    if-eqz v0, :cond_1

    iget-object v0, p0, Landroidx/navigation/NavDeepLink;->mMimeTypePattern:Ljava/util/regex/Pattern;

    invoke-virtual {v0, p1}, Ljava/util/regex/Pattern;->matcher(Ljava/lang/CharSequence;)Ljava/util/regex/Matcher;

    move-result-object v0

    invoke-virtual {v0}, Ljava/util/regex/Matcher;->matches()Z

    move-result v0

    if-nez v0, :cond_0

    goto :goto_0

    .line 244
    :cond_0
    new-instance v0, Landroidx/navigation/NavDeepLink$MimeType;

    iget-object v1, p0, Landroidx/navigation/NavDeepLink;->mMimeType:Ljava/lang/String;

    invoke-direct {v0, v1}, Landroidx/navigation/NavDeepLink$MimeType;-><init>(Ljava/lang/String;)V

    new-instance v1, Landroidx/navigation/NavDeepLink$MimeType;

    invoke-direct {v1, p1}, Landroidx/navigation/NavDeepLink$MimeType;-><init>(Ljava/lang/String;)V

    invoke-virtual {v0, v1}, Landroidx/navigation/NavDeepLink$MimeType;->compareTo(Landroidx/navigation/NavDeepLink$MimeType;)I

    move-result v0

    return v0

    .line 241
    :cond_1
    :goto_0
    const/4 v0, -0x1

    return v0
.end method

.method public getUriPattern()Ljava/lang/String;
    .locals 1

    .line 214
    iget-object v0, p0, Landroidx/navigation/NavDeepLink;->mUri:Ljava/lang/String;

    return-object v0
.end method

.method isExactDeepLink()Z
    .locals 1

    .line 203
    iget-boolean v0, p0, Landroidx/navigation/NavDeepLink;->mExactDeepLink:Z

    return v0
.end method

.method matches(Landroid/net/Uri;)Z
    .locals 2
    .param p1, "uri"    # Landroid/net/Uri;

    .line 158
    new-instance v0, Landroidx/navigation/NavDeepLinkRequest;

    const/4 v1, 0x0

    invoke-direct {v0, p1, v1, v1}, Landroidx/navigation/NavDeepLinkRequest;-><init>(Landroid/net/Uri;Ljava/lang/String;Ljava/lang/String;)V

    invoke-virtual {p0, v0}, Landroidx/navigation/NavDeepLink;->matches(Landroidx/navigation/NavDeepLinkRequest;)Z

    move-result v0

    return v0
.end method

.method matches(Landroidx/navigation/NavDeepLinkRequest;)Z
    .locals 2
    .param p1, "deepLinkRequest"    # Landroidx/navigation/NavDeepLinkRequest;

    .line 162
    invoke-virtual {p1}, Landroidx/navigation/NavDeepLinkRequest;->getUri()Landroid/net/Uri;

    move-result-object v0

    invoke-direct {p0, v0}, Landroidx/navigation/NavDeepLink;->matchUri(Landroid/net/Uri;)Z

    move-result v0

    const/4 v1, 0x0

    if-nez v0, :cond_0

    .line 163
    return v1

    .line 166
    :cond_0
    invoke-virtual {p1}, Landroidx/navigation/NavDeepLinkRequest;->getAction()Ljava/lang/String;

    move-result-object v0

    invoke-direct {p0, v0}, Landroidx/navigation/NavDeepLink;->matchAction(Ljava/lang/String;)Z

    move-result v0

    if-nez v0, :cond_1

    .line 167
    return v1

    .line 170
    :cond_1
    invoke-virtual {p1}, Landroidx/navigation/NavDeepLinkRequest;->getMimeType()Ljava/lang/String;

    move-result-object v0

    invoke-direct {p0, v0}, Landroidx/navigation/NavDeepLink;->matchMimeType(Ljava/lang/String;)Z

    move-result v0

    return v0
.end method
