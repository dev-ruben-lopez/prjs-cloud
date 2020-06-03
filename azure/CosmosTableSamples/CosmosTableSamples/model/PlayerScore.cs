using Microsoft.Azure.Cosmos.Table;
using System;
using System.Collections.Generic;
using System.Text;

namespace CosmosTableSamples.model
{
    class PlayerScore: TableEntity
    {

        public int Score { get; set; }
        public long TimePlayed { get; set; }

        public PlayerScore()
        { 
        }




        public PlayerScore(string p_GameID, string p_PlayerID, int p_score,long p_timeplayed)
        {

            this.PartitionKey = p_GameID;
            this.RowKey = p_PlayerID;
            this.Score = p_score;
            this.TimePlayed = p_timeplayed;
        }
    }
}
