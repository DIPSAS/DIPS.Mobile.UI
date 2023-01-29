.class Landroidx/browser/browseractions/BrowserActionsFallbackMenuUi$2;
.super Ljava/lang/Object;
.source "BrowserActionsFallbackMenuUi.java"

# interfaces
.implements Landroid/content/DialogInterface$OnShowListener;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Landroidx/browser/browseractions/BrowserActionsFallbackMenuUi;->displayMenu()V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Landroidx/browser/browseractions/BrowserActionsFallbackMenuUi;

.field final synthetic val$view:Landroid/view/View;


# direct methods
.method constructor <init>(Landroidx/browser/browseractions/BrowserActionsFallbackMenuUi;Landroid/view/View;)V
    .locals 0
    .param p1, "this$0"    # Landroidx/browser/browseractions/BrowserActionsFallbackMenuUi;
    .annotation system Ldalvik/annotation/MethodParameters;
        accessFlags = {
            0x8010,
            0x1010
        }
        names = {
            "this$0",
            "val$view"
        }
    .end annotation

    .line 146
    iput-object p1, p0, Landroidx/browser/browseractions/BrowserActionsFallbackMenuUi$2;->this$0:Landroidx/browser/browseractions/BrowserActionsFallbackMenuUi;

    iput-object p2, p0, Landroidx/browser/browseractions/BrowserActionsFallbackMenuUi$2;->val$view:Landroid/view/View;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public onShow(Landroid/content/DialogInterface;)V
    .locals 2
    .param p1, "dialogInterface"    # Landroid/content/DialogInterface;
    .annotation system Ldalvik/annotation/MethodParameters;
        accessFlags = {
            0x0
        }
        names = {
            "dialogInterface"
        }
    .end annotation

    .line 149
    iget-object v0, p0, Landroidx/browser/browseractions/BrowserActionsFallbackMenuUi$2;->this$0:Landroidx/browser/browseractions/BrowserActionsFallbackMenuUi;

    iget-object v0, v0, Landroidx/browser/browseractions/BrowserActionsFallbackMenuUi;->mMenuUiListener:Landroidx/browser/browseractions/BrowserActionsFallbackMenuUi$BrowserActionsFallMenuUiListener;

    if-nez v0, :cond_0

    .line 150
    const-string v0, "BrowserActionskMenuUi"

    const-string v1, "Cannot trigger menu item listener, it is null"

    invoke-static {v0, v1}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;)I

    .line 151
    return-void

    .line 153
    :cond_0
    iget-object v0, p0, Landroidx/browser/browseractions/BrowserActionsFallbackMenuUi$2;->this$0:Landroidx/browser/browseractions/BrowserActionsFallbackMenuUi;

    iget-object v0, v0, Landroidx/browser/browseractions/BrowserActionsFallbackMenuUi;->mMenuUiListener:Landroidx/browser/browseractions/BrowserActionsFallbackMenuUi$BrowserActionsFallMenuUiListener;

    iget-object v1, p0, Landroidx/browser/browseractions/BrowserActionsFallbackMenuUi$2;->val$view:Landroid/view/View;

    invoke-interface {v0, v1}, Landroidx/browser/browseractions/BrowserActionsFallbackMenuUi$BrowserActionsFallMenuUiListener;->onMenuShown(Landroid/view/View;)V

    .line 154
    return-void
.end method
