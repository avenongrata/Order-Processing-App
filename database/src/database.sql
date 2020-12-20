CREATE TABLE Договор
(
	id_client integer NOT NULL,
	id_dogovora integer NOT NULL,
	id_zakaza integer NOT NULL,
	Дата_заключения datetime NULL,
	Дата_истечения datetime NULL,
	id_spezialista integer NOT NULL
)
go



ALTER TABLE Договор
	ADD CONSTRAINT XPKДоговор PRIMARY KEY CLUSTERED (id_dogovora ASC)
go



CREATE TABLE Доставка
(
	id_dostavki integer NOT NULL,
	Дата_доставки datetime NULL,
	ФИО_курьера varchar(250) NULL,
	id_zakaza integer NOT NULL,
	Адрес_доставки varchar(250) NULL
)
go



ALTER TABLE Доставка
	ADD CONSTRAINT XPKДоставка PRIMARY KEY CLUSTERED (id_dostavki ASC)
go



CREATE TABLE Заказ
(
	id_client integer NOT NULL,
	id_zakaza integer NOT NULL,
	Статус               varchar(500) NULL,
	Количество_позиций integer NULL,
	id_tovara integer NOT NULL,
	id_spezialista integer NOT NULL
)
go



ALTER TABLE Заказ
	ADD CONSTRAINT XPKЗаказ PRIMARY KEY CLUSTERED (id_zakaza ASC)
go



CREATE TABLE Каталог
(
	id_kataloga integer NOT NULL,
	id_tovara integer NOT NULL,
	Количество           integer NULL,
	Название_каталога varchar(250) NULL,
	Описание             varchar(500) NULL
)
go



ALTER TABLE Каталог
	ADD CONSTRAINT XPKКаталог PRIMARY KEY CLUSTERED (id_kataloga ASC)
go



CREATE TABLE Квитанция
(
	id_zakaza integer NOT NULL,
	id_kvitanzii integer NOT NULL,
	Сумма                money NULL,
	Дата_квитанции datetime NULL,
	Номер_счета varchar(250) NULL
)
go



ALTER TABLE Квитанция
	ADD CONSTRAINT XPKКвитанция PRIMARY KEY CLUSTERED (id_kvitanzii ASC)
go



CREATE TABLE Клиент
(
	id_client integer NOT NULL,
	ФИО                  varchar(250)NULL,
	Реквизиты            varchar(250)NULL,
	Телефон              varchar(11)NULL,
	Паспорт              varchar(250)NULL,
	Адрес_регистрации varchar(250) NULL,
	Дата_рождения datetime NULL
)
go



ALTER TABLE Клиент
	ADD CONSTRAINT XPKКлиент PRIMARY KEY CLUSTERED (id_client ASC)
go



CREATE TABLE Поставщик
(
	id_postavshika integer NOT NULL,
	ФИО                  varchar(250)NULL,
	Реквизиты            varchar(250)NULL,
	Телефон              varchar(11)NULL,
	Паспорт              varchar(250)NULL,
	ИНН                  varchar(250)NULL,
	СНИЛС                varchar(250)NULL
)
go



ALTER TABLE Поставщик
	ADD CONSTRAINT XPKПоставщик PRIMARY KEY CLUSTERED (id_postavshika ASC)
go



CREATE TABLE Склад
(
	id_sklada integer NOT NULL,
	Название_склада varchar(250) NULL,
	Адрес_склада varchar(250) NULL,
	Телефон              varchar(11) NULL,
	id_tovara integer NOT NULL
)
go



ALTER TABLE Склад
	ADD CONSTRAINT XPKСклад PRIMARY KEY CLUSTERED (id_sklada ASC)
go



CREATE TABLE Специалист
(
	id_spezialista integer NOT NULL,
	ФИО                  varchar(250) NULL,
	Паспорт              varchar(250) NULL,
	Дата_рождения datetime NULL,
	Телефон              varchar(11) NULL,
	Снилс varchar(250) NULL,
	Адрес_регистрации varchar(250) NULL,
	ИНН                  varchar(250)NULL
)
go



ALTER TABLE Специалист
	ADD CONSTRAINT XPKСпециалист PRIMARY KEY CLUSTERED (id_spezialista ASC)
go



CREATE TABLE Товар
(
	id_tovara integer NOT NULL,
	id_postavshika integer NOT NULL,
	Количество           integer NULL,
	Наименование         varchar(250) NULL,
	Габариты             varchar(100) NULL,
	Вес                  integer NULL
)
go



ALTER TABLE Товар
	ADD CONSTRAINT XPKТовар PRIMARY KEY CLUSTERED (id_tovara ASC)
go




ALTER TABLE Договор
	ADD CONSTRAINT R_2 FOREIGN KEY (id_client) REFERENCES Клиент(id_client)
		ON DELETE NO ACTION
		ON UPDATE NO ACTION
go




ALTER TABLE Договор
	ADD CONSTRAINT R_4 FOREIGN KEY (id_zakaza) REFERENCES Заказ(id_zakaza)
		ON DELETE NO ACTION
		ON UPDATE NO ACTION
go




ALTER TABLE Договор
	ADD CONSTRAINT R_10 FOREIGN KEY (id_spezialista) REFERENCES Специалист(id_spezialista)
		ON DELETE NO ACTION
		ON UPDATE NO ACTION
go




ALTER TABLE Доставка
	ADD CONSTRAINT R_5 FOREIGN KEY (id_zakaza) REFERENCES Заказ(id_zakaza)
		ON DELETE NO ACTION
		ON UPDATE NO ACTION
go




ALTER TABLE Заказ
	ADD CONSTRAINT R_1 FOREIGN KEY (id_client) REFERENCES Клиент(id_client)
		ON DELETE NO ACTION
		ON UPDATE NO ACTION
go




ALTER TABLE Заказ
	ADD CONSTRAINT R_3 FOREIGN KEY (id_tovara) REFERENCES Товар(id_tovara)
		ON DELETE NO ACTION
		ON UPDATE NO ACTION
go




ALTER TABLE Заказ
	ADD CONSTRAINT R_6 FOREIGN KEY (id_spezialista) REFERENCES Специалист(id_spezialista)
		ON DELETE NO ACTION
		ON UPDATE NO ACTION
go




ALTER TABLE Каталог
	ADD CONSTRAINT R_8 FOREIGN KEY (id_tovara) REFERENCES Товар(id_tovara)
		ON DELETE NO ACTION
		ON UPDATE NO ACTION
go




ALTER TABLE Квитанция
	ADD CONSTRAINT R_11 FOREIGN KEY (id_zakaza) REFERENCES Заказ(id_zakaza)
		ON DELETE NO ACTION
		ON UPDATE NO ACTION
go




ALTER TABLE Склад
	ADD CONSTRAINT R_7 FOREIGN KEY (id_tovara) REFERENCES Товар(id_tovara)
		ON DELETE NO ACTION
		ON UPDATE NO ACTION
go




ALTER TABLE Товар
	ADD CONSTRAINT R_9 FOREIGN KEY (id_postavshika) REFERENCES Поставщик(id_postavshika)
		ON DELETE NO ACTION
		ON UPDATE NO ACTION
go
