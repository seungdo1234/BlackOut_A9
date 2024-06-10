using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    private GameObject monster;

    public PlayerInputController PlayerController { get; set; }

    public Inventory Inventory { get; set; }

    public bool haveBlueLens = false;


    private void Awake()
    {
        if (transform.childCount > 0)
        {
            monster = transform.GetChild(0).gameObject;
        }
    }

    private void Start()
    {
        Screen.SetResolution(1920,1080,FullScreenMode.FullScreenWindow);
    }

    public void GameOver()
    {
        monster.transform.position = -PlayerController.transform.forward * 20f;
        monster.SetActive(true);
        StartCoroutine(ShowDeadScene());
    }


    public bool IsLayerMatched(int layerMask, int objectLayer)
    {
        return layerMask == (layerMask | (1 << objectLayer));
    }

    private IEnumerator ShowDeadScene()
    {
        Monster monsterScript = monster.GetComponent<Monster>();

        // ���Ͱ� �÷��̾ ���������� ��ٸ���
        while (!monsterScript.isBite)
        {
            yield return null;
        }

        yield return new WaitForSeconds(5f);

        // isBite�� true�� �Ǹ� "DeadScene"�� �ε��Ѵ�.
        SceneManager.LoadScene("DeadScene");

        yield return null;
    }


}