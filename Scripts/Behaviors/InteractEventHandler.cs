using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractEventHandler : MonoBehaviour
{
    [Header("# Interact")]
    [SerializeField]private float checkRate = 0.05f; // 얼마나 자주 레이를 쏠 것 인가 ?
    [SerializeField]private float maxCheckDistance; // 탐지 거리
    [SerializeField]private LayerMask layerMask; // 탐지 레이어
    
    // 아이템 캐싱 관련 변수들
    [Header("# UI")]
    public PromptUIHandler promptUIHandler;
    public InteractHelpHadler interactHandler;
    
    // 현재 상호작용하는 오브젝트 정보
    private GameObject curInteractGameObject;
    private IInteractable curInteractable;
    
    private Camera camera;
    private PlayerController playerController;
    private Coroutine searchCoroutine;
    private WaitForSeconds wait;
    private void Awake()
    {
        wait = new WaitForSeconds(checkRate);
        playerController = GetComponent<PlayerController>();
    }

    private void Start()
    {
        camera = Camera.main;

        StartSearch();
    }

    private void StartSearch()
    {
        if (searchCoroutine != null)
        {
            StopCoroutine(searchCoroutine);
        }

        searchCoroutine = StartCoroutine(CheckForInteractables());
    }

    private IEnumerator CheckForInteractables()
    {
        while (true)
        {
            // ScreenToViewportPoint : 터치했을 때 기준
            // ScreenPointToRay : 카메라 기준으로 레이를 쏨
            // new Vector3(Screen.width / 2, Screen.height / 2) => 정 중앙에서 쏘기 위해
            // 카메라가 찍고 있는 방향이 기본적으로 앞을 바라보기 때문에 따로 방향 설정 X
            Ray ray = camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
            
            if (Physics.Raycast(ray, out RaycastHit hit, maxCheckDistance) && GameManager.Instance.IsLayerMatched( layerMask.value, hit.collider.gameObject.layer)) // 충돌이 됐을 때
            {
                if (hit.collider.gameObject != curInteractGameObject) // 충돌한 오브젝트가 현재 상호작용하는 오브젝트가 아닐 때
                {
                    if (curInteractable != null) // 바로 상호작용이 될 수 도 있으니 구독되어 있는 이벤트르 해제함
                    {
                        playerController.OnInteractEvent -= curInteractable.OnInteract; // 구독 해제
                    }

                    if (hit.collider.TryGetComponent(out curInteractable))
                    {
                        curInteractGameObject = hit.collider.gameObject; // 오브젝트 변경

                        playerController.OnInteractEvent += curInteractable.OnInteract;
                        playerController.OnInteractEvent += promptUIHandler.DisablePrompt;

                        ItemSO curData = curInteractable.GetItemData();

                        if (curData.keyPrompt)
                        {
                            interactHandler.ShowOnInteractKey(curInteractGameObject);
                        }

                        if (curData.descPrompt)
                        {
                            promptUIHandler.SetPrompt(curInteractable.GetInteractPrompt());
                        }
                    }
                  
                }
            }
            else // 충돌한 오브젝트가 없다면 기존에 있던 정보들을 초기화
            {
                InteractionOff();
                interactHandler.ShowOffInteractKey();
            }
            yield return wait;
        }
    }

    public void InteractionOff()
    {
        if (curInteractGameObject != null)
        {
            playerController.OnInteractEvent -= curInteractable.OnInteract;
            playerController.OnInteractEvent -= promptUIHandler.DisablePrompt;
            curInteractGameObject = null;
            curInteractable = null;
            promptUIHandler.DisablePrompt();
        }
    }
}