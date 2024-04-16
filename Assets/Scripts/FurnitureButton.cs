using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FurnitureButton : MonoBehaviour // ũ����Ʈ �гο��� ���õǴ� ���๰
{
    [SerializeField] int id;
    private void Start()
    {
        GetComponent<Image>().sprite = FurnitureDatabase.Instance.furnitures[id].sprite;
        GetComponentInChildren<TextMeshProUGUI>().text = FurnitureDatabase.Instance.furnitures[id].itemName;
        if(id != 101) gameObject.SetActive(false);
    }
    public void PressFurnitureButton()
    {
        CraftManager.Instance.FurnitureChoice(id);
    }

    public void EnterMouse()
    {
        CraftManager.Instance.FurnitureOver(id);
    }

    public void ExitMouse()
    {
        CraftManager.Instance.FurnitureExit(id);
    }
}
