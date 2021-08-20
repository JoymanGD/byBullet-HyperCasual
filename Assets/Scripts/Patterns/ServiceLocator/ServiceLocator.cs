using UnityEngine;
using System.Collections.Generic;
using System;

public abstract class ServiceLocator<T1,T2> : IDisposable
{
    protected static Dictionary<T1, T2> Services = new Dictionary<T1, T2>();

    public static T2 GetService(T1 _key) => Services[_key];

    public static void AddService(T1 _key, T2 _value) => Services.Add(_key, _value);

    public void Dispose()
    {
        Services.Clear();
    }
}