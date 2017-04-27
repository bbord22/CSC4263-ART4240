using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DataHandling {
    public static string score;

	public static string Score
    {
        get
        {
            return score;
        }
        set
        {
            score = value;
        }
    }
}
