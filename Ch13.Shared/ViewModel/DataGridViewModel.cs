using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Ch13.Shared.ViewModel
{
    public class DataGridViewModel : INotifyPropertyChanged
    {
        public DataGridViewModel()
        {
            var xml = XDocument.Load(Assembly.GetExecutingAssembly().GetManifestResourceStream("Ch13.Shared.menu.xml"));
            Menu = from food in xml.Element("breakfast_menu").Elements("food")
                   select new MenuItem(
                       (string)food.Element("name"),
                       (string)food.Element("type"),
                       decimal.Parse((string)food.Element("price"), System.Globalization.NumberStyles.Currency),
                       (string)food.Element("description"),
                       (int)food.Element("calories"));
        }

        public IEnumerable<String> MenuItemTypes => Menu.Select(m => m.Type).Distinct();

        public IEnumerable<MenuItem> Menu { get; private set; }

        #region INotifyPropertyChanged Implementation
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value))
                return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
        #endregion
    }
}
