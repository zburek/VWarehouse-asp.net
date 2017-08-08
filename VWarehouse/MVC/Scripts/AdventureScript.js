/// <reference path="angular.js" />

var adventure = angular
                    .module("AdventureModule", [])
                    .controller("AdventureController", function ($scope) {


                        //#region Initialization
                        var disabelPromptPopups = false;
                        $scope.disabelPromptPopups = disabelPromptPopups;

                        var adventurer = {
                            image: "",
                            name: "Bob the Brave",
                            type: "",
                            weapon: "",
                            money: 100,
                            inventory: [],
                        }

                        $scope.adventurer = adventurer;

                        var items = [
                            { name: "Soldier", price: 30, amount: 1 },
                            { name: "Archer", price: 30, amount: 1 },
                            { name: "Knight", price: 50, amount: 1 },
                            { name: "Helmet", price: 5, amount: 1 },
                            { name: "Food", price: 5, amount: 1 },
                            { name: "Horse", price: 50, amount: 1 },
                            { name: "Rope", price: 1, amount: 1 },
                            { name: "Healing Potion", price: 10, amount: 1 },
                        ]
                        $scope.items = items;

                        document.getElementById("Soldier").style.display = "none";
                        document.getElementById("Ranger").style.display = "none";
                        document.getElementById("Barbarian").style.display = "none";
                        document.getElementById("Wizard").style.display = "none";
                        document.getElementById("Cleric").style.display = "none";

                        $scope.selectImage = function () {
                            switch ($scope.adventurer.type) {
                                case 'Soldier':
                                    $scope.adventurer.image = "/Images/soldier.png";
                                    break;

                                case 'Ranger':
                                    $scope.adventurer.image = "/Images/ranger.png";
                                    break;

                                case "Barbarian":
                                    $scope.adventurer.image = "/Images/barbarian.png";
                                    break;

                                case 'Wizard':
                                    $scope.adventurer.image = "/Images/wizard.png";
                                    break;

                                case 'Cleric':
                                    $scope.adventurer.image = "/Images/cleric.png";
                                    break;
                                default:
                                    $scope.adventurer.image = "";
                                    break;
                            }
                        }


                        $scope.showWeapon = function () {
                            switch ($scope.adventurer.type) {
                                case 'Soldier':
                                    document.getElementById("Soldier").style.display = "initial";
                                    document.getElementById("Ranger").style.display = "none";
                                    document.getElementById("Barbarian").style.display = "none";
                                    document.getElementById("Wizard").style.display = "none";
                                    document.getElementById("Cleric").style.display = "none";

                                    break;

                                case 'Ranger':
                                    document.getElementById("Ranger").style.display = "initial";
                                    document.getElementById("Soldier").style.display = "none";
                                    document.getElementById("Barbarian").style.display = "none";
                                    document.getElementById("Wizard").style.display = "none";
                                    document.getElementById("Cleric").style.display = "none";
                                    break;

                                case "Barbarian":
                                    document.getElementById("Barbarian").style.display = "initial";
                                    document.getElementById("Soldier").style.display = "none";
                                    document.getElementById("Ranger").style.display = "none";
                                    document.getElementById("Wizard").style.display = "none";
                                    document.getElementById("Cleric").style.display = "none";
                                    break;

                                case 'Wizard':

                                    document.getElementById("Wizard").style.display = "initial";
                                    document.getElementById("Soldier").style.display = "none";
                                    document.getElementById("Ranger").style.display = "none";
                                    document.getElementById("Barbarian").style.display = "none";
                                    document.getElementById("Cleric").style.display = "none";
                                    break;

                                case 'Cleric':
                                    document.getElementById("Cleric").style.display = "initial";
                                    document.getElementById("Soldier").style.display = "none";
                                    document.getElementById("Ranger").style.display = "none";
                                    document.getElementById("Barbarian").style.display = "none";
                                    document.getElementById("Wizard").style.display = "none";
                                    break;
                                default:
                                    break;
                            }
                        }

                        $scope.buy = function (item) {
                            if (item.price <= $scope.adventurer.money) {
                                for (var i = 0; i < $scope.adventurer.inventory.length; i++) {
                                    if (item.name == $scope.adventurer.inventory[i].name) {
                                        $scope.adventurer.inventory[i].amount++;
                                        $scope.adventurer.money -= item.price;
                                        return;
                                    }
                                }
                                $scope.adventurer.inventory.push({ name: item.name, price: item.price, amount: 1 });
                                $scope.adventurer.money -= item.price;
                                return;
                            }
                            else {
                                if (!$scope.disabelPromptPopups) {
                                    alert("Insufficient funds!");
                                }

                            };
                        };

                        $scope.sell = function (item) {
                            for (var i = 0; i < $scope.adventurer.inventory.length; i++) {
                                if (item.name == $scope.adventurer.inventory[i].name) {
                                    $scope.adventurer.inventory[i].amount--;
                                    $scope.adventurer.money += item.price;
                                    if ($scope.adventurer.inventory[i].amount == 0) {
                                        $scope.adventurer.inventory.splice(i, 1);
                                    }
                                    return;
                                }
                            }
                            if (!$scope.disabelPromptPopups) {
                                alert("You dont have " + item.name + " in your inventory");
                            }
                        };

                        $scope.start = function () {
                            if ($scope.adventurer.type == "" || $scope.adventurer.weapon == "") {
                                document.getElementById("startAdventure").style.display = "none";
                            } else {
                                document.getElementById("startAdventure").style.display = "initial";
                            }
                        }


                        //#endregion

                        //#region screen2

                        $scope.sendGroup = function () {
                            $scope.screen3();
                            document.getElementById("sendingGroup").style.display = "initial";
                            for (var i = 0; i < $scope.adventurer.inventory.length; i++) {
                                if ($scope.adventurer.inventory[i].name == "Soldier" || $scope.adventurer.inventory[i].name == "Archer" || $scope.adventurer.inventory[i].name == "Knight") {
                                    $scope.adventurer.inventory.splice(i, 1);
                                    i--;
                                }
                            }
                        }

                        $scope.attack = function () {
                            $scope.screen3();
                            document.getElementById("attack").style.display = "initial";
                            if ($scope.adventurer.weapon == "Shortsword" || $scope.adventurer.type == "Soldier" || $scope.adventurer.type == "Barbarian") {
                                for (var i = 0; i < $scope.adventurer.inventory.length; i++) {
                                    if ($scope.adventurer.inventory[i].name == "Soldier" || "Archer" || "Knight") {
                                        document.getElementById("attackGroupMelee").style.display = "initial";
                                        return;
                                    }
                                }
                                document.getElementById("attackAloneMelee").style.display = "initial";
                            }

                            if ($scope.adventurer.weapon == "Longbow" || $scope.adventurer.weapon == "Shortbow" || $scope.adventurer.weapon == "Crossbow") {
                                for (var i = 0; i < $scope.adventurer.inventory.length; i++) {
                                    if ($scope.adventurer.inventory[i].name == "Soldier" || "Archer" || "Knight") {
                                        document.getElementById("attackGroupRange").style.display = "initial";
                                        return;
                                    }
                                }
                                document.getElementById("attackAloneRange").style.display = "initial";
                            }

                            if ($scope.adventurer.type == "Wizard" || $scope.adventurer.type == "Cleric") {
                                for (var i = 0; i < $scope.adventurer.inventory.length; i++) {
                                    if ($scope.adventurer.inventory[i].name == "Soldier" || "Archer" || "Knight") {
                                        document.getElementById("attackGroupMagic").style.display = "initial";
                                        return;
                                    }
                                }
                                document.getElementById("attackAloneMagic").style.display = "initial";
                            }
                        }

                        $scope.run = function () {
                            $scope.screen3();
                            for (var i = 0; i < $scope.adventurer.inventory.length; i++) {
                                if ($scope.adventurer.inventory[i].name == "Horse") {
                                    document.getElementById("runWithHorse").style.display = "initial";
                                    return;
                                }
                            }
                            document.getElementById("runOnFoot").style.display = "initial";
                        }

                        //#endregion

                        //#region screen3

                        $scope.attack2 = function () {
                            $scope.screen4();
                            document.getElementById("attack2").style.display = "initial";
                            if ($scope.adventurer.weapon == "Shortsword" || $scope.adventurer.type == "Soldier" || $scope.adventurer.type == "Barbarian") {
                                document.getElementById("attackAloneMelee2").style.display = "initial";
                            } else if ($scope.adventurer.weapon == "Longbow" || $scope.adventurer.weapon == "Shortbow" || $scope.adventurer.weapon == "Crossbow") {
                                document.getElementById("attackAloneRange2").style.display = "initial";
                            } else if ($scope.adventurer.type == "Wizard" || $scope.adventurer.type == "Cleric") {
                                document.getElementById("attackAloneMagic2").style.display = "initial";
                            }
                        }

                        $scope.run2 = function () {
                            $scope.screen4();
                            $scope.adventurer.money += 100;
                            $scope.adventurer.inventory.push({ name: "Magic ring", price: 500, amount: 1 })
                            for (var i = 0; i < $scope.adventurer.inventory.length; i++) {
                                if ($scope.adventurer.inventory[i].name == "Horse") {
                                    document.getElementById("runWithHorse2").style.display = "initial";
                                    return;
                                }
                            }
                            document.getElementById("runOnFoot2").style.display = "initial";
                        }
                        //#endregion

                        $scope.screen2 = function () {
                            document.getElementById("screen1").style.display = "none";
                            document.getElementById("screen2").style.display = "initial";
                            for (var i = 0; i < $scope.adventurer.inventory.length; i++) {
                                if ($scope.adventurer.inventory[i].name == "Soldier" || $scope.adventurer.inventory[i].name == "Archer" || $scope.adventurer.inventory[i].name == "Knight") {
                                    document.getElementById("group").style.display = "initial";
                                    return;
                                }
                            }
                            document.getElementById("alone").style.display = "initial";
                        }

                        $scope.screen3 = function () {
                            document.getElementById("screen2").style.display = "none";
                            document.getElementById("screen3").style.display = "initial";
                        }

                        $scope.screen4 = function () {
                            document.getElementById("screen3").style.display = "none";
                            document.getElementById("screen4").style.display = "initial";
                        }
                    });