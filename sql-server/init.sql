-- This script initializes the database schema for a document management system.
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'DocManager')
BEGIN
    CREATE DATABASE DocManager;
END;
GO

USE DocManager;
GO

-- If the documents table exists, it means the whole schema has been initialized.
CREATE TABLE DOCUMENTS(
	id INT NOT NULL IDENTITY PRIMARY KEY,
	doc_num NVARCHAR(64),
	subject NVARCHAR(128),
	body NVARCHAR(1042),
	usr_sender INT DEFAULT NULL,
	name_sender NVARCHAR(64) DEFAULT NULL,
	lname_sender NVARCHAR(128) DEFAULT NULL,
	title_sender NVARCHAR(64) DEFAULT NULL,
	position_sender NVARCHAR(128) DEFAULT NULL,
	usr_recip INT DEFAULT NULL,
	name_recip NVARCHAR(64) DEFAULT NULL,
	lname_recip NVARCHAR(128) DEFAULT NULL,
	title_recip NVARCHAR(64) DEFAULT NULL,
	position_recip NVARCHAR(128) DEFAULT NULL,
	unit_belong INT,
	dept_name NVARCHAR(256) DEFAULT NULL,
	date_create DATETIME2 DEFAULT GETDATE(),
	date_done DATETIME2 DEFAULT NULL,
	usr_assign INT DEFAULT NULL,
	gen_by_usr INT DEFAULT NULL,
	doc_type INT DEFAULT 0,
	anonym BIT DEFAULT 0,
	deleted BIT DEFAULT 0,
);
GO

CREATE TABLE USER_ACCOUNTS(
	code INT NOT NULL PRIMARY KEY,
	name_usr NVARCHAR(64) NOT NULL,
	lname NVARCHAR(128),
	email NVARCHAR(256),
	unit_belong INT DEFAULT NULL,
	usr_role INT,
	deleted BIT DEFAULT 0,
);
GO

CREATE TABLE UNITS(
	id INT NOT NULL IDENTITY PRIMARY KEY,
	name NVARCHAR(64) NOT NULL,
	prefix NVARCHAR(32) DEFAULT NULL,
	deleted BIT DEFAULT 0,
);
GO

CREATE TABLE USER_ROLES(
	id INT NOT NULL IDENTITY PRIMARY KEY,
	name NVARCHAR(64),
	deleted BIT DEFAULT 0,
);
GO

CREATE TABLE ASSIGNMENT_LOGS(
	id INT NOT NULL IDENTITY PRIMARY KEY,
	doc_id INT NOT NULL,
	usr_assign INT NOT NULL,
	date_assign DATETIME2 DEFAULT GETDATE(),
);
GO

ALTER TABLE DOCUMENTS
ADD FOREIGN KEY(usr_sender) REFERENCES USER_ACCOUNTS(code);
GO

ALTER TABLE DOCUMENTS
ADD FOREIGN KEY(usr_recip) REFERENCES USER_ACCOUNTS(code);
GO

ALTER TABLE DOCUMENTS
ADD FOREIGN KEY(unit_belong) REFERENCES UNITS(id);
GO

ALTER TABLE DOCUMENTS
ADD FOREIGN KEY(usr_assign) REFERENCES USER_ACCOUNTS(code);
GO

ALTER TABLE DOCUMENTS
ADD FOREIGN KEY(gen_by_usr) REFERENCES USER_ACCOUNTS(code);
GO

ALTER TABLE USER_ACCOUNTS
ADD FOREIGN KEY(unit_belong) REFERENCES UNITS(id);
GO

ALTER TABLE USER_ACCOUNTS
ADD FOREIGN KEY(usr_role) REFERENCES USER_ROLES(id);
GO

ALTER TABLE ASSIGNMENT_LOGS
ADD FOREIGN KEY(doc_id) REFERENCES DOCUMENTS(id);
GO

ALTER TABLE ASSIGNMENT_LOGS
ADD FOREIGN KEY(usr_assign) REFERENCES USER_ACCOUNTS(code);
GO

INSERT INTO USER_ROLES (name) VALUES ('Admin');
INSERT INTO USER_ROLES (name) VALUES ('Common');
GO

CREATE TRIGGER tgr_generate_doc_num
ON DOCUMENTS
INSTEAD OF INSERT
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @current_year INT = YEAR(GETDATE());
	DECLARE @unit_belong INT;
	DECLARE @prefix NVARCHAR(16);
	DECLARE @num_year INT;
	DECLARE @doc_num NVARCHAR(32);
	DECLARE @counter INT;
	DECLARE @doc_type INT;
	DECLARE @doc_num_input NVARCHAR(64);

	SELECT TOP 1 
		@unit_belong = unit_belong,
		@doc_type = doc_type,
		@doc_num_input = doc_num
	FROM inserted;

	-- Checks if the document is internal or external. 0 = internal, 1 = external.
	IF @doc_type = 0
	BEGIN
		SELECT @prefix = prefix FROM UNITS WHERE id = @unit_belong;

		SELECT TOP 1
			@num_year = YEAR(date_create),
			@counter = TRY_CAST(LEFT(doc_num, 6) AS INT)
		FROM DOCUMENTS ORDER BY id DESC;

		IF @counter IS NULL OR @num_year != @current_year
			SET @counter = 1;
		ELSE
			SET @counter = @counter + 1

		SET @doc_num = RIGHT('000000' + CAST(@counter AS NVARCHAR), 6) + '/' + @prefix + '/' + CAST(@current_year AS NVARCHAR);
	
	END
	ELSE
	BEGIN
		-- If the documents is external and there''s no input document number. We generate a generic one.
		IF @doc_num_input IS NULL OR @doc_num_input = ''
		BEGIN
			SELECT TOP 1
				@num_year = YEAR(date_create),
				@counter = TRY_CAST(LEFT(doc_num, 6) AS INT)
			FROM DOCUMENTS WHERE doc_type = 1 ORDER BY id DESC;

			IF @counter IS NULL OR @num_year != @current_year
				SET @counter = 1;
			ELSE
				SET @counter = @counter + 1;

			SET @doc_num = RIGHT('000000' + CAST(@counter AS NVARCHAR), 6) + '/SN/' + CAST(@current_year AS NVARCHAR);
		END
		ELSE
		BEGIN
			SET @doc_num = @doc_num_input;
		END
	END

	-- Insert the new document with the generated document number
		INSERT INTO DOCUMENTS (doc_num, subject, body, usr_sender, name_sender, lname_sender, title_sender, position_sender, usr_recip, name_recip, lname_recip, title_recip, position_recip, unit_belong, dept_name, date_create, date_done, usr_assign, gen_by_usr, doc_type, anonym, deleted)
		SELECT
			@doc_num,
			inserted.subject,
			inserted.body,
			inserted.usr_sender,
			inserted.name_sender,
			inserted.lname_sender,
			inserted.title_sender,
			inserted.position_sender,
			inserted.usr_recip,
			inserted.name_recip,
			inserted.lname_recip,
			inserted.title_recip,
			inserted.position_recip,
			inserted.unit_belong,
			inserted.dept_name,
			inserted.date_create,
			inserted.date_done,
			inserted.usr_assign,
			inserted.gen_by_usr,
			inserted.doc_type,
			inserted.anonym,
			inserted.deleted
		FROM inserted;

END;
