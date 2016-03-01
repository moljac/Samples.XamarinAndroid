using System;

namespace Moka.Lang
{
	public partial class Interface : SyntaxElement
	{
		string api_xml = 
			@"
			<implements 
				name=""com.brightcove.player.event.Component"" 
				name-generic-aware=""com.brightcove.player.event.Component""
				>
			</implements>
			";

				
		public Interface ()
		{
		}


	}
}

