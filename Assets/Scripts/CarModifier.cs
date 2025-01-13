using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carModifier : MonoBehaviour
{
    internal enum type
    {
        player,
        AI,
    }

    [SerializeField] private type playerType;

    [Header("Wheels")]
    public GameObject[] wheels; // Roți pentru animație (setate manual din Inspector)

    [Header("Wheel Colliders")]
    public WheelCollider[] wheelColliders; // WheelCollider setate manual din Inspector

    public ParticleSystem smoke;
    [HideInInspector] public ParticleSystem[] smokeArray;

    public AudioClip skid;
    [Range(0, 1)] public float skidVolume = 0.6f;
    [HideInInspector] public AudioSource[] skidArr;

    public TrailRenderer tireMarks;
    [HideInInspector] public TrailRenderer[] tireMarksarr;

    private Vector3 wheelPosition;
    private Quaternion wheelRotation;

    void Awake()
    {
        if (wheels.Length != wheelColliders.Length)
        {
            Debug.LogError("Numărul de roți nu se potrivește cu numărul de WheelColliders!");
            return;
        }

        if (playerType == type.player)
        {
            spawnSmoke();
        }
    }

    void FixedUpdate()
    {
        if (playerType == type.player)
            activateSmoke();
        animateWheels();
    }

    private void activateSmoke()
    {
        for (int i = 0; i < wheels.Length; i++)
        {
            // Exemplu: verificare simplă pentru fum și derapaj
            if (/* Condiție personalizată pentru derapaj */ false)
            {
                if (!skidArr[i].isPlaying)
                {
                    smokeArray[i].Play();
                    skidArr[i].Play();
                    tireMarksarr[i].emitting = true;
                }
            }
            else
            {
                smokeArray[i].Stop();
                skidArr[i].Stop();
                tireMarksarr[i].emitting = false;
            }
        }
    }

    public void animateWheels()
    {
        for (int i = 0; i < wheels.Length; i++)
        {
            wheelColliders[i].GetWorldPose(out wheelPosition, out wheelRotation);
            wheels[i].transform.position = wheelPosition;
            wheels[i].transform.rotation = wheelRotation;
        }
    }

    private void spawnSmoke()
    {
        smokeArray = new ParticleSystem[wheels.Length];
        skidArr = new AudioSource[wheels.Length];
        tireMarksarr = new TrailRenderer[wheels.Length];

        for (int i = 0; i < wheels.Length; i++)
        {
            ParticleSystem newSmoke = Instantiate(smoke, wheels[i].transform);
            smokeArray[i] = newSmoke;

            AudioSource newAudioSource = wheels[i].AddComponent<AudioSource>();
            newAudioSource.clip = skid;
            newAudioSource.volume = skidVolume;
            skidArr[i] = newAudioSource;

            TrailRenderer newTireMarks = Instantiate(tireMarks, wheels[i].transform);
            tireMarksarr[i] = newTireMarks;
        }
    }
}
