using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using WpfSistemaPOS2;

namespace WpfSistemaPOS
{
    /// <summary>
    /// Lógica de interacción para PuntoDeVentaClasico.xaml
    /// </summary>
    public partial class PuntoDeVentaClasico : Window
    {
        public PuntoDeVentaClasico()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            var items = new List<Item>
            {
                new Item { Articulo = "987", Quantity = 1, Descuento = 0, Descripcion = "LIMPIA CARBURADOR T4 VICTORY 53A", Lista = 87.00, Precio = 81.78 },
                new Item { Articulo = "4844", Quantity = 1, Descuento = 0, Descripcion = "BUJIA PLATINO TS-III A3 A4 2.0 'DELGADA'", Lista = 52.00, Precio = 48.88 }
            };

            dataGrid.ItemsSource = items;
        }

        private void Pagina_Inicio(object sender, RoutedEventArgs e)
        {
            Home home = new();
            home.Show();
            this.Close();
        }

        public class Item
        {
            public string Articulo { get; set; }
            public int Quantity { get; set; }
            public double Descuento { get; set; }
            public string Descripcion { get; set; }
            public double Lista { get; set; }
            public double Precio { get; set; }
            public double Importe => Quantity * Precio;
        }
    }
}
