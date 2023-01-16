namespace DataStructures.Extensions
{
    internal static class StringExtensions
    {
        internal static int GetCharCodeAt(this string content, int position)
        {
            return (int)content[position];
        }
    }
}
