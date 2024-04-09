using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FurnitureButton : MonoBehaviour
{
    [SerializeField] int id;
    private void Start()
    {
        GetComponent<Image>().sprite = FurnitureDatabase.Instance.furnitures[id].sprite;
        GetComponentInChildren<TextMeshProUGUI>().text = FurnitureDatabase.Instance.furnitures[id].itemName;
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
