﻿namespace ASP.NETCoreTicTacToe.Models.Games
{
    public class Game
    {
        public History History { get; set; }
        public Board Board { get; private set; }
        public Player TicPlayer { get; set; }
        public Player TacPlayer { get; set; }

        public Game()
        {
            TicPlayer = new Player();
            TicPlayer.Side = Side.Tic;
            TacPlayer = new Player();
            TacPlayer.Side = Side.Tac;
            InitHistory();
            InitBoard();
        }

        public Side? SetSide(string name)
        {
            if (TicPlayer.Name == null || TicPlayer.Name == name)
            {
                TicPlayer.Name = name;
                TicPlayer.IsActive = true;
                return TicPlayer.Side;
            }
            else if (TacPlayer.Name == null || TacPlayer.Name == name)
            {
                TacPlayer.Name = name;
                return TacPlayer.Side;
            }

            return null;
        }

        public void InitHistory()
        {
            History = new History();
        }

        public void InitBoard()
        {
            Board = new Board();
        }

        public bool CanContinue()
        {
            if (Board.HasWinner)
            {
                return false;
            }
            foreach (var cell in Board.Squares)
            {
                if (cell == Cell.Empty)
                {
                    return true;
                }
            }
            return false;
        }

        public bool MakeMove(Turn turn)
        {
            if (turn == null)
            {
                return false;
            }
            
            Turn lastTurn = History.LastTurn;
            if (lastTurn == null)
            {
                return MakeMoveIfNoWinner(turn);
            }
            if (lastTurn.Side == turn.Side)
            { 
                return false;
            }
            else
            {
                return MakeMoveIfNoWinner(turn);
            }
        }

        private bool MakeMoveIfNoWinner(Turn turn)
        {
            if (turn == null)
            {
                return false;
            }
            if (!Board.HasWinner)
            {
                if (Board.Squares[turn.CellNumber] == Cell.Empty)
                {
                    Board newBoard = new Board();
                    newBoard.SetSquares(Board.Squares);
                    newBoard.SetSquare(turn.CellNumber, Board.GetCellBySide(turn.Side));
                    History.Turns.Add(turn);

                    Board = newBoard;

                    SetPlayersActivity(turn);

                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public void MakeBotMove(string player)
        { 
            var opponent = GetOpponent(player);
            if (opponent != null)
            {
                if (opponent.Bot != null)
                {
                    var bot = opponent.Bot;
                    bot.MakeAutoMove(this);
                }
            }
        }       

        private void SetPlayersActivity(Turn turn)
        {
            if (turn != null)
            {
                if (turn.Side == Side.Tic)
                {
                    TacPlayer.IsActive = true;
                    TicPlayer.IsActive = false;
                }
                else
                {
                    TacPlayer.IsActive = false;
                    TicPlayer.IsActive = true;
                }
            }
        }

        private Player GetOpponent(string name)
        {
            if (name == null)
            {
                return null;
            }
            if (TicPlayer.Name == name)
            {
                return TacPlayer;
            }
            if (TacPlayer.Name == name)
            {
                return TicPlayer;
            }
            return null;
        }
    }
}
