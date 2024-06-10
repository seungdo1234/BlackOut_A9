public interface IInteractable
{
    
    public string GetInteractPrompt(); // 화면에다 띄어줄 프롬프트에 관한 함수
    public void OnInteract(); // 상호작용 효과
    public ItemSO GetItemData(); // ItemSO 반환
}