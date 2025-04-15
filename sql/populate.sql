INSERT INTO `books` (`id`, `title`, `author`, `publication_year`, `total_copies`, `available_copies`) VALUES
(1,	'The Book Thief',	'Markus Zusak',	2005,	5,	5),  
(2, 'To Kill a Mockingbird', 'Harper Lee', 1960, 4, 4),
(3, '1984', 'George Orwell', 1949, 6, 5),
(4, 'Pride and Prejudice', 'Jane Austen', 1813, 3, 2),
(5, 'The Great Gatsby', 'F. Scott Fitzgerald', 1925, 5, 3);

INSERT INTO copies (`code`, book_id, edition, `condition`, is_loaned) VALUES
(1001, 1, 1, 'Good', FALSE),
(1002, 1, 1, 'Good', FALSE),
(1003, 1, 2, 'Fair', FALSE),
(1004, 1, 2, 'Good', FALSE),
(1005, 1, 3, 'Good', FALSE),

(2001, 2, 1, 'Good', FALSE),
(2002, 2, 1, 'Fair', FALSE),
(2003, 2, 2, 'Worn', FALSE),
(2004, 2, 1, 'Good', FALSE),

(3001, 3, 1, 'Worn', TRUE),
(3002, 3, 1, 'Fair', FALSE),
(3003, 3, 2, 'Fair', FALSE),
(3004, 3, 1, 'Good', FALSE),
(3005, 3, 2, 'Good', FALSE),
(3006, 3, 2, 'Good', FALSE),

(4001, 4, 1, 'Fair', TRUE),
(4002, 4, 1, 'Fair', FALSE),
(4003, 4, 2, 'Worn', FALSE),

(5001, 5, 1, 'Good', TRUE),
(5002, 5, 2, 'Good', FALSE),
(5003, 5, 2, 'Fair', FALSE),
(5004, 5, 1, 'Good', FALSE),
(5005, 5, 2, 'Good', TRUE);


INSERT INTO users (`name`, username, address, phone, `password`, isAdmin) VALUES
('admin', 'admin', '123 Admin St', '123-456-7890', 'password', TRUE),
('user', 'user', '456 User Ln', '987-654-3210', 'password', FALSE),
('Alice Johnson', 'alicej', '12 Maple Ave', '555-123-4567', 'alicepass', FALSE),
('Bob Smith', 'bobsmith', '34 Oak St', '555-987-6543', 'bobsecure', FALSE),
('Carol White', 'carolw', '56 Pine Rd', '555-321-9876', 'carol123', FALSE),
('David Brown', 'davidb', '78 Cedar Blvd', '555-654-3210', 'davidpw', FALSE),
('Eve Adams', 'evea', '90 Birch Way', '555-111-2222', 'evepass', FALSE),
('Frank Martin', 'frankm', '21 Elm St', '555-333-4444', 'frankpass', FALSE),
('Grace Lee', 'gracel', '43 Willow Ln', '555-555-6666', 'gracepw', FALSE),
('Henry Clark', 'henryc', '65 Poplar Ct', '555-777-8888', 'henrypass', FALSE);


INSERT INTO loans (copy_id, return_condition, loan_from, loan_to, user_id) VALUES
(3001, NULL, '2025-04-01', NULL, 2),  
(4001, NULL, '2025-03-28', NULL, 4),  
(5001, NULL, '2025-04-10', NULL, 5),  
(5005, NULL, '2025-04-12', NULL, 7);  

);
