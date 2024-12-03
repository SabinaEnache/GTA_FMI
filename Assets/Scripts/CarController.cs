using UnityEngine;

public class CarController : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private float turnSpeed;

    public bool isSittingInThisCar = false;
    public GameObject carCam; // Camera pentru mașină
    public GameObject characterCam; // Camera pentru caracter
    public GameObject playerObject;

    void FixedUpdate()
    {
        if (isSittingInThisCar)
        {
            float verticalAxis = Input.GetAxis("Vertical");
            float horizontalAxis = Input.GetAxis("Horizontal");
            transform.Translate(Vector3.forward * speed * Time.deltaTime * verticalAxis);
            transform.Rotate(Vector3.up * turnSpeed * Time.deltaTime * horizontalAxis * verticalAxis);
        }
    }

    public void GetOutOfCar()
    {
        if (playerObject != null)
        {
            var meshRenderers = playerObject.GetComponentsInChildren<Renderer>();
            foreach (var renderer in meshRenderers)
            {
                renderer.enabled = false; // Face invizibil fiecare renderer
            }
            playerObject.transform.position = transform.position +new Vector3(0f,0f,5f); // Poziționează player-ul lângă mașină
            isSittingInThisCar = false;
            this.enabled = false;
            SwitchToCharacterCamera();
            playerObject = null;
        }
    }

    void SwitchToCarCamera()
    {
        carCam.SetActive(true);
        characterCam.SetActive(false);
    }

    void SwitchToCharacterCamera()
    {
        carCam.SetActive(false);
        characterCam.SetActive(true);
    }
}