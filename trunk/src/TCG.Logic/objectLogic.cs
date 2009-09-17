/*
 * Copyright (C) 2009-2009 Q2Mx <http://cms.q4mx.com/>
 * 
 *    本代码以公共的方式开发下载，任何个人和组织可以下载，
 * 修改，进行第二次开发使用，但请保留作者版权信息。
 * 
 *    任何个人或组织在使用本软件过程中造成的直接或间接损失，
 * 需要自行承担后果与本软件开发者(勤奋猪)无关。
 * 
 *    本软件解决中小型商家产品网络化销售方案。
 *    
 *    使用中的问题，咨询作者QQ邮箱 sanyungui@vip.qq.com
 */

using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using TCG.Utils;

namespace TCG.Logic
{

    public class TypeWorker
    {
        /// <summary>
        /// 根据类型获得类实例
        /// </summary>
        /// <param name="objtype">类型</param>
        /// <returns>实例化后的对象</returns>
        public static Object Instantiated(Type objtype)
        {
            Assembly asm = Assembly.Load(objtype.Namespace);
            return asm.CreateInstance(objtype.ToString(), true);
        }

        /// <summary>
        /// 获取当前对象类型内所有的公开属性
        /// </summary>
        /// <param name="obj">对象类</param>
        /// <returns></returns>
        public static PropertyInfo[] PropertyInfos(Object obj)
        {
            return obj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
        }
    }

    /// <summary>
    /// 通用对象处理方法
    /// </summary>
    public class objectLogic : BaseLogic
    {

        /// <summary>
        /// 根据对象类型和记录ID获得对象实例
        /// </summary>
        /// <param name="servicetype">业务类型</param>
        /// <param name="objtype">需要生成的类型</param>
        /// <param name="objid">实体的编号</param>
        /// <returns>返回实体实例化对象</returns>
        public Object Load(Type servicetype,Type objtype,int objid)
        {
            //实例化对象
            Object obj = TypeWorker.Instantiated(objtype);
            if (obj != null)
            {
                //获得当前属性
                PropertyInfo[] PropertyInfos = TypeWorker.PropertyInfos(obj);
                
                //获得查询语句
                string SQL = "SELECT ";
                for (int i = 0; i < PropertyInfos.Length; i++)
                {
                    //最后一位不输出,号
                    string text1 = (i != (PropertyInfos.Length - 1)) ? "," : "";
                    SQL += PropertyInfos[i].Name + text1;
                }
                SQL += " FROM " + objtype.Name + " (NOLOCK) WHERE ID =" + objid.ToString();

                //初始化数据访问
                if (base.Clear() < 0) return null;
                //获取数据
                base._cacheDs = base.conn.GetDataSet(SQL);
                if (base._cacheDs == null) return null;

                if (base._cacheDs.Tables[0].Rows.Count > 0)
                {
                    foreach (PropertyInfo piTemp in PropertyInfos)
                    {
                        //获取当前属性的类型
                        Type tTemp = piTemp.PropertyType;
                        string strValue = base._cacheDs.Tables[0].Rows[0][piTemp.Name].ToString();

                        //判断当前属性类型是否为系统类型，如为系统类型则为System起始，否则为用户自定义class
                        if (tTemp.ToString().IndexOf("System.DateTime") == 0)
                        {
                            //如果是日期类型
                            piTemp.SetValue(obj, DateTime.Parse(strValue), null);
                        }
                        else if (tTemp.ToString().IndexOf("System.Int64") == 0)
                        {
                            //如果是int类型
                            piTemp.SetValue(obj, Int64.Parse(strValue), null);
                        }
                        else if (tTemp.ToString().IndexOf("System.Int") == 0)
                        {
                            //如果是int类型
                            piTemp.SetValue(obj, int.Parse(strValue), null);
                        }
                        else if (tTemp.ToString().IndexOf("System.Single") == 0)
                        {
                            //如果是float类型
                            piTemp.SetValue(obj, float.Parse(strValue), null);
                        }
                        else if (tTemp.ToString().IndexOf("System.Decimal") == 0)
                        {
                            //如果是Decimal类型
                            piTemp.SetValue(obj, Decimal.Parse(strValue), null);
                        }
                        else
                        {
                            //如果是string类型
                            piTemp.SetValue(obj, strValue, null);
                        }
                    }
                }
                else
                {
                    return null;
                }

            }
            return obj;
        }


        public Object Load(Type servicetype, Type objtype, string Conditions, object[] objs)
        {
            //实例化对象
            Object obj = TypeWorker.Instantiated(objtype);
            if (obj != null)
            {
                //获得当前属性
                PropertyInfo[] PropertyInfos = TypeWorker.PropertyInfos(obj);

                //获得查询语句
                string SQL = "SELECT ";
                for (int i = 0; i < PropertyInfos.Length; i++)
                {
                    //最后一位不输出,号
                    string text1 = (i != (PropertyInfos.Length - 1)) ? "," : "";
                    SQL += PropertyInfos[i].Name + text1;
                }
                SQL += " FROM " + objtype.Name + " (NOLOCK) WHERE " + string.Format(Conditions,objs);

                //初始化数据访问
                if (base.Clear() < 0) return null;
                //获取数据
                base._cacheDs = base.conn.GetDataSet(SQL);
                if (base._cacheDs == null) return null;

                if (base._cacheDs.Tables[0].Rows.Count > 0)
                {
                    foreach (PropertyInfo piTemp in PropertyInfos)
                    {
                        //获取当前属性的类型
                        Type tTemp = piTemp.PropertyType;
                        string strValue = base._cacheDs.Tables[0].Rows[0][piTemp.Name].ToString();

                        //判断当前属性类型是否为系统类型，如为系统类型则为System起始，否则为用户自定义class
                        if (tTemp.ToString().IndexOf("System.DateTime") == 0)
                        {
                            //如果是日期类型
                            piTemp.SetValue(obj, DateTime.Parse(strValue), null);
                        }
                        else if (tTemp.ToString().IndexOf("System.Int64") == 0)
                        {
                            //如果是int类型
                            piTemp.SetValue(obj, Int64.Parse(strValue), null);
                        }
                        else if (tTemp.ToString().IndexOf("System.Int") == 0)
                        {
                            //如果是int类型
                            piTemp.SetValue(obj, int.Parse(strValue), null);
                        }
                        else if (tTemp.ToString().IndexOf("System.Single") == 0)
                        {
                            //如果是float类型
                            piTemp.SetValue(obj, float.Parse(strValue), null);
                        }
                        else if (tTemp.ToString().IndexOf("System.Decimal") == 0)
                        {
                            //如果是Decimal类型
                            piTemp.SetValue(obj, Decimal.Parse(strValue), null);
                        }
                        else
                        {
                            //如果是string类型
                            piTemp.SetValue(obj, strValue, null);
                        }
                    }
                }
                else
                {
                    return null;
                }

            }
            return obj;
        }

        /// <summary>
        /// 插入数据库
        /// </summary>
        /// <param name="servicetype"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Insert(Type servicetype, Object entity)
        {
            if (entity == null) return false;
            //获得当前属性
            PropertyInfo[] PropertyInfos = TypeWorker.PropertyInfos(entity);

            //获得查询语句

            string SQL = "INSERT INTO " + entity.GetType().Name + " (";
            for (int i = 0; i < PropertyInfos.Length; i++)
            {
                if(PropertyInfos[i].Name.ToLower() =="id") continue;
                //最后一位不输出,号
                string text1 = (i != (PropertyInfos.Length - 1)) ? "," : "";
                SQL += PropertyInfos[i].Name + text1;
            }

            SQL += ") VALUES(";
           
            for (int i = 0; i < PropertyInfos.Length; i++)
            {
                if(PropertyInfos[i].Name.ToLower() =="id") continue;
                //最后一位不输出,号
                string text2 = (i != (PropertyInfos.Length - 1)) ? "," : "";
                //获取当前属性的类型

                Type tTemp = PropertyInfos[i].PropertyType;
               

                //判断当前属性类型是否为系统类型，如为系统类型则为System起始，否则为用户自定义class
                if (tTemp.ToString().IndexOf("System.DateTime") == 0)
                {
                    //如果是日期类型
                    SQL += "'" + objectHandlers.ToString(PropertyInfos[i].GetValue(entity, null)) + "'" + text2;
                }
                else if (tTemp.ToString().IndexOf("System.Int64") == 0)
                {
                    //如果是int类型
                    SQL +=  objectHandlers.ToString(PropertyInfos[i].GetValue(entity, null)) + text2;
                }
                else if (tTemp.ToString().IndexOf("System.Int") == 0)
                {
                    //如果是int类型
                    SQL +=  objectHandlers.ToString(PropertyInfos[i].GetValue(entity, null)) + text2;
                }
                else if (tTemp.ToString().IndexOf("System.Single") == 0)
                {
                    //如果是float类型
                    SQL +=  objectHandlers.ToString(PropertyInfos[i].GetValue(entity, null)) + text2;
                }
                else if (tTemp.ToString().IndexOf("System.Decimal") == 0)
                {
                    //如果是Decimal类型
                    SQL +=  objectHandlers.ToString(PropertyInfos[i].GetValue(entity, null)) + text2;
                }
                else
                {
                    //如果是string类型
                    SQL += "'" +  objectHandlers.ToString(PropertyInfos[i].GetValue(entity, null)) + "'" + text2;
                }
            }

            SQL += ")";

            //初始化数据访问
            if (base.Clear() < 0) return false;
            //获取数据
            base._revalue = base.conn.Execute(SQL);
            return true;
        }
    }
}