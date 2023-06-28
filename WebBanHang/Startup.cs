using Microsoft.Owin;
using Owin;
using System.Linq;
using WebBanHang.Models;
using WebBanHang.Models.EF;

[assembly: OwinStartupAttribute(typeof(WebBanHang.Startup))]
namespace WebBanHang
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            SettingSystemDB();
        }

        public Startup()
        {

        }

        private void SettingSystemDB() 
        {
            ApplicationDbContext db = new ApplicationDbContext();
            SystemSetting set = new SystemSetting();

            if(!db.SystemSettings.Any(x => x.SettingKey == "SettingTitle"))
            {
                set.SettingKey = "SettingTitle";
                db.SystemSettings.Add(set);
               
            }

            if (!db.SystemSettings.Any(x => x.SettingKey == "SettingLogo"))
            {
                set = new SystemSetting();
                set.SettingKey = "SettingLogo";
                db.SystemSettings.Add(set);
            }

            if (!db.SystemSettings.Any(x => x.SettingKey == "SettingEmail"))
            {
                set = new SystemSetting();
                set.SettingKey = "SettingEmail";
                db.SystemSettings.Add(set);
            }

            if (!db.SystemSettings.Any(x => x.SettingKey == "SettingHotline"))
            {
                set = new SystemSetting();
                set.SettingKey = "SettingHotline";
                db.SystemSettings.Add(set);
            }

            db.SaveChanges();
        }
    }
}
