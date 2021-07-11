using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonthlyPremium.Interface
{
    interface IPremiumCore
    {
        double DeathPremium(string occupationRating, double age, double coverAmount);
    }
}