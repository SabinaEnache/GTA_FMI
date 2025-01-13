using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressKeyOpenDoor : MonoBehaviour
{
	public GameObject Instruction; //textul cu "press e to open"
	public GameObject AnimeObject;
	public bool Action = false; //ca daca Player apasa tasta E sa se deschida usa doar cand este in trigger in acelasi timp

	void Start()
	{
		Instruction.SetActive(false);
	}

	void OnTriggerEnter(Collider collision)
	{
		if (collision.transform.tag == "Player")
		{
			Instruction.SetActive(true);  //apare textul
			Action = true;
		}
	}

	void OnTriggerExit(Collider collision)
	{
		Instruction.SetActive(false); //dispare textul
		Action = false;
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.E))
		{
			if (Action == true)
			{
				Instruction.SetActive(false);
				StartCoroutine(OpenAndCloseDoor());
			}
		}
	}

	IEnumerator OpenAndCloseDoor()
	{
		AnimeObject.GetComponent<Animator>().Play("DoorOpen");
		yield return new WaitForSeconds(2f); //asteapta 2 secunde 
		AnimeObject.GetComponent<Animator>().Play("DoorClose");
	}
}
