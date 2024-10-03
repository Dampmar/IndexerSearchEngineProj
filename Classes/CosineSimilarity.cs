namespace Classes;

public class CosineSimilarity : Similarity
{
    public override double CalculateSimilarity(Dictionary<string, double> Vector1, Dictionary<string, double> Vector2)
    {
        // Checking for null vectors again 
        if (Vector1 == null || Vector2 == null) return 0.0; 
        // Step 1: Calculate the dot product of the two vectors
        double dotProduct = 0;
        foreach (var term in Vector1.Keys)
        {
            if (Vector2.ContainsKey(term))
            {
                dotProduct += Vector1[term] * Vector2[term];
            }
        }

        // Step 2: Calculate the magnitude of the two vectors
        double magnitudeA = Math.Sqrt(Vector1.Values.Sum(x => x * x));
        double magnitudeB = Math.Sqrt(Vector2.Values.Sum(x => x * x));

        // Step 3: Calculate the cosine similarity
        if (magnitudeA == 0 || magnitudeB == 0)
        {
            // Return 0 if one of the vectors has no magnitude (to avoid division by zero)
            return 0.0;
        }

        return dotProduct / (magnitudeA * magnitudeB);
    }
}