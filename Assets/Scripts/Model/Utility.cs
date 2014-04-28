using System.Linq;
using UnityEngine;

public static class Utility
{
    /// <summary>
    /// Select a random item from array.
    /// </summary>
    public static T PickUniformRandom<T>(T[] array)
    {
        if (array.Length > 0)
            return array[Random.Range(0, array.Length)];
        else
            return default(T);
    }

    /// <summary>
    /// Select a random item from weighted array.
    /// </summary>
    public static T PickWeightedRandom<T>(T[] array, int[] weightList)
    {
        if(array.Length != weightList.Length) Debug.LogError("array and weightList must be of the same length!");

        if (array.Length > 0)
            return array[PickWeightedRandom(weightList)];
        else
            return default(T);
    }

    /// <summary>
    /// Select an index from an array of weight (must be positive values!).
    /// Higher weights are more likely to be picked.
    /// </summary>
    public static int PickWeightedRandom(int[] weightList)
    {
        int totalWeight = weightList.Sum();
        int random = UnityEngine.Random.Range(0, totalWeight);

        for (int i = 0; i < weightList.Length; i++)
        {
            if (random >= weightList[i])
                random -= weightList[i];
            else
                return i;
        }
        return -1;
    }

    /// <summary>
    /// Randomly shuffle an array.
    /// </summary>
    public static T[] Shuffle<T>(T[] array)
    {
        T[] shuffledArray = new T[array.Length];
        for (int i = array.Length; i >= 1; i--)
        {
            int index = Random.Range(0, i);
            shuffledArray[i - 1] = array[index];
            array[index] = array[i - 1];
        }
        return shuffledArray;
    }
}