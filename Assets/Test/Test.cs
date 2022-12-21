using System;
using Frameworks;
using UnityEngine;


public class Test : MonoBehaviour
{
    public GameObject go;


    private void Start()
    {
        PoolManager.Instance.GetGameObject(go);
        PoolManager.Instance.GetGameObject(go);
        PoolManager.Instance.GetGameObject(go);
        GameObject go2 = PoolManager.Instance.GetGameObject(go);
        PoolManager.Instance.PushGameObject(go2);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            PoolManager.Instance.ClearGameObject(go);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            var g1 = PoolManager.Instance.GetGameObject(go);
            var g2 = PoolManager.Instance.GetGameObject(go);
            var g3 = PoolManager.Instance.GetGameObject(go);
            PoolManager.Instance.PushGameObject(g1);
            PoolManager.Instance.PushGameObject(g2);
            PoolManager.Instance.PushGameObject(g3);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            PoolManager.Instance.ClearAllGameObject();
        }
    }
}