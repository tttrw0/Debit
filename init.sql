CREATE DATABASE Debit;

USE Debit;
CREATE TABLE `Account`(
    `Id` INT NOT NULL AUTO_INCREMENT,
    `Username` CHAR(20) NOT NULL,
    `Password` CHAR(20) NOT NULL,
    `NickName` CHAR(20) NOT NULL,
    `RegisterTime` BIGINT NOT NULL,
    PRIMARY KEY(`Id`)
);

CREATE TABLE `User`(
    `Id` INT NOT NULL AUTO_INCREMENT,
    `Name` CHAR(20) NOT NULL,
    `AccountId` INT NOT NULL,
    `MoneyAmount` DECIMAL(18,2) NOT NULL,
    `IsShare` BIT NOT NULL,
    `CreateDate` BIGINT NOT NULL,
    PRIMARY KEY(`Id`),
    CONSTRAINT `FK_User_Account` FOREIGN KEY (`AccountId`) REFERENCES `Account` (`Id`)
);

CREATE TABLE `AccountUser`(
    `UserId` INT NOT NULL,
    `AccountId` INT NOT NULL,
    PRIMARY KEY(`UserId`,`AccountId`),
    CONSTRAINT `FK_AccountUser_Account` FOREIGN KEY (`AccountId`) REFERENCES `Account` (`Id`),
    CONSTRAINT `FK_AccountUser_User` FOREIGN KEY (`UserId`) REFERENCES `User` (`Id`)
);

CREATE TABLE `BillType`(
    `Id` INT NOT NULL AUTO_INCREMENT,
    `AccountId` INT NOT NULL,
    `Name` CHAR(20),
    `IsIncome` BIT NOT NULL,
    PRIMARY KEY(`Id`),
    CONSTRAINT `FK_BillType_Account` FOREIGN KEY (`AccountId`) REFERENCES `Account` (`Id`)
);

CREATE TABLE `Bill`(
    `Id` INT NOT NULL AUTO_INCREMENT,
    `UserId` INT NOT NULL,
    `AccountId` INT NOT NULL,
    `Amount` DECIMAL(18,2) NOT NULL,
    `Type` INT,
    `IsIncome` BIT NOT NULL,
    `Date` BIGINT NOT NULL,
    `Name` NCHAR(60) NOT NULL,
    `Remark` NCHAR(60),
    PRIMARY KEY(`Id`),
    CONSTRAINT `FK_Bill_Account` FOREIGN KEY (`AccountId`) REFERENCES `Account` (`Id`),
    CONSTRAINT `FK_Bill_User` FOREIGN KEY (`UserId`) REFERENCES `User` (`Id`),
    CONSTRAINT `FK_Bill_BillType` FOREIGN KEY (`Type`) REFERENCES `BillType` (`Id`)
);