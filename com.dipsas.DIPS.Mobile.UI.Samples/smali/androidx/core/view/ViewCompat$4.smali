.class Landroidx/core/view/ViewCompat$4;
.super Landroidx/core/view/ViewCompat$AccessibilityViewProperty;
.source "ViewCompat.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Landroidx/core/view/ViewCompat;->accessibilityHeadingProperty()Landroidx/core/view/ViewCompat$AccessibilityViewProperty;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation

.annotation system Ldalvik/annotation/Signature;
    value = {
        "Landroidx/core/view/ViewCompat$AccessibilityViewProperty<",
        "Ljava/lang/Boolean;",
        ">;"
    }
.end annotation


# direct methods
.method constructor <init>(ILjava/lang/Class;I)V
    .locals 0
    .param p1, "tagKey"    # I
    .param p3, "frameworkMinimumSdk"    # I

    .line 4382
    .local p2, "type":Ljava/lang/Class;, "Ljava/lang/Class<Ljava/lang/Boolean;>;"
    invoke-direct {p0, p1, p2, p3}, Landroidx/core/view/ViewCompat$AccessibilityViewProperty;-><init>(ILjava/lang/Class;I)V

    return-void
.end method


# virtual methods
.method frameworkGet(Landroid/view/View;)Ljava/lang/Boolean;
    .locals 1
    .param p1, "view"    # Landroid/view/View;

    .line 4387
    invoke-static {p1}, Landroidx/core/view/ViewCompat$Api28Impl;->isAccessibilityHeading(Landroid/view/View;)Z

    move-result v0

    invoke-static {v0}, Ljava/lang/Boolean;->valueOf(Z)Ljava/lang/Boolean;

    move-result-object v0

    return-object v0
.end method

.method bridge synthetic frameworkGet(Landroid/view/View;)Ljava/lang/Object;
    .locals 0

    .line 4382
    invoke-virtual {p0, p1}, Landroidx/core/view/ViewCompat$4;->frameworkGet(Landroid/view/View;)Ljava/lang/Boolean;

    move-result-object p1

    return-object p1
.end method

.method frameworkSet(Landroid/view/View;Ljava/lang/Boolean;)V
    .locals 1
    .param p1, "view"    # Landroid/view/View;
    .param p2, "value"    # Ljava/lang/Boolean;

    .line 4393
    invoke-virtual {p2}, Ljava/lang/Boolean;->booleanValue()Z

    move-result v0

    invoke-static {p1, v0}, Landroidx/core/view/ViewCompat$Api28Impl;->setAccessibilityHeading(Landroid/view/View;Z)V

    .line 4394
    return-void
.end method

.method bridge synthetic frameworkSet(Landroid/view/View;Ljava/lang/Object;)V
    .locals 0

    .line 4382
    check-cast p2, Ljava/lang/Boolean;

    invoke-virtual {p0, p1, p2}, Landroidx/core/view/ViewCompat$4;->frameworkSet(Landroid/view/View;Ljava/lang/Boolean;)V

    return-void
.end method

.method shouldUpdate(Ljava/lang/Boolean;Ljava/lang/Boolean;)Z
    .locals 1
    .param p1, "oldValue"    # Ljava/lang/Boolean;
    .param p2, "newValue"    # Ljava/lang/Boolean;

    .line 4398
    invoke-virtual {p0, p1, p2}, Landroidx/core/view/ViewCompat$4;->booleanNullToFalseEquals(Ljava/lang/Boolean;Ljava/lang/Boolean;)Z

    move-result v0

    xor-int/lit8 v0, v0, 0x1

    return v0
.end method

.method bridge synthetic shouldUpdate(Ljava/lang/Object;Ljava/lang/Object;)Z
    .locals 0

    .line 4382
    check-cast p1, Ljava/lang/Boolean;

    check-cast p2, Ljava/lang/Boolean;

    invoke-virtual {p0, p1, p2}, Landroidx/core/view/ViewCompat$4;->shouldUpdate(Ljava/lang/Boolean;Ljava/lang/Boolean;)Z

    move-result p1

    return p1
.end method
