using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InteractHelpHadler : MonoBehaviour
{
    private TextMeshProUGUI interactText;
    private Image interactImage;

    private void Awake()
    {
        interactImage = GetComponentInChildren<Image>(true);
        interactText = GetComponentInChildren<TextMeshProUGUI>(true);
    }

    private string GetInteractText(GameObject obj)
    {
        if (obj.TryGetComponent(out Door door))
        {
            if (door.IsLock)
            {
                if (GameManager.Instance.Inventory.IsItem(EItemType.KEY))
                {
                    return "잠김\n[E] 열쇠 사용";
                }
                else return "잠김";
            }
            else return "[E] 열기";

        }

        else if (obj.GetComponent<ItemObject>().data.itemType == EItemType.CLIPBOARD)
        {
            if (GameManager.Instance.Inventory.IsItem(EItemType.FLOWER))
            {
                return "[E] 꽃 놓기";
            }
            else return "";
        }
        
        else return "[E] 가져가기";
    }
    public void ShowOnInteractKey(GameObject obj)
    {
        string text = GetInteractText(obj);
        if (text.Equals("")) return;
        interactText.text = text;
        interactImage.gameObject.SetActive(true);
    }

    public void ShowOffInteractKey()
    {
        interactImage.gameObject.SetActive(false);
    }
}
