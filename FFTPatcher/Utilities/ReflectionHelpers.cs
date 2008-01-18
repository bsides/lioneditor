/*
    Copyright 2007, Joe Davidson <joedavidson@gmail.com>

    This file is part of FFTPatcher.

    FFTPatcher is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    FFTPatcher is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with FFTPatcher.  If not, see <http://www.gnu.org/licenses/>.
*/

using System;
using System.Reflection;

namespace FFTPatcher
{
    public static class ReflectionHelpers
    {
        public static bool GetFlag( object o, string name )
        {
            return GetFieldOrProperty<bool>( o, name );
        }

        public static void SetFlag( object o, string name, bool newValue )
        {
            SetFieldOrProperty( o, name, newValue );
        }

        public static T GetFieldOrProperty<T>( object target, string name )
        {
            PropertyInfo pi = target.GetType().GetProperty( name );
            FieldInfo fi = target.GetType().GetField( name );

            if( pi != null )
            {
                return (T)pi.GetValue( target, null );
            }
            else if( fi != null )
            {
                return (T)fi.GetValue( target );
            }
            else
            {
                throw new ArgumentException();
            }
        }

        public static T[] GetFieldsOrProperties<T>( object target, string[] names )
        {
            T[] result = new T[names.Length];
            for( int i = 0; i < names.Length; i++ )
            {
                result[i] = GetFieldOrProperty<T>( target, names[i] );
            }

            return result;
        }

        public static void SetFieldOrProperty( object target, string name, object newValue )
        {
            PropertyInfo pi = target.GetType().GetProperty( name );
            FieldInfo fi = target.GetType().GetField( name );
            if( pi != null )
            {
                pi.SetValue( target, newValue, null );
            }
            else if( fi != null )
            {
                fi.SetValue( target, newValue );
            }
            else
            {
                throw new ArgumentException();
            }
        }
    }
}
