using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PromptUIHandler : MonoBehaviour
{
    private TextMeshProUGUI promptText;
    private Image promptImage;

    private void Awake()
    {
        promptImage = GetComponentInChildren<Image>(true);
        promptText = GetComponentInChildren<TextMeshProUGUI>(true);
    }

    public void SetPrompt(string objectInfo)
    {
        promptText.text = objectInfo;
        // 텍스트 길이에 맞게 배경 이미지 크기 조절
        promptImage.rectTransform.sizeDelta = new Vector2(promptText.preferredWidth + 25f, promptImage.rectTransform.sizeDelta.y);
        promptImage.gameObject.SetActive(true);
    }

    public void DisablePrompt()
    {
        promptImage.gameObject.SetActive(false);
    }
    
    
}