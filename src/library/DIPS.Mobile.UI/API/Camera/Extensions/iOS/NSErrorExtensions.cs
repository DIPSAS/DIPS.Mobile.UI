using Foundation;

namespace DIPS.Mobile.UI.API.Camera.Extensions.iOS;

// ReSharper disable once InconsistentNaming
public static class NSErrorExtensions
{
    public static string ToExceptionMessage(this NSError error)
    {
        var metadata = new List<string>();
        if (!string.IsNullOrEmpty(error.Domain))
        {
            metadata.Add($"Error domain: {error.Domain}");
        }
            
        if (!string.IsNullOrEmpty(error.Description))
        {
            metadata.Add($"Error description: {error.Description}");
        }
            
        if (!string.IsNullOrEmpty(error.LocalizedFailureReason))
        {
            metadata.Add($"Failure reason: {error.LocalizedFailureReason}");
        }
          

        if (!string.IsNullOrEmpty(error.LocalizedRecoverySuggestion))
        {
            metadata.Add($"Error recovery suggestion: {error.LocalizedRecoverySuggestion}");
        }

        // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
        if (error.UserInfo != null)
        {
            metadata.Add("UserInfo:");
            metadata.AddRange(error.UserInfo.Select(keyValuePair => $"{keyValuePair.Key}: {keyValuePair.Value}"));
        }
        return string.Join("\n", metadata);
    }
}