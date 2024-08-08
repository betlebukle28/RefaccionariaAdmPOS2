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
    /// Lógica de interacción para NuevaClave.xaml
    /// </summary>
    public partial class NuevaClave : Window
    {
        public NuevaClave()
        {
            InitializeComponent();
        }

        private void BtnAceptar_Click(object sender, RoutedEventArgs e)
        {
            // Lógica para guardar la nueva clave
            MessageBox.Show("Nueva clave guardada con éxito.");
            this.Close();
        }

        private void BtnCancelar_Click(object sender, RoutedEventArgs e)
        {
            Presentaciones presentaciones = new ();
            presentaciones.Show();
            this.Close();
        }

    }
}
