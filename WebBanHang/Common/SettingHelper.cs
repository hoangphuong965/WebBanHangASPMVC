using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebBanHang.Models;

namespace WebBanHang.Common
{
    public class SettingHelper
    {
        private static ApplicationDbContext db = new ApplicationDbContext();

        public static string GetValue(string key)
        {
            var items = db.SystemSettings.Where(x => x.SettingKey == key);
            if (items != null)
            {
                return items.FirstOrDefault(x => x.SettingKey == key).SettingValue;
            }
            return "";
        }
    }
}