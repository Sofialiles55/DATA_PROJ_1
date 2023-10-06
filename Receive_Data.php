<?php


$ID = $_POST["Id"];
$name = $_POST["Name"];
print "Id entered: $ID ";
print "Name entered: $name ";


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

$sql = "INSERT INTO `Test_Table`(`Id`, `Name`) VALUES ('$ID','$name')";

$result = $connection->query($sql);
/*
$sql = "SELECT * FROM `Test_Table`";
$result = $connection->query($sql);

if($result->num_rows > 0)
{
    echo "<table border='1'>";
    echo "<tr><th>ID</th><th>Name</th></tr>";

    while ($row = $result->fetch_assoc())
    {
        echo "hh";// "<tr><td>" . $row["Id"] . "</td><td>" . $row["Name"] "</td></tr>";
    }
    echo "</table>";
}
else 
{
    echo"No records found";
}
*/
?>
