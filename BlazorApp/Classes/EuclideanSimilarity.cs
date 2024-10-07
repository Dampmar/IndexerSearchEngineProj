namespace Classes;

public class EuclideanSimilarity : Similarity
{
    // Override method to write the new implementation
    public override double CalculateSimilarity(Dictionary<string, double> Vector1, Dictionary<string, double> Vector2)
    {
        // If an null vector is passed the return value should be 0, to prevent exception calls
        if (Vector1 == null || Vector2 == null) return 0.0; 
        // Get the Sum of the squared difference between Vector1 and Vector2 values
        double sumOfSquaredDifferences = 0;
        foreach (var term in Vector1.Keys)
        {
            double val1 = Vector1.ContainsKey(term) ? Vector1[term] : 0;
            double val2 = Vector2.ContainsKey(term) ? Vector2[term] : 0;
            sumOfSquaredDifferences += Math.Pow(val1 - val2, 2);
        }

        // Calculate the distance between the Euclidean Distance and return the value
        double euclideanDistance = Math.Sqrt(sumOfSquaredDifferences);
        return 1 / (1 + euclideanDistance);
    }
}