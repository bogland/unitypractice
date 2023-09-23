using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

enum CommonType
{
    None,
    Header = 0x01,
    Footer = 0x02,
}

public class Common : MonoBehaviour
{
    Header header;
    Footer footer;
    Image bgSafeAreaHeader;
    Image bgSafeAreaFooter;
    CanvasScaler canvasScaler;
    SafeArea safeArea;

    const int Screen_Height = 667;
    const int Screen_Width = 375;


    /* 
    Common의 경우 Header와 Footer로 나눠짐
    Header 및 Footer 사용여부에 따라서 Page 상단 하단의 Achor 제한을 줌 
 */
    [ContextMenu("GetPageAnchor")]
    public Tuple<Vector2, Vector2> GetPageAnchor()
    {
        // SafeArea + Header Height 를 page AnchorMin으로 지정
        // SafeArea + Bottom Height 를 page AnchorMax으로 지정
        
        var scaleRatio = canvasScaler.GetComponent<RectTransform>().localScale.x;
        var anchorMin = new Vector2(GetSafeAreaAnchor().Item1.x, GetSafeAreaAnchor().Item1.y + footer.GetHeight()/ Screen.height * scaleRatio);

        Debug.Log("footerHeight : " + footer.GetHeight() +" "+ Screen.height +" "+ scaleRatio); //referenceResolution
        var anchorMax = new Vector2(GetSafeAreaAnchor().Item2.x, GetSafeAreaAnchor().Item2.y - header.GetHeight()/ Screen.height * scaleRatio);
        Debug.Log(anchorMin);
        Debug.Log(anchorMax);
        return Tuple.Create<Vector2, Vector2>(anchorMin, anchorMax); ;
    }

    Tuple<Vector2,Vector2> GetSafeAreaAnchor()
    {
        var anchorMin = safeArea.GetComponent<RectTransform>().anchorMin;
        var anchorMax = safeArea.GetComponent<RectTransform>().anchorMax;
        return Tuple.Create<Vector2, Vector2>(anchorMin, anchorMax);
    }

    private void Awake()
    {
        header = GetComponentInChildren<Header>();
        footer = GetComponentInChildren<Footer>();
        bgSafeAreaHeader = GetComponentsInChildren<Image>().Single(v => v.name == "BG_SafeArea_Header");
        bgSafeAreaFooter = GetComponentsInChildren<Image>().Single(v => v.name == "BG_SafeArea_Footer");
        safeArea = GetComponentInChildren<SafeArea>();
        canvasScaler = GetComponentInChildren<CanvasScaler>();
    }

    void SetSafeAreaBGHeight()
    {
        bgSafeAreaHeader.rectTransform.anchorMax = new Vector2(1, 1f);
        bgSafeAreaHeader.rectTransform.anchorMin = new Vector2(0, safeArea.MaxAnchor.y);
        bgSafeAreaFooter.rectTransform.anchorMax = new Vector2(1, safeArea.MinAnchor.y);
        bgSafeAreaFooter.rectTransform.anchorMin = new Vector2(0, 0f);
    }

    void Start()
    {
        SetSafeAreaBGHeight();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
