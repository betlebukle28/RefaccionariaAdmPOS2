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
    /// Lógica de interacción para ModificarArticulo.xaml
    /// </summary>
    public partial class ModificarArticulo : Window
    {
        public ModificarArticulo()
        {
            InitializeComponent();
        }

        private void GuardarCambios_Click(object sender, RoutedEventArgs e)
        {
            // Aquí agregas la lógica para guardar los cambios en la base de datos
            MessageBox.Show("Cambios guardados exitosamente.");
        }

    }
}
