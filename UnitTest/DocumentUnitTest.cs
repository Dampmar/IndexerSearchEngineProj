using Classes;
namespace UnitTest;

[TestClass]
public class DocumentUnitTest
{
    private Document _mockDocument;

    [TestInitialize]
    public void Setup()
    {
        _mockDocument = new Query("The quick brown fox jumps, 'over' the old fat dog dog.");
    }

    [TestMethod]
    public void Test_TermsAreStemmed()
    {
        Console.WriteLine(string.Join(", ", _mockDocument.GetTerms()));
        // Expected Terms after Stemming, Stop Word and Split Char Removal 
        var expectedTerms = new List<string> {"quick", "brown", "fox", "jump", "old", "fat", "dog", "dog"};
        CollectionAssert.AreEqual(expectedTerms, _mockDocument.GetTerms());
    }

    [TestMethod]
    public void Test_UniqueTermsReturned()
    {
        // Expected Terms should be unique instances of the terms 
        var expectedUniqueTerms = new List<string> {"quick", "brown", "fox", "jump", "old", "fat", "dog"};
        CollectionAssert.AreEqual(expectedUniqueTerms, _mockDocument.GetUniqueTerms());
    }

    [TestMethod]
    public void Test_StopWordsRemoved()
    {
        var terms = _mockDocument.GetTerms();
        // Ensuring that the terms are properly removed 
        Assert.IsFalse(terms.Contains("the"));
        Assert.IsFalse(terms.Contains("over"));
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void Test_EmptyDocument_ProducesNoTerm()
    {
        var emptyDocument = new Query("");
    }

}