using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GCook.Models;

[Table("Receitas")]
public class Receita
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "A Categoria é obrgatória")]
    public int CategoriaId { get; set; }
    [ForeignKey(nameof(CategoriaId))]
    public Categoria Categoria { get; set; }

    [Required(ErrorMessage = " O nome é obrigatório")]
    [StringLength(100)]
    public string Nome { get; set; }

    [StringLength(1000)]
    [Display(Name = "Descrição")]
    public string Descricao { get; set; }

    [StringLength(30)]
    [Display(Name = "Tempo de Preparo")]
    public string TempoPreparo { get; set; }

    public int Rendimento { get; set; }

    public Dificuldade Dificuldade { get; set; }

    [StringLength(300)]
    public string Foto { get; set; }

    [Required]
    public string UsuarioId { get; set; }
    [ForeignKey(nameof(UsuarioId))]
    public Usuario Usuario { get; set; }

    public List<Preparo> Preparo { get; set; }

    public List<ReceitaIngrediente> Ingredientes { get; set; }
    
}
