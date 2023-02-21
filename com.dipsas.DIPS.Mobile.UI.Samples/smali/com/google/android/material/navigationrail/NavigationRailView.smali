.class public Lcom/google/android/material/navigationrail/NavigationRailView;
.super Lcom/google/android/material/navigation/NavigationBarView;
.source "NavigationRailView.java"


# static fields
.field private static final DEFAULT_HEADER_GRAVITY:I = 0x31

.field static final DEFAULT_MENU_GRAVITY:I = 0x31

.field static final MAX_ITEM_COUNT:I = 0x7

.field static final NO_ITEM_MINIMUM_HEIGHT:I = -0x1


# instance fields
.field private headerView:Landroid/view/View;

.field private final topMargin:I


# direct methods
.method public constructor <init>(Landroid/content/Context;)V
    .locals 1
    .param p1, "context"    # Landroid/content/Context;

    .line 108
    const/4 v0, 0x0

    invoke-direct {p0, p1, v0}, Lcom/google/android/material/navigationrail/NavigationRailView;-><init>(Landroid/content/Context;Landroid/util/AttributeSet;)V

    .line 109
    return-void
.end method

.method public constructor <init>(Landroid/content/Context;Landroid/util/AttributeSet;)V
    .locals 1
    .param p1, "context"    # Landroid/content/Context;
    .param p2, "attrs"    # Landroid/util/AttributeSet;

    .line 112
    sget v0, Lcom/google/android/material/R$attr;->navigationRailStyle:I

    invoke-direct {p0, p1, p2, v0}, Lcom/google/android/material/navigationrail/NavigationRailView;-><init>(Landroid/content/Context;Landroid/util/AttributeSet;I)V

    .line 113
    return-void
.end method

.method public constructor <init>(Landroid/content/Context;Landroid/util/AttributeSet;I)V
    .locals 1
    .param p1, "context"    # Landroid/content/Context;
    .param p2, "attrs"    # Landroid/util/AttributeSet;
    .param p3, "defStyleAttr"    # I

    .line 117
    sget v0, Lcom/google/android/material/R$style;->Widget_MaterialComponents_NavigationRailView:I

    invoke-direct {p0, p1, p2, p3, v0}, Lcom/google/android/material/navigationrail/NavigationRailView;-><init>(Landroid/content/Context;Landroid/util/AttributeSet;II)V

    .line 118
    return-void
.end method

.method public constructor <init>(Landroid/content/Context;Landroid/util/AttributeSet;II)V
    .locals 8
    .param p1, "context"    # Landroid/content/Context;
    .param p2, "attrs"    # Landroid/util/AttributeSet;
    .param p3, "defStyleAttr"    # I
    .param p4, "defStyleRes"    # I

    .line 122
    invoke-direct {p0, p1, p2, p3, p4}, Lcom/google/android/material/navigation/NavigationBarView;-><init>(Landroid/content/Context;Landroid/util/AttributeSet;II)V

    .line 124
    invoke-virtual {p0}, Lcom/google/android/material/navigationrail/NavigationRailView;->getResources()Landroid/content/res/Resources;

    move-result-object v0

    .line 125
    .local v0, "res":Landroid/content/res/Resources;
    sget v1, Lcom/google/android/material/R$dimen;->mtrl_navigation_rail_margin:I

    invoke-virtual {v0, v1}, Landroid/content/res/Resources;->getDimensionPixelSize(I)I

    move-result v1

    iput v1, p0, Lcom/google/android/material/navigationrail/NavigationRailView;->topMargin:I

    .line 128
    invoke-virtual {p0}, Lcom/google/android/material/navigationrail/NavigationRailView;->getContext()Landroid/content/Context;

    move-result-object p1

    .line 131
    sget-object v4, Lcom/google/android/material/R$styleable;->NavigationRailView:[I

    const/4 v1, 0x0

    new-array v7, v1, [I

    .line 132
    move-object v2, p1

    move-object v3, p2

    move v5, p3

    move v6, p4

    invoke-static/range {v2 .. v7}, Lcom/google/android/material/internal/ThemeEnforcement;->obtainTintedStyledAttributes(Landroid/content/Context;Landroid/util/AttributeSet;[III[I)Landroidx/appcompat/widget/TintTypedArray;

    move-result-object v2

    .line 135
    .local v2, "attributes":Landroidx/appcompat/widget/TintTypedArray;
    sget v3, Lcom/google/android/material/R$styleable;->NavigationRailView_headerLayout:I

    invoke-virtual {v2, v3, v1}, Landroidx/appcompat/widget/TintTypedArray;->getResourceId(II)I

    move-result v1

    .line 136
    .local v1, "headerLayoutRes":I
    if-eqz v1, :cond_0

    .line 137
    invoke-virtual {p0, v1}, Lcom/google/android/material/navigationrail/NavigationRailView;->addHeaderView(I)V

    .line 140
    :cond_0
    sget v3, Lcom/google/android/material/R$styleable;->NavigationRailView_menuGravity:I

    const/16 v4, 0x31

    .line 141
    invoke-virtual {v2, v3, v4}, Landroidx/appcompat/widget/TintTypedArray;->getInt(II)I

    move-result v3

    .line 140
    invoke-virtual {p0, v3}, Lcom/google/android/material/navigationrail/NavigationRailView;->setMenuGravity(I)V

    .line 143
    sget v3, Lcom/google/android/material/R$styleable;->NavigationRailView_itemMinHeight:I

    invoke-virtual {v2, v3}, Landroidx/appcompat/widget/TintTypedArray;->hasValue(I)Z

    move-result v3

    if-eqz v3, :cond_1

    .line 144
    sget v3, Lcom/google/android/material/R$styleable;->NavigationRailView_itemMinHeight:I

    const/4 v4, -0x1

    .line 145
    invoke-virtual {v2, v3, v4}, Landroidx/appcompat/widget/TintTypedArray;->getDimensionPixelSize(II)I

    move-result v3

    .line 144
    invoke-virtual {p0, v3}, Lcom/google/android/material/navigationrail/NavigationRailView;->setItemMinimumHeight(I)V

    .line 149
    :cond_1
    invoke-virtual {v2}, Landroidx/appcompat/widget/TintTypedArray;->recycle()V

    .line 151
    invoke-direct {p0}, Lcom/google/android/material/navigationrail/NavigationRailView;->applyWindowInsets()V

    .line 152
    return-void
.end method

.method private applyWindowInsets()V
    .locals 1

    .line 155
    new-instance v0, Lcom/google/android/material/navigationrail/NavigationRailView$1;

    invoke-direct {v0, p0}, Lcom/google/android/material/navigationrail/NavigationRailView$1;-><init>(Lcom/google/android/material/navigationrail/NavigationRailView;)V

    invoke-static {p0, v0}, Lcom/google/android/material/internal/ViewUtils;->doOnApplyWindowInsets(Landroid/view/View;Lcom/google/android/material/internal/ViewUtils$OnApplyWindowInsetsListener;)V

    .line 177
    return-void
.end method

.method private getNavigationRailMenuView()Lcom/google/android/material/navigationrail/NavigationRailMenuView;
    .locals 1

    .line 310
    invoke-virtual {p0}, Lcom/google/android/material/navigationrail/NavigationRailView;->getMenuView()Landroidx/appcompat/view/menu/MenuView;

    move-result-object v0

    check-cast v0, Lcom/google/android/material/navigationrail/NavigationRailMenuView;

    return-object v0
.end method

.method private isHeaderViewVisible()Z
    .locals 2

    .line 334
    iget-object v0, p0, Lcom/google/android/material/navigationrail/NavigationRailView;->headerView:Landroid/view/View;

    if-eqz v0, :cond_0

    invoke-virtual {v0}, Landroid/view/View;->getVisibility()I

    move-result v0

    const/16 v1, 0x8

    if-eq v0, v1, :cond_0

    const/4 v0, 0x1

    goto :goto_0

    :cond_0
    const/4 v0, 0x0

    :goto_0
    return v0
.end method

.method private makeMinWidthSpec(I)I
    .locals 4
    .param p1, "measureSpec"    # I

    .line 322
    invoke-virtual {p0}, Lcom/google/android/material/navigationrail/NavigationRailView;->getSuggestedMinimumWidth()I

    move-result v0

    .line 323
    .local v0, "minWidth":I
    invoke-static {p1}, Landroid/view/View$MeasureSpec;->getMode(I)I

    move-result v1

    const/high16 v2, 0x40000000    # 2.0f

    if-eq v1, v2, :cond_0

    if-lez v0, :cond_0

    .line 324
    invoke-virtual {p0}, Lcom/google/android/material/navigationrail/NavigationRailView;->getPaddingLeft()I

    move-result v1

    invoke-virtual {p0}, Lcom/google/android/material/navigationrail/NavigationRailView;->getPaddingRight()I

    move-result v3

    add-int/2addr v1, v3

    add-int/2addr v0, v1

    .line 326
    nop

    .line 327
    invoke-static {p1}, Landroid/view/View$MeasureSpec;->getSize(I)I

    move-result v1

    invoke-static {v1, v0}, Ljava/lang/Math;->min(II)I

    move-result v1

    .line 326
    invoke-static {v1, v2}, Landroid/view/View$MeasureSpec;->makeMeasureSpec(II)I

    move-result v1

    return v1

    .line 330
    :cond_0
    return p1
.end method


# virtual methods
.method public addHeaderView(I)V
    .locals 2
    .param p1, "layoutRes"    # I

    .line 227
    invoke-virtual {p0}, Lcom/google/android/material/navigationrail/NavigationRailView;->getContext()Landroid/content/Context;

    move-result-object v0

    invoke-static {v0}, Landroid/view/LayoutInflater;->from(Landroid/content/Context;)Landroid/view/LayoutInflater;

    move-result-object v0

    const/4 v1, 0x0

    invoke-virtual {v0, p1, p0, v1}, Landroid/view/LayoutInflater;->inflate(ILandroid/view/ViewGroup;Z)Landroid/view/View;

    move-result-object v0

    invoke-virtual {p0, v0}, Lcom/google/android/material/navigationrail/NavigationRailView;->addHeaderView(Landroid/view/View;)V

    .line 228
    return-void
.end method

.method public addHeaderView(Landroid/view/View;)V
    .locals 2
    .param p1, "headerView"    # Landroid/view/View;

    .line 240
    invoke-virtual {p0}, Lcom/google/android/material/navigationrail/NavigationRailView;->removeHeaderView()V

    .line 241
    iput-object p1, p0, Lcom/google/android/material/navigationrail/NavigationRailView;->headerView:Landroid/view/View;

    .line 243
    new-instance v0, Landroid/widget/FrameLayout$LayoutParams;

    const/4 v1, -0x2

    invoke-direct {v0, v1, v1}, Landroid/widget/FrameLayout$LayoutParams;-><init>(II)V

    .line 244
    .local v0, "params":Landroid/widget/FrameLayout$LayoutParams;
    const/16 v1, 0x31

    iput v1, v0, Landroid/widget/FrameLayout$LayoutParams;->gravity:I

    .line 245
    iget v1, p0, Lcom/google/android/material/navigationrail/NavigationRailView;->topMargin:I

    iput v1, v0, Landroid/widget/FrameLayout$LayoutParams;->topMargin:I

    .line 246
    const/4 v1, 0x0

    invoke-virtual {p0, p1, v1, v0}, Lcom/google/android/material/navigationrail/NavigationRailView;->addView(Landroid/view/View;ILandroid/view/ViewGroup$LayoutParams;)V

    .line 247
    return-void
.end method

.method protected bridge synthetic createNavigationBarMenuView(Landroid/content/Context;)Lcom/google/android/material/navigation/NavigationBarMenuView;
    .locals 0

    .line 97
    invoke-virtual {p0, p1}, Lcom/google/android/material/navigationrail/NavigationRailView;->createNavigationBarMenuView(Landroid/content/Context;)Lcom/google/android/material/navigationrail/NavigationRailMenuView;

    move-result-object p1

    return-object p1
.end method

.method protected createNavigationBarMenuView(Landroid/content/Context;)Lcom/google/android/material/navigationrail/NavigationRailMenuView;
    .locals 1
    .param p1, "context"    # Landroid/content/Context;

    .line 318
    new-instance v0, Lcom/google/android/material/navigationrail/NavigationRailMenuView;

    invoke-direct {v0, p1}, Lcom/google/android/material/navigationrail/NavigationRailMenuView;-><init>(Landroid/content/Context;)V

    return-object v0
.end method

.method public getHeaderView()Landroid/view/View;
    .locals 1

    .line 259
    iget-object v0, p0, Lcom/google/android/material/navigationrail/NavigationRailView;->headerView:Landroid/view/View;

    return-object v0
.end method

.method public getItemMinimumHeight()I
    .locals 2

    .line 290
    invoke-virtual {p0}, Lcom/google/android/material/navigationrail/NavigationRailView;->getMenuView()Landroidx/appcompat/view/menu/MenuView;

    move-result-object v0

    check-cast v0, Lcom/google/android/material/navigationrail/NavigationRailMenuView;

    .line 291
    .local v0, "menuView":Lcom/google/android/material/navigationrail/NavigationRailMenuView;
    invoke-virtual {v0}, Lcom/google/android/material/navigationrail/NavigationRailMenuView;->getItemMinimumHeight()I

    move-result v1

    return v1
.end method

.method public getMaxItemCount()I
    .locals 1

    .line 306
    const/4 v0, 0x7

    return v0
.end method

.method public getMenuGravity()I
    .locals 1

    .line 283
    invoke-direct {p0}, Lcom/google/android/material/navigationrail/NavigationRailView;->getNavigationRailMenuView()Lcom/google/android/material/navigationrail/NavigationRailMenuView;

    move-result-object v0

    invoke-virtual {v0}, Lcom/google/android/material/navigationrail/NavigationRailMenuView;->getMenuGravity()I

    move-result v0

    return v0
.end method

.method protected onLayout(ZIIII)V
    .locals 6
    .param p1, "changed"    # Z
    .param p2, "left"    # I
    .param p3, "top"    # I
    .param p4, "right"    # I
    .param p5, "bottom"    # I

    .line 193
    invoke-super/range {p0 .. p5}, Lcom/google/android/material/navigation/NavigationBarView;->onLayout(ZIIII)V

    .line 195
    invoke-direct {p0}, Lcom/google/android/material/navigationrail/NavigationRailView;->getNavigationRailMenuView()Lcom/google/android/material/navigationrail/NavigationRailMenuView;

    move-result-object v0

    .line 196
    .local v0, "menuView":Lcom/google/android/material/navigationrail/NavigationRailMenuView;
    const/4 v1, 0x0

    .line 197
    .local v1, "offsetY":I
    invoke-direct {p0}, Lcom/google/android/material/navigationrail/NavigationRailView;->isHeaderViewVisible()Z

    move-result v2

    if-eqz v2, :cond_0

    .line 198
    iget-object v2, p0, Lcom/google/android/material/navigationrail/NavigationRailView;->headerView:Landroid/view/View;

    invoke-virtual {v2}, Landroid/view/View;->getBottom()I

    move-result v2

    iget v3, p0, Lcom/google/android/material/navigationrail/NavigationRailView;->topMargin:I

    add-int/2addr v2, v3

    .line 199
    .local v2, "usedTop":I
    invoke-virtual {v0}, Lcom/google/android/material/navigationrail/NavigationRailMenuView;->getTop()I

    move-result v3

    .line 200
    .local v3, "menuTop":I
    if-ge v3, v2, :cond_1

    .line 201
    sub-int v1, v2, v3

    goto :goto_0

    .line 203
    .end local v2    # "usedTop":I
    .end local v3    # "menuTop":I
    :cond_0
    invoke-virtual {v0}, Lcom/google/android/material/navigationrail/NavigationRailMenuView;->isTopGravity()Z

    move-result v2

    if-eqz v2, :cond_1

    .line 204
    iget v1, p0, Lcom/google/android/material/navigationrail/NavigationRailView;->topMargin:I

    goto :goto_1

    .line 203
    :cond_1
    :goto_0
    nop

    .line 207
    :goto_1
    if-lez v1, :cond_2

    .line 208
    nop

    .line 209
    invoke-virtual {v0}, Lcom/google/android/material/navigationrail/NavigationRailMenuView;->getLeft()I

    move-result v2

    .line 210
    invoke-virtual {v0}, Lcom/google/android/material/navigationrail/NavigationRailMenuView;->getTop()I

    move-result v3

    add-int/2addr v3, v1

    .line 211
    invoke-virtual {v0}, Lcom/google/android/material/navigationrail/NavigationRailMenuView;->getRight()I

    move-result v4

    .line 212
    invoke-virtual {v0}, Lcom/google/android/material/navigationrail/NavigationRailMenuView;->getBottom()I

    move-result v5

    add-int/2addr v5, v1

    .line 208
    invoke-virtual {v0, v2, v3, v4, v5}, Lcom/google/android/material/navigationrail/NavigationRailMenuView;->layout(IIII)V

    .line 214
    :cond_2
    return-void
.end method

.method protected onMeasure(II)V
    .locals 4
    .param p1, "widthMeasureSpec"    # I
    .param p2, "heightMeasureSpec"    # I

    .line 181
    invoke-direct {p0, p1}, Lcom/google/android/material/navigationrail/NavigationRailView;->makeMinWidthSpec(I)I

    move-result v0

    .line 182
    .local v0, "minWidthSpec":I
    invoke-super {p0, v0, p2}, Lcom/google/android/material/navigation/NavigationBarView;->onMeasure(II)V

    .line 184
    invoke-direct {p0}, Lcom/google/android/material/navigationrail/NavigationRailView;->isHeaderViewVisible()Z

    move-result v1

    if-eqz v1, :cond_0

    .line 185
    invoke-virtual {p0}, Lcom/google/android/material/navigationrail/NavigationRailView;->getMeasuredHeight()I

    move-result v1

    iget-object v2, p0, Lcom/google/android/material/navigationrail/NavigationRailView;->headerView:Landroid/view/View;

    invoke-virtual {v2}, Landroid/view/View;->getMeasuredHeight()I

    move-result v2

    sub-int/2addr v1, v2

    iget v2, p0, Lcom/google/android/material/navigationrail/NavigationRailView;->topMargin:I

    sub-int/2addr v1, v2

    .line 186
    .local v1, "maxMenuHeight":I
    const/high16 v2, -0x80000000

    invoke-static {v1, v2}, Landroid/view/View$MeasureSpec;->makeMeasureSpec(II)I

    move-result v2

    .line 187
    .local v2, "menuHeightSpec":I
    invoke-direct {p0}, Lcom/google/android/material/navigationrail/NavigationRailView;->getNavigationRailMenuView()Lcom/google/android/material/navigationrail/NavigationRailMenuView;

    move-result-object v3

    invoke-virtual {p0, v3, v0, v2}, Lcom/google/android/material/navigationrail/NavigationRailView;->measureChild(Landroid/view/View;II)V

    .line 189
    .end local v1    # "maxMenuHeight":I
    .end local v2    # "menuHeightSpec":I
    :cond_0
    return-void
.end method

.method public removeHeaderView()V
    .locals 1

    .line 270
    iget-object v0, p0, Lcom/google/android/material/navigationrail/NavigationRailView;->headerView:Landroid/view/View;

    if-eqz v0, :cond_0

    .line 271
    invoke-virtual {p0, v0}, Lcom/google/android/material/navigationrail/NavigationRailView;->removeView(Landroid/view/View;)V

    .line 272
    const/4 v0, 0x0

    iput-object v0, p0, Lcom/google/android/material/navigationrail/NavigationRailView;->headerView:Landroid/view/View;

    .line 274
    :cond_0
    return-void
.end method

.method public setItemMinimumHeight(I)V
    .locals 1
    .param p1, "minHeight"    # I

    .line 300
    invoke-virtual {p0}, Lcom/google/android/material/navigationrail/NavigationRailView;->getMenuView()Landroidx/appcompat/view/menu/MenuView;

    move-result-object v0

    check-cast v0, Lcom/google/android/material/navigationrail/NavigationRailMenuView;

    .line 301
    .local v0, "menuView":Lcom/google/android/material/navigationrail/NavigationRailMenuView;
    invoke-virtual {v0, p1}, Lcom/google/android/material/navigationrail/NavigationRailMenuView;->setItemMinimumHeight(I)V

    .line 302
    return-void
.end method

.method public setMenuGravity(I)V
    .locals 1
    .param p1, "gravity"    # I

    .line 278
    invoke-direct {p0}, Lcom/google/android/material/navigationrail/NavigationRailView;->getNavigationRailMenuView()Lcom/google/android/material/navigationrail/NavigationRailMenuView;

    move-result-object v0

    invoke-virtual {v0, p1}, Lcom/google/android/material/navigationrail/NavigationRailMenuView;->setMenuGravity(I)V

    .line 279
    return-void
.end method
