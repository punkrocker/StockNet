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
  `Level` int(11) DEFAULT '0',
  PRIMARY KEY (`Id`),
  UNIQUE KEY `Name_UNIQUE` (`Name`)
) ENGINE=InnoDB AUTO_INCREMENT=21 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `st_cat`
--

LOCK TABLES `st_cat` WRITE;
/*!40000 ALTER TABLE `st_cat` DISABLE KEYS */;
INSERT INTO `st_cat` VALUES (3,'服装服饰',0,',3,',NULL,0),(4,'鞋帽皮具',0,',4,',NULL,0),(5,'日用百货',0,',5,',NULL,0),(6,'家用电器',0,',6,',NULL,0),(7,'电子器械',0,',7,',NULL,0),(8,'五金工具',0,',8,',NULL,0),(9,'体育用品',0,',9,',NULL,0),(10,'办公用品(书籍音像)',0,',10,',NULL,0),(11,'玩具',0,',11,',NULL,0),(12,'珠宝首饰',0,',12,',NULL,0),(13,'钟表眼镜',0,',13,',NULL,0),(14,'箱包类',0,',14,',NULL,0),(15,'工艺品',0,',15,',NULL,0),(16,'美妆清洁',0,',16,',NULL,0),(17,'衣帽',3,',3,17,',NULL,1),(18,'帽子',17,',3,17,18,',NULL,2),(19,'大衣',17,',3,17,19,',NULL,2),(20,'饰品',3,',3,20,',NULL,1);
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
  `CatPath` varchar(150) DEFAULT NULL,
  `GoodsType` int(11) DEFAULT '0',
  `IsBrandGoods` bit(1) DEFAULT b'0',
  `BrandName` varchar(45) DEFAULT NULL,
  `GoodsArea` varchar(45) DEFAULT NULL,
  `Mobile` varchar(45) DEFAULT NULL,
  `Tel` varchar(45) DEFAULT NULL,
  `QQ` varchar(45) DEFAULT NULL,
  `Wechat` varchar(150) DEFAULT NULL,
  `RealName` varchar(45) DEFAULT NULL,
  `Alipay` varchar(150) DEFAULT NULL,
  `Addr` varchar(150) DEFAULT NULL,
  `Email` varchar(150) DEFAULT NULL,
  `Remark` varchar(150) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `Name_UNIQUE` (`Name`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `st_goods`
--

LOCK TABLES `st_goods` WRITE;
/*!40000 ALTER TABLE `st_goods` DISABLE KEYS */;
INSERT INTO `st_goods` VALUES (1,'你好测试航拍你好测试，你好测试你好测试你好测试你好测试你好测试',17,1,'/Site/Goods/20150820/Chrysanthemum.jpg',23,NULL,2,NULL,'WK1508201','<p><span style=\"white-space: nowrap;\">小黄豆开源免费CRM客户关系管理系统说明</span></p><p><span style=\"white-space: nowrap;\"><br/></span></p><p><span style=\"white-space: nowrap;\">软件名称: 小黄豆CRM</span></p><p><span style=\"white-space: nowrap;\">开发语言: C# .net MSSQL2005 MSSQL2008&nbsp;</span></p><p><span style=\"white-space: nowrap;\">软件类别: 企业管理类</span></p><p><span style=\"white-space: nowrap;\">软件授权: GPL开源协议，免费</span></p><p><span style=\"white-space: nowrap;\">官方网址：http://www.xhdcrm.com</span></p><p><span style=\"white-space: nowrap;\">演示地址：http://demo.xhdcrm.com</span></p><p><span style=\"white-space: nowrap;\">论坛地址：http://bbs.xhdcrm.com</span></p><p><span style=\"white-space: nowrap;\">软件简介：小黄豆CRM客户关系管理系统是一款开源免费的客户关系管理系统，基于LigerUI和.net的技术架构，完全开源加上强大稳定的技术架构，无论是系统速度还是使用安全性，以及可扩展性，都是您对客户关系管理系统的不二选择。</span></p><p><span style=\"white-space: nowrap;\">&nbsp; &nbsp; &nbsp; &nbsp; 小黄豆开源免费CRM客户关系管理系统自开发以来，一直致力于帮助企业快速成长，所以我们推出了一系列的措施来实现这个理念，最大的措施便是开源与免费，相比其他客户关系管理系统，我们不限制用户数，无需注册，您下载下来之后，配置好即可免费放心的使用。</span></p><p><span style=\"white-space: nowrap;\"><br/></span></p><p><span style=\"white-space: nowrap;\"><br/></span></p><p><span style=\"white-space: nowrap;\">默认【管理员】：admin</span></p><p><span style=\"white-space: nowrap;\">&nbsp; &nbsp; 【密 &nbsp;码】：123456</span></p><p><span style=\"white-space: nowrap;\">新增用户默认密码：123456</span></p><p><span style=\"white-space: nowrap;\"><br/></span></p><p><span style=\"white-space: nowrap;\"><br/></span></p><p><span style=\"white-space: nowrap;\">【v1.16.0】更新项</span></p><p><span style=\"white-space: nowrap;\">1、【修复】岗位的职务级别有时不能加载。</span></p><p><span style=\"white-space: nowrap;\">2、【优化】调整所有页面的加载顺序，防止有时数据不能显示。</span></p><p><span style=\"white-space: nowrap;\">3、【优化】所有非必填选项可以选择空。</span></p><p><span style=\"white-space: nowrap;\">4、【修复】部分用户批量导入失败。</span></p><p><span style=\"white-space: nowrap;\">5、【修复】导入数据不刷新。</span></p><p><span style=\"white-space: nowrap;\">6、【修复】城市删除出错。</span></p><p><span style=\"white-space: nowrap;\">7、【修复】城市更新不显示实时数据。</span></p><p><span style=\"white-space: nowrap;\">8、【优化】城市可以排序，方便自己所在城市的业务。</span></p><p><span style=\"white-space: nowrap;\">9、【优化】树形表格的加载效率。</span></p><p><span style=\"white-space: nowrap;\">10、【优化】树形表格加入新图标，更美观，方便分清层次。</span></p><p><span style=\"white-space: nowrap;\">11、【优化】省份不能选择上级，防止修改出错。</span></p><p><span style=\"white-space: nowrap;\">12、【优化】新增客户自动选择当前登录账户。</span></p><p><span style=\"white-space: nowrap;\">13、【优化】批量转移客户可选择条件。</span></p><p><span style=\"white-space: nowrap;\">14、【新增】销售漏斗功能。</span></p><p><span style=\"white-space: nowrap;\">15、【修复】部分用户无法选择联系人。</span></p><p><span style=\"white-space: nowrap;\">16、【修复】员工分析报表错误。</span></p><p><span style=\"white-space: nowrap;\">17、【修复】统计年报统计了回收站的数据。</span></p><p><span style=\"white-space: nowrap;\">18、【优化】统计报表的统计选项有默认值。</span></p><p><span style=\"white-space: nowrap;\">19、【修复】日程的日视图不显示。</span></p><p><span style=\"white-space: nowrap;\">20、【修复】收款人权限问题。</span></p><p><span style=\"white-space: nowrap;\">21、【优化】公告可以插入图片。</span></p><p><span style=\"white-space: nowrap;\">22、【优化】联系人加入更详细显示项。</span></p><p><span style=\"white-space: nowrap;\">23、【优化】客户查询加入模糊多字段搜索选项。</span></p><p><span style=\"white-space: nowrap;\">24、【新增】启动工具，无需配置，直接启动，方便快速浏览。</span></p><p><span style=\"white-space: nowrap;\">25、【优化】弹出页面可以折叠，方便查看数据。</span></p><p><span style=\"white-space: nowrap;\">26、【修复】日志右键错误。</span></p><p><span style=\"white-space: nowrap;\"><br/></span></p><p><span style=\"white-space: nowrap;\">【v1.16.1】更新项</span></p><p><span style=\"white-space: nowrap;\">1、【修复】日程错误。</span></p><p><span style=\"white-space: nowrap;\">2、【修复】新增客户不能添加联系人。</span></p><p><span style=\"white-space: nowrap;\">3、【修复】产品类别错误。</span></p><p><span style=\"white-space: nowrap;\">4、【修复】合同错误。</span></p><p><span style=\"white-space: nowrap;\">5、【优化】部分用户升级脚本出错。</span></p><p><span style=\"white-space: nowrap;\">6、【优化】加入销售漏斗脚本。</span></p><p><span style=\"white-space: nowrap;\"><br/></span></p><p><span style=\"white-space: nowrap;\"><br/></span></p><p><span style=\"white-space: nowrap;\">快速体验请参考“小黄豆CRM在线安装步骤.doc”；</span></p><p><span style=\"white-space: nowrap;\">【注意】工具只是为了快速体验使用，正式用户建议使用IIS配置，配置方法请参考小黄豆CRM论坛的教程板块；</span></p><p><span style=\"white-space: nowrap;\"><br/></span></p><p><span style=\"white-space: nowrap;\">【v1.16.1】用户【升级】步骤：</span></p><p><span style=\"white-space: nowrap;\">【注意】如果是v1.0.0.10以前的版本，请先覆盖再按顺序执行DB文件夹的【v1.0.0.10升级脚本】【v1.0.0.11升级脚本】【v1.15.0.1升级脚本】【v1.16.1.316升级脚本】。</span></p><p><span style=\"white-space: nowrap;\">1、备份原源码。</span></p><p><span style=\"white-space: nowrap;\">2、解压源码并覆盖原来源码。</span></p><p><span style=\"white-space: nowrap;\">3、将备份文件的conn.config文件覆盖现在的conn.config文件。</span></p><p><span style=\"white-space: nowrap;\">4、备份数据库。</span></p><p><span style=\"white-space: nowrap;\">5、执行DB文件夹下【v1.16.1.319升级脚本】</span></p><p><span style=\"white-space: nowrap;\"><br/></span></p><p><span style=\"white-space: nowrap;\">【升级说明】如之前对源码文件进行过修改的用户，请备份好已修改的文件再升级，小黄豆CRM不保证用户自己修改过源码文件的用户能升级成功。</span></p><p><span style=\"white-space: nowrap;\"><br/></span></p><p><span style=\"white-space: nowrap;\">【v1.16.1.319】用户配置不成功的，可以直接附加数据库的方法。</span></p><p><span style=\"white-space: nowrap;\">1、附加DB文件夹下的XHDCRM.mdf，具体方法请baidu一下。</span></p><p><span style=\"white-space: nowrap;\">2、修改conn.config</span></p><p><span class=\"Apple-tab-span\" style=\"white-space:pre\">	</span>①将ConStringEncrypt节设置为false。</p><p><span class=\"Apple-tab-span\" style=\"white-space:pre\">	</span>②将ConnectionString节设置为server=你的sql服务器名;database=XHDCRM;uid=sa;pwd=你的sa密码。</p><p><span class=\"Apple-tab-span\" style=\"white-space:pre\">	</span>③将CompleteConfig节设置为true。</p><p><span style=\"white-space: nowrap;\">3、如果字符串需要加密，可以使用Tools文件夹下的加密解密工具。</span></p><p><span style=\"white-space: nowrap;\"><br/></span></p><p><span style=\"white-space: nowrap;\">欢迎广大用户反馈问题，因系统复杂，所以有bug或设计不合理情况在所难免，所以有问题请大家及时反馈，我们将第一时间为大家解决。</span></p><p><br/></p>','2015-08-20 09:54:16','2015-08-20 09:54:16',2,NULL,1,'',NULL,4,0,',3,17,',0,'\0',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL),(2,'夫共同宏图4365',14,1,'/Site/Goods/20150820/Desert.jpg',23.65,'面议',323,'最少','WK1508202','<h2><span style=\"white-space: nowrap;\">小黄豆开源免费CRM客户关系管理系统说明</span></h2><p><span style=\"white-space: nowrap;\"><br/></span></p><p><span style=\"white-space: nowrap;\">软件名称: <strong>小黄豆CRM</strong></span></p><p><span style=\"white-space: nowrap;\">开发语言: C# .net MSSQL2005 MSSQL2008&nbsp;</span></p><p style=\"text-align: center;\"><span style=\"white-space: nowrap;\">软件类别: 企业管理类</span></p><p><span style=\"white-space: nowrap;\">软件授权: GPL开源协议，免费</span></p><p><span style=\"white-space: nowrap;\">官方网址：http://www.xhdcrm.com</span></p><p><span style=\"white-space: nowrap;\">演示地址：http://demo.xhdcrm.com</span></p><p><span style=\"white-space: nowrap;\">论坛地址：http://bbs.xhdcrm.com</span></p><p><span style=\"white-space: nowrap;\"><img src=\"http://localhost:35340/site/goods/20150820/d50eb485-c4c9-46fe-8075-7937fa32aaee.jpg\" _src=\"http://localhost:35340/site/goods/20150820/d50eb485-c4c9-46fe-8075-7937fa32aaee.jpg\" style=\"width: 523px; height: 394px;\"/></span></p><p><span style=\"white-space: nowrap;\">软件简介：小黄豆CRM客户关系管理系统是一款开源免费的客户关系管理系统，基于LigerUI和.net的技术架构，完全开源加上强大稳定的技术架构，无论是系统速度还是使用安全性，以及可扩展性，都是您对客户关系管理系统的不二选择。</span></p><p><span style=\"white-space: nowrap;\">&nbsp; &nbsp; &nbsp; &nbsp; 小黄豆开源免费CRM客户关系管理系统自开发以来，一直致力于帮助企业快速成长，所以我们推出了一系列的措施来实现这个理念，最大的措施便是开源与免费，相比其他客户关系管理系统，我们不限制用户数，无需注册，您下载下来之后，配置好即可免费放心</span></p><p><br/></p>','2015-08-20 10:01:08','2015-08-20 10:01:08',2,NULL,1,'',NULL,2,0,',14,',0,'\0',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL),(3,'gdfh',16,1,'/Site/Goods/20150820/635756622103758164_Tulips.jpg',43,NULL,3,NULL,'WK1508203','<p>f</p>','2015-08-20 10:10:10','2015-08-20 10:10:10',2,NULL,1,'\0',NULL,1,0,',16,',0,'\0',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL),(4,'bfn b',0,1,'/Site/Goods/20150820/635756631982138100_Penguins.jpg',434,NULL,566,NULL,'WK1508204','<p>g</p>','2015-08-20 10:26:38','2015-08-20 10:26:38',2,NULL,1,'\0',NULL,1,0,NULL,0,'\0',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL),(5,'htj',16,1,'/Site/Goods/20150820/635756632689677390_Tulips.jpg',2,NULL,4,NULL,'WK1508205','<p>g</p>','2015-08-20 10:27:48','2015-08-20 10:27:48',2,NULL,1,'\0',NULL,7,0,',16,',0,'\0',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL);
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
  `AddUser` int(11) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `st_log`
--

LOCK TABLES `st_log` WRITE;
/*!40000 ALTER TABLE `st_log` DISABLE KEYS */;
INSERT INTO `st_log` VALUES (1,6,'admin;::1;Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/31.0.1650.63 Safari/537.36',NULL,'2015-08-19 20:48:40',NULL),(2,6,'admin;::1;Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/31.0.1650.63 Safari/537.36',NULL,'2015-08-19 20:49:27',NULL),(3,6,'admin;::1;Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/31.0.1650.63 Safari/537.36',NULL,'2015-08-19 21:13:29',NULL),(4,6,'admin;::1;Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/31.0.1650.63 Safari/537.36',NULL,'2015-08-20 10:35:48',NULL),(5,6,'admin;::1;Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/31.0.1650.63 Safari/537.36',NULL,'2015-08-20 13:29:41',NULL),(6,6,'admin;::1;Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/31.0.1650.63 Safari/537.36',NULL,'2015-08-26 20:50:48',NULL),(7,6,'admin;::1;Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/31.0.1650.63 Safari/537.36',NULL,'2015-09-05 21:58:24',NULL),(8,6,'admin;::1;Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/31.0.1650.63 Safari/537.36',NULL,'2015-09-06 09:33:30',NULL),(9,5,'test从普通会员到VIP会员',1,'2015-09-06 09:33:51',1);
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
  `VipOverDate` datetime DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `st_member`
--

LOCK TABLES `st_member` WRITE;
/*!40000 ALTER TABLE `st_member` DISABLE KEYS */;
INSERT INTO `st_member` VALUES (1,2,'VIP会员','test','sj','dh','cw@qq.com','qq','wx',NULL,'addr','qt','2015-08-08 12:03:26',NULL,'\0','2016-09-06 09:33:51'),(2,4,'普通会员','flgjt','2462358754',NULL,'fds@rrf.com','45356r55',NULL,NULL,NULL,NULL,'2015-09-05 21:57:52',NULL,'\0',NULL);
/*!40000 ALTER TABLE `st_member` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `st_notice`
--

DROP TABLE IF EXISTS `st_notice`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `st_notice` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Title` varchar(150) DEFAULT NULL,
  `Notice` text,
  `AddTime` datetime DEFAULT CURRENT_TIMESTAMP,
  `AddUser` int(11) DEFAULT NULL,
  `OverTime` datetime DEFAULT NULL,
  `NoticeType` int(11) DEFAULT '0',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `st_notice`
--

LOCK TABLES `st_notice` WRITE;
/*!40000 ALTER TABLE `st_notice` DISABLE KEYS */;
INSERT INTO `st_notice` VALUES (1,'欢迎使用中国尾库网','<p>你好，欢迎使用</p>','2015-08-15 17:37:15',1,NULL,0),(3,'a','<p>f</p>','2015-08-19 21:22:45',1,NULL,1);
/*!40000 ALTER TABLE `st_notice` ENABLE KEYS */;
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
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `st_pic`
--

LOCK TABLES `st_pic` WRITE;
/*!40000 ALTER TABLE `st_pic` DISABLE KEYS */;
INSERT INTO `st_pic` VALUES (1,5,NULL,'/Site/Goods/20150820/Penguins.jpg','2015-08-20 09:46:00',NULL),(2,5,1,'/Site/Goods/20150820/Tulips.jpg','2015-08-20 09:54:05',NULL);
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
INSERT INTO `st_role` VALUES (1,'测试角色','Dash,Cat,Setting',NULL);
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
  `Val` text,
  `AddTime` datetime DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `Name_UNIQUE` (`Name`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `st_setting`
--

LOCK TABLES `st_setting` WRITE;
/*!40000 ALTER TABLE `st_setting` DISABLE KEYS */;
INSERT INTO `st_setting` VALUES (1,'st_about','<p><p>中国尾库网创立于2015年5月，中国尾库网立志打造中国最大库存尾货信息资源发布平台。</p><p>中国尾库网面向全国库存朋友免费开放。本网站货源汇聚来自全国的库存工厂、库存商家、地摊供应商及实体店、淘宝商家等。</p><p>中国尾库网秉持免费、公开、真实、有效的原则，在网站注册会员、发布信息完全免费。&nbsp;本站所有产品供求均经过人工审核、过滤整理，为网站浏览者提供一个绿色、整洁、干净的浏览环境。</p><p>中国尾库网以服务全国所有创业者为己任，为所有创业者提供最新畅销产品市场动态，让创业者能及时查看市场最新产品信息。</p></p>','2015-08-13 10:47:00'),(2,'st_sitename','中国尾库网',NULL),(3,'st_footerinfo','电话：15372387977（刘）或13736803651（王） QQ：168481511/2321234369     \r\n 微信:   15372387977/cookie3663     E-mail: 168481511@qq.com',NULL),(4,'st_contact','<p>广告与市场合作: &nbsp;&nbsp;</p><p>&nbsp; &nbsp; &nbsp; 联系方式: 15372387977（刘）或13736803651（王）</p><p>&nbsp; &nbsp; &nbsp; QQ号码:168481511/2321234369</p><p>&nbsp; &nbsp; &nbsp; 微信: &nbsp; 15372387977/cookie3663</p><p>&nbsp; &nbsp; &nbsp; E-mail: 168481511@qq.com</p><p>地址：青岛。。。。&nbsp;</p><p>&nbsp; &nbsp; &nbsp; 嘉兴。。。。</p><p><br/></p>','2015-08-13 10:55:49'),(5,'st_guide','<p style=\";text-align:justify\"><span style=\";font-weight:bold;font-size:14px;font-family:&#39;宋体&#39;\">1.<span style=\"font-family:宋体\">如何处理库存：您可以在网站上免费发布库存货源信息，如果您的库存货源须找到精准客源，请加入</span><span style=\"font-family:Times New Roman\">VIP</span><span style=\"font-family:宋体\">会员：</span><span style=\"font-family:Times New Roman\">800</span><span style=\"font-family:宋体\">元</span><span style=\"font-family:Times New Roman\">/</span><span style=\"font-family:宋体\">年（服务一年）。一年内无限发布库存信息和为您找对应的精准客源。</span></span></p><p style=\";text-align:justify\"><span style=\"font-weight:bold;font-size:14px;font-family:&#39;宋体&#39;\">2.</span><span style=\";font-weight:bold;font-size:14px;font-family:&#39;宋体&#39;\">如何买库存：我们汇集了全国各地低于成本价的超值库存货源和工厂尾单；您可以在网站上免费注册会员浏览任何一条库存信息；如果您看中的任何库存货源想联系货主；请注册<span style=\"font-family:Times New Roman\">VIP</span><span style=\"font-family:宋体\">会员；</span><span style=\"font-family:Times New Roman\">800</span><span style=\"font-family:宋体\">元</span><span style=\"font-family:Times New Roman\">/</span><span style=\"font-family:宋体\">年（服务一年）。一年内无限次查看任何一条库存货源信息并可直接联系货主。为确保货款安全，本网站也可提供担保交易，买卖双方洽谈好后，把货款直接打到本网站，网站会通知货主发货，买家收到货后通知本网站放款给货主，服务费</span><span style=\"font-family:Times New Roman\">1%</span><span style=\"font-family:宋体\">。</span></span></p><p style=\";text-align:justify\"><span style=\"font-weight:bold;font-size:14px;font-family:&#39;宋体&#39;\">3.</span><span style=\";font-weight:bold;font-size:14px;font-family:&#39;宋体&#39;\">如何做代理：地区代理（地区市）<span style=\"font-family:Times New Roman\">-----</span><span style=\"font-family:宋体\">分代理（县级市）。</span></span></p><p style=\";text-align:justify\"><span style=\";font-weight:bold;font-size:14px;font-family:&#39;宋体&#39;\">地区代理（<span style=\"font-family:Times New Roman\">2980</span><span style=\"font-family:宋体\">元</span><span style=\"font-family:Times New Roman\">/</span><span style=\"font-family:宋体\">年）：根据当地地区，整合整个地区</span><span style=\"font-family:Times New Roman\">(</span><span style=\"font-family:宋体\">包括旗下的县市以及乡镇），包括当地的每个地区的批发市场，步行街，中小超市卖场，淘宝商家等资源招募</span><span style=\"font-family:Times New Roman\">VIP</span><span style=\"font-family:宋体\">会员</span><span style=\"font-family:Times New Roman\">:800</span><span style=\"font-family:宋体\">元</span><span style=\"font-family:Times New Roman\">/</span><span style=\"font-family:宋体\">年。</span></span></p><p style=\";text-align:justify\"><span style=\";font-weight:bold;font-size:14px;font-family:&#39;宋体&#39;\">分代理（<span style=\"font-family:Times New Roman\">1280</span><span style=\"font-family:宋体\">元</span><span style=\"font-family:Times New Roman\">/</span><span style=\"font-family:宋体\">年</span><span style=\"font-family:Times New Roman\">):</span><span style=\"font-family:宋体\">根据本地区优势，整合本地区的资源（包括旗下市区的批发市场，商场卖场，中小超市，乡镇批发商，淘宝商家等）招募</span><span style=\"font-family:Times New Roman\">VIP</span><span style=\"font-family:宋体\">会员：</span><span style=\"font-family:Times New Roman\">800</span><span style=\"font-family:宋体\">元</span><span style=\"font-family:Times New Roman\">/</span><span style=\"font-family:宋体\">年</span></span></p><p style=\";text-align:justify\"><span style=\";font-weight:bold;font-size:14px;font-family:&#39;宋体&#39;\">说明<span style=\"font-family:Times New Roman\">:</span><span style=\"font-family:宋体\">一个地区一家代理的原则，代理的工作就推广和招募，后续所有的工作全部由本网站来服务。&nbsp;</span></span></p><p style=\";text-align:justify\"><span style=\"font-weight:bold;font-size:14px;font-family:&#39;宋体&#39;\">4.</span><span style=\";font-weight:bold;font-size:14px;font-family:&#39;宋体&#39;\">合作共赢：如果您当地有特色的产业，拥有行业的库存资源，请与我们联系，资源共享，合作共赢。</span></p><p style=\";text-align:justify\"><span style=\"font-weight:bold;font-size:14px;font-family:&#39;宋体&#39;\">5.</span><span style=\";font-weight:bold;font-size:14px;font-family:&#39;宋体&#39;\">库存真实性：我们每天会对发布出来的库存信息进行认证和审核，尽可能确保每一条信息的真实性；如果您对某库存货源存有质疑，尽量请当面交易或者通过本网站担保交易。</span></p><p style=\";text-align:justify\"><span style=\"font-weight:bold;font-size:14px;font-family:&#39;宋体&#39;\">6.</span><span style=\";font-weight:bold;font-size:14px;font-family:&#39;宋体&#39;\">价格优势<span style=\"font-family:Times New Roman\">:</span><span style=\"font-family:宋体\">每天所有发布的库存货源必须是要低于生产成本的价格；本网站不接收常规价格的商品。</span></span></p><p><span style=\";font-weight:bold;font-size:14px;font-family:&#39;宋体&#39;\">费用说明：<span style=\"font-family:Times New Roman\">VIP</span><span style=\"font-family:宋体\">会员：</span><span style=\"font-family:Times New Roman\">800</span><span style=\"font-family:宋体\">元</span><span style=\"font-family:Times New Roman\">/</span><span style=\"font-family:宋体\">年。地区城市代理：</span><span style=\"font-family:Times New Roman\">2980</span><span style=\"font-family:宋体\">元</span><span style=\"font-family:Times New Roman\">/</span><span style=\"font-family:宋体\">年。县市级城市代理：</span><span style=\"font-family:Times New Roman\">1280</span><span style=\"font-family:宋体\">元</span><span style=\"font-family:Times New Roman\">/</span><span style=\"font-family:宋体\">年。</span><span style=\"font-family:Times New Roman\">(</span><span style=\"font-family:宋体\">本网站打造的是中国最大库存货源信息平台和打造中国最大客户寻找库存货源平台，为了能够更加的服务好每一位商家与客户，为了每一位商家与客户能够找到自己的资源；</span><span style=\"font-family:Times New Roman\">880</span><span style=\"font-family:宋体\">元</span><span style=\"font-family:Times New Roman\">/</span><span style=\"font-family:宋体\">年我们将会竭诚为您服务一年，一年当中无限量的免费提供服务。</span></span></p><p><br/></p>','2015-08-20 12:38:03'),(6,'st_disclaimer','<p>1、本网站所收集的库存信息部分是通过直接工厂商家提供，为了库存信息的及时性，本网站不接受互联网转载库存信息，发布的目的在于信息的真实性及可靠性，但本网站不参与直接交易，也不构成任何其他建议。 本站部分作品是由网友自主投稿和发布、编辑整理上传，对此类作品本站仅提供交流平台，不为其版权负责。如果您发现网站上有侵犯您的知识产权的商品，请与本站取得联系，我们会及时修改或删除。</p><p>2、本网站所提供所有的库存尾货信息，只供参考之用。本网站不保证信息的100%准确性。本网站及其雇员一概毋须以任何方式就任何信息传递或传送的失误、 不准确或错误，对用户或任何其他人士负任何直接或间接责任。在法律允许的范围内，本网站在此声明，不承担用户或任何人士就使用或未能使用本网站所提供的信息或任何链接所引致的任何直接、间接、附带、从属、特殊、惩罚性或惩戒性的损害赔偿。</p><p>3、任何有关本网站和网站声明的争议、纠纷均适用中国法律。您对访问和使用本网站自行承担风险。不保证本网站使用的软件、信息、在线应用程序或者其他任何通过本网站提供的服务无差错或者不间断。我们进行合理的维护以保持本网站上的内容准确且不断更新，也不保证本网站上的任何内容的100%的准确性、及时性和完整性, 并且可能会在不事先通知您的情况下，中断本网站内容的登载。本网站上的信息仅用于提供一般性指导。&nbsp;</p><p>4、本网站可能包含链接到由本站以外的第三方运营的其它网站的链接。此类链接的提供仅在于为您提供便利。本站无法控制其他网站或者发布者，亦不对其他网站或其他网站提供的内容、产品或服务承担任何责任。</p><p>5、本网站不对使用或依赖本网站或者链接到本网站的其他网站上的内容造成的任何直接的、间接的、偶然的和引发性的损失承担任何责任，不论这些内容是否准确或完整。不对使用或依赖本网站或者链接到本网站的其他网站上的任何内容造成的处罚性或继发性的损失或损伤支付任何赔偿。</p><p><br/></p>','2015-08-20 12:39:47');
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
  `Email` varchar(150) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `LoginName_UNIQUE` (`LoginName`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `st_user`
--

LOCK TABLES `st_user` WRITE;
/*!40000 ALTER TABLE `st_user` DISABLE KEYS */;
INSERT INTO `st_user` VALUES (1,'Admin','admin','QL0AFWMIX8NRZTKeof9cXsvbvu8=','2015-08-04 07:57:07',-1,NULL,0,NULL),(2,'Test','test','QL0AFWMIX8NRZTKeof9cXsvbvu8=','2015-08-07 15:43:15',0,NULL,0,NULL),(3,'ff','ff','4MkDWJjdUvxlxBRUzsnE0mEb+zc=','2015-08-10 15:58:09',0,'\0',NULL,'fw'),(4,'flgjt','aaaaa','BMrkFqLB82tMHZ0pqTeReVKyR9c=','2015-09-05 21:57:52',0,NULL,0,'fds@rrf.com');
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

-- Dump completed on 2015-09-10 20:35:31
