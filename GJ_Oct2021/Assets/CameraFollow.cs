using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float smoothSpeed = 0.125f;
    private Vector3 offset = new Vector3(0, 0, -1);

    void LateUpdate()
    {
        transform.position = target.position + offset;
    }
}
