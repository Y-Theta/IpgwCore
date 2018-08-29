using System;
using IpgwCore.Model.BasicModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;

namespace IpgwCore.Services.FormatServices {

    /// <summary>
    /// xml文件服务
    /// </summary>
    internal class XmlDocService : IXmlOperation {
        #region Properties
        #endregion

        #region Methods
        public bool DeleteNode<T>(T Item, XmlPath path) {
            Item.GetType();
            
            return true;
        }

        public object GetNode<T> (XmlPath path) { 
            Type type = typeof(T);
            if (type.Equals(typeof(Flux)))
                return GetLatestFluxInfo();
            else if (type.Equals(typeof(CourseSet)))
                return GetCourseSetNode(path.Key);
            else if (type.Equals(typeof(InfoSet)))
                return GetInfWithName(path.Key);
            else return default(T);
        }

        public bool SetNode<T>(T Item, XmlPath path) {
            Item.GetType();

            return true;
        }

        public bool UpdateNode<T>(T Item, XmlPath path) {
            Item.GetType();

            return true;
        }

        /// <summary>
        /// 获取最新流量数据
        /// </summary>
        /// <returns></returns>
        private Flux GetLatestFluxInfo() {
            XmlDocument xmlDocument = new XmlDocument();
            using (StreamReader reader = new StreamReader(App.RootPath + App.FluxLog, Encoding.UTF8))
                xmlDocument.Load(reader);
            

            XmlElement Root = xmlDocument.DocumentElement;
            XmlNode lasest = Root.LastChild.LastChild;
            if (lasest is null)
                return null;
            else
            {
                Flux fi = new Flux()
                {
                    FluxData = Convert.ToDouble(lasest.Attributes["FluxData"].Value),
                    Balance = Convert.ToDouble(lasest.Attributes["Balance"].Value),
                    InfoTime = DateTime.FromOADate(Convert.ToDouble(lasest.Attributes["InfoTime"].Value))
                };
                return fi;
            }
        }

        /// <summary>
        /// 通过名称获取网站的信息
        /// </summary>
        private InfoSet GetInfWithName(String name) {
            StreamReader reader = new StreamReader(App.RootPath + App.WebConfig, Encoding.UTF8);
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(reader);
            reader.Close();

            XmlElement root = xmlDocument.DocumentElement;
            XmlNodeList xnl = root.SelectNodes("/Configs/Web");
            XmlNodeList cxnl = null;

            InfoSet loginInfSet = new InfoSet { name = name  };

            foreach (XmlNode i in xnl)
                if (i.Attributes["name"].Value.Equals(name))
                {
                    cxnl = i.ChildNodes;
                    loginInfSet.NeedLogin = i.Attributes["login"].Value.Equals("true");
                    loginInfSet.Compressed = i.Attributes["gzip"].Value.Equals("true");
                    loginInfSet.Verify = i.Attributes["needverifycode"].Value.Equals("true");
                    loginInfSet.CharSet = i.Attributes["charset"].Value;
                    break;
                }

            if (cxnl != null)
            {
                foreach (XmlNode i in cxnl)
                {
                    switch (i.Attributes["type"].Value)
                    {
                        case "uri":
                            loginInfSet.Uris.Add(i.Attributes["uri"].Value);
                            break;
                        case "action":
                            loginInfSet.KeyValuePairs.Add(new KeyValuePair<string, string>
                                (i.Attributes["name"].Value, i.Attributes["value"].Value));
                            break;
                        case "verify":
                            loginInfSet.VerifyCode = i.Attributes["value"].Value;
                            break;
                        case "idcode":
                            loginInfSet.IdCodes = new KeyValuePair<string, string>
                                (i.Attributes["name"].Value, i.Attributes["value"].Value);
                            break;
                        case "cookie":
                            loginInfSet.Cookies.Add(new KeyValuePair<string, string>
                                (i.Attributes["name"].Value, i.Attributes["value"].Value));
                            break;
                    }
                }
                return loginInfSet;
            }
            else return null;
        }

        /// <summary>
        /// 通过学期获取课表
        /// </summary>
        private CourseSet GetCourseSetNode(String term) {
            StreamReader reader = new StreamReader(App.RootPath + App.CourseData, Encoding.UTF8);
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(reader);
            reader.Close();

            XmlElement root = xmlDocument.DocumentElement;
            XmlNodeList xnl = root.SelectNodes("/Courses/CourseSet");
            XmlNodeList xnls = null;
            if (xnl.Count == 0)
                return null;

            CourseSet courseSet = new CourseSet();

            foreach (XmlNode xn in xnl)
                if (xn.Attributes["Term"].Value.Equals(term))
                {
                    xnls = xn.ChildNodes; break;
                }

            if (xnls != null && xnls.Count > 0)
            {
                courseSet.Term = term;
                courseSet.Courses = new List<Course>();
                foreach (XmlNode xn in xnls)
                {
                    Course temp = new Course();
                    temp.CourseName = xn.Attributes["Name"].Value;
                    temp.CourseTeacher = xn.Attributes["Teacher"].Value;
                    temp.CourseLoc = xn.Attributes["Loc"].Value;
                    temp.CourseDur = xn.Attributes["Dur"].Value;
                    temp.CourseTime = CourseTime.FromString(xn.Attributes["Time"].Value);

                    courseSet.Courses.Add(temp);
                }
            }

            return courseSet;
        }
        #endregion


        #region Constructors

        #endregion
    }

}
