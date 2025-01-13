using UnityEngine;
using System.Collections;

public class NPCController : MonoBehaviour
{
    public float speed = 2f; // Viteza de mers
    public float changeDirectionInterval = 3f; // Interval pentru schimbarea direcției
    public float maxHealth = 100f; // Sănătatea maximă
    private float currentHealth;
    private Vector3 randomDirection; // Direcția aleatorie
    private Animator animator; // Animatorul pentru NPC
    private Rigidbody rb; // Rigidbody-ul pentru mișcare fizică
    private bool isTakingDamage = false; // Controlează dacă NPC-ul ia damage
    private bool isDead = false; // Controlează dacă NPC-ul este mort

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        currentHealth = maxHealth; // Inițializează sănătatea
        StartCoroutine(ChangeDirection());
    }

    void FixedUpdate()
    {
        // Mișcă NPC-ul doar dacă nu este în animația de damage sau mort
        if (!isTakingDamage && !isDead)
        {
            if (randomDirection.magnitude > 0)
            {
                MoveCharacter(randomDirection);
                animator.SetBool("isWalking", true);
            }
            else
            {
                animator.SetBool("isWalking", false);
            }
        }
    }

    void MoveCharacter(Vector3 direction)
    {
        // Aplică mișcarea folosind Rigidbody
        Vector3 move = direction * speed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + move);

        // Schimbă orientarea NPC-ului
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            rb.rotation = targetRotation;
        }
    }

    IEnumerator ChangeDirection()
    {
        while (!isDead)
        {
            // Calculează o direcție aleatorie pe planul XZ
            randomDirection = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f)).normalized;
            yield return new WaitForSeconds(changeDirectionInterval);
        }
    }

    public void TakeDamage(float damage)
    {
        if (isDead || isTakingDamage) return; // Dacă este mort sau deja în animația de hit, nu lua damage

        currentHealth -= damage;

        if (currentHealth > 0)
        {
            StartCoroutine(PlayHitAnimation());
        }
        else
        {
            Die();
        }
    }

    IEnumerator PlayHitAnimation()
    {
        isTakingDamage = true;

        // Activează animația de Hit
        animator.SetTrigger("Hit");

        // Așteaptă finalizarea animației de hit (înlocuiește cu durata animației tale, ex. 0.5s)
        yield return new WaitForSeconds(0.5f);

        isTakingDamage = false;
    }

    void Die()
    {
        if (isDead) return;

        isDead = true; // Setează starea de moarte
        animator.SetTrigger("Death"); // Activează animația de moarte
        Debug.Log("NPC a murit!");

        // Dezactivează componentele relevante
        rb.linearVelocity = Vector3.zero; // Oprește mișcarea
        rb.isKinematic = true; // Dezactivează fizica pentru NPC
        this.enabled = false; // Dezactivează scriptul pentru a opri logicile suplimentare
    }
}
