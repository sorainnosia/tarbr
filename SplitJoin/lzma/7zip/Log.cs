﻿namespace master._7zip
{
    internal static class Log
    {
        private static Stack<string> _indent = new Stack<string>();
        private static bool _needsIndent = true;

        static Log()
        {
            _indent.Push("");
        }

        public static void PushIndent(string indent = "  ")
        {
            _indent.Push(_indent.Peek() + indent);
        }

        public static void PopIndent()
        {
            if (_indent.Count == 1)
                throw new InvalidOperationException();

            _indent.Pop();
        }

        private static void EnsureIndent()
        {
            if (_needsIndent)
            {
                _needsIndent = false;
                //System.Diagnostics.Debug.Write(_indent.Peek());
            }
        }

        public static void Write(object value)
        {
            EnsureIndent();
            //System.Diagnostics.Debug.Write(value);
        }

        public static void Write(string text)
        {
            EnsureIndent();
            //System.Diagnostics.Debug.Write(text);
        }

        public static void Write(string format, params object[] args)
        {
            EnsureIndent();
            //System.Diagnostics.Debug.Write(string.Format(format, args));
        }

        public static void WriteLine()
        {
            //System.Diagnostics.Debug.WriteLine("");
            _needsIndent = true;
        }

        public static void WriteLine(object value)
        {
            EnsureIndent();
            //System.Diagnostics.Debug.WriteLine(value);
            _needsIndent = true;
        }

        public static void WriteLine(string text)
        {
            EnsureIndent();
            //System.Diagnostics.Debug.WriteLine(text);
            _needsIndent = true;
        }

        public static void WriteLine(string format, params object[] args)
        {
            EnsureIndent();
            //System.Diagnostics.Debug.WriteLine(string.Format(format, args));
            _needsIndent = true;
        }
    }
}
