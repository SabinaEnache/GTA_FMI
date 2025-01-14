using UnityEngine;
using UnityEngine.UI;

public class NPCDialogue : MonoBehaviour
{
    public string[] dialogueLines; // Liniile de dialog
    public GameObject dialogueUI; // UI pentru dialog
    public Text dialogueText; // Textul de dialog
    private int currentLineIndex = 0;
    private Animator animator;
    private bool isPlayerNearby = false; // Dacă jucătorul este aproape
    private bool isTalking = false; // Dacă dialogul este activ

    void Start()
    {
        animator = GetComponent<Animator>();
        dialogueUI.SetActive(false); // Ascunde UI-ul de dialog la început
    }

    void Update()
    {
        // Verifică dacă jucătorul apasă T pentru a începe dialogul
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.T) && !isTalking)
        {
            StartDialogue();
        }
        // Termină dialogul când apăsăm T din nou și suntem deja în dialog
        else if (isTalking && Input.GetKeyDown(KeyCode.T))
        {
            ContinueDialogue();
        }
    }

    void StartDialogue()
    {
        isTalking = true;
        currentLineIndex = 0;
        dialogueUI.SetActive(true); // Afișează UI-ul
        animator.SetBool("IsTalking", true); // Pornește animația de vorbire
        ShowLine();
    }

    void ContinueDialogue()
    {
        currentLineIndex++;

        if (currentLineIndex < dialogueLines.Length)
        {
            ShowLine();
        }
        else
        {
            EndDialogue();
        }
    }

    void EndDialogue()
    {
        isTalking = false;
        dialogueUI.SetActive(false); // Ascunde UI-ul
        animator.SetBool("IsTalking", false); // Oprește animația de vorbire
    }

    void ShowLine()
    {
        dialogueText.text = dialogueLines[currentLineIndex]; // Actualizează textul de dialog
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Verifică dacă jucătorul a intrat în zonă
        {
            isPlayerNearby = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) // Verifică dacă jucătorul a ieșit din zonă
        {
            isPlayerNearby = false;
            if (isTalking)
            {
                EndDialogue();
            }
        }
    }
}
