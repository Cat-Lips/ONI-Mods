using System;
using System.Reflection;

namespace MyMods;

public static class Access
{
    #region Private

    private static readonly BindingFlags StaticMembers = BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
    private static readonly BindingFlags InstanceMembers = BindingFlags.Instance | StaticMembers;

    #endregion

    #region Fields

    public static T Field<T>(Type type, string name)
        => (T)type.GetField(name, StaticMembers).GetValue(null);

    public static void Field<T>(Type type, string name, T value)
        => type.GetField(name, StaticMembers).SetValue(null, value);

    public static T Field<T>(this object source, string name)
        => (T)source.GetType().GetField(name, InstanceMembers).GetValue(source);

    public static void Field<T>(this object source, string name, T value)
        => source.GetType().GetField(name, InstanceMembers).SetValue(source, value);

    internal static object TryGetValue(this FieldInfo source, object obj)
    {
        try { return source.GetValue(obj); }
        catch { return null; }
    }

    #endregion

    #region Properties

    public static T Property<T>(Type type, string name)
        => (T)type.GetProperty(name, StaticMembers).GetValue(null);

    public static void Property<T>(Type type, string name, T value)
        => type.GetProperty(name, StaticMembers).SetValue(null, value);

    public static T Property<T>(this object source, string name)
        => (T)source.GetType().GetProperty(name, InstanceMembers).GetValue(source);

    public static void Property<T>(this object source, string name, T value)
        => source.GetType().GetProperty(name, InstanceMembers).SetValue(source, value);

    internal static object TryGetValue(this PropertyInfo source, object obj)
    {
        try { return source.GetValue(obj); }
        catch { return null; }
    }

    #endregion

    #region Methods

    public static void Invoke(Type type, string method, params object[] args)
        => type.GetMethod(method, StaticMembers).Invoke(null, args);

    public static T Invoke<T>(Type type, string method, params object[] args)
        => (T)type.GetMethod(method, StaticMembers).Invoke(null, args);

    public static void Invoke(this object source, string method, params object[] args)
        => source.GetType().GetMethod(method, InstanceMembers).Invoke(source, args);

    public static T Invoke<T>(this object source, string method, params object[] args)
        => (T)source.GetType().GetMethod(method, InstanceMembers).Invoke(source, args);

    #endregion

    #region Events

    public static void Raise(this object source, string name, params object[] args)
        => source.Field<Delegate>(name).DynamicInvoke(args);

    #endregion
}
