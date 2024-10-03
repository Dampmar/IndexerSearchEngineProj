using Classes;
namespace UnitTest;

[TestClass]
public class SimilarityUnitTest
{
    private Similarity _euclidean; 
    private Similarity _cosine;

    [TestInitialize]
    public void Setup()
    {
        _euclidean = new EuclideanSimilarity();
        _cosine = new CosineSimilarity();
    }

    [TestMethod]
    public void EuclideanSim_NullVectors_ReturnZero()
    {
        var result = _euclidean.CalculateSimilarity(null, null);
        Assert.AreEqual(0.0, result);
    }

    [TestMethod]
    public void EuclideanSim_EqualVectors_ReturnsOne()
    {
        // Static creation of vectors 
        var vector = new Dictionary<string, double> { { "word1", 0.35 }, { "word2", 0.65 } };
        var result = _euclidean.CalculateSimilarity(vector, vector);
        Assert.AreEqual(1.0, result);
    }

    [TestMethod]
    public void EuclideanSim_DiffVectors_ReturnExpectedVal()
    {   
        // Vector creations, difference between the two is of sqrt(2)
        var vector1 = new Dictionary<string, double> { { "word1", 1.0 }, { "word2", 2.0 } };
        var vector2 = new Dictionary<string, double> { { "word1", 2.0 }, { "word2", 3.0 } };

        var result = _euclidean.CalculateSimilarity(vector1, vector2);
        var expectedValue = 1 / (1 + Math.Sqrt(2));
        Assert.AreEqual(expectedValue, result, 0.0001); // Tolerance for floating point comparisons 
    }

    [TestMethod]
    public void CosineSim_NullVectors_ReturnsZero()
    {
        var result = _cosine.CalculateSimilarity(null, null);
        Assert.AreEqual(0.0, result);
    }

    [TestMethod]
    public void CosineSim_EqualVectors_ReturnsOne()
    {
        var vector = new Dictionary<string, double> { { "word1", 0.35 }, { "word2", 0.65 } };
        var result = _cosine.CalculateSimilarity(vector, vector);
        Assert.AreEqual(1.0, result);
    }

    [TestMethod] 
    public void CosineSim_DiffVectors_ReturnsZero()
    {
        // Using orthogonal vectors for checking validity
        var vector1 = new Dictionary<string, double> { { "word1", 1.0 }, { "word2", 0.0 } };
        var vector2 = new Dictionary<string, double> { { "word1", 0.0 }, { "word2", 1.0 } };
        var result = _cosine.CalculateSimilarity(vector1, vector2);
        Assert.AreEqual(0.0, result);
    }
}