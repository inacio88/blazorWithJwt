using JwtProjeto.Models.Models;
using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;

namespace JwtProjeto.Web.Components.Pages.Product;

public partial class CreateProduct
{
    [Inject]
    public ApiClient ApiClient { get; set; }
    public ProductModel Model { get; set; } = new();
    [Inject]
    private IToastService toastService { get; set; }
    [Inject]
    private NavigationManager navigationManager { get; set; }

    public async Task Submit()
    {

        var res = await ApiClient.PostAsync<BaseResponseModel, ProductModel>("/api/Product", Model);

        if (res is not null && res.Success)
        {
            toastService.ShowSuccess("Create product successfully");
            navigationManager.NavigateTo("/product");
        }

    }
}