using System;
using System.ComponentModel.DataAnnotations;

namespace MinimalAPI.Model;

    public class BankRequest
    {

    [Key]
    public string Iban { get; set; } = string.Empty;

}


