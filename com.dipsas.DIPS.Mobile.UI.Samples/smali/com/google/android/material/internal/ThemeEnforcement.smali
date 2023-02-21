.class public final Lcom/google/android/material/internal/ThemeEnforcement;
.super Ljava/lang/Object;
.source "ThemeEnforcement.java"


# static fields
.field private static final APPCOMPAT_CHECK_ATTRS:[I

.field private static final APPCOMPAT_THEME_NAME:Ljava/lang/String; = "Theme.AppCompat"

.field private static final MATERIAL_CHECK_ATTRS:[I

.field private static final MATERIAL_THEME_NAME:Ljava/lang/String; = "Theme.MaterialComponents"


# direct methods
.method static constructor <clinit>()V
    .locals 4

    .line 43
    const/4 v0, 0x1

    new-array v1, v0, [I

    sget v2, Lcom/google/android/material/R$attr;->colorPrimary:I

    const/4 v3, 0x0

    aput v2, v1, v3

    sput-object v1, Lcom/google/android/material/internal/ThemeEnforcement;->APPCOMPAT_CHECK_ATTRS:[I

    .line 46
    new-array v0, v0, [I

    sget v1, Lcom/google/android/material/R$attr;->colorPrimaryVariant:I

    aput v1, v0, v3

    sput-object v0, Lcom/google/android/material/internal/ThemeEnforcement;->MATERIAL_CHECK_ATTRS:[I

    return-void
.end method

.method private constructor <init>()V
    .locals 0

    .line 49
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method

.method public static checkAppCompatTheme(Landroid/content/Context;)V
    .locals 2
    .param p0, "context"    # Landroid/content/Context;

    .line 211
    sget-object v0, Lcom/google/android/material/internal/ThemeEnforcement;->APPCOMPAT_CHECK_ATTRS:[I

    const-string v1, "Theme.AppCompat"

    invoke-static {p0, v0, v1}, Lcom/google/android/material/internal/ThemeEnforcement;->checkTheme(Landroid/content/Context;[ILjava/lang/String;)V

    .line 212
    return-void
.end method

.method private static checkCompatibleTheme(Landroid/content/Context;Landroid/util/AttributeSet;II)V
    .locals 6
    .param p0, "context"    # Landroid/content/Context;
    .param p1, "set"    # Landroid/util/AttributeSet;
    .param p2, "defStyleAttr"    # I
    .param p3, "defStyleRes"    # I

    .line 127
    sget-object v0, Lcom/google/android/material/R$styleable;->ThemeEnforcement:[I

    .line 128
    invoke-virtual {p0, p1, v0, p2, p3}, Landroid/content/Context;->obtainStyledAttributes(Landroid/util/AttributeSet;[III)Landroid/content/res/TypedArray;

    move-result-object v0

    .line 130
    .local v0, "a":Landroid/content/res/TypedArray;
    sget v1, Lcom/google/android/material/R$styleable;->ThemeEnforcement_enforceMaterialTheme:I

    .line 131
    const/4 v2, 0x0

    invoke-virtual {v0, v1, v2}, Landroid/content/res/TypedArray;->getBoolean(IZ)Z

    move-result v1

    .line 132
    .local v1, "enforceMaterialTheme":Z
    invoke-virtual {v0}, Landroid/content/res/TypedArray;->recycle()V

    .line 134
    if-eqz v1, :cond_1

    .line 135
    new-instance v2, Landroid/util/TypedValue;

    invoke-direct {v2}, Landroid/util/TypedValue;-><init>()V

    .line 136
    .local v2, "isMaterialTheme":Landroid/util/TypedValue;
    nop

    .line 137
    invoke-virtual {p0}, Landroid/content/Context;->getTheme()Landroid/content/res/Resources$Theme;

    move-result-object v3

    sget v4, Lcom/google/android/material/R$attr;->isMaterialTheme:I

    const/4 v5, 0x1

    invoke-virtual {v3, v4, v2, v5}, Landroid/content/res/Resources$Theme;->resolveAttribute(ILandroid/util/TypedValue;Z)Z

    move-result v3

    .line 139
    .local v3, "resolvedValue":Z
    if-eqz v3, :cond_0

    iget v4, v2, Landroid/util/TypedValue;->type:I

    const/16 v5, 0x12

    if-ne v4, v5, :cond_1

    iget v4, v2, Landroid/util/TypedValue;->data:I

    if-nez v4, :cond_1

    .line 143
    :cond_0
    invoke-static {p0}, Lcom/google/android/material/internal/ThemeEnforcement;->checkMaterialTheme(Landroid/content/Context;)V

    .line 146
    .end local v2    # "isMaterialTheme":Landroid/util/TypedValue;
    .end local v3    # "resolvedValue":Z
    :cond_1
    invoke-static {p0}, Lcom/google/android/material/internal/ThemeEnforcement;->checkAppCompatTheme(Landroid/content/Context;)V

    .line 147
    return-void
.end method

.method public static checkMaterialTheme(Landroid/content/Context;)V
    .locals 2
    .param p0, "context"    # Landroid/content/Context;

    .line 215
    sget-object v0, Lcom/google/android/material/internal/ThemeEnforcement;->MATERIAL_CHECK_ATTRS:[I

    const-string v1, "Theme.MaterialComponents"

    invoke-static {p0, v0, v1}, Lcom/google/android/material/internal/ThemeEnforcement;->checkTheme(Landroid/content/Context;[ILjava/lang/String;)V

    .line 216
    return-void
.end method

.method private static varargs checkTextAppearance(Landroid/content/Context;Landroid/util/AttributeSet;[III[I)V
    .locals 5
    .param p0, "context"    # Landroid/content/Context;
    .param p1, "set"    # Landroid/util/AttributeSet;
    .param p2, "attrs"    # [I
    .param p3, "defStyleAttr"    # I
    .param p4, "defStyleRes"    # I
    .param p5, "textAppearanceResIndices"    # [I

    .line 156
    sget-object v0, Lcom/google/android/material/R$styleable;->ThemeEnforcement:[I

    .line 157
    invoke-virtual {p0, p1, v0, p3, p4}, Landroid/content/Context;->obtainStyledAttributes(Landroid/util/AttributeSet;[III)Landroid/content/res/TypedArray;

    move-result-object v0

    .line 159
    .local v0, "themeEnforcementAttrs":Landroid/content/res/TypedArray;
    sget v1, Lcom/google/android/material/R$styleable;->ThemeEnforcement_enforceTextAppearance:I

    .line 160
    const/4 v2, 0x0

    invoke-virtual {v0, v1, v2}, Landroid/content/res/TypedArray;->getBoolean(IZ)Z

    move-result v1

    .line 162
    .local v1, "enforceTextAppearance":Z
    if-nez v1, :cond_0

    .line 163
    invoke-virtual {v0}, Landroid/content/res/TypedArray;->recycle()V

    .line 164
    return-void

    .line 169
    :cond_0
    if-eqz p5, :cond_2

    array-length v3, p5

    if-nez v3, :cond_1

    goto :goto_0

    .line 177
    :cond_1
    nop

    .line 178
    invoke-static/range {p0 .. p5}, Lcom/google/android/material/internal/ThemeEnforcement;->isCustomTextAppearanceValid(Landroid/content/Context;Landroid/util/AttributeSet;[III[I)Z

    move-result v2

    .local v2, "validTextAppearance":Z
    goto :goto_1

    .line 171
    .end local v2    # "validTextAppearance":Z
    :cond_2
    :goto_0
    sget v3, Lcom/google/android/material/R$styleable;->ThemeEnforcement_android_textAppearance:I

    .line 172
    const/4 v4, -0x1

    invoke-virtual {v0, v3, v4}, Landroid/content/res/TypedArray;->getResourceId(II)I

    move-result v3

    if-eq v3, v4, :cond_3

    const/4 v2, 0x1

    .line 182
    .restart local v2    # "validTextAppearance":Z
    :cond_3
    :goto_1
    invoke-virtual {v0}, Landroid/content/res/TypedArray;->recycle()V

    .line 184
    if-eqz v2, :cond_4

    .line 189
    return-void

    .line 185
    :cond_4
    new-instance v3, Ljava/lang/IllegalArgumentException;

    const-string v4, "This component requires that you specify a valid TextAppearance attribute. Update your app theme to inherit from Theme.MaterialComponents (or a descendant)."

    invoke-direct {v3, v4}, Ljava/lang/IllegalArgumentException;-><init>(Ljava/lang/String;)V

    throw v3
.end method

.method private static checkTheme(Landroid/content/Context;[ILjava/lang/String;)V
    .locals 3
    .param p0, "context"    # Landroid/content/Context;
    .param p1, "themeAttributes"    # [I
    .param p2, "themeName"    # Ljava/lang/String;

    .line 240
    invoke-static {p0, p1}, Lcom/google/android/material/internal/ThemeEnforcement;->isTheme(Landroid/content/Context;[I)Z

    move-result v0

    if-eqz v0, :cond_0

    .line 246
    return-void

    .line 241
    :cond_0
    new-instance v0, Ljava/lang/IllegalArgumentException;

    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "The style on this component requires your app theme to be "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v1

    invoke-virtual {v1, p2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v1

    const-string v2, " (or a descendant)."

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v1

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-direct {v0, v1}, Ljava/lang/IllegalArgumentException;-><init>(Ljava/lang/String;)V

    throw v0
.end method

.method public static isAppCompatTheme(Landroid/content/Context;)Z
    .locals 1
    .param p0, "context"    # Landroid/content/Context;

    .line 219
    sget-object v0, Lcom/google/android/material/internal/ThemeEnforcement;->APPCOMPAT_CHECK_ATTRS:[I

    invoke-static {p0, v0}, Lcom/google/android/material/internal/ThemeEnforcement;->isTheme(Landroid/content/Context;[I)Z

    move-result v0

    return v0
.end method

.method private static varargs isCustomTextAppearanceValid(Landroid/content/Context;Landroid/util/AttributeSet;[III[I)Z
    .locals 7
    .param p0, "context"    # Landroid/content/Context;
    .param p1, "set"    # Landroid/util/AttributeSet;
    .param p2, "attrs"    # [I
    .param p3, "defStyleAttr"    # I
    .param p4, "defStyleRes"    # I
    .param p5, "textAppearanceResIndices"    # [I

    .line 198
    nop

    .line 199
    invoke-virtual {p0, p1, p2, p3, p4}, Landroid/content/Context;->obtainStyledAttributes(Landroid/util/AttributeSet;[III)Landroid/content/res/TypedArray;

    move-result-object v0

    .line 200
    .local v0, "componentAttrs":Landroid/content/res/TypedArray;
    array-length v1, p5

    const/4 v2, 0x0

    const/4 v3, 0x0

    :goto_0
    if-ge v3, v1, :cond_1

    aget v4, p5, v3

    .line 201
    .local v4, "customTextAppearanceIndex":I
    const/4 v5, -0x1

    invoke-virtual {v0, v4, v5}, Landroid/content/res/TypedArray;->getResourceId(II)I

    move-result v6

    if-ne v6, v5, :cond_0

    .line 202
    invoke-virtual {v0}, Landroid/content/res/TypedArray;->recycle()V

    .line 203
    return v2

    .line 200
    .end local v4    # "customTextAppearanceIndex":I
    :cond_0
    add-int/lit8 v3, v3, 0x1

    goto :goto_0

    .line 206
    :cond_1
    invoke-virtual {v0}, Landroid/content/res/TypedArray;->recycle()V

    .line 207
    const/4 v1, 0x1

    return v1
.end method

.method public static isMaterialTheme(Landroid/content/Context;)Z
    .locals 1
    .param p0, "context"    # Landroid/content/Context;

    .line 223
    sget-object v0, Lcom/google/android/material/internal/ThemeEnforcement;->MATERIAL_CHECK_ATTRS:[I

    invoke-static {p0, v0}, Lcom/google/android/material/internal/ThemeEnforcement;->isTheme(Landroid/content/Context;[I)Z

    move-result v0

    return v0
.end method

.method private static isTheme(Landroid/content/Context;[I)Z
    .locals 3
    .param p0, "context"    # Landroid/content/Context;
    .param p1, "themeAttributes"    # [I

    .line 227
    invoke-virtual {p0, p1}, Landroid/content/Context;->obtainStyledAttributes([I)Landroid/content/res/TypedArray;

    move-result-object v0

    .line 228
    .local v0, "a":Landroid/content/res/TypedArray;
    const/4 v1, 0x0

    .local v1, "i":I
    :goto_0
    array-length v2, p1

    if-ge v1, v2, :cond_1

    .line 229
    invoke-virtual {v0, v1}, Landroid/content/res/TypedArray;->hasValue(I)Z

    move-result v2

    if-nez v2, :cond_0

    .line 230
    invoke-virtual {v0}, Landroid/content/res/TypedArray;->recycle()V

    .line 231
    const/4 v2, 0x0

    return v2

    .line 228
    :cond_0
    add-int/lit8 v1, v1, 0x1

    goto :goto_0

    .line 234
    .end local v1    # "i":I
    :cond_1
    invoke-virtual {v0}, Landroid/content/res/TypedArray;->recycle()V

    .line 235
    const/4 v1, 0x1

    return v1
.end method

.method public static varargs obtainStyledAttributes(Landroid/content/Context;Landroid/util/AttributeSet;[III[I)Landroid/content/res/TypedArray;
    .locals 1
    .param p0, "context"    # Landroid/content/Context;
    .param p1, "set"    # Landroid/util/AttributeSet;
    .param p2, "attrs"    # [I
    .param p3, "defStyleAttr"    # I
    .param p4, "defStyleRes"    # I
    .param p5, "textAppearanceResIndices"    # [I

    .line 75
    invoke-static {p0, p1, p3, p4}, Lcom/google/android/material/internal/ThemeEnforcement;->checkCompatibleTheme(Landroid/content/Context;Landroid/util/AttributeSet;II)V

    .line 78
    invoke-static/range {p0 .. p5}, Lcom/google/android/material/internal/ThemeEnforcement;->checkTextAppearance(Landroid/content/Context;Landroid/util/AttributeSet;[III[I)V

    .line 81
    invoke-virtual {p0, p1, p2, p3, p4}, Landroid/content/Context;->obtainStyledAttributes(Landroid/util/AttributeSet;[III)Landroid/content/res/TypedArray;

    move-result-object v0

    return-object v0
.end method

.method public static varargs obtainTintedStyledAttributes(Landroid/content/Context;Landroid/util/AttributeSet;[III[I)Landroidx/appcompat/widget/TintTypedArray;
    .locals 1
    .param p0, "context"    # Landroid/content/Context;
    .param p1, "set"    # Landroid/util/AttributeSet;
    .param p2, "attrs"    # [I
    .param p3, "defStyleAttr"    # I
    .param p4, "defStyleRes"    # I
    .param p5, "textAppearanceResIndices"    # [I

    .line 113
    invoke-static {p0, p1, p3, p4}, Lcom/google/android/material/internal/ThemeEnforcement;->checkCompatibleTheme(Landroid/content/Context;Landroid/util/AttributeSet;II)V

    .line 116
    invoke-static/range {p0 .. p5}, Lcom/google/android/material/internal/ThemeEnforcement;->checkTextAppearance(Landroid/content/Context;Landroid/util/AttributeSet;[III[I)V

    .line 119
    invoke-static {p0, p1, p2, p3, p4}, Landroidx/appcompat/widget/TintTypedArray;->obtainStyledAttributes(Landroid/content/Context;Landroid/util/AttributeSet;[III)Landroidx/appcompat/widget/TintTypedArray;

    move-result-object v0

    return-object v0
.end method
