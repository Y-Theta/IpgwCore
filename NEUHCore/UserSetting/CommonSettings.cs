using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using YFrameworkBase;

namespace NEUHCore.UserSetting {
    [DataContract]
    public class CommonSettings :LocalSetting<CommonSettings> {
        #region Properties
        [DataMember(Order = 1)]
        private bool _isArea;
        public bool IsArea {
            get => _isArea;
            set => SetValue(out _isArea, value, IsArea);
        }

        [DataMember(Order = 2)]
        private bool _selfRunning;
        public bool SelfRunning {
            get => _selfRunning;
            set => SetValue(out _selfRunning, value, SelfRunning);
        }

        [DataMember(Order = 3)]
        private List<String> _disabledPlugins;
        public List<String> DisabledPlugins {
            get => _disabledPlugins;
            set => SetValue(out _disabledPlugins, value, DisabledPlugins);
        }

        public override void InitProperties() {
            FileName = "setting.xml";
            CachePath = App.Cache;
            InitDefaultProperties();
            base.InitProperties();
        }

        private void InitDefaultProperties() {
            IsArea = true;
            SelfRunning = true;
            DisabledPlugins = new List<string>();
        }
        #endregion

        #region Constructors
        public CommonSettings() { }
        #endregion
    }

}
