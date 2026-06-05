using System.Collections.ObjectModel;
using System.Windows.Input;
using DIPS.Mobile.UI.MVVM;
using DIPS.Mobile.UI.MVVM.Commands;

namespace Playground.VetleSamples.CollectionViewTests;

public class LoadMoreDividerReproPageViewModel : ViewModel
{
    private const int PageSize = 24;
    private const int MaxPages = 8;

    private bool m_isLoading;
    private int m_nextPage;
    private string m_footerTitle = "Loading documents";
    private string m_footerSubtitle = "Loaded 0 documents";

    public LoadMoreDividerReproPageViewModel()
    {
        LoadMoreDocumentsCommand = new AsyncCommand(LoadNextPageAsync);
        _ = LoadNextPageAsync();
    }

    public ObservableCollection<DocumentListItem> Documents { get; } = [];

    public string FooterTitle
    {
        get => m_footerTitle;
        set => RaiseWhenSet(ref m_footerTitle, value);
    }

    public string FooterSubtitle
    {
        get => m_footerSubtitle;
        set => RaiseWhenSet(ref m_footerSubtitle, value);
    }

    public ICommand LoadMoreDocumentsCommand { get; }

    private async Task LoadNextPageAsync()
    {
        if (m_isLoading || m_nextPage >= MaxPages)
            return;

        m_isLoading = true;
        UpdateFooter("Loading more documents", $"Loaded {Documents.Count} documents");

        var page = m_nextPage;
        m_nextPage++;

        var documents = await DocumentService.FetchDocumentsAsync(page, PageSize);
        foreach (var document in documents)
        {
            Documents.Add(document);
        }

        var hasMoreDocuments = m_nextPage < MaxPages;
        UpdateFooter(
            hasMoreDocuments ? "More documents available" : "All documents loaded",
            $"Loaded {Documents.Count} documents");

        m_isLoading = false;
    }

    private void UpdateFooter(string title, string subtitle)
    {
        FooterTitle = title;
        FooterSubtitle = subtitle;
    }

    private static class DocumentService
    {
        private static readonly string[] s_titles =
        [
            "Treatment plan note",
            "Scanned document",
            "Medication exported from Interactor",
            "Journal note",
            "Discharge summary",
            "Lab result",
            "Referral response",
            "Assistant note"
        ];

        public static async Task<IReadOnlyList<DocumentListItem>> FetchDocumentsAsync(int page, int pageSize)
        {
            await Task.Delay(450);

            var start = page * pageSize;
            return Enumerable.Range(start + 1, pageSize)
                .Select(index => new DocumentListItem(
                    s_titles[index % s_titles.Length],
                    $"Search snippet for loaded document {index}",
                    $"{DateTime.Today.AddDays(-index):dd. MMM yyyy} kl {8 + index % 10:00}:{index % 60:00}",
                    index % 3 == 0 ? "Smistad, Geir" : "Demo, Doktor",
                    index % 2 == 0 ? "KIR" : "MED",
                    index % 9 == 0,
                    index % 4 == 0,
                    index % 5 == 0,
                    index % 7 == 0,
                    index % 13 == 0))
                .ToList();
        }
    }
}

public sealed class DocumentListItem
{
    public DocumentListItem(
        string title,
        string snippet,
        string eventTime,
        string authorName,
        string departmentShortName,
        bool notSigned,
        bool hasDictation,
        bool hasNote,
        bool isApproved,
        bool isSelectedDocument)
    {
        Title = title;
        Snippet = snippet;
        EventTime = eventTime;
        AuthorName = authorName;
        DepartmentShortName = departmentShortName;
        NotSigned = notSigned;
        HasDictation = hasDictation;
        HasNote = hasNote;
        IsApproved = isApproved;
        IsSelectedDocument = isSelectedDocument;
        AccessibilityDescription = $"{Title}, {EventTime}, {AuthorName}, {DepartmentShortName}";
        NavigateToDocumentCommand = new Command(() => { });
    }

    public string Title { get; }
    public string Snippet { get; }
    public string EventTime { get; }
    public string AuthorName { get; }
    public string DepartmentShortName { get; }
    public bool NotSigned { get; }
    public bool HasDictation { get; }
    public bool HasNote { get; }
    public bool IsApproved { get; }
    public bool IsSelectedDocument { get; }
    public string AccessibilityDescription { get; }
    public ICommand NavigateToDocumentCommand { get; }
}