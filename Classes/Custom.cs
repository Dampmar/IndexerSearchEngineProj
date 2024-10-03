namespace Classes;

public class NormalizedBagOfWords : Vectorizer
{
    // Vectorizes a list of documents by calculating normalized term frequencies
    public override Dictionary<string, double> VectorizeDocuments(List<Document> documents)
    {
        var termFrequencies = new Dictionary<string, double>();

        // Calculate term frequencies across all documents
        foreach (var document in documents)
        {
            var docTermFrequencies = VectorizeDocument(document);
            foreach (var term in docTermFrequencies.Keys)
            {
                if (!termFrequencies.ContainsKey(term))
                {
                    termFrequencies[term] = 0;
                }
                termFrequencies[term] += docTermFrequencies[term]; // Sum of term frequencies across all docs
            }
        }

        return termFrequencies;
    }

    // Vectorizes a single document by calculating normalized term frequencies
    public override Dictionary<string, double> VectorizeDocument(Document document)
    {
        var termFrequencies = new Dictionary<string, double>();
        var terms = document.GetTerms();
        var totalTerms = terms.Count;

        // Calculate normalized term frequencies
        foreach (var term in terms)
        {
            if (!termFrequencies.ContainsKey(term))
            {
                var termCount = terms.Count(t => t == term);
                var normalizedTF = (double) termCount / totalTerms; // Normalization
                termFrequencies[term] = normalizedTF;
            }
        }

        return termFrequencies;
    }
}