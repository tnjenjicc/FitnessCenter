-- MembershipType
INSERT INTO `fitness_center_hci`.`MembershipType` (`Name`, `CurrentPrice`) VALUES
('mjesecno', 50.00),
('kvartalno', 135.00),
('godisnje', 500.00),
('student-mjsecno', 40.00),
('student-kvartalno', 108.00),
('student-godisnje', 400.00),
('senior-mjesecno', 45.00),
('senior-kvartalno', 121.50),
('senior-godisnje', 450.00);


-- Membership
INSERT INTO `fitness_center_hci`.`Membership` (`ExpirationDate`, `Member_User_Id`, `MembershipType_IdType`) VALUES
('2023-12-31', 1, 1),
('2023-12-31', 3, 2),
('2023-12-31', 5, 3),
('2024-12-31', 1, 4),
('2024-12-31', 3, 5);

-- Hall
INSERT INTO `fitness_center_hci`.`Hall` (`Name`, `Location`, `Capacity`) VALUES
('Main Hall', 'First Floor', 50),
('Yoga Room', 'Second Floor', 30),
('Weight Room', 'Basement', 40),
('Cardio Room', 'Third Floor', 20),
('Spin Room', 'Ground Floor', 25);

-- TrainingSession
INSERT INTO `fitness_center_hci`.`TrainingSession` (`Session`, `Trainer_User_Id`, `Hall_IdHall`) VALUES
('Monday 5pm', 6, 1),
('Wednesday 8pm', 6, 2),
('Friday 7pm', 7, 3),
('Tuesday 9am', 7, 4),
('Thursday 5pm', 7, 5),
('Monday 6am', 8, 1),
('Wednesday 9am', 8, 2),
('Friday 4pm', 9, 3),
('Tuesday 5am', 10, 4),
('Thursday 8pm', 10, 5);

-- MembershipPayment
INSERT INTO `fitness_center_hci`.`MembershipPayment` (`BillingDate`, `PaymentMethod`, `Price`, `Membership_IdMembership`, `IsPaid`) VALUES
('2023-01-01', 'Credit Card', 50.00, 1, 1),
('2023-01-01', 'Cash', 135.00, 2, 1),
('2023-01-01', 'Credit Card', 500.00, 3, 1),
('2023-01-01', 'Debit Card', 40.00, 4, 1),
('2023-01-01', 'Cash', 45.00, 5, 1),
('2023-11-01', 'Credit Card', 500.00, 2, 1),
('2023-12-01', 'Cash', 135.00, 3, 1),
('2023-03-01', 'Credit Card', 50.00, 4, 1);

-- TrainingReservation
INSERT INTO `fitness_center_hci`.`TrainingReservation` (`Member_User_Id`, `TrainingSession_IdSession`) VALUES
(1, 1),
(3, 2),
(5, 3),
(3, 10),
(4, 9),
(5, 8);


-- GroupMembership
INSERT INTO `fitness_center_hci`.`GroupMembership` (`Group_User_Id`, `Member_User_Id`, `JoinDate`, `LeaveDate`) VALUES
(13, 1, '2023-01-01'),
(13, 3, '2023-02-01'),
(13, 5, '2023-03-01');

-- Promotion
INSERT INTO `fitness_center_hci`.`Promotion` (`Description`, `ValidFrom`, `ValidUntil`, `Discount`, `Member_User_Id`, `Membership_IdMembership`) VALUES
('New Year Special', '2023-01-01 00:00:00', '2023-01-31 23:59:59', 10.00, 1, 1),
('Summer Offer', '2023-06-01 00:00:00', '2023-08-31 23:59:59', 15.00, 3, 2),
('Holiday Discount', '2023-12-01 00:00:00', '2023-12-31 23:59:59', 20.00, 5, 3);