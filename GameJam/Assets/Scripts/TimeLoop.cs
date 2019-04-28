using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class TimeLoop : MonoBehaviour
{
    public Text textbox;

    private string text;

    private void Awake()
    {
        this.text = this.textbox.text;
    }
    
    public Coroutine ScrollingText(string text)
    {
        return this.StartCoroutine(textscrollstart(text, this.textbox));
    }

    public Coroutine ScrollingText()
    {
        return this.StartCoroutine(textscrollstart(this.text, this.textbox));
    }

    public static IEnumerator textscrollstart(string text, Text textbox)
    {
        textbox.text = "";

        StringBuilder builder = new StringBuilder();

        for (int i = 0; i < text.Length; i++)
        { 
            builder.Append(text[i]);
            Audiomanager.instance.PlayTypeWriter();
            if (i % 2 == 0)
            {
                float r = Random.Range(0, 0.005f);
                textbox.text = builder.ToString();
                yield return new WaitForSeconds(0.001f+r);
                
            }
        }

        textbox.text = text;
        
        yield return new WaitForSeconds(1.5f);
        Audiomanager.instance.UnpauseMenuTrack();
    }
}
