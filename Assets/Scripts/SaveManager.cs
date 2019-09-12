using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public SlotInfo[] saveData;
    public string path;

    private void Update()
    {
        if (Input.GetKeyDown("s"))
        {
            Save();
        }
    }

    public void Save()
    {
        Slot[] allSlots = FindObjectsOfType<Slot>(); //Talvez salvar isso como lista e dar sort por info de sala

        saveData = new SlotInfo[allSlots.Length];

        for (int i = 0; i < saveData.Length; i++)
        {
            saveData[i] = new SlotInfo(allSlots[i]);
            //saveData[i] = new SlotInfo();
            //saveData[i].slotName = i.ToString();
            //saveData[i].objectName = "test";
        }

        WriteJson(saveData);

    }    

    public void WriteJson(SlotInfo[] data)
    {
        string json = JsonUtility.ToJson(new ArraySlotInfo(data), true);

        Debug.Log(json);

        File.WriteAllText(path + "/save01.json", json);

    }

}

[Serializable]
public class SlotInfo
{
    public string slotName; //Trocar isso aqui para alguma identificação de sala
    public string objectName;

    public SlotInfo(Slot slot)
    {
        this.slotName = slot.gameObject.name;
        if (slot.placedObject != null)
            this.objectName = slot.placedObject.name;
        else
            this.objectName = "*vazio*";
    }
}

[Serializable]
public class ArraySlotInfo
{
    public SlotInfo[] slotInfos;

    public ArraySlotInfo(SlotInfo[] slotInfos)
    {
        this.slotInfos = slotInfos;
    }
}
