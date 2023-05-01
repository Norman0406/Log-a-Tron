using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.WinUI.UI.Controls;
using Helpers;
using Logatron.Core.Contracts.Services;
using Logatron.Core.Models;
using Microsoft.UI.Xaml.Input;

namespace Logatron.ViewModels;

public struct PageOrEllipsis
{
    public bool IsEllipsis
    {
        get; set;
    }

    public int PageNumber
    {
        get; set;
    }

    public Windows.UI.Text.FontWeight FontWeight
    {
        get; set;
    }

    public ICommand GoToPageNumberCommand
    {
        get; set;
    }
}

public partial class LogbookViewModel : ViewModelBase
{
    private readonly Logbook _logbook;

    [NotifyPropertyChangedFor(nameof(NumberOfPages))]
    [ObservableProperty]
    private ObservableCollection<LogbookEntryListViewModel> _entries = new();

    [NotifyPropertyChangedFor(nameof(HasSelectedEntry))]
    [ObservableProperty]
    private LogbookEntryListViewModel? _selectedEntry;

    public bool HasSelectedEntry => SelectedEntry != null;

    [ObservableProperty]
    private LogbookEntryEditViewModel _entryEdit;

    [NotifyPropertyChangedFor(nameof(Pages))]
    [ObservableProperty]
    public int _currentPageNumber = 1;

    public int NumberOfPages => (NumberOfEntries - 1) / PageSize + 1;

    public IEnumerable<PageOrEllipsis> Pages => CreateEllipsedPages();

    private readonly IList<int> _pageSizes = new List<int> { 10, 25, 50, 100, 250, 500 };
    public IList<int> PageSizes => _pageSizes;

    public int PageSize => PageSizes[SelectedPageSizeIndex];

    private ILogbookService.OrderingDefinition _ordering = new()
    {
        FieldName = nameof(LogbookEntry.StartTime),
        Descending = true,
    };

    [NotifyPropertyChangedFor(nameof(PageSize))]
    [NotifyPropertyChangedFor(nameof(NumberOfPages))]
    [NotifyPropertyChangedFor(nameof(Pages))]
    [ObservableProperty]
    public int _selectedPageSizeIndex = 0;

    [NotifyPropertyChangedFor(nameof(NumberOfPages))]
    [NotifyPropertyChangedFor(nameof(Pages))]
    [ObservableProperty]
    private int _numberOfEntries = 0;

    private DataGridColumn? _previousSortingColumn = null;

    public LogbookViewModel()
    {
        var logbookService = App.GetService<ILogbookService>();
        _logbook = new Logbook(logbookService);
    }

    public override void LoadState()
    {
        //var pageSizeIndex = _pageSizes.IndexOf(Properties.Settings.Default.LogbookPageSize);
        //if (pageSizeIndex >= 0)
        //{
        //    SelectedPageSizeIndex = pageSizeIndex;
        //}
    }

    public override void SaveState()
    {
        //Properties.Settings.Default.LogbookPageSize = PageSize;
    }

    [RelayCommand]
    public async Task UpdateLogbook()
    {
        if (CurrentPageNumber > NumberOfPages && CurrentPageNumber > 1)
        {
            CurrentPageNumber = NumberOfPages;
        }

        await GoToPageNumber(CurrentPageNumber);

        NumberOfEntries = await _logbook.GetNumberOfEntries();

        Clear();
    }

    [RelayCommand]
    private async Task SortingChanged(DataGridColumnEventArgs? e)
    {
        if (e == null || e.Column == null || e.Column.Tag == null || e.Column.Tag is not string)
        {
            return;
        }

        var descending = e.Column.SortDirection != DataGridSortDirection.Descending;

        var sortDirectionWasNull = e.Column.SortDirection == null;
        if (!sortDirectionWasNull && descending)
        {
            e.Column.SortDirection = null;

            // reset sorting
            _ordering.FieldName = nameof(LogbookEntry.StartTime);
            _ordering.Descending = true;
        }
        else
        {
            e.Column.SortDirection = (descending) ? DataGridSortDirection.Descending : DataGridSortDirection.Ascending;

            _ordering.FieldName = (string)e.Column.Tag;
            _ordering.Descending = descending;
        }

        if (_previousSortingColumn != null && _previousSortingColumn != e.Column)
        {
            _previousSortingColumn.SortDirection = null;
        }

        _previousSortingColumn = e.Column;

        await UpdateLogbook();
    }

    private void EntriesChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
        OnPropertyChanged(nameof(NumberOfPages));
    }

    [RelayCommand]
    private async Task CreateEntry()
    {
        LogbookEntry entry = new()
        {
            StartTime = DateTimeExtensions.FromDateAndTime(EntryEdit.StartDate.LocalDateTime, EntryEdit.StartTime),
            EndTime = DateTimeExtensions.FromDateAndTime(EntryEdit.EndDate.LocalDateTime, EntryEdit.EndTime),
            Callsign = EntryEdit.Callsign,
            Name = EntryEdit.Name,
            Comments = EntryEdit.Comments
        };

        await _logbook.CreateEntry(entry);
        await UpdateLogbook();
        Clear();
    }

    [RelayCommand]
    private void BeginUpdateEntry()
    {
        if (SelectedEntry == null)
        {
            return;
        }

        EntryEdit = new LogbookEntryEditViewModel(SelectedEntry.Entry, UpdateEntryCommand, ClearCommand);
    }

    [RelayCommand]
    private async Task UpdateEntry()
    {
        LogbookEntry entry = new()
        {
            Id = EntryEdit.Entry.Id,
            StartTime = DateTimeExtensions.FromDateAndTime(EntryEdit.StartDate.LocalDateTime, EntryEdit.StartTime),
            EndTime = DateTimeExtensions.FromDateAndTime(EntryEdit.EndDate.LocalDateTime, EntryEdit.EndTime),
            Callsign = EntryEdit.Callsign,
            Name = EntryEdit.Name,
            Comments = EntryEdit.Comments
        };

        await _logbook.UpdateEntry(entry);
        await UpdateLogbook();
        Clear();
    }

    [RelayCommand]
    private async Task DeleteEntry()
    {
        if (SelectedEntry == null)
        {
            return;
        }

        if (MessageBox.Show(
            "Are you sure you want to delete this QSO?",
            "Delete QSO?",
            MessageBoxButton.YesNo,
            MessageBoxImage.Warning) == MessageBoxResult.Yes)
        {
            await _logbook.DeleteEntry(SelectedEntry.Entry);

            if (Entries.Count == 1 && NumberOfPages > 1 && CurrentPageNumber == NumberOfPages)
            {
                CurrentPageNumber--;
            }

            await UpdateLogbook();
            Clear();
        }
    }

    [RelayCommand]
    private void SelectRow(RightTappedRoutedEventArgs? param)
    {
        if (param != null && param.OriginalSource is Microsoft.UI.Xaml.FrameworkElement element)
        {
            SelectedEntry = element.DataContext as LogbookEntryListViewModel;
        }
    }

    [RelayCommand]
    private void Clear()
    {
        EntryEdit = new LogbookEntryEditViewModel(CreateEntryCommand, ClearCommand);
    }

    [RelayCommand]
    private async Task GoToFirstPage()
    {
        await GoToPageNumber(1);
    }

    [RelayCommand]
    private async Task GoToPreviousPage()
    {
        await GoToPageNumber(CurrentPageNumber - 1);
    }

    [RelayCommand]
    private async Task GoToPageNumber(int page)
    {
        if (page < 1 || page > NumberOfPages)
        {
            return;
        }

        var paging = new ILogbookService.PagingDefinition
        {
            Page = page,
            Limit = PageSize,
        };

        var entries = await _logbook.GetEntries(paging, _ordering);
        Entries = new ObservableCollection<LogbookEntryListViewModel>(
            entries.Select(entry => new LogbookEntryListViewModel(entry)));
        Entries.CollectionChanged += EntriesChanged;

        CurrentPageNumber = page;
    }

    [RelayCommand]
    private async Task GoToNextPage()
    {
        await GoToPageNumber(CurrentPageNumber + 1);
    }

    [RelayCommand]
    private async Task GoToLastPage()
    {
        await GoToPageNumber(NumberOfPages);
    }

    private IList<PageOrEllipsis> CreateEllipsedPages()
    {
        IList<PageOrEllipsis> pages = new List<PageOrEllipsis>();

        var addedEllipses = false;
        for (var page = 1; page <= NumberOfPages; page++)
        {
            if (page == 1 || page == NumberOfPages ||
                page == CurrentPageNumber - 1 || page == CurrentPageNumber || page == CurrentPageNumber + 1)
            {
                pages.Add(new PageOrEllipsis
                {
                    IsEllipsis = false,
                    PageNumber = page,
                    FontWeight = page == CurrentPageNumber ? Microsoft.UI.Text.FontWeights.Bold : Microsoft.UI.Text.FontWeights.Normal,
                    GoToPageNumberCommand = GoToPageNumberCommand
                });

                addedEllipses = false;
            }
            else if (!addedEllipses)
            {
                pages.Add(new PageOrEllipsis
                {
                    IsEllipsis = true,
                });
                addedEllipses = true;
            }
        }

        return pages;
    }
}
