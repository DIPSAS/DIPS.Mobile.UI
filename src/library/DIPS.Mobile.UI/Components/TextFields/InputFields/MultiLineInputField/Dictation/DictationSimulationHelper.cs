namespace DIPS.Mobile.UI.Components.TextFields.InputFields.MultiLineInputField.Dictation;

internal static class DictationSimulationHelper
{
    // Use this as StartDictationDelegate to simulate starting dictation and updating text based on result, or stopping
    // by using the cancellationToken.
    internal static async Task<StartDictationResult> StartDictationDelegateTest(IDictationConsumerDelegate consumerDelegate, 
        CancellationToken cancellationToken)
    {
        const int maxWords = 200;
            
        string[] vocabulary =
        [
            "The", "quick", "brown", "fox", "jumps", "over", "a", "lazy", "dog.",
            "My", "day", "has", "been", "great,", "thank", "you!",
            "Please", "record", "the", "following", "message", "accurately."
        ];

        var random = new Random();
        
        consumerDelegate.UpdateTextWithDictationResult("Well...");
            
        foreach (var _ in Enumerable.Range(0, maxWords))
        {
            if (cancellationToken.IsCancellationRequested) return new StartDictationResult();
            
            await Task.Delay(TimeSpan.FromMilliseconds(new Random().Next(400, 1501)), cancellationToken);
            consumerDelegate.UpdateTextWithDictationResult(vocabulary[random.Next(vocabulary.Length)]);
        }
            
        return new StartDictationResult("Error");
    }
}