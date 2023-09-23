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
    Common�� ��� Header�� Footer�� ������
    Header �� Footer ��뿩�ο� ���� Page ��� �ϴ��� Achor ������ �� 
 */
    [ContextMenu("GetPageAnchor")]
    public Tuple<Vector2, Vector2> GetPageAnchor()
    {
        // SafeArea + Header Height �� page AnchorMin���� ����
        // SafeArea + Bottom Height �� page AnchorMax���� ����
        
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
