using Classes;
namespace UnitTest;

[TestClass]
public class TxtDOcument_UnitTest
{
    private string _testFilePath;
    private string _sampleTXTcontent;

    [TestInitialize]
    public void Setup()
    {
        _testFilePath = Path.Combine(Path.GetTempPath(),"test.txt");
        _sampleTXTcontent = "The Peaches are gray in the garden of death.";
        File.WriteAllText(_testFilePath, _sampleTXTcontent);
    }

    [TestCleanup]
    public void Cleanup()
    {
        if (File.Exists(_testFilePath)) File.Delete(_testFilePath);
    }

    [TestMethod]
    public void Test_TXTDocument_ContentLoading()
    {
        var txtDoc = new TxtDocument(_testFilePath);
        // Same checking as the XML Document class
        Assert.IsNotNull(txtDoc.Content, "Content should not be null here.");
        Assert.IsTrue(txtDoc.Content.Contains("Peaches are gray"), "Contents should contain 'Peaches are gray'.");
    }
}
