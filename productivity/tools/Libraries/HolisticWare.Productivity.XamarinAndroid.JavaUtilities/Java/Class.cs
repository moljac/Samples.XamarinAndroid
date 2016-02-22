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
	
		public Package Package
		{
			get;
			set;
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

        public static Class ParseFullyQualifiedClassName(string classname_fq)
        {
            int idx = classname_fq.LastIndexOf('.');
            string package_name = classname_fq.Substring(0,idx+1);
            Package package = new Package(package_name);    
            Class c = new Class()
            {
                Package = package
            };

            return c;
        } 

		public void ParseClassLine(string line)
		{
			string tmp = line;
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

				this.ParseFullyQualifiedClassname(classname);
				Class c_base = Class.ParseFullyQualifiedClassName(classname_super_base);
				this.Base = c_base;
			}

			return;
		}

		public void ParseFullyQualifiedClassname(string classname_fully_qualified)
		{
			string tmp = classname_fully_qualified;
			string[] parts = tmp.Split (new string[]{ "." }, StringSplitOptions.RemoveEmptyEntries);

			Package p = new Package (parts);

			return;
		}
  	}
}

