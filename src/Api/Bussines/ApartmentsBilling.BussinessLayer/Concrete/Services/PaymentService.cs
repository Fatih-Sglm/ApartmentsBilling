using ApartmentsBilling.BussinessLayer.Abstract.InterFaces;
using ApartmentsBilling.BussinessLayer.Configuration.Authorize;
using ApartmentsBilling.BussinessLayer.Configuration.Exceptions;
using ApartmentsBilling.Common.Dtos.PaymentDto;
using ApartmentsBilling.DataAccesLayer.Abstract.InterFaces;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ApartmentsBilling.BussinessLayer.Concrete.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IBillRepository _billRepository;
        private readonly IAuthHorize _authHorize;
        readonly HttpClient client = new();
        private readonly IMapper _mapper;

        public PaymentService(IBillRepository billRepository, IMapper mapper, IAuthHorize authHorize)
        {
            _billRepository = billRepository;
            client.BaseAddress = new Uri("https://localhost:44382/api/");
            _mapper = mapper;
            _authHorize = authHorize;
        }

        public async Task CreatePayment(BillPaymentDto billPaymentDto)
        {
            var value = await _billRepository.GetSingleWtihInclude(x => x.Id == billPaymentDto.BillId, true, true, x => x.Flat.User, x => x.BillType);
            if (value.IsPayment)
                throw new ClientSideException("Bu Fatura Daha Önce Ödenmiş");
            PaymentDto dto = new()
            {
                BillPaymentDto = billPaymentDto,
                CreateBillPaymentDto = _mapper.Map<CreateBillReceiptDto>(value)
            };
            var response = await client.PostAsJsonAsync("Receipt", dto);

            if (response.IsSuccessStatusCode)
            {
                value.IsPayment = true;
                _billRepository.Update(value);
            }
            throw new Exception("Ödeme Başarısız Oldu Lütfen Tekrar deneyin!");
        }

        public async Task<List<GetReceiptDto>> GetListAsync()
        {
            List<GetReceiptDto> receiptList;
            if (_authHorize.IsAuthorize())
            {
                receiptList = await client.GetFromJsonAsync<List<GetReceiptDto>>(client.BaseAddress + "Receipt");
            }
            else
            {
                receiptList = await client.GetFromJsonAsync<List<GetReceiptDto>>(client.BaseAddress + $"Receipt/{_authHorize.User().Id}");
            }
            return receiptList;
        }
    }
}
