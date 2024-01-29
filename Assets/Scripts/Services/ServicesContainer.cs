using System;
using System.Collections.Generic;

public class ServicesContainer
{
    private readonly Dictionary<string, IService> _services = new Dictionary<string, IService>();
    
    public static ServicesContainer Instance { get; private set; }

    public static void Init()
    {
        Instance = new ServicesContainer();
    }
    
    public T Get<T>() where T : IService
    {
        string key = typeof(T).Name;
        if (!_services.ContainsKey(key))
        {
            throw new InvalidOperationException();
        }

        return (T)_services[key];
    }

    public void Register<T>(T service) where T : IService
    {
        string key = typeof(T).Name;
        if (_services.ContainsKey(key))
        {
            Unregister<T>();
        }

        _services.Add(key, service);
    }

    public void Unregister<T>() where T : IService
    {
        string key = typeof(T).Name;
        if (!_services.ContainsKey(key))
        {
            return;
        }

        _services.Remove(key);
    }
}
