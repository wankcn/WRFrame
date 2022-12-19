using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Frameworks
{
    /// <summary>
    /// GameObject的数据类型
    /// </summary>
    public class GameObjectPoolData
    {
        // 父节点
        private readonly GameObject fatherObj;

        // 对象容器
        private readonly Queue<GameObject> poolQueue;


        /// <summary>
        /// 需要GameObject对象和库存节点
        /// </summary>
        public GameObjectPoolData(GameObject obj, GameObject poolRootObj)
        {
            // 创建父节点并设置父节点在root下
            fatherObj = new GameObject(obj.name);
            fatherObj.transform.SetParent(poolRootObj.transform);
            poolQueue = new Queue<GameObject>();

            // 首次入队
            PushObj(obj);
        }


        public void PushObj(GameObject obj)
        {
            poolQueue.Enqueue(obj);
            obj.transform.SetParent(fatherObj.transform);
            obj.SetActive(false);
        }

        public GameObject GetObj(Transform parent = null)
        {
            GameObject obj = poolQueue.Dequeue();
            obj.SetActive(true);
            // 对象池是DontDestroy，拿出来以后需要设置父节点，如果为空，拿到当前场景
            obj.transform.SetParent(parent);
            if (parent == null)
                SceneManager.MoveGameObjectToScene(obj, SceneManager.GetActiveScene());
            return obj;
        }

        public bool IsEmpty()
        {
            return poolQueue.Count > 0;
        }
    }


    /// <summary>
    /// Object类型
    /// </summary>
    public class ObjectPoolData
    {
        private readonly Queue<object> poolQueue;

        public ObjectPoolData(object o)
        {
            // 第一次才会执行
            poolQueue = new Queue<object>();
            PushObj(o);
        }

        public void PushObj(object o)
        {
            poolQueue.Enqueue(o);
        }

        public object GetObj()
        {
            return poolQueue.Dequeue();
        }


        public bool IsEmpty()
        {
            return poolQueue.Count > 0;
        }
    }
}