namespace UnitTest;
using Classes;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Layout;
using iText.Layout.Element;
using System.IO;


[TestClass]
public class PDFDocument_UnitTest
{
    private string _testFilePath;
    private string _sampleContent;

    [TestInitialize]
    public void Setup()
    {
        _testFilePath = Path.Combine(Path.GetTempPath(), "test.pdf");
        _sampleContent = @"I love Pokemon Go Like Crazy I am way to tired.";
        CreatePDF(_testFilePath, _sampleContent);
        Console.WriteLine($"PDF created at {_testFilePath}");
    }

    [TestMethod]
    public void Test_PDFDoc_GetFileContents()
    {
        // Arrange
        var pdfDoc = new PDFDocument(_testFilePath);

        // Act and Assert
        Assert.AreEqual(_sampleContent, pdfDoc.Content.Trim()); // Trim to avoid whitespace issues
    }

    // Helper Method for creating the PDF sample
    public static void CreatePDF(string filePath, string content)
    {
        using (var writer = new PdfWriter(filePath))
        using (var pdfDoc = new PdfDocument(writer))
        {
            var document = new iText.Layout.Document(pdfDoc);
            var text = new Paragraph(content)
                .SetFontSize(12)
                .SetTextAlignment(iText.Layout.Properties.TextAlignment.JUSTIFIED);
            document.Add(text);
            document.Close();
        }
    }

    [TestCleanup]
    public void Cleanup()
    {
        if (File.Exists(_testFilePath)) File.Delete(_testFilePath);
    }
}