-- INSERT INTO `cis_actions` (`action_id`, `action`, `parent`, `action_order`) VALUES ('42', 'Referral', '11', '12');

ALTER TABLE `pat_visit_info` 
ADD COLUMN `referral_id` INT NULL AFTER `employee_id`;

DROP TABLE IF EXISTS `cis_referral`;
CREATE TABLE `cis_referral` (
  `cis_referral_id` int(11) NOT NULL AUTO_INCREMENT,
  `referral_name` varchar(100) NOT NULL,
  `contact_no` varchar(45) DEFAULT NULL,
  `contact_address` varchar(250) DEFAULT NULL,
  `status` int(11) DEFAULT '1' COMMENT '1-Active, 2-In-Active',
  PRIMARY KEY (`cis_referral_id`),
  UNIQUE KEY `referral_name_UNIQUE` (`referral_name`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8;

DROP TABLE IF EXISTS `cis_master`;
CREATE TABLE `cis_master` (
  `cism_id` INT NOT NULL AUTO_INCREMENT,
  `group_id` INT NOT NULL,
  `master_id` INT NOT NULL,
  `master_value` VARCHAR(100) NOT NULL,
  `status` INT NOT NULL DEFAULT 1,
  PRIMARY KEY (`cism_id`));
  
  /*
-- Query: SELECT * FROM cis4.cis_master
LIMIT 0, 1000

-- Date: 2018-12-22 13:15
*/
INSERT INTO `cis_master` (`cism_id`,`group_id`,`master_id`,`master_value`,`status`) VALUES (1,1,1,'Damaged',1);
INSERT INTO `cis_master` (`cism_id`,`group_id`,`master_id`,`master_value`,`status`) VALUES (2,1,2,'Expired',1);
INSERT INTO `cis_master` (`cism_id`,`group_id`,`master_id`,`master_value`,`status`) VALUES (3,2,1,'Damaged Product',1);
INSERT INTO `cis_master` (`cism_id`,`group_id`,`master_id`,`master_value`,`status`) VALUES (4,2,2,'Expired Product',1);
INSERT INTO `cis_master` (`cism_id`,`group_id`,`master_id`,`master_value`,`status`) VALUES (5,2,3,'Internal Usage',1);

 DROP TABLE IF EXISTS `pha_internal_movements`; 
  CREATE TABLE `pha_internal_movements` (
  `pha_in_mo_id` INT NOT NULL AUTO_INCREMENT,
  `movement_type` VARCHAR(100) NOT NULL COMMENT '1. Internal Movements, 2. Return Vendor, 3.Consume Items',
  `im_number` VARCHAR(45) NOT NULL,
  `department_id` INT NULL,
  `transaction_date` DATETIME NULL,
  `entry_date` DATETIME NULL,
  `consume_type_id` INT NULL,
  `vendor_id` INT NULL,
  `return_no` VARCHAR(45) NULL,
  `total_amount` DECIMAL(10,2) NOT NULL DEFAULT 0,
  PRIMARY KEY (`pha_in_mo_id`));

  
   DROP TABLE IF EXISTS `pha_internal_movements_details`; 
  CREATE TABLE `pha_internal_movements_details` (
  `pha_in_mo_de_id` INT NOT NULL AUTO_INCREMENT,
  `pha_in_mo_id` INT NOT NULL,
  `transaction_date` DATETIME NULL,
  `item_type_id` INT NULL,
  `item_id` INT NULL,
  `trans_qty` INT NULL,
  `lot_id` VARCHAR(45) NULL,
  `expiry_date` VARCHAR(45) NULL,
  `vendor_price` DECIMAL(10,2) NULL DEFAULT 0,
  `total_amount` DECIMAL(25,2) NULL DEFAULT 0,
  `tax_perc` DECIMAL(10,2) NULL DEFAULT 0,
  `tax_amt` DECIMAL(10,2) NULL DEFAULT 0,
  `net_total_amount` DECIMAL(25,2) NULL DEFAULT 0,
  PRIMARY KEY (`pha_in_mo_de_id`));
  
  INSERT INTO `cis_number_format` (`number_format_id`, `field_name`, `number_format`, `prefix_date`, `prefix_text`) VALUES ('12', 'Inventory Movements', '######', 'yy', 'IM');

  ALTER TABLE `pha_internal_movements_details` 
ADD COLUMN `inventory_stock_id` INT NULL AFTER `net_total_amount`;


DROP TABLE IF EXISTS `cis_actions`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `cis_actions` (
  `action_id` int(11) NOT NULL AUTO_INCREMENT,
  `action` varchar(100) DEFAULT NULL,
  `parent` int(1) DEFAULT NULL,
  `action_order` int(11) DEFAULT NULL,
  PRIMARY KEY (`action_id`)
) ENGINE=InnoDB AUTO_INCREMENT=45 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `cis_actions`
--

LOCK TABLES `cis_actions` WRITE;
/*!40000 ALTER TABLE `cis_actions` DISABLE KEYS */;
INSERT INTO `cis_actions` VALUES (1,'Invoice',0,NULL),(2,'Reg and Invoice',1,1),(3,'Inventory Manage',44,2),(4,'Purchase',44,1),(5,'Transfer Patient',43,2),(6,'Discharge Patient',43,3),(7,'View Bill Info',31,3),(8,'Cancel Visit and Bill',43,1),(9,'Inventory Movements',44,3),(10,'Reports',1,2),(11,'Admin',0,NULL),(12,'Department',11,1),(13,'Patient Type',11,2),(14,'Doctor',11,3),(15,'Address Info',11,4),(16,'Corporate',11,5),(17,'Room and Bed',11,10),(18,'Define Reg Fee',43,4),(19,'Pharmacy Master',44,4),(20,'Investigation Master',24,3),(21,'Account Master',31,5),(22,'Advance Transaction',31,1),(23,'Ward Bill',31,2),(24,'Investigation',0,NULL),(27,'Test Field',24,4),(28,'Map Test Fields',24,5),(29,'Result Entry',24,1),(30,'View Lab Bill',24,2),(31,'Billing',0,NULL),(32,'User Role',11,7),(33,'User',11,6),(34,'User Rights',11,8),(35,'Enable -> Registration',2,1),(36,'Enable -> Invoice',2,2),(37,'Enable -> Cancel/Refund',2,3),(38,'Enable -> Investigation',2,4),(39,'Enable -> Pharmacy',2,5),(40,'Enable -> General',2,6),(41,'Corporate Due List',31,4),(42,'Referral',11,9),(43,'Registration',0,NULL),(44,'Pharmacy',0,NULL);
/*!40000 ALTER TABLE `cis_actions` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `cis_map_role_action`
--

DROP TABLE IF EXISTS `cis_map_role_action`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `cis_map_role_action` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `role_id` int(11) DEFAULT NULL,
  `action_id` int(11) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=297 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `cis_map_role_action`
--

LOCK TABLES `cis_map_role_action` WRITE;
/*!40000 ALTER TABLE `cis_map_role_action` DISABLE KEYS */;
INSERT INTO `cis_map_role_action` VALUES (143,3,2),(144,3,35),(224,2,2),(225,2,35),(261,1,2),(262,1,22),(263,1,23),(264,1,8),(265,1,7),(266,1,5),(267,1,6),(268,1,4),(269,1,3),(270,1,9),(271,1,10),(272,1,35),(273,1,36),(274,1,37),(275,1,38),(276,1,39),(277,1,40),(278,1,12),(279,1,13),(280,1,14),(281,1,15),(282,1,16),(283,1,17),(284,1,18),(285,1,20),(286,1,19),(287,1,21),(288,1,41),(289,1,42),(290,1,27),(291,1,28),(292,1,29),(293,1,30),(294,1,32),(295,1,33),(296,1,34);
/*!40000 ALTER TABLE `cis_map_role_action` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

ALTER TABLE `cis_user` 
DROP FOREIGN KEY `cis_user_ibfk_1`;

-- Dump completed on 2018-12-22 13:13:32
UPDATE `cis_user` SET `user_role_id`='1' WHERE `USER_ID`='1';
UPDATE `cis_roles` SET `role_id`='1' WHERE `role_id`='4';

-- UPDATE `cis_number_format` SET `number_format`='####' WHERE `number_format_id`='1';

-- On 09-01-2019
ALTER TABLE `cis_investigation_list` 
ADD COLUMN `share_type` INT(11) NOT NULL DEFAULT 0 AFTER `unit_price`,
ADD COLUMN `amt_type` INT(11) NOT NULL DEFAULT 0 AFTER `share_type`,
ADD COLUMN `share_per` DECIMAL(20,2) NULL DEFAULT 0.00 AFTER `amt_type`,
ADD COLUMN `share_amt` DECIMAL(20,2) NULL DEFAULT 0.00 AFTER `share_per`;


INSERT INTO `cis_master` (`group_id`, `master_id`, `master_value`, `status`) VALUES ('3', '1', 'Bio-Line', '1');

-- On 24-01-2019
ALTER TABLE `inv_bill_datail_info` 
ADD COLUMN `share_type` INT(11) NULL DEFAULT 0 AFTER `status`,
ADD COLUMN `share_amt` DECIMAL(15,2) NULL DEFAULT 0.00 AFTER `share_type`;

-- Corporate : <CORPORATENAME>