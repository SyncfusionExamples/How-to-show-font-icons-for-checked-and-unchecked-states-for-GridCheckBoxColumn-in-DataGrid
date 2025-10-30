using System.Collections.ObjectModel;
using System.ComponentModel;

namespace DataGridSample
{ 
    public class ViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<OrderInfo> _orders;

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<OrderInfo> Orders
        {
            get { return _orders; }
            set
            {
                if (_orders != value)
                {
                    _orders = value;
                    OnPropertyChanged(nameof(Orders));
                }
            }
        }

        public ViewModel()
        {
            _orders = new ObservableCollection<OrderInfo>();
            GenerateOrders();
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void GenerateOrders()
        {
            _orders.Add(new OrderInfo(1001, "Maria Anders", "Germany", "ALFKI", true));
            _orders.Add(new OrderInfo(1002, "Ana Trujilo", "Mexico", "ANATR", true));
            _orders.Add(new OrderInfo(1003, "Antonio Moreno", "Mexico", "ANTON", false));
            _orders.Add(new OrderInfo(1004, "Thomas Hardy", "UK", "AROUT", true));
            _orders.Add(new OrderInfo(1005, "Christina Berglund", "Sweden", "BERGS", false));
            _orders.Add(new OrderInfo(1006, "Hanna Moos", "Germany", "BLAUS", true));
            _orders.Add(new OrderInfo(1007, "Frederique Citeaux", "France", "BLONP", false));
            _orders.Add(new OrderInfo(1008, "Martin Sommer", "Spain", "BOLID", true));
            _orders.Add(new OrderInfo(1009, "Laurence Lebihan", "France", "BONAP", true));
            _orders.Add(new OrderInfo(1010, "Elizabeth Lincoln", "Canada", "BOTTM", false));
        }
    }

}
