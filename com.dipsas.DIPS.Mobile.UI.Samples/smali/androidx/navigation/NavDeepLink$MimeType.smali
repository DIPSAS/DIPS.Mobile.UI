.class Landroidx/navigation/NavDeepLink$MimeType;
.super Ljava/lang/Object;
.source "NavDeepLink.java"

# interfaces
.implements Ljava/lang/Comparable;


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Landroidx/navigation/NavDeepLink;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0xa
    name = "MimeType"
.end annotation

.annotation system Ldalvik/annotation/Signature;
    value = {
        "Ljava/lang/Object;",
        "Ljava/lang/Comparable<",
        "Landroidx/navigation/NavDeepLink$MimeType;",
        ">;"
    }
.end annotation


# instance fields
.field mSubType:Ljava/lang/String;

.field mType:Ljava/lang/String;


# direct methods
.method constructor <init>(Ljava/lang/String;)V
    .locals 2
    .param p1, "mimeType"    # Ljava/lang/String;

    .line 348
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 351
    const-string v0, "/"

    const/4 v1, -0x1

    invoke-virtual {p1, v0, v1}, Ljava/lang/String;->split(Ljava/lang/String;I)[Ljava/lang/String;

    move-result-object v0

    .line 352
    .local v0, "typeAndSubType":[Ljava/lang/String;
    const/4 v1, 0x0

    aget-object v1, v0, v1

    iput-object v1, p0, Landroidx/navigation/NavDeepLink$MimeType;->mType:Ljava/lang/String;

    .line 353
    const/4 v1, 0x1

    aget-object v1, v0, v1

    iput-object v1, p0, Landroidx/navigation/NavDeepLink$MimeType;->mSubType:Ljava/lang/String;

    .line 354
    return-void
.end method


# virtual methods
.method public compareTo(Landroidx/navigation/NavDeepLink$MimeType;)I
    .locals 3
    .param p1, "o"    # Landroidx/navigation/NavDeepLink$MimeType;

    .line 358
    const/4 v0, 0x0

    .line 363
    .local v0, "result":I
    iget-object v1, p0, Landroidx/navigation/NavDeepLink$MimeType;->mType:Ljava/lang/String;

    iget-object v2, p1, Landroidx/navigation/NavDeepLink$MimeType;->mType:Ljava/lang/String;

    invoke-virtual {v1, v2}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v1

    if-eqz v1, :cond_0

    .line 364
    add-int/lit8 v0, v0, 0x2

    .line 367
    :cond_0
    iget-object v1, p0, Landroidx/navigation/NavDeepLink$MimeType;->mSubType:Ljava/lang/String;

    iget-object v2, p1, Landroidx/navigation/NavDeepLink$MimeType;->mSubType:Ljava/lang/String;

    invoke-virtual {v1, v2}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v1

    if-eqz v1, :cond_1

    .line 368
    add-int/lit8 v0, v0, 0x1

    .line 370
    :cond_1
    return v0
.end method

.method public bridge synthetic compareTo(Ljava/lang/Object;)I
    .locals 0

    .line 344
    check-cast p1, Landroidx/navigation/NavDeepLink$MimeType;

    invoke-virtual {p0, p1}, Landroidx/navigation/NavDeepLink$MimeType;->compareTo(Landroidx/navigation/NavDeepLink$MimeType;)I

    move-result p1

    return p1
.end method
