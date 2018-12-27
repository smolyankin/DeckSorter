using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeckSorter.Context;
using DeckSorter.Models;
using DeckSorter.Request;

namespace DeckSorter.Services
{
    /// <summary>
    /// сервис значений
    /// </summary>
    public class ValueService
    {
        /// <summary>
        /// создать значение
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task CreateValue(CreateValueRequest request)
        {
            using (var db = new DeckContext())
            {
                var exist = db.Values.FirstOrDefault(x => x.Title.ToLower() == request.Title.ToLower());
                if (exist != null)
                    throw new Exception($"value with title {request.Title} exist");
                db.Values.Add(new Value { Title = request.Title });
                await db.SaveChangesAsync();
            }
        }

        /// <summary>
        /// изменить значение
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public async Task<Value> EditValue(Value value)
        {
            using (var db = new DeckContext())
            {
                var exist = await db.Values.FindAsync(value.Id);
                if (exist != null && exist.Title != value.Title)
                {
                    exist.Title = value.Title;
                    await db.SaveChangesAsync();
                }

                return exist;
            }
        }

        /// <summary>
        /// удалить значение
        /// </summary>
        /// <param name="id">ид значения</param>
        /// <returns></returns>
        public async Task DeleteValue(long id)
        {
            using (var db = new DeckContext())
            {
                var exist = await db.Values.FindAsync(id);
                if (exist != null)
                {
                    var cards = db.Cards.ToList();
                    foreach (var card in cards)
                        if (card.ValueId == id)
                        {
                            var cardService = new CardService();
                            await cardService.DeleteCard(card.Id);
                        }
                            
                    db.Values.Remove(exist);
                    await db.SaveChangesAsync();
                }
            }
        }

        /// <summary>
        /// получить все значения
        /// </summary>
        /// <returns></returns>
        public async Task<List<Value>> GetAllValues()
        {
            using (var db = new DeckContext())
            {
                return db.Values.OrderBy(x => x.Title).ToList();
            }
        }

        /// <summary>
        /// получить значение по ид
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Value> GetValueById(long id)
        {
            using (var db = new DeckContext())
            {
                return await db.Values.FindAsync(id);
            }
        }
    }
}