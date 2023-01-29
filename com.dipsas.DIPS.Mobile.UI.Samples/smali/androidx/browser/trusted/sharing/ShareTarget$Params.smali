.class public Landroidx/browser/trusted/sharing/ShareTarget$Params;
.super Ljava/lang/Object;
.source "ShareTarget.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Landroidx/browser/trusted/sharing/ShareTarget;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x9
    name = "Params"
.end annotation


# static fields
.field public static final KEY_FILES:Ljava/lang/String; = "androidx.browser.trusted.sharing.KEY_FILES"

.field public static final KEY_TEXT:Ljava/lang/String; = "androidx.browser.trusted.sharing.KEY_TEXT"

.field public static final KEY_TITLE:Ljava/lang/String; = "androidx.browser.trusted.sharing.KEY_TITLE"


# instance fields
.field public final files:Ljava/util/List;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/List<",
            "Landroidx/browser/trusted/sharing/ShareTarget$FileFormField;",
            ">;"
        }
    .end annotation
.end field

.field public final text:Ljava/lang/String;

.field public final title:Ljava/lang/String;


# direct methods
.method public constructor <init>(Ljava/lang/String;Ljava/lang/String;Ljava/util/List;)V
    .locals 0
    .param p1, "title"    # Ljava/lang/String;
    .param p2, "text"    # Ljava/lang/String;
    .annotation system Ldalvik/annotation/MethodParameters;
        accessFlags = {
            0x0,
            0x0,
            0x0
        }
        names = {
            "title",
            "text",
            "files"
        }
    .end annotation

    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            "Ljava/util/List<",
            "Landroidx/browser/trusted/sharing/ShareTarget$FileFormField;",
            ">;)V"
        }
    .end annotation

    .line 198
    .local p3, "files":Ljava/util/List;, "Ljava/util/List<Landroidx/browser/trusted/sharing/ShareTarget$FileFormField;>;"
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 199
    iput-object p1, p0, Landroidx/browser/trusted/sharing/ShareTarget$Params;->title:Ljava/lang/String;

    .line 200
    iput-object p2, p0, Landroidx/browser/trusted/sharing/ShareTarget$Params;->text:Ljava/lang/String;

    .line 201
    iput-object p3, p0, Landroidx/browser/trusted/sharing/ShareTarget$Params;->files:Ljava/util/List;

    .line 202
    return-void
.end method

.method static fromBundle(Landroid/os/Bundle;)Landroidx/browser/trusted/sharing/ShareTarget$Params;
    .locals 5
    .param p0, "bundle"    # Landroid/os/Bundle;
    .annotation system Ldalvik/annotation/MethodParameters;
        accessFlags = {
            0x0
        }
        names = {
            "bundle"
        }
    .end annotation

    .line 224
    if-nez p0, :cond_0

    .line 225
    const/4 v0, 0x0

    return-object v0

    .line 227
    :cond_0
    const/4 v0, 0x0

    .line 228
    .local v0, "files":Ljava/util/List;, "Ljava/util/List<Landroidx/browser/trusted/sharing/ShareTarget$FileFormField;>;"
    const-string v1, "androidx.browser.trusted.sharing.KEY_FILES"

    invoke-virtual {p0, v1}, Landroid/os/Bundle;->getParcelableArrayList(Ljava/lang/String;)Ljava/util/ArrayList;

    move-result-object v1

    .line 229
    .local v1, "fileBundles":Ljava/util/List;, "Ljava/util/List<Landroid/os/Bundle;>;"
    if-eqz v1, :cond_1

    .line 230
    new-instance v2, Ljava/util/ArrayList;

    invoke-direct {v2}, Ljava/util/ArrayList;-><init>()V

    move-object v0, v2

    .line 231
    invoke-interface {v1}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v2

    :goto_0
    invoke-interface {v2}, Ljava/util/Iterator;->hasNext()Z

    move-result v3

    if-eqz v3, :cond_1

    invoke-interface {v2}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v3

    check-cast v3, Landroid/os/Bundle;

    .line 232
    .local v3, "fileBundle":Landroid/os/Bundle;
    invoke-static {v3}, Landroidx/browser/trusted/sharing/ShareTarget$FileFormField;->fromBundle(Landroid/os/Bundle;)Landroidx/browser/trusted/sharing/ShareTarget$FileFormField;

    move-result-object v4

    invoke-interface {v0, v4}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    .line 233
    .end local v3    # "fileBundle":Landroid/os/Bundle;
    goto :goto_0

    .line 235
    :cond_1
    new-instance v2, Landroidx/browser/trusted/sharing/ShareTarget$Params;

    const-string v3, "androidx.browser.trusted.sharing.KEY_TITLE"

    invoke-virtual {p0, v3}, Landroid/os/Bundle;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v3

    const-string v4, "androidx.browser.trusted.sharing.KEY_TEXT"

    invoke-virtual {p0, v4}, Landroid/os/Bundle;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v4

    invoke-direct {v2, v3, v4, v0}, Landroidx/browser/trusted/sharing/ShareTarget$Params;-><init>(Ljava/lang/String;Ljava/lang/String;Ljava/util/List;)V

    return-object v2
.end method


# virtual methods
.method toBundle()Landroid/os/Bundle;
    .locals 5

    .line 207
    new-instance v0, Landroid/os/Bundle;

    invoke-direct {v0}, Landroid/os/Bundle;-><init>()V

    .line 208
    .local v0, "bundle":Landroid/os/Bundle;
    iget-object v1, p0, Landroidx/browser/trusted/sharing/ShareTarget$Params;->title:Ljava/lang/String;

    const-string v2, "androidx.browser.trusted.sharing.KEY_TITLE"

    invoke-virtual {v0, v2, v1}, Landroid/os/Bundle;->putString(Ljava/lang/String;Ljava/lang/String;)V

    .line 209
    iget-object v1, p0, Landroidx/browser/trusted/sharing/ShareTarget$Params;->text:Ljava/lang/String;

    const-string v2, "androidx.browser.trusted.sharing.KEY_TEXT"

    invoke-virtual {v0, v2, v1}, Landroid/os/Bundle;->putString(Ljava/lang/String;Ljava/lang/String;)V

    .line 210
    iget-object v1, p0, Landroidx/browser/trusted/sharing/ShareTarget$Params;->files:Ljava/util/List;

    if-eqz v1, :cond_1

    .line 211
    new-instance v1, Ljava/util/ArrayList;

    invoke-direct {v1}, Ljava/util/ArrayList;-><init>()V

    .line 212
    .local v1, "fileBundles":Ljava/util/ArrayList;, "Ljava/util/ArrayList<Landroid/os/Bundle;>;"
    iget-object v2, p0, Landroidx/browser/trusted/sharing/ShareTarget$Params;->files:Ljava/util/List;

    invoke-interface {v2}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v2

    :goto_0
    invoke-interface {v2}, Ljava/util/Iterator;->hasNext()Z

    move-result v3

    if-eqz v3, :cond_0

    invoke-interface {v2}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v3

    check-cast v3, Landroidx/browser/trusted/sharing/ShareTarget$FileFormField;

    .line 213
    .local v3, "file":Landroidx/browser/trusted/sharing/ShareTarget$FileFormField;
    invoke-virtual {v3}, Landroidx/browser/trusted/sharing/ShareTarget$FileFormField;->toBundle()Landroid/os/Bundle;

    move-result-object v4

    invoke-virtual {v1, v4}, Ljava/util/ArrayList;->add(Ljava/lang/Object;)Z

    .line 214
    .end local v3    # "file":Landroidx/browser/trusted/sharing/ShareTarget$FileFormField;
    goto :goto_0

    .line 215
    :cond_0
    const-string v2, "androidx.browser.trusted.sharing.KEY_FILES"

    invoke-virtual {v0, v2, v1}, Landroid/os/Bundle;->putParcelableArrayList(Ljava/lang/String;Ljava/util/ArrayList;)V

    .line 218
    .end local v1    # "fileBundles":Ljava/util/ArrayList;, "Ljava/util/ArrayList<Landroid/os/Bundle;>;"
    :cond_1
    return-object v0
.end method
