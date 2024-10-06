using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float moveSpeed = 10f;
    [SerializeField] float laneDistanceX = 2.0f;
    [SerializeField] float laneDistanceY = 2.0f;
    [SerializeField] int maxLanesX = 2; // Número de faixas no eixo X
    [SerializeField] int maxLanesY = 2; // Número de faixas no eixo Y
    [SerializeField] float smoothTime = 0.2f;

    private int currentLaneX = 0;
    private int currentLaneY = 0; //* 0 é o nivel mais baixo da Lane

    private Vector3 targetPosition;
    private Vector3 velocity = Vector3.zero;


    void Start()
    {
        // Definir a posição inicial do player na última faixa inferior do eixo Y
        currentLaneY = -maxLanesY;
        targetPosition = new Vector3(currentLaneX * laneDistanceX, currentLaneY * laneDistanceY, transform.position.z);
        transform.position = targetPosition; // Garantir que o player comece na posição correta
    }

    void Update()
    {
        // Detectar entrada do jogador para o eixo X
        if (Input.GetKeyDown(KeyCode.A) && currentLaneX > -maxLanesX)
        {
            currentLaneX--;
            SetTargetPosition();
        }
        else if (Input.GetKeyDown(KeyCode.D) && currentLaneX < maxLanesX)
        {
            currentLaneX++;
            SetTargetPosition();
        }

        // Detectar entrada do jogador para o eixo Y
        if (Input.GetKeyDown(KeyCode.W))
        {
            currentLaneY++;
            SetTargetPosition();
        }
        // else if (Input.GetKeyDown(KeyCode.DownArrow) && currentLaneY > -maxLanesY)
        // {
        //     currentLaneY--;
        //     SetTargetPosition();
        // }

        // Mover suavemente o player para a posição alvo usando SmoothDamp
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }

    void SetTargetPosition()
    {
        targetPosition = new Vector3(currentLaneX * laneDistanceX, currentLaneY * laneDistanceY, transform.position.z);
    }
}
