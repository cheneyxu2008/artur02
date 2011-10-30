using System.Globalization;

namespace TypeInspector
{
    /// <summary>
    /// Initializes types to default values
    /// </summary>
    public class Initializer
    {
        private bool _typeBool = true;
        private int _stringCounter;

        /// <summary>
        /// Initializes type
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public string Initialize(string type)
        {
            switch(type)
            {
                case "System.String":
                    return GetString();
                case "System.Boolean":
                    return GetBool().ToString().ToLowerInvariant();
                default:
                    return string.Empty;

            }
        }

        /// <summary>
        /// Get Booleand default value
        /// </summary>
        /// <param name="invert"></param>
        /// <returns></returns>
        public bool GetBool(bool invert = false)
        {
            var result = _typeBool;
            if(invert)
            {
                _typeBool = !_typeBool;
            }
            return result;
        }

        /// <summary>
        /// Get string default value
        /// </summary>
        /// <returns></returns>
        public string GetString()
        {
            return string.Format(CultureInfo.InvariantCulture, "\"string{0}\"", _stringCounter++);
        }
    }
}
