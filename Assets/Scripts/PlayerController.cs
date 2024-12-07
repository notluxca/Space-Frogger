using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private float moveSpeed = 10f;
    [SerializeField] float laneDistanceX = 2.0f;
    [SerializeField] float laneDistanceY = 2.0f;
    [SerializeField] int maxLanesX = 2; // Número de faixas no eixo X
    [SerializeField] int maxLanesY = 2; // Número de faixas no eixo Y
    [SerializeField] float smoothTime = 0.2f;
    [SerializeField] float maxTiltAngle = 15f; // Maximum tilt angle based on velocity
    [SerializeField] float tiltSpeed = 5f;   // Speed of tilting back to neutral

    [SerializeField] AudioClip explosionSound;
    [SerializeField] AudioClip moveSound;
    AudioSource audioSource;

    private int currentLaneX = 0;
    private int currentLaneY = 0;

    private Vector3 targetPosition;
    private Vector3 velocity = Vector3.zero;
    private ParticleSystem particleSystem;
    private Rigidbody2D rigidbody2D;
    private SpriteRenderer spriteRenderer;
    private bool isFrozen;
    public bool canMoveFoward = true;
    public float moveFowardCooldown;

    void Start()
    {

        isFrozen = false;
        particleSystem = GetComponent<ParticleSystem>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        currentLaneY = -maxLanesY;
        targetPosition = new Vector3(currentLaneX * laneDistanceX, currentLaneY * laneDistanceY, transform.position.z);
        transform.position = targetPosition; // Garantir que o player comece na posição correta
    }

    void Update()
    {
        if (isFrozen) return;

        // // Detect horizontal input and update lane position
        // if (Input.GetKeyDown(KeyCode.A) && currentLaneX > -maxLanesX)
        // {
        //     currentLaneX--;
        //     SetTargetPosition();
        // }
        // else if (Input.GetKeyDown(KeyCode.D) && currentLaneX < maxLanesX)
        // {
        //     currentLaneX++;
        //     SetTargetPosition();
        // }

        // // Detect vertical input for lane position (no tilt adjustment here)
        // if (Input.GetKeyDown(KeyCode.W) && canMoveFoward)
        // {
        //     currentLaneY++;
        //     SetTargetPosition();
        //     ; StartCoroutine(MoveFowardCooldown());
        // }

        // Smoothly move to the target position using SmoothDamp
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);

        // Calculate tilt based on X velocity
        float tiltAngle = Mathf.Clamp(-velocity.x / laneDistanceX * maxTiltAngle, -maxTiltAngle, maxTiltAngle);

        // Smoothly apply tilt based on the calculated angle
        Quaternion targetRotation = Quaternion.Euler(0, 0, tiltAngle);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * tiltSpeed);
    }

    void SetTargetPosition()
    {
        audioSource.clip = moveSound;
        audioSource.Play();
        targetPosition = new Vector3(currentLaneX * laneDistanceX, currentLaneY * laneDistanceY, transform.position.z);
    }

    public void MoveUp(){
        // Detect vertical input for lane position (no tilt adjustment here)
        if (canMoveFoward)
        {
            currentLaneY++;
            SetTargetPosition();
            ; StartCoroutine(MoveFowardCooldown());
        }
    }

    public void MoveLeft(){
        if (currentLaneX > -maxLanesX)
        {
            currentLaneX--;
            SetTargetPosition();
        }
    }

    public void MoveRight(){
        if (currentLaneX < maxLanesX)
        {
            currentLaneX++;
            SetTargetPosition();
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            StartCoroutine(DeathSequence());
        }
    }

    IEnumerator DeathSequence()
    {
        isFrozen = true;
        particleSystem.Play();
        spriteRenderer.enabled = false;
        rigidbody2D.constraints = RigidbodyConstraints2D.FreezePosition;
        rigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
        audioSource.clip = explosionSound;
        audioSource.Play();
        GetComponent<TrailRenderer>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<DangerDetector>().enabled = false;
        yield return new WaitForSeconds(1);
        UIManager.Instance.FadeToBlack();
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Destroy(this.gameObject);
    }

    IEnumerator MoveFowardCooldown()
    {
        canMoveFoward = false;
        yield return new WaitForSeconds(moveFowardCooldown);
        canMoveFoward = true;
    }
}
