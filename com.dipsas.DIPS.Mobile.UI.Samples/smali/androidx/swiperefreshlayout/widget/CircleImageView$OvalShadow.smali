.class Landroidx/swiperefreshlayout/widget/CircleImageView$OvalShadow;
.super Landroid/graphics/drawable/shapes/OvalShape;
.source "CircleImageView.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Landroidx/swiperefreshlayout/widget/CircleImageView;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0xa
    name = "OvalShadow"
.end annotation


# instance fields
.field private mCircleImageView:Landroidx/swiperefreshlayout/widget/CircleImageView;

.field private mShadowPaint:Landroid/graphics/Paint;

.field private mShadowRadius:I


# direct methods
.method constructor <init>(Landroidx/swiperefreshlayout/widget/CircleImageView;I)V
    .locals 1
    .param p1, "circleImageView"    # Landroidx/swiperefreshlayout/widget/CircleImageView;
    .param p2, "shadowRadius"    # I

    .line 143
    invoke-direct {p0}, Landroid/graphics/drawable/shapes/OvalShape;-><init>()V

    .line 144
    iput-object p1, p0, Landroidx/swiperefreshlayout/widget/CircleImageView$OvalShadow;->mCircleImageView:Landroidx/swiperefreshlayout/widget/CircleImageView;

    .line 145
    new-instance v0, Landroid/graphics/Paint;

    invoke-direct {v0}, Landroid/graphics/Paint;-><init>()V

    iput-object v0, p0, Landroidx/swiperefreshlayout/widget/CircleImageView$OvalShadow;->mShadowPaint:Landroid/graphics/Paint;

    .line 146
    iput p2, p0, Landroidx/swiperefreshlayout/widget/CircleImageView$OvalShadow;->mShadowRadius:I

    .line 147
    invoke-virtual {p0}, Landroidx/swiperefreshlayout/widget/CircleImageView$OvalShadow;->rect()Landroid/graphics/RectF;

    move-result-object v0

    invoke-virtual {v0}, Landroid/graphics/RectF;->width()F

    move-result v0

    float-to-int v0, v0

    invoke-direct {p0, v0}, Landroidx/swiperefreshlayout/widget/CircleImageView$OvalShadow;->updateRadialGradient(I)V

    .line 148
    return-void
.end method

.method private updateRadialGradient(I)V
    .locals 9
    .param p1, "diameter"    # I

    .line 165
    iget-object v0, p0, Landroidx/swiperefreshlayout/widget/CircleImageView$OvalShadow;->mShadowPaint:Landroid/graphics/Paint;

    new-instance v8, Landroid/graphics/RadialGradient;

    div-int/lit8 v1, p1, 0x2

    int-to-float v2, v1

    div-int/lit8 v1, p1, 0x2

    int-to-float v3, v1

    iget v1, p0, Landroidx/swiperefreshlayout/widget/CircleImageView$OvalShadow;->mShadowRadius:I

    int-to-float v4, v1

    const/4 v1, 0x2

    new-array v5, v1, [I

    fill-array-data v5, :array_0

    sget-object v7, Landroid/graphics/Shader$TileMode;->CLAMP:Landroid/graphics/Shader$TileMode;

    const/4 v6, 0x0

    move-object v1, v8

    invoke-direct/range {v1 .. v7}, Landroid/graphics/RadialGradient;-><init>(FFF[I[FLandroid/graphics/Shader$TileMode;)V

    invoke-virtual {v0, v8}, Landroid/graphics/Paint;->setShader(Landroid/graphics/Shader;)Landroid/graphics/Shader;

    .line 172
    return-void

    :array_0
    .array-data 4
        0x3d000000    # 0.03125f
        0x0
    .end array-data
.end method


# virtual methods
.method public draw(Landroid/graphics/Canvas;Landroid/graphics/Paint;)V
    .locals 6
    .param p1, "canvas"    # Landroid/graphics/Canvas;
    .param p2, "paint"    # Landroid/graphics/Paint;

    .line 158
    iget-object v0, p0, Landroidx/swiperefreshlayout/widget/CircleImageView$OvalShadow;->mCircleImageView:Landroidx/swiperefreshlayout/widget/CircleImageView;

    invoke-virtual {v0}, Landroidx/swiperefreshlayout/widget/CircleImageView;->getWidth()I

    move-result v0

    div-int/lit8 v0, v0, 0x2

    .line 159
    .local v0, "x":I
    iget-object v1, p0, Landroidx/swiperefreshlayout/widget/CircleImageView$OvalShadow;->mCircleImageView:Landroidx/swiperefreshlayout/widget/CircleImageView;

    invoke-virtual {v1}, Landroidx/swiperefreshlayout/widget/CircleImageView;->getHeight()I

    move-result v1

    div-int/lit8 v1, v1, 0x2

    .line 160
    .local v1, "y":I
    int-to-float v2, v0

    int-to-float v3, v1

    int-to-float v4, v0

    iget-object v5, p0, Landroidx/swiperefreshlayout/widget/CircleImageView$OvalShadow;->mShadowPaint:Landroid/graphics/Paint;

    invoke-virtual {p1, v2, v3, v4, v5}, Landroid/graphics/Canvas;->drawCircle(FFFLandroid/graphics/Paint;)V

    .line 161
    int-to-float v2, v0

    int-to-float v3, v1

    iget v4, p0, Landroidx/swiperefreshlayout/widget/CircleImageView$OvalShadow;->mShadowRadius:I

    sub-int v4, v0, v4

    int-to-float v4, v4

    invoke-virtual {p1, v2, v3, v4, p2}, Landroid/graphics/Canvas;->drawCircle(FFFLandroid/graphics/Paint;)V

    .line 162
    return-void
.end method

.method protected onResize(FF)V
    .locals 1
    .param p1, "width"    # F
    .param p2, "height"    # F

    .line 152
    invoke-super {p0, p1, p2}, Landroid/graphics/drawable/shapes/OvalShape;->onResize(FF)V

    .line 153
    float-to-int v0, p1

    invoke-direct {p0, v0}, Landroidx/swiperefreshlayout/widget/CircleImageView$OvalShadow;->updateRadialGradient(I)V

    .line 154
    return-void
.end method
