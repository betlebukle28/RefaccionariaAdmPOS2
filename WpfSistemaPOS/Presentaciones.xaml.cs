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
using WpfSistemaPOS.Articulos;
using WpfSistemaPOS2;

namespace WpfSistemaPOS
{
    /// <summary>
    /// Lógica de interacción para Presentaciones.xaml
    /// </summary>
    public partial class Presentaciones : Window
    {
        public Presentaciones()
        {
            InitializeComponent();
        }
        private void BtnNuevaClave_Click(object sender, RoutedEventArgs e)
        {
            NuevaClave nuevaClaveWindow = new ();
            nuevaClaveWindow.Show();
            this.Close();
        }

        private void BtnConsultar_Click(object sender, RoutedEventArgs e)
        {
            ConsultarArticulo consultarWindow = new ();
            consultarWindow.Show();
            this.Close();
        }

        private void BtnModificar_Click(object sender, RoutedEventArgs e)
        {
            ModificarArticulo modificarWindow = new ();
            modificarWindow.Show();
            this.Close();
        }

        private void BtnEliminar_Click(object sender, RoutedEventArgs e)
        {
            EliminarArticulo eliminarWindow = new ();
            eliminarWindow.Show();
            this.Close();
        }
        //BtnRegresar_Click
        private void BtnRegresar_Click(object sender, RoutedEventArgs e)
        {
            Home home = new();
            home.Show();
            this.Close();
        }

    }
}
