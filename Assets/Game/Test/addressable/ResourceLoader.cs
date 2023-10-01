using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

struct ObjectType
{
    public Type type;
    public object obj;
    public AsyncOperationHandle handler;
}

public class ResourceLoader
{
    Dictionary<string, ObjectType> assetDic = new Dictionary<string, ObjectType>();
    ~ResourceLoader()
    {
        UnLoadAll();
        assetDic.Clear();
    }

    public async Task<T> LoadAsync<T>(string key)
    {
        var handler = Addressables.LoadAssetAsync<T>(key);
        Debug.Log("key " + key);
        var asset = await handler.Task;
        if(asset == null)
        {
            return default;
        }
        ObjectType objectType = new ObjectType()
        {
            obj = asset,
            type = typeof(T),
            handler = (AsyncOperationHandle) handler,
        };
        assetDic.TryAdd(key, objectType);
        return asset;
    }
    public void UnLoadAll()
    {
        foreach(var one in assetDic)
        {
            assetDic.Remove(one.Key);
            Addressables.ReleaseInstance(one.Value.handler);
            
        }
    }

    public async Task<T> GetAsync<T>(string key)
    {
        try
        {
            if (assetDic.TryGetValue(key, out var objectType))
            {
                return ConvertObject<T>(objectType.obj);
            }
            else
            {
                var obj = await LoadAsync<T>(key);
                return obj;
            }
        }
        catch(Exception e)
        {
            return default;
        }
        
    }

    public T ConvertObject<T>(object input)
    {
        return (T)Convert.ChangeType(input, typeof(T));
    }

    public void Unload(string key)
    {
        if(assetDic.TryGetValue(key, out var objectType))
        {
            Addressables.ReleaseInstance(objectType.handler);
        }

    }
}
