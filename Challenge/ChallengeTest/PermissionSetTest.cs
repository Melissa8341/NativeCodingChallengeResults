using AE.CoreUtility;
using System;
using Xunit;

namespace PermissionSetTest
{
	public class PermissionSetTest
	{
		[Fact]
		public void PermissionSet()
		{
			string permissionCSV;
			PermissionSet ps = new PermissionSet();
			ps.Add("permission1");
			ps.Add("permission2");
			ps.Add("permission3");
			permissionCSV = string.Join(",", ps.Permissions);
			var byteArray = ps.ExportPermissions();

			ps.Import(byteArray);
			Assert.Equal(permissionCSV, string.Join(",", ps.Permissions));
		}

		[Fact]
		public void PermissionSet_ExceedMax()
		{
			PermissionSet ps = new PermissionSet();
			var exceptionThrown = false;

			try
			{
				for (int i = 1; i <= 101; i++)
				{
					ps.Add($"permission{i}");
				}
			}
			catch (ArgumentOutOfRangeException )
			{
				exceptionThrown = true;
			}

			Assert.True(exceptionThrown);
		}
	}
}
