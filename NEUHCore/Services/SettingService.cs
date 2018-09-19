using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace NEUHCore.Services {
    public class SettingService {
        #region Properties
        private static SettingService _instence;
        private static readonly object _singleton_Lock = new object();
        public static SettingService Instence {
            get {
                if (_instence == null)
                    lock (_singleton_Lock)
                        if (_instence == null)
                            _instence = new SettingService();
                return _instence;
            }
        }
        #endregion

        #region Methods
        static void Serialize<T>(T instance, string fileName) {
            DataContractSerializer serializer = new DataContractSerializer(typeof(T));
            using (XmlWriter writer = new XmlTextWriter(App.Root + App.Component + fileName + ".xml", Encoding.UTF8)) {
                serializer.WriteObject(writer, instance);
            }
        }

        static T DeSerialize<T>(string fileName) {
            DataContractSerializer serializer = new DataContractSerializer(typeof(T));
            using (FileStream fs = new FileStream(App.Root + App.Component + fileName + ".xml", FileMode.Open, FileAccess.Read)) {
                using (XmlReader reader = new XmlTextReader(fileName, fs)) {
                    return (T)serializer.ReadObject(reader);
                }
            }
        }
        #endregion

        #region Constructors
        #endregion
    }

}
