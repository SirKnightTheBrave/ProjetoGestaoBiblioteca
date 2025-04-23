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
    address VARCHAR(50),
    phone VARCHAR(50),
    `password` VARCHAR(50) NOT NULL,
    isAdmin BOOL DEFAULT FALSE
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