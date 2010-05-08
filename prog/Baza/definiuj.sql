--Przemys³aw Gospodarczyk i Adrian Chudziñski
--skrypt definiuj¹cy bazê u¿ytkowników dla aplikacji eTalk

-- sekwencja wyznaczaj¹ca kolejne numery
CREATE SEQUENCE num_gen;
-- sekwencja wyznaczaj¹ca kolejne identyfikatory dla kolejnych pary znajomych
CREATE SEQUENCE znaj_gen;

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

-- tabela znajomi
CREATE TABLE znajomi
(
    znajomi_id integer NOT NULL DEFAULT NEXTVAL('znaj_gen'),
    numer1 integer NOT NULL,
    numer2 integer NOT NULL,
    CONSTRAINT znajomi_pkey PRIMARY KEY (znajomi_id),
    CONSTRAINT fk1_znajomi FOREIGN KEY (numer1)
        REFERENCES uzytkownik(numer) MATCH SIMPLE
        ON UPDATE NO ACTION ON DELETE NO ACTION,
    CONSTRAINT fk2_znajomi FOREIGN KEY (numer2)
        REFERENCES uzytkownik(numer) MATCH SIMPLE
        ON UPDATE NO ACTION ON DELETE NO ACTION    
);

-- Indeks: nazwisko u¿ytkownika (przyda siê przy sortowaniu i wyszukiwaniu)
CREATE INDEX ix_nazw_uzyt ON dane(nazwisko);
-- Indeks: nazwa u¿ytkownika (przyda siê przy sortowaniu i wyszukiwaniu)
CREATE INDEX ix_login_uzyt ON uzytkownik(login);
-- Indeks: numer (przyda siê przy wyszukiwaniu znajomych)
CREATE INDEX ix_num1_uzyt ON znajomi(numer1);
-- Indeks: nazwa u¿ytkownika (przyda siê przy wyszukiwaniu znajomych)
CREATE INDEX ix_num2_uzyt ON znajomi(numer2);

-- tworzymy u¿ytkowników bazy opisanych wczeœniej w modelu konceptualnym
-- klientów mo¿e byæ dowolnie du¿o, ale zdefiniujemy tylko jednego
CREATE USER klient1 PASSWORD 'przemek';
CREATE USER admin1 PASSWORD 'przemek';

-- admininistrator ma pe³ne uprawnienia
GRANT ALL ON uzytkownik, znajomi, dane TO admin1;
-- klient ma ograniczone uprawnienia
GRANT INSERT, UPDATE ON uzytkownik, dane TO klient1;
GRANT DELETE, INSERT, UPDATE ON znajomi TO klient1;
