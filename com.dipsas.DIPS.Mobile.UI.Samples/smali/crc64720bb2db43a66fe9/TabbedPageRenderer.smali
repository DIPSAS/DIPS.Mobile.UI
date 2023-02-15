.class public Lcrc64720bb2db43a66fe9/TabbedPageRenderer;
.super Lcrc643f46942d9dd1fff9/VisualElementRenderer_1;
.source "TabbedPageRenderer.java"

# interfaces
.implements Lmono/android/IGCUserPeer;
.implements Lcom/google/android/material/tabs/TabLayout$BaseOnTabSelectedListener;
.implements Landroidx/viewpager/widget/ViewPager$OnPageChangeListener;
.implements Lcom/google/android/material/navigation/NavigationBarView$OnItemSelectedListener;


# static fields
.field public static final __md_methods:Ljava/lang/String;


# instance fields
.field private refList:Ljava/util/ArrayList;


# direct methods
.method static constructor <clinit>()V
    .locals 3

    .line 15
    const-string v0, "n_onAttachedToWindow:()V:GetOnAttachedToWindowHandler\nn_onDetachedFromWindow:()V:GetOnDetachedFromWindowHandler\nn_onLayout:(ZIIII)V:GetOnLayout_ZIIIIHandler\nn_onTabReselected:(Lcom/google/android/material/tabs/TabLayout$Tab;)V:GetOnTabReselected_Lcom_google_android_material_tabs_TabLayout_Tab_Handler:Google.Android.Material.Tabs.TabLayout/IOnTabSelectedListenerInvoker, Xamarin.Google.Android.Material\nn_onTabSelected:(Lcom/google/android/material/tabs/TabLayout$Tab;)V:GetOnTabSelected_Lcom_google_android_material_tabs_TabLayout_Tab_Handler:Google.Android.Material.Tabs.TabLayout/IOnTabSelectedListenerInvoker, Xamarin.Google.Android.Material\nn_onTabUnselected:(Lcom/google/android/material/tabs/TabLayout$Tab;)V:GetOnTabUnselected_Lcom_google_android_material_tabs_TabLayout_Tab_Handler:Google.Android.Material.Tabs.TabLayout/IOnTabSelectedListenerInvoker, Xamarin.Google.Android.Material\nn_onPageScrollStateChanged:(I)V:GetOnPageScrollStateChanged_IHandler:AndroidX.ViewPager.Widget.ViewPager/IOnPageChangeListenerInvoker, Xamarin.AndroidX.ViewPager\nn_onPageScrolled:(IFI)V:GetOnPageScrolled_IFIHandler:AndroidX.ViewPager.Widget.ViewPager/IOnPageChangeListenerInvoker, Xamarin.AndroidX.ViewPager\nn_onPageSelected:(I)V:GetOnPageSelected_IHandler:AndroidX.ViewPager.Widget.ViewPager/IOnPageChangeListenerInvoker, Xamarin.AndroidX.ViewPager\nn_onNavigationItemSelected:(Landroid/view/MenuItem;)Z:GetOnNavigationItemSelected_Landroid_view_MenuItem_Handler:Google.Android.Material.Navigation.NavigationBarView/IOnItemSelectedListenerInvoker, Xamarin.Google.Android.Material\n"

    sput-object v0, Lcrc64720bb2db43a66fe9/TabbedPageRenderer;->__md_methods:Ljava/lang/String;

    .line 27
    const-class v1, Lcrc64720bb2db43a66fe9/TabbedPageRenderer;

    const-string v2, "Xamarin.Forms.Platform.Android.AppCompat.TabbedPageRenderer, Xamarin.Forms.Platform.Android"

    invoke-static {v2, v1, v0}, Lmono/android/Runtime;->register(Ljava/lang/String;Ljava/lang/Class;Ljava/lang/String;)V

    .line 28
    return-void
.end method

.method public constructor <init>(Landroid/content/Context;)V
    .locals 2

    .line 33
    invoke-direct {p0, p1}, Lcrc643f46942d9dd1fff9/VisualElementRenderer_1;-><init>(Landroid/content/Context;)V

    .line 34
    invoke-virtual {p0}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    move-result-object v0

    const-class v1, Lcrc64720bb2db43a66fe9/TabbedPageRenderer;

    if-ne v0, v1, :cond_0

    .line 35
    const/4 v0, 0x1

    new-array v0, v0, [Ljava/lang/Object;

    const/4 v1, 0x0

    aput-object p1, v0, v1

    const-string p1, "Xamarin.Forms.Platform.Android.AppCompat.TabbedPageRenderer, Xamarin.Forms.Platform.Android"

    const-string v1, "Android.Content.Context, Mono.Android"

    invoke-static {p1, v1, p0, v0}, Lmono/android/TypeManager;->Activate(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Object;[Ljava/lang/Object;)V

    .line 37
    :cond_0
    return-void
.end method

.method public constructor <init>(Landroid/content/Context;Landroid/util/AttributeSet;)V
    .locals 2

    .line 42
    invoke-direct {p0, p1, p2}, Lcrc643f46942d9dd1fff9/VisualElementRenderer_1;-><init>(Landroid/content/Context;Landroid/util/AttributeSet;)V

    .line 43
    invoke-virtual {p0}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    move-result-object v0

    const-class v1, Lcrc64720bb2db43a66fe9/TabbedPageRenderer;

    if-ne v0, v1, :cond_0

    .line 44
    const/4 v0, 0x2

    new-array v0, v0, [Ljava/lang/Object;

    const/4 v1, 0x0

    aput-object p1, v0, v1

    const/4 p1, 0x1

    aput-object p2, v0, p1

    const-string p1, "Xamarin.Forms.Platform.Android.AppCompat.TabbedPageRenderer, Xamarin.Forms.Platform.Android"

    const-string p2, "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android"

    invoke-static {p1, p2, p0, v0}, Lmono/android/TypeManager;->Activate(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Object;[Ljava/lang/Object;)V

    .line 46
    :cond_0
    return-void
.end method

.method public constructor <init>(Landroid/content/Context;Landroid/util/AttributeSet;I)V
    .locals 2

    .line 51
    invoke-direct {p0, p1, p2, p3}, Lcrc643f46942d9dd1fff9/VisualElementRenderer_1;-><init>(Landroid/content/Context;Landroid/util/AttributeSet;I)V

    .line 52
    invoke-virtual {p0}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    move-result-object v0

    const-class v1, Lcrc64720bb2db43a66fe9/TabbedPageRenderer;

    if-ne v0, v1, :cond_0

    .line 53
    const/4 v0, 0x3

    new-array v0, v0, [Ljava/lang/Object;

    const/4 v1, 0x0

    aput-object p1, v0, v1

    const/4 p1, 0x1

    aput-object p2, v0, p1

    const/4 p1, 0x2

    invoke-static {p3}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object p2

    aput-object p2, v0, p1

    const-string p1, "Xamarin.Forms.Platform.Android.AppCompat.TabbedPageRenderer, Xamarin.Forms.Platform.Android"

    const-string p2, "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android:System.Int32, mscorlib"

    invoke-static {p1, p2, p0, v0}, Lmono/android/TypeManager;->Activate(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Object;[Ljava/lang/Object;)V

    .line 55
    :cond_0
    return-void
.end method

.method private native n_onAttachedToWindow()V
.end method

.method private native n_onDetachedFromWindow()V
.end method

.method private native n_onLayout(ZIIII)V
.end method

.method private native n_onNavigationItemSelected(Landroid/view/MenuItem;)Z
.end method

.method private native n_onPageScrollStateChanged(I)V
.end method

.method private native n_onPageScrolled(IFI)V
.end method

.method private native n_onPageSelected(I)V
.end method

.method private native n_onTabReselected(Lcom/google/android/material/tabs/TabLayout$Tab;)V
.end method

.method private native n_onTabSelected(Lcom/google/android/material/tabs/TabLayout$Tab;)V
.end method

.method private native n_onTabUnselected(Lcom/google/android/material/tabs/TabLayout$Tab;)V
.end method


# virtual methods
.method public monodroidAddReference(Ljava/lang/Object;)V
    .locals 1

    .line 140
    iget-object v0, p0, Lcrc64720bb2db43a66fe9/TabbedPageRenderer;->refList:Ljava/util/ArrayList;

    if-nez v0, :cond_0

    .line 141
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    iput-object v0, p0, Lcrc64720bb2db43a66fe9/TabbedPageRenderer;->refList:Ljava/util/ArrayList;

    .line 142
    :cond_0
    iget-object v0, p0, Lcrc64720bb2db43a66fe9/TabbedPageRenderer;->refList:Ljava/util/ArrayList;

    invoke-virtual {v0, p1}, Ljava/util/ArrayList;->add(Ljava/lang/Object;)Z

    .line 143
    return-void
.end method

.method public monodroidClearReferences()V
    .locals 1

    .line 147
    iget-object v0, p0, Lcrc64720bb2db43a66fe9/TabbedPageRenderer;->refList:Ljava/util/ArrayList;

    if-eqz v0, :cond_0

    .line 148
    invoke-virtual {v0}, Ljava/util/ArrayList;->clear()V

    .line 149
    :cond_0
    return-void
.end method

.method public onAttachedToWindow()V
    .locals 0

    .line 60
    invoke-direct {p0}, Lcrc64720bb2db43a66fe9/TabbedPageRenderer;->n_onAttachedToWindow()V

    .line 61
    return-void
.end method

.method public onDetachedFromWindow()V
    .locals 0

    .line 68
    invoke-direct {p0}, Lcrc64720bb2db43a66fe9/TabbedPageRenderer;->n_onDetachedFromWindow()V

    .line 69
    return-void
.end method

.method public onLayout(ZIIII)V
    .locals 0

    .line 76
    invoke-direct/range {p0 .. p5}, Lcrc64720bb2db43a66fe9/TabbedPageRenderer;->n_onLayout(ZIIII)V

    .line 77
    return-void
.end method

.method public onNavigationItemSelected(Landroid/view/MenuItem;)Z
    .locals 0

    .line 132
    invoke-direct {p0, p1}, Lcrc64720bb2db43a66fe9/TabbedPageRenderer;->n_onNavigationItemSelected(Landroid/view/MenuItem;)Z

    move-result p1

    return p1
.end method

.method public onPageScrollStateChanged(I)V
    .locals 0

    .line 108
    invoke-direct {p0, p1}, Lcrc64720bb2db43a66fe9/TabbedPageRenderer;->n_onPageScrollStateChanged(I)V

    .line 109
    return-void
.end method

.method public onPageScrolled(IFI)V
    .locals 0

    .line 116
    invoke-direct {p0, p1, p2, p3}, Lcrc64720bb2db43a66fe9/TabbedPageRenderer;->n_onPageScrolled(IFI)V

    .line 117
    return-void
.end method

.method public onPageSelected(I)V
    .locals 0

    .line 124
    invoke-direct {p0, p1}, Lcrc64720bb2db43a66fe9/TabbedPageRenderer;->n_onPageSelected(I)V

    .line 125
    return-void
.end method

.method public onTabReselected(Lcom/google/android/material/tabs/TabLayout$Tab;)V
    .locals 0

    .line 84
    invoke-direct {p0, p1}, Lcrc64720bb2db43a66fe9/TabbedPageRenderer;->n_onTabReselected(Lcom/google/android/material/tabs/TabLayout$Tab;)V

    .line 85
    return-void
.end method

.method public onTabSelected(Lcom/google/android/material/tabs/TabLayout$Tab;)V
    .locals 0

    .line 92
    invoke-direct {p0, p1}, Lcrc64720bb2db43a66fe9/TabbedPageRenderer;->n_onTabSelected(Lcom/google/android/material/tabs/TabLayout$Tab;)V

    .line 93
    return-void
.end method

.method public onTabUnselected(Lcom/google/android/material/tabs/TabLayout$Tab;)V
    .locals 0

    .line 100
    invoke-direct {p0, p1}, Lcrc64720bb2db43a66fe9/TabbedPageRenderer;->n_onTabUnselected(Lcom/google/android/material/tabs/TabLayout$Tab;)V

    .line 101
    return-void
.end method
