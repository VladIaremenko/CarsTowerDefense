﻿using Assets.Scripts.TargetLogic;
using System.Collections;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.TowerLogic
{
    public class TowerView : MonoBehaviour
    {
        [SerializeField] protected Transform _shootPointOrigin;

        public Transform ShootPointOrigin => _shootPointOrigin;
    }
}