using BitMiracle.LibTiff.Classic;
using Android.Graphics;
using DIPS.Mobile.UI.Internal.Logging;

namespace DIPS.Mobile.UI.Components.TiffViewer;

public partial class TiffViewer
{
    private TaskCompletionSource<byte[]?>[]? m_pageTcs;

    private partial async Task<int> LoadTiffPagesAsync(CancellationToken? cancellationToken = null)
    {
        if (Source == null || Source.Length == 0)
        {
            m_pageTcs = null;
            return 1;
        }

        try
        {
            var tiffData = Source;

            // Use in-memory stream with LibTiff
            using var memoryStream = new MemoryStream(tiffData);
            using var tiff = Tiff.ClientOpen("memory", "r", memoryStream, new TiffStream());
            
            if (tiff == null)
            {
                return 1;
            }

            // Count pages
            var pageCount = 0;
            do
            {
                pageCount++;
            } while (tiff.ReadDirectory());

            m_pageTcs = new TaskCompletionSource<byte[]?>[pageCount];
            for (var i = 0; i < pageCount; i++)
            {
                m_pageTcs[i] = new TaskCompletionSource<byte[]?>();
            }
            
            // Load pages in parallel for much faster performance
            _ = Task.Run(() =>
            {
                try
                {
                    var options = new ParallelOptions
                    {
                        MaxDegreeOfParallelism = Environment.ProcessorCount,
                        CancellationToken = cancellationToken ?? CancellationToken.None
                    };

                    Parallel.For(0, pageCount, options, i =>
                    {
                        try
                        {
                            // Each thread needs its own Tiff instance
                            using var bgMemoryStream = new MemoryStream(tiffData);
                            using var bgTiff = Tiff.ClientOpen($"memory_{i}", "r", bgMemoryStream, new TiffStream());
                            
                            if (bgTiff is null)
                            {
                                m_pageTcs[i].TrySetResult(null);
                                return;
                            }

                            bgTiff.SetDirectory((short)i);
                            
                            var w = bgTiff.GetField(TiffTag.IMAGEWIDTH)[0].ToInt();
                            var h = bgTiff.GetField(TiffTag.IMAGELENGTH)[0].ToInt();
                            var r = new int[w * h];

                            if (!bgTiff.ReadRGBAImageOriented(w, h, r, Orientation.TOPLEFT))
                            {
                                m_pageTcs[i].TrySetResult(null);
                                return;
                            }

                            using var bmp = Bitmap.CreateBitmap(w, h, Bitmap.Config.Argb8888!);
                            bmp.SetPixels(r, 0, w, 0, 0, w, h);
                            
                            using var stream = new MemoryStream();
                            bmp.Compress(Bitmap.CompressFormat.Jpeg!, 90, stream);
                            var imageData = stream.ToArray();
                            m_pageTcs[i].TrySetResult(imageData);
                        }
                        catch (Exception ex)
                        {
                            DUILogService.LogError<Components.TiffViewer.TiffViewer>($"Error loading page {i}: {ex.Message}");
                            m_pageTcs[i].TrySetResult(null);
                        }
                    });
                }
                catch (OperationCanceledException)
                {
                    
                }
                catch (Exception ex)
                {
                    DUILogService.LogError<Components.TiffViewer.TiffViewer>($"Error loading pages in parallel: {ex.Message}");
                }
            });

            return pageCount;
        }
        catch (Exception ex)
        {
            DUILogService.LogError<Components.TiffViewer.TiffViewer>($"Error loading TIFF on Android: {ex.Message}");
            m_pageTcs = null;
            return 1;
        }
    }

    private partial async Task<ImageSource?> GetPageImageSourceAsync(int pageIndex, CancellationToken? cancellationToken = null)
    {
        if (m_pageTcs == null || pageIndex < 0 || pageIndex >= m_pageTcs.Length)
        {
            return null;
        }

        try
        {
            // Wait for the page data to be loaded
            byte[]? imageData;
            if (cancellationToken.HasValue)
            {
                imageData = await m_pageTcs[pageIndex].Task.WaitAsync(cancellationToken.Value);
            }
            else
            {
                imageData = await m_pageTcs[pageIndex].Task;
            }
            
            return imageData == null ? null : ImageSource.FromStream(() => new MemoryStream(imageData));
        }
        catch (OperationCanceledException)
        {
            // Page load was cancelled
            throw;
        }
        catch (Exception ex)
        {
            DUILogService.LogError<Components.TiffViewer.TiffViewer>($"Error getting page {pageIndex} on Android: {ex.Message}");
            return null;
        }
    }
}
