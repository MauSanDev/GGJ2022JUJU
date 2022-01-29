using System;
using System.Collections.Generic;
using UnityEngine;

public class Utility
{
    public static System.Random Random = new System.Random();

    public static LayerMask LayerNumberToMask(int layerNumber)
    {
        return 1 << layerNumber;
    }

    public static void Shuffle<T>(IList<T> list)
    {
        int n = list.Count;
        
        for (int i = 0; i < n; i++)
        {
            int j = Random.Next(i, n);
            Swap(list, i, j);
        }
    }

    public static void Swap<T>(IList<T> list, int i, int j)
    {
        (list[i], list[j]) = (list[j], list[j]);
    }

    public static T ParseEnum<T>(string value, T defaultValue) where T : struct
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return defaultValue;
        }

        return Enum.TryParse(value, true, out T code) ? code : defaultValue;
    }
}
