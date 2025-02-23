namespace VentasWeb.Services
{
    using MongoDB.Driver;
    using Microsoft.Extensions.Configuration;
    using VentasWeb.Models;

    public class MongoDbService
    {
        private readonly IMongoCollection<Usuario> _usuariosCollection;

        public MongoDbService(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetConnectionString("MongoDbConnection"));
            var database = client.GetDatabase("Ventas_DB");
            _usuariosCollection = database.GetCollection<Usuario>("Usuario");
        }

        public async Task<List<Usuario>> GetAsync() =>
            await _usuariosCollection.Find(_ => true).ToListAsync();

        public async Task<Usuario> GetAsync(string Id) =>
            await _usuariosCollection.Find(x => x.Id == Id).FirstOrDefaultAsync();

        public async Task CreateAsync(Usuario usuario) =>
            await _usuariosCollection.InsertOneAsync(usuario);

        public async Task UpdateAsync(string Id, Usuario usuario) =>
            await _usuariosCollection.ReplaceOneAsync(x => x.Id == Id, usuario);

        public async Task RemoveAsync(string Id) =>
            await _usuariosCollection.DeleteOneAsync(x => x.Id == Id);
    }
}
