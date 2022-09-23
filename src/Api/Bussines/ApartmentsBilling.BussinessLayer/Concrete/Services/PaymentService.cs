using ApartmentsBilling.BussinessLayer.Abstract.InterFaces;
using ApartmentsBilling.BussinessLayer.Configuration.Authorize;
using ApartmentsBilling.BussinessLayer.Configuration.Exceptions;
using ApartmentsBilling.Common.Dtos.CustomDto;
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
        private readonly IUserRepository _userRepository;
        private readonly IAuthHorize _authHorize;
        readonly HttpClient client = new();
        private readonly IMapper _mapper;

        public PaymentService(IBillRepository billRepository, IMapper mapper, IAuthHorize authHorize, IUserRepository userRepository)
        {
            _billRepository = billRepository;
            client.BaseAddress = new Uri("https://localhost:44382/api/");
            _mapper = mapper;
            _authHorize = authHorize;
            _userRepository = userRepository;
        }

        public async Task CreatePayment(BillPaymentDto billPaymentDto)
        {
            var value = await _billRepository.GetSingleWtihInclude(x => x.Id == billPaymentDto.BillId, true, true, x => x.Flat.User, x => x.BillType);
            if (value.FlatId != _userRepository.GetSingleWtihInclude(x => x.Id == Guid.Parse(_authHorize.User().Id), includes: x => x.Flat).Result.FlatId)
                throw new Exception("Böyle Bir Fatura Bulunmamaktadır");
            if (value.IsPayment)
                throw new ClientSideException("Bu Fatura Daha Önce Ödenmiş");
            PaymentDto dto = new()
            {
                BillPaymentDto = billPaymentDto,
                CreateBillPaymentDto = _mapper.Map<CreateBillReceiptDto>(value)
            };
            var response = await client.PostAsJsonAsync("Receipt", dto);

            if (!response.IsSuccessStatusCode)
                throw new Exception("Ödeme Başarısız Oldu Lütfen Tekrar deneyin!");
            value.IsPayment = true;
            _billRepository.Update(value);
            await _billRepository.SaveChangeAsync();
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

        public async Task<GetReceiptDto> GetSingleAsync(string id)
        {
            var response = await client.GetFromJsonAsync<GetReceiptDto>(client.BaseAddress + $"Receipt/{id}");
            if (response == null)
                throw new NotFoundException("Böyle Bir Ödenmiş Fatura Bulunamadı");
            return response;
        }
        public async Task RemoveAsync(string id)
        {
            var response = await client.DeleteAsync(client.BaseAddress + $"Receipt/{id}");
            if (!response.IsSuccessStatusCode)
            {
                var res = await response.Content.ReadFromJsonAsync<CustomResponseDto<NoContent>>();
                throw new Exception(string.Join("\n", res.Errors));
            }
        }
    }
}
