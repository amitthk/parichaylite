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
    /// An object representation of the Lookup_request_type table
	/// </summary>
	[JsonObject]
    public class LookupRequestType
	{
		protected System.Int32 _Id;

		private System.String _Name;

		public virtual System.String Name
		{
			get
			{
                return _Name;
			}
			set
			{
				if (value == null)
				{
					throw new BusinessException("Name Required", "Name must not be null.");
				}
                _Name = value;
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


        protected bool Equals(LookupRequestType entity)
		{
            if (entity == null) return false;
            if (!base.Equals(entity)) return false;
            if (!Equals(_Id, entity._Id)) return false;
            return true;
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(this, obj)) return true;
            return Equals(obj as LookupRequestType);
		}

		public override int GetHashCode()
		{
			int result = base.GetHashCode();
			result = 29*result + _Id.GetHashCode();
			return result;
		}

	}
}
