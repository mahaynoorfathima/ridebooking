using Microsoft.Extensions.Options;
using MongoDB.Driver;
using RIDEAPI.Model;

namespace RIDEAPI.Services
{
    public class RideServices
    {
        private readonly IMongoCollection<Rides> ridecollection;
        public RideServices(IOptions<DBSettings> dbSettings)
        {
            var foodConnection = new MongoClient(dbSettings.Value.ConnectionString);
            var foodb = foodConnection.GetDatabase(dbSettings.Value.DatabaseName);
            ridecollection = foodb.GetCollection<Rides>(dbSettings.Value.CollectionName);
        }

        //public async Task<List<Foods>> GetAsync()
        //    => await foodcollection.Find(_ => true).ToListAsync();


        public async Task<List<Rides>> GettheRide()
        {
            return await ridecollection.Find(_ => true).ToListAsync();
        }

        public async Task InsertRideDetails(Rides rides)
        {
            await ridecollection.InsertOneAsync(rides);
        }

        public async Task<Rides> GetById(string id)
        {
            return await ridecollection.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task UpdateRideDetails(string id, Rides ride)
        {
            await ridecollection.ReplaceOneAsync(x => x.Id == id, ride);
        }

        public async Task DeleteRideDetails(string id)
        {
            await ridecollection.DeleteOneAsync(x => x.Id == id);
        }
    }
}
