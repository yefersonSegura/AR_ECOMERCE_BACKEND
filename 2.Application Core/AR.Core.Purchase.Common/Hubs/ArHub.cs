using AR.Core.Purchase.Common.Interfaces;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;

namespace AR.Core.Purchase.Common
{
    public class ARHub:Hub
    {
        private readonly IShoppingCartApplication shoppingCartApplication;
        public ARHub(IShoppingCartApplication shoppingCartApplication) { 
            this.shoppingCartApplication = shoppingCartApplication;
        }
        public override async Task OnConnectedAsync()
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, "MapUsers");
            await Clients.Caller.SendAsync("UserConected","Usuario Conectado");
        }
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, "MapUsers");
            await  base.OnDisconnectedAsync(exception);
        }
        public async Task OnNotifyShoppingCart(int CustomerId)
        {
            var result = await shoppingCartApplication.GetShoppingCartByCustomer(CustomerId);
            if (result.IsSuccessful)
            {
                await Clients.All.SendAsync("refreshShoppingCart", result.Data.Count());
            }
        }
    }
}
