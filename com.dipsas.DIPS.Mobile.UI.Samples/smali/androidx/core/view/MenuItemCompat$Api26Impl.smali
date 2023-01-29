.class Landroidx/core/view/MenuItemCompat$Api26Impl;
.super Ljava/lang/Object;
.source "MenuItemCompat.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Landroidx/core/view/MenuItemCompat;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x8
    name = "Api26Impl"
.end annotation


# direct methods
.method private constructor <init>()V
    .locals 0

    .line 598
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 600
    return-void
.end method

.method static getAlphabeticModifiers(Landroid/view/MenuItem;)I
    .locals 1
    .param p0, "menuItem"    # Landroid/view/MenuItem;

    .line 647
    invoke-interface {p0}, Landroid/view/MenuItem;->getAlphabeticModifiers()I

    move-result v0

    return v0
.end method

.method static getContentDescription(Landroid/view/MenuItem;)Ljava/lang/CharSequence;
    .locals 1
    .param p0, "menuItem"    # Landroid/view/MenuItem;

    .line 609
    invoke-interface {p0}, Landroid/view/MenuItem;->getContentDescription()Ljava/lang/CharSequence;

    move-result-object v0

    return-object v0
.end method

.method static getIconTintList(Landroid/view/MenuItem;)Landroid/content/res/ColorStateList;
    .locals 1
    .param p0, "menuItem"    # Landroid/view/MenuItem;

    .line 657
    invoke-interface {p0}, Landroid/view/MenuItem;->getIconTintList()Landroid/content/res/ColorStateList;

    move-result-object v0

    return-object v0
.end method

.method static getIconTintMode(Landroid/view/MenuItem;)Landroid/graphics/PorterDuff$Mode;
    .locals 1
    .param p0, "menuItem"    # Landroid/view/MenuItem;

    .line 667
    invoke-interface {p0}, Landroid/view/MenuItem;->getIconTintMode()Landroid/graphics/PorterDuff$Mode;

    move-result-object v0

    return-object v0
.end method

.method static getNumericModifiers(Landroid/view/MenuItem;)I
    .locals 1
    .param p0, "menuItem"    # Landroid/view/MenuItem;

    .line 636
    invoke-interface {p0}, Landroid/view/MenuItem;->getNumericModifiers()I

    move-result v0

    return v0
.end method

.method static getTooltipText(Landroid/view/MenuItem;)Ljava/lang/CharSequence;
    .locals 1
    .param p0, "menuItem"    # Landroid/view/MenuItem;

    .line 619
    invoke-interface {p0}, Landroid/view/MenuItem;->getTooltipText()Ljava/lang/CharSequence;

    move-result-object v0

    return-object v0
.end method

.method static setAlphabeticShortcut(Landroid/view/MenuItem;CI)Landroid/view/MenuItem;
    .locals 1
    .param p0, "menuItem"    # Landroid/view/MenuItem;
    .param p1, "alphaChar"    # C
    .param p2, "alphaModifiers"    # I

    .line 642
    invoke-interface {p0, p1, p2}, Landroid/view/MenuItem;->setAlphabeticShortcut(CI)Landroid/view/MenuItem;

    move-result-object v0

    return-object v0
.end method

.method static setContentDescription(Landroid/view/MenuItem;Ljava/lang/CharSequence;)Landroid/view/MenuItem;
    .locals 1
    .param p0, "menuItem"    # Landroid/view/MenuItem;
    .param p1, "contentDescription"    # Ljava/lang/CharSequence;

    .line 604
    invoke-interface {p0, p1}, Landroid/view/MenuItem;->setContentDescription(Ljava/lang/CharSequence;)Landroid/view/MenuItem;

    move-result-object v0

    return-object v0
.end method

.method static setIconTintList(Landroid/view/MenuItem;Landroid/content/res/ColorStateList;)Landroid/view/MenuItem;
    .locals 1
    .param p0, "menuItem"    # Landroid/view/MenuItem;
    .param p1, "tint"    # Landroid/content/res/ColorStateList;

    .line 652
    invoke-interface {p0, p1}, Landroid/view/MenuItem;->setIconTintList(Landroid/content/res/ColorStateList;)Landroid/view/MenuItem;

    move-result-object v0

    return-object v0
.end method

.method static setIconTintMode(Landroid/view/MenuItem;Landroid/graphics/PorterDuff$Mode;)Landroid/view/MenuItem;
    .locals 1
    .param p0, "menuItem"    # Landroid/view/MenuItem;
    .param p1, "tintMode"    # Landroid/graphics/PorterDuff$Mode;

    .line 662
    invoke-interface {p0, p1}, Landroid/view/MenuItem;->setIconTintMode(Landroid/graphics/PorterDuff$Mode;)Landroid/view/MenuItem;

    move-result-object v0

    return-object v0
.end method

.method static setNumericShortcut(Landroid/view/MenuItem;CI)Landroid/view/MenuItem;
    .locals 1
    .param p0, "menuItem"    # Landroid/view/MenuItem;
    .param p1, "numericChar"    # C
    .param p2, "numericModifiers"    # I

    .line 631
    invoke-interface {p0, p1, p2}, Landroid/view/MenuItem;->setNumericShortcut(CI)Landroid/view/MenuItem;

    move-result-object v0

    return-object v0
.end method

.method static setShortcut(Landroid/view/MenuItem;CCII)Landroid/view/MenuItem;
    .locals 1
    .param p0, "menuItem"    # Landroid/view/MenuItem;
    .param p1, "numericChar"    # C
    .param p2, "alphaChar"    # C
    .param p3, "numericModifiers"    # I
    .param p4, "alphaModifiers"    # I

    .line 625
    invoke-interface {p0, p1, p2, p3, p4}, Landroid/view/MenuItem;->setShortcut(CCII)Landroid/view/MenuItem;

    move-result-object v0

    return-object v0
.end method

.method static setTooltipText(Landroid/view/MenuItem;Ljava/lang/CharSequence;)Landroid/view/MenuItem;
    .locals 1
    .param p0, "menuItem"    # Landroid/view/MenuItem;
    .param p1, "tooltipText"    # Ljava/lang/CharSequence;

    .line 614
    invoke-interface {p0, p1}, Landroid/view/MenuItem;->setTooltipText(Ljava/lang/CharSequence;)Landroid/view/MenuItem;

    move-result-object v0

    return-object v0
.end method
