using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using System.Xml.Linq;
using System.Net;
using System.Configuration;

namespace TestTaskSTOUN {
    public partial class Form1 : Form {
        string conString = ConfigurationManager.ConnectionStrings["conString"].ConnectionString; // строка подключения к базе данных
        public Form1() {
            InitializeComponent();
            ToFillListCurrency();
        }

        /*При открытии приложения, проверяется в БД информация на день открытия программы. 
         * Если в базе нет записей по сегодняшнему дню, то записываем новые данные
         * иначе просто открывается программа.
         */
        private void Form1_Load(object sender, EventArgs e) {
            try {
                #region Загрузка в БД данные из справочника валют. Делается один раз
                using (SqlConnection con = new SqlConnection(conString)) {
                    con.Open();
                    using (SqlCommand com1 = new SqlCommand("Select count(*) From DirectoryCurrency", con)) {
                        object countEl = com1.ExecuteScalar();
                        if ((int) countEl == 0) {
                            string url2 = "http://www.cbr.ru/scripts/XML_valFull.asp";
                            var webhost = WebRequest.CreateHttp(url2);
                            string strResp;
                            using (var req = webhost.GetResponse()) {
                                using (var stream = req.GetResponseStream()) {
                                    using (var sReader = new StreamReader(stream, Encoding.Default)) {
                                        strResp = sReader.ReadToEnd();
                                    }
                                }
                            }

                            XDocument xdoc = XDocument.Parse(strResp);
                            foreach (XElement el in xdoc.Element("Valuta").Elements("Item")) {
                                using (SqlCommand com = new SqlCommand("insert into DirectoryCurrency(IDITEM, NAME, ENGNAME, NOMINAL, PARENTCODE, ISONUMCODE, ISOCHARCODE) values(@idit, @name, @engn, @nom, @pcod, @ison, @isoch)", con)) {
                                    string idi = el.Attribute("ID").Value;
                                    string name = el.Element("Name").Value;
                                    string enName = el.Element("EngName").Value;
                                    int nom = el.Element("Nominal").Value == "" ? 0 : Convert.ToInt32(el.Element("Nominal").Value);
                                    string parCode = el.Element("ParentCode").Value;
                                    int isonum = el.Element("ISO_Num_Code").Value == "" ? 0 : Convert.ToInt32(el.Element("ISO_Num_Code").Value);
                                    string isochar = el.Element("ISO_Char_Code").Value;
                                    com.Parameters.AddWithValue("@idit", idi);
                                    com.Parameters.AddWithValue("@name", name);
                                    com.Parameters.AddWithValue("@engn", enName);
                                    com.Parameters.AddWithValue("@nom", nom);
                                    com.Parameters.AddWithValue("@pcod", parCode);
                                    com.Parameters.AddWithValue("@ison", isonum);
                                    com.Parameters.AddWithValue("@isoch", isochar);
                                    com.ExecuteNonQuery();
                                }
                            }
                        }
                    }
                }
                #endregion
                #region Загрузка в БД данные из курса валют с 01.01.2019 по сегодняшний день
                using (SqlConnection con = new SqlConnection(conString)) {
                    con.Open();
                    using (SqlCommand com1 = new SqlCommand("Select count(*) From ExchangeRates", con)) {
                        object countEl = com1.ExecuteScalar();
                        if ((int) countEl == 0) {
                            DateTime start = DateTime.Parse("2019-01-01");
                            DateTime end = DateTime.Parse(DateTime.Now.AddDays(-4).ToShortDateString());
                            

                            for (DateTime i = start; i <= end; i = i.AddDays(1)) {
                                Console.WriteLine(i.ToShortDateString());
                                string url1 = "http://www.cbr.ru/scripts/XML_daily.asp?date_req=" + i.ToShortDateString();
                                var webhost = WebRequest.CreateHttp(url1);
                                string strResp;
                                using (var req = webhost.GetResponse()) {
                                    using (var stream = req.GetResponseStream()) {
                                        using (var sReader = new StreamReader(stream, Encoding.Default)) {
                                            strResp = sReader.ReadToEnd();
                                        }
                                    }
                                }
                                XDocument xdoc = XDocument.Parse(strResp);
                                foreach (XElement el in xdoc.Element("ValCurs").Elements("Valute")) {
                                    using (SqlCommand com = new SqlCommand("insert into ExchangeRates(IDVAL, NUMCODE, CHARCODE, NOMINAL, NAME, VALUE, DATE) values(@idval, @numcode, @charcode, @nom, @name, @val, @dt)", con)) {
                                        string idV = el.Attribute("ID").Value;
                                        string numCode = el.Element("NumCode").Value;
                                        string charCode = el.Element("CharCode").Value;
                                        int nom = el.Element("Nominal").Value == "" ? 0 : Convert.ToInt32(el.Element("Nominal").Value);
                                        string name = el.Element("Name").Value;
                                        double val = el.Element("Value").Value == "" ? 0 : Convert.ToDouble(el.Element("Value").Value.Replace(',', '.'));
                                        com.Parameters.AddWithValue("@idval", idV);
                                        com.Parameters.AddWithValue("@numcode", numCode);
                                        com.Parameters.AddWithValue("@charcode", charCode);
                                        com.Parameters.AddWithValue("@nom", nom);
                                        com.Parameters.AddWithValue("@name", name);
                                        com.Parameters.AddWithValue("@val", val);
                                        com.Parameters.AddWithValue("@dt", i.ToShortDateString());
                                        com.ExecuteNonQuery();
                                    }
                                }
                            }
                        }
                    }
                }
                
                #endregion
                object count;
                
                using (SqlConnection con = new SqlConnection(conString)) {
                    con.Open();
                    using (SqlCommand com1 = new SqlCommand("Select max(DATE) From ExchangeRates", con)) {
                        object dateMaxObj = com1.ExecuteScalar();
                        DateTime dateMax = DateTime.Parse(dateMaxObj.ToString());

                        if (dateMax.ToShortDateString() != DateTime.Now.ToShortDateString()) {
                            DateTime start = dateMax.AddDays(1);
                            DateTime end = DateTime.Parse(DateTime.Now.ToShortDateString());

                            for (DateTime i = start; i <= end; i = i.AddDays(1)) {
                                string url1 = "http://www.cbr.ru/scripts/XML_daily.asp?date_req=" + i.ToShortDateString();
                                var webhost = WebRequest.CreateHttp(url1);
                                string strResp;
                                using (var req = webhost.GetResponse()) {
                                    using (var stream = req.GetResponseStream()) {
                                        using (var sReader = new StreamReader(stream, Encoding.Default)) {
                                            strResp = sReader.ReadToEnd();
                                        }
                                    }
                                }
                                XDocument xdoc = XDocument.Parse(strResp);
                                foreach (XElement el in xdoc.Element("ValCurs").Elements("Valute")) {
                                    using (SqlCommand com = new SqlCommand("insert into ExchangeRates(IDVAL, NUMCODE, CHARCODE, NOMINAL, NAME, VALUE, DATE) values(@idval, @numcode, @charcode, @nom, @name, @val, @dt)", con)) {
                                        string idV = el.Attribute("ID").Value;
                                        string numCode = el.Element("NumCode").Value;
                                        string charCode = el.Element("CharCode").Value;
                                        int nom = el.Element("Nominal").Value == "" ? 0 : Convert.ToInt32(el.Element("Nominal").Value);
                                        string name = el.Element("Name").Value;
                                        double val = el.Element("Value").Value == "" ? 0 : Convert.ToDouble(el.Element("Value").Value.Replace(',', '.'));
                                        com.Parameters.AddWithValue("@idval", idV);
                                        com.Parameters.AddWithValue("@numcode", numCode);
                                        com.Parameters.AddWithValue("@charcode", charCode);
                                        com.Parameters.AddWithValue("@nom", nom);
                                        com.Parameters.AddWithValue("@name", name);
                                        com.Parameters.AddWithValue("@val", val);
                                        com.Parameters.AddWithValue("@dt", i.ToShortDateString());
                                        com.ExecuteNonQuery();
                                    }
                                }
                            }
                        }
                    }
                }

            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }
        

        private void GetCurrencyRate_Click(object sender, EventArgs e) {
            string nameCurrency = ListCurrency.SelectedItem.ToString();
            string date = Calendar.Value.ToShortDateString();
            double rate = ToGiveRate(nameCurrency, date);
            if (rate != -1) {
                ResultText.Text = rate.ToString();
            } else {
                ResultText.Text = "Не найдена валюта или неправильно указана дата";
            }
        }

        double ToGiveRate(string name, string date) {
            using (SqlConnection con = new SqlConnection(conString)) {
                con.Open();
                using (SqlCommand com = new SqlCommand("Select VALUE From ExchangeRates Where NAME = @name and DATE = @dt", con)) {
                    com.Parameters.AddWithValue("@name", name);
                    com.Parameters.AddWithValue("dt", date);
                    using (SqlDataReader reader = com.ExecuteReader()) {
                        if (reader.HasRows) {
                            while (reader.Read()) {
                                object a = reader.GetValue(0);
                                return Math.Round(Convert.ToDouble(a), 4);
                            }
                        }
                        else
                            return -1;
                    }
                }
            }
            return 0;
        }

        void ToFillListCurrency() {
            using (SqlConnection con = new SqlConnection(conString)) {
                con.Open();
                using (SqlCommand com = new SqlCommand("Select Distinct NAME From ExchangeRates order by NAME", con)) {
                    using (SqlDataReader reader = com.ExecuteReader()) {
                        if (reader.HasRows) {
                            while (reader.Read()) {
                                ListCurrency.Items.Add(reader.GetString(0));
                            }
                        }
                    }
                }
            }
        }
    }
}
