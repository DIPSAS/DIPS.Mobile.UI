namespace DIPS.Mobile.UI.API.Camera.BarcodeScanning;

/// <summary>
/// Collects barcode detections over an observation window and determines the most-detected barcode.
/// Extracted from <see cref="BarcodeScanner"/> to isolate the detection aggregation responsibility.
/// </summary>
internal sealed class BarcodeDetectionAggregator
{
    private List<BarcodeObservation> m_observations = new();

    /// <summary>
    /// Records a barcode detection, incrementing the count for an already-observed barcode
    /// or adding a new observation.
    /// </summary>
    public void AddObservation(Barcode barcode)
    {
        var key = GetBarcodeKey(barcode);
        var existing = m_observations.FirstOrDefault(o => GetBarcodeKey(o.Barcode) == key);
        if (existing is null)
        {
            m_observations.Add(new BarcodeObservation(barcode, 1));
        }
        else
        {
            var count = existing.Detections + 1;
            m_observations.Remove(existing);
            m_observations.Add(new BarcodeObservation(barcode, count));
        }
    }

    /// <summary>
    /// Returns all observations ordered by detection count (descending), with the most-detected
    /// observation marked via <see cref="BarcodeObservation.HasMostDetections"/>.
    /// Returns null if no observations have been recorded.
    /// </summary>
    public List<BarcodeObservation>? GetObservationsOrderedByDetectionCount()
    {
        if (m_observations.Count == 0)
            return null;

        var ordered = m_observations.OrderByDescending(o => o.Detections).ToList();
        var best = ordered.First();
        best.HasMostDetections = true;
        return ordered;
    }

    /// <summary>
    /// Clears all recorded observations, preparing for a new detection window.
    /// </summary>
    public void Reset()
    {
        m_observations = [];
    }

    internal static string GetBarcodeKey(Barcode barcode) => barcode.RawValue ?? string.Empty;
}
