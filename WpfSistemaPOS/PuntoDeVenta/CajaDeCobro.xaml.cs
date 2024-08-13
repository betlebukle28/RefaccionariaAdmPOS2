using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using iText.Layout;
using iText.Layout.Element;
using PdfSharp.Drawing;
using PdfSharp.Pdf;


namespace WpfSistemaPOS.PuntoDeVenta
{
    public partial class CajaDeCobro : Window
    {
        private readonly MongoDBService _mongoDBService;

        public CajaDeCobro(double total, string cliente, string vendedor)
        {
            InitializeComponent();
            txtTotal.Text = total.ToString("F2");
            txtCliente.Text = cliente;
            txtVendedor.Text = vendedor;
            _mongoDBService = new MongoDBService();

        }

        private void CbTipoPago_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            txtPagoCon.IsEnabled = ((ComboBoxItem)cbTipoPago.SelectedItem).Content.ToString() == "Efectivo";
        }

        private async void Aceptar_Click(object sender, RoutedEventArgs e)
        {
            await AceptarAsync();
        }

        private async Task AceptarAsync()
        {
            if (string.IsNullOrEmpty(txtTotal.Text) || cbTipoPago.SelectedItem == null)
            {
                MessageBox.Show("Por favor completa todos los campos requeridos.");
                return;
            }

            double importe = double.Parse(txtTotal.Text); // Total a cobrar
            double pagoCon = 0;
            if (txtPagoCon.IsEnabled && !double.TryParse(txtPagoCon.Text, out pagoCon))
            {
                MessageBox.Show("Por favor introduce una cantidad válida.");
                return;
            }

            if (pagoCon < importe && txtPagoCon.IsEnabled)
            {
                MessageBox.Show("El monto introducido es insuficiente.");
                return;
            }

            txtCambio.Text = (pagoCon - importe).ToString("F2");

            string sucursal = "1"; // Supón que este es el ID de la sucursal
            string claveCorte = await GenerarClaveCorte(sucursal);
            string vendedor = txtVendedor.Text;
            string cliente = txtCliente.Text;
            string tipoPago = ((ComboBoxItem)cbTipoPago.SelectedItem).Content.ToString();

            // Guardar en MongoDB
            var documento = new BsonDocument
            {
                { "Clv_Corte", claveCorte },
                { "Descripcion", "Descripción de los artículos vendidos" },
                { "Fecha", DateTime.UtcNow },
                { "Importe", importe },
                { "Sucursal", sucursal },
                { "Clv_Cliente", cliente }
            };
            await _mongoDBService.InsertItemAsync("cortes", documento);

            // Abrir reporte
            GenerarReportePDF(sucursal, importe, vendedor, cliente, tipoPago);

            MessageBox.Show("Pago procesado correctamente.");
            this.Close();
        }

        private async Task<string> GenerarClaveCorte(string sucursalId)
        {
            var collection = _mongoDBService.GetCollection<BsonDocument>("Contador_Cortes");
            var update = Builders<BsonDocument>.Update.Inc("contador", 1);
            var options = new FindOneAndUpdateOptions<BsonDocument>
            {
                ReturnDocument = ReturnDocument.After,
                IsUpsert = true
            };
            var result = await collection.FindOneAndUpdateAsync(
                Builders<BsonDocument>.Filter.Eq("sucursalId", sucursalId),
                update,
                options);

            return $"{result["contador"].AsInt32}-{sucursalId}";
        }

        private async Task<string> GetSucursal(string sucursalId)
        {
            var collection = _mongoDBService.GetCollection<BsonDocument>("Sucursal");
            var filter = Builders<BsonDocument>.Filter.Eq("Clv_Sucursal", sucursalId);
            var sucursalDocument = await collection.Find(filter).FirstOrDefaultAsync();

            if (sucursalDocument != null && sucursalDocument.Contains("NombreSucursal"))
            {
                return sucursalDocument["NombreSucursal"].AsString;
            }

            return "Sucursal no encontrada";
        }


        public async Task GenerarReportePDF(string sucursal, double importe, string vendedor, string cliente, string tipoPago)
        {
            string sucursalId = "1";
            string nombreSucursal =  await GetSucursal(sucursalId);

            // Define el nombre del archivo PDF
            string fileName = "Ticket" + Guid.NewGuid().ToString() + ".pdf";

            // Crear un nuevo documento PDF
            PdfDocument document = new PdfDocument();
            document.Info.Title = "Reporte de Cobro";

            // Crear una nueva página en el documento con tamaño personalizado
            PdfPage page = document.AddPage();
            page.Width = XUnit.FromCentimeter(8);  // Ancho de 8 cm (aproximadamente 3 pulgadas)
            page.Height = XUnit.FromCentimeter(20); // Ajusta la altura según tus necesidades

            XGraphics gfx = XGraphics.FromPdfPage(page);

            // Definir la fuente y estilo
            XFont titleFont = new XFont("Verdana", 14, XFontStyleEx.Bold);
            XFont regularFont = new XFont("Verdana", 10, XFontStyleEx.Regular);
            XFont smallFont = new XFont("Verdana", 8, XFontStyleEx.Regular);

            // Añadir contenido al PDF con un diseño más profesional
            double marginTop = 20;
            double lineHeight = 15;

            gfx.DrawString("**Reporte de Cobro**", titleFont, XBrushes.Black,
              new XRect(0, marginTop, page.Width, lineHeight),
              XStringFormats.TopCenter);

            marginTop += lineHeight + 10; 

            gfx.DrawString($"Sucursal: {nombreSucursal}", regularFont, XBrushes.Black,
              new XRect(10, marginTop, page.Width - 20, lineHeight),
              XStringFormats.TopLeft);

            marginTop += lineHeight;

            gfx.DrawString($"Importe: ${importe:F2}", regularFont, XBrushes.Black,
              new XRect(10, marginTop, page.Width - 20, lineHeight),
              XStringFormats.TopLeft);

            marginTop += lineHeight;

            gfx.DrawString($"Vendedor: {vendedor}", regularFont, XBrushes.Black,
              new XRect(10, marginTop, page.Width - 20, lineHeight),
              XStringFormats.TopLeft);

            marginTop += lineHeight;

            gfx.DrawString($"Cliente: {cliente}", regularFont, XBrushes.Black,
              new XRect(10, marginTop, page.Width - 20, lineHeight),
              XStringFormats.TopLeft);

            marginTop += lineHeight;

            gfx.DrawString($"Tipo de Pago: {tipoPago}", regularFont, XBrushes.Black,
              new XRect(10, marginTop, page.Width - 20, lineHeight),
              XStringFormats.TopLeft);

            marginTop += lineHeight;

            gfx.DrawString($"Fecha: {DateTime.Now.ToString("dd/MM/yyyy")}", smallFont, XBrushes.Black,
              new XRect(10, marginTop, page.Width - 20, lineHeight),
              XStringFormats.TopLeft);

            marginTop += lineHeight + 20; // Espacio antes de la línea final

            gfx.DrawLine(XPens.Black, 10, marginTop, page.Width - 10, marginTop); // Línea horizontal

            // Guardar el documento en el archivo
            document.Save(fileName);

            // Abre el PDF con el lector predeterminado
            AbrirArchivoPDF(fileName);
        }

        private void AbrirArchivoPDF(string filePath)
        {
            // Usa Process para abrir el archivo con el lector de PDF predeterminado del sistema
            try
            {
                Process.Start(new ProcessStartInfo(filePath) { UseShellExecute = true });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"No se pudo abrir el archivo PDF. Error: {ex.Message}");
            }
        }

        private void Cancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
