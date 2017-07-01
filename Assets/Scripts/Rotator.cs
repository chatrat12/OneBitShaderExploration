using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField]
    private float _rotateSpeed = 360;

    private void Update()
    {
        transform.Rotate(Vector3.up, _rotateSpeed * Time.deltaTime);
    }
}
