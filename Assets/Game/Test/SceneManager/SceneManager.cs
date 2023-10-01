using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace oh
{
    public enum eScene
    {
        Lobby = 0
    }
    public class SceneManager : MonoBehaviour
    {
        // Start is called before the first frame update

        private void Awake()
        {

        }
        void Start()
        {
            LoadScene();
        }

        public async void LoadScene(eScene eScene = eScene.Lobby)
        {
            if (eScene == eScene.Lobby)
            {
                BaseUI baseUI = new BaseUI();
                var props = new PageSampleProps() 
                { 
                    title = "본문11",
                    description = "설명22"
                };
                var pageSample = await baseUI.Open<PageSample>(props);
            }
        }
    }

}

