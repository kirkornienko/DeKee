﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeKee.DomainContext.Services.Dto
{
    //------------------------------------------------------------------------------
    // <auto-generated>
    //     This code was generated by a tool.
    //     Runtime Version:4.0.30319.42000
    //
    //     Changes to this file may cause incorrect behavior and will be lost if
    //     the code is regenerated.
    // </auto-generated>
    //------------------------------------------------------------------------------

    using System.Xml.Serialization;

    // 
    // This source code was auto-generated by xsd, Version=4.6.1055.0.
    // 


    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", ElementName ="TransferTo", IsNullable = false)]
    public partial class ReserveIdResponse
    {

        private byte status_codeField;

        private string reserved_idField;

        private string error_txtField;

        private ulong authentication_keyField;

        private byte error_codeField;

        /// <remarks/>
        public byte status_code
        {
            get
            {
                return this.status_codeField;
            }
            set
            {
                this.status_codeField = value;
            }
        }

        /// <remarks/>
        public string reserved_id
        {
            get
            {
                return this.reserved_idField;
            }
            set
            {
                this.reserved_idField = value;
            }
        }

        /// <remarks/>
        public string error_txt
        {
            get
            {
                return this.error_txtField;
            }
            set
            {
                this.error_txtField = value;
            }
        }

        /// <remarks/>
        public ulong authentication_key
        {
            get
            {
                return this.authentication_keyField;
            }
            set
            {
                this.authentication_keyField = value;
            }
        }

        /// <remarks/>
        public byte error_code
        {
            get
            {
                return this.error_codeField;
            }
            set
            {
                this.error_codeField = value;
            }
        }
    }

}