namespace Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

public abstract class Similarity 
{
    // Abstract method implemented by both Children classes: Euclidean and Cosine
    public abstract double CalculateSimilarity(Dictionary<string, double> Vector1, Dictionary<string, double> Vector2);
}