using Blazored.Toast.Services;
using JwtProjeto.Models.Models;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;

namespace JwtProjeto.Web.Components.Pages.Product
{
    public partial class UpdateProduct : ComponentBase
    {
        [Parameter]
        public int ID { get; set; }
        public ProductModel Model { get; set; } = new();
        [Inject]
        private ApiClient apiClient { get; set; }
        [Inject]
        public ApiClient ApiClient { get; set; }
        [Inject]
        private IToastService toastService { get; set; }
        [Inject]
        private NavigationManager navigationManager { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            var res = await apiClient.GetFromJsonAsync<BaseResponseModel>($"/api/Product/{ID}");
            if (res is not null && res.Success)
            {
                Model = JsonConvert.DeserializeObject<ProductModel>(res.Data.ToString());
            }
        }

        public async Task Submit()
        {

            var res = await ApiClient.PutAsync<BaseResponseModel, ProductModel>("/api/Product", Model);

            if (res is not null && res.Success)
            {
                toastService.ShowSuccess("Create product successfully");
                navigationManager.NavigateTo("/product");
            }

        }



    }
}