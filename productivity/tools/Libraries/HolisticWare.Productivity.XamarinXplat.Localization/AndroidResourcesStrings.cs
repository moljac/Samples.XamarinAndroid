using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HolisticWare.Productivity.XamarinXplat.Localization
{
    public partial class AndroidResourcesStrings : ResourceLocalization
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
        public partial class resources
        {

            private resourcesString[] stringField;

            private resourcesStringarray stringarrayField;

            private resourcesPlurals pluralsField;

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute("string")]
            public resourcesString[] @string
            {
                get
                {
                    return this.stringField;
                }
                set
                {
                    this.stringField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute("string-array")]
            public resourcesStringarray stringarray
            {
                get
                {
                    return this.stringarrayField;
                }
                set
                {
                    this.stringarrayField = value;
                }
            }

            /// <remarks/>
            public resourcesPlurals plurals
            {
                get
                {
                    return this.pluralsField;
                }
                set
                {
                    this.pluralsField = value;
                }
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class resourcesString
        {

            private string bField;

            private string[] textField;

            private string nameField;

            /// <remarks/>
            public string b
            {
                get
                {
                    return this.bField;
                }
                set
                {
                    this.bField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlTextAttribute()]
            public string[] Text
            {
                get
                {
                    return this.textField;
                }
                set
                {
                    this.textField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string name
            {
                get
                {
                    return this.nameField;
                }
                set
                {
                    this.nameField = value;
                }
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class resourcesStringarray
        {

            private string[] itemField;

            private string nameField;

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute("item")]
            public string[] item
            {
                get
                {
                    return this.itemField;
                }
                set
                {
                    this.itemField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string name
            {
                get
                {
                    return this.nameField;
                }
                set
                {
                    this.nameField = value;
                }
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class resourcesPlurals
        {

            private resourcesPluralsItem[] itemField;

            private string nameField;

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute("item")]
            public resourcesPluralsItem[] item
            {
                get
                {
                    return this.itemField;
                }
                set
                {
                    this.itemField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string name
            {
                get
                {
                    return this.nameField;
                }
                set
                {
                    this.nameField = value;
                }
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class resourcesPluralsItem
        {

            private string quantityField;

            private string valueField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string quantity
            {
                get
                {
                    return this.quantityField;
                }
                set
                {
                    this.quantityField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlTextAttribute()]
            public string Value
            {
                get
                {
                    return this.valueField;
                }
                set
                {
                    this.valueField = value;
                }
            }
        }


    }
}
