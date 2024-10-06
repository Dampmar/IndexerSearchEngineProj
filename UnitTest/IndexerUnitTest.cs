using Classes;
namespace UnitTest;

[TestClass]
public class Indexer_UnitTest
{
    private Indexer _indexer;
    private string _testFolderPath; 
    private string _indexFilePath;

    [TestInitialize]
    public void Setup()
    {
        _indexer = new Indexer();
        // Create a temporary folder to hold test files 
        _testFolderPath = Path.Combine(Path.GetTempPath(), "TestFolder");
        Directory.CreateDirectory(_testFolderPath);
        // Create test files in the folder 
        File.WriteAllText(Path.Combine(_testFolderPath, "file1.txt"), "This is a test document about dogs.");
        File.WriteAllText(Path.Combine(_testFolderPath, "file2.txt"), "Another mock document, about foxes.");
        File.WriteAllText(Path.Combine(_testFolderPath, "file3.txt"), "Yet Another mock document, about cats.");

        _indexFilePath = Path.Combine(_testFolderPath, "index.json");
    }

    [TestCleanup]
    public void Cleanup()
    {
        if (Directory.Exists(_testFolderPath)) Directory.Delete(_testFolderPath, true);
    }

    [TestMethod]
    public void Test_IndexingFolder_AllCombinations()
    {
        // Testing that all combinations work purposefully 
        _indexer.IndexFolder(_testFolderPath, "tfidf", "cosine");
        Assert.AreEqual(3, _indexer.Documents.Count, "The number of indexed documents should be 3.");
        _indexer.IndexFolder(_testFolderPath, "tfidf", "euclidean");
        Assert.AreEqual(3, _indexer.Documents.Count, "The number of indexed documents should be 3.");
        _indexer.IndexFolder(_testFolderPath, "vectorizer", "cosine");
        Assert.AreEqual(3, _indexer.Documents.Count, "The number of indexed documents should be 3.");
        _indexer.IndexFolder(_testFolderPath, "vectorizer", "euclidean");
        Assert.AreEqual(3, _indexer.Documents.Count, "The number of indexed documents should be 3.");
    }

    [TestMethod]
    public void Test_SaveIndexToJson()
    {
        // Act: Index the folder and check if the save indexed folder exists 
        _indexer.IndexFolder(_testFolderPath, "tfidf", "cosine");
        Assert.IsTrue(File.Exists(_indexFilePath), "The index.json file should exist.");
    }

    [TestMethod]
    public void Test_LoadIndexFromJson()
    {
        // Create the Json Folder 
        _indexer.IndexFolder(_testFolderPath, "tfidf", "cosine");
        var newIndexer = new Indexer();
        newIndexer.LoadIndex(_indexFilePath);

        // This should imply file creation was handled properly 
        Assert.AreEqual(3, newIndexer.Documents.Count, "The loaded index should contain 3 documents.");
    }

    [TestMethod]
    public void Test_SearchDocuments_Cosine()
    {   
        // Create indexer 
        _indexer.IndexFolder(_testFolderPath, "tfidf", "cosine");
        var searchResults = _indexer.Search("fox", 2, "cosine");
        _indexer.LoadIndex(_indexFilePath);

        // Assert: Check the search results
        Assert.AreEqual(1, searchResults.Count, "There should be 2 search results for the query 'fox'.");
        Assert.IsTrue(searchResults.Contains("file2.txt"), "file1.txt should be in the search results.");
        Assert.IsFalse(searchResults.Contains("file3.txt"), "file3.txt should be in the search results.");
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void Test_SearchNullDocuments()
    {
        // Create indexer 
        _indexer.IndexFolder(_testFolderPath, "tfidf", "cosine");
        var searchResults = _indexer.Search("", 2, "cosine");
        _indexer.LoadIndex(_indexFilePath);
    }

    [TestMethod]
    public void Test_SearchDocuments_Euclidean()
    {
        _indexer.IndexFolder(_testFolderPath, "tfidf", "euclidean");
        var searchResults = _indexer.Search("Yet Another mock document, about cats.", 2, "cosine");
        _indexer.LoadIndex(_indexFilePath);

        Assert.AreEqual(searchResults[0], "file3.txt");
    }
}