.class public Lxamarin/android/net/ServerCertificateCustomValidator_TrustManager_FakeSSLSession;
.super Ljava/lang/Object;
.source "ServerCertificateCustomValidator_TrustManager_FakeSSLSession.java"

# interfaces
.implements Lmono/android/IGCUserPeer;
.implements Ljavax/net/ssl/SSLSession;


# static fields
.field public static final __md_methods:Ljava/lang/String;


# instance fields
.field private refList:Ljava/util/ArrayList;


# direct methods
.method static constructor <clinit>()V
    .locals 3

    .line 13
    const-string v0, "n_getApplicationBufferSize:()I:GetGetApplicationBufferSizeHandler:Javax.Net.Ssl.ISSLSessionInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\nn_getCipherSuite:()Ljava/lang/String;:GetGetCipherSuiteHandler:Javax.Net.Ssl.ISSLSessionInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\nn_getCreationTime:()J:GetGetCreationTimeHandler:Javax.Net.Ssl.ISSLSessionInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\nn_isValid:()Z:GetIsValidHandler:Javax.Net.Ssl.ISSLSessionInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\nn_getLastAccessedTime:()J:GetGetLastAccessedTimeHandler:Javax.Net.Ssl.ISSLSessionInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\nn_getLocalPrincipal:()Ljava/security/Principal;:GetGetLocalPrincipalHandler:Javax.Net.Ssl.ISSLSessionInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\nn_getPacketBufferSize:()I:GetGetPacketBufferSizeHandler:Javax.Net.Ssl.ISSLSessionInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\nn_getPeerHost:()Ljava/lang/String;:GetGetPeerHostHandler:Javax.Net.Ssl.ISSLSessionInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\nn_getPeerPort:()I:GetGetPeerPortHandler:Javax.Net.Ssl.ISSLSessionInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\nn_getPeerPrincipal:()Ljava/security/Principal;:GetGetPeerPrincipalHandler:Javax.Net.Ssl.ISSLSessionInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\nn_getProtocol:()Ljava/lang/String;:GetGetProtocolHandler:Javax.Net.Ssl.ISSLSessionInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\nn_getSessionContext:()Ljavax/net/ssl/SSLSessionContext;:GetGetSessionContextHandler:Javax.Net.Ssl.ISSLSessionInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\nn_getId:()[B:GetGetIdHandler:Javax.Net.Ssl.ISSLSessionInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\nn_getLocalCertificates:()[Ljava/security/cert/Certificate;:GetGetLocalCertificatesHandler:Javax.Net.Ssl.ISSLSessionInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\nn_getPeerCertificateChain:()[Ljavax/security/cert/X509Certificate;:GetGetPeerCertificateChainHandler:Javax.Net.Ssl.ISSLSessionInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\nn_getPeerCertificates:()[Ljava/security/cert/Certificate;:GetGetPeerCertificatesHandler:Javax.Net.Ssl.ISSLSessionInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\nn_getValue:(Ljava/lang/String;)Ljava/lang/Object;:GetGetValue_Ljava_lang_String_Handler:Javax.Net.Ssl.ISSLSessionInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\nn_getValueNames:()[Ljava/lang/String;:GetGetValueNamesHandler:Javax.Net.Ssl.ISSLSessionInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\nn_invalidate:()V:GetInvalidateHandler:Javax.Net.Ssl.ISSLSessionInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\nn_putValue:(Ljava/lang/String;Ljava/lang/Object;)V:GetPutValue_Ljava_lang_String_Ljava_lang_Object_Handler:Javax.Net.Ssl.ISSLSessionInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\nn_removeValue:(Ljava/lang/String;)V:GetRemoveValue_Ljava_lang_String_Handler:Javax.Net.Ssl.ISSLSessionInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n"

    sput-object v0, Lxamarin/android/net/ServerCertificateCustomValidator_TrustManager_FakeSSLSession;->__md_methods:Ljava/lang/String;

    .line 36
    const-class v1, Lxamarin/android/net/ServerCertificateCustomValidator_TrustManager_FakeSSLSession;

    const-string v2, "Xamarin.Android.Net.ServerCertificateCustomValidator+TrustManager+FakeSSLSession, Mono.Android"

    invoke-static {v2, v1, v0}, Lmono/android/Runtime;->register(Ljava/lang/String;Ljava/lang/Class;Ljava/lang/String;)V

    .line 37
    return-void
.end method

.method public constructor <init>()V
    .locals 3

    .line 42
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 43
    invoke-virtual {p0}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    move-result-object v0

    const-class v1, Lxamarin/android/net/ServerCertificateCustomValidator_TrustManager_FakeSSLSession;

    if-ne v0, v1, :cond_0

    .line 44
    const/4 v0, 0x0

    new-array v0, v0, [Ljava/lang/Object;

    const-string v1, "Xamarin.Android.Net.ServerCertificateCustomValidator+TrustManager+FakeSSLSession, Mono.Android"

    const-string v2, ""

    invoke-static {v1, v2, p0, v0}, Lmono/android/TypeManager;->Activate(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Object;[Ljava/lang/Object;)V

    .line 46
    :cond_0
    return-void
.end method

.method public constructor <init>([Ljava/security/cert/X509Certificate;)V
    .locals 2

    .line 50
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 51
    invoke-virtual {p0}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    move-result-object v0

    const-class v1, Lxamarin/android/net/ServerCertificateCustomValidator_TrustManager_FakeSSLSession;

    if-ne v0, v1, :cond_0

    .line 52
    const/4 v0, 0x1

    new-array v0, v0, [Ljava/lang/Object;

    const/4 v1, 0x0

    aput-object p1, v0, v1

    const-string p1, "Xamarin.Android.Net.ServerCertificateCustomValidator+TrustManager+FakeSSLSession, Mono.Android"

    const-string v1, "Java.Security.Cert.X509Certificate[], Mono.Android"

    invoke-static {p1, v1, p0, v0}, Lmono/android/TypeManager;->Activate(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Object;[Ljava/lang/Object;)V

    .line 54
    :cond_0
    return-void
.end method

.method private native n_getApplicationBufferSize()I
.end method

.method private native n_getCipherSuite()Ljava/lang/String;
.end method

.method private native n_getCreationTime()J
.end method

.method private native n_getId()[B
.end method

.method private native n_getLastAccessedTime()J
.end method

.method private native n_getLocalCertificates()[Ljava/security/cert/Certificate;
.end method

.method private native n_getLocalPrincipal()Ljava/security/Principal;
.end method

.method private native n_getPacketBufferSize()I
.end method

.method private native n_getPeerCertificateChain()[Ljavax/security/cert/X509Certificate;
.end method

.method private native n_getPeerCertificates()[Ljava/security/cert/Certificate;
.end method

.method private native n_getPeerHost()Ljava/lang/String;
.end method

.method private native n_getPeerPort()I
.end method

.method private native n_getPeerPrincipal()Ljava/security/Principal;
.end method

.method private native n_getProtocol()Ljava/lang/String;
.end method

.method private native n_getSessionContext()Ljavax/net/ssl/SSLSessionContext;
.end method

.method private native n_getValue(Ljava/lang/String;)Ljava/lang/Object;
.end method

.method private native n_getValueNames()[Ljava/lang/String;
.end method

.method private native n_invalidate()V
.end method

.method private native n_isValid()Z
.end method

.method private native n_putValue(Ljava/lang/String;Ljava/lang/Object;)V
.end method

.method private native n_removeValue(Ljava/lang/String;)V
.end method


# virtual methods
.method public getApplicationBufferSize()I
    .locals 1

    .line 59
    invoke-direct {p0}, Lxamarin/android/net/ServerCertificateCustomValidator_TrustManager_FakeSSLSession;->n_getApplicationBufferSize()I

    move-result v0

    return v0
.end method

.method public getCipherSuite()Ljava/lang/String;
    .locals 1

    .line 67
    invoke-direct {p0}, Lxamarin/android/net/ServerCertificateCustomValidator_TrustManager_FakeSSLSession;->n_getCipherSuite()Ljava/lang/String;

    move-result-object v0

    return-object v0
.end method

.method public getCreationTime()J
    .locals 2

    .line 75
    invoke-direct {p0}, Lxamarin/android/net/ServerCertificateCustomValidator_TrustManager_FakeSSLSession;->n_getCreationTime()J

    move-result-wide v0

    return-wide v0
.end method

.method public getId()[B
    .locals 1

    .line 155
    invoke-direct {p0}, Lxamarin/android/net/ServerCertificateCustomValidator_TrustManager_FakeSSLSession;->n_getId()[B

    move-result-object v0

    return-object v0
.end method

.method public getLastAccessedTime()J
    .locals 2

    .line 91
    invoke-direct {p0}, Lxamarin/android/net/ServerCertificateCustomValidator_TrustManager_FakeSSLSession;->n_getLastAccessedTime()J

    move-result-wide v0

    return-wide v0
.end method

.method public getLocalCertificates()[Ljava/security/cert/Certificate;
    .locals 1

    .line 163
    invoke-direct {p0}, Lxamarin/android/net/ServerCertificateCustomValidator_TrustManager_FakeSSLSession;->n_getLocalCertificates()[Ljava/security/cert/Certificate;

    move-result-object v0

    return-object v0
.end method

.method public getLocalPrincipal()Ljava/security/Principal;
    .locals 1

    .line 99
    invoke-direct {p0}, Lxamarin/android/net/ServerCertificateCustomValidator_TrustManager_FakeSSLSession;->n_getLocalPrincipal()Ljava/security/Principal;

    move-result-object v0

    return-object v0
.end method

.method public getPacketBufferSize()I
    .locals 1

    .line 107
    invoke-direct {p0}, Lxamarin/android/net/ServerCertificateCustomValidator_TrustManager_FakeSSLSession;->n_getPacketBufferSize()I

    move-result v0

    return v0
.end method

.method public getPeerCertificateChain()[Ljavax/security/cert/X509Certificate;
    .locals 1

    .line 171
    invoke-direct {p0}, Lxamarin/android/net/ServerCertificateCustomValidator_TrustManager_FakeSSLSession;->n_getPeerCertificateChain()[Ljavax/security/cert/X509Certificate;

    move-result-object v0

    return-object v0
.end method

.method public getPeerCertificates()[Ljava/security/cert/Certificate;
    .locals 1

    .line 179
    invoke-direct {p0}, Lxamarin/android/net/ServerCertificateCustomValidator_TrustManager_FakeSSLSession;->n_getPeerCertificates()[Ljava/security/cert/Certificate;

    move-result-object v0

    return-object v0
.end method

.method public getPeerHost()Ljava/lang/String;
    .locals 1

    .line 115
    invoke-direct {p0}, Lxamarin/android/net/ServerCertificateCustomValidator_TrustManager_FakeSSLSession;->n_getPeerHost()Ljava/lang/String;

    move-result-object v0

    return-object v0
.end method

.method public getPeerPort()I
    .locals 1

    .line 123
    invoke-direct {p0}, Lxamarin/android/net/ServerCertificateCustomValidator_TrustManager_FakeSSLSession;->n_getPeerPort()I

    move-result v0

    return v0
.end method

.method public getPeerPrincipal()Ljava/security/Principal;
    .locals 1

    .line 131
    invoke-direct {p0}, Lxamarin/android/net/ServerCertificateCustomValidator_TrustManager_FakeSSLSession;->n_getPeerPrincipal()Ljava/security/Principal;

    move-result-object v0

    return-object v0
.end method

.method public getProtocol()Ljava/lang/String;
    .locals 1

    .line 139
    invoke-direct {p0}, Lxamarin/android/net/ServerCertificateCustomValidator_TrustManager_FakeSSLSession;->n_getProtocol()Ljava/lang/String;

    move-result-object v0

    return-object v0
.end method

.method public getSessionContext()Ljavax/net/ssl/SSLSessionContext;
    .locals 1

    .line 147
    invoke-direct {p0}, Lxamarin/android/net/ServerCertificateCustomValidator_TrustManager_FakeSSLSession;->n_getSessionContext()Ljavax/net/ssl/SSLSessionContext;

    move-result-object v0

    return-object v0
.end method

.method public getValue(Ljava/lang/String;)Ljava/lang/Object;
    .locals 0

    .line 187
    invoke-direct {p0, p1}, Lxamarin/android/net/ServerCertificateCustomValidator_TrustManager_FakeSSLSession;->n_getValue(Ljava/lang/String;)Ljava/lang/Object;

    move-result-object p1

    return-object p1
.end method

.method public getValueNames()[Ljava/lang/String;
    .locals 1

    .line 195
    invoke-direct {p0}, Lxamarin/android/net/ServerCertificateCustomValidator_TrustManager_FakeSSLSession;->n_getValueNames()[Ljava/lang/String;

    move-result-object v0

    return-object v0
.end method

.method public invalidate()V
    .locals 0

    .line 203
    invoke-direct {p0}, Lxamarin/android/net/ServerCertificateCustomValidator_TrustManager_FakeSSLSession;->n_invalidate()V

    .line 204
    return-void
.end method

.method public isValid()Z
    .locals 1

    .line 83
    invoke-direct {p0}, Lxamarin/android/net/ServerCertificateCustomValidator_TrustManager_FakeSSLSession;->n_isValid()Z

    move-result v0

    return v0
.end method

.method public monodroidAddReference(Ljava/lang/Object;)V
    .locals 1

    .line 227
    iget-object v0, p0, Lxamarin/android/net/ServerCertificateCustomValidator_TrustManager_FakeSSLSession;->refList:Ljava/util/ArrayList;

    if-nez v0, :cond_0

    .line 228
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    iput-object v0, p0, Lxamarin/android/net/ServerCertificateCustomValidator_TrustManager_FakeSSLSession;->refList:Ljava/util/ArrayList;

    .line 229
    :cond_0
    iget-object v0, p0, Lxamarin/android/net/ServerCertificateCustomValidator_TrustManager_FakeSSLSession;->refList:Ljava/util/ArrayList;

    invoke-virtual {v0, p1}, Ljava/util/ArrayList;->add(Ljava/lang/Object;)Z

    .line 230
    return-void
.end method

.method public monodroidClearReferences()V
    .locals 1

    .line 234
    iget-object v0, p0, Lxamarin/android/net/ServerCertificateCustomValidator_TrustManager_FakeSSLSession;->refList:Ljava/util/ArrayList;

    if-eqz v0, :cond_0

    .line 235
    invoke-virtual {v0}, Ljava/util/ArrayList;->clear()V

    .line 236
    :cond_0
    return-void
.end method

.method public putValue(Ljava/lang/String;Ljava/lang/Object;)V
    .locals 0

    .line 211
    invoke-direct {p0, p1, p2}, Lxamarin/android/net/ServerCertificateCustomValidator_TrustManager_FakeSSLSession;->n_putValue(Ljava/lang/String;Ljava/lang/Object;)V

    .line 212
    return-void
.end method

.method public removeValue(Ljava/lang/String;)V
    .locals 0

    .line 219
    invoke-direct {p0, p1}, Lxamarin/android/net/ServerCertificateCustomValidator_TrustManager_FakeSSLSession;->n_removeValue(Ljava/lang/String;)V

    .line 220
    return-void
.end method
