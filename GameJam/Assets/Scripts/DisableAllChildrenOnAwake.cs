using UnityEngine;

public class DisableAllChildrenOnAwake : MonoBehaviour
{
    private void Awake()
    {
        for (int i = 0; i < this.transform.childCount; i++)
        {
            this.transform.GetChild(i).gameObject.SetActive(false);
        }
    }
}
