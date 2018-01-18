using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MittoAgSMS.BusinessLogic.Validators
{
    public class PhoneNumberValidator : IValidator<string>
    {
        private string _input;
        public void SetModel(string input)
        {
            _input = input;
        }

        public bool Validate()
        {
            Regex re = new Regex(@"^\+?(\d[\d-. ]+)?(\([\d-. ]+\))?[\d-. ]+\d$");
            var result = re.Match(_input);
            return result.Success;
        }
    }
}
