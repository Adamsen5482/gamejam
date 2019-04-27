using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Sirenix.OdinInspector;

[CreateAssetMenu]
public class Hint : ScriptableObject
{
    [SerializeField, TableList]
    private List<HintItem> hintitems;

    private Dictionary<string, List<string>> hintmap;
    public string getHint(string location)
    {
        if (hintmap == null)
        {
            hintmap = new Dictionary<string, List<string>>();
            for (int i = 0; i < hintitems.Count; i++)
            {
                hintmap.Add(hintitems[i].Location, hintitems[1].hints);
            }

        

        }
        var h= hintmap[location];
        
        return h[UnityEngine.Random.Range(0, h.Count)]
            .Replace("RPNoMurNoCrrNoGos", "dummyname".FormatPlayerName());

    }
}

 

[Serializable]
public class HintItem 
{
    public string Location;
    
    public List<string> hints;

}