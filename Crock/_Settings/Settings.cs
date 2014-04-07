using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Drawing;

namespace Crock._Settings
{
    [CompilerGeneratedAttribute()]
    class Setting
    {
        private static AppSettings appSettings = (AppSettings)ApplicationSettingsBase.Synchronized(new AppSettings());
        private static UserSettings userSettings = (UserSettings)ApplicationSettingsBase.Synchronized(new UserSettings());

        public static AppSettings App
        {
            get { return appSettings; }
        }

        public static UserSettings User
        {
            get { return userSettings; }
        }

        public static void SaveAll()
        {
            appSettings.Save();
            userSettings.Save();
        }
    }


    class AppSettings : ApplicationSettingsBase
    {
        #region CheckUpdates property
        [UserScopedSettingAttribute()]
        [DebuggerNonUserCodeAttribute()]
        [DefaultSettingValueAttribute("true")]
        [SettingsManageabilityAttribute(SettingsManageability.Roaming)]
        public bool CheckUpdates
        {
            get { return (bool)this["CheckUpdates"]; }
            set { this["CheckUpdates"] = value; }
        }
        #endregion

        #region Version property
        [UserScopedSettingAttribute()]
        [DebuggerNonUserCodeAttribute()]
        [DefaultSettingValueAttribute("1.1")]
        [SettingsManageabilityAttribute(SettingsManageability.Roaming)]
        public string Version
        {
            get { return (string)this["Version"]; }
            set { this["Version"] = value; }
        }
        #endregion

        #region Language property
        [UserScopedSettingAttribute()]
        [DebuggerNonUserCodeAttribute()]
        [DefaultSettingValueAttribute("en")]
        [SettingsManageabilityAttribute(SettingsManageability.Roaming)]
        public string Language
        {
            get { return (string)this["Language"]; }
            set { this["Language"] = value; }
        }
        #endregion

        #region pptTemplate
        [UserScopedSettingAttribute()]
        [DebuggerNonUserCodeAttribute()]
        [DefaultSettingValueAttribute("")]
        [SettingsManageabilityAttribute(SettingsManageability.Roaming)]
        public string Template
        {
            get { return (string)this["Template"]; }
            set { this["Template"] = value; }
        }
        #endregion

        #region EndSlideTitle property
        [UserScopedSettingAttribute()]
        [DebuggerNonUserCodeAttribute()]
        [DefaultSettingValueAttribute("")]
        [SettingsManageabilityAttribute(SettingsManageability.Roaming)]
        public string EndSlideTitle
        {
            get { return (string)this["EndSlideTitle"]; }
            set { this["EndSlideTitle"] = value; }
        }
        #endregion

        #region EndSlideText property
        [UserScopedSettingAttribute()]
        [DebuggerNonUserCodeAttribute()]
        [DefaultSettingValueAttribute("")]
        [SettingsManageabilityAttribute(SettingsManageability.Roaming)]
        public string EndSlideText
        {
            get { return (string)this["EndSlideText"]; }
            set { this["EndSlideText"] = value; }
        }
        #endregion
    }

    class UserSettings : ApplicationSettingsBase
    {
        #region Name property
        [UserScopedSettingAttribute()]
        [DebuggerNonUserCodeAttribute()]
        [DefaultSettingValueAttribute("Undefined")]
        [SettingsManageabilityAttribute(SettingsManageability.Roaming)]
        public string Name
        {
            get { return (string)this["Name"]; }
            set { this["Name"] = value; }
        }
        #endregion

        #region Name property
        [UserScopedSettingAttribute()]
        [DebuggerNonUserCodeAttribute()]
        [DefaultSettingValueAttribute("Undefined")]
        [SettingsManageabilityAttribute(SettingsManageability.Roaming)]
        public string LastName
        {
            get { return (string)this["LastName"]; }
            set { this["LastName"] = value; }
        }
        #endregion

        public string ToShortName()
        {
            return this.Name + " " + this.LastName;
        }
    }
}
