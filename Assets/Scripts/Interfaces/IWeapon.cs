using ET.Weapons;
using System;
using UnityEngine;

namespace ET.Interface
{
    public interface IWeapon
    {
        void Shoot(int num);
        void ReloadingWeapon();
    }
}
