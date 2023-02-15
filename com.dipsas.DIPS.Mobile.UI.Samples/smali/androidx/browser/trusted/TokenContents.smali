.class final Landroidx/browser/trusted/TokenContents;
.super Ljava/lang/Object;
.source "TokenContents.java"


# instance fields
.field private final mContents:[B

.field private mFingerprints:Ljava/util/List;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/List<",
            "[B>;"
        }
    .end annotation
.end field

.field private mPackageName:Ljava/lang/String;


# direct methods
.method public static synthetic $r8$lambda$Q7kOl2yBde7CmQs5Ktpiz56Nr70([B[B)I
    .locals 0

    invoke-static {p0, p1}, Landroidx/browser/trusted/TokenContents;->compareByteArrays([B[B)I

    move-result p0

    return p0
.end method

.method private constructor <init>([B)V
    .locals 0
    .param p1, "contents"    # [B
    .annotation system Ldalvik/annotation/MethodParameters;
        accessFlags = {
            0x0
        }
        names = {
            "contents"
        }
    .end annotation

    .line 63
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 64
    iput-object p1, p0, Landroidx/browser/trusted/TokenContents;->mContents:[B

    .line 65
    return-void
.end method

.method private constructor <init>([BLjava/lang/String;Ljava/util/List;)V
    .locals 4
    .param p1, "contents"    # [B
    .param p2, "packageName"    # Ljava/lang/String;
    .annotation system Ldalvik/annotation/MethodParameters;
        accessFlags = {
            0x0,
            0x0,
            0x0
        }
        names = {
            "contents",
            "packageName",
            "fingerprints"
        }
    .end annotation

    .annotation system Ldalvik/annotation/Signature;
        value = {
            "([B",
            "Ljava/lang/String;",
            "Ljava/util/List<",
            "[B>;)V"
        }
    .end annotation

    .line 75
    .local p3, "fingerprints":Ljava/util/List;, "Ljava/util/List<[B>;"
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 76
    iput-object p1, p0, Landroidx/browser/trusted/TokenContents;->mContents:[B

    .line 77
    iput-object p2, p0, Landroidx/browser/trusted/TokenContents;->mPackageName:Ljava/lang/String;

    .line 78
    new-instance v0, Ljava/util/ArrayList;

    invoke-interface {p3}, Ljava/util/List;->size()I

    move-result v1

    invoke-direct {v0, v1}, Ljava/util/ArrayList;-><init>(I)V

    iput-object v0, p0, Landroidx/browser/trusted/TokenContents;->mFingerprints:Ljava/util/List;

    .line 81
    invoke-interface {p3}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v0

    :goto_0
    invoke-interface {v0}, Ljava/util/Iterator;->hasNext()Z

    move-result v1

    if-eqz v1, :cond_0

    invoke-interface {v0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, [B

    .line 82
    .local v1, "fingerprint":[B
    iget-object v2, p0, Landroidx/browser/trusted/TokenContents;->mFingerprints:Ljava/util/List;

    array-length v3, v1

    invoke-static {v1, v3}, Ljava/util/Arrays;->copyOf([BI)[B

    move-result-object v3

    invoke-interface {v2, v3}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    .line 83
    .end local v1    # "fingerprint":[B
    goto :goto_0

    .line 84
    :cond_0
    return-void
.end method

.method private static compareByteArrays([B[B)I
    .locals 4
    .param p0, "a"    # [B
    .param p1, "b"    # [B
    .annotation system Ldalvik/annotation/MethodParameters;
        accessFlags = {
            0x0,
            0x0
        }
        names = {
            "a",
            "b"
        }
    .end annotation

    .line 149
    const/4 v0, 0x0

    if-ne p0, p1, :cond_0

    return v0

    .line 152
    :cond_0
    if-nez p0, :cond_1

    const/4 v0, -0x1

    return v0

    .line 153
    :cond_1
    if-nez p1, :cond_2

    const/4 v0, 0x1

    return v0

    .line 156
    :cond_2
    const/4 v1, 0x0

    .local v1, "i":I
    :goto_0
    array-length v2, p0

    array-length v3, p1

    invoke-static {v2, v3}, Ljava/lang/Math;->min(II)I

    move-result v2

    if-ge v1, v2, :cond_4

    .line 157
    aget-byte v2, p0, v1

    aget-byte v3, p1, v1

    if-eq v2, v3, :cond_3

    aget-byte v0, p0, v1

    aget-byte v2, p1, v1

    sub-int/2addr v0, v2

    return v0

    .line 156
    :cond_3
    add-int/lit8 v1, v1, 0x1

    goto :goto_0

    .line 161
    .end local v1    # "i":I
    :cond_4
    array-length v1, p0

    array-length v2, p1

    if-eq v1, v2, :cond_5

    array-length v0, p0

    array-length v1, p1

    sub-int/2addr v0, v1

    return v0

    .line 164
    :cond_5
    return v0
.end method

.method static create(Ljava/lang/String;Ljava/util/List;)Landroidx/browser/trusted/TokenContents;
    .locals 2
    .param p0, "packageName"    # Ljava/lang/String;
    .annotation system Ldalvik/annotation/MethodParameters;
        accessFlags = {
            0x0,
            0x0
        }
        names = {
            "packageName",
            "fingerprints"
        }
    .end annotation

    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/lang/String;",
            "Ljava/util/List<",
            "[B>;)",
            "Landroidx/browser/trusted/TokenContents;"
        }
    .end annotation

    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    .line 70
    .local p1, "fingerprints":Ljava/util/List;, "Ljava/util/List<[B>;"
    new-instance v0, Landroidx/browser/trusted/TokenContents;

    .line 71
    invoke-static {p0, p1}, Landroidx/browser/trusted/TokenContents;->createToken(Ljava/lang/String;Ljava/util/List;)[B

    move-result-object v1

    invoke-direct {v0, v1, p0, p1}, Landroidx/browser/trusted/TokenContents;-><init>([BLjava/lang/String;Ljava/util/List;)V

    .line 70
    return-object v0
.end method

.method private static createToken(Ljava/lang/String;Ljava/util/List;)[B
    .locals 5
    .param p0, "packageName"    # Ljava/lang/String;
    .annotation system Ldalvik/annotation/MethodParameters;
        accessFlags = {
            0x0,
            0x0
        }
        names = {
            "packageName",
            "fingerprints"
        }
    .end annotation

    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/lang/String;",
            "Ljava/util/List<",
            "[B>;)[B"
        }
    .end annotation

    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    .line 129
    .local p1, "fingerprints":Ljava/util/List;, "Ljava/util/List<[B>;"
    new-instance v0, Landroidx/browser/trusted/TokenContents$$ExternalSyntheticLambda0;

    invoke-direct {v0}, Landroidx/browser/trusted/TokenContents$$ExternalSyntheticLambda0;-><init>()V

    invoke-static {p1, v0}, Ljava/util/Collections;->sort(Ljava/util/List;Ljava/util/Comparator;)V

    .line 131
    new-instance v0, Ljava/io/ByteArrayOutputStream;

    invoke-direct {v0}, Ljava/io/ByteArrayOutputStream;-><init>()V

    .line 132
    .local v0, "baos":Ljava/io/ByteArrayOutputStream;
    new-instance v1, Ljava/io/DataOutputStream;

    invoke-direct {v1, v0}, Ljava/io/DataOutputStream;-><init>(Ljava/io/OutputStream;)V

    .line 134
    .local v1, "writer":Ljava/io/DataOutputStream;
    invoke-virtual {v1, p0}, Ljava/io/DataOutputStream;->writeUTF(Ljava/lang/String;)V

    .line 135
    invoke-interface {p1}, Ljava/util/List;->size()I

    move-result v2

    invoke-virtual {v1, v2}, Ljava/io/DataOutputStream;->writeInt(I)V

    .line 136
    invoke-interface {p1}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v2

    :goto_0
    invoke-interface {v2}, Ljava/util/Iterator;->hasNext()Z

    move-result v3

    if-eqz v3, :cond_0

    invoke-interface {v2}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v3

    check-cast v3, [B

    .line 137
    .local v3, "fingerprint":[B
    array-length v4, v3

    invoke-virtual {v1, v4}, Ljava/io/DataOutputStream;->writeInt(I)V

    .line 138
    invoke-virtual {v1, v3}, Ljava/io/DataOutputStream;->write([B)V

    .line 139
    .end local v3    # "fingerprint":[B
    goto :goto_0

    .line 140
    :cond_0
    invoke-virtual {v1}, Ljava/io/DataOutputStream;->flush()V

    .line 142
    invoke-virtual {v0}, Ljava/io/ByteArrayOutputStream;->toByteArray()[B

    move-result-object v2

    return-object v2
.end method

.method static deserialize([B)Landroidx/browser/trusted/TokenContents;
    .locals 1
    .param p0, "serialized"    # [B
    .annotation system Ldalvik/annotation/MethodParameters;
        accessFlags = {
            0x0
        }
        names = {
            "serialized"
        }
    .end annotation

    .line 60
    new-instance v0, Landroidx/browser/trusted/TokenContents;

    invoke-direct {v0, p0}, Landroidx/browser/trusted/TokenContents;-><init>([B)V

    return-object v0
.end method

.method private parseIfNeeded()V
    .locals 8
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    .line 168
    iget-object v0, p0, Landroidx/browser/trusted/TokenContents;->mPackageName:Ljava/lang/String;

    if-eqz v0, :cond_0

    return-void

    .line 170
    :cond_0
    new-instance v0, Ljava/io/DataInputStream;

    new-instance v1, Ljava/io/ByteArrayInputStream;

    iget-object v2, p0, Landroidx/browser/trusted/TokenContents;->mContents:[B

    invoke-direct {v1, v2}, Ljava/io/ByteArrayInputStream;-><init>([B)V

    invoke-direct {v0, v1}, Ljava/io/DataInputStream;-><init>(Ljava/io/InputStream;)V

    .line 171
    .local v0, "reader":Ljava/io/DataInputStream;
    invoke-virtual {v0}, Ljava/io/DataInputStream;->readUTF()Ljava/lang/String;

    move-result-object v1

    iput-object v1, p0, Landroidx/browser/trusted/TokenContents;->mPackageName:Ljava/lang/String;

    .line 173
    invoke-virtual {v0}, Ljava/io/DataInputStream;->readInt()I

    move-result v1

    .line 174
    .local v1, "numFingerprints":I
    new-instance v2, Ljava/util/ArrayList;

    invoke-direct {v2, v1}, Ljava/util/ArrayList;-><init>(I)V

    iput-object v2, p0, Landroidx/browser/trusted/TokenContents;->mFingerprints:Ljava/util/List;

    .line 175
    const/4 v2, 0x0

    .local v2, "i":I
    :goto_0
    if-ge v2, v1, :cond_2

    .line 176
    invoke-virtual {v0}, Ljava/io/DataInputStream;->readInt()I

    move-result v3

    .line 177
    .local v3, "size":I
    new-array v4, v3, [B

    .line 178
    .local v4, "fingerprint":[B
    invoke-virtual {v0, v4}, Ljava/io/DataInputStream;->read([B)I

    move-result v5

    .line 179
    .local v5, "bytesRead":I
    if-ne v5, v3, :cond_1

    .line 180
    iget-object v6, p0, Landroidx/browser/trusted/TokenContents;->mFingerprints:Ljava/util/List;

    invoke-interface {v6, v4}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    .line 175
    .end local v3    # "size":I
    .end local v4    # "fingerprint":[B
    .end local v5    # "bytesRead":I
    add-int/lit8 v2, v2, 0x1

    goto :goto_0

    .line 179
    .restart local v3    # "size":I
    .restart local v4    # "fingerprint":[B
    .restart local v5    # "bytesRead":I
    :cond_1
    new-instance v6, Ljava/lang/IllegalStateException;

    const-string v7, "Could not read fingerprint"

    invoke-direct {v6, v7}, Ljava/lang/IllegalStateException;-><init>(Ljava/lang/String;)V

    throw v6

    .line 182
    .end local v2    # "i":I
    .end local v3    # "size":I
    .end local v4    # "fingerprint":[B
    .end local v5    # "bytesRead":I
    :cond_2
    return-void
.end method


# virtual methods
.method public equals(Ljava/lang/Object;)Z
    .locals 3
    .param p1, "o"    # Ljava/lang/Object;
    .annotation system Ldalvik/annotation/MethodParameters;
        accessFlags = {
            0x0
        }
        names = {
            "o"
        }
    .end annotation

    .line 113
    if-ne p0, p1, :cond_0

    const/4 v0, 0x1

    return v0

    .line 114
    :cond_0
    if-eqz p1, :cond_2

    invoke-virtual {p0}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    move-result-object v0

    invoke-virtual {p1}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    move-result-object v1

    if-eq v0, v1, :cond_1

    goto :goto_0

    .line 115
    :cond_1
    move-object v0, p1

    check-cast v0, Landroidx/browser/trusted/TokenContents;

    .line 116
    .local v0, "that":Landroidx/browser/trusted/TokenContents;
    iget-object v1, p0, Landroidx/browser/trusted/TokenContents;->mContents:[B

    iget-object v2, v0, Landroidx/browser/trusted/TokenContents;->mContents:[B

    invoke-static {v1, v2}, Ljava/util/Arrays;->equals([B[B)Z

    move-result v1

    return v1

    .line 114
    .end local v0    # "that":Landroidx/browser/trusted/TokenContents;
    :cond_2
    :goto_0
    const/4 v0, 0x0

    return v0
.end method

.method public getFingerprint(I)[B
    .locals 2
    .param p1, "i"    # I
    .annotation system Ldalvik/annotation/MethodParameters;
        accessFlags = {
            0x0
        }
        names = {
            "i"
        }
    .end annotation

    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    .line 101
    invoke-direct {p0}, Landroidx/browser/trusted/TokenContents;->parseIfNeeded()V

    .line 102
    iget-object v0, p0, Landroidx/browser/trusted/TokenContents;->mFingerprints:Ljava/util/List;

    if-eqz v0, :cond_0

    .line 103
    invoke-interface {v0, p1}, Ljava/util/List;->get(I)Ljava/lang/Object;

    move-result-object v0

    check-cast v0, [B

    iget-object v1, p0, Landroidx/browser/trusted/TokenContents;->mFingerprints:Ljava/util/List;

    invoke-interface {v1, p1}, Ljava/util/List;->get(I)Ljava/lang/Object;

    move-result-object v1

    check-cast v1, [B

    array-length v1, v1

    invoke-static {v0, v1}, Ljava/util/Arrays;->copyOf([BI)[B

    move-result-object v0

    return-object v0

    .line 102
    :cond_0
    new-instance v0, Ljava/lang/IllegalStateException;

    invoke-direct {v0}, Ljava/lang/IllegalStateException;-><init>()V

    throw v0
.end method

.method public getFingerprintCount()I
    .locals 1
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    .line 94
    invoke-direct {p0}, Landroidx/browser/trusted/TokenContents;->parseIfNeeded()V

    .line 95
    iget-object v0, p0, Landroidx/browser/trusted/TokenContents;->mFingerprints:Ljava/util/List;

    if-eqz v0, :cond_0

    .line 96
    invoke-interface {v0}, Ljava/util/List;->size()I

    move-result v0

    return v0

    .line 95
    :cond_0
    new-instance v0, Ljava/lang/IllegalStateException;

    invoke-direct {v0}, Ljava/lang/IllegalStateException;-><init>()V

    throw v0
.end method

.method public getPackageName()Ljava/lang/String;
    .locals 1
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    .line 88
    invoke-direct {p0}, Landroidx/browser/trusted/TokenContents;->parseIfNeeded()V

    .line 89
    iget-object v0, p0, Landroidx/browser/trusted/TokenContents;->mPackageName:Ljava/lang/String;

    if-eqz v0, :cond_0

    .line 90
    return-object v0

    .line 89
    :cond_0
    new-instance v0, Ljava/lang/IllegalStateException;

    invoke-direct {v0}, Ljava/lang/IllegalStateException;-><init>()V

    throw v0
.end method

.method public hashCode()I
    .locals 1

    .line 121
    iget-object v0, p0, Landroidx/browser/trusted/TokenContents;->mContents:[B

    invoke-static {v0}, Ljava/util/Arrays;->hashCode([B)I

    move-result v0

    return v0
.end method

.method public serialize()[B
    .locals 2

    .line 108
    iget-object v0, p0, Landroidx/browser/trusted/TokenContents;->mContents:[B

    array-length v1, v0

    invoke-static {v0, v1}, Ljava/util/Arrays;->copyOf([BI)[B

    move-result-object v0

    return-object v0
.end method
