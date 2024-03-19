using UnityEngine;

public class CameraController : MonoBehaviour
{
    public bool canMove = true;
    public float panSpeed = 20f;
    public int screenEdge = 50;
    public int zoomSpeed;
    public float minY, maxY;
    public float minX, maxX;
    public float minZ, maxZ;

    void Update()
    {

        if (Input.GetKey(KeyCode.W) || Input.mousePosition.y > Screen.height - screenEdge)
        {
            transform.Translate(Vector3.forward * panSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey(KeyCode.S) || Input.mousePosition.y < screenEdge)
        {
            transform.Translate(Vector3.back * panSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey(KeyCode.A) || Input.mousePosition.x < screenEdge)
        {
            transform.Translate(Vector3.left * panSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey(KeyCode.D) || Input.mousePosition.x > Screen.width - screenEdge)
        {
            transform.Translate(Vector3.right * panSpeed * Time.deltaTime, Space.World);
        }

        float wheel = Input.GetAxis("Mouse ScrollWheel");
        Vector3 pos = transform.position;
        pos.y -= wheel * zoomSpeed;
        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);
        pos.z = Mathf.Clamp(pos.z, minZ, maxZ);
        transform.position = pos;
    }
}
