using System.Diagnostics;
using System.Runtime.InteropServices;
using BitMiracle.LibTiff.Classic;

using DIPS.Mobile.UI.API.Camera.ImageCapturing;
using SkiaSharp;
using Enum = DIPS.Mobile.UI.Extensions.Enum;

namespace DIPS.Mobile.UI.API.Camera.TIFF;

public class TiffFactory
{
    private Tiff? m_tiff;
    private MemoryStream? m_memoryStream;

    public void Start()
    {
        m_memoryStream = new MemoryStream();
        m_tiff = Tiff.ClientOpen("in-memory", "w", m_memoryStream, new TiffStream());
    }

    public async Task Stop()
    {
        m_tiff?.Dispose();
        if (m_memoryStream != null)
        {
            await m_memoryStream.DisposeAsync();
        }
    }
    
    public async Task<MemoryStream?> ConvertToTiffAsync(CapturedImage capturedImage, CancellationToken cancellationToken)
    {
        await Stop();
        Start();
        await Task.Run(() =>
        {
            SKBitmap? bitMap = null;
            try
            {
                var timeSpentConvertingToTiff = new Stopwatch();
                timeSpentConvertingToTiff.Start();
                if (m_tiff == null)
                {
                    throw new Exception("Failed to create the TIFF image.");
                }

                bitMap = SKBitmap.Decode(capturedImage.AsByteArray);
                var orientation = Orientation.TOPLEFT;
                
#if __IOS__ //iOS has a 1-1 mapping between the orientation from the photo metadata to the TIFF orientation tag. 
                var iosOrientation= capturedImage.Photo.Properties.Orientation;
                orientation = iosOrientation switch
                {
                    CoreImage.CIImageOrientation.TopLeft => Orientation.TOPLEFT,
                    CoreImage.CIImageOrientation.TopRight => Orientation.TOPRIGHT,
                    CoreImage.CIImageOrientation.BottomRight => Orientation.BOTRIGHT,
                    CoreImage.CIImageOrientation.BottomLeft => Orientation.BOTLEFT,
                    CoreImage.CIImageOrientation.LeftTop => Orientation.LEFTTOP,
                    CoreImage.CIImageOrientation.RightTop => Orientation.RIGHTTOP,
                    CoreImage.CIImageOrientation.RightBottom => Orientation.RIGHTBOT,
                    CoreImage.CIImageOrientation.LeftBottom => Orientation.LEFTBOT,
                    null => Orientation.TOPLEFT,
                    _ => throw new ArgumentOutOfRangeException()
                };
#endif
                
#if __ANDROID__ //We have to grab the EXIF tag from the image and based on our findings from iOS we can set the correct orientation tag.
                if (capturedImage.ImageInfo.RotationDegrees > 0)
                {
                    var memoryStream = new MemoryStream(capturedImage.AsByteArray);
                    var exif = new AndroidX.ExifInterface.Media.ExifInterface(memoryStream);
                    memoryStream.Dispose();
                    var rotation = exif.GetAttributeInt(AndroidX.ExifInterface.Media.ExifInterface.TagOrientation, AndroidX.ExifInterface.Media.ExifInterface.OrientationNormal);
                    orientation = rotation switch
                    {
                        AndroidX.ExifInterface.Media.ExifInterface.OrientationNormal => Orientation.TOPLEFT, //Landscape
                        AndroidX.ExifInterface.Media.ExifInterface.OrientationRotate90 => Orientation.RIGHTTOP, //Portrait
                        AndroidX.ExifInterface.Media.ExifInterface.OrientationRotate180 => Orientation.BOTRIGHT, //Landscape reverse
                        AndroidX.ExifInterface.Media.ExifInterface.OrientationRotate270 => Orientation.LEFTBOT, //Upside down
                        _ => orientation
                    };
                }
#endif
                // Set the TIFF fields
                m_tiff.SetField(TiffTag.BITSPERSAMPLE, 8);
                m_tiff.SetField(TiffTag.SAMPLESPERPIXEL, 4);
                m_tiff.SetField(TiffTag.PHOTOMETRIC, Photometric.RGB);
                m_tiff.SetField(TiffTag.COMPRESSION, BitMiracle.LibTiff.Classic.Compression.JPEG);
                m_tiff.SetField(TiffTag.PLANARCONFIG, PlanarConfig.CONTIG);
                m_tiff.SetField(TiffTag.ORIENTATION, capturedImage.Transformation.OrientationConstant);

                var width = bitMap.Width;
                var height = bitMap.Height;
                m_tiff.SetField(TiffTag.IMAGEWIDTH, width);
                m_tiff.SetField(TiffTag.IMAGELENGTH, height);
                m_tiff.SetField(TiffTag.ROWSPERSTRIP, height);

                // Write each row of the bitmap to the TIFF
                // m_currentConvertingImage++;
                // MainThread.BeginInvokeOnMainThread(() =>
                // {
                //     UploadingText = string.Format(LocalizedStrings.AddPhoto_PreparingPicture, m_currentConvertingImage,
                //         MediaStore.Images.Count);
                // });
                for (var row = 0; row < height; row++)
                {
                    var rowPointer = bitMap.GetAddress(0, row);
                    var rowData = new byte[width * 4];
                    Marshal.Copy(rowPointer, rowData, 0, rowData.Length);
                    if (cancellationToken.IsCancellationRequested)
                    {
                        return;
                    }
                    m_tiff.WriteScanline(rowData, row);
                }

                m_tiff.SetField(TiffTag.SUBFILETYPE, FileType.PAGE);
                m_tiff.WriteDirectory();
                m_tiff.FlushData();
                timeSpentConvertingToTiff.Stop();
                // m_totalTimeSpentConvertingToTiffInSeconds += timeSpentConvertingToTiff.Elapsed.TotalSeconds;
            }
            finally
            {
                bitMap?.Dispose();
            }
        });
        return m_memoryStream;
    }
}