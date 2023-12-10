using AutoMapper;
using Vezeeta.Core.Models;
using Vezeeta.Core.Repositories;
using Vezeeta.Services.Models.DTOs;
using Vezeeta.Sevices.Services.Interfaces;

namespace Vezeeta.Sevices.Services
{
    public class DiscountCodeService:IDiscountCodeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DiscountCodeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<bool> AddDiscountCode(DiscountCodeDto newDiscountCode)
        {
            DiscountCode isCodeRegistered = await _unitOfWork.DiscountCodes
                .FindAsync(e => e.Code == newDiscountCode.Code);
            if (isCodeRegistered != null) return false;
            DiscountCode discountCode = _mapper.Map<DiscountCode>(newDiscountCode);
            await _unitOfWork.DiscountCodes.AddAsync(discountCode);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<bool> UpdateDiscountCode(int discoundId, DiscountCodeDto updatedDiscountCode)
        {
            DiscountCode discountCode = await _unitOfWork.DiscountCodes.FindAsync(e => e.DiscountCodeId == discoundId); //get Discount entity
            if (discountCode == null) return false;
            var isApplied = await _unitOfWork.Bookings.FindAsync(e => e.DiscountCodeId == discountCode.DiscountCodeId);
            if (isApplied != null) return false;
            _mapper.Map(updatedDiscountCode, discountCode);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<bool> DeleteDiscountCode(int discoundId)
        {
            DiscountCode discountCode = await _unitOfWork.DiscountCodes.FindAsync(e => e.DiscountCodeId == discoundId); //get Discount entity
            if (discountCode == null) return false;
            var isApplied = await _unitOfWork.Bookings.FindAsync(e => e.DiscountCodeId == discountCode.DiscountCodeId);
            if (isApplied != null) return false;
            _unitOfWork.DiscountCodes.Remove(discountCode);
            await _unitOfWork.CompleteAsync();
            return true;
        }
        public async Task<bool> ActivateDiscountCode(int discoundId)
        {
            DiscountCode discountCode = await _unitOfWork.DiscountCodes.FindAsync(e => e.DiscountCodeId == discoundId); //get Discount entity
            if (discountCode == null || discountCode.IsValid) return false;
            discountCode.IsValid = true;
            await _unitOfWork.CompleteAsync();
            return true;
        }
        public async Task<bool> DeactivateDiscountCode(int discoundId)
        {
            DiscountCode discountCode = await _unitOfWork.DiscountCodes.FindAsync(e => e.DiscountCodeId == discoundId); //get Discount entity
            if (discountCode == null || discountCode.IsValid == false) return false;
            discountCode.IsValid = false;
            await _unitOfWork.CompleteAsync();
            return true;
        }

    }
}
