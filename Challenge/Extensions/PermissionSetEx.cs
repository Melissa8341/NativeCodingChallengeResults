using AE.CoreInterface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace AE.CoreUtility
{
	public class PermissionSet
	{
		private const int max = 100;

		public List<string> Permissions = new();

		public void Add(string permission)
		{
			if (Permissions.Count < max)
			{
				this.Permissions.Add(permission);
			}
			else
			{
				throw new ArgumentOutOfRangeException("Max permission threshold was breached.");
			}
		}

		public byte[] ExportPermissions()
		{
			if (this.Permissions == null)
			{
				return null;
			}
			using var ms = new MemoryStream();
			var serializer = new DataContractSerializer(typeof(List<string>));
			serializer.WriteObject(ms, this.Permissions);
			return ms.ToArray();
		}

		public void Import(byte[] byteArray)
		{

			if (byteArray == null)
			{
				this.Permissions = null;
			}
			else
			{
				using var memStream = new MemoryStream(byteArray);
				var serializer = new DataContractSerializer(typeof(List<string>));
				var obj = (List<string>)serializer.ReadObject(memStream);
				this.Permissions = obj;
			}
		}
	}
}
