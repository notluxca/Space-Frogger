using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform playerTransform; // public para a camera poder ter alvos dinamicos (modificaveis)
    [SerializeField] float smoothTime = 0.3f;
    [SerializeField] Vector2 yOffset = new Vector2(0, 2f); // Offset em Y para ajustar a altura da câmera em relação ao alvo

    private Vector3 velocity = Vector3.zero;

    void Update()
    {
        if (playerTransform != null)
        {
            Vector3 targetPosition = new Vector3(transform.position.x, playerTransform.position.y + yOffset.y, transform.position.z);

            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        }
    }
}