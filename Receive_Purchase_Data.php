<?php

$userId = $_POST["User_ID"];
$sessionId=$_POST["Session_ID"];
$itemId=$_POST["Item"];
$buyDate=$_POST["Buy_Date"];


$servername = "localhost:3306";
$username = "fernandofg2";
$password = "RYy5VwpM8PAf";
$database = "fernandofg2";

$connection = new mysqli($servername, $username, $password, $database);
  

if($connection->connect_error)
{
    die("Connection failed: " . $connection->connect_error);
}

//$sql = "INSERT INTO `Purchases`(`itemId`) VALUES('$item')";
$sql = "INSERT INTO `Purchases`(`userId`, `sessionId`, `itemId`, `buyDate`) VALUES('$userId','$sessionId', '$itemId', '$buyDate')";
//$sql = "INSERT INTO `Players`(`Name`, `Country`, `Date`) VALUES ('$name','$country', '$date')";

if ($connection->query($sql) == TRUE) {

    $purchaseId = $connection->insert_id;
    echo $purchaseId;    
    
  }
  else {echo "Error";}
 
  
  $connection->close();

?>
