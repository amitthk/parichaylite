﻿using System;

using System.Text;

//using Iesi.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace ParichayLite.Domain.Entities
{
    /// <summary>
    /// An object representation of the MemberMessages table
    /// </summary>
    [JsonObject]
    public class MemberMessage
    {
        protected System.Int32 _Id;

        private System.Int32 _Privacy;
        private System.String _Targeturl;
        private System.DateTime _Createdon;
        private System.DateTime _Modifiedon;
        private System.Int32 _Type;
        private System.String _Source;
        private MemberMessage _ParentId;
        private MemberDetails _Recipient;
        private MemberMessage _Message;
        private System.String _Text;
        private System.String _Thumbnailfilename;
        private MemberDetails _Sender;
        //private System.Int32 _GroupId;
        private HashSet<MemberMessage> _Children;

        //UI field
        public int rUrl { get; set; }

        public virtual HashSet<MemberMessage> Children
        {
            get { return _Children; }
            set { _Children = value; }
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

        public virtual MemberMessage ParentId
        {
            get
            {
                return _ParentId;
            }
            set
            {
                _ParentId = value;
            }
        }

        [Required]
        public virtual MemberDetails Recipient
        {
            get
            {
                return _Recipient;
            }
            set
            {
                _Recipient = value;
            }
        }
        public virtual MemberMessage Message
        {
            get
            {
                return _Message;
            }
            set
            {
                _Message = value;
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
