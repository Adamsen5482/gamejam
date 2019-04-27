using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TimeLoop : MonoBehaviour
{
    public Text textbox;
  
    private void OnEnable()
    {
        ScrollingText(textbox.text);
        Image i = GetComponent<Image>();
        
    }

    void ScrollingText(string text)
    {
        string[] chars = new string[text.Length];
        for (int i = 0; i < text.Length; i++)
        {
            chars[i] = text[i].ToString();
        }

        StartCoroutine(textscrollstart(chars, textbox));
    }

    public static IEnumerator textscrollstart(string[] text, Text textbox)
    {
       
        textbox.text = "";
        for (int i = 0; i < text.Length; i++)
        {
            float r = Random.Range(0, 0.005f);
            if (text[i] == ".")
            {
                yield return new WaitForSeconds(0.01f + r);
            }
            textbox.text += text[i];
            
            yield return new WaitForSeconds(0.001f+r);
        }
      
        yield return null;
      
    }
}
