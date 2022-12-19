using System;
using Frameworks;
using UnityEngine;


public class Test : MonoBehaviour
{
    private TestPool p;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            p = PoolManager.Instance.GetObject<TestPool>();
            p.Init();
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            p.Dispose();
        }
    }
}