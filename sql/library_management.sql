CREATE DATABASE library_management;
USE library_management;

/*
user, name and password are reserved words
PRIMARY KEY implies UNIQUE
*/
CREATE TABLE users (
	id INT PRIMARY KEY AUTO_INCREMENT,
    `name` VARCHAR(50) NOT NULL,
    username VARCHAR(50) UNIQUE NOT NULL, -- unique implies not null?
    address VARCHAR(50) NOT NULL,
    phone VARCHAR(50) NOT NULL,
    `password` VARCHAR(50) NOT NULL,
    is_admin BOOL DEFAULT FALSE
);

CREATE TABLE books (
	id INT PRIMARY KEY AUTO_INCREMENT,
    title VARCHAR(50) NOT NULL,
    author VARCHAR(50) NOT NULL,
    publication_year INT,
    total_copies INT UNSIGNED DEFAULT 0,
    available_copies INT UNSIGNED DEFAULT 0,
    UNIQUE (title, author)
);

CREATE TABLE copies (
    id INT PRIMARY KEY AUTO_INCREMENT,
    `code` INT UNIQUE NOT NULL,
    book_id INT NOT NULL,
    edition INT,
    `condition` ENUM('Good', 'Fair', 'Worn'),
    is_loaned BOOL DEFAULT FALSE,
    FOREIGN KEY (book_id) REFERENCES books(id) ON DELETE CASCADE
);


CREATE TABLE loans (
	id INT PRIMARY KEY AUTO_INCREMENT,
    copy_id INT NOT NULL,
    `return_condition` ENUM('Good', 'Fair', 'Worn') DEFAULT NULL,
    loan_from DATE DEFAULT NULL,
    loan_until DATE DEFAULT NULL,
    user_id INT NOT NULL,
    FOREIGN KEY (copy_id) REFERENCES copies(id),  
    FOREIGN KEY (user_id) REFERENCES users(id)   
);

CREATE TABLE library (
	id INT PRIMARY KEY AUTO_INCREMENT,
    `name` VARCHAR(50) UNIQUE,
	MaxUserLoans INT,
    LoanPeriodInDays INT
);
CREATE VIEW view_current_loans AS
SELECT*
FROM loans
WHERE loan_until IS NULL;

DELIMITER $$
CREATE PROCEDURE loan_copy (IN copy_code INT, IN user_name VARCHAR(50))
BEGIN
	DECLARE copy_id INT;
    DECLARE book_code INT;
    DECLARE user_id INT;
    
	SELECT id, book_id INTO copy_id, book_code
	FROM copies
	WHERE `code` = copy_code;
	
    SELECT id INTO user_id
    FROM users
    WHERE `username` = user_name;
    
    UPDATE books SET available_copies = available_copies - 1 WHERE books.id = book_code;
	UPDATE copies SET is_loaned = true WHERE copies.code = copy_code ;
	INSERT INTO loans (copy_id, loan_from, user_id) VALUES (copy_id, current_date(), user_id); 
END $$

CREATE PROCEDURE return_copy (IN copy_code INT)
BEGIN
	DECLARE copy_id INT;
    DECLARE book_id INT;
  
    
	SELECT id, book_id INTO copy_id, book_id
	FROM copies
	WHERE `code` = copy_code;
	
    
    UPDATE books SET available_copies = available_copies + 1 WHERE books.id = book_id;
	UPDATE copies SET is_loaned = false WHERE copies.code = copy_code ;
	UPDATE loans SET loan_until = current_date(); 
END $$

CREATE PROCEDURE add_copy (IN book_title VARCHAR(50), IN book_author VARCHAR(50), IN copy_code INT, IN copy_edition INT, IN copy_condition ENUM('Good', 'Fair', 'Worn'))
BEGIN
	DECLARE book_code INT;
    
	SELECT id INTO book_code
	FROM books
	WHERE `title` = book_title AND `author`= book_author;
	
    
    
    UPDATE books SET available_copies = available_copies + 1, total_copies = total_copies + 1 WHERE books.id = book_code;
	INSERT INTO copies (book_id, `code`, edition, `condition`) VALUES (book_code, copy_code, copy_edition, copy_condition); 
END $$
