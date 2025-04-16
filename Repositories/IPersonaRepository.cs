using Concierge.Models;

namespace Concierge.Repositories;

/// <summary>
/// IRepository: Persona.
/// </summary>
public interface IPersonaRepository
{
    /// <summary>
    /// Add a Persona into the Repository..
    /// </summary>
    Task<Persona> Add(Persona persona);
    
    /// <summary>
    /// Retrieve all the Personas from the Repository.
    /// </summary>
    Task<IEnumerable<Persona>> GetAll();
    
    /// <summary>
    /// Retrieve a Persona by its ID.
    /// </summary>
    Task<Persona?> GetById(int id);

    /// <summary>
    /// Retrieve a Persona by its email.
    /// </summary>
    Task<Persona?> GetByEmail(string email);
}