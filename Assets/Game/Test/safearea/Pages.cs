using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class Pages : MonoBehaviour
{
    public Transform _safeArea;
    // Start is called before the first frame update
    void Start()
    {
        UpdatePageAnchor();

        //var page1 = Managers.Instance.UI.ShowPopupUI<Page1>("Page1");
        //page1.transform.parent = _safeArea;
        //var rect = page1.GetComponent<RectTransform>();
        //rect.anchorMin = new Vector2(0, 0);
        //rect.anchorMax = new Vector2(1, 1);
        //rect.localScale = Vector3.one;
        //rect.localPosition = Vector3.zero;
        //rect.sizeDelta = Vector2.zero;

        //var popup1 = Managers.Instance.UI.ShowPopupUI<Popup1>("Popup1");

    }

    async Task UpdatePageAnchor()
    {
        // Common의 header와 footer height를 구하는 과정이 느려서 딜레이 필요
        await Task.Delay(100);
        var anchorPage = FindObjectOfType<Common>().GetPageAnchor();
        var safeArea = GetComponentsInChildren<RectTransform>().Single(v=>v.name=="SafeArea");
        safeArea.anchorMin = anchorPage.Item1;
        safeArea.anchorMax = anchorPage.Item2;
        safeArea.offsetMax = new Vector2(safeArea.offsetMax.x, 0f);
        safeArea.offsetMin = new Vector2(safeArea.offsetMin.x, 0f);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
