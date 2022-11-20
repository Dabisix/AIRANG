-- MySQL dump 10.13  Distrib 8.0.28, for Win64 (x86_64)
--
-- Host: k7b305.p.ssafy.io    Database: b305
-- ------------------------------------------------------
-- Server version	8.0.31

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `auth`
--

DROP TABLE IF EXISTS `auth`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `auth` (
  `a_id` int NOT NULL AUTO_INCREMENT,
  `refresh_token` varchar(255) DEFAULT NULL,
  `u_id` bigint DEFAULT NULL,
  PRIMARY KEY (`a_id`),
  KEY `FKrko9bn8a660a85b45uts4m9ww` (`u_id`),
  CONSTRAINT `FKrko9bn8a660a85b45uts4m9ww` FOREIGN KEY (`u_id`) REFERENCES `user` (`u_id`)
) ENGINE=InnoDB AUTO_INCREMENT=217 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `book`
--

DROP TABLE IF EXISTS `book`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `book` (
  `b_id` bigint NOT NULL,
  `a_flag` bit(1) DEFAULT NULL,
  `author` varchar(255) DEFAULT NULL,
  `cnt` int DEFAULT NULL,
  `e_content` mediumtext,
  `k_content` mediumtext,
  `title` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`b_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `book_rec_list`
--

DROP TABLE IF EXISTS `book_rec_list`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `book_rec_list` (
  `book_b_id` bigint NOT NULL,
  `a_flag` bit(1) NOT NULL,
  `b_id` bigint DEFAULT NULL,
  `date` datetime(6) DEFAULT NULL,
  `title` varchar(255) DEFAULT NULL,
  KEY `FKlm0bue5r0kl6ojsl3ojxe8g17` (`book_b_id`),
  CONSTRAINT `FKlm0bue5r0kl6ojsl3ojxe8g17` FOREIGN KEY (`book_b_id`) REFERENCES `book` (`b_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `user`
--

DROP TABLE IF EXISTS `user`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `user` (
  `u_id` bigint NOT NULL AUTO_INCREMENT,
  `email` varchar(255) DEFAULT NULL,
  `name` varchar(255) DEFAULT NULL,
  `pw` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`u_id`),
  UNIQUE KEY `UK_ob8kqyqqgmefl0aco34akdtpe` (`email`)
) ENGINE=InnoDB AUTO_INCREMENT=16 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `user_log_list`
--

DROP TABLE IF EXISTS `user_log_list`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `user_log_list` (
  `user_u_id` bigint NOT NULL,
  `a_flag` bit(1) NOT NULL,
  `b_id` bigint DEFAULT NULL,
  `date` datetime(6) DEFAULT NULL,
  `title` varchar(255) DEFAULT NULL,
  KEY `FKh36coathqhbrfi57w8u0679k0` (`user_u_id`),
  CONSTRAINT `FKh36coathqhbrfi57w8u0679k0` FOREIGN KEY (`user_u_id`) REFERENCES `user` (`u_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `user_star_list`
--

DROP TABLE IF EXISTS `user_star_list`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `user_star_list` (
  `user_u_id` bigint NOT NULL,
  `a_flag` bit(1) NOT NULL,
  `b_id` bigint DEFAULT NULL,
  `date` datetime(6) DEFAULT NULL,
  `title` varchar(255) DEFAULT NULL,
  KEY `FKjg3eonkibel417ke08oqbula0` (`user_u_id`),
  CONSTRAINT `FKjg3eonkibel417ke08oqbula0` FOREIGN KEY (`user_u_id`) REFERENCES `user` (`u_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2022-11-20 14:17:02
