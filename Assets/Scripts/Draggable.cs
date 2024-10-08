using UnityEngine;

public class Draggable : MonoBehaviour
{
    private Vector3 offset;
    private Camera cam;
    public bool isDragging = false;

    private void Start()
    {
        cam = Camera.main;
    }

    void OnMouseDown()
    {

        Vector3 mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
        offset = transform.position - mousePosition;
        offset.z = 0f;

        isDragging = true;
    }

    void OnMouseDrag()
    {
        if (isDragging)
        {
            Vector3 mousePosition = cam.ScreenToWorldPoint(Input.mousePosition); // Get current mouse position in the world
            mousePosition.z = 0f;  // Keep the Z axis fixed (for 2D)

            transform.position = mousePosition + offset;
        }
    }

    void OnMouseUp()
    {
        isDragging = false;
    }
}
