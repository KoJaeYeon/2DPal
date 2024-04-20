using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;

public class Data
{
    public List<PalData> palDatas;
    public PlayerData playerData;
    public List<ItemData> itemDatas;
    public Dictionary<int,int> itemSum;
}

public class PalData
{
    public int key;
    public int id;
    public int lv;
    public PalData(int key, int id, int lv)
    {
        this.key = key;
        this.id = id;
        this.lv = lv;
    }
}

public class PlayerData
{
    public int lv;
    public int exp;
    public float hungry;
    public float health;
    public float maxHealth;
    public float moveWeight;
    public int skillPoint;
    public int TechPoint;

    public float maxStamina;
    public float nowStamina;

    public float playerPosX;
    public float playerPosY;

    public PlayerData(int lv, int exp, float hungry, float health, float maxHealth, float moveWeight, int skillPoint, int techPoint, float maxStamina, float nowStamina, float playerPosX, float playerPosY)
    {
        this.lv = lv;
        this.exp = exp;
        this.hungry = hungry;
        this.health = health;
        this.maxHealth = maxHealth;
        this.moveWeight = moveWeight;
        this.skillPoint = skillPoint;
        TechPoint = techPoint;
        this.maxStamina = maxStamina;
        this.nowStamina = nowStamina;
        this.playerPosX = playerPosX;
        this.playerPosY = playerPosY;
    }
}

public class ItemData
{
    public int key;
    public int id;
    public int count;
    public ItemData(int key, int id, int count)
    {
        this.key = key;
        this.id = id;
        this.count = count;
    }
}

public class DataManager : Singleton<DataManager>
{

    Data data  = new Data();
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
    public void Save()
    {
        #region PalBox
        data.palDatas = new List<PalData>();
        for (int i = 0; i < 110; i++)
        {
            if(PalBoxManager.Instance.palBox.ContainsKey(i))
            {
                Pal pal = PalBoxManager.Instance.palBox[i];
                data.palDatas.Add(new PalData(i,pal.id,pal.lv));
            }
        }
        #endregion
        #region Player
        PlayerControll player = GameManager.Instance.playerController.GetComponent<PlayerControll>();
        data.playerData = new PlayerData(player.lv,player.exp,player.hungry,player.health,player.maxHealth,
            player.moveWeight,player.skillPoint,player.TechPoint,player.maxStamina,player.nowStamina,player.transform.position.x,player.transform.position.y);
        #endregion
        #region Inventory
        data.itemDatas = new List<ItemData>();

        foreach (int key in InventoryManager.Instance.inventory.Keys)
        {
            Item item = InventoryManager.Instance.inventory[key];
            data.itemDatas.Add(new ItemData(key, item.id, item.count));
        }
        data.itemSum = InventoryManager.inventorySum;
        #endregion

        var json = JsonConvert.SerializeObject(data);
        File.WriteAllText(Application.dataPath + "/savedata.json", json);
        Debug.Log(Application.dataPath);
        Debug.Log(json);
    }

    public void Load()
    {
        var json = File.ReadAllText(Application.dataPath + "/savedata.json");
        Debug.Log(json);
        data = JsonConvert.DeserializeObject<Data>(json);

        PalBoxManager.Instance.LoadAllSlot(data.palDatas); //ÆÓ¹Ú½º
        LoadPlayerData();
        InventoryManager.Instance.LoadAllSlot(data.itemDatas);
        InventoryManager.inventorySum = data.itemSum;
    }

    public void LoadPlayerData()
    {
        PlayerControll player = GameManager.Instance.playerController.GetComponent<PlayerControll>();
        player.lv = data.playerData.lv;
        player.exp = data.playerData.exp;
        player.hungry = data.playerData.hungry;
        player.health = data.playerData.health;
        player.maxHealth = data.playerData.maxHealth;
        player.moveWeight = data.playerData.moveWeight;
        player.skillPoint = data.playerData.skillPoint;
        player.TechPoint = data.playerData.TechPoint;
        player.maxStamina = data.playerData.maxStamina;
        player.nowStamina = data.playerData.nowStamina;
        player.transform.position = new Vector2(data.playerData.playerPosX, data.playerData.playerPosY);
    }
}
