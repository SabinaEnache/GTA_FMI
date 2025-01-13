using UnityEngine;

public class CharacterControllerScript : MonoBehaviour
{
    public float speed = 5f; // Viteza de mișcare
    public float jumpHeight = 5f; // Înălțimea săriturii
    public float jumpDuration = 1f; // Durata săriturii
    private Animator animator;
    private bool isJumping = false;
    private float jumpTimeElapsed = 0f;
    private float originalY; // Poziția inițială pe axa Y

    void Start()
    {
        // Obține componenta Animator de pe obiect
        animator = GetComponent<Animator>();
        originalY = transform.position.y; // Salvează poziția inițială pe axa Y
    }

    void Update()
    {
        // Detectează intrările de mișcare
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Detectează click-ul stânga pentru a activa animația de punch
        if (Input.GetMouseButtonDown(0)) // 0 este pentru click-ul stânga
        {
            animator.SetTrigger("Punch");
        }

        // Calculează vectorul de mișcare
        Vector3 movement = new Vector3(horizontal, 0f, vertical);

        // Dacă există mișcare și personajul nu sare, setează isRunning la true
        if (movement.magnitude > 0 && !isJumping)
        {
            animator.SetBool("isRunning", true); // Activează animația de alergare
            MoveCharacter(movement);            // Mișcă personajul
        }
        else if (!isJumping)
        {
            animator.SetBool("isRunning", false); // Activează animația Idle
        }

        // Detectează săritura
        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            StartJump();
        }

        // Gestionează săritura dacă este în curs
        if (isJumping)
        {
            HandleJump();
        }
    }

    void MoveCharacter(Vector3 movement)
{
    // Mișcă personajul pe direcția calculată
    transform.Translate(movement * speed * Time.deltaTime, Space.World);

    // Schimbă orientarea personajului doar dacă există mișcare
    if (movement != Vector3.zero)
    {
        transform.rotation = Quaternion.LookRotation(movement);
    }
}


    void StartJump()
    {
        isJumping = true;
        animator.SetBool("isJumping", true); // Activează animația de săritură
        jumpTimeElapsed = 0f;
    }

    void HandleJump()
    {
        jumpTimeElapsed += Time.deltaTime;

        // Calculează poziția pe axa Y pe baza unei traiectorii sinusoidale
        float normalizedTime = jumpTimeElapsed / jumpDuration;
        if (normalizedTime <= 1f)
        {
            float newY = originalY + Mathf.Sin(normalizedTime * Mathf.PI) * jumpHeight;
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);
        }
        else
        {
            EndJump();
        }
    }

    void EndJump()
    {
        isJumping = false;
        animator.SetBool("isJumping", false); // Dezactivează animația de săritură
        transform.position = new Vector3(transform.position.x, originalY, transform.position.z); // Asigură revenirea exactă la sol
    }
}
