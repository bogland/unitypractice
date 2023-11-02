using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tang.oh.data;
using UnityEngine;

namespace oh.skill
{
    public class Skill : MonoBehaviour
    {
        GameObject _skillObjectRoot;
        GameObject _preCast;
        GameObject _cast;
        GameObject _onHit;
        Dictionary<SkillState,GameObject> stateObjectDics = new Dictionary<SkillState, GameObject>();

        DatumSkill _datumSkill;
        SkillState _skillState = SkillState.None;
        Vector3 _origin;
        Vector3 _target;

        enum SkillState
        {
            None,
            PreCast,
            Cast,
            OnHit,
            Destory,
        }
        public void Init(DatumSkill datumSkill)
        {
            _preCast = gameObject.transform.Find("PreCast").gameObject;
            _cast = gameObject.transform.Find("Cast").gameObject;
            _onHit = gameObject.transform.Find("OnHit").gameObject;

            stateObjectDics.Clear();
            stateObjectDics.Add(SkillState.PreCast,_preCast);
            stateObjectDics.Add(SkillState.Cast,_cast);
            stateObjectDics.Add(SkillState.OnHit,_onHit);

            _datumSkill = datumSkill;
        }

        public void Trigger(Vector3 origin, Vector3 target)
        {
            transform.position = origin;
            _origin = origin;
            _target = target;
            Activate(SkillState.PreCast);
        }
        public async void OnHit()
        {
            var bulletRigid = gameObject.GetComponent<Rigidbody2D>();
            bulletRigid.velocity = Vector2.zero;
            Activate(SkillState.OnHit);
            var timeEffectSecond = 1;
            var timeCast = (int) Mathf.Floor(timeEffectSecond * 1000f);
            await Task.Delay(timeCast);
            Activate(SkillState.Destory);
        }

        public void Destroy(bool instant = false)
        {
            if (instant)
            {

            }
        }


        void Activate(SkillState skillState)
        {
            _skillState = skillState;
            stateObjectDics.Values.ToList().ForEach(v => v.SetActive(false));
            GameObject stateObject;
            switch (_skillState)
            {
                case SkillState.PreCast:
                    if(stateObjectDics.TryGetValue(SkillState.PreCast, out stateObject))
                    {
                        stateObject.SetActive(true);
                    }
                    PreCast();
                    break;
                case SkillState.Cast:
                    if (stateObjectDics.TryGetValue(SkillState.Cast, out stateObject))
                    {
                        stateObject.SetActive(true);
                    }
                    Cast();
                    break;
                case SkillState.OnHit:
                    if (stateObjectDics.TryGetValue(SkillState.OnHit, out stateObject))
                    {
                        stateObject.SetActive(true);
                    }
                    break;
                case SkillState.Destory:
                    Destroy();
                    break;
                default:
                    break;
            }
        }

        async void PreCast()
        {
            //

            Activate(SkillState.Cast);
        }
        async void Cast()
        {
            var bulletRigid = gameObject.GetComponent<Rigidbody2D>();
            var direction = (_target - transform.position).normalized;
            transform.right = direction;
            bulletRigid.velocity = direction * _datumSkill.speed;
        }

    }

}
