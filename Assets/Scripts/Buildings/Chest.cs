using UnityEngine;

public class Chest : Building
{
    public GameObject chestPanel;
    public int volume = 10;
    public GiveKey_Chest chestKey;
    private new void Awake()
    {
        buildingName = "나무 상자";
        base.Awake();
        chestPanel = GameManager.Instance.chestPanel;
        buildingType = BuildingType.Chest;
        chestKey = InventoryManager.Instance.chestBoxSlot.GetComponent<GiveKey_Chest>();
    }

    public override void Action()
    {
        switch (buildingStatement)
        {
            case BuildingStatement.Built:
                chestPanel.GetComponent<QuitPanelUI>().PanelActive(); // 상자 패널 활성화
                InventoryManager.Instance.chestName.text = buildingName; // 패널과 상호작용한 오브젝트의 이름 갱신
                chestKey.SetSlotVolume(volume, index * 50); // Chest 패널에 상자의 최대 보유량과 키 전달
                break;
        }
    }
}
