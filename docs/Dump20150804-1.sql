CREATE DATABASE  IF NOT EXISTS `stocknet` /*!40100 DEFAULT CHARACTER SET utf8 */;
USE `stocknet`;
-- MySQL dump 10.13  Distrib 5.6.13, for Win32 (x86)
--
-- Host: localhost    Database: stocknet
-- ------------------------------------------------------
-- Server version	5.7.3-m13-log

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `st_cat`
--

DROP TABLE IF EXISTS `st_cat`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `st_cat` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(45) DEFAULT NULL,
  `PId` int(11) DEFAULT '0',
  `Path` varchar(150) DEFAULT NULL,
  `Status` int(11) DEFAULT '0',
  PRIMARY KEY (`Id`),
  UNIQUE KEY `Name_UNIQUE` (`Name`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `st_cat`
--

LOCK TABLES `st_cat` WRITE;
/*!40000 ALTER TABLE `st_cat` DISABLE KEYS */;
INSERT INTO `st_cat` VALUES (1,'freg',0,NULL,NULL),(2,'ssss',1,NULL,NULL);
/*!40000 ALTER TABLE `st_cat` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `st_goods`
--

DROP TABLE IF EXISTS `st_goods`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `st_goods` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(150) DEFAULT NULL,
  `CatId` int(11) DEFAULT '0',
  `MemberId` int(11) DEFAULT NULL,
  `MainPic` varchar(550) DEFAULT NULL,
  `Price` double DEFAULT NULL,
  `PriceDetail` varchar(150) DEFAULT NULL,
  `Qty` int(11) DEFAULT NULL,
  `QtyDetail` varchar(150) DEFAULT NULL,
  `SerialNum` varchar(45) DEFAULT NULL,
  `Details` text,
  `AddTime` datetime DEFAULT CURRENT_TIMESTAMP,
  `ModifyTime` datetime DEFAULT CURRENT_TIMESTAMP,
  `AddUser` int(11) DEFAULT NULL,
  `OverDate` bit(1) DEFAULT b'0',
  `Status` int(11) DEFAULT '0',
  `Recommended` bit(1) DEFAULT b'0',
  `StoreId` int(11) DEFAULT NULL,
  `ViewCount` bigint(20) DEFAULT '0',
  `ContactViewCount` int(11) DEFAULT '0',
  PRIMARY KEY (`Id`),
  UNIQUE KEY `Name_UNIQUE` (`Name`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `st_goods`
--

LOCK TABLES `st_goods` WRITE;
/*!40000 ALTER TABLE `st_goods` DISABLE KEYS */;
/*!40000 ALTER TABLE `st_goods` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `st_log`
--

DROP TABLE IF EXISTS `st_log`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `st_log` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `LogType` int(11) DEFAULT '0',
  `LogDetail` varchar(1024) DEFAULT NULL,
  `RelateId` int(11) DEFAULT NULL,
  `AddTime` datetime DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `st_log`
--

LOCK TABLES `st_log` WRITE;
/*!40000 ALTER TABLE `st_log` DISABLE KEYS */;
/*!40000 ALTER TABLE `st_log` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `st_member`
--

DROP TABLE IF EXISTS `st_member`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `st_member` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `UserId` int(11) DEFAULT NULL,
  `Level` varchar(45) DEFAULT NULL,
  `RealName` varchar(45) DEFAULT NULL,
  `Mobile` varchar(45) DEFAULT NULL,
  `Tel` varchar(45) DEFAULT NULL,
  `Email` varchar(150) DEFAULT NULL,
  `QQ` varchar(45) DEFAULT NULL,
  `Wechat` varchar(150) DEFAULT NULL,
  `Alipay` varchar(150) DEFAULT NULL,
  `Addr` varchar(150) DEFAULT NULL,
  `Remark` varchar(150) DEFAULT NULL,
  `AddTime` datetime DEFAULT CURRENT_TIMESTAMP,
  `AddUser` int(11) DEFAULT NULL,
  `HadVerify` bit(1) DEFAULT b'0',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `st_member`
--

LOCK TABLES `st_member` WRITE;
/*!40000 ALTER TABLE `st_member` DISABLE KEYS */;
/*!40000 ALTER TABLE `st_member` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `st_pic`
--

DROP TABLE IF EXISTS `st_pic`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `st_pic` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `PicType` int(11) DEFAULT '0',
  `RelateId` int(11) DEFAULT NULL,
  `PicUrl` varchar(550) DEFAULT NULL,
  `AddTime` datetime DEFAULT CURRENT_TIMESTAMP,
  `Remark` varchar(150) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `st_pic`
--

LOCK TABLES `st_pic` WRITE;
/*!40000 ALTER TABLE `st_pic` DISABLE KEYS */;
/*!40000 ALTER TABLE `st_pic` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `st_role`
--

DROP TABLE IF EXISTS `st_role`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `st_role` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(45) DEFAULT NULL,
  `Perms` varchar(550) DEFAULT NULL,
  `AddTime` datetime DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `Name_UNIQUE` (`Name`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `st_role`
--

LOCK TABLES `st_role` WRITE;
/*!40000 ALTER TABLE `st_role` DISABLE KEYS */;
INSERT INTO `st_role` VALUES (1,'wt','Dash,Owner,Cat,Setting',NULL);
/*!40000 ALTER TABLE `st_role` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `st_setting`
--

DROP TABLE IF EXISTS `st_setting`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `st_setting` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(150) DEFAULT NULL,
  `Val` varchar(1024) DEFAULT NULL,
  `AddTime` datetime DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `Name_UNIQUE` (`Name`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `st_setting`
--

LOCK TABLES `st_setting` WRITE;
/*!40000 ALTER TABLE `st_setting` DISABLE KEYS */;
/*!40000 ALTER TABLE `st_setting` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `st_store`
--

DROP TABLE IF EXISTS `st_store`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `st_store` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(45) DEFAULT NULL,
  `CatId` int(11) DEFAULT NULL,
  `OwnerId` int(11) DEFAULT NULL,
  `MainPic` varchar(550) DEFAULT NULL,
  `Tel` varchar(45) DEFAULT NULL,
  `Addr` varchar(150) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `st_store`
--

LOCK TABLES `st_store` WRITE;
/*!40000 ALTER TABLE `st_store` DISABLE KEYS */;
/*!40000 ALTER TABLE `st_store` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `st_user`
--

DROP TABLE IF EXISTS `st_user`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `st_user` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(45) DEFAULT NULL,
  `LoginName` varchar(45) DEFAULT NULL,
  `LoginPass` varchar(150) DEFAULT NULL,
  `AddTime` datetime DEFAULT CURRENT_TIMESTAMP,
  `RoleId` int(11) DEFAULT '0',
  `Sex` bit(1) DEFAULT NULL,
  `Status` int(11) DEFAULT '0',
  PRIMARY KEY (`Id`),
  UNIQUE KEY `LoginName_UNIQUE` (`LoginName`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `st_user`
--

LOCK TABLES `st_user` WRITE;
/*!40000 ALTER TABLE `st_user` DISABLE KEYS */;
INSERT INTO `st_user` VALUES (1,'Admin','admin','QL0AFWMIX8NRZTKeof9cXsvbvu8=','2015-08-04 07:57:07',-1,NULL,0);
/*!40000 ALTER TABLE `st_user` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2015-08-04 21:11:46
