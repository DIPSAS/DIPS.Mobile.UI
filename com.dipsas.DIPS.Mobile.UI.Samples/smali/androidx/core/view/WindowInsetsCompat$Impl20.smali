.class Landroidx/core/view/WindowInsetsCompat$Impl20;
.super Landroidx/core/view/WindowInsetsCompat$Impl;
.source "WindowInsetsCompat.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Landroidx/core/view/WindowInsetsCompat;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0xa
    name = "Impl20"
.end annotation


# static fields
.field private static sAttachInfoClass:Ljava/lang/Class;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/lang/Class<",
            "*>;"
        }
    .end annotation
.end field

.field private static sAttachInfoField:Ljava/lang/reflect/Field;

.field private static sGetViewRootImplMethod:Ljava/lang/reflect/Method;

.field private static sVisibleInsetsField:Ljava/lang/reflect/Field;

.field private static sVisibleRectReflectionFetched:Z


# instance fields
.field private mOverriddenInsets:[Landroidx/core/graphics/Insets;

.field final mPlatformInsets:Landroid/view/WindowInsets;

.field mRootViewVisibleInsets:Landroidx/core/graphics/Insets;

.field private mRootWindowInsets:Landroidx/core/view/WindowInsetsCompat;

.field private mSystemWindowInsets:Landroidx/core/graphics/Insets;


# direct methods
.method static constructor <clinit>()V
    .locals 1

    .line 880
    const/4 v0, 0x0

    sput-boolean v0, Landroidx/core/view/WindowInsetsCompat$Impl20;->sVisibleRectReflectionFetched:Z

    return-void
.end method

.method constructor <init>(Landroidx/core/view/WindowInsetsCompat;Landroid/view/WindowInsets;)V
    .locals 1
    .param p1, "host"    # Landroidx/core/view/WindowInsetsCompat;
    .param p2, "insets"    # Landroid/view/WindowInsets;

    .line 899
    invoke-direct {p0, p1}, Landroidx/core/view/WindowInsetsCompat$Impl;-><init>(Landroidx/core/view/WindowInsetsCompat;)V

    .line 893
    const/4 v0, 0x0

    iput-object v0, p0, Landroidx/core/view/WindowInsetsCompat$Impl20;->mSystemWindowInsets:Landroidx/core/graphics/Insets;

    .line 900
    iput-object p2, p0, Landroidx/core/view/WindowInsetsCompat$Impl20;->mPlatformInsets:Landroid/view/WindowInsets;

    .line 901
    return-void
.end method

.method constructor <init>(Landroidx/core/view/WindowInsetsCompat;Landroidx/core/view/WindowInsetsCompat$Impl20;)V
    .locals 2
    .param p1, "host"    # Landroidx/core/view/WindowInsetsCompat;
    .param p2, "other"    # Landroidx/core/view/WindowInsetsCompat$Impl20;

    .line 904
    new-instance v0, Landroid/view/WindowInsets;

    iget-object v1, p2, Landroidx/core/view/WindowInsetsCompat$Impl20;->mPlatformInsets:Landroid/view/WindowInsets;

    invoke-direct {v0, v1}, Landroid/view/WindowInsets;-><init>(Landroid/view/WindowInsets;)V

    invoke-direct {p0, p1, v0}, Landroidx/core/view/WindowInsetsCompat$Impl20;-><init>(Landroidx/core/view/WindowInsetsCompat;Landroid/view/WindowInsets;)V

    .line 905
    return-void
.end method

.method private getInsets(IZ)Landroidx/core/graphics/Insets;
    .locals 3
    .param p1, "typeMask"    # I
    .param p2, "ignoreVisibility"    # Z

    .line 941
    sget-object v0, Landroidx/core/graphics/Insets;->NONE:Landroidx/core/graphics/Insets;

    .line 942
    .local v0, "result":Landroidx/core/graphics/Insets;
    const/4 v1, 0x1

    .local v1, "i":I
    :goto_0
    const/16 v2, 0x100

    if-gt v1, v2, :cond_1

    .line 943
    and-int v2, p1, v1

    if-nez v2, :cond_0

    .line 944
    goto :goto_1

    .line 946
    :cond_0
    invoke-virtual {p0, v1, p2}, Landroidx/core/view/WindowInsetsCompat$Impl20;->getInsetsForType(IZ)Landroidx/core/graphics/Insets;

    move-result-object v2

    invoke-static {v0, v2}, Landroidx/core/graphics/Insets;->max(Landroidx/core/graphics/Insets;Landroidx/core/graphics/Insets;)Landroidx/core/graphics/Insets;

    move-result-object v0

    .line 942
    :goto_1
    shl-int/lit8 v1, v1, 0x1

    goto :goto_0

    .line 948
    .end local v1    # "i":I
    :cond_1
    return-object v0
.end method

.method private getRootStableInsets()Landroidx/core/graphics/Insets;
    .locals 1

    .line 1103
    iget-object v0, p0, Landroidx/core/view/WindowInsetsCompat$Impl20;->mRootWindowInsets:Landroidx/core/view/WindowInsetsCompat;

    if-eqz v0, :cond_0

    .line 1104
    invoke-virtual {v0}, Landroidx/core/view/WindowInsetsCompat;->getStableInsets()Landroidx/core/graphics/Insets;

    move-result-object v0

    return-object v0

    .line 1106
    :cond_0
    sget-object v0, Landroidx/core/graphics/Insets;->NONE:Landroidx/core/graphics/Insets;

    return-object v0
.end method

.method private getVisibleInsets(Landroid/view/View;)Landroidx/core/graphics/Insets;
    .locals 5
    .param p1, "rootView"    # Landroid/view/View;

    .line 1127
    const-string v0, "WindowInsetsCompat"

    sget v1, Landroid/os/Build$VERSION;->SDK_INT:I

    const/16 v2, 0x1e

    if-ge v1, v2, :cond_5

    .line 1131
    sget-boolean v1, Landroidx/core/view/WindowInsetsCompat$Impl20;->sVisibleRectReflectionFetched:Z

    if-nez v1, :cond_0

    .line 1132
    invoke-static {}, Landroidx/core/view/WindowInsetsCompat$Impl20;->loadReflectionField()V

    .line 1135
    :cond_0
    sget-object v1, Landroidx/core/view/WindowInsetsCompat$Impl20;->sGetViewRootImplMethod:Ljava/lang/reflect/Method;

    const/4 v2, 0x0

    if-eqz v1, :cond_4

    sget-object v3, Landroidx/core/view/WindowInsetsCompat$Impl20;->sAttachInfoClass:Ljava/lang/Class;

    if-eqz v3, :cond_4

    sget-object v3, Landroidx/core/view/WindowInsetsCompat$Impl20;->sVisibleInsetsField:Ljava/lang/reflect/Field;

    if-nez v3, :cond_1

    goto :goto_0

    .line 1142
    :cond_1
    const/4 v3, 0x0

    :try_start_0
    new-array v3, v3, [Ljava/lang/Object;

    invoke-virtual {v1, p1, v3}, Ljava/lang/reflect/Method;->invoke(Ljava/lang/Object;[Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v1

    .line 1143
    .local v1, "viewRootImpl":Ljava/lang/Object;
    if-nez v1, :cond_2

    .line 1144
    const-string v3, "Failed to get visible insets. getViewRootImpl() returned null from the provided view. This means that the view is either not attached or the method has been overridden"

    new-instance v4, Ljava/lang/NullPointerException;

    invoke-direct {v4}, Ljava/lang/NullPointerException;-><init>()V

    invoke-static {v0, v3, v4}, Landroid/util/Log;->w(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)I

    .line 1148
    return-object v2

    .line 1150
    :cond_2
    sget-object v3, Landroidx/core/view/WindowInsetsCompat$Impl20;->sAttachInfoField:Ljava/lang/reflect/Field;

    invoke-virtual {v3, v1}, Ljava/lang/reflect/Field;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v3

    .line 1151
    .local v3, "mAttachInfo":Ljava/lang/Object;
    sget-object v4, Landroidx/core/view/WindowInsetsCompat$Impl20;->sVisibleInsetsField:Ljava/lang/reflect/Field;

    invoke-virtual {v4, v3}, Ljava/lang/reflect/Field;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v4

    check-cast v4, Landroid/graphics/Rect;

    .line 1152
    .local v4, "visibleRect":Landroid/graphics/Rect;
    if-eqz v4, :cond_3

    invoke-static {v4}, Landroidx/core/graphics/Insets;->of(Landroid/graphics/Rect;)Landroidx/core/graphics/Insets;

    move-result-object v2
    :try_end_0
    .catch Ljava/lang/ReflectiveOperationException; {:try_start_0 .. :try_end_0} :catch_0

    :cond_3
    return-object v2

    .line 1154
    .end local v1    # "viewRootImpl":Ljava/lang/Object;
    .end local v3    # "mAttachInfo":Ljava/lang/Object;
    .end local v4    # "visibleRect":Landroid/graphics/Rect;
    :catch_0
    move-exception v1

    .line 1155
    .local v1, "e":Ljava/lang/ReflectiveOperationException;
    new-instance v3, Ljava/lang/StringBuilder;

    invoke-direct {v3}, Ljava/lang/StringBuilder;-><init>()V

    const-string v4, "Failed to get visible insets. (Reflection error). "

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v3

    .line 1156
    invoke-virtual {v1}, Ljava/lang/ReflectiveOperationException;->getMessage()Ljava/lang/String;

    move-result-object v4

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v3

    invoke-virtual {v3}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v3

    .line 1155
    invoke-static {v0, v3, v1}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)I

    .line 1160
    .end local v1    # "e":Ljava/lang/ReflectiveOperationException;
    return-object v2

    .line 1138
    :cond_4
    :goto_0
    return-object v2

    .line 1128
    :cond_5
    new-instance v0, Ljava/lang/UnsupportedOperationException;

    const-string v1, "getVisibleInsets() should not be called on API >= 30. Use WindowInsets.isVisible() instead."

    invoke-direct {v0, v1}, Ljava/lang/UnsupportedOperationException;-><init>(Ljava/lang/String;)V

    throw v0
.end method

.method private static loadReflectionField()V
    .locals 4

    .line 1172
    const/4 v0, 0x1

    :try_start_0
    const-class v1, Landroid/view/View;

    const-string v2, "getViewRootImpl"

    const/4 v3, 0x0

    new-array v3, v3, [Ljava/lang/Class;

    invoke-virtual {v1, v2, v3}, Ljava/lang/Class;->getDeclaredMethod(Ljava/lang/String;[Ljava/lang/Class;)Ljava/lang/reflect/Method;

    move-result-object v1

    sput-object v1, Landroidx/core/view/WindowInsetsCompat$Impl20;->sGetViewRootImplMethod:Ljava/lang/reflect/Method;

    .line 1173
    const-string v1, "android.view.View$AttachInfo"

    invoke-static {v1}, Ljava/lang/Class;->forName(Ljava/lang/String;)Ljava/lang/Class;

    move-result-object v1

    sput-object v1, Landroidx/core/view/WindowInsetsCompat$Impl20;->sAttachInfoClass:Ljava/lang/Class;

    .line 1174
    const-string v2, "mVisibleInsets"

    invoke-virtual {v1, v2}, Ljava/lang/Class;->getDeclaredField(Ljava/lang/String;)Ljava/lang/reflect/Field;

    move-result-object v1

    sput-object v1, Landroidx/core/view/WindowInsetsCompat$Impl20;->sVisibleInsetsField:Ljava/lang/reflect/Field;

    .line 1175
    const-string v1, "android.view.ViewRootImpl"

    invoke-static {v1}, Ljava/lang/Class;->forName(Ljava/lang/String;)Ljava/lang/Class;

    move-result-object v1

    .line 1176
    .local v1, "viewRootImplClass":Ljava/lang/Class;, "Ljava/lang/Class<*>;"
    const-string v2, "mAttachInfo"

    invoke-virtual {v1, v2}, Ljava/lang/Class;->getDeclaredField(Ljava/lang/String;)Ljava/lang/reflect/Field;

    move-result-object v2

    sput-object v2, Landroidx/core/view/WindowInsetsCompat$Impl20;->sAttachInfoField:Ljava/lang/reflect/Field;

    .line 1177
    sget-object v2, Landroidx/core/view/WindowInsetsCompat$Impl20;->sVisibleInsetsField:Ljava/lang/reflect/Field;

    invoke-virtual {v2, v0}, Ljava/lang/reflect/Field;->setAccessible(Z)V

    .line 1178
    sget-object v2, Landroidx/core/view/WindowInsetsCompat$Impl20;->sAttachInfoField:Ljava/lang/reflect/Field;

    invoke-virtual {v2, v0}, Ljava/lang/reflect/Field;->setAccessible(Z)V
    :try_end_0
    .catch Ljava/lang/ReflectiveOperationException; {:try_start_0 .. :try_end_0} :catch_0

    .line 1182
    .end local v1    # "viewRootImplClass":Ljava/lang/Class;, "Ljava/lang/Class<*>;"
    goto :goto_0

    .line 1179
    :catch_0
    move-exception v1

    .line 1180
    .local v1, "e":Ljava/lang/ReflectiveOperationException;
    new-instance v2, Ljava/lang/StringBuilder;

    invoke-direct {v2}, Ljava/lang/StringBuilder;-><init>()V

    const-string v3, "Failed to get visible insets. (Reflection error). "

    invoke-virtual {v2, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v2

    invoke-virtual {v1}, Ljava/lang/ReflectiveOperationException;->getMessage()Ljava/lang/String;

    move-result-object v3

    invoke-virtual {v2, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v2

    invoke-virtual {v2}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v2

    const-string v3, "WindowInsetsCompat"

    invoke-static {v3, v2, v1}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)I

    .line 1183
    .end local v1    # "e":Ljava/lang/ReflectiveOperationException;
    :goto_0
    sput-boolean v0, Landroidx/core/view/WindowInsetsCompat$Impl20;->sVisibleRectReflectionFetched:Z

    .line 1184
    return-void
.end method


# virtual methods
.method copyRootViewBounds(Landroid/view/View;)V
    .locals 1
    .param p1, "rootView"    # Landroid/view/View;

    .line 1112
    invoke-direct {p0, p1}, Landroidx/core/view/WindowInsetsCompat$Impl20;->getVisibleInsets(Landroid/view/View;)Landroidx/core/graphics/Insets;

    move-result-object v0

    .line 1113
    .local v0, "visibleInsets":Landroidx/core/graphics/Insets;
    if-nez v0, :cond_0

    .line 1114
    sget-object v0, Landroidx/core/graphics/Insets;->NONE:Landroidx/core/graphics/Insets;

    .line 1116
    :cond_0
    invoke-virtual {p0, v0}, Landroidx/core/view/WindowInsetsCompat$Impl20;->setRootViewData(Landroidx/core/graphics/Insets;)V

    .line 1117
    return-void
.end method

.method copyWindowDataInto(Landroidx/core/view/WindowInsetsCompat;)V
    .locals 1
    .param p1, "other"    # Landroidx/core/view/WindowInsetsCompat;

    .line 1087
    iget-object v0, p0, Landroidx/core/view/WindowInsetsCompat$Impl20;->mRootWindowInsets:Landroidx/core/view/WindowInsetsCompat;

    invoke-virtual {p1, v0}, Landroidx/core/view/WindowInsetsCompat;->setRootWindowInsets(Landroidx/core/view/WindowInsetsCompat;)V

    .line 1088
    iget-object v0, p0, Landroidx/core/view/WindowInsetsCompat$Impl20;->mRootViewVisibleInsets:Landroidx/core/graphics/Insets;

    invoke-virtual {p1, v0}, Landroidx/core/view/WindowInsetsCompat;->setRootViewData(Landroidx/core/graphics/Insets;)V

    .line 1089
    return-void
.end method

.method public equals(Ljava/lang/Object;)Z
    .locals 3
    .param p1, "o"    # Ljava/lang/Object;

    .line 1188
    invoke-super {p0, p1}, Landroidx/core/view/WindowInsetsCompat$Impl;->equals(Ljava/lang/Object;)Z

    move-result v0

    if-nez v0, :cond_0

    const/4 v0, 0x0

    return v0

    .line 1189
    :cond_0
    move-object v0, p1

    check-cast v0, Landroidx/core/view/WindowInsetsCompat$Impl20;

    .line 1190
    .local v0, "impl20":Landroidx/core/view/WindowInsetsCompat$Impl20;
    iget-object v1, p0, Landroidx/core/view/WindowInsetsCompat$Impl20;->mRootViewVisibleInsets:Landroidx/core/graphics/Insets;

    iget-object v2, v0, Landroidx/core/view/WindowInsetsCompat$Impl20;->mRootViewVisibleInsets:Landroidx/core/graphics/Insets;

    invoke-static {v1, v2}, Ljava/util/Objects;->equals(Ljava/lang/Object;Ljava/lang/Object;)Z

    move-result v1

    return v1
.end method

.method public getInsets(I)Landroidx/core/graphics/Insets;
    .locals 1
    .param p1, "typeMask"    # I

    .line 915
    const/4 v0, 0x0

    invoke-direct {p0, p1, v0}, Landroidx/core/view/WindowInsetsCompat$Impl20;->getInsets(IZ)Landroidx/core/graphics/Insets;

    move-result-object v0

    return-object v0
.end method

.method protected getInsetsForType(IZ)Landroidx/core/graphics/Insets;
    .locals 7
    .param p1, "type"    # I
    .param p2, "ignoreVisibility"    # Z

    .line 954
    const/4 v0, 0x0

    const/4 v1, 0x0

    sparse-switch p1, :sswitch_data_0

    .line 1043
    sget-object v0, Landroidx/core/graphics/Insets;->NONE:Landroidx/core/graphics/Insets;

    return-object v0

    .line 1032
    :sswitch_0
    iget-object v0, p0, Landroidx/core/view/WindowInsetsCompat$Impl20;->mRootWindowInsets:Landroidx/core/view/WindowInsetsCompat;

    if-eqz v0, :cond_0

    .line 1033
    invoke-virtual {v0}, Landroidx/core/view/WindowInsetsCompat;->getDisplayCutout()Landroidx/core/view/DisplayCutoutCompat;

    move-result-object v0

    goto :goto_0

    .line 1034
    :cond_0
    invoke-virtual {p0}, Landroidx/core/view/WindowInsetsCompat$Impl20;->getDisplayCutout()Landroidx/core/view/DisplayCutoutCompat;

    move-result-object v0

    :goto_0
    nop

    .line 1035
    .local v0, "cutout":Landroidx/core/view/DisplayCutoutCompat;
    if-eqz v0, :cond_1

    .line 1036
    invoke-virtual {v0}, Landroidx/core/view/DisplayCutoutCompat;->getSafeInsetLeft()I

    move-result v1

    invoke-virtual {v0}, Landroidx/core/view/DisplayCutoutCompat;->getSafeInsetTop()I

    move-result v2

    .line 1037
    invoke-virtual {v0}, Landroidx/core/view/DisplayCutoutCompat;->getSafeInsetRight()I

    move-result v3

    invoke-virtual {v0}, Landroidx/core/view/DisplayCutoutCompat;->getSafeInsetBottom()I

    move-result v4

    .line 1036
    invoke-static {v1, v2, v3, v4}, Landroidx/core/graphics/Insets;->of(IIII)Landroidx/core/graphics/Insets;

    move-result-object v1

    return-object v1

    .line 1039
    :cond_1
    sget-object v1, Landroidx/core/graphics/Insets;->NONE:Landroidx/core/graphics/Insets;

    return-object v1

    .line 1028
    .end local v0    # "cutout":Landroidx/core/view/DisplayCutoutCompat;
    :sswitch_1
    invoke-virtual {p0}, Landroidx/core/view/WindowInsetsCompat$Impl20;->getTappableElementInsets()Landroidx/core/graphics/Insets;

    move-result-object v0

    return-object v0

    .line 1024
    :sswitch_2
    invoke-virtual {p0}, Landroidx/core/view/WindowInsetsCompat$Impl20;->getMandatorySystemGestureInsets()Landroidx/core/graphics/Insets;

    move-result-object v0

    return-object v0

    .line 1020
    :sswitch_3
    invoke-virtual {p0}, Landroidx/core/view/WindowInsetsCompat$Impl20;->getSystemGestureInsets()Landroidx/core/graphics/Insets;

    move-result-object v0

    return-object v0

    .line 996
    :sswitch_4
    iget-object v2, p0, Landroidx/core/view/WindowInsetsCompat$Impl20;->mOverriddenInsets:[Landroidx/core/graphics/Insets;

    if-eqz v2, :cond_2

    .line 997
    const/16 v0, 0x8

    invoke-static {v0}, Landroidx/core/view/WindowInsetsCompat$Type;->indexOf(I)I

    move-result v0

    aget-object v0, v2, v0

    goto :goto_1

    :cond_2
    nop

    .line 998
    .local v0, "overriddenInsets":Landroidx/core/graphics/Insets;
    :goto_1
    if-eqz v0, :cond_3

    .line 999
    return-object v0

    .line 1001
    :cond_3
    invoke-virtual {p0}, Landroidx/core/view/WindowInsetsCompat$Impl20;->getSystemWindowInsets()Landroidx/core/graphics/Insets;

    move-result-object v2

    .line 1002
    .local v2, "systemWindow":Landroidx/core/graphics/Insets;
    invoke-direct {p0}, Landroidx/core/view/WindowInsetsCompat$Impl20;->getRootStableInsets()Landroidx/core/graphics/Insets;

    move-result-object v3

    .line 1004
    .local v3, "rootStable":Landroidx/core/graphics/Insets;
    iget v4, v2, Landroidx/core/graphics/Insets;->bottom:I

    iget v5, v3, Landroidx/core/graphics/Insets;->bottom:I

    if-le v4, v5, :cond_4

    .line 1007
    iget v4, v2, Landroidx/core/graphics/Insets;->bottom:I

    invoke-static {v1, v1, v1, v4}, Landroidx/core/graphics/Insets;->of(IIII)Landroidx/core/graphics/Insets;

    move-result-object v1

    return-object v1

    .line 1008
    :cond_4
    iget-object v4, p0, Landroidx/core/view/WindowInsetsCompat$Impl20;->mRootViewVisibleInsets:Landroidx/core/graphics/Insets;

    if-eqz v4, :cond_5

    sget-object v5, Landroidx/core/graphics/Insets;->NONE:Landroidx/core/graphics/Insets;

    .line 1009
    invoke-virtual {v4, v5}, Landroidx/core/graphics/Insets;->equals(Ljava/lang/Object;)Z

    move-result v4

    if-nez v4, :cond_5

    .line 1012
    iget-object v4, p0, Landroidx/core/view/WindowInsetsCompat$Impl20;->mRootViewVisibleInsets:Landroidx/core/graphics/Insets;

    iget v4, v4, Landroidx/core/graphics/Insets;->bottom:I

    iget v5, v3, Landroidx/core/graphics/Insets;->bottom:I

    if-le v4, v5, :cond_5

    .line 1013
    iget-object v4, p0, Landroidx/core/view/WindowInsetsCompat$Impl20;->mRootViewVisibleInsets:Landroidx/core/graphics/Insets;

    iget v4, v4, Landroidx/core/graphics/Insets;->bottom:I

    invoke-static {v1, v1, v1, v4}, Landroidx/core/graphics/Insets;->of(IIII)Landroidx/core/graphics/Insets;

    move-result-object v1

    return-object v1

    .line 1016
    :cond_5
    sget-object v1, Landroidx/core/graphics/Insets;->NONE:Landroidx/core/graphics/Insets;

    return-object v1

    .line 965
    .end local v0    # "overriddenInsets":Landroidx/core/graphics/Insets;
    .end local v2    # "systemWindow":Landroidx/core/graphics/Insets;
    .end local v3    # "rootStable":Landroidx/core/graphics/Insets;
    :sswitch_5
    if-eqz p2, :cond_6

    .line 966
    invoke-direct {p0}, Landroidx/core/view/WindowInsetsCompat$Impl20;->getRootStableInsets()Landroidx/core/graphics/Insets;

    move-result-object v0

    .line 967
    .local v0, "rootStable":Landroidx/core/graphics/Insets;
    invoke-virtual {p0}, Landroidx/core/view/WindowInsetsCompat$Impl20;->getStableInsets()Landroidx/core/graphics/Insets;

    move-result-object v2

    .line 968
    .local v2, "stable":Landroidx/core/graphics/Insets;
    iget v3, v0, Landroidx/core/graphics/Insets;->left:I

    iget v4, v2, Landroidx/core/graphics/Insets;->left:I

    .line 969
    invoke-static {v3, v4}, Ljava/lang/Math;->max(II)I

    move-result v3

    iget v4, v0, Landroidx/core/graphics/Insets;->right:I

    iget v5, v2, Landroidx/core/graphics/Insets;->right:I

    .line 971
    invoke-static {v4, v5}, Ljava/lang/Math;->max(II)I

    move-result v4

    iget v5, v0, Landroidx/core/graphics/Insets;->bottom:I

    iget v6, v2, Landroidx/core/graphics/Insets;->bottom:I

    .line 972
    invoke-static {v5, v6}, Ljava/lang/Math;->max(II)I

    move-result v5

    .line 968
    invoke-static {v3, v1, v4, v5}, Landroidx/core/graphics/Insets;->of(IIII)Landroidx/core/graphics/Insets;

    move-result-object v1

    return-object v1

    .line 975
    .end local v0    # "rootStable":Landroidx/core/graphics/Insets;
    .end local v2    # "stable":Landroidx/core/graphics/Insets;
    :cond_6
    invoke-virtual {p0}, Landroidx/core/view/WindowInsetsCompat$Impl20;->getSystemWindowInsets()Landroidx/core/graphics/Insets;

    move-result-object v2

    .line 976
    .local v2, "systemWindow":Landroidx/core/graphics/Insets;
    iget-object v3, p0, Landroidx/core/view/WindowInsetsCompat$Impl20;->mRootWindowInsets:Landroidx/core/view/WindowInsetsCompat;

    if-eqz v3, :cond_7

    .line 977
    invoke-virtual {v3}, Landroidx/core/view/WindowInsetsCompat;->getStableInsets()Landroidx/core/graphics/Insets;

    move-result-object v0

    goto :goto_2

    .line 978
    :cond_7
    nop

    :goto_2
    nop

    .line 980
    .restart local v0    # "rootStable":Landroidx/core/graphics/Insets;
    iget v3, v2, Landroidx/core/graphics/Insets;->bottom:I

    .line 981
    .local v3, "bottom":I
    if-eqz v0, :cond_8

    .line 985
    iget v4, v0, Landroidx/core/graphics/Insets;->bottom:I

    invoke-static {v3, v4}, Ljava/lang/Math;->min(II)I

    move-result v3

    .line 987
    :cond_8
    iget v4, v2, Landroidx/core/graphics/Insets;->left:I

    iget v5, v2, Landroidx/core/graphics/Insets;->right:I

    invoke-static {v4, v1, v5, v3}, Landroidx/core/graphics/Insets;->of(IIII)Landroidx/core/graphics/Insets;

    move-result-object v1

    return-object v1

    .line 956
    .end local v0    # "rootStable":Landroidx/core/graphics/Insets;
    .end local v2    # "systemWindow":Landroidx/core/graphics/Insets;
    .end local v3    # "bottom":I
    :sswitch_6
    if-eqz p2, :cond_9

    .line 957
    invoke-direct {p0}, Landroidx/core/view/WindowInsetsCompat$Impl20;->getRootStableInsets()Landroidx/core/graphics/Insets;

    move-result-object v0

    .line 958
    .restart local v0    # "rootStable":Landroidx/core/graphics/Insets;
    iget v2, v0, Landroidx/core/graphics/Insets;->top:I

    .line 959
    invoke-virtual {p0}, Landroidx/core/view/WindowInsetsCompat$Impl20;->getSystemWindowInsets()Landroidx/core/graphics/Insets;

    move-result-object v3

    iget v3, v3, Landroidx/core/graphics/Insets;->top:I

    invoke-static {v2, v3}, Ljava/lang/Math;->max(II)I

    move-result v2

    .line 958
    invoke-static {v1, v2, v1, v1}, Landroidx/core/graphics/Insets;->of(IIII)Landroidx/core/graphics/Insets;

    move-result-object v1

    return-object v1

    .line 961
    .end local v0    # "rootStable":Landroidx/core/graphics/Insets;
    :cond_9
    invoke-virtual {p0}, Landroidx/core/view/WindowInsetsCompat$Impl20;->getSystemWindowInsets()Landroidx/core/graphics/Insets;

    move-result-object v0

    iget v0, v0, Landroidx/core/graphics/Insets;->top:I

    invoke-static {v1, v0, v1, v1}, Landroidx/core/graphics/Insets;->of(IIII)Landroidx/core/graphics/Insets;

    move-result-object v0

    return-object v0

    :sswitch_data_0
    .sparse-switch
        0x1 -> :sswitch_6
        0x2 -> :sswitch_5
        0x8 -> :sswitch_4
        0x10 -> :sswitch_3
        0x20 -> :sswitch_2
        0x40 -> :sswitch_1
        0x80 -> :sswitch_0
    .end sparse-switch
.end method

.method public getInsetsIgnoringVisibility(I)Landroidx/core/graphics/Insets;
    .locals 1
    .param p1, "typeMask"    # I

    .line 921
    const/4 v0, 0x1

    invoke-direct {p0, p1, v0}, Landroidx/core/view/WindowInsetsCompat$Impl20;->getInsets(IZ)Landroidx/core/graphics/Insets;

    move-result-object v0

    return-object v0
.end method

.method final getSystemWindowInsets()Landroidx/core/graphics/Insets;
    .locals 4

    .line 1065
    iget-object v0, p0, Landroidx/core/view/WindowInsetsCompat$Impl20;->mSystemWindowInsets:Landroidx/core/graphics/Insets;

    if-nez v0, :cond_0

    .line 1066
    iget-object v0, p0, Landroidx/core/view/WindowInsetsCompat$Impl20;->mPlatformInsets:Landroid/view/WindowInsets;

    .line 1067
    invoke-virtual {v0}, Landroid/view/WindowInsets;->getSystemWindowInsetLeft()I

    move-result v0

    iget-object v1, p0, Landroidx/core/view/WindowInsetsCompat$Impl20;->mPlatformInsets:Landroid/view/WindowInsets;

    .line 1068
    invoke-virtual {v1}, Landroid/view/WindowInsets;->getSystemWindowInsetTop()I

    move-result v1

    iget-object v2, p0, Landroidx/core/view/WindowInsetsCompat$Impl20;->mPlatformInsets:Landroid/view/WindowInsets;

    .line 1069
    invoke-virtual {v2}, Landroid/view/WindowInsets;->getSystemWindowInsetRight()I

    move-result v2

    iget-object v3, p0, Landroidx/core/view/WindowInsetsCompat$Impl20;->mPlatformInsets:Landroid/view/WindowInsets;

    .line 1070
    invoke-virtual {v3}, Landroid/view/WindowInsets;->getSystemWindowInsetBottom()I

    move-result v3

    .line 1066
    invoke-static {v0, v1, v2, v3}, Landroidx/core/graphics/Insets;->of(IIII)Landroidx/core/graphics/Insets;

    move-result-object v0

    iput-object v0, p0, Landroidx/core/view/WindowInsetsCompat$Impl20;->mSystemWindowInsets:Landroidx/core/graphics/Insets;

    .line 1072
    :cond_0
    iget-object v0, p0, Landroidx/core/view/WindowInsetsCompat$Impl20;->mSystemWindowInsets:Landroidx/core/graphics/Insets;

    return-object v0
.end method

.method inset(IIII)Landroidx/core/view/WindowInsetsCompat;
    .locals 2
    .param p1, "left"    # I
    .param p2, "top"    # I
    .param p3, "right"    # I
    .param p4, "bottom"    # I

    .line 1079
    new-instance v0, Landroidx/core/view/WindowInsetsCompat$Builder;

    iget-object v1, p0, Landroidx/core/view/WindowInsetsCompat$Impl20;->mPlatformInsets:Landroid/view/WindowInsets;

    invoke-static {v1}, Landroidx/core/view/WindowInsetsCompat;->toWindowInsetsCompat(Landroid/view/WindowInsets;)Landroidx/core/view/WindowInsetsCompat;

    move-result-object v1

    invoke-direct {v0, v1}, Landroidx/core/view/WindowInsetsCompat$Builder;-><init>(Landroidx/core/view/WindowInsetsCompat;)V

    .line 1080
    .local v0, "b":Landroidx/core/view/WindowInsetsCompat$Builder;
    invoke-virtual {p0}, Landroidx/core/view/WindowInsetsCompat$Impl20;->getSystemWindowInsets()Landroidx/core/graphics/Insets;

    move-result-object v1

    invoke-static {v1, p1, p2, p3, p4}, Landroidx/core/view/WindowInsetsCompat;->insetInsets(Landroidx/core/graphics/Insets;IIII)Landroidx/core/graphics/Insets;

    move-result-object v1

    invoke-virtual {v0, v1}, Landroidx/core/view/WindowInsetsCompat$Builder;->setSystemWindowInsets(Landroidx/core/graphics/Insets;)Landroidx/core/view/WindowInsetsCompat$Builder;

    .line 1081
    invoke-virtual {p0}, Landroidx/core/view/WindowInsetsCompat$Impl20;->getStableInsets()Landroidx/core/graphics/Insets;

    move-result-object v1

    invoke-static {v1, p1, p2, p3, p4}, Landroidx/core/view/WindowInsetsCompat;->insetInsets(Landroidx/core/graphics/Insets;IIII)Landroidx/core/graphics/Insets;

    move-result-object v1

    invoke-virtual {v0, v1}, Landroidx/core/view/WindowInsetsCompat$Builder;->setStableInsets(Landroidx/core/graphics/Insets;)Landroidx/core/view/WindowInsetsCompat$Builder;

    .line 1082
    invoke-virtual {v0}, Landroidx/core/view/WindowInsetsCompat$Builder;->build()Landroidx/core/view/WindowInsetsCompat;

    move-result-object v1

    return-object v1
.end method

.method isRound()Z
    .locals 1

    .line 909
    iget-object v0, p0, Landroidx/core/view/WindowInsetsCompat$Impl20;->mPlatformInsets:Landroid/view/WindowInsets;

    invoke-virtual {v0}, Landroid/view/WindowInsets;->isRound()Z

    move-result v0

    return v0
.end method

.method protected isTypeVisible(I)Z
    .locals 3
    .param p1, "type"    # I

    .line 1048
    const/4 v0, 0x1

    const/4 v1, 0x0

    sparse-switch p1, :sswitch_data_0

    .line 1058
    return v0

    .line 1056
    :sswitch_0
    return v1

    .line 1053
    :sswitch_1
    invoke-virtual {p0, p1, v1}, Landroidx/core/view/WindowInsetsCompat$Impl20;->getInsetsForType(IZ)Landroidx/core/graphics/Insets;

    move-result-object v1

    sget-object v2, Landroidx/core/graphics/Insets;->NONE:Landroidx/core/graphics/Insets;

    invoke-virtual {v1, v2}, Landroidx/core/graphics/Insets;->equals(Ljava/lang/Object;)Z

    move-result v1

    xor-int/2addr v0, v1

    return v0

    nop

    :sswitch_data_0
    .sparse-switch
        0x1 -> :sswitch_1
        0x2 -> :sswitch_1
        0x4 -> :sswitch_0
        0x8 -> :sswitch_1
        0x80 -> :sswitch_1
    .end sparse-switch
.end method

.method isVisible(I)Z
    .locals 2
    .param p1, "typeMask"    # I

    .line 927
    const/4 v0, 0x1

    .local v0, "i":I
    :goto_0
    const/16 v1, 0x100

    if-gt v0, v1, :cond_2

    .line 928
    and-int v1, p1, v0

    if-nez v1, :cond_0

    .line 929
    goto :goto_1

    .line 931
    :cond_0
    invoke-virtual {p0, v0}, Landroidx/core/view/WindowInsetsCompat$Impl20;->isTypeVisible(I)Z

    move-result v1

    if-nez v1, :cond_1

    .line 932
    const/4 v1, 0x0

    return v1

    .line 927
    :cond_1
    :goto_1
    shl-int/lit8 v0, v0, 0x1

    goto :goto_0

    .line 935
    .end local v0    # "i":I
    :cond_2
    const/4 v0, 0x1

    return v0
.end method

.method public setOverriddenInsets([Landroidx/core/graphics/Insets;)V
    .locals 0
    .param p1, "insetsTypeMask"    # [Landroidx/core/graphics/Insets;

    .line 1165
    iput-object p1, p0, Landroidx/core/view/WindowInsetsCompat$Impl20;->mOverriddenInsets:[Landroidx/core/graphics/Insets;

    .line 1166
    return-void
.end method

.method setRootViewData(Landroidx/core/graphics/Insets;)V
    .locals 0
    .param p1, "visibleInsets"    # Landroidx/core/graphics/Insets;

    .line 1098
    iput-object p1, p0, Landroidx/core/view/WindowInsetsCompat$Impl20;->mRootViewVisibleInsets:Landroidx/core/graphics/Insets;

    .line 1099
    return-void
.end method

.method setRootWindowInsets(Landroidx/core/view/WindowInsetsCompat;)V
    .locals 0
    .param p1, "rootWindowInsets"    # Landroidx/core/view/WindowInsetsCompat;

    .line 1093
    iput-object p1, p0, Landroidx/core/view/WindowInsetsCompat$Impl20;->mRootWindowInsets:Landroidx/core/view/WindowInsetsCompat;

    .line 1094
    return-void
.end method
