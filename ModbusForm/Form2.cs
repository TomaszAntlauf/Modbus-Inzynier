using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using EasyModbus;

namespace ModbusForm
{
    public partial class Form2 : Form
    {
        public int x;
        CancellationTokenSource cts = new CancellationTokenSource();
        private int rec;
        public ModbusClient modbusClient2;
        public string ip2;
        public int port2;
        public Form2()
        {
            InitializeComponent();
        }

        private void buttonWyb_Click(object sender, EventArgs e)
        {
            modbusClient2 = new ModbusClient(ip2, port2);
            modbusClient2.Connect();

            if (x == 1)
            {
                cts.Cancel();
                cts.Dispose();
                cts = new CancellationTokenSource();

                Task.Factory.StartNew(() => CoilOdczytAdd(cts.Token));

            }
            else if (x == 2)
            {
                cts.Cancel();
                cts.Dispose();
                cts = new CancellationTokenSource();

                Task.Factory.StartNew(() => InputSOdczytAdd(cts.Token));
            }
            else if (x == 3)
            {
                cts.Cancel();
                cts.Dispose();
                cts = new CancellationTokenSource();

                Task.Factory.StartNew(() => HoldingOdczytAdd(cts.Token));
            }
            else if (x == 4)
            {
                cts.Cancel();
                cts.Dispose();
                cts = new CancellationTokenSource();

                Task.Factory.StartNew(() => InputROdczytAdd(cts.Token));
            }
        }
        
        private void CoilOdczytAdd(CancellationToken token) 
        {
            rec = Int32.Parse(wybBox.Text);
            var values = modbusClient2.ReadCoils(rec, 1);
            ChartTimeData chartTimeDate = new ChartTimeData();
            while (true)
            {
                if (token.IsCancellationRequested)
                {
                    break;
                }
                else
                {
                    values = modbusClient2.ReadCoils(rec, 1);
                    
                    chartTimeDate.Day = DateTime.Now;
                    if (values[0] == true)
                    {
                        chartTimeDate.Value = 1;
                    }
                    else if (values[0] == false)
                    {
                        chartTimeDate.Value = 0;
                    }
                    chartAdd.Invoke((Action)(() => 
                    chartAdd.Series["Wartość"].Points.AddXY(chartTimeDate.Day.ToString(), chartTimeDate.Value)));
                }
                Thread.Sleep(1000);
            }
        }

        private void InputSOdczytAdd(CancellationToken token) 
        {
            rec = Int32.Parse(wybBox.Text);
            var values = modbusClient2.ReadDiscreteInputs(rec, 1);
            ChartTimeData chartTimeDate = new ChartTimeData();
            while (true)
            {
                if (token.IsCancellationRequested)
                {
                    break;
                }
                else
                {
                    values = modbusClient2.ReadDiscreteInputs(rec, 1);
                    
                    chartTimeDate.Day = DateTime.Now;
                    if (values[0] == true)
                    {
                        chartTimeDate.Value = 1;
                    }
                    else if (values[0] == false)
                    {
                        chartTimeDate.Value = 0;
                    }
                    chartAdd.Invoke((Action)(() => 
                    chartAdd.Series["Wartość"].Points.AddXY(chartTimeDate.Day.ToString(), chartTimeDate.Value)));
                }
                Thread.Sleep(1000);
            }
        }

        private void HoldingOdczytAdd(CancellationToken token) 
        {
            rec = Int32.Parse(wybBox.Text);
            var values = modbusClient2.ReadHoldingRegisters(rec, 1);
            ChartTimeData chartTimeDate = new ChartTimeData();
            while (true)
            {
                if (token.IsCancellationRequested)
                {
                    break;
                }
                else
                {
                    values = modbusClient2.ReadHoldingRegisters(rec, 1);

                    chartTimeDate.Day = DateTime.Now;

                    chartTimeDate.Value = values[0];
                    
                    chartAdd.Invoke((Action)(() => 
                    chartAdd.Series["Wartość"].Points.AddXY(chartTimeDate.Day.ToString(), chartTimeDate.Value)));
                }
                Thread.Sleep(1000);
            }
        }

        private void InputROdczytAdd(CancellationToken token) 
        {
            rec = Int32.Parse(wybBox.Text);
            var values = modbusClient2.ReadInputRegisters(rec, 1);
            ChartTimeData chartTimeDate = new ChartTimeData();
            while (true)
            {
                if (token.IsCancellationRequested)
                {
                    break;
                }
                else
                {
                    values = modbusClient2.ReadInputRegisters(rec, 1);

                    chartTimeDate.Day = DateTime.Now;

                    chartTimeDate.Value = values[0];

                    chartAdd.Invoke((Action)(() => 
                    chartAdd.Series["Wartość"].Points.AddXY(chartTimeDate.Day.ToString(), chartTimeDate.Value)));
                }
                Thread.Sleep(1000);
            }
        }

        private void InteruptBtn2_Click(object sender, EventArgs e)
        {
            chartAdd.Invoke((Action)(() => chartAdd.Series["Wartość"].Points.Clear()));
            cts.Cancel();
        }
    }

    public class ChartTimeData
    {
        public DateTime Day { get; set; }
        public int Value { get; set; }
    }
}
