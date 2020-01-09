using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP.NET_CoreTicTacToe.Models
{
    public class Farm
    {
        private Dictionary<int, History> histories = new Dictionary<int, History>();

        public Dictionary<int, History> Histories
        {
            get
            {
                return histories;
            }
            set
            {
                histories = value;
            }
        }

        int GetNewId(int? id)
        {
            if (id == null)
            {
                if (histories.Count > 0)
                {
                    return histories.Keys.Max() + 1;
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return id.Value;
            }
        }

        public (int, History) FindHistory(int? id) 
        {
            if (!id.HasValue || !histories.TryGetValue(id.Value, out History foundHistory)) 
            {
                var newHistory = new History();

                int newId = GetNewId(id);
                
                histories.Add(newId, newHistory);
                return (newId, newHistory);
            }
            
            return (id.Value, foundHistory);
        }
    }
}
