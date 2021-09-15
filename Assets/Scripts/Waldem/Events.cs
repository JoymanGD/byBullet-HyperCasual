using System;
using UnityEngine.Events;

namespace Waldem.Unity.Events
{
    [Serializable] public class ClassicEvent : UnityEvent{}
    [Serializable] public class StringEvent : UnityEvent<string>{}
    [Serializable] public class BoolEvent : UnityEvent<bool>{}
    [Serializable] public class IntEvent : UnityEvent<int>{}
    [Serializable] public class FloatEvent : UnityEvent<float>{}
    [Serializable] public class CustomEvent<T1> : UnityEvent<T1>{}
    [Serializable] public class CustomEvent<T1, T2> : UnityEvent<T1, T2>{}
    [Serializable] public class CustomEvent<T1, T2, T3> : UnityEvent<T1, T2, T3>{}
    [Serializable] public class CustomEvent<T1, T2, T3, T4> : UnityEvent<T1, T2, T3, T4>{}
}