using System;
using System.Collections.Generic;
namespace Nop.Services.Chonves
{
    public class CVCommon
    {
        /// <summary>
        /// Loai bo tieng viet -> khong dau
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string convertToUnSign(string s)
        {
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex("\\p{IsCombiningDiacriticalMarks}+");
            string temp = s.Normalize(System.Text.NormalizationForm.FormD);
            return regex.Replace(temp, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
        }
    }
}
