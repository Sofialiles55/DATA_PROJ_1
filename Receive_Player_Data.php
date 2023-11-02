<?php

$name = $_POST["Name"];
$country = $_POST["Country"];
$date = $_POST["Date"];


$servername = "localhost:3306";
$username = "fernandofg2";
$password = "RYy5VwpM8PAf";
$database = "fernandofg2";

$connection = new mysqli($servername, $username, $password, $database);

if($connection->connect_error)
{
    die("Connection failed: " . $connection->connect_error);
}

$sql = "INSERT INTO `Players`(`Name`, `Country`, `Date`) VALUES ('$name','$country', '$date')";

if ($connection->query($sql) == TRUE) {
    $userId = $connection->insert_id;
    echo $userId;
  }
  
  $connection->close();

?>
