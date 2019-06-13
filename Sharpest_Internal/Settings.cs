using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sharpest_Internal
{
    static class Settings
    {
        public static bool SettingsUpdated = false;
        private static Dictionary<string, object> settingPairs = new Dictionary<string, object>();

        public static void WriteSetting(string name, object value)
        {
            if (!settingPairs.Keys.Contains(name))
                settingPairs.Add(name, value);
            else
                settingPairs[name] = value;

            SettingsUpdated = true;
        }

        public static object ReadSetting(string name)
        {
            if (!settingPairs.Keys.Contains(name))
                return null;
            else
                return settingPairs[name];
        }

        public static void FlushSettingsUpdated()
        {
            SettingsUpdated = false;
        }
        
        public static void InitializeSettings()
        {
            // Legit
            Settings.WriteSetting("legit_enabled", false);
            Settings.WriteSetting("legit_rcs_enabled", false);

            // Anti-aim
            Settings.WriteSetting("fakelag_enabled", false);
        }
    }
}
