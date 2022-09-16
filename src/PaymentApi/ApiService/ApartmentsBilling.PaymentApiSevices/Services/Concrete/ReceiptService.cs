using ApartmentsBilling.Common.Dtos.PaymentDto;
using ApartmentsBilling.PaymentApiSevices.Entities;
using ApartmentsBilling.PaymentApiSevices.Repositories.Common;
using AutoMapper;
using Stripe;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApartmentsBilling.PaymentApiSevices.Services.Concrete
{
    public class ReceiptService : IReceiptService
    {
        private readonly IGenericRepository<Receipt> _repository;
        private readonly IMapper _mapper;

        public ReceiptService(IGenericRepository<Receipt> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task CreatePayment(PaymentDto paymentDto)
        {
            StripeConfiguration.ApiKey = "sk_test_4eC39HqLyjWDarjtT1zdp7dc";
            var options = new PaymentIntentCreateOptions
            {
                Amount = (long)paymentDto.CreateBillPaymentDto.Total,
                Currency = "usd",
                PaymentMethod = "pm_card_visa",
            };
            var service = new PaymentIntentService();
            service.Create(options);

            Random random = new();
            Receipt receipt = _mapper.Map<Receipt>(paymentDto.CreateBillPaymentDto);
            receipt.PaymentNumber = random.Next(111111, 999999);
            await _repository.CreateAsync(receipt);
        }

        public Task<List<GetReceiptDto>> GetAllReceipt()
        {
            return Task.FromResult(_mapper.Map<List<GetReceiptDto>>(_repository.GetAll()));
        }

        public async Task<GetReceiptDto> GetSingleReceipt(string id)
        {
            return _mapper.Map<GetReceiptDto>(await _repository.Get(x => x.Id == id));
        }

        public async Task Remove(string id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
