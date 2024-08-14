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

namespace WpfSistemaPOS.Articulos
{
    /// <summary>
    /// Lógica de interacción para ConsultarArticulo.xaml
    /// </summary>
    public partial class ConsultarArticulo : Window
    {
        public ConsultarArticulo()
        {
            InitializeComponent();
        }
        private void Consultar_Click(object sender, RoutedEventArgs e)
        {
            // Simulación de búsqueda de artículo
            var articulo = new
            {
                IdCompuesto = "AB-000001-01",
                Descripcion = "Artículo de prueba",
                Precio = 10,
                Existencia = 15,
                Categoria = "Pieza"
            };

            // Mostrar en ListView
            lvResultados.Items.Clear();
            lvResultados.Items.Add(articulo);

            // Simulación de compatibilidad
            lbCompatibilidad.Items.Clear();
            lbCompatibilidad.Items.Add("Vehículo A");
            lbCompatibilidad.Items.Add("Vehículo B");
        }

    }
}
