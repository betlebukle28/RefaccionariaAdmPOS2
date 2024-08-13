using MongoDB.Bson;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using WpfSistemaPOS.CS;
using MongoDB.Driver;
using System.Threading.Tasks;


namespace WpfSistemaPOS
{
    public partial class BusquedaArticulo : Window
    {
        public Articulo SelectedItem { get; private set; }

        private  MongoDBService _mongoDBService;

        public BusquedaArticulo(List<BsonDocument> items, MongoDBService mongoDBService )
        {
            InitializeComponent();
            _mongoDBService = mongoDBService;  // Asignación del servicio
            UpdateDataGrid(items);
        }

        private void UpdateDataGrid(List<BsonDocument> items)
        {
            var articulos = items.Select(item => new Articulo
            {
                IdCompuesto = item.GetValue("IdCompuesto").AsString,
                Descripcion = item.GetValue("Descripcion").AsString,
                Precio = item.GetValue("Precio").ToDouble(),
                Existencia = item.GetValue("Existencia").ToInt32()
            }).ToList();

            resultsGrid.ItemsSource = articulos;
        }

        //private void UpdateDataGrid(List<BsonDocument> items)
        //{
        //    var articulos = items.Select(item => new Articulo
        //    {
        //        IdCompuesto = item.GetValue("IdCompuesto").AsString,
        //        Descripcion = item.GetValue("Descripcion").AsString,
        //        Precio = item.GetValue("Precio").ToDouble(),
        //        Existencia = item.GetValue("Existencia").ToInt32()
        //    }).ToList();

        //    resultsGrid.ItemsSource = articulos;
        //}

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

        private async void Buscar_Click(object sender, RoutedEventArgs e)
        {
            // Obtener los valores de los TextBoxes.
            string year = txtYear.Text;
            string model = txtModel.Text;
            string engine = txtEngine.Text;
            string version = txtVersion.Text;

            // Llamada al método SearchArticulosAsync de MongoDBService para obtener los artículos filtrados.
            var filteredItems = await _mongoDBService.SearchArticulosAsync(year, model, engine, version);
            
            // Actualizar el DataGrid con los artículos filtrados.
            UpdateDataGrid(filteredItems);
        }

        
    }
}
