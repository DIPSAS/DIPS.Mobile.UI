.class public final Landroid/support/v4/media/AudioAttributesImplApi26Parcelizer;
.super Landroidx/media/AudioAttributesImplApi26Parcelizer;
.source "AudioAttributesImplApi26Parcelizer.java"


# direct methods
.method public constructor <init>()V
    .locals 0

    .line 11
    invoke-direct {p0}, Landroidx/media/AudioAttributesImplApi26Parcelizer;-><init>()V

    return-void
.end method

.method public static read(Landroidx/versionedparcelable/VersionedParcel;)Landroidx/media/AudioAttributesImplApi26;
    .locals 1
    .param p0, "parcel"    # Landroidx/versionedparcelable/VersionedParcel;
    .annotation system Ldalvik/annotation/MethodParameters;
        accessFlags = {
            0x0
        }
        names = {
            "parcel"
        }
    .end annotation

    .line 13
    invoke-static {p0}, Landroidx/media/AudioAttributesImplApi26Parcelizer;->read(Landroidx/versionedparcelable/VersionedParcel;)Landroidx/media/AudioAttributesImplApi26;

    move-result-object v0

    return-object v0
.end method

.method public static write(Landroidx/media/AudioAttributesImplApi26;Landroidx/versionedparcelable/VersionedParcel;)V
    .locals 0
    .param p0, "obj"    # Landroidx/media/AudioAttributesImplApi26;
    .param p1, "parcel"    # Landroidx/versionedparcelable/VersionedParcel;
    .annotation system Ldalvik/annotation/MethodParameters;
        accessFlags = {
            0x0,
            0x0
        }
        names = {
            "obj",
            "parcel"
        }
    .end annotation

    .line 17
    invoke-static {p0, p1}, Landroidx/media/AudioAttributesImplApi26Parcelizer;->write(Landroidx/media/AudioAttributesImplApi26;Landroidx/versionedparcelable/VersionedParcel;)V

    .line 18
    return-void
.end method
