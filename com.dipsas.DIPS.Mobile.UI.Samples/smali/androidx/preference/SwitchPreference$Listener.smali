.class Landroidx/preference/SwitchPreference$Listener;
.super Ljava/lang/Object;
.source "SwitchPreference.java"

# interfaces
.implements Landroid/widget/CompoundButton$OnCheckedChangeListener;


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Landroidx/preference/SwitchPreference;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x2
    name = "Listener"
.end annotation


# instance fields
.field final synthetic this$0:Landroidx/preference/SwitchPreference;


# direct methods
.method constructor <init>(Landroidx/preference/SwitchPreference;)V
    .locals 0

    .line 230
    iput-object p1, p0, Landroidx/preference/SwitchPreference$Listener;->this$0:Landroidx/preference/SwitchPreference;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public onCheckedChanged(Landroid/widget/CompoundButton;Z)V
    .locals 2
    .param p1, "buttonView"    # Landroid/widget/CompoundButton;
    .param p2, "isChecked"    # Z

    .line 234
    iget-object v0, p0, Landroidx/preference/SwitchPreference$Listener;->this$0:Landroidx/preference/SwitchPreference;

    invoke-static {p2}, Ljava/lang/Boolean;->valueOf(Z)Ljava/lang/Boolean;

    move-result-object v1

    invoke-virtual {v0, v1}, Landroidx/preference/SwitchPreference;->callChangeListener(Ljava/lang/Object;)Z

    move-result v0

    if-nez v0, :cond_0

    .line 237
    xor-int/lit8 v0, p2, 0x1

    invoke-virtual {p1, v0}, Landroid/widget/CompoundButton;->setChecked(Z)V

    .line 238
    return-void

    .line 241
    :cond_0
    iget-object v0, p0, Landroidx/preference/SwitchPreference$Listener;->this$0:Landroidx/preference/SwitchPreference;

    invoke-virtual {v0, p2}, Landroidx/preference/SwitchPreference;->setChecked(Z)V

    .line 242
    return-void
.end method
