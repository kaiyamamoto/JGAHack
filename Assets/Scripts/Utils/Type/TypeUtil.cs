using System;
using System.Reflection;
using UnityEngine;

namespace Util
{
    public class TypeUtil
    {
        /// <summary>
        /// 名前からタイプを取得
		/// ネームスペース考慮していない
        /// </summary>
        /// <param name="className"></param>
        /// <returns></returns>
        public static Type GetTypeByClassName(string className)
        {
            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (Type type in assembly.GetTypes())
                {
                    if (type.Name == className)
                    {
                        return type;
                    }
                }
            }
            return null;
        }
    }
}