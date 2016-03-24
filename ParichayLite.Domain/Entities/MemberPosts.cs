using System;

using System.Text;

//using Iesi.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace ParichayLite.Domain.Entities
{
    /// <summary>
    /// An object representation of the MemberPosts table
    /// </summary>
    [JsonObject]
    public class MemberPosts
    {
        protected System.Int32 _Id;

        private System.Int32 _Privacy;
        private System.String _Targeturl;
        private System.DateTime _Createdon;
        private System.DateTime _Modifiedon;
        private System.Int32 _Type;
        private System.String _Source;
        private System.String _Text;
        private System.String _Thumbnailfilename;
        private MemberDetails _Sender;
        private MemberDetails _Recipient;


        //private System.Int32 _GroupId;
        private HashSet<MemberReplies> _Replies;

        //UI field
        public int rUrl { get; set; }

        public virtual HashSet<MemberReplies> Replies
        {
            get { return _Replies; }
            set { _Replies = value; }
        }

        public MemberDetails Recipient
        {
            get { return _Recipient; }
            set { _Recipient = value; }
        }

        public virtual System.Int32 Privacy
        {
            get
            {
                return _Privacy;
            }
            set
            {
                _Privacy = value;
            }
        }

        public virtual System.String Targeturl
        {
            get
            {
                return _Targeturl;
            }
            set
            {
                _Targeturl = value;
            }
        }

        public virtual System.DateTime Createdon
        {
            get
            {
                return _Createdon;
            }
            set
            {
                _Createdon = value;
            }
        }

        public virtual System.DateTime Modifiedon
        {
            get
            {
                return _Modifiedon;
            }
            set
            {
                _Modifiedon = value;
            }
        }

        public virtual System.Int32 Type
        {
            get
            {
                return _Type;
            }
            set
            {
                _Type = value;
            }
        }

        public virtual System.String Source
        {
            get
            {
                return _Source;
            }
            set
            {
                if (value == null)
                {
                    throw new BusinessException("SourceRequired", "Source must not be null.");
                }
                _Source = value;
            }
        }


        public virtual System.Int32 Id
        {
            get
            {
                return _Id;
            }
            set
            {
                _Id = value;
            }
        }

        [Required]
        public virtual System.String Text
        {
            get
            {
                return _Text;
            }
            set
            {
                if (value == null)
                {
                    throw new BusinessException("TextRequired", "Text must not be null.");
                }
                _Text = value;
            }
        }

        public virtual System.String Thumbnailfilename
        {
            get
            {
                return _Thumbnailfilename;
            }
            set
            {
                _Thumbnailfilename = value;
            }
        }

        [Required]
        public virtual MemberDetails Sender
        {
            get
            {
                return _Sender;
            }
            set
            {
                _Sender = value;
            }
        }

        public bool IsPrivate { get { return (_Privacy != 0); } set { _Privacy = (value) ? 1 : 0; } }

        public string CreatedAgo
        { get { return (_Createdon.Ago()); } }
        //public virtual System.Int32 GroupId
        //{
        //    get { return _GroupId; }
        //    set { _GroupId = value; }
        //}

        //public MemberMessage()
        //    : base()
        //{ }
    }

}
