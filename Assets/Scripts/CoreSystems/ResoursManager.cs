using ET.Interface;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ET.Core
{
    public class ResoursesManager : IResoursManager
    {
        T IResoursManager.CreateObjectInstance<T, E>(E item)
        {
            var prefab = GameObject.Instantiate(
                Resources.Load<GameObject>(
                    string.Format("{0}/{1}", typeof(E).Name, item.ToString())));

            return prefab.GetComponent<T>();
        }

        GameObject IResoursManager.CreatePrefabInstance<E>(E item)
        {
            var path = string.Format("{0}/{1}", typeof(E).Name, item.ToString());
            var assets = Resources.Load<GameObject>(path);
            var result = GameObject.Instantiate(assets);

            return result;
        }
    }
}
