/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

USE ICMS_BetaV1
GO


begin -- Unit
if not exists (SELECT * FROM  dbo.Unit where [Name] = N'µSv/h') BEGIN INSERT INTO dbo.Unit ([Name], [IsActive], [Order]) VALUES (N'µSv/h', 1,1); END
if not exists (SELECT * FROM  dbo.Unit where [Name] = N'mSv/h') BEGIN INSERT INTO dbo.Unit ([Name], [IsActive], [Order]) VALUES (N'mSv/h', 1,2); END
if not exists (SELECT * FROM  dbo.Unit where [Name] = N'mR/h') BEGIN INSERT INTO dbo.Unit ([Name], [IsActive], [Order]) VALUES (N'mR/h', 1,3); END
if not exists (SELECT * FROM  dbo.Unit where [Name] = N'µSv') BEGIN INSERT INTO dbo.Unit ([Name], [IsActive], [Order]) VALUES (N'µSv', 1,4); END
if not exists (SELECT * FROM  dbo.Unit where [Name] = N'mSv') BEGIN INSERT INTO dbo.Unit ([Name], [IsActive], [Order]) VALUES (N'mSv', 1,5); END
if not exists (SELECT * FROM  dbo.Unit where [Name] = N'mR') BEGIN INSERT INTO dbo.Unit ([Name], [IsActive], [Order]) VALUES (N'mR', 1,6); END
if not exists (SELECT * FROM  dbo.Unit where [Name] = N'cps') BEGIN INSERT INTO dbo.Unit ([Name], [IsActive], [Order]) VALUES (N'cps', 1,7); END
if not exists (SELECT * FROM  dbo.Unit where [Name] = N'cpm') BEGIN INSERT INTO dbo.Unit ([Name], [IsActive], [Order]) VALUES (N'cpm', 1,8); END
if not exists (SELECT * FROM  dbo.Unit where [Name] = N'kBq') BEGIN INSERT INTO dbo.Unit ([Name], [IsActive], [Order]) VALUES (N'kBq', 1,9); END
if not exists (SELECT * FROM  dbo.Unit where [Name] = N'Bq/s') BEGIN INSERT INTO dbo.Unit ([Name], [IsActive], [Order]) VALUES (N'Bq/s', 1,10); END
if not exists (SELECT * FROM  dbo.Unit where [Name] = N'kBq/s') BEGIN INSERT INTO dbo.Unit ([Name], [IsActive], [Order]) VALUES (N'kBq/s', 1,11); END
if not exists (SELECT * FROM  dbo.Unit where [Name] = N'Bq') BEGIN INSERT INTO dbo.Unit ([Name], [IsActive], [Order]) VALUES (N'Bq', 1,12); END
if not exists (SELECT * FROM  dbo.Unit where [Name] = N'(µSv/h)/cps') BEGIN INSERT INTO dbo.Unit ([Name], [IsActive], [Order]) VALUES (N'(µSv/h)/cps', 1,13); END
if not exists (SELECT * FROM  dbo.Unit where [Name] = N'(µSv/h)/cpm') BEGIN INSERT INTO dbo.Unit ([Name], [IsActive], [Order]) VALUES (N'(µSv/h)/cpm', 1,14); END
end

begin  -- City
if not exists (SELECT * FROM  dbo.City where [Name] = N'An Giang') BEGIN INSERT INTO dbo.City ([Name],[PhoneCode], [IsActive]) VALUES (N'An Giang', 296,1); END
if not exists (SELECT * FROM  dbo.City where [Name] = N'Bà Rịa - Vũng Tàu') BEGIN INSERT INTO dbo.City ([Name],[PhoneCode], [IsActive]) VALUES (N'Bà Rịa - Vũng Tàu', 254,1); END
if not exists (SELECT * FROM  dbo.City where [Name] = N'Bắc Giang') BEGIN INSERT INTO dbo.City ([Name],[PhoneCode], [IsActive]) VALUES (N'Bắc Giang', 209,1); END
if not exists (SELECT * FROM  dbo.City where [Name] = N'Bắc Cạn') BEGIN INSERT INTO dbo.City ([Name],[PhoneCode], [IsActive]) VALUES (N'Bắc Cạn', 209,1); END
if not exists (SELECT * FROM  dbo.City where [Name] = N'Bạc Liêu') BEGIN INSERT INTO dbo.City ([Name],[PhoneCode], [IsActive]) VALUES (N'Bạc Liêu', 291,1); END
if not exists (SELECT * FROM  dbo.City where [Name] = N'Bắc Ninh') BEGIN INSERT INTO dbo.City ([Name],[PhoneCode], [IsActive]) VALUES (N'Bắc Ninh', 222,1); END
if not exists (SELECT * FROM  dbo.City where [Name] = N'Bến Tre') BEGIN INSERT INTO dbo.City ([Name],[PhoneCode], [IsActive]) VALUES (N'Bến Tre', 275,1); END
if not exists (SELECT * FROM  dbo.City where [Name] = N'Bình Định') BEGIN INSERT INTO dbo.City ([Name],[PhoneCode], [IsActive]) VALUES (N'Bình Định', 256,1); END
if not exists (SELECT * FROM  dbo.City where [Name] = N'Bình Dương') BEGIN INSERT INTO dbo.City ([Name],[PhoneCode], [IsActive]) VALUES (N'Bình Dương', 274,1); END
if not exists (SELECT * FROM  dbo.City where [Name] = N'Bình Phước') BEGIN INSERT INTO dbo.City ([Name],[PhoneCode], [IsActive]) VALUES (N'Bình Phước', 271,1); END
if not exists (SELECT * FROM  dbo.City where [Name] = N'Bình Thuận') BEGIN INSERT INTO dbo.City ([Name],[PhoneCode], [IsActive]) VALUES (N'Bình Thuận', 252,1); END
if not exists (SELECT * FROM  dbo.City where [Name] = N'Cà Mau') BEGIN INSERT INTO dbo.City ([Name],[PhoneCode], [IsActive]) VALUES (N'Cà Mau', 290,1); END
if not exists (SELECT * FROM  dbo.City where [Name] = N'Cần Thơ') BEGIN INSERT INTO dbo.City ([Name],[PhoneCode], [IsActive]) VALUES (N'Cần Thơ', 292,1); END
if not exists (SELECT * FROM  dbo.City where [Name] = N'Cao Bằng') BEGIN INSERT INTO dbo.City ([Name],[PhoneCode], [IsActive]) VALUES (N'Cao Bằng', 206,1); END
if not exists (SELECT * FROM  dbo.City where [Name] = N'Đà Nẵng') BEGIN INSERT INTO dbo.City ([Name],[PhoneCode], [IsActive]) VALUES (N'Đà Nẵng', 236,1); END
if not exists (SELECT * FROM  dbo.City where [Name] = N'Đăk Lăk') BEGIN INSERT INTO dbo.City ([Name],[PhoneCode], [IsActive]) VALUES (N'Đăk Lăk', 262,1); END
if not exists (SELECT * FROM  dbo.City where [Name] = N'Đăk Nông') BEGIN INSERT INTO dbo.City ([Name],[PhoneCode], [IsActive]) VALUES (N'Đăk Nông', 261,1); END
if not exists (SELECT * FROM  dbo.City where [Name] = N'Điện Biên') BEGIN INSERT INTO dbo.City ([Name],[PhoneCode], [IsActive]) VALUES (N'Điện Biên', 215,1); END
if not exists (SELECT * FROM  dbo.City where [Name] = N'Đồng Nai') BEGIN INSERT INTO dbo.City ([Name],[PhoneCode], [IsActive]) VALUES (N'Đồng Nai', 251,1); END
if not exists (SELECT * FROM  dbo.City where [Name] = N'Đồng Tháp') BEGIN INSERT INTO dbo.City ([Name],[PhoneCode], [IsActive]) VALUES (N'Đồng Tháp', 277,1); END
if not exists (SELECT * FROM  dbo.City where [Name] = N'Gia Lai') BEGIN INSERT INTO dbo.City ([Name],[PhoneCode], [IsActive]) VALUES (N'Gia Lai', 269,1); END
if not exists (SELECT * FROM  dbo.City where [Name] = N'Hà Giang') BEGIN INSERT INTO dbo.City ([Name],[PhoneCode], [IsActive]) VALUES (N'Hà Giang', 219,1); END
if not exists (SELECT * FROM  dbo.City where [Name] = N'Hà Nam') BEGIN INSERT INTO dbo.City ([Name],[PhoneCode], [IsActive]) VALUES (N'Hà Nam', 226,1); END
if not exists (SELECT * FROM  dbo.City where [Name] = N'Hà Nội') BEGIN INSERT INTO dbo.City ([Name],[PhoneCode], [IsActive]) VALUES (N'Hà Nội', 24,1); END
if not exists (SELECT * FROM  dbo.City where [Name] = N'Hà Tây') BEGIN INSERT INTO dbo.City ([Name],[PhoneCode], [IsActive]) VALUES (N'Hà Tây', 24,0); END
if not exists (SELECT * FROM  dbo.City where [Name] = N'Hà Tĩnh') BEGIN INSERT INTO dbo.City ([Name],[PhoneCode], [IsActive]) VALUES (N'Hà Tĩnh', 239,1); END
if not exists (SELECT * FROM  dbo.City where [Name] = N'Hải Dương') BEGIN INSERT INTO dbo.City ([Name],[PhoneCode], [IsActive]) VALUES (N'Hải Dương', 220,1); END
if not exists (SELECT * FROM  dbo.City where [Name] = N'Hải Phòng') BEGIN INSERT INTO dbo.City ([Name],[PhoneCode], [IsActive]) VALUES (N'Hải Phòng', 225,1); END
if not exists (SELECT * FROM  dbo.City where [Name] = N'Hậu Giang') BEGIN INSERT INTO dbo.City ([Name],[PhoneCode], [IsActive]) VALUES (N'Hậu Giang', 293,1); END
if not exists (SELECT * FROM  dbo.City where [Name] = N'TP. Hồ Chí Minh') BEGIN INSERT INTO dbo.City ([Name],[PhoneCode], [IsActive]) VALUES (N'TP. Hồ Chí Minh', 28,1); END
if not exists (SELECT * FROM  dbo.City where [Name] = N'Hòa Bình') BEGIN INSERT INTO dbo.City ([Name],[PhoneCode], [IsActive]) VALUES (N'Hòa Bình', 218,1); END
if not exists (SELECT * FROM  dbo.City where [Name] = N'Hưng Yên') BEGIN INSERT INTO dbo.City ([Name],[PhoneCode], [IsActive]) VALUES (N'Hưng Yên', 221,1); END
if not exists (SELECT * FROM  dbo.City where [Name] = N'Khánh Hòa') BEGIN INSERT INTO dbo.City ([Name],[PhoneCode], [IsActive]) VALUES (N'Khánh Hòa', 258,1); END
if not exists (SELECT * FROM  dbo.City where [Name] = N'Kiên Giang') BEGIN INSERT INTO dbo.City ([Name],[PhoneCode], [IsActive]) VALUES (N'Kiên Giang', 297,1); END
if not exists (SELECT * FROM  dbo.City where [Name] = N'Kon Tum') BEGIN INSERT INTO dbo.City ([Name],[PhoneCode], [IsActive]) VALUES (N'Kon Tum', 260,1); END
if not exists (SELECT * FROM  dbo.City where [Name] = N'Lai Châu') BEGIN INSERT INTO dbo.City ([Name],[PhoneCode], [IsActive]) VALUES (N'Lai Châu', 213,1); END
if not exists (SELECT * FROM  dbo.City where [Name] = N'Lâm Đồng') BEGIN INSERT INTO dbo.City ([Name],[PhoneCode], [IsActive]) VALUES (N'Lâm Đồng', 263,1); END
if not exists (SELECT * FROM  dbo.City where [Name] = N'Lạng Sơn') BEGIN INSERT INTO dbo.City ([Name],[PhoneCode], [IsActive]) VALUES (N'Lạng Sơn', 205,1); END
if not exists (SELECT * FROM  dbo.City where [Name] = N'Lào Cai') BEGIN INSERT INTO dbo.City ([Name],[PhoneCode], [IsActive]) VALUES (N'Lào Cai', 214,1); END
if not exists (SELECT * FROM  dbo.City where [Name] = N'Long An') BEGIN INSERT INTO dbo.City ([Name],[PhoneCode], [IsActive]) VALUES (N'Long An', 272,1); END
if not exists (SELECT * FROM  dbo.City where [Name] = N'Nam Định') BEGIN INSERT INTO dbo.City ([Name],[PhoneCode], [IsActive]) VALUES (N'Nam Định', 228,1); END
if not exists (SELECT * FROM  dbo.City where [Name] = N'Nghệ An') BEGIN INSERT INTO dbo.City ([Name],[PhoneCode], [IsActive]) VALUES (N'Nghệ An', 238,1); END
if not exists (SELECT * FROM  dbo.City where [Name] = N'Ninh Bình') BEGIN INSERT INTO dbo.City ([Name],[PhoneCode], [IsActive]) VALUES (N'Ninh Bình', 229,1); END
if not exists (SELECT * FROM  dbo.City where [Name] = N'Ninh Thuận') BEGIN INSERT INTO dbo.City ([Name],[PhoneCode], [IsActive]) VALUES (N'Ninh Thuận', 259,1); END
if not exists (SELECT * FROM  dbo.City where [Name] = N'Phú Thọ') BEGIN INSERT INTO dbo.City ([Name],[PhoneCode], [IsActive]) VALUES (N'Phú Thọ', 210,1); END
if not exists (SELECT * FROM  dbo.City where [Name] = N'Phú Yên') BEGIN INSERT INTO dbo.City ([Name],[PhoneCode], [IsActive]) VALUES (N'Phú Yên', 257,1); END
if not exists (SELECT * FROM  dbo.City where [Name] = N'Quảng Bình') BEGIN INSERT INTO dbo.City ([Name],[PhoneCode], [IsActive]) VALUES (N'Quảng Bình', 232,1); END
if not exists (SELECT * FROM  dbo.City where [Name] = N'Quảng Nam') BEGIN INSERT INTO dbo.City ([Name],[PhoneCode], [IsActive]) VALUES (N'Quảng Nam', 235,1); END
if not exists (SELECT * FROM  dbo.City where [Name] = N'Quảng Ngãi') BEGIN INSERT INTO dbo.City ([Name],[PhoneCode], [IsActive]) VALUES (N'Quảng Ngãi', 255,1); END
if not exists (SELECT * FROM  dbo.City where [Name] = N'Quảng Ninh') BEGIN INSERT INTO dbo.City ([Name],[PhoneCode], [IsActive]) VALUES (N'Quảng Ninh', 203,1); END
if not exists (SELECT * FROM  dbo.City where [Name] = N'Quảng Trị') BEGIN INSERT INTO dbo.City ([Name],[PhoneCode], [IsActive]) VALUES (N'Quảng Trị', 233,1); END
if not exists (SELECT * FROM  dbo.City where [Name] = N'Sóc Trăng') BEGIN INSERT INTO dbo.City ([Name],[PhoneCode], [IsActive]) VALUES (N'Sóc Trăng', 299,1); END
if not exists (SELECT * FROM  dbo.City where [Name] = N'Sơn La') BEGIN INSERT INTO dbo.City ([Name],[PhoneCode], [IsActive]) VALUES (N'Sơn La', 212,1); END
if not exists (SELECT * FROM  dbo.City where [Name] = N'Tây Ninh') BEGIN INSERT INTO dbo.City ([Name],[PhoneCode], [IsActive]) VALUES (N'Tây Ninh', 276,1); END
if not exists (SELECT * FROM  dbo.City where [Name] = N'Thái Bình') BEGIN INSERT INTO dbo.City ([Name],[PhoneCode], [IsActive]) VALUES (N'Thái Bình', 227,1); END
if not exists (SELECT * FROM  dbo.City where [Name] = N'Thái Nguyên') BEGIN INSERT INTO dbo.City ([Name],[PhoneCode], [IsActive]) VALUES (N'Thái Nguyên', 208,1); END
if not exists (SELECT * FROM  dbo.City where [Name] = N'Thanh Hóa') BEGIN INSERT INTO dbo.City ([Name],[PhoneCode], [IsActive]) VALUES (N'Thanh Hóa', 237,1); END
if not exists (SELECT * FROM  dbo.City where [Name] = N'Thừa Thiên - Huế') BEGIN INSERT INTO dbo.City ([Name],[PhoneCode], [IsActive]) VALUES (N'Thừa Thiên - Huế', 234,1); END
if not exists (SELECT * FROM  dbo.City where [Name] = N'Tiền Giang') BEGIN INSERT INTO dbo.City ([Name],[PhoneCode], [IsActive]) VALUES (N'Tiền Giang', 273,1); END
if not exists (SELECT * FROM  dbo.City where [Name] = N'Trà Vinh') BEGIN INSERT INTO dbo.City ([Name],[PhoneCode], [IsActive]) VALUES (N'Trà Vinh', 294,1); END
if not exists (SELECT * FROM  dbo.City where [Name] = N'Tuyên Quang') BEGIN INSERT INTO dbo.City ([Name],[PhoneCode], [IsActive]) VALUES (N'Tuyên Quang', 207,1); END
if not exists (SELECT * FROM  dbo.City where [Name] = N'Vĩnh Long') BEGIN INSERT INTO dbo.City ([Name],[PhoneCode], [IsActive]) VALUES (N'Vĩnh Long', 270,1); END
if not exists (SELECT * FROM  dbo.City where [Name] = N'Vĩnh Phúc') BEGIN INSERT INTO dbo.City ([Name],[PhoneCode], [IsActive]) VALUES (N'Vĩnh Phúc', 211,1); END
if not exists (SELECT * FROM  dbo.City where [Name] = N'Yên Bái') BEGIN INSERT INTO dbo.City ([Name],[PhoneCode], [IsActive]) VALUES (N'Yên Bái', 216,1); END


end

--begin  -- MachineName
--if not exists (SELECT * FROM  dbo.MachineName where [Name] = N'Aloka') BEGIN INSERT INTO dbo.MachineName ([Name]) VALUES (N'Aloka'); END
--if not exists (SELECT * FROM  dbo.MachineName where [Name] = N'Aloka Mydose mini') BEGIN INSERT INTO dbo.MachineName ([Name]) VALUES (N'Aloka Mydose mini'); END
--if not exists (SELECT * FROM  dbo.MachineName where [Name] = N'Apvl') BEGIN INSERT INTO dbo.MachineName ([Name]) VALUES (N'Apvl'); END
--if not exists (SELECT * FROM  dbo.MachineName where [Name] = N'ARROW') BEGIN INSERT INTO dbo.MachineName ([Name]) VALUES (N'ARROW'); END
--if not exists (SELECT * FROM  dbo.MachineName where [Name] = N'AT6101C') BEGIN INSERT INTO dbo.MachineName ([Name]) VALUES (N'AT6101C'); END
--if not exists (SELECT * FROM  dbo.MachineName where [Name] = N'ATOMTEX') BEGIN INSERT INTO dbo.MachineName ([Name]) VALUES (N'ATOMTEX'); END
--if not exists (SELECT * FROM  dbo.MachineName where [Name] = N'Berthold') BEGIN INSERT INTO dbo.MachineName ([Name]) VALUES (N'Berthold'); END
--if not exists (SELECT * FROM  dbo.MachineName where [Name] = N'Bleeper Sv') BEGIN INSERT INTO dbo.MachineName ([Name]) VALUES (N'Bleeper Sv'); END
--if not exists (SELECT * FROM  dbo.MachineName where [Name] = N'CANARY') BEGIN INSERT INTO dbo.MachineName ([Name]) VALUES (N'CANARY'); END
--if not exists (SELECT * FROM  dbo.MachineName where [Name] = N'CAPINTEC') BEGIN INSERT INTO dbo.MachineName ([Name]) VALUES (N'CAPINTEC'); END
--if not exists (SELECT * FROM  dbo.MachineName where [Name] = N'CLOVER') BEGIN INSERT INTO dbo.MachineName ([Name]) VALUES (N'CLOVER'); END
--if not exists (SELECT * FROM  dbo.MachineName where [Name] = N'CRM 100') BEGIN INSERT INTO dbo.MachineName ([Name]) VALUES (N'CRM 100'); END
--if not exists (SELECT * FROM  dbo.MachineName where [Name] = N'Digilert 100') BEGIN INSERT INTO dbo.MachineName ([Name]) VALUES (N'Digilert 100'); END
--if not exists (SELECT * FROM  dbo.MachineName where [Name] = N'DKS-96') BEGIN INSERT INTO dbo.MachineName ([Name]) VALUES (N'DKS-96'); END
--if not exists (SELECT * FROM  dbo.MachineName where [Name] = N'DMC 3000') BEGIN INSERT INTO dbo.MachineName ([Name]) VALUES (N'DMC 3000'); END
--if not exists (SELECT * FROM  dbo.MachineName where [Name] = N'Dose rate meter YF-9200') BEGIN INSERT INTO dbo.MachineName ([Name]) VALUES (N'Dose rate meter YF-9200'); END
--if not exists (SELECT * FROM  dbo.MachineName where [Name] = N'Dosemeter') BEGIN INSERT INTO dbo.MachineName ([Name]) VALUES (N'Dosemeter'); END
--if not exists (SELECT * FROM  dbo.MachineName where [Name] = N'ĐP5 - CT') BEGIN INSERT INTO dbo.MachineName ([Name]) VALUES (N'ĐP5 - CT'); END
--if not exists (SELECT * FROM  dbo.MachineName where [Name] = N'Eberline') BEGIN INSERT INTO dbo.MachineName ([Name]) VALUES (N'Eberline'); END
--if not exists (SELECT * FROM  dbo.MachineName where [Name] = N'ECOTEST') BEGIN INSERT INTO dbo.MachineName ([Name]) VALUES (N'ECOTEST'); END
--if not exists (SELECT * FROM  dbo.MachineName where [Name] = N'Exploranium') BEGIN INSERT INTO dbo.MachineName ([Name]) VALUES (N'Exploranium'); END
--if not exists (SELECT * FROM  dbo.MachineName where [Name] = N'FAG') BEGIN INSERT INTO dbo.MachineName ([Name]) VALUES (N'FAG'); END
--if not exists (SELECT * FROM  dbo.MachineName where [Name] = N'FH40F2') BEGIN INSERT INTO dbo.MachineName ([Name]) VALUES (N'FH40F2'); END
--if not exists (SELECT * FROM  dbo.MachineName where [Name] = N'FLUKE') BEGIN INSERT INTO dbo.MachineName ([Name]) VALUES (N'FLUKE'); END
--if not exists (SELECT * FROM  dbo.MachineName where [Name] = N'Fuji Electric') BEGIN INSERT INTO dbo.MachineName ([Name]) VALUES (N'Fuji Electric'); END
--if not exists (SELECT * FROM  dbo.MachineName where [Name] = N'Gamma - Scout') BEGIN INSERT INTO dbo.MachineName ([Name]) VALUES (N'Gamma - Scout'); END
--if not exists (SELECT * FROM  dbo.MachineName where [Name] = N'Gamma Alarm System') BEGIN INSERT INTO dbo.MachineName ([Name]) VALUES (N'Gamma Alarm System'); END
--if not exists (SELECT * FROM  dbo.MachineName where [Name] = N'Gamma Area Monitor') BEGIN INSERT INTO dbo.MachineName ([Name]) VALUES (N'Gamma Area Monitor'); END
--if not exists (SELECT * FROM  dbo.MachineName where [Name] = N'Gamma RAE II R') BEGIN INSERT INTO dbo.MachineName ([Name]) VALUES (N'Gamma RAE II R'); END
--if not exists (SELECT * FROM  dbo.MachineName where [Name] = N'GammaRAE II R') BEGIN INSERT INTO dbo.MachineName ([Name]) VALUES (N'GammaRAE II R'); END
--if not exists (SELECT * FROM  dbo.MachineName where [Name] = N'GammaTwin') BEGIN INSERT INTO dbo.MachineName ([Name]) VALUES (N'GammaTwin'); END
--if not exists (SELECT * FROM  dbo.MachineName where [Name] = N'Hand-held Neutron Monitor') BEGIN INSERT INTO dbo.MachineName ([Name]) VALUES (N'Hand-held Neutron Monitor'); END
--if not exists (SELECT * FROM  dbo.MachineName where [Name] = N'IDENTIFINDER') BEGIN INSERT INTO dbo.MachineName ([Name]) VALUES (N'IDENTIFINDER'); END
--if not exists (SELECT * FROM  dbo.MachineName where [Name] = N'IDENTIFINDER 2') BEGIN INSERT INTO dbo.MachineName ([Name]) VALUES (N'IDENTIFINDER 2'); END
--if not exists (SELECT * FROM  dbo.MachineName where [Name] = N'Identify PAM 945') BEGIN INSERT INTO dbo.MachineName ([Name]) VALUES (N'Identify PAM 945'); END
--if not exists (SELECT * FROM  dbo.MachineName where [Name] = N'IJRAD ') BEGIN INSERT INTO dbo.MachineName ([Name]) VALUES (N'IJRAD '); END
--if not exists (SELECT * FROM  dbo.MachineName where [Name] = N'Inspector') BEGIN INSERT INTO dbo.MachineName ([Name]) VALUES (N'Inspector'); END
--if not exists (SELECT * FROM  dbo.MachineName where [Name] = N'INSPECTOR 1000') BEGIN INSERT INTO dbo.MachineName ([Name]) VALUES (N'INSPECTOR 1000'); END
--if not exists (SELECT * FROM  dbo.MachineName where [Name] = N'INSPECTOR ALERT') BEGIN INSERT INTO dbo.MachineName ([Name]) VALUES (N'INSPECTOR ALERT'); END
--if not exists (SELECT * FROM  dbo.MachineName where [Name] = N'Inspector EXP') BEGIN INSERT INTO dbo.MachineName ([Name]) VALUES (N'Inspector EXP'); END
--if not exists (SELECT * FROM  dbo.MachineName where [Name] = N'Isotra') BEGIN INSERT INTO dbo.MachineName ([Name]) VALUES (N'Isotra'); END
--if not exists (SELECT * FROM  dbo.MachineName where [Name] = N'Isotrak') BEGIN INSERT INTO dbo.MachineName ([Name]) VALUES (N'Isotrak'); END
--if not exists (SELECT * FROM  dbo.MachineName where [Name] = N'JB4020') BEGIN INSERT INTO dbo.MachineName ([Name]) VALUES (N'JB4020'); END
--if not exists (SELECT * FROM  dbo.MachineName where [Name] = N'LUDLUM') BEGIN INSERT INTO dbo.MachineName ([Name]) VALUES (N'LUDLUM'); END
--if not exists (SELECT * FROM  dbo.MachineName where [Name] = N'Ludlum 3') BEGIN INSERT INTO dbo.MachineName ([Name]) VALUES (N'Ludlum 3'); END
--if not exists (SELECT * FROM  dbo.MachineName where [Name] = N'Ludlum 375 digital area monitor') BEGIN INSERT INTO dbo.MachineName ([Name]) VALUES (N'Ludlum 375 digital area monitor'); END
--if not exists (SELECT * FROM  dbo.MachineName where [Name] = N'44684') BEGIN INSERT INTO dbo.MachineName ([Name]) VALUES (N'44684'); END
--if not exists (SELECT * FROM  dbo.MachineName where [Name] = N'Máy Đo Phóng Xạ 4 Kênh') BEGIN INSERT INTO dbo.MachineName ([Name]) VALUES (N'Máy Đo Phóng Xạ 4 Kênh'); END
--if not exists (SELECT * FROM  dbo.MachineName where [Name] = N'MAZUR') BEGIN INSERT INTO dbo.MachineName ([Name]) VALUES (N'MAZUR'); END
--if not exists (SELECT * FROM  dbo.MachineName where [Name] = N'MicroSievert') BEGIN INSERT INTO dbo.MachineName ([Name]) VALUES (N'MicroSievert'); END
--if not exists (SELECT * FROM  dbo.MachineName where [Name] = N'Mini 900 Ratemeter') BEGIN INSERT INTO dbo.MachineName ([Name]) VALUES (N'Mini 900 Ratemeter'); END
--if not exists (SELECT * FROM  dbo.MachineName where [Name] = N'Mini Telerad') BEGIN INSERT INTO dbo.MachineName ([Name]) VALUES (N'Mini Telerad'); END
--if not exists (SELECT * FROM  dbo.MachineName where [Name] = N'MIRION') BEGIN INSERT INTO dbo.MachineName ([Name]) VALUES (N'MIRION'); END
--if not exists (SELECT * FROM  dbo.MachineName where [Name] = N'Model 375 digital area monitor') BEGIN INSERT INTO dbo.MachineName ([Name]) VALUES (N'Model 375 digital area monitor'); END
--if not exists (SELECT * FROM  dbo.MachineName where [Name] = N'Monitor 1000') BEGIN INSERT INTO dbo.MachineName ([Name]) VALUES (N'Monitor 1000'); END
--if not exists (SELECT * FROM  dbo.MachineName where [Name] = N'Monitor 4') BEGIN INSERT INTO dbo.MachineName ([Name]) VALUES (N'Monitor 4'); END
--if not exists (SELECT * FROM  dbo.MachineName where [Name] = N'Monitor 4EC') BEGIN INSERT INTO dbo.MachineName ([Name]) VALUES (N'Monitor 4EC'); END
--if not exists (SELECT * FROM  dbo.MachineName where [Name] = N'ND 2000') BEGIN INSERT INTO dbo.MachineName ([Name]) VALUES (N'ND 2000'); END
--if not exists (SELECT * FROM  dbo.MachineName where [Name] = N'NDS') BEGIN INSERT INTO dbo.MachineName ([Name]) VALUES (N'NDS'); END
--if not exists (SELECT * FROM  dbo.MachineName where [Name] = N'NovelDetector') BEGIN INSERT INTO dbo.MachineName ([Name]) VALUES (N'NovelDetector'); END
--if not exists (SELECT * FROM  dbo.MachineName where [Name] = N'OHMART') BEGIN INSERT INTO dbo.MachineName ([Name]) VALUES (N'OHMART'); END
--if not exists (SELECT * FROM  dbo.MachineName where [Name] = N'Packeye') BEGIN INSERT INTO dbo.MachineName ([Name]) VALUES (N'Packeye'); END
--if not exists (SELECT * FROM  dbo.MachineName where [Name] = N'Pico catch 100') BEGIN INSERT INTO dbo.MachineName ([Name]) VALUES (N'Pico catch 100'); END
--if not exists (SELECT * FROM  dbo.MachineName where [Name] = N'Picoray') BEGIN INSERT INTO dbo.MachineName ([Name]) VALUES (N'Picoray'); END
--if not exists (SELECT * FROM  dbo.MachineName where [Name] = N'POLIMASTER') BEGIN INSERT INTO dbo.MachineName ([Name]) VALUES (N'POLIMASTER'); END
--if not exists (SELECT * FROM  dbo.MachineName where [Name] = N'PRIMALERT DIGITAL AREA MONITOR') BEGIN INSERT INTO dbo.MachineName ([Name]) VALUES (N'PRIMALERT DIGITAL AREA MONITOR'); END
--if not exists (SELECT * FROM  dbo.MachineName where [Name] = N'Pudibei') BEGIN INSERT INTO dbo.MachineName ([Name]) VALUES (N'Pudibei'); END
--if not exists (SELECT * FROM  dbo.MachineName where [Name] = N'RAD 100') BEGIN INSERT INTO dbo.MachineName ([Name]) VALUES (N'RAD 100'); END
--if not exists (SELECT * FROM  dbo.MachineName where [Name] = N'RAD IQ') BEGIN INSERT INTO dbo.MachineName ([Name]) VALUES (N'RAD IQ'); END
--if not exists (SELECT * FROM  dbo.MachineName where [Name] = N'Radalert 100') BEGIN INSERT INTO dbo.MachineName ([Name]) VALUES (N'Radalert 100'); END
--if not exists (SELECT * FROM  dbo.MachineName where [Name] = N'Radalert 50') BEGIN INSERT INTO dbo.MachineName ([Name]) VALUES (N'Radalert 50'); END
--if not exists (SELECT * FROM  dbo.MachineName where [Name] = N'Radex') BEGIN INSERT INTO dbo.MachineName ([Name]) VALUES (N'Radex'); END
--if not exists (SELECT * FROM  dbo.MachineName where [Name] = N'RadEye') BEGIN INSERT INTO dbo.MachineName ([Name]) VALUES (N'RadEye'); END
--if not exists (SELECT * FROM  dbo.MachineName where [Name] = N'Radi Horiba') BEGIN INSERT INTO dbo.MachineName ([Name]) VALUES (N'Radi Horiba'); END
--if not exists (SELECT * FROM  dbo.MachineName where [Name] = N'RADIAGEM') BEGIN INSERT INTO dbo.MachineName ([Name]) VALUES (N'RADIAGEM'); END
--if not exists (SELECT * FROM  dbo.MachineName where [Name] = N'RADIATION ALERT') BEGIN INSERT INTO dbo.MachineName ([Name]) VALUES (N'RADIATION ALERT'); END
--if not exists (SELECT * FROM  dbo.MachineName where [Name] = N'Radiation Monitor') BEGIN INSERT INTO dbo.MachineName ([Name]) VALUES (N'Radiation Monitor'); END
--if not exists (SELECT * FROM  dbo.MachineName where [Name] = N'Radiation Monitoring Controller - CGN') BEGIN INSERT INTO dbo.MachineName ([Name]) VALUES (N'Radiation Monitoring Controller - CGN'); END
--if not exists (SELECT * FROM  dbo.MachineName where [Name] = N'Radiation Monitoring Station') BEGIN INSERT INTO dbo.MachineName ([Name]) VALUES (N'Radiation Monitoring Station'); END
--if not exists (SELECT * FROM  dbo.MachineName where [Name] = N'Radiation Testing Machine') BEGIN INSERT INTO dbo.MachineName ([Name]) VALUES (N'Radiation Testing Machine'); END
--if not exists (SELECT * FROM  dbo.MachineName where [Name] = N'RADICO') BEGIN INSERT INTO dbo.MachineName ([Name]) VALUES (N'RADICO'); END
--if not exists (SELECT * FROM  dbo.MachineName where [Name] = N'Radiometer Dosimeter') BEGIN INSERT INTO dbo.MachineName ([Name]) VALUES (N'Radiometer Dosimeter'); END
--if not exists (SELECT * FROM  dbo.MachineName where [Name] = N'RADOS') BEGIN INSERT INTO dbo.MachineName ([Name]) VALUES (N'RADOS'); END
--if not exists (SELECT * FROM  dbo.MachineName where [Name] = N'RADSOL') BEGIN INSERT INTO dbo.MachineName ([Name]) VALUES (N'RADSOL'); END
--if not exists (SELECT * FROM  dbo.MachineName where [Name] = N'RANGER') BEGIN INSERT INTO dbo.MachineName ([Name]) VALUES (N'RANGER'); END
--if not exists (SELECT * FROM  dbo.MachineName where [Name] = N'RAPISCAN') BEGIN INSERT INTO dbo.MachineName ([Name]) VALUES (N'RAPISCAN'); END
--if not exists (SELECT * FROM  dbo.MachineName where [Name] = N'Raysafe 452') BEGIN INSERT INTO dbo.MachineName ([Name]) VALUES (N'Raysafe 452'); END
--if not exists (SELECT * FROM  dbo.MachineName where [Name] = N'REN 200') BEGIN INSERT INTO dbo.MachineName ([Name]) VALUES (N'REN 200'); END
--if not exists (SELECT * FROM  dbo.MachineName where [Name] = N'RI-02') BEGIN INSERT INTO dbo.MachineName ([Name]) VALUES (N'RI-02'); END
--if not exists (SELECT * FROM  dbo.MachineName where [Name] = N'RIIDEye X') BEGIN INSERT INTO dbo.MachineName ([Name]) VALUES (N'RIIDEye X'); END
--if not exists (SELECT * FROM  dbo.MachineName where [Name] = N'RTI Survey meter') BEGIN INSERT INTO dbo.MachineName ([Name]) VALUES (N'RTI Survey meter'); END
--if not exists (SELECT * FROM  dbo.MachineName where [Name] = N'SAM 945 ') BEGIN INSERT INTO dbo.MachineName ([Name]) VALUES (N'SAM 945 '); END
--if not exists (SELECT * FROM  dbo.MachineName where [Name] = N'SARA Base Unit Fix Station (LAN)') BEGIN INSERT INTO dbo.MachineName ([Name]) VALUES (N'SARA Base Unit Fix Station (LAN)'); END
--if not exists (SELECT * FROM  dbo.MachineName where [Name] = N'smart Rad') BEGIN INSERT INTO dbo.MachineName ([Name]) VALUES (N'smart Rad'); END
--if not exists (SELECT * FROM  dbo.MachineName where [Name] = N'SPECTRA') BEGIN INSERT INTO dbo.MachineName ([Name]) VALUES (N'SPECTRA'); END
--if not exists (SELECT * FROM  dbo.MachineName where [Name] = N'Spectrometer') BEGIN INSERT INTO dbo.MachineName ([Name]) VALUES (N'Spectrometer'); END
--if not exists (SELECT * FROM  dbo.MachineName where [Name] = N'Station Radiation Dosimeter') BEGIN INSERT INTO dbo.MachineName ([Name]) VALUES (N'Station Radiation Dosimeter'); END
--if not exists (SELECT * FROM  dbo.MachineName where [Name] = N'STEP') BEGIN INSERT INTO dbo.MachineName ([Name]) VALUES (N'STEP'); END
--if not exists (SELECT * FROM  dbo.MachineName where [Name] = N'STORA-TU') BEGIN INSERT INTO dbo.MachineName ([Name]) VALUES (N'STORA-TU'); END
--if not exists (SELECT * FROM  dbo.MachineName where [Name] = N'SVG - 2M') BEGIN INSERT INTO dbo.MachineName ([Name]) VALUES (N'SVG - 2M'); END
--if not exists (SELECT * FROM  dbo.MachineName where [Name] = N'Technidata') BEGIN INSERT INTO dbo.MachineName ([Name]) VALUES (N'Technidata'); END
--if not exists (SELECT * FROM  dbo.MachineName where [Name] = N'TENMARS') BEGIN INSERT INTO dbo.MachineName ([Name]) VALUES (N'TENMARS'); END
--if not exists (SELECT * FROM  dbo.MachineName where [Name] = N'TERRA') BEGIN INSERT INTO dbo.MachineName ([Name]) VALUES (N'TERRA'); END
--if not exists (SELECT * FROM  dbo.MachineName where [Name] = N'TERRA P') BEGIN INSERT INTO dbo.MachineName ([Name]) VALUES (N'TERRA P'); END
--if not exists (SELECT * FROM  dbo.MachineName where [Name] = N'Thermo') BEGIN INSERT INTO dbo.MachineName ([Name]) VALUES (N'Thermo'); END
--if not exists (SELECT * FROM  dbo.MachineName where [Name] = N'Thermo Scientific EPD') BEGIN INSERT INTO dbo.MachineName ([Name]) VALUES (N'Thermo Scientific EPD'); END
--if not exists (SELECT * FROM  dbo.MachineName where [Name] = N'Thiết bị đo phóng xạ') BEGIN INSERT INTO dbo.MachineName ([Name]) VALUES (N'Thiết bị đo phóng xạ'); END
--if not exists (SELECT * FROM  dbo.MachineName where [Name] = N'Tracerco') BEGIN INSERT INTO dbo.MachineName ([Name]) VALUES (N'Tracerco'); END
--if not exists (SELECT * FROM  dbo.MachineName where [Name] = N'TROXLER') BEGIN INSERT INTO dbo.MachineName ([Name]) VALUES (N'TROXLER'); END
--if not exists (SELECT * FROM  dbo.MachineName where [Name] = N'VICTOREEN') BEGIN INSERT INTO dbo.MachineName ([Name]) VALUES (N'VICTOREEN'); END
--if not exists (SELECT * FROM  dbo.MachineName where [Name] = N'X5CEx') BEGIN INSERT INTO dbo.MachineName ([Name]) VALUES (N'X5CEx'); END


--end

begin   -- Role
if not exists (SELECT * FROM  dbo.Role where [Name] = N'Viewer') BEGIN INSERT INTO dbo.Role  ([Name], [User], [Permission],[BackupDB], [RestoreDB],[RadQuantity],[DoseQuantity],[Unit],[TM],[Certificate],[Customer],[City],[MachineName],[MachineType],[SensorType]) VALUES (N'Viewer', 16, 16, 16, 16, 16, 16, 16, 16, 16, 16, 16, 16, 16, 16); END																			
if not exists (SELECT * FROM  dbo.Role where [Name] = N'Technical') BEGIN INSERT INTO dbo.Role  ([Name], [User], [Permission],[BackupDB], [RestoreDB],[RadQuantity],[DoseQuantity],[Unit],[TM],[Certificate],[Customer],[City],[MachineName],[MachineType],[SensorType]) VALUES (N'Technical', 0, 0, 0, 0, 16, 16, 16, 28, 31, 28, 28, 30, 28, 28); END																			
if not exists (SELECT * FROM  dbo.Role where [Name] = N'TM') BEGIN INSERT INTO dbo.Role  ([Name], [User], [Permission],[BackupDB], [RestoreDB],[RadQuantity],[DoseQuantity],[Unit],[TM],[Certificate],[Customer],[City],[MachineName],[MachineType],[SensorType]) VALUES (N'TM', 28, 16, 28, 16, 30, 30, 30, 30, 31, 30, 30, 30, 30, 30); END																			
if not exists (SELECT * FROM  dbo.Role where [Name] = N'QM') BEGIN INSERT INTO dbo.Role  ([Name], [User], [Permission],[BackupDB], [RestoreDB],[RadQuantity],[DoseQuantity],[Unit],[TM],[Certificate],[Customer],[City],[MachineName],[MachineType],[SensorType]) VALUES (N'QM', 30, 20, 30, 30, 30, 30, 30, 30, 31, 30, 30, 30, 30, 30); END																			
if not exists (SELECT * FROM  dbo.Role where [Name] = N'Admin') BEGIN INSERT INTO dbo.Role  ([Name], [User], [Permission],[BackupDB], [RestoreDB],[RadQuantity],[DoseQuantity],[Unit],[TM],[Certificate],[Customer],[City],[MachineName],[MachineType],[SensorType]) VALUES (N'Admin', 30, 20, 30, 30, 30, 30, 30, 30, 31, 30, 30, 30, 30, 30); END																			
											
end

begin   -- RadQuantity
if not exists (SELECT * FROM  dbo.RadQuantity where [NameVN] = N'Cs-137') BEGIN INSERT INTO dbo.RadQuantity  ([NameVN], [NameEN],[Energy],[ReUnc], [IsActive]) VALUES (N'Cs-137', N'Cs-137',662,0.03, 1); END												
if not exists (SELECT * FROM  dbo.RadQuantity where [NameVN] = N'Tia X ISO N100') BEGIN INSERT INTO dbo.RadQuantity  ([NameVN], [NameEN],[Energy],[ReUnc], [IsActive]) VALUES (N'Tia X ISO N100', N'X-ray ISO N100',83.3,0.04, 1); END												
if not exists (SELECT * FROM  dbo.RadQuantity where [NameVN] = N'Co-60') BEGIN INSERT INTO dbo.RadQuantity  ([NameVN], [NameEN],[Energy],[ReUnc], [IsActive]) VALUES (N'Co-60', N'Co-60',1252.5,0.03, 1); END												
if not exists (SELECT * FROM  dbo.RadQuantity where [NameVN] = N'Tia X ISO N40') BEGIN INSERT INTO dbo.RadQuantity  ([NameVN], [NameEN],[Energy],[ReUnc], [IsActive]) VALUES (N'Tia X ISO N40', N'X-ray ISO N40',33.3,0.04, 1); END												
if not exists (SELECT * FROM  dbo.RadQuantity where [NameVN] = N'Tia X ISO N60') BEGIN INSERT INTO dbo.RadQuantity  ([NameVN], [NameEN],[Energy],[ReUnc], [IsActive]) VALUES (N'Tia X ISO N60', N'X-ray ISO N60',47.9,0.04, 1); END												
if not exists (SELECT * FROM  dbo.RadQuantity where [NameVN] = N'Tia X ISO N80') BEGIN INSERT INTO dbo.RadQuantity  ([NameVN], [NameEN],[Energy],[ReUnc], [IsActive]) VALUES (N'Tia X ISO N80', N'X-ray ISO N80',65.2,0.04, 1); END												
if not exists (SELECT * FROM  dbo.RadQuantity where [NameVN] = N'Tia X ISO N120') BEGIN INSERT INTO dbo.RadQuantity  ([NameVN], [NameEN],[Energy],[ReUnc], [IsActive]) VALUES (N'Tia X ISO N120', N'X-ray ISO N120',100,0.04, 1); END												
if not exists (SELECT * FROM  dbo.RadQuantity where [NameVN] = N'Tia X ISO N150') BEGIN INSERT INTO dbo.RadQuantity  ([NameVN], [NameEN],[Energy],[ReUnc], [IsActive]) VALUES (N'Tia X ISO N150', N'X-ray ISO N150',118,0.04, 1); END												
if not exists (SELECT * FROM  dbo.RadQuantity where [NameVN] = N'Tia X ISO L55') BEGIN INSERT INTO dbo.RadQuantity  ([NameVN], [NameEN],[Energy],[ReUnc], [IsActive]) VALUES (N'Tia X ISO L55', N'X-ray ISO L55',47.8,0.04, 1); END												
if not exists (SELECT * FROM  dbo.RadQuantity where [NameVN] = N'Tia X ISO L70') BEGIN INSERT INTO dbo.RadQuantity  ([NameVN], [NameEN],[Energy],[ReUnc], [IsActive]) VALUES (N'Tia X ISO L70', N'X-ray ISO L70',60.6,0.04, 1); END												
if not exists (SELECT * FROM  dbo.RadQuantity where [NameVN] = N'Tia X ISO L100') BEGIN INSERT INTO dbo.RadQuantity  ([NameVN], [NameEN],[Energy],[ReUnc], [IsActive]) VALUES (N'Tia X ISO L100', N'X-ray ISO L100',86.8,0.04, 0); END												
if not exists (SELECT * FROM  dbo.RadQuantity where [NameVN] = N'Tia X ISO L125') BEGIN INSERT INTO dbo.RadQuantity  ([NameVN], [NameEN],[Energy],[ReUnc], [IsActive]) VALUES (N'Tia X ISO L125', N'X-ray ISO L125',109,0.04, 0); END												
if not exists (SELECT * FROM  dbo.RadQuantity where [NameVN] = N'Tia X ISO H40') BEGIN INSERT INTO dbo.RadQuantity  ([NameVN], [NameEN],[Energy],[ReUnc], [IsActive]) VALUES (N'Tia X ISO H40', N'X-ray ISO H40',25.4,0.04, 0); END												
if not exists (SELECT * FROM  dbo.RadQuantity where [NameVN] = N'Tia X ISO H60') BEGIN INSERT INTO dbo.RadQuantity  ([NameVN], [NameEN],[Energy],[ReUnc], [IsActive]) VALUES (N'Tia X ISO H60', N'X-ray ISO H60',38,0.04, 0); END												
if not exists (SELECT * FROM  dbo.RadQuantity where [NameVN] = N'Tia X ISO H80') BEGIN INSERT INTO dbo.RadQuantity  ([NameVN], [NameEN],[Energy],[ReUnc], [IsActive]) VALUES (N'Tia X ISO H80', N'X-ray ISO H80',48.8,0.04, 0); END												
if not exists (SELECT * FROM  dbo.RadQuantity where [NameVN] = N'Tia X ISO H100') BEGIN INSERT INTO dbo.RadQuantity  ([NameVN], [NameEN],[Energy],[ReUnc], [IsActive]) VALUES (N'Tia X ISO H100', N'X-ray ISO H100',57.3,0.04, 0); END												
if not exists (SELECT * FROM  dbo.RadQuantity where [NameVN] = N'Tia X ISO H150') BEGIN INSERT INTO dbo.RadQuantity  ([NameVN], [NameEN],[Energy],[ReUnc], [IsActive]) VALUES (N'Tia X ISO H150', N'X-ray ISO H150',78,0.04, 0); END												
if not exists (SELECT * FROM  dbo.RadQuantity where [NameVN] = N'Tia X ISO W40') BEGIN INSERT INTO dbo.RadQuantity  ([NameVN], [NameEN],[Energy],[ReUnc], [IsActive]) VALUES (N'Tia X ISO W40', N'X-ray ISO W40',29.8,0.04, 0); END												
if not exists (SELECT * FROM  dbo.RadQuantity where [NameVN] = N'Tia X ISO W60') BEGIN INSERT INTO dbo.RadQuantity  ([NameVN], [NameEN],[Energy],[ReUnc], [IsActive]) VALUES (N'Tia X ISO W60', N'X-ray ISO W60',44.8,0.04, 0); END												
if not exists (SELECT * FROM  dbo.RadQuantity where [NameVN] = N'Tia X ISO W80') BEGIN INSERT INTO dbo.RadQuantity  ([NameVN], [NameEN],[Energy],[ReUnc], [IsActive]) VALUES (N'Tia X ISO W80', N'X-ray ISO W80',56.5,0.04, 0); END												
if not exists (SELECT * FROM  dbo.RadQuantity where [NameVN] = N'Tia X ISO W110') BEGIN INSERT INTO dbo.RadQuantity  ([NameVN], [NameEN],[Energy],[ReUnc], [IsActive]) VALUES (N'Tia X ISO W110', N'X-ray ISO W110',79.1,0.04, 0); END												
if not exists (SELECT * FROM  dbo.RadQuantity where [NameVN] = N'Tia X ISO W150') BEGIN INSERT INTO dbo.RadQuantity  ([NameVN], [NameEN],[Energy],[ReUnc], [IsActive]) VALUES (N'Tia X ISO W150', N'X-ray ISO W150',105,0.04, 0); END												
if not exists (SELECT * FROM  dbo.RadQuantity where [NameVN] = N'Sr-90/Y-90') BEGIN INSERT INTO dbo.RadQuantity  ([NameVN], [NameEN],[Energy],[ReUnc], [IsActive]) VALUES (N'Sr-90/Y-90', N'Sr-90/Y-90',1404,0.05, 0); END												
if not exists (SELECT * FROM  dbo.RadQuantity where [NameVN] = N'Am-241') BEGIN INSERT INTO dbo.RadQuantity  ([NameVN], [NameEN],[Energy],[ReUnc], [IsActive]) VALUES (N'Am-241', N'Am-241',59.5,0.05, 0); END												

end

begin   -- DosimetryQuantity
if not exists (SELECT * FROM  dbo.DoseQuantity where [NameVN] = N'Kerma không khí') BEGIN INSERT INTO dbo.DoseQuantity  ([NameVN],[NameEN],[Notation],[IsActive]) VALUES (N'Kerma không khí ' ,'Air-Kerma', N'Kₐ',1); END															
if not exists (SELECT * FROM  dbo.DoseQuantity where [NameVN] = N'Tương đương liều môi trường') BEGIN INSERT INTO dbo.DoseQuantity  ([NameVN],[NameEN],[Notation],[IsActive]) VALUES (N'Tương đương liều môi trường ' ,'Ambient Dose Equivalent', N'H*(10)',1); END															
if not exists (SELECT * FROM  dbo.DoseQuantity where [NameVN] = N'Tương đương liều cá nhân') BEGIN INSERT INTO dbo.DoseQuantity  ([NameVN],[NameEN],[Notation],[IsActive]) VALUES (N'Tương đương liều cá nhân ' ,'Personal Dose Equivalent', N'Hₚ(10)',1); END															
if not exists (SELECT * FROM  dbo.DoseQuantity where [NameVN] = N'Liều hấp thụ') BEGIN INSERT INTO dbo.DoseQuantity  ([NameVN],[NameEN],[Notation],[IsActive]) VALUES (N'Liều hấp thụ ' ,'Absorbed Dose', 'D',1); END															
if not exists (SELECT * FROM  dbo.DoseQuantity where [NameVN] = N'Liều môi trường') BEGIN INSERT INTO dbo.DoseQuantity  ([NameVN],[NameEN],[Notation],[IsActive]) VALUES (N'Liều môi trường ' ,'Ambient Dose', N'H*',1); END															
if not exists (SELECT * FROM  dbo.DoseQuantity where [NameVN] = N'Liều cá nhân') BEGIN INSERT INTO dbo.DoseQuantity  ([NameVN],[NameEN],[Notation],[IsActive]) VALUES (N'Liều cá nhân ' ,'Personal Dose', N'Hₚ',1); END															
																						
end

begin   -- TM
if not exists (SELECT * FROM  dbo.TM where [Name] = N'Hồ Quang Tuấn') BEGIN INSERT INTO dbo.TM ([Name]) VALUES (N'Hồ Quang Tuấn'); END
if not exists (SELECT * FROM  dbo.TM where [Name] = N'Bùi Đức Kỳ') BEGIN INSERT INTO dbo.TM ([Name]) VALUES (N'Bùi Đức Kỳ'); END
end

begin  -- SensorType
if not exists (SELECT * FROM  dbo.SensorType where [Name] = N'Ống đếm GM') BEGIN INSERT INTO dbo.SensorType ([Name],[IsActive]) VALUES (N'Ống đếm GM',1); END
if not exists (SELECT * FROM  dbo.SensorType where [Name] = N'Đầu dò nhấp nháy') BEGIN INSERT INTO dbo.SensorType ([Name],[IsActive]) VALUES (N'Đầu dò nhấp nháy',1); END
if not exists (SELECT * FROM  dbo.SensorType where [Name] = N'Buồng ion hóa') BEGIN INSERT INTO dbo.SensorType ([Name],[IsActive]) VALUES (N'Buồng ion hóa',1); END
if not exists (SELECT * FROM  dbo.SensorType where [Name] = N'Diode bán dẫn') BEGIN INSERT INTO dbo.SensorType ([Name],[IsActive]) VALUES (N'Diode bán dẫn',1); END
if not exists (SELECT * FROM  dbo.SensorType where [Name] = N'Ống đếm tỷ lệ') BEGIN INSERT INTO dbo.SensorType ([Name],[IsActive]) VALUES (N'Ống đếm tỷ lệ',1); END
if not exists (SELECT * FROM  dbo.SensorType where [Name] = N'Tinh thể NaI') BEGIN INSERT INTO dbo.SensorType ([Name],[IsActive]) VALUES (N'Tinh thể NaI',1); END
if not exists (SELECT * FROM  dbo.SensorType where [Name] = N'Tinh thể HPGe') BEGIN INSERT INTO dbo.SensorType ([Name],[IsActive]) VALUES (N'Tinh thể HPGe',1); END
if not exists (SELECT * FROM  dbo.SensorType where [Name] = N'Ống đếm chứa khí He-3') BEGIN INSERT INTO dbo.SensorType ([Name],[IsActive]) VALUES (N'Ống đếm chứa khí He-3',1); END
if not exists (SELECT * FROM  dbo.SensorType where [Name] = N'Tinh thể LiI(Eu)') BEGIN INSERT INTO dbo.SensorType ([Name],[IsActive]) VALUES (N'Tinh thể LiI(Eu)',1); END
if not exists (SELECT * FROM  dbo.SensorType where [Name] = N'N/A') BEGIN INSERT INTO dbo.SensorType ([Name],[IsActive]) VALUES (N'N/A',1); END

end

begin  -- MachineType
if not exists (SELECT * FROM  dbo.MachineType where [Name] = N'Đo suất liều') BEGIN INSERT INTO dbo.MachineType ([Name],[IsActive]) VALUES (N'Đo suất liều',1); END
if not exists (SELECT * FROM  dbo.MachineType where [Name] = N'Đo liều') BEGIN INSERT INTO dbo.MachineType ([Name],[IsActive]) VALUES (N'Đo liều',1); END
if not exists (SELECT * FROM  dbo.MachineType where [Name] = N'Đo nhiễm bẩn bề mặt') BEGIN INSERT INTO dbo.MachineType ([Name],[IsActive]) VALUES (N'Đo nhiễm bẩn bề mặt',1); END

end



begin   -- User

--if not exists (SELECT * FROM  dbo.[User] WHERE [LoginName] = 'Admin') BEGIN INSERT INTO dbo.[User]  ([LoginName],[FullName],[Password],[RoleId],[IsActive]) VALUES ('Admin' , N'Admin ' ,'WVgZZJlK4DiBSMOfTVUXTQy+IdL6U0848zWYE83qp6TvPgIRCfn/x9vjecqzP08HQcnMihHCdw8qujAORNx9Yg==', 5,1); END															
--if not exists (SELECT * FROM  dbo.[User] WHERE [LoginName] = 'QMUser') BEGIN INSERT INTO dbo.[User]  ([LoginName],[FullName],[Password],[RoleId],[IsActive]) VALUES ('QMUser' , N'QMUser ' ,'/b34hjwbowrYt0PYDxmySU/rHd4eTPO89ADlxxGFZDhryi/xcAe1VEB9PovL4o0evuMJKmqwj2hYQnmt2CqU0w==', 4,1); END															
--if not exists (SELECT * FROM  dbo.[User] WHERE [LoginName] = 'TMUser') BEGIN INSERT INTO dbo.[User]  ([LoginName],[FullName],[Password],[RoleId],[IsActive]) VALUES ('TMUser' , N'TMUser ' ,'dyIaSbysQgbWiaClXRFf4U26ArDaQUiNRtJpKZdzDAY9ok3w+pyOBa5hTkC7HsQL9qDt13e60i3byXf8iJznTg==', 3,1); END															
--if not exists (SELECT * FROM  dbo.[User] WHERE [LoginName] = 'TechnicalUser') BEGIN INSERT INTO dbo.[User]  ([LoginName],[FullName],[Password],[RoleId],[IsActive]) VALUES ('TechnicalUser' , N'TechnicalUser ' ,'6E3C+VKtXBH4oiKZxnP1r5UrXmVIrgV0I/lgyCsVCvkYCJnwx5UXkx0i13ULd+cy2v7XYOV1yYSvvX0/UYEQAQ==', 2,1); END															
--if not exists (SELECT * FROM  dbo.[User] WHERE [LoginName] = 'ViewerUser') BEGIN INSERT INTO dbo.[User]  ([LoginName],[FullName],[Password],[RoleId],[IsActive]) VALUES ('ViewerUser' , N'ViewerUser ' ,'gh1L3AT7o0glM3DiTIjL+/aFhueNgQ0qMLW8+kCYpGmwTY9Lm7tEGZ3Kot6GnUhSlcHTEIXkZgwB8LzdpXIIRQ==', 1,1); END															


if not exists (SELECT * FROM  dbo.[User] WHERE [LoginName] = 'Admin') BEGIN INSERT INTO dbo.[User]  ([LoginName],[FullName],[Password],[RoleId],[IsActive]) VALUES ('Admin' , N'Admin ' ,'WVgZZJlK4DiBSMOfTVUXTQy+IdL6U0848zWYE83qp6TvPgIRCfn/x9vjecqzP08HQcnMihHCdw8qujAORNx9Yg==', 5,1); END															
if not exists (SELECT * FROM  dbo.[User] WHERE [LoginName] = 'lnThiem') BEGIN INSERT INTO dbo.[User]  ([LoginName],[FullName],[Password],[RoleId],[IsActive]) VALUES ('lnThiem' , N'Lê Ngọc Thiệm ' ,'q+tmTNslobCit/b76f1S2oPDVW5BKYwsExTklgzqKap98+GPTuxZnTCtHfWQwCNfRmuAyicp09LvHolelYJhuw==', 4,1); END															
if not exists (SELECT * FROM  dbo.[User] WHERE [LoginName] = 'hqTuan') BEGIN INSERT INTO dbo.[User]  ([LoginName],[FullName],[Password],[RoleId],[IsActive]) VALUES ('hqTuan' , N'Hồ Quang Tuấn ' ,'Qpg33dheP8YId/V227rSwJBGsaFBiCiS+gSmH7v3UGe/srMWJLKxaXixYtGn8bVjDTAZUTLkUWd2gozVxGdL4Q==', 3,1); END															
if not exists (SELECT * FROM  dbo.[User] WHERE [LoginName] = 'bdKy') BEGIN INSERT INTO dbo.[User]  ([LoginName],[FullName],[Password],[RoleId],[IsActive]) VALUES ('bdKy' , N'Bùi Đức Kỳ ' ,'mrPKbRbcgI3Y7GyXhTJ1YGYtYmDPiyLMTBcVQr44EKNCqt7iQv6QJj8syq5r5E2Kz+E99I8XDod1fMb+QgOWQA==', 3,1); END															
if not exists (SELECT * FROM  dbo.[User] WHERE [LoginName] = 'nnQuynh') BEGIN INSERT INTO dbo.[User]  ([LoginName],[FullName],[Password],[RoleId],[IsActive]) VALUES ('nnQuynh' , N'Nguyễn Ngọc Quỳnh ' ,'tMCtNNh0nlmHOpv06uidQd1FS0ckiRDKU09ytmppu0u0dqyZowqjgt8bqItNCwz3RuKNYJYDp5Klqka552nlNQ==', 2,1); END															
if not exists (SELECT * FROM  dbo.[User] WHERE [LoginName] = 'dtmLinh') BEGIN INSERT INTO dbo.[User]  ([LoginName],[FullName],[Password],[RoleId],[IsActive]) VALUES ('dtmLinh' , N'Đặng Thị Mỹ Linh ' ,'BeNMFjHskO3r+12WlQA19WT8do+abIFeOcpjNfZJb7dJ45BaLOuI3u0fuoYMEwnTNUX6JRmhkxGcR8I/8iLhQA==', 2,1); END															
if not exists (SELECT * FROM  dbo.[User] WHERE [LoginName] = 'dtNhung') BEGIN INSERT INTO dbo.[User]  ([LoginName],[FullName],[Password],[RoleId],[IsActive]) VALUES ('dtNhung' , N'Dương Thị Nhung ' ,'P2tpNrpjnDjmn09SUfEYS+B7guWs2dYTDqEk2EwKgubblvNXSsFTshkdPR1ooop1EZl6q/qazCZR7pTjDmIgSw==', 2,1); END															
if not exists (SELECT * FROM  dbo.[User] WHERE [LoginName] = 'tvTrung') BEGIN INSERT INTO dbo.[User]  ([LoginName],[FullName],[Password],[RoleId],[IsActive]) VALUES ('tvTrung' , N'Trần Văn Trung ' ,'BQ+sFebCIVZkcxHJ3Yf+KgfHov0d1LAkuffI043niSoS8Q+vR5uEfcF/TUSPONsVOUWVx/Gowz+7nVmdkC6xiw==', 2,1); END															
if not exists (SELECT * FROM  dbo.[User] WHERE [LoginName] = 'ndNguyen') BEGIN INSERT INTO dbo.[User]  ([LoginName],[FullName],[Password],[RoleId],[IsActive]) VALUES ('ndNguyen' , N'Nguyễn Đăng Nguyên ' ,'79smKIES14OHozxHO7CdtyeYMIBz7tX/C+7Zz0osYsz/WmGoUg9vriHKaBPDyIr6dywcVTwUI+Ub/D5ut/i+GQ==', 2,1); END															
if not exists (SELECT * FROM  dbo.[User] WHERE [LoginName] = 'btaDuong') BEGIN INSERT INTO dbo.[User]  ([LoginName],[FullName],[Password],[RoleId],[IsActive]) VALUES ('btaDuong' , N'Bùi Thị Ánh Dương ' ,'YU8M7uEnGPjxcgFRhsIYY0zmZ2R27HhKSOPhctSc1Xo6DN7uCucjctayrOFEychlsVjWevlpywZAubG41J8Wyw==', 1,1); END															

end




