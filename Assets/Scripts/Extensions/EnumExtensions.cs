using System;

namespace Extensions.System.Enumerations
{
    public static class EnumExtensions
    {
        public static int ItemCount(this Enum value)
        {
            return Enum.GetNames(value.GetType()).Length;
        }
        
        public static T RandomItem<T>()
        {
            Random r = new Random();
            Array enumValues = Enum.GetValues(typeof(T));
            return (T)enumValues.GetValue(r.Next(enumValues.Length));
        }
        
        public static T ClearFlag<T>(this Enum variable, T flag)
        {
            return ClearFlags(variable, flag);
        }
        
        public static T ClearFlags<T>(this Enum variable, params T[] flags)
        {
            var result = Convert.ToUInt64(variable);
            foreach (T flag in flags)
                result &= ~Convert.ToUInt64(flag);
            return (T)Enum.Parse(variable.GetType(), result.ToString());
        }
        
        public static T SetFlag<T>(this Enum variable, T flag)
        {
            return SetFlags(variable, flag);
        }
        
        public static T SetFlags<T>(this Enum variable, params T[] flags)
        {
            var result = Convert.ToUInt64(variable);
            foreach (T flag in flags)
                result |= Convert.ToUInt64(flag);
            return (T)Enum.Parse(variable.GetType(), result.ToString());
        }
        
        public static bool HasFlags<E>(this E variable, params E[] flags)
        where E : struct, IComparable, IFormattable, IConvertible
        {
            if (!typeof(E).IsEnum)
                throw new ArgumentException("variable must be an Enum", "variable");
        
            foreach (var flag in flags)
            {
                if (!Enum.IsDefined(typeof(E), flag))
                    return false;
            
                ulong numFlag = Convert.ToUInt64(flag);
                if ((Convert.ToUInt64(variable) & numFlag) != numFlag)
                    return false;
            }
        
            return true;
        }
    }
    
}