.class public Landroidx/browser/browseractions/BrowserActionsFallbackMenuView;
.super Landroid/widget/LinearLayout;
.source "BrowserActionsFallbackMenuView.java"


# annotations
.annotation runtime Ljava/lang/Deprecated;
.end annotation


# instance fields
.field private final mBrowserActionsMenuMaxWidthPx:I

.field private final mBrowserActionsMenuMinPaddingPx:I


# direct methods
.method public constructor <init>(Landroid/content/Context;Landroid/util/AttributeSet;)V
    .locals 2
    .param p1, "context"    # Landroid/content/Context;
    .param p2, "attrs"    # Landroid/util/AttributeSet;
    .annotation system Ldalvik/annotation/MethodParameters;
        accessFlags = {
            0x0,
            0x0
        }
        names = {
            "context",
            "attrs"
        }
    .end annotation

    .line 42
    invoke-direct {p0, p1, p2}, Landroid/widget/LinearLayout;-><init>(Landroid/content/Context;Landroid/util/AttributeSet;)V

    .line 43
    invoke-virtual {p0}, Landroidx/browser/browseractions/BrowserActionsFallbackMenuView;->getResources()Landroid/content/res/Resources;

    move-result-object v0

    sget v1, Landroidx/browser/R$dimen;->browser_actions_context_menu_min_padding:I

    invoke-virtual {v0, v1}, Landroid/content/res/Resources;->getDimensionPixelOffset(I)I

    move-result v0

    iput v0, p0, Landroidx/browser/browseractions/BrowserActionsFallbackMenuView;->mBrowserActionsMenuMinPaddingPx:I

    .line 45
    invoke-virtual {p0}, Landroidx/browser/browseractions/BrowserActionsFallbackMenuView;->getResources()Landroid/content/res/Resources;

    move-result-object v0

    sget v1, Landroidx/browser/R$dimen;->browser_actions_context_menu_max_width:I

    invoke-virtual {v0, v1}, Landroid/content/res/Resources;->getDimensionPixelOffset(I)I

    move-result v0

    iput v0, p0, Landroidx/browser/browseractions/BrowserActionsFallbackMenuView;->mBrowserActionsMenuMaxWidthPx:I

    .line 47
    return-void
.end method


# virtual methods
.method protected onMeasure(II)V
    .locals 3
    .param p1, "widthMeasureSpec"    # I
    .param p2, "heightMeasureSpec"    # I
    .annotation system Ldalvik/annotation/MethodParameters;
        accessFlags = {
            0x0,
            0x0
        }
        names = {
            "widthMeasureSpec",
            "heightMeasureSpec"
        }
    .end annotation

    .line 51
    invoke-virtual {p0}, Landroidx/browser/browseractions/BrowserActionsFallbackMenuView;->getResources()Landroid/content/res/Resources;

    move-result-object v0

    invoke-virtual {v0}, Landroid/content/res/Resources;->getDisplayMetrics()Landroid/util/DisplayMetrics;

    move-result-object v0

    iget v0, v0, Landroid/util/DisplayMetrics;->widthPixels:I

    .line 52
    .local v0, "appWindowWidthPx":I
    iget v1, p0, Landroidx/browser/browseractions/BrowserActionsFallbackMenuView;->mBrowserActionsMenuMinPaddingPx:I

    mul-int/lit8 v1, v1, 0x2

    sub-int v1, v0, v1

    iget v2, p0, Landroidx/browser/browseractions/BrowserActionsFallbackMenuView;->mBrowserActionsMenuMaxWidthPx:I

    invoke-static {v1, v2}, Ljava/lang/Math;->min(II)I

    move-result v1

    .line 54
    .local v1, "contextMenuWidth":I
    const/high16 v2, 0x40000000    # 2.0f

    invoke-static {v1, v2}, Landroid/view/View$MeasureSpec;->makeMeasureSpec(II)I

    move-result p1

    .line 55
    invoke-super {p0, p1, p2}, Landroid/widget/LinearLayout;->onMeasure(II)V

    .line 56
    return-void
.end method
