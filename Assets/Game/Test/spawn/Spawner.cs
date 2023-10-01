using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using tang.oh.data;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.U2D;

namespace tang.oh
{
    public class Spawner : MonoBehaviour
    {
        ResourceLoader _resourceLoader = new ResourceLoader();
        List<DatumSpawn> dataSpawnList;
        
        float timeElapsed = 0f;
        // Start is called before the first frame update
        void Awake()
        {
            dataSpawnList = DataSpawn.Instance.GetList();
        } 

        private void OnEnable()
        {
            InvokeRepeating("SpawnEnemy", 0,1);
        }
        private void OnDisable()
        {
            CancelInvoke("SpawnEnemy");
        }

        async void SpawnEnemy()
        {
            timeElapsed += 1;
            var spawnList = dataSpawnList.Where(v => timeElapsed > v.timeStart && timeElapsed <= v.timeEnd);
            var posPlayer = GameObject.FindObjectOfType<Player>().transform.position;
            var canvas = FindObjectOfType<Canvas>();
            foreach (var one in spawnList)
            {
                var pos = new Vector3(posPlayer.x + UnityEngine.Random.Range(-100.0f, 100.0f), posPlayer.y + UnityEngine.Random.Range(-100.0f, 100.0f), 0);
                var mobInfo = DataMonster.Instance.Get(one.idMonster);
                var enemy = await _resourceLoader.GetAsync<GameObject>("enemy");
                var imagePack = await _resourceLoader.GetAsync<SpriteAtlas>("atlas");
                var sprite = await _resourceLoader.GetAsync<Sprite>(mobInfo.imageKey);
                enemy.GetComponent<Enemy>().Image.sprite = sprite;
                var enemyObj = await Addressables.InstantiateAsync(enemy, pos, Quaternion.identity).Task;
                enemyObj.transform.parent = canvas.transform;
                Debug.Log($"<color=yellow>{DateTime.Now} {one.id} {one.stage} {one.idMonster} </color>");
            }
        }
    }
   
}
