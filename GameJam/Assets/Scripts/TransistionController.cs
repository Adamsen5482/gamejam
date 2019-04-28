using Sirenix.OdinInspector;
using System.Collections;
using UnityEditor;
using UnityEngine;

    [ExecuteInEditMode]
public class TransistionController : MonoBehaviour
{
    public float TransisitionTime;

    //public RectTransform Rect => this.transform as RectTransform;

    //[ShowInInspector] public Vector2 AnchoredPosition => this.Rect.anchoredPosition;
    //[ShowInInspector] public Vector3 AnchoredPosition3D => this.Rect.anchoredPosition3D;
    //[ShowInInspector] public Vector2 AnchorMin => this.Rect.anchorMin;
    //[ShowInInspector] public Vector2 AnchorMax => this.Rect.anchorMax;
    //[ShowInInspector] public Rect RectRect => this.Rect.rect;
    //[ShowInInspector] public Vector2 Size => this.Rect.sizeDelta;

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