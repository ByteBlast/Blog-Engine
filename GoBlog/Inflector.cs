﻿using System.Linq;

namespace GoBlog
{
    public static class Inflector
    {
        public static string CapitalizeFirstLetter(this string word)
        {
            return word.First().ToString().ToUpper() + string.Join("", word.Skip(1));
        }
    }
}