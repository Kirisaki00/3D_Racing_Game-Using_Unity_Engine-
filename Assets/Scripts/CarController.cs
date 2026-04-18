using UnityEngine;

public class CarController : MonoBehaviour
{
    public float speed = 1200f;
    public float turnSpeed = 200f;

    private Rigidbody rb;
    private float moveInput;
    private float turnInput;
    public AudioClip engineClip;   // drag sound here
    private AudioSource engineSound;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        engineSound = gameObject.AddComponent<AudioSource>();
        engineSound.clip = engineClip;
        engineSound.loop = true;
        engineSound.playOnAwake = false;
        engineSound.volume = 0.7f;
    }

    void Update()
    {
        moveInput = Input.GetAxis("Vertical");
        turnInput = Input.GetAxis("Horizontal");
        if (moveInput > 0.1f)
        {      
        if (!engineSound.isPlaying)
            engineSound.Play();
        }
        else
        {
            engineSound.Pause();
        }
        }

    void FixedUpdate()
    {
        // Move
        rb.AddForce(transform.forward * moveInput * speed, ForceMode.Acceleration);

        // Turn only when moving
        if (rb.linearVelocity.magnitude > 1f)
        {
            float turn = turnInput * turnSpeed * Time.fixedDeltaTime;
            Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);
            rb.MoveRotation(rb.rotation * turnRotation);
        }
    }
}