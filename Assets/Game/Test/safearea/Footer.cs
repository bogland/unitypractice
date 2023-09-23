using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footer : MonoBehaviour
{
    // Start is called before the first frame update
    bool isActive = false;
    public float GetHeight()
    {
        return GetComponent<RectTransform>().rect.height;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
