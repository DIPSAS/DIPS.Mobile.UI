.class Landroidx/core/provider/FontProvider;
.super Ljava/lang/Object;
.source "FontProvider.java"


# static fields
.field private static final sByteArrayComparator:Ljava/util/Comparator;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/Comparator<",
            "[B>;"
        }
    .end annotation
.end field


# direct methods
.method static constructor <clinit>()V
    .locals 1

    .line 196
    new-instance v0, Landroidx/core/provider/FontProvider$1;

    invoke-direct {v0}, Landroidx/core/provider/FontProvider$1;-><init>()V

    sput-object v0, Landroidx/core/provider/FontProvider;->sByteArrayComparator:Ljava/util/Comparator;

    return-void
.end method

.method private constructor <init>()V
    .locals 0

    .line 47
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method

.method private static convertToByteArrayList([Landroid/content/pm/Signature;)Ljava/util/List;
    .locals 3
    .param p0, "signatures"    # [Landroid/content/pm/Signature;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "([",
            "Landroid/content/pm/Signature;",
            ")",
            "Ljava/util/List<",
            "[B>;"
        }
    .end annotation

    .line 225
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    .line 226
    .local v0, "shaList":Ljava/util/List;, "Ljava/util/List<[B>;"
    const/4 v1, 0x0

    .local v1, "i":I
    :goto_0
    array-length v2, p0

    if-ge v1, v2, :cond_0

    .line 227
    aget-object v2, p0, v1

    invoke-virtual {v2}, Landroid/content/pm/Signature;->toByteArray()[B

    move-result-object v2

    invoke-interface {v0, v2}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    .line 226
    add-int/lit8 v1, v1, 0x1

    goto :goto_0

    .line 229
    .end local v1    # "i":I
    :cond_0
    return-object v0
.end method

.method private static equalsByteArrayList(Ljava/util/List;Ljava/util/List;)Z
    .locals 4
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/List<",
            "[B>;",
            "Ljava/util/List<",
            "[B>;)Z"
        }
    .end annotation

    .line 213
    .local p0, "signatures":Ljava/util/List;, "Ljava/util/List<[B>;"
    .local p1, "requestSignatures":Ljava/util/List;, "Ljava/util/List<[B>;"
    invoke-interface {p0}, Ljava/util/List;->size()I

    move-result v0

    invoke-interface {p1}, Ljava/util/List;->size()I

    move-result v1

    const/4 v2, 0x0

    if-eq v0, v1, :cond_0

    .line 214
    return v2

    .line 216
    :cond_0
    const/4 v0, 0x0

    .local v0, "i":I
    :goto_0
    invoke-interface {p0}, Ljava/util/List;->size()I

    move-result v1

    if-ge v0, v1, :cond_2

    .line 217
    invoke-interface {p0, v0}, Ljava/util/List;->get(I)Ljava/lang/Object;

    move-result-object v1

    check-cast v1, [B

    invoke-interface {p1, v0}, Ljava/util/List;->get(I)Ljava/lang/Object;

    move-result-object v3

    check-cast v3, [B

    invoke-static {v1, v3}, Ljava/util/Arrays;->equals([B[B)Z

    move-result v1

    if-nez v1, :cond_1

    .line 218
    return v2

    .line 216
    :cond_1
    add-int/lit8 v0, v0, 0x1

    goto :goto_0

    .line 221
    .end local v0    # "i":I
    :cond_2
    const/4 v0, 0x1

    return v0
.end method

.method private static getCertificates(Landroidx/core/provider/FontRequest;Landroid/content/res/Resources;)Ljava/util/List;
    .locals 2
    .param p0, "request"    # Landroidx/core/provider/FontRequest;
    .param p1, "resources"    # Landroid/content/res/Resources;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Landroidx/core/provider/FontRequest;",
            "Landroid/content/res/Resources;",
            ")",
            "Ljava/util/List<",
            "Ljava/util/List<",
            "[B>;>;"
        }
    .end annotation

    .line 189
    invoke-virtual {p0}, Landroidx/core/provider/FontRequest;->getCertificates()Ljava/util/List;

    move-result-object v0

    if-eqz v0, :cond_0

    .line 190
    invoke-virtual {p0}, Landroidx/core/provider/FontRequest;->getCertificates()Ljava/util/List;

    move-result-object v0

    return-object v0

    .line 192
    :cond_0
    invoke-virtual {p0}, Landroidx/core/provider/FontRequest;->getCertificatesArrayResId()I

    move-result v0

    .line 193
    .local v0, "resourceId":I
    invoke-static {p1, v0}, Landroidx/core/content/res/FontResourcesParserCompat;->readCerts(Landroid/content/res/Resources;I)Ljava/util/List;

    move-result-object v1

    return-object v1
.end method

.method static getFontFamilyResult(Landroid/content/Context;Landroidx/core/provider/FontRequest;Landroid/os/CancellationSignal;)Landroidx/core/provider/FontsContractCompat$FontFamilyResult;
    .locals 3
    .param p0, "context"    # Landroid/content/Context;
    .param p1, "request"    # Landroidx/core/provider/FontRequest;
    .param p2, "cancellationSignal"    # Landroid/os/CancellationSignal;
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Landroid/content/pm/PackageManager$NameNotFoundException;
        }
    .end annotation

    .line 53
    nop

    .line 54
    invoke-virtual {p0}, Landroid/content/Context;->getPackageManager()Landroid/content/pm/PackageManager;

    move-result-object v0

    invoke-virtual {p0}, Landroid/content/Context;->getResources()Landroid/content/res/Resources;

    move-result-object v1

    .line 53
    invoke-static {v0, p1, v1}, Landroidx/core/provider/FontProvider;->getProvider(Landroid/content/pm/PackageManager;Landroidx/core/provider/FontRequest;Landroid/content/res/Resources;)Landroid/content/pm/ProviderInfo;

    move-result-object v0

    .line 55
    .local v0, "providerInfo":Landroid/content/pm/ProviderInfo;
    if-nez v0, :cond_0

    .line 56
    const/4 v1, 0x1

    const/4 v2, 0x0

    invoke-static {v1, v2}, Landroidx/core/provider/FontsContractCompat$FontFamilyResult;->create(I[Landroidx/core/provider/FontsContractCompat$FontInfo;)Landroidx/core/provider/FontsContractCompat$FontFamilyResult;

    move-result-object v1

    return-object v1

    .line 59
    :cond_0
    iget-object v1, v0, Landroid/content/pm/ProviderInfo;->authority:Ljava/lang/String;

    invoke-static {p0, p1, v1, p2}, Landroidx/core/provider/FontProvider;->query(Landroid/content/Context;Landroidx/core/provider/FontRequest;Ljava/lang/String;Landroid/os/CancellationSignal;)[Landroidx/core/provider/FontsContractCompat$FontInfo;

    move-result-object v1

    .line 61
    .local v1, "fonts":[Landroidx/core/provider/FontsContractCompat$FontInfo;
    const/4 v2, 0x0

    invoke-static {v2, v1}, Landroidx/core/provider/FontsContractCompat$FontFamilyResult;->create(I[Landroidx/core/provider/FontsContractCompat$FontInfo;)Landroidx/core/provider/FontsContractCompat$FontFamilyResult;

    move-result-object v2

    return-object v2
.end method

.method static getProvider(Landroid/content/pm/PackageManager;Landroidx/core/provider/FontRequest;Landroid/content/res/Resources;)Landroid/content/pm/ProviderInfo;
    .locals 8
    .param p0, "packageManager"    # Landroid/content/pm/PackageManager;
    .param p1, "request"    # Landroidx/core/provider/FontRequest;
    .param p2, "resources"    # Landroid/content/res/Resources;
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Landroid/content/pm/PackageManager$NameNotFoundException;
        }
    .end annotation

    .line 76
    invoke-virtual {p1}, Landroidx/core/provider/FontRequest;->getProviderAuthority()Ljava/lang/String;

    move-result-object v0

    .line 77
    .local v0, "providerAuthority":Ljava/lang/String;
    const/4 v1, 0x0

    invoke-virtual {p0, v0, v1}, Landroid/content/pm/PackageManager;->resolveContentProvider(Ljava/lang/String;I)Landroid/content/pm/ProviderInfo;

    move-result-object v1

    .line 78
    .local v1, "info":Landroid/content/pm/ProviderInfo;
    if-eqz v1, :cond_3

    .line 83
    iget-object v2, v1, Landroid/content/pm/ProviderInfo;->packageName:Ljava/lang/String;

    invoke-virtual {p1}, Landroidx/core/provider/FontRequest;->getProviderPackage()Ljava/lang/String;

    move-result-object v3

    invoke-virtual {v2, v3}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v2

    if-eqz v2, :cond_2

    .line 92
    iget-object v2, v1, Landroid/content/pm/ProviderInfo;->packageName:Ljava/lang/String;

    const/16 v3, 0x40

    invoke-virtual {p0, v2, v3}, Landroid/content/pm/PackageManager;->getPackageInfo(Ljava/lang/String;I)Landroid/content/pm/PackageInfo;

    move-result-object v2

    .line 94
    .local v2, "packageInfo":Landroid/content/pm/PackageInfo;
    iget-object v3, v2, Landroid/content/pm/PackageInfo;->signatures:[Landroid/content/pm/Signature;

    invoke-static {v3}, Landroidx/core/provider/FontProvider;->convertToByteArrayList([Landroid/content/pm/Signature;)Ljava/util/List;

    move-result-object v3

    .line 95
    .local v3, "signatures":Ljava/util/List;, "Ljava/util/List<[B>;"
    sget-object v4, Landroidx/core/provider/FontProvider;->sByteArrayComparator:Ljava/util/Comparator;

    invoke-static {v3, v4}, Ljava/util/Collections;->sort(Ljava/util/List;Ljava/util/Comparator;)V

    .line 96
    invoke-static {p1, p2}, Landroidx/core/provider/FontProvider;->getCertificates(Landroidx/core/provider/FontRequest;Landroid/content/res/Resources;)Ljava/util/List;

    move-result-object v4

    .line 97
    .local v4, "requestCertificatesList":Ljava/util/List;, "Ljava/util/List<Ljava/util/List<[B>;>;"
    const/4 v5, 0x0

    .local v5, "i":I
    :goto_0
    invoke-interface {v4}, Ljava/util/List;->size()I

    move-result v6

    if-ge v5, v6, :cond_1

    .line 99
    new-instance v6, Ljava/util/ArrayList;

    invoke-interface {v4, v5}, Ljava/util/List;->get(I)Ljava/lang/Object;

    move-result-object v7

    check-cast v7, Ljava/util/Collection;

    invoke-direct {v6, v7}, Ljava/util/ArrayList;-><init>(Ljava/util/Collection;)V

    .line 100
    .local v6, "requestSignatures":Ljava/util/List;, "Ljava/util/List<[B>;"
    sget-object v7, Landroidx/core/provider/FontProvider;->sByteArrayComparator:Ljava/util/Comparator;

    invoke-static {v6, v7}, Ljava/util/Collections;->sort(Ljava/util/List;Ljava/util/Comparator;)V

    .line 101
    invoke-static {v3, v6}, Landroidx/core/provider/FontProvider;->equalsByteArrayList(Ljava/util/List;Ljava/util/List;)Z

    move-result v7

    if-eqz v7, :cond_0

    .line 102
    return-object v1

    .line 97
    .end local v6    # "requestSignatures":Ljava/util/List;, "Ljava/util/List<[B>;"
    :cond_0
    add-int/lit8 v5, v5, 0x1

    goto :goto_0

    .line 105
    .end local v5    # "i":I
    :cond_1
    const/4 v5, 0x0

    return-object v5

    .line 84
    .end local v2    # "packageInfo":Landroid/content/pm/PackageInfo;
    .end local v3    # "signatures":Ljava/util/List;, "Ljava/util/List<[B>;"
    .end local v4    # "requestCertificatesList":Ljava/util/List;, "Ljava/util/List<Ljava/util/List<[B>;>;"
    :cond_2
    new-instance v2, Landroid/content/pm/PackageManager$NameNotFoundException;

    new-instance v3, Ljava/lang/StringBuilder;

    invoke-direct {v3}, Ljava/lang/StringBuilder;-><init>()V

    const-string v4, "Found content provider "

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v3

    invoke-virtual {v3, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v3

    const-string v4, ", but package was not "

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v3

    .line 86
    invoke-virtual {p1}, Landroidx/core/provider/FontRequest;->getProviderPackage()Ljava/lang/String;

    move-result-object v4

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v3

    invoke-virtual {v3}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v3

    invoke-direct {v2, v3}, Landroid/content/pm/PackageManager$NameNotFoundException;-><init>(Ljava/lang/String;)V

    throw v2

    .line 79
    :cond_3
    new-instance v2, Landroid/content/pm/PackageManager$NameNotFoundException;

    new-instance v3, Ljava/lang/StringBuilder;

    invoke-direct {v3}, Ljava/lang/StringBuilder;-><init>()V

    const-string v4, "No package found for authority: "

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v3

    invoke-virtual {v3, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v3

    invoke-virtual {v3}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v3

    invoke-direct {v2, v3}, Landroid/content/pm/PackageManager$NameNotFoundException;-><init>(Ljava/lang/String;)V

    throw v2
.end method

.method static query(Landroid/content/Context;Landroidx/core/provider/FontRequest;Ljava/lang/String;Landroid/os/CancellationSignal;)[Landroidx/core/provider/FontsContractCompat$FontInfo;
    .locals 22
    .param p0, "context"    # Landroid/content/Context;
    .param p1, "request"    # Landroidx/core/provider/FontRequest;
    .param p2, "authority"    # Ljava/lang/String;
    .param p3, "cancellationSignal"    # Landroid/os/CancellationSignal;

    .line 120
    move-object/from16 v1, p2

    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    move-object v2, v0

    .line 121
    .local v2, "result":Ljava/util/ArrayList;, "Ljava/util/ArrayList<Landroidx/core/provider/FontsContractCompat$FontInfo;>;"
    new-instance v0, Landroid/net/Uri$Builder;

    invoke-direct {v0}, Landroid/net/Uri$Builder;-><init>()V

    const-string v3, "content"

    invoke-virtual {v0, v3}, Landroid/net/Uri$Builder;->scheme(Ljava/lang/String;)Landroid/net/Uri$Builder;

    move-result-object v0

    .line 122
    invoke-virtual {v0, v1}, Landroid/net/Uri$Builder;->authority(Ljava/lang/String;)Landroid/net/Uri$Builder;

    move-result-object v0

    .line 123
    invoke-virtual {v0}, Landroid/net/Uri$Builder;->build()Landroid/net/Uri;

    move-result-object v13

    .line 124
    .local v13, "uri":Landroid/net/Uri;
    new-instance v0, Landroid/net/Uri$Builder;

    invoke-direct {v0}, Landroid/net/Uri$Builder;-><init>()V

    invoke-virtual {v0, v3}, Landroid/net/Uri$Builder;->scheme(Ljava/lang/String;)Landroid/net/Uri$Builder;

    move-result-object v0

    .line 125
    invoke-virtual {v0, v1}, Landroid/net/Uri$Builder;->authority(Ljava/lang/String;)Landroid/net/Uri$Builder;

    move-result-object v0

    .line 126
    const-string v3, "file"

    invoke-virtual {v0, v3}, Landroid/net/Uri$Builder;->appendPath(Ljava/lang/String;)Landroid/net/Uri$Builder;

    move-result-object v0

    .line 127
    invoke-virtual {v0}, Landroid/net/Uri$Builder;->build()Landroid/net/Uri;

    move-result-object v3

    .line 128
    .local v3, "fileBaseUri":Landroid/net/Uri;
    const/4 v14, 0x0

    .line 130
    .local v14, "cursor":Landroid/database/Cursor;
    :try_start_0
    const-string v4, "_id"

    const-string v5, "file_id"

    const-string v6, "font_ttc_index"

    const-string v7, "font_variation_settings"

    const-string v8, "font_weight"

    const-string v9, "font_italic"

    const-string v10, "result_code"

    filled-new-array/range {v4 .. v10}, [Ljava/lang/String;

    move-result-object v6

    .line 137
    .local v6, "projection":[Ljava/lang/String;
    sget v0, Landroid/os/Build$VERSION;->SDK_INT:I

    const/16 v4, 0x10

    const/4 v15, 0x1

    const/4 v12, 0x0

    if-le v0, v4, :cond_0

    .line 138
    invoke-virtual/range {p0 .. p0}, Landroid/content/Context;->getContentResolver()Landroid/content/ContentResolver;

    move-result-object v4

    const-string v7, "query = ?"

    new-array v8, v15, [Ljava/lang/String;

    .line 139
    invoke-virtual/range {p1 .. p1}, Landroidx/core/provider/FontRequest;->getQuery()Ljava/lang/String;

    move-result-object v0

    aput-object v0, v8, v12

    const/4 v9, 0x0

    .line 138
    move-object v5, v13

    move-object/from16 v10, p3

    invoke-virtual/range {v4 .. v10}, Landroid/content/ContentResolver;->query(Landroid/net/Uri;[Ljava/lang/String;Ljava/lang/String;[Ljava/lang/String;Ljava/lang/String;Landroid/os/CancellationSignal;)Landroid/database/Cursor;

    move-result-object v0

    move-object v14, v0

    const/4 v4, 0x0

    .end local v14    # "cursor":Landroid/database/Cursor;
    .local v0, "cursor":Landroid/database/Cursor;
    goto :goto_0

    .line 142
    .end local v0    # "cursor":Landroid/database/Cursor;
    .restart local v14    # "cursor":Landroid/database/Cursor;
    :cond_0
    invoke-virtual/range {p0 .. p0}, Landroid/content/Context;->getContentResolver()Landroid/content/ContentResolver;

    move-result-object v7

    const-string v10, "query = ?"

    new-array v11, v15, [Ljava/lang/String;

    .line 143
    invoke-virtual/range {p1 .. p1}, Landroidx/core/provider/FontRequest;->getQuery()Ljava/lang/String;

    move-result-object v0

    aput-object v0, v11, v12

    const/4 v0, 0x0

    .line 142
    move-object v8, v13

    move-object v9, v6

    const/4 v4, 0x0

    move-object v12, v0

    invoke-virtual/range {v7 .. v12}, Landroid/content/ContentResolver;->query(Landroid/net/Uri;[Ljava/lang/String;Ljava/lang/String;[Ljava/lang/String;Ljava/lang/String;)Landroid/database/Cursor;

    move-result-object v0

    move-object v14, v0

    .line 146
    :goto_0
    if-eqz v14, :cond_7

    invoke-interface {v14}, Landroid/database/Cursor;->getCount()I

    move-result v0

    if-lez v0, :cond_7

    .line 147
    const-string v0, "result_code"

    invoke-interface {v14, v0}, Landroid/database/Cursor;->getColumnIndex(Ljava/lang/String;)I

    move-result v0

    .line 149
    .local v0, "resultCodeColumnIndex":I
    new-instance v5, Ljava/util/ArrayList;

    invoke-direct {v5}, Ljava/util/ArrayList;-><init>()V

    move-object v2, v5

    .line 150
    const-string v5, "_id"

    invoke-interface {v14, v5}, Landroid/database/Cursor;->getColumnIndex(Ljava/lang/String;)I

    move-result v5

    .line 151
    .local v5, "idColumnIndex":I
    const-string v7, "file_id"

    invoke-interface {v14, v7}, Landroid/database/Cursor;->getColumnIndex(Ljava/lang/String;)I

    move-result v7

    .line 153
    .local v7, "fileIdColumnIndex":I
    const-string v8, "font_ttc_index"

    invoke-interface {v14, v8}, Landroid/database/Cursor;->getColumnIndex(Ljava/lang/String;)I

    move-result v8

    .line 155
    .local v8, "ttcIndexColumnIndex":I
    const-string v9, "font_weight"

    invoke-interface {v14, v9}, Landroid/database/Cursor;->getColumnIndex(Ljava/lang/String;)I

    move-result v9

    .line 157
    .local v9, "weightColumnIndex":I
    const-string v10, "font_italic"

    invoke-interface {v14, v10}, Landroid/database/Cursor;->getColumnIndex(Ljava/lang/String;)I

    move-result v10

    .line 159
    .local v10, "italicColumnIndex":I
    :goto_1
    invoke-interface {v14}, Landroid/database/Cursor;->moveToNext()Z

    move-result v11

    if-eqz v11, :cond_6

    .line 160
    const/4 v11, -0x1

    if-eq v0, v11, :cond_1

    .line 161
    invoke-interface {v14, v0}, Landroid/database/Cursor;->getInt(I)I

    move-result v12

    goto :goto_2

    .line 162
    :cond_1
    const/4 v12, 0x0

    :goto_2
    nop

    .line 163
    .local v12, "resultCode":I
    if-eq v8, v11, :cond_2

    .line 164
    invoke-interface {v14, v8}, Landroid/database/Cursor;->getInt(I)I

    move-result v16

    goto :goto_3

    :cond_2
    const/16 v16, 0x0

    :goto_3
    move/from16 v17, v16

    .line 166
    .local v17, "ttcIndex":I
    if-ne v7, v11, :cond_3

    .line 167
    invoke-interface {v14, v5}, Landroid/database/Cursor;->getLong(I)J

    move-result-wide v18

    move-wide/from16 v20, v18

    .line 168
    .local v20, "id":J
    move/from16 v18, v5

    move-wide/from16 v4, v20

    .end local v5    # "idColumnIndex":I
    .end local v20    # "id":J
    .local v4, "id":J
    .local v18, "idColumnIndex":I
    invoke-static {v13, v4, v5}, Landroid/content/ContentUris;->withAppendedId(Landroid/net/Uri;J)Landroid/net/Uri;

    move-result-object v19

    move-object/from16 v4, v19

    .line 169
    .local v4, "fileUri":Landroid/net/Uri;
    goto :goto_4

    .line 170
    .end local v4    # "fileUri":Landroid/net/Uri;
    .end local v18    # "idColumnIndex":I
    .restart local v5    # "idColumnIndex":I
    :cond_3
    move/from16 v18, v5

    .end local v5    # "idColumnIndex":I
    .restart local v18    # "idColumnIndex":I
    invoke-interface {v14, v7}, Landroid/database/Cursor;->getLong(I)J

    move-result-wide v4

    .line 171
    .local v4, "id":J
    invoke-static {v3, v4, v5}, Landroid/content/ContentUris;->withAppendedId(Landroid/net/Uri;J)Landroid/net/Uri;

    move-result-object v19

    move-object/from16 v4, v19

    .line 174
    .local v4, "fileUri":Landroid/net/Uri;
    :goto_4
    if-eq v9, v11, :cond_4

    invoke-interface {v14, v9}, Landroid/database/Cursor;->getInt(I)I

    move-result v5

    goto :goto_5

    :cond_4
    const/16 v5, 0x190

    .line 175
    .local v5, "weight":I
    :goto_5
    if-eq v10, v11, :cond_5

    invoke-interface {v14, v10}, Landroid/database/Cursor;->getInt(I)I

    move-result v11

    if-ne v11, v15, :cond_5

    const/4 v11, 0x1

    goto :goto_6

    :cond_5
    const/4 v11, 0x0

    .line 177
    .local v11, "italic":Z
    :goto_6
    move/from16 v15, v17

    move/from16 v17, v0

    .end local v0    # "resultCodeColumnIndex":I
    .local v15, "ttcIndex":I
    .local v17, "resultCodeColumnIndex":I
    invoke-static {v4, v15, v5, v11, v12}, Landroidx/core/provider/FontsContractCompat$FontInfo;->create(Landroid/net/Uri;IIZI)Landroidx/core/provider/FontsContractCompat$FontInfo;

    move-result-object v0

    invoke-virtual {v2, v0}, Ljava/util/ArrayList;->add(Ljava/lang/Object;)Z
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    .line 178
    move/from16 v0, v17

    move/from16 v5, v18

    const/4 v4, 0x0

    const/4 v15, 0x1

    .end local v4    # "fileUri":Landroid/net/Uri;
    .end local v5    # "weight":I
    .end local v11    # "italic":Z
    .end local v12    # "resultCode":I
    .end local v15    # "ttcIndex":I
    goto :goto_1

    .line 159
    .end local v17    # "resultCodeColumnIndex":I
    .end local v18    # "idColumnIndex":I
    .restart local v0    # "resultCodeColumnIndex":I
    .local v5, "idColumnIndex":I
    :cond_6
    move/from16 v17, v0

    move/from16 v18, v5

    .line 181
    .end local v0    # "resultCodeColumnIndex":I
    .end local v5    # "idColumnIndex":I
    .end local v6    # "projection":[Ljava/lang/String;
    .end local v7    # "fileIdColumnIndex":I
    .end local v8    # "ttcIndexColumnIndex":I
    .end local v9    # "weightColumnIndex":I
    .end local v10    # "italicColumnIndex":I
    :cond_7
    if-eqz v14, :cond_8

    .line 182
    invoke-interface {v14}, Landroid/database/Cursor;->close()V

    .line 185
    :cond_8
    const/4 v0, 0x0

    new-array v0, v0, [Landroidx/core/provider/FontsContractCompat$FontInfo;

    invoke-virtual {v2, v0}, Ljava/util/ArrayList;->toArray([Ljava/lang/Object;)[Ljava/lang/Object;

    move-result-object v0

    check-cast v0, [Landroidx/core/provider/FontsContractCompat$FontInfo;

    return-object v0

    .line 181
    :catchall_0
    move-exception v0

    if-eqz v14, :cond_9

    .line 182
    invoke-interface {v14}, Landroid/database/Cursor;->close()V

    .line 184
    :cond_9
    throw v0
.end method
