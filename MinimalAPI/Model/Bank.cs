using System;
using System.ComponentModel.DataAnnotations;

namespace MinimalAPI.Model;

public class Bank
{
    [Key]
    public string Code { get; set; } = string.Empty;

    public string BankName { get; set; } = string.Empty;

}


