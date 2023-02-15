.class public Landroid/runtime/XmlReaderResourceParser;
.super Landroid/runtime/XmlReaderPullParser;
.source "XmlReaderResourceParser.java"

# interfaces
.implements Lmono/android/IGCUserPeer;
.implements Landroid/content/res/XmlResourceParser;
.implements Landroid/util/AttributeSet;
.implements Lorg/xmlpull/v1/XmlPullParser;


# static fields
.field public static final __md_methods:Ljava/lang/String;


# instance fields
.field private refList:Ljava/util/ArrayList;


# direct methods
.method static constructor <clinit>()V
    .locals 3

    .line 15
    const-string v0, "n_close:()V:GetCloseHandler:Android.Content.Res.IXmlResourceParserInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\nn_getAttributeNamespace:(I)Ljava/lang/String;:GetGetAttributeNamespace_IHandler:Android.Content.Res.IXmlResourceParserInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\nn_getAttributeCount:()I:GetGetAttributeCountHandler:Android.Util.IAttributeSetInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\nn_getClassAttribute:()Ljava/lang/String;:GetGetClassAttributeHandler:Android.Util.IAttributeSetInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\nn_getIdAttribute:()Ljava/lang/String;:GetGetIdAttributeHandler:Android.Util.IAttributeSetInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\nn_getPositionDescription:()Ljava/lang/String;:GetGetPositionDescriptionHandler:Android.Util.IAttributeSetInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\nn_getStyleAttribute:()I:GetGetStyleAttributeHandler:Android.Util.IAttributeSetInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\nn_getAttributeBooleanValue:(IZ)Z:GetGetAttributeBooleanValue_IZHandler:Android.Util.IAttributeSetInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\nn_getAttributeBooleanValue:(Ljava/lang/String;Ljava/lang/String;Z)Z:GetGetAttributeBooleanValue_Ljava_lang_String_Ljava_lang_String_ZHandler:Android.Util.IAttributeSetInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\nn_getAttributeFloatValue:(IF)F:GetGetAttributeFloatValue_IFHandler:Android.Util.IAttributeSetInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\nn_getAttributeFloatValue:(Ljava/lang/String;Ljava/lang/String;F)F:GetGetAttributeFloatValue_Ljava_lang_String_Ljava_lang_String_FHandler:Android.Util.IAttributeSetInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\nn_getAttributeIntValue:(II)I:GetGetAttributeIntValue_IIHandler:Android.Util.IAttributeSetInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\nn_getAttributeIntValue:(Ljava/lang/String;Ljava/lang/String;I)I:GetGetAttributeIntValue_Ljava_lang_String_Ljava_lang_String_IHandler:Android.Util.IAttributeSetInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\nn_getAttributeListValue:(I[Ljava/lang/String;I)I:GetGetAttributeListValue_IarrayLjava_lang_String_IHandler:Android.Util.IAttributeSetInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\nn_getAttributeListValue:(Ljava/lang/String;Ljava/lang/String;[Ljava/lang/String;I)I:GetGetAttributeListValue_Ljava_lang_String_Ljava_lang_String_arrayLjava_lang_String_IHandler:Android.Util.IAttributeSetInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\nn_getAttributeName:(I)Ljava/lang/String;:GetGetAttributeName_IHandler:Android.Util.IAttributeSetInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\nn_getAttributeNameResource:(I)I:GetGetAttributeNameResource_IHandler:Android.Util.IAttributeSetInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\nn_getAttributeResourceValue:(II)I:GetGetAttributeResourceValue_IIHandler:Android.Util.IAttributeSetInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\nn_getAttributeResourceValue:(Ljava/lang/String;Ljava/lang/String;I)I:GetGetAttributeResourceValue_Ljava_lang_String_Ljava_lang_String_IHandler:Android.Util.IAttributeSetInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\nn_getAttributeUnsignedIntValue:(II)I:GetGetAttributeUnsignedIntValue_IIHandler:Android.Util.IAttributeSetInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\nn_getAttributeUnsignedIntValue:(Ljava/lang/String;Ljava/lang/String;I)I:GetGetAttributeUnsignedIntValue_Ljava_lang_String_Ljava_lang_String_IHandler:Android.Util.IAttributeSetInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\nn_getAttributeValue:(I)Ljava/lang/String;:GetGetAttributeValue_IHandler:Android.Util.IAttributeSetInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\nn_getAttributeValue:(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;:GetGetAttributeValue_Ljava_lang_String_Ljava_lang_String_Handler:Android.Util.IAttributeSetInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\nn_getIdAttributeResourceValue:(I)I:GetGetIdAttributeResourceValue_IHandler:Android.Util.IAttributeSetInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\nn_getColumnNumber:()I:GetGetColumnNumberHandler:Org.XmlPull.V1.IXmlPullParserInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\nn_getDepth:()I:GetGetDepthHandler:Org.XmlPull.V1.IXmlPullParserInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\nn_getEventType:()I:GetGetEventTypeHandler:Org.XmlPull.V1.IXmlPullParserInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\nn_getInputEncoding:()Ljava/lang/String;:GetGetInputEncodingHandler:Org.XmlPull.V1.IXmlPullParserInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\nn_isEmptyElementTag:()Z:GetIsEmptyElementTagHandler:Org.XmlPull.V1.IXmlPullParserInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\nn_isWhitespace:()Z:GetIsWhitespaceHandler:Org.XmlPull.V1.IXmlPullParserInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\nn_getLineNumber:()I:GetGetLineNumberHandler:Org.XmlPull.V1.IXmlPullParserInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\nn_getName:()Ljava/lang/String;:GetGetNameHandler:Org.XmlPull.V1.IXmlPullParserInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\nn_getNamespace:()Ljava/lang/String;:GetGetNamespaceHandler:Org.XmlPull.V1.IXmlPullParserInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\nn_getPrefix:()Ljava/lang/String;:GetGetPrefixHandler:Org.XmlPull.V1.IXmlPullParserInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\nn_getText:()Ljava/lang/String;:GetGetTextHandler:Org.XmlPull.V1.IXmlPullParserInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\nn_defineEntityReplacementText:(Ljava/lang/String;Ljava/lang/String;)V:GetDefineEntityReplacementText_Ljava_lang_String_Ljava_lang_String_Handler:Org.XmlPull.V1.IXmlPullParserInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\nn_getAttributePrefix:(I)Ljava/lang/String;:GetGetAttributePrefix_IHandler:Org.XmlPull.V1.IXmlPullParserInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\nn_getAttributeType:(I)Ljava/lang/String;:GetGetAttributeType_IHandler:Org.XmlPull.V1.IXmlPullParserInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\nn_getFeature:(Ljava/lang/String;)Z:GetGetFeature_Ljava_lang_String_Handler:Org.XmlPull.V1.IXmlPullParserInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\nn_getNamespace:(Ljava/lang/String;)Ljava/lang/String;:GetGetNamespace_Ljava_lang_String_Handler:Org.XmlPull.V1.IXmlPullParserInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\nn_getNamespaceCount:(I)I:GetGetNamespaceCount_IHandler:Org.XmlPull.V1.IXmlPullParserInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\nn_getNamespacePrefix:(I)Ljava/lang/String;:GetGetNamespacePrefix_IHandler:Org.XmlPull.V1.IXmlPullParserInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\nn_getNamespaceUri:(I)Ljava/lang/String;:GetGetNamespaceUri_IHandler:Org.XmlPull.V1.IXmlPullParserInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\nn_getProperty:(Ljava/lang/String;)Ljava/lang/Object;:GetGetProperty_Ljava_lang_String_Handler:Org.XmlPull.V1.IXmlPullParserInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\nn_getTextCharacters:([I)[C:GetGetTextCharacters_arrayIHandler:Org.XmlPull.V1.IXmlPullParserInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\nn_isAttributeDefault:(I)Z:GetIsAttributeDefault_IHandler:Org.XmlPull.V1.IXmlPullParserInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\nn_next:()I:GetNextHandler:Org.XmlPull.V1.IXmlPullParserInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\nn_nextTag:()I:GetNextTagHandler:Org.XmlPull.V1.IXmlPullParserInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\nn_nextText:()Ljava/lang/String;:GetNextTextHandler:Org.XmlPull.V1.IXmlPullParserInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\nn_nextToken:()I:GetNextTokenHandler:Org.XmlPull.V1.IXmlPullParserInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\nn_require:(ILjava/lang/String;Ljava/lang/String;)V:GetRequire_ILjava_lang_String_Ljava_lang_String_Handler:Org.XmlPull.V1.IXmlPullParserInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\nn_setFeature:(Ljava/lang/String;Z)V:GetSetFeature_Ljava_lang_String_ZHandler:Org.XmlPull.V1.IXmlPullParserInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\nn_setInput:(Ljava/io/InputStream;Ljava/lang/String;)V:GetSetInput_Ljava_io_InputStream_Ljava_lang_String_Handler:Org.XmlPull.V1.IXmlPullParserInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\nn_setInput:(Ljava/io/Reader;)V:GetSetInput_Ljava_io_Reader_Handler:Org.XmlPull.V1.IXmlPullParserInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\nn_setProperty:(Ljava/lang/String;Ljava/lang/Object;)V:GetSetProperty_Ljava_lang_String_Ljava_lang_Object_Handler:Org.XmlPull.V1.IXmlPullParserInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n"

    sput-object v0, Landroid/runtime/XmlReaderResourceParser;->__md_methods:Ljava/lang/String;

    .line 72
    const-class v1, Landroid/runtime/XmlReaderResourceParser;

    const-string v2, "Android.Runtime.XmlReaderResourceParser, Mono.Android"

    invoke-static {v2, v1, v0}, Lmono/android/Runtime;->register(Ljava/lang/String;Ljava/lang/Class;Ljava/lang/String;)V

    .line 73
    return-void
.end method

.method public constructor <init>()V
    .locals 3

    .line 78
    invoke-direct {p0}, Landroid/runtime/XmlReaderPullParser;-><init>()V

    .line 79
    invoke-virtual {p0}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    move-result-object v0

    const-class v1, Landroid/runtime/XmlReaderResourceParser;

    if-ne v0, v1, :cond_0

    .line 80
    const/4 v0, 0x0

    new-array v0, v0, [Ljava/lang/Object;

    const-string v1, "Android.Runtime.XmlReaderResourceParser, Mono.Android"

    const-string v2, ""

    invoke-static {v1, v2, p0, v0}, Lmono/android/TypeManager;->Activate(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Object;[Ljava/lang/Object;)V

    .line 82
    :cond_0
    return-void
.end method

.method private native n_close()V
.end method

.method private native n_defineEntityReplacementText(Ljava/lang/String;Ljava/lang/String;)V
.end method

.method private native n_getAttributeBooleanValue(IZ)Z
.end method

.method private native n_getAttributeBooleanValue(Ljava/lang/String;Ljava/lang/String;Z)Z
.end method

.method private native n_getAttributeCount()I
.end method

.method private native n_getAttributeFloatValue(IF)F
.end method

.method private native n_getAttributeFloatValue(Ljava/lang/String;Ljava/lang/String;F)F
.end method

.method private native n_getAttributeIntValue(II)I
.end method

.method private native n_getAttributeIntValue(Ljava/lang/String;Ljava/lang/String;I)I
.end method

.method private native n_getAttributeListValue(I[Ljava/lang/String;I)I
.end method

.method private native n_getAttributeListValue(Ljava/lang/String;Ljava/lang/String;[Ljava/lang/String;I)I
.end method

.method private native n_getAttributeName(I)Ljava/lang/String;
.end method

.method private native n_getAttributeNameResource(I)I
.end method

.method private native n_getAttributeNamespace(I)Ljava/lang/String;
.end method

.method private native n_getAttributePrefix(I)Ljava/lang/String;
.end method

.method private native n_getAttributeResourceValue(II)I
.end method

.method private native n_getAttributeResourceValue(Ljava/lang/String;Ljava/lang/String;I)I
.end method

.method private native n_getAttributeType(I)Ljava/lang/String;
.end method

.method private native n_getAttributeUnsignedIntValue(II)I
.end method

.method private native n_getAttributeUnsignedIntValue(Ljava/lang/String;Ljava/lang/String;I)I
.end method

.method private native n_getAttributeValue(I)Ljava/lang/String;
.end method

.method private native n_getAttributeValue(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;
.end method

.method private native n_getClassAttribute()Ljava/lang/String;
.end method

.method private native n_getColumnNumber()I
.end method

.method private native n_getDepth()I
.end method

.method private native n_getEventType()I
.end method

.method private native n_getFeature(Ljava/lang/String;)Z
.end method

.method private native n_getIdAttribute()Ljava/lang/String;
.end method

.method private native n_getIdAttributeResourceValue(I)I
.end method

.method private native n_getInputEncoding()Ljava/lang/String;
.end method

.method private native n_getLineNumber()I
.end method

.method private native n_getName()Ljava/lang/String;
.end method

.method private native n_getNamespace()Ljava/lang/String;
.end method

.method private native n_getNamespace(Ljava/lang/String;)Ljava/lang/String;
.end method

.method private native n_getNamespaceCount(I)I
.end method

.method private native n_getNamespacePrefix(I)Ljava/lang/String;
.end method

.method private native n_getNamespaceUri(I)Ljava/lang/String;
.end method

.method private native n_getPositionDescription()Ljava/lang/String;
.end method

.method private native n_getPrefix()Ljava/lang/String;
.end method

.method private native n_getProperty(Ljava/lang/String;)Ljava/lang/Object;
.end method

.method private native n_getStyleAttribute()I
.end method

.method private native n_getText()Ljava/lang/String;
.end method

.method private native n_getTextCharacters([I)[C
.end method

.method private native n_isAttributeDefault(I)Z
.end method

.method private native n_isEmptyElementTag()Z
.end method

.method private native n_isWhitespace()Z
.end method

.method private native n_next()I
.end method

.method private native n_nextTag()I
.end method

.method private native n_nextText()Ljava/lang/String;
.end method

.method private native n_nextToken()I
.end method

.method private native n_require(ILjava/lang/String;Ljava/lang/String;)V
.end method

.method private native n_setFeature(Ljava/lang/String;Z)V
.end method

.method private native n_setInput(Ljava/io/InputStream;Ljava/lang/String;)V
.end method

.method private native n_setInput(Ljava/io/Reader;)V
.end method

.method private native n_setProperty(Ljava/lang/String;Ljava/lang/Object;)V
.end method


# virtual methods
.method public close()V
    .locals 0

    .line 87
    invoke-direct {p0}, Landroid/runtime/XmlReaderResourceParser;->n_close()V

    .line 88
    return-void
.end method

.method public defineEntityReplacementText(Ljava/lang/String;Ljava/lang/String;)V
    .locals 0

    .line 367
    invoke-direct {p0, p1, p2}, Landroid/runtime/XmlReaderResourceParser;->n_defineEntityReplacementText(Ljava/lang/String;Ljava/lang/String;)V

    .line 368
    return-void
.end method

.method public getAttributeBooleanValue(IZ)Z
    .locals 0

    .line 143
    invoke-direct {p0, p1, p2}, Landroid/runtime/XmlReaderResourceParser;->n_getAttributeBooleanValue(IZ)Z

    move-result p1

    return p1
.end method

.method public getAttributeBooleanValue(Ljava/lang/String;Ljava/lang/String;Z)Z
    .locals 0

    .line 151
    invoke-direct {p0, p1, p2, p3}, Landroid/runtime/XmlReaderResourceParser;->n_getAttributeBooleanValue(Ljava/lang/String;Ljava/lang/String;Z)Z

    move-result p1

    return p1
.end method

.method public getAttributeCount()I
    .locals 1

    .line 103
    invoke-direct {p0}, Landroid/runtime/XmlReaderResourceParser;->n_getAttributeCount()I

    move-result v0

    return v0
.end method

.method public getAttributeFloatValue(IF)F
    .locals 0

    .line 159
    invoke-direct {p0, p1, p2}, Landroid/runtime/XmlReaderResourceParser;->n_getAttributeFloatValue(IF)F

    move-result p1

    return p1
.end method

.method public getAttributeFloatValue(Ljava/lang/String;Ljava/lang/String;F)F
    .locals 0

    .line 167
    invoke-direct {p0, p1, p2, p3}, Landroid/runtime/XmlReaderResourceParser;->n_getAttributeFloatValue(Ljava/lang/String;Ljava/lang/String;F)F

    move-result p1

    return p1
.end method

.method public getAttributeIntValue(II)I
    .locals 0

    .line 175
    invoke-direct {p0, p1, p2}, Landroid/runtime/XmlReaderResourceParser;->n_getAttributeIntValue(II)I

    move-result p1

    return p1
.end method

.method public getAttributeIntValue(Ljava/lang/String;Ljava/lang/String;I)I
    .locals 0

    .line 183
    invoke-direct {p0, p1, p2, p3}, Landroid/runtime/XmlReaderResourceParser;->n_getAttributeIntValue(Ljava/lang/String;Ljava/lang/String;I)I

    move-result p1

    return p1
.end method

.method public getAttributeListValue(I[Ljava/lang/String;I)I
    .locals 0

    .line 191
    invoke-direct {p0, p1, p2, p3}, Landroid/runtime/XmlReaderResourceParser;->n_getAttributeListValue(I[Ljava/lang/String;I)I

    move-result p1

    return p1
.end method

.method public getAttributeListValue(Ljava/lang/String;Ljava/lang/String;[Ljava/lang/String;I)I
    .locals 0

    .line 199
    invoke-direct {p0, p1, p2, p3, p4}, Landroid/runtime/XmlReaderResourceParser;->n_getAttributeListValue(Ljava/lang/String;Ljava/lang/String;[Ljava/lang/String;I)I

    move-result p1

    return p1
.end method

.method public getAttributeName(I)Ljava/lang/String;
    .locals 0

    .line 207
    invoke-direct {p0, p1}, Landroid/runtime/XmlReaderResourceParser;->n_getAttributeName(I)Ljava/lang/String;

    move-result-object p1

    return-object p1
.end method

.method public getAttributeNameResource(I)I
    .locals 0

    .line 215
    invoke-direct {p0, p1}, Landroid/runtime/XmlReaderResourceParser;->n_getAttributeNameResource(I)I

    move-result p1

    return p1
.end method

.method public getAttributeNamespace(I)Ljava/lang/String;
    .locals 0

    .line 95
    invoke-direct {p0, p1}, Landroid/runtime/XmlReaderResourceParser;->n_getAttributeNamespace(I)Ljava/lang/String;

    move-result-object p1

    return-object p1
.end method

.method public getAttributePrefix(I)Ljava/lang/String;
    .locals 0

    .line 375
    invoke-direct {p0, p1}, Landroid/runtime/XmlReaderResourceParser;->n_getAttributePrefix(I)Ljava/lang/String;

    move-result-object p1

    return-object p1
.end method

.method public getAttributeResourceValue(II)I
    .locals 0

    .line 223
    invoke-direct {p0, p1, p2}, Landroid/runtime/XmlReaderResourceParser;->n_getAttributeResourceValue(II)I

    move-result p1

    return p1
.end method

.method public getAttributeResourceValue(Ljava/lang/String;Ljava/lang/String;I)I
    .locals 0

    .line 231
    invoke-direct {p0, p1, p2, p3}, Landroid/runtime/XmlReaderResourceParser;->n_getAttributeResourceValue(Ljava/lang/String;Ljava/lang/String;I)I

    move-result p1

    return p1
.end method

.method public getAttributeType(I)Ljava/lang/String;
    .locals 0

    .line 383
    invoke-direct {p0, p1}, Landroid/runtime/XmlReaderResourceParser;->n_getAttributeType(I)Ljava/lang/String;

    move-result-object p1

    return-object p1
.end method

.method public getAttributeUnsignedIntValue(II)I
    .locals 0

    .line 239
    invoke-direct {p0, p1, p2}, Landroid/runtime/XmlReaderResourceParser;->n_getAttributeUnsignedIntValue(II)I

    move-result p1

    return p1
.end method

.method public getAttributeUnsignedIntValue(Ljava/lang/String;Ljava/lang/String;I)I
    .locals 0

    .line 247
    invoke-direct {p0, p1, p2, p3}, Landroid/runtime/XmlReaderResourceParser;->n_getAttributeUnsignedIntValue(Ljava/lang/String;Ljava/lang/String;I)I

    move-result p1

    return p1
.end method

.method public getAttributeValue(I)Ljava/lang/String;
    .locals 0

    .line 255
    invoke-direct {p0, p1}, Landroid/runtime/XmlReaderResourceParser;->n_getAttributeValue(I)Ljava/lang/String;

    move-result-object p1

    return-object p1
.end method

.method public getAttributeValue(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;
    .locals 0

    .line 263
    invoke-direct {p0, p1, p2}, Landroid/runtime/XmlReaderResourceParser;->n_getAttributeValue(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object p1

    return-object p1
.end method

.method public getClassAttribute()Ljava/lang/String;
    .locals 1

    .line 111
    invoke-direct {p0}, Landroid/runtime/XmlReaderResourceParser;->n_getClassAttribute()Ljava/lang/String;

    move-result-object v0

    return-object v0
.end method

.method public getColumnNumber()I
    .locals 1

    .line 279
    invoke-direct {p0}, Landroid/runtime/XmlReaderResourceParser;->n_getColumnNumber()I

    move-result v0

    return v0
.end method

.method public getDepth()I
    .locals 1

    .line 287
    invoke-direct {p0}, Landroid/runtime/XmlReaderResourceParser;->n_getDepth()I

    move-result v0

    return v0
.end method

.method public getEventType()I
    .locals 1

    .line 295
    invoke-direct {p0}, Landroid/runtime/XmlReaderResourceParser;->n_getEventType()I

    move-result v0

    return v0
.end method

.method public getFeature(Ljava/lang/String;)Z
    .locals 0

    .line 391
    invoke-direct {p0, p1}, Landroid/runtime/XmlReaderResourceParser;->n_getFeature(Ljava/lang/String;)Z

    move-result p1

    return p1
.end method

.method public getIdAttribute()Ljava/lang/String;
    .locals 1

    .line 119
    invoke-direct {p0}, Landroid/runtime/XmlReaderResourceParser;->n_getIdAttribute()Ljava/lang/String;

    move-result-object v0

    return-object v0
.end method

.method public getIdAttributeResourceValue(I)I
    .locals 0

    .line 271
    invoke-direct {p0, p1}, Landroid/runtime/XmlReaderResourceParser;->n_getIdAttributeResourceValue(I)I

    move-result p1

    return p1
.end method

.method public getInputEncoding()Ljava/lang/String;
    .locals 1

    .line 303
    invoke-direct {p0}, Landroid/runtime/XmlReaderResourceParser;->n_getInputEncoding()Ljava/lang/String;

    move-result-object v0

    return-object v0
.end method

.method public getLineNumber()I
    .locals 1

    .line 327
    invoke-direct {p0}, Landroid/runtime/XmlReaderResourceParser;->n_getLineNumber()I

    move-result v0

    return v0
.end method

.method public getName()Ljava/lang/String;
    .locals 1

    .line 335
    invoke-direct {p0}, Landroid/runtime/XmlReaderResourceParser;->n_getName()Ljava/lang/String;

    move-result-object v0

    return-object v0
.end method

.method public getNamespace()Ljava/lang/String;
    .locals 1

    .line 343
    invoke-direct {p0}, Landroid/runtime/XmlReaderResourceParser;->n_getNamespace()Ljava/lang/String;

    move-result-object v0

    return-object v0
.end method

.method public getNamespace(Ljava/lang/String;)Ljava/lang/String;
    .locals 0

    .line 399
    invoke-direct {p0, p1}, Landroid/runtime/XmlReaderResourceParser;->n_getNamespace(Ljava/lang/String;)Ljava/lang/String;

    move-result-object p1

    return-object p1
.end method

.method public getNamespaceCount(I)I
    .locals 0

    .line 407
    invoke-direct {p0, p1}, Landroid/runtime/XmlReaderResourceParser;->n_getNamespaceCount(I)I

    move-result p1

    return p1
.end method

.method public getNamespacePrefix(I)Ljava/lang/String;
    .locals 0

    .line 415
    invoke-direct {p0, p1}, Landroid/runtime/XmlReaderResourceParser;->n_getNamespacePrefix(I)Ljava/lang/String;

    move-result-object p1

    return-object p1
.end method

.method public getNamespaceUri(I)Ljava/lang/String;
    .locals 0

    .line 423
    invoke-direct {p0, p1}, Landroid/runtime/XmlReaderResourceParser;->n_getNamespaceUri(I)Ljava/lang/String;

    move-result-object p1

    return-object p1
.end method

.method public getPositionDescription()Ljava/lang/String;
    .locals 1

    .line 127
    invoke-direct {p0}, Landroid/runtime/XmlReaderResourceParser;->n_getPositionDescription()Ljava/lang/String;

    move-result-object v0

    return-object v0
.end method

.method public getPrefix()Ljava/lang/String;
    .locals 1

    .line 351
    invoke-direct {p0}, Landroid/runtime/XmlReaderResourceParser;->n_getPrefix()Ljava/lang/String;

    move-result-object v0

    return-object v0
.end method

.method public getProperty(Ljava/lang/String;)Ljava/lang/Object;
    .locals 0

    .line 431
    invoke-direct {p0, p1}, Landroid/runtime/XmlReaderResourceParser;->n_getProperty(Ljava/lang/String;)Ljava/lang/Object;

    move-result-object p1

    return-object p1
.end method

.method public getStyleAttribute()I
    .locals 1

    .line 135
    invoke-direct {p0}, Landroid/runtime/XmlReaderResourceParser;->n_getStyleAttribute()I

    move-result v0

    return v0
.end method

.method public getText()Ljava/lang/String;
    .locals 1

    .line 359
    invoke-direct {p0}, Landroid/runtime/XmlReaderResourceParser;->n_getText()Ljava/lang/String;

    move-result-object v0

    return-object v0
.end method

.method public getTextCharacters([I)[C
    .locals 0

    .line 439
    invoke-direct {p0, p1}, Landroid/runtime/XmlReaderResourceParser;->n_getTextCharacters([I)[C

    move-result-object p1

    return-object p1
.end method

.method public isAttributeDefault(I)Z
    .locals 0

    .line 447
    invoke-direct {p0, p1}, Landroid/runtime/XmlReaderResourceParser;->n_isAttributeDefault(I)Z

    move-result p1

    return p1
.end method

.method public isEmptyElementTag()Z
    .locals 1

    .line 311
    invoke-direct {p0}, Landroid/runtime/XmlReaderResourceParser;->n_isEmptyElementTag()Z

    move-result v0

    return v0
.end method

.method public isWhitespace()Z
    .locals 1

    .line 319
    invoke-direct {p0}, Landroid/runtime/XmlReaderResourceParser;->n_isWhitespace()Z

    move-result v0

    return v0
.end method

.method public monodroidAddReference(Ljava/lang/Object;)V
    .locals 1

    .line 527
    iget-object v0, p0, Landroid/runtime/XmlReaderResourceParser;->refList:Ljava/util/ArrayList;

    if-nez v0, :cond_0

    .line 528
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    iput-object v0, p0, Landroid/runtime/XmlReaderResourceParser;->refList:Ljava/util/ArrayList;

    .line 529
    :cond_0
    iget-object v0, p0, Landroid/runtime/XmlReaderResourceParser;->refList:Ljava/util/ArrayList;

    invoke-virtual {v0, p1}, Ljava/util/ArrayList;->add(Ljava/lang/Object;)Z

    .line 530
    return-void
.end method

.method public monodroidClearReferences()V
    .locals 1

    .line 534
    iget-object v0, p0, Landroid/runtime/XmlReaderResourceParser;->refList:Ljava/util/ArrayList;

    if-eqz v0, :cond_0

    .line 535
    invoke-virtual {v0}, Ljava/util/ArrayList;->clear()V

    .line 536
    :cond_0
    return-void
.end method

.method public next()I
    .locals 1

    .line 455
    invoke-direct {p0}, Landroid/runtime/XmlReaderResourceParser;->n_next()I

    move-result v0

    return v0
.end method

.method public nextTag()I
    .locals 1

    .line 463
    invoke-direct {p0}, Landroid/runtime/XmlReaderResourceParser;->n_nextTag()I

    move-result v0

    return v0
.end method

.method public nextText()Ljava/lang/String;
    .locals 1

    .line 471
    invoke-direct {p0}, Landroid/runtime/XmlReaderResourceParser;->n_nextText()Ljava/lang/String;

    move-result-object v0

    return-object v0
.end method

.method public nextToken()I
    .locals 1

    .line 479
    invoke-direct {p0}, Landroid/runtime/XmlReaderResourceParser;->n_nextToken()I

    move-result v0

    return v0
.end method

.method public require(ILjava/lang/String;Ljava/lang/String;)V
    .locals 0

    .line 487
    invoke-direct {p0, p1, p2, p3}, Landroid/runtime/XmlReaderResourceParser;->n_require(ILjava/lang/String;Ljava/lang/String;)V

    .line 488
    return-void
.end method

.method public setFeature(Ljava/lang/String;Z)V
    .locals 0

    .line 495
    invoke-direct {p0, p1, p2}, Landroid/runtime/XmlReaderResourceParser;->n_setFeature(Ljava/lang/String;Z)V

    .line 496
    return-void
.end method

.method public setInput(Ljava/io/InputStream;Ljava/lang/String;)V
    .locals 0

    .line 503
    invoke-direct {p0, p1, p2}, Landroid/runtime/XmlReaderResourceParser;->n_setInput(Ljava/io/InputStream;Ljava/lang/String;)V

    .line 504
    return-void
.end method

.method public setInput(Ljava/io/Reader;)V
    .locals 0

    .line 511
    invoke-direct {p0, p1}, Landroid/runtime/XmlReaderResourceParser;->n_setInput(Ljava/io/Reader;)V

    .line 512
    return-void
.end method

.method public setProperty(Ljava/lang/String;Ljava/lang/Object;)V
    .locals 0

    .line 519
    invoke-direct {p0, p1, p2}, Landroid/runtime/XmlReaderResourceParser;->n_setProperty(Ljava/lang/String;Ljava/lang/Object;)V

    .line 520
    return-void
.end method
