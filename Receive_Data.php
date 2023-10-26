<?php

$name = $_POST["Name"];
$country = $_POST["Country"];
$date = $_POST["Date"];

print "Name entered: $name ";
print "Country entered: $country ";
print "Date entered: $date ";


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
    $last_id = $connection->insert_id;
    echo $last_id;
  } else {
    echo "Error: " . $sql . "<br>" . $connection->error;
  }
  
  $connection->close();

?>
