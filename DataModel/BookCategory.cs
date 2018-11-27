using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public class BookCategory
    {
        public enum BookCategories {
            [Description("Computer Science")]
            Computer_science = 1,
            [Description("Philosophy And Psychology")]
            Philosophy_and_psychology = 2,
            [Description("Religion")]
            Religion = 3,
            [Description("Social Science")]
            Social_Sciences = 4,
            [Description("Language")]
            Language = 5,
            [Description("Science")]
            Science = 6,
            [Description("Technology and Applied Science")]
            Technology_and_applied_science = 7,
            [Description("Arts and Recreation")]
            Arts_and_recreation = 8,
            [Description("Literature")]
            Literature = 9,
            [Description("Fiction")]
            Fiction = 10,
            [Description("Other")]
            Other = 11
        }
    }
    public class Enumreations
    {
        public static string GetEnumDescription(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(
                typeof(DescriptionAttribute),
                false);

            if (attributes != null &&
                attributes.Length > 0)
                return attributes[0].Description;
            else
                return value.ToString();
        }
    }
}
