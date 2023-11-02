<?php

$userId=$_POST["User_ID"];
$endSession=$_POST["End_Session"];
$sessionId=$_POST["Session_ID"];

$servername = "localhost:3306";
$username = "fernandofg2";
$password = "RYy5VwpM8PAf";
$database = "fernandofg2";

$connection = new mysqli($servername, $username, $password, $database);


echo $endSession;

if($connection->connect_error)
{
    die("Connection failed: " . $connection->connect_error);
}
$sql = "UPDATE `Sessions` SET `endSession`= '$endSession' WHERE `sessionId`='$sessionId'";

if ($connection->query($sql) == TRUE) {
 
  echo "Hecho"; 

 } 


  
  $connection->close();

?>
