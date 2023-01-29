.class public Lmono/android/location/LocationListenerImplementor;
.super Ljava/lang/Object;
.source "LocationListenerImplementor.java"

# interfaces
.implements Lmono/android/IGCUserPeer;
.implements Landroid/location/LocationListener;


# static fields
.field public static final __md_methods:Ljava/lang/String;


# instance fields
.field private refList:Ljava/util/ArrayList;


# direct methods
.method static constructor <clinit>()V
    .locals 3

    .line 13
    const-string v0, "n_onLocationChanged:(Landroid/location/Location;)V:GetOnLocationChanged_Landroid_location_Location_Handler:Android.Locations.ILocationListenerInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\nn_onProviderDisabled:(Ljava/lang/String;)V:GetOnProviderDisabled_Ljava_lang_String_Handler:Android.Locations.ILocationListenerInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\nn_onProviderEnabled:(Ljava/lang/String;)V:GetOnProviderEnabled_Ljava_lang_String_Handler:Android.Locations.ILocationListenerInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\nn_onStatusChanged:(Ljava/lang/String;ILandroid/os/Bundle;)V:GetOnStatusChanged_Ljava_lang_String_ILandroid_os_Bundle_Handler:Android.Locations.ILocationListenerInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\nn_onFlushComplete:(I)V:GetOnFlushComplete_IHandler:Android.Locations.ILocationListener, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n"

    sput-object v0, Lmono/android/location/LocationListenerImplementor;->__md_methods:Ljava/lang/String;

    .line 20
    const-class v1, Lmono/android/location/LocationListenerImplementor;

    const-string v2, "Android.Locations.ILocationListenerImplementor, Mono.Android"

    invoke-static {v2, v1, v0}, Lmono/android/Runtime;->register(Ljava/lang/String;Ljava/lang/Class;Ljava/lang/String;)V

    .line 21
    return-void
.end method

.method public constructor <init>()V
    .locals 3

    .line 26
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 27
    invoke-virtual {p0}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    move-result-object v0

    const-class v1, Lmono/android/location/LocationListenerImplementor;

    if-ne v0, v1, :cond_0

    .line 28
    const/4 v0, 0x0

    new-array v0, v0, [Ljava/lang/Object;

    const-string v1, "Android.Locations.ILocationListenerImplementor, Mono.Android"

    const-string v2, ""

    invoke-static {v1, v2, p0, v0}, Lmono/android/TypeManager;->Activate(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Object;[Ljava/lang/Object;)V

    .line 30
    :cond_0
    return-void
.end method

.method private native n_onFlushComplete(I)V
.end method

.method private native n_onLocationChanged(Landroid/location/Location;)V
.end method

.method private native n_onProviderDisabled(Ljava/lang/String;)V
.end method

.method private native n_onProviderEnabled(Ljava/lang/String;)V
.end method

.method private native n_onStatusChanged(Ljava/lang/String;ILandroid/os/Bundle;)V
.end method


# virtual methods
.method public monodroidAddReference(Ljava/lang/Object;)V
    .locals 1

    .line 75
    iget-object v0, p0, Lmono/android/location/LocationListenerImplementor;->refList:Ljava/util/ArrayList;

    if-nez v0, :cond_0

    .line 76
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    iput-object v0, p0, Lmono/android/location/LocationListenerImplementor;->refList:Ljava/util/ArrayList;

    .line 77
    :cond_0
    iget-object v0, p0, Lmono/android/location/LocationListenerImplementor;->refList:Ljava/util/ArrayList;

    invoke-virtual {v0, p1}, Ljava/util/ArrayList;->add(Ljava/lang/Object;)Z

    .line 78
    return-void
.end method

.method public monodroidClearReferences()V
    .locals 1

    .line 82
    iget-object v0, p0, Lmono/android/location/LocationListenerImplementor;->refList:Ljava/util/ArrayList;

    if-eqz v0, :cond_0

    .line 83
    invoke-virtual {v0}, Ljava/util/ArrayList;->clear()V

    .line 84
    :cond_0
    return-void
.end method

.method public onFlushComplete(I)V
    .locals 0

    .line 67
    invoke-direct {p0, p1}, Lmono/android/location/LocationListenerImplementor;->n_onFlushComplete(I)V

    .line 68
    return-void
.end method

.method public onLocationChanged(Landroid/location/Location;)V
    .locals 0

    .line 35
    invoke-direct {p0, p1}, Lmono/android/location/LocationListenerImplementor;->n_onLocationChanged(Landroid/location/Location;)V

    .line 36
    return-void
.end method

.method public onProviderDisabled(Ljava/lang/String;)V
    .locals 0

    .line 43
    invoke-direct {p0, p1}, Lmono/android/location/LocationListenerImplementor;->n_onProviderDisabled(Ljava/lang/String;)V

    .line 44
    return-void
.end method

.method public onProviderEnabled(Ljava/lang/String;)V
    .locals 0

    .line 51
    invoke-direct {p0, p1}, Lmono/android/location/LocationListenerImplementor;->n_onProviderEnabled(Ljava/lang/String;)V

    .line 52
    return-void
.end method

.method public onStatusChanged(Ljava/lang/String;ILandroid/os/Bundle;)V
    .locals 0

    .line 59
    invoke-direct {p0, p1, p2, p3}, Lmono/android/location/LocationListenerImplementor;->n_onStatusChanged(Ljava/lang/String;ILandroid/os/Bundle;)V

    .line 60
    return-void
.end method
