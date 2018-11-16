using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Windows;

namespace ZNC.Component.Helper
{
    /// <summary>
    /// 扩展方法类.
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// 获取某一参数集合中指定键对应的值, 如果集合中不存在该键, 则返回数据类型默认值.
        /// </summary>
        /// <typeparam name="T">参数集合中值的类型.</typeparam>
        /// <param name="parameters">参数集合</param>
        /// <param name="paramKey">键</param>
        /// <returns>值</returns>
        public static T GetValue<T>(this Dictionary<string, object> parameters, string paramKey)
        {
            return GetValue(parameters, paramKey, default(T));
        }

        /// <summary>
        /// 获取某一参数集合中指定键对应的值, 如果集合中不存在该键, 则返回给定的默认值.
        /// </summary>
        /// <typeparam name="T">参数集合中值的类型.</typeparam>
        /// <param name="parameters">参数集合</param>
        /// <param name="paramKey">键</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>值或默认值</returns>
        public static T GetValue<T>(this Dictionary<string, object> parameters, string paramKey, T defaultValue)
        {
            if (parameters == null || parameters.Count == 0 || !parameters.ContainsKey(paramKey))
            {
                return defaultValue;
            }
            return (T)parameters[paramKey];
        }

        /// <summary>
        /// 将IEnumerable转换为ObservableCollection.
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="sender">源集合</param>
        /// <returns>目标集合</returns>
        public static ObservableCollection<T> ConvertTo<T>(this IEnumerable<T> sender, bool isConvertAll = false)
        {
            if (sender == null) return null; ObservableCollection<T> collection = new ObservableCollection<T>();
            IEnumerator<T> ie = sender.GetEnumerator();
            if (isConvertAll)
            {
                while (ie.MoveNext())
                {
                    collection.Add(ie.Current.Clone());
                }
            }
            else
            {
                while (ie.MoveNext())
                {
                    collection.Add(ie.Current);
                }
            }
            return collection;
        }

        /// <summary>
        /// 将指定对象的所有公用属性复制到新对象中.
        /// </summary>
        /// <typeparam name="T">源数据类型</typeparam>
        /// <param name="source">源对象</param>
        /// <returns></returns>
        public static T Clone<T>(this T source)
        {
            if (typeof(T).IsValueType || source is string) return source;

            if (source == null) return default(T);

            Type sourceType = source.GetType();
            T result;
            if (typeof(T).Equals(typeof(object)))
            {
                result = (T)Activator.CreateInstance(sourceType);
            }
            else
            {
                result = Activator.CreateInstance<T>();
            }
            foreach (PropertyInfo property in sourceType.GetProperties(BindingFlags.Instance | BindingFlags.Public))
            {
                if (!property.CanWrite) continue;

                object sourceValue = property.GetValue(source, null), destinationValue;
                if (sourceValue == null) continue;

                if (property.PropertyType.IsValueType || property.PropertyType.Equals(typeof(string)))
                {
                    property.SetValue(result, sourceValue, null);
                }
                else if (typeof(IEnumerable).IsAssignableFrom(property.PropertyType))
                {
                    destinationValue = Activator.CreateInstance(property.PropertyType);
                    IList list = destinationValue as IList;
                    foreach (var item in sourceValue as IEnumerable)
                    {
                        list.Add(item.Clone());
                    }
                    property.SetValue(result, list, null);
                }
                else
                {
                    destinationValue = sourceValue.Clone();
                    property.SetValue(result, destinationValue, null);
                }
            }
            return result;
        }

        /// <summary>
        /// 获取依赖属性的静态只读字段信息.
        /// </summary>
        /// <param name="type">所属类</param>
        /// <param name="name">依赖属性名称</param>
        /// <returns>依赖属性的字段信息, 不存在则返回<c>null</c>.</returns>
        public static FieldInfo GetDependencyPropertyField(this Type type, string name)
        {
            FieldInfo fi = type.GetField(name, BindingFlags.DeclaredOnly | BindingFlags.Static | BindingFlags.Public);
            if (fi != null && fi.FieldType == typeof(DependencyProperty)) return fi;

            if (type.BaseType != null)
            {
                return type.BaseType.GetDependencyPropertyField(name);
            }

            return null;
        }

        #region 用户信息判断

        ///// <summary>
        ///// 判断当前用户是否管理员.
        ///// </summary>
        ///// <param name="currentUser">当前用户</param>
        ///// <returns>是/否</returns>
        //internal static bool IsAdminRole(this User currentUser)
        //{
        //    return currentUser != null && (currentUser.Irole == 1 || currentUser.Irole == 2);
        //}

        ///// <summary>
        ///// 判断当前用户是否显示局段信息.
        ///// </summary>
        ///// <param name="currentUser">当前用户</param>
        ///// <returns>是/否</returns>
        //internal static bool IsShowOfficeNode(this User currentUser)
        //{
        //    return currentUser != null && (currentUser.Irole == 99 || currentUser.Irole == 1);
        //}

        ///// <summary>
        ///// 判断当前用户是否为全局用户.
        ///// </summary>
        ///// <param name="currentUser">当前用户</param>
        ///// <returns>是/否</returns>
        //internal static bool IsGlobalUser(this User currentUser)
        //{
        //    return currentUser != null && currentUser.Itype == 99;
        //}

        #endregion
    }
}
