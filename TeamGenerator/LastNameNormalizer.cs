namespace TeamGenerator
{
    internal static class LastNameNormalizer
    {
        public static string LastNameNormalize(this string s)
        {
            return s.Replace("'","").Replace("’","");
        }
    }
}
