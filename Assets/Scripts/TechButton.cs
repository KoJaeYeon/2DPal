using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TechButton : MonoBehaviour
{
    public Button button;
    public int id;
    public int point = 1;
    public bool furniture = false;
    public TextMeshProUGUI[] text; //1.name, 2.point
    public Image image;
    public GameObject recipeSlot;
    public RecipeSlot slot;

    public bool[] able = new bool[4]; //1 workbench, 2furnace, 3campfire

    private void Awake()
    {
        if(!furniture)
        {
            recipeSlot.SetActive(true);
            RecipeSlot slot = recipeSlot.GetComponent<RecipeSlot>();
            slot.id = id;
        }
        else
        {
            Destroy(recipeSlot);
        }

        button = GetComponent<Button>();
        text = new TextMeshProUGUI[2];
        image = transform.GetChild(0).GetComponent<Image>();
        text[0] = transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        text[1] = transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>();
        text[1].text = point.ToString();
    }
    private void Start()
    {
        if (!furniture) 
        { 
            image.sprite = ItemDatabase.Instance.GetItem(id).sprite;
            text[0].text = ItemDatabase.Instance.GetItem(id).itemName;
        }
        else
        {
            image.sprite = FurnitureDatabase.Instance.furnitures[id].sprite;
            text[0].text = FurnitureDatabase.Instance.furnitures[id].itemName;
        }
        
    }

    public void ActiveButton() // 기술 해금 창 열기
    {
        if (GameManager.Instance.CheckTechPoint(point)) return;
        FurnitureDatabase.Instance.OpenUnlockPanel(this);
    }

    public void ActiveSlot() // 기술 해금 활성화
    {
        GameManager.Instance.TechnicPointUse(point);
        if(!furniture)
        {
            for (int i = 0; i < FurnitureDatabase.Instance.RecipeSlots.Length; i++)
            {
                if (able[i]) FurnitureDatabase.Instance.RecipeSlots[i].Add(recipeSlot);
            }
            recipeSlot.SetActive(false);
            recipeSlot.transform.SetParent(GameManager.Instance.recipeCraftPanel.transform);
            recipeSlot.transform.localScale = Vector3.one;
        }
        else
        {
            FurnitureDatabase.Instance.OpenFruniture(id);
        }
        button.interactable = false;
        text[1].transform.parent.gameObject.SetActive(false);
    }
}
