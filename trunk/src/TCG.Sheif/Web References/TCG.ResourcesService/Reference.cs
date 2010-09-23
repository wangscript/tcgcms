﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.1
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by Microsoft.VSDesigner, Version 4.0.30319.1.
// 
#pragma warning disable 1591

namespace TCG.Sheif.TCG.ResourcesService {
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.ComponentModel;
    using System.Xml.Serialization;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="ResourcesServiceSoap", Namespace="http://tempuri.org/")]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(EntityBase))]
    public partial class ResourcesService : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private TCGSoapHeader tCGSoapHeaderValueField;
        
        private System.Threading.SendOrPostCallback CreateResourcesOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public ResourcesService() {
            this.Url = global::TCG.Sheif.Properties.Settings.Default.TCG_Sheif_TCG_ResourcesService_ResourcesService;
            if ((this.IsLocalFileSystemWebService(this.Url) == true)) {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        public TCGSoapHeader TCGSoapHeaderValue {
            get {
                return this.tCGSoapHeaderValueField;
            }
            set {
                this.tCGSoapHeaderValueField = value;
            }
        }
        
        public new string Url {
            get {
                return base.Url;
            }
            set {
                if ((((this.IsLocalFileSystemWebService(base.Url) == true) 
                            && (this.useDefaultCredentialsSetExplicitly == false)) 
                            && (this.IsLocalFileSystemWebService(value) == false))) {
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }
        
        public new bool UseDefaultCredentials {
            get {
                return base.UseDefaultCredentials;
            }
            set {
                base.UseDefaultCredentials = value;
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        /// <remarks/>
        public event CreateResourcesCompletedEventHandler CreateResourcesCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapHeaderAttribute("TCGSoapHeaderValue")]
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/CreateResources", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public int CreateResources(Resources inf) {
            object[] results = this.Invoke("CreateResources", new object[] {
                        inf});
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        public void CreateResourcesAsync(Resources inf) {
            this.CreateResourcesAsync(inf, null);
        }
        
        /// <remarks/>
        public void CreateResourcesAsync(Resources inf, object userState) {
            if ((this.CreateResourcesOperationCompleted == null)) {
                this.CreateResourcesOperationCompleted = new System.Threading.SendOrPostCallback(this.OnCreateResourcesOperationCompleted);
            }
            this.InvokeAsync("CreateResources", new object[] {
                        inf}, this.CreateResourcesOperationCompleted, userState);
        }
        
        private void OnCreateResourcesOperationCompleted(object arg) {
            if ((this.CreateResourcesCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.CreateResourcesCompleted(this, new CreateResourcesCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        public new void CancelAsync(object userState) {
            base.CancelAsync(userState);
        }
        
        private bool IsLocalFileSystemWebService(string url) {
            if (((url == null) 
                        || (url == string.Empty))) {
                return false;
            }
            System.Uri wsUri = new System.Uri(url);
            if (((wsUri.Port >= 1024) 
                        && (string.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) == 0))) {
                return true;
            }
            return false;
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://tempuri.org/")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace="http://tempuri.org/", IsNullable=false)]
    public partial class TCGSoapHeader : System.Web.Services.Protocols.SoapHeader {
        
        private string passWordField;
        
        private System.Xml.XmlAttribute[] anyAttrField;
        
        /// <remarks/>
        public string PassWord {
            get {
                return this.passWordField;
            }
            set {
                this.passWordField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyAttributeAttribute()]
        public System.Xml.XmlAttribute[] AnyAttr {
            get {
                return this.anyAttrField;
            }
            set {
                this.anyAttrField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(Template))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(Categories))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(Resources))]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://tempuri.org/")]
    public partial class EntityBase {
        
        private string idField;
        
        /// <remarks/>
        public string Id {
            get {
                return this.idField;
            }
            set {
                this.idField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://tempuri.org/")]
    public partial class Template : EntityBase {
        
        private string contentField;
        
        private string skinIdField;
        
        private TemplateType templateTypeField;
        
        private string iParentIdField;
        
        private int iSystemTypeField;
        
        private System.DateTime dUpdateDateField;
        
        private System.DateTime dAddDateField;
        
        private string vcTempNameField;
        
        private string vcUrlField;
        
        /// <remarks/>
        public string Content {
            get {
                return this.contentField;
            }
            set {
                this.contentField = value;
            }
        }
        
        /// <remarks/>
        public string SkinId {
            get {
                return this.skinIdField;
            }
            set {
                this.skinIdField = value;
            }
        }
        
        /// <remarks/>
        public TemplateType TemplateType {
            get {
                return this.templateTypeField;
            }
            set {
                this.templateTypeField = value;
            }
        }
        
        /// <remarks/>
        public string iParentId {
            get {
                return this.iParentIdField;
            }
            set {
                this.iParentIdField = value;
            }
        }
        
        /// <remarks/>
        public int iSystemType {
            get {
                return this.iSystemTypeField;
            }
            set {
                this.iSystemTypeField = value;
            }
        }
        
        /// <remarks/>
        public System.DateTime dUpdateDate {
            get {
                return this.dUpdateDateField;
            }
            set {
                this.dUpdateDateField = value;
            }
        }
        
        /// <remarks/>
        public System.DateTime dAddDate {
            get {
                return this.dAddDateField;
            }
            set {
                this.dAddDateField = value;
            }
        }
        
        /// <remarks/>
        public string vcTempName {
            get {
                return this.vcTempNameField;
            }
            set {
                this.vcTempNameField = value;
            }
        }
        
        /// <remarks/>
        public string vcUrl {
            get {
                return this.vcUrlField;
            }
            set {
                this.vcUrlField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://tempuri.org/")]
    public enum TemplateType {
        
        /// <remarks/>
        SinglePageType,
        
        /// <remarks/>
        InfoType,
        
        /// <remarks/>
        ListType,
        
        /// <remarks/>
        OriginalType,
        
        /// <remarks/>
        SystemPage,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://tempuri.org/")]
    public partial class Categories : EntityBase {
        
        private string parentField;
        
        private Template resourceTemplateField;
        
        private Template resourceListTemplateField;
        
        private int iOrderField;
        
        private System.DateTime dUpdateDateField;
        
        private string vcClassNameField;
        
        private string vcNameField;
        
        private string vcDirectoryField;
        
        private string vcUrlField;
        
        private string cVisibleField;
        
        private string dataBaseServiceField;
        
        private string skinIdField;
        
        /// <remarks/>
        public string Parent {
            get {
                return this.parentField;
            }
            set {
                this.parentField = value;
            }
        }
        
        /// <remarks/>
        public Template ResourceTemplate {
            get {
                return this.resourceTemplateField;
            }
            set {
                this.resourceTemplateField = value;
            }
        }
        
        /// <remarks/>
        public Template ResourceListTemplate {
            get {
                return this.resourceListTemplateField;
            }
            set {
                this.resourceListTemplateField = value;
            }
        }
        
        /// <remarks/>
        public int iOrder {
            get {
                return this.iOrderField;
            }
            set {
                this.iOrderField = value;
            }
        }
        
        /// <remarks/>
        public System.DateTime dUpdateDate {
            get {
                return this.dUpdateDateField;
            }
            set {
                this.dUpdateDateField = value;
            }
        }
        
        /// <remarks/>
        public string vcClassName {
            get {
                return this.vcClassNameField;
            }
            set {
                this.vcClassNameField = value;
            }
        }
        
        /// <remarks/>
        public string vcName {
            get {
                return this.vcNameField;
            }
            set {
                this.vcNameField = value;
            }
        }
        
        /// <remarks/>
        public string vcDirectory {
            get {
                return this.vcDirectoryField;
            }
            set {
                this.vcDirectoryField = value;
            }
        }
        
        /// <remarks/>
        public string vcUrl {
            get {
                return this.vcUrlField;
            }
            set {
                this.vcUrlField = value;
            }
        }
        
        /// <remarks/>
        public string cVisible {
            get {
                return this.cVisibleField;
            }
            set {
                this.cVisibleField = value;
            }
        }
        
        /// <remarks/>
        public string DataBaseService {
            get {
                return this.dataBaseServiceField;
            }
            set {
                this.dataBaseServiceField = value;
            }
        }
        
        /// <remarks/>
        public string SkinId {
            get {
                return this.skinIdField;
            }
            set {
                this.skinIdField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://tempuri.org/")]
    public partial class Resources : EntityBase {
        
        private Categories categorieField;
        
        private string vcTitleField;
        
        private string vcUrlField;
        
        private string vcContentField;
        
        private string vcAuthorField;
        
        private int iCountField;
        
        private string vcKeyWordField;
        
        private string vcEditorField;
        
        private string cCreatedField;
        
        private string cPostByUserField;
        
        private string vcSmallImgField;
        
        private string vcBigImgField;
        
        private string vcShortContentField;
        
        private string vcSpecialityField;
        
        private string cCheckedField;
        
        private string cDelField;
        
        private string vcFilePathField;
        
        private System.DateTime dAddDateField;
        
        private System.DateTime dUpDateDateField;
        
        private string vcTitleColorField;
        
        private string cStrongField;
        
        /// <remarks/>
        public Categories Categorie {
            get {
                return this.categorieField;
            }
            set {
                this.categorieField = value;
            }
        }
        
        /// <remarks/>
        public string vcTitle {
            get {
                return this.vcTitleField;
            }
            set {
                this.vcTitleField = value;
            }
        }
        
        /// <remarks/>
        public string vcUrl {
            get {
                return this.vcUrlField;
            }
            set {
                this.vcUrlField = value;
            }
        }
        
        /// <remarks/>
        public string vcContent {
            get {
                return this.vcContentField;
            }
            set {
                this.vcContentField = value;
            }
        }
        
        /// <remarks/>
        public string vcAuthor {
            get {
                return this.vcAuthorField;
            }
            set {
                this.vcAuthorField = value;
            }
        }
        
        /// <remarks/>
        public int iCount {
            get {
                return this.iCountField;
            }
            set {
                this.iCountField = value;
            }
        }
        
        /// <remarks/>
        public string vcKeyWord {
            get {
                return this.vcKeyWordField;
            }
            set {
                this.vcKeyWordField = value;
            }
        }
        
        /// <remarks/>
        public string vcEditor {
            get {
                return this.vcEditorField;
            }
            set {
                this.vcEditorField = value;
            }
        }
        
        /// <remarks/>
        public string cCreated {
            get {
                return this.cCreatedField;
            }
            set {
                this.cCreatedField = value;
            }
        }
        
        /// <remarks/>
        public string cPostByUser {
            get {
                return this.cPostByUserField;
            }
            set {
                this.cPostByUserField = value;
            }
        }
        
        /// <remarks/>
        public string vcSmallImg {
            get {
                return this.vcSmallImgField;
            }
            set {
                this.vcSmallImgField = value;
            }
        }
        
        /// <remarks/>
        public string vcBigImg {
            get {
                return this.vcBigImgField;
            }
            set {
                this.vcBigImgField = value;
            }
        }
        
        /// <remarks/>
        public string vcShortContent {
            get {
                return this.vcShortContentField;
            }
            set {
                this.vcShortContentField = value;
            }
        }
        
        /// <remarks/>
        public string vcSpeciality {
            get {
                return this.vcSpecialityField;
            }
            set {
                this.vcSpecialityField = value;
            }
        }
        
        /// <remarks/>
        public string cChecked {
            get {
                return this.cCheckedField;
            }
            set {
                this.cCheckedField = value;
            }
        }
        
        /// <remarks/>
        public string cDel {
            get {
                return this.cDelField;
            }
            set {
                this.cDelField = value;
            }
        }
        
        /// <remarks/>
        public string vcFilePath {
            get {
                return this.vcFilePathField;
            }
            set {
                this.vcFilePathField = value;
            }
        }
        
        /// <remarks/>
        public System.DateTime dAddDate {
            get {
                return this.dAddDateField;
            }
            set {
                this.dAddDateField = value;
            }
        }
        
        /// <remarks/>
        public System.DateTime dUpDateDate {
            get {
                return this.dUpDateDateField;
            }
            set {
                this.dUpDateDateField = value;
            }
        }
        
        /// <remarks/>
        public string vcTitleColor {
            get {
                return this.vcTitleColorField;
            }
            set {
                this.vcTitleColorField = value;
            }
        }
        
        /// <remarks/>
        public string cStrong {
            get {
                return this.cStrongField;
            }
            set {
                this.cStrongField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    public delegate void CreateResourcesCompletedEventHandler(object sender, CreateResourcesCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class CreateResourcesCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal CreateResourcesCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public int Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((int)(this.results[0]));
            }
        }
    }
}

#pragma warning restore 1591