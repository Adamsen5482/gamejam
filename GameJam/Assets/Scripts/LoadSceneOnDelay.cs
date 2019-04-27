using Sirenix.OdinInspector;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadSceneOnDelay : MonoBehaviour
{
    public Image Fade;

    [FilePath(Extensions = ".unity")]
    public string ScenePath;

    public float FadeTime;

    [Button, DisableInEditorMode]
    public void LoadScene()
    {
        this.StartCoroutine(this.LoadSceneRoutine());
    }

    private IEnumerator LoadSceneRoutine()
    {
        float time = Time.unscaledTime;
        float step = 1f / this.FadeTime;

        var c = this.Fade.color;
        c.a = 0f;

        while ((Time.unscaledTime - time) < this.FadeTime)
        {
            c = this.Fade.color;
            c.a = Mathf.Lerp(0f, 1f, (Time.unscaledTime - time) / this.FadeTime);
            this.Fade.color = c;
            yield return null;
        }

        c = this.Fade.color;
        c.a = 1f;
        this.Fade.color = c;

        //yield return new WaitForSeconds(0.05f);
        yield return null;
        yield return null;
        SceneManager.LoadSceneAsync(this.ScenePath);
    }
}
