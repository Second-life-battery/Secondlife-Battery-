﻿using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace SecondLife_Battery
{
    public class DataAccessLayer
    {
        private string connectionString = @"Server=secondlife-battery.database.windows.net;Database=SecondLifeBatteryDB;user=SqlAdmin;password=Secondlife16;";
        private DateTime insertedDateValue;
        DataTable dataTable1 = new DataTable();
        DataTable dataTable2 = new DataTable();
        DataTable dataTable3 = new DataTable();
        DataTable dataTable4 = new DataTable();
        DataTable dataTableWeather1 = new DataTable();
        DataTable dataTableWeather2 = new DataTable();
        DataTable dataTableWeather3 = new DataTable();
        DataTable dataTableWeather4 = new DataTable();
        ArrayList arrayWind1 = new ArrayList();
        ArrayList arrayCloud1 = new ArrayList();
        ArrayList arrayDate1 = new ArrayList();
        ArrayList arrayTemperature1 = new ArrayList();
        ArrayList arrayWind2 = new ArrayList();
        ArrayList arrayCloud2 = new ArrayList();
        ArrayList arrayDate2 = new ArrayList();
        ArrayList arrayTemperature2 = new ArrayList();
        ArrayList arrayWind3 = new ArrayList();
        ArrayList arrayCloud3 = new ArrayList();
        ArrayList arrayDate3 = new ArrayList();
        ArrayList arrayTemperature3 = new ArrayList();
        ArrayList arrayWind4 = new ArrayList();
        ArrayList arrayCloud4 = new ArrayList();
        ArrayList arrayDate4 = new ArrayList();
        ArrayList arrayTemperature4 = new ArrayList();
        private double temperature1;
        private double windSpeed1;
        private double cloudCover1;
        private DateTime weatherDate1;
        private double temperature2;
        private double windSpeed2;
        private double cloudCover2;
        private DateTime weatherDate2;
        private double temperature3;
        private double windSpeed3;
        private double cloudCover3;
        private DateTime weatherDate3;
        private double temperature4;
        private double windSpeed4;
        private double cloudCover4;
        private DateTime weatherDate4;
        public void SetDate(DateTime tempDateValue)
        {
            insertedDateValue = tempDateValue;
        }
        public DataTable GetElectricityPriceSE1()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand();

                DateTime date2 = insertedDateValue.AddDays(6);

                string sqlQuery = "SELECT Date, SE1 FROM ElectricityPrices WHERE Date between CAST(GETDATE()-365 as Date) and CAST(GETDATE()-359 as Date)";
                command.CommandText = sqlQuery;
                command.Connection = connection;

                SqlDataAdapter adapter = new SqlDataAdapter(sqlQuery, connection);
                SqlCommandBuilder commandBuilder = new SqlCommandBuilder(adapter);
                adapter.Fill(dataTable1);
                dataTable1.Columns["SE1"].ColumnName = "Electricity Price";
            }
            return dataTable1;
        }
        public DataTable GetElectricityPriceSE2()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand();

                DateTime date2 = insertedDateValue.AddDays(6);

                string sqlQuery = "SELECT Date, SE2 FROM ElectricityPrices WHERE Date between CAST(GETDATE()-365 as Date) and CAST(GETDATE()-359 as Date)";
                command.CommandText = sqlQuery;
                command.Connection = connection;

                SqlDataAdapter adapter = new SqlDataAdapter(sqlQuery, connection);
                SqlCommandBuilder commandBuilder = new SqlCommandBuilder(adapter);
                adapter.Fill(dataTable2);
                dataTable2.Columns["SE2"].ColumnName = "Electricity Price";
            }
            return dataTable2;
        }
        public DataTable GetElectricityPriceSE3()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand();

                DateTime date2 = insertedDateValue.AddDays(6);

                string sqlQuery = "SELECT Date, SE3 FROM ElectricityPrices WHERE Date between CAST(GETDATE()-365 as Date) and CAST(GETDATE()-359 as Date)";
                command.CommandText = sqlQuery;
                command.Connection = connection;

                SqlDataAdapter adapter = new SqlDataAdapter(sqlQuery, connection);
                SqlCommandBuilder commandBuilder = new SqlCommandBuilder(adapter);
                adapter.Fill(dataTable3);
                dataTable3.Columns["SE3"].ColumnName = "Electricity Price";
            }
            return dataTable3;
        }
        public DataTable GetElectricityPriceSE4()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand();

                DateTime date2 = insertedDateValue.AddDays(6);

                string sqlQuery = "SELECT Date, SE4 FROM ElectricityPrices WHERE Date between CAST(GETDATE()-365 as Date) and CAST(GETDATE()-359 as Date)";
                command.CommandText = sqlQuery;
                command.Connection = connection;

                SqlDataAdapter adapter = new SqlDataAdapter(sqlQuery, connection);
                SqlCommandBuilder commandBuilder = new SqlCommandBuilder(adapter);
                adapter.Fill(dataTable4);
                dataTable4.Columns["SE4"].ColumnName = "Electricity Price";
            }
            return dataTable4;
        }
        public void ClearData()
        {
            //dataTable.Clear();
            //dataTable.Columns.Clear();
        }
        public void ClearWeatherData()
        {
            //dataTableWeather.Clear();
            //dataTableWeather.Columns.Clear();
        }

        public async Task<DataTable> GetWeatherAsyncSE1()
        {
            string aPIRequest;

            aPIRequest = "https://weather.visualcrossing.com/VisualCrossingWebServices/rest/services/timeline/lule%C3%A5?unitGroup=metric&key=QDN24AE8877YDJTTM3MYA7RDS&contentType=json";
            
            
            var client = new HttpClient();

            var request = new HttpRequestMessage(HttpMethod.Get, aPIRequest);

            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode(); // Throw an exception if error

            var body = await response.Content.ReadAsStringAsync();

            dynamic weather = JsonConvert.DeserializeObject(body);

            foreach (var obj in weather.days)
            {
                string weather_Date = obj.datetime;
                try
                {
                    windSpeed1 = Convert.ToDouble(obj.windspeed);
                    cloudCover1 = Convert.ToDouble(obj.cloudcover);
                    weatherDate1 = Convert.ToDateTime(weather_Date);
                    temperature1 = Convert.ToDouble(obj.temp);
                    arrayWind1.Add(windSpeed1);
                    arrayCloud1.Add(cloudCover1);
                    arrayDate1.Add(weather_Date);
                    arrayTemperature1.Add(temperature1);
                }
                catch (Exception ex)
                {

                }
            }
            DataColumn dataColumn;
            //Create Date column.
            dataColumn = new DataColumn();
            dataColumn.ColumnName = "Date";
            dataColumn.DataType = typeof(DateTime);
            dataTableWeather1.Columns.Add(dataColumn);
            //Create Wind velocity column.
            dataColumn = new DataColumn();
            dataColumn.ColumnName = "Wind Velocity (km/h)";
            dataColumn.DataType = typeof(double);
            dataTableWeather1.Columns.Add(dataColumn);
            //Create Cloud Coverage column.
            dataColumn = new DataColumn();
            dataColumn.ColumnName = "Cloud Coverage (%)";
            dataColumn.DataType = typeof(double);
            dataTableWeather1.Columns.Add(dataColumn);
            //Create the Temperature column.
            dataColumn = new DataColumn();
            dataColumn.ColumnName = "Temperature (°C)";
            dataColumn.DataType = typeof(double);
            dataTableWeather1.Columns.Add(dataColumn);
            //Create the recommendation column.
            dataColumn = new DataColumn();
            dataColumn.ColumnName = "Expected change";
            dataColumn.DataType = typeof(string);
            dataTableWeather1.Columns.Add(dataColumn);
            for (int i = 0; i < 7; i++)
            {
                //Tweak the values here to recieve different output
                DataRow row = dataTableWeather1.NewRow();
                //wind between 15-90 km/h. Between 15-30 effects litle, 30-60 more, 60-90 a lot
                //Cloud between 0-25% effects a lot, 25-50 slightly less, 50-75 a litle, 75-100 no effect
                //Temperature minus 0 effects a lot, 0-10 slightly less, 10-15 a litle, 15-25 nothing
                if ((double)arrayWind1[i] < 15 && //No wind, No sun, moderate temperature
                    (double)arrayCloud1[i] >= 90 && (double)arrayCloud1[i] <= 100 &&
                    (double)arrayTemperature1[i] <= 25 && (double)arrayTemperature1[i] >= 15)
                {
                    row[0] = arrayDate1[i];
                    row[1] = (double)arrayWind1[i];
                    row[2] = (double)arrayCloud1[i];
                    row[3] = (double)arrayTemperature1[i];
                    row[4] = "No expected price change"; //No weather impact on price
                    dataTableWeather1.Rows.Add(row);
                }
                else if ((double)arrayWind1[i] < 15 && //No wind, sunny, cold temperature
                    (double)arrayCloud1[i] <= 50 &&
                    (double)arrayTemperature1[i] <= 10 && (double)arrayTemperature1[i] >= -10)
                {
                    row[0] = arrayDate1[i];
                    row[1] = (double)arrayWind1[i];
                    row[2] = (double)arrayCloud1[i];
                    row[3] = (double)arrayTemperature1[i];
                    row[4] = "Reduction due to sun";
                    dataTableWeather1.Rows.Add(row);
                }
                else if ((double)arrayWind1[i] < 15 && //No wind, sunny, cold temperature
                    (double)arrayCloud1[i] <= 50 &&
                    (double)arrayTemperature1[i] <= 0 && (double)arrayTemperature1[i] >= -20)
                {
                    row[0] = arrayDate1[i];
                    row[1] = (double)arrayWind1[i];
                    row[2] = (double)arrayCloud1[i];
                    row[3] = (double)arrayTemperature1[i];
                    row[4] = "No expected change due to cold weather and sun";
                    dataTableWeather1.Rows.Add(row);
                }
                //Tweak the values here to recieve different output
                else if ((double)arrayWind1[i] >= 15 && (double)arrayWind1[i] <= 30 && //Low wind, No sun, moderate temperature
                    (double)arrayCloud1[i] >= 75 && (double)arrayCloud1[i] <= 90 &&
                    (double)arrayTemperature1[i] <= 25 && (double)arrayTemperature1[i] >= 15)
                {
                    row[0] = arrayDate1[i];
                    row[1] = (double)arrayWind1[i];
                    row[2] = (double)arrayCloud1[i];
                    row[3] = (double)arrayTemperature1[i];
                    row[4] = "Slight reduction due to some wind.";
                    dataTableWeather1.Rows.Add(row);
                }
                else if ((double)arrayWind1[i] >= 15 && (double)arrayWind1[i] <= 30 &&//Low wind, moderate sun, moderate temperature
                    (double)arrayCloud1[i] >= 50 && (double)arrayCloud1[i] <= 90 &&
                    (double)arrayTemperature1[i] <= 25 && (double)arrayTemperature1[i] >= 15)
                {
                    row[0] = arrayDate1[i];
                    row[1] = (double)arrayWind1[i];
                    row[2] = (double)arrayCloud1[i];
                    row[3] = (double)arrayTemperature1[i];
                    row[4] = "Slight reduction due to some wind.";
                    dataTableWeather1.Rows.Add(row);
                }
                else if ((double)arrayWind1[i] >= 15 && (double)arrayWind1[i] <= 60 &&//Low to medium wind, moderate sun, low temperature
                    (double)arrayCloud1[i] >= 50 && (double)arrayCloud1[i] <= 90 &&
                    (double)arrayTemperature1[i] <= 10)
                {
                    row[0] = arrayDate1[i];
                    row[1] = (double)arrayWind1[i];
                    row[2] = (double)arrayCloud1[i];
                    row[3] = (double)arrayTemperature1[i];
                    row[4] = "Slight increase due to cold temperature";
                    dataTableWeather1.Rows.Add(row);
                }
                else if ((double)arrayWind1[i] >= 15 && (double)arrayWind1[i] <= 60 &&//low to medium wind, sunny, cold temperature
                    (double)arrayCloud1[i] <= 50 &&
                    (double)arrayTemperature1[i] <= 10 && (double)arrayTemperature1[i] >= 0)
                {
                    row[0] = arrayDate1[i];
                    row[1] = (double)arrayWind1[i];
                    row[2] = (double)arrayCloud1[i];
                    row[3] = (double)arrayTemperature1[i];
                    row[4] = "Slight reduction due to some wind and sun";
                    dataTableWeather1.Rows.Add(row);
                }
                else if ((double)arrayWind1[i] >= 15 && (double)arrayWind1[i] <= 60 &&//low to medium wind, sunny, very cold temperature
                    (double)arrayCloud1[i] <= 50 &&
                    (double)arrayTemperature1[i] <= 10)
                {
                    row[0] = arrayDate1[i];
                    row[1] = (double)arrayWind1[i];
                    row[2] = (double)arrayCloud1[i];
                    row[3] = (double)arrayTemperature1[i];
                    row[4] = "No expected price change";
                    dataTableWeather1.Rows.Add(row);
                }
                else if ((double)arrayWind1[i] >= 15 && (double)arrayWind1[i] <= 60 && //Low to medium wind, moderate sun, moderate temperature
                    (double)arrayCloud1[i] >= 50 && (double)arrayCloud1[i] <= 90 &&
                    (double)arrayTemperature1[i] <= 25 && (double)arrayTemperature1[i] >= 15)
                {
                    row[0] = arrayDate1[i];
                    row[1] = (double)arrayWind1[i];
                    row[2] = (double)arrayCloud1[i];
                    row[3] = (double)arrayTemperature1[i];
                    row[4] = "Slight reduction due to some wind.";
                    dataTableWeather1.Rows.Add(row);
                }
                else if ((double)arrayWind1[i] >= 30 && (double)arrayWind1[i] <= 90 && //Medium to strong wind, moderate sun, moderate temperature
                    (double)arrayCloud1[i] >= 50 && (double)arrayCloud1[i] <= 90 &&
                    (double)arrayTemperature1[i] <= 25 && (double)arrayTemperature1[i] >= 15)
                {
                    row[0] = arrayDate1[i];
                    row[1] = (double)arrayWind1[i];
                    row[2] = (double)arrayCloud1[i];
                    row[3] = (double)arrayTemperature2[i];
                    row[4] = "Reduction due to strong wind.";
                    dataTableWeather1.Rows.Add(row);
                }
                else if ((double)arrayWind1[i] >= 30 && (double)arrayWind1[i] <= 90 && //Medium to strong wind, moderate sun, moderate temperature
                    (double)arrayCloud1[i] >= 50 && (double)arrayCloud1[i] <= 90 &&
                    (double)arrayTemperature1[i] <= 25 && (double)arrayTemperature1[i] >= 15)
                {
                    row[0] = arrayDate1[i];
                    row[1] = (double)arrayWind1[i];
                    row[2] = (double)arrayCloud1[i];
                    row[3] = (double)arrayTemperature1[i];
                    row[4] = "Reduction due to strong wind.";
                    dataTableWeather1.Rows.Add(row);
                }
                else if ((double)arrayWind1[i] >= 30 && (double)arrayWind1[i] <= 90 && //Medium to strong wind, moderate sun, moderate temperature
                    (double)arrayCloud1[i] >= 50 && (double)arrayCloud1[i] <= 90 &&
                    (double)arrayTemperature1[i] <= 25 && (double)arrayTemperature1[i] >= 15)
                {
                    row[0] = arrayDate1[i];
                    row[1] = (double)arrayWind1[i];
                    row[2] = (double)arrayCloud1[i];
                    row[3] = (double)arrayTemperature1[i];
                    row[4] = "Reduction due to strong wind.";
                    dataTableWeather1.Rows.Add(row);
                }
                else if ((double)arrayWind1[i] >= 30 && (double)arrayWind1[i] <= 90 && //Medium to strong wind, sunny, moderate temperature
                    (double)arrayCloud1[i] <= 50 &&
                    (double)arrayTemperature1[i] <= 25 && (double)arrayTemperature1[i] >= 15)
                {
                    row[0] = arrayDate1[i];
                    row[1] = (double)arrayWind1[i];
                    row[2] = (double)arrayCloud1[i];
                    row[3] = (double)arrayTemperature1[i];
                    row[4] = "Reduction due to strong wind and sun.";
                    dataTableWeather1.Rows.Add(row);
                }
                else if ((double)arrayWind1[i] >= 30 && (double)arrayWind1[i] <= 90 && //Medium to strong wind, sunny, moderate temperature
                    (double)arrayCloud1[i] <= 50 &&
                    (double)arrayTemperature1[i] <= 25 && (double)arrayTemperature1[i] >= 15)
                {
                    row[0] = arrayDate1[i];
                    row[1] = (double)arrayWind1[i];
                    row[2] = (double)arrayCloud1[i];
                    row[3] = (double)arrayTemperature1[i];
                    row[4] = "Reduction due to strong wind and sun.";
                    dataTableWeather1.Rows.Add(row);
                }
                else if ((double)arrayWind1[i] >= 30 && (double)arrayWind1[i] <= 90 && //Medium to strong wind, moderate sun, moderate temperature
                    (double)arrayCloud1[i] >= 50 && (double)arrayCloud1[i] <= 90 &&
                    (double)arrayTemperature1[i] <= 10)
                {
                    row[0] = arrayDate1[i];
                    row[1] = (double)arrayWind1[i];
                    row[2] = (double)arrayCloud1[i];
                    row[3] = (double)arrayTemperature1[i];
                    row[4] = "Reduction due to strong wind and sun.";
                    dataTableWeather1.Rows.Add(row);
                }
                else if ((double)arrayWind1[i] >= 30 && (double)arrayWind1[i] <= 90 && //Medium to strong wind, no sun, cold temperature
                    (double)arrayCloud1[i] >= 90 &&
                    (double)arrayTemperature1[i] <= 10)
                {
                    row[0] = arrayDate1[i];
                    row[1] = (double)arrayWind1[i];
                    row[2] = (double)arrayCloud1[i];
                    row[3] = (double)arrayTemperature1[i];
                    row[4] = "Slight to no reduction";
                    dataTableWeather1.Rows.Add(row);
                }
                else if ((double)arrayWind1[i] <= 15 && //no wind, no sun, very cold temperature
                    (double)arrayCloud1[i] >= 90 &&
                    (double)arrayTemperature1[i] <= 0)
                {
                    row[0] = arrayDate1[i];
                    row[1] = (double)arrayWind1[i];
                    row[2] = (double)arrayCloud1[i];
                    row[3] = (double)arrayTemperature1[i];
                    row[4] = "Increase due to cold temperature";
                    dataTableWeather1.Rows.Add(row);
                }
                else if ((double)arrayWind1[i] <= 15 && //no wind, moderate sun, very cold temperature
                    (double)arrayCloud1[i] >= 50 && (double)arrayCloud1[i] <= 90 &&
                    (double)arrayTemperature1[i] <= 0)
                {
                    row[0] = arrayDate1[i];
                    row[1] = (double)arrayWind1[i];
                    row[2] = (double)arrayCloud1[i];
                    row[3] = (double)arrayTemperature1[i];
                    row[4] = "Increase due to low temperature";
                    dataTableWeather1.Rows.Add(row);
                }
                else if ((double)arrayWind1[i] >= 15 && (double)arrayWind1[i] <= 30 &&//low wind, no sun, very cold temperature
                    (double)arrayCloud1[i] >= 90 &&
                    (double)arrayTemperature1[i] <= 0)
                {
                    row[0] = arrayDate1[i];
                    row[1] = (double)arrayWind1[i];
                    row[2] = (double)arrayCloud1[i];
                    row[3] = (double)arrayTemperature1[i];
                    row[4] = "Increase due to low temperature and low wind";
                    dataTableWeather1.Rows.Add(row);
                }
                //Tweak the values here to recieve different output
                else
                {
                    row["Date"] = arrayDate1[i];
                    row["Wind Velocity (km/h)"] = (double)arrayWind1[i];
                    row["Cloud Coverage (%)"] = (double)arrayCloud1[i];
                    row["Temperature (°C)"] = (double)arrayTemperature1[i];
                    row["Expected change"] = "Low probability of change.";
                    dataTableWeather1.Rows.Add(row);
                }
            }
            return dataTableWeather1;
        }
        public async Task<DataTable> GetWeatherAsyncSE2()
        {
            string aPIRequest;

            aPIRequest = "https://weather.visualcrossing.com/VisualCrossingWebServices/rest/services/timeline/%C3%B6stersund?unitGroup=metric&key=QDN24AE8877YDJTTM3MYA7RDS&contentType=json";
           

            var client = new HttpClient();

            var request = new HttpRequestMessage(HttpMethod.Get, aPIRequest);

            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode(); // Throw an exception if error

            var body = await response.Content.ReadAsStringAsync();

            dynamic weather = JsonConvert.DeserializeObject(body);

            foreach (var obj in weather.days)
            {
                string weather_Date = obj.datetime;
                try
                {
                    windSpeed2 = Convert.ToDouble(obj.windspeed);
                    cloudCover2 = Convert.ToDouble(obj.cloudcover);
                    weatherDate2 = Convert.ToDateTime(weather_Date);
                    temperature2 = Convert.ToDouble(obj.temp);
                    arrayWind2.Add(windSpeed2);
                    arrayCloud2.Add(cloudCover2);
                    arrayDate2.Add(weather_Date);
                    arrayTemperature2.Add(temperature2);
                }
                catch (Exception ex)
                {

                }
            }
            DataColumn dataColumn;
            //Create Date column.
            dataColumn = new DataColumn();
            dataColumn.ColumnName = "Date";
            dataColumn.DataType = typeof(DateTime);
            dataTableWeather2.Columns.Add(dataColumn);
            //Create Wind velocity column.
            dataColumn = new DataColumn();
            dataColumn.ColumnName = "Wind Velocity (km/h)";
            dataColumn.DataType = typeof(double);
            dataTableWeather2.Columns.Add(dataColumn);
            //Create Cloud Coverage column.
            dataColumn = new DataColumn();
            dataColumn.ColumnName = "Cloud Coverage (%)";
            dataColumn.DataType = typeof(double);
            dataTableWeather2.Columns.Add(dataColumn);
            //Create the Temperature column.
            dataColumn = new DataColumn();
            dataColumn.ColumnName = "Temperature (°C)";
            dataColumn.DataType = typeof(double);
            dataTableWeather2.Columns.Add(dataColumn);
            //Create the recommendation column.
            dataColumn = new DataColumn();
            dataColumn.ColumnName = "Expected change";
            dataColumn.DataType = typeof(string);
            dataTableWeather2.Columns.Add(dataColumn);
            for (int i = 0; i < 7; i++)
            {
                //Tweak the values here to recieve different output
                DataRow row = dataTableWeather2.NewRow();
                //wind between 15-90 km/h. Between 15-30 effects litle, 30-60 more, 60-90 a lot
                //Cloud between 0-25% effects a lot, 25-50 slightly less, 50-75 a litle, 75-100 no effect
                //Temperature minus 0 effects a lot, 0-10 slightly less, 10-15 a litle, 15-25 nothing
                if ((double)arrayWind2[i] < 15 && //No wind, No sun, moderate temperature
                    (double)arrayCloud2[i] >= 90 && (double)arrayCloud2[i] <= 100 &&
                    (double)arrayTemperature2[i] <= 25 && (double)arrayTemperature2[i] >= 15)
                {
                    row[0] = arrayDate2[i];
                    row[1] = (double)arrayWind2[i];
                    row[2] = (double)arrayCloud2[i];
                    row[3] = (double)arrayTemperature2[i];
                    row[4] = "No expected price change"; //No weather impact on price
                    dataTableWeather2.Rows.Add(row);
                }
                else if ((double)arrayWind2[i] < 15 && //No wind, sunny, cold temperature
                    (double)arrayCloud2[i] <= 50 &&
                    (double)arrayTemperature2[i] <= 10 && (double)arrayTemperature2[i] >= -10)
                {
                    row[0] = arrayDate2[i];
                    row[1] = (double)arrayWind2[i];
                    row[2] = (double)arrayCloud2[i];
                    row[3] = (double)arrayTemperature2[i];
                    row[4] = "Reduction due to sun";
                    dataTableWeather2.Rows.Add(row);
                }
                else if ((double)arrayWind2[i] < 15 && //No wind, sunny, cold temperature
                    (double)arrayCloud2[i] <= 50 &&
                    (double)arrayTemperature2[i] <= 0 && (double)arrayTemperature2[i] >= -20)
                {
                    row[0] = arrayDate2[i];
                    row[1] = (double)arrayWind2[i];
                    row[2] = (double)arrayCloud2[i];
                    row[3] = (double)arrayTemperature2[i];
                    row[4] = "No expected change due to cold weather and sun";
                    dataTableWeather2.Rows.Add(row);
                }
                //Tweak the values here to recieve different output
                else if ((double)arrayWind2[i] >= 15 && (double)arrayWind2[i] <= 30 && //Low wind, No sun, moderate temperature
                    (double)arrayCloud2[i] >= 75 && (double)arrayCloud2[i] <= 90 &&
                    (double)arrayTemperature2[i] <= 25 && (double)arrayTemperature2[i] >= 15)
                {
                    row[0] = arrayDate2[i];
                    row[1] = (double)arrayWind2[i];
                    row[2] = (double)arrayCloud2[i];
                    row[3] = (double)arrayTemperature2[i];
                    row[4] = "Slight reduction due to some wind.";
                    dataTableWeather2.Rows.Add(row);
                }
                else if ((double)arrayWind2[i] >= 15 && (double)arrayWind2[i] <= 30 &&//Low wind, moderate sun, moderate temperature
                    (double)arrayCloud2[i] >= 50 && (double)arrayCloud2[i] <= 90 &&
                    (double)arrayTemperature2[i] <= 25 && (double)arrayTemperature2[i] >= 15)
                {
                    row[0] = arrayDate2[i];
                    row[1] = (double)arrayWind2[i];
                    row[2] = (double)arrayCloud2[i];
                    row[3] = (double)arrayTemperature2[i];
                    row[4] = "Slight reduction due to some wind.";
                    dataTableWeather2.Rows.Add(row);
                }
                else if ((double)arrayWind2[i] >= 15 && (double)arrayWind2[i] <= 60 &&//Low to medium wind, moderate sun, low temperature
                    (double)arrayCloud2[i] >= 50 && (double)arrayCloud2[i] <= 90 &&
                    (double)arrayTemperature2[i] <= 10)
                {
                    row[0] = arrayDate1[i];
                    row[1] = (double)arrayWind2[i];
                    row[2] = (double)arrayCloud2[i];
                    row[3] = (double)arrayTemperature2[i];
                    row[4] = "Slight increase due to cold temperature";
                    dataTableWeather2.Rows.Add(row);
                }
                else if ((double)arrayWind2[i] >= 15 && (double)arrayWind2[i] <= 60 &&//low to medium wind, sunny, cold temperature
                    (double)arrayCloud2[i] <= 50 &&
                    (double)arrayTemperature2[i] <= 10 && (double)arrayTemperature2[i] >= 0)
                {
                    row[0] = arrayDate2[i];
                    row[1] = (double)arrayWind2[i];
                    row[2] = (double)arrayCloud2[i];
                    row[3] = (double)arrayTemperature2[i];
                    row[4] = "Slight reduction due to some wind and sun";
                    dataTableWeather2.Rows.Add(row);
                }
                else if ((double)arrayWind2[i] >= 15 && (double)arrayWind2[i] <= 60 &&//low to medium wind, sunny, very cold temperature
                    (double)arrayCloud2[i] <= 50 &&
                    (double)arrayTemperature2[i] <= 10)
                {
                    row[0] = arrayDate2[i];
                    row[1] = (double)arrayWind2[i];
                    row[2] = (double)arrayCloud2[i];
                    row[3] = (double)arrayTemperature2[i];
                    row[4] = "No expected price change";
                    dataTableWeather2.Rows.Add(row);
                }
                else if ((double)arrayWind2[i] >= 15 && (double)arrayWind2[i] <= 60 && //Low to medium wind, moderate sun, moderate temperature
                    (double)arrayCloud2[i] >= 50 && (double)arrayCloud2[i] <= 90 &&
                    (double)arrayTemperature2[i] <= 25 && (double)arrayTemperature2[i] >= 15)
                {
                    row[0] = arrayDate2[i];
                    row[1] = (double)arrayWind2[i];
                    row[2] = (double)arrayCloud2[i];
                    row[3] = (double)arrayTemperature2[i];
                    row[4] = "Slight reduction due to some wind.";
                    dataTableWeather2.Rows.Add(row);
                }
                else if ((double)arrayWind2[i] >= 30 && (double)arrayWind2[i] <= 90 && //Medium to strong wind, moderate sun, moderate temperature
                    (double)arrayCloud2[i] >= 50 && (double)arrayCloud2[i] <= 90 &&
                    (double)arrayTemperature2[i] <= 25 && (double)arrayTemperature2[i] >= 15)
                {
                    row[0] = arrayDate2[i];
                    row[1] = (double)arrayWind2[i];
                    row[2] = (double)arrayCloud2[i];
                    row[3] = (double)arrayTemperature2[i];
                    row[4] = "Reduction due to strong wind.";
                    dataTableWeather2.Rows.Add(row);
                }
                else if ((double)arrayWind2[i] >= 30 && (double)arrayWind2[i] <= 90 && //Medium to strong wind, moderate sun, moderate temperature
                    (double)arrayCloud2[i] >= 50 && (double)arrayCloud2[i] <= 90 &&
                    (double)arrayTemperature2[i] <= 25 && (double)arrayTemperature2[i] >= 15)
                {
                    row[0] = arrayDate2[i];
                    row[1] = (double)arrayWind2[i];
                    row[2] = (double)arrayCloud2[i];
                    row[3] = (double)arrayTemperature2[i];
                    row[4] = "Reduction due to strong wind.";
                    dataTableWeather2.Rows.Add(row);
                }
                else if ((double)arrayWind2[i] >= 30 && (double)arrayWind2[i] <= 90 && //Medium to strong wind, moderate sun, moderate temperature
                    (double)arrayCloud2[i] >= 50 && (double)arrayCloud2[i] <= 90 &&
                    (double)arrayTemperature2[i] <= 25 && (double)arrayTemperature2[i] >= 15)
                {
                    row[0] = arrayDate2[i];
                    row[1] = (double)arrayWind2[i];
                    row[2] = (double)arrayCloud2[i];
                    row[3] = (double)arrayTemperature2[i];
                    row[4] = "Reduction due to strong wind.";
                    dataTableWeather2.Rows.Add(row);
                }
                else if ((double)arrayWind2[i] >= 30 && (double)arrayWind2[i] <= 90 && //Medium to strong wind, sunny, moderate temperature
                    (double)arrayCloud2[i] <= 50 &&
                    (double)arrayTemperature2[i] <= 25 && (double)arrayTemperature2[i] >= 15)
                {
                    row[0] = arrayDate1[i];
                    row[1] = (double)arrayWind2[i];
                    row[2] = (double)arrayCloud2[i];
                    row[3] = (double)arrayTemperature2[i];
                    row[4] = "Reduction due to strong wind and sun.";
                    dataTableWeather2.Rows.Add(row);
                }
                else if ((double)arrayWind2[i] >= 30 && (double)arrayWind2[i] <= 90 && //Medium to strong wind, sunny, moderate temperature
                    (double)arrayCloud2[i] <= 50 &&
                    (double)arrayTemperature2[i] <= 25 && (double)arrayTemperature2[i] >= 15)
                {
                    row[0] = arrayDate1[i];
                    row[1] = (double)arrayWind2[i];
                    row[2] = (double)arrayCloud2[i];
                    row[3] = (double)arrayTemperature2[i];
                    row[4] = "Reduction due to strong wind and sun.";
                    dataTableWeather2.Rows.Add(row);
                }
                else if ((double)arrayWind2[i] >= 30 && (double)arrayWind2[i] <= 90 && //Medium to strong wind, moderate sun, moderate temperature
                    (double)arrayCloud2[i] >= 50 && (double)arrayCloud2[i] <= 90 &&
                    (double)arrayTemperature2[i] <= 10)
                {
                    row[0] = arrayDate1[i];
                    row[1] = (double)arrayWind2[i];
                    row[2] = (double)arrayCloud2[i];
                    row[3] = (double)arrayTemperature2[i];
                    row[4] = "Reduction due to strong wind and sun.";
                    dataTableWeather2.Rows.Add(row);
                }
                else if ((double)arrayWind2[i] >= 30 && (double)arrayWind2[i] <= 90 && //Medium to strong wind, no sun, cold temperature
                    (double)arrayCloud2[i] >= 90 &&
                    (double)arrayTemperature2[i] <= 10)
                {
                    row[0] = arrayDate1[i];
                    row[1] = (double)arrayWind2[i];
                    row[2] = (double)arrayCloud2[i];
                    row[3] = (double)arrayTemperature2[i];
                    row[4] = "Slight to no reduction";
                    dataTableWeather2.Rows.Add(row);
                }
                else if ((double)arrayWind2[i] <= 15 && //no wind, no sun, very cold temperature
                    (double)arrayCloud2[i] >= 90 &&
                    (double)arrayTemperature2[i] <= 0)
                {
                    row[0] = arrayDate1[i];
                    row[1] = (double)arrayWind2[i];
                    row[2] = (double)arrayCloud2[i];
                    row[3] = (double)arrayTemperature2[i];
                    row[4] = "Increase due to cold temperature";
                    dataTableWeather2.Rows.Add(row);
                }
                else if ((double)arrayWind2[i] <= 15 && //no wind, no sun, very cold temperature
                    (double)arrayCloud2[i] >= 50 && (double)arrayCloud2[i] <=90 &&
                    (double)arrayTemperature2[i] <= 0)
                {
                    row[0] = arrayDate1[i];
                    row[1] = (double)arrayWind2[i];
                    row[2] = (double)arrayCloud2[i];
                    row[3] = (double)arrayTemperature2[i];
                    row[4] = "Increase due to low temperature";
                    dataTableWeather2.Rows.Add(row);
                }
                //Tweak the values here to recieve different output
                else
                {
                    row["Date"] = arrayDate1[i];
                    row["Wind Velocity (km/h)"] = (double)arrayWind2[i];
                    row["Cloud Coverage (%)"] = (double)arrayCloud2[i];
                    row["Temperature (°C)"] = (double)arrayTemperature2[i];
                    row["Expected change"] = "Low probability of change.";
                    dataTableWeather2.Rows.Add(row);
                }
            }
            return dataTableWeather2;
        }
        public async Task<DataTable> GetWeatherAsyncSE3()
        {
            string aPIRequest;

            aPIRequest = "https://weather.visualcrossing.com/VisualCrossingWebServices/rest/services/timeline/stockholm?unitGroup=metric&key=QDN24AE8877YDJTTM3MYA7RDS&contentType=json";
            

            var client = new HttpClient();

            var request = new HttpRequestMessage(HttpMethod.Get, aPIRequest);

            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode(); // Throw an exception if error

            var body = await response.Content.ReadAsStringAsync();

            dynamic weather = JsonConvert.DeserializeObject(body);

            foreach (var obj in weather.days)
            {
                string weather_Date = obj.datetime;
                try
                {
                    windSpeed3 = Convert.ToDouble(obj.windspeed);
                    cloudCover3 = Convert.ToDouble(obj.cloudcover);
                    weatherDate3 = Convert.ToDateTime(weather_Date);
                    temperature3 = Convert.ToDouble(obj.temp);
                    arrayWind3.Add(windSpeed3);
                    arrayCloud3.Add(cloudCover3);
                    arrayDate3.Add(weather_Date);
                    arrayTemperature3.Add(temperature3);
                }
                catch (Exception ex)
                {

                }
            }
            DataColumn dataColumn;
            //Create Date column.
            dataColumn = new DataColumn();
            dataColumn.ColumnName = "Date";
            dataColumn.DataType = typeof(DateTime);
            dataTableWeather3.Columns.Add(dataColumn);
            //Create Wind velocity column.
            dataColumn = new DataColumn();
            dataColumn.ColumnName = "Wind Velocity (km/h)";
            dataColumn.DataType = typeof(double);
            dataTableWeather3.Columns.Add(dataColumn);
            //Create Cloud Coverage column.
            dataColumn = new DataColumn();
            dataColumn.ColumnName = "Cloud Coverage (%)";
            dataColumn.DataType = typeof(double);
            dataTableWeather3.Columns.Add(dataColumn);
            //Create the Temperature column.
            dataColumn = new DataColumn();
            dataColumn.ColumnName = "Temperature (°C)";
            dataColumn.DataType = typeof(double);
            dataTableWeather3.Columns.Add(dataColumn);
            //Create the recommendation column.
            dataColumn = new DataColumn();
            dataColumn.ColumnName = "Expected change";
            dataColumn.DataType = typeof(string);
            dataTableWeather3.Columns.Add(dataColumn);
            for (int i = 0; i < 7; i++)
            {
                //Tweak the values here to recieve different output
                DataRow row = dataTableWeather3.NewRow();
                //wind between 15-90 km/h. Between 15-30 effects litle, 30-60 more, 60-90 a lot
                //Cloud between 0-25% effects a lot, 25-50 slightly less, 50-75 a litle, 75-100 no effect
                //Temperature minus 0 effects a lot, 0-10 slightly less, 10-15 a litle, 15-25 nothing
                if ((double)arrayWind3[i] < 15 && //No wind, No sun, moderate temperature
                    (double)arrayCloud3[i] >= 90 && (double)arrayCloud3[i] <= 100 &&
                    (double)arrayTemperature3[i] <= 25 && (double)arrayTemperature3[i] >= 15)
                {
                    row[0] = arrayDate3[i];
                    row[1] = (double)arrayWind3[i];
                    row[2] = (double)arrayCloud3[i];
                    row[3] = (double)arrayTemperature3[i];
                    row[4] = "No expected price change"; //No weather impact on price
                    dataTableWeather3.Rows.Add(row);
                }
                else if ((double)arrayWind3[i] < 15 && //No wind, sunny, cold temperature
                    (double)arrayCloud3[i] <= 50 &&
                    (double)arrayTemperature3[i] <= 10 && (double)arrayTemperature3[i] >= -10)
                {
                    row[0] = arrayDate3[i];
                    row[1] = (double)arrayWind3[i];
                    row[2] = (double)arrayCloud3[i];
                    row[3] = (double)arrayTemperature3[i];
                    row[4] = "Reduction due to sun";
                    dataTableWeather3.Rows.Add(row);
                }
                else if ((double)arrayWind3[i] < 15 && //No wind, sunny, cold temperature
                    (double)arrayCloud3[i] <= 50 &&
                    (double)arrayTemperature3[i] <= 0 && (double)arrayTemperature3[i] >= -20)
                {
                    row[0] = arrayDate3[i];
                    row[1] = (double)arrayWind3[i];
                    row[2] = (double)arrayCloud3[i];
                    row[3] = (double)arrayTemperature3[i];
                    row[4] = "No expected change due to cold weather and sun";
                    dataTableWeather3.Rows.Add(row);
                }
                //Tweak the values here to recieve different output
                else if ((double)arrayWind3[i] >= 15 && (double)arrayWind3[i] <= 30 && //Low wind, No sun, moderate temperature
                    (double)arrayCloud3[i] >= 75 && (double)arrayCloud3[i] <= 90 &&
                    (double)arrayTemperature3[i] <= 25 && (double)arrayTemperature3[i] >= 15)
                {
                    row[0] = arrayDate3[i];
                    row[1] = (double)arrayWind3[i];
                    row[2] = (double)arrayCloud3[i];
                    row[3] = (double)arrayTemperature3[i];
                    row[4] = "Slight reduction due to some wind.";
                    dataTableWeather3.Rows.Add(row);
                }
                else if ((double)arrayWind3[i] >= 15 && (double)arrayWind3[i] <= 30 &&//Low wind, moderate sun, moderate temperature
                    (double)arrayCloud3[i] >= 50 && (double)arrayCloud3[i] <= 90 &&
                    (double)arrayTemperature3[i] <= 25 && (double)arrayTemperature3[i] >= 15)
                {
                    row[0] = arrayDate3[i];
                    row[1] = (double)arrayWind3[i];
                    row[2] = (double)arrayCloud3[i];
                    row[3] = (double)arrayTemperature3[i];
                    row[4] = "Slight reduction due to some wind.";
                    dataTableWeather3.Rows.Add(row);
                }
                else if ((double)arrayWind3[i] >= 15 && (double)arrayWind3[i] <= 60 &&//Low to medium wind, moderate sun, low temperature
                    (double)arrayCloud3[i] >= 50 && (double)arrayCloud3[i] <= 90 &&
                    (double)arrayTemperature3[i] <= 10)
                {
                    row[0] = arrayDate1[i];
                    row[1] = (double)arrayWind3[i];
                    row[2] = (double)arrayCloud3[i];
                    row[3] = (double)arrayTemperature3[i];
                    row[4] = "Slight increase due to cold temperature";
                    dataTableWeather3.Rows.Add(row);
                }
                else if ((double)arrayWind3[i] >= 15 && (double)arrayWind3[i] <= 60 &&//low to medium wind, sunny, cold temperature
                    (double)arrayCloud3[i] <= 50 &&
                    (double)arrayTemperature3[i] <= 10 && (double)arrayTemperature3[i] >= 0)
                {
                    row[0] = arrayDate3[i];
                    row[1] = (double)arrayWind3[i];
                    row[2] = (double)arrayCloud3[i];
                    row[3] = (double)arrayTemperature3[i];
                    row[4] = "Slight reduction due to some wind and sun";
                    dataTableWeather3.Rows.Add(row);
                }
                else if ((double)arrayWind3[i] >= 15 && (double)arrayWind3[i] <= 60 &&//low to medium wind, sunny, very cold temperature
                    (double)arrayCloud3[i] <= 50 &&
                    (double)arrayTemperature3[i] <= 10)
                {
                    row[0] = arrayDate3[i];
                    row[1] = (double)arrayWind3[i];
                    row[2] = (double)arrayCloud3[i];
                    row[3] = (double)arrayTemperature3[i];
                    row[4] = "No expected price change";
                    dataTableWeather3.Rows.Add(row);
                }
                else if ((double)arrayWind3[i] >= 15 && (double)arrayWind3[i] <= 60 && //Low to medium wind, moderate sun, moderate temperature
                    (double)arrayCloud3[i] >= 50 && (double)arrayCloud3[i] <= 90 &&
                    (double)arrayTemperature3[i] <= 25 && (double)arrayTemperature3[i] >= 15)
                {
                    row[0] = arrayDate3[i];
                    row[1] = (double)arrayWind3[i];
                    row[2] = (double)arrayCloud3[i];
                    row[3] = (double)arrayTemperature3[i];
                    row[4] = "Slight reduction due to some wind.";
                    dataTableWeather3.Rows.Add(row);
                }
                else if ((double)arrayWind3[i] >= 30 && (double)arrayWind3[i] <= 90 && //Medium to strong wind, moderate sun, moderate temperature
                    (double)arrayCloud3[i] >= 50 && (double)arrayCloud3[i] <= 90 &&
                    (double)arrayTemperature3[i] <= 25 && (double)arrayTemperature3[i] >= 15)
                {
                    row[0] = arrayDate3[i];
                    row[1] = (double)arrayWind3[i];
                    row[2] = (double)arrayCloud3[i];
                    row[3] = (double)arrayTemperature3[i];
                    row[4] = "Reduction due to strong wind.";
                    dataTableWeather3.Rows.Add(row);
                }
                else if ((double)arrayWind3[i] >= 30 && (double)arrayWind3[i] <= 90 && //Medium to strong wind, moderate sun, moderate temperature
                    (double)arrayCloud3[i] >= 50 && (double)arrayCloud3[i] <= 90 &&
                    (double)arrayTemperature3[i] <= 25 && (double)arrayTemperature3[i] >= 15)
                {
                    row[0] = arrayDate3[i];
                    row[1] = (double)arrayWind3[i];
                    row[2] = (double)arrayCloud3[i];
                    row[3] = (double)arrayTemperature3[i];
                    row[4] = "Reduction due to strong wind.";
                    dataTableWeather3.Rows.Add(row);
                }
                else if ((double)arrayWind3[i] >= 30 && (double)arrayWind3[i] <= 90 && //Medium to strong wind, moderate sun, moderate temperature
                    (double)arrayCloud3[i] >= 50 && (double)arrayCloud3[i] <= 90 &&
                    (double)arrayTemperature3[i] <= 25 && (double)arrayTemperature3[i] >= 15)
                {
                    row[0] = arrayDate3[i];
                    row[1] = (double)arrayWind3[i];
                    row[2] = (double)arrayCloud3[i];
                    row[3] = (double)arrayTemperature3[i];
                    row[4] = "Reduction due to strong wind.";
                    dataTableWeather3.Rows.Add(row);
                }
                else if ((double)arrayWind3[i] >= 30 && (double)arrayWind3[i] <= 90 && //Medium to strong wind, sunny, moderate temperature
                    (double)arrayCloud3[i] <= 50 &&
                    (double)arrayTemperature3[i] <= 25 && (double)arrayTemperature3[i] >= 15)
                {
                    row[0] = arrayDate1[i];
                    row[1] = (double)arrayWind3[i];
                    row[2] = (double)arrayCloud3[i];
                    row[3] = (double)arrayTemperature3[i];
                    row[4] = "Reduction due to strong wind and sun.";
                    dataTableWeather3.Rows.Add(row);
                }
                else if ((double)arrayWind3[i] >= 30 && (double)arrayWind3[i] <= 90 && //Medium to strong wind, sunny, moderate temperature
                    (double)arrayCloud3[i] <= 50 &&
                    (double)arrayTemperature3[i] <= 25 && (double)arrayTemperature3[i] >= 15)
                {
                    row[0] = arrayDate1[i];
                    row[1] = (double)arrayWind3[i];
                    row[2] = (double)arrayCloud3[i];
                    row[3] = (double)arrayTemperature3[i];
                    row[4] = "Reduction due to strong wind and sun.";
                    dataTableWeather3.Rows.Add(row);
                }
                else if ((double)arrayWind3[i] >= 30 && (double)arrayWind3[i] <= 90 && //Medium to strong wind, moderate sun, moderate temperature
                    (double)arrayCloud3[i] >= 50 && (double)arrayCloud3[i] <= 90 &&
                    (double)arrayTemperature3[i] <= 10)
                {
                    row[0] = arrayDate1[i];
                    row[1] = (double)arrayWind3[i];
                    row[2] = (double)arrayCloud3[i];
                    row[3] = (double)arrayTemperature3[i];
                    row[4] = "Reduction due to strong wind and sun.";
                    dataTableWeather3.Rows.Add(row);
                }
                else if ((double)arrayWind3[i] >= 30 && (double)arrayWind3[i] <= 90 && //Medium to strong wind, no sun, cold temperature
                    (double)arrayCloud3[i] >= 90 &&
                    (double)arrayTemperature3[i] <= 10)
                {
                    row[0] = arrayDate1[i];
                    row[1] = (double)arrayWind3[i];
                    row[2] = (double)arrayCloud3[i];
                    row[3] = (double)arrayTemperature3[i];
                    row[4] = "Slight to no reduction";
                    dataTableWeather3.Rows.Add(row);
                }
                else if ((double)arrayWind3[i] <= 15 && //no wind, no sun, very cold temperature
                    (double)arrayCloud3[i] >= 90 &&
                    (double)arrayTemperature3[i] <= 0)
                {
                    row[0] = arrayDate1[i];
                    row[1] = (double)arrayWind3[i];
                    row[2] = (double)arrayCloud3[i];
                    row[3] = (double)arrayTemperature3[i];
                    row[4] = "High increase due to cold temperature";
                    dataTableWeather3.Rows.Add(row);
                }
                else if ((double)arrayWind3[i] <= 15 && //no wind, moderate sun, very cold temperature
                    (double)arrayCloud3[i] >= 50 && (double)arrayCloud3[i] <= 90 &&
                    (double)arrayTemperature3[i] <= 0)
                {
                    row[0] = arrayDate1[i];
                    row[1] = (double)arrayWind3[i];
                    row[2] = (double)arrayCloud3[i];
                    row[3] = (double)arrayTemperature3[i];
                    row[4] = "Increase due to low temperature";
                    dataTableWeather3.Rows.Add(row);
                }
                else if ((double)arrayWind3[i] >= 15 && (double)arrayWind3[i] <= 30 &&//low wind, no sun, very cold temperature
                    (double)arrayCloud3[i] >= 90 &&
                    (double)arrayTemperature3[i] <= 0)
                {
                    row[0] = arrayDate1[i];
                    row[1] = (double)arrayWind3[i];
                    row[2] = (double)arrayCloud3[i];
                    row[3] = (double)arrayTemperature3[i];
                    row[4] = "Increase due to low temperature and low wind";
                    dataTableWeather3.Rows.Add(row);
                }
                //Tweak the values here to recieve different output
                else
                {
                    row["Date"] = arrayDate1[i];
                    row["Wind Velocity (km/h)"] = (double)arrayWind3[i];
                    row["Cloud Coverage (%)"] = (double)arrayCloud3[i];
                    row["Temperature (°C)"] = (double)arrayTemperature3[i];
                    row["Expected change"] = "Low probability of change.";
                    dataTableWeather3.Rows.Add(row);
                }
            }
            return dataTableWeather3;
        }
        public async Task<DataTable> GetWeatherAsyncSE4()
        {
            string aPIRequest;

            aPIRequest = "https://weather.visualcrossing.com/VisualCrossingWebServices/rest/services/timeline/lund?unitGroup=metric&key=QDN24AE8877YDJTTM3MYA7RDS&contentType=json";
            

            var client = new HttpClient();

            var request = new HttpRequestMessage(HttpMethod.Get, aPIRequest);

            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode(); // Throw an exception if error

            var body = await response.Content.ReadAsStringAsync();

            dynamic weather = JsonConvert.DeserializeObject(body);

            foreach (var obj in weather.days)
            {
                string weather_Date = obj.datetime;
                try
                {
                    windSpeed4 = Convert.ToDouble(obj.windspeed);
                    cloudCover4 = Convert.ToDouble(obj.cloudcover);
                    weatherDate3 = Convert.ToDateTime(weather_Date);
                    temperature4 = Convert.ToDouble(obj.temp);
                    arrayWind4.Add(windSpeed4);
                    arrayCloud4.Add(cloudCover4);
                    arrayDate4.Add(weather_Date);
                    arrayTemperature4.Add(temperature4);
                }
                catch (Exception ex)
                {

                }
            }
            DataColumn dataColumn;
            //Create Date column.
            dataColumn = new DataColumn();
            dataColumn.ColumnName = "Date";
            dataColumn.DataType = typeof(DateTime);
            dataTableWeather4.Columns.Add(dataColumn);
            //Create Wind velocity column.
            dataColumn = new DataColumn();
            dataColumn.ColumnName = "Wind Velocity (km/h)";
            dataColumn.DataType = typeof(double);
            dataTableWeather4.Columns.Add(dataColumn);
            //Create Cloud Coverage column.
            dataColumn = new DataColumn();
            dataColumn.ColumnName = "Cloud Coverage (%)";
            dataColumn.DataType = typeof(double);
            dataTableWeather4.Columns.Add(dataColumn);
            //Create the Temperature column.
            dataColumn = new DataColumn();
            dataColumn.ColumnName = "Temperature (°C)";
            dataColumn.DataType = typeof(double);
            dataTableWeather4.Columns.Add(dataColumn);
            //Create the recommendation column.
            dataColumn = new DataColumn();
            dataColumn.ColumnName = "Expected change";
            dataColumn.DataType = typeof(string);
            dataTableWeather4.Columns.Add(dataColumn);
            for (int i = 0; i < 7; i++)
            {
                //Tweak the values here to recieve different output
                DataRow row = dataTableWeather4.NewRow();
                //wind between 15-90 km/h. Between 15-30 effects litle, 30-60 more, 60-90 a lot
                //Cloud between 0-25% effects a lot, 25-50 slightly less, 50-75 a litle, 75-100 no effect
                //Temperature minus 0 effects a lot, 0-10 slightly less, 10-15 a litle, 15-25 nothing
                if ((double)arrayWind4[i] < 15 && //No wind, No sun, moderate temperature
                    (double)arrayCloud4[i] >= 90 && (double)arrayCloud4[i] <= 100 &&
                    (double)arrayTemperature4[i] <= 25 && (double)arrayTemperature4[i] >= 15)
                {
                    row[0] = arrayDate4[i];
                    row[1] = (double)arrayWind4[i];
                    row[2] = (double)arrayCloud4[i];
                    row[3] = (double)arrayTemperature4[i];
                    row[4] = "No expected price change"; //No weather impact on price
                    dataTableWeather4.Rows.Add(row);
                }
                else if ((double)arrayWind4[i] < 15 && //No wind, sunny, cold temperature
                    (double)arrayCloud4[i] <= 50 &&
                    (double)arrayTemperature4[i] <= 10 && (double)arrayTemperature4[i] >= -10)
                {
                    row[0] = arrayDate4[i];
                    row[1] = (double)arrayWind4[i];
                    row[2] = (double)arrayCloud4[i];
                    row[3] = (double)arrayTemperature4[i];
                    row[4] = "Reduction due to sun";
                    dataTableWeather4.Rows.Add(row);
                }
                else if ((double)arrayWind4[i] < 15 && //No wind, sunny, cold temperature
                    (double)arrayCloud4[i] <= 50 &&
                    (double)arrayTemperature4[i] <= 0 && (double)arrayTemperature4[i] >= -20)
                {
                    row[0] = arrayDate4[i];
                    row[1] = (double)arrayWind4[i];
                    row[2] = (double)arrayCloud4[i];
                    row[3] = (double)arrayTemperature4[i];
                    row[4] = "No expected change due to cold weather and sun";
                    dataTableWeather4.Rows.Add(row);
                }
                //Tweak the values here to recieve different output
                else if ((double)arrayWind4[i] >= 15 && (double)arrayWind4[i] <= 30 && //Low wind, No sun, moderate temperature
                    (double)arrayCloud4[i] >= 75 && (double)arrayCloud4[i] <= 90 &&
                    (double)arrayTemperature4[i] <= 25 && (double)arrayTemperature4[i] >= 15)
                {
                    row[0] = arrayDate4[i];
                    row[1] = (double)arrayWind4[i];
                    row[2] = (double)arrayCloud4[i];
                    row[3] = (double)arrayTemperature4[i];
                    row[4] = "Slight reduction due to some wind.";
                    dataTableWeather4.Rows.Add(row);
                }
                else if ((double)arrayWind4[i] >= 15 && (double)arrayWind4[i] <= 30 &&//Low wind, moderate sun, moderate temperature
                    (double)arrayCloud4[i] >= 50 && (double)arrayCloud4[i] <= 90 &&
                    (double)arrayTemperature4[i] <= 25 && (double)arrayTemperature4[i] >= 15)
                {
                    row[0] = arrayDate4[i];
                    row[1] = (double)arrayWind4[i];
                    row[2] = (double)arrayCloud4[i];
                    row[3] = (double)arrayTemperature4[i];
                    row[4] = "Slight reduction due to some wind.";
                    dataTableWeather4.Rows.Add(row);
                }
                else if ((double)arrayWind4[i] >= 15 && (double)arrayWind4[i] <= 60 &&//Low to medium wind, moderate sun, low temperature
                    (double)arrayCloud4[i] >= 50 && (double)arrayCloud4[i] <= 90 &&
                    (double)arrayTemperature4[i] <= 10)
                {
                    row[0] = arrayDate1[i];
                    row[1] = (double)arrayWind4[i];
                    row[2] = (double)arrayCloud4[i];
                    row[3] = (double)arrayTemperature4[i];
                    row[4] = "Slight increase due to cold temperature";
                    dataTableWeather4.Rows.Add(row);
                }
                else if ((double)arrayWind4[i] >= 15 && (double)arrayWind4[i] <= 60 &&//low to medium wind, sunny, cold temperature
                    (double)arrayCloud4[i] <= 50 &&
                    (double)arrayTemperature4[i] <= 10 && (double)arrayTemperature4[i] >= 0)
                {
                    row[0] = arrayDate4[i];
                    row[1] = (double)arrayWind4[i];
                    row[2] = (double)arrayCloud4[i];
                    row[3] = (double)arrayTemperature4[i];
                    row[4] = "Slight reduction due to some wind and sun";
                    dataTableWeather4.Rows.Add(row);
                }
                else if ((double)arrayWind4[i] >= 15 && (double)arrayWind4[i] <= 60 &&//low to medium wind, sunny, very cold temperature
                    (double)arrayCloud4[i] <= 50 &&
                    (double)arrayTemperature4[i] <= 10)
                {
                    row[0] = arrayDate4[i];
                    row[1] = (double)arrayWind4[i];
                    row[2] = (double)arrayCloud4[i];
                    row[3] = (double)arrayTemperature4[i];
                    row[4] = "No expected price change";
                    dataTableWeather4.Rows.Add(row);
                }
                else if ((double)arrayWind4[i] >= 15 && (double)arrayWind4[i] <= 60 && //Low to medium wind, moderate sun, moderate temperature
                    (double)arrayCloud4[i] >= 50 && (double)arrayCloud4[i] <= 90 &&
                    (double)arrayTemperature4[i] <= 25 && (double)arrayTemperature4[i] >= 15)
                {
                    row[0] = arrayDate4[i];
                    row[1] = (double)arrayWind4[i];
                    row[2] = (double)arrayCloud4[i];
                    row[3] = (double)arrayTemperature4[i];
                    row[4] = "Slight reduction due to some wind.";
                    dataTableWeather4.Rows.Add(row);
                }
                else if ((double)arrayWind4[i] >= 30 && (double)arrayWind4[i] <= 90 && //Medium to strong wind, moderate sun, moderate temperature
                    (double)arrayCloud4[i] >= 50 && (double)arrayCloud4[i] <= 90 &&
                    (double)arrayTemperature4[i] <= 25 && (double)arrayTemperature4[i] >= 15)
                {
                    row[0] = arrayDate4[i];
                    row[1] = (double)arrayWind4[i];
                    row[2] = (double)arrayCloud4[i];
                    row[3] = (double)arrayTemperature4[i];
                    row[4] = "Reduction due to strong wind.";
                    dataTableWeather4.Rows.Add(row);
                }
                else if ((double)arrayWind4[i] >= 30 && (double)arrayWind4[i] <= 90 && //Medium to strong wind, moderate sun, moderate temperature
                    (double)arrayCloud4[i] >= 50 && (double)arrayCloud4[i] <= 90 &&
                    (double)arrayTemperature4[i] <= 25 && (double)arrayTemperature4[i] >= 15)
                {
                    row[0] = arrayDate4[i];
                    row[1] = (double)arrayWind4[i];
                    row[2] = (double)arrayCloud4[i];
                    row[3] = (double)arrayTemperature4[i];
                    row[4] = "Reduction due to strong wind.";
                    dataTableWeather4.Rows.Add(row);
                }
                else if ((double)arrayWind4[i] >= 30 && (double)arrayWind4[i] <= 90 && //Medium to strong wind, moderate sun, moderate temperature
                    (double)arrayCloud4[i] >= 50 && (double)arrayCloud4[i] <= 90 &&
                    (double)arrayTemperature4[i] <= 25 && (double)arrayTemperature4[i] >= 15)
                {
                    row[0] = arrayDate4[i];
                    row[1] = (double)arrayWind4[i];
                    row[2] = (double)arrayCloud4[i];
                    row[3] = (double)arrayTemperature4[i];
                    row[4] = "Reduction due to strong wind.";
                    dataTableWeather4.Rows.Add(row);
                }
                else if ((double)arrayWind4[i] >= 30 && (double)arrayWind4[i] <= 90 && //Medium to strong wind, sunny, moderate temperature
                    (double)arrayCloud4[i] <= 50 &&
                    (double)arrayTemperature4[i] <= 25 && (double)arrayTemperature4[i] >= 15)
                {
                    row[0] = arrayDate4[i];
                    row[1] = (double)arrayWind4[i];
                    row[2] = (double)arrayCloud4[i];
                    row[3] = (double)arrayTemperature4[i];
                    row[4] = "Reduction due to strong wind and sun.";
                    dataTableWeather4.Rows.Add(row);
                }
                else if ((double)arrayWind4[i] >= 30 && (double)arrayWind4[i] <= 90 && //Medium to strong wind, sunny, moderate temperature
                    (double)arrayCloud4[i] <= 50 &&
                    (double)arrayTemperature4[i] <= 25 && (double)arrayTemperature4[i] >= 15)
                {
                    row[0] = arrayDate4[i];
                    row[1] = (double)arrayWind4[i];
                    row[2] = (double)arrayCloud4[i];
                    row[3] = (double)arrayTemperature4[i];
                    row[4] = "Reduction due to strong wind and sun.";
                    dataTableWeather4.Rows.Add(row);
                }
                else if ((double)arrayWind4[i] >= 30 && (double)arrayWind4[i] <= 90 && //Medium to strong wind, moderate sun, moderate temperature
                    (double)arrayCloud4[i] >= 50 && (double)arrayCloud4[i] <= 90 &&
                    (double)arrayTemperature4[i] <= 10)
                {
                    row[0] = arrayDate4[i];
                    row[1] = (double)arrayWind4[i];
                    row[2] = (double)arrayCloud4[i];
                    row[3] = (double)arrayTemperature4[i];
                    row[4] = "Reduction due to strong wind and sun.";
                    dataTableWeather4.Rows.Add(row);
                }
                else if ((double)arrayWind4[i] >= 30 && (double)arrayWind4[i] <= 90 && //Medium to strong wind, no sun, cold temperature
                    (double)arrayCloud4[i] >= 90 &&
                    (double)arrayTemperature4[i] <= 10)
                {
                    row[0] = arrayDate4[i];
                    row[1] = (double)arrayWind4[i];
                    row[2] = (double)arrayCloud4[i];
                    row[3] = (double)arrayTemperature4[i];
                    row[4] = "Slight to no reduction";
                    dataTableWeather4.Rows.Add(row);
                }
                else if ((double)arrayWind4[i] <= 15 && //no wind, no sun, very cold temperature
                    (double)arrayCloud4[i] >= 90 &&
                    (double)arrayTemperature4[i] <= 0)
                {
                    row[0] = arrayDate4[i];
                    row[1] = (double)arrayWind4[i];
                    row[2] = (double)arrayCloud4[i];
                    row[3] = (double)arrayTemperature4[i];
                    row[4] = "Increase due to cold temperature";
                    dataTableWeather4.Rows.Add(row);
                }
                else if ((double)arrayWind4[i] <= 15 && //no wind, moderate sun, very cold temperature
                    (double)arrayCloud4[i] >= 50 && (double)arrayCloud4[i] <= 90 &&
                    (double)arrayTemperature4[i] <= 0)
                {
                    row[0] = arrayDate4[i];
                    row[1] = (double)arrayWind4[i];
                    row[2] = (double)arrayCloud4[i];
                    row[3] = (double)arrayTemperature4[i];
                    row[4] = "Increase due to low temperature";
                    dataTableWeather4.Rows.Add(row);
                }
                else if ((double)arrayWind4[i] >= 15 && (double)arrayWind4[i] <= 30 &&//low wind, no sun, very cold temperature
                    (double)arrayCloud4[i] >= 90 &&
                    (double)arrayTemperature4[i] <= 0)
                {
                    row[0] = arrayDate4[i];
                    row[1] = (double)arrayWind4[i];
                    row[2] = (double)arrayCloud4[i];
                    row[3] = (double)arrayTemperature4[i];
                    row[4] = "Increase due to low temperature and low wind";
                    dataTableWeather4.Rows.Add(row);
                }
                //Tweak the values here to recieve different output
                else
                {
                    row["Date"] = arrayDate4[i];
                    row["Wind Velocity (km/h)"] = (double)arrayWind4[i];
                    row["Cloud Coverage (%)"] = (double)arrayCloud4[i];
                    row["Temperature (°C)"] = (double)arrayTemperature4[i];
                    row["Expected change"] = "Low probability of change.";
                    dataTableWeather4.Rows.Add(row);
                }
            }
            return dataTableWeather4;
        }
    }
}
