using System;

namespace Moka.Lang
{
	public partial class Package : SyntaxElement
	{
		public Package ()
		{
		}

        public Package (string java_class)
        {
         
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

	}
}

