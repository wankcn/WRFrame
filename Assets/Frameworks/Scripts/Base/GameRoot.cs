using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Frameworks
{
    public class GameRoot : SingletonMono<GameRoot>
    {
        protected override void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            base.Awake();
            DontDestroyOnLoad(gameObject);

            // 初始化所有管理器
            InitManager();
        }


        private void InitManager()
        {
            IManagerBase[] managers = GetComponents<IManagerBase>();
            foreach (var t in managers) t.Init();
        }
    }
}