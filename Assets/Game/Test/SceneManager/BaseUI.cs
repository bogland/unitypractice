using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace oh
{
    public class UIProperty
    {

    }

    public class BaseUI : MonoBehaviour
    {
        ResourceLoader _ResourceLoader = new ResourceLoader();
        GameObject _UIObject;
        public virtual async Task<T> Open<T>()
        {
            var target = await _ResourceLoader.GetAsync<GameObject>(typeof(T).Name);
            if(target == null)
            {
                return default;
            }

            var gameObject = GameObject.Instantiate(target);
            var result = gameObject.GetComponent<T>();

            if (result == null)
            {
                Debug.LogError("오브젝트 발견 못함 " + typeof(T).Name);
                Destroy(gameObject);
                return default;
            }

            var parent = GameObject.FindObjectOfType<Pages>().transform;
            if (parent == null)
            {
                Debug.LogError("SafeArea 없음 ");
                Destroy(gameObject);
                return default;
            }
            gameObject.transform.parent = parent;
            var rectTransform = gameObject.GetComponent<RectTransform>();
            rectTransform.localScale = Vector3.one;
            rectTransform.anchorMin = Vector2.zero;
            rectTransform.anchorMax = Vector2.one;
            rectTransform.offsetMin = Vector2.zero;
            rectTransform.offsetMax = Vector2.one;

            return result;
        }



        public virtual void Close()
        {
            Destroy(gameObject);
        }

        public virtual void Init(UIProperty uIProperty)
        {

        }
        public virtual void Refresh(UIProperty uIProperty)
        {

        }
    }
}

