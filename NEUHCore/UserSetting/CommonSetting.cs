using NEUHCore.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using YMvvmBase;

namespace NEUHCore.UserSetting {
    [DataContract]
    public class CommonSetting : YSettingModelBase, ILocalSetting {
        #region Properties
        private static CommonSetting _instence;
        private static readonly object _singleton_Lock = new object();
        public static CommonSetting Instence {
            get {
                if (_instence == null)
                    lock (_singleton_Lock)
                        if (_instence == null)
                            new CommonSetting(true);
                return _instence;
            }
        }

        public string FileName { get; set; }

        public string CachePath { get; set; }

        public string FilePath { get; set; }
        #endregion

        #region defaultproperties
        private string _package;
        [DataMember(Order = 1)]
        public string Package {
            get => _package;
            set => SetValue(out _package, value, Package);
        }
        #endregion

        #region Methods
        public void Reset() {
            
        }

        public void Save() {
            if (!SerializeService.FileCompleteCheck(FilePath + FileName))
                SerializeService.Serialize(_instence, FilePath + FileName, true);
            SerializeService.DictionaryCompleteCheck(CachePath, true);
            SerializeService.Serialize(_instence, CachePath + FileName, true);
        }

        private void Init() {
            FileName = "setting.xml";
            CachePath = App.Cache;
            FilePath = App.Component;
            if (SerializeService.FileCompleteCheck(CachePath + FileName))
                _instence = SerializeService.DeSerialize<CommonSetting>(CachePath + FileName, true);
            else
                _instence = SerializeService.DeSerialize<CommonSetting>(FilePath + FileName, true);
            if (_instence is null)
                _instence = new CommonSetting();
        }
        #endregion

        #region Constructors
        public CommonSetting() {
            FileName = "setting.xml";
            CachePath = App.Cache;
            FilePath = App.Component;
        }

        public CommonSetting(bool init) { Init(); }
        #endregion

    }

}
