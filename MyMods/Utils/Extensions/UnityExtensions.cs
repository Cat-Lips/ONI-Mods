using System.Collections;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace MyMods;

public static class UnityExtensions
{
    public static GameObject Get(this GameObject source, string path) => source.transform.Find(path)?.gameObject;
    public static GameObject Get(this Component source, string path) => source.transform.Find(path)?.gameObject;

    public static GameObject GameObject(this GameObject source) => source.gameObject;
    public static GameObject GameObject(this Component source) => source.gameObject;
    public static GameObject GameObject(this Transform source) => source.gameObject;

    public static GameObject Parent(this GameObject source) => source.transform.Parent();
    public static GameObject Parent(this Component source) => source.transform.Parent();
    public static GameObject Parent(this Transform source) => source.parent.gameObject;

    public static string Name(this GameObject source) => source.gameObject.name;
    public static string Name(this Component source) => source.gameObject.name;
    public static string Name(this Transform source) => source.gameObject.name;

    public static string Path(this GameObject source) => source.transform.Path();
    public static string Path(this Component source) => source.transform.Path();
    public static string Path(this Transform source)
    {
        return source.parent is null ? source.name
            : $"{source.parent.Path()}/{source.name}";
    }

    public static string Path(this GameObject source, int rcount) => source.transform.Path(rcount);
    public static string Path(this Component source, int rcount) => source.transform.Path(rcount);
    public static string Path(this Transform source, int rcount)
    {
        Debug.Assert(rcount > 0);
        return rcount is 1 || source.parent is null ? source.name
            : source.parent.Path(rcount - 1) + "/" + source.name;
    }

    public static void PrintTree(this GameObject current, bool detail = false) => current.transform.PrintTree(detail);
    public static void PrintTree(this Component current, bool detail = false) => current.transform.PrintTree(detail);
    public static void PrintTree(this Transform current, bool detail = false, string path = null)
    {
        if (current.name.StartsWith("TMP ")) return;

        Debug.Log(path = path is null ? current.name : $"{path}/{current.name}");

        if (detail) PrintDetail();
        foreach (Transform child in current)
            child.PrintTree(detail, path);

        void PrintDetail()
        {
            var flags = BindingFlags.Instance | BindingFlags.Public;

            foreach (var obj in current.GetComponents<Component>())
            {
                var type = obj.GetType();
                var fields = type.GetFields(flags).Select(FieldPrint);
                var properties = type.GetProperties(flags).Where(x => !PropertyIgnore(x)).Select(PropertyPrint);

                Debug.Log($" - {type.Name} [{string.Join(", ", properties.Concat(fields))}]");

                bool PropertyIgnore(PropertyInfo x)
                {
                    return
                        !x.CanRead ||
                        x.Name is "name" ||
                        x.PropertyType == typeof(Matrix4x4) ||
                        x.PropertyType == typeof(GameObject) ||
                        x.GetIndexParameters().Count() is > 0;
                }

                string PropertyPrint(PropertyInfo x)
                    => $"{x.Name}: {x.TryGetValue(obj)}";

                string FieldPrint(FieldInfo x)
                    => $"{x.Name}: {x.TryGetValue(obj)}";
            }
        }
    }

    public static void CallDeferred(this MonoBehaviour source, System.Action action)
    {
        source.StartCoroutine(ExecuteAction());

        IEnumerator ExecuteAction()
        {
            yield return null;
            action();
        }
    }
}
