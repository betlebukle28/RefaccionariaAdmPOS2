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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfSistemaPOS;
using MongoDB.Bson;

namespace WpfSistemaPOS2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private readonly MongoDBService _mongoDBService;

        public MainWindow()
        {
            InitializeComponent();
            _mongoDBService = new MongoDBService();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            string usuario = txtUsuario.Text;
            string password = txtPassword.Password;



            bool isValidUser = await _mongoDBService.VerifyUserCredentialsAsync(usuario, password);

            if (isValidUser)
            {
               // MessageBox.Show("Login exitoso", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);

                // Cerrar la ventana actual y abrir la nueva
                Home home = new();
                home.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Usuario o contraseña incorrectos ", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
