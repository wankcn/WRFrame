using System;
using Frameworks;
using UnityEngine;

namespace Frameworks
{
    public class TestPool
    {
        public void Init()
        {
            Debug.Log("shenghcneg!");
        }

        public void Dispose()
        {
            PoolManager.Instance.PushObject(this);
        }
    }
}