using UnityEngine;

[ExecuteInEditMode]
public class RotateSelf : MonoBehaviour
{
    public Vector3 Axis;
    public float Speed;

    private float angle;

    private void Update()
    {
        this.angle += this.Speed * Time.smoothDeltaTime;
        this.transform.localRotation = Quaternion.AngleAxis(this.angle, this.Axis.normalized);
    }
}