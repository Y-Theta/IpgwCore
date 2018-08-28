using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IpgwCore.MVVMBase {
    public class ViewModelBase : INotifyPropertyChanged {
        #region Properties
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Methods
        /// <summary>
        /// 带事件通知回调属性设置方法
        /// 只有在用于Property的set方法时才能触发PropertyChanged
        /// </summary>
        /// <typeparam name="T">属性类型</typeparam>
        /// <param name="nProperty">新属性：一般指私有变量</param>
        /// <param name="value">传入新属性</param>
        /// <param name="oProperty">旧熟悉：一般指共有变量</param>
        /// <param name="callBack">回调若属性的更改需要得到控制</param>
        public void SetValue<T>(out T nProperty, T value, T oProperty, PropertyChangedCallBack callBack = null) {
            bool accept = true;
            accept = callBack is null ? true : callBack.Invoke(value, oProperty);
            if (accept)
                nProperty = value;
            else
                nProperty = oProperty;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(GetParaString()));
        }

        private static string GetParaString() {
            var stackTrace = new StackTrace();
            var stackFrame = stackTrace.GetFrame(2);

            var methodBase = stackFrame.GetMethod();
            return methodBase.Name.Split('_')[1];
        }
        #endregion

        #region Constructors
        #endregion
    }

}
