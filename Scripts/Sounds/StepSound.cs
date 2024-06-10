using UnityEngine;

public class StepSound : MonoBehaviour
{
    public AudioClip[] footstepClips;
    private AudioSource audioSource;
    private Rigidbody rigid;
    public float footstepThreshold;
    public float footstepRate;
    private float footstepTime;

    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        if (Mathf.Abs(rigid.velocity.y) >= 0.0f)
        {
            if (rigid.velocity.magnitude > footstepThreshold)
            {
                if (Time.time - footstepTime > footstepRate)
                {
                    footstepTime = Time.time;
                    audioSource.PlayOneShot(footstepClips[Random.Range(0, footstepClips.Length)]);
                }
            }
        }
    }
}
