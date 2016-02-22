using System;

namespace HolisticWare.Productivity.XamarinAndroid.JavaUtilities
{
	public class MetadataTransforms
	{
		public MetadataTransforms ()
		{
		}





		static readonly string XmlFileMetadata =
		@"
<?xml version=""1.0"" encoding=""UTF-8"" ?>
<metadata>
	<!-- %$ATTR_PACKAGE_MANAGED_NAME$ -->
</metadata>
		";

		static readonly string XmlFileMetadataComment =
		@"
	<!--
	===========================================================================================================
	.net-ification/normalization of the namespace (java package)
	
		*	shortened (removed top level domain like com)
		*	uppercase
		*	pluralized

	.netification/plurlization of namespaces (packages) will solve some of wranings 

		";

		static readonly string AttrChangePackageManagedName =
		@"
	<attr 
		path=""/api/package[@name='com.product.namespace\']"" 
		name=""managedName""
		>
		ProductSDK.Nmespace
	</attr>
	";

	}
}

