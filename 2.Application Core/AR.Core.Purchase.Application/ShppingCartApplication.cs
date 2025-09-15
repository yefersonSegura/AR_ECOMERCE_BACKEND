using AR.Common.Dto;
using AR.Core.Purchase.Common;
using AR.Core.Purchase.Common.Dto;
using AR.Core.Purchase.Common.Interfaces;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

namespace AR.Core.Purchase.Application
{
    public class ShppingCartApplication : IShoppingCartApplication
    {
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly IHubContext<ARHub> _hubContext;
        public ShppingCartApplication(IShoppingCartRepository _shoppingCartRepository, IHubContext<ARHub> _hubContext)
        {
            this._shoppingCartRepository = _shoppingCartRepository;
            this._hubContext = _hubContext;
        }
        public async Task<BaseResponseDto> AddProductToShoppingCart(int customerId, int productId, int quantity)
        {
            BaseResponseDto result = new BaseResponseDto();
            try
            {
                result = await _shoppingCartRepository.AddProductToShoppingCart(customerId, productId, quantity);
                var listShoppingCart = await _shoppingCartRepository.GetShoppingCartByCustomer(customerId);
                await _hubContext.Clients.All.SendAsync("refreshShoppingCart", listShoppingCart.Count());
            }
            catch (Exception ex)
            {
                ExceptionDispatchInfo.Capture(new Exception(ex.Message)).Throw();
            }
            return result;
        }

        public ResponseDto<InvoiceDto> calulateCart(InvoiceDto invoice)
        {
            ResponseDto<InvoiceDto> result = new ResponseDto<InvoiceDto>();
            try
            {
                invoice.GrossTotal = 0;
                invoice.TotalDiscount = 0;
                invoice.TotalIGV = 0;
                invoice.TotalGrandPrix = 0;
                foreach (var item in invoice.items)
                {
                    invoice.GrossTotal = invoice.GrossTotal + item.MontoVenta;
                    invoice.TotalDiscount = invoice.TotalDiscount + item.LineDiscount;
                }
                invoice.TotalIGV = invoice.GrossTotal * (0.18m / 100);
                invoice.TotalGrandPrix = invoice.GrossTotal + invoice.TotalIGV;
                invoice.TotalGrandPrix = Math.Round(invoice.TotalGrandPrix, 2);
                result.IsSuccessful = true;
                result.Message = "Cart calculated successfully";
                result.Data = invoice;
            }
            catch (Exception ex)
            {
                ExceptionDispatchInfo.Capture(new Exception(ex.Message)).Throw();
            }
            return result;
        }

        public async Task<ResponseDto<List<ProductDto>>> GetShoppingCartByCustomer(int CustomerId)
        {
            ResponseDto<List<ProductDto>> result = new ResponseDto<List<ProductDto>>();
            try
            {
                result.Data = await _shoppingCartRepository.GetShoppingCartByCustomer(CustomerId);
                result.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                ExceptionDispatchInfo.Capture(new Exception(ex.Message)).Throw();
            }
            return result;
        }

        public async Task<BaseResponseDto> RemoveProductOfShoppingCart(int customerId, int productId)
        {
            BaseResponseDto result = new BaseResponseDto();
            try
            {
                result = await _shoppingCartRepository.RemoveProductOfShoppingCart(customerId, productId);
                var listShoppingCart = await _shoppingCartRepository.GetShoppingCartByCustomer(customerId);
                await _hubContext.Clients.All.SendAsync("refreshShoppingCart", listShoppingCart.Count());
            }
            catch (Exception ex)
            {
                ExceptionDispatchInfo.Capture(new Exception(ex.Message)).Throw();
            }
            return result;
        }
    }
}
