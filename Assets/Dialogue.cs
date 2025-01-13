using UnityEngine;

public class Dialogue : MonoBehaviour
{
    public string[] dialogueLines; // Reține liniile de dialog
    private bool playerInRange; // Verifică dacă jucătorul este în trigger

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Asigură-te că player-ul are tag-ul "Player"
        {
            playerInRange = true;
            StartDialogue();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            EndDialogue();
        }
    }

    void StartDialogue()
    {
        Debug.Log("Începe dialogul!");
        foreach (string line in dialogueLines)
        {
            Debug.Log(line); // Afișează dialogul în consola Unity
        }
        // Poți apela aici funcții dintr-un sistem de UI pentru a afișa dialogul pe ecran
    }

    void EndDialogue()
    {
        Debug.Log("Dialog încheiat.");
        // Adaugă logică pentru a închide fereastra de dialog, dacă e cazul
    }
}