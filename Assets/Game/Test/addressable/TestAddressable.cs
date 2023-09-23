using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class TestAddressable : MonoBehaviour
{
    ResourceLoader resourceLoader = new ResourceLoader();

    private void Awake()
    {
        //MakeObject();
    }
    // Start is called before the first frame update
    private void OnEnable()
    {
        Debug.Log("1");
        //MakeObject();
        Debug.Log("2");
    }
    void Start()
    {
        MakeObjects();
    }
    async void MakeObject2()
    {
        var resourcePath = GameObject.FindObjectOfType<ResourcePath>();
        var asset = await resourcePath.TestImage.LoadAssetAsync<GameObject>().Task;
        Instantiate(asset);
    }

    void MakeObjects()
    {
        for(var i = 0; i < 10; i++)
        {
            MakeObject();
        }
    }
    async void MakeObject()
    {
        await Task.Delay(1000);
        var obj = await resourceLoader.GetAsync<GameObject>("key1");
        var gameobject = Instantiate(obj);
        gameobject.transform.Find("Canvas/Image").localPosition = new Vector3(Random.Range(-300f, 300f), 0, 0);
    }

    private void OnDisable()
    {
        resourceLoader.UnLoadAll();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
