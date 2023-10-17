using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
public class UnityTool
{
    private static UnityTool instance;
    public static UnityTool Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new UnityTool();
            }
            return instance;
        }
    }
    private GameObject m_Canvas;
    public UnityTool()
    {
        m_Canvas = GameObject.Find("MainCanvas")?.gameObject;
        EventCenter.Instance.RegisterObserver(EventType.OnSceneChangeComplete, () =>
        {
            m_Canvas = GameObject.Find("MainCanvas")?.gameObject;
        });
    }
    public GameObject GetGameObjectFromCanvas(string name)
    {
        return GetGameObjectInChild(m_Canvas, name);
    }
    public GameObject GetGameObjectInChild(GameObject parent, string name)
    {
        Transform[] childs = parent.transform.GetComponentsInChildren<Transform>(true);
        foreach (Transform child in childs)
        {
            if (child.name == name)
            {
                return child.transform.gameObject;
            }
        }
        return null;
    }
    public T GetComponentFromChild<T>(GameObject parent, string name)
    {
        return GetGameObjectInChild(parent, name).GetComponent<T>();
    }
    public PlantType ButtonNameToPlantType(string name)
    {
        return (PlantType)Enum.Parse(typeof(PlantType), name.Replace("Card", ""));
    }
    public object ChangeType(string val,Type type)
    {
        if(type==typeof(bool))
        {
            if (val == "TRUE")
            {
                return true;
            }
            else if (val == "FALSE")
            {
                return false;
            }
        }
        if(typeof(System.Enum).IsAssignableFrom(type))
        {
            return System.Enum.Parse(type, val);
        }
        return Convert.ChangeType(val, type);
    }
    public void WriteDataToListFromTextAsset<T>(List<T> list,TextAsset textAsset) where T : new()
    {
        if (textAsset == null) return;
        list.Clear();
        string text=textAsset.text.Replace("\r","");
        string[] rows=text.Split('\n');
        string[] filedNames = rows[0].Split(',');
        for(int row=1;row<rows.Length;row++)
        {
            if (rows[row] == "") continue;
            Type type = typeof(T);
            T item = new T();
            string[] columes = rows[row].Split(",");
            for(int colume=0;colume<columes.Length;colume++)
            {
                if (columes[colume] == "") continue;
                FieldInfo info = type.GetField(filedNames[colume]);
                if(info==null) continue;
                info.SetValue(item, ChangeType(columes[colume], info.FieldType));
            }
            list.Add(item);
        }
    }
}
