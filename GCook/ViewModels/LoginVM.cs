using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace GCook.ViewModels;

public class LoginVM
{
    [Display(Name = "E-mail", Prompt = "seu@email.com")]
    [Required(ErrorMessage = "O e-mail é obrigatório ")]
    [EmailAddress(ErrorMessage = "informe um e-mail valido")]
    public string Email { get; set; }

    [Display(Name = "Senha", Prompt = "************")]
    [Required(ErrorMessage = "A senha é obrigatória")]
    [DataType(DataType.Password)]
    public string Senha { get; set; }

    [Display(Name = "Manter Conectado")]
    public bool Lembrar { get; set; }

    [HiddenInput]
    public string UrlRetorno { get; set; }
}
