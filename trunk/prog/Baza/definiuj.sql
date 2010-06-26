--Przemys³aw Gospodarczyk i Adrian Chudziñski
--skrypt definiuj¹cy bazê u¿ytkowników dla aplikacji eTalk

-- sekwencja wyznaczaj¹ca kolejne numery
CREATE SEQUENCE num_gen;
-- sekwencja wyznaczaj¹ca kolejne identyfikatory dla kolejnych wiadomosci
CREATE SEQUENCE wiad_gen;

-- typ dla pola e_mail
CREATE DOMAIN email_type AS character varying(30) CHECK (VALUE like '%@%\.%');
-- typ dla pola kod_pocztowy
CREATE DOMAIN kod_type AS char(6) CHECK (VALUE like '%-%' AND LENGTH(VALUE) = 6);
-- typ dla pola status
CREATE DOMAIN status_type AS character varying(20) CHECK (VALUE = 'Dostepny' OR VALUE = 'Niedostepny' OR VALUE = 'Niewidoczny' OR VALUE = 'Zaraz wracam');

-- tabela u¿ytkownik
CREATE TABLE uzytkownik
(
    numer integer NOT NULL DEFAULT NEXTVAL('num_gen'),
    login character varying(15) NOT NULL UNIQUE,
    haslo character varying(15) NOT NULL,
    ostatnieZapytanie timestamp,
    status status_type NOT NULL,
    CONSTRAINT uzytkownik_pkey PRIMARY KEY (numer),
    CONSTRAINT uzytkownik_haslo CHECK (LENGTH(haslo) > 5)    
);

-- tabela danych u¿ytkownika
CREATE TABLE dane
(
    numer integer NOT NULL,
    imie character varying(30) NOT NULL,
    nazwisko character varying(30) NOT NULL,
    miasto character varying(30),
    kod_pocztowy kod_type,
    e_mail email_type NOT NULL,
    data_ur date,
    zainteresowania text, 
    CONSTRAINT dane_pkey PRIMARY KEY (numer),
    CONSTRAINT fk_dane FOREIGN KEY (numer)
        REFERENCES uzytkownik(numer) MATCH SIMPLE
        ON UPDATE NO ACTION ON DELETE NO ACTION  
);

-- tabela wiadomosci
CREATE TABLE wiadomosci
(
    id integer NOT NULL DEFAULT NEXTVAL('wiad_gen'),
    login1 character varying(15) NOT NULL,
    login2 character varying(15) NOT NULL,
    tresc TEXT NOT NULL,
    CONSTRAINT wiadomosc_pkey PRIMARY KEY (id),
    CONSTRAINT fk1_wiadomosc FOREIGN KEY (login1)
        REFERENCES uzytkownik(login) MATCH SIMPLE
        ON UPDATE NO ACTION ON DELETE NO ACTION,
    CONSTRAINT fk2_wiadomosc FOREIGN KEY (login2)
        REFERENCES uzytkownik(login) MATCH SIMPLE
        ON UPDATE NO ACTION ON DELETE NO ACTION    
);

-- Indeks: nazwisko u¿ytkownika (przyda siê przy sortowaniu i wyszukiwaniu)
CREATE INDEX ix_nazw_uzyt ON dane(nazwisko);
-- Indeks: nazwa u¿ytkownika (przyda siê przy sortowaniu i wyszukiwaniu)
CREATE INDEX ix_login_uzyt ON uzytkownik(login);
-- Indeks: numer (przyda siê przy wysylaniu starych, niedostarczonych wiadomosci)
CREATE INDEX ix_num2_wiad ON wiadomosci(numer2);

-- tworzymy u¿ytkowników bazy opisanych wczeœniej w modelu konceptualnym
-- klientów mo¿e byæ dowolnie du¿o, ale zdefiniujemy tylko jednego
CREATE USER klient1 PASSWORD 'przemek';
CREATE USER admin1 PASSWORD 'przemek';

-- admininistrator ma pe³ne uprawnienia
GRANT ALL ON uzytkownik, dane TO admin1;
-- klient ma ograniczone uprawnienia
GRANT INSERT, UPDATE ON uzytkownik, dane TO klient1;
