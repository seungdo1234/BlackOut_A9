using System.Collections;
using UnityEngine;

public class FlashLightEventHandler : MonoBehaviour
{
    [Header("# FlashLight")]
    [SerializeField] private float maxBatteryAmount = 100f; // 최대 배터리 잔량
    [SerializeField] private float batteryDrainSpeed = 1.5f; // 배터리 감소 속도
    [SerializeField] private float flashLightRange = 15f;// 배터리 감소 속도
    private Light flashlight; // 플래시 불빛 오브젝트
    public bool IsFlashlightOn { get; private set; } // 플래시라이트가 켜졌는지 여부
    
    [Header("# Battery Bar")]
    [SerializeField] private float curBatteryAmount; // 현재 배터리 잔량
    [SerializeField] private BarEventHandler barEventHandler;
    private Coroutine batteryDrainCoroutine;

    [Header("# Light Color")]
    [SerializeField] private float blueLightRange = 8f;
    private bool isBlue;

    private void Awake()
    {
        flashlight = transform.GetChild(0).gameObject.GetComponent<Light>();
        curBatteryAmount = maxBatteryAmount;
    }

    private void Start()
    {
        // 이벤트 구독
        GameManager.Instance.PlayerController.OnFlashEvent += ToggleFlashlight;
        GameManager.Instance.PlayerController.OnFlashColorSwitchEvent += ToggleFlashColorLightColor;
        GameManager.Instance.PlayerController.OnFlashEvent += FlashOnOffSound;
    }

    // private void OnDestroy()
    // {
    //     GameManager.Instance.PlayerController.OnFlashEvent -= ToggleFlashlight;
    //     GameManager.Instance.PlayerController.OnFlashColorSwitchEvent -= ToggleFlashColorLightColor;
    // }

    private void ToggleFlashColorLightColor() // 라이트 색깔 체인지
    {
        if (CheckBlueLens()) // 파란색 렌즈를 가지고 있어야 색상 변경 가능
        {
            isBlue = !isBlue;
            flashlight.color = isBlue ? Color.blue : Color.white;
            flashlight.intensity = isBlue ? 4f : 1.5f;
            flashlight.range = isBlue ? blueLightRange : flashLightRange;
        }
      
    }
    
    private void ToggleFlashlight() // 손전등 Toggle
    {
        if (curBatteryAmount <= 0)
        {
            ChargeBattery();
            return;
        }

        IsFlashlightOn = !IsFlashlightOn;
        flashlight.gameObject.SetActive(IsFlashlightOn);

        if (IsFlashlightOn)
        {
            StartBatteryDrain();
        }
        else
        {
            StopBatteryDrain();
           
        }
    }

    private void StartBatteryDrain()
    {
        if (batteryDrainCoroutine != null)
        {
            StopCoroutine(batteryDrainCoroutine);
        }
        batteryDrainCoroutine = StartCoroutine(BatteryDrainCoroutine());
    }

    private void StopBatteryDrain()
    {
        if (batteryDrainCoroutine != null)
        {
            StopCoroutine(batteryDrainCoroutine);
        }
    }

    private IEnumerator BatteryDrainCoroutine() // 배터리 감소 코루틴
    {
        while (curBatteryAmount > 0)
        {
            curBatteryAmount -= batteryDrainSpeed * Time.deltaTime;
            barEventHandler.UpdateFillAmount(maxBatteryAmount, curBatteryAmount);
            yield return null;
        }

        flashlight.gameObject.SetActive(false);
        IsFlashlightOn = false;
        ChargeBattery();
    }

    public void ChargeBattery() // 배터리 교체 함수
    {
        ItemSO batterySO = GameManager.Instance.Inventory.FindItem(EItemType.BATTERY);

        if (batterySO != null)
        {
            curBatteryAmount = batterySO.chargeAmount;
            barEventHandler.UpdateFillAmount(maxBatteryAmount, curBatteryAmount);
            GameManager.Instance.Inventory.RemoveItem(EItemType.BATTERY);
            GameManager.Instance.Inventory.UpdateUI();
            return;
        }
    }

    private bool CheckBlueLens() // 파란색 렌즈를 가지고 있는지 확인
    {
        return GameManager.Instance.Inventory.IsItem(EItemType.BlueLens);
    }

    public void FlashOnOffSound()
    {
        AudioManager.Instance.PlaySfx(EItemType.FLASH);
    }
}