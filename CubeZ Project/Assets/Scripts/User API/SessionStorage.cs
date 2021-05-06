using System.Collections.Generic;

namespace CBZ.API
{
    public static class SessionStorage
    {
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

        public static bool ContainsVaritable (string nameVaritable)
        {
            var vars = GetVaritablesContainer();


            return vars.ContainsKey(nameVaritable);
        }

        private static Dictionary<string, object> GetVaritablesContainer ()
        {
            return GameCacheManager.gameCache.pythonSource.varitables;
        }
    }
}