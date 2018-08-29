using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IpgwCore.Model.BasicModel {
    /// <summary>
    /// 星期
    /// </summary>
    public enum DAYS { MON, TUE, WED, THU, FRI, SAT, SUN }

    /// <summary>
    /// 学期
    /// </summary>
    public enum TERM { C1, C2, C3, C4, C5, C6 }

    /// <summary>
    /// 课程类
    /// </summary>
    internal class Course {
        #region Properties
        /// <summary>
        /// 课程名称
        /// </summary>
        public string CourseName { get; set; }

        /// <summary>
        /// 任课教师
        /// </summary>
        public string CourseTeacher { get; set; }

        /// <summary>
        /// 上课教室
        /// </summary>
        public string CourseLoc { get; set; }

        /// <summary>
        /// 课程时间
        /// </summary>
        public CourseTime CourseTime { get; set; }

        /// <summary>
        /// 课程安排
        /// </summary>
        public string CourseDur { get; set; }
        #endregion

        #region Methods
        public override string ToString() {
            if (CourseName == null)
                return String.Format("<Name:{0} Teacher:{1} Loc:{2} Week:{3}  | 0 - 0 | >"
                    , "NULL"
                    , "NULL"
                    , "NULL"
                    , "NULL");
            else
                return String.Format("<Name:{0} Teacher:{1} Loc:{2} Week:{3}  |{4}-{5}| >"
                    , CourseName.PadLeft(10)
                    , CourseTeacher.PadLeft(5)
                    , CourseLoc.PadLeft(5)
                    , CourseDur
                    , CourseTime.WeekDay
                    , CourseTime.DayTime);
        }
        #endregion
    }

    /// <summary>
    /// 课程时间
    /// </summary>
    internal class CourseTime {
        #region Methods

        /// <summary>
        /// 第几天
        /// </summary>
        public DAYS WeekDay { get; set; }

        /// <summary>
        /// 第几节
        /// </summary>
        public TERM DayTime { get; set; }
        #endregion

        public override string ToString() {
            return String.Format("{0}-{1}", Convert.ToInt32(WeekDay), Convert.ToInt32(DayTime));
        }

        public static CourseTime FromString(string s) {
            CourseTime temp = new CourseTime();
            string[] ts = s.Split('-');
            temp.WeekDay = (DAYS)Convert.ToInt32(ts[0]);
            temp.DayTime = (TERM)Convert.ToInt32(ts[1]);
            return temp;
        }

        public override bool Equals(object obj) {
            CourseTime time = (CourseTime)obj;
            return time.DayTime.Equals(DayTime) && time.WeekDay.Equals(WeekDay);
        }

        public override int GetHashCode() {
            var hashCode = -735683111;
            hashCode = hashCode * -1521134295 + WeekDay.GetHashCode();
            hashCode = hashCode * -1521134295 + DayTime.GetHashCode();
            return hashCode;
        }
    }

    /// <summary>
    /// 课程表
    /// </summary>
    internal class CourseSet {
        #region Methods

        /// <summary>
        /// 课程学期
        /// </summary>
        public string Term { get; set; }

        /// <summary>
        /// 课程情况
        /// </summary>
        public List<Course> Courses { get; set; }
        #endregion

        //public void RemoveNotNow() {
        //    OverallSettingManger.WeekNowCheck();
        //    try
        //    {
        //        if (Courses.Count > 0)
        //        {
        //            for (int i = 0; i < Courses.Count; i++)
        //            {
        //                if (Courses[i].CourseName != "")
        //                {
        //                    if (Courses[i].CourseDur.Length <= 2)
        //                    {
        //                        if (Convert.ToInt32(Courses[i].CourseDur) != Properties.Settings.Default.WeekNow)
        //                        {
        //                            if (Courses[i].CourseTime.Equals(Courses[i + 1].CourseTime) || (i > 1 && Courses[i].CourseTime.Equals(Courses[i - 1].CourseTime)))
        //                            {
        //                                Courses.RemoveAt(i);
        //                                i--;
        //                            }
        //                            else
        //                            {
        //                                Courses[i] = new Course() { CourseTime = Courses[i].CourseTime };
        //                            }
        //                        }
        //                    }
        //                    else if (Courses[i].CourseDur.Length <= 6)
        //                    {
        //                        string[] during = Courses[i].CourseDur.Split('-');
        //                        if (during.Length > 1)
        //                        {
        //                            int begin = Convert.ToInt32(during[0]);
        //                            int end = Convert.ToInt32(during[1]);
        //                            if (Properties.Settings.Default.WeekNow < begin || Properties.Settings.Default.WeekNow > end)
        //                            {
        //                                if (Courses[i].CourseTime.Equals(Courses[i + 1].CourseTime) || (i > 1 && Courses[i].CourseTime.Equals(Courses[i - 1].CourseTime)))
        //                                {
        //                                    Courses.RemoveAt(i);
        //                                    i--;
        //                                }
        //                                else
        //                                {
        //                                    Courses[i] = new Course() { CourseTime = Courses[i].CourseTime };
        //                                }
        //                            }
        //                        }
        //                    }
        //                    else
        //                    {
        //                        string[] during = Courses[i].CourseDur.Split(new char[] { '-', ':' });
        //                        if (during.Length > 1)
        //                        {
        //                            int begin1 = Convert.ToInt32(during[0]);
        //                            int begin2 = Convert.ToInt32(during[2]);
        //                            int end1 = Convert.ToInt32(during[1]);
        //                            int end2 = Convert.ToInt32(during[3]);
        //                            if ((Properties.Settings.Default.WeekNow < begin1 || Properties.Settings.Default.WeekNow > end1) ||
        //                                (Properties.Settings.Default.WeekNow < begin2 || Properties.Settings.Default.WeekNow > end2))
        //                            {
        //                                if (Courses[i].CourseTime.Equals(Courses[i + 1].CourseTime) || (i > 1 && Courses[i].CourseTime.Equals(Courses[i - 1].CourseTime)))
        //                                {
        //                                    Courses.RemoveAt(i);
        //                                    i--;
        //                                }
        //                                else
        //                                {
        //                                    Courses[i] = new Course() { CourseTime = Courses[i].CourseTime };
        //                                }
        //                            }
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        MessageService.Instence.ShowError(null, "获取课程信息失败，可能原因： 不存在本地课程表且网络未连接");
        //    }
        //}

        public override string ToString() {
            string s = "";
            s += Term + "\n";
            foreach (Course c in Courses)
            {
                s += c.ToString() + "\n";
            }
            return s;
        }
    }
}
