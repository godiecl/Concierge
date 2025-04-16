using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Concierge.Models;

/// <summary>
/// Entity: Persona
/// </summary>
[Table("Personas")]
public class Persona
{
    /// <summary>
    /// Unique Identifier for the persona.
    /// </summary>
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    /// <summary>
    /// The RUT.
    /// </summary>
    [Required]
    [StringLength(10, MinimumLength = 10)]
    [Column("Rut")]
    public required string Rut { get; set; }
    
    /// <summary>
    /// The Email.
    /// </summary>
    [Required]
    [EmailAddress] // TODO: Review the email validation
    [Column("Email")]
    public required string Email { get; set; }

    /// <summary>
    /// The Name.
    /// </summary>
    [Required]
    [StringLength(255, MinimumLength = 2)]
    [Column("Nombre")]
    public required string Nombre { get; set; }

    /// <summary>
    /// The Apellidos.
    /// </summary>
    [Required]
    [StringLength(255, MinimumLength = 2)]
    [Column("Apellidos")]
    public required string Apellidos { get; set; }

    /// <summary>
    /// Return the full name of the Persona.
    /// </summary>
    [NotMapped]
    public string NombreCompleto => $"{Nombre} {Apellidos} ({Rut})";

    /// <summary>
    /// String representation of the Persona object.
    /// </summary>
    public override string ToString() => $"ID: {Id}, Nombre: {NombreCompleto}";
}