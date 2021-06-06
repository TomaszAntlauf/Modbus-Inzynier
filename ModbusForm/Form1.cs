using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using EasyModbus;

namespace ModbusForm
{
    public partial class Form1 : Form
    {
        public string ip;
        public int port;
        private int start;
        private int startUp;
        private int licznik;
        public ModbusClient modbusClient;
        double x = 1;
        private delegate void SafeCallDelegate(object obj);
        CancellationTokenSource cts = new CancellationTokenSource();
        bool ChEnable = false;
        int stan;


        public Form1()
        {
            InitializeComponent();
            functionBox.Items.Add("01 Read Coil Status");
            functionBox.Items.Add("02 Read Input Status");
            functionBox.Items.Add("03 Read Holding Registers");
            functionBox.Items.Add("04 Read Input Registers");

            functionBox2.Items.Add("05 Write Single Coil");
            functionBox2.Items.Add("06 Write Single Register");
            functionBox2.Items.Add("15 Write Multiple Coils");
            functionBox2.Items.Add("16 Write Multiple Registers");
          
        }

        private void buttonCon_Click(object sender, EventArgs e)
        {
            ip = ipBox.Text;
            port = Int32.Parse(portBox.Text);

                modbusClient = new ModbusClient(ip, port);         //Ip-Address and Port of Modbus-TCP-Server
                modbusClient.Connect();
                if (modbusClient.Connected == true)
                {
                    labelStan.Text = "Stan: Połączono";
                }
  
        }
        
        private void buttonExecute_Click(object sender, EventArgs e)
        {
            if ((string)functionBox.SelectedItem == "01 Read Coil Status")
            {
                cts.Cancel();
                cts.Dispose();
                cts = new CancellationTokenSource();

                stan = 1;
                
                Task.Factory.StartNew(() => CoilOdczyt(listBox1 , cts.Token));

            }
            else if ((string)functionBox.SelectedItem == "02 Read Input Status")
            {
                cts.Cancel();
                cts.Dispose();
                cts = new CancellationTokenSource();

                stan = 2;

                Task.Factory.StartNew(() => InputSOdczyt(listBox1, cts.Token));
            }
            else if ((string)functionBox.SelectedItem == "03 Read Holding Registers")
            {
                cts.Cancel();
                cts.Dispose();
                cts = new CancellationTokenSource();

                stan = 3;

                Task.Factory.StartNew(() => HoldingOdczyt(listBox1, cts.Token));
            }
            else if ((string)functionBox.SelectedItem == "04 Read Input Registers")
            {
                cts.Cancel();
                cts.Dispose();
                cts = new CancellationTokenSource();

                stan = 4;

                Task.Factory.StartNew(() => InputROdczyt(listBox1, cts.Token));
            }
            else
            {
                cts.Cancel();
                cts.Dispose();
                cts = new CancellationTokenSource();

                stan = 3;

                Task.Factory.StartNew(() => HoldingOdczyt(listBox1, cts.Token));
            }

        }
        
        private void CoilOdczyt(ListBox listBox, CancellationToken token)
        {
            start = Int32.Parse(addBox1.Text);
            licznik = Int32.Parse(addBox2.Text);
            var values = modbusClient.ReadCoils(start, licznik);
            ChartData chartDate = new ChartData();

            while (true)
            {
                if (token.IsCancellationRequested) 
                { 
                    break; 
                }
                else 
                {
                    if (listBox.InvokeRequired)
                    {
                        values = modbusClient.ReadCoils(start, licznik);
                        listBox.Invoke((Action)(() => listBox.Items.Clear()));
                        chartD.Invoke((Action)(() => chartD.Series["Wartość"].Points.Clear()));
                        int i = 0;

                        foreach (var item in values)
                        {
                            listBox.Invoke((Action)(() => listBox.Items.Add(item)));
                            if (ChEnable == true)
                            {
                                chartDate.IDex = start;
                                if (bool.TryParse(listBox.Items[i].ToString(), out bool output))
                                {
                                    if (bool.Parse(listBox.Items[i].ToString()) == false)
                                    {
                                        chartDate.Value = 0;
                                    }
                                    else if (bool.Parse(listBox.Items[i].ToString()) == true)
                                    {
                                        chartDate.Value = 1;
                                    }
                                }
                                chartD.Invoke((Action)(() => chartD.Series["Wartość"].Points.AddXY(chartDate.IDex, chartDate.Value)));
                            }
                            i++;
                            start++;
                        }
                        i = 0;
                        start = Int32.Parse(addBox1.Text);

                    }
                    Thread.Sleep(1000);
                }
            }
        }

        private void InputSOdczyt(ListBox listBox, CancellationToken token)
        {
            start = Int32.Parse(addBox1.Text);
            licznik = Int32.Parse(addBox2.Text);
            var values = modbusClient.ReadDiscreteInputs(start, licznik);
            ChartData chartDate = new ChartData();

            while (true)
            {
                if (token.IsCancellationRequested)
                {
                    break;
                }
                else
                {
                    
                    if (listBox.InvokeRequired)
                    {
                        values = modbusClient.ReadDiscreteInputs(start, licznik);
                        listBox.Invoke((Action)(() => listBox.Items.Clear()));
                        chartD.Invoke((Action)(() => chartD.Series["Wartość"].Points.Clear()));
                        int i = 0;

                        foreach (var item in values)
                        {
                            listBox.Invoke((Action)(() => listBox.Items.Add(item)));
                            if (ChEnable == true)
                            {
                                chartDate.IDex = start;
                                if (bool.TryParse(listBox1.Items[i].ToString(), out bool output))
                                {
                                    if (bool.Parse(listBox1.Items[i].ToString()) == false)
                                    {
                                        chartDate.Value = 0;
                                    }
                                    else if (bool.Parse(listBox1.Items[i].ToString()) == true)
                                    {
                                        chartDate.Value = 1;
                                    }
                                }
                                chartD.Invoke((Action)(() => chartD.Series["Wartość"].Points.AddXY(chartDate.IDex, chartDate.Value)));
                            }
                            i++;
                            start++;
                        }
                        i = 0;
                        start = Int32.Parse(addBox1.Text);

                    }
                    Thread.Sleep(1000);
                }
            }
        }

        private void HoldingOdczyt(ListBox listBox, CancellationToken token)
        {
            start = Int32.Parse(addBox1.Text);
            licznik = Int32.Parse(addBox2.Text);
            var values = modbusClient.ReadHoldingRegisters(start, licznik);
            ChartData chartDate = new ChartData();

            while (true)
            {
                if (token.IsCancellationRequested)
                {
                    break;
                }
                else
                {

                    if (listBox.InvokeRequired)
                    {
                        values = modbusClient.ReadHoldingRegisters(start, licznik);
                        listBox.Invoke((Action)(() => listBox.Items.Clear()));
                        chartD.Invoke((Action)(() => chartD.Series["Wartość"].Points.Clear()));
                        int i = 0;

                        foreach (var item in values)
                        {
                            listBox.Invoke((Action)(() => listBox.Items.Add(item)));
                            if (ChEnable == true)
                            {
                                chartDate.IDex = start;
                                chartDate.Value = int.Parse(listBox.Items[i].ToString());
                                chartD.Invoke((Action)(() => chartD.Series["Wartość"].Points.AddXY(chartDate.IDex, chartDate.Value)));
                            }
                            i++;
                            start++;
                        }
                        i = 0;
                        start = Int32.Parse(addBox1.Text); 

                    }
                    Thread.Sleep(1000);
                }
            }
        }

        private void InputROdczyt(ListBox listBox, CancellationToken token)
        {
            start = Int32.Parse(addBox1.Text);
            licznik = Int32.Parse(addBox2.Text);
            var values = modbusClient.ReadInputRegisters(start, licznik);
            ChartData chartDate = new ChartData();
            
            while (true)
            {
                if (token.IsCancellationRequested)
                {
                    break;
                }
                else
                {

                    if (listBox.InvokeRequired)
                    {
                        values = modbusClient.ReadInputRegisters(start, licznik);
                        listBox.Invoke((Action)(() => listBox.Items.Clear()));
                        chartD.Invoke((Action)(() => chartD.Series["Wartość"].Points.Clear()));
                        int i = 0;

                        foreach (var item in values)
                        {
                            listBox.Invoke((Action)(() => listBox.Items.Add(item)));
                            if (ChEnable == true)
                            {
                                chartDate.IDex = start;
                                chartDate.Value = int.Parse(listBox.Items[i].ToString());
                                chartD.Invoke((Action)(() => chartD.Series["Wartość"].Points.AddXY(chartDate.IDex, chartDate.Value)));
                            }
                            i++;
                            start++;
                        }

                        i = 0;
                        start = Int32.Parse(addBox1.Text);
                    }
                    Thread.Sleep(1000);
                }
            }
        }

        private void buttonDis_Click(object sender, EventArgs e)
        {
            modbusClient.Disconnect();
            labelStan.Text = "Stan: Brak połączenia";
        }

        private void textBoxBool_Click(object sender, EventArgs e)
        {
            if (x % 2 == 0) {
                textBoxBool.Text = "TRUE";
                x++;
            }
            else
            {
                textBoxBool.Text = "FALSE";
                x++;
            }
            
        }

        private void buttonPrepBool_Click(object sender, EventArgs e)
        {
            listBox2.Items.Add(textBoxBool.Text);
        }

        private void buttonPrepInt_Click(object sender, EventArgs e)
        {
            if(int.TryParse(textBoxInt.Text,out int output))
            {
                listBox2.Items.Add(textBoxInt.Text);
            }
        }

        private void buttonClearAll_Click(object sender, EventArgs e)
        {
            listBox2.Items.Clear();    
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            if (listBox2.TopIndex != listBox2.SelectedIndex)
            {
                listBox2.TopIndex = listBox2.SelectedIndex;
            }
            
            listBox2.Items.RemoveAt(listBox2.SelectedIndex);
            listBox2.ClearSelected();
        }

        private void buttonWExecute_Click(object sender, EventArgs e)
        {
            startUp = int.Parse(addBox3.Text);
            
            
            if ((string)functionBox2.SelectedItem == "05 Write Single Coil")
            {
                modbusClient.WriteSingleCoil(startUp, bool.Parse(listBox2.Items[0].ToString()));
            }
            else if ((string)functionBox2.SelectedItem == "06 Write Single Register")
            {
                modbusClient.WriteSingleRegister(startUp, int.Parse(listBox2.Items[0].ToString()));
            }
            else if ((string)functionBox2.SelectedItem == "15 Write Multiple Coils")
            {
                bool[] tab;
                int l = 0;
                int j = 0;
                foreach(string b in listBox2.Items)
                {
                    l++;
                }
                tab = new bool[l];
                foreach (string b in listBox2.Items)
                {
                    tab[j]= bool.Parse(b);
                    j++;
                }

                modbusClient.WriteMultipleCoils(startUp, tab);
            }
            else if ((string)functionBox2.SelectedItem == "16 Write Multiple Registers")
            {
                int[] tab;
                int l = 0;
                int j = 0;
                foreach (string b in listBox2.Items)
                {
                    l++;
                }
                tab = new int[l];
                foreach (string b in listBox2.Items)
                {
                    tab[j] = int.Parse(b);
                    j++;
                }

                modbusClient.WriteMultipleRegisters(startUp, tab);
            }
            else
            {
                int[] tab;
                int l = 0;
                int j = 0;
                foreach (string b in listBox2.Items)
                {
                    l++;
                }
                tab = new int[l];
                foreach (string b in listBox2.Items)
                {
                    tab[j] = int.Parse(b);
                    j++;
                }

                modbusClient.WriteMultipleRegisters(startUp, tab);
            }
        }

        private void buttonCrt_Click(object sender, EventArgs e)
        {
            ChEnable = true;
        }
        
        private void InteruptBtn_Click(object sender, EventArgs e)
        {
            cts.Cancel();
            ChEnable = false;
            listBox1.Items.Add("Watek przerwany");
        }

        private void buttonObs_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.x = stan;
            form2.ip2 = ipBox.Text;
            form2.port2 = Int32.Parse(portBox.Text);
            form2.Show();
        }
    }


    public class ChartData
    {
        public int IDex { get; set; }
        public int Value { get; set; } 
    }
}
