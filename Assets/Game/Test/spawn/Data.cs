using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace tang.oh.data
{
    public class DatumSpawn
    {
        public long id;
        public int stage;
        public long idMonster;
        public int timeStart;
        public int timeEnd;
        public int timeGap;
        public int count;
        public eFlagSpawn flagSpawn;
    }

    public class DataSpawn : Singleton<DataSpawn>
    {
        Dictionary<string, DatumSpawn> dataDic = new Dictionary<string, DatumSpawn>();
        override protected void Init()
        {
            dataDic.Add("0", new()
            {
                id = 0,
                stage = 0,
                idMonster = 0,
                timeStart = 0,
                timeEnd = 5,
                timeGap = 1,
                flagSpawn = eFlagSpawn.none,
                count = 1
            });
            dataDic.Add("1", new()
            {
                id = 1,
                stage = 0,
                idMonster = 1,
                timeStart = 5,
                timeEnd = 10,
                timeGap = 1,
                flagSpawn = eFlagSpawn.none | eFlagSpawn.inCrowd,
                count = 1
            });
        }

        public List<DatumSpawn> GetList()
        {
            return dataDic.Values.ToList();
        }
        public DatumSpawn Get(long id)
        {
            if (dataDic.TryGetValue(id.ToString(), out var one))
            {
                return one;
            }
            return null;
        }
    }

    public enum eFlagSpawn
    {
        none = 0x0000,
        outCamera = 0x0001,
        inCrowd = 0x0002,
    }

    public class DatumMonster
    {
        public long id;
        public int hp;
        public int speed;
        public long idSkill;
        public float damage;
        public string animation;
        public string imageKey;
    }

    public class DataMonster : Singleton<DataMonster>
    {
        Dictionary<string, DatumMonster> dataDic = new Dictionary<string, DatumMonster>();

        override protected void Init()
        {
            dataDic.Add("0", new()
            {
                id = 0,
                imageKey = "image_enemy1"

            });
            dataDic.Add("1", new()
            {
                id = 1,
                imageKey = "image_enemy2"
            });
        }

        public DatumMonster Get(long id)
        {
            if (dataDic.TryGetValue(id.ToString(), out var one))
            {
                return one;
            }
            return null;
        }


    }


    public enum FlagTarget
    {
        None = 0x000,
        Me = 0x001,
        Friend = 0x002,
        Enemy = 0x004,
        Ground = 0x008,
    }

    public enum CastType
    {
        Instant ,
        Casting,
        Channeling,
    }

    public enum CastRole
    {
        Attack,
        Buf, 
        Debuf,
        Heal,
        EffectBuf,
        EffectDebuf,
        Special,
    }

    public class DatumSkill
    {
        public string id;
        public CastType castType; 
        public CastRole castRole;
        public FlagTarget flagTarget;
        public float range;
        public float timeCast;
        public float timeCool;
        public int damageStandard;
        public float timeCoolGlobal;
        public float damage;
        public float speed;
    }

    public class DataSkill : Singleton<DataSkill>
    {
        Dictionary<string, DatumSkill> dataDic = new Dictionary<string, DatumSkill>();

        protected override void Init()
        {
            dataDic.Add("skill_bullet", new DatumSkill
            { 
                id = "skill_bullet",
                castType = CastType.Instant,
                castRole = CastRole.Attack,
                flagTarget = FlagTarget.Enemy,
                timeCast = 0f,
                range = 10,
                damageStandard = 0,
                timeCool = 1,
                timeCoolGlobal = 1,
                damage = 10,
                speed = 3,
            });
        }

        public DatumSkill Get(string id)
        {
            if (dataDic.TryGetValue(id.ToString(), out var one))
            {
                return one;
            }
            return null;
        }
    }


    public class Singleton<T> where T : Singleton<T>, new()
    {
        static T mInstnace;
        public static T Instance
        {
            get
            {
                if (mInstnace == null)
                {
                    mInstnace = new T();
                    mInstnace.Init();
                }

                return mInstnace;
            }
        }

        protected virtual void Init()
        {

        }
    }
}
