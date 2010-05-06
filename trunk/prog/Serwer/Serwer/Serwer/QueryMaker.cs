using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using Npgsql;
using NpgsqlTypes;

namespace Serwer
{
    public class QueryMaker
    {
        private NpgsqlConnection conn;
        public const int MAX_KLIENT = 200;
        public const int MAX_DATA = 10;

        /// <summary>
        /// Konstruktor klasy QueryMaker.
        /// </summary>
        /// <param name="conn"> Obiekt przechowujący połączenie z bazą PostgreSQL. </param>  
        public QueryMaker(NpgsqlConnection conn)
        {
            this.conn = conn;
        }

        /// <summary>
        /// Zwracanie listy klientów.
        /// </summary>
        /// <returns>Zwraca listę znalezionych klientów w bazie PostgreSQL.</returns>
        public String[] getClients()
        {
            NpgsqlCommand command = new NpgsqlCommand("select * from uzytkownik", conn);
            String[] data = new String[MAX_KLIENT];

            try
            {
                NpgsqlDataReader dr = command.ExecuteReader();
                int j = 0;
                while (dr.Read())
                {
                    for (int i = 0; i < dr.FieldCount - 1; i++)
                    {
                        data[j] = dr[i].ToString();
                        j++;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Błąd połączenia z bazą danych!\n"+ex.Message);
            }
            return data;
        }

        /// <summary>
        /// Zwracanie listy szczegółowych danych klienta.
        /// </summary>
        /// <param name="conn"> Obiekt przechowujący połączenie z bazą PostgreSQL. </param>  
        /// <returns>Zwraca listę znalezionych danych klienta w bazie PostgreSQL.</returns>
        public String[] getClientDetail(String numer)
        {
            // Zapytanie o detale klienta.
            NpgsqlCommand command = new NpgsqlCommand("select imie, nazwisko, miasto, kod_pocztowy, e_mail, data_ur, zainteresowania from uzytkownik, dane where uzytkownik.numer = :numer and dane.numer = uzytkownik.numer", conn);
            // Typ parametru w zapytaniu.
            command.Parameters.Add(new NpgsqlParameter("numer", NpgsqlDbType.Integer));
            // Zapisywanie wartości parametru.
            command.Parameters[0].Value = Int32.Parse(numer);
            String[] data = new String[MAX_DATA];
            
            try
            {
                // Wykonanie zapytania.
                NpgsqlDataReader dr = command.ExecuteReader();

                while (dr.Read())
                {
                    for (int i = 0; i < dr.FieldCount; i++)
                    {
                        data[i] = dr[i].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Błąd połączenia z bazą danych!\n"+ex.Message);
            }

            return data;

        }

        public void addClient(String[] data)
        {
            NpgsqlCommand command = new NpgsqlCommand("insert into uzytkownik(login, haslo) values('janek87', 'janek87')", conn);
            Int32 rowsaffected;

                rowsaffected = command.ExecuteNonQuery();
           
         

        }
        
    }
}
