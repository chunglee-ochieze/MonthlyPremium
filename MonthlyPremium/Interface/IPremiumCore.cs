using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonthlyPremium.Interface
{
    interface IPremiumCore
    {
        decimal DeathPremium(string occupationRating, uint age, decimal coverAmount);
    }
}