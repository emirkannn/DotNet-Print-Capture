using System;
using System.Windows.Forms;
using System.IO;
using iText.Html2pdf;
using iText.Kernel.Pdf;
using iText.Layout;

namespace yazici2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void yazdir_Click(object sender, EventArgs e)
        {
            // HTML dosyasını seçme işlemi
            
            string mutlakYol = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "kantar.html");

            // Dosyadaki HTML içeriğini okuma
            string htmlContent = File.ReadAllText(mutlakYol);

            // HTML içeriğini PDF dosyasına dönüştür
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.FileName = "kantar.pdf";
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "  " + saveFileDialog.FileName;

            DialogResult result = MessageBox.Show("Çıktı almak istiyor musunuz?", "Print Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            try
            {
                if (result == DialogResult.Yes)

                {
                    string pdfFilePath = saveFileDialog.InitialDirectory;
                    
                    ConvertHtmlToPdf(htmlContent, pdfFilePath);

                    // PDF dosyasını yazdır
                    PrintPdf(pdfFilePath);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Yazdırma işlemi sırasında bir hata oluştu. \n" + ex.ToString(), "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConvertHtmlToPdf(string htmlContent, string pdfFilePath)
        {


            // PDF dosyasını oluşturmak ve içeriğini doldurmak için FileStream oluşturulur.
            using (var stream = new FileStream(pdfFilePath, FileMode.Create))
            {
                // PDF yazıcısı oluşturulur ve FileStream'e bağlanır.
                var writer = new PdfWriter(stream);

                // PDF belgesi oluşturulur ve yazıcıya bağlanır.
                var pdf = new PdfDocument(writer);

                // PDF belgesinin düzenini oluşturmak için Document nesnesi oluşturulur.
                var document = new Document(pdf);

                // HTML içeriğini PDF dosyasına çevirmek için iText HtmlConverter kullanılır.
                HtmlConverter.ConvertToPdf(htmlContent, pdf, new ConverterProperties());

                // PDF belgesi kapatılır, kaynakları serbest bırakılır.
                document.Close();
            }

                MessageBox.Show("PDF başarıyla oluşturuldu: " + pdfFilePath, "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void PrintPdf(string pdfFilePath)
        {

            var pdf = IronPdf.PdfDocument.FromFile(pdfFilePath);
            pdf.Print(100);

            
                
            MessageBox.Show("PDF başarıyla yazıcıya gönderildi", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }

