-- MySQL Workbench Forward Engineering

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION';

-- -----------------------------------------------------
-- Schema fitness_center_hci
-- -----------------------------------------------------
DROP SCHEMA IF EXISTS `fitness_center_hci` ;

-- -----------------------------------------------------
-- Schema fitness_center_hci
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `fitness_center_hci` DEFAULT CHARACTER SET utf8 COLLATE utf8_bin ;
USE `fitness_center_hci` ;

-- -----------------------------------------------------
-- Table `fitness_center_hci`.`User`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `fitness_center_hci`.`User` (
  `Id` INT NOT NULL AUTO_INCREMENT,
  `Username` VARCHAR(45) NOT NULL,
  `Password` VARCHAR(512) NOT NULL,
  `EmailAddress` VARCHAR(20) NOT NULL,
  `PhoneNumber` VARCHAR(20) NOT NULL,
  `AccountType` VARCHAR(20) NOT NULL,
  `Font` VARCHAR(45) NOT NULL,
  `Theme` TINYINT NOT NULL,
  `Mode` TINYINT NOT NULL,
  `Logged` TINYINT NOT NULL,
  PRIMARY KEY (`Id`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `fitness_center_hci`.`Member`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `fitness_center_hci`.`Member` (
  `DateOfBirth` DATE NOT NULL,
  `DateOfEnrollment` DATE NOT NULL,
  `User_Id` INT NOT NULL,
  `Name` VARCHAR(20) NOT NULL,
  `Surname` VARCHAR(20) NOT NULL,
  PRIMARY KEY (`User_Id`),
  CONSTRAINT `FK_Member_User`
    FOREIGN KEY (`User_Id`)
    REFERENCES `fitness_center_hci`.`User` (`Id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `fitness_center_hci`.`Trainer`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `fitness_center_hci`.`Trainer` (
  `User_Id` INT NOT NULL,
  `Specialization` VARCHAR(45) NOT NULL,
  `WorkingTime` VARCHAR(20) NOT NULL,
  `Name` VARCHAR(20) NOT NULL,
  `Surname` VARCHAR(20) NOT NULL,
  PRIMARY KEY (`User_Id`),
  CONSTRAINT `FK_Trainer_User`
    FOREIGN KEY (`User_Id`)
    REFERENCES `fitness_center_hci`.`User` (`Id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `fitness_center_hci`.`MembershipType`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `fitness_center_hci`.`MembershipType` (
  `IdType` INT NOT NULL AUTO_INCREMENT,
  `Name` VARCHAR(45) NOT NULL,
  `CurrentPrice` DECIMAL(10,2) NOT NULL,
  PRIMARY KEY (`IdType`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `fitness_center_hci`.`Membership`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `fitness_center_hci`.`Membership` (
  `IdMembership` INT NOT NULL AUTO_INCREMENT,
  `ExpirationDate` DATE NOT NULL,
  `Member_User_Id` INT NOT NULL,
  `MembershipType_IdType` INT NOT NULL,
  PRIMARY KEY (`IdMembership`),
  INDEX `FK_Membership_Member_idx` (`Member_User_Id` ASC) VISIBLE,
  INDEX `FK_Membership_MembershipType_idx` (`MembershipType_IdType` ASC) VISIBLE,
  CONSTRAINT `FK_Membership_Member`
    FOREIGN KEY (`Member_User_Id`)
    REFERENCES `fitness_center_hci`.`Member` (`User_Id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `FK_Membership_MembershipType`
    FOREIGN KEY (`MembershipType_IdType`)
    REFERENCES `fitness_center_hci`.`MembershipType` (`IdType`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `fitness_center_hci`.`Hall`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `fitness_center_hci`.`Hall` (
  `IdHall` INT NOT NULL AUTO_INCREMENT,
  `Name` VARCHAR(45) NOT NULL,
  `Location` VARCHAR(100) NOT NULL,
  `Capacity` INT NOT NULL,
  PRIMARY KEY (`IdHall`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `fitness_center_hci`.`TrainingSession`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `fitness_center_hci`.`TrainingSession` (
  `IdSession` INT NOT NULL AUTO_INCREMENT,
  `Session` VARCHAR(50) NOT NULL,
  `Trainer_User_Id` INT NOT NULL,
  `Hall_IdHall` INT NOT NULL,
  PRIMARY KEY (`IdSession`),
  INDEX `FK_TrainingSession_Trainer_idx` (`Trainer_User_Id` ASC) VISIBLE,
  INDEX `FK_TrainingSession_Hall_idx` (`Hall_IdHall` ASC) VISIBLE,
  CONSTRAINT `FK_TrainingSession_Trainer`
    FOREIGN KEY (`Trainer_User_Id`)
    REFERENCES `fitness_center_hci`.`Trainer` (`User_Id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `FK_TrainingSession_Hall`
    FOREIGN KEY (`Hall_IdHall`)
    REFERENCES `fitness_center_hci`.`Hall` (`IdHall`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `fitness_center_hci`.`MembershipPayment`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `fitness_center_hci`.`MembershipPayment` (
  `IdPayment` INT NOT NULL AUTO_INCREMENT,
  `BillingDate` DATE NOT NULL,
  `PaymentMethod` VARCHAR(45) NOT NULL,
  `Price` DECIMAL(10,2) NOT NULL,
  `Membership_IdMembership` INT NOT NULL,
  `IsPaid` TINYINT(1) NOT NULL,
  PRIMARY KEY (`IdPayment`),
  INDEX `FK_MembershipPayment_Membership_idx` (`Membership_IdMembership` ASC) VISIBLE,
  CONSTRAINT `FK_MembershipPayment_Membership`
    FOREIGN KEY (`Membership_IdMembership`)
    REFERENCES `fitness_center_hci`.`Membership` (`IdMembership`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `fitness_center_hci`.`Group`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `fitness_center_hci`.`Group` (
  `Name` VARCHAR(50) NOT NULL,
  `Description` TEXT NOT NULL,
  `MaxNumberOfMembers` INT NOT NULL,
  `Trainer_User_Id` INT NOT NULL,
  `User_Id` INT NOT NULL,
  PRIMARY KEY (`User_Id`),
  INDEX `FK_Group_Trainer_idx` (`Trainer_User_Id` ASC) VISIBLE,
  CONSTRAINT `FK_Group_Trainer`
    FOREIGN KEY (`Trainer_User_Id`)
    REFERENCES `fitness_center_hci`.`Trainer` (`User_Id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `FK_Group_User`
    FOREIGN KEY (`User_Id`)
    REFERENCES `fitness_center_hci`.`User` (`Id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `fitness_center_hci`.`GroupMembership`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `fitness_center_hci`.`GroupMembership` (
  `Group_User_Id` INT NOT NULL,
  `Member_User_Id` INT NOT NULL,
  `JoinDate` DATE NOT NULL,
  `LeaveDate` DATE NULL,
  PRIMARY KEY (`Group_User_Id`, `Member_User_Id`),
  INDEX `FK_Group_has_Member_Member_idx` (`Member_User_Id` ASC) VISIBLE,
  CONSTRAINT `FK_Group_has_Member_Group`
    FOREIGN KEY (`Group_User_Id`)
    REFERENCES `fitness_center_hci`.`Group` (`User_Id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `FK_Group_has_Member_Member`
    FOREIGN KEY (`Member_User_Id`)
    REFERENCES `fitness_center_hci`.`Member` (`User_Id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `fitness_center_hci`.`TrainingReservation`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `fitness_center_hci`.`TrainingReservation` (
  `IdReservation` INT NOT NULL AUTO_INCREMENT,
  `Member_User_Id` INT NOT NULL,
  `TrainingSession_IdSession` INT NOT NULL,
  PRIMARY KEY (`IdReservation`),
  INDEX `FK_TrainingSession_Member_idx` (`Member_User_Id` ASC) VISIBLE,
  INDEX `FK_TrainingReservation_TrainingSession_idx` (`TrainingSession_IdSession` ASC) VISIBLE,
  CONSTRAINT `FK_TrainingReservation_Member`
    FOREIGN KEY (`Member_User_Id`)
    REFERENCES `fitness_center_hci`.`Member` (`User_Id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `FK_TrainingReservation_TrainingSession`
    FOREIGN KEY (`TrainingSession_IdSession`)
    REFERENCES `fitness_center_hci`.`TrainingSession` (`IdSession`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `fitness_center_hci`.`Promotion`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `fitness_center_hci`.`Promotion` (
  `IdPromotion` INT NOT NULL AUTO_INCREMENT,
  `Description` TEXT NOT NULL,
  `ValidFrom` DATETIME NOT NULL,
  `ValidUntil` DATETIME NOT NULL,
  `Discount` DECIMAL(10,2) NOT NULL,
  `Member_User_Id` INT NOT NULL,
  `Membership_IdMembership` INT NOT NULL,
  PRIMARY KEY (`IdPromotion`),
  INDEX `FK_Promotion_Member_idx` (`Member_User_Id` ASC) VISIBLE,
  INDEX `FK_Promotion_Membership_idx` (`Membership_IdMembership` ASC) VISIBLE,
  CONSTRAINT `FK_Promotion_Member`
    FOREIGN KEY (`Member_User_Id`)
    REFERENCES `fitness_center_hci`.`Member` (`User_Id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `FK_Promotion_Membership`
    FOREIGN KEY (`Membership_IdMembership`)
    REFERENCES `fitness_center_hci`.`Membership` (`IdMembership`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;

 
ALTER TABLE fitness_center_hci.MembershipPayment
MODIFY COLUMN PaymentMethod VARCHAR(100) NOT NULL;


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;
