using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Header : MonoBehaviour
{
    bool isActive = false;

    public float GetHeight()
    {
        return GetComponent<RectTransform>().rect.height;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
