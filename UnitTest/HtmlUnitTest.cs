using Classes;
using HtmlAgilityPack;
namespace UnitTest;

[TestClass]
public class HtmlDocument_UnitTest
{
    private string _testFilePath;
    private string _sampleContent;

    [TestInitialize]
    public void Setup()
    {
        _testFilePath = Path.Combine(Path.GetTempPath(), "test.html");
        _sampleContent = "<html><body><h1>The beautiful world.</h1><p>The Garden of Death.</p></body></html>";
        File.WriteAllText(_testFilePath, _sampleContent);
    }

    [TestCleanup]
    public void Cleanup()
    {
        if (File.Exists(_testFilePath)) File.Delete(_testFilePath);
    }

    [TestMethod]
    public void Test_HtmlDoc_GetFileContents()
    {
        var htmlDoc = new Classes.HtmlDocument(_testFilePath);
        var expectedResult = "The beautiful world.The Garden of Death.";
        Assert.AreEqual(expectedResult, htmlDoc.Content.Trim());
    }
}