<?php
$algorithm = 'sha256';
$payload = '{"monday":"75F","tuesday":"80F"}';
$secret = 'test-secret';
$expectedMac = hash_hmac($algorithm, $payload, $secret);

$signature = 'b7412f05e981a473b5ecbdb5393afaea02a679db6d7c8e56803512ec4ba98151';
var_export(hash_equals($expectedMac, $signature));
?>
