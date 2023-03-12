using System;
using System.Threading;
using System.Windows.Controls;

namespace Logatron.Views
{
    public partial class LogbookView : UserControl
    {
        public LogbookView()
        {
            var database = new Logbook.Database("logbook.db");

            InitializeComponent();

            DataContext = new ViewModels.LogbookViewModel(database);

            //Thread a = new Thread(() =>
            //{
            //    Thread.Sleep(10000);
            //    this.Dispatcher.Invoke(() =>
            //    {
            //        database.Entries.Local.Add(new Logbook.Entry()
            //        {
            //            StartTime = DateTime.UtcNow,
            //            EndTime = DateTime.UtcNow,
            //            Callsign = "ABCD",
            //        });
            //        database.SaveChanges();
            //    });
            //});
            //a.Start();
        }
    }
}
