using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeckSorter.Context;
using DeckSorter.Models;
using DeckSorter.Request;
using DeckSorter.Response;
using DeckSorter.Extensions;

namespace DeckSorter.Services
{
    public class ValueService
    {
        public async Task CreateValue(CreateValueRequest request)
        {
            using (var db = new DeckContext())
            {
                var result = new Value();
                var exist = db.Values.FirstOrDefault(x => x.Title.ToLower() == request.Title.ToLower());
                if (exist != null)
                    throw new Exception($"value with title {request.Title} exist");
                db.Values.Add(new Value { Title = request.Title });
                await db.SaveChangesAsync();
            }
        }

        public async Task<Value> EditValue(Value value)
        {
            using (var db = new DeckContext())
            {
                var result = new Value();
                var exist = db.Values.FirstOrDefault(x => x.Id == value.Id);
                if (exist == null)
                    throw new Exception($"value with title {value.Title} not exist");
                if (exist.Title != value.Title)
                {
                    exist.Title = value.Title;
                    await db.SaveChangesAsync();
                }

                return result;
            }
        }

        public async Task DeleteValue(long id)
        {
            using (var db = new DeckContext())
            {
                var result = new Value();
                var exist = await db.Values.FindAsync(id);
                if (exist == null)
                    throw new Exception($"value not exist");
                db.Values.Remove(exist);
                await db.SaveChangesAsync();
            }
        }

        public async Task<List<Value>> GetAllValues()
        {
            using (var db = new DeckContext())
            {
                return db.Values.ToList();
            }
        }

        public async Task<Value> GetValueById(long id)
        {
            using (var db = new DeckContext())
            {
                return await db.Values.FindAsync(id);
            }
        }
    }

    /// <summary>
    /// интерфейс колод
    /// </summary>
    public interface IValueService
    {
        Task CreateValue(CreateValueRequest request);

        Task<Value> EditValue(Value value);

        Task DeleteValue(Value value);

        Task<List<Value>> GetAllValues();

        Task<Value> GetValueById(long id);
    }
}