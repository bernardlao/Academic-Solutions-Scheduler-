-- MySQL dump 10.13  Distrib 5.7.9, for Win64 (x86_64)
--
-- Host: localhost    Database: db
-- ------------------------------------------------------
-- Server version	5.6.21-log

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
-- Table structure for table `tblcategory`
--

DROP TABLE IF EXISTS `tblcategory`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tblcategory` (
  `CategoryID` bigint(20) NOT NULL AUTO_INCREMENT,
  `CategoryDescription` varchar(200) NOT NULL,
  `CategoryCode` varchar(100) NOT NULL,
  PRIMARY KEY (`CategoryID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tblcategory`
--

LOCK TABLES `tblcategory` WRITE;
/*!40000 ALTER TABLE `tblcategory` DISABLE KEYS */;
/*!40000 ALTER TABLE `tblcategory` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tblclass`
--

DROP TABLE IF EXISTS `tblclass`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tblclass` (
  `ClassID` bigint(20) NOT NULL AUTO_INCREMENT,
  `CourseID` bigint(20) DEFAULT NULL,
  `MajorID` bigint(20) DEFAULT NULL,
  `YearLevel` int(11) NOT NULL,
  `Section` varchar(45) NOT NULL,
  `Semester` bit(1) NOT NULL,
  `SchoolYear` varchar(45) NOT NULL,
  `LogBy` bigint(20) NOT NULL,
  `LogDate` datetime NOT NULL,
  PRIMARY KEY (`ClassID`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tblclass`
--

LOCK TABLES `tblclass` WRITE;
/*!40000 ALTER TABLE `tblclass` DISABLE KEYS */;
INSERT INTO `tblclass` VALUES (1,1,NULL,1,'A','','2017-2018',-2,'2018-02-17 06:07:54'),(2,NULL,NULL,0,'IRR','','2017-2018',-2,'2018-02-19 03:57:01'),(3,1,NULL,1,'B','','2017-2018',-2,'2018-02-19 17:53:03');
/*!40000 ALTER TABLE `tblclass` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tblclassschedule`
--

DROP TABLE IF EXISTS `tblclassschedule`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tblclassschedule` (
  `ClassScheduleID` bigint(20) NOT NULL AUTO_INCREMENT,
  `ClassSubjectID` bigint(20) NOT NULL,
  `Days` varchar(45) NOT NULL,
  `FromTime` time NOT NULL,
  `ToTime` time NOT NULL,
  `Room` varchar(45) NOT NULL,
  PRIMARY KEY (`ClassScheduleID`)
) ENGINE=InnoDB AUTO_INCREMENT=14 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tblclassschedule`
--

LOCK TABLES `tblclassschedule` WRITE;
/*!40000 ALTER TABLE `tblclassschedule` DISABLE KEYS */;
INSERT INTO `tblclassschedule` VALUES (2,2,'1','09:02:00','14:02:00','100'),(3,1,'1','06:07:00','08:07:00','100'),(4,1,'1','08:07:00','10:07:00','101'),(5,3,'1','18:00:00','23:00:00','102'),(6,4,'1','07:30:00','10:30:00','TBA'),(7,4,'2','07:30:00','09:30:00','TBA'),(8,5,'1','10:30:00','13:30:00','TBA'),(9,5,'2','10:30:00','12:30:00','TBA'),(10,6,'4','10:30:00','14:30:00','TBA'),(11,7,'5','10:30:00','14:30:00','101'),(12,8,'6','10:30:00','12:30:00','101'),(13,9,'1','15:30:00','19:30:00','101');
/*!40000 ALTER TABLE `tblclassschedule` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tblclasssubjects`
--

DROP TABLE IF EXISTS `tblclasssubjects`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tblclasssubjects` (
  `ClassSubjectID` bigint(20) NOT NULL AUTO_INCREMENT,
  `ClassID` bigint(20) NOT NULL,
  `SubjectID` varchar(50) NOT NULL,
  `ProfessorID` bigint(20) NOT NULL,
  `MaxClassCount` int(11) NOT NULL,
  PRIMARY KEY (`ClassSubjectID`)
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tblclasssubjects`
--

LOCK TABLES `tblclasssubjects` WRITE;
/*!40000 ALTER TABLE `tblclasssubjects` DISABLE KEYS */;
INSERT INTO `tblclasssubjects` VALUES (1,1,'BFIL 1',1,40),(2,1,'CS1A3',1,40),(3,2,'CS1A3',1,40),(4,3,'IT1A1',5,40),(5,3,'MAT 105',3,40),(6,3,'ENG 1',3,40),(7,3,'BFIL 1',6,40),(8,3,'PE 1',1,40),(9,3,'MAT 109',4,40);
/*!40000 ALTER TABLE `tblclasssubjects` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tblcourse`
--

DROP TABLE IF EXISTS `tblcourse`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tblcourse` (
  `CourseID` bigint(20) NOT NULL AUTO_INCREMENT,
  `CourseDescription` varchar(200) NOT NULL,
  `CategoryID` bigint(20) NOT NULL,
  `NoOfYears` int(11) NOT NULL,
  `CourseCode` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`CourseID`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tblcourse`
--

LOCK TABLES `tblcourse` WRITE;
/*!40000 ALTER TABLE `tblcourse` DISABLE KEYS */;
INSERT INTO `tblcourse` VALUES (1,'Bachelor of Science in Computer Science',0,4,'BSCS'),(2,'Bachelor of Elementary Education',0,4,'BEED'),(3,'Bachelor of Secondary Education',0,4,'BSED'),(4,'Bachelor of Science in Business Administration',0,4,'BSBA');
/*!40000 ALTER TABLE `tblcourse` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tblmajor`
--

DROP TABLE IF EXISTS `tblmajor`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tblmajor` (
  `MajorID` varchar(20) NOT NULL,
  `MajorDescription` varchar(30) NOT NULL,
  `CourseID` varchar(20) NOT NULL,
  `MajorCode` varchar(45) NOT NULL,
  PRIMARY KEY (`MajorID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tblmajor`
--

LOCK TABLES `tblmajor` WRITE;
/*!40000 ALTER TABLE `tblmajor` DISABLE KEYS */;
INSERT INTO `tblmajor` VALUES ('1','General Education','2','GEN-ED'),('2','Mathematics','3','MATH'),('3','English','3','ENGLISH'),('4','Marketing','4','MKTG'),('5','Human Resources','4','HR');
/*!40000 ALTER TABLE `tblmajor` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tblprofessor`
--

DROP TABLE IF EXISTS `tblprofessor`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tblprofessor` (
  `ProfessorID` bigint(20) NOT NULL AUTO_INCREMENT,
  `LastName` varchar(100) NOT NULL,
  `FirstName` varchar(100) NOT NULL,
  `MiddleName` varchar(100) DEFAULT NULL,
  `BirthDate` datetime NOT NULL,
  `Gender` bit(1) NOT NULL,
  `ContactNumber` varchar(50) NOT NULL,
  `ContactPerson` varchar(150) NOT NULL,
  `ContactPersonNumber` varchar(30) NOT NULL,
  `UnderGrad` varchar(200) NOT NULL,
  `GraduateDegree` varchar(200) NOT NULL,
  `MasteralDegree` varchar(200) NOT NULL,
  `DoctoralDegree` varchar(200) NOT NULL,
  `ImagePath` varchar(255) NOT NULL,
  `Comment` varchar(255) NOT NULL,
  PRIMARY KEY (`ProfessorID`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tblprofessor`
--

LOCK TABLES `tblprofessor` WRITE;
/*!40000 ALTER TABLE `tblprofessor` DISABLE KEYS */;
INSERT INTO `tblprofessor` VALUES (1,'Deseo Jr.','Oscar','Santos','2000-01-01 00:00:00','','1','1','1','1','1','1','1','1','1'),(2,'Malquitar','Welly',NULL,'2000-01-01 00:00:00','','2','2','2','2','2','2','2','2','2'),(3,'Santillan','Mikaela Jensen',NULL,'2000-01-01 00:00:00','\0','3','3','3','3','3','3','3','3','3'),(4,'Doe','John',NULL,'2000-01-01 00:00:00','','4','4','44','4','4','4','4','4','4'),(5,'Baker','Susan','Martin','2000-01-01 00:00:00','\0','5','5','5','55','5','5','5','5','5'),(6,'Musk','Elon',NULL,'2000-01-01 00:00:00','','6','6','6','6','6','6','6','6','6');
/*!40000 ALTER TABLE `tblprofessor` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tblstudentsched`
--

DROP TABLE IF EXISTS `tblstudentsched`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tblstudentsched` (
  `StudentScheduleID` bigint(20) NOT NULL AUTO_INCREMENT,
  `StudentNumber` varchar(100) NOT NULL,
  `ClassSubjectID` bigint(20) NOT NULL,
  `Dropped` bit(1) NOT NULL,
  PRIMARY KEY (`StudentScheduleID`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tblstudentsched`
--

LOCK TABLES `tblstudentsched` WRITE;
/*!40000 ALTER TABLE `tblstudentsched` DISABLE KEYS */;
INSERT INTO `tblstudentsched` VALUES (1,'1',3,'\0');
/*!40000 ALTER TABLE `tblstudentsched` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tblsubjects`
--

DROP TABLE IF EXISTS `tblsubjects`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tblsubjects` (
  `SubjectID` varchar(50) NOT NULL,
  `SubjectDescription` varchar(255) NOT NULL,
  `Abbreviation` varchar(50) NOT NULL,
  `Prerequisite` varchar(50) DEFAULT NULL,
  `NoUnits` int(11) NOT NULL,
  `LectureHours` double NOT NULL,
  `LaboratoryHours` double NOT NULL,
  `LaboratoryType` varchar(100) DEFAULT NULL,
  PRIMARY KEY (`SubjectID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tblsubjects`
--

LOCK TABLES `tblsubjects` WRITE;
/*!40000 ALTER TABLE `tblsubjects` DISABLE KEYS */;
INSERT INTO `tblsubjects` VALUES ('BFIL 1','Komunikasyon sa Akademikong Filipino','FIL 1',NULL,3,3,1,NULL),('CS1A3','Computer Fundamentals','ComFun',NULL,3,3,2,NULL),('ENG 1','Integrated Skills In English','ENG 1',NULL,3,3,1,NULL),('IT1A1','Bussiness Application Software','BAS 1',NULL,3,3,2,NULL),('IT1A2','Fundamentals of Logical Formulation','Logic',NULL,3,3,1,NULL),('MAT 105','College Algebra','Algebra',NULL,3,3,2,NULL),('MAT 109','Plane Trigonometry','Trigo',NULL,3,3,1,NULL),('NSTP 1','National Service Training Program 1','NSTP 1',NULL,0,3,0,NULL),('PE 1','Physical Education 1','PE 1',NULL,2,2,0,NULL);
/*!40000 ALTER TABLE `tblsubjects` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tblsystemvariable`
--

DROP TABLE IF EXISTS `tblsystemvariable`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tblsystemvariable` (
  `[Key]` varchar(50) NOT NULL,
  `DataType` varchar(50) NOT NULL,
  `Value` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`[Key]`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tblsystemvariable`
--

LOCK TABLES `tblsystemvariable` WRITE;
/*!40000 ALTER TABLE `tblsystemvariable` DISABLE KEYS */;
INSERT INTO `tblsystemvariable` VALUES ('ITClassMaxCount','Numeric','40'),('MaxNumberOfYears','Numeric','6'),('NonITClassMaxCount','Numeric','40'),('SchoolYear','Text','2017-2018'),('Semester','Boolean','True');
/*!40000 ALTER TABLE `tblsystemvariable` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping routines for database 'db'
--
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2018-02-26  3:22:25
