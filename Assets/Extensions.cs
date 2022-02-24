using System.Collections.Generic;

public static class Extensions
{
    public static string Bold(this string str) => "<b>" + str + "</b>";
    public static string Color(this string str, string clr) => string.Format("<color={0}>{1}</color>", clr, str);
    public static string Italic(this string str) => "<i>" + str + "</i>";
    public static string Size(this string str, int size) => string.Format("<size={0}>{1}</size>", size, str);

    public static void AddMany<T>(this List<T> l, params T[] value)
    {
        for (int i = 0; i < value.Length; i++)
        {
            l.Add(value[i]);
        }
    }
}
