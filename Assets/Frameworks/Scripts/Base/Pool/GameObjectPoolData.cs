using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Frameworks
{
    public class GameObjectPoolData
    {
        // 父节点
        public GameObject fatherObj;

        // 对象容器
        private readonly Queue<GameObject> poolQueue;


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

        public GameObject GetObj()
        {
            GameObject obj = poolQueue.Dequeue();
            obj.SetActive(true);
            obj.transform.parent = null;
            // 对象池是DontDestroy，拿出来以后需要放到当前加载场景
            SceneManager.MoveGameObjectToScene(obj, SceneManager.GetActiveScene());
            return obj;
        }

        public bool IsEmpty()
        {
            return poolQueue.Count > 0;
        }
    }
}