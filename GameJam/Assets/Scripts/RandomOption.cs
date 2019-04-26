using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RandomOption : MonoBehaviour
{
   
    public static RandomOption instance = null;

    void Awake()
    {

        if (instance == null)

            instance = this;

        else if (instance != this)

            Destroy(gameObject);


        DontDestroyOnLoad(gameObject);
    }
  

    string random3Options(string rigth, string wrong1, string wrong2)
    {
        string result = "";
        int r = Random.Range(0, 4);
        switch (r)
        {
            case 0:
                result = rigth + wrong1 + wrong2;
                break;

            case 1:
                result = wrong1 + rigth + wrong2;
                break;

            case 2:
                result = wrong1 + wrong2 + rigth;
                break;

            case 3:
                result = wrong2 + wrong1 + rigth;
                break;
             
        }
        return result;

    }
    string random2Options(string rigth, string wrong1)
    {
        string result = "";
        int r = Random.Range(0, 2);
        switch (r)
        {
            case 0:
                result = rigth + wrong1 ;
                break;

            case 1:
                result = wrong1 + rigth ;
                break;

  

        }
        return result;

    }
}
