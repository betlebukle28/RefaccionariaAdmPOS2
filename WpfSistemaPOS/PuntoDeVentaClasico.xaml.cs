using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using WpfSistemaPOS2;
using WpfSistemaPOS.CS;

using WpfSistemaPOS.PuntoDeVenta;
using System;
using MongoDB.Bson;
using WpfSistemaPOS.Clientes;

namespace WpfSistemaPOS
{
    /// <summary>
    /// Lógica de interacción para PuntoDeVentaClasico.xaml
    /// </summary>
    public partial class PuntoDeVentaClasico : Window
    {
        private readonly MongoDBService _mongoDBService;
        private string NombreCliente = "";

        public PuntoDeVentaClasico()
        {
            InitializeComponent();
            _mongoDBService = new MongoDBService();
            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.Focus();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F5)
            {
                Console.WriteLine("F5 presionado");
                AbrirCajaDeCobro();
            }
        }

        private void AbrirCajaDeCobro()
        {
            double total = CalculateTotal();  // Asumiendo que tienes un método para calcular el total.
            string Cliente = NombreCliente; //txtClvCliente.Text;
            string Vendedor = "SYS";
            var cajaDeCobro = new CajaDeCobro(total, Cliente, Vendedor);  // Pasa el total al constructor de CajaDeCobro.
            cajaDeCobro.ShowDialog();
        }

        private double CalculateTotal()
        {
            return dataGrid.Items.OfType<Articulo>().Sum(articulo => articulo.Precio * articulo.Cantidad);
        }

        private async void SearchBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                string searchText = searchBox.Text;

                // Buscar por IdCompuesto exacto
                var itemByIdCompuesto = await _mongoDBService.GetItemByIdCompuestoAsync(searchText);
                if (itemByIdCompuesto != null)
                {
                    AddItemToGrid(new Articulo
                    {
                        IdCompuesto = itemByIdCompuesto.GetValue("IdCompuesto").AsString,
                        Descripcion = itemByIdCompuesto.GetValue("Descripcion").AsString,
                        Precio = itemByIdCompuesto.GetValue("Precio").ToDouble(),
                        Existencia = itemByIdCompuesto.GetValue("Existencia").ToInt32(),
                        Cantidad = 1,
                        Importe = itemByIdCompuesto.GetValue("Precio").ToDouble(),
                    });
                }
                else
                {
                    // Buscar por IdCompuesto o Descripcion parcial
                    var itemsByPartialText = await _mongoDBService.GetItemsByPartialIdOrDescriptionAsync(searchText);
                    if (itemsByPartialText.Any())
                    {
                        var busquedaArticuloWindow = new BusquedaArticulo(itemsByPartialText, _mongoDBService); // Asegúrate de pasar _mongoDBService aquí
                        if (busquedaArticuloWindow.ShowDialog() == true)
                        {
                            var selectedItem = busquedaArticuloWindow.SelectedItem;
                            if (selectedItem != null)
                            {
                                selectedItem.Cantidad = 1;
                                selectedItem.Importe = selectedItem.Precio;
                                AddItemToGrid(selectedItem);
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("No se encontraron artículos.", "Búsqueda", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
        }


        private void AddItemToGrid(Articulo item)
        {
            var items = dataGrid.ItemsSource as List<Articulo> ?? new List<Articulo>();

            // Verificar si el artículo ya está en el carrito
            var existingItem = items.FirstOrDefault(x => x.IdCompuesto == item.IdCompuesto);
            if (existingItem != null)
            {
                existingItem.Importe = 0;
                existingItem.Cantidad += item.Cantidad;
                //item.Importe = items.Sum(x => x.Precio * x.Cantidad);
                existingItem.Importe += existingItem.Cantidad * existingItem.Precio;
            }
            else
            {
                items.Add(item);
            }

            dataGrid.ItemsSource = items;
            dataGrid.Items.Refresh();

            UpdateTotalAndUnits(items);
        }

        private void UpdateTotalAndUnits(List<Articulo> items)
        {
            int totalUnidades = items.Sum(x => x.Cantidad);
            double totalMN = items.Sum(x => x.Precio * x.Cantidad);
            unidadesTextBox.Text = totalUnidades.ToString();
            totalTextBlock.Text = totalMN.ToString("F2");
        }

        private void Pagina_Inicio(object sender, RoutedEventArgs e)
        {
            Home home = new();
            home.Show();
            this.Close();
        }

        private void Buscar_Cliente(object sender, RoutedEventArgs e)
        {
            var buscarClienteWindow = new BuscarCliente(_mongoDBService);
            if (buscarClienteWindow.ShowDialog() == true)
            {
                var selectedCliente = buscarClienteWindow.SelectedCliente;
                if (selectedCliente != null)
                {
                    txtClvCliente.Text = selectedCliente.Clv_Cliente; // Usamos directamente la propiedad Clv_Cliente del objeto Cliente
                    NombreCliente = selectedCliente.NombreCliente;
                }
            }
        }

    }
}
