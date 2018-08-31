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

        private static XmlDocService _instence;
        private static readonly object _singleton_Lock = new object();
        public static XmlDocService Instence {
            get {
                if (_instence == null)
                    lock (_singleton_Lock)
                        if (_instence == null)
                            _instence = new XmlDocService();
                return _instence;
            }
        }

        #endregion

        #region Methods
        public bool DeleteNode(XmlPath path) {

            return true;
        }

        public T GetNode<T>(XmlPath path) where T : class {
            Type type = typeof(T);
            if (type.Equals(typeof(Flux)))
                return (T)(object)GetLatestFluxInfo();
            else if (type.Equals(typeof(FluxTrendGroup)))
                return (T)(object)GetFluxTrendGroup();
            else if (type.Equals(typeof(CourseSet)))
                return (T)(object)GetCourseSetNode(path.Key);
            else if (type.Equals(typeof(InfoSet)))
                return (T)(object)GetInfWithName(path.Key);
            else return null;
        }

        public bool SetNode<T>(T Item, XmlPath path) {
            Type type = typeof(T);
            if (type.Equals(typeof(Flux)))
                return CreatFluxNode(Item as Flux);
            else if (type.Equals(typeof(CourseSet)))
                return CreatCourseSetNode(Item as CourseSet);

            return true;
        }

        public bool UpdateNode(object Item, XmlPath path) {
            return UpdateWebNodeValue(path.Key, Item as Dictionary<string, string>);
        }

        public bool ResetAll() {
            Dictionary<string, string> dc = new Dictionary<string, string> {
                    { "WebUserNO", "" },
                    { "Password", "" },
                    { "Agnomen", "" }};
            UpdateWebNodeValue("NEUZhjw", dc);
            dc = new Dictionary<string, string> {
                    { "password", "" },
                    { "username", "" }};
            UpdateWebNodeValue("NEUIpgw", dc);
            StreamReader reader = new StreamReader(App.RootPath + App.CourseData, Encoding.UTF8);
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(reader);
            reader.Close();

            XmlElement root = xmlDocument.DocumentElement;
            root.RemoveAll();
            xmlDocument.Save(App.RootPath + App.CourseData);

            reader = new StreamReader(App.RootPath + App.FluxLog, Encoding.UTF8);
            xmlDocument.Load(reader);
            reader.Close();

            root = xmlDocument.DocumentElement;
            root.RemoveAll();
            xmlDocument.Save(App.RootPath + App.FluxLog);

            return true;
        }

        /// <summary>
        /// 更新网络信息数据
        /// </summary>
        private bool UpdateWebNodeValue(string loc, Dictionary<string, string> dc) {
            StreamReader reader = new StreamReader(App.RootPath + App.WebConfig, Encoding.UTF8);
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(reader);
            reader.Close();

            XmlElement root = xmlDocument.DocumentElement;
            XmlNodeList xnl = root.SelectNodes("/Configs/Web");
            XmlNodeList cxnl = null;

            foreach (XmlNode i in xnl)
                if (i.Attributes["name"].Value.Equals(loc))
                {
                    cxnl = i.ChildNodes;
                    break;
                }

            if (cxnl != null)
            {
                foreach (XmlElement i in cxnl)
                    if (dc.ContainsKey(i.Attributes["name"].Value))
                        i.SetAttribute("value", dc[i.Attributes["name"].Value]);
                xmlDocument.Save(App.RootPath + App.WebConfig);
                return true;
            }
            else return false;
        }

        /// <summary>
        /// 创建流量节点
        /// </summary>
        private bool CreatFluxNode(Flux fi) {
            XmlDocument xmlDocument = new XmlDocument();
            using (StreamReader reader = new StreamReader(App.RootPath + App.FluxLog, Encoding.UTF8))
                xmlDocument.Load(reader);
            if (fi is null)
                return false;
            XmlNode Root = xmlDocument.DocumentElement;

            XmlNode LastDate = null;
            LastDate = Root.LastChild;

            if (Root.ChildNodes.Count == 0 || !LastDate.Attributes["Day"].Value.Equals(fi.InfoTime.Day.ToString())
                || !LastDate.Attributes["Mon"].Value.Equals(fi.InfoTime.Month.ToString()))
            {
                XmlElement xml = xmlDocument.CreateElement("Date");
                xml.SetAttribute("Year", DateTime.Now.Year.ToString());
                xml.SetAttribute("Mon", DateTime.Now.Month.ToString());
                xml.SetAttribute("Day", DateTime.Now.Day.ToString());
                Root.AppendChild(xml);
            }

            LastDate = Root.LastChild;
            if (LastDate.HasChildNodes)
                LastDate.RemoveChild(LastDate.FirstChild);
            var Item = xmlDocument.CreateElement("Item");
            Item.SetAttribute("FluxData", fi.FluxData.ToString());
            Item.SetAttribute("Balance", fi.Balance.ToString());
            Item.SetAttribute("InfoTime", fi.InfoTime.ToOADate().ToString());
            LastDate.AppendChild(Item);

            xmlDocument.Save(App.RootPath + App.FluxLog);
            return true;
        }

        /// <summary>
        /// 创建课程表
        /// </summary>
        private bool CreatCourseSetNode(CourseSet set) {
            StreamReader reader = new StreamReader(App.RootPath + App.CourseData, Encoding.UTF8);
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(reader);
            reader.Close();

            XmlElement root = xmlDocument.DocumentElement;
            XmlNodeList xnl = root.SelectNodes("/Courses/CourseSet");
            foreach (XmlNode xn in xnl)
                if (xn.Attributes["Term"].Value.Equals(set.Term))
                {
                    root.RemoveChild(xn); break;
                }

            XmlElement Courses = xmlDocument.CreateElement("CourseSet");
            Courses.SetAttribute("Term", set.Term);
            root.AppendChild(Courses);

            foreach (Course c in set.Courses)
            {
                XmlElement NewCourse = xmlDocument.CreateElement("Course");
                NewCourse.SetAttribute("Name", c.CourseName);
                NewCourse.SetAttribute("Teacher", c.CourseTeacher);
                NewCourse.SetAttribute("Loc", c.CourseLoc);
                NewCourse.SetAttribute("Dur", c.CourseDur);
                NewCourse.SetAttribute("Time", c.CourseTime.ToString());
                Courses.AppendChild(NewCourse);
            }

            xmlDocument.Save(App.RootPath + App.CourseData);
            return true;
        }

        /// <summary>
        /// 获取最新流量数据
        /// </summary>
        private Flux GetLatestFluxInfo() {
            XmlDocument xmlDocument = new XmlDocument();
            using (StreamReader reader = new StreamReader(App.RootPath + App.FluxLog, Encoding.UTF8))
                xmlDocument.Load(reader);

            XmlElement Root = xmlDocument.DocumentElement;
            if (Root.HasChildNodes)
            {
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
            else
                return null;
        }

        /// <summary>
        /// 获取流量趋势
        /// </summary>
        private FluxTrendGroup GetFluxTrendGroup() {
            FluxTrendGroup ftg = new FluxTrendGroup();
            XmlDocument xmlDocument = new XmlDocument();
            using (StreamReader reader = new StreamReader(App.RootPath + App.FluxLog, Encoding.UTF8))
                xmlDocument.Load(reader);

            XmlElement Root = xmlDocument.DocumentElement;


            if (Root.HasChildNodes)
            {
                int k = Root.ChildNodes.Count - 1;
                for (int i = 0; i < 7; i++)
                {
                    if (k > 0 && Root.ChildNodes[k].HasChildNodes)
                    {
                        XmlNode node = Root.ChildNodes[k].FirstChild;
                        Flux fi = new Flux
                        {
                            FluxData = Convert.ToDouble(node.Attributes["FluxData"].Value),
                            Balance = Convert.ToDouble(node.Attributes["Balance"].Value),
                            InfoTime = DateTime.FromOADate(Convert.ToDouble(node.Attributes["InfoTime"].Value))
                        };
                        ftg.FluxInfos[i] = fi;
                        k--;
                    }
                    else if (k > 0)
                        i--;
                    else
                        ftg.FluxInfos[i] = new Flux
                        {
                            FluxData = 0,
                            Balance = 0,
                            InfoTime = new DateTime(1, 1, 1)
                        };
                }
            }
            return ftg;
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

            InfoSet loginInfSet = new InfoSet { name = name };

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
