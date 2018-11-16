using System;
using System.ComponentModel.Composition.Hosting;
using System.Reflection;
using System.Windows;

namespace ZNC.Utility
{
    public class Utility
    {
       
        public static bool IsInDesignMode
        {
            get
            {
                return Application.Current == null ||
                  Application.Current.GetType() == typeof(Application);
                  
            }
        }
        /// <summary>
        /// currrent wpf application path
        /// </summary>
        public static string ApplicationPath
        {
            get
            {
                string path = System.IO.Path.GetFullPath(typeof(Utility).Assembly.Location);

                return path;
            }
        }
        private static CompositionContainer _directoryContainer;
        /// <summary>
        /// current dirctory container for mef
        /// </summary>
        public static CompositionContainer DirectoryContainer
        {
            get 
            {
                if (_directoryContainer == null)
                {                  
                    DirectoryCatalog catalog = new DirectoryCatalog(ApplicationPath);
                    _directoryContainer = new CompositionContainer(catalog);

                }

                return _directoryContainer;
               
            }
            
        }

        /// <summary>
        /// 根据控件的Name获取控件对象
        /// </summary>
        /// <typeparam name="T">控件类型</typeparam>
        /// <param name="controlName">Name</param>
        /// <returns></returns>
        public T GetControlObject<T>(string controlName)
        {
            try
            {
                Type type = this.GetType();
                FieldInfo fieldInfo = type.GetField(controlName, System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.IgnoreCase);
                if (fieldInfo != null)
                {
                    T obj = (T)fieldInfo.GetValue(this);
                    return obj;
                }
                else
                {
                    return default(T);
                }


            }
            catch (Exception)
            {
                return default(T);
            }
        }
    }
}
