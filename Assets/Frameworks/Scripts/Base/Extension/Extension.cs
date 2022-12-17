using System;
using System.Reflection;
using UnityEngine;

namespace Frameworks
{
    public static class Extension
    {
        /// <summary>
        /// 获取特性
        /// </summary>
        public static T GetAttribute<T>(this object obj) where T : Attribute
        {
            return obj.GetType().GetCustomAttribute<T>();
        }

        /// <summary>
        /// 获取特性
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="type">特性所在的类型</param>
        public static T GetAttribute<T>(this object obj, Type type) where T : Attribute
        {
            return type.GetCustomAttribute<T>();
        }
    }
}