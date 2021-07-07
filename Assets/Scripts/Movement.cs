using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rb ; 
    AudioSource audioSource;

    [SerializeField] float speedThrust = 1000f;
    [SerializeField] float speedRotation = 100f;
    [SerializeField] AudioClip mainEngine;

    
    [SerializeField] ParticleSystem leftSideBooster;
    [SerializeField] ParticleSystem righttSideBooster;
    [SerializeField] ParticleSystem mainBooster;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;

    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }
    
    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }
    void ProcessRotation()
    {
         if (Input.GetKey(KeyCode.Q))
        {
            StartLeftRotation();

        }
        else if (Input.GetKey(KeyCode.D))
        {
            StartRightRotation();
        }
        else
        {
            StopSideBoosters();
        }
    }

     void StopSideBoosters()
    {
        righttSideBooster.Stop();
        leftSideBooster.Stop();
    }

    void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * speedThrust * Time.deltaTime);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine, 1f);
        }
        if (!mainBooster.isPlaying)
        {
            mainBooster.Play();
        }
    }

     void StopThrusting()
    {
        audioSource.Stop();

        mainBooster.Stop();
    }


     void StartRightRotation()
    {
        ApplyRotation(-speedRotation);
        if (!leftSideBooster.isPlaying)
        {
            leftSideBooster.Play();
        }
    }

     void StartLeftRotation()
    {
        if (!righttSideBooster.isPlaying)
        {
            righttSideBooster.Play();
        }
        ApplyRotation(speedRotation);
    }

    public void ApplyRotation(float speedThisFrame)
    {
        rb.freezeRotation = true; // freeze the rotation so we can do it manualy.
        transform.Rotate(Vector3.forward * speedThisFrame * Time.deltaTime);
        rb.freezeRotation = false; // unfreeze the rotation so the system can take over.
    }
}
