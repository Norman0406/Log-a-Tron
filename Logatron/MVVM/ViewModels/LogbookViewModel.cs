using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Logatron.MVVM.Models;
using Logatron.MVVM.Models.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using static Logatron.Database.Services.ILogbookProvider;

namespace Logatron.MVVM.ViewModels
{
    public partial class LogbookViewModel : ViewModelBase
    {
        private readonly Logbook _logbook;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(NumberOfPages))]
        private ObservableCollection<LogbookEntryListViewModel> _entries = new();

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(HasSelectedEntry))]
        private LogbookEntryListViewModel? _selectedEntry;

        public bool HasSelectedEntry => SelectedEntry != null;

        [ObservableProperty]
        private LogbookEntryEditViewModel _entryEdit;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(Pages))]
        public int _currentPageNumber = 1;

        public int NumberOfPages => (NumberOfEntries - 1) / PageSize + 1;

        public struct PageOrEllipsis
        {
            public bool IsEllipsis { get; set; }
            public int PageNumber { get; set; }
        }

        public IEnumerable<PageOrEllipsis> Pages => CreateEllipsedPages();

        private readonly IList<int> _pageSizes = new List<int> { 10, 25, 50, 100, 250, 500 };
        public IList<int> PageSizes => _pageSizes;

        public int PageSize => PageSizes[SelectedPageSizeIndex];

        OrderingDefinition _ordering = new OrderingDefinition
        {
            FieldName = nameof(LogbookEntry.StartTime),
            Descending = true,
        };

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(PageSize))]
        [NotifyPropertyChangedFor(nameof(NumberOfPages))]
        [NotifyPropertyChangedFor(nameof(Pages))]
        public int _selectedPageSizeIndex = 0;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(NumberOfPages))]
        [NotifyPropertyChangedFor(nameof(Pages))]
        private int _numberOfEntries = 0;

        public ICommand UpdateLogbookCommand => new AsyncRelayCommand(UpdateLogbook);
        public ICommand SortingChangedCommand => new AsyncRelayCommand<DataGridSortingEventArgs>(SortingChanged);
        public ICommand CreateEntryCommand => new AsyncRelayCommand(CreateEntry);
        public ICommand BeginUpdateEntryCommand => new RelayCommand(BeginUpdateEntry);
        public ICommand UpdateEntryCommand => new AsyncRelayCommand(UpdateEntry);
        public ICommand DeleteEntryCommand => new AsyncRelayCommand(DeleteEntry);
        public ICommand ClearCommand => new RelayCommand(Clear);
        public ICommand GoToFirstPageCommand => new AsyncRelayCommand(GoToFirstPage);
        public ICommand GoToPreviousPageCommand => new AsyncRelayCommand(GoToPreviousPage);
        public ICommand GoToPageNumberCommand => new AsyncRelayCommand<int>(GoToPageNumber);
        public ICommand GoToNextPageCommand => new AsyncRelayCommand(GoToNextPage);
        public ICommand GoToLastPageCommand => new AsyncRelayCommand(GoToLastPage);

        public LogbookViewModel()
        {
            var dummyLogbookService = new DummyLogbookService();
            _logbook = new Logbook(dummyLogbookService, dummyLogbookService, dummyLogbookService, dummyLogbookService);
            Clear();
        }

        public LogbookViewModel(Logbook logbook)
        {
            _logbook = logbook;
            Clear();
        }

        public override void LoadState()
        {
            var pageSizeIndex = _pageSizes.IndexOf(Properties.Settings.Default.LogbookPageSize);
            if (pageSizeIndex >= 0)
            {
                SelectedPageSizeIndex = pageSizeIndex;
            }
        }

        public override void SaveState()
        {
            Properties.Settings.Default.LogbookPageSize = PageSize;
        }

        private async Task UpdateLogbook()
        {
            // TODO: For some reason the "Loaded" event is not getting called

            if (CurrentPageNumber > NumberOfPages && CurrentPageNumber > 1)
            {
                CurrentPageNumber = NumberOfPages;
            }

            await GoToPageNumber(CurrentPageNumber);

            NumberOfEntries = await _logbook.GetNumberOfEntries();

            Clear();
        }

        private async Task SortingChanged(DataGridSortingEventArgs? e)
        {
            if (e == null)
            {
                return;
            }

            var descending = e.Column.SortDirection != ListSortDirection.Descending;

            e.Handled = true;

            _ordering.FieldName = e.Column.SortMemberPath;
            _ordering.Descending = descending;
            await UpdateLogbook();

            e.Column.SortDirection = (descending) ? ListSortDirection.Descending : ListSortDirection.Ascending;
        }

        private void EntriesChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged(nameof(NumberOfPages));
        }

        private async Task CreateEntry()
        {
            LogbookEntry entry = new()
            {
                StartTime = EntryEdit.StartTime,
                EndTime = EntryEdit.EndTime,
                Callsign = EntryEdit.Callsign,
                Name = EntryEdit.Name,
                Comments = EntryEdit.Comments
            };

            await _logbook.CreateEntry(entry);
            await UpdateLogbook();
            Clear();
        }

        private void BeginUpdateEntry()
        {
            if (SelectedEntry == null)
            {
                return;
            }

            EntryEdit = new LogbookEntryEditViewModel(SelectedEntry.Entry, UpdateEntryCommand, ClearCommand);
        }

        private async Task UpdateEntry()
        {
            LogbookEntry entry = new()
            {
                Id = EntryEdit.Entry.Id,
                StartTime = EntryEdit.StartTime,
                EndTime = EntryEdit.EndTime,
                Callsign = EntryEdit.Callsign,
                Name = EntryEdit.Name,
                Comments = EntryEdit.Comments
            };

            await _logbook.UpdateEntry(entry);
            await UpdateLogbook();
            Clear();
        }

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

        private void Clear()
        {
            EntryEdit = new LogbookEntryEditViewModel(CreateEntryCommand, ClearCommand);
        }

        private async Task GoToFirstPage()
        {
            await GoToPageNumber(1);
        }

        private async Task GoToPreviousPage()
        {
            await GoToPageNumber(CurrentPageNumber - 1);
        }

        private async Task GoToPageNumber(int page)
        {
            if (page < 1 || page > NumberOfPages)
            {
                return;
            }

            var paging = new PagingDefinition
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

        private async Task GoToNextPage()
        {
            await GoToPageNumber(CurrentPageNumber + 1);
        }

        private async Task GoToLastPage()
        {
            await GoToPageNumber(NumberOfPages);
        }

        private IList<PageOrEllipsis> CreateEllipsedPages()
        {
            IList<PageOrEllipsis> pages = new List<PageOrEllipsis>();

            bool addedEllipses = false;
            for (int page = 1; page <= NumberOfPages; page++)
            {
                if (page == 1 || page == NumberOfPages ||
                    page == CurrentPageNumber - 1 || page == CurrentPageNumber || page == CurrentPageNumber + 1)
                {
                    pages.Add(new PageOrEllipsis
                    {
                        IsEllipsis = false,
                        PageNumber = page
                    });

                    addedEllipses = false;
                }
                else if (!addedEllipses)
                {
                    pages.Add(new PageOrEllipsis
                    {
                        IsEllipsis = true,
                        PageNumber = 0
                    });
                    addedEllipses = true;
                }
            }

            return pages;
        }
    }
}
