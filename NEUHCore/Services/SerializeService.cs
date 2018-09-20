using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace NEUHCore.Services {
    public class SerializeService {
        #region Methods
        /// <summary>
        /// 是否在主程序的文件目录下存放序列化文件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance"></param>
        /// <param name="fileName"></param>
        /// <param name="local">是否使用相对路径</param>
        public static void Serialize<T>(T instance, string fileName, bool local) {
            if (local)
                Serialize(instance, App.Root + fileName);
            else
                Serialize(instance, fileName);
        }

        /// <summary>
        /// 是否从主程序的文件目录下读取序列化文件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileName"></param>
        /// <param name="local">是否使用相对路径</param>
        /// <returns></returns>
        public static T DeSerialize<T>(string fileName, bool local) where T : class {
            if (local)
                return DeSerialize<T>(App.Root + fileName);
            else
                return DeSerialize<T>(fileName);
        }

        /// <summary>
        /// 检查路径是否存在，并在必要时创建
        /// </summary>
        public static bool DictionaryCompleteCheck(string path, bool create = false, bool relative = true) {
            string newpath = path;
            if (relative)
                newpath = App.Root + path;
            if (!Directory.Exists(newpath)) {
                if (create)
                    Directory.CreateDirectory(newpath);
                return false;
            }
            else
                return true;
        }

        /// <summary>
        /// 检查文件是否存在，并在必要时创建
        /// </summary>
        public static bool FileCompleteCheck(string path, bool create = false, bool relative = true) {
            string newpath = path;
            if (relative)
                newpath = App.Root + path;
            if (!File.Exists(newpath)) {
                if (create)
                    File.Create(newpath);
                return false;
            }
            else
                return true;
        }

        private static void Serialize<T>(T instance, string fileName) {
            DataContractSerializer serializer = new DataContractSerializer(typeof(T));
            using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite)) {
                using (XmlWriter writer = new XmlTextWriter(fs, Encoding.UTF8)) {
                    serializer.WriteObject(writer, instance);
                }
            }
        }

        private static T DeSerialize<T>(string fileName) where T : class {
            DataContractSerializer serializer = new DataContractSerializer(typeof(T));
            try {
                using (FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)) {
                    using (XmlReader reader = new XmlTextReader(fs)) {
                        return (T)serializer.ReadObject(reader);
                    }
                }
            }
            catch (Exception e) { return null; }
        }
        #endregion
    }
}
