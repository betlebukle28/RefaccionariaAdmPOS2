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
using WpfSistemaPOS;

namespace WpfSistemaPOS2
{
    /// <summary>
    /// Lógica de interacción para Home.xaml
    /// </summary>
    public partial class Home : Window
    {
        public Home()
        {
            InitializeComponent();
        }

        private void Pantalla_Clientes(object sender, RoutedEventArgs e)
        {
            GestionClientes gestionclientes = new();
            gestionclientes.Show();
            this.Close();
        }

        private void Pantalla_PuntoDeVenta(object sender, RoutedEventArgs e)
        {
            PuntoDeVentaClasico puntoDeVentaClasico = new ();
            puntoDeVentaClasico.Show();
            this.Close();
        }

        private void Pantalla_Articulo(object sender, RoutedEventArgs e)
        {
            NuevaClave nuevaClave = new();
            nuevaClave.Show();
            this.Close();
        }
    }
}
