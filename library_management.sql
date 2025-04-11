CREATE DATABASE library_management;
USE library_management;

/*
user, name and password are reserved words
PRIMARY KEY implies UNIQUE
*/
CREATE TABLE `user` (
	id INT PRIMARY KEY AUTO_INCREMENT,
    `name` VARCHAR(50) NOT NULL,
    username VARCHAR(50) UNIQUE NOT NULL, -- unique implies not null?
    address VARCHAR(50),
    phone VARCHAR(50),
    `password` VARCHAR(50) NOT NULL,
    isAdmin BOOL DEFAULT FALSE
);

CREATE TABLE book (
	id INT PRIMARY KEY AUTO_INCREMENT,
    title VARCHAR(50) NOT NULL,
);