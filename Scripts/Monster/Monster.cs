using Cinemachine;
using System.Collections;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class Monster : MonoBehaviour
{

    public CinemachineVirtualCamera virtualCamera;
    public GameObject spotLight;
    private Animator animator;
    public bool isBite = false;
    [SerializeField] private float speed;
    private Coroutine coroutine;
    public AudioSource audioSource;
    public AudioClip clip;
    

    private void Awake()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        clip = audioSource.clip;
    }
    // Update is called once per frame
    void Update()
    {
        if(isBite) return;
        
        ChasePlayer();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isBite)
        {
            isBite = true;
            if (other.CompareTag("Player"))
            {
                GameManager.Instance.PlayerController.ControlLocked();
                // �÷��̾ ���̴� ȿ��
                coroutine = StartCoroutine(KillPlayer());
            }
        }
    }

    private void ChasePlayer()
    {
        Vector3 playerPosition = GameManager.Instance.PlayerController.transform.position;


        Vector3 targetPosition = playerPosition;
        targetPosition.y -= 1.15f;

        transform.LookAt(targetPosition);

        // ���Ͱ� ���� ���� ���͸� ����մϴ�.
        Vector3 direction = (targetPosition - transform.position).normalized;
        // direction.y = 0; 

        transform.position += direction * speed * Time.deltaTime;
    }

    private IEnumerator KillPlayer()
    {
        virtualCamera.Priority = 20;
        spotLight.SetActive(true);

        yield return new WaitForSeconds(1f);

        audioSource.loop = false;
        AudioManager.Instance.PlaySfx(EItemType.MONSTERBITE);
        animator.SetTrigger("Bite");

        yield return new WaitForSeconds(0.5f);

        AudioManager.Instance.PlaySfx(EItemType.MANSCREAM);

        StopCoroutine(coroutine);
        coroutine = null;
    }
}
