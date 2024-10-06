using Classes;
using System.IO;
namespace UnitTest;

[TestClass]
public class CsvDocument_UnitTest
{
    private string _testFilePath;
    private string _sampleContent;

    [TestInitialize]
    public void Setup()
    {
        _testFilePath = Path.Combine(Path.GetTempPath(),"test.csv");
        _sampleContent = "Name, Age, City, Fortnite,";
        File.WriteAllText(_testFilePath, _sampleContent);
    }

    [TestCleanup]
    public void Cleanup()
    {
        if (File.Exists(_testFilePath)) File.Delete(_testFilePath);
    }

    [TestMethod]
    public void Test_CsvDocument_ContentLoading()
    {
        var csvDoc = new CsvDocument(_testFilePath);
        Assert.IsNotNull(csvDoc.Content, "Content shouldn't be NULL.");
        Assert.IsTrue(csvDoc.Content.Contains("Fortnite"), "Contents should contain 'Fortnite'.");
    }
}