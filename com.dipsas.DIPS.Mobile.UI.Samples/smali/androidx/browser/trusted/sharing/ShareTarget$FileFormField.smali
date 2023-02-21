.class public final Landroidx/browser/trusted/sharing/ShareTarget$FileFormField;
.super Ljava/lang/Object;
.source "ShareTarget.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Landroidx/browser/trusted/sharing/ShareTarget;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x19
    name = "FileFormField"
.end annotation


# static fields
.field public static final KEY_ACCEPTED_TYPES:Ljava/lang/String; = "androidx.browser.trusted.sharing.KEY_ACCEPTED_TYPES"

.field public static final KEY_NAME:Ljava/lang/String; = "androidx.browser.trusted.sharing.KEY_FILE_NAME"


# instance fields
.field public final acceptedTypes:Ljava/util/List;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/List<",
            "Ljava/lang/String;",
            ">;"
        }
    .end annotation
.end field

.field public final name:Ljava/lang/String;


# direct methods
.method public constructor <init>(Ljava/lang/String;Ljava/util/List;)V
    .locals 1
    .param p1, "name"    # Ljava/lang/String;
    .annotation system Ldalvik/annotation/MethodParameters;
        accessFlags = {
            0x0,
            0x0
        }
        names = {
            "name",
            "acceptedTypes"
        }
    .end annotation

    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/lang/String;",
            "Ljava/util/List<",
            "Ljava/lang/String;",
            ">;)V"
        }
    .end annotation

    .line 266
    .local p2, "acceptedTypes":Ljava/util/List;, "Ljava/util/List<Ljava/lang/String;>;"
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 267
    iput-object p1, p0, Landroidx/browser/trusted/sharing/ShareTarget$FileFormField;->name:Ljava/lang/String;

    .line 268
    invoke-static {p2}, Ljava/util/Collections;->unmodifiableList(Ljava/util/List;)Ljava/util/List;

    move-result-object v0

    iput-object v0, p0, Landroidx/browser/trusted/sharing/ShareTarget$FileFormField;->acceptedTypes:Ljava/util/List;

    .line 269
    return-void
.end method

.method static fromBundle(Landroid/os/Bundle;)Landroidx/browser/trusted/sharing/ShareTarget$FileFormField;
    .locals 3
    .param p0, "bundle"    # Landroid/os/Bundle;
    .annotation system Ldalvik/annotation/MethodParameters;
        accessFlags = {
            0x0
        }
        names = {
            "bundle"
        }
    .end annotation

    .line 283
    const/4 v0, 0x0

    if-nez p0, :cond_0

    .line 284
    return-object v0

    .line 286
    :cond_0
    const-string v1, "androidx.browser.trusted.sharing.KEY_FILE_NAME"

    invoke-virtual {p0, v1}, Landroid/os/Bundle;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v1

    .line 287
    .local v1, "name":Ljava/lang/String;
    const-string v2, "androidx.browser.trusted.sharing.KEY_ACCEPTED_TYPES"

    invoke-virtual {p0, v2}, Landroid/os/Bundle;->getStringArrayList(Ljava/lang/String;)Ljava/util/ArrayList;

    move-result-object v2

    .line 288
    .local v2, "acceptedTypes":Ljava/util/ArrayList;, "Ljava/util/ArrayList<Ljava/lang/String;>;"
    if-eqz v1, :cond_2

    if-nez v2, :cond_1

    goto :goto_0

    .line 291
    :cond_1
    new-instance v0, Landroidx/browser/trusted/sharing/ShareTarget$FileFormField;

    invoke-direct {v0, v1, v2}, Landroidx/browser/trusted/sharing/ShareTarget$FileFormField;-><init>(Ljava/lang/String;Ljava/util/List;)V

    return-object v0

    .line 289
    :cond_2
    :goto_0
    return-object v0
.end method


# virtual methods
.method toBundle()Landroid/os/Bundle;
    .locals 3

    .line 274
    new-instance v0, Landroid/os/Bundle;

    invoke-direct {v0}, Landroid/os/Bundle;-><init>()V

    .line 275
    .local v0, "bundle":Landroid/os/Bundle;
    iget-object v1, p0, Landroidx/browser/trusted/sharing/ShareTarget$FileFormField;->name:Ljava/lang/String;

    const-string v2, "androidx.browser.trusted.sharing.KEY_FILE_NAME"

    invoke-virtual {v0, v2, v1}, Landroid/os/Bundle;->putString(Ljava/lang/String;Ljava/lang/String;)V

    .line 276
    new-instance v1, Ljava/util/ArrayList;

    iget-object v2, p0, Landroidx/browser/trusted/sharing/ShareTarget$FileFormField;->acceptedTypes:Ljava/util/List;

    invoke-direct {v1, v2}, Ljava/util/ArrayList;-><init>(Ljava/util/Collection;)V

    const-string v2, "androidx.browser.trusted.sharing.KEY_ACCEPTED_TYPES"

    invoke-virtual {v0, v2, v1}, Landroid/os/Bundle;->putStringArrayList(Ljava/lang/String;Ljava/util/ArrayList;)V

    .line 277
    return-object v0
.end method
