using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ET.Interface
{
    public interface IResoursManager
    {
        T CreateObjectInstance<T, E>(E item) where E : Enum;
        GameObject CreatePrefabInstance<E>(E item) where E : Enum;
    }
}

