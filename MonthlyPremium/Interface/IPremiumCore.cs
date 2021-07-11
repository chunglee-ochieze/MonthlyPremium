using MonthlyPremiumModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonthlyPremium.Interface
{
    interface IPremiumCore
    {
        double CalculateSavePremium(UserDataModel user);
    }
}