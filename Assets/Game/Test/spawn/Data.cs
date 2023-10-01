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

    public class DatumSkill
    {
        public long id;
        public string imageProjectile;
        public int speed;
        public float damage;
    }

    public class DataSkill : Singleton<DataSkill>
    {
        Dictionary<string, DatumSkill> dataMonsterDic = new Dictionary<string, DatumSkill>();
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
