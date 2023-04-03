using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

namespace DIPS.Mobile.UI.SourceGenerator
{
    [Generator]
    public class XCTGenerator : ISourceGenerator
    {
        const string code = @"
namespace DIPS.Mobile.UI.Initializer
{
	sealed class DUIInitCaller
	{
		public void CallInit()
		{
			DIPS.Mobile.UI.DUI.Init();
		}
	}
}";
        public void Execute(GeneratorExecutionContext context)
        {
            context.AddSource("DUIInitCaller.g.cs", SourceText.From(code, Encoding.UTF8));
        }

        public void Initialize(GeneratorInitializationContext context)
        {
        }
    }
}