using oh.skill;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using tang.oh.data;
using UnityEngine;

public class TestCharacterAnimation : MonoBehaviour
{

    ResourceLoader _resourceLoader = new ResourceLoader();
    CharacterMove _characterMove;
    Coroutine coroutineFireBulletLoop;

    // Start is called before the first frame update
    void Start()
    {
        _characterMove = GameObject.FindObjectOfType<CharacterMove>();
        coroutineFireBulletLoop = StartCoroutine(FireBulletLoop());
        
    }
    private void OnDisable()
    {
        StopCoroutine(coroutineFireBulletLoop);
    }

    IEnumerator FireBulletLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            FireBullet();
        }
    }

    async void FireBullet()
    {
        var datumSkill = DataSkill.Instance.Get("skill_bullet");
        var skillInstance = await _resourceLoader.GetAsync<GameObject>("skill_bullet");
        var skillObject = Instantiate(skillInstance);
        var skill = skillObject.GetComponent<Skill>();
        skill.Init(datumSkill);
        skill.Trigger(_characterMove.transform.position, _characterMove.LastDirection * datumSkill.range);
        await Task.Delay(1000);
        skill.OnHit();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
