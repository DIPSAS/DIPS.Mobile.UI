.class public Lcrc643f46942d9dd1fff9/CarouselPageAdapter;
.super Landroidx/viewpager/widget/PagerAdapter;
.source "CarouselPageAdapter.java"

# interfaces
.implements Lmono/android/IGCUserPeer;
.implements Landroidx/viewpager/widget/ViewPager$OnPageChangeListener;


# static fields
.field public static final __md_methods:Ljava/lang/String;


# instance fields
.field private refList:Ljava/util/ArrayList;


# direct methods
.method static constructor <clinit>()V
    .locals 3

    .line 13
    const-string v0, "n_getCount:()I:GetGetCountHandler\nn_destroyItem:(Landroid/view/ViewGroup;ILjava/lang/Object;)V:GetDestroyItem_Landroid_view_ViewGroup_ILjava_lang_Object_Handler\nn_getItemPosition:(Ljava/lang/Object;)I:GetGetItemPosition_Ljava_lang_Object_Handler\nn_instantiateItem:(Landroid/view/ViewGroup;I)Ljava/lang/Object;:GetInstantiateItem_Landroid_view_ViewGroup_IHandler\nn_isViewFromObject:(Landroid/view/View;Ljava/lang/Object;)Z:GetIsViewFromObject_Landroid_view_View_Ljava_lang_Object_Handler\nn_onPageScrollStateChanged:(I)V:GetOnPageScrollStateChanged_IHandler:AndroidX.ViewPager.Widget.ViewPager/IOnPageChangeListenerInvoker, Xamarin.AndroidX.ViewPager\nn_onPageScrolled:(IFI)V:GetOnPageScrolled_IFIHandler:AndroidX.ViewPager.Widget.ViewPager/IOnPageChangeListenerInvoker, Xamarin.AndroidX.ViewPager\nn_onPageSelected:(I)V:GetOnPageSelected_IHandler:AndroidX.ViewPager.Widget.ViewPager/IOnPageChangeListenerInvoker, Xamarin.AndroidX.ViewPager\n"

    sput-object v0, Lcrc643f46942d9dd1fff9/CarouselPageAdapter;->__md_methods:Ljava/lang/String;

    .line 23
    const-class v1, Lcrc643f46942d9dd1fff9/CarouselPageAdapter;

    const-string v2, "Xamarin.Forms.Platform.Android.CarouselPageAdapter, Xamarin.Forms.Platform.Android"

    invoke-static {v2, v1, v0}, Lmono/android/Runtime;->register(Ljava/lang/String;Ljava/lang/Class;Ljava/lang/String;)V

    .line 24
    return-void
.end method

.method public constructor <init>()V
    .locals 3

    .line 29
    invoke-direct {p0}, Landroidx/viewpager/widget/PagerAdapter;-><init>()V

    .line 30
    invoke-virtual {p0}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    move-result-object v0

    const-class v1, Lcrc643f46942d9dd1fff9/CarouselPageAdapter;

    if-ne v0, v1, :cond_0

    .line 31
    const/4 v0, 0x0

    new-array v0, v0, [Ljava/lang/Object;

    const-string v1, "Xamarin.Forms.Platform.Android.CarouselPageAdapter, Xamarin.Forms.Platform.Android"

    const-string v2, ""

    invoke-static {v1, v2, p0, v0}, Lmono/android/TypeManager;->Activate(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Object;[Ljava/lang/Object;)V

    .line 33
    :cond_0
    return-void
.end method

.method private native n_destroyItem(Landroid/view/ViewGroup;ILjava/lang/Object;)V
.end method

.method private native n_getCount()I
.end method

.method private native n_getItemPosition(Ljava/lang/Object;)I
.end method

.method private native n_instantiateItem(Landroid/view/ViewGroup;I)Ljava/lang/Object;
.end method

.method private native n_isViewFromObject(Landroid/view/View;Ljava/lang/Object;)Z
.end method

.method private native n_onPageScrollStateChanged(I)V
.end method

.method private native n_onPageScrolled(IFI)V
.end method

.method private native n_onPageSelected(I)V
.end method


# virtual methods
.method public destroyItem(Landroid/view/ViewGroup;ILjava/lang/Object;)V
    .locals 0

    .line 46
    invoke-direct {p0, p1, p2, p3}, Lcrc643f46942d9dd1fff9/CarouselPageAdapter;->n_destroyItem(Landroid/view/ViewGroup;ILjava/lang/Object;)V

    .line 47
    return-void
.end method

.method public getCount()I
    .locals 1

    .line 38
    invoke-direct {p0}, Lcrc643f46942d9dd1fff9/CarouselPageAdapter;->n_getCount()I

    move-result v0

    return v0
.end method

.method public getItemPosition(Ljava/lang/Object;)I
    .locals 0

    .line 54
    invoke-direct {p0, p1}, Lcrc643f46942d9dd1fff9/CarouselPageAdapter;->n_getItemPosition(Ljava/lang/Object;)I

    move-result p1

    return p1
.end method

.method public instantiateItem(Landroid/view/ViewGroup;I)Ljava/lang/Object;
    .locals 0

    .line 62
    invoke-direct {p0, p1, p2}, Lcrc643f46942d9dd1fff9/CarouselPageAdapter;->n_instantiateItem(Landroid/view/ViewGroup;I)Ljava/lang/Object;

    move-result-object p1

    return-object p1
.end method

.method public isViewFromObject(Landroid/view/View;Ljava/lang/Object;)Z
    .locals 0

    .line 70
    invoke-direct {p0, p1, p2}, Lcrc643f46942d9dd1fff9/CarouselPageAdapter;->n_isViewFromObject(Landroid/view/View;Ljava/lang/Object;)Z

    move-result p1

    return p1
.end method

.method public monodroidAddReference(Ljava/lang/Object;)V
    .locals 1

    .line 102
    iget-object v0, p0, Lcrc643f46942d9dd1fff9/CarouselPageAdapter;->refList:Ljava/util/ArrayList;

    if-nez v0, :cond_0

    .line 103
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    iput-object v0, p0, Lcrc643f46942d9dd1fff9/CarouselPageAdapter;->refList:Ljava/util/ArrayList;

    .line 104
    :cond_0
    iget-object v0, p0, Lcrc643f46942d9dd1fff9/CarouselPageAdapter;->refList:Ljava/util/ArrayList;

    invoke-virtual {v0, p1}, Ljava/util/ArrayList;->add(Ljava/lang/Object;)Z

    .line 105
    return-void
.end method

.method public monodroidClearReferences()V
    .locals 1

    .line 109
    iget-object v0, p0, Lcrc643f46942d9dd1fff9/CarouselPageAdapter;->refList:Ljava/util/ArrayList;

    if-eqz v0, :cond_0

    .line 110
    invoke-virtual {v0}, Ljava/util/ArrayList;->clear()V

    .line 111
    :cond_0
    return-void
.end method

.method public onPageScrollStateChanged(I)V
    .locals 0

    .line 78
    invoke-direct {p0, p1}, Lcrc643f46942d9dd1fff9/CarouselPageAdapter;->n_onPageScrollStateChanged(I)V

    .line 79
    return-void
.end method

.method public onPageScrolled(IFI)V
    .locals 0

    .line 86
    invoke-direct {p0, p1, p2, p3}, Lcrc643f46942d9dd1fff9/CarouselPageAdapter;->n_onPageScrolled(IFI)V

    .line 87
    return-void
.end method

.method public onPageSelected(I)V
    .locals 0

    .line 94
    invoke-direct {p0, p1}, Lcrc643f46942d9dd1fff9/CarouselPageAdapter;->n_onPageSelected(I)V

    .line 95
    return-void
.end method
