.class Landroidx/appcompat/widget/AppCompatTextView$InspectionCompanion$1;
.super Ljava/lang/Object;
.source "AppCompatTextView$InspectionCompanion.java"

# interfaces
.implements Ljava/util/function/IntFunction;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Landroidx/appcompat/widget/AppCompatTextView$InspectionCompanion;->mapProperties(Landroid/view/inspector/PropertyMapper;)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation

.annotation system Ldalvik/annotation/Signature;
    value = {
        "Ljava/lang/Object;",
        "Ljava/util/function/IntFunction<",
        "Ljava/lang/String;",
        ">;"
    }
.end annotation


# instance fields
.field final synthetic this$0:Landroidx/appcompat/widget/AppCompatTextView$InspectionCompanion;


# direct methods
.method constructor <init>(Landroidx/appcompat/widget/AppCompatTextView$InspectionCompanion;)V
    .locals 0
    .param p1, "this$0"    # Landroidx/appcompat/widget/AppCompatTextView$InspectionCompanion;

    .line 45
    iput-object p1, p0, Landroidx/appcompat/widget/AppCompatTextView$InspectionCompanion$1;->this$0:Landroidx/appcompat/widget/AppCompatTextView$InspectionCompanion;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public bridge synthetic apply(I)Ljava/lang/Object;
    .locals 0

    .line 45
    invoke-virtual {p0, p1}, Landroidx/appcompat/widget/AppCompatTextView$InspectionCompanion$1;->apply(I)Ljava/lang/String;

    move-result-object p1

    return-object p1
.end method

.method public apply(I)Ljava/lang/String;
    .locals 1
    .param p1, "value"    # I

    .line 48
    packed-switch p1, :pswitch_data_0

    .line 54
    invoke-static {p1}, Ljava/lang/String;->valueOf(I)Ljava/lang/String;

    move-result-object v0

    return-object v0

    .line 52
    :pswitch_0
    const-string v0, "uniform"

    return-object v0

    .line 50
    :pswitch_1
    const-string v0, "none"

    return-object v0

    :pswitch_data_0
    .packed-switch 0x0
        :pswitch_1
        :pswitch_0
    .end packed-switch
.end method
