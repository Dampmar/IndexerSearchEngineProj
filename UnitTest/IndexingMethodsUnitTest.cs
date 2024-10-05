using Classes;
namespace UnitTest;

[TestClass]
public class CustomIndexingMethodUnitTest
{
    private Document _mockDoc1;
    private Document _mockDoc2;
    private NormalizedBagOfWords _vectorizer; 

    [TestInitialize]
    public void Setup()
    {
        _vectorizer = new NormalizedBagOfWords();

        // Mock documents with data to test
        _mockDoc1 = new Query("The quick brown fox jumps, 'over' the old fat dog dog.");
        _mockDoc2 = new Query("The lazy dog jumps over the fox.");
    }

    [TestMethod]
    public void Test_VectorizingSingleDocument()
    {
        // Expected Results 
        var expectedVector = new Dictionary<string, double> 
        {
            {"quick", 1.0 / 8}, {"brown", 1.0 / 8},{"fox", 1.0 / 8},
            {"jump", 1.0 / 8},{"old", 1.0 / 8}, {"fat", 1.0 / 8},
            {"dog", 2.0 / 8}
        };

        var result = _vectorizer.VectorizeDocument(_mockDoc1);
        CollectionAssert.AreEquivalent(result, expectedVector);
    }
    
    [TestMethod]
    public void Test_VectorizingMulDocuments()
    {
        var documents = new List<Document> {_mockDoc1, _mockDoc2};
        var results = _vectorizer.VectorizeDocuments(documents);

        // Expected results
        var expectedResult = new Dictionary<string, double>
        {
            {"quick", 1.0 / 8},
            {"brown", 1.0 / 8},
            {"fox", 1.0 / 8 + 1.0 / 4},
            {"jump", 1.0 / 8 + 1.0 / 4},
            {"dog", 2.0 / 8 + 1.0 / 4},
            {"lazi", 1.0 / 4},
            {"old", 1.0 / 8}, 
            {"fat", 1.0 / 8},
        };
        // Compare dictionary contents
        CollectionAssert.AreEquivalent(expectedResult, results);
    }      
}