using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Frameworks
{
    public class SingletonMono<T> : MonoBehaviour where T : SingletonMono<T>
    {
        private static T instance;

        // 继承Mono 不能直接new 只能通过拖拽或者加脚本api U3D内部帮助实例化
        public static T Instance => instance;

        protected virtual void Awake()
        {
            instance = this as T;
        }
    }
}