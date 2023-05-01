using Logatron.Contracts.ViewModels;

namespace Logatron.ViewModels;

public class LogbookPageViewModel : ViewModelBase, INavigationAware
{
    public LogbookViewModel LogbookViewModel
    {
        get;
    }

    public RadioViewModel RadioViewModel
    {
        get;
    }

    public LogbookPageViewModel()
    {
        LogbookViewModel = App.GetService<LogbookViewModel>();
        RadioViewModel = App.GetService<RadioViewModel>();
    }

    public async void OnNavigatedTo(object parameter)
    {
        await LogbookViewModel.UpdateLogbook();
    }

    public void OnNavigatedFrom()
    {
    }
}
