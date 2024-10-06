using Classes;
using Newtonsoft.Json.Linq;
namespace UnitTest;

[TestClass]
public class JsonDocument_UnitTest
{
    private string _testFilePath;
    private string _sampleContent;

    [TestInitialize]
    public void Setup()
    {
        _testFilePath = Path.Combine(Path.GetTempPath(),"test.json");
        _sampleContent = "{\"name\": \"test\", \"age\": 30}";
        File.WriteAllText(_testFilePath, _sampleContent);
    }

    [TestMethod]
    public void Test_JsonDoc_GetFileContents()
    {
        var jsonDoc = new JsonDocument(_testFilePath);
        //expected content 
        JObject expectedContent = JObject.Parse(_sampleContent);
        Assert.AreEqual(expectedContent.ToString(), jsonDoc.Content);
    }

    [TestCleanup]
    public void Cleanup()
    {
        if (File.Exists(_testFilePath)) File.Delete(_testFilePath);
    }
}