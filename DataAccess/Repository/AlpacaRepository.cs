using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using Alpacinator.DataAccess.Base;
using Alpacinator.Models;

namespace Alpacinator.DataAccess.Repository
{
    public class AlpacaRepository : IAlpacaRepository
    {
        private AlpacinatorDb _alpacinatorDb = null;
		public AlpacaRepository()
        {
            _alpacinatorDb = new AlpacinatorDb();
			_alpacinatorDb.Initialize();
		}

		public List<Alpaca> GetAll()
        {
            List<Alpaca> alpacaList = new List<Alpaca>();

            using (SQLiteConnection dbConnection = _alpacinatorDb.GetDbConnection())
            {
				dbConnection.Open();

                string sql = _alpacinatorDb.GetScript("SelectAll_Alpaca.sql");
                SQLiteCommand sqlCommand = new SQLiteCommand(sql, dbConnection);
                SQLiteDataReader dr = sqlCommand.ExecuteReader();
                while (dr.Read())
                {
                    Alpaca alpaca = new Alpaca();
                    alpaca.Id = Convert.ToInt32(dr["alpaca_id"]);
                    alpaca.Name = Convert.ToString(dr["alpaca_name"].ToString());
                    alpaca.Weight = Convert.ToDouble(dr["alpaca_weight"]);
                    alpaca.Color = Convert.ToString(dr["alpaca_color"]);

                    alpaca.Farm = new Farm();
                    alpaca.Farm.Id = Convert.ToInt32(dr["farm_id"]);
                    alpaca.Farm.Name = Convert.ToString(dr["farm_name"]);
                    alpaca.Farm.Multiplier = Convert.ToDouble(dr["farm_multiplier"]);

                    alpacaList.Add(alpaca);
                }

                dr.Close();
                dbConnection.Close();
            }

            return alpacaList;
        }

		public Alpaca GetById(int id)
        {
            Alpaca alpaca = null;

            using (SQLiteConnection dbConnection = _alpacinatorDb.GetDbConnection())
            {
                dbConnection.Open();

                string sql = _alpacinatorDb.GetScript("SelectById_Alpaca.sql");
                SQLiteCommand sqlCommand = new SQLiteCommand(sql, dbConnection);
                sqlCommand.Parameters.Add("@AlpacaId", DbType.Int32);
                sqlCommand.Parameters["@AlpacaId"].Value = id;
                
                SQLiteDataReader dr = sqlCommand.ExecuteReader();
                if (dr.Read())
                {
                    alpaca = new Alpaca();
                    alpaca.Id = Convert.ToInt32(dr["alpaca_id"]);
                    alpaca.Name = Convert.ToString(dr["alpaca_name"].ToString());
                    alpaca.Weight = Convert.ToDouble(dr["alpaca_weight"]);
                    alpaca.Color = Convert.ToString(dr["alpaca_color"]);

                    alpaca.Farm = new Farm();
                    alpaca.Farm.Id = Convert.ToInt32(dr["farm_id"]);
                    alpaca.Farm.Name = Convert.ToString(dr["farm_name"]);
                    alpaca.Farm.Multiplier = Convert.ToDouble(dr["farm_multiplier"]);
                }

                dr.Close();

                dbConnection.Close();
            }

            return alpaca;
        }

		public void Add(Alpaca alpaca)
		{
            using (SQLiteConnection dbConnection = _alpacinatorDb.GetDbConnection())
            {
                dbConnection.Open();

                SQLiteCommand sqlCommand = new SQLiteCommand("insert into Alpaca (Id,Name,Weight,Color,FarmId) values (@Id,@Name,@Weight,@Color,@FarmId)", dbConnection);

                sqlCommand.Parameters.Add("@Id", DbType.Int32);
                sqlCommand.Parameters.Add("@Name", DbType.String);
                sqlCommand.Parameters.Add("@Weight", DbType.Double);
                sqlCommand.Parameters.Add("@Color", DbType.String);
                sqlCommand.Parameters.Add("@FarmId", DbType.Int32);
                sqlCommand.Parameters["@Id"].Value = alpaca.Id;
                sqlCommand.Parameters["@Name"].Value = alpaca.Name;
                sqlCommand.Parameters["@Weight"].Value = alpaca.Weight;
                sqlCommand.Parameters["@Color"].Value = alpaca.Color;
                sqlCommand.Parameters["@FarmId"].Value = alpaca.Farm.Id;

                sqlCommand.ExecuteNonQuery();
                dbConnection.Close();
            }
        }

		public void Update(Alpaca alpaca)
		{
            using (SQLiteConnection dbConnection = _alpacinatorDb.GetDbConnection())
            {
                dbConnection.Open();

                SQLiteCommand sqlCommand = new SQLiteCommand("update Alpaca set Name=@Name, Weight=@Weight, Color=@Color, FarmId=@FarmId where Id=@Id", dbConnection);

                sqlCommand.Parameters.Add("@Id", DbType.Int32);
                sqlCommand.Parameters.Add("@Name", DbType.String);
                sqlCommand.Parameters.Add("@Weight", DbType.Double);
                sqlCommand.Parameters.Add("@Color", DbType.String);
                sqlCommand.Parameters.Add("@FarmId", DbType.Int32);
                sqlCommand.Parameters["@Id"].Value = alpaca.Id;
                sqlCommand.Parameters["@Name"].Value = alpaca.Name;
                sqlCommand.Parameters["@Weight"].Value = alpaca.Weight;
                sqlCommand.Parameters["@Color"].Value = alpaca.Color;
                sqlCommand.Parameters["@FarmId"].Value = alpaca.Farm.Id;

                sqlCommand.ExecuteNonQuery();

                dbConnection.Close();
            }
        }

		public void Delete(int id)
		{
            using (SQLiteConnection dbConnection = _alpacinatorDb.GetDbConnection())
            {
                dbConnection.Open();

                SQLiteCommand sqlCommand = new SQLiteCommand("delete from Alpaca where Id=@Id", dbConnection);
                sqlCommand.Parameters.Add("@Id", DbType.Int32);
                sqlCommand.Parameters["@Id"].Value = id;

                sqlCommand.ExecuteNonQuery();

                dbConnection.Close();
            }
        }

		public int GetNextId()
        {
            int nextId = 1;

            using (SQLiteConnection dbConnection = _alpacinatorDb.GetDbConnection())
            {
                dbConnection.Open();

                SQLiteCommand sqlCommand = new SQLiteCommand("select max(id) from Alpaca", dbConnection);

                nextId = Convert.ToInt32(sqlCommand.ExecuteScalar()) + 1;

                dbConnection.Close();
            }

            return nextId;
        }
	}
}
