.class Landroidx/lifecycle/Transformations$2;
.super Ljava/lang/Object;
.source "Transformations.java"

# interfaces
.implements Landroidx/lifecycle/Observer;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Landroidx/lifecycle/Transformations;->switchMap(Landroidx/lifecycle/LiveData;Landroidx/arch/core/util/Function;)Landroidx/lifecycle/LiveData;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation

.annotation system Ldalvik/annotation/Signature;
    value = {
        "Ljava/lang/Object;",
        "Landroidx/lifecycle/Observer<",
        "TX;>;"
    }
.end annotation


# instance fields
.field mSource:Landroidx/lifecycle/LiveData;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Landroidx/lifecycle/LiveData<",
            "TY;>;"
        }
    .end annotation
.end field

.field final synthetic val$result:Landroidx/lifecycle/MediatorLiveData;

.field final synthetic val$switchMapFunction:Landroidx/arch/core/util/Function;


# direct methods
.method constructor <init>(Landroidx/arch/core/util/Function;Landroidx/lifecycle/MediatorLiveData;)V
    .locals 0
    .annotation system Ldalvik/annotation/MethodParameters;
        accessFlags = {
            0x1010,
            0x1010
        }
        names = {
            "val$switchMapFunction",
            "val$result"
        }
    .end annotation

    .line 138
    iput-object p1, p0, Landroidx/lifecycle/Transformations$2;->val$switchMapFunction:Landroidx/arch/core/util/Function;

    iput-object p2, p0, Landroidx/lifecycle/Transformations$2;->val$result:Landroidx/lifecycle/MediatorLiveData;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public onChanged(Ljava/lang/Object;)V
    .locals 3
    .annotation system Ldalvik/annotation/MethodParameters;
        accessFlags = {
            0x0
        }
        names = {
            "x"
        }
    .end annotation

    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(TX;)V"
        }
    .end annotation

    .line 143
    .local p1, "x":Ljava/lang/Object;, "TX;"
    iget-object v0, p0, Landroidx/lifecycle/Transformations$2;->val$switchMapFunction:Landroidx/arch/core/util/Function;

    invoke-interface {v0, p1}, Landroidx/arch/core/util/Function;->apply(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Landroidx/lifecycle/LiveData;

    .line 144
    .local v0, "newLiveData":Landroidx/lifecycle/LiveData;, "Landroidx/lifecycle/LiveData<TY;>;"
    iget-object v1, p0, Landroidx/lifecycle/Transformations$2;->mSource:Landroidx/lifecycle/LiveData;

    if-ne v1, v0, :cond_0

    .line 145
    return-void

    .line 147
    :cond_0
    if-eqz v1, :cond_1

    .line 148
    iget-object v2, p0, Landroidx/lifecycle/Transformations$2;->val$result:Landroidx/lifecycle/MediatorLiveData;

    invoke-virtual {v2, v1}, Landroidx/lifecycle/MediatorLiveData;->removeSource(Landroidx/lifecycle/LiveData;)V

    .line 150
    :cond_1
    iput-object v0, p0, Landroidx/lifecycle/Transformations$2;->mSource:Landroidx/lifecycle/LiveData;

    .line 151
    if-eqz v0, :cond_2

    .line 152
    iget-object v1, p0, Landroidx/lifecycle/Transformations$2;->val$result:Landroidx/lifecycle/MediatorLiveData;

    new-instance v2, Landroidx/lifecycle/Transformations$2$1;

    invoke-direct {v2, p0}, Landroidx/lifecycle/Transformations$2$1;-><init>(Landroidx/lifecycle/Transformations$2;)V

    invoke-virtual {v1, v0, v2}, Landroidx/lifecycle/MediatorLiveData;->addSource(Landroidx/lifecycle/LiveData;Landroidx/lifecycle/Observer;)V

    .line 159
    :cond_2
    return-void
.end method
