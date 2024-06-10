using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalPuzzle : MonoBehaviour
{
    public int[] password = { 0, 6, 1, 0 };

    private FinalPuzzlePasswordUI ui;

    private Coroutine noiseCamCoroutine;
    private AudioSource[] audioSource;
    [SerializeField] private float fearAmount = 30f;
    [SerializeField] private float clearDuration = 8f;

    public void Awake()
    {
        ui = GameObject.Find("HUD").GetComponentInChildren<FinalPuzzlePasswordUI>();
        audioSource = GetComponents<AudioSource>();
    }

    public void FindPassword(int puzzleNum)
    {
        ui.UpdatePassword(puzzleNum, password[puzzleNum]);
    }

    public void ClearPuzzle()
    {
        // 게임 배경음 끄기
        AudioManager.Instance.PlayBgm(false);
        // 게임 클리어 사운드
        audioSource[1].volume = 0.7f;
        audioSource[1].Play();
        // 하얀 페이드 아웃
        StartCoroutine(ShowClearFadeOut());
        // 게임 클리어 씬
        Invoke("ClearScene", clearDuration);
        // 커서 락모드 해제
        GameManager.Instance.PlayerController.ControlLocked();
    }

    public void FailPuzzle()
    {
        Invoke("InvokeFailPuzzle", 1f);
    }

    public void InvokeFailPuzzle()
    {
        // 카메라 흔들림
        noiseCamCoroutine = StartCoroutine(noiseCamera());
        // 괴물 소리
        audioSource[0].volume = 0.8f;
        audioSource[0].PlayOneShot(audioSource[0].clip);
        // 공포 게이지 상승
        GameManager.Instance.PlayerController.gameObject.GetComponent<FearEventHandler>().AddFear(fearAmount);
    }

    private IEnumerator noiseCamera()
    {
        PuzzleManager.Instance.noiseCam.Priority = 20;

        yield return new WaitForSeconds(3f);

        PuzzleManager.Instance.noiseCam.Priority = 0;

        StopCoroutine(noiseCamCoroutine);
    }

    private void ClearScene()
    {

        SceneManager.LoadScene("ClearScene");
    }

    IEnumerator ShowClearFadeOut()
    {

        float elapsed = 0f; // 경과 시간
        float startAlpha = 0f; // 시작 알파값
        float targetAlpha = 1f; // 목표 알파값 (1f는 255f로 표현한 알파값)

        while (elapsed < clearDuration)
        {
            elapsed += Time.deltaTime; // 경과 시간을 증가
            float alpha = Mathf.Lerp(startAlpha, targetAlpha, elapsed / clearDuration); // 알파 값을 선형 보간
            PuzzleManager.Instance.clearFadeOut.color = new Color(1f, 1f, 1f, alpha); // 색상 설정 (1f는 255f로 표현한 색상 값)
            yield return null; // 다음 프레임까지 대기
        }

        // 최종 알파 값을 목표 알파 값으로 설정
        PuzzleManager.Instance.clearFadeOut.color = new Color(1f, 1f, 1f, targetAlpha);
        PuzzleManager.Instance.clearFadeOut = null;
    }


}