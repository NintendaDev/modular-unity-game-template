using UnityEngine;

namespace Modules.Probabilities
{
    public static class ProbabilityExtensions
    {
        private const float MinProbability = 0;
        private const float MaxProbability = 1f;
        
        public static bool HasChance(this float chanceProbability)
        {
            ValidateProbability(chanceProbability);

            if (Mathf.Approximately(chanceProbability, MaxProbability))
                return true;

            return Random.Range(MinProbability, MaxProbability) < chanceProbability;
        }

        private static void ValidateProbability(float probability)
        {
            if (probability > 1 || probability < 0)
                throw new System.Exception("Probability value can only be from 0 to 1");
        }
    }
}