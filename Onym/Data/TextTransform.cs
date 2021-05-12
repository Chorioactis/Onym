using System;
using System.Text.RegularExpressions;
using UnidecodeSharpCore;

namespace Onym.Data
{
    public class TextTransform
    {
        /* CLASS VARIABLES */
        private readonly Random _random = new Random();
        
        /* SLUGIFY */
        public string Slugify(string str)
        {
            str = str.ToLower();
            str = str.Unidecode();
            str = Regex.Replace(str, @"[^a-z0-9\s-]", ""); // invalid chars          
            str = Regex.Replace(str, @"\s+", " ").Trim(); // convert multiple spaces into one space  
            str = str.Substring(0, str.Length <= 45 ? str.Length : 45).Trim(); // cut and trim it  
            str = Regex.Replace(str, @"\s", "-"); // replace spaces with '-' 
            return str + "-" + _random.Next();
        }
        
        /* FIRST LETTER TO UPPER */
        public string FirstLetterToUpper(string str)
        {
            if (str.Length > 1) return char.ToUpper(str[0]) + str.Substring(1);
            return str.ToUpper();
        }
        
        /* CHECK TAG */
        public string CheckTag(string str)
        {
            if (str == null) return null;
            str = Regex.Replace(str, @"[^A-Za-zА-Яа-яЁё\d\s-]", "").Trim(); // invalid chars ;
            var charPosition = 0;
            if (str.Length > 1)
            {
                while (str[charPosition] == '-')
                {
                    str = str.Substring(charPosition + 1);
                    charPosition++;
                }
            }
            else if(str == "-")
            {
                return null;
            }

            str = Regex.Replace(str, @"\s+", " ").Trim(); // multiple spaces;
            str = Regex.Replace(str, @"-+", " ").Trim(); // multiple '-' ;
            str = FirstLetterToUpper(str);
            return str == "" ? null : str;
        }
    }
}