using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YMvvmBase;

namespace NEUHCore.UserSetting {

    public interface ILocalSetting {
        string FileName { get; set; }

        string CachePath { get; set; }

        string FilePath { get; set; }

        void Save();

        void Reset();
    }
}
