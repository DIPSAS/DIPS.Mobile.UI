.class public Lxamarin/android/net/ServerCertificateCustomValidator_TrustManager;
.super Ljava/lang/Object;
.source "ServerCertificateCustomValidator_TrustManager.java"

# interfaces
.implements Lmono/android/IGCUserPeer;
.implements Ljavax/net/ssl/X509TrustManager;
.implements Ljavax/net/ssl/TrustManager;


# static fields
.field public static final __md_methods:Ljava/lang/String;


# instance fields
.field private refList:Ljava/util/ArrayList;


# direct methods
.method static constructor <clinit>()V
    .locals 3

    .line 14
    const-string v0, "n_checkClientTrusted:([Ljava/security/cert/X509Certificate;Ljava/lang/String;)V:GetCheckClientTrusted_arrayLjava_security_cert_X509Certificate_Ljava_lang_String_Handler:Javax.Net.Ssl.IX509TrustManagerInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\nn_checkServerTrusted:([Ljava/security/cert/X509Certificate;Ljava/lang/String;)V:GetCheckServerTrusted_arrayLjava_security_cert_X509Certificate_Ljava_lang_String_Handler:Javax.Net.Ssl.IX509TrustManagerInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\nn_getAcceptedIssuers:()[Ljava/security/cert/X509Certificate;:GetGetAcceptedIssuersHandler:Javax.Net.Ssl.IX509TrustManagerInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n"

    sput-object v0, Lxamarin/android/net/ServerCertificateCustomValidator_TrustManager;->__md_methods:Ljava/lang/String;

    .line 19
    const-class v1, Lxamarin/android/net/ServerCertificateCustomValidator_TrustManager;

    const-string v2, "Xamarin.Android.Net.ServerCertificateCustomValidator+TrustManager, Mono.Android"

    invoke-static {v2, v1, v0}, Lmono/android/Runtime;->register(Ljava/lang/String;Ljava/lang/Class;Ljava/lang/String;)V

    .line 20
    return-void
.end method

.method public constructor <init>()V
    .locals 3

    .line 25
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 26
    invoke-virtual {p0}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    move-result-object v0

    const-class v1, Lxamarin/android/net/ServerCertificateCustomValidator_TrustManager;

    if-ne v0, v1, :cond_0

    .line 27
    const/4 v0, 0x0

    new-array v0, v0, [Ljava/lang/Object;

    const-string v1, "Xamarin.Android.Net.ServerCertificateCustomValidator+TrustManager, Mono.Android"

    const-string v2, ""

    invoke-static {v1, v2, p0, v0}, Lmono/android/TypeManager;->Activate(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Object;[Ljava/lang/Object;)V

    .line 29
    :cond_0
    return-void
.end method

.method private native n_checkClientTrusted([Ljava/security/cert/X509Certificate;Ljava/lang/String;)V
.end method

.method private native n_checkServerTrusted([Ljava/security/cert/X509Certificate;Ljava/lang/String;)V
.end method

.method private native n_getAcceptedIssuers()[Ljava/security/cert/X509Certificate;
.end method


# virtual methods
.method public checkClientTrusted([Ljava/security/cert/X509Certificate;Ljava/lang/String;)V
    .locals 0

    .line 34
    invoke-direct {p0, p1, p2}, Lxamarin/android/net/ServerCertificateCustomValidator_TrustManager;->n_checkClientTrusted([Ljava/security/cert/X509Certificate;Ljava/lang/String;)V

    .line 35
    return-void
.end method

.method public checkServerTrusted([Ljava/security/cert/X509Certificate;Ljava/lang/String;)V
    .locals 0

    .line 42
    invoke-direct {p0, p1, p2}, Lxamarin/android/net/ServerCertificateCustomValidator_TrustManager;->n_checkServerTrusted([Ljava/security/cert/X509Certificate;Ljava/lang/String;)V

    .line 43
    return-void
.end method

.method public getAcceptedIssuers()[Ljava/security/cert/X509Certificate;
    .locals 1

    .line 50
    invoke-direct {p0}, Lxamarin/android/net/ServerCertificateCustomValidator_TrustManager;->n_getAcceptedIssuers()[Ljava/security/cert/X509Certificate;

    move-result-object v0

    return-object v0
.end method

.method public monodroidAddReference(Ljava/lang/Object;)V
    .locals 1

    .line 58
    iget-object v0, p0, Lxamarin/android/net/ServerCertificateCustomValidator_TrustManager;->refList:Ljava/util/ArrayList;

    if-nez v0, :cond_0

    .line 59
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    iput-object v0, p0, Lxamarin/android/net/ServerCertificateCustomValidator_TrustManager;->refList:Ljava/util/ArrayList;

    .line 60
    :cond_0
    iget-object v0, p0, Lxamarin/android/net/ServerCertificateCustomValidator_TrustManager;->refList:Ljava/util/ArrayList;

    invoke-virtual {v0, p1}, Ljava/util/ArrayList;->add(Ljava/lang/Object;)Z

    .line 61
    return-void
.end method

.method public monodroidClearReferences()V
    .locals 1

    .line 65
    iget-object v0, p0, Lxamarin/android/net/ServerCertificateCustomValidator_TrustManager;->refList:Ljava/util/ArrayList;

    if-eqz v0, :cond_0

    .line 66
    invoke-virtual {v0}, Ljava/util/ArrayList;->clear()V

    .line 67
    :cond_0
    return-void
.end method
