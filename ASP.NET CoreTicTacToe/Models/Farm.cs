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

        public (int, History) FindHistory(int? id) 
        {
            if (!id.HasValue || !histories.TryGetValue(id.Value, out History foundHistory)) 
            {
                var newHistory = new History();
                
                int newId;
                if (id == null)
                {
                    if (histories.Count > 0)
                    {
                        newId = histories.Keys.Max() + 1;
                    }
                    else
                    {
                        newId = 0;
                    }
                }
                else
                {
                    newId = id.Value;  
                }
                histories.Add(newId, newHistory);
                return (newId, newHistory);
            }
            
            return (id.Value, foundHistory);
        }
    }
}
