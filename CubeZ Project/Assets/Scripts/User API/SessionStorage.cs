using System.Collections.Generic;

namespace CBZ.API
{
    public static class SessionStorage
    {
        public static int Count { get => GetVaritablesContainer().Count; }
        public static void AddValue(string nameVaritable, object value)
        {
            var vars = GetVaritablesContainer();


            if (!vars.ContainsKey(nameVaritable))
            {
                vars.Add(nameVaritable, value);
            }
        }

        public static bool TryAddValue(string nameVaritable, object value)
        {
            var vars = GetVaritablesContainer();


            if (!vars.ContainsKey(nameVaritable))
            {
                vars.Add(nameVaritable, value);
                return true;
            }

            return false;
        }

        public static void RemoveValue (string nameVaritable)
        {
            var vars = GetVaritablesContainer();


            if (vars.ContainsKey(nameVaritable))
            {
                vars.Remove(nameVaritable);
            }
        }

        public static object GetValue (string nameVaritable)
        {

            var vars = GetVaritablesContainer();


            if (vars.ContainsKey(nameVaritable))
            {
                return vars[nameVaritable];
            }

            return null;
        }

        public static void SetValue(string nameVaritable, object value)
        {

            var vars = GetVaritablesContainer();


            if (vars.ContainsKey(nameVaritable))
            {
                 vars[nameVaritable] = value;
            }
        }

        public static bool TryRemoveValue(string nameVaritable)
        {
            var vars = GetVaritablesContainer();


            if (vars.ContainsKey(nameVaritable))
            {
                vars.Remove(nameVaritable);
                return true;
            }


            return false;
        }

        public static bool ContainsValue (string nameVaritable)
        {
            var vars = GetVaritablesContainer();


            return vars.ContainsKey(nameVaritable);
        }

        public static void Clear ()
        {
            var vars = GetVaritablesContainer();
            vars.Clear();
        }

        private static Dictionary<string, object> GetVaritablesContainer ()
        {
            return GameCacheManager.gameCache.pythonSource.varitables;
        }
    }
}