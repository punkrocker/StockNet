using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Threading.Tasks;
using ServiceStack.OrmLite;
using StockNet.Model;

namespace StockNet.Service
{
    public class SettingService
    {
        public static IDictionary<string, string> GetSettings()
        {
            using (var db = DBService.OpenDb())
            {
                var settings = db.Select<St_setting>();
                var sdict = new Dictionary<string, string>();
                if (settings != null && settings.Count > 0)
                {
                    foreach (var sett in settings)
                    {
                        sdict.Add(sett.Name, sett.Val);
                    }
                }
                return sdict;// db.Dictionary<string, string>("select Name,Val from St_setting");//db.From<St_setting>().Select(st => new { st.Name, st.Val })
            }
        }

        public static Settings GetSettingsObject()
        {
            Settings sts = new Settings();
            Type ststype=sts.GetType();
            using (var db = DBService.OpenDb())
            {
                var settings = db.Select<St_setting>();
                var dicts = new Dictionary<string, string>();
                if (settings != null && settings.Count > 0)
                {
                    foreach (var sett in settings)
                    {
                        dicts.Add(sett.Name, sett.Val);
                    }
                }
                //var dicts = db.Dictionary<string, string>("select Name,Val from St_setting");
                if(dicts!=null&&dicts.Count>0)
                {
                    PropertyInfo property = null;
                    foreach(var dict in dicts)
                    {
                        property = ststype.GetProperty(dict.Key);
                        if(property!=null)
                        {
                            property.SetValue(sts,Convert.ChangeType(dict.Value,property.PropertyType),null);
                        }
                    }
                }
            }
            return sts;
        }

        public static void SaveSettings(IDictionary<string,string> settings)
        {
            if (settings != null && settings.Count > 0)
            {
                using (var db = DBService.OpenDb())
                {
                    var stttes = db.Select<St_setting>();
                    var sts = new Dictionary<string, string>();
                    if (stttes != null && stttes.Count > 0)
                    {
                        foreach (var sett in stttes)
                        {
                            sts.Add(sett.Name, sett.Val);
                        }
                    }
                    //var sts = db.Dictionary<string, string>("select Name,Val from St_setting");
                    foreach (var setting in settings)
                    {
                        if (sts.ContainsKey(setting.Key))
                        {
                            db.Update<St_setting>(new { Val=setting.Value},st=>st.Name==setting.Key);
                        }else
                        {
                            db.Insert<St_setting>(new St_setting { Name=setting.Key,Val=setting.Value});
                        }
                    }
                }
            }
        }

        public static St_setting GetSetting(string name)
        {
            St_setting st = null;
            if (!string.IsNullOrEmpty(name))
            {
                using (var db = DBService.OpenDb())
                {
                    st = db.SingleOrDefault<St_setting>("Name={0}", name);
                }
            }
            return st;
        }

        public static string GetSettingContent(string name)
        {
            string val="";
            if (!string.IsNullOrEmpty(name))
            {
                using (var db = DBService.OpenDb())
                {
                    var st = db.SingleOrDefault<St_setting>("Name={0}", name);
                    if (st != null && !string.IsNullOrEmpty(st.Val))
                        val = st.Val;
                }
            }
            return val;
        }

        public static void SetSettingContent(string name,string value)
        {
            using (var db = DBService.OpenDb())
            {
                var stttes = db.Select<St_setting>();
                var sts = new Dictionary<string, string>();
                if (stttes != null && stttes.Count > 0)
                {
                    foreach (var sett in stttes)
                    {
                        sts.Add(sett.Name, sett.Val);
                    }
                }

                Dictionary<string, string> settings = new Dictionary<string, string>();
                settings.Add(name, value);
                foreach (var setting in settings)
                {
                    if (sts.ContainsKey(setting.Key))
                    {
                        db.Update<St_setting>(new
                        {
                            Val = setting.Value
                        }, st => st.Name == setting.Key);
                    }
                    else
                    {
                        db.Insert<St_setting>(new St_setting
                        {
                            Name = setting.Key,
                            Val = setting.Value
                        });
                    }
                }
            }
        }
    }
}
