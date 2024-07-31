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

namespace WpfSistemaPOS
{
    /// <summary>
    /// Lógica de interacción para PuntoDeVentas.xaml
    /// </summary>
    public partial class PuntoDeVentas : Window
    {
        public PuntoDeVentas()
        {
            InitializeComponent();

            private void BtnPresentaciones_Click(object sender, RoutedEventArgs e)
            {
                Presentaciones presentacionesWindow = new Presentaciones();
                presentacionesWindow.Show();
                this.Close(); // Cierra la ventana actual si es necesario
            }

        }
    }
}
