-- phpMyAdmin SQL Dump
-- version 4.6.2
-- https://www.phpmyadmin.net/
--
-- Host: localhost
-- Generation Time: Oct 10, 2016 at 02:29 AM
-- Server version: 5.7.13-log
-- PHP Version: 5.6.22

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;

--
-- Database: `huachun`
--

-- --------------------------------------------------------

--
-- Table structure for table `city`
--

CREATE TABLE `city` (
  `PK_` int(4) NOT NULL,
  `sort` int(2) NOT NULL,
  `city` varchar(5) COLLATE utf8_unicode_ci NOT NULL,
  `district` varchar(10) COLLATE utf8_unicode_ci NOT NULL,
  `code` int(3) NOT NULL
) ENGINE=MyISAM DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

--
-- Dumping data for table `city`
--

INSERT INTO `city` (`PK_`, `sort`, `city`, `district`, `code`) VALUES
(1, 1, '基隆市', '仁愛區', 200),
(2, 1, '基隆市', '中正區', 202),
(3, 1, '基隆市', '信義區', 201),
(4, 1, '基隆市', '中山區', 203),
(5, 1, '基隆市', '安樂區', 204),
(6, 1, '基隆市', '暖暖區', 205),
(7, 1, '基隆市', '七堵區', 206),
(8, 2, '台北市', '中正區', 100),
(9, 2, '台北市', '大同區', 103),
(10, 2, '台北市', '中山區', 104),
(11, 2, '台北市', '松山區', 105),
(12, 2, '台北市', '大安區', 106),
(13, 2, '台北市', '萬華區', 108),
(14, 2, '台北市', '信義區', 110),
(15, 2, '台北市', '士林區', 111),
(16, 2, '台北市', '北投區', 112),
(17, 2, '台北市', '內湖區', 114),
(18, 2, '台北市', '南港區', 115),
(19, 2, '台北市', '文山區', 116),
(20, 3, '新北市', '板橋區', 220),
(21, 3, '新北市', '新莊區', 242),
(22, 3, '新北市', '中和區', 235),
(23, 3, '新北市', '永和區', 234),
(24, 3, '新北市', '土城區', 236),
(25, 3, '新北市', '樹林區', 238),
(26, 3, '新北市', '三峽區', 237),
(27, 3, '新北市', '鶯歌區', 239),
(28, 3, '新北市', '三重區', 241),
(29, 3, '新北市', '蘆洲區', 247),
(30, 3, '新北市', '五股區', 248),
(31, 3, '新北市', '泰山區', 243),
(32, 3, '新北市', '林口區', 244),
(33, 3, '新北市', '淡水區', 251),
(34, 3, '新北市', '金山區', 208),
(35, 3, '新北市', '八里區', 249),
(36, 3, '新北市', '萬里區', 207),
(37, 3, '新北市', '石門區', 253),
(38, 3, '新北市', '三芝區', 252),
(39, 3, '新北市', '瑞芳區', 224),
(40, 3, '新北市', '汐止區', 221),
(41, 3, '新北市', '平溪區', 226),
(42, 3, '新北市', '貢寮區', 228),
(43, 3, '新北市', '雙溪區', 227),
(44, 3, '新北市', '深坑區', 222),
(45, 3, '新北市', '石碇區', 223),
(46, 3, '新北市', '新店區', 231),
(47, 3, '新北市', '坪林區', 232),
(48, 3, '新北市', '烏來區', 233),
(49, 4, '桃園市', '桃園區', 330),
(50, 4, '桃園市', '中壢區', 320),
(51, 4, '桃園市', '平鎮區', 324),
(52, 4, '桃園市', '八德區', 334),
(53, 4, '桃園市', '楊梅區', 326),
(54, 4, '桃園市', '蘆竹區', 338),
(55, 4, '桃園市', '大溪區', 335),
(56, 4, '桃園市', '龍潭區', 325),
(57, 4, '桃園市', '龜山區', 333),
(58, 4, '桃園市', '大園區', 337),
(59, 4, '桃園市', '觀音區', 328),
(60, 4, '桃園市', '新屋區', 327),
(61, 4, '桃園市', '復興區', 336),
(62, 5, '新竹市', '東區', 300),
(63, 5, '新竹市', '北區', 300),
(64, 5, '新竹市', '香山區', 300),
(65, 6, '新竹縣', '竹北市', 302),
(66, 6, '新竹縣', '竹東鎮', 310),
(67, 6, '新竹縣', '新埔鎮', 305),
(68, 6, '新竹縣', '關西鎮', 306),
(69, 6, '新竹縣', '湖口鄉', 303),
(70, 6, '新竹縣', '新豐鄉', 304),
(71, 6, '新竹縣', '峨眉鄉', 315),
(72, 6, '新竹縣', '寶山鄉', 308),
(73, 6, '新竹縣', '北埔鄉', 314),
(74, 6, '新竹縣', '芎林鄉', 307),
(75, 6, '新竹縣', '橫山鄉', 312),
(76, 6, '新竹縣', '尖石鄉', 313),
(77, 6, '新竹縣', '五峰鄉', 311),
(78, 7, '苗栗縣', '苗栗市', 360),
(79, 7, '苗栗縣', '造橋鄉', 361),
(80, 7, '苗栗縣', '西湖鄉', 368),
(81, 7, '苗栗縣', '頭屋鄉', 362),
(82, 7, '苗栗縣', '公館鄉', 363),
(83, 7, '苗栗縣', '銅鑼鄉', 366),
(84, 7, '苗栗縣', '三義鄉', 367),
(85, 7, '苗栗縣', '大湖鄉', 364),
(86, 7, '苗栗縣', '獅潭鄉', 354),
(87, 7, '苗栗縣', '卓蘭鎮', 369),
(88, 7, '苗栗縣', '竹南鎮', 350),
(89, 7, '苗栗縣', '頭份市', 351),
(90, 7, '苗栗縣', '三灣鄉', 352),
(91, 7, '苗栗縣', '南庄鄉', 353),
(92, 7, '苗栗縣', '後龍鎮', 356),
(93, 7, '苗栗縣', '通霄鎮', 357),
(94, 7, '苗栗縣', '苑裡鎮', 358),
(95, 7, '苗栗縣', '泰安鄉', 365),
(96, 8, '台中市', '中區', 400),
(97, 8, '台中市', '東區', 401),
(98, 8, '台中市', '南區', 402),
(99, 8, '台中市', '西區', 403),
(100, 8, '台中市', '北區', 404),
(101, 8, '台中市', '北屯區', 406),
(102, 8, '台中市', '西屯區', 407),
(103, 8, '台中市', '南屯區', 408),
(104, 8, '台中市', '太平區', 411),
(105, 8, '台中市', '大里區', 412),
(106, 8, '台中市', '霧峰區', 413),
(107, 8, '台中市', '烏日區', 414),
(108, 8, '台中市', '豐原區', 420),
(109, 8, '台中市', '后里區', 421),
(110, 8, '台中市', '石岡區', 422),
(111, 8, '台中市', '東勢區', 423),
(112, 8, '台中市', '和平區', 424),
(113, 8, '台中市', '新社區', 426),
(114, 8, '台中市', '潭子區', 427),
(115, 8, '台中市', '大雅區', 428),
(116, 8, '台中市', '神岡區', 429),
(117, 8, '台中市', '大肚區', 432),
(118, 8, '台中市', '沙鹿區', 433),
(119, 8, '台中市', '龍井區', 434),
(120, 8, '台中市', '梧棲區', 435),
(121, 8, '台中市', '清水區', 436),
(122, 8, '台中市', '大甲區', 437),
(123, 8, '台中市', '外埔區', 438),
(124, 8, '台中市', '大安區', 439),
(125, 9, '彰化縣', '彰化市', 500),
(126, 9, '彰化縣', '員林市', 510),
(127, 9, '彰化縣', '和美鎮', 508),
(128, 9, '彰化縣', '鹿港鎮', 505),
(129, 9, '彰化縣', '溪湖鎮', 514),
(130, 9, '彰化縣', '二林鎮', 526),
(131, 9, '彰化縣', '田中鎮', 520),
(132, 9, '彰化縣', '北斗鎮', 521),
(133, 9, '彰化縣', '花壇鄉', 503),
(134, 9, '彰化縣', '芬園鄉', 502),
(135, 9, '彰化縣', '大村鄉', 515),
(136, 9, '彰化縣', '永靖鄉', 512),
(137, 9, '彰化縣', '伸港鄉', 509),
(138, 9, '彰化縣', '線西鄉', 507),
(139, 9, '彰化縣', '福興鄉', 506),
(140, 9, '彰化縣', '秀水鄉', 504),
(141, 9, '彰化縣', '埔心鄉', 513),
(142, 9, '彰化縣', '埔鹽鄉', 516),
(143, 9, '彰化縣', '大城鄉', 527),
(144, 9, '彰化縣', '芳苑鄉', 528),
(145, 9, '彰化縣', '竹塘鄉', 525),
(146, 9, '彰化縣', '社頭鄉', 511),
(147, 9, '彰化縣', '二水鄉', 530),
(148, 9, '彰化縣', '田尾鄉', 522),
(149, 9, '彰化縣', '埤頭鄉', 523),
(150, 9, '彰化縣', '溪州鄉', 524),
(151, 10, '南投縣', '南投市', 540),
(152, 10, '南投縣', '埔里鎮', 545),
(153, 10, '南投縣', '草屯鎮', 542),
(154, 10, '南投縣', '竹山鎮', 557),
(155, 10, '南投縣', '集集鎮', 552),
(156, 10, '南投縣', '名間鄉', 551),
(157, 10, '南投縣', '鹿谷鄉', 558),
(158, 10, '南投縣', '中寮鄉', 541),
(159, 10, '南投縣', '魚池鄉', 555),
(160, 10, '南投縣', '國姓鄉', 544),
(161, 10, '南投縣', '水里鄉', 553),
(162, 10, '南投縣', '信義鄉', 556),
(163, 10, '南投縣', '仁愛鄉', 546),
(164, 11, '雲林縣', '斗六市', 640),
(165, 11, '雲林縣', '斗南鎮', 630),
(166, 11, '雲林縣', '林內鄉', 643),
(167, 11, '雲林縣', '古坑鄉', 646),
(168, 11, '雲林縣', '大埤鄉', 631),
(169, 11, '雲林縣', '莿桐鄉', 647),
(170, 11, '雲林縣', '虎尾鎮', 632),
(171, 11, '雲林縣', '西螺鎮', 648),
(172, 11, '雲林縣', '土庫鎮', 633),
(173, 11, '雲林縣', '褒忠鄉', 634),
(174, 11, '雲林縣', '二崙鄉', 649),
(175, 11, '雲林縣', '崙背鄉', 637),
(176, 11, '雲林縣', '麥寮鄉', 638),
(177, 11, '雲林縣', '臺西鄉', 636),
(178, 11, '雲林縣', '東勢鄉', 635),
(179, 11, '雲林縣', '北港鎮', 651),
(180, 11, '雲林縣', '元長鄉', 655),
(181, 11, '雲林縣', '四湖鄉', 654),
(182, 11, '雲林縣', '口湖鄉', 653),
(183, 11, '雲林縣', '水林鄉', 652),
(184, 12, '嘉義市', '東區', 600),
(185, 12, '嘉義市', '西區', 600),
(186, 13, '嘉義縣', '太保市', 612),
(187, 13, '嘉義縣', '朴子市', 613),
(188, 13, '嘉義縣', '布袋鎮', 625),
(189, 13, '嘉義縣', '大林鎮', 622),
(190, 13, '嘉義縣', '民雄鄉', 621),
(191, 13, '嘉義縣', '溪口鄉', 623),
(192, 13, '嘉義縣', '新港鄉', 616),
(193, 13, '嘉義縣', '六腳鄉', 615),
(194, 13, '嘉義縣', '東石鄉', 614),
(195, 13, '嘉義縣', '義竹鄉', 624),
(196, 13, '嘉義縣', '鹿草鄉', 611),
(197, 13, '嘉義縣', '水上鄉', 608),
(198, 13, '嘉義縣', '中埔鄉', 606),
(199, 13, '嘉義縣', '竹崎鄉', 604),
(200, 13, '嘉義縣', '梅山鄉', 603),
(201, 13, '嘉義縣', '番路鄉', 602),
(202, 13, '嘉義縣', '大埔鄉', 607),
(203, 13, '嘉義縣', '阿里山鄉', 605),
(204, 14, '台南市', '中西區', 700),
(205, 14, '台南市', '東區', 701),
(206, 14, '台南市', '南區', 702),
(207, 14, '台南市', '北區', 704),
(208, 14, '台南市', '安平區', 708),
(209, 14, '台南市', '安南區', 709),
(210, 14, '台南市', '永康區', 710),
(211, 14, '台南市', '歸仁區', 711),
(212, 14, '台南市', '新化區', 712),
(213, 14, '台南市', '左鎮區', 713),
(214, 14, '台南市', '玉井區', 714),
(215, 14, '台南市', '楠西區', 715),
(216, 14, '台南市', '南化區', 716),
(217, 14, '台南市', '仁德區', 717),
(218, 14, '台南市', '關廟區', 718),
(219, 14, '台南市', '龍崎區', 719),
(220, 14, '台南市', '官田區', 720),
(221, 14, '台南市', '麻豆區', 721),
(222, 14, '台南市', '佳里區', 722),
(223, 14, '台南市', '西港區', 723),
(224, 14, '台南市', '七股區', 724),
(225, 14, '台南市', '將軍區', 725),
(226, 14, '台南市', '學甲區', 726),
(227, 14, '台南市', '北門區', 727),
(228, 14, '台南市', '新營區', 730),
(229, 14, '台南市', '後壁區', 731),
(230, 14, '台南市', '白河區', 732),
(231, 14, '台南市', '東山區', 733),
(232, 14, '台南市', '六甲區', 734),
(233, 14, '台南市', '下營區', 735),
(234, 14, '台南市', '柳營區', 736),
(235, 14, '台南市', '鹽水區', 737),
(236, 14, '台南市', '善化區', 741),
(237, 14, '台南市', '大內區', 742),
(238, 14, '台南市', '山上區', 743),
(239, 14, '台南市', '新市區', 744),
(240, 14, '台南市', '安定區', 745),
(241, 15, '高雄市', '楠梓區', 811),
(242, 15, '高雄市', '左營區', 813),
(243, 15, '高雄市', '鼓山區', 804),
(244, 15, '高雄市', '三民區', 807),
(245, 15, '高雄市', '鹽埕區', 803),
(246, 15, '高雄市', '前金區', 801),
(247, 15, '高雄市', '新興區', 800),
(248, 15, '高雄市', '苓雅區', 802),
(249, 15, '高雄市', '前鎮區', 806),
(250, 15, '高雄市', '旗津區', 805),
(251, 15, '高雄市', '小港區', 812),
(252, 15, '高雄市', '鳳山區', 830),
(253, 15, '高雄市', '大寮區', 831),
(254, 15, '高雄市', '鳥松區', 833),
(255, 15, '高雄市', '林園區', 832),
(256, 15, '高雄市', '仁武區', 814),
(257, 15, '高雄市', '大樹區', 840),
(258, 15, '高雄市', '大社區', 815),
(259, 15, '高雄市', '岡山區', 820),
(260, 15, '高雄市', '路竹區', 821),
(261, 15, '高雄市', '橋頭區', 825),
(262, 15, '高雄市', '梓官區', 826),
(263, 15, '高雄市', '彌陀區', 827),
(264, 15, '高雄市', '永安區', 828),
(265, 15, '高雄市', '燕巢區', 824),
(266, 15, '高雄市', '田寮區', 823),
(267, 15, '高雄市', '阿蓮區', 822),
(268, 15, '高雄市', '茄萣區', 852),
(269, 15, '高雄市', '湖內區', 829),
(270, 15, '高雄市', '旗山區', 842),
(271, 15, '高雄市', '美濃區', 843),
(272, 15, '高雄市', '內門區', 845),
(273, 15, '高雄市', '杉林區', 846),
(274, 15, '高雄市', '甲仙區', 847),
(275, 15, '高雄市', '六龜區', 844),
(276, 15, '高雄市', '茂林區', 851),
(277, 15, '高雄市', '桃源區', 848),
(278, 15, '高雄市', '那瑪夏區', 849),
(279, 16, '屏東縣', '屏東市', 900),
(280, 16, '屏東縣', '潮州鎮', 920),
(281, 16, '屏東縣', '東港鎮', 928),
(282, 16, '屏東縣', '恆春鎮', 946),
(283, 16, '屏東縣', '萬丹鄉', 913),
(284, 16, '屏東縣', '崁頂鄉', 924),
(285, 16, '屏東縣', '新園鄉', 932),
(286, 16, '屏東縣', '林邊鄉', 927),
(287, 16, '屏東縣', '南州鄉', 926),
(288, 16, '屏東縣', '琉球鄉', 929),
(289, 16, '屏東縣', '枋寮鄉', 940),
(290, 16, '屏東縣', '枋山鄉', 941),
(291, 16, '屏東縣', '車城鄉', 944),
(292, 16, '屏東縣', '滿州鄉', 947),
(293, 16, '屏東縣', '高樹鄉', 906),
(294, 16, '屏東縣', '九如鄉', 904),
(295, 16, '屏東縣', '鹽埔鄉', 907),
(296, 16, '屏東縣', '里港鄉', 905),
(297, 16, '屏東縣', '內埔鄉', 912),
(298, 16, '屏東縣', '竹田鄉', 911),
(299, 16, '屏東縣', '長治鄉', 908),
(300, 16, '屏東縣', '麟洛鄉', 909),
(301, 16, '屏東縣', '萬巒鄉', 923),
(302, 16, '屏東縣', '新埤鄉', 925),
(303, 16, '屏東縣', '佳冬鄉', 931),
(304, 16, '屏東縣', '霧台鄉', 902),
(305, 16, '屏東縣', '泰武鄉', 921),
(306, 16, '屏東縣', '瑪家鄉', 903),
(307, 16, '屏東縣', '來義鄉', 922),
(308, 16, '屏東縣', '春日鄉', 942),
(309, 16, '屏東縣', '獅子鄉', 943),
(310, 16, '屏東縣', '牡丹鄉', 945),
(311, 16, '屏東縣', '三地門鄉', 901),
(312, 17, '台東縣', '臺東市', 950),
(313, 17, '台東縣', '成功鎮', 961),
(314, 17, '台東縣', '關山鎮', 956),
(315, 17, '台東縣', '長濱鄉', 962),
(316, 17, '台東縣', '池上鄉', 958),
(317, 17, '台東縣', '東河鄉', 959),
(318, 17, '台東縣', '鹿野鄉', 955),
(319, 17, '台東縣', '卑南鄉', 954),
(320, 17, '台東縣', '大武鄉', 965),
(321, 17, '台東縣', '綠島鄉', 951),
(322, 17, '台東縣', '太麻里鄉', 963),
(323, 17, '台東縣', '海端鄉', 957),
(324, 17, '台東縣', '延平鄉', 953),
(325, 17, '台東縣', '金峰鄉', 964),
(326, 17, '台東縣', '達仁鄉', 966),
(327, 17, '台東縣', '蘭嶼鄉', 952),
(328, 18, '花蓮縣', '花蓮市', 970),
(329, 18, '花蓮縣', '鳳林鎮', 975),
(330, 18, '花蓮縣', '玉里鎮', 981),
(331, 18, '花蓮縣', '新城鄉', 971),
(332, 18, '花蓮縣', '吉安鄉', 973),
(333, 18, '花蓮縣', '壽豐鄉', 974),
(334, 18, '花蓮縣', '光復鄉', 976),
(335, 18, '花蓮縣', '豐濱鄉', 977),
(336, 18, '花蓮縣', '瑞穗鄉', 978),
(337, 18, '花蓮縣', '富里鄉', 983),
(338, 18, '花蓮縣', '秀林鄉', 972),
(339, 18, '花蓮縣', '萬榮鄉', 979),
(340, 18, '花蓮縣', '卓溪鄉', 982),
(341, 19, '宜蘭縣', '宜蘭市', 260),
(342, 19, '宜蘭縣', '頭城鎮', 261),
(343, 19, '宜蘭縣', '礁溪鄉', 262),
(344, 19, '宜蘭縣', '壯圍鄉', 263),
(345, 19, '宜蘭縣', '員山鄉', 264),
(346, 19, '宜蘭縣', '羅東鎮', 265),
(347, 19, '宜蘭縣', '蘇澳鎮', 270),
(348, 19, '宜蘭縣', '五結鄉', 268),
(349, 19, '宜蘭縣', '三星鄉', 266),
(350, 19, '宜蘭縣', '冬山鄉', 269),
(351, 19, '宜蘭縣', '大同鄉', 267),
(352, 19, '宜蘭縣', '南澳鄉', 272),
(353, 20, '澎湖縣', '馬公市', 880),
(354, 20, '澎湖縣', '湖西鄉', 885),
(355, 20, '澎湖縣', '白沙鄉', 884),
(356, 20, '澎湖縣', '西嶼鄉', 881),
(357, 20, '澎湖縣', '望安鄉', 882),
(358, 20, '澎湖縣', '七美鄉', 883),
(359, 21, '金門縣', '金城鎮', 893),
(360, 21, '金門縣', '金湖鎮', 891),
(361, 21, '金門縣', '金沙鎮', 890),
(362, 21, '金門縣', '金寧鄉', 892),
(363, 21, '金門縣', '烈嶼鄉', 894),
(364, 21, '金門縣', '烏坵鄉', 896),
(365, 22, '連江縣', '南竿鄉', 209),
(366, 22, '連江縣', '北竿鄉', 210),
(367, 22, '連江縣', '莒光鄉', 211),
(368, 22, '連江縣', '東引鄉', 212);

-- --------------------------------------------------------

--
-- Table structure for table `dailyreport`
--

CREATE TABLE `dailyreport` (
  `PK_` int(6) NOT NULL,
  `project_no` varchar(10) COLLATE utf8_unicode_ci NOT NULL,
  `date` date NOT NULL,
  `morning_weather` varchar(10) COLLATE utf8_unicode_ci NOT NULL,
  `afternoon_weather` varchar(10) COLLATE utf8_unicode_ci NOT NULL,
  `interference` varchar(10) COLLATE utf8_unicode_ci NOT NULL,
  `morning_condition` varchar(10) COLLATE utf8_unicode_ci NOT NULL,
  `afternoon_condition` varchar(10) COLLATE utf8_unicode_ci NOT NULL,
  `nonecounting` varchar(10) COLLATE utf8_unicode_ci NOT NULL
) ENGINE=MyISAM DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

--
-- Dumping data for table `dailyreport`
--

INSERT INTO `dailyreport` (`PK_`, `project_no`, `date`, `morning_weather`, `afternoon_weather`, `interference`, `morning_condition`, `afternoon_condition`, `nonecounting`) VALUES
(22, 'A01', '2016-10-11', '雨', '晴', '', '無', '無', '1');

-- --------------------------------------------------------

--
-- Table structure for table `dailyreport_manpower`
--

CREATE TABLE `dailyreport_manpower` (
  `PK_` int(7) NOT NULL,
  `project_no` varchar(10) COLLATE utf8_unicode_ci NOT NULL,
  `date` varchar(20) COLLATE utf8_unicode_ci NOT NULL,
  `data_index` int(5) NOT NULL,
  `vendor_no` varchar(10) COLLATE utf8_unicode_ci NOT NULL,
  `vendor_name` varchar(20) COLLATE utf8_unicode_ci NOT NULL,
  `manpower_no` varchar(10) COLLATE utf8_unicode_ci NOT NULL,
  `manpower_name` varchar(10) COLLATE utf8_unicode_ci NOT NULL,
  `amount` varchar(10) COLLATE utf8_unicode_ci NOT NULL,
  `hour` varchar(10) COLLATE utf8_unicode_ci NOT NULL,
  `amount_today` varchar(10) COLLATE utf8_unicode_ci NOT NULL,
  `ps` varchar(10) COLLATE utf8_unicode_ci NOT NULL
) ENGINE=MyISAM DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

--
-- Dumping data for table `dailyreport_manpower`
--

INSERT INTO `dailyreport_manpower` (`PK_`, `project_no`, `date`, `data_index`, `vendor_no`, `vendor_name`, `manpower_no`, `manpower_name`, `amount`, `hour`, `amount_today`, `ps`) VALUES
(1, '', '2016-4-22', 0, '12345', 'test', '001', '泥水工3', '', '', '', '');

-- --------------------------------------------------------

--
-- Table structure for table `dailyreport_material`
--

CREATE TABLE `dailyreport_material` (
  `PK_` int(7) NOT NULL,
  `project_no` varchar(20) COLLATE utf8_unicode_ci NOT NULL,
  `date` varchar(20) COLLATE utf8_unicode_ci NOT NULL,
  `data_index` int(20) NOT NULL,
  `vendor_no` varchar(20) COLLATE utf8_unicode_ci NOT NULL,
  `vendor_name` varchar(20) COLLATE utf8_unicode_ci NOT NULL,
  `material_no` varchar(20) COLLATE utf8_unicode_ci NOT NULL,
  `material_name` varchar(20) COLLATE utf8_unicode_ci NOT NULL,
  `unit` varchar(20) COLLATE utf8_unicode_ci NOT NULL,
  `location` varchar(20) COLLATE utf8_unicode_ci NOT NULL,
  `amount_past` varchar(20) COLLATE utf8_unicode_ci NOT NULL,
  `amount_today` varchar(20) COLLATE utf8_unicode_ci NOT NULL,
  `amount_all` varchar(20) COLLATE utf8_unicode_ci NOT NULL,
  `used_past` varchar(20) COLLATE utf8_unicode_ci NOT NULL,
  `used_today` varchar(20) COLLATE utf8_unicode_ci NOT NULL,
  `used_all` varchar(20) COLLATE utf8_unicode_ci NOT NULL,
  `storage` varchar(20) COLLATE utf8_unicode_ci NOT NULL
) ENGINE=MyISAM DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

--
-- Dumping data for table `dailyreport_material`
--

INSERT INTO `dailyreport_material` (`PK_`, `project_no`, `date`, `data_index`, `vendor_no`, `vendor_name`, `material_no`, `material_name`, `unit`, `location`, `amount_past`, `amount_today`, `amount_all`, `used_past`, `used_today`, `used_all`, `storage`) VALUES
(1, 'w04', '2016-5-16', 0, '6002', '台灣積體電路製造', '001', '平面鋼筋', 'KG', '', '', '', '', '', '', '', ''),
(2, 'w04', '2016-5-8', 0, '6002', '台灣積體電路製造', '001', '平面鋼筋', 'KG', '', '', '1', '', '', '1', '', ''),
(3, 'w04', '2016-5-8', 1, '1001', '123', '005', '水泥', 'KG', '', '', '2', '', '', '2', '', '');

-- --------------------------------------------------------

--
-- Table structure for table `dailyreport_outsourcing`
--

CREATE TABLE `dailyreport_outsourcing` (
  `PK_` int(7) NOT NULL,
  `project_no` varchar(10) COLLATE utf8_unicode_ci NOT NULL,
  `date` varchar(20) COLLATE utf8_unicode_ci NOT NULL,
  `data_index` int(5) NOT NULL,
  `vendor_no` varchar(10) COLLATE utf8_unicode_ci NOT NULL,
  `vendor_name` varchar(20) COLLATE utf8_unicode_ci NOT NULL,
  `process_no` varchar(10) COLLATE utf8_unicode_ci NOT NULL,
  `process_name` varchar(20) COLLATE utf8_unicode_ci NOT NULL,
  `unit` varchar(10) COLLATE utf8_unicode_ci NOT NULL,
  `dispatch_past` varchar(10) COLLATE utf8_unicode_ci NOT NULL,
  `dispatch_today` varchar(10) COLLATE utf8_unicode_ci NOT NULL,
  `dispatch_all` varchar(10) COLLATE utf8_unicode_ci NOT NULL,
  `build_past` varchar(10) COLLATE utf8_unicode_ci NOT NULL,
  `build_today` varchar(10) COLLATE utf8_unicode_ci NOT NULL,
  `build_all` varchar(10) COLLATE utf8_unicode_ci NOT NULL,
  `ps` varchar(10) COLLATE utf8_unicode_ci NOT NULL
) ENGINE=MyISAM DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- --------------------------------------------------------

--
-- Table structure for table `dailyreport_tool`
--

CREATE TABLE `dailyreport_tool` (
  `PK_` int(7) NOT NULL,
  `project_no` varchar(10) COLLATE utf8_unicode_ci NOT NULL,
  `date` varchar(20) COLLATE utf8_unicode_ci NOT NULL,
  `data_index` int(5) NOT NULL,
  `vendor_no` varchar(10) COLLATE utf8_unicode_ci NOT NULL,
  `vendor_name` varchar(20) COLLATE utf8_unicode_ci NOT NULL,
  `tool_no` varchar(10) COLLATE utf8_unicode_ci NOT NULL,
  `tool_name` varchar(10) COLLATE utf8_unicode_ci NOT NULL,
  `amount` varchar(10) COLLATE utf8_unicode_ci NOT NULL,
  `hour` varchar(10) COLLATE utf8_unicode_ci NOT NULL,
  `amount_today` varchar(10) COLLATE utf8_unicode_ci NOT NULL,
  `ps` varchar(10) COLLATE utf8_unicode_ci NOT NULL
) ENGINE=MyISAM DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- --------------------------------------------------------

--
-- Table structure for table `dailyreport_vacation`
--

CREATE TABLE `dailyreport_vacation` (
  `PK_` int(7) NOT NULL,
  `project_no` varchar(20) COLLATE utf8_unicode_ci NOT NULL,
  `date` date NOT NULL,
  `data_index` int(10) NOT NULL,
  `employee_no` varchar(20) COLLATE utf8_unicode_ci NOT NULL,
  `employee_name` varchar(20) COLLATE utf8_unicode_ci NOT NULL,
  `days` varchar(20) COLLATE utf8_unicode_ci NOT NULL,
  `ps` varchar(20) COLLATE utf8_unicode_ci NOT NULL
) ENGINE=MyISAM DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- --------------------------------------------------------

--
-- Table structure for table `extendduration`
--

CREATE TABLE `extendduration` (
  `PK_` int(5) NOT NULL,
  `project_no` varchar(40) COLLATE utf8_unicode_ci NOT NULL,
  `grantdate` date NOT NULL,
  `grantnumber` varchar(20) COLLATE utf8_unicode_ci NOT NULL,
  `extendvalue` varchar(20) COLLATE utf8_unicode_ci NOT NULL,
  `extendstartdate` date NOT NULL,
  `extendduration` varchar(20) COLLATE utf8_unicode_ci NOT NULL,
  `writedate` date NOT NULL
) ENGINE=MyISAM DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

--
-- Dumping data for table `extendduration`
--

INSERT INTO `extendduration` (`PK_`, `project_no`, `grantdate`, `grantnumber`, `extendvalue`, `extendstartdate`, `extendduration`, `writedate`) VALUES
(4, 'A01', '2016-10-20', 'B01', '1000', '2016-10-21', '9', '2016-10-08');

-- --------------------------------------------------------

--
-- Table structure for table `holiday`
--

CREATE TABLE `holiday` (
  `PK_` int(5) NOT NULL,
  `date` date NOT NULL,
  `reason` varchar(20) COLLATE utf8_unicode_ci NOT NULL,
  `working` int(1) NOT NULL
) ENGINE=MyISAM DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

--
-- Dumping data for table `holiday`
--

INSERT INTO `holiday` (`PK_`, `date`, `reason`, `working`) VALUES
(1, '2016-01-01', '元旦連假', 1),
(2, '2016-01-02', '元旦連假', 1),
(3, '2016-01-03', '元旦連假', 1),
(4, '2016-02-06', '春節', 1),
(5, '2016-02-07', '春節', 1),
(6, '2016-02-08', '春節', 1),
(7, '2016-02-09', '春節', 1),
(8, '2016-02-10', '春節', 1),
(9, '2016-02-11', '春節', 1),
(10, '2016-02-12', '春節', 1),
(11, '2016-02-13', '春節', 1),
(12, '2016-02-14', '春節', 1),
(13, '2016-02-29', '二二八補假', 1),
(14, '2016-04-02', '清明', 1),
(15, '2016-04-03', '清明', 1),
(16, '2016-04-04', '清明', 1),
(17, '2016-04-05', '清明', 1),
(18, '2016-06-09', '端午節', 1),
(19, '2016-06-10', '端午節', 1),
(20, '2016-06-04', '端午補班', 2),
(21, '2016-09-15', '中秋節', 1),
(22, '2016-09-16', '中秋節', 1),
(23, '2016-09-10', '中秋補班', 2),
(24, '2016-10-10', '國慶日', 1),
(25, '2016-06-11', '端午節', 1),
(26, '2013-04-04', '清明', 1),
(27, '2013-04-05', '清明', 1),
(28, '2013-05-01', '勞動節', 1),
(29, '2013-06-12', '端午節', 1),
(30, '2013-09-19', '中秋', 1),
(31, '2013-09-20', '中秋', 1),
(32, '2013-10-10', '國慶', 1),
(33, '2014-01-01', '元旦', 1),
(34, '2014-01-30', '春節', 1),
(35, '2014-01-31', '春節', 1),
(36, '2014-02-03', '春節', 1),
(37, '2014-02-04', '春節', 1),
(38, '2014-02-28', '228', 1),
(39, '2016-10-25', '光復節', 1),
(40, '2016-10-31', '殺人魔破蛋', 1),
(41, '2017-01-02', '元旦補假', 1),
(42, '2017-01-27', '春節', 1),
(43, '2017-01-28', '春節', 1),
(44, '2017-01-29', '春節', 1),
(45, '2017-01-30', '春節', 1),
(46, '2017-01-31', '春節', 1),
(47, '2017-02-01', '春節', 1),
(48, '2017-02-27', '228', 1),
(49, '2017-02-28', '228', 1),
(50, '2017-02-18', '228連假補班', 2),
(51, '2017-04-03', '清明', 1),
(52, '2017-04-04', '清明', 1),
(53, '2017-05-29', '端午', 1),
(54, '2017-05-30', '端午', 1),
(55, '2017-05-20', '端午補班', 2),
(56, '2017-10-09', '國慶', 1),
(57, '2017-10-10', '國慶', 1),
(58, '2017-09-30', '國慶補班', 2),
(59, '2017-05-01', '勞動節', 1),
(60, '2017-10-04', '中秋', 1);

-- --------------------------------------------------------

--
-- Table structure for table `labor`
--

CREATE TABLE `labor` (
  `PK_` int(5) NOT NULL,
  `number` varchar(10) CHARACTER SET utf8 NOT NULL,
  `name` varchar(40) COLLATE utf8_unicode_ci NOT NULL
) ENGINE=MyISAM DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

--
-- Dumping data for table `labor`
--

INSERT INTO `labor` (`PK_`, `number`, `name`) VALUES
(1, '001', '泥水工3'),
(2, '002', '板模工');

-- --------------------------------------------------------

--
-- Table structure for table `material`
--

CREATE TABLE `material` (
  `PK_` int(5) NOT NULL,
  `number` varchar(10) CHARACTER SET utf8 NOT NULL,
  `name` varchar(20) COLLATE utf8_unicode_ci NOT NULL,
  `unit` varchar(10) COLLATE utf8_unicode_ci NOT NULL
) ENGINE=MyISAM DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

--
-- Dumping data for table `material`
--

INSERT INTO `material` (`PK_`, `number`, `name`, `unit`) VALUES
(1, '001', '平面鋼筋', 'KG'),
(2, '002', '一般鋼筋', 'KG'),
(3, '003', '水泥', 'KG'),
(5, '005', 'test', 'test'),
(6, '006', '模板', '材'),
(7, '004', '木板', '條');

-- --------------------------------------------------------

--
-- Table structure for table `member`
--

CREATE TABLE `member` (
  `PK_` int(4) NOT NULL,
  `account` varchar(20) COLLATE utf8_unicode_ci NOT NULL,
  `name` varchar(10) CHARACTER SET utf8 NOT NULL,
  `password` varchar(10) CHARACTER SET utf8 NOT NULL,
  `level` int(10) NOT NULL,
  `dayoff` int(2) NOT NULL,
  `id` varchar(10) COLLATE utf8_unicode_ci NOT NULL,
  `sex` int(1) NOT NULL,
  `birthdate` date NOT NULL,
  `degree` varchar(40) COLLATE utf8_unicode_ci NOT NULL,
  `resident_city` varchar(5) COLLATE utf8_unicode_ci NOT NULL,
  `resident_district` varchar(5) COLLATE utf8_unicode_ci NOT NULL,
  `resident_address` varchar(40) COLLATE utf8_unicode_ci NOT NULL,
  `living_city` varchar(5) COLLATE utf8_unicode_ci NOT NULL,
  `living_district` varchar(5) COLLATE utf8_unicode_ci NOT NULL,
  `living_address` varchar(40) COLLATE utf8_unicode_ci NOT NULL,
  `phone` varchar(15) COLLATE utf8_unicode_ci NOT NULL,
  `cell` varchar(15) COLLATE utf8_unicode_ci NOT NULL,
  `startdate` date NOT NULL,
  `insurancedate` date NOT NULL,
  `enddate` date NOT NULL,
  `position` varchar(20) COLLATE utf8_unicode_ci NOT NULL,
  `serviceyear` float NOT NULL,
  `relative` int(3) NOT NULL,
  `bank_name` varchar(20) COLLATE utf8_unicode_ci NOT NULL,
  `bank_account` varchar(20) COLLATE utf8_unicode_ci NOT NULL,
  `workingtype` int(1) NOT NULL,
  `onjob` int(1) NOT NULL
) ENGINE=MyISAM DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

--
-- Dumping data for table `member`
--

INSERT INTO `member` (`PK_`, `account`, `name`, `password`, `level`, `dayoff`, `id`, `sex`, `birthdate`, `degree`, `resident_city`, `resident_district`, `resident_address`, `living_city`, `living_district`, `living_address`, `phone`, `cell`, `startdate`, `insurancedate`, `enddate`, `position`, `serviceyear`, `relative`, `bank_name`, `bank_account`, `workingtype`, `onjob`) VALUES
(1, 'foxking', '杜維謙', '12345', 0, 0, 'E123392317', 1, '1983-04-16', '台大機械2', '高雄市', '三民區', '高雄市三民區延慶街', '高雄市', '三民區', '新竹市東區新莊街', '3822974', '0932890731', '2010-12-01', '2013-07-17', '2015-12-20', '工程師', 0, 2, '1234', '5678', 1, 2),
(2, 'foxking2', '杜維誠', '', 0, 0, 'E123392319', 1, '2015-12-20', '台大機械', '', '', '高雄市三民區延慶街', '', '', '新竹市東區新莊街', '3822974', '0932890731', '2015-12-01', '2015-12-20', '2015-12-20', '工程師', 5, 0, '1234', '5678', 1, 2),
(5, 'chichi', '詹雅琪', '', 0, 0, 'A123456789', 2, '2016-10-08', 'Upenn', '彰化縣', '彰化市', '埔市街', '新北市', '新店區', '西園路', '', '0919015342', '2016-10-01', '2016-10-02', '2016-10-03', '經理', 0, 1, '彰化銀行', '111222', 1, 2);

-- --------------------------------------------------------

--
-- Table structure for table `processcode`
--

CREATE TABLE `processcode` (
  `PK_` int(4) NOT NULL,
  `number` varchar(40) COLLATE utf8_unicode_ci NOT NULL,
  `name` varchar(400) COLLATE utf8_unicode_ci NOT NULL,
  `unit` varchar(40) COLLATE utf8_unicode_ci NOT NULL
) ENGINE=MyISAM DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

--
-- Dumping data for table `processcode`
--

INSERT INTO `processcode` (`PK_`, `number`, `name`, `unit`) VALUES
(1, '002', '灌漿', '立方');

-- --------------------------------------------------------

--
-- Table structure for table `project_info`
--

CREATE TABLE `project_info` (
  `PK_` int(4) NOT NULL,
  `project_no` varchar(20) COLLATE utf8_unicode_ci NOT NULL,
  `contract_no` varchar(20) COLLATE utf8_unicode_ci NOT NULL,
  `project_name` varchar(20) COLLATE utf8_unicode_ci NOT NULL,
  `project_location` varchar(20) COLLATE utf8_unicode_ci NOT NULL,
  `contractor` varchar(20) COLLATE utf8_unicode_ci NOT NULL,
  `owner` varchar(20) COLLATE utf8_unicode_ci NOT NULL,
  `manage` varchar(20) COLLATE utf8_unicode_ci NOT NULL,
  `design` varchar(20) COLLATE utf8_unicode_ci NOT NULL,
  `supervise` varchar(20) COLLATE utf8_unicode_ci NOT NULL,
  `responsible` varchar(20) COLLATE utf8_unicode_ci NOT NULL,
  `quality` varchar(20) COLLATE utf8_unicode_ci NOT NULL,
  `biddate` date NOT NULL,
  `startdate` date NOT NULL,
  `contract_finishdate` date NOT NULL,
  `modified_finishdate` date NOT NULL,
  `contractamount` varchar(20) COLLATE utf8_unicode_ci NOT NULL,
  `contractduration` varchar(20) COLLATE utf8_unicode_ci NOT NULL,
  `contractdays` float NOT NULL,
  `handle1` varchar(20) COLLATE utf8_unicode_ci NOT NULL,
  `phone1` varchar(20) COLLATE utf8_unicode_ci NOT NULL,
  `handle2` varchar(20) COLLATE utf8_unicode_ci NOT NULL,
  `phone2` varchar(20) COLLATE utf8_unicode_ci NOT NULL,
  `handle3` varchar(20) COLLATE utf8_unicode_ci NOT NULL,
  `phone3` varchar(20) COLLATE utf8_unicode_ci NOT NULL,
  `handle4` varchar(20) COLLATE utf8_unicode_ci NOT NULL,
  `phone4` varchar(20) COLLATE utf8_unicode_ci NOT NULL,
  `onsite` varchar(20) COLLATE utf8_unicode_ci NOT NULL,
  `security` varchar(20) COLLATE utf8_unicode_ci NOT NULL,
  `computetype` int(2) NOT NULL,
  `holiday` int(1) NOT NULL,
  `rainyday` int(1) NOT NULL,
  `confirm_finishdate` date NOT NULL
) ENGINE=MyISAM DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

--
-- Dumping data for table `project_info`
--

INSERT INTO `project_info` (`PK_`, `project_no`, `contract_no`, `project_name`, `project_location`, `contractor`, `owner`, `manage`, `design`, `supervise`, `responsible`, `quality`, `biddate`, `startdate`, `contract_finishdate`, `modified_finishdate`, `contractamount`, `contractduration`, `contractdays`, `handle1`, `phone1`, `handle2`, `phone2`, `handle3`, `phone3`, `handle4`, `phone4`, `onsite`, `security`, `computetype`, `holiday`, `rainyday`, `confirm_finishdate`) VALUES
(18, 'A01', 'A01', '東尼史塔克的家', '紐約市', '華春營造', '東尼史塔克', '神盾局', '神盾局特工', '鋼鐵人', '黑寡婦', '鷹眼', '2016-10-08', '2016-10-05', '2016-11-04', '2016-11-07', '9,527,000.0000', '20.0', 31, '蟻人', '1234', '索爾', '4321', '蜘蛛人', '5678', '戰爭機器', '8765', '浩克', '美國隊長', 5, 1, 0, '0000-00-00'),
(19, 'A02', 'A02', '蝙蝠俠的巢穴', '高譚市', '華春營造', '布魯斯偉恩', '非正義聯盟', '超人', '神力女超人', '綠光戰警', '閃電俠', '2016-10-09', '2016-10-19', '2016-12-27', '2016-12-27', '3,939,889.0000', '60.0', 70, '火風暴', '', '', '', '', '', '', '', '小丑', '', 4, 0, 0, '0000-00-00');

-- --------------------------------------------------------

--
-- Table structure for table `test`
--

CREATE TABLE `test` (
  `PK_` int(5) NOT NULL,
  `num` int(5) NOT NULL,
  `count` int(5) NOT NULL
) ENGINE=MyISAM DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

--
-- Dumping data for table `test`
--

INSERT INTO `test` (`PK_`, `num`, `count`) VALUES
(1, 1, 2),
(2, 4, 5),
(3, 6, 7);

-- --------------------------------------------------------

--
-- Table structure for table `tool`
--

CREATE TABLE `tool` (
  `PK_` int(4) NOT NULL,
  `number` varchar(5) COLLATE utf8_unicode_ci NOT NULL,
  `name` varchar(20) COLLATE utf8_unicode_ci NOT NULL
) ENGINE=MyISAM DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

--
-- Dumping data for table `tool`
--

INSERT INTO `tool` (`PK_`, `number`, `name`) VALUES
(1, '001', '挖土機'),
(2, '003', '怪手');

-- --------------------------------------------------------

--
-- Table structure for table `vendor`
--

CREATE TABLE `vendor` (
  `PK_` int(4) NOT NULL,
  `vendor_no` varchar(5) COLLATE utf8_unicode_ci NOT NULL,
  `vendor_name` varchar(20) COLLATE utf8_unicode_ci NOT NULL,
  `vendor_abbre` varchar(10) COLLATE utf8_unicode_ci NOT NULL,
  `contact1` varchar(10) COLLATE utf8_unicode_ci NOT NULL,
  `contact2` varchar(10) COLLATE utf8_unicode_ci NOT NULL,
  `phone1` varchar(15) COLLATE utf8_unicode_ci NOT NULL,
  `phone2` varchar(15) COLLATE utf8_unicode_ci NOT NULL,
  `cell` varchar(12) COLLATE utf8_unicode_ci NOT NULL,
  `email` varchar(40) COLLATE utf8_unicode_ci NOT NULL,
  `fax` varchar(15) COLLATE utf8_unicode_ci NOT NULL,
  `code3` varchar(3) COLLATE utf8_unicode_ci NOT NULL,
  `code2` varchar(2) COLLATE utf8_unicode_ci NOT NULL,
  `address_city` varchar(5) COLLATE utf8_unicode_ci NOT NULL,
  `address_district` varchar(10) COLLATE utf8_unicode_ci NOT NULL,
  `address_road` varchar(40) COLLATE utf8_unicode_ci NOT NULL,
  `id` varchar(8) COLLATE utf8_unicode_ci NOT NULL,
  `taxtitle` varchar(15) COLLATE utf8_unicode_ci NOT NULL,
  `businessitems` varchar(200) COLLATE utf8_unicode_ci NOT NULL,
  `other` varchar(400) COLLATE utf8_unicode_ci NOT NULL
) ENGINE=MyISAM DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

--
-- Dumping data for table `vendor`
--

INSERT INTO `vendor` (`PK_`, `vendor_no`, `vendor_name`, `vendor_abbre`, `contact1`, `contact2`, `phone1`, `phone2`, `cell`, `email`, `fax`, `code3`, `code2`, `address_city`, `address_district`, `address_road`, `id`, `taxtitle`, `businessitems`, `other`) VALUES
(1, '6002', '台灣積體電路製造', '台積電', '張忠謀', '郭台銘', '1111111', '123456789', '900000000', '123@123123', '456456456', '893', '2', '金門縣', '金城鎮', '我我我我我我我我我我', '22525199', '215121512', '哈囉 你好', ''),
(2, '1001', '123', '123', 'john', '', '', '', '', '\'', '', '200', '', '基隆市', '仁愛區', '', '', '', '', ''),
(3, '1229', '聯華實業1', '聯華', '苗豐強', '景虎士', '(02)2786-1188', '', '', 'info@lhic.com.tw', '123', '115', '', '台北市', '南港區', '南港路一段209號A棟10F', '11996904', '', '1952 年本公司創辦人苗育秀先生於南港創設聯華麵粉廠，並於1955年改組登記為聯華實業股份有限公司，為聯華神通集團的創始公司，生產的產品包括各種品項的麵粉以及義大利麵，是國內第一家通過GMP食品良好作業規範之麵粉廠，更於2008年通過ISO22000及HACCP國際食品安全管制系統驗證。為擴大發展食品本業，於1995年在煙台成立煙台台華食品實業有限公司；並於2012年設立通路事業體，專職經營餐飲', ''),
(4, '12345', 'test', 'te', '', '', '', '', '', '', '', '200', '', '基隆市', '仁愛區', '', '', '', '', '');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `city`
--
ALTER TABLE `city`
  ADD PRIMARY KEY (`PK_`);

--
-- Indexes for table `dailyreport`
--
ALTER TABLE `dailyreport`
  ADD PRIMARY KEY (`PK_`);

--
-- Indexes for table `dailyreport_manpower`
--
ALTER TABLE `dailyreport_manpower`
  ADD PRIMARY KEY (`PK_`);

--
-- Indexes for table `dailyreport_material`
--
ALTER TABLE `dailyreport_material`
  ADD PRIMARY KEY (`PK_`);

--
-- Indexes for table `dailyreport_outsourcing`
--
ALTER TABLE `dailyreport_outsourcing`
  ADD PRIMARY KEY (`PK_`);

--
-- Indexes for table `dailyreport_tool`
--
ALTER TABLE `dailyreport_tool`
  ADD PRIMARY KEY (`PK_`);

--
-- Indexes for table `dailyreport_vacation`
--
ALTER TABLE `dailyreport_vacation`
  ADD PRIMARY KEY (`PK_`);

--
-- Indexes for table `extendduration`
--
ALTER TABLE `extendduration`
  ADD PRIMARY KEY (`PK_`);

--
-- Indexes for table `holiday`
--
ALTER TABLE `holiday`
  ADD PRIMARY KEY (`PK_`);

--
-- Indexes for table `labor`
--
ALTER TABLE `labor`
  ADD PRIMARY KEY (`PK_`);

--
-- Indexes for table `material`
--
ALTER TABLE `material`
  ADD PRIMARY KEY (`PK_`);

--
-- Indexes for table `member`
--
ALTER TABLE `member`
  ADD PRIMARY KEY (`PK_`);

--
-- Indexes for table `processcode`
--
ALTER TABLE `processcode`
  ADD PRIMARY KEY (`PK_`);

--
-- Indexes for table `project_info`
--
ALTER TABLE `project_info`
  ADD PRIMARY KEY (`PK_`);

--
-- Indexes for table `test`
--
ALTER TABLE `test`
  ADD PRIMARY KEY (`PK_`);

--
-- Indexes for table `tool`
--
ALTER TABLE `tool`
  ADD PRIMARY KEY (`PK_`);

--
-- Indexes for table `vendor`
--
ALTER TABLE `vendor`
  ADD PRIMARY KEY (`PK_`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `city`
--
ALTER TABLE `city`
  MODIFY `PK_` int(4) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=369;
--
-- AUTO_INCREMENT for table `dailyreport`
--
ALTER TABLE `dailyreport`
  MODIFY `PK_` int(6) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=25;
--
-- AUTO_INCREMENT for table `dailyreport_manpower`
--
ALTER TABLE `dailyreport_manpower`
  MODIFY `PK_` int(7) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;
--
-- AUTO_INCREMENT for table `dailyreport_material`
--
ALTER TABLE `dailyreport_material`
  MODIFY `PK_` int(7) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;
--
-- AUTO_INCREMENT for table `dailyreport_outsourcing`
--
ALTER TABLE `dailyreport_outsourcing`
  MODIFY `PK_` int(7) NOT NULL AUTO_INCREMENT;
--
-- AUTO_INCREMENT for table `dailyreport_tool`
--
ALTER TABLE `dailyreport_tool`
  MODIFY `PK_` int(7) NOT NULL AUTO_INCREMENT;
--
-- AUTO_INCREMENT for table `dailyreport_vacation`
--
ALTER TABLE `dailyreport_vacation`
  MODIFY `PK_` int(7) NOT NULL AUTO_INCREMENT;
--
-- AUTO_INCREMENT for table `extendduration`
--
ALTER TABLE `extendduration`
  MODIFY `PK_` int(5) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;
--
-- AUTO_INCREMENT for table `holiday`
--
ALTER TABLE `holiday`
  MODIFY `PK_` int(5) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=61;
--
-- AUTO_INCREMENT for table `labor`
--
ALTER TABLE `labor`
  MODIFY `PK_` int(5) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;
--
-- AUTO_INCREMENT for table `material`
--
ALTER TABLE `material`
  MODIFY `PK_` int(5) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=8;
--
-- AUTO_INCREMENT for table `member`
--
ALTER TABLE `member`
  MODIFY `PK_` int(4) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=7;
--
-- AUTO_INCREMENT for table `processcode`
--
ALTER TABLE `processcode`
  MODIFY `PK_` int(4) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;
--
-- AUTO_INCREMENT for table `project_info`
--
ALTER TABLE `project_info`
  MODIFY `PK_` int(4) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=20;
--
-- AUTO_INCREMENT for table `test`
--
ALTER TABLE `test`
  MODIFY `PK_` int(5) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;
--
-- AUTO_INCREMENT for table `tool`
--
ALTER TABLE `tool`
  MODIFY `PK_` int(4) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;
--
-- AUTO_INCREMENT for table `vendor`
--
ALTER TABLE `vendor`
  MODIFY `PK_` int(4) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
