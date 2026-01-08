using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] InputAction thrust;
    [SerializeField] InputAction rotation;
    [SerializeField] float thrustrength = 100f;
    [SerializeField] float rotationStrength = 100f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] ParticleSystem mainEngineParticles;
    [SerializeField] ParticleSystem rigthThrustParticles;
    [SerializeField] ParticleSystem leftThrustParticles;



    Rigidbody rb;
    AudioSource audioSource;



    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }
    private void OnEnable()
    {
        thrust.Enable();
        rotation.Enable();

    }

    private void FixedUpdate()
    {
        ProcessThrust();
        ProcessRotation();

    }

    private void ProcessRotation()
    {
        float rotationInput = rotation.ReadValue<float>();
        if(rotationInput < 0 )
        {
            RotateRight();
        }
        else if(rotationInput > 0)
        {
            RotateLeft();
        }
        else
        {
            StopRotating();
        }
    }
    private void RotateRight()
    {
        ApplyRotation(rotationStrength);
        if (!rigthThrustParticles.isPlaying)
        {
            leftThrustParticles.Stop();
            rigthThrustParticles.Play();
        }
    }
    private void RotateLeft()
    {
        ApplyRotation(-rotationStrength);
        if (!leftThrustParticles.isPlaying)
        {
            rigthThrustParticles.Stop();
            leftThrustParticles.Play();
        }
    }
    private void StopRotating()
   {
        rigthThrustParticles.Stop();
        leftThrustParticles.Stop();
   }

   
    

    private void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.fixedDeltaTime);
        rb.freezeRotation = false;
    }

    private void ProcessThrust()
    {
        if (thrust.IsPressed())
        {
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }
    private void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * thrustrength * Time.fixedDeltaTime);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }
        if (!mainEngineParticles.isPlaying)
        {
            mainEngineParticles.Play();
        }
    }
    private void StopThrusting()
    {
        audioSource.Stop();
        mainEngineParticles.Stop();
    }

    
}
