using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOSMDMProdConsoleApp.Models
{
        /// <summary>
        /// mail request 
        /// </summary>
     public class MailRequest
     {
            #region PrivateFields

            /// <summary>
            /// file name
            /// </summary>
            private string _fromField;

            /// <summary>
            /// return to
            /// </summary>
            private string _toField;

            /// <summary>
            /// copy field
            /// </summary>
            private string _copyField;

            /// <summary>
            /// bcc field
            /// </summary>
            private string _bccField;

            /// <summary>
            /// title
            /// </summary>
            private string _subjectField;

            /// <summary>
            /// sender name
            /// </summary>
            private string _bodyField;

            /// <summary>
            /// content
            /// </summary>
            private MailRequestAttachments[] _attachmentsField;

            #endregion

            /// <summary>
            /// sender，seperated by;
            /// </summary>
            public string From
            {
                get
                {
                    return this._fromField;
                }

                set
                {
                    this._fromField = value;
                }
            }

            /// <summary>
            /// receiver，seperated by;
            /// </summary>
            public string To
            {
                get
                {
                    return this._toField;
                }

                set
                {
                    this._toField = value;
                }
            }

            /// <summary>
            /// copyer，seperated by;
            /// </summary>
            public string CC
            {
                get
                {
                    return this._copyField;
                }

                set
                {
                    this._copyField = value;
                }
            }

            /// <summary>
            /// serect copyer，seperated by;
            /// </summary>
            public string Bcc
            {
                get
                {
                    return this._bccField;
                }

                set
                {
                    this._bccField = value;
                }
            }

            /// <summary>
            /// subject
            /// </summary>
            public string Subject
            {
                get
                {
                    return this._subjectField;
                }

                set
                {
                    this._subjectField = value;
                }
            }

            /// <summary>
            /// body
            /// </summary>
            public string Body
            {
                get
                {
                    return this._bodyField;
                }

                set
                {
                    this._bodyField = value;
                }
            }

            /// <summary>
            /// attachments list
            /// </summary>
            public MailRequestAttachments[] Attachments
            {
                get
                {
                    return this._attachmentsField;
                }

                set
                {
                    this._attachmentsField = value;
                }
            }
        }

     /// <summary>
     /// the mail request acttachment of sender
     /// </summary>
     public class MailRequestAttachments
     {
         #region PrivateFields

         /// <summary>
         /// file name field
         /// </summary>
         private string _fileNameField;

         /// <summary>
         /// file data
         /// </summary>
         private byte[] _fileDataField;

         #endregion

         /// <summary>
         /// filename
         /// </summary>
         public string FileName
         {
             get
             {
                 return this._fileNameField;
             }

             set
             {
                 this._fileNameField = value;
             }
         }

         /// <summary>
         /// file data
         /// </summary>
         public byte[] FileData
         {
             get
             {
                 return this._fileDataField;
             }

             set
             {
                 this._fileDataField = value;
             }
         }
     }
}

