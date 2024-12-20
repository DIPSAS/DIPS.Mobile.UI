using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Components.ComponentsSamples.SyntaxHighlighting;

public partial class CodeBottomSheet
{
    private readonly CodeBottomSheetViewModel m_viewModel;

    public CodeBottomSheet(string code, string language)
    {
        BindingContext = m_viewModel = new CodeBottomSheetViewModel(code, language);
        InitializeComponent();
    }

    private void Copy(object? sender, EventArgs e)
    {
        Clipboard.SetTextAsync(m_viewModel.Code);
    }
}