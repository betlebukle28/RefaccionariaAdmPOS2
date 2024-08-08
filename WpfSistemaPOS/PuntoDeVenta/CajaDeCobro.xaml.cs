using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;


namespace WpfSistemaPOS.PuntoDeVenta
{
    /// <summary>
    /// Lógica de interacción para CajaDeCobro.xaml
    /// </summary>
    public partial class CajaDeCobro : Window
    {
        public CajaDeCobro(double total)
        {
            InitializeComponent();
            txtTotal.Text = total.ToString("F2");
        }

        private void CbTipoPago_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Habilita el textbox para el pago en efectivo si es necesario
            txtPagoCon.IsEnabled = ((ComboBoxItem)cbTipoPago.SelectedItem).Content.ToString() == "Efectivo";
        }

        private void Aceptar_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtTotal.Text) || cbTipoPago.SelectedItem == null)
            {
                MessageBox.Show("Por favor completa todos los campos requeridos.");
                return;
            }

            double pagoCon = 0;
            if (txtPagoCon.IsEnabled && !double.TryParse(txtPagoCon.Text, out pagoCon))
            {
                MessageBox.Show("Por favor introduce una cantidad válida.");
                return;
            }

            double total = double.Parse(txtTotal.Text);
            if (pagoCon < total && txtPagoCon.IsEnabled)
            {
                MessageBox.Show("El monto introducido es insuficiente.");
                return;
            }

            txtCambio.Text = (pagoCon - total).ToString("F2");
            MessageBox.Show("Pago procesado correctamente.");
            this.Close();
            // Aquí podrías cerrar la ventana o hacer otra acción deseada.
        }

        private void Cancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
