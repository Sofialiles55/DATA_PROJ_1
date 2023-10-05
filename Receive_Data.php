<?php
$servername = "localhost3306";
$username = "fernandofg2";
$password = "RYy5VwpM8PAf";
$database = "fernandofg2";

$connection = new mysqli($servername, $username, $password, $database);

if($connection->connect_error)
{
    die("Connection failed: " . $connection->connect_error);
}
echo "Connected succesfully";


$sql = "SELECT * FROM `SQL_Table`";
$result = $connection->query($sql);

?>
