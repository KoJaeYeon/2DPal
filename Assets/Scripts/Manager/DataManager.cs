using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;
using UnityEngine.UIElements;
using System;

public class Data
{
    public string saveDate;
    public string saveTime;

    public float bgmVolume;
    public float sfxVolume;

    public float time;
    public int day;

    public int worldIndexNum;

    public List<bool> dropActive;
    public List<PalData> palDatas;
    public PlayerData playerData;
    public List<ItemData> itemDatas;
    public Dictionary<int, int> itemSum;
    public List<int> techList;
    public List<FurnitureData> furnitureDatas;
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
    public int maxExp;
    public float hungry;
    public float health;
    public float maxHealth;
    public float moveWeight;
    public int skillPoint;
    public int TechPoint;

    public float maxStamina;
    public float nowStamina;
    public float attack;

    public float playerPosX;
    public float playerPosY;

    public PlayerData(int lv, int exp,int maxExp, float hungry, float health, float maxHealth, float moveWeight, int skillPoint, int techPoint, float maxStamina, float nowStamina,float attack, float playerPosX, float playerPosY)
    {
        this.lv = lv;
        this.exp = exp;
        this.maxExp = maxExp;
        this.hungry = hungry;
        this.health = health;
        this.maxHealth = maxHealth;
        this.moveWeight = moveWeight;
        this.skillPoint = skillPoint;
        TechPoint = techPoint;
        this.maxStamina = maxStamina;
        this.nowStamina = nowStamina;
        this.attack = attack;
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

public class ItemSumData
{
    public int id;
    public int count;
    public ItemSumData(int id, int count)
    {
        this.id = id;
        this.count = count;
    }
}

public class FurnitureData
{
    public int id;
    public int index;
    public int buildingType;
    public int buildingStatement;
    public float nowContructTime;
    public float maxWorkTime;
    public float nowWorkTime;
    public float positionX;
    public float positionY;
    public int productId;
    public int productCount;
    public FurnitureData(int id, int index,int buildingType, int buildingStatement, float nowContructTime,float maxWorkTime, float nowWorkTime,float positionX,float positionY,  int productId, int productCount)
    {
        this.id = id;
        this.index = index;
        this.buildingType = buildingType;
        this.buildingStatement = buildingStatement;
        this.nowContructTime = nowContructTime;
        this.maxWorkTime = maxWorkTime;
        this.nowWorkTime = nowWorkTime;
        this.positionX = positionX;
        this.positionY = positionY;
        this.productId = productId;
        this.productCount = productCount;
    }
}

public class DataManager : Singleton<DataManager>
{
    public int loadNum = 0;
    Data data  = new Data();
    private void Awake()
    {
        if (DataManager.Instance != this && DataManager.Instance != null) { Destroy(this.gameObject); }
        DontDestroyOnLoad(this);
    }
    public void Save(int num)
    {
        
        data.saveDate = DateTime.Now.ToString("yyyy-MM-dd");
        data.saveTime = DateTime.Now.ToString("hh:mm:ss");
        data.bgmVolume = SoundManager.Instance.bgm.volume;
        data.sfxVolume = SoundManager.Instance.sfx[0].volume;
        data.day = GameManager.Instance.night.day;
        data.time = GameManager.Instance.night.time;

        data.worldIndexNum = Building.getBuildStaticNum();

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
        data.playerData = new PlayerData(player.lv,player.exp,player.maxExp,player.hungry,player.health,player.maxHealth,
            player.moveWeight,player.skillPoint,player.TechPoint,player.maxStamina,player.nowStamina,player.attack,player.transform.position.x,player.transform.position.y);
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
        #region Tech
        data.techList = new List<int>();
        data.techList = FurnitureDatabase.Instance.techList;
        #endregion
        #region Furniture
        data.furnitureDatas = new List<FurnitureData>();
        int length = FurnitureDatabase.Instance.ConstructedBuilding.Count;
        for(int i = 0; i < length; i++)
        {
            Building building = FurnitureDatabase.Instance.ConstructedBuilding[i];
            data.furnitureDatas.Add(new FurnitureData(building.id, building.index,(int)building.buildingType, (int)building.buildingStatement,
                building.nowConstructTime, building.MaxWorkTime, building.nowWorkTime,building.transform.position.x,building.transform.position.y, 0, 0));
            if(building.buildingType == 0 && (int)building.buildingStatement >=3) // Recipe건물이면서 생산중이었으면
            {
                if(building is PrimitiveWorkbench)
                {
                    data.furnitureDatas[i].productId = ((PrimitiveWorkbench)building).production.id;
                    data.furnitureDatas[i].productCount = ((PrimitiveWorkbench)building).production.count;
                }
                else if (building is PrimitiveFurnature)
                {
                    data.furnitureDatas[i].productId = ((PrimitiveFurnature)building).production.id;
                    data.furnitureDatas[i].productCount = ((PrimitiveFurnature)building).production.count;
                }
                else if( building is Campfire)
                {
                    data.furnitureDatas[i].productId = ((Campfire)building).production.id;
                    data.furnitureDatas[i].productCount = ((Campfire)building).production.count;
                }
            }
        }
        #endregion
        #region Environment
        data.dropActive = new List<bool>();
        length = ItemDatabase.Instance.poolParent.transform.childCount;
        for(int i = 0; i < length; i++)
        {
            data.dropActive.Add(ItemDatabase.Instance.poolParent.transform.GetChild(i).gameObject.activeSelf);
        }

        #endregion

        var json = JsonConvert.SerializeObject(data);
        File.WriteAllText(Application.dataPath + "/savedata" +num+".json", json);
    }

    public void Load()
    {
        if (loadNum == 0) return;
        var json = File.ReadAllText(Application.dataPath + "/savedata"+loadNum+".json");
        data = JsonConvert.DeserializeObject<Data>(json);

        LoadPlayerData();
        InventoryManager.Instance.LoadAllSlot(data.itemDatas); // 인벤토리
        InventoryManager.inventorySum.Clear();
        InventoryManager.inventorySum = data.itemSum;
        FurnitureDatabase.Instance.techList = data.techList; // 테크 준비
        FurnitureDatabase.Instance.LoadAll(data.furnitureDatas); //가구와 테크     
        PalBoxManager.Instance.LoadAllSlot(data.palDatas); //팰박스
        int length = data.dropActive.Count;
        for (int i = 0; i < length; i++)
        {
            ItemDatabase.Instance.poolParent.transform.GetChild(i).gameObject.SetActive(data.dropActive[i]);
        }

        SoundManager.Instance.bgmVolume(data.bgmVolume);
        SoundManager.Instance.sfxVolume(data.sfxVolume);
        GameManager.Instance.night.SetTime(data.day, data.time);
        Building.setBuildStaticNum(data.worldIndexNum);
    }

    private void LoadPlayerData()
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
        player.attack = data.playerData.attack;
        player.transform.position = new Vector2(data.playerData.playerPosX, data.playerData.playerPosY);
        GameManager.Instance.StatusRenew();
    }

    public void SetLoadNum(int num)
    {
        loadNum = num;
    }

    public string ButtonText(int num)
    {
        try
        {
            var json = File.ReadAllText(Application.dataPath + "/savedata" + num + ".json");
            data = JsonConvert.DeserializeObject<Data>(json);

            return data.day + "일차\n"  + data.saveDate + " " + data.saveTime;
        }
        catch
        {
            return null;
        }
    }
}
