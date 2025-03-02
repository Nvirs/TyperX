using Avalonia.Controls;
using Avalonia.Input;
using TyperX.ViewModels;

namespace TyperX.Views
{
    public partial class MainWindow : Window
    {
        //Main window application 
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
        }

        public void InputKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                var viewModel = (MainWindowViewModel)DataContext;
                viewModel.CheckInput();
            }
        }
    }
}
