using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Moka.Lang
{
	public partial class SyntaxElement
	{
		public SyntaxElement ()
		{
		}

		public string Name
		{
			get;
			set;
		}

		public string NameFullyQualified
		{
			get;
			set;
		}

		public string[] NameFullyQualifiedParts
		{
			get;
			set;
		}

		public string Visibility
		{
			get;
			set;
		}


		public Package Package
		 {
			get;
			set;
		}

		//-------------------------------------------------------------------------
		# region  Property string JavaPOutput w Event pre and post
		/// <summary>
		/// JavaPOutput
		/// </summary>
		public
			string
			JavaPOutput
		{
			get
			{
				return javap_output;
			} // JavaPOutput.get
			set
			{
				// for multi threading apps uncomment lines beginnig with //MT:
				//if (javap_output != value)		// do not write if equivalent/equal/same
				{
					// raise/trigger Event if somebody has subscribed to the event
					if (null != JavaPOutputChanging)
					{
						// raise/trigger Event
						JavaPOutputChanging(this, new EventArgs());
					}

					//MT: lock(javap_output) // MultiThread safe
					{
						// Set the property value
						javap_output = value;
						// raise/trigger Event if somebody has subscribed to the event
						if (null != JavaPOutputChanged)
						{
							// raise/trigger Event
							JavaPOutputChanged(this, new EventArgs());
						}
					}

					// raise/trigger Event if somebody has subscribed to the event
					if (null != JavaPOutputChangePerformed)
					{
						// raise/trigger Event
						JavaPOutputChangePerformed(this, new EventArgs());
					}
				}

				return;
			} // JavaPOutput.set
		} // JavaPOutput

		/// <summary>
		/// private member field for holding JavaPOutput data
		/// </summary>
		private
			string
			javap_output
			;

		///<summary>
		/// Event for wiring BusinessLogic object changes and presentation
		/// layer notifications.
		/// JavaPOutputChanged (<propertyname>Changed) is intercepted by Windows Forms
		/// 1.x and 2.0 event dispatcher 
		/// and for some CLR types by WPF event dispatcher 
		/// usually INotifyPropertyChanged and PropertyChanged event
		///</summary>
		public
			event
			EventHandler
			JavaPOutputChanged
			;

		///<summary>
		/// Event for wiring BusinessLogic object changes and presentation
		/// layer notifications.
		/// Use this event to catch start of the property before it changes
		///</summary>
		public
			event
			EventHandler    //Core.Globals.PropertyChangedEventHandler
			JavaPOutputChanging
			;

		///<summary>
		/// Event for wiring BusinessLogic object changes and presentation
		/// layer notifications.
		/// Use this event to catch end of the property after it changes
		///</summary>
		public
			event
			EventHandler    //Core.Globals.PropertyChangedEventHandler
			JavaPOutputChangePerformed
			;
		# endregion Property string JavaPOutput w Event pre and post
		//-------------------------------------------------------------------------

		public string JavaPCompiledFrom
		{
			get;
			set;
		}

		public static Moka.Lang.SyntaxElement ParseJarTFOutput (string java_syntax_element)
		{
			string name_fully_qualified = java_syntax_element;
			string[] name_parts = java_syntax_element.Split
														(
															new String[] 
									                      		{
																	"/"
																},
										                      StringSplitOptions.RemoveEmptyEntries
														);
			int position_class = name_parts.Length - 1;
			string name = name_parts [position_class];
			name = name.Replace (".class", "");
			string[] name_parts_package = new string[position_class];
			Array.Copy (name_parts, name_parts_package, position_class);
			string name_package = string.Join (".", name_parts_package, 0, position_class);

			SyntaxElement se = null;
			if (java_syntax_element.Contains (@".class"))
			{
				se = new Class () {
					Name = name,
					NameFullyQualified = name_fully_qualified,
					NameFullyQualifiedParts = name_parts,
					Package = new Package () {
						Name = name_package,
						NameFullyQualified = name_package,
						NameFullyQualifiedParts = name_parts_package
					}
				};
			}
			else
			{
			}

			return se;
		}

		protected string[] javap_output_lines = null;
		protected Task task_split_lines = null;


		protected async void Class_JavaPOutputChanged(object sender, EventArgs e)
		{
			javap_output_lines = await ParseJavaPOutputSplitLines(this.JavaPOutput);
			string compiled_form = await ParseJavaPOutputCompiledFromJavaClass();
			Dictionary<string, string> d = await ParseJavaPOutputClass();

			return;
		}

		protected async Task<string[]> ParseJavaPOutputSplitLines(string javap_output)
		{
			string[] lines = null;

			task_split_lines = 
					 Task.Run
						(
							() => 
							{
								Console.WriteLine("\t\t javap parsing - ParseJavaPOutputSplitLines");
								lines = this.JavaPOutput.Split
															(
																new String[]
																	{
																		Environment.NewLine
																	},
																StringSplitOptions.RemoveEmptyEntries
															);
								javap_output_lines = lines;
							}
						);

			return lines;
		}

		protected async Task<string> ParseJavaPOutputCompiledFromJavaClass()
		{
			await Task.Run
				(
					() => 
					{
						Console.WriteLine("\t\t javap parsing - ParseJavaPOutputCompiledFromJavaClass");
						task_split_lines.Wait();
						string line = javap_output_lines[0];
						int i1 = @"Compiled from """.Length;
						int i2 = line.IndexOf(@".java", i1);
						int length = i2 - i1;
						this.JavaPCompiledFrom = line.Substring(i1, length) + @".java";

						return;
					}
				);

			return this.JavaPCompiledFrom;
		}

		protected async Task<Dictionary<string, string>> ParseJavaPOutputClass()
		{
			Dictionary<string, string> d = null;

			await Task.Run
				(
					() => 
					{
						Console.WriteLine("\t\t javap parsing - ParseJavaPOutputClass");
						task_split_lines.Wait();
						string line = javap_output_lines[1];
						string[] tokens = line.Split
										(
											new String[]
												{
													" "
												},
											StringSplitOptions.RemoveEmptyEntries
										);

						d = new Dictionary<string, string>();
						if (
								tokens[2] == "{"	// just to be sure
								&&
								tokens[0] == "class"
							)
						{	
							d.Add(tokens[0], tokens[1]);
						}

						return;
					}
				);

			return d;
		}


	}
}

