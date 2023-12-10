using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vezeeta.Services.Models.DTOs;

namespace Vezeeta.Sevices.Services.Interfaces
{
    public interface IDiscountCodeService
    {
        Task<bool> AddDiscountCode(DiscountCodeDto discountCode);
        Task<bool> UpdateDiscountCode(int discoundId, DiscountCodeDto updatedDiscountCode);
        Task<bool> DeleteDiscountCode(int discoundId);
        Task<bool> ActivateDiscountCode(int discoundId);
        Task<bool> DeactivateDiscountCode(int discoundId);
    }
}
