.class public Landroidx/slidingpanelayout/widget/SlidingPaneLayout$LayoutParams;
.super Landroid/view/ViewGroup$MarginLayoutParams;
.source "SlidingPaneLayout.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Landroidx/slidingpanelayout/widget/SlidingPaneLayout;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x9
    name = "LayoutParams"
.end annotation


# static fields
.field private static final ATTRS:[I


# instance fields
.field dimPaint:Landroid/graphics/Paint;

.field dimWhenOffset:Z

.field slideable:Z

.field public weight:F


# direct methods
.method static constructor <clinit>()V
    .locals 3

    .line 1450
    const/4 v0, 0x1

    new-array v0, v0, [I

    const/4 v1, 0x0

    const v2, 0x1010181

    aput v2, v0, v1

    sput-object v0, Landroidx/slidingpanelayout/widget/SlidingPaneLayout$LayoutParams;->ATTRS:[I

    return-void
.end method

.method public constructor <init>()V
    .locals 1

    .line 1474
    const/4 v0, -0x1

    invoke-direct {p0, v0, v0}, Landroid/view/ViewGroup$MarginLayoutParams;-><init>(II)V

    .line 1458
    const/4 v0, 0x0

    iput v0, p0, Landroidx/slidingpanelayout/widget/SlidingPaneLayout$LayoutParams;->weight:F

    .line 1475
    return-void
.end method

.method public constructor <init>(II)V
    .locals 1
    .param p1, "width"    # I
    .param p2, "height"    # I

    .line 1478
    invoke-direct {p0, p1, p2}, Landroid/view/ViewGroup$MarginLayoutParams;-><init>(II)V

    .line 1458
    const/4 v0, 0x0

    iput v0, p0, Landroidx/slidingpanelayout/widget/SlidingPaneLayout$LayoutParams;->weight:F

    .line 1479
    return-void
.end method

.method public constructor <init>(Landroid/content/Context;Landroid/util/AttributeSet;)V
    .locals 3
    .param p1, "c"    # Landroid/content/Context;
    .param p2, "attrs"    # Landroid/util/AttributeSet;

    .line 1495
    invoke-direct {p0, p1, p2}, Landroid/view/ViewGroup$MarginLayoutParams;-><init>(Landroid/content/Context;Landroid/util/AttributeSet;)V

    .line 1458
    const/4 v0, 0x0

    iput v0, p0, Landroidx/slidingpanelayout/widget/SlidingPaneLayout$LayoutParams;->weight:F

    .line 1497
    sget-object v1, Landroidx/slidingpanelayout/widget/SlidingPaneLayout$LayoutParams;->ATTRS:[I

    invoke-virtual {p1, p2, v1}, Landroid/content/Context;->obtainStyledAttributes(Landroid/util/AttributeSet;[I)Landroid/content/res/TypedArray;

    move-result-object v1

    .line 1498
    .local v1, "a":Landroid/content/res/TypedArray;
    const/4 v2, 0x0

    invoke-virtual {v1, v2, v0}, Landroid/content/res/TypedArray;->getFloat(IF)F

    move-result v0

    iput v0, p0, Landroidx/slidingpanelayout/widget/SlidingPaneLayout$LayoutParams;->weight:F

    .line 1499
    invoke-virtual {v1}, Landroid/content/res/TypedArray;->recycle()V

    .line 1500
    return-void
.end method

.method public constructor <init>(Landroid/view/ViewGroup$LayoutParams;)V
    .locals 1
    .param p1, "source"    # Landroid/view/ViewGroup$LayoutParams;

    .line 1482
    invoke-direct {p0, p1}, Landroid/view/ViewGroup$MarginLayoutParams;-><init>(Landroid/view/ViewGroup$LayoutParams;)V

    .line 1458
    const/4 v0, 0x0

    iput v0, p0, Landroidx/slidingpanelayout/widget/SlidingPaneLayout$LayoutParams;->weight:F

    .line 1483
    return-void
.end method

.method public constructor <init>(Landroid/view/ViewGroup$MarginLayoutParams;)V
    .locals 1
    .param p1, "source"    # Landroid/view/ViewGroup$MarginLayoutParams;

    .line 1486
    invoke-direct {p0, p1}, Landroid/view/ViewGroup$MarginLayoutParams;-><init>(Landroid/view/ViewGroup$MarginLayoutParams;)V

    .line 1458
    const/4 v0, 0x0

    iput v0, p0, Landroidx/slidingpanelayout/widget/SlidingPaneLayout$LayoutParams;->weight:F

    .line 1487
    return-void
.end method

.method public constructor <init>(Landroidx/slidingpanelayout/widget/SlidingPaneLayout$LayoutParams;)V
    .locals 1
    .param p1, "source"    # Landroidx/slidingpanelayout/widget/SlidingPaneLayout$LayoutParams;

    .line 1490
    invoke-direct {p0, p1}, Landroid/view/ViewGroup$MarginLayoutParams;-><init>(Landroid/view/ViewGroup$MarginLayoutParams;)V

    .line 1458
    const/4 v0, 0x0

    iput v0, p0, Landroidx/slidingpanelayout/widget/SlidingPaneLayout$LayoutParams;->weight:F

    .line 1491
    iget v0, p1, Landroidx/slidingpanelayout/widget/SlidingPaneLayout$LayoutParams;->weight:F

    iput v0, p0, Landroidx/slidingpanelayout/widget/SlidingPaneLayout$LayoutParams;->weight:F

    .line 1492
    return-void
.end method
