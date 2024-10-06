using Classes;
namespace UnitTest;

[TestClass]
public class Custom_IndexingMethod_UnitTest
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
    
    // Should return a value of the relevance of each word 
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

[TestClass]
public class TFIDF_IndexingMethod_UnitTest
{
    private Document _mockDoc1;
    private Document _mockDoc2;
    private TFIDF _vectorizer;

    [TestInitialize]
    public void Setup()
    {
        _vectorizer = new TFIDF();

        // Mock documents with data to test
        _mockDoc1 = new Query("fox dog fort.");
        _mockDoc2 = new Query("mock dog fire.");
    }

    // Should retrieve the IDF of the document 
    [TestMethod]
    public void Test_VectorizingMulDocuments()
    {
        var documents = new List<Document> {_mockDoc1, _mockDoc2};
        var expectedVector = new Dictionary<string, double>
        {
            {"fox", 0},
            {"dog", Math.Log((double) documents.Count / (double)(1 + 2))},
            {"fort", 0},
            {"mock", 0},
            {"fire", 0}
        };

        // Act
        var result = _vectorizer.VectorizeDocuments(documents);
        foreach (var kvp in expectedVector)
        {
                Assert.IsTrue(result.ContainsKey(kvp.Key), $"Result does not contain key: {kvp.Key}");
                Assert.AreEqual(kvp.Value, result[kvp.Key], 0.001, $"IDF value for '{kvp.Key}' is not as expected.");
        }
    }

    [TestMethod]
    public void Test_VectorizingSingleDocument()
    {   
        var documents = new List<Document> {_mockDoc1, _mockDoc2};
        var vector = _vectorizer.VectorizeDocuments(documents);
        var expectedVector = new Dictionary<string, double> 
        {
            {"fox", 0},
            {"dog", vector["dog"]*(1.0/3)},
            {"fort", 0},
            {"mock", 0},
            {"fire", 0}
        };
        var result = _vectorizer.VectorizeDocument(_mockDoc1);
        foreach (var kvp in expectedVector)
        {
                Assert.IsTrue(result.ContainsKey(kvp.Key), $"Result does not contain key: {kvp.Key}");
                Assert.AreEqual(kvp.Value, result[kvp.Key], 0.001, $"IDF value for '{kvp.Key}' is not as expected.");
        }
    }
}