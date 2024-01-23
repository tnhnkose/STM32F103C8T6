using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.IO;
using System.Data.SqlClient;
using System.Globalization;

namespace c_Stm32Proje2
{
    public partial class Form2 : Form
    {
        private DateTime ReceivedTime;
        private int receivedValue;
        private double lowerLimit = 0;
        private double upperLimit = 24;
        private double? receivedVoltValue = null;
        private int mode=0;
        float ReceivedValue1;
        float ReceivedValue2;
        float ReceivedValue3;
        float ReceivedValue4;
        float ReceivedValue5;
        float ReceivedValue6;
        float ReceivedValue7;
        float ReceivedValue8;
        float ReceivedValue9;
        float ReceivedValue10;
        float ReceivedValue11;
        float ReceivedValue12;

        public Form2()
        {
            InitializeComponent();
            getAvailablePorts();
            panelOnay.BackColor = Color.Transparent;  // Set the panel color to transparent initially.
            textBoxLowerLimit.TextChanged += TextBoxLimit_TextChanged;
            textBoxUpperLimit.TextChanged += TextBoxLimit_TextChanged;
            serialPort1.DataReceived += serialPort1_DataReceived;

        }

        void getAvailablePorts()
        {
            string[] ports = SerialPort.GetPortNames();
            comboBoxPorts.Items.AddRange(ports);
        }


        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private List<int> receivedIntList = new List<int>(); // Alınan float değerlerini saklamak için liste

        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (mode == 1)
            {
                int dataSize = 48; // Her bir int değeri için 4 byte bekleniyor

                if (serialPort1.BytesToRead >= dataSize)
                {
                    byte[] responseBytes = new byte[dataSize];

                    try
                    {
                        serialPort1.BaseStream.Read(responseBytes, 0, responseBytes.Length);
                    }
                    catch (Exception ex)
                    {
                        // Log the exception or show it
                        MessageBox.Show("An error occurred while reading from the serial port: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    receivedIntList.Clear(); // Önceki verileri temizle

                    for (int i = 0; i < 12; i++)
                    {
                        byte[] intBytes = new byte[4];
                        Array.Copy(responseBytes, i * 4, intBytes, 0, 4);

                            if (BitConverter.IsLittleEndian)
                            Array.Reverse(intBytes); // Eğer STM32 küçük endian kullanıyorsa, byte dizisini ters çevir

                        int receivedInt = BitConverter.ToInt32(intBytes, 0);
                        receivedIntList.Add(receivedInt);
                    }
                    List<float> voltageList = ConvertToVoltage(receivedIntList); // Dönüşümü yap

                    if (listBoxValues.InvokeRequired)
                    {
                        listBoxValues.Invoke(new Action<List<float>>(UpdateListBoxValues), voltageList); // İşlevi UI iş parçacığında çağır
                    }
                    else
                    {
                        listBoxValues.Items.Clear();
                        foreach (float value in voltageList)
                        {

                            listBoxValues.Items.Add(value.ToString("F4"));
                        }
                    }
                    ReceivedValue1 = voltageList[0];
                    ReceivedValue2 = voltageList[1];
                    ReceivedValue3 = voltageList[2];
                    ReceivedValue4 = voltageList[3];
                    ReceivedValue5 = voltageList[4];
                    ReceivedValue6 = voltageList[5];
                    ReceivedValue7 = voltageList[6];
                    ReceivedValue8 = voltageList[7];
                    ReceivedValue9 = voltageList[8];
                    ReceivedValue10 = voltageList[9];
                    ReceivedValue11 = voltageList[10];
                    ReceivedValue12 = voltageList[11];

                }
                mode = 0;
                textBoxReceive.Invoke(new Action(() => { textBoxReceive.Text = ""; }));
                

            }
            else if (mode == 2)
            {
                // Mode 2 için UART üzerinden veriyi oku
                byte[] receivedData = new byte[13]; // 11 byte'lık bir dizi oluştur
                int bytesRead = 0; // Okunan byte sayısını saklayacak değişken

                // UART üzerinden beklenen veriyi oku
                while (bytesRead < receivedData.Length)
                {
                    int availableBytes = serialPort1.BytesToRead;
                    if (availableBytes > 0)
                    {
                        int bytesToRead = Math.Min(availableBytes, receivedData.Length - bytesRead);
                        bytesRead += serialPort1.Read(receivedData, bytesRead, bytesToRead);
                    }
                }

                string receivedString = Encoding.ASCII.GetString(receivedData); // Gelen veriyi ASCII string olarak al

                if (receivedString.Trim() == "CAN TEST OK")
                {
                    // Control.Invoke kullanarak UI iş parçacığında güncelleme yap
                    textBoxReceive.Invoke(new Action(() => { textBoxReceive.Text = "CAN TESTİ BAŞARILI"; }));
                }
                else
                {
                    textBoxReceive.Invoke(new Action(() => { textBoxReceive.Text = "CAN TESTİ BAŞARISIZ"; }));
                }
                mode = 0;
            }
            else if (mode == 3)
            {
                // Mode 3 için UART üzerinden veriyi oku
                byte[] receivedData = new byte[26]; //  byte'lık bir dizi oluştur
                int bytesRead = 0; // Okunan byte sayısını saklayacak değişken

                // UART üzerinden beklenen veriyi oku
                while (bytesRead < receivedData.Length)
                {
                    int availableBytes = serialPort1.BytesToRead;
                    if (availableBytes > 0)
                    {
                        int bytesToRead = Math.Min(availableBytes, receivedData.Length - bytesRead);
                        bytesRead += serialPort1.Read(receivedData, bytesRead, bytesToRead);
                    }
                }

                string receivedString = Encoding.ASCII.GetString(receivedData); // Gelen veriyi ASCII string olarak al

                if (receivedString.Trim() == "CIHAZ BEKLEME MODUNDADIR")
                {
                    // Control.Invoke kullanarak UI iş parçacığında güncelleme yap
                    textBoxReceive.Invoke(new Action(() => { textBoxReceive.Text = "CİHAZ BEKLEME MODUNDADIR"; }));
                }
                else
                {
                    textBoxReceive.Invoke(new Action(() => { textBoxReceive.Text = "CİHAZ BEKLEME MODUNA GEÇEMEDİ"; }));
                }
            }
            mode = 0;
        }

        private void UpdateListBoxValues(List<float> values)
        {
            if (listBoxValues.InvokeRequired)
            {
                listBoxValues.Invoke(new Action<List<float>>(UpdateListBoxValues), values); // İşlevi UI iş parçacığında çağır
            }
            else
            {
                listBoxValues.Items.Clear();
                foreach (float value in values)
                {
                    listBoxValues.Items.Add(value.ToString("F4"));
                }
            }
        }

        private void buttonConnect_Click(object sender, EventArgs e)
        {
            serialPort1.PortName = comboBoxPorts.Text;
            serialPort1.BaudRate = 9600;
            serialPort1.Parity = Parity.None;
            serialPort1.StopBits = StopBits.One;
            serialPort1.DataBits = 8;

            try
            {
                serialPort1.Open();
            }
            catch (UnauthorizedAccessException)
            {
                textBoxReceive.Text = "Unauthorized Access";
            }

            buttonConnect.Enabled = false;
            buttonDisconnect.Enabled = true;
            buttonSend.Enabled = true;
            buttonReceive.Enabled = true;

        }

        private void buttonDisconnect_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                serialPort1.Close();
                buttonConnect.Enabled = true;
                buttonDisconnect.Enabled = false;
                buttonSend.Enabled = false;
                buttonReceive.Enabled = false;
            }
        }

        


        private void AddToListBox(string data)
        {
            if (listBoxValues.InvokeRequired)
            {
                listBoxValues.Invoke(new Action<string>(AddToListBox), data);
            }
            else
            {
                listBoxValues.Items.Add(data);
            }
        }

        private async void buttonReceive_Click(object sender, EventArgs e)
        {

            if (serialPort1.IsOpen)
            {
                while (true)
                {
                    if (serialPort1.BytesToRead >= sizeof(int))
                    {
                        byte[] responseBytes = new byte[sizeof(int)];

                        try
                        {
                            await serialPort1.BaseStream.ReadAsync(responseBytes, 0, responseBytes.Length);
                        }
                        catch (Exception ex)
                        {
                            // Log the exception or show it
                            MessageBox.Show("An error occurred while reading from the serial port: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        int receivedValue = BitConverter.ToInt32(responseBytes, 0);
                        DateTime receivedTime = DateTime.Now;
                        textBoxReceive.Text = "Received value from STM32F103: " + receivedValue + " at " + receivedTime;

                     //   receivedVoltValue = ConvertToVolt(receivedValue);
                        textBoxVolt.Text = receivedVoltValue.Value.ToString("F2");

                        // Update the panel color whenever a new value is received.
                        UpdatePanelColor();




                        break;
                    }

                    // Avoid busy-waiting
                    await Task.Delay(100); // Wait for 100 ms
                }
            }
            else
            {
                MessageBox.Show("Serial port is not connected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }





        private void buttonSaveSql_Click(object sender, EventArgs e)
        {
            string username = "saharobotik";
            string connectionString = "Server=DESKTOP-2QEAL1J\\SQLEXPRESS;Database= Stm32CSharpData; Integrated Security=True; ";
            ReceivedTime = DateTime.Now; // Şu anki zamanı al
            // SQL veritabanına bağlanma
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Veri ekleme SQL sorgusu
                string insertQuery = "INSERT INTO TesterDeviceDatas (Username, ReceivedValue1, ReceivedValue2, ReceivedValue3, ReceivedValue4, ReceivedValue5, ReceivedValue6, ReceivedValue7, ReceivedValue8, ReceivedValue9, ReceivedValue10, ReceivedValue11, ReceivedValue12, ReceivedTime ) VALUES (@Username, @ReceivedValue1 , @ReceivedValue2, @ReceivedValue3, @ReceivedValue4, @ReceivedValue5, @ReceivedValue6, @ReceivedValue7, @ReceivedValue8, @ReceivedValue9, @ReceivedValue10, @ReceivedValue11, @ReceivedValue12, @ReceivedTime)";

                // SQL komutunu oluşturma
                using (SqlCommand command = new SqlCommand(insertQuery, connection))
                {
                    // Parametreleri belirleme ve değerlerini atama
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@ReceivedValue1", ReceivedValue1);
                    command.Parameters.AddWithValue("@ReceivedValue2", ReceivedValue2);
                    command.Parameters.AddWithValue("@ReceivedValue3", ReceivedValue3);
                    command.Parameters.AddWithValue("@ReceivedValue4", ReceivedValue4);
                    command.Parameters.AddWithValue("@ReceivedValue5", ReceivedValue5);
                    command.Parameters.AddWithValue("@ReceivedValue6", ReceivedValue6);
                    command.Parameters.AddWithValue("@ReceivedValue7", ReceivedValue7);
                    command.Parameters.AddWithValue("@ReceivedValue8", ReceivedValue8);
                    command.Parameters.AddWithValue("@ReceivedValue9", ReceivedValue9);
                    command.Parameters.AddWithValue("@ReceivedValue10", ReceivedValue10);
                    command.Parameters.AddWithValue("@ReceivedValue11", ReceivedValue11);
                    command.Parameters.AddWithValue("@ReceivedValue12", ReceivedValue12);
                    command.Parameters.AddWithValue("@ReceivedTime", ReceivedTime);

                    // SQL komutunu çalıştırma
                    command.ExecuteNonQuery();
                }
            }


        }



        private void comboBoxPorts_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBoxVolt_TextChanged(object sender, EventArgs e)
        {

        }

        private void TextBoxLimit_TextChanged(object sender, EventArgs e)
        {
            double value;
            if (double.TryParse((sender as TextBox).Text, out value))
            {
                if (sender == textBoxLowerLimit)
                {
                    lowerLimit = value;
                }
                else if (sender == textBoxUpperLimit)
                {
                    upperLimit = value;
                }
                // Update the panel color whenever the limit is changed.
                UpdatePanelColor();
            }
            else
            {
                MessageBox.Show("Limit değeri geçersiz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private List<float> ConvertToVoltage(List<int> intValues)
        {
            List<float> voltageValues = new List<float>();

            foreach (int intValue in intValues)
            {
                float voltage = (float)intValue / 3300 * 31; // Dönüşüm formülü
                if ((voltage < 0 || voltage > 35))
                {
                    voltage = 0;
                    voltageValues.Add(voltage);
                }
                else
                {
                    voltageValues.Add(voltage);
                }
                
            }

            return voltageValues;
        }
        private void textBoxLowerLimit_TextChanged(object sender, EventArgs e)
        {
            if (double.TryParse(textBoxLowerLimit.Text, out lowerLimit))
            {
                // Update the panel color whenever the lower limit is changed.
                UpdatePanelColor();
            }
            else
            {
                MessageBox.Show("Alt limit değeri geçersiz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBoxUpperLimit_TextChanged(object sender, EventArgs e)
        {
            if (double.TryParse(textBoxUpperLimit.Text, out upperLimit))
            {
                // Update the panel color whenever the upper limit is changed.
                UpdatePanelColor();
            }
            else
            {
                MessageBox.Show("Üst limit değeri geçersiz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdatePanelColor()
        {
            // Only update the color if a voltage value has been received.
            if (receivedVoltValue.HasValue)
            {
                if (receivedVoltValue.Value >= lowerLimit && receivedVoltValue.Value <= upperLimit)
                {
                    panelOnay.BackColor = Color.Green;
                }
                else
                {
                    panelOnay.BackColor = Color.Red;
                }
            }





            
        }

        private void buttonADC_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen) 
            {
                mode = 1;
                serialPort1.Write("ADC OKU");

            }
            
        }

        private void buttonCAN_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                mode = 2;
                serialPort1.Write("CAN TEST");

            }
        }

        private void buttonBekle_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                mode = 3;
                serialPort1.Write("BEKLE");

            }
        }

        private void panelOnay_Paint(object sender, PaintEventArgs e)
        {

        }

        private void listBoxValues_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxLowerLimit_TextChanged_1(object sender, EventArgs e)
        {

        }
    }
}

