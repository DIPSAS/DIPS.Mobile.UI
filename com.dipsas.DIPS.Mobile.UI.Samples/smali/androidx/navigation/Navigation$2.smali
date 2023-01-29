.class Landroidx/navigation/Navigation$2;
.super Ljava/lang/Object;
.source "Navigation.java"

# interfaces
.implements Landroid/view/View$OnClickListener;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Landroidx/navigation/Navigation;->createNavigateOnClickListener(Landroidx/navigation/NavDirections;)Landroid/view/View$OnClickListener;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic val$directions:Landroidx/navigation/NavDirections;


# direct methods
.method constructor <init>(Landroidx/navigation/NavDirections;)V
    .locals 0

    .line 134
    iput-object p1, p0, Landroidx/navigation/Navigation$2;->val$directions:Landroidx/navigation/NavDirections;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public onClick(Landroid/view/View;)V
    .locals 2
    .param p1, "view"    # Landroid/view/View;

    .line 137
    invoke-static {p1}, Landroidx/navigation/Navigation;->findNavController(Landroid/view/View;)Landroidx/navigation/NavController;

    move-result-object v0

    iget-object v1, p0, Landroidx/navigation/Navigation$2;->val$directions:Landroidx/navigation/NavDirections;

    invoke-virtual {v0, v1}, Landroidx/navigation/NavController;->navigate(Landroidx/navigation/NavDirections;)V

    .line 138
    return-void
.end method
