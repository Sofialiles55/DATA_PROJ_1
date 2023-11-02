<?php

$userId=$_POST["User_ID"];
$startSession=$_POST["Start_Session"];

$servername = "localhost:3306";
$username = "fernandofg2";
$password = "RYy5VwpM8PAf";
$database = "fernandofg2";

$connection = new mysqli($servername, $username, $password, $database);

if($connection->connect_error)
{
    die("Connection failed: " . $connection->connect_error);
}
$sql ="INSERT INTO `Sessions`(`userId`, `startSession`)  VALUES ('$userId', '$startSession')";

if ($connection->query($sql) == TRUE) {   
    
  $sessionId = $connection->insert_id;
  echo $sessionId;
   

  }
  
  $connection->close();

?>
