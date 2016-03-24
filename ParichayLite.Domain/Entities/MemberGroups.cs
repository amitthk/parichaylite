using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace ParichayLite.Domain.Entities
{
    /// <summary>
    /// An object representation of the MemberGroups table
    /// </summary>
    [JsonObject]
    public class MemberGroups
    {
        protected System.Int32 _Id;

        private Nullable<System.DateTime> _Createdon;
        private System.Int32 _Isautoapprove;
        private Nullable<System.Int32> _Visibility;
        private Nullable<System.DateTime> _Modifiedon;
        private System.Int32 _Canmembersinvite;
        private  IList<MemberGroupmessages> _FKGrpMsgGrp = new List<MemberGroupmessages>();
        private  IList<MemberGroupmembers> _FKGrpMmbrsGrp = new List<MemberGroupmembers>();
        private System.String _About;
        private System.String _Url;
        private System.String _Name;
        private Nullable<System.Int32> _AvatarId;
        private MemberDetails _Owner;


        public virtual Nullable<System.DateTime> Createdon
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

        public virtual System.Int32 Isautoapprove
        {
            get
            {
                return _Isautoapprove;
            }
            set
            {
                _Isautoapprove = value;
            }
        }

        public virtual Nullable<System.Int32> Visibility
        {
            get
            {
                return _Visibility;
            }
            set
            {
                _Visibility = value;
            }
        }

        public virtual Nullable<System.DateTime> Modifiedon
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

        public virtual System.Int32 Canmembersinvite
        {
            get
            {
                return _Canmembersinvite;
            }
            set
            {
                _Canmembersinvite = value;
            }
        }

        public virtual MemberDetails Owner
        {
            get { return _Owner; }
            set { _Owner = value; }
        }

        [Required]
        public System.Int32 OwnerId
        {
            get
            {
                return (_Owner.Id);
            }
            set
            {
                if (_Owner == null)
                {
                    _Owner = new MemberDetails() { Id = value };
                }
                else
                {
                    _Owner.Id = value;
                }
            }
        }


        public Nullable<System.Int32> AvatarId
        {
            get { return _AvatarId; }
            set { _AvatarId = value; }
        }


        public virtual IList<MemberGroupmessages> FKGrpMsgGrp
        {
            get
            {
                return _FKGrpMsgGrp;
            }
        }

        public virtual IList<MemberGroupmembers> FKGrpMmbrsGrp
        {
            get
            {
                return _FKGrpMmbrsGrp;
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

        public virtual System.String About
        {
            get
            {
                return _About;
            }
            set
            {
                _About = value;
            }
        }

        public virtual System.String Url
        {
            get
            {
                return _Url;
            }
            set
            {
                _Url = value;
            }
        }

        [Required]
        [StringLength(255)]
        public virtual System.String Name
        {
            get
            {
                return _Name;
            }
            set
            {
                _Name = value;
            }
        }

        public bool bIsautoapprove { get { return (_Isautoapprove != 0); } set { _Isautoapprove = (value) ? 1 : 0; } }
        public bool bIsVisible { get { return (_Visibility != 0); } set { _Visibility = (value) ? 1 : 0; } }
        public bool bCanmembersinvite { get { return (_Canmembersinvite != 0); } set { _Canmembersinvite = (value) ? 1 : 0; } }
    }
}
