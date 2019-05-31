using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardRamber : MonoBehaviour
{
    public List<string> GuardRambles = new List<string>();
    public int specificRamble = 0;

    public string RandomRamble()
    {
        return GuardRambles[(int)Random.Range(0, GuardRambles.Count-1)];
    }

    public string NextRamble()
    {
        string ret = GuardRambles[specificRamble];
        specificRamble++;
        specificRamble = specificRamble % GuardRambles.Count;
        return ret;
    }
}
