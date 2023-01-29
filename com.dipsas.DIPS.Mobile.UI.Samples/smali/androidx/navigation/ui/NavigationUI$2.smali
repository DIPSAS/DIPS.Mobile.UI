.class Landroidx/navigation/ui/NavigationUI$2;
.super Ljava/lang/Object;
.source "NavigationUI.java"

# interfaces
.implements Landroid/view/View$OnClickListener;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Landroidx/navigation/ui/NavigationUI;->setupWithNavController(Lcom/google/android/material/appbar/CollapsingToolbarLayout;Landroidx/appcompat/widget/Toolbar;Landroidx/navigation/NavController;Landroidx/navigation/ui/AppBarConfiguration;)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic val$configuration:Landroidx/navigation/ui/AppBarConfiguration;

.field final synthetic val$navController:Landroidx/navigation/NavController;


# direct methods
.method constructor <init>(Landroidx/navigation/NavController;Landroidx/navigation/ui/AppBarConfiguration;)V
    .locals 0

    .line 420
    iput-object p1, p0, Landroidx/navigation/ui/NavigationUI$2;->val$navController:Landroidx/navigation/NavController;

    iput-object p2, p0, Landroidx/navigation/ui/NavigationUI$2;->val$configuration:Landroidx/navigation/ui/AppBarConfiguration;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public onClick(Landroid/view/View;)V
    .locals 2
    .param p1, "v"    # Landroid/view/View;

    .line 423
    iget-object v0, p0, Landroidx/navigation/ui/NavigationUI$2;->val$navController:Landroidx/navigation/NavController;

    iget-object v1, p0, Landroidx/navigation/ui/NavigationUI$2;->val$configuration:Landroidx/navigation/ui/AppBarConfiguration;

    invoke-static {v0, v1}, Landroidx/navigation/ui/NavigationUI;->navigateUp(Landroidx/navigation/NavController;Landroidx/navigation/ui/AppBarConfiguration;)Z

    .line 424
    return-void
.end method
