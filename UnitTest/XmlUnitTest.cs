using Classes;
using System.IO;
namespace UnitTest;

[TestClass]
public class XmlDocument_UnitTest
{
    private string _testXMLFilePath;
    private string _sampleXMLcontent;

    [TestInitialize]
    public void Setup()
    {
        // Create a temporary XML File to use the testing method
        _testXMLFilePath = Path.Combine(Path.GetTempPath(),"test.xml");

        // Create a mock content 
        _sampleXMLcontent = @"
            <root>
                <element>Peaches are gray</element>
                <element>More text</element>
            </root>";
        
        File.WriteAllText(_testXMLFilePath, _sampleXMLcontent);
    }
    
    [TestCleanup]
    public void Cleanup()
    {
        // Delete the temporary file after the test
        if (File.Exists(_testXMLFilePath)) File.Delete(_testXMLFilePath);
    }

    [TestMethod]
    public void Test_XmlDocument_ContentLoading()
    {
        var xmlDocument = new XmlDocument(_testXMLFilePath);
        // Check that the content was leaded and parsed properly.
        Assert.IsNotNull(xmlDocument.Content,"Content should not be null here.");
        Assert.IsTrue(xmlDocument.Content.Contains("Peaches are gray"), "Contents should contain 'Peaches are gray'.");
        Assert.IsTrue(xmlDocument.Content.Contains("More text"), "Should contain 'More text'.");
    }

    
}