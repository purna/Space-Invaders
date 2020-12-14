-- phpMyAdmin SQL Dump
-- version 4.9.5
-- https://www.phpmyadmin.net/
--
-- Host: localhost:3306
-- Generation Time: Dec 12, 2020 at 01:46 PM
-- Server version: 5.7.30
-- PHP Version: 7.4.9

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";

--
-- Database: `unityaccess`
--

-- --------------------------------------------------------

--
-- Table structure for table `leaderboard`
--

CREATE TABLE `leaderboard` (
  `id` int(10) NOT NULL,
  `name` varchar(10) NOT NULL,
  `score` int(10) NOT NULL,
  `ts` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

--
-- Dumping data for table `leaderboard`
--

INSERT INTO `leaderboard` (`id`, `name`, `score`, `ts`) VALUES
(1, 'nms', 0, '2020-12-09 09:55:52'),
(2, 'mdf', 0, '2020-12-09 09:56:31'),
(3, 'nsm', 10, '2020-12-09 17:09:12'),
(4, 'sss', 21, '2020-12-09 17:10:50'),
(5, 'Test', 9999, '2020-12-09 17:27:10'),
(6, 'Test', 100, '2020-12-09 17:29:27'),
(7, 'Test', 100, '2020-12-09 18:38:02'),
(8, 'Test', 2342, '2020-12-09 18:41:11'),
(9, 'Test', 100, '2020-12-09 18:46:13'),
(10, 'Test', 100, '2020-12-09 18:48:36'),
(11, 'Test', 100, '2020-12-09 18:48:58'),
(12, 'Test', 100, '2020-12-09 18:58:15'),
(13, 'Test', 100, '2020-12-09 18:59:12'),
(14, 'Test', 100, '2020-12-09 18:59:57'),
(15, 'Test', 100, '2020-12-09 19:00:15');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `leaderboard`
--
ALTER TABLE `leaderboard`
  ADD PRIMARY KEY (`id`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `leaderboard`
--
ALTER TABLE `leaderboard`
  MODIFY `id` int(10) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=16;
