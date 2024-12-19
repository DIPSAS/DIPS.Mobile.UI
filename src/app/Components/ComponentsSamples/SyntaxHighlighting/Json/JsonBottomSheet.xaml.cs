using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Components.ComponentsSamples.SyntaxHighlighting.Json;

public partial class JsonBottomSheet
{
    private readonly string m_json;

    public JsonBottomSheet(string json)
    {
        InitializeComponent();
        BindingContext = m_json  = json;
    }

    private void CopyJson(object? sender, EventArgs e)
    {
        Clipboard.SetTextAsync(m_json);
    }
}