using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CryingSound : MonoBehaviour
{
    public AudioSource audioSource;
    [SerializeField] private float maxDistance;
    [SerializeField] private float curDistance;
    private Transform playerTransform;
    private Coroutine volumeCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();  
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            audioSource.Play();
            playerTransform = other.transform;
            maxDistance = Vector3.Distance(other.transform.position, transform.position);
            volumeCoroutine = StartCoroutine(ControlVolume());
        }
    }

    private void OnTriggerExit(Collider other)
    {
            audioSource.Stop();
            if (volumeCoroutine != null)
            {
                StopCoroutine(volumeCoroutine);
                volumeCoroutine = null;
            }
    }

    private IEnumerator ControlVolume()
    {
        while (true)
        {
            curDistance = Vector3.Distance(playerTransform.position, transform.position); // �÷��̾�� ���� �ڽ��� �Ÿ�
            audioSource.volume = (maxDistance - curDistance) / maxDistance; // �Ÿ���ŭ ���� ���� (�������� Ŀ��)
            yield return null; // ���� �����ӱ��� ���
        }
    }

}
