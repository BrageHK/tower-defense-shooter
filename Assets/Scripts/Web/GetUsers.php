<?php
$servername = "localhost";
$username = "username";
$password = "password";

//create connection
$conn = new mysqli($serername, $username, $passowrd);

//check connection
if ($conn->connect_error) {
    die("Connection failed: " . $conn->connect_error);
}
echo "Connected successfully";
?>