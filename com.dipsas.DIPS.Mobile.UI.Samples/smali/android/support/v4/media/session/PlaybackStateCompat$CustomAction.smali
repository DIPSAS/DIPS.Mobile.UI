.class public final Landroid/support/v4/media/session/PlaybackStateCompat$CustomAction;
.super Ljava/lang/Object;
.source "PlaybackStateCompat.java"

# interfaces
.implements Landroid/os/Parcelable;


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Landroid/support/v4/media/session/PlaybackStateCompat;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x19
    name = "CustomAction"
.end annotation

.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Landroid/support/v4/media/session/PlaybackStateCompat$CustomAction$Builder;
    }
.end annotation


# static fields
.field public static final CREATOR:Landroid/os/Parcelable$Creator;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Landroid/os/Parcelable$Creator<",
            "Landroid/support/v4/media/session/PlaybackStateCompat$CustomAction;",
            ">;"
        }
    .end annotation
.end field


# instance fields
.field private final mAction:Ljava/lang/String;

.field private mCustomActionFwk:Landroid/media/session/PlaybackState$CustomAction;

.field private final mExtras:Landroid/os/Bundle;

.field private final mIcon:I

.field private final mName:Ljava/lang/CharSequence;


# direct methods
.method static constructor <clinit>()V
    .locals 1

    .line 981
    new-instance v0, Landroid/support/v4/media/session/PlaybackStateCompat$CustomAction$1;

    invoke-direct {v0}, Landroid/support/v4/media/session/PlaybackStateCompat$CustomAction$1;-><init>()V

    sput-object v0, Landroid/support/v4/media/session/PlaybackStateCompat$CustomAction;->CREATOR:Landroid/os/Parcelable$Creator;

    return-void
.end method

.method constructor <init>(Landroid/os/Parcel;)V
    .locals 1
    .param p1, "in"    # Landroid/os/Parcel;
    .annotation system Ldalvik/annotation/MethodParameters;
        accessFlags = {
            0x0
        }
        names = {
            "in"
        }
    .end annotation

    .line 910
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 911
    invoke-virtual {p1}, Landroid/os/Parcel;->readString()Ljava/lang/String;

    move-result-object v0

    iput-object v0, p0, Landroid/support/v4/media/session/PlaybackStateCompat$CustomAction;->mAction:Ljava/lang/String;

    .line 912
    sget-object v0, Landroid/text/TextUtils;->CHAR_SEQUENCE_CREATOR:Landroid/os/Parcelable$Creator;

    invoke-interface {v0, p1}, Landroid/os/Parcelable$Creator;->createFromParcel(Landroid/os/Parcel;)Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Ljava/lang/CharSequence;

    iput-object v0, p0, Landroid/support/v4/media/session/PlaybackStateCompat$CustomAction;->mName:Ljava/lang/CharSequence;

    .line 913
    invoke-virtual {p1}, Landroid/os/Parcel;->readInt()I

    move-result v0

    iput v0, p0, Landroid/support/v4/media/session/PlaybackStateCompat$CustomAction;->mIcon:I

    .line 914
    const-class v0, Landroid/support/v4/media/session/MediaSessionCompat;

    invoke-virtual {v0}, Ljava/lang/Class;->getClassLoader()Ljava/lang/ClassLoader;

    move-result-object v0

    invoke-virtual {p1, v0}, Landroid/os/Parcel;->readBundle(Ljava/lang/ClassLoader;)Landroid/os/Bundle;

    move-result-object v0

    iput-object v0, p0, Landroid/support/v4/media/session/PlaybackStateCompat$CustomAction;->mExtras:Landroid/os/Bundle;

    .line 915
    return-void
.end method

.method constructor <init>(Ljava/lang/String;Ljava/lang/CharSequence;ILandroid/os/Bundle;)V
    .locals 0
    .param p1, "action"    # Ljava/lang/String;
    .param p2, "name"    # Ljava/lang/CharSequence;
    .param p3, "icon"    # I
    .param p4, "extras"    # Landroid/os/Bundle;
    .annotation system Ldalvik/annotation/MethodParameters;
        accessFlags = {
            0x0,
            0x0,
            0x0,
            0x0
        }
        names = {
            "action",
            "name",
            "icon",
            "extras"
        }
    .end annotation

    .line 903
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 904
    iput-object p1, p0, Landroid/support/v4/media/session/PlaybackStateCompat$CustomAction;->mAction:Ljava/lang/String;

    .line 905
    iput-object p2, p0, Landroid/support/v4/media/session/PlaybackStateCompat$CustomAction;->mName:Ljava/lang/CharSequence;

    .line 906
    iput p3, p0, Landroid/support/v4/media/session/PlaybackStateCompat$CustomAction;->mIcon:I

    .line 907
    iput-object p4, p0, Landroid/support/v4/media/session/PlaybackStateCompat$CustomAction;->mExtras:Landroid/os/Bundle;

    .line 908
    return-void
.end method

.method public static fromCustomAction(Ljava/lang/Object;)Landroid/support/v4/media/session/PlaybackStateCompat$CustomAction;
    .locals 6
    .param p0, "customActionObj"    # Ljava/lang/Object;
    .annotation system Ldalvik/annotation/MethodParameters;
        accessFlags = {
            0x0
        }
        names = {
            "customActionObj"
        }
    .end annotation

    .line 942
    if-eqz p0, :cond_1

    sget v0, Landroid/os/Build$VERSION;->SDK_INT:I

    const/16 v1, 0x15

    if-ge v0, v1, :cond_0

    goto :goto_0

    .line 946
    :cond_0
    move-object v0, p0

    check-cast v0, Landroid/media/session/PlaybackState$CustomAction;

    .line 948
    .local v0, "customActionFwk":Landroid/media/session/PlaybackState$CustomAction;
    invoke-static {v0}, Landroid/support/v4/media/session/PlaybackStateCompat$Api21Impl;->getExtras(Landroid/media/session/PlaybackState$CustomAction;)Landroid/os/Bundle;

    move-result-object v1

    .line 949
    .local v1, "extras":Landroid/os/Bundle;
    invoke-static {v1}, Landroid/support/v4/media/session/MediaSessionCompat;->ensureClassLoader(Landroid/os/Bundle;)V

    .line 950
    new-instance v2, Landroid/support/v4/media/session/PlaybackStateCompat$CustomAction;

    .line 952
    invoke-static {v0}, Landroid/support/v4/media/session/PlaybackStateCompat$Api21Impl;->getAction(Landroid/media/session/PlaybackState$CustomAction;)Ljava/lang/String;

    move-result-object v3

    .line 953
    invoke-static {v0}, Landroid/support/v4/media/session/PlaybackStateCompat$Api21Impl;->getName(Landroid/media/session/PlaybackState$CustomAction;)Ljava/lang/CharSequence;

    move-result-object v4

    .line 954
    invoke-static {v0}, Landroid/support/v4/media/session/PlaybackStateCompat$Api21Impl;->getIcon(Landroid/media/session/PlaybackState$CustomAction;)I

    move-result v5

    invoke-direct {v2, v3, v4, v5, v1}, Landroid/support/v4/media/session/PlaybackStateCompat$CustomAction;-><init>(Ljava/lang/String;Ljava/lang/CharSequence;ILandroid/os/Bundle;)V

    .line 956
    .local v2, "customActionCompat":Landroid/support/v4/media/session/PlaybackStateCompat$CustomAction;
    iput-object v0, v2, Landroid/support/v4/media/session/PlaybackStateCompat$CustomAction;->mCustomActionFwk:Landroid/media/session/PlaybackState$CustomAction;

    .line 957
    return-object v2

    .line 943
    .end local v0    # "customActionFwk":Landroid/media/session/PlaybackState$CustomAction;
    .end local v1    # "extras":Landroid/os/Bundle;
    .end local v2    # "customActionCompat":Landroid/support/v4/media/session/PlaybackStateCompat$CustomAction;
    :cond_1
    :goto_0
    const/4 v0, 0x0

    return-object v0
.end method


# virtual methods
.method public describeContents()I
    .locals 1

    .line 927
    const/4 v0, 0x0

    return v0
.end method

.method public getAction()Ljava/lang/String;
    .locals 1

    .line 1001
    iget-object v0, p0, Landroid/support/v4/media/session/PlaybackStateCompat$CustomAction;->mAction:Ljava/lang/String;

    return-object v0
.end method

.method public getCustomAction()Ljava/lang/Object;
    .locals 3

    .line 971
    iget-object v0, p0, Landroid/support/v4/media/session/PlaybackStateCompat$CustomAction;->mCustomActionFwk:Landroid/media/session/PlaybackState$CustomAction;

    if-nez v0, :cond_1

    sget v0, Landroid/os/Build$VERSION;->SDK_INT:I

    const/16 v1, 0x15

    if-ge v0, v1, :cond_0

    goto :goto_0

    .line 975
    :cond_0
    iget-object v0, p0, Landroid/support/v4/media/session/PlaybackStateCompat$CustomAction;->mAction:Ljava/lang/String;

    iget-object v1, p0, Landroid/support/v4/media/session/PlaybackStateCompat$CustomAction;->mName:Ljava/lang/CharSequence;

    iget v2, p0, Landroid/support/v4/media/session/PlaybackStateCompat$CustomAction;->mIcon:I

    .line 976
    invoke-static {v0, v1, v2}, Landroid/support/v4/media/session/PlaybackStateCompat$Api21Impl;->createCustomActionBuilder(Ljava/lang/String;Ljava/lang/CharSequence;I)Landroid/media/session/PlaybackState$CustomAction$Builder;

    move-result-object v0

    .line 977
    .local v0, "builder":Landroid/media/session/PlaybackState$CustomAction$Builder;
    iget-object v1, p0, Landroid/support/v4/media/session/PlaybackStateCompat$CustomAction;->mExtras:Landroid/os/Bundle;

    invoke-static {v0, v1}, Landroid/support/v4/media/session/PlaybackStateCompat$Api21Impl;->setExtras(Landroid/media/session/PlaybackState$CustomAction$Builder;Landroid/os/Bundle;)V

    .line 978
    invoke-static {v0}, Landroid/support/v4/media/session/PlaybackStateCompat$Api21Impl;->build(Landroid/media/session/PlaybackState$CustomAction$Builder;)Landroid/media/session/PlaybackState$CustomAction;

    move-result-object v1

    return-object v1

    .line 972
    .end local v0    # "builder":Landroid/media/session/PlaybackState$CustomAction$Builder;
    :cond_1
    :goto_0
    iget-object v0, p0, Landroid/support/v4/media/session/PlaybackStateCompat$CustomAction;->mCustomActionFwk:Landroid/media/session/PlaybackState$CustomAction;

    return-object v0
.end method

.method public getExtras()Landroid/os/Bundle;
    .locals 1

    .line 1033
    iget-object v0, p0, Landroid/support/v4/media/session/PlaybackStateCompat$CustomAction;->mExtras:Landroid/os/Bundle;

    return-object v0
.end method

.method public getIcon()I
    .locals 1

    .line 1021
    iget v0, p0, Landroid/support/v4/media/session/PlaybackStateCompat$CustomAction;->mIcon:I

    return v0
.end method

.method public getName()Ljava/lang/CharSequence;
    .locals 1

    .line 1010
    iget-object v0, p0, Landroid/support/v4/media/session/PlaybackStateCompat$CustomAction;->mName:Ljava/lang/CharSequence;

    return-object v0
.end method

.method public toString()Ljava/lang/String;
    .locals 2

    .line 1038
    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    const-string v1, "Action:mName=\'"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v0

    iget-object v1, p0, Landroid/support/v4/media/session/PlaybackStateCompat$CustomAction;->mName:Ljava/lang/CharSequence;

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    move-result-object v0

    const-string v1, ", mIcon="

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v0

    iget v1, p0, Landroid/support/v4/media/session/PlaybackStateCompat$CustomAction;->mIcon:I

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    move-result-object v0

    const-string v1, ", mExtras="

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v0

    iget-object v1, p0, Landroid/support/v4/media/session/PlaybackStateCompat$CustomAction;->mExtras:Landroid/os/Bundle;

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    move-result-object v0

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v0

    return-object v0
.end method

.method public writeToParcel(Landroid/os/Parcel;I)V
    .locals 1
    .param p1, "dest"    # Landroid/os/Parcel;
    .param p2, "flags"    # I
    .annotation system Ldalvik/annotation/MethodParameters;
        accessFlags = {
            0x0,
            0x0
        }
        names = {
            "dest",
            "flags"
        }
    .end annotation

    .line 919
    iget-object v0, p0, Landroid/support/v4/media/session/PlaybackStateCompat$CustomAction;->mAction:Ljava/lang/String;

    invoke-virtual {p1, v0}, Landroid/os/Parcel;->writeString(Ljava/lang/String;)V

    .line 920
    iget-object v0, p0, Landroid/support/v4/media/session/PlaybackStateCompat$CustomAction;->mName:Ljava/lang/CharSequence;

    invoke-static {v0, p1, p2}, Landroid/text/TextUtils;->writeToParcel(Ljava/lang/CharSequence;Landroid/os/Parcel;I)V

    .line 921
    iget v0, p0, Landroid/support/v4/media/session/PlaybackStateCompat$CustomAction;->mIcon:I

    invoke-virtual {p1, v0}, Landroid/os/Parcel;->writeInt(I)V

    .line 922
    iget-object v0, p0, Landroid/support/v4/media/session/PlaybackStateCompat$CustomAction;->mExtras:Landroid/os/Bundle;

    invoke-virtual {p1, v0}, Landroid/os/Parcel;->writeBundle(Landroid/os/Bundle;)V

    .line 923
    return-void
.end method
