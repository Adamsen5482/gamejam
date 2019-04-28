using Sirenix.OdinInspector;
using System.Collections;
using UnityEngine;

public class TransistionController : MonoBehaviour
{
    public float TransisitionTime;
    public Vector3 StartPosition;
    public Vector3 EndPosition;

    [Button]
    public YieldInstruction ShowTransistion()
    {
        this.gameObject.SetActive(true);
        this.StartCoroutine(this.TransistionRoutine());
        return new WaitForSeconds(this.TransisitionTime * 0.5f);
    }

    private IEnumerator TransistionRoutine()
    {
        this.transform.position = this.StartPosition;
        float time = Time.unscaledTime;

        while ((Time.unscaledTime - time) < this.TransisitionTime)
        {
            this.transform.position = Vector3.Lerp(this.StartPosition, this.EndPosition, (Time.unscaledTime - time) / this.TransisitionTime);
            yield return null;
        }
    }

    [ButtonGroup("Record")]
    private void RecordStartPosition()
    {
        this.StartPosition = this.transform.position;
    }
    [ButtonGroup("Record")]
    private void RecordEndPosition()
    {
        this.EndPosition = this.transform.position;
    }
    [ButtonGroup("Record")]
    private void Swap()
    {
        var a = this.StartPosition;
        this.StartPosition = this.EndPosition;
        this.EndPosition = a;
    }
}