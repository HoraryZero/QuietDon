﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Don_3000.Properties
{


    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "11.0.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase
    {

        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        internal string Login;
        internal string Password;

        public static Settings Default
        {
            get
            {
                return defaultInstance;
            }
        }

        public string Name { get; internal set; }
        public string Pass { get; internal set; }
        public string PASS { get; internal set; }
        public int Port { get; internal set; }
        public string Protokol { get; internal set; }
        public string LOG { get; internal set; }
    }
}