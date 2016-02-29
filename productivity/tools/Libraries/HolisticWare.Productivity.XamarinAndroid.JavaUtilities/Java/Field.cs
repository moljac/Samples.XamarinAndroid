using System;

namespace Moka.Lang
{
	public partial class Field
	{
		string api_xml_field_initialized_w_value = 
			@"
			<field 
				visibility=""public"" static=""true"" 
				type=""java.lang.String"" type-generic-aware=""java.lang.String"" 
				name=""APPLICATION_ID"" %VALUE% 
				deprecated=""not deprecated"" final=""true""  transient=""false"" volatile=""false""
				>
			</field>
			";

		string api_xml_field_wo_value = 
			@"
			<field 
				visibility=""public"" static=""true"" 
				type=""java.lang.String"" type-generic-aware=""java.lang.String"" 
				name=""APPLICATION_ID"" 
				deprecated=""not deprecated"" final=""true""  transient=""false"" volatile=""false""
				>
			</field>
			";
		string api_xml_field_value = @"value=""&quot;io.realm&quot;""";

		public Field ()
		{
		}
	}
}

