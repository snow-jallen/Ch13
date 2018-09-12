using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace Ch13
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MyData myData;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = myData = new MyData(); 
        }

        private void btnClick(object sender, RoutedEventArgs e)
        {
            myData.MyTextValue = DateTime.Now.ToString();
            myData.MyDateValue = DateTime.Now;
        }
    }

    public class MyData : INotifyPropertyChanged
    {
        private string _myTextValue;

        private string myTextValue;

        public string MyTextValue
        {
            get { return myTextValue; }
            set
            {
                myTextValue = value;
                propertyChanged(nameof(MyTextValue));
            }
        }

        private void propertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private DateTime myDateValue;

        public DateTime MyDateValue
        {
            get { return myDateValue; }
            set { myDateValue = value;
                propertyChanged(nameof(MyDateValue));
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
    }
}
