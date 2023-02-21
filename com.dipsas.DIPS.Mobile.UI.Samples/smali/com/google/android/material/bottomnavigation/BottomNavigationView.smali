.class public Lcom/google/android/material/bottomnavigation/BottomNavigationView;
.super Lcom/google/android/material/navigation/NavigationBarView;
.source "BottomNavigationView.java"


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lcom/google/android/material/bottomnavigation/BottomNavigationView$OnNavigationItemReselectedListener;,
        Lcom/google/android/material/bottomnavigation/BottomNavigationView$OnNavigationItemSelectedListener;
    }
.end annotation


# static fields
.field static final MAX_ITEM_COUNT:I = 0x5


# direct methods
.method public constructor <init>(Landroid/content/Context;)V
    .locals 1
    .param p1, "context"    # Landroid/content/Context;

    .line 94
    const/4 v0, 0x0

    invoke-direct {p0, p1, v0}, Lcom/google/android/material/bottomnavigation/BottomNavigationView;-><init>(Landroid/content/Context;Landroid/util/AttributeSet;)V

    .line 95
    return-void
.end method

.method public constructor <init>(Landroid/content/Context;Landroid/util/AttributeSet;)V
    .locals 1
    .param p1, "context"    # Landroid/content/Context;
    .param p2, "attrs"    # Landroid/util/AttributeSet;

    .line 98
    sget v0, Lcom/google/android/material/R$attr;->bottomNavigationStyle:I

    invoke-direct {p0, p1, p2, v0}, Lcom/google/android/material/bottomnavigation/BottomNavigationView;-><init>(Landroid/content/Context;Landroid/util/AttributeSet;I)V

    .line 99
    return-void
.end method

.method public constructor <init>(Landroid/content/Context;Landroid/util/AttributeSet;I)V
    .locals 1
    .param p1, "context"    # Landroid/content/Context;
    .param p2, "attrs"    # Landroid/util/AttributeSet;
    .param p3, "defStyleAttr"    # I

    .line 103
    sget v0, Lcom/google/android/material/R$style;->Widget_Design_BottomNavigationView:I

    invoke-direct {p0, p1, p2, p3, v0}, Lcom/google/android/material/bottomnavigation/BottomNavigationView;-><init>(Landroid/content/Context;Landroid/util/AttributeSet;II)V

    .line 104
    return-void
.end method

.method public constructor <init>(Landroid/content/Context;Landroid/util/AttributeSet;II)V
    .locals 7
    .param p1, "context"    # Landroid/content/Context;
    .param p2, "attrs"    # Landroid/util/AttributeSet;
    .param p3, "defStyleAttr"    # I
    .param p4, "defStyleRes"    # I

    .line 108
    invoke-direct {p0, p1, p2, p3, p4}, Lcom/google/android/material/navigation/NavigationBarView;-><init>(Landroid/content/Context;Landroid/util/AttributeSet;II)V

    .line 111
    invoke-virtual {p0}, Lcom/google/android/material/bottomnavigation/BottomNavigationView;->getContext()Landroid/content/Context;

    move-result-object p1

    .line 114
    sget-object v2, Lcom/google/android/material/R$styleable;->BottomNavigationView:[I

    const/4 v6, 0x0

    new-array v5, v6, [I

    .line 115
    move-object v0, p1

    move-object v1, p2

    move v3, p3

    move v4, p4

    invoke-static/range {v0 .. v5}, Lcom/google/android/material/internal/ThemeEnforcement;->obtainTintedStyledAttributes(Landroid/content/Context;Landroid/util/AttributeSet;[III[I)Landroidx/appcompat/widget/TintTypedArray;

    move-result-object v0

    .line 118
    .local v0, "attributes":Landroidx/appcompat/widget/TintTypedArray;
    sget v1, Lcom/google/android/material/R$styleable;->BottomNavigationView_itemHorizontalTranslationEnabled:I

    .line 119
    const/4 v2, 0x1

    invoke-virtual {v0, v1, v2}, Landroidx/appcompat/widget/TintTypedArray;->getBoolean(IZ)Z

    move-result v1

    .line 118
    invoke-virtual {p0, v1}, Lcom/google/android/material/bottomnavigation/BottomNavigationView;->setItemHorizontalTranslationEnabled(Z)V

    .line 122
    sget v1, Lcom/google/android/material/R$styleable;->BottomNavigationView_android_minHeight:I

    invoke-virtual {v0, v1}, Landroidx/appcompat/widget/TintTypedArray;->hasValue(I)Z

    move-result v1

    if-eqz v1, :cond_0

    .line 123
    sget v1, Lcom/google/android/material/R$styleable;->BottomNavigationView_android_minHeight:I

    .line 124
    invoke-virtual {v0, v1, v6}, Landroidx/appcompat/widget/TintTypedArray;->getDimensionPixelSize(II)I

    move-result v1

    .line 123
    invoke-virtual {p0, v1}, Lcom/google/android/material/bottomnavigation/BottomNavigationView;->setMinimumHeight(I)V

    .line 127
    :cond_0
    invoke-virtual {v0}, Landroidx/appcompat/widget/TintTypedArray;->recycle()V

    .line 129
    invoke-direct {p0}, Lcom/google/android/material/bottomnavigation/BottomNavigationView;->shouldDrawCompatibilityTopDivider()Z

    move-result v1

    if-eqz v1, :cond_1

    .line 130
    invoke-direct {p0, p1}, Lcom/google/android/material/bottomnavigation/BottomNavigationView;->addCompatibilityTopDivider(Landroid/content/Context;)V

    .line 133
    :cond_1
    invoke-direct {p0}, Lcom/google/android/material/bottomnavigation/BottomNavigationView;->applyWindowInsets()V

    .line 134
    return-void
.end method

.method private addCompatibilityTopDivider(Landroid/content/Context;)V
    .locals 4
    .param p1, "context"    # Landroid/content/Context;

    .line 232
    new-instance v0, Landroid/view/View;

    invoke-direct {v0, p1}, Landroid/view/View;-><init>(Landroid/content/Context;)V

    .line 233
    .local v0, "divider":Landroid/view/View;
    sget v1, Lcom/google/android/material/R$color;->design_bottom_navigation_shadow_color:I

    .line 234
    invoke-static {p1, v1}, Landroidx/core/content/ContextCompat;->getColor(Landroid/content/Context;I)I

    move-result v1

    .line 233
    invoke-virtual {v0, v1}, Landroid/view/View;->setBackgroundColor(I)V

    .line 235
    new-instance v1, Landroid/widget/FrameLayout$LayoutParams;

    .line 238
    invoke-virtual {p0}, Lcom/google/android/material/bottomnavigation/BottomNavigationView;->getResources()Landroid/content/res/Resources;

    move-result-object v2

    sget v3, Lcom/google/android/material/R$dimen;->design_bottom_navigation_shadow_height:I

    invoke-virtual {v2, v3}, Landroid/content/res/Resources;->getDimensionPixelSize(I)I

    move-result v2

    const/4 v3, -0x1

    invoke-direct {v1, v3, v2}, Landroid/widget/FrameLayout$LayoutParams;-><init>(II)V

    .line 239
    .local v1, "dividerParams":Landroid/widget/FrameLayout$LayoutParams;
    invoke-virtual {v0, v1}, Landroid/view/View;->setLayoutParams(Landroid/view/ViewGroup$LayoutParams;)V

    .line 240
    invoke-virtual {p0, v0}, Lcom/google/android/material/bottomnavigation/BottomNavigationView;->addView(Landroid/view/View;)V

    .line 241
    return-void
.end method

.method private applyWindowInsets()V
    .locals 1

    .line 137
    new-instance v0, Lcom/google/android/material/bottomnavigation/BottomNavigationView$1;

    invoke-direct {v0, p0}, Lcom/google/android/material/bottomnavigation/BottomNavigationView$1;-><init>(Lcom/google/android/material/bottomnavigation/BottomNavigationView;)V

    invoke-static {p0, v0}, Lcom/google/android/material/internal/ViewUtils;->doOnApplyWindowInsets(Landroid/view/View;Lcom/google/android/material/internal/ViewUtils$OnApplyWindowInsetsListener;)V

    .line 159
    return-void
.end method

.method private makeMinHeightSpec(I)I
    .locals 4
    .param p1, "measureSpec"    # I

    .line 168
    invoke-virtual {p0}, Lcom/google/android/material/bottomnavigation/BottomNavigationView;->getSuggestedMinimumHeight()I

    move-result v0

    .line 169
    .local v0, "minHeight":I
    invoke-static {p1}, Landroid/view/View$MeasureSpec;->getMode(I)I

    move-result v1

    const/high16 v2, 0x40000000    # 2.0f

    if-eq v1, v2, :cond_0

    if-lez v0, :cond_0

    .line 170
    invoke-virtual {p0}, Lcom/google/android/material/bottomnavigation/BottomNavigationView;->getPaddingTop()I

    move-result v1

    invoke-virtual {p0}, Lcom/google/android/material/bottomnavigation/BottomNavigationView;->getPaddingBottom()I

    move-result v3

    add-int/2addr v1, v3

    add-int/2addr v0, v1

    .line 172
    nop

    .line 173
    invoke-static {p1}, Landroid/view/View$MeasureSpec;->getSize(I)I

    move-result v1

    invoke-static {v1, v0}, Ljava/lang/Math;->min(II)I

    move-result v1

    .line 172
    invoke-static {v1, v2}, Landroid/view/View$MeasureSpec;->makeMeasureSpec(II)I

    move-result v1

    return v1

    .line 176
    :cond_0
    return p1
.end method

.method private shouldDrawCompatibilityTopDivider()Z
    .locals 2

    .line 223
    sget v0, Landroid/os/Build$VERSION;->SDK_INT:I

    const/16 v1, 0x15

    if-ge v0, v1, :cond_0

    invoke-virtual {p0}, Lcom/google/android/material/bottomnavigation/BottomNavigationView;->getBackground()Landroid/graphics/drawable/Drawable;

    move-result-object v0

    instance-of v0, v0, Lcom/google/android/material/shape/MaterialShapeDrawable;

    if-nez v0, :cond_0

    const/4 v0, 0x1

    goto :goto_0

    :cond_0
    const/4 v0, 0x0

    :goto_0
    return v0
.end method


# virtual methods
.method protected createNavigationBarMenuView(Landroid/content/Context;)Lcom/google/android/material/navigation/NavigationBarMenuView;
    .locals 1
    .param p1, "context"    # Landroid/content/Context;

    .line 215
    new-instance v0, Lcom/google/android/material/bottomnavigation/BottomNavigationMenuView;

    invoke-direct {v0, p1}, Lcom/google/android/material/bottomnavigation/BottomNavigationMenuView;-><init>(Landroid/content/Context;)V

    return-object v0
.end method

.method public getMaxItemCount()I
    .locals 1

    .line 207
    const/4 v0, 0x5

    return v0
.end method

.method public isItemHorizontalTranslationEnabled()Z
    .locals 1

    .line 202
    invoke-virtual {p0}, Lcom/google/android/material/bottomnavigation/BottomNavigationView;->getMenuView()Landroidx/appcompat/view/menu/MenuView;

    move-result-object v0

    check-cast v0, Lcom/google/android/material/bottomnavigation/BottomNavigationMenuView;

    invoke-virtual {v0}, Lcom/google/android/material/bottomnavigation/BottomNavigationMenuView;->isItemHorizontalTranslationEnabled()Z

    move-result v0

    return v0
.end method

.method protected onMeasure(II)V
    .locals 1
    .param p1, "widthMeasureSpec"    # I
    .param p2, "heightMeasureSpec"    # I

    .line 163
    invoke-direct {p0, p2}, Lcom/google/android/material/bottomnavigation/BottomNavigationView;->makeMinHeightSpec(I)I

    move-result v0

    .line 164
    .local v0, "minHeightSpec":I
    invoke-super {p0, p1, v0}, Lcom/google/android/material/navigation/NavigationBarView;->onMeasure(II)V

    .line 165
    return-void
.end method

.method public setItemHorizontalTranslationEnabled(Z)V
    .locals 3
    .param p1, "itemHorizontalTranslationEnabled"    # Z

    .line 187
    invoke-virtual {p0}, Lcom/google/android/material/bottomnavigation/BottomNavigationView;->getMenuView()Landroidx/appcompat/view/menu/MenuView;

    move-result-object v0

    check-cast v0, Lcom/google/android/material/bottomnavigation/BottomNavigationMenuView;

    .line 188
    .local v0, "menuView":Lcom/google/android/material/bottomnavigation/BottomNavigationMenuView;
    invoke-virtual {v0}, Lcom/google/android/material/bottomnavigation/BottomNavigationMenuView;->isItemHorizontalTranslationEnabled()Z

    move-result v1

    if-eq v1, p1, :cond_0

    .line 189
    invoke-virtual {v0, p1}, Lcom/google/android/material/bottomnavigation/BottomNavigationMenuView;->setItemHorizontalTranslationEnabled(Z)V

    .line 190
    invoke-virtual {p0}, Lcom/google/android/material/bottomnavigation/BottomNavigationView;->getPresenter()Lcom/google/android/material/navigation/NavigationBarPresenter;

    move-result-object v1

    const/4 v2, 0x0

    invoke-virtual {v1, v2}, Lcom/google/android/material/navigation/NavigationBarPresenter;->updateMenuView(Z)V

    .line 192
    :cond_0
    return-void
.end method

.method public setOnNavigationItemReselectedListener(Lcom/google/android/material/bottomnavigation/BottomNavigationView$OnNavigationItemReselectedListener;)V
    .locals 0
    .param p1, "listener"    # Lcom/google/android/material/bottomnavigation/BottomNavigationView$OnNavigationItemReselectedListener;
    .annotation runtime Ljava/lang/Deprecated;
    .end annotation

    .line 271
    invoke-virtual {p0, p1}, Lcom/google/android/material/bottomnavigation/BottomNavigationView;->setOnItemReselectedListener(Lcom/google/android/material/navigation/NavigationBarView$OnItemReselectedListener;)V

    .line 272
    return-void
.end method

.method public setOnNavigationItemSelectedListener(Lcom/google/android/material/bottomnavigation/BottomNavigationView$OnNavigationItemSelectedListener;)V
    .locals 0
    .param p1, "listener"    # Lcom/google/android/material/bottomnavigation/BottomNavigationView$OnNavigationItemSelectedListener;
    .annotation runtime Ljava/lang/Deprecated;
    .end annotation

    .line 256
    invoke-virtual {p0, p1}, Lcom/google/android/material/bottomnavigation/BottomNavigationView;->setOnItemSelectedListener(Lcom/google/android/material/navigation/NavigationBarView$OnItemSelectedListener;)V

    .line 257
    return-void
.end method
