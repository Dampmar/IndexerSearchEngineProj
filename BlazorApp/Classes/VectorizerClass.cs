namespace Classes;

public abstract class Vectorizer
{
    public Dictionary<string, double> Vector{get; protected set; } = new Dictionary<string, double>();
    public abstract Dictionary<string, double> VectorizeDocuments(List<Document> documents);
    public abstract Dictionary<string, double> VectorizeDocument(Document query);

}