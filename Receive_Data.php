<?php

$name = $_POST["Name"];
$country = $_POST["Country"];
$date = $_POST["Date"];
$startSession=$_POST["Start Session"];
$endSession=$_POST["End Session"];
$sessionInProgress =$_POST["Session in progress"];
$item=$_POST["Item"];
$buyDate=$_POST["Buy Date"];


print "Name entered: $name ";
print "Country entered: $country ";
print "Date entered: $date ";
print "sessionStart entered: $startSession";
print "sessionEnd entered: $endSession";
print "Item entered: $item";

$servername = "localhost:3306";
$username = "fernandofg2";
$password = "RYy5VwpM8PAf";
$database = "fernandofg2";

$connection = new mysqli($servername, $username, $password, $database);

if($connection->connect_error)
{
    die("Connection failed: " . $connection->connect_error);
}
echo "Connected succesfully";

$sql = "INSERT INTO `Players`(`Name`, `Country`, `Date`) VALUES ('$name','$country', '$date')";

if ($connection->query($sql) === TRUE) {
    echo "New record created successfully";
    $userId = $connection->insert_id;
    echo $userId;

    if($sessionInProgress == "1")
    {
      $sql ="INSERT INTO `Sessions`(`userId`, `startSession`, `endSession`)  VALUES ('$userId', '$startSession', '$endSession')";

    }
    else
    {
      $sql = "UPDATE `Sessions`SET `endSession`= $endSession WHERE `userId`=$userID";

    }

    $sql = "INSERT INTO `Purchases` (`userId`, `itemId`, `Buy Date`) VALUES('$userId', '$item', '$buyDate')";
  }
  
  $connection->close();

?>
