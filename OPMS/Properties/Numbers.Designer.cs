﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OPMS.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "16.7.0.0")]
    internal sealed partial class Numbers : global::System.Configuration.ApplicationSettingsBase {
        
        private static Numbers defaultInstance = ((Numbers)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Numbers())));
        
        public static Numbers Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("1")]
        public int patient_bill_no {
            get {
                return ((int)(this["patient_bill_no"]));
            }
            set {
                this["patient_bill_no"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("1")]
        public int patient_receipt_no {
            get {
                return ((int)(this["patient_receipt_no"]));
            }
            set {
                this["patient_receipt_no"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("1")]
        public int hdl_bill_no {
            get {
                return ((int)(this["hdl_bill_no"]));
            }
            set {
                this["hdl_bill_no"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("1")]
        public int hdl_receipt_no {
            get {
                return ((int)(this["hdl_receipt_no"]));
            }
            set {
                this["hdl_receipt_no"] = value;
            }
        }
    }
}
