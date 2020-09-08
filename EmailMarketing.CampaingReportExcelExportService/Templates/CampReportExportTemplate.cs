﻿// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 16.0.0.0
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------
namespace EmailMarketing.CampaingReportExcelExportService.Templates
{
    using System.Linq;
    using System.Text;
    using System.Collections.Generic;
    using System;
    
    /// <summary>
    /// Class to produce the template output
    /// </summary>
    
    #line 1 "H:\EmailMarketing\EmailMarketing.CampaingReportExcelExportService\Templates\CampReportExportTemplate.tt"
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "16.0.0.0")]
    public partial class CampReportExportTemplate : CampReportExportTemplateBase
    {
#line hidden
        /// <summary>
        /// Create the template output
        /// </summary>
        public virtual string TransformText()
        {
            this.Write("\r\n\r\n");
            this.Write(@"
<!DOCTYPE html>
<html>
<head>
    <link rel=""stylesheet"" type=""text/css"" href=""https://maxcdn.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css"">
</head>
<body style=""background-color: #c9d6df;margin: 0px;"">
    <table border=""0"" width=""750px"" style=""margin:auto;padding:30px;background-color: #F3F3F3;border:1px solid #3f72af;"">
        <tr>
            <td>
                <table border=""0"" width=""100%"">
                    <tr>
                        <td style=""width:30px;"">
                            <img src=""https://img.icons8.com/color/48/000000/filled-sent.png"" />
                        </td>
                        <td>
                            <h1 style=""font-family: system-ui;font-size:25px;"">");
            
            #line 24 "H:\EmailMarketing\EmailMarketing.CampaingReportExcelExportService\Templates\CampReportExportTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(CompanyFullName));
            
            #line default
            #line hidden
            this.Write("</h1>\r\n                        </td>\r\n                        <td>\r\n             " +
                    "               <p style=\"font-family: system-ui;text-align: right;font-size:13px" +
                    ";\"><a href=\"");
            
            #line 27 "H:\EmailMarketing\EmailMarketing.CampaingReportExcelExportService\Templates\CampReportExportTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(CompanyUrl));
            
            #line default
            #line hidden
            this.Write(@""" target=""_blank"" style=""text-decoration: none;"">View In Website</a></p>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table border=""0"" cellpadding=""0"" cellspacing=""0"" style=""text-align:center;width:100%;background-color: #fff;"">
                    <tr>
                        <td style=""background-color:#3f72af;height:80px;font-size:50px;color:#fff;""><img style=""margin-top:10px;"" src=""https://img.icons8.com/color/48/000000/secured-letter.png"" /></i></td>
                    </tr>
                    <tr>
                        <td>
                            <h2 style=""font-family: system-ui;padding-top:25px;font-size:20px;"">Campaign Report Export Confirmation</h2>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <p style=""padding:0px 100px;font-family:system-ui;text-align:left;font-weight:bold;font-size:15px;"">
                                Dear ");
            
            #line 47 "H:\EmailMarketing\EmailMarketing.CampaingReportExcelExportService\Templates\CampReportExportTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(ReceiverName));
            
            #line default
            #line hidden
            this.Write(@",
                            </p>
                            <p style=""padding:0px 100px;font-family: system-ui;text-align:left;font-size:13px;"">
                                Your campaign reports have been exported successfully. We have attached the Excel file with this email. Please check your attached report below.
                            </p>
                            <p style=""padding:0px 100px;font-family: system-ui;text-align:left;font-size:13px;"">
                                If you face any problem, feel free to reply us. 
                            </p>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <p style=""padding:0px 100px;font-family: system-ui;text-align:left;font-size:13px;"">
                                All the best,<br /> ");
            
            #line 60 "H:\EmailMarketing\EmailMarketing.CampaingReportExcelExportService\Templates\CampReportExportTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(CompanyShortName));
            
            #line default
            #line hidden
            this.Write(@" Team
                            </p>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table border=""0"" width=""100%"" style=""border-radius: 5px;text-align: center;margin-top:10px;"">
                    <tr>
                        <td>
                            <h3 style=""margin-top:10px;font-family: system-ui;font-size:17px; "">Stay in touch</h3>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div style=""margin-top:0px;"">
                                <a href=""#"" style=""text-decoration: none;""><img src=""https://img.icons8.com/color/35/000000/twitter-circled.png"" /></a>
                                <a href=""#"" style=""text-decoration: none;""><img src=""https://img.icons8.com/fluent/35/000000/facebook-new.png"" /></a>
                                <a href=""#"" style=""text-decoration: none;""><img src=""https://img.icons8.com/color/35/000000/circled-envelope.png"" /></a>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div style=""margin-top: 10px;"">
                                <span style=""font-size:12px;font-family: system-ui;font-size:11px;"">");
            
            #line 87 "H:\EmailMarketing\EmailMarketing.CampaingReportExcelExportService\Templates\CampReportExportTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(CompanyFullName));
            
            #line default
            #line hidden
            this.Write("</span><br>\r\n                                <span style=\"font-size:12px;font-fam" +
                    "ily: system-ui;font-size:11px;\">Copyright © 2020 ");
            
            #line 88 "H:\EmailMarketing\EmailMarketing.CampaingReportExcelExportService\Templates\CampReportExportTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(CompanyShortName));
            
            #line default
            #line hidden
            this.Write("</span>\r\n                            </div>\r\n                        </td>\r\n     " +
                    "               </tr>\r\n                </table>\r\n            </td>\r\n        </tr>" +
                    "\r\n    </table>\r\n</body>\r\n</html>\r\n");
            return this.GenerationEnvironment.ToString();
        }
    }
    
    #line default
    #line hidden
    #region Base class
    /// <summary>
    /// Base class for this transformation
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "16.0.0.0")]
    public class CampReportExportTemplateBase
    {
        #region Fields
        private global::System.Text.StringBuilder generationEnvironmentField;
        private global::System.CodeDom.Compiler.CompilerErrorCollection errorsField;
        private global::System.Collections.Generic.List<int> indentLengthsField;
        private string currentIndentField = "";
        private bool endsWithNewline;
        private global::System.Collections.Generic.IDictionary<string, object> sessionField;
        #endregion
        #region Properties
        /// <summary>
        /// The string builder that generation-time code is using to assemble generated output
        /// </summary>
        protected System.Text.StringBuilder GenerationEnvironment
        {
            get
            {
                if ((this.generationEnvironmentField == null))
                {
                    this.generationEnvironmentField = new global::System.Text.StringBuilder();
                }
                return this.generationEnvironmentField;
            }
            set
            {
                this.generationEnvironmentField = value;
            }
        }
        /// <summary>
        /// The error collection for the generation process
        /// </summary>
        public System.CodeDom.Compiler.CompilerErrorCollection Errors
        {
            get
            {
                if ((this.errorsField == null))
                {
                    this.errorsField = new global::System.CodeDom.Compiler.CompilerErrorCollection();
                }
                return this.errorsField;
            }
        }
        /// <summary>
        /// A list of the lengths of each indent that was added with PushIndent
        /// </summary>
        private System.Collections.Generic.List<int> indentLengths
        {
            get
            {
                if ((this.indentLengthsField == null))
                {
                    this.indentLengthsField = new global::System.Collections.Generic.List<int>();
                }
                return this.indentLengthsField;
            }
        }
        /// <summary>
        /// Gets the current indent we use when adding lines to the output
        /// </summary>
        public string CurrentIndent
        {
            get
            {
                return this.currentIndentField;
            }
        }
        /// <summary>
        /// Current transformation session
        /// </summary>
        public virtual global::System.Collections.Generic.IDictionary<string, object> Session
        {
            get
            {
                return this.sessionField;
            }
            set
            {
                this.sessionField = value;
            }
        }
        #endregion
        #region Transform-time helpers
        /// <summary>
        /// Write text directly into the generated output
        /// </summary>
        public void Write(string textToAppend)
        {
            if (string.IsNullOrEmpty(textToAppend))
            {
                return;
            }
            // If we're starting off, or if the previous text ended with a newline,
            // we have to append the current indent first.
            if (((this.GenerationEnvironment.Length == 0) 
                        || this.endsWithNewline))
            {
                this.GenerationEnvironment.Append(this.currentIndentField);
                this.endsWithNewline = false;
            }
            // Check if the current text ends with a newline
            if (textToAppend.EndsWith(global::System.Environment.NewLine, global::System.StringComparison.CurrentCulture))
            {
                this.endsWithNewline = true;
            }
            // This is an optimization. If the current indent is "", then we don't have to do any
            // of the more complex stuff further down.
            if ((this.currentIndentField.Length == 0))
            {
                this.GenerationEnvironment.Append(textToAppend);
                return;
            }
            // Everywhere there is a newline in the text, add an indent after it
            textToAppend = textToAppend.Replace(global::System.Environment.NewLine, (global::System.Environment.NewLine + this.currentIndentField));
            // If the text ends with a newline, then we should strip off the indent added at the very end
            // because the appropriate indent will be added when the next time Write() is called
            if (this.endsWithNewline)
            {
                this.GenerationEnvironment.Append(textToAppend, 0, (textToAppend.Length - this.currentIndentField.Length));
            }
            else
            {
                this.GenerationEnvironment.Append(textToAppend);
            }
        }
        /// <summary>
        /// Write text directly into the generated output
        /// </summary>
        public void WriteLine(string textToAppend)
        {
            this.Write(textToAppend);
            this.GenerationEnvironment.AppendLine();
            this.endsWithNewline = true;
        }
        /// <summary>
        /// Write formatted text directly into the generated output
        /// </summary>
        public void Write(string format, params object[] args)
        {
            this.Write(string.Format(global::System.Globalization.CultureInfo.CurrentCulture, format, args));
        }
        /// <summary>
        /// Write formatted text directly into the generated output
        /// </summary>
        public void WriteLine(string format, params object[] args)
        {
            this.WriteLine(string.Format(global::System.Globalization.CultureInfo.CurrentCulture, format, args));
        }
        /// <summary>
        /// Raise an error
        /// </summary>
        public void Error(string message)
        {
            System.CodeDom.Compiler.CompilerError error = new global::System.CodeDom.Compiler.CompilerError();
            error.ErrorText = message;
            this.Errors.Add(error);
        }
        /// <summary>
        /// Raise a warning
        /// </summary>
        public void Warning(string message)
        {
            System.CodeDom.Compiler.CompilerError error = new global::System.CodeDom.Compiler.CompilerError();
            error.ErrorText = message;
            error.IsWarning = true;
            this.Errors.Add(error);
        }
        /// <summary>
        /// Increase the indent
        /// </summary>
        public void PushIndent(string indent)
        {
            if ((indent == null))
            {
                throw new global::System.ArgumentNullException("indent");
            }
            this.currentIndentField = (this.currentIndentField + indent);
            this.indentLengths.Add(indent.Length);
        }
        /// <summary>
        /// Remove the last indent that was added with PushIndent
        /// </summary>
        public string PopIndent()
        {
            string returnValue = "";
            if ((this.indentLengths.Count > 0))
            {
                int indentLength = this.indentLengths[(this.indentLengths.Count - 1)];
                this.indentLengths.RemoveAt((this.indentLengths.Count - 1));
                if ((indentLength > 0))
                {
                    returnValue = this.currentIndentField.Substring((this.currentIndentField.Length - indentLength));
                    this.currentIndentField = this.currentIndentField.Remove((this.currentIndentField.Length - indentLength));
                }
            }
            return returnValue;
        }
        /// <summary>
        /// Remove any indentation
        /// </summary>
        public void ClearIndent()
        {
            this.indentLengths.Clear();
            this.currentIndentField = "";
        }
        #endregion
        #region ToString Helpers
        /// <summary>
        /// Utility class to produce culture-oriented representation of an object as a string.
        /// </summary>
        public class ToStringInstanceHelper
        {
            private System.IFormatProvider formatProviderField  = global::System.Globalization.CultureInfo.InvariantCulture;
            /// <summary>
            /// Gets or sets format provider to be used by ToStringWithCulture method.
            /// </summary>
            public System.IFormatProvider FormatProvider
            {
                get
                {
                    return this.formatProviderField ;
                }
                set
                {
                    if ((value != null))
                    {
                        this.formatProviderField  = value;
                    }
                }
            }
            /// <summary>
            /// This is called from the compile/run appdomain to convert objects within an expression block to a string
            /// </summary>
            public string ToStringWithCulture(object objectToConvert)
            {
                if ((objectToConvert == null))
                {
                    throw new global::System.ArgumentNullException("objectToConvert");
                }
                System.Type t = objectToConvert.GetType();
                System.Reflection.MethodInfo method = t.GetMethod("ToString", new System.Type[] {
                            typeof(System.IFormatProvider)});
                if ((method == null))
                {
                    return objectToConvert.ToString();
                }
                else
                {
                    return ((string)(method.Invoke(objectToConvert, new object[] {
                                this.formatProviderField })));
                }
            }
        }
        private ToStringInstanceHelper toStringHelperField = new ToStringInstanceHelper();
        /// <summary>
        /// Helper to produce culture-oriented representation of an object as a string
        /// </summary>
        public ToStringInstanceHelper ToStringHelper
        {
            get
            {
                return this.toStringHelperField;
            }
        }
        #endregion
    }
    #endregion
}
