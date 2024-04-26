using UnityEngine;

public class Chest : Building
{
    public GameObject chestPanel;
    public int volume = 10;
    public GiveKey_Chest chestKey;
    private new void Awake()
    {
        buildingName = "���� ����";
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
                chestPanel.GetComponent<QuitPanelUI>().PanelActive(); // ���� �г� Ȱ��ȭ
                InventoryManager.Instance.chestName.text = buildingName; // �гΰ� ��ȣ�ۿ��� ������Ʈ�� �̸� ����
                chestKey.SetSlotVolume(volume, index * 50); // Chest �гο� ������ �ִ� �������� Ű ����
                break;
        }
    }
}
