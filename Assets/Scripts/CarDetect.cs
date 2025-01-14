using UnityEngine;

public class CarDetect : MonoBehaviour
{
    public bool isCarAvailable = false;
    public bool sitInCarAllowed = false;
    public GameObject carCam; // Camera pentru mașină
    public GameObject characterCam; // Camera pentru caracter
    private CarController carController = null;
    private PoliceSpawner PoliceSpawnerr;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && isCarAvailable && !sitInCarAllowed)
        {
            sitInCarAllowed = true;
            SitInCar(carController.gameObject);
        }
        else if (Input.GetKeyDown(KeyCode.F))
        {

            GetOutOfCar();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("CarTrigger"))
        {
            isCarAvailable = true;
            carController = other.transform.parent.GetComponent<CarController>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("CarTrigger"))
        {
            isCarAvailable = false;
            sitInCarAllowed = false;
        }
    }

    public void SitInCar(GameObject car)
    {
        PoliceSpawnerr=car.GetComponent<PoliceSpawner>();
        PoliceSpawnerr.policeGonSpawn = 1;
        transform.position = car.transform.position;
        carController = car.GetComponent<CarController>();
        if (carController != null)
        {
            carController.enabled = true;
            carController.isSittingInThisCar = true;
            carController.playerObject = gameObject;
            SwitchToCarCamera();
            var meshRenderers = gameObject.GetComponentsInChildren<Renderer>();
            foreach (var renderer in meshRenderers)
            {
                renderer.enabled = false; // Face invizibil fiecare renderer
            }
        }
    }

    public void GetOutOfCar()
    {
        if (carController != null)
        {
            carController.GetOutOfCar();
            SwitchToCharacterCamera();
            var meshRenderers = gameObject.GetComponentsInChildren<Renderer>();
            foreach (var renderer in meshRenderers)
            {
                renderer.enabled = true; // Face invizibil fiecare renderer
            }
            sitInCarAllowed = false;
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
