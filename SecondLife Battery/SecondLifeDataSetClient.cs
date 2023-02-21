﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SecondLife_Battery
{
    public partial class SecondLifeDataSetClient : Form
    {
        DataAccessLayer dal = new DataAccessLayer();
        public SecondLifeDataSetClient()
        {
            InitializeComponent();
        }

        private void DataGridViewElectricityPrices(object sender, DataGridViewCellEventArgs e)
        {

        }

        private async void GetResult_Click(object sender, EventArgs e)
        {
            dal.ClearData();
            DateTime date = dateTimePicker.Value;
            dal.SetDate(date);
            dataGridViewElectricityPrices.DataSource = dal.GetElectricityPrice();
            await dal.GetWeatherAsync();
            
        }

        private void DateTimePicker_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
