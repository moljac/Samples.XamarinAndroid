using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moka.Lang
{
	public partial class Class : SyntaxElement
	{
		public Class()
		{
			this.JavaPOutputChanged += Class_JavaPOutputChanged;

			return;
		}
	
        public Class(string javap_output_class)
            : this()
        {
            string tmp = javap_output_class;
            tmp = tmp.Replace("class ", "");
            tmp = tmp.Replace(" {", "");

            if (tmp.Contains("public"))
            {
                AccessModifier = "public";
                tmp = tmp.Replace("public ", "");  
            }
            if (tmp.Contains("protected"))
            {
                AccessModifier = "protected";
                tmp = tmp.Replace("protected ", "");  
            }
            if (tmp.Contains("private"))
            {
                AccessModifier = "private";
                tmp = tmp.Replace("private ", "");  
            }

            if (tmp.Contains("static"))
            {
                Static = true;
                tmp = tmp.Replace("static ", "");  
            }
            if (tmp.Contains("abstract"))
            {
                Abstract = true;
                tmp = tmp.Replace("abstract ", "");  
            }

            string[] parts_implements = null;
            if (tmp.Contains("implements"))
            {
                parts_implements = tmp.Split
                                        (
                                            new string[]{" implements "}, 
                                            StringSplitOptions.RemoveEmptyEntries
                                        );  
                tmp = parts_implements[0];
            }

            string[] parts_extends = null;
            string classname = null;
            string classname_super_base = null;
            if (tmp.Contains("extends"))
            {
                parts_extends = tmp.Split
                                        (
                                            new string[] {" extends "}, 
                                            StringSplitOptions.RemoveEmptyEntries
                                        );  
                classname = parts_extends[0];
                classname_super_base = parts_extends[1];
            }

            return;
        }

        public string AccessModifier
        {
            get;
            set;
        }

        public bool Static
        {
            get;
            set;
        }

        public bool Abstract
        {
            get;
            set;
        }

        public Class Base
        {
            get;
            set;
        }

		public List<Method> Methods
		{
			get;
			set;
		}

		public List<Field> Fields
		{
			get;
			set;
		}
	}
}

