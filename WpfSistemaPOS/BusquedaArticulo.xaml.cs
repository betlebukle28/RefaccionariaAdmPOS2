using MongoDB.Bson;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using WpfSistemaPOS.CS;

namespace WpfSistemaPOS
{
    /// <summary>
    /// Lógica de interacción para BusquedaArticulo.xaml
    /// </summary>
    public partial class BusquedaArticulo : Window
    {
        public Articulo SelectedItem { get; private set; }


        public BusquedaArticulo(List<BsonDocument> items)
        {
            InitializeComponent();
            var articulos = items.Select(item => new Articulo
            {
                IdCompuesto = item.GetValue("IdCompuesto").AsString,
                Descripcion = item.GetValue("Descripcion").AsString,
                Precio = item.GetValue("Precio").ToDouble(),
                Existencia = item.GetValue("Existencia").ToInt32()
            }).ToList();

            resultsGrid.ItemsSource = articulos;
        }

        private void Aceptar_Click(object sender, RoutedEventArgs e)
        {
            SelectedItem = resultsGrid.SelectedItem as Articulo;
            DialogResult = true;
            Close();
        }

        private void Cancelar_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }

    //public class Articulo
    //{
    //    public string IdCompuesto { get; set; }
    //    public string Descripcion { get; set; }
    //    public double Precio { get; set; }
    //    public int Existencia { get; set; }
    //    public int Cantidad { get; set; }
    //}
}
