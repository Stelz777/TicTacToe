using System;
using System.ComponentModel.DataAnnotations;

namespace ASP.NET_CoreTicTacToe.Models
{
    //TODO создать entity (DTO)
    //Automapper
    //Сериализация в json состояния доски
    //Затем сделать gameAPI, как на github

    public class Turn
    {
        public int CellNumber { get; set; }
        public Side WhichTurn { get; set; } 

        [Key]
        public Guid ID { get; set; }
    }
}
