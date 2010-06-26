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
    /// <summary>
    /// Klasa umożliwiająca interakcję z bazą danych.
    /// </summary>
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
                    for (int i = 0; i < dr.FieldCount - 2; i++)
                    {
                        data[j] = dr[i].ToString();
                        j++;
                    }
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Błąd połączenia z bazą danych!\n" + ex.Message);
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

                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Błąd połączenia z bazą danych!\n" + ex.Message);
            }

            return data;

        }

        /// <summary>
        /// Dodaje klienta do bazy.
        /// </summary>
        /// <param name="data"> Słownik z danymi klienta. </param>
        /// /// <returns>1 - brak danych, 0 - poprawne wykonanie, 2 - login zajęty.</returns>
        public byte addClient(Dictionary<String, String> data)
        {
            if (data["login"] == null || data["haslo"] == null || data["status"] == null || data["imie"] == null || data["nazwisko"] == null ||
                data["email"] == null)
                return 1;

            // Komenda dodająca klienta do bazy.
            NpgsqlCommand command = new NpgsqlCommand("insert into uzytkownik(login, haslo, status) values(:login, :haslo, :status)", conn);

            // Typy parametrów w zapytaniu.
            command.Parameters.Add(new NpgsqlParameter("login", NpgsqlDbType.Varchar));
            command.Parameters.Add(new NpgsqlParameter("haslo", NpgsqlDbType.Varchar));
            command.Parameters.Add(new NpgsqlParameter("status", NpgsqlDbType.Varchar));
            // Zapisywanie wartości parametrów.
            command.Parameters[0].Value = data["login"];
            command.Parameters[1].Value = data["haslo"];
            command.Parameters[2].Value = data["status"];

            Int32 rowsaffected;

            try
            {
                rowsaffected = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Błąd połączenia z bazą danych!\n" + ex.Message);
                return 2;
            }

            NpgsqlCommand command2 = null;
            // Zapytanie o przydzielony numer klienta.
            NpgsqlCommand command1 = new NpgsqlCommand("select numer from uzytkownik where uzytkownik.login = :login", conn);
            // Typy parametrów w zapytaniu.
            command1.Parameters.Add(new NpgsqlParameter("login", NpgsqlDbType.Varchar));
            // Zapisywanie wartości parametrów.
            command1.Parameters[0].Value = data["login"];

            Int32 numer = 0;
            try
            {
                // Wykonanie zapytania.
                NpgsqlDataReader dr = command1.ExecuteReader();

                while (dr.Read())
                {
                    numer = Int32.Parse(dr[0].ToString());
                }

                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Błąd połączenia z bazą danych!\n" + ex.Message);
            }


            if (data["miasto"] == null && data["kod"] == null && data["data"] == null && data["zainteresowania"] == null)
            {
                // Komenda dodająca klienta do bazy.
                command2 = new NpgsqlCommand("insert into dane(numer, imie, nazwisko, e_mail) values(:numer, :imie, :nazwisko, :email)", conn);

                // Typy parametrów w zapytaniu.
                command2.Parameters.Add(new NpgsqlParameter("numer", NpgsqlDbType.Integer));
                command2.Parameters.Add(new NpgsqlParameter("imie", NpgsqlDbType.Varchar));
                command2.Parameters.Add(new NpgsqlParameter("nazwisko", NpgsqlDbType.Varchar));
                command2.Parameters.Add(new NpgsqlParameter("email", NpgsqlDbType.Varchar));
                // Zapisywanie wartości parametrów.
                command2.Parameters[0].Value = numer;
                command2.Parameters[1].Value = data["imie"];
                command2.Parameters[2].Value = data["nazwisko"];
                command2.Parameters[3].Value = data["email"];
            }
            else if (data["miasto"] != null && data["kod"] != null && data["data"] == null && data["zainteresowania"] == null)
            {

                // Komenda dodająca klienta do bazy.
                command2 = new NpgsqlCommand("insert into dane(numer, imie, nazwisko, e_mail, miasto, kod_pocztowy) values(:numer, :imie, :nazwisko, :email, :miasto, :kod)", conn);

                // Typy parametrów w zapytaniu.
                command2.Parameters.Add(new NpgsqlParameter("numer", NpgsqlDbType.Integer));
                command2.Parameters.Add(new NpgsqlParameter("imie", NpgsqlDbType.Varchar));
                command2.Parameters.Add(new NpgsqlParameter("nazwisko", NpgsqlDbType.Varchar));
                command2.Parameters.Add(new NpgsqlParameter("email", NpgsqlDbType.Varchar));
                command2.Parameters.Add(new NpgsqlParameter("miasto", NpgsqlDbType.Varchar));
                command2.Parameters.Add(new NpgsqlParameter("kod", NpgsqlDbType.Varchar));
                // Zapisywanie wartości parametrów.
                command2.Parameters[0].Value = numer;
                command2.Parameters[1].Value = data["imie"];
                command2.Parameters[2].Value = data["nazwisko"];
                command2.Parameters[3].Value = data["email"];
                command2.Parameters[4].Value = data["miasto"];
                command2.Parameters[5].Value = data["kod"];
            }
            else if (data["miasto"] != null && data["kod"] != null && data["data"] != null && data["zainteresowania"] == null)
            {

                // Komenda dodająca klienta do bazy.
                command2 = new NpgsqlCommand("insert into dane(numer, imie, nazwisko, e_mail, miasto, kod_pocztowy, data_ur) values(:numer, :imie, :nazwisko, :email, :miasto, :kod, :data)", conn);

                // Typy parametrów w zapytaniu.
                command2.Parameters.Add(new NpgsqlParameter("numer", NpgsqlDbType.Integer));
                command2.Parameters.Add(new NpgsqlParameter("imie", NpgsqlDbType.Varchar));
                command2.Parameters.Add(new NpgsqlParameter("nazwisko", NpgsqlDbType.Varchar));
                command2.Parameters.Add(new NpgsqlParameter("email", NpgsqlDbType.Varchar));
                command2.Parameters.Add(new NpgsqlParameter("miasto", NpgsqlDbType.Varchar));
                command2.Parameters.Add(new NpgsqlParameter("kod", NpgsqlDbType.Varchar));
                command2.Parameters.Add(new NpgsqlParameter("data", NpgsqlDbType.Date));
                // Zapisywanie wartości parametrów.
                command2.Parameters[0].Value = numer;
                command2.Parameters[1].Value = data["imie"];
                command2.Parameters[2].Value = data["nazwisko"];
                command2.Parameters[3].Value = data["email"];
                command2.Parameters[4].Value = data["miasto"];
                command2.Parameters[5].Value = data["kod"];
                command2.Parameters[6].Value = data["data"];
            }
            else if (data["miasto"] != null && data["kod"] != null && data["data"] != null && data["zainteresowania"] != null)
            {

                // Komenda dodająca klienta do bazy.
                command2 = new NpgsqlCommand("insert into dane(numer, imie, nazwisko, e_mail, miasto, kod_pocztowy, data_ur, zainteresowania) values(:numer, :imie, :nazwisko, :email, :miasto, :kod, :data, :zainteresowania)", conn);

                // Typy parametrów w zapytaniu.
                command2.Parameters.Add(new NpgsqlParameter("numer", NpgsqlDbType.Integer));
                command2.Parameters.Add(new NpgsqlParameter("imie", NpgsqlDbType.Varchar));
                command2.Parameters.Add(new NpgsqlParameter("nazwisko", NpgsqlDbType.Varchar));
                command2.Parameters.Add(new NpgsqlParameter("email", NpgsqlDbType.Varchar));
                command2.Parameters.Add(new NpgsqlParameter("miasto", NpgsqlDbType.Varchar));
                command2.Parameters.Add(new NpgsqlParameter("kod", NpgsqlDbType.Varchar));
                command2.Parameters.Add(new NpgsqlParameter("data", NpgsqlDbType.Date));
                command2.Parameters.Add(new NpgsqlParameter("zainteresowania", NpgsqlDbType.Varchar));
                // Zapisywanie wartości parametrów.
                command2.Parameters[0].Value = numer;
                command2.Parameters[1].Value = data["imie"];
                command2.Parameters[2].Value = data["nazwisko"];
                command2.Parameters[3].Value = data["email"];
                command2.Parameters[4].Value = data["miasto"];
                command2.Parameters[5].Value = data["kod"];
                command2.Parameters[6].Value = data["data"];
                command2.Parameters[7].Value = data["zainteresowania"];
            }
            else if (data["miasto"] != null && data["kod"] != null && data["data"] == null && data["zainteresowania"] != null)
            {

                // Komenda dodająca klienta do bazy.
                command2 = new NpgsqlCommand("insert into dane(numer, imie, nazwisko, e_mail, miasto, kod_pocztowy, data_ur, zainteresowania) values(:numer, :imie, :nazwisko, :email, :miasto, :kod, :zainteresowania)", conn);

                // Typy parametrów w zapytaniu.
                command2.Parameters.Add(new NpgsqlParameter("numer", NpgsqlDbType.Integer));
                command2.Parameters.Add(new NpgsqlParameter("imie", NpgsqlDbType.Varchar));
                command2.Parameters.Add(new NpgsqlParameter("nazwisko", NpgsqlDbType.Varchar));
                command2.Parameters.Add(new NpgsqlParameter("email", NpgsqlDbType.Varchar));
                command2.Parameters.Add(new NpgsqlParameter("miasto", NpgsqlDbType.Varchar));
                command2.Parameters.Add(new NpgsqlParameter("kod", NpgsqlDbType.Varchar));
                command2.Parameters.Add(new NpgsqlParameter("zainteresowania", NpgsqlDbType.Varchar));
                // Zapisywanie wartości parametrów.
                command2.Parameters[0].Value = numer;
                command2.Parameters[1].Value = data["imie"];
                command2.Parameters[2].Value = data["nazwisko"];
                command2.Parameters[3].Value = data["email"];
                command2.Parameters[4].Value = data["miasto"];
                command2.Parameters[5].Value = data["kod"];
                command2.Parameters[6].Value = data["zainteresowania"];
            }
            else if (data["miasto"] == null && data["kod"] == null && data["data"] == null && data["zainteresowania"] != null)
            {

                // Komenda dodająca klienta do bazy.
                command2 = new NpgsqlCommand("insert into dane(numer, imie, nazwisko, e_mail, zainteresowania) values(:numer, :imie, :nazwisko, :email, :zainteresowania)", conn);

                // Typy parametrów w zapytaniu.
                command2.Parameters.Add(new NpgsqlParameter("numer", NpgsqlDbType.Integer));
                command2.Parameters.Add(new NpgsqlParameter("imie", NpgsqlDbType.Varchar));
                command2.Parameters.Add(new NpgsqlParameter("nazwisko", NpgsqlDbType.Varchar));
                command2.Parameters.Add(new NpgsqlParameter("email", NpgsqlDbType.Varchar));
                command2.Parameters.Add(new NpgsqlParameter("zainteresowania", NpgsqlDbType.Varchar));
                // Zapisywanie wartości parametrów.
                command2.Parameters[0].Value = numer;
                command2.Parameters[1].Value = data["imie"];
                command2.Parameters[2].Value = data["nazwisko"];
                command2.Parameters[3].Value = data["email"];
                command2.Parameters[4].Value = data["zainteresowania"];
            }
            else if (data["miasto"] == null && data["kod"] == null && data["data"] != null && data["zainteresowania"] != null)
            {

                // Komenda dodająca klienta do bazy.
                command2 = new NpgsqlCommand("insert into dane(numer, imie, nazwisko, e_mail, data_ur, zainteresowania) values(:numer, :imie, :nazwisko, :email, :data, :zainteresowania)", conn);

                // Typy parametrów w zapytaniu.
                command2.Parameters.Add(new NpgsqlParameter("numer", NpgsqlDbType.Integer));
                command2.Parameters.Add(new NpgsqlParameter("imie", NpgsqlDbType.Varchar));
                command2.Parameters.Add(new NpgsqlParameter("nazwisko", NpgsqlDbType.Varchar));
                command2.Parameters.Add(new NpgsqlParameter("email", NpgsqlDbType.Varchar));
                command2.Parameters.Add(new NpgsqlParameter("data", NpgsqlDbType.Date));
                command2.Parameters.Add(new NpgsqlParameter("zainteresowania", NpgsqlDbType.Varchar));
                // Zapisywanie wartości parametrów.
                command2.Parameters[0].Value = numer;
                command2.Parameters[1].Value = data["imie"];
                command2.Parameters[2].Value = data["nazwisko"];
                command2.Parameters[3].Value = data["email"];
                command2.Parameters[4].Value = data["data"];
                command2.Parameters[5].Value = data["zainteresowania"];
            }
            else if (data["miasto"] == null && data["kod"] == null && data["data"] != null && data["zainteresowania"] == null)
            {

                // Komenda dodająca klienta do bazy.
                command2 = new NpgsqlCommand("insert into dane(numer, imie, nazwisko, e_mail, data_ur) values(:numer, :imie, :nazwisko, :email, :data)", conn);

                // Typy parametrów w zapytaniu.
                command2.Parameters.Add(new NpgsqlParameter("numer", NpgsqlDbType.Integer));
                command2.Parameters.Add(new NpgsqlParameter("imie", NpgsqlDbType.Varchar));
                command2.Parameters.Add(new NpgsqlParameter("nazwisko", NpgsqlDbType.Varchar));
                command2.Parameters.Add(new NpgsqlParameter("email", NpgsqlDbType.Varchar));
                command2.Parameters.Add(new NpgsqlParameter("data", NpgsqlDbType.Date));
                // Zapisywanie wartości parametrów.
                command2.Parameters[0].Value = numer;
                command2.Parameters[1].Value = data["imie"];
                command2.Parameters[2].Value = data["nazwisko"];
                command2.Parameters[3].Value = data["email"];
                command2.Parameters[4].Value = data["data"];
            }

            Int32 rowsaffected2;

            try
            {
                rowsaffected2 = command2.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Błąd połączenia z bazą danych!\n" + ex.Message);
            }

            return 0;
        }

        /// <summary>
        /// Usuwa klienta z bazy.
        /// </summary>
        /// <param name="numer"> Numer klienta. </param>  
        public void deleteClient(Int32 numer)
        {
            // Komenda usuwająca klienta z tabeli dane.
            NpgsqlCommand command2 = new NpgsqlCommand("delete from dane where numer = :numer", conn);

            // Typy parametrów w zapytaniu.
            command2.Parameters.Add(new NpgsqlParameter("numer", NpgsqlDbType.Integer));
            // Zapisywanie wartości parametrów.
            command2.Parameters[0].Value = numer;

            Int32 rowsaffected2;

            try
            {
                rowsaffected2 = command2.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Błąd połączenia z bazą danych!\n" + ex.Message);
            }

            // Komenda usuwająca klienta z tabeli uzytkownik.
            NpgsqlCommand command = new NpgsqlCommand("delete from uzytkownik where numer = :numer", conn);

            // Typy parametrów w zapytaniu.
            command.Parameters.Add(new NpgsqlParameter("numer", NpgsqlDbType.Integer));
            // Zapisywanie wartości parametrów.
            command.Parameters[0].Value = numer;

            Int32 rowsaffected;

            try
            {
                rowsaffected = command.ExecuteNonQuery();
                if (rowsaffected == 0)
                    MessageBox.Show("Nie ma takiej osoby w bazie");
                else
                    MessageBox.Show("OK!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Błąd połączenia z bazą danych!\n" + ex.Message);
            }
        }

        /// <summary>
        /// Dodaje wiadomość do bazy.
        /// </summary>
        /// <param name="login1"> Login 1. klienta. </param> 
        /// <param name="login2"> Login 2. klienta. </param>
        /// <param name="msg"> Wiadomość od 1. klienta dla 2. klienta</param>
        public void saveMessage(String login1, String login2, String msg)
        {
            NpgsqlCommand command2 = new NpgsqlCommand("insert into wiadomosci(login1, login2, tresc) values(:login1, :login2, :msg)", conn);

            // Typy parametrów w zapytaniu.
            command2.Parameters.Add(new NpgsqlParameter("login1", NpgsqlDbType.Varchar));
            command2.Parameters.Add(new NpgsqlParameter("login2", NpgsqlDbType.Varchar));
            command2.Parameters.Add(new NpgsqlParameter("msg", NpgsqlDbType.Text));
            // Zapisywanie wartości parametrów.
            command2.Parameters[0].Value = login1;
            command2.Parameters[1].Value = login2;
            command2.Parameters[2].Value = msg;

            Int32 rowsaffected2;

            try
            {
                rowsaffected2 = command2.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Błąd połączenia z bazą danych!\n" + ex.Message);
            }
        }

        /// <summary>
        /// Zwraca i usuwa wiadomości z bazy.
        /// </summary>
        /// <param name="login"> Login adresata. </param> 
        /// <returns>Zwraca listę par (nadawca, wiadomosc) dla zadanego numeru.</returns>
        public List<Pair<String, String>> getMessage(String login)
        {
            NpgsqlCommand command2 = new NpgsqlCommand("select login1, tresc from wiadomosci where login2 = :login", conn);
            // Typ parametru w zapytaniu.
            command2.Parameters.Add(new NpgsqlParameter("login", NpgsqlDbType.Varchar));
            // Zapisywanie wartości parametru.
            command2.Parameters[0].Value = login;

            List<Pair<String, String>> list = new List<Pair<String, String>>();

            try
            {
                // Wykonanie zapytania.
                NpgsqlDataReader dr2 = command2.ExecuteReader();
                while (dr2.Read())
                {
                    if (dr2.FieldCount > 0)
                    {
                        Pair<String, String> p = new Pair<String, String>(dr2[0].ToString(), dr2[1].ToString());
                        list.Add(p);
                    }
                }
                dr2.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Błąd połączenia z bazą danych!\n" + ex.Message);
            }

            NpgsqlCommand command = new NpgsqlCommand("delete from wiadomosci where login2 = :login", conn);

            // Typy parametrów w zapytaniu.
            command.Parameters.Add(new NpgsqlParameter("login", NpgsqlDbType.Varchar));
            // Zapisywanie wartości parametrów.
            command.Parameters[0].Value = login;

            Int32 rowsaffected;

            try
            {
                rowsaffected = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Błąd połączenia z bazą danych!\n" + ex.Message);
            }

            return list;
        }


        /// <summary>
        /// Modyfikuje dane klienta.
        /// </summary>
        /// <param name="login"> Login klienta. </param> 
        /// <param name="data"> Zmieniane dane. </param>
        /// <returns>Zwraca prawdę, gdy operacja przebiegnie poprawnie i fałsz w przeciwnym przypadku.</returns>
        public bool modifyClient(String login, Dictionary<String, String> dict)
        {
            // Komenda modyfikująca wpis o kliencie w tabeli uzytkownik.
            NpgsqlCommand command4 = new NpgsqlCommand("update uzytkownik set haslo = :haslo where login = :login", conn);

            // Typy parametrów w zapytaniu.
            command4.Parameters.Add(new NpgsqlParameter("login", NpgsqlDbType.Varchar));
            command4.Parameters.Add(new NpgsqlParameter("haslo", NpgsqlDbType.Varchar));
            // Zapisywanie wartości parametrów.
            command4.Parameters[0].Value = login;
            command4.Parameters[1].Value = dict["haslo"];


            Int32 rowsaffected4;

            try
            {
                rowsaffected4 = command4.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Błąd połączenia z bazą danych!\n" + ex.Message);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Zmienia status klienta.
        /// </summary>
        /// <param name="login"> Login klienta. </param> 
        /// <param name="status"> Nowy status. </param>
        public void changeStatus(String login, String status)
        {
            // Komenda modyfikująca wpis o kliencie w tabeli uzytkownik.
            NpgsqlCommand command4 = new NpgsqlCommand("update uzytkownik set status = :status where login = :login", conn);

            // Typy parametrów w zapytaniu.
            command4.Parameters.Add(new NpgsqlParameter("login", NpgsqlDbType.Varchar));
            command4.Parameters.Add(new NpgsqlParameter("status", NpgsqlDbType.Varchar));
            // Zapisywanie wartości parametrów.
            command4.Parameters[0].Value = login;
            if (status.CompareTo("dostepny") == 0)
                status = "Dostepny";
            else if (status.CompareTo("niedostepny") == 0)
                status = "Niedostepny";
            else
                status = "Niewidoczny";
            command4.Parameters[1].Value = status;

            Int32 rowsaffected4;

            try
            {
                rowsaffected4 = command4.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Błąd połączenia z bazą danych!\n" + ex.Message);
            }
        }



        /// <summary>
        /// Uwierzytelnienie klienta.
        /// </summary>
        /// <param name="login"> Nazwa użytkownika. </param> 
        /// <param name="haslo"> Hasło. </param>
        public bool login(String login, String haslo)
        {
            // Zapytanie o podstawowe dane klienta.
            NpgsqlCommand command = new NpgsqlCommand("select login, haslo, status from uzytkownik where login = :login and haslo = :haslo;", conn);
            // Typ parametru w zapytaniu.
            command.Parameters.Add(new NpgsqlParameter("login", NpgsqlDbType.Varchar));
            command.Parameters.Add(new NpgsqlParameter("haslo", NpgsqlDbType.Varchar));
            // Zapisywanie wartości parametru.
            command.Parameters[0].Value = login;
            command.Parameters[1].Value = haslo;

            try
            {
                // Wykonanie zapytania.
                NpgsqlDataReader dr = command.ExecuteReader();
                while (dr.Read())
                {
                    if (dr[0].ToString().CompareTo(login) == 0)
                    {
                        dr.Close();
                        return true;
                    }
                }

                dr.Close();
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Błąd połączenia z bazą danych!\n" + ex.Message);
                return false;
            }
        }
    }
}
