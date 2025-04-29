using Concierge.Data;
using Concierge.Models;
using Microsoft.EntityFrameworkCore;

namespace Concierge.Repositories;

/// <summary>
/// Repository: Persona.
/// </summary>
public class PersonaRepository(AppDbContext context) : IPersonaRepository
{
    /// <summary>
    /// The Connection to the database.
    /// </summary>
    private readonly AppDbContext _context = context;

    public async Task<Persona> Add(Persona persona)
    {
        await _context.Personas.AddAsync(persona);
        await _context.SaveChangesAsync();
        return persona;
    }

    public async Task<IEnumerable<Persona>> GetAll()
    {
        return await _context.Personas.ToListAsync();
    }

    public async Task<Persona?> GetById(int id)
    {
        return await _context.Personas.FindAsync(id);
    }

    public async Task<Persona?> GetByEmail(string email)
    {
        return await _context.Personas.FirstOrDefaultAsync(p => p.Email == email);
    }
}