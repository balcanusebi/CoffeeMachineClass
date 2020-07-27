using CoffeeMachineSimulator.UI.ViewModel;
using System.Windows;

namespace CoffeeMachineSimulator.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(MainViewModel model) : this()
        {
            DataContext = model;
        }

        public MainWindow()
        {
            InitializeComponent();
        }
    }
}
