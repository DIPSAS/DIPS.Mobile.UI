.class Landroidx/navigation/NavControllerViewModel$1;
.super Ljava/lang/Object;
.source "NavControllerViewModel.java"

# interfaces
.implements Landroidx/lifecycle/ViewModelProvider$Factory;


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Landroidx/navigation/NavControllerViewModel;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# direct methods
.method constructor <init>()V
    .locals 0

    .line 34
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public create(Ljava/lang/Class;)Landroidx/lifecycle/ViewModel;
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "<T:",
            "Landroidx/lifecycle/ViewModel;",
            ">(",
            "Ljava/lang/Class<",
            "TT;>;)TT;"
        }
    .end annotation

    .line 39
    .local p1, "modelClass":Ljava/lang/Class;, "Ljava/lang/Class<TT;>;"
    new-instance v0, Landroidx/navigation/NavControllerViewModel;

    invoke-direct {v0}, Landroidx/navigation/NavControllerViewModel;-><init>()V

    .line 40
    .local v0, "viewModel":Landroidx/navigation/NavControllerViewModel;
    return-object v0
.end method
