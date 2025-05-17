CREATE DATABASE  IF NOT EXISTS `hotel_reservation` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `hotel_reservation`;
-- MySQL dump 10.13  Distrib 8.0.42, for Win64 (x86_64)
--
-- Host: localhost    Database: hotel_reservation
-- ------------------------------------------------------
-- Server version	9.3.0

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
-- Table structure for table `admin`
--

DROP TABLE IF EXISTS `admin`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `admin` (
  `id` int NOT NULL AUTO_INCREMENT,
  `username` varchar(50) NOT NULL,
  `password` varchar(255) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `admin`
--

LOCK TABLES `admin` WRITE;
/*!40000 ALTER TABLE `admin` DISABLE KEYS */;
INSERT INTO `admin` VALUES (1,'admin','907039');
/*!40000 ALTER TABLE `admin` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `guests`
--

DROP TABLE IF EXISTS `guests`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `guests` (
  `guest_id` int NOT NULL AUTO_INCREMENT,
  `first_name` varchar(100) DEFAULT NULL,
  `last_name` varchar(100) DEFAULT NULL,
  `email` varchar(255) DEFAULT NULL,
  `phone` varchar(20) DEFAULT NULL,
  `address` text,
  `created_at` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`guest_id`),
  UNIQUE KEY `email` (`email`)
) ENGINE=InnoDB AUTO_INCREMENT=40 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `guests`
--

LOCK TABLES `guests` WRITE;
/*!40000 ALTER TABLE `guests` DISABLE KEYS */;
INSERT INTO `guests` VALUES (1,'John Dave B.','Bañas','johndave.bañas@example.com','9123456780','San Isidro, City','2025-02-19 14:17:48'),(2,'Markhy','Bañas','markhy.bañas@example.com','9234567891','San Isidro, City','2025-02-19 14:17:48'),(3,'Annielyn','Bermas','annielyn.bermas@example.com','9345678902','San Pedro, City','2025-02-19 14:17:48'),(4,'Nikki Shane','Bibat','nikkishane.bibat@example.com','9456789013','Santa Maria, City','2025-02-19 14:17:48'),(5,'Issy','Biqeo','issy.biqeo@example.com','9567890124','San Juan, City','2025-02-19 14:17:48'),(6,'Jelon','Basmayor','jelon.basmayor@example.com','9678901235','San Fernando, City','2025-02-19 14:17:48'),(7,'Ronnie','Caro','ronnie.caro@example.com','9789012346','San Andres, City','2025-02-19 14:17:48'),(8,'Kyla','Benavente','kyla.benavente@example.com','9890123457','San Lorenzo, City','2025-02-19 14:17:48'),(9,'Macking B.','Bañas','macking.bañas@example.com','9901234568','San Gabriel, City','2025-02-19 14:17:48'),(10,'Emil B.','Bañas','emil.bañas@example.com','9012345679','San Antonio, City','2025-02-19 14:17:48'),(11,'Juliet','Benosa','juliet.benosa@example.com','9112345671','Manila, Philippines','2025-02-19 14:17:48'),(12,'Macky','Bongat','macky.bongat@example.com','9223456782','Quezon City, Philippines','2025-02-19 14:17:48'),(13,'Charlien','Beruela','charlien.beruela@example.com','9334567893','Cebu City, Philippines','2025-02-19 14:17:48'),(14,'Angeline','Barlizo','angeline.barlizo@example.com','9445678904','Davao City, Philippines','2025-02-19 14:17:48'),(15,'Gloria','Bañas','gloria.bañas@example.com','9556789015','Baguio City, Philippines','2025-02-19 14:17:48'),(16,'Zaldy','Bañas','zaldy.bañas@example.com','9667890126','Pasig City, Philippines','2025-02-19 14:17:48'),(17,'Peter','Parker','peter.parker@example.com','9778901237','Taguig, Philippines','2025-02-19 14:17:48'),(18,'Tony','Stark','tony.stark@example.com','9889012348','Las Piñas, Philippines','2025-02-19 14:17:48'),(19,'Clint','Barton','clint.barton@example.com','9990123459','Marikina, Philippines','2025-02-19 14:17:48'),(20,'Bruce','Banner','bruce.banner@example.com','9001234560','Manila, Philippines','2025-02-19 14:17:48'),(21,'Meh','Bongat','mehbongat@example.com','09123456789','123 Main St','2025-02-20 10:35:56'),(22,'Jay','Bomales','jay@email.com','09203903949','Tabaco, City','2025-02-21 12:42:09'),(28,'Test','User','test@example.com','1234567890','Test Address','2025-03-22 14:03:41'),(30,'Kate','Manlangit','gandakate@gmail.com','09102939023','Tabaco, City','2025-05-11 09:33:30'),(32,'Kim','Gil','kim@gmail.com','09120939093','Tabaco, City','2025-05-11 16:47:28'),(33,'Kara','Bendana','kara@gmail.com','09102930902','Tabco, City','2025-05-11 16:58:58'),(34,'Yelena','Balova','yelenabelova@gmail.com','09104979045','Tabaco, City','2025-05-13 12:28:02'),(36,'Yelena','Balova','yelenavaloba@gmail.com','09109029033','Tabaco, City','2025-05-13 12:42:56'),(37,'Eva V.','Gosline','evagosline09@gmail.com','09102903803','Panal, Tabaco, City','2025-05-13 14:00:21'),(38,'Macking','Banas','mackingsasdasdasd@gmail.com','0910290390','Tabaco, City','2025-05-13 14:07:16');
/*!40000 ALTER TABLE `guests` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `payments`
--

DROP TABLE IF EXISTS `payments`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `payments` (
  `payment_id` int NOT NULL AUTO_INCREMENT,
  `reservation_id` int NOT NULL,
  `amount` decimal(10,2) NOT NULL,
  `payment_date` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  `payment_method` enum('Cash','Credit Card','Debit Card','Online') NOT NULL,
  `status` enum('Pending','Completed','Failed') DEFAULT 'Pending',
  PRIMARY KEY (`payment_id`),
  KEY `reservation_id` (`reservation_id`),
  CONSTRAINT `payments_ibfk_1` FOREIGN KEY (`reservation_id`) REFERENCES `reservations` (`reservation_id`)
) ENGINE=InnoDB AUTO_INCREMENT=28 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `payments`
--

LOCK TABLES `payments` WRITE;
/*!40000 ALTER TABLE `payments` DISABLE KEYS */;
INSERT INTO `payments` VALUES (2,2,1500.00,'2025-02-19 14:17:48','Cash','Pending'),(3,3,3000.00,'2025-02-19 14:17:48','Debit Card','Completed'),(4,4,4000.00,'2025-02-19 14:17:48','Online','Completed'),(5,5,1800.00,'2025-02-19 14:17:48','Credit Card','Completed'),(6,6,2500.00,'2025-05-13 12:39:16','Cash','Completed'),(7,7,3500.00,'2025-02-19 14:17:48','Debit Card','Failed'),(8,8,2200.00,'2025-02-19 14:17:48','Online','Completed'),(9,9,2800.00,'2025-02-19 14:17:48','Credit Card','Completed'),(10,10,3200.00,'2025-02-19 14:17:48','Cash','Completed'),(11,11,2700.00,'2025-02-19 14:17:48','Debit Card','Pending'),(12,12,3100.00,'2025-02-19 14:17:48','Online','Completed'),(13,13,2900.00,'2025-02-19 14:17:48','Credit Card','Completed'),(14,14,3400.00,'2025-02-19 14:17:48','Cash','Failed'),(15,15,2000.00,'2025-02-19 14:17:48','Online','Completed'),(16,16,3600.00,'2025-05-13 14:03:51','Debit Card','Completed'),(17,17,3300.00,'2025-02-19 14:17:48','Credit Card','Completed'),(18,18,3700.00,'2025-02-19 14:17:48','Cash','Completed'),(19,19,3100.00,'2025-02-19 14:17:48','Debit Card','Pending'),(20,20,3900.00,'2025-02-19 14:17:48','Online','Completed'),(21,508,100.00,'2025-05-11 09:33:30','Cash','Completed'),(22,509,1000.00,'2025-05-11 16:47:28','Debit Card','Completed'),(23,510,2000.00,'2025-05-11 16:59:12','Cash','Completed'),(24,511,1000.00,'2025-05-13 12:29:20','Cash','Completed'),(25,512,900.00,'2025-05-13 12:42:57','Cash','Completed'),(26,513,1000.00,'2025-05-13 14:01:20','Cash','Completed'),(27,514,1000.00,'2025-05-13 14:07:16','Credit Card','Completed');
/*!40000 ALTER TABLE `payments` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `reservations`
--

DROP TABLE IF EXISTS `reservations`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `reservations` (
  `reservation_id` int NOT NULL AUTO_INCREMENT,
  `guest_id` int NOT NULL,
  `room_id` int NOT NULL,
  `check_in_date` date NOT NULL,
  `check_out_date` date NOT NULL,
  `status` enum('Pending','Confirmed','Checked In','Checked Out','Cancelled') DEFAULT 'Pending',
  `created_at` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`reservation_id`),
  KEY `guest_id` (`guest_id`),
  KEY `room_id` (`room_id`),
  CONSTRAINT `reservations_ibfk_1` FOREIGN KEY (`guest_id`) REFERENCES `guests` (`guest_id`),
  CONSTRAINT `reservations_ibfk_2` FOREIGN KEY (`room_id`) REFERENCES `rooms` (`room_id`)
) ENGINE=InnoDB AUTO_INCREMENT=515 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `reservations`
--

LOCK TABLES `reservations` WRITE;
/*!40000 ALTER TABLE `reservations` DISABLE KEYS */;
INSERT INTO `reservations` VALUES (1,1,1,'2025-03-01','2025-03-06','Checked Out','2025-02-19 14:17:48'),(2,2,2,'2025-03-02','2025-03-04','Checked Out','2025-02-19 14:17:48'),(3,3,3,'2025-03-03','2025-03-05','Checked Out','2025-02-19 14:17:48'),(4,4,4,'2025-03-04','2025-03-20','Checked Out','2025-02-19 14:17:48'),(5,5,5,'2025-03-05','2025-03-07','Cancelled','2025-02-19 14:17:48'),(6,6,6,'2025-03-06','2025-03-08','Checked Out','2025-02-19 14:17:48'),(7,7,7,'2025-03-07','2025-03-09','Cancelled','2025-02-19 14:17:48'),(8,8,8,'2025-03-08','2025-03-10','Checked Out','2025-02-19 14:17:48'),(9,9,9,'2025-03-09','2025-03-11','Checked Out','2025-02-19 14:17:48'),(10,10,10,'2025-03-10','2025-03-12','Checked Out','2025-02-19 14:17:48'),(11,11,11,'2025-03-11','2025-03-13','Pending','2025-02-19 14:17:48'),(12,12,12,'2025-03-12','2025-03-14','Checked Out','2025-02-19 14:17:48'),(13,13,13,'2025-03-13','2025-03-15','Checked Out','2025-02-19 14:17:48'),(14,14,14,'2025-03-14','2025-03-16','Cancelled','2025-02-19 14:17:48'),(15,15,15,'2025-03-15','2025-03-17','Checked Out','2025-02-19 14:17:48'),(16,16,16,'2025-03-16','2025-03-18','Checked Out','2025-02-19 14:17:48'),(17,17,17,'2025-03-17','2025-03-19','Checked Out','2025-02-19 14:17:48'),(18,18,18,'2025-03-18','2025-03-20','Checked Out','2025-02-19 14:17:48'),(19,19,19,'2025-03-19','2025-03-21','Checked Out','2025-02-19 14:17:48'),(20,20,20,'2025-03-20','2025-03-22','Checked Out','2025-02-19 14:17:48'),(22,21,26,'2025-01-01','2025-01-05','Checked Out','2025-03-22 14:06:35'),(23,22,26,'2025-01-01','2025-01-05','Checked Out','2025-03-22 14:08:24'),(25,1,1,'2025-03-01','2025-03-05','Cancelled','2025-03-22 14:48:00'),(26,1,1,'2025-03-01','2025-03-05','Cancelled','2025-03-22 14:49:36'),(29,1,1,'2025-03-01','2025-03-05','Checked Out','2025-03-23 11:09:30'),(30,1,1,'2025-03-22','2025-03-24','Checked Out','2025-03-23 11:36:11'),(31,1,1,'2025-04-01','2025-04-05','Confirmed','2025-03-23 11:41:43'),(32,1,1,'2025-04-01','2025-04-05','Pending','2025-03-23 11:41:44'),(508,30,5,'2025-05-11','2025-05-12','Confirmed','2025-05-11 09:33:30'),(509,32,1,'2025-05-11','2025-05-17','Confirmed','2025-05-11 16:47:28'),(510,33,4,'2025-05-11','2025-05-16','Confirmed','2025-05-11 16:58:58'),(511,34,20,'2025-05-13','2025-05-16','Confirmed','2025-05-13 12:28:02'),(512,36,8,'2025-05-13','2025-05-30','Confirmed','2025-05-13 12:42:56'),(513,37,12,'2025-05-13','2025-05-16','Confirmed','2025-05-13 14:00:21'),(514,38,14,'2025-05-13','2025-05-14','Confirmed','2025-05-13 14:07:16');
/*!40000 ALTER TABLE `reservations` ENABLE KEYS */;
UNLOCK TABLES;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
/*!50003 CREATE*/ /*!50017 DEFINER=`root`@`localhost`*/ /*!50003 TRIGGER `ValidateReservationDates` BEFORE INSERT ON `reservations` FOR EACH ROW BEGIN
    IF NEW.check_out_date <= NEW.check_in_date THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'Check-out date must be after check-in date';
    END IF;
END */;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
/*!50003 CREATE*/ /*!50017 DEFINER=`root`@`localhost`*/ /*!50003 TRIGGER `AfterReservationInsert` AFTER INSERT ON `reservations` FOR EACH ROW BEGIN
    UPDATE Rooms
    SET status = 'Booked'
    WHERE room_id = NEW.room_id;
END */;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
/*!50003 CREATE*/ /*!50017 DEFINER=`root`@`localhost`*/ /*!50003 TRIGGER `PreventCheckedInModification` BEFORE UPDATE ON `reservations` FOR EACH ROW BEGIN
    IF OLD.status = 'Checked In' AND (NEW.check_in_date <> OLD.check_in_date OR NEW.check_out_date <> OLD.check_out_date) THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'Cannot modify dates for checked-in reservations';
    END IF;
END */;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
/*!50003 CREATE*/ /*!50017 DEFINER=`root`@`localhost`*/ /*!50003 TRIGGER `AfterReservationUpdate` AFTER UPDATE ON `reservations` FOR EACH ROW BEGIN
    IF NEW.status <> OLD.status THEN
        INSERT INTO Room_Booking_Logs(room_id, reservation_id, action)
        VALUES (NEW.room_id, NEW.reservation_id, CONCAT('Status changed to ', NEW.status));
    END IF;
END */;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
/*!50003 CREATE*/ /*!50017 DEFINER=`root`@`localhost`*/ /*!50003 TRIGGER `PreventActiveReservationDeletion` BEFORE DELETE ON `reservations` FOR EACH ROW BEGIN
    IF OLD.status IN ('Checked In', 'Confirmed') THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'Cannot delete active reservations';
    END IF;
END */;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
/*!50003 CREATE*/ /*!50017 DEFINER=`root`@`localhost`*/ /*!50003 TRIGGER `AfterReservationDelete` AFTER DELETE ON `reservations` FOR EACH ROW BEGIN
    INSERT INTO Room_Booking_Logs(room_id, reservation_id, action)
    VALUES (OLD.room_id, OLD.reservation_id, 'Reservation deleted');
    
    UPDATE Rooms
    SET status = 'Available'
    WHERE room_id = OLD.room_id;
END */;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;

--
-- Table structure for table `reviews`
--

DROP TABLE IF EXISTS `reviews`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `reviews` (
  `review_id` int NOT NULL AUTO_INCREMENT,
  `guest_id` int NOT NULL,
  `room_id` int NOT NULL,
  `rating` int DEFAULT NULL,
  `comment` text,
  `created_at` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`review_id`),
  KEY `guest_id` (`guest_id`),
  KEY `room_id` (`room_id`),
  CONSTRAINT `reviews_ibfk_1` FOREIGN KEY (`guest_id`) REFERENCES `guests` (`guest_id`),
  CONSTRAINT `reviews_ibfk_2` FOREIGN KEY (`room_id`) REFERENCES `rooms` (`room_id`),
  CONSTRAINT `reviews_chk_1` CHECK ((`rating` between 1 and 5))
) ENGINE=InnoDB AUTO_INCREMENT=21 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `reviews`
--

LOCK TABLES `reviews` WRITE;
/*!40000 ALTER TABLE `reviews` DISABLE KEYS */;
INSERT INTO `reviews` VALUES (1,1,1,5,'Great stay, clean room!','2025-03-04 02:00:00'),(2,2,2,4,'Comfortable bed but slow service.','2025-03-05 03:00:00'),(3,3,3,3,'Room was under maintenance upon arrival.','2025-03-06 04:00:00'),(4,4,4,5,'Amazing experience!','2025-03-07 05:00:00'),(5,5,5,4,'Nice hotel, good staff.','2025-03-08 06:00:00'),(6,6,6,3,'Average service, room was clean.','2025-03-09 07:00:00'),(7,7,7,5,'Best stay ever!','2025-03-10 08:00:00'),(8,8,8,4,'Overall great experience.','2025-03-11 09:00:00'),(9,9,9,2,'Room was too small for the price.','2025-03-12 10:00:00'),(10,10,10,5,'Luxurious stay, loved it!','2025-03-13 11:00:00'),(11,11,11,5,'Fantastic service and room!','2025-03-14 02:00:00'),(12,12,12,4,'Good value, but could be cleaner.','2025-03-15 03:00:00'),(13,13,13,3,'Not bad, but a bit noisy.','2025-03-16 04:00:00'),(14,14,14,5,'Really enjoyed my stay!','2025-03-17 05:00:00'),(15,15,15,4,'Comfortable room, friendly staff.','2025-03-18 06:00:00'),(16,16,16,3,'Average experience overall.','2025-03-19 07:00:00'),(17,17,17,5,'Exceeded my expectations!','2025-03-20 08:00:00'),(18,18,18,4,'Good amenities and service.','2025-03-21 09:00:00'),(19,19,19,2,'Room felt cramped.','2025-03-22 10:00:00'),(20,20,20,5,'Absolutely perfect stay!','2025-03-23 11:00:00');
/*!40000 ALTER TABLE `reviews` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `room_booking_logs`
--

DROP TABLE IF EXISTS `room_booking_logs`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `room_booking_logs` (
  `log_id` int NOT NULL AUTO_INCREMENT,
  `room_id` int NOT NULL,
  `reservation_id` int NOT NULL,
  `action` varchar(50) DEFAULT NULL,
  `timestamp` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`log_id`),
  KEY `room_id` (`room_id`),
  KEY `reservation_id` (`reservation_id`),
  CONSTRAINT `room_booking_logs_ibfk_1` FOREIGN KEY (`room_id`) REFERENCES `rooms` (`room_id`),
  CONSTRAINT `room_booking_logs_ibfk_2` FOREIGN KEY (`reservation_id`) REFERENCES `reservations` (`reservation_id`)
) ENGINE=InnoDB AUTO_INCREMENT=64 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `room_booking_logs`
--

LOCK TABLES `room_booking_logs` WRITE;
/*!40000 ALTER TABLE `room_booking_logs` DISABLE KEYS */;
INSERT INTO `room_booking_logs` VALUES (1,1,1,'Booked','2025-02-28 05:00:00'),(2,2,2,'Booked','2025-03-01 06:00:00'),(3,3,3,'Checked In','2025-03-02 07:00:00'),(4,4,4,'Checked In','2025-03-03 08:00:00'),(5,5,5,'Booked','2025-03-04 09:00:00'),(6,6,6,'Checked In','2025-03-05 10:00:00'),(7,7,7,'Cancelled','2025-03-06 11:00:00'),(8,8,8,'Booked','2025-03-07 12:00:00'),(9,9,9,'Checked Out','2025-03-08 13:00:00'),(10,10,10,'Booked','2025-03-09 14:00:00'),(11,11,11,'Pending','2025-03-10 05:00:00'),(12,12,12,'Booked','2025-03-11 06:00:00'),(13,13,13,'Checked In','2025-03-12 07:00:00'),(14,14,14,'Cancelled','2025-03-13 08:00:00'),(15,15,15,'Confirmed','2025-03-14 09:00:00'),(16,16,16,'Checked In','2025-03-15 10:00:00'),(17,17,17,'Checked Out','2025-03-16 11:00:00'),(18,18,18,'Booked','2025-03-17 12:00:00'),(19,19,19,'Checked In','2025-03-18 13:00:00'),(20,20,20,'Booked','2025-03-19 14:00:00'),(21,1,1,'Status changed to Checked In','2025-03-22 14:42:31'),(22,1,25,'Status changed to Checked Out','2025-03-23 11:14:58'),(23,1,26,'Status changed to Checked Out','2025-03-23 11:14:58'),(24,1,29,'Status changed to Checked Out','2025-03-23 11:14:58'),(25,1,1,'Status changed to Cancelled','2025-03-23 11:15:25'),(26,1,25,'Status changed to Cancelled','2025-03-23 11:15:25'),(27,1,26,'Status changed to Cancelled','2025-03-23 11:15:25'),(28,2,2,'Status changed to Checked In','2025-03-23 11:19:14'),(29,1,1,'Status changed to Checked In','2025-03-23 11:44:14'),(30,1,1,'Status changed to Checked Out','2025-04-04 12:05:40'),(31,2,2,'Status changed to Checked Out','2025-04-04 12:05:40'),(32,3,3,'Status changed to Checked Out','2025-04-04 12:05:40'),(33,4,4,'Status changed to Checked Out','2025-04-04 12:05:40'),(34,6,6,'Status changed to Checked Out','2025-04-04 12:05:40'),(35,8,8,'Status changed to Checked Out','2025-04-04 12:05:40'),(36,10,10,'Status changed to Checked Out','2025-04-04 12:05:40'),(37,12,12,'Status changed to Checked Out','2025-04-04 12:05:40'),(38,13,13,'Status changed to Checked Out','2025-04-04 12:05:40'),(39,15,15,'Status changed to Checked Out','2025-04-04 12:05:40'),(40,16,16,'Status changed to Checked Out','2025-04-04 12:05:40'),(41,18,18,'Status changed to Checked Out','2025-04-04 12:05:40'),(42,19,19,'Status changed to Checked Out','2025-04-04 12:05:40'),(43,20,20,'Status changed to Checked Out','2025-04-04 12:05:40'),(44,26,22,'Status changed to Checked Out','2025-04-04 12:05:40'),(45,26,23,'Status changed to Checked Out','2025-04-04 12:05:40'),(46,1,30,'Status changed to Checked In','2025-04-04 12:38:18'),(47,1,30,'Status changed to Checked Out','2025-04-04 12:39:08'),(48,1,509,'Pending','2025-05-11 16:47:28'),(49,1,509,'Status changed to Confirmed','2025-05-11 16:56:55'),(50,1,509,'Confirmed','2025-05-11 16:56:55'),(51,4,510,'Confirmed','2025-05-11 16:58:58'),(52,1,31,'Status changed to Confirmed','2025-05-11 22:16:26'),(53,1,31,'Confirmed','2025-05-11 22:16:26'),(54,20,511,'Pending','2025-05-13 12:28:02'),(55,20,511,'Status changed to Confirmed','2025-05-13 12:29:14'),(56,20,511,'Confirmed','2025-05-13 12:29:14'),(57,8,512,'Pending','2025-05-13 12:42:56'),(58,8,512,'Status changed to Confirmed','2025-05-13 12:43:46'),(59,8,512,'Confirmed','2025-05-13 12:43:46'),(60,12,513,'Pending','2025-05-13 14:00:21'),(61,12,513,'Status changed to Confirmed','2025-05-13 14:01:09'),(62,12,513,'Confirmed','2025-05-13 14:01:09'),(63,14,514,'Confirmed','2025-05-13 14:07:16');
/*!40000 ALTER TABLE `room_booking_logs` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `room_types`
--

DROP TABLE IF EXISTS `room_types`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `room_types` (
  `type_id` int NOT NULL AUTO_INCREMENT,
  `type_name` varchar(50) NOT NULL,
  `price` decimal(10,2) NOT NULL,
  PRIMARY KEY (`type_id`),
  UNIQUE KEY `type_name` (`type_name`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `room_types`
--

LOCK TABLES `room_types` WRITE;
/*!40000 ALTER TABLE `room_types` DISABLE KEYS */;
INSERT INTO `room_types` VALUES (1,'Single',1412.88),(2,'Double',2119.32),(3,'Suite',3532.21),(4,'Test Type',100.00);
/*!40000 ALTER TABLE `room_types` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `rooms`
--

DROP TABLE IF EXISTS `rooms`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `rooms` (
  `room_id` int NOT NULL AUTO_INCREMENT,
  `floor_id` int NOT NULL,
  `room_number` varchar(10) NOT NULL,
  `room_type` int NOT NULL,
  `status` enum('Available','Booked','Under Maintenance') NOT NULL DEFAULT 'Available',
  PRIMARY KEY (`room_id`),
  UNIQUE KEY `room_number` (`room_number`),
  KEY `room_type` (`room_type`),
  CONSTRAINT `rooms_ibfk_1` FOREIGN KEY (`room_type`) REFERENCES `room_types` (`type_id`)
) ENGINE=InnoDB AUTO_INCREMENT=29 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `rooms`
--

LOCK TABLES `rooms` WRITE;
/*!40000 ALTER TABLE `rooms` DISABLE KEYS */;
INSERT INTO `rooms` VALUES (1,1,'101',1,'Booked'),(2,1,'102',2,'Booked'),(3,1,'103',3,'Available'),(4,1,'104',1,'Booked'),(5,1,'105',2,'Booked'),(6,2,'201',1,'Booked'),(7,2,'202',2,'Available'),(8,2,'203',3,'Booked'),(9,2,'204',1,'Available'),(10,2,'205',2,'Booked'),(11,3,'301',1,'Available'),(12,3,'302',3,'Booked'),(13,3,'303',2,'Available'),(14,3,'304',1,'Booked'),(15,3,'305',3,'Available'),(16,4,'401',1,'Available'),(17,4,'402',2,'Booked'),(18,4,'403',3,'Available'),(19,4,'404',1,'Available'),(20,4,'405',2,'Booked'),(21,5,'501',1,'Booked'),(22,5,'502',2,'Available'),(23,5,'503',3,'Booked'),(24,5,'504',1,'Available'),(25,5,'505',2,'Booked'),(26,1,'506',4,'Booked'),(27,1,'507',1,'Available'),(28,1,'508',2,'Available');
/*!40000 ALTER TABLE `rooms` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Temporary view structure for view `viewguestpending`
--

DROP TABLE IF EXISTS `viewguestpending`;
/*!50001 DROP VIEW IF EXISTS `viewguestpending`*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `viewguestpending` AS SELECT 
 1 AS `guest_id`,
 1 AS `first_name`,
 1 AS `last_name`,
 1 AS `email`,
 1 AS `phone`,
 1 AS `address`*/;
SET character_set_client = @saved_cs_client;

--
-- Temporary view structure for view `viewguestreservations`
--

DROP TABLE IF EXISTS `viewguestreservations`;
/*!50001 DROP VIEW IF EXISTS `viewguestreservations`*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `viewguestreservations` AS SELECT 
 1 AS `guest_id`,
 1 AS `guest_name`,
 1 AS `reservation_id`,
 1 AS `room_id`,
 1 AS `check_in_date`,
 1 AS `check_out_date`,
 1 AS `reservation_status`*/;
SET character_set_client = @saved_cs_client;

--
-- Temporary view structure for view `viewroomreservationcount`
--

DROP TABLE IF EXISTS `viewroomreservationcount`;
/*!50001 DROP VIEW IF EXISTS `viewroomreservationcount`*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `viewroomreservationcount` AS SELECT 
 1 AS `room_id`,
 1 AS `total_reservations`*/;
SET character_set_client = @saved_cs_client;

--
-- Temporary view structure for view `viewtotalpayments`
--

DROP TABLE IF EXISTS `viewtotalpayments`;
/*!50001 DROP VIEW IF EXISTS `viewtotalpayments`*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `viewtotalpayments` AS SELECT 
 1 AS `guest_id`,
 1 AS `guest_name`,
 1 AS `total_paid`*/;
SET character_set_client = @saved_cs_client;

--
-- Dumping events for database 'hotel_reservation'
--

--
-- Dumping routines for database 'hotel_reservation'
--
/*!50003 DROP PROCEDURE IF EXISTS `InsertNewGuest` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `InsertNewGuest`(
  IN p_first_name VARCHAR(100),
  IN p_last_name  VARCHAR(100),
  IN p_email      VARCHAR(255),
  IN p_phone      VARCHAR(20),
  IN p_address    TEXT
)
BEGIN
  INSERT INTO Guests (first_name, last_name, email, phone, address)
  VALUES (p_first_name, p_last_name, p_email, p_phone, p_address);
  -- The SELECT LAST_INSERT_ID(); will be handled in the C# code right after calling this.
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;

--
-- Final view structure for view `viewguestpending`
--

/*!50001 DROP VIEW IF EXISTS `viewguestpending`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb3_general_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `viewguestpending` AS select 1 AS `guest_id`,1 AS `first_name`,1 AS `last_name`,1 AS `email`,1 AS `phone`,1 AS `address` */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `viewguestreservations`
--

/*!50001 DROP VIEW IF EXISTS `viewguestreservations`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb3_general_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `viewguestreservations` AS select 1 AS `guest_id`,1 AS `guest_name`,1 AS `reservation_id`,1 AS `room_id`,1 AS `check_in_date`,1 AS `check_out_date`,1 AS `reservation_status` */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `viewroomreservationcount`
--

/*!50001 DROP VIEW IF EXISTS `viewroomreservationcount`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb3_general_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `viewroomreservationcount` AS select 1 AS `room_id`,1 AS `total_reservations` */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `viewtotalpayments`
--

/*!50001 DROP VIEW IF EXISTS `viewtotalpayments`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb3_general_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `viewtotalpayments` AS select 1 AS `guest_id`,1 AS `guest_name`,1 AS `total_paid` */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2025-05-17 16:36:00
