-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Mar 27, 2026 at 07:25 AM
-- Server version: 10.4.32-MariaDB
-- PHP Version: 8.2.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `bvsdb`
--

-- --------------------------------------------------------

--
-- Table structure for table `customers`
--

CREATE TABLE `customers` (
  `CustomerId` int(11) NOT NULL,
  `FullName` longtext NOT NULL,
  `Contact` longtext NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `customers`
--

INSERT INTO `customers` (`CustomerId`, `FullName`, `Contact`) VALUES
(1, 'Thirdy Andrade', '09123456789'),
(2, 'Thirdy Andrade 1', '09123456789');

-- --------------------------------------------------------

--
-- Table structure for table `rentals`
--

CREATE TABLE `rentals` (
  `RentalId` int(11) NOT NULL,
  `CustomerId` int(11) NOT NULL,
  `VideoId` int(11) NOT NULL,
  `RentedDate` datetime(6) NOT NULL,
  `ReturnDate` datetime(6) DEFAULT NULL,
  `DueDate` datetime(6) NOT NULL,
  `Status` longtext NOT NULL,
  `Penalty` decimal(10,2) NOT NULL,
  `Price` decimal(10,2) NOT NULL DEFAULT 0.00
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `rentals`
--

INSERT INTO `rentals` (`RentalId`, `CustomerId`, `VideoId`, `RentedDate`, `ReturnDate`, `DueDate`, `Status`, `Penalty`, `Price`) VALUES
(1, 1, 1, '2026-03-27 13:08:07.491874', '2026-03-27 13:08:11.032557', '2026-03-29 13:08:07.492417', 'Returned', 0.00, 25.00),
(2, 1, 1, '2026-03-27 13:16:40.485292', '2026-03-27 14:05:52.926615', '2026-03-29 13:16:40.485294', 'Returned', 0.00, 25.00),
(3, 2, 2, '2026-03-27 14:23:11.193764', '2026-03-27 14:24:11.716193', '2026-03-30 14:23:11.194259', 'Returned', 0.00, 50.00);

-- --------------------------------------------------------

--
-- Table structure for table `videos`
--

CREATE TABLE `videos` (
  `VideoId` int(11) NOT NULL,
  `Title` longtext NOT NULL,
  `Category` int(11) NOT NULL,
  `AvailableQuantity` int(11) NOT NULL,
  `TotalQuantity` int(11) NOT NULL,
  `RentalDays` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `videos`
--

INSERT INTO `videos` (`VideoId`, `Title`, `Category`, `AvailableQuantity`, `TotalQuantity`, `RentalDays`) VALUES
(1, 'Vivamax', 0, 100, 100, 2),
(2, 'VivaMax 1', 1, 100, 100, 3);

-- --------------------------------------------------------

--
-- Table structure for table `__efmigrationshistory`
--

CREATE TABLE `__efmigrationshistory` (
  `MigrationId` varchar(150) NOT NULL,
  `ProductVersion` varchar(32) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `__efmigrationshistory`
--

INSERT INTO `__efmigrationshistory` (`MigrationId`, `ProductVersion`) VALUES
('20260327031748_InitialCreate', '9.0.5'),
('20260327044958_InitialCreate1', '9.0.5'),
('20260327050210_InitialCreate2', '9.0.5');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `customers`
--
ALTER TABLE `customers`
  ADD PRIMARY KEY (`CustomerId`);

--
-- Indexes for table `rentals`
--
ALTER TABLE `rentals`
  ADD PRIMARY KEY (`RentalId`),
  ADD KEY `IX_Rentals_CustomerId` (`CustomerId`),
  ADD KEY `IX_Rentals_VideoId` (`VideoId`);

--
-- Indexes for table `videos`
--
ALTER TABLE `videos`
  ADD PRIMARY KEY (`VideoId`);

--
-- Indexes for table `__efmigrationshistory`
--
ALTER TABLE `__efmigrationshistory`
  ADD PRIMARY KEY (`MigrationId`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `customers`
--
ALTER TABLE `customers`
  MODIFY `CustomerId` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT for table `rentals`
--
ALTER TABLE `rentals`
  MODIFY `RentalId` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT for table `videos`
--
ALTER TABLE `videos`
  MODIFY `VideoId` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `rentals`
--
ALTER TABLE `rentals`
  ADD CONSTRAINT `FK_Rentals_Customers_CustomerId` FOREIGN KEY (`CustomerId`) REFERENCES `customers` (`CustomerId`) ON DELETE CASCADE,
  ADD CONSTRAINT `FK_Rentals_Videos_VideoId` FOREIGN KEY (`VideoId`) REFERENCES `videos` (`VideoId`) ON DELETE CASCADE;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
