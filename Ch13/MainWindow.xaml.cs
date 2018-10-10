using GalaSoft.MvvmLight.Messaging;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TaskDialogInterop;

namespace Ch13
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            Messenger.Default.Register<string>(this, (s) =>
            {
                var options = new TaskDialogOptions
                {
                    CommandButtons = new[] { "Button 1", "Button 2", "Button 3" },
                    Title = "This is a TaskDialog",
                    MainInstruction = s,
                    Content="This is extra content that can go along with the message."
                };
                var result = TaskDialog.Show(options);
                MessageBox.Show($"You clicked on \n* Command Button {result.CommandButtonResult ?? -1}\n* Radio Button {result.RadioButtonResult ?? -1}\n* Custom Button {result.CustomButtonResult ?? -1}\n* Result: {result.Result}");
            });
        }
    }
}
