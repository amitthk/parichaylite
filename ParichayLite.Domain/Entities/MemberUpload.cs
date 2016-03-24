using System;
using System.Collections.Generic;
using System.ComponentModel;
//using Iesi.Collections;
//using Iesi.Collections.Generic;



using ParichayLite.Domain;
using Newtonsoft.Json;

namespace ParichayLite.Domain.Entities
{    
	/// <summary>
	/// An object representation of the Member_Uploads table
	/// </summary>
	[JsonObject]
	public class MemberUploads
	{
		protected System.Int32 _Id;

		private System.String _SectionNo;
		private System.String _Rank;
		private System.String _FileContentT; //Content Type
		private Nullable<System.Int32> _FileSize;
        private Nullable<System.Int32> _CuniqN; //UploadedMediaLocation
        private MemberDetails _Owner;
		private Nullable<System.Int32> _CoUniqN;
		private System.Byte[] _FileDetail;
        private Nullable<System.Int32> _ConfC;
		private System.String _Attachmt; //File Name
		private Nullable<System.DateTime> _CreateD;
		private Nullable<System.Int32> _PUniqN;

		public virtual System.String SectionNo
		{
			get
			{
				return _SectionNo;
			}
			set
			{
				_SectionNo = value;
			}
		}

		public virtual System.String Rank
		{
			get
			{
				return _Rank;
			}
			set
			{
				_Rank = value;
			}
		}

		public virtual System.String FileContentT
		{
			get
			{
				return _FileContentT;
			}
			set
			{
				_FileContentT = value;
			}
		}

		public virtual Nullable<System.Int32> FileSize
		{
			get
			{
				return _FileSize;
			}
			set
			{
				_FileSize = value;
			}
		}

        public virtual MemberDetails Owner
        {
            get { return _Owner; }
            set { _Owner = value; }
        }

        public virtual Nullable<System.Int32> CuniqN
        {
            get
            {
                return _CuniqN;
            }
            set
            {
                _CuniqN = value;
            }
        }

		public virtual Nullable<System.Int32> CoUniqN
		{
			get
			{
				return _CoUniqN;
			}
			set
			{
				_CoUniqN = value;
			}
		}

		public virtual System.Byte[] FileDetail
		{
			get
			{
				return _FileDetail;
			}
			set
			{
				_FileDetail = value;
			}
		}

        public virtual Nullable<System.Int32> ConfC
		{
			get
			{
				return _ConfC;
			}
			set
			{
				_ConfC = value;
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

		public virtual System.String Attachmt
		{
			get
			{
				return _Attachmt;
			}
			set
			{
				_Attachmt = value;
			}
		}

		public virtual Nullable<System.DateTime> CreateD
		{
			get
			{
				return _CreateD;
			}
			set
			{
				_CreateD = value;
			}
		}

		public virtual Nullable<System.Int32> PUniqN
		{
			get
			{
				return _PUniqN;
			}
			set
			{
				_PUniqN = value;
			}
		}

        public UploadedMediaLocation UploadedMediaLocation
        {
            get {
                return(_CuniqN.HasValue ? (UploadedMediaLocation)_CuniqN.Value : UploadedMediaLocation.Database);
            
            }
            set { _CuniqN = (int)value; }
        }

	}
}
