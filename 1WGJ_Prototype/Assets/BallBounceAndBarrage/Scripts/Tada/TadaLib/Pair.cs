using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// C++のstd::pair型とほぼ同じ機能を持つ これは嘘ばっかり
/// シリアライズできるのでJsonUtilityでセーブできる できなかった
/// </summary>

namespace TadaLib
{
    [System.Serializable]
    public class Pair<T, U>
    {
        [SerializeField]
        private T f;
        public T first => f;
        [SerializeField]
        private U s;
        public U second => s;

        public Pair(T v1, U v2)
        {
            f = v1;
            s = v2;
        }
    }
}