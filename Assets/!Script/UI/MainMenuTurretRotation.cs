using UnityEngine;

public class MainMenuTurretRotation : MonoBehaviour
{
    public float rotationSpeed = 10f;
    public Transform partToRotate;

    void Update()
    {
        partToRotate.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
}
