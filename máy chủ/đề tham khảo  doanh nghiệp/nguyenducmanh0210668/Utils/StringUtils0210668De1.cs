using System.Linq;
using System.Text;
using System.Globalization;

namespace nguyenducmanh0210668.Utils
{
    public static class StringUtils0210668De1
    {
        public static string RemoveDiacritics(string text)
        {
            if (string.IsNullOrWhiteSpace(text)) return text;
            text = text.Normalize(NormalizationForm.FormD);
            var chars = text.Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark).ToArray();
            return new string(chars).Normalize(NormalizationForm.FormC);
        }
    }
}
