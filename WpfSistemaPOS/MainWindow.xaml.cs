using System;
using System.Windows;
using WpfSistemaPOS;
using MongoDB.Bson;
using System.Windows.Input;


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
            Console.WriteLine("Click presionado");
            Console.WriteLine(sender);
            Console.WriteLine(e);
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
