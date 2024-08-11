using MongoDB.Driver;

public class TutorScheduleService
{
    private readonly IMongoCollection<TutorSchedule> _tutorSchedules;

    public TutorScheduleService(IMongoDatabase database)
    {
        _tutorSchedules = database.GetCollection<TutorSchedule>("TutorSchedules");
    }

    public async Task<TutorSchedule> ScheduleTutorAsync(TutorSchedule tutorSchedule)
    {
        await _tutorSchedules.InsertOneAsync(tutorSchedule);
        return tutorSchedule;
    }

    public async Task<List<TutorSchedule>> GetAllTutorSchedulesAsync()
    {
        return await _tutorSchedules.Find(ts => true).ToListAsync();
    }

    public async Task<List<TutorSchedule>> GetSchedulesByUsernameAsync(string username)
    {
        var filter = Builders<TutorSchedule>.Filter.Eq(ts => ts.username, username);
        return await _tutorSchedules.Find(filter).ToListAsync();
    }

    public async Task<bool> AddUserToScheduleAsync(string eventId, string username)
    {
        var filter = Builders<TutorSchedule>.Filter.Eq(ts => ts.eventId, eventId);
        var update = Builders<TutorSchedule>.Update.AddToSet(ts => ts.Users, username);
        var result = await _tutorSchedules.UpdateOneAsync(filter, update);
        return result.ModifiedCount > 0;
    }

    public async Task<bool> RemoveUserFromScheduleAsync(string eventId, string username)
    {
        var filter = Builders<TutorSchedule>.Filter.Eq(ts => ts.eventId, eventId);
        var update = Builders<TutorSchedule>.Update.Pull(ts => ts.Users, username);
        var result = await _tutorSchedules.UpdateOneAsync(filter, update);
        return result.ModifiedCount > 0;
    }

    public async Task<bool> DeleteTutorScheduleAsync(string eventId)
    {
        var filter = Builders<TutorSchedule>.Filter.Eq(ts => ts.eventId, eventId);
        var result = await _tutorSchedules.DeleteOneAsync(filter);
        return result.DeletedCount > 0;
    }
}
