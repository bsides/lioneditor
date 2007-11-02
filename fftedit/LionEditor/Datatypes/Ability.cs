using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Xml;
using System.Resources;

namespace LionEditor
{
    public struct Ability
    {
        public UInt16 Value;
        public string Name;

        public Ability( UInt16 value )
        {
            Ability? i = AbilityList.Find(
                delegate( Ability j )
                {
                    return j.Value == value;
                } );

            if( i.HasValue )
            {
                this.Value = i.Value.Value;
                this.Name = i.Value.Name;
            }
            else
            {
                this.Value = 0;
                this.Name = string.Empty;
            }
        }

        public override string ToString()
        {
            return this.Name;
        }

        /// <summary>
        /// Converts this Ability into a series of bytes appropriate for putting into a character's struct
        /// </summary>
        /// <remarks>Returned byte[] is little-endian (least significant byte is in 0th index)</remarks>
        public byte[] ToByte()
        {
            return new byte[] { (byte)(Value & 0xFF), (byte)((Value & 0xFF00) >> 8) };
        }


        #region Static members

        private static List<Ability> abilityList;

        public static List<Ability> AbilityList
        {
            get
            {
                if( abilityList == null )
                {
                    XmlDocument d = new XmlDocument();
                    System.Reflection.Assembly a = System.Reflection.Assembly.GetExecutingAssembly();
                    d.Load( a.GetManifestResourceStream( "Abilities" ) );

                    XmlNodeList abilities = d.SelectNodes( "//Ability" );

                    abilityList = new List<Ability>( abilities.Count );

                    foreach( XmlNode i in abilities )
                    {
                        Ability newItem;
                        newItem.Name = i.InnerText;
                        newItem.Value = Convert.ToUInt16( i.Attributes["offset"].InnerText );

                        abilityList.Add( newItem );
                    }
                }

                return abilityList;
            }
        }

        #endregion
    }
}
