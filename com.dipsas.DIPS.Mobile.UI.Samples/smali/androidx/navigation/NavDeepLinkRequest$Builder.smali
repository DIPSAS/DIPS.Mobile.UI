.class public final Landroidx/navigation/NavDeepLinkRequest$Builder;
.super Ljava/lang/Object;
.source "NavDeepLinkRequest.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Landroidx/navigation/NavDeepLinkRequest;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x19
    name = "Builder"
.end annotation


# instance fields
.field private mAction:Ljava/lang/String;

.field private mMimeType:Ljava/lang/String;

.field private mUri:Landroid/net/Uri;


# direct methods
.method private constructor <init>()V
    .locals 0

    .line 116
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method

.method public static fromAction(Ljava/lang/String;)Landroidx/navigation/NavDeepLinkRequest$Builder;
    .locals 2
    .param p0, "action"    # Ljava/lang/String;

    .line 141
    invoke-virtual {p0}, Ljava/lang/String;->isEmpty()Z

    move-result v0

    if-nez v0, :cond_0

    .line 145
    new-instance v0, Landroidx/navigation/NavDeepLinkRequest$Builder;

    invoke-direct {v0}, Landroidx/navigation/NavDeepLinkRequest$Builder;-><init>()V

    .line 146
    .local v0, "builder":Landroidx/navigation/NavDeepLinkRequest$Builder;
    invoke-virtual {v0, p0}, Landroidx/navigation/NavDeepLinkRequest$Builder;->setAction(Ljava/lang/String;)Landroidx/navigation/NavDeepLinkRequest$Builder;

    .line 147
    return-object v0

    .line 142
    .end local v0    # "builder":Landroidx/navigation/NavDeepLinkRequest$Builder;
    :cond_0
    new-instance v0, Ljava/lang/IllegalArgumentException;

    const-string v1, "The NavDeepLinkRequest cannot have an empty action."

    invoke-direct {v0, v1}, Ljava/lang/IllegalArgumentException;-><init>(Ljava/lang/String;)V

    throw v0
.end method

.method public static fromMimeType(Ljava/lang/String;)Landroidx/navigation/NavDeepLinkRequest$Builder;
    .locals 1
    .param p0, "mimeType"    # Ljava/lang/String;

    .line 158
    new-instance v0, Landroidx/navigation/NavDeepLinkRequest$Builder;

    invoke-direct {v0}, Landroidx/navigation/NavDeepLinkRequest$Builder;-><init>()V

    .line 159
    .local v0, "builder":Landroidx/navigation/NavDeepLinkRequest$Builder;
    invoke-virtual {v0, p0}, Landroidx/navigation/NavDeepLinkRequest$Builder;->setMimeType(Ljava/lang/String;)Landroidx/navigation/NavDeepLinkRequest$Builder;

    .line 160
    return-object v0
.end method

.method public static fromUri(Landroid/net/Uri;)Landroidx/navigation/NavDeepLinkRequest$Builder;
    .locals 1
    .param p0, "uri"    # Landroid/net/Uri;

    .line 126
    new-instance v0, Landroidx/navigation/NavDeepLinkRequest$Builder;

    invoke-direct {v0}, Landroidx/navigation/NavDeepLinkRequest$Builder;-><init>()V

    .line 127
    .local v0, "builder":Landroidx/navigation/NavDeepLinkRequest$Builder;
    invoke-virtual {v0, p0}, Landroidx/navigation/NavDeepLinkRequest$Builder;->setUri(Landroid/net/Uri;)Landroidx/navigation/NavDeepLinkRequest$Builder;

    .line 128
    return-object v0
.end method


# virtual methods
.method public build()Landroidx/navigation/NavDeepLinkRequest;
    .locals 4

    .line 226
    new-instance v0, Landroidx/navigation/NavDeepLinkRequest;

    iget-object v1, p0, Landroidx/navigation/NavDeepLinkRequest$Builder;->mUri:Landroid/net/Uri;

    iget-object v2, p0, Landroidx/navigation/NavDeepLinkRequest$Builder;->mAction:Ljava/lang/String;

    iget-object v3, p0, Landroidx/navigation/NavDeepLinkRequest$Builder;->mMimeType:Ljava/lang/String;

    invoke-direct {v0, v1, v2, v3}, Landroidx/navigation/NavDeepLinkRequest;-><init>(Landroid/net/Uri;Ljava/lang/String;Ljava/lang/String;)V

    return-object v0
.end method

.method public setAction(Ljava/lang/String;)Landroidx/navigation/NavDeepLinkRequest$Builder;
    .locals 2
    .param p1, "action"    # Ljava/lang/String;

    .line 187
    invoke-virtual {p1}, Ljava/lang/String;->isEmpty()Z

    move-result v0

    if-nez v0, :cond_0

    .line 191
    iput-object p1, p0, Landroidx/navigation/NavDeepLinkRequest$Builder;->mAction:Ljava/lang/String;

    .line 192
    return-object p0

    .line 188
    :cond_0
    new-instance v0, Ljava/lang/IllegalArgumentException;

    const-string v1, "The NavDeepLinkRequest cannot have an empty action."

    invoke-direct {v0, v1}, Ljava/lang/IllegalArgumentException;-><init>(Ljava/lang/String;)V

    throw v0
.end method

.method public setMimeType(Ljava/lang/String;)Landroidx/navigation/NavDeepLinkRequest$Builder;
    .locals 5
    .param p1, "mimeType"    # Ljava/lang/String;

    .line 207
    const-string v0, "^[-\\w*.]+/[-\\w+*.]+$"

    invoke-static {v0}, Ljava/util/regex/Pattern;->compile(Ljava/lang/String;)Ljava/util/regex/Pattern;

    move-result-object v0

    .line 208
    .local v0, "mimeTypePattern":Ljava/util/regex/Pattern;
    invoke-virtual {v0, p1}, Ljava/util/regex/Pattern;->matcher(Ljava/lang/CharSequence;)Ljava/util/regex/Matcher;

    move-result-object v1

    .line 210
    .local v1, "mimeTypeMatcher":Ljava/util/regex/Matcher;
    invoke-virtual {v1}, Ljava/util/regex/Matcher;->matches()Z

    move-result v2

    if-eqz v2, :cond_0

    .line 215
    iput-object p1, p0, Landroidx/navigation/NavDeepLinkRequest$Builder;->mMimeType:Ljava/lang/String;

    .line 216
    return-object p0

    .line 211
    :cond_0
    new-instance v2, Ljava/lang/IllegalArgumentException;

    new-instance v3, Ljava/lang/StringBuilder;

    invoke-direct {v3}, Ljava/lang/StringBuilder;-><init>()V

    const-string v4, "The given mimeType "

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v3

    invoke-virtual {v3, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v3

    const-string v4, " does not match to required \"type/subtype\" format"

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v3

    invoke-virtual {v3}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v3

    invoke-direct {v2, v3}, Ljava/lang/IllegalArgumentException;-><init>(Ljava/lang/String;)V

    throw v2
.end method

.method public setUri(Landroid/net/Uri;)Landroidx/navigation/NavDeepLinkRequest$Builder;
    .locals 0
    .param p1, "uri"    # Landroid/net/Uri;

    .line 172
    iput-object p1, p0, Landroidx/navigation/NavDeepLinkRequest$Builder;->mUri:Landroid/net/Uri;

    .line 173
    return-object p0
.end method
