using Common;
using Common.Enums;
using Common.Structure;
using Game.Actors;
using Game.Logic;
using Game.Structure;
using System.Collections.Generic;

namespace Game.Actors
{
    public class GameTable
    {
        protected Player[] seats;
        protected GameObject game;
        protected GameState state;
        protected GameMode gameMode;
        protected object[] modeParams;

        public GameTable()
        {
            seats = new Player[4];
            state = GameState.INLOBBY;
            ScoreBoard.gameTable = this;
        }

        #region PLAYER RELATED METHODS

        public Player GetPlayerSeatedAt(int seat)
        {
            return seats[seat];
        }

        public virtual void Sit(Player player, int seat)
        {
            if (seat == -1)
            {
                seat = GetAvailableSeats()[0];
            }

            seats[seat] = player;
            player.UpdateName("Player");

            if (GetAvailableSeatCount() == 0)
            {
                InitializeGame();
            }
        }

        public void Sit(Player player)
        {
            Sit(player, -1);
        }

        public void LeaveTable(Player player)
        {
            seats[GetPlayersSeat(player)] = null;
            Game.Actors.ScoreBoard.ResetScores();
        }

        public void LeaveTable(int seat)
        {
            seats[seat] = null;
            Game.Actors.ScoreBoard.ResetScores();
        }

        public void ResetPlayers()
        {
            seats = new Player[4];
        }

        #endregion

        #region Table Related Methods

        public int GetPlayersSeat(Player player)
        {
            int i = 0;
            bool found = false;
            while (!found && i < 4)
            {
                if (seats[i] != null && seats[i].PlayerName == player.PlayerName)
                {
                    found = true;
                }
                else
                {
                    i++;
                }
            }

            if (found)
            {
                return i;
            }
            else
            {
                return -1;
            }
        }

        public List<int> GetAvailableSeats()
        {
            List<int> availableSeats = new List<int>(4);

            for (int i = 0; i < 4; i++)
            {
                if (seats[i] == null)
                {
                    availableSeats.Add(i);
                }
            }

            return availableSeats;
        }

        public int GetAvailableSeatCount()
        {
            return GetAvailableSeats().Count;
        }

        #endregion

        #region Game Related Methods
        public GameState GetGameState()
        {
            return this.state;
        }

        public void SetGameState(GameState newGameState)
        {
            this.state = newGameState;
        }

        public virtual void InitializeGame()
        {
            GameEndingCondition gameEndingCondition = null;
            switch (gameMode)
            {
                case GameMode.TargetScore:
                    gameEndingCondition = new TargetScoreCondition(modeParams);
                    break;
                case GameMode.RoundCount:
                    object[] roundCountConditionParams = new object[modeParams.Length + 1];
                    roundCountConditionParams[0] = game;
                    modeParams.CopyTo(roundCountConditionParams, 1);
                    gameEndingCondition = new RoundCountCondition(modeParams);
                    break;
            }

            DealCards();
            UpdatePlayerDeckValues();

            game = new GameObject(this, gameEndingCondition);
            game.Commence();
            state = GameState.PLAYING;
        }

        public void EndGame()
        {
            state = GameState.INLOBBY;
        }

        public void ChangeGameMode(GameMode newMode, object[] modeParams)
        {
            this.gameMode = newMode;
            this.modeParams = modeParams;
        }

        public GameObject GetGameInstance()
        {
            return game;
        }

        public void DealCards()
        {
            //      HANDLE PLAYER ACTIONS   //

            Deck mainDeck = new Deck();
            List<Card>[] splittedCards = mainDeck.Deal();
            DealSplittedCards(splittedCards);
        }

        public virtual void DealSplittedCards(List<Card>[] splittedCards)
        {
            for (int i = 0; i < 4; i++)
            {
                Deck newPlayerDeck = new Deck(splittedCards[i]);
                seats[i].SetPlayersDeck(newPlayerDeck);
            }
        }

        #endregion


        public void UpdatePlayerDeckValues()
        {
            for (int i = 0; i < 4; i++)
            {
                Player thisPlayer = GetPlayerSeatedAt(i);
                Deck playersDeck = thisPlayer.GetPlayersDeck();
                DeckValue bestValue = DeckHelper.CalculateDeckValues(playersDeck)[3];
                playersDeck.SetDeckValue(bestValue.value);
                playersDeck.SetTrumpToChoose(bestValue.trumpType);
            }
        }
    }
}
