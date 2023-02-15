.class Landroidx/navigation/Navigation$1;
.super Ljava/lang/Object;
.source "Navigation.java"

# interfaces
.implements Landroid/view/View$OnClickListener;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Landroidx/navigation/Navigation;->createNavigateOnClickListener(ILandroid/os/Bundle;)Landroid/view/View$OnClickListener;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic val$args:Landroid/os/Bundle;

.field final synthetic val$resId:I


# direct methods
.method constructor <init>(ILandroid/os/Bundle;)V
    .locals 0

    .line 116
    iput p1, p0, Landroidx/navigation/Navigation$1;->val$resId:I

    iput-object p2, p0, Landroidx/navigation/Navigation$1;->val$args:Landroid/os/Bundle;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public onClick(Landroid/view/View;)V
    .locals 3
    .param p1, "view"    # Landroid/view/View;

    .line 119
    invoke-static {p1}, Landroidx/navigation/Navigation;->findNavController(Landroid/view/View;)Landroidx/navigation/NavController;

    move-result-object v0

    iget v1, p0, Landroidx/navigation/Navigation$1;->val$resId:I

    iget-object v2, p0, Landroidx/navigation/Navigation$1;->val$args:Landroid/os/Bundle;

    invoke-virtual {v0, v1, v2}, Landroidx/navigation/NavController;->navigate(ILandroid/os/Bundle;)V

    .line 120
    return-void
.end method
