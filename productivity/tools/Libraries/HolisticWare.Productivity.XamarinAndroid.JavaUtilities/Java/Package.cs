using System;
using Core;

namespace Moka.Lang
{
	public partial class Package : SyntaxElement
	{
		public Package PackageParent
		{
			get;
			set;
		}

		public Package ()
		{
			
		}

        public Package (string java_class)
        {
			return;
        }

		public Package (string[] parts)
		{
			this.Name = parts [0];
			this.PackageParent = null;

			if (parts.Length == 1)
			{
				return;
			}

			int size_new = parts.Length - 1;
			string[] parts_new = parts.Slice (1, parts.Length);

			Package package_new = new Package (parts_new)
			{
				PackageParent = this,
			};
				
			return;
		}

		public static Package Parse (string java_class)
		{
			Package p =  new Package();

			p.NameFullyQualifiedParts = java_class.Split
													(
														new String[]{"/"},
														StringSplitOptions.RemoveEmptyEntries
													);
			return p;
		}



		private const string package_namespace_normalization_template = 
			@"
	<attr
		path=""/api/package[@name='PLACEHOLDER_NAME_PACKAGE']""
		name=""managedName""
		>
		PLACEHOLDER_NAME_NAMESPACE
	</attr>
			";


		string api_xml_package = 
			@"
		<add-node path=""/api"">
			<package name=""PLACEHOLDER_NAME_PACKAGE"">

				<!-- PLACEHOLDER_INTERFACE -->

				<!-- PLACEHOLDER_CLASS -->

			</package>
		</add-node>
			";
	}
}

