using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Object = UnityEngine.Object;
using LitJson;

public class SaveSystemByJson
{
    public static void SaveDataByJson(object data,string fileName)//注意是object而不是Object
    {
        string path = Path.Combine(Application.persistentDataPath, fileName);
        string json = JsonMapper.ToJson(data);

        try
        {
            File.WriteAllText(path, json);
            
#if UNITY_EDITOR
            Debug.Log($"Successfully Save Data{data} Into {path}");
        }
        catch (Exception e)
        {
            Debug.LogWarning($"Failed Save Data{data} Into {path}" + e );
            throw;
        }
#endif
    }

    public static T LoadDataFromJson<T>(string fileName)
    {
        string path = Path.Combine(Application.persistentDataPath, fileName);
       
        try
        {
            string json = File.ReadAllText(path);
            var data = JsonMapper.ToObject<T>(json);
          
#if UNITY_EDITOR
            Debug.Log($"Successfully Load Data{data} From {path}");
#endif
            return data;
        }
#if UNITY_EDITOR
        catch (Exception e)
        {
            Debug.LogWarning($"Failed Load Data From {path}" + e);
            return default;
        }
#endif
    }

    public static void DeleteJsonData(string fileName)
    {
        string path = Path.Combine(Application.persistentDataPath, fileName);

        try
        {
            if(File.Exists(path))
                File.Delete(path);
#if UNITY_EDITOR
            Debug.Log($"Successfully Delete Data From{path}");
        }
        catch (Exception e)
        {
            Debug.LogWarning($"Fail Delete Data From{path}" + e);
            throw;
        }
#endif
    }
    
}
