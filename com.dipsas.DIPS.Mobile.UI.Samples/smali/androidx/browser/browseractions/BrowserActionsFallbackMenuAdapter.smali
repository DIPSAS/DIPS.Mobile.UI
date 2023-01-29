.class Landroidx/browser/browseractions/BrowserActionsFallbackMenuAdapter;
.super Landroid/widget/BaseAdapter;
.source "BrowserActionsFallbackMenuAdapter.java"


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Landroidx/browser/browseractions/BrowserActionsFallbackMenuAdapter$ViewHolderItem;
    }
.end annotation

.annotation runtime Ljava/lang/Deprecated;
.end annotation


# instance fields
.field private final mContext:Landroid/content/Context;

.field private final mMenuItems:Ljava/util/List;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/List<",
            "Landroidx/browser/browseractions/BrowserActionItem;",
            ">;"
        }
    .end annotation
.end field


# direct methods
.method constructor <init>(Ljava/util/List;Landroid/content/Context;)V
    .locals 0
    .param p2, "context"    # Landroid/content/Context;
    .annotation system Ldalvik/annotation/MethodParameters;
        accessFlags = {
            0x0,
            0x0
        }
        names = {
            "menuItems",
            "context"
        }
    .end annotation

    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/List<",
            "Landroidx/browser/browseractions/BrowserActionItem;",
            ">;",
            "Landroid/content/Context;",
            ")V"
        }
    .end annotation

    .line 50
    .local p1, "menuItems":Ljava/util/List;, "Ljava/util/List<Landroidx/browser/browseractions/BrowserActionItem;>;"
    invoke-direct {p0}, Landroid/widget/BaseAdapter;-><init>()V

    .line 51
    iput-object p1, p0, Landroidx/browser/browseractions/BrowserActionsFallbackMenuAdapter;->mMenuItems:Ljava/util/List;

    .line 52
    iput-object p2, p0, Landroidx/browser/browseractions/BrowserActionsFallbackMenuAdapter;->mContext:Landroid/content/Context;

    .line 53
    return-void
.end method


# virtual methods
.method public getCount()I
    .locals 1

    .line 57
    iget-object v0, p0, Landroidx/browser/browseractions/BrowserActionsFallbackMenuAdapter;->mMenuItems:Ljava/util/List;

    invoke-interface {v0}, Ljava/util/List;->size()I

    move-result v0

    return v0
.end method

.method public getItem(I)Ljava/lang/Object;
    .locals 1
    .param p1, "position"    # I
    .annotation system Ldalvik/annotation/MethodParameters;
        accessFlags = {
            0x0
        }
        names = {
            "position"
        }
    .end annotation

    .line 62
    iget-object v0, p0, Landroidx/browser/browseractions/BrowserActionsFallbackMenuAdapter;->mMenuItems:Ljava/util/List;

    invoke-interface {v0, p1}, Ljava/util/List;->get(I)Ljava/lang/Object;

    move-result-object v0

    return-object v0
.end method

.method public getItemId(I)J
    .locals 2
    .param p1, "position"    # I
    .annotation system Ldalvik/annotation/MethodParameters;
        accessFlags = {
            0x0
        }
        names = {
            "position"
        }
    .end annotation

    .line 67
    int-to-long v0, p1

    return-wide v0
.end method

.method public getView(ILandroid/view/View;Landroid/view/ViewGroup;)Landroid/view/View;
    .locals 6
    .param p1, "position"    # I
    .param p2, "convertView"    # Landroid/view/View;
    .param p3, "parent"    # Landroid/view/ViewGroup;
    .annotation system Ldalvik/annotation/MethodParameters;
        accessFlags = {
            0x0,
            0x0,
            0x0
        }
        names = {
            "position",
            "convertView",
            "parent"
        }
    .end annotation

    .line 72
    iget-object v0, p0, Landroidx/browser/browseractions/BrowserActionsFallbackMenuAdapter;->mMenuItems:Ljava/util/List;

    invoke-interface {v0, p1}, Ljava/util/List;->get(I)Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Landroidx/browser/browseractions/BrowserActionItem;

    .line 74
    .local v0, "menuItem":Landroidx/browser/browseractions/BrowserActionItem;
    const/4 v1, 0x0

    if-nez p2, :cond_1

    .line 75
    iget-object v2, p0, Landroidx/browser/browseractions/BrowserActionsFallbackMenuAdapter;->mContext:Landroid/content/Context;

    invoke-static {v2}, Landroid/view/LayoutInflater;->from(Landroid/content/Context;)Landroid/view/LayoutInflater;

    move-result-object v2

    sget v3, Landroidx/browser/R$layout;->browser_actions_context_menu_row:I

    invoke-virtual {v2, v3, v1}, Landroid/view/LayoutInflater;->inflate(ILandroid/view/ViewGroup;)Landroid/view/View;

    move-result-object p2

    .line 77
    sget v2, Landroidx/browser/R$id;->browser_actions_menu_item_icon:I

    invoke-virtual {p2, v2}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object v2

    check-cast v2, Landroid/widget/ImageView;

    .line 78
    .local v2, "icon":Landroid/widget/ImageView;
    sget v3, Landroidx/browser/R$id;->browser_actions_menu_item_text:I

    invoke-virtual {p2, v3}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object v3

    check-cast v3, Landroid/widget/TextView;

    .line 79
    .local v3, "text":Landroid/widget/TextView;
    if-eqz v2, :cond_0

    if-eqz v3, :cond_0

    .line 83
    new-instance v4, Landroidx/browser/browseractions/BrowserActionsFallbackMenuAdapter$ViewHolderItem;

    invoke-direct {v4, v2, v3}, Landroidx/browser/browseractions/BrowserActionsFallbackMenuAdapter$ViewHolderItem;-><init>(Landroid/widget/ImageView;Landroid/widget/TextView;)V

    .line 84
    .local v4, "viewHolder":Landroidx/browser/browseractions/BrowserActionsFallbackMenuAdapter$ViewHolderItem;
    invoke-virtual {p2, v4}, Landroid/view/View;->setTag(Ljava/lang/Object;)V

    .line 85
    .end local v2    # "icon":Landroid/widget/ImageView;
    .end local v3    # "text":Landroid/widget/TextView;
    goto :goto_0

    .line 80
    .end local v4    # "viewHolder":Landroidx/browser/browseractions/BrowserActionsFallbackMenuAdapter$ViewHolderItem;
    .restart local v2    # "icon":Landroid/widget/ImageView;
    .restart local v3    # "text":Landroid/widget/TextView;
    :cond_0
    new-instance v1, Ljava/lang/IllegalStateException;

    const-string v4, "Browser Actions fallback UI does not contain necessary Views."

    invoke-direct {v1, v4}, Ljava/lang/IllegalStateException;-><init>(Ljava/lang/String;)V

    throw v1

    .line 86
    .end local v2    # "icon":Landroid/widget/ImageView;
    .end local v3    # "text":Landroid/widget/TextView;
    :cond_1
    invoke-virtual {p2}, Landroid/view/View;->getTag()Ljava/lang/Object;

    move-result-object v2

    move-object v4, v2

    check-cast v4, Landroidx/browser/browseractions/BrowserActionsFallbackMenuAdapter$ViewHolderItem;

    .line 89
    .restart local v4    # "viewHolder":Landroidx/browser/browseractions/BrowserActionsFallbackMenuAdapter$ViewHolderItem;
    :goto_0
    invoke-virtual {v0}, Landroidx/browser/browseractions/BrowserActionItem;->getTitle()Ljava/lang/String;

    move-result-object v2

    .line 90
    .local v2, "titleText":Ljava/lang/String;
    iget-object v3, v4, Landroidx/browser/browseractions/BrowserActionsFallbackMenuAdapter$ViewHolderItem;->mText:Landroid/widget/TextView;

    invoke-virtual {v3, v2}, Landroid/widget/TextView;->setText(Ljava/lang/CharSequence;)V

    .line 91
    invoke-virtual {v0}, Landroidx/browser/browseractions/BrowserActionItem;->getIconId()I

    move-result v3

    if-eqz v3, :cond_2

    .line 92
    iget-object v3, p0, Landroidx/browser/browseractions/BrowserActionsFallbackMenuAdapter;->mContext:Landroid/content/Context;

    .line 93
    invoke-virtual {v3}, Landroid/content/Context;->getResources()Landroid/content/res/Resources;

    move-result-object v3

    invoke-virtual {v0}, Landroidx/browser/browseractions/BrowserActionItem;->getIconId()I

    move-result v5

    .line 92
    invoke-static {v3, v5, v1}, Landroidx/core/content/res/ResourcesCompat;->getDrawable(Landroid/content/res/Resources;ILandroid/content/res/Resources$Theme;)Landroid/graphics/drawable/Drawable;

    move-result-object v1

    .line 94
    .local v1, "drawable":Landroid/graphics/drawable/Drawable;
    iget-object v3, v4, Landroidx/browser/browseractions/BrowserActionsFallbackMenuAdapter$ViewHolderItem;->mIcon:Landroid/widget/ImageView;

    invoke-virtual {v3, v1}, Landroid/widget/ImageView;->setImageDrawable(Landroid/graphics/drawable/Drawable;)V

    .line 95
    .end local v1    # "drawable":Landroid/graphics/drawable/Drawable;
    goto :goto_1

    :cond_2
    invoke-virtual {v0}, Landroidx/browser/browseractions/BrowserActionItem;->getIconUri()Landroid/net/Uri;

    move-result-object v3

    if-eqz v3, :cond_3

    .line 96
    iget-object v1, p0, Landroidx/browser/browseractions/BrowserActionsFallbackMenuAdapter;->mContext:Landroid/content/Context;

    .line 97
    invoke-virtual {v1}, Landroid/content/Context;->getContentResolver()Landroid/content/ContentResolver;

    move-result-object v1

    invoke-virtual {v0}, Landroidx/browser/browseractions/BrowserActionItem;->getIconUri()Landroid/net/Uri;

    move-result-object v3

    invoke-static {v1, v3}, Landroidx/browser/browseractions/BrowserServiceFileProvider;->loadBitmap(Landroid/content/ContentResolver;Landroid/net/Uri;)Lcom/google/common/util/concurrent/ListenableFuture;

    move-result-object v1

    .line 99
    .local v1, "bitmapFuture":Lcom/google/common/util/concurrent/ListenableFuture;, "Lcom/google/common/util/concurrent/ListenableFuture<Landroid/graphics/Bitmap;>;"
    new-instance v3, Landroidx/browser/browseractions/BrowserActionsFallbackMenuAdapter$1;

    invoke-direct {v3, p0, v2, v4, v1}, Landroidx/browser/browseractions/BrowserActionsFallbackMenuAdapter$1;-><init>(Landroidx/browser/browseractions/BrowserActionsFallbackMenuAdapter;Ljava/lang/String;Landroidx/browser/browseractions/BrowserActionsFallbackMenuAdapter$ViewHolderItem;Lcom/google/common/util/concurrent/ListenableFuture;)V

    new-instance v5, Landroidx/browser/browseractions/BrowserActionsFallbackMenuAdapter$2;

    invoke-direct {v5, p0}, Landroidx/browser/browseractions/BrowserActionsFallbackMenuAdapter$2;-><init>(Landroidx/browser/browseractions/BrowserActionsFallbackMenuAdapter;)V

    invoke-interface {v1, v3, v5}, Lcom/google/common/util/concurrent/ListenableFuture;->addListener(Ljava/lang/Runnable;Ljava/util/concurrent/Executor;)V

    .line 127
    .end local v1    # "bitmapFuture":Lcom/google/common/util/concurrent/ListenableFuture;, "Lcom/google/common/util/concurrent/ListenableFuture<Landroid/graphics/Bitmap;>;"
    goto :goto_1

    .line 128
    :cond_3
    iget-object v3, v4, Landroidx/browser/browseractions/BrowserActionsFallbackMenuAdapter$ViewHolderItem;->mIcon:Landroid/widget/ImageView;

    invoke-virtual {v3, v1}, Landroid/widget/ImageView;->setImageBitmap(Landroid/graphics/Bitmap;)V

    .line 129
    iget-object v1, v4, Landroidx/browser/browseractions/BrowserActionsFallbackMenuAdapter$ViewHolderItem;->mIcon:Landroid/widget/ImageView;

    const/4 v3, 0x4

    invoke-virtual {v1, v3}, Landroid/widget/ImageView;->setVisibility(I)V

    .line 131
    :goto_1
    return-object p2
.end method
