using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using Alpacinator.DataAccess.Base;
using Alpacinator.Models;

namespace Alpacinator.DataAccess.Repository
{
    public class FarmRepository : IFarmRepository
    {
        private AlpacinatorDb _alpacinatorDb = null;

        public FarmRepository()
        {
            _alpacinatorDb = new AlpacinatorDb();
            _alpacinatorDb.Initialize();
        }


        public List<Farm> GetAll()
        {
            List<Farm> farms = new List<Farm>();

            using (SQLiteConnection dbConnection = _alpacinatorDb.GetDbConnection())
            {
                dbConnection.Open();

                SQLiteCommand sqlCommand = new SQLiteCommand("select * from Farm order by name", dbConnection);
                SQLiteDataReader dr = sqlCommand.ExecuteReader();
                while (dr.Read())
                {
                    Farm farm = new Farm();
                    farm.Id = Convert.ToInt32(dr["id"]);
                    farm.Name = dr["name"].ToString();
                    farm.Multiplier = Convert.ToDouble(dr["multiplier"]);

                    farms.Add(farm);
                }

                dr.Close();
                dbConnection.Close();
            }

            return farms;
        }

        public Farm GetByName(string farmName)
        {
            Farm farm = null;

            using (SQLiteConnection dbConnection = _alpacinatorDb.GetDbConnection())
            {
                dbConnection.Open();

                SQLiteCommand sqlCommand = new SQLiteCommand("select * from Farm where Name=@Name", dbConnection);
                sqlCommand.Parameters.Add("@Name", DbType.String);
                sqlCommand.Parameters["@Name"].Value = farmName;

                SQLiteDataReader dr = sqlCommand.ExecuteReader();

                if (dr.Read())
                {
                    farm = new Farm();
                    farm.Id = Convert.ToInt32(dr["id"]);
                    farm.Name = dr["name"].ToString();
                    farm.Multiplier = Convert.ToDouble(dr["multiplier"]);
                }

                dr.Close();
                dbConnection.Close();
            }

            return farm;
        }
    }
}
