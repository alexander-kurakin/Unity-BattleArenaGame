using UnityEngine;

public class LookCameraScript : MonoBehaviour
{
    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void LateUpdate()
    {
        if (_camera == null) return;

        transform.rotation = Quaternion.LookRotation(transform.position - _camera.transform.position);
    }
}
