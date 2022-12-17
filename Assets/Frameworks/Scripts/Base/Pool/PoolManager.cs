using UnityEngine;

namespace Frameworks
{
    public class PoolManager : ManagerBase<PoolManager>
    {
        public override void Init()
        {
            base.Init();
            print("PoolManager -- Init");
        }
    }
}