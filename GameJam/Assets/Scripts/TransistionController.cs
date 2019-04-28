using Sirenix.OdinInspector;
using System.Collections;
using UnityEditor;
using UnityEngine;

    [ExecuteInEditMode]
public class TransistionController : MonoBehaviour
{
    public float TransisitionTime;

    [Button]
    public YieldInstruction ShowTransistion()
    {
        this.gameObject.SetActive(true);
        this.StartCoroutine(this.TransistionRoutine());
        return new WaitForSeconds(this.TransisitionTime * 0.5f);
    }

    private IEnumerator TransistionRoutine()
    {
        float time = Time.unscaledTime;
        var rect = this.transform as RectTransform;

        while ((Time.unscaledTime - time) < this.TransisitionTime)
        {
            var p = rect.anchoredPosition;
            p.x = Mathf.Lerp(rect.rect.width, -rect.rect.width, (Time.unscaledTime - time) / this.TransisitionTime);
            rect.anchoredPosition = p;
            rect.sizeDelta = Vector2.zero;

            yield return null;
        }

        var pos = rect.anchoredPosition;
        pos.x = -rect.rect.width;
        rect.anchoredPosition = pos;
        rect.sizeDelta = Vector2.zero;

        this.gameObject.SetActive(false);
    }
}