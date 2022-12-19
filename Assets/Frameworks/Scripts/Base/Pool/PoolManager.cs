using System.Collections.Generic;
using UnityEngine;

namespace Frameworks
{
    public class PoolManager : ManagerBase<PoolManager>
    {
        // 游戏物体根节点
        [SerializeField] private GameObject poolRoot;

        // 对象容器 <名称，数据>
        private Dictionary<string, GameObjectPoolData> goPoolDic = new Dictionary<string, GameObjectPoolData>();


        public override void Init()
        {
            base.Init();
            print("== Debug in PoolManager Init ==");
        }


        public T GetGameObject<T>(GameObject prefab) where T : Object
        {
            GameObject obj = GetGameObject(prefab);
            if (obj != null) return obj.GetComponent<T>();
            return null;
        }

        public GameObject GetGameObject(GameObject prefab)
        {
            GameObject obj = null;

            string prefabName = prefab.name;
            if (CheckGameObjectCache(prefabName))
            {
                obj = goPoolDic[prefabName].GetObj();
            }
            else
            {
                obj = Instantiate(prefab);
                // 必要重命名
                obj.name = prefabName;
            }

            return obj;
        }


        public void PushGameObject(GameObject obj)
        {
            string keyName = obj.name;
            if (goPoolDic.ContainsKey(keyName))
            {
                goPoolDic[keyName].PushObj(obj);
            }
            else
            {
                goPoolDic.Add(keyName, new GameObjectPoolData(obj, poolRoot));
            }
        }


        /// check has prefab,isNull create a new Prefab
        public bool CheckGameObjectCache(GameObject prefab)
        {
            return CheckGameObjectCache(prefab.name);
        }

        private bool CheckGameObjectCache(string keyName)
        {
            return goPoolDic.ContainsKey(keyName) && goPoolDic[keyName].IsEmpty();
        }

        public void Clear()
        {
            goPoolDic.Clear();
        }
    }
}