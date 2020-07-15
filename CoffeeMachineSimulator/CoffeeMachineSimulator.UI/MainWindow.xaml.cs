using CoffeeMachineSimulator.Implementation.Sender;
using CoffeeMachineSimulator.UI.ViewModel;
using System.Windows;

namespace CoffeeMachineSimulator.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var evConnString = "Endpoint=sb://coffeemachineeventhubnamespacee.servicebus.windows.net/;SharedAccessKeyName=coffeemachinepolicy;SharedAccessKey=hsVGSf/+g11UWq3e+sEzv0TOv92gxhNE8i19ixajwcg=;EntityPath=coffeemachineeventhub";
            DataContext = new MainViewModel(new CoffeeMachineDataSender(evConnString));
        }
    }
}
