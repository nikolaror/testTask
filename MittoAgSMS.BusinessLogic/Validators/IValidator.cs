using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MittoAgSMS.BusinessLogic.Validators
{
    public interface IValidator<T>
    {
        bool Validate();
        void SetModel(T input);
    }
}
