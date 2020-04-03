using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace eTrace.Core
{
    public enum EmFormatType
    {
        Xml = 1,
        Json = 2,
    }

    public class SerializationHelper
    {
        /// <summary>
        /// 将C#数据实体转化为xml数据
        /// </summary>
        /// <param name="obj">要转化的数据实体</param>
        /// <returns>xml格式字符串</returns>
        public static string XmlSerialize<T>(T obj)
        {
            return XmlSerialize<T>(obj, Encoding.GetEncoding("GB2312"));
        }
        /// <summary>
        /// 将C#数据实体转化为xml数据
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="obj">要转化的数据实体</param>
        /// <param name="encoding">编码</param>
        /// <returns></returns>
        public static string XmlSerialize<T>(T obj, Encoding encoding)
        {
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "");
            return XmlSerialize<T>(obj, encoding, ns);
        }
        /// <summary>
        /// 将C#数据实体转化为xml数据
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="obj">要转化的数据实体</param>
        /// <param name="encoding">编码</param>
        /// <param name="ns">命名空间</param>
        /// <returns></returns>
        public static string XmlSerialize<T>(T obj, Encoding encoding, XmlSerializerNamespaces ns)
        {
            using (MemoryStream output = new MemoryStream())
            {
                XmlTextWriter w = new XmlTextWriter(output, encoding);
                w.Formatting = System.Xml.Formatting.Indented;
                XmlSerializer xs = new XmlSerializer(typeof(T), string.Empty);
                xs.Serialize(w, obj, ns);
                output.Position = 0;
                StreamReader r = new StreamReader(output, encoding);
                return r.ReadToEnd();
            }
        }


        private static T XmlDeserialize<T>(string s)
        {
            XmlDocument xdoc = new XmlDocument();
            try
            {
                xdoc.LoadXml(s);
                XmlNodeReader reader = new XmlNodeReader(xdoc.DocumentElement);
                XmlSerializer ser = new XmlSerializer(typeof(T));
                object obj = ser.Deserialize(reader);
                return (T)obj;
            }
            catch
            {
                return default(T);
            }
        }


        /// <summary>
        /// 将xml数据转化为C#数据实体
        /// </summary>
        /// <param name="xml">符合xml格式的字符串</param>
        /// <returns>T类型的对象</returns>
        public static T XmlDeserialize2<T>(string xml)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            XmlTextReader x = new XmlTextReader(new StringReader(xml));
            T obj = (T)serializer.Deserialize(x);
            return obj;
        }


        private static string JsonSerialize(object o)
        {
            //using (var ms = new MemoryStream())
            //{
            //    new DataContractJsonSerializer(o.GetType()).WriteObject(ms, o);
            //    return Encoding.UTF8.GetString(ms.ToArray());
            //}
            return JsonConvert.SerializeObject(o);
        }

        private static T JsonDeserialize<T>(string s)
        {
            //using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(s)))
            //{
            //    return (T)new DataContractJsonSerializer(typeof(T)).ReadObject(ms);
            //}
            return JsonConvert.DeserializeObject<T>(s);
        }

        /// <summary>
        /// 将对象根据格式（XML/JSON）序列化成字符串结果
        /// </summary>
        /// <param name="o">目标对象</param>
        /// <param name="format">输出格式</param>
        /// <returns></returns>
        public static string Serialize<T>(T o, EmFormatType format)
        {
            if (format == EmFormatType.Xml)
            {
                return XmlSerialize<T>(o);
            }
            else
            {
                return JsonSerialize(o);
            }
        }

        /// <summary>
        /// 将字符串根据格式（XML/JSON）反序列化成指定类型的对象
        /// </summary>
        /// <typeparam name="T">指定类型</typeparam>
        /// <param name="s">目标字符串</param>
        /// <param name="format">输入格式</param>
        /// <returns></returns>
        public static T Deserialize<T>(string s, EmFormatType format)
        {
            if (format == EmFormatType.Xml)
            {
                return SerializationHelper.XmlDeserialize2<T>(s);
            }
            else
            {
                return SerializationHelper.JsonDeserialize<T>(s);
            }
        }
    }
}
