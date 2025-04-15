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

