using Microsoft.EntityFrameworkCore;
using MinimalAPI.Model;
using MinimalAPI.Data;
using System.Text.Json;
using System.Collections.Generic;
using System.Numerics;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(

    builder.Configuration.GetConnectionString("DefaultConnection"))); 



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.UseHttpsRedirection();

app.MapGet("/", () => "Welcome!!");


app.MapPost("/bank", async (BankRequest bankRequest) =>	   
{

    var trimmedIban = bankRequest.Iban.Replace(" ", "");                                                    //Girilen iban değerinden boşlukları çıkarıyoruz
    var code = trimmedIban.Substring(5, 4);                                                                 //IBAN'da kontrol etmemiz gereken kısmı alıyoruz (6-9)

    var a = app.Environment.ContentRootPath;                                                                //Path'imiz

    var jsonString = File.ReadAllText($"{app.Environment.ContentRootPath}/Json/Banks.json");                //json dosyamızın tam pathini belirtiyoruz

    //---                                Deserialization
    using FileStream openStream = File.OpenRead($"{app.Environment.ContentRootPath}/Json/Banks.json");      //JSON dosyamızı belirtiyoruz ve json dosyamızı read ediyoruz
    var bankList =  JsonSerializer.Deserialize<List<Bank>>(jsonString);                                     //JSON dosyamızı bir listeye dönüştürüyoruz
    //---

    var failState = new FailState();                                                                        //FailState objesi oluşturuyoruz

    if (bankList == null || !bankList.Any())                                                                //Banka listemiz boş mu?
    {
        failState.Message = "Banka listesi bulunamadı!";                                                    //FailState mesajını güncelliyoruz
    return Results.NotFound(failState);                                                                     //Eğer json dosyamızdaki liste boşsa FailState dönüyor
    }

    var requestedBank = bankList.FirstOrDefault(x => x.Code == code);                                       //Kullanıcı tarafından girilen IBAN'ı kıyaslıyoruz

    if (requestedBank == null)
    {
        failState.Message = "Belirtilen ibana ait kayıt bulunamadı!";                                       //FailState mesajını güncelliyoruz
        return Results.NotFound(failState);                                                                 //Eğer öyle bir IBAN kodu kayıtlı değilse hata veriyor
    }
    var successState = new SuccessState();                                                                  //SuccessState objesi oluşturuyoruz

    successState.Data = requestedBank.BankName;                                                             //Data değerini değiştiriyoruz
    return Results.Ok(successState);                                                                        //SuccessState objesini return ediyoruz

});


app.Run();


