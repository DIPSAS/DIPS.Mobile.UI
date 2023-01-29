.class Landroidx/navigation/ui/ActionBarOnDestinationChangedListener;
.super Landroidx/navigation/ui/AbstractAppBarOnDestinationChangedListener;
.source "ActionBarOnDestinationChangedListener.java"


# instance fields
.field private final mActivity:Landroidx/appcompat/app/AppCompatActivity;


# direct methods
.method constructor <init>(Landroidx/appcompat/app/AppCompatActivity;Landroidx/navigation/ui/AppBarConfiguration;)V
    .locals 1
    .param p1, "activity"    # Landroidx/appcompat/app/AppCompatActivity;
    .param p2, "configuration"    # Landroidx/navigation/ui/AppBarConfiguration;

    .line 41
    invoke-virtual {p1}, Landroidx/appcompat/app/AppCompatActivity;->getDrawerToggleDelegate()Landroidx/appcompat/app/ActionBarDrawerToggle$Delegate;

    move-result-object v0

    invoke-interface {v0}, Landroidx/appcompat/app/ActionBarDrawerToggle$Delegate;->getActionBarThemedContext()Landroid/content/Context;

    move-result-object v0

    invoke-direct {p0, v0, p2}, Landroidx/navigation/ui/AbstractAppBarOnDestinationChangedListener;-><init>(Landroid/content/Context;Landroidx/navigation/ui/AppBarConfiguration;)V

    .line 42
    iput-object p1, p0, Landroidx/navigation/ui/ActionBarOnDestinationChangedListener;->mActivity:Landroidx/appcompat/app/AppCompatActivity;

    .line 43
    return-void
.end method


# virtual methods
.method protected setNavigationIcon(Landroid/graphics/drawable/Drawable;I)V
    .locals 2
    .param p1, "icon"    # Landroid/graphics/drawable/Drawable;
    .param p2, "contentDescription"    # I

    .line 54
    iget-object v0, p0, Landroidx/navigation/ui/ActionBarOnDestinationChangedListener;->mActivity:Landroidx/appcompat/app/AppCompatActivity;

    invoke-virtual {v0}, Landroidx/appcompat/app/AppCompatActivity;->getSupportActionBar()Landroidx/appcompat/app/ActionBar;

    move-result-object v0

    .line 55
    .local v0, "actionBar":Landroidx/appcompat/app/ActionBar;
    if-nez p1, :cond_0

    .line 56
    const/4 v1, 0x0

    invoke-virtual {v0, v1}, Landroidx/appcompat/app/ActionBar;->setDisplayHomeAsUpEnabled(Z)V

    goto :goto_0

    .line 58
    :cond_0
    const/4 v1, 0x1

    invoke-virtual {v0, v1}, Landroidx/appcompat/app/ActionBar;->setDisplayHomeAsUpEnabled(Z)V

    .line 59
    iget-object v1, p0, Landroidx/navigation/ui/ActionBarOnDestinationChangedListener;->mActivity:Landroidx/appcompat/app/AppCompatActivity;

    invoke-virtual {v1}, Landroidx/appcompat/app/AppCompatActivity;->getDrawerToggleDelegate()Landroidx/appcompat/app/ActionBarDrawerToggle$Delegate;

    move-result-object v1

    .line 60
    .local v1, "delegate":Landroidx/appcompat/app/ActionBarDrawerToggle$Delegate;
    invoke-interface {v1, p1, p2}, Landroidx/appcompat/app/ActionBarDrawerToggle$Delegate;->setActionBarUpIndicator(Landroid/graphics/drawable/Drawable;I)V

    .line 62
    .end local v1    # "delegate":Landroidx/appcompat/app/ActionBarDrawerToggle$Delegate;
    :goto_0
    return-void
.end method

.method protected setTitle(Ljava/lang/CharSequence;)V
    .locals 1
    .param p1, "title"    # Ljava/lang/CharSequence;

    .line 47
    iget-object v0, p0, Landroidx/navigation/ui/ActionBarOnDestinationChangedListener;->mActivity:Landroidx/appcompat/app/AppCompatActivity;

    invoke-virtual {v0}, Landroidx/appcompat/app/AppCompatActivity;->getSupportActionBar()Landroidx/appcompat/app/ActionBar;

    move-result-object v0

    .line 48
    .local v0, "actionBar":Landroidx/appcompat/app/ActionBar;
    invoke-virtual {v0, p1}, Landroidx/appcompat/app/ActionBar;->setTitle(Ljava/lang/CharSequence;)V

    .line 49
    return-void
.end method
