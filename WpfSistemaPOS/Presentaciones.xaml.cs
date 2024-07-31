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
            NuevaClave nuevaClaveWindow = new NuevaClave();
            nuevaClaveWindow.Show();
            this.Close();
        }

        private void BtnConsultar_Click(object sender, RoutedEventArgs e)
        {
            Consultar consultarWindow = new Consultar();
            consultarWindow.Show();
            this.Close();
        }

        private void BtnModificar_Click(object sender, RoutedEventArgs e)
        {
            Modificar modificarWindow = new Modificar();
            modificarWindow.Show();
            this.Close();
        }

        private void BtnEliminar_Click(object sender, RoutedEventArgs e)
        {
            Eliminar eliminarWindow = new Eliminar();
            eliminarWindow.Show();
            this.Close();
        }
    }
}
