using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Scripts.MainMenuScripts
{
    public class LevelNameValidator
    {
        private const string c_validatePattern = @"^[^0-9][А-яA-z0-9]+$";
        Regex _validateRegex;
        public LevelNameValidator()
        {
            _validateRegex = new Regex(c_validatePattern);
        }

        public bool ValidateInputName(string name)
        {
            if(_validateRegex.IsMatch(name))
            {
                return true;
            }
            else
                return false;
        }
        /// <summary>
        /// true если имя существует, false - нет
        /// </summary>
        /// <param name="name"></param>
        /// <param name="names"></param>
        /// <returns></returns>
        public bool CheckForExist(string name, List<string> names)
        {
            foreach(string savedName in names)
            {
                if(savedName == name)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
