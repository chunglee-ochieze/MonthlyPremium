using MonthlyPremiumModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonthlyPremium.Interface
{
    interface IPremiumCore
    {
        double CalculatePremium(UserDataModel user);
        Task SavePremium(string data);
        Task<List<UserDataModel>> ViewPremiums();
    }
}