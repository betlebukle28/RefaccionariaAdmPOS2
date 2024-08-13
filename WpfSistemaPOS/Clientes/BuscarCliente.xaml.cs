using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using WpfSistemaPOS.CS;

namespace WpfSistemaPOS.Clientes
{
    public partial class BuscarCliente : Window
    {
        private MongoDBService _mongoDBService;

        public Cliente SelectedCliente { get; private set; }

        public BuscarCliente(MongoDBService mongoDBService)
        {
            InitializeComponent();
            _mongoDBService = mongoDBService;
        }

        private async void Buscar_Click(object sender, RoutedEventArgs e)
        {
            var searchText = searchTextBox.Text;
            var clients = await _mongoDBService.SearchClientesAsync(searchText);
            var clientList = clients.Select(doc => new Cliente
            {
                Clv_Cliente = doc["Clv_Cliente"].AsString,
                NombreCliente = doc["NombreCliente"].AsString
            }).ToList();

            resultsGrid.ItemsSource = clientList;
        }

        private void Aceptar_Click(object sender, RoutedEventArgs e)
        {
            SelectedCliente = resultsGrid.SelectedItem as Cliente;
            if (SelectedCliente != null)
            {
                DialogResult = true;
                Close();
            }
            else
            {
                MessageBox.Show("Seleccione un cliente válido.");
            }
        }

        private void Cancelar_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
