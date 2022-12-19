using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Frameworks
{
    public class PoolManager : ManagerBase<PoolManager>
    {
        // 游戏物体根节点
        private GameObject poolRoot;

        // 对象容器 <名称，数据>
        private Dictionary<string, GameObjectPoolData> goPoolDic;

        // 普通类容器
        private Dictionary<string, ObjectPoolData> objectPoolDic;


        public override void Init()
        {
            base.Init();
            print("== Debug in PoolManager Init ==");
            goPoolDic = new Dictionary<string, GameObjectPoolData>();
            objectPoolDic = new Dictionary<string, ObjectPoolData>();
            // 创建根节点
            poolRoot = new GameObject("Pool");
            poolRoot.transform.SetParent(GameRoot.Instance.transform);
        }


        #region GameObject

        public T GetComponent<T>(GameObject prefab, Transform parent = null) where T : UnityEngine.Object
        {
            GameObject obj = GetGameObject(prefab, parent);
            if (obj != null) return obj.GetComponent<T>();
            return null;
        }

        public GameObject GetGameObject(GameObject prefab, Transform parent = null)
        {
            GameObject obj = null;

            string prefabName = prefab.name;
            if (CheckGameObjectCache(prefabName))
            {
                obj = goPoolDic[prefabName].GetObj(parent);
            }
            else
            {
                obj = Instantiate(prefab, parent);
                // 必要重命名
                obj.name = prefabName;
            }

            return obj;
        }

        public void PushGameObject(GameObject obj)
        {
            string keyName = obj.name;
            if (goPoolDic.TryGetValue(keyName, out GameObjectPoolData poolData))
                poolData.PushObj(obj);
            else
                goPoolDic.Add(keyName, new GameObjectPoolData(obj, poolRoot));
        }

        /// check has prefab,isNull create a new Prefab
        private bool CheckGameObjectCache(string keyName)
        {
            return goPoolDic.ContainsKey(keyName) && goPoolDic[keyName].IsEmpty();
        }

        #endregion

        #region Object

        public T GetObject<T>() where T : class, new()
        {
            // 相比 typeof(T).FullName 不需要判空
            string keyName = typeof(T).CSharpFullName();
            if (CheckObjectCache(keyName))
                return (T)objectPoolDic[keyName].GetObj();
            return new T();
        }


        public void PushObject(object o)
        {
            string keyName = o.GetType().CSharpFullName();
            if (objectPoolDic.TryGetValue(keyName, out ObjectPoolData poolData))
                poolData.PushObj(o);
            else
                objectPoolDic.Add(keyName, new ObjectPoolData(o));
        }


        private bool CheckObjectCache(string keyName)
        {
            return objectPoolDic.ContainsKey(keyName) && objectPoolDic[keyName].IsEmpty();
        }

        #endregion

        #region Delete

        public void Clear(bool isClearGameObject = true, bool isClearObject = true)
        {
            if (isClearObject)
            {
                for (int i = poolRoot.transform.childCount - 1; i >= 0; i--)
                {
                    Destroy(poolRoot.transform.GetChild(i).gameObject);
                }

                goPoolDic.Clear();
            }

            if (isClearObject) objectPoolDic.Clear();
        }

        public void ClearAllGameObject()
        {
            Clear(true, false);
        }

        public void ClearGameObject(GameObject prefab)
        {
            ClearGameObject(prefab.name);
        }

        public void ClearGameObject(string prefabName)
        {
            // Find拿到null无法拿到gameObject
            Transform trans = poolRoot.transform.Find(prefabName);
            if (trans != null)
            {
                Destroy(trans.gameObject);
                goPoolDic.Remove(prefabName);
            }
        }

        public void ClearAllObject()
        {
            Clear(false);
        }

        public void ClearObject<T>()
        {
            objectPoolDic.Remove(typeof(T).CSharpFullName());
        }

        public void ClearObject(Type type)
        {
            objectPoolDic.Remove(type.CSharpFullName());
        }

        #endregion
    }
}