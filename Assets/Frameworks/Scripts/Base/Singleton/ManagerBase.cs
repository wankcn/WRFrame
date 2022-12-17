using UnityEngine;

namespace Frameworks
{
    interface IManagerBase
    {
        void Init();
    }

    public class ManagerBase<T> : MonoBehaviour, IManagerBase where T : ManagerBase<T>
    {
        private static T instance;
        public static T Instance => instance;

        /// 管理器的初始化
        public virtual void Init()
        {
            instance = this as T;
        }
    }
}