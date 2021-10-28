﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoListWForms.Helpers
{
    public static class TextEditor
    {
        public static bool IsWordInArray(string[] array, string word)
        {
            return array.Any(a => a == word);
        }

        public static string ReduceRowToN(string row, int n)
        {
            if (row.Length < n) return row;
            return $"{row.Substring(0, n)}...";
        }
    }
}
