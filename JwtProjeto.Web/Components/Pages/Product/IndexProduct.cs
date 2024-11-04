using Blazored.Toast.Services;
using JwtProjeto.Models.Models;
using JwtProjeto.Web.Components.BaseComponents;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;

namespace JwtProjeto.Web.Components.Pages.Product
{
    public partial class IndexProduct
    {
        [Inject]
        public ApiClient ApiClient { get; set; }
        public List<ProductModel> ProductModels { get; set; }
        public AppModal appModal { get; set; }
        public int DeleteId { get; set; }
        [Inject]
        private IToastService toastService { get; set; }
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            await LoadProduct();
        }

        protected async Task LoadProduct()
        {
            var res = await ApiClient.GetFromJsonAsync<BaseResponseModel>("/api/Product");
            if (res is not null && res.Success)
            {
                ProductModels = JsonConvert.DeserializeObject<List<ProductModel>>(res.Data.ToString());
            }
        }

        protected async Task HandleDelete()
        {
            var res = await ApiClient.DeleteAsync<BaseResponseModel>($"/api/Product/{DeleteId}");
            if (res is not null && res.Success)
            {
                toastService.ShowSuccess("Delete product successfully");
                await LoadProduct();
                appModal.Close();
            }
        }
    }
}